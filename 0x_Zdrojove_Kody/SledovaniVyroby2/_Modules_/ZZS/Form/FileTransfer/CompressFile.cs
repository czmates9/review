using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FASK.SledovaniVyroby.Module.ZZS.Forms.FileTransfer
    
{
   public class CompressFile
    {

       public static string CompressToZip(string[] filePath, string filePathZip)
       {
           using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
           {
               try
               {
                   foreach (string item in filePath)
                   {
                       zip.AddFile(item, "");
                   }

                   zip.Comment = "Made in FASK";
                   zip.Save(filePathZip);
                   zip.Dispose();

               }
               catch (Exception ex)
               {
                   return "ERR:" + ex.Message.ToString();
               }

               return "OK";
           }
       }



       public static string CompressToZip(string filePath, string filePathZip) 
       {

           string[] arrFile = new string[] { filePath };


           return CompressToZip(arrFile, filePathZip);
       }

       public static bool DeCompressFromZip(string filePathZip, string filePath)
       {
           try
           {
               var options = new Ionic.Zip.ReadOptions { StatusMessageWriter = System.Console.Out };

               using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(filePathZip, options))
               {
                   // This call to ExtractAll() assumes:
                   //   - none of the entries are password-protected.
                   //   - want to extract all entries to current working directory
                   //   - none of the files in the zip already exist in the directory;
                   //     if they do, the method will throw.
                   zip.ExtractAll(System.IO.Path.GetDirectoryName(filePath));
               }
           }
           catch (Exception ex)
           {
               return false;
               //throw new Exception("Zip error: " + ex.Message);
           }

               return true;
           
       }


    }
}
