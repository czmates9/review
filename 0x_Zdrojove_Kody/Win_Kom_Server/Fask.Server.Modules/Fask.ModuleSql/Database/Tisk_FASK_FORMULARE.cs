using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.ModuleSql.Database
{
    public class Tisk_FASK_FORMULARE
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


                        InitializeCommandInsert_FASK_FORMULARE(commandInsert);
                        InitializeCommandUpdate_FASK_FORMULARE(commandUpdate);
                        InitializeCommandDelete_FASK_FORMULARE(commandDelete);
                        InitializeCommandSelect_FASK_FORMULARE(commandSelect);

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

        private static void InitializeCommandInsert_FASK_FORMULARE(SqlCommand command)
        {
            command.CommandText =
                @" INSERT INTO " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH +
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

        private static void InitializeCommandUpdate_FASK_FORMULARE(SqlCommand command)
        {
            command.CommandText =
               @" UPDATE " + Fask.SQL.Constants.Common.TABLE_FASK_FORMULARE +
               " SET nazev_okna = @nazev_okna," +
               " nazev = @nazev," +
               " ord = @ord," +
               " typ = @typ," +
               " loginid = @loginid," +
               " machineid = @machineid," +
               " formular = @formular" +
               " WHERE(nazev_okna = @Original_nazev_okna)" +
               " AND (id = @Original_id)";


            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@nazev_okna", DbType = DbType.String, SourceColumn = "nazev_okna", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@nazev", DbType = DbType.String, SourceColumn = "nazev", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@ord", DbType = DbType.Int32, SourceColumn = "ord", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@typ", DbType = DbType.String, SourceColumn = "typ", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@loginid", DbType = DbType.String, SourceColumn = "loginid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@formular", DbType = DbType.String, SourceColumn = "formular", SourceVersion = DataRowVersion.Current });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@Original_nazev_okna", DbType = DbType.String, SourceColumn = "nazev_okna", SourceVersion = DataRowVersion.Original });
            command.Parameters.Add(new SqlParameter()
            { ParameterName = "@Original_id", DbType = DbType.Int32, SourceColumn = "id", SourceVersion = DataRowVersion.Original });

        }

        private static void InitializeCommandDelete_FASK_FORMULARE(SqlCommand command)
        {
            command.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH + " WHERE (CountEntries = @Original_CountEntries) AND (SOPNUMBE = @Original_SOPNUMBE)";


            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_CountEntries",
                DbType = DbType.Int32,
                SourceColumn = "CountEntries",
                SourceVersion = DataRowVersion.Original

            });

            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Original_SOPNUMBE",
                DbType = DbType.String,
                SourceColumn = "SOPNUMBE",
                SourceVersion = DataRowVersion.Original

            });
        }

        private static void InitializeCommandSelect_FASK_FORMULARE(SqlCommand command)
        {
            command.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPH;
        }


        #endregion

        public static int FASK_FORMULARE_Insert(string CS, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow row)
        {
            int returnValue = 0;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(CS);
                conn.Open(); // Otevření spojení.

                using (var command = conn.CreateCommand())
                {
                    // SQL příkaz pro vložení dat.
                    command.CommandText = @"
                INSERT INTO " + Fask.SQL.Constants.Common.TABLE_FASK_FORMULARE + @"
                (nazev_okna, nazev, ord, typ, loginid, machineid, formular)
                VALUES (@nazev_okna, @nazev, @ord, @typ, @loginid, @machineid, @formular)";

                    // Parametry.
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@nazev_okna", DbType = DbType.String, SourceColumn = "nazev_okna", Value = row.Isnazev_oknaNull() ? (object)DBNull.Value : row.nazev_okna });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@nazev", DbType = DbType.String, SourceColumn = "nazev", Value = row.IsnazevNull() ? (object)DBNull.Value : row.nazev });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@ord", DbType = DbType.Int32, SourceColumn = "nazev_okna", Value = row.ord });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@typ", DbType = DbType.String, SourceColumn = "typ", Value = row.IstypNull() ? (object)DBNull.Value : row.typ });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@loginid", DbType = DbType.String, SourceColumn = "loginid", Value = row.IsloginidNull() ? (object)DBNull.Value : row.loginid });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", Value = row.IsmachineidNull() ? (object)DBNull.Value : row.machineid });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@formular", DbType = DbType.String, SourceColumn = "formular", Value = row.IsformularNull() ? (object)DBNull.Value : row.formular });


                    // Vykonání příkazu.
                    returnValue = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Zde byste měli zpracovat výjimku (např. logováním).
                Fask.Logging.ExceptionHandler2.Handle(ex);
                // MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close(); // Uzavření spojení v bloku finally.
                    conn.Dispose(); // Zajištění úplného uvolnění zdrojů.
                }
            }

            return returnValue;
        }



        public static int FASK_FORMULARE_Delete(string CS, Fask.Interfaces.DataSets.Vyroba.FASK_FORMULARERow row)
        {
            int returnValue = 0;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(CS);
                conn.Open(); // Otevření spojení.

                using (var command = conn.CreateCommand())
                {
                    // SQL příkaz pro vložení dat.

                    command.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_FASK_FORMULARE +
                    " WHERE (id = @id)";


                    // Parametry.
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@id", DbType = DbType.Int32, SourceColumn = "id", Value = row.id });

                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@nazev_okna", DbType = DbType.String, SourceColumn = "nazev_okna", Value = row.Isnazev_oknaNull() ? (object)DBNull.Value : row.nazev_okna });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@nazev", DbType = DbType.String, SourceColumn = "nazev", Value = row.IsnazevNull() ? (object)DBNull.Value : row.nazev });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@ord", DbType = DbType.Int32, SourceColumn = "nazev_okna", Value = row.ord });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@typ", DbType = DbType.String, SourceColumn = "typ", Value = row.IstypNull() ? (object)DBNull.Value : row.typ });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@loginid", DbType = DbType.String, SourceColumn = "loginid", Value = row.IsloginidNull() ? (object)DBNull.Value : row.loginid });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", Value = row.IsmachineidNull() ? (object)DBNull.Value : row.machineid });
                    command.Parameters.Add(new SqlParameter()
                    { ParameterName = "@formular", DbType = DbType.String, SourceColumn = "formular", Value = row.IsformularNull() ? (object)DBNull.Value : row.formular });


                    // Vykonání příkazu.
                    returnValue = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Zde byste měli zpracovat výjimku (např. logováním).
                Fask.Logging.ExceptionHandler2.Handle(ex);
                // MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close(); // Uzavření spojení v bloku finally.
                    conn.Dispose(); // Zajištění úplného uvolnění zdrojů.
                }
            }

            return returnValue;
        }





    }
}
