using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rezacka.Configuration;
using System.IO;
using System.Data.SqlClient;
using FASK.SledovaniVyroby.Logging;
using System.Data;

namespace FASK.SledovaniVyroby.Module.Rezacka
{
    public class RezackaConfig
    {
        private const string cfilename = "rezackaconfig.xml";

        private static string configfilename;
        public static string ConfigFileName
        {
            get { return configfilename; }
        }

        public static ConfigTable config = new ConfigTable();

        static RezackaConfig()
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
