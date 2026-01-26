using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Logging;

namespace Fask.ModulePohodaXML.Classes
{
    public static class Inventura
    {
        public static bool LoadInventura()
        {
            SqlTransaction trans = null;
            SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();

                int I3_VENDNAME_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["VENDNAME"].MaxLength;
                int I3_MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["MJ"].MaxLength;
                int I3_ITEMNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["ITEMNMBR"].MaxLength;

                int I1H_Description_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1H["Description"].MaxLength;

                int I1_ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1["ITEMDESC"].MaxLength;
                int I1_ITEMCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1["ITEMCODE"].MaxLength;

                Pohoda_DataSets.Inventura InventuraDS = new Pohoda_DataSets.Inventura();

                //Datasets.Inventura.SKzInvSeznamyPolDataTable dt_polozky = Database.Inventura.SKzInvSeznamyPol();
               Pohoda_DataSets.Inventura.SKzInvDataTable dt_polozky = Database.Pohoda.SKzInvNezauct();
               Pohoda_DataSets.Inventura.SKzInvLstDataTable dt_inventury = Database.Pohoda.SKzInvList();

                Pohoda_DataSets.Inventura.SKzInvVCDataTable dt_sarze = Database.Pohoda.SKzInvVCNezauct();

                Pohoda_DataSets.Inventura.SKzInvAlternativniDodavateleDataTable dt_polozkyAlternativy = null;
                if (Globals_V1.Konfigurace.Inventura1[0].I_DotahovatAlternativniDodavatele)
                    dt_polozkyAlternativy = Database.Pohoda.SKzInvNezauctAlternativy();
                else
                    dt_polozkyAlternativy = new Pohoda_DataSets.Inventura.SKzInvAlternativniDodavateleDataTable(); //musi existovat...???

                if (dt_polozky == null)
                {
                    Fask.Logging.ExceptionHandler2.Handle(LogLevel.Warn,"Neni zadna inventura na stahnuti");
                    return false;
                }

                Pohoda_DataSets.DatabasePohoda.SKzRow zasoba = null;
                Pohoda_DataSets.DatabasePohoda.ADRow dodavatel = null;
                string ean;
                string itemnmbr;
                string plu;
                string locncode;
                string nazev;
                string skl_id;
                string mj;
                string ids;
                byte sernumtrack = 0;
                byte expiracetrack = 0;

                Pohoda_DataSets.Inventura.CZMST_I1DataTable dtI1 = new Pohoda_DataSets.Inventura.CZMST_I1DataTable();
                Pohoda_DataSets.Inventura.CZMST_I3DataTable dtI3 = new Pohoda_DataSets.Inventura.CZMST_I3DataTable();
                Pohoda_DataSets.Inventura.CZMST_I2DataTable dtI2 = new Pohoda_DataSets.Inventura.CZMST_I2DataTable();

                Pohoda_DataSets.Inventura inventura = new Pohoda_DataSets.Inventura();


                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction();

               Pohoda_DataSets.InventuraTableAdapters.CZMST_I1TableAdapter CZMST_I1TableAdapter = new Pohoda_DataSets.InventuraTableAdapters.CZMST_I1TableAdapter();
               Pohoda_DataSets.InventuraTableAdapters.CZMST_I3TableAdapter CZMST_I3TableAdapter = new Pohoda_DataSets.InventuraTableAdapters.CZMST_I3TableAdapter();
               Pohoda_DataSets.InventuraTableAdapters.CZMST_I1HTableAdapter CZMST_I1HTableAdapter = new Pohoda_DataSets.InventuraTableAdapters.CZMST_I1HTableAdapter();
                Pohoda_DataSets.InventuraTableAdapters.CZMST_I2TableAdapter CZMST_I2TableAdapter = new Pohoda_DataSets.InventuraTableAdapters.CZMST_I2TableAdapter();

                //CZMST_I3TableAdapter.Connection.ConnectionString = Globals.ConnectionString;
                //CZMST_I1TableAdapter.Connection.ConnectionString = Globals.ConnectionString;
                CZMST_I1TableAdapter.Connection = trans.Connection;
                CZMST_I3TableAdapter.Connection = trans.Connection;
                CZMST_I1HTableAdapter.Connection = trans.Connection;
                CZMST_I2TableAdapter.Connection = trans.Connection;

                //Datasets.InventuraTableAdapters.CZMST_I2TableAdapter CZMST_I2TableAdapter = new Pohoda_DataSets.InventuraTableAdapters.CZMST_I2TableAdapter();
                //CZMST_I2TableAdapter.Connection.ConnectionString = Globals.ConnectionString;

                //smazeme vsechny predchozi zaznamy
                CZMST_I1TableAdapter.Transaction = trans;
                CZMST_I3TableAdapter.Transaction = trans;
                CZMST_I1HTableAdapter.Transaction = trans;
                CZMST_I2TableAdapter.Transaction = trans;


                CZMST_I1TableAdapter.DeleteQuery();
                CZMST_I3TableAdapter.DeleteQuery();
                CZMST_I1HTableAdapter.DeleteQuery();
                CZMST_I2TableAdapter.DeleteQuery();


                Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter ParamTA = null;
                if (!Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                {
                    ParamTA = new Pohoda_DataSets.ZboziTableAdapters.FASK_ZASOBY_PARAMETRYTableAdapter();
                    ParamTA.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                }


                foreach (Pohoda_DataSets.Inventura.SKzInvLstRow row in dt_inventury)
                {
                    string text = "-";

                    if (!row.IsSTextNull())
                    {
                        text = row.SText;
                        if (text.Length > I1H_Description_MaxLength)
                            text = text.Remove(I1H_Description_MaxLength);
                    }

                    inventura.CZMST_I1H.AddCZMST_I1HRow(row.ID, text, 0);
                }

                foreach (Pohoda_DataSets.Inventura.SKzInvRow row in dt_polozky)
                {
                    if (Globals_V1.Konfigurace.Inventura1[0].DotahovatInformaceDodavatele)
                        zasoba = Database.Inventura.SKzRow(row.RefSKz);
                    else
                        zasoba = null;

                    ean = row.IsEANNull() ? string.Empty : row.EAN;
                    plu = row.IsPLUNull() ? string.Empty : row.PLU.ToString();
                    nazev = row.IsNazevNull() ? string.Empty : row.Nazev;
                    itemnmbr = row.IsRefSKzNull() ? string.Empty : row.RefSKz.ToString();
                    // TODO : ? dotahnout informaci o vychozi lokaci ... 
                    // locncode =  row.IsRefSkladNull() ? string.Empty : row.RefSklad.ToString();  
                    locncode = string.Empty;

                    skl_id = row.IsRefSkladNull() ? string.Empty : row.RefSklad.ToString();
                    mj = row.IsMJNull() ? string.Empty : row.MJ;
                    ids = row.IsIDSNull() ? string.Empty : row.IDS;

                    sernumtrack = 0;
                    expiracetrack = 0;

                    //TaD 19.9.2018
                    //sernumtrack = Classes.Pohoda.GetSerNumTrack(row.IsRelSKzVCNull() ? (int?)null : (int?)row.RelSKzVC);

                    //TaD 1.8.2019 , přechod na FASK_ZASOBY_PARAMETRY
                    //if (Properties.Settings.Default.POHODA_E1)
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

                    //TaD 1.8.2019 , přechod na FASK_ZASOBY_PARAMETRY

                    //if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                    //{
                    //    sernumtrack = Classes.Pohoda.GetPriznakSledovani(row.IsRelSKzVCNull() ? (int?)null : (int?)row.RelSKzVC, row);
                    //}
                    //else
                    //{
                    //    Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
                    //    var dt_param = ParamTA.GetDataByITEMNMBR(itemnmbr.Trim());

                    //    if ((dt_param != null) && (dt_param.Count > 0))
                    //    {
                    //        dt_row_param = dt_param.First();
                    //    }

                    //    sernumtrack = Classes.Pohoda.GetPriznakSledovani(row.IsRelSKzVCNull() ? (int?)null : (int?)row.RelSKzVC, dt_row_param, Pohoda.TypAgendy.Inventura);
                    //}

                    #region MyRegion

                    var skzdt = Database.Pohoda.SKz_GetDataByID(row.RefSKz);

                    if ((skzdt != null) && (skzdt.Count > 0))
                    {
                        //19.9.2018 TaD nova logika SerNumTrack
                        var skz_row = skzdt[0];

                        if (Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1)
                        {
                            int? tmp_RelSKzVC = skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC;

                            sernumtrack = Classes.Pohoda.GetPriznakSledovani(tmp_RelSKzVC, skz_row, Classes.Pohoda.TypAgendy.Inventura);

                            if (tmp_RelSKzVC.HasValue)
                            {
                                if (tmp_RelSKzVC.Value == 2 && sernumtrack == 2)
                                {
                                    bool? expTrack = skz_row.IsVPrCZExpTrackISNull() ? (bool?)null : skz_row.VPrCZExpTrackIS;

                                    if (expTrack.HasValue && expTrack.Value)
                                    {
                                        expiracetrack = 1;
                                    }
                                }

                                if (tmp_RelSKzVC.Value == 1 && sernumtrack == 1)
                                {
                                    bool? expTrack = skz_row.IsVPrCZExpTrackISNull() ? (bool?)null : skz_row.VPrCZExpTrackIS;

                                    if (expTrack.HasValue && expTrack.Value)
                                    {
                                        expiracetrack = 1;
                                    }
                                    else
                                    {
                                        expiracetrack = 0;
                                    }

                                    //bool? SerTrack = skz_row.IsVPrCZSerNumTrISNull() ? (bool?)null : skz_row.VPrCZSerNumTrIS;

                                    //if (SerTrack.HasValue && SerTrack.Value)
                                    //{
                                    //    seRow.CZ_SerNum_Track = 10;
                                    //}
                                }
                            }
                        }
                        else
                        {
                            Pohoda_DataSets.Zbozi.FASK_ZASOBY_PARAMETRYRow dt_row_param = null;
                            var dt_param = ParamTA.GetDataByITEMNMBR(row.RefSKz.ToString());

                            if ((dt_param != null) && (dt_param.Count > 0))
                            {
                                dt_row_param = dt_param.First();
                            }

                            sernumtrack = Classes.Pohoda.GetPriznakSledovani(skz_row.IsRelSKzVCNull() ? (int?)null : (int?)skz_row.RelSKzVC, dt_row_param, Classes.Pohoda.TypAgendy.Inventura);
                        }

                    }
                    else
                    {
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "Nebylo nalezeno zbozi v SKz, hodnota CZ_SerNum_Track byla nastavena na 0");
                        sernumtrack = 0;
                    }

                    #endregion

                    if (itemnmbr.Length > I3_ITEMNMBR_MaxLength)
                        itemnmbr = itemnmbr.Remove(I3_ITEMNMBR_MaxLength);


                    if (nazev.Length > I1_ITEMDESC_MaxLength)
                        nazev = nazev.Remove(I1_ITEMDESC_MaxLength);

                    if (ids.Length > I1_ITEMCODE_MaxLength)
                        ids = ids.Remove(I1_ITEMCODE_MaxLength);

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
                        "",
                        "",
                        ids,
                        skl_id,
                        expiracetrack);

                    //CZMST_I1TableAdapter.Insert(row.RefAg, itemnmbr, ean,  /*zasoba.IsRefSkladNull() ? string.Empty : zasoba.RefSklad.ToString()*/, ,
                    //    string.Empty, DateTime.Now, 0, 0, (byte)row.RelSKzVC, 0 /* row.RelSKzVC > 0 ? (byte)1 : (byte)0*/, 0, 0, "","");

                    if (mj.Length > I3_MJ_MaxLength)
                        mj = mj.Remove(I3_MJ_MaxLength);

                    if (zasoba != null && !zasoba.IsRefADNull())
                        dodavatel = Database.Inventura.AD(zasoba.RefAD);

                    if (dodavatel == null)
                    {
                        inventura.CZMST_I3.AddCZMST_I3Row(row.RefAg, itemnmbr, plu, (decimal)0, mj, string.Empty, ean, string.Empty, 0);
                        //CZMST_I3TableAdapter.Insert(row.RefAg, itemnmbr, plu, (decimal)0, row.IsMJNull() ? string.Empty : row.MJ, string.Empty, ean, string.Empty);
                    }
                    else
                    {
                        if (!dodavatel.IsFirmaNull() && dodavatel.Firma.Length > I3_VENDNAME_MaxLength)
                            dodavatel.Firma = dodavatel.Firma.Remove(I3_VENDNAME_MaxLength);

                        inventura.CZMST_I3.AddCZMST_I3Row(row.RefAg, itemnmbr, plu, (decimal)0, mj, zasoba.RefAD.ToString(), ean, dodavatel.IsFirmaNull() ? string.Empty : dodavatel.Firma, 0);
                        //CZMST_I3TableAdapter.Insert(row.RefAg, itemnmbr, plu, (decimal)0, row.IsMJNull() ? string.Empty : row.MJ, zasoba.RefAD.ToString(), ean, dodavatel.IsFirmaNull() ? string.Empty : dodavatel.Firma);
                    }
                }

                //alternativy polozek dodavatelu do I3
                foreach (Pohoda_DataSets.Inventura.SKzInvAlternativniDodavateleRow row in dt_polozkyAlternativy)
                {
                    if (!row.IsNCMJEANNull() && row.NCMJEAN.Length > I3_MJ_MaxLength)
                        row.NCMJEAN = row.NCMJEAN.Remove(I3_MJ_MaxLength);
                    if (!row.IsNCFirmaNull() && row.NCFirma.Length > I3_VENDNAME_MaxLength)
                        row.NCFirma = row.NCFirma.Remove(I3_VENDNAME_MaxLength);

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

                // Dotazeni šarži do I2
                foreach (Pohoda_DataSets.Inventura.SKzInvVCRow row in dt_sarze)
                {
                    var radek = inventura.CZMST_I2.NewCZMST_I2Row();

                    radek.CountEntries = row.RefAg;
                    radek.ITEMNMBR = row.IsRefSKzNull() ? string.Empty : row.RefSKz.ToString();
                    radek.SERLNMBR = row.IsVCisloNull() ? string.Empty : row.VCislo.Trim();
                    radek.QTY = Convert.ToDecimal(row.StavZ);

                    if (row.IsDatExpNull())
                        radek.SetExpiraceNull();
                    else
                        radek.Expirace = row.DatExp; 

                    inventura.CZMST_I2.AddCZMST_I2Row(radek);
                }

                CZMST_I1TableAdapter.Update(inventura.CZMST_I1);
                CZMST_I3TableAdapter.Update(inventura.CZMST_I3);
                CZMST_I1HTableAdapter.Update(inventura.CZMST_I1H);
                CZMST_I2TableAdapter.Update(inventura.CZMST_I2);

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
                Globals_V1.LoadConfiguration();

               Pohoda_DataSets.InventuraTableAdapters.CZMST_I1TableAdapter CZMST_I1TableAdapter = new Pohoda_DataSets.InventuraTableAdapters.CZMST_I1TableAdapter();
               Pohoda_DataSets.InventuraTableAdapters.CZMST_I41TableAdapter CZMST_I4TableAdapter = new Pohoda_DataSets.InventuraTableAdapters.CZMST_I41TableAdapter();
                CZMST_I4TableAdapter.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                CZMST_I1TableAdapter.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

               Pohoda_DataSets.Inventura.CZMST_I1DataTable dt1 = CZMST_I1TableAdapter.GetDataByTop1CountEntries(countentries);
                if (!inv_edn)
                {
                    if (dt1.Count > 0 && dt1[0].TerminalID > 100)
                        return true;
                }

               Pohoda_DataSets.Inventura.CZMST_I41DataTable dt_polozky = CZMST_I4TableAdapter.GetData(countentries);

                #region Nove volani v transakci

                System.Data.OleDb.OleDbTransaction trans = null;
                System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                try
                {
                    connection.Open();
                    trans = connection.BeginTransaction();

                    int i;
                    foreach (Pohoda_DataSets.Inventura.CZMST_I41Row row in dt_polozky)
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
