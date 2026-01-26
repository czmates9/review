using Fask.Interfaces.DataSets_Import;
using Fask.Interfaces.Filtry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using Fask.ModulePohodaXML.Classes;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider : Fask.Interfaces.ImportnyMustky.IImportnyMustky,
        Fask.Interfaces.ImportnyMustky.IImportnyMustky_GetImportPohoda_SKzNC,
        Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC,
        Fask.Interfaces.ImportnyMustky.IImportnyMustky_UpdateImportPohoda_SKzNC,
        Fask.Interfaces.ImportnyMustky.IImportnyMustky_Delete_ImportSKzNC,
        Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_AddRefAg,
           Fask.Interfaces.ImportnyMustky.IImportnyMustky_ImportPohoda_SKzNC_EXIST
    {
        public ImportPOHODA_FromExcel GetImportPohoda_SKzNC(Import_SKzNC_ListFiltr filtry)
        {
            Globals_V1.LoadConfiguration();

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;

            ImportPOHODA_FromExcel ds = new ImportPOHODA_FromExcel();
            try
            {
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

                command.CommandText =
                    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_IMPORT_POHODA_SKzNC +
                    " WHERE" +
                    " 1=1 ";



                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(ds, ds.FASK_ZASOBY_IMPORT_POHODA_SKzNC.TableName);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ImportPohoda_SKzNC(ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCDataTable dt)
        {
            try
            {
                string filename;
                filename = XML.PohodaComunication.FilenameCompose(Fask.ModulePohodaXML.XML.PohodaComunication.import_dodavatel_zasoby);

                string pom = string.Empty;

                pom = Globals_V1.LoadConfiguration();

                if (pom != "OK")
                    throw new Exception("Nezdařilo se načteni konfigurace.");

                if (!Importni_Mustek.CreateRequest_Import_DodavateleZasob_XML(dt, filename))
                    throw new Exception("Nepodaril se create requestu.");

                string respfilename;
                if (!XML.PohodaComunication.Communicate(filename, out respfilename))
                {
                    throw new Exception("Komunikace s Pohodou se nezdařila");
                }

                pom = string.Empty;
                pom = Importni_Mustek.LoadResponse_Import_DodavateleZasob_XML(dt, respfilename);

                if (pom != "OK")
                    return false;


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete_ImportSKzNC(int DEX_ROW_ID)
        {


            System.Data.SqlClient.SqlTransaction transaction = null;
            SqlConnection connection = null;
            try
            {
                Globals_V1.LoadConfiguration();

                using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    connection.Open();

                    transaction = connection.BeginTransaction();

                    using (var commandDelete = connection.CreateCommand())
                    {

                        commandDelete.Transaction = transaction;
                        
                        commandDelete.CommandText = "DELETE FROM " +
                            Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_IMPORT_POHODA_SKzNC +
                            " WHERE (DEX_ROW_ID = @DEX_ROW_ID) ";

                        commandDelete.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@DEX_ROW_ID",
                            DbType = System.Data.DbType.Int32,
                            Value = DEX_ROW_ID
                        });

                        int retunValue;
                        retunValue = commandDelete.ExecuteNonQuery();

                        transaction.Commit();

                        return retunValue;

                    }
                }
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

        public int UpdateImportPohoda_SKzNC(object data)
        {
            System.Data.SqlClient.SqlTransaction transaction = null;
            SqlConnection connection = null;
            try
            {
                Globals_V1.LoadConfiguration();

                int result = 0;
                using (connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
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

                        InitializeCommandInsert(commandInsert);
                        InitializeCommandUpdate(commandUpdate);
                        InitializeCommandDelete(commandDelete);
                        InitializeCommandSelect(commandSelect);

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

        public static void InitializeCommandInsert(SqlCommand command)
        {

            command.CommandText = @"INSERT INTO " +
                Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_IMPORT_POHODA_SKzNC +
                " ( " +
                " [DefDod], [RefAg], [IDS_SKz], [ID_sSklad], [RefAD], " +
                " [Firma], [NakupC], [RefCM], [CmKurs], [EAN], " +
                " [MJEAN], [MJkoefEAN], [Pozn], [Status_Err]" +
                ") VALUES( " +
                " @DefDod, @RefAg, @IDS_SKz, @ID_sSklad, @RefAD, " +
                " @Firma, @NakupC, @RefCM, @CmKurs, @EAN, " +
                " @MJEAN, @MJkoefEAN, @Pozn, @Status_Err" +
                ")";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@DefDod", DbType = DbType.Boolean, SourceColumn = "DefDod", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@RefAg", DbType = DbType.Int32, SourceColumn = "RefAg", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IDS_SKz", DbType = DbType.String, SourceColumn = "IDS_SKz", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ID_sSklad", DbType = DbType.Int32, SourceColumn = "ID_sSklad", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@RefAD", DbType = DbType.Int32, SourceColumn = "RefAD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Firma", DbType = DbType.String, SourceColumn = "Firma", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@NakupC", DbType = DbType.Decimal, SourceColumn = "NakupC", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@RefCM", DbType = DbType.Int32, SourceColumn = "RefCM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CmKurs", DbType = DbType.Decimal, SourceColumn = "CmKurs", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@EAN", DbType = DbType.String, SourceColumn = "EAN", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@MJEAN", DbType = DbType.String, SourceColumn = "MJEAN", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@MJkoefEAN", DbType = DbType.Decimal, SourceColumn = "MJkoefEAN", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Pozn", DbType = DbType.String, SourceColumn = "Pozn", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Status_Err", DbType = DbType.Int32, SourceColumn = "Status_Err", SourceVersion = DataRowVersion.Current });

        }

        public static void InitializeCommandUpdate(SqlCommand command)
        {

            command.CommandText = "UPDATE " + 
                Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_IMPORT_POHODA_SKzNC +
                " SET "+
                " [DefDod] = @DefDod, " +
                " [RefAg] = @RefAg, " +
                " [IDS_SKz] = @IDS_SKz, " +
                " [ID_sSklad] = @ID_sSklad, " +
                " [RefAD] = @RefAD, " +
                " [Firma] = @Firma, " +
                " [NakupC] = @NakupC, " +
                " [RefCM] = @RefCM, " +
                " [CmKurs] = @CmKurs, " +
                " [EAN] = @EAN,  " +
                " [MJEAN] = @MJEAN,  " +
                " [MJkoefEAN] = @MJkoefEAN, " +
                " [Pozn] = @Pozn, " +
                " [Status_Err] = @Status_Err " +
                " WHERE (DEX_ROW_ID = @Original_DEX_ROW_ID) ";


            command.Parameters.Add(new SqlParameter() { ParameterName = "@DefDod", DbType = DbType.Boolean, SourceColumn = "DefDod", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@RefAg", DbType = DbType.Int32, SourceColumn = "RefAg", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@IDS_SKz", DbType = DbType.String, SourceColumn = "IDS_SKz", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ID_sSklad", DbType = DbType.Int32, SourceColumn = "ID_sSklad", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@RefAD", DbType = DbType.Int32, SourceColumn = "RefAD", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Firma", DbType = DbType.String, SourceColumn = "Firma", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@NakupC", DbType = DbType.Decimal, SourceColumn = "NakupC", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@RefCM", DbType = DbType.Int32, SourceColumn = "RefCM", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@CmKurs", DbType = DbType.Decimal, SourceColumn = "CmKurs", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@EAN", DbType = DbType.String, SourceColumn = "EAN", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@MJEAN", DbType = DbType.String, SourceColumn = "MJEAN", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@MJkoefEAN", DbType = DbType.Decimal, SourceColumn = "MJkoefEAN", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Pozn", DbType = DbType.String, SourceColumn = "Pozn", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Status_Err", DbType = DbType.Int32, SourceColumn = "Status_Err", SourceVersion = DataRowVersion.Current });

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_DEX_ROW_ID",
                DbType = System.Data.DbType.Int32,
                SourceColumn = "DEX_ROW_ID",
                SourceVersion = System.Data.DataRowVersion.Original
            });
        }

        public static void InitializeCommandDelete(SqlCommand command)
        {
            command.CommandText = "DELETE FROM " + 
                Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_IMPORT_POHODA_SKzNC +
                " WHERE (DEX_ROW_ID = @Original_DEX_ROW_ID) ";

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_DEX_ROW_ID",
                DbType = System.Data.DbType.Int32,
                SourceColumn = "DEX_ROW_ID",
                SourceVersion = System.Data.DataRowVersion.Original
            });

        }

        public static void InitializeCommandSelect(SqlCommand command)
        {
            command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_IMPORT_POHODA_SKzNC;
        }

        #endregion

        public int? AddRefAg(ImportPOHODA_FromExcel.FASK_ZASOBY_IMPORT_POHODA_SKzNCRow row)
        {
            try
            {
                    if (row.IsIDS_SKzNull() || row.IsID_sSkladNull())
                        return null;

                    return Database.Pohoda.SKz_Get_ID_byIDS_sSklad(row.IDS_SKz, row.ID_sSklad);
            
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        public bool EXIST(string ColumnName, string TableName, object value)
        {
            try
            {

                System.Data.OleDb.OleDbType type = System.Data.OleDb.OleDbType.VarChar;

                if (value is string)
                    type = System.Data.OleDb.OleDbType.VarChar;
                else if (value is int)
                    type = System.Data.OleDb.OleDbType.Integer;


                System.Data.OleDb.OleDbCommand SQLCommand = new System.Data.OleDb.OleDbCommand();
                SQLCommand.Connection = new System.Data.OleDb.OleDbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].PohodaDB);
                SQLCommand.CommandType = System.Data.CommandType.Text;

                SQLCommand.CommandText = "SELECT ID FROM " + TableName + " WHERE (" + ColumnName + " = ?) ";
                SQLCommand.Parameters.Add("?", type).Value = value;
                

                SQLCommand.Connection.Open();

                object result = SQLCommand.ExecuteScalar();

                SQLCommand.Connection.Close();

                if (result != null)
                {
                    if (result is int)
                        return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle("Pohoda EXIST SQL Dotazu", " EXIST", ex);
                return false;
            }
        }
    }
}
