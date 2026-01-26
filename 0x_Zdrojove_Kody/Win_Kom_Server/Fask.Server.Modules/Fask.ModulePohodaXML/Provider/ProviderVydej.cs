using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;
using System.IO;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Fask.Tracing;

namespace Fask.SQL
{
    public partial class Provider : Fask.Server.Interfaces.Vydej.IVydej
    {
        private string TABLE_CZMST_SE = "CZMST_SE";
        private string TABLE_CZMST_SI = "CZMST_SI";
        //private string TABLE_CZMST_SE_SN = "CZMST_SE_SN";
        private string TABLE_CZMST_SIH = "CZMST_SIH";
		private string TABLE_FASK_ZASOBY = "FASK_ZASOBY";


        public const string vydej_import_vydejka = XML.MST_Pohoda._import_vydejka + ".xml";
        public const string vydej_import_faktura = XML.MST_Pohoda._import_faktura + ".xml";
        public const string vydej_import_prevodku = XML.MST_Pohoda._import_prevodka + ".xml";

        #region IVydej Members

        public Fask.Server.Interfaces.Classes.StatusInfo Vydej_GenerateDavka(
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
            Globals_V1.LoadConfiguration();
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug,"Vydej_GenerateDavka : 1");
            StatusInfo si = new StatusInfo();
            si.Description = "Vydej_GenerateDavka start";
            si.ID = 0;

			// \TODO : generovat data dokladu na zaklade cisla dokladu ... 
            // 1) test, zda v SE existuje rozpracovany doklad ...
            // 2) Muze byt Faktura ze skladu nebo Prevodka ze skladu (Sklad musi byt uveden)
            // Dotazuje se pres xml, ale nejdrive zjisti dotazem, zda doklad existuje a je to 
            // a) fakttura
            // b) prevodka ze zvoleneho skladu

            #region Test code
            //Globals.LoadConfiguration(); //nacteni konfigurace
            //Datasets.DatabasePohodaTableAdapters.FATableAdapter ta_fa = new Fask.ModulePohodaXML.Datasets.DatabasePohodaTableAdapters.FATableAdapter();
            //ta_fa.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
            //var tbl_fa = ta_fa.GetDataBy_Typ_Vyrizeno(1, false);

            //Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();
            //StringBuilder s = new StringBuilder();
            //foreach (var row_fa in tbl_fa)
            //{
            //    s.AppendLine(row_fa.Cislo);
            //}
            //si.Description = s.ToString();
            //si.ID = tbl_fa.Count;
            #endregion

            // 1) test na rozpracovany doklad 
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 2");
            byte? czdoslo = Database.Vydej.CZMSTSE_SOPNUMBER_CZDOSLO(objednavka.ID);
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 3");
            if (czdoslo.HasValue)
            { //existuje a je stazene v terminalu => vrati chybu ... nelze vytvorit duplicitu ...
                if (czdoslo.Value > 0)
                {
                    si.ID = -1;
                    si.Description = string.Format("Existuje rozpracovaná dávka pro doklad '{0}' na terminálu č.:{1}", objednavka.ID, czdoslo.Value); //"Existuje rozpracovaná dávka pro doklad '" + objednavka.ID + "' na terminálu č.:" + czdoslo.Value;
                    si.InnerException = new Exception(si.Description);
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, si.Description);
                    return si;
                }
                else if (czdoslo.Value == 0) //je pripravena ke zpracovani => uzavrit ... 
                {
                    Database.Vydej.CZMSTSE_UPDATE_CZDOSLO(objednavka.ID);
                }
            }
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 4");
            // Dotazeni ID skladu
            if (String.IsNullOrEmpty(sklad.ID))
                sklad.ID = Globals_V1.Konfigurace.Vydej[0].HlavnySkladID.ToString();
			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 5");
            // 2)a) test zda je to faktura
            if (Database.Pohoda.FA_Exists(objednavka.ID))
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 5a");
                // Pokud je na fakture polozka z jineho skladu, tak neumoznit provest export(vydej) 
                // => oznamit na uroven terminalu...
                if (!Database.Pohoda.FA_JedenSklad(objednavka.ID, sklad.ID))
                {
                    throw new Exception("Faktura '" + objednavka.ID + "' obsahuje položky z více skladů!");
                }
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 5b");
                string stav = this.ExportVydejkaPohoda_Z_Faktury_DirectAccess2DB(objednavka, sklad);
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 5c");
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
            // 2)b) test zda je to prevodka
            else if (Database.Pohoda.Prevod_Exists(objednavka.ID))
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 6a");
                // Pokud je na prevodce polozka z jineho skladu, tak neumoznit provest export(vydej) 
                // => oznamit na uroven terminalu...
                if (!Database.Pohoda.Prevod_JedenZdrojSklad(objednavka.ID, sklad.ID))
                {
                    throw new Exception("Převodka '" + objednavka.ID + "' obsahuje položky z jiných skladů!");
                }
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 6b");
                string stav = this.ExportVydejkaPohoda_Z_Prevodka(objednavka, sklad);
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 6c");
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
            // 2)c) test zda je to vydejka
            else if (Database.Pohoda.Vydejka_Exists(objednavka.ID))
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 7a");
                // Pokud je na vydejce polozka z jineho skladu, tak neumoznit provest export(vydej) 
                // => oznamit na uroven terminalu...
                if (!Database.Pohoda.Vydejka_JedenSklad(objednavka.ID, sklad.ID))
                {
                    throw new Exception("Výdejka '" + objednavka.ID + "' obsahuje položky z jiných skladů!");
                }
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 7b");
                string stav = this.ExportVydejkaPohoda_Z_Vydejky(objednavka, sklad);
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 7c");
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
            // 2)d) test zda je to prodejka
            else if (Database.Pohoda.Prodejka_Exists(objednavka.ID))
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 8a");
                // Pokud je na vydejce polozka z jineho skladu, tak neumoznit provest export(vydej) 
                // => oznamit na uroven terminalu...
                if (!Database.Pohoda.Prodejka_JedenSklad(objednavka.ID, sklad.ID))
                {
                    throw new Exception("Prodejka '" + objednavka.ID + "' obsahuje položky z jiných skladů!");
                }
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 8b");
                string stav = this.ExportVydejkaPohoda_Z_Prodejky(objednavka, sklad);
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 8c");
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
            // 2)e) test zda je to prijata objednavka
            else if (Database.Pohoda.OBJ_Exists(objednavka.ID))
            {

				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 9a");
                // Pokud je na vydejce polozka z jineho skladu, tak neumoznit provest export(vydej) 
                // => oznamit na uroven terminalu...
                if (!Database.Pohoda.PrjateObjednavky_JedenSklad(objednavka.ID, sklad.ID))
                { 
                    throw new Exception("Výdejka '" + objednavka.ID + "' obsahuje položky z jiných skladů!");
                }
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 9b");
                string stav = this.ExportVydejkaPohoda_Z_PrjateObjednavky(objednavka, sklad);
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 9c");
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
            // 2) nepodporovany typ dokladu
            else
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 10a");
                si.Description = "Doklad '" + objednavka.ID + "' nenalezen.";
                if (sklad != null)
                    si.Description += "\nSklad " + sklad.ID;
                si.InnerException = new Exception(si.Description);
                si.ID = -10;
                throw new Exception(si.Description);
            }

			Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Vydej_GenerateDavka : 11");
            return si;
        }


        private string ExportVydejkaPohoda_Z_PrjateObjednavky_DirectAccess2DB(Objednavka objednavka, Sklad sklad)
        {
            try
            {

                if (String.IsNullOrEmpty(objednavka.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

				Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
				if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
					ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				}

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
                //ta_pohoda_OBJCislo.Fill(ds_pohoda.OBJCislo, objednavka.ID);
				Database.Pohoda.OBJCislo_Fill(ds_pohoda.OBJCislo, objednavka.ID);

                // dotazeni parametru pro locncode (vychozi lokaci ..)
				//Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
				//SKzParametryTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

                // *************************************************
                // naplneni do czmst_se ... 
                // *************************************************
                //Datasets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
                //CZMST_SETableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

          
                Datasets.Vydej.CZMST_SEDataTable seTable = new Datasets.Vydej.CZMST_SEDataTable();
                Datasets.Vydej.CZMST_SERow seRow = null;

                int cisloDavky = 0;

				int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
				int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;

                foreach (var invoiceItem in ds_pohoda.OBJCislo)
                {
                    // Pokud to neni skladova polozka, tak ji neresit ...
					// \TODO : otestovat, zda to takto je ... => tedy test na NULL hodnotu...
                    if ((invoiceItem == null) || (invoiceItem.IsSKz_IDNull()))
                        continue;

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

                    //seRow.CZ_CarKod = typPLU == null ? string.Empty : typPLU.Value; //carovy kod => dotahnout z xml PLU
                    seRow.CZ_CarKod = invoiceItem.IsSKz_IDSNull() ? string.Empty : invoiceItem.SKz_IDS.Trim();
                    seRow.CZ_DatVyr_Delka = 0; //?
                    seRow.CZ_DatVyr_Track = 0; //?
                    seRow.CZ_Doslo = 0; // pripraveno pro zpracovani
                    seRow.CZ_SerNum_Delka = 0; //? delka serioveho cisla 
                    //seRow.CZ_SerNum_Track = 0; //? sledovat na SN/Sarze => je sledovano na seriove cislo => zjistit v Pohoda
                    //seRow.CZ_SerNum_Track = (byte)(skz_row == null ? 0 : skz_row.RelSKzVC);
                    
                    //TaD 19.9.2018 Uprava prace s  SerNumTrack
                    //seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack(
                    //    (invoiceItem == null) || (invoiceItem.IsSKz_RelSKzVCNull()) ? (int?)null : (int?)invoiceItem.SKz_RelSKzVC
                    //    );
                    
					//if (Properties.Settings.Default.POHODA_E1)
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
                    //seRow.DEX_ROW_ID
                    seRow.LOCNCODE = locncodeDeafult;
                    seRow.Note = " "; //invoiceItem.IsSKz_STextNull() ? " " : invoiceItem.SKz_SText;  //""; //? poznamka 
                    seRow.ORD = invoiceItem.OBJpol_ID; //int.Parse(invoiceItem.Element(inv + "id").Value.Trim()); //? poradi polozky na dokladu - pro pohoda id zaznamu v tabulce polozek <inv:id>
                    seRow.PRINTED = 0;
                    seRow.PRIORITY = 3; //? priorita ... 
                    seRow.QTYPACK = 1;//invoiceItem.IsOBJpol_MJKoefNull() ? 0 : Convert.ToDecimal(invoiceItem.OBJpol_MJKoef); // TODO : rozpad na varianty baleni dle car kodu ... 
                    seRow.QTYPAL = 0; //? palety neresime ... ???
                    seRow.QTYSHPPD = Convert.ToDecimal(invoiceItem.OBJ_Mnozstvi); //decimal.Parse(invoiceItem.Element(inv + "quantity").Value.Trim(), System.Globalization.NumberFormatInfo.InvariantInfo); // mnozstvi k vydeji => <inv:quantity>
                    seRow.SKL_ID = invoiceItem.SKz_RefSklad.ToString(); //stockItem.Element(typ + "store").Element(typ + "id").Value.Trim(); //? id skladu => <inv:stockItem><typ:store><typ:id> : jde o identifikator skladu
                    seRow.SOPNUMBE = objednavka.ID;
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

                    // Nacist vychozi lokaci ...
                    // az tady, protoze musi mit nastaveny skl_id a itemnmbr...
                    Vydej.LocncodeFindAlgorithmVychozi(seRow);

                    //vlozit do se ...
                    seTable.AddCZMST_SERow(seRow);

                    //if (Properties.Settings.Default.MerneJednotky_DotahovatDalsiVarianty)
                    //{
                    //    // MJ2 a qtypack (pokud existuje)
					//    // \TODO: pridat mernou jednotku
                    //    if (invoiceItem != null && !invoiceItem.IsSkz_MJ2Null() && !invoiceItem.IsSkz_MJ2KoefNull())
                    //    {
                    //        // kopie zaznamu
                    //        Datasets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                    //        seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                    //        seRow = seRow2;

                    //        seRow.QTYPACK = (decimal)invoiceItem.Skz_MJ2Koef;
                    //        seRow.MJ = invoiceItem.Skz_MJ2;
                    //        if (seRow.MJ.Length > seTable.MJColumn.MaxLength)
                    //        {
                    //            seRow.MJ = seRow.MJ.Remove(seTable.MJColumn.MaxLength);
                    //        }

                    //        seTable.AddCZMST_SERow(seRow);
                    //    }

                    //    // MJ3 a qtypack (pokud existuje)
                    //    if (invoiceItem != null && !invoiceItem.IsSkz_MJ3Null() && !invoiceItem.IsSkz_MJ3KoefNull())
                    //    {
                    //        // kopie zaznamu
                    //        Datasets.Vydej.CZMST_SERow seRow3 = seTable.NewCZMST_SERow();
                    //        seRow3.ItemArray = seRow.ItemArray.Clone() as object[];
                    //        seRow = seRow3;

                    //        seRow.MJ = invoiceItem.Skz_MJ3;
                    //        seRow.QTYPACK = (decimal)invoiceItem.Skz_MJ3Koef;
					//        // \TODO: osetrit delku
                    //        if (seRow.MJ.Length > seTable.MJColumn.MaxLength)
                    //        {
                    //            seRow.MJ = seRow.MJ.Remove(seTable.MJColumn.MaxLength);
                    //        }

                    //        seTable.AddCZMST_SERow(seRow);
                    //    }
                    //}

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
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "ExportVydejkaPohoda_Z_Faktury_DirectTSQL");
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
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(Vydejka: " + objednavka.ID + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);

                throw ex;
                //return ex.Message;
            }

        }

        private string ExportVydejkaPohoda_Z_PrjateObjednavky(Objednavka objednavka, Sklad sklad)
        {
            try
            {
                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(XML.MST_Pohoda._export_PrjateObjednavky + ".xml");

                //filename = XML.PohodaComunication.FilenameCompose(XML.PohodaComunication._export_PrjateObjednavky + ".xml");

                if (String.IsNullOrEmpty(objednavka.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Vydej.CreateRequest_PrjateObjednavky_XML(filename, objednavka))// CreateRequest_Vydejka_XML(filename, objednavka))
                    return "CHYBA";

                bool saveToDB = objednavka.ID != string.Empty ? true : false;

                //pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, objednavka, new Fask.Server.Interfaces.Classes.User());
                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                return Vydej.LoadResponse_PrjateObjednavky_XML(responsefilename, saveToDB, objednavka);
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(Vydejka: " + objednavka.ID + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);

                throw ex;
                //return ex.Message;
            }
        }

        public Fask.DataSets.Vydejky Vydej_GetVydejky(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item, Fask.Server.Interfaces.Classes.User user)
        {
			// \TODO : vracet seznam dokladu, ktere jsou k vydani ...
            // Jsou to data z tabulky czmst_se
            // ??? : mohou se k tomu pridat i doklady faktur a prevodek, ktere nejsou potvrzene ... ??? => Union => group by ... ???
            //throw new NotImplementedException();

            //string select = "SELECT distinct [CountEntries], [SOPNUMBE], 1 AS [CntItems], 2 as [SumItems], 'Info' as [Info1], [PRIORITY], 1 AS [ROZPRACOVANO]  FROM [CZMST_SE] WHERE CountEntries not in (Select CountEntries from czmst_si) and CZ_Doslo <= 0 and SKL_ID LIKE '" + SQLInjection.Filter(prefixskladu) + "%' order by countentries";				
            //string select = "SELECT distinct [CountEntries], [SOPNUMBE], 1 AS [CntFields], 2 as [SumItems] FROM [CZMST_SE] WHERE CountEntries not in (Select CountEntries from czmst_si) and CZ_Doslo <= 0 and SKL_ID LIKE '" + SQLInjection.Filter(prefixskladu) + "%' order by countentries";				
            //JS : 19.8.2008
            //Novy select na doplnkove informace dle zadani pro AGRO
            //Pocet polozek(itemnmbr) v davce
            //Soucet kusu na vydani (qtyshppd)
            //Soucet kusu v baleni (qtypack)
            string select =
                " SELECT A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO " +
                //" SELECT distinct A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO " +
                " FROM CZMST_SE A" +
                " INNER JOIN " +
                " ( " +
                "	SELECT X.COUNTENTRIES, X.SOPNUMBE, COUNT(X.ITEMNMBR) CNTITEMS " +
                "	FROM ( " +
                "		SELECT COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
                "		FROM CZMST_SE " +
                "       WHERE QTYPACK=0" +
                "       AND " +
                "       SKL_ID LIKE '" + sklad.ID + "%' " +
                "       AND " +
                "       ITEMTYPE LIKE '" + item.Type + "%' " +
                "       AND " +
                "       (CZ_DOSLO<=0 OR CZ_DOSLO=" + terminal.ID + ")" +
                "		GROUP BY COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
                "	) X " +
                "	GROUP BY X.COUNTENTRIES, X.SOPNUMBE " +
                " ) B ON  " +
                " B.COUNTENTRIES=A.COUNTENTRIES " +
                " AND B.SOPNUMBE=A.SOPNUMBE " +
                " INNER JOIN " +
                " ( " +
                "	SELECT COUNTENTRIES, SOPNUMBE, " +
                "       SUM(QTYSHPPD) AS QTYSHPPDSUM" +
                "	FROM CZMST_SE " +
                "   WHERE QTYPACK=0" +
                "   AND " +
                "   SKL_ID LIKE '" + sklad.ID + "%' " +
                "   AND " +
                "   ITEMTYPE LIKE '" + item.Type + "%' " +
                "   AND " +
                "   (CZ_DOSLO<=0 OR CZ_DOSLO=" + terminal.ID + ")" +
                "	GROUP BY COUNTENTRIES, SOPNUMBE " +
                " ) C ON " +
                " C.COUNTENTRIES=A.COUNTENTRIES  " +
                " AND C.SOPNUMBE=A.SOPNUMBE " +
                //15.2.2011, JiS - rozsireni o dotazeni stavu rozpracovanosti vydejky
                " LEFT OUTER JOIN " +
                " ( " +
                "   SELECT COUNTENTRIES, SOPNUMBE, COUNT(*) AS ROZPRACOVANO FROM CZMST_SI" +
                "   GROUP BY COUNTENTRIES, SOPNUMBE " +
                " ) D ON " +
                " D.COUNTENTRIES=A.COUNTENTRIES " +
                " and D.SOPNUMBE=A.SOPNUMBE " +
                //konec rozpracovanosti vydejky
                " WHERE " +
                //" A.CountEntries not in (Select CountEntries from " + TABLE_CZMST_SI + ") " +
                //" and " +
                //" A.CZ_Doslo<=0 " + 
                " (A.CZ_DOSLO<=0 OR A.CZ_DOSLO=" + terminal.ID + ")" +
                " AND " +
                " A.SKL_ID LIKE '" + sklad.ID + "%' " +
                " AND " +
                " A.ITEMTYPE LIKE '" + item.Type + "%' " +
                " GROUP BY A.PRIORITY, A.COUNTENTRIES, A.SOPNUMBE, B.CNTITEMS, C.QTYSHPPDSUM, D.ROZPRACOVANO " +
                ""; //(vydejSeznamDavekRazeni == string.Empty ? vydejSeznamDavekRazeni : " ORDER BY " + vydejSeznamDavekRazeni);

            System.Data.SqlClient.SqlDataAdapter xda = new System.Data.SqlClient.SqlDataAdapter(select, (new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB)));

            Fask.DataSets.Vydejky volnevydejky = new Fask.DataSets.Vydejky();
            ((System.Data.Common.DbDataAdapter)xda).Fill(volnevydejky, volnevydejky.Hlavicky.TableName);

            // \TODO : dotazeni detailu ...
            #region Rozsireni o dotazeni informace do infa z existujiciho pohledu detailu. vic neni mozne
            //try
            //{
            //    bool vydejInfo1allow = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["VydejInfo1Allow"]);
            //    bool vydejVydejkaDetail2Hlavicka = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["VydejVydejkaDetail2Hlavicka"]);
            //    string vydejInfo1field = System.Configuration.ConfigurationManager.AppSettings["VydejInfo1Field"];
            //    if (vydejInfo1allow)
            //    {
            //        foreach (Fask.DataSets.Vydejky.HlavickyRow hrow in volnevydejky.Hlavicky)
            //        {
            //            try
            //            {
            //                DataSet ds = Detail(hrow.SOPNUMBE);
            //                hrow.Info1 = Convert.ToString(ds.Tables[0].Rows[0][vydejInfo1field]);
            //            }
            //            catch
            //            { }
            //        }
            //    }
            //    else if (vydejVydejkaDetail2Hlavicka)
            //    {
            //        //detail dle cisla cisla objednavky
            //        DataSet dsdetail = Detail(string.Empty);
            //        if (dsdetail != null)
            //        {
            //            if (dsdetail.Tables.Count > 0)
            //            {
            //                //DataColumn[] dcols = new DataColumn[ds.Tables[0].Columns.Count];
            //                //ds.Tables[0].Columns.CopyTo(dcols, 0);
            //                //volnevydejky.Hlavicky.Columns.AddRange(dcols);
            //                foreach (DataColumn dcol in dsdetail.Tables[0].Columns)
            //                {
            //                    volnevydejky.Hlavicky.Columns.Add(dcol.ColumnName, dcol.DataType);
            //                }

            //                #region trace
            //                Trac.Write(volnevydejky, "GetVydejky, vydejVydejkaDetail2Hlavicka: " + vydejVydejkaDetail2Hlavicka.ToString(), tracid);
            //                #endregion
            //            }

            //            if (volnevydejky.Hlavicky.Count > 0)
            //            {
            //                foreach (Fask.DataSets.Vydejky.HlavickyRow hrow in volnevydejky.Hlavicky)
            //                {
            //                    try
            //                    {
            //                        dsdetail = Detail(hrow.SOPNUMBE);
            //                        //hrow.Info1 = Convert.ToString(ds.Tables[0].Rows[0][vydejInfo1field]);
            //                        foreach (DataColumn dcol in dsdetail.Tables[0].Columns)
            //                        {
            //                            DataColumn dcolhrow = hrow.Table.Columns[dcol.ColumnName];
            //                            hrow.SetField<object>(dcolhrow, dsdetail.Tables[0].Rows[0][dcol]);
            //                        }
            //                    }
            //                    catch
            //                    { }
            //                }
            //            }
            //        }

            //        //detail dle cisla cisla davky
            //        DataSet dsdetaildavka = DetailDavka(string.Empty);
            //        if (dsdetaildavka != null)
            //        {
            //            if (dsdetaildavka.Tables.Count > 0)
            //            {
            //                //DataColumn[] dcols = new DataColumn[ds.Tables[0].Columns.Count];
            //                //ds.Tables[0].Columns.CopyTo(dcols, 0);
            //                //volnevydejky.Hlavicky.Columns.AddRange(dcols);
            //                foreach (DataColumn dcol in dsdetaildavka.Tables[0].Columns)
            //                {
            //                    volnevydejky.Hlavicky.Columns.Add(dcol.ColumnName, dcol.DataType);
            //                }
            //            }

            //            if (volnevydejky.Hlavicky.Count > 0)
            //            {
            //                foreach (Fask.DataSets.Vydejky.HlavickyRow hrow in volnevydejky.Hlavicky)
            //                {
            //                    try
            //                    {
            //                        dsdetaildavka = DetailDavka(hrow.CountEntries.ToString());
            //                        //hrow.Info1 = Convert.ToString(ds.Tables[0].Rows[0][vydejInfo1field]);
            //                        foreach (DataColumn dcol in dsdetaildavka.Tables[0].Columns)
            //                        {
            //                            DataColumn dcolhrow = hrow.Table.Columns[dcol.ColumnName];
            //                            hrow.SetField<object>(dcolhrow, dsdetaildavka.Tables[0].Rows[0][dcol]);
            //                        }
            //                    }
            //                    catch
            //                    { }
            //                }
            //            }
            //        }

            //        volnevydejky.AcceptChanges();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Log.writeErrorLog(ex.Message);
            //}
            #endregion

            volnevydejky.AcceptChanges();

            return volnevydejky;
        }

        public Fask.DataSets.Vydej Vydej_GetVydejka(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        {
            // Vraci vydejku z tabulky SE
            //throw new NotImplementedException();

            Globals_V1.LoadConfiguration();

            string select = string.Empty;

            if (Globals_V1.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
            {
                select = "SELECT * FROM CZMST_SE " +
                " where CountEntries=" + davka.ID.Value.ToString() +
                    //" and ITEMTYPE like '" + SQLInjection.Filter(itemtype) + "%' " +
                " and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
                " order by dex_row_id" +
                "";
            }
            else
            {
                select = "SELECT * FROM CZMST_SE " +
                " where CountEntries=" + davka.ID.Value.ToString() +
                " and ITEMTYPE like '" + item.Type + "%' " +
                " and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
                " order by dex_row_id" +
                "";
            }


            System.Data.SqlClient.SqlDataAdapter xda = new System.Data.SqlClient.SqlDataAdapter(select, (new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB)));

            Fask.DataSets.Vydej vydej = new Fask.DataSets.Vydej();
            ((System.Data.Common.DbDataAdapter)xda).Fill(vydej, vydej.CZMST_SE.TableName);
            foreach (Fask.DataSets.Vydej.CZMST_SERow sr in vydej.CZMST_SE)
            {
                sr.CZ_Doslo = (byte)terminal.ID;


            }

            // Dotazeni dat z SI => pouze v pripade, ze se nepouziva vychystavani vice terminaly!!!
            // Slouzi k poslani davky k pozdejsimu zpracovani na jinem terminalu ...
            if (item.Type.Trim().Length == 0)
            {
                string selectSI =
                    "SELECT * FROM CZMST_SI " +
                    " where CountEntries=" + davka.ID.Value.ToString() +
                    //" and ITEMTYPE like '" + SQLInjection.Filter(itemtype) + "%' " +
                    "";
                System.Data.SqlClient.SqlDataAdapter xdaSI = new System.Data.SqlClient.SqlDataAdapter(selectSI, (new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB)));
                ((System.Data.Common.DbDataAdapter)xdaSI).Fill(vydej, vydej.CZMST_SI.TableName);
            }

            //Dotazeni predlohy seriovych cisel
            try
            {
                string selectSESN =
                    "Select * from CZMST_SE_SN " +
                    " where CountEntries=" + davka.ID.Value.ToString();

                SqlDataAdapter xdaSESN = new SqlDataAdapter(selectSESN, (new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB)));
                ((DbDataAdapter)xdaSESN).Fill(vydej, vydej.CZMST_SE_SN.TableName);

            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle("Vydej", "GetVydejka.PredlohaSN", ex);
            }

            return vydej;

        }

        #region 15.9.2025 MaR predelani na podminku inventury
        //public bool Vydej_GetVydejkaReceived(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        //{
        //    // Potvrzuje prevzeti davky vydejky
        //    //throw new NotImplementedException();

        //    Globals_V1.LoadConfiguration();

        //    string selectCount = string.Empty;
        //    string update = string.Empty;


        //    ////15.9.2025 pridani podminky DavkaTerminalVice
        //    ////kdyz bude itemType == I a soucasne v sekci konfiguracniho souboru sekci Inventura prvek DavkaTerminalVice == true, nezmeni CZDoslo z 0 na cislo terminalu, zustane zachovany..
        //    //if (item.Type == "I" && Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
        //    if (Globals_V1.Konfigurace.Inventura1[0].DavkaTerminalVice)
        //    {
        //        if (Globals_V1.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
        //        {
        //            selectCount =
        //                "SELECT Count(CountEntries) as davka FROM CZMST_SE " +
        //                " where CountEntries=" + davka.ID.Value.ToString() +
        //                //" and ITEMTYPE like '" + SQLInjection.Filter(itemtype) + "%' " +
        //                " AND (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";

        //        }
        //        else
        //        {
        //            selectCount =
        //                "SELECT Count(CountEntries) as davka FROM CZMST_SE " +
        //                " where CountEntries=" + davka.ID.Value.ToString() +
        //                " and ITEMTYPE like '" + item.Type + "%' " +
        //                " AND (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";


        //        }

        //        SqlConnection xconn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        //        SqlCommand xcommAllowed = new SqlCommand(selectCount, xconn);

        //        try
        //        {
        //            xconn.Open();
        //            object r = xcommAllowed.ExecuteScalar();
        //            if (r == null)
        //                throw new Exception("Dávka nenalezena.");
        //            if (r is int && ((int)r) <= 0)
        //                throw new Exception("Dávka se již zpracovává.");


        //        }
        //        catch (Exception ex)
        //        {
        //            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Dávka: " + davka.ID.Value.ToString() + ", Terminál ID:" + terminal.ID.ToString() + ", ITEMTYPE:" + item.Type + ")");
        //            Fask.Logging.ExceptionHandler2.Handle(ex);

        //            //throw ex;
        //            return false;

        //        }
        //        finally
        //        {
        //            if (xconn.State == ConnectionState.Open)
        //                xconn.Close();
        //        }

        //        //nic se nenastavuje
        //        return true;
        //    }
        //    else
        //    {

        //        if (Globals_V1.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
        //        {
        //            selectCount =
        //                "SELECT Count(CountEntries) as davka FROM CZMST_SE " +
        //                " where CountEntries=" + davka.ID.Value.ToString() +
        //                //" and ITEMTYPE like '" + SQLInjection.Filter(itemtype) + "%' " +
        //                " AND (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";

        //            update =
        //                "Update CZMST_SE " +
        //                " set cz_doslo=" + terminal.ID.ToString() +
        //                " where countentries=" + davka.ID.Value.ToString() +
        //                //" and ITEMTYPE like '" + SQLInjection.Filter(itemtype) + "%' " +
        //                " and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";
        //        }
        //        else
        //        {
        //            selectCount =
        //                "SELECT Count(CountEntries) as davka FROM CZMST_SE " +
        //                " where CountEntries=" + davka.ID.Value.ToString() +
        //                " and ITEMTYPE like '" + item.Type + "%' " +
        //                " AND (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";

        //            update =
        //                "Update CZMST_SE " +
        //                " set cz_doslo=" + terminal.ID.ToString() +
        //                " where countentries=" + davka.ID.Value.ToString() +
        //                " and ITEMTYPE like '" + item.Type + "%' " +
        //                " and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
        //                "";
        //        }

        //        SqlConnection xconn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        //        SqlCommand xcommAllowed = new SqlCommand(selectCount, xconn);
        //        SqlCommand xcomm = new SqlCommand(update, xconn);
        //        IDbTransaction itrans = null;

        //        int res = 0;
        //        try
        //        {
        //            xconn.Open();
        //            itrans = xconn.BeginTransaction(IsolationLevel.Serializable);

        //            xcommAllowed.Transaction = (SqlTransaction)itrans;
        //            object r = xcommAllowed.ExecuteScalar();
        //            if (r == null)
        //                throw new Exception("Dávka nenalezena.");
        //            if (r is int && ((int)r) <= 0)
        //                throw new Exception("Dávka se již zpracovává.");

        //            xcomm.Transaction = (SqlTransaction)itrans;
        //            res = xcomm.ExecuteNonQuery();
        //            if (itrans != null)
        //                itrans.Commit();

        //        }
        //        catch (Exception ex)
        //        {
        //            if (itrans != null)
        //                itrans.Rollback();

        //            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Dávka: " + davka.ID.Value.ToString() + ", Terminál ID:" + terminal.ID.ToString() + ", ITEMTYPE:" + item.Type + ")");
        //            Fask.Logging.ExceptionHandler2.Handle(ex);

        //            //throw ex;
        //            return false;
        //        }
        //        finally
        //        {
        //            if (xconn.State == ConnectionState.Open)
        //                xconn.Close();
        //        }

        //        return (res > 0);

        //    }



        //}

        #endregion

        public bool Vydej_GetVydejkaReceived(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        {
            // Potvrzuje prevzeti davky vydejky
            //throw new NotImplementedException();

            Globals_V1.LoadConfiguration();

            string selectCount = string.Empty;
            string update = string.Empty;
            string inventura = "I";

            ////15.9.2025 pridani podminky DavkaTerminalVice
            ////kdyz bude itemType == I a soucasne v sekci konfiguracniho souboru sekci Inventura prvek DavkaTerminalVice == true, nezmeni CZDoslo z 0 na cislo terminalu, zustane zachovany..
            // Předpoklady:
            // string TABLE_CZMST_SE;
            // var terminal.ID (int), davka.ID (int), item.Type (string), string inventura = "I";

            string selectCountSql;
            string updateSql;

            if (Globals_V1.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
            {
                selectCountSql =
                    $"SELECT COUNT(CountEntries) AS davka FROM {TABLE_CZMST_SE} " +
                    "WHERE CountEntries = @countEntries " +
                    "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) ";// +
                                                                       //"  AND ITEMTYPE <> @inventura;";

                //     //if (item.Type == "I" && Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
                if (Globals_V1.Konfigurace.Inventura1[0].DavkaTerminalVice)
                {
                    updateSql =
                                        $"UPDATE {TABLE_CZMST_SE} " +
                                        "SET cz_doslo = @terminalId " +
                                        "WHERE CountEntries = @countEntries " +
                                        "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) " +
                                        "  AND ITEMTYPE <> @inventura;";
                }
                else
                {
                    updateSql =
                               $"UPDATE {TABLE_CZMST_SE} " +
                               "SET cz_doslo = @terminalId " +
                               "WHERE CountEntries = @countEntries " +
                               "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) "; //+
                               //"  AND ITEMTYPE <> @inventura;";
                }
                    
            }
            else
            {
                selectCountSql =
                    $"SELECT COUNT(CountEntries) AS davka FROM {TABLE_CZMST_SE} " +
                    "WHERE CountEntries = @countEntries " +
                    "  AND ITEMTYPE LIKE @itemTypeLike " +
                    "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) ";// +
                                                                       //"  AND ITEMTYPE <> @inventura;";

                //     //if (item.Type == "I" && Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
                if (Globals_V1.Konfigurace.Inventura1[0].DavkaTerminalVice)
                {
                    updateSql =
                                   $"UPDATE {TABLE_CZMST_SE} " +
                                   "SET cz_doslo = @terminalId " +
                                   "WHERE CountEntries = @countEntries " +
                                   "  AND ITEMTYPE LIKE @itemTypeLike " +
                                   "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) " +
                                   "  AND ITEMTYPE <> @inventura;";
                }
                else
                {
                    updateSql =
                                   $"UPDATE {TABLE_CZMST_SE} " +
                                   "SET cz_doslo = @terminalId " +
                                   "WHERE CountEntries = @countEntries " +
                                   "  AND ITEMTYPE LIKE @itemTypeLike " +
                                   "  AND (cz_doslo <= 0 OR cz_doslo = @terminalId) "; //+
                                  // "  AND ITEMTYPE <> @inventura;";
                }

           
            }

            int res = 0;

            using (var xconn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
            using (var xcommAllowed = new SqlCommand(selectCountSql, xconn))
            using (var xcomm = new SqlCommand(updateSql, xconn))
            {
                xconn.Open();
                using (var tran = xconn.BeginTransaction(IsolationLevel.Serializable))
                {
                    xcommAllowed.Transaction = tran;
                    xcomm.Transaction = tran;

                    // Společné parametry
                    xcommAllowed.Parameters.Add("@countEntries", SqlDbType.Int).Value = davka.ID;
                    xcomm.Parameters.Add("@countEntries", SqlDbType.Int).Value = davka.ID;

                    xcommAllowed.Parameters.Add("@terminalId", SqlDbType.Int).Value = terminal.ID;
                    xcomm.Parameters.Add("@terminalId", SqlDbType.Int).Value = terminal.ID;

                    xcommAllowed.Parameters.Add("@inventura", SqlDbType.NVarChar, 50).Value = inventura;
                    xcomm.Parameters.Add("@inventura", SqlDbType.NVarChar, 50).Value = inventura;

                    // Parametr pro LIKE jen v případě, že se používá
                    if (!Globals_V1.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
                    {
                        // SQLInjection.Filter(item.Type) nahrazuje parametrizace + přidání %
                        string itemTypeLike = (item?.Type ?? string.Empty) + "%";

                        xcommAllowed.Parameters.Add("@itemTypeLike", SqlDbType.NVarChar, 100).Value = itemTypeLike;
                        xcomm.Parameters.Add("@itemTypeLike", SqlDbType.NVarChar, 100).Value = itemTypeLike;
                    }

                    try
                    {
                        object r = xcommAllowed.ExecuteScalar();
                        int cnt = (r == null || r == DBNull.Value) ? 0 : Convert.ToInt32(r);

                        if (cnt <= 0)
                            throw new Exception("Dávka se již zpracovává nebo nebyla nalezena.");

                        res = xcomm.ExecuteNonQuery();
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }

            return (res > 0);



        }



        public Fask.Server.Interfaces.Classes.StatusObject Vydej_Process(
            Fask.Server.Interfaces.Classes.Davka davka, 
            Fask.Server.Interfaces.Classes.Terminal terminal,
            Fask.Server.Interfaces.Classes.Sklad sklad,
            Fask.Server.Interfaces.Classes.Item item,
            Fask.DataSets.Vydej vydejdata,
            Fask.Server.Interfaces.Vydej.ProcessState processVydejState,
            string itemtype)
        {
            string SOPNUMBE = string.Empty;
            string pom = string.Empty;
            pom = Globals_V1.LoadConfiguration();
            if (pom != "OK")
                throw new Exception("Vydej_Process, chyba nacteni konfigurace: " + pom);

            TracId tracid = new TracId(null, terminal.ID, davka.ID.Value);

            string guidDavka = string.Empty;
            if (vydejdata.CZMST_SEH.Count > 0)
                guidDavka = vydejdata.CZMST_SEH[0].GUID.ToString();
            else
                guidDavka = Guid.NewGuid().ToString();
            string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals_V1.Konfigurace.Sdilene[0].StatusObjectsDirectory, guidDavka));

            //StatusObject so = new StatusObject(filePath);
            StatusObject so = StatusObject.Load(filePath);
            if (so == null) //neexistuje => vytvorit a pokracovat
            {
                so = new StatusObject(filePath);
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

            so.StatusText = "Výdej zpracování ...";

            // Zpracuje data 
            // Faktura , Prevodka => maximalne oznaci jako vyrizene ...
            // Pokud neni vse nasimano, tak muze generovat avizo o rozdilu na email ... ????
            //throw new NotImplementedException();

            SqlTransaction trans = null;
            SqlConnection conn = null;

            if (vydejdata == null)
                return so;

            #region trace
            Trac.Write(vydejdata, "ProcessVydejka, provider null", tracid);
            #endregion

            vydejdata.AcceptChanges();

            //int countadded = vydejdata.CZMST_SI.Select(null, null, DataViewRowState.Added).Length;
            //bool uvolnitdavku = countadded == 0;

            //bool uvolnitdavku = vydejdata.CZMST_SI.Rows.Count == 0;
            
            try
            {
                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                conn.Open();
                trans = conn.BeginTransaction();

                if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.Uvolnit) //uvolnit davku
                {
                    foreach (Fask.DataSets.Vydej.CZMST_SERow serow in vydejdata.CZMST_SE.Rows)
                    {
                        if (serow.RowState == DataRowState.Modified || serow.RowState == DataRowState.Unchanged)
                        {
                            serow["CZ_Doslo"] = 0;
                        }
                    }

                    //Datasets.VydejTableAdapters.CZMST_SETableAdapter seta = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
                    //seta.Connection = conn;
                    //seta.Transaction = trans;
                    var v = vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent);
                    //int pocetRadku = seta.Update(v);
                    int pocetRadku = Database.Vydej.Update_CZMST_SE(v, conn, trans);

                    #region trace
                    Trac.Write(vydejdata.CZMST_SE, "ProcessVydejka, ProcessVydejState: " + processVydejState.ToString() + ", upraveno radku: " + pocetRadku.ToString(), tracid);
                    #endregion

                    if (trans != null)
                        trans.Commit();

                }
                else if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.Zpracovat || processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.ZpracovatAPokracovat)
                {
                    byte terminalid = (byte)terminal.ID;
                    bool allowInsertData = true;

                    if (Globals_V1.Konfigurace.Inventura1[0].DavkaTerminalVice && itemtype == "I")
                    {
                        //15.9.2025 MaR
                        //stale true
                        //bool allowInsertData = true;
                    }
                    else
                    {

                        string xselectcommand =
                            "Select Count(*) as number from " + TABLE_CZMST_SE +
                            " where countentries=" + davka.ID +
                            //" and cz_doslo>0 and cz_doslo<100" + 
                            " and cz_doslo=" + terminalid +
                            "";


                        System.Data.SqlClient.SqlCommand xselect = new System.Data.SqlClient.SqlCommand(xselectcommand, conn, trans);
                        object datacount = xselect.ExecuteScalar();
                        if (datacount != null && ((int)datacount) == 0)
                        {
                            allowInsertData = false;
                        }
                    }


                    if (allowInsertData)
                    {

                        #region Online validace dat vuči pohode

                        if (((Globals_V1.Konfigurace.Vydej[0].Validace_Mnozstvi_import) && (Globals_V1.Konfigurace.Vydej[0].GenerovatFakturu && (itemtype == "J")) || (Globals_V1.Konfigurace.Vydej[0].GenerovatVydejku && itemtype == "J")))
                        {
							#region 20190628 TaD Stara metoda kontroly disponibility

                            //Database.InfoValidace info = Database.Pohoda.OBJPol_Validace(vydejdata);

                            //if(info.Status == "OK")
                            //{
                            //    //ve je OK a mužeme pokračovat

                            //} 
                            //else if(info.Status == "ERR")
                            //{
                            //    throw new Exception(string.Format("Obj.:{0}" + Environment.NewLine + "Pol.:{1}" + Environment.NewLine + "MN:{2}" + Environment.NewLine + "ErrMN:{3} ", info.RadekVydej.SOPNUMBE, info.RadekVydej.ITEMNMBR, info.RadekVydej.QTYSHPPD, info.RadekVydej.ZBUDE));
                            //}
                            //else
                            //{
                            //    throw new Exception(info.Status);
                            //}

							#endregion

							#region Univerzalny spusob validace

							Fask.POHODA.Disponibility.StatusInfo info = Database.Pohoda.Vydej_Validace(vydejdata);

							if (info.ID != 0)
							{
								//ve je OK a mužeme pokračovat
								throw new Exception(info.Description);

							}
							
							#endregion

                        }

                        #endregion


                        so.Write("Import Dávky.");


                        if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.ZpracovatAPokracovat)
                            terminalid = 0;
                        else 
                            terminalid += 100;

                        foreach (Fask.DataSets.Vydej.CZMST_SERow serow in vydejdata.CZMST_SE)
                        {
                           
                            if (serow.RowState == DataRowState.Modified || serow.RowState == DataRowState.Unchanged)
                            {
                                serow.CZ_Doslo = terminalid;
                            }
                        }

                        vydejdata.CZMST_SI.AcceptChanges();

                        #region Smazani starych dat davky z vystupu
                        if (Globals_V1.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
                        {
                            System.Data.SqlClient.SqlCommand xdeleteolddata = new System.Data.SqlClient.SqlCommand("Delete from " + TABLE_CZMST_SI + " where countentries=" + davka.ID, conn, trans);
                            int ndeleted = xdeleteolddata.ExecuteNonQuery();

                            xdeleteolddata.CommandText = "Delete from " + TABLE_CZMST_SIH + " where countentries=" + davka.ID;
                            ndeleted = xdeleteolddata.ExecuteNonQuery();
                        }
                        #endregion

                        foreach (Fask.DataSets.Vydej.CZMST_SIRow sirow in vydejdata.CZMST_SI)
                        {
                            SOPNUMBE = sirow.SOPNUMBE;
                            sirow.SetAdded();
                        }
                        foreach (Fask.DataSets.Vydej.CZMST_SIHRow sihrow in vydejdata.CZMST_SIH)
                        {
                            sihrow.SetAdded();
                        }

                        foreach (Fask.DataSets.Vydej.CZMST_SI_BVRow siBVrow in vydejdata.CZMST_SI_BV)
                        {
                            siBVrow.SetAdded();
                        }
                        so.Write("Dávka již byla jednou importována.");

                        var se = vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent);
                        Database.Vydej.Update_CZMST_SE(se, conn, trans);

                        var si = vydejdata.CZMST_SI.Select(null, null, DataViewRowState.Added);
                        Database.Vydej.Update_CZMST_SI(si, conn, trans);

                        var sih = vydejdata.CZMST_SIH.Select(null, null, DataViewRowState.Added);
                        Database.Vydej.Update_CZMST_SIH(sih, conn, trans);

                        var si_bv = vydejdata.CZMST_SI_BV.Select(null, null, DataViewRowState.Added);
                        Database.Vydej.Update_CZMST_SI_BV(si_bv, conn, trans);

                        #region lokace

                        if (vydejdata.Parametry.Count > 0 && !vydejdata.Parametry.First().IsCONFIG_LOKACE_POVOLITNull() && vydejdata.Parametry.First().CONFIG_LOKACE_POVOLIT)
                        {
                            so.Write("Lokační Mechanizmus");

                            foreach (Fask.DataSets.Vydej.CZMST_SIRow sirow in vydejdata.CZMST_SI)
                            {
                                string countCommandText = "select count(*) from " + TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " where guid=@guid";
                                SqlCommand countCommand = new SqlCommand(countCommandText, conn, trans);
                                countCommand.Parameters.Clear();
                                countCommand.Parameters.AddWithValue("@guid", sirow.GUID);

                                int guidcount = (int)countCommand.ExecuteScalar();
                                if (guidcount % 2 == 0)
                                {
                                    var nazev = Database.Spolecne.GET_ITEMDESC_by_ITEMCODE(sirow.ITEMNMBR);

                                    DateTime dtnow = DateTime.Now;
                                    Fask.Server.Interfaces.Lokace.LokacePohyb pohybrow = new Fask.Server.Interfaces.Lokace.LokacePohyb();
                                    pohybrow.ITEMNMBR = sirow.ITEMNMBR;
                                    pohybrow.DOCUMENT_NUMBER = sirow.SOPNUMBE;
                                    pohybrow.POHYB_TYPE = Fask.Server.Interfaces.Lokace.TypeOfRecord.V;
                                    pohybrow.POHYB_SRC = "V";
                                    pohybrow.SOURCE = "S";      // doplneni ze serveru ...
                                    pohybrow.QTYSHPPD = sirow.QTYSHPPD;
                                    pohybrow.SERLTNUM = sirow.SERLTNUM;
                                    pohybrow.SKL_ID_SRC = sirow.IsSKL_IDNull() ? string.Empty : sirow.SKL_ID;
                                    pohybrow.SKL_ID_DST = string.Empty;
                                    pohybrow.LOCNCODE_SRC = sirow.IsLOCNCODENull() ? string.Empty : sirow.LOCNCODE;
                                    pohybrow.LOCNCODE_DST = string.Empty;
                                    pohybrow.UserID = sirow.USER_ID;
                                    pohybrow.TermID = terminal.ID;
                                    pohybrow.guid = sirow.GUID;
                                    pohybrow.Expiration = sirow.IsExpiraceNull() ? (DateTime?)null : sirow.Expirace;
                                    pohybrow.ITEMDESC = nazev;   // dotahnout nazev??  5.5.2021 TaD konečne se dotahuje nazev
                                    pohybrow.CountEntries = sirow.CountEntries;
                                    pohybrow.dateeveS = dtnow;
                                    if (sirow.IsDATEDONENull() || sirow.IsTIMEDONENull())
                                        pohybrow.dateeveS = dtnow;
                                    else
                                        pohybrow.dateeveT = DateTime.ParseExact(sirow.DATEDONE + " " + sirow.TIMEDONE, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);

                                    ProcessVydej(pohybrow, new SqlCommand(), conn, trans, new SqlDataAdapter());

                                }
                            }
                        }

                        #endregion

                        #region Baleni

						Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Vydej_Expedice_Baleni:" + Globals_V1.Konfigurace.Vydej[0].Expedice_Baleni.ToString());

                        if (Globals_V1.Konfigurace.Vydej[0].Expedice_Baleni)
                        {
							Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info,"Vydej_Expedice_Baleni: 1");

                            so.Write("Baleni");

                            #region tvorba hlavicky baleni
                            //Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter taHlavicka = new Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_HlavickaTableAdapter();
                            //taHlavicka.Connection = conn;
                            //taHlavicka.MyTransaction = trans;
                            //Guid hlavickaID = Guid.NewGuid();
                            //taHlavicka.Insert(
                            //    hlavickaID,
                            //    0,  // 0 -> nove
                            //    0,
                            //    terminal.ID,
                            //    string.Empty,   // barcode
                            //    DateTime.Now,
                            //    DateTime.Now,
                            //    sklad.ID    // TODO: poresit
                            //    );
                            #endregion


                            #region naplneni polozek baleni

                            Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_BufferTableAdapter taBuffer = null;
                            taBuffer = new Datasets.ExpediceTableAdapters.CZMST_Expedice_Baleni_BufferTableAdapter();
                            taBuffer.Connection = conn;
                            taBuffer.MyTransaction = trans;
                            // kontrola existence neni potreba, vse je v transakci ... status object by se mel postarat o to, aby nevznikly duplicity
                            SqlCommand commandItemdesc = null;

                            foreach (Fask.DataSets.Vydej.CZMST_SIRow sirow in vydejdata.CZMST_SI)
                            {
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info,"Vydej_Expedice_Baleni: ITEMNMBR" + sirow.ITEMNMBR);
                                // najiti nazvu TODO: prepsat
                                // nacteni dat typu dokladu
								string commandTextItemdesc = "select TOP 1 itemdesc from " + TABLE_FASK_ZASOBY + " where itemnmbr=@itemnmbr";
                                commandItemdesc = new SqlCommand(commandTextItemdesc, conn, trans);
                                commandItemdesc.Parameters.Add(new SqlParameter("@itemnmbr", sirow.ITEMNMBR));

                                object itemdesc = commandItemdesc.ExecuteScalar();

                                //di.QTYSHPPD = qty * (zbozi.QTYPACK > 0 ? zbozi.QTYPACK : 1);
                                //qtydmj .. zadane cislo uzivatelem
                                // pridani do tabulky Expedice_Polozky



                                taBuffer.Insert(
                                    sirow.ITEMNMBR,
                                    itemdesc != null ? (string)itemdesc : string.Empty, //string.Empty,   // TODO: dotahnout itemdesc
                                    sirow.QTYSHPPD,
                                    sirow.SERLTNUM,
                                    sirow.IsNMBRPALNull() ? string.Empty : sirow.NMBRPAL,
                                    sirow.IsVNDITNUMNull() ? string.Empty : sirow.VNDITNUM,
                                    sirow.IsCZ_CarKodNull() ? string.Empty : sirow.CZ_CarKod,
                                    sirow.GUID);

								Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Vydej_Expedice_Baleni: Insert OK");
                            }

                            #endregion

                            #region trace
                            Trac.Write(vydejdata.CZMST_SE, "ProcessVydejka, ProcessVydejState: " + processVydejState.ToString() + ", zapis o provedeni do prehlohy", tracid);
                            #endregion
                        }

                        #endregion

                        //Duvod umisteni Commitu transakce je : aby byli data v databazi ulozena pred generovanim vydejky...

						Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info,"Vydej Trancakce..." );

                        if (trans != null)
                            trans.Commit();

						
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info,"Vydej Trancakce OK" );


                        #region Definovani na typu dokladu co vznikne

                        bool vydejka = false;
                        bool faktura = false;
                        bool prevodka = false;
                        int? SkladCil = null;

                        //29.7.2025 MaR nasledujici objekt zjisti zda jde o verzi E1 nebo SQL a vrati zaznamy z tabulky OBJ z POHODA
                        var dsOBJ = Database.Pohoda.OBJ_GetDataByCislo(SOPNUMBE);
                        //29.7.2025 MaR pokud je verze E1, tak o nasledujicim typu dokladu rozhoduje dodatecny prvek RefVPrVysDoklad, jinak se preskoci a o nasledujicim typu dokladu rozhodne konfigurace
                        if (dsOBJ != null && dsOBJ.Count > 0)
                        {
                            var radek = dsOBJ.First();

                            if (!radek.IsRefVPrVysDokladNull())
                            {
                                switch (radek.RefVPrVysDoklad)
                                {
                                    case 1:
                                        prevodka = true;
                                        SkladCil = radek.IsRefVPrSkladCilNull() ? null : (int?)radek.RefVPrSkladCil;
                                        break;
                                    case 2:
                                        vydejka = true;
                                        break;
                                    case 3:
                                        faktura = true;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        #endregion



                        //29.7.2025 MaR podle konfigurace se provadeji generovani nasledujicich typu dokladu (vsechny ktere jsou ppovoleny!)

                        #region Generovat TO pozadavek

                        if (Globals_V1.Konfigurace.Vydej[0].GenerovatPrevodku_TO_Pozadavek )
                        {
                            so.Write("Generovani TO pozadavku");

                            if (vydejdata.CZMST_SI.Count > 0)
                            {
                                var groupObjednavky = vydejdata.CZMST_SI.GroupBy(y => y.SOPNUMBE);

                                if (groupObjednavky.Count() != 1)
                                    throw new Exception("Nalezeno vicero TO požadavku. Chyba?");

                                string SKL_ID = groupObjednavky.First().First().SKL_ID;
                                string cobj = groupObjednavky.First().Key;

                                #region FASK_RADY

                                Doklad typDoklad = null;

                                try
                                {
                                    #region TypDokladu

                                    Fask.Rady.DS_Rady.FASK_RADYRow row = null;

                                    try
                                    {
                                        Fask.Rady.NumericalSeries NS = new Fask.Rady.NumericalSeries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                                        row = NS.GetRadaRow(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.VyP, SKL_ID, string.Empty, false, string.Empty, string.Empty);
                                    }
                                    catch (Exception ex)
                                    {
                                        Fask.Logging.ExceptionHandler2.Handle("Fask.Module.PohodaXML", " Rada,dotaženi", ex);
                                    }

                                    if (row == null)
                                        throw new Exception("Nenalezen Definovany doc_id ve FASK_RADY v stloupci Rady_Nazev");

                                    typDoklad = new Doklad(row);


                                    #endregion

                                }
                                catch (System.Exception ex)
                                {
                                    Fask.Logging.ExceptionHandler2.Handle(ex);

                                }


                                #endregion

                                string DOC_ID = cobj.Substring(0, typDoklad.rada_Text.Length);

                                bool state = true;

                                if (DOC_ID.Trim() != typDoklad.rada_Text.Trim())
                                {
                                    string x = string.Format("Hodnota z naparsovaneho SOPNUMBER: '{0}' není shodná s: '{1}'", DOC_ID, typDoklad.rada_Text);
                                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, x);
                                    state = false;
                                }

                                string CountEntries = cobj.Remove(0, typDoklad.rada_Text.Length);

                                try
                                {
                                    int c = int.Parse(CountEntries);
                                }
                                catch (System.Exception ex)
                                {
                                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Parsovani CountEntries");
                                    throw ex;
                                }

                                string SKL_ID_Cil = string.Empty;
                                string LOCNCODE_Cil = string.Empty;

                                Fask.SQL.Database.Prodej2.GETDATA_CZMSTDI_SKLID_LOCNCODE(CountEntries, DOC_ID, out SKL_ID_Cil, out LOCNCODE_Cil);


                                if (state)
                                {
                                    if (vydejdata.Parametry == null || vydejdata.Parametry.Count != 1)
                                        throw new Exception("Paramery nenalezeny, chyba...");

                                    string vysledek = Vydej_Import_TO_Pozadavek(davka.ID.Value, vydejdata.CZMST_SI[0].SKL_ID.Trim(), vydejdata.Parametry[0], SKL_ID_Cil, LOCNCODE_Cil, typDoklad);

                                    if (vysledek != "OK")
                                    {
                                        so.StatusText = vysledek;
                                        so.Exception = true;
                                        so.Write();
                                        return so;
                                    }
                                }
                            }
                        }

                        #endregion

                        #region Generovani vydejky

                        if ((Globals_V1.Konfigurace.Vydej[0].GenerovatVydejku && itemtype == "J") || (vydejka && itemtype == "J"))
                        {

                            so.Write("Generovani vydejky");

                            if (vydejdata.CZMST_SI.Count > 0)
                            {
                                //tady odeslat do pohody ... jako novou vydejku ...

                                int userID = 0;
                                try
                                {
                                    userID = vydejdata.CZMST_SI[0].USER_ID;
                                }
                                catch { }

                                // 7.9.2018 JiS : na vydeji mohou byt pro I-Tec seskupovany objednavky do jedne davky
                                // => je treba testovat kazdou objednavku v dave, zda existuje
                                var groupObjednavky = vydejdata.CZMST_SI.GroupBy(y => y.SOPNUMBE);
                                groupObjednavky.ToList().ForEach(x => 
                                {
                                    if (!Database.Pohoda.OBJ_Exists(x.Key))
                                        throw new Exception(String.Format("Objednávka {0} nenalezena!", x.Key));
                                });

								#region 28.11.2018 Nepouží sa , prešlo se na Faktury
								//28.11.2018 Nepouží sa , prešlo se na Faktury
								// => nove je test na Rezervaci
								//      ! docasne, pokud je objednavka rezervovana, tak vyjimka
								//      ? resenim by bylo tyto objednavky "odrezervovat" a po provedeni vydeje, pokud neni uplna opet "Rezervovat"
								//List<string> listRezervovanychObjednavek = new List<string>();

								// tento try obsluhuje odrezervace/rezervace
								//groupObjednavky.ToList().ForEach(x =>
								//{
								//    if (Database.Pohoda.OBJ_Reserved(x.Key))
								//    {
								//        listRezervovanychObjednavek.Add(x.Key);
								//    }
								//});

								// Rezervovane objednavky => OdRezervovat
								//28.11.2018 Nepouží sa , prešlo se na Faktury
								//if (!Database.Pohoda.OBJ_OdRezervovat(listRezervovanychObjednavek))
								//{ // nepodarilo se OdRezervovat => problem
								//    Log.writeErrorLog("Nepodařilo se odrezervovat");
								//} 
								#endregion

								try
                                { 
                                    string vysledek = this.Vydej_ImportVydejkaPohoda(davka.ID.Value, userID, vydejdata.CZMST_SI[0].SKL_ID.Trim(), "");

                                    if (vysledek != "OK")
                                    {
                                        so.StatusText = vysledek;
                                        so.Exception = true;
                                        so.Write();
                                        return so;
                                    }

                                }
                                finally
                                {
                                    // Rezervovane objednavky => ZaRezervovat
									//28.11.2018 Nepouží sa , prešlo se na Faktury
									//if (!Database.Pohoda.OBJ_ZaRezervovat(listRezervovanychObjednavek))
									//{
									//    Log.writeErrorLog("Nepodařilo se zarezervovat");
									//}
                                }
                            } 
                        }

                        #endregion

                        #region Generovani Faktury

                        if (Globals_V1.Konfigurace.Vydej[0].GenerovatFakturu || faktura)
                        {

                            so.Write("Generovani Faktury");

                            if (vydejdata.CZMST_SI.Count > 0)
                            {
                                //tady odeslat do pohody ... jako novou vydejku ...

                                int userID = 0;
                                try
                                {
                                    userID = vydejdata.CZMST_SI[0].USER_ID;
                                }
                                catch { }

                                // 7.9.2018 JiS : na vydeji mohou byt pro I-Tec seskupovany objednavky do jedne davky
                                // => je treba testovat kazdou objednavku v dave, zda existuje
                                var groupObjednavky = vydejdata.CZMST_SI.GroupBy(y => y.SOPNUMBE);
                                groupObjednavky.ToList().ForEach(x =>
                                {
                                    if (!Database.Pohoda.OBJ_Exists(x.Key))
                                        throw new Exception(String.Format("Objednávka {0} nenalezena!", x.Key));
                                });

                                // => nove je test na Rezervaci
                                //      ! docasne, pokud je objednavka rezervovana, tak vyjimka
                                //      ? resenim by bylo tyto objednavky "odrezervovat" a po provedeni vydeje, pokud neni uplna opet "Rezervovat"
                                //List<string> listRezervovanychObjednavek = new List<string>();
                                try
                                {   // tento try obsluhuje odrezervace/rezervace
                                    //groupObjednavky.ToList().ForEach(x =>
                                    //{
                                    //    if (Database.Pohoda.OBJ_Reserved(x.Key))
                                    //    {
                                    //        listRezervovanychObjednavek.Add(x.Key);
                                    //    }
                                    //});

                                    // Rezervovane objednavky => OdRezervovat
                                    //if (!Database.Pohoda.OBJ_OdRezervovat(listRezervovanychObjednavek))
                                    //{ // nepodarilo se OdRezervovat => problem
                                    //    Log.writeErrorLog("Nepodařilo se odrezervovat");
                                    //}

                                    string vysledek = this.Vydej_ImportFakturaPohoda(davka.ID.Value, userID, vydejdata.CZMST_SI[0].SKL_ID.Trim(), "");

                                    if (vysledek != "OK")
                                    {
                                        so.StatusText = vysledek;
                                        so.Exception = true;
                                        so.Write();
                                        return so;
                                    }

                                }
                                finally
                                {
                                    // Rezervovane objednavky => ZaRezervovat
                                    //if (!Database.Pohoda.OBJ_ZaRezervovat(listRezervovanychObjednavek))
                                    //{
                                    //    Log.writeErrorLog("Nepodařilo se zarezervovat");
                                    //}
                                }
                            }
                        }

                        #endregion

                        #region Prijemka

                        if (itemtype == "P")
                        {

                            int userID = 0;
                            try
                            {
                                userID = vydejdata.CZMST_SI[0].USER_ID;
                            }
                            catch { }

                            if (Database.Pohoda.OBJ_Exists(vydejdata.CZMST_SI[0].SOPNUMBE))
                            {
                                string vysledek = this.ImportPrijemkaPohoda_Z_SI(davka.ID.Value, userID);
                                if (vysledek != "OK")
                                {
                                    so.StatusText = vysledek;
                                    so.Exception = true;
                                    so.Write();
                                    return so;
                                }
                            }

                         }

                        #endregion

                        #region Generovani Prevodky

                        if (prevodka && SkladCil.HasValue)
                        {
                            so.Write("Generovani vydejky");

                            if (vydejdata.CZMST_SI.Count > 0)
                            {
                                //tady odeslat do pohody ... jako novou vydejku ...

                                int userID = 0;
                                try
                                {
                                    userID = vydejdata.CZMST_SI[0].USER_ID;
                                }
                                catch { }

                                // 7.9.2018 JiS : na vydeji mohou byt pro I-Tec seskupovany objednavky do jedne davky
                                // => je treba testovat kazdou objednavku v dave, zda existuje
                                var groupObjednavky = vydejdata.CZMST_SI.GroupBy(y => y.SOPNUMBE);
                                groupObjednavky.ToList().ForEach(x =>
                                {
                                    if (!Database.Pohoda.OBJ_Exists(x.Key))
                                        throw new Exception(String.Format("Objednávka {0} nenalezena!", x.Key));
                                });


                                try
                                {

                                    var dt_si = Database.Vydej.GETDATA_CZMSTSI_DS_GroupBy_CountEntries(davka.ID.Value);

                                    #region FASK_RADY

                                    Doklad typDoklad = null;

                                    try
                                    {
                                        string SKL_ID = dt_si.First().SKL_ID;

                                        #region TypDokladu

                                        Fask.Rady.DS_Rady.FASK_RADYRow row = null;

                                        try
                                        {
                                            Fask.Rady.NumericalSeries NS = new Fask.Rady.NumericalSeries(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                                            row = NS.GetRadaRow(Fask.Rady.TypDB.SQL, Fask.Rady.Modul.VyP, SKL_ID, string.Empty, false, string.Empty, string.Empty);
                                        }
                                        catch (Exception ex)
                                        {
                                            Fask.Logging.ExceptionHandler2.Handle("Fask.Module.PohodaXML", " Rada,dotaženi", ex);
                                        }

                                        if (row == null)
                                            throw new Exception("Nenalezen Definovany doc_id ve FASK_RADY v stloupci Rady_Nazev");

                                        typDoklad = new Doklad(row);


                                        #endregion

                                    }
                                    catch (System.Exception ex)
                                    {
                                        Fask.Logging.ExceptionHandler2.Handle(ex);

                                    }


                                    #endregion

                                    string vysledek = this.Vydej_ImportPrevodku_zOBJ(dt_si, typDoklad, SkladCil.Value.ToString(), "MES Import převodky");

                                    if (vysledek != "OK")
                                    {
                                        so.StatusText = vysledek;
                                        so.Exception = true;
                                        so.Write();
                                        return so;
                                    }

                                }
                                finally
                                {
                                }
                            }


                        }
                        else
                        {
                            if (prevodka && !SkladCil.HasValue)
                                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Fatal, "Nenastaven cílovy sklad pro Převodku z vydeje z předlohou");
                        }

                        #endregion

                        #region Action after data processed
                        //if (aDP_Action_Asynch)
                        //{
                        //    //System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(AfterProcessedAction));
                        //    //thread.Start(vydejdata.CZMST_SE[0].CountEntries);
                        //}
                        //else
                        //{
                        //    if (!AfterProcessedAction(vydejdata.CZMST_SE[0].CountEntries, xConnection1, xTrans1))
                        //    {
                        //        throw new Exception("Vydej: AfterProcessAction not succeded on countentries=" + vydejdata.CZMST_SE[0].CountEntries);
                        //    }
                        //}
                        #endregion
                    }

                    //Log.writeOKData(vydejdata, "vydej");

                }


            }
            catch (Exception ex)
            {

				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(ProcessVydejState: " + processVydejState.ToString() + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();

                }
                catch (Exception exTrans)
                {
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(ProcessVydejState: " + processVydejState.ToString() + ")");
					Fask.Logging.ExceptionHandler2.Handle(exTrans);
                }
                throw ex;
            }
            finally
            {
                if (conn != null && (conn.State & ConnectionState.Open) == ConnectionState.Open)
                    conn.Close();
            }

            #region Action after data processed
            //if (aDP_Action_Asynch)
            //{
            //    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(AfterProcessedAction));
            //    //thread.Start(vydejdata.CZMST_SE[0].CountEntries);
            //    thread.Start(countentries);
            //}
            //else
            //{
            //    //return AfterProcessedAction(vydejdata.CZMST_SE[0].CountEntries);
            //    //if (!AfterProcessedAction(countentries))
            //    //{
            //    //
            //    //    processStatus.StatusText = "chyba";
            //    //    processStatus.Exception = true;
            //    //
            //    //    return processStatus;
            //    //}
            //}
            #endregion

            so.SetOK();
            //so.StatusText = "OK";
            //so.Finished = true; //Pridano kvuli čtečke aby lepe vyhodnocovala
            //so.Exception = false;

            return so;

        }

        private string Vydej_Import_TO_Pozadavek(int countEntries, string SKL_ID, Fask.DataSets.Vydej.ParametryRow rowParam, string SKL_ID_Cil, string LOCNCODE_Cil, Doklad typDoklad)
        {

            SqlTransaction trans = null;
            SqlConnection conn = null;

            try
            {

                //Transakce
                //vramci transakce se provede příjem do Lok. Mechanizmu

                Globals_V1.LoadConfiguration();

                var dt_si = Database.Vydej.GETDATA_CZMSTSI_DS_GroupBy_CountEntries(countEntries);

                conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                conn.Open();
                trans = conn.BeginTransaction();


                if (!rowParam.IsCONFIG_LOKACE_POVOLITNull() && rowParam.CONFIG_LOKACE_POVOLIT)
                {
                    foreach (Datasets.Vydej.CZMST_SIRow sirow in dt_si)
                    {
                        string countCommandText = "select count(*) from " + TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " where guid=@guid";
                        SqlCommand countCommand = new SqlCommand(countCommandText, conn, trans);
                        countCommand.Parameters.Clear();
                        countCommand.Parameters.AddWithValue("@guid", sirow.GUID);

                        int guidcount = (int)countCommand.ExecuteScalar();

                        string ITEMNMBR_Cil = sirow.ITEMNMBR;
                        string ITEMDESC_Cil = string.Empty;

                        if (!string.IsNullOrEmpty(SKL_ID_Cil) && (sirow.SKL_ID.Trim() != SKL_ID_Cil.Trim()))
                        {
                            Database.Pohoda.Get_MapingLokMechID(sirow.ITEMNMBR, sirow.SKL_ID, SKL_ID_Cil, out ITEMNMBR_Cil, out ITEMDESC_Cil);
                        }

                        // 18.2.2021 ... tohle je velka otazka... co s tym??
                        //22.2.2021 dle JiS má byt guid lichý počet a musí byt aspon jeden
                        if (guidcount > 0 && (guidcount % 2 != 0))
                        {
                            DateTime dtnow = DateTime.Now;
                            Fask.Server.Interfaces.Lokace.LokacePohyb pohybrow = new Fask.Server.Interfaces.Lokace.LokacePohyb();
                            pohybrow.ITEMNMBR = ITEMNMBR_Cil; // sirow.ITEMNMBR;
                            pohybrow.DOCUMENT_NUMBER = sirow.SOPNUMBE;
                            pohybrow.POHYB_TYPE = Fask.Server.Interfaces.Lokace.TypeOfRecord.P;
                            pohybrow.POHYB_SRC = "P";
                            pohybrow.SOURCE = "S";      // doplneni ze serveru ...
                            pohybrow.QTYSHPPD = sirow.QTYSHPPD;
                            pohybrow.SERLTNUM = sirow.SERLTNUM;
                            pohybrow.SKL_ID_SRC = SKL_ID_Cil;
                            pohybrow.SKL_ID_DST = string.Empty;
                            pohybrow.LOCNCODE_SRC = LOCNCODE_Cil;
                            pohybrow.LOCNCODE_DST = string.Empty;
                            pohybrow.UserID = sirow.USER_ID;
                            pohybrow.TermID = sirow.ID_TERMINAL;
                            pohybrow.guid = sirow.GUID;
                            pohybrow.Expiration = sirow.IsExpiraceNull() ? (DateTime?)null : sirow.Expirace;
                            pohybrow.ITEMDESC = ITEMDESC_Cil; // string.Empty;   // dotahnout nazev??
                            pohybrow.CountEntries = sirow.CountEntries;
                            pohybrow.dateeveS = dtnow;
                            if (sirow.IsDATEDONENull() || sirow.IsTIMEDONENull())
                                pohybrow.dateeveT = dtnow;
                            else
                                pohybrow.dateeveT = DateTime.ParseExact(sirow.DATEDONE + " " + sirow.TIMEDONE, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);

                            ProcessPrijem(pohybrow, new SqlCommand(), conn, trans, new SqlDataAdapter());

                        }
                    }
                }

                //Tvorba prevodky v IS POHODA

                var stav = Vydej_ImportPrevodku(dt_si, typDoklad, SKL_ID_Cil, "MST Vytvořeno z TO požadavku");

                if (stav != "OK")
                    throw new Exception(stav);


                if (trans != null)
                    trans.Commit();


                return "OK";

            }
            catch (System.Exception ex)
            {

                try
                {
                    if (trans != null)
                        trans.Rollback();

                }
                catch (Exception exTrans)
                {
                    Fask.Logging.ExceptionHandler2.Handle(exTrans);
                }

                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        public StatusObject TEST_ImportVydejka_Do_IS(int countEntries, int userID, string SKL_ID, string note)
        {
            StatusObject so = new StatusObject();
            try
            {

                string vysledek = this.Vydej_ImportVydejkaPohoda(countEntries, userID, SKL_ID, note);

                if (vysledek != "OK")
                {
                    so.StatusText = vysledek;
                    so.Exception = true;
                    so.Write();
                    return so;
                }
                else
                    so.SetOK();

            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return so;
        }

        #region IVydej Members


        public StatusObject TEST_ImportFaktura_Do_IS(int countEntries, int userID, string SKL_ID, string note)
        {
            StatusObject so = new StatusObject();
            try
            {

                string vysledek = this.Vydej_ImportFakturaPohoda(countEntries, userID, SKL_ID, note);

                if (vysledek != "OK")
                {
                    so.StatusText = vysledek;
                    so.Exception = true;
                    so.Write();
                    return so;
                }
                else
                    so.SetOK();

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return so;
        }

        #endregion


        public bool Vydej_AfterProcessedAction(Fask.Server.Interfaces.Classes.Davka davka)
        {
            // Akce po provedeni ulozeni dat ...
            // Momentalne neni reseno ...
            //throw new NotImplementedException();
            return true;
        }

        #region IVydej Members

        public System.Data.DataSet Vydej_Detail(Fask.Server.Interfaces.Classes.Objednavka objednavka)
        {
            try
            {
                Globals_V1.LoadConfiguration();

                if (Globals_V1.Konfigurace.Vydej[0].VydejkaDetail == string.Empty)
                    return null;

                System.Data.SqlClient.SqlConnection dbconnection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();
                dbCommand.CommandType = (CommandType)Enum.Parse(typeof(CommandType), Globals_V1.Konfigurace.Vydej[0].VydejkaDetail_CommandType);


                if (dbCommand.CommandType == CommandType.StoredProcedure)
                {
                    dbCommand.CommandText = Globals_V1.Konfigurace.Vydej[0].VydejkaDetail;
                    dbCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(Globals_V1.Konfigurace.Vydej[0].VydejkaDetail_paramName, objednavka.ID));
                }
                else if (dbCommand.CommandType == CommandType.Text)
                {
                    dbCommand.CommandText = "Select * from " + Globals_V1.Konfigurace.Vydej[0].VydejkaDetail + " where " + Globals_V1.Konfigurace.Vydej[0].VydejkaDetail_paramName + "='" + objednavka.ID + "'";
                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + dbCommand.CommandType.ToString());
                }

                dbCommand.Connection = dbconnection;

                DataSet data = new DataSet();
                System.Data.SqlClient.SqlDataAdapter dbda = new System.Data.SqlClient.SqlDataAdapter(dbCommand);
                dbda.Fill(data);
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public System.Data.DataSet Vydej_DetailDavka(Fask.Server.Interfaces.Classes.Davka davka)
        {
            try
            {
                Globals_V1.LoadConfiguration();

                if (Globals_V1.Konfigurace.Vydej[0].VydejkaDetailDavka == string.Empty)
                    return null;

                System.Data.SqlClient.SqlConnection dbconnection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();
                dbCommand.CommandType = (CommandType)Enum.Parse(typeof(CommandType), Globals_V1.Konfigurace.Vydej[0].VydejkaDetailDavka_CommandType);


                if (dbCommand.CommandType == CommandType.StoredProcedure)
                {
                    dbCommand.CommandText = Globals_V1.Konfigurace.Vydej[0].VydejkaDetailDavka;
                    dbCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(Globals_V1.Konfigurace.Vydej[0].VydejkaDetailDavka_paramName, davka.ID));
                }
                else if (dbCommand.CommandType == CommandType.Text)
                {
                    dbCommand.CommandText = "Select * from " + Globals_V1.Konfigurace.Vydej[0].VydejkaDetailDavka + " where " + Globals_V1.Konfigurace.Vydej[0].VydejkaDetailDavka_paramName + "='" + davka.ID + "'";
                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + dbCommand.CommandType.ToString());
                }


                dbCommand.Connection = dbconnection;

                DataSet data = new DataSet();
                System.Data.SqlClient.SqlDataAdapter dbda = new System.Data.SqlClient.SqlDataAdapter(dbCommand);
                dbda.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataSet Vydej_DetailPolozka(Item item)
        {
            try
            {
                Globals_V1.LoadConfiguration();

                string vydejkadetailpolozka = Globals_V1.Konfigurace.Vydej[0].vydejkadetailpolozka;
                string vydejkadetailpolozka_paramname = Globals_V1.Konfigurace.Vydej[0].vydejkadetailpolozka_paramname;


                if (vydejkadetailpolozka == string.Empty)
                    return null;

                System.Data.SqlClient.SqlConnection xconn = null;
                System.Data.SqlClient.SqlCommand xcomm = null;
                xconn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                xcomm = new SqlCommand();
                xcomm.CommandType = (CommandType)Enum.Parse(typeof(CommandType), Globals_V1.Konfigurace.Vydej[0].vydejkadetailpolozka_CommandType);
                if (xcomm.CommandType == CommandType.StoredProcedure)
                {
                    xcomm.CommandText = vydejkadetailpolozka;
                    xcomm.Parameters.Add(new SqlParameter(vydejkadetailpolozka_paramname, item.ID));
                }
                else if (xcomm.CommandType == CommandType.Text)
                {
                    xcomm.CommandText = "Select * from " + vydejkadetailpolozka + " where " + vydejkadetailpolozka_paramname + "='" + item.ID + "'";
                }

                else
                {
                    throw new Exception("Neznámý typ příkazu: " + xcomm.CommandType.ToString());
                }

                xcomm.Connection = xconn;

                DataSet data = new DataSet();
                System.Data.SqlClient.SqlDataAdapter xda = new SqlDataAdapter();
                xda.SelectCommand = xcomm;
                xda.Fill(data);

                return data;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (VydejkaDetail:" + item.ID + ")");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                throw ex;
            }
        }

        #endregion

        public StatusObject Vydej_Finish_Vydejka(Davka davka, Terminal terminal, string password)
        {
            StatusObject so = new StatusObject();
            so.StatusText = "";

            try
            {
                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    throw new Exception("Vydej_Finish_Vydejka, chyba nacteni konfigurace: " + pom);

                //Datasets.VydejTableAdapters.CZMST_SETableAdapter se_ta = null;
                //se_ta = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
                //se_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                //se_ta.UpdateCzDosloByCountEntries((byte)(terminal.ID + 100), davka.ID.Value);
                Database.Vydej.CZMSTSE_UPDATE_CZDOSLO_By_CountEntries((byte)(terminal.ID + 100), davka.ID.Value);

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

        public StatusObject Vydej_Storno_Vydejka(Davka davka, Terminal terminal, string password)
        {
            StatusObject so = new StatusObject();
            string pom = string.Empty;
            pom = Globals_V1.LoadConfiguration();
            if (pom != "OK")
                throw new Exception("Vydej_Storno_Vydejka, chyba nacteni konfigurace: " + pom);

            if (password != Globals_V1.Konfigurace.Vydej[0].HesloStornoVydejka)
            {
                so.StatusText = "Zadané heslo je špatně!";
                so.Exception = true;
                return so;
            }

            //Datasets.VydejTableAdapters.CZMST_SITableAdapter si_ta = null;
            //Datasets.VydejTableAdapters.CZMST_SETableAdapter se_ta = null;

            try
            {
                //si_ta = new Datasets.VydejTableAdapters.CZMST_SITableAdapter();
                //si_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                //if (si_ta.GetDataByCountEntries(davka.ID.Value).Count > 0)
                if (Database.Vydej.GETDATA_CZMSTSI_By_CountEntries(davka.ID.Value).Count > 0)
                {
                    so.StatusText = "Danou dávku nelze zrušit! Existují nasnímané položky!";
                    so.Exception = true;
                    return so;
                }

                //se_ta = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
                //se_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                //se_ta.UpdateCzDosloByCountEntries(100, davka.ID.Value);
                Database.Vydej.CZMSTSE_UPDATE_CZDOSLO_By_CountEntries(100, davka.ID.Value);

                so.StatusText = "OK";
                return so;

            }
            finally
            {
                //if (si_ta != null && si_ta.Connection.State == System.Data.ConnectionState.Open)
                //    si_ta.Connection.Close();


                //if (se_ta != null && se_ta.Connection.State == System.Data.ConnectionState.Open)
                //    se_ta.Connection.Close();
            }


            //return so;
        }

        #endregion

        #region Metody komunikace s Pohodou ...

        /// <summary>
        /// generuje davku z faktury a ulozi ji do czmst_se... 
        /// </summary>
        /// <param name="objednavka">nese id(cislo) dokladu(faktury)</param>
        /// <returns>OK = vse dobre, jinak nejaky jiny text k pripadnemu zobrazeni na terminalu</returns>
        public string ExportVydejkaPohoda_Z_Faktury(
            Fask.Server.Interfaces.Classes.Objednavka objednavka
            ,Fask.Server.Interfaces.Classes.Sklad sklad
            )
        {
            try
            {

                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(XML.MST_Pohoda._export_faktura_vydana + ".xml");

                if (String.IsNullOrEmpty(objednavka.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Vydej.CreateRequest_FakturaVydana_XML(filename, objednavka))
                    return "CHYBA";

                //pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, objednavka, new Fask.Server.Interfaces.Classes.User());
                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                return Vydej.LoadResponse_FakturaVydana_XML(responsefilename, objednavka);
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
        /// generuje davku z faktury a ulozi ji do czmst_se... 
        /// </summary>
        /// <param name="objednavka">nese id(cislo) dokladu(faktury)</param>
        /// <returns>OK = vse dobre, jinak nejaky jiny text k pripadnemu zobrazeni na terminalu</returns>
            public string ExportVydejkaPohoda_Z_Faktury_DirectAccess2DB(
            Fask.Server.Interfaces.Classes.Objednavka objednavka
            , Fask.Server.Interfaces.Classes.Sklad sklad
            )
        {
            try
            {

                if (String.IsNullOrEmpty(objednavka.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

				Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
				if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
					ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				}

                // 1) nacist hlavicku faktury
                // 2) nacist polozky faktury
                // 3) nacist doplnujici informace k polozkam
                // 4) ulozit do predlohy vydeje
                // 5) verifikace
                // - konec - 
                // pri chybe vratit informaci o chybe ...

                Datasets.DatabasePohoda ds_pohoda = new Datasets.DatabasePohoda();

                //Datasets.DatabasePohodaTableAdapters.FakturaCisloTableAdapter ta_pohoda_fakturacislo = new Datasets.DatabasePohodaTableAdapters.FakturaCisloTableAdapter();

                //ta_pohoda_fakturacislo.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

                // nacteni faktury a polozek faktury
                //ta_pohoda_fakturacislo.Fill(ds_pohoda.FakturaCislo, objednavka.ID);
                //ta_pohoda_fakturacislo.FillByNoMSTParams(ds_pohoda.FakturaCislo, objednavka.ID);
				Database.Pohoda.FakturaCislo_FillByNoMSTParams(ds_pohoda.FakturaCislo, objednavka.ID);


                // dotazeni parametru pro locncode (vychozi lokaci ..)
				//Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter SKzParametryTableAdapter = new Datasets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();
				//SKzParametryTableAdapter.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
                
                // *************************************************
                // naplneni do czmst_se ... 
                // *************************************************
                //Datasets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new Datasets.VydejTableAdapters.CZMST_SETableAdapter();
                //CZMST_SETableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                
                Datasets.Vydej.CZMST_SEDataTable seTable = new Datasets.Vydej.CZMST_SEDataTable();
                Datasets.Vydej.CZMST_SERow seRow = null;

                int cisloDavky = 0;

				int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
				int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;

                foreach (var invoiceItem in ds_pohoda.FakturaCislo)
                {
                    // Pokud to neni skladova polozka, tak ji neresit ...
					// \TODO : otestovat, zda to takto je ... => tedy test na NULL hodnotu...
                    if ((invoiceItem == null) || (invoiceItem.IsSKz_IDNull()))
                        continue;

                    // dotazeni vychozi lokace pro zasobu
                    string locncodeDeafult = string.Empty;
                    if (invoiceItem != null)
                    {
                        if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                        {
                            //Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = SKzParametryTableAdapter.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, invoiceItem.FApol_ID);
							Datasets.DatabasePohoda.SKzParametryDataTable skzParametry_table = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, invoiceItem.FApol_ID);
							Datasets.DatabasePohoda.SKzParametryRow skzParametry_row = skzParametry_table.Count > 0 ? skzParametry_table[0] : null;
                            if (skzParametry_row != null)
                            {
                                locncodeDeafult = skzParametry_row.IspValTextNull() ? string.Empty : skzParametry_row.pValText.Trim();
                            }
                        }
                    }

                    seRow = seTable.NewCZMST_SERow();

                    seRow.ITEMDESC = invoiceItem.FApol_SText; // nazev polozky => z xml <inv:text>
                    if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
                    {
						seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
                    }

                    seRow.ITEMNMBR = invoiceItem.SKz_ID.ToString(); // cislo polozky => z xml <inv:stockItem><typ:stockItem><typ:id>
                    //22.7.2025 MaR
                    seRow.ITEMTYPE = "J"; //vydej s predlohou
                    //seRow.ITEMCODE = ""; // ? ITEMCODE neni???
					seRow.CountEntries = cisloDavky; // \TODO : ??? nove cislo davky ... 

                    //seRow.CZ_CarKod = typPLU == null ? string.Empty : typPLU.Value; //carovy kod => dotahnout z xml PLU
                    seRow.CZ_CarKod = invoiceItem.IsSKz_IDSNull() ? string.Empty : invoiceItem.SKz_IDS.Trim();
                    seRow.CZ_DatVyr_Delka = 0; //?
                    seRow.CZ_DatVyr_Track = 0; //?
                    seRow.CZ_Doslo = 0; // pripraveno pro zpracovani
                    seRow.CZ_SerNum_Delka = 0; //? delka serioveho cisla 
                    //seRow.CZ_SerNum_Track = 0; //? sledovat na SN/Sarze => je sledovano na seriove cislo => zjistit v Pohoda
                    //seRow.CZ_SerNum_Track = (byte)(skz_row == null ? 0 : skz_row.RelSKzVC);
                    
                    
                    //TaD 19.9.2018 Uprava prace s serltnum
                    //seRow.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack(
                    //    (invoiceItem == null) || (invoiceItem.IsSKz_RelSKzVCNull()) ? (int?)null : (int?)invoiceItem.SKz_RelSKzVC
                    //    );

					//if (Properties.Settings.Default.POHODA_E1)
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
                    //seRow.DEX_ROW_ID
                    seRow.LOCNCODE = locncodeDeafult;
                    seRow.Note = invoiceItem.IsSKz_STextNull() ? " " : invoiceItem.SKz_SText;  //""; //? poznamka 
                    seRow.ORD = invoiceItem.FApol_ID; //int.Parse(invoiceItem.Element(inv + "id").Value.Trim()); //? poradi polozky na dokladu - pro pohoda id zaznamu v tabulce polozek <inv:id>
                    seRow.PRINTED = 0;
                    seRow.PRIORITY = 3; //? priorita ... 
					seRow.QTYPACK = 0; // \TODO : rozpad na varianty baleni dle car kodu ... 
                    seRow.QTYPAL = 0; //? palety neresime ... ???
                    seRow.QTYSHPPD = Convert.ToDecimal(invoiceItem.FApol_Mnozstvi); //decimal.Parse(invoiceItem.Element(inv + "quantity").Value.Trim(), System.Globalization.NumberFormatInfo.InvariantInfo); // mnozstvi k vydeji => <inv:quantity>
                    seRow.SKL_ID = invoiceItem.Skz_RefSklad.ToString(); //stockItem.Element(typ + "store").Element(typ + "id").Value.Trim(); //? id skladu => <inv:stockItem><typ:store><typ:id> : jde o identifikator skladu
                    seRow.SOPNUMBE = objednavka.ID;
                    seRow.TYPEPAL = ""; //? neresime palety ...
                    seRow.USERID = 0;
                    seRow.VNDDOCNM = ""; //? co by tady melo byt >>>
                    seRow.VNDITNUM = invoiceItem.IsSKz_EANNull() ? string.Empty : invoiceItem.SKz_EAN.Trim(); //typEAN == null ? string.Empty : typEAN.Value; //carovy kod zbozi ... => dotahnout z xml 
                    if (invoiceItem != null && !invoiceItem.IsFApol_MJNull())
                    {
                        seRow.MJ = invoiceItem.FApol_MJ;
                        if (seRow.MJ.Length > MJ_MaxLength)
                        {
							seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
                        }
                    }
                    else
                    {
                        seRow.MJ = string.Empty;
                    }

                    seRow.CZ_REZ1_Track = 0;
                    seRow.CZ_REZ2_Track = 0;
                    seRow.ITEMCODE = invoiceItem.IsSKz_IDSNull() ? string.Empty : invoiceItem.SKz_IDS.Trim();
                    seRow.SetWEIGHTNull();
                    seRow.SetRealization_StartNull();
                    seRow.SetRealization_StopNull();
                    seRow.CZ_Expirace_Track = 0;


                    //MaR 11.12.2024 pridani dat na J
                    seRow.ITEMTYPE = "J";


                    // Nacist vychozi lokaci ...
                    // az tady, protoze musi mit nastaveny skl_id a itemnmbr...
                    Vydej.LocncodeFindAlgorithmVychozi(seRow);

                    //vlozit do se ...
                    seTable.AddCZMST_SERow(seRow);

                    if (Globals_V1.Konfigurace.Vydej[0].MerneJednotky_DotahovatDalsiVarianty)
                    {
                        // MJ2 a qtypack (pokud existuje)
						// \TODO: pridat mernou jednotku
                        if (invoiceItem != null && !invoiceItem.IsSkz_MJ2Null() && !invoiceItem.IsSkz_MJ2KoefNull())
                        {
                            // kopie zaznamu
                            Datasets.Vydej.CZMST_SERow seRow2 = seTable.NewCZMST_SERow();
                            seRow2.ItemArray = seRow.ItemArray.Clone() as object[];
                            seRow = seRow2;

                            seRow.QTYPACK = (decimal)invoiceItem.Skz_MJ2Koef;
                            seRow.MJ = invoiceItem.Skz_MJ2;
                            if (seRow.MJ.Length > MJ_MaxLength)
                            {
								seRow.MJ = seRow.MJ.Remove(MJ_MaxLength);
                            }

                            seTable.AddCZMST_SERow(seRow);
                        }

                        // MJ3 a qtypack (pokud existuje)
                        if (invoiceItem != null && !invoiceItem.IsSkz_MJ3Null() && !invoiceItem.IsSkz_MJ3KoefNull())
                        {
                            // kopie zaznamu
                            Datasets.Vydej.CZMST_SERow seRow3 = seTable.NewCZMST_SERow();
                            seRow3.ItemArray = seRow.ItemArray.Clone() as object[];
                            seRow = seRow3;

                            seRow.MJ = invoiceItem.Skz_MJ3;
                            seRow.QTYPACK = (decimal)invoiceItem.Skz_MJ3Koef;
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
                catch (Exception ex)
                {
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "ExportVydejkaPohoda_Z_Faktury_DirectTSQL");
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
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(Vydejka: " + objednavka.ID + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);

                throw ex;
                //return ex.Message;
            }


        }

        /// <summary>
        /// generuje davku z prevodky a ulozi ji do czmst_se... 
        /// </summary>
        /// <param name="objednavka">nese id(cislo) dokladu(prevodky)</param>
        /// <returns>OK = vse dobre, jinak nejaky jiny text k pripadnemu zobrazeni na terminalu</returns>
        public string ExportVydejkaPohoda_Z_Prevodka(
            Fask.Server.Interfaces.Classes.Objednavka doklad
            , Fask.Server.Interfaces.Classes.Sklad sklad
            )
        {
            try
            {
                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(XML.MST_Pohoda._export_prevodka + ".xml");

                if (String.IsNullOrEmpty(doklad.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;
                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    return pom;

                if (!Vydej.CreateRequest_Prevodka_XML(filename, doklad))
                    return "CHYBA";

                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                return Vydej.LoadResponse_Prevodka_XML(responsefilename, true, doklad);
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(Vydejka z prevodky: " + doklad.ID + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);

                throw ex;
                //return ex.Message;
            }
        }

        /// <summary>
        /// generuje davku z vydejky a ulozi ji do czmst_se... 
        /// </summary>
        /// <param name="objednavka">nese id(cislo) dokladu(vydejky)</param>
        /// <returns>OK = vse dobre, jinak nejaky jiny text k pripadnemu zobrazeni na terminalu</returns>
        public string ExportVydejkaPohoda_Z_Vydejky(
            Fask.Server.Interfaces.Classes.Objednavka objednavka
            , Fask.Server.Interfaces.Classes.Sklad sklad
            )
        {
            try
            {
                
                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(XML.MST_Pohoda._export_vydejka + ".xml");

                if (String.IsNullOrEmpty(objednavka.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Vydej.CreateRequest_Vydejka_XML(filename, objednavka))
                    return "CHYBA";

                //pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, objednavka, new Fask.Server.Interfaces.Classes.User());
                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                return Vydej.LoadResponse_Vydejka_XML(responsefilename, objednavka);
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
        /// generuje davku z prodejky a ulozi ji do czmst_se... 
        /// </summary>
        /// <param name="objednavka">nese id(cislo) dokladu(prodejky)</param>
        /// <returns>OK = vse dobre, jinak nejaky jiny text k pripadnemu zobrazeni na terminalu</returns>
        public string ExportVydejkaPohoda_Z_Prodejky(
            Fask.Server.Interfaces.Classes.Objednavka objednavka
            , Fask.Server.Interfaces.Classes.Sklad sklad
            )
        {
            try
            {
                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(XML.MST_Pohoda._export_prodejka + ".xml");

                if (String.IsNullOrEmpty(objednavka.ID.Trim()))
                    throw new Exception("Číslo dokladu nesmí být prázdné");

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Vydej.CreateRequest_Prodejka_XML(filename, objednavka))
                    return "CHYBA";

                //pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, objednavka, new Fask.Server.Interfaces.Classes.User());
                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                return Vydej.LoadResponse_Prodejka_XML(responsefilename, objednavka);
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(Prodejka: " + objednavka.ID + ")");
				Fask.Logging.ExceptionHandler2.Handle(ex);

                throw ex;
                //return ex.Message;
            }
        }


        public string Vydej_ImportVydejkaPohoda( int countEntries, int userID, string SKL_ID, string note )
        {
            try
            {

                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(vydej_import_vydejka);

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

                if (!Vydej.CreateRequest_Import_Vydejka_XML_NEW( filename, countEntries, SKL_ID, note))
                    return "CHYBA";



                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                //bool saveToDB = true;

                //Fask.Server.Interfaces.Classes.User user = new Fask.Server.Interfaces.Classes.User();
                //user.ID = userID;

                //pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, new Fask.Server.Interfaces.Classes.Objednavka(), user);

                if (pom != "OK")
                    return pom;

                //Parovani a update objednavka/prijemka v pohoda ... 
                pom = Vydej.LoadVydej_ImportVydejka_ResponseXML(responsefilename, countEntries, SKL_ID);

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Vydej_ImportFakturaPohoda(int countEntries, int userID, string SKL_ID, string note)
        {
            try
            {
                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(vydej_import_faktura);

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

                if (!Vydej.CreateRequest_Import_Faktura_XML_NEW(filename, countEntries, SKL_ID, note))
                    return "CHYBA";



                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                //bool saveToDB = true;

                //Fask.Server.Interfaces.Classes.User user = new Fask.Server.Interfaces.Classes.User();
                //user.ID = userID;

                //pom = XML.MST_Pohoda.ZpracovaniUlozeniVsechDatDoDB(filename, saveToDB, new Fask.Server.Interfaces.Classes.Objednavka(), user);

				//if (pom != "OK")
				//    return pom;

                //Parovani a update objednavka/prijemka v pohoda ... 
                pom = Vydej.LoadVydej_ImportFaktura_ResponseXML(responsefilename, countEntries, SKL_ID);

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string Vydej_ImportPrevodku(Datasets.Vydej.CZMST_SIDataTable dtSI, Doklad typDoklad, string SKL_ID_Cil, string text)
        {
            try
            {
                
                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(vydej_import_prevodku);
                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Vydej.CreateRequest_Pre_XML(filename, "FASK Import XML", dtSI, typDoklad, SKL_ID_Cil, text))
                    return "CHYBA";

                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                pom = Vydej.LoadResponse_Pre_XML(responsefilename);

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string Vydej_ImportPrevodku_zOBJ(Datasets.Vydej.CZMST_SIDataTable dtSI, Doklad typDoklad, string SKL_ID_Cil, string text)
        {
            try
            {

                var filename = XML.MST_Pohoda.FilenameCompose_FullPath(vydej_import_prevodku);
                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    return pom;

                if (!Vydej.CreateRequest_Pre_zOBJ_XML(filename, "FASK Import XML", dtSI, typDoklad, SKL_ID_Cil, text))
                    return "CHYBA";

                string responsefilename;
                if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                pom = Vydej.LoadResponse_Pre_zOBJ_XML(responsefilename);

                return pom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Online metody
        public Fask.Server.Interfaces.DataSets.Vydej_Items_Online Vydej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum)
        {
            Fask.Server.Interfaces.DataSets.Vydej_Items_Online vydej_Items_Online = new Server.Interfaces.DataSets.Vydej_Items_Online();

            if (string.IsNullOrEmpty(itemnmbr) && string.IsNullOrEmpty(serltnum))
                throw new Exception("Nelze vyhledat bez zadané šarže anebo ID položky!");

            var ds = Database.Pohoda.Get_Vydej_Online_GetMaterial(itemnmbr, skl_id, serltnum);

            if (ds != null && ds.Items.Count > 0)
            {

                foreach (var row in ds.Items)
                {
                    var radek = vydej_Items_Online.Items.NewItemsRow();

                    radek.Index = row.IndexXX;
                    radek.Itemnmbr = row.IsItemnmbrNull() ? string.Empty : row.Itemnmbr;
                    radek.Itemdesc = row.IsItemdescNull() ? string.Empty : row.Itemdesc;
                    radek.Skl_Id = row.IsSkl_IdNull() ? string.Empty : row.Skl_Id;
                    radek.Locncode = row.IsLocncodeNull() ? string.Empty : row.Locncode;
                    radek.Qty = row.Qty;
                    radek.Serltnum = row.IsSerltnumNull() ? string.Empty : row.Serltnum;

                    if (!row.IsExpirationNull())
                    {
                        radek.Expiration = row.Expiration;
                    }
                    else
                    {
                        radek.SetExpirationNull();
                    }

                    if (!row.IsPrijemNull())
                    {
                        radek.Prijem = row.Prijem;
                    }
                    else
                    {
                        radek.SetPrijemNull();
                    }

                    vydej_Items_Online.Items.AddItemsRow(radek);

                }

            }

            return vydej_Items_Online;
        }

        public StatusOverExpirace Vydej_Online_Expirace_Verify(string itemnmbr, string sklad_id, string serltnum, DateTime? expirace)
        {
            Globals_V1.LoadConfiguration();


            double pocetdnidoexpirace = Globals_V1.Konfigurace.Vydej[0].IsFEFO_FIFO_Expirace_Po_PocetDniNull() ? 30 : Globals_V1.Konfigurace.Vydej[0].FEFO_FIFO_Expirace_Po_PocetDni;

            // TODO : implementovat 
            // vratit status expirace, pokud je ji mozne vydat ... 
            if (!expirace.HasValue)
                return new StatusOverExpirace() { State = StatusOverExpiraceState.OK, Message = string.Empty };

            if (expirace.Value > (DateTime.Now.AddDays(pocetdnidoexpirace)))
                return new StatusOverExpirace() { State = StatusOverExpiraceState.OK, Message = string.Empty };

            if (expirace.Value > DateTime.Now)
            {

                string msg = string.Empty;

                msg += string.Format("Šarže bude expirovat dříve než za {0} dní!", pocetdnidoexpirace);

                DateTime NowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                double rozdil = (expirace.Value - NowDate).TotalDays;

                msg += Environment.NewLine;
                msg += string.Format("Expirovat bude za {0} dní!", rozdil);


                return new StatusOverExpirace() { State = StatusOverExpiraceState.WARNING, Message = msg };
            }

            if (expirace.Value == DateTime.Now)
            {

                string msg = string.Empty;

                msg += string.Format("Šarže expiruje dnes!");
                return new StatusOverExpirace() { State = StatusOverExpiraceState.WARNING, Message = msg };
            }

            return new StatusOverExpirace() { State = StatusOverExpiraceState.WARNING, Message = $"Šarže je již expirovaná !!!" };
        }

        public bool Vydej_Online_Expirace_Confirm(byte terminalID, string userID, string password)
        {
            try
            {

                Globals_V1.LoadConfiguration();

                using (SqlConnection con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                using (SqlCommand comm = new SqlCommand("CZMST_Verify_Operation", con))
                {
                    comm.CommandType = CommandType.StoredProcedure;

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@operation", DbType = DbType.String, Value = "expirace" });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@pwdhash", DbType = DbType.String, Value = password });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@verified", DbType = DbType.Boolean, Direction = ParameterDirection.Output });

                    comm.Connection.Open();
                    comm.ExecuteNonQuery();

                    object verified = ((IDbDataParameter)(comm.Parameters["@verified"])).Value;

                    return Convert.ToBoolean(verified);
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        public StatusInfo Vydej_GenerateData_CZMST094(Objednavka objednavka, Sklad sklad)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
