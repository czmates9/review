using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Fask.SQL.Database
{
    class Vydej
    {
        #region Zravidovano lehce

        #region Update

        #region Update SE

        public static int Update_CZMST_SE(object data, SqlConnection connection, SqlTransaction trans)
        {
            try
            {
                int result = 0;

                using (var commandInsert = connection.CreateCommand())
                using (var commandUpdate = connection.CreateCommand())
                using (var commandSelect = connection.CreateCommand())
                {
                    commandInsert.Transaction = trans;
                    commandUpdate.Transaction = trans;
                    commandSelect.Transaction = trans;
                    InitializeCommandInsert_SE(commandInsert);
                    InitializeCommandUpdate_SE(commandUpdate);
                    InitializeCommandSelect_SE(commandSelect);

                    using (var adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = commandInsert;
                        adapter.UpdateCommand = commandUpdate;
                        adapter.SelectCommand = commandSelect;

                        var dataIsDataSet = data as System.Data.DataSet;
                        var dataIsDataTable = data as System.Data.DataTable;
                        var dataIsDataRow = data as System.Data.DataRow;
                        var dataIsDataRowArray = data as System.Data.DataRow[];


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

                return result;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #region Inicialize metody

        public static void InitializeCommandInsert_SE(SqlCommand command)
        {
            //command.CommandText = " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_SE + " ON ";

            command.CommandText += @" INSERT INTO " + Constants.Common.TABLE_CZMST_SE + " (" +
                " CountEntries, SOPNUMBE, ITEMNMBR, ITEMTYPE, ITEMDESC, " +
                " VNDDOCNM, VNDITNUM, ORD, CZ_CarKod, SKL_ID, " +
                " LOCNCODE, MJ, QTYSHPPD, QTYPACK, CZ_DatVyr_Track, " +
                " CZ_DatVyr_Delka, CZ_SerNum_Track, CZ_SerNum_Delka, CZ_SW_Track, CZ_SW_Delka, " +
                " CZ_Doslo, Note, TYPEPAL, QTYPAL, PRIORITY, " +
                " PRINTED, USERID, CZ_REZ1_TRACK, CZ_REZ2_TRACK, ITEMCODE, " +
                " WEIGHT, Realization_Start, Realization_Stop, CZ_Expirace_Track " +
                " ) " +
                " VALUES " +
                "( " +
                " @CountEntries, @SOPNUMBE, @ITEMNMBR, @ITEMTYPE, @ITEMDESC, " +
                " @VNDDOCNM, @VNDITNUM, @ORD, @CZ_CarKod, @SKL_ID, " +
                " @LOCNCODE, @MJ, @QTYSHPPD, @QTYPACK, @CZ_DatVyr_Track, " +
                " @CZ_DatVyr_Delka, @CZ_SerNum_Track, @CZ_SerNum_Delka, @CZ_SW_Track, @CZ_SW_Delka, " +
                " @CZ_Doslo, @Note, @TYPEPAL, @QTYPAL, @PRIORITY, " +
                " @PRINTED, @USERID, @CZ_REZ1_TRACK, @CZ_REZ2_TRACK, @ITEMCODE, " +
                " @WEIGHT, @Realization_Start, @Realization_Stop, @CZ_Expirace_Track" +
                " )";
            //command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_SE + " OFF ";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, SourceColumn = "ITEMTYPE", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC", SourceVersion = System.Data.DataRowVersion.Current });
            
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
            
            command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_DatVyr_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_DatVyr_Track", SourceVersion = System.Data.DataRowVersion.Current });
            
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_DatVyr_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_DatVyr_Delka", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Track", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SerNum_Delka", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SW_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SW_Track", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SW_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SW_Delka", SourceVersion = System.Data.DataRowVersion.Current });
            
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Doslo", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Doslo", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Note", DbType = System.Data.DbType.String, SourceColumn = "Note", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPAL", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPAL", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@PRIORITY", DbType = System.Data.DbType.Byte, SourceColumn = "PRIORITY", SourceVersion = System.Data.DataRowVersion.Current });
            
            command.Parameters.Add(new SqlParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Boolean, SourceColumn = "PRINTED", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@USERID", DbType = System.Data.DbType.Int32, SourceColumn = "USERID", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ1_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_TRACK", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ2_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_TRACK", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = System.Data.DataRowVersion.Current });
            
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Realization_Start", DbType = System.Data.DbType.DateTime, SourceColumn = "Realization_Start", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Realization_Stop", DbType = System.Data.DbType.DateTime, SourceColumn = "Realization_Stop", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Expirace_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Expirace_Track", SourceVersion = System.Data.DataRowVersion.Current });


        }

        public static void InitializeCommandUpdate_SE(SqlCommand command)
        {
            command.CommandText = "UPDATE " + Constants.Common.TABLE_CZMST_SE + " SET CZ_Doslo = @CZ_Doslo " +
                " WHERE (DEX_ROW_ID = @DEX_ROW_ID) AND (CountEntries = @CountEntries)";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Doslo", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Doslo", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = System.Data.DataRowVersion.Original });

        }

        public static void InitializeCommandSelect_SE(SqlCommand command)
        {
            command.CommandText = "Select * from " + Constants.Common.TABLE_CZMST_SE;
        }


        #endregion

        #endregion

        #region Update SI

        public static int Update_CZMST_SI(object data, SqlConnection connection, SqlTransaction trans)
        {
            try
            {
                int result = 0;

                using (var commandInsert = connection.CreateCommand())
                using (var commandSelect = connection.CreateCommand())
                {
                    commandInsert.Transaction = trans;
                    commandSelect.Transaction = trans;

                    InitializeCommandInsert_SI(commandInsert);
                    InitializeCommandSelect_SI(commandSelect);

                    using (var adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = commandInsert;
                        adapter.SelectCommand = commandSelect;

                        var dataIsDataSet = data as System.Data.DataSet;
                        var dataIsDataTable = data as System.Data.DataTable;
                        var dataIsDataRow = data as System.Data.DataRow;
                        var dataIsDataRowArray = data as System.Data.DataRow[];


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

                return result;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #region Inicialize metody

        public static void InitializeCommandInsert_SI(SqlCommand command)
        {
            command.CommandText = "INSERT INTO " + Constants.Common.TABLE_CZMST_SI + " (" +
                " CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDDOCNM, " +
                " VNDITNUM, CZ_CarKod, SKL_ID, LOCNCODE, MJ, " +
                " QTYSHPPD, QTYPACK, QTYSHPPDMJ, SERLTNUM, KOD_SW, " +
                " DAT_VYROBY, REZ_1, REZ_2, ODBER_ID, DATEDONE, " +
                " TIMEDONE, USER_ID, TYPEPAL, NMBRPAL, PRINTED, " +
                " GUID, INPUT_MODE, ID_TERMINAL, ITEMCODE, WEIGHT," +
                " Expirace) " +
                " VALUES " +
                " ( " +
                " @CountEntries, @SOPNUMBE, @ITEMNMBR, @ORD, @VNDDOCNM, " +
                " @VNDITNUM, @CZ_CarKod, @SKL_ID, @LOCNCODE, @MJ, " +
                " @QTYSHPPD, @QTYPACK, @QTYSHPPDMJ, @SERLTNUM, @KOD_SW, " +
                " @DAT_VYROBY, @REZ_1, @REZ_2, @ODBER_ID, @DATEDONE, " +
                " @TIMEDONE, @USER_ID, @TYPEPAL, @NMBRPAL, @PRINTED, " +
                " @GUID, @INPUT_MODE, @ID_TERMINAL, @ITEMCODE, @WEIGHT," +
                " @Expirace " +
                " )";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM", SourceVersion = System.Data.DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW", SourceVersion = System.Data.DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ODBER_ID", DbType = System.Data.DbType.String, SourceColumn = "ODBER_ID", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE", SourceVersion = System.Data.DataRowVersion.Current });
            
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Byte, SourceColumn = "PRINTED", SourceVersion = System.Data.DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace", SourceVersion = System.Data.DataRowVersion.Current });

        }

        public static void InitializeCommandSelect_SI(SqlCommand command)
        {
            command.CommandText = "Select * from " + Constants.Common.TABLE_CZMST_SI;
        }


        #endregion

        #endregion

        #region Update SIH

        public static int Update_CZMST_SIH(object data, SqlConnection connection, SqlTransaction trans)
        {
            try
            {
                int result = 0;

                using (var commandInsert = connection.CreateCommand())
                using (var commandSelect = connection.CreateCommand())
                {
                    commandInsert.Transaction = trans;
                    commandSelect.Transaction = trans;

                    InitializeCommandInsert_SIH(commandInsert);
                    InitializeCommandSelect_SIH(commandSelect);

                    using (var adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = commandInsert;
                        adapter.SelectCommand = commandSelect;

                        var dataIsDataSet = data as System.Data.DataSet;
                        var dataIsDataTable = data as System.Data.DataTable;
                        var dataIsDataRow = data as System.Data.DataRow;
                        var dataIsDataRowArray = data as System.Data.DataRow[];


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

                return result;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #region Inicialize metody

        public static void InitializeCommandInsert_SIH(SqlCommand command)
        {

            command.CommandText = "INSERT INTO " + Constants.Common.TABLE_CZMST_SIH + " (CountEntries, TISKARNA_NAME, PRAC_ID, ISOK, status) " +
                " VALUES " +
                " (@CountEntries, @TISKARNA_NAME, @PRAC_ID, @ISOK, @status)";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@TISKARNA_NAME", DbType = System.Data.DbType.String, SourceColumn = "TISKARNA_NAME", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@PRAC_ID", DbType = System.Data.DbType.String, SourceColumn = "PRAC_ID", SourceVersion = System.Data.DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter() { ParameterName = "@ISOK", DbType = System.Data.DbType.DateTime, SourceColumn = "ISOK", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@status", DbType = System.Data.DbType.Int32, SourceColumn = "status", SourceVersion = System.Data.DataRowVersion.Current });

        }

        public static void InitializeCommandSelect_SIH(SqlCommand command)
        {
            command.CommandText = "Select * from " + Constants.Common.TABLE_CZMST_SIH;
        }


        #endregion

        #endregion

        #region Update SI_BV

        public static int Update_CZMST_SI_BV(object data, SqlConnection connection, SqlTransaction trans)
        {
            try
            {
                int result = 0;

                using (var commandInsert = connection.CreateCommand())
                using (var commandSelect = connection.CreateCommand())
                {
                    commandInsert.Transaction = trans;
                    commandSelect.Transaction = trans;

                    InitializeCommandInsert_SI_BV(commandInsert);
                    InitializeCommandSelect_SI_BV(commandSelect);

                    using (var adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = commandInsert;
                        adapter.SelectCommand = commandSelect;

                        var dataIsDataSet = data as System.Data.DataSet;
                        var dataIsDataTable = data as System.Data.DataTable;
                        var dataIsDataRow = data as System.Data.DataRow;
                        var dataIsDataRowArray = data as System.Data.DataRow[];


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

                return result;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #region Inicialize metody

        public static void InitializeCommandInsert_SI_BV(SqlCommand command)
        {

            command.CommandText  = @"INSERT INTO " + Constants.Common.TABLE_CZMST_SI_BV +
                " (CountEntries, NMBRPAL, USER_ID, pal_W, pal_H, pal_D, pal_WEIGHT) " +
                " VALUES " +
                " (@CountEntries, @NMBRPAL, @USER_ID, @pal_W, @pal_H, @pal_D, @pal_WEIGHT) ";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID", SourceVersion = System.Data.DataRowVersion.Current }); 
            command.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL", SourceVersion = System.Data.DataRowVersion.Current });
            

            command.Parameters.Add(new SqlParameter() { ParameterName = "@pal_W", DbType = System.Data.DbType.Decimal, SourceColumn = "pal_W", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@pal_H", DbType = System.Data.DbType.Decimal, SourceColumn = "pal_H", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@pal_D", DbType = System.Data.DbType.Decimal, SourceColumn = "pal_D", SourceVersion = System.Data.DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@pal_WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "pal_WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });

        }

        public static void InitializeCommandSelect_SI_BV(SqlCommand command)
        {
            command.CommandText = "Select * from " + Constants.Common.TABLE_CZMST_SI_BV;
        }


        #endregion

        #endregion

        #endregion

        public static int Fill_CZMST_SE_SOPNUMBE( Datasets.Vydej.CZMST_SE_SOPNUMBEDataTable dt, int CountEntries)
        {
            Globals_V1.LoadConfiguration();
            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {
                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = "SELECT SOPNUMBE FROM " + Constants.Common.TABLE_CZMST_SE + " WHERE (CountEntries = " + CountEntries.ToString() + ") GROUP BY SOPNUMBE";
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(dt);
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

        #region Get ane Fill CZMST_SE by CountEntries celá tabulka

        internal static Datasets.Vydej.CZMST_SEDataTable GETDATA_CZMSTSE_By_CountEntries(int countEntries)
        {
            Datasets.Vydej.CZMST_SEDataTable tbl_se = new Datasets.Vydej.CZMST_SEDataTable();

            try
            {
                Database.Vydej.Fill_CZMSTSE_By_CountEntries(tbl_se, countEntries);
                return tbl_se;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }

        internal static int Fill_CZMSTSE_By_CountEntries(Datasets.Vydej.CZMST_SEDataTable dt, int countEntries)
        {
            try
            {

                Globals_V1.LoadConfiguration();
                try
                {
                    using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {
                        using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                        {
                            using (var com = con.CreateCommand())
                            {
                                ada.SelectCommand = com;
                                //ada.SelectCommand.CommandText = @"SELECT CZ_CarKod, CZ_DatVyr_Delka, CZ_DatVyr_Track, CZ_Doslo, CZ_SW_Delka, " + 
                                //    " CZ_SW_Track, CZ_SerNum_Delka, CZ_SerNum_Track, CountEntries, DEX_ROW_ID, ITEMDESC, ITEMNMBR, ITEMTYPE, " + 
                                //    " LOCNCODE, MJ, Note, ORD, PRINTED, PRIORITY, QTYPACK, QTYPAL, QTYSHPPD, SKL_ID, SOPNUMBE, TYPEPAL, USERID, " + 
                                //    " VNDDOCNM, VNDITNUM, CZ_REZ1_Track, CZ_REZ2_Track, ITEMCODE, WEIGHT, Realization_Start, Realization_Stop, CZ_Expirace_Track " + 
                                //    " FROM " + Constants.Common.TABLE_CZMST_SE + " WHERE (CountEntries = " + countEntries.ToString() + ")";

                                ada.SelectCommand.CommandText =
                                    " SELECT * FROM " +
                                    Constants.Common.TABLE_CZMST_SE +
                                    " WHERE (CountEntries = " + countEntries.ToString() + ")";

                                ada.SelectCommand.Connection = con;
                                ada.SelectCommand.CommandType = CommandType.Text;

                                int returnValue;
                                returnValue = ada.Fill(dt);
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
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return -1;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return -1;
            }
        }

        #endregion

        #region Get ane Fill CZMST_SI by CountEntries celá tabulka

        internal static Datasets.Vydej.CZMST_SIDataTable GETDATA_CZMSTSI_By_CountEntries(int countEntries)
        {
            Datasets.Vydej.CZMST_SIDataTable tbl_si = new Datasets.Vydej.CZMST_SIDataTable();

            try
            {
                Database.Vydej.Fill_CZMSTSI_By_CountEntries(tbl_si, countEntries);
                return tbl_si;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }

        internal static int Fill_CZMSTSI_By_CountEntries(Datasets.Vydej.CZMST_SIDataTable dt, int countEntries)
        {
            try
            {

                Globals_V1.LoadConfiguration();
                try
                {
                    using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {
                        using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                        {
                            using (var com = con.CreateCommand())
                            {
                                ada.SelectCommand = com;

                                ada.SelectCommand.CommandText =
                                    " SELECT * FROM " +
                                    Constants.Common.TABLE_CZMST_SI +
                                    " WHERE (CountEntries = " + countEntries.ToString() + ")";

                                ada.SelectCommand.Connection = con;
                                ada.SelectCommand.CommandType = CommandType.Text;

                                int returnValue;
                                returnValue = ada.Fill(dt);
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
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return -1;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return -1;
            }
        }

        #endregion

        #region Get ane Fill CZMST_SI by CountEntries celá tabulka

        internal static Datasets.Vydej GETDATA_CZMSTSI_By_CountEntries_SOPNUMBE(int countEntries, string SOPNUMBE)
        {
            Datasets.Vydej tbl_si = new Datasets.Vydej();

            try
            {
                Database.Vydej.Fill_CZMSTSI_By_CountEntries_SOPNUMBE(tbl_si, countEntries, SOPNUMBE);
                return tbl_si;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }

        internal static int Fill_CZMSTSI_By_CountEntries_SOPNUMBE(Datasets.Vydej dt, int countEntries, string SOPNUMBE)
        {
            try
            {

                Globals_V1.LoadConfiguration();
                try
                {
                    using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {
                        using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                        {
                            using (var com = con.CreateCommand())
                            {
                                ada.SelectCommand = com;

                                ada.SelectCommand.CommandText =
                                    " SELECT * FROM " +
                                    Constants.Common.TABLE_CZMST_SI +
                                    " WHERE (CountEntries = " + countEntries.ToString() + ") AND (SOPNUMBE = '" + SOPNUMBE.Trim() + "') ";
                                    //" WHERE (CountEntries = " + countEntries.ToString() + ")";

                                ada.SelectCommand.Connection = con;
                                ada.SelectCommand.CommandType = CommandType.Text;

                                int returnValue;
                                returnValue = ada.Fill(dt, dt.CZMST_SI.TableName);
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
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return -1;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return -1;
            }
        }

        #endregion


        #region Insert

        internal static int Insert_CZMST_SE(
        SqlTransaction trans,
        SqlConnection conn,
        int CountEntries,
        string SOPNUMBE,
        string ITEMNMBR,
        string ITEMTYPE,
        string ITEMDESC,
        string VNDDOCNM,
        string VNDITNUM,
        int ORD,
        string CZ_CarKod,
        string SKL_ID,
        string LOCNCODE,
        decimal QTYSHPPD,
        decimal QTYPACK,
        byte CZ_DatVyr_Track,
        short CZ_DatVyr_Delka,
        byte CZ_SerNum_Track,
        short CZ_SerNum_Delka,
        byte CZ_SW_Track,
        short CZ_SW_Delka,
        byte CZ_Doslo,
        string Note,
        string TYPEPAL,
        decimal? QTYPAL,
        byte PRIORITY,
        byte? PRINTED,
        int? USERID,
        string MJ,
        byte CZ_REZ1_Track,
        byte CZ_REZ2_Track,
        string ITEMCODE,
        decimal? WEIGHT,
        byte CZ_Expirace_Track,
        DateTime? Realization_Stop,
        DateTime? Realization_Start)
            {
                try
                {

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        using (var command = conn.CreateCommand())
                        {


                            adapter.InsertCommand = command;
                            adapter.InsertCommand.Connection = conn;
                            adapter.InsertCommand.Transaction = trans;

                            adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

                            //command.CommandText = " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_SE + " ON ";

                            command.CommandText += @" INSERT INTO " + Constants.Common.TABLE_CZMST_SE + " (" +
                                "CountEntries, " +
                                "SOPNUMBE, " +
                                "ITEMNMBR, " +
                                "ITEMTYPE, " +
                                "ITEMDESC, " +
                                "VNDDOCNM, " +
                                "VNDITNUM, " +
                                "ORD, " +
                                "CZ_CarKod, " +
                                "SKL_ID, " +
                                "LOCNCODE, " +
                                "MJ, " +
                                "QTYSHPPD, " +
                                "QTYPACK, " +
                                "CZ_DatVyr_Track, " +
                                "CZ_DatVyr_Delka, " +
                                "CZ_SerNum_Track, " +
                                "CZ_SerNum_Delka, " +
                                "CZ_SW_Track, " +
                                "CZ_SW_Delka, " +
                                "CZ_Doslo, " +
                                "Note, " +
                                "TYPEPAL, " +
                                "QTYPAL, " +
                                "PRIORITY, " +
                                "PRINTED, " +
                                "USERID, " +
                                "CZ_REZ1_TRACK, " +
                                "CZ_REZ2_TRACK, " +
                                "ITEMCODE, " +
                                "WEIGHT, " +
                                "Realization_Start, " +
                                "Realization_Stop, " +
                                "CZ_Expirace_Track " +
                                " ) " +
                                " VALUES " +
                                "( " +
                                "@CountEntries, " +
                                "@SOPNUMBE, " +
                                "@ITEMNMBR, " +
                                "@ITEMTYPE, " +
                                "@ITEMDESC, " +
                                "@VNDDOCNM, " +
                                "@VNDITNUM, " +
                                "@ORD, " +
                                "@CZ_CarKod, " +
                                "@SKL_ID, " +
                                "@LOCNCODE, " +
                                "@MJ, " +
                                "@QTYSHPPD, " +
                                "@QTYPACK, " +
                                "@CZ_DatVyr_Track, " +
                                "@CZ_DatVyr_Delka, " +
                                "@CZ_SerNum_Track, " +
                                "@CZ_SerNum_Delka, " +
                                "@CZ_SW_Track, " +
                                "@CZ_SW_Delka, " +
                                "@CZ_Doslo, " +
                                "@Note, " +
                                "@TYPEPAL, " +
                                "@QTYPAL, " +
                                "@PRIORITY, " +
                                "@PRINTED, " +
                                "@USERID, " +
                                "@CZ_REZ1_TRACK, " +
                                "@CZ_REZ2_TRACK, " +
                                "@ITEMCODE, " +
                                "@WEIGHT, " +
                                "@Realization_Start, " +
                                "@Realization_Stop, " +
                                "@CZ_Expirace_Track)";

                            //command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_SE + " OFF ";


                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, Value = ITEMTYPE == null ? (object)DBNull.Value : ITEMTYPE });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = ITEMDESC == null ? (object)DBNull.Value : ITEMDESC });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, Value = VNDDOCNM == null ? (object)DBNull.Value : VNDDOCNM });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, Value = VNDITNUM == null ? (object)DBNull.Value : VNDITNUM });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, Value = CZ_CarKod == null ? (object)DBNull.Value : CZ_CarKod });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = LOCNCODE == null ? (object)DBNull.Value : LOCNCODE });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, Value = QTYSHPPD });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = QTYPACK });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_DatVyr_Track", DbType = System.Data.DbType.Byte, Value = CZ_DatVyr_Track });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_DatVyr_Delka", DbType = System.Data.DbType.Int16, Value = CZ_DatVyr_Delka });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, Value = CZ_SerNum_Track });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, Value = CZ_SerNum_Delka });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SW_Track", DbType = System.Data.DbType.Byte, Value = CZ_SW_Track });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SW_Delka", DbType = System.Data.DbType.Int16, Value = CZ_SW_Delka });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Doslo", DbType = System.Data.DbType.Byte, Value = CZ_Doslo });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@Note", DbType = System.Data.DbType.String, Value = Note == null ? (object)DBNull.Value : Note });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, Value = TYPEPAL == null ? (object)DBNull.Value : TYPEPAL });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPAL", DbType = System.Data.DbType.Decimal, Value = QTYPAL == null ? (object)DBNull.Value : QTYPAL });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@PRIORITY", DbType = System.Data.DbType.Byte, Value = PRIORITY });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Boolean, Value = PRINTED == null ? (object)DBNull.Value : PRINTED });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@USERID", DbType = System.Data.DbType.Int32, Value = USERID == null ? (object)DBNull.Value : USERID });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, Value = MJ == null ? (object)DBNull.Value : MJ });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ1_TRACK", DbType = System.Data.DbType.Byte, Value = CZ_REZ1_Track });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_REZ2_TRACK", DbType = System.Data.DbType.Byte, Value = CZ_REZ2_Track });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, Value = WEIGHT == null ? (object)DBNull.Value : WEIGHT });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@Realization_Start", DbType = System.Data.DbType.DateTime, Value = Realization_Start == null ? (object)DBNull.Value : Realization_Start });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@Realization_Stop", DbType = System.Data.DbType.DateTime, Value = Realization_Stop == null ? (object)DBNull.Value : Realization_Stop });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Expirace_Track", DbType = System.Data.DbType.Byte, Value = CZ_Expirace_Track });

                            int returnValue = adapter.InsertCommand.ExecuteNonQuery();
                            return returnValue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.ExceptionHandler2.Handle(ex);
                    throw ex;
                }

            }

        #endregion

        public static int CZMSTSE_MAX_CountEntries(SqlConnection sqlconnection, SqlTransaction sqltransaction)
        {
            System.Data.SqlClient.SqlCommand sqlcommand = null;
            sqlcommand = new System.Data.SqlClient.SqlCommand();
            sqlcommand.Connection = sqlconnection;
            sqlcommand.CommandType = System.Data.CommandType.Text;
            sqlcommand.CommandText = "SELECT MAX( CountEntries ) FROM " + Constants.Common.TABLE_CZMST_SE;
            sqlcommand.Transaction = sqltransaction;
            object o = sqlcommand.ExecuteScalar();

            try
            {
                int countentries = Convert.ToInt32(o);
                return countentries;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return 0; //pokud nenalezeno ... ???
            }
        }

        public static byte? CZMSTSE_SOPNUMBER_CZDOSLO(string SOPNUMBE)
        {
            try
            {
                using (var conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var comm = conn.CreateCommand())
                    {
                        comm.CommandText = "SELECT DISTINCT CZ_Doslo FROM " + Constants.Common.TABLE_CZMST_SE + " WHERE (SOPNUMBE = '" + SOPNUMBE.Trim() + "') AND (CZ_Doslo < 100)";
                        comm.CommandType = CommandType.Text;

                        conn.Open();
                        var tmp = comm.ExecuteScalar();                     
                        conn.Close();

                        if ((tmp != null) && (tmp is byte?))
                            return (byte?)tmp;
                        else
                            return null;

                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        public static bool CZMSTSE_EXIST_SOPNUMBE(string SOPNUMBE)
        {
            try
            {
                using (var conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var comm = conn.CreateCommand())
                    {
                        comm.CommandText = "SELECT COUNT(*) AS Expr1 FROM " + Constants.Common.TABLE_CZMST_SE + " WHERE (SOPNUMBE = '" + SOPNUMBE.Trim() + "')";
                        comm.CommandType = CommandType.Text;

                        conn.Open();
                        var tmp = comm.ExecuteScalar();
                        conn.Close();

                        if ((tmp != null) && (tmp is int))
                        {
                            if (((int)tmp) > 0)
                                return true;
                            else
                                return false;

                        }
                        else
                            return false;

                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        internal static Datasets.Vydej.SeznamSluzebDataTable GetData_ListSluzeb(int oRD)
        {


            Globals_V1.LoadConfiguration();
            try
            {

                Datasets.Vydej ds = new Datasets.Vydej();

                using (var con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var com = con.CreateCommand())
                    {

                        using (var ada = new SqlDataAdapter())
                        {

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = "Select * from dbo.[FASK_GetListPolozekPoSluzbe](@ORD)";
                            ada.SelectCommand.Parameters.AddWithValue("@ORD", oRD);

                            ada.Fill(ds, ds.SeznamSluzeb.TableName);
                        }

                    }

                }

                return ds.SeznamSluzeb;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }
        #region OLD puvodny dotaz

        //public static Datasets.Vydej.CZMST_SIDataTable GETDATA_CZMSTSI_DS_GroupBy_CountEntries(int countentries)
        //{
        //    Datasets.Vydej.CZMST_SIDataTable tbl_si = new Datasets.Vydej.CZMST_SIDataTable();
        //    try
        //    {


        //        try
        //        {
        //            Globals_V1.LoadConfiguration();

        //            using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
        //            {
        //                using (var com = con.CreateCommand())
        //                {
        //                    //com.CommandText = "SELECT " +
        //                    //    " CountEntries, PONUMBER, ITEMNMBR, ORD, VNDITNUM, CZ_CarKod, SKL_ID, SUM(QTYSHPPD) AS QTYSHPPD, 0 AS QTYPACK, USER_ID, MAX(DEX_ROW_ID) AS DEX_ROW_ID, ID_TERMINAL, MJ, SERLTNUM, Expirace, 0 AS INPUT_MODE, SUM(QTYSHPPDMJ) AS QTYSHPPDMJ, CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID" +
        //                    //    " FROM CZMST_PI " +
        //                    //    " WHERE CountEntries = " + countentries.ToString() +
        //                    //    " GROUP BY CountEntries, PONUMBER, ITEMNMBR, VNDITNUM, CZ_CarKod, SKL_ID, USER_ID, ID_TERMINAL, MJ, ORD, SERLTNUM, Expirace";


        //                    //com.CommandText = @"SELECT " +
        //                    //    " CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDITNUM, CZ_CarKod, SKL_ID, SUM(QTYSHPPD) AS QTYSHPPD, " +
        //                    //    " 0 AS QTYPACK, USER_ID, MAX(DEX_ROW_ID) AS DEX_ROW_ID, ID_TERMINAL, MJ, SERLTNUM, Expirace, " +
        //                    //    " 0 AS INPUT_MODE, CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID, VNDDOCNM " +
        //                    //    " FROM " + Constants.Common.TABLE_CZMST_SI + " WHERE (CountEntries = " + countentries.ToString() + ") " +
        //                    //    " GROUP BY CountEntries, SOPNUMBE, ITEMNMBR, VNDITNUM, CZ_CarKod, SKL_ID, USER_ID, ID_TERMINAL, MJ, ORD, VNDDOCNM, SERLTNUM, Expirace";

        //                    //TaD Prasacka verze vez grupovana SN  a expirace, je to rychrooprava pro I-Tec
        //                    // Je potřeba nejak udelat aby se data grupovala podle toho
        //                    // že je položka sledovana na šarži v IS POHODA
        //                    // anebo že je sledovana našima parametrama na šarži/SN
        //                    // a pokud je sledovana našima, tak či sa chce zapisovat do IS POHOD
        //                    // anebo nechce zapisovat do IS POHODA, třeba do poznamky
        //                    // Tady to už vidim na nejakou funkci anebo proceduru na SQL serveru, ktera to bude rozhodivat
        //                    com.CommandText = @"SELECT " +
        //                         " CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDITNUM, CZ_CarKod, SKL_ID, SUM(QTYSHPPD) AS QTYSHPPD, " +
        //                         " 0 AS QTYPACK, USER_ID, MAX(DEX_ROW_ID) AS DEX_ROW_ID, ID_TERMINAL, MJ, " +
        //                         " 0 AS INPUT_MODE, CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID, VNDDOCNM " +
        //                         " FROM " + Constants.Common.TABLE_CZMST_SI + " WHERE (CountEntries = " + countentries.ToString() + ") " +
        //                         " GROUP BY CountEntries, SOPNUMBE, ITEMNMBR, VNDITNUM, CZ_CarKod, SKL_ID, USER_ID, ID_TERMINAL, MJ, ORD, VNDDOCNM";


        //                    com.CommandType = System.Data.CommandType.Text;

        //                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
        //                    {
        //                        ada.SelectCommand = com;
        //                        ada.Fill(tbl_si);

        //                    }
        //                }
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            Fask.Logging.ExceptionHandler2.Handle(ex);
        //        }


        //        return tbl_si;
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(sqlex);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //        return null;
        //    }
        //    finally
        //    {
        //    }
        //}

        #endregion
        public static Datasets.Vydej.CZMST_SIDataTable GETDATA_CZMSTSI_DS_GroupBy_CountEntries(int countentries)
        {
            Datasets.Vydej.CZMST_SIDataTable tbl_si = new Datasets.Vydej.CZMST_SIDataTable();
            try
            {


                try
                {
                    Globals_V1.LoadConfiguration();

                    using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {
                        using (var com = con.CreateCommand())
                        {
                            com.CommandText = @"SELECT * FROM [FASK_GetGroupFromSI]('" + countentries.ToString() + "')";
                            com.CommandType = System.Data.CommandType.Text;

                            using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                            {
                                ada.SelectCommand = com;
                                ada.Fill(tbl_si);

                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                }


                return tbl_si;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }


        internal static Datasets.Vydej GETDATA_CZMSTSI_GroupBy_CountEntries_SOPNUMBE(int countEntries, string SOPNUMBE)
        {
            Datasets.Vydej ds = new Datasets.Vydej();
            try
            {
                Database.Vydej.Fill_CZMSTSI_GroupBy_CountEntries_SOPNUMBE(ds, countEntries, SOPNUMBE);
                return ds;
            }
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return null;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
            }
        }

        internal static int Fill_CZMSTSI_GroupBy_CountEntries_SOPNUMBE(Datasets.Vydej ds, int countentries, string SOPNUMBE)
        {
            try
            {

                Globals_V1.LoadConfiguration();
                try
                {
                    using (var con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {
                        using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                        {
                            using (var com = con.CreateCommand())
                            {
                                ada.SelectCommand = com;

                                ada.SelectCommand.CommandText = @"SELECT " +
                                    " CountEntries, SOPNUMBE, ITEMNMBR, ORD, VNDITNUM, CZ_CarKod, SKL_ID," +
                                    " SUM(QTYSHPPD) AS QTYSHPPD, SUM(QTYPACK) AS QTYPACK, USER_ID," +
                                    " MAX(DEX_ROW_ID) AS DEX_ROW_ID, ID_TERMINAL, MJ, '' AS SERLTNUM, " +
                                    " 0 AS INPUT_MODE, CAST(MIN(CAST(GUID AS BINARY(16))) AS UNIQUEIDENTIFIER) AS GUID, " +
                                    " VNDDOCNM " +
                                    " FROM " + Constants.Common.TABLE_CZMST_SI +
                                    " WHERE (CountEntries = " + countentries.ToString() + ") AND (SOPNUMBE = '" + SOPNUMBE.Trim() + "') " +
                                    " GROUP BY CountEntries, SOPNUMBE, ITEMNMBR, VNDITNUM, CZ_CarKod, " +
                                    " SKL_ID, USER_ID, ID_TERMINAL, MJ, ORD, VNDDOCNM";

                                ada.SelectCommand.Connection = con;
                                ada.SelectCommand.CommandType = CommandType.Text;

                                int returnValue;
                                returnValue = ada.Fill(ds, ds.CZMST_SI.TableName);
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
            catch (SqlException sqlex)
            {
                Fask.Logging.ExceptionHandler2.Handle(sqlex);
                return -1;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return -1;
            }
        }

        public static bool CZMSTSE_UPDATE_CZDOSLO(string SOPNUMBE)
        {

            int? result = 0;
            try
            {
                using (SqlDataAdapter ada = new SqlDataAdapter())
                {
                    using (SqlConnection conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {
                        using (ada.UpdateCommand = conn.CreateCommand())
                        {
                            ada.UpdateCommand.CommandType = System.Data.CommandType.Text;
                            ada.UpdateCommand.CommandText = "UPDATE CZMST_SE SET CZ_Doslo = 201 WHERE (SOPNUMBE = @SOPNUMBE) AND (CZ_Doslo = 0)";

                            ada.UpdateCommand.Parameters.AddWithValue("@SOPNUMBE", SOPNUMBE.Trim());
                            ada.UpdateCommand.Connection.Open();
                            result = ada.UpdateCommand.ExecuteNonQuery();

                            if ((result != null) && (result is int))
                            {
                                if (((int)result) > 0)
                                    return true;
                                else
                                    return false;

                            }
                            else
                                return false;

                        } 
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }


        public static bool CZMSTSE_UPDATE_CZDOSLO_By_CountEntries( byte CZ_Doslo, int CountEntries)
        {

            int? result = 0;
            try
            {
                using (SqlDataAdapter ada = new SqlDataAdapter())
                {
                    using (SqlConnection conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {
                        using (ada.UpdateCommand = conn.CreateCommand())
                        {
                            ada.UpdateCommand.CommandType = System.Data.CommandType.Text;
                            
                            ada.UpdateCommand.CommandText = "UPDATE CZMST_SE SET CZ_Doslo = @CZ_Doslo WHERE (CountEntries = @CountEntries)";

                            ada.UpdateCommand.Parameters.AddWithValue("@CZ_Doslo", CZ_Doslo);
                            ada.UpdateCommand.Parameters.AddWithValue("@CountEntries", CountEntries);
                            ada.UpdateCommand.Connection.Open();
                            result = ada.UpdateCommand.ExecuteNonQuery();

                            if ((result != null) && (result is int))
                            {
                                if (((int)result) > 0)
                                    return true;
                                else
                                    return false;

                            }
                            else
                                return false;

                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }


        #endregion

    }
}
