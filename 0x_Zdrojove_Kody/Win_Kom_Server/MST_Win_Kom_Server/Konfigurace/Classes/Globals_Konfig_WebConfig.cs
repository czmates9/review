using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Fask.MST_W_Server.Konfigurace.Classes
{
    public class Globals_Konfig_WebConfig
    {
        private static string _configFilePath = "MST_Konfig_WebConfig.xml";


        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFilePath { get { return _configFilePath; } set { _configFilePath = value; } }


        public static DataSets.Konfig_WebConfig Konfigurace;


        public static string LoadConfiguration()
        {
            return LoadConfiguration(Globals_Konfig_WebConfig.ConfigFilePath);
        }


        public static string SaveConfiguration()
        {
            return SaveConfiguration(Globals_Konfig_WebConfig.ConfigFilePath);
        }

        private static string LoadConfiguration(string FileName)
        {
            try
            {

                string FilePath = GetPath(FileName);

                //Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, FilePath);

                if (!File.Exists(FilePath))
                {
                    //Soubor neexistuje, tak vytvořit..
                    Globals_Konfig_WebConfig.CreateFile(FilePath);
                }



                //if (Konfigurace == null)
                //    Konfigurace = new DataSets.Konfig_WebConfig();

                //Konfigurace.Clear();
                //Konfigurace.ReadXml(FilePath);

                DataSets.Konfig_WebConfig tmp_Konfigurace = new DataSets.Konfig_WebConfig();
                //tmp_Konfigurace.Clear();
                tmp_Konfigurace.ReadXml(FilePath);
                Konfigurace = tmp_Konfigurace;

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string SaveConfiguration(string FileName)
        {
            try
            {

                string FilePath = GetPath(FileName);
                Konfigurace.WriteXml(FilePath);

                return "OK";
            }
            catch (Exception ex)
            { return ex.Message; }

        }

        private static void CreateFile(string FilePath)
        {
            try
            {
                // testy na existenci souboru ... 
                // TODO : base.CreateFile();

                string path = Path.GetDirectoryName(FilePath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (File.Exists(FilePath))
                    return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {

                DataSets.Konfig_WebConfig  ds = new DataSets.Konfig_WebConfig();

                ds.ConnectionString.AddConnectionStringRow(@"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=FASKPOH_BOBES_TEST;User ID=sa;Password=sasa");

                ds.Providers.AddProvidersRow(
                    @"bin\Fask.ModulePohodaXML.dll",
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty
                    );

                ds.Licenses.AddLicensesRow(
                    false,
                    1,
                    false,
                    12
                    );

                ds.Directories.AddDirectoriesRow(
                    @"Logs\ErrorDataFileDirectory\",
                    @"Logs\PrintLogDirectory\",
                    @"Logs\ProcessedDataFileDirectory\",
                    @"Logs\SoDir\",
                    @"Logs\ImagesDataFileDirectory\",
                    @"Logs\",
                    @".\PrintTemplates\"
                    );

                ds.Image.AddImageRow(
                    string.Empty,
                    string.Empty,
                    string.Empty
                    );

                ds.Logs.AddLogsRow(
                    true,
                    true,
                    true,
                    true
                    );

                ds.PaletovyListek.AddPaletovyListekRow(
                    "HP LaserJet 2430 PCL6 Class Driver", 
                    false, 
                    1250, 
                    string.Empty,
                    string.Empty,
                    string.Empty
                    );

                ds.Vyroba.AddVyrobaRow(
                    "1",
                    true,
                    true
                    );


                ds.AcceptChanges();

                ds.WriteXml(FilePath);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //private static string GetPath(string FileName)
        //{
        //    string FilePath = string.Empty;

        //    if (Path.IsPathRooted(FileName))
        //        FilePath = FileName;
        //    else
        //    {
        //        string rootpath = HttpContext.Current.Server.MapPath(@"~\Konfigurace\Konfigurace_Soubory");
        //        FilePath = Path.Combine(rootpath, FileName);
        //    }

        //    return FilePath;
        //}

        private static string GetPath(string FileName)
        {
            string FilePath = string.Empty;

            try
            {
                bool InRoot = false;

                try
                {
                    InRoot = Path.IsPathRooted(FileName);
                }
                catch (Exception ex)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vinník je: Path.IsPathRooted");
                    Logging.ExceptionHandler2.Handle(ex);
                }

                if (InRoot)
                    FilePath = FileName;
                else
                {
                    try
                    {
                        string rootpath = @"~\Konfigurace\Konfigurace_Soubory";

                        try
                        {
                            rootpath = AppDomain.CurrentDomain.BaseDirectory;
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, rootpath);
                        }
                        catch (Exception ex)
                        {

                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vinník je: AppDomain.CurrentDomain.BaseDirectory");
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, ex.Message);
                        }

                        FilePath = Path.Combine(rootpath + @"\Konfigurace\Konfigurace_Soubory", FileName);
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw;
            }

            return FilePath;
        }


    }
}