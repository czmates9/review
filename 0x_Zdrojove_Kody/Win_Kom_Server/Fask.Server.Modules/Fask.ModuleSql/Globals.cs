using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Fask.ModuleSql
{
    public class Globals
    {

        private static string _configFilePath = "MST_SQL_Config.xml";


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

                ds.dbtypes.AdddbtypesRow("ms-sql",
        string.Empty,
        true   // aktivni DB
    );

                ds.dbtypes.AdddbtypesRow("firebird",
                    @"User=SYSDBA;Password=masterkey;Database=C:\FASK\ABRA_DB\DATAFB3.FDB;DataSource=localhost;Charset=WIN1250;Connection lifetime=15;Pooling=true;MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;",
                    false
                );



                ds.Inventura.AddInventuraRow(
                    string.Empty,
                    string.Empty,
                    false,
                    string.Empty,
                    @"C:\FASK\",
                    false,
                    "FASK_proc_EXPORT_SQL_FASK_Inventura"
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
                    "procPrijemNezrealizovane",
                    "FASK_proc_EXPORT_SQL_FASK_Prijem"
                    );

                ds.Vydej.AddVydejRow(
                    string.Empty,
                    false,
                    string.Empty,
                    string.Empty,
                    "FASK_proc_EXPORT_SQL_FASK_Vydej",
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
                    "@ITEMNMBR"
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
                    string.Empty,
                    "StoredProcedure",
                    "FASK_proc_FEFOFIFO",
                    1,
                    "ms-sql");

                ds.LokMech.AddLokMechRow(true);

                ds.EXE.AddEXERow(
                    @"C:\FASK\TEXT.exe"
                    , false,
                    @"C:\FASK\TEXT.exe");

                ds.ExportZasoby.AddExportZasobyRow(
                 "'01'",
                 "1,5",
                 true,
                 false
                 );

                ds.Sdilene.AddSdileneRow(
                  @"Logs\SoDir\",
                  "fask_vychozi_lokace",
                  false,
                  true
                  );

                ds.Ostatni.AddOstatniRow(
                 "FASK_proc_EXPORT_SQL_FASK_Ostatni"
                 );

                ds.Expedice.AddExpediceRow(
                "FASK_proc_EXPORT_SQL_FASK_Expedice"
                );

                ds.Vyroba.AddVyrobaRow(
               "FASK_proc_EXPORT_SQL_FASK_Vyroba"
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
