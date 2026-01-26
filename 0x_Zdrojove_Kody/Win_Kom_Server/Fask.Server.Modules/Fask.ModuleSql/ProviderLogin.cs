using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using static Fask.Server.Interfaces.DataSets.Pristupy;
using Fask.Server.Interfaces.DataSets;
using Fask.SQL.Constants;

namespace Fask.ModuleSql
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část Uživatele.
	/// </summary>
    public partial class Provider : Fask.Server.Interfaces.Login.ILogin, Fask.Server.Interfaces.Login.ILogin_Commans , Fask.Server.Interfaces.Login.ILogin_Komunikace
    {
        #region ILogin Members

		/// <summary>
		/// Metoda pro online oveřeni uživatele
		/// </summary>
		/// <param name="uzivatel">Uživatel</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="hash">Hash</param>
		/// <param name="uzivatelID">Reference na ID uživatele</param>
		/// <returns>True-OK, False- chyba</returns>
        public bool Login_OnlineLogin(Fask.Server.Interfaces.Classes.User uzivatel, Fask.Server.Interfaces.Classes.Terminal terminal, string hash, ref int uzivatelID)
        {
			throw new NotImplementedException();

        }

		/// <summary>
		/// Metoda pro dotaženi uživatele z Databaze
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="so">reference na StatusObjekt</param>
		/// <returns>Dataset Uzivatele naplnen uživatelama</returns>
        public Fask.DataSets.Uzivatele Login_GetKatalogUzivatele(Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.Server.Interfaces.Classes.StatusObject so)
        {
			throw new NotImplementedException();

        }

		/// <summary>
		/// Metoda pro vypočet HASH
		/// </summary>
		/// <param name="uzivatel">Uživatel</param>
		/// <param name="terminal">Terminal</param>
		/// <returns>Vypočitany HASH</returns>
        public string Login_GetHash(Fask.Server.Interfaces.Classes.User uzivatel, Fask.Server.Interfaces.Classes.Terminal terminal)
        {
            throw new NotImplementedException();
        }

        public Pristupy GetViewData(string USERID)
        {
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            Pristupy prava = new Pristupy();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "Select * from FASK_Logins_View_Prava"
                //+ " Where USERID=@USERID AND psswd = @psswd";
                + " Where USERID=@USERID ";
                da.SelectCommand.Parameters.AddWithValue("@USERID", USERID);
                //da.SelectCommand.Parameters.AddWithValue("@psswd", Heslo);

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(connectionString);

                da.Fill(prava.FASK_Logins_View_Prava);
            }
            catch (Exception ex)
            {
                //Fask.Logging.ExceptionHandler2.Handle(ex);
                //Fask.Logging.ExceptionHandler2.Handle(prava);
                throw ex;
                //return null;
            }

            return prava;
        }

        public Pristupy GetLikeAgednaID(string AgendaID)
        {
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            Pristupy prava = new Pristupy();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT L.* FROM FASK_Logins as L left join FASK_Logins_Auth as A ON L.USERID = A.USERID "
                + " WHERE A.AGENDAID LIKE  '" + AgendaID + "%' OR AGENDAID='_'"
                + " Group by L.USERID, L.firstname, L.surname , L.psswd, L.CREATED, L.VALIDFROM, L.VALIDTO, L.RFID ";

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(connectionString);

                da.Fill(prava.FASK_Logins);
            }
            catch (Exception ex)
            {
                //Fask.Logging.ExceptionHandler2.Handle(ex);
                //Fask.Logging.ExceptionHandler2.Handle(prava);
                throw ex;
                //return null;
            }

            return prava;
        }

        public Pristupy GetAgednaID(string AgendaID)
        {
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            Pristupy prava = new Pristupy();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT L.* FROM FASK_Logins as L left join FASK_Logins_Auth as A ON L.USERID = A.USERID "
                + " WHERE A.AGENDAID =  '" + AgendaID + "'"
                + " Group by L.USERID, L.firstname, L.surname , L.psswd, L.CREATED, L.VALIDFROM, L.VALIDTO, L.RFID ";

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(connectionString);

                da.Fill(prava.FASK_Logins);
            }
            catch (Exception ex)
            {
                //Fask.Logging.ExceptionHandler2.Handle(ex);
                //Fask.Logging.ExceptionHandler2.Handle(prava);
                throw ex;
                //return null;
            }

            return prava;
        }

        public FASK_LoginsDataTable GetLoginsByID(string AgendaID)
        {
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            Pristupy.FASK_LoginsDataTable prava = new Pristupy.FASK_LoginsDataTable();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT * FROM FASK_Logins "
                + " WHERE USERID =  '" + AgendaID + "'";

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(connectionString);

                da.Fill(prava);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return prava;
        }

        public FASK_LoginsDataTable GetLogins()
        {
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            FASK_LoginsDataTable prava = new FASK_LoginsDataTable();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;

                da.SelectCommand.CommandText = "SELECT * FROM FASK_Logins ";
                //+ " WHERE AGENDAID =  '" + AgendaID + "'";

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(connectionString);

                da.Fill(prava);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return prava;

        }

        public FASK_LoginsDataTable GetOverLogins(string USERID, string PWD, string AGENDA)
        {
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            Pristupy.FASK_LoginsDataTable prava = new Pristupy.FASK_LoginsDataTable();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand();
                da.SelectCommand.CommandType = System.Data.CommandType.Text;


                da.SelectCommand.CommandText = "SELECT L.* FROM FASK_Logins as L left join FASK_Logins_Auth as A ON L.USERID = A.USERID ";
                da.SelectCommand.CommandText += " WHERE  1=1";
                da.SelectCommand.CommandText += " AND L.USERID = '" + USERID + "'";
                da.SelectCommand.CommandText += " AND L.PSSWD ='" + PWD + "'";

                if (!string.IsNullOrEmpty(AGENDA))
                {
                    da.SelectCommand.CommandText += " AND A.AGENDAID LIKE '" + AGENDA + "%'";
                }

                da.SelectCommand.CommandText += " Group by L.USERID, L.firstname, L.surname , L.psswd, L.CREATED, L.VALIDFROM, L.VALIDTO, L.RFID ";

                da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(connectionString);

                da.Fill(prava);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return prava;
        }

        public Pristupy GetFiltrovanyLogins(Fask.WEBAPI.API_BusinessObjects.Filtry_Login Filtr)
        {
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Pristupy dsLogins = new Pristupy();

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(connectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "SELECT * from " + Common.TABLE_FASK_LOGINS + " WHERE 1=1 ";


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

        public Pristupy GetLogins_Auth(Fask.WEBAPI.API_BusinessObjects.Filtry_Auth filtr)
        {
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Pristupy ds_Auth = new Pristupy();
            try
            {

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(connectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM " + Common.TABLE_FASK_LOGINS_AUTH + " WHERE 1=1";

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete_Login(string id)
        {
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlCommand command2 = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(connectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string deleteCmd = "DELETE from " + Common.TABLE_FASK_LOGINS + " where USERID=@USERID";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@USERID", id);

                command.ExecuteNonQuery();

                string deleteAuth = "DELETE from " + Common.TABLE_FASK_LOGINS_AUTH + " where USERID=@USERID";
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
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command2 = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(connectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);


                string deleteAuth = "DELETE from " + Common.TABLE_FASK_LOGINS_AUTH + " where DEX_ROW_ID=@DEX_ROW_ID";
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

        public bool Update_Login_Row(FASK_LoginsRow loginsrow)
        {
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(connectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd = "UPDATE " + Common.TABLE_FASK_LOGINS + " SET firstname=@firstname, surname=@surname, psswd=@psswd where USERID=@USERID";
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
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
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
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(connectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd = "INSERT into " + Common.TABLE_FASK_LOGINS + "(USERID, firstname, surname, psswd) VALUES(@USERID, @firstname, @surname, @psswd)";
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

        public Pristupy GetFASK_AGENDA_Filtrovana(Fask.WEBAPI.API_BusinessObjects.Filtry_Agenda filtr)
        {
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Pristupy ds_Auth = new Pristupy();
            try
            {

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(connectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM " + Common.TABLE_FASK_AGENDA + " WHERE 1=1";


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
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(connectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd = "INSERT into " + Common.TABLE_FASK_LOGINS_AUTH + "(USERID, AGENDAID, AUTH) VALUES(@USERID, @AGENDAID, @AUTH)";
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
            Globals.LoadConfiguration();
            string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Pristupy ds_Auth = new Pristupy();
            try
            {

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(connectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM " + Common.TABLE_FASK_LOGINS_AUTH + " WHERE 1=1";


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

        #endregion


    }
}
