using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controller pro ciselnik pracovniku : CZMST096
    /// </summary>
    public class SQLite_Controller_Pracovnici : SQLite_Controller
    {
		//private Fask.SQLiteDBs.DataSets.PracovniciTableAdapters.CZMST096TableAdapter ta_pracovnici = null;
		//internal Fask.SQLiteDBs.DataSets.PracovniciTableAdapters.CZMST096TableAdapter Ta_pracovnici
		//{
		//    get
		//    {
		//        if (ta_pracovnici == null)
		//        {
		//            ta_pracovnici = new Fask.SQLiteDBs.DataSets.PracovniciTableAdapters.CZMST096TableAdapter();
		//            ta_pracovnici.Connection = this.Connection;
		//        }
		//        return ta_pracovnici;
		//    }
		//}

        #region c'tors
		//public SQLite_Controller_Pracovnici()
		//    : base(Main.CiselnikPracovniciDB)
		//{
		//}

        public SQLite_Controller_Pracovnici(string sqliteFileName)
            : base(sqliteFileName)
        {
        }

        public SQLite_Controller_Pracovnici(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();

		//    ta_pracovnici = new Fask.SQLiteDBs.DataSets.PracovniciTableAdapters.CZMST096TableAdapter();

		//    ta_pracovnici.Connection = this.Connection;

		//}
        #endregion

        public override void Dispose()
        {
            // disposing adapters ...
			//if (ta_pracovnici != null)
			//    ta_pracovnici.Dispose();

			//this.DisposeObject(ta_pracovnici);

			//this.ta_pracovnici = null;

            base.Dispose();
        }

        /// <summary>
        /// Vraci pocet zaznamu select count() commandu
        /// </summary>
        /// <param name="select"></param>
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
                return -1;
            }
            finally
            {
                Connection_Close();
            }
        }

        /// <summary>
        /// Naplni tabulku pracovniku dle select commandu
        /// </summary>
        /// <param name="dt_strediska">Tabulka pracovniku</param>
        /// <param name="select">select command</param>
        /// <param name="indexStart">pocatecni index</param>
        /// <param name="indexEnd">koncovy index</param>
        public void Load(Fask.SQLiteDBs.DataSets.Pracovnici.CZMST096DataTable dt_pracovnici, string select, int indexStart, int indexEnd)
        {
            try
            {
                Connection_Open();

                //System.Data.SqlServerCe.SqlCeCommand scecommand = _practa.Connection.CreateCommand();
                using (var scecommand = this.Connection.CreateCommand())
                {
                    scecommand.CommandText = select;

                    using (var sceresultset = scecommand.ExecuteReader())
                    {
                        dt_pracovnici.BeginLoadData();
                        dt_pracovnici.Clear();

                        // posun na startovaci index sqlite
                        for (int index = 0; index <= indexStart; index++)
                        {
                            if (!sceresultset.Read())
                                break;
                        }
                        int i = indexStart;
                        do
                        {
                            //SqlCEDBs.DataSets.Pracovnici.CZMST096Row prow = dt_pracovnici.NewCZMST096Row();
                            //prow.DEX_ROW_ID = (int)sceresultset["DEX_ROW_ID"];
                            //prow.prac_carcode = (string)sceresultset["prac_carcode"] ?? string.Empty;
                            //prow.prac_desc = (string)sceresultset["prac_desc"] ?? string.Empty;
                            //prow.prac_id = (string)sceresultset["prac_id"] ?? string.Empty;
                            //prow.prac_typ = (string)sceresultset["prac_typ"] ?? string.Empty;
                            //dt_pracovnici.AddCZMST096Row(prow);
                            _Routines.LoadRowFromReader(sceresultset, dt_pracovnici);

                            i++;
                            if (!sceresultset.Read())
                                break;
                        } while (i < indexEnd);
                    }

                    dt_pracovnici.EndLoadData();
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

		public  Fask.SQLiteDBs.DataSets.Pracovnici.CZMST096DataTable GetDataByPracID(string prac_id)
		{
			Fask.SQLiteDBs.DataSets.Pracovnici.CZMST096DataTable dataTable  = new Fask.SQLiteDBs.DataSets.Pracovnici.CZMST096DataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST096 Where prac_id=@prac_id";;
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@prac_id", DbType = System.Data.DbType.Int32, Value = prac_id == null ? (object)DBNull.Value : prac_id });
						adapter.Fill(dataTable);

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
			command.CommandText = "INSERT INTO [CZMST096] (" + 
				" [prac_id], [prac_desc], [prac_typ], [prac_carcode], [DEX_ROW_ID]" + 
				" ) VALUES (" +
				" @prac_id, @prac_desc, @prac_typ, @prac_carcode, @DEX_ROW_ID" + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@prac_id", DbType = System.Data.DbType.String, SourceColumn = "prac_id" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@prac_desc", DbType = System.Data.DbType.String, SourceColumn = "prac_desc" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@prac_typ", DbType = System.Data.DbType.String, SourceColumn = "prac_typ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@prac_carcode", DbType = System.Data.DbType.String, SourceColumn = "prac_carcode" });
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
			command.CommandText = "Select * from CZMST096";
		}


		#endregion


		#endregion


	}
}
