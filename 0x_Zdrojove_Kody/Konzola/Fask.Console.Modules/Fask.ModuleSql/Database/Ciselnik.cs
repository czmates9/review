using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql.Database
{
    public class Ciselnik
    {
        /// <summary>
        /// Jedná se o univerzalní metodu pro naplneni datatable z databaze
        /// </summary>
        /// <param name="CS">ConnectionString</param>
        /// <param name="script">Vykonaný script</param>
        /// <param name="dt">tabulka pro naplneni</param>
        public void Fill_Universal(string CS, string script, DataTable dt)
        {
            SqlConnection conn = null;
            SqlCommand comm = null;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = script;
                    comm.CommandType = CommandType.Text;
                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        ada.Fill(dt);

                    }
                }
            }
        }




		public static int Delete_CZMST093(SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				using (SqlCommand comm = connection.CreateCommand())
				{
					comm.Transaction = trans;
					comm.CommandType = System.Data.CommandType.Text;
					comm.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_CZMST093;

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

		public static int Delete_ByID_CZMST093(SqlConnection connection, SqlTransaction trans, string ID)
		{
			try
			{
				using (SqlCommand comm = connection.CreateCommand())
				{
					comm.Transaction = trans;
					comm.CommandType = System.Data.CommandType.Text;
					comm.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_CZMST093;
					comm.CommandText += " WHERE (skl_id = '" + ID + "')";

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




		public static int Update_CZMST093(object data, SqlConnection connection, SqlTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_CZMST093(commandInsert);
					InitializeCommandSelect_CZMST093(commandSelect);

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

		private static void InitializeCommandInsert_CZMST093(SqlCommand command)
		{
			command.CommandText = " SET IDENTITY_INSERT " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " ON ";


			command.CommandText = "INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " (" +
				" skl_id, skl_desc, skl_typ, skl_carcode " +
				" ) VALUES (" +
				" @skl_id, @skl_desc, @skl_typ, @skl_carcode " +
				" )";

			command.CommandText += " SET IDENTITY_INSERT " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " OFF ";


			command.Parameters.Add(new SqlParameter() { ParameterName = "@skl_id", DbType = System.Data.DbType.String, SourceColumn = "skl_id", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@skl_desc", DbType = System.Data.DbType.String, SourceColumn = "skl_desc", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@skl_typ", DbType = System.Data.DbType.String, SourceColumn = "skl_typ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@skl_carcode", DbType = System.Data.DbType.String, SourceColumn = "skl_carcode", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SqlParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = System.Data.DataRowVersion.Current });
		}



		private static void InitializeCommandSelect_CZMST093(SqlCommand command)
		{
			command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST093;
		}


		#endregion


	}
}
