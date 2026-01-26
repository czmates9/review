using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Configuration
{
    class AgroSledovaniVozikuConfig
    {

        /// <summary>
        /// Nazvev konfiguracniho souboru
        /// </summary>
        private const string cfilename = "vyroba_agro_SledovaniVoziku_config.xml";

        /// <summary>
        /// Absolutni nazev konfiguracniho souboru
        /// </summary>
        private static string configfilename;
        public static string ConfigFileName
        {
            get { return configfilename; }
        }

        /// <summary>
        /// Tabulka s konfiguraci modulu
        /// </summary>
        public static ConfigTable config = new ConfigTable();


        /// <summary>
        /// Konstruktor - vytvoreni absolutni cesty konfiguracniho souboru a nacteni konfigurace
        /// </summary>
        static AgroSledovaniVozikuConfig()
        {
            string filename = Path.GetDirectoryName(Application.ExecutablePath);
            configfilename = Path.Combine(filename, cfilename);
            //Nacteni
            Load();
        }

        public static bool ExistiFile()
        {
            if (File.Exists(configfilename))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Nacteni konfiguracniho xml souboru do vnitrnich tabulek
        /// </summary>
        public static void Load()
        {
            try { config.ReadXml(ConfigFileName); }
            catch { /*Napr soubor jeste neexistuje*/ }
        }

        /// <summary>
        /// Ulozeni konfiguracniho xml souboru z vnitrnich tabulek
        /// </summary>
        public static void Save()
        {
            config.WriteXml(ConfigFileName);
        }

    }
}
