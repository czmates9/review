using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using FASK.SledovaniVyroby.Logging;
using System.Data;
using Module.Rapol.InkJet.Configuration;

namespace Module.Rapol.InkJet
{
    public class VrtackaConfig
    {
        private const string cfilename = "vrtackaconfig.xml";

        private static string configfilename;
        public static string ConfigFileName
        {
            get { return configfilename; }
        }

        public static ConfigTable config = new ConfigTable();

        static VrtackaConfig()
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
