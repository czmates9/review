using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Fask.ModulePohodaXML.XML
{
    public class PohodaComunication
    {
        public const string _import_vydejka = "_import_vydejka";
        public const string _import_prijemka = "_import_prijemka";
        public const string _import_Vyroba = "_import_vyroba";
        public const string _import_dodavatel_zasoby = "_import_DZ";

        public const string _export_vydejka = "_export_vydejka";
        public const string _export_prodejka = "_export_prodejka";
        public const string _export_prevodka = "_export_prevodka";
        public const string _export_PrjateObjednavky = "_export_PrjateObjednavky";

        public const string prijem_import_prijemka = XML.PohodaComunication._import_prijemka + ".xml";
        public const string prijem_import_Vyroba = XML.PohodaComunication._import_Vyroba + ".xml";
        public const string import_dodavatel_zasoby = XML.PohodaComunication._import_dodavatel_zasoby + ".xml";

        /// <summary>
        /// Vraci Exitcode procesu
        /// </summary>
        /// <param name="pathToINIFile">cesta k ini souboru pro provedeni akce</param>
        /// <returns></returns>
        private static int RunPohodaExeIniFile(string pathToINIFile)
        {
            try
            {

                Network.Drives.MapDrive(
                    Globals_V1.Konfigurace.ComunicatorDriveMapping[0].Letter,
                    Globals_V1.Konfigurace.ComunicatorDriveMapping[0].UNCPath,
                    Globals_V1.Konfigurace.ComunicatorDriveMapping[0].Domain,
                    Globals_V1.Konfigurace.ComunicatorDriveMapping[0].User,
                    Globals_V1.Konfigurace.ComunicatorDriveMapping[0].Password
                    );

                Process p = new Process();
                p.StartInfo.FileName = Globals_V1.Konfigurace.PohodaInfo[0].PathToPOHODAexe;
                p.StartInfo.Arguments = "/XML " + "\"" + Globals_V1.Konfigurace.PohodaInfo[0].Login_pohoda + "\" " + "\"" + Globals_V1.Konfigurace.PohodaInfo[0].Password_pohoda + "\" " + "\"" + pathToINIFile + "\"";
                p.StartInfo.UseShellExecute = Globals_V1.Konfigurace.PohodaInfo[0].Process_UseShellExecute;
                //p.StartInfo.ErrorDialog = false;
                //p.StartInfo.LoadUserProfile = true;
                //p.StartInfo.UseShellExecute = false; //?true
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info,"Pohoda spustena s temito argumenty: " + p.StartInfo.Arguments + " a z tohoto umisteni: " + p.StartInfo.FileName);

                p.Start();
                p.WaitForExit();

                int exitcode = p.ExitCode;
                if (exitcode != 0) // zalogovat, pokud je jiny nez 0=OK
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info,"Exit Code: " + p.ExitCode);

                return p.ExitCode;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "RunPohodaExeFile", ex);
                //return ex.ToString();
                throw ex;
            }
            finally
            {
               // Konzola.MySystem.Network.Drives.UnMapDrive(Properties.Settings.Default.Communicator_Drive_Mapping_Letter);
            }
        }

        /// <summary>
        /// Replaces text in a file.
        /// </summary>
        /// <param name="filePath">Path of the text file.</param>
        /// <param name="searchText">Text to search for.</param>
        /// <param name="replaceText">Text to replace the search text.</param>
        private static void ReplaceInFile(string filePath, string searchText, string replaceText)
        {
            String strFile = File.ReadAllText(filePath);

            strFile = strFile.Replace(searchText, replaceText);

            File.WriteAllText(filePath, strFile);
        }

        /// <summary>
        /// Sestaveni ini souboru pro konkretni xml a jeho volani
        /// </summary> 
        /// <param name="requestFile">request filename</param>
        /// <param name="responseFile">response filename</param>
        /// <returns></returns>
        public static bool Communicate(string requestFile, out string responseFile)
        {
            responseFile = Path.Combine(Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory, "Response");
            responseFile = Path.Combine(responseFile, Path.GetFileName(requestFile));

            string iniFileName = Globals_V1.Konfigurace.PohodaInfo[0].PathToINIFile + Guid.NewGuid().ToString() + ".ini";

            try
            {
                File.Copy(Globals_V1.Konfigurace.PohodaInfo[0].PathToINIFile, iniFileName);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "File.Copy hodilo chybu");
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Request:'" + requestFile + "'");
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Response:'" + responseFile + "'");
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "PathToINIFile:'" + Globals_V1.Konfigurace.PohodaInfo[0].PathToINIFile + "'");
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "INIFileName:'" + iniFileName + "'");
                throw ex;
            }

            ReplaceInFile(iniFileName, "$source_xml$", Path.Combine(Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory, requestFile));
            ReplaceInFile(iniFileName, "$Catalog$", Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda);

            int exitcode = RunPohodaExeIniFile(iniFileName);

            if (exitcode != 0)
                return false;

            File.Delete(iniFileName);
            return true;
        }

        /// <summary>
        /// Priradi novy nazev souboru pro xml komunikaci
        /// </summary>
        /// <param name="fileidentification">postfix identifikace souboru komunikace</param>
        /// <returns>vraci unikatni nazev souboru pro komunikaci</returns>
        public static string FilenameCompose(string fileidentification)
        {
            return DateTime.Now.ToString("yyMMdd") + "_" + Guid.NewGuid() + fileidentification;
        }
    }
}
