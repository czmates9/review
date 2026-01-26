using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Fask.Logging;
using Ionic.Zip;

namespace Fask.Compressing
{
    public class Zip
    {

		private const string ZIP = ".zip";

        /// <summary>
        /// Zazipuje soubor vysledkem je soubor
        /// </summary>
        public static bool Compress(string fullfilename)
        {
            try
            {
                // ZIP souboru ... 
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
					zip.ParallelDeflateThreshold = -1;
                    zip.Comment = "Made in FASK :)";
					zip.AddFile(fullfilename, "");
					zip.Save(fullfilename + ZIP);
                    zip.Dispose();
                }

                File.Delete(fullfilename);

                return true;
            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle("Fask.Compressing.Zip", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex; // \TODO : ? return false X throw ex; ?
            }
        }


        /// <summary>
        /// Odzipuje soubor vysledkem je soubor, pokud existuje, bude prepsan
        /// </summary>
        public static bool Decompress(string fullfilenameZip)
        {
            //Logging.Log.writeErrorLog(
            //    "Decompress"
            //    + Environment.NewLine
            //    + "fullfilenameZip : " + fullfilenameZip
            //    + Environment.NewLine
            //    + "GetDirectoryName : " + Path.GetDirectoryName(fullfilenameZip)
            //    + Environment.NewLine
            //    + "GetFileNameWithoutExtension : " + Path.GetFileNameWithoutExtension(fullfilenameZip)
            //    + Environment.NewLine
            //    + "fullfilename : " + Path.Combine(Path.GetDirectoryName(fullfilenameZip), Path.GetFileNameWithoutExtension(fullfilenameZip))
            //    + Environment.NewLine
            //);


            //fullfilename 
            //fullfilename = Path.Combine(Path.GetDirectoryName(fullfilenameZip), Path.GetFileNameWithoutExtension(fullfilenameZip));

            try
            {
                var options = new ReadOptions { StatusMessageWriter = System.Console.Out };

                using (ZipFile zip = ZipFile.Read(fullfilenameZip, options))
                {
                    // This call to ExtractAll() assumes:
                    //   - none of the entries are password-protected.
                    //   - want to extract all entries to current working directory
                    //   - none of the files in the zip already exist in the directory;
                    //     if they do, the method will throw.
                    zip.ExtractAll(Path.GetDirectoryName(fullfilenameZip), ExtractExistingFileAction.OverwriteSilently);
                }
                
                File.Delete(fullfilenameZip);

                return true;
            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle("Fask.Compressing.Zip", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				throw ex; // \TODO : ? return false X throw ex; ?
            }

        }

    }
}
