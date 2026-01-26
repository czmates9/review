using System;
using System.IO;
using System.IO.Compression;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Fask.Aktualizace_API.Konzola.Classes
{
    public class CompressFile
    {



        public static void DeCompressFromZip(string odkudZip, string kam)
        {
            try
            {
                using (ZipArchive zip = ZipFile.OpenRead(odkudZip))
                {
                    ZipArchiveEntry zipEntry = zip.GetEntry(Path.GetFileName(kam));

                    if (zipEntry != null)
                    {
                        using (Stream entryStream = zipEntry.Open())
                        {
                            if (File.Exists(kam))
                                File.Delete(kam);

                            using (FileStream fs = new FileStream(kam, FileMode.CreateNew, FileAccess.Write))
                            {
                                entryStream.CopyTo(fs);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
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

        public static void CompressToZip(string odkud, string kamZip)
        {
            try
            {
                // Pokud zip soubor již existuje, vrátíme se
                if (File.Exists(kamZip))
                    return;

             

                // Vytvoříme proud pro zápis do zip souboru
                using (FileStream fos = new FileStream(kamZip, FileMode.Create))
                {
                    // Vytvoříme instance tříd pro práci se zipem
                    using (ZipArchive zos = new ZipArchive(fos, ZipArchiveMode.Create))
                    {
                        // Vytvoříme novou vstupní položku v zip archivu
                        ZipArchiveEntry ze = zos.CreateEntry(Path.GetFileName(odkud));

                        // Otevřeme vstupní proud pro soubor, který chceme zazipovat
                        using (FileStream vstup = new FileStream(odkud, FileMode.Open, FileAccess.Read))
                        {
                            // Vytvoříme buffer pro čtení a zápis dat
                            byte[] buffer = new byte[1024];
                            int len;

                            // Kopírujeme data ze vstupního proudu do výstupního proudu v zip souboru
                            using (Stream zipStream = ze.Open())
                            {
                                while ((len = vstup.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    zipStream.Write(buffer, 0, len);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Zpracování výjimky
                throw ex;
            }
        }



        //public static void CompressToZip(string odkud, string kamZip)
        //{
        //    try
        //    {
        //        // Pokud zip soubor již existuje, vrátíme se
        //        if (File.Exists(kamZip))
        //            return;

        //        // Vytvoříme proud pro zápis do zip souboru
        //        using (FileStream fos = new FileStream(kamZip, FileMode.Create))
        //        {
        //            // Vytvoříme instance tříd pro práci se zipem
        //            using (ZipArchive zos = new ZipArchive(fos, ZipArchiveMode.Create))
        //            {
        //                // Vytvoříme novou vstupní položku v zip archivu
        //                ZipArchiveEntry ze = zos.CreateEntry(Path.GetFileName(odkud));

        //                // Otevřeme vstupní proud pro soubor, který chceme zazipovat
        //                using (FileStream vstup = new FileStream(odkud, FileMode.Open, FileAccess.Read))
        //                {
        //                    // Vytvoříme buffer pro čtení a zápis dat
        //                    byte[] buffer = new byte[1024];
        //                    int len;

        //                    // Kopírujeme data ze vstupního proudu do výstupního proudu v zip souboru
        //                    using (Stream zipStream = ze.Open())
        //                    {
        //                        while ((len = vstup.Read(buffer, 0, buffer.Length)) > 0)
        //                        {
        //                            zipStream.Write(buffer, 0, len);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Zpracování výjimky
        //        throw ex;
        //    }
        //}

        public static void CompressToZip(string[] filepaths, string zipPath)
        {
            try
            {
                // Pokud zip soubor již existuje, vrátíme se
                if (File.Exists(zipPath))
                    return;

                // Vytvoříme proud pro zápis do zip souboru
                using (FileStream fos = new FileStream(zipPath, FileMode.Create))
                {
                    // Vytvoříme instance tříd pro práci se zipem
                    using (ZipArchive zos = new ZipArchive(fos, ZipArchiveMode.Create))
                    {
                        byte[] buffer = new byte[1024];

                        // Pro každý soubor v seznamu filepaths vytvoříme v zip archivu novou vstupní položku
                        foreach (string filepath in filepaths)
                        {
                            // Otevřeme vstupní proud pro soubor, který chceme zazipovat
                            using (FileStream vstup = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                            {
                                // Vytvoříme novou vstupní položku v zip archivu
                                ZipArchiveEntry ze = zos.CreateEntry(Path.GetFileName(filepath));

                                // Kopírujeme data ze vstupního proudu do výstupního proudu v zip souboru
                                using (Stream zipStream = ze.Open())
                                {
                                    int len;
                                    while ((len = vstup.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        zipStream.Write(buffer, 0, len);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Zpracování výjimky
                throw ex;
            }
        }


        #region old

        //public static void DeCompressFromZip(string OdkudZip, string Kam)
        //{

        //    using (Java.Util.Zip.ZipFile zip = new Java.Util.Zip.ZipFile(OdkudZip))
        //    {
        //        Java.Util.Zip.ZipEntry zipEntry = zip.GetEntry(System.IO.Path.GetFileName(Kam));

        //        System.IO.Stream vstupStream = zip.GetInputStream(zipEntry);

        //        if (System.IO.File.Exists(Kam))
        //            System.IO.File.Delete(Kam);

        //        using (System.IO.FileStream fs = new System.IO.FileStream(Kam, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write))
        //        {

        //            CopyStream(vstupStream, fs);
        //        }
        //    }
        //}

        //public static void CompressToZip(string odkud, string KamZip)
        //{
        //    byte[] buffer = new byte[1024];

        //    try
        //    {

        //        //Java.IO.FileOutputStream fos = new Java.IO.FileOutputStream("C:\\MyFile.zip");

        //        if (System.IO.File.Exists(KamZip))
        //            return;


        //        System.IO.Stream fos = new System.IO.FileStream(KamZip, System.IO.FileMode.Create);

        //        ZipOutputStream zos = new ZipOutputStream(fos);
        //        ZipEntry ze = new ZipEntry(System.IO.Path.GetFileName(odkud));
        //        zos.PutNextEntry(ze);

        //        Java.IO.FileInputStream vstup = new Java.IO.FileInputStream(odkud);

        //        int len;
        //        while ((len = vstup.Read(buffer)) > 0)
        //        {
        //            zos.Write(buffer, 0, len);
        //        }

        //        vstup.Close();
        //        zos.CloseEntry();

        //        //remember close it
        //        zos.Close();

        //        // System.out.println("Done");

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle("CompressFile", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        //Logging.Log.Write(ex, contx);
        //        //ex.printStackTrace();
        //    }

        //}

        //public static void CompressToZip(string[] filepaths, string zipPath)
        //{
        //    byte[] buffer = new byte[1024];

        //    try
        //    {

        //        //Java.IO.FileOutputStream fos = new Java.IO.FileOutputStream("C:\\MyFile.zip");

        //        if (System.IO.File.Exists(zipPath))
        //            return;


        //        System.IO.Stream fos = new System.IO.FileStream(zipPath, System.IO.FileMode.Create);

        //        ZipOutputStream zos = new ZipOutputStream(fos);
        //        foreach (string filepath in filepaths)
        //        {
        //            ZipEntry ze = new ZipEntry(System.IO.Path.GetFileName(filepath));
        //            zos.PutNextEntry(ze);

        //            Java.IO.FileInputStream vstup = new Java.IO.FileInputStream(filepath);

        //            int len;
        //            while ((len = vstup.Read(buffer)) > 0)
        //            {
        //                zos.Write(buffer, 0, len);
        //            }
        //            vstup.Close();
        //            zos.CloseEntry();
        //        }

        //        //remember close it
        //        zos.Close();
        //        // System.out.println("Done");
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle("CompressFile", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        //Logging.Log.Write(ex);
        //        //ex.printStackTrace();
        //    }
        //} 
        #endregion
    }
}