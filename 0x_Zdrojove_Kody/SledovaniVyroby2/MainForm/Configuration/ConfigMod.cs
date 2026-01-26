using System.Windows.Forms;
using System.IO;

namespace FASK.SledovaniVyroby.Main.Configuration
{
    /// <summary>
    /// Konfigurace modulù
    /// </summary>
    class ConfigMod
    {
        /// <summary>
        /// Nazev konfiguracniho souboru modulu
        /// </summary>
        private const string cfilename = "modulesconfig.xml";

        public const string modulenonestring = " < none > ";

        /// <summary>
        /// Cesta konfiguracniho souboru modulu - vytvoren v konstruktoru
        /// </summary>
        private static string configfilename;
        public static string ConfigFileName
        {
            get { return configfilename; }
        }

        /// <summary>
        /// Dataset pro konfiguraci - informace o jednotlivych modulech
        /// </summary>
        public static ConfigModules config = new ConfigModules();

        /// <summary>
        /// Bezparametrovy konstruktor
        /// </summary>
        static ConfigMod()
        {
            //Sestaveni cesty
            string filename = Path.GetDirectoryName(Application.ExecutablePath);
            configfilename = Path.Combine(filename, cfilename);
        }

        /// <summary>
        /// Nacteni informace o modulech
        /// </summary>
        public static void Load()
        {
            try { config.ReadXml(ConfigFileName); }
            catch { }
        }

        /// <summary>
        /// Ulozeni informaci o modulech
        /// </summary>
        public static void Save()
        {
            try { config.WriteXml(ConfigFileName); }
            catch { }
        }

        /// <summary>
        /// Tvorba nazvu modulu pro zobrazeni
        /// </summary>
        /// <param name="id">Id modulu</param>
        /// <param name="name">Jmeno modulu</param>
        /// <returns>Nazev modulu</returns>
        public static string createModuleListString(string id, string name)
        {
            return id + " : " + name;
        }
    }
}
