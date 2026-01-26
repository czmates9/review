using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


//using System.IO;
using System.Net;
using System.Data;
using System.Threading.Tasks;



using System.Diagnostics;
using Android.Util;
//using Java.IO;
using Java.Util.Zip;
using System.IO;

namespace MES_Android
{
    public class CompressFile
    {

        public static void DeCompressFromZip(string OdkudZip, string Kam)
        {

            using (Java.Util.Zip.ZipFile zip = new Java.Util.Zip.ZipFile(OdkudZip))
            {
                Java.Util.Zip.ZipEntry zipEntry = zip.GetEntry(System.IO.Path.GetFileName(Kam));

                System.IO.Stream vstupStream = zip.GetInputStream(zipEntry);

                if (System.IO.File.Exists(Kam))
                    System.IO.File.Delete(Kam);

                using (System.IO.FileStream fs = new System.IO.FileStream(Kam, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write))
                {

                    CopyStream(vstupStream, fs);
                }
            }
        }

        private static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public static void CompressToZip(string odkud, string KamZip)
        {
            byte[] buffer = new byte[1024];

            try
            {

                //Java.IO.FileOutputStream fos = new Java.IO.FileOutputStream("C:\\MyFile.zip");

                if (System.IO.File.Exists(KamZip))
                    return;


                System.IO.Stream fos = new System.IO.FileStream(KamZip, System.IO.FileMode.Create);

                ZipOutputStream zos = new ZipOutputStream(fos);
                ZipEntry ze = new ZipEntry(System.IO.Path.GetFileName(odkud));
                zos.PutNextEntry(ze);

                Java.IO.FileInputStream vstup = new Java.IO.FileInputStream(odkud);

                int len;
                while ((len = vstup.Read(buffer)) > 0)
                {
                    zos.Write(buffer, 0, len);
                }

                vstup.Close();
                zos.CloseEntry();

                //remember close it
                zos.Close();

                // System.out.println("Done");

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("CompressFile", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //Logging.Log.Write(ex, contx);
                //ex.printStackTrace();
            }

        }

        public static void CompressToZip(string[] filepaths, string zipPath)
        {
            byte[] buffer = new byte[1024];

            try
            {

                //Java.IO.FileOutputStream fos = new Java.IO.FileOutputStream("C:\\MyFile.zip");

                if (System.IO.File.Exists(zipPath))
                    return;


                System.IO.Stream fos = new System.IO.FileStream(zipPath, System.IO.FileMode.Create);

                ZipOutputStream zos = new ZipOutputStream(fos);
                foreach (string filepath in filepaths)
                {
                    ZipEntry ze = new ZipEntry(System.IO.Path.GetFileName(filepath));
                    zos.PutNextEntry(ze);

                    Java.IO.FileInputStream vstup = new Java.IO.FileInputStream(filepath);

                    int len;
                    while ((len = vstup.Read(buffer)) > 0)
                    {
                        zos.Write(buffer, 0, len);
                    }
                    vstup.Close();
                    zos.CloseEntry();
                }

                //remember close it
                zos.Close();
                // System.out.println("Done");
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("CompressFile", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //Logging.Log.Write(ex);
                //ex.printStackTrace();
            }
        }
    }
}