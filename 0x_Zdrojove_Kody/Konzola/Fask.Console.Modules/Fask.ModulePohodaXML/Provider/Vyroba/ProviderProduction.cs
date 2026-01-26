using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Fask.Interfaces.Vyroba.Production;
using Fask.Interfaces.Filtry;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider   :
        Fask.Interfaces.Vyroba.Production.IProduction,
        Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionList,
        Fask.Interfaces.Vyroba.Production.IProduction_FillByCORRGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_FillBySOUBEHGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_GetDataByCORRGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_GetDataBySOUBEHGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_Update,
        Fask.Interfaces.Vyroba.Production.IProduction_ImportPrijemkaPohoda_Zdroj_P_PS,
        Fask.Interfaces.Vyroba.Production.IProduction_ImportPrijemkaPohoda_Zdroj_P_TP,
        Fask.Interfaces.Vyroba.Production.IProduction_ImportPrijemkaPohoda_Zdroj_P,
        Fask.Interfaces.Vyroba.Production.IProduction_ImportPohoda_Zdroj_P_Vyroba,
        Fask.Interfaces.Vyroba.Production.IProduction_GetDataSelectListImport,
        Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby,
        Fask.Interfaces.Vyroba.Production.IProduction_Delete_ISOK

    {

        #region IProduction_GetFiltrovanyProductionList Members

        public Fask.Interfaces.DataSets.Vyroba Production_GetFiltrovanyProductionList(Fask.Interfaces.Filtry.ProductionListFiltr filtr)
        {
            #region OLD
            //Globals_V1.LoadConfiguration();
            //System.Data.SqlClient.SqlConnection connection = null;
            //System.Data.SqlClient.SqlCommand command = null;
            //System.Data.SqlClient.SqlDataAdapter adapter = null;
            //Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            //adapter = new System.Data.SqlClient.SqlDataAdapter();
            //connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            //command = new System.Data.SqlClient.SqlCommand();
            //command.Connection = connection;

            //command.CommandText =
            //    "Select l.firstname, l.surname,";
            //if (filtr.VyrobaPouzivatTabulkuZbozi)
            //    command.CommandText += "z.ITEMDESC,";
            //command.CommandText +=
            //    "o.name operationName, m.name machineName, g.name groupName" +
            //    ", sklady.skl_desc skladName" +
            //    ", lokace.Barcode lokaceKod, lokace.Description lokaceName" +
            //    ", hlavicky.SOPDESC popiszakazky" +
            //    ", p.* from " + Fask.SQL.Constants.Common.TABLE_Production + " p";


            //if (filtr.VyrobaPouzivatTabulkuZbozi)
            //{
            //    command.CommandText += " left join (select distinct ITEMDESC, ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + ") z on z.itemnmbr = p.itemnmbr";
            //}

            //command.CommandText += " left join " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " l on l.USERID = p.userid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_Operations + " o on o.id = p.operationid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_Machines + " m on m.id = p.machineid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_VLoginsGroups + " vg on l.USERID = vg.loginid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_Groups + " g on vg.groupid = g.id" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklady on sklady.skl_id = p.skl_id" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " lokace on lokace.skl_id = p.skl_id and lokace.locncode=p.locncode" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " hlavicky on hlavicky.SOPNUMBE = p.SOPNUMBE" +
            //    " Where ";

            //command.CommandText += "1=1 ";
            //// číslo zakázky
            //if (filtr.rowVPH != null && filtr.rowVPH.Count > 0)
            //{
            //    command.CommandText += " AND p.SOPNUMBE = @SOPNUMBE";
            //    command.Parameters.AddWithValue("@SOPNUMBE", filtr.rowVPH[0].SOPNUMBE.Trim());
            //}
            //else if (filtr.VyrobniPrikaz.Length > 0)
            //{
            //    command.CommandText += " AND isnull(p.SOPNUMBE, '') IN(" +
            //        "Select SOPNUMBE from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " where (SOPNUMBE like '%' + @SOPNUMBE + '%')" +
            //        " union " +
            //        "Select SOPNUMBE from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " where (SOPDESC like '%' + @SOPNUMBE + '%')" +
            //        ")";

            //    command.Parameters.AddWithValue("@SOPNUMBE", filtr.VyrobniPrikaz);
            //}


            //if (filtr.VyrobaPouzivatTabulkuZbozi)
            //{
            //    // hledaní zboží
            //    if (filtr.rowZbozi != null && filtr.rowZbozi.Count > 0)
            //    {
            //        command.CommandText += " AND p.ITEMNMBR=@itemdesc";
            //        command.Parameters.AddWithValue("@itemdesc", filtr.rowZbozi[0].ITEMDESC.Trim());
            //    }
            //    else if (filtr.Zbozi_itemdesc.Length > 0)
            //    {
            //        command.CommandText += " AND p.ITEMNMBR IN (" +
            //        " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + 
            //        " where ITEMDESC like '%' + @itemdesc + '%'" +
            //        " union" +
            //        " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + 
            //        " where ITEMNMBR like '' + @itemdesc + '%' " +
            //        " )";
            //        command.Parameters.AddWithValue("@itemdesc", filtr.Zbozi_itemdesc);
            //    }

            //}

            //// hledání uživatele
            //if (!string.IsNullOrEmpty(filtr.Uzivatel))
            //{
            //    if (filtr.Uzivatel != null)
            //    {
            //        command.CommandText += " AND p.UserID=@name";
            //    }
            //    else 
            //    {
            //        command.CommandText += " AND p.UserID IN (" +
            //        " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
            //        " where firstname like '%' + @name + '%'" +
            //        " union" +
            //        " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
            //        " where surname like '%' + @name + '%'" +
            //        " union" +
            //        " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
            //        " where USERID = @name" +
            //        " )";
            //    }

            //    if(filtr.rowUzivatel != null && filtr.rowUzivatel.Count > 0)
            //        command.Parameters.AddWithValue("@name", filtr.rowUzivatel[0].USERID.Trim());
            //    else
            //        command.Parameters.AddWithValue("@name", filtr.Uzivatel);
            //}

            //// hledání skupiny
            //if (!string.IsNullOrEmpty(filtr.Skupina))
            //{
            //    if (filtr.Skupina != null)
            //    {
            //        command.CommandText += " AND g.name=@groupname";
            //    }
            //    else
            //    {
            //        command.CommandText += " AND isnull(g.name, '') like '%' + @groupname + '%'";
            //    }

            //    if (filtr.rowGroups != null && filtr.rowGroups.Count > 0)
            //        command.Parameters.AddWithValue("@groupname", filtr.rowGroups[0].name.Trim());
            //    else
            //        command.Parameters.AddWithValue("@groupname", filtr.Skupina);

            //}

            //// hledání podle stroje
            //if (!string.IsNullOrEmpty(filtr.Stroj))
            //{
            //    if (filtr.Stroj != null)
            //    {
            //        command.CommandText += " AND m.name=@machinename";
            //    }
            //    else
            //    {
            //        command.CommandText += " AND isnull(m.name, '') like '%' + @machinename + '%'";
            //    }

            //    if (filtr.rowMachine != null && filtr.rowMachine.Count > 0)
            //        command.Parameters.AddWithValue("@machinename", filtr.rowMachine[0].name.Trim());
            //    else
            //        command.Parameters.AddWithValue("@machinename", filtr.Stroj);

            //}

            //// hledání podle operace
            //if (!string.IsNullOrEmpty(filtr.Operace))
            //{
            //    if (filtr.Operace != null)
            //    {
            //        command.CommandText += " AND o.name=@operationname";
            //    }
            //    else
            //    {
            //        command.CommandText += " AND isnull(o.name, '') like '%' + @operationname + '%'";
            //    }

            //    if (filtr.rowOperation != null && filtr.rowOperation.Count > 0)
            //        command.Parameters.AddWithValue("@operationname", filtr.rowOperation[0].name.Trim());
            //    else
            //        command.Parameters.AddWithValue("@operationname", filtr.Operace);

            //}

            //// hledání podle datumu
            //if (filtr.DatumOdValue.HasValue && filtr.DatumDoValue.HasValue)
            //{
            //    command.CommandText += " AND p.dateeve between @datumOd and @datumDo";
            //    command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
            //    command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
            //}
            //else
            //{
            //    if (filtr.DatumOdValue.HasValue)
            //    {
            //        command.CommandText += " AND p.dateeve > @datumOd";
            //        command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
            //    }
            //    else if (filtr.DatumDoValue.HasValue)
            //    {
            //        command.CommandText += " AND p.dateeve < @datumDo";
            //        command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
            //    }
            //}
            //// zobrazit vsechny zakazky
            //if (filtr.OdvadeniVse)
            //{
            //    command.CommandText += " AND p.SOUBEHGUID is not null";
            //}

            //// zobrazit vsechny korekce
            //if (filtr.KorekceVse)
            //{
            //    command.CommandText += " AND p.CORRGUID is not null";
            //}

            //// zobrazit nedokonceny odvod vyroby
            //if (filtr.OdvadeniNedokoncene)
            //{
            //    command.CommandText += " AND p.SOUBEHGUID not IN (" +
            //    " select distinct SOUBEHGUID from " + Fask.SQL.Constants.Common.TABLE_Production + 
            //    " where" +
            //    " SOUBEHGUID is not null and TIMESTOP is not null" +
            //    " )";
            //}
            //// zobrazit nedokoncene korekce
            //if (filtr.KorekceNedokoncene)
            //{
            //    //command.CommandText += " AND p.CORRGUID not IN (" +
            //    //" select distinct CORRGUID from Production" +
            //    //" where" +
            //    //" CORRGUID is not null and TIMECORSTOP is not null" +
            //    //" )";
            //    command.CommandText += " AND not exists ( " +
            //    " select SOUBEHGUID " +
            //    " from " + Fask.SQL.Constants.Common.TABLE_Production + 
            //    " where " +
            //    " (TIMESTOP is not null or TIMEPREPSTOP is not null) " +
            //    " and " +
            //    " SOUBEHGUID=p.SOUBEHGUID " +
            //    " ) " +
            //    " and p.SOUBEHGUID is not null "
            //    ;
            //}

            //if (filtr.PouzeNeschvalene)
            //{
            //    command.CommandText += " AND p.idVS is null";
            //}

            //if (!string.IsNullOrEmpty(filtr.TerminalID))
            //{
            //    command.CommandText += " AND p.TermID in (" + filtr.TerminalID + ")";
            //}


            //command.CommandText += " order by p.dateeve desc";

            //vyrobaDataSet1.Production_Konzola.Clear();
            //vyrobaDataSet1.Production_Konzola.AcceptChanges();
            //vyrobaDataSet1.Production_Konzola.BeginLoadData();
            //adapter.SelectCommand = command;
            //adapter.Fill(vyrobaDataSet1.Production_Konzola);
            //vyrobaDataSet1.Production_Konzola.EndLoadData();

            //return vyrobaDataSet1; 
            #endregion

            Globals_V1.LoadConfiguration();
            return Database.Vyroba_Production.GetFiltrovanyProductionList(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, filtr);
        }

        #endregion

        #region IProduction_FillByCORRGUID Members

        public void Production_FillByCORRGUID(Fask.Interfaces.DataSets.Vyroba ds, Guid CORRGUID)
        {
            //Globals_V1.LoadConfiguration();
            //ds.Production_Konzola.Clear();
            //Pohoda_DataSets.VyrobaDataSet.ProductionDataTable dt = new Pohoda_DataSets.VyrobaDataSet.ProductionDataTable();

            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //var tmp = ta.FillByCORRGUID(dt, CORRGUID);


            //foreach (var item in dt)
            //{
            //    ds.Production_Konzola.ImportRow(item);
            //}

            Globals_V1.LoadConfiguration();
            Database.Vyroba_Production.Production_FillByCORRGUID(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, ds, CORRGUID);

        }

        #endregion

        #region IProduction_FillBySOUBEHGUID Members

        public void Production_FillBySOUBEHGUID(Fask.Interfaces.DataSets.Vyroba ds, Guid SOUBEHGUID)
        {
            //Globals_V1.LoadConfiguration();
            //ds.Production_Konzola.Clear();
            //Pohoda_DataSets.VyrobaDataSet.ProductionDataTable dt = new Pohoda_DataSets.VyrobaDataSet.ProductionDataTable();

            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //var tmp = ta.FillBySOUBEHGUID(dt, SOUBEHGUID);


            //foreach (var item in dt)
            //{
            //    ds.Production_Konzola.ImportRow(item);
            //}

            Globals_V1.LoadConfiguration();
            Database.Vyroba_Production.Production_FillBySOUBEHGUID(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, ds, SOUBEHGUID);

        }

        #endregion

        #region IProduction_GetDataByCORRGUID Members

        public Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataByCORRGUID(Guid CORRGUID)
        {
            //Globals_V1.LoadConfiguration();
            //Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //var tmp = ta.GetDataByCORRGUID(CORRGUID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}

            //return dt;

            Globals_V1.LoadConfiguration();
            return Database.Vyroba_Production.Production_GetDataByCORRGUID(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, CORRGUID);

        }

        #endregion

        #region IProduction_GetDataBySOUBEHGUID Members

        public Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataBySOUBEHGUID(Guid SOUBEHGUID)
        {
            //Globals_V1.LoadConfiguration();
            //Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            //var tmp = ta.GetDataBySOUBEHGUID(SOUBEHGUID);


            //foreach (var item in tmp)
            //{
            //    dt.ImportRow(item);
            //}


            //return dt;

            Globals_V1.LoadConfiguration();
            return Database.Vyroba_Production.Production_GetDataBySOUBEHGUID(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, SOUBEHGUID);

        }

        #endregion

        #region IProduction_Update Members

        public void Production_Update(Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt)
        {
            //Globals_V1.LoadConfiguration();
            //var lta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            //lta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            //lta.Update(dt.ToArray());

            Globals_V1.LoadConfiguration();
            Database.Vyroba_Production.Update(dt, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

        }

        #endregion

        #region Production_ImportPrijemkaPohoda_Zdroj_P_PS Members

        public string Production_ImportPrijemkaPohoda_Zdroj_P_PS(int countEntries, string SKL_ID, string userID, string Vydejka)
        {
            //TODO PRedelat na upravenou verzi kde se komunikuje pomoci 
            // PohodaComunication.cs
            // a pouziva sa Objektove sestavovani XML souboru



            try
            {

                Globals_V1.LoadConfiguration();

                string filename;
                filename = XML.PohodaComunication.FilenameCompose(Fask.ModulePohodaXML.XML.PohodaComunication.prijem_import_prijemka);

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
                    throw new Exception("Nezdařilo se načteni konfigurace.");

                //Pohoda_DataSets.Prijem.ProductionDataTable dt_p = null;
                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt_p = null;


                if (Globals_V1.Konfigurace.Konzola[0].Prijemka_Vyroba_Grupuj)
                {
                    //dt_p = Database.Prijem.GETDATA_Grupuj_Production( countEntries, SKL_ID);
                    dt_p = Database.Vyroba_Production.GetDataByImportProduction(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, countEntries, SKL_ID);
                }
                else
                {
                    //dt_p = Database.Prijem.GETDATA_Production(countEntries, SKL_ID);
                    dt_p = Database.Vyroba_Production.GetDataByImportProduction(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, countEntries, SKL_ID);
                }


                if (!Prijem.CreateRequest_Import_Prijemka_XML_NEW(dt_p, filename, countEntries, SKL_ID, "", Vydejka))
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
                    //if (!Database.Prijem.UPDATEDATA_Production(dt_p, countEntries, SKL_ID))
                    //{
                    //    throw new Exception("Nepodařilo se aktualizovat odeslaná data výrobků!");
                    //}
                     Database.Vyroba_Production.Update(dt_p, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
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
                Globals_V1.LoadConfiguration();
                //nazev souboru s odpovedi
                string filename = Globals_V1.Konfigurace.PohodaInfo[0].PathToInputDirectory + "Response\\" + file;

                string actualStrName = string.Empty;
                string previousStrName = string.Empty;
                string actualSonnumber = string.Empty;

                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
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
                //Pohoda_DataSets.Prijem prijemDS = new Pohoda_DataSets.Prijem();
                Fask.Interfaces.DataSets.Vyroba prijemDS = new Interfaces.DataSets.Vyroba();

                //Pohoda_DataSets.PrijemTableAdapters.CZMST_PETableAdapter peAdapter = new Pohoda_DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
                //Pohoda_DataSets.PrijemTableAdapters.CZMST_PITableAdapter piAdapter = new Pohoda_DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
                //Pohoda_DataSets.PrijemTableAdapters.ProductionTableAdapter prod_Adapter = new Pohoda_DataSets.PrijemTableAdapters.ProductionTableAdapter();

                //peAdapter.Connection.ConnectionString = Globals.ConnectionString;
                //piAdapter.Connection.ConnectionString = Globals.ConnectionString;
                //prod_Adapter.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                //peAdapter.FillByCountEntries(prijemDS.CZMST_PE, countentries);
                //piAdapter.FillByCountEntries(prijemDS.CZMST_PI, countentries);
                //prod_Adapter.FillByCountEntries(prijemDS.Production, countentries);
                Database.Vyroba_Production.Production_FillByCountEntries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, prijemDS, countentries);

                //string cislodokladuobjednavky = prijemDS.CZMST_PE[0].PONUMBER.Trim();

                Pohoda_DataSets.DatabasePohoda pohodaDS = new Pohoda_DataSets.DatabasePohoda();
                oledbConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

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

                foreach (var pir in prijemDS.Production_Konzola)
                {
                    //neprirazene polozky dle itemnmbr a mnozstvi
                    string query = " RefSKz=" + pir.ITEMNMBR.Trim() + " AND Mnozstvi=" + pir.qty.ToString("0.00", CultureInfo.InvariantCulture) + " AND RefPol is NULL AND RelAgID is NULL";
                    Pohoda_DataSets.DatabasePohoda.SKPPpolRow[] neprirazenePolozky = (Pohoda_DataSets.DatabasePohoda.SKPPpolRow[])pohodaDS.SKPPpol.Select(
                        query
                        , "ID"
                        , System.Data.DataViewRowState.CurrentRows);
                    if (neprirazenePolozky.Length == 0)
                    { //nic se nedeje, protoze nebyly nalezeny => log... jde o chybu ...
                        Fask.Logging.ExceptionHandler2.Handle( Logging.LogLevel.Error,"Fask.ModulePohodaXML.Provider.Provider" , "LoadPrijemImportResponseXmlAndMakeUpdateDB",  "Parovani nenalezlo volnou shodu v databazi skpppol s polozkou:" + pir.ORD + "|" + pir.ITEMNMBR + "|" + pir.qty + "|" + pir.GUID.ToString());
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
                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML.Provider.Provider", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Nenalezena karta zasoby pro snizeni objednaneho mnozstvi:" + pir.ORD + "|" + pir.ITEMNMBR + "|" + pir.qty + "|" + pir.GUID.ToString());
                        else
                            skzObjedVRow.ObjedV -= (double)pir.qty; //pokud jiz bylo v cyklu drive snizeno, tak se snizi opet ...
                    }
                    else //je-li null, tak se nenaslo zbozi ... coz je chyba a bude zalogovana ...
                    {
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML.Provider.Provider", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo volnou shodu v databazi objpol s polozkou:" + pir.ORD + "|" + pir.ITEMNMBR + "|" + pir.qty + "|" + pir.GUID.ToString());
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
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Fask.ModulePohodaXML.Provider.Provider", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo shodu v databazi skzbuf s polozkou:" + item.ID + "|" + item.ObjedV);
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

                if (Globals_V1.Konfigurace.Konzola[0].Pohoda_Objednavka_Set_Vyrizeno)
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
                        Fask.Logging.ExceptionHandler2.Handle( exx);

                    }

                    throw ex;

                }

                #endregion

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


        #endregion

        #region IProduction_GetDataSelectListImport Members

        public Fask.Interfaces.DataSets.Vyroba Production_GetDataSelectListImport()
        {
            
            Globals_V1.LoadConfiguration();
            return Database.Vyroba_Production.Production_GetDataSelectListImport(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        }

        #endregion

        #region IProduction_GetFiltrovanyProductionVazby Members

        public Fask.Interfaces.DataSets.Vyroba Production_GetFiltrovanyProductionVazby(Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
        {
            Globals_V1.LoadConfiguration();
            return Database.Vyroba_Production.Production_GetFiltrovanyProductionVazby(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, filtr);
        }

        #endregion

        #region IProduction_ImportPrijemkaPohoda_Zdroj_P_TP Members

        public string Production_ImportPrijemkaPohoda_Zdroj_P_TP(int countEntries, string SKL_ID, string userID, string Vydejka)
        {
            try
            {
                string filename;
                filename = XML.PohodaComunication.FilenameCompose(Fask.ModulePohodaXML.XML.PohodaComunication.prijem_import_prijemka);

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
                    throw new Exception("Nezdařilo se načteni konfigurace.");


                //Pohoda_DataSets.Prijem.ProductionDataTable dt_p = null;
                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt_p = null;


                if (Globals_V1.Konfigurace.Konzola[0].Prijemka_Vyroba_Grupuj)
                {
                    //dt_p = Database.Prijem.GETDATA_Grupuj_Production( countEntries, SKL_ID);
                    dt_p = Database.Vyroba_Production.GetDataByImportProduction(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, countEntries, SKL_ID);
                }
                else
                {
                    //dt_p = Database.Prijem.GETDATA_Production(countEntries, SKL_ID);
                    dt_p = Database.Vyroba_Production.GetDataByImportProduction(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, countEntries, SKL_ID);
                }


                if (!Prijem.CreateRequest_Import_Prijemka_XML_NEW(dt_p, filename, countEntries, SKL_ID, "", Vydejka))
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
                    //if (!Database.Prijem.UPDATEDATA_Production(dt_p, countEntries, SKL_ID))
                    //{
                    //    throw new Exception("Nepodařilo se aktualizovat odeslaná data výrobků!");
                    //}
                    Database.Vyroba_Production.Update(dt_p, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
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

        #endregion

        #region IProduction_ImportPohoda_Zdroj_P_Vyroba Members

        public string Production_ImportPohoda_Zdroj_P_Vyroba(int countEntries, string SKL_ID, string userID)
        {
            try
            {
                string filename;
                filename = XML.PohodaComunication.FilenameCompose(Fask.ModulePohodaXML.XML.PohodaComunication.prijem_import_Vyroba);

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
                    throw new Exception("Nezdařilo se načteni konfigurace.");


                //Pohoda_DataSets.Prijem.ProductionDataTable dt_p = null;
                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt_p = null;


                if (Globals_V1.Konfigurace.Konzola[0].Prijemka_Vyroba_Grupuj)
                {
                    //dt_p = Database.Prijem.GETDATA_Grupuj_Production( countEntries, SKL_ID);
                    dt_p = Database.Vyroba_Production.GetDataByImportProduction(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, countEntries, SKL_ID);
                }
                else
                {
                    //dt_p = Database.Prijem.GETDATA_Production(countEntries, SKL_ID);
                    dt_p = Database.Vyroba_Production.GetDataByImportProduction(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, countEntries, SKL_ID);
                }


                if (!Classes.Vyroba.CreateRequest_Import_Vyroba_XML(dt_p, filename, countEntries, SKL_ID, ""))
                    throw new Exception("Nepodaril se create requestu.");

                string respfilename;
                if (!XML.PohodaComunication.Communicate(filename, out respfilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                pom = string.Empty;
                pom = Classes.Vyroba.LoadResponse_Import_Vyroba_XML(dt_p, respfilename, countEntries, SKL_ID);

                if (!string.IsNullOrEmpty(pom))
                {
                    //if (!Database.Prijem.UPDATEDATA_Production(dt_p, countEntries, SKL_ID))
                    //{
                    //    throw new Exception("Nepodařilo se aktualizovat odeslaná data výrobků!");
                    //}
                    Database.Vyroba_Production.Update(dt_p, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                }

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region #region IProduction_Delete_ISOK Members

        public void Production_Delete_ISOK(Fask.Interfaces.DataSets.Vyroba.Production_OdvodRow Row)
        {
            try
            {
                Globals_V1.LoadConfiguration();
                Database.Vyroba_Production.Update_ISOK_to_null_Production(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, Row.CountEntries,Row.ITEMNMBR, Row.SOUBEHGUID);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

        #region Production_ImportPrijemkaPohoda_Zdroj_P_PS Members

        public string Production_ImportPrijemkaPohoda_Zdroj_P(int countEntries, string userID, string SKL_ID)
        {
            try
            {

                Globals_V1.LoadConfiguration();

                string filename;
                filename = XML.PohodaComunication.FilenameCompose(Fask.ModulePohodaXML.XML.PohodaComunication.prijem_import_prijemka);

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
                    throw new Exception("Nezdařilo se načteni konfigurace.");

                Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt_p = null;
                dt_p = Database.Vyroba_Production.GetDataByImportProduction(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB, countEntries, SKL_ID);
          
                if (!Prijem.CreateRequest_Import_Prijemka_XML_V2(dt_p, filename, countEntries, ""))
                    throw new Exception("Nepodaril se create requestu.");

                string respfilename;
                if (!XML.PohodaComunication.Communicate(filename, out respfilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                pom = string.Empty;
                pom = Prijem.LoadResponse_Import_Prijemka_XML_V2(dt_p, respfilename, countEntries);

                if (!string.IsNullOrEmpty(pom))
                {
                    Database.Vyroba_Production.Update(dt_p, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                }

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

    }
}
