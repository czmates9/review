using System;
using System.Data;
using System.Data.SQLite;

namespace Fask.SQLiteDBs.Controllers
{
	public class SQLite_Controller_Settings : SQLite_Controller
	{


		#region c'tors

		public SQLite_Controller_Settings(string sqliteFileName)
			: base(sqliteFileName)
		{
		}

		public SQLite_Controller_Settings(SQLiteConnection sqliteconnection)
			: base(sqliteconnection)
		{
		}

		#endregion

		public override void Dispose()
		{
			base.Dispose();
		}

		internal int Insert_Settings(string Key, string value)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				{
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.InsertCommand = commandInsert;

						adapter.InsertCommand.CommandText = @" INSERT INTO Settings_Values (KEY, VALUE ) VALUES( @KEY, @VALUE )";

						adapter.InsertCommand.Parameters.AddWithValue("@KEY", Key);
						adapter.InsertCommand.Parameters.AddWithValue("@VALUE", value == null ? string.Empty : value);

						//adapter.InsertCommand.Connection.Open();

						result = adapter.InsertCommand.ExecuteNonQuery();
					}
				}

				transaction.Commit();
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
				Connection_Close();
			}
		}

		internal int Update_Settings(string Key, string value)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				{
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.InsertCommand = commandInsert;

						adapter.InsertCommand.CommandText = @" UPDATE Settings_Values SET VALUE = @VALUE WHERE KEY = @KEY";

						adapter.InsertCommand.Parameters.AddWithValue("@KEY", Key);
						adapter.InsertCommand.Parameters.AddWithValue("@VALUE", value == null ? string.Empty : value);

						//adapter.InsertCommand.Connection.Open();

						result = adapter.InsertCommand.ExecuteNonQuery();
					}
				}

				transaction.Commit();
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
				Connection_Close();
			}
		}

		internal string GetValueByKEY(string Key)
		{

			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT VALUE FROM Settings_Values WHERE KEY = '" + Key + "'";
					command.CommandType = System.Data.CommandType.Text;

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return null;
					}
					else
					{
						if (returnValue is string)
							return returnValue as string;
						else
							return null;

					}
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
				Connection_Close();
			}
		}
	}
}
