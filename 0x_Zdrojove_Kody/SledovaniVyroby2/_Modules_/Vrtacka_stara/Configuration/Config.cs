
using System.IO;
using System.Windows.Forms;
using Vrtacka_stara.Configuration;

namespace FASK.SledovaniVyroby.Module.Vrtacka_stara
{
    class VrtackaStaraConfig
    {
        private const string cfilename = "vrtackastaraconfig.xml";

        private static string configfilename;
        public static string ConfigFileName
        {
            get { return configfilename; }
        }

        public static ConfigTable config = new ConfigTable();

        static VrtackaStaraConfig()
        {
            string filename = Path.GetDirectoryName(Application.ExecutablePath);
            configfilename = Path.Combine(filename, cfilename);

            Load();
        }

        public static void Load()
        {
            try { config.ReadXml(ConfigFileName); }
            catch { }
        }

        public static void Save()
        {
            config.WriteXml(ConfigFileName);
        }
    }
}
