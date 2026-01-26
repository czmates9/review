using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Reflection;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;
using Ionic.Zip;
using System.Threading;
using System.Runtime.InteropServices;


namespace MSTW_Update
{

    public class Updater
    {
        //[DllImport("coredll")]
        //protected static extern bool CeRunAppAtTime(string pwszAppName, ref SystemTime lpTime);


        public delegate void UpdateDone();
        public event UpdateDone UpdateDoneEvent;
        private delegate void performUpdateDelegate();

        public delegate void UpdateError(Exception ex);
        public event UpdateError UpdateErrorEvent;

        private const string UPDATE_FOLDER_NAME = "Fask.autoupdates";
        private const string BACKUP_FOLDER_NAME = "Fask.autobackups";
        private readonly String URL;
        private readonly String updateFilePath;
        private readonly String appPath;
        //private readonly Assembly callingAssembly;
        private string zipFileURL;
        private Notification notification;
        private volatile bool abortUpdate = false;

        private Thread t;
        private Stream s;
        private Exception message_from_thread;
        //public Fask.Upgrade.DataSet.UpdateInfo  ds_update;


        public Updater(String url, String updateFilePath)
        {
            this.URL = url;
            //this.callingAssembly = callingAssembly;
            this.appPath = Path.GetDirectoryName(updateFilePath);
            this.updateFilePath = updateFilePath;
            //ds_update = new Fask.Upgrade.DataSet.Update();
            this.assertPreviousUpdate();
        }

        private void assertPreviousUpdate()
        {
            string backupDir = appPath + "\\" + BACKUP_FOLDER_NAME;
            string updateDir = appPath + "\\" + UPDATE_FOLDER_NAME;

            if (File.Exists(updateFilePath))
                this.removeFile(updateFilePath);
            if (Directory.Exists(updateDir))
                Directory.Delete(updateDir, true);

            if (File.Exists(backupDir + "\\" + "success"))
                Directory.Delete(backupDir, true);
            else
            {
                if (Directory.Exists(backupDir))
                {
                    foreach (string f in Directory.GetFiles(backupDir))
                    {
                        File.Move(f, appPath + "\\" + getFilenameFromPath(f));
                    }
                    Directory.Delete(backupDir, true);
                }
            }
        }


        /// <summary>
        /// Metoda pomoci ktere se tahaji a zistuji verze umistene na severu
        /// </summary>
        /// <param name="ds">Dataset ktery se naplni informacema z XML souboru </param>
        public void CheckForVersion(MSTW_Update.DataSet.UpdateInfo ds)
        {
            try
            {
                TransferManager tm = new TransferManager();
                if (tm.downloadFile(URL, out s, updateFilePath, null))
                {
                    s.Seek(0, SeekOrigin.Begin);
                    ds.ReadXml(s);
                    s.Close();
                    //return this.showVersion(s);
                }
                //return null ;
            }
            catch (Exception ex)
            {
                if (ex.Message == "The remote server returned an error: (404) Not Found.")
                    throw (new Exception("Nenalezen soubor update.xml"));
                else
                    throw (ex);
            }
            finally
            {
                if (s != null)
                { s.Close(); }
                s = null;
            }
        }

        /// <summary>
        /// Jedna se o funkci pomoci ktere se nahraje nova verze
        /// </summary>
        /// <param name="udr"> Radek s vybranou verzi pro upgrade/downgrade </param>
        /// <param name="ver">aktialni verze na ctecke</param>
        /// <returns> DialogResoult stav</returns>
        public DialogResult CheckForNewVersion(MSTW_Update.DataSet.UpdateInfo.UpdateDataRow udr, Version ver)
        {
            try
            {

                if (this.showUpdateDialog(udr, ver) == DialogResult.No)
                    return DialogResult.No;

                this.cleanup();

                return DialogResult.Yes;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region puvodni metoda pro nalezeni verze

        //public DialogResult CheckForNewVersion()
        //{

        //        TransferManager tm = new TransferManager();
        //        try
        //        {

        //            if (tm.downloadFile(URL, out s, updateFilePath, null))
        //            {
        //                s.Seek(0, SeekOrigin.Begin);

        //                if (this.showUpdateDialog(s) == DialogResult.No)
        //                    return DialogResult.No;

        //                s.Close();
        //                this.cleanup();
        //            }
        //            return DialogResult.Yes;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw (ex);
        //        }
        //        finally
        //        {
        //            if (s != null)
        //            { s.Close(); }
        //            s = null;
        //        }
        //}
        #endregion

        #region puvodni verze na parsing XML
        //protected void showVersion(Stream file)
        //{
        //    try
        //    {
        //        ds_update.UpdateData.ReadXml(file);

        //        //Version currentVersion = callingAssembly.GetName().Version;
        //        XmlDocument xDoc = new XmlDocument();
        //        xDoc.Load(updateFilePath);



        //        XmlNodeList modules = xDoc.GetElementsByTagName("download");
        //        //XmlNodeList versions = xDoc.GetElementsByTagName("version");
        //        //XmlNodeList message = xDoc.GetElementsByTagName("message");
        //        //XmlNodeList link = xDoc.GetElementsByTagName("link");

        //        //string str = null;

        //        for (int i = 0; i <= modules.Count - 1; i++) 
        //        {
        //            Fask.Upgrade.DataSet.Update.UpdateDataRow row = ds_update.UpdateData.NewUpdateDataRow();

        //            row.Name = modules[i].Attributes["name"].Value;

        //            XmlNodeList childNodes = modules[i].ChildNodes;

        //            string major = childNodes.Item(0).Attributes["major"].Value;
        //            string minor = childNodes.Item(0).Attributes["minor"].Value;
        //            string build = childNodes.Item(0).Attributes["build"].Value;
        //            string revision = childNodes.Item(0).Attributes["revision"].Value;

        //            row.Version = major.Trim() + "." + minor.Trim() + "." + build.Trim() + "." + revision.Trim();

        //            row.Message = childNodes.Item(1).InnerText;

        //            row.Link = childNodes.Item(2).InnerText;


        //            //System.Xml.XmlAttribute verze = modules[i].Attributes["version"];
        //            //System.Xml.XmlAttribute sprava = modules[i].Attributes["message"];
        //            //System.Xml.XmlAttribute cesta = modules[i].Attributes["link"];


        //            ds_update.UpdateData.AddUpdateDataRow(row);
        //        }

        //        ds_update.UpdateData.AcceptChanges();

        //        //XmlNodeList a = modules[0].ChildNodes;


        //        //int major = int.Parse(versions[0].Attributes["major"].Value);
        //        //int minor = int.Parse(versions[0].Attributes["minor"].Value);
        //        //int build = int.Parse(versions[0].Attributes["build"].Value);
        //        //int revision = int.Parse(versions[0].Attributes["revision"].Value);

        //        //Version newVersion = new Version(0, 0, 0,0);

        //        return ds_update;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        #endregion

        /// <summary>
        /// metoda slouzici pro zobrazeni stahovaniho dialogu
        /// </summary>
        /// <param name="udr"> Radek s vybranou verzi pro upgrade/downgrade </param>
        /// <param name="ver">aktialni verze na ctecke</param>
        /// <returns></returns>
        protected DialogResult showUpdateDialog(MSTW_Update.DataSet.UpdateInfo.UpdateDataRow udr, Version ver)
        {
            
           return DialogResult.Cancel;
        //    try
        //    {

        //        this.zipFileURL = udr.Link;

        //        using (notification = new Notification(udr.Name, udr.Message, udr.Version, ver, this))
        //        {
        //            notification.AbortUpdateEvent += new Notification.AbortUpdate(notification_AbortUpdateEvent);
        //            notification.StartUpdateEvent += new Notification.StartUpdate(startUpdate);
        //            DialogResult res = notification.ShowDialog();

        //            if (res == DialogResult.Yes)
        //            {
        //                string backupDir = appPath + "\\" + BACKUP_FOLDER_NAME;
        //                File.Create(backupDir + "\\" + "success");
        //                return res;
        //            }
        //            else
        //            {
        //                return res;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        }


        #region
        //    /// <summary>
        ///// puvodni parsovani streamu XML souboru
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns></returns>
        //    protected DialogResult showUpdateDialog(Stream file)
        //    {
        //          try
        //        {
        //      Version currentVersion = callingAssembly.GetName().Version;
        //      XmlDocument xDoc = new XmlDocument();
        //      xDoc.Load(updateFilePath);
        //      XmlNodeList modules = xDoc.GetElementsByTagName("download");
        //      XmlNodeList versions = xDoc.GetElementsByTagName("version");


        //        int major = int.Parse(versions[0].Attributes["major"].Value);
        //        int minor = int.Parse(versions[0].Attributes["minor"].Value);
        //        int build = int.Parse(versions[0].Attributes["build"].Value);
        //        int revision = int.Parse(versions[0].Attributes["revision"].Value);
        //        int majorRevision = int.Parse(versions[0].Attributes["majorRevision"].Value);
        //        int minorRevision = int.Parse(versions[0].Attributes["minorRevision"].Value);

        //        Version newVersion = new Version(major, minor, build, revision);


        //        if (currentVersion.CompareTo(newVersion) < 0)
        //        {

        //            XmlNodeList messages = xDoc.GetElementsByTagName("message");
        //            XmlNodeList links = xDoc.GetElementsByTagName("link");
        //            String name = modules[0].Attributes["name"].Value;
        //            String link = links[0].InnerText;
        //            String message = messages[0].InnerText;
        //            this.zipFileURL = link;

        //            using (notification = new Notification(name, message, newVersion.ToString(), currentVersion, this))
        //            {
        //                notification.AbortUpdateEvent += new Notification.AbortUpdate(notification_AbortUpdateEvent);
        //                notification.StartUpdateEvent += new Notification.StartUpdate(startUpdate);
        //                DialogResult res = notification.ShowDialog();

        //                if (res == DialogResult.Yes)
        //                {
        //                    string backupDir = appPath + "\\" + BACKUP_FOLDER_NAME;
        //                    File.Create(backupDir + "\\" + "success");
        //                    return res;
        //                }
        //                else
        //                {
        //                    return res;
        //                }

        //            }
        //        }
        //        else
        //            return DialogResult.No;

        //      }
        //        catch(Exception ex)
        //        {
        //            throw (ex);
        //        }
        //    }

        #endregion

        void notification_AbortUpdateEvent()
        {
            this.abortUpdate = true;
        }

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

        private void cleanup()
        {
            string backupDir = appPath + "\\" + BACKUP_FOLDER_NAME;
            string updateDir = appPath + "\\" + UPDATE_FOLDER_NAME;

            this.removeFile(updateFilePath);
            if (Directory.Exists(updateDir))
                Directory.Delete(updateDir, true);
        }


        private void performUpdate()
        {
            try
            {
                string url = (string)zipFileURL;


                String fullAppName = Assembly.GetExecutingAssembly().GetName().CodeBase;
                String appPath = Path.GetDirectoryName(fullAppName);
                String updateDir = appPath + "\\" + UPDATE_FOLDER_NAME;
                String updateFilename = getFilename(url);
                string updateZip = updateDir + "\\" + updateFilename;
                // Create directory to store update files
                Directory.CreateDirectory(updateDir);
                TransferManager tm = new TransferManager();
                tm.AddObserver(notification);
                Stream s;

                tm.downloadFile(url, out s, updateZip, notification.trans);
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
                    string backupDir = appPath + "\\" + BACKUP_FOLDER_NAME;
                    if (Directory.Exists(backupDir))
                        Directory.Delete(backupDir, true);
                    Directory.CreateDirectory(backupDir);
                    foreach (string filepath in Directory.GetFiles(updateDir))
                    {
                        string originalFile = appPath + "\\" + getFilenameFromPath(filepath);
                        if (File.Exists(originalFile))
                        {
                            string backupFilepath = backupDir + "\\" + getFilenameFromPath(filepath);
                            File.Move(originalFile, backupFilepath);
                            File.Move(filepath, originalFile);
                        }
                    }
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

        protected void OnUpdateDone()
        {
            if (UpdateDoneEvent != null)
            {
                UpdateDoneEvent();
            }
        }

        protected void OnUpdateError(Exception ex)
        {
            if (UpdateErrorEvent != null)
            {
                UpdateErrorEvent(ex);
            }
        }

        #region Remove File
        protected bool removeFile(string FileName)
        {
            bool Ret = false;
            try
            {
                if (File.Exists(FileName) == false) { return true; }
                File.Delete(FileName);
                Ret = true;
            }
            catch (Exception) { throw; }
            return Ret;
        }
        #endregion

    }
}

