using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Fask.SQLiteDBs.Controllers
{
	/// <summary>
	/// Controller pro Servis Ciselniky
	/// </summary>
	public class SQLite_Controller_Servis_Ciselniky : SQLite_Controller
	{
        #region Table adapters

        #region Ciselniky
		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojTableAdapter taCiselnikZdroj = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojTableAdapter TaCiselnikZdroj
		//{
		//    get
		//    {
		//        if (taCiselnikZdroj == null)
		//        {
		//            taCiselnikZdroj = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_ZdrojTableAdapter();
		//            taCiselnikZdroj.Connection = this.Connection;
		//        }
		//        return taCiselnikZdroj;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavTableAdapter taCiselnikStav = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavTableAdapter TaCiselnikStav
		//{
		//    get
		//    {
		//        if (taCiselnikStav == null)
		//        {
		//            taCiselnikStav = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavTableAdapter();
		//            taCiselnikStav.Connection = this.Connection;
		//        }
		//        return taCiselnikStav;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavNextTableAdapter taCiselnikStavNext = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavNextTableAdapter TaCiselnikStavNext
		//{
		//    get
		//    {
		//        if (taCiselnikStavNext == null)
		//        {
		//            taCiselnikStavNext = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_StavNextTableAdapter();
		//            taCiselnikStavNext.Connection = this.Connection;
		//        }
		//        return taCiselnikStavNext;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostTableAdapter taCiselnikCinnost = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostTableAdapter TaCiselnikCinnost
		//{
		//    get
		//    {
		//        if (taCiselnikCinnost == null)
		//        {
		//            taCiselnikCinnost = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostTableAdapter();
		//            taCiselnikCinnost.Connection = this.Connection;
		//        }
		//        return taCiselnikCinnost;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostNextTableAdapter taCiselnikCinnostNext = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostNextTableAdapter TaCiselnikCinnostNext
		//{
		//    get
		//    {
		//        if (taCiselnikCinnostNext == null)
		//        {
		//            taCiselnikCinnostNext = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_CinnostNextTableAdapter();
		//            taCiselnikCinnostNext.Connection = this.Connection;
		//        }
		//        return taCiselnikCinnostNext;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_Table_DefinitionTableAdapter taDynTabDef = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_Table_DefinitionTableAdapter TaDynTabDef
		//{
		//    get
		//    {
		//        if (taDynTabDef == null)
		//        {
		//            taDynTabDef = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_Table_DefinitionTableAdapter();
		//            taDynTabDef.Connection = this.Connection;
		//        }
		//        return taDynTabDef;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_TableTableAdapter taDynTab = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_TableTableAdapter TaDynTab
		//{
		//    get
		//    {
		//        if (taDynTab == null)
		//        {
		//            taDynTab = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_Dynamic_TableTableAdapter();
		//            taDynTab.Connection = this.Connection;
		//        }
		//        return taDynTab;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter taCiselnikOkruh = null;
		//internal Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter TaCiselnikOkruh
		//{
		//    get
		//    {
		//        if (taCiselnikOkruh == null)
		//        {
		//            taCiselnikOkruh = new Fask.SQLiteDBs.DataSets.ServisTableAdapters.CZMST_Servis_OkruhTableAdapter();
		//            taCiselnikOkruh.Connection = this.Connection;
		//        }
		//        return taCiselnikOkruh;
		//    }
		//}


        #endregion

        #endregion

        #region c'tors

        public SQLite_Controller_Servis_Ciselniky(string sqliteFileName)
			: base(sqliteFileName)
		{
        }

		#endregion

		public override void Dispose()
		{

			//this.DisposeObject(taCiselnikZdroj);
			//this.DisposeObject(taCiselnikStav);
			//this.DisposeObject(taCiselnikStavNext);
			//this.DisposeObject(taCiselnikCinnost);
			//this.DisposeObject(taCiselnikCinnostNext);
            //this.DisposeObject(taCiselnikOkruh);
            //this.DisposeObject(taDynTabDef);
            //this.DisposeObject(taDynTab);			

			//this.taCiselnikZdroj = null;
			//this.taCiselnikStav = null;
			//this.taCiselnikStavNext = null;
			//this.taCiselnikCinnost = null;
			//this.taCiselnikCinnostNext = null;
            //this.taCiselnikOkruh = null;
            //this.taDynTabDef = null;
            //this.taDynTab = null;
            
			base.Dispose();
		}

		#region Metody

		#region Zdroj

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojDataTable GetDataByID_Zdroj(string ID)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_ZdrojDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_Zdroj WHERE (ID = @ID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, Value = ID == null ? (object)DBNull.Value : ID });

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

		#region Update_CZMST_Servis_Zdroj

		public int Update_CZMST_Servis_Zdroj(object data)
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
					InitializeCommandInsert_CZMST_Servis_Zdroj(commandInsert);
					//InitializeCommandUpdate_CZMST_Servis_Zdroj(commandUpdate);
					//InitializeCommandDelete_CZMST_Servis_Zdroj(commandDelete);
					InitializeCommandSelect_CZMST_Servis_Zdroj(commandSelect);

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

		public void InitializeCommandInsert_CZMST_Servis_Zdroj(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_Servis_Zdroj ( " + 
				" ID, Oznaceni, Barcode, Type, Misto " + 
				" ) VALUES ( " + 
				" @ID,@Oznaceni,@Barcode,@Type,@Misto" + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Oznaceni", DbType = System.Data.DbType.String, SourceColumn = "Oznaceni" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, SourceColumn = "Barcode" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Type", DbType = System.Data.DbType.String, SourceColumn = "Type" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Misto", DbType = System.Data.DbType.String, SourceColumn = "Misto" });
		}

		//public void InitializeCommandUpdate_CZMST_Servis_Zdroj(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE CZMST097 SET " +
		//		" mena_ID = @mena_ID," +
		//		" mena_text = @mena_text," +
		//		" mena_hlavni = @mena_hlavni," +
		//		" mena_kurz = @mena_kurz," +
		//		" mena_kurzDatum = @mena_kurzDatum " +
		//		" WHERE (mena_ID = @mena_ID_Orig)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		//}

		//public void InitializeCommandDelete_CZMST_Servis_Zdroj(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST097 WHERE (mena_ID = @mena_ID)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@mena_ID",
		//		DbType = System.Data.DbType.String,
		//		SourceColumn = "mena_ID",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_CZMST_Servis_Zdroj(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_Zdroj";
		}


		#endregion


		#endregion

		#endregion

		#region Zdroj Seznam

		#region Update_CZMST_Servis_ZdrojSeznam

		public int Update_CZMST_Servis_ZdrojSeznam(object data)
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
					InitializeCommandInsert_CZMST_Servis_ZdrojSeznam(commandInsert);
					//InitializeCommandUpdate_CZMST_Servis_ZdrojSeznam(commandUpdate);
					InitializeCommandDelete_CZMST_Servis_ZdrojSeznam(commandDelete);
					InitializeCommandSelect_CZMST_Servis_ZdrojSeznam(commandSelect);

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

		public void InitializeCommandInsert_CZMST_Servis_ZdrojSeznam(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_Servis_ZdrojSeznam ( " + 
				" ID, ZdrojID, Poradi, IDStav, IDCinnost " + 
				" ) VALUES ( " + 
				" @ID, @ZdrojID, @Poradi, @IDStav, @IDCinnost " + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ZdrojID", DbType = System.Data.DbType.String, SourceColumn = "ZdrojID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Poradi", DbType = System.Data.DbType.Int32, SourceColumn = "Poradi" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDStav", DbType = System.Data.DbType.String, SourceColumn = "IDStav" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDCinnost", DbType = System.Data.DbType.String, SourceColumn = "IDCinnost" });

		}

		//public void InitializeCommandUpdate_CZMST_Servis_ZdrojSeznam(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE CZMST097 SET " +
		//		" mena_ID = @mena_ID," +
		//		" mena_text = @mena_text," +
		//		" mena_hlavni = @mena_hlavni," +
		//		" mena_kurz = @mena_kurz," +
		//		" mena_kurzDatum = @mena_kurzDatum " +
		//		" WHERE (mena_ID = @mena_ID_Orig)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		//}

		public void InitializeCommandDelete_CZMST_Servis_ZdrojSeznam(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST_Servis_ZdrojSeznam WHERE ID = @ID AND ZdrojID = @ZdrojID ";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@ID",
				DbType = System.Data.DbType.String,
				SourceColumn = "ID",
				SourceVersion = System.Data.DataRowVersion.Original
			});

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@ZdrojID",
				DbType = System.Data.DbType.String,
				SourceColumn = "ZdrojID",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_CZMST_Servis_ZdrojSeznam(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_ZdrojSeznam";
		}


		#endregion


		#endregion


		#endregion

		#region Stav

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavDataTable GetDataByID_Stav(string ID)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_Stav WHERE (ID = @ID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, Value = ID == null ? (object)DBNull.Value : ID });

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

		public int FillByList_Stav(Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavDataTable dataTable, List<string> listStavu)
		{
			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "Select * from CZMST_Servis_Stav " +
					"Where ID IN (" + String.Join(",", listStavu.ToArray()) + ")";
					//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, Value = ID == null ? (object)DBNull.Value : ID });

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

		public string Oznaceni_Stav(string ID)
		{
			try
			{
				Connection_Open();

				object OznaceniObject = null;
				using (var command = Connection.CreateCommand())
				{
					command.CommandText = "SELECT Oznaceni FROM CZMST_Servis_Stav WHERE (ID = @ID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, Value = ID == null ? (object)DBNull.Value : ID });
					OznaceniObject = command.ExecuteScalar();
				}

				string Oznaceni = "-";
				if (OznaceniObject is string)
					Oznaceni = (OznaceniObject as string).Trim();

				if (string.IsNullOrEmpty(Oznaceni))
					Oznaceni = "-";


				return Oznaceni;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return "Err";
			}
			finally
			{
				Connection_Close();
			}
		}

		#region Update_CZMST_Servis_Stav

		public int Update_CZMST_Servis_Stav(object data)
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
					InitializeCommandInsert_CZMST_Servis_Stav(commandInsert);
					//InitializeCommandUpdate_CZMST_Servis_Stav(commandUpdate);
					//InitializeCommandDelete_CZMST_Servis_Stav(commandDelete);
					InitializeCommandSelect_CZMST_Servis_Stav(commandSelect);

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

		public void InitializeCommandInsert_CZMST_Servis_Stav(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_Servis_Stav ( " + 
				" ID, Oznaceni, IDCinnost, Barcode" + 
				" ) VALUES ( " + 
				" @ID,@Oznaceni,@IDCinnost,@Barcode)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Oznaceni", DbType = System.Data.DbType.String, SourceColumn = "Oznaceni" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDCinnost", DbType = System.Data.DbType.String, SourceColumn = "IDCinnost" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, SourceColumn = "Barcode" });
			
		}

		//public void InitializeCommandUpdate_CZMST_Servis_Stav(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE CZMST097 SET " +
		//		" mena_ID = @mena_ID," +
		//		" mena_text = @mena_text," +
		//		" mena_hlavni = @mena_hlavni," +
		//		" mena_kurz = @mena_kurz," +
		//		" mena_kurzDatum = @mena_kurzDatum " +
		//		" WHERE (mena_ID = @mena_ID_Orig)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		//}

		//public void InitializeCommandDelete_CZMST_Servis_Stav(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST097 WHERE (mena_ID = @mena_ID)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@mena_ID",
		//		DbType = System.Data.DbType.String,
		//		SourceColumn = "mena_ID",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_CZMST_Servis_Stav(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_Stav";
		}


		#endregion


		#endregion

		#endregion

		#region StavNext

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavNextDataTable GetDataByID_StavNext(string ID)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavNextDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_StavNextDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ID, IDNext FROM CZMST_Servis_StavNext WHERE (ID = @ID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, Value = ID == null ? (object)DBNull.Value : ID });

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

		#region Update_CZMST_Servis_StavNext

		public int Update_CZMST_Servis_StavNext(object data)
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
					InitializeCommandInsert_CZMST_Servis_StavNext(commandInsert);
					//InitializeCommandUpdate_CZMST_Servis_Stav(commandUpdate);
					//InitializeCommandDelete_CZMST_Servis_Stav(commandDelete);
					InitializeCommandSelect_CZMST_Servis_StavNext(commandSelect);

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

		public void InitializeCommandInsert_CZMST_Servis_StavNext(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_Servis_StavNext ( " + 
				" ID, IDNext " + 
				") VALUES ( " +
				" @ID,@IDNext " + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDNext", DbType = System.Data.DbType.String, SourceColumn = "IDNext" });

		}

		//public void InitializeCommandUpdate_CZMST_Servis_StavNext(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE CZMST097 SET " +
		//		" mena_ID = @mena_ID," +
		//		" mena_text = @mena_text," +
		//		" mena_hlavni = @mena_hlavni," +
		//		" mena_kurz = @mena_kurz," +
		//		" mena_kurzDatum = @mena_kurzDatum " +
		//		" WHERE (mena_ID = @mena_ID_Orig)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		//}

		//public void InitializeCommandDelete_CZMST_Servis_StavNext(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST097 WHERE (mena_ID = @mena_ID)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@mena_ID",
		//		DbType = System.Data.DbType.String,
		//		SourceColumn = "mena_ID",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_CZMST_Servis_StavNext(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_StavNext";
		}


		#endregion


		#endregion

		#endregion

		#region Cinnost

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostDataTable GetDataByID_Cinnost(string ID)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_Cinnost WHERE (ID = @ID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, Value = ID == null ? (object)DBNull.Value : ID });

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

		public int FillByList_Cinnost(Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostDataTable dataTable, List<string> listStavu)
		{
			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "Select * from CZMST_Servis_Cinnost " +
					"Where ID IN (" + String.Join(",", listStavu.ToArray()) + ")";

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

		public string Oznaceni_Cinnost(string ID)
		{
			try
			{
				Connection_Open();

				object OznaceniObject = null;
				using (var command = Connection.CreateCommand())
				{
					command.CommandText = "SELECT Oznaceni FROM CZMST_Servis_Cinnost WHERE (ID = @ID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, Value = ID == null ? (object)DBNull.Value : ID });
					OznaceniObject = command.ExecuteScalar();
				}

				string Oznaceni = "-";
				if (OznaceniObject is string)
					Oznaceni = (OznaceniObject as string).Trim();

				if (string.IsNullOrEmpty(Oznaceni))
					Oznaceni = "-";


				return Oznaceni;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return "Err";
			}
			finally
			{
				Connection_Close();
			}
		}

		#region Update_CZMST_Servis_Cinnost

		public int Update_CZMST_Servis_Cinnost(object data)
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
					InitializeCommandInsert_CZMST_Servis_Cinnost(commandInsert);
					//InitializeCommandUpdate_CZMST_Servis_Cinnost(commandUpdate);
					//InitializeCommandDelete_CZMST_Servis_Cinnost(commandDelete);
					InitializeCommandSelect_CZMST_Servis_Cinnost(commandSelect);

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

		public void InitializeCommandInsert_CZMST_Servis_Cinnost(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_Servis_Cinnost ( " + 
				" ID, Oznaceni, Barcode, TYPE, TYPEVALUE, Mandatory, RequiredLength " + 
				" ) VALUES ( " + 
				" @ID,@Oznaceni,@Barcode,@TYPE,@TYPEVALUE,@Mandatory,@RequiredLength" + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Oznaceni", DbType = System.Data.DbType.String, SourceColumn = "Oznaceni" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, SourceColumn = "Barcode" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPE", DbType = System.Data.DbType.String, SourceColumn = "TYPE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEVALUE", DbType = System.Data.DbType.String, SourceColumn = "TYPEVALUE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Mandatory", DbType = System.Data.DbType.Byte, SourceColumn = "Mandatory" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@RequiredLength", DbType = System.Data.DbType.Int32, SourceColumn = "RequiredLength" });

		}

		//public void InitializeCommandUpdate_CZMST_Servis_Cinnost(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE CZMST097 SET " +
		//		" mena_ID = @mena_ID," +
		//		" mena_text = @mena_text," +
		//		" mena_hlavni = @mena_hlavni," +
		//		" mena_kurz = @mena_kurz," +
		//		" mena_kurzDatum = @mena_kurzDatum " +
		//		" WHERE (mena_ID = @mena_ID_Orig)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		//}

		//public void InitializeCommandDelete_CZMST_Servis_Cinnost(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST097 WHERE (mena_ID = @mena_ID)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@mena_ID",
		//		DbType = System.Data.DbType.String,
		//		SourceColumn = "mena_ID",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_CZMST_Servis_Cinnost(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_Cinnost";
		}


		#endregion


		#endregion


		#endregion

		#region CinnostNext

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostNextDataTable GetDataByIDAndIDValue_CinnostNext(string ID, string IDValue)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostNextDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostNextDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ID, IDNext, IDValue FROM CZMST_Servis_CinnostNext WHERE (ID = @ID) AND (IDValue = @IDValue)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, Value = ID == null ? (object)DBNull.Value : ID });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDValue", DbType = System.Data.DbType.String, Value = IDValue == null ? (object)DBNull.Value : IDValue });

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

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostNextDataTable GetDataByID_CinnostNext(string ID)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostNextDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_CinnostNextDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_CinnostNext WHERE (ID = @ID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, Value = ID == null ? (object)DBNull.Value : ID });

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

		#region Update_CZMST_Servis_CinnostNext

		public int Update_CZMST_Servis_CinnostNext(object data)
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
					InitializeCommandInsert_CZMST_Servis_Cinnost(commandInsert);
					//InitializeCommandUpdate_CZMST_Servis_Cinnost(commandUpdate);
					//InitializeCommandDelete_CZMST_Servis_Cinnost(commandDelete);
					InitializeCommandSelect_CZMST_Servis_Cinnost(commandSelect);

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

		public void InitializeCommandInsert_CZMST_Servis_CinnostNext(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_Servis_CinnostNext ( " + 
				" ID, IDNext, IDValue " + 
				" ) VALUES ( " + 
				" @ID,@IDNext,@IDValue " + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDNext", DbType = System.Data.DbType.String, SourceColumn = "IDNext" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IDValue", DbType = System.Data.DbType.String, SourceColumn = "IDValue" });
	
		}

		//public void InitializeCommandUpdate_CZMST_Servis_CinnostNext(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE CZMST097 SET " +
		//		" mena_ID = @mena_ID," +
		//		" mena_text = @mena_text," +
		//		" mena_hlavni = @mena_hlavni," +
		//		" mena_kurz = @mena_kurz," +
		//		" mena_kurzDatum = @mena_kurzDatum " +
		//		" WHERE (mena_ID = @mena_ID_Orig)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		//}

		//public void InitializeCommandDelete_CZMST_Servis_CinnostNext(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST097 WHERE (mena_ID = @mena_ID)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@mena_ID",
		//		DbType = System.Data.DbType.String,
		//		SourceColumn = "mena_ID",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_CZMST_Servis_CinnostNext(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_CinnostNext";
		}


		#endregion


		#endregion

		#endregion

		#region DynTapDef

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionDataTable GetDataByTypeName_DynTabDef(string TypeName)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_Dynamic_Table_Definition WHERE (TypeName = @TypeName)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TypeName", DbType = System.Data.DbType.String, Value = TypeName == null ? (object)DBNull.Value : TypeName });

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

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionDataTable GetData_DynTabDef()
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_Table_DefinitionDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_Dynamic_Table_Definition ";

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

		#region Update_CZMST_Servis_Dynamic_Table_Definition

		public int Update_CZMST_Servis_Dynamic_Table_Definition(object data)
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
					InitializeCommandInsert_CZMST_Servis_Cinnost(commandInsert);
					//InitializeCommandUpdate_CZMST_Servis_Cinnost(commandUpdate);
					//InitializeCommandDelete_CZMST_Servis_Cinnost(commandDelete);
					InitializeCommandSelect_CZMST_Servis_Cinnost(commandSelect);

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

		public void InitializeCommandInsert_CZMST_Servis_Dynamic_Table_Definition(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_Servis_Dynamic_Table_Definition ( " + 
				" FullName, TypeName " + 
				") VALUES ( " + 
				" @FullName,@TypeName " + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@FullName", DbType = System.Data.DbType.String, SourceColumn = "FullName" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TypeName", DbType = System.Data.DbType.String, SourceColumn = "TypeName" });

		}

		//public void InitializeCommandUpdate_CZMST_Servis_Dynamic_Table_Definition(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE CZMST097 SET " +
		//		" mena_ID = @mena_ID," +
		//		" mena_text = @mena_text," +
		//		" mena_hlavni = @mena_hlavni," +
		//		" mena_kurz = @mena_kurz," +
		//		" mena_kurzDatum = @mena_kurzDatum " +
		//		" WHERE (mena_ID = @mena_ID_Orig)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		//}

		//public void InitializeCommandDelete_CZMST_Servis_Dynamic_Table_Definition(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST097 WHERE (mena_ID = @mena_ID)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@mena_ID",
		//		DbType = System.Data.DbType.String,
		//		SourceColumn = "mena_ID",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_CZMST_Servis_Dynamic_Table_Definition(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_Dynamic_Table_Definition";
		}


		#endregion


		#endregion


		#endregion

		#region DynTab

		public int Fill_DynTab(Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_Dynamic_TableDataTable dataTable)
		{
			try
			{
				dataTable.Clear();
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_Dynamic_Table";

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

		#region Update_CZMST_Servis_Dynamic_Table

		public int Update_CZMST_Servis_Dynamic_Table(object data)
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
					InitializeCommandInsert_CZMST_Servis_Dynamic_Table(commandInsert);
					//InitializeCommandUpdate_CZMST_Servis_Dynamic_Table(commandUpdate);
					//InitializeCommandDelete_CZMST_Servis_Dynamic_Table(commandDelete);
					InitializeCommandSelect_CZMST_Servis_Dynamic_Table(commandSelect);

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

		public void InitializeCommandInsert_CZMST_Servis_Dynamic_Table(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_Servis_Dynamic_Table ( " + 
				"ID, Oznaceni, Barcode " + 
				" ) VALUES ( " + 
				" @ID,@Oznaceni,@Barcode" +
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Oznaceni", DbType = System.Data.DbType.String, SourceColumn = "Oznaceni" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, SourceColumn = "Barcode" });

		}

		//public void InitializeCommandUpdate_CZMST_Servis_Dynamic_Table(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE CZMST097 SET " +
		//		" mena_ID = @mena_ID," +
		//		" mena_text = @mena_text," +
		//		" mena_hlavni = @mena_hlavni," +
		//		" mena_kurz = @mena_kurz," +
		//		" mena_kurzDatum = @mena_kurzDatum " +
		//		" WHERE (mena_ID = @mena_ID_Orig)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		//}

		//public void InitializeCommandDelete_CZMST_Servis_Dynamic_Table(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST097 WHERE (mena_ID = @mena_ID)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@mena_ID",
		//		DbType = System.Data.DbType.String,
		//		SourceColumn = "mena_ID",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_CZMST_Servis_Dynamic_Table(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_Dynamic_Table";
		}


		#endregion


		#endregion

		#endregion

		#region Okruh

		public Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhDataTable GetDataByID_Okruh(string ID)
		{
			Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhDataTable dataTable = new Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhDataTable();

			try
			{

				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_Okruh WHERE (ID=@ID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, Value = ID == null ? (object)DBNull.Value : ID });

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

		public int Fill_Okruh(Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhDataTable dataTable)
		{
			try
			{
				dataTable.Clear();
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_Okruh";

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

		public int FillByOdbID_Okruh(Fask.SQLiteDBs.DataSets.Servis.CZMST_Servis_OkruhDataTable dataTable, string ODB_ID)
		{
			try
			{
				dataTable.Clear();
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_Servis_Okruh WHERE (ODB_ID=@ODB_ID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID });


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

		#region Update_CZMST_Servis_Okruh

		public int Update_CZMST_Servis_Okruh(object data)
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
					InitializeCommandInsert_CZMST_Servis_Okruh(commandInsert);
					//InitializeCommandUpdate_CZMST_Servis_Okruh(commandUpdate);
					InitializeCommandDelete_CZMST_Servis_Okruh(commandDelete);
					InitializeCommandSelect_CZMST_Servis_Okruh(commandSelect);

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

		public void InitializeCommandInsert_CZMST_Servis_Okruh(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_Servis_Okruh ( " + 
				" ID, Oznaceni, ODB_ID, Barcode, ZdrojSeznamID " + 
				" ) VALUES ( " + 
				" @ID,@Oznaceni,@ODB_ID,@Barcode,@ZdrojSeznamID " + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.String, SourceColumn = "ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Oznaceni", DbType = System.Data.DbType.String, SourceColumn = "Oznaceni" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, SourceColumn = "ODB_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, SourceColumn = "Barcode" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ZdrojSeznamID", DbType = System.Data.DbType.String, SourceColumn = "ZdrojSeznamID" });

		}

		//public void InitializeCommandUpdate_CZMST_Servis_Dynamic_Table_Definition(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE CZMST097 SET " +
		//		" mena_ID = @mena_ID," +
		//		" mena_text = @mena_text," +
		//		" mena_hlavni = @mena_hlavni," +
		//		" mena_kurz = @mena_kurz," +
		//		" mena_kurzDatum = @mena_kurzDatum " +
		//		" WHERE (mena_ID = @mena_ID_Orig)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		//}

		public void InitializeCommandDelete_CZMST_Servis_Okruh(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST_Servis_Okruh WHERE ID = @ID";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@ID",
				DbType = System.Data.DbType.String,
				SourceColumn = "ID",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_CZMST_Servis_Okruh(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_Servis_Okruh";
		}


		#endregion


		#endregion


		#endregion

		#endregion
	}
}
