using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModuleSql
{
    public partial class Provider :
        Fask.Console.Interfaces.Konzola.IKonzola2,
        Fask.Console.Interfaces.Konzola.IKonzola2_GetUzivatel,
        Fask.Console.Interfaces.Konzola.IKonzola2_GetUzivateleAOpravneni,
        Fask.Console.Interfaces.Konzola.IKonzola2_GetUzivatelskaOpravneni,
        Fask.Console.Interfaces.Konzola.IKonzola2_InsertUzivatelAOpravneni,
        Fask.Console.Interfaces.Konzola.IKonzola2_UpdateUzivatelAOpravneni,
        Fask.Console.Interfaces.Konzola.IKonzola2_DeleteUzivatelAOpravneni,
        Fask.Console.Interfaces.Parametry.IParametry2,
        Fask.Console.Interfaces.Parametry.IParametry2_ConnectionString
    {
        // TODO: pouzivat sql dataset pro inserty, updaty, ...
        //public string ConnectionString { get; set; }

        public Console.Interfaces.DataSets.Konzola.FASK_LoginsRow GetUzivatel(string userid)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Console.Interfaces.DataSets.Konzola dsKonzola = new Console.Interfaces.DataSets.Konzola();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText = "select * from " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_LOGINS + " where ID=@id";
                command.Parameters.AddWithValue("@id", userid);
                adapter.SelectCommand = command;
                adapter.Fill(dsKonzola.FASK_Logins);

                if (dsKonzola.FASK_Logins.Count > 0)
                    return dsKonzola.FASK_Logins.First();
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Console.Interfaces.DataSets.Konzola.FASK_Logins_AuthRow GetUzivatelskaOpravneni(string userid)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Fask.Console.Interfaces.DataSets.Konzola dsKonzola = new Console.Interfaces.DataSets.Konzola();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText = "select * from " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_LOGINS_AUTH + " where ID=@id";
                command.Parameters.AddWithValue("@id", userid);
                adapter.SelectCommand = command;
                adapter.Fill(dsKonzola.FASK_Logins_Auth);

                if (dsKonzola.FASK_Logins_Auth.Count > 0)
                    return dsKonzola.FASK_Logins_Auth.First();
                else
                    return null;
            }
            catch 
            {                
                throw;
            }
        }


        public Console.Interfaces.DataSets.Konzola GetUzivateleAOpravneni()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Console.Interfaces.DataSets.Konzola dsKonzola = new Console.Interfaces.DataSets.Konzola();

            try
            {                
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "select * from " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_LOGINS;
                adapter.SelectCommand = command;
                adapter.Fill(dsKonzola.FASK_Logins);

                command.CommandText = "select * from " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_LOGINS_AUTH;
                adapter.SelectCommand = command;
                adapter.Fill(dsKonzola.FASK_Logins_Auth);

                return dsKonzola;
            }
            catch
            {
                throw;
            }
        }


        public bool InsertUzivatelAOpravneni(Console.Interfaces.DataSets.Konzola.FASK_LoginsRow loginRow, Console.Interfaces.DataSets.Konzola.FASK_Logins_AuthRow authRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlCommand command2 = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd = "insert into " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_LOGINS + "(id, firstname, surname, psswd) VALUES(@id, @firstname, @surname, @psswd)";
                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@id", loginRow.id);
                command.Parameters.AddWithValue("@firstname", loginRow.firstname);
                command.Parameters.AddWithValue("@surname", loginRow.surname);
                command.Parameters.AddWithValue("@psswd", loginRow.psswd);
                
                command.ExecuteNonQuery();

                string insertAuth = "insert into " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_LOGINS_AUTH + "(id, ADM, opr_select, opr_insert, opr_edit, opr_delete) VALUES(@id, @ADM, @opr_select, @opr_insert, @opr_edit, @opr_delete)";
                command2 = new System.Data.SqlClient.SqlCommand(insertAuth, connection, trans);
                command2.Parameters.AddWithValue("@id", authRow.id);
                command2.Parameters.AddWithValue("@ADM", authRow.ADM);
                command2.Parameters.AddWithValue("@opr_select", authRow.Isopr_selectNull() ? (object)DBNull.Value : authRow.opr_select);
                command2.Parameters.AddWithValue("@opr_insert", authRow.Isopr_insertNull() ? (object)DBNull.Value : authRow.opr_insert);
                command2.Parameters.AddWithValue("@opr_edit", authRow.Isopr_editNull() ? (object)DBNull.Value : authRow.opr_edit);
                command2.Parameters.AddWithValue("@opr_delete", authRow.Isopr_deleteNull() ? (object)DBNull.Value : authRow.opr_delete);

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

        public bool DeleteUzivatelAOpravneni(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlCommand command2 = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string deleteCmd = "delete from " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_LOGINS + " where id=@id";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

                string deleteAuth = "delete from " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_LOGINS_AUTH + " where id=@id";
                command2 = new System.Data.SqlClient.SqlCommand(deleteAuth, connection, trans);
                command2.Parameters.AddWithValue("@id", id);
                
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


        public bool UpdateUzivatelAOpravneni(Console.Interfaces.DataSets.Konzola.FASK_LoginsRow loginRow, Console.Interfaces.DataSets.Konzola.FASK_Logins_AuthRow authRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlCommand command2 = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                string insertCmd = "update " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_LOGINS + " set firstname=@firstname, surname=@surname, psswd=@psswd where id=@id";
                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@id", loginRow.id);
                command.Parameters.AddWithValue("@firstname", loginRow.firstname);
                command.Parameters.AddWithValue("@surname", loginRow.surname);
                command.Parameters.AddWithValue("@psswd", loginRow.psswd);

                command.ExecuteNonQuery();

                string insertAuth = "update " + Fask.Console.Interfaces.Constants.Tables.TABLE_FASK_LOGINS_AUTH + " set ADM=@ADM, opr_select=@opr_select, opr_insert=@opr_insert, opr_edit=@opr_edit, opr_delete=@opr_delete where id=@id";
                command2 = new System.Data.SqlClient.SqlCommand(insertAuth, connection, trans);
                command2.Parameters.AddWithValue("@id", authRow.id);
                command2.Parameters.AddWithValue("@ADM", authRow.ADM);
                command2.Parameters.AddWithValue("@opr_select", authRow.Isopr_selectNull() ? (object)DBNull.Value : authRow.opr_select);
                command2.Parameters.AddWithValue("@opr_insert", authRow.Isopr_insertNull() ? (object)DBNull.Value : authRow.opr_insert);
                command2.Parameters.AddWithValue("@opr_edit", authRow.Isopr_editNull() ? (object)DBNull.Value : authRow.opr_edit);
                command2.Parameters.AddWithValue("@opr_delete", authRow.Isopr_deleteNull() ? (object)DBNull.Value : authRow.opr_delete);

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
    }
}
