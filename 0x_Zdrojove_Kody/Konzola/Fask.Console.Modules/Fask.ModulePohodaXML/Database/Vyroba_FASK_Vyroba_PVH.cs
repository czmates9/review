using Fask.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModulePohodaXML.Database
{
    public class Vyroba_FASK_Vyroba_PVH
    {

        #region Planovani

        public static string Get_FASK_PLANOVANI_Insert_PVH(
            string SOPNUMBE,
            string SOPTYPE,
            string SOPDESC,
            string BarcodeH,
            byte Active,
            Fask.Interfaces.Classes.ZaplanovanyDoklad zaplanovanyDoklad
            )
        {
            string name = string.Empty;
            try
            {
                Globals_V1.LoadConfiguration();

                using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    string query = "INSERT INTO " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVH
                        + " ( "
                        + " SOPNUMBE, SOPTYPE, SOPDESC, BarcodeH, Active, TypDok "
                        + " ) VALUES ( "
                        + " @SOPNUMBE, @SOPTYPE, @SOPDESC, @BarcodeH, @Active, @TypDok"
                        + " ) ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SOPNUMBE", string.IsNullOrEmpty(SOPNUMBE) ? string.Empty : SOPNUMBE.Trim());
                        command.Parameters.AddWithValue("@SOPTYPE", string.IsNullOrEmpty(SOPTYPE) ? string.Empty : SOPTYPE.Trim());
                        command.Parameters.AddWithValue("@SOPDESC", string.IsNullOrEmpty(SOPDESC) ? (object)DBNull.Value : SOPDESC.Trim());
                        command.Parameters.AddWithValue("@BarcodeH", string.IsNullOrEmpty(BarcodeH) ? string.Empty : BarcodeH.Trim());
                        command.Parameters.AddWithValue("@Active", Active);
                        command.Parameters.AddWithValue("@TypDok", zaplanovanyDoklad.ToString());

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        connection.Close();
                        // Check Error
                        if (result < 0)
                            throw new Exception("Nevložen zaznam do " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVH);
                    }
                }

                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Fill_PVH(Fask.Interfaces.DataSets.Vyroba_Planovani ds)
        {
            try
            {
                ds.FASK_Vyroba_PVH.Clear();

                Globals_V1.LoadConfiguration();

                using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVH;

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.SelectCommand = command;
                            int returnValue = adapter.Fill(ds, ds.FASK_Vyroba_PVH.TableName);
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        public static int? GetDEXROWID_BySOPNUMBE(string SOPNUMBE)
        {
            try
            {
                Globals_V1.LoadConfiguration();

                using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "SELECT DISTINCT DEX_ROW_ID FROM " +
                            Fask.SQL.Constants.Common.TABLE_FASK_Vyroba_PVH +
                            " WHERE (SOPNUMBE = @SOPNUMBE)";

                        command.Parameters.AddWithValue("@SOPNUMBE", string.IsNullOrEmpty(SOPNUMBE) ? (object)DBNull.Value : SOPNUMBE.Trim());

                        con.Open();

                        object returnValue = null;
                        returnValue = command.ExecuteScalar();

                        con.Close();
                        if (returnValue is int)
                        {
                            return (int)returnValue;
                        }
                    }
                }

                return null;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        #region FASK_PLANOVANI

        public static int Update_FASK_PLANOVANI(object data)
        {
            //System.Data.SqlClient.SqlTransaction transaction = null;
            //System.Data.SqlClient.SqlConnection con = null;
            try
            {
                int result = 0;

                Globals_V1.LoadConfiguration();

                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    //con.Open();
                    //transaction = con.BeginTransaction();

                    using (var commandInsert = con.CreateCommand())
                    using (var commandUpdate = con.CreateCommand())
                    using (var commandDelete = con.CreateCommand())
                    using (var commandSelect = con.CreateCommand())
                    {
                        InitializeCommandInsert_FASK_PLANOVANI(commandInsert);
                        InitializeCommandUpdate_FASK_PLANOVANI(commandUpdate);
                        InitializeCommandDelete_FASK_PLANOVANI(commandDelete);
                        InitializeCommandSelect_FASK_PLANOVANI(commandSelect);

                        using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
                        {
                            adapter.DeleteCommand = commandDelete;
                            adapter.InsertCommand = commandInsert;
                            adapter.UpdateCommand = commandUpdate;
                            adapter.SelectCommand = commandSelect;

                            var dataIsDataSet = data as System.Data.DataSet;
                            var dataIsDataTable = data as System.Data.DataTable;
                            var dataIsDataRow = data as System.Data.DataRow;
                            var dataIsDataRowArray = data as System.Data.DataRow[];

                            //if (data is System.Data.DataSet)
                            if (dataIsDataSet != null)
                                result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
                            else if (dataIsDataTable != null)
                                result = adapter.Update(dataIsDataTable);
                            else if (dataIsDataRow != null)
                                result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
                            else if (dataIsDataRowArray != null)
                                result = adapter.Update(dataIsDataRowArray);
                            else
                                throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
                        }
                    }
                }

                //transaction.Commit();

                return result;
            }
            catch (Exception ex)
            {
                //Logging.Log.Write(ex);
                Logging.ExceptionHandler2.Handle(ex);

                //try
                //{
                //    if (transaction != null)
                //        transaction.Rollback();
                //}
                //catch (Exception exTransaction)
                //{
                //    //Logging.Log.Write(exTransaction);
                //    Logging.ExceptionHandler2.Handle(exTransaction);
                //}

                throw ex;
            }
            finally
            {
                //if (con != null && con.State != System.Data.ConnectionState.Closed)
                //{
                //    con.Close();
                //    con = null;
                //}
            }
        }

        #region Inicialize metody

        public static void InitializeCommandInsert_FASK_PLANOVANI(SqlCommand command)
        {
            command.CommandText = "INSERT INTO FASK_PLANOVANI (" +
                " [DESC], GUID " +
                " ) VALUES( " +
                " @DESC, @GUID " +
                " )";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@DESC", DbType = System.Data.DbType.String, SourceColumn = "DESC", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID", SourceVersion = System.Data.DataRowVersion.Current });

        }

        public static void InitializeCommandUpdate_FASK_PLANOVANI(SqlCommand command)
        {
            command.CommandText = @"UPDATE FASK_PLANOVANI SET [DESC] = @DESC WHERE (GUID = @GUID)";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@DESC", DbType = System.Data.DbType.String, SourceColumn = "DESC", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID", SourceVersion = System.Data.DataRowVersion.Original });

        }

        public static void InitializeCommandDelete_FASK_PLANOVANI(SqlCommand command)
        {
            command.CommandText = "DELETE FROM FASK_PLANOVANI WHERE (GUID = @GUID)";

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@GUID",
                DbType = System.Data.DbType.Guid,
                SourceColumn = "GUID",
                SourceVersion = System.Data.DataRowVersion.Original
            });
        }

        public static void InitializeCommandSelect_FASK_PLANOVANI(SqlCommand command)
        {
            command.CommandText = "Select * from FASK_PLANOVANI";
        }


        #endregion

        #endregion

        #region FASK_PLANOVANI_PARAMS

        public static int Update_FASK_PLANOVANI_PARAMS(object data)
        {
            try
            {
                int result = 0;

                Globals_V1.LoadConfiguration();

                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var commandInsert = con.CreateCommand())
                    using (var commandUpdate = con.CreateCommand())
                    using (var commandDelete = con.CreateCommand())
                    using (var commandSelect = con.CreateCommand())
                    {
                        InitializeCommandInsert_FASK_PLANOVANI_PARAMS(commandInsert);
                        InitializeCommandUpdate_FASK_PLANOVANI_PARAMS(commandUpdate);
                        InitializeCommandDelete_FASK_PLANOVANI_PARAMS(commandDelete);
                        InitializeCommandSelect_FASK_PLANOVANI_PARAMS(commandSelect);

                        using (var adapter = new System.Data.SqlClient.SqlDataAdapter())
                        {
                            adapter.DeleteCommand = commandDelete;
                            adapter.InsertCommand = commandInsert;
                            adapter.UpdateCommand = commandUpdate;
                            adapter.SelectCommand = commandSelect;

                            var dataIsDataSet = data as System.Data.DataSet;
                            var dataIsDataTable = data as System.Data.DataTable;
                            var dataIsDataRow = data as System.Data.DataRow;
                            var dataIsDataRowArray = data as System.Data.DataRow[];

                            //if (data is System.Data.DataSet)
                            if (dataIsDataSet != null)
                                result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
                            else if (dataIsDataTable != null)
                                result = adapter.Update(dataIsDataTable);
                            else if (dataIsDataRow != null)
                                result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
                            else if (dataIsDataRowArray != null)
                                result = adapter.Update(dataIsDataRowArray);
                            else
                                throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
                        }
                    }
                }

                //transaction.Commit();

                return result;
            }
            catch (Exception ex)
            {
                //Logging.Log.Write(ex);
                Logging.ExceptionHandler2.Handle(ex);

                //try
                //{
                //    if (transaction != null)
                //        transaction.Rollback();
                //}
                //catch (Exception exTransaction)
                //{
                //    //Logging.Log.Write(exTransaction);
                //    Logging.ExceptionHandler2.Handle(exTransaction);
                //}

                throw ex;
            }
            finally
            {
                //if (con != null && con.State != System.Data.ConnectionState.Closed)
                //{
                //    con.Close();
                //    con = null;
                //}
            }
        }

        #region Inicialize metody

        public static void InitializeCommandInsert_FASK_PLANOVANI_PARAMS(SqlCommand command)
        {
            command.CommandText = "INSERT INTO FASK_PLANOVANI_PARAMS (" +
                " GUID_PLANOVANI, ColumnName, Value " +
                " ) VALUES( " +
                " @GUID_PLANOVANI, @ColumnName, @Value " +
                " )";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID_PLANOVANI", DbType = System.Data.DbType.Guid, SourceColumn = "GUID_PLANOVANI", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ColumnName", DbType = System.Data.DbType.String, SourceColumn = "ColumnName", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Value", DbType = System.Data.DbType.Boolean, SourceColumn = "Value", SourceVersion = System.Data.DataRowVersion.Current });


        }

        public static void InitializeCommandUpdate_FASK_PLANOVANI_PARAMS(SqlCommand command)
        {
            command.CommandText = @"UPDATE FASK_PLANOVANI_PARAMS SET Value = @Value WHERE (GUID_PLANOVANI = @GUID_PLANOVANI AND ColumnName = @ColumnName)";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID_PLANOVANI", DbType = System.Data.DbType.Guid, SourceColumn = "GUID_PLANOVANI", SourceVersion = System.Data.DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ColumnName", DbType = System.Data.DbType.String, SourceColumn = "ColumnName", SourceVersion = System.Data.DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Value", DbType = System.Data.DbType.Boolean, SourceColumn = "Value", SourceVersion = System.Data.DataRowVersion.Current });

        }

        public static void InitializeCommandDelete_FASK_PLANOVANI_PARAMS(SqlCommand command)
        {
            command.CommandText = "DELETE FROM FASK_PLANOVANI_PARAMS WHERE (GUID_PLANOVANI = @GUID_PLANOVANI)";

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@GUID_PLANOVANI",
                DbType = System.Data.DbType.Guid,
                SourceColumn = "GUID_PLANOVANI",
                SourceVersion = System.Data.DataRowVersion.Original
            });
        }

        public static void InitializeCommandSelect_FASK_PLANOVANI_PARAMS(SqlCommand command)
        {
            command.CommandText = "Select * from FASK_PLANOVANI_PARAMS";
        }


        #endregion

        #endregion

        public static string Get_FASK_PLANOVANI_NameColumn(string ColumnName)
        {
            string name = string.Empty;
            string NameColumn = "DESC";
            try
            {
                Globals_V1.LoadConfiguration();

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add(NameColumn);

                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var com = con.CreateCommand())
                    {
                        using (var ada = new SqlDataAdapter())
                        {

                            com.CommandText = "SELECT [DESC] from " + Fask.SQL.Constants.Common.TABLE_FASK_PLANOVANI_PARAMS_Name +
                                " where Column_Name=@Column_Name ";

                            com.Parameters.AddWithValue("@Column_Name", ColumnName);

                            ada.SelectCommand = com;
                            ada.Fill(dt);

                            if (dt.Rows.Count <= 0)
                            {
                                return name;
                            }
                            else if (dt.Rows.Count > 1)
                            {
                                name = dt.Rows[0][NameColumn].ToString();
                                string msg = string.Format("Nalezeno vícero lokalizací v tabulkce {0} pro stloupec: {1}", Fask.SQL.Constants.Common.TABLE_FASK_PLANOVANI_PARAMS_Name, ColumnName);
                                Logging.ExceptionHandler2.Handle(LogLevel.Warn, msg);
                            }
                            else if (dt.Rows.Count == 1)
                            {
                                name = dt.Rows[0][NameColumn].ToString();
                            }

                        }
                    }

                }

                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Get_FASK_PLANOVANI_Insert_NameColumn(string ColumnName, string NAME)
        {
            string name = string.Empty;
            try
            {
                Globals_V1.LoadConfiguration();

                using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    String query = "INSERT INTO " + Fask.SQL.Constants.Common.TABLE_FASK_PLANOVANI_PARAMS_Name + " (Column_Name, [DESC]) VALUES (@Column_Name, @DESC)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Column_Name", ColumnName);
                        command.Parameters.AddWithValue("@DESC", NAME);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            throw new Exception("Nevložen zaznam do ");
                    }
                }

                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Get_FASK_PLANOVANI_DeleteVariantu(Guid G)
        {
            string name = string.Empty;
            try
            {
                Globals_V1.LoadConfiguration();

                using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    String query = "DELETE FROM " +
                        Fask.SQL.Constants.Common.TABLE_FASK_PLANOVANI +
                        " WHERE GUID = @GUID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GUID", G);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            throw new Exception("Nevložen zaznam do ");
                    }
                }

                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Get_FASK_PLANOVANI_Insert_Parametr(Guid G, string ColumnName, bool Value)
        {
            string name = string.Empty;
            try
            {
                Globals_V1.LoadConfiguration();

                using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    String query = "INSERT INTO " + Fask.SQL.Constants.Common.TABLE_FASK_PLANOVANI_PARAMS +
                        " (GUID_PLANOVANI, ColumnName, Value) VALUES (@GUID_PLANOVANI, @ColumnName, @Value)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GUID_PLANOVANI", G);
                        command.Parameters.AddWithValue("@ColumnName", ColumnName);
                        command.Parameters.AddWithValue("@Value", Value);

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        connection.Close();
                        // Check Error
                        if (result < 0)
                            throw new Exception("Nevložen zaznam do ");
                    }
                }

                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Get_FASK_PLANOVANI_Update_Parametr(Guid G, string ColumnName, bool Value)
        {
            string name = string.Empty;
            try
            {
                Globals_V1.LoadConfiguration();

                using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    String query = "UPDATE " + Fask.SQL.Constants.Common.TABLE_FASK_PLANOVANI_PARAMS +
                        " SET Value = @Value " +
                        " WHERE GUID_PLANOVANI = @G AND ColumnName = @ColumnName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Value", Value);
                        command.Parameters.AddWithValue("@G", G);
                        command.Parameters.AddWithValue("@ColumnName", ColumnName);


                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        connection.Close();
                        // Check Error
                        if (result < 0)
                            throw new Exception("Nevložen zaznam do ");
                    }
                }

                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Get_FASK_PLANOVANI_DeleteParametry(Guid G)
        {
            string name = string.Empty;
            try
            {
                Globals_V1.LoadConfiguration();

                using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    String query = "DELETE FROM " +
                        Fask.SQL.Constants.Common.TABLE_FASK_PLANOVANI_PARAMS +
                        " WHERE GUID_PLANOVANI = @GUID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GUID", G);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            throw new Exception("Nevložen zaznam do ");
                    }
                }

                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Get_FASK_PLANOVANI_Update_Variantu(Guid G, string nazev)
        {
            string name = string.Empty;
            try
            {
                Globals_V1.LoadConfiguration();

                using (SqlConnection connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    String query = "UPDATE " + Fask.SQL.Constants.Common.TABLE_FASK_PLANOVANI +
                        " SET [DESC] = @DESC " +
                        " WHERE GUID = @G ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DESC", nazev);
                        command.Parameters.AddWithValue("@G", G);

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        connection.Close();
                        // Check Error
                        if (result < 0)
                            throw new Exception("Nevložen zaznam do ");
                    }
                }

                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region FIFO planovani

        public static bool Get_Info_FIFO_OBJ(string ITEMNMBR, int ORD, decimal QTY)
        {

            try
            {
                Globals_V1.LoadConfiguration();

                using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "SELECT distinct Flag FROM[FASK_Get_Planovani_Navrhar_FIFO_OBJ](@ORD, @ITEMNMBR, @QTY) ";

                        command.Parameters.AddWithValue("@ORD", ORD);
                        command.Parameters.AddWithValue("@ITEMNMBR", ITEMNMBR.Trim());
                        command.Parameters.AddWithValue("@QTY", QTY);

                        con.Open();

                        object returnValue = null;
                        returnValue = command.ExecuteScalar();

                        con.Close();
                        if (returnValue is bool)
                        {
                            return (bool)returnValue;
                        }
                    }
                }

                return false;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }

        }

        #endregion

        #endregion
    }
}
