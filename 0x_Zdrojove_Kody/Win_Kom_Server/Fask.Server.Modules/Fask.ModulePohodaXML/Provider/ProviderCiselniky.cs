using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Fask.DataSets;

namespace Fask.SQL
{
    public partial class Provider : 
        Fask.Server.Interfaces.Ciselniky.IZbozi, 
        Fask.Server.Interfaces.Ciselniky.IOdberatele, 
        Fask.Server.Interfaces.Ciselniky.IMeny,
        Fask.Server.Interfaces.Ciselniky.ISklady,
        Fask.Server.Interfaces.Ciselniky.ILokace,
        Fask.Server.Interfaces.Ciselniky.ITypDokladu,
		Fask.Server.Interfaces.Ciselniky.IStrediska
		,Fask.Server.Interfaces.Ciselniky.IPracovnici
	{
		#region Nazvy tabuelek

		private string TABLE_CZMST094 = "CZMST094";
		private string TABLE_CZMST092 = "CZMST092";
		private string TABLE_CZMST091 = "CZMST091";
		private string TABLE_CZMST096 = "CZMST096";

		#endregion

		#region Privatne metody pro export

		// \TODO Predelat do zvlašt class, tady by mnely byt pouze interface implementace...

		/// <summary>
		/// Metoda pro export Skladu
		/// </summary>
		/// <param name="so">reference na statusObjekt</param>
		/// <returns>OK anebo chyba</returns>
		/// 
		public string ExportKatalogSkladyPohodaSQL(ref StatusObject so)
        {
            try
            {
                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    return pom;

				so.Write("export zahajen");
				//\TODO: vybirat jen nektere sloupce
                //Datasets.DatabasePohoda.sSkladDataTable ssklad_dt = ssklad_ta.GetData();
				Datasets.DatabasePohoda.sSkladDataTable ssklad_dt = Database.Pohoda.sSklad_GetData();


                Datasets.ProdejTableAdapters.CZMST093TableAdapter CZMST_093TableAdapter = new Datasets.ProdejTableAdapters.CZMST093TableAdapter();
                CZMST_093TableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                CZMST_093TableAdapter.DeleteQuery();

                Datasets.Prodej sklady = new Datasets.Prodej();

                string skl_id;
                string skl_desc;
                string skl_typ;
                string skl_carcode;


				int skl_id_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_id"].MaxLength;
				int skl_desc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_desc"].MaxLength;
				int skl_typ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_typ"].MaxLength;
				int skl_carcode_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_carcode"].MaxLength;


                so.Write("export polozek z pohody do databaze");
                //SKz - brat "NAZEV"- ITEMDESC, "EAN" - VNDITNUM, "ID" - do ITEMNMBR, "RefStruc" - LOCNCODE, "IDS" - CZ_CarCode
                //Skz - "RelSKzVC" - "2" Sarze a "1" vyrobni cislo, "MJ" - do MJ,
                foreach (var item in ssklad_dt)
                {
                    skl_id = item.ID.ToString();
                    //skl_desc = item.IsSTextNull() ? "" : item.SText;
                    if (item.IsIDSNull())
                        skl_desc = item.IsSTextNull() ? "" : item.SText.Trim();
                    else
                        skl_desc = item.IDS.Trim();
                    skl_typ = "";
                    skl_carcode = item.IsIDSNull() ? "" : item.IDS.Trim();

					if (skl_id.Length > skl_id_MaxLength)
						skl_id = skl_id.Remove(skl_id_MaxLength);

					if (skl_desc.Length > skl_desc_MaxLength)
						skl_desc = skl_desc.Remove(skl_desc_MaxLength);

					if (skl_typ.Length > skl_typ_MaxLength)
						skl_typ = skl_typ.Remove(skl_typ_MaxLength);

					if (skl_carcode.Length > skl_carcode_MaxLength)
						skl_carcode = skl_carcode.Remove(skl_carcode_MaxLength);

                    sklady.CZMST093.AddCZMST093Row(skl_id, skl_desc, skl_typ, skl_carcode);

                }

                CZMST_093TableAdapter.Update(sklady.CZMST093);

                so.Write("export se provedl uspesne");
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            return "OK";
        }

		/// <summary>
		/// Metoda pro export Odběratele
		/// </summary>
		/// <param name="so">reference na statusObjekt</param>
		/// <returns>OK anebo chyba</returns>
        public string ExportKatalogAdresyPohodaSQL(ref StatusObject so)
        {
            Datasets.DatabasePohoda.ADDataTable ad_dt = new Datasets.DatabasePohoda.ADDataTable();
            Datasets.Prodej odberatele = new Datasets.Prodej();

            try
            {
                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();
                if (pom != "OK")
                    return pom;

                so.Write("export zahajen");
				// \TODO: vybirat jen nektere sloupce
                //ad_dt = ad_ta.GetData();
				ad_dt = Database.Pohoda.AD_GetData();


				Datasets.ProdejTableAdapters.CZMST090TableAdapter CZMST_090TableAdapter = new Datasets.ProdejTableAdapters.CZMST090TableAdapter();
                CZMST_090TableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;


                CZMST_090TableAdapter.DeleteQuery();


                odberatele = new Datasets.Prodej();

                string odb_id;
                string odb_desc;
                string odb_barcode;
                string odb_ico;
                string mena_id;

                so.Write("export polozek z pohody do databaze");

				int odb_id_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_id"].MaxLength;
				int odb_desc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_desc"].MaxLength;
				int odb_carcode_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_carcode"].MaxLength;
				int odb_ico_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["odb_ico"].MaxLength;
				int mena_ID_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Odberatele.ColumnsInfo_CZMST090["mena_ID"].MaxLength;


                //SKz - brat "NAZEV"- ITEMDESC, "EAN" - VNDITNUM, "ID" - do ITEMNMBR, "RefStruc" - LOCNCODE, "IDS" - CZ_CarCode
                //Skz - "RelSKzVC" - "2" Sarze a "1" vyrobni cislo, "MJ" - do MJ,
                foreach (var item in ad_dt)
                {

                    odb_ico = item.IsICONull() ? "" : item.ICO;

                    odb_barcode = item.IsCisloNull() ? "" : item.Cislo;
                    odb_desc = item.IsFirmaNull() ? "" : item.Firma;
                    odb_id = item.ID.ToString();
                    mena_id = "";

					if (odb_id.Length > odb_id_MaxLength)
						odb_id = odb_id.Remove(odb_id_MaxLength);

					if (odb_desc.Length > odb_desc_MaxLength)
						odb_desc = odb_desc.Remove(odb_desc_MaxLength);

					if (odb_barcode.Length > odb_carcode_MaxLength)
						odb_barcode = odb_barcode.Remove(odb_carcode_MaxLength);

					if (odb_ico.Length > odb_ico_MaxLength)
						odb_ico = odb_ico.Remove(odb_ico_MaxLength);

					if (mena_id.Length > mena_ID_MaxLength)
						mena_id = mena_id.Remove(mena_ID_MaxLength);

                    odberatele.CZMST090.AddCZMST090Row(
                        odb_id, 
                        odb_desc, 
                        "0", 
                        odb_barcode, 
                        odb_ico, 
                        null);

                }

                CZMST_090TableAdapter.Update(odberatele.CZMST090);

                so.Write("export se provedl uspesne");
            }
            catch (Exception ex)
            {
				Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				Fask.Logging.ExceptionHandler2.Handle(ad_dt);
				Fask.Logging.ExceptionHandler2.Handle(odberatele);

                return ex.Message;

            }
            return "OK";
        }

		/// <summary>
		/// Metoda pro export Zasob z SKz do FASK_ZASOBY
		/// </summary>
		/// <param name="so">reference na statusObjekt</param>
		/// <returns>return OK, anebo chybu...</returns>
        private string ExportKatalogZasobyPohodaSQL(ref StatusObject so)
        {
            SqlTransaction trans = null;
            SqlConnection connection = null;

            Datasets.DatabasePohoda.SKzDataTable skzDataTable = new Datasets.DatabasePohoda.SKzDataTable();
            Datasets.DatabasePohoda.SKzAlternativesDataTable skzalternativesDT = new Datasets.DatabasePohoda.SKzAlternativesDataTable();
            Datasets.DatabasePohoda.SKzParametryDataTable skzparametryDT = new Datasets.DatabasePohoda.SKzParametryDataTable();

            try
            {
                Globals_V1.LoadConfiguration();

                if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter) || !String.IsNullOrEmpty(Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter))
                {
                    if (Globals_V1.Konfigurace.ExportZasoby[0].ExportovatPouzeAktivniPolozky)
                    {
						
						Database.Pohoda.SKz_FillBy_EPAP_DAD(skzDataTable, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);

                        //dotazeni alternativ
                        if (Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele)
                        {
							Database.Pohoda.SKzAlternatives_FillBy_EPAP_DAD(skzalternativesDT, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);
                        } else
                            skzalternativesDT = new Datasets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...

                    }
                    else
                    {
						Database.Pohoda.SKz_FillBy_DAD(skzDataTable, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);

                        //dotazeni alternativ
                        if (Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele)
                        {
							Database.Pohoda.SKzAlternatives_FillBy_DAD(skzalternativesDT, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);
                        }
                        else
                            skzalternativesDT = new Datasets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...
                    }
                }
                else
                {
                    if (Globals_V1.Konfigurace.ExportZasoby[0].ExportovatPouzeAktivniPolozky)
                    {
						skzDataTable =  Database.Pohoda.SKz_GetDataByAktivniPolozky();
                        //dotazeni alternativ
						skzalternativesDT = Database.Pohoda.SKzAlternatives_GetDataByAktivni();
                    }
                    else
                    {
						skzDataTable = Database.Pohoda.SKz_GetDataByOptimalize();
                        //dotazeni alternativ
						if (Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele)
						{
							skzalternativesDT = Database.Pohoda.SKzAlternatives_GetData();
						}
						else
							skzalternativesDT = new Datasets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...
                    
                    }
                }

                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction();

				Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter FASK_ZASOBY_ta = new Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();
				FASK_ZASOBY_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
				FASK_ZASOBY_ta.Connection = trans.Connection;

				FASK_ZASOBY_ta.Transaction = trans;
				FASK_ZASOBY_ta.DeleteQuery();

				Datasets.Zbozi zbozi = new Datasets.Zbozi();

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
                so.Write("export polozek z pohody do databaze (" + polCelkem + ")");

				int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["ITEMDESC"].MaxLength;
				int ODB_ID_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["ODB_ID"].MaxLength;
				int LOCNCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["LOCNCODE"].MaxLength;
				int VNDITNUM_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["VNDITNUM"].MaxLength;
				int CZ_CarKod_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["CZ_CarKod"].MaxLength;
				int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["MJ"].MaxLength;
				int ITEMNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["ITEMNMBR"].MaxLength;


                //SKz - brat "NAZEV"- ITEMDESC,
				//"EAN" - VNDITNUM, 
				//"ID" - do ITEMNMBR, 
				//"RefStruc" - LOCNCODE, 
				//"IDS" - CZ_CarCode
                //Skz - "RelSKzVC" - "2" Sarze a "1" vyrobni cislo, "MJ" - do MJ,
				foreach (var item in skzDataTable)
				{

					var Row = zbozi.FASK_ZASOBY.NewFASK_ZASOBYRow();

					polProgress++;
					if ((polProgress % 100) == 0)
						so.Write("export polozek z pohody do databaze (" + polProgress + "/" + polCelkem + ")");

					#region Logiky a orezavačky delky

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

					if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
					{
						skzparametryDT = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, item.ID);
						if (skzparametryDT.Count > 0)
						{
							lokace = skzparametryDT[0].IspValTextNull() ? string.Empty : skzparametryDT[0].pValText.Trim();
						}
					}


					if (nazev.Length > ITEMDESC_MaxLength)
						nazev = nazev.Remove(ITEMDESC_MaxLength);

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

					// \TODO : merne jednotky pro zbozi ... 
					// rozsirit o merne jednotky pro (QTYPACK ... dle prijem ... )
					// if (Properties.Settings.Default.MerneJednotky_DotahovatDalsiVarianty) ...


					byte sernumrack = 0;


					//if (Properties.Settings.Default.pohodaXfaskPouzitVPrFXTS)
					//{
					//    if (item.IsVPrFXTSNull())
					//    {
					//        sernumrack = 0;
					//    }
					//    else
					//    {
					//        if (item.VPrFXTS)
					//        {
					//            sernumrack = Classes.Pohoda.GetSerNumTrack((item == null) || (item.IsRefVPrFXTSNull()) ? (int?)null : ((int?)item.RefVPrFXTS - 1), item.VPrFXTS);
					//        }
					//        else
					//        {
					//            sernumrack = Classes.Pohoda.GetSerNumTrack((item == null) || (item.IsRefVPrFDTSNull()) ? (int?)null : ((int?)item.RefVPrFDTS - 1), item.VPrFDTS);
					//        }
					//    }
					//}
					//else
					//{
					//    sernumrack = Classes.Pohoda.GetSerNumTrack((item == null) || (item.IsRelSKzVCNull()) ? (int?)null : (int?)item.RelSKzVC, true);
					//}

					sernumrack = Classes.Pohoda.GetPriznakSledovani_Zbozi(item.IsRelSKzVCNull() ? (int?)null : (int?)item.RelSKzVC, item);

					#endregion



					Row.ITEMNMBR = id;
					Row.ITEMDESC = nazev;
					Row.ITEMCODE = ids;
					Row.VNDITNUM = ean;
					Row.CZ_CarKod = cz_carkod;
					Row.LOCNCODE = lokace;
					Row.SKL_ID = sklad_id.ToString();
					Row.QTY = decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString());
					Row.QTYPACK = 0;
					Row.MJ = mj;
					Row.DMJ = string.Empty;
					Row.TAXRATE = 0;
					Row.PRICE0 = 0;
					Row.PRICE1 = 0;
					Row.PRICE2 = 0;
					Row.PRICE3 = 0;
					Row.PRICE4 = 0;
					Row.PRICE5 = 0;
					Row.CZ_SerNum_Track = sernumrack;
					Row.CZ_SerNum_Delka = 0;
					Row.CZ_Rez1_Track = 0;
					Row.CZ_Rez2_Track = 0;
					Row.CZ_Rez3_Track = 0;
					Row.CZ_Rez4_Track = 0;
					Row.REZ1 = string.Empty;
					Row.REZ2 = string.Empty;
					Row.REZ3 = string.Empty;
					Row.REZ4 = string.Empty;
					Row.ODB_ID = refAD;
					Row.mena_ID = string.Empty;
					Row.SERLTNUM = string.Empty;
					Row.SetWEIGHTNull();
					Row.SetTIMEFROMNull();
					Row.SetTIMETONull();
					Row.LSTMod = DateTime.Now;
					Row.loginid = string.Empty;

					Row.CZ_Expirace_Track = 0;
					Row.SetEXPIRACENull();

					zbozi.FASK_ZASOBY.AddFASK_ZASOBYRow(Row);

					//zbozi.FASK_ZASOBY.AddFASK_ZASOBYRow(
					//    id, 
					//    nazev,
					//    ids,
					//    ean, 
					//    cz_carkod, 
					//    lokace, 
					//    sklad_id.ToString(), 
					//    decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString()), 
					//    0, 
					//    mj, 
					//    "", 
					//    0, 0, 0, 0, 0, 0, 0,
					//    sernumrack, 
					//    0, 0, 0, 0, 0,
					//    string.Empty, string.Empty, string.Empty, string.Empty,
					//    refAD, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

				}

                //vlozeni alternativ ...
                foreach (var item in skzalternativesDT)
                {
					var Row = zbozi.FASK_ZASOBY.NewFASK_ZASOBYRow();

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

					sklad_id = item.RefSklad;

                    //lokace = item.IsRefStructNull() ? "" : item.RefStruct.ToString();
                    lokace = string.Empty;
                    if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda))
                    {
						skzparametryDT = Database.Pohoda.SKzParametry_GetDataByParamNameSKzID(Globals_V1.Konfigurace.Sdilene[0].LokaceVychoziParametrNazevPohoda, item.ID);
                        if (skzparametryDT.Count > 0)
                        {
                            lokace = skzparametryDT[0].IspValTextNull() ? string.Empty : skzparametryDT[0].pValText.Trim();
                        }
                    }

                    if (nazev.Length > ITEMDESC_MaxLength)
						nazev = nazev.Remove(ITEMDESC_MaxLength);

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

					Row.ITEMNMBR = id;
					Row.ITEMDESC = nazev;
					Row.ITEMCODE = ids;
					Row.VNDITNUM = ean;
					Row.CZ_CarKod = cz_carkod;
					Row.LOCNCODE = lokace;
					Row.SKL_ID = sklad_id.ToString();
					Row.QTY = decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString());
					Row.QTYPACK = 0;
					Row.MJ = mj;
					Row.DMJ = string.Empty;
					Row.TAXRATE = 0;
					Row.PRICE0 = 0;
					Row.PRICE1 = 0;
					Row.PRICE2 = 0;
					Row.PRICE3 = 0;
					Row.PRICE4 = 0;
					Row.PRICE5 = 0;
					Row.CZ_SerNum_Track = byte.Parse(item.IsRelSKzVCNull() ? "0" : item.RelSKzVC.ToString());
					Row.CZ_SerNum_Delka = 0;
					Row.CZ_Rez1_Track = 0;
					Row.CZ_Rez2_Track = 0;
					Row.CZ_Rez3_Track = 0;
					Row.CZ_Rez4_Track = 0;
					Row.REZ1 = string.Empty;
					Row.REZ2 = string.Empty;
					Row.REZ3 = string.Empty;
					Row.REZ4 = string.Empty;
					Row.ODB_ID = refAD;
					Row.mena_ID = string.Empty;
					Row.SERLTNUM = string.Empty;
					Row.SetWEIGHTNull();
					Row.SetTIMEFROMNull();
					Row.SetTIMETONull();
					Row.SetLSTModNull();
					Row.loginid = string.Empty;


					zbozi.FASK_ZASOBY.AddFASK_ZASOBYRow(Row);

					//zbozi.FASK_ZASOBY.AddFASK_ZASOBYRow(id, nazev, ean, cz_carkod, lokace, "", decimal.Parse(item.IsStavZNull() ? "0" : item.StavZ.ToString()), 0, mj, "", 0, 0, 0, 0, 0, 0, 0,
					//    byte.Parse(item.IsRelSKzVCNull() ? "0" : item.RelSKzVC.ToString()), 0, 0, 0, 0, 0, "", ids, refAD, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                }

                so.Write("vkladani polozek z pameti do databaze (" + zbozi.FASK_ZASOBY.Count + ")");

                FASK_ZASOBY_ta.Update(zbozi.FASK_ZASOBY);

                so.Write("export se provedl uspesne");

                if (trans != null)
                    trans.Commit();

                return "OK";
            }
            catch (Exception ex)
            {

				Fask.Logging.ExceptionHandler2.Handle(skzDataTable);
				Fask.Logging.ExceptionHandler2.Handle(skzalternativesDT);
				Fask.Logging.ExceptionHandler2.Handle(skzparametryDT);


                if (trans != null) trans.Rollback();

                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
		}

		private string ExportKatalogZasobyPohoda_Procedura(ref StatusObject so)
		{

			// TaD, nová metoda pro rychly export číselniku zasob
			// POZOR, v konfiguraci do ted seznam skladu byl v '04','03' ...
			// od ted tam ty uvozovky nesmí byt !!!

			// a jednotlive sklady jsou oddeleny čarkou !!!

			System.Data.SqlClient.SqlConnection adpaconnection = null;

			try
			{
				Globals_V1.LoadConfiguration();

				adpaconnection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_POHODA_FASK_ZASOBY");
				adpacommand.CommandType = CommandType.StoredProcedure;

				adpacommand.CommandTimeout = 1000;

				adpacommand.Parameters.Add((new SqlParameter("@ExportTypFilter", SqlDbType.NVarChar, 100)));
				adpacommand.Parameters.Add((new SqlParameter("@ExportSkladFilter", SqlDbType.NVarChar, 100)));
				adpacommand.Parameters.Add((new SqlParameter("@ExportovatPouzeAktivniPolozky", SqlDbType.Bit)));
				adpacommand.Parameters.Add((new SqlParameter("@EXZas_DotahovatAlternativniDodavatele", SqlDbType.Bit)));
				adpacommand.Parameters.Add((new SqlParameter("@EvidenceSarzi", SqlDbType.Bit)));
				adpacommand.Parameters.Add((new SqlParameter("@EvidenceVyrobnichCisel", SqlDbType.Bit)));
				adpacommand.Parameters.Add((new SqlParameter("@PohodaE1", SqlDbType.Bit)));


                ((IDataParameter)adpacommand.Parameters["@ExportTypFilter"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter;
                ((IDataParameter)adpacommand.Parameters["@ExportSkladFilter"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter;
                ((IDataParameter)adpacommand.Parameters["@ExportovatPouzeAktivniPolozky"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportovatPouzeAktivniPolozky;
                ((IDataParameter)adpacommand.Parameters["@EXZas_DotahovatAlternativniDodavatele"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele;
				((IDataParameter)adpacommand.Parameters["@EvidenceSarzi"]).Value = Globals_V1.Konfigurace.Sdilene[0].EvidenceSarzi;
				((IDataParameter)adpacommand.Parameters["@EvidenceVyrobnichCisel"]).Value = Globals_V1.Konfigurace.Sdilene[0].EvidenceVyrobnichCisel;
				((IDataParameter)adpacommand.Parameters["@PohodaE1"]).Value = Globals_V1.Konfigurace.PohodaInfo[0].POHODA_E1;



				adpacommand.Connection = adpaconnection;
				
				adpaconnection.Open();
				adpacommand.ExecuteNonQuery();

				return "OK";
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
					adpaconnection.Close();
			}
		}

			/// <summary>
			/// Metoda pro export Strediska
			/// </summary>
			/// <param name="so">reference na StatusObjekt</param>
			/// <returns>OK anebo Chyba</returns>
			private string ExportKatalogStrediskaPohodaSQL(ref StatusObject so)
		{

			var ds = Database.Pohoda.sSTR_GetData();

			Fask.Interfaces.DataSets.Strediska.CZMST091DataTable dt = new Fask.Interfaces.DataSets.Strediska.CZMST091DataTable();

			if ((ds != null) && (ds.Count > 0))
			{

				int str_carcode_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Strediska.ColumnsInfo_CZMST091["str_carcode"].MaxLength;
				int str_desc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Strediska.ColumnsInfo_CZMST091["str_desc"].MaxLength;
				//int str_id_MaxLength = (int)Fask.InitInstance.Inicializace.InitInstance.Strediska.ColumnsInfo_CZMST091["str_id"].MaxLength;
				int str_typ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Strediska.ColumnsInfo_CZMST091["str_typ"].MaxLength;

				this.DeleteStredisko();


				foreach (var item in ds)
				{
					var ROW = dt.NewCZMST091Row();

					ROW.str_carcode = item.ID.ToString();
					ROW.str_desc = item.IsSTextNull() ? string.Empty : (item.SText.Length > str_desc_MaxLength ? item.SText.Substring(0, str_desc_MaxLength) : item.SText.Trim());
					ROW.str_id = item.ID.ToString();

					if (!item.IsIDSNull())
					{
						string Typtmp = string.Empty;
						if (item.IDS.Length > str_typ_MaxLength)
						{
							Typtmp = item.IDS.Substring(0, str_typ_MaxLength);
						}
						else
						{
							Typtmp = item.IDS.Trim();
						}

						ROW.str_typ = Typtmp;
					}
					else
					{
						ROW.str_typ = string.Empty;
					}


					this.InsertStredisko(ROW);

				}

				so.Write("export se provedl uspesne");

			}
			else
			{
				return null;
			}

			return "OK";
		}

		/// <summary>
		/// Metoda která smaže tabulku stredisek pred exportem
		/// </summary>
		private void DeleteStredisko()
		{
			try
			{
                Globals_V1.LoadConfiguration();

				Datasets.StrediskaTableAdapters.CZMST091TableAdapter ta = new Datasets.StrediskaTableAdapters.CZMST091TableAdapter();
				ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

				ta.DeleteAll();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda která vloží jeden řadek střediska do tabulky středisek
		/// </summary>
		/// <param name="strediskoRow">řadek na vložení</param>
		/// <returns>true- OK, False- chyba</returns>
		private bool InsertStredisko(Fask.Interfaces.DataSets.Strediska.CZMST091Row strediskoRow)
		{

			try
			{
                Globals_V1.LoadConfiguration();

				Datasets.StrediskaTableAdapters.CZMST091TableAdapter ta = new Datasets.StrediskaTableAdapters.CZMST091TableAdapter();
				ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

				ta.Insert(
					strediskoRow.str_id,
					strediskoRow.Isstr_descNull() ? null : strediskoRow.str_desc.Trim(),
					strediskoRow.Isstr_typNull() ? null : strediskoRow.str_typ.Trim(),
					strediskoRow.Isstr_carcodeNull() ? null : strediskoRow.str_carcode.Trim(),
					strediskoRow.Isskl_idNull() ? null : strediskoRow.skl_id.Trim(),
					strediskoRow.Isodb_idNull() ? null : strediskoRow.odb_id.Trim());

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion


		#region IZbozi Members

		/// <summary>
		/// Metoda pro export Zásob z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        public StatusInfo KatalogZboziExport(Terminal terminal, Sklad sklad, ref StatusObject so)
        {
            StatusInfo statusInfo = new StatusInfo();
            try
            {


				///TaD 17.10.2022 Predelano natvrdo uz na export pouze pomoci SQL procedury
				//if (true)
				//{
					if (ExportKatalogZasobyPohoda_Procedura(ref so) != "OK")
					{
						statusInfo.Description = "Chyba";
						return statusInfo;
					}

				//}
				//else
				//{
				//	if (ExportKatalogZasobyPohodaSQL(ref so) != "OK")
				//	{
				//		statusInfo.Description = "Chyba";
				//		return statusInfo;
				//	}
				//}

                statusInfo.Description = "OK";
                return statusInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region OLD 7.1.2025 MaR
        ///// <summary>
        ///// Metoda pro přípravu dat pro Souboru Zboží pro terminal
        ///// </summary>
        ///// <param name="terminal">Terminal</param>
        ///// <param name="sklad">Sklad</param>
        ///// <param name="so">reference na StatusObjekt</param>
        ///// <returns>Dataset Zbozi naplnen datama</returns>
        //Fask.Interfaces.DataSets.Zbozi Fask.Server.Interfaces.Ciselniky.IZbozi.KatalogZbozi(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, ref Fask.Server.Interfaces.Classes.StatusObject so)
        //      {
        //	Fask.Interfaces.DataSets.Zbozi zbozi = null;
        //          try
        //          {
        //              Globals_V1.LoadConfiguration();

        //		Datasets.Zbozi zbozids = new Datasets.Zbozi();
        //		Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter zbozita = new Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();

        //              zbozita.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
        //              zbozita.Fill(zbozids.FASK_ZASOBY);

        //              zbozi = new Fask.Interfaces.DataSets.Zbozi();


        //              int i = 0;
        //              foreach (Datasets.Zbozi.FASK_ZASOBYRow zrow in zbozids.FASK_ZASOBY)
        //              {
        //                  zbozi.FASK_ZASOBY.ImportRow(zrow);
        //                  zbozi.FASK_ZASOBY[i].SetAdded();
        //                  i++;
        //              }

        //              return zbozi;
        //          }
        //          catch (Exception ex)
        //          {
        //              so.Exception = true;
        //              so.Write(ex.Message);
        //              throw ex;
        //          }
        //      } 
        #endregion



        public Fask.Interfaces.DataSets.Zbozi KatalogZbozi(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{
			Globals_V1.LoadConfiguration();
			System.Data.SqlClient.SqlConnection conn = null;

			try
			{
				// Inicializace připojení k databázi
				conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

				// SQL dotaz
				//25.8.2025 MaR zakomentoval brala se hvezdickova konvence zprava
				//string select = "SELECT * FROM " + "FASK_ZASOBY" + " WHERE SKL_ID LIKE '" + sklad.ID + "%'";

				string select = "SELECT * FROM FASK_ZASOBY ";

				if (!string.IsNullOrEmpty(sklad.ID))
				{
					select += " Where SKL_ID = '" + sklad.ID.Trim() + "'";
				}


				// Použití SqlDataAdapter k naplnění DataSetu
				SqlDataAdapter xda = new SqlDataAdapter(select, conn);
				Fask.Interfaces.DataSets.Zbozi zbozi = new Fask.Interfaces.DataSets.Zbozi();

				// Naplnění DataSetu
				xda.Fill(zbozi, zbozi.FASK_ZASOBY.TableName);

				// Označení všech řádků jako nové
				foreach (Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBYRow srow in zbozi.FASK_ZASOBY)
				{
					srow.SetAdded();
				}

				return zbozi;
			}
			catch (Exception ex)
			{
				// Nastavení chybového stavu
				so.Exception = true;
				so.Write(ex.Message);
				throw;
			}
			finally
			{
				// Uzavření připojení
				if (conn != null && conn.State == System.Data.ConnectionState.Open)
				{
					conn.Close();
				}
			}
		}


		#endregion

		#region IOdberatele Members

		/// <summary>
		/// Metoda pro export Odběratele z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
		public StatusInfo KatalogOdberateleExport(Terminal terminal, ref StatusObject so)
        {
            StatusInfo statusInfo = new StatusInfo();
            try
            {
                if (ExportKatalogAdresyPohodaSQL(ref so) != "OK")
                {
                    statusInfo.Description = "Chyba";
                    return statusInfo;
                }

                statusInfo.Description = "OK";
                return statusInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Odběratele pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset Odberatele naplnen datama</returns>
        public Fask.Interfaces.DataSets.Odberatele KatalogOdberatele(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
			Fask.Interfaces.DataSets.Odberatele odberatele = null;
            try
            {
                //if (ExportKatalogAdresyPohoda() != "OK")
                //    return odberatele;

				Datasets.ProdejTableAdapters.CZMST090TableAdapter odberateleta = new Datasets.ProdejTableAdapters.CZMST090TableAdapter();
                odberateleta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                Datasets.Prodej odberateleds = new Datasets.Prodej();
                odberateleta.Fill(odberateleds.CZMST090);

                odberatele = new Fask.Interfaces.DataSets.Odberatele();
                int i = 0;
                foreach (Datasets.Prodej.CZMST090Row orow in odberateleds.CZMST090)
                {
                    odberatele.CZMST090.ImportRow(orow);
                    odberatele.CZMST090[i].SetAdded();
                    i++;
                }
                /*foreach (Fask.DataSets.Odberatele.CZMST090Row orow in odberatele.CZMST090)
                {
                    orow.SetAdded();
                }*/

                return odberatele;
            }
            catch (Exception ex)
            {
                //so.Exception = true;
                //so.Write(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region ISklady Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Sklady pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <returns>Dataset Sklady naplnen datama</returns>
        public Fask.Interfaces.DataSets.Sklady KatalogSklady(Terminal terminal)
        {
			Fask.Interfaces.DataSets.Sklady sklady = null;
            try
            {
                Globals_V1.LoadConfiguration();

                //if (ExportKatalogZasobyPohoda() != "OK")
                //    return zbozi;

                Datasets.Prodej skladyds = new Datasets.Prodej();
				Datasets.ProdejTableAdapters.CZMST093TableAdapter skladyta = new Datasets.ProdejTableAdapters.CZMST093TableAdapter();

                skladyta.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                skladyta.Fill(skladyds.CZMST093);

                sklady = new Fask.Interfaces.DataSets.Sklady();

                int i = 0;
                foreach (Datasets.Prodej.CZMST093Row srow in skladyds.CZMST093)
                {
                    sklady.CZMST093.ImportRow(srow);
                    sklady.CZMST093[i].SetAdded();
                    i++;
                }

                return sklady;
            }
            catch (Exception ex)
            {
                //so.Exception = true;
                //so.Write(ex.Message);
                throw ex;
            }
        }

		/// <summary>
		/// Metoda pro export Sklady z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        public Fask.Server.Interfaces.Classes.StatusInfo KatalogSkladyExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so) 
        {
            StatusInfo statusInfo = new StatusInfo();
            try
            {
                if (ExportKatalogSkladyPohodaSQL(ref so) != "OK")
                {
                    statusInfo.Description = "Chyba";
                    return statusInfo;
                }

                statusInfo.Description = "OK";
                return statusInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IMeny Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Měny pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <returns>Dataset Meny naplnen datama</returns>
        public Fask.DataSets.Meny KatalogMen(Terminal terminal)
        {
            //Datasets.DatabasePohodaTableAdapters.sCMenyTableAdapter meny_ta = null;
            Fask.DataSets.Meny meny = new Fask.DataSets.Meny();
            Datasets.MenyTableAdapters.CZMST097TableAdapter czmst097_ta = null;
            try
            {
                Globals_V1.LoadConfiguration();

                czmst097_ta = new Datasets.MenyTableAdapters.CZMST097TableAdapter();
                czmst097_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                Datasets.Meny.CZMST097DataTable meny_dt = czmst097_ta.GetData();

                foreach (var item in meny_dt)
                {
                    Fask.DataSets.Meny.CZMST097Row mena = meny.CZMST097.NewCZMST097Row();
                    mena.mena_ID = item.mena_ID;
                    mena.mena_text = item.mena_text;
                    mena.mena_hlavni = false;
                    meny.CZMST097.AddCZMST097Row(mena);
                }
                return meny;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (czmst097_ta != null && czmst097_ta.Connection.State == System.Data.ConnectionState.Open)
                    czmst097_ta.Connection.Close();
            }
        }

		/// <summary>
		/// Metoda pro export Měny z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
        public StatusInfo KatalogMenExport(Terminal terminal, ref StatusObject so)
        {
            StatusInfo statusInfo = new StatusInfo();


            //Datasets.DatabasePohodaTableAdapters.sCMenyTableAdapter meny_ta = null;
            Datasets.MenyTableAdapters.CZMST097TableAdapter czmst097_ta = null;
            try
            {
                Globals_V1.LoadConfiguration();

                //meny_ta = new Datasets.DatabasePohodaTableAdapters.sCMenyTableAdapter();
                //meny_ta.Connection.ConnectionString = Globals.ConnectionStringPohodaDB;

                //Datasets.DatabasePohoda.sCMenyDataTable menyDataTable = meny_ta.GetDataByAktivni();
				Datasets.DatabasePohoda.sCMenyDataTable menyDataTable = Database.Pohoda.sCMeny_GetDataByAktivni();

                czmst097_ta = new Datasets.MenyTableAdapters.CZMST097TableAdapter();
                czmst097_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

                czmst097_ta.DeleteQuery();

                Datasets.Meny meny = new Datasets.Meny();


                foreach (var item in menyDataTable)
                {
                    //czmst097_ta.Insert(item.ID.ToString(), item.IsKodNull() ? "-" : item.Kod);
                    meny.CZMST097.AddCZMST097Row(item.ID.ToString(), item.IsKodNull() ? "-" : item.Kod);
                }

                czmst097_ta.Update(meny);

                statusInfo.Description = "OK";
                return statusInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (czmst097_ta != null && czmst097_ta.Connection.State == System.Data.ConnectionState.Open)
                    czmst097_ta.Connection.Close();

				//if (meny_ta != null && meny_ta.Connection.State == System.Data.ConnectionState.Open)
				//    czmst097_ta.Connection.Close();
            }
        }

        #endregion

        #region ILokace Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Lokace pro terminal 
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Lokace, dataset naplnen daty</returns>
        public Fask.DataSets.Lokace KatalogLokace(Terminal terminal, Sklad sklad)
        {
            try
            {
                Globals_V1.LoadConfiguration();

                System.Data.SqlClient.SqlConnection conn = null;

                conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                string select = "SELECT * FROM " + TABLE_CZMST094;
                // naplneni czmst094
                SqlDataAdapter xda = new SqlDataAdapter(select, conn);

                Fask.DataSets.Lokace lokace = new Fask.DataSets.Lokace();
                xda.Fill(lokace, lokace.CZMST094.TableName);

                foreach (DataSets.Lokace.CZMST094Row srow in lokace.CZMST094)
                {
                    srow.SetAdded();
                }

                // naplneni CZMST_SkladLokace_LokaceTypy
                select = "SELECT * FROM " + TABLE_CZMST_SkladLokace_LokaceTypy;

                xda = new SqlDataAdapter(select, conn);

                xda.Fill(lokace, lokace.CZMST_SkladLokace_LokaceTypy.TableName);

                foreach (DataSets.Lokace.CZMST_SkladLokace_LokaceTypyRow srow in lokace.CZMST_SkladLokace_LokaceTypy)
                {
                    srow.SetAdded();
                }

                return lokace;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		public StatusInfo KatalogLokaceExport(Terminal terminal, Sklad sklad, ref StatusObject so)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region ITypDokladu Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Typy Dokladu pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset TypDokladu naplnen datama </returns>
		public Fask.DataSets.TypDokladu KatalogTypDokladu(Terminal terminal, Sklad sklad)
        {
            try
            {
                Globals_V1.LoadConfiguration();

                System.Data.SqlClient.SqlConnection conn = null;

                conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				
				string select = "SELECT * FROM " + TABLE_CZMST092 ;

				if (!string.IsNullOrEmpty(sklad.ID))
				{
					select += " Where SKL_ID = '" + sklad.ID.Trim() + "'";
				}


                SqlDataAdapter xda = new SqlDataAdapter(select, conn);

                Fask.DataSets.TypDokladu typdokladu = new Fask.DataSets.TypDokladu();
                xda.Fill(typdokladu, typdokladu.CZMST092.TableName);

                foreach (DataSets.TypDokladu.CZMST092Row srow in typdokladu.CZMST092)
                {
                    srow.SetAdded();
                }

                typdokladu.CZMST092.AcceptChanges();

                return typdokladu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

		#region IStrediska Members

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Střediska pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset Strediska naplnen datama</returns>
		public Fask.Interfaces.DataSets.Strediska KatalogStrediska(Terminal terminal, Sklad sklad)
		{
			try
			{
                Globals_V1.LoadConfiguration();

				System.Data.SqlClient.SqlConnection conn = null;

				conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

				string select = "SELECT * FROM " + TABLE_CZMST091; // +" where str_typ like '" + sklad + "%' order by dex_row_id";

				SqlDataAdapter xda = new SqlDataAdapter(select, conn);

				//Nacteni dat prijemky z databaze                    
				Fask.Interfaces.DataSets.Strediska strediska = new Fask.Interfaces.DataSets.Strediska();
				xda.Fill(strediska, strediska.CZMST091.TableName);

				//Ulozeni dat prijemky pro terminal
				foreach (Fask.Interfaces.DataSets.Strediska.CZMST091Row srow in strediska.CZMST091)
				{
					srow.SetAdded();
				}

				return strediska;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pro export Střediska z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
		public StatusInfo KatalogStrediskaExport(Terminal terminal, ref StatusObject so)
		{
			StatusInfo statusInfo = new StatusInfo();
			try
			{
				if (ExportKatalogStrediskaPohodaSQL(ref so) != "OK")
				{
					statusInfo.Description = "Chyba";
					return statusInfo;
				}

				statusInfo.Description = "OK";
				return statusInfo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}



		#endregion

		#region IPracovnici Members

		public Fask.DataSets.Pracovnici KatalogPracovnici(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{

            try
            {
                Globals_V1.LoadConfiguration();

                System.Data.SqlClient.SqlConnection conn = null;

                conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                string select = "SELECT * FROM " + TABLE_CZMST096; // +" where str_typ like '" + sklad + "%' order by dex_row_id";


                SqlDataAdapter xda = new SqlDataAdapter(select, conn);

                Fask.DataSets.Pracovnici pracovnici = new Fask.DataSets.Pracovnici();
                xda.Fill(pracovnici, pracovnici.CZMST096.TableName);

                foreach (DataSets.Pracovnici.CZMST096Row srow in pracovnici.CZMST096)
                {
                    srow.SetAdded();
                }

                return pracovnici;
            }
            catch (Exception ex)
            {

                throw ex;
            }




			//	return new Fask.DataSets.Pracovnici();
		}

		public Fask.Server.Interfaces.Classes.StatusInfo KatalogPracovniciExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{

			StatusInfo statusInfo = new StatusInfo();
			try
			{
				statusInfo.Description = "OK";
				return statusInfo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        public StatusInfo KatalogTypDokladuExport(Terminal terminal, Sklad sklad, ref StatusObject so)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
