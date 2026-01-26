using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controller pro Uzivatele
    /// </summary>
    public class SQLite_Controller_Users : SQLite_Controller
    {
		//private Fask.SQLiteDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter ta_users = null;
		//internal Fask.SQLiteDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter Ta_users
		//{
		//    get
		//    {
		//        if (ta_users == null)
		//        {
		//            ta_users = new Fask.SQLiteDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter();
		//            ta_users.Connection = this.Connection;
		//        }
		//        return ta_users;
		//    }
		//}


        #region c'tors
		//public SQLite_Controller_Users()
		//    : base(Main.CiselnikUzivateleDB)
		//{
		//}

        public SQLite_Controller_Users(string sqliteFileName)
            : base(sqliteFileName)
        {
        }

        public SQLite_Controller_Users(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();

		//    ta_users = new Fask.SQLiteDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter();

		//    ta_users.Connection = this.Connection;

		//}
        #endregion

        public override void Dispose()
        {
            // disposing adapters ...
			//if (ta_users != null)
			//    ta_users.Dispose();

			//this.DisposeObject(ta_users);

			//this.ta_users = null;

            base.Dispose();
        }

		#region Metody

		public Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable GetData()
		{
			Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable dt = new Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable();
			try
			{
				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT ID, Login, Pwd, Hash, EAN, FIRSTNAME, SECONDNAME FROM Users";

						adapter.Fill(dt);
					}
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}

			return dt;
		}

		public Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable GetDataByEAN(string EAN)
		{
			Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable dt = new Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable();
			try
			{
				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT ID, Login, Pwd, Hash, EAN, FIRSTNAME, SECONDNAME FROM Users WHERE (EAN = @EAN)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, Value = EAN == null ? (object)DBNull.Value : EAN });
						adapter.Fill(dt);
					}
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}
			return dt;
		}

		public Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable GetDataByLogin(string Login)
		{
			Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable dt = new Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable();
			try
			{
				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT ID, Login, Pwd, Hash, EAN, FIRSTNAME, SECONDNAME FROM Users WHERE (Login = @Login)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@Login", DbType = System.Data.DbType.String, Value = Login == null ? (object)DBNull.Value : Login });

						adapter.Fill(dt);
					}
				}
			}
			catch (System.Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
			}
			return dt;
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
				using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert(commandInsert);
					//InitializeCommandUpdate_CZMST_Servis_ZdrojSeznam(commandUpdate);
					InitializeCommandDelete(commandDelete);
					InitializeCommandSelect(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.DeleteCommand = commandDelete;
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
			command.CommandText = "INSERT INTO Users ( " +
				" ID, Login, Pwd, Hash, EAN, FIRSTNAME, SECONDNAME " +
				" ) VALUES ( " +
				" @ID, @Login, @Pwd, @Hash, @EAN, @FIRSTNAME, @SECONDNAME " +
				" ) ";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Login", DbType = System.Data.DbType.String, SourceColumn = "Login" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Pwd", DbType = System.Data.DbType.String, SourceColumn = "Pwd" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Hash", DbType = System.Data.DbType.String, SourceColumn = "Hash" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, SourceColumn = "EAN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@FIRSTNAME", DbType = System.Data.DbType.String, SourceColumn = "FIRSTNAME" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SECONDNAME", DbType = System.Data.DbType.String, SourceColumn = "SECONDNAME" });

		}

		public void InitializeCommandUpdate(SQLiteCommand command)
		{
			command.CommandText = "UPDATE [Users] SET " +
				" [ID] = @ID," +
				" [Login] = @Login," +
				" [Pwd] = @Pwd," +
				" [Hash] = @Hash," +
				" [EAN] = @EAN" +
				" WHERE [ID] = @ID_P " + 
				" AND " +
				" Login = @Login_P))";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Login", DbType = System.Data.DbType.String, SourceColumn = "Login" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Pwd", DbType = System.Data.DbType.String, SourceColumn = "Pwd" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Hash", DbType = System.Data.DbType.String, SourceColumn = "Hash" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, SourceColumn = "EAN" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_P", DbType = System.Data.DbType.String, SourceColumn = "ID" , SourceVersion = System.Data.DataRowVersion.Original});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Login_P", DbType = System.Data.DbType.String, SourceColumn = "Login", SourceVersion = System.Data.DataRowVersion.Original  });
		}

		public void InitializeCommandDelete(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM Users WHERE ID = @ID AND Login = @Login";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@ID",
				DbType = System.Data.DbType.String,
				SourceColumn = "ID",
				SourceVersion = System.Data.DataRowVersion.Original
			});

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@Login",
				DbType = System.Data.DbType.String,
				SourceColumn = "Login",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect(SQLiteCommand command)
		{
			command.CommandText = "Select * from Users";
		}


		#endregion


		#endregion


		#endregion

	}
}
