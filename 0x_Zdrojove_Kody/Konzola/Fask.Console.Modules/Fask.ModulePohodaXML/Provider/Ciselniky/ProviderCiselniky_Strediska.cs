using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModulePohodaXML.Provider.Ciselniky
{
    public partial class Provider :
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_DeleteStredisko,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediska,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_InsertStredisko,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_UpdateStredisko,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetStrediskoByID,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_ImportStrediska,
        Fask.Interfaces.Ciselniky.Strediska.IStrediska2_GetFiltrovaneStrediska
    {
        #region IStrediska2

        #region IStrediska2_DeleteStredisko Members

        public bool DeleteStredisko(string id)
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

                // odstraneni zaznamu z tabulky [CZMST090]
                string deleteCmd =
                    "delete from " + Fask.SQL.Constants.Common.TABLE_CZMST091 + " " +
                    "where " +
                    "str_id=@id";
                command = new System.Data.SqlClient.SqlCommand(deleteCmd, connection, trans);
                command.Parameters.AddWithValue("@id", id);

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

        #region IStrediska2_GetStrediska Members

        public Fask.Interfaces.DataSets.Strediska GetStrediska()
        {
            Fask.Interfaces.Filtry.StrediskaListFiltr filtr = new Fask.Interfaces.Filtry.StrediskaListFiltr();
            return GetFiltrovaneStrediska(filtr);
        }

        #endregion

        #region IStrediska2_InsertStredisko Members

        public bool InsertStredisko(Fask.Interfaces.DataSets.Strediska.CZMST091Row strediskoRow)
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

                string insertCmd =
                    "insert into " + Fask.SQL.Constants.Common.TABLE_CZMST091 + " " +
                    "(str_id, str_desc, str_typ, str_carcode, skl_id, odb_id) " +
                    "VALUES " +
                    "(@str_id, @str_desc, @str_typ, @str_carcode, @skl_id, @odb_id)";

                command = new System.Data.SqlClient.SqlCommand(insertCmd, connection, trans);
                command.Parameters.AddWithValue("@str_id", strediskoRow.str_id);
                command.Parameters.AddWithValue("@str_desc", strediskoRow.Isstr_descNull() ? (object)DBNull.Value : strediskoRow.str_desc);
                command.Parameters.AddWithValue("@str_typ", strediskoRow.Isstr_typNull() ? (object)DBNull.Value : strediskoRow.str_typ);
                command.Parameters.AddWithValue("@str_carcode", strediskoRow.Isstr_carcodeNull() ? (object)DBNull.Value : strediskoRow.str_carcode);
                command.Parameters.AddWithValue("@skl_id", strediskoRow.Isskl_idNull() ? (object)DBNull.Value : strediskoRow.skl_id);
                command.Parameters.AddWithValue("@odb_id", strediskoRow.Isodb_idNull() ? (object)DBNull.Value : strediskoRow.odb_id);


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

        #region IStrediska2_UpdateStredisko Members

        public bool UpdateStredisko(Fask.Interfaces.DataSets.Strediska.CZMST091Row strediskoRow)
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

                string updateCmd =
                    "update " + Fask.SQL.Constants.Common.TABLE_CZMST091 + " " +
                    "set str_desc=@str_desc, str_typ=@str_typ, str_carcode=@str_carcode, skl_id=@skl_id, odb_id=@odb_id " +
                    "where str_id=@id";
                command = new System.Data.SqlClient.SqlCommand(updateCmd, connection, trans);
                command.Parameters.AddWithValue("@id", strediskoRow.str_id);
                command.Parameters.AddWithValue("@str_desc", strediskoRow.Isstr_descNull() ? (object)DBNull.Value : strediskoRow.str_desc);
                command.Parameters.AddWithValue("@str_typ", strediskoRow.Isstr_typNull() ? (object)DBNull.Value : strediskoRow.str_typ);
                command.Parameters.AddWithValue("@str_carcode", strediskoRow.Isstr_carcodeNull() ? (object)DBNull.Value : strediskoRow.str_carcode);
                command.Parameters.AddWithValue("@skl_id", strediskoRow.Isskl_idNull() ? (object)DBNull.Value : strediskoRow.skl_id);
                command.Parameters.AddWithValue("@odb_id", strediskoRow.Isodb_idNull() ? (object)DBNull.Value : strediskoRow.odb_id);

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

        #region IStrediska2_GetStrediskoByID Members

        public Fask.Interfaces.DataSets.Strediska.CZMST091Row GetStrediskoByID(string id)
        {


            try
            {
                Fask.Interfaces.DataSets.Strediska dsStr = new Fask.Interfaces.DataSets.Strediska();

                Fask.Interfaces.Filtry.StrediskaListFiltr filtr = new Fask.Interfaces.Filtry.StrediskaListFiltr();
                filtr.str_id = id;
                dsStr =  GetFiltrovaneStrediska(filtr);

                if (dsStr.CZMST091.Count > 0)
                    return dsStr.CZMST091.First();
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region IStrediska2_ImportStrediska Members

        public string ImportStrediska()
        {

            var ds = Database.Pohoda.sSTR_GetData();

            Fask.Interfaces.DataSets.Strediska.CZMST091DataTable dt = new Fask.Interfaces.DataSets.Strediska.CZMST091DataTable();

            if ((ds != null) && (ds.Count > 0))
            {



                int str_carcode_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Strediska.ColumnsInfo_CZMST091["str_carcode"].MaxLength;
                int str_desc_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Strediska.ColumnsInfo_CZMST091["str_desc"].MaxLength;
                //int str_id_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Strediska.ColumnsInfo_CZMST091["str_id"].MaxLength;
                int str_typ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Strediska.ColumnsInfo_CZMST091["str_typ"].MaxLength;

                this.DeleteStredisko();

                foreach (var item in ds)
                {
                    var ROW = dt.NewCZMST091Row();

                    ROW.str_carcode = item.ID.ToString();
                    ROW.str_desc = item.IsSTextNull() ? string.Empty : (item.SText.Length > str_desc_MaxLength ? item.SText.Substring(0, str_desc_MaxLength) : item.SText.Trim());
                    ROW.str_id = item.ID.ToString();
                    ROW.str_typ = item.IsIDSNull() ? string.Empty : (item.IDS.Length > str_typ_MaxLength ? item.IDS.Substring(0, str_typ_MaxLength) : item.IDS.Trim());

                    this.InsertStredisko(ROW);

                }

            }
            else
            {
                return null;
            }

            return "OK";
        }

        private void DeleteStredisko()
        {
            try
            {
                Globals_V1.LoadConfiguration();

                Pohoda_DataSets.StrediskaTableAdapters.CZMST091TableAdapter ta = new Pohoda_DataSets.StrediskaTableAdapters.CZMST091TableAdapter();
                ta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                ta.DeleteAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion


        #region IStrediska2_GetFiltrovaneStrediska Members

        public Fask.Interfaces.DataSets.Strediska GetFiltrovaneStrediska(Fask.Interfaces.Filtry.StrediskaListFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Strediska ds = new Fask.Interfaces.DataSets.Strediska();

            try
            {
                Globals_V1.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                string comnd = "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST091;

                comnd += " where 1=1";


                if (!string.IsNullOrEmpty(filtr.str_carcode))
                {
                    comnd += " AND str_carcode='" + filtr.str_carcode.Trim() + "' ";
                }

                if (!string.IsNullOrEmpty(filtr.str_desc))
                {
                    comnd += " AND str_desc like '" + filtr.str_desc.Trim() + "%' ";
                }

                if (!string.IsNullOrEmpty(filtr.str_id))
                {
                    comnd += " AND str_id=" + filtr.str_id.Trim();
                }
                if (!string.IsNullOrEmpty(filtr.str_typ))
                {
                    comnd += " AND str_typ='" + filtr.str_typ.Trim() + "' ";
                }


                command.CommandText = comnd;
                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST091);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        public List<Tuple<string, string, bool>> GetStrediskaTableInfo()
        {
            List<Tuple<string, string, bool>> tableInfo = new List<Tuple<string, string, bool>>();

            using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
            {
                string query = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + Fask.SQL.Constants.Common.TABLE_CZMST091 + "'";

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
