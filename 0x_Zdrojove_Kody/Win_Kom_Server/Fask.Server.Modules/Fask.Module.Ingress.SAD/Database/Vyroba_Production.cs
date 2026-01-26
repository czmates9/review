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
    public class Vyroba_Production
    {
        #region Update
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


                        //InitializeCommandInsert_Production(commandInsert);
                        InitializeCommandUpdate_Production(commandUpdate);
                        //InitializeCommandDelete_Production(commandDelete);
                        InitializeCommandSelect_Production(commandSelect);

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

        //private static void InitializeCommandInsert_Production(IngresCommand command)
        //{

        //    //   ([CountEntries],[SOPNUMBE],[ITEMNMBR],[ITEMTYPE],[ITEMMJ],[ITEMDESC],[ORD],[TIMEMODE],[TIMEPREPSTART],[TIMEPREPSTOP]
        //    //,[TIMEPREP],[TIMEUNIT],[TIMESTART],[TIMESTOP],[TIMECORSTART],[TIMECORSTOP],[TIMECOR],[TIMECRID],[TIMECRIDTYPE]
        //    //,[loginid],[machineid],[operationid],[dateeve],[qty],[qtyReal],[QTYPACK],[QTYPACKMJ],[description],[BarcodeP]
        //    //,[UserID],[TermID],[ISOK],[GUID],[SOUBEHGUID],[CORRGUID],[qtyOld],[idVS],[dateedit],[SKL_ID],[LOCNCODE]
        //    //,[SERLTNUM],[EXPIRATION],[NMBRPAL],[TYPEPAL],[PackType],[status],[WEIGHT],[STORNOGUID])

        //    command.CommandText =
        //        @" INSERT INTO " + Fask.SQL.Constants.Common.TABLE_Production +
        //    " (CountEntries, SOPNUMBE, SOPTYPE, SOPDESC, VNDDOCNMH, BarcodeH, LOCNCODE, DateProd, Rez1, Rez2, TermID, LSTMod, Active)" +
        //    " VALUES (@CountEntries,@SOPNUMBE,@SOPTYPE,@SOPDESC,@VNDDOCNMH,@BarcodeH,@LOCNCODE,@DateProd,@Rez1,@Rez2,@TermID,@LSTMod,@Active)";

        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@SOPTYPE", DbType = DbType.String, SourceColumn = "SOPTYPE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@SOPDESC", DbType = DbType.String, SourceColumn = "SOPDESC", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@VNDDOCNMH", DbType = DbType.String, SourceColumn = "VNDDOCNMH", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@BarcodeH", DbType = DbType.String, SourceColumn = "BarcodeH", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@DateProd", DbType = DbType.Int16, SourceColumn = "DateProd", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@Rez1", DbType = DbType.String, SourceColumn = "Rez1", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@Rez2", DbType = DbType.String, SourceColumn = "Rez2", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@LSTMod", DbType = DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current });
        //    command.Parameters.Add(new IngresParameter()
        //    { ParameterName = "@Active", DbType = DbType.Byte, SourceColumn = "Active", SourceVersion = DataRowVersion.Current });


        //}

        private static void InitializeCommandUpdate_Production(IngresCommand command)
        {

            #region new



            command.CommandText = "UPDATE " + Fask.SQL.Constants.Common.TABLE_Production +
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

            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMTYPE", DbType = DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMMJ", DbType = DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMDESC", DbType = DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ORD", DbType = DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEMODE", DbType = DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEPREPSTART", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEPREPSTOP", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEPREP", DbType = DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEUNIT", DbType = DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMESTART", DbType = DbType.DateTime, SourceColumn = "TIMESTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMESTOP", DbType = DbType.DateTime, SourceColumn = "TIMESTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECORSTART", DbType = DbType.DateTime, SourceColumn = "TIMECORSTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECORSTOP", DbType = DbType.DateTime, SourceColumn = "TIMECORSTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECOR", DbType = DbType.Single, SourceColumn = "TIMECOR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECRID", DbType = DbType.Int32, SourceColumn = "TIMECRID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECRIDTYPE", DbType = DbType.Byte, SourceColumn = "TIMECRIDTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@loginid", DbType = DbType.String, SourceColumn = "loginid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@operationid", DbType = DbType.String, SourceColumn = "operationid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@dateeve", DbType = DbType.DateTime, SourceColumn = "dateeve", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@qty", DbType = DbType.Decimal, SourceColumn = "qty", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@qtyReal", DbType = DbType.Decimal, SourceColumn = "qtyReal", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@QTYPACKMJ", DbType = DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@BarcodeP", DbType = DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@UserID", DbType = DbType.String, SourceColumn = "UserID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ISOK", DbType = DbType.DateTime, SourceColumn = "ISOK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@GUID", DbType = DbType.Guid, SourceColumn = "GUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SOUBEHGUID", DbType = DbType.Guid, SourceColumn = "SOUBEHGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@CORRGUID", DbType = DbType.Guid, SourceColumn = "CORRGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@qtyOld", DbType = DbType.Decimal, SourceColumn = "qtyOld", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@idVS", DbType = DbType.String, SourceColumn = "idVS", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@dateedit", DbType = DbType.DateTime, SourceColumn = "dateedit", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SKL_ID", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SERLTNUM", DbType = DbType.String, SourceColumn = "SERLTNUM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@EXPIRATION", DbType = DbType.String, SourceColumn = "EXPIRATION", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@NMBRPAL", DbType = DbType.String, SourceColumn = "NMBRPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TYPEPAL", DbType = DbType.String, SourceColumn = "TYPEPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@PackType", DbType = DbType.String, SourceColumn = "PackType", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@status", DbType = DbType.Int32, SourceColumn = "status", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@WEIGHT", DbType = DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@STORNOGUID", DbType = DbType.Guid, SourceColumn = "STORNOGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_1", DbType = DbType.String, SourceColumn = "REZ_1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_2", DbType = DbType.String, SourceColumn = "REZ_2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_3", DbType = DbType.String, SourceColumn = "REZ_3", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_4", DbType = DbType.String, SourceColumn = "REZ_4", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_5", DbType = DbType.String, SourceColumn = "REZ_5", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@WEIGHT_OLD", DbType = DbType.Decimal, SourceColumn = "WEIGHT_OLD", SourceVersion = DataRowVersion.Current });



            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@id", DbType = DbType.Int32, SourceColumn = "id", SourceVersion = DataRowVersion.Original });


            #endregion

            #region old

            //     command.CommandText = "UPDATE " + Fask.SQL.Constants.Common.TABLE_Production +
            //         " SET [CountEntries] = @CountEntries," +
            //         " [SOPNUMBE] = @SOPNUMBE," +
            //         " [ITEMNMBR] = @ITEMNMBR," +
            //         " [ITEMTYPE] = @ITEMTYPE," +
            //         " [ITEMMJ] = @ITEMMJ," +
            //         " [ORD] = @ORD," +
            //" [TIMEMODE] = @TIMEMODE," +
            //" [TIMEPREPSTART] = @TIMEPREPSTART," +
            //" [TIMEPREPSTOP] = @TIMEPREPSTOP," +
            //" [TIMEPREP] = @TIMEPREP," +
            //" [TIMEUNIT] = @TIMEUNIT," +
            //" [TIMESTART] = @TIMESTART," +
            //" [TIMESTOP] = @TIMESTOP," +
            //" [TIMECORSTART] = @TIMECORSTART," +
            //" [TIMECORSTOP] = @TIMECORSTOP," +
            //" [TIMECOR] = @TIMECOR," +
            //" [TIMECRID] = @TIMECRID," +
            //" [loginid] = @loginid," +
            //" [machineid] = @machineid," +
            //" [operationid] = @operationid," +
            //" [dateeve] = @dateeve," +
            //" [qty] = @qty," +
            //" [qtyReal] = @qtyReal," +
            //" [QTYPACK] = @QTYPACK," +
            //" [QTYPACKMJ] = @QTYPACKMJ," +
            //" [description] = @description," +
            //" [BarcodeP] = @BarcodeP," +
            //" [UserID] = @UserID," +
            //" [TermID] = @TermID," +
            //" [ISOK] = @ISOK," +
            //" [GUID] = @GUID," +
            //" [SOUBEHGUID] = @SOUBEHGUID," +
            //" [qtyOld] = @qtyOld," +
            //" [idVS] = @idVS," +
            //" [dateedit] = @dateedit," +
            //" [CORRGUID] = @CORRGUID," +
            //" [TIMECRIDTYPE] = @TIMECRIDTYPE," +
            //" [SKL_ID] = @SKL_ID," +
            //" [LOCNCODE] = @LOCNCODE," +
            //" [ITEMDESC] = @ITEMDESC" +
            //" WHERE (((@IsNull_CountEntries = 1 AND [CountEntries] IS NULL) " +
            //"OR ([CountEntries] = @Original_CountEntries)) AND ((@IsNull_SOPNUMBE = 1 AND [SO" +
            //"PNUMBE] IS NULL) OR ([SOPNUMBE] = @Original_SOPNUMBE)) AND ((@IsNull_ITEMNMBR = " +
            //"1 AND [ITEMNMBR] IS NULL) OR ([ITEMNMBR] = @Original_ITEMNMBR)) AND ((@IsNull_IT" +
            //"EMTYPE = 1 AND [ITEMTYPE] IS NULL) OR ([ITEMTYPE] = @Original_ITEMTYPE)) AND ((@" +
            //"IsNull_ITEMMJ = 1 AND [ITEMMJ] IS NULL) OR ([ITEMMJ] = @Original_ITEMMJ)) AND ((" +
            //"@IsNull_ORD = 1 AND [ORD] IS NULL) OR ([ORD] = @Original_ORD)) AND ((@IsNull_TIM" +
            //"EMODE = 1 AND [TIMEMODE] IS NULL) OR ([TIMEMODE] = @Original_TIMEMODE)) AND ((@I" +
            //"sNull_TIMEPREPSTART = 1 AND [TIMEPREPSTART] IS NULL) OR ([TIMEPREPSTART] = @Orig" +
            //"inal_TIMEPREPSTART)) AND ((@IsNull_TIMEPREPSTOP = 1 AND [TIMEPREPSTOP] IS NULL) " +
            //"OR ([TIMEPREPSTOP] = @Original_TIMEPREPSTOP)) AND ((@IsNull_TIMEPREP = 1 AND [TI" +
            //"MEPREP] IS NULL) OR ([TIMEPREP] = @Original_TIMEPREP)) AND ((@IsNull_TIMEUNIT = " +
            //"1 AND [TIMEUNIT] IS NULL) OR ([TIMEUNIT] = @Original_TIMEUNIT)) AND ((@IsNull_TI" +
            //"MESTART = 1 AND [TIMESTART] IS NULL) OR ([TIMESTART] = @Original_TIMESTART)) AND" +
            //" ((@IsNull_TIMESTOP = 1 AND [TIMESTOP] IS NULL) OR ([TIMESTOP] = @Original_TIMES" +
            //"TOP)) AND ((@IsNull_TIMECORSTART = 1 AND [TIMECORSTART] IS NULL) OR ([TIMECORSTA" +
            //"RT] = @Original_TIMECORSTART)) AND ((@IsNull_TIMECORSTOP = 1 AND [TIMECORSTOP] I" +
            //"S NULL) OR ([TIMECORSTOP] = @Original_TIMECORSTOP)) AND ((@IsNull_TIMECOR = 1 AN" +
            //"D [TIMECOR] IS NULL) OR ([TIMECOR] = @Original_TIMECOR)) AND ((@IsNull_TIMECRID " +
            //"= 1 AND [TIMECRID] IS NULL) OR ([TIMECRID] = @Original_TIMECRID)) AND ([id] = @O" +
            //"riginal_id) AND ([loginid] = @Original_loginid) AND ((@IsNull_machineid = 1 AND " +
            //"[machineid] IS NULL) OR ([machineid] = @Original_machineid)) AND ((@IsNull_opera" +
            //"tionid = 1 AND [operationid] IS NULL) OR ([operationid] = @Original_operationid)" +
            //") AND ([dateeve] = @Original_dateeve) AND ([qty] = @Original_qty) AND ([qtyReal]" +
            //" = @Original_qtyReal) AND ((@IsNull_QTYPACK = 1 AND [QTYPACK] IS NULL) OR ([QTYP" +
            //"ACK] = @Original_QTYPACK)) AND ((@IsNull_QTYPACKMJ = 1 AND [QTYPACKMJ] IS NULL) " +
            //"OR ([QTYPACKMJ] = @Original_QTYPACKMJ)) AND ((@IsNull_BarcodeP = 1 AND [BarcodeP" +
            //"] IS NULL) OR ([BarcodeP] = @Original_BarcodeP)) AND ([UserID] = @Original_UserI" +
            //"D) AND ([TermID] = @Original_TermID) AND ((@IsNull_ISOK = 1 AND [ISOK] IS NULL) " +
            //"OR ([ISOK] = @Original_ISOK)) AND ([GUID] = @Original_GUID) AND ((@IsNull_SOUBEH" +
            //"GUID = 1 AND [SOUBEHGUID] IS NULL) OR ([SOUBEHGUID] = @Original_SOUBEHGUID)) AND" +
            //" ((@IsNull_qtyOld = 1 AND [qtyOld] IS NULL) OR ([qtyOld] = @Original_qtyOld)) AN" +
            //"D ((@IsNull_idVS = 1 AND [idVS] IS NULL) OR ([idVS] = @Original_idVS)) AND ((@Is" +
            //"Null_dateedit = 1 AND [dateedit] IS NULL) OR ([dateedit] = @Original_dateedit)) " +
            //"AND ((@IsNull_CORRGUID = 1 AND [CORRGUID] IS NULL) OR ([CORRGUID] = @Original_CO" +
            //"RRGUID)) AND ((@IsNull_TIMECRIDTYPE = 1 AND [TIMECRIDTYPE] IS NULL) OR ([TIMECRI" +
            //"DTYPE] = @Original_TIMECRIDTYPE)) AND ((@IsNull_SKL_ID = 1 AND [SKL_ID] IS NULL)" +
            //" OR ([SKL_ID] = @Original_SKL_ID)) AND ((@IsNull_LOCNCODE = 1 AND [LOCNCODE] IS " +
            //"NULL) OR ([LOCNCODE] = @Original_LOCNCODE)) AND ((@IsNull_ITEMDESC = 1 AND [ITEM" +
            //"DESC] IS NULL) OR ([ITEMDESC] = @Original_ITEMDESC)));\r\nSELECT CountEntries, SOP" +
            //"NUMBE, ITEMNMBR, ITEMTYPE, ITEMMJ, ORD, TIMEMODE, TIMEPREPSTART, TIMEPREPSTOP, T" +
            //"IMEPREP, TIMEUNIT, TIMESTART, TIMESTOP, TIMECORSTART, TIMECORSTOP, TIMECOR, TIME" +
            //"CRID, id, loginid, machineid, operationid, dateeve, qty, qtyReal, QTYPACK, QTYPA" +
            //"CKMJ, description, BarcodeP, UserID, TermID, ISOK, GUID, SOUBEHGUID, qtyOld, idV" +
            //"S, dateedit, CORRGUID, TIMECRIDTYPE, SKL_ID, LOCNCODE, ITEMDESC FROM Production " +
            //"WHERE (id = @id)";

            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@ITEMTYPE", DbType = DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@ITEMMJ", DbType = DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@ORD", DbType = DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMEMODE", DbType = DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMEPREPSTART", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTART", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMEPREPSTOP", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTOP", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMEPREP", DbType = DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMEUNIT", DbType = DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMESTART", DbType = DbType.DateTime, SourceColumn = "TIMESTART", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMESTOP", DbType = DbType.DateTime, SourceColumn = "TIMESTOP", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMECORSTART", DbType = DbType.DateTime, SourceColumn = "TIMECORSTART", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMECORSTOP", DbType = DbType.DateTime, SourceColumn = "TIMECORSTOP", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMECOR", DbType = DbType.Single, SourceColumn = "TIMECOR", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMECRID", DbType = DbType.Int32, SourceColumn = "TIMECRID", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@loginid", DbType = DbType.String, SourceColumn = "loginid", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@operationid", DbType = DbType.String, SourceColumn = "operationid", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@dateeve", DbType = DbType.DateTime, SourceColumn = "dateeve", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@qty", DbType = DbType.Decimal, SourceColumn = "qty", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@qtyReal", DbType = DbType.Decimal, SourceColumn = "qtyReal", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@QTYPACKMJ", DbType = DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@BarcodeP", DbType = DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@UserID", DbType = DbType.String, SourceColumn = "UserID", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@ISOK", DbType = DbType.DateTime, SourceColumn = "ISOK", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@GUID", DbType = DbType.Guid, SourceColumn = "GUID", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@SOUBEHGUID", DbType = DbType.Guid, SourceColumn = "SOUBEHGUID", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@qtyOld", DbType = DbType.Decimal, SourceColumn = "qtyOld", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@idVS", DbType = DbType.String, SourceColumn = "idVS", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@dateedit", DbType = DbType.DateTime, SourceColumn = "dateedit", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@CORRGUID", DbType = DbType.Guid, SourceColumn = "CORRGUID", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@TIMECRIDTYPE", DbType = DbType.Byte, SourceColumn = "TIMECRIDTYPE", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@SKL_ID", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter()
            //     { ParameterName = "@ITEMDESC", DbType = DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_SOPNUMBE", DbType = DbType.Int32, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_ITEMNMBR", DbType = DbType.Int32, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_ITEMTYPE", DbType = DbType.Int32, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_ITEMTYPE", DbType = DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_ITEMMJ", DbType = DbType.Int32, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_ITEMMJ", DbType = DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_ORD", DbType = DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_ORD", DbType = DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMEMODE", DbType = DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMEMODE", DbType = DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMEPREPSTART", DbType = DbType.Int32, SourceColumn = "TIMEPREPSTART", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMEPREPSTART", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTART", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMEPREPSTOP", DbType = DbType.Int32, SourceColumn = "TIMEPREPSTOP", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMEPREPSTOP", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTOP", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMEPREP", DbType = DbType.Int32, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMEPREP", DbType = DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMEUNIT", DbType = DbType.Int32, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMEUNIT", DbType = DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMESTART", DbType = DbType.Int32, SourceColumn = "TIMESTART", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMESTART", DbType = DbType.DateTime, SourceColumn = "TIMESTART", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMESTOP", DbType = DbType.Int32, SourceColumn = "TIMESTOP", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMESTOP", DbType = DbType.DateTime, SourceColumn = "TIMESTOP", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMECORSTART", DbType = DbType.Int32, SourceColumn = "TIMECORSTART", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMECORSTART", DbType = DbType.DateTime, SourceColumn = "TIMECORSTART", SourceVersion = DataRowVersion.Original }); command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMECORSTOP", DbType = DbType.Int32, SourceColumn = "TIMECORSTOP", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMECORSTOP", DbType = DbType.DateTime, SourceColumn = "TIMECORSTOP", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMECOR", DbType = DbType.Int32, SourceColumn = "TIMECOR", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMECOR", DbType = DbType.Single, SourceColumn = "TIMECOR", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMECRID", DbType = DbType.Int32, SourceColumn = "TIMECRID", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMECRID", DbType = DbType.Int32, SourceColumn = "TIMECRID", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_id", DbType = DbType.Int32, SourceColumn = "id", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_loginid", DbType = DbType.String, SourceColumn = "loginid", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_machineid", DbType = DbType.Int32, SourceColumn = "machineid", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_machineid", DbType = DbType.String, SourceColumn = "machineid", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_operationid", DbType = DbType.Int32, SourceColumn = "operationid", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_operationid", DbType = DbType.String, SourceColumn = "operationid", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_dateeve", DbType = DbType.DateTime, SourceColumn = "dateeve", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_qty", DbType = DbType.Decimal, SourceColumn = "qty", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_qtyReal", DbType = DbType.Decimal, SourceColumn = "qtyReal", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_QTYPACK", DbType = DbType.Int32, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_QTYPACKMJ", DbType = DbType.Int32, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_QTYPACKMJ", DbType = DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_BarcodeP", DbType = DbType.Int32, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_BarcodeP", DbType = DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_UserID", DbType = DbType.String, SourceColumn = "UserID", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_ISOK", DbType = DbType.Int32, SourceColumn = "ISOK", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_ISOK", DbType = DbType.DateTime, SourceColumn = "ISOK", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_GUID", DbType = DbType.Guid, SourceColumn = "GUID", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_SOUBEHGUID", DbType = DbType.Int32, SourceColumn = "SOUBEHGUID", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_SOUBEHGUID", DbType = DbType.Guid, SourceColumn = "SOUBEHGUID", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_qtyOld", DbType = DbType.Int32, SourceColumn = "qtyOld", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_qtyOld", DbType = DbType.Decimal, SourceColumn = "qtyOld", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_idVS", DbType = DbType.Int32, SourceColumn = "idVS", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_idVS", DbType = DbType.String, SourceColumn = "idVS", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_dateedit", DbType = DbType.Int32, SourceColumn = "dateedit", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_dateedit", DbType = DbType.DateTime, SourceColumn = "dateedit", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_CORRGUID", DbType = DbType.Int32, SourceColumn = "CORRGUID", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_CORRGUID", DbType = DbType.Guid, SourceColumn = "CORRGUID", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_TIMECRIDTYPE", DbType = DbType.Int32, SourceColumn = "TIMECRIDTYPE", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_TIMECRIDTYPE", DbType = DbType.Byte, SourceColumn = "TIMECRIDTYPE", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_SKL_ID", DbType = DbType.Int32, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_SKL_ID", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_LOCNCODE", DbType = DbType.Int32, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@IsNull_ITEMDESC", DbType = DbType.Int32, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@Original_ITEMDESC", DbType = DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Original });
            //     command.Parameters.Add(new IngresParameter() { ParameterName = "@id", DbType = DbType.Int32, SourceColumn = "id", SourceVersion = DataRowVersion.Current });


            #endregion
        }

        //private static void InitializeCommandDelete_Production(IngresCommand command)
        //{
        //    command.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_Production + " WHERE (CountEntries = @Original_CountEntries) AND (SOPNUMBE = @Original_SOPNUMBE)";


        //    command.Parameters.Add(new IngresParameter()
        //    {
        //        ParameterName = "@Original_CountEntries",
        //        DbType = DbType.Int32,
        //        SourceColumn = "CountEntries",
        //        SourceVersion = DataRowVersion.Original

        //    });

        //    command.Parameters.Add(new IngresParameter()
        //    {
        //        ParameterName = "@Original_SOPNUMBE",
        //        DbType = DbType.String,
        //        SourceColumn = "SOPNUMBE",
        //        SourceVersion = DataRowVersion.Original

        //    });
        //}

        private static void InitializeCommandSelect_Production(IngresCommand command)
        {
            command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Production;
        }


        #endregion

        #endregion

        #region Update Transakce

        public static int Update_Trans(IngresConnection conn, IngresTransaction transaction, object data)
        {
     
            int result = 0;
            try
            {

                //using (conn = new IngresConnection(CS))
                //{

                //    conn.Open();

                    //transaction = conn.BeginTransaction();

                    using (var commandInsert = conn.CreateCommand())
                    using (var commandUpdate = conn.CreateCommand())
                    using (var commandDelete = conn.CreateCommand())
                    using (var commandSelect = conn.CreateCommand())
                    {

                        commandInsert.Transaction = transaction;
                        commandUpdate.Transaction = transaction;
                        commandDelete.Transaction = transaction;
                        commandSelect.Transaction = transaction;


                        InitializeCommandInsert_Trans_Production(commandInsert);
                        InitializeCommandUpdate_Trans_Production(commandUpdate);
                        InitializeCommandDelete_Trans_Production(commandDelete);
                        InitializeCommandSelect_Trans_Production(commandSelect);

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

                //    transaction.Commit();
                //}

                return result;
            }
            catch (Exception ex)
            {
                //Logging.Log.Write(ex);
                //Logging.ExceptionHandler2.Handle(ex);

                //try
                //{
                //    if (transaction != null)
                //        transaction.Rollback();
                //}
                //catch (Exception exTransaction)
                //{
                //    //Logging.Log.Write(exTransaction);
                //    //Logging.ExceptionHandler2.Handle(exTransaction);
                //    throw exTransaction;
                //}

                throw ex;
            }
            //finally
            //{
            //    if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
            //    {
            //        conn.Close();
            //        conn.Dispose();
            //    }
            //}

        }

        #region Inicialize metody

        private static void InitializeCommandInsert_Trans_Production(IngresCommand command)
        {

   
            command.CommandText = "INSERT INTO [Production] (" +
                " [CountEntries], [SOPNUMBE], [ITEMNMBR], [ITEMTYPE], [ITEMMJ], " +
                " [ORD], [TIMEMODE], [TIMEPREPSTART], [TIMEPREPSTOP], [TIMEPREP], " +
                " [TIMEUNIT], [TIMESTART], [TIMESTOP], [TIMECORSTART], [TIMECORSTOP], " +
                " [TIMECOR], [TIMECRID], [loginid], [machineid], [dateeve], " +
                " [qty], [qtyReal], [QTYPACK], [QTYPACKMJ], [description], " +
                " [BarcodeP], [UserID], [TermID], [ISOK], [GUID], " +
                " [operationid], [SOUBEHGUID], [CORRGUID], [TIMECRIDTYPE], [SKL_ID], " +
                " [LOCNCODE], [ITEMDESC], [SERLTNUM], [STORNOGUID], [WEIGHT], " +
                " [status], [PackType], [TYPEPAL], [NMBRPAL], [WEIGHT_OLD], " +
                " [REZ_5], [REZ_4], [REZ_3], [REZ_2], [REZ_1]" +
                ") VALUES (" +
                " @CountEntries, @SOPNUMBE, @ITEMNMBR, @ITEMTYPE, @ITEMMJ, " +
                " @ORD, @TIMEMODE, @TIMEPREPSTART, @TIMEPREPSTOP, @TIMEPREP, " +
                " @TIMEUNIT, @TIMESTART, @TIMESTOP, @TIMECORSTART, @TIMECORSTOP, " +
                " @TIMECOR, @TIMECRID, @loginid, @machineid, @dateeve, " +
                " @qty, @qtyReal, @QTYPACK, @QTYPACKMJ, @description, " +
                " @BarcodeP, @UserID, @TermID, @ISOK, @GUID, " +
                " @operationid, @SOUBEHGUID, @CORRGUID, @TIMECRIDTYPE, @SKL_ID, " +
                " @LOCNCODE, @ITEMDESC, @SERLTNUM, @STORNOGUID, @WEIGHT, " +
                " @status, @PackType, @TYPEPAL, @NMBRPAL, @WEIGHT_OLD, " +
                " @REZ_5, @REZ_4, @REZ_3, @REZ_2, @REZ_1" +
                " )";

            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMTYPE", DbType = DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMMJ", DbType = DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMDESC", DbType = DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ORD", DbType = DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEMODE", DbType = DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEPREPSTART", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEPREPSTOP", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEPREP", DbType = DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEUNIT", DbType = DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMESTART", DbType = DbType.DateTime, SourceColumn = "TIMESTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMESTOP", DbType = DbType.DateTime, SourceColumn = "TIMESTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECORSTART", DbType = DbType.DateTime, SourceColumn = "TIMECORSTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECORSTOP", DbType = DbType.DateTime, SourceColumn = "TIMECORSTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECOR", DbType = DbType.Single, SourceColumn = "TIMECOR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECRID", DbType = DbType.Int32, SourceColumn = "TIMECRID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECRIDTYPE", DbType = DbType.Byte, SourceColumn = "TIMECRIDTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@loginid", DbType = DbType.String, SourceColumn = "loginid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@operationid", DbType = DbType.String, SourceColumn = "operationid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@dateeve", DbType = DbType.DateTime, SourceColumn = "dateeve", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@qty", DbType = DbType.Decimal, SourceColumn = "qty", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@qtyReal", DbType = DbType.Decimal, SourceColumn = "qtyReal", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@QTYPACKMJ", DbType = DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@BarcodeP", DbType = DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@UserID", DbType = DbType.String, SourceColumn = "UserID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ISOK", DbType = DbType.DateTime, SourceColumn = "ISOK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@GUID", DbType = DbType.Guid, SourceColumn = "GUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SOUBEHGUID", DbType = DbType.Guid, SourceColumn = "SOUBEHGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@CORRGUID", DbType = DbType.Guid, SourceColumn = "CORRGUID", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@qtyOld", DbType = DbType.Decimal, SourceColumn = "qtyOld", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@idVS", DbType = DbType.String, SourceColumn = "idVS", SourceVersion = DataRowVersion.Current });
           //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@dateedit", DbType = DbType.DateTime, SourceColumn = "dateedit", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SKL_ID", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SERLTNUM", DbType = DbType.String, SourceColumn = "SERLTNUM", SourceVersion = DataRowVersion.Current });
            //command.Parameters.Add(new IngresParameter()
            //{ ParameterName = "@EXPIRATION", DbType = DbType.String, SourceColumn = "EXPIRATION", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@NMBRPAL", DbType = DbType.String, SourceColumn = "NMBRPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TYPEPAL", DbType = DbType.String, SourceColumn = "TYPEPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@PackType", DbType = DbType.String, SourceColumn = "PackType", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@status", DbType = DbType.Int32, SourceColumn = "status", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@WEIGHT", DbType = DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@STORNOGUID", DbType = DbType.Guid, SourceColumn = "STORNOGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@WEIGHT_OLD", DbType = DbType.Decimal, SourceColumn = "WEIGHT_OLD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_1", DbType = DbType.String, SourceColumn = "REZ_1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_2", DbType = DbType.String, SourceColumn = "REZ_2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_3", DbType = DbType.String, SourceColumn = "REZ_3", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_4", DbType = DbType.String, SourceColumn = "REZ_4", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_5", DbType = DbType.String, SourceColumn = "REZ_5", SourceVersion = DataRowVersion.Current });
         

        }

        private static void InitializeCommandUpdate_Trans_Production(IngresCommand command)
        {
            command.CommandText = "UPDATE " + Fask.SQL.Constants.Common.TABLE_Production +
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
                                    " ,[STORNOGUID] = @WEIGHT" +
                                    " ,[REZ_1] = @WEIGHT" +
                                    " ,[REZ_2] = @WEIGHT" +
                                    " ,[REZ_3] = @WEIGHT" +
                                    " ,[REZ_4] = @WEIGHT" +
                                    " ,[REZ_5] = @WEIGHT" +
                                    " ,[WEIGHT_OLD] = @WEIGHT" +
                                    " WHERE (id = @id)";

            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMTYPE", DbType = DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMMJ", DbType = DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ITEMDESC", DbType = DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ORD", DbType = DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEMODE", DbType = DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEPREPSTART", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEPREPSTOP", DbType = DbType.DateTime, SourceColumn = "TIMEPREPSTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEPREP", DbType = DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMEUNIT", DbType = DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMESTART", DbType = DbType.DateTime, SourceColumn = "TIMESTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMESTOP", DbType = DbType.DateTime, SourceColumn = "TIMESTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECORSTART", DbType = DbType.DateTime, SourceColumn = "TIMECORSTART", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECORSTOP", DbType = DbType.DateTime, SourceColumn = "TIMECORSTOP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECOR", DbType = DbType.Single, SourceColumn = "TIMECOR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECRID", DbType = DbType.Int32, SourceColumn = "TIMECRID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TIMECRIDTYPE", DbType = DbType.Byte, SourceColumn = "TIMECRIDTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@loginid", DbType = DbType.String, SourceColumn = "loginid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@operationid", DbType = DbType.String, SourceColumn = "operationid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@dateeve", DbType = DbType.DateTime, SourceColumn = "dateeve", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@qty", DbType = DbType.Decimal, SourceColumn = "qty", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@qtyReal", DbType = DbType.Decimal, SourceColumn = "qtyReal", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@QTYPACKMJ", DbType = DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@BarcodeP", DbType = DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@UserID", DbType = DbType.String, SourceColumn = "UserID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@ISOK", DbType = DbType.DateTime, SourceColumn = "ISOK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@GUID", DbType = DbType.Guid, SourceColumn = "GUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SOUBEHGUID", DbType = DbType.Guid, SourceColumn = "SOUBEHGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@CORRGUID", DbType = DbType.Guid, SourceColumn = "CORRGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@qtyOld", DbType = DbType.Decimal, SourceColumn = "qtyOld", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@idVS", DbType = DbType.String, SourceColumn = "idVS", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@dateedit", DbType = DbType.DateTime, SourceColumn = "dateedit", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SKL_ID", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@SERLTNUM", DbType = DbType.String, SourceColumn = "SERLTNUM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@EXPIRATION", DbType = DbType.String, SourceColumn = "EXPIRATION", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@NMBRPAL", DbType = DbType.String, SourceColumn = "NMBRPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@TYPEPAL", DbType = DbType.String, SourceColumn = "TYPEPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@PackType", DbType = DbType.String, SourceColumn = "PackType", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@status", DbType = DbType.Int32, SourceColumn = "status", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@WEIGHT", DbType = DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@STORNOGUID", DbType = DbType.Guid, SourceColumn = "STORNOGUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_1", DbType = DbType.String, SourceColumn = "REZ_1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_2", DbType = DbType.String, SourceColumn = "REZ_2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_3", DbType = DbType.String, SourceColumn = "REZ_3", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_4", DbType = DbType.String, SourceColumn = "REZ_4", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@REZ_5", DbType = DbType.String, SourceColumn = "REZ_5", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@WEIGHT_OLD", DbType = DbType.Decimal, SourceColumn = "WEIGHT_OLD", SourceVersion = DataRowVersion.Current });



            command.Parameters.Add(new IngresParameter()
            { ParameterName = "@id", DbType = DbType.Int32, SourceColumn = "id", SourceVersion = DataRowVersion.Original });

        }

        private static void InitializeCommandDelete_Trans_Production(IngresCommand command)
        {
            command.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_Production + " WHERE (id = @Original_id) ";


            command.Parameters.Add(new IngresParameter()
            {
                ParameterName = "@Original_id",
                DbType = DbType.Int32,
                SourceColumn = "id",
                SourceVersion = DataRowVersion.Original

            });
        }

        private static void InitializeCommandSelect_Trans_Production(IngresCommand command)
        {
            command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Production;
        }


        #endregion


        #endregion

        public static Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataBySOUBEHGUID(string CS, Guid SOUBEHGUID)
        {
            IngresConnection conn = null;
            IngresCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                        " WHERE ( SOUBEHGUID = @SOUBEHGUID ) ";

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@SOUBEHGUID", DbType = DbType.Guid, SourceColumn = "SOUBEHGUID", Value = SOUBEHGUID == null ? (object)DBNull.Value : SOUBEHGUID });

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


        public static int Production_FillBySOUBEHGUID(string CS, Fask.Interfaces.DataSets.Vyroba ds, Guid SOUBEHGUID)
        {
            IngresConnection conn = null;
            IngresCommand comm = null;
            int returnValue = 0;

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                              " WHERE ( SOUBEHGUID = @SOUBEHGUID ) ";

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@SOUBEHGUID", DbType = DbType.Guid, SourceColumn = "SOUBEHGUID", Value = SOUBEHGUID == null ? (object)DBNull.Value : SOUBEHGUID });


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

        public static Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataByCountEntriesAndSOPNUMBEandITEMNMBR(string CS, int CountEntries, string SOPNUMBE, string ITEMNMBR)
        {
            IngresConnection conn = null;
            IngresCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                      " WHERE (CountEntries = @CountEntries)" +
                      " AND (SOPNUMBE = @SOPNUMBE)" +
                      " AND (ITEMNMBR = @ITEMNMBR)" +
                      " ORDER BY dateeve DESC";

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries });

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });

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


        public static int Production_FillByCountEntriesAndSOPNUMBEandITEMNMBR(string CS, Fask.Interfaces.DataSets.Vyroba ds, int CountEntries, string SOPNUMBE, string ITEMNMBR)
        {
            IngresConnection conn = null;
            IngresCommand comm = null;
            int returnValue = 0;

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                       " WHERE (CountEntries = @CountEntries)" +
                       " AND (SOPNUMBE = @SOPNUMBE)" +
                       " AND (ITEMNMBR = @ITEMNMBR)" +
                       " ORDER BY dateeve DESC";

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries });

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });


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


        public static Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataByCountEntriesAndSOPNUMBE(string CS, int CountEntries, string SOPNUMBE)
        {
            IngresConnection conn = null;
            IngresCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                        " WHERE (CountEntries = @CountEntries)" +
                        " AND (SOPNUMBE = @SOPNUMBE)";

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries });

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

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


        public static int Production_FillByCountEntriesAndSOPNUMBE(string CS, Fask.Interfaces.DataSets.Vyroba ds, int CountEntries, string SOPNUMBE)
        {
            IngresConnection conn = null;
            IngresCommand comm = null;
            int returnValue = 0;

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                        " WHERE (CountEntries = @CountEntries)" +
                        " AND (SOPNUMBE = @SOPNUMBE)";

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries});

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

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


        public static Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataByCORRGUID(string CS, Guid CORRGUID)
        {

            IngresConnection conn = null;
            IngresCommand comm = null;
            Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dataTable = new Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable();

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    //comm.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Production;
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                        " WHERE ( CORRGUID = @CORRGUID ) ";

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@CORRGUID", DbType = DbType.Guid, SourceColumn = "CORRGUID", Value = CORRGUID == null ? (object)DBNull.Value : CORRGUID });

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


        public static int Production_FillByCORRGUID(string CS, Fask.Interfaces.DataSets.Vyroba ds, Guid CORRGUID)
        {
            IngresConnection conn = null;
            IngresCommand comm = null;
            int returnValue = 0;

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                        " WHERE ( CORRGUID = @CORRGUID ) ";

                    comm.Parameters.Add(new IngresParameter()
                    { ParameterName = "@CORRGUID", DbType = DbType.Guid, SourceColumn = "CORRGUID", Value = CORRGUID == null ? (object)DBNull.Value : CORRGUID });

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

       public static int Production_Fill(string CS, Fask.Interfaces.DataSets.Vyroba ds)
        {
            IngresConnection conn = null;
            IngresCommand comm = null;
            int returnValue = 0;

            using (conn = new IngresConnection(CS))
            {
                using (comm = conn.CreateCommand())
                {
                    comm.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_Production;
                    comm.CommandType = CommandType.Text;
                    using (var ada = new IngresDataAdapter())
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
            IngresConnection connection = null;
            IngresCommand command = null;
            IngresDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new IngresDataAdapter();
            connection = new IngresConnection(CS);
            command = new IngresCommand();
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

            IngresConnection connection = null;
            IngresCommand command = null;
            IngresDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba vyrobaDataSet1 = new Fask.Interfaces.DataSets.Vyroba();


            adapter = new IngresDataAdapter();
            connection = new IngresConnection(CS);
            command = new IngresCommand();
            command.Connection = connection;

            command.CommandText =
                "Select l.firstname, l.surname,";
            if (filtr.VyrobaPouzivatTabulkuZbozi)
                command.CommandText += "z.ITEMDESC,";
            command.CommandText +=
                "o.name operationName, m.name machineName, g.name groupName" +
                ", sklady.skl_desc skladName" +
                ", lokace.Barcode lokaceKod, lokace.Description lokaceName" +
                ", hlavicky.SOPDESC popiszakazky" +
                ", p.* from " + Fask.SQL.Constants.Common.TABLE_Production + " p";


            if (filtr.VyrobaPouzivatTabulkuZbozi)
            {
                command.CommandText += " left join (select distinct ITEMDESC, ITEMNMBR from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + ") z on z.itemnmbr = p.itemnmbr";
            }

            command.CommandText += " left join " + Fask.SQL.Constants.Common.TABLE_FASK_LOGINS + " l on l.USERID = p.userid" +
                " left join " + Fask.SQL.Constants.Common.TABLE_Operations + " o on o.id = p.operationid" +
                " left join " + Fask.SQL.Constants.Common.TABLE_Machines + " m on m.id = p.machineid" +
                " left join " + Fask.SQL.Constants.Common.TABLE_VLoginsGroups + " vg on l.USERID = vg.loginid" +
                " left join " + Fask.SQL.Constants.Common.TABLE_Groups + " g on vg.groupid = g.id" +
                " left join " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " sklady on sklady.skl_id = p.skl_id" +
                " left join " + Fask.SQL.Constants.Common.TABLE_CZMST094 + " lokace on lokace.skl_id = p.skl_id and lokace.locncode=p.locncode" +
                " left join " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " hlavicky on hlavicky.SOPNUMBE = p.SOPNUMBE" +
                " Where ";

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

        public static int? CountByGUID(IngresConnection conn, IngresTransaction trans, Guid GUID)
        {

            object returnValue;
            try
            {

                using (var com = conn.CreateCommand())
                {
                    com.Transaction = trans;
                    com.CommandText = "SELECT COUNT(*) FROM Production WHERE (GUID = @GUID)";

                    com.Parameters.AddWithValue("@GUID", GUID);

                    com.CommandType = System.Data.CommandType.Text;

                    returnValue = com.ExecuteScalar();


                    if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
                    {
                        return null;
                    }
                    else
                    {
                        return (int)returnValue;
                    }
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }


        }

    }
}
