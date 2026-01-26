using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using FASK.SledovaniVyroby.Logging;
using Fask.Logging;

namespace FASK.SledovaniVyroby.Main.Configuration
{
    public class Config
    {
        private const string cfilename = "vyrobaconfig.xml";

        

        /// <summary>
        /// Ci je nacitany stroj
        /// </summary>
        /// <returns></returns>
        public static bool MachineLoad()
        {
            try
            {
                if (config.Main.Count == 0 || Configuration.Config.config.Main[0].MachineID == string.Empty)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private static string configfilename;
        public static string ConfigFileName
        {
            get { return configfilename; }
        }

        public static GlobalConfig config = new GlobalConfig();

        static Config()
        {
            string filename = Path.GetDirectoryName(Application.ExecutablePath);
            configfilename = Path.Combine(filename, cfilename);
        }

        public static bool ExistiFile()
        {
            if (File.Exists(ConfigFileName))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Nacteni globalni konfigurace do tabulky
        /// </summary>
        public static void Load()
        {
            try
            {

                //Pokud nebyly nacteny zadne parametry, vlozim vychzoi (?)
                if (!ExistiFile())
                {
                    Config.config.WEBAPI.AddWEBAPIRow(
                        "MDox",
                       /* "192.168.1.121:8080", */ "192.168.1.69/MST_Win_Kom_Server_7_Dasenka",                     
                        "api",
                        false,
                        5000,
                        false
                        );

                    Config.config.Main.AddMainRow(
                        "4",
                        "SV",
                        "SV",
                        @"d:\_w\MES-Projekt\0x_Zdrojove_Kody\SledovaniVyroby2\!Build!\Vyroba\Debug\SQLiteCommLib.dll ",
                        @"d:\_w\MES-Projekt\0x_Zdrojove_Kody\SledovaniVyroby2\!Build!\Vyroba\Debug\SQLRemoteLib.dll ",
                        0,
                        "0",
                        "4",
                        "1,0",
                        string.Empty
                        );

                    Config.config.Logging.AddLoggingRow(
                        true,
                        true
                        );

                    //Ulozeni
                    Save();
                }


                config.ReadXml(ConfigFileName);
                //1.moznost pro ziskani informaci
                //2.moznost = save global configuration v gui - viz frmGlobalConfiguration.cs
                //Hned po nacteni se pokusim predat logging modulu informace - machineID, localSqlConnString
                LogConfig.MachineID = Configuration.Config.config.Main[0].MachineID;
                LogConfig.MachineType = Configuration.Config.config.Main[0].MachineType;
                LogConfig.Koeficient = decimal.Parse(Configuration.Config.config.Main[0].Koeficient);
                LogConfig.SqlConnectionStringLocal = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
                LogConfig.SqlConnectionStringGlobal = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
                LogConfig.KomServer = Configuration.Config.config.Main[0].KomServer;

              

            }
            catch (Exception ex)
            {
               // ErrorLog.Log.Write(ex.Message);
                ExceptionHandler2.Handle(ex);
            }
        }

        /// <summary>
        /// Ulozeni globalni konfigurace
        /// </summary>
        public static void Save()
        {
            config.WriteXml(ConfigFileName);
        }
    }
}
