using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Fask.SQL.Database
{
	public static class Prodej2
	{
		//public static bool UpdateOrderNumber(int countEntries, string orderNumber)
		//{
		//    try
		//    {
		//        Datasets.VydejBezPredlohyTableAdapters.CZMST_DITableAdapter ta_se = new Datasets.VydejBezPredlohyTableAdapters.CZMST_DITableAdapter();
		//        ta_se.Connection.ConnectionString = Globals.ConnectionString;
		//        return (int)ta_se.UpdateQuery(orderNumber, countEntries) > 0 ? true : false;
		//    }
		//    catch (SqlException sqlex)
		//    {
		//        Log.writeErrorLog(sqlex.Message);
		//        return false;
		//    }
		//    catch (Exception ex)
		//    {
		//        Log.writeErrorLog(ex.Message);
		//        return false;
		//    }
		//}

		public static void FillUniversal(Fask.DataSets.ProdejData ds, string SQL)
		{
			try
			{
				try
				{
					Globals_V1.LoadConfiguration();

					using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
					{
						using (var com = con.CreateCommand())
						{
							//com.CommandText = "SELECT * FROM CZMST_DI WHERE CountEntries = " + CountEntries.ToString();
							com.CommandText = SQL;
							com.CommandType = System.Data.CommandType.Text;

							using (var ada = new System.Data.SqlClient.SqlDataAdapter())
							{
								ada.SelectCommand = com;
								ada.Fill(ds, ds.CZMST_DI.TableName);
							}
						}
					}
				}
				catch (System.Exception ex)
				{
					Fask.Logging.ExceptionHandler2.Handle(ex);
				}

			}
			catch (SqlException sqlex)
			{
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
				throw sqlex;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
			}
		}

		public static Fask.DataSets.ProdejData GETDATA_CZMSTDI_DS(int? countentries)
		{
			Fask.DataSets.ProdejData ds = new Fask.DataSets.ProdejData();

			try
			{
				//string SQL = @"SELECT * FROM dbo.CZMST_DI WHERE (CountEntries=" + countentries.Value + ") order by DATEDONE, TIMEDONE";

				string SQL = @"SELECT DI.*, Zakazka_ID FROM dbo.CZMST_DI AS DI " +
" LEFT JOIN CZMST_DIH AS DIH ON DIH.CountEntries = DI.CountEntries " +
" WHERE (DI.CountEntries = " + countentries.Value + " ) " +
" order by DATEDONE, TIMEDONE ";

				FillUniversal(ds, SQL);
				return ds;
			}
			catch (SqlException sqlex)
			{
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
			}
		}

		public static Fask.DataSets.ProdejData GETDATA_CZMSTDI_DS_GroupBy_CountEntries(int? countentries)
		{
			Fask.DataSets.ProdejData ds = new Fask.DataSets.ProdejData();

			try
			{
				//string SQL = @"SELECT CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID, CountEntries, ODB_ID, STR_ID, DOC_ID, " +
				//	" ITEMNMBR, LOCNCODE, SUM(QTYSHPPD) AS QTYSHPPD, QTYPACK, SERLTNUM, " +
				//	" TAXAMPIE, AMOUNPIE, WITHTAX, PRICEX, REZ_1, " +
				//	" REZ_2, REZ_3, REZ_4, USER_ID, DATEDONE, " +
				//	" MAX(TIMEDONE) AS TIMEDONE, VNDITNUM, CZ_CarKod, SKL_ID, MJ, " +
				//	" SUM(QTYSHPPDMJ) AS QTYSHPPDMJ, PRAC_ID, INPUT_MODE, ID_TERMINAL, ITEMCODE, " +
				//	" DOC_ID2, mena_ID, TAXAMPIEM, AMOUNPIEM, mena_IDM, " +
				//	" LOCNCODEDEST, SKL_ID_DEST, EXPIRACE, AttributeToSN " +
				//	" FROM CZMST_DI " +
				//	" WHERE (CountEntries = " + countentries.Value + ") " +
				//	" GROUP BY CountEntries, ODB_ID, STR_ID, DOC_ID, ITEMNMBR, " +
				//	" LOCNCODE, QTYPACK, SERLTNUM, TAXAMPIE, AMOUNPIE, " +
				//	" WITHTAX, PRICEX, REZ_1, REZ_2, REZ_3, " +
				//	" REZ_4, USER_ID, DATEDONE, VNDITNUM, CZ_CarKod, " +
				//	" SKL_ID, MJ, PRAC_ID, INPUT_MODE, ID_TERMINAL, " +
				//	" ITEMCODE, DOC_ID2, mena_ID, TAXAMPIEM, AMOUNPIEM, " +
				//	" mena_IDM, LOCNCODEDEST, SKL_ID_DEST, EXPIRACE, AttributeToSN " +
				//	" order by DATEDONE, TIMEDONE";

				string SQL = @"SELECT " +
	" CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID, " +
	" DI.CountEntries, ODB_ID, STR_ID, DOC_ID, " +
	" ITEMNMBR, LOCNCODE, SUM(QTYSHPPD) AS QTYSHPPD, QTYPACK, SERLTNUM, " +
	" TAXAMPIE, AMOUNPIE, WITHTAX, PRICEX, REZ_1, " +
	" REZ_2, REZ_3, REZ_4, USER_ID, DATEDONE, " +
	" MAX(TIMEDONE) AS TIMEDONE, VNDITNUM, CZ_CarKod, DI.SKL_ID, MJ, " +
	" SUM(QTYSHPPDMJ) AS QTYSHPPDMJ, PRAC_ID, INPUT_MODE, ID_TERMINAL, ITEMCODE, " +
	" DOC_ID2, DI.mena_ID, TAXAMPIEM, AMOUNPIEM, mena_IDM, " +
	" LOCNCODEDEST, SKL_ID_DEST, EXPIRACE, AttributeToSN, Zakazka_ID " +
" FROM CZMST_DI AS DI " +
" LEFT JOIN CZMST_DIH AS DIH ON DIH.CountEntries = DI.CountEntries " +
" WHERE (DI.CountEntries =  " + countentries.Value + ") " +
" GROUP BY " +
	" DI.CountEntries, ODB_ID, STR_ID, DOC_ID, ITEMNMBR, " +
	" LOCNCODE, QTYPACK, SERLTNUM, TAXAMPIE, AMOUNPIE, " +
	" WITHTAX, PRICEX, REZ_1, REZ_2, REZ_3, " +
	" REZ_4, USER_ID, DATEDONE, VNDITNUM, CZ_CarKod, " +
	" DI.SKL_ID, MJ, PRAC_ID, INPUT_MODE, ID_TERMINAL, " +
	" ITEMCODE, DOC_ID2, DI.mena_ID, TAXAMPIEM, AMOUNPIEM, " +
	" mena_IDM, LOCNCODEDEST, SKL_ID_DEST, EXPIRACE, AttributeToSN, Zakazka_ID " +
" ORDER BY DATEDONE, TIMEDONE;";




				FillUniversal(ds, SQL);
				return ds;
			}
			catch (SqlException sqlex)
			{
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
			}
		}

		internal static Fask.Interfaces.DataSets.Strediska.CZMST091Row GETDATA_CZMST091(string SKL_ID)
		{
			try
			{
				Fask.Interfaces.DataSets.Strediska.CZMST091DataTable dtlocal = new Fask.Interfaces.DataSets.Strediska.CZMST091DataTable();

				Globals_V1.LoadConfiguration();
				Datasets.StrediskaTableAdapters.CZMST091TableAdapter ta = new Datasets.StrediskaTableAdapters.CZMST091TableAdapter();
				ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

				var data = ta.GetDataBySKL_ID(SKL_ID);

				if ((data != null) && (data.Count > 0))
				{
					dtlocal.Clear();
					dtlocal.ImportRow(data.First());
					return dtlocal.First();
				}
				else
					return null;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Fask.ModulePohodaXML.Database.Prodej2", System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
				return null;
			}
		}

		public static void GETDATA_CZMSTDI_SKLID_LOCNCODE(string countentries, string DOC_ID, out string SKL_ID, out string LOCNCODE)
		{
			SKL_ID = string.Empty;
			LOCNCODE = string.Empty;

			Globals_V1.LoadConfiguration();

			try
			{

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{

					using (var com = con.CreateCommand())
					{

						com.CommandType = System.Data.CommandType.Text;
						com.CommandText = "SELECT distinct LOCNCODEDEST FROM CZMST_DI WHERE CountEntries = '" + countentries.Trim() + "' AND DOC_ID = '" + DOC_ID + "' ";
						com.Connection.Open();
						object o = com.ExecuteScalar();

						try
						{
							LOCNCODE = o.ToString();
						}
						catch (Exception ex)
						{
							Logging.ExceptionHandler2.Handle(ex);
							throw ex;
						}

						com.CommandText = "SELECT distinct SKL_ID_DEST FROM CZMST_DI WHERE CountEntries = '" + countentries.Trim() + "' AND DOC_ID = '" + DOC_ID + "' ";
						object s = com.ExecuteScalar();

						try
						{
							SKL_ID = s.ToString();
						}
						catch (Exception ex)
						{
							Logging.ExceptionHandler2.Handle(ex);
							throw ex;
						}

					}
				}
			}
			catch (SqlException sqlex)
			{
				Fask.Logging.ExceptionHandler2.Handle(sqlex);
			}
			catch (Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
			finally
			{
			}
		}

		public static int Update_CZMST_DI(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;

					InitializeCommandInsert_DI(commandInsert);
					InitializeCommandSelect_DI(commandSelect);

					using (var adapter = new SqlDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


						if (dataIsDataSet != null)
							result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
						else if (dataIsDataTable != null)
							result = adapter.Update(dataIsDataTable);
						else if (dataIsDataRow != null)
							result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
						else if (dataIsDataRowArray != null)
							result = adapter.Update(dataIsDataRowArray);
						else
							throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		private static void InitializeCommandInsert_DI(SqlCommand command)
		{
			command.CommandText =
				@"INSERT INTO " +
				Constants.Common.TABLE_CZMST_DI +
				"( " +
				" CountEntries, VNDITNUM, CZ_CarKod, ODB_ID, STR_ID, " +
				" DOC_ID, DOC_ID2, SKL_ID, PRAC_ID, ITEMNMBR," +
				" LOCNCODE, MJ, QTYSHPPD, QTYSHPPDMJ, QTYPACK," +
				" SERLTNUM, TAXAMPIE, AMOUNPIE, WITHTAX, PRICEX," +
				" REZ_1, REZ_2, REZ_3, REZ_4, USER_ID," +
				" DATEDONE, TIMEDONE, GUID, INPUT_MODE, ID_TERMINAL," +
				" LOCNCODEDEST, SKL_ID_DEST, WEIGHT, EXPIRACE, AttributeToSN " +
				" ) VALUES ( " +
				" @CountEntries, @VNDITNUM, @CZ_CarKod, @ODB_ID, @STR_ID, " +
				" @DOC_ID, @DOC_ID2, @SKL_ID, @PRAC_ID, @ITEMNMBR," +
				" @LOCNCODE, @MJ, @QTYSHPPD, @QTYSHPPDMJ, @QTYPACK, " +
				" @SERLTNUM, @TAXAMPIE, @AMOUNPIE, @WITHTAX, @PRICEX, " +
				" @REZ_1, @REZ_2, @REZ_3, @REZ_4, @USER_ID, " +
				" @DATEDONE, @TIMEDONE, @GUID, @INPUT_MODE, @ID_TERMINAL," +
				" @LOCNCODEDEST, @SKL_ID_DEST, @WEIGHT, @EXPIRACE, @AttributeToSN " +
				" ) ";


			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, SourceColumn = "ODB_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@STR_ID", DbType = System.Data.DbType.String, SourceColumn = "STR_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DOC_ID", DbType = System.Data.DbType.String, SourceColumn = "DOC_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRAC_ID", DbType = System.Data.DbType.String, SourceColumn = "PRAC_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TAXAMPIE", DbType = System.Data.DbType.Decimal, SourceColumn = "TAXAMPIE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@AMOUNPIE", DbType = System.Data.DbType.Decimal, SourceColumn = "AMOUNPIE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@WITHTAX", DbType = System.Data.DbType.Byte, SourceColumn = "WITHTAX", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRICEX", DbType = System.Data.DbType.Byte, SourceColumn = "PRICEX", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_3", DbType = System.Data.DbType.String, SourceColumn = "REZ_3", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_4", DbType = System.Data.DbType.String, SourceColumn = "REZ_4", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.String, SourceColumn = "DEX_ROW_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DOC_ID2", DbType = System.Data.DbType.String, SourceColumn = "DOC_ID2", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODEDEST", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODEDEST", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID_DEST", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID_DEST", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@EXPIRACE", DbType = System.Data.DbType.DateTime, SourceColumn = "EXPIRACE", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new SqlParameter() { ParameterName = "@AttributeToSN", DbType = System.Data.DbType.String, SourceColumn = "AttributeToSN", SourceVersion = System.Data.DataRowVersion.Current });


		}

		private static void InitializeCommandSelect_DI(SqlCommand command)
		{
			command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_DI;
		}


		#endregion

		public static int Update_CZMST_DIH(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;

					InitializeCommandInsert_DIH(commandInsert);
					InitializeCommandSelect_DIH(commandSelect);

					using (var adapter = new SqlDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


						if (dataIsDataSet != null)
							result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
						else if (dataIsDataTable != null)
							result = adapter.Update(dataIsDataTable);
						else if (dataIsDataRow != null)
							result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
						else if (dataIsDataRowArray != null)
							result = adapter.Update(dataIsDataRowArray);
						else
							throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		private static void InitializeCommandInsert_DIH(SqlCommand command)
		{
			//command.CommandText = @"INSERT INTO " + Constants.Common.TABLE_CZMST_DIH + "(" +
			//	" [Zakazka_ID],[Paleta_ID],[mena_ID],[SKL_ID],[CountEntries]" +
			//	")VALUES(" +
			//	" @Zakazka_ID,@Paleta_ID,@mena_ID,@SKL_ID,@CountEntries" +
			//	")";

			command.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZMST_DIH + " (" +
	" [Zakazka_ID],[Paleta_ID],[mena_ID],[SKL_ID],[CountEntries],[ISOK],[status]" +
	" ) VALUES (" +
	" @Zakazka_ID,@Paleta_ID,@mena_ID,@SKL_ID,@CountEntries,@ISOK,@status" +
	" )";
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ISOK", SqlDbType = SqlDbType.DateTime, SourceColumn = "ISOK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@status", SqlDbType = SqlDbType.Int, SourceColumn = "status", SourceVersion = System.Data.DataRowVersion.Current });



			command.Parameters.Add(new SqlParameter() { ParameterName = "@Zakazka_ID", SqlDbType = SqlDbType.NVarChar,SourceColumn = "Zakazka_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@Paleta_ID", SqlDbType = SqlDbType.NVarChar, SourceColumn = "Paleta_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@mena_ID", SqlDbType = SqlDbType.NVarChar, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", SqlDbType = SqlDbType.NVarChar, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", SqlDbType = SqlDbType.Int, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
		}

		private static void InitializeCommandSelect_DIH(SqlCommand command)
		{
			command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_DIH;
		}


		#endregion


	}
}
