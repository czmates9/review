using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Fask.MST_W.SqlCEDBs.Controllers
{
    /// <summary>
    /// Base trida pro controllery
    /// </summary>
    public class SQLite_Controller_1_Example : SQLite_Controller
    {
        #region c'tors
        public SQLite_Controller_1_Example()
            : base("filename")
            //: base(Main.ciselnikExample)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="sqliteconnection">Spojeni na databazi davky prijmu</param>
        public SQLite_Controller_1_Example(SQLiteConnection sqliteconnection) : base(sqliteconnection)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="sqliteConnectionstring">Conectionstring na databazi davky prijmu</param>
        public SQLite_Controller_1_Example(string sqliteFilename)
            : base(sqliteFilename)
        {
        }

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();
		//}

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

        #region Metody z TableAdpateru ukazkove

        private void InitializeCommandInsert(SQLiteCommand command)
        {
            command.CommandText =
                "INSERT INTO CZMST094" +
                " (SKL_ID, LOCNCODE, TYPE, IS_RECEIVE, DEX_ROW_ID, Description, Barcode)" +
                " VALUES " +
                " (@SKL_ID,@LOCNCODE,@TYPE,@IS_RECEIVE,@DEX_ROW_ID,@Description,@Barcode)";
            global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@SKL_ID";
            param.SourceColumn = "SKL_ID";
            command.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@LOCNCODE";
            param.SourceColumn = "LOCNCODE";
            command.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@TYPE";
            param.SourceColumn = "TYPE";
            command.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@IS_RECEIVE";
            param.DbType = global::System.Data.DbType.Boolean;
            param.SourceColumn = "IS_RECEIVE";
            command.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@DEX_ROW_ID";
            param.DbType = global::System.Data.DbType.Int32;
            param.SourceColumn = "DEX_ROW_ID";
            command.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Description";
            param.SourceColumn = "Description";
            command.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Barcode";
            param.SourceColumn = "Barcode";
            command.Parameters.Add(param);
        }
        private void InitializeCommandUpdate(SQLiteCommand command)
        {
            command.CommandText =
                "Update CZMST094" +
                " Set SKL_ID=@SKL_ID" +
                " Where Guid=@Guid";
            global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@SKL_ID";
            param.SourceColumn = "SKL_ID";
            command.Parameters.Add(param);
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Guid";
            param.SourceColumn = "Guid";
            command.Parameters.Add(param);
        }
        private void InitializeCommandDelete(SQLiteCommand command)
        {
            command.CommandText =
                "Delete from CZMST094" +
                " Where Guid=@Guid";
            global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Guid";
            param.SourceColumn = "Guid";
            command.Parameters.Add(param);
        }
        private void InitializeCommandSelect(SQLiteCommand command)
        {
            command.CommandText =
                "Select * from CZMST094" +
                " Where Guid=@Guid";
            global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
            param = new global::System.Data.SQLite.SQLiteParameter();
            param.ParameterName = "@Guid";
            param.SourceColumn = "Guid";
            command.Parameters.Add(param);
        }

        public int Update(object data)
        {
            SQLiteTransaction transaction = null;
            try
            {
                int result = 0;
                Connection_Open();

                transaction = this.Connection.BeginTransaction();

                using (var commandInsert = this.Connection.CreateCommand())
                using (var commandUpdate = this.Connection.CreateCommand())
                using (var commandDelete = this.Connection.CreateCommand())
                using (var commandSelect = this.Connection.CreateCommand())
                {
                    InitializeCommandInsert(commandInsert);
                    InitializeCommandUpdate(commandUpdate);
                    InitializeCommandDelete(commandDelete);
                    InitializeCommandSelect(commandSelect);

                    using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
                    {
                        adapter.InsertCommand = commandInsert;
                        adapter.UpdateCommand = commandUpdate;
                        adapter.DeleteCommand = commandDelete;
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
                return result;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);

                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch (Exception exTransaction)
                {
                    Logging.Log.Write(exTransaction);
                }

                throw ex;
            }
            finally
            {
                Connection_Close();
            }
        }

        public int Fill(System.Data.DataTable dataTable)
        {
            try
            {
                Connection_Open();
                using (var command = this.Connection.CreateCommand())
                {
                    command.CommandText =
                        "SELECT * FROM CZMST094";
                    using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        int returnValue = adapter.Fill(dataTable);
                        return returnValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                throw ex;
            }
            finally
            {
                Connection_Close();
            }
        }
        public Fask.MST_W.SqlCEDBs.DataSets.Lokace.CZMST094DataTable GetData()
        {
            var dataTable = new Fask.MST_W.SqlCEDBs.DataSets.Lokace.CZMST094DataTable();
            this.Fill(dataTable);
            return dataTable;
        }

        public object GetDesc(string LOCNCODE, string SKL_ID)
        {
            try
            {
                Connection_Open();

                using (var command = this.Connection.CreateCommand())
                {
                    command.CommandText =
                        "SELECT Description FROM CZMST094 WHERE (LOCNCODE = @LOCNCODE) AND (SKL_ID = @SKL_ID)";
                    command.Parameters.AddWithValue("@LOCNCODE", LOCNCODE);
                    command.Parameters.AddWithValue("@SKL_ID", SKL_ID);
                    object o = command.ExecuteScalar();
                    return o;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                throw ex;
            }
            finally
            {
                Connection_Close();
            }
        }

        #endregion

    }
}
