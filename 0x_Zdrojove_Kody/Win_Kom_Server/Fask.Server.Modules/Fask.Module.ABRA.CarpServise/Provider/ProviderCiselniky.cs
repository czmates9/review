using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using Fask.Interfaces.DataSets;
using Fask.DataSets;

namespace Fask.Module.ABRA.CarpServise
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část čísleník.
	/// </summary>
    public partial class Provider : Fask.Server.Interfaces.Ciselniky.ICiselniky
    {

		#region IStrediska Members

		public Fask.Interfaces.DataSets.Strediska KatalogStrediska(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{
			
			return new Fask.Interfaces.DataSets.Strediska();
		}

		public StatusInfo KatalogStrediskaExport(Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
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

		#endregion

		#region IZbozi Members Implementovano

		/// <summary>
		/// Metoda pro export Zásob z IS do SQL DB
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Objekt StatusInfo který nese info o stavu</returns>
		public StatusInfo KatalogZboziExport(Server.Interfaces.Classes.Terminal terminal, Sklad sklad, ref StatusObject so)
		{
			StatusInfo statusInfo = new StatusInfo();
			try
			{
				if (ExportKatalogZasobyABRAFirebird(ref so) != "OK")
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
		/// Metoda pro přípravu dat pro Souboru Zboží pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Dataset Zbozi naplnen datama</returns>
		Fask.Interfaces.DataSets.Zbozi Fask.Server.Interfaces.Ciselniky.IZbozi.KatalogZbozi(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{
			Fask.Interfaces.DataSets.Zbozi zbozi = null;
			try
			{
				Globals_V1.LoadConfiguration();

				SQL_Datasets.Zbozi zbozids = new SQL_Datasets.Zbozi();
				SQL_Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter zbozita = new SQL_Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();

				zbozita.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				zbozita.Fill(zbozids.FASK_ZASOBY);

				zbozi = new Fask.Interfaces.DataSets.Zbozi();


				int i = 0;
				foreach (SQL_Datasets.Zbozi.FASK_ZASOBYRow zrow in zbozids.FASK_ZASOBY)
				{
					zbozi.FASK_ZASOBY.ImportRow(zrow);
					zbozi.FASK_ZASOBY[i].SetAdded();
					i++;
				}

				return zbozi;
			}
			catch (Exception ex)
			{
				so.Exception = true;
				so.Write(ex.Message);
				throw ex;
			}
		}


		#endregion

		#region ITypDokladu Members Implementovano

		/// <summary>
		/// Metoda pro přípravu dat pro Souboru Typy Dokladu pro terminal
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset TypDokladu naplnen datama </returns>
		public TypDokladu KatalogTypDokladu(Server.Interfaces.Classes.Terminal terminal, Sklad sklad)
		{
			try
			{
				Globals_V1.LoadConfiguration();

				System.Data.SqlClient.SqlConnection conn = null;

				conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

				string select = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST092;

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

		#region IOdberatele Members

		public Odberatele KatalogOdberatele(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{
			
			return new Fask.Interfaces.DataSets.Odberatele();
		}

		public Fask.Server.Interfaces.Classes.StatusInfo KatalogOdberateleExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
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

		#endregion

		#region IPracovnici Members

		public Fask.DataSets.Pracovnici KatalogPracovnici(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{
			
			return new Fask.DataSets.Pracovnici();
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

		#endregion

		#region ISklady Members Implementovano

		public Fask.Interfaces.DataSets.Sklady KatalogSklady(Fask.Server.Interfaces.Classes.Terminal terminal)
		{
			

			System.Data.SqlClient.SqlConnection conn = null;

			conn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
			string select = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST093;

			SqlDataAdapter xda = new SqlDataAdapter(select, conn);

			Fask.Interfaces.DataSets.Sklady sklady = new Fask.Interfaces.DataSets.Sklady();
			xda.Fill(sklady, sklady.CZMST093.TableName);

			foreach (Fask.Interfaces.DataSets.Sklady.CZMST093Row srow in sklady.CZMST093)
			{
				srow.SetAdded();
			}

			return sklady;
		}

		public Fask.Server.Interfaces.Classes.StatusInfo KatalogSkladyExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
		{
			
			StatusInfo statusInfo = new StatusInfo();
			try
			{
				if (ExportKatalogSkladyABRAFirebird(ref so) != "OK")
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

		#region ILokace Members

		public Fask.DataSets.Lokace KatalogLokace(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
		{
			
			return new Fask.DataSets.Lokace();
		}

		public StatusInfo KatalogLokaceExport(Server.Interfaces.Classes.Terminal terminal, Sklad sklad, ref StatusObject so)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IMeny Members

		public Fask.DataSets.Meny KatalogMen(Fask.Server.Interfaces.Classes.Terminal terminal)
		{
			
			return new Fask.DataSets.Meny();
		}

		public Fask.Server.Interfaces.Classes.StatusInfo KatalogMenExport(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
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

		#endregion

		#region Private metody


		/// <summary>
		/// Metoda pro export Skladu
		/// </summary>
		/// <param name="so">reference na statusObjekt</param>
		/// <returns>OK anebo chyba</returns>
		public string ExportKatalogSkladyABRAFirebird(ref StatusObject so)
		{
			try
			{
				
				//string pom = string.Empty;

				//so.Write("export zahajen");
				////\TODO: vybirat jen nektere sloupce
				////Datasets.DatabasePohoda.sSkladDataTable ssklad_dt = ssklad_ta.GetData();
				SQL_Datasets.Ciselniky.CZMST093DataTable ssklad_dt = Database.ABRA.Sklad_GetData();


				SQL_Datasets.CiselnikyTableAdapters.CZMST093TableAdapter  CZMST_093TableAdapter = new Fask.Module.ABRA.CarpServise.SQL_Datasets.CiselnikyTableAdapters.CZMST093TableAdapter();
				CZMST_093TableAdapter.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;

				CZMST_093TableAdapter.DeleteQuery();

				SQL_Datasets.Ciselniky sklady = new SQL_Datasets.Ciselniky();

				string skl_id;
				string skl_desc;
				string skl_typ;
				string skl_carcode;


				int skl_id_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_id"].MaxLength;
				int skl_desc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_desc"].MaxLength;
				int skl_typ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_typ"].MaxLength;
				int skl_carcode_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_carcode"].MaxLength;


				so.Write("export polozek z pohody do databaze");

				foreach (var item in ssklad_dt)
				{
					skl_id = item.skl_id;
					skl_desc = item.Isskl_descNull() ? "" : item.skl_desc.Trim();
					skl_typ = item.Isskl_typNull() ? "" : item.skl_typ.Trim();
					skl_carcode = item.Isskl_carcodeNull() ? "" : item.skl_carcode.Trim();

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
		/// 
		/// </summary>
		/// <param name="so"></param>
		/// <returns></returns>
		private string ExportKatalogZasobyABRAFirebird(ref StatusObject so)
		{
			SqlTransaction trans = null;
			SqlConnection connection = null;

			//Datasets.DatabasePohoda.SKzDataTable skzDataTable = new Datasets.DatabasePohoda.SKzDataTable();
			//Datasets.DatabasePohoda.SKzAlternativesDataTable skzalternativesDT = new Datasets.DatabasePohoda.SKzAlternativesDataTable();
			ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable zasobyDT = new ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable();

			try
			{
				Globals_V1.LoadConfiguration();

				// Možna TODO, chystrejší konfiguračne filtry...

				//if (!String.IsNullOrEmpty(Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter) || !String.IsNullOrEmpty(Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter))
				//{
				//	if (Globals_V1.Konfigurace.ExportZasoby[0].ExportovatPouzeAktivniPolozky)
				//	{

				//		Database.Pohoda.SKz_FillBy_EPAP_DAD(skzDataTable, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);

				//		//dotazeni alternativ
				//		if (Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele)
				//		{
				//			Database.Pohoda.SKzAlternatives_FillBy_EPAP_DAD(skzalternativesDT, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);
				//		}
				//		else
				//			skzalternativesDT = new Datasets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...

				//	}
				//	else
				//	{
				//		Database.Pohoda.SKz_FillBy_DAD(skzDataTable, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);

				//		//dotazeni alternativ
				//		if (Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele)
				//		{
				//			Database.Pohoda.SKzAlternatives_FillBy_DAD(skzalternativesDT, Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter, Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter);
				//		}
				//		else
				//			skzalternativesDT = new Datasets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...
				//	}
				//}
				//else
				//{
				//	if (Globals_V1.Konfigurace.ExportZasoby[0].ExportovatPouzeAktivniPolozky)
				//	{
				//		skzDataTable = Database.Pohoda.SKz_GetDataByAktivniPolozky();
				//		//dotazeni alternativ
				//		skzalternativesDT = Database.Pohoda.SKzAlternatives_GetDataByAktivni();
				//	}
				//	else
				//	{
				//		skzDataTable = Database.Pohoda.SKz_GetDataByOptimalize();
				//		//dotazeni alternativ
				//		if (Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele)
				//		{
				//			skzalternativesDT = Database.Pohoda.SKzAlternatives_GetData();
				//		}
				//		else
				//			skzalternativesDT = new Datasets.DatabasePohoda.SKzAlternativesDataTable(); // toto musi existovat ...

				//	}
				//}

				zasobyDT = Database.ABRA.Zasoby_GetData(Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter);
				

				connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				connection.Open();
				trans = connection.BeginTransaction();

				SQL_Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter FASK_ZASOBY_ta = new SQL_Datasets.ZboziTableAdapters.FASK_ZASOBYTableAdapter();
				FASK_ZASOBY_ta.Connection.ConnectionString = Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB;
				FASK_ZASOBY_ta.Connection = trans.Connection;

				FASK_ZASOBY_ta.Transaction = trans;
				FASK_ZASOBY_ta.DeleteQuery();

				SQL_Datasets.Zbozi zbozi = new SQL_Datasets.Zbozi();

				string ITEMNMBR;
				string ITEMDESC;
				string ITEMCODE;
				string VNDITNUM;
				string CZ_CarKod;
				string SKL_ID;
				string LOCNCODE;
				decimal QTY;
				decimal QTYPACK;
				string MJ;
				decimal? WEIGHT;

				int polCelkem = zasobyDT.Count;
				int polProgress = 0;
				so.Write("export polozek z pohody do databaze (" + polCelkem + ")");

				int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["ITEMDESC"].MaxLength;
				int LOCNCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["LOCNCODE"].MaxLength;
				int SKL_ID_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["SKL_ID"].MaxLength;
				int VNDITNUM_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["VNDITNUM"].MaxLength;
				int CZ_CarKod_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["CZ_CarKod"].MaxLength;
				int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["MJ"].MaxLength;
				int ITEMNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["ITEMNMBR"].MaxLength;
				int ITEMCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Zbozi.ColumnsInfo_FASK_ZASOBY["ITEMCODE"].MaxLength;


				foreach (var item in zasobyDT)
				{

					var Row = zbozi.FASK_ZASOBY.NewFASK_ZASOBYRow();

					polProgress++;
					if ((polProgress % 100) == 0)
						so.Write("export polozek z pohody do databaze (" + polProgress + "/" + polCelkem + ")");

					#region Logiky a orezavačky delky


					ITEMNMBR = item.ITEMNMBR;
					ITEMDESC = item.IsITEMDESCNull() ? "" : item.ITEMDESC;
					ITEMCODE = item.IsITEMCODENull() ? "" : item.ITEMCODE;
					VNDITNUM = item.IsVNDITNUMNull() ? "" : item.VNDITNUM;
					CZ_CarKod = item.IsCZ_CarKodNull() ? "" : item.CZ_CarKod;
					SKL_ID = item.IsSKL_IDNull() ? "" : item.SKL_ID;
					LOCNCODE = item.IsLOCNCODENull() ? "" : item.LOCNCODE;
					QTY = item.QTY;
					QTYPACK = item.IsQTYPACKNull() ? 0 : item.QTYPACK;
					MJ = item.MJ;
					WEIGHT = item.IsWEIGHTNull() ? (decimal?)null : item.WEIGHT;


					if (ITEMNMBR.Length > ITEMNMBR_MaxLength)
						ITEMNMBR = ITEMNMBR.Remove(ITEMNMBR_MaxLength);


					if (ITEMDESC.Length > ITEMDESC_MaxLength)
						ITEMDESC = ITEMDESC.Remove(ITEMDESC_MaxLength);

					if (ITEMCODE.Length > ITEMCODE_MaxLength)
						ITEMCODE = ITEMCODE.Remove(ITEMCODE_MaxLength);

					if (VNDITNUM.Length > VNDITNUM_MaxLength)
						VNDITNUM = VNDITNUM.Remove(VNDITNUM_MaxLength);

					if (CZ_CarKod.Length > CZ_CarKod_MaxLength)
						CZ_CarKod = CZ_CarKod.Remove(CZ_CarKod_MaxLength);

					if (SKL_ID.Length > SKL_ID_MaxLength)
						SKL_ID = SKL_ID.Remove(SKL_ID_MaxLength);

					if (LOCNCODE.Length > LOCNCODE_MaxLength)
						LOCNCODE = LOCNCODE.Remove(LOCNCODE_MaxLength);

					if (MJ.Length > MJ_MaxLength)
						MJ = MJ.Remove(MJ_MaxLength);


					byte CZ_SerNum_Track = 0;

					#endregion



					Row.ITEMNMBR = ITEMNMBR;
					Row.ITEMDESC = ITEMDESC;
					Row.ITEMCODE = ITEMCODE;
					Row.VNDITNUM = VNDITNUM;
					Row.CZ_CarKod = CZ_CarKod;
					Row.LOCNCODE = LOCNCODE;
					Row.SKL_ID = SKL_ID;
					Row.QTY = QTY;
					Row.QTYPACK = QTYPACK;
					Row.MJ = MJ;
					Row.DMJ = string.Empty;
					Row.TAXRATE = 0;
					Row.PRICE0 = 0;
					Row.PRICE1 = 0;
					Row.PRICE2 = 0;
					Row.PRICE3 = 0;
					Row.PRICE4 = 0;
					Row.PRICE5 = 0;
					Row.CZ_SerNum_Track = CZ_SerNum_Track;
					Row.CZ_SerNum_Delka = 0;
					Row.CZ_Rez1_Track = 0;
					Row.CZ_Rez2_Track = 0;
					Row.CZ_Rez3_Track = 0;
					Row.CZ_Rez4_Track = 0;
					Row.REZ1 = string.Empty;
					Row.REZ2 = string.Empty;
					Row.REZ3 = string.Empty;
					Row.REZ4 = string.Empty;
					Row.ODB_ID = string.Empty;
					Row.mena_ID = string.Empty;
					Row.SERLTNUM = string.Empty;

					if (WEIGHT.HasValue)
						Row.WEIGHT = WEIGHT.Value;
					else
						Row.SetWEIGHTNull();
					
					Row.SetTIMEFROMNull();
					Row.SetTIMETONull();
					Row.LSTMod = DateTime.Now;
					Row.loginid = string.Empty;


					zbozi.FASK_ZASOBY.AddFASK_ZASOBYRow(Row);

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

				//Fask.Logging.ExceptionHandler2.Handle(skzDataTable);
				//Fask.Logging.ExceptionHandler2.Handle(skzalternativesDT);
				//Fask.Logging.ExceptionHandler2.Handle(skzparametryDT);


				if (trans != null) trans.Rollback();

				throw ex;
			}
			finally
			{
				if (connection != null && connection.State == System.Data.ConnectionState.Open)
					connection.Close();
			}
		}

        public StatusInfo KatalogTypDokladuExport(Server.Interfaces.Classes.Terminal terminal, Sklad sklad, ref StatusObject so)
        {
            throw new NotImplementedException();
        }





        #endregion



    }
}
