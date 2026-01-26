using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Fask.Tracing;

namespace Fask.SQL
{
    public partial class Provider : Fask.Server.Interfaces.Prijem.IPrijem, Fask.Server.Interfaces.WebControl.IWebControl
    {
        public const string prijem_import_prijemka = XML.MST_Pohoda._import_prijemka + ".xml";

        /// <summary>
        /// generuje vydanou objednavku a ulozi ji do czmst_pe... doufam ...
        /// </summary>
        /// <param name="vydejka">id(cislo) vydane objednavky ...</param>
        /// <returns>OK = vse dobre, jinak nejaky jiny text ... ???</returns>
        public string ExportPrijemkaPohoda_Z_ObjednavkyVydane(Fask.Server.Interfaces.Classes.Objednavka objednavka)
        {
            try
            {
                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(XML.MST_Pohoda._export_objednavka_vydana + ".xml");

                string dateFrom = string.Empty;
                string dateTill = string.Empty;
                string dateLasChange = string.Empty;
                List<string> icos = new List<string>();
                string uzivFiltrPohoda = string.Empty;
                List<string> companyNames = new List<string>();
                List<string> cislaDoklady = new List<string>();

                if (objednavka.ID != string.Empty)
                    cislaDoklady.Add(objednavka.ID);

                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    return pom;

                if (!Prijem.CreateRequest_ObjednavkaVydana_XML(filename, dateLasChange, dateFrom, dateTill, "", companyNames, uzivFiltrPohoda, icos, cislaDoklady))
                    return "CHYBA";

                bool saveToDB = objednavka.ID != string.Empty ? true : false;

                //pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, objednavka, new Fask.Server.Interfaces.Classes.User());
                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                return Prijem.LoadResponse_ObjednavkaVydana_XML(responsefilename, saveToDB, objednavka);
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(Vydejka: " + objednavka.ID + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
                //return ex.Message;
            }
        }

        /// <summary>
        /// generuje davku z prevodky a ulozi ji do czmst_pe... 
        /// </summary>
        /// <param name="objednavka">nese id(cislo) dokladu(prevodky)</param>
        /// <returns>OK = vse dobre, jinak nejaky jiny text k pripadnemu zobrazeni na terminalu</returns>
        public string ExportPrijemkaPohoda_Z_Prevodky(Fask.Server.Interfaces.Classes.Objednavka objednavka)
        {
            try
            {
                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(XML.MST_Pohoda._export_prevodka + ".xml");

                if (String.IsNullOrEmpty(objednavka.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    return pom;

                if (!Prijem.CreateRequest_Prevodka_XML(filename, objednavka))
                    return "CHYBA";
                
                bool saveToDB = objednavka.ID != string.Empty ? true : false;

                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                return Prijem.LoadResponse_Prevodka_XML(responsefilename, saveToDB, objednavka);
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(Prijemka z prevodky: " + objednavka.ID + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
                //return ex.Message;
            }
        }

        /// <summary>
        /// Provede import Prijemky do Pohody na zaklade vydane objednavky a prijmovych dat
        /// Parovani pres SOPNUMBE a ORD => prijemka 
        /// </summary>
        /// <param name="countEntries">cislo davky, ktera se importovala</param>
        /// <returns>OK kdyz vse v poradku, jinak text chyby ... </returns>
        public string ImportPrijemkaPohoda(int countEntries, int userID)
        {
            try
            {

                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(prijem_import_prijemka);

                string dateFrom = string.Empty;
                string dateTill = string.Empty;
                string dateLasChange = string.Empty;
                List<string> icos = new List<string>();
                string uzivFiltrPohoda = string.Empty;
                List<string> companyNames = new List<string>();
                List<string> cislaDoklady = new List<string>();

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Prijem.CreateImportXML_PrijemkaVazbaObjednavka_Z_Prijmu(filename, countEntries, ""))
                    return "CHYBA";

                bool saveToDB = true;
                string responsefilename;

                Fask.Server.Interfaces.Classes.User user = new Fask.Server.Interfaces.Classes.User();
                user.ID = userID;

                pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(
                    filename, 
                    saveToDB, 
                    new Fask.Server.Interfaces.Classes.Objednavka(), 
                    user,
                    out responsefilename
                    );

                if (pom != "OK")
                    return pom;
                //Parovani a update objednavka/prijemka v pohoda ... 
                pom = LoadPrijemImportResponseXMLAndMakeUpdateDB(responsefilename, countEntries);

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Provede import Prijemky do Pohody na zaklade vydane objednavky a prijmovych dat
        /// Parovani pres SOPNUMBE a ORD => prijemka 
        /// </summary>
        /// <param name="countEntries">cislo davky, ktera se importovala</param>
        /// <returns>OK kdyz vse v poradku, jinak text chyby ... </returns>
        public string ImportPrijemkaPohoda_Z_SI(int countEntries, int userID)
        {
            try
            {

                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(prijem_import_prijemka);

                string dateFrom = string.Empty;
                string dateTill = string.Empty;
                string dateLasChange = string.Empty;
                List<string> icos = new List<string>();
                string uzivFiltrPohoda = string.Empty;
                List<string> companyNames = new List<string>();
                List<string> cislaDoklady = new List<string>();

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Prijem.CreateImportXML_PrijemkaVazbaObjednavka_Z_Prijmu_Z_SI(filename, countEntries, ""))
                    return "CHYBA";

                bool saveToDB = true;
                string responsefilename;

                Fask.Server.Interfaces.Classes.User user = new Fask.Server.Interfaces.Classes.User();
                user.ID = userID;

                pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(
                    filename,
                    saveToDB,
                    new Fask.Server.Interfaces.Classes.Objednavka(),
                    user,
                    out responsefilename
                    );

                if (pom != "OK")
                    return pom;
                //Parovani a update objednavka/prijemka v pohoda ... 
                pom = LoadPrijemImportResponseXMLAndMakeUpdateDB_Z_SI(responsefilename, countEntries);

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Provede parovani importovane prijemky s vydanou objednavkou ze ktere byly data porizeny
        /// </summary>
        /// <param name="file">soubor s odpovedi serveru pro vytazeni cisla nove prijemky</param>
        /// <param name="countentries">cislo davky prijemky MST, ktera byla vykryta pro vytazeni cisla objednavky vydane</param>
        /// <returns>OK kdyz ok, jinak text chyby ...</returns>
        public string LoadPrijemImportResponseXMLAndMakeUpdateDB(string filename, int countentries)
        {
            System.Data.OleDb.OleDbConnection oledbConnection = null;
            try
            {

                string actualStrName = string.Empty;
                string previousStrName = string.Empty;
                string actualSonnumber = string.Empty;

                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    return pom;

                //ResponsePack odpoved = new ResponsePack();
                XML.Classes.Response2 response = new XML.Classes.Response2(filename);

                // 17.6.2016 PeV: uprava, aby bylo mozne odeslat davku v pripade, ze data se jiz dostala do pohody (napr. nastal timeout na terminalu)
                // OK2 -> zaznam se jiz dostal uspesne do pohody, je treba umoznit smazat davku z terminalu
                if (response.Status == "OK2")
                    return "OK";

                if (response.Status != "OK")
                    return response.Status;

				// \TODO : !!! "Trvaly doklad" : pozor toto neni reseno => jak vyresit?
                // Definice "Trvaly doklad" z dokumentace Pohoda:
                ////Záznamy zapsané v agendách Přijaté a Vydané nabídky, Přijaté a Vydané poptávky, Přijaté a Vydané objednávky můžete označit jako trvalé pomocí povelu Trvalý doklad z nabídky Záznam. Označit lze jen celý doklad, nikoli jednotlivé položky. 
                ////Povel nastaví na dokladu příznak Trvalý doklad. U takto označených dokladů se nebude sledovat přenesené množství položek a bude se neomezeně nabízet pro vložení do jiných dokladů.
                //// Doklady, které budou mít příznak Trvalý doklad, se vždy po datové uzávěrce přenesou do nové účetní jednotky. Nebudete-li chtít objednávku již dále používat, musíte zrušit příznak Trvalý doklad resp. doklad vymazat.
                //// Sloupec Trvalý doklad můžete pomocí dotazu využít pro vyhledávání trvalých záznamů.
                //// Rezervovanou objednávku nelze označit jako trvalou.

				// \TODO : obecne byse v pohode melo pracovat pres skzbuf
                // SKzBuf => aktualizovat hodnoty, nastavit Foul<>0 (Foul=-1)
                // Aktualizovat Skz dle SkzBuf kdyz je Foul<>0 a nastavit pak Foul=0

                //cislo dokladu nalezeno => aktualizace dokladu v pohode...
                //1) zjistit cislo dokladu prijemky
                //2) vytahnout polozky z pe s ord, itemnmbr, sopnumbe?
                //2a) cislo dokladu objednavky
                //3) vytahnout polozky z skpp a skppol pro konkretni novy doklad
                //4) vytahnout polozka z obj a objpol pro doklad
                //5) sparovat polozky obj(pe) s skpp
                // a) dohledani karty zasoby a zmena mnozstvi hodnoty OBJV
                // b) zajistit oznaceni prenesene 
                // - preneseno se nastavuje jen kdyz je preneseno cele mnozstvi polozek ...
                // - projit polozky objednavky a pokud je u vsech polozek dodano >= mnozstvi tak nastavit
                // c) nastaveni priznaku vyrizeno 
                // - musi se prepocitat hodnoty na skladove zasobe ObjedV
                //// 5 d) dokonceni update hodnoty v SKzBuf...
                //6) update v transakci ...


                // 1) Cislo dokladu prijemky
                string cislodokladuprijemka = response.CisloDokladuPrijemka;
                if (String.IsNullOrEmpty(cislodokladuprijemka))
                    return "Cislo dokladu prijemky nenalezeno v odpovedi pohody";

                // 2) Vytahnout polozky prijemky z MST
                Datasets.Prijem prijemDS = new Datasets.Prijem();

                //Datasets.PrijemTableAdapters.CZMST_PETableAdapter peAdapter = new Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
                //Datasets.PrijemTableAdapters.CZMST_PITableAdapter piAdapter = new Datasets.PrijemTableAdapters.CZMST_PITableAdapter();
                //peAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
                //piAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                if (Globals_V1.Konfigurace.Prijem[0].GrupujDataPrijemka)
                    prijemDS = Database.Prijem.GETDATA_CZMSTPI_DS_GroupBy_CountEntries(countentries);
                else
                    prijemDS = Database.Prijem.GETDATA_CZMSTPI_DS_By_CountEntries(countentries);


                //peAdapter.FillByCountEntries(prijemDS.CZMST_PE, countentries);
                Database.Prijem.GETDATA_CZMSTPE_DS_By_CountEntries(prijemDS.CZMST_PE, countentries);

                //piAdapter.FillByCountEntries(prijemDS.CZMST_PI, countentries);

                string cislodokladuobjednavky = prijemDS.CZMST_PE[0].PONUMBER.Trim();

                Datasets.DatabasePohoda pohodaDS = new Datasets.DatabasePohoda();
                oledbConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                //3) vytahnout polozky z skpp a skppol pro konkretni novy doklad
                //Datasets.DatabasePohodaTableAdapters.SKPPTableAdapter skppAdapter = new Datasets.DatabasePohodaTableAdapters.SKPPTableAdapter();
                //Datasets.DatabasePohodaTableAdapters.SKPPpolTableAdapter skpppolAdapter = new Datasets.DatabasePohodaTableAdapters.SKPPpolTableAdapter();
                //skppAdapter.Connection = oledbConnection;
                //skpppolAdapter.Connection = oledbConnection;

                //skppAdapter.FillByCislo(pohodaDS.SKPP, cislodokladuprijemka);
				Database.Pohoda.SKPP_FillByCislo(pohodaDS.SKPP, cislodokladuprijemka);
                //skpppolAdapter.FillByRefAg(pohodaDS.SKPPpol, pohodaDS.SKPP[0].ID);
				Database.Pohoda.SKPPpol_FillByRefAg(pohodaDS.SKPPpol, pohodaDS.SKPP[0].ID);

                //4) vytahnout polozky objednavky z pohody
                //Datasets.DatabasePohodaTableAdapters.OBJTableAdapter objAdapter = new Datasets.DatabasePohodaTableAdapters.OBJTableAdapter();
                //Datasets.DatabasePohodaTableAdapters.OBJpolTableAdapter objpolAdapter = new Datasets.DatabasePohodaTableAdapters.OBJpolTableAdapter();
                //objAdapter.Connection = oledbConnection;
                //objpolAdapter.Connection = oledbConnection;

                //objAdapter.FillByCislo(pohodaDS.OBJ, cislodokladuobjednavky);
				Database.Pohoda.OBJ_FillByCislo(pohodaDS.OBJ, cislodokladuobjednavky);
                //objpolAdapter.FillByRefAg(pohodaDS.OBJpol, pohodaDS.OBJ[0].ID);
				Database.Pohoda.OBJPol_FillByRefAg(pohodaDS.OBJpol, pohodaDS.OBJ[0].ID);

                //5) sparovani ...

                // 5)a) dohledani karty zasoby a zmena mnozstvi hodnoty OBJV
                //Datasets.DatabasePohodaTableAdapters.SKzObjedVTableAdapter skzObjedVAdapter = new Datasets.DatabasePohodaTableAdapters.SKzObjedVTableAdapter();
                //skzObjedVAdapter.Connection = oledbConnection;
                //skzObjedVAdapter.ClearBeforeFill = false;

                //Datasets.DatabasePohodaTableAdapters.SKzBufTableAdapter skzBufAdapter = new Datasets.DatabasePohodaTableAdapters.SKzBufTableAdapter();
                //skzBufAdapter.Connection = oledbConnection;
                //skzBufAdapter.ClearBeforeFill = false;

                oledbConnection.Open();
                // transakce se resi na konci v ta managerovi ... 
                //System.Data.OleDb.OleDbTransaction oletrans = oledbConnection.BeginTransaction();

                foreach (Datasets.Prijem.CZMST_PIRow pir in prijemDS.CZMST_PI)
                {
                    //neprirazene polozky dle itemnmbr a mnozstvi

                    Datasets.DatabasePohoda.SKzDataTable SKzDT = new Datasets.DatabasePohoda.SKzDataTable(); 
                    Database.Pohoda.SKz_FillByID(SKzDT, int.Parse(pir.ITEMNMBR));

                    string query = " RefSKz=" + pir.ITEMNMBR.Trim() + " AND Mnozstvi=" + pir.QTYSHPPD.ToString("0.00", CultureInfo.InvariantCulture) + " AND RefPol is NULL AND RelAgID is NULL";

                    if (SKzDT.Count == 1)
                    {
                        var SKzRow = SKzDT.First();
                        int? resskzvc = SKzRow.IsRelSKzVCNull() ? (int?)null : SKzRow.RelSKzVC;

                        if ( resskzvc.HasValue)
                        {
                            if (resskzvc > 0)
                            {
                                query = " RefSKz=" + pir.ITEMNMBR.Trim() +
                                    " AND Mnozstvi=" + pir.QTYSHPPD.ToString("0.00", CultureInfo.InvariantCulture) +
                                    " AND RefPol is NULL " +
                                    " AND RelAgID is NULL " +
                                    " AND VCislo = '" + pir.SERLTNUM.Trim() + "'";
                            }
                        }
                    }

                   //string query = " RefSKz=" + pir.ITEMNMBR.Trim() + " AND Mnozstvi=" + pir.QTYSHPPD.ToString("0.00", CultureInfo.InvariantCulture) + " AND RefPol is NULL AND RelAgID is NULL";


                    Datasets.DatabasePohoda.SKPPpolRow[] neprirazenePolozky = (Datasets.DatabasePohoda.SKPPpolRow[])pohodaDS.SKPPpol.Select(
                        query
                        , "ID"
                        , System.Data.DataViewRowState.CurrentRows);
                    if (neprirazenePolozky.Length == 0)
                    { //nic se nedeje, protoze nebyly nalezeny => log... jde o chybu ...
						Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PrijemPohodaXML", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo volnou shodu v databazi skpppol s polozkou:" + pir.ORD + "|" + pir.ITEMNMBR + "|" + pir.QTYSHPPD + "|" + pir.GUID.ToString());
                        continue; //pokracuji dalsim radkem ... 
                    }
                    else if (neprirazenePolozky.Length > 0)
                    { //je jich vice, no tak priradim prvni nalezenou, protoze je to v podstate uplne sumus...
                        //prirazeni se deje az po dohledani radku objednavky ...
                    }

                    Datasets.DatabasePohoda.OBJpolRow objpolr = pohodaDS.OBJpol.FindByID(pir.ORD);
                    if (objpolr != null)
                    {
                        //experimentalne zjistena hodnota pro dokladovou vazbu na vydanou objednavku ... 
                        // CHECK : !!! muze se v aktualizaci pohody zmenit !!!
                        neprirazenePolozky[0].RelAgID = 12;
                        neprirazenePolozky[0].RefPol = objpolr.ID;

                        // Pokud to je trvaly doklad, tak se mnozstvi na zasobe neponizuje ...
                        if (!pohodaDS.OBJ[0].TrvalyDok) 
                        {
							objpolr.Dodano += (double)pir.QTYSHPPD; // \TODO : zaokroulovani????
                            // 5)a) dohledani / dotazeni zbozi karty
                            int skzID = int.Parse(pir.ITEMNMBR);
                            Datasets.DatabasePohoda.SKzObjedVRow skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                            if (skzObjedVRow == null)
                            {
                                //skzObjedVAdapter.FillByID(pohodaDS.SKzObjedV, int.Parse(pir.ITEMNMBR));
								Database.Pohoda.SKzObjedV_FillByID(pohodaDS.SKzObjedV, int.Parse(pir.ITEMNMBR));
                                skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                            }
                            if (skzObjedVRow == null)
								Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PrijemPohodaXML", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Nenalezena karta zasoby pro snizeni objednaneho mnozstvi:" + pir.ORD + "|" + pir.ITEMNMBR + "|" + pir.QTYSHPPD + "|" + pir.GUID.ToString());
                            else
                                skzObjedVRow.ObjedV -= (double)pir.QTYSHPPD; //pokud jiz bylo v cyklu drive snizeno, tak se snizi opet ...
                        }
                    }
                    else //je-li null, tak se nenaslo zbozi ... coz je chyba a bude zalogovana ...
                    {
						Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PrijemPohodaXML", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo volnou shodu v databazi objpol s polozkou:" + pir.ORD + "|" + pir.ITEMNMBR + "|" + pir.QTYSHPPD + "|" + pir.GUID.ToString());
                    }

                }

                // 5 b) zajistit oznaceni prenesene 
                // - preneseno se nastavuje jen kdyz je preneseno cele mnozstvi polozek ...
                // - projit polozky objednavky a pokud je u vsech polozek dodano >= mnozstvi tak nastavit
                bool bdodano = true;
                foreach (var item in pohodaDS.OBJpol)
                {
                    if (item.Dodano < item.Mnozstvi)
                    {
                        bdodano = false;
                        break;
                    }
                }
                pohodaDS.OBJ[0].BDodano = bdodano;

                // 5 c) nastaveni priznaku vyrizeno 
                // - musi se prepocitat hodnoty na skladove zasobe ObjedV
                // - a to jen tehdy, pokud neni objednavka Trvaly doklad
                if (!pohodaDS.OBJ[0].TrvalyDok && Globals_V1.Konfigurace.Prijem[0].Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit)
                {
                    // 5.9.2018 : JiS - aktualizace hodnot ObjedV na karte zasob 
                    // Je-li jiz vyrizeno, tak nic nemenit...
                    bool vyrizeno = pohodaDS.OBJ[0].Vyrizeno;
                    if (!vyrizeno)
                    {
                        if (pohodaDS.OBJ[0].BDodano && Globals_V1.Konfigurace.Prijem[0].Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit)
                        {
                            pohodaDS.OBJ[0].Vyrizeno = true;
                        }
                        else if (Globals_V1.Konfigurace.Prijem[0].Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit_Castecne_Plneni) // neni uplne dodano
                        {

                            pohodaDS.OBJ[0].Vyrizeno = true;

                            // Pokud menim na vyrizeno, tak musim aktualizovat hodnoty mnozstvi na karte zasob v skz a skzBuf..???
                            // pro kazdou polozku objednavky

							foreach (Datasets.DatabasePohoda.OBJpolRow item in pohodaDS.OBJpol)
                            {
                                // jeste neco zbyva dodat
                                // ??? co zaokrouhlovaci chyba??? (prevest na decimal 5desmist?)
                                if (item.Dodano < item.Mnozstvi)
                                {
                                    double zbyvadodat = item.Mnozstvi - item.Dodano; // toto se musi ponizit

                                    // 5)a) dohledani / dotazeni zbozi karty
                                    int skzID = item.RefSKz; //int.Parse(pir.ITEMNMBR);
                                    Datasets.DatabasePohoda.SKzObjedVRow skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                                    if (skzObjedVRow == null)
                                    {
                                        //skzObjedVAdapter.FillByID(pohodaDS.SKzObjedV, skzID); //pir.ITEMNMBR));
										Database.Pohoda.SKzObjedV_FillByID(pohodaDS.SKzObjedV, skzID); //pir.ITEMNMBR));
										skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                                    }
                                    if (skzObjedVRow == null)
										Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PrijemPohodaXML", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Nenalezena karta zasoby pro snizeni objednaneho mnozstvi:" + skzID + "| zbyva dodat: " + zbyvadodat);
                                    else
                                        skzObjedVRow.ObjedV -= zbyvadodat; //(double)pir.QTYSHPPD; //pokud jiz bylo v cyklu drive snizeno, tak se snizi opet ...
                                }
                            }
                        }
                    }
                }

                //// 5 d) dokonceni update hodnoty v SKzBuf...
                // ??? Je to vubec treba ??? k cemu je a jak se plni SKzBuf???
                foreach (var item in pohodaDS.SKzObjedV)
                {
                    //skzBufAdapter.FillByRefSkz(pohodaDS.SKzBuf, item.ID);
					Database.Pohoda.SKzBuf_FillByRefSkz(pohodaDS.SKzBuf, item.ID);
                    var skzbufrows = pohodaDS.SKzBuf.Where(x => x.RefSKz == item.ID);
                    if (skzbufrows.Count() > 0)
                    {
                        foreach (var skzbufrow in skzbufrows)
                        {
                            skzbufrow.ObjedV = item.ObjedV;
                        }
                    }
                    else
                    {
						Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PrijemPohodaXML", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo shodu v databazi skzbuf s polozkou:" + item.ID + "|" + item.ObjedV);
                    }
                }

                //7) update v transakci ... 

                //Datasets.DatabasePohodaTableAdapters.TableAdapterManager pohodaTaManager = new Datasets.DatabasePohodaTableAdapters.TableAdapterManager();
                //pohodaTaManager.OBJpolTableAdapter = objpolAdapter;
                //pohodaTaManager.SKPPpolTableAdapter = skpppolAdapter;
                //pohodaTaManager.OBJTableAdapter = objAdapter;
                //pohodaTaManager.SKzObjedVTableAdapter = skzObjedVAdapter;
                //pohodaTaManager.SKzBufTableAdapter = skzBufAdapter;

                //pohodaTaManager.Connection = oledbConnection;
                //transakce se otevira v managerovi ... 
                //pohodaTaManager.Connection.Open();
                //System.Data.IDbTransaction pohodaTransaction = pohodaTaManager.Connection.BeginTransaction();
                //pohodaTaManager.UpdateOrder = Datasets.DatabasePohodaTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
                //pohodaTaManager.UpdateAll(pohodaDS);

				System.Data.OleDb.OleDbTransaction trans = null;
				System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
				try
				{
					connection.Open();
					trans = connection.BeginTransaction();
					Database.Pohoda.OBJPol_Update(pohodaDS, connection, trans);
					Database.Pohoda.SKPPpol_Update(pohodaDS, connection, trans);
					Database.Pohoda.OBJ_Update(pohodaDS, connection, trans);
					Database.Pohoda.SKzObjedV_Update(pohodaDS, connection, trans);
					Database.Pohoda.SKzBuf_Update(pohodaDS, connection, trans);


					if (trans != null)
						trans.Commit();
				}
				catch (Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
					try
					{
						if (trans != null)
							trans.Rollback();
					}
					catch (Exception exx)
					{
						Fask.Logging.ExceptionHandler2.Handle(exx);
				
					}

					throw ex;
					
				}


                return "OK";

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return ex.Message;
            }
            finally
            {
                if (oledbConnection != null && ((oledbConnection.State & ConnectionState.Open) == ConnectionState.Open))
                    oledbConnection.Close();
            }
        }

        /// <summary>
        /// Provede parovani importovane prijemky s vydanou objednavkou ze ktere byly data porizeny
        /// </summary>
        /// <param name="file">soubor s odpovedi serveru pro vytazeni cisla nove prijemky</param>
        /// <param name="countentries">cislo davky prijemky MST, ktera byla vykryta pro vytazeni cisla objednavky vydane</param>
        /// <returns>OK kdyz ok, jinak text chyby ...</returns>
        public string LoadPrijemImportResponseXMLAndMakeUpdateDB_Z_SI(string filename, int countentries)
        {
            System.Data.OleDb.OleDbConnection oledbConnection = null;
            try
            {

                string actualStrName = string.Empty;
                string previousStrName = string.Empty;
                string actualSonnumber = string.Empty;
                bool logovani = true;

                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    return pom;

                //ResponsePack odpoved = new ResponsePack();
                XML.Classes.Response2 response = new XML.Classes.Response2(filename);

                // 17.6.2016 PeV: uprava, aby bylo mozne odeslat davku v pripade, ze data se jiz dostala do pohody (napr. nastal timeout na terminalu)
                // OK2 -> zaznam se jiz dostal uspesne do pohody, je treba umoznit smazat davku z terminalu
                if (response.Status == "OK2")
                    return "OK";

                if (response.Status != "OK")
                    return response.Status;

                // \TODO : !!! "Trvaly doklad" : pozor toto neni reseno => jak vyresit?
                // Definice "Trvaly doklad" z dokumentace Pohoda:
                ////Záznamy zapsané v agendách Přijaté a Vydané nabídky, Přijaté a Vydané poptávky, Přijaté a Vydané objednávky můžete označit jako trvalé pomocí povelu Trvalý doklad z nabídky Záznam. Označit lze jen celý doklad, nikoli jednotlivé položky. 
                ////Povel nastaví na dokladu příznak Trvalý doklad. U takto označených dokladů se nebude sledovat přenesené množství položek a bude se neomezeně nabízet pro vložení do jiných dokladů.
                //// Doklady, které budou mít příznak Trvalý doklad, se vždy po datové uzávěrce přenesou do nové účetní jednotky. Nebudete-li chtít objednávku již dále používat, musíte zrušit příznak Trvalý doklad resp. doklad vymazat.
                //// Sloupec Trvalý doklad můžete pomocí dotazu využít pro vyhledávání trvalých záznamů.
                //// Rezervovanou objednávku nelze označit jako trvalou.

                // \TODO : obecne byse v pohode melo pracovat pres skzbuf
                // SKzBuf => aktualizovat hodnoty, nastavit Foul<>0 (Foul=-1)
                // Aktualizovat Skz dle SkzBuf kdyz je Foul<>0 a nastavit pak Foul=0

                //cislo dokladu nalezeno => aktualizace dokladu v pohode...
                //1) zjistit cislo dokladu prijemky
                //2) vytahnout polozky z pe s ord, itemnmbr, sopnumbe?
                //2a) cislo dokladu objednavky
                //3) vytahnout polozky z skpp a skppol pro konkretni novy doklad
                //4) vytahnout polozka z obj a objpol pro doklad
                //5) sparovat polozky obj(pe) s skpp
                // a) dohledani karty zasoby a zmena mnozstvi hodnoty OBJV
                // b) zajistit oznaceni prenesene 
                // - preneseno se nastavuje jen kdyz je preneseno cele mnozstvi polozek ...
                // - projit polozky objednavky a pokud je u vsech polozek dodano >= mnozstvi tak nastavit
                // c) nastaveni priznaku vyrizeno 
                // - musi se prepocitat hodnoty na skladove zasobe ObjedV
                //// 5 d) dokonceni update hodnoty v SKzBuf...
                //6) update v transakci ...


                // 1) Cislo dokladu prijemky
                string cislodokladuprijemka = response.CisloDokladuPrijemka;
                if (String.IsNullOrEmpty(cislodokladuprijemka))
                    return "Cislo dokladu prijemky nenalezeno v odpovedi pohody";

                // 2) Vytahnout polozky prijemky z MST
                Datasets.Vydej.CZMST_SIDataTable vydejSIdt = new Datasets.Vydej.CZMST_SIDataTable();
                Datasets.Vydej.CZMST_SEDataTable vydejSEdt = new Datasets.Vydej.CZMST_SEDataTable();
                Datasets.Vydej.CZMST_SERow vydejSErow = null;

                //Datasets.PrijemTableAdapters.CZMST_PETableAdapter peAdapter = new Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
                //Datasets.PrijemTableAdapters.CZMST_PITableAdapter piAdapter = new Datasets.PrijemTableAdapters.CZMST_PITableAdapter();
                //peAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
                //piAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                if (Globals_V1.Konfigurace.Prijem[0].GrupujDataPrijemka)
                    vydejSIdt = Database.Vydej.GETDATA_CZMSTSI_DS_GroupBy_CountEntries(countentries);
                else
                    vydejSIdt = Database.Vydej.GETDATA_CZMSTSI_By_CountEntries(countentries);


                //peAdapter.FillByCountEntries(prijemDS.CZMST_PE, countentries);
                vydejSEdt = Database.Vydej.GETDATA_CZMSTSE_By_CountEntries(countentries);

                //piAdapter.FillByCountEntries(prijemDS.CZMST_PI, countentries);
                try
                {
                    vydejSErow = vydejSEdt.First();
                }
                catch (Exception ex)
                {

                    //nic
                }
                string cislodokladuobjednavky = vydejSErow.SOPNUMBE.Trim();

                Datasets.DatabasePohoda pohodaDS = new Datasets.DatabasePohoda();
                Datasets.DatabasePohoda pohodaDS_2 = new Datasets.DatabasePohoda();


                oledbConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                //3) vytahnout polozky z skpp a skppol pro konkretni novy doklad
                //Datasets.DatabasePohodaTableAdapters.SKPPTableAdapter skppAdapter = new Datasets.DatabasePohodaTableAdapters.SKPPTableAdapter();
                //Datasets.DatabasePohodaTableAdapters.SKPPpolTableAdapter skpppolAdapter = new Datasets.DatabasePohodaTableAdapters.SKPPpolTableAdapter();
                //skppAdapter.Connection = oledbConnection;
                //skpppolAdapter.Connection = oledbConnection;

                //skppAdapter.FillByCislo(pohodaDS.SKPP, cislodokladuprijemka);
                Database.Pohoda.SKPP_FillByCislo(pohodaDS.SKPP, cislodokladuprijemka);
                //skpppolAdapter.FillByRefAg(pohodaDS.SKPPpol, pohodaDS.SKPP[0].ID);
                Database.Pohoda.SKPPpol_FillByRefAg(pohodaDS.SKPPpol, pohodaDS.SKPP[0].ID);

                //4) vytahnout polozky objednavky z pohody
                //Datasets.DatabasePohodaTableAdapters.OBJTableAdapter objAdapter = new Datasets.DatabasePohodaTableAdapters.OBJTableAdapter();
                //Datasets.DatabasePohodaTableAdapters.OBJpolTableAdapter objpolAdapter = new Datasets.DatabasePohodaTableAdapters.OBJpolTableAdapter();
                //objAdapter.Connection = oledbConnection;
                //objpolAdapter.Connection = oledbConnection;


                if (logovani)
                {
                    Fask.Logging.ExceptionHandler2.Handle(
                                   Logging.LogLevel.Debug,
                                   "zacatek logovani",
                                   "LoadPrijemImportResponseXMLAndMakeUpdateDB_Z_SI",
                                   $"cislodokladuobjednavky={cislodokladuobjednavky}");
                }


                //objAdapter.FillByCislo(pohodaDS.OBJ, cislodokladuobjednavky);
                Database.Pohoda.OBJ_FillByCislo(pohodaDS.OBJ, cislodokladuobjednavky);

                foreach (var row in pohodaDS.OBJ)
                {
                    if (logovani)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(
                                       Logging.LogLevel.Debug,
                                       "OBJ data",
                                       "Radek OBJ",
                                       $"ID={row.ID}, BDodano={row.BDodano}, TrvalyDok={row.TrvalyDok}");
                    }
                }

                //objpolAdapter.FillByRefAg(pohodaDS.OBJpol, pohodaDS.OBJ[0].ID);
                Database.Pohoda.OBJPol_FillByRefAg(pohodaDS.OBJpol, pohodaDS.OBJ[0].ID);


                if (logovani)
                {
                    Fask.Logging.ExceptionHandler2.Handle(
                                   Logging.LogLevel.Debug,
                                   "OBJPol_FillByRefAg",
                                   "pohodaDS.OBJpol",
                                   $"pohodaDS.OBJ[0].ID={pohodaDS.OBJ[0].ID}");
                }


                foreach (var row in pohodaDS.OBJpol)
                {
                    if (logovani)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(
                                       Logging.LogLevel.Debug,
                                       "OBJpol data",
                                       "Radek OBJpol",
                                       $"ID={row.ID}"); 
                    }
                }

                //5) sparovani ...

                // 5)a) dohledani karty zasoby a zmena mnozstvi hodnoty OBJV
                //Datasets.DatabasePohodaTableAdapters.SKzObjedVTableAdapter skzObjedVAdapter = new Datasets.DatabasePohodaTableAdapters.SKzObjedVTableAdapter();
                //skzObjedVAdapter.Connection = oledbConnection;
                //skzObjedVAdapter.ClearBeforeFill = false;

                //Datasets.DatabasePohodaTableAdapters.SKzBufTableAdapter skzBufAdapter = new Datasets.DatabasePohodaTableAdapters.SKzBufTableAdapter();
                //skzBufAdapter.Connection = oledbConnection;
                //skzBufAdapter.ClearBeforeFill = false;

                oledbConnection.Open();
                // transakce se resi na konci v ta managerovi ... 
                //System.Data.OleDb.OleDbTransaction oletrans = oledbConnection.BeginTransaction();

                foreach (Datasets.Vydej.CZMST_SIRow sir in vydejSIdt)
                {
                    //neprirazene polozky dle itemnmbr a mnozstvi

                    Datasets.DatabasePohoda.SKzDataTable SKzDT = new Datasets.DatabasePohoda.SKzDataTable();
                    Database.Pohoda.SKz_FillByID(SKzDT, int.Parse(sir.ITEMNMBR));

                    string query = " RefSKz=" + sir.ITEMNMBR.Trim() + " AND Mnozstvi=" + sir.QTYSHPPD.ToString("0.00", CultureInfo.InvariantCulture) + " AND RefPol is NULL AND RelAgID is NULL";

                    if (SKzDT.Count == 1)
                    {
                        var SKzRow = SKzDT.First();
                        int? resskzvc = SKzRow.IsRelSKzVCNull() ? (int?)null : SKzRow.RelSKzVC;

                        if (resskzvc.HasValue)
                        {
                            if (resskzvc > 0)
                            {
                                query = " RefSKz=" + sir.ITEMNMBR.Trim() +
                                    " AND Mnozstvi=" + sir.QTYSHPPD.ToString("0.00", CultureInfo.InvariantCulture) +
                                    " AND RefPol is NULL " +
                                    " AND RelAgID is NULL " +
                                    " AND VCislo = '" + sir.SERLTNUM.Trim() + "'";
                            }
                        }
                    }

                    //string query = " RefSKz=" + pir.ITEMNMBR.Trim() + " AND Mnozstvi=" + pir.QTYSHPPD.ToString("0.00", CultureInfo.InvariantCulture) + " AND RefPol is NULL AND RelAgID is NULL";


                    Datasets.DatabasePohoda.SKPPpolRow[] neprirazenePolozky = (Datasets.DatabasePohoda.SKPPpolRow[])pohodaDS.SKPPpol.Select(
                        query
                        , "ID"
                        , System.Data.DataViewRowState.CurrentRows);
                    if (neprirazenePolozky.Length == 0)
                    { //nic se nedeje, protoze nebyly nalezeny => log... jde o chybu ...
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PrijemPohodaXML", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo volnou shodu v databazi skpppol s polozkou:" + sir.ORD + "|" + sir.ITEMNMBR + "|" + sir.QTYSHPPD + "|" + sir.GUID.ToString());
                        continue; //pokracuji dalsim radkem ... 
                    }
                    else if (neprirazenePolozky.Length > 0)
                    { //je jich vice, no tak priradim prvni nalezenou, protoze je to v podstate uplne sumus...
                        //prirazeni se deje az po dohledani radku objednavky ...
                    }

                    //25.7.2025 MaR zakomentoval
                    Datasets.DatabasePohoda.OBJpolRow objpolr = pohodaDS.OBJpol.FindByID(sir.ORD);


                    #region logika 25.7.2025 MaR
                    if (logovani)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(
           Logging.LogLevel.Debug, "pohodaDS.OBJ[0].TrvalyDok",
           "Trvaly doklad",
           $"OBJ.TrvalyDok = {pohodaDS.OBJ[0].TrvalyDok}");


                        Fask.Logging.ExceptionHandler2.Handle(
    Logging.LogLevel.Debug, "Parovani",
    "pir hodnoty",
    $"pir.ORD = {sir.ORD}, pir.ITEMNMBR = {sir.ITEMNMBR}, pir.QTYSHPPD = {sir.QTYSHPPD}"); 
                    }

                    //Database.Pohoda.OBJPol_FillByID(pohodaDS_2.OBJpol, sir.ORD);
                    //Datasets.DatabasePohoda.OBJpolRow objpolr = null;
                    //if (pohodaDS_2.OBJpol != null)
                    //{
                    //    objpolr = pohodaDS_2.OBJpol.First();
                    //}
                    //else
                    //{
                    //    if (logovani)
                    //    {
                    //        Fask.Logging.ExceptionHandler2.Handle(
                    //                           Logging.LogLevel.Debug, "Parovani",
                    //                           "pir hodnoty v tabulce OBJpol", "je null!"); 
                    //    }
                    //} 
                    #endregion


                    if (objpolr != null)
                    {
                        //experimentalne zjistena hodnota pro dokladovou vazbu na vydanou objednavku ... 
                        // CHECK : !!! muze se v aktualizaci pohody zmenit !!!
                        neprirazenePolozky[0].RelAgID = 12;
                        neprirazenePolozky[0].RefPol = objpolr.ID;

                        if (logovani)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(
                                               Logging.LogLevel.Debug, "Prvni odpis",
                                               "objpolr zacatek", "neni null!");
                        }


                        // Pokud to je trvaly doklad, tak se mnozstvi na zasobe neponizuje ...
                        if (!pohodaDS.OBJ[0].TrvalyDok)
                        {
                            objpolr.Dodano += (double)sir.QTYSHPPD; // \TODO : zaokroulovani????
                            // 5)a) dohledani / dotazeni zbozi karty
                            int skzID = int.Parse(sir.ITEMNMBR);
                            Datasets.DatabasePohoda.SKzObjedVRow skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                            if (skzObjedVRow == null)
                            {
                                //skzObjedVAdapter.FillByID(pohodaDS.SKzObjedV, int.Parse(pir.ITEMNMBR));
                                Database.Pohoda.SKzObjedV_FillByID(pohodaDS.SKzObjedV, int.Parse(sir.ITEMNMBR));
                                skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                            }
                            if (skzObjedVRow == null)
                                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PrijemPohodaXML", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Nenalezena karta zasoby pro snizeni objednaneho mnozstvi:" + sir.ORD + "|" + sir.ITEMNMBR + "|" + sir.QTYSHPPD + "|" + sir.GUID.ToString());
                            else
                            {
                                if (logovani)
                                {
                                    Fask.Logging.ExceptionHandler2.Handle(
                                                       Logging.LogLevel.Debug, "pohodaDS.SKzObjedV.FindByID(skzID)",
                                                       "skzObjedVRow.ObjedV -= (double)pir.QTYSHPPD",
                                                        $"pir.QTYSHPPD = {sir.QTYSHPPD}, skzObjedVRow.ObjedV = {skzObjedVRow.ObjedV}");
                                }

                                skzObjedVRow.ObjedV -= (double)sir.QTYSHPPD; //pokud jiz bylo v cyklu drive snizeno, tak se snizi opet ...
                                
                              
                            }
                               
                        }
                        else
                        {
                            Fask.Logging.ExceptionHandler2.Handle(
                        Logging.LogLevel.Debug, "Parovani",
                        "Trvaly doklad", "mnozstvi na zasobe se neponizuje!");
                        }
                    }
                    else //je-li null, tak se nenaslo zbozi ... coz je chyba a bude zalogovana ...
                    {
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PrijemPohodaXML", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo volnou shodu v databazi objpol s polozkou:" + sir.ORD + "|" + sir.ITEMNMBR + "|" + sir.QTYSHPPD + "|" + sir.GUID.ToString());
                    }

                }

                // 5 b) zajistit oznaceni prenesene 
                // - preneseno se nastavuje jen kdyz je preneseno cele mnozstvi polozek ...
                // - projit polozky objednavky a pokud je u vsech polozek dodano >= mnozstvi tak nastavit
                bool bdodano = true;
                foreach (var item in pohodaDS.OBJpol)
                {
                    if (item.Dodano < item.Mnozstvi)
                    {
                        bdodano = false;
                        break;
                    }
                }
                pohodaDS.OBJ[0].BDodano = bdodano;

                if (logovani)
                {
                    Fask.Logging.ExceptionHandler2.Handle(
                                       Logging.LogLevel.Debug, "5 b) zajistit oznaceni prenesene",
                                       "pohodaDS.OBJ[0].BDodano",
                                        $"pohodaDS.OBJ[0].BDodano = {pohodaDS.OBJ[0].BDodano}");
                }

                // 5 c) nastaveni priznaku vyrizeno 
                // - musi se prepocitat hodnoty na skladove zasobe ObjedV
                // - a to jen tehdy, pokud neni objednavka Trvaly doklad
                if (!pohodaDS.OBJ[0].TrvalyDok && Globals_V1.Konfigurace.Prijem[0].Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit)
                {

                    if (logovani)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(
                                           Logging.LogLevel.Debug, "5 c) nastaveni priznaku vyrizeno ",
                                           "pohodaDS.OBJ[0].TrvalyDok && Globals_V1.Konfigurace.Prijem[0].Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit",
                                            $"zacatek logiky");
                    }

                    // 5.9.2018 : JiS - aktualizace hodnot ObjedV na karte zasob 
                    // Je-li jiz vyrizeno, tak nic nemenit...
                    bool vyrizeno = pohodaDS.OBJ[0].Vyrizeno;
                    if (!vyrizeno)
                    {
                        if (pohodaDS.OBJ[0].BDodano && Globals_V1.Konfigurace.Prijem[0].Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit)
                        {
                            pohodaDS.OBJ[0].Vyrizeno = true;

                            if (logovani)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(
                                                   Logging.LogLevel.Debug, "5 c) nastaveni priznaku vyrizeno",
                                                   "pohodaDS.OBJ[0].BDodano && Globals_V1.Konfigurace.Prijem[0].Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit",
                                                    $"pohodaDS.OBJ[0].Vyrizeno = true");
                            }

                        }
                        else if (Globals_V1.Konfigurace.Prijem[0].Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit_Castecne_Plneni) // neni uplne dodano
                        {

                            if (logovani)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(
                                                   Logging.LogLevel.Debug, "5 c) nastaveni priznaku vyrizeno ",
                                                   "Globals_V1.Konfigurace.Prijem[0].Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit_Castecne_Plneni",
                                                    $"zacatek logiky");
                            }


                            pohodaDS.OBJ[0].Vyrizeno = true;

                            // Pokud menim na vyrizeno, tak musim aktualizovat hodnoty mnozstvi na karte zasob v skz a skzBuf..???
                            // pro kazdou polozku objednavky

                            foreach (Datasets.DatabasePohoda.OBJpolRow item in pohodaDS.OBJpol)
                            {
                                // jeste neco zbyva dodat
                                // ??? co zaokrouhlovaci chyba??? (prevest na decimal 5desmist?)
                                if (item.Dodano < item.Mnozstvi)
                                {
                                    double zbyvadodat = item.Mnozstvi - item.Dodano; // toto se musi ponizit

                                    // 5)a) dohledani / dotazeni zbozi karty
                                    int skzID = item.RefSKz; //int.Parse(pir.ITEMNMBR);
                                    Datasets.DatabasePohoda.SKzObjedVRow skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                                    if (skzObjedVRow == null)
                                    {
                                        //skzObjedVAdapter.FillByID(pohodaDS.SKzObjedV, skzID); //pir.ITEMNMBR));
                                        Database.Pohoda.SKzObjedV_FillByID(pohodaDS.SKzObjedV, skzID); //pir.ITEMNMBR));
                                        skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                                    }
                                    if (skzObjedVRow == null)
                                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PrijemPohodaXML", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Nenalezena karta zasoby pro snizeni objednaneho mnozstvi:" + skzID + "| zbyva dodat: " + zbyvadodat);
                                    else
                                    {
                                        if (logovani)
                                        {
                                            Fask.Logging.ExceptionHandler2.Handle(
                                                               Logging.LogLevel.Debug, "c) nastaveni priznaku vyrizeno",
                                                               "Globals_V1.Konfigurace.Prijem[0].Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit_Castecne_Plneni",
                                                                $"skzObjedVRow.ObjedV = {skzObjedVRow.ObjedV}, zbyvadodat = {zbyvadodat}");
                                        }

                                        skzObjedVRow.ObjedV -= zbyvadodat; //(double)pir.QTYSHPPD; //pokud jiz bylo v cyklu drive snizeno, tak se snizi opet ...
                                    }
                                        
                                }
                                else
                                {
                                    if (logovani)
                                    {
                                        Fask.Logging.ExceptionHandler2.Handle(
                                                           Logging.LogLevel.Debug, "5 c) nastaveni priznaku vyrizeno ",
                                                           "Globals_V1.Konfigurace.Prijem[0].Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit_Castecne_Plneni",
                                                            $"prochazeni radku z tabulky pohodaDS.OBJpol if (item.Dodano < item.Mnozstvi) je else");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Fask.Logging.ExceptionHandler2.Handle(
                                              Logging.LogLevel.Debug, "pohodaDS.OBJ[0].Vyrizeno",
                                              " 5 c)", "vetev nic nevykona!");
                    }
                }
                else
                {
                    Fask.Logging.ExceptionHandler2.Handle(
                        Logging.LogLevel.Debug, "TrvalyDok && Pohoda_Objednavka_Vydana_Vyrizeno_Nastavit",
                        " 5 c)", "vetev nic nevykona!");
                }

                //// 5 d) dokonceni update hodnoty v SKzBuf...
                // ??? Je to vubec treba ??? k cemu je a jak se plni SKzBuf???
                foreach (var item in pohodaDS.SKzObjedV)
                {

                    if (logovani)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(
                                           Logging.LogLevel.Debug, "5 d) dokonceni update hodnoty v SKzBuf ",
                                           "foreach (var item in pohodaDS.SKzObjedV)",
                                            $"zacatek logiky");
                    }

                    //skzBufAdapter.FillByRefSkz(pohodaDS.SKzBuf, item.ID);
                    Database.Pohoda.SKzBuf_FillByRefSkz(pohodaDS.SKzBuf, item.ID);
                    var skzbufrows = pohodaDS.SKzBuf.Where(x => x.RefSKz == item.ID);
                    if (skzbufrows.Count() > 0)
                    {

                        if (logovani)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(
                                               Logging.LogLevel.Debug, "5 d) dokonceni update hodnoty v SKzBuf ",
                                               "Database.Pohoda.SKzBuf_FillByRefSkz(pohodaDS.SKzBuf, item.ID)",
                                                $"if (skzbufrows.Count() > 0)");
                        }

                        foreach (var skzbufrow in skzbufrows)
                        {
                            skzbufrow.ObjedV = item.ObjedV;
                        }
                    }
                    else
                    {
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PrijemPohodaXML", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo shodu v databazi skzbuf s polozkou:" + item.ID + "|" + item.ObjedV);
                    }
                }

                //7) update v transakci ... 

                //Datasets.DatabasePohodaTableAdapters.TableAdapterManager pohodaTaManager = new Datasets.DatabasePohodaTableAdapters.TableAdapterManager();
                //pohodaTaManager.OBJpolTableAdapter = objpolAdapter;
                //pohodaTaManager.SKPPpolTableAdapter = skpppolAdapter;
                //pohodaTaManager.OBJTableAdapter = objAdapter;
                //pohodaTaManager.SKzObjedVTableAdapter = skzObjedVAdapter;
                //pohodaTaManager.SKzBufTableAdapter = skzBufAdapter;

                //pohodaTaManager.Connection = oledbConnection;
                //transakce se otevira v managerovi ... 
                //pohodaTaManager.Connection.Open();
                //System.Data.IDbTransaction pohodaTransaction = pohodaTaManager.Connection.BeginTransaction();
                //pohodaTaManager.UpdateOrder = Datasets.DatabasePohodaTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
                //pohodaTaManager.UpdateAll(pohodaDS);

                System.Data.OleDb.OleDbTransaction trans = null;
                System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                try
                {

                    if (logovani)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(
                                           Logging.LogLevel.Debug, "7) update v transakci",
                                           "zacatek zapisu do tabulek pohoda",
                                            $"START");
                    }


                    connection.Open();
                    trans = connection.BeginTransaction();
                    Database.Pohoda.OBJPol_Update(pohodaDS, connection, trans);
                    Database.Pohoda.SKPPpol_Update(pohodaDS, connection, trans);
                    Database.Pohoda.OBJ_Update(pohodaDS, connection, trans);
                    Database.Pohoda.SKzObjedV_Update(pohodaDS, connection, trans);
                    Database.Pohoda.SKzBuf_Update(pohodaDS, connection, trans);


                    if (trans != null)
                        trans.Commit();


                    if (logovani)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(
                                           Logging.LogLevel.Debug, "7) update v transakci",
                                           "konec zapisu do tabulek pohoda commit" ,
                                            $"END");
                    }
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    try
                    {
                        if (trans != null)
                            trans.Rollback();
                    }
                    catch (Exception exx)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(exx);

                    }

                    throw ex;

                }


                return "OK";

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return ex.Message;
            }
            finally
            {
                if (oledbConnection != null && ((oledbConnection.State & ConnectionState.Open) == ConnectionState.Open))
                    oledbConnection.Close();
            }
        }

        #region IPrijem Members

        public Fask.Server.Interfaces.Classes.StatusInfo Prijem_GenerateDavka(
            Fask.Server.Interfaces.Classes.Objednavka objednavka, 
            Fask.Server.Interfaces.Classes.Sklad sklad
            )
        {
            //switch (statustoreturn)
            //{
            //    case 0: statusinfo = "OK"; break;
            //    case 1: statusinfo = "Již existuje"; break;
            //    case 2: statusinfo = "Neexistuje"; break;
            //    case 3: statusinfo = "Bylo nahráno"; break;
            //    default:
            //        statusinfo = "Neznámý status";
            //        break;
            //}

            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();
            string result = string.Empty;

            string pom = string.Empty;
            pom = Globals_V1.LoadConfiguration();
            if (pom != "OK")
                throw new Exception("Prijem_GenerateDavka, chyba nacteni konfigurace: " + pom);

            byte? czdoslo = Database.Prijem.CZMSTPE_PONUMBER_CZDOSLO(objednavka.ID);
            // 1) test na rozpracovany doklad 
            // 7.4.2016 PeV: oprava, probihalo generovani i v pripade, kdyz se jiz plnilo czmst_pe
            //if (czdoslo.HasValue && czdoslo.Value > 0)
            //if (czdoslo.HasValue)
            if (czdoslo.HasValue && czdoslo.Value > 0)
            { //existuje a je stazene v terminalu => vrati chybu ... nelze vytvorit duplicitu ...
                si.ID = -1;
                si.Description = "Existuje rozpracovaná dávka pro doklad '" + objednavka.ID + "' na terminálu č.:" + czdoslo.Value;
                si.InnerException = new Exception(si.Description);
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn,si.Description);
                return si;
            }

            // Dotazeni ID skladu
            if (String.IsNullOrEmpty(sklad.ID))
                sklad.ID = Globals_V1.Konfigurace.Prijem[0].HlavnySkladID.ToString();

            // 2) test zda je to objednavka vydana
            if (Database.Pohoda.OBJ_Exists(objednavka.ID))
            {
                // Test zda obsahuje pouze polozky pozadovaneho skladu
                if (!Database.Pohoda.OBJ_VYD_JedenSklad(objednavka.ID, sklad.ID))
                {
                    throw new Exception("Objednávka '" + objednavka.ID + "' obsahuje položky více skladů!");
                }

                string stav = this.ExportPrijemkaPohoda_Z_ObjednavkyVydane(objednavka);
                if (stav != "OK")
                {
                    si.ID = -10;
                    si.Description = stav;
                    si.InnerException = new Exception(si.Description);
                    return si;
                }
                else
                {
                    si.ID = int.Parse(objednavka.CisloDavky);
                    si.Description = "OK";
                    si.InnerException = null;
                }
            }
            else if (Database.Pohoda.Prevod_Exists(objednavka.ID, sklad.ID))    // test zda je to prevodka
            {
				// \TODO : Generuj prijemku z prevodky
                string stav = this.ExportPrijemkaPohoda_Z_Prevodky(objednavka);
                if (stav != "OK")
                {
                    si.ID = -11;
                    si.Description = stav;
                    si.InnerException = new Exception(si.Description);
                    return si;
                }
                else
                {
                    si.ID = int.Parse(objednavka.CisloDavky);
                    si.Description = "OK";
                    si.InnerException = null;
                }
            }
            else
            {
                si.Description = "Doklad '" + objednavka.ID + "' nenalezen.";
                if (sklad != null)
                    si.Description += "\nSklad " + sklad.ID;
                si.InnerException = new Exception(si.Description);
                si.ID = -12;
                throw new Exception(si.Description);
            }

            return si;
        }

        public Fask.DataSets.PrijemDavky Prijem_GetPrijemky(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        {
			// \TODO : seznam objednavek vydanych z sql serveru ... 
            //throw new NotImplementedException();
            try
            {
                //string select = "SELECT distinct [CountEntries], [PONUMBER]  FROM [CZMST_PE] WHERE CountEntries not in (Select CountEntries from czmst_pi) and CZ_Doslo <= 0 and SKL_ID LIKE '" + SQLInjection.Filter(prefixskladu) + "%' order by countentries";
                string select =
                    " select A.countentries, A.ponumber, B.CntItems CntItems, C.qtyshppdsum SumItems " +
                    " from " + TABLE_CZMST_PE + " A " +
                    " INNER JOIN " +
                    " ( " +
                    "	select X.countentries, X.ponumber, count(X.itemnmbr) CntItems " +
                    "	from ( " +
                    "		Select countentries, ponumber, itemnmbr " +
                    "		from " + TABLE_CZMST_PE + " " +
                    "       where qtypack=0" +
                    "		group by countentries, ponumber, itemnmbr " +
                    "	) X " +
                    "	group by X.countentries, X.ponumber " +
                    " ) B ON  " +
                    " A.countentries=B.CountEntries " +
                    " and A.ponumber=B.ponumber" +
                    " INNER JOIN " +
                    " ( " +
                    "	Select countentries, ponumber, " +
                    "		Sum(qtyshppd) as qtyshppdsum" +
                    "	from " + TABLE_CZMST_PE + " " +
                    "   where qtypack=0" +
                    "	group by countentries, ponumber " +
                    " ) C ON " +
                    " B.countentries=C.CountEntries  " +
                    " and B.ponumber=C.ponumber " +
                    " where " +
                    //" A.CountEntries not in (Select CountEntries from " + TABLE_CZMST_PI + ") " +
                    //" and " +
                    " (A.CZ_Doslo<=0 OR A.CZ_Doslo=" + terminal.ID + ")" +
                    " and A.SKL_ID LIKE '" + sklad.ID + "%' " +
                    " group by A.countentries, A.ponumber, B.CntItems, C.qtyshppdsum " +
                    " order by A.countentries ";

                Globals_V1.LoadConfiguration();

                System.Data.SqlClient.SqlDataAdapter xda = new System.Data.SqlClient.SqlDataAdapter(select, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                Fask.DataSets.PrijemDavky volneprijemky = new Fask.DataSets.PrijemDavky();
                ((DbDataAdapter)xda).Fill(volneprijemky, volneprijemky.Hlavicky.TableName);

                #region Rozsireni o dotazeni informace do infa z existujiciho pohledu detailu. vic neni mozne
                /* potlaceno ... docasne ???
                try
                {
                    bool prijemPrijemkaDetail2Hlavicka = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["PrijemPrijemkaDetail2Hlavicka"]);
                    if (prijemPrijemkaDetail2Hlavicka)
                    {
                        DataSet ds = Detail(string.Empty);
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataColumn dcol in ds.Tables[0].Columns)
                            {
                                volneprijemky.Hlavicky.Columns.Add(dcol.ColumnName, dcol.DataType);
                            }
                        }

                        if (volneprijemky.Hlavicky.Count > 0)
                        {
                            foreach (Fask.DataSets.PrijemDavky.HlavickyRow hrow in volneprijemky.Hlavicky)
                            {
                                try
                                {
                                    ds = Detail(hrow.PONUMBER);
                                    foreach (DataColumn dcol in ds.Tables[0].Columns)
                                    {
                                        DataColumn dcolhrow = hrow.Table.Columns[dcol.ColumnName];
                                        hrow.SetField<object>(dcolhrow, ds.Tables[0].Rows[0][dcol]);
                                    }
                                }
                                catch
                                { }
                            }
                        }
                        volneprijemky.AcceptChanges();
                    }

                }
                catch (Exception ex)
                {
                    Log.writeErrorLog(ex.Message);
                }
                */
                #endregion

                return volneprijemky;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(Sklad: " + sklad.ID + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        public Fask.DataSets.Prijem Prijem_GetPrijemka(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        {
            Fask.DataSets.Prijem prijem = new Fask.DataSets.Prijem();

            try
            {
				// \TODO : stahnout konkretni objednavku vydanou ...
                //throw new NotImplementedException();
                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    throw new Exception("Prijem_GenerateDavka, chyba nacteni konfigurace: " + pom);

                string select = "SELECT * FROM " + TABLE_CZMST_PE + " where CountEntries=" + davka.ID.Value + " order by dex_row_id";
                System.Data.SqlClient.SqlDataAdapter xda = new System.Data.SqlClient.SqlDataAdapter(select, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                //Nacteni dat prijemky z databaze
                
                ((DbDataAdapter)xda).Fill(prijem, prijem.CZMST_PE.TableName);

                select = "SELECT * FROM " + TABLE_CZMST_PI + " where CountEntries=" + davka.ID.Value + " order by dex_row_id";
                xda.SelectCommand.CommandText = select;

                ((DbDataAdapter)xda).Fill(prijem, prijem.CZMST_PI.TableName);
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
				Fask.Logging.ExceptionHandler2.Handle(prijem);

                throw ex;
            }

            return prijem;
        }

        public bool Prijem_GetPrijemkaReceived(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        {
			// \TODO : potvrdit stazeni ... 
            //throw new NotImplementedException();

            string pom = string.Empty;
            pom = Globals_V1.LoadConfiguration();
            if (pom != "OK")
                throw new Exception("Prijem_GenerateDavka, chyba nacteni konfigurace: " + pom);

            string selectCount = "SELECT Count(CountEntries) as davka FROM " + TABLE_CZMST_PE + " where CountEntries=" + davka.ID.Value + " AND (CZ_Doslo<=0 OR CZ_Doslo=" + terminal.ID + ")";
            string update = "Update " + TABLE_CZMST_PE + " set CZ_Doslo=" + terminal.ID + " where countentries=" + davka.ID.Value;
            System.Data.SqlClient.SqlConnection xconn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            System.Data.SqlClient.SqlCommand xcommAllowed = new System.Data.SqlClient.SqlCommand(selectCount, xconn);
            System.Data.SqlClient.SqlCommand xcomm = new System.Data.SqlClient.SqlCommand(update, xconn);
            System.Data.IDbTransaction itrans = null;

            int res = 0;
            try
            {
                xconn.Open();
                itrans = xconn.BeginTransaction(System.Data.IsolationLevel.Serializable);

                xcommAllowed.Transaction = (System.Data.SqlClient.SqlTransaction)itrans;
                object r = xcommAllowed.ExecuteScalar();
                if (r == null)
                    throw new Exception("Dávka nenalezena.");
                if (r is int && ((int)r) <= 0)
                    throw new Exception("Dávka se již zpracovává.");

                xcomm.Transaction = (System.Data.SqlClient.SqlTransaction)itrans;
                res = xcomm.ExecuteNonQuery();
                if (itrans != null)
                    itrans.Commit();

            }
            catch (Exception ex)
            {
                if (itrans != null)
                    itrans.Rollback();

				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (I1 : Dávka: " + davka.ID.Value + ", Terminál ID:" + terminal.ID + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);
                //throw ex;
                return false;
            }
            finally
            {
                if (xconn.State == System.Data.ConnectionState.Open)
                    xconn.Close();
            }

            return (res > 0);

        }

        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Process(
            Fask.Server.Interfaces.Classes.Davka davka, 
            Fask.Server.Interfaces.Classes.Terminal terminal, 
            Fask.Server.Interfaces.Classes.Sklad sklad, 
            Fask.Server.Interfaces.Classes.Item item, 
            Fask.DataSets.Prijem prijemdata, 
            Fask.Server.Interfaces.Prijem.ProcessState processPrijemState)
        {
            string pom = string.Empty;
            pom = Globals_V1.LoadConfiguration();
            if (pom != "OK")
                throw new Exception("Prijem_Process, chyba nacteni konfigurace: " + pom);

            TracId tracid = new TracId(null, terminal.ID, davka.ID.Value);

            string guidDavka = string.Empty;
            if (prijemdata.CZMST_PEH.Count > 0)
                guidDavka = prijemdata.CZMST_PEH[0].GUID.ToString();
            else
                guidDavka = Guid.NewGuid().ToString();
            string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals_V1.Konfigurace.Sdilene[0].StatusObjectsDirectory, guidDavka));

            //StatusObject so = new StatusObject(filePath);
            Fask.Server.Interfaces.Classes.StatusObject so = Fask.Server.Interfaces.Classes.StatusObject.Load(filePath);
            if (so == null) //neexistuje => vytvorit a pokracovat
            {
                so = new Fask.Server.Interfaces.Classes.StatusObject(filePath);
                so.Write("probiha zpracovani");
            }
            else if (so.Exists)
            {
                //return so; //pokud existuje, tak vraci informaci terminalu, at se rozhodne co s tim chce delat ...
                ////pokud se chce pokracovat znovu, tak se musi nejprve smazat

                if (so.Exception) // || so.Finished)
                { // nastala vyjimka pri zpracovani => umozni nasledne volat znovu...
                    so.Delete();
                    return so; // Po prvnim volani vrati informaci o chybe, pri druhem volani uz to pusti dal, protoze neexistuje ...
                }
                else
                { // probiha nebo bylo dokonceno uspesne => neumozni znovu zpracovat...
                    return so; // pokud neni ukoncen nebo neni chyba, tak jeste probiha a neni mozne znovu generovat ...
                }
            }
         
            //Fask.Server.Interfaces.Classes.StatusObject so = new Fask.Server.Interfaces.Classes.StatusObject();
            so.StatusText = "Příjem zpracování...";

            SqlTransaction trans = null;
            SqlConnection conn = null;
            bool uvolnitdavku = processPrijemState == Fask.Server.Interfaces.Prijem.ProcessState.Uvolnit;
            try
            {                
                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                conn.Open();

                if (uvolnitdavku) //uvolnit davku
                {
                    string updateuvolnit = "Update " + TABLE_CZMST_PE + " set CZ_Doslo=0 where CountEntries=" + davka.ID.Value;
                    System.Data.SqlClient.SqlCommand xcommand = new System.Data.SqlClient.SqlCommand(updateuvolnit, conn, trans);
                    int rows = xcommand.ExecuteNonQuery();
                }
                else //zapsat davku
                {

                    bool allowInsertData = true;
                    //Test zda je mozne data pridat, jestlize jiz existuji, tak nepridat. 
                    System.Data.SqlClient.SqlCommand xselect = new System.Data.SqlClient.SqlCommand("Select Count(*) as number from " + TABLE_CZMST_PI + " where countentries=" + davka.ID.Value, conn);
                    object datacount = xselect.ExecuteScalar();
                    if (datacount != null && ((int)datacount) > 0)
                    {
						Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "Data allready exist in database" + "(Prijem : Dávka " + davka.ID.Value + ")");
                        //return true;
                        allowInsertData = false;
                    }

                    if (allowInsertData)
                    {
                        //if (dexrowidInsert)
                        //{
                        //    int dexrowid = 0;
                        //    System.Data.SqlClient.SqlCommand xdexrowidmax = new System.Data.SqlClient.SqlCommand(
                        //        "Select MAX(DEX_ROW_ID) from " + TABLE_CZMST_PI,
                        //        conn,
                        //        trans
                        //        );
                        //    object maxdexrowid = xdexrowidmax.ExecuteScalar();
                        //    if (maxdexrowid != null && !(maxdexrowid is System.DBNull))
                        //        dexrowid = (int)maxdexrowid;

                        //    prijemdata.CZMST_PI.Columns["DEX_ROW_ID"].ReadOnly = false;

                        //    foreach (Fask.DataSets.Prijem.CZMST_PIRow pirow in prijemdata.CZMST_PI)
                        //    {
                        //        pirow.DEX_ROW_ID = ++dexrowid;
                        //    }
                        //}

                        //Update databaze
                        //dbTrans1 = connection1.BeginTransaction();
                        //string updatePE = "Update " + TABLE_CZMST_PE + " set CZ_Doslo=" + (terminal.ID + 100) + " where CountEntries=" + davka.ID.Value;
                        //System.Data.SqlClient.SqlCommand xcommand = new System.Data.SqlClient.SqlCommand(updatePE, connection1, dbTrans1);

                        //daPI.SelectCommand.Transaction = dbTrans1;
                        //daPI.InsertCommand.Transaction = dbTrans1;
                        //((System.Data.Common.DbDataAdapter)daPI).Update(prijemdata.CZMST_PI.Select(null, null, System.Data.DataViewRowState.Added));
                        trans = conn.BeginTransaction();
                        string updatePE = "Update " + TABLE_CZMST_PE + " set CZ_Doslo=" + (terminal.ID + 100) + " where CountEntries=" + davka.ID;
                        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatePE, conn, trans);

                        //Datasets.PrijemTableAdapters.CZMST_PITableAdapter dita = new Datasets.PrijemTableAdapters.CZMST_PITableAdapter();
                        //dita.Connection = conn;
                        //dita.Transaction = trans;
                        //dita.Update(prijemdata.CZMST_PI.Select(null, null, DataViewRowState.Added));
                         Database.Prijem.Update_CZMST_PI(prijemdata.CZMST_PI.Select(null, null, DataViewRowState.Added), conn, trans);

                        #region lokace
                        if (prijemdata.Parametry.Count > 0 && !prijemdata.Parametry.First().IsCONFIG_LOKACE_POVOLITNull() && prijemdata.Parametry.First().CONFIG_LOKACE_POVOLIT)
                        {
                            foreach (Fask.DataSets.Prijem.CZMST_PIRow pirow in prijemdata.CZMST_PI)
                            {
                                string countCommandText = "select count(*) from " + TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " where guid=@guid";
                                SqlCommand countCommand = new SqlCommand(countCommandText, conn, trans);
                                countCommand.Parameters.Clear();
                                countCommand.Parameters.AddWithValue("@guid", pirow.GUID);

                                int guidcount = (int)countCommand.ExecuteScalar();

                                if (guidcount % 2 == 0)
                                {
                                    var nazev = Database.Spolecne.GET_ITEMDESC_by_ITEMCODE(pirow.ITEMNMBR);

                                    DateTime dtnow = DateTime.Now;
                                    Fask.Server.Interfaces.Lokace.LokacePohyb pohybrow = new Fask.Server.Interfaces.Lokace.LokacePohyb();
                                    pohybrow.ITEMNMBR = pirow.ITEMNMBR;
                                    pohybrow.DOCUMENT_NUMBER = pirow.PONUMBER;
                                    pohybrow.POHYB_TYPE = Fask.Server.Interfaces.Lokace.TypeOfRecord.P;
                                    pohybrow.POHYB_SRC = "P";
                                    pohybrow.SOURCE = "S";      // doplneni ze serveru ...
                                    pohybrow.QTYSHPPD = pirow.QTYSHPPD;
                                    pohybrow.SERLTNUM = pirow.SERLTNUM;
                                    pohybrow.SKL_ID_SRC = pirow.IsSKL_IDNull() ? string.Empty : pirow.SKL_ID;
                                    pohybrow.SKL_ID_DST = string.Empty;
                                    pohybrow.LOCNCODE_SRC = pirow.IsLOCNCODENull() ? string.Empty : pirow.LOCNCODE;
                                    pohybrow.LOCNCODE_DST = string.Empty;
                                    pohybrow.UserID = pirow.USER_ID;
                                    pohybrow.TermID = terminal.ID;
                                    pohybrow.guid = pirow.GUID;
                                    pohybrow.Expiration = pirow.IsExpiraceNull() ? (DateTime?)null : pirow.Expirace;
                                    pohybrow.ITEMDESC = nazev;   // dotahnout nazev??  5.5.2021 TaD konečne se dotahuje nazev
                                    pohybrow.CountEntries = pirow.CountEntries;
                                    pohybrow.dateeveS = dtnow;
                                    if (pirow.IsDATEDONENull() || pirow.IsTIMEDONENull())
                                        pohybrow.dateeveS = dtnow;
                                    else
                                        pohybrow.dateeveT = DateTime.ParseExact(pirow.DATEDONE + " " + pirow.TIMEDONE, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);

                                    ProcessPrijem(pohybrow, new SqlCommand(), conn, trans, new SqlDataAdapter());

                                }
                            }
                        }
                        #endregion

                        daPIH.SelectCommand.Transaction = trans;
                        daPIH.InsertCommand.Transaction = trans;

                        if (prijemdata.CZMST_PIH.Count > 0)
                        { 
                            daPIH.Update(prijemdata.CZMST_PIH.Select(null, null, System.Data.DataViewRowState.Added));
                        }

                        int rows = command.ExecuteNonQuery();

                        if (trans != null)
                            trans.Commit();
                    }

                    // Pokud je vicenasobne odeslani, presto se pokusit o provedeni importu do pohody ...
                    if (prijemdata.CZMST_PI.Count > 0)
                    {
                        //tady odeslat do pohody ... jako novou prijemku ...

                        int userID = 0;
                        try
                        {
                            userID = prijemdata.CZMST_PI[0].USER_ID;
                        }
                        catch { }

                        if (Database.Pohoda.OBJ_Exists(prijemdata.CZMST_PI[0].PONUMBER))
                        {
                            string vysledek = this.ImportPrijemkaPohoda(davka.ID.Value, userID);
                            if (vysledek != "OK")
                            {
                                so.StatusText = vysledek;
                                so.Exception = true;
                                so.Write();
                                return so;
                            }

                            if (prijemdata.CZMST_PE.Count > 0)
                            {

                                var dataTrack = prijemdata.CZMST_PE.Where(x => x.CZ_SerNum_Track == 10 );

                                if (dataTrack.Count() > 0)
                                {

                                    System.Data.OleDb.OleDbTransaction transPOH = null;
                                    System.Data.OleDb.OleDbConnection connPOH = null;

                                    try
                                    {
                                        using (connPOH = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB))
                                        {
                                            connPOH.Open();
                                            transPOH = connPOH.BeginTransaction();

                                            foreach (Fask.DataSets.Prijem.CZMST_PIRow pol in prijemdata.CZMST_PI)
                                            {
                                                if (!pol.IsAttributeToSNNull() && !string.IsNullOrEmpty(pol.AttributeToSN))
                                                {
                                                    Database.Pohoda.Update_SKzVC(connPOH, transPOH, pol.ITEMNMBR, pol.SERLTNUM, pol.AttributeToSN);
                                                }

                                                if (!pol.IsExpiraceNull())
                                                {
                                                    Database.Pohoda.Update_SKzVC(connPOH, transPOH, pol.ITEMNMBR, pol.SERLTNUM, pol.Expirace);
                                                }
                                            }

                                            if (transPOH != null)
                                                transPOH.Commit();
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        if (transPOH != null)
                                            transPOH.Rollback();

                                        throw ex;
                                    }
                                    
                                }
                            }
                        }
                        //else if  prevodka...
                        // else ... doklad nenalezen ...
                    } //// else nic nebylo nasnimano zalogovat ...

                }

                //Log.writeOKData(prijemdata, countentries + "." + idterminal + "." + "prijem");
                so.SetOK();
                return so;
            }
            catch (Exception ex)
            {
                // StatusObject
                so.SetException(ex);
                so.Finished = true;

                if (trans != null)
                    trans.Rollback();

				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error,"(ProcessPrijemState: '" + processPrijemState.ToString() + "', davka:'" + davka.ID.Value + "')" );
				Fask.Logging.ExceptionHandler2.Handle(ex);

                throw ex;
                //processStatus.StatusText = "Nastala chyba";
                //return processStatus;
            }
            finally
            {
                so.Write();
                if ((conn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool Prijem_AfterProcessedAction(Fask.Server.Interfaces.Classes.Davka davka)
        {
            // Akce po provedeni ulozeni dat ...
            // Momentalne neni reseno ...
            //throw new NotImplementedException();
            return true;
        }

        public System.Data.DataSet Prijem_Detail(Fask.Server.Interfaces.Classes.Objednavka objednavka, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            try
            {
                //string selectstr = 
                //    "SELECT" +
                //    " o.Cislo 'ocislo'" +
                //    " ,z.Cislo 'zcislo' " +
                //    " ,z.SText 'zstext' " +
                //    " ,c.ID 'cid'" +
                //    " ,c.IDS 'cids'" +
                //    " ,c.SText 'cstext'" +
                //    " from OBJ o" +
                //    " left join sZAK z on z.Cislo = o.CisloZAK" +
                //    " left join sCIN c on c.ID = o.RefCin" +
                //    " where o.Cislo=?";
                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    throw new Exception("Prijem_Detail, chyba nacteni konfigurace: " + pom);

                string selectstr = Globals_V1.Konfigurace.Prijem[0].ObjednavkaDetail.Trim();

                System.Data.DataSet ds = new System.Data.DataSet();
                System.Data.OleDb.OleDbDataAdapter oledb_da = new System.Data.OleDb.OleDbDataAdapter(
                    selectstr,
                    Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                oledb_da.SelectCommand.Parameters.AddWithValue("?", objednavka.ID);

                oledb_da.Fill(ds);
                return ds;

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(Prijem_Detail: " + objednavka.ID + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        public System.Data.DataSet Prijem_Detail_Polozka(Fask.Server.Interfaces.Classes.Objednavka objednavka, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item polozka)
        {
            try
            {
                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    throw new Exception("Prijem_Detail_Polozka, chyba nacteni konfigurace: " + pom);

                string selectstr = Globals_V1.Konfigurace.Prijem[0].ObjednavkaDetailPolozka.Trim();

                System.Data.DataSet ds = new System.Data.DataSet();
                System.Data.OleDb.OleDbDataAdapter oledb_da = new System.Data.OleDb.OleDbDataAdapter(
                    selectstr,
                    Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                oledb_da.SelectCommand.Parameters.AddWithValue("?", objednavka.ID);
                oledb_da.SelectCommand.Parameters.AddWithValue("?", polozka.ID);
                oledb_da.SelectCommand.Parameters.AddWithValue("?", polozka.Order);

                oledb_da.Fill(ds);
                return ds;

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(Prijem_Detail_Polozka: " + objednavka.ID + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);
               
                throw ex;
            }
        }

        public System.Data.DataSet Prijem_DetailDavka(Fask.Server.Interfaces.Classes.Davka davka)
        {
            //throw new NotImplementedException();
            return null;
        }

        #endregion

        #region IWebControl Members

        /*
        private List<TreeNode> actionsPrijem = null;
        private PlaceHolder phPrijem = null;
        private Button btnStornoPrijemka = null;
        private Label lblbStornoPrijemka = null;
        private TextBox txtPrijemka = null;

        List<TreeNode> Fask.Server.Interfaces.WebControl.IWebControl.getActions(Fask.Server.Interfaces.Classes.User uzivatel)
        {
            if (actionsPrijem == null)
            {
                actionsPrijem = new List<TreeNode>();

                TreeNode tn = new TreeNode();
                tn.Value = "Stornovat příjemku";
                tn.Text = "Stornovat příjemku";
                actionsPrijem.Add(tn);
            }

            return actionsPrijem;
        }

        object Fask.Server.Interfaces.WebControl.IWebControl.getAction(TreeNode selectedAction, System.Web.UI.Page page)
        {
            if (selectedAction.Value == "Stornovat příjemku")
            {
                #region export
                if (phPrijem == null)
                {
                    phPrijem = new PlaceHolder();

                    txtPrijemka = new TextBox();
                    phPrijem.Controls.Add(txtPrijemka);

                    btnStornoPrijemka = new Button();
                    btnStornoPrijemka.OnClientClick = "if (! confirm('Opravdu chcete danou příjemku stornovat?')) return false;";
                    btnStornoPrijemka.Click += new EventHandler(btnStornoPrijemka_Click);
                    btnStornoPrijemka.Text = "Stornovat příjemku";
                    phPrijem.Controls.Add(btnStornoPrijemka);
                    btnStornoPrijemka.ID = "btnStornoPrijemka";

                    phPrijem.Controls.Add(new LiteralControl("<br />"));

                    lblbStornoPrijemka = new Label();
                    lblbStornoPrijemka.ID = "lblbStornoPrijemka";
                    phPrijem.Controls.Add(lblbStornoPrijemka);

                }
                else
                {
                    btnStornoPrijemka.Click -= new EventHandler(btnStornoPrijemka_Click);
                    btnStornoPrijemka.Click += new EventHandler(btnStornoPrijemka_Click);
                }
                return phPrijem;

                #endregion
            }
            
           
            return null;
        }
        */

        void btnStornoPrijemka_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion


        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Storno_Prijemka(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, string password)
        {
            Fask.Server.Interfaces.Classes.StatusObject so = new Fask.Server.Interfaces.Classes.StatusObject();
            string pom = string.Empty;
            pom = Globals_V1.LoadConfiguration();
            if (pom != "OK")
                throw new Exception("Prijem_Storno_Prijemka, chyba nacteni konfigurace: " + pom);

            if (password != Globals_V1.Konfigurace.Prijem[0].HesloStornoPrijemka)
            {
                so.StatusText = "Zadané heslo je špatně!";
                so.Exception = true;
                return so;
            }

            
            Datasets.PrijemTableAdapters.CZMST_PETableAdapter pe_ta = null;

            try
            {
                
                    if (Database.Prijem.GETDATA_CZMSTPI_DT(davka.ID.Value).Count > 0)
                    {
                    so.StatusText = "Danou dávku nelze zrušit! Existují nasnímané položky!";
                    so.Exception = true;
                    return so;
                }



                pe_ta = new Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
                pe_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                pe_ta.UpdateCzDosloByCountEntries(100, davka.ID.Value);

                so.StatusText = "OK";
                return so;

            }
            finally
            {
                if (pe_ta != null && pe_ta.Connection.State == System.Data.ConnectionState.Open)
                    pe_ta.Connection.Close();
            }


            //return so;

        }

        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Finish_Prijemka(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, string password)
        {
            Fask.Server.Interfaces.Classes.StatusObject so = new Fask.Server.Interfaces.Classes.StatusObject();
            so.StatusText = "";

            try
            {
                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    throw new Exception("Prijem_Finish_Prijemka, chyba nacteni konfigurace: " + pom);

                Datasets.PrijemTableAdapters.CZMST_PETableAdapter pe_ta = null;
                pe_ta = new Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
                pe_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                pe_ta.UpdateCzDosloByCountEntries((byte)(terminal.ID + 100), davka.ID.Value);

                so.SetOK();
            }
            catch (Exception e)
            {
                so.Exception = true;
                so.StatusText = e.Message;
            }

            #region zakomentovany kod prevzaty z HeO ...
            //if (password != Properties.Settings.Default.PrijemPassword)
            //{
            //    so.Exception = true;
            //    so.StatusText = "Heslo není zadáno správně";
            //    return so;
            //}

            //System.Data.SqlClient.SqlConnection dbconnection = null;
            //try
            //{
            //    dbconnection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.ConnectionString);
            //    System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();
            //    dbCommand.Connection = dbconnection;
            //    dbCommand.CommandType = CommandType.Text;
            //    dbCommand.CommandText = "Update " + TABLE_CZMST_PE +
            //        " set cz_doslo=" + (terminal.ID + 100) + " where countentries=@davka";
            //    dbCommand.Parameters.AddWithValue("@davka", davka.ID.Value);

            //    dbconnection.Open();
            //    dbCommand.ExecuteNonQuery();
            //    dbconnection.Close();

            //    so.SetOK();

            //}
            //catch (Exception e)
            //{
            //    so.Exception = true;
            //    so.StatusText = e.Message;
            //}
            //finally
            //{
            //    if (dbconnection != null && (dbconnection.State & ConnectionState.Open) == ConnectionState.Open)
            //        dbconnection.Close();
            //}
            #endregion

            return so;
        }

        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Online_Add(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.DataSets.Prijem prijemRows)
        {
            throw new NotImplementedException();
        }

        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Online_Del(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.DataSets.Prijem prijemRows)
        {
            throw new NotImplementedException();
        }

        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Online_Quantity(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.DataSets.Prijem prijemRows)
        {
            throw new NotImplementedException();
        }

        public Fask.Server.Interfaces.Classes.StatusOverLokace Prijem_Online_OverLokace(string serltnum, string itemnmbr, string locncode, decimal qtyshppd, string skl_id)
        {
            return Lokace_OverLokace(skl_id, locncode);
        }

        #region IPrijem Members


        public Fask.Server.Interfaces.DataSets.Obecne Prijem_GetPrijemky_External(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item, List<string> ListCarKod)
        {
            //Generovat 
            Fask.Server.Interfaces.DataSets.Obecne ds_obecne = new Fask.Server.Interfaces.DataSets.Obecne();
            Fask.DataSets.Prijem ds_prijem = new Fask.DataSets.Prijem();
            //DataSet data = new DataSet();

            Globals_V1.LoadConfiguration();

            try
            {
                //Volani funkce na SQL serveru...
                #region SQL 
		System.Data.SqlClient.SqlConnection dbconnection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();
                dbCommand.CommandType = CommandType.Text;

                dbCommand.CommandText = "Select * from CZMST_PE where cz_doslo <= 100"; // jen pripravene nebo stazene se budou vylucovat


                //dbCommand.Parameters.Add(new SqlParameter("@TerminalID", terminal.ID));
                //dbCommand.Parameters.Add(new SqlParameter("@SkladID", sklad.ID));
                //dbCommand.Parameters.Add(new SqlParameter("@CarKod", String.Join(",", ListCarKod.ToArray())));

                dbCommand.Connection = dbconnection;
                System.Data.SqlClient.SqlDataAdapter dbda = new System.Data.SqlClient.SqlDataAdapter(dbCommand);

                dbda.Fill(ds_prijem, ds_prijem.CZMST_PE.TableName);

                //Logging.Log.writeErrorLog("Pocet radku Prijem:" + ds_prijem.CZMST_PE.Count.ToString());


                #endregion

				#region FASK_GetDavkyByCarKody

				#region OLE DB

				//System.Data.OleDb.OleDbDataAdapter oledb_da = new System.Data.OleDb.OleDbDataAdapter(
				//"Select * from dbo.FASK_GetDavkyByCarKody(?,?,?)",
				//Globals.ConnectionStringPohodaDB);

				//oledb_da.SelectCommand.Parameters.AddWithValue("?", terminal.ID);
				//oledb_da.SelectCommand.Parameters.AddWithValue("?", sklad.ID);
				//oledb_da.SelectCommand.Parameters.AddWithValue("?", String.Join(",", ListCarKod.ToArray()));

				//oledb_da.Fill(ds_obecne, ds_obecne.Prijemky.TableName);
				
				#endregion

				#region SQL  1.2.2019 HANIBAL

				System.Data.SqlClient.SqlDataAdapter SQL_da = new System.Data.SqlClient.SqlDataAdapter(
					"Select * from dbo.FASK_GetDavkyByCarKody(@TerminalID,@SkladID,@CarKod)",
                    Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

				SQL_da.SelectCommand.Parameters.AddWithValue("@TerminalID", terminal.ID);
				SQL_da.SelectCommand.Parameters.AddWithValue("@SkladID", sklad.ID);
				SQL_da.SelectCommand.Parameters.AddWithValue("@CarKod", String.Join(",", ListCarKod.ToArray()));

				SQL_da.Fill(ds_obecne, ds_obecne.Prijemky.TableName);


				#region

				//Logging.Log.writeErrorLog("Pocet radku Obecne:" + ds_obecne.Prijemky.Count.ToString());
				
				#endregion



				#endregion

				//var ListList = ds_prijem.CZMST_PE.GroupBy(x => x.PONUMBER).Select(group => group.ToList()).ToList();

                //foreach (List<Fask.DataSets.Prijem.CZMST_PERow> List in ListList)
                //{
                //    foreach (Fask.DataSets.Prijem.CZMST_PERow Row in List)
                //    {
                //        bool tmp = ds_obecne.Prijemky.Any(x => x.PONUMBER == Row.PONUMBER);


                //    }
                //}

                //Logging.Log.writeErrorLog("pocet radku pred foreach " + ds_obecne.Prijemky.Count.ToString());


                ds_obecne.Prijemky.ToList().ForEach(row =>
                {
                    //Logging.Log.writeErrorLog("Pocet radku Hodnota :" + row.PONUMBER.Trim());

                    if (ds_prijem.CZMST_PE.Any(rowp => rowp.PONUMBER.Trim() == row.PONUMBER.Trim()))
                    {
                        //Logging.Log.writeErrorLog("SMAZANA:" + row.PONUMBER.Trim());
                        row.Delete();
                    }
                });

                //Logging.Log.writeErrorLog("Pocet radku pred foreach " + ds_obecne.Prijemky.Count.ToString());

                //foreach (Fask.Server.Interfaces.DataSets.Obecne.PrijemkyRow Row in ds_obecne.Prijemky)
                //{
                //    Logging.Log.writeErrorLog("PrijemkyRow:" + Row.PONUMBER.Trim());

                //    if (ds_prijem.CZMST_PE.Any(x => x.PONUMBER.Trim() == Row.PONUMBER.Trim()))
                //    {
                //        Row.Delete();
                //        Logging.Log.writeErrorLog("Mazu:" + Row.PONUMBER.Trim());
                //    }
                //}

                //Logging.Log.writeErrorLog("pocet radku pred acept " + ds_obecne.Prijemky.Count.ToString());
                ds_obecne.Prijemky.AcceptChanges();
                //Logging.Log.writeErrorLog("pocet radku po acept " + ds_obecne.Prijemky.Count.ToString());  
                
                #endregion
            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				Logging.ExceptionHandler2.Handle(ds_obecne);
				Logging.ExceptionHandler2.Handle(ds_prijem);
                
                throw ex;
            }

            return ds_obecne;

        }

    

        #region IPrijem Members


        //public DataSet Prijem_GetSkladExpedice(string ITEMNMBR, string nasnimano, string odvedeno, string SERLTNUM)
        public bool Prijem_GetSkladExpedice(
            string ITEMNMBR,
            decimal MnozstviZadane,
            decimal MnozstviNasnimane,
            out decimal MnozstviDodavatelePozadovano,
            out decimal MnozstviDodavateleDodano,
            out decimal MnozstviDodavateleDodat,
            out decimal MnozstviOdberateliPozadovano,
            out decimal MnozstviOdberatelumDodano,
            out decimal MnozstviOdberatelumDodat,
            out decimal Vysledek
            )    
    {
        MnozstviDodavatelePozadovano =
        MnozstviDodavateleDodano =
        MnozstviDodavateleDodat =
        MnozstviOdberateliPozadovano =
        MnozstviOdberatelumDodano =
        MnozstviOdberatelumDodat =
        Vysledek = 0;

		#region Puvodne OLEDB
		//try
		//{
		//    System.Data.OleDb.OleDbConnection dbconnection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);
		//    System.Data.OleDb.OleDbCommand dbCommand = new System.Data.OleDb.OleDbCommand();
		//    dbCommand.CommandType = (CommandType)Enum.Parse(typeof(CommandType), Properties.Settings.Default.CommandType);
		//    dbCommand.Connection = dbconnection;

		//    string prijemkadetail = Properties.Settings.Default.PrijemkaGetSkladExpedice;
		//    string prijemkadetail_paramname_IDPozlozky = Properties.Settings.Default.PrijemkaGetSkladExpedice_paramName_IDPozlozky;
		//    string prijemkadetail_paramname_CountNasnimano = Properties.Settings.Default.PrijemkaGetSkladExpedice_paramName_CountNasnimano;
		//    string prijemkadetail_paramname_CountZadane = Properties.Settings.Default.PrijemkaGetSkladExpedice_paramName_CountOdvedeno;



		//    if (dbCommand.CommandType == CommandType.StoredProcedure)
		//    {
		//        dbCommand.CommandText = prijemkadetail;

		//        var pItemnmbr = dbCommand.Parameters.AddWithValue(prijemkadetail_paramname_IDPozlozky, ITEMNMBR.Trim());
		//        //var pItemnmbr = dbCommand.Parameters.AddWithValue(prijemkadetail_paramname_IDPozlozky, ITEMNMBR);
		//        var pMnozstviZadane = dbCommand.Parameters.AddWithValue(prijemkadetail_paramname_CountZadane, MnozstviZadane);
		//        var pMnozstviNasnimano = dbCommand.Parameters.AddWithValue(prijemkadetail_paramname_CountNasnimano, MnozstviNasnimane);

		//        var pMnozstviDodavatelePozadovano = dbCommand.Parameters.AddWithValue("?", MnozstviDodavatelePozadovano);
		//        var pMnozstviDodavateleDodano = dbCommand.Parameters.AddWithValue("?", MnozstviDodavateleDodano);
		//        var pMnozstviDodavateleDodat = dbCommand.Parameters.AddWithValue("?", MnozstviDodavateleDodat);
		//        var pMnozstviOdberateliPozadovano = dbCommand.Parameters.AddWithValue("?", MnozstviOdberateliPozadovano);
		//        var pMnozstviOdberatelumDodano = dbCommand.Parameters.AddWithValue("?", MnozstviOdberatelumDodano);
		//        var pMnozstviOdberatelumDodat = dbCommand.Parameters.AddWithValue("?", MnozstviOdberatelumDodat);
		//        var pVysledek = dbCommand.Parameters.AddWithValue("?", Vysledek);

		//        pMnozstviDodavatelePozadovano.Direction = ParameterDirection.Output;
		//        pMnozstviDodavateleDodano.Direction = ParameterDirection.Output;
		//        pMnozstviDodavateleDodat.Direction = ParameterDirection.Output;
		//        pMnozstviOdberateliPozadovano.Direction = ParameterDirection.Output;
		//        pMnozstviOdberatelumDodano.Direction = ParameterDirection.Output;
		//        pMnozstviOdberatelumDodat.Direction = ParameterDirection.Output;
		//        pVysledek.Direction = ParameterDirection.Output;

		//        pItemnmbr.OleDbType = System.Data.OleDb.OleDbType.VarChar;
		//        pItemnmbr.Precision = 31;
		//        pItemnmbr.Scale = 0;

		//        pMnozstviZadane.OleDbType = System.Data.OleDb.OleDbType.Numeric;
		//        pMnozstviZadane.Precision = 19;
		//        pMnozstviZadane.Scale = 5;
		//        pMnozstviZadane.Size = 4;

		//        pMnozstviNasnimano.OleDbType = System.Data.OleDb.OleDbType.Numeric;
		//        pMnozstviNasnimano.Precision = 19;
		//        pMnozstviNasnimano.Scale = 5;
		//        pMnozstviNasnimano.Size = 4;

		//        pMnozstviDodavatelePozadovano.OleDbType = System.Data.OleDb.OleDbType.Numeric;
		//        pMnozstviDodavatelePozadovano.Precision = 19;
		//        pMnozstviDodavatelePozadovano.Scale = 5;
		//        pMnozstviDodavatelePozadovano.Size = 4;

		//        pMnozstviDodavateleDodano.OleDbType = System.Data.OleDb.OleDbType.Numeric;
		//        pMnozstviDodavateleDodano.Precision = 19;
		//        pMnozstviDodavateleDodano.Scale = 5;
		//        pMnozstviDodavateleDodano.Size = 4;

		//        pMnozstviDodavateleDodat.OleDbType = System.Data.OleDb.OleDbType.Numeric;
		//        pMnozstviDodavateleDodat.Precision = 19;
		//        pMnozstviDodavateleDodat.Scale = 5;
		//        pMnozstviDodavateleDodat.Size = 4;

		//        pMnozstviOdberateliPozadovano.OleDbType = System.Data.OleDb.OleDbType.Numeric;
		//        pMnozstviOdberateliPozadovano.Precision = 19;
		//        pMnozstviOdberateliPozadovano.Scale = 5;
		//        pMnozstviOdberateliPozadovano.Size = 4;

		//        pMnozstviOdberatelumDodano.OleDbType = System.Data.OleDb.OleDbType.Numeric;
		//        pMnozstviOdberatelumDodano.Precision = 19;
		//        pMnozstviOdberatelumDodano.Scale = 5;
		//        pMnozstviOdberatelumDodano.Size = 4;

		//        pMnozstviOdberatelumDodat.OleDbType = System.Data.OleDb.OleDbType.Numeric;
		//        pMnozstviOdberatelumDodat.Precision = 19;
		//        pMnozstviOdberatelumDodat.Scale = 5;
		//        pMnozstviOdberatelumDodat.Size = 4;

		//        pVysledek.OleDbType = System.Data.OleDb.OleDbType.Numeric;
		//        pVysledek.Precision = 19;
		//        pVysledek.Scale = 5;
		//        pVysledek.Size = 4;

		//        dbCommand.Connection.Open();

		//        //dbCommand.Prepare();

		//        dbCommand.ExecuteNonQuery();
		//        dbCommand.Connection.Close();

		//        MnozstviDodavatelePozadovano = (decimal)pMnozstviDodavatelePozadovano.Value;
		//        MnozstviDodavateleDodano = (decimal)pMnozstviDodavateleDodano.Value;
		//        MnozstviDodavateleDodat = (decimal)pMnozstviDodavateleDodat.Value;
		//        MnozstviOdberateliPozadovano = (decimal)pMnozstviOdberateliPozadovano.Value;
		//        MnozstviOdberatelumDodano = (decimal)pMnozstviOdberatelumDodano.Value;
		//        MnozstviOdberatelumDodat = (decimal)pMnozstviOdberatelumDodat.Value;
		//        Vysledek = (decimal)pVysledek.Value; 



		//        return true;
		//    }
		//    else
		//    {
		//        throw new Exception("Neznámý typ příkazu: " + dbCommand.CommandType.ToString());
		//    }

		//    return true;
		//}
		//catch (Exception ex)
		//{

		//    throw ex;
		//} 
		#endregion

		#region SQL  1.2.2019 HANIBAL

		try
		{
			System.Data.SqlClient.SqlConnection SQLcon = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();
			dbCommand.CommandType = CommandType.StoredProcedure;
			dbCommand.Connection = SQLcon;

			if (dbCommand.CommandType == CommandType.StoredProcedure)
			{
				dbCommand.CommandText = "FASK_PrijemGetSkladExpedice";

				var pItemnmbr = dbCommand.Parameters.AddWithValue("@Itemnmbr", ITEMNMBR.Trim());
				//var pItemnmbr = dbCommand.Parameters.AddWithValue(prijemkadetail_paramname_IDPozlozky, ITEMNMBR);
				var pMnozstviZadane = dbCommand.Parameters.AddWithValue("@MnozstviZadane", MnozstviZadane);
				var pMnozstviNasnimano = dbCommand.Parameters.AddWithValue("@MnozstviNasnimane", MnozstviNasnimane);

				var pMnozstviDodavatelePozadovano = dbCommand.Parameters.AddWithValue("@MnozstviDodavatelePozadovano", MnozstviDodavatelePozadovano);
				var pMnozstviDodavateleDodano = dbCommand.Parameters.AddWithValue("@MnozstviDodavateleDodano", MnozstviDodavateleDodano);
				var pMnozstviDodavateleDodat = dbCommand.Parameters.AddWithValue("@MnozstviDodavateleDodat", MnozstviDodavateleDodat);
				var pMnozstviOdberateliPozadovano = dbCommand.Parameters.AddWithValue("@MnozstviOdberateliPozadovano", MnozstviOdberateliPozadovano);
				var pMnozstviOdberatelumDodano = dbCommand.Parameters.AddWithValue("@MnozstviOdberatelumDodano", MnozstviOdberatelumDodano);
				var pMnozstviOdberatelumDodat = dbCommand.Parameters.AddWithValue("@MnozstviOdberatelumDodat", MnozstviOdberatelumDodat);
				var pVysledek = dbCommand.Parameters.AddWithValue("@Vysledek", Vysledek);

				pMnozstviDodavatelePozadovano.Direction = ParameterDirection.Output;
				pMnozstviDodavateleDodano.Direction = ParameterDirection.Output;
				pMnozstviDodavateleDodat.Direction = ParameterDirection.Output;
				pMnozstviOdberateliPozadovano.Direction = ParameterDirection.Output;
				pMnozstviOdberatelumDodano.Direction = ParameterDirection.Output;
				pMnozstviOdberatelumDodat.Direction = ParameterDirection.Output;
				pVysledek.Direction = ParameterDirection.Output;

				pItemnmbr.SqlDbType = System.Data.SqlDbType.VarChar;
				pItemnmbr.Precision = 31;
				pItemnmbr.Scale = 0;

				pMnozstviZadane.SqlDbType = System.Data.SqlDbType.Decimal;
				pMnozstviZadane.Precision = 19;
				pMnozstviZadane.Scale = 5;
				pMnozstviZadane.Size = 4;

				pMnozstviNasnimano.SqlDbType = System.Data.SqlDbType.Decimal;
				pMnozstviNasnimano.Precision = 19;
				pMnozstviNasnimano.Scale = 5;
				pMnozstviNasnimano.Size = 4;

				pMnozstviDodavatelePozadovano.SqlDbType = System.Data.SqlDbType.Decimal;
				pMnozstviDodavatelePozadovano.Precision = 19;
				pMnozstviDodavatelePozadovano.Scale = 5;
				pMnozstviDodavatelePozadovano.Size = 4;

				pMnozstviDodavateleDodano.SqlDbType = System.Data.SqlDbType.Decimal;
				pMnozstviDodavateleDodano.Precision = 19;
				pMnozstviDodavateleDodano.Scale = 5;
				pMnozstviDodavateleDodano.Size = 4;

				pMnozstviDodavateleDodat.SqlDbType = System.Data.SqlDbType.Decimal;
				pMnozstviDodavateleDodat.Precision = 19;
				pMnozstviDodavateleDodat.Scale = 5;
				pMnozstviDodavateleDodat.Size = 4;

				pMnozstviOdberateliPozadovano.SqlDbType = System.Data.SqlDbType.Decimal;
				pMnozstviOdberateliPozadovano.Precision = 19;
				pMnozstviOdberateliPozadovano.Scale = 5;
				pMnozstviOdberateliPozadovano.Size = 4;

				pMnozstviOdberatelumDodano.SqlDbType = System.Data.SqlDbType.Decimal;
				pMnozstviOdberatelumDodano.Precision = 19;
				pMnozstviOdberatelumDodano.Scale = 5;
				pMnozstviOdberatelumDodano.Size = 4;

				pMnozstviOdberatelumDodat.SqlDbType = System.Data.SqlDbType.Decimal;
				pMnozstviOdberatelumDodat.Precision = 19;
				pMnozstviOdberatelumDodat.Scale = 5;
				pMnozstviOdberatelumDodat.Size = 4;

				pVysledek.SqlDbType = System.Data.SqlDbType.Decimal;
				pVysledek.Precision = 19;
				pVysledek.Scale = 5;
				pVysledek.Size = 4;

				dbCommand.Connection.Open();

				//dbCommand.Prepare();

				dbCommand.ExecuteNonQuery();
				dbCommand.Connection.Close();

				MnozstviDodavatelePozadovano = (decimal)pMnozstviDodavatelePozadovano.Value;
				MnozstviDodavateleDodano = (decimal)pMnozstviDodavateleDodano.Value;
				MnozstviDodavateleDodat = (decimal)pMnozstviDodavateleDodat.Value;
				MnozstviOdberateliPozadovano = (decimal)pMnozstviOdberateliPozadovano.Value;
				MnozstviOdberatelumDodano = (decimal)pMnozstviOdberatelumDodano.Value;
				MnozstviOdberatelumDodat = (decimal)pMnozstviOdberatelumDodat.Value;
				Vysledek = (decimal)pVysledek.Value;

				return true;
			}
			else if (dbCommand.CommandType == CommandType.Text)
			{
				
			}
			else
			{
				throw new Exception("Neznámý typ příkazu: " + dbCommand.CommandType.ToString());
			}


			return true;
		}
		catch (Exception ex)
		{

			throw ex;
		} 


		#endregion
	}

        public string Online_GenerateSerltnum(string itemnmbr, string skl_id, string oldSerltnum)
        {
            string serltnum = string.Empty;
            SqlConnection adpaconnection = null;

            try
            {
                Globals_V1.LoadConfiguration();

                string prijem_generateData = Globals_V1.Konfigurace.Prijem[0].GenerateSerltnum_Action;

                if (prijem_generateData.Length != 0)
                {
                    adpaconnection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                    SqlCommand adpacommand = new SqlCommand(prijem_generateData);
                    adpacommand.CommandType = CommandType.StoredProcedure;

                    // parametry
                    adpacommand.Parameters.Add((new SqlParameter("@Itemnmbr", SqlDbType.NVarChar, 31)));
                    adpacommand.Parameters.Add((new SqlParameter("@OldSerltnum", SqlDbType.NVarChar, 21)));
                    adpacommand.Parameters.Add((new SqlParameter("@Skl_id", SqlDbType.NVarChar, 20)));
                    adpacommand.Parameters.Add((new SqlParameter("@Serltnum", SqlDbType.NVarChar, 21)));

                    // hodnoty vstupnich parametru
                    ((IDataParameter)adpacommand.Parameters["@Itemnmbr"]).Value = itemnmbr;
                    ((IDataParameter)adpacommand.Parameters["@OldSerltnum"]).Value = oldSerltnum;
                    ((IDataParameter)adpacommand.Parameters["@Skl_id"]).Value = skl_id;

                    // hodnoty vystupnich parametru
                    ((IDataParameter)adpacommand.Parameters["@Serltnum"]).Direction = ParameterDirection.Output;

                    adpacommand.Connection = adpaconnection;

                    adpaconnection.Open();
                    adpacommand.ExecuteNonQuery();

                    serltnum = ((IDataParameter)adpacommand.Parameters["@Serltnum"]).Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
                if ((adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }

            return serltnum;
        }
        public Fask.Server.Interfaces.DataSets.Obecne Online_GetDoporuceneLokace(string itemnmbr, string skl_id, string serltnum)
        {
            Fask.Server.Interfaces.DataSets.Obecne ds = new Fask.Server.Interfaces.DataSets.Obecne();
            SqlConnection adpaconnection = null;
            try
            {
                Globals_V1.LoadConfiguration();

                string prijem_generateData = Globals_V1.Konfigurace.Prijem[0].GetDoporuceneLokace_Action;
                if (prijem_generateData.Length != 0)
                {
                    adpaconnection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                    SqlCommand adpacommand = new SqlCommand(prijem_generateData);
                    adpacommand.CommandType = CommandType.StoredProcedure;

                    // vstupni parametry
                    adpacommand.Parameters.Add((new SqlParameter("@Itemnmbr", SqlDbType.NVarChar, 31)));
                    adpacommand.Parameters.Add((new SqlParameter("@Serltnum", SqlDbType.NVarChar, 21)));
                    adpacommand.Parameters.Add((new SqlParameter("@Skl_id", SqlDbType.NVarChar, 20)));

                    // hodnoty vstupnich parametru
                    ((IDataParameter)adpacommand.Parameters["@Itemnmbr"]).Value = itemnmbr;
                    ((IDataParameter)adpacommand.Parameters["@Serltnum"]).Value = serltnum;
                    ((IDataParameter)adpacommand.Parameters["@Skl_id"]).Value = skl_id;

                    adpacommand.Connection = adpaconnection;

                    adpaconnection.Open();

                    SqlDataAdapter xda = new SqlDataAdapter();
                    xda.SelectCommand = adpacommand;

                    xda.Fill(ds, ds.Lokace.TableName);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
                if ((adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }

            return ds;
        }

        public Fask.Server.Interfaces.DataSets.Obecne Online_GetNezrealizovanePrijemky()
        {
            Fask.Server.Interfaces.DataSets.Obecne ds = new Fask.Server.Interfaces.DataSets.Obecne();
            SqlConnection adpaconnection = null;
            try
            {
                Globals_V1.LoadConfiguration();

                string prijem_generateData = Globals_V1.Konfigurace.Prijem[0].GetNezrealizovanePrijemky_Action;
                if (prijem_generateData.Length != 0)
                {
                    adpaconnection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                    SqlCommand adpacommand = new SqlCommand(prijem_generateData);
                    adpacommand.CommandType = CommandType.StoredProcedure;

                    adpacommand.Connection = adpaconnection;

                    adpaconnection.Open();

                    SqlDataAdapter xda = new SqlDataAdapter();
                    xda.SelectCommand = adpacommand;

                    xda.Fill(ds, ds.Prijemky.TableName);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
                if ((adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }

            return ds;
        }


        #endregion

        #endregion

        public string TEST_ImportPrijem_Do_IS(int countEntries, string SKL_ID, string PONUMBER, string note)
        {
            if (Database.Pohoda.OBJ_Exists(PONUMBER))
            {
                string vysledek = this.ImportPrijemkaPohoda(countEntries, 99);
                if (vysledek != "OK")
                {
                    return "Error";
                }

            }

            return "OK";
        }

    }
}
