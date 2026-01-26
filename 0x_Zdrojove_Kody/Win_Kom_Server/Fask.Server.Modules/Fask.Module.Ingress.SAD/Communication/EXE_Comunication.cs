using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Fask.Module.Ingres.SAD.Communication
{
    public class EXE_Comunication
    {
        public static void Initialize()
        {
            MapDrive();
        }

        public static void Terminate()
        {
            UnMapDrive();
        }

        public static void MapDrive()
        {
            Globals.LoadConfiguration();
            Tracing.Trac.Write("MapDrive start");

            // pokud neni nastaveno pismeno jednotky, tak se nebude mapovat ...
            if (String.IsNullOrEmpty(Globals.Konfigurace.ComunicatorDriveMapping[0].Letter))
                return;

            Tracing.Trac.Write("MapDrive Drive exists Test");
            if (!Fask.MyPath.Network.Drives.ExistsDrive(Globals.Konfigurace.ComunicatorDriveMapping[0].Letter))
            {
                Tracing.Trac.Write("MapDrive Drive not exists");
                Fask.MyPath.Network.Drives.MapDrive(
                    Globals.Konfigurace.ComunicatorDriveMapping[0].Letter,
                    Globals.Konfigurace.ComunicatorDriveMapping[0].UNCPath,
                    Globals.Konfigurace.ComunicatorDriveMapping[0].Domain,
                    Globals.Konfigurace.ComunicatorDriveMapping[0].User,
                    Globals.Konfigurace.ComunicatorDriveMapping[0].Password
                    );
            }
            Tracing.Trac.Write("MapDrive end");
        }

        public static void UnMapDrive()
        {
            //Fask.MyPath.Network.Drives.UnMapDrive(Properties.Settings.Default.Comunicator_Drive_Mapping_Letter);
        }

        /// <summary>
        /// Vraci Exitcode procesu
        /// </summary>
        /// <param name="pathToINIFile">cesta k souboru pro provedeni akce</param>
        /// <returns></returns>
        private static int RunExeFile(List<string> Parametry)
        {
            try
            {
                Globals.LoadConfiguration();
                Process p = new Process();
                p.StartInfo.LoadUserProfile = true;
                p.StartInfo.FileName = Globals.Konfigurace.EXE[0].FileName;
                p.StartInfo.WorkingDirectory = Globals.Konfigurace.EXE[0].WorkingDirectory;
                p.StartInfo.Arguments = string.Join(" ", Parametry.ToArray());
                p.StartInfo.UseShellExecute = Globals.Konfigurace.EXE[0].UseShellExecute;
                //p.StartInfo.ErrorDialog = false;
                //p.StartInfo.LoadUserProfile = true;
                //p.StartInfo.UseShellExecute = false; //?true
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug,"Komunikátor spuštěn s těmito argumenty: " + p.StartInfo.Arguments + " a z tohoto umisteni: " + p.StartInfo.WorkingDirectory + " a tento program: " + p.StartInfo.FileName);

                p.Start();
                p.WaitForExit();

                int exitcode = p.ExitCode;
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Exit Code: " + exitcode);

                if (exitcode != 0) // zalogovat, pokud je jiny nez 0=OK
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Exit Code ERR: " + exitcode);


                return exitcode;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
            }
        }


        /// <summary>
        /// Sestaveni ini souboru pro konkretni xml a jeho volani
        /// </summary>
        /// <param name="requestFile">request filename</param>
        /// <param name="responseFile">response filename</param>
        /// <returns></returns>
        public static bool Communicate(List<string> Parametry)
        {
            int exitcode = RunExeFile(Parametry);
            return exitcode == 0;
        }
    }
}
