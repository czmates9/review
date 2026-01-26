using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Reflection;


namespace FASK.SledovaniVyroby.Main.Configuration.Update
{

    public class Updater
    {
        //[DllImport("coredll")]
        //protected static extern bool CeRunAppAtTime(string pwszAppName, ref SystemTime lpTime);


        public delegate void UpdateDone();
        //public event UpdateDone UpdateDoneEvent;
        private delegate void performUpdateDelegate();

        public delegate void UpdateError(Exception ex);
        //public event UpdateError UpdateErrorEvent;

        private const string UPDATE_FOLDER_NAME = "Fask.autoupdates";
        private const string BACKUP_FOLDER_NAME = "Fask.autobackups";
        private readonly String URL;
        private readonly String updateFilePath;
        private readonly String appPath;
        //private readonly Assembly callingAssembly;
        //private string zipFileURL;
        //private Notification notification;
        //private volatile bool abortUpdate = false;

        //private Thread t;
        private Stream s;
        //private Exception message_from_thread;
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

            if (Directory.Exists(backupDir))
                Directory.Delete(backupDir, true);
        }


        /// <summary>
        /// Metoda pomoci ktere se tahaji a zistuji verze umistene na severu
        /// </summary>
        /// <param name="ds">Dataset ktery se naplni informacema z XML souboru </param>
        public void CheckForVersion(UpdateInfo ds)
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


        //void notification_AbortUpdateEvent()
        //{
        //    this.abortUpdate = true;
        //}

        //private void startUpdate()
        //{
        //    try
        //    {
        //        message_from_thread = null;
        //        t = new Thread(new ThreadStart(performUpdate));
        //        t.Start();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        //private void cleanup()
        //{
        //    string backupDir = appPath + "\\" + BACKUP_FOLDER_NAME;
        //    string updateDir = appPath + "\\" + UPDATE_FOLDER_NAME;

        //    this.removeFile(updateFilePath);
        //    if (Directory.Exists(updateDir))
        //        Directory.Delete(updateDir, true);
        //}


        //private void performUpdate()
        //{
        //    try
        //    {
        //        string url = (string)zipFileURL;


        //        String fullAppName = Assembly.GetExecutingAssembly().GetName().CodeBase;
        //        String appPath = Path.GetDirectoryName(fullAppName);
        //        String updateDir = appPath + "\\" + UPDATE_FOLDER_NAME;
        //        String updateFilename = getFilename(url);
        //        string updateZip = updateDir + "\\" + updateFilename;
        //        // Create directory to store update files
        //        Directory.CreateDirectory(updateDir);
        //        TransferManager tm = new TransferManager();
        //        tm.AddObserver(notification);
        //        Stream s;

        //        tm.downloadFile(url, out s, updateZip, notification.trans);
        //        if (s != null)
        //            s.Close();

        //        if (!abortUpdate)
        //        {
        //            using (ZipFile zip1 = ZipFile.Read(updateZip))
        //            {
        //                foreach (ZipEntry e in zip1)
        //                {
        //                    e.Extract(updateDir, ExtractExistingFileAction.OverwriteSilently);
        //                }
        //            }

        //            File.Delete(updateZip);
        //            string backupDir = appPath + "\\" + BACKUP_FOLDER_NAME;
        //            if (Directory.Exists(backupDir))
        //                Directory.Delete(backupDir, true);
        //            Directory.CreateDirectory(backupDir);
        //            foreach (string filepath in Directory.GetFiles(updateDir))
        //            {
        //                string originalFile = appPath + "\\" + getFilenameFromPath(filepath);
        //                if (File.Exists(originalFile))
        //                {
        //                    string backupFilepath = backupDir + "\\" + getFilenameFromPath(filepath);
        //                    File.Move(originalFile, backupFilepath);
        //                    File.Move(filepath, originalFile);
        //                }
        //            }
        //            OnUpdateDone();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message == "The remote server returned an error: (404) Not Found.")
        //            OnUpdateError(new Exception("Nenalezen soubor .zip na serveru."));
        //        else
        //            OnUpdateError(ex);

        //    }
        //}

        //private string getFilename(String url)
        //{
        //    char[] delim = { '/' };
        //    string[] a = url.Split(delim);
        //    return a[a.Length - 1];
        //}

        //private string getFilenameFromPath(String path)
        //{
        //    char[] delim = { '\\' };
        //    string[] a = path.Split(delim);
        //    return a[a.Length - 1];
        //}

        //protected void OnUpdateDone()
        //{
        //    if (UpdateDoneEvent != null)
        //    {
        //        UpdateDoneEvent();
        //    }
        //}

        //protected void OnUpdateError(Exception ex)
        //{
        //    if (UpdateErrorEvent != null)
        //    {
        //        UpdateErrorEvent(ex);
        //    }
        //}

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
