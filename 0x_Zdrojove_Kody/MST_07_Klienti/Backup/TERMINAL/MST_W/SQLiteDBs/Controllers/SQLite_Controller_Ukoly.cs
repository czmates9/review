using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Fask.SQLiteDBs.Controllers
{
	public class SQLite_Controller_Ukoly : SQLite_Controller
	{
		//private SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_STATETableAdapter ta_UkolyState;
		//internal SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_STATETableAdapter Ta_UkolyState
		//{
		//    get
		//    {
		//        if (ta_UkolyState == null)
		//        {
		//            ta_UkolyState = new Fask.MST_W.SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_STATETableAdapter();
		//            ta_UkolyState.Connection = this.Connection;
		//        }
		//        return ta_UkolyState;
		//    }
		//}

		//private SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter ta_UkolyUziv;
		//internal SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter Ta_UkolyUziv
		//{
		//    get
		//    {
		//        if (ta_UkolyUziv == null)
		//        {
		//            ta_UkolyUziv = new Fask.MST_W.SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();
		//            ta_UkolyUziv.Connection = this.Connection;
		//        }
		//        return ta_UkolyUziv;
		//    }
		//}

		//private SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter ta_Ukol;
		//internal SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter Ta_Ukol
		//{
		//    get
		//    {
		//        if (ta_Ukol == null)
		//        {
		//            ta_Ukol = new Fask.MST_W.SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter();
		//            ta_Ukol.Connection = this.Connection;
		//        }
		//        return ta_Ukol;
		//    }
		//}

		//private SqlCEDBs.DataSets.UkolyTableAdapters.UkolyTableAdapter ta_Ukoly;
		//internal SqlCEDBs.DataSets.UkolyTableAdapters.UkolyTableAdapter Ta_Ukoly
		//{
		//    get
		//    {
		//        if (ta_Ukoly == null)
		//        {
		//            ta_Ukoly = new Fask.MST_W.SqlCEDBs.DataSets.UkolyTableAdapters.UkolyTableAdapter();
		//            ta_Ukoly.Connection = this.Connection;
		//        }
		//        return ta_Ukoly;
		//    }
		//}


		#region c'tors

		
		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="sqliteconnection">Spojeni na databazi davky ukoly</param>
		//public SQLite_Controller_Ukoly()
		//	: base(Main.CiselnikUkolyDB)
		//{
		//}

		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="sqliteconnection">Spojeni na databazi davky Ukoly</param>
		public SQLite_Controller_Ukoly(SQLiteConnection sqliteconnection)
			: base(sqliteconnection)
		{
		}

		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="sqliteConnectionstring">Conectionstring na databazi davky Ukoly</param>
		public SQLite_Controller_Ukoly(string sqliteFilename)
			: base(sqliteFilename)
		{
		}


		public override void Dispose()
		{
			//this.DisposeObject(ta_UkolyState);
			//this.DisposeObject(ta_UkolyUziv);
			//this.DisposeObject(ta_Ukol);
			//this.DisposeObject(ta_Ukoly);

			//ta_UkolyState = null;
			//ta_UkolyUziv = null;
			//ta_Ukol = null;
			//ta_Ukoly = null;


			base.Dispose();
		}

		#endregion

		#region Metody

		#region CZ_UKOL_STATE Fill a Get

		public int CZ_UKOL_STATE_Fill(SQLiteDBs.DataSets.Ukoly.CZ_UKOL_STATEDataTable dataTable)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZ_UKOL_STATE";

						int ReturnValue;
						ReturnValue = adapter.Fill(dataTable);

						return ReturnValue;
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

		public SQLiteDBs.DataSets.Ukoly.CZ_UKOL_STATEDataTable CZ_UKOL_STATE_GetData()
		{
			SQLiteDBs.DataSets.Ukoly.CZ_UKOL_STATEDataTable dataTable = new SQLiteDBs.DataSets.Ukoly.CZ_UKOL_STATEDataTable();
			this.CZ_UKOL_STATE_Fill(dataTable);
			return dataTable;
		}
		
		#endregion

		#region CZ_UKOL_UZIV Fill a Get ByUserID

		public int CZ_UKOL_UZIV_FillByUserID(SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable dataTable, int UserID)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZ_UKOL_UZIV WHERE (UserID = @UserID)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.Int32, Value = UserID });
						int ReturnValue;
						ReturnValue = adapter.Fill(dataTable);

						return ReturnValue;
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

		public SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable CZ_UKOL_UZIV_GetDataByUserID(int UserID)
		{
			SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable dataTable = new SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable();
			this.CZ_UKOL_UZIV_FillByUserID(dataTable, UserID);
			return dataTable;
		}
		
		#endregion

		#region CZ_UKOL_UZIV Fill a Get ByUserID

		public int CZ_UKOL_UZIV_FillByUserIDNew(SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable dataTable, int UserID)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT CZ_UKOL_UZIV.ID, CZ_UKOL_UZIV.UkolID, CZ_UKOL_UZIV.UserID, CZ_UKOL_UZIV.State," + 
							" CZ_UKOL_UZIV.DateChanged, CZ_UKOL_UZIV.UserIDChanged, CZ_UKOL_UZIV.Note, CZ_UKOL_UZIV.DateNotify, CZ_UKOL_UZIV.DateFinished " + 
							" FROM CZ_UKOL_UZIV " + 
							" INNER JOIN CZ_UKOL_STATE ON CZ_UKOL_UZIV.State = CZ_UKOL_STATE.State " +
							" WHERE (CZ_UKOL_STATE.IsStart = 1) AND (CZ_UKOL_UZIV.UserID = @UserID)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.Int32, Value = UserID });
						int ReturnValue;
						ReturnValue = adapter.Fill(dataTable);

						return ReturnValue;
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

		public SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable CZ_UKOL_UZIV_GetDataByUserIDNew(int UserID)
		{
			SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable dataTable = new SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable();
			this.CZ_UKOL_UZIV_FillByUserID(dataTable, UserID);
			return dataTable;
		}

		#endregion

		#region CZ_UKOL_UZIV Fill a Get ByID

		public int CZ_UKOL_UZIV_FillByID(SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable dataTable, int ID)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZ_UKOL_UZIV WHERE (ID = @ID)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, Value = ID });
						int ReturnValue;
						ReturnValue = adapter.Fill(dataTable);

						return ReturnValue;
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

		public SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable CZ_UKOL_UZIV_GetDataByID(int ID)
		{
			SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable dataTable = new SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVDataTable();
			this.CZ_UKOL_UZIV_FillByID(dataTable, ID);
			return dataTable;
		}

		#endregion

		#region CZ_UKOL Fill a Get ByUserID

		public int CZ_UKOL_FillByUserID(SQLiteDBs.DataSets.Ukoly.CZ_UKOLDataTable dataTable, int UserID)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZ_UKOL WHERE (UserID = @UserID)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.Int32, Value = UserID });
						int ReturnValue;
						ReturnValue = adapter.Fill(dataTable);

						return ReturnValue;
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

		public SQLiteDBs.DataSets.Ukoly.CZ_UKOLDataTable CZ_UKOL_GetDataByUserID(int UserID)
		{
			SQLiteDBs.DataSets.Ukoly.CZ_UKOLDataTable dataTable = new SQLiteDBs.DataSets.Ukoly.CZ_UKOLDataTable();
			this.CZ_UKOL_FillByUserID(dataTable, UserID);
			return dataTable;
		}

		#endregion

		#region CZ_UKOL Fill a Get ByID

		public int CZ_UKOL_FillByID(SQLiteDBs.DataSets.Ukoly.CZ_UKOLDataTable dataTable, int ID)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZ_UKOL WHERE (ID = @ID)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, Value = ID });
						int ReturnValue;
						ReturnValue = adapter.Fill(dataTable);

						return ReturnValue;
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

		public SQLiteDBs.DataSets.Ukoly.CZ_UKOLDataTable CZ_UKOL_GetDataByID(int ID)
		{
			SQLiteDBs.DataSets.Ukoly.CZ_UKOLDataTable dataTable = new SQLiteDBs.DataSets.Ukoly.CZ_UKOLDataTable();
			this.CZ_UKOL_FillByID(dataTable, ID);
			return dataTable;
		}
		
		#endregion

		#region Ukoly Fill a Get ByUserID

		public int Ukoly_FillByUserID(SQLiteDBs.DataSets.Ukoly.UkolyDataTable dataTable, int UserID)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText =
						   @" SELECT CZ_UKOL.ID, CZ_UKOL.Name, CZ_UKOL.Description, CZ_UKOL.Code, CZ_UKOL.CreatorID, CZ_UKOL.DateCreated, CZ_UKOL.DateFrom, CZ_UKOL.DateTo, " +
							" CZ_UKOL.State, CZ_UKOL.Kind, CZ_UKOL.Type, CZ_UKOL.Priority, CZ_UKOL.PartnerID, CZ_UKOL_UZIV.ID AS ID_Uziv, CZ_UKOL_UZIV.UkolID, " +
							" CZ_UKOL_UZIV.UserID, CZ_UKOL_UZIV.State AS State_Uziv, CZ_UKOL_UZIV.DateChanged, CZ_UKOL_UZIV.UserIDChanged, CZ_UKOL_UZIV.Note, " +
							" CZ_UKOL_UZIV.DateNotify, CZ_UKOL_UZIV.DateFinished, CZ_UKOL_STATE.State AS State_State, CZ_UKOL_STATE.Description AS State_Desc, " +
							" CZ_UKOL_STATE.IsStart, CZ_UKOL_STATE.IsEnd  " +
							" FROM CZ_UKOL INNER JOIN CZ_UKOL_UZIV ON CZ_UKOL.ID = CZ_UKOL_UZIV.UkolID " +
							" INNER JOIN CZ_UKOL_STATE ON CZ_UKOL_UZIV.State = CZ_UKOL_STATE.State " +
							" WHERE (CZ_UKOL_UZIV.UserID = @UserID)";

						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.Int32, Value = UserID });
						int ReturnValue;
						ReturnValue = adapter.Fill(dataTable);

						return ReturnValue;
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

		public SQLiteDBs.DataSets.Ukoly.UkolyDataTable Ukoly_GetDataByUserID(int UserID)
		{
			SQLiteDBs.DataSets.Ukoly.UkolyDataTable dataTable = new SQLiteDBs.DataSets.Ukoly.UkolyDataTable();
			this.Ukoly_FillByUserID(dataTable, UserID);
			return dataTable;
		}

		#endregion

		#region Update CZ_UKOL_UZIV

		public int CZ_UKOL_UZIV_Update(object data)
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
					InitializeCommandInsert_CZ_UKOL_UZIV(commandInsert);
					InitializeCommandUpdate_CZ_UKOL_UZIV(commandUpdate);
					InitializeCommandDelete_CZ_UKOL_UZIV(commandDelete);
					InitializeCommandSelect_CZ_UKOL_UZIV(commandSelect);

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

		public void InitializeCommandInsert_CZ_UKOL_UZIV(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZ_UKOL_UZIV " + 
				"(ID, UkolID, UserID, State, DateChanged, UserIDChanged, Note, DateNotify, DateFinished) " + 
				"VALUES " + 
				" (@ID,@UkolID,@UserID,@State,@DateChanged,@UserIDChanged,@Note,@DateNotify,@DateFinished)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UkolID", DbType = System.Data.DbType.Int32, SourceColumn = "UkolID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.Int32, SourceColumn = "UserID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@State", DbType = System.Data.DbType.String, SourceColumn = "State", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateChanged", DbType = System.Data.DbType.DateTime, SourceColumn = "DateChanged", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserIDChanged", DbType = System.Data.DbType.Int32, SourceColumn = "UserIDChanged", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Note", DbType = System.Data.DbType.String, SourceColumn = "Note", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateNotify", DbType = System.Data.DbType.DateTime, SourceColumn = "DateNotify", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateFinished", DbType = System.Data.DbType.DateTime, SourceColumn = "DateFinished", SourceVersion = DataRowVersion.Current });
		}

		public void InitializeCommandUpdate_CZ_UKOL_UZIV(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE CZ_UKOL_UZIV SET " + 
				" UkolID = @UkolID, UserID = @UserID, State = @State, DateChanged = @DateChanged, " + 
				" UserIDChanged = @UserIDChanged, Note = @Note, DateNotify = @DateNotify, DateFinished = @DateFinished " + 
				" WHERE (ID = @ID_O)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID" , SourceVersion = DataRowVersion.Current});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UkolID", DbType = System.Data.DbType.Int32, SourceColumn = "UkolID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.Int32, SourceColumn = "UserID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@State", DbType = System.Data.DbType.String, SourceColumn = "State", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateChanged", DbType = System.Data.DbType.DateTime, SourceColumn = "DateChanged", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserIDChanged", DbType = System.Data.DbType.Int32, SourceColumn = "UserIDChanged", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Note", DbType = System.Data.DbType.String, SourceColumn = "Note", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateNotify", DbType = System.Data.DbType.DateTime, SourceColumn = "DateNotify", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateFinished", DbType = System.Data.DbType.DateTime, SourceColumn = "DateFinished", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_O", DbType = System.Data.DbType.Int32, SourceColumn = "ID", SourceVersion = DataRowVersion.Original });
		}

		public void InitializeCommandDelete_CZ_UKOL_UZIV(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZ_UKOL_UZIV WHERE (ID = @ID)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@ID",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "ID",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_CZ_UKOL_UZIV(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZ_UKOL_UZIV";
		}


		#endregion

		#endregion

		#region Update CZ_UKOL

		public int CZ_UKOL_Update(object data)
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
					InitializeCommandInsert_CZ_UKOL(commandInsert);
					InitializeCommandUpdate_CZ_UKOL(commandUpdate);
					InitializeCommandDelete_CZ_UKOL(commandDelete);
					InitializeCommandSelect_CZ_UKOL(commandSelect);

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

		public void InitializeCommandInsert_CZ_UKOL(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZ_UKOL " + 
				" ([ID], [Name], [Description], [Code], [CreatorID], [DateCreated], [DateFrom], [DateTo], [State], [Kind], [Type], [Priority], [PartnerID]) " +
				" VALUES (@ID ,@Name, @Description,  @Code,  @CreatorID,  @DateCreated,  @DateFrom,  @DateTo,  @State,  @Kind,  @Type,  @Priority,  @PartnerID)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Name", DbType = System.Data.DbType.String, SourceColumn = "Name", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, SourceColumn = "Description", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Code", DbType = System.Data.DbType.Int32, SourceColumn = "Code", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CreatorID", DbType = System.Data.DbType.String, SourceColumn = "CreatorID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateCreated", DbType = System.Data.DbType.DateTime, SourceColumn = "DateCreated", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateFrom", DbType = System.Data.DbType.DateTime, SourceColumn = "DateFrom", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateTo", DbType = System.Data.DbType.DateTime, SourceColumn = "DateTo", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@State", DbType = System.Data.DbType.String, SourceColumn = "State", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Kind", DbType = System.Data.DbType.String, SourceColumn = "Kind", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Type", DbType = System.Data.DbType.String, SourceColumn = "Type", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Priority", DbType = System.Data.DbType.Int32, SourceColumn = "Priority", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PartnerID", DbType = System.Data.DbType.String, SourceColumn = "PartnerID", SourceVersion = DataRowVersion.Current });
		}

		public void InitializeCommandUpdate_CZ_UKOL(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE [CZ_UKOL] SET " +
				" [ID] = @ID," +
				" [Name] = @Name," +
				" [Description] = @Description," +
				" [Code] = @Code," +
				" [CreatorID] = @CreatorID," +
				" [DateCreated] = @DateCreated," +
				" [DateFrom] = @DateFrom," +
				" [DateTo] = @DateTo," +
				" [State] = @State," +
				" [Kind] = @Kind," +
				" [Type] = @Type," +
				" [Priority] = @Priority," +
				" [PartnerID] = @PartnerID " +
				" WHERE (([ID] = @ID_O))";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID", DbType = System.Data.DbType.Int32, SourceColumn = "ID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Name", DbType = System.Data.DbType.String, SourceColumn = "Name", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, SourceColumn = "Description", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Code", DbType = System.Data.DbType.Int32, SourceColumn = "Code", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CreatorID", DbType = System.Data.DbType.String, SourceColumn = "CreatorID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateCreated", DbType = System.Data.DbType.DateTime, SourceColumn = "DateCreated", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateFrom", DbType = System.Data.DbType.DateTime, SourceColumn = "DateFrom", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DateTo", DbType = System.Data.DbType.DateTime, SourceColumn = "DateTo", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@State", DbType = System.Data.DbType.String, SourceColumn = "State", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Kind", DbType = System.Data.DbType.String, SourceColumn = "Kind", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Type", DbType = System.Data.DbType.String, SourceColumn = "Type", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Priority", DbType = System.Data.DbType.Int32, SourceColumn = "Priority", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PartnerID", DbType = System.Data.DbType.String, SourceColumn = "PartnerID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_O", DbType = System.Data.DbType.Int32, SourceColumn = "ID", SourceVersion = DataRowVersion.Original });
		}

		public void InitializeCommandDelete_CZ_UKOL(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZ_UKOL WHERE (ID = @ID)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@ID",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "ID",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_CZ_UKOL(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZ_UKOL";
		}


		#endregion


		#endregion

		#region Update CZ_UKOL_STATE

		public int CZ_UKOL_STATE_Update(object data)
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
					InitializeCommandInsert_CZ_UKOL(commandInsert);
					InitializeCommandUpdate_CZ_UKOL(commandUpdate);
					InitializeCommandDelete_CZ_UKOL(commandDelete);
					InitializeCommandSelect_CZ_UKOL(commandSelect);

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

		public void InitializeCommandInsert_CZ_UKOL_STATE(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [CZ_UKOL_STATE] " + 
				" ([State], [Description], [IsStart], [IsEnd]) " + 
				" VALUES " +
				" (@State, @Description, @IsStart, @IsEnd)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@State", DbType = System.Data.DbType.String, SourceColumn = "State", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, SourceColumn = "Description", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IsStart", DbType = System.Data.DbType.Boolean, SourceColumn = "IsStart", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IsEnd", DbType = System.Data.DbType.Boolean, SourceColumn = "IsEnd", SourceVersion = DataRowVersion.Current });			
		}

		public void InitializeCommandUpdate_CZ_UKOL_STATE(SQLiteCommand command)
		{
			command.CommandText = "UPDATE [CZ_UKOL_STATE] SET " +
				" [State] = @State," +
				" [Description] = @Description," +
				" [IsStart] = @IsStart," +
				" [IsEnd] = @IsEnd" +
				" WHERE (([State] = @State_O))";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@State", DbType = System.Data.DbType.String, SourceColumn = "State", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, SourceColumn = "Description", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IsStart", DbType = System.Data.DbType.Boolean, SourceColumn = "IsStart", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IsEnd", DbType = System.Data.DbType.Boolean, SourceColumn = "IsEnd", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@State_O", DbType = System.Data.DbType.String, SourceColumn = "State", SourceVersion = DataRowVersion.Original });
		}

		public void InitializeCommandDelete_CZ_UKOL_STATE(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZ_UKOL_STATE WHERE (State = @State)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@State",
				DbType = System.Data.DbType.String,
				SourceColumn = "State",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_CZ_UKOL_STATE(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZ_UKOL_STATE";
		}


		#endregion


		#endregion

		#endregion

	}
}
