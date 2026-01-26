using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Ingres.SAD.Database
{
	class Prijem
	{

		public static int Fill_DataSet(string select, DataSet ds, string TableName)
		{
			Globals.LoadConfiguration();
			try
			{
				using (var con = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
				{
					using (var ada = new IngresDataAdapter())
					{
						using (var com = con.CreateCommand())
						{
							ada.SelectCommand = com;
							ada.SelectCommand.CommandText = select;
							ada.SelectCommand.Connection = con;
							ada.SelectCommand.CommandType = CommandType.Text;

							int returnValue;
							returnValue = ada.Fill(ds, TableName);
							return returnValue;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return -1;
			}
		}

		public static bool UpdateCzDosloByCountEntries(byte CZ_Doslo, int CountEntries)
		{
			try
			{

				using (IngresConnection conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
				{
					string select = "UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST_PE + " SET CZ_Doslo = '" + CZ_Doslo.ToString() + "' WHERE (CountEntries = " + CountEntries.ToString() + ")";

					IngresCommand cmd = new IngresCommand(select, conn);

					conn.Open();

					int tmp = cmd.ExecuteNonQuery();

					conn.Close();

					return tmp > 0 ? true : false;
				}
			}
			catch (IngresException sqlex)
			{
				Logging.ExceptionHandler2.Handle(sqlex);
				return false;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
			finally
			{
			}
		}

		public static bool UpdateCzDosloByCountEntries(IngresConnection con, IngresTransaction tran, byte CZ_Doslo, int CountEntries)
		{
			try
			{
				int tmp = 0;

				using (var com = con.CreateCommand())
				{
					com.Transaction = tran;
					com.CommandText = "UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST_PE + " SET CZ_Doslo = '" + CZ_Doslo.ToString() + "' WHERE (CountEntries = " + CountEntries.ToString() + ")";
					com.CommandType = CommandType.Text;

					tmp = com.ExecuteNonQuery();

					return tmp > 0 ? true : false;
				}
			}
			catch (IngresException sqlex)
			{
				Logging.ExceptionHandler2.Handle(sqlex);
				return false;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
			finally
			{
			}
		}


		public static DataTable GetDataByCountEntries(int CountEntries)
		{

			DataTable dataTable = new DataTable();


			IngresDataAdapter da = new IngresDataAdapter();
			try
			{
				da.SelectCommand = new IngresCommand();
				da.SelectCommand.CommandType = System.Data.CommandType.Text;

				da.SelectCommand.CommandText =
				@"SELECT CZ_CarKod, CountEntries, DATEDONE, DAT_VYROBY, DEX_ROW_ID, GUID, ID_TERMINAL, INPUT_MODE, ITEMNMBR, KOD_SW, LOCNCODE, MJ, ORD, " +
				" PONUMBER, QTYPACK, QTYSHPPD, QTYSHPPDMJ, REZ_1, REZ_2, SERLTNUM, TIMEDONE, USER_ID, VNDDOCNM, VNDITNUM, SKL_ID " +
				" FROM CZMST_PI " +
				" WHERE (CountEntries = '" + CountEntries.ToString() + "')";

				da.SelectCommand.Connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
				da.SelectCommand.Connection.Open();
				da.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(dataTable);
				Logging.ExceptionHandler2.Handle(ex);
			}

			return dataTable;

		}

		public static byte? CZMSTPE_PONUMBER_CZDOSLO(string PONUMBER)
		{
			IngresConnection conn = null;
			try
			{

				conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
				string select = "SELECT DISTINCT CZ_Doslo " +
					" FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_PE +
					" WHERE (PONUMBER ='" + PONUMBER + "') AND (CZ_Doslo < 100)";

				IngresCommand cmd = new IngresCommand(select, conn);
				cmd.Connection.Open();
				object tmp = cmd.ExecuteScalar();

				if ((tmp != null) && (tmp is byte?))
					return (byte?)tmp;
				else
					return null;
			}
			catch (IngresException sqlex)
			{
				Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
				if ((conn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					conn.Close();
			}
		}

		public static int CZMSTPE_MAX_CountEntries()
		{
			try
			{
				using (var con = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{
						com.CommandType = System.Data.CommandType.Text;
						com.CommandText = "SELECT MAX( CountEntries ) FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_PE;
						com.Connection.Open();
						object o = com.ExecuteScalar();

						try
						{

							int countentries = Convert.ToInt32(o);
							return countentries;
						}
						catch (Exception e)
						{
							Logging.ExceptionHandler2.Handle(e);
							return 0; //pokud nenalezeno ... ???
						}

					}
				}

			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		public static bool PI_AllowInsert(int? CountEntries, byte ID_Terminal)
		{
			Globals.LoadConfiguration();
			try
			{
				using (var con = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{
						com.CommandText = "SELECT Count(*) as number from " + Fask.SQL.Constants.Common.TABLE_CZMST_PI +
							" WHERE countentries=" + CountEntries +
							" AND ID_TERMINAL='" + ID_Terminal + "'";
						com.Connection = con;
						com.CommandType = CommandType.Text;

						con.Open();

						object datacount = com.ExecuteScalar();

						//if (datacount != null && ((int)datacount) == 0)
						//	return false;
						//else
						//	return true;

						// kdyz tam neco je, tak neumoznit vlozeni, kdyz nic neni tak umoznit vlozeni
						return (datacount != null && ((int)datacount) == 0);
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
		}

		#region Update PI

		public static int Update_CZMST_PI(object data, IngresConnection connection, IngresTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;

					InitializeCommandInsert_PI(commandInsert);
					InitializeCommandSelect_PI(commandSelect);

					using (var adapter = new IngresDataAdapter())
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

		public static void InitializeCommandInsert_PI(IngresCommand command)
		{
			command.CommandText = string.Empty;

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_PI + " ON ";

			command.CommandText += @"INSERT INTO " +
				Fask.SQL.Constants.Common.TABLE_CZMST_PI +
				" ( CountEntries, PONUMBER, ORD, ITEMNMBR, VNDDOCNM," +
				" VNDITNUM, SKL_ID, LOCNCODE, MJ, QTYSHPPD," +
				" QTYSHPPDMJ, QTYPACK, SERLTNUM, KOD_SW, DAT_VYROBY," +
				" DATEDONE, TIMEDONE, CZ_CarKod, REZ_1, REZ_2," +
				" USER_ID, GUID, INPUT_MODE, ID_TERMINAL, " +
				" WEIGHT, NMBRPAL, TYPEPAL, ITEMCODE, Expirace) " +
				" VALUES " +
				" ( @CountEntries, @PONUMBER, @ORD, @ITEMNMBR, @VNDDOCNM," +
				" @VNDITNUM, @SKL_ID, @LOCNCODE, @MJ, @QTYSHPPD," +
				" @QTYSHPPDMJ, @QTYPACK, @SERLTNUM, @KOD_SW, @DAT_VYROBY, " +
				" @DATEDONE, @TIMEDONE, @CZ_CarKod, @REZ_1, @REZ_2," +
				" @USER_ID, @GUID, @INPUT_MODE, @ID_TERMINAL, " +
				" @WEIGHT, @NMBRPAL, @TYPEPAL, @ITEMCODE, @Expirace)";

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_PI + " OFF ";

			command.Parameters.Add(new IngresParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, SourceColumn = "PONUMBER", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace", SourceVersion = System.Data.DataRowVersion.Current });

		}


		public static void InitializeCommandSelect_PI(IngresCommand command)
		{
			command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_PE;
		}


		#endregion

		#endregion

		#region Update PE

		public static int Update_CZMST_PE(object data, IngresConnection connection, IngresTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;

					InitializeCommandInsert_PE(commandInsert);
					InitializeCommandSelect_PE(commandSelect);

					using (var adapter = new IngresDataAdapter())
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

		public static void InitializeCommandInsert_PE(IngresCommand command)
		{
			command.CommandText = string.Empty;

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_PE + " ON ";

			command.CommandText += @"INSERT INTO " +
				Fask.SQL.Constants.Common.TABLE_CZMST_PE +
				" ( CountEntries, PONUMBER, ITEMNMBR, ITEMDESC, ORD, " +
				" VNDDOCNM, VNDITNUM, CZ_CarKod, SKL_ID, LOCNCODE, " +
				" MJ, QTYSHPPD, QTYPACK, CZ_DatVyr_Track, CZ_DatVyr_Delka, " +
				" CZ_SerNum_Track, CZ_SerNum_Delka, CZ_SW_Track, CZ_SW_Delka, CZ_Doslo, " +
				" WEIGHT, NMBRPAL, TYPEPAL, ITEMCODE, SERLTNUM, " +
				" CZ_REZ1_Track, CZ_REZ2_Track, CZ_Expirace_Track )" +
				" VALUES " +
				" ( @CountEntries, @PONUMBER, @ITEMNMBR, @ITEMDESC, @ORD, " +
				" @VNDDOCNM, @VNDITNUM, @CZ_CarKod, @SKL_ID, @LOCNCODE, " +
				" @MJ, @QTYSHPPD, @QTYPACK, @CZ_DatVyr_Track, @CZ_DatVyr_Delka, " +
				" @CZ_SerNum_Track, @CZ_SerNum_Delka, @CZ_SW_Track, @CZ_SW_Delka, @CZ_Doslo, " +
				" @WEIGHT, @NMBRPAL, @TYPEPAL, @ITEMCODE, @SERLTNUM, " +
				" @CZ_REZ1_Track, @CZ_REZ2_Track, @CZ_Expirace_Track)";

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_PE + " OFF ";

			command.Parameters.Add(new IngresParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, SourceColumn = "PONUMBER", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_DatVyr_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_DatVyr_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_DatVyr_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_DatVyr_Delka", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SerNum_Delka", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_SW_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SW_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_SW_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SW_Delka", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_Doslo", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Doslo", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_REZ1_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_REZ2_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new IngresParameter() { ParameterName = "@CZ_Expirace_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Expirace_Track", SourceVersion = System.Data.DataRowVersion.Current });


		}


		public static void InitializeCommandSelect_PE(IngresCommand command)
		{
			command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_PE;
		}


		#endregion

		#endregion

		internal static int Delete_PI(object data, IngresConnection connection, IngresTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandDelete = connection.CreateCommand())
				{
					commandDelete.Transaction = trans;

					commandDelete.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_PI;
					commandDelete.CommandType = CommandType.Text;

					using (var adapter = new IngresDataAdapter())
					{
						adapter.DeleteCommand = commandDelete;


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



	}
}
