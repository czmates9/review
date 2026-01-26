using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controller pro ciselnik skladu : CZMST093
    /// </summary>
    public class SQLite_Controller_Sklady : SQLite_Controller
    {
		//private Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklady = null;
		//internal Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter Ta_sklady
		//{
		//    get
		//    {
		//        if (ta_sklady == null)
		//        {
		//            ta_sklady = new Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
		//            ta_sklady.Connection = this.Connection;
		//        }
		//        return ta_sklady;
		//    }
		//}


        #region c'tors
		//public SQLite_Controller_Sklady()
		//    : base(Main.CiselnikSkladyDB)
		//{
		//}
        public SQLite_Controller_Sklady(string sqliteFileName)
            : base(sqliteFileName)
        {
        }

        public SQLite_Controller_Sklady(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();
		//    ta_sklady = new Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
		//    ta_sklady.Connection = this.Connection;
		//}
        #endregion

        public override void Dispose()
        {
            // disposing adapters ...
			//this.DisposeObject(ta_sklady);

			//this.ta_sklady = null;

            base.Dispose();
        }

        /// <summary>
        /// Seznam skladu dle skl_id (respektive max jeden sklad)
        /// </summary>
        /// <param name="skl_id">id skladu</param>
        /// <returns>Tabulka se skladem, pokud id skladu existuje v ciselniku skladu</returns>
		public Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable GetDataBySkl_id(string skl_id)
		{

			try
			{
				var dataTable = new Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable();

				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT [skl_id], [skl_desc], [skl_typ], [skl_carcode], [DEX_ROW_ID] FROM [CZMST093] where skl_id=@skl_id";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_id", DbType = System.Data.DbType.String, Value = skl_id == null ? (object)DBNull.Value : skl_id });
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


        /// <summary>
        /// Provede akutalizaci hodnoty ITEMDESC pro volny pohyb z ciselniku zbozi
        /// </summary>
        /// <param name="dt_di">data volneho pohybu</param>
        public void Update_SkladDescription(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dt_zbozi)
        {
            try
            {
                Connection_Open();

                using (var zcommand = this.Connection.CreateCommand())
                {
					zcommand.CommandText = "Select skl_desc from czmst093 where skl_id=@sklid";
					zcommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@sklid", DbType = System.Data.DbType.String });
					foreach (var diro in dt_zbozi.Rows)
                    {
                        try
                        {
							var dir = (DataSets.Zbozi.CZMST095Row)diro;
                            // načte se název položky pouze v případě, že je null
                            if (dir.IsSKL_DESCNull())
                            {
                                zcommand.Parameters["@sklid"].Value = dir.SKL_ID.Trim();
								object skl_desc = zcommand.ExecuteScalar();
								if (skl_desc is string) // pretypuje se pouze, pokud je vysledek string...
									dir.SKL_DESC = (string)skl_desc;
                            }
                        }
                        catch (Exception exCommand)
                        {
							Logging.ExceptionHandler2.Handle(exCommand);
						}
                    }
                }
            }
            catch (Exception ex)
            {
				Logging.ExceptionHandler2.Handle(ex);
			}
            finally
            {
                Connection_Close();
            }

		}

		public string GetSkladDesc(string skl_id)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT skl_desc FROM CZMST093 WHERE (skl_id = @skl_id)";
					command.CommandType = global::System.Data.CommandType.Text;
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_id", DbType = System.Data.DbType.String, Value = skl_id == null ? (object)DBNull.Value : skl_id });
					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return null;
					}
					else
					{
						return ((string)(returnValue));
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

		public int Fill(Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST093 ";
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
				//using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert(commandInsert);
					//InitializeCommandUpdate(commandUpdate);
					//InitializeCommandDelete(commandDelete);
					InitializeCommandSelect(commandSelect);

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

		public void InitializeCommandInsert(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO [CZMST093] (" +
				" [skl_id], [skl_desc], [skl_typ], [skl_carcode], [DEX_ROW_ID]" + 
				" ) VALUES ( " +
				" @skl_id, @skl_desc, @skl_typ, @skl_carcode, @DEX_ROW_ID " + 
				" ) ";


			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_id", DbType = System.Data.DbType.String, SourceColumn = "skl_id" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_desc", DbType = System.Data.DbType.String, SourceColumn = "skl_desc" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_typ", DbType = System.Data.DbType.String, SourceColumn = "skl_typ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_carcode", DbType = System.Data.DbType.String, SourceColumn = "skl_carcode" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID" });

		}

		//public void InitializeCommandUpdate(SQLiteCommand command)
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

		//public void InitializeCommandDelete(SQLiteCommand command)
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

		public void InitializeCommandSelect(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST093";
		}


		#endregion


		#endregion


	}
}
