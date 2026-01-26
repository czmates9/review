using Fask.Interfaces.DataSets;
using Fask.Interfaces.Filtry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModulePohodaXML.Database
{
    public class Vyroba_Machines
    {        

        public static int Update(object data, string CS)
        {
            System.Data.SqlClient.SqlTransaction transaction = null;
            System.Data.SqlClient.SqlConnection conn = null;
            int result = 0;
            try
            {

                using (conn = new System.Data.SqlClient.SqlConnection(CS))
                {

                    conn.Open();

                    transaction = conn.BeginTransaction();

                    using (var commandInsert = conn.CreateCommand())
                    using (var commandUpdate = conn.CreateCommand())
                    using (var commandDelete = conn.CreateCommand())
                    using (var commandSelect = conn.CreateCommand())
                    {

                        commandInsert.Transaction = transaction;
                        commandUpdate.Transaction = transaction;
                        commandDelete.Transaction = transaction;
                        commandSelect.Transaction = transaction;


                        InitializeCommandInsert_Machines(commandInsert);
                        InitializeCommandUpdate_Machines(commandUpdate);
                        InitializeCommandDelete_Machines(commandDelete);
                        InitializeCommandSelect_Machines(commandSelect);

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.DeleteCommand = commandDelete;
                            adapter.InsertCommand = commandInsert;
                            adapter.UpdateCommand = commandUpdate;
                            adapter.SelectCommand = commandSelect;

                            var dataIsDataSet = data as DataSet;
                            var dataIsDataTable = data as DataTable;
                            var dataIsDataRow = data as DataRow;
                            var dataIsDataRowArray = data as DataRow[];

                            //if (data is DataSet)
                            if (dataIsDataSet != null)
                                result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
                            else if (dataIsDataTable != null)
                                result = adapter.Update(dataIsDataTable);
                            else if (dataIsDataRow != null)
                                result = adapter.Update(new DataRow[] { dataIsDataRow });
                            else if (dataIsDataRowArray != null)
                                result = adapter.Update(dataIsDataRowArray);
                            else
                                throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
                        }
                    }

                    transaction.Commit();
                }

                return result;
            }
            catch (Exception ex)
            {
                //Logging.Log.Write(ex);
                //Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch (Exception exTransaction)
                {
                    //Logging.Log.Write(exTransaction);
                    //Logging.ExceptionHandler2.Handle(exTransaction);
                    throw exTransaction;
                }

                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

        }

        #region Inicialize metody

        private static void InitializeCommandInsert_Machines(SqlCommand command)
        {
            #region 20.1.2026 MaR NEW

            command.CommandText =
       @" INSERT INTO " + Fask.SQL.Constants.Common.TABLE_Machines +
         " (id, name, description, SKL_ID, LOCNCODE)"+
         " VALUES (@id, @name, @description, @SKL_ID, @LOCNCODE)";

            command.Parameters.Add(new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = SqlDbType.NVarChar,
                Size = 20,
                SourceColumn = "id",
                SourceVersion = DataRowVersion.Current
            });

            command.Parameters.Add(new SqlParameter
            {
                ParameterName = "@name",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                SourceColumn = "name",
                SourceVersion = DataRowVersion.Current
            });

            command.Parameters.Add(new SqlParameter
            {
                ParameterName = "@description",
                SqlDbType = SqlDbType.NVarChar,
                Size = 100,
                SourceColumn = "description",
                SourceVersion = DataRowVersion.Current
            });

            command.Parameters.Add(new SqlParameter
            {
                ParameterName = "@SKL_ID",
                SqlDbType = SqlDbType.NVarChar,
                Size = 20,
                SourceColumn = "SKL_ID",
                SourceVersion = DataRowVersion.Current
            });

            command.Parameters.Add(new SqlParameter
            {
                ParameterName = "@LOCNCODE",
                SqlDbType = SqlDbType.NVarChar,
                Size = 11,
                SourceColumn = "LOCNCODE",
                SourceVersion = DataRowVersion.Current
            });
            #endregion


            #region 20.1.2026 MaR OLD
            ////command.CommandText =
            ////    @" INSERT INTO " + Tables.TABLE_Machines +
            ////" (CountEntries, SOPNUMBE, SOPTYPE, SOPDESC, VNDDOCNMH, BarcodeH, LOCNCODE, DateProd, Rez1, Rez2, TermID, LSTMod, Active)" +
            ////" VALUES (@CountEntries,@SOPNUMBE,@SOPTYPE,@SOPDESC,@VNDDOCNMH,@BarcodeH,@LOCNCODE,@DateProd,@Rez1,@Rez2,@TermID,@LSTMod,@Active)";


            //command.Parameters.Add(new SqlParameter()
            //{ ParameterName = "@id", DbType = DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new SqlParameter()
            //{ ParameterName = "@name", DbType = DbType.String, SourceColumn = "name", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new SqlParameter()
            //{ ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new SqlParameter()
            //{ ParameterName = "@StrojSklad", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new SqlParameter()
            //{ ParameterName = "@StrojLokace", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current }); 
            #endregion


        }

        private static void InitializeCommandUpdate_Machines(SqlCommand command)
        {

            command.CommandText = "UPDATE " + Fask.SQL.Constants.Common.TABLE_Machines +
                                    " SET [name] = @name" +
                                    " ,[description] = @description" +
                                    " ,[SKL_ID] = @StrojSklad" +
                                    " ,[LOCNCODE] = @StrojLokace" +
                                    " WHERE (id = @id)";

            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@name", DbType = DbType.String, SourceColumn = "name", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@StrojSklad", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@StrojLokace", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@id", DbType = DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Original });

        }

        private static void InitializeCommandDelete_Machines(SqlCommand command)
        {
            command.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_Machines + " WHERE (id = @id)";


            command.Parameters.Clear();

            command.Parameters.Add(new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = SqlDbType.NVarChar,
                Size = 20,
                SourceColumn = "id",
                SourceVersion = DataRowVersion.Original
            });
        }

        private static void InitializeCommandSelect_Machines(SqlCommand command)
        {
            command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Machines;
        }


        #endregion

        public static int Machines_Insert(string CS, string Name, string Description)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText =
                         @" INSERT INTO " + Fask.SQL.Constants.Common.TABLE_Machines +
                            " ([name], [description])" +
                            " VALUES (@name, @description)";


                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@name", DbType = DbType.String, SourceColumn = "name", Value = Name == null ? (object)DBNull.Value : Name }); 
                    
                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", Value = Description == null ? (object)DBNull.Value : Description });

                    comm.CommandType = CommandType.Text;

                    comm.ExecuteNonQuery();
                    //using (var ada = new SqlDataAdapter())
                    //{
                    //    ada.InsertCommand = comm;
                    //    //ds.Production.Clear();
                    //    returnValue = ada.InsertCommand.ExecuteNonQuery();
                    //}
                }
            }
            return returnValue;
        }
        public static Vyroba.MachinesDataTable Machines_GetDataByID(string CS, string ID)
        {

            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            Vyroba.MachinesDataTable dataTable = new Vyroba.MachinesDataTable();

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    //comm.CommandText = "Select * from " + Tables.TABLE_Machines;
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Machines +
                        " WHERE ( id = @id ) ";


                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@id", DbType = DbType.String, SourceColumn = "id", Value = ID == null ? (object)DBNull.Value : ID });

                    comm.CommandType = CommandType.Text;
                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        ada.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }
        public static int Machines_FillByID(string CS, Vyroba ds, string ID)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Machines +
                        " WHERE ( id = @id ) ";


                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@id", DbType = DbType.String, SourceColumn = "id", Value = ID == null ? (object)DBNull.Value : ID });

                    comm.CommandType = CommandType.Text;
                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        //ds.Production.Clear();
                        returnValue = ada.Fill(ds, ds.Production_Konzola.TableName);
                    }
                }
            }
            return returnValue;
        }
        public static Vyroba.MachinesDataTable Machines_GetData(string CS)
        {

            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            Vyroba.MachinesDataTable dataTable = new Vyroba.MachinesDataTable();

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Machines ;

                    comm.CommandType = CommandType.Text;
                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        ada.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }
        public static int Machines_Fill(string CS, Vyroba ds)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Machines;
                    comm.CommandType = CommandType.Text;
                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;//ds.Production.Clear();
                        returnValue = ada.Fill(ds, ds.Machines.TableName);
                        
                    }
                }
            }
            return returnValue;
        }


        #region 19.1.2026 OLD
        //public static Vyroba Machines_GetFilterData(string CS, Odvod_MachineStateSetListFiltr filtr)
        //{

        //    System.Data.SqlClient.SqlConnection conn = null;
        //    System.Data.SqlClient.SqlCommand comm = null;
        //    Vyroba ds = new Vyroba();

        //    using (conn = new SqlConnection(CS))
        //    {
        //        using (comm = conn.CreateCommand())
        //        {
        //            //comm.CommandText = "Select * from " + Tables.TABLE_Machines;
        //            comm.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_Machines + " WHERE 1=1";


        //            if (!string.IsNullOrEmpty(filtr.id))
        //            {
        //                comm.CommandText += " AND ( id = @id ) ";
        //                comm.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@id", DbType = DbType.String, SourceColumn = "id", Value = filtr.id });
        //            }

        //            if (!string.IsNullOrEmpty(filtr.name))
        //            {
        //                comm.CommandText += " AND ( name = @name ) ";
        //                comm.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@name", DbType = DbType.String, SourceColumn = "name", Value = filtr.name });
        //            }

        //            if (!string.IsNullOrEmpty(filtr.Description))
        //            {
        //                comm.CommandText += " AND ( description = @description ) ";
        //                comm.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", Value = filtr.Description });
        //            }

        //            if (!string.IsNullOrEmpty(filtr.StrojSklad))
        //            {
        //                comm.CommandText += " AND ( SKL_ID = @SKL_ID ) ";
        //                comm.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@SKL_ID", DbType = DbType.String, SourceColumn = "SKL_ID", Value = filtr.StrojSklad });
        //            }

        //            if (!string.IsNullOrEmpty(filtr.StrojLokace))
        //            {
        //                comm.CommandText += " AND ( LOCNCODE = @LOCNCODE ) ";
        //                comm.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", Value = filtr.StrojLokace });
        //            }



        //            //comm.CommandType = CommandType.Text;
        //            using (var ada = new SqlDataAdapter(comm))
        //            {
        //                ada.Fill(ds.Machines); // nejlepší pro typed dataset
        //                                       // nebo: ada.Fill(ds, ds.Machines.TableName);
        //            }
        //        }
        //    }
        //    return ds;
        //} 
        #endregion

        #region 19.1.2026 NEW dohledani stroju

        public static Vyroba Machines_GetFilterData(string CS, Odvod_MachineStateSetListFiltr filtr)
        {
            SqlConnection conn = null;
            SqlCommand comm = null;
            Vyroba ds = new Vyroba();

            try
            {
                conn = new SqlConnection(CS);
                comm = conn.CreateCommand();

                comm.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_Machines + " WHERE 1=1";

                // id -> IN
                AddInFilterIfAny(comm, "id", "id", filtr.id);

                // name -> pouze =
                if (!string.IsNullOrWhiteSpace(filtr.name))
                {
                    comm.CommandText += " AND ( name = @name ) ";
                    comm.Parameters.Add("@name", SqlDbType.NVarChar).Value = filtr.name.Trim();
                }

                //// description -> IN
                //AddInFilterIfAny(comm, "description", "description", filtr.Description);

                // description -> vždy contains (%ABC%)
                AddContainsFilterIfAny(comm, "description", "description", filtr.Description);

                // SKL_ID -> IN
                AddInFilterIfAny(comm, "SKL_ID", "SKL_ID", filtr.StrojSklad);

                // LOCNCODE -> IN
                AddInFilterIfAny(comm, "LOCNCODE", "LOCNCODE", filtr.StrojLokace);

                using (SqlDataAdapter ada = new SqlDataAdapter(comm))
                {
                    ada.Fill(ds.Machines);
                }
            }
            finally
            {
                if (comm != null)
                    comm.Dispose();

                if (conn != null)
                    conn.Dispose();
            }

            return ds;
        }

        private static void AddContainsFilterIfAny(
    SqlCommand comm,
    string columnName,
    string paramName,
    string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return;

            string value = raw.Trim();

            comm.CommandText += $" AND ( {columnName} LIKE @{paramName} ) ";
            comm.Parameters.Add("@" + paramName, SqlDbType.NVarChar)
                .Value = "%" + value + "%";
        }

        private static void AddInFilterIfAny(
         SqlCommand comm,
         string columnName,
         string paramBase,
         string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return;

            string[] values = raw
                .Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            if (values.Length == 0)
                return;

            if (values.Length == 1)
            {
                string p = "@" + paramBase;
                comm.CommandText += " AND ( " + columnName + " = " + p + " ) ";
                comm.Parameters.Add(p, SqlDbType.NVarChar).Value = values[0];
                return;
            }

            List<string> paramNames = new List<string>();

            for (int i = 0; i < values.Length; i++)
            {
                string p = "@" + paramBase + "_" + i;
                paramNames.Add(p);
                comm.Parameters.Add(p, SqlDbType.NVarChar).Value = values[i];
            }

            comm.CommandText +=
                " AND ( " + columnName + " IN (" + string.Join(", ", paramNames) + ") ) ";
        }

        #endregion

        public static int DeleteById(string id, string CS)
        {
             var conn = new SqlConnection(CS);
             var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_Machines + " WHERE id = @id";
            cmd.Parameters.Add("@id", SqlDbType.NVarChar, 20).Value = id;
            conn.Open();
            return cmd.ExecuteNonQuery();
        }


    }
}
