using Fask.Logging;
using Fask.Module.ABRA.SAB.Classes.ABRA_BO;
using Fask.Server.Interfaces.Classes;
using Fask.Tracing;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Classes
{
	class ABRA
	{
		#region Vydej

		#region Hlavne metody

		/// <summary>
		/// Metoda pro exportovaní dat z IS ABRA do FASK, předlodo co CZMST_SE
		/// </summary>
		/// <param name="objednavka">Objekt objednavky</param>
		/// <param name="sklad">objekt skladu</param>
		/// <returns></returns>
		public static string ExportVydejka_ABRA_Z_DodaciList(Objednavka objednavka, Sklad sklad)
		{
			SqlTransaction trans = null;
			SqlConnection connection = null;

			try
			{

				if (String.IsNullOrEmpty(objednavka.ID.Trim()))
					throw new Exception("Číslo dokladu nesmí být prázdné");

				ABRA_Datasets.Vydej.CZMST_SEDataTable dt_abra = Database.ABRA.DodaciList_GetData(objednavka.ID, sklad.ID);

				if (dt_abra == null || dt_abra.Count == 0)
				{
					throw new Exception("Nenalezen doklad pro export");
				}

				int cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries();
				cisloDavky += 1;


				using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					connection.Open();
					trans = connection.BeginTransaction();

					SQL_Datasets.Vydej.CZMST_SEDataTable seTable = new SQL_Datasets.Vydej.CZMST_SEDataTable();

					int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
					int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;
					int VNDDOCNM_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["VNDDOCNM"].MaxLength;

					#region Foreach

					foreach (var Item in dt_abra)
					{

						if (Item.CZ_SerNum_Track < 0 || Item.CZ_SerNum_Track > 2)
						{
							string msg = string.Format("Byl proveden pokus o export do CZMST_SE položky, která ma nepodporovaný příznak sledování :'{0}' " + Environment.NewLine, Item.CZ_SerNum_Track);
							msg += string.Format("Položka : " + Environment.NewLine + "		ITEMDESC:{0}" + Environment.NewLine + "		ITEMNMBR:{1}" + Environment.NewLine + "		ITEMCODE:{2}" + Environment.NewLine + "		SKL_ID:{3}", Item.ITEMDESC, Item.ITEMNMBR, Item.ITEMCODE, Item.SKL_ID);
							Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, msg);

							continue;
						}

						var seRow = seTable.NewCZMST_SERow();

						seRow.ITEMDESC = Item.IsITEMDESCNull() ? null : Item.ITEMDESC;
						if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
						{
							seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
						}


						seRow.VNDDOCNM = Item.IsVNDDOCNMNull() ? "" : Item.VNDDOCNM;
						if (seRow.VNDDOCNM.Length > VNDDOCNM_MaxLength)
						{
							seRow.VNDDOCNM = seRow.VNDDOCNM.Remove(VNDDOCNM_MaxLength);
						}

						seRow.ITEMNMBR = Item.ITEMNMBR;
						seRow.ITEMTYPE = Item.IsITEMTYPENull() ? string.Empty : Item.ITEMTYPE;
						seRow.ITEMCODE = Item.IsITEMCODENull() ? null : Item.ITEMCODE;
						seRow.CountEntries = cisloDavky;

						seRow.CZ_CarKod = Item.IsITEMCODENull() ? null : Item.ITEMCODE;
						seRow.CZ_DatVyr_Delka = 0;
						seRow.CZ_DatVyr_Track = 0;
						seRow.CZ_Doslo = 0;
						seRow.CZ_SerNum_Delka = 0;
						seRow.CZ_SerNum_Track = Item.CZ_SerNum_Track; // TODO ??

						//Po uprave SQL tohle smazat
						//if (Item.CZ_SerNum_Track == 2)
						//	seRow.CZ_Expirace_Track = 1;
						//else
						//	seRow.CZ_Expirace_Track = 0;

						// a tohle odkomentovat
						seRow.CZ_Expirace_Track = Item.CZ_Expirace_Track;

						seRow.CZ_SW_Delka = 0;
						seRow.CZ_SW_Track = 0;

						seRow.LOCNCODE = string.Empty;
						seRow.Note = string.Empty;
						seRow.ORD = Item.ORD;
						seRow.PRINTED = 0;
						seRow.PRIORITY = 3;
						seRow.QTYPACK = Item.IsQTYPACKNull() ? 0 : Item.QTYPACK;
						seRow.QTYPAL = 0;
						seRow.QTYSHPPD = Item.QTYSHPPD;
						seRow.SKL_ID = Item.SKL_ID;
						seRow.SOPNUMBE = Item.SOPNUMBE;
						seRow.TYPEPAL = "";

						seRow.VNDITNUM = Item.VNDITNUM;
						seRow.MJ = Item.MJ;
						if (seRow.MJ.Length > MJ_MaxLength)
						{
							seRow.MJ = seRow.ITEMDESC.Remove(MJ_MaxLength);
						}

						//seRow.USERID = null;
						seRow.CZ_REZ1_Track = 0;
						seRow.CZ_REZ2_Track = 0;

						seRow.SetWEIGHTNull();

						seTable.AddCZMST_SERow(seRow);
					}

					#endregion

					Database.Vydej.Update_CZMST_SE(seTable, connection, trans);

					if (trans != null)
						trans.Commit();

					objednavka.CisloDavky = cisloDavky.ToString();
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


		#region 22.9.2025 MaR stara metoda se spatnou logikou transakce
		///// <summary>
		///// Metoda pro exportovaní dat z IS ABRA do FASK, tabulka dat do CZMST094
		///// </summary>
		///// <param name="sklad">objekt skladu</param>
		///// <returns></returns>
		//public static string ExportData_CZMST094_Z_ABRA(Objednavka objednavka, Sklad sklad)
		//{
		//    SqlTransaction trans = null;
		//    SqlConnection connection = null;

		//    try
		//    {


		//        //17.9.2025 zmenit metodu na vybrani dat pomoci SELECT nad tabulkou ABRA-LogStorePositions 
		//        ABRA_Datasets.Lokace.Lokace_Ciselnik_Pozic_ABRADataTable dt_abra = Database.ABRA.Lokace_Ciselnik_Pozic_ABRA_GetData(sklad.ID);



		//        if (dt_abra == null || dt_abra.Count == 0)
		//        {
		//            throw new Exception("Žádná data pro export");
		//        }

		//        //15.9.2025 MaR smazani zaznamu v CZMST094
		//        Database.Vydej.Delete_CZMST094_vTransakci(string.Empty, string.Empty);

		//        using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
		//        {
		//            connection.Open();
		//            trans = connection.BeginTransaction();

		//            SQL_Datasets.Lokace.CZMST094DataTable dt_CZMST094 = new SQL_Datasets.Lokace.CZMST094DataTable();


		//            #region Foreach

		//            foreach (var Item in dt_abra)
		//            {

		//                var row_CZMST094 = dt_CZMST094.NewCZMST094Row();

		//                row_CZMST094.SKL_ID = Item.IsSTORE_IDNull() ? string.Empty : Item.STORE_ID;
		//                row_CZMST094.LOCNCODE = Item.CODE;
		//                //row_CZMST094.TYPE = Item.IsStore_IDNull() ? null : Item.Store_ID;
		//                row_CZMST094.Description = Item.IsNAMENull() ? string.Empty : Item.NAME;
		//                row_CZMST094.Barcode = Item.IsBARCODENull() ? string.Empty : Item.BARCODE;


		//                dt_CZMST094.AddCZMST094Row(row_CZMST094);
		//            }

		//            #endregion

		//            Database.Vydej.Update_CZMST094(dt_CZMST094, connection, trans);

		//            if (trans != null)
		//                trans.Commit();
		//        }

		//        return "OK";
		//    }
		//    catch (Exception ex)
		//    {
		//        if (trans != null) trans.Rollback();

		//        throw ex;
		//    }
		//    finally
		//    {
		//        if (connection != null && connection.State == System.Data.ConnectionState.Open)
		//            connection.Close();
		//    }
		//} 

		#endregion

		#region 22.9.2025 MaR nova metoda z AI pro spravnou funkci SQL transakce


		public static string ExportData_CZMST094_Z_ABRA(Objednavka objednavka, Sklad sklad)
		{
			if (sklad == null) throw new ArgumentNullException(nameof(sklad));

			// 1) Načti data z ABRA (read-only, bez vlivu na níže vytvořenou SQL transakci)
			var dtAbra = Database.ABRA.Lokace_Ciselnik_Pozic_ABRA_GetData(sklad.ID);
            if (dtAbra == null || dtAbra.Count == 0)
            {
                throw new Exception("Žádná data pro export");
            }

            // 2) Připrav si cílovou tabulku datasetu pro CZMST094
            var dtCzmst094 = new SQL_Datasets.Lokace.CZMST094DataTable();
			foreach (var item in dtAbra)
			{
				var row = dtCzmst094.NewCZMST094Row();

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

						// Vložit/aktualizovat data
						Database.Vydej.Update_CZMST094(dtCzmst094, connection, trans);

						trans.Commit();
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


		/// <summary>
		/// Puvodní verze metody pro odvadení na výdeji
		/// </summary>
		/// <param name="countEntries"></param>
		/// <param name="SKL_ID"></param>
		/// <param name="SOPNUMBE"></param>
		/// <param name="note"></param>
		/// <returns></returns>
		//public static string Vydej_ImportFaktura_ABRA(int countEntries, string SKL_ID, string SOPNUMBE, string note)
		//{
		//	try
		//	{
		//		string ID_DLtmp = null;
		//		string pom = string.Empty;
		//		pom = Globals_V1.LoadConfiguration();

		//		if (pom != "OK")
		//			return pom;

		//		Fask.DataSets.Vydej dsV = new Fask.DataSets.Vydej();
		//		string selectSI = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SI + " WHERE CountEntries='" + countEntries + "'";
		//		Database.Vydej.Fill_DataSet(selectSI, dsV, dsV.CZMST_SI.TableName); //Vrati data dle SELECTU

		//		string selectSE = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SE + " WHERE CountEntries='" + countEntries + "'";
		//		Database.Vydej.Fill_DataSet(selectSE, dsV, dsV.CZMST_SE.TableName); //Vrati data dle SELECTU

		//		//První request do ABRY, import FV z DL
		//		Classes.ABRA_BO.Rootobject_FV FV_Objekt = Import_DL_to_FV(dsV, SKL_ID, SOPNUMBE);

		//		//Druhy request do IS ABRA, uprava QTY v DL
		//		string s = Edit_QTY_in_FV(FV_Objekt, dsV);

		//		//Tretí request do IS ABRA, zmena stavu DL
		//		string ID_DL = Database.ABRA.Get_ID_DodaciList(SOPNUMBE, SKL_ID);
		//		pom = Classes.ABRA.Edit_Stav_DL(Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Vydej_Vyskladneno.Trim(), ID_DL);

		//		if (pom != "OK")
		//			throw new Exception(pom);

		//		//Kontrola Vykriti + pripadne čtvrtý request na nový DL
		//		List<Polozky_Vydej> pol = new List<Polozky_Vydej>();
		//		bool stav = Classes.Vydej.KontrolaVykriti(dsV, out pol);

		//		if (stav)
		//		{
		//			pom = Create_DL(pol, out ID_DLtmp);
		//		}

		//		return pom;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}
		//}

		/// <summary>
		/// Aktualná metoda pro praci s dokladama po odeslaní dávky
		/// </summary>
		/// <param name="countEntries">číslo dávky</param>
		/// <param name="SKL_ID">ID skladu</param>
		/// <param name="SOPNUMBE">Označení objednávky</param>
		/// <param name="note">poznamka</param>
		/// <returns></returns>
		public static string Vydej_Import_ABRA_DL(int countEntries, string SKL_ID, string SOPNUMBE, string note)
		{
			try
			{
				string pom = string.Empty;
				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				Fask.DataSets.Vydej dsV = new Fask.DataSets.Vydej();
				string selectSI = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SI + " WHERE CountEntries='" + countEntries + "'";
				if (Database.Vydej.Fill_DataSet(selectSI, dsV, dsV.CZMST_SI.TableName) == -1) //Vrati data dle SELECTU
				{
					Logging.ExceptionHandler2.Handle(new Exception("Nastala chyba při plnění :'" + Constants.Common.TABLE_CZMST_SI + "'"));
				}

				string selectSE = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SE + " WHERE CountEntries='" + countEntries + "'";
				if (Database.Vydej.Fill_DataSet(selectSE, dsV, dsV.CZMST_SE.TableName) == -1) //Vrati data dle SELECTU
				{
					Logging.ExceptionHandler2.Handle(new Exception("Nastala chyba při plnění :'" + Constants.Common.TABLE_CZMST_SE + "'"));
				}

				//Postup :
				//1) Zeditovat DL, upravit množství kompletne nevydaných, smazat vubec nevydane
				//2) Vytvořit fakturu z modifikovaneho DL
				//3) Zmenit stav v procesním řizení
				//4) Pokud je nejaký rozdíl medzi predlohu a nasnimanima tak překlopit OP do DL, mnel by se vytvořit rozdilový DL, 
				// Dotaz, jak nastavit stav už při překlopení?


				// 16.3.2022
				// TaD
				// Zmena pořadi
				// 3) Nejdriv zeditovat stav proces řizeni
				// 1) nasledne editovat DL
				// 2) pokud se chce tak vygenerovat fakturu
				// 4) Pokud je nejaký rozdíl medzi predlohu a nasnimanima tak překlopit OP do DL, mnel by se vytvořit rozdilový DL, 
				// Dotaz, jak nastavit stav už při překlopení?

				List<string> listOP = new List<string>();
				string ID_DL = Database.ABRA.Get_ID_DodaciList(SOPNUMBE, SKL_ID);


				//Tretí request do IS ABRA, zmena stavu DL
				pom = Classes.ABRA.Edit_Stav_DL(Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Vydej_Vyskladneno.Trim(), ID_DL.Trim());


				//Prvni request do IS ABRA, uprava QTY v DL
				string s = Edit_Hlavny_DL(dsV, ID_DL.Trim(), out listOP);

				//Druhy request do ABRY, import celeho DL do FV
				//Zde pridat parametr pro vypnuti tvorby faktury
				if (Globals_V1.Konfigurace.Vydej[0].GenerovatFakturu)
				{

					var IS_CREATEINVOICE = Database.ABRA.Get_IS_CREATEINVOICE_By_IDPRV(ID_DL.Trim());

					if (IS_CREATEINVOICE == "A")
					{
						string DocQueueID_FV = Database.ABRA.Get_DocQueueID_FV_By_IDPRV(ID_DL.Trim());
						Classes.ABRA_BO.Rootobject_FV FV_Objekt = Import_DL_to_FV_all(ID_DL.Trim(), SOPNUMBE.Trim(), DocQueueID_FV);
					}
				}

				if (pom != "OK")
					throw new Exception(pom);

				//Kontrola Vykriti + pripadne čtvrtý request na nový DL

				if (listOP.Count() > 0)
				{
					string ID_DLedit = string.Empty;
					if (listOP.Count() == 1)
					{
						pom = Create_DL_all(listOP[0], out ID_DLedit);
					}
					else
					{
						//TODO : 
						pom = "Nalezeno vicero OP";
					}

					if (pom != "OK")
						throw new Exception(pom);

					pom = Classes.ABRA.Edit_Stav_DL(Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Vydej_KeKontrole.Trim(), ID_DLedit.Trim());

					if (pom != "OK")
						throw new Exception(pom);
				}

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region Aktivne Použivaji se

		/// <summary>
		/// Metoda slouží k Editaci DL ze kterým se pracuje
		/// </summary>
		/// <param name="dsV"></param>
		/// <param name="iD_DL"></param>
		/// <param name="listOP"></param>
		/// <returns></returns>
		private static string Edit_Hlavny_DL(DataSets.Vydej dsV, string iD_DL, out List<string> listOP)
		{
			try
			{
				listOP = new List<string>();
				var polEdit = new List<Polozky_Vydej>();
				var polDelete = new List<Polozky_Vydej>();
				Classes.Vydej.KontrolaVykriti_A_Smazani(dsV, out polEdit, out polDelete, out listOP);

				if (polEdit.Count() > 0)
				{
					if (Edit_SERLTNUM_QTY_in_DL(iD_DL, polEdit) != "OK")
					{
						throw new Exception("Nelze editovat DL:'" + iD_DL + "'");
					}
				}

				if (polDelete.Count() > 0)
				{
					foreach (Polozky_Vydej item in polDelete)
					{
						if (Delete_in_DL(iD_DL, item) != "OK")
							throw new Exception("Nelze smazat ITEMNMBR:'" + item.ITEMNMBR + "'");
					}
				}

				return "OK";
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}

		}

		/// <summary>
		/// Metoda pomoci ktere se překlopí OP do DL a vznikne tym rozdilovy DL
		/// </summary>
		/// <param name="polozky"></param>
		/// <param name="ID_DL"></param>
		/// <returns></returns>
		public static string Create_DL_all(string ID_OP, out string ID_DL)
		{
			try
			{
				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Create_DL_all(G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				//string ID = Database.ABRA.Get_ID_OP(polozky);

				if (string.IsNullOrEmpty(ID_OP))
					throw new Exception("Dodaci list nenalezen...");

				string param =
					Constants.Common.DL +
					Constants.Common.SLASH +
					Constants.Common.IMPORT +
					Constants.Common.SLASH +
					Constants.Common.OP +
					Constants.Common.SLASH +
					ID_OP;

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Create_DL_all", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.POST, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				ID_DL = null;

				return Classes.WEBAPI.RequestResponse.LoadResponse_Create_DL_all(restResponse, G, out ID_DL);

			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;

			}
		}

		/// <summary>
		/// Metoda pomoci které se překlopí upraveny Dodací List do Faktury Vydanej
		/// </summary>
		/// <param name="ID_DL">ID Dodaciho Listu</param>
		/// <returns></returns>
		private static Rootobject_FV Import_DL_to_FV_all(string ID_DL, string SOPNUMBE, string DocQueueID_FV)
		{
			IRestResponse restResponse;

			string JSON = string.Empty;
			Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

			string ID_OP = Database.ABRA.Get_ID_OP(SOPNUMBE);

			if (string.IsNullOrEmpty(ID_OP))
				throw new Exception("Objednava přijatá nenalezena...");

			if (!Classes.WEBAPI.RequestResponse.CreateRequest_Import_DL_to_FV_all(G, ID_OP,DocQueueID_FV, out JSON))
			{
				throw new Exception("Vytvoření requestu se nezdařilo");
			}

			if (string.IsNullOrEmpty(ID_DL))
				throw new Exception("Dodaci list nenalezen...");

			string param =
				Constants.Common.FV +
				Constants.Common.SLASH +
				Constants.Common.IMPORT +
				Constants.Common.SLASH +
				Constants.Common.DL +
				Constants.Common.SLASH +
				ID_DL;

			if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
			{
				SAB.Constants.SaveToFile.Save(Constants.Common.URL, "DL_to_FV_all", param, G, Constants.Common.txt);
			}

			if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.POST, out restResponse, param, JSON))
			{
				throw new Exception("Komunikace s IS ABRA se nezdařila");
			}

			return Classes.WEBAPI.RequestResponse.LoadResponse_Import_DL_to_FV_all(restResponse, G);

		}

		/// <summary>
		/// Metoda pro editaci Stavu Procesniho řizeni Dodaciho Listu
		/// </summary>
		/// <param name="stav">Stav na ktery se ma přepnout</param>
		/// <param name="iD_DL">ID Dodaciho Listu</param>
		/// <returns></returns>
		internal static string Edit_Stav_DL(string stav, string iD_DL)
		{
			try
			{

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_Stav_DL(stav, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				if (string.IsNullOrEmpty(iD_DL.Trim()))
					throw new Exception("Dodaci List nenalezen...");

				string param =
					Constants.Common.DL +
					Constants.Common.SLASH +
					iD_DL.Trim() +
					Constants.Common.SLASH +
					Constants.Common.PM_Change;

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_Stav_DL", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Edit_Stav_DL(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Pomocne podMetody

		/// <summary>
		/// Metoda pomoci ktere se smaže jeden řadek, jedna položka v Dodacim Listu podle ID
		/// </summary>
		/// <param name="ID_DL">ID Dodaciho listu</param>
		/// <param name="polozky">Objekt položky</param>
		/// <returns></returns>
		private static string Delete_in_DL(string ID_DL, Polozky_Vydej polozky)
		{
			try
			{

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (string.IsNullOrEmpty(ID_DL.Trim()))
					throw new Exception("ID Dodaciho Listu nezadano!");

				string ID_Row_DL = null;

				ID_Row_DL = Database.ABRA.Get_ID_Polozka_DodaciList(polozky);

				if (string.IsNullOrEmpty(ID_Row_DL.Trim()))
					throw new Exception("ID Dodaciho Listu nezadano!");


				string param =
					Constants.Common.DL +
					Constants.Common.SLASH +
					ID_DL.Trim() +
					Constants.Common.SLASH +
					Constants.Common.ROWS +
					Constants.Common.SLASH +
					ID_Row_DL.Trim();

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Delete_in_DL", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.DELETE, out restResponse, param))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Delete_in_DL(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pomoci ktere se meňí Množství u položek v Listu
		/// </summary>
		/// <param name="ID_DL"></param>
		/// <param name="polEdit"></param>
		/// <returns></returns>
		public static string Edit_QTY_in_DL(string ID_DL, List<Polozky_Vydej> polEdit)
		{
			try
			{

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_QTY_in_DL(polEdit, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				if (string.IsNullOrEmpty(ID_DL.Trim()))
					throw new Exception("DL nenalezena...");

				string param =
					Constants.Common.DL +
					Constants.Common.SLASH +
					ID_DL.Trim();

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_QTY_in_DL", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Edit_QTY_in_DL(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		private static string Edit_SERLTNUM_QTY_in_DL(string ID_DL, List<Polozky_Vydej> polEdit)
		{
			try
			{

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_SERLTNUM_QTY_in_DL(polEdit, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				if (string.IsNullOrEmpty(ID_DL.Trim()))
					throw new Exception("DL nenalezena...");

				string param =
					Constants.Common.DL +
					Constants.Common.SLASH +
					ID_DL.Trim();

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_SERLTNUM_QTY_in_DL", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Edit_SERLTNUM_QTY_in_DL(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#endregion

		#endregion

		#region Z puvodni logiky 

		//public static Classes.ABRA_BO.Rootobject_FV Import_DL_to_FV(Fask.DataSets.Vydej dsV, string SKL_ID, string SOPNUMBE)
		//{
		//	IRestResponse restResponse;

		//	string JSON = string.Empty;
		//	Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

		//	if (!Classes.WEBAPI.RequestResponse.CreateRequest_Import_DL_to_FV(dsV, G, out JSON))
		//	{
		//		throw new Exception("Vytvoření requestu se nezdařilo");
		//	}

		//	string ID = Database.ABRA.Get_ID_DodaciList(SOPNUMBE, SKL_ID);

		//	if (string.IsNullOrEmpty(ID))
		//		throw new Exception("Dodaci list nenalezen...");

		//	string param =
		//		Constants.Common.FV +
		//		Constants.Common.SLASH +
		//		Constants.Common.IMPORT +
		//		Constants.Common.SLASH +
		//		Constants.Common.DL +
		//		Constants.Common.SLASH +
		//		ID;

		//	if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
		//	{
		//		SAB.Constants.SaveToFile.Save(Constants.Common.URL, "DL_to_FV", param, G, Constants.Common.txt);
		//	}

		//	if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.POST, out restResponse, param, JSON))
		//	{
		//		throw new Exception("Komunikace s IS ABRA se nezdařila");
		//	}

		//	return Classes.WEBAPI.RequestResponse.LoadResponse_Import_DL_to_FV(restResponse, G);

		//}

		public static string Edit_QTY_in_FV(Classes.ABRA_BO.Rootobject_FV rootobject, Fask.DataSets.Vydej dsV)
		{
			try
			{

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_QTY_in_FV(rootobject, dsV, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				if (string.IsNullOrEmpty(rootobject.id.Trim()))
					throw new Exception("Faktura Vydana nenalezena...");

				string param =
					Constants.Common.FV +
					Constants.Common.SLASH +
					rootobject.id.Trim();

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_QTY_in_FV", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Edit_QTY_in_FV(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}

		}

		public static string Create_DL(List<Polozky_Vydej> pol, out string ID_DL)
		{
			try
			{
				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Create_DL(pol, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				string ID = Database.ABRA.Get_ID_OP(pol[0]);

				if (string.IsNullOrEmpty(ID))
					throw new Exception("Dodaci list nenalezen...");

				string param =
					Constants.Common.DL +
					Constants.Common.SLASH +
					Constants.Common.IMPORT +
					Constants.Common.SLASH +
					Constants.Common.OP +
					Constants.Common.SLASH +
					ID;

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Create_DL", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.POST, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				ID_DL = null;

				return Classes.WEBAPI.RequestResponse.LoadResponse_Create_DL(restResponse, G, out ID_DL);

			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;

			}
		}

		#endregion

		#endregion

		#region Prijem

		#region Hlavne metody

		/// <summary>
		/// Metoda pro exportovaní dat z IS ABRA do FASK, předlodo co CZMST_PE
		/// </summary>
		/// <param name="objednavka">Objekt objednavky</param>
		/// <param name="sklad">objekt skladu</param>
		/// <returns></returns>
		public static string ExportPrijemka_ABRA_Z_PR(Objednavka objednavka, Sklad sklad)
		{
			SqlTransaction trans = null;
			SqlConnection connection = null;

			try
			{

				if (String.IsNullOrEmpty(objednavka.ID.Trim()))
					throw new Exception("Číslo dokladu nesmí být prázdné");

				ABRA_Datasets.Prijem.CZMST_PEDataTable dt_abra = Database.ABRA.Prijemka_GetData(objednavka.ID, sklad.ID);

				if (dt_abra == null || dt_abra.Count == 0)
				{
					throw new Exception("Nenalezen doklad pro export");
				}

				int cisloDavky = Database.Prijem.CZMSTPE_MAX_CountEntries();
				cisloDavky += 1;


				using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					connection.Open();
					trans = connection.BeginTransaction();

					SQL_Datasets.Prijem.CZMST_PEDataTable peTable = new SQL_Datasets.Prijem.CZMST_PEDataTable();

					int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["MJ"].MaxLength;
					int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["ITEMDESC"].MaxLength;

					#region Foreach

					foreach (var Item in dt_abra)
					{

						if (Item.CZ_SerNum_Track < 0 || Item.CZ_SerNum_Track > 2)
						{
							string msg = string.Format("Byl proveden pokus o export do CZMST_PE položky, která ma nepodporovaný příznak sledování :'{0}' " + Environment.NewLine, Item.CZ_SerNum_Track);
							msg += string.Format("Položka : " + Environment.NewLine + "		ITEMDESC:{0}" + Environment.NewLine + "		ITEMNMBR:{1}" + Environment.NewLine + "		ITEMCODE:{2}" + Environment.NewLine + "		SKL_ID:{3}", Item.ITEMDESC, Item.ITEMNMBR, Item.ITEMCODE, Item.SKL_ID);
							Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, msg);

							continue;
						}

						var peRow = peTable.NewCZMST_PERow();

						peRow.CountEntries = cisloDavky;
						peRow.PONUMBER = Item.PONUMBER;
						peRow.ITEMNMBR = Item.ITEMNMBR;

						peRow.ITEMDESC = Item.IsITEMDESCNull() ? string.Empty : Item.ITEMDESC;
						if (peRow.ITEMDESC.Length > ITEMDESC_MaxLength)
						{
							peRow.ITEMDESC = peRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
						}

						peRow.ORD = Item.ORD;
						peRow.VNDDOCNM = String.Empty; // Externe číslo?
						peRow.VNDITNUM = Item.VNDITNUM;
						peRow.CZ_CarKod = Item.IsITEMCODENull() ? string.Empty : Item.ITEMCODE;
						peRow.SKL_ID = Item.SKL_ID;
						peRow.LOCNCODE = string.Empty;
						peRow.MJ = Item.IsMJNull() ? string.Empty : Item.MJ;
						if (peRow.MJ.Length > MJ_MaxLength)
						{
							peRow.MJ = peRow.MJ.Remove(MJ_MaxLength);
						}

						peRow.QTYSHPPD = Item.QTYSHPPD;
						peRow.QTYPACK = Item.IsQTYPACKNull() ? 0 : Item.QTYPACK;
						peRow.CZ_DatVyr_Track = 0;
						peRow.CZ_DatVyr_Delka = 0;
						peRow.CZ_SerNum_Track = Item.CZ_SerNum_Track;

						//if (Item.CZ_SerNum_Track == 2)
						//	peRow.CZ_Expirace_Track = 1;
						//else
						//	peRow.CZ_Expirace_Track = 0;

						peRow.CZ_Expirace_Track = Item.CZ_Expirace_Track;


						peRow.CZ_SerNum_Delka = 0;
						peRow.CZ_SW_Track = 0;
						peRow.CZ_SW_Delka = 0;
						peRow.CZ_Doslo = 0;
						peRow.SetWEIGHTNull();
						peRow.SetNMBRPALNull();
						peRow.TYPEPAL = string.Empty;
						peRow.ITEMCODE = Item.IsITEMCODENull() ? string.Empty : Item.ITEMCODE;
						peRow.SERLTNUM = string.Empty;
						peRow.CZ_REZ1_Track = 0;
						peRow.CZ_REZ2_Track = 0;

						peTable.AddCZMST_PERow(peRow);
					}

					#endregion

					Database.Prijem.Update_CZMST_PE(peTable, connection, trans);

					if (trans != null)
						trans.Commit();

					objednavka.CisloDavky = cisloDavky.ToString();
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


		/// <summary>
		/// Aktualná metoda pro praci s dokladama po odeslaní dávky
		/// </summary>
		/// <param name="countEntries">číslo dávky</param>
		/// <param name="SKL_ID">ID skladu</param>
		/// <param name="SOPNUMBE">Označení objednávky</param>
		/// <param name="note">poznamka</param>
		/// <returns></returns>
		internal static string Prijem_Import_ABRA(int countEntries, string SKL_ID, string PONUMBER, string note)
		{
			try
			{
				string pom = string.Empty;
				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				Fask.DataSets.Prijem dsP = new Fask.DataSets.Prijem();
				string selectSI = "SELECT * FROM " + Constants.Common.TABLE_CZMST_PI + " WHERE CountEntries='" + countEntries + "'";
				if (Database.Vydej.Fill_DataSet(selectSI, dsP, dsP.CZMST_PI.TableName) == -1) //Vrati data dle SELECTU
				{
					Logging.ExceptionHandler2.Handle(new Exception("Nastala chyba při plnění :'" + Constants.Common.TABLE_CZMST_PI + "'"));
				}

				string selectSE = "SELECT * FROM " + Constants.Common.TABLE_CZMST_PE + " WHERE CountEntries='" + countEntries + "'";
				if (Database.Vydej.Fill_DataSet(selectSE, dsP, dsP.CZMST_PE.TableName) == -1) //Vrati data dle SELECTU
				{
					Logging.ExceptionHandler2.Handle(new Exception("Nastala chyba při plnění :'" + Constants.Common.TABLE_CZMST_PE + "'"));
				}


				//List<string> listOV = new List<string>();
				string ID_PR = Database.ABRA.Get_ID_PR(PONUMBER, SKL_ID);

				//Prvni request do IS ABRA, uprava QTY v DL
				//string s = Edit_Hlavna_PR(dsP, ID_PR.Trim(), out listOV);
				string s = Edit_Hlavna_PR(dsP, ID_PR.Trim());

				//Druhy request do IS ABRA, zmena PR
				pom = Classes.ABRA.Edit_Stav_PR(Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Prijem_Naskladneno.Trim(), ID_PR.Trim());

				if (pom != "OK")
					throw new Exception(pom);

				//TaD Vypnuta tvorba dokladu ke kontrole
                //if (false)
                //{
                //    // Treti rozdilova PR
                //    if (listOV.Count() > 0)
                //    {
                //        string ID_DLedit = string.Empty;
                //        if (listOV.Count() == 1)
                //        {
                //            pom = Create_PR_all(listOV[0], out ID_DLedit);
                //        }
                //        else
                //        {
                //            //TODO : 
                //            pom = "Nalezeno vicero OV";
                //        }

                //        if (pom != "OK")
                //            throw new Exception(pom);

                //        pom = Classes.ABRA.Edit_Stav_PR(Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Vydej_KeKontrole.Trim(), ID_DLedit.Trim());

                //        if (pom != "OK")
                //            throw new Exception(pom);
                //    } 
                //}


				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region Aktivne sa použivaju

		/// <summary>
		/// Metoda slouží k Editaci DL ze kterým se pracuje
		/// </summary>
		/// <param name="dsV"></param>
		/// <param name="iD_DL"></param>
		/// <param name="listOP"></param>
		/// <returns></returns>
		private static string Edit_Hlavna_PR(DataSets.Prijem dsP, string iD_PR, out List<string> listOV)
		{
			try
			{
				listOV = new List<string>();
				var polEdit = new List<Polozky_Prijem>();
				var polDelete = new List<Polozky_Prijem>();
				Classes.Prijem.KontrolaVykriti_A_Smazani(dsP, out polEdit, out polDelete, out listOV);

				if (polEdit.Count() > 0)
				{
					if (Edit_SERLTNUM_QTY_in_PR(iD_PR, polEdit) != "OK")
					{
						throw new Exception("Nelze editovat PR:'" + iD_PR + "'");
					}
				}

				if (polDelete.Count() > 0)
				{
					foreach (Polozky_Prijem item in polDelete)
					{
						if (Delete_in_PR(iD_PR, item) != "OK")
							throw new Exception("Nelze smazat ITEMNMBR:'" + item.ITEMNMBR + "'");
					}
				}

				return "OK";
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}

		}

		private static string Edit_Hlavna_PR(DataSets.Prijem dsP, string iD_PR)
		{
			try
			{
				var polEdit = new List<Polozky_Prijem>();
				var polDelete = new List<Polozky_Prijem>();
				Classes.Prijem.KontrolaVykriti_A_Smazani(dsP, out polEdit, out polDelete);

				if (polEdit.Count() > 0)
				{
					if (Edit_SERLTNUM_QTY_in_PR(iD_PR, polEdit) != "OK")
					{
						throw new Exception("Nelze editovat PR:'" + iD_PR + "'");
					}
				}

				if (polDelete.Count() > 0)
				{
					foreach (Polozky_Prijem item in polDelete)
					{
						if (Delete_in_PR(iD_PR, item) != "OK")
							throw new Exception("Nelze smazat ITEMNMBR:'" + item.ITEMNMBR + "'");
					}
				}

				return "OK";
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}

		}


		internal static string Edit_Stav_PR(string stav, string iD_PR)
		{
			try
			{
				Globals_V1.LoadConfiguration();

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_Stav_PR(stav, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				if (string.IsNullOrEmpty(iD_PR.Trim()))
					throw new Exception("Prijemka nenalezena...");

				string param =
					Constants.Common.PR +
					Constants.Common.SLASH +
					iD_PR.Trim() +
					Constants.Common.SLASH +
					Constants.Common.PM_Change;

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_Stav_PR", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Edit_Stav_PR(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pomoci ktere se překlopí OP do DL a vznikne tym rozdilovy DL
		/// </summary>
		/// <param name="polozky"></param>
		/// <param name="ID_DL"></param>
		/// <returns></returns>
		public static string Create_PR_all(string ID_OV, out string ID_PR)
		{
			try
			{
				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Create_PR_all(G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}


				if (string.IsNullOrEmpty(ID_OV))
					throw new Exception("Prijemka nenalezen...");

				string param =
					Constants.Common.PR +
					Constants.Common.SLASH +
					Constants.Common.IMPORT +
					Constants.Common.SLASH +
					Constants.Common.OV +
					Constants.Common.SLASH +
					ID_OV;

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Create_PR_all", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.POST, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				ID_PR = null;

				return Classes.WEBAPI.RequestResponse.LoadResponse_Create_PR_all(restResponse, G, out ID_PR);

			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;

			}
		}


		#region Pomocne

		/// <summary>
		/// Metoda pomoci ktere se smaže jeden řadek, jedna položka v Prijemke podle ID
		/// </summary>
		/// <param name="ID_PR">ID Prijemky</param>
		/// <param name="polozky">Objekt položky</param>
		/// <returns></returns>
		private static string Delete_in_PR(string ID_PR, Polozky_Prijem polozky)
		{
			try
			{

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (string.IsNullOrEmpty(ID_PR.Trim()))
					throw new Exception("ID Prijemky nezadano!");

				string ID_Row_DL = null;

				ID_Row_DL = Database.ABRA.Get_ID_Polozka_Prijemka(polozky);

				if (string.IsNullOrEmpty(ID_Row_DL.Trim()))
					throw new Exception("ID radku Prijemky nezadano!");


				string param =
					Constants.Common.PR +
					Constants.Common.SLASH +
					ID_PR.Trim() +
					Constants.Common.SLASH +
					Constants.Common.ROWS +
					Constants.Common.SLASH +
					ID_Row_DL.Trim();

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Delete_in_PR", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.DELETE, out restResponse, param))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Delete_in_PR(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}


		private static string Edit_SERLTNUM_QTY_in_PR(string ID_PR, List<Polozky_Prijem> polEdit)
		{
			try
			{

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_SERLTNUM_QTY_in_PR(polEdit, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				if (string.IsNullOrEmpty(ID_PR.Trim()))
					throw new Exception("PR nenalezena...");

				string param =
					Constants.Common.PR +
					Constants.Common.SLASH +
					ID_PR.Trim();

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_SERLTNUM_QTY_in_PR", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Edit_SERLTNUM_QTY_in_PR(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}


		#endregion

		#endregion

		#endregion

		#region Inventura

		public static bool LoadInventura(out int PocetInventur)
		{

			//Treba naplnit CZMST_I1 a CZMST_I1H a CZMST_I3
			PocetInventur = 0;

			Globals_V1.LoadConfiguration();
			SqlTransaction trans = null;
			SqlConnection connection = null;

			Database.Inventura i = new Database.Inventura();

			try
			{
				SQL_Datasets.Inventura InventuraDS = new SQL_Datasets.Inventura();

				ABRA_Datasets.Inventura.CZMST_I1DataTable dt_polozky = Database.ABRA.PolozkyInventur();
				ABRA_Datasets.Inventura.CZMST_I1HDataTable dt_inventury = Database.ABRA.SeznamInventur();
				ABRA_Datasets.Inventura.CZMST_I3DataTable dt_MJ_Eans = Database.ABRA.PolozkyInventur_MJ_Eans();
				ABRA_Datasets.Inventura.CZMST_I2DataTable dt_sarze = Database.ABRA.PolozkySarzeInventur();

				if (dt_polozky == null)
				{
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "Neni zadna inventura na stahnuti");
					return false;
				}

				//string CZ_CarKod;
				//string ITEMNMBR;
				//string LOCNCODE;
				//string ITEMDESC;
				//string SKL_ID;
				//string MJ;
				//string ITEMCODE;
				//byte CZ_SerNum_Track;
				//byte TerminalID;

				SQL_Datasets.Inventura inventura = new SQL_Datasets.Inventura();

				Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "LoadInventura CS: " + Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{

					connection.Open();
					trans = connection.BeginTransaction();

					int Description_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1H["Description"].MaxLength;


					int? IDtmp = i.CZMSTI1H_MAX_CountEntries();

					if (!IDtmp.HasValue)
						throw new Exception("Nastala chyba při dotažení MAX ID davky inventury...");

					int ID = IDtmp.Value;

					foreach (ABRA_Datasets.Inventura.CZMST_I1HRow row in dt_inventury)
					{
						PocetInventur++;
						var rowI1H = inventura.CZMST_I1H.NewCZMST_I1HRow();

						string text = "-";

						if (!row.IsDescriptionNull())
						{
							text = row.Description;
							if (text.Length > Description_MaxLength)
								text = text.Remove(Description_MaxLength);
						}

						rowI1H.Description = text;

						rowI1H.CountEntries = ID++;
						rowI1H.State = 0;
						rowI1H.SetCE_OrigNull();


						inventura.CZMST_I1H.AddCZMST_I1HRow(rowI1H);
					}



					int ITEMNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["ITEMNMBR"].MaxLength;
					int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["MJ"].MaxLength;
					int VENDNAME_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["VENDNAME"].MaxLength;
					int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1["ITEMDESC"].MaxLength;
					int ITEMCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1["ITEMCODE"].MaxLength;

					int I2_ITEMNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I2["ITEMNMBR"].MaxLength;
					int I2_SERLNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I2["SERLNMBR"].MaxLength;

					foreach (ABRA_Datasets.Inventura.CZMST_I1Row row in dt_polozky)
					{
						#region CZMST_I1

						var rowI1 = inventura.CZMST_I1.NewCZMST_I1Row();

						foreach (SQL_Datasets.Inventura.CZMST_I1HRow item in inventura.CZMST_I1H.Rows)
						{
							if (item.Description.Trim() == row.CountEntries.Trim())
							{
								rowI1.CountEntries = item.CountEntries;
								break;
							}
						}

						rowI1.CZ_CarKod = row.IsCZ_CarKodNull() ? string.Empty : row.CZ_CarKod;
						rowI1.ITEMDESC = row.IsITEMDESCNull() ? string.Empty : row.ITEMDESC;
						rowI1.ITEMNMBR = row.IsITEMNMBRNull() ? string.Empty : row.ITEMNMBR;
						rowI1.LOCNCODE = string.Empty;

						rowI1.skl_id = row.IsSKL_IDNull() ? string.Empty : row.SKL_ID;
						rowI1.DMJ = row.IsMJNull() ? string.Empty : row.MJ;
						rowI1.ITEMCODE = row.IsITEMCODENull() ? string.Empty : row.ITEMCODE;

						rowI1.QUANTITY = row.QUANTITY;


						rowI1.CZ_SerNum_Track = row.IsCZ_SerNum_TrackNull() ? (byte)0 : row.CZ_SerNum_Track;

						rowI1.CZ_Expirace_Track = row.IsCZ_Expirace_TrackNull() ? (byte)0 : row.CZ_Expirace_Track;

						//if (rowI1.CZ_SerNum_Track > 0)
						//	rowI1.CZ_Expirace_Track = 1;
						//else
						//	rowI1.CZ_Expirace_Track = 0;

						byte TerminalID = 0;

						if (!row.IsTerminalIDNull())
						{
							if (!byte.TryParse(row.TerminalID, out TerminalID))
							{
								rowI1.TerminalID = 0;
							}
							else
							{
								rowI1.TerminalID = TerminalID;
							}
						}



						if (row.ITEMNMBR.Length > ITEMNMBR_MaxLength)
							row.ITEMNMBR = row.ITEMNMBR.Remove(ITEMNMBR_MaxLength);

						rowI1.ITEMNMBR = row.ITEMNMBR;

						if (row.ITEMDESC.Length > ITEMDESC_MaxLength)
							row.ITEMDESC = row.ITEMDESC.Remove(ITEMDESC_MaxLength);

						rowI1.ITEMDESC = row.ITEMDESC;

						if (row.ITEMCODE.Length > ITEMCODE_MaxLength)
							row.ITEMCODE = row.ITEMCODE.Remove(ITEMCODE_MaxLength);

						rowI1.ITEMCODE = row.ITEMCODE;

						rowI1.DATEDONE = DateTime.Now;

						rowI1.IntegerValue = 0;
						rowI1.TIMESPRT = 0;
						rowI1.CZ_SerNum_Find = 0;
						rowI1.O_TID = 0;
						rowI1.REZ_1 = string.Empty;
						rowI1.REZ_2 = string.Empty;

						rowI1.SetCE_OrigNull();

						rowI1.CZ_REZ1_Track = 0;
						rowI1.CZ_REZ2_Track = 0;

						inventura.CZMST_I1.AddCZMST_I1Row(rowI1);

						#endregion


					}


                    foreach (var row in dt_MJ_Eans)
                    {
						var rowI3 = inventura.CZMST_I3.NewCZMST_I3Row();

						foreach (SQL_Datasets.Inventura.CZMST_I1HRow item in inventura.CZMST_I1H.Rows)
						{
							if (item.Description.Trim() == row.CountEntries.Trim())
							{
								rowI3.CountEntries = item.CountEntries;
								break;
							}
						}
					
						rowI3.ITEMNMBR = row.IsITEMNMBRNull() ? string.Empty : row.ITEMNMBR;
						rowI3.CZ_CarKod = row.IsCZ_CarKodNull() ? string.Empty : row.CZ_CarKod;
						rowI3.QTYPACK = row.IsQTYPACKNull() ? 0 : row.QTYPACK;
						rowI3.MJ = row.IsMJNull() ? string.Empty : row.MJ;
						rowI3.VENDORID = string.Empty;
						rowI3.VNDITNUM = row.IsVNDITNUMNull() ? string.Empty : row.VNDITNUM;
						rowI3.VENDNAME = string.Empty;
						rowI3.SetWEIGHTNull();
						rowI3.SetCE_OrigNull();

						inventura.CZMST_I3.AddCZMST_I3Row(rowI3);

					}



					foreach (ABRA_Datasets.Inventura.CZMST_I2Row row in dt_sarze)
					{
						var rowI2 = inventura.CZMST_I2.NewCZMST_I2Row();

						foreach (SQL_Datasets.Inventura.CZMST_I1HRow item in inventura.CZMST_I1H.Rows)
						{
							if (item.Description.Trim() == row.CountEntries.Trim())
							{
								rowI2.CountEntries = item.CountEntries;
								break;
							}
						}

						rowI2.SetCE_OrigNull();

						if (row.ITEMNMBR.Length > I2_ITEMNMBR_MaxLength)
							row.ITEMNMBR = row.ITEMNMBR.Remove(I2_ITEMNMBR_MaxLength);

						rowI2.ITEMNMBR = row.ITEMNMBR;

						if (row.SERLNMBR.Length > I2_SERLNMBR_MaxLength)
							row.SERLNMBR = row.SERLNMBR.Remove(I2_SERLNMBR_MaxLength);

						rowI2.SERLNMBR = row.SERLNMBR;

						if (row.IsExpiraceNull())
							rowI2.SetExpiraceNull();
						else
							rowI2.Expirace = new DateTime(1899, 12, 30).AddDays(row.Expirace);

						rowI2.QTY = row.QTY;

						inventura.CZMST_I2.AddCZMST_I2Row(rowI2);


					}

					i.Update_I1(inventura.CZMST_I1, connection, trans);
					i.Update_I1H(inventura.CZMST_I1H, connection, trans);
					i.Update_I2(inventura.CZMST_I2, connection, trans);
					i.Update_I3(inventura.CZMST_I3, connection, trans);

					if (trans != null)
						trans.Commit();
				}

				return true;
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();

				throw ex;
			}
		}

		public static bool LoadInventura(string DIP, out int CountEntriesOut)
		{

			//Treba naplnit CZMST_I1 a CZMST_I1H a CZMST_I3
			CountEntriesOut = 0;

			Globals_V1.LoadConfiguration();
			SqlTransaction trans = null;
			SqlConnection connection = null;

			Database.Inventura i = new Database.Inventura();

			try
			{
				SQL_Datasets.Inventura InventuraDS = new SQL_Datasets.Inventura();

				ABRA_Datasets.Inventura.CZMST_I1DataTable dt_polozky = Database.ABRA.PolozkyInventur(DIP);
				ABRA_Datasets.Inventura.CZMST_I3DataTable dt_MJ_Eans = Database.ABRA.PolozkyInventur_MJ_Eans(DIP);
				ABRA_Datasets.Inventura.CZMST_I2DataTable dt_sarze = Database.ABRA.PolozkySarzeInventur(DIP);

				if (dt_polozky == null)
				{
					Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "Neni zadna inventura na stahnuti");
					return false;
				}


				SQL_Datasets.Inventura inventura = new SQL_Datasets.Inventura();

				Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "LoadInventura CS: " + Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
				using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{

					connection.Open();
					trans = connection.BeginTransaction();

					int Description_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1H["Description"].MaxLength;


					int? IDtmp = i.CZMSTI1H_MAX_CountEntries();

					if (IDtmp.HasValue)
					{
						CountEntriesOut = IDtmp.Value;
					}
					else
					{
						CountEntriesOut++;
					}

					var rowI1H = inventura.CZMST_I1H.NewCZMST_I1HRow();

					rowI1H.Description = DIP;
					rowI1H.CountEntries = CountEntriesOut;
					rowI1H.State = 0;
					rowI1H.SetCE_OrigNull();


					inventura.CZMST_I1H.AddCZMST_I1HRow(rowI1H);
					

					int ITEMNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["ITEMNMBR"].MaxLength;
					int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["MJ"].MaxLength;
					int VENDNAME_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I3["VENDNAME"].MaxLength;
					int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1["ITEMDESC"].MaxLength;
					int ITEMCODE_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I1["ITEMCODE"].MaxLength;

					int I2_ITEMNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I2["ITEMNMBR"].MaxLength;
					int I2_SERLNMBR_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Inventura1.ColumnsInfo_CZMST_I2["SERLNMBR"].MaxLength;

					foreach (ABRA_Datasets.Inventura.CZMST_I1Row row in dt_polozky)
					{
						#region CZMST_I1

						var rowI1 = inventura.CZMST_I1.NewCZMST_I1Row();

						foreach (SQL_Datasets.Inventura.CZMST_I1HRow item in inventura.CZMST_I1H.Rows)
						{
							if (item.Description.Trim() == row.CountEntries.Trim())
							{
								rowI1.CountEntries = item.CountEntries;
								break;
							}
						}

						rowI1.CZ_CarKod = row.IsCZ_CarKodNull() ? string.Empty : row.CZ_CarKod;
						rowI1.ITEMDESC = row.IsITEMDESCNull() ? string.Empty : row.ITEMDESC;
						rowI1.ITEMNMBR = row.IsITEMNMBRNull() ? string.Empty : row.ITEMNMBR;
						rowI1.LOCNCODE = string.Empty;

						rowI1.skl_id = row.IsSKL_IDNull() ? string.Empty : row.SKL_ID;
						rowI1.DMJ = row.IsMJNull() ? string.Empty : row.MJ;
						rowI1.ITEMCODE = row.IsITEMCODENull() ? string.Empty : row.ITEMCODE;

						rowI1.QUANTITY = row.QUANTITY;


						rowI1.CZ_SerNum_Track = row.IsCZ_SerNum_TrackNull() ? (byte)0 : row.CZ_SerNum_Track;

						rowI1.CZ_Expirace_Track = row.IsCZ_Expirace_TrackNull() ? (byte)0 : row.CZ_Expirace_Track;

						//if (rowI1.CZ_SerNum_Track > 0)
						//	rowI1.CZ_Expirace_Track = 1;
						//else
						//	rowI1.CZ_Expirace_Track = 0;

						byte TerminalID = 0;

						if (!row.IsTerminalIDNull())
						{
							if (!byte.TryParse(row.TerminalID, out TerminalID))
							{
								rowI1.TerminalID = 0;
							}
							else
							{
								rowI1.TerminalID = TerminalID;
							}
						}



						if (row.ITEMNMBR.Length > ITEMNMBR_MaxLength)
							row.ITEMNMBR = row.ITEMNMBR.Remove(ITEMNMBR_MaxLength);

						rowI1.ITEMNMBR = row.ITEMNMBR;

						if (row.ITEMDESC.Length > ITEMDESC_MaxLength)
							row.ITEMDESC = row.ITEMDESC.Remove(ITEMDESC_MaxLength);

						rowI1.ITEMDESC = row.ITEMDESC;

						if (row.ITEMCODE.Length > ITEMCODE_MaxLength)
							row.ITEMCODE = row.ITEMCODE.Remove(ITEMCODE_MaxLength);

						rowI1.ITEMCODE = row.ITEMCODE;

						rowI1.DATEDONE = DateTime.Now;

						rowI1.IntegerValue = 0;
						rowI1.TIMESPRT = 0;
						rowI1.CZ_SerNum_Find = 0;
						rowI1.O_TID = 0;
						rowI1.REZ_1 = string.Empty;
						rowI1.REZ_2 = string.Empty;

						rowI1.SetCE_OrigNull();

						rowI1.CZ_REZ1_Track = 0;
						rowI1.CZ_REZ2_Track = 0;

						inventura.CZMST_I1.AddCZMST_I1Row(rowI1);

						#endregion


					}


					foreach (var row in dt_MJ_Eans)
					{
						var rowI3 = inventura.CZMST_I3.NewCZMST_I3Row();

						foreach (SQL_Datasets.Inventura.CZMST_I1HRow item in inventura.CZMST_I1H.Rows)
						{
							if (item.Description.Trim() == row.CountEntries.Trim())
							{
								rowI3.CountEntries = item.CountEntries;
								break;
							}
						}

						rowI3.ITEMNMBR = row.IsITEMNMBRNull() ? string.Empty : row.ITEMNMBR;
						rowI3.CZ_CarKod = row.IsCZ_CarKodNull() ? string.Empty : row.CZ_CarKod;
						rowI3.QTYPACK = row.IsQTYPACKNull() ? 0 : row.QTYPACK;
						rowI3.MJ = row.IsMJNull() ? string.Empty : row.MJ;
						rowI3.VENDORID = string.Empty;
						rowI3.VNDITNUM = row.IsVNDITNUMNull() ? string.Empty : row.VNDITNUM;
						rowI3.VENDNAME = string.Empty;
						rowI3.SetWEIGHTNull();
						rowI3.SetCE_OrigNull();

						inventura.CZMST_I3.AddCZMST_I3Row(rowI3);

					}



					foreach (ABRA_Datasets.Inventura.CZMST_I2Row row in dt_sarze)
					{
						var rowI2 = inventura.CZMST_I2.NewCZMST_I2Row();

						foreach (SQL_Datasets.Inventura.CZMST_I1HRow item in inventura.CZMST_I1H.Rows)
						{
							if (item.Description.Trim() == row.CountEntries.Trim())
							{
								rowI2.CountEntries = item.CountEntries;
								break;
							}
						}

						rowI2.SetCE_OrigNull();

						if (row.ITEMNMBR.Length > I2_ITEMNMBR_MaxLength)
							row.ITEMNMBR = row.ITEMNMBR.Remove(I2_ITEMNMBR_MaxLength);

						rowI2.ITEMNMBR = row.ITEMNMBR;

						if (row.SERLNMBR.Length > I2_SERLNMBR_MaxLength)
							row.SERLNMBR = row.SERLNMBR.Remove(I2_SERLNMBR_MaxLength);

						rowI2.SERLNMBR = row.SERLNMBR;

						if (row.IsExpiraceNull())
							rowI2.SetExpiraceNull();
						else
							rowI2.Expirace = new DateTime(1899, 12, 30).AddDays(row.Expirace);

						rowI2.QTY = row.QTY;

						inventura.CZMST_I2.AddCZMST_I2Row(rowI2);


					}

					i.Update_I1(inventura.CZMST_I1, connection, trans);
					i.Update_I1H(inventura.CZMST_I1H, connection, trans);
					i.Update_I2(inventura.CZMST_I2, connection, trans);
					i.Update_I3(inventura.CZMST_I3, connection, trans);

					if (trans != null)
						trans.Commit();
				}

				return true;
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();

				throw ex;
			}
		}


		public static bool ImportInventura(int CountEntries)
		{

			//1. Dotažení dat z I4 na zaklade CountEntries
			//2. Zistení :
			// SKL_ID
			// z I1H v description dotahnut DIP na zaklade CountEntries
			//3. Dotazem dotahnout grupnute data z CZMST_I4 podle :
			// ITEMNMBR,ITEMCODE, CZ_CarKod, SKL_ID, VNDITNUM, SUM(QUANTITY), SERLNMBR, USERID, TerminalID
			//3.1 pomoci Linq vytahnout zvlašt CZ_SerNum_Track == 0 a (CZ_SerNum_Track == 1 || CZ_SerNum_Track == 2)
			//3.2 Co muže nastat?
			//4. Vytvořit Request
			//5. Poslat ho to ABRA
			//6. vyčíst response...
			//
			//Pokud je vše OK tak je to OK
			//


			//Co muže nastat?
			//
			// Dojde položka na Množství tak:
			//			- pouze zapíšu do DIP nasnimane množství (RealQuantity) a nastavim přiznak že sem zmenil (RealQuantityChanged)
			//
			//Dojde položka na šarže/SN
			//
			//			- dohledat ID šarže s StoreBatch (Filtr pouze na NAME šarže, bez expirace)
			//				-ANO je
			//						- dotahnout si expiraci z ABRA a poronat s zadanou
			//							- NE sedí :
			//								- Upravit Expiraci pro konkretní ID šarže
			//							- ANO sedí :
			//						- zistit či je tohle ID v HIP
			//								- pokud je v HIP tak pomoci tohohle ID zistit či je v DIP
			//										-Ano je, tak vytahnout ID a sem zapsat nasnimane množství
			//										- NE neni, tak vložit ID z HIP a množství
			//								- pokud neni v HIP tak je potreba ho pridat do HIP
			//										-pokud je v HIP tak pomoci tohohle ID zistit či je v DIP
			//											-Ano je, tak vytahnout ID a sem zapsat nasnimane množství
			//											- NE neni, tak vložit ID z HIP a množství
			//			  					
			//				-NE neni
			//						- tak je potreba vytvořit novou šarži
			//						-Nasledně podle ID ktere vrati response vložit ho do HIP
			//						-Nasledne podle ID ktere vrati response vložit ho do DIP a vyplnit nasnimane množství

			TracId tracid = new TracId(null, null, null, "ImportInventura");
			Trac.Write("Start ImportInventura", tracid);


			Database.Inventura i = new Database.Inventura();

			try
			{
				Trac.Write("Start dotahovat data z DB", tracid);

				var rowsI4_0 = i.GetDataByCountEntries_I4_Grupa_Mnozstvi(CountEntries);
				var rowsI4_2 = i.GetDataByCountEntries_I4_Grupa_SARZE_SN(CountEntries, false);
				var rowsI4_2_Nezname = i.GetDataByCountEntries_I4_Grupa_SARZE_SN(CountEntries, true);
				string DIP = i.Get_Desc_I1H(CountEntries);

				Trac.Write("Stop dotahovat data z DB", tracid);

				if (string.IsNullOrEmpty(DIP))
				{
					throw new Exception("Identifikator DIP nenalezen...");
				}

				Trac.Write("Start 1. Import množství", tracid);
				//1. Import množství 
				string s = Edit_DIP_Mnozstvi(rowsI4_0, DIP);

				Trac.Write("Stop 1. Import množství", tracid);

				if (s != "OK")
					throw new Exception(s);

				Trac.Write("Start 2. Zname šarže", tracid);
				//2. Zname šarže
				s = Edit_DIP_ZnameSarze(rowsI4_2, DIP);

				Trac.Write("Stop 1. Import šarže", tracid);

				if (s != "OK")
					throw new Exception(s);

				Trac.Write("Start 3. NEzname šarže", tracid);
				//3. NEzname šarže
				s = Edit_DIP_NeznameSarze(rowsI4_2_Nezname, DIP);

				Trac.Write("Stop 3. NEzname šarže", tracid);

				if (s != "OK")
					throw new Exception(s);

				return true;
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}

			//return true;
		}


		#region Používá se


		#region Množství

		private static string Edit_DIP_Mnozstvi(SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable rowsI4_0, string DIP)
		{
			try
			{
				TracId tracid = new TracId(null, null, null, "Edit_DIP_Mnozstvi");
				Trac.Write("Start pred foreach", tracid);
				foreach (var row in rowsI4_0)
				{
					string ID_DipRow = Database.ABRA.Get_ID_DIPRow(DIP, row.SKL_ID, row.ITEMNMBR);

					if (string.IsNullOrEmpty(ID_DipRow))
					{
						Trac.Write("FUJ ?ZLE? Stop Get_ID_DIPRow: '" + ID_DipRow + "'", tracid);
						Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, string.Format("Upozornení množství(Get_ID_HIPRow), nenalezeno pro DIP: '{0}' pro SKL_ID: '{1}' pro ITEMNMBR: '{2}'", DIP, row.SKL_ID, row.ITEMNMBR));
						continue;
					}

					IRestResponse restResponse;

					string JSON = string.Empty;
					Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

					Trac.Write("Tvorba JSON", tracid);
					if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_DIP_Mnozstvi(row, DIP, G, out JSON))
					{
						throw new Exception("Vytvoření requestu se nezdařilo");
					}

					string param = Constants.Common.DIProws +
						Constants.Common.SLASH +
						ID_DipRow;

					if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
					{
						SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_DIP_Mnozstvi", param, G, Constants.Common.txt);
					}

					Trac.Write("Start komunikace", tracid);
					if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
					{
						throw new Exception("Komunikace s IS ABRA se nezdařila");
					}
					Trac.Write("Stop komunikace", tracid);

					Trac.Write("Star load response", tracid);
					string state = Classes.WEBAPI.RequestResponse.LoadResponse_Edit_DIP_Mnozstvi(restResponse, G);
					Trac.Write("Stop load response", tracid);

					if (state != "OK")
						throw new Exception(state);
				}
				Trac.Write("Stop po foreach", tracid);

				return "OK";
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#endregion

		#region ZnameSarze

		private static string Edit_DIP_ZnameSarze(SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable rowsI4_2, string DIP)
		{
			try
			{

				foreach (var row in rowsI4_2)
                {
                    string ID_DipRow = Database.ABRA.Get_ID_DIPRow(DIP, row.SKL_ID, row.ITEMNMBR);

					if (string.IsNullOrEmpty(ID_DipRow))
					{
						Fask.Logging.ExceptionHandler2.Handle(LogLevel.Warn,string.Format("Upozornení Zname šarže(Get_ID_DIPRow), nenalezeno pro DIP: '{0}' pro SKL_ID: '{1}' pro ITEMNMBR: '{2}'", DIP, row.SKL_ID, row.ITEMNMBR));
						continue;
					}

                    IRestResponse restResponse;

                    string JSON = string.Empty;
                    Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

                    DateTime? exp = row.IsExpiraceNull() ? (DateTime?)null : row.Expirace;

					CheckExpirace(DIP, row, exp);
                    


                    if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_DIP_ZnameSarze(row, DIP, G, out JSON))
                    {
                        throw new Exception("Vytvoření requestu se nezdařilo");
                    }

                    string param = Constants.Common.DIProws +
                        Constants.Common.SLASH +
                        ID_DipRow;

                    if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
                    {
                        SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_DIP_ZnameSarze", param, G, Constants.Common.txt);
                    }

                    if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
                    {
                        throw new Exception("Komunikace s IS ABRA se nezdařila");
                    }

                    string state = Classes.WEBAPI.RequestResponse.LoadResponse_Edit_DIP_ZnameSarze(restResponse, G);

                    if (state != "OK")
                        throw new Exception(state);
                }

                return "OK";
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}


        #endregion

        #region NE zname šarže

        private static string Edit_DIP_NeznameSarze(SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable rowsI4, string DIP)
		{
			try
			{
				Inventura i = new Inventura();
				i.AlgoritmusDoplneniNeznamejSarze(rowsI4, DIP);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}

			return "OK";
		}

		/// <summary>
		/// Metoda pomoci ktere se vytvori šarže
		/// </summary>
		/// <param name="polozky"></param>
		/// <param name="ID_DL"></param>
		/// <returns></returns>
		public static string Create_Sarzi(string ID_StoreCard, string Name, DateTime? exp, bool? SN, out string ID_SB)
		{
			try
			{
				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Create_Sarzi(ID_StoreCard, Name, exp, SN, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				string param = Constants.Common.SB;

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Create_Sarzi", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.POST, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				ID_SB = null;

				return Classes.WEBAPI.RequestResponse.LoadResponse_Create_Sarzi(restResponse, G, out ID_SB);

			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				ID_SB = null;
				throw ex;
			}
		}

		internal static string Create_HIPBatch(string ID_HIPRow, string MJ, string ID_Sarze, out string ID_HIPBatch)
		{
			try
			{
				ID_HIPBatch = null;
				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Create_HIPBatch(ID_Sarze, MJ, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				string param =
					Constants.Common.HIProws +
					Constants.Common.SLASH +
					ID_HIPRow.Trim();
				;

				//string param = Constants.Common.HIProws ;
				Globals_V1.LoadConfiguration();

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Create_HIPBatch", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Create_HIPBatch(restResponse, G, ID_Sarze, out ID_HIPBatch);

			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		internal static string Create_DIPBatch(string ID_DIPRow, string ID_HIPBatch, decimal QTY, string MJ)
		{
			try
			{
				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Create_DIPBatch(ID_HIPBatch, QTY, MJ, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				string param =
					Constants.Common.DIProws +
					Constants.Common.SLASH +
					ID_DIPRow.Trim();
				;

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Create_DIPBatch", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Create_DIPBatch(restResponse, G);

			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}


		#endregion

		#region Kontrola Expirace

		public static void CheckExpirace(string DIP, SQL_Datasets.Inventura.CZMST_I4_GrupaRow row, DateTime? exp)
		{


            try
            {

                string IDSarzeRow = string.Empty;
                bool Stav = Database.ABRA.Get_SarzeExpirace(DIP, row.SKL_ID, row.ITEMNMBR, row.SERLNMBR, exp, out IDSarzeRow);

                if (!Stav)
                {
                    EditaceExpirace(exp, IDSarzeRow);
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
		}

		public static string CheckExpirace(SQL_Datasets.Inventura.CZMST_I4_GrupaRow row, DateTime? exp)
		{


			string IDSarzeRow = string.Empty;
			bool Stav = Database.ABRA.Get_SarzeExpirace(row.SKL_ID, row.ITEMNMBR, row.SERLNMBR, exp, out IDSarzeRow);

			if (!Stav)
			{
				EditaceExpirace(exp, IDSarzeRow);
			}

			return IDSarzeRow;
		}

		private static void EditaceExpirace(DateTime? exp, string IDSarzeRow)
		{
			Guid G = Guid.NewGuid();
			string JSON;
			IRestResponse restResponse;

			if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_Expirace(exp.Value, G, out JSON))
			{
				throw new Exception("Vytvoření requestu se nezdařilo");
			}

			string param0 = Constants.Common.SB +
				Constants.Common.SLASH +
				IDSarzeRow;

			if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
			{
				SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_Expirace", param0, G, Constants.Common.txt);
			}

			if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param0, JSON))
			{
				throw new Exception("Komunikace s IS ABRA se nezdařila");
			}

			string state0 = Classes.WEBAPI.RequestResponse.LoadResponse_Edit_Expirace(restResponse, G);

			if (state0 != "OK")
				throw new Exception(state0);

		}


		#endregion

		#endregion

		#region Puvodne pokuse


		//private static string Edit_DIP_0_2(SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable rowsI4_0, SQL_Datasets.Inventura.CZMST_I4_GrupaDataTable rowsI4_12, string DIP)
		//{
		//	try
		//	{

		//		IRestResponse restResponse;

		//		string JSON = string.Empty;
		//		Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

		//		if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_DIP_0_2(rowsI4_0, rowsI4_12, DIP, G, out JSON))
		//		{
		//			throw new Exception("Vytvoření requestu se nezdařilo");
		//		}

		//		string param = Constants.Common.DIProws;

		//		if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
		//		{
		//			SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_DIP_0_2", param, G, Constants.Common.txt);
		//		}

		//		if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
		//		{
		//			throw new Exception("Komunikace s IS ABRA se nezdařila");
		//		}

		//		return Classes.WEBAPI.RequestResponse.LoadResponse_Edit_DIP_0_2(restResponse, G);
		//	}
		//	catch (System.Exception ex)
		//	{
		//		Fask.Logging.ExceptionHandler2.Handle(ex);
		//		throw ex;
		//	}
		//}

		#endregion

		#endregion

		#region Prodej

		/// <summary>
		/// Metoda pro import Prevodka
		/// </summary>
		/// <param name="data">data z CZMST_DI</param>
		/// <param name="uzivatel">Uzivatel</param>
		/// <returns>bud OK anebo chybu</returns>
		public static string Prodej_Import_Pre_ABRA(Fask.DataSets.ProdejData data, Doklad typDoklad, User uzivatel)
		{
			try
			{

				string pom = string.Empty;

				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				//TODO tady mnela bzt implementovava komunikace s IS ABRA

				//if (!Classes.Prodej2.CreateRequest_Pre_XML(filename, "FASK Import XML", data.CZMST_DI, typDoklad))
				//	return "CHYBA";

				//string responsefilename;
				//if (!XML.PohodaComunication.Communicate(filename, out responsefilename))
				//{
				//	throw new Exception("Komunikace s Pohodou se nezdařila");
				//}

				//pom = Classes.Prodej2.LoadResponse_Pre_XML(filename, uzivatel);

				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		#endregion


		#region Prevodka Vydej

		/// <summary>
		/// Metoda pro exportovaní dat z IS ABRA do FASK, předlodo co CZMST_SE
		/// </summary>
		/// <param name="objednavka">Objekt objednavky</param>
		/// <param name="sklad">objekt skladu</param>
		/// <returns></returns>
		public static string ExportVydejka_ABRA_Z_PrevodkaVydej(Objednavka objednavka, Sklad sklad)
		{
			SqlTransaction trans = null;
			SqlConnection connection = null;

			try
			{

				if (String.IsNullOrEmpty(objednavka.ID.Trim()))
					throw new Exception("Číslo dokladu nesmí být prázdné");

				ABRA_Datasets.Vydej.CZMST_SEDataTable dt_abra = Database.ABRA.PrevodkaVydej_GetData(objednavka.ID, sklad.ID);

				if (dt_abra == null || dt_abra.Count == 0)
				{
					throw new Exception("Nenalezen doklad pro export");
				}

				int cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries();
				cisloDavky += 1;


				using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					connection.Open();
					trans = connection.BeginTransaction();

					SQL_Datasets.Vydej.CZMST_SEDataTable seTable = new SQL_Datasets.Vydej.CZMST_SEDataTable();

					int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
					int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;

					#region Foreach

					foreach (var Item in dt_abra)
					{

						if (Item.CZ_SerNum_Track < 0 || Item.CZ_SerNum_Track > 2)
						{
							string msg = string.Format("Byl proveden pokus o export do CZMST_SE položky, která ma nepodporovaný příznak sledování :'{0}' " + Environment.NewLine, Item.CZ_SerNum_Track);
							msg += string.Format("Položka : " + Environment.NewLine + "		ITEMDESC:{0}" + Environment.NewLine + "		ITEMNMBR:{1}" + Environment.NewLine + "		ITEMCODE:{2}" + Environment.NewLine + "		SKL_ID:{3}", Item.ITEMDESC, Item.ITEMNMBR, Item.ITEMCODE, Item.SKL_ID);
							Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, msg);

							continue;
						}


						var seRow = seTable.NewCZMST_SERow();

						seRow.ITEMDESC = Item.IsITEMDESCNull() ? null : Item.ITEMDESC;
						if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
						{
							seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
						}

						seRow.ITEMNMBR = Item.ITEMNMBR;
						seRow.ITEMTYPE = Item.IsITEMTYPENull() ? string.Empty : Item.ITEMTYPE;
						seRow.ITEMCODE = Item.IsITEMCODENull() ? null : Item.ITEMCODE;
						seRow.CountEntries = cisloDavky;

						seRow.CZ_CarKod = Item.IsITEMCODENull() ? null : Item.ITEMCODE;
						seRow.CZ_DatVyr_Delka = 0;
						seRow.CZ_DatVyr_Track = 0;
						seRow.CZ_Doslo = 0;
						seRow.CZ_SerNum_Delka = 0;
						seRow.CZ_SerNum_Track = Item.CZ_SerNum_Track; // TODO ??

						//Po uprave SQL tohle smazat
						//if (Item.CZ_SerNum_Track == 2)
						//	seRow.CZ_Expirace_Track = 1;
						//else
						//	seRow.CZ_Expirace_Track = 0;

						// a tohle odkomentovat
						seRow.CZ_Expirace_Track = Item.CZ_Expirace_Track;

						seRow.CZ_SW_Delka = 0;
						seRow.CZ_SW_Track = 0;

						seRow.LOCNCODE = string.Empty;
						seRow.Note = string.Empty;
						seRow.ORD = Item.ORD;
						seRow.PRINTED = 0;
						seRow.PRIORITY = 3;
						seRow.QTYPACK = Item.IsQTYPACKNull() ? 0 : Item.QTYPACK;
						seRow.QTYPAL = 0;
						seRow.QTYSHPPD = Item.QTYSHPPD;
						seRow.SKL_ID = Item.SKL_ID;
						seRow.SOPNUMBE = Item.SOPNUMBE;
						seRow.TYPEPAL = "";

						seRow.VNDDOCNM = "";
						seRow.VNDITNUM = Item.VNDITNUM;
						seRow.MJ = Item.MJ;
						if (seRow.MJ.Length > MJ_MaxLength)
						{
							seRow.MJ = seRow.ITEMDESC.Remove(MJ_MaxLength);
						}

						//seRow.USERID = null;
						seRow.CZ_REZ1_Track = 0;
						seRow.CZ_REZ2_Track = 0;

						seRow.SetWEIGHTNull();

						seTable.AddCZMST_SERow(seRow);
					}

					#endregion

					Database.Vydej.Update_CZMST_SE(seTable, connection, trans);

					if (trans != null)
						trans.Commit();

					objednavka.CisloDavky = cisloDavky.ToString();
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


		/// <summary>
		/// Metoda pro editaci Stavu Procesniho řizeni Dodaciho Listu
		/// </summary>
		/// <param name="stav">Stav na ktery se ma přepnout</param>
		/// <param name="iD_DL">ID Dodaciho Listu</param>
		/// <returns></returns>
		internal static string Edit_Stav_PRV(string stav, string iD_PRV)
		{
			try
			{

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_Stav_PRV(stav, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				if (string.IsNullOrEmpty(iD_PRV.Trim()))
					throw new Exception("Dodaci List nenalezen...");

				string param =
					Constants.Common.PRV +
					Constants.Common.SLASH +
					iD_PRV.Trim() +
					Constants.Common.SLASH +
					Constants.Common.PM_Change;

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_Stav_PRV", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Edit_Stav_PRV(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		/// <summary>
		/// Aktualná metoda pro praci s dokladama po odeslaní dávky
		/// </summary>
		/// <param name="countEntries">číslo dávky</param>
		/// <param name="SKL_ID">ID skladu</param>
		/// <param name="SOPNUMBE">Označení objednávky</param>
		/// <param name="note">poznamka</param>
		/// <returns></returns>
		public static string Vydej_Import_ABRA_PRV(int countEntries, string SKL_ID, string SOPNUMBE, string note)
		{
			try
			{
				string pom = string.Empty;
				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				Fask.DataSets.Vydej dsV = new Fask.DataSets.Vydej();
				string selectSI = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SI + " WHERE CountEntries='" + countEntries + "'";
				if (Database.Vydej.Fill_DataSet(selectSI, dsV, dsV.CZMST_SI.TableName) == -1) //Vrati data dle SELECTU
				{
					Logging.ExceptionHandler2.Handle(new Exception("Nastala chyba při plnění :'" + Constants.Common.TABLE_CZMST_SI + "'"));
				}

				string selectSE = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SE + " WHERE CountEntries='" + countEntries + "'";
				if (Database.Vydej.Fill_DataSet(selectSE, dsV, dsV.CZMST_SE.TableName) == -1) //Vrati data dle SELECTU
				{
					Logging.ExceptionHandler2.Handle(new Exception("Nastala chyba při plnění :'" + Constants.Common.TABLE_CZMST_SE + "'"));
				}

				string ID_PRV = Database.ABRA.Get_ID_PrevodkaVydej(SOPNUMBE, SKL_ID);

				//Prvni request do IS ABRA, uprava QTY v PRV
				string s = Edit_Hlavny_PRV(dsV, ID_PRV.Trim());

				//Druhy request do ABRY, import celeho DL do FV
				//Zde pridat parametr pro vypnuti tvorby faktury
				if (Globals_V1.Konfigurace.Vydej[0].GenerovatPrevodkaPrijem)
				{
					Classes.ABRA_BO.Rootobject_PRP PRP_Objekt = Import_PRV_to_PRP_all(ID_PRV.Trim(), SOPNUMBE.Trim());
				}

				//Tretí request do IS ABRA, zmena stavu PRV
				pom = Classes.ABRA.Edit_Stav_PRV(Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Vydej_Vyskladneno.Trim(), ID_PRV.Trim());

				if (pom != "OK")
					throw new Exception(pom);


				return pom;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Aktualná metoda pro praci s dokladama po odeslaní dávky prelokovani
		/// </summary>
		/// <param name="countEntries">číslo dávky</param>
		/// <param name="SKL_ID">ID skladu</param>
		/// <param name="SOPNUMBE">Označení objednávky</param>
		/// <param name="note">poznamka</param>
		/// <returns></returns>
		public static string Vydej_Import_ABRA_PRV_prelokovani(int countEntries, string SKL_ID, string SOPNUMBE, string note)
		{
			try
			{
				string pom = string.Empty;
				pom = Globals_V1.LoadConfiguration();

				if (pom != "OK")
					return pom;

				Fask.DataSets.Vydej dsV = new Fask.DataSets.Vydej();
				string selectSI = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SI + " WHERE CountEntries='" + countEntries + "'";
				if (Database.Vydej.Fill_DataSet(selectSI, dsV, dsV.CZMST_SI.TableName) == -1) //Vrati data dle SELECTU
				{
					Logging.ExceptionHandler2.Handle(new Exception("Nastala chyba při plnění :'" + Constants.Common.TABLE_CZMST_SI + "'"));
				}

				
				//string res = Priprava_odeslani_dat_prelokovani_PRV(dsV, countEntries);

				if (dsV.CZMST_SI.Count() > 0)
				{
					if (Odeslani_dat_prelokovani_PRV(dsV, countEntries) != "OK")
					{
						throw new Exception("Nelze odeslat data prelokovani davky:'" + countEntries + "'");
					}
				}


				return "OK";
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}



		internal static void KontrolaVykriti_A_Smazani_PRV_prelokovani(Fask.DataSets.Vydej dsV, out List<Polozky_Vydej> polEdit, out List<Polozky_Vydej> polDelete)
		{
			try
			{
				polEdit = new List<Polozky_Vydej>();
				polDelete = new List<Polozky_Vydej>();

				foreach (var rowSE in dsV.CZMST_SE.Where(x => x.QTYPACK == 0))
				{
					//var rows = dsV.CZMST_SI.ToList().Where(x => x.ITEMNMBR == rowSE.ITEMNMBR && x.ORD == rowSE.ORD);
					var rows = dsV.CZMST_SI.ToList().Where(
						x =>
							x.ITEMNMBR == rowSE.ITEMNMBR &&
							x.SOPNUMBE == rowSE.SOPNUMBE &&
							x.ORD == rowSE.ORD &&
							x.SKL_ID == rowSE.SKL_ID
					);


					if (rows == null || rows.Count() == 0)
					{
						polDelete.Add(new Polozky_Vydej()
						{
							ITEMNMBR = rowSE.ITEMNMBR,
							ORD = rowSE.ORD,
							SKL_ID = rowSE.SKL_ID,
							SOPNUMBE = rowSE.SOPNUMBE
						});

						//listOP = Get_List_ID_OP(listOP, rowSE);
					}
					else
					{

						decimal qtySI_Sum = rows.Sum(x => x.QTYSHPPD);

						decimal qty = rowSE.QTYSHPPD - qtySI_Sum;

						if (qty != 0)
						{
							polEdit.Add(new Polozky_Vydej()
							{
								QTY = qtySI_Sum,
								ITEMNMBR = rows.ToArray()[0].ITEMNMBR,
								ORD = rows.ToArray()[0].ORD,
								SKL_ID = rows.ToArray()[0].SKL_ID,
								SOPNUMBE = rows.ToArray()[0].SOPNUMBE,
								SELTNUM = null,
								Expirace = null
							});

							//listOP = Get_List_ID_OP(listOP, rowSE);
						}

						if (rowSE.CZ_SerNum_Track == 2 || rowSE.CZ_SerNum_Track == 1)
						{
							var rows_group = rows.GroupBy(x => new { x.SERLTNUM }); // !!! serltnum != null ...
							foreach (var item in rows_group)
							{
								if (!string.IsNullOrEmpty(item.Key.SERLTNUM))
								{
									DateTime? expirace = null;
									if (rowSE.CZ_Expirace_Track > 0)
									{
										expirace = item.First().IsExpiraceNull() ? (DateTime?)null : item.First().Expirace;
										if ((expirace.HasValue) && !item.All(x => x.Expirace == expirace.Value))
											throw new Exception("Různé exspirace u jedné šarže !!!!");
									}

									polEdit.Add(new Polozky_Vydej()
									{
										QTY = null,
										ITEMNMBR = rowSE.ITEMNMBR.Trim(),
										ORD = rowSE.ORD,
										SKL_ID = rowSE.SKL_ID.Trim(),
										SOPNUMBE = rowSE.SOPNUMBE.Trim(),
										SELTNUM = item.Key.SERLTNUM.Trim(),
										QTY_SELTNUM = item.Sum(x => x.QTYSHPPD),
										Expirace = expirace
									}); ;
								}
							}
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw (ex);
			}

		}


		private static string Odeslani_dat_prelokovani_PRV_OLD_20251208(DataSets.Vydej dsV, int countEntries)
		{
			try
			{

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_PRV_prelokovani(dsV, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				string ID_PRV = string.Empty;
				foreach (var radek in dsV.CZMST_SI)
				{
					string idPRV_Row = Database.ABRA.Get_ID_Polozka_PrevodkyVydej_prelokovani(radek);
                    if (!string.IsNullOrEmpty(idPRV_Row))
                    {
						ID_PRV = idPRV_Row.Trim();
						break;
					}
					
				}

                if (string.IsNullOrEmpty(ID_PRV.Trim()))
                    throw new Exception("PRV nenalezena...");

                string param =
                    Constants.Common.PRV +
                    Constants.Common.SLASH +
                    ID_PRV.Trim();


				//if (string.IsNullOrEmpty(ID_PRV.Trim()))
				//	throw new Exception("PRV nenalezena...");

				//string param =
				//	Constants.Common.PRV +
				//	Constants.Common.SLASH +
				//	ID_PRV.Trim();

				//if (string.IsNullOrEmpty(ID_PRV.Trim()))
				//	throw new Exception("PRV nenalezena...");

				//           string param =
				//Constants.Common.PRV +
				//Constants.Common.SLASH +
				//countEntries.ToString();
				//JSON = Classes.JSON_Class.Serialize_JSON("");

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "PRV_prelokovani", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_PRV_prelokovani(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}



private static string Odeslani_dat_prelokovani_PRV_OLD20251208_02(DataSets.Vydej dsV, int countEntries)
	{
		try
		{
			if (dsV == null || dsV.CZMST_SI == null || dsV.CZMST_SI.Count == 0)
				throw new Exception("DataSet neobsahuje žádný řádek v CZMST_SI.");

				IRestResponse restResponse;
				Guid g = Guid.NewGuid(); // párovací GUID

				// relativní URL z mailu – base URL je v konfiguraci WEBAPI
				string param = "testsab/script/fask/api/LogToStore";

				// pro jednoduchost beru první řádek – typicky budeš posílat jeden záznam
				var r = dsV.CZMST_SI[0]; //zde se budou posilat vsechny data na prelokovani

			var payload = new
			{
				// => CountEntries z parametru metody
				CountEntries = countEntries,

				SOPNUMBE = r.SOPNUMBE,
				ITEMNMBR = r.IsITEMNMBRNull() ? null : r.ITEMNMBR,
				ORD = r.ORD,
				VNDDOCNM = r.IsVNDDOCNMNull() ? null : r.VNDDOCNM,
				VNDITNUM = r.IsVNDITNUMNull() ? null : r.VNDITNUM,
				CZ_CarKod = r.IsCZ_CarKodNull() ? null : r.CZ_CarKod,
				SKL_ID = r.IsSKL_IDNull() ? null : r.SKL_ID,
				LOCNCODE = r.IsLOCNCODENull() ? null : r.LOCNCODE,

				MJ = r.IsMJNull() ? null : r.MJ,
				QTYSHPPD = r.QTYSHPPD,
				QTYPACK = r.QTYPACK,
				QTYSHPPDMJ = r.QTYSHPPDMJ,

				SERLTNUM = r.SERLTNUM,
				KOD_SW = r.IsKOD_SWNull() ? null : r.KOD_SW,
				DAT_VYROBY = r.IsDAT_VYROBYNull() ? null : r.DAT_VYROBY,

				REZ_1 = r.IsREZ_1Null() ? null : r.REZ_1,
				REZ_2 = r.IsREZ_2Null() ? null : r.REZ_2,
				ODBER_ID = r.IsODBER_IDNull() ? null : r.ODBER_ID,

				DATEDONE = r.IsDATEDONENull() ? null : r.DATEDONE,
				TIMEDONE = r.IsTIMEDONENull() ? null : r.TIMEDONE,
				USER_ID = r.USER_ID,
				DEX_ROW_ID = r.DEX_ROW_ID,

				TYPEPAL = r.IsTYPEPALNull() ? null : r.TYPEPAL,
				NMBRPAL = r.IsNMBRPALNull() ? null : r.NMBRPAL,
				PRINTED = r.IsPRINTEDNull() ? 0 : (r.PRINTED ? 1 : 0),

				GUID = r.IsGUIDNull() ? g.ToString("B") : r.GUID.ToString("B"),

				INPUT_MODE = r.IsINPUT_MODENull() ? (byte)1 : r.INPUT_MODE,
				ID_TERMINAL = r.IsID_TERMINALNull() ? 0 : r.ID_TERMINAL,

				ITEMCODE = r.IsITEMCODENull() ? null : r.ITEMCODE,
				WEIGHT = r.IsWEIGHTNull() ? (decimal?)null : r.WEIGHT,

				Expirace = r.IsExpiraceNull()
							? null
							: r.Expirace.ToString("yyyy-MM-ddTHH:mm:ss")
			};

			string JSON =Classes.JSON_Class.Serialize_JSON(payload); //JsonConvert.SerializeObject(payload);

			

					if (!Classes.WEBAPI.ABRAComunication.Communicate(
					WEBAPI.ABRAComunication.REST_Type.POST,
					out restResponse,
					param,
					JSON))
			{
				throw new Exception("Komunikace s IS ABRA (LogToStore) se nezdařila.");
			}


				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "PRV_prelokovani", param, g, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_PRV_prelokovani(restResponse, g);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}


		private static string Odeslani_dat_prelokovani_PRV(DataSets.Vydej dsV, int countEntries)
		{
			try
			{
				if (dsV == null || dsV.CZMST_SI == null || dsV.CZMST_SI.Count == 0)
					throw new Exception("DataSet neobsahuje žádný řádek v CZMST_SI.");

				IRestResponse restResponse;
				Guid g = Guid.NewGuid(); // párovací GUID pro logování

				#region 12.12.2025 old MaR
				//// ==============================
				//// 1) PŘÍPRAVA PAYLOADU PRO LogToStore – VŠECHNY ŘÁDKY
				//// ==============================
				////int count = countEntriesStart;

				//var payloadList = new List<object>();

				//foreach (var r in dsV.CZMST_SI)
				//{
				//	var payloadRow = new
				//	{
				//		// CountEntries
				//		CountEntries = countEntries,

				//		//SOPNUMBE = r.SOPNUMBE,  // v tvém JSON příkladu být může, schema ho má také
				//		//SOPNUMBE = r.SOPNUMBE,
				//		SOPNUMBE = "prelokovani",


				//		ITEMNMBR = r.IsITEMNMBRNull() ? null : r.ITEMNMBR,
				//		ORD = r.ORD,
				//		VNDDOCNM = r.IsVNDDOCNMNull() ? null : r.VNDDOCNM,
				//		VNDITNUM = r.IsVNDITNUMNull() ? null : r.VNDITNUM,
				//		CZ_CarKod = r.IsCZ_CarKodNull() ? null : r.CZ_CarKod,
				//		SKL_ID = r.IsSKL_IDNull() ? null : r.SKL_ID,
				//		LOCNCODE = r.IsLOCNCODENull() ? null : r.LOCNCODE,

				//		MJ = r.IsMJNull() ? null : r.MJ,
				//		QTYSHPPD = r.QTYSHPPD,
				//		QTYPACK = r.QTYPACK,
				//		QTYSHPPDMJ = r.QTYSHPPDMJ,

				//		SERLTNUM = r.SERLTNUM,
				//		KOD_SW = r.IsKOD_SWNull() ? null : r.KOD_SW,
				//		DAT_VYROBY = r.IsDAT_VYROBYNull() ? null : r.DAT_VYROBY,

				//		REZ_1 = r.IsREZ_1Null() ? null : r.REZ_1,
				//		REZ_2 = r.IsREZ_2Null() ? null : r.REZ_2,
				//		ODBER_ID = r.IsODBER_IDNull() ? null : r.ODBER_ID,

				//		DATEDONE = r.IsDATEDONENull() ? null : r.DATEDONE,
				//		TIMEDONE = r.IsTIMEDONENull() ? null : r.TIMEDONE,
				//		USER_ID = r.USER_ID,
				//		DEX_ROW_ID = r.DEX_ROW_ID,

				//		TYPEPAL = r.IsTYPEPALNull() ? null : r.TYPEPAL,
				//		NMBRPAL = r.IsNMBRPALNull() ? null : r.NMBRPAL,
				//		PRINTED = r.IsPRINTEDNull() ? 0 : (r.PRINTED ? 1 : 0),

				//		GUID = r.IsGUIDNull() ? g.ToString("B") : r.GUID.ToString("B"),

				//		INPUT_MODE = r.IsINPUT_MODENull() ? (byte)1 : r.INPUT_MODE,
				//		ID_TERMINAL = r.IsID_TERMINALNull() ? 0 : r.ID_TERMINAL,

				//		ITEMCODE = r.IsITEMCODENull() ? null : r.ITEMCODE,
				//		WEIGHT = r.IsWEIGHTNull() ? (decimal?)null : r.WEIGHT,

				//		Expirace = r.IsExpiraceNull()
				//			? null
				//			: r.Expirace.ToString("yyyy-MM-ddTHH:mm:ss")
				//	};

				//	payloadList.Add(payloadRow);
				//}

				//// JSON = pole objektů => [ { ... }, { ... }, ... ]
				//string json = Classes.JSON_Class.Serialize_JSON(payloadList); 
				#endregion

				// payload = obálka pro JSON
				CZMST_SI payload = new CZMST_SI();

				// inicializace pole rows na přesnou velikost datasetu
				payload.rows = new CZMST_SI_row[dsV.CZMST_SI.Count];
				int index = 0;

				foreach (Fask.DataSets.Vydej.CZMST_SIRow r in dsV.CZMST_SI)
				{
					CZMST_SI_row row = new CZMST_SI_row();

					row.CountEntries = countEntries;
					row.SOPNUMBE = "prelokovani";

					row.ITEMNMBR = r.IsITEMNMBRNull() ? null : r.ITEMNMBR;
					row.ORD = r.ORD;
					row.VNDDOCNM = r.IsVNDDOCNMNull() ? null : r.VNDDOCNM;
					row.VNDITNUM = r.IsVNDITNUMNull() ? null : r.VNDITNUM;
					row.CZ_CarKod = r.IsCZ_CarKodNull() ? null : r.CZ_CarKod;
					row.SKL_ID = r.IsSKL_IDNull() ? null : r.SKL_ID;
					row.LOCNCODE = r.IsLOCNCODENull() ? null : r.LOCNCODE;

					row.MJ = r.IsMJNull() ? null : r.MJ;
					row.QTYSHPPD = r.QTYSHPPD;
					row.QTYPACK = r.QTYPACK;
                    //row.QTYSHPPDMJ = r.IsQTYSHPPDMJNull() ? (decimal?)null : r.QTYSHPPDMJ;

                    try
                    {
                        row.QTYSHPPDMJ = r.QTYSHPPDMJ;
                    }
                    catch
                    {

						//row.QTYSHPPDMJ = (decimal?)null;
					}
					
					


					row.SERLTNUM = r.SERLTNUM;
					row.KOD_SW = r.IsKOD_SWNull() ? null : r.KOD_SW;
					row.DAT_VYROBY = r.IsDAT_VYROBYNull() ? null : r.DAT_VYROBY;

					row.REZ_1 = r.IsREZ_1Null() ? null : r.REZ_1;
					row.REZ_2 = r.IsREZ_2Null() ? null : r.REZ_2;
					row.ODBER_ID = r.IsODBER_IDNull() ? null : r.ODBER_ID;

					row.DATEDONE = r.IsDATEDONENull() ? null : r.DATEDONE;
					row.TIMEDONE = r.IsTIMEDONENull() ? null : r.TIMEDONE;
					row.USER_ID = r.USER_ID;

					row.DEX_ROW_ID = r.DEX_ROW_ID;

					row.TYPEPAL = r.IsTYPEPALNull() ? null : r.TYPEPAL;
					row.NMBRPAL = r.IsNMBRPALNull() ? null : r.NMBRPAL;
					row.PRINTED = r.IsPRINTEDNull() ? (byte)0 : (r.PRINTED ? (byte)1 : (byte)0);

					row.GUID = r.IsGUIDNull() ? g : r.GUID;

					row.INPUT_MODE = r.IsINPUT_MODENull() ? (byte)1 : r.INPUT_MODE;
					row.ID_TERMINAL = r.IsID_TERMINALNull() ? 0 : r.ID_TERMINAL;

					row.ITEMCODE = r.IsITEMCODENull() ? null : r.ITEMCODE;
					row.WEIGHT = r.IsWEIGHTNull() ? (decimal?)null : r.WEIGHT;

					row.Expirace = r.IsExpiraceNull() ? (DateTime?)null : r.Expirace;

					// přiřazení do payloadu
					payload.rows[index] = row;
					index++;
				}

				string json = Classes.JSON_Class.Serialize_JSON(payload.rows);


				// relativní cesta – base URL je v konfiguraci (např. http://localhost:8085/)
				string param = "script/fask/api/LogToStore";

				// ==============================
				// 2) ODESLÁNÍ JEDNOHO POST S VÍCE ŘÁDKY
				// ==============================
				if (!Classes.WEBAPI.ABRAComunication.Communicate(
						WEBAPI.ABRAComunication.REST_Type.POST,
						out restResponse,
						param,
						json))
				{
					throw new Exception("Komunikace s IS ABRA (LogToStore) se nezdařila.");
				}

				// volitelné logování requestu/response
				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Odeslani_dat_prelokovani_PRV", param, g, Constants.Common.txt);
				}


				return Classes.WEBAPI.RequestResponse.LoadResponse_PRV_prelokovani(restResponse, g);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}


		/// <summary>
		/// Metoda slouží k Editaci DL ze kterým se pracuje
		/// </summary>
		/// <param name="dsV"></param>
		/// <param name="iD_DL"></param>
		/// <param name="listOP"></param>
		/// <returns></returns>
		private static string Edit_Hlavny_PRV(DataSets.Vydej dsV, string iD_PRV)
		{
			try
			{
				var polEdit = new List<Polozky_Vydej>();
				var polDelete = new List<Polozky_Vydej>();
				Classes.Vydej.KontrolaVykriti_A_Smazani_PRV(dsV, out polEdit, out polDelete);

				if (polEdit.Count() > 0)
				{
					if (Edit_SERLTNUM_QTY_in_PRV(iD_PRV, polEdit) != "OK")
					{
						throw new Exception("Nelze editovat PRV:'" + iD_PRV + "'");
					}
				}

				if (polDelete.Count() > 0)
				{
					foreach (Polozky_Vydej item in polDelete)
					{
						if (Delete_in_PRV(iD_PRV, item) != "OK")
							throw new Exception("Nelze smazat ITEMNMBR:'" + item.ITEMNMBR + "'");
					}
				}

				return "OK";
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}

		}

		private static string Edit_SERLTNUM_QTY_in_PRV(string ID_PRV, List<Polozky_Vydej> polEdit)
		{
			try
			{

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (!Classes.WEBAPI.RequestResponse.CreateRequest_Edit_SERLTNUM_QTY_in_PRV(polEdit, G, out JSON))
				{
					throw new Exception("Vytvoření requestu se nezdařilo");
				}

				if (string.IsNullOrEmpty(ID_PRV.Trim()))
					throw new Exception("PRV nenalezena...");

				string param =
					Constants.Common.PRV +
					Constants.Common.SLASH +
					ID_PRV.Trim();

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Edit_SERLTNUM_QTY_in_PRV", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.PUT, out restResponse, param, JSON))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Edit_SERLTNUM_QTY_in_PRV(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}


		/// <summary>
		/// Metoda pomoci ktere se smaže jeden řadek, jedna položka v Dodacim Listu podle ID
		/// </summary>
		/// <param name="ID_DL">ID Dodaciho listu</param>
		/// <param name="polozky">Objekt položky</param>
		/// <returns></returns>
		private static string Delete_in_PRV(string ID_PRV, Polozky_Vydej polozky)
		{
			try
			{

				IRestResponse restResponse;

				string JSON = string.Empty;
				Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

				if (string.IsNullOrEmpty(ID_PRV.Trim()))
					throw new Exception("ID Prevodky vydej nezadano!");

				string ID_Row_PRV = null;

				ID_Row_PRV = Database.ABRA.Get_ID_Polozka_PrevodkyVydej(polozky);

				if (string.IsNullOrEmpty(ID_Row_PRV.Trim()))
					throw new Exception("ID radku Prevodky vydej nezadano!");


				string param =
					Constants.Common.PRV +
					Constants.Common.SLASH +
					ID_PRV.Trim() +
					Constants.Common.SLASH +
					Constants.Common.ROWS +
					Constants.Common.SLASH +
					ID_Row_PRV.Trim();

				if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
				{
					SAB.Constants.SaveToFile.Save(Constants.Common.URL, "Delete_in_PRV", param, G, Constants.Common.txt);
				}

				if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.DELETE, out restResponse, param))
				{
					throw new Exception("Komunikace s IS ABRA se nezdařila");
				}

				return Classes.WEBAPI.RequestResponse.LoadResponse_Delete_in_PRV(restResponse, G);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		/// <summary>
		/// Metoda pomoci které se překlopí upraveny Prevodka Vydej do Prevodka Prijem
		/// </summary>
		/// <param name="ID_DL">ID Dodaciho Listu</param>
		/// <returns></returns>
		private static Rootobject_PRP Import_PRV_to_PRP_all(string ID_PRV, string SOPNUMBE)
		{
			IRestResponse restResponse;

			string JSON = string.Empty;
			Guid G = Guid.NewGuid(); // Parovaci GUID ked je zapnute logovani...

			string DocQueue_ID = string.Empty;
			string Store_ID = string.Empty;

			Store_ID = Database.ABRA.Get_StoreID_By_IDPRV(ID_PRV);
			DocQueue_ID = Database.ABRA.Get_DocQueueID_PRP_By_IDPRV(ID_PRV);

			if (string.IsNullOrEmpty(Store_ID))
			{
				throw new Exception("Vytvoření requestu se nezdařilo, nevyplnení zdrojovy sklad");
			}

			if (string.IsNullOrEmpty(DocQueue_ID))
			{
				throw new Exception("Vytvoření requestu se nezdařilo, nevyplnene číslo řady");
			}

			//if (string.IsNullOrEmpty(ID_OP))
			//	throw new Exception("Objednava přijatá nenalezena...");

			if (!Classes.WEBAPI.RequestResponse.CreateRequest_Import_PRV_to_PRP_all(G, DocQueue_ID, Store_ID, out JSON))
			{
				throw new Exception("Vytvoření requestu se nezdařilo");
			}

			if (string.IsNullOrEmpty(ID_PRV))
				throw new Exception("Prevodka Vydej nenalezena...");

			string param =
				Constants.Common.PRP +
				Constants.Common.SLASH +
				Constants.Common.IMPORT +
				Constants.Common.SLASH +
				Constants.Common.PRV +
				Constants.Common.SLASH +
				ID_PRV;

			if (Globals_V1.Konfigurace.WEBAPI[0].LogovatRequestyResponsy)
			{
				SAB.Constants.SaveToFile.Save(Constants.Common.URL, "PRV_to_PRP_all", param, G, Constants.Common.txt);
			}

			if (!Classes.WEBAPI.ABRAComunication.Communicate(WEBAPI.ABRAComunication.REST_Type.POST, out restResponse, param, JSON))
			{
				throw new Exception("Komunikace s IS ABRA se nezdařila");
			}

			return Classes.WEBAPI.RequestResponse.LoadResponse_Import_PRV_to_PRP_all(restResponse, G);

		}


		#endregion



		#region Prelokovani

		#region 3.10.2025 MaR old
		///// <summary>
		///// Metoda pro exportovaní dat z IS ABRA lokace do FASK, předlodo co CZMST_SE
		///// </summary>
		///// <param name="objednavka">Objekt objednavky</param>
		///// <param name="sklad">objekt skladu</param>
		///// <returns></returns>
		//public static string Export_Prelokovani_ABRA(Objednavka objednavka, Sklad sklad)
		//{
		//	SqlTransaction trans = null;
		//	SqlConnection connection = null;

		//	try
		//	{

		//		if (String.IsNullOrEmpty(objednavka.ID.Trim()))
		//			throw new Exception("Číslo dokladu nesmí být prázdné");

		//		var dt_abra = Database.ABRA.Prelokovani_GetData(objednavka.ID, sklad.ID);

		//		if (dt_abra == null || dt_abra.Count == 0)
		//		{
		//			throw new Exception("Nenalezeny záznamy pro export");
		//		}

		//		int cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries();
		//		cisloDavky += 1;
		//		int poradiPolozky = 1;

		//		using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
		//		{
		//			connection.Open();
		//			trans = connection.BeginTransaction();

		//			SQL_Datasets.Vydej.CZMST_SEDataTable seTable = new SQL_Datasets.Vydej.CZMST_SEDataTable();

		//			//int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
		//			//int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;

		//			#region Foreach

		//			foreach (var Item in dt_abra)
		//			{
		//				#region 3.10.2025 MaR  nove prirazeni prvku podle issue #148
		//				var seRow = seTable.NewCZMST_SERow();

		//                      seRow.CountEntries = cisloDavky;
		//                      seRow.ITEMTYPE = "O";
		//                      seRow.ORD = poradiPolozky;

		//				//--------------
		//				//seRow.CountEntries = Item.CountEntries;
		//				seRow.SOPNUMBE = Item.SOPNUMBE ?? "";
		//				seRow.ITEMNMBR = Item.ITEMNMBR ?? "";
		//				//seRow.ITEMTYPE = Item.ITEMTYPE ?? "";
		//				seRow.ITEMDESC = Item.ITEMDESC ?? "";
		//				seRow.VNDDOCNM = Item.VNDDOCNM ?? "";
		//				seRow.VNDITNUM = Item.VNDITNUM ?? "";
		//				//seRow.ORD = Item.ORD;
		//				seRow.CZ_CarKod = Item.CZ_CarKod ?? "";
		//				seRow.LOCNCODE = Item.LOCNCODE ?? "";
		//				seRow.QTYSHPPD = Item.QTYSHPPD;
		//				seRow.QTYPACK = Item.QTYPACK;
		//				seRow.CZ_DatVyr_Track = Item.CZ_DatVyr_Track;
		//				seRow.CZ_DatVyr_Delka = Item.CZ_DatVyr_Delka;
		//				seRow.CZ_SerNum_Track = Item.CZ_SerNum_Track;
		//				seRow.CZ_SerNum_Delka = Item.CZ_SerNum_Delka;
		//				seRow.CZ_SW_Track = Item.CZ_SW_Track;
		//				seRow.CZ_SW_Delka = Item.CZ_SW_Delka;
		//				seRow.CZ_Doslo = Item.CZ_Doslo;
		//				seRow.Note = Item.Note ?? "";
		//				seRow.TYPEPAL = Item.TYPEPAL ?? "";
		//				seRow.QTYPAL = Item.QTYPAL;
		//				seRow.PRIORITY = Item.PRIORITY;
		//				seRow.PRINTED = Item.PRINTED;
		//				// seRow.DEX_ROW_ID – ne, ten je auto-increment v datasetu
		//				seRow.SKL_ID = Item.SKL_ID ?? "";
		//				seRow.MJ = Item.MJ ?? "";
		//				seRow.CZ_REZ1_Track = Item.CZ_REZ1_Track;
		//				seRow.CZ_REZ2_Track = Item.CZ_REZ2_Track;
		//				seRow.ITEMCODE = Item.ITEMCODE ?? "";
		//				seRow.WEIGHT = Item.WEIGHT;
		//				seRow.USERID = Item.USERID;
		//				seRow.CZ_Expirace_Track = Item.CZ_Expirace_Track;
		//				//--------------

		//				seTable.AddCZMST_SERow(seRow);

		//                      poradiPolozky += 1; 
		//                      #endregion

		//                      #region 25.9.2025 MaR doplnit kod podle zadani JaS
		//                      //25.9.2025 MaR doplnit kod podle zadani JaS

		//                      //                  if (Item.CZ_SerNum_Track < 0 || Item.CZ_SerNum_Track > 2)
		//                      //                  {
		//                      //                      string msg = string.Format("Byl proveden pokus o export do CZMST_SE položky, která ma nepodporovaný příznak sledování :'{0}' " + Environment.NewLine, Item.CZ_SerNum_Track);
		//                      //                      msg += string.Format("Položka : " + Environment.NewLine + "		ITEMDESC:{0}" + Environment.NewLine + "		ITEMNMBR:{1}" + Environment.NewLine + "		ITEMCODE:{2}" + Environment.NewLine + "		SKL_ID:{3}", Item.ITEMDESC, Item.ITEMNMBR, Item.ITEMCODE, Item.SKL_ID);
		//                      //                      Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, msg);

		//                      //                      continue;
		//                      //                  }



		//                      //                  seRow.ITEMDESC = Item.IsITEMDESCNull() ? null : Item.ITEMDESC;
		//                      //                  if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
		//                      //                  {
		//                      //                      seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
		//                      //                  }

		//                      //                  seRow.ITEMNMBR = Item.ITEMNMBR;
		//                      //                  seRow.ITEMTYPE = Item.IsITEMTYPENull() ? string.Empty : Item.ITEMTYPE;
		//                      //                  seRow.ITEMCODE = Item.IsITEMCODENull() ? null : Item.ITEMCODE;
		//                      //                  seRow.CountEntries = cisloDavky;

		//                      //                  seRow.CZ_CarKod = Item.IsITEMCODENull() ? null : Item.ITEMCODE;
		//                      //                  seRow.CZ_DatVyr_Delka = 0;
		//                      //                  seRow.CZ_DatVyr_Track = 0;
		//                      //                  seRow.CZ_Doslo = 0;
		//                      //                  seRow.CZ_SerNum_Delka = 0;
		//                      //                  seRow.CZ_SerNum_Track = Item.CZ_SerNum_Track; // TODO ??

		//                      //                  //Po uprave SQL tohle smazat
		//                      //                  //if (Item.CZ_SerNum_Track == 2)
		//                      //                  //	seRow.CZ_Expirace_Track = 1;
		//                      //                  //else
		//                      //                  //	seRow.CZ_Expirace_Track = 0;

		//                      //                  // a tohle odkomentovat
		//                      //                  seRow.CZ_Expirace_Track = Item.CZ_Expirace_Track;

		//                      //                  seRow.CZ_SW_Delka = 0;
		//                      //                  seRow.CZ_SW_Track = 0;

		//                      //                  seRow.LOCNCODE = string.Empty;
		//                      //                  seRow.Note = string.Empty;
		//                      //                  seRow.ORD = Item.ORD;
		//                      //                  seRow.PRINTED = 0;
		//                      //                  seRow.PRIORITY = 3;
		//                      //                  seRow.QTYPACK = Item.IsQTYPACKNull() ? 0 : Item.QTYPACK;
		//                      //                  seRow.QTYPAL = 0;
		//                      //                  seRow.QTYSHPPD = Item.QTYSHPPD;
		//                      //                  seRow.SKL_ID = Item.SKL_ID;
		//                      //                  seRow.SOPNUMBE = Item.SOPNUMBE;
		//                      //                  seRow.TYPEPAL = "";

		//                      //                  seRow.VNDDOCNM = "";
		//                      //                  seRow.VNDITNUM = Item.VNDITNUM;
		//                      //                  seRow.MJ = Item.MJ;
		//                      //                  if (seRow.MJ.Length > MJ_MaxLength)
		//                      //                  {
		//                      //                      seRow.MJ = seRow.ITEMDESC.Remove(MJ_MaxLength);
		//                      //                  }

		//                      //                  seRow.USERID = null;
		//                      //                  seRow.CZ_REZ1_Track = 0;
		//                      //seRow.CZ_REZ2_Track = 0;

		//                      //seRow.SetWEIGHTNull(); 
		//                      #endregion


		//                  }

		//			#endregion

		//			Database.Vydej.Update_CZMST_SE(seTable, connection, trans);

		//			if (trans != null)
		//				trans.Commit();

		//			objednavka.CisloDavky = cisloDavky.ToString();
		//		}

		//		return "OK";
		//	}
		//	catch (Exception ex)
		//	{
		//		if (trans != null) trans.Rollback();

		//		throw ex;
		//	}
		//	finally
		//	{
		//		if (connection != null && connection.State == System.Data.ConnectionState.Open)
		//			connection.Close();
		//	}
		//} 
		#endregion

		#region 3.10.2025 MaR new AI
		public static string Export_Prelokovani_ABRA_Vydej(Objednavka objednavka, Sklad sklad)
		{
			//objednavka.ID==""prelokovani

			int pocetZaznamuABRA = 0;
			if (objednavka == null) throw new ArgumentNullException(nameof(objednavka));
			if (sklad == null) throw new ArgumentNullException(nameof(sklad));
			if (string.IsNullOrWhiteSpace(objednavka.ID))
				throw new Exception("Číslo dokladu nesmí být prázdné");

			var dt_abra = Database.ABRA.Prelokovani_GetData_Vydej(objednavka.ID, sklad.ID);
			if (dt_abra == null || dt_abra.Count == 0)
			{
				throw new Exception("Nenalezeny záznamy pro export");
			}
   //         else
   //         {
			//	pocetZaznamuABRA = dt_abra.Count;
			//	Logging.ExceptionHandler2.Handle(LogLevel.Info,"Export_Prelokovani_ABRA", "Pocet záznamů: ", pocetZaznamuABRA.ToString());
			//}
			int cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries() + 1;
			int poradiPolozky = 1;

			int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
			int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;

			using (var connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
			{
				connection.Open();
				using (var trans = connection.BeginTransaction())
				{
					bool committed = false;
					try
					{
						var seTable = new SQL_Datasets.Vydej.CZMST_SEDataTable();

						foreach (var Item in dt_abra)
						{

							if (Item.CZ_SerNum_Track < 0 || Item.CZ_SerNum_Track > 2)
							{
								string msg = string.Format("Byl proveden pokus o export do CZMST_SE položky, která ma nepodporovaný příznak sledování :'{0}' " + Environment.NewLine, Item.CZ_SerNum_Track);
								msg += string.Format("Položka : " + Environment.NewLine + "		ITEMDESC:{0}" + Environment.NewLine + "		ITEMNMBR:{1}" + Environment.NewLine + "		ITEMCODE:{2}" + Environment.NewLine + "		SKL_ID:{3}", Item.ITEMDESC, Item.ITEMNMBR, Item.ITEMCODE, Item.SKL_ID);
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, msg);

								continue;
							}

							var seRow = seTable.NewCZMST_SERow();

							// — vlastnictví položky dávky uvnitř tohoto exportu —
							seRow.CountEntries = cisloDavky;
							seRow.ITEMTYPE = "J";
							seRow.ORD = poradiPolozky;

							// mapování z ABRA dat
							//seRow.SOPNUMBE = Item.SOPNUMBE ?? "prelokovani";
							seRow.SOPNUMBE = "prelokovani";
							seRow.ITEMNMBR = Item.ITEMNMBR ?? "";


							//seRow.ITEMDESC = Item.ITEMDESC ?? "";

							seRow.ITEMDESC = Item.IsITEMDESCNull() ? "" : Item.ITEMDESC;
							if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
							{
								seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
							}


							seRow.VNDDOCNM = Item.VNDDOCNM ?? "";
							seRow.VNDITNUM = Item.VNDITNUM ?? "";
							seRow.CZ_CarKod = Item.VNDITNUM ?? "";
							seRow.LOCNCODE = "RAMPA";
							seRow.QTYSHPPD = Item.QTYSHPPD;
							seRow.QTYPACK = Item.QTYPACK;
							seRow.CZ_DatVyr_Track = Item.CZ_DatVyr_Track;
							seRow.CZ_DatVyr_Delka = Item.CZ_DatVyr_Delka;
							seRow.CZ_SerNum_Track = Item.CZ_SerNum_Track;
							seRow.CZ_SerNum_Delka = Item.CZ_SerNum_Delka;
							seRow.CZ_SW_Track = Item.CZ_SW_Track;
							seRow.CZ_SW_Delka = Item.CZ_SW_Delka;
							seRow.CZ_Doslo = Item.CZ_Doslo;
							seRow.Note = Item.Note ?? "";
							seRow.TYPEPAL = Item.TYPEPAL ?? "";
							seRow.QTYPAL = Item.QTYPAL;
							seRow.PRIORITY = Item.PRIORITY;
							seRow.PRINTED = Item.PRINTED;
							seRow.SKL_ID = Item.SKL_ID ?? "";
							seRow.MJ = Item.MJ ?? "";

							if (seRow.MJ.Length > MJ_MaxLength)
							{
								seRow.MJ = seRow.ITEMDESC.Remove(MJ_MaxLength);
							}

							seRow.CZ_REZ1_Track = Item.CZ_REZ1_Track;
							seRow.CZ_REZ2_Track = Item.CZ_REZ2_Track;
							seRow.ITEMCODE = Item.ITEMCODE ?? "";
							seRow.WEIGHT = Item.WEIGHT;

							if(!Item.IsUSERIDNull())
                            {
								seRow.USERID = Item.USERID;
							}
						

							seRow.CZ_Expirace_Track = Item.CZ_Expirace_Track;

							seTable.AddCZMST_SERow(seRow);
							poradiPolozky++;
						}

						// IMPORTANT: tahle metoda NESMÍ uvnitř commit/rollbackovat, když dostává transakci
						Database.Vydej.Update_CZMST_SE(seTable, connection, trans);

						trans.Commit();
						committed = true;
						objednavka.CisloDavky = cisloDavky.ToString();
						return "OK";
					}
					catch (Exception ex)
					{
						Logging.ExceptionHandler2.Handle("ABRA.cs", "Export_Prelokovani_ABRA_Vydej", ex);
						if (!committed && trans?.Connection != null)
						{
							try { trans.Rollback(); } catch { }
						}
						throw;
					}
				}
			}
		}

		#endregion




		#region 22.10.2025 MaR new pro sekci Ostatni
		public static string Export_Prelokovani_ABRA_Ostatni(Objednavka objednavka, Sklad sklad)
		{
			//objednavka.ID==""prelokovani

			int pocetZaznamuABRA = 0;
			if (objednavka == null) throw new ArgumentNullException(nameof(objednavka));
			if (sklad == null) throw new ArgumentNullException(nameof(sklad));
			if (string.IsNullOrWhiteSpace(objednavka.ID))
				throw new Exception("Číslo dokladu nesmí být prázdné");

			var dt_abra = Database.ABRA.Prelokovani_GetData_Ostatni(objednavka.ID, sklad.ID);
			if (dt_abra == null || dt_abra.Count == 0)
			{
				throw new Exception("Nenalezeny záznamy pro export");
			}
			//         else
			//         {
			//	pocetZaznamuABRA = dt_abra.Count;
			//	Logging.ExceptionHandler2.Handle(LogLevel.Info,"Export_Prelokovani_ABRA", "Pocet záznamů: ", pocetZaznamuABRA.ToString());
			//}
			int cisloDavky = Database.Ostatni.CZMSTSE_MAX_CountEntries() + 1;
			int poradiPolozky = 1;

			int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
			int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;

			using (var connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
			{
				connection.Open();
				using (var trans = connection.BeginTransaction())
				{
					bool committed = false;
					try
					{
						var seTable = new SQL_Datasets.Ostatni.CZMST_SEDataTable();

						foreach (var Item in dt_abra)
						{

							if (Item.CZ_SerNum_Track < 0 || Item.CZ_SerNum_Track > 2)
							{
								string msg = string.Format("Byl proveden pokus o export do CZMST_SE položky, která ma nepodporovaný příznak sledování :'{0}' " + Environment.NewLine, Item.CZ_SerNum_Track);
								msg += string.Format("Položka : " + Environment.NewLine + "		ITEMDESC:{0}" + Environment.NewLine + "		ITEMNMBR:{1}" + Environment.NewLine + "		ITEMCODE:{2}" + Environment.NewLine + "		SKL_ID:{3}", Item.ITEMDESC, Item.ITEMNMBR, Item.ITEMCODE, Item.SKL_ID);
								Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, msg);

								continue;
							}

							var seRow = seTable.NewCZMST_SERow();

							// — vlastnictví položky dávky uvnitř tohoto exportu —
							seRow.CountEntries = cisloDavky;
							seRow.ITEMTYPE = "O";
							seRow.ORD = poradiPolozky;

							// mapování z ABRA dat
							seRow.SOPNUMBE = Item.SOPNUMBE ?? "";
							seRow.ITEMNMBR = Item.ITEMNMBR ?? "";


							//seRow.ITEMDESC = Item.ITEMDESC ?? "";

							seRow.ITEMDESC = Item.IsITEMDESCNull() ? "" : Item.ITEMDESC;
							if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
							{
								seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
							}


							seRow.VNDDOCNM = Item.VNDDOCNM ?? "";
							seRow.VNDITNUM = Item.VNDITNUM ?? "";
							seRow.CZ_CarKod = Item.VNDITNUM ?? "";
							seRow.LOCNCODE = "RAMPA";
							seRow.QTYSHPPD = Item.QTYSHPPD;
							seRow.QTYPACK = Item.QTYPACK;
							seRow.CZ_DatVyr_Track = Item.CZ_DatVyr_Track;
							seRow.CZ_DatVyr_Delka = Item.CZ_DatVyr_Delka;
							seRow.CZ_SerNum_Track = Item.CZ_SerNum_Track;
							seRow.CZ_SerNum_Delka = Item.CZ_SerNum_Delka;
							seRow.CZ_SW_Track = Item.CZ_SW_Track;
							seRow.CZ_SW_Delka = Item.CZ_SW_Delka;
							seRow.CZ_Doslo = Item.CZ_Doslo;
							seRow.Note = Item.Note ?? "";
							seRow.TYPEPAL = Item.TYPEPAL ?? "";
							seRow.QTYPAL = Item.QTYPAL;
							seRow.PRIORITY = Item.PRIORITY;
							seRow.PRINTED = Item.PRINTED;
							seRow.SKL_ID = Item.SKL_ID ?? "";
							seRow.MJ = Item.MJ ?? "";

							if (seRow.MJ.Length > MJ_MaxLength)
							{
								seRow.MJ = seRow.ITEMDESC.Remove(MJ_MaxLength);
							}

							seRow.CZ_REZ1_Track = Item.CZ_REZ1_Track;
							seRow.CZ_REZ2_Track = Item.CZ_REZ2_Track;
							seRow.ITEMCODE = Item.ITEMCODE ?? "";
							seRow.WEIGHT = Item.WEIGHT;

							if (!Item.IsUSERIDNull())
							{
								seRow.USERID = Item.USERID;
							}


							seRow.CZ_Expirace_Track = Item.CZ_Expirace_Track;

							seTable.AddCZMST_SERow(seRow);
							poradiPolozky++;
						}

						// IMPORTANT: tahle metoda NESMÍ uvnitř commit/rollbackovat, když dostává transakci
						Database.Ostatni.Update_CZMST_SE(seTable, connection, trans);

						trans.Commit();
						committed = true;
						objednavka.CisloDavky = cisloDavky.ToString();
						return "OK";
					}
					catch (Exception ex)
					{
						Logging.ExceptionHandler2.Handle("ABRA.cs", "Export_Prelokovani_ABRA_Ostatni", ex);
						if (!committed && trans?.Connection != null)
						{
							try { trans.Rollback(); } catch { }
						}
						throw;
					}
				}
			}
		}

		#endregion







		#endregion
	}
}
