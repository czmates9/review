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
    public class SQLite_Controller_EventsUser : SQLite_Controller
    {
		//private Fask.SQLiteDBs.DataSets.EventsUserTableAdapters.CZMST_EventsUserTableAdapter ta_eventsuser = null;
		//internal Fask.SQLiteDBs.DataSets.EventsUserTableAdapters.CZMST_EventsUserTableAdapter Ta_eventsuser
		//{
		//    get
		//    {
		//        if (ta_eventsuser == null)
		//        {
		//            ta_eventsuser = new Fask.SQLiteDBs.DataSets.EventsUserTableAdapters.CZMST_EventsUserTableAdapter();
		//            ta_eventsuser.Connection = this.Connection;
		//        }
		//        return ta_eventsuser;
		//    }
		//}

        #region c'tors
        public SQLite_Controller_EventsUser(string sqliteFileName)
            : base(sqliteFileName)
        {
        }

        public SQLite_Controller_EventsUser(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();

		//    ta_eventsuser.Connection = this.Connection;
		//}

        public override void Dispose()
        {
            //if (ta_eventsuser != null) ta_eventsuser.Dispose();
			//this.DisposeObject(ta_eventsuser);

            //ta_eventsuser = null;

            base.Dispose();
        }

        #endregion

        #region adapter methods

        //#region Update
        //public void InitializeCommandInsert(SQLiteCommand command)
        //{
        //    command.CommandText = @"INSERT INTO CZMST_EventsUser" +
        //        " (eguid, eid, etype, etime, termid, userid, loginid, machineid, modul, countentries, docnmbr, itemnmbr, REZ1, REZ2)" +
        //        " VALUES "+
        //        " (@eguid,@eid,@etype,@etime,@termid,@userid,@loginid,@machineid,@modul,@countentries,@docnmbr,@itemnmbr,@REZ1,@REZ2)";

        //    global::System.Data.SQLite.SQLiteParameter param;
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@eguid";
        //    param.SourceColumn = "eguid";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@eid";
        //    param.SourceColumn = "eid";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@etype";
        //    param.SourceColumn = "etype";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@etime";
        //    param.SourceColumn = "etime";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@termid";
        //    param.SourceColumn = "termid";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@userid";
        //    param.SourceColumn = "userid";
        //    command.Parameters.Add(param); 
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@loginid";
        //    param.SourceColumn = "loginid";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@machineid";
        //    param.SourceColumn = "machineid";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@modul";
        //    param.SourceColumn = "modul";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@countentries";
        //    param.SourceColumn = "countentries";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@docnmbr";
        //    param.SourceColumn = "docnmbr";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@itemnmbr";
        //    param.SourceColumn = "itemnmbr";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@REZ1";
        //    param.SourceColumn = "REZ1";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@REZ2";
        //    param.SourceColumn = "REZ2";
        //    command.Parameters.Add(param); 
        //}
        //public void InitializeCommandUpdate(SQLiteCommand command)
        //{
        //    command.CommandText = @"UPDATE    CZMST_EventsUser "+
        //        " SET eid = @eid, etype = @etype, etime = @etime, termid = @termid, userid = @userid, loginid = @loginid, machineid = @machineid, modul = @modul, "+
        //        " countentries = @countentries, docnmbr = @docnmbr, itemnmbr = @itemnmbr, REZ1 = @REZ1, REZ2 = @REZ2"+
        //        " WHERE (eguid = @eguid)";

        //    global::System.Data.SQLite.SQLiteParameter param;

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@eid";
        //    param.SourceColumn = "eid";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@etype";
        //    param.SourceColumn = "etype";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@etime";
        //    param.SourceColumn = "etime";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@termid";
        //    param.SourceColumn = "termid";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@userid";
        //    param.SourceColumn = "userid";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@loginid";
        //    param.SourceColumn = "loginid";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@machineid";
        //    param.SourceColumn = "machineid";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@modul";
        //    param.SourceColumn = "modul";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@countentries";
        //    param.DbType = global::System.Data.DbType.Int32;
        //    param.SourceColumn = "countentries";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@docnmbr";
        //    param.SourceColumn = "docnmbr";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@itemnmbr";
        //    param.SourceColumn = "itemnmbr";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@REZ1";
        //    param.SourceColumn = "REZ1";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@REZ2";
        //    param.SourceColumn = "REZ2";
        //    command.Parameters.Add(param);

        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@eguid";
        //    param.SourceColumn = "eguid";
        //    param.SourceVersion = global::System.Data.DataRowVersion.Original;
        //    command.Parameters.Add(param);
        //}
        //public void InitializeCommandDelete(SQLiteCommand command)
        //{
        //    command.CommandText = "DELETE FROM CZMST_EventsUser WHERE (eguid = @eguid)";
        //    global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@eguid";
        //    param.DbType = global::System.Data.DbType.Guid;
        //    param.SourceColumn = "eguid";
        //    param.SourceVersion = global::System.Data.DataRowVersion.Original;
        //    command.Parameters.Add(param);
        //}
        //public void InitializeCommandSelect(SQLiteCommand command)
        //{
        //    command.CommandText = "Select * from CZMST_EventsUser";
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
        //        using (var commandUpdate = this.Connection.CreateCommand())
        //        using (var commandDelete = this.Connection.CreateCommand())
        //        using (var commandSelect = this.Connection.CreateCommand())
        //        {
        //            InitializeCommandInsert(commandInsert);
        //            InitializeCommandUpdate(commandUpdate);
        //            InitializeCommandDelete(commandDelete);
        //            InitializeCommandSelect(commandSelect);

        //            using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
        //            {
        //                adapter.DeleteCommand = commandDelete;
        //                adapter.InsertCommand = commandInsert;
        //                adapter.UpdateCommand = commandUpdate;
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
        //public int CZMST_EventsUser_Fill(Fask.SQLiteDBs.DataSets.EventsUser.CZMST_EventsUserDataTable dataTable) {
        //    try
        //    {
        //        Connection_Open();
        //        using (var command = this.Connection.CreateCommand())
        //        {
        //            command.CommandText = 
        //                "SELECT * FROM CZMST_EventsUser";
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

        //public Fask.SQLiteDBs.DataSets.EventsUser.CZMST_EventsUserDataTable CZMST_EventsUser_GetData() {
        //    var dataTable = new Fask.SQLiteDBs.DataSets.EventsUser.CZMST_EventsUserDataTable();
        //    this.CZMST_EventsUser_Fill(dataTable);
        //    return dataTable;
        //}

        //public object CZMST_EventsUser_PocetUdalosti() {
        //    try
        //    {
        //        Connection_Open();

        //        using (var command = this.Connection.CreateCommand())
        //        {
        //            command.CommandText =
        //                "SELECT COUNT(*) AS Pocet FROM CZMST_EventsUser";;
        //            object o = command.ExecuteScalar();
        //            return o;
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
        //#endregion

        #endregion



        #region Zrevidovane


        public int Fill(Fask.SQLiteDBs.DataSets.EventsUser.CZMST_EventsUserDataTable dataTable)
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
                        adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_EventsUser";

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



        public int? PocetUdalosti()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT COUNT(*) FROM CZMST_EventsUser"; ;
					object returnValue = command.ExecuteScalar();
					
					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return null;
					}
					else
					{
						Int64 pp = (Int64)returnValue;
						return (int?)pp;
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
			command.CommandText = @"INSERT INTO CZMST_EventsUser" +
				" (eguid, eid, etype, etime, termid, userid, loginid, machineid, modul, countentries, docnmbr, itemnmbr, REZ1, REZ2)" +
				" VALUES " +
				" (@eguid,@eid,@etype,@etime,@termid,@userid,@loginid,@machineid,@modul,@countentries,@docnmbr,@itemnmbr,@REZ1,@REZ2)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@eguid", DbType = System.Data.DbType.Guid, SourceColumn = "eguid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@eid", DbType = System.Data.DbType.String, SourceColumn = "eid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@etype", DbType = System.Data.DbType.String, SourceColumn = "etype" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@etime", DbType = System.Data.DbType.DateTime, SourceColumn = "etime" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@termid", DbType = System.Data.DbType.String, SourceColumn = "termid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@userid", DbType = System.Data.DbType.Int32, SourceColumn = "userid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.Int32, SourceColumn = "loginid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = System.Data.DbType.String, SourceColumn = "machineid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@modul", DbType = System.Data.DbType.String, SourceColumn = "modul" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@countentries", DbType = System.Data.DbType.Int32, SourceColumn = "countentries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@docnmbr", DbType = System.Data.DbType.String, SourceColumn = "docnmbr" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@itemnmbr", DbType = System.Data.DbType.String, SourceColumn = "itemnmbr" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, SourceColumn = "REZ1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ2", DbType = System.Data.DbType.String, SourceColumn = "REZ2" });
			


		}

		public void InitializeCommandUpdate(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE    CZMST_EventsUser " +
				" SET eid = @eid, etype = @etype, etime = @etime, termid = @termid, userid = @userid, loginid = @loginid, machineid = @machineid, modul = @modul, " +
				" countentries = @countentries, docnmbr = @docnmbr, itemnmbr = @itemnmbr, REZ1 = @REZ1, REZ2 = @REZ2" +
				" WHERE (eguid = @eguid)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@eid", DbType = System.Data.DbType.String , SourceColumn = "eid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@etype", DbType = System.Data.DbType.String, SourceColumn = "etype" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@etime", DbType = System.Data.DbType.DateTime, SourceColumn = "etime" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@termid", DbType = System.Data.DbType.String, SourceColumn = "termid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@userid", DbType = System.Data.DbType.Int32, SourceColumn = "userid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.Int32, SourceColumn = "loginid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@machineid", DbType = System.Data.DbType.String, SourceColumn = "machineid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@modul", DbType = System.Data.DbType.String, SourceColumn = "modul" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@countentries", DbType = System.Data.DbType.Int32, SourceColumn = "countentries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@docnmbr", DbType = System.Data.DbType.String, SourceColumn = "docnmbr" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@itemnmbr", DbType = System.Data.DbType.String, SourceColumn = "itemnmbr" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, SourceColumn = "REZ1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ2", DbType = System.Data.DbType.String, SourceColumn = "REZ2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@eguid", DbType = System.Data.DbType.Guid, SourceColumn = "eguid" });
		}

		public void InitializeCommandDelete(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST_EventsUser WHERE (eguid = @eguid)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@eguid",
				DbType = System.Data.DbType.Guid,
				SourceColumn = "eguid",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_EventsUser";
		}

		
		#endregion
		
		#endregion


		#endregion

	}
}
