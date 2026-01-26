using Fask.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModulePohodaXML.Database
{
    public class Vyroba_Production
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


                        //InitializeCommandInsert_Production(commandInsert);
                        InitializeCommandUpdate_Production(commandUpdate);
                        //InitializeCommandDelete_Production(commandDelete);
                        InitializeCommandSelect_Production(commandSelect);

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

           
                var dataIsDataTable = data as DataTable;

                if(dataIsDataTable != null)
                    Logging.ExceptionHandler2.Handle((DataTable)data);

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

        //private static void InitializeCommandInsert_Production(SqlCommand command)
        //{

        // //   ([CountEntries],[SOPNUMBE],[ITEMNMBR],[ITEMTYPE],[ITEMMJ],[ITEMDESC],[ORD],[TIMEMODE],[TIMEPREPSTART],[TIMEPREPSTOP]
        // //,[TIMEPREP],[TIMEUNIT],[TIMESTART],[TIMESTOP],[TIMECORSTART],[TIMECORSTOP],[TIMECOR],[TIMECRID],[TIMECRIDTYPE]
        // //,[loginid],[machineid],[operationid],[dateeve],[qty],[qtyReal],[QTYPACK],[QTYPACKMJ],[description],[BarcodeP]
        // //,[UserID],[TermID],[ISOK],[GUID],[SOUBEHGUID],[CORRGUID],[qtyOld],[idVS],[dateedit],[SKL_ID],[LOCNCODE]
        // //,[SERLTNUM],[EXPIRATION],[NMBRPAL],[TYPEPAL],[PackType],[status],[WEIGHT],[STORNOGUID])

        //    command.CommandText =
        //        @" INSERT INTO " + Fask.SQL.Constants.Common.TABLE_Production +
        //    " (CountEntries, SOPNUMBE, SOPTYPE, SOPDESC, VNDDOCNMH, BarcodeH, LOCNCODE, DateProd, Rez1, Rez2, TermID, LSTMod, Active)" +
        //    " VALUES (@CountEntries,@SOPNUMBE,@SOPTYPE,@SOPDESC,@VNDDOCNMH,@BarcodeH,@LOCNCODE,@DateProd,@Rez1,@Rez2,@TermID,@LSTMod,@Active)";

        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@SOPTYPE", DbType = DbType.String, SourceColumn = "SOPTYPE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@SOPDESC", DbType = DbType.String, SourceColumn = "SOPDESC", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@VNDDOCNMH", DbType = DbType.String, SourceColumn = "VNDDOCNMH", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@BarcodeH", DbType = DbType.String, SourceColumn = "BarcodeH", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@DateProd", DbType = DbType.Int16, SourceColumn = "DateProd", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@Rez1", DbType = DbType.String, SourceColumn = "Rez1", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@Rez2", DbType = DbType.String, SourceColumn = "Rez2", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@LSTMod", DbType = DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new SqlParameter()
        //    { ParameterName = "@Active", DbType = DbType.Byte, SourceColumn = "Active", SourceVersion = DataRowVersion.Current });


        //}

        private static void InitializeCommandUpdate_Production(SqlCommand command)
        {

            #region new pohoda

           

            command.CommandText =   "UPDATE " + Fask.SQL.Constants.Common.TABLE_Production +
                                    " SET " +
                                    "  [CountEntries] = @CountEntries" +
                                    " ,[SOPNUMBE] = @SOPNUMBE" +
                                    " ,[ITEMNMBR] = @ITEMNMBR" +
                                    " ,[ITEMTYPE] = @ITEMTYPE" +
                                    " ,[ITEMMJ] = @ITEMMJ" +
                                    " ,[ITEMDESC] = @ITEMDESC" +
                                    " ,[ORD] = @ORD" +
                                    " ,[TIMEMODE] = @TIMEMODE" +
                                    " ,[TIMEPREPSTART] = @TIMEPREPSTART" +
                                    " ,[TIMEPREPSTOP] = @TIMEPREPSTOP" +
                                    " ,[TIMEPREP] = @TIMEPREP" +
                                    " ,[TIMEUNIT] = @TIMEUNIT" +
                                    " ,[TIMESTART] = @TIMESTART" +
                                    " ,[TIMESTOP] = @TIMESTOP" +
                                    " ,[TIMECORSTART] = @TIMECORSTART" +
                                    " ,[TIMECORSTOP] = @TIMECORSTOP" +
                                    " ,[TIMECOR] = @TIMECOR" +
                                    " ,[TIMECRID] = @TIMECRID" +
                                    " ,[TIMECRIDTYPE] = @TIMECRIDTYPE" +
                                    " ,[loginid] = @loginid" +
                                    " ,[machineid] = @machineid" +
                                    " ,[operationid] = @operationid" +
                                    " ,[dateeve] = @dateeve" +
                                    " ,[qty] = @qty" +
                                    " ,[qtyReal] = @qtyReal" +
                                    " ,[QTYPACK] = @QTYPACK" +
                                    " ,[QTYPACKMJ] = @QTYPACKMJ" +
                                    " ,[description] = @description" +
                                    " ,[BarcodeP] = @BarcodeP" +
                                    " ,[UserID] = @UserID" +
                                    " ,[TermID] = @TermID" +
                                    " ,[ISOK] = @ISOK" +
                                    " ,[GUID] = @GUID" +
                                    " ,[SOUBEHGUID] = @SOUBEHGUID" +
                                    " ,[CORRGUID] = @CORRGUID" +
                                    " ,[qtyOld] = @qtyOld" +
                                    " ,[idVS] = @idVS" +
                                    " ,[dateedit] = @dateedit" +
                                    " ,[SKL_ID] = @SKL_ID" +
                                    " ,[LOCNCODE] = @LOCNCODE" +
                                    " ,[SERLTNUM] = @SERLTNUM" +
                                    " ,[EXPIRATION] = @EXPIRATION" +
                                    " ,[NMBRPAL] = @NMBRPAL" +
                                    " ,[TYPEPAL] = @TYPEPAL" +
                                    " ,[PackType] = @PackType" +
                                    " ,[status] = @status" +
                                    " ,[WEIGHT] = @WEIGHT" +
                                    " ,[STORNOGUID] = @STORNOGUID" +
                                    " ,[REZ_1] = @REZ_1" +
                                    " ,[REZ_2] = @REZ_2" +
                                    " ,[REZ_3] = @REZ_3" +
                                    " ,[REZ_4] = @REZ_4" +
                                    " ,[REZ_5] = @REZ_5" +
                                    " ,[WEIGHT_OLD] = @WEIGHT_OLD" +
                                    " WHERE (id = @id)";

            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@ITEMTYPE", DbType = DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@ITEMMJ", DbType = DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@ITEMDESC", DbType = DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@ORD", DbType = DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMEMODE", DbType = DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMEPREPSTART", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMEPREPSTOP", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMEPREP", DbType = DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMEUNIT", DbType = DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMESTART", DbType = DbType.DateTime, SourceColumn = "TIMESTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMESTOP", DbType = DbType.DateTime, SourceColumn = "TIMESTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMECORSTART", DbType = DbType.DateTime, SourceColumn = "TIMECORSTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMECORSTOP", DbType = DbType.DateTime, SourceColumn = "TIMECORSTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMECOR", DbType = DbType.Single, SourceColumn = "TIMECOR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMECRID", DbType = DbType.Int32, SourceColumn = "TIMECRID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMECRIDTYPE", DbType = DbType.Byte, SourceColumn = "TIMECRIDTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@loginid", DbType = DbType.String, SourceColumn = "loginid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@operationid", DbType = DbType.String, SourceColumn = "operationid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@dateeve", DbType = DbType.DateTime, SourceColumn = "dateeve", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@qty", DbType = DbType.Decimal, SourceColumn = "qty", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@qtyReal", DbType = DbType.Decimal, SourceColumn = "qtyReal", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@QTYPACKMJ", DbType = DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@BarcodeP", DbType = DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@UserID", DbType = DbType.String, SourceColumn = "UserID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@ISOK", DbType = DbType.DateTime, SourceColumn = "ISOK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@GUID", DbType = DbType.Guid, SourceColumn = "GUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@SOUBEHGUID", DbType = DbType.Guid, SourceColumn = "SOUBEHGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@CORRGUID", DbType = DbType.Guid, SourceColumn = "CORRGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@qtyOld", DbType = DbType.Decimal, SourceColumn = "qtyOld", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@idVS", DbType = DbType.String, SourceColumn = "idVS", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@dateedit", DbType = DbType.DateTime, SourceColumn = "dateedit", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@SKL_ID", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@SERLTNUM", DbType = DbType.String, SourceColumn = "SERLTNUM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@EXPIRATION", DbType = DbType.String, SourceColumn = "EXPIRATION", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@NMBRPAL", DbType = DbType.String, SourceColumn = "NMBRPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TYPEPAL", DbType = DbType.String, SourceColumn = "TYPEPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@PackType", DbType = DbType.String, SourceColumn = "PackType", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@status", DbType = DbType.Int32, SourceColumn = "status", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@WEIGHT", DbType = DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@STORNOGUID", DbType = DbType.Guid, SourceColumn = "STORNOGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@REZ_1", DbType = DbType.String, SourceColumn = "REZ_1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@REZ_2", DbType = DbType.String, SourceColumn = "REZ_2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@REZ_3", DbType = DbType.String, SourceColumn = "REZ_3", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@REZ_4", DbType = DbType.String, SourceColumn = "REZ_4", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@REZ_5", DbType = DbType.String, SourceColumn = "REZ_5", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@WEIGHT_OLD", DbType = DbType.Decimal, SourceColumn = "WEIGHT_OLD", SourceVersion = DataRowVersion.Current });



            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@id", DbType = DbType.Int32, SourceColumn = "id", SourceVersion = DataRowVersion.Original });


            #endregion

        }

        //private static void InitializeCommandDelete_Production(SqlCommand command)
        //{
        //    command.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_Production + " WHERE (CountEntries = @Original_CountEntries) AND (SOPNUMBE = @Original_SOPNUMBE)";


        //    command.Parameters.Add(new SqlParameter()
        //    {
        //        ParameterName = "@Original_CountEntries",
        //        DbType = DbType.Int32,
        //        SourceColumn = "CountEntries",
        //        SourceVersion = DataRowVersion.Original

        //    });

        //    command.Parameters.Add(new SqlParameter()
        //    {
        //        ParameterName = "@Original_SOPNUMBE",
        //        DbType = DbType.String,
        //        SourceColumn = "SOPNUMBE",
        //        SourceVersion = DataRowVersion.Original

        //    });
        //}

        private static void InitializeCommandSelect_Production(SqlCommand command)
        {
            command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Production;
        }


        #endregion

        public static Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataBySOUBEHGUID(string CS, Guid SOUBEHGUID)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                        " WHERE ( SOUBEHGUID = @SOUBEHGUID ) ";

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@SOUBEHGUID", DbType = DbType.Guid, SourceColumn = "SOUBEHGUID", Value = SOUBEHGUID == null ? (object)DBNull.Value : SOUBEHGUID });

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

        public static int Production_FillBySOUBEHGUID(string CS, Fask.Interfaces.DataSets.Vyroba ds, Guid SOUBEHGUID)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                              " WHERE ( SOUBEHGUID = @SOUBEHGUID ) ";

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@SOUBEHGUID", DbType = DbType.Guid, SourceColumn = "SOUBEHGUID", Value = SOUBEHGUID == null ? (object)DBNull.Value : SOUBEHGUID });


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

        public static Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataByCountEntriesAndSOPNUMBEandITEMNMBR(string CS, int CountEntries, string SOPNUMBE, string ITEMNMBR)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                      " WHERE (CountEntries = @CountEntries)" +
                      " AND (SOPNUMBE = @SOPNUMBE)" +
                      " AND (ITEMNMBR = @ITEMNMBR)" +
                      " ORDER BY dateeve DESC";

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries });

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });

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

        public static int Production_FillByCountEntriesAndSOPNUMBEandITEMNMBR(string CS, Fask.Interfaces.DataSets.Vyroba ds, int CountEntries, string SOPNUMBE, string ITEMNMBR)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                       " WHERE (CountEntries = @CountEntries)" +
                       " AND (SOPNUMBE = @SOPNUMBE)" +
                       " AND (ITEMNMBR = @ITEMNMBR)" +
                       " ORDER BY dateeve DESC";

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries });

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });


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

        public static Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataByCountEntriesAndSOPNUMBE(string CS, int CountEntries, string SOPNUMBE)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                        " WHERE (CountEntries = @CountEntries)" +
                        " AND (SOPNUMBE = @SOPNUMBE)";

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries });

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

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

        public static int Production_FillByCountEntriesAndSOPNUMBE(string CS, Fask.Interfaces.DataSets.Vyroba ds, int CountEntries, string SOPNUMBE)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                        " WHERE (CountEntries = @CountEntries)" +
                        " AND (SOPNUMBE = @SOPNUMBE)";

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries});

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

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

        public static Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataByCORRGUID(string CS, Guid CORRGUID)
        {

            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    //comm.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Production;
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                        " WHERE ( CORRGUID = @CORRGUID ) ";

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@CORRGUID", DbType = DbType.Guid, SourceColumn = "CORRGUID", Value = CORRGUID == null ? (object)DBNull.Value : CORRGUID });

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

        public static int Production_FillByCORRGUID(string CS, Fask.Interfaces.DataSets.Vyroba ds, Guid CORRGUID)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                        " WHERE ( CORRGUID = @CORRGUID ) ";

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@CORRGUID", DbType = DbType.Guid, SourceColumn = "CORRGUID", Value = CORRGUID == null ? (object)DBNull.Value : CORRGUID });

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

       public static int Production_Fill(string CS, Fask.Interfaces.DataSets.Vyroba ds)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Production;
                    comm.CommandType = CommandType.Text;
                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;//ds.Production.Clear();
                        returnValue = ada.Fill(ds, ds.Production_Konzola.TableName);
                        
                    }
                }
            }
            return returnValue;
        }

        public static Fask.Interfaces.DataSets.Vyroba GetFiltrovanyProductionVazby(string CS, Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(CS);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;

            command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_Production + " as P ";
            command.CommandText += " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as sklad ON sklad.skl_id = P.SKL_ID ";

            command.CommandText += " WHERE ";
            command.CommandText += "1=1 ";

            command.CommandText += " AND ( P.TIMESTOP is not null or P.TIMEPREPSTOP is not null) ";

            if (!string.IsNullOrEmpty(filtr.MaterialITEMNMBR))
            {
                command.CommandText += " AND P.ITEMNMBR=@ITEMNMBR";
                command.Parameters.AddWithValue("@ITEMNMBR", filtr.MaterialITEMNMBR.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.MaterialITEMDESC))
            {
                command.CommandText += " AND P.ITEMDESC like @ITEMDESC + '%'";
                command.Parameters.AddWithValue("@ITEMDESC", filtr.MaterialITEMDESC.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.MaterialMJ))
            {
                command.CommandText += " AND P.ITEMMJ=@ITEMMJ";
                command.Parameters.AddWithValue("@ITEMMJ", filtr.MaterialMJ.Trim());
            }

            command.CommandText += " order by P.dateeve desc";


            vyrobaDataSet1.Production_Odvod.Clear();
            vyrobaDataSet1.Production_Odvod.AcceptChanges();
            vyrobaDataSet1.Production_Odvod.BeginLoadData();
            adapter.SelectCommand = command;
            adapter.Fill(vyrobaDataSet1.Production_Odvod);
            vyrobaDataSet1.Production_Odvod.EndLoadData();

            return vyrobaDataSet1;
        }

       public static Fask.Interfaces.DataSets.Vyroba GetFiltrovanyProductionList(string CS, Fask.Interfaces.Filtry.ProductionListFiltr filtr)
        {

            #region SQL 

            //Select
            //l.firstname, 
            //l.surname,
            //z.ITEMDESC,
            //o.name operationName,
            //m.name machineName,
            //g.name groupName,
            //sklady.skl_desc skladName,
            //lokace.Barcode lokaceKod,
            //lokace.Description lokaceName,
            //hlavicky.SOPDESC popiszakazky,
            //p.*
            //from Production p
            //left
            //join (
            //select distinct ITEMDESC, ITEMNMBR from FASK_ZASOBY
            //) z on z.itemnmbr = p.itemnmbr
            //left join FASK_Logins l on l.USERID = p.userid
            //left join Operations o on o.id = p.operationid
            //left join Machines m on m.id = p.machineid
            //left join VLoginsGroups vg on l.USERID = vg.loginid
            //left join Groups g on vg.groupid = g.id
            //left join CZMST093 sklady on sklady.skl_id = p.skl_id
            //left join CZMST094 lokace on lokace.skl_id = p.skl_id and lokace.locncode = p.locncode
            //left join CZPRO_VPH hlavicky on hlavicky.SOPNUMBE = p.SOPNUMBE
            //Where 1 = 1

            //order by p.dateeve desc

            #endregion

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(CS);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;

            #region 8.9.2025 MaR oprava OLD
            //command.CommandText =
            //      "Select l.firstname, l.surname,";
            //if (filtr.VyrobaPouzivatTabulkuZbozi)
            //    command.CommandText += "z.ITEMDESC,";
            //command.CommandText +=
            //    "o.name operationName, m.name machineName, g.name groupName" +
            //    ", sklady.skl_desc skladName" +
            //    ", lokace.Barcode lokaceKod, lokace.Description lokaceName" +
            //    ", hlavicky.SOPDESC popiszakazky" +
            //    ", p.* from " + Fask.SQL.Constants.Common.TABLE_Production + " p";


            //if (filtr.VyrobaPouzivatTabulkuZbozi)
            //{
            //    command.CommandText += " left join (select distinct ITEMDESC, ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + ") z on z.itemnmbr = p.itemnmbr";
            //}

            //command.CommandText += " left join " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " l on l.USERID = p.userid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_Operations + " o on o.id = p.operationid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_Machines + " m on m.id = p.machineid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_VLoginsGroups + " vg on l.USERID = vg.loginid" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_Groups + " g on vg.groupid = g.id" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklady on sklady.skl_id = p.skl_id" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " lokace on lokace.skl_id = p.skl_id and lokace.locncode=p.locncode" +
            //    " left join " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " hlavicky on hlavicky.SOPNUMBE = p.SOPNUMBE"; 
            #endregion


            #region 8.9.2025 MaR oprava NEW


            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("  l.firstname AS firstname,");
            sb.AppendLine("  l.surname   AS surname,");

            // ITEMDESC – buď ze zásob (priorita), nebo z Production
            if (filtr.VyrobaPouzivatTabulkuZbozi)
                sb.AppendLine("  z1.ITEMDESC AS ITEMDESC,");
            else
                sb.AppendLine("  p.ITEMDESC  AS ITEMDESC,");

            sb.AppendLine("  o.name  AS operationName,");
            sb.AppendLine("  m.name  AS machineName,");
            sb.AppendLine("  grp.groupName AS groupName,");
            sb.AppendLine("  sklady.skl_desc   AS skladName,");
            sb.AppendLine("  lokace.Barcode    AS lokaceKod,");
            sb.AppendLine("  lokace.[Description] AS lokaceName,");
            sb.AppendLine("  hlavicky.SOPDESC  AS popiszakazky,");

            // ====== Production sloupce v pořadí dle XSD ======
            sb.AppendLine("  p.CountEntries,");
            sb.AppendLine("  p.SOPNUMBE,");
            sb.AppendLine("  p.ITEMNMBR,");
            sb.AppendLine("  p.ITEMTYPE,");
            sb.AppendLine("  p.ITEMMJ,");
            sb.AppendLine("  p.ORD,");
            sb.AppendLine("  p.TIMEMODE,");
            sb.AppendLine("  p.TIMEPREPSTART,");
            sb.AppendLine("  p.TIMEPREPSTOP,");
            sb.AppendLine("  p.TIMEPREP,");
            sb.AppendLine("  p.TIMEUNIT,");
            sb.AppendLine("  p.TIMESTART,");
            sb.AppendLine("  p.TIMESTOP,");
            sb.AppendLine("  p.TIMECORSTART,");
            sb.AppendLine("  p.TIMECORSTOP,");
            sb.AppendLine("  p.TIMECOR,");
            sb.AppendLine("  p.TIMECRID,");
            sb.AppendLine("  p.id,");
            // povinná pole dle XSD – nenechat NULL:
            sb.AppendLine("  COALESCE(p.loginid,'') AS loginid,");
            sb.AppendLine("  p.machineid,");
            sb.AppendLine("  p.operationid,");
            sb.AppendLine("  p.dateeve,");
            sb.AppendLine("  p.qty,");
            sb.AppendLine("  p.qtyReal,");
            sb.AppendLine("  p.QTYPACK,");
            sb.AppendLine("  p.QTYPACKMJ,");
            sb.AppendLine("  p.[description],");
            sb.AppendLine("  p.BarcodeP,");
            sb.AppendLine("  COALESCE(p.UserID,'') AS UserID,");
            sb.AppendLine("  CAST(COALESCE(p.TermID,0) AS TINYINT) AS TermID,");
            sb.AppendLine("  p.ISOK,");
            sb.AppendLine("  CAST(p.GUID       AS VARCHAR(36)) AS GUID,");
            sb.AppendLine("  CAST(p.SOUBEHGUID AS VARCHAR(36)) AS SOUBEHGUID,");
            sb.AppendLine("  p.qtyOld,");
            sb.AppendLine("  p.idVS,");
            sb.AppendLine("  p.dateedit,");
            // firstname, surname už jsou nahoře
            // ITEMDESC už je výše
            // operationName, machineName už výše
            sb.AppendLine("  CAST(p.CORRGUID AS VARCHAR(36)) AS CORRGUID,");
            // groupName už výše
            sb.AppendLine("  p.TIMECRIDTYPE,");
            sb.AppendLine("  p.SKL_ID,");
            // skladName už výše
            sb.AppendLine("  p.LOCNCODE,");
            // lokaceKod, lokaceName už výše
            // popiszakazky už výše
            sb.AppendLine("  p.SERLTNUM,");
            sb.AppendLine("  p.EXPIRATION,");
            sb.AppendLine("  p.WEIGHT,");
            sb.AppendLine("  p.NMBRPAL,");
            sb.AppendLine("  p.TYPEPAL,");
            sb.AppendLine("  p.PackType,");
            sb.AppendLine("  p.[status],");
            sb.AppendLine("  CAST(p.STORNOGUID AS VARCHAR(36)) AS STORNOGUID,");
            sb.AppendLine("  p.REZ_1,");
            sb.AppendLine("  p.REZ_2,");
            sb.AppendLine("  p.REZ_3,");
            sb.AppendLine("  p.REZ_4,");
            sb.AppendLine("  p.REZ_5,");
            sb.AppendLine("  p.WEIGHT_OLD");

            sb.AppendLine($"FROM {Fask.SQL.Constants.Common.TABLE_Production} p");

            // --- bezpečné zjištění ITEMDESC bez rozmnožení (jen pokud povoleno) ---
            if (filtr.VyrobaPouzivatTabulkuZbozi)
            {
                sb.AppendLine("OUTER APPLY (");
                sb.AppendLine($"   SELECT TOP(1) zz.ITEMDESC");
                sb.AppendLine($"   FROM {Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY} zz");
                sb.AppendLine("   WHERE zz.ITEMNMBR = p.ITEMNMBR");
                sb.AppendLine("   ORDER BY zz.ITEMDESC"); // libovolné deterministické pořadí
                sb.AppendLine(") z1");
            }

            sb.AppendLine($"LEFT JOIN {Fask.SQL.Constants.Common.TABLE_FASK_LOGINS} l ON l.USERID = p.UserID");
            sb.AppendLine($"LEFT JOIN {Fask.SQL.Constants.Common.TABLE_Operations}  o ON o.id = p.operationid");
            sb.AppendLine($"LEFT JOIN {Fask.SQL.Constants.Common.TABLE_Machines}    m ON m.id = p.machineid");

            // --- jediná skupina na login (vyhne se duplicitám) ---
            sb.AppendLine("OUTER APPLY (");
            sb.AppendLine($"   SELECT TOP(1) g.name AS groupName");
            sb.AppendLine($"   FROM {Fask.SQL.Constants.Common.TABLE_VLoginsGroups} vg");
            sb.AppendLine($"   JOIN {Fask.SQL.Constants.Common.TABLE_Groups} g ON vg.groupid = g.id");
            sb.AppendLine("   WHERE vg.loginid = l.USERID");
            sb.AppendLine("   ORDER BY g.id");
            sb.AppendLine(") grp");

            sb.AppendLine($"LEFT JOIN {Fask.SQL.Constants.Common.TABLE_CZMST093} sklady ON sklady.skl_id = p.skl_id");
            sb.AppendLine($"LEFT JOIN {Fask.SQL.Constants.Common.TABLE_CZMST094} lokace ON lokace.skl_id = p.skl_id AND lokace.locncode = p.locncode");
            sb.AppendLine($"LEFT JOIN {Fask.SQL.Constants.Common.TABLE_CZPRO_VPH} hlavicky ON hlavicky.SOPNUMBE = p.SOPNUMBE");


            command.CommandText = sb.ToString();

            #endregion

            command.CommandText += " Where ";

            command.CommandText += "1=1 ";
            // číslo zakázky
            if (filtr.rowVPH != null && filtr.rowVPH.Count > 0)
            {
                command.CommandText += " AND p.SOPNUMBE = @SOPNUMBE";
                command.Parameters.AddWithValue("@SOPNUMBE", filtr.rowVPH[0].SOPNUMBE.Trim());
            }
            else if (filtr.VyrobniPrikaz.Length > 0)
            {
                command.CommandText += " AND isnull(p.SOPNUMBE, '') IN(" +
                    "Select SOPNUMBE from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " where (SOPNUMBE like '%' + @SOPNUMBE + '%')" +
                    " union " +
                    "Select SOPNUMBE from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " where (SOPDESC like '%' + @SOPNUMBE + '%')" +
                    ")";

                command.Parameters.AddWithValue("@SOPNUMBE", filtr.VyrobniPrikaz);
            }


            if (filtr.VyrobaPouzivatTabulkuZbozi)
            {
                // hledaní zboží
                if (filtr.rowZbozi != null && filtr.rowZbozi.Count > 0)
                {
                    command.CommandText += " AND p.ITEMNMBR=@itemdesc";
                    command.Parameters.AddWithValue("@itemdesc", filtr.rowZbozi[0].ITEMDESC.Trim());
                }
                else if (filtr.Zbozi_itemdesc.Length > 0)
                {
                    command.CommandText += " AND p.ITEMNMBR IN (" +
                    " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
                    " where ITEMDESC like '%' + @itemdesc + '%'" +
                    " union" +
                    " select ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY +
                    " where ITEMNMBR like '' + @itemdesc + '%' " +
                    " )";
                    command.Parameters.AddWithValue("@itemdesc", filtr.Zbozi_itemdesc);
                }

            }

            // hledání uživatele
            if (!string.IsNullOrEmpty(filtr.Uzivatel))
            {
                if (filtr.Uzivatel != null)
                {
                    command.CommandText += " AND p.UserID=@name";
                }
                else
                {
                    command.CommandText += " AND p.UserID IN (" +
                    " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    " where firstname like '%' + @name + '%'" +
                    " union" +
                    " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    " where surname like '%' + @name + '%'" +
                    " union" +
                    " select distinct USERID from " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS +
                    " where USERID = @name" +
                    " )";
                }

                if (filtr.rowUzivatel != null && filtr.rowUzivatel.Count > 0)
                    command.Parameters.AddWithValue("@name", filtr.rowUzivatel[0].USERID.Trim());
                else
                    command.Parameters.AddWithValue("@name", filtr.Uzivatel);
            }

            // hledání skupiny
            if (!string.IsNullOrEmpty(filtr.Skupina))
            {
                if (filtr.Skupina != null)
                {
                    command.CommandText += " AND g.name=@groupname";
                }
                else
                {
                    command.CommandText += " AND isnull(g.name, '') like '%' + @groupname + '%'";
                }

                if (filtr.rowGroups != null && filtr.rowGroups.Count > 0)
                    command.Parameters.AddWithValue("@groupname", filtr.rowGroups[0].name.Trim());
                else
                    command.Parameters.AddWithValue("@groupname", filtr.Skupina);
            }

            // hledání podle stroje
            if (!string.IsNullOrEmpty(filtr.Stroj))
            {
                if (filtr.Stroj != null)
                {
                    command.CommandText += " AND m.name=@machinename";
                }
                else
                {
                    command.CommandText += " AND isnull(m.name, '') like '%' + @machinename + '%'";
                }

                if (filtr.rowMachine != null && filtr.rowMachine.Count > 0)
                    command.Parameters.AddWithValue("@machinename", filtr.rowMachine[0].name.Trim());
                else
                    command.Parameters.AddWithValue("@machinename", filtr.Stroj);
            }

            // hledání podle operace
            if (!string.IsNullOrEmpty(filtr.Operace))
            {
                if (filtr.Operace != null)
                {
                    command.CommandText += " AND o.name=@operationname";
                }
                else
                {
                    command.CommandText += " AND isnull(o.name, '') like '%' + @operationname + '%'";
                }

                if (filtr.rowOperation != null && filtr.rowOperation.Count > 0)
                    command.Parameters.AddWithValue("@operationname", filtr.rowOperation[0].name.Trim());
                else
                    command.Parameters.AddWithValue("@operationname", filtr.Operace);

            }

            // hledání podle datumu
            if (filtr.DatumOdValue.HasValue && filtr.DatumDoValue.HasValue)
            {
                command.CommandText += " AND p.dateeve between @datumOd and @datumDo";
                command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
                command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
            }
            else
            {
                if (filtr.DatumOdValue.HasValue)
                {
                    command.CommandText += " AND p.dateeve > @datumOd";
                    command.Parameters.AddWithValue("@datumOd", filtr.DatumOdValue);
                }
                else if (filtr.DatumDoValue.HasValue)
                {
                    command.CommandText += " AND p.dateeve < @datumDo";
                    command.Parameters.AddWithValue("@datumDo", filtr.DatumDoValue);
                }
            }

            if (!string.IsNullOrEmpty(filtr.DATEEVE_TimeVariant))
            {
                if (!filtr.DATEEVE_TimeVariant.Contains("unknow"))
                {
                    var arr = filtr.DATEEVE_TimeVariant.Split(';');
                    Fask.Interfaces.Classes.TimeFilters.TimeVariants TimeVar = (Fask.Interfaces.Classes.TimeFilters.TimeVariants)Enum.Parse(typeof(Fask.Interfaces.Classes.TimeFilters.TimeVariants), arr[0], true);
                    command.CommandText += " AND p.dateeve > @dateeve_TV";
                    command.Parameters.AddWithValue("@dateeve_TV", Fask.Interfaces.Classes.TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                }
            }

            // zobrazit vsechny zakazky
            if (filtr.OdvadeniVse)
            {
                command.CommandText += " AND p.SOUBEHGUID is not null";
            }

            // zobrazit vsechny korekce
            if (filtr.KorekceVse)
            {
                command.CommandText += " AND p.CORRGUID is not null";
            }

            // zobrazit nedokonceny odvod vyroby
            if (filtr.OdvadeniNedokoncene)
            {
                command.CommandText += " AND p.SOUBEHGUID not IN (" +
                " select distinct SOUBEHGUID from " + Fask.SQL.Constants.Common.TABLE_Production +
                " where" +
                " SOUBEHGUID is not null and TIMESTOP is not null" +
                " )";
            }
            // zobrazit nedokoncene korekce
            if (filtr.KorekceNedokoncene)
            {
                //command.CommandText += " AND p.CORRGUID not IN (" +
                //" select distinct CORRGUID from Production" +
                //" where" +
                //" CORRGUID is not null and TIMECORSTOP is not null" +
                //" )";
                command.CommandText += " AND not exists ( " +
                " select SOUBEHGUID " +
                " from " + Fask.SQL.Constants.Common.TABLE_Production +
                " where " +
                " (TIMESTOP is not null or TIMEPREPSTOP is not null) " +
                " and " +
                " SOUBEHGUID=p.SOUBEHGUID " +
                " ) " +
                " and p.SOUBEHGUID is not null "
                ;
            }


            if(filtr.PouzeNeschvalene)
            {
                command.CommandText += " AND p.idVS is null";
            }


            if (!string.IsNullOrEmpty(filtr.TerminalID))
            {
                command.CommandText += " AND p.TermID in (" + filtr.TerminalID + ")";
            }

            command.CommandText += " order by p.dateeve desc";

            vyrobaDataSet1.Production_Konzola.Clear();
            vyrobaDataSet1.Production_Konzola.AcceptChanges();
            vyrobaDataSet1.Production_Konzola.BeginLoadData();
            adapter.SelectCommand = command;
            adapter.Fill(vyrobaDataSet1.Production_Konzola);
            vyrobaDataSet1.Production_Konzola.EndLoadData();

            return vyrobaDataSet1;
        }

        #region Pohoda provider navic

        public static Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable GetDataByImportProduction(string CS, int CountEntries, string SKL_ID)
        {

            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    //comm.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Production;
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                       " WHERE(CountEntries = @CountEntries) " +
                       " AND ( ISOK IS NULL) " +
                       " AND ( ISNULL(SKL_ID, @SKLID) = @SKLID) " +
                       " AND (TIMECRID IS NULL) " +
                       " ORDER BY id";

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries });

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@SKLID", DbType = DbType.String, SourceColumn = "SKLID", Value = string.IsNullOrEmpty(SKL_ID) ? string.Empty : SKL_ID.Trim() });


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

        public static Fask.Interfaces.DataSets.Vyroba Production_GetDataSelectListImport(string CS)
        {
            Fask.Interfaces.DataSets.Vyroba ds = new Interfaces.DataSets.Vyroba();
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = @"SELECT DISTINCT p.CountEntries, p.SOPNUMBE, p.SKL_ID, vph.SOPDESC FROM " + Fask.SQL.Constants.Common.TABLE_Production + " AS p " +
                        " LEFT OUTER JOIN " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " AS vph ON vph.CountEntries = p.CountEntries AND vph.SOPNUMBE = p.SOPNUMBE " +
                        " WHERE (p.ISOK IS NULL) " +
                        " AND (p.CountEntries IS NOT NULL) " +
                        " AND (p.TIMECRID IS NULL) " +
                        " AND (p.TIMESTOP IS NOT NULL)";

                    comm.CommandType = CommandType.Text;
                    using (var ada = new SqlDataAdapter())
                    {
                        ada.SelectCommand = comm;
                        returnValue = ada.Fill(ds, ds.ProductionImport.TableName);
                    }
                }
            }
            return ds;
        }

        public static void Update_ISOK_to_null_Production(string CS, int CountEntries, string ITEMNMBR, Guid SOUBEHGUID)
        {

            try
            {
              
                using (var con = new System.Data.SqlClient.SqlConnection(CS))
                {
                    using (var com = con.CreateCommand())
                    {

                        com.CommandText = " UPDATE Production " +
                        " SET ISOK = null " +
                        " WHERE " +
                        " CountEntries = @CountEntries " +
                        " AND " +
                        " ITEMNMBR = @ITEMNMBR " +
                        " AND " +
                        " SOUBEHGUID = @SOUBEHGUID " +
                        " AND " +
                        " ISOK is not null " +
                        " AND " +
                        " TIMECRID is null ";


                        com.Parameters.AddWithValue("@CountEntries", CountEntries);
                        com.Parameters.AddWithValue("@ITEMNMBR", ITEMNMBR);
                        com.Parameters.AddWithValue("@SOUBEHGUID", SOUBEHGUID);

                        con.Open();

                        com.ExecuteNonQuery();

                        con.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static Fask.Interfaces.DataSets.Vyroba Production_GetFiltrovanyProductionVazby(string CS, Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new System.Data.SqlClient.SqlDataAdapter();
            connection = new System.Data.SqlClient.SqlConnection(CS);
            command = new System.Data.SqlClient.SqlCommand();
            command.Connection = connection;


            command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_Production + " as P ";
            command.CommandText += " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as sklad ON sklad.skl_id = P.SKL_ID ";

            command.CommandText += " WHERE ";
            command.CommandText += "1=1 ";

            command.CommandText += " AND ( P.TIMESTOP is not null or P.TIMEPREPSTOP is not null) ";

            if (!string.IsNullOrEmpty(filtr.MaterialITEMNMBR))
            {
                command.CommandText += " AND P.ITEMNMBR=@ITEMNMBR";
                command.Parameters.AddWithValue("@ITEMNMBR", filtr.MaterialITEMNMBR.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.MaterialITEMDESC))
            {
                command.CommandText += " AND P.ITEMDESC like @ITEMDESC + '%'";
                command.Parameters.AddWithValue("@ITEMDESC", filtr.MaterialITEMDESC.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.MaterialMJ))
            {
                command.CommandText += " AND P.ITEMMJ=@ITEMMJ";
                command.Parameters.AddWithValue("@ITEMMJ", filtr.MaterialMJ.Trim());
            }

            //21.11.2025 MaR pridan filtr------------------------------------------
            if (!string.IsNullOrEmpty(filtr.CountEntries))
            {
                command.CommandText += " AND P.CountEntries=@CountEntries";
                command.Parameters.AddWithValue("@CountEntries", filtr.CountEntries.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.SOPNUMBE))
            {
                command.CommandText += " AND P.SOPNUMBE=@SOPNUMBE";
                command.Parameters.AddWithValue("@SOPNUMBE", filtr.SOPNUMBE.Trim());
            }

            if (!string.IsNullOrEmpty(filtr.SKL_ID))
            {
                command.CommandText += " AND P.SKL_ID=@SKL_ID";
                command.Parameters.AddWithValue("@SKL_ID", filtr.SKL_ID.Trim());
            }

            if (filtr.PotvrzeniPriznak)
            {
                command.CommandText += " AND P.idVS IS NULL";
            }
            //-------------------------------------------------------------------

            command.CommandText += " order by P.dateeve desc";

            vyrobaDataSet1.Production_Odvod.Clear();
            vyrobaDataSet1.Production_Odvod.AcceptChanges();
            vyrobaDataSet1.Production_Odvod.BeginLoadData();
            adapter.SelectCommand = command;
            adapter.Fill(vyrobaDataSet1.Production_Odvod);
            vyrobaDataSet1.Production_Odvod.EndLoadData();

            return vyrobaDataSet1;
        }

        public static int Production_FillByCountEntries(string CS, Fask.Interfaces.DataSets.Vyroba ds, int CountEntries)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand comm = null;
            int returnValue = 0;

            using (conn = new SqlConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                        " WHERE (CountEntries = @CountEntries)";

                    comm.Parameters.Add(new SqlParameter()
                    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries });

                    
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


        #endregion

    }
}
