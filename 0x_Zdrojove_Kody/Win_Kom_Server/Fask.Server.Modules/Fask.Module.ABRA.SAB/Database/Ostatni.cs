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
    class Ostatni
    {


		#region Update SE


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
						com.CommandText = "SELECT max( CountEntries ) FROM " + Constants.Common.TABLE_CZMST_SE;

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
		#endregion

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
			command.CommandText = "UPDATE " + Constants.Common.TABLE_CZMST_SE + " SET CZ_Doslo = @CZ_Doslo " +
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


	}
}
