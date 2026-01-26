using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Fask.SQL
{
    public class Globals_V1
    {

        private static string _configFilePath = "MST_Pohoda_Config.xml";


        /// <summary>
        /// Jmeno konfiguracniho souboru
        /// </summary>
        public static string ConfigFilePath { get { return _configFilePath; } set { _configFilePath = value; } }


        public static Fask.ModulePohodaXML.Datasets.POHODA_V1 Konfigurace;


        public static string LoadConfiguration()
        {
            return LoadConfiguration(Globals_V1.ConfigFilePath);
        }


        public static string SaveConfiguration()
        {
            return SaveConfiguration(Globals_V1.ConfigFilePath);
        }

        //private static object _load_configuration_lock = new object();

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


                //lock (_load_configuration_lock)
                //{
                //    if (Konfigurace == null)
                //        Konfigurace = new Datasets.POHODA_V1();

                //    Konfigurace.Clear();
                //    Konfigurace.ReadXml(FilePath);
                //}

                Fask.ModulePohodaXML.Datasets.POHODA_V1 tmp_Konfigurace = new Fask.ModulePohodaXML.Datasets.POHODA_V1();
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

                Fask.ModulePohodaXML.Datasets.POHODA_V1 ds = new Fask.ModulePohodaXML.Datasets.POHODA_V1();

                ds.ComunicatorDriveMapping.AddComunicatorDriveMappingRow(
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty
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
                    "@param3",
                    "StoredProcedure",
                    "FASK_proc_FEFOFIFO",
                    1);


                ds.ConnectionStrings.AddConnectionStringsRow(
                    string.Empty,
                    string.Empty);

                ds.ExportZasoby.AddExportZasobyRow(
                    "'01'",
                    "1,5",
                    true,
                    false
                    );


                ds.Inventura1.AddInventura1Row(
                    true,
                    false,
                    @"..\Data",
                    @"C:\FASK\Pohoda\",
                    @"C:\FASK\Pohoda\",
                    false,
                    false
                    );

                ds.PohodaInfo.AddPohodaInfoRow(
                    "Admin",
                    "@",
                    "admin",
                    "12345678",
                    "StwPh_123456789",
                    @"C:\Program Files\STORMWARE\POHODA\Pohoda.exe",
                    true,
                    @"C:\FASK\Pohoda\Export\",
                    false,
                    @"C:\FASK\Pohoda\xml_imp.ini"
                    );

                ds.Prijem.AddPrijemRow(
                    true,
                    true,
                    false,
                    1,

                    "SELECT" +
                    " o.Cislo 'ocislo' " +
                    " , isnull(z.Cislo, '') 'zcislo' " +
                    " , z.SText 'zstext' " +
                    " , c.ID 'cid' " +
                    " , isnull(c.IDS, '') 'cids' " +
                    " , c.SText 'cstext' " +
                    " from OBJ o " +
                    " left join sZAK z on z.Cislo = o.CisloZAK " +
                    " left join sCIN c on c.ID = o.RefCin " +
                    " where o.Cislo =? ",

                    " SELECT " +
                    " o.Cislo 'ocislo' " +
                    " , isnull(z.Cislo, '') 'zcislo' " +
                    " , z.SText 'zstext' " +
                    " , c.ID 'cid' " +
                    " , isnull(c.IDS, '') 'cids' " +
                    " , c.SText 'cstext' " +
                    " , isnull(op.Pozn, '') 'oppozn' " +
                    " from OBJ o " +
                    " left join sZAK z on z.Cislo = o.CisloZAK " +
                    " left join sCIN c on c.ID = o.RefCin " +
                    " join OBJPol op on op.RefAg = o.ID " +
                    " where o.Cislo =? and op.refskz =? and op.ID =? ",

                    "4321",
                    false,
                    false,
                    false,
                    true,
                    true,
                    "procGenerateSerltnum",
                    "procPrijemGetDoporuceneLokace",
                    "procPrijemNezrealizovane"
                    );

                ds.Prodej.AddProdejRow(
                    true,
                    true,
                    false,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "příkazem",
                    "cizí měna",
                    "EUR",
                    "getSklad",
                    "doc_id",
                    "matID",
                    "serialID",
                    "skladSrc",
                    "skladDest"
                    );

                ds.Sdilene.AddSdileneRow(
                    @"Logs\SoDir\",
                    "fask_vychozi_lokace",
                    false,
                    true
                    );

                ds.Vydej.AddVydejRow(
                    false,
                    true,
                    true,
                    true,
                    false,
                    false,
                    true,
                    false,
                    true,
                    1,
                    "1234",
                    false,
                    false,
                    false,
                    true,
                    true,
                    "StoredProcedure",
                    "StoredProcedure",
                    "StoredProcedure",
                    "FASK_procGetpolozka",
                    "@ITEMNMBR",
                    "FASK_procGetAdresa",
                    "@SOPNUMBE",
                    "fask_VydejDetailDavka",
                    ":pDavka",
                    false,
                    false,
                    false,
                    "1,5",
                    30
                    );

                ds.Ukolovani.AddUkolovaniRow(
                    string.Empty,
                    string.Empty,
                    "Servisni Zasah.",
                    10000,
                    "smtp.fask.cz",
                    587,
                    string.Empty,
                    true,
                    false,
                    false
                    );

                ds.LokMech.AddLokMechRow(true);

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
