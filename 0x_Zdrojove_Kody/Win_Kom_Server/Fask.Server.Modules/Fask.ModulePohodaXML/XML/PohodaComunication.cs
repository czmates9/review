using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Fask.SQL.XML
{
    public class PohodaComunication
    {
        /// <summary>
        /// Vraci Exitcode procesu
        /// </summary>
        /// <param name="pathToINIFile">cesta k ini souboru pro provedeni akce</param>
        /// <returns></returns>
        private static int RunPohodaExeIniFile(string pathToINIFile)
        {
            try
            {
                Fask.MyPath.Network.Drives.MapDrive(
                    Globals_V1.Konfigurace.ComunicatorDriveMapping[0].Letter,
                    Globals_V1.Konfigurace.ComunicatorDriveMapping[0].UNCPath,
                    Globals_V1.Konfigurace.ComunicatorDriveMapping[0].Domain,
                    Globals_V1.Konfigurace.ComunicatorDriveMapping[0].User,
                    Globals_V1.Konfigurace.ComunicatorDriveMapping[0].Password
                    );

                Process p = new Process();
				p.StartInfo.FileName = "\"" + Globals_V1.Konfigurace.PohodaInfo[0].PathToPOHODAexe + "\"";
                p.StartInfo.Arguments = "/XML " + "\"" + Globals_V1.Konfigurace.PohodaInfo[0].Login_pohoda + "\" " + "\"" + Globals_V1.Konfigurace.PohodaInfo[0].Password_pohoda + "\" " + "\"" + pathToINIFile + "\"";
                p.StartInfo.UseShellExecute = Globals_V1.Konfigurace.PohodaInfo[0].Process_UseShellExecute;
                //p.StartInfo.ErrorDialog = false;
                //p.StartInfo.LoadUserProfile = true;
                //p.StartInfo.UseShellExecute = false; //?true
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info,"Pohoda spustena s temito argumenty: " + p.StartInfo.Arguments + " a z tohoto umisteni: " + p.StartInfo.FileName);
				
				

                p.Start();
                p.WaitForExit();

				int exitcode = -1;
                exitcode = p.ExitCode;
                //if (exitcode != 0) // zalogovat, pokud je jiny nez 0=OK
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info,"Exit Code: " + p.ExitCode);

				return exitcode;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "RunPohodaExeFile", ex);
                //return ex.ToString();
                throw ex;
            }
            finally
            {
                //Fask.MyPath.Network.Drives.UnMapDrive(Properties.Settings.Default.Communicator_Drive_Mapping_Letter);
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
            try
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
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug,"File.Copy hodilo chybu");
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Request:'" + requestFile + "'");
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Response:'" + responseFile + "'");
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "PathToINIFile:'" + Globals_V1.Konfigurace.PohodaInfo[0].PathToINIFile + "'");
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "INIFileName:'" + iniFileName + "'");
                    throw ex;
                }

                ReplaceInFile(iniFileName, "$source_xml$", requestFile);
                ReplaceInFile(iniFileName, "$Catalog$", Globals_V1.Konfigurace.PohodaInfo[0].DB_Name_pohoda);

                int exitcode = RunPohodaExeIniFile(iniFileName);

                // TaD 13.7.2021 Dle JaS by exitKod nemnel kontrolovat, ale pouze se informativne loguje, duležite je že na jine urovni 
                //se kontroluje zda existuje RESPONSE


                //if (exitcode != 0)
                //    return false;

                File.Delete(iniFileName);

                return true;
            }
            catch (Exception ex)
            {
                responseFile = string.Empty;
                Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
            
        }
    }
}
