using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using FASK.SledovaniVyroby.Logging;
using System.Data;
using Module.DCDIdeal.InkJet.Configuration;

namespace Module.DCDIdeal.InkJet
{
    public class Config
    {
        private const string cfilename = "config_dcdideal.xml";

        private static string configfilename;
        public static string ConfigFileName
        {
            get { return configfilename; }
        }

        public static ConfigTable config = new ConfigTable();

        static Config()
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
