using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Fask.SQL
{
    public static class Inventura
    {
        //private static Datasets.Inventura InventuraDS = new Datasets.Inventura();


        public static bool LoadInventura()
        {
            Globals_V1.LoadConfiguration();
            SqlTransaction trans = null;
            SqlConnection connection = null;

            try
            {
                Datasets.Inventura InventuraDS = new Datasets.Inventura();

                //Datasets.Inventura.SKzInvSeznamyPolDataTable dt_polozky = Database.Inventura.SKzInvSeznamyPol();
                Datasets.Inventura.SKzInvDataTable dt_polozky = Database.Pohoda.SKzInvNezauct();
                Datasets.Inventura.SKzInvLstDataTable dt_inventury = Database.Pohoda.SKzInvList();


                Datasets.Inventura.SKzInvAlternativniDodavateleDataTable dt_polozkyAlternativy = null;
                if (Globals_V1.Konfigurace.Inventura1[0].I_DotahovatAlternativniDodavatele)
                    dt_polozkyAlternativy = Database.Pohoda.SKzInvNezauctAlternativy();
                else 
                    dt_polozkyAlternativy = new Datasets.Inventura.SKzInvAlternativniDodavateleDataTable(); //musi existovat...???

                if (dt_polozky == null)
                {
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "Neni zadna inventura na stahnuti");
                    return false;
                }

                Datasets.DatabasePohoda.SKzRow zasoba = null;
                Datasets.DatabasePohoda.ADRow dodavatel = null;
                string ean;
                string itemnmbr;
                string plu;
                string locncode;
                string nazev;
                string skl_id;
                string mj;
                string ids;
                byte sernumtrack;

                Datasets.Inventura.CZMST_I1DataTable dtI1 = new Datasets.Inventura.CZMST_I1DataTable();
                Datasets.Inventura.CZMST_I3DataTable dtI3 = new Datasets.Inventura.CZMST_I3DataTable();

                Datasets.Inventura inventura = new Datasets.Inventura();


                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction();

                Datasets.InventuraTableAdapters.CZMST_I1TableAdapter CZMST_I1TableAdapter = new Datasets.InventuraTableAdapters.CZMST_I1TableAdapter();
                Datasets.InventuraTableAdapters.CZMST_I3TableAdapter CZMST_I3TableAdapter = new Datasets.InventuraTableAdapters.CZMST_I3TableAdapter();
                Datasets.InventuraTableAdapters.CZMST_I1HTableAdapter CZMST_I1HTableAdapter = new Datasets.InventuraTableAdapters.CZMST_I1HTableAdapter();

                //CZMST_I3TableAdapter.Connection.ConnectionString = Globals.ConnectionString;
                //CZMST_I1TableAdapter.Connection.ConnectionString = Globals.ConnectionString;
                CZMST_I1TableAdapter.Connection = trans.Connection;
                CZMST_I3TableAdapter.Connection = trans.Connection;
                CZMST_I1HTableAdapter.Connection = trans.Connection;

                //Datasets.InventuraTableAdapters.CZMST_I2TableAdapter CZMST_I2TableAdapter = new Datasets.InventuraTableAdapters.CZMST_I2TableAdapter();
                //CZMST_I2TableAdapter.Connection.ConnectionString = Globals.ConnectionString;

                //smazeme vsechny predchozi zaznamy
                CZMST_I1TableAdapter.Transaction = trans;
                CZMST_I3TableAdapter.Transaction = trans;
                CZMST_I1HTableAdapter.Transaction = trans;


                CZMST_I1TableAdapter.DeleteQuery();
                CZMST_I3TableAdapter.DeleteQuery();
                CZMST_I1HTableAdapter.DeleteQuery();

				Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
				if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
				{
					ParamTA = new Datasets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
					ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				}

				int Description_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1H["Description"].MaxLength;
                
                foreach (Datasets.Inventura.SKzInvLstRow row in dt_inventury)
                {
                    string text = "-";

                    if (!row.IsSTextNull())
                    {
                        text = row.SText;
						if (text.Length > Description_MaxLength)
							text = text.Remove(Description_MaxLength);
                    }

                    inventura.CZMST_I1H.AddCZMST_I1HRow(row.ID, text, 0);
                }

				int ITEMNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["ITEMNMBR"].MaxLength;
				int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["MJ"].MaxLength;
				int VENDNAME_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["VENDNAME"].MaxLength;
				int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1["ITEMDESC"].MaxLength;
				int ITEMCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1["ITEMCODE"].MaxLength;

                foreach (Datasets.Inventura.SKzInvRow row in dt_polozky)
                {
                    if (Globals_V1.Konfigurace.Inventura1[0].DotahovatInformaceDodavatele)
                        zasoba = Database.Inventura.SKzRow(row.RefSKz);
                    else
                        zasoba = null;

                    ean = row.IsEANNull() ? string.Empty : row.EAN;
                    plu = row.IsPLUNull() ? string.Empty : row.PLU.ToString();
                    nazev = row.IsNazevNull() ? string.Empty : row.Nazev;
                    itemnmbr = row.IsRefSKzNull() ? string.Empty : row.RefSKz.ToString();
					// \TODO : ? dotahnout informaci o vychozi lokaci ... 
                    // locncode =  row.IsRefSkladNull() ? string.Empty : row.RefSklad.ToString();  
                    locncode = string.Empty;

                    skl_id = row.IsRefSkladNull() ? string.Empty : row.RefSklad.ToString();
                    mj = row.IsMJNull() ? string.Empty : row.MJ;
                    ids = row.IsIDSNull() ? string.Empty : row.IDS;
                    //TaD 19.9.2018
                    //sernumtrack = Classes.Pohoda.GetSerNumTrack(row.IsRelSKzVCNull() ? (int?)null : (int?)row.RelSKzVC);

					//1.8.2019, přechod na FASK_ZASOBY_PARAMETRY
					//if (Properties.Settings.Default.pohodaXfaskPouzitVPrFXTS)
					//{
					//    if (row.IsSKz_VPrFXTSNull())
					//    {
					//        sernumtrack = 0;
					//    }
					//    else
					//    {
					//        if (row.SKz_VPrFXTS)
					//        {
					//            sernumtrack = Classes.Pohoda.GetSerNumTrack((row == null) || (row.IsSKz_RefVPrFXTSNull()) ? (int?)null : ((int?)row.SKz_RefVPrFXTS - 1), row.SKz_VPrFXTS);
					//        }
					//        else
					//        {
					//            sernumtrack = Classes.Pohoda.GetSerNumTrack((row == null) || (row.IsSKz_RefVPrFITSNull()) ? (int?)null : ((int?)row.SKz_RefVPrFITS - 1), row.SKz_VPrFITS);
					//        }
					//    }
					//}
					//else
					//{
					//    sernumtrack = Classes.Pohoda.GetSerNumTrack((row == null) || (row.IsRelSKzVCNull()) ? (int?)null : (int?)row.RelSKzVC, true);
					//}

					if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
					{
						sernumtrack = Classes.Pohoda.GetPriznakSledovani(row.IsRelSKzVCNull() ? (int?)null : (int?)row.RelSKzVC, row, Classes.Pohoda.TypAgendy.Inventura);
					}
					else
					{
						Datasets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
						var dt_param = ParamTA.GetDataByITEMNMBR(itemnmbr.Trim());

						if ((dt_param != null) && (dt_param.Count > 0))
						{
							dt_row_param = dt_param.First();
						}

						sernumtrack = Classes.Pohoda.GetPriznakSledovani(row.IsRelSKzVCNull() ? (int?)null : (int?)row.RelSKzVC, dt_row_param, Classes.Pohoda.TypAgendy.Inventura);
					}


					if (itemnmbr.Length > ITEMNMBR_MaxLength)
						itemnmbr = itemnmbr.Remove(ITEMNMBR_MaxLength);


					if (nazev.Length > ITEMDESC_MaxLength)
						nazev = nazev.Remove(ITEMDESC_MaxLength);

					if (ids.Length > ITEMCODE_MaxLength)
						ids = ids.Remove(ITEMCODE_MaxLength);

                    inventura.CZMST_I1.AddCZMST_I1Row(
                        row.RefAg, 
                        itemnmbr, 
                        ean, 
                        nazev, 
                        locncode,
                        (decimal)row.StavZ,
                        string.Empty,
                        DateTime.Now,
                        0,
                        0,
                        sernumtrack,
                        0,
                        0,
                        0,
                        string.Empty,
                        string.Empty,
                        ids,
                        skl_id,
                        0,
                        0,
                        0
                        );

                    //CZMST_I1TableAdapter.Insert(row.RefAg, itemnmbr, ean,  /*zasoba.IsRefSkladNull() ? string.Empty : zasoba.RefSklad.ToString()*/, ,
                    //    string.Empty, DateTime.Now, 0, 0, (byte)row.RelSKzVC, 0 /* row.RelSKzVC > 0 ? (byte)1 : (byte)0*/, 0, 0, "","");

					if (mj.Length > MJ_MaxLength)
						mj = mj.Remove(MJ_MaxLength);

                    if (zasoba != null && !zasoba.IsRefADNull())
                        dodavatel = Database.Inventura.AD(zasoba.RefAD);

                    if (dodavatel == null)
                    {
                        inventura.CZMST_I3.AddCZMST_I3Row(row.RefAg, itemnmbr, plu, (decimal)0, mj, string.Empty, ean, string.Empty, 0);
                        //CZMST_I3TableAdapter.Insert(row.RefAg, itemnmbr, plu, (decimal)0, row.IsMJNull() ? string.Empty : row.MJ, string.Empty, ean, string.Empty);
                    }
                    else
                    {
						if (!dodavatel.IsFirmaNull() && dodavatel.Firma.Length > VENDNAME_MaxLength)
							dodavatel.Firma = dodavatel.Firma.Remove(VENDNAME_MaxLength);

                        inventura.CZMST_I3.AddCZMST_I3Row(row.RefAg, itemnmbr, plu, (decimal)0, mj, zasoba.RefAD.ToString(), ean, dodavatel.IsFirmaNull() ? string.Empty : dodavatel.Firma, 0);
                        //CZMST_I3TableAdapter.Insert(row.RefAg, itemnmbr, plu, (decimal)0, row.IsMJNull() ? string.Empty : row.MJ, zasoba.RefAD.ToString(), ean, dodavatel.IsFirmaNull() ? string.Empty : dodavatel.Firma);
                    }
                }

                //alternativy polozek dodavatelu do I3
                foreach (Datasets.Inventura.SKzInvAlternativniDodavateleRow row in dt_polozkyAlternativy)
                {
					if (!row.IsNCMJEANNull() && row.NCMJEAN.Length > MJ_MaxLength)
						row.NCMJEAN = row.NCMJEAN.Remove(MJ_MaxLength);
					if (!row.IsNCFirmaNull() && row.NCFirma.Length > VENDNAME_MaxLength)
						row.NCFirma = row.NCFirma.Remove(VENDNAME_MaxLength);

                    inventura.CZMST_I3.AddCZMST_I3Row(
                        row.RefAg
                        , row.IsRefSKzNull() ? string.Empty : row.RefSKz.ToString()
                        , row.IsPLUNull() ? string.Empty : row.PLU.ToString()
                        , (decimal)0
                        , row.IsNCMJEANNull() ? string.Empty : row.NCMJEAN.Trim()
                        , row.IsNCDodavatelIDNull() ? string.Empty : row.NCDodavatelID.ToString()
                        , row.IsNCEANNull() ? string.Empty : row.NCEAN.Trim()
                        , row.IsNCFirmaNull() ? string.Empty : row.NCFirma.Trim(),
                        0
                        );
                }

                CZMST_I1TableAdapter.Update(inventura.CZMST_I1);
                CZMST_I3TableAdapter.Update(inventura.CZMST_I3);
                CZMST_I1HTableAdapter.Update(inventura.CZMST_I1H);

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();

                throw ex;
                //Log.writeErrorLog(ex.ToString());
                //return false;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="countentries"></param>
        /// <param name="inv_edn">true - provede se import vzdy (i pokud je terminalID > 100); false - pokud je terminalID > 100, tak se import dat neprovede</param>
        /// <returns></returns>
        public static bool ImportInventura(int countentries, bool inv_edn)
        {
            try
            {
                Datasets.InventuraTableAdapters.CZMST_I1TableAdapter CZMST_I1TableAdapter = new Datasets.InventuraTableAdapters.CZMST_I1TableAdapter();
                Datasets.InventuraTableAdapters.CZMST_I41TableAdapter CZMST_I4TableAdapter = new Datasets.InventuraTableAdapters.CZMST_I41TableAdapter();
                CZMST_I4TableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
                CZMST_I1TableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                Datasets.Inventura.CZMST_I1DataTable dt1 = CZMST_I1TableAdapter.GetDataByTop1CountEntries(countentries);
                if (!inv_edn)
                {
                    if (dt1.Count > 0 && dt1[0].TerminalID > 100)
                        return true;
                }

                Datasets.Inventura.CZMST_I41DataTable dt_polozky = CZMST_I4TableAdapter.GetData(countentries);

                #region Nove volani v transakci

                System.Data.OleDb.OleDbTransaction trans = null;
                System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                try
                {
                    connection.Open();
                    trans = connection.BeginTransaction();

                    int i;
                    foreach (var row in dt_polozky)
                    {
                        if (Globals_V1.Konfigurace.Inventura1[0].PrepisovatZkontrolovanePolozky)
                            i = Database.Pohoda.SKzINV_UpdateQuery(connection, trans, row.Q, row.Q, Convert.ToInt32(row.ITEMNMBR.Trim()), int.Parse(row.skl_id), countentries);
                        else
                            i = Database.Pohoda.SKzINV_UpdateQueryNotAudit(connection, trans, row.Q, row.Q, Convert.ToInt32(row.ITEMNMBR.Trim()), int.Parse(row.skl_id), countentries);
                    }

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

                #endregion

                return true;
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }

    }
}
