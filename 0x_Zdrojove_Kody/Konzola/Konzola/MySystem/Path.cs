using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Konzola.MySystem
{
    public class MyPath
    {
        private static string _fullFileName = System.Reflection.Assembly.GetExecutingAssembly().Location;

        private static string _currentDirectory = Path.GetDirectoryName(_fullFileName);
        public static string CurrentDirectory
        {
            get { return _currentDirectory; }
        }

        private static string _dataDirectory = Path.Combine(CurrentDirectory, "Data");
        public static string DataDirectory
        {
            get
            {
                if (!Directory.Exists(_dataDirectory))
                    Directory.CreateDirectory(_dataDirectory);
                return _dataDirectory;
            }
        }

        private static string _filterDirectory = Path.Combine(DataDirectory, "Filter");
        public static string FilterDirectory
        {
            get
            {
                if (!Directory.Exists(_filterDirectory))
                    Directory.CreateDirectory(_filterDirectory);
                return _filterDirectory;
            }
        }

        private static string _configDirectory = Path.Combine(CurrentDirectory, "Config");
        public static string ConfigDirectory
        {
            get
            {
                if (!Directory.Exists(_configDirectory))
                    Directory.CreateDirectory(_configDirectory);
                return _configDirectory;
            }
        }

        private static string _configButtonDirectory = Path.Combine(CurrentDirectory, "ConfigButton");
        public static string ConfigButtonDirectory
        {
            get
            {
                if (!Directory.Exists(_configButtonDirectory))
                    Directory.CreateDirectory(_configButtonDirectory);
                return _configButtonDirectory;
            }
        }

        private static string _printDirectory = Path.Combine(CurrentDirectory, @"Servis\Print\Templates");
        public static string PrintDirectory
        {
            get
            {
                if (!Directory.Exists(_printDirectory))
                    Directory.CreateDirectory(_printDirectory);
                return _printDirectory;
            }
        }

        private static string _printLogDirectory = Path.Combine(CurrentDirectory, @"Logs\PrintLogDirectory");
        public static string PrintLogDirectory
        {
            get
            {
                if (!Directory.Exists(_printLogDirectory))
                    Directory.CreateDirectory(_printLogDirectory);
                return _printLogDirectory;
            }
        }
    }
}
