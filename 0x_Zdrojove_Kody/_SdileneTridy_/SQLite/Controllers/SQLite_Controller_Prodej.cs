using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Fask.SQLiteDBs.Controllers
{
	/// <summary>
	/// Controler pro praci s daty davky volneho pohybu : DEH, DI_RFID, DIH, DI, SOUHRN
	/// </summary>
	public class SQLite_Controller_Prodej : SQLite_Controller
	{

		//private Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DEHTableAdapter ta_deh = null;
		//internal Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DEHTableAdapter Ta_deh
		//{
		//    get
		//    {
		//        if (ta_deh == null)
		//        {
		//            ta_deh = new Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DEHTableAdapter();
		//            ta_deh.Connection = this.Connection;
		//        }
		//        return ta_deh;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DI_RFIDTableAdapter ta_di_rfid = null;
		//internal Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DI_RFIDTableAdapter Ta_di_rfid
		//{
		//    get
		//    {
		//        if (ta_di_rfid == null)
		//        {
		//            ta_di_rfid = new Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DI_RFIDTableAdapter();
		//            ta_di_rfid.Connection = this.Connection;
		//        }
		//        return ta_di_rfid;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter ta_dih = null;
		//internal Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter Ta_dih
		//{
		//    get
		//    {
		//        if (ta_dih == null)
		//        {
		//            ta_dih = new Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
		//            ta_dih.Connection = this.Connection;
		//        }
		//        return ta_dih;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter ta_di = null;
		//internal Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter Ta_di
		//{
		//    get
		//    {
		//        if (ta_di == null)
		//        {
		//            ta_di = new Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter();
		//            ta_di.Connection = this.Connection;
		//        }
		//        return ta_di;
		//    }
		//}

		//private Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_SOUHRNTableAdapter ta_souhrn = null;
		//internal Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_SOUHRNTableAdapter Ta_souhrn
		//{
		//    get
		//    {
		//        if (ta_souhrn == null)
		//        {
		//            ta_souhrn = new Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_SOUHRNTableAdapter();
		//            ta_souhrn.Connection = this.Connection;
		//        }
		//        return ta_souhrn;
		//    }
		//}

		#region c'tors

		public SQLite_Controller_Prodej(string sqliteFileName)
			: base(sqliteFileName)
		{
			//AdaptersInitialize();
		}

		public SQLite_Controller_Prodej(SQLiteConnection sqliteconnection)
			: base(sqliteconnection)
		{
		}

		//protected override void AdaptersInitialize()
		//{
		//    base.AdaptersInitialize();

		//    ta_deh = new Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DEHTableAdapter();
		//    ta_di = new Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DITableAdapter();
		//    ta_di_rfid = new Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DI_RFIDTableAdapter();
		//    ta_dih = new Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_DIHTableAdapter();
		//    ta_souhrn = new Fask.SQLiteDBs.DataSets.ProdejTableAdapters.CZMST_SOUHRNTableAdapter();

		//    ta_deh.Connection = this.Connection;
		//    ta_di.Connection = this.Connection;
		//    ta_di_rfid.Connection = this.Connection;
		//    ta_dih.Connection = this.Connection;
		//    ta_souhrn.Connection = this.Connection;
		//}


		#endregion

		public override void Dispose()
		{

			//this.DisposeObject(ta_deh);
			//this.DisposeObject(ta_di);
			//this.DisposeObject(ta_di_rfid);
			//this.DisposeObject(ta_dih);
			//this.DisposeObject(ta_souhrn);

			//ta_deh = null;
			//ta_di = null;
			//ta_di_rfid = null;
			//ta_dih = null;
			//ta_souhrn = null;


			base.Dispose();
		}


		//public string CZMST_PE_Get_ItemDescription(string itemnmbr)
		//{
		//    try
		//    {
		//        Connection_Open();

		//        object result = null;
		//        using (var command = Connection.CreateCommand())
		//        {
		//            command.CommandText = "Select ITEMDESC from czmst_pe where itemnmbr=@itemnmbr";
		//            command.Parameters.AddWithValue("@itemnmbr", itemnmbr);

		//            result = command.ExecuteScalar();
		//        }
		//        if (result != null && result is string)
		//            return ((string)result).Trim();
		//        else
		//            return null;
		//    }
		//    catch (Exception ex)
		//    {
		//        Logging.Log.Write(ex);
		//        return null;
		//    }
		//    finally
		//    {
		//        Connection_Close();
		//    }
		//}


		#region ručně psana SQL komunikace, pro zbaveni se TableAdapteru

		#region DI

		/// <summary>
		/// Vraci prvni nalezeny zaznam z DI
		/// </summary>
		/// <returns>1. DIRow z DI tabulky, pokud neni zaznam, tak null</returns>
		public Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow CZMST_DI_GetFirstRecord()
		{
			Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dt_di = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable();
			try
			{
				Connection_Open();

				using (var command = Connection.CreateCommand())
				{
					command.CommandText = "Select * from czmst_di limit 1";
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
							_Routines.LoadRowFromReader(reader, dt_di);
					}
				}

				if (dt_di.Count > 0)
					return dt_di[0];
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

		/// <summary>
		/// Pocet zaznamu na vystupu DI
		/// </summary>
		/// <param name="itemnmbr"></param>
		/// <returns>Celkový počet záznamů</returns>
		public int CZMST_DI_Count()
		{
			try
			{
				Connection_Open();

				object result = null;
				using (var command = Connection.CreateCommand())
				{
					command.CommandText = "Select count(*) from czmst_di";
					result = command.ExecuteScalar();
				}

				return Convert.ToInt32(result);
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


		public int FillBy_TOPjeden_NMBRPAL_DI(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dataTable, string nmbrpal)
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
						adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST_DI WHERE (NMBRPAL = @nmbrpal) LIMIT 1";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@nmbrpal", DbType = System.Data.DbType.String, Value = nmbrpal == null ? (object)DBNull.Value : nmbrpal });

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


		#endregion

		#region Stav Prodeje

		/// <summary>
		/// spocita celkovou cenu davky
		/// u nasnimaneho baleni se do QTYSHPPD uklada celkove mnozstvi (quant*QTYPACK) !!!
		/// </summary>
		/// <param name="cena_s_dph"></param>
		/// <param name="cena_bez_dph"></param>
		public void stav_prodeje(out decimal cena_s_dph, out decimal cena_bez_dph)
		{

			// TODO : poradne otestovat
			// converze pomoci ConvertTo....

			cena_s_dph = 0;
			cena_bez_dph = 0;

			try
			{
				this.Connection_Open();

				using (var scecommand = this.Connection.CreateCommand())
				{
					scecommand.CommandText = "select * from czmst_di";

					using (var reader = scecommand.ExecuteReader())
					{
						while (reader.Read())
						{
							decimal qty = 0;
							qty = (decimal)reader["QTYSHPPD"];

							if ((Int64)reader["WITHTAX"] > 0)
							{
								cena_bez_dph += ((decimal)reader["AMOUNPIE"] - (decimal)reader["TAXAMPIE"]) * qty;
								cena_s_dph += (decimal)reader["AMOUNPIE"] * qty;
							}
							else
							{
								cena_bez_dph += (decimal)reader["AMOUNPIE"] * qty;
								cena_s_dph += ((decimal)reader["AMOUNPIE"] + (decimal)reader["TAXAMPIE"]) * qty;
							}
						}
						//di.Close();
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
				this.Connection_Close();
			}
		}

		#endregion

		#region CZMST_DEH

		/// <summary>
		/// Metoda pro dotaženi všech hlaviček z CZMST_DIH
		/// </summary>
		/// <returns></returns>
		public Fask.SQLiteDBs.DataSets.Prodej.CZMST_DEHDataTable GetData_DEH()
		{
			Fask.SQLiteDBs.DataSets.Prodej.CZMST_DEHDataTable dt = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DEHDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_DEH";

						adapter.Fill(dt);

						return dt;
					}
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				//throw ex;
				return new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DEHDataTable();
			}
			finally
			{
				Connection_Close();
			}
		}

		/// <summary>
		/// Metoda pro vložení hlavičky do CZMST_DIH
		/// </summary>
		/// <param name="CountEntries">číslo davky</param>
		/// <param name="GUID">GUID jedinečny identifikator</param>
		/// <returns></returns>
		public int Insert_DEH(int CountEntries, Guid? GUID)
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
						adapter.InsertCommand.CommandText = "INSERT INTO CZMST_DEH (CountEntries, GUID) VALUES (@CountEntries,@GUID)";
						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@GUID", DbType = System.Data.DbType.Guid, Value = GUID });

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

		public int Fill_DEH(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DEHDataTable dt)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_DEH";

						int returnValue;
						returnValue = adapter.Fill(dt);

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

		#region CZMST_DI

		#region Insert to DI

		/// <summary>
		/// Metoda pro vloženi řadku do CZMST_DI
		/// </summary>
		/// <param name="CountEntries"></param>
		/// <param name="ODB_ID"></param>
		/// <param name="STR_ID"></param>
		/// <param name="DOC_ID"></param>
		/// <param name="DOC_ID2"></param>
		/// <param name="ITEMNMBR"></param>
		/// <param name="LOCNCODE"></param>
		/// <param name="QTYSHPPD"></param>
		/// <param name="QTYPACK"></param>
		/// <param name="SERLTNUM"></param>
		/// <param name="TAXAMPIE"></param>
		/// <param name="AMOUNPIE"></param>
		/// <param name="WITHTAX"></param>
		/// <param name="PRICEX"></param>
		/// <param name="REZ_1"></param>
		/// <param name="REZ_2"></param>
		/// <param name="REZ_3"></param>
		/// <param name="REZ_4"></param>
		/// <param name="USER_ID"></param>
		/// <param name="DATEDONE"></param>
		/// <param name="TIMEDONE"></param>
		/// <param name="guid"></param>
		/// <param name="VNDITNUM"></param>
		/// <param name="CZ_CarKod"></param>
		/// <param name="SKL_ID"></param>
		/// <param name="MJ"></param>
		/// <param name="QTYSHPPDMJ"></param>
		/// <param name="PRAC_ID"></param>
		/// <param name="INPUT_MODE"></param>
		/// <param name="ID_TERMINAL"></param>
		/// <param name="TYPEPAL"></param>
		/// <param name="NMBRPAL"></param>
		/// <param name="ITEMCODE"></param>
		/// <param name="mena_ID"></param>
		/// <param name="TAXAMPIEM"></param>
		/// <param name="AMOUNPIEM"></param>
		/// <param name="mena_IDM"></param>
		/// <param name="ITEMDESC"></param>
		/// <param name="LOCNCODEDEST"></param>
		/// <param name="SKL_ID_DEST"></param>
		/// <param name="WEIGHT"></param>
		/// <param name="PRINTED"></param>
		/// <param name="DEX_ROW_ID"></param>
		/// /// <param name="EXPIRACE"></param>
		/// <returns></returns>
		public int Insert_DI(
					int CountEntries,
					string ODB_ID,
					string STR_ID,
					string DOC_ID,
					string DOC_ID2,
					string ITEMNMBR,
					string LOCNCODE,
					decimal QTYSHPPD,
					decimal? QTYPACK,
					string SERLTNUM,
					decimal? TAXAMPIE,
					decimal? AMOUNPIE,
					byte? WITHTAX,
					byte? PRICEX,
					string REZ_1,
					string REZ_2,
					string REZ_3,
					string REZ_4,
					int? USER_ID,
					string DATEDONE,
					string TIMEDONE,
					System.Guid guid,
					string VNDITNUM,
					string CZ_CarKod,
					string SKL_ID,
					string MJ,
					decimal QTYSHPPDMJ,
					string PRAC_ID,
					byte INPUT_MODE,
					int ID_TERMINAL,
					string TYPEPAL,
					string NMBRPAL,
					string ITEMCODE,
					string mena_ID,
					decimal? TAXAMPIEM,
					decimal? AMOUNPIEM,
					string mena_IDM,
					string ITEMDESC,
					string LOCNCODEDEST,
					string SKL_ID_DEST,
					decimal? WEIGHT,
					bool PRINTED,
					int DEX_ROW_ID,
					DateTime? EXPIRACE,
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
						adapter.InsertCommand.CommandText = @"INSERT INTO CZMST_DI " +
							" (CountEntries, ODB_ID, STR_ID, DOC_ID, DOC_ID2, ITEMNMBR, LOCNCODE, QTYSHPPD, QTYPACK, SERLTNUM, TAXAMPIE, AMOUNPIE, WITHTAX, PRICEX, REZ_1, " +
							" REZ_2, REZ_3, REZ_4, USER_ID, DATEDONE, TIMEDONE, guid, VNDITNUM, CZ_CarKod, SKL_ID, MJ, QTYSHPPDMJ, PRAC_ID, INPUT_MODE, ID_TERMINAL, " +
							"TYPEPAL, NMBRPAL, ITEMCODE, mena_ID, TAXAMPIEM, AMOUNPIEM, mena_IDM, ITEMDESC, LOCNCODEDEST, SKL_ID_DEST, WEIGHT, PRINTED, DEX_ROW_ID, EXPIRACE, AttributeToSN) " +
							"VALUES (@CountEntries,@ODB_ID,@STR_ID,@DOC_ID,@DOC_ID2,@ITEMNMBR,@LOCNCODE,@QTYSHPPD,@QTYPACK,@SERLTNUM,@TAXAMPIE,@AMOUNPIE,@WITHTAX," +
							"@PRICEX,@REZ_1,@REZ_2,@REZ_3,@REZ_4,@USER_ID,@DATEDONE,@TIMEDONE,@guid,@VNDITNUM,@CZ_CarKod,@SKL_ID,@MJ,@QTYSHPPDMJ,@PRAC_ID,@INPUT_MODE," +
							"@ID_TERMINAL,@TYPEPAL,@NMBRPAL,@ITEMCODE,@mena_ID,@TAXAMPIEM,@AMOUNPIEM,@mena_IDM,@ITEMDESC,@LOCNCODEDEST,@SKL_ID_DEST,@WEIGHT,@PRINTED,@DEX_ROW_ID, @EXPIRACE, @AttributeToSN)";

						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = ODB_ID == null ? (object)DBNull.Value : ODB_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@STR_ID", DbType = System.Data.DbType.String, Value = STR_ID == null ? (object)DBNull.Value : STR_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@DOC_ID", DbType = System.Data.DbType.String, Value = DOC_ID == null ? (object)DBNull.Value : DOC_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@DOC_ID2", DbType = System.Data.DbType.String, Value = DOC_ID2 == null ? (object)DBNull.Value : DOC_ID2 });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = LOCNCODE == null ? (object)DBNull.Value : LOCNCODE });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, Value = QTYSHPPD });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = QTYPACK.HasValue? QTYPACK.Value : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, Value = SERLTNUM == null ? (object)DBNull.Value : SERLTNUM });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@TAXAMPIE", DbType = System.Data.DbType.Decimal, Value = TAXAMPIE.HasValue ? TAXAMPIE.Value : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@AMOUNPIE", DbType = System.Data.DbType.Decimal, Value = AMOUNPIE.HasValue ? AMOUNPIE.Value : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@WITHTAX", DbType = System.Data.DbType.Byte, Value = WITHTAX.HasValue ? WITHTAX.Value : (object)DBNull.Value  });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRICEX", DbType = System.Data.DbType.Byte, Value = PRICEX.HasValue  ? PRICEX.Value : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, Value = REZ_1 == null ? (object)DBNull.Value : REZ_1 });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, Value = REZ_2 == null ? (object)DBNull.Value : REZ_2 });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_3", DbType = System.Data.DbType.String, Value = REZ_3 == null ? (object)DBNull.Value : REZ_3 });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_4", DbType = System.Data.DbType.String, Value = REZ_4 == null ? (object)DBNull.Value : REZ_4 });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, Value = USER_ID.HasValue ? USER_ID.Value : (object)DBNull.Value});
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, Value = DATEDONE == null ? (object)DBNull.Value : DATEDONE });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, Value = TIMEDONE == null ? (object)DBNull.Value : TIMEDONE });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.Guid, Value = guid });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, Value = VNDITNUM == null ? (object)DBNull.Value : VNDITNUM });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, Value = CZ_CarKod == null ? (object)DBNull.Value : CZ_CarKod });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, Value = MJ == null ? (object)DBNull.Value : MJ });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, Value = QTYSHPPDMJ });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRAC_ID", DbType = System.Data.DbType.String, Value = PRAC_ID == null ? (object)DBNull.Value : PRAC_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, Value = INPUT_MODE });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, Value = ID_TERMINAL });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, Value = TYPEPAL == null ? (object)DBNull.Value : TYPEPAL });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = NMBRPAL == null ? (object)DBNull.Value : NMBRPAL });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, Value = mena_ID == null ? (object)DBNull.Value : mena_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@TAXAMPIEM", DbType = System.Data.DbType.Decimal, Value = TAXAMPIEM.HasValue ? TAXAMPIEM.Value : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@AMOUNPIEM", DbType = System.Data.DbType.Decimal, Value = AMOUNPIEM.HasValue ? AMOUNPIEM.Value : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_IDM", DbType = System.Data.DbType.String, Value = mena_IDM == null ? (object)DBNull.Value : mena_IDM });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = ITEMDESC == null ? (object)DBNull.Value : ITEMDESC });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODEDEST", DbType = System.Data.DbType.String, Value = LOCNCODEDEST == null ? (object)DBNull.Value : LOCNCODEDEST });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID_DEST", DbType = System.Data.DbType.String, Value = SKL_ID_DEST == null ? (object)DBNull.Value : SKL_ID_DEST });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, Value = WEIGHT.HasValue ? WEIGHT.Value : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Boolean, Value = PRINTED });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, Value = DEX_ROW_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@EXPIRACE", DbType = System.Data.DbType.DateTime, Value = EXPIRACE.HasValue ? EXPIRACE.Value : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@AttributeToSN", DbType = System.Data.DbType.String, Value = AttributeToSN == null ? (object)DBNull.Value : AttributeToSN });
						

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

		/// <summary>
		/// Metoda pro primi zapis do lokalne databaze
		/// </summary>
		/// <param name="dita">TableAdapter</param>
		/// <param name="di">Radek co se vlozi do databaze</param>
		/// <returns>Number of inserted rows (expected is 1). 0 or -1 is error</returns>
		public int DI_DirectInsert(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow di)
		{
			try
			{
				int result = this.Insert_DI(
					di.CountEntries,
					di.IsODB_IDNull() ? string.Empty : di.ODB_ID.Trim(),
					di.IsSTR_IDNull() ? string.Empty : di.STR_ID,
					di.IsDOC_IDNull() ? string.Empty : di.DOC_ID,
					di.IsDOC_ID2Null() ? string.Empty : di.DOC_ID2,
					di.ITEMNMBR.Trim(),
					di.IsLOCNCODENull() ? string.Empty : di.LOCNCODE,
					di.QTYSHPPD,
					di.IsQTYPACKNull() ? 0 : di.QTYPACK,
					di.SERLTNUM.Trim(),
					di.IsTAXAMPIENull() ? 0 : di.TAXAMPIE,
					di.IsAMOUNPIENull() ? 0 : di.AMOUNPIE,
					di.IsWITHTAXNull() ? (byte)0 : di.WITHTAX,
					di.IsPRICEXNull() ? (byte)0 : di.PRICEX,
					di.IsREZ_1Null() ? string.Empty : di.REZ_1,
					di.IsREZ_2Null() ? string.Empty : di.REZ_2,
					di.IsREZ_3Null() ? string.Empty : di.REZ_3,
					di.IsREZ_4Null() ? string.Empty : di.REZ_4,
					di.IsUSER_IDNull() ? 0 : di.USER_ID,
					di.IsDATEDONENull() ? string.Empty : di.DATEDONE,
					di.IsTIMEDONENull() ? string.Empty : di.TIMEDONE,
					di.guid,
					di.IsVNDITNUMNull() ? string.Empty : di.VNDITNUM,
					di.IsCZ_CarKodNull() ? string.Empty : di.CZ_CarKod,
					di.IsSKL_IDNull() ? string.Empty : di.SKL_ID,
					di.MJ.Trim(),
					di.QTYSHPPDMJ,
					di.IsPRAC_IDNull() ? string.Empty : di.PRAC_ID,
					di.INPUT_MODE,
					di.ID_TERMINAL,
					di.IsTYPEPALNull() ? string.Empty : di.TYPEPAL, //_paleta == null ? null : _paleta.Typ,
					di.IsNMBRPALNull() ? string.Empty : di.NMBRPAL,//_paleta == null ? null : _paleta.Cislo,
					di.IsITEMCODENull() ? string.Empty : di.ITEMCODE,
					di.Ismena_IDNull() ? string.Empty : di.mena_ID,
					di.IsTAXAMPIEMNull() ? (decimal?)0 : di.TAXAMPIEM,
					di.IsAMOUNPIEMNull() ? (decimal?)0 : di.AMOUNPIEM,
					di.Ismena_IDMNull() ? string.Empty : di.mena_IDM,
					di.IsITEMDESCNull() ? string.Empty : di.ITEMDESC,
					di.IsLOCNCODEDESTNull() ? string.Empty : di.LOCNCODEDEST,
					di.IsSKL_ID_DESTNull() ? string.Empty : di.SKL_ID_DEST,
					di.IsWEIGHTNull() ? (decimal?)0 : di.WEIGHT,
					di.PRINTED,
					di.IsDEX_ROW_IDNull() ? 0 : di.DEX_ROW_ID,
					di.IsEXPIRACENull() ? (DateTime?)null : di.EXPIRACE,
					di.IsAttributeToSNNull() ? string.Empty : di.AttributeToSN
				);
				return result;
			}
			catch (Exception exDIInsert)
			{
				Logging.ExceptionHandler2.Handle(exDIInsert);
				return -1;
			}
		}

		public Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable GetData_DI()
		{
			Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dataTable = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable();
			Fill_DI(dataTable);
			return dataTable;
		}

		public int Fill_DI(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dt)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_DI";

						int returnValue;
						returnValue = adapter.Fill(dt);

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

		public int Fill_DI_ByNmbrpal(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dt, string NMBRPAL)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_DI WHERE (NMBRPAL=@NMBRPAL)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = NMBRPAL == null ? (object)DBNull.Value : NMBRPAL });
						
						int returnValue;
						returnValue = adapter.Fill(dt);

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

		public object SUM_QTYSHPPD_By_ITEMNMBR_from_DI(string ITEMNMBR)
		{

			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText  = "SELECT SUM(QTYSHPPD) AS QTYSHPPD FROM CZMST_DI WHERE (ITEMNMBR = @ITEMNMBR)";
					command.CommandType = System.Data.CommandType.Text;
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
					object returnValue  = command.ExecuteScalar();

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

		public int? PocetPolozek_DI()
		{

			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM CZMST_DI";
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

		public virtual int Delete_DI_ByGUID(Guid guid)
		{
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.DeleteCommand = command;
						adapter.DeleteCommand.CommandText = "DELETE FROM CZMST_DI WHERE (guid = @GUID)";
						adapter.DeleteCommand.CommandType = global::System.Data.CommandType.Text;

						adapter.DeleteCommand.Parameters.Add(new SQLiteParameter()
						{
							ParameterName = "@GUID",
							DbType = System.Data.DbType.Guid,
							SourceVersion = System.Data.DataRowVersion.Original,
							Value = guid
						});

						int returnValue;
						returnValue = adapter.DeleteCommand.ExecuteNonQuery();

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
				using (var commandDelete = this.Connection.CreateCommand())
				using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_CZMST095(commandInsert);
					//InitializeCommandUpdate_CZMST095(commandUpdate);
					InitializeCommandDelete_CZMST095(commandDelete);
					InitializeCommandSelect_CZMST095(commandSelect);

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

        public void InitializeCommandUpdate_CZMST095(SQLiteCommand command)
        {
            command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

            command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
        }

        public void InitializeCommandDelete_CZMST095(SQLiteCommand command)
        {
            command.CommandText = "DELETE FROM CZMST_SE WHERE (guid = @guid)";

            command.Parameters.Add(new SQLiteParameter()
            {
                ParameterName = "@guid",
                DbType = System.Data.DbType.Guid,
                SourceColumn = "guid",
                SourceVersion = System.Data.DataRowVersion.Original
            });
        }

        public void InitializeCommandSelect_CZMST095(SQLiteCommand command)
		{
			command.CommandText = "SELECT * FROM CZMST095";
		}


		#endregion

		#endregion



		#endregion

		#region CZMST_DIH

		public int DeleteQuery_DIH()
		{
			try
			{
				Connection_Open();

					using (var command = this.Connection.CreateCommand())
					{
						command.CommandText = "DELETE FROM CZMST_DIH";

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

		public Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable GetData_DIH()
		{
			Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dt = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_DIH";

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

		public int Insert_DIH(string Zakazka_ID, string Paleta_ID, string mena_ID, string SKL_ID, int CountEntries)
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
						adapter.InsertCommand.CommandText = "INSERT INTO CZMST_DIH (Zakazka_ID, Paleta_ID, mena_ID, SKL_ID, CountEntries) VALUES (@Zakazka_ID,@Paleta_ID,@mena_ID,@SKL_ID,@CountEntries)";
						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@Zakazka_ID", DbType = System.Data.DbType.String, Value = Zakazka_ID == null ? (object)DBNull.Value : Zakazka_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@Paleta_ID", DbType = System.Data.DbType.String, Value = Paleta_ID == null ? (object)DBNull.Value : Paleta_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, Value = mena_ID == null ? (object)DBNull.Value : mena_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, Value = CountEntries  });

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

		public int Fill_DIH(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dt)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_DIH";

						int returnValue;
						returnValue = adapter.Fill(dt);

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

		#region CZMST_DI_RFID

		public int Insert_DI_RFID(
			string ITEMNMBR,
			int SEQUENCENMBR,
			string SKL_ID,
			int? CountEntries,
			string DOCUMENTNMBR,
			int? ORD,
			string SERLNMBR,
			System.Guid guid,
			string M_ID,
			string M_TID,
			string M_EPC,
			string M_USER,
			string M_RESERVED,
			string O_M_ID,
			string O_M_TID,
			string O_M_EPC,
			string O_M_USER,
			string O_M_RESERVED,
			int TerminalID,
			int UserID,
			System.DateTime Created_T)
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

						adapter.InsertCommand.CommandText = @"INSERT INTO CZMST_DI_RFID (ITEMNMBR, SEQUENCENMBR, SKL_ID, CountEntries, DOCUMENTNMBR, " +
							" ORD, SERLNMBR, guid, M_ID, M_TID, M_EPC, M_USER, M_RESERVED, O_M_ID, O_M_TID, O_M_EPC, O_M_USER, O_M_RESERVED, TerminalID, " +
							" UserID, Created_T) VALUES (@ITEMNMBR,@SEQUENCENMBR,@SKL_ID,@CountEntries,@DOCUMENTNMBR,@ORD,@SERLNMBR,@guid,@M_ID,@M_TID," +
							"@M_EPC,@M_USER,@M_RESERVED,@O_M_ID,@O_M_TID,@O_M_EPC,@O_M_USER,@O_M_RESERVED,@TerminalID,@UserID,@Created_T)";

						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SEQUENCENMBR", DbType = System.Data.DbType.Int32, Value = SEQUENCENMBR });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, Value = CountEntries.HasValue ? CountEntries : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@DOCUMENTNMBR", DbType = System.Data.DbType.String, Value = DOCUMENTNMBR == null ? (object)DBNull.Value : DOCUMENTNMBR });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD.HasValue ? ORD.Value : (object)DBNull.Value });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLNMBR", DbType = System.Data.DbType.String, Value = SERLNMBR == null ? (object)DBNull.Value : SERLNMBR });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.Guid, Value = guid });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@M_ID", DbType = System.Data.DbType.String, Value = M_ID == null ? (object)DBNull.Value : M_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@M_TID", DbType = System.Data.DbType.String, Value = M_TID == null ? (object)DBNull.Value : M_TID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@M_EPC", DbType = System.Data.DbType.String, Value = M_EPC == null ? (object)DBNull.Value : M_EPC });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@M_USER", DbType = System.Data.DbType.String, Value = M_USER == null ? (object)DBNull.Value : M_USER });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@M_RESERVED", DbType = System.Data.DbType.String, Value = M_RESERVED == null ? (object)DBNull.Value : M_RESERVED });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_M_ID", DbType = System.Data.DbType.String, Value = O_M_ID == null ? (object)DBNull.Value : O_M_ID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_M_TID", DbType = System.Data.DbType.String, Value = O_M_TID == null ? (object)DBNull.Value : O_M_TID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_M_EPC", DbType = System.Data.DbType.String, Value = O_M_EPC == null ? (object)DBNull.Value : O_M_EPC });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_M_USER", DbType = System.Data.DbType.String, Value = O_M_USER == null ? (object)DBNull.Value : O_M_USER });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_M_RESERVED", DbType = System.Data.DbType.String, Value = O_M_RESERVED == null ? (object)DBNull.Value : O_M_RESERVED });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@TerminalID", DbType = System.Data.DbType.String, Value = TerminalID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, Value = UserID });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@Created_T", DbType = System.Data.DbType.DateTime, Value = Created_T });

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


		public int Fill_DI_RFID(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DI_RFIDDataTable dt)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_DI_RFID";

						int returnValue;
						returnValue = adapter.Fill(dt);

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

		#region CZMST_SOUHRN

		public int Fill_SOUHRNByNMBRPAL(Fask.SQLiteDBs.DataSets.Prodej.CZMST_SOUHRNDataTable dataTable, string nmbrpal)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ITEMNMBR, ITEMDESC, SUM(QTYSHPPD) AS Sum, COUNT(*) AS CNT, SUM(QTYSHPPD * WEIGHT) AS Vaha FROM CZMST_DI WHERE (NMBRPAL = @nmbrpal) GROUP BY ITEMNMBR, ITEMDESC";
					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@nmbrpal", DbType = System.Data.DbType.String, Value = nmbrpal == null ? (object)DBNull.Value : nmbrpal });
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

		public virtual int Fill_SOUHRN(Fask.SQLiteDBs.DataSets.Prodej.CZMST_SOUHRNDataTable dataTable)
		{
			try
			{
				Connection_Open();
				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT ITEMNMBR, ITEMDESC, SUM(QTYSHPPD) AS Sum, COUNT(*) AS CNT, SUM(QTYSHPPD * WEIGHT) AS Vaha FROM CZMST_DI GROUP BY ITEMNMBR, ITEMDESC";
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




		#endregion

	}
}
