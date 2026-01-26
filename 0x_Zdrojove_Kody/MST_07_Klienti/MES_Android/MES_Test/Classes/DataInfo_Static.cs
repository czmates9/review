using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.IO;

namespace MES_Android.Classes
{
    class DataInfo_Static
    {
        /// <summary>
        /// objekt
        /// </summary>
        //public static Classes.Users Uzivatel = new Users();

        //        /storage/emulated/0/Documents/Android_MES
        public static string PathDir = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath, "Android_MES");
        //public static string PathDir = Path.Combine(Android.OS.Environment.DirectoryDocuments, "Android_MES");
        //public static string PathDir = Path.Combine(Android.OS.Environment.Get DirectoryDocuments, "Android_MES");
        //public static string PathDir = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonDocuments), "Android_MES");


        public static string SQLiteDBsDir = Path.Combine(PathDir, "SQLiteDBs");
        public static string SoundDir = Path.Combine(PathDir, "Sounds");
        public static string ParseCodeDir = Path.Combine(PathDir, "ParseCode_Logs");

        public const string FilterAll = "*";

        //Pripony
        public const string PriponaSQL = ".sql";
        public const string PriponaZIP = ".zip";
        public const string PriponaPRD = ".prd";
        public const string PriponaDI = ".di";
        public const string PriponaXML = ".xml";
        public const string PriponaJSON = ".json";
        public const string PriponaASMX = ".asmx";
        public const string PriponaTMP = ".tmp";

        // Nazvy souboru
        public const string Uzivatele = "Uzivatele";
        public const string Test = "Test";
        public const string Pracovnici = "Pracovnici";
        public const string Lokace = "Lokace";
        public const string Sklady = "Sklady";
        public const string TypDokladu = "TypDokladu";
        public const string Meny = "Meny";
        public const string Strediska = "Strediska";
        public const string Odberatele = "Odberatele";
        public const string Zbozi = "Zbozi";
        public const string Prodej = "Prodej";
        public const string InternalState = "InternalState";
        public const string Vyroba = "Vyroba";
        public const string Production = "Production";

        public const string VyrobaPrd = Vyroba + PriponaPRD;
        public const string VyrobaPrdTmp = VyrobaPrd + PriponaTMP;

        //public const string ProductionSQL = Production + PriponaSQL;
        //public const string ProductionPrd = Production + PriponaPRD;
        //public const string ProductionPrdTmp = ProductionPrd + PriponaTMP;

        public const string Production_ALL = "Production_";
        public static string Production_ALL_TemplateDB = Path.Combine(PathDir, Production_ALL + FilterAll + PriponaPRD + PriponaTMP);

        //Cesty s souborom
        public static string CiselnikUzivateleDB = Path.Combine(PathDir, Uzivatele + PriponaPRD);
        public static string TestDB = Path.Combine(PathDir, Test + PriponaPRD);

        public static string CiselnikPracovniciDB = Path.Combine(PathDir, Pracovnici + PriponaPRD);
        public static string CiselnikLokaceDB = Path.Combine(PathDir, Lokace + PriponaPRD);
        public static string CiselnikSkladyDB = Path.Combine(PathDir, Sklady + PriponaPRD);
        public static string CiselnikTypDokladuDB = Path.Combine(PathDir, TypDokladu + PriponaPRD);
        public static string CiselnikMenyDB = Path.Combine(PathDir, Meny + PriponaPRD);
        public static string CiselnikStrediskaDB = Path.Combine(PathDir, Strediska + PriponaPRD);
        public static string CiselnikOdberateleDB = Path.Combine(PathDir, Odberatele + PriponaPRD);
        public static string CiselnikZboziDB = Path.Combine(PathDir, Zbozi + PriponaPRD);

        public static string OdvadeniVyrobyDB = Path.Combine(PathDir, Vyroba + PriponaPRD);
        public static string OdvadeniVyrobyDBTMP = Path.Combine(PathDir, Vyroba + PriponaPRD + PriponaTMP);

        public static string ProductionDBTMP = Path.Combine(PathDir, Production + PriponaPRD + PriponaTMP);
        public static string ProductionDB = Path.Combine(PathDir, Production + PriponaPRD);
        public static string ProductionScript = Path.Combine(SQLiteDBsDir, Production + PriponaSQL);

        public static string InternalStateDB = Path.Combine(PathDir, InternalState + PriponaPRD);
        public static string InternalStateDBTMP = Path.Combine(PathDir, InternalState + PriponaPRD + PriponaTMP);
        public static string InternalStateSript = Path.Combine(SQLiteDBsDir, InternalState + PriponaSQL);

        public static string ProdejScript = Path.Combine(SQLiteDBsDir, Prodej + PriponaSQL);

        public static string Prodej_SearchPatern_AllFilesDavek = FilterAll + PriponaDI;

        // Nazvy a cesty ke konfiguracim
        public static string SettingsFileName = "Settings";
        public static string SettingsXML = Path.Combine(PathDir, SettingsFileName + PriponaXML);
        public static string SettingsPRD = Path.Combine(PathDir, SettingsFileName + PriponaPRD);
        public static string SettingsSQL = Path.Combine(SQLiteDBsDir, SettingsFileName + PriponaSQL);


        public static string ConfigTypyPalet = "TypyPalet";
        public static string ConfigTypyPaletXML = Path.Combine(SQLiteDBsDir, ConfigTypyPalet + PriponaXML);

        //TODO budoucnost
        #region Predelat podle JiS na DB kde se budou tyhle informace ukladat

        //public static string Prodej_RADY_Range_FileName = "Prodej_RADY_Range";
        //public static string Prodej_RADY_Delete_FileName = "Prodej_RADY_Delete";

        //public static string Prodej_RADY_Range_SQL = Path.Combine(PathDir, Prodej_RADY_Range_FileName + PriponaSQL);
        //public static string Prodej_RADY_Range_PRD = Path.Combine(PathDir, Prodej_RADY_Range_FileName + PriponaPRD);

        //public static string Prodej_RADY_Delete_SQL = Path.Combine(PathDir, Prodej_RADY_Delete_FileName + PriponaSQL);
        //public static string Prodej_RADY_Delete_PRD = Path.Combine(PathDir, Prodej_RADY_Delete_FileName + PriponaPRD);

        public static string ConfigProdej = "ConfigProdej";
        public static string ConfigProdejXML = Path.Combine(PathDir, ConfigProdej + PriponaXML);

        #endregion

        #region Vyroba

        public const string SoundChimes = "chimes.wav";
        public const string SoundDotaz = "dotaz.wav";
        public const string SoundChyba = "chyba.wav";
        public const string SoundInfo = "info.wav";

        public const string ConfigScanner = "ScannerConfig.xml";

        #endregion


        public static string Konfigurace_FileName = "Konfigurace";
        public static string Konfigurace_JSON = Path.Combine(PathDir, Konfigurace_FileName + PriponaJSON);

        #region Prodej

        //Konstanty pro predavany v Intent
        public const string TypSkladu = "TypSkladu";
        public const string TypSkladu_Zdroj = "Zdroj";
        public const string TypSkladu_Cil = "Cil";

        public const string Sklad_Zasoby = "SKL_Zas";

        public const string TypVratky = "TypVratky";
        public const string SkladRow = "SkladRow";

        public const string Lok_SKL_ID = "Lok_SKL_ID";
        public const string Lok_Text = "Lok_Text";

        public const string Lok_Lokace = "Lok_Lokace";

        public const string MenaID = "MenaID";
        public const string User = "User";
        public const string object_User = "object_User";

        public const string Row095 = "Row095";
        public const string object_Row095 = "object_Row095";

        public const string DT_095 = "DT_095";
        public const string object_DT_095 = "object_DT_095";

        public const string ParsingConfig = "ParsingConfig";
        public const string object_ParsingConfig = "object_ParsingConfig";

        public const string Mat_SERLTNUM = "Mat_SERLTNUM";
        public const string Mat_LOCNCODE = "Mat_LOCNCODE";
        public const string Mat_QTY = "Mat_QTY";

        public const string Location = "Location";
        public const string object_Location = "object_Location";

        public const string PPP = "PPP";
        public const string object_PPP = "object_PPP";

        public const string PPP_out = "PPP_out";
        public const string object_PPP_out = "object_PPP_out";

        public const string doc_id = "doc_id";

        public const string ProdejItem = "ProdejItem";
        public const string object_ProdejItem = "object_ProdejItem";

        public const string VolatelKonfigurace = "Source";
        public const string Activity_HlavneMenu = "Activity_HlavneMenu";
        public const string MainActivity = "MainActivity";

        public const string SkladCil = "SkladCil";
        public const string object_SkladCil = "object_SkladCil";

        #endregion

        #region UserLoginWithTimeInput

        //OUT
        public const string OV_DateTime_UserLoginWithTimeInput = "OV_DateTime_UserLoginWithTimeInput";

        #endregion

        #region OdvadeniVyroby_SberDat

        //IN
        public const string OV_Pracovnik = "OV_Pracovnik";
        public const string object_OV_Pracovnik = "object_OV_Pracovnik";

        public const string OV_Machine = "OV_Machine";
        public const string object_OV_Machine = "object_OV_Machine";

        public const string object_OV_Zakazka = "object_OV_Zakazka";
        public const string OV_Zakazka = "OV_Zakazka";

        #endregion

        # region GetIDMachineAsync

        //OUT
        public const string OV_IDMachine = "OV_IDMachine";
        public const string object_OV_IDMachine = "object_OV_IDMachine";

        #endregion

        #region Prikaz vyber
        
        //IN
        public const string object_PrikazVyber_SOPNUMBE = "object_PrikazVyber_SOPNUMBE";
        public const string PrikazVyber_SOPNUMBE = "PrikazVyber_SOPNUMBE";

        #endregion

        #region OperaceVyber

        //OUT
        public const string OV_VPPRow = "OV_VPPRow";
        public const string object_OV_VPPRow = "object_OV_VPPRow";

        #endregion

        #region GetFormInputQuantityAsync

        //IN
        public const string OV_QTY_Kod = "OV_QTY_Kod";
        public const string OV_QTY_Text = "OV_QTY_Text";
        
        //OUT
        public const string OV_Kod = "OV_Kod";

        #endregion

        #region GetFormInputWeightAsync

        //IN
        public const string OV_Weight_Kod = "OV_Weight_Kod";
        public const string OV_Weight_Text = "OV_Weight_Text";

        //OUT
        public const string OV_Weight_Kod_out = "OV_Weight_Kod_out";

        #endregion

        #region GetOperace_VyberAsync

        public const string OV_dt_VPP = "OV_dt_VPP";
        public const string object_OV_dt_VPP = "object_OV_dt_VPP"; 

        #endregion

        #region GetOdvadeniPrehledAsync

        public const string OV_row_VPP = "OV_row_VPP";
        public const string object_OV_row_VPP = "object_OV_row_VPP";

        public const string OV_row_VPH = "OV_row_VPH";
        public const string object_OV_row_VPH = "object_OV_row_VPH";

        public const string OV_lActualTimeState = "OV_lActualTimeState";
        public const string object_OV_lActualTimeState = "object_OV_lActualTimeState";

        public const string OV_lLastTimeState = "OV_lLastTimeState";
        public const string object_OV_lLastTimeState = "object_OV_lLastTimeState";

        public const string OV_row_lastProductionRowTmp = "OV_row_lastProductionRowTmp";
        public const string object_OV_row_lastProductionRowTmp = "object_OV_row_lastProductionRowTmp";

        #endregion

        #region GetOperacePotvrzeniAsync

        //IN
        public const string OV_OP_PocetOdvedeno = "OV_OP_PocetOdvedeno";
        public const string object_OV_OP_PocetOdvedeno = "object_OV_OP_PocetOdvedeno";

        public const string OV_OP_Pracovnik = "OV_OP_Pracovnik";
        public const string object_OV_OP_Pracovnik = "object_OV_OP_Pracovnik";

        //public const string OV_OP_VPPRow = "OV_OP_VPPRow";
        //public const string object_OV_OP_VPPRow = "object_OV_OP_VPPRow";

        public const string OV_OP_Machine = "OV_OP_Machine";
        public const string object_OV_OP_Machine = "object_OV_OP_Machine";

        public const string OV_OP_VPP_ROW = "OV_OP_VPP_ROW";
        public const string object_OV_OP_VPP_ROW = "object_OV_OP_VPP_ROW";

        public const string OV_OP_ProductionRow = "OV_OP_ProductionRow";
        public const string object_OV_OP_ProductionRow = "object_OV_OP_ProductionRow";

        public const string OV_OP_ProductionSDT = "OV_OP_ProductionSDT";
        public const string object_OV_OP_ProductionSDT = "object_OV_OP_ProductionSDT";


        #endregion

        #region GetInputKod2Async

        //IN
        public const string OV_IN_InputKod2_Nazev = "OV_IN_InputKod2_Nazev";
        public const string OV_IN_InputKod2_Mnozstvi = "OV_IN_InputKod2_Mnozstvi";
        public const string OV_IN_InputKod2_Sklad = "OV_IN_InputKod2_Sklad";
        public const string OV_IN_InputKod2_Lokace = "OV_IN_InputKod2_Lokace";
        public const string OV_IN_InputKod2_Material = "OV_IN_InputKod2_Material";
        public const string OV_IN_InputKod2_Kod = "OV_IN_InputKod2_Kod";

        //OUT
        public const string OV_OUT_InputKod2_Kod = "OV_OUT_InputKod2_Kod";

        #endregion

        public static int ServiceTimeOut = 10000;

        private static Prodej.GlobalObject _prodejGO;
        public static Prodej.GlobalObject ProdejGO_Instance
        {
            get 
            {
                if (_prodejGO == null)
                {

                    //TODO : možná varianta, ale asi ne..
                    //Dotažení čísla davky z serializovaneho souboru
                    //nejaky objekt který pracuje s tym objektem a souborem
                    // bacha na zabiti projeku, odpozorovat život aktivity, OnPause, OnRestore, OnResume... OnDestory ?? atd...
                    // https://www.itnetwork.cz/images/61148/lifecycle.png

                    _prodejGO = new Prodej.GlobalObject();
                    _prodejGO.Davka = 0;
                }
                return _prodejGO;
            }
        }

        private static Fask.RestSharp.API.Communication_4_7 _API_GO;
        public static Fask.RestSharp.API.Communication_4_7 API_GO_Instance
        {
            get
            {
                if (_API_GO == null)
                {
                    _API_GO = new Fask.RestSharp.API.Communication_4_7(
                        Config.Settings.Adresa_API,
                        Config.Settings.API_Autorizace,
                        Config.Settings.API_konstant,
                        Config.Settings.isHTTPS,
                        Config.Settings.TimeOut,
                        Config.Settings.TerminalID.ToString(),
                        "MES_Android"
                        );
                }
                return _API_GO;
            }

        }

        public static string dateFormatRRMMDD = "yyMMdd";

        public static DateTime? Date_RRMMDD(string date)
        {
            try
            {
                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("cs-CZ");
                cultureInfo.Calendar.TwoDigitYearMax = 2099;
                return DateTime.ParseExact(date, dateFormatRRMMDD, cultureInfo);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }


        private static OdvadeniVyroby.GlobalObject _vyrobaGO;
        public static OdvadeniVyroby.GlobalObject VyrobaGO_Instance
        {
            get
            {
                if (_vyrobaGO == null)
                {
                    _vyrobaGO = new OdvadeniVyroby.GlobalObject();
                }
                return _vyrobaGO;
            }
        }
    }


    public static class Convert
    {
        public static float pxFromDp(Context context, float dp)
        {
            return dp * context.Resources.DisplayMetrics.Density;
        }

        public static float dpFromPx(Context context, float px)
        {
            return px / context.Resources.DisplayMetrics.Density;
        }
    }

}