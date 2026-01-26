using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

namespace Fask.SQL
{
    public static class Vydej
    {

        //private static Datasets.Vydej VydejDS = new Datasets.Vydej();

        #region Faktury Vydane
        /// <summary>
        /// Export dokladu Vydane Faktury (Issued Invoice)
        /// </summary>
        /// <param name="file">nazev souboru kam se request ulozi</param>
        /// <param name="objednavka">id dokladu</param>
        /// <returns>True : ok; False(exception) : nepovedlo se sestaveni nebo ulozeni(False: bez vyjimky, duvod neznamy; Exception: vyjimka)</returns>
        public static bool CreateRequest_FakturaVydana_XML(string filename, Fask.Server.Interfaces.Classes.Objednavka objednavka)
        {
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

            try
            {

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
                    new XAttribute("id", "issuedInvoice"),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Fask export issued invoice"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "issuedInvoiceRequest"),
                            new XAttribute("version", "2.0"),

                                new XElement(lst + "listInvoiceRequest",
                                    new XAttribute("version", "2.0"),
                                    new XAttribute("invoiceType", "issuedInvoice"),
                                    new XAttribute("invoiceVersion", "2.0"),
                                        new XElement(lst + "requestInvoice", mainFilter)
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
        /// Nacte data z odpovedi faktury vydane
        /// </summary>
        /// <param name="filename">cesta ke xml souboru</param>
        public static string LoadResponse_FakturaVydana_XML(string filename, Fask.Server.Interfaces.Classes.Objednavka objednavka)
        {
            XML.MST_Pohoda.CheckExistResponseFile(filename);

            //Datasety pro komunikaci a zjistovani dat ...
            //Datasets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzTableAdapter();
            //SKzTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

            // dotazeni parametru pro locncode (vychozi lokaci ..)
            //Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
            //SKzParametryTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

            //Datasets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
            //CZMST_SETableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;


            Datasets.Vydej.CZMST_SEDataTable seTable = new Datasets.Vydej.CZMST_SEDataTable();
            Datasets.Vydej.CZMST_SERow seRow = null;

			Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
			if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
				ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			}

            // HAVETO : Vice uzivatelsky pristup pri generovani dalsiho cisla davky je problematicky timto zpusobem...
            // Melo by byt reseno nejakym jinym mechanismem !!!
            //int cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries() + 1;
            int cisloDavky = 0;

            //Nacteni odpovedi ... 
            // - pokud neexistuje, tak se vyvola vyjimka ... ??? => mozna radeji nejdrive otestovat?




			int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
			int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
			

            //Pouzite namespacy
            XNamespace rsp = "http://www.stormware.cz/schema/version_2/response.xsd";
            XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd"; 
            XNamespace inv = "http://www.stormware.cz/schema/version_2/invoice.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

            // nacteni hodnot z odpovedi ...
            XDocument root = XDocument.Load(filename);

            XElement responsePack = root.Element(rsp + "responsePack");
            XAttribute state = responsePack.Attribute("state");
            if (state.Value != "ok")
            {
                XAttribute note = responsePack.Attribute("note");
                throw new Exception("Nepodařilo se získat data faktury.\nStatus:" + state.Value + "\nChyba:" + note.Value);
            }
            XElement responsePackItem = responsePack.Element(rsp + "responsePackItem");
            XAttribute responsePackItemState = responsePackItem.Attribute("state");
            if (responsePackItemState.Value.Trim() != "ok")
            {
                XAttribute responsePackItemNote = responsePackItem.Attribute("note");
                throw new Exception("Nepodařilo se získat data faktury.\nStatus:" + responsePackItemState.Value + "\nChyba:" + responsePackItemNote.Value);
            }

            // ok hlavicka je v poradku
            // dal overit vracenou fakturu => ocekavam prave jednu
            XElement listInvoice = responsePackItem.Element(lst + "listInvoice");
            XAttribute listInvoiceState = listInvoice.Attribute("state");
            if (listInvoiceState.Value != "ok")
            {
				// \TODO : docilit nejake chyby ...??? => jak je chyba indikovana??? <rdc:details>???
                throw new Exception("Nepodařilo se získat data faktury.\nStatus:" + state.Value);
            }
            // ok => bez chyby ... fakturu ...
            XElement invoice = listInvoice.Element(lst + "invoice");
            // Nemusi obsahovat data => faktura neexistuje, respektive nebyla urcena pro zpracovani??? 
            // => filtrovat dotaz na zaklade nejakeho priznaku nebo uzivatelskeho filtru ???
            if (invoice == null)
            {
                throw new Exception("Faktura nebyla nalezena!");
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

            XElement invoiceHeader = invoice.Element(inv + "invoiceHeader");
            XElement invoiceDetail = invoice.Element(inv + "invoiceDetail");
            XElement invoiceSummary = invoice.Element(inv + "invoiceSummary");

            //1) hlavicka ...
            XElement numberRequested = invoiceHeader.Element(inv + "number").Element(typ + "numberRequested");
            if (!numberRequested.Value.Trim().Equals(objednavka.ID.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("Číslo požadovaného dokladu se liší!");
            }
            XElement invoiceText = invoiceHeader.Element(inv + "text");

            //2) polozky
            // vsechny polozky
            var invoiceItems = invoiceDetail.Elements(inv + "invoiceItem");
            // pro kazdou polozku provest ulozeni do mst tabulky ...
            // Nejlepe v transakci ...
            foreach (var invoiceItem in invoiceItems)
            {
                // Pokud to neni skladova polozka, tak ji neresit ...
				// \TODO : otestovat, zda to takto je ... => tedy test na NULL hodnotu...
                XElement stockItem = invoiceItem.Element(inv + "stockItem");
                if (stockItem == null)
                    continue;

                XElement typEAN = stockItem.Element(typ + "stockItem").Element(typ + "EAN");
                XElement typPLU = stockItem.Element(typ + "stockItem").Element(typ + "PLU");
                XElement typIDS = stockItem.Element(typ + "stockItem").Element(typ + "ids");
                //Datasets.DatabasePohoda.SKzDataTable skz_table = SKzTableAdapter.GetDataByID(int.Parse(stockItem.Element(typ + "stockItem").Element(typ + "id").Value));
				Datasets.DatabasePohoda.SKzDataTable skz_table = Database.Pohoda.SKz_GetDataByID(int.Parse(stockItem.Element(typ + "stockItem").Element(typ + "id").Value));
				Datasets.DatabasePohoda.SKzRow skz_row = skz_table.Count > 0 ? skz_table[0] : null;
                
                // dotazeni vychozi lokace pro zasobu
                string locncodeDeafult = string.Empty;
                if (skz_row != null) 
                {
                    if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                    {
                        //Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, skz_row.ID);
						Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, skz_row.ID);
                        Datasets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                        if (skzParametry_row != null)
                        {
                            locncodeDeafult = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                        }
                    }
                }

                seRow = seTable.NewCZMST_SERow();

                seRow.ITEMDESC = invoiceItem.Element(inv + "text").Value.Trim(); // nazev polozky => z xml <inv:text>
                if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
                {
					seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
                }

                seRow.ITEMNMBR = stockItem.Element(typ + "stockItem").Element(typ + "id").Value.Trim(); // cislo polozky => z xml <inv:stockItem><typ:stockItem><typ:id>
                seRow.ITEMTYPE = ""; //bez typu
                //seRow.ITEMCODE = ""; // ? ITEMCODE neni???
				seRow.CountEntries = cisloDavky; // \TODO : ??? nove cislo davky ... 
                //seRow.CZ_CarKod = typPLU == null ? string.Empty : typPLU.Value; //carovy kod => dotahnout z xml PLU
                seRow.CZ_CarKod = typIDS == null ? string.Empty : typIDS.Value.Trim();
                seRow.CZ_DatVyr_Delka = 0; //?
                seRow.CZ_DatVyr_Track = 0; //?
                seRow.CZ_Doslo = 0; // pripraveno pro zpracovani
                seRow.CZ_SerNum_Delka = 0; //? delka serioveho cisla 
                //seRow.CZ_SerNum_Track = 0; //? sledovat na SN/Sarze => je sledovano na seriove cislo => zjistit v Pohoda
                //seRow.CZ_SerNum_Track = (byte)(skz_row == null ? 0 : skz_row.RelSKzVC);
                
                //TaD 19.9.2018 Uprava prace s SerNumTrack
                //seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack(
                //    (skz_row == null) || (skz_row.IsRelSKzVCNull()) ? (int?)null : (int?)skz_row.RelSKzVC
                //    );


				//if (Properties.Settings.Default.pohodaXfaskPouzitVPrFXTS)
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
				//    seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((skz_row == null) || (skz_row.IsRelSKzVCNull()) ? (int?)null : (int?)skz_row.RelSKzVC,true);
				//}

				if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, skz_row, Classes.Pohoda.TypAgendy.Vydej);
				}
				else
				{
					Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
					var dt_param = ParamTA.GetDataByITEMNMBR(seRow.ITEMNMBR.Trim());

					if ((dt_param != null) && (dt_param.Count > 0))
					{
						dt_row_param = dt_param.First();
					}

					seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, dt_row_param, Classes.Pohoda.TypAgendy.Vydej);
				}

                seRow.CZ_SW_Delka = 0; //?
                seRow.CZ_SW_Track = 0; //?
                //seRow.DEX_ROW_ID
                seRow.LOCNCODE = locncodeDeafult;
                seRow.Note = ""; //? poznamka 
                seRow.ORD = int.Parse(invoiceItem.Element(inv + "id").Value.Trim()); //? poradi polozky na dokladu - pro pohoda id zaznamu v tabulce polozek <inv:id>
                seRow.PRINTED = 0;
                seRow.PRIORITY = 3; //? priorita ... 
				seRow.QTYPACK = 0; // \TODO : rozpad na varianty baleni dle car kodu ... 
                seRow.QTYPAL = 0; //? palety neresime ... ???
                seRow.QTYSHPPD = decimal.Parse(invoiceItem.Element(inv + "quantity").Value.Trim(), System.Globalization.NumberFormatInfo.InvariantInfo); // mnozstvi k vydeji => <inv:quantity>
                seRow.SKL_ID = stockItem.Element(typ + "store").Element(typ + "id").Value.Trim(); //? id skladu => <inv:stockItem><typ:store><typ:id> : jde o identifikator skladu
                seRow.SOPNUMBE = objednavka.ID;
                seRow.TYPEPAL = ""; //? neresime palety ...
                seRow.USERID = 0;
                seRow.VNDDOCNM = ""; //? co by tady melo byt >>>
                seRow.VNDITNUM = typEAN == null ? string.Empty : typEAN.Value; //carovy kod zbozi ... => dotahnout z xml 
                if (skz_row != null && !skz_row.IsMJNull())
                {
                    seRow.MJ = skz_row.MJ;
                    if (seRow.MJ.Length > MJ_MaxLength)
                    {
						seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
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
					// \TODO: pridat mernou jednotku
                    if (skz_row != null && !skz_row.IsMJ2Null() && !skz_row.IsMJ2KoefNull())
                    {
                        // kopie zaznamu
                        Datasets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                        seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                        seRow = seRow2;

                        seRow.QTYPACK = (decimal)skz_row.MJ2Koef;
                        seRow.MJ = skz_row.MJ2;
                        if (seRow.MJ.Length > MJ_MaxLength)
                        {
							seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
                        }

                        seTable.AddCZMST_SERow(seRow);
                    }

                    // MJ3 a qtypack (pokud existuje)
                    if (skz_row != null && !skz_row.IsMJ3Null() && !skz_row.IsMJ3KoefNull())
                    {
                        // kopie zaznamu
                        Datasets.Vydej.CZMST_SERow seRow3 = seTable.NewCZMST_SERow();
                        seRow3.ItemArray = seRow.ItemArray.Clone() as object[];
                        seRow = seRow3;

                        seRow.MJ = skz_row.MJ3;
                        seRow.QTYPACK = (decimal)skz_row.MJ3Koef;
						// \TODO: osetrit delku
                        if (seRow.MJ.Length > MJ_MaxLength)
                        {
							seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
                        }

                        seTable.AddCZMST_SERow(seRow);
                    }
                }

                seRow = null;
            }

            SqlTransaction trans = null;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                conn.Open();
                trans = conn.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // ulozit do se
                //CZMST_SETableAdapter.Connection.Open();
                //CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                // prideleni cisla davky v transakci ..
                cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries(conn, trans);
                cisloDavky += 1;
                foreach (var item in seTable)
                {
                    item.CountEntries = cisloDavky;
                    // porad je pouze pridana, nikoli zmenena po zmene countentries...
                    item.AcceptChanges();
                    item.SetAdded();
                }
                int updatedRows = Database.Vydej.Update_CZMST_SE(seTable, conn, trans);
                //CZMST_SETableAdapter.Transaction.Commit();
                trans.Commit();
                objednavka.CisloDavky = cisloDavky.ToString();
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                try
                {
                    //CZMST_SETableAdapter.Transaction.Rollback();
                    trans.Rollback();
                }
                catch { }
                throw new Exception("Uložení načtených položek se nezdařilo!");
            }
            finally
            {
                //if ((CZMST_SETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                //    CZMST_SETableAdapter.Connection.Close();

                if ((conn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return "OK";

        }

        #endregion

        #region Prevodka
        /// <summary>
        /// Export dokladu prevodky
        /// </summary>
        /// <param name="file">nazev souboru kam se request ulozi</param>
        /// <param name="objednavka">id dokladu</param>
        /// <returns>True : ok; False(exception) : nepovedlo se sestaveni nebo ulozeni(False: bez vyjimky, duvod neznamy; Exception: vyjimka)</returns>
        public static bool CreateRequest_Prevodka_XML(string filename, Fask.Server.Interfaces.Classes.Objednavka objednavka)
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
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
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
				Fask.Logging.ExceptionHandler2.Handle(ex);
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
            //    Log.writeErrorLog(ex.ToString());
            //    throw ex;
            //    //return false;
            //}
        }

        /// <summary>
        /// Nacte data z odpovedi prevodky
        /// </summary>
        /// <param name="filename">cesta ke xml souboru</param>
        public static string LoadResponse_Prevodka_XML(string filename, bool save, Fask.Server.Interfaces.Classes.Objednavka objednavka)
        {
            XML.MST_Pohoda.CheckExistResponseFile(filename);


			int CZ_CarKod_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["CZ_CarKod"].MaxLength;
			int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
			int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
			//Prevodka pData = new Prevodka();

            // adapter pro dotahnuti informaci o polozce z pohody
            //Datasets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzTableAdapter();
            //SKzTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

            // adapter pro dotahnuti informace o vychozi lokaci
			//Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
			//SKzParametryTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

			Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
			if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
				ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			}

            // czmst_se adapter
            //Datasets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
            //CZMST_SETableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

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

			// \TODO: prepsat hlasku
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

			// \TODO: Poresit, pustit, nepustit ...
            if (Database.Prijem.CZMSTPE_PONUMBER_EXIST(ponumber))
            {
                // toto cislo davky se uz v db vyskytuje -> smazeme jej
                bool test = Database.Prijem.CZMSTPE_UPDATE_CZDOSLO(ponumber);
                if (test)
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "prijem - proveden update cz_doslo na 201 u prevodky=" + ponumber);
                else
                {
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "prijem - nebyl proveden update cz_doslo na 201 u prevodky=" + ponumber);
                }
            }

            // nacteni cisla davky a zvetseni o 1
            //int countEntries = Database.Vydej.CZMSTSE_MAX_CountEntries();
            //countEntries += 1;
            int countEntries = 0;

            Datasets.Vydej.CZMST_SEDataTable seTable = new Datasets.Vydej.CZMST_SEDataTable();
            Datasets.Vydej.CZMST_SERow seRow = null;

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

                //Datasets.DatabasePohoda.SKzDataTable SKzrows = SKzTableAdapter.GetDataByID(Convert.ToInt32(seRow.ITEMNMBR));
				Datasets.DatabasePohoda.SKzDataTable SKzrows = Database.Pohoda.SKz_GetDataByID(Convert.ToInt32(seRow.ITEMNMBR));
               
				// polozka nalezena, doplnit data ...
                if (SKzrows.Count > 0)
                {
                    // qtyshppd
                    seRow.QTYSHPPD = Convert.ToDecimal(prevodkaItem.Element(pre + "quantity").Value, System.Globalization.NumberFormatInfo.InvariantInfo);

                    // nacteni prvniho zaznamu
                    Datasets.DatabasePohoda.SKzRow SKzrow = SKzrows.First();
                    // dotazeni vychozi lokace pro zasobu
                    string locncodeDeafult = string.Empty;
                    if (SKzrow != null)
                    {
                        if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                        {
                            //Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, SKzrow.ID);
							Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, SKzrow.ID);
                            
							Datasets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
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
                    
                    //TaD 19.9.2018 Uprava prace s SerNumTrack
                    //seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack(SKzrow.IsRelSKzVCNull() ? (int?)null : (int?)SKzrow.RelSKzVC);
					//if (Properties.Settings.Default.pohodaXfaskPouzitVPrFXTS)
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
					//    seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((SKzrow == null) || (SKzrow.IsRelSKzVCNull()) ? (int?)null : (int?)SKzrow.RelSKzVC,true);
					//}

							if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
							{
								seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(SKzrow.IsRelSKzVCNull() ? (int?)null : (int?)SKzrow.RelSKzVC, SKzrow, Classes.Pohoda.TypAgendy.Vydej);
							}
							else
							{
								Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
								var dt_param = ParamTA.GetDataByITEMNMBR(seRow.ITEMNMBR.Trim());

								if ((dt_param != null) && (dt_param.Count > 0))
								{
									dt_row_param = dt_param.First();
								}

								seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(SKzrow.IsRelSKzVCNull() ? (int?)null : (int?)SKzrow.RelSKzVC, dt_row_param, Classes.Pohoda.TypAgendy.Vydej);
							}



                    seRow.VNDITNUM = (string)prevodkaItem.
                    Element(pre + "stockItem").
                        Element(typ + "stockItem").
                            Element(typ + "EAN") ?? string.Empty;

                    // cz_carkod
                    seRow.CZ_CarKod = SKzrow.IsIDSNull() ? string.Empty : SKzrow.IDS;
                    if (seRow.CZ_CarKod.Length > CZ_CarKod_MaxLength)
                    {
						seRow.CZ_CarKod = seRow.CZ_CarKod.Remove(CZ_CarKod_MaxLength);
                    }

                    // itemdesc
                    seRow.ITEMDESC = SKzrow.Nazev;
                    if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
                    {
						seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
                    }

                    // skl_id
                    seRow.SKL_ID = (string)prevodkaItem.
                    Element(pre + "stockItem").
                        Element(typ + "store").
                            Element(typ + "id") ?? string.Empty;

                    seRow.MJ = SKzrow.IsMJNull() ? string.Empty : SKzrow.MJ;
                    if (seRow.MJ.Length > MJ_MaxLength)
                    {
						seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
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
                            Datasets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                            seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                            seRow = seRow2;

                            seRow.QTYPACK = (decimal)SKzrow.MJ2Koef;
                            seRow.MJ = SKzrow.MJ2;
                            if (seRow.MJ.Length > MJ_MaxLength)
                            {
								seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
                            }

                            seTable.AddCZMST_SERow(seRow);
                        }

                        // MJ2 a qtypack (pokud existuje)
                        if (!SKzrow.IsMJ3Null() && !SKzrow.IsMJ3KoefNull())
                        {
                            // kopie zaznamu
                            Datasets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                            seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                            seRow = seRow2;

                            seRow.QTYPACK = (decimal)SKzrow.MJ3Koef;
                            seRow.MJ = SKzrow.MJ3;
                            if (seRow.MJ.Length > MJ_MaxLength)
                            {
								seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
                            }

                            seTable.AddCZMST_SERow(seRow);
                        }
                    }
                }
                else
                    throw new Exception("LoadResponse_Prevodka_XML (Prevodka), Zaznam s ID '" + seRow.ITEMNMBR + "' nebyl nalezen v seznamu skladovych karet.");

                seRow = null;
            }


            SqlTransaction trans = null;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                conn.Open();
                trans = conn.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // ulozeni do czmst_se
                //CZMST_SETableAdapter.Connection.Open();
                //CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                // prideleni cisla davky v transakci ..
                countEntries = Database.Vydej.CZMSTSE_MAX_CountEntries(conn, trans);
                countEntries += 1;
                foreach (var item in seTable)
                {
                    item.CountEntries = countEntries;

                    // porad je pouze pridana, nikoli zmenena po zmene countentries...
                    item.AcceptChanges();
                    item.SetAdded();
                }
                //int updatedRows = CZMST_SETableAdapter.Update(seTable);
                //CZMST_SETableAdapter.Transaction.Commit();
                int updatedRows = Database.Vydej.Update_CZMST_SE(seTable, conn, trans);
                trans.Commit();

                objednavka.CisloDavky = countEntries.ToString();
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                try
                {
                    //CZMST_SETableAdapter.Transaction.Rollback();
                    trans.Rollback();

                }
                catch { }
                throw new Exception("Uložení načtených položek se nezdařilo!");
            }
            finally
            {
                //if ((CZMST_SETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                //    CZMST_SETableAdapter.Connection.Close();

                if ((conn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    conn.Close();
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
        public static bool CreateRequest_Vydejka_XML(string filename, Fask.Server.Interfaces.Classes.Objednavka objednavka)
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
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
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
        public static string LoadResponse_Vydejka_XML(string filename, Fask.Server.Interfaces.Classes.Objednavka objednavka)
        {

            XML.MST_Pohoda.CheckExistResponseFile(filename);

            //Datasety pro komunikaci a zjistovani dat ...
            //Datasets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzTableAdapter();
            //SKzTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

            // dotazeni parametru pro locncode (vychozi lokaci ..)
            //Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
            //SKzParametryTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

            //Datasets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
            //CZMST_SETableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

			Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
			if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
				ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			}
   
            Datasets.Vydej.CZMST_SEDataTable seTable = new Datasets.Vydej.CZMST_SEDataTable();
            Datasets.Vydej.CZMST_SERow seRow = null;

            // HAVETO : Vice uzivatelsky pristup pri generovani dalsiho cisla davky je problematicky timto zpusobem...
            // Melo by byt reseno nejakym jinym mechanismem !!!
            //int cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries() + 1;
            int cisloDavky = 0;

            //Nacteni odpovedi ... 
            // - pokud neexistuje, tak se vyvola vyjimka ... ??? => mozna radeji nejdrive otestovat?



			int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
			int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;

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
				// \TODO : docilit nejake chyby ...??? => jak je chyba indikovana??? <rdc:details>???
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

            //2) polozky
            // vsechny polozky
            var vydejkaItems = vydejkaDetail.Elements(vyd + "vydejkaItem");
            // pro kazdou polozku provest ulozeni do mst tabulky ...
            // Nejlepe v transakci ...
            foreach (var vydejkaItem in vydejkaItems)
            {
                // Pokud to neni skladova polozka, tak ji neresit ...
				// \TODO : otestovat, zda to takto je ... => tedy test na NULL hodnotu...
                XElement stockItem = vydejkaItem.Element(vyd + "stockItem");
                if (stockItem == null)
                    continue;

                XElement typEAN = stockItem.Element(typ + "stockItem").Element(typ + "EAN");
                XElement typPLU = stockItem.Element(typ + "stockItem").Element(typ + "PLU");
                XElement typIDS = stockItem.Element(typ + "stockItem").Element(typ + "ids");
                //Datasets.DatabasePohoda.SKzDataTable skz_table = SKzTableAdapter.GetDataByID(int.Parse(stockItem.Element(typ + "stockItem").Element(typ + "id").Value));
				Datasets.DatabasePohoda.SKzDataTable skz_table = Database.Pohoda.SKz_GetDataByID(int.Parse(stockItem.Element(typ + "stockItem").Element(typ + "id").Value));
				Datasets.DatabasePohoda.SKzRow skz_row = skz_table.Count > 0 ? skz_table[0] : null;

                // dotazeni vychozi lokace pro zasobu
                string locncodeDeafult = string.Empty;
                if (skz_row != null)
                {
                    if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                    {
                        //Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, skz_row.ID);
						Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, skz_row.ID);
						Datasets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                        if (skzParametry_row != null)
                        {
                            locncodeDeafult = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                        }
                    }
                }

                seRow = seTable.NewCZMST_SERow();

                seRow.ITEMDESC = vydejkaItem.Element(vyd + "text").Value.Trim(); // nazev polozky => z xml <inv:text>
                if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
                {
					seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
                }

                seRow.ITEMNMBR = stockItem.Element(typ + "stockItem").Element(typ + "id").Value.Trim(); // cislo polozky => z xml <inv:stockItem><typ:stockItem><typ:id>
                //22.7.2022 MaR
                seRow.ITEMTYPE = "J"; //bez typu
                //seRow.ITEMCODE = ""; // ? ITEMCODE neni???
				seRow.CountEntries = cisloDavky; // \TODO : ??? nove cislo davky ... 
                //seRow.CZ_CarKod = typPLU == null ? string.Empty : typPLU.Value; //carovy kod => dotahnout z xml PLU
                seRow.CZ_CarKod = typIDS == null ? string.Empty : typIDS.Value.Trim();
                seRow.CZ_DatVyr_Delka = 0; //?
                seRow.CZ_DatVyr_Track = 0; //?
                seRow.CZ_Doslo = 0; // pripraveno pro zpracovani
                seRow.CZ_SerNum_Delka = 0; //? delka serioveho cisla 
                //seRow.CZ_SerNum_Track = 0; //? sledovat na SN/Sarze => je sledovano na seriove cislo => zjistit v Pohoda
                //seRow.CZ_SerNum_Track = (byte)(skz_row == null ? 0 : skz_row.RelSKzVC);
				//if (Properties.Settings.Default.pohodaXfaskPouzitVPrFXTS)
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
				//    seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((skz_row == null) || (skz_row.IsRelSKzVCNull()) ? (int?)null : (int?)skz_row.RelSKzVC,true);
				//}

				if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, skz_row, Classes.Pohoda.TypAgendy.Vydej);
				}
				else
				{
					Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
					var dt_param = ParamTA.GetDataByITEMNMBR(seRow.ITEMNMBR.Trim());

					if ((dt_param != null) && (dt_param.Count > 0))
					{
						dt_row_param = dt_param.First();
					}

					seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, dt_row_param, Classes.Pohoda.TypAgendy.Vydej);
				}
                
                seRow.CZ_SW_Delka = 0; //?
                seRow.CZ_SW_Track = 0; //?
                //seRow.DEX_ROW_ID
                seRow.LOCNCODE = locncodeDeafult;
                seRow.Note = ""; //? poznamka 
                seRow.ORD = int.Parse(vydejkaItem.Element(vyd + "id").Value.Trim()); //? poradi polozky na dokladu - pro pohoda id zaznamu v tabulce polozek <inv:id>
                seRow.PRINTED = 0;
                seRow.PRIORITY = 3; //? priorita ... 
				seRow.QTYPACK = 0; // \TODO : rozpad na varianty baleni dle car kodu ... 
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
                    if (seRow.MJ.Length > MJ_MaxLength)
                    {
						seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
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
					// \TODO: pridat mernou jednotku
                    if (skz_row != null && !skz_row.IsMJ2Null() && !skz_row.IsMJ2KoefNull())
                    {
                        // kopie zaznamu
                        Datasets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                        seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                        seRow = seRow2;

                        seRow.QTYPACK = (decimal)skz_row.MJ2Koef;
                        seRow.MJ = skz_row.MJ2;
                        if (seRow.MJ.Length > MJ_MaxLength)
                        {
							seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
                        }

                        seTable.AddCZMST_SERow(seRow);
                    }

                    // MJ3 a qtypack (pokud existuje)
                    if (skz_row != null && !skz_row.IsMJ3Null() && !skz_row.IsMJ3KoefNull())
                    {
                        // kopie zaznamu
                        Datasets.Vydej.CZMST_SERow seRow3 = seTable.NewCZMST_SERow();
                        seRow3.ItemArray = seRow.ItemArray.Clone() as object[];
                        seRow = seRow3;

                        seRow.MJ = skz_row.MJ3;
                        seRow.QTYPACK = (decimal)skz_row.MJ3Koef;
						// \TODO: osetrit delku
                        if (seRow.MJ.Length > MJ_MaxLength)
                        {
							seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
                        }

                        seTable.AddCZMST_SERow(seRow);
                    }
                }

                seRow = null;
            }

            SqlTransaction trans = null;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                conn.Open();
                trans = conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                // ulozit do se
                //CZMST_SETableAdapter.Connection.Open();
                //CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                // prideleni cisla davky v transakci ..
                //cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries(CZMST_SETableAdapter.Connection, CZMST_SETableAdapter.Transaction);
                cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries(conn, trans);
                cisloDavky += 1;
                foreach (var item in seTable)
                {
                    item.CountEntries = cisloDavky;

                    // porad je pouze pridana, nikoli zmenena po zmene countentries...
                    item.AcceptChanges();
                    item.SetAdded();
                }
                //int updatedRows = CZMST_SETableAdapter.Update(seTable);
                //CZMST_SETableAdapter.Transaction.Commit();
                int updatedRows = Database.Vydej.Update_CZMST_SE(seTable, conn, trans);
                trans.Commit();
                objednavka.CisloDavky = cisloDavky.ToString();
            }
            catch (Exception e)
            {
				Fask.Logging.ExceptionHandler2.Handle(e);
                try
                {
                    //CZMST_SETableAdapter.Transaction.Rollback();
                    trans.Rollback();
                }
                catch { }
                throw new Exception("Uložení načtených položek se nezdařilo!");
            }
            finally
            {
                //if ((CZMST_SETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                //    CZMST_SETableAdapter.Connection.Close();

                if ((conn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    conn.Close();
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
        public static bool CreateRequest_Prodejka_XML(string filename, Fask.Server.Interfaces.Classes.Objednavka objednavka)
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
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
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
        public static string LoadResponse_Prodejka_XML(string filename, Fask.Server.Interfaces.Classes.Objednavka objednavka)
        {
            XML.MST_Pohoda.CheckExistResponseFile(filename);

            //Datasety pro komunikaci a zjistovani dat ...
            //Datasets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzTableAdapter();
            //SKzTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

            // dotazeni parametru pro locncode (vychozi lokaci ..)
			//Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
			//SKzParametryTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

            //Datasets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
            //CZMST_SETableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

			Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
			if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
				ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			}
     
            Datasets.Vydej.CZMST_SEDataTable seTable = new Datasets.Vydej.CZMST_SEDataTable();
            Datasets.Vydej.CZMST_SERow seRow = null;

            // HAVETO : Vice uzivatelsky pristup pri generovani dalsiho cisla davky je problematicky timto zpusobem...
            // Melo by byt reseno nejakym jinym mechanismem !!!
            //int cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries() + 1;
            int cisloDavky = 0;

            //Nacteni odpovedi ... 
            // - pokud neexistuje, tak se vyvola vyjimka ... ??? => mozna radeji nejdrive otestovat?

            if (!File.Exists(filename))
                throw new Exception("Response soubor neexistuje!");

			int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
			int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
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
				// \TODO : docilit nejake chyby ...??? => jak je chyba indikovana??? <rdc:details>???
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

            //2) polozky
			// \TODO : prodejkaitem neobsahuje prvek id (ORD) - poradi na dokladu(index?)
            int ord = 0;
            // vsechny polozky
            var prodejkaItems = prodejkaDetail.Elements(pro + "prodejkaItem");
            // pro kazdou polozku provest ulozeni do mst tabulky ...
            // Nejlepe v transakci ...
            foreach (var prodejkaItem in prodejkaItems)
            {
                // Pokud to neni skladova polozka, tak ji neresit ...
				// \TODO : otestovat, zda to takto je ... => tedy test na NULL hodnotu...
                XElement stockItem = prodejkaItem.Element(pro + "stockItem");
                if (stockItem == null)
                    continue;

                XElement typEAN = stockItem.Element(typ + "stockItem").Element(typ + "EAN");
                XElement typPLU = stockItem.Element(typ + "stockItem").Element(typ + "PLU");
                XElement typIDS = stockItem.Element(typ + "stockItem").Element(typ + "ids");
                //Datasets.DatabasePohoda.SKzDataTable skz_table = SKzTableAdapter.GetDataByID(int.Parse(stockItem.Element(typ + "stockItem").Element(typ + "id").Value));
				Datasets.DatabasePohoda.SKzDataTable skz_table = Database.Pohoda.SKz_GetDataByID(int.Parse(stockItem.Element(typ + "stockItem").Element(typ + "id").Value));
                Datasets.DatabasePohoda.SKzRow skz_row = skz_table.Count > 0 ? skz_table[0] : null;

                // dotazeni vychozi lokace pro zasobu
                string locncodeDeafult = string.Empty;
                if (skz_row != null)
                {
                    if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                    {
                        //Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, skz_row.ID);
						Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, skz_row.ID);    
						Datasets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                        if (skzParametry_row != null)
                        {
                            locncodeDeafult = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                        }
                    }
                }

                seRow = seTable.NewCZMST_SERow();

                seRow.ITEMDESC = prodejkaItem.Element(pro + "text").Value.Trim(); // nazev polozky => z xml <inv:text>
                if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
                {
					seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
                }

                seRow.ITEMNMBR = stockItem.Element(typ + "stockItem").Element(typ + "id").Value.Trim(); // cislo polozky => z xml <inv:stockItem><typ:stockItem><typ:id>
                //22.7.2025 MaR 
                seRow.ITEMTYPE = "J"; //bez typu
                //seRow.ITEMCODE = ""; // ? ITEMCODE neni???
				seRow.CountEntries = cisloDavky; // \TODO : ??? nove cislo davky ... 
                //seRow.CZ_CarKod = typPLU == null ? string.Empty : typPLU.Value; //carovy kod => dotahnout z xml PLU
                seRow.CZ_CarKod = typIDS == null ? string.Empty : typIDS.Value.Trim();
                seRow.CZ_DatVyr_Delka = 0; //?
                seRow.CZ_DatVyr_Track = 0; //?
                seRow.CZ_Doslo = 0; // pripraveno pro zpracovani
                seRow.CZ_SerNum_Delka = 0; //? delka serioveho cisla 
                //seRow.CZ_SerNum_Track = 0; //? sledovat na SN/Sarze => je sledovano na seriove cislo => zjistit v Pohoda
                //seRow.CZ_SerNum_Track = (byte)(skz_row == null ? 0 : skz_row.RelSKzVC);

				//if (Properties.Settings.Default.pohodaXfaskPouzitVPrFXTS)
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
				//    seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((skz_row == null) || (skz_row.IsRelSKzVCNull()) ? (int?)null : (int?)skz_row.RelSKzVC,true);
				//}

				if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, skz_row, Classes.Pohoda.TypAgendy.Vydej);
				}
				else
				{
					Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
					var dt_param = ParamTA.GetDataByITEMNMBR(seRow.ITEMNMBR.Trim());

					if ((dt_param != null) && (dt_param.Count > 0))
					{
						dt_row_param = dt_param.First();
					}

					seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, dt_row_param, Classes.Pohoda.TypAgendy.Vydej);
				}

                seRow.CZ_SW_Delka = 0; //?
                seRow.CZ_SW_Track = 0; //?
                //seRow.DEX_ROW_ID
                seRow.LOCNCODE = locncodeDeafult;
                seRow.Note = ""; //? poznamka 
				// \TODO : sledovat, zda se nekdy tento prvek dostavi do vystupu xml ...
                //seRow.ORD = int.Parse(prodejkaItem.Element(pro + "id").Value.Trim()); //? poradi polozky na dokladu - pro pohoda id zaznamu v tabulce polozek <inv:id>
                seRow.ORD = --ord;
                seRow.PRINTED = 0;
                seRow.PRIORITY = 3; //? priorita ... 
				seRow.QTYPACK = 0; // \TODO : rozpad na varianty baleni dle car kodu ... 
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
                    if (seRow.MJ.Length > MJ_MaxLength)
                    {
						seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
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
					// \TODO: pridat mernou jednotku
                    if (skz_row != null && !skz_row.IsMJ2Null() && !skz_row.IsMJ2KoefNull())
                    {
                        // kopie zaznamu
                        Datasets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                        seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                        seRow = seRow2;

                        seRow.QTYPACK = (decimal)skz_row.MJ2Koef;
                        seRow.MJ = skz_row.MJ2;
                        if (seRow.MJ.Length > MJ_MaxLength)
                        {
							seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
                        }

                        seTable.AddCZMST_SERow(seRow);
                    }

                    // MJ3 a qtypack (pokud existuje)
                    if (skz_row != null && !skz_row.IsMJ3Null() && !skz_row.IsMJ3KoefNull())
                    {
                        // kopie zaznamu
                        Datasets.Vydej.CZMST_SERow seRow3 = seTable.NewCZMST_SERow();
                        seRow3.ItemArray = seRow.ItemArray.Clone() as object[];
                        seRow = seRow3;

                        seRow.MJ = skz_row.MJ3;
                        seRow.QTYPACK = (decimal)skz_row.MJ3Koef;
						// \TODO: osetrit delku
						if (seRow.MJ.Length > MJ_MaxLength)
                        {
							seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
                        }

                        seTable.AddCZMST_SERow(seRow);
                    }
                }

                seRow = null;
            }

            SqlTransaction trans = null;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                conn.Open();
                trans = conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                // ulozit do se
                //CZMST_SETableAdapter.Connection.Open();
                //CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                // prideleni cisla davky v transakci ..
                //cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries(CZMST_SETableAdapter.Connection, CZMST_SETableAdapter.Transaction);
                cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries(conn, trans);
                cisloDavky += 1;
                foreach (var item in seTable)
                {
                    item.CountEntries = cisloDavky;

                    // porad je pouze pridana, nikoli zmenena po zmene countentries...
                    item.AcceptChanges();
                    item.SetAdded();
                }

                //int updatedRows = CZMST_SETableAdapter.Update(seTable);
                //CZMST_SETableAdapter.Transaction.Commit();
                int updatedRows = Database.Vydej.Update_CZMST_SE(seTable,conn, trans);
                trans.Commit();
                objednavka.CisloDavky = cisloDavky.ToString();
            }
            catch (Exception e)
            {
				Fask.Logging.ExceptionHandler2.Handle(e);
                try
                {
                    //CZMST_SETableAdapter.Transaction.Rollback();
                    trans.Rollback();
                }
                catch { }
                throw new Exception("Uložení načtených položek se nezdařilo!");
            }
            finally
            {
                //if ((CZMST_SETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                //    CZMST_SETableAdapter.Connection.Close();

                if ((conn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return "OK";

        }

        #endregion


        /// <summary>
        /// Vytvori xml soubor pro export prijatych objedanavek podle zadanych parametru
        /// </summary>
        /// <param name="filename">nazev souboru (cesta se doplni)</param>
        /// <param name="lastChanges">Program POHODA exportuje všechny záznamy, které mají datum "uloženo" novější(menší) než datum parametru lastchange (format: 2011-04-29T14:30:00)</param>
        /// <param name="dateFrom">filtr dle datumu vystavení dokladu (format: 2011-01-10)</param>
        /// <param name="dateTill">filtr dle datumu vystavení dokladu (format: 2011-01-10)</param>
        /// <param name="ico">IČ firmy, pro kterou je XML určeno. Hodnota musí souhlasit s IČ zadané firmy.</param>
        /// <param name="note">Textová poznámka, hodnota se zobrazí v záložce "Poznámky" v agendě XML Import.</param>
        /// <returns>TRUE - OK, FALSE - CHYBA</returns>
        public static bool CreatePrijataObjednavkaXML(string filename, string lastChanges, string dateFrom, string dateTill, string note, List<string> companys, string userFilterName, List<string> icos, List<string> cislaDokladu)
        {
            try
            { 
                List<object> filterList = new List<object>();
                List<object> mainfilterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace lst = "http://www.stormware.cz/schema/version_2/list.xsd";
                XNamespace ftr = "http://www.stormware.cz/schema/version_2/filter.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                if (lastChanges != string.Empty)
                    filterList.Add(new XElement(ftr + "lastChanges", lastChanges));

                if (dateFrom != string.Empty && dateTill != string.Empty)
                {
                    filterList.Add(new XElement(ftr + "dateFrom", dateFrom));
                    filterList.Add(new XElement(ftr + "dateTill", dateTill));
                }

                List<object> filterCompanyList = new List<object>();
                if (companys.Count > 0)
                {
                    for (int i = 0; i < companys.Count; i++)
                    {
                        filterCompanyList.Add(new XElement(ftr + "company", companys[i]));
                    }

                    filterList.Add(new XElement(ftr + "selectedCompanys", filterCompanyList.ToArray()));
                }

                List<object> filterIcoList = new List<object>();
                if (icos.Count > 0)
                {
                    for (int i = 0; i < icos.Count; i++)
                    {
                        filterIcoList.Add(new XElement(ftr + "ico", icos[i]));
                    }

                    filterList.Add(new XElement(ftr + "selectedIco", filterIcoList.ToArray()));
                }

                List<object> filterCisloDokladuList = new List<object>();
                if (cislaDokladu.Count > 0)
                {
                    for (int i = 0; i < cislaDokladu.Count; i++)
                    {
                        filterCisloDokladuList.Add(new XElement(ftr + "number",
                           (new XElement(typ + "numberRequested", cislaDokladu[i]))));
                    }

                    filterList.Add(new XElement(ftr + "selectedNumbers", filterCisloDokladuList.ToArray()));
                }


                object[] filter = null;
                object mainFilter = null;

                filter = filterList.ToArray();

                if (userFilterName != string.Empty)
                {
                    mainFilter = new XElement(ftr + "userFilterName", userFilterName);
                }
                else
                    mainFilter = new XElement(ftr + "filter", filter);

                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "ftr", "http://www.stormware.cz/schema/version_2/filter.xsd"),
                    new XAttribute(XNamespace.Xmlns + "lst", "http://www.stormware.cz/schema/version_2/list.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "001"),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", note),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "li1"),
                            new XAttribute("version", "2.0"),

                                new XElement(lst + "listOrderRequest",
                                    new XAttribute("version", "2.0"),
                                    new XAttribute("orderType", "receivedOrder"),
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

        [Obsolete("Old neobjektove")]
        public static bool CreateImportXML(string file, int countEntries, string note, Datasets.Vydej.CZMST_SIDataTable dt_si)
        {

			// \TODO TaD 15.8.2018 Predelat na objektove skladani xml...

            #region Nepouživá se
            ////Datasets.Vydej.CZMST_SIDataTable dt_si = null;

            //try
            //{
            //    //dt_si = Database.Vydej.GETDATA_CZMSTSI(countEntries);

            //    string filename = Globals.PathToInputDirectory + file;
            //    string partnerID = string.Empty;

            //    foreach (Datasets.Vydej.CZMST_SIRow row in dt_si)
            //    {
            //        if (!row.IsODBER_IDNull() && row.ODBER_ID.Trim() != string.Empty)
            //        {
            //            partnerID = row.ODBER_ID.Trim();
            //            break;
            //        }
            //    }

            //    DateTime date = DateTime.Now;

            //    TextWriter tw = new StreamWriter(filename);
            //    tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            //    tw.WriteLine("<dat:dataPack id=\"vyd" + countEntries + "\" ico=\"" + Globals.ICO + "\" application=\"MST_Pohoda\" version = \"2.0\" note=\"Import Vydejky\"");
            //    tw.WriteLine("xmlns:dat=\"http://www.stormware.cz/schema/version_2/data.xsd\"");
            //    tw.WriteLine("xmlns:vyd=\"http://www.stormware.cz/schema/version_2/vydejka.xsd\"");
            //    tw.WriteLine("xmlns:typ=\"http://www.stormware.cz/schema/version_2/type.xsd\">");

            //    tw.WriteLine("<dat:dataPackItem id=\"VYD" + countEntries + "\" version=\"2.0\">");
            //    tw.WriteLine("  <vyd:vydejka version=\"2.0\">");

            //    // hlavicka
            //    tw.WriteLine("      <vyd:vydejkaHeader>");
            //    //tw.WriteLine("          <vyd:date>" + date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString("00") + "</vyd:date>");
            //    tw.WriteLine("          <vyd:date>" + XmlConvert.ToString(date, "yyyy-MM-dd") + "</vyd:date>");




            //    tw.WriteLine("          <vyd:numberOrder></vyd:numberOrder>");
            //    //tw.WriteLine("          <vyd:dateOrder>" + "1999-01-01" + "</vyd:dateOrder>");
            //    tw.WriteLine("          <vyd:text>" + "Test import MSTW-Pohoda" + "</vyd:text>");

            //    //partner
            //    if (partnerID != string.Empty)
            //    {
            //        tw.WriteLine("          <vyd:partnerIdentity>");
            //        #region old
            //        /*
            //        tw.WriteLine("              <typ:address>");
            //        tw.WriteLine("                  <typ:company>ZET s.r.o.</typ:company>");
            //        tw.WriteLine("                  <typ:division>Obchodni oddeleni</typ:division>");
            //        tw.WriteLine("                  <typ:name>Ondrej Marsik</typ:name>");
            //        tw.WriteLine("                  <typ:city>Praha 3</typ:city>");
            //        tw.WriteLine("                  <typ:street>Zahradni</typ:street>");
            //        tw.WriteLine("                  <typ:zip>56801</typ:zip>");
            //        tw.WriteLine("                  <typ:ico>1111</typ:ico>");
            //        tw.WriteLine("                  <typ:dic>330-11111</typ:dic>");
            //        tw.WriteLine("              </typ:address>");
            //        tw.WriteLine("              <typ:shipToAddress>");
            //        tw.WriteLine("                  <typ:name>Soukup Bohumil</typ:name>");
            //        tw.WriteLine("                  <typ:city>Trebic</typ:city>");
            //        tw.WriteLine("                  <typ:street>Nova</typ:street>");
            //        tw.WriteLine("              </typ:shipToAddress>");
            //        */
            //        #endregion
            //        tw.WriteLine("<typ:id>" + partnerID + "</typ:id>");
            //        tw.WriteLine("          </vyd:partnerIdentity>");
            //    }
            //    #region old
            //    /*
            //    tw.WriteLine("          <vyd:priceLevel>");
            //    tw.WriteLine("              <typ:id>" + "2" + "</typ:id>");
            //    tw.WriteLine("              <typ:ids>" + "Sleva 4" + "</typ:ids>");
            //    tw.WriteLine("          </vyd:priceLevel>");

            //    tw.WriteLine("          <vyd:centre>");
            //    tw.WriteLine("              <typ:id>" + "1" + "</typ:id>");
            //    tw.WriteLine("              <typ:ids>" + "Trebic" + "</typ:ids>");
            //    tw.WriteLine("          </vyd:centre>");

            //    tw.WriteLine("          <vyd:activity>");
            //    tw.WriteLine("              <typ:id>" + "1" + "</typ:id>");
            //    tw.WriteLine("          </vyd:activity>");

            //    tw.WriteLine("          <vyd:contract>");
            //    tw.WriteLine("              <typ:id>" + "1" + "</typ:id>");
            //    tw.WriteLine("          </vyd:contract>");
            //    */
            //    #endregion
            //    tw.WriteLine("          <vyd:note>" + "nacteno z xml (czmst_si)" + "</vyd:note>");
            //    tw.WriteLine("          <vyd:intNote>" + "bla bla" + "</vyd:intNote>");
            //    tw.WriteLine("      </vyd:vydejkaHeader>");

            //    //polozky 
            //    tw.WriteLine("      <vyd:vydejkaDetail>");

            //    foreach (Datasets.Vydej.CZMST_SIRow row in dt_si)
            //    {
            //        tw.WriteLine("      <vyd:vydejkaItem>");
            //        tw.WriteLine("          <vyd:quantity>" + ((float)row.QTYSHPPD).ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "</vyd:quantity>");
            //        tw.WriteLine("          <vyd:coefficient>1</vyd:coefficient>");
            //        tw.WriteLine("          <vyd:note>" + row.SERLTNUM.Trim() + "</vyd:note>");
            //        tw.WriteLine("          <vyd:discountPercentage>0</vyd:discountPercentage>");
            //        tw.WriteLine("          <vyd:stockItem>");
            //        tw.WriteLine("              <typ:stockItem>");
            //        tw.WriteLine("                  <typ:id>" + row.ITEMNMBR.Trim() + "</typ:id>");
            //        tw.WriteLine("              </typ:stockItem>");
            //        tw.WriteLine("          </vyd:stockItem>");
            //        tw.WriteLine("      </vyd:vydejkaItem>");
            //    }

            //    #region old
            //    /*
            //    tw.WriteLine("      <vyd:vydejkaItem>");
            //    tw.WriteLine("          <vyd:quantity>1</vyd:quantity>");
            //    tw.WriteLine("          <vyd:unit>ks</vyd:unit>");
            //    tw.WriteLine("          <vyd:coefficient>1</vyd:coefficient>");
            //    tw.WriteLine("          <vyd:rateVAT>high</vyd:rateVAT>");
            //    tw.WriteLine("          <vyd:discountPercentage>0</vyd:discountPercentage>");
            //    tw.WriteLine("          <vyd:homeCurrency>");
            //    tw.WriteLine("              <typ:unitPrice>2000</typ:unitPrice>");
            //    tw.WriteLine("          </vyd:homeCurrency>");
            //    tw.WriteLine("          <vyd:stockItem>");
            //    tw.WriteLine("              <typ:stockItem>");
            //    tw.WriteLine("                  <typ:ids>Z120</typ:ids>");
            //    tw.WriteLine("              </typ:stockItem>");
            //    tw.WriteLine("          </vyd:stockItem>");
            //    tw.WriteLine("          <vyd:centre>");
            //    tw.WriteLine("              <typ:id>1</typ:id>");
            //    tw.WriteLine("          </vyd:centre>");
            //    tw.WriteLine("          <vyd:activity>");
            //    tw.WriteLine("              <typ:id>1</typ:id>");
            //    tw.WriteLine("          </vyd:activity>");
            //    tw.WriteLine("          <vyd:contract>");
            //    tw.WriteLine("              <typ:id>1</typ:id>");
            //    tw.WriteLine("          </vyd:contract>");
            //    tw.WriteLine("      </vyd:vydejkaItem>");
            //    */
            //    //textova polozka
            //    /*
            //    tw.WriteLine("      <vyd:vydejkaItem>");
            //    tw.WriteLine("          <vyd:text>Balne</vyd:text>");
            //    tw.WriteLine("          <vyd:quantity>1</vyd:quantity>");
            //    tw.WriteLine("          <vyd:unit>ks</vyd:unit>");
            //    tw.WriteLine("          <vyd:coefficient>1</vyd:coefficient>");
            //    tw.WriteLine("          <vyd:rateVAT>high</vyd:rateVAT>");
            //    tw.WriteLine("          <vyd:discountPercentage>0</vyd:discountPercentage>");
            //    tw.WriteLine("          <vyd:homeCurrency>");
            //    tw.WriteLine("              <typ:unitPrice>150</typ:unitPrice>");
            //    tw.WriteLine("          </vyd:homeCurrency>");
            //    tw.WriteLine("      </vyd:vydejkaItem>");
            //    */
            //    #endregion
            //    tw.WriteLine("      </vyd:vydejkaDetail>");
            //    tw.WriteLine("  </vyd:vydejka>");
            //    tw.WriteLine("</dat:dataPackItem>");
            //    tw.WriteLine("</dat:dataPack>");

            //    tw.Flush();
            //    tw.Close();
            //    return true;

            //}
            //catch (Exception ex)
            //{
            //    Log.writeErrorLog(ex.ToString());
            //    return false;
            //} 
            #endregion

            return true;
        }

        private static void NastavPromenne(Datasets.Vydej.CZMST_SERow seRow)
        {
            seRow.VNDITNUM = string.Empty;
            seRow.ORD = 0;
            seRow.CZ_DatVyr_Delka = 0;
            seRow.CZ_DatVyr_Track = 0;
            seRow.CZ_SerNum_Delka = 0;
            seRow.CZ_SerNum_Track = 0;
            seRow.CZ_SW_Delka = 0;
            seRow.CZ_SW_Track = 0;
            seRow.CZ_Doslo = 0;
            seRow.DEX_ROW_ID = 0;
            seRow.Note = string.Empty;
            seRow.ITEMNMBR = string.Empty;
            seRow.LOCNCODE = string.Empty;
            seRow.ITEMDESC = string.Empty;
            seRow.QTYSHPPD = 0;
            seRow.QTYPACK = 0;
            //22.7.2022 MaR
            seRow.ITEMTYPE = "J";
            seRow.VNDDOCNM = string.Empty;
            seRow.CZ_CarKod = string.Empty;
            seRow.TYPEPAL = string.Empty;
            seRow.QTYPAL = 0;
            seRow.PRIORITY = 3;
            seRow.PRINTED = 0;
            seRow.USERID = 0;

            seRow.CZ_REZ1_Track = 0;
            seRow.CZ_REZ2_Track = 0;

            seRow.ITEMCODE = string.Empty;
            seRow.SetWEIGHTNull();

            seRow.CZ_Expirace_Track = 0;

        }


        /// <summary>
        /// Nacte data z prijatych objednavek 
        /// </summary>
        /// <param name="filename">cesta ke xml souboru</param>
        public static string LoadPrijataObjednavkaXML(string filename, bool save)
        {
            XML.MST_Pohoda.CheckExistResponseFile(filename);

            string actualStrName = string.Empty;
            string previousStrName = string.Empty;
            int actualCountEntries = 0;
            bool InVydejkItem = false;
            decimal quantity = 0;
            decimal delivered = 0;
            int indexPriID = 0;
            int indexTypID = 0;
            int indexTypIDS = 0;

            bool isExecuted = false;
            bool isDelivered = false;
            bool incCountEntr = true;
            bool check = false;
            string actualSonnumber = string.Empty;

			int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
			int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
			int SOPNUMBE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["SOPNUMBE"].MaxLength;
			int SKL_ID_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["SKL_ID"].MaxLength;
			int VNDITNUM_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["VNDITNUM"].MaxLength;
			int CZ_CarKod_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["CZ_CarKod"].MaxLength;

			// dotazeni informace o polozce z pohoda ...
            //Datasets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzTableAdapter();
            //SKzTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

            // dotazeni parametru pro locncode (vychozi lokaci ..)
			//Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
			//SKzParametryTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

            //Datasets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
            //CZMST_SETableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

			Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
			if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
				ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			}

            Datasets.Vydej.CZMST_SEDataTable seTable = new Datasets.Vydej.CZMST_SEDataTable();
            Datasets.Vydej.CZMST_SERow seRow = null;
        

            XmlTextReader reader = new XmlTextReader(filename);


            SqlTransaction trans = null;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                conn.Open();
                trans = conn.BeginTransaction();


                seRow = seTable.NewCZMST_SERow();

                NastavPromenne(seRow);

                actualCountEntries = Database.Vydej.CZMSTSE_MAX_CountEntries(conn, trans);


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
                                { //jsme u dalsiho zbozi na prijemce...ulozime aktulani zbozi 

                                    seRow.CountEntries = actualCountEntries;
                                    seRow.SOPNUMBE = actualSonnumber;

                                    if (Database.Vydej.CZMSTSE_EXIST_SOPNUMBE(seRow.SOPNUMBE) && !check)
                                    { // toto cislo davky se uz v db vyskytuje -> nastavime cz_doslo na 201
                                        check = true;
                                        bool test = Database.Vydej.CZMSTSE_UPDATE_CZDOSLO(seRow.SOPNUMBE);
                                        if (test)
											Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "vydej - proveden update cz_doslo na 201 u objednavky=" + seRow.SOPNUMBE);
                                        else
											Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "vydej - nebyl proveden update cz_doslo na 201 u objednavky=" + seRow.SOPNUMBE);
                                    }
                                    else
                                        check = true;


                                    //TODO: revidovat, přepsat
                                    //if (!Database.Vydej.CZMSTSE_EXIST_SOPNUMBE(peRow.SOPNUMBE, peRow.ITEMNMBR, peRow.ORD))
                                    //{
                                    if (Globals_V1.Konfigurace.Vydej[0].StatusObjednavky)
                                    {
                                        if (save)
                                        {
                                            SaveData(ref quantity, delivered, ref incCountEntr, conn, trans , seRow);
                                        }
                                        else if ((Globals_V1.Konfigurace.Vydej[0].Executed == isExecuted) && (Globals_V1.Konfigurace.Vydej[0].Delivered == isDelivered))
                                        {
                                            SaveData(ref quantity, delivered, ref incCountEntr, conn, trans, seRow);
                                        }
                                    }
                                    else
                                    {
                                        SaveData(ref quantity, delivered, ref incCountEntr, conn, trans, seRow);
                                    }

                                    /*}
                                    else
                                    {
                                        incCountEntr = false;
                                        int pom = Database.Vydej.CZMSTSE_CountEntries_SOPNUMBE(peRow.SOPNUMBE);

                                        if (pom >= 0)
                                            peRow.CountEntries = actualCountEntries = pom; 

                                    }*/

                                    seRow = seTable.NewCZMST_SERow();

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

                                    if (seRow.SOPNUMBE.Length > SOPNUMBE_MaxLength)
                                    {
										seRow.SOPNUMBE = seRow.SOPNUMBE.Remove(SOPNUMBE_MaxLength);
                                    }


                                    if (Database.Vydej.CZMSTSE_EXIST_SOPNUMBE(seRow.SOPNUMBE) && !check)
                                    { // toto cislo davky se uz v db vyskytuje -> nastavime cz_doslo na 201
                                        check = true;
                                        bool test = Database.Vydej.CZMSTSE_UPDATE_CZDOSLO(seRow.SOPNUMBE);
                                        if (test)
											Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "vydej - proveden update cz_doslo na 201 u objednavky=" + seRow.SOPNUMBE);
                                        else
											Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "vydej - nebyl proveden update cz_doslo na 201 u objednavky=" + seRow.SOPNUMBE);
                                    }
                                    else
                                        check = true;

                                    //if (!Database.Vydej.CZMSTSE_EXIST_SOPNUMBE(peRow.SOPNUMBE, peRow.ITEMNMBR, peRow.ORD))
                                    //{//pridame pokud objednavka jeste neni v databazi
                                    if (save)
                                    {
                                        SaveData(ref quantity, delivered, ref incCountEntr, conn, trans, seRow);
                                    }
                                    else if (Globals_V1.Konfigurace.Vydej[0].StatusObjednavky)
                                    {
                                        if ((Globals_V1.Konfigurace.Vydej[0].Executed == isExecuted) && (Globals_V1.Konfigurace.Vydej[0].Delivered == isDelivered))
                                        {
                                            SaveData(ref quantity, delivered, ref incCountEntr, conn, trans, seRow);

                                        }
                                    }
                                    else
                                    {
                                        SaveData(ref quantity, delivered, ref incCountEntr, conn, trans, seRow);
                                    }
                                    /*}
                                    else
                                    {
                                        incCountEntr = false;
                                        int pom = Database.Vydej.CZMSTSE_CountEntries_SOPNUMBE(peRow.SOPNUMBE);

                                        if (pom >= 0)
                                            peRow.CountEntries = actualCountEntries = pom; 

                                    }*/

                                    seRow = seTable.NewCZMST_SERow();

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
                                    actualCountEntries++;

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
                                        if (seRow.SKL_ID.Length > SKL_ID_MaxLength)
                                        {
											seRow.SKL_ID = seRow.SKL_ID.Remove(SKL_ID_MaxLength);
                                        }
                                        
                                        //peRow.LOCNCODE = reader.Value;
                                        //if (peRow.LOCNCODE.Length > VydejDS.CZMST_SE.LOCNCODEColumn.MaxLength)
                                        //{
                                        //    peRow.LOCNCODE = peRow.LOCNCODE.Remove(VydejDS.CZMST_SE.LOCNCODEColumn.MaxLength);
                                        //}
                                    }

                                    if (indexTypID == 1)
                                    {
                                        seRow.ITEMNMBR = reader.Value;

                                        // TOTO se nesmi delat, jinak muze dojit k problemum s identifikaci...
                                        // pokud presahne delku, tak je problem !!!
                                        //if (peRow.ITEMNMBR.Length > VydejDS.CZMST_SE.ITEMNMBRColumn.MaxLength)
                                        //{
                                        //    peRow.ITEMNMBR = peRow.ITEMNMBR.Remove(VydejDS.CZMST_SE.ITEMNMBRColumn.MaxLength);
                                        //}

                                        // dotazeni vychozi lokace pro zasobu
                                        string locncodedefault = string.Empty;
                                        if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                                        {
                                            //Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, int.Parse(seRow.ITEMNMBR));
											Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, int.Parse(seRow.ITEMNMBR));
											Datasets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                                            if (skzParametry_row != null)
                                            {
                                                locncodedefault = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                                            }
                                        }
                                        seRow.LOCNCODE = locncodedefault;

                                        //SKzTableAdapter.GetDataByID(seRow.ITEMNMBR);
                                        //var skz_dt = SKzTableAdapter.GetDataByID(int.Parse(seRow.ITEMNMBR));
										var skz_dt = Database.Pohoda.SKz_GetDataByID(int.Parse(seRow.ITEMNMBR));

                                        if ((skz_dt != null) && (skz_dt.Count > 0))
                                        {

                                            var skz_row = skz_dt[0];
                                            //TaD 19.9.2018 Nova logika prebirani SERNUMTRACK

										//    if (Properties.Settings.Default.pohodaXfaskPouzitVPrFXTS)
										//    {
										//        if (skz_row.IsVPrFXTSNull())
										//        {
										//            seRow.CZ_SerNum_Track = 0;
										//        }
										//        else
										//        {
										//            if (skz_row.VPrFXTS)
										//            {
										//                seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((skz_row == null) || (skz_row.IsRefVPrFXTSNull()) ? (int?)null : ((int?)skz_row.RefVPrFXTS - 1), skz_row.VPrFXTS);
										//            }
										//            else
										//            {
										//                seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((skz_row == null) || (skz_row.IsRefVPrFVTSNull()) ? (int?)null : ((int?)skz_row.RefVPrFVTS - 1), skz_row.VPrFVTS);
										//            }
										//        }
										//    }
										//    else
										//    {
										//        seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((skz_row == null) || (skz_row.IsRelSKzVCNull()) ? (int?)null : (int?)skz_row.RelSKzVC,true);
										//    }
										//}
										//else
										//{
										//    seRow.CZ_SerNum_Track = 0;
										//}

											if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
											{
												seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, skz_row, Classes.Pohoda.TypAgendy.Vydej);
											}
											else
											{
												Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
												var dt_param = ParamTA.GetDataByITEMNMBR(seRow.ITEMNMBR.Trim());

												if ((dt_param != null) && (dt_param.Count > 0))
												{
													dt_row_param = dt_param.First();
												}

												seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, dt_row_param, Classes.Pohoda.TypAgendy.Vydej);
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

                                    if (seRow.VNDITNUM.Length > VNDITNUM_MaxLength)
                                    {
										seRow.VNDITNUM = seRow.VNDITNUM.Remove(VNDITNUM_MaxLength);
                                    }
                                }
                                /*
                                else
                                {
                                    peRow.CZ_CarKod = reader.Value;

                                    if (peRow.CZ_CarKod.Length > VydejDS.CZMST_SE.CZ_CarKodColumn.MaxLength)
                                    {
                                        peRow.CZ_CarKod = peRow.CZ_CarKod.Remove(VydejDS.CZMST_SE.CZ_CarKodColumn.MaxLength);
                                    }
                                }
                                */
                            }
                            else if (actualStrName == "ord:text")
                            {
                                if (InVydejkItem)
                                    seRow.ITEMDESC = reader.Value;

                                if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
                                {
									seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
                                }
                            }
                            else if (actualStrName == "typ:ids")
                            {
                                if (InVydejkItem)
                                {
                                    if (indexTypIDS == 1)
                                    {
                                        seRow.CZ_CarKod = reader.Value;

                                        if (seRow.CZ_CarKod.Length > CZ_CarKod_MaxLength)
                                        {
											seRow.CZ_CarKod = seRow.CZ_CarKod.Remove(CZ_CarKod_MaxLength);
                                        }
                                    }
                                    indexTypIDS++;
                                }
                            }
                            else if (actualStrName == "ord:isExecuted")
                                isExecuted = Convert.ToBoolean(reader.Value);
                            else if (actualStrName == "ord:isDelivered")
                                isDelivered = Convert.ToBoolean(reader.Value);
                            else if (actualStrName == "ord:coefficient")
                                seRow.QTYPACK = 0;
                            else if (actualStrName == "ord:quantity")
                                quantity = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                            else if (actualStrName == "ord:delivered")
                                delivered = decimal.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);


                            break;

                        default:
                            break;
                    }
                }

                trans.Commit();


                return "OK";
            }
            catch (XmlException e)
            {
                try
                {

                    if (trans != null)
                        trans.Rollback();
                }
                catch (System.Exception exx)
                {
                    Fask.Logging.ExceptionHandler2.Handle(exx);
                }

                Fask.Logging.ExceptionHandler2.Handle(e);
                return e.Message;
            }
            catch (Exception ex)
            {
                try
                {

                    if (trans != null)
                        trans.Rollback();
                }
                catch (System.Exception exx)
                {
                    Fask.Logging.ExceptionHandler2.Handle(exx);
                }

                Fask.Logging.ExceptionHandler2.Handle(ex);
                return ex.Message;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private static void SaveData(
            ref decimal quantity, 
            decimal delivered, 
            ref bool incCountEntr, 
            SqlConnection conn,
            SqlTransaction trans,
            Datasets.Vydej.CZMST_SERow seRow)
        {
            if (Globals_V1.Konfigurace.Vydej[0].Zbyva)
                quantity -= delivered;

            if (seRow.IsITEMNMBRNull() || string.IsNullOrEmpty(seRow.ITEMNMBR.Trim()))
                return;

            //Datasets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzTableAdapter();
            //SKzTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
            //Datasets.DatabasePohoda.SKzDataTable skz_table = SKzTableAdapter.GetDataByID(int.Parse(seRow.ITEMNMBR));
			Datasets.DatabasePohoda.SKzDataTable skz_table = Database.Pohoda.SKz_GetDataByID(int.Parse(seRow.ITEMNMBR));
            Datasets.DatabasePohoda.SKzRow skz_row = skz_table.Count > 0 ? skz_table[0] : null;

            // Nacist vychozi lokaci ...
            // az tady, protoze musi mit nastaveny skl_id a itemnmbr...
            LocncodeFindAlgorithmVychozi(seRow);

            //CZMST_SETableAdapter.Insert(
            Database.Vydej.Insert_CZMST_SE(
                trans,
                conn,
                seRow.CountEntries,
                seRow.SOPNUMBE,
                seRow.ITEMNMBR,
                seRow.ITEMTYPE,
                seRow.ITEMDESC,
                seRow.VNDDOCNM,
                seRow.VNDITNUM,
                seRow.ORD,
                seRow.CZ_CarKod,
                seRow.SKL_ID,
                seRow.LOCNCODE,
                quantity,
                seRow.QTYPACK,
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
                seRow.USERID,
                skz_row != null ? skz_row.MJ : string.Empty,
                seRow.CZ_REZ1_Track,
                seRow.CZ_REZ2_Track,
                seRow.ITEMCODE,
                seRow.IsWEIGHTNull() ? (decimal?)null : seRow.WEIGHT,
                0,
                (DateTime?)null,
                (DateTime?)null
                );


            incCountEntr = true;
        }

        internal static void LocncodeFindAlgorithmVychozi(Datasets.Vydej.CZMST_SERow seRow)
        {
			// \TODO : dotazeni vychozi lokace pro polozku skladu ...
            // pro ruzne MJ se muze lisit??? => mozna do budoucna ...
            Provider provider = new Provider();
            Fask.Server.Interfaces.DataSets.Location locationDS = provider.Lokace_VariantySortimentGet(seRow.ITEMNMBR.Trim(), seRow.IsSKL_IDNull() ? string.Empty : seRow.SKL_ID.Trim());
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

            // 1.algoritmus dle pozadavku fy (Vychozi/Dostatek/Nejvetsi/Vychozi)
			// \TODO : nastavit konfiguracni podminku
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

	//[Obsolete("Nepouživá se. Bere se přímo Objektore skladani XML")]
	//    internal static bool CreateImportXML_Vydejka( string filename, int countEntries, string SKL_ID, string p)
	//    {
	//        return CreateRequest_Import_Vydejka_XML_NEW( filename, countEntries, SKL_ID, p);
	//    }


        /// <summary>
        /// Interni metoda pro vytvoreni xml requestu na pohodu pro vytvoreji vydejky
        /// </summary>
        /// <param name="file">nazev vystupniho souboru</param>
        /// <param name="countEntries">cislo davky vydejky, podle ktere ma byt request vytvoren</param>
        /// <param name="SKL_ID">id skladu</param>
        /// <param name="note">poznamka, ktera bude uvedena v poznamce doklau</param>
        /// <returns>True = uspesne vytvoreno, False = neuspech</returns>
        internal static bool CreateRequest_Import_Vydejka_XML_NEW(string filename, int countEntries, string SKL_ID, string note)
        {
            // konstanta pro tostrin() cisel na invariantni format ...
            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;
            Datasets.Vydej.CZMST_SIDataTable dt_si = null;
            try
            {


                if (Globals_V1.Konfigurace.Vydej[0].GrupujDataVydejka)
                    dt_si = Database.Vydej.GETDATA_CZMSTSI_DS_GroupBy_CountEntries(countEntries);
                else
                    dt_si = Database.Vydej.GETDATA_CZMSTSI_By_CountEntries(countEntries);

                //Datasets.DatabasePohodaTableAdapters.OBJpolTableAdapter taOBJPol = new Datasets.DatabasePohodaTableAdapters.OBJpolTableAdapter();
                //taSKZ.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);
                //taOBJPol.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
                //DateTime? datumdokladu = null;

                //Datasets.DatabasePohodaTableAdapters.OBJTableAdapter taOBJ = new Datasets.DatabasePohodaTableAdapters.OBJTableAdapter();
                //taOBJ.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
                Datasets.DatabasePohoda.OBJDataTable OBJdt = null;
                Datasets.DatabasePohoda.OBJRow OBJrow = null;

                //DateTime date = DateTime.Now;

                //var row_id;
                Datasets.Vydej.CZMST_SIRow row_id = null;

                int? cizimenaid = null;

                if ((dt_si != null) && (dt_si.Count > 0))
                {

                    var data = dt_si.OrderBy(x => x.DEX_ROW_ID);
                    row_id = data.First();

                    //OBJdt = taOBJ.GetDataByCislo(dt_si[0].SOPNUMBE);
                    OBJdt = Database.Pohoda.OBJ_GetDataByCislo(dt_si[0].SOPNUMBE);

                    if ((OBJdt != null) && (OBJdt.Count > 0))
                        OBJrow = OBJdt.First();


                    //Cizi meny ... 
                    try
                    {
                        if (!OBJrow.IsRefCMNull())
                            cizimenaid = OBJrow.RefCM;
                    }
                    catch (Exception excm)
                    {
                        Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "CreateImportXML", excm);
                    }
                }
                else
                {
                    throw new Exception("Nenalezena data k importu v tabulce SI");
                }


                List<object> filterList = new List<object>();
                List<object> mainfilterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace vyd = "http://www.stormware.cz/schema/version_2/vydejka.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                #region Header

                List<XElement> listHeader = new List<XElement>();

                //text = "Test import MSTW-Pohoda";
                string text = OBJrow.IsSTextNull() ? null : OBJrow.SText.Trim();

                if (text != null)
                {
                    listHeader.Add(new XElement(vyd + "text", text));
                }

                string poznamka = OBJrow.IsPoznNull() ? null : OBJrow.Pozn;
                string poznamka2 = OBJrow.IsPozn2Null() ? null : OBJrow.Pozn2;

                StringBuilder poznB = new StringBuilder();

                if (!String.IsNullOrEmpty(poznamka))
                    poznB.AppendLine(poznamka);
                if (!String.IsNullOrEmpty(note))
                    poznB.AppendLine(note);
                if (poznB.Length > 0)
                {
                    //tw.WriteLine("              <pri:note>" + poznB.ToString() + "</pri:note>");
                    listHeader.Add(new XElement(vyd + "note", poznB.ToString()));
                }
                //tw.WriteLine("          <pri:intNote>" + (poznamka2 == null ? string.Empty : poznamka2 + "\n") + "nacteno z mobilniho terminalu z dávky č.:" + countEntries.ToString() + "</pri:intNote>");
                listHeader.Add(new XElement(vyd + "intNote", (poznamka2 == null ? string.Empty : poznamka2 + "\n") + "nacteno z Serveru z dávky č.:" + countEntries.ToString()));
               //22.7.2025 MaR
                listHeader.Add(new XElement(vyd + "text", "MST Vytvořeno z předlohy objednávky:" + countEntries.ToString()));

                //if (datumdokladu.HasValue)
                //{
                listHeader.Add(new XElement(vyd + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));
                //}

                if (!dt_si[0].IsVNDDOCNMNull())
                {
                    listHeader.Add(new XElement(vyd + "numberOrder", dt_si[0].VNDDOCNM.Trim()));
                }
                else
                {
                    listHeader.Add(new XElement(vyd + "numberOrder", string.Empty));
                }

                #region Partner
                if (true)
                {

                    XElement IDPartnerFakturacni;

                    if (OBJrow.IsRefADNull())
                    {
                        //pokud neni zavedeny ...
                        List<XElement> listPartnerFakturacni = new List<XElement>();
                        listPartnerFakturacni.Add(new XElement(typ + "company", OBJrow.IsFirmaNull() ? string.Empty : OBJrow.Firma));  // stringCompany je Řetězec o délce 255 znaků
                        listPartnerFakturacni.Add(new XElement(typ + "division", OBJrow.IsUtvarNull() ? string.Empty : OBJrow.Utvar)); //string32
                        listPartnerFakturacni.Add(new XElement(typ + "name", OBJrow.IsJmenoNull() ? string.Empty : OBJrow.Jmeno));     //string32
                        listPartnerFakturacni.Add(new XElement(typ + "city", OBJrow.IsObecNull() ? string.Empty : OBJrow.Obec));     //string45
                        listPartnerFakturacni.Add(new XElement(typ + "street", OBJrow.IsUliceNull() ? string.Empty : OBJrow.Ulice));   //string64
                        listPartnerFakturacni.Add(new XElement(typ + "zip", OBJrow.IsPSCNull() ? string.Empty : OBJrow.PSC));      //string15
                        listPartnerFakturacni.Add(new XElement(typ + "ico", OBJrow.IsICONull() ? string.Empty : OBJrow.ICO));      //icoType string o velkosti 15
                        listPartnerFakturacni.Add(new XElement(typ + "dic", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      //dictype string o velkosti 18

                        //listHeader.Add(new XElement(typ + "VATPayerType", OBJrow.vat IsDICNull() ? string.Empty : OBJrow.DIC));      

                        listPartnerFakturacni.Add(new XElement(typ + "idDph", OBJrow.IsICDPHNull() ? string.Empty : OBJrow.ICDPH));
                        //listHeader.Add(new XElement(typ + "country", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        //listHeader.Add(new XElement(typ + "phone", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        //listHeader.Add(new XElement(typ + "mobilPhone", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        listPartnerFakturacni.Add(new XElement(typ + "fax", OBJrow.IsFaxNull() ? string.Empty : OBJrow.Fax));
                        listPartnerFakturacni.Add(new XElement(typ + "email", OBJrow.IsEmailNull() ? string.Empty : OBJrow.Email));
                        //listHeader.Add(new XElement(typ + "linkToAddress", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));     



                        // Partner
                        //listHeader.Add(new XElement(vyd + "partnerIdentity",
                        //    new XElement(typ + "address", listPartner)));  

                        IDPartnerFakturacni = new XElement(typ + "address", listPartnerFakturacni);

                    }
                    else
                    {
                        //pokud je zavedeny v agende  Adresař tak se pošle ID 

                        IDPartnerFakturacni = new XElement(typ + "id", OBJrow.RefAD);

                        //listHeader.Add(new XElement(vyd + "partnerIdentity",
                        //new XElement(typ + "id", OBJrow.RefAD)));  

                    }

                    XElement IDPartnerDodaci = null;

                    if (!OBJrow.IsFirma2Null()
                        || !OBJrow.IsUtvar2Null()
                        || !OBJrow.IsJmeno2Null()
                        || !OBJrow.IsObec2Null()
                        || !OBJrow.IsUlice2Null()
                        || !OBJrow.IsPSC2Null()
                        || !OBJrow.IsEmail2Null())
                    {
                        List<XElement> listPartnerDodaci = new List<XElement>();
                        listPartnerDodaci.Add(new XElement(typ + "company", OBJrow.IsFirma2Null() ? string.Empty : OBJrow.Firma2));  // stringCompany je Řetězec o délce 255 znaků
                        listPartnerDodaci.Add(new XElement(typ + "division", OBJrow.IsUtvar2Null() ? string.Empty : OBJrow.Utvar2)); //string32
                        listPartnerDodaci.Add(new XElement(typ + "name", OBJrow.IsJmeno2Null() ? string.Empty : OBJrow.Jmeno2));     //string32
                        listPartnerDodaci.Add(new XElement(typ + "city", OBJrow.IsObec2Null() ? string.Empty : OBJrow.Obec2));     //string45
                        listPartnerDodaci.Add(new XElement(typ + "street", OBJrow.IsUlice2Null() ? string.Empty : OBJrow.Ulice2));   //string64
                        listPartnerDodaci.Add(new XElement(typ + "zip", OBJrow.IsPSC2Null() ? string.Empty : OBJrow.PSC2));      //string15
                                                                                                                                 //listPartnerDodaci.Add(new XElement(typ + "ico", OBJrow.IsICONull() ? string.Empty : OBJrow.ICO));      //icoType string o velkosti 15
                                                                                                                                 //listPartnerDodaci.Add(new XElement(typ + "dic", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      //dictype string o velkosti 18

                        //listHeader.Add(new XElement(typ + "VATPayerType", OBJrow.vat IsDICNull() ? string.Empty : OBJrow.DIC));      

                        //listPartnerDodaci.Add(new XElement(typ + "idDph", OBJrow.IsICDPHNull() ? string.Empty : OBJrow.ICDPH));
                        //listHeader.Add(new XElement(typ + "country", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        //listHeader.Add(new XElement(typ + "phone", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        //listHeader.Add(new XElement(typ + "mobilPhone", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        //listPartnerDodaci.Add(new XElement(typ + "fax", OBJrow.IsFaxNull() ? string.Empty : OBJrow.Fax));
                        listPartnerDodaci.Add(new XElement(typ + "email", OBJrow.IsEmail2Null() ? string.Empty : OBJrow.Email2));
                        //listHeader.Add(new XElement(typ + "linkToAddress", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));     

                        IDPartnerDodaci = new XElement(typ + "shipToAddress", listPartnerDodaci);
                    }


                    if (IDPartnerDodaci != null)
                    {
                        listHeader.Add(new XElement(vyd + "partnerIdentity",
                        IDPartnerFakturacni, IDPartnerDodaci));
                    }
                    else
                    {
                        listHeader.Add(new XElement(vyd + "partnerIdentity",
                        IDPartnerFakturacni));
                    }
                }


                #endregion

                #region Středisko

                if (!OBJrow.IsRefStrNull())
                {
                    List<XElement> listStredisko = new List<XElement>();

                    listStredisko.Add(new XElement(typ + "id", OBJrow.RefStr));
                    //listStredisko.Add(new XElement(vyd + "ids", string.Empty)); // otazne či je to potreba 

                    listHeader.Add(new XElement(vyd + "centre", listStredisko));
                }
                #endregion

                #region cinnost

                if (!OBJrow.IsRefCinNull())
                {
                    List<XElement> listcinnost = new List<XElement>();

                    listcinnost.Add(new XElement(typ + "id", OBJrow.RefCin));
                    //listStredisko.Add(new XElement(vyd + "ids", string.Empty)); // otazne či je to potreba 

                    listHeader.Add(new XElement(vyd + "activity", listcinnost));
                }
                #endregion

                #region zakazka

                if (!OBJrow.IsCisloZAKNull())
                {
                    List<XElement> listzakazka = new List<XElement>();

                    listzakazka.Add(new XElement(typ + "ids", OBJrow.CisloZAK));
                    //listStredisko.Add(new XElement(vyd + "ids", string.Empty)); // otazne či je to potreba 

                    listHeader.Add(new XElement(vyd + "contract", listzakazka));
                }
                #endregion

                // 17.10.2019 JiS : oprava odesilani pri registraci dane v jinem clenskem state EU, 
                // pokud je zdrojova objednavka registorvana k dani v jinem clenskem state EU
                if (
                    ((!OBJrow.IsDICRegDPHEUNull()) && (OBJrow.DICRegDPHEU.Trim().Length > 0))
                    ||
                    ((!OBJrow.IsHistSzDPHNull()) && (OBJrow.HistSzDPH))
                    )
                {
                    List<XElement> listDPH = new List<XElement>();

                    listDPH.Add(new XElement(typ + "ids", OBJrow.DICRegDPHEU));  // TaD 19.10.2018 Tady se ma dotahovat DIC s objedvavky, stloupec DICRegDPHEU, v serveru neni tento tloupec, pravdepodobne novy update Pohody
                    listHeader.Add(new XElement(vyd + "regVATinEU", listDPH));
                }


                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();



                #region Zjisteni Vyrizeno

                string Preneseno = Vydej_KontrolaVuciPohodeVykriti(countEntries, dt_si);

                #endregion

                //foreach (Pohoda_DataSets.Vydej.Production_SourcesRow row in dt_ps)
                foreach (Datasets.Vydej.CZMST_SIRow row in dt_si)
                {
                    string Cislo;
                    int RefSKz;

                    try
                    {
                        Cislo = row.SOPNUMBE; // Cislo v OBJ... zebere to pak z OBJ ID a join s OBJPol pomoci RefAg v OBJPol
                        RefSKz = int.Parse(row.ITEMNMBR); // v OBJPol je RefSKz

                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        continue;
                    }

                    //Vyplnovani dane
                    // 0%       none    0
                    // 10%      third   3
                    // 15%      low     1
                    // 21%      high    2



                    //Datasets.DatabasePohoda.OBJpolDataTable dtOBJPol = taOBJPol.GetDataBy_RefAg_RefSKz(RefSKz, Cislo);
                    Datasets.DatabasePohoda.OBJpolDataTable dtOBJPol = Database.Pohoda.OBJPol_GetDataBy_ID(row.ORD);
                    Datasets.DatabasePohoda.OBJpolRow radek = null;

                    if ((dtOBJPol != null) && (dtOBJPol.Count > 0))
                        radek = dtOBJPol.First();

                    #region DPH 

                    //Dotahovani DPH sazby s OBJPol položek objednavky

                    //int sazbaDPH = radek.RelSzDPH;
                    //string typSazbaDPH = "high";

                    //switch (sazbaDPH)
                    //{
                    //    case 0:
                    //        typSazbaDPH = "none";
                    //        break;
                    //    case 1 :
                    //        typSazbaDPH = "low";
                    //        break;
                    //    case 2:
                    //        typSazbaDPH = "high";
                    //        break;
                    //    case 3:
                    //        typSazbaDPH = "third";
                    //        break;
                    //    default:
                    //        typSazbaDPH = "high";
                    //        break;
                    //}

                    #endregion

                    #region

                    //decimal JednotkovaCena = 0;

                    //if (radek.IsCmJednNull())
                    //    JednotkovaCena = radek.KcJedn; // jednotkova cena v CM
                    //else
                    //    JednotkovaCena = radek.CmJedn; // jednotkova cena v kč



                    #endregion

                    List<XElement> vydejkaItem = new List<XElement>();

                    List<XElement> LinkItem = new List<XElement>();

                    LinkItem.Add(new XElement(typ + "sourceAgenda", "receivedOrder"));
                    LinkItem.Add(new XElement(typ + "sourceItemId", row.ORD.ToString()));

                    LinkItem.Add(new XElement(typ + "settingsSourceDocumentOrderItem", new XElement(typ + "linkOrderItemToIssueSlip", Preneseno)));

                    vydejkaItem.Add(new XElement(vyd + "link", LinkItem));

                    vydejkaItem.Add(new XElement(vyd + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));

                    vydejkaItem.Add(new XElement(vyd + "payVAT", false));

                    // 8.10.2018 TaD Upraveno kvuli Cizy mene
                    //Classes.Pohoda.Sazby dphSazba = (Classes.Pohoda.Sazby)radek.RelSzDPH;
                    string dphSazba = Classes.Pohoda.Sazby.GetName(radek.RelSzDPH);
                    //vydejkaItem.Add(new XElement(vyd + "rateVAT", dphSazba.ToString()));
                    vydejkaItem.Add(new XElement(vyd + "rateVAT", dphSazba));


                    if ((radek != null) && (!radek.IsProcentoDPHNull()))
                    {
                        vydejkaItem.Add(new XElement(vyd + "percentVAT", radek.ProcentoDPH.ToString(nfi)));
                    }

                    #region Uprava Ceny

                    if ((radek != null) && (!radek.IsKcJednNull()))
                    {
                        //tw.WriteLine("          <pri:homeCurrency><typ:unitPrice>" + radek.KcJedn.ToString(nfi) + "</typ:unitPrice></pri:homeCurrency>");
                        vydejkaItem.Add(new XElement(vyd + "homeCurrency", new XElement(typ + "unitPrice", radek.KcJedn.ToString(nfi))));
                    }

                    if ((radek != null) && (!radek.IsCmJednNull()))
                    {
                        //tw.WriteLine("          <pri:foreignCurrency><typ:unitPrice>" + radek.CmJedn.ToString(nfi) + "</typ:unitPrice></pri:foreignCurrency>");
                        vydejkaItem.Add(new XElement(vyd + "foreignCurrency", new XElement(typ + "unitPrice", radek.CmJedn.ToString(nfi))));
                    }


                    #endregion


                    if (row.SERLTNUM.Trim().Length > 0)
                        vydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
                    else
                        vydejkaItem.Add(new XElement(vyd + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));


                    if (!row.IsExpiraceNull())
                    {
                        vydejkaItem.Add(new XElement(vyd + "expirationDate", XmlConvert.ToString(row.Expirace, "yyyy-MM-dd")));
                    }

                    #region AttributeToSN

                    var ele = Vypln_AttributeToSN(vyd, typ, row);

                    if (ele != null)
                        vydejkaItem.Add(ele);

                    #endregion


                    XElement item = new XElement(vyd + "vydejkaItem", vydejkaItem);

                    listItem.Add(item);
                }



                #endregion


                #region Summary

                List<XElement> vydejkaSummary = new List<XElement>();

                if (cizimenaid.HasValue)
                {


                    XElement currency = new XElement(typ + "currency", new XElement(typ + "id", cizimenaid));

                    if (!OBJrow.IsCmKursNull())
                    {
                        vydejkaSummary.Add(new XElement(vyd + "foreignCurrency", currency, new XElement(typ + "rate", OBJrow.CmKurs.ToString(nfi))));
                    }
                    else
                    {
                        vydejkaSummary.Add(new XElement(vyd + "foreignCurrency", currency));

                    }

                }

                #endregion


                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "vyd", "http://www.stormware.cz/schema/version_2/vydejka.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "VYD" + row_id.DEX_ROW_ID.ToString()),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Import Vydejky"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "VYD" + row_id.DEX_ROW_ID.ToString()),
                            new XAttribute("version", "2.0"),

                                new XElement(vyd + "vydejka",
                                    new XAttribute("version", "2.0"),
                                    new XElement(vyd + "vydejkaHeader", listHeader),
                                    new XElement(vyd + "vydejkaDetail", listItem),
                                    new XElement(vyd + "vydejkaSummary", vydejkaSummary)
                                        )));

                root.Save(filename);

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                //Logging.Log.Write(ex.ToString());
                return false;
            }
            return true;
        }

        //internal static string LoadVydejImportResponseXML(string filename, int countEntries)
        internal static string LoadVydej_ImportVydejka_ResponseXML(string filename, int countentries, string SKL_ID)
        {
            XML.MST_Pohoda.CheckExistResponseFile(filename);

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

                //cislo dokladu nalezeno => aktualizace dokladu v pohode...
                //1) zjistit cislo dokladu Vydejky
                //2) vytahnout polozky z se s ord, itemnmbr, sopnumbe?
                //2a) cislo dokladu objednavky
                //3) vytahnout polozky z skpv a skpvpol pro konkretni novy doklad
                //4) vytahnout polozka z obj a objpol pro doklad
                //5) sparovat polozky obj(se) s skpp
                // a) Karta Zasoby => Objednano < nutne ponizit hodnotu o prijate mnozstvi
                // b) oznaceni preneseno na polozce objednavky
                //6) update v transakci ...


                // 1) Cislo dokladu prijemky
                string cislodokladuvydejka = response.CisloDokladuPrijemka;
                if (String.IsNullOrEmpty(cislodokladuvydejka))
                    return "Cislo dokladu vydejka nenalezeno v odpovedi pohody";

                // 2) Vytahnout polozky prijemky z MST
                Datasets.Vydej VydejDS = new Datasets.Vydej();

                //Datasets.VydejTableAdapters.CZMST_SE1TableAdapter seAdapter = new Datasets.VydejTableAdapters.CZMST_SE1TableAdapter();
                //Datasets.VydejTableAdapters.CZMST_SITableAdapter siAdapter = new Datasets.VydejTableAdapters.CZMST_SITableAdapter();
                //seAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
                //siAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                //seAdapter.FillByCountEntries(VydejDS.CZMST_SE, countentries);
                //siAdapter.FillByCountEntries(VydejDS.CZMST_SI, countentries);

                //TaD 22.8.2018 Uprava když davka obsahuje vic objednavek...

                #region Uprave funkcionalita...

                //var data = (Datasets.Vydej.CZMST_SEDataTable)VydejDS.CZMST_SE.GroupBy(x => x.SOPNUMBE);

                //seAdapter.Fill(VydejDS.CZMST_SE1, countentries);
                Database.Vydej.Fill_CZMST_SE_SOPNUMBE(VydejDS.CZMST_SE_SOPNUMBE, countentries);


                //foreach (Datasets.Vydej.CZMST_SE1Row JednaObjednavka in VydejDS.CZMST_SE1)
                foreach (Datasets.Vydej.CZMST_SE_SOPNUMBERow JednaObjednavka in VydejDS.CZMST_SE_SOPNUMBE)
				{
					//item.SOPNUMBE;
					//string cislodokladuobjednavky = VydejDS.CZMST_SE[0].SOPNUMBE.Trim();
					string cislodokladuobjednavky = JednaObjednavka.SOPNUMBE;


					VydejDS.CZMST_SI.Clear();

					if (Globals_V1.Konfigurace.Vydej[0].GrupujDataVydejka)
                        Database.Vydej.Fill_CZMSTSI_GroupBy_CountEntries_SOPNUMBE(VydejDS, countentries, cislodokladuobjednavky.Trim());
					else
                        Database.Vydej.Fill_CZMSTSI_By_CountEntries_SOPNUMBE(VydejDS, countentries, cislodokladuobjednavky.Trim());


					Datasets.DatabasePohoda pohodaDS = new Datasets.DatabasePohoda();
					oledbConnection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);

					//3) vytahnout polozky z skpp a skppol pro konkretni novy doklad
					//Datasets.DatabasePohodaTableAdapters.SKPVTableAdapter skpvAdapter = new Datasets.DatabasePohodaTableAdapters.SKPVTableAdapter();
					//Datasets.DatabasePohodaTableAdapters.SKPVpolTableAdapter skpvpolAdapter = new Datasets.DatabasePohodaTableAdapters.SKPVpolTableAdapter();
					//skpvAdapter.Connection = oledbConnection;
					//skpvpolAdapter.Connection = oledbConnection;

					//skpvAdapter.FillByCislo(pohodaDS.SKPV, cislodokladuvydejka);
					Database.Pohoda.SKPV_FillByCislo(pohodaDS.SKPV, cislodokladuvydejka);
					//skpvpolAdapter.FillByRefAg(pohodaDS.SKPVpol, pohodaDS.SKPV[0].ID);
					Database.Pohoda.SKPVpol_FillByRefAg(pohodaDS.SKPVpol, pohodaDS.SKPV[0].ID);

					//4) vytahnout polozky objednavky z pohody
					//28.11.2018 TaD Dotahovani z DB ve vlastni reži
					Database.Pohoda.OBJ_FillByCislo(pohodaDS.OBJ, cislodokladuobjednavky);
					Database.Pohoda.OBJPol_FillByRefAg(pohodaDS.OBJpol, pohodaDS.OBJ[0].ID);

					//5) sparovani ...

					// 5)a) dohledani karty zasoby a zmena mnozstvi hodnoty OBJV
					// 5)b) take aktualizace hodnoty v bufferu SKzBuf, ktery nese informaci o objednanem mnozstvi ... 
					//      => toto na zaklade spoctene hodnoty v SKz, protoze tam je to platne... ???
					//Datasets.DatabasePohodaTableAdapters.SKzObjedPTableAdapter skzObjedPAdapter = new Datasets.DatabasePohodaTableAdapters.SKzObjedPTableAdapter();
					//skzObjedPAdapter.Connection = oledbConnection;
					//skzObjedPAdapter.ClearBeforeFill = false;

					//Datasets.DatabasePohodaTableAdapters.SKzBufTableAdapter skzBufAdapter = new Datasets.DatabasePohodaTableAdapters.SKzBufTableAdapter();
					//skzBufAdapter.Connection = oledbConnection;
					//skzBufAdapter.ClearBeforeFill = false;

					oledbConnection.Open();
					// transakce se resi na konci v ta managerovi ... 
					//System.Data.OleDb.OleDbTransaction oletrans = oledbConnection.BeginTransaction();

					foreach (Datasets.Vydej.CZMST_SIRow sir in VydejDS.CZMST_SI)
					{
						// \TODO : dodelat provazovani a SKzObjedP 

						//neprirazene polozky dle itemnmbr a mnozstvi
						string query = " RefSKz=" + sir.ITEMNMBR.Trim() + " AND Mnozstvi=" + sir.QTYSHPPD.ToString("0.00", CultureInfo.InvariantCulture) + " AND RefPol is NULL AND RelAgID is NULL";
						Datasets.DatabasePohoda.SKPVpolRow[] neprirazenePolozky = (Datasets.DatabasePohoda.SKPVpolRow[])pohodaDS.SKPVpol.Select(
							query
							, "ID"
							, System.Data.DataViewRowState.CurrentRows);
						if (neprirazenePolozky.Length == 0)
						{ //nic se nedeje, protoze nebyly nalezeny => log... jde o chybu ...
							Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error,"VydejPohodaXML", "LoadVydejImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo volnou shodu v databazi skpvpol s polozkou:" + sir.ORD + "|" + sir.ITEMNMBR + "|" + sir.QTYSHPPD + "|" + sir.GUID.ToString());
							continue; //pokracuji dalsim radkem ... 
						}
						else if (neprirazenePolozky.Length > 0)
						{ //je jich vice, no tak priradim prvni nalezenou, protoze je to v podstate uplne sumus...
							//prirazeni se deje az po dohledani radku objednavky ...
						}

						Datasets.DatabasePohoda.OBJpolRow objpolr = pohodaDS.OBJpol.FindByID(sir.ORD);
						if (objpolr != null)
						{
							//experimentalne zjistena hodnota pro dokladovou vazbu na vydanou objednavku ... 
							// CHECK : !!! muze se v aktualizaci pohody zmenit !!!
							neprirazenePolozky[0].RelAgID = 11;  //TaD s pohody zisteno 
							neprirazenePolozky[0].RefPol = objpolr.ID;

							if (!pohodaDS.OBJ[0].TrvalyDok)
							{
								objpolr.Dodano += (double)sir.QTYSHPPD; // \TODO : zaokroulovani????
								// 5)a) dohledani / dotazeni zbozi karty
								int skzID = int.Parse(sir.ITEMNMBR);
								Datasets.DatabasePohoda.SKzObjedPRow skzObjedPRow = pohodaDS.SKzObjedP.FindByID(skzID);
								if (skzObjedPRow == null)
								{
									//skzObjedPAdapter.FillByID(pohodaDS.SKzObjedP, int.Parse(sir.ITEMNMBR));
									Database.Pohoda.SKzObjedP_FillByID(pohodaDS.SKzObjedP, int.Parse(sir.ITEMNMBR));
									skzObjedPRow = pohodaDS.SKzObjedP.FindByID(skzID);
								}
								if (skzObjedPRow == null)
									Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "VydejPohodaXML", "LoadVydejImportResponseXmlAndMakeUpdateDB", "Nenalezena karta zasoby pro snizeni objednaneho mnozstvi:" + sir.ORD + "|" + sir.ITEMNMBR + "|" + sir.QTYSHPPD + "|" + sir.GUID.ToString());
								else
									skzObjedPRow.ObjedP -= (double)sir.QTYSHPPD; //pokud jiz bylo v cyklu drive snizeno, tak se snizi opet ...
							}
						}
						else //je-li null, tak se nenaslo zbozi ... coz je chyba a bude zalogovana ...
						{
							Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "VydejPohodaXML", "LoadVydejImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo volnou shodu v databazi objpol s polozkou:" + sir.ORD + "|" + sir.ITEMNMBR + "|" + sir.QTYSHPPD + "|" + sir.GUID.ToString());
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
					if (!pohodaDS.OBJ[0].TrvalyDok)
					{
						bool vyrizeno = pohodaDS.OBJ[0].Vyrizeno;
						if (!vyrizeno)
						{
							if (pohodaDS.OBJ[0].BDodano && Globals_V1.Konfigurace.Vydej[0].Pohoda_Objednavka_Prijata_Vyrizeno_Nastavit)
							{
								pohodaDS.OBJ[0].Vyrizeno = true;
							}
							else if (Globals_V1.Konfigurace.Vydej[0].Pohoda_Objednavka_Prijata_Vyrizeno_Nastavit_Castecne_Plneni) // neni uplne dodano
							{
								pohodaDS.OBJ[0].Vyrizeno = true;

								// Pokud menim na vyrizeno, tak musim aktualizovat hodnoty mnozstvi na karte zasob v skz a skzBuf..???
								// pro kazdou polozku objednavky
								foreach (var item in pohodaDS.OBJpol)
								{
									pohodaDS.OBJ[0].Vyrizeno = true;

									// jeste neco zbyva dodat
									// ??? co zaokrouhlovaci chyba??? (prevest na decimal 5desmist?)
									if (item.Dodano < item.Mnozstvi)
									{
										double zbyvadodat = item.Mnozstvi - item.Dodano; // toto se musi ponizit

										// 5)a) dohledani / dotazeni zbozi karty
										int skzID = item.RefSKz; //int.Parse(pir.ITEMNMBR);
										Datasets.DatabasePohoda.SKzObjedPRow skzObjedPRow = pohodaDS.SKzObjedP.FindByID(skzID);
										if (skzObjedPRow == null)
										{
											//skzObjedPAdapter.FillByID(pohodaDS.SKzObjedP, skzID); //pir.ITEMNMBR));
											Database.Pohoda.SKzObjedP_FillByID(pohodaDS.SKzObjedP, skzID); //pir.ITEMNMBR));
											skzObjedPRow = pohodaDS.SKzObjedP.FindByID(skzID);
										}
										if (skzObjedPRow == null)
											Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PrijemPohodaXML", "LoadPrijemImportResponseXmlAndMakeUpdateDB", "Nenalezena karta zasoby pro snizeni objednaneho mnozstvi:" + skzID + "| zbyva dodat: " + zbyvadodat);
										else
											skzObjedPRow.ObjedP -= zbyvadodat; //(double)pir.QTYSHPPD; //pokud jiz bylo v cyklu drive snizeno, tak se snizi opet ...
									}
								}
							}
						}
					}

					// 5 d) dokonceni update hodnoty v SKzBuf...
					//??? Je to vubec treba ??? k cemu je a jak se plni SKzBuf???
					// \TODO : dodelat provazovani a SKzObjedP 
					foreach (var item in pohodaDS.SKzObjedP)
					{
						//skzBufAdapter.FillByRefSkz(pohodaDS.SKzBuf, item.ID);
						Database.Pohoda.SKzBuf_FillByRefSkz(pohodaDS.SKzBuf, item.ID);
						var skzbufrows = pohodaDS.SKzBuf.Where(x => x.RefSKz == item.ID);
						if (skzbufrows.Count() > 0)
						{
							foreach (var skzbufrow in skzbufrows)
							{
								skzbufrow.ObjedP = item.ObjedP;
							}
						}
						else
						{
							Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "VydejPohodaXML", "LoadVydejImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo shodu v databazi skzbuf s polozkou:" + item.ID + "|" + item.ObjedP);
						}
					}

					//7) update v transakci ... 

					//Datasets.DatabasePohodaTableAdapters.TableAdapterManager pohodaTaManager = new Datasets.DatabasePohodaTableAdapters.TableAdapterManager();
					//pohodaTaManager.OBJpolTableAdapter = objpolAdapter;
					//pohodaTaManager.SKPVpolTableAdapter = skpvpolAdapter;
					//pohodaTaManager.OBJTableAdapter = objAdapter;
					//pohodaTaManager.SKzObjedPTableAdapter = skzObjedPAdapter;
					//pohodaTaManager.SKzBufTableAdapter = skzBufAdapter;

					//pohodaTaManager.Connection = oledbConnection;
					//transakce se otevira v managerovi ... 
					//pohodaTaManager.Connection.Open();
					//System.Data.IDbTransaction pohodaTransaction = pohodaTaManager.Connection.BeginTransaction();
					//pohodaTaManager.UpdateOrder = Datasets.DatabasePohodaTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
					//pohodaTaManager.UpdateAll(pohodaDS);

					// Udelat Inser, Update a Delete Tabulek


					System.Data.OleDb.OleDbTransaction trans = null;
					System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
					try
					{

						Database.Pohoda.OBJPol_Update(pohodaDS, connection, trans);
						Database.Pohoda.SKPVpol_Update(pohodaDS, connection, trans);
						Database.Pohoda.OBJ_Update(pohodaDS, connection, trans);
						Database.Pohoda.SKzObjedP_Update(pohodaDS, connection, trans);
						Database.Pohoda.SKzBuf_Update(pohodaDS, connection, trans);

						if (trans != null)
							trans.Commit();
					}
					catch (Exception ex)
					{
						if (trans != null)
							trans.Rollback();

						throw ex;

					}

				}

                #endregion

                #region puvodni funkcionalita....

                /***********************************
                 
                string cislodokladuobjednavky = VydejDS.CZMST_SE[0].SOPNUMBE.Trim();

                Datasets.DatabasePohoda pohodaDS = new Datasets.DatabasePohoda();
                oledbConnection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);

                //3) vytahnout polozky z skpp a skppol pro konkretni novy doklad
                Datasets.DatabasePohodaTableAdapters.SKPVTableAdapter skpvAdapter = new Datasets.DatabasePohodaTableAdapters.SKPVTableAdapter();
                Datasets.DatabasePohodaTableAdapters.SKPVpolTableAdapter skpvpolAdapter = new Datasets.DatabasePohodaTableAdapters.SKPVpolTableAdapter();
                skpvAdapter.Connection = oledbConnection;
                skpvpolAdapter.Connection = oledbConnection;

                skpvAdapter.FillByCislo(pohodaDS.SKPV, cislodokladuvydejka);
                skpvpolAdapter.FillByRefAg(pohodaDS.SKPVpol, pohodaDS.SKPV[0].ID);

                //4) vytahnout polozky objednavky z pohody
                Datasets.DatabasePohodaTableAdapters.OBJTableAdapter objAdapter = new Datasets.DatabasePohodaTableAdapters.OBJTableAdapter();
                Datasets.DatabasePohodaTableAdapters.OBJpolTableAdapter objpolAdapter = new Datasets.DatabasePohodaTableAdapters.OBJpolTableAdapter();
                objAdapter.Connection = oledbConnection;
                objpolAdapter.Connection = oledbConnection;

                objAdapter.FillByCislo(pohodaDS.OBJ, cislodokladuobjednavky);
                objpolAdapter.FillByRefAg(pohodaDS.OBJpol, pohodaDS.OBJ[0].ID);

                //5) sparovani ...

                // 5)a) dohledani karty zasoby a zmena mnozstvi hodnoty OBJV
                // 5)b) take aktualizace hodnoty v bufferu SKzBuf, ktery nese informaci o objednanem mnozstvi ... 
                //      => toto na zaklade spoctene hodnoty v SKz, protoze tam je to platne... ???
                Datasets.DatabasePohodaTableAdapters.SKzObjedVTableAdapter skzObjedVAdapter = new Datasets.DatabasePohodaTableAdapters.SKzObjedVTableAdapter();
                skzObjedVAdapter.Connection = oledbConnection;
                skzObjedVAdapter.ClearBeforeFill = false;

                Datasets.DatabasePohodaTableAdapters.SKzBufTableAdapter skzBufAdapter = new Datasets.DatabasePohodaTableAdapters.SKzBufTableAdapter();
                skzBufAdapter.Connection = oledbConnection;
                skzBufAdapter.ClearBeforeFill = false;

                oledbConnection.Open();
                // transakce se resi na konci v ta managerovi ... 
                //System.Data.OleDb.OleDbTransaction oletrans = oledbConnection.BeginTransaction();

                foreach (Datasets.Vydej.CZMST_SIRow sir in VydejDS.CZMST_SI)
                {
                    //neprirazene polozky dle itemnmbr a mnozstvi
                    string query = " RefSKz=" + sir.ITEMNMBR.Trim() + " AND Mnozstvi=" + sir.QTYSHPPD.ToString("0.00", CultureInfo.InvariantCulture) + " AND RefPol is NULL AND RelAgID is NULL";
                    Datasets.DatabasePohoda.SKPVpolRow[] neprirazenePolozky = (Datasets.DatabasePohoda.SKPVpolRow[])pohodaDS.SKPVpol.Select(
                        query
                        , "ID"
                        , System.Data.DataViewRowState.CurrentRows);
                    if (neprirazenePolozky.Length == 0)
                    { //nic se nedeje, protoze nebyly nalezeny => log... jde o chybu ...
                        Log.writeErrorLog("VydejPohodaXML", "LoadVydejImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo volnou shodu v databazi skpvpol s polozkou:" + sir.ORD + "|" + sir.ITEMNMBR + "|" + sir.QTYSHPPD + "|" + sir.GUID.ToString());
                        continue; //pokracuji dalsim radkem ... 
                    }
                    else if (neprirazenePolozky.Length > 0)
                    { //je jich vice, no tak priradim prvni nalezenou, protoze je to v podstate uplne sumus...
                        //prirazeni se deje az po dohledani radku objednavky ...
                    }

                    Datasets.DatabasePohoda.OBJpolRow objpolr = pohodaDS.OBJpol.FindByID(sir.ORD);
                    if (objpolr != null)
                    {
                        objpolr.Dodano += (double)sir.QTYSHPPD; // TODO : zaokroulovani????
                        //experimentalne zjistena hodnota pro dokladovou vazbu na vydanou objednavku ... 
                        // CHECK : !!! muze se v aktualizaci pohody zmenit !!!
                        neprirazenePolozky[0].RelAgID = 11;  //TaD s pohody zisteno 
                        neprirazenePolozky[0].RefPol = objpolr.ID;

                        // 5)a) dohledani / dotazeni zbozi karty
                        int skzID = int.Parse(sir.ITEMNMBR);
                        Datasets.DatabasePohoda.SKzObjedVRow skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                        if (skzObjedVRow == null)
                        {
                            skzObjedVAdapter.FillByID(pohodaDS.SKzObjedV, int.Parse(sir.ITEMNMBR));
                            skzObjedVRow = pohodaDS.SKzObjedV.FindByID(skzID);
                        }
                        if (skzObjedVRow == null)
                            Log.writeErrorLog("VydejPohodaXML", "LoadVydejImportResponseXmlAndMakeUpdateDB", "Nenalezena karta zasoby pro snizeni objednaneho mnozstvi:" + sir.ORD + "|" + sir.ITEMNMBR + "|" + sir.QTYSHPPD + "|" + sir.GUID.ToString());
                        else
                            skzObjedVRow.ObjedV -= (double)sir.QTYSHPPD; //pokud jiz bylo v cyklu drive snizeno, tak se snizi opet ...
                    }
                    else //je-li null, tak se nenaslo zbozi ... coz je chyba a bude zalogovana ...
                    {
                        Log.writeErrorLog("VydejPohodaXML", "LoadVydejImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo volnou shodu v databazi objpol s polozkou:" + sir.ORD + "|" + sir.ITEMNMBR + "|" + sir.QTYSHPPD + "|" + sir.GUID.ToString());
                    }

                }

                //// 5 a) dokonceni update hodnoty v SKzBuf...
                // ??? Je to vubec treba ??? k cemu je a jak se plni SKzBuf???
                foreach (var item in pohodaDS.SKzObjedV)
                {
                    skzBufAdapter.FillByRefSkz(pohodaDS.SKzBuf, item.ID);
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
                        Log.writeErrorLog("VydejPohodaXML", "LoadVydejImportResponseXmlAndMakeUpdateDB", "Parovani nenalezlo shodu v databazi skzbuf s polozkou:" + item.ID + "|" + item.ObjedV);
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

               // if (Properties.Settings.Default.Pohoda_Objednavka_Set_Vyrizeno)
                pohodaDS.OBJ[0].Vyrizeno = bdodano;

                //6) update v transakci ... 

                Datasets.DatabasePohodaTableAdapters.TableAdapterManager pohodaTaManager = new Datasets.DatabasePohodaTableAdapters.TableAdapterManager();
                pohodaTaManager.OBJpolTableAdapter = objpolAdapter;
                pohodaTaManager.SKPVpolTableAdapter = skpvpolAdapter;
                pohodaTaManager.OBJTableAdapter = objAdapter;
                pohodaTaManager.SKzObjedVTableAdapter = skzObjedVAdapter;
                pohodaTaManager.SKzBufTableAdapter = skzBufAdapter;

                pohodaTaManager.Connection = oledbConnection;
                //transakce se otevira v managerovi ... 
                //pohodaTaManager.Connection.Open();
                //System.Data.IDbTransaction pohodaTransaction = pohodaTaManager.Connection.BeginTransaction();
                pohodaTaManager.UpdateOrder = Datasets.DatabasePohodaTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
                pohodaTaManager.UpdateAll(pohodaDS);

                ***************************************************/
                #endregion

                return "OK";

            }
            catch (Exception e)
            {
				Fask.Logging.ExceptionHandler2.Handle(e);
                return e.Message;
            }
            finally
            {
                if (oledbConnection != null && ((oledbConnection.State & ConnectionState.Open) == ConnectionState.Open))
                    oledbConnection.Close();
            }

          
        }

        internal static bool CreateRequest_PrjateObjednavky_XML(string filename, Fask.Server.Interfaces.Classes.Objednavka objednavka)
        {
            try
            {

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
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Export s Serveru"),

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

        internal static string LoadResponse_PrjateObjednavky_XML(string filename, bool save, Fask.Server.Interfaces.Classes.Objednavka objednavka)
        {
            XML.MST_Pohoda.CheckExistResponseFile(filename);

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

			int SOPNUMBE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["SOPNUMBE"].MaxLength;
			int SKL_ID_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["SKL_ID"].MaxLength;
			int VNDITNUM_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["VNDITNUM"].MaxLength;
			int CZ_CarKod_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["CZ_CarKod"].MaxLength;
			int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
            int ITEMCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMCODE"].MaxLength;
            
            // dotazeni informace o polozce z pohoda
            //Datasets.DatabasePohodaTableAdapters.SKzTableAdapter SKzTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzTableAdapter();
            //SKzTableAdapter.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);// .ConnectionString = Globals.ConnectionStringPohodaDB;

            // dotazeni parametru pro locncode (vychozi lokaci ..)
            //Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
            //SKzParametryTableAdapter.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);

            Datasets.Vydej VydejDS = new Datasets.Vydej();

            //Datasets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
            //CZMST_SETableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

			Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
			if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
			{
				ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
				ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			}

            Datasets.Vydej.CZMST_SERow seRow = null;

            XmlTextReader reader = new XmlTextReader(filename);

            

            SqlTransaction trans = null;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                conn.Open();
                trans = conn.BeginTransaction();

                seRow = VydejDS.CZMST_SE.NewCZMST_SERow();

                NastavPromenne(seRow);

                //CZMST_SETableAdapter.Connection.Open();
                //CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                //actualCountEntries = Database.Vydej.CZMSTSE_MAX_CountEntries(CZMST_SETableAdapter.Connection, CZMST_SETableAdapter.Transaction);
                actualCountEntries = Database.Vydej.CZMSTSE_MAX_CountEntries(conn, trans);
                //objednavka.CisloDavky = actualCountEntries;
                //trans.Commit();

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

                                    if (seRow.SOPNUMBE.Length > SOPNUMBE_MaxLength)
                                    {
										seRow.SOPNUMBE = seRow.SOPNUMBE.Remove(SOPNUMBE_MaxLength);
                                    }


                                    if (Database.Vydej.CZMSTSE_EXIST_SOPNUMBE(seRow.SOPNUMBE) && !check)
                                    { // toto cislo davky se uz v db vyskytuje -> smazeme jej
                                        check = true;

                                        //if (Database.Vydej.CZMSTSE_CZDOSLO_SOPNUMBE_InUse(seRow.SOPNUMBE))
                                        //{
                                        //    return "Nelze generovat...";
                                        //}


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
                                        SaveData_toDB(ref quantity, delivered, ref incCountEntr, conn, trans, seRow);
                                    }
                                    else if (Globals_V1.Konfigurace.Vydej[0].StatusObjednavky)
                                    {
                                        if ((Globals_V1.Konfigurace.Vydej[0].Executed == isExecuted) && (Globals_V1.Konfigurace.Vydej[0].Delivered == isDelivered))
                                        {
											SaveData_toDB(ref quantity, delivered, ref incCountEntr, conn, trans, seRow);
                                        }

                                    }
                                    else
                                    {
										SaveData_toDB(ref quantity, delivered, ref incCountEntr, conn, trans, seRow);
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

                                    if (seRow.SOPNUMBE.Length > SOPNUMBE_MaxLength)
                                    {
										seRow.SOPNUMBE = seRow.SOPNUMBE.Remove(SOPNUMBE_MaxLength);
                                    }

                                    if (Database.Vydej.CZMSTSE_EXIST_SOPNUMBE(seRow.SOPNUMBE) && !check)
                                    { // toto cislo davky se uz v db vyskytuje -> smazeme jej
                                        check = true;

                                        //if (Database.Vydej.CZMSTSE_CZDOSLO_SOPNUMBE_InUse(seRow.SOPNUMBE))
                                        //{
                                        //    return "Nelze generovat...";
                                        //}

                                        //{
                                        //    check = true;
                                        //}


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
										SaveData_toDB(ref quantity, delivered, ref incCountEntr, conn, trans, seRow);

                                    }
                                    else if (Globals_V1.Konfigurace.Vydej[0].StatusObjednavky)
                                    {
                                        if ((Globals_V1.Konfigurace.Vydej[0].Executed == isExecuted) && (Globals_V1.Konfigurace.Vydej[0].Delivered == isDelivered))
                                        {
											SaveData_toDB(ref quantity, delivered, ref incCountEntr, conn, trans, seRow);
                                        }
                                    }
                                    else
                                    {
										SaveData_toDB(ref quantity, delivered, ref incCountEntr, conn, trans, seRow);
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
                                        if (seRow.SKL_ID.Length > SKL_ID_MaxLength)
                                        {
											seRow.SKL_ID = seRow.SKL_ID.Remove(SKL_ID_MaxLength);
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
                                            //Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, int.Parse(seRow.ITEMNMBR));
											Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, int.Parse(seRow.ITEMNMBR));
                                            Datasets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                                            if (skzParametry_row != null)
                                            {
                                                locncodedefault = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                                            }
                                        }
                                        seRow.LOCNCODE = locncodedefault;

                                        // 13.6.2016 PeV: presunuto do metody SaveData, az jsou znamy vsechna data
                                        //LocncodeFindAlgorithmVychozi(seRow);
                                        
                                        var skzdt = Database.Pohoda.SKz_GetDataByID(int.Parse(seRow.ITEMNMBR));

                                        if ((skzdt != null) && (skzdt.Count > 0))
                                        {
                                            //19.9.2018 TaD nova logika SerNumTrack
                                            var skz_row = skzdt[0];

                                            if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                                            {
                                                int? tmp_RelSKzVC = skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC;

                                                seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(tmp_RelSKzVC, skz_row, Classes.Pohoda.TypAgendy.Vydej);

                                                if (tmp_RelSKzVC.HasValue)
                                                {
                                                    if (tmp_RelSKzVC.Value == 2 && seRow.CZ_SerNum_Track == 2)
                                                    {
                                                        bool? expTrack = skz_row.IsVPrCZExpTrackISNull() ? (bool?)null : skz_row.VPrCZExpTrackIS;

                                                        if (expTrack.HasValue && expTrack.Value)
                                                        {
                                                            seRow.CZ_Expirace_Track = 1;
                                                        }
                                                    }

                                                    if (tmp_RelSKzVC.Value == 1 && seRow.CZ_SerNum_Track == 1)
                                                    {
                                                        bool? expTrack = skz_row.IsVPrCZExpTrackISNull() ? (bool?)null : skz_row.VPrCZExpTrackIS;

                                                        if (expTrack.HasValue && expTrack.Value)
                                                        {
                                                            seRow.CZ_Expirace_Track = 1;
                                                        }
                                                        else
                                                        {
                                                            seRow.CZ_Expirace_Track = 0;
                                                        }

                                                        //bool? SerTrack = skz_row.IsVPrCZSerNumTrISNull() ? (bool?)null : skz_row.VPrCZSerNumTrIS;

                                                        //if (SerTrack.HasValue && SerTrack.Value)
                                                        //{
                                                        //    seRow.CZ_SerNum_Track = 10;
                                                        //}

                                                        bool? SerTrackIGN = skz_row.IsVPrCZSNumTrIGNNull() ? (bool?)null : skz_row.VPrCZSNumTrIGN;

                                                        if (SerTrackIGN.HasValue && SerTrackIGN.Value)
                                                        {
                                                            seRow.CZ_SerNum_Track = 11;
                                                        }

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
                                                var dt_param = ParamTA.GetDataByITEMNMBR(seRow.ITEMNMBR.Trim());

                                                if ((dt_param != null) && (dt_param.Count > 0))
                                                {
                                                    dt_row_param = dt_param.First();
                                                }

                                                seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, dt_row_param, Classes.Pohoda.TypAgendy.Vydej);
                                            }

                                        }
                                        else
                                        {
                                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "Nebylo nalezeno zbozi v SKz, hodnota CZ_SerNum_Track byla nastavena na 0");
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

                                    if (seRow.VNDITNUM.Length > VNDITNUM_MaxLength)
                                    {
										seRow.VNDITNUM = seRow.VNDITNUM.Remove(VNDITNUM_MaxLength);
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

                                        if (seRow.CZ_CarKod.Length > CZ_CarKod_MaxLength)
                                        {
											seRow.CZ_CarKod = seRow.CZ_CarKod.Remove(CZ_CarKod_MaxLength);
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

                                if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
                                {
									seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
                                }

                            }
                            else if (actualStrName == "ord:code")
                            {
                                if (InVydejkItem)
                                    seRow.ITEMCODE = reader.Value;

                                if (seRow.ITEMCODE.Length > ITEMCODE_MaxLength)
                                {
                                    seRow.ITEMCODE = seRow.ITEMCODE.Remove(ITEMCODE_MaxLength);
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
                //Datasets.DatabasePohodaTableAdapters.SKzNCTableAdapter skznc_ta = new Datasets.DatabasePohodaTableAdapters.SKzNCTableAdapter();
                //skznc_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

                Datasets.Vydej vydej_ds = new Datasets.Vydej();
                Datasets.Vydej vydej_ds_added = new Datasets.Vydej();
                //CZMST_SETableAdapter.FillByCountEntries(VydejDS.CZMST_SE, int.Parse(objednavka.CisloDavky));
                Database.Vydej.Fill_CZMSTSE_By_CountEntries(VydejDS.CZMST_SE, int.Parse(objednavka.CisloDavky));
				// \TODO : kolekce byla zmenena => musim udelat do noveho a ten pak updatnout ... 
                foreach (Datasets.Vydej.CZMST_SERow serow in VydejDS.CZMST_SE)
                {
                    //Datasets.DatabasePohoda.SKzNCDataTable skznc_dt = skznc_ta.GetDataSKzID(int.Parse(serow.ITEMNMBR));
					Datasets.DatabasePohoda.SKzNCDataTable skznc_dt = Database.Pohoda.SKzNC_GetDataSKzID(int.Parse(serow.ITEMNMBR));

					if ((skznc_dt != null) && (skznc_dt.Count > 0))
					{
						

                    foreach (Datasets.DatabasePohoda.SKzNCRow skzncrow in skznc_dt)
                    {
							if (skzncrow.IsEANNull())
								continue;

                            var Row = vydej_ds_added.CZMST_SE.NewCZMST_SERow();

                            Row.CountEntries = serow.CountEntries;
                            Row.SOPNUMBE = serow.SOPNUMBE;
                            Row.ITEMNMBR = serow.ITEMNMBR;
                            Row.ITEMTYPE = serow.ITEMTYPE;
                            Row.ITEMDESC = serow.ITEMDESC;
                            Row.VNDDOCNM = serow.VNDDOCNM;
                            Row.VNDITNUM = skzncrow.EAN;
                            Row.ORD = serow.ORD;
                            Row.CZ_CarKod = serow.CZ_CarKod;
                            Row.LOCNCODE = serow.LOCNCODE;
                            Row.QTYSHPPD = serow.QTYSHPPD;
                            Row.QTYPACK = serow.QTYPACK;
                            Row.CZ_DatVyr_Track = serow.CZ_DatVyr_Track;
                            Row.CZ_DatVyr_Delka = serow.CZ_DatVyr_Delka;
                            Row.CZ_SerNum_Track = serow.CZ_SerNum_Track;
                            Row.CZ_SerNum_Delka = serow.CZ_SerNum_Delka;
                            Row.CZ_SW_Track = serow.CZ_SW_Track;
                            Row.CZ_SW_Delka = serow.CZ_SW_Delka;
                            Row.CZ_Doslo = serow.CZ_Doslo;
                            Row.Note = serow.Note;
                            Row.TYPEPAL = serow.TYPEPAL;
                            Row.QTYPAL = serow.QTYPAL;
                            Row.PRIORITY = serow.PRIORITY;
                            Row.PRINTED = serow.PRINTED;
                            Row.SKL_ID = serow.IsSKL_IDNull() ? string.Empty : serow.SKL_ID;
                            Row.USERID = serow.USERID;
                            Row.MJ = skzncrow.IsMJEANNull() ? string.Empty : skzncrow.MJEAN;
                            Row.CZ_REZ1_Track = serow.CZ_REZ1_Track;
                            Row.CZ_REZ2_Track = serow.CZ_REZ2_Track;
                            Row.ITEMCODE = serow.ITEMCODE;
                            Row.WEIGHT = serow.IsWEIGHTNull() ? 0 : serow.WEIGHT;

                            Row.CZ_Expirace_Track = serow.CZ_Expirace_Track;
                            Row.SetRealization_StartNull();
                            Row.SetRealization_StopNull();

                            vydej_ds_added.CZMST_SE.AddCZMST_SERow(Row);

                            //vydej_ds_added.CZMST_SE.AddCZMST_SERow(
                            //serow.CountEntries,
                            //serow.SOPNUMBE,
                            //serow.ITEMNMBR,
                            //serow.ITEMTYPE,
                            //serow.ITEMDESC,
                            //serow.VNDDOCNM,
                            //skzncrow.EAN,
                            //serow.ORD,
                            //serow.CZ_CarKod,
                            //serow.LOCNCODE, 
                            //serow.QTYSHPPD,
                            //serow.QTYPACK,
                            //serow.CZ_DatVyr_Track,
                            //serow.CZ_DatVyr_Delka,
                            //serow.CZ_SerNum_Track,
                            //serow.CZ_SerNum_Delka,
                            //serow.CZ_SW_Track,
                            //serow.CZ_SW_Delka,
                            //serow.CZ_Doslo,
                            //serow.Note,
                            //serow.TYPEPAL,
                            //serow.QTYPAL,
                            //serow.PRIORITY,
                            //serow.PRINTED,
                            //serow.IsSKL_IDNull() ? string.Empty : serow.SKL_ID,
                            //serow.USERID,
                            //skzncrow.IsMJEANNull() ? string.Empty : skzncrow.MJEAN,
                            //serow.CZ_REZ1_Track,
                            //serow.CZ_REZ2_Track,
                            //serow.ITEMCODE,
                            //serow.IsWEIGHTNull() ? 0 : serow.WEIGHT,
                            //);
                        }
                    }
                }

                //CZMST_SETableAdapter.Update(vydej_ds_added.CZMST_SE);
                Database.Vydej.Update_CZMST_SE(vydej_ds_added.CZMST_SE, conn, trans);

                trans.Commit();

                return "OK";
            }
            catch (XmlException e)
            {
                try
                {

                    if (trans != null)
                        trans.Rollback();
                }
                catch (System.Exception exx)
                {
                    Fask.Logging.ExceptionHandler2.Handle(exx);
                }

                Fask.Logging.ExceptionHandler2.Handle(e);
                return e.Message;
            }
            catch (Exception ex)
            {
                try
                {

                    if (trans != null)
                        trans.Rollback();
                }
                catch (System.Exception exx)
                {
                    Fask.Logging.ExceptionHandler2.Handle(exx);
                }

                Fask.Logging.ExceptionHandler2.Handle(ex);
                return ex.Message;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

		private static void SaveData_toDB(
            ref decimal quantity,
            decimal delivered, 
            ref bool incCountEntr, 
            SqlConnection conn, 
            SqlTransaction tran,
            Datasets.Vydej.CZMST_SERow seRow)
        {
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

            //Datasets.DatabasePohoda.SKzDataTable dt_skz = SKzTableAdapter.GetDataByID(int.Parse(seRow.ITEMNMBR));
			Datasets.DatabasePohoda.SKzDataTable dt_skz = Database.Pohoda.SKz_GetDataByID(int.Parse(seRow.ITEMNMBR));
            if (dt_skz.Count > 0)
            { // pro kazdou variantu MJ vlozit alternativni MJ ... 
                var item = dt_skz[0];
                //foreach (var item in dt_skz)
                {
                    // pro MJ 
                    //CZMST_SETableAdapter.Insert(
                    Database.Vydej.Insert_CZMST_SE(tran, conn,
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
                        quantity,
                        0,
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
                        item.MJ,
                        seRow.CZ_REZ1_Track,
                        seRow.CZ_REZ2_Track,
                        seRow.ITEMCODE,
                        seRow.IsWEIGHTNull() ? (decimal?)null : seRow.WEIGHT,
                        seRow.CZ_Expirace_Track,
                        (DateTime?)null,
                        (DateTime?)null
                    );

                    if (Globals_V1.Konfigurace.Vydej[0].MerneJednotky_DotahovatDalsiVarianty)
                    {
                        //pro MJ2
                        if (!item.IsMJ2Null() && !item.IsMJ2KoefNull())
                        {
                            //CZMST_SETableAdapter.Insert(
                            Database.Vydej.Insert_CZMST_SE(tran, conn,
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
                        quantity,
                        0,
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
                        item.MJ2Koef.ToString(),
                        seRow.CZ_REZ1_Track,
                        seRow.CZ_REZ2_Track,
                        seRow.ITEMCODE,
                        seRow.IsWEIGHTNull() ? (decimal?)null : seRow.WEIGHT,
                        seRow.CZ_Expirace_Track,
                        (DateTime?)null,
                        (DateTime?)null
                        );

                        }
                        //pro MJ3
                        if (!item.IsMJ3Null() && !item.IsMJ3KoefNull())
                        {
                            //CZMST_SETableAdapter.Insert(
                            Database.Vydej.Insert_CZMST_SE(tran, conn,
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
                        quantity,
                        0,
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
                        item.MJ3Koef.ToString(),
                        seRow.CZ_REZ1_Track,
                        seRow.CZ_REZ2_Track,
                        seRow.ITEMCODE,
                        seRow.IsWEIGHTNull() ? (decimal?)null : seRow.WEIGHT,
                        seRow.CZ_Expirace_Track,
                        (DateTime?)null,
                        (DateTime?)null
                        );
                        }
                    }
                }
            }
            else
            {
                //CZMST_SETableAdapter.Insert(
                Database.Vydej.Insert_CZMST_SE(tran, conn,
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
                    quantity,
                    0,
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
                    seRow.MJ,
                    seRow.CZ_REZ1_Track,
                    seRow.CZ_REZ2_Track,
                    seRow.ITEMCODE,
                    seRow.IsWEIGHTNull() ? (decimal?)null : seRow.WEIGHT,
                    seRow.CZ_Expirace_Track,
                    (DateTime?)null,
                    (DateTime?)null
                    );
            }

            incCountEntr = true;
        }

        internal static string LoadVydej_ImportFaktura_ResponseXML(string filename, int countEntries, string SKL_ID)
        {
            XML.MST_Pohoda.CheckExistResponseFile(filename);
            // \TODO !!!implementovat!!!
            // TaD udalat reakci na request
            //throw new NotImplementedException();
            return "OK";
        }


        /// <summary>
        /// Interni metoda pro vytvoreni xml requestu na pohodu pro vytvoreji faktury vydane
        /// </summary>
        /// <param name="file">nazev vystupniho souboru</param>
        /// <param name="countEntries">cislo davky vydejky, podle ktere ma byt request vytvoren</param>
        /// <param name="SKL_ID">id skladu</param>
        /// <param name="note">poznamka, ktera bude uvedena v poznamce doklau</param>
        /// <returns>True = uspesne vytvoreno, False = neuspech</returns>
        internal static bool CreateRequest_Import_Faktura_XML_NEW(string filename, int countEntries, string SKL_ID, string note)
        {
            // konstanta pro tostrin() cisel na invariantni format ...
            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;
            Datasets.Vydej.CZMST_SIDataTable dt_si = null;
            try
            {

                #region OLD 13.12.2024


                //if (Globals_V1.Konfigurace.Vydej[0].GrupujDataVydejka)
                //    dt_si = Database.Vydej.GETDATA_CZMSTSI_DS_GroupBy_CountEntries(countEntries);
                //else
                //    dt_si = Database.Vydej.GETDATA_CZMSTSI_By_CountEntries(countEntries);

                ////Datasets.DatabasePohodaTableAdapters.OBJpolTableAdapter taOBJPol = new Datasets.DatabasePohodaTableAdapters.OBJpolTableAdapter();
                ////taSKZ.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);
                ////taOBJPol.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
                ////DateTime? datumdokladu = null;

                ////Datasets.DatabasePohodaTableAdapters.OBJTableAdapter taOBJ = new Datasets.DatabasePohodaTableAdapters.OBJTableAdapter();
                ////taOBJ.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;


                //Datasets.DatabasePohoda.OBJDataTable OBJdt = null;
                //Datasets.DatabasePohoda.OBJRow OBJrow = null;

                ////DateTime date = DateTime.Now;

                ////var row_id;
                //Datasets.Vydej.CZMST_SIRow row_id = null;

                //int? cizimenaid = null;


                //if ((dt_si != null) && (dt_si.Count > 0))
                //{

                //    var data = dt_si.OrderBy(x => x.DEX_ROW_ID);
                //    row_id = data.First();

                //    //OBJdt = taOBJ.GetDataByCislo(dt_si[0].SOPNUMBE);
                //    OBJdt = Database.Pohoda.OBJ_GetDataByCislo(dt_si[0].SOPNUMBE);

                //    if ((OBJdt != null) && (OBJdt.Count > 0))
                //        OBJrow = OBJdt.First();


                //    //Cizi meny ... 
                //    try
                //    {
                //        if (!OBJrow.IsRefCMNull())
                //            cizimenaid = OBJrow.RefCM;
                //    }
                //    catch (Exception excm)
                //    {
                //        Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "CreateImportXML", excm);
                //    }
                //}
                //else
                //{
                //    throw new Exception("Nenalezena data k importu v tabulce SI");
                //} 
                #endregion


                #region NEW 13.12.2024
                // Inicializace proměnných
                Datasets.DatabasePohoda.OBJDataTable OBJdt = null;
                Datasets.DatabasePohoda.OBJRow OBJrow = null;
                Datasets.Vydej.CZMST_SIRow row_id = null;
                int? cizimenaid = null;

                try
                {
                    // Získání dat podle konfigurace
                    if (Globals_V1.Konfigurace.Vydej[0].GrupujDataVydejka)
                        dt_si = Database.Vydej.GETDATA_CZMSTSI_DS_GroupBy_CountEntries(countEntries);
                    else
                        dt_si = Database.Vydej.GETDATA_CZMSTSI_By_CountEntries(countEntries);

                    // Kontrola, zda data existují
                    if (dt_si != null && dt_si.Count > 0)
                    {
                        // Seřazení a získání prvního řádku
                        var data = dt_si.OrderBy(x => x.DEX_ROW_ID);
                        row_id = data.FirstOrDefault(); // Bezpečně získat první záznam
                        if (row_id == null)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "row_id je null. Data tabulky CZMST_SI nejsou validní.");
                            throw new Exception("row_id je null. Data tabulky CZMST_SI nejsou validní.");
                        }

                        // Kontrola a zpracování SOPNUMBE
                        if (!string.IsNullOrEmpty(dt_si[0].SOPNUMBE))
                        {
                            OBJdt = Database.Pohoda.OBJ_GetDataByCislo(dt_si[0].SOPNUMBE);

                            if (OBJdt != null && OBJdt.Count > 0)
                            {
                                OBJrow = OBJdt.FirstOrDefault();
                                if (OBJrow == null)
                                {
                                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "OBJrow je null, i když OBJdt obsahuje data.");
                                    throw new Exception("OBJrow je null.");
                                }
                            }
                            else
                            {
                                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "OBJdt je prázdný nebo null.");
                                throw new Exception("OBJdt je prázdný nebo null.");
                            }
                        }
                        else
                        {
                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Hodnota SOPNUMBE je null nebo prázdná.");
                            throw new Exception("SOPNUMBE je null nebo prázdná.");
                        }

                        // Zpracování cizí měny
                        if (OBJrow != null)
                        {
                            if (!OBJrow.IsRefCMNull())
                            {
                                cizimenaid = OBJrow.RefCM;
                            }
                            else
                            {
                                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "RefCM je null, cizí měna není nastavena.");
                            }
                        }
                        else
                        {
                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "OBJrow je null, nelze zpracovat cizí měnu.");
                            throw new Exception("OBJrow je null.");
                        }
                    }
                    else
                    {
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Nenalezena data k importu v tabulce SI.");
                        throw new Exception("Nenalezena data k importu v tabulce SI.");
                    }
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML", "CreateImportXML", ex);
                    throw new Exception("Chyba během zpracování dat: " + ex.Message, ex);
                }



                #endregion

                List<object> filterList = new List<object>();
                List<object> mainfilterList = new List<object>();

                XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
                XNamespace inv = "http://www.stormware.cz/schema/version_2/invoice.xsd";
                XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";

                #region Header

                List<XElement> listHeader = new List<XElement>();

                //text = "Test import MSTW-Pohoda";
                string text = OBJrow.IsSTextNull() ? null : OBJrow.SText.Trim();

                if (text != null)
                {
                    listHeader.Add(new XElement(inv + "text", text));
                }

                string poznamka = OBJrow.IsPoznNull() ? null : OBJrow.Pozn;
                string poznamka2 = OBJrow.IsPozn2Null() ? null : OBJrow.Pozn2;

                StringBuilder poznB = new StringBuilder();

                if (!String.IsNullOrEmpty(poznamka))
                    poznB.AppendLine(poznamka);
                if (!String.IsNullOrEmpty(note))
                    poznB.AppendLine(note);
                if (poznB.Length > 0)
                {
                    //tw.WriteLine("              <pri:note>" + poznB.ToString() + "</pri:note>");
                    listHeader.Add(new XElement(inv + "note", poznB.ToString()));
                }
           
                listHeader.Add(new XElement(inv + "intNote", (poznamka2 == null ? string.Empty : poznamka2 + "\n") + "nacteno z Serveru z dávky č.:" + countEntries.ToString()));
                listHeader.Add(new XElement(inv + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));


                if (!dt_si[0].IsVNDDOCNMNull()) { listHeader.Add(new XElement(inv + "numberOrder", dt_si[0].VNDDOCNM.Trim())); }
                else { listHeader.Add(new XElement(inv + "numberOrder", string.Empty)); }


                #region Partner
                if (true)
                {

                    XElement IDPartnerFakturacni;

                    if (OBJrow.IsRefADNull())
                    {
                        //pokud neni zavedeny ...
                        List<XElement> listPartnerFakturacni = new List<XElement>();
                        listPartnerFakturacni.Add(new XElement(typ + "company", OBJrow.IsFirmaNull() ? string.Empty : OBJrow.Firma));  // stringCompany je Řetězec o délce 255 znaků
                        listPartnerFakturacni.Add(new XElement(typ + "division", OBJrow.IsUtvarNull() ? string.Empty : OBJrow.Utvar)); //string32
                        listPartnerFakturacni.Add(new XElement(typ + "name", OBJrow.IsJmenoNull() ? string.Empty : OBJrow.Jmeno));     //string32
                        listPartnerFakturacni.Add(new XElement(typ + "city", OBJrow.IsObecNull() ? string.Empty : OBJrow.Obec));     //string45
                        listPartnerFakturacni.Add(new XElement(typ + "street", OBJrow.IsUliceNull() ? string.Empty : OBJrow.Ulice));   //string64
                        listPartnerFakturacni.Add(new XElement(typ + "zip", OBJrow.IsPSCNull() ? string.Empty : OBJrow.PSC));      //string15
                        listPartnerFakturacni.Add(new XElement(typ + "ico", OBJrow.IsICONull() ? string.Empty : OBJrow.ICO));      //icoType string o velkosti 15
                        listPartnerFakturacni.Add(new XElement(typ + "dic", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      //dictype string o velkosti 18

                        //listHeader.Add(new XElement(typ + "VATPayerType", OBJrow.vat IsDICNull() ? string.Empty : OBJrow.DIC));      

                        listPartnerFakturacni.Add(new XElement(typ + "idDph", OBJrow.IsICDPHNull() ? string.Empty : OBJrow.ICDPH));
                        //listHeader.Add(new XElement(typ + "country", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        //listHeader.Add(new XElement(typ + "phone", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        //listHeader.Add(new XElement(typ + "mobilPhone", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        listPartnerFakturacni.Add(new XElement(typ + "fax", OBJrow.IsFaxNull() ? string.Empty : OBJrow.Fax));
                        listPartnerFakturacni.Add(new XElement(typ + "email", OBJrow.IsEmailNull() ? string.Empty : OBJrow.Email));
                        //listHeader.Add(new XElement(typ + "linkToAddress", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));     



                        // Partner
                        //listHeader.Add(new XElement(vyd + "partnerIdentity",
                        //    new XElement(typ + "address", listPartner)));  

                        IDPartnerFakturacni = new XElement(typ + "address", listPartnerFakturacni);

                    }
                    else
                    {
                        IDPartnerFakturacni = new XElement(typ + "id", OBJrow.RefAD);
                    }

                    XElement IDPartnerDodaci = null;

                    if (!OBJrow.IsFirma2Null()
                        || !OBJrow.IsUtvar2Null()
                        || !OBJrow.IsJmeno2Null()
                        || !OBJrow.IsObec2Null()
                        || !OBJrow.IsUlice2Null()
                        || !OBJrow.IsPSC2Null()
                        || !OBJrow.IsEmail2Null())
                    {
                        List<XElement> listPartnerDodaci = new List<XElement>();
                        listPartnerDodaci.Add(new XElement(typ + "company", OBJrow.IsFirma2Null() ? string.Empty : OBJrow.Firma2));  // stringCompany je Řetězec o délce 255 znaků
                        listPartnerDodaci.Add(new XElement(typ + "division", OBJrow.IsUtvar2Null() ? string.Empty : OBJrow.Utvar2)); //string32
                        listPartnerDodaci.Add(new XElement(typ + "name", OBJrow.IsJmeno2Null() ? string.Empty : OBJrow.Jmeno2));     //string32
                        listPartnerDodaci.Add(new XElement(typ + "city", OBJrow.IsObec2Null() ? string.Empty : OBJrow.Obec2));     //string45
                        listPartnerDodaci.Add(new XElement(typ + "street", OBJrow.IsUlice2Null() ? string.Empty : OBJrow.Ulice2));   //string64
                        listPartnerDodaci.Add(new XElement(typ + "zip", OBJrow.IsPSC2Null() ? string.Empty : OBJrow.PSC2));      //string15
                        //listPartnerDodaci.Add(new XElement(typ + "ico", OBJrow.IsICONull() ? string.Empty : OBJrow.ICO));      //icoType string o velkosti 15
                        //listPartnerDodaci.Add(new XElement(typ + "dic", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      //dictype string o velkosti 18

                        //listHeader.Add(new XElement(typ + "VATPayerType", OBJrow.vat IsDICNull() ? string.Empty : OBJrow.DIC));      

                        //listPartnerDodaci.Add(new XElement(typ + "idDph", OBJrow.IsICDPHNull() ? string.Empty : OBJrow.ICDPH));
                        //listHeader.Add(new XElement(typ + "country", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        //listHeader.Add(new XElement(typ + "phone", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        //listHeader.Add(new XElement(typ + "mobilPhone", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));      
                        //listPartnerDodaci.Add(new XElement(typ + "fax", OBJrow.IsFaxNull() ? string.Empty : OBJrow.Fax));
                        listPartnerDodaci.Add(new XElement(typ + "email", OBJrow.IsEmail2Null() ? string.Empty : OBJrow.Email2));
                        //listHeader.Add(new XElement(typ + "linkToAddress", OBJrow.IsDICNull() ? string.Empty : OBJrow.DIC));     

                        IDPartnerDodaci = new XElement(typ + "shipToAddress", listPartnerDodaci);
                    }


                    if (IDPartnerDodaci != null)
                    {
                        listHeader.Add(new XElement(inv + "partnerIdentity",
                        IDPartnerFakturacni, IDPartnerDodaci));
                    }
                    else
                    {
                        listHeader.Add(new XElement(inv + "partnerIdentity",
                        IDPartnerFakturacni));
                    }
                }


                #endregion

                #region Středisko

                if (!OBJrow.IsRefStrNull())
                {
                    List<XElement> listStredisko = new List<XElement>();
                    listStredisko.Add(new XElement(typ + "id", OBJrow.RefStr));
                    listHeader.Add(new XElement(inv + "centre", listStredisko));
                }
                #endregion

                #region cinnost

                if (!OBJrow.IsRefCinNull())
                {
                    List<XElement> listcinnost = new List<XElement>();
                    listcinnost.Add(new XElement(typ + "id", OBJrow.RefCin));
                    listHeader.Add(new XElement(inv + "activity", listcinnost));
                }
                #endregion

                #region zakazka

                if (!OBJrow.IsCisloZAKNull())
                {
                    List<XElement> listzakazka = new List<XElement>();
                    listzakazka.Add(new XElement(typ + "ids", OBJrow.CisloZAK));
                    listHeader.Add(new XElement(inv + "contract", listzakazka));
                }
                #endregion

                #region Historicka sazba DPH pri registraci DPH v EU

                // 17.10.2019 JiS : oprava odesilani pri registraci dane v jinem clenskem state EU, 
                // pokud je zdrojova objednavka registorvana k dani v jinem clenskem state EU
                if (
                    ((!OBJrow.IsDICRegDPHEUNull()) && (OBJrow.DICRegDPHEU.Trim().Length > 0))
                    ||
                    ((!OBJrow.IsHistSzDPHNull()) && (OBJrow.HistSzDPH))
                    )
                {
                    List<XElement> listDPH = new List<XElement>();
                    // TaD 19.10.2018 Tady se ma dotahovat DIC s objedvavky, stloupec DICRegDPHEU, v serveru neni tento tloupec, pravdepodobne novy update Pohody
                    listDPH.Add(new XElement(typ + "ids", OBJrow.DICRegDPHEU));
                    listHeader.Add(new XElement(inv + "regVATinEU", listDPH));

                    List<XElement> listClassificationVATTYpe = new List<XElement>();
                    listClassificationVATTYpe.Add(new XElement(typ + "classificationVATType", "nonSubsume"));//Nezahrnovat do DPH.
                    listHeader.Add(new XElement(inv + "classificationVAT", listClassificationVATTYpe));
                }
                else
                {
                    List<XElement> listClassificationVATTYpe = new List<XElement>();
                    listClassificationVATTYpe.Add(new XElement(typ + "classificationVATType", "inland")); //Tuzemske plneni.
                    listHeader.Add(new XElement(inv + "classificationVAT", listClassificationVATTYpe));
                }

                #endregion

                // Typ faktury   issuedInvoice je Faktura
                listHeader.Add(new XElement(inv + "invoiceType", "issuedInvoice"));

                // Predkontace
                if (!OBJrow.IsRefADNull())
                {
                    // pokud je reference na Adresaš nenulova tak se dotahne Predkontace s adresaře
                    //listHeader.Add(new XElement(inv + "accounting", new XElement(typ + "ids","3Fv")));

                    //Datasets.DatabasePohodaTableAdapters.pPK_PredkontaceTableAdapter adta = new Datasets.DatabasePohodaTableAdapters.pPK_PredkontaceTableAdapter();
                    //adta.Connection = new System.Data.OleDb.OleDbConnection(Globals.ConnectionStringPohodaDB);

                    //var addt = adta.GetData(OBJrow.RefAD);
					var addt = Database.Pohoda.pPK_Predkontace_GetDataByID(OBJrow.RefAD);

                    if ((addt != null) & (addt.Count > 0))
                    {
                        string Predkontace = addt[0].IsIDSNull() ? string.Empty : addt[0].IDS.Trim() ;
                        listHeader.Add(new XElement(inv + "accounting", new XElement(typ + "ids", Predkontace)));
                    }

                }

                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();

                #region Zjisteni Vyrizeno

                string Preneseno = Vydej_KontrolaVuciPohodeVykriti(countEntries, dt_si);

                #endregion

                var dataOrdered = dt_si.OrderBy(x => x.ORD);

                foreach (Datasets.Vydej.CZMST_SIRow row in dataOrdered)
                {
                    XElement item = Get_Item_Faktura(inv,typ, nfi, row, Preneseno);
                    
                    if (item == null)
                        continue;

                    listItem.Add(item);


                    if (Globals_V1.Konfigurace.Vydej[0].Faktura_Importovat_SluzbyPodPolozky)
                    {
                        //1. Zavolat online funkci, ktere dam jak parametr
                        // ORD
                        //2. ta mi vratí tabulku všech nalezenych služeb
                        //3. tyto pomoci foreach radek po radku projdu  a vložím do XML requestu
                        //4. užívam si to že to snad funguje :D

                        Datasets.Vydej.SeznamSluzebDataTable tmp = Database.Vydej.GetData_ListSluzeb(row.ORD);

                        foreach (var radek in tmp)
                        {
                            XElement pol = Get_Item_Faktura_Sluzba(inv, typ, nfi, row, Preneseno, radek.ID_Polozky);

                            if (pol == null)
                                continue;

                            listItem.Add(pol);
                        }
                    }
                }
                #endregion 

                #region Summary

                List<XElement> vydejkaSummary = new List<XElement>();

                if (cizimenaid.HasValue)
                {
                    XElement currency = new XElement(typ + "currency", new XElement(typ + "id", cizimenaid));

                    if (!OBJrow.IsCmKursNull())
                    {
                        vydejkaSummary.Add(new XElement(inv + "foreignCurrency", currency, new XElement(typ + "rate", OBJrow.CmKurs.ToString(nfi))));
                    }
                    else
                    {
                        vydejkaSummary.Add(new XElement(inv + "foreignCurrency", currency));

                    }
                }


                #endregion


                XElement root = new XElement(dat + "dataPack",
                    new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                    new XAttribute(XNamespace.Xmlns + "inv", "http://www.stormware.cz/schema/version_2/invoice.xsd"),
                    new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                    new XAttribute("id", "INV" + row_id.DEX_ROW_ID.ToString()),
                    new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                    new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                    new XAttribute("version", "2.0"),
                    new XAttribute("note", "Import faktury"),

                        new XElement(dat + "dataPackItem",
                            new XAttribute("id", "INV" + row_id.DEX_ROW_ID.ToString()),
                            new XAttribute("version", "2.0"),

                                new XElement(inv + "invoice",
                                    new XAttribute("version", "2.0"),
                                    new XElement(inv + "invoiceHeader", listHeader),
                                    new XElement(inv + "invoiceDetail", listItem),
                                    new XElement(inv + "invoiceSummary", vydejkaSummary)
                                        )));

                root.Save(filename);

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                //Logging.Log.Write(ex.ToString());
                return false;
            }
            return true;
        }

        private static XElement Get_Item_Faktura_Sluzba(
            XNamespace inv,
            XNamespace typ,
            System.Globalization.NumberFormatInfo nfi,
            Datasets.Vydej.CZMST_SIRow row,
            string Preneseno,
            int ORD_Radku)
        {

            //< inv:invoiceItem >
            //< inv:link >
            //< typ:sourceAgenda > receivedOrder </ typ:sourceAgenda >
            //< typ:sourceItemId > 462987 </ typ:sourceItemId >           
            //</ inv:link >
            //< inv:quantity > 10 </ inv:quantity >     
            //</ inv:invoiceItem >

            List<XElement> invoiceItem = new List<XElement>();

            List<XElement> LinkItem = new List<XElement>();

            LinkItem.Add(new XElement(typ + "sourceAgenda", "receivedOrder"));
            LinkItem.Add(new XElement(typ + "sourceItemId", ORD_Radku.ToString()));

            LinkItem.Add(new XElement(typ + "settingsSourceDocumentOrderItem", new XElement(typ + "linkOrderItemToInvoice", Preneseno)));

            invoiceItem.Add(new XElement(inv + "link", LinkItem));

            invoiceItem.Add(new XElement(inv + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));

            return new XElement(inv + "invoiceItem", invoiceItem);
        }

        private static XElement Get_Item_Faktura(
            XNamespace inv, 
            XNamespace typ, 
            System.Globalization.NumberFormatInfo nfi, 
            Datasets.Vydej.CZMST_SIRow row, 
            string Preneseno)
        {
            string Cislo;
            int RefSKz;

            try
            {
                Cislo = row.SOPNUMBE; // Cislo v OBJ... zebere to pak z OBJ ID a join s OBJPol pomoci RefAg v OBJPol
                RefSKz = int.Parse(row.ITEMNMBR); // v OBJPol je RefSKz

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }

            Datasets.DatabasePohoda.OBJpolDataTable dtOBJPol = Database.Pohoda.OBJPol_GetDataBy_ID(row.ORD);
            Datasets.DatabasePohoda.OBJpolRow radek = null;

            if ((dtOBJPol != null) && (dtOBJPol.Count > 0))
                radek = dtOBJPol.First();

            List<XElement> invoiceItem = new List<XElement>();


            List<XElement> LinkItem = new List<XElement>();

            LinkItem.Add(new XElement(typ + "sourceAgenda", "receivedOrder"));
            LinkItem.Add(new XElement(typ + "sourceItemId", row.ORD.ToString()));

            LinkItem.Add(new XElement(typ + "settingsSourceDocumentOrderItem", new XElement(typ + "linkOrderItemToInvoice", Preneseno)));

            invoiceItem.Add(new XElement(inv + "link", LinkItem));

            invoiceItem.Add(new XElement(inv + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));

            invoiceItem.Add(new XElement(inv + "payVAT", false));

            // 8.10.2018 TaD Upraveno kvuli Cizy mene
            string dphSazba = Classes.Pohoda.Sazby.GetName(radek.RelSzDPH);
            invoiceItem.Add(new XElement(inv + "rateVAT", dphSazba));


            if ((radek != null) && (!radek.IsProcentoDPHNull()))
            {
                invoiceItem.Add(new XElement(inv + "percentVAT", radek.ProcentoDPH.ToString(nfi)));
            }

            //vydejkaItem.Add(new XElement(vyd + "note", row.SERLTNUM.Trim()));
            //vydejkaItem.Add(Cena);

            #region Uprava Ceny

            if ((radek != null) && (!radek.IsKcJednNull()))
            {
                //tw.WriteLine("          <pri:homeCurrency><typ:unitPrice>" + radek.KcJedn.ToString(nfi) + "</typ:unitPrice></pri:homeCurrency>");
                invoiceItem.Add(new XElement(inv + "homeCurrency", new XElement(typ + "unitPrice", radek.KcJedn.ToString(nfi))));
            }

            if ((radek != null) && (!radek.IsCmJednNull()))
            {
                //tw.WriteLine("          <pri:foreignCurrency><typ:unitPrice>" + radek.CmJedn.ToString(nfi) + "</typ:unitPrice></pri:foreignCurrency>");
                invoiceItem.Add(new XElement(inv + "foreignCurrency", new XElement(typ + "unitPrice", radek.CmJedn.ToString(nfi))));
            }


            #endregion



            if (row.SERLTNUM.Trim().Length > 0)
                invoiceItem.Add(new XElement(inv + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
            else
                invoiceItem.Add(new XElement(inv + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));


            if (!row.IsExpiraceNull())
            {
                invoiceItem.Add(new XElement(inv + "expirationDate", XmlConvert.ToString(row.Expirace, "yyyy-MM-dd")));
            }

            #region AttributeToSN

            var ele = Vypln_AttributeToSN(inv, typ, row);

            if (ele != null)
                invoiceItem.Add(ele);

            #endregion


            return new XElement(inv + "invoiceItem", invoiceItem);
        }

        static private Datasets.Vydej.CZMST_SEDataTable GetSEbySOPNUMBEfromOBJ(string SOPNUMBE) 
        {

			Datasets.Vydej.CZMST_SEDataTable seTable = new Datasets.Vydej.CZMST_SEDataTable();

            try
            {
                Globals_V1.LoadConfiguration();

				Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
				if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
					ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				}

                string pom = string.Empty;
				int ITEMDESC_MaxLength = 0;
				int MJ_MaxLength = 0;
				try
				{
					ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
					MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
				}
				catch (System.Exception ex)
				{
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error,"Vydej Columns MaxLen...");
					Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Vydej", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
					throw ex;
				}

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return null;

                // 1) nacist hlavicku faktury
                // 2) nacist polozky faktury
                // 3) nacist doplnujici informace k polozkam
                // 4) ulozit do predlohy vydeje
                // 5) verifikace
                // - konec - 
                // pri chybe vratit informaci o chybe ...

                Datasets.DatabasePohoda ds_pohoda = new Datasets.DatabasePohoda();

                //Datasets.DatabasePohodaTableAdapters.OBJCisloTableAdapter ta_pohoda_OBJCislo = new Datasets.DatabasePohodaTableAdapters.OBJCisloTableAdapter();

                //ta_pohoda_OBJCislo.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

                // nacteni faktury a polozek faktury
                //ta_pohoda_OBJCislo.Fill(ds_pohoda.OBJCislo, SOPNUMBE.Trim());
				Database.Pohoda.OBJCislo_Fill(ds_pohoda.OBJCislo, SOPNUMBE.Trim());


                // dotazeni parametru pro locncode (vychozi lokaci ..)
				//Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
				//SKzParametryTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

                // *************************************************
                // naplneni do czmst_se ... 
                // *************************************************
                //Datasets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
                //CZMST_SETableAdapter.Connection.ConnectionString = Globals.ConnectionString;

           
                Datasets.Vydej.CZMST_SERow seRow = null;

                int cisloDavky = 0;

                foreach (var invoiceItem in ds_pohoda.OBJCislo)
                {
                    // Pokud to neni skladova polozka, tak ji neresit ...
					// \TODO : otestovat, zda to takto je ... => tedy test na NULL hodnotu...
                    if ((invoiceItem == null) || (invoiceItem.IsSKz_IDNull()))
                        continue;


                    if (Globals_V1.Konfigurace.Vydej[0].Predloha_Generovat_PodleTypuPolozky)
                    {
                        if (!invoiceItem.IsSKz_RelSkTypNull())
                        {
                            //item.RelSkTyp
                            string[] stringArray = Globals_V1.Konfigurace.Vydej[0].Predloha_Generovat_PodleTypuPolozky_Seznam.Split(',');
                            var listINT = stringArray.Select(x => Int32.Parse(x)).ToList();

                            if (!listINT.Contains(invoiceItem.SKz_RelSkTyp))
                            {
                                continue;
                            }
                        }
                    }


                    // dotazeni vychozi lokace pro zasobu
                    string locncodeDeafult = string.Empty;
                    if (invoiceItem != null)
                    {
                        if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                        {
                            //Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, invoiceItem.SKz_ID);
							Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, invoiceItem.SKz_ID);
							Datasets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                            if (skzParametry_row != null)
                            {
                                locncodeDeafult = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                            }
                        }
                    }

                    seRow = seTable.NewCZMST_SERow();

                    seRow.ITEMDESC = invoiceItem.SKz_Nazev; // nazev polozky => z xml <inv:text>
                    if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
                    {
						seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
                    }

                    seRow.ITEMNMBR = invoiceItem.SKz_ID.ToString(); // cislo polozky => z xml <inv:stockItem><typ:stockItem><typ:id>
                    seRow.ITEMTYPE = ""; //bez typu
                    //seRow.ITEMCODE = ""; // ? ITEMCODE neni???
					seRow.CountEntries = cisloDavky; // \TODO : ??? nove cislo davky ... 

                    seRow.CZ_CarKod = invoiceItem.IsSKz_IDSNull() ? string.Empty : invoiceItem.SKz_IDS.Trim();
                    seRow.CZ_DatVyr_Delka = 0; //?
                    seRow.CZ_DatVyr_Track = 0; //?
                    seRow.CZ_Doslo = 0; // pripraveno pro zpracovani
                    seRow.CZ_SerNum_Delka = 0; //? delka serioveho cisla 

					//if (Properties.Settings.Default.pohodaXfaskPouzitVPrFXTS)
					//{
					//    if (invoiceItem.IsSKz_VPrFXTSNull())
					//    {
					//        seRow.CZ_SerNum_Track = 0;
					//    }
					//    else
					//    {
					//        if (invoiceItem.SKz_VPrFXTS)
					//        {
					//            seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((invoiceItem == null) || (invoiceItem.IsSKz_RefVPrFXTSNull()) ? (int?)null : ((int?)invoiceItem.SKz_RefVPrFXTS - 1), invoiceItem.SKz_VPrFXTS);
					//        }
					//        else
					//        {
					//            seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((invoiceItem == null) || (invoiceItem.IsSKz_RefVPrFVTSNull()) ? (int?)null : ((int?)invoiceItem.SKz_RefVPrFVTS - 1), invoiceItem.SKz_VPrFVTS);
					//        }
					//    }
					//}
					//else
					//{
					//    seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((invoiceItem == null) || (invoiceItem.IsSKz_RelSKzVCNull()) ? (int?)null : (int?)invoiceItem.SKz_RelSKzVC,true);
					//}

					if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
					{
						seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(invoiceItem.IsSKz_RelSKzVCNull() ? (int?)null : (int?)invoiceItem.SKz_RelSKzVC, invoiceItem, Classes.Pohoda.TypAgendy.Vydej);
					}
					else
					{
						Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
						var dt_param = ParamTA.GetDataByITEMNMBR(seRow.ITEMNMBR.Trim());

						if ((dt_param != null) && (dt_param.Count > 0))
						{
							dt_row_param = dt_param.First();
						}

						seRow.CZ_SerNum_Track = Classes.Pohoda.GetPriznakSledovani(invoiceItem.IsSKz_RelSKzVCNull() ? (int?)null : (int?)invoiceItem.SKz_RelSKzVC, dt_row_param, Classes.Pohoda.TypAgendy.Vydej);
					}

                    seRow.CZ_SW_Delka = 0; //?
                    seRow.CZ_SW_Track = 0; //?


                    seRow.CZ_REZ1_Track = 0; //?
                    seRow.CZ_REZ2_Track = 0; //?

                    seRow.LOCNCODE = locncodeDeafult;
                    seRow.Note = " "; //invoiceItem.IsSKz_STextNull() ? " " : invoiceItem.SKz_SText;  //""; //? poznamka 
                    seRow.ORD = invoiceItem.OBJpol_ID; //int.Parse(invoiceItem.Element(inv + "id").Value.Trim()); //? poradi polozky na dokladu - pro pohoda id zaznamu v tabulce polozek <inv:id>
                    seRow.PRINTED = 0;
                    seRow.PRIORITY = 3; //? priorita ... 
                    seRow.QTYPACK = 1;//invoiceItem.IsOBJpol_MJKoefNull() ? 0 : Convert.ToDecimal(invoiceItem.OBJpol_MJKoef); // TODO : rozpad na varianty baleni dle car kodu ... 
                    seRow.QTYPAL = 0; //? palety neresime ... ???
                    seRow.QTYSHPPD = Convert.ToDecimal(invoiceItem.OBJ_Mnozstvi); //decimal.Parse(invoiceItem.Element(inv + "quantity").Value.Trim(), System.Globalization.NumberFormatInfo.InvariantInfo); // mnozstvi k vydeji => <inv:quantity>
                    seRow.SKL_ID = invoiceItem.SKz_RefSklad.ToString(); //stockItem.Element(typ + "store").Element(typ + "id").Value.Trim(); //? id skladu => <inv:stockItem><typ:store><typ:id> : jde o identifikator skladu
                    seRow.SOPNUMBE = SOPNUMBE.Trim();
                    seRow.TYPEPAL = ""; //? neresime palety ...
                    seRow.USERID = 0;
                    seRow.VNDDOCNM = ""; //? co by tady melo byt >>>
                    seRow.VNDITNUM = invoiceItem.IsSKz_EANNull() ? string.Empty : invoiceItem.SKz_EAN.Trim(); //typEAN == null ? string.Empty : typEAN.Value; //carovy kod zbozi ... => dotahnout z xml 
                    if (invoiceItem != null && !invoiceItem.IsOBJPol_MJNull())
                    {
                        seRow.MJ = invoiceItem.OBJPol_MJ;
                        if (seRow.MJ.Length > MJ_MaxLength)
                        {
							seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
                        }
                    }
                    else
                    {
                        seRow.MJ = string.Empty;
                    }

                    seRow.ITEMCODE = invoiceItem.IsSKz_IDSNull() ? string.Empty : invoiceItem.SKz_IDS.Trim();

                    seRow.SetWEIGHTNull();

                    seRow.CZ_Expirace_Track = 0;
                    seRow.SetRealization_StartNull();
                    seRow.SetRealization_StopNull();


                    // Nacist vychozi lokaci ...
                    // az tady, protoze musi mit nastaveny skl_id a itemnmbr...
                    Vydej.LocncodeFindAlgorithmVychozi(seRow);

                    //vlozit do se ...
                    seTable.AddCZMST_SERow(seRow);

                    
                    seRow = null;
                }

                return seTable;

				#region OLD
                //try
                //{
                //    // ulozit do se
                //    //CZMST_SETableAdapter.Connection.Open();
                //    //CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                //    //// prideleni cisla davky v transakci ..
                //    //cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries(CZMST_SETableAdapter.Connection, CZMST_SETableAdapter.Transaction);
                //    //cisloDavky += 1;
                //    //foreach (var item in seTable)
                //    //{
                //    //    item.CountEntries = cisloDavky;

                //    //    // porad je pouze pridana, nikoli zmenena po zmene countentries...
                //    //    item.AcceptChanges();
                //    //    item.SetAdded();
                //    //}
                //    //int updatedRows = CZMST_SETableAdapter.Update(seTable);
                //    //CZMST_SETableAdapter.Transaction.Commit();
                //    //objednavka.CisloDavky = cisloDavky.ToString();
                //}
                //catch //(Exception e)
                //{
                //    //Log.writeErrorLog("ExportVydejkaPohoda_Z_Faktury_DirectTSQL: " + e.Message);
                //    //try
                //    //{
                //    //    CZMST_SETableAdapter.Transaction.Rollback();
                //    //}
                //    //catch { }
                //    throw new Exception("Uložení načtených položek se nezdařilo!");
                //}
                //finally
                //{
                //    //if ((CZMST_SETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                //    //    CZMST_SETableAdapter.Connection.Close();
                //}

				//return ; 
				#endregion
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error,"(Vydejka: " + SOPNUMBE.Trim() + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);
				Fask.Logging.ExceptionHandler2.Handle(seTable);
                throw ex;
                //return ex.Message;
            }



        }


        #region Prevodka

        /// <summary>
        /// \TODO Predelat na lepsi reakci
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="uzivatel"></param>
        /// <returns></returns>
        internal static string LoadResponse_Pre_XML(string filename)
        {
            XML.MST_Pohoda.CheckExistResponseFile(filename);

            return "OK";
        }

        /// <summary>
        /// Metoda pro Vytvořeni requestu Prevodky z volneho pohybu
        /// </summary>
        /// <param name="filename">Nazev souboru</param>
        /// <param name="p">poznamka</param>
        /// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
        /// <returns>Zda je nalezeno nebo ne</returns>
        internal static bool CreateRequest_Pre_XML(string filename, string p, Datasets.Vydej.CZMST_SIDataTable SI_dt, Doklad typDoklad, string SKL_ID_Cil, string text)
        {
            
            //string SKL_ID = SI_dt[0].SKL_ID;
            string objednavka = SI_dt[0].SOPNUMBE;
            int dex = SI_dt[0].DEX_ROW_ID;

            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
            XNamespace pre = "http://www.stormware.cz/schema/version_2/prevodka.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
            XNamespace prn = "http://www.stormware.cz/schema/version_2/print.xsd";

            try
            {
                #region Hlavicka

                List<XElement> listHeader = new List<XElement>();

                listHeader.Add(new XElement(pre + "text", text));

                listHeader.Add(new XElement(pre + "note", objednavka));

                listHeader.Add(new XElement(pre + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));
                listHeader.Add(new XElement(pre + "time", XmlConvert.ToString(DateTime.Now, "HH:mm:ss")));
                listHeader.Add(new XElement(pre + "store", new XElement(typ + "id", SKL_ID_Cil.Trim())));


                if ((typDoklad != null) && (typDoklad.Vyber_Typ_Prevodka != null))
                {
                    switch (typDoklad.Vyber_Typ_Prevodka)
                    {
                        case 0:
                            {
                                // Tato varianta nastaví datum vydejky aj prijemky totožný
                                //listHeader.Add(new XElement(pre + "dateOfReceipt", string.Empty));
                                //listHeader.Add(new XElement(pre + "timeOfReceipt", string.Empty));
                                break;
                            }
                        case 1:
                            {
                                //Tato varianta
                                listHeader.Add(new XElement(pre + "dateOfReceipt", string.Empty));
                                listHeader.Add(new XElement(pre + "timeOfReceipt", string.Empty));
                                break;
                            }
                        case 2:
                            {
                                // Tato varianta nastaví konkretný čas a datum prijemky
                                // odkud vzit?? 
                                listHeader.Add(new XElement(pre + "dateOfReceipt", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));
                                listHeader.Add(new XElement(pre + "timeOfReceipt", XmlConvert.ToString(DateTime.Now, "HH:mm:ss")));
                                break;
                            }
                        default:
                            break;
                    }

                }

                if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Stredisko0))
                {
                    listHeader.Add(new XElement(pre + "centreSource", new XElement(typ + "id", typDoklad.Stredisko0.Trim())));
                }


                Fask.Interfaces.DataSets.Strediska.CZMST091Row RowStredisko = Database.Prodej2.GETDATA_CZMST091(SKL_ID_Cil);

                if (RowStredisko != null)
                {

                    if (!string.IsNullOrEmpty(RowStredisko.odb_id))
                    {
                        XElement IDPartnerFakturacni;

                        IDPartnerFakturacni = new XElement(typ + "id", RowStredisko.odb_id.Trim());

                        listHeader.Add(new XElement(pre + "partnerIdentity", IDPartnerFakturacni));
                    }

                    //centreSource // Zdrojove stredisko
                    //centreDestination // cilove stredisko

                    if (!string.IsNullOrEmpty(RowStredisko.str_id))
                    {
                        listHeader.Add(new XElement(pre + "centreDestination", new XElement(typ + "id", RowStredisko.str_id.Trim())));
                    }
                }


                #endregion

                #region Itemy

                List<XElement> listItem = new List<XElement>();

                foreach (var row in SI_dt)
                {
                    List<XElement> PrevodkaItem = new List<XElement>();

                    PrevodkaItem.Add(new XElement(pre + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));

                    if (row.SERLTNUM.Trim().Length > 0)
                        PrevodkaItem.Add(new XElement(pre + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
                    else
                        PrevodkaItem.Add(new XElement(pre + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

                    #region AttributeToSN

                    var ele = Vypln_AttributeToSN(pre, typ, row);

                    if (ele != null)
                        PrevodkaItem.Add(ele);

                    #endregion

                    XElement polozka = new XElement(pre + "prevodkaItem", PrevodkaItem);
                    listItem.Add(polozka);
                }

                #endregion


                XElement PrevodkaElement = null;

                if (typDoklad.Prodej_Prevodka_Tisk_ID_sablona.HasValue && !string.IsNullOrEmpty(typDoklad.Prodej_Prevodka_Tisk_Tiskarna))
                {


                    List<XElement> print = new List<XElement>();
                    List<XElement> printSettings = new List<XElement>();

                    //Jedná se o ID šablony, pokud neni vyplneno tak je to chyba
                    printSettings.Add(new XElement(prn + "report", new XElement(prn + "id", typDoklad.Prodej_Prevodka_Tisk_ID_sablona.Value.ToString())));

                    //pocet vytisku projistotu je napevno jeden...
                    printSettings.Add(new XElement(prn + "parameters", new XElement(prn + "copy", 1)));

                    //dotahuje se vždy, pokud je vyplnena tak se vytiskne na nu, pokud je to praydne tak se tiskne na defaultní
                    printSettings.Add(new XElement(prn + "printer", typDoklad.Prodej_Prevodka_Tisk_Tiskarna.Trim()));

                    print.Add(new XElement(prn + "printerSettings", printSettings));


                    PrevodkaElement = new XElement(pre + "prevodka",
                            new XAttribute("version", "2.0"),
                            new XElement(pre + "prevodkaHeader", listHeader),
                            new XElement(pre + "prevodkaDetail", listItem),
                            new XElement(pre + "print", print)
                            );
                }
                else
                {
                    PrevodkaElement = new XElement(pre + "prevodka",
                            new XAttribute("version", "2.0"),
                            new XElement(pre + "prevodkaHeader", listHeader),
                            new XElement(pre + "prevodkaDetail", listItem)
                            );
                }

                XElement root = new XElement(dat + "dataPack",
                        new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                        new XAttribute(XNamespace.Xmlns + "pre", "http://www.stormware.cz/schema/version_2/prevodka.xsd"),
                        new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                        new XAttribute(XNamespace.Xmlns + "prn", "http://www.stormware.cz/schema/version_2/print.xsd"),
                        new XAttribute("id", "PRE_" + dex.ToString()),
                        new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                        new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                        new XAttribute("version", "2.0"),
                        new XAttribute("note", "Import prevodky z TO požadavku"),
                    new XElement(dat + "dataPackItem",
                        new XAttribute("id", "PRE_" + dex.ToString()),
                        new XAttribute("version", "2.0"),
                    PrevodkaElement
                    ));


                root.Save(filename);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }

            return true;

        }



        #endregion

        #region Prevodka z OBJ

        /// <summary>
        /// \TODO Predelat na lepsi reakci
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="uzivatel"></param>
        /// <returns></returns>
        internal static string LoadResponse_Pre_zOBJ_XML(string filename)
        {
            XML.MST_Pohoda.CheckExistResponseFile(filename);

            XML.Classes.Response2 response = new XML.Classes.Response2(filename);

            if (response != null)
            {
                if (response.Status == "OK")
                {
                    var doc = response.DocumentNumber;

                    if(!string.IsNullOrEmpty(doc))
                    {
                        Classes.Pohoda.Update_AttributeToSN_Prevod(doc);
                    }
                    else
                    {
                        throw new Exception("Dokument nenalezen");
                    }
                }
                else
                {
                    return response.Status;
                }
                
            }
            else
            {
                throw new Exception("Response nerozparsovan");
            }


            return "OK";
        }

        /// <summary>
        /// Metoda pro Vytvořeni requestu Prevodky z volneho pohybu
        /// </summary>
        /// <param name="filename">Nazev souboru</param>
        /// <param name="p">poznamka</param>
        /// <param name="cZMST_DIDataTable"> Data z CZMST_DI</param>
        /// <returns>Zda je nalezeno nebo ne</returns>
        internal static bool CreateRequest_Pre_zOBJ_XML(string filename, string p, Datasets.Vydej.CZMST_SIDataTable SI_dt, Doklad typDoklad, string SKL_ID_Cil, string text)
        {

            //string SKL_ID = SI_dt[0].SKL_ID;
            string objednavka = SI_dt[0].SOPNUMBE;
            int dex = SI_dt[0].DEX_ROW_ID;
            int CountEntries = SI_dt[0].CountEntries;

            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            XNamespace dat = "http://www.stormware.cz/schema/version_2/data.xsd";
            XNamespace pre = "http://www.stormware.cz/schema/version_2/prevodka.xsd";
            XNamespace typ = "http://www.stormware.cz/schema/version_2/type.xsd";
            XNamespace prn = "http://www.stormware.cz/schema/version_2/print.xsd";

            try
            {
                #region Hlavicka

                List<XElement> listHeader = new List<XElement>();

                listHeader.Add(new XElement(pre + "text", text));

                listHeader.Add(new XElement(pre + "note", objednavka));

                listHeader.Add(new XElement(pre + "date", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));
                listHeader.Add(new XElement(pre + "time", XmlConvert.ToString(DateTime.Now, "HH:mm:ss")));
                listHeader.Add(new XElement(pre + "store", new XElement(typ + "id", SKL_ID_Cil.Trim())));


                if ((typDoklad != null) && (typDoklad.Vyber_Typ_Prevodka != null))
                {
                    switch (typDoklad.Vyber_Typ_Prevodka)
                    {
                        case 0:
                            {
                                // Tato varianta nastaví datum vydejky aj prijemky totožný
                                //listHeader.Add(new XElement(pre + "dateOfReceipt", string.Empty));
                                //listHeader.Add(new XElement(pre + "timeOfReceipt", string.Empty));
                                break;
                            }
                        case 1:
                            {
                                //Tato varianta
                                listHeader.Add(new XElement(pre + "dateOfReceipt", string.Empty));
                                listHeader.Add(new XElement(pre + "timeOfReceipt", string.Empty));
                                break;
                            }
                        case 2:
                            {
                                // Tato varianta nastaví konkretný čas a datum prijemky
                                // odkud vzit?? 
                                listHeader.Add(new XElement(pre + "dateOfReceipt", XmlConvert.ToString(DateTime.Now, "yyyy-MM-dd")));
                                listHeader.Add(new XElement(pre + "timeOfReceipt", XmlConvert.ToString(DateTime.Now, "HH:mm:ss")));
                                break;
                            }
                        default:
                            break;
                    }

                }

                if ((typDoklad != null) && !string.IsNullOrEmpty(typDoklad.Stredisko0))
                {
                    listHeader.Add(new XElement(pre + "centreSource", new XElement(typ + "id", typDoklad.Stredisko0.Trim())));
                }

                Fask.Interfaces.DataSets.Strediska.CZMST091Row RowStredisko = Database.Prodej2.GETDATA_CZMST091(SKL_ID_Cil);

                if (RowStredisko != null)
                {

                    if (!string.IsNullOrEmpty(RowStredisko.odb_id))
                    {
                        XElement IDPartnerFakturacni;

                        IDPartnerFakturacni = new XElement(typ + "id", RowStredisko.odb_id.Trim());

                        listHeader.Add(new XElement(pre + "partnerIdentity", IDPartnerFakturacni));
                    }

                    //centreSource // Zdrojove stredisko
                    //centreDestination // cilove stredisko

                    if (!string.IsNullOrEmpty(RowStredisko.str_id))
                    {
                        listHeader.Add(new XElement(pre + "centreDestination", new XElement(typ + "id", RowStredisko.str_id.Trim())));
                    }
                }


                #endregion

                #region Itemy

                string Preneseno = Vydej_KontrolaVuciPohodeVykriti(CountEntries, SI_dt);

                List<XElement> listItem = new List<XElement>();

                foreach (var row in SI_dt)
                {
                    List<XElement> PrevodkaItem = new List<XElement>();

                    List<XElement> LinkItem = new List<XElement>();

                    LinkItem.Add(new XElement(typ + "sourceAgenda", "receivedOrder"));
                    LinkItem.Add(new XElement(typ + "sourceItemId", row.ORD.ToString()));

                    LinkItem.Add(new XElement(typ + "settingsSourceDocumentOrderItem", new XElement(typ + "linkOrderItemToInvoice", Preneseno)));

                    PrevodkaItem.Add(new XElement(pre + "link", LinkItem));

                    PrevodkaItem.Add(new XElement(pre + "quantity", ((float)row.QTYSHPPD).ToString(nfi)));

                    if (row.SERLTNUM.Trim().Length > 0)
                        PrevodkaItem.Add(new XElement(pre + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim())), new XElement(typ + "serialNumber", row.SERLTNUM.Trim())));
                    else
                        PrevodkaItem.Add(new XElement(pre + "stockItem", new XElement(typ + "stockItem", new XElement(typ + "id", row.ITEMNMBR.Trim()))));

                    #region AttributeToSN

                    var ele = Vypln_AttributeToSN(pre, typ, row);

                    if (ele != null)
                        PrevodkaItem.Add(ele);

                    #endregion

                    XElement polozka = new XElement(pre + "prevodkaItem", PrevodkaItem);
                    listItem.Add(polozka);
                }

                #endregion


                XElement PrevodkaElement = null;

                if (typDoklad.Prodej_Prevodka_Tisk_ID_sablona.HasValue && !string.IsNullOrEmpty(typDoklad.Prodej_Prevodka_Tisk_Tiskarna))
                {


                    List<XElement> print = new List<XElement>();
                    List<XElement> printSettings = new List<XElement>();

                    //Jedná se o ID šablony, pokud neni vyplneno tak je to chyba
                    printSettings.Add(new XElement(prn + "report", new XElement(prn + "id", typDoklad.Prodej_Prevodka_Tisk_ID_sablona.Value.ToString())));

                    //pocet vytisku projistotu je napevno jeden...
                    printSettings.Add(new XElement(prn + "parameters", new XElement(prn + "copy", 1)));

                    //dotahuje se vždy, pokud je vyplnena tak se vytiskne na nu, pokud je to praydne tak se tiskne na defaultní
                    printSettings.Add(new XElement(prn + "printer", typDoklad.Prodej_Prevodka_Tisk_Tiskarna.Trim()));

                    print.Add(new XElement(prn + "printerSettings", printSettings));


                    PrevodkaElement = new XElement(pre + "prevodka",
                            new XAttribute("version", "2.0"),
                            new XElement(pre + "prevodkaHeader", listHeader),
                            new XElement(pre + "prevodkaDetail", listItem),
                            new XElement(pre + "print", print)
                            );
                }
                else
                {
                    PrevodkaElement = new XElement(pre + "prevodka",
                            new XAttribute("version", "2.0"),
                            new XElement(pre + "prevodkaHeader", listHeader),
                            new XElement(pre + "prevodkaDetail", listItem)
                            );
                }

                XElement root = new XElement(dat + "dataPack",
                        new XAttribute(XNamespace.Xmlns + "dat", "http://www.stormware.cz/schema/version_2/data.xsd"),
                        new XAttribute(XNamespace.Xmlns + "pre", "http://www.stormware.cz/schema/version_2/prevodka.xsd"),
                        new XAttribute(XNamespace.Xmlns + "typ", "http://www.stormware.cz/schema/version_2/type.xsd"),
                        new XAttribute(XNamespace.Xmlns + "prn", "http://www.stormware.cz/schema/version_2/print.xsd"),
                        new XAttribute("id", "PRE_" + dex.ToString()),
                        new XAttribute("ico", Globals_V1.Konfigurace.PohodaInfo[0].ICO),
                        new XAttribute("application", Fask.SQL.Constants.Common.application_S),
                        new XAttribute("version", "2.0"),
                        new XAttribute("note", "Import prevodky z TO požadavku"),
                    new XElement(dat + "dataPackItem",
                        new XAttribute("id", "PRE_" + dex.ToString()),
                        new XAttribute("version", "2.0"),
                    PrevodkaElement
                    ));


                root.Save(filename);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Classes.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }

            return true;

        }



        #endregion

        #region Pomocne metody

        private static XElement Vypln_AttributeToSN(XNamespace XName, XNamespace typ, Datasets.Vydej.CZMST_SIRow row)
        {
            XElement ele = null;
            List<XElement> listVPrParams = new List<XElement>();

            var attr = Database.Pohoda.SKzVC_AttributeToSN(row.ITEMNMBR, row.SERLTNUM);

            if (attr.Count == 1)
            {
                var radek = attr.First();

                if (!radek.IsVPrSarzeKSNNull() && !string.IsNullOrEmpty(radek.VPrSarzeKSN))
                {
                   

                    List<XElement> listVPr = new List<XElement>();
                    var name = new XElement(typ + "name", "VPrSarzeKSN");
                    var textValue = new XElement(typ + "textValue", radek.VPrSarzeKSN);
                    listVPr.Add(name);
                    listVPr.Add(textValue);

                    listVPrParams.Add(new XElement(typ + "parameter", listVPr));
                }


                if (!radek.IsVPrExspiraceKSNNull())
                {
                    List<XElement> listVPrEXP = new List<XElement>();
                    var nameEXP = new XElement(typ + "name", "VPrExspiraceKSN");
                    var textValueEXP = new XElement(typ + "datetimeValue", XmlConvert.ToString(radek.VPrExspiraceKSN, "yyyy-MM-dd"));
                    listVPrEXP.Add(nameEXP);
                    listVPrEXP.Add(textValueEXP);

                    listVPrParams.Add(new XElement(typ + "parameter", listVPrEXP));

                }

                ele = new XElement(XName + "parameters", listVPrParams);

            }

            return ele;
        }

        private static string Vydej_KontrolaVuciPohodeVykriti(int countEntries, Datasets.Vydej.CZMST_SIDataTable dt_si)
        {
            string Preneseno = "4";
            List<int> preneseno = new List<int>();

            // 0) dotahnout se data dle countentries
            // => Database.Vydej.czmst_se_select(countentries);
            // 1 ) dotahnout do si data z se, ktera nejsou v si dle ORD
            //  => insert(add) novy radek s qtyshppd = 0 => tyto radky jsou ROW.INSERTED ... 
            // 2) projit algoritmus vyrizeno ... 
            // 3) a pocycklu provst
            // dt_si.RejectChanges();
            Datasets.Vydej.CZMST_SEDataTable dt_se = new Datasets.Vydej.CZMST_SEDataTable();

            var dt_seOnly = Database.Vydej.GETDATA_CZMSTSE_By_CountEntries(countEntries); // puvodni verze 

            #region 19.12.2018 TaD uprava kontrola vuči pohode

            var grupSE = dt_seOnly.GroupBy(x => x.SOPNUMBE.Trim());
            List<string> SOPNUMBEList = new List<string>();

            foreach (var item in grupSE)
            {
                if (!string.IsNullOrEmpty(item.Key))
                    SOPNUMBEList.Add(item.Key);

            }

            foreach (string item in SOPNUMBEList)
            {
                var dt_setmp = GetSEbySOPNUMBEfromOBJ(item);

                foreach (var row in dt_setmp)
                {
                    dt_se.ImportRow(row);
                }
            }

            #endregion



            System.Diagnostics.Debug.Assert(true, "dt_si.count=" + dt_si.Count.ToString());

            var senotinsi = dt_se.Where(x => !dt_si.Any(y => y.ORD == x.ORD));
            foreach (var se in senotinsi)
            {
                var Row = dt_si.NewCZMST_SIRow();

                Row.CountEntries = se.CountEntries;
                Row.SOPNUMBE = se.SOPNUMBE;
                Row.ITEMNMBR = se.ITEMNMBR;
                Row.ORD = se.ORD;
                Row.VNDDOCNM = se.VNDDOCNM;
                Row.VNDITNUM = se.VNDITNUM;
                Row.CZ_CarKod = se.CZ_CarKod;
                Row.LOCNCODE = se.LOCNCODE;

                Row.QTYSHPPD = 0;
                Row.QTYPACK = 0;

                Row.SERLTNUM = string.Empty;
                Row.KOD_SW = string.Empty;
                Row.DAT_VYROBY = string.Empty;
                Row.REZ_1 = string.Empty;
                Row.ODBER_ID = string.Empty;
                Row.DATEDONE = string.Empty;
                Row.TIMEDONE = string.Empty;

                Row.USER_ID = 0;

                Row.TYPEPAL = string.Empty;
                Row.NMBRPAL = string.Empty;

                Row.PRINTED = 0;

                Row.GUID = Guid.NewGuid();

                Row.SKL_ID = se.SKL_ID;

                Row.REZ_2 = string.Empty;

                Row.INPUT_MODE = 0;
                Row.ID_TERMINAL = 0;

                Row.MJ = string.Empty;

                Row.ITEMCODE = se.ITEMCODE;
                Row.WEIGHT = se.IsWEIGHTNull() ? 0 : se.WEIGHT;

                Row.SetExpiraceNull();

                dt_si.AddCZMST_SIRow(Row);
            }

            System.Diagnostics.Debug.Assert(true, "dt_si.count=" + dt_si.Count.ToString());



            //foreach (Datasets.Vydej.CZMST_SIRow row in dt_si)
                foreach (IGrouping<int, Datasets.Vydej.CZMST_SIRow> row in dt_si.GroupBy(x => x.ORD))
                {

                //Datasets.DatabasePohoda.OBJpolDataTable dtOBJPol = taOBJPol.GetDataBy_RefAg_RefSKz(int.Parse(row.ITEMNMBR), row.SOPNUMBE);
                Datasets.DatabasePohoda.OBJpolDataTable dtOBJPol = Database.Pohoda.OBJPol_GetDataBy_ID(row.Key);
                Datasets.DatabasePohoda.OBJpolRow radek = null;

                if ((dtOBJPol != null) && (dtOBJPol.Count > 0))
                    radek = dtOBJPol.First();

                //string Preneseno = "4"; // doklad bude mit priznak vyřizeho
                //string Preneseno = "2"; // doklad NEbude mit priznak vyřizeho

                decimal dodano = radek.IsDodanoNull() ? 0 : (decimal)radek.Dodano;
                decimal mnozstvi = radek.IsMnozstviNull() ? 0 : (decimal)radek.Mnozstvi;

                var qty = row.Sum(x => x.QTYSHPPD);

                if (mnozstvi <= (qty + dodano))
                    preneseno.Add(4);
                else
                    preneseno.Add(2);

                //if (mnozstvi <= (row.QTYSHPPD + dodano))
                //    preneseno.Add(4);
                //else
                //    preneseno.Add(2);

            }



            if (preneseno.Contains(2))
            {
                Preneseno = "2";
            }

            dt_si.RejectChanges();

            System.Diagnostics.Debug.Assert(true, "dt_si.count=" + dt_si.Count.ToString());
            return Preneseno;
        }


        #endregion
    }
}
