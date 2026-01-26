using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Fask.SQLiteDBs.Controllers
{
	/// <summary>
	/// Controller pro Tiskarny
	/// </summary>
	public class SQLite_Controller_Tiskarny : SQLite_Controller
	{
		//private Fask.SQLiteDBs.DataSets.TiskarnyTableAdapters.CZMST_TISKARNATableAdapter ta_tiskarny = null;
		//internal Fask.SQLiteDBs.DataSets.TypDokladuTableAdapters.CZMST092TableAdapter Ta_typdokladu
		//{
		//    get
		//    {
		//        if (ta_tiskarny == null)
		//        {
		//            ta_tiskarny = new Fask.SQLiteDBs.DataSets.TiskarnyTableAdapters.CZMST_TISKARNATableAdapter();
		//            ta_tiskarny.Connection = this.Connection;
		//        }
		//        return ta_tiskarny;
		//    }
		//}

		#region c'tors
		//public SQLite_Controller_Tiskarny()
		//    : base(Main.CiselnikTiskarnyDB)
		//{
		//}

		public SQLite_Controller_Tiskarny(string sqliteFileName)
			: base(sqliteFileName)
		{
		}

		public SQLite_Controller_Tiskarny(SQLiteConnection sqliteconnection)
			: base(sqliteconnection)
		{
		}

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();

		//    ta_typdokladu = new Fask.SQLiteDBs.DataSets.TypDokladuTableAdapters.CZMST092TableAdapter();

		//    ta_typdokladu.Connection = this.Connection;

		//}
		#endregion

		public override void Dispose()
		{
			// disposing adapters ...
			//if (ta_typdokladu != null)
			//    ta_typdokladu.Dispose();

			//this.DisposeObject(ta_tiskarny);

			//this.ta_tiskarny = null;

			base.Dispose();
		}


		#region Metody

		public int Fill(Fask.SQLiteDBs.DataSets.Tiskarny.CZMST_TISKARNADataTable dataTable)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_TISKARNA";

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
			command.CommandText = 
				" INSERT INTO [CZMST_TISKARNA] " + 
				" ([ID], [NAME], [LOCATION], [IP], [PORT], [COM], [SOUBOR], [TIMEOUT], [BARCODE], [DEFAULT]) " + 
				" VALUES " +
				" (@ID, @NAME, @LOCATION, @IP, @PORT, @COM, @SOUBOR, @TIMEOUT, @BARCODE, @DEFAULT)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAME", DbType = System.Data.DbType.String, SourceColumn = "NAME" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCATION", DbType = System.Data.DbType.String, SourceColumn = "LOCATION" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IP", DbType = System.Data.DbType.String, SourceColumn = "IP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PORT", DbType = System.Data.DbType.String, SourceColumn = "PORT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@COM", DbType = System.Data.DbType.String, SourceColumn = "COM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOUBOR", DbType = System.Data.DbType.String, SourceColumn = "SOUBOR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEOUT", DbType = System.Data.DbType.Int32, SourceColumn = "TIMEOUT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BARCODE", DbType = System.Data.DbType.String, SourceColumn = "BARCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEFAULT", DbType = System.Data.DbType.Boolean, SourceColumn = "DEFAULT" });
		}

		public void InitializeCommandUpdate(SQLiteCommand command)
		{
			command.CommandText = "UPDATE [CZMST_TISKARNA] SET " +
				" [ID] = @ID, [NAME] = @NAME, [LOCATION] = @LOCATION, [IP] = @IP, [PORT] = @PORT, [COM] = @COM, [SOUBOR] = @SOUBOR, [TIMEOUT] = @TIMEOUT, [BARCODE] = @BARCODE, [DEFAULT] = @DEFAULT " +
				" WHERE (([ID] = @ID) AND ([NAME] = @NAME))";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NAME", DbType = System.Data.DbType.String, SourceColumn = "NAME" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCATION", DbType = System.Data.DbType.String, SourceColumn = "LOCATION" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IP", DbType = System.Data.DbType.String, SourceColumn = "IP" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PORT", DbType = System.Data.DbType.String, SourceColumn = "PORT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@COM", DbType = System.Data.DbType.String, SourceColumn = "COM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOUBOR", DbType = System.Data.DbType.String, SourceColumn = "SOUBOR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEOUT", DbType = System.Data.DbType.Int32, SourceColumn = "TIMEOUT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@BARCODE", DbType = System.Data.DbType.String, SourceColumn = "BARCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEFAULT", DbType = System.Data.DbType.Boolean, SourceColumn = "DEFAULT" });
		}

		public void InitializeCommandDelete(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM [CZMST_TISKARNA] WHERE (([ID] = @ID) AND ([NAME] = @NAME))";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@ID",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "ID",
				SourceVersion = System.Data.DataRowVersion.Original
			});

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@NAME",
				DbType = System.Data.DbType.String,
				SourceColumn = "NAME",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_TISKARNA";
		}


		#endregion

		#endregion


		#endregion


	}
}
