using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Fask.Logging;
using Fask.MST_W.Forms;
using System.Windows.Forms;
using Fask.MST_W;
using Fask.MST_W.Online.BYZNYS;
using Fask.MST_W.Online.BYZNYS.DatabaseOnlineTableAdapters;

namespace Fask.MST_W.BYZNYS
{
    public class Database
    {

        public static string ONL_BWSCANNER_NOVY_EAN(int klic_ma, string eankod, string dmj, int? prepocet1, int? prepocet2, int? klic_odb)
        {
            System.Data.SqlClient.SqlCommand sqlcommand = null;
            string transactionname = "terminal_" + MST_Global.TerminalID + "_onl_novy_ean_";
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                sqlcommand = new System.Data.SqlClient.SqlCommand();
                sqlcommand.Connection = new System.Data.SqlClient.SqlConnection(Settings.Online_BYZNYS_ConnectionString);
                sqlcommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlcommand.CommandText = "ONL_BWSCANNER_NOVY_EAN";
                sqlcommand.Parameters.Add("@klic_ma", klic_ma);
                sqlcommand.Parameters.Add("@eankod", eankod);
                if (prepocet1.HasValue && prepocet2.HasValue && !String.IsNullOrEmpty(dmj))
                {
                    sqlcommand.Parameters.Add("@dmj", dmj);
                    sqlcommand.Parameters.Add("@prepocet1", prepocet1.Value);
                    sqlcommand.Parameters.Add("@prepocet2", prepocet2.Value);
                }

                if (klic_odb.HasValue)
                {
                    sqlcommand.Parameters.Add("@klic_odb", klic_odb.Value);
                }

                sqlcommand.CommandTimeout = Settings.Online_BYZNYS_CommandTimeout;

                sqlcommand.Connection.Open();
                //sqlcommand.Transaction = sqlcommand.Connection.BeginTransaction(transactionname);

                //sqlcommand.Prepare();

                string status = Convert.ToString(sqlcommand.ExecuteScalar());

                if (sqlcommand.Transaction != null)
                    sqlcommand.Transaction.Commit();

                return status; //status: ok!=neuspech, ok=uspech
            }
            catch (Exception ex)
            {
                string msg = string.Empty;

                Cursor.Current = Cursors.Default;
                if (ex is System.Data.SqlClient.SqlException)
                {
                    Fask.Logging.Log.Write(msg = ((SqlException)ex).Message, ((SqlException)ex).Procedure);
                }
                else
                {
                    msg = ex.Message;
                    Fask.Logging.Log.Write(ex);
                }

                try
                {
                    if (sqlcommand.Transaction != null)
                        sqlcommand.Transaction.Rollback(transactionname);
                }
                catch (Exception extrans)
                {
                    Fask.Logging.Log.Write(extrans);
                }

                return msg;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                if (sqlcommand != null && sqlcommand.Connection.State == System.Data.ConnectionState.Open)
                    sqlcommand.Connection.Close();                
            }
        }
    
        public static DatabaseOnline.ONL_PARTNERRow ONL_PARTNER(int typ, string str)
        {
            DatabaseOnline.ONL_PARTNERDataTable tbl_onl_partner = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ONL_PARTNERTableAdapter ta_onl_partner = new ONL_PARTNERTableAdapter();
                ta_onl_partner.Connection.ConnectionString = Settings.Online_BYZNYS_ConnectionString;

                tbl_onl_partner = ta_onl_partner.GetData(typ, str);

                return tbl_onl_partner.Count == 0 ? null : tbl_onl_partner[0];
            }
            catch (SqlException sqlex)
            {
                Cursor.Current = Cursors.Default;
                Fask.Logging.Log.Write(sqlex.Message, sqlex.Procedure);
                return null;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Fask.Logging.Log.Write(ex);
                return null;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

    }
}
