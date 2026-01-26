using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Database
{
    class Zbozi
    {

		public static int Fill_Zasoby(Fask.Interfaces.DataSets.Zbozi ds)
		{
			Globals_V1.LoadConfiguration();
			try
			{
				ds.FASK_ZASOBY.Clear();

				using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
				{
					SqlCommand comm = con.CreateCommand();

					comm.CommandText = "SELECT * FROM " + Constants.Common.TABLE_FASK_ZASOBY;



					comm.CommandType = System.Data.CommandType.Text;

					using (var ada = new SqlDataAdapter())
					{
						ada.SelectCommand = comm;
						int returnValue;
						returnValue = ada.Fill(ds, ds.FASK_ZASOBY.TableName);

						return returnValue;
					}
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ds);
				Logging.ExceptionHandler2.Handle(ex);

				return -1;
			}
		}

		public static int Delete_FASK_ZASOBY(SqlConnection connection, SqlTransaction trans)
		{
			Globals_V1.LoadConfiguration();
			try
			{
				using (SqlCommand comm = connection.CreateCommand())
				{
					comm.Transaction = trans;
					comm.CommandType = System.Data.CommandType.Text;
					comm.CommandText = "DELETE FROM " + Constants.Common.TABLE_FASK_ZASOBY;
					
					int retunValue;
					retunValue = comm.ExecuteNonQuery();
					return retunValue;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);

				return -1;
			}
		}

		public static int Update_FASK_ZASOBY(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_FASK_ZASOBY(commandInsert);
					InitializeCommandSelect_FASK_ZASOBY(commandSelect);

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

		private static void InitializeCommandInsert_FASK_ZASOBY(SqlCommand command)
		{
			command.CommandText = @"INSERT INTO " + Constants.Common.TABLE_FASK_ZASOBY  + " (" + 
				" [ITEMNMBR], [ITEMDESC], [ITEMCODE], [VNDITNUM], [CZ_CarKod]," + 
				" [LOCNCODE], [SKL_ID], [QTY], [QTYPACK], [MJ]," + 
				" [DMJ], [TAXRATE], [PRICE0], [PRICE1], [PRICE2]," + 
				" [PRICE3], [PRICE4], [PRICE5], [CZ_SerNum_Track], [CZ_SerNum_Delka]," + 
				" [CZ_Rez1_Track], [CZ_Rez2_Track], [CZ_Rez3_Track], [CZ_Rez4_Track], [REZ1]," + 
				" [REZ2], [REZ3], [REZ4], [ODB_ID], [mena_ID]," +
				" [SERLTNUM], [WEIGHT], [TIMEFROM], [TIMETO], [LSTMod], [loginid], [CZ_Expirace_Track],[Expirace]" + 
				" ) VALUES ( " + 
				" @ITEMNMBR, @ITEMDESC, @ITEMCODE, @VNDITNUM, @CZ_CarKod," + 
				" @LOCNCODE, @SKL_ID, @QTY, @QTYPACK, @MJ," + 
				" @DMJ, @TAXRATE, @PRICE0, @PRICE1, @PRICE2," + 
				" @PRICE3, @PRICE4, @PRICE5, @CZ_SerNum_Track, @CZ_SerNum_Delka," + 
				" @CZ_Rez1_Track, @CZ_Rez2_Track, @CZ_Rez3_Track, @CZ_Rez4_Track, @REZ1," + 
				" @REZ2, @REZ3, @REZ4, @ODB_ID, @mena_ID," +
				" @SERLTNUM, @WEIGHT, @TIMEFROM, @TIMETO, @LSTMod, @loginid, @CZ_Expirace_Track, @Expirace" +
				" )";

			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTY", DbType = System.Data.DbType.Decimal, SourceColumn = "QTY", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DMJ", DbType = System.Data.DbType.String, SourceColumn = "DMJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TAXRATE", DbType = System.Data.DbType.Decimal, SourceColumn = "TAXRATE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE0", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE0", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE1", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE1", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE2", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE2", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE3", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE3", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE4", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE4", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE5", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE5", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SerNum_Delka", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez1_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez1_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez2_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez2_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez3_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez3_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez4_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez4_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, SourceColumn = "REZ1", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ2", DbType = System.Data.DbType.String, SourceColumn = "REZ2", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ3", DbType = System.Data.DbType.String, SourceColumn = "REZ3", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ4", DbType = System.Data.DbType.String, SourceColumn = "REZ4", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, SourceColumn = "ODB_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEFROM", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMEFROM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMETO", DbType = System.Data.DbType.DateTime, SourceColumn = "TIMETO", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, SourceColumn = "loginid", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Expirace_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Expirace_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace", SourceVersion = System.Data.DataRowVersion.Current });

		}

		private static void InitializeCommandSelect_FASK_ZASOBY(SqlCommand command)
		{
			command.CommandText = "SELECT * FROM " + Constants.Common.TABLE_FASK_ZASOBY;
		}


		#endregion
	}
}
