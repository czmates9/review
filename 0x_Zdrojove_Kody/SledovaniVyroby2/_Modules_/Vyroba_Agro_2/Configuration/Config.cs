using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using FASK.SledovaniVyroby.Module.Vyroba_Agro.Configuration;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro
{
    /// <summary>
    /// Trida pro praci s konfiguracnim souborem pily
    /// </summary>
    public class AgroConfig
    {
        // kody udalosti v log souboru
        public const string LOG_STAV_NASTAVEN = "0";            
        public const string LOG_PRIHLASENI_SMENY = "1";
        public const string LOG_ODHLASENI_SMENY = "4";
        public const string LOG_PRIHLASENI_PRACOVNIKA = "2";
        public const string LOG_ZAPNOUT_PROHAZ = "3";
        public const string LOG_VYPNOUT_PROHAZ = "5";
        public const string LOG_ZMENA_VYROBKU = "6";
        public const string LOG_VYZVA_VLOZIT_EAN = "7";
        public const string LOG_VLOZENI_EANU_ZRUSENO = "8";
        public const string LOG_SARZE_ZMENA_HESLO = "9";
        public const string LOG_SARZE_ZMENA_HODNOTA = "10";
        public const string LOG_SARZE_ZMENA_ID = "11";
        public const string LOG_BUTTON_CLICK = "12";

        public const string LOG_ZADANI_VPH = "62";
        public const string LOG_ZADANI_VPP = "63";

        public const string LOG_ZAHALENI_ZAHAJENO = "14";
        public const string LOG_ZAHALENI_UKONCENO = "15";

        public const string LOG_SPUSTENA_HOUKACKA = "16";

        public const string LOG_EVENT11_OCURED = "17";

        public const string LOG_SPATNE_CTENI = "22";

        public const string LOG_CODE_COUNT = "29";
        public const string LOG_CODE_READ_COUNT = "25";
        public const string LOG_CODE_NOREAD_COUNT = "28";

        public const string LOG_CODE_BADREAD = "23";
        public const string LOG_CODE_TOOLONG = "24";
        public const string LOG_EVENT36_OCURED = "26";
        public const string LOG_EAN_MINUS_COUNT = "27";
        public const string LOG_FALESNE_CTENI = "30";
        public const string LOG_FALESNY_NEPRUCHOD = "31";
        public const string LOG_BEZ_CTENI = "32";
        public const string LOG_NO_UDALOST13 = "33";
        public const string LOG_NO_UDALOST14 = "34";
        public const string LOG_NO_CODE_READ = "35";
        public const string LOG_PHOTO_NOT_READ = "36";
        public const string LOG_NO_READ_CODE = "37";
        public const string LOG_FASK_EVENTS_DATA_BARCODE = "40";
        public const string LOG_FASK_EVENTS_DATA_COUNT_READ = "41";
        public const string LOG_FASK_EVENTS_DATA_COUNT_NOREAD = "42";

        //pocet sepnuti cidla
        public const string LOG_PHOTO1_COUNT = "51";   
        public const string LOG_PHOTO2_COUNT = "52";
        public const string LOG_PHOTO3_COUNT = "53";
        public const string LOG_PHOTO4_COUNT = "54";
        public const string LOG_PHOTO5_COUNT = "55";
        public const string LOG_PHOTO6_COUNT = "56";

        public const string LOG_ULOZENI_ODVOD_VYROBA = "60";
        public const string LOG_SET_LINKA_STATE = "61";
        public const string LOG_SET_PALETIZATOR_STATE = "62";
        public const string LOG_SET_SERVIS_STATE = "63";

        /// <summary>
        /// Nazvev konfiguracniho souboru
        /// </summary>
        private const string cfilename = "vyroba_agro_config.xml";

        public static string pathDllLibraryDatabase = string.Empty;


        /// <summary>
        /// Absolutni nazev konfiguracniho souboru
        /// </summary>
        private static string configfilename;
        public static string ConfigFileName
        {
            get { return configfilename; }
        }

        private static int maxEANLength = 13;
        public static int MaxEANLength
        {
            get { return maxEANLength; }
        }

        /// <summary>
        /// Tabulka s konfiguraci modulu
        /// </summary>
        public static ConfigTable config = new ConfigTable();

        /// <summary>
        /// Konstruktor - vytvoreni absolutni cesty konfiguracniho souboru a nacteni konfigurace
        /// </summary>
        static AgroConfig()
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
        /// Ulozeni konfiguracniho xml souboru z vnitrnich tabulek
        /// </summary>
        public static void Save()
        {
            config.WriteXml(ConfigFileName);
        }
    }
}
