using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider : 
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_ExportFaskCons095,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_GetFaskCons095
    {
        #region IZboziVyroba_GetFaskCons095 Members

        public string ExportFaskCons095()
        {
            #region MyRegion
            // selektnut data z Pohoda databaze do Fask databaze...
            Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter skz_ta = null;
            Pohoda_DataSets.DatabasePohodaTableAdapters.SKzAlternativesTableAdapter skzalternatives_ta = null;
            Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter skzparametry_ta = null;

            SqlTransaction trans = null;
            SqlConnection connection = null;
            try
            {
                //nacteni konfigurace Pohody...
                Globals.LoadConfiguration();

                skz_ta = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter();
                skzalternatives_ta = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzAlternativesTableAdapter();
                skzparametry_ta = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();

                skz_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
                skzalternatives_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;
                skzparametry_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

                Pohoda_DataSets.DatabasePohoda.SKzDataTable skzDataTable = null;
                Pohoda_DataSets.DatabasePohoda.SKzAlternativesDataTable skzalternativesDT = null;
                Pohoda_DataSets.DatabasePohoda.SKzParametryDataTable skzparametryDT = null;

                if (!String.IsNullOrEmpty(Globals.ExportTypFilter) || !String.IsNullOrEmpty(Globals.ExportSkladFilter))
                {
                    if (Globals.ExportovatPouzeAktivniPolozky)
                    {
                        skz_ta.Adapter.SelectCommand = new System.Data.OleDb.OleDbCommand(
                            "SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKz.RefSklad, SKz.RefAD, SKz.MJ2, SKz.MJ3, " +
                            "SKz.MJ2Koef, SKz.MJ3Koef " +
                            "FROM SKz " +
                            "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad " +
                            "WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (" + Globals.ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + Globals.ExportTypFilter + "))"
                            , skz_ta.Connection
                            );
                        skzDataTable = new Pohoda_DataSets.DatabasePohoda.SKzDataTable();
                        skz_ta.Adapter.Fill(skzDataTable);

                        //dotazeni alternativ
                        if (Properties.Settings.Default.Zbozi_DotahovatAlternativniDodavatele)
                        {
                            skzalternatives_ta.Adapter.SelectCommand = new System.Data.OleDb.OleDbCommand(
                                @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, " +
                                "SKz.RefSklad, SKzNC.RefAD AS NCRefAD " +
                                "FROM SKz ((" +
                                "INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg ) " +
                                "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad ) " +
                                "WHERE (SKz.Odbyt <> 0)  AND (s.IDS IN (" + Globals.ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + Globals.ExportTypFilter + "))"
                                , skzalternatives_ta.Connection
                                );
                            skzalternativesDT = new Pohoda_DataSets.DatabasePohoda.SKzAlternativesDataTable();
                            skzalternatives_ta.Adapter.Fill(skzalternativesDT);
                        }
                        else
                            skzalternativesDT = new Pohoda_DataSets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...

                    }
                    else
                    {
                        skz_ta.Adapter.SelectCommand = new System.Data.OleDb.OleDbCommand(
                            "SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKz.RefSklad, SKz.RefAD, SKz.MJ2, SKz.MJ3, " +
                            "SKz.MJ2Koef, SKz.MJ3Koef " +
                            "FROM SKz " +
                            "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad " +
                            "WHERE (s.IDS IN (" + Globals.ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + Globals.ExportTypFilter + "))"
                            , skz_ta.Connection
                            );
                        skzDataTable = new Pohoda_DataSets.DatabasePohoda.SKzDataTable();
                        skz_ta.Adapter.Fill(skzDataTable);

                        //dotazeni alternativ
                        if (Properties.Settings.Default.Zbozi_DotahovatAlternativniDodavatele)
                        {
                            skzalternatives_ta.Adapter.SelectCommand = new System.Data.OleDb.OleDbCommand(
                                @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, " +
                                "SKz.RefSklad, SKzNC.RefAD AS NCRefAD " +
                                "FROM SKz ((" +
                                "INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg) " +
                                "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad ) " +
                                "WHERE (s.IDS IN (" + Globals.ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + Globals.ExportTypFilter + "))"
                                , skzalternatives_ta.Connection
                                );
                            skzalternatives_ta.Adapter.Fill(skzalternativesDT);
                        }
                        else
                            skzalternativesDT = new Pohoda_DataSets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...
                    }
                }
                else
                {
                    if (Globals.ExportovatPouzeAktivniPolozky)
                    {
                        skzDataTable = skz_ta.GetDataByAktivniPolozky();
                        //dotazeni alternativ
                        skzalternativesDT = skzalternatives_ta.GetDataByAktivni();
                    }
                    else
                    {
                        skzDataTable = skz_ta.GetDataByOptimalize();
                        //dotazeni alternativ
                        if (Properties.Settings.Default.Zbozi_DotahovatAlternativniDodavatele)
                            skzalternativesDT = skzalternatives_ta.GetData();
                        else
                            skzalternativesDT = new Pohoda_DataSets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...

                    }
                }

                connection = new SqlConnection(Globals.ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction();

                Pohoda_DataSets.VydejBezPredlohyTableAdapters.FASK_CONS_095TableAdapter CZMST_095TableAdapter = new Pohoda_DataSets.VydejBezPredlohyTableAdapters.FASK_CONS_095TableAdapter();
                CZMST_095TableAdapter.Connection.ConnectionString = Globals.ConnectionString;
                CZMST_095TableAdapter.Connection = trans.Connection;

                //CZMST_095TableAdapter.Adapter.InsertCommand.Connection = trans.Connection;
                //CZMST_095TableAdapter.Adapter.InsertCommand.Transaction = trans;

                //CZMST_095TableAdapter.Adapter.
                CZMST_095TableAdapter.Transaction = trans;
                CZMST_095TableAdapter.DeleteQuery();

                Pohoda_DataSets.VydejBezPredlohy zbozi = new Pohoda_DataSets.VydejBezPredlohy();

                string nazev;
                string lokace;
                string ean;
                string cz_carkod;
                string mj;
                string id;
                string ids;
                int sklad_id;
                string refAD;

                int polCelkem = skzDataTable.Count;
                int polProgress = 0;
                
                //Status objekt neni v tomto projekte
                //so.Write("export polozek z pohody do databaze (" + polCelkem + ")");
                
                
                //SKz - brat "NAZEV"- ITEMDESC, "EAN" - VNDITNUM, "ID" - do ITEMNMBR, "RefStruc" - LOCNCODE, "IDS" - CZ_CarCode
                //Skz - "RelSKzVC" - "2" Sarze a "1" vyrobni cislo, "MJ" - do MJ,
                foreach (var item in skzDataTable)
                {
                    polProgress++;

                    //Status objekt neni v tomto projekte
                    //if ((polProgress % 100) == 0)
                    //    so.Write("export polozek z pohody do databaze (" + polProgress + "/" + polCelkem + ")");

                    nazev = item.IsNazevNull() ? "" : item.Nazev;
                    ean = item.IsEANNull() ? "" : item.EAN;
                    //cz_carkod = item.IsIDSNull() ? "" : item.IDS;
                    cz_carkod = string.Empty;
                    refAD = item.IsRefADNull() ? "" : item.RefAD.ToString();
                    mj = item.IsMJNull() ? "" : item.MJ;
                    ids = item.IsIDSNull() ? "" : item.IDS;
                    id = item.ID.ToString();

                    sklad_id = item.RefSklad;

                    //lokace = item.IsRefStructNull() ? "" : item.RefStruct.ToString();
                    lokace = string.Empty;

                    if (!String.IsNullOrEmpty(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda))
                    {
                        skzparametryDT = skzparametry_ta.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, item.ID);
                        if (skzparametryDT.Count > 0)
                        {
                            lokace = skzparametryDT[0].IspValTextNull() ? string.Empty : skzparametryDT[0].pValText.Trim();
                        }
                    }

                    if (nazev.Length > zbozi.FASK_CONS_095.ITEMDESCColumn.MaxLength)
                        nazev = nazev.Remove(zbozi.FASK_CONS_095.ITEMDESCColumn.MaxLength);

                    if (refAD.Length > zbozi.FASK_CONS_095.ODB_IDColumn.MaxLength)
                        refAD = refAD.Remove(zbozi.FASK_CONS_095.ODB_IDColumn.MaxLength);


                    if (lokace.Length > zbozi.FASK_CONS_095.LOCNCODEColumn.MaxLength)
                        lokace = lokace.Remove(zbozi.FASK_CONS_095.LOCNCODEColumn.MaxLength);

                    if (ean.Length > zbozi.FASK_CONS_095.VNDITNUMColumn.MaxLength)
                        ean = ean.Remove(zbozi.FASK_CONS_095.VNDITNUMColumn.MaxLength);

                    if (cz_carkod.Length > zbozi.FASK_CONS_095.CZ_CarKodColumn.MaxLength)
                        cz_carkod = cz_carkod.Remove(zbozi.FASK_CONS_095.CZ_CarKodColumn.MaxLength);

                    if (mj.Length > zbozi.FASK_CONS_095.MJColumn.MaxLength)
                        mj = mj.Remove(zbozi.FASK_CONS_095.MJColumn.MaxLength);

                    if (id.Length > zbozi.FASK_CONS_095.ITEMNMBRColumn.MaxLength)
                        id = id.Remove(zbozi.FASK_CONS_095.ITEMNMBRColumn.MaxLength);

                    // TODO : merne jednotky pro zbozi ... 
                    // rozsirit o merne jednotky pro (QTYPACK ... dle prijem ... )
                    // if (Properties.Settings.Default.MerneJednotky_DotahovatDalsiVarianty) ...

                    var nr = zbozi.FASK_CONS_095.NewFASK_CONS_095Row();
                    nr.ITEMNMBR = id;
                    nr.ITEMDESC = nazev;
                    nr.VNDITNUM = ean;
                    nr.CZ_CarKod = cz_carkod;
                    nr.LOCNCODE = lokace;
                    nr.SKL_ID = sklad_id.ToString();
                    nr.QTY = decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString());
                    nr.QTYPACK = 0;
                    nr.MJ = mj;
                    nr.DMJ = "";
                    nr.TAXRATE = 0;
                    nr.PRICE0 = 0;
                    nr.PRICE1 = 0;
                    nr.PRICE2 = 0;
                    nr.PRICE3 = 0;
                    nr.PRICE4 = 0;
                    nr.PRICE5 = 0;
                    nr.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack(item.IsRelSKzVCNull() ? (int?)null : (int?)item.RelSKzVC);
                    nr.CZ_SerNum_Delka= 0;
                    nr.CZ_Rez1_Track= 0;
                    nr.CZ_Rez2_Track= 0;
                    nr.CZ_Rez3_Track= 0;
                    nr.CZ_Rez4_Track = 0;
                    nr.REZ1 = "";
                    nr.ITEMCODE = ids;
                    nr.ODB_ID = refAD;
                    nr.TIMEMODE = 0;
                    nr.TIMEPREP = 0;
                    nr.TIMEUNIT = 0;
                    nr.SetTIMEFROMNull();
                    nr.SetTIMETONull();
                    nr.LSTMod = DateTime.Now;
                    nr.loginid = string.Empty;

                    //nr.SetTIMEFROMNull();
                    zbozi.FASK_CONS_095.AddFASK_CONS_095Row(nr);

                    //zbozi.FASK_CONS_095.AddFASK_CONS_095Row(
                    //    id,                                                              //ITEMNMBR
                    //    nazev,                                                           //ITEMDESC
                    //    ean,                                                             //VNDITNUM
                    //    cz_carkod,                                                       //CZ_CarKod
                    //    lokace,                                                          //LOCNCODE
                    //    sklad_id.ToString(),                                             //SKL_ID
                    //    decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString()), // QTY
                    //    0,                                                               //QTYPACK
                    //    mj,                                                              //MJ
                    //    "",                                                              //DMJ
                    //    0,                                                               //TAXRATE
                    //    0,                                                               //PRICE0
                    //    0,                                                               //PRICE1
                    //    0,                                                               //PRICE2
                    //    0,                                                               //PRICE3
                    //    0,                                                               //PRICE4
                    //    0,                                                               //PRICE5
                    //    Classes.Pohoda.GetSerNumTrack(item.IsRelSKzVCNull() ? (int?)null : (int?)item.RelSKzVC),//CZ_SerNum_Track
                    //    0,                                                               //CZ_SerNum_Delka
                    //    0,                                                               //CZ_Rez1_Track
                    //    0,                                                               //CZ_Rez2_Track
                    //    0,                                                               //CZ_Rez3_Track
                    //    0,                                                               //CZ_Rez4_Track
                    //    "",                                                              //REZ1
                    //    ids,                                                             //ITEMCODE
                    //    refAD,                                                           //ODB_ID
                    //    0,                                                               //TIMEMODE
                    //    0,                                                               //TIMEPREP  
                    //    0,                                                               //TIMEUNIT 
                    //    new DateTime(2000, 1, 1, 1, 1, 1),                                               //TIMEFROM
                    //    new DateTime(2000, 1, 1, 1, 1, 1),                                               //TIMETO
                    //    new DateTime(2000, 1, 1, 1, 1, 1),                                               //LSTMod
                    //    string.Empty                                                     //loginid
                    //    );
                    //CZMST_095TableAdapter.Insert(id, nazev, ean, cz_carkod, lokace, "", decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString()), 0, mj, "", 0, 0, 0, 0, 0, 0, 0,
                    //    byte.Parse(item.IsRelSKzVCNull() ? "0" : item.RelSKzVC.ToString()), 0, 0, 0, 0, 0, "");

                }


                //vlozeni alternativ ...
                foreach (var item in skzalternativesDT)
                {
                    nazev = item.IsNazevNull() ? "" : item.Nazev;
                    //ean = item.IsEANNull() ? "" : item.EAN;
                    ean = item.IsNCEANNull() ? "" : item.NCEAN;
                    //cz_carkod = item.IsIDSNull() ? "" : item.IDS;
                    cz_carkod = string.Empty;
                    //mj = item.IsMJNull() ? "" : item.MJ;
                    mj = item.IsNCMJEANNull() ? "" : item.NCMJEAN;
                    ids = item.IsIDSNull() ? "" : item.IDS;
                    id = item.ID.ToString();
                    refAD = item.IsNCRefADNull() ? "" : item.NCRefAD.ToString();

                    //lokace = item.IsRefStructNull() ? "" : item.RefStruct.ToString();
                    lokace = string.Empty;
                    if (!String.IsNullOrEmpty(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda))
                    {
                        skzparametryDT = skzparametry_ta.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, item.ID);
                        if (skzparametryDT.Count > 0)
                        {
                            lokace = skzparametryDT[0].IspValTextNull() ? string.Empty : skzparametryDT[0].pValText.Trim();
                        }
                    }

                    if (nazev.Length > zbozi.FASK_CONS_095.ITEMDESCColumn.MaxLength)
                        nazev = nazev.Remove(zbozi.FASK_CONS_095.ITEMDESCColumn.MaxLength);

                    if (lokace.Length > zbozi.FASK_CONS_095.LOCNCODEColumn.MaxLength)
                        lokace = lokace.Remove(zbozi.FASK_CONS_095.LOCNCODEColumn.MaxLength);

                    if (ean.Length > zbozi.FASK_CONS_095.VNDITNUMColumn.MaxLength)
                        ean = ean.Remove(zbozi.FASK_CONS_095.VNDITNUMColumn.MaxLength);

                    if (cz_carkod.Length > zbozi.FASK_CONS_095.CZ_CarKodColumn.MaxLength)
                        cz_carkod = cz_carkod.Remove(zbozi.FASK_CONS_095.CZ_CarKodColumn.MaxLength);

                    if (mj.Length > zbozi.FASK_CONS_095.MJColumn.MaxLength)
                        mj = mj.Remove(zbozi.FASK_CONS_095.MJColumn.MaxLength);

                    if (id.Length > zbozi.FASK_CONS_095.ITEMNMBRColumn.MaxLength)
                        id = id.Remove(zbozi.FASK_CONS_095.ITEMNMBRColumn.MaxLength);

                    var nr = zbozi.FASK_CONS_095.NewFASK_CONS_095Row();
                    nr.ITEMNMBR = id;
                    nr.ITEMDESC = nazev;
                    nr.VNDITNUM = ean;
                    nr.CZ_CarKod = cz_carkod;
                    nr.LOCNCODE = lokace;
                    nr.SKL_ID = "";
                    nr.QTY = decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString());
                    nr.QTYPACK = 0;
                    nr.MJ = mj;
                    nr.DMJ = "";
                    nr.TAXRATE = 0;
                    nr.PRICE0 = 0;
                    nr.PRICE1 = 0;
                    nr.PRICE2 = 0;
                    nr.PRICE3 = 0;
                    nr.PRICE4 = 0;
                    nr.PRICE5 = 0;
                    nr.CZ_SerNum_Track = byte.Parse(item.IsRelSKzVCNull() ? "0" : item.RelSKzVC.ToString());
                    nr.CZ_SerNum_Delka = 0;
                    nr.CZ_Rez1_Track = 0;
                    nr.CZ_Rez2_Track = 0;
                    nr.CZ_Rez3_Track = 0;
                    nr.CZ_Rez4_Track = 0;
                    nr.REZ1 = "";
                    nr.ITEMCODE = ids;
                    nr.ODB_ID = refAD;
                    nr.TIMEMODE = 0;
                    nr.TIMEPREP = 0;
                    nr.TIMEUNIT = 0;
                    nr.SetTIMEFROMNull();
                    nr.SetTIMETONull();
                    nr.LSTMod = DateTime.Now;
                    nr.loginid = string.Empty;

                    //nr.SetTIMEFROMNull();
                    zbozi.FASK_CONS_095.AddFASK_CONS_095Row(nr);

                    //zbozi.FASK_CONS_095.AddFASK_CONS_095Row(
                    //    id,                                                              //ITEMNMBR
                    //    nazev,                                                           //ITEMDESC
                    //    ean,                                                             //VNDITNUM
                    //    cz_carkod,                                                       //CZ_CarKod
                    //    lokace,                                                          //LOCNCODE
                    //    "",                                             //SKL_ID
                    //    decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString()), // QTY
                    //    0,                                                               //QTYPACK
                    //    mj,                                                              //MJ
                    //    "",                                                              //DMJ
                    //    0,                                                               //TAXRATE
                    //    0,                                                               //PRICE0
                    //    0,                                                               //PRICE1
                    //    0,                                                               //PRICE2
                    //    0,                                                               //PRICE3
                    //    0,                                                               //PRICE4
                    //    0,                                                               //PRICE5
                    //    byte.Parse(item.IsRelSKzVCNull() ? "0" : item.RelSKzVC.ToString()),//CZ_SerNum_Track
                    //    0,                                                               //CZ_SerNum_Delka
                    //    0,                                                               //CZ_Rez1_Track
                    //    0,                                                               //CZ_Rez2_Track
                    //    0,                                                               //CZ_Rez3_Track
                    //    0,                                                               //CZ_Rez4_Track
                    //    "",                                                              //REZ1
                    //    ids,                                                             //ITEMCODE
                    //    refAD,                                                           //ODB_ID
                    //    0,                                                               //TIMEMODE
                    //    0,                                                               //TIMEPREP  
                    //    0,                                                               //TIMEUNIT 
                    //    new DateTime(2000, 1, 1, 1, 1, 1),                                               //TIMEFROM
                    //    new DateTime(2000, 1, 1, 1, 1, 1),                                               //TIMETO
                    //    new DateTime(2000, 1, 1, 1, 1, 1),                                               //LSTMod
                    //    string.Empty                                                     //loginid
                    //);
                    //CZMST_095TableAdapter.Insert(id, nazev, ean, cz_carkod, lokace, "", decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString()), 0, mj, "", 0, 0, 0, 0, 0, 0, 0,
                    //    byte.Parse(item.IsRelSKzVCNull() ? "0" : item.RelSKzVC.ToString()), 0, 0, 0, 0, 0, "");

                }

                //so.Write("vkladani polozek z pameti do databaze (" + zbozi.FASK_CONS_095.Count + ")");
                CZMST_095TableAdapter.Update(zbozi.FASK_CONS_095);

                //so.Write("export se provedl uspesne");

               
                if (trans != null)
                    trans.Commit();

                return "OK";
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();

                throw ex;
            }
            finally
            {
                if (skz_ta != null && skz_ta.Connection.State == System.Data.ConnectionState.Open)
                    skz_ta.Connection.Close();

                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            
            #endregion
        }

        #endregion


        #region IZboziVyroba_GetFaskCons095 Members

        public Production.DataServices.KonzolaDataSet GetFaskCons095()
        {
            Production.DataServices.KonzolaDataSet ds = new Production.DataServices.KonzolaDataSet();

            var lta = new Production.DataServices.KonzolaDataSetTableAdapters.FASK_CONS_095TableAdapter();
            lta.Connection.ConnectionString = Properties.Settings.Default.FASKPOHConnectionString;
            lta.Fill(ds.FASK_CONS_095);

            return ds;
        }

        #endregion
    }
}
