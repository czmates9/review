using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fask.ModulePohodaXML.Provider.Ciselniky
{
    public partial class Provider :
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_DeletePracovnici,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetFiltrovanePracovniky,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnici,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_GetPracovnikByID,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_InsertPracovnici,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_UpdatePracovnici,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_SynchronizacePracovniciAD,
        Fask.Interfaces.Ciselniky.Pracovnici.IPracovnici2_ImportPracovnici
    {
        #region IPracovnici2

        #region IPracovnici2_DeletePracovnici Members

        public bool DeletePracovnici(string id)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.PracovniciTableAdapters.CZMST096TableAdapter ta_096 = new Pohoda_DataSets.PracovniciTableAdapters.CZMST096TableAdapter();
                ta_096.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_096.MyTransaction = trans;

                ta_096.Delete(id);

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

        #region IPracovnici2_GetFiltrovanePracovniky Members

        public Fask.Interfaces.DataSets.Pracovnici GetFiltrovanePracovniky(Fask.Interfaces.Filtry.PracovniciListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Pracovnici ds = new Fask.Interfaces.DataSets.Pracovnici();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST096 + " pracovnici " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.Prac_ID))
                {
                    command.CommandText += "and pracovnici.prac_id=@prac_id ";
                    command.Parameters.AddWithValue("@prac_id", filtr.Prac_ID);
                }

                if (!string.IsNullOrEmpty(filtr.Prac_Desc))
                {
                    command.CommandText += "and pracovnici.prac_desc like '%' + @nazev + '%' ";
                    command.Parameters.AddWithValue("@nazev", filtr.Prac_Desc);
                }



                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST096);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IPracovnici2_GetPracovnici Members

        public Fask.Interfaces.DataSets.Pracovnici GetPracovnici()
        {
            try
            {
                Fask.Interfaces.Filtry.PracovniciListFiltr filtr = new Fask.Interfaces.Filtry.PracovniciListFiltr();
                return GetFiltrovanePracovniky(filtr);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IPracovnici2_GetPracovnikByID Members

        public Fask.Interfaces.DataSets.Pracovnici.CZMST096Row GetPracovnikByID(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return null;

                Fask.Interfaces.Filtry.PracovniciListFiltr filtr = new Fask.Interfaces.Filtry.PracovniciListFiltr();
                filtr.Prac_ID = id;
                Fask.Interfaces.DataSets.Pracovnici ds = GetFiltrovanePracovniky(filtr);

                if (ds.CZMST096.Count > 0)
                    return ds.CZMST096.First();
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IPracovnici2_InsertPracovnici Members

        public bool InsertPracovnici(Fask.Interfaces.DataSets.Pracovnici.CZMST096Row PracovniciRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.PracovniciTableAdapters.CZMST096TableAdapter ta_096 = new Pohoda_DataSets.PracovniciTableAdapters.CZMST096TableAdapter();
                ta_096.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_096.MyTransaction = trans;

                ta_096.Insert(
                    PracovniciRow.prac_id,
                    PracovniciRow.prac_desc,
                    PracovniciRow.prac_typ,
                    PracovniciRow.prac_carcode);


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

        #region IPracovnici2_UpdatePracovnici Members

        public bool UpdatePracovnici(Fask.Interfaces.DataSets.Pracovnici.CZMST096Row PracovniciRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.PracovniciTableAdapters.CZMST096TableAdapter ta_096 = new Pohoda_DataSets.PracovniciTableAdapters.CZMST096TableAdapter();
                ta_096.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_096.MyTransaction = trans;

                ta_096.Update(
                    PracovniciRow.prac_id,
                    PracovniciRow.prac_desc,
                    PracovniciRow.prac_carcode,
                    PracovniciRow.prac_typ);


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


        #region IPracovnici2_SynchronizacePracovniciAD Members

        public void SynchronizacePracovniciAD()
        {
            throw new NotImplementedException();
        }

        public string ImportPracovnici()
        {
            ExportKatalogPracovniciPohoda_Procedura();

            return "OK";
        }

        private string ExportKatalogPracovniciPohoda_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                Globals_V1.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(
                    Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                using (var adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_POHODA_FASK_Pracovnici", adpaconnection))
                {
                    adpacommand.CommandType = CommandType.StoredProcedure;
                    adpacommand.CommandTimeout = 1000;

                    //// 1) vytvořit parametry
                    //adpacommand.Parameters.Add(new SqlParameter("@TypyVyrobku", SqlDbType.NVarChar, -1));
                    //adpacommand.Parameters.Add(new SqlParameter("@ListVyrobku", SqlDbType.NVarChar, -1));
                    //adpacommand.Parameters.Add(new SqlParameter("@TypyMaterialu", SqlDbType.NVarChar, -1));

                    //// 2) poslat prázdné řetězce, ne null
                    //adpacommand.Parameters["@TypyVyrobku"].Value = "5";// string.Empty;
                    //adpacommand.Parameters["@ListVyrobku"].Value = string.Empty;
                    //adpacommand.Parameters["@TypyMaterialu"].Value = "1";// string.Empty;

                    adpaconnection.Open();
                    var odpoved = adpacommand.ExecuteNonQuery();
                }

                return "OK";
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }
        }


        #endregion
    }
}
