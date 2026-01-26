using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace PrintReportLibrary
{
    public class MyPath
    {
        //private static string _fullFileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
        private static string _fullFileName = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;

        private static string _currentDirectory = _fullFileName;
        public static string CurrentDirectory
        {
            get { return _currentDirectory; }
        }

        //private static string _SQLCEDBDirectory = Path.Combine(CurrentDirectory, "SQLCEDBS");
        //public static string SQLCEDBDirectory
        //{
        //    get
        //    {
        //        if (!Directory.Exists(_SQLCEDBDirectory))
        //            Directory.CreateDirectory(_SQLCEDBDirectory);
        //        return _SQLCEDBDirectory;
        //    }
        //}

        //private static string _configDirectory = Path.Combine(CurrentDirectory, "Config");
        //public static string ConfigDirectory
        //{
        //    get
        //    {
        //        if (!Directory.Exists(_configDirectory))
        //            Directory.CreateDirectory(_configDirectory);
        //        return _configDirectory;
        //    }
        //}

        //private static string _soundDirectory = Path.Combine(CurrentDirectory, "Sounds");
        //public static string SoundDirectory
        //{
        //    get
        //    {
        //        return _soundDirectory;
        //    }
        //}

        private static string _printTemplateDirectory = Path.Combine(CurrentDirectory, @"PrintTemplate");
        public static string PrintTemplateDirectory
        {
            get
            {
                return _printTemplateDirectory;
            }

            set 
            {
                _printTemplateDirectory = value;
            }
        }

        //private static string _odberateleDirectory = Path.Combine(SQLCEDBDirectory, "Odberatele.sdf");
        //public static string OdberateleDirectory
        //{
        //    get
        //    {
        //        return _odberateleDirectory;
        //    }
        //}

        //private static string _pracovniciDirectory = Path.Combine(SQLCEDBDirectory, "Pracovnici.sdf");
        //public static string PracovniciDirectory
        //{
        //    get
        //    {
        //        return _pracovniciDirectory;
        //    }
        //}

        //private static string _prodejDirectory = Path.Combine(SQLCEDBDirectory, "Prodej.sdf");
        //public static string ProdejDirectory
        //{
        //    get
        //    {
        //        return _prodejDirectory;
        //    }
        //}

        //private static string _skladyDirectory = Path.Combine(SQLCEDBDirectory, "Sklady.sdf");
        //public static string SkladyDirectory
        //{
        //    get
        //    {
        //        return _skladyDirectory;
        //    }
        //}

        //private static string _strediskaDirectory = Path.Combine(SQLCEDBDirectory, "Strediska.sdf");
        //public static string StrediskaDirectory
        //{
        //    get
        //    {
        //        return _strediskaDirectory;
        //    }
        //}

        //private static string _typDokladuDirectory = Path.Combine(SQLCEDBDirectory, "TypDokladu.sdf");
        //public static string TypDokladuDirectory
        //{
        //    get
        //    {
        //        return _typDokladuDirectory;
        //    }
        //}

        //        private static string _zboziDirectory = Path.Combine(SQLCEDBDirectory, "Zbozi.sdf");
        //public static string ZboziDirectory
        //{
        //    get
        //    {
        //        return _zboziDirectory;
        //    }
        //}


    }
}
