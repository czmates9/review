using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;


namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controler pro praci s daty prijemky.
    /// </summary>
    public class SQLite_Controller_Prijem : SQLite_Controller
    {

		#region TableAdaptery


		//private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PE_SNTableAdapter ta_pe_sn;
		//internal Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PE_SNTableAdapter Ta_pe_sn
		//{
		//    get
		//    {
		//        if (ta_pe_sn == null)
		//        {
		//            ta_pe_sn = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PE_SNTableAdapter();
		//            ta_pe_sn.Connection = this.Connection;
		//        }
		//        return ta_pe_sn;
		//    }
		//    //set { ta_pe_sn = value; }
		//}


		//private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PEHTableAdapter ta_peh;
		//internal Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PEHTableAdapter Ta_peh
		//{
		//    get
		//    {
		//        if (ta_peh == null)
		//        {
		//            ta_peh = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PEHTableAdapter();
		//            ta_peh.Connection = this.Connection;
		//        }
		//        return ta_peh;
		//    }
		//    //set { ta_peh = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter ta_pe;
		//internal Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter Ta_pe
		//{
		//    get
		//    {
		//        if (ta_pe == null)
		//        {
		//            ta_pe = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PETableAdapter();
		//            ta_pe.Connection = this.Connection;
		//        }
		//        return ta_pe;
		//    }
		//    //set { ta_pe = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_FTableAdapter ta_pif;
		//internal Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_FTableAdapter Ta_pif
		//{
		//    get
		//    {
		//        if (ta_pif == null)
		//        {
		//            ta_pif = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_FTableAdapter();
		//            ta_pif.Connection = this.Connection;
		//        }
		//        return ta_pif;
		//    }
		//    //set { ta_pif = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_NasnimanoTableAdapter ta_pi_nasnimano;
		//internal Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_NasnimanoTableAdapter Ta_pi_nasnimano
		//{
		//    get
		//    {
		//        if (ta_pi_nasnimano == null)
		//        {
		//            ta_pi_nasnimano = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PI_NasnimanoTableAdapter();
		//            ta_pi_nasnimano.Connection = this.Connection;
		//        }
		//        return ta_pi_nasnimano;
		//    }
		//    //set { ta_pi_nasnimano = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PIHTableAdapter ta_pih;
		//internal Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PIHTableAdapter Ta_pih
		//{
		//    get
		//    {
		//        if (ta_pih == null)
		//        {
		//            ta_pih = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PIHTableAdapter();
		//            ta_pih.Connection = this.Connection;
		//        }
		//        return ta_pih;
		//    }
		//    //set { ta_pih = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter ta_pi;
		//internal Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter Ta_pi
		//{
		//    get
		//    {
		//        if (ta_pi == null)
		//        {
		//            ta_pi = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.CZMST_PITableAdapter();
		//            ta_pi.Connection = this.Connection;
		//        }
		//        return ta_pi;
		//    }
		//    //set { ta_pi = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.ParametryTableAdapter ta_parametry;
		//internal Fask.SQLiteDBs.DataSets.PrijemTableAdapters.ParametryTableAdapter Ta_parametry
		//{
		//    get
		//    {
		//        if (ta_parametry == null)
		//        {
		//            ta_parametry = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.ParametryTableAdapter();
		//            ta_parametry.Connection = this.Connection;
		//        }
		//        return ta_parametry;
		//    }
		//    //set { ta_parametry = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.QueriesTableAdapter ta_queries;
		//internal Fask.SQLiteDBs.DataSets.PrijemTableAdapters.QueriesTableAdapter Ta_queries
		//{
		//    get
		//    {
		//        if (ta_queries == null)
		//        {
		//            ta_queries = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.QueriesTableAdapter();
		//            ta_queries.Connection = this.Connection;
		//        }
		//        return ta_queries;
		//    }
		//    //set { ta_queries = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.PrijemTableAdapters.SlouceneTableAdapter ta_sloucene;
		//internal Fask.SQLiteDBs.DataSets.PrijemTableAdapters.SlouceneTableAdapter Ta_sloucene
		//{
		//    get
		//    {
		//        if (ta_sloucene == null)
		//        {
		//            ta_sloucene = new Fask.SQLiteDBs.DataSets.PrijemTableAdapters.SlouceneTableAdapter();
		//            ta_sloucene.Connection = this.Connection;
		//        }
		//        return ta_sloucene;
		//    }
		//    //set { ta_sloucene = value; }
		//} 

		#endregion

		#region c'tor + dispose

		public SQLite_Controller_Prijem(string sqliteFileName)
			: base(sqliteFileName)
		{
		}

		public SQLite_Controller_Prijem(SQLiteConnection sqliteconnection)
			: base(sqliteconnection)
		{
		}

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();

		//    ta_parametry.Connection = this.Connection;
		//    ta_pe.Connection = this.Connection;
		//    ta_pe_sn.Connection = this.Connection;
		//    ta_peh.Connection = this.Connection;
		//    ta_pi.Connection = this.Connection;
		//    ta_pi_nasnimano.Connection = this.Connection;
		//    ta_pif.Connection = this.Connection;
		//    ta_pih.Connection = this.Connection;
		//    ta_queries.Connection = this.Connection;
		//    ta_sloucene.Connection = this.Connection;
		//}

		public override void Dispose()
		{
			//this.DisposeObject(ta_parametry);
			//this.DisposeObject(ta_pe);
			//this.DisposeObject(ta_pe_sn);
			//this.DisposeObject(ta_peh);
			//this.DisposeObject(ta_pi);
			//this.DisposeObject(ta_pi_nasnimano);
			//this.DisposeObject(ta_pif);
			//this.DisposeObject(ta_pih);
			//this.DisposeObject(ta_queries);
			//this.DisposeObject(ta_sloucene);

			//ta_parametry = null;
			//ta_pe = null;
			//ta_pe_sn = null;
			//ta_peh = null;
			//ta_pi = null;
			//ta_pi_nasnimano = null;
			//ta_pif = null;
			//ta_pih = null;
			//ta_queries = null;
			//ta_sloucene = null;

			base.Dispose();
		} 

		#endregion

		#region Puvodne metody, treba zrevidovat

		#region tableadaptermethods

		#region PE Tabulka

		public string CZMST_PE_Get_ItemDescription(string itemnmbr)
		{
			try
			{
				Connection_Open();

				object result = null;
				using (var command = Connection.CreateCommand())
				{
					command.CommandText = "Select ITEMDESC from czmst_pe where itemnmbr=@itemnmbr";
					command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

					result = command.ExecuteScalar();
				}
				if (result != null && result is string)
					return ((string)result).Trim();
				else
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

		#endregion

		#region PI tabulka

		/// <summary>
		/// Vrati pocet vsech zaznamu z tabulky PI
		/// </summary>
		/// <returns></returns>
		public int CZMST_PI_Count_All()
		{
			try
			{
				Connection_Open();

				using (var command = Connection.CreateCommand())
				{
					command.CommandText = "Select count(*) from CZMST_PI";
					object o = command.ExecuteScalar();
					return Convert.ToInt32(o);
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
		/// Vrati zaznam z PI tabulky na danem indexu
		/// </summary>
		/// <param name="index">index zaznamu z PI tabulky</param>
		/// <returns></returns>
		public DataSets.Prijem.CZMST_PIRow CZMST_PI_Get_One(int index)
		{
			try
			{
				Connection_Open();


				using (var command = Connection.CreateCommand())
				{
					command.CommandText = "Select * from CZMST_PI";

					using (var reader = command.ExecuteReader())
					{
						for (int i = 0; i <= index; i++)
						{
							reader.Read();
							//reader.
						}

						//var dtpi = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable();
						//var rwpi = dtpi.NewCZMST_PIRow();
						//this.MapValues2Rows(reader, rwpi);
						//dtpi.AddCZMST_PIRow(rwpi);
						//return rwpi;

						//object[] values = new object[reader.FieldCount];
						//reader.GetValues(values);
						//return dtpi.LoadDataRow(values, true);

						if (reader.HasRows)
							return _Routines.LoadRowFromReader(reader, new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable()) as Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIRow;
						else
							return null;
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

		/// <summary>
		/// Smazani polozky z vystupu
		/// </summary>
		/// <param name="guid">Guid zaznamu ke smazani</param>
		public bool CZMST_PI_DeleteByGuid(Guid guid)
		{
			try
			{
				Connection_Open();

				using (var command = Connection.CreateCommand())
				{
					command.CommandText = "Delete FROM CZMST_PI where Guid=@guid";
					command.Parameters.AddWithValue("@guid", guid);

					int recordsAffected = command.ExecuteNonQuery();
					return recordsAffected > 0;
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

		#endregion

		#endregion

		#region Special methods

		public void CZMST_PI_DeleteAndUpdate(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dtPI)
		{
			IDbTransaction transaction = null;
			try
			{
				this.Connection_Open();

				transaction = this.Connection.BeginTransaction();

				this.DeleteQuery_PI();
				this.Update_PI(dtPI);

				transaction.Commit();
			}
			catch (Exception ex)
			{
				try
				{
					if (transaction != null) transaction.Rollback();
				}
				catch (Exception exRollback)
				{
					Logging.ExceptionHandler2.Handle(exRollback);
				}

				throw ex; // vybublat puvodni vyjimku ...
			}
			finally
			{
				this.Connection_Close();
			}
		}

		public bool CZMST_PI_Duplicita_SN_ByKey(bool prim_key, Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow PERow, string sn)
		{
			try
			{
				this.Connection_Open();

				#region old code
				//string wherecond = string.Empty;
				//if (prim_key)
				//    wherecond = "ITEMNMBR='" + PERow.ITEMNMBR + "' AND SERLTNUM='" + sn + "'";
				//else
				//    wherecond = "PONUMBER='" + PERow.PONUMBER + "' AND ORD=" + PERow.ORD + " AND SERLTNUM='" + sn + "'";

				//System.Data.SQLite.SQLiteCommand scecommand = new System.Data.SQLite.SQLiteCommand(
				//    //"Select count(*) from czmst_pi where " + wherecond,
				//    "Select itemnmbr from czmst_pi where " + wherecond,
				//    _davkasqlceconnection //new System.Data.SqlServerCe.SqlCeConnection("Data source=" + sqlfilename)
				//);

				//bool prevCnStateOpened = false;
				//try
				//{
				//    prevCnStateOpened = scecommand.Connection.State == ConnectionState.Open;
				//    if (!prevCnStateOpened)
				//        scecommand.Connection.Open();
				//    exist = scecommand.ExecuteReader(CommandBehavior.SingleRow).Read();
				//    if (!prevCnStateOpened)
				//        scecommand.Connection.Close();
				//}
				//finally
				//{
				//    if (!prevCnStateOpened && scecommand.Connection.State == ConnectionState.Open)
				//        scecommand.Connection.Close();
				//}
				//return exist;
				#endregion

				string wherecond = string.Empty;
				if (prim_key)
					wherecond = "ITEMNMBR='" + PERow.ITEMNMBR + "' AND SERLTNUM='" + sn + "'";
				else
					wherecond = "PONUMBER='" + PERow.PONUMBER + "' AND ORD=" + PERow.ORD + " AND SERLTNUM='" + sn + "'";

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "Select itemnmbr from czmst_pi where " + wherecond + " LIMIT 1";
					using (var reader = command.ExecuteReader())
					{
						return reader.Read(); // pokud ho nactu, tak existuje
					}
				}

				//return false; // pokud dojdu az sem, tak nic neni ... 

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

		#endregion

		#region Metody misto TableAdapteru

		#region PE_SN

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PE_SNDataTable GetDataByDavkaPolozka_PE_SN(int CountEntries, string ITEMNMBR)
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PE_SNDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PE_SNDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_PE_SN where CountEntries=@CountEntries and Itemnmbr=@ITEMNMBR";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
						

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

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

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PE_SNDataTable GetData_PE_SN()
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PE_SNDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PE_SNDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_PE_SN";

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

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

		public int FillBySN_PE_SN(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PE_SNDataTable dataTable, string SERLNMBR)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_PE_SN WHERE serlnmbr=@SERLNMBR";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLNMBR", DbType = System.Data.DbType.String, Value = SERLNMBR == null ? (object)DBNull.Value : SERLNMBR });


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

		public int Update_PE_SN(object data)
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
					InitializeCommandInsert_PE_SN(commandInsert);
					//InitializeCommandUpdate_PE_SN(commandUpdate);
					//InitializeCommandDelete_PE_SN(commandDelete);
					InitializeCommandSelect_PE_SN(commandSelect);

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

		public void InitializeCommandInsert_PE_SN(SQLiteCommand command)
		{
			command.CommandText = 
				"INSERT INTO CZMST_PE_SN (CountEntries, ITEMNMBR, SERLNMBR, DEX_ROW_ID, Expirace) " + 
				" VALUES " +
				" (@CountEntries, @ITEMNMBR, @SERLNMBR, @DEX_ROW_ID, @Expirace)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, IsNullable = false, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, IsNullable = false, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLNMBR", DbType = System.Data.DbType.String, IsNullable = false, SourceColumn = "SERLNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, IsNullable = false, SourceColumn = "DEX_ROW_ID" });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, IsNullable = true , SourceColumn = "Expirace" });            
		}

		//public void InitializeCommandUpdate_SI(SQLiteCommand command)
		//{
		//    //command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODBER_ID", DbType = System.Data.DbType.String, SourceColumn = "ODBER_ID" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.Guid, SourceColumn = "guid" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Boolean, SourceColumn = "PRINTED" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });
		//    //command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
		//}

		//public void InitializeCommandDelete_SI(SQLiteCommand command)
		//{
		//    command.CommandText = "DELETE FROM CZMST_SI WHERE (guid = @guid)";

		//    command.Parameters.Add(new SQLiteParameter()
		//    {
		//        ParameterName = "@guid",
		//        DbType = System.Data.DbType.Guid,
		//        SourceColumn = "guid",
		//        SourceVersion = System.Data.DataRowVersion.Original
		//    });
		//}

		public void InitializeCommandSelect_PE_SN(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_PE_SN";
		}


		#endregion


		#endregion

		#region PE_H

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEHDataTable GetData_PEH()
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEHDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEHDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_PEH";

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

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

		private int Fill_PEH_Universal(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEHDataTable dataTable, SQLiteParameter[] Parameter, string Select)
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
						adapter.SelectCommand.CommandText = Select;

						if (Parameter != null)
							adapter.SelectCommand.Parameters.AddRange(Parameter);

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


		public int Fill_PEH(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEHDataTable dataTable)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PEH";
			return Fill_PEH_Universal(dataTable, null, where);
		}

		public int Insert_PEH(string CountEntries, System.Guid GUID)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.InsertCommand = command;
						adapter.InsertCommand.Connection = this.Connection;

						adapter.InsertCommand.Connection = this.Connection;
						adapter.InsertCommand.CommandText = @"INSERT INTO CZMST_PEH (CountEntries, GUID) VALUES (@CountEntries, @GUID)";
						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, Value = CountEntries == null ? (object)DBNull.Value : CountEntries });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID });

						#endregion

						int returnValue = adapter.InsertCommand.ExecuteNonQuery();
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

		#region PE

		#region Fill + Get metody

		private int Fill_PE_Universal(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dataTable, SQLiteParameter[] Parameter, string Select)
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
						adapter.SelectCommand.CommandText = Select;
						
						if(Parameter != null)
							adapter.SelectCommand.Parameters.AddRange(Parameter);

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

		public int FillByKey_PE(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dataTable, string PONUMBER, string ITEMNMBR, int ORD)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PE WHERE (PONUMBER = @PONUMBER) AND (ITEMNMBR = @ITEMNMBR) AND (ORD = @ord)";
			SQLiteParameter[] par = new SQLiteParameter[3];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };
			par[1] = new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, Value = PONUMBER == null ? (object)DBNull.Value : PONUMBER };
			par[2] = new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD };

			return Fill_PE_Universal(dataTable, par, where);
		}

		public virtual Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable GetDataByKey_PE(string PONUMBER, string ITEMNMBR, int ORD)
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable();
			FillByKey_PE(dt, PONUMBER, ITEMNMBR, ORD);
			return dt;
		}

		public int FillByItemdescpart_PE(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dataTable, string ItemDescPart)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PE WHERE (ITEMDESC LIKE @ItemDescPart)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@ItemDescPart", DbType = System.Data.DbType.String, Value = ItemDescPart == null ? (object)DBNull.Value : ItemDescPart };

			return Fill_PE_Universal(dataTable, par, where);
		}

		public int FillByItemdescpartNmbrpal_PE(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dataTable, string ItemDescPart, string NMBRPAL)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PE WHERE (ITEMDESC LIKE @ItemDescPart) AND (NMBRPAL = @NMBRPAL)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@ItemDescPart", DbType = System.Data.DbType.String, Value = ItemDescPart == null ? (object)DBNull.Value : ItemDescPart };
			par[1] = new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = NMBRPAL == null ? (object)DBNull.Value : NMBRPAL };

			return Fill_PE_Universal(dataTable, par, where);
		}

		public int FillByBarcode_PE(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dataTable, string carkod)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PE WHERE (VNDITNUM = @carkod)" + 
				" UNION " + 
				" SELECT * FROM CZMST_PE AS CZMST_PE_1 WHERE (CZ_CarKod = @carkod)" + 
				" ORDER BY ORD";

			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@carkod", DbType = System.Data.DbType.String, Value = carkod == null ? (object)DBNull.Value : carkod };

			return Fill_PE_Universal(dataTable, par, where);
		}

		public int FillByBarcodeNmbrpal_PE(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dataTable, string carkod, string NMBRPAL)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PE WHERE (VNDITNUM = @carkod) AND (NMBRPAL = @NMBRPAL) " + 
				" UNION " +
				" SELECT FROM CZMST_PE AS CZMST_PE_1 WHERE (CZ_CarKod = @carkod) AND (NMBRPAL = @NMBRPAL) " + 
				" ORDER BY ORD";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@carkod", DbType = System.Data.DbType.String, Value = carkod == null ? (object)DBNull.Value : carkod };
			par[1] = new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = NMBRPAL == null ? (object)DBNull.Value : NMBRPAL };

			return Fill_PE_Universal(dataTable, par, where);
		}

		public int FillByITEMNMBR_PE(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dataTable, string ITEMNMBR)
		{
			string where = @"SELECT * FROM CZMST_PE WHERE (ITEMNMBR = @ITEMNMBR)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };

			return Fill_PE_Universal(dataTable, par, where);
		}

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable GetData_PE()
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dataTable = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable();
			string where = @"SELECT * FROM CZMST_PE";
			Fill_PE_Universal(dataTable, null, where);
			return dataTable;
		}

		public int Fill_PE(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dataTable)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PE";
			return Fill_PE_Universal(dataTable, null, where);
		}

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable GetDataByCountEntries_PE(int CountEntries)
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dataTable = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable();
			string where = @"SELECT * FROM CZMST_PE WHERE (CountEntries = @CountEntries)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries };

			Fill_PE_Universal(dataTable, par, where);
			return dataTable;
		}

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable GetDataByKey2_PE(string PONUMBER, string ITEMNMBR, int ORD, decimal QTYPACK)
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PEDataTable();
			string where = @"SELECT * FROM CZMST_PE WHERE (PONUMBER = @PONUMBER) AND (ITEMNMBR = @ITEMNMBR) AND (ORD = @ORD) AND (QTYPACK = @QTYPACK)";
			SQLiteParameter[] par = new SQLiteParameter[4];
			par[0] = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };
			par[1] = new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, Value = PONUMBER == null ? (object)DBNull.Value : PONUMBER };
			par[2] = new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD };
			par[3] = new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = QTYPACK };

			Fill_PE_Universal(dt, par, where);
			return dt;
		}

		#endregion

		public int DeleteQuery_PE(int CountEntries)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_PE WHERE (CountEntries = @CountEntries)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries, SourceVersion = DataRowVersion.Original });

					int returnValue;
					returnValue = command.ExecuteNonQuery();

					return returnValue;
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

		public int? CountQueryPonumber_PE(string PONUMBER)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM CZMST_PE WHERE (PONUMBER = @PONUMBER)";
					command.CommandType = System.Data.CommandType.Text;

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, Value = PONUMBER == null ? (object)DBNull.Value : PONUMBER, SourceVersion = DataRowVersion.Original });
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

		public int? CountQuery_PE()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM CZMST_PE";
					command.CommandType = System.Data.CommandType.Text;

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

		public int UpdateNasnimanoAddValue_PE(decimal Nasnimano, string PONUMBER, string ITEMNMBR, int ORD)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.UpdateCommand = command;
						adapter.UpdateCommand.Connection = this.Connection;

						adapter.UpdateCommand.Connection = this.Connection;
						adapter.UpdateCommand.CommandText = "UPDATE CZMST_PE SET Nasnimano = Nasnimano + @Nasnimano WHERE (PONUMBER = @PONUMBER) AND (ITEMNMBR = @ITEMNMBR) AND (ORD = @ORD)";
						adapter.UpdateCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.UpdateCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@Nasnimano", DbType = System.Data.DbType.Decimal, Value = Nasnimano });
						adapter.UpdateCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
						adapter.UpdateCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, Value = PONUMBER == null ? (object)DBNull.Value : PONUMBER });
						adapter.UpdateCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD });
						#endregion

						int returnValue = adapter.UpdateCommand.ExecuteNonQuery();
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

		public int UpdateNasnimano_PE(decimal Nasnimano, string PONUMBER, string ITEMNMBR, int ORD)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.UpdateCommand = command;
						adapter.UpdateCommand.Connection = this.Connection;

						adapter.UpdateCommand.Connection = this.Connection;
						adapter.UpdateCommand.CommandText = "UPDATE CZMST_PE SET Nasnimano = @Nasnimano WHERE (PONUMBER = @PONUMBER) AND (ITEMNMBR = @ITEMNMBR) AND (ORD = @ORD)";
						adapter.UpdateCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.UpdateCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@Nasnimano", DbType = System.Data.DbType.Decimal, Value = Nasnimano });
						adapter.UpdateCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
						adapter.UpdateCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, Value = PONUMBER == null ? (object)DBNull.Value : PONUMBER });
						adapter.UpdateCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD });
						#endregion

						int returnValue = adapter.UpdateCommand.ExecuteNonQuery();
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

		public int Update_PE(object data)
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
					InitializeCommandInsert_PE(commandInsert);
					//InitializeCommandUpdate_PE(commandUpdate);
					//InitializeCommandDelete_PE(commandDelete);
					InitializeCommandSelect_PE(commandSelect);

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

		public void InitializeCommandInsert_PE(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO CZMST_PE (CountEntries, PONUMBER, ITEMNMBR, ITEMDESC, ORD, VNDDOCNM, VNDITNUM, " + 
				" CZ_CarKod, LOCNCODE, QTYSHPPD, QTYPACK, CZ_DatVyr_Track, CZ_DatVyr_Delka, CZ_SerNum_Track, CZ_SerNum_Delka, " + 
				" CZ_SW_Track, CZ_SW_Delka, CZ_Doslo, DEX_ROW_ID, Nasnimano, MJ, SKL_ID, WEIGHT, NMBRPAL, TYPEPAL, ITEMCODE, " +
                " SERLTNUM, CZ_REZ1_TRACK, CZ_REZ2_TRACK, CZ_Expirace_Track) " + 
				" VALUES " +
			    "(@CountEntries,@PONUMBER,@ITEMNMBR,@ITEMDESC,@ORD,@VNDDOCNM,@VNDITNUM,@CZ_CarKod,@LOCNCODE,@QTYSHPPD," + 
				" @QTYPACK,@CZ_DatVyr_Track,@CZ_DatVyr_Delka,@CZ_SerNum_Track,@CZ_SerNum_Delka,@CZ_SW_Track,@CZ_SW_Delka," + 
				" @CZ_Doslo,@DEX_ROW_ID,@Nasnimano,@MJ,@SKL_ID,@WEIGHT,@NMBRPAL,@TYPEPAL,@ITEMCODE,@SERLTNUM," +
                " @CZ_REZ1_TRACK,@CZ_REZ2_TRACK, @CZ_Expirace_Track)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, SourceColumn = "PONUMBER" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_DatVyr_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_DatVyr_Track" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_DatVyr_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_DatVyr_Delka" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Track" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SerNum_Delka" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SW_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SW_Track" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SW_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SW_Delka" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Doslo", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Doslo" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Nasnimano", DbType = System.Data.DbType.Decimal, SourceColumn = "Nasnimano" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ1_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_TRACK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ2_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_TRACK" });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Expirace_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Expirace_Track" });            
		}

		//public void InitializeCommandUpdate_PE(SQLiteCommand command)
		//{
		//    command.CommandText = "UPDATE CZMST_PIH SET DATUMDOKLADU = @DATUMDOKLADU WHERE (CountEntries = @CountEntries)";

		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, SourceColumn = "PONUMBER" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_DatVyr_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_DatVyr_Track" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_DatVyr_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_DatVyr_Delka" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Track" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SerNum_Delka" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SW_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SW_Track" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SW_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SW_Delka" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Doslo", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Doslo" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Nasnimano", DbType = System.Data.DbType.Decimal, SourceColumn = "Nasnimano" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ1_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_TRACK" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ2_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_TRACK" });
		//}

		//public void InitializeCommandDelete_PE(SQLiteCommand command)
		//{
		//    command.CommandText = "DELETE FROM CZMST_PIH WHERE (CountEntries = @CountEntries)";

		//    command.Parameters.Add(new SQLiteParameter()
		//    {
		//        ParameterName = "@CountEntries",
		//        DbType = System.Data.DbType.Int32,
		//        SourceColumn = "CountEntries",
		//        SourceVersion = System.Data.DataRowVersion.Original
		//    });
		//}

		public void InitializeCommandSelect_PE(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_PE";
		}

		#endregion


		#endregion

		#region PIF

		public int Insert_PIF(string IMG_NAME, System.Guid GUID)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.InsertCommand = command;
						adapter.InsertCommand.Connection = this.Connection;

						adapter.InsertCommand.Connection = this.Connection;
						adapter.InsertCommand.CommandText = "INSERT INTO CZMST_PI_F (IMG_NAME, GUID) VALUES (@IMG_NAME,@GUID)";
						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@IMG_NAME", DbType = System.Data.DbType.String, Value = IMG_NAME == null ? (object)DBNull.Value : IMG_NAME });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID });

						#endregion

						int returnValue = adapter.InsertCommand.ExecuteNonQuery();
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

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PI_FDataTable GetDataByGUID_PIF(System.Guid GUID)
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PI_FDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PI_FDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_PIF where GUID=@GUID";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID });

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

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

		public int Fill_PIF(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PI_FDataTable dt)
		{
			try
			{
				dt.Clear();

				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_PI_F";

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

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
		
		public int DeleteByGUID_PIF(Guid GUID)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_PIF WHERE (GUID = @GUID)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID, SourceVersion = DataRowVersion.Original });

					int returnValue;
					returnValue = command.ExecuteNonQuery();

					return returnValue;
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

		#region PI_Nasnimano

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PI_NasnimanoDataTable GetData_PI_Nasnimano()
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PI_NasnimanoDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PI_NasnimanoDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT PONUMBER, ORD, ITEMNMBR, SUM(QTYSHPPD) AS Nasnimano FROM CZMST_PI GROUP BY PONUMBER, ORD, ITEMNMBR";

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

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

		#region PIH


		private int Fill_PIH_Universal(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIHDataTable dataTable, SQLiteParameter[] Parameter, string Select)
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
						adapter.SelectCommand.CommandText = Select;

						if (Parameter != null)
							adapter.SelectCommand.Parameters.AddRange(Parameter);

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


		public int Fill_PIH(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIHDataTable dataTable)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PIH";
			return Fill_PIH_Universal(dataTable, null, where);
		}

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIHDataTable GetDataByCountentries_PIH(int CountEntries)
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIHDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIHDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_PIH where CountEntries=@CountEntries";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

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


		public int Update_PIH(object data)
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
					InitializeCommandInsert_PIH(commandInsert);
					InitializeCommandUpdate_PIH(commandUpdate);
					InitializeCommandDelete_PIH(commandDelete);
					InitializeCommandSelect_PIH(commandSelect);

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

		public void InitializeCommandInsert_PIH(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_PIH (CountEntries, DATUMDOKLADU) VALUES (@CountEntries,@DATUMDOKLADU)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATUMDOKLADU", DbType = System.Data.DbType.DateTime, SourceColumn = "DATUMDOKLADU" });
		}

		public void InitializeCommandUpdate_PIH(SQLiteCommand command)
		{
			command.CommandText = "UPDATE CZMST_PIH SET DATUMDOKLADU = @DATUMDOKLADU WHERE (CountEntries = @CountEntries)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATUMDOKLADU", DbType = System.Data.DbType.DateTime, SourceColumn = "DATUMDOKLADU" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
		}

		public void InitializeCommandDelete_PIH(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST_PIH WHERE (CountEntries = @CountEntries)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@CountEntries",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "CountEntries",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_PIH(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_PIH";
		}

		#endregion

		#endregion

		#region PI

		#region Fill + Get metody

		private int Fill_PI_Universal(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dataTable, SQLiteParameter[] Parameter, string Select)
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
						adapter.SelectCommand.CommandText = Select;

						if (Parameter != null)
							adapter.SelectCommand.Parameters.AddRange(Parameter);

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

		public int Fill_PI(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dataTable)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PI";
			return Fill_PI_Universal(dataTable, null, where);
		}

		public int FillBySerltnum_PI(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dataTable, string SERLTNUM)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PI WHERE (SERLTNUM = @SERLTNUM)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, Value = SERLTNUM == null ? (object)DBNull.Value : SERLTNUM };

			return Fill_PI_Universal(dataTable, null, where);
		}

		public int FillByBarcode_PI(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dataTable, string CZ_CarKod)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PI WHERE (CZ_CarKod = @CZ_CarKod) " +
				" UNION" +
				" SELECT *  FROM CZMST_PI AS CZMST_PI_1 WHERE (VNDITNUM = @CZ_CarKod)";

			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, Value = CZ_CarKod == null ? (object)DBNull.Value : CZ_CarKod };

			return Fill_PI_Universal(dataTable, null, where);
		}

		public int FillBySerltnumLocncode_PI(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dataTable, string SERLTNUM, string LOCNCODE)
		{
			dataTable.Clear();
			string where = @"SELECT * FROM CZMST_PI WHERE (SERLTNUM = @SERLTNUM) AND (LOCNCODE = '' OR LOCNCODE = @LOCNCODE)";
			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, Value = SERLTNUM == null ? (object)DBNull.Value : SERLTNUM };
			par[1] = new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = LOCNCODE == null ? (object)DBNull.Value : LOCNCODE };

			return Fill_PI_Universal(dataTable, null, where);
		}

		public int FillByBarcodeLocncode_PI(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dataTable, string CZ_CarKod,string LOCNCODE)
		{
			dataTable.Clear();
			string where = @"SELECT *  FROM CZMST_PI WHERE (CZ_CarKod = @CZ_CarKod) AND (LOCNCODE = '' OR LOCNCODE = @LOCNCODE) " + 
				" UNION " + 
				" SELECT *  FROM CZMST_PI AS CZMST_PI_1 WHERE (VNDITNUM = @CZ_CarKod) AND (LOCNCODE = '' OR LOCNCODE = @LOCNCODE)";

			SQLiteParameter[] par = new SQLiteParameter[2];
			par[0] = new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, Value = CZ_CarKod == null ? (object)DBNull.Value : CZ_CarKod };
			par[1] = new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = LOCNCODE == null ? (object)DBNull.Value : LOCNCODE };

			return Fill_PI_Universal(dataTable, null, where);
		}

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable GetDataByKey_PI(int CountEntries, string PONUMBER, int ORD, string ITEMNMBR)
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable();
			string where = @"SELECT * FROM CZMST_PI WHERE (CountEntries = @CountEntries) AND (PONUMBER = @PONUMBER) AND (ORD = @ORD) AND (ITEMNMBR = @ITEMNMBR)";
			SQLiteParameter[] par = new SQLiteParameter[4];
			par[0] = new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries };
			par[1] = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };
			par[2] = new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, Value = PONUMBER == null ? (object)DBNull.Value : PONUMBER };
			par[3] = new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD };

			Fill_PI_Universal(dt, par, where);
			return dt;
		}

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable GetDataByCntItemnmbrPonmbrOrd_PI(int CountEntries, string ITEMNMBR, string PONUMBER, int ORD)
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable();
			string where = @"SELECT * FROM CZMST_PI WHERE (CountEntries = @CountEntries) AND (ITEMNMBR = @ITEMNMBR) AND (PONUMBER = @PONUMBER) AND (ORD = @ORD)";
			SQLiteParameter[] par = new SQLiteParameter[4];
			par[0] = new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries };
			par[1] = new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR };
			par[2] = new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, Value = PONUMBER == null ? (object)DBNull.Value : PONUMBER };
			par[3] = new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD };

			Fill_PI_Universal(dt, par, where);
			return dt;
		}

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable GetDataByGuid_PI(System.Guid guid)
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable();
			string where = @"SELECT * FROM CZMST_PI WHERE (guid = @guid)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.Guid, Value = guid };

			Fill_PI_Universal(dt, par, where);
			return dt;
		}

		public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable GetDataByCountEntries_PI(int CountEntries)
		{
			Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable();
			string where = @"SELECT * FROM CZMST_PI WHERE (CountEntries = @CountEntries)";
			SQLiteParameter[] par = new SQLiteParameter[1];
			par[0] = new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries };

			Fill_PI_Universal(dt, par, where);
			return dt;
		}


		#endregion

		public int Delete_PI(Guid guid)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_PI WHERE (guid = @guid)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.Guid, Value = guid, SourceVersion = DataRowVersion.Original });

					int returnValue;
					returnValue = command.ExecuteNonQuery();

					return returnValue;
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

		public int DeleteQuery_PI()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_PI ";

					int returnValue;
					returnValue = command.ExecuteNonQuery();

					return returnValue;
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

		public virtual int DeleteQueryByCountEntries_PI(int CountEntries)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_PI WHERE (CountEntries = @CountEntries)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries, SourceVersion = DataRowVersion.Original });
					int returnValue;
					returnValue = command.ExecuteNonQuery();

					return returnValue;
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

		public int? CountLocncodeNotIn_PI(string locncode)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM CZMST_PI WHERE (LOCNCODE IS NULL) OR (LOCNCODE NOT IN (\'\', @locncode))";
					command.CommandType = System.Data.CommandType.Text;

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@locncode", DbType = System.Data.DbType.String, Value = locncode == null ? (object)DBNull.Value : locncode, SourceVersion = DataRowVersion.Original });

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

		public int? CountQuery_PI()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM CZMST_PI";
					command.CommandType = System.Data.CommandType.Text;

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

		public int? CountLocncodeEmpty_PI()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM CZMST_PI WHERE (LOCNCODE IS NULL) OR (LOCNCODE = \'\')";
					command.CommandType = System.Data.CommandType.Text;

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


		public int Update_PI(object data)
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
					InitializeCommandInsert_PI(commandInsert);
					//InitializeCommandUpdate_PI(commandUpdate);
					InitializeCommandDelete_PI(commandDelete);
					InitializeCommandSelect_PI(commandSelect);

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

		public void InitializeCommandInsert_PI(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO CZMST_PI " + 
				" (CountEntries, PONUMBER, ORD, ITEMNMBR, VNDDOCNM, VNDITNUM, LOCNCODE, QTYSHPPD, QTYPACK, SERLTNUM, KOD_SW, DAT_VYROBY, DATEDONE, " +
				" TIMEDONE, CZ_CarKod, REZ_1, REZ_2, USER_ID, guid, INPUT_MODE, ID_TERMINAL, MJ, QTYSHPPDMJ, SKL_ID, WEIGHT, NMBRPAL, TYPEPAL, ITEMCODE, DEX_ROW_ID, Expirace, AttributeToSN) " + 
				" VALUES (" + 
				" @CountEntries," + 
				" @PONUMBER," + 
				" @ORD," + 
				" @ITEMNMBR," + 
				" @VNDDOCNM," + 
				" @VNDITNUM," + 
				" @LOCNCODE," + 
				" @QTYSHPPD," + 
				" @QTYPACK," + 
				" @SERLTNUM," + 
				" @KOD_SW," + 
				" @DAT_VYROBY, " + 
				" @DATEDONE," + 
				" @TIMEDONE," + 
				" @CZ_CarKod," + 
				" @REZ_1," + 
				" @REZ_2," + 
				" @USER_ID," + 
				" @guid," + 
				" @INPUT_MODE," + 
				" @ID_TERMINAL," + 
				" @MJ," + 
				" @QTYSHPPDMJ," + 
				" @SKL_ID," + 
				" @WEIGHT," + 
				" @NMBRPAL," + 
				" @TYPEPAL," + 
				" @ITEMCODE," + 
				" @DEX_ROW_ID," + 
                " @Expirace,"+
				" @AttributeToSN" + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, SourceColumn = "PONUMBER" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.Guid, SourceColumn = "guid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int64, SourceColumn = "DEX_ROW_ID" });
            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@AttributeToSN", DbType = System.Data.DbType.String, SourceColumn = "AttributeToSN" });
			
		}

		//public void InitializeCommandUpdate_PE(SQLiteCommand command)
		//{
		//    command.CommandText = "UPDATE CZMST_PIH SET DATUMDOKLADU = @DATUMDOKLADU WHERE (CountEntries = @CountEntries)";

		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, SourceColumn = "PONUMBER" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_DatVyr_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_DatVyr_Track" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_DatVyr_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_DatVyr_Delka" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SerNum_Track" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SerNum_Delka" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SW_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_SW_Track" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_SW_Delka", DbType = System.Data.DbType.Int16, SourceColumn = "CZ_SW_Delka" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Doslo", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Doslo" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Nasnimano", DbType = System.Data.DbType.Decimal, SourceColumn = "Nasnimano" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ1_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_TRACK" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ2_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_TRACK" });
		//}

		public void InitializeCommandDelete_PI(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST_PI WHERE (guid = @guid)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@guid",
				DbType = System.Data.DbType.Guid,
				SourceColumn = "guid",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_PI(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_PI";
		}

		#endregion

		public int UpdateLocncodeByGuid_PI(string LOCNCODE, Guid GUID)
		{
			try
			{
				this.Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "UPDATE CZMST_PI set LOCNCODE=@LOCNCODE WHERE (GUID = @GUID)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.Int32, Value = LOCNCODE == null ? (object)DBNull.Value : LOCNCODE });
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
				this.Connection_Close();
			}
		}

		public int Insert_PI(
			int CountEntries,
			string PONUMBER,
			int ORD,
			string ITEMNMBR,
			string VNDDOCNM,
			string VNDITNUM,
			string LOCNCODE,
			decimal QTYSHPPD,
			decimal QTYPACK,
			string SERLTNUM,
			string KOD_SW,
			string DAT_VYROBY,
			string DATEDONE,
			string TIMEDONE,
			string CZ_CarKod,
			string REZ_1,
			string REZ_2,
			int USER_ID,
			Guid guid,
			byte INPUT_MODE,
			int ID_TERMINAL,
			string MJ,
			decimal QTYSHPPDMJ,
			string SKL_ID,
			decimal? WEIGHT,
			string NMBRPAL,
			string TYPEPAL,
			string ITEMCODE,
			long DEX_ROW_ID,
            DateTime? Expirace,
			string AttributeToSN)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.InsertCommand = command;
						adapter.InsertCommand.Connection = this.Connection;

						adapter.InsertCommand.Connection = this.Connection;
						adapter.InsertCommand.CommandText =  @"INSERT INTO CZMST_PI " + 
							" (CountEntries, PONUMBER, ORD, ITEMNMBR, VNDDOCNM, VNDITNUM, LOCNCODE, QTYSHPPD, QTYPACK, SERLTNUM, KOD_SW, DAT_VYROBY, DATEDONE, " +
							" TIMEDONE, CZ_CarKod, REZ_1, REZ_2, USER_ID, guid, INPUT_MODE, ID_TERMINAL, MJ, QTYSHPPDMJ, SKL_ID, WEIGHT, NMBRPAL, TYPEPAL, ITEMCODE, DEX_ROW_ID, Expirace, AttributeToSN) " + 
							" VALUES (@CountEntries, @PONUMBER, @ORD, @ITEMNMBR, @VNDDOCNM, @VNDITNUM, @LOCNCODE, @QTYSHPPD, @QTYPACK, @SERLTNUM, @KOD_SW, @DAT_VYROBY, " + 
							" @DATEDONE, @TIMEDONE, @CZ_CarKod, @REZ_1, @REZ_2, @USER_ID, @guid, @INPUT_MODE, @ID_TERMINAL, @MJ, @QTYSHPPDMJ, @SKL_ID, @WEIGHT, @NMBRPAL, @TYPEPAL," +
							" @ITEMCODE, @DEX_ROW_ID, @Expirace, @AttributeToSN)";
						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region Parametry

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries", Value = CountEntries });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@PONUMBER", DbType = System.Data.DbType.String, SourceColumn = "PONUMBER", Value = PONUMBER == null ? (object)DBNull.Value : PONUMBER });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD", Value = ORD });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR", Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM", Value = VNDDOCNM == null ? (object)DBNull.Value : VNDDOCNM });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM", Value = VNDITNUM == null ? (object)DBNull.Value : VNDITNUM });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE", Value = LOCNCODE == null ? (object)DBNull.Value : LOCNCODE });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD", Value = QTYSHPPD });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK", Value = QTYPACK });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM", Value = SERLTNUM == null ? (object)DBNull.Value : SERLTNUM });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW", Value = KOD_SW == null ? (object)DBNull.Value : KOD_SW });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY", Value = DAT_VYROBY == null ? (object)DBNull.Value : DAT_VYROBY });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE", Value = DATEDONE == null ? (object)DBNull.Value : DATEDONE });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE", Value = TIMEDONE == null ? (object)DBNull.Value : TIMEDONE });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod", Value = CZ_CarKod == null ? (object)DBNull.Value : CZ_CarKod });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1", Value = REZ_1 == null ? (object)DBNull.Value : REZ_1 });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2", Value = REZ_2 == null ? (object)DBNull.Value : REZ_2 });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID", Value = USER_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.Guid, SourceColumn = "guid", Value = guid });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE", Value = INPUT_MODE });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL", Value = ID_TERMINAL });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID", Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT", Value = WEIGHT.HasValue ? WEIGHT.Value : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL", Value = NMBRPAL == null ? (object)DBNull.Value : NMBRPAL });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL", Value = TYPEPAL == null ? (object)DBNull.Value : TYPEPAL });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE", Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int64, SourceColumn = "DEX_ROW_ID", Value = DEX_ROW_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace", Value = Expirace == null ? (object)DBNull.Value : Expirace });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@AttributeToSN", DbType = System.Data.DbType.String, SourceColumn = "AttributeToSN", Value = AttributeToSN == null ? (object)DBNull.Value : AttributeToSN });
						

						#endregion

						int returnValue = adapter.InsertCommand.ExecuteNonQuery();
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

		#region Parametry

		public Fask.SQLiteDBs.DataSets.Prijem.ParametryDataTable GetData_Param()
		{
			Fask.SQLiteDBs.DataSets.Prijem.ParametryDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.ParametryDataTable();
			Fill_Param(dt);
			return dt;
		}

		public int Fill_Param(Fask.SQLiteDBs.DataSets.Prijem.ParametryDataTable dt)
		{
			try
			{
				dt.Clear();

				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT *  FROM Parametry";

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

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

		public int Update_Param(object data)
		{
			SQLiteTransaction transaction = null;
			try
			{
				int result = 0;
				Connection_Open();

				transaction = this.Connection.BeginTransaction();

				using (var commandInsert = this.Connection.CreateCommand())
				using (var commandUpdate = this.Connection.CreateCommand())
				//using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_Param(commandInsert);
					InitializeCommandUpdate_Param(commandUpdate);
					//InitializeCommandDelete_Param(commandDelete);
					InitializeCommandSelect_Param(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						//adapter.DeleteCommand = commandDelete;
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

		public void InitializeCommandInsert_Param(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO Parametry (" + 
				" CONFIG_DUPLIC_SN," + 
				" CONFIG_KONT_DELKA," + 
				" CONFIG_KONT_DOKONCENOSTI, " + 
				" CONFIG_KONT_NUL_DELKA," + 
				" CONFIG_KONT_UPL_POL," + 
				" CONFIG_LISTSNIM," + 
				" CONFIG_NOVA_KARTA," + 
				" CONFIG_POKRDOHLED, " + 
				" CONFIG_PRIM_KEY1," + 
				" CONFIG_PTATSE_NEANO," + 
				" CONFIG_SNIM_LOCNCODE," + 
				" CONFIG_SNIM_PONUMBER," + 
				" CONFIG_SNIM_REZ2, " + 
				" CONFIG_SNIMAT_POL1," + 
				" CONFIG_SNIMAT_POL2," + 
				" CONFIG_SNIMAT_POL3," + 
				" CONFIG_SNIMATZADAT_SN," + 
				" CONFIG_ZADAT_MN_POKAZDE, " + 
				" ENABLE_LISTSNIM," + 
				" ENABLE_SNIMATZADAT_SN," + 
				" CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA," + 
				" CONFIG_MNOZSTVI_PREDVYPLNIT, " + 
				" CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI," + 
				" CONFIG_MNOZSTVI_ZADAVAT," + 
				" CONFIG_MNOZSTVI_SCANNEREM," + 
				" CONFIG_LOKACE_POVOLIT," +
				" CONFIG_LOKACE_TIMEOUT," + 
				" CONFIG_LOKACE_PRIJMOVA) " + 
				" VALUES " + 
				" (@CONFIG_DUPLIC_SN," + 
				" @CONFIG_KONT_DELKA," + 
				" @CONFIG_KONT_DOKONCENOSTI," + 
				" @CONFIG_KONT_NUL_DELKA," + 
				" @CONFIG_KONT_UPL_POL," + 
				" @CONFIG_LISTSNIM," + 
				" @CONFIG_NOVA_KARTA," + 
				" @CONFIG_POKRDOHLED," + 
				" @CONFIG_PRIM_KEY1," + 
				" @CONFIG_PTATSE_NEANO," + 
				" @CONFIG_SNIM_LOCNCODE," + 
				" @CONFIG_SNIM_PONUMBER," + 
				" @CONFIG_SNIM_REZ2," + 
				" @CONFIG_SNIMAT_POL1," + 
				" @CONFIG_SNIMAT_POL2," + 
				" @CONFIG_SNIMAT_POL3," + 
				" @CONFIG_SNIMATZADAT_SN," + 
				" @CONFIG_ZADAT_MN_POKAZDE," + 
				" @ENABLE_LISTSNIM," + 
				" @ENABLE_SNIMATZADAT_SN," + 
				" @CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA," + 
				" @CONFIG_MNOZSTVI_PREDVYPLNIT," + 
				" @CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI," + 
				" @CONFIG_MNOZSTVI_ZADAVAT," + 
				" @CONFIG_MNOZSTVI_SCANNEREM," + 
				" @CONFIG_LOKACE_POVOLIT," + 
				" @CONFIG_LOKACE_TIMEOUT," + 
				" @CONFIG_LOKACE_PRIJMOVA)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_DUPLIC_SN", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_DUPLIC_SN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_DELKA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_DELKA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_DOKONCENOSTI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_DOKONCENOSTI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_NUL_DELKA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_NUL_DELKA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_UPL_POL", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_UPL_POL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LISTSNIM", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LISTSNIM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_NOVA_KARTA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_NOVA_KARTA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_POKRDOHLED", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_POKRDOHLED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_PRIM_KEY1", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_PRIM_KEY1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_PTATSE_NEANO", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_PTATSE_NEANO" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIM_LOCNCODE", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIM_LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIM_PONUMBER", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIM_PONUMBER" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIM_REZ2", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIM_REZ2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIMAT_POL1", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIMAT_POL1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIMAT_POL2", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIMAT_POL2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIMAT_POL3", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIMAT_POL3" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIMATZADAT_SN", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIMATZADAT_SN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_ZADAT_MN_POKAZDE", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_ZADAT_MN_POKAZDE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ENABLE_LISTSNIM", DbType = System.Data.DbType.Boolean, SourceColumn = "ENABLE_LISTSNIM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ENABLE_SNIMATZADAT_SN", DbType = System.Data.DbType.Boolean, SourceColumn = "ENABLE_SNIMATZADAT_SN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_ZADAVAT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_ZADAVAT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_SCANNEREM", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_SCANNEREM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_POVOLIT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LOKACE_POVOLIT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_TIMEOUT", DbType = System.Data.DbType.Int32, SourceColumn = "CONFIG_LOKACE_TIMEOUT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_PRIJMOVA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LOKACE_PRIJMOVA" });

		}

		public void InitializeCommandUpdate_Param(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE Parametry SET " +
				" CONFIG_DUPLIC_SN = @CONFIG_DUPLIC_SN," +
				" CONFIG_KONT_DELKA = @CONFIG_KONT_DELKA," +
				" CONFIG_KONT_DOKONCENOSTI = @CONFIG_KONT_DOKONCENOSTI," +
				" CONFIG_KONT_NUL_DELKA = @CONFIG_KONT_NUL_DELKA," +
				" CONFIG_KONT_UPL_POL = @CONFIG_KONT_UPL_POL," +
				" CONFIG_LISTSNIM = @CONFIG_LISTSNIM," +
				" CONFIG_NOVA_KARTA = @CONFIG_NOVA_KARTA," +
				" CONFIG_POKRDOHLED = @CONFIG_POKRDOHLED," +
				" CONFIG_PRIM_KEY1 = @CONFIG_PRIM_KEY1," +
				" CONFIG_PTATSE_NEANO = @CONFIG_PTATSE_NEANO," +
				" CONFIG_SNIM_LOCNCODE = @CONFIG_SNIM_LOCNCODE," +
				" CONFIG_SNIM_PONUMBER = @CONFIG_SNIM_PONUMBER," +
				" CONFIG_SNIM_REZ2 = @CONFIG_SNIM_REZ2," +
				" CONFIG_SNIMAT_POL1 = @CONFIG_SNIMAT_POL1," +
				" CONFIG_SNIMAT_POL2 = @CONFIG_SNIMAT_POL2," +
				" CONFIG_SNIMAT_POL3 = @CONFIG_SNIMAT_POL3," +
				" CONFIG_SNIMATZADAT_SN = @CONFIG_SNIMATZADAT_SN," +
				" CONFIG_ZADAT_MN_POKAZDE = @CONFIG_ZADAT_MN_POKAZDE," +
				" ENABLE_LISTSNIM = @ENABLE_LISTSNIM," +
				" ENABLE_SNIMATZADAT_SN = @ENABLE_SNIMATZADAT_SN," +
				" CONFIG_MNOZSTVI_PREDVYPLNIT = @CONFIG_MNOZSTVI_PREDVYPLNIT," +
				" CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI = @CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI," +
				" CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA = @CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA," +
				" CONFIG_MNOZSTVI_ZADAVAT = @CONFIG_MNOZSTVI_ZADAVAT," +
				" CONFIG_MNOZSTVI_SCANNEREM = @CONFIG_MNOZSTVI_SCANNEREM," +
				" CONFIG_LOKACE_POVOLIT = @CONFIG_LOKACE_POVOLIT," +
				" CONFIG_LOKACE_TIMEOUT = @CONFIG_LOKACE_TIMEOUT," +
				" CONFIG_LOKACE_PRIJMOVA = @CONFIG_LOKACE_PRIJMOVA";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_DUPLIC_SN", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_DUPLIC_SN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_DELKA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_DELKA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_DOKONCENOSTI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_DOKONCENOSTI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_NUL_DELKA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_NUL_DELKA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_UPL_POL", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_UPL_POL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LISTSNIM", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LISTSNIM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_NOVA_KARTA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_NOVA_KARTA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_POKRDOHLED", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_POKRDOHLED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_PRIM_KEY1", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_PRIM_KEY1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_PTATSE_NEANO", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_PTATSE_NEANO" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIM_LOCNCODE", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIM_LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIM_PONUMBER", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIM_PONUMBER" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIM_REZ2", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIM_REZ2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIMAT_POL1", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIMAT_POL1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIMAT_POL2", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIMAT_POL2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIMAT_POL3", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIMAT_POL3" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIMATZADAT_SN", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIMATZADAT_SN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_ZADAT_MN_POKAZDE", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_ZADAT_MN_POKAZDE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ENABLE_LISTSNIM", DbType = System.Data.DbType.Boolean, SourceColumn = "ENABLE_LISTSNIM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ENABLE_SNIMATZADAT_SN", DbType = System.Data.DbType.Boolean, SourceColumn = "ENABLE_SNIMATZADAT_SN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_ZADAVAT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_ZADAVAT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_SCANNEREM", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_SCANNEREM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_POVOLIT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LOKACE_POVOLIT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_TIMEOUT", DbType = System.Data.DbType.Int32, SourceColumn = "CONFIG_LOKACE_TIMEOUT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_PRIJMOVA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LOKACE_PRIJMOVA" });
		}

		//public void InitializeCommandDelete_Param(SQLiteCommand command)
		//{
		//    command.CommandText = "DELETE FROM CZMST_PIH WHERE (CountEntries = @CountEntries)";

		//    command.Parameters.Add(new SQLiteParameter()
		//    {
		//        ParameterName = "@CountEntries",
		//        DbType = System.Data.DbType.Int32,
		//        SourceColumn = "CountEntries",
		//        SourceVersion = System.Data.DataRowVersion.Original
		//    });
		//}

		public void InitializeCommandSelect_Param(SQLiteCommand command)
		{
			command.CommandText = "Select * from Parametry";
		}

		#endregion


		#endregion

		#region Queries

		public int DeletePI_Queries(int countentries, string itemnmbr, string ponumber, int ord)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_PI WHERE countentries=@countentries and itemnmbr=@itemnmbr and ponumber=@ponumber and ord=@ord";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@countentries", DbType = System.Data.DbType.Int32, Value = countentries, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@itemnmbr", DbType = System.Data.DbType.String, Value = itemnmbr == null ? (object)DBNull.Value : itemnmbr, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ponumber", DbType = System.Data.DbType.String, Value = ponumber == null ? (object)DBNull.Value : ponumber, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ord", DbType = System.Data.DbType.Int32, Value = ord, SourceVersion = DataRowVersion.Original });

					int returnValue;
					returnValue = command.ExecuteNonQuery();

					return returnValue;
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

		#region Sloucene

		public Fask.SQLiteDBs.DataSets.Prijem.SlouceneDataTable GetData_Sloucene()
		{

			Fask.SQLiteDBs.DataSets.Prijem.SlouceneDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.SlouceneDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT DISTINCT CountEntries FROM CZMST_PE";

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

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


		#region Palety

		public int FillBy_TOPjeden_NMBRPAL_PI(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dataTable, string nmbrpal)
		{
			try
			{
				dataTable.Clear();

				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST_PI WHERE (NMBRPAL = @nmbrpal) LIMIT 1";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@nmbrpal", DbType = System.Data.DbType.String, Value = nmbrpal == null ? (object)DBNull.Value : nmbrpal });

						int ReturnValue = 0;
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

		
		#endregion

		#endregion

    }
}
