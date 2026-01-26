using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Fask.SQLiteDBs.Controllers
{
	/// <summary>
	/// Base trida pro controllery
	/// </summary>
	public class SQLite_Controller_Inventura1 : SQLite_Controller
	{
		#region c'tors
		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="sqliteconnection">Spojeni na databazi davky prijmu</param>
		public SQLite_Controller_Inventura1(SQLiteConnection sqliteconnection)
			: base(sqliteconnection)
		{
		}

		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="sqliteConnectionstring">Conectionstring na databazi davky prijmu</param>
		public SQLite_Controller_Inventura1(string sqliteFilename)
			: base(sqliteFilename)
		{
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		#endregion

		#region tableadapters methods

		#region CZMST_IH

		private void CZMST_IH_InitializeCommandInsert(SQLiteCommand command)
		{
			command.CommandText =
				"INSERT INTO CZMST_IH" +
				"(CountEntries, GUID) VALUES (@CountEntries, @GUID)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
		}
		private void CZMST_IH_InitializeCommandUpdate(SQLiteCommand command)
		{
			command.CommandText =
				"UPDATE CZMST_IH" +
				" SET CountEntries=@CountEntries" +
				" WHERE GUID=@GUID";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
		}
		private void CZMST_IH_InitializeCommandDelete(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST_IH WHERE GUID=@GUID";
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
		}

		private void CZMST_IH_InitializeCommandSelect(SQLiteCommand command)
		{
			command.CommandText = "SELECT * FROM CZMST_IH";
		}

		public int Update_IH(object data)
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
					CZMST_IH_InitializeCommandInsert(commandInsert);
					CZMST_IH_InitializeCommandUpdate(commandUpdate);
					CZMST_IH_InitializeCommandDelete(commandDelete);
					CZMST_IH_InitializeCommandSelect(commandSelect);

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

		public int Fill_IH(System.Data.DataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_IH";
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

		public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_IHDataTable GetData_IH()
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_IHDataTable();
			this.Fill_IH(dataTable);
			return dataTable;
		}

		#endregion

		#region CZMST_I1H

		//private void CZMST_I1H_InitializeCommandInsert(SQLiteCommand command)
		//{
		//    command.CommandText =
		//        "INSERT INTO CZMST_I1H (CountEntries, Description, Status) " +
		//        " VALUES " +
		//        " (@CountEntries, @Description, @Status)";
		//    global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@CountEntries";
		//    param.SourceColumn = "CountEntries";
		//    command.Parameters.Add(param);
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@Description";
		//    param.SourceColumn = "Description";
		//    command.Parameters.Add(param);
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@Status";
		//    param.SourceColumn = "Status";
		//    command.Parameters.Add(param);
		//}

		//private void CZMST_I1H_InitializeCommandUpdate(SQLiteCommand command)
		//{
		//    command.CommandText =
		//        "Update CZMST_I1H" +
		//        " Set Description=@Description, Status=@Status" +
		//        " Where CountEntries=@CountEntries";
		//    global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@Description";
		//    param.SourceColumn = "Description";
		//    command.Parameters.Add(param);
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@Status";
		//    param.SourceColumn = "Status";
		//    command.Parameters.Add(param);
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@CountEntries";
		//    param.SourceColumn = "CountEntries";
		//    param.SourceVersion = System.Data.DataRowVersion.Original;
		//    command.Parameters.Add(param);
		//}

		//private void CZMST_I1H_InitializeCommandDelete(SQLiteCommand command)
		//{
		//    command.CommandText =
		//        "Delete from CZMST_I1H" +
		//        " Where CountEntries=@CountEntries";
		//    global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@CountEntries";
		//    param.SourceColumn = "CountEntries";
		//    command.Parameters.Add(param);
		//}

		//private void CZMST_I1H_InitializeCommandSelect(SQLiteCommand command)
		//{
		//    command.CommandText =
		//        "Select * from CZMST_I1H" +
		//        " Where CountEntries=@CountEntries";
		//    global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@CountEntries";
		//    param.SourceColumn = "CountEntries";
		//    command.Parameters.Add(param);
		//}

		//public int CZMST_I1H_Update(object data)
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
		//            CZMST_I1H_InitializeCommandInsert(commandInsert);
		//            CZMST_I1H_InitializeCommandUpdate(commandUpdate);
		//            CZMST_I1H_InitializeCommandDelete(commandDelete);
		//            CZMST_I1H_InitializeCommandSelect(commandSelect);

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
		//        Logging.ExceptionHandler2.Handle(ex);

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

		public int Fill_I1H(System.Data.DataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_I1H";
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

		public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1HDataTable GetData_I1H()
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1HDataTable();
			this.Fill_I1H(dataTable);
			return dataTable;
		}

		public int Update_I1H(object data, SQLiteConnection connection, SQLiteTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_I1H(commandInsert);
					InitializeCommandSelect_I1H(commandSelect);

					using (var adapter = new SQLiteDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


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

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		public void InitializeCommandInsert_I1H(SQLiteCommand command)
		{
			command.CommandText = string.Empty;
			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I1H + " ON ";

			command.CommandText +=
				" INSERT INTO CZMST_I1H " +
				" ( CountEntries, Description, Status ) " +
				" VALUES ( @CountEntries, @Description, @Status ) ";

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I1H + " OFF ";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CE_Orig", DbType = System.Data.DbType.Int32, SourceColumn = "CE_Orig", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, SourceColumn = "Description", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Status", DbType = System.Data.DbType.Byte, SourceColumn = "State", SourceVersion = System.Data.DataRowVersion.Current });

		}

		public void InitializeCommandSelect_I1H(SQLiteCommand command)
		{
			command.CommandText = "SELECT * FROM CZMST_I1H " ;
		}


		#endregion


		#endregion

		#region CZMST_I1

		//        private void CZMST_I1_InitializeCommandInsert(SQLiteCommand command)
		//        {
		//            command.CommandText = @"INSERT INTO CZMST_I1
		//		(CountEntries, ITEMNMBR, CZ_CarKod, ITEMDESC, LOCNCODE, QUANTITY, DATEDONE, IntegerValue, TIMESPRT, CZ_SerNum_Track, CZ_SerNum_Find, 
		//		DEX_ROW_ID, TerminalID, O_TID, skl_id, DMJ, REZ_1, REZ_2, ITEMCODE, CZ_REZ1_Track, CZ_REZ2_Track)
		//		VALUES 
		//		(@CountEntries,@ITEMNMBR,@CZ_CarKod,@ITEMDESC,@LOCNCODE,@QUANTITY,@DATEDONE,@IntegerValue,@TIMESPRT,@CZ_SerNum_Track,@CZ_SerNum_Find,
		//		@DEX_ROW_ID,@TerminalID,@O_TID,@skl_id,@DMJ,@REZ_1,@REZ_2,@ITEMCODE,@CZ_REZ_1_Track,@CZ_REZ_2_Track)";
		//            global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@CountEntries";
		//            param.DbType = global::System.Data.DbType.Int32;
		//            param.SourceColumn = "CountEntries";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@ITEMNMBR";
		//            param.Size = 31;
		//            param.SourceColumn = "ITEMNMBR";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@CZ_CarKod";
		//            param.Size = 31;
		//            param.SourceColumn = "CZ_CarKod";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@ITEMDESC";
		//            param.SourceColumn = "ITEMDESC";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@LOCNCODE";
		//            param.SourceColumn = "LOCNCODE";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@QUANTITY";
		//            param.DbType = global::System.Data.DbType.Decimal;
		//            param.DbType = global::System.Data.DbType.Decimal;
		//            param.SourceColumn = "QUANTITY";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@DATEDONE";
		//            param.DbType = global::System.Data.DbType.DateTime;
		//            param.DbType = global::System.Data.DbType.DateTime;
		//            param.SourceColumn = "DATEDONE";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@IntegerValue";
		//            param.DbType = global::System.Data.DbType.Int16;
		//            param.SourceColumn = "IntegerValue";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@TIMESPRT";
		//            param.DbType = global::System.Data.DbType.Int16;
		//            param.SourceColumn = "TIMESPRT";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@CZ_SerNum_Track";
		//            param.DbType = global::System.Data.DbType.Byte;
		//            param.SourceColumn = "CZ_SerNum_Track";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@CZ_SerNum_Find";
		//            param.DbType = global::System.Data.DbType.Byte;
		//            param.SourceColumn = "CZ_SerNum_Find";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@DEX_ROW_ID";
		//            param.DbType = global::System.Data.DbType.Int32;
		//            param.SourceColumn = "DEX_ROW_ID";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@TerminalID";
		//            param.DbType = global::System.Data.DbType.Byte;
		//            param.SourceColumn = "TerminalID";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@O_TID";
		//            param.DbType = global::System.Data.DbType.Byte;
		//            param.SourceColumn = "O_TID";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@skl_id";
		//            param.SourceColumn = "skl_id";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@DMJ";
		//            param.SourceColumn = "DMJ";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@REZ_1";
		//            param.SourceColumn = "REZ_1";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@REZ_2";
		//            param.SourceColumn = "REZ_2";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@ITEMCODE";
		//            param.SourceColumn = "ITEMCODE";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@CZ_REZ_1_Track";
		//            param.DbType = global::System.Data.DbType.Byte;
		//            param.SourceColumn = "CZ_REZ_1_Track";
		//            command.Parameters.Add(param);
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@CZ_REZ_2_Track";
		//            param.DbType = global::System.Data.DbType.Byte;
		//            param.SourceColumn = "CZ_REZ_2_Track";
		//            command.Parameters.Add(param);
		//        }

		//        private void CZMST_I1_InitializeCommandUpdate(SQLiteCommand command)
		//        {
		//        }

		//        private void CZMST_I1_InitializeCommandDelete(SQLiteCommand command)
		//        {
		//        }

		//        private void CZMST_I1_InitializeCommandSelect(SQLiteCommand command)
		//        {
		//            command.CommandText =
		//                "Select * from CZMST_I1" +
		//                " Where Itemnmbr=@Itemnmbr";
		//            global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
		//            param = new global::System.Data.SQLite.SQLiteParameter();
		//            param.ParameterName = "@Itemnmbr";
		//            param.SourceColumn = "Itemnmbr";
		//            command.Parameters.Add(param);
		//        }

		//        public int CZMST_I1_Update(object data)
		//        {
		//            SQLiteTransaction transaction = null;
		//            try
		//            {
		//                int result = 0;
		//                Connection_Open();

		//                transaction = this.Connection.BeginTransaction();

		//                using (var commandInsert = this.Connection.CreateCommand())
		//                //using (var commandUpdate = this.Connection.CreateCommand())
		//                //using (var commandDelete = this.Connection.CreateCommand())
		//                using (var commandSelect = this.Connection.CreateCommand())
		//                {
		//                    CZMST_I1_InitializeCommandInsert(commandInsert);
		//                    //CZMST_I1_InitializeCommandUpdate(commandUpdate);
		//                    //CZMST_I1_InitializeCommandDelete(commandDelete);
		//                    CZMST_I1_InitializeCommandSelect(commandSelect);

		//                    using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
		//                    {
		//                        adapter.InsertCommand = commandInsert;
		//                        //adapter.UpdateCommand = commandUpdate;
		//                        //adapter.DeleteCommand = commandDelete;
		//                        adapter.SelectCommand = commandSelect;

		//                        var dataIsDataSet = data as System.Data.DataSet;
		//                        var dataIsDataTable = data as System.Data.DataTable;
		//                        var dataIsDataRow = data as System.Data.DataRow;
		//                        var dataIsDataRowArray = data as System.Data.DataRow[];

		//                        //if (data is System.Data.DataSet)
		//                        if (dataIsDataSet != null)
		//                            result = adapter.Update(dataIsDataSet, dataIsDataSet.Tables[0].TableName);
		//                        else if (dataIsDataTable != null)
		//                            result = adapter.Update(dataIsDataTable);
		//                        else if (dataIsDataRow != null)
		//                            result = adapter.Update(new System.Data.DataRow[] { dataIsDataRow });
		//                        else if (dataIsDataRowArray != null)
		//                            result = adapter.Update(dataIsDataRowArray);
		//                        else
		//                            throw new Exception(String.Format("Neodpovídající datový typ: {0}", data.GetType().ToString()));
		//                    }
		//                }

		//                transaction.Commit();
		//                return result;
		//            }
		//            catch (Exception ex)
		//            {
		//                Logging.ExceptionHandler2.Handle(ex);

		//                try
		//                {
		//                    if (transaction != null)
		//                        transaction.Rollback();
		//                }
		//                catch (Exception exTransaction)
		//                {
		//                    Logging.Log.Write(exTransaction);
		//                }

		//                throw ex;
		//            }
		//            finally
		//            {
		//                Connection_Close();
		//            }
		//        }

		//public int CZMST_I1_Fill(System.Data.DataTable dataTable)
		//{
		//    try
		//    {
		//        Connection_Open();
		//        using (var command = this.Connection.CreateCommand())
		//        {
		//            command.CommandText =
		//                "SELECT * FROM CZMST_I1";
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
		//        Logging.ExceptionHandler2.Handle(ex);
		//        throw ex;
		//    }
		//    finally
		//    {
		//        Connection_Close();
		//    }
		//}

		//public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1HDataTable CZMST_I1_GetData()
		//{
		//    var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1HDataTable();
		//    this.CZMST_I1_Fill(dataTable);
		//    return dataTable;
		//}

		public int FillByITEMCODE_I1(System.Data.DataTable dataTable, string ITEMCODE)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText ="SELECT * FROM CZMST_I1 WHERE (ITEMCODE = @ITEMCODE)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE });

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

		public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1DataTable CZMST_I1_GetDataByITEMCODE(string itemcode)
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1DataTable();
			this.FillByITEMCODE_I1(dataTable, itemcode);
			return dataTable;
		}

		public int FillByITEMNMBR_I1(System.Data.DataTable dataTable, string ITEMNMBR)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_I1 WHERE (ITEMNMBR = @ITEMNMBR)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
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

		public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1DataTable GetDataByITEMNMBR_I1(string ITEMNMBR)
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1DataTable();
			this.FillByITEMNMBR_I1(dataTable, ITEMNMBR);
			return dataTable;
		}

		public string NazevPolozky_I1(string ITEMNMBR)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ITEMDESC FROM CZMST_I1 WHERE (ITEMNMBR = @ITEMNMBR) LIMIT 1";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
					object o = command.ExecuteScalar();

					string s = null;

					if (o is string)
						s = o as string;

					return s;
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

		public int Fill_I1(System.Data.DataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_I1";
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

		public int Update_I1(object data, SQLiteConnection connection, SQLiteTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_I1(commandInsert);
					InitializeCommandSelect_I1(commandSelect);

					using (var adapter = new SQLiteDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


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

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		public void InitializeCommandInsert_I1(SQLiteCommand command)
		{
			command.CommandText = string.Empty;

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I1 + " ON ";

			//command.CommandText += @"INSERT INTO CZMST_I1" +
			//	" ([CountEntries], [CE_Orig], [ITEMNMBR], [CZ_CarKod], [ITEMDESC]," +
			//	" [LOCNCODE], [SKL_ID], [QUANTITY], [DMJ], [DATEDONE]," +
			//	" [IntegerValue], [TIMESPRT], [CZ_SerNum_Track], [CZ_SerNum_Find], [TerminalID]," +
			//	" [O_TID], [REZ_1], [REZ_2], [ITEMCODE], [CZ_REZ1_Track], [CZ_REZ2_Track]," +
			//	" [CZ_Expirace_Track]) " +
			//	" VALUES " +
			//	" (@CountEntries, @CE_Orig, @ITEMNMBR, @CZ_CarKod, @ITEMDESC," +
			//	" @LOCNCODE, @SKL_ID, @QUANTITY, @DMJ, @DATEDONE," +
			//	" @IntegerValue, @TIMESPRT, @CZ_SerNum_Track, @CZ_SerNum_Find, @TerminalID," +
			//	" @O_TID, @REZ_1, @REZ_2, @ITEMCODE, @CZ_REZ1_Track, @CZ_REZ2_Track, " +
			//	" @CZ_Expirace_Track)";

			command.CommandText += @"INSERT INTO CZMST_I1" +
								" ([CountEntries], [ITEMNMBR], [CZ_CarKod], [ITEMDESC]," +
								" [LOCNCODE], [SKL_ID], [QUANTITY], [DMJ], [DATEDONE]," +
								" [IntegerValue], [TIMESPRT], [CZ_SerNum_Track], [CZ_SerNum_Find], [TerminalID]," +
								" [O_TID], [REZ_1], [REZ_2], [ITEMCODE], [CZ_REZ1_Track], [CZ_REZ2_Track]," +
								" [CZ_Expirace_Track], [DEX_ROW_ID]) " +
								" VALUES " +
								" (@CountEntries, @ITEMNMBR, @CZ_CarKod, @ITEMDESC," +
								" @LOCNCODE, @SKL_ID, @QUANTITY, @DMJ, @DATEDONE," +
								" @IntegerValue, @TIMESPRT, @CZ_SerNum_Track, @CZ_SerNum_Find, @TerminalID," +
								" @O_TID, @REZ_1, @REZ_2, @ITEMCODE, @CZ_REZ1_Track, @CZ_REZ2_Track, " +
								" @CZ_Expirace_Track, @DEX_ROW_ID)";


			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I1 + " OFF ";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CE_Orig", DbType = System.Data.DbType.Int32, SourceColumn = "CE_Orig", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QUANTITY", DbType = System.Data.DbType.Decimal, SourceColumn = "QUANTITY", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DMJ", DbType = System.Data.DbType.String, SourceColumn = "DMJ", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.DateTime, SourceColumn = "DATEDONE", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IntegerValue", DbType = System.Data.DbType.Int16, SourceColumn = "IntegerValue", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMESPRT", DbType = System.Data.DbType.Int16, SourceColumn = "TIMESPRT", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Find", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Find", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TerminalID", DbType = System.Data.DbType.Byte, SourceColumn = "TerminalID", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_TID", DbType = System.Data.DbType.Byte, SourceColumn = "O_TID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ1_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ2_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_Track", SourceVersion = System.Data.DataRowVersion.Current });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Expirace_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Expirace_Track", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = System.Data.DataRowVersion.Current });

		}

		public void InitializeCommandSelect_I1(SQLiteCommand command)
		{
			command.CommandText = "SELECT * FROM CZMST_I1";
		}


		#endregion


		#endregion

		#region CZMST_I2

		//private void CZMST_I2_InitializeCommandInsert(SQLiteCommand command)
		//{
		//    command.CommandText = @"INSERT INTO CZMST_I2 (CountEntries, ITEMNMBR, SERLNMBR, DEX_ROW_ID, QTY) " + 
		//        " VALUES " + 
		//        "(@CountEntries,@ITEMNMBR,@SERLNMBR,@DEX_ROW_ID,@QTY)";
		//    global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@CountEntries";
		//    param.DbType = global::System.Data.DbType.Int32;
		//    param.SourceColumn = "CountEntries";
		//    command.Parameters.Add(param);
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@ITEMNMBR";
		//    param.Size = 31;
		//    param.SourceColumn = "ITEMNMBR";
		//    command.Parameters.Add(param);
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@SERLNMBR";
		//    param.SourceColumn = "SERLNMBR";
		//    command.Parameters.Add(param);
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@DEX_ROW_ID";
		//    param.DbType = global::System.Data.DbType.Int32;
		//    param.SourceColumn = "DEX_ROW_ID";
		//    command.Parameters.Add(param);
		//    param = new global::System.Data.SQLite.SQLiteParameter();
		//    param.ParameterName = "@QTY";
		//    param.DbType = global::System.Data.DbType.Decimal;
		//    param.DbType = global::System.Data.DbType.Decimal;
		//    param.SourceColumn = "QTY";
		//    command.Parameters.Add(param);
		//}
		//private void CZMST_I2_InitializeCommandUpdate(SQLiteCommand command)
		//{
		//}
		//private void CZMST_I2_InitializeCommandDelete(SQLiteCommand command)
		//{
		//}
		//private void CZMST_I2_InitializeCommandSelect(SQLiteCommand command)
		//{
		//}

		//public int CZMST_I2_Update(object data)
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
		//        //using (var commandSelect = this.Connection.CreateCommand())
		//        {
		//            CZMST_I2_InitializeCommandInsert(commandInsert);
		//            //CZMST_I2_InitializeCommandUpdate(commandUpdate);
		//            //CZMST_I2_InitializeCommandDelete(commandDelete);
		//            //CZMST_I2_InitializeCommandSelect(commandSelect);

		//            using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
		//            {
		//                adapter.InsertCommand = commandInsert;
		//                //adapter.UpdateCommand = commandUpdate;
		//                //adapter.DeleteCommand = commandDelete;
		//                //adapter.SelectCommand = commandSelect;

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
		//        Logging.ExceptionHandler2.Handle(ex);

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

		//public int CZMST_I2_Fill(System.Data.DataTable dataTable)
		//{
		//    try
		//    {
		//        Connection_Open();
		//        using (var command = this.Connection.CreateCommand())
		//        {
		//            command.CommandText =
		//                "SELECT * FROM CZMST_I2";
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
		//        Logging.ExceptionHandler2.Handle(ex);
		//        throw ex;
		//    }
		//    finally
		//    {
		//        Connection_Close();
		//    }
		//}
		//public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I2DataTable CZMST_I2_GetData()
		//{
		//    var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I2DataTable();
		//    this.CZMST_I2_Fill(dataTable);
		//    return dataTable;
		//}

		public int FillByITEMNMBR_I2(System.Data.DataTable dataTable, string ITEMNMBR)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM CZMST_I2 WHERE (ITEMNMBR=@ITEMNMBR)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });

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

		public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I2DataTable GetDataByITEMNMBR_I2(string ITEMNMBR)
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I2DataTable();
			this.FillByITEMNMBR_I2(dataTable, ITEMNMBR);
			return dataTable;
		}

		public int FillBySERLNMBR_I2(System.Data.DataTable dataTable, string SERLNMBR)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT * FROM CZMST_I2 WHERE (SERLNMBR=@SERLNMBR)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLNMBR", DbType = System.Data.DbType.String, Value = SERLNMBR == null ? (object)DBNull.Value : SERLNMBR });
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
		
		public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I2DataTable GetDataBySERLNMBR_I2(string SERLNMBR)
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I2DataTable();
			this.FillBySERLNMBR_I2(dataTable, SERLNMBR);
			return dataTable;
		}

		//        public object CZMST_I2_SNCount(string itemnmbr, string serlnmbr)
		//        {
		//            try
		//            {
		//                Connection_Open();

		//                using (var command = this.Connection.CreateCommand())
		//                {
		//                    command.CommandText =
		//@"SELECT COUNT(SERLNMBR) AS CntSN
		//		FROM CZMST_I2
		//		WHERE (ITEMNMBR = @ITEMNMBR) AND (SERLNMBR = @SERLNMBR)";
		//                    var param = new global::System.Data.SQLite.SQLiteParameter();
		//                    param.ParameterName = "@ITEMNMBR";
		//                    param.SourceColumn = "ITEMNMBR";
		//                    param.Value = itemnmbr;
		//                    command.Parameters.Add(param);
		//                    param = new global::System.Data.SQLite.SQLiteParameter();
		//                    param.ParameterName = "@SERLNMBR";
		//                    param.SourceColumn = "SERLNMBR";
		//                    param.Value = serlnmbr;
		//                    command.Parameters.Add(param);

		//                    object o = command.ExecuteScalar();
		//                    return o;
		//                }
		//            }
		//            catch (Exception ex)
		//            {
		//                Logging.ExceptionHandler2.Handle(ex);
		//                throw ex;
		//            }
		//            finally
		//            {
		//                Connection_Close();
		//            }
		//        }

		public int Fill_I2(System.Data.DataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_I2";
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

		public int Update_I2(object data, SQLiteConnection connection, SQLiteTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_I2(commandInsert);
					InitializeCommandSelect_I2(commandSelect);

					using (var adapter = new SQLiteDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


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

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		public void InitializeCommandInsert_I2(SQLiteCommand command)
		{
			command.CommandText = string.Empty;
			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I3 + " ON ";

			command.CommandText += "INSERT INTO CZMST_I2 " +
				"([CountEntries], [DEX_ROW_ID], [ITEMNMBR], [SERLNMBR], [QTY], [Expirace]) " +
				" VALUES " +
				" (@CountEntries, @DEX_ROW_ID, @ITEMNMBR, @SERLNMBR, @QTY, @Expirace)";

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I3 + " OFF ";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLNMBR", DbType = System.Data.DbType.String, SourceColumn = "SERLNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTY", DbType = System.Data.DbType.Decimal, SourceColumn = "QTY", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace", SourceVersion = System.Data.DataRowVersion.Current });			


		}

		public void InitializeCommandSelect_I2(SQLiteCommand command)
		{
			command.CommandText = "SELECT * FROM CZMST_I2";
		}

		#endregion


		#endregion

		#region CZMST_I3

		//private void CZMST_I3_InitializeCommandInsert(SQLiteCommand command)
		//{
		//}
		//private void CZMST_I3_InitializeCommandUpdate(SQLiteCommand command)
		//{
		//}
		//private void CZMST_I3_InitializeCommandDelete(SQLiteCommand command)
		//{
		//}
		//private void CZMST_I3_InitializeCommandSelect(SQLiteCommand command)
		//{
		//}

		//public int CZMST_I3_Update(object data)
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
		//            CZMST_I3_InitializeCommandInsert(commandInsert);
		//            CZMST_I3_InitializeCommandUpdate(commandUpdate);
		//            CZMST_I3_InitializeCommandDelete(commandDelete);
		//            CZMST_I3_InitializeCommandSelect(commandSelect);

		//            using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
		//            {
		//                adapter.InsertCommand = commandInsert;
		//                adapter.UpdateCommand = commandUpdate;
		//                adapter.DeleteCommand = commandDelete;
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
		//        Logging.ExceptionHandler2.Handle(ex);

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

		//public int CZMST_I3_Fill(System.Data.DataTable dataTable)
		//{
		//    try
		//    {
		//        Connection_Open();
		//        using (var command = this.Connection.CreateCommand())
		//        {
		//            command.CommandText =
		//                "SELECT * FROM CZMST_I3";
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
		//        Logging.ExceptionHandler2.Handle(ex);
		//        throw ex;
		//    }
		//    finally
		//    {
		//        Connection_Close();
		//    }
		//}
		//public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3DataTable CZMST_I3_GetData()
		//{
		//    var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3DataTable();
		//    this.CZMST_I3_Fill(dataTable);
		//    return dataTable;
		//}

		public int FillByCarcode_I3(System.Data.DataTable dataTable, string carcode)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT i3.*, i1.ITEMDESC " + 
						" FROM CZMST_I3 AS i3 " + 
						" LEFT OUTER JOIN CZMST_I1 AS i1 ON i1.CountEntries = i3.CountEntries AND i1.ITEMNMBR = i3.ITEMNMBR " + 
						" WHERE (i3.VNDITNUM = @carcode) " + 
						" UNION" + 
						" SELECT i3.*, i1.ITEMDESC FROM CZMST_I3 AS i3 " + 
						" LEFT OUTER JOIN CZMST_I1 AS i1 ON i1.CountEntries = i3.CountEntries AND i1.ITEMNMBR = i3.ITEMNMBR " + 
						" WHERE (i3.CZ_CarKod = @carcode)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@carcode", DbType = System.Data.DbType.String, Value = carcode == null ? (object)DBNull.Value : carcode });

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
		public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3DataTable GetDataByCarcode_I3(string carcode)
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3DataTable();
			this.FillByCarcode_I3(dataTable, carcode);
			return dataTable;
		}

		public int FillByITEMNMBR_I3(System.Data.DataTable dataTable, string ITEMNMBR)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT     i3.*, i1.ITEMDESC FROM CZMST_I3 as i3 " + 
						" INNER JOIN CZMST_I1 i1 ON i3.CountEntries = i1.CountEntries AND i3.ITEMNMBR = i1.ITEMNMBR " + 
						" WHERE (i3.ITEMNMBR = @ITEMNMBR)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });

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
		public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3DataTable GetDataByITEMNMBR_I3(string ITEMNMBR)
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I3DataTable();
			this.FillByITEMNMBR_I3(dataTable, ITEMNMBR);
			return dataTable;
		}

		public int Fill_I3(System.Data.DataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_I3";
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

		public int Update_I3(object data, SQLiteConnection connection, SQLiteTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_I3(commandInsert);
					InitializeCommandSelect_I3(commandSelect);

					using (var adapter = new SQLiteDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


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

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}

		#region Inicialize metody

		public void InitializeCommandInsert_I3(SQLiteCommand command)
		{
			command.CommandText = string.Empty;
			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I3 + " ON ";

			command.CommandText += "INSERT INTO CZMST_I3 " +
				"([CountEntries], [DEX_ROW_ID], [ITEMNMBR], [CZ_CarKod], [QTYPACK], [MJ]," +
				" [VENDORID], [VNDITNUM], [VENDNAME], [WEIGHT]) " +
				" VALUES " +
				" (@CountEntries, @DEX_ROW_ID, @ITEMNMBR, @CZ_CarKod, @QTYPACK, @MJ," +
				" @VENDORID, @VNDITNUM, @VENDNAME, @WEIGHT)";

			//command.CommandText += " SET IDENTITY_INSERT " + Constants.Common.TABLE_CZMST_I3 + " OFF ";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = System.Data.DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VENDORID", DbType = System.Data.DbType.String, SourceColumn = "VENDORID", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VENDNAME", DbType = System.Data.DbType.String, SourceColumn = "VENDNAME", SourceVersion = System.Data.DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = System.Data.DataRowVersion.Current });

		}

		public void InitializeCommandSelect_I3(SQLiteCommand command)
		{
			command.CommandText = "SELECT * FROM CZMST_I3 ";
		}

		#endregion


		#endregion

		#region CZMST_I4

		private void CZMST_I4_InitializeCommandInsert(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO CZMST_I4 (CountEntries, ITEMNMBR, CZ_CarKod, LOCNCODE, VNDITNUM, QUANTITY, QTYPACK, SERLNMBR, DATEDONE, TIMEDONE, USERID, DEX_ROW_ID, GUID, O_Checked, skl_id, MJ, QUANTITYMJ, INPUT_MODE, ID_TERMINAL, ITEMCODE, REZ_1, REZ_2, WEIGHT, Expirace) " + 
				" VALUES " +
				" (@CountEntries,@ITEMNMBR,@CZ_CarKod,@LOCNCODE,@VNDITNUM,@QUANTITY,@QTYPACK,@SERLNMBR,@DATEDONE,@TIMEDONE,@USERID,@DEX_ROW_ID,@GUID, @O_Checked,@skl_id,@MJ,@QUANTITYMJ,@INPUT_MODE,@ID_TERMINAL,@ITEMCODE,@REZ_1,@REZ_2,@WEIGHT, @Expirace)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QUANTITY", DbType = System.Data.DbType.Decimal, SourceColumn = "QUANTITY" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLNMBR", DbType = System.Data.DbType.String, SourceColumn = "SERLNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@USERID", DbType = System.Data.DbType.Int32, SourceColumn = "USERID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, SourceColumn = "GUID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_Checked", DbType = System.Data.DbType.Boolean, SourceColumn = "O_Checked" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_id", DbType = System.Data.DbType.String, SourceColumn = "skl_id" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QUANTITYMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QUANTITYMJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace" });
		}

		//private void CZMST_I4_InitializeCommandUpdate(SQLiteCommand command)
		//{
		//}

		//private void CZMST_I4_InitializeCommandDelete(SQLiteCommand command)
		//{
		//}

		//private void CZMST_I4_InitializeCommandSelect(SQLiteCommand command)
		//{
		//}

		public int Update_I4(object data)
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
				//using (var commandSelect = this.Connection.CreateCommand())
				{
					CZMST_I4_InitializeCommandInsert(commandInsert);
					//CZMST_I4_InitializeCommandUpdate(commandUpdate);
					//CZMST_I4_InitializeCommandDelete(commandDelete);
					//CZMST_I4_InitializeCommandSelect(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						//adapter.UpdateCommand = commandUpdate;
						//adapter.DeleteCommand = commandDelete;
						//adapter.SelectCommand = commandSelect;

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

		public int Update_I4(object data, SQLiteConnection connection, SQLiteTransaction trans)
		{
			try
			{
				int result = 0;
				//Connection_Open();

				using (var commandInsert = connection.CreateCommand())
				//using (var commandUpdate = this.Connection.CreateCommand())
				//using (var commandDelete = this.Connection.CreateCommand())
				//using (var commandSelect = this.Connection.CreateCommand())
				{
					commandInsert.Transaction = trans;

					CZMST_I4_InitializeCommandInsert(commandInsert);
					//CZMST_I4_InitializeCommandUpdate(commandUpdate);
					//CZMST_I4_InitializeCommandDelete(commandDelete);
					//CZMST_I4_InitializeCommandSelect(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						//adapter.UpdateCommand = commandUpdate;
						//adapter.DeleteCommand = commandDelete;
						//adapter.SelectCommand = commandSelect;

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

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				//Connection_Close();
			}
		}


		//public int CZMST_I4_Fill(System.Data.DataTable dataTable)
		//{
		//    try
		//    {
		//        Connection_Open();
		//        using (var command = this.Connection.CreateCommand())
		//        {
		//            command.CommandText =
		//                "SELECT * FROM CZMST_I4";
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
		//        Logging.ExceptionHandler2.Handle(ex);
		//        throw ex;
		//    }
		//    finally
		//    {
		//        Connection_Close();
		//    }
		//}
		//public Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I4DataTable CZMST_I4_GetData()
		//{
		//    var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I4DataTable();
		//    this.CZMST_I4_Fill(dataTable);
		//    return dataTable;
		//}

		public int DeleteByGUID_I4(Guid GUID)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_I4 WHERE (GUID = @GUID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID });
					return command.ExecuteNonQuery();
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

		public int DeleteByITEMNMBR_I4(string ITEMNMBR)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_I4 WHERE (ITEMNMBR = @ITEMNMBR)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
					return command.ExecuteNonQuery();
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

		public int DeleteBy_ITEMNMBR_SKL_ID_I4(string ITEMNMBR, string SKL_ID)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_I4 WHERE (ITEMNMBR = @ITEMNMBR) and (SKL_ID=@SKL_ID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
					return command.ExecuteNonQuery();
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

		public decimal? NasnimanoQuantity_I4(string ITEMNMBR)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT SUM(QUANTITY) AS NASNIMANO FROM CZMST_I4 WHERE (ITEMNMBR = @ITEMNMBR)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(System.DBNull))))
					{
						return null;
					}
					else
					{
						decimal pp = Convert.ToDecimal(returnValue);
						return pp;
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

		public decimal? NasnimanoQuantity_I4(string ITEMNMBR, string SKL_ID)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT SUM(QUANTITY) AS NASNIMANO FROM CZMST_I4 WHERE (ITEMNMBR = @ITEMNMBR) AND (SKL_ID = @SKL_ID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(System.DBNull))))
					{
						return null;
					}
					else
					{
						decimal pp = Convert.ToDecimal(returnValue);
						return pp;
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

		public int? NasnimanoPocet_I4(string ITEMNMBR)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT Count(*) AS Pocet FROM CZMST_I4 WHERE (ITEMNMBR = @ITEMNMBR)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
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

		//        public object CZMST_I4_NasnimanoPocet(string itemnmbr, string skl_id)
		//        {
		//            try
		//            {
		//                Connection_Open();

		//                using (var command = this.Connection.CreateCommand())
		//                {
		//                    command.CommandText =
		//@"SELECT Count(*) AS Pocet
		//		FROM CZMST_I4
		//		WHERE (ITEMNMBR = @ITEMNMBR) AND (SKL_ID = @SKL_ID)";
		//                    command.Parameters.AddWithValue("@ITEMNMBR", itemnmbr);
		//                    command.Parameters.AddWithValue("@SKL_ID", skl_id);
		//                    object o = command.ExecuteScalar();
		//                    return o;
		//                }
		//            }
		//            catch (Exception ex)
		//            {
		//                Logging.ExceptionHandler2.Handle(ex);
		//                throw ex;
		//            }
		//            finally
		//            {
		//                Connection_Close();
		//            }
		//        }

		public int Fill_I4(System.Data.DataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_I4";
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

		#endregion

		#region Queries

		//        /// <summary>
		//        /// Počet variant materiálu v CZMST_I3
		//        /// </summary>
		//        /// <param name="itemnmbr">číslo položky</param>
		//        /// <returns>Int32</returns>
		//        public object ItemsCountVariants(string itemnmbr)
		//        {
		//            try
		//            {
		//                Connection_Open();

		//                using (var command = this.Connection.CreateCommand())
		//                {
		//                    command.CommandText =
		//@"SELECT COUNT(ITEMNMBR) AS CNT
		//		FROM CZMST_I3
		//		WHERE (ITEMNMBR = @ITEMNMBR)";
		//                    command.Parameters.AddWithValue("@ITEMNMBR", itemnmbr);
		//                    return command.ExecuteScalar();
		//                }
		//            }
		//            catch (Exception ex)
		//            {
		//                Logging.ExceptionHandler2.Handle(ex);
		//                throw ex;
		//            }
		//            finally
		//            {
		//                Connection_Close();
		//            }
		//        }

		//        /// <summary>
		//        /// Počet položek v předloze CZMST_I1
		//        /// </summary>
		//        /// <returns>Int32</returns>
		//        public object ItemsCount()
		//        {
		//            try
		//            {
		//                Connection_Open();

		//                using (var command = this.Connection.CreateCommand())
		//                {
		//                    command.CommandText =
		//                        "SELECT COUNT(*) AS Pocet FROM CZMST_I1";
		//                    return command.ExecuteScalar();
		//                }
		//            }
		//            catch (Exception ex)
		//            {
		//                Logging.ExceptionHandler2.Handle(ex);
		//                throw ex;
		//            }
		//            finally
		//            {
		//                Connection_Close();
		//            }
		//        }

		/// <summary>
		/// Počet zbyvajicich položek i1.nasnimano mensi nez i4.qunatity
		/// </summary>
		/// <returns>Int32</returns>
		public int? ZbyvaPolozek_queries()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = @"SELECT Count(*) as ZBYVAPOLOZEK " + 
						" FROM CZMST_I1 AS i1 " + 
						" LEFT OUTER JOIN ( " + 
						" SELECT ITEMNMBR, SUM(QUANTITY) AS NASNIMANO FROM CZMST_I4 GROUP BY ITEMNMBR ) " + 
						" AS i4 ON i1.ITEMNMBR = i4.ITEMNMBRWHERE (coalesce(i4.NASNIMANO, 0) < i1.QUANTITY)";
					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(System.DBNull))))
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

		#endregion

		#region Parametry

		public int Fill_Parametry(System.Data.DataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM Parametry";
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

		public Fask.SQLiteDBs.DataSets.Inventura1.ParametryDataTable GetData_Parametry()
		{
			var dataTable = new Fask.SQLiteDBs.DataSets.Inventura1.ParametryDataTable();
			this.Fill_Parametry(dataTable);
			return dataTable;
		}

		
		public int Update_Params(object data, SQLiteConnection connection, SQLiteTransaction trans)
		{
			try
			{
				int result = 0;

				using (var commandInsert = connection.CreateCommand())
				using (var commandSelect = connection.CreateCommand())
				{
					commandInsert.Transaction = trans;
					commandSelect.Transaction = trans;
					InitializeCommandInsert_Params(commandInsert);
					InitializeCommandSelect_Params(commandSelect);

					using (var adapter = new SQLiteDataAdapter())
					{
						adapter.InsertCommand = commandInsert;
						adapter.SelectCommand = commandSelect;

						var dataIsDataSet = data as System.Data.DataSet;
						var dataIsDataTable = data as System.Data.DataTable;
						var dataIsDataRow = data as System.Data.DataRow;
						var dataIsDataRowArray = data as System.Data.DataRow[];


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

				return result;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
		}


		#region Inicialize metody

		public void InitializeCommandInsert_Params(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO Parametry ( " + 
				" CFG_UpozornitNaPrebytek, CFG_DalsiPolozkuBezDotazu, CFG_KontrolaUplnostiPolozky," + 
				" CFG_PoZadaniSNZpetNaMN, CFG_PredvyplnitMnozstvi, CFG_PredvyplnitMnozstviZbyvajici," + 
				" CFG_PredvyplnitMnozstviOJedna, CFG_PovolitDuplicituSN, CFG_MnozstviScannerem," + 
				" CFG_PosunNaDalsiPolozku, CFG_PovolitZaporneMnozstvi, CFG_KontrolaUplnosti," + 
				" CFG_PovolitZmenuLokace, CFG_PovolitZobrazeniMnozstviNaSklade, CONFIG_LOKACE_POVOLIT," + 
				" CONFIG_LOKACE_TIMEOUT " + 
				" ) VALUES ( " + 
				" @CFG_UpozornitNaPrebytek, @CFG_DalsiPolozkuBezDotazu, @CFG_KontrolaUplnostiPolozky," + 
				" @CFG_PoZadaniSNZpetNaMN, @CFG_PredvyplnitMnozstvi, @CFG_PredvyplnitMnozstviZbyvajici, " + 
				" @CFG_PredvyplnitMnozstviOJedna, @CFG_PovolitDuplicituSN, @CFG_MnozstviScannerem, " + 
				" @CFG_PosunNaDalsiPolozku, @CFG_PovolitZaporneMnozstvi, @CFG_KontrolaUplnosti," + 
				" @CFG_PovolitZmenuLokace, @CFG_PovolitZobrazeniMnozstviNaSklade, @CONFIG_LOKACE_POVOLIT," + 
				" @CONFIG_LOKACE_TIMEOUT " + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_UpozornitNaPrebytek", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_UpozornitNaPrebytek" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_DalsiPolozkuBezDotazu", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_DalsiPolozkuBezDotazu" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_KontrolaUplnostiPolozky", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_KontrolaUplnostiPolozky" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PoZadaniSNZpetNaMN", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PoZadaniSNZpetNaMN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PredvyplnitMnozstvi", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PredvyplnitMnozstvi" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PredvyplnitMnozstviZbyvajici", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PredvyplnitMnozstviZbyvajici" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PredvyplnitMnozstviOJedna", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PredvyplnitMnozstviOJedna" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PovolitDuplicituSN", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PovolitDuplicituSN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_MnozstviScannerem", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_MnozstviScannerem" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PosunNaDalsiPolozku", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PosunNaDalsiPolozku" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PovolitZaporneMnozstvi", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PovolitZaporneMnozstvi" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_KontrolaUplnosti", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_KontrolaUplnosti" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PovolitZmenuLokace", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PovolitZmenuLokace" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CFG_PovolitZobrazeniMnozstviNaSklade", DbType = System.Data.DbType.Boolean, SourceColumn = "CFG_PovolitZobrazeniMnozstviNaSklade" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_POVOLIT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LOKACE_POVOLIT" });			
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_TIMEOUT", DbType = System.Data.DbType.Int32, SourceColumn = "CONFIG_LOKACE_TIMEOUT" });

		}

		public void InitializeCommandSelect_Params(SQLiteCommand command)
		{
			command.CommandText = "Select * from Parametry";
		}

		#endregion


		#endregion

		#endregion

		#region Puvodne metody, zrevidovat

		/// <summary>
		/// Pokud existuji nasnimana data, vraci true, jinak false
		/// </summary>
		public bool dataExistsInI4()
		{

			bool dataexists = false;
			try
			{
				Connection_Open();

				using (var command = new SQLiteCommand("Select Itemnmbr from czmst_i4 limit 1", this.Connection))
				{
					using (var sqlreader = command.ExecuteReader())
					{
						dataexists = sqlreader.Read();
					}

					return dataexists;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
			finally
			{
				Connection_Close();
			}
		}

		public bool dataExistsInI1(string selectNazev)
		{

			bool dataexists = false;
			try
			{
				Connection_Open();
				//string selectNazev = "select itemnmbr from czmst_i1 where itemdesc like '" + (Settings.Inventura1_HledatFulltext ? "%" : "") + nazev + "%'";

				using (var command = new SQLiteCommand(selectNazev, this.Connection))
				{
					using (var sqlreader = command.ExecuteReader())
					{
						dataexists = sqlreader.Read();
					}

					return dataexists;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
			finally
			{
				Connection_Close();
			}
		}

		public bool dataExistsInI4_ByITEMNMBR_SERLNMBR(string ITEMNMBR, string SERLTNMBR)
		{

			bool dataexists = false;
			try
			{
				Connection_Open();
				string selectNazev = "select * from czmst_i4 where itemnmbr='" + ITEMNMBR + "' and serlnmbr='" + SERLTNMBR + "'";

				using (var command = new SQLiteCommand(selectNazev, this.Connection))
				{
					using (var sqlreader = command.ExecuteReader())
					{
						dataexists = sqlreader.Read();
					}

					return dataexists;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
			finally
			{
				Connection_Close();
			}
		}

		public bool dataExistsInI2_ByITEMNMBR_SERLNMBR(string ITEMNMBR, string SERLTNMBR)
		{

			bool dataexists = false;
			try
			{
				Connection_Open();
				string selectNazev = "select * from czmst_i2 where itemnmbr='" + ITEMNMBR + "' and serlnmbr='" + SERLTNMBR + "'";

				using (var command = new SQLiteCommand(selectNazev, this.Connection))
				{
					using (var sqlreader = command.ExecuteReader())
					{
						dataexists = sqlreader.Read();
					}

					return dataexists;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return false;
			}
			finally
			{
				Connection_Close();
			}
		}

		public int CreateResultSet_GetGount(string _select_current_count)
		{

			try
			{
				Connection_Open();

				using (var command = new SQLiteCommand(_select_current_count, this.Connection))
				{
					object obj = command.ExecuteScalar();

					if (obj != null)
					{
						return Convert.ToInt32(obj);
					}
					else
					{
						return 0;
					}
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return 0;
			}
			finally
			{
				Connection_Close();
			}
		}

		internal Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I4DataTable CreateResultSet_Get_I4(string select_command, int indexStart, int indexEnd)
		{
			var table = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I4DataTable();

			try
			{
				table.BeginLoadData();

				this.Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = select_command;

					using (var reader = command.ExecuteReader())
					{
						if (reader.HasRows) //29,1,2021 TaD Přidal, protože netuší co to JiS vymyslel za složitost... a nefunguje to...
						{

							// najeti na startovaci index pro sqlite
							for (int index = 0; index <= indexStart; index++)
							{
								if (!reader.Read())
									break;
							}

							int i = indexStart;

							do
							{
								_Routines.LoadRowFromReader(reader, table);

								i++;
								if (!reader.Read())
									break;
							} while (i < indexEnd);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				this.Connection_Close();

				table.EndLoadData();
			}

			return table;
		}

		internal Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1DataTable CreateResultSet_Get_I1(string select_command, int indexStart, int indexEnd)
		{
			var table = new Fask.SQLiteDBs.DataSets.Inventura1.CZMST_I1DataTable();

			try
			{
				table.BeginLoadData();

				this.Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = select_command;

					using (var reader = command.ExecuteReader())
					{
						// najeti na startovaci index pro sqlite
						for (int index = 0; index <= indexStart; index++)
						{
							if (!reader.Read())
								break;
						}

						int i = indexStart;

						do
						{
							_Routines.LoadRowFromReader(reader, table);

							i++;
							if (!reader.Read())
								break;
						} while (i < indexEnd);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				this.Connection_Close();

				table.EndLoadData();
			}

			return table;
		} 


		#endregion

	}
}
