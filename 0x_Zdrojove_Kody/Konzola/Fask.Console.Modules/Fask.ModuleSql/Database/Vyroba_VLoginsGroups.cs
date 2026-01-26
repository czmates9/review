using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql.Database
{
    public class Vyroba_VLoginsGroups
    {
        internal static void DeleteByLoginID(string CS, string loginid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    using (SqlCommand comm = con.CreateCommand())
                    {
                        con.Open();
                        comm.CommandType = System.Data.CommandType.Text;
                        comm.CommandText = "DELETE from " + Fask.SQL.Constants.Common.TABLE_VLoginsGroups + " where loginid='" + loginid + "'";
                        int retunValue;
                        retunValue = comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #region delete group by id
        //internal static void DeleteByGoupID(string CS, string groupid)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(CS))
        //        {
        //            using (SqlCommand comm = con.CreateCommand())
        //            {
        //                con.Open();
        //                comm.CommandType = System.Data.CommandType.Text;
        //                comm.CommandText = "DELETE from " + Fask.SQL.Constants.Common.TABLE_VLoginsGroups + " where groupid='" + groupid + "'";
        //                int retunValue;
        //                retunValue = comm.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.ExceptionHandler2.Handle(ex);
        //        throw ex;
        //    }
        //}

        internal static void DeleteByGoupID(string CS, string groupid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();  // Otevření připojení

                    using (SqlCommand comm = con.CreateCommand())
                    {
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_Groups + " WHERE id = @groupid";

                        // Použití parametrizovaného dotazu pro ochranu proti SQL injection
                        comm.Parameters.Add(new SqlParameter
                        {
                            ParameterName = "@groupid",
                            DbType = DbType.String,
                            Value = groupid
                        });

                        int retunValue;
                        retunValue = comm.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Specifické ošetření SQL výjimek, které mohou nastat při práci s databází
                Logging.ExceptionHandler2.Handle(sqlEx);
                throw new ApplicationException("Chyba při provádění SQL dotazu.", sqlEx);
            }
            catch (Exception ex)
            {
                // Ostatní obecné výjimky
                Logging.ExceptionHandler2.Handle(ex);
                throw new ApplicationException("Došlo k neočekávané chybě.", ex);
            }
        } 
        #endregion



        internal static Fask.Interfaces.DataSets.Vyroba.VLoginsGroupsDataTable GetDataByLoginID(string CS, string LoginID)
        {
            Fask.Interfaces.DataSets.Vyroba.VLoginsGroupsDataTable dt = new Fask.Interfaces.DataSets.Vyroba.VLoginsGroupsDataTable();
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_VLoginsGroups;

                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        ada.Fill(dt);
                    }
                }
            }

            return dt;
        }

        internal static void Insert(string CS, string loginid, string groupid)
        {

            System.Data.SqlClient.SqlTransaction transaction = null;
            System.Data.SqlClient.SqlConnection conn = null;
            try
            {

                using (conn = new System.Data.SqlClient.SqlConnection(CS))
                {

                    conn.Open();

                    transaction = conn.BeginTransaction();

                    using (var commandInsert = conn.CreateCommand())
                    {
                        //command a parametry definice
                        commandInsert.CommandText = "INSERT INTO " + Fask.SQL.Constants.Common.TABLE_VLoginsGroups + 
                            " (" +
                            "[loginid], [groupid]" +
                            ") VALUES (" +
                            "@loginid, @groupid" +
                            ")";



                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@loginid", DbType = DbType.Int32, SourceColumn = "loginid", Value = loginid == null ? (object)DBNull.Value : loginid });
                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@groupid", DbType = DbType.String, SourceColumn = "groupid", Value = groupid == null ? (object)DBNull.Value : groupid });


                        commandInsert.Transaction = transaction;
                        commandInsert.ExecuteNonQuery();
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
        }
    }
}
