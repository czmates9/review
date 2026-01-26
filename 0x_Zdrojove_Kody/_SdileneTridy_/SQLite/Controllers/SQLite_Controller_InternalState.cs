using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controller pro Vyrobu
    /// </summary>
    public class SQLite_Controller_InternalState : SQLite_Controller
    {
		//private SQLiteDBs.DataSets.InternalStateTableAdapters.LstOperationUserTableAdapter ta_IS = null;
		//internal SQLiteDBs.DataSets.InternalStateTableAdapters.LstOperationUserTableAdapter Ta_IS
		//{
		//	get
		//	{
		//		if (ta_IS == null)
		//		{
		//			ta_IS = new Fask.SQLiteDBs.DataSets.InternalStateTableAdapters.LstOperationUserTableAdapter();
		//			ta_IS.Connection = this.Connection;
		//		}
		//		return ta_IS;
		//	}
		//}

        #region c'tors

        public SQLite_Controller_InternalState(string SQLiteFilePath_FileName)
            : base(SQLiteFilePath_FileName)
        {
        }

		public SQLite_Controller_InternalState(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

        #endregion

        public override void Dispose()
        {
			// disposing adapters ...
			//this.DisposeObject(ta_IS);
			//this.ta_IS = null;

            base.Dispose();
        }

        #region Metody

        public Fask.SQLiteDBs.DataSets.InternalState.LstOperationUserDataTable GetDataByUserID(string UserID)
        {
			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.InternalState.LstOperationUserDataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM LstOperationUser WHERE (UserID = @UserID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						int returnValue = adapter.Fill(dataTable);
						return dataTable;
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

		public int DeleteUserID(string UserID)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM LstOperationUser WHERE (UserID = @UserID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID, SourceVersion = DataRowVersion.Original });

					int returnValue;
					returnValue = command.ExecuteNonQuery();

					return returnValue;
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

		public int DeleteAll()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM LstOperationUser";

					int returnValue;
					returnValue = command.ExecuteNonQuery();

					return returnValue;
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

		public DateTime? GetLastOperationDateTime(string UserID)
		{
			try
			{
				Connection_Open();

				object dtObject = null;
				using (var command = Connection.CreateCommand())
				{
					command.CommandText = "SELECT LastOper FROM LstOperationUser WHERE (UserID = @UserID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID == null ? (object)DBNull.Value : UserID });
					dtObject = command.ExecuteScalar();
				}

				if (dtObject is DateTime)
				{
					return (DateTime)dtObject;
				}
                                else if (dtObject is string)
                                {
                                   return DateTime.Parse((string)dtObject);
                                }                	
				else
					return null;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}
			finally
			{
				Connection_Close();
			}

			return null;

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
				//using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert(commandInsert);
					InitializeCommandUpdate(commandUpdate);
					//InitializeCommandDelete(commandDelete);
					InitializeCommandSelect(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						//adapter.DeleteCommand = commandDelete;
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
			command.CommandText = "INSERT INTO [LstOperationUser] ([UserID], [LastOper]) VALUES (@UserID, @LastOper)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, SourceColumn = "UserID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LastOper", DbType = System.Data.DbType.DateTime, SourceColumn = "LastOper" });
		}

		public void InitializeCommandUpdate(SQLiteCommand command)
		{
			command.CommandText = "UPDATE LstOperationUser SET LastOper = @LastOper WHERE (UserID = @UserID)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, SourceColumn = "UserID" , SourceVersion = DataRowVersion.Original });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LastOper", DbType = System.Data.DbType.DateTime, SourceColumn = "LastOper" });
		}

		//public void InitializeCommandDelete(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST_Servis_ZdrojPohyb WHERE (GUID = @GUID)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@GUID",
		//		DbType = System.Data.DbType.Guid,
		//		SourceColumn = "GUID",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect(SQLiteCommand command)
		{
			command.CommandText = "Select * from LstOperationUser";
		}


		#endregion

		#endregion

		#endregion


	}
}
