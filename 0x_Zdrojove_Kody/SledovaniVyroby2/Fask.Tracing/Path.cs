using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Fask.MyPath
{
    public class Path
    {

        /// <summary>
        /// Cesta do korenoveho adresare.
        /// </summary>
        private static string _rootPath = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).AbsolutePath;
        public static string RootPath
        {
            get { return _rootPath; }
            set { _rootPath = value; }
        }

        // ErrorLogFile
        private static string _errorLogFile = string.Empty;
        public static string ErrorLogFile
        {
            get { return System.IO.Path.Combine(RootPath, @_errorLogFile); }
            set { _errorLogFile = value; }
        }

        // ProcessedDataFileDirectory
        private static string _processedDataFileDirectory = string.Empty;
        public static string ProcessedDataFileDirectory
        {
            get { return System.IO.Path.Combine(RootPath, @_processedDataFileDirectory); }
            set { _processedDataFileDirectory = value; }
        }

        // ErrorDataFileDirectory
        private static string _errorDataFileDirectory = string.Empty;
        public static string ErrorDataFileDirectory 
        {
            get { return System.IO.Path.Combine(RootPath, @_errorDataFileDirectory); }
            set { _errorDataFileDirectory = value; }
        }

        // PrintLogDirectory
        private static string _printLogDirectory = string.Empty;
        public static string PrintLogDirectory
        {
            get { return System.IO.Path.Combine(RootPath, @_printLogDirectory); }
            set { _printLogDirectory = value; }
        }

        private static string _statusObjectDirectory = string.Empty;
        public static string StatusObjectDirectory
        {
            get { return System.IO.Path.Combine(RootPath, @_statusObjectDirectory); }
            set { _statusObjectDirectory = value; }
        }

        // ImagesDataFileDirectory
        private static string _imagesDataFileDirectory = string.Empty;
        public static string ImagesDataFileDirectory
        {
            get { return System.IO.Path.Combine(RootPath, @_imagesDataFileDirectory); }
            set { _imagesDataFileDirectory = value; }
        }

        // TracingDataFileDirectory
        private static string _tracingDataFileDirectory = string.Empty;
        public static string TracingDataFileDirectory
        {
            get { return System.IO.Path.Combine(RootPath, @_tracingDataFileDirectory); }
            set { _tracingDataFileDirectory = value; }
        }

		private const string _SQLiteDBsDirectory = "SQLiteDBs";
        public static string SQLiteDBsDirectory
        {
            get { return System.IO.Path.Combine(RootPath, _SQLiteDBsDirectory); }
        }

        private const string _LogsDirectory = "Logs";
		public static string LogsDirectory
		{
			get { return System.IO.Path.Combine(RootPath, _LogsDirectory); }
		}

        private const string _configDirectory = "Config";
        public static string ConfigDirectory
        {
            get { return System.IO.Path.Combine(RootPath, @_configDirectory); }
        }

        private const string _binDirectory = "bin";
        public static string BinDirectory
        {
            get { return System.IO.Path.Combine(RootPath, @_binDirectory); }
        }


        private static string _prodejPLPrintConfig = "ProdejPLPrintConfig.xml";
        public static string ProdejPLPrintConfig
        {
            get { return System.IO.Path.Combine(ConfigDirectory, @_prodejPLPrintConfig); }
        }

        private static string _prijemParams = "ProdejPLPrintConfig.xml";
        public static string PrijemParams
        {
            get { return System.IO.Path.Combine(ConfigDirectory, @_prijemParams); }
        }

        private const string _konfiguraceTerminalDirectory = @"Konfigurace\Konfigurace_Soubory_Terminal";
        public static string KonfiguraceTerminalDirectory
        {
            get { return System.IO.Path.Combine(RootPath, _konfiguraceTerminalDirectory); }
        }

      }
}
