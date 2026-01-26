using Fask.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql.Database
{
    public class Vyroba_CZPRO_VPP
    {

        #region CZPRO_VPP OK

        internal static int Update(object data, string CS)
        {
            System.Data.SqlClient.SqlTransaction transaction = null;
            SqlConnection connection = null;
            try
            {
                int result = 0;
                using (connection = new SqlConnection(CS))
                {
                    connection.Open();

                    transaction = connection.BeginTransaction();

                    using (var commandInsert = connection.CreateCommand())
                    using (var commandUpdate = connection.CreateCommand())
                    using (var commandDelete = connection.CreateCommand())
                    using (var commandSelect = connection.CreateCommand())
                    {

                        commandInsert.Transaction = transaction;
                        commandUpdate.Transaction = transaction;
                        commandDelete.Transaction = transaction;
                        commandSelect.Transaction = transaction;

                        InitializeCommandInsert_CZPRO_VPP(commandInsert);
                        InitializeCommandUpdate_CZPRO_VPP(commandUpdate);
                        InitializeCommandDelete_CZPRO_VPP(commandDelete);
                        InitializeCommandSelect_CZPRO_VPP(commandSelect);

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

                    transaction.Commit();

                }
                return result;
            }
            catch (Exception ex)
            {
                //Logging.Log.Write(ex);
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch (Exception exTransaction)
                {
                    //Logging.Log.Write(exTransaction);
                    Logging.ExceptionHandler2.Handle(exTransaction);
                }

                throw ex;
            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #region Inicialize metody

        public static void InitializeCommandInsert_CZPRO_VPP(SqlCommand command)
        {
            command.CommandText = @"INSERT INTO [CZPRO_VPP] (" +
                " [CountEntries], [SOPNUMBE], [ITEMNMBR], [ITEMTYPE], [ITEMDESC], " +
                " [ITEMMJ], [VNDDOCNMP], [VNDITNUM], [ORD], [BarcodeP], " +
                " [LOCNCODE], [QTYSHPPD], [QTYDOKON], [QTYPACK], [QTYPACKMJ], " +
                " [TIMEMODE], [TIMEPREP], [TIMEUNIT], [DtProdT], [DtProdL], " +
                " [SerNumT], [SerNumL], [VerT], [VerL], [TermID], " +
                " [LSTMod], [BarcodeT], [CZ_REZ1_Track], [CZ_REZ2_Track], [CZ_REZ3_Track]," +
                " [CZ_REZ4_Track], [CZ_REZ5_Track], [WEIGHT_TARA], [WEIGHT_NETTO], [WEIGHT_TOL_PLUS], [WEIGHT_TOL_MINUS] " +
            " ) VALUES (" +
                " @CountEntries, @SOPNUMBE, @ITEMNMBR, @ITEMTYPE, @ITEMDESC, " +
                " @ITEMMJ, @VNDDOCNMP, @VNDITNUM, @ORD, @BarcodeP, " +
                " @LOCNCODE, @QTYSHPPD, @QTYDOKON, @QTYPACK, @QTYPACKMJ, " +
                " @TIMEMODE, @TIMEPREP, @TIMEUNIT, @DtProdT, @DtProdL, " +
                " @SerNumT, @SerNumL, @VerT, @VerL, @TermID, " +
                " @LSTMod, @BarcodeT, @CZ_REZ1_Track, @CZ_REZ2_Track, @CZ_REZ3_Track," +
                " @CZ_REZ4_Track, @CZ_REZ5_Track, @WEIGHT_TARA, @WEIGHT_NETTO, @WEIGHT_TOL_PLUS, @WEIGHT_TOL_MINUS " +
            " ) ";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMMJ", DbType = System.Data.DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNMP", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNMP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYDOKON", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYDOKON", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACKMJ", DbType = System.Data.DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEMODE", DbType = System.Data.DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEPREP", DbType = System.Data.DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEUNIT", DbType = System.Data.DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DtProdT", DbType = System.Data.DbType.Byte, SourceColumn = "DtProdT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DtProdL", DbType = System.Data.DbType.Int16, SourceColumn = "DtProdL", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@SerNumT", DbType = System.Data.DbType.Byte, SourceColumn = "SerNumT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SerNumL", DbType = System.Data.DbType.Int16, SourceColumn = "SerNumL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VerT", DbType = System.Data.DbType.Byte, SourceColumn = "VerT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VerL", DbType = System.Data.DbType.Int16, SourceColumn = "VerL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Int16, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@BarcodeT", DbType = System.Data.DbType.Byte, SourceColumn = "BarcodeT", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ1_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_Track", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ2_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_Track", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ3_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ3_Track", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ4_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ4_Track", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ5_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ5_Track", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT_TARA", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_TARA", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT_NETTO", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_NETTO", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT_TOL_PLUS", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_TOL_PLUS", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT_TOL_MINUS", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_TOL_MINUS", SourceVersion = DataRowVersion.Current });


        }

        public static void InitializeCommandUpdate_CZPRO_VPP(SqlCommand command)
        {

            command.CommandText = "UPDATE [CZPRO_VPP] SET " +
             " [CountEntries] = @CountEntries, " +
             " [SOPNUMBE] = @SOPNUMBE, " +
             " [ITEMNMBR] = @ITEMNMBR, " +
             " [ITEMTYPE] = @ITEMTYPE, " +
             " [ITEMDESC] = @ITEMDESC, " +
             " [ITEMMJ] = @ITEMMJ, " +
             " [VNDDOCNMP] = @VNDDOCNMP," +
             " [VNDITNUM] = @VNDITNUM, " +
             " [ORD] = @ORD, " +
             " [BarcodeP] = @BarcodeP, " +
             " [LOCNCODE] = @LOCNCODE, " +
             " [QTYSHPPD] = @QTYSHPPD, " +
             " [QTYDOKON] = @QTYDOKON, " +
             " [QTYPACK] = @QTYPACK, " +
             " [QTYPACKMJ] = @QTYPACKMJ, " +
             " [TIMEMODE] = @TIMEMODE, " +
             " [TIMEPREP] = @TIMEPREP, " +
             " [TIMEUNIT] = @TIMEUNIT, " +
             " [DtProdT] = @DtProdT, " +
             " [DtProdL] = @DtProdL, " +
             " [SerNumT] = @SerNumT, " +
             " [SerNumL] = @SerNumL, " +
             " [VerT] = @VerT, " +
             " [VerL] = @VerL, " +
             " [TermID] = @TermID, " +
             " [LSTMod] = @LSTMod, " +
             " [BarcodeT] = @BarcodeT, " +
             " [CZ_REZ1_Track] = @CZ_REZ1_Track," +
             " [CZ_REZ2_Track] = @CZ_REZ2_Track," +
             " [CZ_REZ3_Track] = @CZ_REZ3_Track," +
             " [CZ_REZ4_Track] = @CZ_REZ4_Track," +
             " [CZ_REZ5_Track] = @CZ_REZ5_Track," +
             " [WEIGHT_TARA] = @WEIGHT_TARA," +
             " [WEIGHT_NETTO] = @WEIGHT_NETTO," +
             " [WEIGHT_TOL_PLUS] = @WEIGHT_TOL_PLUS," +
             " [WEIGHT_TOL_MINUS] = @WEIGHT_TOL_MINUS" +
             " WHERE " +
             " ([CountEntries] = @Original_CountEntries) " +
             " AND ([SOPNUMBE] = @Original_SOPNUMBE) " +
             " AND ([ITEMNMBR] = @Original_ITEMNMBR) " +
             " AND ([BarcodeP] = @Original_BarcodeP) ";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMTYPE", DbType = DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMMJ", DbType = DbType.String, SourceColumn = "ITEMMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNMP", DbType = DbType.String, SourceColumn = "VNDDOCNMP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = DbType.String, SourceColumn = "VNDITNUM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = DbType.Int32, SourceColumn = "ORD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@BarcodeP", DbType = DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYDOKON", DbType = DbType.Decimal, SourceColumn = "QTYDOKON", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACKMJ", DbType = DbType.String, SourceColumn = "QTYPACKMJ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEMODE", DbType = DbType.Int32, SourceColumn = "TIMEMODE", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEPREP", DbType = DbType.Single, SourceColumn = "TIMEPREP", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEUNIT", DbType = DbType.Single, SourceColumn = "TIMEUNIT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DtProdT", DbType = DbType.Byte, SourceColumn = "DtProdT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DtProdL", DbType = DbType.Int16, SourceColumn = "DtProdL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SerNumT", DbType = DbType.Byte, SourceColumn = "SerNumT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SerNumL", DbType = DbType.Int16, SourceColumn = "SerNumL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VerT", DbType = DbType.Byte, SourceColumn = "VerT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VerL", DbType = DbType.Int16, SourceColumn = "VerL", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TermID", DbType = DbType.Byte, SourceColumn = "TermID", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@LSTMod", DbType = DbType.DateTime, SourceColumn = "LSTMod", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@BarcodeT", DbType = DbType.Byte, SourceColumn = "BarcodeT", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ1_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_Track", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ2_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_Track", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ3_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ3_Track", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ4_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ4_Track", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ5_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ5_Track", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT_TARA", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_TARA", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT_NETTO", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_NETTO", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT_TOL_PLUS", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_TOL_PLUS", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT_TOL_MINUS", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT_TOL_MINUS", SourceVersion = DataRowVersion.Current });


            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_CountEntries", DbType = DbType.Int32, SourceColumn = "CountEntries", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_SOPNUMBE", DbType = DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_ITEMNMBR", DbType = DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Original_BarcodeP", DbType = DbType.String, SourceColumn = "BarcodeP", SourceVersion = DataRowVersion.Original });


        }

        public static void InitializeCommandDelete_CZPRO_VPP(SqlCommand command)
        {
            command.CommandText = "DELETE FROM CZPRO_VPP WHERE " +
                " (CountEntries = @Original_CountEntries) " +
                " AND (SOPNUMBE = @Original_SOPNUMBE) " +
                " AND (ITEMNMBR = @Original_ITEMNMBR)" +
                " AND (BarcodeP = @Original_BarcodeP)";


            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_CountEntries",
                DbType = System.Data.DbType.Int32,
                SourceColumn = "CountEntries",
                SourceVersion = System.Data.DataRowVersion.Original
            });

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_SOPNUMBE",
                DbType = System.Data.DbType.String,
                SourceColumn = "SOPNUMBE",
                SourceVersion = System.Data.DataRowVersion.Original
            });

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_ITEMNMBR",
                DbType = System.Data.DbType.String,
                SourceColumn = "ITEMNMBR",
                SourceVersion = System.Data.DataRowVersion.Original
            });

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_BarcodeP",
                DbType = System.Data.DbType.String,
                SourceColumn = "BarcodeP",
                SourceVersion = System.Data.DataRowVersion.Original
            });

        }

        public static void InitializeCommandSelect_CZPRO_VPP(SqlCommand command)
        {
            command.CommandText = "Select * from CZPRO_VPP";
        }


        #endregion

        #endregion


        internal static Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSOPNUMBEITEMNMBR(string CS, int CountEntries, string SOPNUMBE, string ITEMNMBR)
        {

            Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();


            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(CS))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPP +
                                " WHERE " +
                                " (CountEntries = " + CountEntries.ToString() + ") AND (SOPNUMBE = '" + SOPNUMBE + "') AND (ITEMNMBR = '" + ITEMNMBR + "')";

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }

        }

        internal static Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(string CS, int CountEntries, string SOPNUMBE, string ITEMNMBR, string BarcodeP)
        {
            Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();
            try
            {

                using (var con = new System.Data.SqlClient.SqlConnection(CS))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPP +
                                " WHERE" +
                                " (CountEntries = " + CountEntries.ToString() + ") AND (SOPNUMBE = '" + SOPNUMBE + "') AND (ITEMNMBR = '" + ITEMNMBR + "') AND (BarcodeP = '" + BarcodeP + "')";


                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }

        }

        internal static int FillByCountEntriesAndSOPNUMBE(string CS, Fask.Interfaces.DataSets.Vyroba ds, string TypeORDERBY, int CountEntries, string SOPNUMBE)
        {
            try
            {
                ds.CZPRO_VPP.Clear();

                using (var con = new System.Data.SqlClient.SqlConnection(CS))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT " +
                                " vpp.CountEntries, vpp.SOPNUMBE, vpp.ITEMNMBR, vpp.ITEMTYPE, vpp.ITEMDESC, vpp.ITEMMJ, vpp.VNDDOCNMP, vpp.VNDITNUM, vpp.ORD, vpp.BarcodeP," +
                                " vpp.LOCNCODE, vpp.QTYSHPPD, vpp.QTYDOKON, vpp.QTYPACK, vpp.QTYPACKMJ, vpp.TIMEMODE, vpp.TIMEPREP, vpp.TIMEUNIT, vpp.DtProdT, vpp.DtProdL, " +
                                " vpp.SerNumT, vpp.SerNumL, vpp.VerT, vpp.VerL, vpp.TermID, vpp.LSTMod, vpp.DEX_ROW_ID, vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) " +
                                " AS QTYODVEDENO, ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO " +
                                " , vpp.BarcodeT " +
                                " , vpp.CZ_REZ1_Track , vpp.CZ_REZ2_Track, vpp.CZ_REZ3_Track, vpp.CZ_REZ4_Track, vpp.CZ_REZ5_Track " +
                                " , vpp.WEIGHT_TARA, vpp.WEIGHT_NETTO, vpp.WEIGHT_TOL_PLUS, vpp.WEIGHT_TOL_MINUS " +
                                " FROM " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPP +
                                " AS vpp LEFT OUTER JOIN " +
                                " (SELECT CountEntries, SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, COUNT(*) AS CNTODVEDENO " +
                                " FROM Production " +
                                " GROUP BY CountEntries, SOPNUMBE, ITEMNMBR) AS psum ON psum.CountEntries = vpp.CountEntries AND psum.SOPNUMBE = vpp.SOPNUMBE AND " +
                                " psum.ITEMNMBR = vpp.ITEMNMBR " +
                                " WHERE (vpp.CountEntries = '" + CountEntries.ToString() + "') AND (vpp.SOPNUMBE = '" + SOPNUMBE + "') ";
                            //" WHERE (vpp.CountEntries = @CountEntries) AND (vpp.SOPNUMBE = @SOPNUMBE) ";                                 

                            if (string.IsNullOrEmpty(TypeORDERBY))
                            {
                                select += " ORDER BY vpp.ITEMDESC ";
                            }
                            else
                            {
                                select += " ORDER BY vpp." + TypeORDERBY;
                            }

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(ds, ds.CZPRO_VPP.TableName);
                            return returnValue;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return -1;
            }
        }

        internal static void DeleteByCountEntriesSOPNUMBE(string CS, int CountEntries, string SOPNUMBE)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    using (SqlCommand comm = con.CreateCommand())
                    {
                        con.Open();
                        comm.CommandType = System.Data.CommandType.Text;
                        comm.CommandText = "DELETE FROM CZPRO_VPP " +
                            " WHERE (CountEntries = " + CountEntries + ") AND (SOPNUMBE = '" + SOPNUMBE + "')";

                        int retunValue;
                        retunValue = comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static void VPP_Fill(string CS, Fask.Interfaces.DataSets.Vyroba ds)
        {
            try
            {

                ds.CZPRO_VPP.Clear();

                using (var con = new System.Data.SqlClient.SqlConnection(CS))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT" +
                                " vpp.CountEntries, vpp.SOPNUMBE, vpp.ITEMNMBR, vpp.ITEMTYPE, vpp.ITEMDESC, " +
                                " vpp.ITEMMJ, vpp.VNDDOCNMP, vpp.VNDITNUM, vpp.ORD, vpp.BarcodeP, " +
                                " vpp.LOCNCODE, vpp.QTYSHPPD, vpp.QTYDOKON, vpp.QTYPACK, vpp.QTYPACKMJ, " +
                                " vpp.TIMEMODE, vpp.TIMEPREP, vpp.TIMEUNIT, vpp.DtProdT, vpp.DtProdL, " +
                                " vpp.SerNumT, vpp.SerNumL, vpp.VerT, vpp.VerL, vpp.TermID, " +
                                " vpp.LSTMod, vpp.DEX_ROW_ID, vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) AS QTYODVEDENO, ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO, " +
                                " vpp.BarcodeT " +
                                " , vpp.CZ_REZ1_Track , vpp.CZ_REZ2_Track, vpp.CZ_REZ3_Track, vpp.CZ_REZ4_Track, vpp.CZ_REZ5_Track " +
                                " , vpp.WEIGHT_TARA, vpp.WEIGHT_NETTO, vpp.WEIGHT_TOL_PLUS, vpp.WEIGHT_TOL_MINUS " +
                                " FROM " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPP +
                                " AS vpp " +
                                " LEFT OUTER JOIN " +
                                " (SELECT CountEntries, SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, COUNT(*) AS CNTODVEDENO FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                                " GROUP BY CountEntries, SOPNUMBE, ITEMNMBR) AS psum " +
                                " ON psum.CountEntries = vpp.CountEntries " +
                                " AND psum.SOPNUMBE = vpp.SOPNUMBE " +
                                " AND psum.ITEMNMBR = vpp.ITEMNMBR";


                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(ds, ds.CZPRO_VPP.TableName);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        internal static Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVPPList(string cs, Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr)
        {

            Fask.Interfaces.DataSets.Vyroba ds = new Interfaces.DataSets.Vyroba();
            try
            {
                ds.CZPRO_VPP.Clear();

                using (var con = new System.Data.SqlClient.SqlConnection(cs))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT " +
                                " vpp.CountEntries, vpp.SOPNUMBE, vpp.ITEMNMBR, vpp.ITEMTYPE, vpp.ITEMDESC, " +
                                " vpp.ITEMMJ, vpp.VNDDOCNMP, vpp.VNDITNUM, vpp.ORD, vpp.BarcodeP," +
                                " vpp.LOCNCODE, vpp.QTYSHPPD, vpp.QTYDOKON, vpp.QTYPACK, vpp.QTYPACKMJ, " +
                                " vpp.TIMEMODE, vpp.TIMEPREP, vpp.TIMEUNIT, vpp.DtProdT, vpp.DtProdL, " +
                                " vpp.SerNumT, vpp.SerNumL, vpp.VerT, vpp.VerL, vpp.TermID, " +
                                " vpp.LSTMod, vpp.DEX_ROW_ID, " +
                                " vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) AS QTYODVEDENO, " +
                                " ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO " +
                                " , vpp.BarcodeT" +
                                " , vpp.CZ_REZ1_Track , vpp.CZ_REZ2_Track, vpp.CZ_REZ3_Track, vpp.CZ_REZ4_Track, vpp.CZ_REZ5_Track " +
                                " , vpp.WEIGHT_TARA, vpp.WEIGHT_NETTO, vpp.WEIGHT_TOL_PLUS, vpp.WEIGHT_TOL_MINUS " +
                                " FROM " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPP +
                                " AS vpp LEFT OUTER JOIN " +
                                " (SELECT CountEntries, SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, COUNT(*) AS CNTODVEDENO " +
                                " FROM " + Fask.SQL.Constants.Common.TABLE_Production +
                                " GROUP BY CountEntries, SOPNUMBE, ITEMNMBR) AS psum ON psum.CountEntries = vpp.CountEntries AND psum.SOPNUMBE = vpp.SOPNUMBE AND " +
                                " psum.ITEMNMBR = vpp.ITEMNMBR " +
                                " WHERE  1 = 1";



                            if (string.IsNullOrEmpty(filtr.SOPNUMBE))
                            {
                                select += "AND (vpp.SOPNUMBE = '" + filtr.SOPNUMBE + "') ";
                            }

                            if (filtr.CountEntries.HasValue)
                            {
                                select += "AND (vpp.CountEntries = " + filtr.CountEntries.Value.ToString() + ") ";
                            }

                            if (string.IsNullOrEmpty(filtr.OrderBy))
                            {
                                select += " ORDER BY vpp.ITEMDESC ";
                            }
                            else
                            {
                                select += " ORDER BY vpp." + filtr.OrderBy;
                            }

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(ds, ds.CZPRO_VPP.TableName);
                            return ds;
                        }
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
