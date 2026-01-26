using System;
using System.Data;
using System.Data.SQLite;

namespace Fask.SQLiteDBs.Controllers
{
	/// <summary>
	/// Controller pro ciselnik zbozi : CZMST095, CZMST095M
	/// </summary>
	public class SQLite_Controller_Zbozi : SQLite_Controller
	{
		//private Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095TableAdapter ta_zbozi = null;
		//internal Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095TableAdapter Ta_zbozi
		//{
		//    get
		//    {
		//        if (ta_zbozi == null)
		//        {
		//            ta_zbozi = new Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095TableAdapter();
		//            ta_zbozi.Connection = this.Connection;
		//        }
		//        return ta_zbozi;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095MTableAdapter ta_zbozi_M = null;
		//internal Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095MTableAdapter Ta_zbozi_M
		//{
		//    get
		//    {
		//        if (ta_zbozi_M == null)
		//        {
		//            ta_zbozi_M = new Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095MTableAdapter();
		//            ta_zbozi_M.Connection = this.Connection;
		//        }
		//        return ta_zbozi_M;
		//    }
		//}

		#region c'tors
		//public SQLite_Controller_Zbozi()
		//    : base(Main.CiselnikZboziDB)
		//{
		//}

		public SQLite_Controller_Zbozi(string sqliteFileName)
			: base(sqliteFileName)
		{
		}

		public SQLite_Controller_Zbozi(SQLiteConnection sqliteconnection)
			: base(sqliteconnection)
		{
		}

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();

		//    ta_zbozi = new Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095TableAdapter();
		//    ta_zbozi_M = new Fask.SQLiteDBs.DataSets.ZboziTableAdapters.CZMST095MTableAdapter();

		//    ta_zbozi.Connection = this.Connection;
		//    ta_zbozi_M.Connection = this.Connection;

		//}
		#endregion

		public override void Dispose()
		{
			// disposing adapters ...
			//if (ta_zbozi != null)
			//    ta_zbozi.Dispose();
			//if (ta_zbozi_M != null)
			//    ta_zbozi_M.Dispose();

			//this.DisposeObject(ta_zbozi);
			//this.DisposeObject(ta_zbozi_M);

			//this.ta_zbozi = null;
			//this.ta_zbozi_M = null;

			base.Dispose();
		}


		#region Puvodni dotaky , !!Zrevidovat!!

		#region SQL Dotazy

		/// <summary>
		/// Pocet vsech polozek
		/// </summary>
		/// <returns>pocet polozek</returns>
		public int Get_Count_All()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "select count(*) from czmst095";
					return Convert.ToInt32(command.ExecuteScalar());
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

		/// <summary>
		/// Nacte data do tabulky zbozi
		/// </summary>
		/// <param name="tbl_zbozi">kam nacist zbozi.</param>
		/// <param name="orderBy">razeni (jeli prazdne nebo null, tak bez razeni)</param>
		/// <param name="indexStart">index prvni polozky</param>
		/// <param name="indexEnd">index posledni polozky</param>
		public void Load_All(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable tbl_zbozi, string orderBy, int indexStart, int indexEnd)
		{
			try
			{
				tbl_zbozi.BeginLoadData();
				tbl_zbozi.Clear();

				Connection_Open();

				string cmdselect = "Select * from czmst095";
				string cmdsort = orderBy;

				string selectcommandfinal = cmdselect + cmdsort;

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = selectcommandfinal;

					using (var reader = command.ExecuteReader())
					{
						if (true)
						{
							// najeti na startovaci index pro sqlite
							for (int index = 0; index <= indexStart; index++)
							{
								if (!reader.Read())
									break;
							}

							int i = indexStart;
							//for (int i = indexStart; i < indexEnd; i++)
							do
							{
								_Routines.LoadRowFromReader(reader, tbl_zbozi);

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
				Logging.ExceptionHandler2.Handle(ex);
			}
			finally
			{
				Connection_Close();

				tbl_zbozi.EndLoadData();
			}
		}

		/// <summary>
		/// Nacte data do tabulky zbozi filtrovane dle skladu a serazene
		/// </summary>
		/// <param name="tbl_zbozi">kam nacist zbozi.</param>
		/// <param name="skl_id">filtr skladu</param>
		/// <param name="orderBy">razeni (jeli prazdne nebo null, tak bez razeni)</param>
		/// <param name="indexStart">index prvni polozky</param>
		/// <param name="indexEnd">index posledni polozky</param>
		public void Load_By_Sklid(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable tbl_zbozi, string skl_id, string orderBy, int indexStart, int indexEnd)
		{
			try
			{
				tbl_zbozi.BeginLoadData();
				tbl_zbozi.Clear();

				Connection_Open();

				string cmdselect = "Select * from czmst095";
				string cmdwhere = " where SKL_ID=@sklid";
				string cmdsort = orderBy;

				string selectcommandfinal = cmdselect + cmdwhere + cmdsort;

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = selectcommandfinal;
					command.Parameters.AddWithValue("@sklid", skl_id);

					using (var reader = command.ExecuteReader())
					{
						if (true)
						{
							// najeti na startovaci index pro sqlite
							for (int index = 0; index <= indexStart; index++)
							{
								if (!reader.Read())
									break;
							}

							int i = indexStart;
							//for (int i = indexStart; i < indexEnd; i++)
							do
							{
								_Routines.LoadRowFromReader(reader, tbl_zbozi);

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
				Logging.ExceptionHandler2.Handle(ex);
			}
			finally
			{
				Connection_Close();

				tbl_zbozi.EndLoadData();
			}
		}

		/// <summary>
		/// Pocet polozek konkretniho skladu
		/// </summary>
		/// <param name="skl_id">id skladu</param>
		/// <returns>pocet polozek</returns>
		public int Get_Count_By_Sklid(string skl_id)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "select count(*) from czmst095 where SKL_ID=@sklid";
					command.Parameters.AddWithValue("@sklid", skl_id.Trim());
					return Convert.ToInt32(command.ExecuteScalar());
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

		public Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable GetTableByConfirmValue(string CZ_CarKod, string VNDITNUM, string confirmValueColumnName, string Kod)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					string cmdText = "SELECT * FROM czmst095 WHERE (cz_carkod = @cz_carkod) AND " + confirmValueColumnName + " = @param" +
						" UNION " +
						" SELECT * FROM czmst095 WHERE (vnditnum = @vnditnum) AND " + confirmValueColumnName + " = @param";

					command.CommandText = cmdText;

					command.Parameters.Clear();
					command.Parameters.AddWithValue("@cz_carkod", CZ_CarKod);
					command.Parameters.AddWithValue("@vnditnum", VNDITNUM);
					command.Parameters.AddWithValue("@param", Kod);

					using (var reader = command.ExecuteReader())
					{
						//Tabulka
						Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable table = new Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable();
						table.Load(reader);
						return table;
					}
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return new Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable(); // prazdna tabulka
			}
			finally
			{
				Connection_Close();
			}
		}

		public Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable GetTableByBoth(string CZ_CarKod, string VNDITNUM)
		{
			try
			{
				Connection_Open();

				//Prikaz
				// Uprava funkcnosti 22.3.2013 v ICT dle konzultace a skutecneho pozadavku
				// 1) vyhledat existenci dle cz_carkod and vnditnum 
				// 2) pokud existuje prave 1 tak potvrdit,
				//    pokud neexistuje nebo vice, tak zobrazit list zbozi na zaklade podminky pro vyber dle nastaveni ...  
				//string cmdText = "SELECT * FROM czmst095 WHERE (cz_carkod = @cz_carkod OR vnditnum = @vnditnum)";
				using (var cmd = this.Connection.CreateCommand())
				{
					string cmdText = "SELECT * FROM czmst095 WHERE (cz_carkod = @cz_carkod AND vnditnum = @vnditnum)";

					cmd.CommandText = cmdText;

					cmd.Parameters.AddWithValue("@cz_carkod", CZ_CarKod);
					cmd.Parameters.AddWithValue("@vnditnum", VNDITNUM);

					using (var reader = cmd.ExecuteReader())
					{
						//Tabulka
						Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable table = new Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable();
						table.Load(reader);
						return table;
					}
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return new Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable();
			}
		}

		#endregion

		#region SQL Manipulace pro Volny pohyb

		/// <summary>
		/// Provede akutalizaci hodnoty ITEMDESC pro volny pohyb z ciselniku zbozi
		/// </summary>
		/// <param name="dt_di">data volneho pohybu</param>
		public void UpdateItemDesc(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dt_di)
		{
			try
			{
				Connection_Open();

				using (var zcommand = this.Connection.CreateCommand())
				{
					zcommand.CommandText = "Select ITEMDESC from czmst095 where ITEMNMBR=@ITEMNMBR";
					//System.Data.SqlServerCe.SqlCeParameter zparam = zcommand.Parameters.Add("@ITEMNMBR", SqlDbType.NVarChar, _prodejTable.CZMST_DI.ITEMNMBRColumn.MaxLength, "ITEMNMBR");
					System.Data.SQLite.SQLiteParameter zparam = zcommand.Parameters.AddWithValue("@ITEMNMBR", "itemnmbr");
					foreach (var diro in dt_di.Rows)
					{
						try
						{
							var dir = (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow)diro;

							// načte se název položky pouze v případě, že je null
							if (dir.IsITEMDESCNull())
							{
								zcommand.Parameters["@ITEMNMBR"].Value = dir.ITEMNMBR;
								object itemdesc = zcommand.ExecuteScalar();
								//if (itemdesc == null)
								//    continue;
								//if (itemdesc is System.DBNull)
								//    continue;
								if (itemdesc is string) // pretypuje se pouze, pokud je vysledek string...
									dir.ITEMDESC = (string)(zcommand.ExecuteScalar());
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

		#endregion

		#region SQL Dotazy pro Vydej

		public void FillBySpecificCondition(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable globalTable, string searchCondition, string vnditnum, string cz_carkod)
		{
			try
			{
				this.Connection_Open();

				//Prikaz - rozvetveni dle toho, podle ceho se ma hledat - opet musi odpovidat konfiguraci a xml
				using (var cmd = this.Connection.CreateCommand())
				{

					if (searchCondition == "CZ_CARKOD,VNDITNUM")
					{
						//string cmdText = "SELECT * FROM czmst095 WHERE cz_carkod = @cz_carkod OR vnditnum = @vnditnum";
						string cmdText =
							" SELECT * FROM czmst095 WHERE cz_carkod = @cz_carkod " +
							" UNION " +
							" SELECT * FROM czmst095 WHERE vnditnum = @vnditnum";

						cmd.CommandText = cmdText;
						cmd.Parameters.Clear();
						cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter("@cz_carkod", cz_carkod));
						cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter("@vnditnum", vnditnum));
					}
					else if (searchCondition == "CZ_CARKOD")
					{
						string cmdText = "SELECT * FROM czmst095 WHERE cz_carkod = @cz_carkod";

						cmd.CommandText = cmdText;
						cmd.Parameters.Clear();
						cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter("@cz_carkod", cz_carkod));
					}
					else if (searchCondition == "VNDITNUM")
					{
						string cmdText = "SELECT * FROM czmst095 WHERE vnditnum = @vnditnum";

						cmd.CommandText = cmdText;
						cmd.Parameters.Clear();
						cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter("@vnditnum", vnditnum));
					}
					else
					{
						//throw new ApplicationException(Fask.Localization.Localization.Vydej3ListZboziChybneZadanySloupecProVyhledavani);
						throw new Exception("Chybně zadaný sloupec/sloupce pro vyhledávání v seznamu zboží!");
					}

					//Citac a tabulka
					using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
					{
						globalTable.Load(reader);
					}

				} // end using command 
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				this.Connection_Close();
			}
		}

		#endregion

		#region Volny pohyb Global

		public decimal? GetPrice(string ITEMNMBR, string mena_ID, int? PRICEX)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT PRICE FROM CZMST095M WHERE (ITEMNMBR = @ITEMNMBR) AND (mena_ID = @mena_ID) AND (PRICEX = @PRICEX)";
					command.CommandType = System.Data.CommandType.Text;


					var param = new System.Data.SQLite.SQLiteParameter();
					param.ParameterName = "@ITEMNMBR";
					param.DbType = System.Data.DbType.String;

					if (ITEMNMBR == null)
						param.Value = DBNull.Value;
					else
						param.Value = ITEMNMBR;

					command.Parameters.Add(param);

					param = new System.Data.SQLite.SQLiteParameter();
					param.ParameterName = "@mena_ID";
					param.DbType = System.Data.DbType.String;

					if (mena_ID == null)
						param.Value = DBNull.Value;
					else
						param.Value = mena_ID;

					command.Parameters.Add(param);

					param = new System.Data.SQLite.SQLiteParameter();
					param.ParameterName = "@PRICEX";
					param.DbType = System.Data.DbType.Int32;

					if (PRICEX.HasValue)
						param.Value = PRICEX;
					else
						param.Value = DBNull.Value;

					command.Parameters.Add(param);

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return new global::System.Nullable<decimal>();
					}
					else
					{
						return new global::System.Nullable<decimal>(((decimal)(returnValue)));
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

		#endregion

		#endregion

		#region Dotazy zrevidovane misto TableAdapteru

		#region Universal Metody

		private int Fill_Universal(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, SQLiteParameter[] Parameter, string Select)
		{
			try
			{
				Connection_Open();
				
				dataTable.Clear();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = Select;
						if (Parameter != null)
						{
						adapter.SelectCommand.Parameters.AddRange(Parameter);
						}

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

		private int? CountBy_Universal(SQLiteParameter[] Parameter, string Select)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = Select;
					command.CommandType = System.Data.CommandType.Text;

					command.Parameters.AddRange(Parameter);

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
				return -1;
			}
			finally
			{
				Connection_Close();
			}
		} 
		#endregion

		#region Použi Fill Universal, ale zakomentovany i puvodny kód

		#region PolozkacisloLike

		public int FillByPolozkacisloLike(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string ITEMNMBR)
		{

			string where = @"SELECT * FROM CZMST095 WHERE (ITEMNMBR LIKE @ITEMNMBR)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };

			return Fill_Universal(dataTable, par, where);

			//try
			//{
			//    Connection_Open();

			//    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
			//    {
			//        using (var command = this.Connection.CreateCommand())
			//        {
			//            adapter.SelectCommand = command;
			//            adapter.SelectCommand.Connection = this.Connection;
			//            adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST095 WHERE ITEMNMBR LIKE @ITEMNMBR";
			//            adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
			//            int returnValue;

			//            returnValue = adapter.Fill(dataTable);

			//            return returnValue;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    throw ex;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		public Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable GetDataByPolozkacisloLike(string ITEMNMBR)
		{
			//!!!!!!!!!!!!!!
			//!!!!!!!!!!!!!!
			//POZOR, je použito v cyklu, kde pred cyklem se otevře spojeni, a po se zase zavře, proto se to neřeši tady vevnitř
			//!!!!!!!!!!!!!!
			//!!!!!!!!!!!!!!


			Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable = new Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable();
			try
			{
				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST095 WHERE ITEMNMBR LIKE @ITEMNMBR";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });

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
		}

		#endregion

		#region PolozkacisloSkladLike

		public Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable GetDataByPolozkacisloSkladLike(string ITEMNMBR, string SKL_ID)
		{
			Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dt = new Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable();
			int a = this.FillByPolozkacisloSkladLike(dt, ITEMNMBR, SKL_ID);
			return dt;
		}

		public int FillByPolozkacisloSkladLike(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string ITEMNMBR, string SKL_ID)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (SKL_ID = @SKL_ID) AND (ITEMNMBR LIKE @ITEMNMBR)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };
			par[1] = new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID };

			return Fill_Universal(dataTable, par, where);

			//try
			//{
			//    Connection_Open();

			//    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
			//    {
			//        using (var command = this.Connection.CreateCommand())
			//        {
			//            adapter.SelectCommand = command;
			//            adapter.SelectCommand.Connection = this.Connection;
			//            adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST095 WHERE (ITEMNMBR LIKE @ITEMNMBR) AND (SKL_ID = @SKL_ID)";
			//            adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
			//            adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
			//            int ReturnValue;
			//            ReturnValue = adapter.Fill(dataTable);

			//            return ReturnValue;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    throw ex;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		#endregion

		public int FillByPolozkaCisloAndOdbId(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string ITEMNMBR, string ODB_ID)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (ODB_ID = @ODB_ID) AND (ITEMNMBR LIKE @ITEMNMBR)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };
			par[1] = new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID };

			return Fill_Universal(dataTable, par, where);
			//try
			//{
			//    Connection_Open();

			//    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
			//    {
			//        using (var command = this.Connection.CreateCommand())
			//        {
			//            adapter.SelectCommand = command;
			//            adapter.SelectCommand.Connection = this.Connection;
			//            adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST095 WHERE (ITEMNMBR LIKE @ITEMNMBR) AND (ODB_ID = @ODB_ID)";
			//            adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
			//            adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID });
			//            int returnValue;

			//            returnValue = adapter.Fill(dataTable);

			//            return returnValue;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    throw ex;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		public int FillByPolozkaCodeLike(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string ITEMCODE)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (ITEMCODE LIKE @ITEMCODE)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE };

			return Fill_Universal(dataTable, par, where);

			//try
			//{
			//    Connection_Open();

			//    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
			//    {
			//        using (var command = this.Connection.CreateCommand())
			//        {
			//            adapter.SelectCommand = command;
			//            adapter.SelectCommand.Connection = this.Connection;
			//            adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST095 WHERE ITEMCODE LIKE @ITEMCODE";
			//            adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE });
			//            int returnValue;

			//            returnValue = adapter.Fill(dataTable);

			//            return returnValue;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    throw ex;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		public int FillByPolozkaCodeOdbIdLike(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string ITEMCODE, string ODB_ID)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (ODB_ID = @ODB_ID) AND (ITEMCODE LIKE @ITEMCODE)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE };
			par[1] = new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID };

			return Fill_Universal(dataTable, par, where);

			//try
			//{
			//    Connection_Open();

			//    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
			//    {
			//        using (var command = this.Connection.CreateCommand())
			//        {
			//            adapter.SelectCommand = command;
			//            adapter.SelectCommand.Connection = this.Connection;
			//            adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST095 WHERE (ITEMCODE LIKE @ITEMCODE) AND (ODB_ID = @ODB_ID)";
			//            adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE });
			//            adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID });
			//            int ReturnValue;
			//            ReturnValue = adapter.Fill(dataTable);

			//            return ReturnValue;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    throw ex;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		public int FillByPolozkaCodeSkladLike(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string SKL_ID, string ITEMCODE)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (SKL_ID = @SKL_ID) AND (ITEMCODE LIKE @ITEMCODE)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE };
			par[1] = new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID };

			return Fill_Universal(dataTable, par, where);

			//try
			//{
			//    Connection_Open();

			//    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
			//    {
			//        using (var command = this.Connection.CreateCommand())
			//        {
			//            adapter.SelectCommand = command;
			//            adapter.SelectCommand.Connection = this.Connection;
			//            adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST095 WHERE (SKL_ID = @SKL_ID) AND (ITEMCODE LIKE @ITEMCODE)";
			//            adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE });
			//            adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
			//            int ReturnValue;
			//            ReturnValue = adapter.Fill(dataTable);

			//            return ReturnValue;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    throw ex;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		#endregion

		#region Get Count s CountBy_Universal

		public int? CountByPolozkacislo(string ITEMNMBR)
		{
			string where = "SELECT COUNT(*) FROM CZMST095 WHERE ITEMNMBR like @ITEMNMBR";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };
			return CountBy_Universal(par, where);

			//try
			//{
			//    Connection_Open();

			//    using (var command = this.Connection.CreateCommand())
			//    {

			//        command.CommandText = "SELECT COUNT(*) FROM CZMST095 WHERE ITEMNMBR like @ITEMNMBR";
			//        command.CommandType = System.Data.CommandType.Text;

			//        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });

			//        object returnValue = command.ExecuteScalar();

			//        if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
			//        {
			//            return null;
			//        }
			//        else
			//        {
			//            Int64 pp = (Int64)returnValue;
			//            return (int?)pp;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    return -1;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		public int? CountByPolozkacisloOdbId(string ITEMNMBR, string ODB_ID)
		{
			string where = "SELECT COUNT(*) FROM CZMST095 WHERE (ITEMNMBR LIKE @ITEMNMBR) AND (ODB_ID = @ODB_ID)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };
			par[1] = new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID };
			return CountBy_Universal(par, where);

			//try
			//{
			//    Connection_Open();

			//    using (var command = this.Connection.CreateCommand())
			//    {

			//        command.CommandText = "SELECT COUNT(*) FROM CZMST095 WHERE (ITEMNMBR LIKE @itemnmbr) AND (ODB_ID = @odb_id)";
			//        command.CommandType = System.Data.CommandType.Text;

			//        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@itemnmbr", DbType = System.Data.DbType.String, Value = itemnmbr == null ? (object)DBNull.Value : itemnmbr });
			//        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@odb_id", DbType = System.Data.DbType.String, Value = odb_id == null ? (object)DBNull.Value : odb_id });

			//        object returnValue = command.ExecuteScalar();

			//        if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
			//        {
			//            return null;
			//        }
			//        else
			//        {
			//            Int64 pp = (Int64)returnValue;
			//            return (int?)pp;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    return -1;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		public int? CountByPolozkacisloSklad(string ITEMNMBR, string SKL_ID)
		{
			string where = "SELECT COUNT(*) FROM CZMST095 WHERE (ITEMNMBR LIKE @ITEMNMBR) AND (ODB_ID = @ODB_ID)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };
			par[1] = new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID };
			return CountBy_Universal(par, where);

			//try
			//{
			//    Connection_Open();

			//    using (var command = this.Connection.CreateCommand())
			//    {

			//        command.CommandText = "SELECT COUNT(*) FROM CZMST095 where itemnmbr like @itemnmbr and skl_id = @skl_id";
			//        command.CommandType = System.Data.CommandType.Text;

			//        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@itemnmbr", DbType = System.Data.DbType.String, Value = itemnmbr == null ? (object)DBNull.Value : itemnmbr });
			//        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_id", DbType = System.Data.DbType.String, Value = skl_id == null ? (object)DBNull.Value : skl_id });

			//        object returnValue = command.ExecuteScalar();

			//        if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
			//        {
			//            return null;
			//        }
			//        else
			//        {
			//            Int64 pp = (Int64)returnValue;
			//            return (int?)pp;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    return -1;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		public int? CountByPolozkaCode(string ITEMCODE)
		{
			string where = "SELECT COUNT(*) FROM CZMST095 WHERE (ITEMCODE LIKE @ITEMCODE)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE };
			return CountBy_Universal(par, where);

			//try
			//{
			//    Connection_Open();

			//    using (var command = this.Connection.CreateCommand())
			//    {

			//        command.CommandText = "SELECT COUNT(*) FROM CZMST095 WHERE ITEMCODE LIKE @ITEMCODE)";
			//        command.CommandType = System.Data.CommandType.Text;

			//        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE });

			//        object returnValue = command.ExecuteScalar();

			//        if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
			//        {
			//            return null;
			//        }
			//        else
			//        {
			//            Int64 pp = (Int64)returnValue;
			//            return (int?)pp;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    return -1;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		public int? CountByPolozkaCodeOdbId(string ODB_ID, string ITEMCODE)
		{
			string where = "SELECT COUNT(*) FROM CZMST095 WHERE (ITEMCODE LIKE @ITEMCODE) AND (ODB_ID = @ODB_ID)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE };
			par[1] = new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID };
			return CountBy_Universal(par, where);

			//try
			//{
			//    Connection_Open();

			//    using (var command = this.Connection.CreateCommand())
			//    {

			//        command.CommandText = "SELECT COUNT(*) FROM CZMST095 where(ODB_ID = @ODB_ID) AND (ITEMCODE LIKE @ITEMCODE)";
			//        command.CommandType = System.Data.CommandType.Text;

			//        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID });
			//        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE });

			//        object returnValue = command.ExecuteScalar();

			//        if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
			//        {
			//            return null;
			//        }
			//        else
			//        {
			//            Int64 pp = (Int64)returnValue;
			//            return (int?)pp;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    return -1;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		public int? CountByPolozkaCodeSklad(string SKL_ID, string ITEMCODE)
		{
			string where = "SELECT COUNT(*) FROM CZMST095 WHERE (ITEMCODE LIKE @ITEMCODE) AND (SKL_ID = @SKL_ID)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE };
			par[1] = new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID };
			return CountBy_Universal(par, where);

			//try
			//{
			//    Connection_Open();

			//    using (var command = this.Connection.CreateCommand())
			//    {

			//        command.CommandText = "SELECT COUNT(*) FROM CZMST095 WHERE (SKL_ID = @skl_id) AND (ITEMCODE LIKE @ITEMCODE)";
			//        command.CommandType = System.Data.CommandType.Text;

			//        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE });
			//        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@skl_id", DbType = System.Data.DbType.String, Value = skl_id == null ? (object)DBNull.Value : skl_id });

			//        object returnValue = command.ExecuteScalar();

			//        if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
			//        {
			//            return null;
			//        }
			//        else
			//        {
			//            Int64 pp = (Int64)returnValue;
			//            return (int?)pp;
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Logging.Log.Write(ex);
			//    return -1;
			//}
			//finally
			//{
			//    Connection_Close();
			//}
		}

		public int? CountByNazev(string ITEMDESC)
		{
			string where = "SELECT COUNT(*) FROM CZMST095 WHERE (ITEMDESC LIKE @ITEMDESC)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = ITEMDESC == null ? (object)DBNull.Value : ITEMDESC };
			return CountBy_Universal(par, where);
		}

		public  int? CountByNazevOdbId(string ITEMDESC, string ODB_ID)
		{
			string where = "SELECT COUNT(*) FROM CZMST095 WHERE (ITEMDESC LIKE @ITEMDESC) AND (ODB_ID = @ODB_ID)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = ITEMDESC == null ? (object)DBNull.Value : ITEMDESC };
			par[1] = new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID };
			return CountBy_Universal(par, where);
		}

		public int? CountByNazevSklad(string ITEMDESC, string SKL_ID)
		{
			string where = "SELECT COUNT(*) FROM CZMST095 WHERE (ITEMDESC LIKE @ITEMDESC) AND (SKL_ID = @SKL_ID)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = ITEMDESC == null ? (object)DBNull.Value : ITEMDESC };
			par[1] = new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID };
			return CountBy_Universal(par, where);
		}

		#endregion

		#region Pouze použiti FillUniversal

		#region Nazev

		public int FillByNazev(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string ITEMDESC)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (ITEMDESC LIKE @ITEMDESC)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = ITEMDESC == null ? (object)DBNull.Value : ITEMDESC };
			return Fill_Universal(dataTable, par, where);
		}

		public int FillByNazevOdbID(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string ITEMDESC, string ODB_ID)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (ITEMDESC LIKE @ITEMDESC) AND (ODB_ID = @ODB_ID)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = ITEMDESC == null ? (object)DBNull.Value : ITEMDESC };
			par[1] = new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID };
			return Fill_Universal(dataTable, par, where);
		}

		public int FillByNazevSklad(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string ITEMDESC, string SKL_ID)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (ITEMDESC LIKE @ITEMDESC) AND (SKL_ID = @SKL_ID)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = ITEMDESC == null ? (object)DBNull.Value : ITEMDESC };
			par[1] = new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID };
			return Fill_Universal(dataTable, par, where);
		}

		#endregion

		public int Fill(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable)
		{
			string where = @"SELECT * FROM CZMST095 ";
			return Fill_Universal(dataTable, null, where);
		}

		public int Fill_BySKL_ID(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string SKL_ID)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (SKL_ID = @SKL_ID) order by ITEMDESC";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID };
			return Fill_Universal(dataTable, par, where);
		}

		public int FillByCarKodSarze(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string carkod)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (VNDITNUM = @carkod) " +
				" UNION " + "SELECT * FROM CZMST095 AS CZMST095_1 WHERE (CZ_CarKod = @carkod) " +
				" UNION " + "SELECT * FROM CZMST095 AS CZMST095_2 WHERE (SERLTNUM = @carkod)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@carkod", DbType = System.Data.DbType.String, Value = carkod == null ? (object)DBNull.Value : carkod };
			return Fill_Universal(dataTable, par, where);
		}

		public int FillByCarKodOdbIdSarze(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string carkod, string ODB_ID)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (VNDITNUM = @carkod) AND (ODB_ID = @ODB_ID) " +
				" UNION " + "SELECT * FROM CZMST095 AS CZMST095_1 WHERE (CZ_CarKod = @carkod) AND (ODB_ID = @ODB_ID) " +
				" UNION " + "SELECT * FROM CZMST095 AS CZMST095_2 WHERE (SERLTNUM = @carkod) AND (ODB_ID = @ODB_ID)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@carkod", DbType = System.Data.DbType.String, Value = carkod == null ? (object)DBNull.Value : carkod };
			par[1] = new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID };
			return Fill_Universal(dataTable, par, where);
		}

		public int FillByCarKodSkladSarze(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string carkod, string SKL_ID)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (VNDITNUM = @carkod) AND (SKL_ID = @SKL_ID) " +
				" UNION " + "SELECT * FROM CZMST095 AS CZMST095_1 WHERE (CZ_CarKod = @carkod) AND (SKL_ID = @SKL_ID) " +
				" UNION " + "SELECT * FROM CZMST095 AS CZMST095_2 WHERE (SERLTNUM = @carkod) AND (SKL_ID = @SKL_ID)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@carkod", DbType = System.Data.DbType.String, Value = carkod == null ? (object)DBNull.Value : carkod };
			par[1] = new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID };
			return Fill_Universal(dataTable, par, where);
		}

		public int FillByCarKod(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string carkod)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (VNDITNUM = @carkod) " +
				" UNION " + "SELECT * FROM CZMST095 AS CZMST095_1 WHERE (CZ_CarKod = @carkod) ";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@carkod", DbType = System.Data.DbType.String, Value = carkod == null ? (object)DBNull.Value : carkod };
			return Fill_Universal(dataTable, par, where);
		}

		public int FillByCarKodOdbId(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string carkod, string ODB_ID)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (VNDITNUM = @carkod) AND (ODB_ID = @ODB_ID) " +
				" UNION " + "SELECT * FROM CZMST095 AS CZMST095_1 WHERE (CZ_CarKod = @carkod) AND (ODB_ID = @ODB_ID) ";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@carkod", DbType = System.Data.DbType.String, Value = carkod == null ? (object)DBNull.Value : carkod };
			par[1] = new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID };
			return Fill_Universal(dataTable, par, where);
		}

		public int FillByCarKodSklad(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dataTable, string carkod, string SKL_ID)
		{
			string where = @"SELECT * FROM CZMST095 WHERE (VNDITNUM = @carkod) AND (SKL_ID = @SKL_ID) " +
				" UNION " + "SELECT * FROM CZMST095 AS CZMST095_1 WHERE (CZ_CarKod = @carkod) AND (SKL_ID = @SKL_ID) ";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@carkod", DbType = System.Data.DbType.String, Value = carkod == null ? (object)DBNull.Value : carkod };
			par[1] = new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID };
			return Fill_Universal(dataTable, par, where);
		}


		#endregion

		#region UPDATE

		#region CZMST095

		internal int Update_CZMST095(object data)
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
					InitializeCommandInsert_CZMST095(commandInsert);
					//InitializeCommandUpdate_CZMST095(commandUpdate);
					//InitializeCommandDelete_CZMST095(commandDelete);
					InitializeCommandSelect_CZMST095(commandSelect);

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
				//Logging.Log.Write(ex);
				Logging.ExceptionHandler2.Handle(ex);

				try
				{
					if (transaction != null)
						transaction.Rollback();
				}
				catch (Exception exTransaction)
				{
					//Logging.Log.Write(exTransaction);
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

		public void InitializeCommandInsert_CZMST095(SQLiteCommand command)
		{
			command.CommandText = @" INSERT INTO CZMST095 " + 
				" (ITEMNMBR, ITEMDESC, VNDITNUM, CZ_CarKod, LOCNCODE," + 
				" QTY, QTYPACK, TAXRATE, PRICE0, PRICE1," + 
				" PRICE2, PRICE3, PRICE4, PRICE5, CZ_SerNum_Track," + 
				" CZ_SerNum_Delka, CZ_Rez1_Track, CZ_Rez2_Track, CZ_Rez3_Track, CZ_Rez4_Track," + 
				" DEX_ROW_ID, SKL_ID, MJ, DMJ, REZ1," +
				" ITEMCODE, ODB_ID, REZ2, REZ3, REZ4," + 
				" MENA_ID, SERLTNUM, WEIGHT, CZ_Expirace_Track, EXPIRACE " + 
				" ) VALUES( " + 
				" @ITEMNMBR, @ITEMDESC, @VNDITNUM, @CZ_CarKod, @LOCNCODE," + 
				" @QTY, @QTYPACK, @TAXRATE, @PRICE0, @PRICE1," + 
				" @PRICE2, @PRICE3, @PRICE4, @PRICE5, @CZ_SerNum_Track," + 
				" @CZ_SerNum_Delka, @CZ_Rez1_Track, @CZ_Rez2_Track, @CZ_Rez3_Track, @CZ_Rez4_Track," + 
				" @DEX_ROW_ID, @SKL_ID, @MJ, @DMJ, @REZ1," + 
				" @ITEMCODE, @ODB_ID, @REZ2, @REZ3, @REZ4," +
				" @MENA_ID, @SERLTNUM, @WEIGHT, @CZ_Expirace_Track, @EXPIRACE" + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", SourceVersion = DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTY", DbType = System.Data.DbType.Decimal, SourceColumn = "QTY", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TAXRATE", DbType = System.Data.DbType.Decimal, SourceColumn = "TAXRATE", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE0", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE0", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE1", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE1", SourceVersion = DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE2", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE2", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE3", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE3", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE4", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE4", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE5", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE5", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Track", SourceVersion = DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SerNum_Delka", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Rez1_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez1_Track", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Rez2_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez2_Track", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Rez3_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez3_Track", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Rez4_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Rez4_Track", SourceVersion = DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DMJ", DbType = System.Data.DbType.String, SourceColumn = "DMJ", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, SourceColumn = "REZ1", SourceVersion = DataRowVersion.Current });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, SourceColumn = "ODB_ID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ2", DbType = System.Data.DbType.String, SourceColumn = "REZ2", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ3", DbType = System.Data.DbType.String, SourceColumn = "REZ3", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ4", DbType = System.Data.DbType.String, SourceColumn = "REZ4", SourceVersion = DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MENA_ID", DbType = System.Data.DbType.String, SourceColumn = "MENA_ID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", SourceVersion = DataRowVersion.Current });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Expirace_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Expirace_Track", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@EXPIRACE", DbType = System.Data.DbType.DateTime, SourceColumn = "EXPIRACE", SourceVersion = DataRowVersion.Current });

		}

		//public void InitializeCommandUpdate_CZMST095(SQLiteCommand command)
		//{
		//	command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
		//}

		//public void InitializeCommandDelete_CZMST095(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST_SE WHERE (guid = @guid)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@guid",
		//		DbType = System.Data.DbType.Guid,
		//		SourceColumn = "guid",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_CZMST095(SQLiteCommand command)
		{
			command.CommandText = "SELECT * FROM CZMST095";
		}


		#endregion

		#endregion

		#region CZMST095M

		internal int Update_CZMST095M(object data)
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
					InitializeCommandInsert_CZMST095M(commandInsert);
					//InitializeCommandUpdate_CZMST095M(commandUpdate);
					//InitializeCommandDelete_CZMST095M(commandDelete);
					InitializeCommandSelect_CZMST095M(commandSelect);

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
				//Logging.Log.Write(ex);
				Logging.ExceptionHandler2.Handle(ex);

				try
				{
					if (transaction != null)
						transaction.Rollback();
				}
				catch (Exception exTransaction)
				{
					//Logging.Log.Write(exTransaction);
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

		public void InitializeCommandInsert_CZMST095M(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST095M (" +
				" ITEMNMBR, mena_ID, PRICE, PRICEX, DEX_ROW_ID " +
				" ) VALUES( " +
				" @ITEMNMBR, @mena_ID, @PRICE, @PRICEX, @DEX_ROW_ID" + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, SourceColumn = "mena_ID", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICE", DbType = System.Data.DbType.Decimal, SourceColumn = "PRICE", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICEX", DbType = System.Data.DbType.Int32, SourceColumn = "PRICEX", SourceVersion = DataRowVersion.Current });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID", SourceVersion = DataRowVersion.Current });

		}

		//public void InitializeCommandUpdate_CZMST095M(SQLiteCommand command)
		//{
		//	command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//	command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
		//}

		//public void InitializeCommandDelete_CZMST095M(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST_SE WHERE (guid = @guid)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@guid",
		//		DbType = System.Data.DbType.Guid,
		//		SourceColumn = "guid",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_CZMST095M(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST095M";
		}


		#endregion

		#endregion

		#endregion

		#region MyRegion

		public string Get_CZ_SerNum_Track_By_ITEMNMBR(string ITEMNMBR)
		{
						try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT CZ_SerNum_Track FROM CZMST095 WHERE (ITEMNMBR LIKE @ITEMNMBR)"; ;
					command.CommandType = System.Data.CommandType.Text;

					var par = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };

					command.Parameters.Add(par);

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return null;
					}
					else
					{
						string pp = returnValue.ToString();
						return pp;
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

		#endregion

		#endregion

	}
}
