using Fask.Module.ABRA.SAB.Classes;
using Fask.Server.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Database
{
	public class ABRA
	{

		//StoreCard
		//Třida Skladovej Karty
		//0 : Jednoduchá (na množství)
		//1 : Seriove číslo
		//2 : šarže
		//3 : MakroKarta
		//4 : Obal

		#region Sklad

		public static SQL_Datasets.Ciselniky.CZMST093DataTable Sklad_GetData()
		{
			SQL_Datasets.Ciselniky.CZMST093DataTable dataTable = new Fask.Module.ABRA.SAB.SQL_Datasets.Ciselniky.CZMST093DataTable();
			Sklad_Fill(dataTable);
			return dataTable;
		}

		public static void Sklad_Fill(SQL_Datasets.Ciselniky.CZMST093DataTable dataTable)
		{

			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;


					da.SelectCommand.CommandText = "SELECT " +
					"ID as skl_id, " +
					"NAME as skl_desc, " +
					"NULL as skl_typ, " +
					"CODE as skl_carcode " +
					"FROM Stores ";

					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);

					da.Fill(dataTable);
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(dataTable);
					Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " Sklad_Fill", ex);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}

		}

		#endregion

		#region Zasoby

		public static ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable Zasoby_GetData(string SKL_ID)
		{
			ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable dataTable = new ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable();
			Zasoby_Fill(dataTable, SKL_ID);
			return dataTable;
		}

		public static void Zasoby_Fill(ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable dataTable, string SKL_ID)
		{
			int pocetZaznamuSelect = 0;
			string select = string.Empty;
			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText = " SELECT " +
													" SC.ID AS ITEMNMBR, " +
													" SC.Name AS ITEMDESC, " +
													" SC.CODE AS ITEMCODE, " +
													" SE.EAN AS VNDITNUM, " +
													" SC.CODE AS CZ_CarKod, " +
													" SSC.STORE_ID AS SKL_ID, " +
													" SSC.Location_ID AS LOCNCODE, " +
													" SSC.Quantity AS QTY, " +
													" SU.UNITRATE AS QTYPACK, " +
													" SU.Code AS MJ, " +
													" SU.Weight AS WEIGHT ";
					
					if (Globals_V1.Konfigurace.ExportZasoby[0].SerNumTrack_Vsude)
					{
						da.SelectCommand.CommandText += ", SC.Category as CZ_SerNum_Track ";
						//da.SelectCommand.CommandText += ", 0 as CZ_Expirace_Track ";
						da.SelectCommand.CommandText += ", SC.X_CZ_Expirace_Track as CZ_Expirace_Track ";
					}
					else
					{
						da.SelectCommand.CommandText += ", 0 as CZ_SerNum_Track ";
						da.SelectCommand.CommandText += ", 0 as CZ_Expirace_Track ";
					}

                    da.SelectCommand.CommandText += " FROM STORECARDS AS SC " +
                                                    " JOIN STOREUNITS AS  SU ON SU.PARENT_ID = SC.ID " +
                                                    " JOIN STORESUBCARDS AS SSC ON SSC.STORECARD_ID = SC.ID " +
                                                    " JOIN STOREEANS AS SE ON SE.PARENT_ID = SU.ID ";




                    if (!string.IsNullOrEmpty(SKL_ID))
                    {
                        da.SelectCommand.CommandText += "WHERE SSC.STORE_ID IN(" + SKL_ID.Trim() + ") ";
                    }
                    //else
                    //{
                    //    da.SelectCommand.CommandText += "WHERE SSC.STORE_ID IN(" + hlavniSklad + ") ";
                    //}

                    //MaR 19.1.2024 SAB - doplneni pro serazeni podle pozadavku Hudec 
                    //da.SelectCommand.CommandText += " ORDER BY SC.ID, SSC.Store_ID";
					//da.SelectCommand.CommandText += " ORDER BY SSC.Store_ID";


					select = da.SelectCommand.CommandText; 
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Select: ", " Zasoby_Fill", select);


					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);

					//int pocetZaznamuSelect = 0;
                    pocetZaznamuSelect = da.Fill(dataTable);
					//Logging.ExceptionHandler2.Handle( Logging.LogLevel.Info,"ABRA Tabulky Select, pocet nactenych zaznamu: ", " Zasoby_Fill", pocetZaznamuSelect.ToString());
				}
				catch (Exception ex)
				{
					//pocetZaznamuSelect = da.Fill(dataTable);
					//Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "ABRA Tabulky Select, pocet nactenych zaznamu: ", " Zasoby_Fill", pocetZaznamuSelect.ToString());

					Logging.ExceptionHandler2.Handle(dataTable);
					Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " Zasoby_Fill", ex);
				}
				finally
				{
					// Blok finally se provede vždy, bez ohledu na to, zda došlo k chybě nebo ne
					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Počet načtených záznamů po try-catch dt: ", " Zasoby_Fill", dataTable.Count.ToString());

					Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Počet načtených záznamů po try-catch: ", " Zasoby_Fill", pocetZaznamuSelect.ToString());
				}

			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}

		}


		#endregion

		#region Docací list

		/// <summary>
		/// Metoda která kontroluje existenci Dodacího listu
		/// </summary>
		/// <param name="SOPNUMBE"></param>
		/// <returns></returns>
		internal static bool DodaciList_Exists(string SOPNUMBE)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel =
					" SELECT " +
					" (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as SOPNUMBE " +
					" FROM   StoreDocuments as A " +
					" JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
					" JOIN Periods P ON P.ID=A.Period_ID " +
					" JOIN PMSTATES PM ON PM.ID = A.PMSTATE_ID " +
					" WHERE  A.DocumentType='" + Constants.Common.DL_DocumentType + "' " +
					" AND A.finished = 'N' " +
					" AND PM.ID ='" + Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Vydej_KVydeji.Trim() + "'" +
					" AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE + "'";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return false; // neexistuje Dodaci List
				else
					return true; // existuje Dodaci List

			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		/// <summary>
		/// Metoda která kontroluje existenci Dodacího listu
		/// </summary>
		/// <param name="SOPNUMBE"></param>
		/// <returns></returns>
		internal static bool DodaciList_Exists_Process(string SOPNUMBE)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel =
					" SELECT " +
					" (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as SOPNUMBE " +
					" FROM   StoreDocuments as A " +
					" JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
					" JOIN Periods P ON P.ID=A.Period_ID " +
					" JOIN PMSTATES PM ON PM.ID = A.PMSTATE_ID " +
					" WHERE  A.DocumentType='" + Constants.Common.DL_DocumentType + "' " +
					" AND A.finished = 'N' " +
					" AND PM.ID ='" + Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Vydej_VyskladnujeSe.Trim() + "'" +
					" AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE + "'";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return false; // neexistuje Dodaci List
				else
					return true; // existuje Dodaci List

			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}


		/// <summary>
		/// Metoda která vrací data do predlohy z DL ve stavu k Výdeji
		/// </summary>
		/// <param name="SOPNUMBE"></param>
		/// <param name="SKL_ID"></param>
		/// <returns></returns>
		internal static ABRA_Datasets.Vydej.CZMST_SEDataTable DodaciList_GetData(string SOPNUMBE, string SKL_ID)
		{
			Globals_V1.LoadConfiguration();

			ABRA_Datasets.Vydej.CZMST_SEDataTable dataTable = new Fask.Module.ABRA.SAB.ABRA_Datasets.Vydej.CZMST_SEDataTable();
			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					// Select rozsirit o :
					// SELECT  SM.TEXT as VNDOCNM FROM STORECARDS AS SC
					// JOIN STOREMENU SM ON  SM.ID = SC.STOREMENUITEM_ID

					//ROzsiri dataset

					da.SelectCommand.CommandText =
												" SELECT " +
												" SC.CODE as ITEMCODE, " +
												" SC.NAME as ITEMDESC, " +
												//" B.ID as ITEMNMBR, " +
												" B.STORECARD_ID as ITEMNMBR, " +
												" B.ROWTYPE as ITEMTYPE, " +
												" B.POSINDEX as ORD, " +
												" (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as SOPNUMBE, " +
												" B.STORE_ID as SKL_ID, " +
												" SEAN.EAN as VNDITNUM, " +
												" SM.TEXT as VNDDOCNM, " +
												" B.QUANTITY as  QTYSHPPD, " +
												" SU.CODE as MJ, " +
												//" SU.UNITRATE AS QTYPACK ";
												" CASE " +
												"   WHEN SU.UNITRATE = 1 AND SU.CODE = SC.MAINUNITCODE AND SEAN.EAN = SC.EAN THEN 0 " +
												"   ELSE SU.UNITRATE " +
												" END AS QTYPACK ";


					if (Globals_V1.Konfigurace.ExportZasoby[0].SerNumTrack_Vsude)
					{
						da.SelectCommand.CommandText += ", SC.Category as CZ_SerNum_Track ";
						//da.SelectCommand.CommandText += ", 1 as CZ_Expirace_Track ";
						da.SelectCommand.CommandText += ", SC.X_CZ_Expirace_Track as CZ_Expirace_Track ";
					}
					else
					{
						da.SelectCommand.CommandText += ", 0 as CZ_SerNum_Track ";
						da.SelectCommand.CommandText += ", 0 as CZ_Expirace_Track ";
					}

					da.SelectCommand.CommandText += " FROM   StoreDocuments as A  " +
												" JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
												" JOIN Periods P ON P.ID=A.Period_ID " +
												" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
												" JOIN Stores S ON S.ID = B.STORE_ID " +
												" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
												" JOIN STOREUNITS SU ON SU.PARENT_ID = SC.ID " + 
												" JOIN STOREEANS SEAN ON SEAN.PARENT_ID = SU.ID " +
												" JOIN PMSTATES PM ON PM.ID = A.PMSTATE_ID " +
												" JOIN STOREMENU SM ON  SM.ID = SC.STOREMENUITEM_ID " +
												" WHERE A.DocumentType='" + Constants.Common.DL_DocumentType + "' " +
												" AND A.FINISHED = 'N' " +
												" AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) ='" + SOPNUMBE + "'" +
												" AND B.STORE_ID ='" + SKL_ID + "'" +
												" AND PM.ID ='" + Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Vydej_KVydeji.Trim() + "'" +
												" AND ( SC.Category >= 0 AND SC.Category < 3 ) " +
												" ORDER BY   B.PosIndex ";


					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
					da.SelectCommand.Connection.Open();
					da.Fill(dataTable);
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " sSklad_Fill", ex);
					Logging.ExceptionHandler2.Handle(dataTable);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}
			return dataTable;

		}

		/// <summary>
		/// Metoda která vrací data do docasne tabulky lokaci ABRA
		/// </summary>
		/// <param name="SOPNUMBE"></param>
		/// <param name="SKL_ID"></param>
		/// <returns></returns>
		internal static ABRA_Datasets.Lokace.Lokace_ABRADataTable Lokace_ABRA_GetData(string SOPNUMBE, string SKL_ID)
		{
			Globals_V1.LoadConfiguration();

			ABRA_Datasets.Lokace.Lokace_ABRADataTable dataTable = new Fask.Module.ABRA.SAB.ABRA_Datasets.Lokace.Lokace_ABRADataTable();
			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;


					#region 12.9.2025 MaR doplnit po dohode s JaS
					//zde bych potreboval doplnit popisky odpovidajicichm sloupcum nasi tabulky CZMST094
					//potrebujeme SKL_ID,LCNCODE,Description,Barcode
					//lsp.BARCODE je prvek pro CZMST094 Barcode
					//                    string sql = @"
					//SELECT
					//    lsp.ID AS Position_ID, --cemu odpovida v tabulce CZMST094??
					//    lsp.Code AS PositionCode, --cemu odpovida v tabulce CZMST094?? -- je LCNCODE
					//    lsp.Name AS PositionName, --cemu odpovida v tabulce CZMST094?? -- je Description
					//    lsp.Store_ID AS Store_ID, --cemu odpovida v tabulce CZMST094??
					//    lsc.StoreCard_ID, --cemu odpovida v tabulce CZMST094??
					//    lsc.Quantity, --sloupec navic ktery se do tabulky CZMST094 nevleze
					//    lsc.QuantityReserved, --sloupec navic ktery se do tabulky CZMST094 nevleze
					//    lsc.QuantityAwaited, --sloupec navic ktery se do tabulky CZMST094 nevleze
					//	lsp.BARCODE
					//FROM LogStoreContents lsc
					//JOIN LogStorePositions lsp
					//    ON lsp.ID = lsc.Parent_ID
					//WHERE
					//    1 = 1  
					//    AND lsc.Quantity > 0
					//";





					string sql = @"
SELECT
    lsp.ID AS Position_ID,
    lsp.Code AS PositionCode,
    lsp.Name AS PositionName,
    lsp.Store_ID AS Store_ID,
    lsc.StoreCard_ID,
    lsc.Quantity,
    lsc.QuantityReserved,
    lsc.QuantityAwaited,
	lsp.BARCODE
FROM LogStoreContents lsc
JOIN LogStorePositions lsp
    ON lsp.ID = lsc.Parent_ID
WHERE
    1 = 1  
    AND lsc.Quantity > 0
";

					#region 12.9.2025 MaR prepsat na zaklade dodanych informaci od SAB
					if (!string.IsNullOrEmpty(SKL_ID))
                    {
                        sql += " AND lsp.Store_ID = @Store_ID";
                    }
                    else
                    {
                        sql += " AND lsp.Store_ID = @Store_ID";
                    }

                    if (!string.IsNullOrEmpty(SOPNUMBE))
                    {
                        sql += " AND lsc.StoreCard_ID = @StoreCard_ID";
                    }

                    sql += " ORDER BY lsp.Code;";

                    da.SelectCommand.CommandText = sql;

                    // --- Parametry ---
                    //cislo dokladu? pokud neni dodana hodnota nepridavam do podminky
                    if (!string.IsNullOrEmpty(SOPNUMBE))
                    {
                        var pStoreCard = new FirebirdSql.Data.FirebirdClient.FbParameter("@StoreCard_ID", FirebirdSql.Data.FirebirdClient.FbDbType.VarChar);
                        pStoreCard.Value = SOPNUMBE;
                        da.SelectCommand.Parameters.Add(pStoreCard);
                    }

                    // Store_ID sklad? pokud neni dodana hodnota nastavuji na vychozi
                    if (!string.IsNullOrEmpty(SKL_ID))
                    {
                        var pStoreId = new FirebirdSql.Data.FirebirdClient.FbParameter("@Store_ID", FirebirdSql.Data.FirebirdClient.FbDbType.VarChar);
                        pStoreId.Value = SKL_ID;
                        da.SelectCommand.Parameters.Add(pStoreId);
                    }
                    else
                    {
                        var pStoreId = new FirebirdSql.Data.FirebirdClient.FbParameter("@Store_ID", FirebirdSql.Data.FirebirdClient.FbDbType.VarChar);
                        pStoreId.Value = "2100000101";
                        da.SelectCommand.Parameters.Add(pStoreId);
                    }


                    #endregion 
                    #endregion




                    da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
					da.SelectCommand.Connection.Open();
					da.Fill(dataTable);
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " sSklad_Fill", ex);
					Logging.ExceptionHandler2.Handle(dataTable);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}
			return dataTable;

		}

		#region 22.9.2025 MaR stara nechodici metoda

		//        /// <summary>
		//        /// Metoda která vrací data do docasne tabulky lokaci-ciselnik pozic z ABRA IS
		//        /// </summary>
		//        /// <param name="SOPNUMBE"></param>
		//        /// <param name="SKL_ID">cislo skladu</param>
		//        /// <returns></returns>
		//        internal static ABRA_Datasets.Lokace.Lokace_Ciselnik_Pozic_ABRADataTable Lokace_Ciselnik_Pozic_ABRA_GetData(string SKL_ID)
		//        {
		//            Globals_V1.LoadConfiguration();

		//            ABRA_Datasets.Lokace.Lokace_Ciselnik_Pozic_ABRADataTable dataTable = new Fask.Module.ABRA.SAB.ABRA_Datasets.Lokace.Lokace_Ciselnik_Pozic_ABRADataTable();
		//            try
		//            {

		//                FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
		//                try
		//                {
		//                    da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
		//                    da.SelectCommand.CommandType = System.Data.CommandType.Text;


		//                    //potrebujeme SKL_ID,LCNCODE,Description,Barcode
		//                    //lsp.BARCODE je prvek pro CZMST094 Barcode
		//                    //                    string sql = @"
		//                    //SELECT
		//                    //    lsp.Code AS PositionCode, --cemu odpovida v tabulce CZMST094?? -- je LCNCODE
		//                    //    lsp.Name AS PositionName, --cemu odpovida v tabulce CZMST094?? -- je Description
		//                    //    lsp.Store_ID AS Store_ID, --cemu odpovida v tabulce CZMST094?? -- je SKL_ID
		//                    //	  lsp.BARCODE --cemu odpovida v tabulce CZMST094?? -- je Barcode
		//                    //FROM LogStorePositions lsp
		//                    //WHERE
		//                    //    1 = 1  
		//                    //";



		//                    string sql = @"
		//SELECT
		//    lsp.CODE,
		//    lsp.NAME,
		//    lsp.STORE_ID,
		//	lsp.BARCODE
		//FROM LogStorePositions lsp
		//WHERE
		//    1 = 1  
		//";

		//                    if (!string.IsNullOrEmpty(SKL_ID))
		//                    {
		//                        sql += " AND lsp.STORE_ID = @Store_ID";
		//                    }
		//                    else
		//                    {
		//                        sql += " AND lsp.STORE_ID = @Store_ID";
		//                    }

		//                    sql += " ORDER BY lsp.CODE;";

		//                    da.SelectCommand.CommandText = sql;

		//                    // --- Parametry ---
		//                    // Store_ID sklad? pokud neni dodana hodnota nastavuji na vychozi
		//                    if (!string.IsNullOrEmpty(SKL_ID))
		//                    {
		//                        var pStoreId = new FirebirdSql.Data.FirebirdClient.FbParameter("@Store_ID", FirebirdSql.Data.FirebirdClient.FbDbType.VarChar);
		//                        pStoreId.Value = SKL_ID;
		//                        da.SelectCommand.Parameters.Add(pStoreId);
		//                    }
		//                    else
		//                    {
		//                        var pStoreId = new FirebirdSql.Data.FirebirdClient.FbParameter("@Store_ID", FirebirdSql.Data.FirebirdClient.FbDbType.VarChar);
		//                        pStoreId.Value = "2100000101";
		//                        da.SelectCommand.Parameters.Add(pStoreId);
		//                    }


		//                    da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
		//                    da.SelectCommand.Connection.Open();
		//                    da.Fill(dataTable);
		//                }
		//                catch (Exception ex)
		//                {
		//                    Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " Lokace_Ciselnik_Pozic_ABRA_GetData", ex);
		//                    Logging.ExceptionHandler2.Handle(dataTable);
		//                }
		//            }
		//            catch (System.Exception ex)
		//            {
		//                Logging.ExceptionHandler2.Handle(ex);
		//            }
		//            return dataTable;

		//        }

		#endregion

		#region 22.9.2025 MaR oprava a zprovozneni transakce
		internal static ABRA_Datasets.Lokace.Lokace_Ciselnik_Pozic_ABRADataTable Lokace_Ciselnik_Pozic_ABRA_GetData(string SKL_ID)
		{
			Globals_V1.LoadConfiguration();

			var table = new Fask.Module.ABRA.SAB.ABRA_Datasets.Lokace.Lokace_Ciselnik_Pozic_ABRADataTable();

			string baseSql;

			if (!string.IsNullOrEmpty(SKL_ID))
			{
				baseSql = @"
SELECT
    lsp.CODE,
    lsp.NAME,
    lsp.STORE_ID,
    lsp.BARCODE
FROM LogStorePositions lsp
WHERE lsp.STORE_ID = @Store_ID
ORDER BY lsp.CODE;";
			}
			else
			{
				baseSql = @"
SELECT
    lsp.CODE,
    lsp.NAME,
    lsp.STORE_ID,
    lsp.BARCODE
FROM LogStorePositions lsp
ORDER BY lsp.CODE;";
			}

			try
			{
				using (var conn = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
				using (var cmd = new FirebirdSql.Data.FirebirdClient.FbCommand(baseSql, conn))
				using (var da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter(cmd))
				{
					cmd.CommandType = System.Data.CommandType.Text;

					if (!string.IsNullOrEmpty(SKL_ID))
					{
						// Zvolte typ dle DB schématu (VarChar/Integer/BigInt). Zde ponecháno jako VarChar.
						cmd.Parameters.Add(new FirebirdSql.Data.FirebirdClient.FbParameter("@Store_ID", FirebirdSql.Data.FirebirdClient.FbDbType.VarChar)
						{
							Value = SKL_ID
						});
					}

					conn.Open();
					da.Fill(table);
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", "Lokace_Ciselnik_Pozic_ABRA_GetData", ex);
				Logging.ExceptionHandler2.Handle(table);
				throw ex;
			}

			return table;
		}


		#endregion


		/// <summary>
		/// Metoda která vrací ID Dodacího listu 
		/// </summary>
		/// <param name="SOPNUMBE"></param>
		/// <param name="SKL_ID"></param>
		/// <returns></returns>
		internal static string Get_ID_DodaciList(string SOPNUMBE, string SKL_ID)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT " +
								" A.ID " +
								" FROM StoreDocuments as A " +
								" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
								" JOIN Periods P ON P.ID = A.Period_ID " +
								" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
								" JOIN Stores S ON S.ID = B.STORE_ID " +
								" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
								" WHERE A.DocumentType = '" + Constants.Common.DL_DocumentType + "' " +
								" AND A.finished = 'N' " +
								" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE + "' " +
								" AND B.STORE_ID = '" + SKL_ID + "' " +
								" GROUP BY A.ID ";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID dodaciho listu
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		//      /// <summary>
		//      /// Metoda která vrací ID stavu dle definovaneho nazvu v konfiguraci
		//      /// </summary>
		//      /// <param name="stav"></param>
		//      /// <returns></returns>
		//      internal static string Get_ID_PMState(string stav)
		//{
		//	FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
		//	try
		//	{
		//		string sel =
		//			" SELECT ID FROM PMSTATES " +
		//			" WHERE CODE = '" + stav.Trim() + "'";
		//			//" AND CLSID = '" + Constants.Common.CLSID + "'";

		//		FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
		//			sel,
		//			FBConnection
		//			);

		//		FBCommand.Connection.Open();
		//		object o = FBCommand.ExecuteScalar();
		//		if ((o == null) || (o is DBNull) || (o == DBNull.Value))
		//			return null; // nenalezen
		//		else
		//			return (string)o; // ID dodaciho listu
		//	}
		//	finally
		//	{
		//		if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
		//			FBConnection.Close();
		//	}
		//}

		/// <summary>
		/// Metoda která vrací ID Objednavky přijatek, z kterej jsou položky v Dodacil Listu
		/// </summary>
		/// <param name="polozky"></param>
		/// <returns></returns>
		internal static string Get_ID_OP(Polozky_Vydej polozky)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel =
					" SELECT " +
					" B.PROVIDE_ID " +
					" FROM StoreDocuments as A " +
					" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
					" JOIN Periods P ON P.ID = A.Period_ID " +
					" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
					" JOIN Stores S ON S.ID = B.STORE_ID " +
					" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
					" WHERE A.DocumentType = '" + Constants.Common.DL_DocumentType + "' " +
					" AND A.finished = 'N' " +
					" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + polozky.SOPNUMBE.Trim() + "' " +
					" AND B.STORE_ID = '" + polozky.SKL_ID.Trim() + "' " +
					" AND B.STORECARD_ID = '" + polozky.ITEMNMBR.Trim() + "' " +
					" ORDER BY B.PosIndex ";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID dodaciho listu
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		/// <summary>
		/// Metoda která vrací ID Objednavky přijate, z kterej je Dodaci List
		/// </summary>
		/// <param name="polozky"></param>
		/// <returns></returns>
		internal static string Get_ID_OP(string SOPNUMBE)
		{
			try
			{

				string sel =
					" SELECT " +
					" distinct B.PROVIDE_ID " +
					" FROM StoreDocuments as A " +
					" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
					" JOIN Periods P ON P.ID = A.Period_ID " +
					" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
					" JOIN Stores S ON S.ID = B.STORE_ID " +
					" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
					" WHERE A.DocumentType = '" + Constants.Common.DL_DocumentType + "' " +
					" AND A.finished = 'N' " +
					" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE.Trim() + "' ";

				using (var FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
				{
					using (var FBCommand = FBConnection.CreateCommand())
					{
						FBCommand.CommandType = System.Data.CommandType.Text;
						FBCommand.CommandText = sel;

						FBCommand.Connection.Open();
						object o = FBCommand.ExecuteScalar();
						if ((o == null) || (o is DBNull) || (o == DBNull.Value))
							return null; // nenalezen
						else
							return (string)o; // ID OP
					}

				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return null;

			}
		}

		/// <summary>
		/// Metoda která vrací ID řadku položky, Objednavky Přijatej
		/// </summary>
		/// <param name="polozky"></param>
		/// <returns></returns>
		internal static string Get_ID_Row_OP(Polozky_Vydej polozky)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT " +
								" B.PROVIDEROW_ID " +
								" FROM   StoreDocuments as A " +
								" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
								" JOIN Periods P ON P.ID = A.Period_ID " +
								" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
								" JOIN Stores S ON S.ID = B.STORE_ID " +
								" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
								" WHERE A.DocumentType = '" + Constants.Common.DL_DocumentType + "' " +
								" AND A.finished = 'N' " +
								" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + polozky.SOPNUMBE.Trim() + "' " +
								" AND B.STORE_ID = '" + polozky.SKL_ID.Trim() + "' " +
								" AND B.STORECARD_ID = '" + polozky.ITEMNMBR.Trim() + "' " +
								" ORDER BY B.PosIndex ";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID dodaciho listu
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		/// <summary>
		/// Metoda kter8 vrací ID Skladove karty pro položku
		/// </summary>
		/// <param name="SOPNUMBE"></param>
		/// <param name="ITEMNMBR"></param>
		/// <param name="SKL_ID"></param>
		/// <returns></returns>
		internal static string Get_STORECARD_ID_z_DL(string SOPNUMBE, string ITEMNMBR, string SKL_ID)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT " +
								" B.STORECARD_ID " +
								" FROM   StoreDocuments as A " +
								" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
								" JOIN Periods P ON P.ID = A.Period_ID " +
								" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
								" JOIN Stores S ON S.ID = B.STORE_ID " +
								" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
								" WHERE A.DocumentType = '" + Constants.Common.DL_DocumentType + "' " +
								" AND A.finished = 'N' " +
								" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE + "' " +
								" AND B.STORE_ID = '" + SKL_ID + "' " +
								" AND B.STORECARD_ID = '" + ITEMNMBR + "' " +
								" ORDER BY B.PosIndex ";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID Skladove karty
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		internal static string Get_ID_StoreBatch(string SELTNUM, DateTime? exp, string ITEMNMBR)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{
				string sel =
					" SELECT ID FROM STOREBATCHES " +
					" WHERE NAME = '" + SELTNUM.Trim() + "'" +
					" AND STORECARD_ID = '" + ITEMNMBR.Trim() + "'";

				if(exp.HasValue)
				{
					sel += " AND EXPIRATIONDATE$DATE = " + exp.Value.ToOADate();
				}

				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID šarže
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		internal static string Get_ID_Polozka_DodaciList(Polozky_Vydej radek)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT " +
								" B.ID " +
								" FROM   StoreDocuments as A " +
								" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
								" JOIN Periods P ON P.ID = A.Period_ID " +
								" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
								" JOIN Stores S ON S.ID = B.STORE_ID " +
								" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
								" WHERE A.DocumentType = '" + Constants.Common.DL_DocumentType + "' " +
								" AND A.finished = 'N' " +
								" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + radek.SOPNUMBE.Trim() + "' " +
								" AND B.STORE_ID = '" + radek.SKL_ID.Trim() + "' " +
								" AND B.STORECARD_ID = '" + radek.ITEMNMBR.Trim() + "' ";
				if (radek.ORD.HasValue)
				{
					sel += " AND B.POSINDEX = '" + radek.ORD.Value.ToString() + "' ";
				}

				sel += " ORDER BY B.PosIndex ";

				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID Skladove karty
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		internal static string Get_IS_CREATEINVOICE_By_IDPRV(string iD_DL)
		{
			try
			{

				string sel =
					" SELECT " +
					" X_IS_CREATEINVOICE " +
					" FROM StoreDocuments " +
					" WHERE ID = '" + iD_DL.Trim() + "'";

				using (var FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
				{
					using (var FBCommand = FBConnection.CreateCommand())
					{
						FBCommand.CommandType = System.Data.CommandType.Text;
						FBCommand.CommandText = sel;

						FBCommand.Connection.Open();
						object o = FBCommand.ExecuteScalar();
						if ((o == null) || (o is DBNull) || (o == DBNull.Value))
							return null; // nenalezen
						else
							return (string)o;
					}

				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return null;

			}

		}

		internal static string Get_DocQueueID_FV_By_IDPRV(string iD_DL)
		{
			try
			{

				string sel =
					" SELECT " +
					" X_DOCQUEUES_FV_ID" +
					" FROM StoreDocuments " +
					" WHERE ID = '" + iD_DL.Trim() + "'";

				using (var FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
				{
					using (var FBCommand = FBConnection.CreateCommand())
					{
						FBCommand.CommandType = System.Data.CommandType.Text;
						FBCommand.CommandText = sel;

						FBCommand.Connection.Open();
						object o = FBCommand.ExecuteScalar();
						if ((o == null) || (o is DBNull) || (o == DBNull.Value))
							return null; // nenalezen
						else
							return (string)o;
					}

				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return null;

			}

		}


		#endregion

		#region Prijemka

		internal static bool Prijemka_Exists(string PONUMBER)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = "SELECT " +
				" (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as SOPNUMBE " +
				" FROM   StoreDocuments as A " +
				" JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
				" JOIN Periods P ON P.ID=A.Period_ID " +
				" JOIN PMSTATES PM ON PM.ID = A.PMSTATE_ID " +
				" WHERE  A.DocumentType='" + Constants.Common.PR_DocumentType + "' " +
				" AND A.finished = 'N' " +
				" AND PM.ID ='" + Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Prijem_KNaskladneni.Trim() + "'" +
				" AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + PONUMBER + "'";

				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return false; // neexistuje cislo faktury
				else
					return true; // existuje cislo faktury

			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

        internal static ABRA_Datasets.Prijem.CZMST_PEDataTable Prijemka_GetData(string PONUMBER, string SKL_ID)
		{
			ABRA_Datasets.Prijem.CZMST_PEDataTable dataTable = new Fask.Module.ABRA.SAB.ABRA_Datasets.Prijem.CZMST_PEDataTable();
			try
			{
				Globals_V1.LoadConfiguration();
				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText =
							" SELECT " +
							" SC.CODE as ITEMCODE, " +
							" SC.NAME as ITEMDESC, " +
							" B.STORECARD_ID as ITEMNMBR, " +
							" B.POSINDEX as ORD, " +
							" (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as PONUMBER, " +
							" B.STORE_ID as SKL_ID, " +
							" SEAN.EAN as VNDITNUM, " +
							" B.QUANTITY as  QTYSHPPD, " +
							" SU.CODE as MJ, " +
							//" SU.UNITRATE AS QTYPACK ";
							" CASE " +
							"   WHEN SU.UNITRATE = 1 AND SU.CODE = SC.MAINUNITCODE AND SEAN.EAN = SC.EAN THEN 0 " +
							"   ELSE SU.UNITRATE " +
							" END AS QTYPACK ";

					if (Globals_V1.Konfigurace.ExportZasoby[0].SerNumTrack_Vsude)
					{
						da.SelectCommand.CommandText += ", SC.Category as CZ_SerNum_Track ";
						//da.SelectCommand.CommandText += ", 1 as CZ_Expirace_Track ";
						da.SelectCommand.CommandText += ", SC.X_CZ_Expirace_Track as CZ_Expirace_Track ";
						
					}
					else
					{
						da.SelectCommand.CommandText += ", 0 as CZ_SerNum_Track ";
						da.SelectCommand.CommandText += ", 0 as CZ_Expirace_Track ";
					}


					da.SelectCommand.CommandText += " FROM   StoreDocuments as A  " +
							" JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
							" JOIN Periods P ON P.ID=A.Period_ID " +
							" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
							" JOIN Stores S ON S.ID = B.STORE_ID " +
							" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
							" JOIN STOREUNITS SU ON SU.PARENT_ID = SC.ID " +
							" JOIN STOREEANS SEAN ON SEAN.PARENT_ID = SU.ID " +
							" JOIN PMSTATES PM ON PM.ID = A.PMSTATE_ID " +
							" WHERE  A.DocumentType='" + Constants.Common.PR_DocumentType + "' " +
							" AND A.FINISHED = 'N' " +
							" AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) ='" + PONUMBER + "'" +
							" AND B.STORE_ID ='" + SKL_ID + "'" +
							" AND PM.ID ='" + Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Prijem_KNaskladneni.Trim() + "'" +
							" AND ( SC.Category >= 0 AND SC.Category < 3 ) " +
							" ORDER BY   B.PosIndex ";


					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
					da.SelectCommand.Connection.Open();
					da.Fill(dataTable);
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(dataTable);
					Logging.ExceptionHandler2.Handle(ex);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}
			return dataTable;

		}


		/// <summary>
		/// Tahle metoda by mnela vracet seznam všech nerealizovanych přijemek...
		/// </summary>
		/// <param name="TerminalID"></param>
		/// <param name="SKL_ID"></param>
		/// <param name="List_C_Kodu"></param>
		/// <returns></returns>
		internal static Obecne Prijemka_GetDavkyByCarKody(int TerminalID, string SKL_ID, string List_C_Kodu)
		{

			Obecne obecne = new Obecne();
			System.Data.DataSet ds = new System.Data.DataSet();

			try
			{

				string sel =
					" SELECT " +
					" (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as PONUMBER, " +
					" F.NAME AS Name," +
					" (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as CZ_CarKod, " +
					" A.DESCRIPTION AS DESC, " +
					" A.DocDate$DATE AS DateTime " +
					" FROM StoreDocuments as A " +
					" JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
					" JOIN Periods P ON P.ID=A.Period_ID " +
					" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID" +
					" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
					" JOIN FIRMS F ON F.ID = A.FIRM_ID" +
					" JOIN PMSTATES PM ON PM.ID = A.PMSTATE_ID " +
					" WHERE  A.DocumentType='" + Constants.Common.PR_DocumentType + "' " +
					" AND PM.ID ='" + Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Prijem_KNaskladneni.Trim() + "'" +
					" AND A.finished = 'N'";

				if (!string.IsNullOrEmpty(List_C_Kodu))
				{
					sel += " AND SC.EAN IN ('" + List_C_Kodu + "')";
				}

				sel += " GROUP BY DQ.Code, A.OrdNumber, P.Code, F.NAME, A.DESCRIPTION, A.DocDate$DATE";

				using (var ada = new FirebirdSql.Data.FirebirdClient.FbDataAdapter())
				using (var con = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
				{
					using (var com = con.CreateCommand())
					{
						ada.SelectCommand = com;
						ada.SelectCommand.CommandText = sel;
						ada.SelectCommand.CommandType = System.Data.CommandType.Text;

						ada.Fill(ds);
					}
				}

				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					var table = ds.Tables[0];

					foreach (System.Data.DataRow row in table.Rows)
					{
						try
						{
							string PONUMBER = (string)row["PONUMBER"];
							string Name = (string)row["Name"];
							string CZ_CarKod = (string)row["CZ_CarKod"];
							string DESC = (string)row["DESC"];
							double dat = (double)row["DateTime"];
							DateTime dateTime;

							//if (false)
							//{
							//	double tmpRok = (dat - 1) / 365; // dat je (43973 - 1) / 356 = 120,4712329
							//	int tROK = (int)tmpRok; // 120  
							//	int ROK = tROK + 1900; // 120 + 1900 = 2020 ROK
							//	double tmpMesic = ((tmpRok - tROK) * 365) / 30; // 120,4712329 -120 = 0,4712329 * 365 = 172 / 30 = 5.7333333333
							//	int MESIC = (int)tmpMesic; // 5 mesic
							//	int DEN = Convert.ToInt32((tmpMesic - MESIC) * 30);
							//	dateTime = new DateTime(ROK, MESIC, DEN);
							//}
							//else
							//{
								dateTime = new DateTime(1899, 12, 30).AddDays(dat);
							//}

							obecne.Prijemky.AddPrijemkyRow(
								PONUMBER,
								Name,
								CZ_CarKod,
								dateTime,
								DESC
								);
						}
						catch
						{
							continue;
						}
					}

					obecne.Prijemky.AcceptChanges();

				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}

			return obecne;
		}

		internal static string Get_ID_PR(string PONUMBER, string SKL_ID)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT " +
								" A.ID " +
								" FROM   StoreDocuments as A " +
								" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
								" JOIN Periods P ON P.ID = A.Period_ID " +
								" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
								" JOIN Stores S ON S.ID = B.STORE_ID " +
								" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
								" WHERE A.DocumentType = '" + Constants.Common.PR_DocumentType + "' " +
								" AND A.finished = 'N' " +
								" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + PONUMBER + "' " +
								" AND B.STORE_ID = '" + SKL_ID + "' " +
								" GROUP BY A.ID ";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID dodaciho listu
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		internal static string Get_ID_Polozka_Prijemka(Polozky_Prijem radek)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT " +
								" B.ID " +
								" FROM StoreDocuments as A " +
								" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
								" JOIN Periods P ON P.ID = A.Period_ID " +
								" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
								" JOIN Stores S ON S.ID = B.STORE_ID " +
								" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
								" WHERE A.DocumentType = '" + Constants.Common.PR_DocumentType + "' " +
								" AND A.finished = 'N' " +
								" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + radek.PONUMBER.Trim() + "' " +
								" AND B.STORE_ID = '" + radek.SKL_ID.Trim() + "' " +
								" AND B.STORECARD_ID = '" + radek.ITEMNMBR.Trim() + "' ";
				if (radek.ORD.HasValue)
				{
					sel += " AND B.POSINDEX = '" + radek.ORD.Value.ToString() + "' ";
				}

				sel += " ORDER BY B.PosIndex ";

				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID Skladove karty
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		/// <summary>
		/// Metoda která vrací ID Objednavky vydanej, z kterej jsou položky v Dodacil Listu
		/// </summary>
		/// <param name="polozky"></param>
		/// <returns></returns>
		internal static string Get_ID_OV(Polozky_Prijem polozky)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel =
					" SELECT " +
					" B.PROVIDE_ID " +
					" FROM StoreDocuments as A " +
					" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
					" JOIN Periods P ON P.ID = A.Period_ID " +
					" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
					" JOIN Stores S ON S.ID = B.STORE_ID " +
					" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
					" WHERE A.DocumentType = '" + Constants.Common.PR_DocumentType + "' " +
					" AND A.finished = 'N' " +
					" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + polozky.PONUMBER.Trim() + "' " +
					" AND B.STORE_ID = '" + polozky.SKL_ID.Trim() + "' " +
					" AND B.STORECARD_ID = '" + polozky.ITEMNMBR.Trim() + "' " +
					" ORDER BY B.PosIndex ";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID dodaciho listu
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		/// <summary>
		/// Metoda která vrací ID řadku položky, Objednavky Přijatej
		/// </summary>
		/// <param name="polozky"></param>
		/// <returns></returns>
		internal static string Get_ID_Row_OV(Polozky_Prijem polozky)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT " +
								" B.PROVIDEROW_ID " +
								" FROM   StoreDocuments as A " +
								" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
								" JOIN Periods P ON P.ID = A.Period_ID " +
								" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
								" JOIN Stores S ON S.ID = B.STORE_ID " +
								" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
								" WHERE A.DocumentType = '" + Constants.Common.PR_DocumentType + "' " +
								" AND A.finished = 'N' " +
								" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + polozky.PONUMBER.Trim() + "' " +
								" AND B.STORE_ID = '" + polozky.SKL_ID.Trim() + "' " +
								" AND B.STORECARD_ID = '" + polozky.ITEMNMBR.Trim() + "' " +
								" ORDER BY B.PosIndex ";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID řadku Prijemky
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}


		#endregion

		#region Inventura

		internal static Fask.DataSets.Inventury1 SeznamInventurProExport()
		{
			// TODO zmenit na DIP(dilčí inventarny protokol)
			Fask.DataSets.Inventury1 dataTable = new Fask.DataSets.Inventury1();
			try
			{
				Globals_V1.LoadConfiguration();
				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText =
						 " SELECT 0 AS CountEntries, (DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) AS DESC " +
						 " FROM PARTIALINVPROTOCOLS AS PP " +
						 " JOIN MAININVPROTOCOLS AS MP ON MP.ID = PP.MAINPROTOCOL_ID " +
						 " JOIN DocQueues DQ ON DQ.ID = PP.DOCQUEUE_ID " +
						 " JOIN Periods P ON P.ID = PP.Period_ID " +
						 " WHERE PP.CLOSED = 'N' " +
						 "  AND  MP.STARTEDAT$DATE > 0 " +
						 " GROUP BY(DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code), MP.STARTEDAT$DATE";

					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);

					da.Fill(dataTable, dataTable.Hlavicky_Seznam.TableName);

					return dataTable;
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(dataTable);
					Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " SeznamInventurProExport", ex);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}

			return null;

		}

		internal static ABRA_Datasets.Inventura.CZMST_I1HDataTable SeznamInventur()
		{
			// TODO zmenit na DIP(dilčí inventarny protokol)
			ABRA_Datasets.Inventura.CZMST_I1HDataTable dataTable = new ABRA_Datasets.Inventura.CZMST_I1HDataTable();
			try
			{
				Globals_V1.LoadConfiguration();
				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText = "SELECT (DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as Description " +
					"FROM PARTIALINVPROTOCOLS AS PP " +
					"JOIN DocQueues DQ ON DQ.ID = PP.DOCQUEUE_ID " +
					"JOIN Periods P ON P.ID = PP.Period_ID " +
					"AND PP.CLOSED = 'N'";

					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);

					da.Fill(dataTable);

					return dataTable;
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(dataTable);
					Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " SeznamInventur", ex);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}

			return null;

		}

		internal static ABRA_Datasets.Inventura.CZMST_I1DataTable PolozkyInventur(
			string DIP = null
			)
		{
			// TODO zmenit na DIP(dilčí inventarny protokol)
			ABRA_Datasets.Inventura.CZMST_I1DataTable dataTable = new ABRA_Datasets.Inventura.CZMST_I1DataTable();
			try
			{
				Globals_V1.LoadConfiguration();

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText =
									" SELECT " +
									" (DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) AS CountEntries, " +
									" SC.ID AS ITEMNMBR, " +
									" SC.CODE as CZ_CarKod, " +
									" SC.EAN as VNDITNUM, " +
									" SC.NAME AS ITEMDESC, " +
									" SSC.STORE_ID AS SKL_ID, " +
									" MPR.DOCUMENTEDQUANTITY AS QUANTITY, " +
									" SC.CODE AS ITEMCODE, " +
									" MPR.QUNIT AS MJ";


					if (Globals_V1.Konfigurace.ExportZasoby[0].SerNumTrack_Vsude)
					{
						da.SelectCommand.CommandText += ", SC.Category as CZ_SerNum_Track ";

						da.SelectCommand.CommandText += ", SC.X_CZ_Expirace_Track as CZ_Expirace_Track ";
						//da.SelectCommand.CommandText += ", 1 as CZ_Expirace_Track ";
					}
					else
					{
						da.SelectCommand.CommandText += ", 0 as CZ_SerNum_Track ";
						da.SelectCommand.CommandText += ", 0 as CZ_Expirace_Track ";
					}

					if (Globals_V1.Konfigurace.Inventura1[0].IDTerm_Predloha)
					{
						da.SelectCommand.CommandText += ", PP.READERID AS TerminalID ";
					}
					else
					{
						da.SelectCommand.CommandText += ", 0 AS TerminalID ";
					}


					da.SelectCommand.CommandText += " FROM PARTIALINVPROTOCOLS AS PP " +
									" LEFT JOIN PARTIALINVPROTOCOLROWS AS PPR ON PPR.PARENT_ID = PP.ID " +
									" LEFT JOIN MAININVPROTOCOLROWS MPR ON MPR.ID = PPR.MIPROW_ID " +
									" LEFT JOIN STORECARDS AS SC ON SC.ID = MPR.STORECARD_ID " +
									" LEFT JOIN MAININVPROTOCOLS AS MP ON MP.ID = MPR.PARENT_ID " +
									" LEFT JOIN StoreSubCards SSC ON SSC.StoreCard_ID = SC.ID " +  " AND SSC.STORE_ID = MP.STORE_ID " +
									" JOIN DocQueues DQ ON DQ.ID = PP.DOCQUEUE_ID " +
									" JOIN Periods P ON P.ID = PP.Period_ID " +
									" WHERE PP.CLOSED = 'N' " +
									" AND SC.HIDDEN = 'N' " +
									" AND SSC.INVENTORYSTATUS = 1 ";

					if (!string.IsNullOrEmpty(DIP))
					{
						da.SelectCommand.CommandText += " AND (DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + DIP.Trim() + "'";
					}


					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);

					da.Fill(dataTable);

					return dataTable;
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(dataTable);
					Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " PolozkyInventur", ex);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}

			return null;
		}

		internal static ABRA_Datasets.Inventura.CZMST_I2DataTable PolozkySarzeInventur(
			string DIP = null
			)
		{
			ABRA_Datasets.Inventura.CZMST_I2DataTable dataTable = new ABRA_Datasets.Inventura.CZMST_I2DataTable();
			try
			{
				Globals_V1.LoadConfiguration();

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					//Tady se bude dotahovat předloha, ale až podle přiznaku na karte... 
					//takže pak vznikde asi union... kde budou všechny s přiznakem, a ty bez přiznaku budou mit Expiraci NULL...

					da.SelectCommand.CommandText =
									" SELECT " +
									" (DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) AS CountEntries, " +
									" SB.STORECARD_ID AS ITEMNMBR, " +
									" SC.NAME , " +
									" SB.NAME AS SERLNMBR, " +
									" MPR.DOCUMENTEDQUANTITY AS QTY ";

				    da.SelectCommand.CommandText += ", iif(SC.X_CZ_Expirace_Track > 0, SB.EXPIRATIONDATE$DATE, NULL) as Expirace";
					//da.SelectCommand.CommandText += ", SB.EXPIRATIONDATE$DATE as Expirace";
					

					da.SelectCommand.CommandText += " FROM PARTIALINVPROTOCOLS AS PP " +
									" LEFT JOIN PARTIALINVPROTOCOLROWS AS PPR ON PPR.PARENT_ID = PP.ID " +
									" LEFT JOIN PARTIALINVPROTOCOLBATCHES AS PPB ON PPB.PARENT_ID = PPR.ID " +
									" LEFT JOIN MAININVPROTOCOLROWS AS MPR ON MPR.ID = PPR.MIPROW_ID " +
									" LEFT JOIN MAININVPROTOCOLS AS MP ON MP.ID = MPR.PARENT_ID " +
									" LEFT JOIN MAININVPROTOCOLBATCHES AS MPB ON MPB.ID = PPB.MIPBATCH_ID " +
									" LEFT JOIN STOREBATCHES AS SB ON SB.ID = MPB.STOREBATCH_ID " +
									" LEFT JOIN STORESUBCARDS AS SSC ON SSC.STORECARD_ID = SB.STORECARD_ID AND SSC.STORE_ID = MP.STORE_ID " +
									" LEFT JOIN STORECARDS AS SC ON SC.ID = SSC.STORECARD_ID " +
									" LEFT JOIN DocQueues DQ ON DQ.ID = PP.DOCQUEUE_ID " +
									" LEFT JOIN Periods P ON P.ID = PP.Period_ID " +
									" WHERE PP.CLOSED = 'N' " +
									" AND SSC.INVENTORYSTATUS = 1";

					if (!string.IsNullOrEmpty(DIP))
					{
						da.SelectCommand.CommandText += " AND (DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + DIP.Trim() + "'";
					}

					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);

					da.Fill(dataTable);

					return dataTable;
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(dataTable);
					Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " PolozkyInventur", ex);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}

			return null;
		}

		internal static ABRA_Datasets.Inventura.CZMST_I3DataTable PolozkyInventur_MJ_Eans(
			string DIP = null
			)
		{
			// TODO zmenit na DIP(dilčí inventarny protokol)
			ABRA_Datasets.Inventura.CZMST_I3DataTable dataTable = new ABRA_Datasets.Inventura.CZMST_I3DataTable();
			try
			{
				Globals_V1.LoadConfiguration();

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText =
									" SELECT " +
									" (DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) AS CountEntries, " +
									" SC.ID AS ITEMNMBR, " +
									" SC.CODE as CZ_CarKod, " +
									" SEAN.EAN as VNDITNUM, " +
									//" SU.UNITRATE AS QTYPACK ";
									" CASE " +
									"   WHEN SU.UNITRATE = 1 AND SU.CODE = SC.MAINUNITCODE AND SEAN.EAN = SC.EAN THEN 0 " +
									"   ELSE SU.UNITRATE " +
									" END AS QTYPACK, " + 
									" SU.CODE AS MJ " +
									" FROM PARTIALINVPROTOCOLS AS PP " +
									" LEFT JOIN PARTIALINVPROTOCOLROWS AS PPR ON PPR.PARENT_ID = PP.ID " +
									" LEFT JOIN MAININVPROTOCOLROWS MPR ON MPR.ID = PPR.MIPROW_ID " +
									" LEFT JOIN STORECARDS AS SC ON SC.ID = MPR.STORECARD_ID " +
									" LEFT JOIN MAININVPROTOCOLS AS MP ON MP.ID = MPR.PARENT_ID " +
									" LEFT JOIN StoreSubCards SSC ON SSC.StoreCard_ID = SC.ID " +  " AND SSC.STORE_ID = MP.STORE_ID " +
									" LEFT JOIN STOREUNITS SU ON SU.PARENT_ID = SC.ID " +
									" LEFT JOIN STOREEANS SEAN ON SEAN.PARENT_ID = SU.ID " +
									" JOIN DocQueues DQ ON DQ.ID = PP.DOCQUEUE_ID " +
									" JOIN Periods P ON P.ID = PP.Period_ID " +
									" WHERE PP.CLOSED = 'N' " +
									" AND SC.HIDDEN = 'N' " +
									" AND SSC.INVENTORYSTATUS = 1 " +
									" AND SEAN.EAN IS NOT NULL ";

					if (!string.IsNullOrEmpty(DIP))
					{
						da.SelectCommand.CommandText += " AND (DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + DIP.Trim() + "'";
					}

					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);

					da.Fill(dataTable);

					return dataTable;
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(dataTable);
					Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " PolozkyInventur_MJ_Eans", ex);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}

			return null;
		}


		internal static string Get_ID_DIPRow(string dIP, string sKL_ID, string iTEMNMBR)
		{


			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT PPR.ID FROM PARTIALINVPROTOCOLROWS PPR " +
								" LEFT JOIN PARTIALINVPROTOCOLS PP ON PP.ID = PPR.PARENT_ID " +
								" LEFT JOIN DocQueues DQ ON DQ.ID = PP.DOCQUEUE_ID " +
								" LEFT JOIN Periods P ON P.ID = PP.Period_ID " +
								" LEFT JOIN MAININVPROTOCOLROWS AS MPR ON MPR.ID = PPR.MIPROW_ID " +
								" LEFT JOIN MAININVPROTOCOLS AS MP ON MP.ID = MPR.PARENT_ID " +
								" LEFT JOIN STORES AS S ON S.ID = MP.STORE_ID " +
								" LEFT JOIN STORECARDS AS SC ON SC.ID = MPR.STORECARD_ID " +
								" WHERE(DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + dIP.Trim() + "' " +
								" AND SC.ID = '" + iTEMNMBR.Trim() + "' " +
								" AND S.ID = '" + sKL_ID.Trim() + "' ";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID Radku DIP
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		internal static string Get_ID_DipBatchRow(string dIP, string sKL_ID, string iTEMNMBR, string SERLNMBR, DateTime? exp)
		{


			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT PPB.ID FROM PARTIALINVPROTOCOLROWS PPR " +
								" LEFT JOIN PARTIALINVPROTOCOLS PP ON PP.ID = PPR.PARENT_ID " +
								" LEFT JOIN PARTIALINVPROTOCOLBATCHES AS PPB ON PPB.PARENT_ID = PPR.ID " +
								" LEFT JOIN DocQueues DQ ON DQ.ID = PP.DOCQUEUE_ID " +
								" LEFT JOIN Periods P ON P.ID = PP.Period_ID " +
								" LEFT JOIN MAININVPROTOCOLROWS AS MPR ON MPR.ID = PPR.MIPROW_ID " +
								" LEFT JOIN MAININVPROTOCOLS AS MP ON MP.ID = MPR.PARENT_ID " +
								" LEFT JOIN MAININVPROTOCOLBATCHES MPB ON MPB.ID = PPB.MIPBATCH_ID " +
								" LEFT JOIN STORES AS S ON S.ID = MP.STORE_ID " +
								" LEFT JOIN STORECARDS AS SC ON SC.ID = MPR.STORECARD_ID " +
								" LEFT JOIN STOREBATCHES AS SB ON SB.ID = MPB.STOREBATCH_ID " +
								" WHERE(DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + dIP.Trim() + "' " +
								" AND SC.ID = '" + iTEMNMBR.Trim() + "' " +
								" AND S.ID = '" + sKL_ID.Trim() + "' " +
								" AND SB.NAME = '" + SERLNMBR.Trim() + "'";
				
				if(exp.HasValue)
					sel += " AND SB.EXPIRATIONDATE$DATE = " + exp.Value.ToOADate();

				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID Radku DIP
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		internal static string Get_ID_HIPBatch(string dIP, string sKL_ID, string iTEMNMBR, string ID_Sarze)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT MPB.ID FROM PARTIALINVPROTOCOLROWS PPR " +
								" LEFT JOIN PARTIALINVPROTOCOLS PP ON PP.ID = PPR.PARENT_ID " +
								" LEFT JOIN PARTIALINVPROTOCOLBATCHES AS PPB ON PPB.PARENT_ID = PPR.ID " +
								" LEFT JOIN DocQueues DQ ON DQ.ID = PP.DOCQUEUE_ID " +
								" LEFT JOIN Periods P ON P.ID = PP.Period_ID " +
								" LEFT JOIN MAININVPROTOCOLROWS AS MPR ON MPR.ID = PPR.MIPROW_ID " +
								" LEFT JOIN MAININVPROTOCOLS AS MP ON MP.ID = MPR.PARENT_ID " +
								" LEFT JOIN MAININVPROTOCOLBATCHES MPB ON MPB.ID = PPB.MIPBATCH_ID " +
								" LEFT JOIN STORES AS S ON S.ID = MP.STORE_ID " +
								" LEFT JOIN STORECARDS AS SC ON SC.ID = MPR.STORECARD_ID " +
								//" LEFT JOIN STOREBATCHES AS SB ON SB.ID = MPB.STOREBATCH_ID " +
								" WHERE(DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + dIP.Trim() + "' " +
								" AND SC.ID = '" + iTEMNMBR.Trim() + "' " +
								" AND S.ID = '" + sKL_ID.Trim() + "' " +
								" AND MPB.STOREBATCH_ID = '" + ID_Sarze.Trim() + "'";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID Radku DIP
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		internal static string Get_ID_HIPRow(string dIP, string sKL_ID, string iTEMNMBR)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT MPR.ID FROM PARTIALINVPROTOCOLROWS PPR " +
								" LEFT JOIN PARTIALINVPROTOCOLS PP ON PP.ID = PPR.PARENT_ID " +
								" LEFT JOIN PARTIALINVPROTOCOLBATCHES AS PPB ON PPB.PARENT_ID = PPR.ID " +
								" LEFT JOIN DocQueues DQ ON DQ.ID = PP.DOCQUEUE_ID " +
								" LEFT JOIN Periods P ON P.ID = PP.Period_ID " +
								" LEFT JOIN MAININVPROTOCOLROWS AS MPR ON MPR.ID = PPR.MIPROW_ID " +
								" LEFT JOIN MAININVPROTOCOLS AS MP ON MP.ID = MPR.PARENT_ID " +
								" LEFT JOIN MAININVPROTOCOLBATCHES MPB ON MPB.ID = PPB.MIPBATCH_ID " +
								" LEFT JOIN STORES AS S ON S.ID = MP.STORE_ID " +
								" LEFT JOIN STORECARDS AS SC ON SC.ID = MPR.STORECARD_ID " +
								//" LEFT JOIN STOREBATCHES AS SB ON SB.ID = MPB.STOREBATCH_ID " +
								" WHERE(DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + dIP.Trim() + "' " +
								" AND SC.ID = '" + iTEMNMBR.Trim() + "' " +
								" AND S.ID = '" + sKL_ID.Trim() + "' ";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID Radku DIP
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		internal static bool Get_SarzeExpirace(string dIP, string sKL_ID, string iTEMNMBR, string SERLNMBR, DateTime? exp, out string IDSarzeRow)
		{
			IDSarzeRow = string.Empty;
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				IDSarzeRow = Get_ID_StoreBatch(SERLNMBR.Trim(), null, iTEMNMBR.Trim());

				if (!exp.HasValue)
					return true;


				string sel = " SELECT SB.EXPIRATIONDATE$DATE FROM PARTIALINVPROTOCOLROWS PPR " +
								" LEFT JOIN PARTIALINVPROTOCOLS PP ON PP.ID = PPR.PARENT_ID " +
								" LEFT JOIN PARTIALINVPROTOCOLBATCHES AS PPB ON PPB.PARENT_ID = PPR.ID " +
								" LEFT JOIN DocQueues DQ ON DQ.ID = PP.DOCQUEUE_ID " +
								" LEFT JOIN Periods P ON P.ID = PP.Period_ID " +
								" LEFT JOIN MAININVPROTOCOLROWS AS MPR ON MPR.ID = PPR.MIPROW_ID " +
								" LEFT JOIN MAININVPROTOCOLS AS MP ON MP.ID = MPR.PARENT_ID " +
								" LEFT JOIN MAININVPROTOCOLBATCHES MPB ON MPB.ID = PPB.MIPBATCH_ID " +
								" LEFT JOIN STORES AS S ON S.ID = MP.STORE_ID " +
								" LEFT JOIN STORECARDS AS SC ON SC.ID = MPR.STORECARD_ID " +
								" LEFT JOIN STOREBATCHES AS SB ON SB.ID = MPB.STOREBATCH_ID " +
								" WHERE(DQ.Code || '-' || CAST(PP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + dIP.Trim() + "' " +
								" AND SC.ID = '" + iTEMNMBR.Trim() + "' " +
								" AND S.ID = '" + sKL_ID.Trim() + "' " +
								" AND SB.NAME = '" + SERLNMBR.Trim() + "'";

				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();

				double? dat = 0;

				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					dat = null;
				else
					dat = (double)o;

				if (dat.HasValue)
				{
					DateTime dateTime = new DateTime(1899, 12, 30).AddDays(dat.Value);

					if ((dateTime.Year == exp.Value.Year) && (dateTime.Month == exp.Value.Month) && (dateTime.Day == exp.Value.Day) )
					{
						return true;
					}
					else
                    {
						return false;
                    }

				}
				else
				{
					throw new Exception("Hodnota datumu nenalezena pro šarži: " + SERLNMBR.Trim());
				}

			}
			catch ( Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		internal static bool Get_SarzeExpirace(string sKL_ID, string iTEMNMBR, string SERLNMBR, DateTime? exp, out string IDSarzeRow)
		{
			IDSarzeRow = string.Empty;
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				IDSarzeRow = Get_ID_StoreBatch(SERLNMBR.Trim(), null, iTEMNMBR.Trim());

				if (!exp.HasValue)
					return true;


				string sel =	" SELECT SB.EXPIRATIONDATE$DATE " + 
								" FROM  STORECARDS AS SC " +
								" LEFT JOIN STORESUBCARDS SSC ON SSC.STORECARD_ID = SC.ID " +
								" LEFT JOIN STORES AS S ON S.ID = SSC.STORE_ID" +
								" LEFT JOIN STOREBATCHES AS SB ON SB.STORECARD_ID = SC.ID " +
								" WHERE SC.ID = '" + iTEMNMBR.Trim() + "' " +
								" AND S.ID = '" + sKL_ID.Trim() + "' " +
								" AND SB.NAME = '" + SERLNMBR.Trim() + "'";

				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();

				double? dat = 0;

				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					dat = null;
				else
					dat = (double)o;

				if (dat.HasValue)
				{
					DateTime dateTime = new DateTime(1899, 12, 30).AddDays(dat.Value);

					if ((dateTime.Year == exp.Value.Year) && (dateTime.Month == exp.Value.Month) && (dateTime.Day == exp.Value.Day))
					{
						return true;
					}
					else
					{
						return false;
					}

				}
				else
				{
					throw new Exception("Hodnota datumu nenalezena pro šarži: " + SERLNMBR.Trim());
				}

			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}


		#endregion

		#region Online Lok Mech

		internal static ABRA_Datasets.Vydej Get_Vydej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum)
		{
			ABRA_Datasets.Vydej ds = new ABRA_Datasets.Vydej();

			try
			{
				Globals_V1.LoadConfiguration();
				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					string select = GetScript_Vydej_Online_GetMaterial(itemnmbr, skl_id, serltnum);

					da.SelectCommand.CommandText = select;

					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
					da.SelectCommand.Connection.Open();
					da.Fill(ds, ds.Items.TableName);
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(ds);
					Logging.ExceptionHandler2.Handle(ex);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}
			return ds;
		}

		//private static string GetScript(string itemnmbr, string skl_id, string serltnum)
		//{
		//	string Command = string.Empty;

		//	if(string.IsNullOrEmpty(serltnum))
		//	{
		//		Command += 
		//					" SELECT " +
		//					" ROW_NUMBER() OVER (ORDER BY SB.EXPIRATIONDATE$DATE) AS IndexXX, " + // Tohle je blby... Index ve firebird nejde dat jak alias
		//					" SC.ID AS Itemnmbr, " +
		//					" SC.NAME AS Itemdesc, " +
		//					" SSC.STORE_ID AS Skl_id, " +
		//					" NULL AS Locncode, " +
		//					" SSC.QUANTITY AS Qty, " +
		//					" SB.NAME AS Serltnum, " +
		//					" SB.EXPIRATIONDATE$DATE AS Expiration, " + // ?? jak datetime anebo double?
		//					" NULL AS Prijem " + // je tohle važně čas přijmu??
		//					" FROM STORECARDS AS SC " +
		//					" JOIN STORESUBCARDS AS SSC ON SSC.STORECARD_ID = SC.ID " +
		//					" JOIN STOREBATCHES AS SB ON SB.STORECARD_ID = SC.ID " +
		//					" WHERE 1 = 1 ";

		//		if (!string.IsNullOrEmpty(itemnmbr))
		//		{
		//			Command += " AND SC.ID = '" + itemnmbr.Trim() + "' ";
		//		}

		//		if (!string.IsNullOrEmpty(skl_id))
		//		{
		//			Command += " AND SSC.STORE_ID = '" + skl_id.Trim() + "' ";
		//		}

		//		Command += " ORDER BY SB.EXPIRATIONDATE$DATE ";
		//	}
		//	else
		//	{
		//		Command +=
		//			" SELECT " +

		//			" ROW_NUMBER() OVER(ORDER BY COALESCE(iif(SC.X_CZ_Expirace_Track > 0, SB.EXPIRATIONDATE$DATE, NULL), A.CREATEDAT$DATE)) AS IndexXX,SC.ID AS Itemnmbr, " +
		//			//" ROW_NUMBER() OVER(ORDER BY A.CREATEDAT$DATE) AS IndexXX," +

		//			" SC.ID AS Itemnmbr, " +
		//			" SC.NAME AS Itemdesc, " +
		//			" SSC.STORE_ID AS Skl_id, " +
		//			" NULL AS Locncode, " +
		//			" SSC.QUANTITY AS Qty, " +
		//			" SB.NAME AS Serltnum, " +
		//			" SB.EXPIRATIONDATE$DATE AS Expiration, " +
		//			" A.CREATEDAT$DATE AS Prijem " +
		//			" FROM StoreDocuments as A " +
		//			" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
		//			" JOIN Stores S ON S.ID = B.STORE_ID " +
		//			" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
		//			" JOIN STORESUBCARDS AS SSC ON SSC.STORECARD_ID = SC.ID " +
		//			" JOIN DOCROWBATCHES D ON D.PARENT_ID = B.ID " +
		//			" JOIN STOREBATCHES SB ON SB.ID = D.STOREBATCH_ID " +
		//			" WHERE A.DocumentType = '" + Constants.Common.PR_DocumentType + "' ";

		//		if (!string.IsNullOrEmpty(itemnmbr))
		//		{
		//			Command += " AND SC.ID = '" + itemnmbr.Trim() + "' ";
		//		}

		//		if (!string.IsNullOrEmpty(serltnum))
		//		{
		//			Command += " AND SB.NAME = '" + serltnum.Trim() + "' ";
		//		}

		//		if (!string.IsNullOrEmpty(skl_id))
		//		{
		//			Command += " AND SSC.STORE_ID = '" + skl_id.Trim() + "' ";
		//		}

		//		Command += " ORDER BY COALESCE(iif(SC.X_CZ_Expirace_Track > 0, SB.EXPIRATIONDATE$DATE, NULL ), A.CREATEDAT$DATE) ";
		//		//Command += " ORDER BY A.CREATEDAT$DATE ";

		//	}

		//	return Command;
		//}


		private static string GetScript_Vydej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum)
		{
			string Command = string.Empty;

			#region Puvodny
			//    Command +=
			//" SELECT " +
			//" ROW_NUMBER() OVER(ORDER BY COALESCE(iif(SC.X_CZ_Expirace_Track > 0, SB.EXPIRATIONDATE$DATE, NULL), A.CREATEDAT$DATE)) AS IndexXX, " +
			//" SC.ID AS Itemnmbr, " +
			//" SC.NAME AS Itemdesc, " +
			//" SSC.STORE_ID AS Skl_id, " +
			//" NULL AS Locncode, " +
			//" SSB.QUANTITY AS Qty, " +
			//" SB.NAME AS Serltnum, " +
			//" SB.EXPIRATIONDATE$DATE AS Expiration, " +
			//" A.CREATEDAT$DATE AS Prijem " +
			//" FROM StoreDocuments as A " +
			//" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
			//" JOIN Stores S ON S.ID = B.STORE_ID " +
			//" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
			//" JOIN STORESUBCARDS AS SSC ON SSC.STORECARD_ID = SC.ID " +
			//" JOIN DOCROWBATCHES D ON D.PARENT_ID = B.ID " +
			//" JOIN STOREBATCHES SB ON SB.ID = D.STOREBATCH_ID " +
			//" JOIN STORESUBBATCHES SSB ON SSB.STOREBATCH_ID = SB.ID " +
			//" WHERE A.DocumentType = '" + Constants.Common.PR_DocumentType + "' ";

			//    Command += "AND SSB.QUANTITY > 0"; // 24.3.2021 požadaven pro FIFO/FEFO pouze na kladný stav

			//    if (!string.IsNullOrEmpty(itemnmbr))
			//    {
			//        Command += " AND SC.ID = '" + itemnmbr.Trim() + "' ";
			//    }

			//    if (!string.IsNullOrEmpty(serltnum))
			//    {
			//        Command += " AND SB.NAME = '" + serltnum.Trim() + "' ";
			//    }

			//    if (!string.IsNullOrEmpty(skl_id))
			//    {
			//        Command += " AND SSC.STORE_ID = '" + skl_id.Trim() + "' ";
			//    }

			//    Command += " ORDER BY COALESCE(iif(SC.X_CZ_Expirace_Track > 0, SB.EXPIRATIONDATE$DATE, NULL ), A.CREATEDAT$DATE) ";
			//    //Command += " ORDER BY A.CREATEDAT$DATE ";

			#endregion

			Command +=
					" SELECT x.* FROM ( " +
					" SELECT " +
					" ROW_NUMBER() OVER(ORDER BY COALESCE(iif(SC.X_CZ_Expirace_Track > 0, SB.EXPIRATIONDATE$DATE, NULL), MIN(A.CREATEDAT$DATE))) AS IndexXX, " +
					" SC.ID AS Itemnmbr, " +
					" SC.NAME AS Itemdesc, " +
					" SSC.STORE_ID AS Skl_id, " +
					" NULL AS Locncode, " +
					" SSB.QUANTITY AS Qty, " +
					" SB.NAME AS Serltnum, " +
					" SB.EXPIRATIONDATE$DATE AS Expiration, " +
					" SC.X_CZ_Expirace_Track AS ExpTrack, " +
					" MIN(A.CREATEDAT$DATE) AS Prijem " +
					" FROM StoreDocuments as A " +
					" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
					" JOIN Stores S ON S.ID = B.STORE_ID " +
					" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
					" JOIN STORESUBCARDS AS SSC ON SSC.STORECARD_ID = SC.ID " +
					" JOIN DOCROWBATCHES D ON D.PARENT_ID = B.ID " +
					" JOIN STOREBATCHES SB ON SB.ID = D.STOREBATCH_ID " +
					" JOIN STORESUBBATCHES SSB ON SSB.STOREBATCH_ID = SB.ID AND SSB.STORE_ID = SSC.STORE_ID " +
					" WHERE 1 = 1 ";


			//TOHLE je otazka zda je sparve... určite tady ešte neco chyby... 
			// Prijem a Inventarni manko asi nebude jediny doklad

			//TaD 16.4.2021 Dle vyjadrení pana Hudce z IS ABRA by typ dokladu nemnel byt relevantní, vždy beru kladný stav a nejstarší datum MIN(A.CREATEDAT$DATE)
			//Command +=	" AND " +
			//			" ( " +
			//			" A.DocumentType = '" + Constants.Common.PR_DocumentType + "' " +
			//			" OR " +
			//			" A.DocumentType = '" + Constants.Common.INM_DocumentType + "' " +
			//			" ) ";

			Command += "AND SSB.QUANTITY > 0"; // 24.3.2021 požadaven pro FIFO/FEFO pouze na kladný stav

			if (!string.IsNullOrEmpty(itemnmbr))
			{
				Command += " AND SC.ID = '" + itemnmbr.Trim() + "' ";
			}

			if (!string.IsNullOrEmpty(serltnum))
			{
				Command += " AND SB.NAME = '" + serltnum.Trim() + "' ";
			}

			if (!string.IsNullOrEmpty(skl_id))
			{
				Command += " AND SSC.STORE_ID = '" + skl_id.Trim() + "' ";
			}

		   Command += " GROUP BY SC.ID , SC.NAME,SB.EXPIRATIONDATE$DATE, SC.X_CZ_Expirace_Track, SSC.STORE_ID, SSB.QUANTITY, SB.NAME ";
			Command += " ) x ";
		   Command += " ORDER BY COALESCE(iif(x.ExpTrack > 0, Expiration, NULL ), Prijem) ";

			return Command;
		}


        #endregion

        #region Prevod Vydej

        /// <summary>
        /// Metoda která kontroluje existenci Prevod Vydej
        /// </summary>
        /// <param name="SOPNUMBE"></param>
        /// <returns></returns>
        internal static bool PrevodVydej_Exists(string SOPNUMBE)
        {
            FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
            try
            {

                string sel =
                    " SELECT " +
                    " (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as SOPNUMBE " +
                    " FROM   StoreDocuments as A " +
                    " JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
                    " JOIN Periods P ON P.ID=A.Period_ID " +
                    " JOIN PMSTATES PM ON PM.ID = A.PMSTATE_ID " +
                    " WHERE  A.DocumentType='" + Constants.Common.PRV_DocumentType + "' " +
                    " AND A.finished = 'N' " +
                    " AND PM.ID ='" + Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_PrevodVydej_VPriprave.Trim() + "'" +
                    " AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE + "'";


                FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
                    sel,
                    FBConnection
                    );

                FBCommand.Connection.Open();
                object o = FBCommand.ExecuteScalar();
                if ((o == null) || (o is DBNull) || (o == DBNull.Value))
                    return false; // neexistuje PrevodkaVydej
                else
                    return true; // existuje Prevodka Vydej

            }
            finally
            {
                if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    FBConnection.Close();
            }
        }

		/// <summary>
		/// Metoda která kontroluje existenci Prevod Vydej
		/// </summary>
		/// <param name="SOPNUMBE"></param>
		/// <returns></returns>
		internal static bool PrevodVydej_Exists_Process(string SOPNUMBE)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel =
					" SELECT " +
					" (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as SOPNUMBE " +
					" FROM   StoreDocuments as A " +
					" JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
					" JOIN Periods P ON P.ID=A.Period_ID " +
					" JOIN PMSTATES PM ON PM.ID = A.PMSTATE_ID " +
					" WHERE  A.DocumentType='" + Constants.Common.PRV_DocumentType + "' " +
					" AND A.finished = 'N' " +
					" AND PM.ID ='" + Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_PrevodVydej_VTerminalu.Trim() + "'" +
					" AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE + "'";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return false; // neexistuje PrevodkaVydej
				else
					return true; // existuje Prevodka Vydej

			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}


		/// <summary>
		/// Metoda která vrací data do predlohy z DL ve stavu k Výdeji
		/// </summary>
		/// <param name="SOPNUMBE"></param>
		/// <param name="SKL_ID"></param>
		/// <returns></returns>
		internal static ABRA_Datasets.Vydej.CZMST_SEDataTable PrevodkaVydej_GetData(string SOPNUMBE, string SKL_ID)
		{
			Globals_V1.LoadConfiguration();

			ABRA_Datasets.Vydej.CZMST_SEDataTable dataTable = new Fask.Module.ABRA.SAB.ABRA_Datasets.Vydej.CZMST_SEDataTable();
			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText =
												" SELECT " +
												" SC.CODE as ITEMCODE, " +
												" SC.NAME as ITEMDESC, " +
												//" B.ID as ITEMNMBR, " +
												" B.STORECARD_ID as ITEMNMBR, " +
												" B.ROWTYPE as ITEMTYPE, " +
												" B.POSINDEX as ORD, " +
												" (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as SOPNUMBE, " +
												" B.STORE_ID as SKL_ID, " +
												" SEAN.EAN as VNDITNUM, " +
												" B.QUANTITY as  QTYSHPPD, " +
												" SU.CODE as MJ, " +
												//" SU.UNITRATE AS QTYPACK ";
												" CASE " +
												"   WHEN SU.UNITRATE = 1 AND SU.CODE = SC.MAINUNITCODE AND SEAN.EAN = SC.EAN THEN 0 " +
												"   ELSE SU.UNITRATE " +
												" END AS QTYPACK ";


					if (Globals_V1.Konfigurace.ExportZasoby[0].SerNumTrack_Vsude)
					{
						da.SelectCommand.CommandText += ", SC.Category as CZ_SerNum_Track ";
						//da.SelectCommand.CommandText += ", 1 as CZ_Expirace_Track ";
						da.SelectCommand.CommandText += ", SC.X_CZ_Expirace_Track as CZ_Expirace_Track ";
					}
					else
					{
						da.SelectCommand.CommandText += ", 0 as CZ_SerNum_Track ";
						da.SelectCommand.CommandText += ", 0 as CZ_Expirace_Track ";
					}

					da.SelectCommand.CommandText += " FROM   StoreDocuments as A  " +
												" JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
												" JOIN Periods P ON P.ID=A.Period_ID " +
												" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
												" JOIN Stores S ON S.ID = B.STORE_ID " +
												" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
												" JOIN STOREUNITS SU ON SU.PARENT_ID = SC.ID " +
												" JOIN STOREEANS SEAN ON SEAN.PARENT_ID = SU.ID " +
												" JOIN PMSTATES PM ON PM.ID = A.PMSTATE_ID " +
												" WHERE A.DocumentType='" + Constants.Common.PRV_DocumentType + "' " +
												" AND A.FINISHED = 'N' " +
												" AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) ='" + SOPNUMBE + "'" +
												" AND B.STORE_ID ='" + SKL_ID + "'" +
												" AND PM.ID ='" + Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_PrevodVydej_VPriprave.Trim() + "'" +
												" AND ( SC.Category >= 0 AND SC.Category < 3 ) " +
												" ORDER BY   B.PosIndex ";


					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
					da.SelectCommand.Connection.Open();
					da.Fill(dataTable);
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " PrevodkaVydej_GetData", ex);
					Logging.ExceptionHandler2.Handle(dataTable);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}
			return dataTable;

		}


		/// <summary>
		/// Metoda která vrací ID Dodacího listu 
		/// </summary>
		/// <param name="SOPNUMBE"></param>
		/// <param name="SKL_ID"></param>
		/// <returns></returns>
		internal static string Get_ID_PrevodkaVydej(string SOPNUMBE, string SKL_ID)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT " +
								" A.ID " +
								" FROM StoreDocuments as A " +
								" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
								" JOIN Periods P ON P.ID = A.Period_ID " +
								" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
								" JOIN Stores S ON S.ID = B.STORE_ID " +
								" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
								" WHERE A.DocumentType = '" + Constants.Common.PRV_DocumentType + "' " +
								" AND A.finished = 'N' " +
								" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE + "' " +
								" AND B.STORE_ID = '" + SKL_ID + "' " +
								" GROUP BY A.ID ";


				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID PrevodkaVydej
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		internal static string Get_ID_Polozka_PrevodkyVydej(Polozky_Vydej radek)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT " +
								" B.ID " +
								" FROM   StoreDocuments as A " +
								" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
								" JOIN Periods P ON P.ID = A.Period_ID " +
								" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
								" JOIN Stores S ON S.ID = B.STORE_ID " +
								" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
								" WHERE A.DocumentType = '" + Constants.Common.PRV_DocumentType + "' " +
								" AND A.finished = 'N' " +
								" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + radek.SOPNUMBE.Trim() + "' " +
								" AND B.STORE_ID = '" + radek.SKL_ID.Trim() + "' " +
								" AND B.STORECARD_ID = '" + radek.ITEMNMBR.Trim() + "' ";

				if (radek.ORD.HasValue)
				{
					sel += " AND B.POSINDEX = '" + radek.ORD.Value.ToString() + "' ";
				}

				sel += " ORDER BY B.PosIndex ";

				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID Skladove karty
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		internal static string Get_ID_Polozka_PrevodkyVydej_prelokovani(DataSets.Vydej.CZMST_SIRow radek)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = " SELECT " +
							" B.ID " +
							" FROM   StoreDocuments as A " +
							" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
							" JOIN Periods P ON P.ID = A.Period_ID " +
							" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
							" JOIN Stores S ON S.ID = B.STORE_ID " +
							" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
							" WHERE A.DocumentType = '" + Constants.Common.PRV_DocumentType + "' " +
							" AND A.finished = 'N' " +
							" AND B.STORE_ID = '" + radek.SKL_ID.Trim() + "' " +
							" AND B.STORECARD_ID = '" + radek.ITEMNMBR.Trim() + "' ";

				//string sel = " SELECT " +
				//				" B.ID " +
				//				" FROM   StoreDocuments as A " +
				//				" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
				//				" JOIN Periods P ON P.ID = A.Period_ID " +
				//				" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
				//				" JOIN Stores S ON S.ID = B.STORE_ID " +
				//				" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
				//				" WHERE A.DocumentType = '" + Constants.Common.PRV_DocumentType + "' " +
				//				" AND A.finished = 'N' " +
				//				" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + radek.SOPNUMBE.Trim() + "' " +
				//				" AND B.STORE_ID = '" + radek.SKL_ID.Trim() + "' " +
				//				" AND B.STORECARD_ID = '" + radek.ITEMNMBR.Trim() + "' ";

				//if (radek.ORD.HasValue)
				//{
				//	sel += " AND B.POSINDEX = '" + radek.ORD.Value.ToString() + "' ";
				//}

				sel += " ORDER BY B.PosIndex ";

				FirebirdSql.Data.FirebirdClient.FbCommand FBCommand = new FirebirdSql.Data.FirebirdClient.FbCommand(
					sel,
					FBConnection
					);

				FBCommand.Connection.Open();
				object o = FBCommand.ExecuteScalar();
				if ((o == null) || (o is DBNull) || (o == DBNull.Value))
					return null; // nenalezen
				else
					return (string)o; // ID Skladove karty
			}
			finally
			{
				if ((FBConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					FBConnection.Close();
			}
		}

		/// <summary>
		/// Metoda která vrací ID Objednavky přijate, z kterej je Dodaci List
		/// </summary>
		/// <param name="polozky"></param>
		/// <returns></returns>
		internal static string Get_ID_PRV(string SOPNUMBE)
		{
			try
			{

				string sel =
					" SELECT " +
					" distinct B.PROVIDE_ID " +
					" FROM StoreDocuments as A " +
					" JOIN DocQueues DQ ON DQ.ID = A.DocQueue_ID " +
					" JOIN Periods P ON P.ID = A.Period_ID " +
					" JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
					" JOIN Stores S ON S.ID = B.STORE_ID " +
					" JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
					" WHERE A.DocumentType = '" + Constants.Common.PRV_DocumentType + "' " +
					" AND A.finished = 'N' " +
					" AND(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE.Trim() + "' ";

				using (var FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
				{
					using (var FBCommand = FBConnection.CreateCommand())
					{
						FBCommand.CommandType = System.Data.CommandType.Text;
						FBCommand.CommandText = sel;

						FBCommand.Connection.Open();
						object o = FBCommand.ExecuteScalar();
						if ((o == null) || (o is DBNull) || (o == DBNull.Value))
							return null; // nenalezen
						else
							return (string)o; // ID OP
					}

				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return null;

			}
		}

        internal static string Get_StoreID_By_IDPRV(string iD_PRV)
        {
            try
            {

				string sel =
					" SELECT " +
					" X_TARGETSTORE_ID " +
					" FROM StoreDocuments " +
					" WHERE ID = '"  + iD_PRV.Trim() + "'";

				using (var FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
				{
					using (var FBCommand = FBConnection.CreateCommand())
					{
						FBCommand.CommandType = System.Data.CommandType.Text;
						FBCommand.CommandText = sel;

						FBCommand.Connection.Open();
						object o = FBCommand.ExecuteScalar();
						if ((o == null) || (o is DBNull) || (o == DBNull.Value))
							return null; // nenalezen
						else
							return (string)o;
					}

				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return null;

			}

		}

		internal static string Get_DocQueueID_PRP_By_IDPRV(string iD_PRV)
		{
			try
			{

				string sel =
					" SELECT " +
					" X_DOCQUEUES_PRP_ID" +
					" FROM StoreDocuments " +
					" WHERE ID = '" + iD_PRV.Trim() + "'";

				using (var FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
				{
					using (var FBCommand = FBConnection.CreateCommand())
					{
						FBCommand.CommandType = System.Data.CommandType.Text;
						FBCommand.CommandText = sel;

						FBCommand.Connection.Open();
						object o = FBCommand.ExecuteScalar();
						if ((o == null) || (o is DBNull) || (o == DBNull.Value))
							return null; // nenalezen
						else
							return (string)o;
					}

				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return null;

			}

		}

		#endregion


		#region prelokovani

		/// <summary>
		/// Metoda která vrací data do predlohy z lokace rampa
		/// </summary>
		/// <param name="SOPNUMBE"></param>
		/// <param name="SKL_ID"></param>
		/// <returns></returns>
		internal static Fask.Module.ABRA.SAB.ABRA_Datasets.Lokace.CZMST_SEDataTable Prelokovani_GetData_Vydej(string SOPNUMBE, string SKL_ID)
		{
			Globals_V1.LoadConfiguration();

			//var table = new Fask.Module.ABRA.SAB.ABRA_Datasets.Lokace.Polozky_Na_Lokaci_Rampa_ABRADataTable();

			var table = new Fask.Module.ABRA.SAB.ABRA_Datasets.Lokace.CZMST_SEDataTable();
			string baseSql;

			//if (Globals_V1.Konfigurace.ExportZasoby[0].SerNumTrack_Vsude)
			//{
			//	da.SelectCommand.CommandText += ", SC.Category as CZ_SerNum_Track ";
			//	//da.SelectCommand.CommandText += ", 1 as CZ_Expirace_Track ";
			//	da.SelectCommand.CommandText += ", SC.X_CZ_Expirace_Track as CZ_Expirace_Track ";
			//}
			//else
			//{
			//	da.SelectCommand.CommandText += ", 0 as CZ_SerNum_Track ";
			//	da.SelectCommand.CommandText += ", 0 as CZ_Expirace_Track ";
			//}

			if (!string.IsNullOrEmpty(SKL_ID))
			{


                #region old
                //				baseSql = @"
                //SELECT * FROM LogStoreContents A
                // JOIN LogStorePositions LSP ON LSP.ID = A.Parent_ID
                // WHERE (LSP.Code = 'RAMPA') AND (LSP.Store_ID = @Store_ID ) ;";

                //				baseSql = @"
                //SELECT
                //    /* int */
                //    0 AS CountEntries,

                //    /* string */
                //    '' AS SOPNUMBE,

                //    /* string (nullable v XSD) */
                //    LSC.STORECARD_ID AS ITEMNMBR,

                //    /* string */
                //    'O' AS ITEMTYPE,

                //    /* string (nullable) */
                //    SC.NAME AS ITEMDESC,

                //    /* string (nullable) */
                //    '' AS VNDDOCNM,

                //    /* string (nullable) */
                //    SC.EAN AS VNDITNUM,

                //    /* int */
                //    0 AS ORD,

                //    /* string */
                //    '' AS CZ_CarKod,

                //    /* string (nullable) */
                //    '' AS LOCNCODE,

                //    /* decimal */
                //    LSC.QUANTITY AS QTYSHPPD,

                //    /* decimal */
                //    CAST(0 AS DECIMAL(18,4)) AS QTYPACK,

                //    /* unsignedByte -> smallint/tinyint-safe */
                //    CAST(0 AS SMALLINT) AS CZ_DatVyr_Track,

                //    /* short */
                //    CAST(0 AS SMALLINT) AS CZ_DatVyr_Delka,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_SerNum_Track,

                //    /* short */
                //    CAST(0 AS SMALLINT) AS CZ_SerNum_Delka,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_SW_Track,

                //    /* short */
                //    CAST(0 AS SMALLINT) AS CZ_SW_Delka,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_Doslo,

                //    /* string (nullable) */
                //    '' AS Note,

                //    /* string (nullable) */
                //    '' AS TYPEPAL,

                //    /* decimal (nullable v XSD; může být i NULL) */
                //    CAST(0 AS DECIMAL(18,4)) AS QTYPAL,

                //    /* unsignedByte */
                //    CAST(3 AS SMALLINT) AS PRIORITY,

                //    /* unsignedByte (nullable v XSD; default 0) */
                //    CAST(0 AS SMALLINT) AS PRINTED,

                //    /* int (v DataSetu auto-increment se seedem -1) */
                //    -1 AS DEX_ROW_ID,

                //    /* string (nullable) */
                //    LSP.STORE_ID AS SKL_ID,

                //    /* string (není nullable) */
                //    LSC.QUNIT AS MJ,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_REZ1_Track,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_REZ2_Track,

                //    /* string (nullable, maxlen 50 v XSD) */
                //    '' AS ITEMCODE,

                //    /* decimal (nullable; zde 0) */
                //    CAST(0 AS DECIMAL(18,4)) AS WEIGHT,

                //    /* int (nullable) */
                //    NULL AS USERID,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_Expirace_Track
                //  FROM LogStoreContents LSC
                //  JOIN LogStorePositions LSP ON LSP.ID = LSC.Parent_ID
                //  LEFT JOIN StoreCards SC ON SC.ID = LSC.StoreCard_ID
                //  WHERE (LSP.Code = 'RAMPA') AND (LSP.Store_ID = @Store_ID ) ;"; 
                #endregion

                baseSql = @"
SELECT
    0 AS CountEntries,
    '' AS SOPNUMBE,
    LSC.STORECARD_ID AS ITEMNMBR,
    'O' AS ITEMTYPE,
    SC.NAME AS ITEMDESC,
    '' AS VNDDOCNM,
    SC.EAN AS VNDITNUM,
    0 AS ORD,
    '' AS CZ_CarKod,
    '' AS LOCNCODE,
    LSC.QUANTITY AS QTYSHPPD,
    CAST(0 AS NUMERIC(18,4)) AS QTYPACK,
    CAST(0 AS SMALLINT) AS CZ_DatVyr_Track,
    CAST(0 AS SMALLINT) AS CZ_DatVyr_Delka,
    CAST(0 AS SMALLINT) AS CZ_SerNum_Delka,
    SC.Category as CZ_SerNum_Track,
    SC.X_CZ_Expirace_Track as CZ_Expirace_Track,
    CAST(0 AS SMALLINT) AS CZ_SW_Track,
    CAST(0 AS SMALLINT) AS CZ_SW_Delka,
    CAST(0 AS SMALLINT) AS CZ_Doslo,
    '' AS Note,
    '' AS TYPEPAL,
    CAST(0 AS NUMERIC(18,4)) AS QTYPAL,
    CAST(3 AS SMALLINT) AS PRIORITY,
	CAST(0 AS SMALLINT) AS PRINTED,
	LSP.STORE_ID AS SKL_ID,
    LSC.QUNIT AS MJ,
    CAST(0 AS SMALLINT) AS CZ_REZ1_Track,
	CAST(0 AS SMALLINT) AS CZ_REZ2_Track,
	'' AS ITEMCODE,
	CAST(0 AS NUMERIC(18, 4)) AS WEIGHT,
	 NULL AS USERID
 FROM LogStoreContents LSC
 JOIN LogStorePositions LSP ON LSP.ID = LSC.Parent_ID
 LEFT JOIN StoreCards SC ON SC.ID = LSC.StoreCard_ID
 WHERE (LSP.Code = 'RAMPA') AND (LSP.Store_ID = @Store_ID );";

			}
			else
			{

                #region old

                //				baseSql = @"
                //SELECT * FROM LogStoreContents A
                // JOIN LogStorePositions LSP ON LSP.ID = A.Parent_ID
                // WHERE (LSP.Code = 'RAMPA');";

                //				baseSql = @"
                //SELECT
                //    /* int */
                //    0 AS CountEntries,

                //    /* string */
                //    '' AS SOPNUMBE,

                //    /* string (nullable v XSD) */
                //    LSC.STORECARD_ID AS ITEMNMBR,

                //    /* string */
                //    'O' AS ITEMTYPE,

                //    /* string (nullable) */
                //    SC.NAME AS ITEMDESC,

                //    /* string (nullable) */
                //    '' AS VNDDOCNM,

                //    /* string (nullable) */
                //    SC.EAN AS VNDITNUM,

                //    /* int */
                //    0 AS ORD,

                //    /* string */
                //    '' AS CZ_CarKod,

                //    /* string (nullable) */
                //    '' AS LOCNCODE,

                //    /* decimal */
                //    LSC.QUANTITY AS QTYSHPPD,

                //    /* decimal */
                //    CAST(0 AS DECIMAL(18,4)) AS QTYPACK,

                //    /* unsignedByte -> smallint/tinyint-safe */
                //    CAST(0 AS SMALLINT) AS CZ_DatVyr_Track,

                //    /* short */
                //    CAST(0 AS SMALLINT) AS CZ_DatVyr_Delka,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_SerNum_Track,

                //    /* short */
                //    CAST(0 AS SMALLINT) AS CZ_SerNum_Delka,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_SW_Track,

                //    /* short */
                //    CAST(0 AS SMALLINT) AS CZ_SW_Delka,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_Doslo,

                //    /* string (nullable) */
                //    '' AS Note,

                //    /* string (nullable) */
                //    '' AS TYPEPAL,

                //    /* decimal (nullable v XSD; může být i NULL) */
                //    CAST(0 AS DECIMAL(18,4)) AS QTYPAL,

                //    /* unsignedByte */
                //    CAST(3 AS SMALLINT) AS PRIORITY,

                //    /* unsignedByte (nullable v XSD; default 0) */
                //    CAST(0 AS SMALLINT) AS PRINTED,

                //    /* int (v DataSetu auto-increment se seedem -1) */
                //    -1 AS DEX_ROW_ID,

                //    /* string (nullable) */
                //    LSP.STORE_ID AS SKL_ID,

                //    /* string (není nullable) */
                //    LSC.QUNIT AS MJ,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_REZ1_Track,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_REZ2_Track,

                //    /* string (nullable, maxlen 50 v XSD) */
                //    '' AS ITEMCODE,

                //    /* decimal (nullable; zde 0) */
                //    CAST(0 AS DECIMAL(18,4)) AS WEIGHT,

                //    /* int (nullable) */
                //    NULL AS USERID,

                //    /* unsignedByte */
                //    CAST(0 AS SMALLINT) AS CZ_Expirace_Track
                //  FROM LogStoreContents LSC
                //  JOIN LogStorePositions LSP ON LSP.ID = LSC.Parent_ID
                //  LEFT JOIN StoreCards SC ON SC.ID = LSC.StoreCard_ID
                //  WHERE (LSP.Code = 'RAMPA');"; 
                #endregion

                baseSql = @"
SELECT
    0 AS CountEntries,
    '' AS SOPNUMBE,
    LSC.STORECARD_ID AS ITEMNMBR,
    'O' AS ITEMTYPE,
    SC.NAME AS ITEMDESC,
    '' AS VNDDOCNM,
    SC.EAN AS VNDITNUM,
    0 AS ORD,
    '' AS CZ_CarKod,
    '' AS LOCNCODE,
    LSC.QUANTITY AS QTYSHPPD,
    CAST(0 AS NUMERIC(18,4)) AS QTYPACK,
    CAST(0 AS SMALLINT) AS CZ_DatVyr_Track,
    CAST(0 AS SMALLINT) AS CZ_DatVyr_Delka,
    CAST(0 AS SMALLINT) AS CZ_SerNum_Delka,
    SC.Category as CZ_SerNum_Track,
    SC.X_CZ_Expirace_Track as CZ_Expirace_Track,
    CAST(0 AS SMALLINT) AS CZ_SW_Track,
    CAST(0 AS SMALLINT) AS CZ_SW_Delka,
    CAST(0 AS SMALLINT) AS CZ_Doslo,
    '' AS Note,
    '' AS TYPEPAL,
    CAST(0 AS NUMERIC(18,4)) AS QTYPAL,
    CAST(3 AS SMALLINT) AS PRIORITY,
	CAST(0 AS SMALLINT) AS PRINTED,
	LSP.STORE_ID AS SKL_ID,
    LSC.QUNIT AS MJ,
    CAST(0 AS SMALLINT) AS CZ_REZ1_Track,
	CAST(0 AS SMALLINT) AS CZ_REZ2_Track,
	'' AS ITEMCODE,
	CAST(0 AS NUMERIC(18, 4)) AS WEIGHT,
	 NULL AS USERID
 FROM LogStoreContents LSC
 JOIN LogStorePositions LSP ON LSP.ID = LSC.Parent_ID
 LEFT JOIN StoreCards SC ON SC.ID = LSC.StoreCard_ID
 WHERE LSP.Code = 'RAMPA';";

			}

			try
			{
				using (var conn = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
				using (var cmd = new FirebirdSql.Data.FirebirdClient.FbCommand(baseSql, conn))
				using (var da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter(cmd))
				{
					cmd.CommandType = System.Data.CommandType.Text;

					if (!string.IsNullOrEmpty(SKL_ID))
					{
						// Zvolte typ dle DB schématu (VarChar/Integer/BigInt). Zde ponecháno jako VarChar.
						cmd.Parameters.Add(new FirebirdSql.Data.FirebirdClient.FbParameter("@Store_ID", FirebirdSql.Data.FirebirdClient.FbDbType.VarChar)
						{
							Value = SKL_ID
						});
					}

					conn.Open();
					da.Fill(table);
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", "Prelokovani_GetData", ex);
				Logging.ExceptionHandler2.Handle(table);
				throw ex;
			}

			return table;

		}

		/// <summary>
		/// Metoda která vrací data do predlohy z lokace rampa
		/// </summary>
		/// <param name="SOPNUMBE"></param>
		/// <param name="SKL_ID"></param>
		/// <returns></returns>
		internal static Fask.Module.ABRA.SAB.ABRA_Datasets.Lokace.CZMST_SEDataTable Prelokovani_GetData_Ostatni(string SOPNUMBE, string SKL_ID)
		{
			Globals_V1.LoadConfiguration();

			//var table = new Fask.Module.ABRA.SAB.ABRA_Datasets.Lokace.Polozky_Na_Lokaci_Rampa_ABRADataTable();

			var table = new Fask.Module.ABRA.SAB.ABRA_Datasets.Lokace.CZMST_SEDataTable();
			string baseSql;

			//if (Globals_V1.Konfigurace.ExportZasoby[0].SerNumTrack_Vsude)
			//{
			//	da.SelectCommand.CommandText += ", SC.Category as CZ_SerNum_Track ";
			//	//da.SelectCommand.CommandText += ", 1 as CZ_Expirace_Track ";
			//	da.SelectCommand.CommandText += ", SC.X_CZ_Expirace_Track as CZ_Expirace_Track ";
			//}
			//else
			//{
			//	da.SelectCommand.CommandText += ", 0 as CZ_SerNum_Track ";
			//	da.SelectCommand.CommandText += ", 0 as CZ_Expirace_Track ";
			//}

			if (!string.IsNullOrEmpty(SKL_ID))
			{


			

				baseSql = @"
SELECT
    0 AS CountEntries,
    '' AS SOPNUMBE,
    LSC.STORECARD_ID AS ITEMNMBR,
    'O' AS ITEMTYPE,
    SC.NAME AS ITEMDESC,
    '' AS VNDDOCNM,
    SC.EAN AS VNDITNUM,
    0 AS ORD,
    '' AS CZ_CarKod,
    '' AS LOCNCODE,
    LSC.QUANTITY AS QTYSHPPD,
    CAST(0 AS NUMERIC(18,4)) AS QTYPACK,
    CAST(0 AS SMALLINT) AS CZ_DatVyr_Track,
    CAST(0 AS SMALLINT) AS CZ_DatVyr_Delka,
    CAST(0 AS SMALLINT) AS CZ_SerNum_Delka,
    SC.Category as CZ_SerNum_Track,
    SC.X_CZ_Expirace_Track as CZ_Expirace_Track,
    CAST(0 AS SMALLINT) AS CZ_SW_Track,
    CAST(0 AS SMALLINT) AS CZ_SW_Delka,
    CAST(0 AS SMALLINT) AS CZ_Doslo,
    '' AS Note,
    '' AS TYPEPAL,
    CAST(0 AS NUMERIC(18,4)) AS QTYPAL,
    CAST(3 AS SMALLINT) AS PRIORITY,
	CAST(0 AS SMALLINT) AS PRINTED,
	LSP.STORE_ID AS SKL_ID,
    LSC.QUNIT AS MJ,
    CAST(0 AS SMALLINT) AS CZ_REZ1_Track,
	CAST(0 AS SMALLINT) AS CZ_REZ2_Track,
	'' AS ITEMCODE,
	CAST(0 AS NUMERIC(18, 4)) AS WEIGHT,
	 NULL AS USERID
 FROM LogStoreContents LSC
 JOIN LogStorePositions LSP ON LSP.ID = LSC.Parent_ID
 LEFT JOIN StoreCards SC ON SC.ID = LSC.StoreCard_ID
 WHERE (LSP.Code = 'RAMPA') AND (LSP.Store_ID = @Store_ID );";

			}
			else
			{

			

				baseSql = @"
SELECT
    0 AS CountEntries,
    '' AS SOPNUMBE,
    LSC.STORECARD_ID AS ITEMNMBR,
    'O' AS ITEMTYPE,
    SC.NAME AS ITEMDESC,
    '' AS VNDDOCNM,
    SC.EAN AS VNDITNUM,
    0 AS ORD,
    '' AS CZ_CarKod,
    '' AS LOCNCODE,
    LSC.QUANTITY AS QTYSHPPD,
    CAST(0 AS NUMERIC(18,4)) AS QTYPACK,
    CAST(0 AS SMALLINT) AS CZ_DatVyr_Track,
    CAST(0 AS SMALLINT) AS CZ_DatVyr_Delka,
    CAST(0 AS SMALLINT) AS CZ_SerNum_Delka,
    SC.Category as CZ_SerNum_Track,
    SC.X_CZ_Expirace_Track as CZ_Expirace_Track,
    CAST(0 AS SMALLINT) AS CZ_SW_Track,
    CAST(0 AS SMALLINT) AS CZ_SW_Delka,
    CAST(0 AS SMALLINT) AS CZ_Doslo,
    '' AS Note,
    '' AS TYPEPAL,
    CAST(0 AS NUMERIC(18,4)) AS QTYPAL,
    CAST(3 AS SMALLINT) AS PRIORITY,
	CAST(0 AS SMALLINT) AS PRINTED,
	LSP.STORE_ID AS SKL_ID,
    LSC.QUNIT AS MJ,
    CAST(0 AS SMALLINT) AS CZ_REZ1_Track,
	CAST(0 AS SMALLINT) AS CZ_REZ2_Track,
	'' AS ITEMCODE,
	CAST(0 AS NUMERIC(18, 4)) AS WEIGHT,
	 NULL AS USERID
 FROM LogStoreContents LSC
 JOIN LogStorePositions LSP ON LSP.ID = LSC.Parent_ID
 LEFT JOIN StoreCards SC ON SC.ID = LSC.StoreCard_ID
 WHERE LSP.Code = 'RAMPA';";

			}

			try
			{
				using (var conn = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
				using (var cmd = new FirebirdSql.Data.FirebirdClient.FbCommand(baseSql, conn))
				using (var da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter(cmd))
				{
					cmd.CommandType = System.Data.CommandType.Text;

					if (!string.IsNullOrEmpty(SKL_ID))
					{
						// Zvolte typ dle DB schématu (VarChar/Integer/BigInt). Zde ponecháno jako VarChar.
						cmd.Parameters.Add(new FirebirdSql.Data.FirebirdClient.FbParameter("@Store_ID", FirebirdSql.Data.FirebirdClient.FbDbType.VarChar)
						{
							Value = SKL_ID
						});
					}

					conn.Open();
					da.Fill(table);
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", "Prelokovani_GetData_Ostatni", ex);
				Logging.ExceptionHandler2.Handle(table);
				throw ex;
			}

			return table;

		}



		#endregion

	}
}
