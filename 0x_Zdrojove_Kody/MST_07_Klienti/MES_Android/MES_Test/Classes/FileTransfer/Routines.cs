using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MES_Android
{
    public class Routines
    {
        public static void DownloadDecompressDelete(string filename)
        {
            Downloading.DownloadFileFromServer(filename + Classes.DataInfo_Static.PriponaZIP);
            CompressFile.DeCompressFromZip(filename + Classes.DataInfo_Static.PriponaZIP, filename);
            FileOperations.DeleteFileOnServer(Path.GetFileName(filename + Classes.DataInfo_Static.PriponaZIP));
            FileOperations.DeleteFileOnServer(Path.GetFileName(filename));
            File.Delete(filename + Classes.DataInfo_Static.PriponaZIP);
        }

        public static void DownloadDecompressDelete(Android.Support.V7.App.AppCompatActivity activity, string filename)
        {
            Downloading.DownloadFileFromServer(activity, filename + Classes.DataInfo_Static.PriponaZIP);
            CompressFile.DeCompressFromZip(filename + Classes.DataInfo_Static.PriponaZIP, filename);
            FileOperations.DeleteFileOnServer(Path.GetFileName(filename + Classes.DataInfo_Static.PriponaZIP));
            FileOperations.DeleteFileOnServer(Path.GetFileName(filename));
            File.Delete(filename + Classes.DataInfo_Static.PriponaZIP);
        }
    }
}