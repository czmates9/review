using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Fask.MST_W_Server.Konfigurace.Classes
{
    public class Globals_Konfig_Agendy
    {
        private static string _configFilePath = "MST_Konfig_Agendy.xml";


        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFilePath { get { return _configFilePath; } set { _configFilePath = value; } }


        public static DataSets.Konfig_Agendy Konfigurace;


        public static string LoadConfiguration()
        {
            return LoadConfiguration(Globals_Konfig_Agendy.ConfigFilePath);
        }


        public static string SaveConfiguration()
        {
            return SaveConfiguration(Globals_Konfig_Agendy.ConfigFilePath);
        }

        private static string LoadConfiguration(string FileName)
        {
            try
            {

                string FilePath = GetPath(FileName);


                if (!File.Exists(FilePath))
                {
                    //Soubor neexistuje, tak vytvořit..
                    Globals_Konfig_Agendy.CreateFile(FilePath);
                }



                //if (Konfigurace == null)
                //    Konfigurace = new DataSets.Konfig_Agendy();

                //Konfigurace.Clear();
                //Konfigurace.ReadXml(FilePath);


                DataSets.Konfig_Agendy tmp_Konfigurace = new DataSets.Konfig_Agendy();
                //tmp_Konfigurace.Clear();
                tmp_Konfigurace.ReadXml(FilePath);
                Konfigurace = tmp_Konfigurace;

                return "OK";
            }
            catch (Exception ex)
            { return ex.Message; }
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

                DataSets.Konfig_Agendy  ds = new DataSets.Konfig_Agendy();

                ds.Inventura1Parametry.AddInventura1ParametryRow(
                    false,
                    true,
                    false,
                    true,
                    false,
                    true,
                    false,
                    true,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    20000
                    );

                ds.Inventura2Parametry.AddInventura2ParametryRow(
                    true,
                    false,
                    false,
                    false,
                    true,
                    true,
                    true,
                    false,
                    false,
                    false,
                    false,
                    false,
                    true
                    );

                ds.PrijemParametry.AddPrijemParametryRow(
                    true,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    true,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    true,
                    true,
                    true,
                    true,
                    true,
                    false,
                    true,
                    true,
                    false,
                    false,
                    20000,
                    false
                    );


                ds.VydejParametry.AddVydejParametryRow(
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    false,
                    false,
                    true,
                    true,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    true,
                    false,
                    true,
                    true,
                    false,
                    false,
                    false,
                    false,
                    "VNDITNUM",
                    "CZ_CarKod",
                    false,
                    20000
                    );

                ds.ServisParametry.AddServisParametryRow(true );

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