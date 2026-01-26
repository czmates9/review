using Fask.Server.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Database
{
    class Vydej
    {

		public static byte? CZMSTSE_SOPNUMBER_CZDOSLO(string sopnumber)
		{
			Globals_V1.LoadConfiguration();
			try
			{

				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{

						com.CommandText = "SELECT DISTINCT CZ_Doslo " +
						" FROM " + Constants.Common.TABLE_CZMST_SE +
						" WHERE (SOPNUMBE ='" + sopnumber + "') AND (CZ_Doslo < 100)";

						com.CommandType = System.Data.CommandType.Text;

						con.Open();

						object tmp = com.ExecuteScalar();

						if ((tmp != null) && (tmp is byte?))
							return (byte?)tmp;
						else
							return null;
					}
				}
			}
			catch (SqlException sqlex)
			{
				Logging.ExceptionHandler2.Handle(sqlex);
				return null;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

		public static bool CZMSTSE_UPDATE_CZDOSLO(string sopnumber)
		{

			Globals_V1.LoadConfiguration();
			try
			{
				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{

					using (var com = con.CreateCommand())
					{

						com.CommandText = "UPDATE " + Constants.Common.TABLE_CZMST_SE +
										" SET CZ_Doslo = 201 " +
										" WHERE (SOPNUMBE = '" + sopnumber + "') AND (CZ_Doslo = 0) ";

						com.CommandType = System.Data.CommandType.Text;

						con.Open();
						int tmp = com.ExecuteNonQuery();

						return tmp > 0 ? true : false;
					}
				}
			}
			catch (SqlException sqlex)
			{
				Logging.ExceptionHandler2.Handle(sqlex);
				return false;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
		}

		public static int CZMSTSE_MAX_CountEntries()
		{
			Globals_V1.LoadConfiguration();
			try
			{
				using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{
						com.CommandType = System.Data.CommandType.Text;
						com.CommandText = "SELECT max( CountEntries ) FROM " + Constants.Common.TABLE_CZMST_SE ;

						con.Open();

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

		#region Update SE

		public static int Update_CZMST_SE(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandUpdate = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandUpdate.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_SE(commandInsert);
					InitializeCommandUpdate_SE(commandUpdate);
					InitializeCommandSelect_SE(commandSelect);

					using (var adapter = new SqlDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		public static void InitializeCommandInsert_SE(SqlCommand command)
		{
			//command.CommandText = " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_SE + " ON ";

			//command.CommandText += @" INSERT INTO " + Constants.Common.TABLE_CZMST_SE + " (CountEntries, SOPNUMBE, ITEMNMBR, ITEMTYPE, ITEMDESC, VNDDOCNM, VNDITNUM, ORD, CZ_CarKod, LOCNCODE, QTYSHPPD, QTYPACK, CZ_DatVyr_Track, CZ_DatVyr_Delka, CZ_SerNum_Track, CZ_SerNum_Delka, CZ_SW_Track, CZ_SW_Delka, CZ_Doslo, DEX_ROW_ID, Note, TYPEPAL, QTYPAL, PRINTED, PRIORITY, SKL_ID, MJ, CZ_REZ1_Track, CZ_REZ2_Track, ITEMCODE, WEIGHT, CZ_Expirace_Track) " +
			//	" VALUES " +
			//	"(@CountEntries, @SOPNUMBE, @ITEMNMBR, @ITEMTYPE, @ITEMDESC, @VNDDOCNM, @VNDITNUM, @ORD, @CZ_CarKod, @LOCNCODE, @QTYSHPPD, @QTYPACK, @CZ_DatVyr_Track, @CZ_DatVyr_Delka, @CZ_SerNum_Track, @CZ_SerNum_Delka, @CZ_SW_Track, @CZ_SW_Delka, @CZ_Doslo, @DEX_ROW_ID, @Note, @TYPEPAL, @QTYPAL, @PRINTED, @PRIORITY, @SKL_ID, @MJ, @CZ_REZ1_TRACK, @CZ_REZ2_TRACK, @ITEMCODE, @WEIGHT, @CZ_Expirace_Track)";


			command.CommandText += @" INSERT INTO " + Constants.Common.TABLE_CZMST_SE + " (CountEntries, SOPNUMBE, ITEMNMBR, ITEMTYPE, ITEMDESC, VNDDOCNM, VNDITNUM, ORD, CZ_CarKod, LOCNCODE, QTYSHPPD, QTYPACK, CZ_DatVyr_Track, CZ_DatVyr_Delka, CZ_SerNum_Track, CZ_SerNum_Delka, CZ_SW_Track, CZ_SW_Delka, CZ_Doslo, Note, TYPEPAL, QTYPAL, PRINTED, PRIORITY, SKL_ID, MJ, CZ_REZ1_Track, CZ_REZ2_Track, ITEMCODE, WEIGHT, CZ_Expirace_Track) " +
				" VALUES " +
				"(@CountEntries, @SOPNUMBE, @ITEMNMBR, @ITEMTYPE, @ITEMDESC, @VNDDOCNM, @VNDITNUM, @ORD, @CZ_CarKod, @LOCNCODE, @QTYSHPPD, @QTYPACK, @CZ_DatVyr_Track, @CZ_DatVyr_Delka, @CZ_SerNum_Track, @CZ_SerNum_Delka, @CZ_SW_Track, @CZ_SW_Delka, @CZ_Doslo, @Note, @TYPEPAL, @QTYPAL, @PRINTED, @PRIORITY, @SKL_ID, @MJ, @CZ_REZ1_TRACK, @CZ_REZ2_TRACK, @ITEMCODE, @WEIGHT, @CZ_Expirace_Track)";


			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_SE + " OFF ";

			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_DatVyr_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_DatVyr_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_DatVyr_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_DatVyr_Delka", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SerNum_Delka", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SW_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SW_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SW_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SW_Delka", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Doslo", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Doslo", SourceVersion = System.Data.DataRowVersion.Current });
			//command.Parameters.Add(new SqlParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@Note", DbType = System.Data.DbType.String, SourceColumn = "Note", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPAL", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Boolean, SourceColumn = "PRINTED", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRIORITY", DbType = System.Data.DbType.Byte, SourceColumn = "PRIORITY", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });
			
			//command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ1_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_TRACK", SourceVersion = System.Data.DataRowVersion.Current });
			//command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ2_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_TRACK", SourceVersion = System.Data.DataRowVersion.Current });
			
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Expirace_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Expirace_Track", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ1_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ2_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_Track", SourceVersion = System.Data.DataRowVersion.Current });


		}

		public static void InitializeCommandUpdate_SE(SqlCommand command)
		{
			command.CommandText  = "UPDATE " + Constants.Common.TABLE_CZMST_SE + " SET CZ_Doslo = @CZ_Doslo " + 
				" WHERE (DEX_ROW_ID = @DEX_ROW_ID) AND (CountEntries = @CountEntries)";

			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Doslo", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Doslo", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Original });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = System.Data.DataRowVersion.Original });

		}

		public static void InitializeCommandSelect_SE(SqlCommand command)
		{
			command.CommandText = "Select * from " + Constants.Common.TABLE_CZMST_SE;
		}


		#endregion

		#endregion

		#region Update SI

		public static int Update_CZMST_SI(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;

					InitializeCommandInsert_SI(commandInsert);
					InitializeCommandSelect_SI(commandSelect);

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

		public static void InitializeCommandInsert_SI(SqlCommand command)
		{
			command.CommandText = "INSERT INTO " + Constants.Common.TABLE_CZMST_SI + " (CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDDOCNM, VNDITNUM, CZ_CarKod, SKL_ID, LOCNCODE, MJ, QTYSHPPD, QTYPACK, QTYSHPPDMJ, SERLTNUM, KOD_SW, DAT_VYROBY, REZ_1, REZ_2, ODBER_ID,DATEDONE, TIMEDONE, USER_ID, TYPEPAL, NMBRPAL, PRINTED, GUID, INPUT_MODE, ID_TERMINAL, Expirace) " + 
				" VALUES " + 
				" (@CountEntries,@SOPNUMBE,@ITEMNMBR,@ORD,@VNDDOCNM,@VNDITNUM,@CZ_CarKod,@SKL_ID,@LOCNCODE,@MJ,@QTYSHPPD,@QTYPACK,@QTYSHPPDMJ,@SERLTNUM,@KOD_SW,@DAT_VYROBY,@REZ_1,@REZ_2,@ODBER_ID,@DATEDONE,@TIMEDONE,@USER_ID,@TYPEPAL,@NMBRPAL,@PRINTED,@GUID,@INPUT_MODE,@ID_TERMINAL, @Expirace)";
			
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ODBER_ID", DbType = System.Data.DbType.String, SourceColumn = "ODBER_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Byte, SourceColumn = "PRINTED", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace", SourceVersion = System.Data.DataRowVersion.Current });

		}

		public static void InitializeCommandSelect_SI(SqlCommand command)
		{
			command.CommandText = "Select * from " + Constants.Common.TABLE_CZMST_SI;
		}


		#endregion

		#endregion

		#region Update SIH

		public static int Update_CZMST_SIH(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;

					InitializeCommandInsert_SIH(commandInsert);
					InitializeCommandSelect_SIH(commandSelect);

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

        public static void InitializeCommandInsert_SIH(SqlCommand command)
		{
			command.CommandText = "INSERT INTO "+ Constants.Common.TABLE_CZMST_SIH + " (CountEntries, TISKARNA_NAME, PRAC_ID) " + 
				" VALUES " + 
				" (@CountEntries, @TISKARNA_NAME, @PRAC_ID)";

			command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TISKARNA_NAME", DbType = System.Data.DbType.String, SourceColumn = "TISKARNA_NAME", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRAC_ID", DbType = System.Data.DbType.String, SourceColumn = "PRAC_ID", SourceVersion = System.Data.DataRowVersion.Current });

		}

		public static void InitializeCommandSelect_SIH(SqlCommand command)
		{
			command.CommandText = "Select * from " + Constants.Common.TABLE_CZMST_SIH;
		}


		#endregion

		#endregion


		public static int Fill_DataSet(string select, DataSet ds, string TableName)
		{
			Globals_V1.LoadConfiguration();
			try
			{
				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var ada = new System.Data.SqlClient.SqlDataAdapter())
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
			Globals_V1.LoadConfiguration();

			try
			{
				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{

					using (var com = con.CreateCommand())
					{
						com.CommandType = CommandType.Text;
						com.CommandText = "UPDATE " + Constants.Common.TABLE_CZMST_SE + " SET CZ_Doslo = '" + CZ_Doslo.ToString() + "' WHERE (CountEntries = " + CountEntries.ToString() + ")";

						con.Open();
						int tmp = com.ExecuteNonQuery();
						return tmp > 0 ? true : false;
					}
				}
			}
			catch (SqlException sqlex)
			{
				Logging.ExceptionHandler2.Handle(sqlex);
				return false;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
		}

		public static bool SI_AllowInsert(int? CountEntries, byte ID_Terminal)
		{
			Globals_V1.LoadConfiguration();
			try
			{
				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var com = con.CreateCommand())
					{
						com.CommandText = "SELECT Count(*) as number from " + Constants.Common.TABLE_CZMST_SI +
							" WHERE countentries=" + CountEntries +
							" AND ID_TERMINAL='" + ID_Terminal + "'";
						com.Connection = con;
						com.CommandType = CommandType.Text;

						con.Open();

						object datacount = com.ExecuteScalar();

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

		public static bool Deleted_SI_SIH_vTransakci(int? CountEntries)
		{
			Globals_V1.LoadConfiguration();

			SqlTransaction trans = null;
			SqlConnection con = null;
			try
			{
				
				using (con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					con.Open();
					trans = con.BeginTransaction();

					using (var com = con.CreateCommand())
					{
						com.CommandText =  "DELETE FROM " + Constants.Common.TABLE_CZMST_SI + " WHERE countentries=" + CountEntries;
						com.Connection = con;
						com.CommandType = CommandType.Text;
						com.ExecuteNonQuery();
					}

					using (var com = con.CreateCommand())
					{
						com.CommandText = "DELETE FROM " + Constants.Common.TABLE_CZMST_SIH + " WHERE countentries=" + CountEntries;
						com.Connection = con;
						com.CommandType = CommandType.Text;
						com.ExecuteNonQuery();
					}

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
			finally
			{
				if (con != null && con.State == System.Data.ConnectionState.Open)
					con.Close();
			}
		}

		#region Delete

		public static int Delete_SE(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandDelete = connection.CreateCommand())
				{
					commandDelete.Transaction = trans;

					commandDelete.CommandText = "DELETE FROM " + Constants.Common.TABLE_CZMST_SE ;
					commandDelete.CommandType = CommandType.Text;

					using (var adapter = new SqlDataAdapter())
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

		public static int Delete_SI(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandDelete = connection.CreateCommand())
				{
					commandDelete.Transaction = trans;

					commandDelete.CommandText = "DELETE FROM " + Constants.Common.TABLE_CZMST_SI;
					commandDelete.CommandType = CommandType.Text;

					using (var adapter = new SqlDataAdapter())
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


		public static int Delete_SIH(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandDelete = connection.CreateCommand())
				{
					commandDelete.Transaction = trans;

					commandDelete.CommandText = "DELETE FROM " + Constants.Common.TABLE_CZMST_SIH;
					commandDelete.CommandType = CommandType.Text;

					using (var adapter = new SqlDataAdapter())
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

		#endregion



		#region Update CZMST094

		public static int Update_CZMST094(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandUpdate = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandUpdate.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_CZMST094(commandInsert);
					InitializeCommandUpdate_CZMST094(commandUpdate);
					InitializeCommandSelect_CZMST094(commandSelect);

					using (var adapter = new SqlDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		public static void InitializeCommandInsert_CZMST094(SqlCommand command)
		{
			//command.CommandText = " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST094 + " ON ";

			command.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZMST094 + "([SKL_ID],[LOCNCODE],[TYPE],[Description],[Barcode]" +
					   " ) VALUES (" +
					   " @SKL_ID, @LOCNCODE, @TYPE, @Description, " +
					   " @Barcode)";
			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST094 + " OFF ";

			command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TYPE", DbType = System.Data.DbType.String, SourceColumn = "TYPE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, SourceColumn = "Description", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, SourceColumn = "Barcode", SourceVersion = System.Data.DataRowVersion.Current });


		}

		public static void InitializeCommandUpdate_CZMST094(SqlCommand command)
		{
			command.CommandText = @"UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " SET" +
					  "  SKL_ID = @SKL_ID, TYPE = @TYPE, Description = @Description," +
					  " Barcode = @Barcode, LOCNCODE = @LOCNCODE" +
					  " WHERE (LOCNCODE = @LOCNCODE)";


			//comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(row.SKL_ID) ? throw new Exception("SKL_ID is null!") : row.SKL_ID });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TYPE", DbType = System.Data.DbType.String, SourceColumn = "TYPE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, SourceColumn = "Description", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, SourceColumn = "Barcode", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Original });
			//comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsLOCNCODENull() ? (object)DBNull.Value : zboziRow.LOCNCODE });


		}

		public static void InitializeCommandSelect_CZMST094(SqlCommand command)
		{
			command.CommandText = "Select * from " + Constants.Common.TABLE_CZMST094;
		}


		#endregion

		#region delete
		public static bool Delete_CZMST094_vTransakci(string SKL_ID, string LOCNCODE)
		{
			Globals_V1.LoadConfiguration();

			using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
			{
				con.Open();
				using (var trans = con.BeginTransaction(IsolationLevel.ReadCommitted))
				{
					try
					{
						// veřejná metoda – vlastní spojení + transakce
						Delete_CZMST094_Core(con, trans, SKL_ID, LOCNCODE);
						trans.Commit();
						return true;
					}
					catch
					{
						try { trans.Rollback(); } catch { /* log */ }
						throw;
					}
				}
			}
		}

		/// <summary>
		/// Interní „core“ varianta – používá dodané spojení + transakci.
		/// Lze volat z exportu, aby mazání i zápis běžely v JEDNÉ transakci.
		/// </summary>
		internal static void Delete_CZMST094_Core(SqlConnection con, SqlTransaction trans, string SKL_ID, string LOCNCODE)
		{
			if (con == null) throw new ArgumentNullException(nameof(con));
			if (trans == null) throw new ArgumentNullException(nameof(trans));

			using (var com = con.CreateCommand())
			{
				com.Transaction = trans;
				com.CommandType = CommandType.Text;

				// Když nejsou filtry -> použij rychlejší TRUNCATE (pokud tabulka nemá FK na sebe/není cílem FK)
				if (string.IsNullOrEmpty(SKL_ID) && string.IsNullOrEmpty(LOCNCODE))
				{
					com.CommandText = $"TRUNCATE TABLE {Constants.Common.TABLE_CZMST094}";
					com.ExecuteNonQuery();
					return;
				}

				// Jinak mazání s WHERE (parametrizované)
				var sb = new StringBuilder();
				sb.Append("DELETE FROM ").Append(Constants.Common.TABLE_CZMST094).Append(" WHERE 1=1");

				if (!string.IsNullOrEmpty(SKL_ID))
				{
					sb.Append(" AND SKL_ID = @SKL_ID");
					var p = com.CreateParameter();
					p.ParameterName = "@SKL_ID";
					p.SqlDbType = SqlDbType.VarChar; // upravte dle skutečného typu (Int/BigInt/…)
					p.Value = SKL_ID;
					com.Parameters.Add(p);
				}

				if (!string.IsNullOrEmpty(LOCNCODE))
				{
					sb.Append(" AND LOCNCODE = @LOCNCODE");
					var p = com.CreateParameter();
					p.ParameterName = "@LOCNCODE";
					p.SqlDbType = SqlDbType.VarChar; // upravte dle schématu (délka)
					p.Value = LOCNCODE;
					com.Parameters.Add(p);
				}

				com.CommandText = sb.ToString();
				com.ExecuteNonQuery();
			}
		}

		#endregion

		#endregion


	}
}
