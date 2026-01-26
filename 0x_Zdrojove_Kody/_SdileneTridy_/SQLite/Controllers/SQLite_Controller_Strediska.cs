using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controller pro ciselnik stredisek : CZMST091
    /// </summary>
    public class SQLite_Controller_Strediska : SQLite_Controller
    {
		//private Fask.SQLiteDBs.DataSets.StrediskaTableAdapters.CZMST091TableAdapter ta_strediska = null;
		//internal Fask.SQLiteDBs.DataSets.StrediskaTableAdapters.CZMST091TableAdapter Ta_strediska
		//{
		//    get
		//    {
		//        if (ta_strediska == null)
		//        {
		//            ta_strediska = new Fask.SQLiteDBs.DataSets.StrediskaTableAdapters.CZMST091TableAdapter();
		//            ta_strediska.Connection = this.Connection;
		//        }
		//        return ta_strediska;
		//    }
		//}

        #region c'tors
		//public SQLite_Controller_Strediska()
		//    : base(Main.CiselnikStrediskaDB)
		//{
		//}
        public SQLite_Controller_Strediska(string sqliteFileName)
            : base(sqliteFileName)
        {
        }

        public SQLite_Controller_Strediska(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();

		//    ta_strediska = new Fask.SQLiteDBs.DataSets.StrediskaTableAdapters.CZMST091TableAdapter();

		//    ta_strediska.Connection = this.Connection;

		//}
        #endregion

        public override void Dispose()
        {
            // disposing adapters ...
			//if (ta_strediska != null)
			//    ta_strediska.Dispose();


			//this.DisposeObject(ta_strediska);

			//this.ta_strediska = null;
            
			base.Dispose();
        }

        /// <summary>
        /// Pocet zaznamu na zaklade pozadovaneho select count() commandu
        /// </summary>
        /// <param name="select"></param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        /// <returns></returns>
        public int Count(string select)
        {
            try
            {
                Connection_Open();

                using (var scecommand = this.Connection.CreateCommand())
                {
                    scecommand.CommandText = select;
                    return Convert.ToInt32(scecommand.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                return -1; // -1 indikuje chybu
            }
            finally
            {
                Connection_Close();
            }
        }

        /// <summary>
        /// Zaznamy na zaklade pozadovaneho select commandu
        /// </summary>
        /// <param name="select"></param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        public void Load(Fask.SQLiteDBs.DataSets.Strediska.CZMST091DataTable dt_strediska, string select, int indexStart, int indexEnd)
        {
            try
            {
                Connection_Open();

                using (var scecommand = this.Connection.CreateCommand())
                {
                    scecommand.CommandText = select;
                    using (var sceresultset = scecommand.ExecuteReader())
                    {
                        dt_strediska.BeginLoadData();
                        dt_strediska.Clear();

                        // najeti na startovaci index pro sqlite
                        for (int index = 0; index <= indexStart; index++)
                        {
                            if (!sceresultset.Read())
                                break;
                        }

                        int i = indexStart;
                        //for (int i = indexStart; i < indexEnd; i++)
                        do
                        {
                            //SqlCEDBs.DataSets.Strediska.CZMST091Row srow = _strediska.CZMST091.NewCZMST091Row();
                            //srow.DEX_ROW_ID = (int)sceresultset["DEX_ROW_ID"];
                            //srow.str_carcode = (string)sceresultset["str_carcode"] ?? string.Empty;
                            //srow.str_desc = (string)sceresultset["str_desc"] ?? string.Empty;
                            //srow.str_id = (string)sceresultset["str_id"] ?? string.Empty;
                            //srow.str_typ = (string)sceresultset["str_typ"] ?? string.Empty;
                            //_strediska.CZMST091.AddCZMST091Row(srow);
                            
                            _Routines.LoadRowFromReader(sceresultset, dt_strediska);

                            i++;
                            if (!sceresultset.Read())
                                break;
                        } while (i < indexEnd);
                        
                        dt_strediska.EndLoadData();

                    }
                }
            }
            catch (Exception exObecna)
            {
                Logging.ExceptionHandler2.Handle(exObecna);
            }
            finally
            {
                Connection_Close();
            }
        }

		public string GetDescription(string str_id)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT str_desc FROM CZMST091 where str_id=@str_id";
					command.CommandType = System.Data.CommandType.Text;
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@str_id", DbType = System.Data.DbType.String, Value = str_id == null ? (object)DBNull.Value : str_id });
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

		public Fask.SQLiteDBs.DataSets.Strediska.CZMST091DataTable GetDataByStr_id(string str_id)
		{
			Fask.SQLiteDBs.DataSets.Strediska.CZMST091DataTable dt = new Fask.SQLiteDBs.DataSets.Strediska.CZMST091DataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT [str_id], [str_desc], [str_typ], [str_carcode], [DEX_ROW_ID] FROM [CZMST091] where str_id=@str_id";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@str_id", DbType = System.Data.DbType.String, Value = str_id == null ? (object)DBNull.Value : str_id });

						adapter.Fill(dt);

						return dt;
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
			command.CommandText = "INSERT INTO CZMST091 (" +
				" str_id, str_desc, str_typ, str_carcode, DEX_ROW_ID" +
				" ) VALUES (" + 
				" @str_id, @str_desc, @str_typ, @str_carcode, @DEX_ROW_ID)";


			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@str_id", DbType = System.Data.DbType.String, SourceColumn = "str_id" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@str_desc", DbType = System.Data.DbType.String, SourceColumn = "str_desc" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@str_typ", DbType = System.Data.DbType.String, SourceColumn = "str_typ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@str_carcode", DbType = System.Data.DbType.String, SourceColumn = "str_carcode" });
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
			command.CommandText = "Select * from CZMST091";
		}


		#endregion


		#endregion


	}
}
