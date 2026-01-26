using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Filtry;

namespace Fask.ModulePohodaXML.Provider.Ciselniky
{
    public partial class Provider :
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_DeleteSkladLokace_Mapa,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetFiltrovaneSkladLokace_Mapa,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_Mapa,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_InsertSkladLokace_Mapa,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_UpdateSkladLokace_Mapa,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_GenerovatLokace_SkladLokace_Mapa,
        Fask.Interfaces.Ciselniky.SkladLokace_Mapa.ISkladLokace_Mapa2_ImportSkladLokace_Mapa,

         Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_GetFiltrovaneSkladLokace,
        Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_DeleteSkladLokace,
        Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_InsertSkladLokace,
        Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_UpdateSkladLokace,
        Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_GetSkladLokaceByLOCNCODE,
        Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094.ISkladLokace_CZMST094_ImportSkladLokace
    {

        #region ISkladLokace_Mapa2

        #region ISkladLokace_Mapa2_DeleteSkladLokace_Mapa Members

        public bool DeleteSkladLokace_Mapa(string skl_id, string locncode)
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

                // odstraneni zaznamu z tabulky [TABLE_CZMST_SkladLokace_Mapa]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " " +
                    "where " +
                    "skl_id=@skl_id and locncode=@locncode";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@skl_id", skl_id);
                command.Parameters.AddWithValue("@locncode", locncode);

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

        #region ISkladLokace_Mapa2_GetFiltrovaneSkladLokace_Mapa Members

        public Fask.Interfaces.DataSets.SkladLokace GetFiltrovaneSkladLokace_Mapa(Fask.Interfaces.Filtry.SkladLokaceMapaListFiltr filtr)
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
                    "select m.*, sklad.skl_desc as SkladOznaceni " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " m " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklad on sklad.skl_id = m.skl_id " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.skl_id))
                {
                    command.CommandText += "and m.SKL_ID=@skl_id ";
                    command.Parameters.AddWithValue("@skl_id", filtr.skl_id);
                }

                //if (!string.IsNullOrEmpty(filtr.skl_id))
                //{
                //    command.CommandText += "and m.ITEMDESC like '%' + @nazev + '%' ";
                //    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                //}

                if (!string.IsNullOrEmpty(filtr.locncode))
                {
                    command.CommandText += "and m.LOCNCODE=@locncode ";
                    command.Parameters.AddWithValue("@locncode", filtr.locncode);
                }

                command.CommandText += "order by m.skl_id asc, m.locncode asc;";

                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_SkladLokace_Mapa);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace_Mapa2_GetSkladLokace_Mapa Members

        public Fask.Interfaces.DataSets.SkladLokace GetSkladLokace_Mapa()
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
                    "select m.*, sklad.skl_desc as SkladOznaceni " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " m " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklad on sklad.skl_id = m.skl_id " +
                    "order by m.skl_id asc, m.locncode asc ";
                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_SkladLokace_Mapa);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace_Mapa2_GetSkladLokace_MapaBySklIDAndLocncode Members

        public Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow GetSkladLokace_MapaBySklIDAndLocncode(string skl_id, string locncode)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            try
            {
                Globals_V1.LoadConfiguration();
                Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;
                command.CommandText =
                    "select m.*, sklad.skl_desc as SkladOznaceni " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + " m " +
                    "left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklad on sklad.skl_id = m.skl_id " +
                    "where " +
                    "m.SKL_ID=@skl_id AND m.locncode=@locncode";
                command.Parameters.AddWithValue("@skl_id", skl_id);
                command.Parameters.AddWithValue("@locncode", locncode);
                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST_SkladLokace_Mapa);

                if (ds.CZMST_SkladLokace_Mapa.Count > 0)
                    return ds.CZMST_SkladLokace_Mapa.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ISkladLokace_Mapa2_InsertSkladLokace_Mapa Members

        public bool InsertSkladLokace_Mapa(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_MapaTableAdapter ta_mapa = new Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_MapaTableAdapter();
                ta_mapa.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_mapa.MyTransaction = trans;

                ta_mapa.Insert(
                    row.IsSKL_IDNull() ? null : row.SKL_ID,
                    row.IsLOCNCODENull() ? null : row.LOCNCODE,
                    row.IsTYPENull() ? null : row.TYPE,
                    row.IsBarcodeNull() ? null : row.Barcode,
                    row.IsDescriptionNull() ? null : row.Description
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

        #region ISkladLokace_Mapa2_UpdateSkladLokace_Mapa Members

        public bool UpdateSkladLokace_Mapa(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_MapaRow row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals_V1.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();

                Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_MapaTableAdapter ta_mapa = new Pohoda_DataSets.SkladLokaceTableAdapters.CZMST_SkladLokace_MapaTableAdapter();
                ta_mapa.Connection = connection;
                trans = connection.BeginTransaction(IsolationLevel.Serializable);
                ta_mapa.MyTransaction = trans;

                ta_mapa.Update(
                    row.IsTYPENull() ? null : row.TYPE,
                    row.IsBarcodeNull() ? null : row.Barcode,
                    row.IsDescriptionNull() ? null : row.Description,
                    row.IsSKL_IDNull() ? null : row.SKL_ID,
                    row.IsLOCNCODENull() ? null : row.LOCNCODE
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

        #region ISkladLokace_Mapa2_GenerovatLokace_SkladLokace_Mapa Members

        public void GenerovatLokace_SkladLokace_Mapa(Fask.Interfaces.Classes.GenerovaniLokaci gl)
        {
            Fask.ModulePohodaXML.Classes.SkladLokace_Mapa.GenerujLokaci(gl);
        }

        public string ImportSkladLokace_Mapa()
        {
            throw new NotImplementedException();
        }

        public List<Tuple<string, string, bool>> GetSkladLokace_MapaTableInfo()
        {
            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
            {
                string query = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + Fask.SQL.Constants.Common.TABLE_CZMST094 + "'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ZasobyTableInfo info = new ZasobyTableInfo();
                        info.ColumnName = reader["COLUMN_NAME"].ToString();
                        info.DataType = reader["DATA_TYPE"].ToString();
                        info.IsNullable = reader["IS_NULLABLE"].ToString() == "YES";
                        tableInfo.Add(new Tuple<string, string, bool>(info.ColumnName, info.DataType, info.IsNullable));
                    }

                    reader.Close();
                }
            }

            return tableInfo;
        }

        #endregion

        #endregion

        #region Lokace CZMST094

        public Interfaces.DataSets.SkladLokace GetFiltrovaneSkladLokace_CZMST094(SkladLokaceCZMST094ListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.SkladLokace ds = new Fask.Interfaces.DataSets.SkladLokace();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "select m.* " +
                    "from " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " m " +
                    "where " +
                    "1=1 "
                    ;

                if (!string.IsNullOrEmpty(filtr.skl_id))
                {
                    command.CommandText += "and m.SKL_ID=@skl_id ";
                    command.Parameters.AddWithValue("@skl_id", filtr.skl_id);
                }

                //if (!string.IsNullOrEmpty(filtr.skl_id))
                //{
                //    command.CommandText += "and m.ITEMDESC like '%' + @nazev + '%' ";
                //    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                //}

                if (!string.IsNullOrEmpty(filtr.locncode))
                {
                    command.CommandText += "and m.LOCNCODE=@locncode ";
                    command.Parameters.AddWithValue("@locncode", filtr.locncode);
                }

                command.CommandText += "order by m.skl_id asc, m.locncode asc;";

                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST094);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteSkladLokace_CZMST094(string skl_id, string locncode)
        {

            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

                // odstraneni zaznamu z tabulky [TABLE_CZMST_SkladLokace_Mapa]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " " +
                    "where " +
                    "skl_id=@skl_id and locncode=@locncode";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@skl_id", skl_id);
                command.Parameters.AddWithValue("@locncode", locncode);

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

        public bool InsertSkladLokace_CZMST094(Interfaces.DataSets.SkladLokace.CZMST094Row row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;


            try
            {
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    comm.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZMST094 + "([SKL_ID],[LOCNCODE],[TYPE],[Description],[Barcode]" +
                        " ) VALUES (" +
                        " @SKL_ID, @LOCNCODE, @TYPE, @Description, " +
                        " @Barcode)";


                    //comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(row.SKL_ID) ? throw new Exception("SKL_ID is null!") : row.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = row.IsSKL_IDNull() ? (object)DBNull.Value : row.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = row.LOCNCODE });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TYPE", DbType = System.Data.DbType.String, Value = row.IsTYPENull() ? (object)DBNull.Value : row.TYPE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, Value = row.IsDescriptionNull() ? (object)DBNull.Value : row.Description });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, Value = row.IsBarcodeNull() ? (object)DBNull.Value : row.Barcode });
                    //comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsLOCNCODENull() ? (object)DBNull.Value : zboziRow.LOCNCODE });


                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool UpdateSkladLokace_CZMST094(Interfaces.DataSets.SkladLokace.CZMST094Row row)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;


            try
            {
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    //comm.CommandText = @"UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST094 + "([SKL_ID],[LOCNCODE],[TYPE],[Description],[Barcode]" +
                    //    " ) VALUES (" +
                    //    " @SKL_ID, @LOCNCODE, @TYPE, @Description, " +
                    //    " @Barcode)";


                    comm.CommandText = @"UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " SET" +
                        "  SKL_ID = @SKL_ID, TYPE = @TYPE, Description = @Description," +
                        " Barcode = @Barcode, LOCNCODE = @LOCNCODE" +
                        " WHERE (LOCNCODE = @LOCNCODE)";


                    //comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(row.SKL_ID) ? throw new Exception("SKL_ID is null!") : row.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = row.IsSKL_IDNull() ? (object)DBNull.Value : row.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = row.LOCNCODE });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TYPE", DbType = System.Data.DbType.String, Value = row.IsTYPENull() ? (object)DBNull.Value : row.TYPE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, Value = row.IsDescriptionNull() ? (object)DBNull.Value : row.Description });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, Value = row.IsBarcodeNull() ? (object)DBNull.Value : row.Barcode });
                    //comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsLOCNCODENull() ? (object)DBNull.Value : zboziRow.LOCNCODE });


                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public Interfaces.DataSets.SkladLokace.CZMST094Row GetSkladLokaceByLOCNCODE_CZMST094(string LOCNCODE)
        {
            try
            {
                if (string.IsNullOrEmpty(LOCNCODE))
                    return null;

                Fask.Interfaces.Filtry.SkladLokaceCZMST094ListFiltr filtr = new Fask.Interfaces.Filtry.SkladLokaceCZMST094ListFiltr();
                filtr.locncode = LOCNCODE;
                Fask.Interfaces.DataSets.SkladLokace ds = GetFiltrovaneSkladLokace_CZMST094(filtr);

                if (ds.CZMST094.Count > 0)
                    return ds.CZMST094.First();
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ImportSkladLokace_CZMST094()
        {
            ExportKatalogLokace_CZMST094_Procedura();


            return "OK";
        }

        private string ExportKatalogLokace_CZMST094_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                //Globals.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_POHODA_FASK_LOKACE");
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = 1000;


                adpacommand.Connection = adpaconnection;

                adpaconnection.Open();
                adpacommand.ExecuteNonQuery();

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

            return "OK";
        }

        public List<Tuple<string, string, bool>> GetSkladLokace_CZMST094TableInfo()
        {
            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
            {
                string query = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + Fask.SQL.Constants.Common.TABLE_CZMST_SKLADLOKACE_MAPA + "'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ZasobyTableInfo info = new ZasobyTableInfo();
                        info.ColumnName = reader["COLUMN_NAME"].ToString();
                        info.DataType = reader["DATA_TYPE"].ToString();
                        info.IsNullable = reader["IS_NULLABLE"].ToString() == "YES";
                        tableInfo.Add(new Tuple<string, string, bool>(info.ColumnName, info.DataType, info.IsNullable));
                    }

                    reader.Close();
                }
            }

            return tableInfo;
        }

        #endregion

    }
}
