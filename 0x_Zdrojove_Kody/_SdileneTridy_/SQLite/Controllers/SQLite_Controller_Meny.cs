using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controller pro ciselnik men : CZMST097
    /// </summary>
    public class SQLite_Controller_Meny : SQLite_Controller
    {
		//private Fask.SQLiteDBs.DataSets.MenyTableAdapters.CZMST097TableAdapter ta_meny = null;
		//internal Fask.SQLiteDBs.DataSets.MenyTableAdapters.CZMST097TableAdapter Ta_meny
		//{
		//    get
		//    {
		//        if (ta_meny == null)
		//        {
		//            ta_meny = new Fask.SQLiteDBs.DataSets.MenyTableAdapters.CZMST097TableAdapter();
		//            ta_meny.Connection = this.Connection;
		//        }
		//        return ta_meny;
		//    }
		//}


        #region c'tors
		//public SQLite_Controller_Meny()
		//    : base(Main.CiselnikMenDB)
		//{
		//}

        public SQLite_Controller_Meny(string sqliteFileName)
            : base(sqliteFileName)
        {
        }

        public SQLite_Controller_Meny(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();

		//    ta_meny = new Fask.SQLiteDBs.DataSets.MenyTableAdapters.CZMST097TableAdapter();

		//    ta_meny.Connection = this.Connection;

		//}
        #endregion

        public override void Dispose()
        {
            // disposing adapters ...
			//if (ta_meny != null)
			//    ta_meny.Dispose();

			//this.DisposeObject(ta_meny);

			//this.ta_meny = null;

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
        /// Naplni tabulku dle select commandu
        /// </summary>
        /// <param name="dt_strediska">Tabulka</param>
        /// <param name="select">select command</param>
        /// <param name="indexStart">pocatecni index</param>
        /// <param name="indexEnd">koncovy index</param>
        public void Load(Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable dt_meny, string select, int indexStart, int indexEnd)
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
                        dt_meny.BeginLoadData();
                        dt_meny.Clear();

                        // posun na startovaci index sqlite
                        for (int index = 0; index <= indexStart; index++)
                        {
                            if (!sceresultset.Read())
                                break;
                        }
                        int i = indexStart;
                        do
                        {
                            _Routines.LoadRowFromReader(sceresultset, dt_meny);

                            i++;
                            if (!sceresultset.Read())
                                break;
                        } while (i < indexEnd);
                    }

                    dt_meny.EndLoadData();
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

		#region Get metody

		public Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable GetDataByHlavni(bool mena_hlavni)
		{
			Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable dt = new Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST097 WHERE (mena_hlavni = @mena_hlavni)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Int32, Value = mena_hlavni });
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

		public virtual Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable GetDataByMenaID(string mena_ID)
		{
			Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable dt = new Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST097 WHERE (mena_ID = @mena_ID)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.Int32, Value = (string.IsNullOrEmpty(mena_ID) ? (object)DBNull.Value : mena_ID) });
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

		#endregion

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
			command.CommandText = "INSERT INTO CZMST097 (" + 
				" mena_ID, mena_text, mena_hlavni, mena_kurz, mena_kurzDatum" + 
				" ) VALUES (" + 
				" @mena_ID, @mena_text, @mena_hlavni, @mena_kurz, @mena_kurzDatum " + 
				" )";


			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });
		}

		public void InitializeCommandUpdate(SQLiteCommand command)
		{
			command.CommandText = "UPDATE CZMST097 SET " + 
				" mena_ID = @mena_ID," + 
				" mena_text = @mena_text," + 
				" mena_hlavni = @mena_hlavni," + 
				" mena_kurz = @mena_kurz," + 
				" mena_kurzDatum = @mena_kurzDatum " + 
				" WHERE (mena_ID = @mena_ID_Orig)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_text", DbType = System.Data.DbType.String, SourceColumn = "mena_text" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_hlavni", DbType = System.Data.DbType.Boolean, SourceColumn = "mena_hlavni" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurz", DbType = System.Data.DbType.Decimal, SourceColumn = "mena_kurz" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_kurzDatum", DbType = System.Data.DbType.DateTime, SourceColumn = "mena_kurzDatum" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID_Orig", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = System.Data.DataRowVersion.Original });
		}

		public void InitializeCommandDelete(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST097 WHERE (mena_ID = @mena_ID)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@mena_ID",
				DbType = System.Data.DbType.String,
				SourceColumn = "mena_ID",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST097";
		}


		#endregion


		#endregion

		public int Fill(Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST097";
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
	}
}
