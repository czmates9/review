using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql.Database
{
    class Vyroba_FaskEvents
    {
        private const string TableName_ = "Fask_Events";


        public static int Update(object data)
        {
            System.Data.SqlClient.SqlTransaction transaction = null;
            System.Data.SqlClient.SqlConnection conn = null;
            int result = 0;
            try
            {
                Globals.LoadConfiguration();

                using (conn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
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


                        InitializeCommandInsert_FaskEvents(commandInsert);
                        InitializeCommandUpdate_FaskEvents(commandUpdate);
                        InitializeCommandDelete_FaskEvents(commandDelete);
                        InitializeCommandSelect_FaskEvents(commandSelect);

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
               // Logging.Log.Write(ex);
                Logging.ExceptionHandler2.Handle(ex);

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

        private static void InitializeCommandInsert_FaskEvents(SqlCommand command)
        {
            command.CommandText =
            @" INSERT INTO " + TableName_ +
            " ( loginid, machineid, dateeve, qty, qtyReal, description, barcodeReaded, barcodeSended, zakazka, popis, faskGUID, reportType, isProcessed, IDO, scan1, scan2, scan3, sensor, material, VPH, VPPol, EAN_IS, IS_ID, NMBRPAL, status, productionGuid, QTYPACK, PackType, WEIGHT)" +
            " VALUES (@loginid, @machineid, @dateeve, @qty, @qtyReal, @description, @barcodeReaded, @barcodeSended, @zakazka, @popis, @faskGUID, @reportType, @isProcessed, @IDO, @scan1, @scan2, @scan3, @sensor, @material, @VPH, @VPPol, @EAN_IS, @IS_ID, @NMBRPAL, @status, @productionGuid, @QTYPACK, @PackType, @WEIGHT)";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@loginid", DbType = DbType.String, SourceColumn = "loginid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@dateeve", DbType = DbType.DateTime, SourceColumn = "dateeve", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@qty", DbType = DbType.Decimal, SourceColumn = "qty", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@qtyReal", DbType = DbType.Decimal, SourceColumn = "qtyReal", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@barcodeReaded", DbType = DbType.String, SourceColumn = "barcodeReaded", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@barcodeSended", DbType = DbType.String, SourceColumn = "barcodeSended", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@zakazka", DbType = DbType.String, SourceColumn = "zakazka", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@popis", DbType = DbType.String, SourceColumn = "popis", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@faskGUID", DbType = DbType.Guid, SourceColumn = "faskGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@reportType", DbType = DbType.String, SourceColumn = "reportType", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@isProcessed", DbType = DbType.DateTime, SourceColumn = "isProcessed", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IDO", DbType = DbType.String, SourceColumn = "IDO", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@scan1", DbType = DbType.String, SourceColumn = "scan1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@scan2", DbType = DbType.String, SourceColumn = "scan2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@scan3", DbType = DbType.String, SourceColumn = "scan3", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@sensor", DbType = DbType.String, SourceColumn = "sensor", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@material", DbType = DbType.String, SourceColumn = "material", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VPH", DbType = DbType.String, SourceColumn = "VPH", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VPPol", DbType = DbType.Int32, SourceColumn = "VPPol", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@EAN_IS", DbType = DbType.String, SourceColumn = "EAN_IS", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IS_ID", DbType = DbType.String, SourceColumn = "IS_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = DbType.String, SourceColumn = "NMBRPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@status", DbType = DbType.Int32, SourceColumn = "status", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@productionGuid", DbType = DbType.Guid, SourceColumn = "productionGuid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@PackType", DbType = DbType.String, SourceColumn = "PackType", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = DataRowVersion.Current });

        }

        private static void InitializeCommandUpdate_FaskEvents(SqlCommand command)
        {
            command.CommandText =
                @" UPDATE " + TableName_ +
                " SET [loginid] = @loginid," +
                " [machineid] = @machineid," +
                " [dateeve] = @dateeve," +
                " [qty] = @qty," +
                " [qtyReal] = @qtyReal," +
                " [description] = @description," +
                " [barcodeReaded] = @barcodeReaded," +
                " [barcodeSended] = @barcodeSended," +
                " [zakazka] = @zakazka," +
                " [popis] = @popis," +
                " [faskGUID] = @faskGUID," +
                " [reportType] = @reportType," +
                " [isProcessed] = @isProcessed," +
                " [IDO] = @IDO," +
                " [scan1] = @scan1," +
                " [scan2] = @scan2," +
                " [scan3] = @scan3," +
                " [sensor] = @sensor," +
                " [material] = @material," +
                " [VPH] = @VPH," +
                " [VPPol] = @VPPol," +
                " [EAN_IS] = @EAN_IS," +
                " [IS_ID] = @IS_ID," +
                " [NMBRPAL] = @NMBRPAL," +
                " [status] = @status," +
                " [productionGuid] = @productionGuid," +
                " [QTYPACK] = @QTYPACK," +
                " [PackType] = @PackType," +
                " [WEIGHT] = @WEIGHT" +
                " WHERE ([faskGUID] = @Original_faskGUID)";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@loginid", DbType = DbType.String, SourceColumn = "loginid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@dateeve", DbType = DbType.DateTime, SourceColumn = "dateeve", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@qty", DbType = DbType.Decimal, SourceColumn = "qty", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@qtyReal", DbType = DbType.Decimal, SourceColumn = "qtyReal", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@barcodeReaded", DbType = DbType.String, SourceColumn = "barcodeReaded", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@barcodeSended", DbType = DbType.String, SourceColumn = "barcodeSended", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@zakazka", DbType = DbType.String, SourceColumn = "zakazka", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@popis", DbType = DbType.String, SourceColumn = "popis", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@faskGUID", DbType = DbType.Guid, SourceColumn = "faskGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@reportType", DbType = DbType.String, SourceColumn = "reportType", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@isProcessed", DbType = DbType.DateTime, SourceColumn = "isProcessed", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IDO", DbType = DbType.String, SourceColumn = "IDO", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@scan1", DbType = DbType.String, SourceColumn = "scan1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@scan2", DbType = DbType.String, SourceColumn = "scan2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@scan3", DbType = DbType.String, SourceColumn = "scan3", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@sensor", DbType = DbType.String, SourceColumn = "sensor", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@material", DbType = DbType.String, SourceColumn = "material", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VPH", DbType = DbType.String, SourceColumn = "VPH", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VPPol", DbType = DbType.Int32, SourceColumn = "VPPol", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@EAN_IS", DbType = DbType.String, SourceColumn = "EAN_IS", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IS_ID", DbType = DbType.String, SourceColumn = "IS_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = DbType.String, SourceColumn = "NMBRPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@status", DbType = DbType.Int32, SourceColumn = "status", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@productionGuid", DbType = DbType.Guid, SourceColumn = "productionGuid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@PackType", DbType = DbType.String, SourceColumn = "PackType", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_faskGUID", DbType = DbType.String, SourceColumn = "faskGUID", SourceVersion = DataRowVersion.Original });

        }

        private static void InitializeCommandDelete_FaskEvents(SqlCommand command)
        {
            command.CommandText = "DELETE FROM " + TableName_ + " WHERE (faskGUID = @Original_faskGUID)";

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_faskGUID",
                DbType = DbType.String,
                SourceColumn = "faskGUID",
                SourceVersion = DataRowVersion.Original

            });
        }

        private static void InitializeCommandSelect_FaskEvents(SqlCommand command)
        {
            command.CommandText = "Select * from " + TableName_;
        }


        #endregion

    }
}
