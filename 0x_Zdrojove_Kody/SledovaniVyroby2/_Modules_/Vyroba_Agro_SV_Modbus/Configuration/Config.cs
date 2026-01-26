using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_SV_Modbus.Configuration
{
    public class AgroSledovaniVozikuConfig
    {

        private static string _configFilePath = "vyroba_agro_SV_Modbus_config.xml";

        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFilePath { get { return _configFilePath; } set { _configFilePath = value; } }


        public static ConfigTable config;

        public static string LoadConfiguration()
        {
            return LoadConfiguration(AgroSledovaniVozikuConfig.ConfigFilePath);
        }


        public static string SaveConfiguration()
        {
            return SaveConfiguration(AgroSledovaniVozikuConfig.ConfigFilePath);
        }



        private static string LoadConfiguration(string FileName)
        {
            try
            {

                string FilePath = GetPath(FileName);


                if (!File.Exists(FilePath))
                {
                    //Soubor neexistuje, tak vytvořit..
                    AgroSledovaniVozikuConfig.CreateFile(FilePath);
                }



                ConfigTable tmp_config = new ConfigTable();
                //tmp_config.Clear();
                tmp_config.ReadXml(FilePath);
                config = tmp_config;

                return "OK";
            }
            catch (Exception ex)
            { return ex.Message; }
        }

        private static string SaveConfiguration(string FileName)
        {
            try
            {

                string FilePath = GetPath(FileName);
                config.WriteXml(FilePath);

                return "OK";
            }
            catch (Exception ex)
            { return ex.Message; }

        }

        private static void CreateFile(string FilePath)
        {

            try
            {

                ConfigTable ds = new ConfigTable();

                string cestaKAdresari_SQLiteCommLib = (new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), "SQLiteCommLib.dll"))).LocalPath;
                string cestaKAdresari_SQLRemoteLib = (new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), "SQLRemoteLib.dll"))).LocalPath;


                ds.DB_Config.AddDB_ConfigRow(
                    "automaticke ulozeni paleta;posledni paleta 1;posledni paleta 3",
                    50,
                    100,
                    "presun na vozik;presun na streckovacku",
                    20,
                    "presun na streckovacku;presun na tisk",
                    "vaha;vaha_chyba;presun na tisk",
                    100,
                    "presun na tisk;tisk_aplikovano;tisk_neaplikovano_NP;tisk_neaplikovano_FP;tisk_chyba",
                    100,
                    20
                    );

                ds.ADAM_1.AddADAM_1Row(1025, "192.168.1.63", 1025, 350, 100, false, false, true);
                ds.ADAM_2.AddADAM_2Row(1025, "192.168.1.63", 1025, 350, 100, false, false, true);


                ds.ADAM_DI.AddADAM_DIRow(-1, -1);
                ds.ADAM_2_DI.AddADAM_2_DIRow(-1, -1, -1, -1, -1, -1);
                ds.ADAM_1_DESCRIPTION.AddADAM_1_DESCRIPTIONRow(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false);
                ds.ADAM_2_DESCRIPTION.AddADAM_2_DESCRIPTIONRow(
                    string.Empty, 
                    "Vykladka", 
                    "Tiskni", 
                    "Vaha start", 
                    "Odjezd", 
                    "Vaha konec", 
                    "Vaha chyba", 
                    string.Empty, 
                    string.Empty, 
                    string.Empty, 
                    string.Empty, 
                    string.Empty, 
                    false);

                ds.Vykladka.AddVykladkaRow(5000, 5000, 5000, 5000, 3000);

                ds.CS.AddCSRow(
                    cestaKAdresari_SQLiteCommLib,
                    cestaKAdresari_SQLRemoteLib
                    );

                ds.WEBAPI_TISK.AddWEBAPI_TISKRow(
                    "MDox",
                    "10.11.10.60:56425", // 192.168.1.69/MST_Win_Kom_Server_7_Dasenka                     
                    "api",
                    false,
                    5000,
                    false,
                    false
                    );

                ds.HesloDoKonfigurace.AddHesloDoKonfiguraceRow(string.Empty);


                ds.AcceptChanges();

                ds.WriteXml(FilePath);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        private static string GetPath(string FileName)
        {
            string FilePath = string.Empty;

            try
            {
                FilePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), FileName);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }

            return FilePath;
        }

    }
}
