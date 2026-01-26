using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Ingres.SAD.Constants
{
    public static class Common
    {
        //public const string REQ = "Request";
        //public const string RES = "Response";
        //public const string URL = "URL";
        //public const string json = ".json";
        //public const string txt = ".txt";

        //public const string IMPORT = "import";
        //public const string SLASH = @"/";
        //public const string ROWS = "rows";

        //public const string FV = "issuedinvoices";
        //public const string DL = "billsofdelivery";
        //public const string PRV = "outgoingtransfers";
        //public const string PRP = "incomingtransfers";
        //public const string OP = "receivedorders";
        //public const string OV = "issuedorders";
        //public const string PR = "receiptcards";
        //public const string DIProws = "partialinvprotocolrows";
        //public const string Strediska = "divisions";
        //public const string SB = "StoreBatches";
        //public const string HIProws = "MainInvProtocolRows";

        //public const string FV_ID = "5600000101";
        //public const string DL_ID = "P600000101"; // ID rady, je to treba ja promenna
        //public const string PR_ID = "O600000101";

        //public const string FV_ID = "5000000101";
        //public const string DL_ID = "N000000101";
        //public const string PR_ID = "O600000101";

        //public const string CLSID = "GF1U1H4R1ZE13HS401K0LEYWLS";

        //public const string PM_Change = "pmchangestate?select=ID,PMState_ID.Code";

        //public const string DL_DocumentType = "21"; // konstanta pro Dodaci List DOCUMENTTYPE
        //public const string PR_DocumentType = "20";// konstanta pro Prijemku na sklad DOCUMENTTYPE
        //public const string INM_DocumentType = "26";// konstanta pro InventuryManko na sklad DOCUMENTTYPE
        //public const string PRV_DocumentType = "22"; // konstanta pro Dodaci List DOCUMENTTYPE

        //Stavy , v tabulke PMSTATES je to stloupec CODE
        // na dokladoch je to vetšinou PMSTATE_ID

        //public const string KVydeji = "K výdeji";
        //public const string VyskladnujeSe = "Vyskladnuje se";
        //public const string Vyskladneno = "Vyskladněno";
        //public const string KeKontrole = "Ke kontrole";

        //Vydej
        public const string TABLE_CZMST_SE = "CZMST_SE";
        public const string TABLE_CZMST_SE_SN = "CZMST_SE_SN";
        public const string TABLE_CZMST_SI = "CZMST_SI";
        public const string TABLE_CZMST_SIH = "CZMST_SIH";
        //Prodej
        public const string TABLE_CZMST_DI = "CZMST_DI";
        //Prijem
        public const string TABLE_CZMST_PE = "CZMST_PE";
        public const string TABLE_CZMST_PI = "CZMST_PI";
        public const string TABLE_CZMST_PIH = "CZMST_PIH";

        //Ciselniky
        public const string TABLE_FASK_ZASOBY = "FASK_ZASOBY";
        public const string TABLE_CZMST092 = "CZMST092"; //Typy dokladu
        public const string TABLE_CZMST093 = "CZMST093"; // Sklady

        //LokMech
        public const string TABLE_CZMST_SKLADLOKACE_STAV = "CZMST_SkladLokace_Stav";
        public const string TABLE_CZMST_SKLADLOKACE_STAVPOHYB = "CZMST_SkladLokace_StavPohyb";
        public const string TABLE_CZMST_SKLADLOKACE_MAPA = "CZMST_SkladLokace_Mapa";
        //public const string TABLE_CZMST_SkladLokace_LokaceTypy = "CZMST_SkladLokace_LokaceTypy";
        //public const string TABLE_CZMST_SkladLokace_LokaceVariantySortiment = "CZMST_SkladLokace_LokaceVariantySortiment";

        //Inventura

        public const string TABLE_CZMST_I1 = "CZMST_I1";
        public const string TABLE_CZMST_I1H = "CZMST_I1H";
        public const string TABLE_CZMST_I2 = "CZMST_I2";
        public const string TABLE_CZMST_I3 = "CZMST_I3";
        public const string TABLE_CZMST_I4 = "CZMST_I4";

        //Konstanta pro Export
        //public const string ExportSession_DIP_Text = "DIP_Exp";
        //public const string PrehledSession_DIP_Text = "DIP_Prehled";

    }

    //public static class SaveToFile
    //{
    //    public static void Save(string SubPath, string Zdroj, string Obsah, Guid G, string Pripona)
    //    {
    //        string FileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff_") + Zdroj + "_" + G.ToString() + Pripona;
    //        if (string.IsNullOrEmpty(Obsah))
    //        {
    //            string c = Path.Combine(Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy_Path, "ERR" + @"\" + FileName);
    //            string msg = string.Format("Halo tady je prazdny soubor, proč?" + Environment.NewLine +
    //                "SubPath: {0}" + Environment.NewLine +
    //                "Zdroj: {1}" + Environment.NewLine +
    //                "G: {2}" + Environment.NewLine +
    //                "Pripona: {3}" + Environment.NewLine ,
    //                SubPath,
    //                Zdroj,
    //                G,
    //                Pripona
    //                );
    //            Logging.ExceptionHandler2.Handle("",c);
    //        }

    //        //string FileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff_") + Zdroj + "_" + G.ToString() + Pripona;
    //        string cesta = Path.Combine(Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy_Path , SubPath + @"\" +  FileName);
    //        Logging.ExceptionHandler2.Handle(Obsah, cesta);
    //    }

    //    public static void Save(string Obsah, string SubPath, string FileName, string Pripona)
    //    {
    //        string FileNameFull = DateTime.Now.ToString("yyyy_MM_dd_") + FileName  + Pripona;
    //        string cesta = Path.Combine(MyPath.Path.LogsDirectory, SubPath + @"\" + FileNameFull);
    //        Logging.ExceptionHandler2.Handle(Obsah, cesta);
    //    }
    //}
}
