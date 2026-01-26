using Fask.Server.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql.Database
{
    public class Vyroba_Groups
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


                       // InitializeCommandInsert_Groups(commandInsert);
                        InitializeCommandUpdate_Groups(commandUpdate);
                        //InitializeCommandDelete_Groups(commandDelete);
                        InitializeCommandSelect_Groups(commandSelect);

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

        private static void InitializeCommandInsert_Groups(SqlCommand command)
        {
            command.CommandText =
                @" INSERT INTO " + Fask.SQL.Constants.Common.TABLE_Groups +
            " (CountEntries, SOPNUMBE, SOPTYPE, SOPDESC, VNDDOCNMH, BarcodeH, LOCNCODE, DateProd, Rez1, Rez2, TermID, LSTMod, Active)" +
            " VALUES (@CountEntries,@SOPNUMBE,@SOPTYPE,@SOPDESC,@VNDDOCNMH,@BarcodeH,@LOCNCODE,@DateProd,@Rez1,@Rez2,@TermID,@LSTMod,@Active)";

            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@SOPTYPE", DbType = DbType.String, SourceColumn = "SOPTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@SOPDESC", DbType = DbType.String, SourceColumn = "SOPDESC", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@VNDDOCNMH", DbType = DbType.String, SourceColumn = "VNDDOCNMH", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@BarcodeH", DbType = DbType.String, SourceColumn = "BarcodeH", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@DateProd", DbType = DbType.Int16, SourceColumn = "DateProd", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@Rez1", DbType = DbType.String, SourceColumn = "Rez1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@Rez2", DbType = DbType.String, SourceColumn = "Rez2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@LSTMod", DbType = DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@Active", DbType = DbType.Byte, SourceColumn = "Active", SourceVersion = DataRowVersion.Current });


        }

        private static void InitializeCommandUpdate_Groups(SqlCommand command)
        {

            command.CommandText = "UPDATE " + Fask.SQL.Constants.Common.TABLE_Groups +
                                    " SET [name] = @name" +
                                    " ,[description] = @description" +
                                    " WHERE (id = @id)";

            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@name", DbType = DbType.String, SourceColumn = "name", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@id", DbType = DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Original });

        }

        private static void InitializeCommandDelete_Groups(SqlCommand command)
        {
            command.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_Groups + " WHERE (id = @id)";


            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@id",
                DbType = DbType.String,
                SourceColumn = "id",
                SourceVersion = DataRowVersion.Original

            });
        }

        private static void InitializeCommandSelect_Groups(SqlCommand command)
        {
            command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Groups;
        }


        #endregion

        public static int Groups_Insert(string CS, string Name, string Description)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText =
                         @" INSERT INTO " + Fask.SQL.Constants.Common.TABLE_Groups +
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



        public static Fask.Interfaces.DataSets.Vyroba.GroupsDataTable Groups_GetDataByID(string CS, string ID)
        {

            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.GroupsDataTable();

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    //comm.CommandText = "Select * from " + Tables.TABLE_Groups;
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Groups +
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


        public static int Groups_FillByID(string CS, Fask.Interfaces.DataSets.Vyroba ds, string ID)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Groups +
                        " WHERE ( id = @id ) ";


                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@id", DbType = DbType.String, SourceColumn = "id", Value = ID == null ? (object)DBNull.Value : ID });

                    comm.CommandType = CommandType.Text;
                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        //ds.Production.Clear();
                        returnValue = ada.Fill(ds, ds.Production.TableName);
                    }
                }
            }
            return returnValue;
        }


        public static Fask.Interfaces.DataSets.Vyroba.GroupsDataTable Groups_GetData(string CS)
        {

            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.GroupsDataTable();


            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Groups ;

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
        public static int Groups_Fill(string CS, Fask.Interfaces.DataSets.Vyroba ds)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Groups;
                    comm.CommandType = CommandType.Text;
                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;//ds.Production.Clear();
                        returnValue = ada.Fill(ds, ds.Groups.TableName);
                        
                    }
                }
            }
            return returnValue;
        }

    }
}
