using Fask.Module.ABRA.CarpServise.SQL_Datasets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Fask.Module.ABRA.CarpServise
{
    public class Globals_V1
    {

        private static string _configFilePath = "MST_ABRA_CarpServis_Config.xml";


        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFilePath { get { return _configFilePath; } set { _configFilePath = value; } }


        public static ABRA_V1 Konfigurace;


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
                //    Konfigurace = new ABRA_V1();

                //Konfigurace.Clear();
                //Konfigurace.ReadXml(FilePath);

                ABRA_V1 tmp_Konfigurace = new ABRA_V1();
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

                ABRA_V1 ds = new ABRA_V1();


                ds.ConnectionStrings.AddConnectionStringsRow(
                    @"User=SYSDBA;Password=masterkey;Database=C:\FASK\ABRA_DB\DATAFB3.FDB;DataSource=localhost;Charset=WIN1250;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;",
                    @"Data Source=FASKCZ-CO010\SQLEXPRESS;Initial Catalog=Abra_CARP_Servis;User ID=sa;Password=sasa"
                    );

                ds.Vydej.AddVydejRow(
                    false,
                    "1234",
                    @"Logs\SoDir\"
                    );

                ds.Prijem.AddPrijemRow(
                    "1234",
                    @"Logs\SoDir\"
                    );

                ds.Inventura1.AddInventura1Row(
                    @"C:\FASK\ABRA\",
                    @"C:\FASK\ABRA\",
                    false,
                    @"C:\FASK\ABRA\"
                    );

                ds.Prodej.AddProdejRow(@"Logs\SoDir\");

                ds.ExportZasoby.AddExportZasobyRow("");

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

            return FilePath;
        }

    }
}

