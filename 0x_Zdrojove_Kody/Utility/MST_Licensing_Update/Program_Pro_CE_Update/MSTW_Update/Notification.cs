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
    public partial class Notification : Form
    {
        
        #region Globalne Promenne


        /*
        ///////////////////////////////////////////////////////////

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
        */
        #endregion
        
        #region c'Tor
        /// <summary>
        ///  c'tor pro nacteni Formu a vsech informaci z predavnych argumentu
        /// </summary>
        /// <Arguments>
        /// Predavane argumety pri spusteni .exe ve tvaru : "Version_New" "Version_Old" "AppPath"
        /// </Arguments>
        public Notification()
        {
            InitializeComponent();

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            if (!(Program.commandLineArguments.Length <= 0))
            {

                this.Version_New = Program.commandLineArguments[0];
                this.Version_Old = Program.commandLineArguments[1];
                this.AppPath = Program.commandLineArguments[2];

                String updateFilePath = Path.Combine(this._appPath, this._nameXmlFile);

                MSTW_Update.DataSet.UpdateInfo ds = new MSTW_Update.DataSet.UpdateInfo();
                ds.ReadXml(updateFilePath);

                //mnelo by vzdy najit jenom jeden radek, ten ktery byl vybran na ctecke
                var rows = ds.UpdateData.Where(x => x.Version == this._version_new);

                MSTW_Update.DataSet.UpdateInfo.UpdateDataRow row = rows.First();

                //nacteni informaci do Label a Textbox pro uzivatele
                this.appname_label.Text = row.Name;
                this.version_label.Text = row.Version;
                this.message_textbox.Text = row.Message;
                this.ZipFileURL = row.Link;

                this.ActualVersion = new Version(this._version_old);
                this.NewVersion = new Version(this._version_new);

                #region Farebne porovnanie verzi a uprava textu

                if (this.NewVersion > this.ActualVersion)
                {
                    this.appname_label.ForeColor = Color.Green;
                    this.version_label.ForeColor = Color.Green;
                    this.label2.Text = this._text_NewVersion;
                }
                else if (this.NewVersion < this.ActualVersion)
                {
                    this.appname_label.ForeColor = Color.Red;
                    this.version_label.ForeColor = Color.Red;
                    this.label2.Text = this.Text_OldVersion;
                }
                else
                {
                    this.appname_label.ForeColor = Color.Black;
                    this.version_label.ForeColor = Color.Black;
                    this.label2.Text = this.Text_SameVersion;
                }

                #endregion

                //Sluzi pro prenašeni hodnoty staženi do progressbaru pomoci delegata(asi)
                trans = new TransferManager.TransferProgress(UpdateProgressBar);

                //Done event po uspesnem update
                this.UpdateDoneEvent += new UpdateDone(updater_UpdateDoneEvent);

                //error event pokud nastane nejaka chyba
                this.UpdateErrorEvent += new UpdateError(updater_UpdateErrorEvent);

                //start update event, nastartuje sa tym cely update
                this.StartUpdateEvent += new StartUpdate(startUpdate);

     

            }
            else
            {
                label1.Text = "DEMO VERZE";
            }
        }

        #endregion

        #region Button Click Event

        private void btn_Install_Click(object sender, EventArgs e)
        {
            try
            {
                if (StartUpdateEvent != null)
                {
                    Button button = (Button)sender;
                    button.Visible = false;
                    button.Dock = DockStyle.None;
                    progressBar1.Dock = DockStyle.Top;
                    progressBar1.Visible = true;
                    StartUpdateEvent();
                    this._updateStarted = true;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            if (this._updateStarted)
            {
                if (AbortUpdateEvent != null)
                    AbortUpdateEvent();
            }

            //?? Co delat??
            //this.DialogResult = DialogResult.No;

            this.Close();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            if (this._updateStarted)
            {
                //?? co delata??
                //this.DialogResult = DialogResult.Yes;
            }
        }


        #endregion

        #region Eventy
        /*
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
                    Directory.Delete(backupDir,true);

                if (Directory.Exists(autoupdateDir))
                    Directory.Delete(autoupdateDir,true);
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
        */
        #endregion

        #region Vytvoreni vlakna pro stazeni a update
        /*
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
        */
        /// <summary>
        /// Metoda ktera ve vlaknu provadi update
        /// </summary>
        /*
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
        */
        
        #region pomocne metody pro getFileName
        /*
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
        */
        #endregion

        #endregion

    }
}