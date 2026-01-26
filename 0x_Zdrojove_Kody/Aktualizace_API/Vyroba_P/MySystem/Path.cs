using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Fask.Aktualizace_API.MySystem
{
    class MyPath
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

        private static string _soundDirectory = Path.Combine(CurrentDirectory, "Sounds");
        public static string SoundDirectory
        {
            get
            {
                return _soundDirectory;
            }
        }

    }
}
