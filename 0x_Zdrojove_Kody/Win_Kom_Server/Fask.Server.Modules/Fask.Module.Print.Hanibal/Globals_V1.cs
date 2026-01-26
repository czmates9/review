using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Fask.Module.Print.Hanibal
{
    public class Globals_V1
    {

        private static string _configFilePath = "Fask_Module_Print_Hanibal.xml";


        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFilePath { get { return _configFilePath; } set { _configFilePath = value; } }


        public static Datasets.Konfig Konfigurace;


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


                if (!File.Exists(FilePath))
                {
                    //Soubor neexistuje, tak vytvořit..
                    Globals_V1.CreateFile(FilePath);
                }



                //if (Konfigurace == null)
                //    Konfigurace = new Datasets.Konfig();

                //Konfigurace.Clear();
                //Konfigurace.ReadXml(FilePath);


                Datasets.Konfig tmp_Konfigurace = new Datasets.Konfig();
                //tmp_Konfigurace.Clear();
                tmp_Konfigurace.ReadXml(FilePath);
                Konfigurace = tmp_Konfigurace;

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

        private static void CreateFile(string FilePath)
        {

            try
            {

                Datasets.Konfig ds = new Datasets.Konfig();


                ds.ConnectionStrings.AddConnectionStringsRow(
                    string.Empty);

                ds.PrintParams.AddPrintParamsRow(
                    "doporučená cena",
                    "cena po slevě",
                    "cena Hanibal",
                    "H2_01.prn",
                    "H2_02.prn",
                    "H2_03.prn",
                    "^FT50,195^GB190,0,3^FS",
                    20,
                    50
                    );


                ds.AcceptChanges();

                ds.WriteXml(FilePath);

            }
            catch (Exception ex)
            {

                throw ex;
            }

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
            finally
            {
                //if (!string.IsNullOrEmpty(FilePath))
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Path to Konfigurace: " + FilePath ?? "Nenastaven");
            }

            return FilePath;
        }

    }
}
