using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Logging;
using Fask.Module.ABRA.CarpServise.ABRA_Datasets;

namespace Fask.Module.ABRA.CarpServise.Database
{
	class ABRA
	{

		#region Inventura

		internal static ABRA_Datasets.Inventura.CZMST_I1HDataTable SeznamInventur()
		{
			ABRA_Datasets.Inventura.CZMST_I1HDataTable dataTable = new ABRA_Datasets.Inventura.CZMST_I1HDataTable();
			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText = "SELECT(DQ.Code || '-' || CAST(MP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as Description " +
					"FROM MainInvProtocols AS MP " +
					"JOIN DocQueues DQ ON DQ.ID = MP.DOCQUEUE_ID " +
					"JOIN Periods P ON P.ID = MP.Period_ID ";

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

		internal static ABRA_Datasets.Inventura.CZMST_I1DataTable PolozkyInventur()
		{
			ABRA_Datasets.Inventura.CZMST_I1DataTable dataTable = new ABRA_Datasets.Inventura.CZMST_I1DataTable();
			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText = "SELECT " +
					"(DQ.Code || '-' || CAST(MP.OrdNumber AS VARCHAR(10)) || '/' || P.Code) AS CountEntries, " +
					"A.ID as ITEMNMBR, " +
					"A.EAN as CZ_CarKod, " +
					"A.NAME as ITEMDESC, " +
					"SSC.STORE_ID as SKL_ID, " +
					"SSC.QUANTITY as QUANTITY, " +
					"A.CODE as ITEMCODE, " +
					"MPROW.QUNIT AS MJ " +
					"FROM StoreCards A " +
					"LEFT JOIN StoreSubCards SSC ON SSC.StoreCard_ID = A.ID " +
					"LEFT JOIN StoreCardCategories SCC ON SCC.ID = A.StoreCardCategory_ID " +
					"LEFT JOIN Firms F ON F.ID = A.Producer_ID  " +
					"JOIN Countries CT ON CT.ID = A.Country_ID " +
					"LEFT JOIN StoreMenu SMI ON SMI.ID = A.StoreMenuItem_ID " +
					"LEFT JOIN DealerDiscounts DD ON DD.ID = A.DealerDiscount_ID " +
					"LEFT JOIN QuantityDiscounts QD ON QD.ID = A.QuantityDiscount_ID " +
					"LEFT JOIN MAININVPROTOCOLROWS AS MPROW ON MPROW.StoreCard_ID = A.ID " +
					"LEFT JOIN MainInvProtocols AS MP ON MP.ID = MPROW.PARENT_ID " +
					"JOIN DocQueues DQ ON DQ.ID = MP.DOCQUEUE_ID " +
					"JOIN Periods P ON P.ID = MP.Period_ID " +
					"WHERE A.Hidden = 'N' " +
					"AND SSC.InventoryStatus = 1 ";


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

		#endregion

		#region Sklad

		public static SQL_Datasets.Ciselniky.CZMST093DataTable Sklad_GetData()
		{
			SQL_Datasets.Ciselniky.CZMST093DataTable dataTable = new Fask.Module.ABRA.CarpServise.SQL_Datasets.Ciselniky.CZMST093DataTable();
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

        #region Vydejka

        internal static bool Vydejka_Exists(string SOPNUMBE)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = "SELECT " +
				"(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as SOPNUMBE " +
				"FROM   StoreDocuments as A " +
				"JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
				"JOIN Periods P ON P.ID=A.Period_ID " +
				"WHERE  A.DocumentType='21' " +
				"AND A.finished = 'N' " +
				"AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE + "'";


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

		internal static ABRA_Datasets.Vydej.CZMST_SEDataTable Vydejka_GetData(string SOPNUMBE, string SKL_ID)
		{
			ABRA_Datasets.Vydej.CZMST_SEDataTable dataTable = new Fask.Module.ABRA.CarpServise.ABRA_Datasets.Vydej.CZMST_SEDataTable();
			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;


					da.SelectCommand.CommandText =
											"SELECT " +
											"SC.NAME as ITEMDESC, " +
											"B.ID as ITEMNMBR, " +
											"NULL as CZ_SerNum_Track, " +
											"B.ID as ORD, " +
											"B.QUANTITY as  QTYSHPPD, " +
											"B.STORE_ID as SKL_ID, " +
											"(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as SOPNUMBE, " +
											"SC.EAN as VNDITNUM, " +
											"B.QUNIT as MJ " +
											"FROM   StoreDocuments as A  " +
											"JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
											"JOIN Periods P ON P.ID=A.Period_ID " +
											"JOIN StoreDocuments2 B ON B.Parent_ID = A.ID " +
											"JOIN Stores S ON S.ID = B.STORE_ID " +
											"JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
											"WHERE  A.DocumentType='21' " +
											"AND A.finished = 'N' " +
											"AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) ='" + SOPNUMBE + "'" +
											"AND B.STORE_ID ='" + SKL_ID + "'" +
											"ORDER BY   B.PosIndex ";

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
		
		#endregion

		#region Objednavka Prijata

		internal static bool ObjednavkaPrijata_Exists(string SOPNUMBE)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = "SELECT " +
				"(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as SOPNUMBE " +
				"FROM   ReceivedOrders as A " +
				"JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
				"JOIN Periods P ON P.ID=A.Period_ID " +
				"WHERE A.CLOSED = 'N' " +
				"AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE + "'";


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
		

		internal static ABRA_Datasets.Vydej.CZMST_SEDataTable ObjednavkaPrijata_GetData(string SOPNUMBE, string SKL_ID)
		{
			ABRA_Datasets.Vydej.CZMST_SEDataTable dataTable = new Fask.Module.ABRA.CarpServise.ABRA_Datasets.Vydej.CZMST_SEDataTable();
			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText =
					"SELECT " +
					"SC.CODE as ITEMCODE, " +
					"SC.NAME as ITEMDESC, " +
					"B.ID as ITEMNMBR, " +
					"B.ROWTYPE as ITEMTYPE, " +
					"B.POSINDEX as ORD, " +
					"B.QUANTITY as  QTYSHPPD, " +
					"B.STORE_ID as SKL_ID, " +
					"(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as SOPNUMBE, " +
					"SC.EAN as VNDITNUM, " +
					"B.QUNIT as MJ " +
					"FROM   ReceivedOrders as A  " +
					"JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
					"JOIN Periods P ON P.ID=A.Period_ID " +
					"JOIN ReceivedOrders2 B ON B.Parent_ID = A.ID " +
					"JOIN Stores S ON S.ID = B.STORE_ID " +
					"JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
					"WHERE A.CLOSED = 'N' " +
					"AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE + "'" +
					"AND B.STORE_ID ='" + SKL_ID + "'" +
					"ORDER BY   B.PosIndex ";


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
		
		#endregion

		#region Objednavka Vydana

		internal static bool ObjednavkaVydana_Exists(string PONUMBER)
		{
			FirebirdSql.Data.FirebirdClient.FbConnection FBConnection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
			try
			{

				string sel = "SELECT " +
				"(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as PONUMBER " +
				"FROM   IssuedOrders as A " +
				"JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
				"JOIN Periods P ON P.ID=A.Period_ID " +
				"WHERE A.CLOSED = 'N' " +
				"AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + PONUMBER + "'";


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

		internal static ABRA_Datasets.Prijem.CZMST_PEDataTable ObjednavkaVydana_GetData(string PONUMBER, string SKL_ID)
		{
			ABRA_Datasets.Prijem.CZMST_PEDataTable dataTable = new Fask.Module.ABRA.CarpServise.ABRA_Datasets.Prijem.CZMST_PEDataTable();
			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText =
					"SELECT " +
					"SC.CODE as ITEMCODE, " +
					"SC.NAME as ITEMDESC, " +
					"B.ID as ITEMNMBR, " +
					//"B.ROWTYPE as ITEMTYPE, " +
					"B.POSINDEX as ORD, " +
					"B.QUANTITY as  QTYSHPPD, " +
					"B.STORE_ID as SKL_ID, " +
					"(DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) as PONUMBER, " +
					"SC.EAN as VNDITNUM, " +
					"B.QUNIT as MJ " +
					"FROM   IssuedOrders as A  " +
					"JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
					"JOIN Periods P ON P.ID=A.Period_ID " +
					"JOIN IssuedOrders2 B ON B.Parent_ID = A.ID " +
					"JOIN Stores S ON S.ID = B.STORE_ID " +
					"JOIN STORECARDS SC ON SC.ID = B.STORECARD_ID " +
					"WHERE A.CLOSED = 'N' " +
					"AND (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + PONUMBER + "'" +
					"AND B.STORE_ID ='" + SKL_ID + "'" +
					"ORDER BY   B.PosIndex ";


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

		#endregion

		internal static Fask.Server.Interfaces.DataSets.Obecne ObjednavkaVydana_GetDavkyByCarKody(int Terminal_ID, string SKL_ID, string ListCarKody)
		{
			throw new NotImplementedException();
		}

		#region Tisk


		internal static ABRA_Datasets.Tisk.DataTiskRow Tisk_GetData(string SOPNUMBE)
		{
			ABRA_Datasets.Tisk.DataTiskDataTable dataTable = new ABRA_Datasets.Tisk.DataTiskDataTable();
			ABRA_Datasets.Tisk.DataTiskRow Row = null;
			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText =
					"SELECT " +
					"F.NAME, " +
					"ADR.CITY, " +
					"ADR.STREET, " +
					"ADR.COUNTRY, " +
					"ADR.POSTCODE " +
					"FROM   ReceivedOrders  as A " +
					"JOIN DocQueues DQ ON DQ.ID=A.DocQueue_ID " +
					"JOIN Periods P ON P.ID=A.Period_ID " +
					"JOIN Firms F ON F.ID=A.Firm_ID " +
					"JOIN ADDRESSES ADR ON ADR.ID = F.RESIDENCEADDRESS_ID " +
					"WHERE (DQ.Code || '-' || CAST(A.OrdNumber AS VARCHAR(10)) || '/' || P.Code) = '" + SOPNUMBE + "' ";

					

					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
					da.SelectCommand.Connection.Open();
					da.Fill(dataTable);

					if ((dataTable != null) && (dataTable.Count == 1))
					{
						Row = dataTable.First();
					}
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
			return Row;

		}





		#endregion

		#region Zasoby

		public static ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable Zasoby_GetData(string SKL_ID)
		{
			ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable dataTable = new ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable();
			Zasoby_Fill(dataTable,SKL_ID);
			return dataTable;
		}

		public static void Zasoby_Fill(ABRA_Datasets.Zbozi.FASK_ZASOBYDataTable dataTable, string SKL_ID)
		{

			try
			{

				FirebirdSql.Data.FirebirdClient.FbDataAdapter da = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
				try
				{
					da.SelectCommand = new FirebirdSql.Data.FirebirdClient.FbCommand();
					da.SelectCommand.CommandType = System.Data.CommandType.Text;

					da.SelectCommand.CommandText =	"SELECT " +
													"SC.ID AS ITEMNMBR, " +
													"SC.Name AS ITEMDESC, " +
													"SC.CODE AS ITEMCODE, " +
													"SC.EAN AS VNDITNUM, " +
													"SC.EAN AS CZ_CarKod, " +
													"SSC.STORE_ID AS SKL_ID, " +
													"SSC.Location_ID AS LOCNCODE, " +
													"SSC.Quantity AS QTY, " +
													"SU.UNITRATE AS QTYPACK, " + 
													"SU.Code AS MJ, " +
													"SU.Weight AS WEIGHT " +
													"FROM STORECARDS AS SC " +
													"JOIN STOREUNITS AS  SU ON SU.PARENT_ID = SC.ID "+
													"JOIN STORESUBCARDS AS SSC ON SSC.STORECARD_ID = SC.ID ";

					if (!string.IsNullOrEmpty(SKL_ID))
					{
						da.SelectCommand.CommandText += "WHERE SSC.STORE_ID IN(" + SKL_ID.Trim() + ") ";
					}

					da.SelectCommand.Connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);

					da.Fill(dataTable);
				}
				catch (Exception ex)
				{
					Logging.ExceptionHandler2.Handle(dataTable);
					Logging.ExceptionHandler2.Handle("ABRA Tabulky Definice SQL Dotazu", " Zasoby_Fill", ex);
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}

		}

		#endregion
	}
}
