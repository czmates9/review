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
    public class SQLite_Controller_Lokace : SQLite_Controller
    {
		//private Fask.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST094TableAdapter ta_lokace = null;
		//internal Fask.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST094TableAdapter Ta_lokace
		//{
		//    get
		//    {
		//        if (ta_lokace == null)
		//        {
		//            ta_lokace = new Fask.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST094TableAdapter();
		//            ta_lokace.Connection = this.Connection;
		//        }
		//        return ta_lokace;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter ta_skladlokace_lokacetypy = null;
		//internal Fask.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter Ta_skladlokace_lokacetypy
		//{
		//    get
		//    {
		//        if (ta_skladlokace_lokacetypy == null)
		//        {
		//            ta_skladlokace_lokacetypy = new Fask.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter();
		//            ta_skladlokace_lokacetypy.Connection = this.Connection;
		//        }
		//        return ta_skladlokace_lokacetypy;
		//    }
		//}

        #region c'tors
		//public SQLite_Controller_Lokace()
		//    : base(Main.CiselnikLokaceDB)
		//{
		//}

        public SQLite_Controller_Lokace(string sqliteFileName)
            : base(sqliteFileName)
        {
        }

        public SQLite_Controller_Lokace(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();

		//    ta_lokace = new Fask.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST094TableAdapter();
		//    ta_skladlokace_lokacetypy = new Fask.SQLiteDBs.DataSets.LokaceTableAdapters.CZMST_SkladLokace_LokaceTypyTableAdapter();

		//    ta_lokace.Connection = this.Connection;
		//    ta_lokace.Connection = this.Connection;
		//}

        public override void Dispose()
        {
            // disposing adapters ...
			//if (ta_lokace != null) ta_lokace.Dispose();
			//if (ta_skladlokace_lokacetypy != null) ta_skladlokace_lokacetypy.Dispose();

			//this.DisposeObject(ta_lokace);
			//this.DisposeObject(ta_skladlokace_lokacetypy);

            //ta_lokace = null;
            //ta_skladlokace_lokacetypy = null;

            base.Dispose();
        }

        #endregion

        #region Metody z TableAdpateru
        
        #region CZMST094
        //private void CZMST094_InsertCommandInitialize(SQLiteCommand command)
        //{
        //    command.CommandText =
        //        "INSERT INTO CZMST094" +
        //        " (SKL_ID, LOCNCODE, TYPE, IS_RECEIVE, DEX_ROW_ID, Description, Barcode)" +
        //        " VALUES " +
        //        " (@SKL_ID,@LOCNCODE,@TYPE,@IS_RECEIVE,@DEX_ROW_ID,@Description,@Barcode)";
        //    global::System.Data.SQLite.SQLiteParameter param = new global::System.Data.SQLite.SQLiteParameter();
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@SKL_ID";
        //    param.SourceColumn = "SKL_ID";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@LOCNCODE";
        //    param.SourceColumn = "LOCNCODE";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@TYPE";
        //    param.SourceColumn = "TYPE";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@IS_RECEIVE";
        //    param.DbType = global::System.Data.DbType.Boolean;
        //    param.SourceColumn = "IS_RECEIVE";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@DEX_ROW_ID";
        //    param.DbType = global::System.Data.DbType.Int32;
        //    param.SourceColumn = "DEX_ROW_ID";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@Description";
        //    param.SourceColumn = "Description";
        //    command.Parameters.Add(param);
        //    param = new global::System.Data.SQLite.SQLiteParameter();
        //    param.ParameterName = "@Barcode";
        //    param.SourceColumn = "Barcode";
        //    command.Parameters.Add(param);
        //}

        //public int CZMST094_Update(object data)
        //{
        //    SQLiteTransaction transaction = null;
        //    try
        //    {
        //        int result = 0;
        //        Connection_Open();

        //        transaction = this.Connection.BeginTransaction();

        //        using (var command = this.Connection.CreateCommand())
        //        {
        //            CZMST094_InsertCommandInitialize(command);

        //            using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
        //            {
        //                adapter.InsertCommand = command;
        //                if (data is Fask.SQLiteDBs.DataSets.Lokace)
        //                    result = adapter.Update(data as Fask.SQLiteDBs.DataSets.Lokace, (data as Fask.SQLiteDBs.DataSets.Lokace).CZMST094.TableName);
        //                else if (data is Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable)
        //                    result = adapter.Update(data as Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable);
        //                else if (data is Fask.SQLiteDBs.DataSets.Lokace.CZMST094Row)
        //                    result = adapter.Update(new Fask.SQLiteDBs.DataSets.Lokace.CZMST094Row[] { data as Fask.SQLiteDBs.DataSets.Lokace.CZMST094Row });
        //                else if (data is Fask.SQLiteDBs.DataSets.Lokace.CZMST094Row[])
        //                    result = adapter.Update(data as Fask.SQLiteDBs.DataSets.Lokace.CZMST094Row[]);
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


        //public Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable CZMST094_GetData()
        //{
        //    var dataTable = new Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable();
        //    this.CZMST094_Fill(dataTable);
        //    return dataTable;
        //}

        //public int CZMST094_FillByBarcode(Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable dataTable, string Barcode)
        //{
        //    try
        //    {
        //        Connection_Open();

        //        using (var command = this.Connection.CreateCommand())
        //        {
        //            command.CommandText = 
        //                "SELECT * FROM CZMST094 WHERE (Barcode = @Barcode)";
        //            command.Parameters.AddWithValue("@Barcode", Barcode);
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
        //public Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable CZMST094_GetDataByBarcode(string Barcode)
        //{
        //    var dataTable = new Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable();
        //    this.CZMST094_FillByBarcode(dataTable, Barcode);
        //    return dataTable;
        //}
        
        //public Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable CZMST094_GetDataBySklID(string SKL_ID)
        //{
        //    var dataTable = new Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable();
        //    this.CZMST094_FillBySklID(dataTable, SKL_ID);
        //    return dataTable;
        //}
        //public int CZMST094_FillBySklidBarcode(Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable dataTable, string SKL_ID, string Barcode)
        //{
        //    try
        //    {
        //        Connection_Open();

        //        using (var command = this.Connection.CreateCommand())
        //        {
        //            command.CommandText =
        //                "SELECT * FROM CZMST094 WHERE (SKL_ID=@SKL_ID AND Barcode=@Barcode)";
        //            command.Parameters.AddWithValue("@SKL_ID", SKL_ID);
        //            command.Parameters.AddWithValue("@Barcode", Barcode);
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
        //public Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable CZMST094_GetDataBySklidBarcode(string SKL_ID, string Barcode)
        //{
        //    var dataTable = new Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable();
        //    this.CZMST094_FillBySklidBarcode(dataTable, SKL_ID, Barcode);
        //    return dataTable;
        //}


        #endregion

        #region CZMST SKlad Lokace Typy

        //public virtual Fask.SQLiteDBs.DataSets.Lokace.CZMST_SkladLokace_LokaceTypyDataTable CZMST_SkladLokace_LokaceTypy_GetData()
        //{
        //    var dataTable = new Fask.SQLiteDBs.DataSets.Lokace.CZMST_SkladLokace_LokaceTypyDataTable();
        //    this.CZMST_SkladLokace_LokaceTypy_Fill(dataTable);
        //    return dataTable;
        //}
        #endregion

		#region Zrevidovane

		#region CZMST094

		public int CZMST094_Fill(Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable dataTable)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST094";
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

		public int CZMST094_FillBySklID(Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable dataTable, string SKL_ID)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST094 WHERE (SKL_ID=@SKL_ID)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
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

		public object CZMST094_GetLocDesc(string LOCNCODE, string SKL_ID)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText =
						"SELECT Description FROM CZMST094 WHERE (LOCNCODE = @LOCNCODE) AND (SKL_ID = @SKL_ID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = LOCNCODE == null ? (object)DBNull.Value : LOCNCODE });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });

					object o = command.ExecuteScalar();
					return o;
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

		public int Update_CZMST094(object data)
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
					InitializeCommandInsert_CZMST094(commandInsert);
					//InitializeCommandUpdate_CZMST094(commandUpdate);
					//InitializeCommandDelete_CZMST094(commandDelete);
					InitializeCommandSelect_CZMST094(commandSelect);

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

		public void InitializeCommandInsert_CZMST094(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST094 (" + 
				"SKL_ID, LOCNCODE, TYPE, DEX_ROW_ID, Description, Barcode" +
				" ) VALUES ( " + 
				" @SKL_ID, @LOCNCODE, @TYPE, @DEX_ROW_ID, @Description, @Barcode " + 
				" ) ";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPE", DbType = System.Data.DbType.String, SourceColumn = "TYPE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, SourceColumn = "Description" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Barcode", DbType = System.Data.DbType.String, SourceColumn = "Barcode" });
		}

		//public void InitializeCommandUpdate_CZMST094(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE [OSOBY] SET " +
		//		" [OSOBA_ZODP] = @OSOBA_ZODP," +
		//		" [TITUL] = @TITUL," +
		//		" [PRIJMENI] = @PRIJMENI," +
		//		" [JMENO] = @JMENO " +
		//		"WHERE [OSOBA_ZODP] = @OSOBA_ZODP_P";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA_ZODP", DbType = System.Data.DbType.Int32, SourceColumn = "OSOBA_ZODP" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TITUL", DbType = System.Data.DbType.String, SourceColumn = "TITUL" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRIJMENI", DbType = System.Data.DbType.String, SourceColumn = "PRIJMENI" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@JMENO", DbType = System.Data.DbType.String, SourceColumn = "JMENO" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA_ZODP_P", DbType = System.Data.DbType.Int32, SourceColumn = "OSOBA_ZODP" });
		//}

		//public void InitializeCommandDelete_CZMST094(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM [OSOBY] WHERE [OSOBA_ZODP] = @OSOBA_ZODP";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@OSOBA_ZODP",
		//		DbType = System.Data.DbType.Int32,
		//		SourceColumn = "OSOBA_ZODP",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_CZMST094(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST094";
		}


		#endregion


		#endregion

		#region CZMST SKlad Lokace Typy

		public virtual int CZMST_SkladLokace_LokaceTypy_Fill(Fask.SQLiteDBs.DataSets.Lokace.CZMST_SkladLokace_LokaceTypyDataTable dataTable)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_SkladLokace_LokaceTypy";

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

		public int Update_CZMST_SkladLokace_LokaceTypy(object data)
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
					InitializeCommandInsert_CZMST_SkladLokace_LokaceTypy(commandInsert);
					//InitializeCommandUpdate_CZMST_SkladLokace_LokaceTypy(commandUpdate);
					//InitializeCommandDelete_CZMST_SkladLokace_LokaceTypy(commandDelete);
					InitializeCommandSelect_CZMST_SkladLokace_LokaceTypy(commandSelect);

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

		public void InitializeCommandInsert_CZMST_SkladLokace_LokaceTypy(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_SkladLokace_LokaceTypy(" + 
				" TYPE, Description, IS_RECEIVE, IS_DEFAULT, IS_NORMAL" + 
				") VALUES ( " + 
				"@TYPE,@Description,@IS_RECEIVE,@IS_DEFAULT,@IS_NORMAL )";


			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPE", DbType = System.Data.DbType.String, SourceColumn = "TYPE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Description", DbType = System.Data.DbType.String, SourceColumn = "Description" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IS_RECEIVE", DbType = System.Data.DbType.Byte, SourceColumn = "IS_RECEIVE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IS_DEFAULT", DbType = System.Data.DbType.Byte, SourceColumn = "IS_DEFAULT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@IS_NORMAL", DbType = System.Data.DbType.Byte, SourceColumn = "IS_NORMAL" });
			

		}

		//public void InitializeCommandUpdate_CZMST_SkladLokace_LokaceTypy(SQLiteCommand command)
		//{
		//	command.CommandText = "UPDATE [OSOBY] SET " +
		//		" [OSOBA_ZODP] = @OSOBA_ZODP," +
		//		" [TITUL] = @TITUL," +
		//		" [PRIJMENI] = @PRIJMENI," +
		//		" [JMENO] = @JMENO " +
		//		"WHERE [OSOBA_ZODP] = @OSOBA_ZODP_P";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA_ZODP", DbType = System.Data.DbType.Int32, SourceColumn = "OSOBA_ZODP" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TITUL", DbType = System.Data.DbType.String, SourceColumn = "TITUL" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRIJMENI", DbType = System.Data.DbType.String, SourceColumn = "PRIJMENI" });
		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@JMENO", DbType = System.Data.DbType.String, SourceColumn = "JMENO" });

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@OSOBA_ZODP_P", DbType = System.Data.DbType.Int32, SourceColumn = "OSOBA_ZODP" });
		//}

		//public void InitializeCommandDelete_CZMST_SkladLokace_LokaceTypy(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM [OSOBY] WHERE [OSOBA_ZODP] = @OSOBA_ZODP";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@OSOBA_ZODP",
		//		DbType = System.Data.DbType.Int32,
		//		SourceColumn = "OSOBA_ZODP",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_CZMST_SkladLokace_LokaceTypy(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_SkladLokace_LokaceTypy";
		}


		#endregion


		#endregion

		#endregion

		#endregion


	}
}
