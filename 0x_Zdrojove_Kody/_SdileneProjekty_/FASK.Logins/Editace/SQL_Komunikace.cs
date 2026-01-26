using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FASK.Logins.Editace
{
    public class SQL_Komunikace : IKomunikace
    {

        private string _ConnectionString;

        public SQL_Komunikace(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

         public DataSets.Pristupy GetFiltrovanyLogins(FASK.Logins.Editace.Filtry_Login_A Filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                DataSets.Pristupy dsLogins = new DataSets.Pristupy();

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(_ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "SELECT * from " + FASK.Logins.Editace.Constants.TABLE_FASK_LOGINS + " WHERE 1=1 ";


                if (!string.IsNullOrEmpty(Filtr.USERID))
                {
                    command.CommandText += " AND USERID=@USERID";
                    command.Parameters.AddWithValue("@USERID", Filtr.USERID);
                }

                if (!string.IsNullOrEmpty(Filtr.SurName))
                {
                    command.CommandText += " AND surname=@surname";
                    command.Parameters.AddWithValue("@surname", Filtr.SurName);
                }

                if (!string.IsNullOrEmpty(Filtr.FirstName))
                {
                    command.CommandText += " AND firstname=@firstname";
                    command.Parameters.AddWithValue("@firstname", Filtr.FirstName);
                }

                if (!string.IsNullOrEmpty(Filtr.RFID))
                {
                    command.CommandText += " AND RFID=@RFID";
                    command.Parameters.AddWithValue("@RFID", Filtr.RFID);
                }

                if (Filtr.dtp_Create)
                {
                    command.CommandText += "AND CREATED >= @CREATED ";
                    command.Parameters.AddWithValue("@CREATED", Filtr.dtp_Create_Value);
                }

                if (Filtr.dtp_ValidFrom == true && Filtr.dtp_ValidTo == true)
                {
                    command.CommandText += "AND ( VALIDFROM <= @VALIDTO AND VALIDTO >= @VALIDFROM ) ";
                    command.Parameters.AddWithValue("@VALIDFROM", Filtr.dtp_ValidFrom_Value);
                    command.Parameters.AddWithValue("@VALIDTO", Filtr.dtp_ValidTo_Value);
                }
                else if (Filtr.dtp_ValidTo == false && Filtr.dtp_ValidFrom == true)
                {
                    command.CommandText += "AND VALIDTO >= @VALIDFROM ";
                    command.Parameters.AddWithValue("@VALIDFROM", Filtr.dtp_ValidFrom_Value);
                }
                else if (Filtr.dtp_ValidTo == true && Filtr.dtp_ValidFrom == false)
                {
                    command.CommandText += "AND VALIDFROM <= @VALIDTO ";
                    command.Parameters.AddWithValue("@VALIDTO", Filtr.dtp_ValidTo_Value);
                }

                adapter.SelectCommand = command;
                adapter.Fill(dsLogins.FASK_Logins);

                return dsLogins;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSets.Pristupy GetLogins_Auth(FASK.Logins.Editace.Filtry_Auth_A filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            DataSets.Pristupy ds_Auth = new DataSets.Pristupy();
            try
            {
                
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(_ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM " + FASK.Logins.Editace.Constants.TABLE_FASK_LOGINS_AUTH + " WHERE 1=1";

                if (!string.IsNullOrEmpty(filtr.USERID))
                {
                    command.CommandText += " AND USERID='" + filtr.USERID + "'";
                }

                if (!string.IsNullOrEmpty(filtr.IDAgendy))
                {
                    command.CommandText += " AND AGENDAID like '" + filtr.IDAgendy + "%'";
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds_Auth.FASK_Logins_Auth);

                return ds_Auth;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete_Login(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlCommand command2 = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(_ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string deleteCmd = "DELETE from " + FASK.Logins.Editace.Constants.TABLE_FASK_LOGINS + " where USERID=@USERID";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@USERID", id);

                command.ExecuteNonQuery();

                string deleteAuth = "DELETE from " + FASK.Logins.Editace.Constants.TABLE_FASK_LOGINS_AUTH + " where USERID=@USERID";
                command2 = new System.Data.SqlClient.SqlCommand(deleteAuth, connection, trans);
                command2.Parameters.AddWithValue("@USERID", id);

                command2.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool Delete_Auth(int DEX_ROW_ID)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command2 = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(_ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);


                string deleteAuth = "DELETE from " + FASK.Logins.Editace.Constants.TABLE_FASK_LOGINS_AUTH + " where DEX_ROW_ID=@DEX_ROW_ID";
                command2 = new System.Data.SqlClient.SqlCommand(deleteAuth, connection, trans);
                command2.Parameters.AddWithValue("@DEX_ROW_ID", DEX_ROW_ID);

                command2.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool Update_Login_Row(DataSets.Pristupy.FASK_LoginsRow loginsrow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(_ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd = "UPDATE " + FASK.Logins.Editace.Constants.TABLE_FASK_LOGINS + " SET firstname=@firstname, surname=@surname, psswd=@psswd where USERID=@USERID";
                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@USERID", loginsrow.USERID);
                command.Parameters.AddWithValue("@firstname", loginsrow.firstname);
                command.Parameters.AddWithValue("@surname", loginsrow.surname);
                command.Parameters.AddWithValue("@psswd", loginsrow.psswd);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool Insert_Login(string USERID, string firstname, string surname, string psswd)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(_ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd = "INSERT into " + FASK.Logins.Editace.Constants.TABLE_FASK_LOGINS + "(USERID, firstname, surname, psswd) VALUES(@USERID, @firstname, @surname, @psswd)";
                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@USERID", USERID);
                command.Parameters.AddWithValue("@firstname", firstname);
                command.Parameters.AddWithValue("@surname", surname);
                command.Parameters.AddWithValue("@psswd", psswd);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public DataSets.Pristupy GetFASK_AGENDA_Filtrovana(Filtry_Agenda_A filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            DataSets.Pristupy ds_Auth = new DataSets.Pristupy();
            try
            {

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(_ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM " + FASK.Logins.Editace.Constants.TABLE_FASK_AGENDA + " WHERE 1=1";


                if (!string.IsNullOrEmpty(filtr.AGENDAID))
                {
                    command.CommandText += " AND AGENDAID like '" + filtr.AGENDAID + "%'";
                }

                if (!string.IsNullOrEmpty(filtr.NAME))
                {
                    command.CommandText += " AND NAME like '%" + filtr.NAME + "%'";
                }

                if (!string.IsNullOrEmpty(filtr.DESCIPTION))
                {
                    command.CommandText += " AND DESCIPTION like '%" + filtr.DESCIPTION + "%'";
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds_Auth.FASK_AGENDA);

                return ds_Auth;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert_Auth(string USERID, string AGENDAID)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(_ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd = "INSERT into " + FASK.Logins.Editace.Constants.TABLE_FASK_LOGINS_AUTH + "(USERID, AGENDAID, AUTH) VALUES(@USERID, @AGENDAID, @AUTH)";
                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@USERID", USERID);
                command.Parameters.AddWithValue("@AGENDAID", AGENDAID);
                command.Parameters.AddWithValue("@AUTH", 0);

                command.ExecuteNonQuery();

                if (trans != null)
                    trans.Commit();

                return true;
            }
            catch
            {
                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch { }
                throw;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool isExist_Auth(string USERID, string AGENDAID)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            DataSets.Pristupy ds_Auth = new DataSets.Pristupy();
            try
            {

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(_ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM " + FASK.Logins.Editace.Constants.TABLE_FASK_LOGINS_AUTH+ " WHERE 1=1";


                command.CommandText += " AND USERID = '" + USERID + "'";
                command.CommandText += " AND AGENDAID = '" + AGENDAID + "'";
                

                adapter.SelectCommand = command;
                adapter.Fill(ds_Auth.FASK_Logins_Auth);

                if ((ds_Auth.FASK_Logins_Auth != null) && (ds_Auth.FASK_Logins_Auth.Count > 0))
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
