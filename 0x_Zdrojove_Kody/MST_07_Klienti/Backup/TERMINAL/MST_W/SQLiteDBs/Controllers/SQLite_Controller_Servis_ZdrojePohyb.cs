using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Fask.SQLiteDBs.Controllers
{
	/// <summary>
	/// Controller pro Servis pohyby Zdroju
	/// </summary>
	public class SQLite_Controller_Servis_ZdrojePohyb : SQLite_Controller
	{

		#region Table adapters

		#region Pohyby zdroju
		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter taZdrojePohyb = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter TaZdrojePohyb
		//{
		//    get
		//    {
		//        if (taZdrojePohyb == null)
		//        {
		//            taZdrojePohyb = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojPohybTableAdapter();
		//            taZdrojePohyb.Connection = this.Connection;
		//        }
		//        return taZdrojePohyb;
		//    }
		//}

		#endregion

		#endregion

		#region c'tors

		public SQLite_Controller_Servis_ZdrojePohyb(string sqliteFileName)
			: base(sqliteFileName)
		{
		}

		#endregion

		public override void Dispose()
		{

			//this.DisposeObject(taZdrojePohyb);

			//this.taZdrojePohyb = null;

			base.Dispose();
		}

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable GetDataByIDZdrojModified_ZdrojePohyb(string IDZdroj, System.DateTime Modified)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable();

			try
			{
				
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM CZMST_Servis_ZdrojPohyb WHERE (IDZdroj = @IDZdroj) AND (Modified > @Modified) ORDER BY Modified DESC";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDZdroj", DbType = System.Data.DbType.String, Value = IDZdroj == null ? (object)DBNull.Value : IDZdroj, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Modified", DbType = System.Data.DbType.DateTime, Value = Modified, SourceVersion = DataRowVersion.Original });


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

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable GetDataByGUID_ZdrojePohyb(System.Guid GUID)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM CZMST_Servis_ZdrojPohyb WHERE (GUID = @GUID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID , SourceVersion = DataRowVersion.Original });

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

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable GetData_ZdrojePohyb()
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable();
			this.Fill_ZdrojePohyb(dataTable);
			return dataTable;
		}

		public int Fill_ZdrojePohyb(Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dataTable)
		{
			try
			{
				dataTable.Clear();
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_ZdrojPohyb ";
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
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				Connection_Close();
			}
		}

		public int FillByModified_ZdrojePohyb(Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojPohybDataTable dataTable, System.DateTime Modified)
		{
			try
			{
				dataTable.Clear();
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM CZMST_Servis_ZdrojPohyb WHERE (Modified < @Modified)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Modified", DbType = System.Data.DbType.DateTime, Value = Modified, SourceVersion = DataRowVersion.Original });

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
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				Connection_Close();
			}
		}

		public int? PocetPohybu_ZdrojePohyb()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM CZMST_Servis_ZdrojPohyb";
					command.CommandType = System.Data.CommandType.Text;

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return 0;
					}
					else
					{
						Int64 pp = (Int64)returnValue;
						return (int)pp;
					}
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return -1;
			}
			finally
			{
				Connection_Close();
			}
		}

		public int Delete_ZdrojePohyb(System.Guid GUID)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_Servis_ZdrojPohyb WHERE (GUID = @GUID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID, SourceVersion = DataRowVersion.Original });

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

		public int DeleteAllQuery_ZdrojePohyb()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_Servis_ZdrojPohyb";

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

		public int Insert_ZdrojePohyb(
			string IDZdroj,
			string IDStav,
			string IDCinnost,
			System.DateTime Modified,
			int IDTerminal,
			int IDUser,
			System.Guid GUID,
			string CinnostValue,
			string CinnostType,
			int? CountEntries,
			string ODB_ID,
			string OkruhID,
			string CinnostOznaceni,
			double? GPS_X,
			double? GPS_Y,
			int? GPS_Z)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.InsertCommand = command;
						adapter.InsertCommand.Connection = this.Connection;

						adapter.InsertCommand.Connection = this.Connection;
						#region Command

						adapter.InsertCommand.CommandText = @"INSERT INTO CZMST_Servis_ZdrojPohyb (" +
					" IDZdroj," +
					" IDStav," +
					" IDCinnost," +
					" Modified," +
					" IDTerminal," +
					" IDUser," +
					" GUID," +
					" CinnostValue," +
					" CinnostType," +
					" CountEntries," +
					" ODB_ID," +
					" OkruhID," +
					" CinnostOznaceni," +
					" GPS_X," +
					" GPS_Y," +
					" GPS_Z" +
					") VALUES (" +
					" @IDZdroj," +
					" @IDStav," +
					" @IDCinnost," +
					" @Modified," +
					" @IDTerminal," +
					" @IDUser," +
					" @GUID," +
					" @CinnostValue," +
					" @CinnostType," +
					" @CountEntries," +
					" @ODB_ID," +
					" @OkruhID," +
					" @CinnostOznaceni," +
					" @GPS_X," +
					" @GPS_Y," +
					" @GPS_Z " +
					" )"; 

						#endregion

						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDZdroj", DbType = System.Data.DbType.String, Value = IDZdroj == null ? (object)DBNull.Value : IDZdroj });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDStav", DbType = System.Data.DbType.String, Value = IDStav == null ? (object)DBNull.Value : IDStav });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDCinnost", DbType = System.Data.DbType.String, Value = IDCinnost == null ? (object)DBNull.Value : IDCinnost });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@Modified", DbType = System.Data.DbType.DateTime, Value = Modified });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDTerminal", DbType = System.Data.DbType.Int32, Value = IDTerminal });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDUser", DbType = System.Data.DbType.Int32, Value = IDUser });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostValue", DbType = System.Data.DbType.String, Value = CinnostValue == null ? (object)DBNull.Value : CinnostValue });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostType", DbType = System.Data.DbType.String, Value = CinnostType == null ? (object)DBNull.Value : CinnostType });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries.HasValue ? CountEntries : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@OkruhID", DbType = System.Data.DbType.String, Value = OkruhID == null ? (object)DBNull.Value : OkruhID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostOznaceni", DbType = System.Data.DbType.String, Value = CinnostOznaceni == null ? (object)DBNull.Value : CinnostOznaceni });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_X", DbType = System.Data.DbType.Double, Value = GPS_X.HasValue ? GPS_X : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Y", DbType = System.Data.DbType.Double , Value = GPS_Y.HasValue ? GPS_Y : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Z", DbType = System.Data.DbType.Int32, Value = GPS_Z.HasValue ? GPS_Z : (object)DBNull.Value });
						#endregion

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
			finally
			{
				Connection_Close();
			}
		}

		#region Update

		public int Update_ZdrojePohyb(object data)
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
					InitializeCommandInsert_ZdrojePohyb(commandInsert);
					InitializeCommandUpdate_ZdrojePohyb(commandUpdate);
					InitializeCommandDelete_ZdrojePohyb(commandDelete);
					InitializeCommandSelect_ZdrojePohyb(commandSelect);

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

		public void InitializeCommandInsert_ZdrojePohyb(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO CZMST_Servis_ZdrojPohyb (" +
					" IDZdroj," +
					" IDStav," +
					" IDCinnost," +
					" Modified," +
					" IDTerminal," +
					" IDUser," +
					" GUID," +
					" CinnostValue," +
					" CinnostType," +
					" CountEntries," +
					" ODB_ID," +
					" OkruhID," +
					" CinnostOznaceni," +
					" GPS_X," +
					" GPS_Y," +
					" GPS_Z" +
					") VALUES (" +
					" @IDZdroj," +
					" @IDStav," +
					" @IDCinnost," +
					" @Modified," +
					" @IDTerminal," +
					" @IDUser," +
					" @GUID," +
					" @CinnostValue," +
					" @CinnostType," +
					" @CountEntries," +
					" @ODB_ID," +
					" @OkruhID," +
					" @CinnostOznaceni," +
					" @GPS_X," +
					" @GPS_Y," +
					" @GPS_Z " +
					" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDZdroj", DbType = System.Data.DbType.String, SourceColumn = "IDZdroj"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDStav", DbType = System.Data.DbType.String, SourceColumn = "IDStav"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDCinnost", DbType = System.Data.DbType.String, SourceColumn = "IDCinnost"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Modified", DbType = System.Data.DbType.DateTime, SourceColumn = "Modified" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDTerminal", DbType = System.Data.DbType.Int32, SourceColumn = "IDTerminal" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDUser", DbType = System.Data.DbType.Int32, SourceColumn = "IDUser" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostValue", DbType = System.Data.DbType.String, SourceColumn = "CinnostValue"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostType", DbType = System.Data.DbType.String, SourceColumn = "CinnostType"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, SourceColumn = "ODB_ID"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OkruhID", DbType = System.Data.DbType.String, SourceColumn = "OkruhID"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostOznaceni", DbType = System.Data.DbType.String, SourceColumn = "CinnostOznaceni"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_X", DbType = System.Data.DbType.Double, SourceColumn = "GPS_X" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Y", DbType = System.Data.DbType.Double, SourceColumn = "GPS_Y" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Z", DbType = System.Data.DbType.Int32, SourceColumn = "GPS_Z" });
		}

		public void InitializeCommandUpdate_ZdrojePohyb(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE CZMST_Servis_ZdrojPohyb SET " + 
				" IDZdroj = @IDZdroj," + 
				" IDStav = @IDStav," + 
				" IDCinnost = @IDCinnost," + 
				" Modified = @Modified," + 
				" IDTerminal = @IDTerminal," + 
				" IDUser = @IDUser," + 
				" CinnostValue = @CinnostValue," + 
				" CinnostType = @CinnostType," + 
				" CountEntries = @CountEntries," + 
				" ODB_ID = @ODB_ID," + 
				" OkruhID = @OkruhID," + 
				" CinnostOznaceni = @CinnostOznaceni," + 
				" GPS_X = @GPS_X," + 
				" GPS_Y = @GPS_Y," + 
				" GPS_Z = @GPS_Z " + 
				"WHERE (GUID = @GUID)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDZdroj", DbType = System.Data.DbType.String, SourceColumn = "IDZdroj" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDStav", DbType = System.Data.DbType.String, SourceColumn = "IDStav" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDCinnost", DbType = System.Data.DbType.String, SourceColumn = "IDCinnost" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Modified", DbType = System.Data.DbType.DateTime, SourceColumn = "Modified" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDTerminal", DbType = System.Data.DbType.Int32, SourceColumn = "IDTerminal" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDUser", DbType = System.Data.DbType.Int32, SourceColumn = "IDUser" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostValue", DbType = System.Data.DbType.String, SourceColumn = "CinnostValue" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostType", DbType = System.Data.DbType.String, SourceColumn = "CinnostType" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, SourceColumn = "ODB_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OkruhID", DbType = System.Data.DbType.String, SourceColumn = "OkruhID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CinnostOznaceni", DbType = System.Data.DbType.String, SourceColumn = "CinnostOznaceni" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_X", DbType = System.Data.DbType.Double, SourceColumn = "GPS_X" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Y", DbType = System.Data.DbType.Double, SourceColumn = "GPS_Y" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GPS_Z", DbType = System.Data.DbType.Int32, SourceColumn = "GPS_Z" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID", SourceVersion = DataRowVersion.Original });
		}

		public void InitializeCommandDelete_ZdrojePohyb(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST_Servis_ZdrojPohyb WHERE (GUID = @GUID)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@GUID",
				DbType = System.Data.DbType.Guid,
				SourceColumn = "GUID",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_ZdrojePohyb(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_ZdrojPohyb";
		}


		#endregion

		#endregion
	}
}
