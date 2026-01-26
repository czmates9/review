using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using FASK.SledovaniVyroby.Logging;
using System.Data;
using Pila.Configuration;

namespace FASK.SledovaniVyroby.Module.Pila
{
    /// <summary>
    /// Trida pro praci s konfiguracnim souborem pily
    /// </summary>
    public class PilaConfig
    {
        /// <summary>
        /// Nazvev konfiguracniho souboru
        /// </summary>
        private const string cfilename = "pilaconfig.xml";

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
        static PilaConfig()
        {
            string filename = Path.GetDirectoryName(Application.ExecutablePath);
            configfilename = Path.Combine(filename, cfilename);
            //Nacteni
            Load();
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
        /// Nacteni konfiguracniho xml souboru z vnitrnich tabulek
        /// </summary>
        public static void Save()
        {
            config.WriteXml(ConfigFileName);
        }
    }
}
