using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Fask.ModulePohodaXML
{
    public static class Prijem
    {
        #region Historie Textove skladani
        //[Obsolete("Pouziva se objektove skladani XML", true)]
        //public static bool CreateRequest_Import_Prijemka_XML(Pohoda_DataSets.Prijem.ProductionDataTable dt_p, string file, int countEntries, string SKL_ID, string note)
        //{
        //    //TODO dodelat skladani XML kodu objektovo

        //    // konstanta pro tostrin() cisel na invariantni format ...
        //    System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

        //    //Datasets.Prijem.CZMST_PIDataTable dt_pi = null;
        //    //Datasets.Prijem.CZMST_PIHDataTable dt_pih = null;
        //    //Pohoda_DataSets.Prijem.ProductionDataTable dt_production = null;

        //    try
        //    {
        //        //dt_p = Database.Prijem.GETDATA_Production(countEntries, SKL_ID);
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
        //        var data = dt_p.OrderBy(x => x.id);
        //        var row_id = data.First();


        //        TextWriter tw = new StreamWriter(filename);
        //        tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //        tw.WriteLine("<dat:dataPack id=\"pri" + row_id.id.ToString() + "\" ico=\"" + Globals.ICO + "\" application=\"Konzole\" version=\"2.0\" note=\"Import Prijemky\"");
        //        tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
        //        tw.WriteLine("xmlns:pri=\"http://www.stormware.cz/schema/version_2/prijemka.xsd\"");
        //        tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

        //        //Production datatable
        //        //nejmensi ID
        //        //dt_p



        //        tw.WriteLine("<dat:dataPackItem id=\"PRI" + row_id.id.ToString() + "\" version=\"2.0\">");
        //        tw.WriteLine("  <pri:prijemka version=\"2.0\">");

        //        // hlavicka
        //        tw.WriteLine("      <pri:prijemkaHeader>");
        //        //tw.WriteLine("          <pri:date>" + date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString("00") + "</pri:date>");
        //        //tw.WriteLine("          <pri:numberOrder></pri:numberOrder>");
        //        //tw.WriteLine("          <pri:dateOrder>" + "1999-01-01" + "</pri:dateOrder>");
        //        //tw.WriteLine("          <pri:text>" + "Test import MSTW-Pohoda" + "</pri:text>");
        //        if (text != null)
        //            tw.WriteLine("          <pri:text>" + text + "</pri:text>");

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

        //        tw.WriteLine("      </pri:prijemkaHeader>");

        //        //polozky 
        //        tw.WriteLine("      <pri:prijemkaDetail>");

        //        //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter taSKZ = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter();
        //        //taSKZ.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

        //        Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter ta = new Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter();
        //        ta.Connection = new System.Data.SqlClient.SqlConnection(Globals.ConnectionString);

        //        foreach (Pohoda_DataSets.Prijem.ProductionRow row in dt_p)
        //        {

        //            if (row.IsTIMESTOPNull())
        //                continue;

        //            #region Vypočet ceny
        //            //// Guid pomoci ktereho se budou nasledne dohledavat pouzite materialy    
        //            //row.GUID;
        //            decimal materialy = 0;


        //            Pohoda_DataSets.Vydej.Production_SourcesDataTable dt = new Pohoda_DataSets.Vydej.Production_SourcesDataTable();

        //            ta.FillByGUIDProduction(dt, row.GUID);

        //            foreach (Pohoda_DataSets.Vydej.Production_SourcesRow item in dt)
        //            {
        //                //decimal Vnakup = (decimal)taSKZ.GetVNakupByID(int.Parse(item.ITEMNMBR));
        //                decimal Vnakup = (decimal)Database.Pohoda.SKz_GetVNakupByID(int.Parse(item.ITEMNMBR));
        //                materialy = materialy + (item.QTYSHPPD * Vnakup);
        //            }

        //            decimal fullPrice = (materialy) / row.qty;

        //            #endregion


        //            //// TODO : najit polozku v objednavce ... ???
        //            ////
        //            ////var objpols = pohoda_odbpol_dt.Where(x => (x.RefSKz.ToString() == row.ITEMNMBR) && (x.));
        //            //var objpols = pohoda_odbpol_dt.Where(x => (x.ID == row.ORD)); //jedinecna vazba ...
        //            //Pohoda_DataSets.DatabasePohoda.OBJpolRow objpol = null;
        //            //if (objpols.Count() > 0)
        //            //    objpol = objpols.First();

        //            tw.WriteLine("      <pri:prijemkaItem>");
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

        //            tw.WriteLine("          <pri:quantity>" + ((float)row.qty).ToString(nfi) + "</pri:quantity>");
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

        //            tw.WriteLine("          <pri:homeCurrency><typ:unitPrice>" + fullPrice.ToString(nfi) + "</typ:unitPrice></pri:homeCurrency>");
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
        //            tw.WriteLine("      </pri:prijemkaItem>");
        //        }

        //        tw.WriteLine("      </pri:prijemkaDetail>");

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


        //        tw.WriteLine("  </pri:prijemka>");
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

        #region Tvorba prijemky
        //public static bool CreateRequest_Import_Prijemka_XML_NEW(Pohoda_DataSets.Prijem.ProductionDataTable dt_p, string file, int countEntries, string SKL_ID, string note, string Vydejka)
        public static bool CreateRequest_Import_Prijemka_XML_NEW(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt_p, string file, int countEntries, string SKL_ID, string note, string Vydejka)
        {
            // konstanta pro tostrin() cisel na invariantni format ...
            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            try
            {

                //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter taSKZ = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter();
                //taSKZ.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter ta = new Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter();
                ta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

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

                var data = dt_p.OrderBy(x => x.id);
                var row_id = data.First();

                List<object> filterList = new List<object>();
                List<object> mainfilterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace pri = "http://www.stormware.cz/schema/version_2/prijemka.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                #region Header

                List<XElement> listHeader = new List<XElement>();

                if (text != null) { listHeader.Add(new XElement(pri + "text", text)); }

                if (poznB.Length > 0) { listHeader.Add(new XElement(pri + "note", poznB.ToString())); }

                listHeader.Add(new XElement(pri + "intNote", (poznamka2 == null ? string.Empty : poznamka2 + "\n") + "nacteno z konzole z dávky č.:" + countEntries.ToString()));

                if (datumdokladu.HasValue) { listHeader.Add(new XElement(pri + "date", XmlConvert.ToString(datumdokladu.Value, "yyyy-MM-dd"))); }

                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();

                //foreach (Pohoda_DataSets.Prijem.ProductionRow row in dt_p)
                foreach (var row in dt_p)
                {
                    if (row.IsTIMESTOPNull())
                        continue;

                    XElement itemElement;

                    #region Vypočet ceny varianta Dotahovani všech materialu z Production_Sources a dotaženi z SKz nakupni ceny


                    decimal materialy = 0;


                    Pohoda_DataSets.Vydej.Production_SourcesDataTable dt = new Pohoda_DataSets.Vydej.Production_SourcesDataTable();

                    ta.FillByGUIDProduction(dt, row.GUID);

                    foreach (Pohoda_DataSets.Vydej.Production_SourcesRow item in dt)
                    {
                        decimal Vnakup = (decimal)Database.Pohoda.SKz_GetVNakupByID(int.Parse(item.ITEMNMBR));
                        //decimal Vnakup = (decimal)Database.Pohoda.Get_Kc_From_SKPV_By_Cislo_ID(Vydejka, int.Parse(item.ITEMNMBR));
                        materialy = materialy + (item.QTYSHPPD * Vnakup);
                        //materialy +=  Vnakup;
                    }

                    decimal fullPrice = (materialy) / row.qty;

                    itemElement = new XElement(pri + "prijemkaItem",
                    new XElement(pri + "quantity", ((float)row.qty).ToString(nfi)),
                    new XElement(pri + "homeCurrency", new XElement(typ + "unitPrice", fullPrice.ToString(nfi))),
                    new XElement(pri + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

                    #endregion


                    #region Variantra neposila sa cena vubec, POHODA logika, Ceny i DPH SOU !!NULA!!

                    //itemElement = new XElement(pri + "prijemkaItem", 
                    //    new XElement(pri + "quantity", ((float)row.qty).ToString(nfi)),
                    //    new XElement(pri + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));


                    #endregion


                    listItem.Add(itemElement);
                }


                #endregion


                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "pri", "http://www.stormware.cz/schema/version_2/prijemka.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "pri_p" + row_id.id.ToString()),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_K),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Import Prijemky"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "pri_p" + row_id.id.ToString()),
                            new XAttribute("version", "2.0"),

                                new XElement(pri + "prijemka",
                                    new XAttribute("version", "2.0"),
                                    new XElement(pri + "prijemkaHeader", listHeader),
                                    new XElement(pri + "prijemkaDetail", listItem)
                                        )));

                root.Save(filename);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
            return true;

        }

        //public static string LoadResponse_Import_Prijemka_XML(Pohoda_DataSets.Prijem.ProductionDataTable dt_p, string file, int countEntries, string SKL_ID)
        public static string LoadResponse_Import_Prijemka_XML(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt_p, string file, int countEntries, string SKL_ID)
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
                XNamespace pri = "http://www.stormware.cz/schema/version_2/prijemka.xsd";

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
                XElement prijemkaResponse =
                    root.Element(rsp + "responsePack").
                        Element(rsp + "responsePackItem").
                            Element(pri + "prijemkaResponse");

                //XAttribute prijemkaResponseState = prijemkaResponse.Attribute("state");

                XElement dokladnumber =
                    prijemkaResponse.Element(rdc + "producedDetails").
                    Element(rdc + "number");

                string number = dokladnumber.Value;


                // koretne ulozeno a muzu dokoncit ... 
                // dokonceni => ISOK = datetime.now
                DateTime isOK = DateTime.Now;
                dt_p.ToList().ForEach(x => x.ISOK = isOK);


                return number;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Tvorba prijemky Verze BIOMAG

        public static bool CreateRequest_Import_Prijemka_XML_V2(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt_p, string file, int countEntries, string note)
        {
            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter ta = new Pohoda_DataSets.VydejTableAdapters.Production_SourcesTableAdapter();
                ta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                DateTime? datumdokladu = null;

                string text = null;
                string poznamka = null;
                string poznamka2 = null;
                StringBuilder poznB = new StringBuilder();

                if (!String.IsNullOrEmpty(poznamka))
                    poznB.AppendLine(poznamka);

                if (!String.IsNullOrEmpty(note))
                    poznB.AppendLine(note);


                string filename = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + file;
                DateTime date = DateTime.Now;

                var data = dt_p.OrderBy(x => x.id);
                var row_id = data.First();

                List<object> filterList = new List<object>();
                List<object> mainfilterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace pri = "http://www.stormware.cz/schema/version_2/prijemka.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                #region Header

                List<XElement> listHeader = new List<XElement>();

                if (text != null) { listHeader.Add(new XElement(pri + "text", text)); }

                if (poznB.Length > 0) { listHeader.Add(new XElement(pri + "note", poznB.ToString())); }

                listHeader.Add(new XElement(pri + "intNote", (poznamka2 == null ? string.Empty : poznamka2 + "\n") + "nacteno z konzole z dávky č.:" + countEntries.ToString()));

                if (datumdokladu.HasValue) { listHeader.Add(new XElement(pri + "date", XmlConvert.ToString(datumdokladu.Value, "yyyy-MM-dd"))); }

                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();

                foreach (var row in dt_p)
                {
                    if (row.IsTIMESTOPNull())
                        continue;

                    List<XElement> prijemkaItem = new List<XElement>();

                    prijemkaItem.Add(new XElement(pri + "quantity", ((float)row.qty).ToString(nfi)));
                    prijemkaItem.Add(new XElement(pri + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

                    string poz = string.Empty;

                    if (!row.IsBarcodePNull() && !string.IsNullOrEmpty(row.BarcodeP))
                    {
                        poz += string.Format("BarcodeP: '{0}'", row.BarcodeP);
                    }

                    if (!row.IsREZ_1Null() && !string.IsNullOrEmpty(row.REZ_1))
                    {
                        poz += string.Format(Globals_V1.Konfigurace.Vyroba[0].Prijemka_Lokalizace_REZ_1 + " '{0}'"  , row.REZ_1);
                    }

                    if (!row.IsREZ_2Null() && !string.IsNullOrEmpty(row.REZ_2))
                    {
                        poz += string.Format(Globals_V1.Konfigurace.Vyroba[0].Prijemka_Lokalizace_REZ_2 + " '{0}'", row.REZ_2);
                    }

                    if (!row.IsREZ_3Null() && !string.IsNullOrEmpty(row.REZ_3))
                    {
                        poz += string.Format(Globals_V1.Konfigurace.Vyroba[0].Prijemka_Lokalizace_REZ_3 + " '{0}'", row.REZ_3);
                    }

                    if (!row.IsREZ_4Null() && !string.IsNullOrEmpty(row.REZ_4))
                    {
                        poz += string.Format(Globals_V1.Konfigurace.Vyroba[0].Prijemka_Lokalizace_REZ_4 + " '{0}'", row.REZ_4);
                    }

                    if (!row.IsREZ_5Null() && !string.IsNullOrEmpty(row.REZ_5))
                    {
                        poz += string.Format(Globals_V1.Konfigurace.Vyroba[0].Prijemka_Lokalizace_REZ_5 + " '{0}'", row.REZ_5);
                    }


                    if (!string.IsNullOrEmpty(poz))
                        prijemkaItem.Add(new XElement(pri + "note", poz));

                    XElement itemElement = new XElement(pri + "prijemkaItem",
                    prijemkaItem
                    );

                    listItem.Add(itemElement);
                }


                #endregion


                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "pri", "http://www.stormware.cz/schema/version_2/prijemka.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "pri_p" + row_id.id.ToString()),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_K),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Import Prijemky"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "pri_p" + row_id.id.ToString()),
                            new XAttribute("version", "2.0"),

                                new XElement(pri + "prijemka",
                                    new XAttribute("version", "2.0"),
                                    new XElement(pri + "prijemkaHeader", listHeader),
                                    new XElement(pri + "prijemkaDetail", listItem)
                                        )));
                root.Save(filename);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
            return true;

        }

        public static string LoadResponse_Import_Prijemka_XML_V2(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt_p, string file, int countEntries)
        {
            try
            {
                string filename = file;

                if (!File.Exists(filename))
                    throw new Exception("Response soubor '" + file + "' neexistuje!");

                XNamespace rdc = "http://www.stormware.cz/schema/version_2/documentresponse.xsd";
                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace rsp = "http://www.stormware.cz/schema/version_2/response.xsd";
                XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
                XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
                XNamespace pri = "http://www.stormware.cz/schema/version_2/prijemka.xsd";

                XDocument root = XDocument.Load(filename);

                XElement responsePack = root.Element(rsp + "responsePack");
                XAttribute state = responsePack.Attribute("state");
                if (state.Value != "ok")
                {
                    XAttribute note = responsePack.Attribute("note");
                    throw new Exception("Nepodařilo se získat data příjemky.\nStatus:" + state.Value + "\nChyba:" + note.Value);
                }
                XElement responsePackItem = responsePack.Element(rsp + "responsePackItem");
                XAttribute responsePackItemState = responsePackItem.Attribute("state");
                if (responsePackItemState.Value.Trim() != "ok")
                {
                    XAttribute responsePackItemNote = responsePackItem.Attribute("note");
                    throw new Exception("Nepodařilo se získat data příjemky.\nStatus:" + responsePackItemState.Value + "\nChyba:" + responsePackItemNote.Value);
                }
                XElement prijemkaResponse =
                    root.Element(rsp + "responsePack").
                        Element(rsp + "responsePackItem").
                            Element(pri + "prijemkaResponse");

                XElement dokladnumber =
                    prijemkaResponse.Element(rdc + "producedDetails").
                    Element(rdc + "number");

                string number = dokladnumber.Value;

                DateTime isOK = DateTime.Now;
                dt_p.ToList().ForEach(x => x.ISOK = isOK);

                return number;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
