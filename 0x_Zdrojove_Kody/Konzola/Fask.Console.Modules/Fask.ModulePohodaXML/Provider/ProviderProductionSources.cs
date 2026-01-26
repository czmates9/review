using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider : 
        Fask.Console.Interfaces.Vyroba.IProductionSources,
        Fask.Console.Interfaces.Vyroba.IProductionSources_GetFiltrovanyProductionSourcesList,
        Fask.Console.Interfaces.Vyroba.IProductionSources_ImportVydejkaPohoda,
        Fask.Console.Interfaces.Vyroba.IProductionSources_GetDataSelectListImport
    {
        public const string vydej_import_vydejka = XML.PohodaComunication._import_vydejka + ".xml";

        #region IProductionSources_GetFiltrovanyProductionSourcesList Members

        public Production.DataServices.VyrobaDataSet GetFiltrovanyProductionSourcesList(Console.Interfaces.Classes.ProductionSourcesListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Production.DataServices.VyrobaDataSet vyrobaDataSet1 = new Production.DataServices.VyrobaDataSet();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;


            #region command
            command.CommandText = "Select l.firstname, l.surname" +
                ", sklady.skl_desc skladName" + ", sklady.skl_carcode skladKod" +
                ", lokace.Barcode lokaceKod, lokace.Description lokaceName" +
                ", hlavicky.SOPDESC popiszakazky" +
                //", polozky.ITEMDESC nazevvyrobku, polozky.BarcodeP EANvyrobku, polozky.VNDITNUM cislovyrobku";
                ", p.ITEMDESC nazevvyrobku, p.BarcodeP EANvyrobku, p.ITEMNMBR cislovyrobku" +
                ", fc.vnditnum as EANMaterialu";
            // TODO : ean vyrobku ??

            //TODO : EAN spotrebovaneho materialu, potreba rozsitit 
            //17.4.2018 Kod od JiS dotazeni caroveho kodu
            //select distinct x.itemnmbr, x.itemdesc, y.vnditnum, y.cz_carkod
            //from fask_cons_095 x
            //left join fask_cons_095 y on y.dex_row_id = (select top 1 dex_row_id from fask_cons_095 where itemnmbr=x.itemnmbr)

            command.CommandText += ", ps.* from Production_Sources ps" +
            " left join Production p on p.GUID=ps.GUID_Production" +
            " left join CZMST093 sklady on sklady.skl_id = ps.SKL_ID" +
            " left join CZMST094 lokace on lokace.skl_id = ps.SKL_ID and lokace.locncode=ps.LOCNCODE" +
            " left join CZPRO_VPH hlavicky on hlavicky.SOPNUMBE = ps.SOPNUMBE" +
                //" left join CZPRO_VPP polozky on polozky.CountEntries = ps.CountEntries and polozky.SOPNUMBE = ps.SOPNUMBE and polozky.ITEMNMBR = ps.ITEMNMBR ";
                //" left join (select distinct itemnmbr, itemdesc from fask_cons_095) polozky on polozky.itemnmbr=p.itemnmbr";
                //" left join FASK_CONS_095 fc on fc.dex_row_id = (select top 1 dex_row_id from fask_cons_095 where itemnmbr=ps.itemnmbr)";
            " left join (select itemnmbr, Min(vnditnum) vnditnum from fask_cons_095 group by itemnmbr) fc on fc.itemnmbr=ps.itemnmbr";

            command.CommandText += " left join Logins l on l.id = ps.USER_ID" +
                " Where ";

            command.CommandText += "1=1 ";
            // číslo davky
            if (!string.IsNullOrEmpty(filtr.CountEntries))
            {
                command.CommandText += " AND ps.CountEntries = @CountEntries";
                command.Parameters.AddWithValue("@CountEntries", filtr.CountEntries.Trim());
            }

            //vyrobny prikaz
            if (filtr.rowVPH != null)
            {
                command.CommandText += " AND ps.SOPNUMBE = @SOPNUMBE";
                command.Parameters.AddWithValue("@SOPNUMBE", filtr.rowVPH.SOPNUMBE);

            }
            else if (filtr.SOPNUMBE.Trim().Length > 0)
            {
                command.CommandText += " AND isnull(ps.SOPNUMBE, '') IN(" +
                    "Select SOPNUMBE from CZPRO_VPH where (SOPNUMBE like '%' + @SOPNUMBE + '%')" +
                    " union " +
                    "Select SOPNUMBE from CZPRO_VPH where (SOPDESC like '%' + @SOPNUMBE + '%')" +
                    ")";
                command.Parameters.AddWithValue("@SOPNUMBE", filtr.rowVPH != null ? filtr.rowVPH.SOPNUMBE.Trim() : filtr.SOPNUMBE.Trim());

            }
            if (filtr.EAN.Trim().Length > 0)
            {
                command.CommandText += " AND (" +
                    " polozky.BarcodeP like '%' + @EAN + '%'" +
                    " or " +
                    " polozky.VNDITNUM like '%' + @EAN + '%' " +
                    ")";
                command.Parameters.AddWithValue("@EAN", filtr.EAN.Trim());

            }


            //sklad
            if (filtr.rowSKLAD != null)
            {
                command.CommandText += " AND ps.SKL_ID = @SKL_ID";
            }
            else if (filtr.SKLAD.Trim().Length > 0)
            {
                command.CommandText += " AND isnull(ps.SKL_ID, '') like '%' + @SKL_ID + '%'";
            }
            command.Parameters.AddWithValue("@SKL_ID", filtr.rowSKLAD != null ? filtr.rowSKLAD.skl_id.Trim() : filtr.SKLAD.Trim());

            //lokace
            if (filtr.rowLokace != null)
            {
                command.CommandText += " AND ps.LOCNCODE = @LOCNCODE";
                command.Parameters.AddWithValue("@LOCNCODE", filtr.rowLokace.LOCNCODE.Trim());
            }
            else if (filtr.LOCNCODE.Trim().Length > 0)
            {
                command.CommandText +=
                    " AND isnull(ps.LOCNCODE, '') IN (" +
                    "Select LOCNCODE from czmst094 where (Barcode like '%' + @BarcodeL + '%')" +
                    " union " +
                    "Select LOCNCODE from czmst094 where (Description like '%' + @BarcodeL + '%')" +
                    ")"; //"like '%' + @LOCNCODE + '%'";
                command.Parameters.AddWithValue("@BarcodeL", filtr.LOCNCODE.Trim());
            }
            //command.Parameters.AddWithValue("@LOCNCODE", rowLokace != null ? rowLokace.LOCNCODE.Trim() : ComboBoxLOCNCODE.Text);

            if (filtr.VyrobaPouzivatTabulkuZbozi)
            {
                // hledaní material
                if (filtr.rowZbozi != null)
                {
                    command.CommandText += " AND ps.ITEMNMBR=@itemdesc";
                    command.Parameters.AddWithValue("@itemdesc", filtr.rowZbozi.ITEMNMBR.Trim());
                }
                else if (filtr.ZBOZI.Trim().Length > 0)
                {
                    command.CommandText += " AND ps.ITEMNMBR IN (" +
                    " select ITEMNMBR from FASK_CONS_095" +
                    " where ITEMDESC like '%' + @itemdesc + '%'" +
                    " union" +
                    " select ITEMNMBR from FASK_CONS_095" +
                    " where ITEMNMBR like '' + @itemdesc + '%' " +
                    " )";

                    command.Parameters.AddWithValue("@itemdesc", filtr.rowZbozi != null ? filtr.rowZbozi.ITEMNMBR.Trim() : filtr.ZBOZI.Trim());
                }


                // hledaní vyrobek
                if (filtr.rowVyrobek != null)
                {
                    command.CommandText += " AND p.ITEMNMBR=@itemnmbr";
                    command.Parameters.AddWithValue("@itemnmbr", filtr.rowVyrobek.ITEMNMBR.Trim());
                }
                else if (filtr.VYROBEK.Trim().Length > 0)
                {
                    command.CommandText += " AND p.ITEMNMBR IN (" +
                    " select ITEMNMBR from FASK_CONS_095" +
                    " where ITEMDESC like '%' + @itemnmbr + '%'" +
                    " union" +
                    " select ITEMNMBR from FASK_CONS_095" +
                    " where ITEMNMBR like '' + @itemnmbr + '%' " +
                    " )";

                    command.Parameters.AddWithValue("@itemnmbr", filtr.rowZbozi != null ? filtr.rowZbozi.ITEMNMBR.Trim() : filtr.VYROBEK.Trim());
                }
            }
            // Typ polozky
            if (!string.IsNullOrEmpty(filtr.ITEMTYPE))
            {
                command.CommandText += " AND ps.ITEMTYPE = @ITEMTYPE";
                command.Parameters.AddWithValue("@ITEMTYPE", filtr.ITEMTYPE.Trim());
            }

            // car. kod polozky
            if (!string.IsNullOrEmpty(filtr.ITEMCODE))
            {
                command.CommandText += " AND ps.ITEMCODE = @ITEMCODE";
                command.Parameters.AddWithValue("@ITEMCODE", filtr.ITEMCODE.Trim());
            }

            // merna jednotka
            if (!string.IsNullOrEmpty(filtr.MJ))
            {
                command.CommandText += " AND ps.MJ = @MJ";
                command.Parameters.AddWithValue("@MJ", filtr.MJ.Trim());
            }
            // seriove cislo
            if (!string.IsNullOrEmpty(filtr.SERLTNUM))
            {
                command.CommandText += " AND ps.SERLTNUM = @SERLTNUM";
                command.Parameters.AddWithValue("@SERLTNUM", filtr.SERLTNUM.Trim());
            }

            if (filtr.DatumOd && filtr.DatumDo)
            {
                command.CommandText += " AND ps.ISOK between @datumOd and @datumDo";
                command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
                command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
            }
            else
            {
                if (filtr.DatumOd)
                {
                    command.CommandText += " AND ps.ISOK > @datumOd";
                    command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
                }
                else if (filtr.DatumDo)
                {
                    command.CommandText += " AND ps.ISOK < @datumDo";
                    command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
                }
            }


            // hledání uživatele
            if (!string.IsNullOrEmpty(filtr.Uzivatel))
            {
                if (filtr.Uzivatel != null)
                {
                    command.CommandText += " AND ps.USER_ID=@name";
                }
                else
                {
                    command.CommandText += " AND ps.USER_ID IN (" +
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

            // ID terminal
            if (!string.IsNullOrEmpty(filtr.TERMINAL_ID))
            {
                command.CommandText += " AND ps.TERMINAL_ID = @TERMINAL_ID";
                command.Parameters.AddWithValue("@TERMINAL_ID", filtr.TERMINAL_ID.Trim());
            }
            // cislo palety
            if (!string.IsNullOrEmpty(filtr.NMBRPAL))
            {
                command.CommandText += " AND ps.NMBRPAL = @NMBRPAL";
                command.Parameters.AddWithValue("@NMBRPAL", filtr.NMBRPAL.Trim());
            }
            // typ palety
            if (!string.IsNullOrEmpty(filtr.TYPEPAL))
            {
                command.CommandText += " AND ps.TYPEPAL = @TYPEPAL";
                command.Parameters.AddWithValue("@TYPEPAL", filtr.TYPEPAL.Trim());
            }
            // vytisknuto
            if (!string.IsNullOrEmpty(filtr.PRINTED))
            {
                command.CommandText += " AND ps.PRINTED = @PRINTED";
                command.Parameters.AddWithValue("@PRINTED", filtr.PRINTED.Trim());
            }

            command.CommandText += " order by ps.ISOK desc";

            #endregion


            vyrobaDataSet1.Production.Clear();
            vyrobaDataSet1.Production.AcceptChanges();
            vyrobaDataSet1.Production.BeginLoadData();
            adapter.SelectCommand = command;
            adapter.Fill(vyrobaDataSet1.Production_Sources);
            vyrobaDataSet1.Production.EndLoadData();

            return vyrobaDataSet1;
        }

        #endregion

        #region IProductionSources_ImportVydejkaPohoda Members

        public string ImportVydejkaPohoda(int countEntries, string SKL_ID, Console.Interfaces.DataSets.Konzola.FASK_CONS_LoginsRow userID)
        {
            try{

                string filename;
                filename = XML.PohodaComunication.FilenameCompose(vydej_import_vydejka);

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
                    return pom;

                Pohoda_DataSets.Vydej.Production_SourcesDataTable dt_ps = null;
                dt_ps = Database.Vydej.GETDATA_ProductionSources(countEntries, SKL_ID);

                //TaD Nova metoda na vytvoreni xml
                if (!Vydej.CreateRequest_Import_Vydejka_XML(dt_ps,filename, countEntries,SKL_ID, ""))
                    return "CHYBA";

                bool saveToDB = true;

                //Fask.Server.Interfaces.Classes.User user = new Fask.Server.Interfaces.Classes.User();
                //user.ID = userID;

                string respfilename;
                if (!XML.PohodaComunication.Communicate(filename, out respfilename))
                { 
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                ////TaD nova metoda na update
                //pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, null, userID);

                //if (pom != "OK")
                //    return pom;

                //TaD upravyt na vydej...
                //Parovani a update objednavka/prijemka v pohoda ... 
                //pom = LoadPrijemImportResponseXMLAndMakeUpdateDB(filename, countEntries);

                if (Vydej.LoadResponse_Import_Vydejka_XML(dt_ps, respfilename, countEntries, SKL_ID))
                {
                    if (!Database.Vydej.UPDATEDATA_ProductionSources(dt_ps, countEntries, SKL_ID))
                    {
                        throw new Exception("Nepodařilo se aktualizovat odeslaná data materiálů!");
                    }
                }

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IProductionSources_GetDataSelectListImport Members

        Production.DataServices.VyrobaDataSet Console.Interfaces.Vyroba.IProductionSources_GetDataSelectListImport.GetDataSelectListImport()
        {
            Production.DataServices.VyrobaDataSet dsout = new Production.DataServices.VyrobaDataSet();
           Pohoda_DataSets.Vydej ds = new Pohoda_DataSets.Vydej();
            try
            {

                Pohoda_DataSets.VydejTableAdapters.Production_SourcesImportTableAdapter ta = new Pohoda_DataSets.VydejTableAdapters.Production_SourcesImportTableAdapter();
                ta.Connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);

                ta.Fill(ds.Production_SourcesImport); // Fill_GroupByCountEntriesSOPNUMBE(ds.Production_Sources);


                foreach (System.Data.DataRow row in ds.Production_SourcesImport)
                {
                    var RowNew = dsout.Production_SourcesImport.NewProduction_SourcesImportRow();

                    foreach (System.Data.DataColumn Column in row.Table.Columns)
                    {
                        try { RowNew[Column.ColumnName] = row[Column.ColumnName]; }
                        catch { continue; }
                    }
                    dsout.Production_SourcesImport.AddProduction_SourcesImportRow(RowNew);
                }


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
