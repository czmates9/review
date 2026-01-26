using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModulePohodaXML.Database
{
    class Sklady_CZMST_DI
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


                        //InitializeCommandInsert_CZMST_DI(commandInsert);
                        InitializeCommandUpdate_CZMST_DI(commandUpdate);
                        InitializeCommandDelete_CZMST_DI(commandDelete);
                        // InitializeCommandSelect_CZMST_DI(commandSelect);

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

        private static void InitializeCommandInsert_CZMST_DI(SqlCommand command)
        {
            command.CommandText =
                @" INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZMST_DI +
            " (CountEntries, SOPNUMBE, SOPTYPE, SOPDESC, VNDDOCNMH, BarcodeH, LOCNCODE, DateProd, Rez1, Rez2, TermID, LSTMod, Active, USERID)" +
            " VALUES (@CountEntries,@SOPNUMBE,@SOPTYPE,@SOPDESC,@VNDDOCNMH,@BarcodeH,@LOCNCODE,@DateProd,@Rez1,@Rez2,@TermID,@LSTMod,@Active, @USERID)";

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
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@USERID", DbType = DbType.Int32, SourceColumn = "USERID", SourceVersion = DataRowVersion.Current });


        }

        private static void InitializeCommandUpdate_CZMST_DI(SqlCommand command)
        {
            //command.CommandText =
            //   @" UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST_DI +
            //   " SET CountEntries = @CountEntries," +
            //   " SOPNUMBE = @SOPNUMBE," +
            //   " SOPTYPE = @SOPTYPE," +
            //   " SOPDESC = @SOPDESC," +
            //   " VNDDOCNMH = @VNDDOCNMH," +
            //   " BarcodeH = @BarcodeH," +
            //   " LOCNCODE = @LOCNCODE," +
            //   " DateProd = @DateProd," +
            //   " Rez1 = @Rez1," +
            //   " Rez2 = @Rez2," +
            //   " TermID = @TermID," +
            //   " LSTMod = @LSTMod," +
            //   " Active = @Active," +
            //   " USERID = @USERID" +
            //   " WHERE(CountEntries = @Original_CountEntries)" +
            //   " AND (SOPNUMBE = @Original_SOPNUMBE)";


            command.CommandText =
                     @" UPDATE " + Fask.SQL.Constants.Common.TABLE_CZMST_DI +
                  " SET [CountEntries] = @CountEntries" +
                ",[VNDITNUM] = @VNDITNUM" +//
       ",[CZ_CarKod] =  @CZ_CarKod" +//
       ",[ODB_ID] =  @ODB_ID" +//
      ",[STR_ID] =  @STR_ID" +//
      ",[DOC_ID] = @DOC_ID" +//
      ",[DOC_ID2] =  @DOC_ID2" +//
      ",[SKL_ID] =  @SKL_ID" +//
      ",[PRAC_ID] =  @PRAC_ID" +//
      ",[ITEMNMBR] =  @ITEMNMBR" +//
      ",[ITEMCODE] =  @ITEMCODE" +//
      ",[LOCNCODE] =  @LOCNCODE" +//
      ",[MJ] =  @MJ" +//
      ",[QTYSHPPD] =  @QTYSHPPD" +//
      ",[QTYSHPPDMJ] =  @QTYSHPPDMJ" +//
      ",[QTYPACK] =  @QTYPACK" +//
      ",[SERLTNUM] =  @SERLTNUM" +//
      ",[TAXAMPIE] = @TAXAMPIE" +//
      ",[AMOUNPIE] = @AMOUNPIE" +//
      ",[WITHTAX] = @WITHTAX" +//
      ",[PRICEX] =  @PRICEX" +//
      ",[mena_ID] = @mena_ID" +//
      ",[TAXAMPIEM] = @TAXAMPIEM" +//
      ",[AMOUNPIEM] =  @AMOUNPIEM" +//
      ",[mena_IDM] =  @mena_IDM" +//
      ",[REZ_1] =  @REZ_1" +//
      ",[REZ_2] =  @REZ_2" +//
      ",[REZ_3] =  @REZ_3" +//
      ",[REZ_4] =  @REZ_4" +//
      ",[USER_ID] =  @USER_ID" +//
      ",[DATEDONE] =  @DATEDONE" +//
      ",[TIMEDONE] =  @TIMEDONE" +//
      ",[GUID] =  @GUID" +//
      ",[INPUT_MODE] = @INPUT_MODE" +//
      ",[ID_TERMINAL] =  @ID_TERMINAL" +//
      ",[LOCNCODEDEST] = @LOCNCODEDEST" +//
      ",[SKL_ID_DEST] =  @SKL_ID_DEST" +//
      ",[WEIGHT] =  @WEIGHT" +//
      ",[NMBRPAL] = @NMBRPAL" +//
      ",[TYPEPAL] =  @TYPEPAL" +//
      ",[PRINTED] =  @PRINTED" +
       ",[EXPIRACE] =   @EXPIRACE" +//
      ",[AttributeToSN] =   @AttributeToSN" +
                      " WHERE(CountEntries = @Original_CountEntries)" +
                     " AND (DEX_ROW_ID = @Original_DEX_ROW_ID)";



            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@PRAC_ID", DbType = DbType.String, SourceColumn = "PRAC_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@DOC_ID2", DbType = DbType.String, SourceColumn = "DOC_ID2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@DOC_ID", DbType = DbType.String, SourceColumn = "DOC_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@STR_ID", DbType = DbType.String, SourceColumn = "STR_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@ODB_ID", DbType = DbType.String, SourceColumn = "ODB_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@NMBRPAL", DbType = DbType.String, SourceColumn = "NMBRPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@AttributeToSN", DbType = DbType.String, SourceColumn = "AttributeToSN", SourceVersion = DataRowVersion.Current });


            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@mena_IDM", DbType = DbType.String, SourceColumn = "mena_IDM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@AMOUNPIEM", DbType = DbType.Decimal, SourceColumn = "AMOUNPIEM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@REZ_3", DbType = DbType.String, SourceColumn = "REZ_3", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@REZ_4", DbType = DbType.String, SourceColumn = "REZ_4", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TAXAMPIEM", DbType = DbType.Decimal, SourceColumn = "TAXAMPIEM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@mena_ID", DbType = DbType.String, SourceColumn = "mena_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@PRICEX", DbType = DbType.Byte, SourceColumn = "PRICEX", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@WITHTAX", DbType = DbType.Byte, SourceColumn = "WITHTAX", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@AMOUNPIE", DbType = DbType.Decimal, SourceColumn = "AMOUNPIE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TAXAMPIE", DbType = DbType.Decimal, SourceColumn = "TAXAMPIE", SourceVersion = DataRowVersion.Current });



            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@USER_ID", DbType = DbType.Int32, SourceColumn = "USER_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@INPUT_MODE", DbType = DbType.Byte, SourceColumn = "INPUT_MODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@VNDITNUM", DbType = DbType.String, SourceColumn = "VNDITNUM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@CZ_CarKod", DbType = DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@SKL_ID", DbType = DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@MJ", DbType = DbType.String, SourceColumn = "MJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@QTYSHPPD", DbType = DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@QTYSHPPDMJ", DbType = DbType.Decimal, SourceColumn = "QTYSHPPDMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@LOCNCODEDEST", DbType = DbType.String, SourceColumn = "LOCNCODEDEST", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@SKL_ID_DEST", DbType = DbType.String, SourceColumn = "SKL_ID_DEST", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@DATEDONE", DbType = DbType.String, SourceColumn = "DATEDONE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TIMEDONE", DbType = DbType.String, SourceColumn = "TIMEDONE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@REZ_1", DbType = DbType.String, SourceColumn = "REZ_1", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@REZ_2", DbType = DbType.String, SourceColumn = "REZ_2", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@WEIGHT", DbType = DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@PRINTED", DbType = DbType.Byte, SourceColumn = "PRINTED", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@TYPEPAL", DbType = DbType.String, SourceColumn = "TYPEPAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@ITEMCODE", DbType = DbType.String, SourceColumn = "ITEMCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@SERLTNUM", DbType = DbType.String, SourceColumn = "SERLTNUM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@GUID", DbType = DbType.Guid, SourceColumn = "GUID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@ID_TERMINAL", DbType = DbType.Int32, SourceColumn = "ID_TERMINAL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@EXPIRACE", DbType = DbType.DateTime, SourceColumn = "EXPIRACE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@Original_CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@Original_DEX_ROW_ID", DbType = DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = DataRowVersion.Original });

        }

        private static void InitializeCommandDelete_CZMST_DI(SqlCommand command)
        {
            command.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_CZMST_DI +
            //" WHERE (CountEntries = @Original_CountEntries) AND (SOPNUMBE = @Original_SOPNUMBE)";


            " WHERE(CountEntries = @Original_CountEntries)" +
           " AND (DEX_ROW_ID = @Original_DEX_ROW_ID)";


            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_CountEntries",
                DbType = DbType.Int32,
                SourceColumn = "CountEntries",
                SourceVersion = DataRowVersion.Original

            });

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_DEX_ROW_ID",
                DbType = DbType.Int32,
                SourceColumn = "DEX_ROW_ID",
                SourceVersion = DataRowVersion.Original

            });
        }

        private static void InitializeCommandSelect_CZMST_DI(SqlCommand command)
        {
            command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_CZMST_DI;
        }


        #endregion

        //internal static Fask.Interfaces.DataSets.Vyroba Get_CZMST_PIByFilter(string CS, Vyroba_VPH_Filtr filtr)
        //{
        //    Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();
        //    System.Data.SqlClient.SqlConnection conn = null;
        //    System.Data.SqlClient.SqlCommand comm = null;

        //    using (conn = new SqlConnection(CS))
        //    {
        //        using (comm = conn.CreateCommand())
        //        {
        //            comm.CommandType = CommandType.Text;
        //            comm.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH;

        //            comm.CommandText += " WHERE 1 = 1 ";


        //            if (filtr.Active.HasValue)
        //            {
        //                comm.CommandText += " AND Active = " + filtr.Active.Value;
        //            }

        //            if (!string.IsNullOrEmpty(filtr.SOPNUMBE))
        //            {
        //                comm.CommandText += " AND SOPNUMBE like '" + filtr.SOPNUMBE + "%'";
        //            }

        //            using (var ada = new SqlDataAdapter())
        //            {
        //                ada.SelectCommand = comm;
        //                ada.Fill(ds, ds.CZPRO_VPH.TableName);

        //            }
        //        }
        //    }

        //    return ds;
        //}

        //internal static void Fill_CZMST_PI(string CS, Fask.Interfaces.DataSets.Vyroba ds)
        //{
        //    System.Data.SqlClient.SqlConnection conn = null;
        //    System.Data.SqlClient.SqlCommand comm = null;

        //    using (conn = new SqlConnection(CS))
        //    {
        //        using (comm = conn.CreateCommand())
        //        {
        //            comm.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH;
        //            comm.CommandType = CommandType.Text;
        //            using (var ada = new SqlDataAdapter())
        //            {
        //                ada.SelectCommand = comm;
        //                ada.Fill(ds, ds.CZPRO_VPH.TableName);

        //            }
        //        }
        //    }
        //}

        //internal static Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable Get_CZMST_PI_ByCountEntriesSOPNUMBE(string CS, int CountEntries, string SOPNUMBE)
        //{
        //    System.Data.SqlClient.SqlConnection conn = null;
        //    System.Data.SqlClient.SqlCommand comm = null;
        //    Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable();

        //    using (conn = new SqlConnection(CS))
        //    {
        //        using (comm = conn.CreateCommand())
        //        {
        //            comm.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH +
        //                " WHERE (CountEntries = @CountEntries)" +
        //                " AND (SOPNUMBE = @SOPNUMBE)";

        //            comm.Parameters.Add(new SqlParameter()
        //            { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries });
        //            comm.Parameters.Add(new SqlParameter()
        //            { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

        //            comm.CommandType = CommandType.Text;

        //            using (var ada = new SqlDataAdapter())
        //            {
        //                ada.SelectCommand = comm;
        //                ada.Fill(dt);
        //            }
        //        }
        //    }
        //    return dt;
        //}

        //internal static void Insert_CZMST_PI(string CS, Vyroba.CZPRO_VPHRow row)
        //{

        //    System.Data.SqlClient.SqlTransaction transaction = null;
        //    System.Data.SqlClient.SqlConnection conn = null;
        //    try
        //    {

        //        using (conn = new System.Data.SqlClient.SqlConnection(CS))
        //        {

        //            conn.Open();

        //            transaction = conn.BeginTransaction();

        //            using (var commandInsert = conn.CreateCommand())
        //            {
        //                //command a parametry definice
        //                commandInsert.CommandText =
        //                    @" INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH +
        //                    " (CountEntries, SOPNUMBE, SOPTYPE, SOPDESC, VNDDOCNMH, BarcodeH, LOCNCODE, DateProd, Rez1, Rez2, TermID, LSTMod, Active, USERID)" +
        //                    " VALUES (@CountEntries,@SOPNUMBE,@SOPTYPE,@SOPDESC,@VNDDOCNMH,@BarcodeH,@LOCNCODE,@DateProd,@Rez1,@Rez2,@TermID,@LSTMod,@Active,@USERID)";

        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", Value = row.CountEntries });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", Value = row.SOPNUMBE == null ? (object)DBNull.Value : row.SOPNUMBE });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@SOPTYPE", DbType = DbType.String, SourceColumn = "SOPTYPE", Value = row.SOPTYPE == null ? (object)DBNull.Value : row.SOPTYPE });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@SOPDESC", DbType = DbType.String, SourceColumn = "SOPDESC", Value = row.SOPDESC == null ? (object)DBNull.Value : row.SOPDESC });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@VNDDOCNMH", DbType = DbType.String, SourceColumn = "VNDDOCNMH", Value = row.VNDDOCNMH == null ? (object)DBNull.Value : row.VNDDOCNMH });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@BarcodeH", DbType = DbType.String, SourceColumn = "BarcodeH", Value = row.BarcodeH == null ? (object)DBNull.Value : row.BarcodeH });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", Value = row.LOCNCODE == null ? (object)DBNull.Value : row.LOCNCODE });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@DateProd", DbType = DbType.Int16, SourceColumn = "DateProd", Value = row.DateProd });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@Rez1", DbType = DbType.String, SourceColumn = "Rez1", Value = row.Rez1 == null ? (object)DBNull.Value : row.Rez1 });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@Rez2", DbType = DbType.String, SourceColumn = "Rez2", Value = row.Rez2 == null ? (object)DBNull.Value : row.Rez2 });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", Value = row.TermID });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@LSTMod", DbType = DbType.DateTime, SourceColumn = "LSTMod", Value = row.LSTMod == null ? (object)DBNull.Value : row.LSTMod });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@Active", DbType = DbType.Byte, SourceColumn = "Active", Value = row.Active });
        //                commandInsert.Parameters.Add(new SqlParameter()
        //                { ParameterName = "@USERID", DbType = DbType.Int32, SourceColumn = "USERID", Value = row.USERID });


        //                commandInsert.Transaction = transaction;
        //                commandInsert.ExecuteNonQuery();
        //            }

        //            transaction.Commit();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        try
        //        {
        //            if (transaction != null)
        //                transaction.Rollback();
        //        }
        //        catch (Exception exTransaction)
        //        {
        //            throw exTransaction;
        //        }

        //        throw ex;
        //    }
        //    finally
        //    {
        //        if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
        //        {
        //            conn.Close();
        //            conn.Dispose();
        //        }
        //    }
        //}
    }
}
