using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PohodaImportVolitelneParametry.XML
{
    class PohodaComunication
    {

        private static int RunPohodaExeIniFile(string pathToINIFile)
        {
            try
            {
                MyPath.Drives.MapDrive(
                    Settings.Communicator_Drive_Mapping_Letter,
                    Settings.Comunicator_Drive_Mapping_UNCPath,
                    Settings.Comunicator_Drive_Mapping_Domain,
                    Settings.Comunicator_Drive_Mapping_User,
                    Settings.Comunicator_Drive_Mapping_Password
                    );

                Process p = new Process();
                p.StartInfo.FileName = Settings.PathToPohodaEXE;
                p.StartInfo.Arguments = "/XML " + "\"" + Settings.Pohoda_Login + "\" " + "\"" + Settings.Pohoda_Heslo + "\" " + "\"" + pathToINIFile + "\"";
                p.StartInfo.UseShellExecute = Settings.Process_UseShellExecute;
                //p.StartInfo.ErrorDialog = false;
                //p.StartInfo.LoadUserProfile = true;
                //p.StartInfo.UseShellExecute = false; //?true
                Log.Logging.Write("Pohoda spustena s temito argumenty: " + p.StartInfo.Arguments + " a z tohoto umisteni: " + p.StartInfo.FileName);

                p.Start();
                p.WaitForExit();

                int exitcode = p.ExitCode;
                if (exitcode != 0) // zalogovat, pokud je jiny nez 0=OK
                    Log.Logging.Write("Exit Code: " + p.ExitCode);

                return p.ExitCode;
            }
            catch (Exception ex)
            {
                Log.Logging.Write( ex, "Fask.ModulePohodaXML.Perlacasa, RunPohodaExeFile");
                //return ex.ToString();
                throw ex;
            }
            finally
            {
                MyPath.Drives.UnMapDrive(Settings.Communicator_Drive_Mapping_Letter);
            }
        }

        public static bool Communicate(string requestFile, out string responseFile)
        {
            responseFile = Path.Combine(Settings.PathToInputDirectory, "Response");
            responseFile = Path.Combine(responseFile, Path.GetFileName(requestFile));

            string iniFileName = Settings.PathToINIFile + Guid.NewGuid().ToString() + ".ini";
            File.Copy(Settings.PathToINIFile, iniFileName);

            ReplaceInFile(iniFileName, "$source_xml$", Path.Combine(Settings.PathToInputDirectory, requestFile));
            ReplaceInFile(iniFileName, "$Catalog$", Settings.Catalog);
            

            int exitcode = RunPohodaExeIniFile(iniFileName);

            if (exitcode != 0)
                return false;

            File.Delete(iniFileName);
            return true;
        }


        private static void ReplaceInFile(string filePath, string searchText, string replaceText)
        {
            String strFile = File.ReadAllText(filePath);

            strFile = strFile.Replace(searchText, replaceText);

            File.WriteAllText(filePath, strFile);
        }
    }
}
