using Fask.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Ingres.SAD.Database
{
    public class Vyroba_Machines
    {        

        public static int Update(object data, string CS)
        {
            IngresTransaction transaction = null;
            IngresConnection conn = null;
            int result = 0;
            try
            {

                using (conn = new IngresConnection(CS))
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


                        //InitializeCommandInsert_Machines(commandInsert);
                        InitializeCommandUpdate_Machines(commandUpdate);
                        //InitializeCommandDelete_Machines(commandDelete);
                        InitializeCommandSelect_Machines(commandSelect);

                        using (var adapter = new IngresDataAdapter())
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

        private static void InitializeCommandInsert_Machines(IngresCommand command)
        {
            //command.CommandText =
            //    @" INSERT INTO " + Tables.TABLE_Machines +
            //" (CountEntries, SOPNUMBE, SOPTYPE, SOPDESC, VNDDOCNMH, BarcodeH, LOCNCODE, DateProd, Rez1, Rez2, TermID, LSTMod, Active)" +
            //" VALUES (@CountEntries,@SOPNUMBE,@SOPTYPE,@SOPDESC,@VNDDOCNMH,@BarcodeH,@LOCNCODE,@DateProd,@Rez1,@Rez2,@TermID,@LSTMod,@Active)";

            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@SOPTYPE", DbType = DbType.String, SourceColumn = "SOPTYPE", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@SOPDESC", DbType = DbType.String, SourceColumn = "SOPDESC", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@VNDDOCNMH", DbType = DbType.String, SourceColumn = "VNDDOCNMH", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@BarcodeH", DbType = DbType.String, SourceColumn = "BarcodeH", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@DateProd", DbType = DbType.Int16, SourceColumn = "DateProd", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@Rez1", DbType = DbType.String, SourceColumn = "Rez1", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@Rez2", DbType = DbType.String, SourceColumn = "Rez2", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@LSTMod", DbType = DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@Active", DbType = DbType.Byte, SourceColumn = "Active", SourceVersion = DataRowVersion.Current });


        }

        private static void InitializeCommandUpdate_Machines(IngresCommand command)
        {

            command.CommandText = "UPDATE " + Fask.SQL.Constants.Common.TABLE_Machines +
                                    " SET [name] = @name" +
                                    " ,[description] = @description" +
                                    " WHERE (id = @id)";

            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@name", DbType = DbType.String, SourceColumn = "name", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@id", DbType = DbType.String, SourceColumn = "id", SourceVersion = DataRowVersion.Original });

        }

        private static void InitializeCommandDelete_Machines(IngresCommand command)
        {
            command.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_Machines + " WHERE (id = @id)";


            command.Parameters.Add(new IngresParameter()
            {
                ParameterName = "@id",
                DbType = DbType.String,
                SourceColumn = "id",
                SourceVersion = DataRowVersion.Original

            });
        }

        private static void InitializeCommandSelect_Machines(IngresCommand command)
        {
            command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Machines;
        }


        #endregion

        public static int Machines_Insert(string CS, string Name, string Description)
        {
            IngresConnection conn = null;
            IngresCommand comm = null;
            int returnValue = 0;

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText =
                         @" INSERT INTO " + Fask.SQL.Constants.Common.TABLE_Machines +
                            " ([name], [description])" +
                            " VALUES (@name, @description)";


                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@name", DbType = DbType.String, SourceColumn = "name", Value = Name == null ? (object)DBNull.Value : Name }); 
                    
                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", Value = Description == null ? (object)DBNull.Value : Description });

                    comm.CommandType = CommandType.Text;

                    comm.ExecuteNonQuery();
                    //using (var ada = new IngresDataAdapter())
                    //{
                    //    ada.InsertCommand = comm;
                    //    //ds.Production.Clear();
                    //    returnValue = ada.InsertCommand.ExecuteNonQuery();
                    //}
                }
            }
            return returnValue;
        }
        public static Fask.Interfaces.DataSets.Vyroba.MachinesDataTable Machines_GetDataByID(string CS, string ID)
        {

            IngresConnection conn = null;
            IngresCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.MachinesDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.MachinesDataTable();

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    //comm.CommandText = "Select * from " + Tables.TABLE_Machines;
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Machines +
                        " WHERE ( id = @id ) ";


                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@id", DbType = DbType.String, SourceColumn = "id", Value = ID == null ? (object)DBNull.Value : ID });

                    comm.CommandType = CommandType.Text;
                    using (var ada = new IngresDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        ada.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }
        public static int Machines_FillByID(string CS, Fask.Interfaces.DataSets.Vyroba ds, string ID)
        {
            IngresConnection conn = null;
            IngresCommand comm = null;
            int returnValue = 0;

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Machines +
                        " WHERE ( id = @id ) ";


                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@id", DbType = DbType.String, SourceColumn = "id", Value = ID == null ? (object)DBNull.Value : ID });

                    comm.CommandType = CommandType.Text;
                    using (var ada = new IngresDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        //ds.Production.Clear();
                        returnValue = ada.Fill(ds, ds.Production_Konzola.TableName);
                    }
                }
            }
            return returnValue;
        }
        public static Fask.Interfaces.DataSets.Vyroba.MachinesDataTable Machines_GetData(string CS)
        {

            IngresConnection conn = null;
            IngresCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.MachinesDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.MachinesDataTable();

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Machines ;

                    comm.CommandType = CommandType.Text;
                    using (var ada = new IngresDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        ada.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }
        public static int Machines_Fill(string CS, Fask.Interfaces.DataSets.Vyroba ds)
        {
            IngresConnection conn = null;
            IngresCommand comm = null;
            int returnValue = 0;

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Machines;
                    comm.CommandType = CommandType.Text;
                    using (var ada = new IngresDataAdapter())
                    {
                        ada.SelectCommand = comm;//ds.Production.Clear();
                        returnValue = ada.Fill(ds, ds.Machines.TableName);
                        
                    }
                }
            }
            return returnValue;
        }

    }
}
