using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider :
        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources,
        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetFiltrovanyProductionSourcesList,
        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_Update_Row,
        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_Update,

        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_ImportVydejkaPohoda,
        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetDataSelectListImport,

        Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetFiltrovanyVazby
    {

        public const string vydej_import_vydejka = XML.PohodaComunication._import_vydejka + ".xml";
        public Guid? GUID_Production { get; set; }

        #region IProductionSources_GetFiltrovanyProductionSourcesList Members

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyProductionSourcesList(Fask.Interfaces.Filtry.ProductionSourcesListFiltr filtr)
        {
            Globals_V1.LoadConfiguration();
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
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
            //from FASK_ZASOBY x
            //left join FASK_ZASOBY y on y.dex_row_id = (select top 1 dex_row_id from FASK_ZASOBY where itemnmbr=x.itemnmbr)

            command.CommandText += ", ps.* from " + Fask.SQL.Constants.Common.TABLE_Production_Sources + " ps" +
            " left join " + Fask.SQL.Constants.Common.TABLE_Production + " p on p.GUID=ps.GUID_Production" +
            " left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklady on sklady.skl_id = ps.SKL_ID" +
            " left join " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " lokace on lokace.skl_id = ps.SKL_ID and lokace.locncode=ps.LOCNCODE" +
            " left join " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " hlavicky on hlavicky.SOPNUMBE = ps.SOPNUMBE" +
            " left join (select itemnmbr, Min(vnditnum) vnditnum from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " group by itemnmbr) fc on fc.itemnmbr=ps.itemnmbr";

            command.CommandText += " left join " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " l on l.USERID = ps.USER_ID" +
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
                    "Select SOPNUMBE from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " where (SOPNUMBE like '%' + @SOPNUMBE + '%')" +
                    " union " +
                    "Select SOPNUMBE from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " where (SOPDESC like '%' + @SOPNUMBE + '%')" +
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
                    "Select LOCNCODE from " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " where (Barcode like '%' + @BarcodeL + '%')" +
                    " union " +
                    "Select LOCNCODE from " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " where (Description like '%' + @BarcodeL + '%')" +
                    ")"; //"like '%' + @LOCNCODE + '%'";
                command.Parameters.AddWithValue("@BarcodeL", filtr.LOCNCODE.Trim());
            }
            //command.Parameters.AddWithValue("@LOCNCODE", rowLokace != null ? rowLokace.LOCNCODE.Trim() : ComboBoxLOCNCODE.Text);

            if (filtr.VyrobaPouzivatTabulkuZbozi)
            {
                // hledaní material
                if (filtr.rowZASOBY != null)
                {
                    command.CommandText += " AND ps.ITEMNMBR=@itemdesc";
                    command.Parameters.AddWithValue("@itemdesc", filtr.rowZASOBY.ITEMNMBR.Trim());
                }
                else if (filtr.ZBOZI.Trim().Length > 0)
                {
                    command.CommandText += " AND ps.ITEMNMBR IN (" +
                    " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
                    " where ITEMDESC like '%' + @itemdesc + '%'" +
                    " union" +
                    " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + 
                    " where ITEMNMBR like '' + @itemdesc + '%' " +
                    " )";

                    command.Parameters.AddWithValue("@itemdesc", filtr.rowZASOBY != null ? filtr.rowZASOBY.ITEMNMBR.Trim() : filtr.ZBOZI.Trim());
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
                    " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + 
                    " where ITEMDESC like '%' + @itemnmbr + '%'" +
                    " union" +
                    " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + 
                    " where ITEMNMBR like '' + @itemnmbr + '%' " +
                    " )";

                    command.Parameters.AddWithValue("@itemnmbr", filtr.rowZASOBY != null ? filtr.rowZASOBY.ITEMNMBR.Trim() : filtr.VYROBEK.Trim());
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
                    " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS+
                    " where firstname like '%' + @name + '%'" +
                    " union" +
                    " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    " where surname like '%' + @name + '%'" +
                    " union" +
                    " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    " where USERID = @name" +
                    " )";
                }
                command.Parameters.AddWithValue("@name", filtr.rowUzivatel != null ? filtr.rowUzivatel.USERID.Trim() : filtr.Uzivatel);
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


            vyrobaDataSet1.Production_Sources.Clear();
            vyrobaDataSet1.Production_Sources.AcceptChanges();
            vyrobaDataSet1.Production_Sources.BeginLoadData();
            adapter.SelectCommand = command;
            adapter.Fill(vyrobaDataSet1.Production_Sources);
            vyrobaDataSet1.Production_Sources.EndLoadData();

            return vyrobaDataSet1;
        }

        #endregion

        #region IProductionSources_Update_Row Members

        public void Update_Row(Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow Row)
        {
            //Globals_V1.LoadConfiguration();
            //Pohoda_DataSets.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            //ta.Update(Row);

            Globals_V1.LoadConfiguration();
            Database.Vyroba_Production_Sources.Update(Row, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        }

        #endregion

        #region IProductionSources_Update Members

        public void Update(Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt)
        {
            //Globals_V1.LoadConfiguration();
            //var ta = new Pohoda_DataSets.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter();
            //ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            //ta.Update(dt.ToArray());

            Globals_V1.LoadConfiguration();
            Database.Vyroba_Production_Sources.Update(dt, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        }

        #endregion

        #region IProductionSources_ImportVydejkaPohoda Members



        /// <summary>
        /// Zpracuje výdejku materiálu pro systém POHODA.
        /// Provádí kontrolu disponibilního množství, vytváří XML požadavek, komunikuje se systémem Pohoda
        /// a aktualizuje databázi na základě odpovědi.
        /// </summary>
        /// <param name="countEntries">ID dávky (číselník).</param>
        /// <param name="SKL_ID">Identifikátor skladu, kde je prováděn výdej.</param>
        /// <param name="userID">ID uživatele, který operaci provádí.</param>
        /// <param name="smazat_PS">Příznak určující, zda se mají data o výrobě smazat, pokud selže kontrola disponibility.</param>
        /// <param name="PreskocDisp">Pokud je true, kontrola disponibilního množství se přeskočí.</param>
        /// <returns>Status operace typu <c>StatusInfo_Dispo</c>, obsahující výsledek a případná chybová data.</returns>
        /// <exception cref="Exception">Vyhozena, pokud dojde k chybě při konfiguraci, tvorbě XML, komunikaci s POHODOU nebo aktualizaci databáze.</exception>

        public Fask.Interfaces.Classes.StatusInfo_Dispo ImportVydejkaPohoda(int countEntries, string SKL_ID, string userID, bool smazat_PS, bool PreskocDisp)
        {
            try
            {
                Fask.Interfaces.Classes.StatusInfo_Dispo si = null;
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

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    throw new Exception("Nenačtena konfigurace.");

                Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dt_ps = null;



                if (Globals_V1.Konfigurace.Konzola[0].Vydejka_Vyroba_Grupuj)
                {
                    //dt_ps = Database.Vydej.GETDATA_Group_ProductionSources(countEntries, SKL_ID);
                    dt_ps = Database.Vydej.GETDATA_ProductionSources(countEntries, SKL_ID);
                }
                else
                {
                    dt_ps = Database.Vydej.GETDATA_ProductionSources(countEntries, SKL_ID);
                }

                #region Validace Dat

                if (!PreskocDisp)
                {
                    Fask.POHODA.Disponibility.ValidateData dsDisp = new POHODA.Disponibility.ValidateData();

                    foreach (Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow item in dt_ps)
                    {
                        Fask.POHODA.Disponibility.ValidateData.DataDispRow Row = dsDisp.DataDisp.NewDataDispRow();

                        Row.DEX_ROW_ID = item.DEX_ROW_ID;
                        Row.ITEMNMBR = item.ITEMNMBR;
                        Row.ITEMDESC = item.ITEMNAME;
                        Row.SKL_ID = item.SKL_ID;
                        Row.QTY = item.QTYSHPPD;
                        //Row.SOPNUMBE = item.IsSOPNUMBENull() ? null : item.SOPNUMBE;
                        Row.SetSOPNUMBENull();
                        Row.SetORDNull();
                        Row.SetSKz_RezerNull();
                        Row.SetSKz_StavZNull();
                        Row.SetOBJ_RezerNull();
                       // Row.SKL_ID = item.SKL_ID;

                        dsDisp.DataDisp.AddDataDispRow(Row);
                    }

                    Fask.POHODA.Disponibility.CheckDisp dispClass = new Fask.POHODA.Disponibility.CheckDisp();
                    Fask.POHODA.Disponibility.ValidateData.VydejKontrolaDataTable dt_dispo = dispClass.KontrolaDisponibilityDT(dsDisp, Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

                    Fask.POHODA.Disponibility.ValidateData.VydejKontrolaDataTable ChyboveData = new POHODA.Disponibility.ValidateData.VydejKontrolaDataTable();

                    foreach (var item in dt_dispo)
                    {
                        #region puvodny
                        if ((item.STAV_SKLAD >= 0) && (item.ZBUDE >= 0) && (item.STAV_SKLAD >= item.ZBUDE))
                        //if (item.STAV_SKLAD >= 0)
                        {
                            //Tady projde každa položka ktera je disponibilny
                        }
                        else
                        {
                            //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                            ChyboveData.ImportRow(item);
                        }
                        #endregion

                        #region 8.1.2020 ZdD + JaS uprava podminek

                        if (item.IsREZ_JANull() || string.IsNullOrEmpty(item.REZ_JA))
                        {

                            if ((item.STAV_SKLAD >= 0) && (item.ZBUDE >= 0) && (item.STAV_SKLAD >= item.ZBUDE))
                            {
                                //Tady projde každa položka ktera je disponibilny
                            }
                            else
                            {
                                //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                                ChyboveData.ImportRow(item);
                            }

                        }
                        else
                        {
                            if ((item.STAV_SKLAD >= 0))
                            {
                                //Tady projde každa položka ktera je disponibilny
                            }
                            else
                            {
                                //v tetpo variante rozhovovani když to narazi na první nedisponibilni položku tak ji to pošle ven. zbytek neřeší
                                ChyboveData.ImportRow(item);
                            }
                        }

                        #endregion
                    }

                    if ((ChyboveData != null) && (ChyboveData.Count > 0))
                    {
                        if (smazat_PS)
                        {
                            //V tomto kroku když neprošla Diponibilita tak smazat tabulku ProductionSources 
                            Database.Vydej.DELETEDATA_ProductionSources(countEntries, SKL_ID);
                        }

                        si = new Fask.Interfaces.Classes.StatusInfo_Dispo(-1, "ERROR", dt_dispo);
                        return si;

                    } 
                }


                #endregion


                //TaD Nova metoda na vytvoreni xml
                if (!Vydej.CreateRequest_Import_Vydejka_XML_NEW(dt_ps, filename, countEntries, SKL_ID, ""))
                    throw new Exception("Tvorba XML Requestu se nezdařila");

                //bool saveToDB = true;

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

                pom = string.Empty;

                pom = Vydej.LoadResponse_Import_Vydejka_XML(dt_ps, respfilename, countEntries, SKL_ID);

                si = new Fask.Interfaces.Classes.StatusInfo_Dispo(0, pom);

                if (!string.IsNullOrEmpty(pom))
                {
                    if (!Database.Vydej.UPDATEDATA_ProductionSources(dt_ps))
                    {
                        throw new Exception("Nepodařilo se aktualizovat odeslaná data materiálů!");
                    }
                }
                else
                {
                    throw new Exception("Nenalezeno číslo vydejky.");
                }

                return si;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IProductionSources_GetDataSelectListImport Members

        Fask.Interfaces.DataSets.Vyroba Fask.Interfaces.Vyroba.ProductionSources.IProductionSources_GetDataSelectListImport.GetDataSelectListImport()
        {
            Fask.Interfaces.DataSets.Vyroba dsout = new Fask.Interfaces.DataSets.Vyroba();
            Pohoda_DataSets.Vydej ds = new Pohoda_DataSets.Vydej();
            try
            {
                Globals_V1.LoadConfiguration();
                Pohoda_DataSets.VydejTableAdapters.Production_SourcesImportTableAdapter ta = new Pohoda_DataSets.VydejTableAdapters.Production_SourcesImportTableAdapter();
                ta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

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
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return dsout;
        }

        #endregion


        #region IProductionSources_GetFiltrovanyVazby Members

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVazby(Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
        {
            Globals_V1.LoadConfiguration();
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;


            #region command

            command.CommandText  = " SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_Production_Sources + " as PS ";
            command.CommandText += " LEFT JOIN CZMST093 as sklad ON sklad.skl_id = PS.SKL_ID";
            command.CommandText += " Where ";
            command.CommandText += " 1=1 ";

            

            if (GUID_Production != null)
            {
                command.CommandText += " AND PS.GUID_Production=@GUID_Production";
                command.Parameters.AddWithValue("@GUID_Production", GUID_Production);
                
            }

            if (!string.IsNullOrEmpty(filtr.MaterialITEMNMBR))
            {
                command.CommandText += " AND PS.ITEMNMBR=@ITEMNMBR";
                command.Parameters.AddWithValue("@ITEMNMBR", filtr.MaterialITEMNMBR.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.MaterialITEMDESC))
            {
                command.CommandText += " AND PS.ITEMNAME like @ITEMNAME + '%'";

                command.Parameters.AddWithValue("@ITEMNAME", filtr.MaterialITEMDESC.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.MaterialMJ))
            {
                command.CommandText += " AND PS.MJ=@MJ";
                command.Parameters.AddWithValue("@MJ", filtr.MaterialMJ.Trim());
            }

            //21.11.2025 MaR pridan filtr-------------------------------------------------
            if (!string.IsNullOrEmpty(filtr.CountEntries))
            {
                command.CommandText += " AND PS.CountEntries=@CountEntries";
                command.Parameters.AddWithValue("@CountEntries", filtr.CountEntries.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.SOPNUMBE))
            {
                command.CommandText += " AND PS.SOPNUMBE=@SOPNUMBE";
                command.Parameters.AddWithValue("@SOPNUMBE", filtr.SOPNUMBE.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.SKL_ID))
            {
                command.CommandText += " AND PS.SKL_ID=@SKL_ID";
                command.Parameters.AddWithValue("@SKL_ID", filtr.SKL_ID.Trim());
            }

            if (filtr.PotvrzeniPriznak)
            {
                command.CommandText += " AND PS.idVS IS NULL";
            }
            //---------------------------------------------------------------------------


            #endregion


            vyrobaDataSet1.Production_Sources_Odvod.Clear();
            vyrobaDataSet1.Production_Sources_Odvod.AcceptChanges();
            vyrobaDataSet1.Production_Sources_Odvod.BeginLoadData();
            adapter.SelectCommand = command;
            adapter.Fill(vyrobaDataSet1.Production_Sources_Odvod);
            vyrobaDataSet1.Production_Sources_Odvod.EndLoadData();

            return vyrobaDataSet1;
        }

        #endregion


    }
}
