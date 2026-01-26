using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider : 
        Fask.Console.Interfaces.Ciselniky.IUzivatele2,
        Fask.Console.Interfaces.Ciselniky.IUzivatele2_DeleteUzivatel,
        Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetFiltrovaneUzivatele,
        Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetUzivatelByID,
        Fask.Console.Interfaces.Ciselniky.IUzivatele2_GetUzivatele,
        Fask.Console.Interfaces.Ciselniky.IUzivatele2_InsertUzivatel,
        Fask.Console.Interfaces.Ciselniky.IUzivatele2_UpdateUzivatel
    {
        #region IUzivatele2

        #region IUzivatele2_DeleteUzivatel Members

        public bool DeleteUzivatel(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals.ConnectionString);
                connection.Open();

                Pohoda_DataSets.UzivateleTableAdapters.CZMSTPWDTableAdapter ta_uzivatele = new Pohoda_DataSets.UzivateleTableAdapters.CZMSTPWDTableAdapter();
                ta_uzivatele.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_uzivatele.MyTransaction = trans;

                ta_uzivatele.Delete(Convert.ToInt32(id));

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

        #endregion

        #region IUzivatele2_GetFiltrovaneUzivatele Members

        public Console.Interfaces.DataSets.Uzivatele GetFiltrovaneUzivatele(Console.Interfaces.Classes.UzivateleListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Console.Interfaces.DataSets.Uzivatele ds = new Console.Interfaces.DataSets.Uzivatele();

            try
            {
                Globals.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals.ConnectionString);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * from " + Fask.Console.Interfaces.Constants.Tables.TABLE_CZMSTPWD + " uzivatele " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.UserID))
                {
                    command.CommandText += "and uzivatele.ID=@id ";
                    command.Parameters.AddWithValue("@id", filtr.UserID);
                }

                if (!string.IsNullOrEmpty(filtr.UserLogin))
                {
                    command.CommandText += "and uzivatele.LOGIN=@login ";
                    command.Parameters.AddWithValue("@login", filtr.UserLogin);
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMSTPWD);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IUzivatele2_GetUzivatelByID Members

        public Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow GetUzivatelByID(int id)
        {
            try
            {
                Console.Interfaces.Classes.UzivateleListFiltr filtr = new Console.Interfaces.Classes.UzivateleListFiltr();
                filtr.UserID = id.ToString();

                Console.Interfaces.DataSets.Uzivatele ds = GetFiltrovaneUzivatele(filtr);

                if (ds.CZMSTPWD.Count > 0)
                    return ds.CZMSTPWD.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IUzivatele2_GetUzivatele Members

        public Console.Interfaces.DataSets.Uzivatele GetUzivatele()
        {
            try
            {
                Console.Interfaces.Classes.UzivateleListFiltr filtr = new Console.Interfaces.Classes.UzivateleListFiltr();
                return GetFiltrovaneUzivatele(filtr);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IUzivatele2_InsertUzivatel Members

        public bool InsertUzivatel(Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow uzivatelRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals.ConnectionString);
                connection.Open();

                Pohoda_DataSets.UzivateleTableAdapters.CZMSTPWDTableAdapter ta_uzivatele = new Pohoda_DataSets.UzivateleTableAdapters.CZMSTPWDTableAdapter();
                ta_uzivatele.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_uzivatele.MyTransaction = trans;

                ta_uzivatele.Insert(
                    uzivatelRow.LOGIN,
                    uzivatelRow.PASSWD,
                    uzivatelRow.ID,
                    uzivatelRow.IsADMNull() ? (short?)null : uzivatelRow.ADM,
                    uzivatelRow.FIRSTNAME,
                    uzivatelRow.SECONDNAME,
                    uzivatelRow.IsHASHNull() ? string.Empty : uzivatelRow.HASH,
                    uzivatelRow.IsEANNull() ? string.Empty : uzivatelRow.EAN,
                    uzivatelRow.CODE
                    );


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

        #endregion

        #region IUzivatele2_UpdateUzivatel Members

        public bool UpdateUzivatel(Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow uzivatelRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals.ConnectionString);
                connection.Open();

                Pohoda_DataSets.UzivateleTableAdapters.CZMSTPWDTableAdapter ta_uzivatele = new Pohoda_DataSets.UzivateleTableAdapters.CZMSTPWDTableAdapter();
                ta_uzivatele.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_uzivatele.MyTransaction = trans;

                ta_uzivatele.Update(
                    uzivatelRow.LOGIN,
                    uzivatelRow.PASSWD,
                    uzivatelRow.IsADMNull() ? (short?)null : uzivatelRow.ADM,
                    uzivatelRow.FIRSTNAME,
                    uzivatelRow.SECONDNAME,
                    uzivatelRow.IsHASHNull() ? string.Empty : uzivatelRow.HASH,
                    uzivatelRow.IsEANNull() ? string.Empty : uzivatelRow.EAN,
                    uzivatelRow.CODE,
                    uzivatelRow.ID
                    );


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

        #endregion 

        #endregion
    }
}
