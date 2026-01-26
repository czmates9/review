using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql.Database
{
    public class Vydej
    {
        public int Update_row(Fask.Interfaces.DataSets.Vydej.CZMST_SERow row,string ConnectionString)
        {

            int pocet = 0;
            System.Data.SqlClient.SqlTransaction transaction = null;
            System.Data.SqlClient.SqlConnection conn = null;
            //string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            try
            {

                using (conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {

                    conn.Open();

                    transaction = conn.BeginTransaction();

                    using (var commandInsert = conn.CreateCommand())
                    {
                        //command a parametry definice
                        commandInsert.CommandText =
                            @" UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST_SE +
                            " SET" +
                            " CZ_Doslo = @CZ_Doslo " +
                            " WHERE  DEX_ROW_ID = @DEX_ROW_ID" +
                            " AND SOPNUMBE = @SOPNUMBE" +
                            " AND CountEntries = @CountEntries" +
                            " AND ITEMNMBR = @ITEMNMBR "
                            ;

                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@CZ_Doslo", DbType = DbType.Byte, SourceColumn = "CZ_Doslo", Value = 201 });
                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@DEX_ROW_ID", DbType = DbType.Int32, SourceColumn = "DEX_ROW_ID", Value = row.DEX_ROW_ID });
                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", Value = row.SOPNUMBE });
                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = row.CountEntries });
                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", Value = row.ITEMNMBR });


                        commandInsert.Transaction = transaction;
                       pocet = commandInsert.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }

            }
            catch (Exception ex)
            {
                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch (Exception exTransaction)
                {
                    throw exTransaction;
                }

                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return pocet;
        }
   
    }
}
