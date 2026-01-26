using Fask.Server.Interfaces.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes
{
    class TransformerCiselniky
    {
		/// <summary>
		/// Metoda pro export Skladu
		/// </summary>
		/// <param name="so">reference na statusObjekt</param>
		/// <returns>OK anebo chyba</returns>
		public static string ExportKatalogSkladyABRAFirebird(ref StatusObject so)
		{
			SqlTransaction trans = null;
			SqlConnection connection = null;

			try
			{
				Globals_V1.LoadConfiguration();

				//Dotažení dat z DB ABRA
				SQL_Datasets.Ciselniky.CZMST093DataTable ssklad_dt = Database.ABRA.Sklad_GetData();


				using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					connection.Open();
					trans = connection.BeginTransaction();

					//Smazani puvodneho čisleniku skladu
					Database.Ciselniky.Delete_CZMST093(connection, trans);


					SQL_Datasets.Ciselniky sklady = new SQL_Datasets.Ciselniky();

					string skl_id;
					string skl_desc;
					string skl_typ;
					string skl_carcode;


					int skl_id_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_id"].MaxLength;
					int skl_desc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_desc"].MaxLength;
					int skl_typ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_typ"].MaxLength;
					int skl_carcode_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Sklady.ColumnsInfo_CZMST093["skl_carcode"].MaxLength;


					so.Write("Export polozek z DB ABRY do DB FASK");

					foreach (var item in ssklad_dt)
					{
						skl_id = item.skl_id;
						skl_desc = item.Isskl_descNull() ? "" : item.skl_desc.Trim();
						skl_typ = item.Isskl_typNull() ? "" : item.skl_typ.Trim();
						skl_carcode = item.Isskl_carcodeNull() ? "" : item.skl_carcode.Trim();

						sklady.CZMST093.AddCZMST093Row(skl_id, skl_desc, skl_typ, skl_carcode);

					}

					Database.Ciselniky.Update_CZMST093(sklady.CZMST093, connection, trans);

					if (trans != null)
						trans.Commit();

					so.Write("export se provedl uspesne");

				}
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();

				throw ex;
			}
			finally
			{
				if (connection != null && connection.State == System.Data.ConnectionState.Open)
					connection.Close();
			}
			return "OK";
		}

		/// <summary>
		/// Metoda pro export zasob
		/// </summary>
		/// <param name="so"></param>
		/// <returns></returns>
		public static string ExportKatalogZasobyABRAFirebird(ref StatusObject so)
		{
			SqlTransaction trans = null;
			SqlConnection connection = null;

			ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable zasobyDT = new ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable();

			try
			{
				Globals_V1.LoadConfiguration();

				zasobyDT = Database.ABRA.Zasoby_GetData(Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter);

				using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					connection.Open();
					trans = connection.BeginTransaction();

					Database.Zbozi.Delete_FASK_ZASOBY(connection, trans);

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

						if(item.CZ_SerNum_Track < 0 || item.CZ_SerNum_Track > 2)
						{
							string msg = string.Format("Byl proveden pokus o export do FASK_ZASOBY položky, která ma nepodporovaný příznak sledování :'{0}' " + Environment.NewLine , item.CZ_SerNum_Track);
							msg += string.Format("Položka : " + Environment.NewLine + "		ITEMDESC:{0}" + Environment.NewLine + "		ITEMNMBR:{1}" + Environment.NewLine + "		ITEMCODE:{2}" + Environment.NewLine + "		SKL_ID:{3}", item.ITEMDESC, item.ITEMNMBR, item.ITEMCODE, item.SKL_ID);
							Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, msg);

							continue;
						}

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


						byte CZ_SerNum_Track = item.CZ_SerNum_Track;

						//Po uprave SQL dotahu tohle odkomentovat
						byte CZ_Expirace_Track = item.CZ_Expirace_Track;

						//a tohle smazat
						//byte CZ_Expirace_Track = 0;

						//if (CZ_SerNum_Track == 2)
						//	CZ_Expirace_Track = 1;



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

						Row.CZ_Expirace_Track = CZ_Expirace_Track;
						Row.SetExpiraceNull();

						zbozi.FASK_ZASOBY.AddFASK_ZASOBYRow(Row);

					}

					so.Write("vkladani polozek z pameti do databaze (" + zbozi.FASK_ZASOBY.Count + ")");

					Database.Zbozi.Update_FASK_ZASOBY(zbozi.FASK_ZASOBY, connection, trans);

					so.Write("export se provedl uspesne");

					if (trans != null)
						trans.Commit();

				}

				return "OK";
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();

				throw ex;
			}
			finally
			{
				if (connection != null && connection.State == System.Data.ConnectionState.Open)
					connection.Close();
			}
		}


		#region Lokace
		/// <summary>
		/// Metoda pro export lokací
		/// </summary>
		/// <param name="so"></param>
		/// <returns></returns>
		public static string ExportKatalogLokaceABRAFirebird(Sklad sklad, ref StatusObject so)
		{
			Globals_V1.LoadConfiguration();
			string sklad_ID = string.Empty;

			if (sklad != null)
				sklad_ID = sklad.ID;

			// 1) Načti data z ABRA (read-only, bez vlivu na níže vytvořenou SQL transakci)
			ABRA_Datasets.Lokace.Lokace_Ciselnik_Pozic_ABRADataTable dtAbra = new ABRA_Datasets.Lokace.Lokace_Ciselnik_Pozic_ABRADataTable();

			try
            {
                dtAbra = Database.ABRA.Lokace_Ciselnik_Pozic_ABRA_GetData(sklad_ID);
            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle("ABRA ziskani dat", "Lokace_Ciselnik_Pozic_ABRA_GetData", ex);
				so.SetException(ex);
				return "ABRA_CHYBA";
            }
			
			if (dtAbra.Count == 0)
			{
				return "ABRA_NO_DATA";
				//throw new Exception("Žádná data pro export");
			}

			int polCelkem = dtAbra.Count;
			int polProgress = 0;
			so.Write("export polozek z ABRA do databaze (" + polCelkem + ")");

			// 2) Připrav si cílovou tabulku datasetu pro CZMST094
			var dtCzmst094 = new SQL_Datasets.Lokace.CZMST094DataTable();
			foreach (var item in dtAbra)
			{
				var row = dtCzmst094.NewCZMST094Row();
				
				polProgress++;
				if ((polProgress % 100) == 0)
					so.Write("export polozek z ABRA do databaze (" + polProgress + "/" + polCelkem + ")");

				// POZOR na datové typy: přizpůsobte podle schématu SQL
				row.SKL_ID = item.IsSTORE_IDNull() ? string.Empty : item.STORE_ID;  // pokud je v SQL numerický, převést
				row.LOCNCODE = item.CODE;
				row.Description = item.IsNAMENull() ? string.Empty : item.NAME;
				row.Barcode = item.IsBARCODENull() ? string.Empty : item.BARCODE;

				dtCzmst094.AddCZMST094Row(row);
			}

			// 3) Transakční blok: mazání + zápis na jednom spojení
			using (var connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
			{
				connection.Open();

				// Zvolte si vhodnou úroveň izolace; ReadCommitted bývá OK
				using (var trans = connection.BeginTransaction(IsolationLevel.ReadCommitted))
				{
					try
					{
						// Mazání musí jet v rámci stejné transakce i stejného připojení
						// => upravte signaturu metody, ať přijímá connection + trans
						Database.Vydej.Delete_CZMST094_Core(connection, trans, string.Empty, string.Empty);

						so.Write("vkladani polozek z pameti do databaze (" + polCelkem + ")");
						// Vložit/aktualizovat data
						Database.Vydej.Update_CZMST094(dtCzmst094, connection, trans);

						trans.Commit();
						so.Write("export se provedl uspesne");
						return "OK";
					}
					catch
					{
						// Rollback chránit vlastním try/catch, ať nepřepíšete původní výjimku
						try { trans.Rollback(); } catch { /* logujte pokud chcete */ }
						throw; // zachovej původní stacktrace
					}
				}
			}


		}

	
		#endregion





	}
}
