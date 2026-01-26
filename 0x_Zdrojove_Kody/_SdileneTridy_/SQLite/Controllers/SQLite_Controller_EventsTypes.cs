using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controller pro ciselnik lokaci : CZMST094
    /// </summary>
    public class SQLite_Controller_EventsTypes : SQLite_Controller
    {
		//private Fask.SQLiteDBs.DataSets.EventsTypesTableAdapters.CZMST_EventsTypesTableAdapter ta_eventstypes = null;
		//internal Fask.SQLiteDBs.DataSets.EventsTypesTableAdapters.CZMST_EventsTypesTableAdapter Ta_eventstypes
		//{
		//    get
		//    {
		//        if (ta_eventstypes == null)
		//        {
		//            ta_eventstypes = new Fask.SQLiteDBs.DataSets.EventsTypesTableAdapters.CZMST_EventsTypesTableAdapter();
		//            ta_eventstypes.Connection = this.Connection;
		//        }
		//        return ta_eventstypes;
		//    }
		//}


        #region c'tors
        public SQLite_Controller_EventsTypes(string sqliteFileName)
            : base(sqliteFileName)
        {
        }

        public SQLite_Controller_EventsTypes(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();

		//    ta_eventstypes.Connection = this.Connection;
		//}

        public override void Dispose()
        {
			//if (ta_eventstypes != null)
			//    ta_eventstypes.Dispose();
			//this.DisposeObject(ta_eventstypes);

            //ta_eventstypes = null;

            base.Dispose();
        }

        #endregion

        #region adapter methods

        //public void allmethods()
        //{
        //    DataSets.EventsTypesTableAdapters.CZMST_EventsTypesTableAdapter ta_e = new Fask.SQLiteDBs.DataSets.EventsTypesTableAdapters.CZMST_EventsTypesTableAdapter();
        //    ta_e.Fill( new Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesDataTable());
        //    ta_e.GetData();
        //    ta_e.Insert(string.Empty, string.Empty, string.Empty, string.Empty);
            
        //    ta_e.Update(new Fask.SQLiteDBs.DataSets.EventsTypes());
        //    ta_e.Update(new Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesDataTable());
        //    ta_e.Update(new Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesDataTable().NewCZMST_EventsTypesRow());
        //    ta_e.Update(new System.Data.DataRow[] { });
        //}

        //#region Update
        //public void InitializeCommandInsert(SQLiteCommand command)
        //{
        //    command.CommandText = 
        //        "INSERT INTO CZMST_EventsTypes" +
        //        " ([eid], [etype], [edesc], [ebarcode]) " + " VALUES " +
        //        " (@p1, @p2, @p3, @p4)";
            
        //    global::System.Data.SQLite.SQLiteParameter param;
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@p1";
        //    param.SourceColumn = "eid";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@p2";
        //    param.SourceColumn = "etype";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@p3";
        //    param.SourceColumn = "edesc";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@p4";
        //    param.SourceColumn = "ebarcode";
        //    command.Parameters.Add(param);
        //}
        //public void InitializeCommandSelect(SQLiteCommand command)
        //{
        //    command.CommandText = "Select * from CZMST_EventsTypes";
        //}

        //public int Update(object data)
        //{
        //    SQLiteTransaction transaction = null;
        //    try
        //    {
        //        int result = 0;
        //        Connection_Open();

        //        transaction = this.Connection.BeginTransaction();

        //        using (var commandInsert = this.Connection.CreateCommand())
        //        //using (var commandUpdate = this.Connection.CreateCommand())
        //        //using (var commandDelete = this.Connection.CreateCommand())
        //        using (var commandSelect = this.Connection.CreateCommand())
        //        {
        //            InitializeCommandInsert(commandInsert);
        //            //InitializeCommandUpdate(commandUpdate);
        //            //InitializeCommandDelete(commandDelete);
        //            InitializeCommandSelect(commandSelect);

        //            using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
        //            {
        //                adapter.InsertCommand = commandInsert;
        //                //adapter.UpdateCommand = commandUpdate;
        //                //adapter.DeleteCommand = commandDelete;
        //                adapter.SelectCommand = commandSelect;

        //                var dataIsDataSet = data as System.Data.DataSet;
        //                var dataIsDataTable = data as System.Data.DataTable;
        //                var dataIsDataRow = data as System.Data.DataRow;
        //                var dataIsDataRowArray = data as System.Data.DataRow[];

        //                //if (data is System.Data.DataSet)
        //                if (dataIsDataSet != null)
        //                    result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
        //                else if (dataIsDataTable != null)
        //                    result = adapter.Update(dataIsDataTable);
        //                else if (dataIsDataRow != null)
        //                    result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
        //                else if (dataIsDataRowArray != null)
        //                    result = adapter.Update(dataIsDataRowArray);
        //                else
        //                    throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
        //            }
        //        }

        //        transaction.Commit();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex);

        //        try
        //        {
        //            if (transaction != null)
        //                transaction.Rollback();
        //        }
        //        catch (Exception exTransaction)
        //        {
        //            Logging.Log.Write(exTransaction);
        //        }

        //        throw ex;
        //    }
        //    finally
        //    {
        //        Connection_Close();
        //    }
        //}

        //#endregion

        //#region methods
        //public int CZMST_EventsTypes_Fill(Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesDataTable dataTable) {
        //    try
        //    {
        //        Connection_Open();
        //        using (var command = this.Connection.CreateCommand())
        //        {
        //            command.CommandText = 
        //                "SELECT * FROM CZMST_EventsTypes";
        //            using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
        //            {
        //                adapter.SelectCommand = command;
        //                int returnValue = adapter.Fill(dataTable);
        //                return returnValue;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        Connection_Close();
        //    }
        //}

        //public Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesDataTable CZMST_EventsTypes_GetData() {
        //    var dataTable = new Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesDataTable();
        //    this.CZMST_EventsTypes_Fill(dataTable);
        //    return dataTable;
        //}

        //#endregion

        #endregion

		#region Zrevidovane

		public int CZMST_EventsTypes_Fill(Fask.SQLiteDBs.DataSets.EventsTypes.CZMST_EventsTypesDataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_EventsTypes";

						int returnValue = adapter.Fill(dataTable);
						return returnValue;
					}
				}
			}
			catch (Exception ex)
			{
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
			}
			finally
			{
				Connection_Close();
			}
		}


		#region Update

		public int Update(object data)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				//using (var commandUpdate = this.Connection.CreateCommand())
				//using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert(commandInsert);
					//InitializeCommandUpdate(commandUpdate);
					//InitializeCommandDelete(commandDelete);
					InitializeCommandSelect(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						//adapter.DeleteCommand = commandDelete;
						adapter.InsertCommand = commandInsert;
						//adapter.UpdateCommand = commandUpdate;
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
				Logging.ExceptionHandler2.Handle(ex);

				try
				{
					if (transaction != null)
						transaction.Rollback();
				}
				catch (Exception exTransaction)
				{
					Logging.ExceptionHandler2.Handle(exTransaction);
				}

				throw ex;
			}
			finally
			{
				Connection_Close();
			}
		}

		#region Inicialize metody

		public void InitializeCommandInsert(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [CZMST_EventsTypes] ([eid], [etype], [edesc], [ebarcode]" + 
                ") VALUES (" +
                "@eid, @etype, @edesc, @ebarcode)";

            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@eid", DbType = System.Data.DbType.String, SourceColumn = "eid" });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@etype", DbType = System.Data.DbType.String, SourceColumn = "etype" });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@edesc", DbType = System.Data.DbType.String, SourceColumn = "edesc" });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ebarcode", DbType = System.Data.DbType.String, SourceColumn = "ebarcode" });

		}

		public void InitializeCommandSelect(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_EventsTypes";
		}


		#endregion

		#endregion


		#endregion


	}
}
