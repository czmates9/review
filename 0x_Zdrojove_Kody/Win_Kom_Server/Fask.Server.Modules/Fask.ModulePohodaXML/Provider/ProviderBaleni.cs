using Fask.Server.Interfaces.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.SQL
{
    public partial class Provider : Fask.Server.Interfaces.Baleni.IBaleni
    {
        public StatusBaleni BaleniDataCommit(string userid, byte terminalid, string objednavkacislo, int balikcislo, DateTime printedtime)
        {
            StatusBaleni sb = new StatusBaleni();
            //OleDbConnection conn = null;
            //OleDbDataAdapter adapter = null;
            //OleDbCommand command = null;

            try
            {
                //conn = new OleDbConnection();
                //conn.ConnectionString = Properties.Settings.Default.BaleniConnectionString;
                //adapter = new OleDbDataAdapter();
                //command = new OleDbCommand();
                //command.CommandText = Properties.Settings.Default.BaleniDataCommit; //"mtj_fask_Baleni_Data_Commit";
                //command.CommandType = CommandType.StoredProcedure;
                //conn.Open();
                //command.Connection = conn;

                //OleDbParameter parameterReturn = command.CreateParameter();
                //parameterReturn.ParameterName = "@RETURN_VALUE";
                //parameterReturn.DbType = DbType.Int32;
                //parameterReturn.Direction = System.Data.ParameterDirection.ReturnValue;
                //command.Parameters.Add(parameterReturn);


                //if (Properties.Settings.Default.BaleniDataCommit_paramName1.Length > 0)
                //    command.Parameters.AddWithValue(Properties.Settings.Default.BaleniDataCommit_paramName1, objednavkacislo);
                //if (Properties.Settings.Default.BaleniDataCommit_paramName2.Length > 0)
                //    command.Parameters.AddWithValue(Properties.Settings.Default.BaleniDataCommit_paramName2, balikcislo);
                //if (Properties.Settings.Default.BaleniDataCommit_paramName3.Length > 0)
                //    command.Parameters.AddWithValue(Properties.Settings.Default.BaleniDataCommit_paramName3, userid);
                //if (Properties.Settings.Default.BaleniDataCommit_paramName4.Length > 0)
                //    command.Parameters.AddWithValue(Properties.Settings.Default.BaleniDataCommit_paramName4, terminalid.ToString());    // vyzkouset !!
                //if (Properties.Settings.Default.BaleniDataCommit_paramName5.Length > 0)
                //    command.Parameters.AddWithValue(Properties.Settings.Default.BaleniDataCommit_paramName5, printedtime);

                //OleDbParameter outmessage = command.CreateParameter();
                //outmessage.ParameterName = Properties.Settings.Default.BaleniDataCommit_paramName6;
                //outmessage.DbType = DbType.String;
                //outmessage.Size = -1;  // Je to nutne ?
                //outmessage.Direction = System.Data.ParameterDirection.Output;
                //command.Parameters.Add(outmessage);
                //command.ExecuteNonQuery();
                //int result = (int)parameterReturn.Value;

                //sb.Result = (STATUS)Enum.Parse(typeof(STATUS), result.ToString());

                //// nastala chyba
                //if (sb.Result == STATUS.ERROR)
                //{
                //    if (outmessage.Value == DBNull.Value)
                //        sb.Message = "Neznámá chyba.";
                //    else
                //        sb.Message = (string)outmessage.Value;
                //}

                sb.Message = new NotImplementedException().Message;
                sb.Result = STATUS.ERROR;

                return sb;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                //if ((conn != null) && (conn.State & ConnectionState.Open) == ConnectionState.Open)
                //    conn.Close();
            }
        }

        public DataSet GetBaleniData(string objednavkacislo, out int balikcislo)
        {
            throw new NotImplementedException();
            //DataSet ds = new DataSet();
            //OleDbConnection conn = null;
            //OleDbDataAdapter adapter = null;
            //OleDbCommand command = null;

            //try
            //{
                //conn = new OleDbConnection();
                //conn.ConnectionString = Properties.Settings.Default.BaleniConnectionString;
                //adapter = new OleDbDataAdapter();
                //command = new OleDbCommand();
                //command.CommandText = Properties.Settings.Default.BaleniDetailItem;     //"mtj_fask_Baleni_Data";
                //command.CommandType = CommandType.StoredProcedure;
                //command.Connection = conn;

                //// command.Parameters.Add("objednavkacislo", objednavkacislo);
                //command.Parameters.AddWithValue(Properties.Settings.Default.BaleniDetailItem_paramName1, objednavkacislo);

                //OleDbParameter outparam = command.CreateParameter();
                //outparam.ParameterName = Properties.Settings.Default.BaleniDetailItem_paramName2;   // "balikcislo";
                //outparam.DbType = DbType.Int32;
                ////parameterText.Value = string.Empty;
                //outparam.Direction = System.Data.ParameterDirection.Output;
                //command.Parameters.Add(outparam);
                //adapter.SelectCommand = command;
                //adapter.Fill(ds);

                //balikcislo = (int)outparam.Value;
                //return ds;
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(ex);
            //    throw ex;
            //}
        }
    }
}
