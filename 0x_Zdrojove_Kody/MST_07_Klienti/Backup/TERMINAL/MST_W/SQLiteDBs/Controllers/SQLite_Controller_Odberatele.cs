using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controller pro ciselnik odberatelu : CZMST090
    /// </summary>
    public class SQLite_Controller_Odberatele : SQLite_Controller
    {
		//private Fask.SQLiteDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter ta_odberatele = null;
		//internal Fask.SQLiteDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter Ta_odberatele
		//{
		//    get
		//    {
		//        if (ta_odberatele == null)
		//        {
		//            ta_odberatele = new Fask.SQLiteDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter();
		//            ta_odberatele.Connection = this.Connection;
		//        }
		//        return ta_odberatele;
		//    }
		//}


        #region c'tors
		//public SQLite_Controller_Odberatele()
		//    : base(Main.CiselnikOdberateleDB)
		//{
		//}
        public SQLite_Controller_Odberatele(string sqliteFileName)
            : base(sqliteFileName)
        {
        }

        public SQLite_Controller_Odberatele(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();
            
		//    ta_odberatele = new Fask.SQLiteDBs.DataSets.OdberateleTableAdapters.CZMST090TableAdapter();
            
		//    ta_odberatele.Connection = this.Connection;
		//}


        #endregion

        public override void Dispose()
        {
            // disposing adapters ...
			//if (ta_odberatele != null)
			//    ta_odberatele.Dispose();

			//this.DisposeObject(ta_odberatele);

			//this.ta_odberatele = null;

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
        public void Load(Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable dt_odberatele, string select, int indexStart, int indexEnd)
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
                        dt_odberatele.BeginLoadData();
                        dt_odberatele.Clear();

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
                            _Routines.LoadRowFromReader(sceresultset, dt_odberatele);

                            i++;
                            if (!sceresultset.Read())
                                break;
                        } while (i < indexEnd);
                    }

                    dt_odberatele.EndLoadData();
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

        /// <summary>
        /// Nacte tabulku odberatelu
        /// </summary>
        /// <returns></returns>
        public Fask.SQLiteDBs.DataSets.Odberatele Load()
        {
            var ds_odberatele = new Fask.SQLiteDBs.DataSets.Odberatele();
            this.Fill(ds_odberatele.CZMST090);
            return ds_odberatele;
        }

        /// <summary>
        /// vraci prvni nalezeny zaznam v tabulce odberatelu
        /// </summary>
        /// <returns>Radek nalezeneho odberatele, pokud neni nalezen tak je null</returns>
        public Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row Get_First_Row()
        {
            try
            {
                Connection_Open();

                using (var command = this.Connection.CreateCommand())
                {
                    command.CommandText = "Select * from CZMST090";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return _Routines.LoadRowFromReader(reader, new Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable()) as Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row;
                        }
                    }
                }

                return null;
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


		public object Get_OdbDesc(string odb_id)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT odb_desc FROM CZMST090 WHERE (odb_id = @odb_id)";
					command.CommandType = global::System.Data.CommandType.Text;

					var param = new System.Data.SQLite.SQLiteParameter();
					param.ParameterName = "@odb_id";
					param.DbType = System.Data.DbType.String;

					if (odb_id == null)
						param.Value = DBNull.Value;
					else
						param.Value = odb_id;

					command.Parameters.Add(param);

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return null;
					}
					else
					{
						return ((object)(returnValue));
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

		public int Fill(Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST090";
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

		public Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable GetDataByOdbid(string odb_id)
		{
			Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable dt = new Fask.SQLiteDBs.DataSets.Odberatele.CZMST090DataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST090 WHERE (odb_id=@odb_id)";

						var param = new System.Data.SQLite.SQLiteParameter();
						param.ParameterName = "@odb_id";
						param.DbType = System.Data.DbType.String;

						if (odb_id == null)
							param.Value = DBNull.Value;
						else
							param.Value = odb_id;

						command.Parameters.Add(param);

						adapter.Fill(dt);

						return dt;
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
			command.CommandText = "INSERT INTO CZMST090 (" +
			" odb_id, odb_desc, odb_typ," + 
			" odb_carcode, DEX_ROW_ID, odb_ico," + 
			" mena_ID, odb_misto, odb_ulice," + 
			" odb_cisloOr, odb_psc, odb_dic," + 
			" odb_Odberatel, odb_Dodavatel" +
			" ) VALUES( " + 
			" @odb_id, @odb_desc, @odb_typ," + 
			" @odb_carcode, @DEX_ROW_ID, @odb_ico," + 
			" @mena_ID, @odb_misto, @odb_ulice," + 
			" @odb_cisloOr, @odb_psc, @odb_dic," + 
			" @odb_Odberatel, @odb_Dodavatel)";



			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_id", DbType = System.Data.DbType.String, SourceColumn = "odb_id" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_desc", DbType = System.Data.DbType.String, SourceColumn = "odb_desc" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_typ", DbType = System.Data.DbType.String, SourceColumn = "odb_typ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_carcode", DbType = System.Data.DbType.String, SourceColumn = "odb_carcode" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_ico", DbType = System.Data.DbType.String, SourceColumn = "odb_ico" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_misto", DbType = System.Data.DbType.String, SourceColumn = "odb_misto" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_ulice", DbType = System.Data.DbType.String, SourceColumn = "odb_ulice" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_cisloOr", DbType = System.Data.DbType.String, SourceColumn = "odb_cisloOr" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_psc", DbType = System.Data.DbType.String, SourceColumn = "odb_psc" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_dic", DbType = System.Data.DbType.String, SourceColumn = "odb_dic" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_Odberatel", DbType = System.Data.DbType.Boolean, SourceColumn = "odb_Odberatel" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_Dodavatel", DbType = System.Data.DbType.Boolean, SourceColumn = "odb_Dodavatel" });

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
			command.CommandText = "Select * from CZMST090";
		}


		#endregion


		#endregion


	}
}
