using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.Module.Pohoda.I_Tec.Routines
{
    class AfterProcessAction
    {
        public static void Execute(string connstring, string table, int countentries, string aDP_Action, string aDP_Action_P1, string aDP_Action_P2, int AfterDataProcessed_Action_CommandTimeout)
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                adpaconnection = new System.Data.SqlClient.SqlConnection(connstring);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand(aDP_Action);
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = AfterDataProcessed_Action_CommandTimeout;

                adpacommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(aDP_Action_P1, SqlDbType.NVarChar, 20));
                adpacommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(aDP_Action_P2, SqlDbType.NVarChar, 20));

                ((IDataParameter)adpacommand.Parameters[aDP_Action_P1]).Value = table;
                ((IDataParameter)adpacommand.Parameters[aDP_Action_P2]).Value = countentries.ToString();

                adpacommand.Connection = adpaconnection;

                adpaconnection.Open();
                adpacommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }
        }

        public static void Execute(string connstring, string table, Guid id, string aDP_Action, string aDP_Action_P1, string aDP_Action_P2, int AfterDataProcessed_Action_CommandTimeout)
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                adpaconnection = new System.Data.SqlClient.SqlConnection(connstring);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand(aDP_Action);
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = AfterDataProcessed_Action_CommandTimeout;

                adpacommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(aDP_Action_P1, SqlDbType.NVarChar, 20));
                adpacommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(aDP_Action_P2, SqlDbType.UniqueIdentifier));

                ((IDataParameter)adpacommand.Parameters[aDP_Action_P1]).Value = table;
                ((IDataParameter)adpacommand.Parameters[aDP_Action_P2]).Value = id;

                adpacommand.Connection = adpaconnection;

                adpaconnection.Open();
                adpacommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }
        }

        public static void ExecuteInTransaction(System.Data.SqlClient.SqlConnection sqlconn, System.Data.SqlClient.SqlTransaction sqltrans, string table, Guid id, string aDP_Action, string aDP_Action_P1, string aDP_Action_P2, int AfterDataProcessed_Action_CommandTimeout)
        {
            //System.Data.SqlClient.SqlConnection adpaconnection = null;

            DateTime start, end;
            start = DateTime.Now;

            try
            {
                //adpaconnection = new System.Data.SqlClient.SqlConnection(connstring);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand(aDP_Action);
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = AfterDataProcessed_Action_CommandTimeout;

                adpacommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(aDP_Action_P1, SqlDbType.NVarChar, 20));
                adpacommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(aDP_Action_P2, SqlDbType.UniqueIdentifier));

                ((IDataParameter)adpacommand.Parameters[aDP_Action_P1]).Value = table;
                ((IDataParameter)adpacommand.Parameters[aDP_Action_P2]).Value = id;

                adpacommand.Connection = sqlconn;

                bool connopenedbefore = (sqlconn.State & ConnectionState.Open) == ConnectionState.Open;
                if (!connopenedbefore)
                    sqlconn.Open();

                adpacommand.Transaction = sqltrans;
                //adpaconnection.Open();
                adpacommand.ExecuteNonQuery();

                if (!connopenedbefore && ((sqlconn.State & ConnectionState.Open) == ConnectionState.Open))
                    sqlconn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                end = DateTime.Now;
                //Debug.WriteLine("Seconds: " + (end - start).TotalSeconds);

				// \TODO : kvuli transakci neukoncovat spojeni ...
                //if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                //    adpaconnection.Close();
            }
        }
    }
}
