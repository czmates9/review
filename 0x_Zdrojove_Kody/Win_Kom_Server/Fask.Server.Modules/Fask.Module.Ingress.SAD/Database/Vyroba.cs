using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.Ingres.SAD.Database
{
    public class Vyroba
    {

        private const string TableName_ = "MachineStateSet";


        public static int Update(object data)
        {
            IngresTransaction transaction = null;
            IngresConnection conn = null;
            int result = 0;
            try
            {
                Globals.LoadConfiguration();

                using (conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
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


                        InitializeCommandInsert_MachineStateSet(commandInsert);
                        InitializeCommandUpdate_MachineStateSet(commandUpdate);
                        InitializeCommandDelete_MachineStateSet(commandDelete);
                        InitializeCommandSelect_MachineStateSet(commandSelect);

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

        private static void InitializeCommandInsert_MachineStateSet(IngresCommand command)
        {
            command.CommandText =
            @" INSERT INTO " + TableName_ +
            " (IP, DateModified, S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10, S11, LastError,counter_0,counter_1,counter_2,counter_3,counter_4,counter_5,counter_6,counter_7,counter_8,counter_9,counter_10,counter_11,ID_group)" +
            " VALUES (@IP,@DateModified,@S0,@S1,@S2,@S3,@S4,@S5,@S6,@S7,@S8,@S9,@S10,@S11,@LastError,@counter_0,@counter_1,@counter_2,@counter_3,@counter_4,@counter_5,@counter_6,@counter_7,@counter_8,@counter_9,@counter_10,@counter_11,@ID_group)";


            command.Parameters.Add(new IngresParameter() { ParameterName = "@IP", DbType = DbType.String, SourceColumn = "IP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@DateModified", DbType = DbType.DateTime, SourceColumn = "DateModified", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S0", DbType = DbType.Int32, SourceColumn = "S0", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S1", DbType = DbType.Int32, SourceColumn = "S1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S2", DbType = DbType.Int32, SourceColumn = "S2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S3", DbType = DbType.Int32, SourceColumn = "S3", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S4", DbType = DbType.Int32, SourceColumn = "S4", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S5", DbType = DbType.Int32, SourceColumn = "S5", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S6", DbType = DbType.Int32, SourceColumn = "S6", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S7", DbType = DbType.Int32, SourceColumn = "S7", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S8", DbType = DbType.Int32, SourceColumn = "S8", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S9", DbType = DbType.Int32, SourceColumn = "S9", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S10", DbType = DbType.Int32, SourceColumn = "S10", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@S11", DbType = DbType.Int32, SourceColumn = "S11", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_0", DbType = DbType.Int32, SourceColumn = "counter_0", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_1", DbType = DbType.Int32, SourceColumn = "counter_1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_2", DbType = DbType.Int32, SourceColumn = "counter_2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_3", DbType = DbType.Int32, SourceColumn = "counter_3", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_4", DbType = DbType.Int32, SourceColumn = "counter_4", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_5", DbType = DbType.Int32, SourceColumn = "counter_5", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_6", DbType = DbType.Int32, SourceColumn = "counter_6", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_7", DbType = DbType.Int32, SourceColumn = "counter_7", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_8", DbType = DbType.Int32, SourceColumn = "counter_8", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_9", DbType = DbType.Int32, SourceColumn = "counter_9", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_10", DbType = DbType.Int32, SourceColumn = "counter_10", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@counter_11", DbType = DbType.Int32, SourceColumn = "counter_11", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter() { ParameterName = "@LastError", DbType = DbType.String, SourceColumn = "LastError", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new IngresParameter() { ParameterName = "@ID_group", DbType = DbType.Int32, SourceColumn = "ID_group", SourceVersion = DataRowVersion.Current });
        }

        private static void InitializeCommandUpdate_MachineStateSet(IngresCommand command)
        {
            command.CommandText =
                @" UPDATE " + TableName_ +
                " SET [IP] = @IP," +
                " [DateModified] = @DateModified," +
                " [S0] = @S0," +
                " [S1] = @S1," +
                " [S2] = @S2," +
                " [S3] = @S3," +
                " [S4] = @S4," +
                " [S5] = @S5," +
                " [S6] = @S6," +
                " [S7] = @S7," +
                " [S8] = @S8," +
                " [S9] = @S9," +
                " [S10] = @S10," +
                " [S11] = @S11," +
                " [counter_0] = @counter_0," +
                " [counter_1] = @counter_1," +
                " [counter_2] = @counter_2," +
                " [counter_3] = @counter_3," +
                " [counter_4] = @counter_4," +
                " [counter_5] = @counter_5," +
                " [counter_6] = @counter_6," +
                " [counter_7] = @counter_7," +
                " [counter_8] = @counter_8," +
                " [counter_9] = @counter_9," +
                " [counter_10] = @counter_10," +
                " [counter_11] = @counter_11," +
                " [LastError] = @LastError," +
                " [ID_group] = @ID_group" +
                " WHERE ([IP] = @Original_IP)";

            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@IP", DbType = DbType.String, SourceColumn = "IP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@DateModified", DbType = DbType.DateTime, SourceColumn = "DateModified", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S0", DbType = DbType.Int32, SourceColumn = "S0", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S1", DbType = DbType.Int32, SourceColumn = "S1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S2", DbType = DbType.Int32, SourceColumn = "S2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S3", DbType = DbType.Int32, SourceColumn = "S3", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S4", DbType = DbType.Int32, SourceColumn = "S4", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S5", DbType = DbType.Int32, SourceColumn = "S5", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S6", DbType = DbType.Int32, SourceColumn = "S6", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S7", DbType = DbType.Int32, SourceColumn = "S7", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S8", DbType = DbType.Int32, SourceColumn = "S8", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S9", DbType = DbType.Int32, SourceColumn = "S9", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S10", DbType = DbType.Int32, SourceColumn = "S10", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@S11", DbType = DbType.Int32, SourceColumn = "S11", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_0", DbType = DbType.Int32, SourceColumn = "counter_0", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_1", DbType = DbType.Int32, SourceColumn = "counter_1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_2", DbType = DbType.Int32, SourceColumn = "counter_2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_3", DbType = DbType.Int32, SourceColumn = "counter_3", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_4", DbType = DbType.Int32, SourceColumn = "counter_4", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_5", DbType = DbType.Int32, SourceColumn = "counter_5", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_6", DbType = DbType.Int32, SourceColumn = "counter_6", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_7", DbType = DbType.Int32, SourceColumn = "counter_7", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_8", DbType = DbType.Int32, SourceColumn = "counter_8", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_9", DbType = DbType.Int32, SourceColumn = "counter_9", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_10", DbType = DbType.Int32, SourceColumn = "counter_10", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@counter_11", DbType = DbType.Int32, SourceColumn = "counter_11", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@LastError", DbType = DbType.String, SourceColumn = "LastError", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ID_group", DbType = DbType.Int32, SourceColumn = "ID_group", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@Original_IP", DbType = DbType.String, SourceColumn = "IP", SourceVersion = DataRowVersion.Original });

        }

        private static void InitializeCommandDelete_MachineStateSet(IngresCommand command)
        {
            command.CommandText = "DELETE FROM " + TableName_ + " WHERE (IP = @Original_IP)";

            command.Parameters.Add(new IngresParameter()
            {
                ParameterName = "@Original_IP",
                DbType = DbType.String,
                SourceColumn = "IP",
                SourceVersion = DataRowVersion.Original

            });
        }

        private static void InitializeCommandSelect_MachineStateSet(IngresCommand command)
        {
            command.CommandText = "Select * from " + TableName_;
        }


        #endregion

    }
}
