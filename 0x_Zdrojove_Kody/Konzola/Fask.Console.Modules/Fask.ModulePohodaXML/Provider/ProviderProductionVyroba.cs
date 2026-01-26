using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Data.Common;
using System.Globalization;
//using System.Web.UI.WebControls;
//using System.Web. UI;
//using Fask.Server.Interfaces.Classes;
using System.IO;
using System.Data.SqlClient;
using System.Data;
//using Fask.Tracing;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider :
        Fask.Console.Interfaces.Vyroba.Production.IProduction,
        Fask.Console.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionList,
        Fask.Console.Interfaces.Vyroba.Production.IProduction_ImportPrijemkaPohoda,
        Fask.Console.Interfaces.Vyroba.Production.IProduction_GetDataSelectListImport

    {
        public const string prijem_import_prijemka = XML.PohodaComunication._import_prijemka + ".xml";

        #region IProduction_GetFiltrovanyProductionList Members

        public Fask.Console.Interfaces.DataSets.Vyroba GetFiltrovanyProductionList(Console.Interfaces.Classes.ProductionListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Console.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Console.Interfaces.DataSets.Vyroba();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;

            command.CommandText =
                "Select l.firstname, l.surname,";
            if (filtr.VyrobaPouzivatTabulkuZbozi)
                command.CommandText += "z.ITEMDESC,";
            command.CommandText +=
                "o.name operationName, m.name machineName, g.name groupName" +
                ", sklady.skl_desc skladName" +
                ", lokace.Barcode lokaceKod, lokace.Description lokaceName" +
                ", hlavicky.SOPDESC popiszakazky" +
                ", p.* from Production p";


            if (filtr.VyrobaPouzivatTabulkuZbozi)
            {
                command.CommandText += " left join (select distinct ITEMDESC, ITEMNMBR from FASK_CONS_095) z on z.itemnmbr = p.itemnmbr";
            }

            command.CommandText += " left join Logins l on l.id = p.userid" +
                " left join Operations o on o.id = p.operationid" +
                " left join Machines m on m.id = p.machineid" +
                " left join VLoginsGroups vg on l.id = vg.loginid" +
                " left join Groups g on vg.groupid = g.id" +
                " left join CZMST093 sklady on sklady.skl_id = p.skl_id" +
                " left join CZMST094 lokace on lokace.skl_id = p.skl_id and lokace.locncode=p.locncode" +
                " left join CZPRO_VPH hlavicky on hlavicky.SOPNUMBE = p.SOPNUMBE" +
                " Where ";

            command.CommandText += "1=1 ";
            // číslo zakázky
            if (filtr.rowVPH != null)
            {
                command.CommandText += " AND p.SOPNUMBE = @SOPNUMBE";
                command.Parameters.AddWithValue("@SOPNUMBE", filtr.rowVPH.SOPNUMBE.Trim());
            }
            else if (filtr.VyrobniPrikaz.Length > 0)
            {
                command.CommandText += " AND isnull(p.SOPNUMBE, '') IN(" +
                    "Select SOPNUMBE from CZPRO_VPH where (SOPNUMBE like '%' + @SOPNUMBE + '%')" +
                    " union " +
                    "Select SOPNUMBE from CZPRO_VPH where (SOPDESC like '%' + @SOPNUMBE + '%')" +
                    ")";

                command.Parameters.AddWithValue("@SOPNUMBE", filtr.VyrobniPrikaz);
            }


            if (filtr.VyrobaPouzivatTabulkuZbozi)
            {
                // hledaní zboží
                if (filtr.rowZbozi != null)
                {
                    command.CommandText += " AND p.ITEMNMBR=@itemdesc";
                    command.Parameters.AddWithValue("@itemdesc", filtr.rowZbozi.ITEMDESC.Trim());
                }
                else if (filtr.Zbozi_itemdesc.Length > 0)
                {
                    command.CommandText += " AND p.ITEMNMBR IN (" +
                    " select ITEMNMBR from FASK_CONS_095" +
                    " where ITEMDESC like '%' + @itemdesc + '%'" +
                    " union" +
                    " select ITEMNMBR from FASK_CONS_095" +
                    " where ITEMNMBR like '' + @itemdesc + '%' " +
                    " )";
                    command.Parameters.AddWithValue("@itemdesc", filtr.Zbozi_itemdesc);
                }

            }

            // hledání uživatele
            if (!string.IsNullOrEmpty(filtr.Uzivatel))
            {
                if (filtr.Uzivatel != null)
                {
                    command.CommandText += " AND p.UserID=@name";
                }
                else
                {
                    command.CommandText += " AND p.UserID IN (" +
                    " select distinct id from Logins" +
                    " where firstname like '%' + @name + '%'" +
                    " union" +
                    " select distinct id from Logins" +
                    " where surname like '%' + @name + '%'" +
                    " union" +
                    " select distinct id from Logins" +
                    " where id = @name" +
                    " )";
                }
                command.Parameters.AddWithValue("@name", filtr.rowUzivatel != null ? filtr.rowUzivatel.id.Trim() : filtr.Uzivatel);
            }

            // hledání skupiny
            if (!string.IsNullOrEmpty(filtr.Skupina))
            {
                if (filtr.Skupina != null)
                {
                    command.CommandText += " AND g.name=@groupname";
                }
                else
                {
                    command.CommandText += " AND isnull(g.name, '') like '%' + @groupname + '%'";
                }
                command.Parameters.AddWithValue("@groupname", filtr.rowGroups != null ? filtr.rowGroups.name : filtr.Skupina);
            }

            // hledání podle stroje
            if (!string.IsNullOrEmpty(filtr.Stroj))
            {
                if (filtr.Stroj != null)
                {
                    command.CommandText += " AND m.name=@machinename";
                }
                else
                {
                    command.CommandText += " AND isnull(m.name, '') like '%' + @machinename + '%'";
                }
                command.Parameters.AddWithValue("@machinename", filtr.rowMachine != null ? filtr.rowMachine.name : filtr.Stroj);
            }

            // hledání podle operace
            if (!string.IsNullOrEmpty(filtr.Operace))
            {
                if (filtr.Operace != null)
                {
                    command.CommandText += " AND o.name=@operationname";
                }
                else
                {
                    command.CommandText += " AND isnull(o.name, '') like '%' + @operationname + '%'";
                }
                command.Parameters.AddWithValue("@operationname", filtr.rowOperation != null ? filtr.rowOperation.name : filtr.Operace);
            }

            // hledání podle datumu
            if (filtr.DatumOd && filtr.DatumDo)
            {
                command.CommandText += " AND dateeve between @datumOd and @datumDo";
                command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
                command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
            }
            else
            {
                if (filtr.DatumOd)
                {
                    command.CommandText += " AND dateeve > @datumOd";
                    command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
                }
                else if (filtr.DatumDo)
                {
                    command.CommandText += " AND dateeve < @datumDo";
                    command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
                }
            }
            // zobrazit vsechny zakazky
            if (filtr.OdvadeniVse)
            {
                command.CommandText += " AND p.SOUBEHGUID is not null";
            }

            // zobrazit vsechny korekce
            if (filtr.KorekceVse)
            {
                command.CommandText += " AND p.CORRGUID is not null";
            }

            // zobrazit nedokonceny odvod vyroby
            if (filtr.OdvadeniNedokoncene)
            {
                command.CommandText += " AND p.SOUBEHGUID not IN (" +
                " select distinct SOUBEHGUID from Production" +
                " where" +
                " SOUBEHGUID is not null and TIMESTOP is not null" +
                " )";
            }
            // zobrazit nedokoncene korekce
            if (filtr.KorekceNedokoncene)
            {
                //command.CommandText += " AND p.CORRGUID not IN (" +
                //" select distinct CORRGUID from Production" +
                //" where" +
                //" CORRGUID is not null and TIMECORSTOP is not null" +
                //" )";
                command.CommandText += " AND not exists ( " +
                "select SOUBEHGUID " +
                "from Production " +
                "where " +
                "(TIMESTOP is not null or TIMEPREPSTOP is not null) " +
                "and " +
                "SOUBEHGUID=p.SOUBEHGUID " +
                ") " +
                "and SOUBEHGUID is not null "
                ;
            }

            command.CommandText += " order by dateeve desc";
            //Point p = Point.Empty;
            //try
            //{
            //    DataGridViewCell currentCell = this.dataGridView1.CurrentCell;
            //    p = new Point(currentCell.ColumnIndex, currentCell.RowIndex);
            //}
            //catch
            //{
            //}
            // test
            //int FirstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex; //Save Current Scroll Index
            //int SelectedRowIndex = 0;
            //if (this.dataGridView1.SelectedRows.Count > 0) SelectedRowIndex = this.dataGridView1.SelectedRows[0].Index; //Save Current Selected Row Index

            vyrobaDataSet1.Production.Clear();
            vyrobaDataSet1.Production.AcceptChanges();
            vyrobaDataSet1.Production.BeginLoadData();
            adapter.SelectCommand = command;
            adapter.Fill(vyrobaDataSet1.Production);
            vyrobaDataSet1.Production.EndLoadData();

            return vyrobaDataSet1;
        }

        #endregion

        #region IProduction_ImportPrijemkaPohoda Members



        public string ImportPrijemkaPohoda(int countEntries,string SKL_ID, Fask.Console.Interfaces.DataSets.Konzola.FASK_CONS_LoginsRow userID)
        {
            //TODO PRedelat na upravenou verzi kde se komunikuje pomoci 
            // PohodaComunication.cs
            // a pouziva sa Objektove sestavovani XML souboru



            try{
                string filename;
                filename = XML.PohodaComunication.FilenameCompose(prijem_import_prijemka);

                string dateFrom = string.Empty;
                string dateTill = string.Empty;
                string dateLasChange = string.Empty;
                List<string> icos = new List<string>();
                string uzivFiltrPohoda = string.Empty;
                List<string> companyNames = new List<string>();
                List<string> cislaDoklady = new List<string>();

                string pom = string.Empty;

                pom = Globals.LoadConfiguration();

                if (pom != "OK")
                    throw new Exception("Nezdařilo se načteni konfigurace.");

                Pohoda_DataSets.Prijem.ProductionDataTable dt_p = null;
                dt_p = Database.Prijem.GETDATA_Production(countEntries, SKL_ID);

                if (!Prijem.CreateRequest_Import_Prijemka_XML_NEW(dt_p, filename, countEntries, SKL_ID, ""))
                    throw new Exception("Nepodaril se create requestu.");

                string respfilename;
                if (!XML.PohodaComunication.Communicate(filename, out respfilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                pom = string.Empty;
                pom = Prijem.LoadResponse_Import_Prijemka_XML(dt_p, respfilename, countEntries, SKL_ID);

                if (!string.IsNullOrEmpty(pom))
                {
                    if (!Database.Prijem.UPDATEDATA_Production(dt_p, countEntries, SKL_ID))
                    {
                        throw new Exception("Nepodařilo se aktualizovat odeslaná data výrobků!");
                    }
                }

                //Parovani a update objednavka/prijemka v pohoda ... 
                //pom = LoadPrijemImportResponseXMLAndMakeUpdateDB(filename, countEntries);

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
        public string LoadPrijemImportResponseXMLAndMakeUpdateDB(string file, int countentries)
        {
            System.Data.OleDb.OleDbConnection oledbConnection = null;
            try
            {
                //nazev souboru s odpovedi
                string filename = Globals.PathToInputDirectory + "Response\\" + file;

                string actualStrName = string.Empty;
                string previousStrName = string.Empty;
                string actualSonnumber = string.Empty;

                string pom = string.Empty;
                pom = Globals.LoadConfiguration();
                if (pom != "OK")
                    return pom;

                //ResponsePack odpoved = new ResponsePack();
                XML.Classes.Response2 response =
                    new XML.Classes.Response2(filename);

                // 17.6.2016 PeV: uprava, aby bylo mozne odeslat davku v pripade, ze data se jiz dostala do pohody (napr. nastal timeout na terminalu)
                // OK2 -> zaznam se jiz dostal uspesne do pohody, je treba umoznit smazat davku z terminalu
                if (response.Status == "OK2")
                    return "OK";

                if (response.Status != "OK")
                    return response.Status;

                //cislo dokladu nalezeno => aktualizace dokladu v pohode...
                //1) zjistit cislo dokladu prijemky
                //2) vytahnout polozky z pe s ord, itemnmbr, sopnumbe?
                //2a) cislo dokladu objednavky
                //3) vytahnout polozky z skpp a skppol pro konkretni novy doklad
                //4) vytahnout polozka z obj a objpol pro doklad
                //5) sparovat polozky obj(pe) s skpp
                // a) Karta Zasoby => Objednano < nutne ponizit hodnotu o prijate mnozstvi
                // b) oznaceni preneseno na polozce objednavky
                //6) update v transakci ...


                // 1) Cislo dokladu prijemky
                string cislodokladuprijemka = response.CisloDokladuPrijemka;
                if (String.IsNullOrEmpty(cislodokladuprijemka))
                    return "Cislo dokladu prijemky nenalezeno v odpovedi pohody";

                // 2) Vytahnout polozky prijemky z MST
                Pohoda_DataSets.Prijem prijemDS = new Pohoda_DataSets.Prijem();

                //Pohoda_DataSets.PrijemTableAdapters.CZMST_PETableAdapter peAdapter = new Pohoda_DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
                //Pohoda_DataSets.PrijemTableAdapters.CZMST_PITableAdapter piAdapter = new Pohoda_DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
                Pohoda_DataSets.PrijemTableAdapters.ProductionTableAdapter prod_Adapter = new Pohoda_DataSets.PrijemTableAdapters.ProductionTableAdapter();
                
                //peAdapter.Connection.ConnectionString = Globals.ConnectionString;
                //piAdapter.Connection.ConnectionString = Globals.ConnectionString;
                prod_Adapter.Connection = new SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);

                //peAdapter.FillByCountEntries(prijemDS.CZMST_PE, countentries);
                //piAdapter.FillByCountEntries(prijemDS.CZMST_PI, countentries);
                prod_Adapter.FillByCountEntries(prijemDS.Production, countentries);

                //string cislodokladuobjednavky = prijemDS.CZMST_PE[0].PONUMBER.Trim();

                Pohoda_DataSets.DatabasePohoda pohodaDS = new Pohoda_DataSets.DatabasePohoda();
                oledbConnection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

                //3) vytahnout polozky z skpp a skppol pro konkretni novy doklad
                //Pohoda_DataSets.DatabasePohodaTableAdapters.SKPPTableAdapter skppAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKPPTableAdapter();
                //Pohoda_DataSets.DatabasePohodaTableAdapters.SKPPpolTableAdapter skpppolAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKPPpolTableAdapter();
                //skppAdapter.Connection = oledbConnection;
                //skpppolAdapter.Connection = oledbConnection;

                //skppAdapter.FillByCislo(pohodaDS.SKPP, cislodokladuprijemka);
                //skpppolAdapter.FillByRefAg(pohodaDS.SKPPpol, pohodaDS.SKPP[0].ID);

                Database.Pohoda.SKPP_FillByCislo(pohodaDS.SKPP, cislodokladuprijemka);
                Database.Pohoda.SKPPpol_FillByRefAg(pohodaDS.SKPPpol, pohodaDS.SKPP[0].ID);

                //4) vytahnout polozky objednavky z pohody
                //Pohoda_DataSets.DatabasePohodaTableAdapters.OBJTableAdapter objAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.OBJTableAdapter();
                //Pohoda_DataSets.DatabasePohodaTableAdapters.OBJpolTableAdapter objpolAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.OBJpolTableAdapter();
                //objAdapter.Connection = oledbConnection;
                //objpolAdapter.Connection = oledbConnection;

                //objAdapter.FillByCislo(pohodaDS.OBJ, cislodokladuobjednavky);
                //objpolAdapter.FillByRefAg(pohodaDS.OBJpol, pohodaDS.OBJ[0].ID);

                //5) sparovani ...

                // 5)a) dohledani karty zasoby a zmena mnozstvi hodnoty OBJV
                // 5)b) take aktualizace hodnoty v bufferu SKzBuf, ktery nese informaci o objednanem mnozstvi ... 
                //      => toto na zaklade spoctene hodnoty v SKz, protoze tam je to platne... ???
                //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzObjedVTableAdapter skzObjedVAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzObjedVTableAdapter();
                //skzObjedVAdapter.Connection = oledbConnection;
                //skzObjedVAdapter.ClearBeforeFill = false;

                //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzBufTableAdapter skzBufAdapter = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzBufTableAdapter();
                //skzBufAdapter.Connection = oledbConnection;
                //skzBufAdapter.ClearBeforeFill = false;

                oledbConnection.Open();
                // transakce se resi na konci v ta managerovi ... 
                //System.Data.OleDb.OleDbTransaction oletrans = oledbConnection.BeginTransaction();

                foreach (Pohoda_DataSets.Prijem.ProductionRow pir in prijemDS.Production)
                {
                    //neprirazene polozky dle itemnmbr a mnozstvi
                    string query = " RefSKz=" + pir.ITEMNMBR.Trim() + " AND Mnozstvi=" + pir.qty.ToString("0.00", CultureInfo.InvariantCulture) + " AND RefPol is NULL AND RelAgID is NULL";
                    Pohoda_DataSets.DatabasePohoda.SKPPpolRow[] neprirazenePolozky = (Pohoda_DataSets.DatabasePohoda.SKPPpolRow[])pohodaDS.SKPPpol.Select(
                        query
                        , "ID"
                        , System.Data.DataViewRowState.CurrentRows);
                    if (neprirazenePolozky.Length == 0)
                    { //nic se nedeje, protoze nebyly nalezeny => log... jde o chybu ...
                        Fask.Logging.Log.Write("Parovani nenalezlo volnou shodu v databazi skpppol s polozkou:" + pir.ORD + "|" + pir.ITEMNMBR + "|" + pir.qty + "|" + pir.GUID.ToString(), "PrijemPohodaXML" + "," + "LoadPrijemImportResponseXmlAndMakeUpdateDB");
                        continue; //pokracuji dalsim radkem ... 
                    }
                    else if (neprirazenePolozky.Length > 0)
                    { //je jich vice, no tak priradim prvni nalezenou, protoze je to v podstate uplne sumus...
                        //prirazeni se deje az po dohledani radku objednavky ...
                    }

                    Pohoda_DataSets.DatabasePohoda.OBJpolRow objpolr = pohodaDS.OBJpol.FindByID(pir.ORD);
                    if (objpolr != null)
                    {
                        objpolr.Dodano += (double)pir.qty; // TODO : zaokroulovani????
                        //experimentalne zjistena hodnota pro dokladovou vazbu na vydanou objednavku ... 
                        // CHECK : !!! muze se v aktualizaci pohody zmenit !!!
                        neprirazenePolozky[0].RelAgID = 12;
                        neprirazenePolozky[0].RefPol = objpolr.ID;

                        // 5)a) dohledani / dotazeni zbozi karty
                        int skzID = int.Parse(pir.ITEMNMBR);
                        Pohoda_DataSets.DatabasePohoda.SKzObjedVRow skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                        if (skzObjedVRow == null)
                        {
                            //skzObjedVAdapter.FillByID(pohodaDS.SKzObjedV, int.Parse(pir.ITEMNMBR));
                            Database.Pohoda.SKzObjedV_FillByID(pohodaDS.SKzObjedV, int.Parse(pir.ITEMNMBR));
                            skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                        }
                        if (skzObjedVRow == null)
                            Logging.Log.Write( "Nenalezena karta zasoby pro snizeni objednaneho mnozstvi:" + pir.ORD + "|" + pir.ITEMNMBR + "|" + pir.qty + "|" + pir.GUID.ToString(),"PrijemPohodaXML"+","+"LoadPrijemImportResponseXmlAndMakeUpdateDB");
                        else
                            skzObjedVRow.ObjedV -= (double)pir.qty; //pokud jiz bylo v cyklu drive snizeno, tak se snizi opet ...
                    }
                    else //je-li null, tak se nenaslo zbozi ... coz je chyba a bude zalogovana ...
                    {
                        Logging.Log.Write("Parovani nenalezlo volnou shodu v databazi objpol s polozkou:" + pir.ORD + "|" + pir.ITEMNMBR + "|" + pir.qty + "|" + pir.GUID.ToString(), "PrijemPohodaXML" + "," + "LoadPrijemImportResponseXmlAndMakeUpdateDB");
                    }

                }

                //// 5 a) dokonceni update hodnoty v SKzBuf...
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
                        Logging.Log.Write("Parovani nenalezlo shodu v databazi skzbuf s polozkou:" + item.ID + "|" + item.ObjedV, "PrijemPohodaXML" + "," + "LoadPrijemImportResponseXmlAndMakeUpdateDB");
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

                if (Properties.Settings.Default.Pohoda_Objednavka_Set_Vyrizeno)
                    pohodaDS.OBJ[0].Vyrizeno = true;

                #region update v transakci ...
                //6)  update v transakci ...

                //Pohoda_DataSets.DatabasePohodaTableAdapters.TableAdapterManager pohodaTaManager = new Pohoda_DataSets.DatabasePohodaTableAdapters.TableAdapterManager();
                //pohodaTaManager.OBJpolTableAdapter = objpolAdapter;
                //pohodaTaManager.SKPPpolTableAdapter = skpppolAdapter;
                //pohodaTaManager.OBJTableAdapter = objAdapter;
                //pohodaTaManager.SKzObjedVTableAdapter = skzObjedVAdapter;
                //pohodaTaManager.SKzBufTableAdapter = skzBufAdapter;

                //pohodaTaManager.Connection = oledbConnection;
                //transakce se otevira v managerovi ... 
                //pohodaTaManager.Connection.Open();
                //System.Data.IDbTransaction pohodaTransaction = pohodaTaManager.Connection.BeginTransaction();
                //pohodaTaManager.UpdateOrder = Pohoda_DataSets.DatabasePohodaTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
                //pohodaTaManager.UpdateAll(pohodaDS);

                System.Data.OleDb.OleDbTransaction trans = null;
                System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);
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
                    Logging.Log.Write("");
                    try
                    {
                        if (trans != null)
                            trans.Rollback();
                    }
                    catch (Exception exx)
                    {
                        Logging.Log.Write("Neprošla Rollback transakce:" + exx.Message);

                    }

                    throw ex;

                }
                
                #endregion

                return "OK";

            }
            catch (Exception e)
            {
                Logging.Log.Write(e.ToString());
                return e.Message;
            }
            finally
            {
                if (oledbConnection != null && ((oledbConnection.State & ConnectionState.Open) == ConnectionState.Open))
                    oledbConnection.Close();
            }
        }


        #endregion



        #region IProduction_GetDataSelectListImport Members

        public Fask.Console.Interfaces.DataSets.Vyroba GetDataSelectListImport()
        {
            Pohoda_DataSets.Prijem ds = new Pohoda_DataSets.Prijem();
            Fask.Console.Interfaces.DataSets.Vyroba dsout = new Fask.Console.Interfaces.DataSets.Vyroba();

            try
            {

                Pohoda_DataSets.PrijemTableAdapters.ProductionImportTableAdapter ta = new Pohoda_DataSets.PrijemTableAdapters.ProductionImportTableAdapter();
                ta.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);

                ta.Fill(ds.ProductionImport);

                foreach (System.Data.DataRow row in ds.ProductionImport)
                {
                    var RowNew = dsout.ProductionImport.NewProductionImportRow();

                    foreach (System.Data.DataColumn Column in row.Table.Columns)
                    {
                        try { RowNew[Column.ColumnName] = row[Column.ColumnName]; }
                        catch { continue; }
                    }
                    dsout.ProductionImport.AddProductionImportRow(RowNew);
                }

                //foreach (Pohoda_DataSets.Prijem.ProductionRow Row in ds.Production)
                //{
                //    var newrow = dsout.Production.NewProductionRow();

                //    newrow.CountEntries = Row.CountEntries;

                //    newrow.SOPNUMBE = Row.SOPNUMBE;
                //    newrow.ITEMNMBR = Row.ITEMNMBR;
                //    newrow.ITEMTYPE = Row.ITEMTYPE;
                //    newrow.ITEMMJ = Row.ITEMMJ;
                //    newrow.ORD = Row.ORD;
                //    newrow.TIMEMODE = Row.TIMEMODE;
                //    newrow.TIMEPREPSTART = Row.TIMEPREPSTART;
                //    newrow.TIMEPREPSTOP = Row.TIMEPREPSTOP;
                //    newrow.TIMEPREP = Row.TIMEPREP;
                //    newrow.TIMEUNIT = Row.TIMEUNIT;
                //    newrow.TIMESTART = Row.TIMESTART;
                //    newrow.TIMESTOP = Row.TIMESTOP;
                //    newrow.TIMECORSTART = Row.TIMECORSTART;
                //    newrow.TIMECORSTOP = Row.TIMECORSTOP;
                //    newrow.TIMECOR = Row.TIMECOR;
                //    newrow.TIMECRID = Row.TIMECRID;
                //    newrow.id = Row.id;
                //    newrow.loginid = Row.loginid;
                //    newrow.machineid = Row.machineid;
                //    newrow.operationid = Row.operationid;
                //    newrow.dateeve = Row.dateeve;
                //    newrow.qty = Row.qty;
                //    newrow.qtyReal = Row.qtyReal;
                //    newrow.QTYPACK = Row.QTYPACK;
                //    newrow.QTYPACKMJ = Row.QTYPACKMJ;
                //    newrow.description = Row.description;
                //    newrow.BarcodeP = Row.BarcodeP;
                //    newrow.UserID = Row.UserID;
                //    newrow.TermID = Row.TermID;
                //    newrow.ISOK = Row.ISOK;
                //    newrow.GUID = Row.GUID;
                //    newrow.SOUBEHGUID = Row.SOUBEHGUID;
                //    newrow.qtyOld = Row.qtyOld;
                //    newrow.idVS = Row.idVS;
                //    newrow.dateedit = Row.dateedit;
                //    //newrow.firstname= Row.firstname;
                //    //newrow.surname= Row.surname;
                //    newrow.ITEMDESC = Row.ITEMDESC;
                //    //newrow.operationName= Row.operationName;
                //    //newrow.machineName= Row.machineName;
                //    newrow.CORRGUID = Row.CORRGUID;
                //    //newrow.groupName= Row.groupName;
                //    newrow.TIMECRIDTYPE = Row.TIMECRIDTYPE;
                //    newrow.SKL_ID = Row.SKL_ID;
                //    //newrow.skladName= Row.skladName;
                //    newrow.LOCNCODE = Row.LOCNCODE;
                //    //newrow.lokaceKod= Row.lokaceKod;
                //    //newrow.lokaceName= Row.lokaceName;
                //    //newrow.popiszakazky= Row.popiszakazky;

                //    dsout.Production.AddProductionRow(newrow);
                //}
                

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex.Message);
            }

            return dsout;
        }

        #endregion
    }
}