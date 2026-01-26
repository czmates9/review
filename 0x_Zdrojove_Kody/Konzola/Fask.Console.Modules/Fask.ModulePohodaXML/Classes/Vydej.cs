using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Fask.Logging;
using Fask.ModulePohodaXML.Classes;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML
{
    public static class Vydej
    {
        //public static bool CreateImportXML_Vydejka(string file, int countEntries, string SKL_ID, string note)
        //{
        //    // konstanta pro tostrin() cisel na invariantni format ...
        //    System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

        //    //Datasets.Prijem.CZMST_PIDataTable dt_pi = null;
        //    //Datasets.Prijem.CZMST_PIHDataTable dt_pih = null;
        //    Pohoda_DataSets.Vydej.Production_SourcesDataTable dt_ps = null;

        //    try
        //    {
        //        dt_ps = Database.Vydej.GETDATA_ProductionSources(countEntries, SKL_ID);

        //        //dt_pih = Database.Prijem.GETDATA_CZMSTPIH(countEntries);



        //        // dotazeni data dokladu ze zadaneho na terminalu ...
        //        DateTime? datumdokladu = null;
        //        //TaD ?? 
        //        //if (dt_pih != null && dt_pih.Count > 0)
        //        //    datumdokladu = dt_pih[0].DATUMDOKLADU;



        //        // dotahnout informace z vydane objednavky ...
        //        //string ponumber = dt_production[0].PONUMBER.Trim();

        //        //Pohoda_DataSets.DatabasePohodaTableAdapters.OBJTableAdapter pohoda_obj_ta = new Pohoda_DataSets.DatabasePohodaTableAdapters.OBJTableAdapter();
        //        //pohoda_obj_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

        //        //Pohoda_DataSets.DatabasePohoda.OBJDataTable pohoda_obj_dt = pohoda_obj_ta.GetDataByCislo(ponumber);
        //        //Pohoda_DataSets.DatabasePohoda.OBJRow pohoda_obj = pohoda_obj_dt[0];
        //        //// dodavatel
        //        //int? refad = null;
        //        //if (!pohoda_obj.IsRefADNull())
        //        //    refad = pohoda_obj.RefAD;
        //        //string cislozak = null;
        //        //if (!pohoda_obj.IsCisloZAKNull())
        //        //    cislozak = pohoda_obj.CisloZAK;
        //        //int? cinnost = null;
        //        //if (!pohoda_obj.IsRefCinNull())
        //        //    cinnost = pohoda_obj.RefCin;
        //        //int? stredisko = null;
        //        //if (!pohoda_obj.IsRefStrNull())
        //        //    stredisko = pohoda_obj.RefStr;
        //        string text = null;//pohoda_obj.IsSTextNull() ? null : pohoda_obj.SText;
        //        string poznamka = null;//pohoda_obj.IsPoznNull() ? null : pohoda_obj.Pozn;
        //        string poznamka2 = null;//pohoda_obj.IsPozn2Null() ? null : pohoda_obj.Pozn2;

        //        //Pohoda_DataSets.DatabasePohodaTableAdapters.OBJpolTableAdapter pohoda_objpol_ta = new Pohoda_DataSets.DatabasePohodaTableAdapters.OBJpolTableAdapter();
        //        //pohoda_objpol_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
        //        //Pohoda_DataSets.DatabasePohoda.OBJpolDataTable pohoda_odbpol_dt = pohoda_objpol_ta.GetDataByRefAg(pohoda_obj.ID);

        //        string filename = Globals.PathToInputDirectory + file;
        //        //string partnerID = string.Empty;
        //        //if (refad.HasValue)
        //        //    partnerID = refad.Value.ToString();

        //        // TODO : odberatel ??? je na vydane objednavce ... ???
        //        //foreach (Datasets.Prijem.CZMST_PIRow row in dt_pi)
        //        //{
        //        //    if (!row.IsODBER_IDNull() && row.ODBER_ID.Trim() != string.Empty)
        //        //    {
        //        //        partnerID = row.ODBER_ID.Trim();
        //        //        break;
        //        //    }
        //        //}

        //        DateTime date = DateTime.Now;

        //        //int? cizimenaid = null;
        //        ////Cizi meny ... 
        //        //try
        //        //{
        //        //    if (!pohoda_obj.IsRefCMNull())
        //        //        cizimenaid = pohoda_obj.RefCM;
        //        //}
        //        //catch (Exception excm)
        //        //{
        //        //    Logging.Log.Write(excm.Message + "\n" + excm.StackTrace, "Fask.ModulePohodaXML" + "," + "CreateImportXML");
        //        //}

        //        ////rada dokladu ... 
        //        ////rada dokladu cizi meny ...
        //        //string idsradadokladu = null;
        //        //try
        //        //{
        //        //    if (cizimenaid.HasValue) //pouze pokud bude doklad v cizi mene ...
        //        //    {
        //        //        Pohoda_DataSets.DatabasePohodaTableAdapters.sCRadyTableAdapter ta_crady = new Pohoda_DataSets.DatabasePohodaTableAdapters.sCRadyTableAdapter();
        //        //        ta_crady.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

        //        //        Pohoda_DataSets.DatabasePohoda.sCRadyDataTable dt_crady = ta_crady.GetDataBy_RokDokladObsahtextu(
        //        //            DateTime.Now.Year,
        //        //            26, //Příjemky
        //        //            "%" + Globals.RadaCiziMenaText + "%");

        //        //        if (dt_crady != null && dt_crady.Count > 0)
        //        //        {
        //        //            idsradadokladu = dt_crady[0].IDS;
        //        //        }
        //        //    }
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    Logging.Log.Write("Fask.ModulePohodaXML"+ ","+"WriteObjednavkaBezOdbXMLCM", ex.Message + "\n" + ex.StackTrace);
        //        //}

        //        TextWriter tw = new StreamWriter(filename);
        //        tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //        tw.WriteLine("<dat:dataPack id=\"pri" + countEntries + "\" ico=\"" + Globals.ICO + "\" application=\"MST\" version=\"2.0\" note=\"Import Vydejky\"");
        //        tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
        //        tw.WriteLine("xmlns:pri=\"http://www.stormware.cz/schema/version_2/vydejka.xsd\"");
        //        tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

        //        tw.WriteLine("<dat:dataPackItem id=\"PRI" + countEntries + "\" version=\"2.0\">");
        //        tw.WriteLine("  <pri:vydejka version=\"2.0\">");

        //        // hlavicka
        //        tw.WriteLine("      <pri:vydejkaHeader>");
        //        //tw.WriteLine("          <pri:date>" + date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString("00") + "</pri:date>");
        //        //tw.WriteLine("          <pri:numberOrder></pri:numberOrder>");
        //        //tw.WriteLine("          <pri:dateOrder>" + "1999-01-01" + "</pri:dateOrder>");
        //        //tw.WriteLine("          <pri:text>" + "Test import MSTW-Pohoda" + "</pri:text>");
        //        //if (text != null)
        //        tw.WriteLine("          <pri:text>" + text + "</pri:text>");

        //        StringBuilder poznB = new StringBuilder();
        //        if (!String.IsNullOrEmpty(poznamka))
        //            poznB.AppendLine(poznamka);
        //        if (!String.IsNullOrEmpty(note))
        //            poznB.AppendLine(note);
        //        if (poznB.Length > 0)
        //            tw.WriteLine("              <pri:note>" + poznB.ToString() + "</pri:note>");

        //        tw.WriteLine("          <pri:intNote>" + (poznamka2 == null ? string.Empty : poznamka2 + "\n") + "nacteno z konzole z dávky č.:" + countEntries.ToString() + "</pri:intNote>");

        //        // datum dokladu, pokud bylo zadano na terminalu ...
        //        if (datumdokladu.HasValue)
        //        {
        //            tw.WriteLine("          <pri:date>" + XmlConvert.ToString(datumdokladu.Value, "yyyy-MM-dd") + "</pri:date>");
        //        }

        //        //rada dokladu pro cizi menu ...
        //        //if (!string.IsNullOrEmpty(idsradadokladu))
        //        //{
        //        //    tw.WriteLine("          <pri:number>");
        //        //    tw.WriteLine("              <typ:ids>" + idsradadokladu + "</typ:ids>");
        //        //    tw.WriteLine("          </pri:number>");
        //        //}

        //        //partner
        //        //if (partnerID != string.Empty)
        //        //{
        //        //    tw.WriteLine("          <pri:partnerIdentity>");
        //        //    tw.WriteLine("              <typ:id>" + partnerID + "</typ:id>");
        //        //    tw.WriteLine("          </pri:partnerIdentity>");
        //        //}

        //        //zakazka
        //        //if (cislozak != null)
        //        //{
        //        //    tw.WriteLine("          <pri:contract>");
        //        //    tw.WriteLine("              <typ:ids>" + cislozak + "</typ:ids>");
        //        //    tw.WriteLine("          </pri:contract>");
        //        //}

        //        //if (cinnost.HasValue)
        //        //{
        //        //    tw.WriteLine("          <pri:activity>");
        //        //    tw.WriteLine("              <typ:id>" + cinnost.Value + "</typ:id>");
        //        //    tw.WriteLine("          </pri:activity>");
        //        //}

        //        //if (stredisko.HasValue)
        //        //{
        //        //    tw.WriteLine("          <pri:centre>");
        //        //    tw.WriteLine("              <typ:id>" + stredisko.Value + "</typ:id>");
        //        //    tw.WriteLine("          </pri:centre>");
        //        //}

        //        tw.WriteLine("      </pri:vydejkaHeader>");

        //        //polozky 
        //        tw.WriteLine("      <pri:vydejkaDetail>");

        //        foreach (Pohoda_DataSets.Vydej.Production_SourcesRow row in dt_ps)
        //        {
        //            //// TODO : najit polozku v objednavce ... ???
        //            ////
        //            ////var objpols = pohoda_odbpol_dt.Where(x => (x.RefSKz.ToString() == row.ITEMNMBR) && (x.));
        //            //var objpols = pohoda_odbpol_dt.Where(x => (x.ID == row.ORD)); //jedinecna vazba ...
        //            //Pohoda_DataSets.DatabasePohoda.OBJpolRow objpol = null;
        //            //if (objpols.Count() > 0)
        //            //    objpol = objpols.First();

        //            tw.WriteLine("      <pri:vydejkaItem>");
        //            //if ((objpol != null) && (!objpol.IsRefStrNull()))
        //            //{ //stredisko
        //            //    tw.WriteLine("          <pri:centre><typ:id>" + objpol.RefStr + "</typ:id></pri:centre>");
        //            //}
        //            //if ((objpol != null) && (!objpol.IsRefCinNull()))
        //            //{ //cinnost
        //            //    tw.WriteLine("          <pri:activity><typ:id>" + objpol.RefCin + "</typ:id></pri:activity>");
        //            //}
        //            //if ((objpol != null) && (!objpol.IsCisloZAKNull()))
        //            //{ //zakazka
        //            //    tw.WriteLine("          <pri:contract><typ:ids>" + objpol.CisloZAK + "</typ:ids></pri:contract>");
        //            //}

        //            tw.WriteLine("          <pri:quantity>" + ((float)row.QTYSHPPD).ToString(nfi) + "</pri:quantity>");
        //            //if ((objpol != null) && (!objpol.IsMJNull()))
        //            //{
        //            //    tw.WriteLine("          <pri:unit>" + objpol.MJ.Trim() + "</pri:unit>");
        //            //}
        //            //if ((objpol != null) && (!objpol.IsMJKoefNull()))
        //            //{
        //            //    tw.WriteLine("          <pri:coefficient>" + objpol.MJKoef.ToString(nfi) + "</pri:coefficient>");
        //            //}
        //            //// sleva polozky ...
        //            //if ((objpol != null) && (!objpol.IsSlevaNull()))
        //            //{
        //            //    tw.WriteLine("          <pri:discountPercentage>" + objpol.Sleva.ToString(nfi) + "</pri:discountPercentage>");
        //            //}
        //            ////((Datasets.DatabasePohoda.OBJpolDataTable)objpol.Table).SDphColumn
        //            //if ((objpol != null))
        //            //{
        //            //    tw.WriteLine("          <pri:payVAT>" + objpol.SDph.ToString().ToLower() + "</pri:payVAT>");
        //            //}
        //            //if ((objpol != null) && (!objpol.IsRelSzDPHNull()))
        //            //{
        //            //    Classes.Pohoda.Sazby dphSazba = (Classes.Pohoda.Sazby)objpol.RelSzDPH;
        //            //    tw.WriteLine("          <pri:rateVAT>" + dphSazba.ToString() + "</pri:rateVAT>");
        //            //}
        //            //// home currency (prevzit z objednavky ...) => jednotkova cena ...
        //            //if ((objpol != null) && (!objpol.IsKcJednNull()))
        //            //{
        //            //    //tw.WriteLine("          <pri:homeCurrency><typ:price>" + objpol.KcJedn.ToString(nfi) + "</typ:price></pri:homeCurrency>");
        //            //    tw.WriteLine("          <pri:homeCurrency><typ:unitPrice>" + objpol.KcJedn.ToString(nfi) + "</typ:unitPrice></pri:homeCurrency>");
        //            //}
        //            //// foreign currency (prevzit z objednavky ...) => jednotkova cena ...
        //            //if ((objpol != null) && (!objpol.IsCmJednNull()))
        //            //{
        //            //    //tw.WriteLine("          <pri:foreignCurrency><typ:price>" + objpol.CmJedn.ToString(nfi) + "</typ:price></pri:foreignCurrency>");
        //            //    tw.WriteLine("          <pri:foreignCurrency><typ:unitPrice>" + objpol.CmJedn.ToString(nfi) + "</typ:unitPrice></pri:foreignCurrency>");
        //            //}

        //            tw.WriteLine("          <pri:stockItem>");
        //            tw.WriteLine("              <typ:stockItem>");
        //            tw.WriteLine("                  <typ:id>" + row.ITEMNMBR.Trim() + "</typ:id>");
        //            tw.WriteLine("              </typ:stockItem>");
        //            //if (row. SERLTNUM.Trim().Length > 0)
        //            //{
        //            //    tw.WriteLine("              <typ:serialNumber>" + row.SERLTNUM.Trim() + "</typ:serialNumber>");
        //            //}
        //            tw.WriteLine("          </pri:stockItem>");
        //            tw.WriteLine("      </pri:vydejkaItem>");
        //        }

        //        tw.WriteLine("      </pri:vydejkaDetail>");

        //        //if (cizimenaid.HasValue)
        //        //{
        //        //    tw.WriteLine("      <pri:prijemkaSummary>");
        //        //    tw.WriteLine("          <pri:foreignCurrency>");
        //        //    tw.WriteLine("              <typ:currency>");
        //        //    tw.WriteLine("                  <typ:id>" + cizimenaid + "</typ:id>");
        //        //    tw.WriteLine("              </typ:currency>");
        //        //    if (!pohoda_obj.IsCmKursNull()) // kurs prebrat z objednavky ... ???
        //        //    {
        //        //        tw.WriteLine("              <typ:rate>" + pohoda_obj.CmKurs.ToString(nfi) + "</typ:rate>");
        //        //    }
        //        //    // Toto by snad mela pohoda dotahnout automaticky dle id meny ...
        //        //    //if (!pohoda_obj.IsCmMnozNull())
        //        //    //{
        //        //    //    tw.WriteLine("              <typ:amount>" + pohoda_obj.CmMnoz + "</typ:amount>");
        //        //    //}

        //        //    tw.WriteLine("          </pri:foreignCurrency>");
        //        //    tw.WriteLine("      </pri:prijemkaSummary>");
        //        //}


        //        tw.WriteLine("  </pri:vydejka>");
        //        tw.WriteLine("</dat:dataPackItem>");
        //        tw.WriteLine("</dat:dataPack>");

        //        tw.Flush();
        //        tw.Close();
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex.ToString());
        //        return false;
        //    }
        //}

        #region Import
        #region OLD Nepouziva sa

        //[Obsolete("Pouziva se objektove skladani XML", true)]
        //public static bool CreateRequest_Import_Vydejka_XML(Pohoda_DataSets.Vydej.Production_SourcesDataTable dt_ps, string file, int countEntries, string SKL_ID, string note)
        //{
        //    // konstanta pro tostrin() cisel na invariantni format ...
        //    System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

        //    //Datasets.Prijem.CZMST_PIDataTable dt_pi = null;
        //    //Datasets.Prijem.CZMST_PIHDataTable dt_pih = null;
        //    //Pohoda_DataSets.Vydej.Production_SourcesDataTable dt_ps = null;

        //    try
        //    {
        //        //dt_ps = Database.Vydej.GETDATA_ProductionSources(countEntries, SKL_ID);

        //        //dt_pih = Database.Prijem.GETDATA_CZMSTPIH(countEntries);



        //        // dotazeni data dokladu ze zadaneho na terminalu ...
        //        DateTime? datumdokladu = null;
        //        //TaD ?? 
        //        //if (dt_pih != null && dt_pih.Count > 0)
        //        //    datumdokladu = dt_pih[0].DATUMDOKLADU;



        //        // dotahnout informace z vydane objednavky ...
        //        //string ponumber = dt_production[0].PONUMBER.Trim();

        //        //Pohoda_DataSets.DatabasePohodaTableAdapters.OBJTableAdapter pohoda_obj_ta = new Pohoda_DataSets.DatabasePohodaTableAdapters.OBJTableAdapter();
        //        //pohoda_obj_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

        //        //Pohoda_DataSets.DatabasePohoda.OBJDataTable pohoda_obj_dt = pohoda_obj_ta.GetDataByCislo(ponumber);
        //        //Pohoda_DataSets.DatabasePohoda.OBJRow pohoda_obj = pohoda_obj_dt[0];
        //        //// dodavatel
        //        //int? refad = null;
        //        //if (!pohoda_obj.IsRefADNull())
        //        //    refad = pohoda_obj.RefAD;
        //        //string cislozak = null;
        //        //if (!pohoda_obj.IsCisloZAKNull())
        //        //    cislozak = pohoda_obj.CisloZAK;
        //        //int? cinnost = null;
        //        //if (!pohoda_obj.IsRefCinNull())
        //        //    cinnost = pohoda_obj.RefCin;
        //        //int? stredisko = null;
        //        //if (!pohoda_obj.IsRefStrNull())
        //        //    stredisko = pohoda_obj.RefStr;
        //        string text = null;//pohoda_obj.IsSTextNull() ? null : pohoda_obj.SText;
        //        string poznamka = null;//pohoda_obj.IsPoznNull() ? null : pohoda_obj.Pozn;
        //        string poznamka2 = null;//pohoda_obj.IsPozn2Null() ? null : pohoda_obj.Pozn2;

        //        //Pohoda_DataSets.DatabasePohodaTableAdapters.OBJpolTableAdapter pohoda_objpol_ta = new Pohoda_DataSets.DatabasePohodaTableAdapters.OBJpolTableAdapter();
        //        //pohoda_objpol_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
        //        //Pohoda_DataSets.DatabasePohoda.OBJpolDataTable pohoda_odbpol_dt = pohoda_objpol_ta.GetDataByRefAg(pohoda_obj.ID);

        //        string filename = Globals.PathToInputDirectory + file;
        //        //string partnerID = string.Empty;
        //        //if (refad.HasValue)
        //        //    partnerID = refad.Value.ToString();

        //        // TODO : odberatel ??? je na vydane objednavce ... ???
        //        //foreach (Datasets.Prijem.CZMST_PIRow row in dt_pi)
        //        //{
        //        //    if (!row.IsODBER_IDNull() && row.ODBER_ID.Trim() != string.Empty)
        //        //    {
        //        //        partnerID = row.ODBER_ID.Trim();
        //        //        break;
        //        //    }
        //        //}

        //        DateTime date = DateTime.Now;

        //        //int? cizimenaid = null;
        //        ////Cizi meny ... 
        //        //try
        //        //{
        //        //    if (!pohoda_obj.IsRefCMNull())
        //        //        cizimenaid = pohoda_obj.RefCM;
        //        //}
        //        //catch (Exception excm)
        //        //{
        //        //    Logging.Log.Write(excm.Message + "\n" + excm.StackTrace, "Fask.ModulePohodaXML" + "," + "CreateImportXML");
        //        //}

        //        ////rada dokladu ... 
        //        ////rada dokladu cizi meny ...
        //        //string idsradadokladu = null;
        //        //try
        //        //{
        //        //    if (cizimenaid.HasValue) //pouze pokud bude doklad v cizi mene ...
        //        //    {
        //        //        Pohoda_DataSets.DatabasePohodaTableAdapters.sCRadyTableAdapter ta_crady = new Pohoda_DataSets.DatabasePohodaTableAdapters.sCRadyTableAdapter();
        //        //        ta_crady.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

        //        //        Pohoda_DataSets.DatabasePohoda.sCRadyDataTable dt_crady = ta_crady.GetDataBy_RokDokladObsahtextu(
        //        //            DateTime.Now.Year,
        //        //            26, //Příjemky
        //        //            "%" + Globals.RadaCiziMenaText + "%");

        //        //        if (dt_crady != null && dt_crady.Count > 0)
        //        //        {
        //        //            idsradadokladu = dt_crady[0].IDS;
        //        //        }
        //        //    }
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    Logging.Log.Write("Fask.ModulePohodaXML"+ ","+"WriteObjednavkaBezOdbXMLCM", ex.Message + "\n" + ex.StackTrace);
        //        //}
        //        var data = dt_ps.OrderBy(x => x.DEX_ROW_ID);
        //        var row_id = data.First();

        //        TextWriter tw = new StreamWriter(filename);
        //        tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //        tw.WriteLine("<dat:dataPack id=\"vyd" + row_id.DEX_ROW_ID.ToString() + "\" ico=\"" + Globals.ICO + "\" application=\"Konzole\" version=\"2.0\" note=\"Import Vydejky\"");
        //        tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
        //        tw.WriteLine("xmlns:vyd=\"http://www.stormware.cz/schema/version_2/vydejka.xsd\"");
        //        tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

        //        //Production sources
        //        //nejmensi DEX_ROW_ID
        //        //pouzit jak ID
        //        //dt_ps



        //        tw.WriteLine("<dat:dataPackItem id=\"VYD" + row_id.DEX_ROW_ID.ToString() + "\" version=\"2.0\">");
        //        tw.WriteLine("  <vyd:vydejka version=\"2.0\">");

        //        // hlavicka
        //        tw.WriteLine("      <vyd:vydejkaHeader>");
        //        //tw.WriteLine("          <vyd:date>" + date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString("00") + "</pri:date>");
        //        //tw.WriteLine("          <vyd:numberOrder></vyd:numberOrder>");
        //        //tw.WriteLine("          <vyd:dateOrder>" + "1999-01-01" + "</vyd:dateOrder>");
        //        //tw.WriteLine("          <vyd:text>" + "Test import MSTW-Pohoda" + "</vyd:text>");

        //        if (text != null)
        //            tw.WriteLine("          <vyd:text>" + text + "</vyd:text>");

        //        StringBuilder poznB = new StringBuilder();
        //        if (!String.IsNullOrEmpty(poznamka))
        //            poznB.AppendLine(poznamka);
        //        if (!String.IsNullOrEmpty(note))
        //            poznB.AppendLine(note);
        //        if (poznB.Length > 0)
        //            tw.WriteLine("              <vyd:note>" + poznB.ToString() + "</vyd:note>");

        //        tw.WriteLine("          <vyd:intNote>" + (poznamka2 == null ? string.Empty : poznamka2 + "\n") + "nacteno z konzole z dávky č.:" + countEntries.ToString() + "</vyd:intNote>");

        //        // datum dokladu, pokud bylo zadano na terminalu ...
        //        if (datumdokladu.HasValue)
        //        {
        //            tw.WriteLine("          <vyd:date>" + XmlConvert.ToString(datumdokladu.Value, "yyyy-MM-dd") + "</vyd:date>");
        //        }

        //        //rada dokladu pro cizi menu ...
        //        //if (!string.IsNullOrEmpty(idsradadokladu))
        //        //{
        //        //    tw.WriteLine("          <vyd:number>");
        //        //    tw.WriteLine("              <typ:ids>" + idsradadokladu + "</typ:ids>");
        //        //    tw.WriteLine("          </vyd:number>");
        //        //}

        //        //partner
        //        //if (partnerID != string.Empty)
        //        //{
        //        //    tw.WriteLine("          <vyd:partnerIdentity>");
        //        //    tw.WriteLine("              <typ:id>" + partnerID + "</typ:id>");
        //        //    tw.WriteLine("          </vyd:partnerIdentity>");
        //        //}

        //        //zakazka
        //        //if (cislozak != null)
        //        //{
        //        //    tw.WriteLine("          <vyd:contract>");
        //        //    tw.WriteLine("              <typ:ids>" + cislozak + "</typ:ids>");
        //        //    tw.WriteLine("          </vyd:contract>");
        //        //}

        //        //if (cinnost.HasValue)
        //        //{
        //        //    tw.WriteLine("          <vyd:activity>");
        //        //    tw.WriteLine("              <typ:id>" + cinnost.Value + "</typ:id>");
        //        //    tw.WriteLine("          </vyd:activity>");
        //        //}

        //        //if (stredisko.HasValue)
        //        //{
        //        //    tw.WriteLine("          <vyd:centre>");
        //        //    tw.WriteLine("              <typ:id>" + stredisko.Value + "</typ:id>");
        //        //    tw.WriteLine("          </vyd:centre>");
        //        //}

        //        tw.WriteLine("      </vyd:vydejkaHeader>");

        //        //polozky 
        //        tw.WriteLine("      <vyd:vydejkaDetail>");

        //        //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter taSKZ = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter();
        //        //taSKZ.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

        //        foreach (Pohoda_DataSets.Vydej.Production_SourcesRow row in dt_ps)
        //        {
        //            //// TODO : najit polozku v objednavce ... ???
        //            ////
        //            ////var objpols = pohoda_odbpol_dt.Where(x => (x.RefSKz.ToString() == row.ITEMNMBR) && (x.));
        //            //var objpols = pohoda_odbpol_dt.Where(x => (x.ID == row.ORD)); //jedinecna vazba ...
        //            //Pohoda_DataSets.DatabasePohoda.OBJpolRow objpol = null;
        //            //if (objpols.Count() > 0)
        //            //    objpol = objpols.First();

        //            tw.WriteLine("      <vyd:vydejkaItem>");
        //            //if ((objpol != null) && (!objpol.IsRefStrNull()))
        //            //{ //stredisko
        //            //    tw.WriteLine("          <vyd:centre><typ:id>" + objpol.RefStr + "</typ:id></vyd:centre>");
        //            //}
        //            //if ((objpol != null) && (!objpol.IsRefCinNull()))
        //            //{ //cinnost
        //            //    tw.WriteLine("          <vyd:activity><typ:id>" + objpol.RefCin + "</typ:id></vyd:activity>");
        //            //}
        //            //if ((objpol != null) && (!objpol.IsCisloZAKNull()))
        //            //{ //zakazka
        //            //    tw.WriteLine("          <vyd:contract><typ:ids>" + objpol.CisloZAK + "</typ:ids></vyd:contract>");
        //            //}

        //            tw.WriteLine("          <vyd:quantity>" + ((float)row.QTYSHPPD).ToString(nfi) + "</vyd:quantity>");
        //            //if ((objpol != null) && (!objpol.IsMJNull()))
        //            //{
        //            //    tw.WriteLine("          <vyd:unit>" + objpol.MJ.Trim() + "</vyd:unit>");
        //            //}
        //            //if ((objpol != null) && (!objpol.IsMJKoefNull()))
        //            //{
        //            //    tw.WriteLine("          <vyd:coefficient>" + objpol.MJKoef.ToString(nfi) + "</vyd:coefficient>");
        //            //}
        //            //// sleva polozky ...
        //            //if ((objpol != null) && (!objpol.IsSlevaNull()))
        //            //{
        //            //    tw.WriteLine("          <vyd:discountPercentage>" + objpol.Sleva.ToString(nfi) + "</vyd:discountPercentage>");
        //            //}
        //            ////((Datasets.DatabasePohoda.OBJpolDataTable)objpol.Table).SDphColumn
        //            //if ((objpol != null))
        //            //{
        //            //    tw.WriteLine("          <vyd:payVAT>" + objpol.SDph.ToString().ToLower() + "</vyd:payVAT>");
        //            tw.WriteLine("          <vyd:payVAT>" + "false" + "</vyd:payVAT>");
        //            //}
        //            //if ((objpol != null) && (!objpol.IsRelSzDPHNull()))
        //            //{
        //            //    Classes.Pohoda.Sazby dphSazba = (Classes.Pohoda.Sazby)objpol.RelSzDPH;
        //            //    tw.WriteLine("          <vyd:rateVAT>" + dphSazba.ToString() + "</vyd:rateVAT>");
        //            tw.WriteLine("          <vyd:rateVAT>" + "none" + "</vyd:rateVAT>");
        //            //}
        //            // home currency (prevzit z objednavky ...) => jednotkova cena ...
        //            //if ((objpol != null) && (!objpol.IsKcJednNull()))
        //            //{
        //            //tw.WriteLine("          <vyd:homeCurrency><typ:price>" + objpol.KcJedn.ToString(nfi) + "</typ:vydce></vyd:homeCurrency>");

        //            #region dotaženi ceny

        //            //decimal Vnakup = (decimal)taSKZ.GetVNakupByID(int.Parse(row.ITEMNMBR));
        //            decimal Vnakup = (decimal)Database.Pohoda.SKz_GetVNakupByID(int.Parse(row.ITEMNMBR));
        //            //decimal materialy = (row.QTYSHPPD * Vnakup);
        //            #endregion

        //            tw.WriteLine("          <vyd:homeCurrency><typ:unitPrice>" + Vnakup.ToString(nfi) + "</typ:unitPrice></vyd:homeCurrency>");
        //            //}
        //            //// foreign currency (prevzit z objednavky ...) => jednotkova cena ...
        //            //if ((objpol != null) && (!objpol.IsCmJednNull()))
        //            //{
        //            //    //tw.WriteLine("          <vyd:foreignCurrency><typ:price>" + objpol.CmJedn.ToString(nfi) + "</typ:price></vyd:foreignCurrency>");
        //            //    tw.WriteLine("          <vyd:foreignCurrency><typ:unitPrice>" + objpol.CmJedn.ToString(nfi) + "</typ:unitPrice></vyd:foreignCurrency>");
        //            //}

        //            tw.WriteLine("          <vyd:stockItem>");
        //            tw.WriteLine("              <typ:stockItem>");
        //            tw.WriteLine("                  <typ:id>" + row.ITEMNMBR.Trim() + "</typ:id>");
        //            tw.WriteLine("              </typ:stockItem>");
        //            //if (row. SERLTNUM.Trim().Length > 0)
        //            //{
        //            //    tw.WriteLine("              <typ:serialNumber>" + row.SERLTNUM.Trim() + "</typ:serialNumber>");
        //            //}
        //            tw.WriteLine("          </vyd:stockItem>");
        //            tw.WriteLine("      </vyd:vydejkaItem>");
        //        }

        //        tw.WriteLine("      </vyd:vydejkaDetail>");

        //        //if (cizimenaid.HasValue)
        //        //{
        //        //    tw.WriteLine("      <vyd:prijemkaSummary>");
        //        //    tw.WriteLine("          <vyd:foreignCurrency>");
        //        //    tw.WriteLine("              <typ:currency>");
        //        //    tw.WriteLine("                  <typ:id>" + cizimenaid + "</typ:id>");
        //        //    tw.WriteLine("              </typ:currency>");
        //        //    if (!pohoda_obj.IsCmKursNull()) // kurs prebrat z objednavky ... ???
        //        //    {
        //        //        tw.WriteLine("              <typ:rate>" + pohoda_obj.CmKurs.ToString(nfi) + "</typ:rate>");
        //        //    }
        //        //    // Toto by snad mela pohoda dotahnout automaticky dle id meny ...
        //        //    //if (!pohoda_obj.IsCmMnozNull())
        //        //    //{
        //        //    //    tw.WriteLine("              <typ:amount>" + pohoda_obj.CmMnoz + "</typ:amount>");
        //        //    //}

        //        //    tw.WriteLine("          </vyd:foreignCurrency>");
        //        //    tw.WriteLine("      </vyd:prijemkaSummary>");
        //        //}


        //        tw.WriteLine("  </vyd:vydejka>");
        //        tw.WriteLine("</dat:dataPackItem>");
        //        tw.WriteLine("</dat:dataPack>");

        //        tw.Flush();
        //        tw.Close();
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex.ToString());
        //        return false;
        //    }
        //}

        #endregion


        public static bool CreateRequest_Import_Vydejka_XML_NEW(Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt_ps, string file, int countEntries, string SKL_ID, string note)
        {
            // konstanta pro tostrin() cisel na invariantni format ...
            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            

            try
            {

                if ((dt_ps == null) || (dt_ps.Count < 1))
                    return false;


                //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter taSKZ = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter();
                //taSKZ.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

                DateTime? datumdokladu = null;

                string text = null;//pohoda_obj.IsSTextNull() ? null : pohoda_obj.SText;
                string poznamka = null;//pohoda_obj.IsPoznNull() ? null : pohoda_obj.Pozn;
                string poznamka2 = null;//pohoda_obj.IsPozn2Null() ? null : pohoda_obj.Pozn2;
                StringBuilder poznB = new StringBuilder();

                if (!String.IsNullOrEmpty(poznamka))
                    poznB.AppendLine(poznamka);

                if (!String.IsNullOrEmpty(note))
                    poznB.AppendLine(note);


                string filename = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + file;
                DateTime date = DateTime.Now;

                var data = dt_ps.OrderBy(x => x.DEX_ROW_ID);
                var row_id = data.First();

                List<object> filterList = new List<object>();
                List<object> mainfilterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace vyd = "http://www.stormware.cz/schema/version_2/vydejka.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                #region Header

                List<XElement> listHeader = new List<XElement>();

                if (text != null)
                {
                    listHeader.Add(new XElement(vyd + "text", text));
                }

                if (poznB.Length > 0)
                {
                    listHeader.Add(new XElement(vyd + "note", poznB.ToString()));
                }

                listHeader.Add(new XElement(vyd + "intNote", (poznamka2 == null ? string.Empty : poznamka2 + "\n") + "nacteno z konzole z dávky č.:" + countEntries.ToString()));

                if (datumdokladu.HasValue)
                {
                    listHeader.Add(new XElement(vyd + "date", XmlConvert.ToString(datumdokladu.Value, "yyyy-MM-dd")));
                }

                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();

                foreach (Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow row in dt_ps)
                {

                    //Varianta Dotahuje se nakupni cena z SKz a vyplnuje se natvrdo BEZ DPH


                    ////decimal Vnakup = (decimal)taSKZ.GetVNakupByID(int.Parse(row.ITEMNMBR));
                    //decimal Vnakup = (decimal)Database.Pohoda.SKz_GetVNakupByID(int.Parse(row.ITEMNMBR));

                    //XElement item = new XElement(vyd + "vydejkaItem",
                    //    new XElement(vyd + "quantity", ((float)row.QTYSHPPD).ToString(nfi)),
                    //    new XElement(vyd + "payVAT", false),
                    //    new XElement(vyd + "rateVAT", "none"),

                    //    new XElement(vyd + "homeCurrency", new XElement(typ + "unitPrice", Vnakup.ToString(nfi))),
                    //    new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));



                    // Varinata, že se neposila NIC a dotahuje se cena se dela podle logiky POHODY (nakupni s SKz + 21% DPH)

                    //decimal Vnakup = 0;

                    //TaD 9.9.2019 na rozkaz JaS zakomentovano, do vydejky se ma cena dotahovat dle logiky pohody
                    // jedná se o výdejku vramci firmy, takže ceny neřeším ja ale řeší si to pohoda
                   

                    XElement item = new XElement(vyd + "vydejkaItem",
                        new XElement(vyd + "quantity", ((float)row.QTYSHPPD).ToString(nfi)),
                        new XElement(vyd + "unit", row.MJ.Trim()),
                        new XElement(vyd + "discountPercentage", 0),
                        //new XElement(vyd + "payVAT", false),
                        //new XElement(vyd + "rateVAT", "none"),
                        //new XElement(vyd + "homeCurrency", new XElement(typ + "unitPrice", Vnakup.ToString(nfi))),
                        new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

                                        

                    listItem.Add(item);
                }


                #endregion


                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "vyd", "http://www.stormware.cz/schema/version_2/vydejka.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "vyd_ps" + row_id.DEX_ROW_ID.ToString()),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_K),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Import Vydejky"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "VYD_ps" + row_id.DEX_ROW_ID.ToString()),
                            new XAttribute("version", "2.0"),

                                new XElement(vyd + "vydejka",
                                    new XAttribute("version", "2.0"),
                                    new XElement(vyd + "vydejkaHeader", listHeader),
                                    new XElement(vyd + "vydejkaDetail", listItem)
                                        )));

                Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info,"Ukladany soubor Vydejka :'" + filename + "'"); 
                root.Save(filename);

            } 
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
            return true;
        }

        public static string LoadResponse_Import_Vydejka_XML(Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt_ps, string file, int countEntries, string SKL_ID)
        {
            try
            {
                //string filename = Globals.PathToInputDirectory + "Response\\" + file;
                string filename = file;

                if (!File.Exists(filename))
                    throw new Exception("Response soubor '" + file + "' neexistuje!");

                //Pouzite namespacy
                XNamespace rdc = "http://www.stormware.cz/schema/version_2/documentresponse.xsd";
                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace rsp = "http://www.stormware.cz/schema/version_2/response.xsd";
                XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
                XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
                XNamespace vyd = "http://www.stormware.cz/schema/version_2/vydejka.xsd";

                // nacteni hodnot z odpovedi ...
                XDocument root = XDocument.Load(filename);

                XElement responsePack = root.Element(rsp + "responsePack");
                XAttribute state = responsePack.Attribute("state");
                if (state.Value != "ok")
                {
                    XAttribute note = responsePack.Attribute("note");
                    throw new Exception("Nepodařilo se získat data výdejky.\nStatus:" + state.Value + "\nChyba:" + note.Value);
                }
                XElement responsePackItem = responsePack.Element(rsp + "responsePackItem");
                XAttribute responsePackItemState = responsePackItem.Attribute("state");
                if (responsePackItemState.Value.Trim() != "ok")
                {
                    XAttribute responsePackItemNote = responsePackItem.Attribute("note");
                    throw new Exception("Nepodařilo se získat data převodky.\nStatus:" + responsePackItemState.Value + "\nChyba:" + responsePackItemNote.Value);
                }

                XElement vydejkaResponse =
                    root.Element(rsp + "responsePack").
                        Element(rsp + "responsePackItem").
                            Element(vyd + "vydejkaResponse");

                XElement dokladnumber =
                    vydejkaResponse.Element(rdc + "producedDetails").
                    Element(rdc + "number");

                string number = dokladnumber.Value;

                // koretne ulozeno a muzu dokoncit ... 
                // dokonceni => ISOK = datetime.now
                DateTime isOK = DateTime.Now;
                dt_ps.ToList().ForEach(x => x.ISOK = isOK);


                return number;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string LoadResponse_Import_Vydejka_XML(string file, int countEntries, string SKL_ID)
        {
            try
            {
                //string filename = Globals.PathToInputDirectory + "Response\\" + file;
                string filename = file;

                if (!File.Exists(filename))
                    throw new Exception("Response soubor '" + file + "' neexistuje!");

                //Pouzite namespacy
                XNamespace rdc = "http://www.stormware.cz/schema/version_2/documentresponse.xsd";
                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace rsp = "http://www.stormware.cz/schema/version_2/response.xsd";
                XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
                XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
                XNamespace vyd = "http://www.stormware.cz/schema/version_2/vydejka.xsd";

                // nacteni hodnot z odpovedi ...
                XDocument root = XDocument.Load(filename);

                XElement responsePack = root.Element(rsp + "responsePack");
                XAttribute state = responsePack.Attribute("state");
                if (state.Value != "ok")
                {
                    XAttribute note = responsePack.Attribute("note");
                    throw new Exception("Nepodařilo se získat data výdejky.\nStatus:" + state.Value + "\nChyba:" + note.Value);
                }
                XElement responsePackItem = responsePack.Element(rsp + "responsePackItem");
                XAttribute responsePackItemState = responsePackItem.Attribute("state");
                if (responsePackItemState.Value.Trim() != "ok")
                {
                    XAttribute responsePackItemNote = responsePackItem.Attribute("note");
                    throw new Exception("Nepodařilo se získat data převodky.\nStatus:" + responsePackItemState.Value + "\nChyba:" + responsePackItemNote.Value);
                }

                XElement vydejkaResponse =
                    root.Element(rsp + "responsePack").
                        Element(rsp + "responsePackItem").
                            Element(vyd + "vydejkaResponse");

                XElement dokladnumber =
                    vydejkaResponse.Element(rdc + "producedDetails").
                    Element(rdc + "number");

                string number = dokladnumber.Value;

                return number;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        
        internal static void LocncodeFindAlgorithmVychozi(Pohoda_DataSets.Vydej.CZMST_SERow seRow)
        {
            // TODO : dotazeni vychozi lokace pro polozku skladu ...
            // pro ruzne MJ se muze lisit??? => mozna do budoucna ...
            Provider.Provider provider = new Provider.Provider();
            Fask.Interfaces.DataSets.Location locationDS = provider.Lokace_VariantySortimentGet(seRow.ITEMNMBR.Trim(), seRow.IsSKL_IDNull() ? string.Empty : seRow.SKL_ID.Trim());
            var loctypedefaultrows = locationDS.CZMST_SkladLokace_LokaceTypy.Where(x => x.IS_DEFAULT);
            if (loctypedefaultrows.Count() > 0)
            {
                var loctypedefault = loctypedefaultrows.First();
                var loctypedefaultforitemrows = locationDS.CZMST_SkladLokace_LokaceVariantySortiment.Where(x => x.TYPE.Trim() == loctypedefault.TYPE.Trim());
                if (loctypedefaultforitemrows.Count() > 0)
                {
                    var loctypedefaultforitem = loctypedefaultforitemrows.First();
                    seRow.LOCNCODE = loctypedefaultforitem.LOCNCODE.Trim();
                }
            }

            // 20.10.2016 JiS uprava pro nastaveni vychozi lokace na lokaci, ktera ma dostatek kusu k vykryti...
            // algoritmy pro dohledavani vychozich/doporucenych lokaci pro vydej

            // 0) nacist stav materialu z lok.mech
            // a) pokud stavajici lokace (vychozi) ma dostatek materialu, tak ponechat
            // b) pokud lokace nesplnuje vykryti, tak se pokusit nalezt lokaci, kterou bude pozadavek vykryt
            // c) pokud neexistuje lokace, ktera uplne vykryje potrebu, pak lokaci s nejmensim/nejvetsim mnozstvim?
            decimal pozadavekMnozstvi = seRow.QTYSHPPD * (seRow.QTYPACK == 0 ? 1 : seRow.QTYPACK);
            var locationMaterialStav = provider.Lokace_ShowMaterial(seRow.ITEMNMBR);

            // 1.algoritmus dle pozadavku fy Perlaccasa (Vychozi/Dostatek/Nejvetsi/Vychozi)
            // TODO : nastavit konfiguracni podminku
            if (true)
            {
                // test na dostatek materialu na vychozi lokaci
                // seRow.Locncode je nyni nastaveno na vychozi lokaci nebo neni nastaveno ...
                var locationMaterialVychoziDostatek = locationMaterialStav.CZMST_SkladLokace_Stav
                    .Where(x => x.LOCNCODE.Trim() == seRow.LOCNCODE.Trim() && x.SKL_ID.Trim() == seRow.SKL_ID.Trim())
                    .Where(x => x.QTYSHPPD >= pozadavekMnozstvi);
                if (locationMaterialVychoziDostatek.Count() > 0)
                {   // na vychozi lokaci JE dostatek materialu => konec
                    return;
                }
                // pokud dojdu sem, tak na vychozi lokaci neni dostatek mnozstvi  => hledam
                // filtr na sklad a mnozstvi vetsi jak 0 !!!
                var locationMaterialDostatek = locationMaterialStav.CZMST_SkladLokace_Stav
                    .Where(x => x.SKL_ID.Trim() == seRow.SKL_ID.Trim())
                    .Where(x => x.QTYSHPPD > 0)         // omezit na vetsi jak nula, protoze stavlokace vraci !Ruzne od nula!q<>0
                    .OrderByDescending(x => x.QTYSHPPD);
                if (locationMaterialDostatek.Count() > 0)
                { // nalezeno, tak vratim prvni, kterou najdu at uz na ni je dostatek nebo neni
                    seRow.LOCNCODE = locationMaterialDostatek.First().LOCNCODE.Trim();
                    return;
                }

            }
        }

        #region Export

        #region Prevodka
        /// <summary>
        /// Export dokladu prevodky
        /// </summary>
        /// <param name="file">nazev souboru kam se request ulozi</param>
        /// <param name="objednavka">id dokladu</param>
        /// <returns>True : ok; False(exception) : nepovedlo se sestaveni nebo ulozeni(False: bez vyjimky, duvod neznamy; Exception: vyjimka)</returns>
        public static bool CreateRequest_Prevodka_XML(string file, Fask.Interfaces.Classes.Objednavka objednavka)
        {

            // 
            // ***** VZOR ****
            // 
            //<?xml version="1.0" encoding="utf-8" ?>
            //<dat:dataPack
            //  xmlns:dat="http://www.stormware.cz/schema/version_2/data.xsd"
            //  xmlns:lst="http://www.stormware.cz/schema/version_2/list.xsd"
            //  xmlns:typ="http://www.stormware.cz/schema/version_2/type.xsd"
            //  xmlns:ftr="http://www.stormware.cz/schema/version_2/filter.xsd"
            //  version="2.0"
            //  id="PrevodkaRequest_01"
            //  ico="12345678"
            //  application="FASK_prevodka_Test"
            //  note="Fask prevodka_test_request_01"
            //>
            //  <dat:dataPackItem id="prevodka_test_request_01" version="2.0">
            //    <lst:listPrevodkaRequest version="2.0" prevodkaVersion="2.0">
            //      <lst:requestPrevodka>
            //        <ftr:filter>
            //          <ftr:selectedNumbers>
            //            <ftr:number>
            //              <typ:numberRequested>15Prv00001</typ:numberRequested>
            //            </ftr:number>
            //          </ftr:selectedNumbers>
            //        </ftr:filter>
            //      </lst:requestPrevodka>
            //    </lst:listPrevodkaRequest>
            //  </dat:dataPackItem>
            //</dat:dataPack>

            try
            {
                string filename = Path.Combine(Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory, file);

                List<object> filterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
                XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                XElement ftrSelectedNumbers =
                    new XElement(ftr + "selectedNumbers",
                        new XElement(ftr + "number",
                            new XElement(typ + "numberRequested", objednavka.ID.Trim())
                        )
                    );

                XElement mainFilter = new XElement(ftr + "filter", ftrSelectedNumbers);

                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "lst", "http://www.stormware.cz/schema/version_2/list.xsd"),
                    new XAttribute(XNamespace.Xmlns + "ftr", "http://www.stormware.cz/schema/version_2/filter.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "prevodka"),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_K),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Fask export prevodka"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "prevodkaRequest"),
                            new XAttribute("version", "2.0"),

                                new XElement(lst + "listPrevodkaRequest",
                                    new XAttribute("version", "2.0"),
                    // new XAttribute("invoiceType", "issuedInvoice"),  // existuje prevodka type??
                                    new XAttribute("prevodkaVersion", "2.0"),
                                        new XElement(lst + "requestPrevodka", mainFilter)
                                        )));

                root.Save(filename);

                return true;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
                //return false;
            }

            //
            // ***** VZOR ****
            //
            //<?xml version="1.0" encoding="utf-8" ?>
            //<dat:dataPack 
            //  xmlns:dat="http://www.stormware.cz/schema/version_2/data.xsd"
            //  xmlns:lst="http://www.stormware.cz/schema/version_2/list.xsd"
            //  xmlns:ftr="http://www.stormware.cz/schema/version_2/filter.xsd"
            //  xmlns:typ="http://www.stormware.cz/schema/version_2/type.xsd"
            //  version="2.0"
            //  id="issuedInvoiceRequest_01"
            //  ico="111111"
            //  application="FASK_Test"
            //  note="Fask test issued invoice request 01"
            //  >
            //  <dat:dataPackItem id="issuedInvoiceRequest_01" version="2.0">
            //    <lst:listInvoiceRequest version="2.0" invoiceType="issuedInvoice" invoiceVersion="2.0">
            //      <lst:requestInvoice>
            //        <ftr:filter>
            //          <ftr:selectedNumbers>
            //            <ftr:number>
            //              <typ:numberRequested>XXX</typ:numberRequested>
            //            </ftr:number>
            //            <ftr:number>
            //              <typ:numberRequested>YYY</typ:numberRequested>
            //            </ftr:number>
            //          </ftr:selectedNumbers>
            //        </ftr:filter>
            //      </lst:requestInvoice>
            //    </lst:listInvoiceRequest>
            //  </dat:dataPackItem>
            //</dat:dataPack>

            //try
            //{
            //    string filename = Path.Combine(Globals.PathToInputDirectory, file);

            //    List<object> filterList = new List<object>();

            //    XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
            //    XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
            //    XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
            //    XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

            //    XElement ftrSelectedNumbers =
            //        new XElement(ftr + "selectedNumbers",
            //            new XElement(ftr + "number",
            //                new XElement(ftr + "numberRequested", objednavka.ID.Trim())
            //            )
            //        );

            //    XElement mainFilter = new XElement(ftr + "filter", ftrSelectedNumbers);

            //    XElement root = new XElement(dat + "dataPack",
            //        new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
            //        new XAttribute(XNamespace.Xmlns + "lst", "http://www.stormware.cz/schema/version_2/list.xsd"),
            //        new XAttribute(XNamespace.Xmlns + "ftr", "http://www.stormware.cz/schema/version_2/filter.xsd"),
            //        new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
            //        new XAttribute("id", "issuedInvoice"),
            //        new XAttribute("ico", Globals.ICO),
            //        new XAttribute("application", Fask.ModulePohodaXML.Constants.Common.application),
            //        new XAttribute("version", "2.0"),
            //        new XAttribute("note", "Fask export issued invoice"),

            //            new XElement(dat + "dataPackItem",
            //                new XAttribute("id", "issuedInvoiceRequest"),
            //                new XAttribute("version", "2.0"),

            //                    new XElement(lst + "listInvoiceRequest",
            //                        new XAttribute("version", "2.0"),
            //                        new XAttribute("invoiceType", "issuedInvoice"),
            //                        new XAttribute("invoiceVersion", "2.0"),
            //                            new XElement(lst + "requestInvoice", mainFilter)
            //                            )));

            //    root.Save(filename);

            //    return true;

            //}
            //catch (Exception ex)
            //{
            //    Log.Write(ex.ToString());
            //    throw ex;
            //    //return false;
            //}
        }

        /// <summary>
        /// Nacte data z odpovedi prevodky
        /// </summary>
        /// <param name="filename">cesta ke xml souboru</param>
        public static string LoadResponse_Prevodka_XML(string file, bool save, Fask.Interfaces.Classes.Objednavka objednavka)
        {
            Globals_V1.LoadConfiguration();

            string filename = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + "Response\\" + file;

            if (!File.Exists(filename))
                throw new Exception("Response soubor '" + file + "' neexistuje!");

            //Prevodka pData = new Prevodka();

            int SE_MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
            int SE_ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
            int SE_CZ_CarKod_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["CZ_CarKod"].MaxLength;

            Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
            CZMST_SETableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);


            Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
            if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
            {
                ParamTA = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            }

            //Pouzite namespacy
            XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
            XNamespace rsp = "http://www.stormware.cz/schema/version_2/response.xsd";
            XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
            XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
            XNamespace pre = "http://www.stormware.cz/schema/version_2/prevodka.xsd";

            // nacteni hodnot z odpovedi ...
            XDocument root = XDocument.Load(filename);

            XElement responsePack = root.Element(rsp + "responsePack");
            XAttribute state = responsePack.Attribute("state");
            if (state.Value != "ok")
            {
                XAttribute note = responsePack.Attribute("note");
                throw new Exception("Nepodařilo se získat data převodky.\nStatus:" + state.Value + "\nChyba:" + note.Value);
            }
            XElement responsePackItem = responsePack.Element(rsp + "responsePackItem");
            XAttribute responsePackItemState = responsePackItem.Attribute("state");
            if (responsePackItemState.Value.Trim() != "ok")
            {
                XAttribute responsePackItemNote = responsePackItem.Attribute("note");
                throw new Exception("Nepodařilo se získat data převodky.\nStatus:" + responsePackItemState.Value + "\nChyba:" + responsePackItemNote.Value);
            }

            XElement prevodka =
                root.Element(rsp + "responsePack").
                    Element(rsp + "responsePackItem").
                        Element(lst + "listPrevodka").
                            Element(lst + "prevodka");

            // TODO: prepsat hlasku
            if (prevodka == null)
                throw new Exception("Response byla uspesne vygenerovana, ale prevodka pravdepodoobne neexistuje!");

            // nacteni headeru prevodky
            XElement numberRequested = prevodka.
                Element(pre + "prevodkaHeader").
                    Element(pre + "number").
                        Element(typ + "numberRequested");
            string ponumber = numberRequested.Value;

            // nacteni samotnych dat
            var prevodkaItems = prevodka.
                Element(pre + "prevodkaDetail").
                    Elements(pre + "prevodkaItem");

            // TODO: Poresit, pustit, nepustit ...
            if (Database.Prijem.CZMSTPE_PONUMBER_EXIST(ponumber))
            {
                // toto cislo davky se uz v db vyskytuje -> smazeme jej
                bool test = Database.Prijem.CZMSTPE_UPDATE_CZDOSLO(ponumber);
                if (test)
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info,"prijem - proveden update cz_doslo na 201 u prevodky=" + ponumber);
                else
                {
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Info, "prijem - nebyl proveden update cz_doslo na 201 u prevodky=" + ponumber);
                }
            }

            // nacteni cisla davky a zvetseni o 1
            //int countEntries = Database.Vydej.CZMSTSE_MAX_CountEntries();
            //countEntries += 1;
            int countEntries = 0;

            Pohoda_DataSets.Vydej.CZMST_SEDataTable seTable = new Pohoda_DataSets.Vydej.CZMST_SEDataTable();
            Pohoda_DataSets.Vydej.CZMST_SERow seRow = null;

            int ord = 0;
            // nacteni vsech polozek
            foreach (var prevodkaItem in prevodkaItems)
            {
                // Pokud to neni skladova polozka, tak ji neresit ...
                XElement stockItem = prevodkaItem.Element(pre + "stockItem");
                if (stockItem == null)
                    continue;

                seRow = seTable.NewCZMST_SERow();
                NastavPromenne(seRow);

                // nastaveni promennych
                NastavPromenne(seRow);
                seRow.CountEntries = countEntries;  // countentries
                seRow.SOPNUMBE = ponumber;  // ponumber                
                seRow.ITEMNMBR = prevodkaItem.
                Element(pre + "stockItem").
                    Element(typ + "stockItem").
                        Element(typ + "id").Value;  // itemnmbr

                seRow.ORD = --ord;

                //Pohoda_DataSets.DatabasePohoda.SKzDataTable SKzrows = SKzTableAdapter.GetDataByID(Convert.ToInt32(seRow.ITEMNMBR));
                Pohoda_DataSets.DatabasePohoda.SKzDataTable SKzrows = Database.Pohoda.SKz_GetDataByID(Convert.ToInt32(seRow.ITEMNMBR));
                // polozka nalezena, doplnit data ...
                if (SKzrows.Count > 0)
                {
                    // qtyshppd
                    seRow.QTYSHPPD = Convert.ToDecimal(prevodkaItem.Element(pre + "quantity").Value, System.Globalization.NumberFormatInfo.InvariantInfo);

                    // nacteni prvniho zaznamu
                    Pohoda_DataSets.DatabasePohoda.SKzRow SKzrow = SKzrows.First();
                    // dotazeni vychozi lokace pro zasobu
                    string locncodeDeafult = string.Empty;
                    if (SKzrow != null)
                    {
                        if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                        {
                            //Pohoda_DataSets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, SKzrow.ID);
                            Pohoda_DataSets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, SKzrow.ID);
                            Pohoda_DataSets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                            if (skzParametry_row != null)
                            {
                                locncodeDeafult = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                            }
                        }
                    }

                    seRow.LOCNCODE = locncodeDeafult;

                    // nastaveni CZ_SerNumTrack
                    // Skz.RelSKzVC" -> CZ_SerNumTrack ("2" Sarze a "1" vyrobni cislo v Pohode)
                    //if (SKzrow.IsRelSKzVCNull())
                    //    seRow.CZ_SW_Track = 0;
                    //else
                    //    seRow.CZ_SW_Track = (byte)SKzrow.RelSKzVC;
                    //seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack(SKzrow.IsRelSKzVCNull() ? (int?)null : (int?)SKzrow.RelSKzVC);

                    //TaD 1.8.2019 , přechod na FASK_ZASOBY_PARAMETRY
                    //if (Properties.Settings.Default.POHODA_E1)
                    //{
                    //    if (SKzrow.IsVPrFXTSNull())
                    //    {
                    //        seRow.CZ_SerNum_Track = 0;
                    //    }
                    //    else
                    //    {
                    //        if (SKzrow.VPrFXTS)
                    //        {
                    //            seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((SKzrow == null) || (SKzrow.IsRefVPrFXTSNull()) ? (int?)null : ((int?)SKzrow.RefVPrFXTS - 1), SKzrow.VPrFXTS);
                    //        }
                    //        else
                    //        {
                    //            seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((SKzrow == null) || (SKzrow.IsRefVPrFVTSNull()) ? (int?)null : ((int?)SKzrow.RefVPrFVTS - 1), SKzrow.VPrFVTS);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((SKzrow == null) || (SKzrow.IsRelSKzVCNull()) ? (int?)null : (int?)SKzrow.RelSKzVC, true);
                    //}

                    //TaD 1.8.2019 , přechod na FASK_ZASOBY_PARAMETRY
                     //= Classes.Pohoda.GetPriznakSledovani(SKzrow.IsRelSKzVCNull() ? (int?)null : (int?)SKzrow.RelSKzVC, SKzrow, Classes.Pohoda.TypAgendy.Vydej);

                    if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                    {
                        seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(SKzrow.IsRelSKzVCNull() ? (int?)null : (int?)SKzrow.RelSKzVC, SKzrow);
                    }
                    else
                    {
                        Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
                        var dt_param = ParamTA.GetDataByITEMNMBR(seRow.ITEMNMBR.Trim());

                        if ((dt_param != null) && (dt_param.Count > 0))
                        {
                            dt_row_param = dt_param.First();
                        }

                        seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(SKzrow.IsRelSKzVCNull() ? (int?)null : (int?)SKzrow.RelSKzVC, dt_row_param, Pohoda.TypAgendy.Vydej);
                    }

                    seRow.VNDITNUM = (string)prevodkaItem.
                    Element(pre + "stockItem").
                        Element(typ + "stockItem").
                            Element(typ + "EAN") ?? string.Empty;

                    // cz_carkod
                    seRow.CZ_CarKod = SKzrow.IsIDSNull() ? string.Empty : SKzrow.IDS;
                    if (seRow.CZ_CarKod.Length > SE_CZ_CarKod_MaxLength)
                    {
                        seRow.CZ_CarKod = seRow.CZ_CarKod.Remove(SE_CZ_CarKod_MaxLength);
                    }

                    // itemdesc
                    seRow.ITEMDESC = SKzrow.Nazev;
                    if (seRow.ITEMDESC.Length > SE_ITEMDESC_MaxLength)
                    {
                        seRow.ITEMDESC = seRow.ITEMDESC.Remove(SE_ITEMDESC_MaxLength);
                    }

                    // skl_id
                    seRow.SKL_ID = (string)prevodkaItem.
                    Element(pre + "stockItem").
                        Element(typ + "store").
                            Element(typ + "id") ?? string.Empty;

                    seRow.MJ = SKzrow.IsMJNull() ? string.Empty : SKzrow.MJ;
                    if (seRow.MJ.Length > SE_MJ_MaxLength)
                    {
                        seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                    }

                    // Nacist vychozi lokaci ...
                    // az tady, protoze musi mit nastaveny skl_id a itemnmbr...
                    LocncodeFindAlgorithmVychozi(seRow);

                    seTable.AddCZMST_SERow(seRow);

                    if (Globals_V1.Konfigurace.Vydej[0].MerneJednotky_DotahovatDalsiVarianty)
                    {
                        // MJ2 a qtypack (pokud existuje)
                        if (!SKzrow.IsMJ2Null() && !SKzrow.IsMJ2KoefNull())
                        {
                            // kopie zaznamu
                            Pohoda_DataSets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                            seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                            seRow = seRow2;

                            seRow.QTYPACK = (decimal)SKzrow.MJ2Koef;
                            seRow.MJ = SKzrow.MJ2;
                            if (seRow.MJ.Length > SE_MJ_MaxLength)
                            {
                                seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                            }

                            seTable.AddCZMST_SERow(seRow);
                        }

                        // MJ2 a qtypack (pokud existuje)
                        if (!SKzrow.IsMJ3Null() && !SKzrow.IsMJ3KoefNull())
                        {
                            // kopie zaznamu
                            Pohoda_DataSets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                            seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                            seRow = seRow2;

                            seRow.QTYPACK = (decimal)SKzrow.MJ3Koef;
                            seRow.MJ = SKzrow.MJ3;
                            if (seRow.MJ.Length > SE_MJ_MaxLength)
                            {
                                seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                            }

                            seTable.AddCZMST_SERow(seRow);
                        }
                    }
                }
                else
                    throw new Exception("LoadResponse_Prevodka_XML (Prevodka), Zaznam s ID '" + seRow.ITEMNMBR + "' nebyl nalezen v seznamu skladovych karet.");

                seRow = null;
            }



            try
            {
                // ulozeni do czmst_se
                CZMST_SETableAdapter.Connection.Open();
                CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                // prideleni cisla davky v transakci ..
                countEntries = Database.Vydej.CZMSTSE_MAX_CountEntries(CZMST_SETableAdapter.Connection, CZMST_SETableAdapter.Transaction);
                countEntries += 1;
                foreach (var item in seTable)
                {
                    item.CountEntries = countEntries;

                    // porad je pouze pridana, nikoli zmenena po zmene countentries...
                    item.AcceptChanges();
                    item.SetAdded();
                }
                int updatedRows = CZMST_SETableAdapter.Update(seTable);
                CZMST_SETableAdapter.Transaction.Commit();
                objednavka.CisloDavky = countEntries.ToString();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                try
                {
                    CZMST_SETableAdapter.Transaction.Rollback();
                }
                catch { }
                throw new Exception("Uložení načtených položek se nezdařilo!");
            }
            finally
            {
                if ((CZMST_SETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    CZMST_SETableAdapter.Connection.Close();
            }

            return "OK";
        }

        #endregion

        #region Vydejka
        /// <summary>
        /// Export dokladu Vydejky (vydejka)
        /// </summary>
        /// <param name="file">nazev souboru kam se request ulozi</param>
        /// <param name="objednavka">id dokladu</param>
        /// <returns>True : ok; False(exception) : nepovedlo se sestaveni nebo ulozeni(False: bez vyjimky, duvod neznamy; Exception: vyjimka)</returns>
        public static bool CreateRequest_Vydejka_XML(string file, Fask.Interfaces.Classes.Objednavka objednavka)
        {
            //
            // ***** VZOR ****
            //
            //<?xml version="1.0" encoding="utf-8" ?>
            //<dat:dataPack
            //  xmlns:dat="http://www.stormware.cz/schema/version_2/data.xsd"
            //  xmlns:lst="http://www.stormware.cz/schema/version_2/list.xsd"
            //  xmlns:typ="http://www.stormware.cz/schema/version_2/type.xsd"
            //  xmlns:ftr="http://www.stormware.cz/schema/version_2/filter.xsd"
            //  xmlns:vyd="http://www.stormware.cz/schema/version_2/vydejka.xsd"
            //  version="2.0"
            //  id="Vydejka ExportRequest 01"
            //  ico="63489040"
            //  application="FASK Test vydejka 01"
            //  note="Fask vydejka request 01"
            //>
            //  <dat:dataPackItem id="vydejka test request 01" version="2.0">
            //    <lst:listVydejkaRequest vydejkaVersion="2.0" version="2.0">
            //      <lst:requestVydejka>
            //        <ftr:filter>
            //          <ftr:selectedNumbers>
            //            <ftr:number>
            //              <typ:numberRequested>16SV00001</typ:numberRequested>
            //            </ftr:number>
            //          </ftr:selectedNumbers>
            //        </ftr:filter>
            //      </lst:requestVydejka>
            //    </lst:listVydejkaRequest>
            //  </dat:dataPackItem>
            //</dat:dataPack>

            try
            {
                string filename = Path.Combine(Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory, file);

                List<object> filterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
                XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                XElement ftrSelectedNumbers =
                    new XElement(ftr + "selectedNumbers",
                        new XElement(ftr + "number",
                            new XElement(typ + "numberRequested", objednavka.ID.Trim())
                        )
                    );

                XElement mainFilter = new XElement(ftr + "filter", ftrSelectedNumbers);

                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "lst", "http://www.stormware.cz/schema/version_2/list.xsd"),
                    new XAttribute(XNamespace.Xmlns + "ftr", "http://www.stormware.cz/schema/version_2/filter.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "vydejka"),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_K),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Fask export vydejka"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "vydejkaRequest"),
                            new XAttribute("version", "2.0"),

                                new XElement(lst + "listVydejkaRequest",
                                    new XAttribute("version", "2.0"),
                                    new XAttribute("vydejkaVersion", "2.0"),
                                        new XElement(lst + "requestVydejka", mainFilter)
                                        )));

                root.Save(filename);

                return true;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
                //return false;
            }
        }

        /// <summary>
        /// Nacte data z odpovedi vydejky
        /// </summary>
        /// <param name="filename">cesta ke xml souboru</param>
        public static string LoadResponse_Vydejka_XML(string file, Fask.Interfaces.Classes.Objednavka objednavka)
        {
            Globals_V1.LoadConfiguration();
            string filename = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + "Response\\" + file;

            //Datasety pro komunikaci a zjistovani dat ...
            //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter();
            //SKzTableAdapter.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

            // dotazeni parametru pro locncode (vychozi lokaci ..)
            //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
            //SKzParametryTableAdapter.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

            Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
            CZMST_SETableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            Pohoda_DataSets.Vydej.CZMST_SEDataTable seTable = new Pohoda_DataSets.Vydej.CZMST_SEDataTable();
            Pohoda_DataSets.Vydej.CZMST_SERow seRow = null;


            Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
            if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
            {
                ParamTA = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            }

            // HAVETO : Vice uzivatelsky pristup pri generovani dalsiho cisla davky je problematicky timto zpusobem...
            // Melo by byt reseno nejakym jinym mechanismem !!!
            //int cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries() + 1;
            int cisloDavky = 0;

            //Nacteni odpovedi ... 
            // - pokud neexistuje, tak se vyvola vyjimka ... ??? => mozna radeji nejdrive otestovat?

            if (!File.Exists(filename))
                throw new Exception("Response soubor neexistuje!");

            //Pouzite namespacy
            XNamespace rsp = "http://www.stormware.cz/schema/version_2/response.xsd";
            XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
            XNamespace vyd = "http://www.stormware.cz/schema/version_2/vydejka.xsd";
            // nacteni hodnot z odpovedi ...
            XDocument root = XDocument.Load(filename);

            XElement responsePack = root.Element(rsp + "responsePack");
            XAttribute state = responsePack.Attribute("state");
            if (state.Value != "ok")
            {
                XAttribute note = responsePack.Attribute("note");
                throw new Exception("Nepodařilo se získat data výdejky.\nStatus:" + state.Value + "\nChyba:" + note.Value);
            }
            XElement responsePackItem = responsePack.Element(rsp + "responsePackItem");
            XAttribute responsePackItemState = responsePackItem.Attribute("state");
            if (responsePackItemState.Value.Trim() != "ok")
            {
                XAttribute responsePackItemNote = responsePackItem.Attribute("note");
                throw new Exception("Nepodařilo se získat data výdejky.\nStatus:" + responsePackItemState.Value + "\nChyba:" + responsePackItemNote.Value);
            }

            // ok hlavicka je v poradku
            // dal overit vracenou fakturu => ocekavam prave jednu
            XElement listVydejka = responsePackItem.Element(lst + "listVydejka");
            XAttribute listVydejkaState = listVydejka.Attribute("state");
            if (listVydejkaState.Value != "ok")
            {
                // TODO : docilit nejake chyby ...??? => jak je chyba indikovana??? <rdc:details>???
                throw new Exception("Nepodařilo se získat data výdejky.\nStatus:" + state.Value);
            }
            // ok => bez chyby ... fakturu ...
            XElement vydejka = listVydejka.Element(lst + "vydejka");
            // Nemusi obsahovat data => faktura neexistuje, respektive nebyla urcena pro zpracovani??? 
            // => filtrovat dotaz na zaklade nejakeho priznaku nebo uzivatelskeho filtru ???
            if (vydejka == null)
            {
                throw new Exception("Výdejka nebyla nalezena!");
            }
            // ok mam fakturu, tak jdeme na to ...
            // faktura obsahuje:
            // 1) Header (invoiceHeader)
            // => overit cislo dokladu : <inv:number><typ:numberRequested>
            // => text dokladu : <inv:text>
            // => odberatel : <inv:partnerIdentity>
            // 2) Detail polozek... (invoiceDetail)
            // => Pocet polozek dle poctu vyskytu : <inv:invoiceItem>*
            // => muze tam byt textova !!!
            // 3) Paticku (invoiceSummary)
            // => nic zajimaveho ...???

            XElement vydejkaHeader = vydejka.Element(vyd + "vydejkaHeader");
            XElement vydejkaDetail = vydejka.Element(vyd + "vydejkaDetail");
            XElement vydejkaSummary = vydejka.Element(vyd + "vydejkaSummary");

            //1) hlavicka ...
            XElement numberRequested = vydejkaHeader.Element(vyd + "number").Element(typ + "numberRequested");
            if (!numberRequested.Value.Trim().Equals(objednavka.ID.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("Číslo požadovaného dokladu se liší!");
            }
            XElement vydejkaText = vydejkaHeader.Element(vyd + "text");

            // Definice MaxLenght
            int SE_MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
            int SE_ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
            
            
            

            //2) polozky
            // vsechny polozky
            var vydejkaItems = vydejkaDetail.Elements(vyd + "vydejkaItem");
            // pro kazdou polozku provest ulozeni do mst tabulky ...
            // Nejlepe v transakci ...
            foreach (var vydejkaItem in vydejkaItems)
            {
                // Pokud to neni skladova polozka, tak ji neresit ...
                // TODO : otestovat, zda to takto je ... => tedy test na NULL hodnotu...
                XElement stockItem = vydejkaItem.Element(vyd + "stockItem");
                if (stockItem == null)
                    continue;

                XElement typEAN = stockItem.Element(typ + "stockItem").Element(typ + "EAN");
                XElement typPLU = stockItem.Element(typ + "stockItem").Element(typ + "PLU");
                XElement typIDS = stockItem.Element(typ + "stockItem").Element(typ + "ids");
                Pohoda_DataSets.DatabasePohoda.SKzDataTable skz_table = Database.Pohoda.SKz_GetDataByID(int.Parse(stockItem.Element(typ + "stockItem").Element(typ + "id").Value));
                Pohoda_DataSets.DatabasePohoda.SKzRow skz_row = skz_table.Count > 0 ? skz_table[0] : null;

                // dotazeni vychozi lokace pro zasobu
                string locncodeDeafult = string.Empty;
                if (skz_row != null)
                {
                    if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                    {
                        //Pohoda_DataSets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, skz_row.ID);
                        Pohoda_DataSets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, skz_row.ID);
                        Pohoda_DataSets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                        if (skzParametry_row != null)
                        {
                            locncodeDeafult = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                        }
                    }
                }

                seRow = seTable.NewCZMST_SERow();

                seRow.ITEMDESC = vydejkaItem.Element(vyd + "text").Value.Trim(); // nazev polozky => z xml <inv:text>
                if (seRow.ITEMDESC.Length > SE_ITEMDESC_MaxLength)
                {
                    seRow.ITEMDESC = seRow.ITEMDESC.Remove(SE_ITEMDESC_MaxLength);
                }

                seRow.ITEMNMBR = stockItem.Element(typ + "stockItem").Element(typ + "id").Value.Trim(); // cislo polozky => z xml <inv:stockItem><typ:stockItem><typ:id>
                seRow.ITEMTYPE = ""; //bez typu
                //seRow.ITEMCODE = ""; // ? ITEMCODE neni???
                seRow.CountEntries = cisloDavky; // TODO : ??? nove cislo davky ... 
                //seRow.CZ_CarKod = typPLU == null ? string.Empty : typPLU.Value; //carovy kod => dotahnout z xml PLU
                seRow.CZ_CarKod = typIDS == null ? string.Empty : typIDS.Value.Trim();
                seRow.CZ_DatVyr_Delka = 0; //?
                seRow.CZ_DatVyr_Track = 0; //?
                seRow.CZ_Doslo = 255; // pripraveno pro zpracovani
                seRow.CZ_SerNum_Delka = 0; //? delka serioveho cisla 
                //seRow.CZ_SerNum_Track = 0; //? sledovat na SN/Sarze => je sledovano na seriove cislo => zjistit v Pohoda
                //seRow.CZ_SerNum_Track = (byte)(skz_row == null ? 0 : skz_row.RelSKzVC);
                //seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack(
                //    (skz_row == null) || (skz_row.IsRelSKzVCNull()) ? (int?)null : (int?)skz_row.RelSKzVC
                //    );

                //MaR 29.5.2025
                seRow.CZ_REZ1_Track = 0;
                seRow.CZ_REZ2_Track = 0;
                //TaD 1.8.2019 , přechod na FASK_ZASOBY_PARAMETRY
                //if (Properties.Settings.Default.POHODA_E1)
                //{
                //    if (skz_row.IsVPrFXTSNull())
                //    {
                //        seRow.CZ_SerNum_Track = 0;
                //    }
                //    else
                //    {
                //        if (skz_row.VPrFXTS)
                //        {
                //            seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((skz_row == null) || (skz_row.IsRefVPrFXTSNull()) ? (int?)null : ((int?)skz_row.RefVPrFXTS - 1), skz_row.VPrFXTS);
                //        }
                //        else
                //        {
                //            seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((skz_row == null) || (skz_row.IsRefVPrFVTSNull()) ? (int?)null : ((int?)skz_row.RefVPrFVTS - 1), skz_row.VPrFVTS);
                //        }
                //    }
                //}
                //else
                //{
                //    seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((skz_row == null) || (skz_row.IsRelSKzVCNull()) ? (int?)null : (int?)skz_row.RelSKzVC, true);
                //}

                //TaD 1.8.2019 , přechod na FASK_ZASOBY_PARAMETRY

                if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                {
                    seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, skz_row);
                }
                else
                {
                    Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
                    var dt_param = ParamTA.GetDataByITEMNMBR(seRow.ITEMNMBR.Trim());

                    if ((dt_param != null) && (dt_param.Count > 0))
                    {
                        dt_row_param = dt_param.First();
                    }

                    seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, dt_row_param, Pohoda.TypAgendy.Vydej);
                }


                seRow.CZ_SW_Delka = 0; //?
                seRow.CZ_SW_Track = 0; //?
                //seRow.DEX_ROW_ID
                seRow.LOCNCODE = locncodeDeafult;
                seRow.Note = ""; //? poznamka 
                seRow.ORD = int.Parse(vydejkaItem.Element(vyd + "id").Value.Trim()); //? poradi polozky na dokladu - pro pohoda id zaznamu v tabulce polozek <inv:id>
                seRow.PRINTED = 0;
                seRow.PRIORITY = 3; //? priorita ... 
                seRow.QTYPACK = 0; // TODO : rozpad na varianty baleni dle car kodu ... 
                seRow.QTYPAL = 0; //? palety neresime ... ???
                seRow.QTYSHPPD = decimal.Parse(vydejkaItem.Element(vyd + "quantity").Value.Trim(), System.Globalization.NumberFormatInfo.InvariantInfo); // mnozstvi k vydeji => <inv:quantity>
                seRow.SKL_ID = stockItem.Element(typ + "store").Element(typ + "id").Value.Trim(); //? id skladu => <inv:stockItem><typ:store><typ:id> : jde o identifikator skladu
                seRow.SOPNUMBE = objednavka.ID;
                seRow.TYPEPAL = ""; //? neresime palety ...
                seRow.USERID = 0;
                seRow.VNDDOCNM = ""; //? co by tady melo byt >>>
                seRow.VNDITNUM = typEAN == null ? string.Empty : typEAN.Value; //carovy kod zbozi ... => dotahnout z xml 
                if (skz_row != null && !skz_row.IsMJNull())
                {
                    seRow.MJ = skz_row.MJ;
                    if (seRow.MJ.Length > SE_MJ_MaxLength)
                    {
                        seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                    }
                }
                else
                {
                    seRow.MJ = string.Empty;
                }

                // Nacist vychozi lokaci ...
                // az tady, protoze musi mit nastaveny skl_id a itemnmbr...
                LocncodeFindAlgorithmVychozi(seRow);

                //vlozit do se ...
                seTable.AddCZMST_SERow(seRow);

                if (Globals_V1.Konfigurace.Vydej[0].MerneJednotky_DotahovatDalsiVarianty)
                {
                    // MJ2 a qtypack (pokud existuje)
                    // TODO: pridat mernou jednotku
                    if (skz_row != null && !skz_row.IsMJ2Null() && !skz_row.IsMJ2KoefNull())
                    {
                        // kopie zaznamu
                        Pohoda_DataSets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                        seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                        seRow = seRow2;

                        seRow.QTYPACK = (decimal)skz_row.MJ2Koef;
                        seRow.MJ = skz_row.MJ2;
                        if (seRow.MJ.Length > SE_MJ_MaxLength)
                        {
                            seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                        }

                        seTable.AddCZMST_SERow(seRow);
                    }

                    // MJ3 a qtypack (pokud existuje)
                    if (skz_row != null && !skz_row.IsMJ3Null() && !skz_row.IsMJ3KoefNull())
                    {
                        // kopie zaznamu
                        Pohoda_DataSets.Vydej.CZMST_SERow seRow3 = seTable.NewCZMST_SERow();
                        seRow3.ItemArray = seRow.ItemArray.Clone() as object[];
                        seRow = seRow3;

                        seRow.MJ = skz_row.MJ3;
                        seRow.QTYPACK = (decimal)skz_row.MJ3Koef;
                        // TODO: osetrit delku
                        if (seRow.MJ.Length > SE_MJ_MaxLength)
                        {
                            seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                        }

                        seTable.AddCZMST_SERow(seRow);
                    }
                }

                seRow = null;
            }

            try
            {
                // ulozit do se
                CZMST_SETableAdapter.Connection.Open();
                CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                // prideleni cisla davky v transakci ..
                cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries(CZMST_SETableAdapter.Connection, CZMST_SETableAdapter.Transaction);
                cisloDavky += 1;
                foreach (var item in seTable)
                {
                    item.CountEntries = cisloDavky;

                    // porad je pouze pridana, nikoli zmenena po zmene countentries...
                    item.AcceptChanges();
                    item.SetAdded();
                }
                int updatedRows = CZMST_SETableAdapter.Update(seTable);
                CZMST_SETableAdapter.Transaction.Commit();
                objednavka.CisloDavky = cisloDavky.ToString();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                try
                {
                    CZMST_SETableAdapter.Transaction.Rollback();
                }
                catch { }
                throw new Exception("Uložení načtených položek se nezdařilo!");
            }
            finally
            {
                if ((CZMST_SETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    CZMST_SETableAdapter.Connection.Close();
            }

            return "OK";

        }

        #endregion

        #region Prodejka
        /// <summary>
        /// Export dokladu Prodejky (vydejka)
        /// </summary>
        /// <param name="file">nazev souboru kam se request ulozi</param>
        /// <param name="objednavka">id dokladu</param>
        /// <returns>True : ok; False(exception) : nepovedlo se sestaveni nebo ulozeni(False: bez vyjimky, duvod neznamy; Exception: vyjimka)</returns>
        public static bool CreateRequest_Prodejka_XML(string file, Fask.Interfaces.Classes.Objednavka objednavka)
        {
            //
            // ***** VZOR ****
            //
            //<?xml version="1.0" encoding="utf-8" ?>
            //<dat:dataPack
            //  xmlns:dat="http://www.stormware.cz/schema/version_2/data.xsd"
            //  xmlns:lst="http://www.stormware.cz/schema/version_2/list.xsd"
            //  xmlns:typ="http://www.stormware.cz/schema/version_2/type.xsd"
            //  xmlns:ftr="http://www.stormware.cz/schema/version_2/filter.xsd"
            //  xmlns:pro="http://www.stormware.cz/schema/version_2/prodejka.xsd"
            //  version="2.0"
            //  id="ProdejkaExportRequest_01"
            //  ico="63489040"
            //  application="FASK_prodejka_Test_01"
            //  note="Fask prodejka_request_01"
            //>
            //  <dat:dataPackItem id="prodejka_test_request_01" version="2.0">
            //    <lst:listProdejkaRequest prodejkaVersion="2.0" version="2.0" >
            //      <lst:requestProdejka>
            //        <ftr:filter>
            //          <ftr:selectedNumbers>
            //            <ftr:number>
            //              <typ:numberRequested>16PH00001</typ:numberRequested>
            //            </ftr:number>
            //          </ftr:selectedNumbers>
            //        </ftr:filter>
            //      </lst:requestProdejka>
            //    </lst:listProdejkaRequest>
            //  </dat:dataPackItem>
            //</dat:dataPack>

            try
            {
                string filename = Path.Combine(Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory, file);

                List<object> filterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
                XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                XElement ftrSelectedNumbers =
                    new XElement(ftr + "selectedNumbers",
                        new XElement(ftr + "number",
                            new XElement(typ + "numberRequested", objednavka.ID.Trim())
                        )
                    );

                XElement mainFilter = new XElement(ftr + "filter", ftrSelectedNumbers);

                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "lst", "http://www.stormware.cz/schema/version_2/list.xsd"),
                    new XAttribute(XNamespace.Xmlns + "ftr", "http://www.stormware.cz/schema/version_2/filter.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "prodejka"),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_K),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Fask export prodejka"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "prodejkaRequest"),
                            new XAttribute("version", "2.0"),

                                new XElement(lst + "listProdejkaRequest",
                                    new XAttribute("version", "2.0"),
                                    new XAttribute("prodejkaVersion", "2.0"),
                                        new XElement(lst + "requestProdejka", mainFilter)
                                        )));

                root.Save(filename);

                return true;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
                //return false;
            }
        }

        /// <summary>
        /// Nacte data z odpovedi vydejky
        /// </summary>
        /// <param name="filename">cesta ke xml souboru</param>
        public static string LoadResponse_Prodejka_XML(string file, Fask.Interfaces.Classes.Objednavka objednavka)
        {
            Globals_V1.LoadConfiguration();
            string filename = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + "Response\\" + file;

            //Datasety pro komunikaci a zjistovani dat ...
            //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter();
            //SKzTableAdapter.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

            // dotazeni parametru pro locncode (vychozi lokaci ..)
            //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
            //SKzParametryTableAdapter.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

            Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
            CZMST_SETableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            Pohoda_DataSets.Vydej.CZMST_SEDataTable seTable = new Pohoda_DataSets.Vydej.CZMST_SEDataTable();
            Pohoda_DataSets.Vydej.CZMST_SERow seRow = null;

            Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
            if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
            {
                ParamTA = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            }

            // HAVETO : Vice uzivatelsky pristup pri generovani dalsiho cisla davky je problematicky timto zpusobem...
            // Melo by byt reseno nejakym jinym mechanismem !!!
            //int cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries() + 1;
            int cisloDavky = 0;

            //Nacteni odpovedi ... 
            // - pokud neexistuje, tak se vyvola vyjimka ... ??? => mozna radeji nejdrive otestovat?

            if (!File.Exists(filename))
                throw new Exception("Response soubor neexistuje!");

            //Pouzite namespacy
            XNamespace rsp = "http://www.stormware.cz/schema/version_2/response.xsd";
            XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
            XNamespace pro = "http://www.stormware.cz/schema/version_2/prodejka.xsd";
            // nacteni hodnot z odpovedi ...
            XDocument root = XDocument.Load(filename);

            XElement responsePack = root.Element(rsp + "responsePack");
            XAttribute state = responsePack.Attribute("state");
            if (state.Value != "ok")
            {
                XAttribute note = responsePack.Attribute("note");
                throw new Exception("Nepodařilo se získat data prodejky.\nStatus:" + state.Value + "\nChyba:" + note.Value);
            }
            XElement responsePackItem = responsePack.Element(rsp + "responsePackItem");
            XAttribute responsePackItemState = responsePackItem.Attribute("state");
            if (responsePackItemState.Value.Trim() != "ok")
            {
                XAttribute responsePackItemNote = responsePackItem.Attribute("note");
                throw new Exception("Nepodařilo se získat data prodejky.\nStatus:" + responsePackItemState.Value + "\nChyba:" + responsePackItemNote.Value);
            }

            // ok hlavicka je v poradku
            // dal overit vracenou fakturu => ocekavam prave jednu
            XElement listProdejka = responsePackItem.Element(lst + "listProdejka");
            XAttribute listProdejkaState = listProdejka.Attribute("state");
            if (listProdejkaState.Value != "ok")
            {
                // TODO : docilit nejake chyby ...??? => jak je chyba indikovana??? <rdc:details>???
                throw new Exception("Nepodařilo se získat data prodejky.\nStatus:" + state.Value);
            }
            // ok => bez chyby ... fakturu ...
            XElement prodejka = listProdejka.Element(lst + "prodejka");
            // Nemusi obsahovat data => faktura neexistuje, respektive nebyla urcena pro zpracovani??? 
            // => filtrovat dotaz na zaklade nejakeho priznaku nebo uzivatelskeho filtru ???
            if (prodejka == null)
            {
                throw new Exception("Prodejka nebyla nalezena!");
            }
            // ok mam fakturu, tak jdeme na to ...
            // faktura obsahuje:
            // 1) Header (invoiceHeader)
            // => overit cislo dokladu : <inv:number><typ:numberRequested>
            // => text dokladu : <inv:text>
            // => odberatel : <inv:partnerIdentity>
            // 2) Detail polozek... (invoiceDetail)
            // => Pocet polozek dle poctu vyskytu : <inv:invoiceItem>*
            // => muze tam byt textova !!!
            // 3) Paticku (invoiceSummary)
            // => nic zajimaveho ...???

            XElement prodejkaHeader = prodejka.Element(pro + "prodejkaHeader");
            XElement prodejkaDetail = prodejka.Element(pro + "prodejkaDetail");
            XElement prodejkaSummary = prodejka.Element(pro + "prodejkaSummary");

            //1) hlavicka ...
            XElement numberRequested = prodejkaHeader.Element(pro + "number").Element(typ + "numberRequested");
            if (!numberRequested.Value.Trim().Equals(objednavka.ID.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("Číslo požadovaného dokladu se liší!");
            }
            XElement prodejkaText = prodejkaHeader.Element(pro + "text");


            //Definice MaxLength
            int SE_ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
            int SE_MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;


            //2) polozky
            // TODO : prodejkaitem neobsahuje prvek id (ORD) - poradi na dokladu(index?)
            int ord = 0;
            // vsechny polozky
            var prodejkaItems = prodejkaDetail.Elements(pro + "prodejkaItem");
            // pro kazdou polozku provest ulozeni do mst tabulky ...
            // Nejlepe v transakci ...
            foreach (var prodejkaItem in prodejkaItems)
            {
                // Pokud to neni skladova polozka, tak ji neresit ...
                // TODO : otestovat, zda to takto je ... => tedy test na NULL hodnotu...
                XElement stockItem = prodejkaItem.Element(pro + "stockItem");
                if (stockItem == null)
                    continue;

                XElement typEAN = stockItem.Element(typ + "stockItem").Element(typ + "EAN");
                XElement typPLU = stockItem.Element(typ + "stockItem").Element(typ + "PLU");
                XElement typIDS = stockItem.Element(typ + "stockItem").Element(typ + "ids");
                //Pohoda_DataSets.DatabasePohoda.SKzDataTable skz_table = SKzTableAdapter.GetDataByID(int.Parse(stockItem.Element(typ + "stockItem").Element(typ + "id").Value));
                Pohoda_DataSets.DatabasePohoda.SKzDataTable skz_table =  Database.Pohoda.SKz_GetDataByID(int.Parse(stockItem.Element(typ + "stockItem").Element(typ + "id").Value));
                Pohoda_DataSets.DatabasePohoda.SKzRow skz_row = skz_table.Count > 0 ? skz_table[0] : null;

                // dotazeni vychozi lokace pro zasobu
                string locncodeDeafult = string.Empty;
                if (skz_row != null)
                {
                    if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                    {
                        //Pohoda_DataSets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, skz_row.ID);
                        Pohoda_DataSets.DatabasePohoda.SKzParametryDataTable skzParametry_table =  Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, skz_row.ID);
                        Pohoda_DataSets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                        if (skzParametry_row != null)
                        {
                            locncodeDeafult = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                        }
                    }
                }

                seRow = seTable.NewCZMST_SERow();

                seRow.ITEMDESC = prodejkaItem.Element(pro + "text").Value.Trim(); // nazev polozky => z xml <inv:text>
                if (seRow.ITEMDESC.Length > SE_ITEMDESC_MaxLength)
                {
                    seRow.ITEMDESC = seRow.ITEMDESC.Remove(SE_ITEMDESC_MaxLength);
                }

                seRow.ITEMNMBR = stockItem.Element(typ + "stockItem").Element(typ + "id").Value.Trim(); // cislo polozky => z xml <inv:stockItem><typ:stockItem><typ:id>
                seRow.ITEMTYPE = ""; //bez typu
                //seRow.ITEMCODE = ""; // ? ITEMCODE neni???
                seRow.CountEntries = cisloDavky; // TODO : ??? nove cislo davky ... 
                //seRow.CZ_CarKod = typPLU == null ? string.Empty : typPLU.Value; //carovy kod => dotahnout z xml PLU
                seRow.CZ_CarKod = typIDS == null ? string.Empty : typIDS.Value.Trim();
                seRow.CZ_DatVyr_Delka = 0; //?
                seRow.CZ_DatVyr_Track = 0; //?
                seRow.CZ_Doslo = 255; // pripraveno pro zpracovani
                seRow.CZ_SerNum_Delka = 0; //? delka serioveho cisla 
                //seRow.CZ_SerNum_Track = 0; //? sledovat na SN/Sarze => je sledovano na seriove cislo => zjistit v Pohoda
                //seRow.CZ_SerNum_Track = (byte)(skz_row == null ? 0 : skz_row.RelSKzVC);


                //seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack( (skz_row == null) || (skz_row.IsRelSKzVCNull()) ? (int?)null : (int?)skz_row.RelSKzVC,true );

                //1.8.2019 TaD prechod na FASK_ZASOBY_PARAMETRY
                if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                {
                    seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, skz_row);
                }
                else
                {
                    Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
                    var dt_param = ParamTA.GetDataByITEMNMBR(seRow.ITEMNMBR.Trim());

                    if ((dt_param != null) && (dt_param.Count > 0))
                    {
                        dt_row_param = dt_param.First();
                    }

                    seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, dt_row_param, Pohoda.TypAgendy.Vydej);
                }


                
                seRow.CZ_SW_Delka = 0; //?
                seRow.CZ_SW_Track = 0; //?
                //seRow.DEX_ROW_ID
                seRow.LOCNCODE = locncodeDeafult;
                seRow.Note = ""; //? poznamka 
                // TODO : sledovat, zda se nekdy tento prvek dostavi do vystupu xml ...
                //seRow.ORD = int.Parse(prodejkaItem.Element(pro + "id").Value.Trim()); //? poradi polozky na dokladu - pro pohoda id zaznamu v tabulce polozek <inv:id>
                seRow.ORD = --ord;
                seRow.PRINTED = 0;
                seRow.PRIORITY = 3; //? priorita ... 
                seRow.QTYPACK = 0; // TODO : rozpad na varianty baleni dle car kodu ... 
                seRow.QTYPAL = 0; //? palety neresime ... ???
                seRow.QTYSHPPD = decimal.Parse(prodejkaItem.Element(pro + "quantity").Value.Trim(), System.Globalization.NumberFormatInfo.InvariantInfo); // mnozstvi k vydeji => <inv:quantity>
                seRow.SKL_ID = stockItem.Element(typ + "store").Element(typ + "id").Value.Trim(); //? id skladu => <inv:stockItem><typ:store><typ:id> : jde o identifikator skladu
                seRow.SOPNUMBE = objednavka.ID;
                seRow.TYPEPAL = ""; //? neresime palety ...
                seRow.USERID = 0;
                seRow.VNDDOCNM = ""; //? co by tady melo byt >>>
                seRow.VNDITNUM = typEAN == null ? string.Empty : typEAN.Value; //carovy kod zbozi ... => dotahnout z xml 
                if (skz_row != null && !skz_row.IsMJNull())
                {
                    seRow.MJ = skz_row.MJ;
                    if (seRow.MJ.Length > SE_MJ_MaxLength)
                    {
                        seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                    }
                }
                else
                {
                    seRow.MJ = string.Empty;
                }

                // Nacist vychozi lokaci ...
                // az tady, protoze musi mit nastaveny skl_id a itemnmbr...
                LocncodeFindAlgorithmVychozi(seRow);

                //vlozit do se ...
                seTable.AddCZMST_SERow(seRow);

                if (Globals_V1.Konfigurace.Vydej[0].MerneJednotky_DotahovatDalsiVarianty)
                {
                    // MJ2 a qtypack (pokud existuje)
                    // TODO: pridat mernou jednotku
                    if (skz_row != null && !skz_row.IsMJ2Null() && !skz_row.IsMJ2KoefNull())
                    {
                        // kopie zaznamu
                        Pohoda_DataSets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                        seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                        seRow = seRow2;

                        seRow.QTYPACK = (decimal)skz_row.MJ2Koef;
                        seRow.MJ = skz_row.MJ2;
                        if (seRow.MJ.Length > SE_MJ_MaxLength)
                        {
                            seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                        }

                        seTable.AddCZMST_SERow(seRow);
                    }

                    // MJ3 a qtypack (pokud existuje)
                    if (skz_row != null && !skz_row.IsMJ3Null() && !skz_row.IsMJ3KoefNull())
                    {
                        // kopie zaznamu
                        Pohoda_DataSets.Vydej.CZMST_SERow seRow3 = seTable.NewCZMST_SERow();
                        seRow3.ItemArray = seRow.ItemArray.Clone() as object[];
                        seRow = seRow3;

                        seRow.MJ = skz_row.MJ3;
                        seRow.QTYPACK = (decimal)skz_row.MJ3Koef;
                        // TODO: osetrit delku
                        if (seRow.MJ.Length > SE_MJ_MaxLength)
                        {
                            seRow.MJ = seRow.MJ.Remove(SE_MJ_MaxLength);
                        }

                        seTable.AddCZMST_SERow(seRow);
                    }
                }

                seRow = null;
            }

            try
            {
                // ulozit do se
                CZMST_SETableAdapter.Connection.Open();
                CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                // prideleni cisla davky v transakci ..
                cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries(CZMST_SETableAdapter.Connection, CZMST_SETableAdapter.Transaction);
                cisloDavky += 1;
                foreach (var item in seTable)
                {
                    item.CountEntries = cisloDavky;

                    // porad je pouze pridana, nikoli zmenena po zmene countentries...
                    item.AcceptChanges();
                    item.SetAdded();
                }
                int updatedRows = CZMST_SETableAdapter.Update(seTable);
                CZMST_SETableAdapter.Transaction.Commit();
                objednavka.CisloDavky = cisloDavky.ToString();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                try
                {
                    CZMST_SETableAdapter.Transaction.Rollback();
                }
                catch { }
                throw new Exception("Uložení načtených položek se nezdařilo!");
            }
            finally
            {
                if ((CZMST_SETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    CZMST_SETableAdapter.Connection.Close();
            }

            return "OK";

        }

        #endregion

        #region PrjateObjednavky

        internal static bool CreateRequest_PrjateObjednavky_XML(string file,Fask.Interfaces.Classes.Objednavka objednavka)
        {

            try
            {
                string filename = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + file;

                List<object> filterList = new List<object>();
                //List<object> mainfilterList = new List<object>();


                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
                XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                //if (lastChanges != string.Empty)
                //    filterList.Add(new XElement(ftr + "lastChanges", lastChanges));

                //if (dateFrom != string.Empty && dateTill != string.Empty)
                //{
                //    filterList.Add(new XElement(ftr + "dateFrom", dateFrom));
                //    filterList.Add(new XElement(ftr + "dateTill", dateTill));
                //}

                //List<object> filterCompanyList = new List<object>();
                //if (companys.Count > 0)
                //{
                //    for (int i = 0; i < companys.Count; i++)
                //    {
                //        filterCompanyList.Add(new XElement(ftr + "company", companys[i]));
                //    }

                //    filterList.Add(new XElement(ftr + "selectedCompanys", filterCompanyList.ToArray()));
                //}

                //List<object> filterIcoList = new List<object>();
                //if (icos.Count > 0)
                //{
                //    for (int i = 0; i < icos.Count; i++)
                //    {
                //        filterIcoList.Add(new XElement(ftr + "ico", icos[i]));
                //    }

                //    filterList.Add(new XElement(ftr + "selectedIco", filterIcoList.ToArray()));
                //}

                //List<object> filterCisloDokladuList = new List<object>();
                //if (cislaDokladu.Count > 0)
                //{
                //    for (int i = 0; i < cislaDokladu.Count; i++)
                //    {
                //        filterCisloDokladuList.Add(new XElement(ftr + "number",
                //           (new XElement(typ + "numberRequested", cislaDokladu[i]))));
                //    }

                //    filterList.Add(new XElement(ftr + "selectedNumbers", filterCisloDokladuList.ToArray()));
                //}


                XElement ftrSelectedNumbers =
                    new XElement(ftr + "selectedNumbers",
                        new XElement(ftr + "number",
                            new XElement(typ + "numberRequested", objednavka.ID.Trim())
                        )
                    );

                XElement mainFilter = new XElement(ftr + "filter", ftrSelectedNumbers);



                //object[] filter = null;
                //object mainFilter = null;

                //filter = filterList.ToArray();

                //if (userFilterName != string.Empty)
                //{
                //    mainFilter = new XElement(ftr + "userFilterName", userFilterName);
                //}
                //else
                //    mainFilter = new XElement(ftr + "filter", filter);

                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "ftr", "http://www.stormware.cz/schema/version_2/filter.xsd"),
                    new XAttribute(XNamespace.Xmlns + "lst", "http://www.stormware.cz/schema/version_2/list.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "001"),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_K),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Export s konzole"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "li1"),
                            new XAttribute("version", "2.0"),

                                new XElement(lst + "listOrderRequest",
                                    new XAttribute("version", "2.0"),
                                    new XAttribute("orderType", "receivedOrder"),  // Prijata objednavka
                                    new XAttribute("orderVersion", "2.0"),

                                        new XElement(lst + "requestOrder",
                                               mainFilter
                                        )
                                )
                        )

                );

                root.Save(filename);

                return true;

            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }


        }

        internal static string LoadResponse_PrjateObjednavky_XML(string file, bool save, Fask.Interfaces.Classes.Objednavka objednavka, int? CountEntries)
        {
            Globals_V1.LoadConfiguration();
            string filename = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + "Response\\" + file;

            string actualStrName = string.Empty;
            string previousStrName = string.Empty;
            int actualCountEntries = 0;
            bool InVydejkItem = false;
            int indexPriID = 0;
            int indexTypID = 0;
            int indexTypIDS = 0;
            decimal quantity = 0;
            decimal delivered = 0;
            string mj = string.Empty;
            string actualSonnumber = string.Empty;
            string actualNumberorder = string.Empty;
            bool isExecuted = false;
            bool check = false;
            bool isDelivered = false;
            bool incCountEntr = true;

            // dotazeni informace o polozce z pohoda
            //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter();
            //SKzTableAdapter.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);// .ConnectionString = Globals.ConnectionStringPohodaDB;

            // dotazeni parametru pro locncode (vychozi lokaci ..)
            //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
            //SKzParametryTableAdapter.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

            Pohoda_DataSets.Vydej VydejDS = new Pohoda_DataSets.Vydej();

            Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter();
            CZMST_SETableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //Pohoda_DataSets.Zbozi.FASK_ZASOBYRow fask_ZASOBYRow = null;
           // Pohoda_DataSets.DatabasePohoda.SKzRow skzRow = null;
            Pohoda_DataSets.Vydej.CZMST_SERow seRow = null;

            //seRow.ITEMTYPE = "J";

            Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
            if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
            {
                ParamTA = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            }

            XmlTextReader reader = new XmlTextReader(filename);



            try
            {

                int SE_SOPNUMBE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["SOPNUMBE"].MaxLength;
                int SE_SKL_ID_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["SKL_ID"].MaxLength;
                int SE_VNDITNUM_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["VNDITNUM"].MaxLength;
                int SE_ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
                int SE_CZ_CarKod_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["CZ_CarKod"].MaxLength;
                
                int SE_ITEMCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMCODE"].MaxLength;

                seRow = VydejDS.CZMST_SE.NewCZMST_SERow();

                NastavPromenne(seRow);

                CZMST_SETableAdapter.Connection.Open();

                if ((CountEntries != null) && (CountEntries.HasValue))
                {
                    actualCountEntries = CountEntries.Value;
                }
                else 
                {
                    CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                    actualCountEntries = Database.Vydej.CZMSTSE_MAX_CountEntries(CZMST_SETableAdapter.Connection, CZMST_SETableAdapter.Transaction);

                    //objednavka.CisloDavky = actualCountEntries;
                    CZMST_SETableAdapter.Transaction.Commit();
                }

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            previousStrName = actualStrName;
                            actualStrName = reader.Name;

                            if (actualStrName == "ord:orderItem")
                            {
                                InVydejkItem = true;
                                if (seRow.ITEMDESC != string.Empty)
                                { //jsme u dalsiho zbozi na vydejce...ulozime aktulani zbozi 

                                    seRow.CountEntries = actualCountEntries;
                                    seRow.SOPNUMBE = actualSonnumber;
                                    seRow.VNDDOCNM = actualNumberorder;
                                    
                                    
                                    //MaR 13.12.2024 pridany zapisy z POHODA promennych
                                    seRow.ITEMTYPE = "J";
                                    seRow.MJ = mj;



                                    if (seRow.SOPNUMBE.Length > SE_SOPNUMBE_MaxLength)
                                    {
                                        seRow.SOPNUMBE = seRow.SOPNUMBE.Remove(SE_SOPNUMBE_MaxLength);
                                    }

                                    if (Database.Vydej.CZMSTSE_EXIST_SOPNUMBE(seRow.SOPNUMBE) && !check)
                                    { // toto cislo davky se uz v db vyskytuje -> smazeme jej
                                        check = true;

                                        byte? czdoslotmp = Database.Vydej.CZMSTSE_CZDOSLO_SOPNUMBE_InUse(seRow.SOPNUMBE);

                                        if (czdoslotmp != null)
                                        {
                                            string message = string.Empty;

                                            message = string.Format("Nelze generovat doklad:'{0}'" + Environment.NewLine , seRow.SOPNUMBE);


                                            if (czdoslotmp == 0)
                                                message += "Je již vygenerován a uvolnen";

                                            if ((czdoslotmp > 0) || (czdoslotmp < 100))
                                                message += "Je již vygenerován a uvolnen";

                                            //if ((czdoslotmp > 100) || (czdoslotmp < 200))
                                            //    message += "uspešne spracovana";


                                            return message;
                                        }


                                        bool test = Database.Vydej.CZMSTSE_UPDATE_CZDOSLO(seRow.SOPNUMBE);
                                        if (test)
                                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "vydej - proveden update cz_doslo na 201 u objednavky=" + seRow.SOPNUMBE);
                                        else
                                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "vydej - nebyl proveden update cz_doslo na 201 u objednavky=" + seRow.SOPNUMBE);
                                    }
                                    else
                                        check = true;

                                    //if (!Database.Prijem.CZMSTPE_EXIST_PONNUMBER(seRow.SOPNUMBE, seRow.ITEMNMBR, seRow.ORD))
                                    //{
                                    if (save)
                                    {
                                        SaveData(ref quantity, delivered, ref incCountEntr, CZMST_SETableAdapter, seRow);
                                    }
                                    else if (Globals_V1.Konfigurace.Vydej[0].StatusObjednavky)
                                    {
                                        if ((Globals_V1.Konfigurace.Vydej[0].Executed == isExecuted) && (Globals_V1.Konfigurace.Vydej[0].Delivered == isDelivered))
                                        {
                                            SaveData(ref quantity, delivered, ref incCountEntr, CZMST_SETableAdapter, seRow);
                                        }

                                    }
                                    else
                                    {
                                        SaveData(ref quantity, delivered, ref incCountEntr, CZMST_SETableAdapter, seRow);
                                    }
                                    /*}
                                    else
                                    {
                                        incCountEntr = false;
                                        //pokud uz objednavka je v tabulce, zjistime jeji countentries
                                        int pom = Database.Prijem.CZMSTPE_CountEntries_SOPNUMBE(seRow.SOPNUMBE);

                                        if (pom >= 0)
                                            actualCountEntries = pom; 

                                    }*/

                                    seRow = VydejDS.CZMST_SE.NewCZMST_SERow();

                                    NastavPromenne(seRow);
                                    InVydejkItem = true;
                                    indexTypID = 0;
                                    indexTypIDS = 0;
                                }

                            }
                            else if (actualStrName == "ord:orderSummary")
                            {
                                if (seRow.ITEMDESC != string.Empty)
                                { //jsme u dalsiho zbozi na prijemce...ulozime aktulani zbozi 

                                    seRow.CountEntries = actualCountEntries;
                                    seRow.SOPNUMBE = actualSonnumber;
                                    seRow.VNDDOCNM = actualNumberorder;

                                    //MaR 13.12.2024 pridany zapisy z POHODA promennych
                                    seRow.ITEMTYPE = "J";
                                    seRow.MJ = mj;



                                    if (seRow.SOPNUMBE.Length > SE_SOPNUMBE_MaxLength)
                                    {
                                        seRow.SOPNUMBE = seRow.SOPNUMBE.Remove(SE_SOPNUMBE_MaxLength);
                                    }

                                    if (Database.Vydej.CZMSTSE_EXIST_SOPNUMBE(seRow.SOPNUMBE) && !check)
                                    { // toto cislo davky se uz v db vyskytuje -> smazeme jej
                                        check = true;

                                        byte? czdoslotmp = Database.Vydej.CZMSTSE_CZDOSLO_SOPNUMBE_InUse(seRow.SOPNUMBE);

                                        if (czdoslotmp != null)
                                        {
                                            string message = string.Empty;

                                            message = string.Format("Nelze generovat doklad:'{0}'" + Environment.NewLine, seRow.SOPNUMBE);


                                            if (czdoslotmp == 0)
                                                message += "Je již vygenerován a uvolnen";

                                            if ((czdoslotmp > 0) || (czdoslotmp < 100))
                                                message += "Je již vygenerován a uvolnen";

                                            //if ((czdoslotmp > 100) || (czdoslotmp < 200))
                                            //    message += "uspešne spracovana";


                                            return message;
                                            
                                        }

                                        //{
                                        //    check = true;
                                        //}


                                        bool test = Database.Vydej.CZMSTSE_UPDATE_CZDOSLO(seRow.SOPNUMBE);
                                        if (test)
                                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info,"vydej - proveden update cz_doslo na 201 u objednavky=" + seRow.SOPNUMBE);
                                        else
                                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "vydej - nebyl proveden update cz_doslo na 201 u objednavky=" + seRow.SOPNUMBE);
                                    }
                                    else
                                        check = true;

                                    //if (!Database.Prijem.CZMSTPE_EXIST_PONNUMBER(seRow.SOPNUMBE, seRow.ITEMNMBR, seRow.ORD))
                                    //{
                                    if (save)
                                    {
                                        SaveData(ref quantity, delivered, ref incCountEntr, CZMST_SETableAdapter, seRow);

                                    }
                                    else if (Globals_V1.Konfigurace.Vydej[0].StatusObjednavky)
                                    {
                                        if ((Globals_V1.Konfigurace.Vydej[0].Executed == isExecuted) && (Globals_V1.Konfigurace.Vydej[0].Delivered == isDelivered))
                                        {
                                            SaveData(ref quantity, delivered, ref incCountEntr, CZMST_SETableAdapter, seRow);
                                        }
                                    }
                                    else
                                    {
                                        SaveData(ref quantity, delivered, ref incCountEntr, CZMST_SETableAdapter, seRow);
                                    }
                                    /*}
                                    else
                                    {
                                        incCountEntr = false;
                                        int pom = Database.Prijem.CZMSTPE_CountEntries_SOPNUMBE(seRow.SOPNUMBE);

                                        if (pom >= 0)
                                            actualCountEntries = pom; 
                                    }*/

                                    seRow = VydejDS.CZMST_SE.NewCZMST_SERow();

                                    NastavPromenne(seRow);
                                    InVydejkItem = false;
                                    indexTypID = 0;
                                    indexTypIDS = 0;
                                }
                            }
                            else if (actualStrName == "lst:order" && seRow.ITEMDESC == string.Empty)
                            {
                                indexTypIDS = indexTypID = indexPriID = 0;

                                if (incCountEntr)
                                {

                                    if ((CountEntries == null))
                                        actualCountEntries++;

                                    objednavka.CisloDavky = actualCountEntries.ToString();
                                }

                                isExecuted = false;
                                isDelivered = false;
                            }
                            break;

                        case XmlNodeType.Text:

                            if (actualStrName == "typ:id")
                            {
                                if (InVydejkItem)
                                {
                                    if (indexTypID == 0)
                                    {
                                        seRow.SKL_ID = reader.Value;

                                        

                                        if (seRow.SKL_ID.Length > SE_SKL_ID_MaxLength)
                                        {
                                            seRow.SKL_ID = seRow.SKL_ID.Remove(SE_SKL_ID_MaxLength);
                                        }


                                        //seRow.LOCNCODE = reader.Value;
                                        //if (seRow.LOCNCODE.Length > VydejDS.CZMST_SE.LOCNCODEColumn.MaxLength)
                                        //{
                                        //    seRow.LOCNCODE = seRow.LOCNCODE.Remove(VydejDS.CZMST_SE.LOCNCODEColumn.MaxLength);
                                        //} 
                                    }

                                    if (indexTypID == 1)
                                    {
                                        seRow.ITEMNMBR = reader.Value;

                                        // TOTO se nesmi delat, jinak muze dojit k problemum s identifikaci...
                                        // pokud presahne delku, tak je problem !!!
                                        //if (seRow.ITEMNMBR.Length > VydejDS.CZMST_SE.ITEMNMBRColumn.MaxLength)
                                        //{
                                        //    seRow.ITEMNMBR = seRow.ITEMNMBR.Remove(VydejDS.CZMST_SE.ITEMNMBRColumn.MaxLength);
                                        //}

                                        // dotazeni vychozi lokace pro zasobu
                                        string locncodedefault = string.Empty;
                                        if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                                        {
                                            //Pohoda_DataSets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, int.Parse(seRow.ITEMNMBR));
                                            Pohoda_DataSets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, int.Parse(seRow.ITEMNMBR));
                                            Pohoda_DataSets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                                            if (skzParametry_row != null)
                                            {
                                                locncodedefault = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                                            }
                                        }
                                        seRow.LOCNCODE = locncodedefault;



                                        var skzdt = Database.Pohoda.SKz_GetDataByID(int.Parse(seRow.ITEMNMBR));

                                        if ((skzdt != null) && (skzdt.Count > 0))
                                        {
                                            var skz_row = skzdt[0];

                                            //TaD 1.8.2019 , přechod na FASK_ZASOBY_PARAMETRY
                                            
                                            if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                                            {

                                                seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, skz_row);
                                            }
                                            else
                                            {
                                                Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
                                                var dt_param = ParamTA.GetDataByITEMNMBR(seRow.ITEMNMBR.Trim());

                                                if ((dt_param != null) && (dt_param.Count > 0))
                                                {
                                                    dt_row_param = dt_param.First();
                                                }

                                                seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, dt_row_param, Pohoda.TypAgendy.Vydej);
                                            }
                                        }
                                        else
                                        {
                                            seRow.CZ_SerNum_Track = 0;
                                        }

                                 


                                    }

                                    indexTypID++;
                                }
                            }
                            if (actualStrName == "ord:id")
                            {
                                if (InVydejkItem)
                                    seRow.ORD = Convert.ToInt32(reader.Value);

                                indexPriID++;
                            }
                            else if (actualStrName == "typ:numberRequested")
                            {
                                check = false;
                                actualSonnumber = reader.Value;
                            }
                            else if (actualStrName == "typ:EAN" /*|| actualStrName == "typ:PLU"*/)
                            {
                                if (seRow.VNDITNUM == string.Empty)
                                {
                                    seRow.VNDITNUM = reader.Value;

                                    if (seRow.VNDITNUM.Length > SE_VNDITNUM_MaxLength)
                                    {
                                        seRow.VNDITNUM = seRow.VNDITNUM.Remove(SE_VNDITNUM_MaxLength);
                                    }
                                }
                                /*
                                else
                                {
                                    seRow.CZ_CarKod = reader.Value;

                                    if (seRow.CZ_CarKod.Length > VydejDS.CZMST_SE.CZ_CarKodColumn.MaxLength)
                                    {
                                        seRow.CZ_CarKod = seRow.CZ_CarKod.Remove(VydejDS.CZMST_SE.CZ_CarKodColumn.MaxLength);
                                    }
                                }*/
                            }
                            else if (actualStrName == "typ:ids")
                            {
                                if (InVydejkItem)
                                {
                                    if (indexTypIDS == 1)
                                    {
                                        seRow.CZ_CarKod = reader.Value;

                                        
                                        if (seRow.CZ_CarKod.Length > SE_CZ_CarKod_MaxLength)
                                        {
                                            seRow.CZ_CarKod = seRow.CZ_CarKod.Remove(SE_CZ_CarKod_MaxLength);
                                        }
                                    }
                                    indexTypIDS++;
                                }
                            }
                            else if (actualStrName == "ord:isExecuted")
                                isExecuted = Convert.ToBoolean(reader.Value);
                            else if (actualStrName == "ord:isDelivered")
                                isDelivered = Convert.ToBoolean(reader.Value);
                            else if (actualStrName == "ord:text")
                            {
                                if (InVydejkItem)
                                    seRow.ITEMDESC = reader.Value;
                                
                                if (seRow.ITEMDESC.Length > SE_ITEMDESC_MaxLength)
                                {
                                    seRow.ITEMDESC = seRow.ITEMDESC.Remove(SE_ITEMDESC_MaxLength);
                                }

                                //if (seRow.ITEMDESC.Length > VydejDS.CZMST_SE.ITEMDESCColumn.MaxLength)
                                //{
                                //    seRow.ITEMDESC = seRow.ITEMDESC.Remove(VydejDS.CZMST_SE.ITEMDESCColumn.MaxLength);
                                //}

                            }
                            else if (actualStrName == "ord:code")
                            {
                                if (InVydejkItem)
                                    seRow.ITEMCODE = reader.Value;

                                if (seRow.ITEMCODE.Length > SE_ITEMCODE_MaxLength)
                                {
                                    seRow.ITEMCODE = seRow.ITEMCODE.Remove(SE_ITEMCODE_MaxLength);
                                }

                            }
                            else if (actualStrName == "typ:PLU")
                            {
                                //seRow.ORD = Convert.ToInt32(reader.Value);
                                //seRow.VNDDOCNM = reader.Value;
                            }
                            else if (actualStrName == "ord:numberOrder")
                            {
                                actualNumberorder = reader.Value;
                            }
                            else if (actualStrName == "ord:unit")
                                mj = reader.Value.Trim();
                            else if (actualStrName == "ord:coefficient")
                                seRow.QTYPACK = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo); //System.Globalization.CultureInfo.InvariantCulture);
                            else if (actualStrName == "ord:quantity")
                                quantity = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                            else if (actualStrName == "ord:delivered")
                                delivered = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);

                            break;

                        default:
                            break;
                    }
                }

                //dotazeni alternativnich kodu k polozkam objednavky ... 
                // predpoklad, ze je pouze jedna objednavka/prijemka vygenerovana ...
                //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzNCTableAdapter skznc_ta = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzNCTableAdapter();
                //skznc_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

                Pohoda_DataSets.Vydej vydej_ds = new Pohoda_DataSets.Vydej();
                Pohoda_DataSets.Vydej vydej_ds_added = new Pohoda_DataSets.Vydej();
                try
                {
                    CZMST_SETableAdapter.FillByCountEntries(VydejDS.CZMST_SE, int.Parse(objednavka.CisloDavky));
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    Fask.Logging.ExceptionHandler2.Handle(VydejDS.CZMST_SE);
                }
                // TODO : kolekce byla zmenena => musim udelat do noveho a ten pak updatnout ... 
                foreach (Pohoda_DataSets.Vydej.CZMST_SERow serow in VydejDS.CZMST_SE)
                {
                    Pohoda_DataSets.DatabasePohoda.SKzNCDataTable skznc_dt = Database.Pohoda.SKzNC_GetDataSKzID(int.Parse(serow.ITEMNMBR));
                    foreach (Pohoda_DataSets.DatabasePohoda.SKzNCRow skzncrow in skznc_dt)
                    {
                      
                        //11.12.2024 pridano
                       // serow.MJ = mj;

                        vydej_ds_added.CZMST_SE.AddCZMST_SERow(
                            serow.CountEntries,
                            serow.SOPNUMBE,
                            serow.ITEMNMBR,
                            serow.ITEMTYPE,
                            serow.ITEMDESC,
                            serow.VNDDOCNM,
                            skzncrow.EAN,
                            serow.ORD,
                            serow.CZ_CarKod,
                            serow.IsSKL_IDNull() ? string.Empty : serow.SKL_ID,
                            serow.LOCNCODE,
                            skzncrow.MJEAN,
                            serow.QTYSHPPD,
                            serow.QTYPACK,
                            serow.CZ_DatVyr_Track,
                            serow.CZ_DatVyr_Delka,
                            serow.CZ_SerNum_Track,
                            serow.CZ_SerNum_Delka,
                            serow.CZ_SW_Track,
                            serow.CZ_SW_Delka,
                            serow.CZ_Doslo,
                            serow.Note,
                            serow.TYPEPAL,
                            serow.QTYPAL,
                            serow.PRIORITY,
                            serow.PRINTED,
                            serow.USERID,
                            serow.IsCZ_REZ1_TrackNull() ? (byte)0 : serow.CZ_REZ1_Track,
                            serow.IsCZ_REZ2_TrackNull() ? (byte)0 : serow.CZ_REZ2_Track,
                            serow.IsITEMCODENull() ? string.Empty : serow.ITEMCODE,
                            serow.IsWEIGHTNull() ? 1 : serow.WEIGHT
                            );
                    }
                }

                CZMST_SETableAdapter.Update(vydej_ds_added.CZMST_SE);

                return "OK";
            }
            catch (XmlException e)
            {
                Fask.Logging.ExceptionHandler2.Handle(e);
                return e.Message;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return ex.Message;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        #endregion

        #endregion


        private static void SaveData(
    ref decimal quantity,
    decimal delivered,
    ref bool incCountEntr,
    Pohoda_DataSets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter,
    Pohoda_DataSets.Vydej.CZMST_SERow seRow
    //,Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter
            )
        {

            Globals_V1.LoadConfiguration();

            if (Globals_V1.Konfigurace.Vydej[0].Zbyva)
                quantity -= delivered;

            if ((Globals_V1.Konfigurace.Vydej[0].GenerovatPrenesenePolozky) && (quantity <= 0))
                return;

            // tady se uklada neco z xml exportu ... 
            // vzhledem k Mernym jednotkam, dotazeni variant ... 
            if (seRow.IsITEMNMBRNull() || string.IsNullOrEmpty(seRow.ITEMNMBR.Trim()))
                return;

            // Pred ulozenim nacte vychozi lokaci pro prijem ...
            LocncodeFindAlgorithmVychozi(seRow);



            //Pohoda_DataSets.DatabasePohoda.SKzDataTable dt_skz = SKzTableAdapter.GetDataByID(int.Parse(seRow.ITEMNMBR));
            Pohoda_DataSets.DatabasePohoda.SKzDataTable dt_skz = Database.Pohoda.SKz_GetDataByID(int.Parse(seRow.ITEMNMBR));

            if (dt_skz.Count > 0)
            { // pro kazdou variantu MJ vlozit alternativni MJ ... 
                var item = dt_skz[0];



                if (Globals_V1.Konfigurace.Vydej[0].Predloha_Generovat_PodleTypuPolozky)
                {
                    if (!item.IsRelSkTypNull())
                    {
                        //item.RelSkTyp
                        string[] stringArray = Globals_V1.Konfigurace.Vydej[0].Predloha_Generovat_PodleTypuPolozky_Seznam.Split(',');
                       var listINT = stringArray.Select(x => Int32.Parse(x)).ToList();

                        if (!listINT.Contains(item.RelSkTyp))
                        {
                            return;
                        }
                    }
                }


                //foreach (var item in dt_skz)
                {
                    #region pro MJ

                    CZMST_SETableAdapter.Insert(
                seRow.CountEntries,
                seRow.SOPNUMBE,
                seRow.ITEMNMBR,
                seRow.ITEMTYPE,
                seRow.ITEMDESC,
                seRow.VNDDOCNM,
                seRow.VNDITNUM,
                seRow.ORD,
                seRow.CZ_CarKod,
                seRow.IsSKL_IDNull() ? string.Empty : seRow.SKL_ID,
                seRow.LOCNCODE,
                item.MJ,
                quantity,
                0, //peRow.QTYPACK,
                seRow.CZ_DatVyr_Track,
                seRow.CZ_DatVyr_Delka,
                seRow.CZ_SerNum_Track,
                seRow.CZ_SerNum_Delka,
                seRow.CZ_SW_Track,
                seRow.CZ_SW_Delka,
                seRow.CZ_Doslo,
                seRow.Note,
                seRow.TYPEPAL,
                seRow.QTYPAL,
                seRow.PRIORITY,
                seRow.PRINTED,
                seRow.IsUSERIDNull() ? (int?)null : seRow.USERID,
                seRow.CZ_REZ1_Track,
                seRow.CZ_REZ2_Track,
                seRow.IsITEMCODENull() ? string.Empty : seRow.ITEMCODE,
                item.IsHmotnostNull() ? 0 : (decimal)item.Hmotnost
                ); 

                    #endregion

                    if (Globals_V1.Konfigurace.Vydej[0].MerneJednotky_DotahovatDalsiVarianty)
                    {
                        #region pro MJ2
                        if (!item.IsMJ2Null() && !item.IsMJ2KoefNull())
                        {
                            CZMST_SETableAdapter.Insert(
                                seRow.CountEntries,
                        seRow.SOPNUMBE,
                        seRow.ITEMNMBR,
                        seRow.ITEMTYPE,
                        seRow.ITEMDESC,
                        seRow.VNDDOCNM,
                        seRow.VNDITNUM,
                        seRow.ORD,
                        seRow.CZ_CarKod,
                        seRow.IsSKL_IDNull() ? string.Empty : seRow.SKL_ID,
                        seRow.LOCNCODE,
                        item.MJ2Koef.ToString(),
                        quantity,
                        0, //peRow.QTYPACK,
                        seRow.CZ_DatVyr_Track,
                        seRow.CZ_DatVyr_Delka,
                        seRow.CZ_SerNum_Track,
                        seRow.CZ_SerNum_Delka,
                        seRow.CZ_SW_Track,
                        seRow.CZ_SW_Delka,
                        seRow.CZ_Doslo,
                        seRow.Note,
                        seRow.TYPEPAL,
                        seRow.QTYPAL,
                        seRow.PRIORITY,
                        seRow.PRINTED,
                        seRow.IsUSERIDNull() ? (int?)null : seRow.USERID,
                        seRow.CZ_REZ1_Track,
                        seRow.CZ_REZ2_Track,
                        seRow.IsITEMCODENull() ? string.Empty : seRow.ITEMCODE,
                        item.IsHmotnostNull() ? 0 : (decimal)item.Hmotnost
                                );
                        }
                        #endregion

                        #region pro MJ3
                        if (!item.IsMJ3Null() && !item.IsMJ3KoefNull())
                        {
                            CZMST_SETableAdapter.Insert(
                                seRow.CountEntries,
                        seRow.SOPNUMBE,
                        seRow.ITEMNMBR,
                        seRow.ITEMTYPE,
                        seRow.ITEMDESC,
                        seRow.VNDDOCNM,
                        seRow.VNDITNUM,
                        seRow.ORD,
                        seRow.CZ_CarKod,
                        seRow.IsSKL_IDNull() ? string.Empty : seRow.SKL_ID,
                        seRow.LOCNCODE,
                        item.MJ3Koef.ToString(),
                        quantity,
                        0, //peRow.QTYPACK,
                        seRow.CZ_DatVyr_Track,
                        seRow.CZ_DatVyr_Delka,
                        seRow.CZ_SerNum_Track,
                        seRow.CZ_SerNum_Delka,
                        seRow.CZ_SW_Track,
                        seRow.CZ_SW_Delka,
                        seRow.CZ_Doslo,
                        seRow.Note,
                        seRow.TYPEPAL,
                        seRow.QTYPAL,
                        seRow.PRIORITY,
                        seRow.PRINTED,
                        seRow.IsUSERIDNull() ? (int?)null : seRow.USERID,
                        seRow.CZ_REZ1_Track,
                        seRow.CZ_REZ2_Track,
                        seRow.IsITEMCODENull() ? string.Empty : seRow.ITEMCODE,
                        item.IsHmotnostNull() ? 0 : (decimal)item.Hmotnost
                                );
                        } 
                        #endregion
                    }
                }
            }
            else
            {
                CZMST_SETableAdapter.Insert(
                        seRow.CountEntries,
                        seRow.SOPNUMBE,
                        seRow.ITEMNMBR,
                        seRow.ITEMTYPE,
                        seRow.ITEMDESC,
                        seRow.VNDDOCNM,
                        seRow.VNDITNUM,
                        seRow.ORD,
                        seRow.CZ_CarKod,
                        seRow.IsSKL_IDNull() ? string.Empty : seRow.SKL_ID,
                        seRow.LOCNCODE,
                        seRow.MJ,
                        quantity,
                        0, //peRow.QTYPACK,
                        seRow.CZ_DatVyr_Track,
                        seRow.CZ_DatVyr_Delka,
                        seRow.CZ_SerNum_Track,
                        seRow.CZ_SerNum_Delka,
                        seRow.CZ_SW_Track,
                        seRow.CZ_SW_Delka,
                        seRow.CZ_Doslo,
                        seRow.Note,
                        seRow.TYPEPAL,
                        seRow.QTYPAL,
                        seRow.PRIORITY,
                        seRow.PRINTED,
                        seRow.IsUSERIDNull() ? (int?)null : seRow.USERID,
                        seRow.CZ_REZ1_Track,
                        seRow.CZ_REZ2_Track,
                        seRow.IsITEMCODENull() ? string.Empty : seRow.ITEMCODE,
                        0
                    );
            }

            incCountEntr = true;
        }


        private static void NastavPromenne(Pohoda_DataSets.Vydej.CZMST_SERow seRow)
        {
            seRow.VNDITNUM = string.Empty;
            seRow.ORD = 0;
            seRow.CZ_DatVyr_Delka = 0;
            seRow.CZ_DatVyr_Track = 0;
            seRow.CZ_SerNum_Delka = 0;
            seRow.CZ_SerNum_Track = 0;
            seRow.CZ_SW_Delka = 0;
            seRow.CZ_SW_Track = 0;
            seRow.CZ_Doslo = 255;
            seRow.DEX_ROW_ID = 0;
            seRow.Note = string.Empty;
            seRow.ITEMNMBR = string.Empty;
            seRow.LOCNCODE = string.Empty;
            seRow.ITEMDESC = string.Empty;
            seRow.QTYSHPPD = 0;
            seRow.QTYPACK = 0;
            
            //11.12.2024 zmena
            seRow.ITEMTYPE = string.Empty;
            //seRow.ITEMTYPE = "J";

            seRow.VNDDOCNM = string.Empty;
            seRow.CZ_CarKod = string.Empty;
            seRow.TYPEPAL = string.Empty;
            seRow.QTYPAL = 0;
            seRow.PRIORITY = 3;
            seRow.PRINTED = 0;
            seRow.SKL_ID = string.Empty;
            seRow.MJ = string.Empty;
            seRow.USERID = 0;
            seRow.CZ_REZ1_Track = 0;
            seRow.CZ_REZ2_Track= 0;
            seRow.ITEMCODE= string.Empty;
            seRow.WEIGHT = 0;
        }







    }
}
