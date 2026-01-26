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
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_GetFaskCons095,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_GetFiltrovaneZbozi,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_InsertZbozi,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_UpdateZbozi,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_DeleteZbozi,
        Fask.Console.Interfaces.Vyroba.Zbozi.IZboziVyroba_UpdateZboziTable
    {
        #region IZboziVyroba_ExportFaskCons095 Members

        public string ExportFaskCons095()
        {
            #region MyRegion
            // selektnut data z Pohoda databaze do Fask databaze...
            //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter skz_ta = null;
            //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzAlternativesTableAdapter skzalternatives_ta = null;
            //Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter skzparametry_ta = null;

            SqlTransaction trans = null;
            SqlConnection connection = null;
            try
            {
                //nacteni konfigurace Pohody...
                Globals.LoadConfiguration();

                //skz_ta = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzTableAdapter();
                //skzalternatives_ta = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzAlternativesTableAdapter();
                //skzparametry_ta = new Pohoda_DataSets.DatabasePohodaTableAdapters.SKzParametryTableAdapter();

                //skz_ta.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);
                //skzalternatives_ta.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);
                //skzparametry_ta.Connection = new System.Data.OleDb.OleDbConnection(Properties.Settings.Default.PohodaConnectionString);

                Pohoda_DataSets.DatabasePohoda.SKzDataTable skzDataTable = null;
                Pohoda_DataSets.DatabasePohoda.SKzAlternativesDataTable skzalternativesDT = null;
                Pohoda_DataSets.DatabasePohoda.SKzParametryDataTable skzparametryDT = null;

                if (!String.IsNullOrEmpty(Globals.ExportTypFilter) || !String.IsNullOrEmpty(Globals.ExportSkladFilter))
                {
                    if (Globals.ExportovatPouzeAktivniPolozky)
                    {
                        //skz_ta.Adapter.SelectCommand = new System.Data.OleDb.OleDbCommand(
                        //    "SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKz.RefSklad, SKz.RefAD, SKz.MJ2, SKz.MJ3, " +
                        //    "SKz.MJ2Koef, SKz.MJ3Koef " +
                        //    "FROM SKz " +
                        //    "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad " +
                        //    "WHERE (SKz.Odbyt <> 0) AND (s.IDS IN (" + Globals.ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + Globals.ExportTypFilter + "))"
                        //    , skz_ta.Connection
                        //    );
                        //skzDataTable = new Pohoda_DataSets.DatabasePohoda.SKzDataTable();
                        //skz_ta.Adapter.Fill(skzDataTable);

                        Database.Pohoda.SKz_FillBy_EPAP_DAD(skzDataTable, Globals.ExportSkladFilter, Globals.ExportTypFilter);


                        //dotazeni alternativ
                        if (Properties.Settings.Default.Zbozi_DotahovatAlternativniDodavatele)
                        {
                            //skzalternatives_ta.Adapter.SelectCommand = new System.Data.OleDb.OleDbCommand(
                            //    @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, " +
                            //    "SKz.RefSklad, SKzNC.RefAD AS NCRefAD " +
                            //    "FROM SKz ((" +
                            //    "INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg ) " +
                            //    "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad ) " +
                            //    "WHERE (SKz.Odbyt <> 0)  AND (s.IDS IN (" + Globals.ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + Globals.ExportTypFilter + "))"
                            //    , skzalternatives_ta.Connection
                            //    );
                            //skzalternativesDT = new Pohoda_DataSets.DatabasePohoda.SKzAlternativesDataTable();
                            //skzalternatives_ta.Adapter.Fill(skzalternativesDT);

                            Database.Pohoda.SKzAlternatives_FillBy_EPAP_DAD(skzalternativesDT, Globals.ExportSkladFilter, Globals.ExportTypFilter);
                        }
                        else
                            skzalternativesDT = new Pohoda_DataSets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...

                    }
                    else
                    {
                        //skz_ta.Adapter.SelectCommand = new System.Data.OleDb.OleDbCommand(
                        //    "SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKz.RefSklad, SKz.RefAD, SKz.MJ2, SKz.MJ3, " +
                        //    "SKz.MJ2Koef, SKz.MJ3Koef " +
                        //    "FROM SKz " +
                        //    "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad " +
                        //    "WHERE (s.IDS IN (" + Globals.ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + Globals.ExportTypFilter + "))"
                        //    , skz_ta.Connection
                        //    );
                        //skzDataTable = new Pohoda_DataSets.DatabasePohoda.SKzDataTable();
                        //skz_ta.Adapter.Fill(skzDataTable);

                        Database.Pohoda.SKz_FillBy_EPAP_DAD(skzDataTable, Globals.ExportSkladFilter, Globals.ExportTypFilter);

                        //dotazeni alternativ
                        if (Properties.Settings.Default.Zbozi_DotahovatAlternativniDodavatele)
                        {
                            //skzalternatives_ta.Adapter.SelectCommand = new System.Data.OleDb.OleDbCommand(
                            //    @"SELECT SKz.ID, SKz.RefStruct, SKz.RelSKzVC, SKz.IDS, SKz.EAN, SKz.Nazev, SKz.MJ, SKz.StavZ, SKzNC.EAN AS NCEAN, SKzNC.MJEAN AS NCMJEAN, " +
                            //    "SKz.RefSklad, SKzNC.RefAD AS NCRefAD " +
                            //    "FROM SKz ((" +
                            //    "INNER JOIN SKzNC ON SKz.ID = SKzNC.RefAg) " +
                            //    "INNER JOIN sSklad AS s ON s.ID = SKz.RefSklad ) " +
                            //    "WHERE (s.IDS IN (" + Globals.ExportSkladFilter + ")) AND (SKz.RelSkTyp IN (" + Globals.ExportTypFilter + "))"
                            //    , skzalternatives_ta.Connection
                            //    );
                            //skzalternatives_ta.Adapter.Fill(skzalternativesDT);

                            Database.Pohoda.SKzAlternatives_FillBy_DAD(skzalternativesDT, Globals.ExportSkladFilter, Globals.ExportTypFilter);
                        }
                        else
                            skzalternativesDT = new Pohoda_DataSets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...
                    }
                }
                else
                {
                    if (Globals.ExportovatPouzeAktivniPolozky)
                    {
                        //skzDataTable = skz_ta.GetDataByAktivniPolozky();
                        skzDataTable = Database.Pohoda.SKz_GetDataByAktivniPolozky();
                        //dotazeni alternativ
                        //skzalternativesDT = skzalternatives_ta.GetDataByAktivni();
                        skzalternativesDT = Database.Pohoda.SKzAlternatives_GetDataByAktivni();
                    }
                    else
                    {
                        //skzDataTable = skz_ta.GetDataByOptimalize();
                        skzDataTable = Database.Pohoda.SKz_GetDataByOptimalize();
                        //dotazeni alternativ
                        if (Properties.Settings.Default.Zbozi_DotahovatAlternativniDodavatele)
                            //skzalternativesDT = skzalternatives_ta.GetData();
                            skzalternativesDT = Database.Pohoda.SKzAlternatives_GetData();
                        else
                            skzalternativesDT = new Pohoda_DataSets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...

                    }
                }

                connection = new SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);
                connection.Open();
                trans = connection.BeginTransaction();

                Pohoda_DataSets.VydejBezPredlohyTableAdapters.FASK_CONS_095TableAdapter CZMST_095TableAdapter = new Pohoda_DataSets.VydejBezPredlohyTableAdapters.FASK_CONS_095TableAdapter();
                CZMST_095TableAdapter.Connection = new SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);
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

                int ITEMDES_MaxLength = (int)Fask.Console.Interfaces.Columns.Vyroba.ColumnsInfo_FASK_CONS_095["ITEMDESC"].MaxLength;
                int ODB_ID_MaxLength = (int)Fask.Console.Interfaces.Columns.Vyroba.ColumnsInfo_FASK_CONS_095["ODB_ID"].MaxLength;
                int LOCNCODE_MaxLength = (int)Fask.Console.Interfaces.Columns.Vyroba.ColumnsInfo_FASK_CONS_095["LOCNCODE"].MaxLength;
                int VNDITNUM_MaxLength = (int)Fask.Console.Interfaces.Columns.Vyroba.ColumnsInfo_FASK_CONS_095["VNDITNUM"].MaxLength;
                int CZ_CarKod_MaxLength = (int)Fask.Console.Interfaces.Columns.Vyroba.ColumnsInfo_FASK_CONS_095["CZ_CarKod"].MaxLength;
                int MJ_MaxLength = (int)Fask.Console.Interfaces.Columns.Vyroba.ColumnsInfo_FASK_CONS_095["MJ"].MaxLength;
                int ITEMNMBR_MaxLength = (int)Fask.Console.Interfaces.Columns.Vyroba.ColumnsInfo_FASK_CONS_095["ITEMNMBR"].MaxLength;

                
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

                        skzparametryDT = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, item.ID);
                        //skzparametryDT = skzparametry_ta.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, item.ID);
                        if (skzparametryDT.Count > 0)
                        {
                            lokace = skzparametryDT[0].IspValTextNull() ? string.Empty : skzparametryDT[0].pValText.Trim();
                        }
                    }



                    if (nazev.Length > ITEMDES_MaxLength)
                        nazev = nazev.Remove(ITEMDES_MaxLength);

                    if (refAD.Length > ODB_ID_MaxLength)
                        refAD = refAD.Remove(ODB_ID_MaxLength);


                    if (lokace.Length > LOCNCODE_MaxLength)
                        lokace = lokace.Remove(LOCNCODE_MaxLength);

                    if (ean.Length > VNDITNUM_MaxLength)
                        ean = ean.Remove(VNDITNUM_MaxLength);

                    if (cz_carkod.Length > CZ_CarKod_MaxLength)
                        cz_carkod = cz_carkod.Remove(CZ_CarKod_MaxLength);

                    if (mj.Length > MJ_MaxLength)
                        mj = mj.Remove(MJ_MaxLength);

                    if (id.Length > ITEMNMBR_MaxLength)
                        id = id.Remove(ITEMNMBR_MaxLength);

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
                    //nr.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack(item.IsRelSKzVCNull() ? (int?)null : (int?)item.RelSKzVC);
                    if (Properties.Settings.Default.pohodaXfaskPouzitVPrFXTS)
                    {
                        if (item.IsVPrFXTSNull())
                        {
                            nr.CZ_SerNum_Track = 0;
                        }
                        else
                        {
                            if (item.VPrFXTS)
                            {
                                nr.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((item == null) || (item.IsRefVPrFXTSNull()) ? (int?)null : ((int?)item.RefVPrFXTS - 1), item.VPrFXTS);
                            }
                            else
                            {
                                nr.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((item == null) || (item.IsRefVPrFVTSNull()) ? (int?)null : ((int?)item.RefVPrFVTS - 1), item.VPrFVTS);
                            }
                        }
                    }
                    else
                    {
                        nr.CZ_SerNum_Track = Classes.Pohoda.GetSerNumTrack((item == null) || (item.IsRelSKzVCNull()) ? (int?)null : (int?)item.RelSKzVC, true);
                    }
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
                        
                        //skzparametryDT = skzparametry_ta.GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, item.ID);
                        skzparametryDT = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Properties.Settings.Default.LokaceVychoziParametrNazevPohoda, item.ID);
                        if (skzparametryDT.Count > 0)
                        {
                            lokace = skzparametryDT[0].IspValTextNull() ? string.Empty : skzparametryDT[0].pValText.Trim();
                        }
                    }

                    if (nazev.Length > ITEMDES_MaxLength)
                        nazev = nazev.Remove(ITEMDES_MaxLength);

                    if (lokace.Length > LOCNCODE_MaxLength)
                        lokace = lokace.Remove(LOCNCODE_MaxLength);

                    if (ean.Length > VNDITNUM_MaxLength)
                        ean = ean.Remove(VNDITNUM_MaxLength);

                    if (cz_carkod.Length > CZ_CarKod_MaxLength)
                        cz_carkod = cz_carkod.Remove(CZ_CarKod_MaxLength);

                    if (mj.Length > MJ_MaxLength)
                        mj = mj.Remove(MJ_MaxLength);

                    if (id.Length > ITEMNMBR_MaxLength)
                        id = id.Remove(ITEMNMBR_MaxLength);

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
                //if (skz_ta != null && skz_ta.Connection.State == System.Data.ConnectionState.Open)
                //    skz_ta.Connection.Close();

                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            
            #endregion
        }

        #endregion


        #region IZboziVyroba_GetFaskCons095 Members

        public Fask.Console.Interfaces.DataSets.Vyroba GetFaskCons095()
        {

            Console.Interfaces.Classes.ZboziVyrobaListFiltr filtr = new Console.Interfaces.Classes.ZboziVyrobaListFiltr();

            Fask.Console.Interfaces.DataSets.Vyroba ds = GetFiltrovaneZbozi(filtr);

            return ds;
        }

        #endregion

        #region IZboziVyroba_GetFiltrovaneZbozi Members

        public Fask.Console.Interfaces.DataSets.Vyroba GetFiltrovaneZbozi(Console.Interfaces.Classes.ZboziVyrobaListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Console.Interfaces.DataSets.Vyroba ds = new Fask.Console.Interfaces.DataSets.Vyroba();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * from " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_CONS_095 + " zbozi " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.MaterialID))
                {
                    command.CommandText += "and zbozi.ITEMNMBR=@itemnmbr ";
                    command.Parameters.AddWithValue("@itemnmbr", filtr.MaterialID);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialNazev))
                {
                    command.CommandText += "and zbozi.ITEMDESC like '%' + @nazev + '%' ";
                    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialItemcode))
                {
                    command.CommandText += "and zbozi.ITEMCODE=@itemcode ";
                    command.Parameters.AddWithValue("@itemcode", filtr.MaterialItemcode);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialBarcode))
                {
                    command.CommandText += "and (zbozi.CZ_CarKod=@barcode or zbozi.VNDITNUM=@barcode) ";
                    command.Parameters.AddWithValue("@barcode", filtr.MaterialBarcode);
                }

                if (filtr.ZobrazitDuplicitniCaroveKody)
                {
                    command.CommandText +=
                        "and zbozi.VNDITNUM IN ( " +
                        "   SELECT VNDITNUM " +
                        "   FROM " + Fask.Console.Interfaces.Constants.Tables.TABLE_CZMST095 + " " +
                        "   where VNDITNUM <> '' " +
                        "   group by VNDITNUM " +
                        "   HAVING COUNT(*) > 1" +
                        ") "
                        ;
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_CONS_095);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IZboziVyroba_InsertZbozi Members

        public int InsertZbozi(string ITEMNMBR, string ITEMDESC, string VNDITNUM, string CZ_CarKod, string LOCNCODE, string SKL_ID, decimal QTY, decimal? QTYPACK, string MJ, string DMJ, decimal? TAXRATE, decimal? PRICE0, decimal? PRICE1, decimal? PRICE2, decimal? PRICE3, decimal? PRICE4, decimal? PRICE5, byte CZ_SerNum_Track, short CZ_SerNum_Delka, byte CZ_Rez1_Track, byte CZ_Rez2_Track, byte CZ_Rez3_Track, byte CZ_Rez4_Track, string REZ1, string ITEMCODE, string ODB_ID, float TIMEPREP, float TIMEUNIT, DateTime? TIMEFROM, DateTime? TIMETO, DateTime LSTMod, string loginid, int TIMEMODE)
        {

            Pohoda_DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta = new Pohoda_DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
            ta.Connection = new SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);

            return ta.Insert(
                 ITEMNMBR,
                 ITEMDESC,
                 VNDITNUM,
                 CZ_CarKod,
                 LOCNCODE,
                 SKL_ID,
                 QTY,
                 QTYPACK,
                 MJ,
                 DMJ,
                 TAXRATE,
                 PRICE0,
                 PRICE1,
                 PRICE2,
                 PRICE3,
                 PRICE4,
                 PRICE5,
                 CZ_SerNum_Track,
                 CZ_SerNum_Delka,
                 CZ_Rez1_Track,
                 CZ_Rez2_Track,
                 CZ_Rez3_Track,
                 CZ_Rez4_Track,
                 REZ1,
                 ITEMCODE,
                 ODB_ID,
                 TIMEPREP,
                 TIMEUNIT,
                 TIMEFROM,
                 TIMETO,
                 LSTMod,
                 loginid,
                 TIMEMODE
                );
        }

        #endregion

        #region IZboziVyroba_UpdateZbozi Members

        public int UpdateZbozi(System.Data.DataRow dataRow)
        {
            Pohoda_DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta = new Pohoda_DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
            ta.Connection = new SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);
            return ta.Update(dataRow);
        }

        #endregion

        #region IZboziVyroba_DeleteZbozi Members

        public int DeleteZbozi(string ITEMNMBR)
        {
            Pohoda_DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta = new Pohoda_DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
            ta.Connection = new SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);
            return ta.Delete(ITEMNMBR);
        }

        #endregion

        #region IZboziVyroba_UpdateZboziTable Members

        public int UpdateZboziTable(Console.Interfaces.DataSets.Vyroba.FASK_CONS_095DataTable dt)
        {
            Pohoda_DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta = new Pohoda_DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
            ta.Connection = new SqlConnection(Properties.Settings.Default.FASKPOHConnectionString);
            return ta.Update(dt.ToArray());
        }

        #endregion
    }
}
