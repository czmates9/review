using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Fask.Module.Ingres.SAD
{
    public class Globals
    {

        private static string _configFilePath = "MST_Ingres_Config.xml";


        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFilePath { get { return _configFilePath; } set { _configFilePath = value; } }


        public static SQL_Datasets.SQL Konfigurace;


        public static string LoadConfiguration()
        {
            return LoadConfiguration(Globals.ConfigFilePath);
        }


        public static string SaveConfiguration()
        {
            return SaveConfiguration(Globals.ConfigFilePath);
        }

        private static string LoadConfiguration(string FileName)
        {
            try
            {

                string FilePath = GetPath(FileName);


                if (!File.Exists(FilePath))
                {
                    //Soubor neexistuje, tak vytvořit..
                    Globals.CreateFile(FilePath);
                }



                //if (Konfigurace == null)
                //Konfigurace = new SQL_Datasets.SQL();

                //Konfigurace.Clear();
                //Konfigurace.ReadXml(FilePath);


                SQL_Datasets.SQL tmp_Konfigurace = new SQL_Datasets.SQL();
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

                SQL_Datasets.SQL ds = new SQL_Datasets.SQL();



                //TODO  doplnit defaultne hodnoty....

                //ds.Settings.AddSettingsRow(
                //    "StoredProcedure", // CommandType
                //    false, // DexRowIdInsert
                //    @"C:\FASK\", //Path...
                //    @"C:\FASK\", //Path...
                //    @"C:\FASK\", //Path...
                //    @"Logs\SoDir\" //StatusObjectDirestory
                //    );

                ds.ComunicatorDriveMapping.AddComunicatorDriveMappingRow(
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty
                    );

                ds.ConnectionString.AddConnectionStringRow(string.Empty);

                ds.Inventura.AddInventuraRow(
                    string.Empty,
                    string.Empty,
                    false,
                    string.Empty,
                    @"C:\FASK\",
                    false
                    );

                ds.Inventura2.AddInventura2Row(
                    false,
                    @"C:\FASK\",
                    @"C:\FASK\",
                    @"C:\FASK\",
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    false
                    );

                ds.Prijem.AddPrijemRow(
                    string.Empty,
                    false,
                    string.Empty,
                    string.Empty,
                    "FASK_FIRMAOBJEDNAVKAVYDANA",
                    "IN_DISPLAYNAME",
                    false,
                    "StoredProcedure",
                    false,
                    @"Logs\SoDir\",
                    "procGenerateSerltnum",
                    "procPrijemGetDoporuceneLokace",
                    "procPrijemNezrealizovane"
                    );

                ds.Vydej.AddVydejRow(
                    string.Empty,
                    false,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    false,
                    string.Empty,
                    "FASK_procGetAdresa",
                    "@SOPNUMBE",
                    "fask_VydejDetailDavka",
                    ":pDavka",
                    false,
                    "A.COUNTENTRIES DESC",
                    false,
                    "StoredProcedure",
                    false,
                    @"Logs\SoDir\",
                    "StoredProcedure",
                    "StoredProcedure",
                    "FASK_procGetpolozka",
                    "@ITEMNMBR",
                    string.Empty
                    );


                ds.Prodej.AddProdejRow(
                    string.Empty,
                    false,
                    string.Empty,
                    string.Empty,
                    @"Logs\SoDir\",
                    "getSklad",
                    "doc_id",
                    "matID",
                    "serialID",
                    "skladSrc",
                    "skladDest",
                    "PROC",
                    "/AUTOMAT:MST_IMP"
                    );

                ds.Servis.AddServisRow(
                    string.Empty,
                    false,
                    string.Empty,
                    string.Empty,
                    "fask_Servis_Generate_Predloha",
                    "document_number",
                    "odb_id",
                    "okruh_id",
                    "UserID",
                    "CountEntries",
                    "message",
                    "fask_Servis_Generate_Predloha_Data",
                    "CountEntries",
                    "1234",
                    @"C:\FASK\"
                    );

                ds.Informations.AddInformationsRow(
                    "czmst_se_volne",
                    "@param1",
                    "@param2",
                    "fask_mnozstvinasklade",
                    "@itemnmbr",
                    "fask_mnozstvinasklade2",
                    "@itemnmbr",
                    "@location",
                    "fask_polozka_detail",
                    "@itemnmbr",
                    "@doklad",
                    "StoredProcedure",
                    "StoredProcedure",
                    "StoredProcedure",
                    string.Empty);

                ds.LokMech.AddLokMechRow(true);

                ds.EXE.AddEXERow(
                    @"C:\FASK\TEXT.exe"
                    , false,
                    @"C:\FASK\TEXT.exe");

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
