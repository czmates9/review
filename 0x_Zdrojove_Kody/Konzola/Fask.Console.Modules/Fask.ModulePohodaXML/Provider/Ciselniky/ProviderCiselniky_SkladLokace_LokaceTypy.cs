using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModulePohodaXML.Provider.Ciselniky
{
    public partial class Provider :
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_DeleteSkladLokace_LokaceTypy,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetFiltrovaneSkladLokace_LokaceTypy,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_InsertSkladLokace_LokaceTypy,
        Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy.ISkladLokace_LokaceTypy2_UpdateSkladLokace_LokaceTypy
    {
        #region ISkladLokace_LokaceTypy2

        #region ISkladLokace_LokaceTypy2_DeleteSkladLokace_LokaceTypy Members

        public bool DeleteSkladLokace_LokaceTypy(string type)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z tabulky [TABLE_CZMST_SkladLokace_LokaceTypy]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_LOKACETYPY + " " +
                    "where " +
                    "[TYPE]=@type";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@type", type);

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

        #endregion

        #region ISkladLokace_LokaceTypy2_GetFiltrovaneSkladLokace_LokaceTypy Members

        public Fask.Interfaces.DataSets.SkladLokace GetFiltrovaneSkladLokace_LokaceTypy(Fask.Interfaces.Filtry.LokaceTypyListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_LOKACETYPY + " typy " +
                    "where " +
                    "1=1 "
                    ;

                // typ lokace
                if (!string.IsNullOrEmpty(filtr.type))
                {
                    command.CommandText += "and typy.[TYPE]=@typ ";
                    command.Parameters.AddWithValue("@typ", filtr.type);
                }

                #region Logika zaškrtavatek

                command.CommandText += " AND ( 1!=1 ";


                // je prijmova lokace
                if (filtr.is_receive)
                {
                    command.CommandText += "OR typy.IS_RECEIVE=@isreceive ";
                    command.Parameters.AddWithValue("@isreceive", filtr.is_receive); 
                }

                // je vychozi lokace
                if (filtr.is_default)
                {
                    command.CommandText += "OR typy.IS_DEFAULT=@isdefault ";
                    command.Parameters.AddWithValue("@isdefault", filtr.is_default);
                    
                }

                // je bezna/normalni lokace
                if (filtr.is_normal)
                {
                    command.CommandText += "OR typy.IS_NORMAL=@isnormal ";
                    command.Parameters.AddWithValue("@isnormal", filtr.is_normal);
                    
                }
                command.CommandText += ")"; 
                #endregion



                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_SkladLokace_LokaceTypy);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy Members

        public Fask.Interfaces.DataSets.SkladLokace GetSkladLokace_LokaceTypy()
        {
            try
            {
                Fask.Interfaces.Filtry.LokaceTypyListFiltr filtr = new Fask.Interfaces.Filtry.LokaceTypyListFiltr();
                filtr.is_default = true;
                filtr.is_normal = true;
                filtr.is_receive = true;
                return GetFiltrovaneSkladLokace_LokaceTypy(filtr);
            }
            catch
            {
                throw;
            }

            // 10.5.2016 PeV: predelano na skladani filtru ...
            //System.Data.SqlClient.SqlConnection connection = null;
            //System.Data.SqlClient.SqlCommand command = null;
            //System.Data.SqlClient.SqlDataAdapter adapter = null;
            //Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

            //try
            //{
            //    adapter = new System.Data.SqlClient.SqlDataAdapter();
            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    command = new System.Data.SqlClient.SqlCommand();
            //    command.Connection = connection;

            //    command.CommandText =
            //        "select * " +
            //        "from " + TABLE_CZMST_SkladLokace_LokaceTypy;
            //    adapter.SelectCommand = command;
            //    adapter.Fill(ds.CZMST_SkladLokace_LokaceTypy);

            //    return ds;
            //}
            //catch
            //{
            //    throw;
            //}
        }

        #endregion

        #region ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType Members

        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow GetSkladLokace_LokaceTypyByType(string type)
        {
            try
            {
                if (string.IsNullOrEmpty(type))
                    return null;

                Fask.Interfaces.Filtry.LokaceTypyListFiltr filtr = new Fask.Interfaces.Filtry.LokaceTypyListFiltr();
                filtr.type = type;
                filtr.is_default = true;
                filtr.is_normal = true;
                filtr.is_receive = true;
                Fask.Interfaces.DataSets.SkladLokace ds = GetFiltrovaneSkladLokace_LokaceTypy(filtr);
                if (ds.CZMST_SkladLokace_LokaceTypy.Count > 0)
                    return ds.CZMST_SkladLokace_LokaceTypy.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }

            // 10.5.2016 PeV: zakomentovano, zbytecna duplicita kodu
            //System.Data.SqlClient.SqlConnection connection = null;
            //System.Data.SqlClient.SqlCommand command = null;
            //System.Data.SqlClient.SqlDataAdapter adapter = null;

            //try
            //{
            //    Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();
            //    adapter = new System.Data.SqlClient.SqlDataAdapter();
            //    connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            //    command = new System.Data.SqlClient.SqlCommand();
            //    command.Connection = connection;
            //    command.CommandText =
            //        "select * " +
            //        "from " + TABLE_CZMST_SkladLokace_LokaceTypy + " " + 
            //        "where " +
            //        "[TYPE]=@type";
            //    command.Parameters.AddWithValue("@type", type);
            //    adapter.SelectCommand = command;
            //    adapter.Fill(ds.CZMST_SkladLokace_LokaceTypy);

            //    if (ds.CZMST_SkladLokace_LokaceTypy.Count > 0)
            //        return ds.CZMST_SkladLokace_LokaceTypy.First();
            //    else
            //        return null;
            //}
            //catch
            //{
            //    throw;
            //}
        }

        #endregion

        #region ISkladLokace_LokaceTypy2_InsertSkladLokace_LokaceTypy Members

        public bool InsertSkladLokace_LokaceTypy(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter ta_typy = new Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter();
                ta_typy.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_typy.MyTransaction = trans;

                ta_typy.Insert(
                    row.IsTYPENull() ? string.Empty : row.TYPE,
                    row.IsDescriptionNull() ? null : row.Description,
                    row.IS_RECEIVE,
                    row.IS_DEFAULT,
                    row.IS_NORMAL
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

        #region ISkladLokace_LokaceTypy2_UpdateSkladLokace_LokaceTypy Members

        public bool UpdateSkladLokace_LokaceTypy(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter ta_typy = new Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter();
                ta_typy.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_typy.MyTransaction = trans;

                ta_typy.Update(
                    row.IsDescriptionNull() ? null : row.Description,
                    row.IS_RECEIVE,
                    row.IS_DEFAULT,
                    row.IS_NORMAL,
                    row.IsTYPENull() ? string.Empty : row.TYPE
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
