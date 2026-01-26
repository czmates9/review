using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using SledovaniStroju_Service.Configurace;

namespace FASK.Configurace
{
    public class Config_trida
    {

        /// <summary>
        /// Nazvev konfiguracniho souboru
        /// </summary>
        private const string cfilename = "Config.xml";

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
        public static SledovaniStroju_Service.Configurace.Configurace Config_ds = new SledovaniStroju_Service.Configurace.Configurace();


        /// <summary>
        /// Konstruktor - vytvoreni absolutni cesty konfiguracniho souboru a nacteni konfigurace
        /// </summary>
        static Config_trida()
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
            try { Config_ds.ReadXml(ConfigFileName); }
            catch { /*Napr soubor jeste neexistuje*/ }
        }

        /// <summary>
        /// Ulozeni konfiguracniho xml souboru z vnitrnich tabulek
        /// </summary>
        public static void Save()
        {
            Config_ds.WriteXml(ConfigFileName);
        }

    }
}
