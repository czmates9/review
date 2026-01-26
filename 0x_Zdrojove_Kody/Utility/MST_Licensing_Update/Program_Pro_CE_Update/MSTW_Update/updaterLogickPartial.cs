using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;
using Ionic.Zip;
using System.Diagnostics;

namespace MSTW_Update
{
    public partial class Notification
    {
        #region globalni promenne

        private volatile bool _updateStarted = false;
        public bool UpdateStarted { set { this._updateStarted = value; } get { return this._updateStarted; } }

        /// <summary>
        /// Sprava o chybe z vlakna
        /// </summary>
        private Exception message_from_thread;

        /// <summary>
        /// Nazev zlosky doktere se stahuji data pro update
        /// </summary>
        private string _update_FolderName = "Fask.autoupdates";
        public string Update_FolderName { set { this._update_FolderName = value; } get { return this._update_FolderName; } }


        /// <summary>
        /// Nazev zlosky doktere se kopiruji data jak zaloha keby se nepodaril update
        /// </summary>
        private string _backup_FolderName = "Fask.autobackups";
        public string Backup_FolderName { set { this._backup_FolderName = value; } get { return this._backup_FolderName; } }

        /// <summary>
        /// Zruseni stahovani
        /// </summary>
        private volatile bool abortUpdate = false;

        /// <summary>
        /// Vlakno v kteren se stahuji a aktualizji data
        /// </summary>
        private Thread t;

        /// <summary>
        /// Predavany argument z MSTW na jakou verzi provest update
        /// </summary>
        private string _version_new;
        public string Version_New { set { this._version_new = value; } get { return this._version_new; } }

        /// <summary>
        /// Predavany argument z MSTW z jake verze se provadi update
        /// </summary>
        private string _version_old;
        public string Version_Old { set { this._version_old = value; } get { return this._version_old; } }
        /// <summary>
        /// Cesta do slozky kde se provede update
        /// </summary>
        private string _appPath;
        public string AppPath { set { this._appPath = value; } get { return this._appPath; } }

        /// <summary>
        /// Nazev xml souboru ktery obsahuje seznam verzi na serveru a je mozne ho naciost do DataTable
        /// </summary>
        private string _nameXmlFile = "update.xml";
        public string NameXmlFile { set { this._nameXmlFile = value; } get { return this._nameXmlFile; } }

        /// <summary>
        /// Cesta k ZIP souboru umistenemu na Kom. Serveru
        /// </summary>
        private string _zipFileUrl;
        public string ZipFileURL { set { this._zipFileUrl = value; } get { return this._zipFileUrl; } }

        /// <summary>
        /// Aktualni verze programu pro update vyjadrena ale jak trida Version
        /// </summary>
        private Version _actualVersion;
        public Version ActualVersion { set { this._actualVersion = value; } get { return this._actualVersion; } }

        /// <summary>
        /// Nova verze programu pro update vyjadrena ale jak trida Version
        /// </summary>
        private Version _newVersion;
        public Version NewVersion { set { this._newVersion = value; } get { return this._newVersion; } }

        /// <summary>
        /// možnost lokalozovat text pri zobrazeni verze
        /// </summary>
        private string _text_NewVersion = "Nová verze";
        public string Text_NewVersion { set { this._text_NewVersion = value; } get { return this._text_NewVersion; } }

        /// <summary>
        /// možnost lokalozovat text pri zobrazeni verze
        /// </summary>
        private string _text_OldVersion = "Stará verze";
        public string Text_OldVersion { set { this._text_OldVersion = value; } get { return this._text_OldVersion; } }

        /// <summary>
        /// možnost lokalozovat text pri zobrazeni verze
        /// </summary>
        private string _text_SameVersion = "Stejná verze";
        public string Text_SameVersion { set { this._text_SameVersion = value; } get { return this._text_SameVersion; } }
        #endregion 
        
        #region eventy
        //netuším jak presne funguju a ci to neni zbytecne zložite
        public TransferManager.TransferProgress trans;

        public delegate void StartUpdate();
        public event StartUpdate StartUpdateEvent;

        public delegate void UpdateDone();
        public event UpdateDone UpdateDoneEvent;

        public delegate void UpdateError(Exception ex);
        public event UpdateError UpdateErrorEvent;

        public delegate void AbortUpdate();
        public event AbortUpdate AbortUpdateEvent;


        /// <summary>
        /// Event ktery je zavlonay ked nastane chyba
        /// </summary>
        /// <param name="ex">Exception je chyba ktera vznikla</param>
        void updater_UpdateErrorEvent(Exception ex)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((System.Threading.ThreadStart)delegate() { this.updater_UpdateErrorEvent(ex); });
                return;
            }

            //throw (ex);
        }


        /// <summary>
        /// Metoda ktera je volana pro zapisovani do progressbaru z jineho vlakna
        /// </summary>
        /// <param name="status"></param>
        public void UpdateProgressBar(int status)
        {
            if (this.InvokeRequired)
            {
                if (!this.IsDisposed)
                {
                    this.BeginInvoke(new TransferManager.TransferProgress(UpdateProgressBar), status);
                }
                return;
            }

            progressBar1.Value = (int)status;
        }

        /// <summary>
        /// Event ktery je zavolany uplne na konci ked uz vse probjehlo tak jak mnelo zavre za sebu tuto aplikaci a otevre aktualizovane MSTW
        /// </summary>
        void updater_UpdateDoneEvent()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UpdateDone(updater_UpdateDoneEvent), null);
                return;
            }
            //btn_Cancle.Visible = false;
            //btn_Install.Visible = false;
            //progressBar1.Visible = false;
            //buttonRestart.Visible = true;
            //btn_Cancle.Dock = DockStyle.None;
            //btn_Install.Dock = DockStyle.None;
            //progressBar1.Dock = DockStyle.None;
            //buttonRestart.Dock = DockStyle.Fill;
            string backupDir = Path.Combine(this._appPath, this._backup_FolderName);
            string autoupdateDir = Path.Combine(this._appPath, this._update_FolderName);

            try
            {
                if (Directory.Exists(backupDir))
                    Directory.Delete(backupDir, true);

                if (Directory.Exists(autoupdateDir))
                    Directory.Delete(autoupdateDir, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error...");
                throw;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(this._appPath, "MST_W.exe"); // @"\Application\mst_update\MSTW_Update.exe";
            Process.Start(startInfo);

            this.Close();
        }

        /// <summary>
        /// metoda ktera je volana ked nastane chyba
        /// </summary>
        /// <param name="ex"></param>
        protected void OnUpdateError(Exception ex)
        {
            if (UpdateErrorEvent != null)
            {
                UpdateErrorEvent(ex);
            }
        }

        /// <summary>
        /// metoda ktera se zavola ked je uspesne aktualizovany
        /// </summary>
        protected void OnUpdateDone()
        {
            if (UpdateDoneEvent != null)
            {
                UpdateDoneEvent();
            }
        }

        #endregion 

        #region logika update

        /// <summary>
        /// Vytvori sa vlakno a stahne se update a zaktualizuje se 
        /// </summary>
        private void startUpdate()
        {
            try
            {
                message_from_thread = null;
                t = new Thread(new ThreadStart(performUpdate));
                t.Start();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        private void performUpdate()
        {
            try
            {
                //string url = this._zipFileUrl;

                Cursor.Current = Cursors.WaitCursor;

                //String fullAppName = this.appPath;
                //String appPath = Path.GetDirectoryName(fullAppName);
                String updateDir = Path.Combine(this._appPath, this._update_FolderName);
                String updateFilename = getFilename(this._zipFileUrl);
                string updateZip = Path.Combine(updateDir, updateFilename);

                // Create directory to store update files
                Directory.CreateDirectory(updateDir);
                TransferManager tm = new TransferManager();
                tm.AddObserver(this);

                Stream s;

                tm.downloadFile(this._zipFileUrl, out s, updateZip, this.trans);

                if (s != null)
                    s.Close();

                if (!abortUpdate)
                {
                    using (ZipFile zip1 = ZipFile.Read(updateZip))
                    {
                        foreach (ZipEntry e in zip1)
                        {
                            e.Extract(updateDir, ExtractExistingFileAction.OverwriteSilently);
                        }
                    }

                    File.Delete(updateZip);
                    string backupDir = Path.Combine(this._appPath, this._backup_FolderName);
                    if (Directory.Exists(backupDir))
                        Directory.Delete(backupDir, true);
                    Directory.CreateDirectory(backupDir);
                    try
                    {
                        foreach (string filepath in Directory.GetFiles(updateDir))
                        {
                            string originalFile = Path.Combine(this._appPath, Path.GetFileName(filepath)); //getFilenameFromPath(filepath)
                            if (File.Exists(originalFile))
                            {
                                string backupFilepath = Path.Combine(backupDir, Path.GetFileName(filepath)); //getFilenameFromPath(filepath)
                                File.Move(originalFile, backupFilepath);
                                File.Move(filepath, originalFile);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        foreach (string f in Directory.GetFiles(backupDir))
                        {
                            if (File.Exists(f))
                                File.Delete(this._appPath + "\\" + getFilenameFromPath(f));

                            File.Move(f, this._appPath + "\\" + getFilenameFromPath(f));
                        }
                    }

                    Cursor.Current = Cursors.Default;
                    OnUpdateDone();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "The remote server returned an error: (404) Not Found.")
                    OnUpdateError(new Exception("Nenalezen soubor .zip na serveru."));
                else
                    OnUpdateError(ex);

            }
        }

        #region pomocne metody pro getFileName

        private string getFilename(String url)
        {
            char[] delim = { '/' };
            string[] a = url.Split(delim);
            return a[a.Length - 1];
        }

        private string getFilenameFromPath(String path)
        {
            char[] delim = { '\\' };
            string[] a = path.Split(delim);
            return a[a.Length - 1];
        }

        #endregion

        #endregion
    }
}
