using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Database
{
    class Prodej
    {


		public static int? CZMST_DI_DavkaExist(SqlConnection con , int? CountEntries)
		{

			try
			{
				using (var com = con.CreateCommand())
				{
					com.CommandText = "SELECT Count(*) AS number FROM " + Constants.Common.TABLE_CZMST_DI + " where countentries=" + CountEntries.ToString();
					com.CommandType = System.Data.CommandType.Text;

					con.Open();

					object tmp = com.ExecuteScalar();

					if ((tmp != null) && (tmp is int?))
						return (int?)tmp;
					else
						return null;
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

		public static Fask.DataSets.TypDokladu Get_CZMST_DI(string DOC_ID, string DOC_ID2)
		{
			Globals_V1.LoadConfiguration();

			Fask.DataSets.TypDokladu dt = new DataSets.TypDokladu();
			try
			{
				using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					using (var ada = new System.Data.SqlClient.SqlDataAdapter())
					{
						using (var com = con.CreateCommand())
						{
							ada.SelectCommand = com;
							ada.SelectCommand.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST092 + 
								" WHERE doc_id=@doc_id and doc_id2=@doc_id2";
							ada.SelectCommand.Connection = con;
							ada.SelectCommand.CommandType = CommandType.Text;

							ada.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "@doc_id", DbType = System.Data.DbType.String, Value = DOC_ID, SourceVersion = DataRowVersion.Current });
							ada.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "@doc_id2", DbType = System.Data.DbType.String, Value = DOC_ID2, SourceVersion = DataRowVersion.Current });

							int returnValue;
							returnValue = ada.Fill(dt, dt.CZMST092.TableName);
							return dt;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				return null;
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
					InitializeCommandInsert_CZMST_DI(commandInsert);
					InitializeCommandSelect_CZMST_DI(commandSelect);

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

		private static void InitializeCommandInsert_CZMST_DI(SqlCommand command)
		{
			command.CommandText = 
				@"INSERT INTO " + 
				Constants.Common.TABLE_CZMST_DI +  
				"( "  +
				" CountEntries, VNDITNUM, CZ_CarKod, ODB_ID, STR_ID, " + 
				" DOC_ID, DOC_ID2, SKL_ID, PRAC_ID, ITEMNMBR," + 
				" LOCNCODE, MJ, QTYSHPPD, QTYSHPPDMJ, QTYPACK," + 
				" SERLTNUM, TAXAMPIE, AMOUNPIE, WITHTAX, PRICEX," + 
				" REZ_1, REZ_2, REZ_3, REZ_4, USER_ID," + 
				" DATEDONE, TIMEDONE, GUID, INPUT_MODE, ID_TERMINAL," + 
				" LOCNCODEDEST, SKL_ID_DEST, WEIGHT, EXPIRACE " +
				" ) VALUES ( " + 
				" @CountEntries, @VNDITNUM, @CZ_CarKod, @ODB_ID, @STR_ID, " + 
				" @DOC_ID, @DOC_ID2, @SKL_ID, @PRAC_ID, @ITEMNMBR," + 
				" @LOCNCODE, @MJ, @QTYSHPPD, @QTYSHPPDMJ, @QTYPACK, " + 
				" @SERLTNUM, @TAXAMPIE, @AMOUNPIE, @WITHTAX, @PRICEX, " + 
				" @REZ_1, @REZ_2, @REZ_3, @REZ_4, @USER_ID, " + 
				" @DATEDONE, @TIMEDONE, @GUID, @INPUT_MODE, @ID_TERMINAL," +
				" @LOCNCODEDEST, @SKL_ID_DEST, @WEIGHT, @EXPIRACE " + 
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
			
		}

		private static void InitializeCommandSelect_CZMST_DI(SqlCommand command)
		{
			command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_CZMST_DI;
		}


		#endregion
	}
}
