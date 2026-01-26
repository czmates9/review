using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Fask.ModuleSql_API.API_DataSets;

namespace Fask.ModuleSql_API
{
    public class Globals_V1
    {
        private static string _configFilePath = "MST_SQL_API_Config.xml";

        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFilePath { get { return _configFilePath; } set { _configFilePath = value; } }


        public static API_V1 Konfigurace;


        public static string LoadConfiguration()
        {
            return LoadConfiguration(Globals_V1.ConfigFilePath);
        }


        public static string SaveConfiguration()
        {
            return SaveConfiguration(Globals_V1.ConfigFilePath);
        }

        private static string LoadConfiguration(string FileName)
        {
            try
            {

                string FilePath = GetPath(FileName);


                if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
                }


                if (!File.Exists(FilePath))
                {
                    //Soubor neexistuje, tak vytvořit..
                    Globals_V1.CreateFile(FilePath);
                }

                if (Konfigurace == null)
                    Konfigurace = new API_V1();

                Konfigurace.Clear();
                Konfigurace.ReadXml(FilePath);

                return "OK";
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
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
                        string rootpath = @"\Konfigurace";

                        try
                        {
                            rootpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, rootpath);
                        }
                        catch (Exception ex)
                        {
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vinník je: System.Reflection.Assembly.GetExecutingAssembly().Location");
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, ex.Message);
                        }

                        FilePath = Path.Combine(rootpath + @"\Konfigurace", FileName);
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

        private static void CreateFile(string FilePath)
        {

            try
            {

                API_V1 ds = new API_V1();

                string rootpath = @"\Konfigurace";

                try
                {
                    rootpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, rootpath);
                }
                catch (Exception ex)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vinník je: System.Reflection.Assembly.GetExecutingAssembly().Location");
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, ex.Message);
                    rootpath = string.Empty;
                }

                rootpath = Path.Combine(rootpath, rootpath + @"\Konfigurace");

                ds.Nastaveni.AddNastaveniRow(
                     "192.168.1.121:8080", // 192.168.1.69/MST_Win_Kom_Server_7_Dasenka 
                    "MDox",
                    "api",
                    false,
                    10000,
                    rootpath,
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

    }
}

