using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes
{
    public class DatabaseCentral
    {
        public static string ReturnSarze(string smenaID, string userID, string linkaID)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                sqlConn = new System.Data.SqlClient.SqlConnection(Logging.LogConfig.SqlConnectionStringGlobal);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                sqlComm.CommandText = "fask_vyroba_GetSarze";
                sqlComm.Parameters.AddWithValue("@smenaID", smenaID);
                sqlComm.Parameters.AddWithValue("@userID", userID);
                sqlComm.Parameters.AddWithValue("@linkaID", linkaID);

                sqlComm.Connection.Open();

                object o = sqlComm.ExecuteScalar();
                if (o is string)
                {
                    return (string)o;
                }
                else
                {
                    return string.Empty;
                }
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }
            }
        }

        public static bool ReturnID(string inID)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                sqlConn = new System.Data.SqlClient.SqlConnection(Logging.LogConfig.SqlConnectionStringGlobal);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.Text;
                //sqlComm.CommandText = "SELECT id FROM Fask_Logins WHERE " + "id = " + inID;
                //sqlComm.CommandText = "IF (EXISTS (SELECT id FROM Fask_Logins WHERE " + "id = " + inID + ")) RETURN 1 ELSE RETURN 0" ;

                sqlComm.CommandText = "Select * from FASK_vyroba_OverId(@id)";
                sqlComm.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id", inID));
                sqlComm.Connection.Open();


                object o = sqlComm.ExecuteScalar();

                if (String.IsNullOrEmpty((string)o))
                    return false;
                else
                {
                    string tmp = (string)o;
                    if (tmp.Trim() == inID)
                        return true;
                    else
                        return false;
                }



            }
            catch (Exception ex)
            {
                FASK.SledovaniVyroby.ErrorLog.Log.Write(ex.Message.ToString());
                //sql prikaz sa nevykonal tak to hodi throw heslo nenalezeno nebo se nepripojilo k serveru
                //System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                return false;
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }

            }
        }

        public static bool ReturnHeslo(string inHESLO,string inID)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                sqlConn = new System.Data.SqlClient.SqlConnection(Logging.LogConfig.SqlConnectionStringGlobal);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.Text;
                //sqlComm.CommandText = "SELECT psswd FROM Fask_Logins WHERE " + "psswd = " + inHESLO;
                //sqlComm.CommandText = "IF (EXISTS (SELECT psswd FROM Fask_Logins WHERE " + "psswd = " + inHESLO + ")) RETURN 1 ELSE RETURN 0" ;

                sqlComm.CommandText = "Select * from FASK_vyroba_OverHeslo(@id,@heslo)";
                sqlComm.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id", inID));
                sqlComm.Parameters.Add(new System.Data.SqlClient.SqlParameter("@heslo", inHESLO));
                sqlComm.Connection.Open();


                object o = sqlComm.ExecuteScalar();

                if (String.IsNullOrEmpty((string)o))
                    return false;
                else
                {
                    string tmp = (string)o;
                    if (tmp.Trim() == inHESLO)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                FASK.SledovaniVyroby.ErrorLog.Log.Write(ex.Message.ToString());
                //System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                //sql prikaz sa nevykonal tak to hodi throw heslo nenalezeno nebo se nepripojilo k serveru
                return false;
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }

            }
        }

    }
}
