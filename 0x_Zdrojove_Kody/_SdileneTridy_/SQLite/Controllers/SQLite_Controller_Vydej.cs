using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;


namespace Fask.SQLiteDBs.Controllers
{
    /// <summary>
    /// Controler pro praci s daty davky vydejky. SEH, SE, SE_SN, SI, SI_RFID, Parametry, HromadnyVydej, Sloucene
    /// </summary>
    public class SQLite_Controller_Vydej : SQLite_Controller
    {
        #region Table adapters
        // vydejka
		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SEHTableAdapter ta_seh = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SEHTableAdapter Ta_seh
		//{
		//    get
		//    {
		//        if (ta_seh == null)
		//        {
		//            ta_seh = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SEHTableAdapter();
		//            ta_seh.Connection = this.Connection;
		//        }
		//        return ta_seh;
		//    }
		//    //set { ta_seh = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter ta_se = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter Ta_se
		//{
		//    get
		//    {
		//        if (ta_se == null)
		//        {
		//            ta_se = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter();
		//            ta_se.Connection = this.Connection;
		//        }
		//        return ta_se;
		//    }
		//    //set { ta_se = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter ta_se_sn = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter Ta_se_sn
		//{
		//    get
		//    {
		//        if (ta_se_sn == null)
		//        {
		//            ta_se_sn = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter();
		//            ta_se_sn.Connection = this.Connection;
		//        }
		//        return ta_se_sn;
		//    }
		//    //set { ta_sesn = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter ta_si = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter Ta_si
		//{
		//    get
		//    {
		//        if (ta_si == null)
		//        {
		//            ta_si = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter();
		//            ta_si.Connection = this.Connection;
		//        }
		//        return ta_si;
		//    }
		//    //set { ta_si = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_RFIDTableAdapter ta_si_rfid = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_RFIDTableAdapter Ta_si_rfid
		//{
		//    get
		//    {
		//        if (ta_si_rfid == null)
		//        {
		//            ta_si_rfid = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_RFIDTableAdapter();
		//            ta_si_rfid.Connection = this.Connection;
		//        }
		//        return ta_si_rfid;
		//    }
		//    //set { ta_si_rfid = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.ParametryTableAdapter ta_parametry = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.ParametryTableAdapter Ta_parametry
		//{
		//    get
		//    {
		//        if (ta_parametry == null)
		//        {
		//            ta_parametry = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.ParametryTableAdapter();
		//            ta_parametry.Connection = this.Connection;
		//        }
		//        return ta_parametry;
		//    }
		//    //set { ta_parametry = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.HromadnyVydejTableAdapter ta_hromadnyvydej = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.HromadnyVydejTableAdapter Ta_hromadnyvydej
		//{
		//    get
		//    {
		//        if (ta_hromadnyvydej == null)
		//        {
		//            ta_hromadnyvydej = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.HromadnyVydejTableAdapter();
		//            ta_hromadnyvydej.Connection = this.Connection;
		//        }
		//        return ta_hromadnyvydej;
		//    }
		//    //set { ta_hromadnyvydej = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.SlouceneTableAdapter ta_sloucene = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.SlouceneTableAdapter Ta_sloucene
		//{
		//    get
		//    {
		//        if (ta_sloucene == null)
		//        {
		//            ta_sloucene = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.SlouceneTableAdapter();
		//            ta_sloucene.Connection = this.Connection;
		//        }
		//        return ta_sloucene;
		//    }
		//    //set { ta_sloucene = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_SN_TableAdapter ta_si_sn = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_SN_TableAdapter Ta_si_sn
		//{
		//    get
		//    {
		//        if (ta_si_sn == null)
		//        {
		//            ta_si_sn = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_SN_TableAdapter();
		//            ta_si_sn.Connection = this.Connection;
		//        }
		//        return ta_si_sn;
		//    }
		//    //set { ta_si_sn = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_TiskTableAdapter ta_si_tisk = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_TiskTableAdapter Ta_si_tisk
		//{
		//    get
		//    {
		//        if (ta_si_tisk == null)
		//        {
		//            ta_si_tisk = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_TiskTableAdapter();
		//            ta_si_tisk.Connection = this.Connection;
		//        }
		//        return ta_si_tisk;
		//    }
		//    //set { ta_si_tisk = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SIHTableAdapter ta_sih = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SIHTableAdapter Ta_sih
		//{
		//    get
		//    {
		//        if (ta_sih == null)
		//        {
		//            ta_sih = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SIHTableAdapter();
		//            ta_sih.Connection = this.Connection;
		//        }
		//        return ta_sih;
		//    }
		//    //set { ta_sih = value; }
		//}

		//private Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_SE_SoupisTableAdapter ta_sisesoupis = null;
		//internal Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_SE_SoupisTableAdapter Ta_sisesoupis
		//{
		//    get
		//    {
		//        if (ta_sisesoupis == null)
		//        {
		//            ta_sisesoupis = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_SE_SoupisTableAdapter();
		//            ta_sisesoupis.Connection = this.Connection;
		//        }
		//        return ta_sisesoupis;
		//    }
		//    //set { ta_sih = value; }
		//}



        #endregion

        #region c'tors
        public SQLite_Controller_Vydej(string sqliteFileName)
            : base(sqliteFileName)
        {
            //AdaptersInitialize();
        }

        public SQLite_Controller_Vydej(SQLiteConnection sqliteconnection)
            : base(sqliteconnection)
        {
        }

        //protected override void AdaptersInitialize()
        //{
        //    ta_seh = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SEHTableAdapter();
        //    ta_se = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter();
        //    ta_sesn = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SE_SNTableAdapter();
        //    ta_si = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter();
        //    ta_si_rfid = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SI_RFIDTableAdapter();
        //    ta_parametry = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.ParametryTableAdapter();
        //    ta_hromadnyvydej = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.HromadnyVydejTableAdapter();
        //    ta_sloucene = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.SlouceneTableAdapter();

        //    ta_seh.Connection = this.Connection;
        //    ta_se.Connection = this.Connection;
        //    ta_sesn.Connection = this.Connection;
        //    ta_si.Connection = this.Connection;
        //    ta_si_rfid.Connection = this.Connection;
        //    ta_parametry.Connection = this.Connection;
        //    ta_hromadnyvydej.Connection = this.Connection;
        //    ta_sloucene.Connection = this.Connection;
        //}
        #endregion

        public override void Dispose()
        {
            // disposing adapters ...
			//this.DisposeObject(ta_seh);
			//this.DisposeObject(ta_se);
			//this.DisposeObject(ta_se_sn);
			//this.DisposeObject(ta_si);
			//this.DisposeObject(ta_si_rfid);
			//this.DisposeObject(ta_parametry);
			//this.DisposeObject(ta_hromadnyvydej);
			//this.DisposeObject(ta_sloucene);
			//this.DisposeObject(ta_si_sn);
			//this.DisposeObject(ta_si_tisk);
			//this.DisposeObject(ta_sih);
			//this.DisposeObject(ta_sisesoupis);

            //ta_seh = null;
            //ta_se = null;
            //ta_se_sn = null;
            //ta_si = null;
            //ta_si_rfid = null;
            //ta_parametry = null;
            //ta_hromadnyvydej = null;
            //ta_sloucene = null;
            //ta_si_sn = null;
            //ta_si_tisk = null;
            //ta_sih = null;
			//ta_sisesoupis = null;

            this.NactenoFinalize();

            base.Dispose();
        }

		#region Metody misto TableAdapteru

		#region SEH

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEHDataTable GetData_SEH()
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEHDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEHDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_SEH";
						//adapter.SelectCommand.Parameters.AddRange(Parameter);

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

		public int Insert_SEH(int CountEntries, Guid GUID)
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
						adapter.InsertCommand.CommandText = @"INSERT INTO CZMST_SEH (CountEntries, GUID) VALUES (@CountEntries, @GUID)";
						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });
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

		public int Fill_SEH(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEHDataTable dt)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_SEH";
						//adapter.SelectCommand.Parameters.AddRange(Parameter);

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


		#endregion

		#region SE

        /// <summary>
        /// Polozky z se dle itemnmbr
        /// </summary>
        /// <param name="vydejDataTable">cilova tabulka</param>
        /// <param name="itemdesc_part">internal item number</param>
        public void SE_FillByItemnmbr(DataTable vydejDataTable, string itemnmbr)
        {
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
            {
                using (var command = this.Connection.CreateCommand())
                {
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.Connection = this.Connection;
                    adapter.SelectCommand.CommandText = "Select * from czmst_se where ITEMNMBR=@itemnmbr";
                    adapter.SelectCommand.Parameters.AddWithValue("@itemnmbr", itemnmbr);

                    adapter.Fill(vydejDataTable);
                }
            }
        }

		/// <summary>
		/// Polozky z se dle contextu casti itemdesc
		/// </summary>
		/// <param name="vydejDataTable">cilova tabulka</param>
		/// <param name="itemdesc_part">cast nazvu polozky</param>
		public void SE_FillByItemdescLike(DataTable vydejDataTable, string itemdesc_part)
		{
			//using (System.Data.SQLite.SQLiteDataAdapter sda = new System.Data.SQLite.SQLiteDataAdapter(
			//    "Select * from czmst_se where ITEMDESC like '%" + nazev + "%'",
			//    "Data source=" + filename
			//    ))
			//{
			//    sda.Fill(vydejData, vydejData.CZMST_SE.TableName);
			//}

			using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
			{
				using (var command = this.Connection.CreateCommand())
				{
					adapter.SelectCommand = command;
					adapter.SelectCommand.Connection = this.Connection;
					adapter.SelectCommand.CommandText = "Select * from czmst_se where ITEMDESC like '%" + itemdesc_part + "%'";
					adapter.SelectCommand.Parameters.AddWithValue("@kod", itemdesc_part);

					adapter.Fill(vydejDataTable);
				}
			}
		}

		/// <summary>
		/// Nacte data z CZMST_SE
		/// </summary>
		/// <param name="vydejDataTable">tabulka k naplneni</param>
		/// <param name="sopnumbe">cislo dokladu</param>
		/// <param name="itemnmbr">cislo polozky</param>
		/// <param name="ord">poradi polozky</param>
		public void SE_FillByI_SopnumbeItemnmbrOrd(DataTable vydejDataTable, string sopnumbe, string itemnmbr, int ord)
		{
			//using (System.Data.SQLite.SQLiteDataAdapter sda = new System.Data.SQLite.SQLiteDataAdapter(
			//    "Select * from czmst_se where Itemnmbr='" + lprow.Itemnmbr +
			//    "' and sopnumbe='" + lprow.SOPNUMBE +
			//    "' and ord=" + lprow.ORD,
			//    "Data source=" + filename
			//    ))
			//{
			//    sda.Fill(vydejData, vydejData.CZMST_SE.TableName);
			//}

			using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
			{
				using (var command = this.Connection.CreateCommand())
				{
					adapter.SelectCommand = command;
					adapter.SelectCommand.Connection = this.Connection;
					adapter.SelectCommand.CommandText = @"SELECT * FROM czmst_se WHERE sopnumbe=@sopnumbe AND itemnmbr=@itemnmbr AND ord=@ord";
					adapter.SelectCommand.Parameters.AddWithValue("@sopnumbe", sopnumbe);
					adapter.SelectCommand.Parameters.AddWithValue("@itemnmbr", itemnmbr);
					adapter.SelectCommand.Parameters.AddWithValue("@ord", ord);

					adapter.Fill(vydejDataTable);
				}
			}
		}

		public void SE_FillByEAN(DataTable vydejDataTable, string kod)
		{
			using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
			{
				using (var command = this.Connection.CreateCommand())
				{
					adapter.SelectCommand = command;
					adapter.SelectCommand.Connection = this.Connection;
					adapter.SelectCommand.CommandText =
						" Select * from czmst_se where CZ_CarKod=@kod" +
						" UNION " +
						" Select * from czmst_se where vnditnum=@kod" +
						" order by ord";
					adapter.SelectCommand.Parameters.AddWithValue("@kod", kod);

					adapter.Fill(vydejDataTable);
				}
			}
		}

		public string Get_ITEMDESC_SE(string SOPNUMBE, string ITEMNMBR) 
		{
			try
			{
				Connection_Open();

				object itemdescObject = null;
				using (var command = Connection.CreateCommand())
				{
					command.CommandText = "SELECT ITEMDESC FROM CZMST_SE WHERE (SOPNUMBE = @SOPNUMBE) AND (ITEMNMBR = @ITEMNMBR)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });

					itemdescObject = command.ExecuteScalar();
				}

				string itemdesc = "-";
				if (itemdescObject is string)
					itemdesc = (itemdescObject as string).Trim();

				if (string.IsNullOrEmpty(itemdesc))
					itemdesc = "-";


				return itemdesc;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return "Err";
			}
			finally
			{
				Connection_Close();
			}
		}

		public byte Get_CZSerNumTrack_SE(string SOPNUMBE, string ITEMNMBR)
		{
			try
			{
				Connection_Open();

				object itemdescObject = null;
				using (var command = Connection.CreateCommand())
				{
					command.CommandText = "SELECT CZ_SerNum_Track FROM CZMST_SE WHERE (SOPNUMBE = @SOPNUMBE) AND (ITEMNMBR = @ITEMNMBR)";

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });

					itemdescObject = command.ExecuteScalar();
				}

				byte trac = 0;
				if (itemdescObject is long)
					trac = Convert.ToByte(itemdescObject);

				return trac;
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


		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable GetDataByEan_SE(string EAN)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST_SE WHERE (VNDITNUM = @EAN) " + 
							" UNION " + 
							"SELECT * FROM CZMST_SE CZMST_SE_1 WHERE (CZ_CarKod = @EAN)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@EAN", DbType = System.Data.DbType.String, Value = EAN == null ? (object)DBNull.Value : EAN });

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

		public int Fill_SE(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable dt)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_SE";
						//adapter.SelectCommand.Parameters.AddRange(Parameter);

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

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable GetDataBySOPNUMBEITEMNMBRORD_SE( string SOPNUMBE, string ITEMNMBR, int ORD)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable();
			FillBySOPNUMBEITEMNMBRORD_SE(dt, SOPNUMBE,ITEMNMBR, ORD);
			return dt;
		}

		public int FillBySOPNUMBEITEMNMBRORD_SE(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable dt, string SOPNUMBE, string ITEMNMBR, int ORD)
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
						adapter.SelectCommand.CommandText= @"SELECT * FROM CZMST_SE WHERE (SOPNUMBE = @SOPNUMBE) AND (ITEMNMBR = @ITEMNMBR) AND (ORD = @ORD)";

						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD });

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

		public int CountQuerySopnumbe_SE(string SOPNUMBE)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = "SELECT COUNT(*) FROM CZMST_SE WHERE (SOPNUMBE = @SOPNUMBE)";
					command.CommandType = System.Data.CommandType.Text;

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return 0;
					}
					else
					{
						Int64 pp = (Int64)returnValue;
						return (int)pp;
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

		public int DeleteQuery_SE(int CountEntries)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_SE WHERE (CountEntries = @CountEntries)";

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

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable GetDataByCountEntries_SE(int CountEntries)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SEDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST_SE WHERE (CountEntries = @CountEntries)";
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


		public int Update_SE(object data)
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
					InitializeCommandInsert_SE(commandInsert);
					//InitializeCommandUpdate_SE(commandUpdate);
					InitializeCommandDelete_SE(commandDelete);
					InitializeCommandSelect_SE(commandSelect);

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

		public void InitializeCommandInsert_SE(SQLiteCommand command)
		{
			command.CommandText = @"INSERT INTO CZMST_SE (" +
				" CountEntries, SOPNUMBE, ITEMNMBR, ITEMTYPE, ITEMDESC," + 
				" VNDDOCNM, VNDITNUM, ORD, CZ_CarKod, LOCNCODE," + 
				" QTYSHPPD, QTYPACK, CZ_DatVyr_Track, CZ_DatVyr_Delka, CZ_SerNum_Track," + 
				" CZ_SerNum_Delka, CZ_SW_Track, CZ_SW_Delka, CZ_Doslo, DEX_ROW_ID," + 
				" Note, TYPEPAL, QTYPAL, PRINTED, PRIORITY," + 
				" SKL_ID, MJ, CZ_REZ1_TRACK, CZ_REZ2_TRACK, ITEMCODE," + 
				" WEIGHT, CZ_Expirace_Track" + 
				" ) VALUES ( " +
				" @CountEntries, @SOPNUMBE, @ITEMNMBR, @ITEMTYPE, @ITEMDESC," + 
				" @VNDDOCNM, @VNDITNUM, @ORD, @CZ_CarKod, @LOCNCODE," + 
				" @QTYSHPPD, @QTYPACK, @CZ_DatVyr_Track, @CZ_DatVyr_Delka, @CZ_SerNum_Track," + 
				" @CZ_SerNum_Delka, @CZ_SW_Track, @CZ_SW_Delka, @CZ_Doslo, @DEX_ROW_ID," + 
				" @Note, @TYPEPAL, @QTYPAL, @PRINTED, @PRIORITY," + 
				" @SKL_ID, @MJ, @CZ_REZ1_TRACK, @CZ_REZ2_TRACK, @ITEMCODE," + 
				" @WEIGHT, @CZ_Expirace_Track" + 
				" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, SourceColumn = "ITEMTYPE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, SourceColumn = "ITEMDESC" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
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
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Doslo", DbType = System.Data.DbType.Int32, SourceColumn = "CZ_Doslo" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Note", DbType = System.Data.DbType.String, SourceColumn = "Note" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPAL", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Boolean, SourceColumn = "PRINTED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRIORITY", DbType = System.Data.DbType.Byte, SourceColumn = "PRIORITY" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ1_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ1_TRACK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_REZ2_TRACK", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_REZ2_TRACK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_Expirace_Track", DbType = System.Data.DbType.Byte, SourceColumn = "CZ_Expirace_Track" });			
		}

		//public void InitializeCommandUpdate_SE(SQLiteCommand command)
		//{
			//command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODBER_ID", DbType = System.Data.DbType.String, SourceColumn = "ODBER_ID" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.Guid, SourceColumn = "guid" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Boolean, SourceColumn = "PRINTED" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });
			//command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
		//}

		public void InitializeCommandDelete_SE(SQLiteCommand command)
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

		public void InitializeCommandSelect_SE(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_SE";
		}


		#endregion


		#endregion

		#region SE_SN

		public int FillBySERLNMBR_SE_SN(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNDataTable dataTable, string SERLNMBR)
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
						adapter.SelectCommand.CommandText = "SELECT CountEntries, ITEMNMBR, SERLNMBR, DEX_ROW_ID, QTY, SOPNUMBE, ORD, 0 Nacteno FROM CZMST_SE_SN where SERLNMBR=@SERLNMBR";
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

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNDataTable GetDataByKeyNacteno_SE_SN(int CountEntries, string SOPNUMBE, string ITEMNMBR, int? ORD)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SE_SNDataTable();
			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT CZMST_SE_SN.CountEntries, CZMST_SE_SN.ITEMNMBR, CZMST_SE_SN.SERLNMBR, CZMST_SE_SN.QTY, CZMST_SE_SN.SOPNUMBE, CZMST_SE_SN.ORD, SUM(CZMST_SI.QTYSHPPD) AS Nacteno, CZMST_SE_SN.DEX_ROW_ID  " + 
							" FROM CZMST_SE_SN " + 
							" LEFT OUTER JOIN CZMST_SI ON " + 
							" CZMST_SE_SN.CountEntries = CZMST_SI.CountEntries " + 
							" AND CZMST_SE_SN.SOPNUMBE = CZMST_SI.SOPNUMBE " + 
							" AND CZMST_SE_SN.ITEMNMBR = CZMST_SI.ITEMNMBR " + 
							" AND CZMST_SE_SN.ORD = CZMST_SI.ORD " + 
							" AND CZMST_SE_SN.SERLNMBR = CZMST_SI.SERLTNUM " + 
							" GROUP BY  " + 
							" CZMST_SE_SN.CountEntries, CZMST_SE_SN.ITEMNMBR, CZMST_SE_SN.SERLNMBR, CZMST_SE_SN.QTY, CZMST_SE_SN.SOPNUMBE, CZMST_SE_SN.ORD, CZMST_SE_SN.DEX_ROW_ID " + 
							" HAVING  " + 
							"(CZMST_SE_SN.CountEntries = @CountEntries) " + 
							" AND (CZMST_SE_SN.SOPNUMBE = @SOPNUMBE) " + 
							" AND (CZMST_SE_SN.ITEMNMBR = @ITEMNMBR) " + 
							" AND (CZMST_SE_SN.ORD = @ORD)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.String, Value = ORD.HasValue ?  ORD.Value : (object)DBNull.Value  });

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


		public int Update_SE_SN(object data)
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
					InitializeCommandInsert_SE_SN(commandInsert);
					//InitializeCommandUpdate_SE_SN(commandUpdate);
					//InitializeCommandDelete_SE_SN(commandDelete);
					InitializeCommandSelect_SE_SN(commandSelect);

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

		public void InitializeCommandInsert_SE_SN(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_SE_SN ( " +
				"CountEntries, ITEMNMBR, SERLNMBR, QTY, DEX_ROW_ID, SOPNUMBE, ORD, Expirace " + 
				" ) VALUES (" +
				" @CountEntries, @ITEMNMBR, @SERLNMBR, @QTY, @DEX_ROW_ID, @SOPNUMBE, @ORD, @Expirace" + 
				" ) ";
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLNMBR", DbType = System.Data.DbType.String, SourceColumn = "SERLNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTY", DbType = System.Data.DbType.Decimal, SourceColumn = "QTY" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace" });			
		}

		//public void InitializeCommandUpdate_SE_SN(SQLiteCommand command)
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

		//public void InitializeCommandDelete_SE_SN(SQLiteCommand command)
		//{
		//	command.CommandText = "DELETE FROM CZMST_SI WHERE (guid = @guid)";

		//	command.Parameters.Add(new SQLiteParameter()
		//	{
		//		ParameterName = "@guid",
		//		DbType = System.Data.DbType.Guid,
		//		SourceColumn = "guid",
		//		SourceVersion = System.Data.DataRowVersion.Original
		//	});
		//}

		public void InitializeCommandSelect_SE_SN(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_SE_SN";
		}


		#endregion


		#endregion

		#region SI

        public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable SI_GetData()
        {
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable();

            try
            {
                Connection_Open();

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
                {
                    using (var command = this.Connection.CreateCommand())
                    {
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.Connection = this.Connection;
                        adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST_SI";

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

        public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable SI_GetDataByItemnmbr(string itemnmbr)
        {
            Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable();

            try
            {
                Connection_Open();

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
                {
                    using (var command = this.Connection.CreateCommand())
                    {
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.Connection = this.Connection;
                        adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST_SI WHERE (ITEMNMBR = @ITEMNMBR)";
                        adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = itemnmbr});

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

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable GetDataByItemnmbrSopnumbeOrd_SI(string ITEMNMBR, string SOPNUMBE, int ORD)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST_SI WHERE (ITEMNMBR = @ITEMNMBR) AND (SOPNUMBE = @SOPNUMBE) AND (ORD = @ORD)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.String, Value = ORD });

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

		public int DeleteQueryByGuid_SI(Guid guid)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_SI WHERE (guid = @guid)";

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

		public int DeleteQueryByCountEntries_SI(int CountEntries)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_SI WHERE (CountEntries = @CountEntries)";
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

		public int DeleteByItemnmbrSopnumbeOrd_SI(string ITEMNMBR, string SOPNUMBE, int ORD)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_SI WHERE (ITEMNMBR = @ITEMNMBR) AND (SOPNUMBE = @SOPNUMBE) AND (ORD = @ORD)";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE, SourceVersion = DataRowVersion.Original });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD, SourceVersion = DataRowVersion.Original });

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

		public int DeleteQuery_SI()
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "DELETE FROM CZMST_SI";

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

		public int Update_SI(object data)
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
					InitializeCommandInsert_SI(commandInsert);
					InitializeCommandUpdate_SI(commandUpdate);
					InitializeCommandDelete_SI(commandDelete);
					InitializeCommandSelect_SI(commandSelect);

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

		public void InitializeCommandInsert_SI(SQLiteCommand command)
		{
			command.CommandText =  @"INSERT INTO [CZMST_SI] (" + 
									" [CountEntries], [SOPNUMBE], [ITEMNMBR], [ORD], [VNDDOCNM]," + 
									" [VNDITNUM], [CZ_CarKod], [LOCNCODE], [QTYSHPPD], [QTYPACK]," + 
									" [QTYSHPPDMJ], [SERLTNUM], [KOD_SW], [DAT_VYROBY], [REZ_1]," + 
									" [ODBER_ID], [DATEDONE], [TIMEDONE], [USER_ID], [DEX_ROW_ID]," + 
									" [guid], [TYPEPAL], [NMBRPAL], [PRINTED], [REZ_2]," + 
									" [INPUT_MODE], [ID_TERMINAL], [SKL_ID], [MJ], [ITEMCODE]," + 
									" [WEIGHT], [Expirace] " + 
									" ) VALUES ( " +
									" @CountEntries, @SOPNUMBE, @ITEMNMBR, @ORD, @VNDDOCNM," + 
									" @VNDITNUM, @CZ_CarKod, @LOCNCODE, @QTYSHPPD, @QTYPACK," + 
									" @QTYSHPPDMJ, @SERLTNUM, @KOD_SW, @DAT_VYROBY, @REZ_1," + 
									" @ODBER_ID, @DATEDONE, @TIMEDONE, @USER_ID, @DEX_ROW_ID," + 
									" @guid, @TYPEPAL, @NMBRPAL, @PRINTED, @REZ_2," + 
									" @INPUT_MODE, @ID_TERMINAL, @SKL_ID, @MJ, @ITEMCODE," + 
									" @WEIGHT, @Expirace" + 
									" )";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODBER_ID", DbType = System.Data.DbType.String, SourceColumn = "ODBER_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID"});
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.Guid, SourceColumn = "guid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Boolean, SourceColumn = "PRINTED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });
			
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace" });			
		}

		public void InitializeCommandUpdate_SI(SQLiteCommand command)
		{
					command.CommandText = @"UPDATE [CZMST_SI] SET " +
						" [CountEntries] = @CountEntries, " +
						" [SOPNUMBE] = @SOPNUMBE," +
						" [ITEMNMBR] = @ITEMNMBR," +
						" [ORD] = @ORD," +
						" [VNDDOCNM] = @VNDDOCNM," +
						" [VNDITNUM] = @VNDITNUM," +
						" [CZ_CarKod] = @CZ_CarKod," +
						" [LOCNCODE] = @LOCNCODE," +
						" [QTYSHPPD] = @QTYSHPPD," +
						" [QTYPACK] = @QTYPACK," +
						" [QTYSHPPDMJ] = @QTYSHPPDMJ," +
						" [SERLTNUM] = @SERLTNUM," +
						" [KOD_SW] = @KOD_SW," +
						" [DAT_VYROBY] = @DAT_VYROBY," +
						" [REZ_1] = @REZ_1," +
						" [ODBER_ID] = @ODBER_ID," +
						" [DATEDONE] = @DATEDONE," +
						" [TIMEDONE] = @TIMEDONE," +
						" [USER_ID] = @USER_ID," +
						" [DEX_ROW_ID] = @DEX_ROW_ID," +
						" [TYPEPAL] = @TYPEPAL," +
						" [NMBRPAL] = @NMBRPAL," +
						" [PRINTED] = @PRINTED," +
						" [REZ_2] = @REZ_2," +
						" [INPUT_MODE] = @INPUT_MODE," +
						" [ID_TERMINAL] = @ID_TERMINAL," +
						" [SKL_ID] = @SKL_ID," +
						" [MJ] = @MJ," +
						" [ITEMCODE] = @ITEMCODE," +
						" [WEIGHT] = @WEIGHT," +
						" [Expirace] = @Expirace " +
						"  WHERE (guid = @guid)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, SourceColumn = "SOPNUMBE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, SourceColumn = "ORD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, SourceColumn = "VNDDOCNM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, SourceColumn = "VNDITNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, SourceColumn = "CZ_CarKod" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, SourceColumn = "LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYPACK" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, SourceColumn = "QTYSHPPDMJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, SourceColumn = "SERLTNUM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, SourceColumn = "KOD_SW" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, SourceColumn = "DAT_VYROBY" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, SourceColumn = "REZ_1" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODBER_ID", DbType = System.Data.DbType.String, SourceColumn = "ODBER_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, SourceColumn = "DATEDONE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, SourceColumn = "TIMEDONE"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, SourceColumn = "USER_ID"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, SourceColumn = "DEX_ROW_ID"});
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, SourceColumn = "TYPEPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, SourceColumn = "NMBRPAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Boolean, SourceColumn = "PRINTED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, SourceColumn = "REZ_2" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, SourceColumn = "INPUT_MODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, SourceColumn = "ID_TERMINAL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, SourceColumn = "MJ" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, SourceColumn = "ITEMCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, SourceColumn = "WEIGHT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, SourceColumn = "Expirace" });			

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.Guid, SourceColumn = "guid" , SourceVersion = DataRowVersion.Original });
		}

		public void InitializeCommandDelete_SI(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST_SI WHERE (guid = @guid)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@guid",
				DbType = System.Data.DbType.Guid,
				SourceColumn = "guid",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_SI(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_SI";
		}


		#endregion

		#region INSERT

		public int InsertQuery_SI(
	        int CountEntries,
	        string SOPNUMBE,
	        string ITEMNMBR,
	        int ORD,
	        string VNDDOCNM,
	        string VNDITNUM,
	        string CZ_CarKod,
	        string LOCNCODE,
	        decimal QTYSHPPD,
	        decimal QTYPACK,
	        decimal QTYSHPPDMJ,
	        string SERLTNUM,
	        string KOD_SW,
	        string DAT_VYROBY,
	        string REZ_1,
	        string ODBER_ID,
	        string DATEDONE,
	        string TIMEDONE,
	        int USER_ID,
	        int? DEX_ROW_ID,
	        Guid guid,
	        string TYPEPAL,
	        string NMBRPAL,
	        bool? PRINTED,
	        string REZ_2,
	        byte INPUT_MODE,
	        int ID_TERMINAL,
	        string SKL_ID,
	        string MJ,
	        string ITEMCODE,
	        decimal? WEIGHT,
            DateTime? Expirace)
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
						adapter.InsertCommand.CommandText = @"INSERT INTO [CZMST_SI] ([CountEntries], [SOPNUMBE], [ITEMNMBR], [ORD], [VNDDOCNM], [VNDITNUM], [CZ_CarKod], [LOCNCODE], [QTYSHPPD], [QTYPACK], [QTYSHPPDMJ], [SERLTNUM], [KOD_SW], [DAT_VYROBY], [REZ_1], [ODBER_ID], [DATEDONE], [TIMEDONE], [USER_ID], [DEX_ROW_ID], [guid], [TYPEPAL], [NMBRPAL], [PRINTED], [REZ_2], [INPUT_MODE], [ID_TERMINAL], [SKL_ID], [MJ], [ITEMCODE], [WEIGHT], [Expirace]) " +
						" VALUES " +
						"(@CountEntries, @SOPNUMBE, @ITEMNMBR, @ORD, @VNDDOCNM, @VNDITNUM, @CZ_CarKod, @LOCNCODE, @QTYSHPPD, @QTYPACK, @QTYSHPPDMJ, @SERLTNUM, @KOD_SW, @DAT_VYROBY, @REZ_1, @ODBER_ID, @DATEDONE, @TIMEDONE, @USER_ID, @DEX_ROW_ID, @guid, @TYPEPAL, @NMBRPAL, @PRINTED, @REZ_2, @INPUT_MODE, @ID_TERMINAL, @SKL_ID, @MJ, @ITEMCODE, @WEIGHT, @Expirace)";

						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						#region PARAMETRY

						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDDOCNM", DbType = System.Data.DbType.String, Value = VNDDOCNM == null ? (object)DBNull.Value : VNDDOCNM });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, Value = VNDITNUM == null ? (object)DBNull.Value : VNDITNUM });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, Value = CZ_CarKod == null ? (object)DBNull.Value : CZ_CarKod });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = LOCNCODE == null ? (object)DBNull.Value : LOCNCODE });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, Value = QTYSHPPD });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = QTYPACK });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@QTYSHPPDMJ", DbType = System.Data.DbType.Decimal, Value = QTYSHPPDMJ });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, Value = SERLTNUM == null ? (object)DBNull.Value : SERLTNUM });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@KOD_SW", DbType = System.Data.DbType.String, Value = KOD_SW == null ? (object)DBNull.Value : KOD_SW });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DAT_VYROBY", DbType = System.Data.DbType.String, Value = DAT_VYROBY == null ? (object)DBNull.Value : DAT_VYROBY });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, Value = REZ_1 == null ? (object)DBNull.Value : REZ_1 });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ODBER_ID", DbType = System.Data.DbType.String, Value = ODBER_ID == null ? (object)DBNull.Value : ODBER_ID });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DATEDONE", DbType = System.Data.DbType.String, Value = DATEDONE == null ? (object)DBNull.Value : DATEDONE });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TIMEDONE", DbType = System.Data.DbType.String, Value = TIMEDONE == null ? (object)DBNull.Value : TIMEDONE });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, Value = USER_ID });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, Value = DEX_ROW_ID.HasValue ? DEX_ROW_ID.Value : (object)DBNull.Value });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.Guid, Value = guid });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TYPEPAL", DbType = System.Data.DbType.String, Value = TYPEPAL == null ? (object)DBNull.Value : TYPEPAL });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = NMBRPAL == null ? (object)DBNull.Value : NMBRPAL });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRINTED", DbType = System.Data.DbType.Boolean, Value = PRINTED.HasValue ? PRINTED.Value : (object)DBNull.Value });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_2", DbType = System.Data.DbType.String, Value = REZ_2 == null ? (object)DBNull.Value : REZ_2 });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@INPUT_MODE", DbType = System.Data.DbType.Byte, Value = INPUT_MODE });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ID_TERMINAL", DbType = System.Data.DbType.Int32, Value = ID_TERMINAL });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = SKL_ID == null ? (object)DBNull.Value : SKL_ID });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, Value = MJ == null ? (object)DBNull.Value : MJ });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = ITEMCODE == null ? (object)DBNull.Value : ITEMCODE });
						command.Parameters.Add(new SQLiteParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, Value = WEIGHT.HasValue ? WEIGHT.Value : (object)DBNull.Value });
                        command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Expirace", DbType = System.Data.DbType.DateTime, Value = Expirace.HasValue ? Expirace.Value : (object)DBNull.Value });

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

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable GetDataByGuid_SI(Guid Guid)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST_SI WHERE (guid = @Guid)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@Guid", DbType = System.Data.DbType.Guid, Value = Guid });

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

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable GetDataByCountEntries_SI(int CountEntries)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST_SI WHERE (CountEntries = @CountEntries)";
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

		public int FillBy_TOPjeden_NMBRPAL_SI(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable dataTable, string nmbrpal)
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
						adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST_SI WHERE (NMBRPAL = @nmbrpal) LIMIT 1";
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

		public string CZMST_SI_GetFirstSkladID()
		{
			#region 1.Existujici zaznam z SI

			try
			{
				Connection_Open();

				using (System.Data.SQLite.SQLiteCommand scecommand = this.Connection.CreateCommand())
				{
					scecommand.CommandText = "Select * from czmst_si LIMIT 1";
					System.Data.SQLite.SQLiteDataReader sdr = scecommand.ExecuteReader();

					if (sdr.Read())
					{
						return Convert.ToString(sdr["SKL_ID"] is System.DBNull ? string.Empty : sdr["SKL_ID"]).Trim();
					}
				}

				return string.Empty;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return string.Empty;
			}
			finally
			{
				Connection_Close();
			}

			#endregion
		}

		/// <summary>
		/// Počet záznamů ve výstupní tabulce
		/// </summary>
		/// <returns>Počet záznamů</returns>
		public int CZMST_SI_Count_All()
		{
			try
			{
				Connection_Open();

				using (var command = new SQLiteCommand("select count(*) from czmst_si", this.Connection))
				{
					return Convert.ToInt32(command.ExecuteScalar());
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
		/// Vrati zaznam z SI tabulky na danem indexu
		/// </summary>
		/// <param name="index">index zaznamu z SI tabulky</param>
		/// <returns></returns>
		public DataSets.Vydej.CZMST_SIRow CZMST_SI_Get_One(int index)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "SELECT * FROM CZMST_SI";

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

						return _Routines.LoadRowFromReader(reader, new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable()) as Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow;
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
		/// Aktualizuje/Vlozi/Smaze jeden zaznam v databazi dle Update/Insert/Delete pravidla SEAdapteru a RowState zaznamu
		/// </summary>
		/// <param name="vydejRow">zaznam k aktualizaci</param>
		/// <returns>Pocet ovlivnenych zaznamu</returns>
		public int CZMST_SI_Update_One(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow vydejRow)
		{
			try
			{
				return this.Update_SI(vydejRow);
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return -1;
			}
			finally
			{
			}
		}

		/// <summary>
		/// Vraci pocet duplicitnich zaznamu v SI
		/// </summary>
		/// <param name="ITEMNMBR"></param>
		/// <param name="SOPNUMBE"></param>
		/// <param name="ORD"></param>
		/// <param name="SERLTNUM"></param>
		/// <returns></returns>
		public int CZMST_SI_Get_Pocet_Duplicit_SN_Count(string ITEMNMBR, string SOPNUMBE, int ORD, string SERLTNUM)
		{
			int sernumCount = 0;
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText =
						"Select Count(*) from czmst_si where ITEMNMBR=@ITEMNMBR" +
						" AND SOPNUMBE=@SOPNUMBE" +
						" AND ORD=@ORD" +
						" AND SERLTNUM=@SERLTNUM";
					command.Parameters.AddWithValue("@ITEMNMBR", ITEMNMBR);
					command.Parameters.AddWithValue("@SOPNUMBE", SOPNUMBE);
					command.Parameters.AddWithValue("@ORD", ORD);
					command.Parameters.AddWithValue("@SERLTNUM", SERLTNUM);

					sernumCount = Convert.ToInt32(command.ExecuteScalar());
					return sernumCount;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return sernumCount; // pri vyijmce by melo byt 0
			}
			finally
			{
				Connection_Close();
			}

		}

		public int Fill_SI(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable dt)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_SI";
						//adapter.SelectCommand.Parameters.AddRange(Parameter);

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


		#endregion

		#region SI_RFID

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_RFIDDataTable GetDataBy_M_ID_SI_RFID(string M_ID)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_RFIDDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_RFIDDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT * FROM CZMST_SI_RFID WHERE (M_ID = @M_ID)";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@M_ID", DbType = System.Data.DbType.String, Value = M_ID == null ? (object)DBNull.Value : M_ID });

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

		public int Update_SI_RFID(object data)
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
					InitializeCommandInsert_SI_RFID(commandInsert);
					//InitializeCommandUpdate_SI_RFID(commandUpdate);
					InitializeCommandDelete_SI_RFID(commandDelete);
					InitializeCommandSelect_SI_RFID(commandSelect);

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

		public void InitializeCommandInsert_SI_RFID(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_SI_RFID (ITEMNMBR, SEQUENCENMBR, SKL_ID, CountEntries, DOCUMENTNMBR, ORD, SERLNMBR, guid, M_ID, M_TID, M_EPC, M_USER, M_RESERVED, O_M_ID, O_M_TID, O_M_EPC, O_M_USER, O_M_RESERVED, TerminalID, UserID, Created_T) " + 
				" VALUES (@ITEMNMBR,@SEQUENCENMBR,@SKL_ID,@CountEntries,@DOCUMENTNMBR,@ORD,@SERLNMBR,@guid,@M_ID,@M_TID,@M_EPC,@M_USER,@M_RESERVED,@O_M_ID,@O_M_TID,@O_M_EPC,@O_M_USER,@O_M_RESERVED,@TerminalID,@UserID,@Created_T)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, SourceColumn = "ITEMNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SEQUENCENMBR", DbType = System.Data.DbType.String, SourceColumn = "SEQUENCENMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, SourceColumn = "SKL_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.String, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@DOCUMENTNMBR", DbType = System.Data.DbType.String, SourceColumn = "DOCUMENTNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.String, SourceColumn = "ORD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SERLNMBR", DbType = System.Data.DbType.String, SourceColumn = "SERLNMBR" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@guid", DbType = System.Data.DbType.String, SourceColumn = "guid" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@M_ID", DbType = System.Data.DbType.String, SourceColumn = "M_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@M_TID", DbType = System.Data.DbType.String, SourceColumn = "M_TID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@M_EPC", DbType = System.Data.DbType.String, SourceColumn = "M_EPC" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@M_USER", DbType = System.Data.DbType.String, SourceColumn = "M_USER" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@M_RESERVED", DbType = System.Data.DbType.String, SourceColumn = "M_RESERVED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_M_ID", DbType = System.Data.DbType.String, SourceColumn = "O_M_ID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_M_TID", DbType = System.Data.DbType.String, SourceColumn = "O_M_TID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_M_EPC", DbType = System.Data.DbType.String, SourceColumn = "O_M_EPC" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_M_USER", DbType = System.Data.DbType.String, SourceColumn = "O_M_USER" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@O_M_RESERVED", DbType = System.Data.DbType.String, SourceColumn = "O_M_RESERVED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TerminalID", DbType = System.Data.DbType.String, SourceColumn = "TerminalID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@UserID", DbType = System.Data.DbType.String, SourceColumn = "UserID" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@Created_T", DbType = System.Data.DbType.String, SourceColumn = "Created_T" });
		}

		//public void InitializeCommandUpdate_SI_RFID(SQLiteCommand command)
		//{
		//    command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TISKARNA_NAME", DbType = System.Data.DbType.String, SourceColumn = "TISKARNA_NAME" });
		//    command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRAC_ID", DbType = System.Data.DbType.String, SourceColumn = "PRAC_ID" });
		//}

		public void InitializeCommandDelete_SI_RFID(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST_SI_RFID WHERE (guid = @guid)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@guid",
				DbType = System.Data.DbType.Guid,
				SourceColumn = "guid",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_SI_RFID(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_SI_RFID";
		}


		#endregion


		#endregion

		#region Parametry

		public int Fill_Param(Fask.SQLiteDBs.DataSets.Vydej.ParametryDataTable dataTable)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM Parametry";
				
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


		public Fask.SQLiteDBs.DataSets.Vydej.ParametryDataTable GetData_Param()
		{
			Fask.SQLiteDBs.DataSets.Vydej.ParametryDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.ParametryDataTable();
			Fill_Param(dt);
			return dt;
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
				//using (var commandSelect = this.Connection.CreateCommand())
				{
					InitializeCommandInsert_Param(commandInsert);
					InitializeCommandUpdate_Param(commandUpdate);
					//InitializeCommandDelete_Param(commandDelete);
					//InitializeCommandSelect_Param(commandSelect);

					using (var adapter = new System.Data.SQLite.SQLiteDataAdapter())
					{
						//adapter.DeleteCommand = commandDelete;
						adapter.InsertCommand = commandInsert;
						adapter.UpdateCommand = commandUpdate;
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

		#region Inicialize metody

		public void InitializeCommandInsert_Param(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO Parametry (ENABLE_LISTSNIM, CONFIG_LISTSNIM, CONFIG_POKRDOHLED, CONFIG_PTATSE_NEANO, " + 
				" CONFIG_KONT_UPL_POL, CONFIG_ZADAT_MN_POKAZDE, ENABLE_DOHLED_ODB, CONFIG_DOHLED_ODB, CONFIG_KONT_DOKONCENOSTI, " + 
				" CONFIG_KONT_DELKA, CONFIG_KONT_SN_CARKOD, CONFIG_DUPLIC_SN, CONFIG_NOVA_KARTA, CONFIG_SKRYT_MNOZSTVI, " + 
				" CONFIG_SNIMEJ_PRI_LISTU, CONFIG_KONT_NUL_DELKA, CONFIG_SNIM_LOCNCODE, CONFIG_MNOZSTVI_PREDVYPLNIT, " + 
				" CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA, CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI, CONFIG_MNOZSTVI_ZADAVAT, CONFIG_MNOZSTVI_SCANNEREM, " + 
				" CONFIG_KONT_PREDLOHA_SN, CONFIG_LOCNCODE_OVERIT_SCANEREM, CONFIG_POUZIT_CISELNIK_ZBOZI, CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU, " + 
				" CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU, CONFIG_LOKACE_POVOLIT, CONFIG_LOKACE_TIMEOUT) " + 
				" VALUES " +
				" (@ENABLE_LISTSNIM,@CONFIG_LISTSNIM,@CONFIG_POKRDOHLED,@CONFIG_PTATSE_NEANO,@CONFIG_KONT_UPL_POL,@CONFIG_ZADAT_MN_POKAZDE," + 
				" @ENABLE_DOHLED_ODB,@CONFIG_DOHLED_ODB,@CONFIG_KONT_DOKONCENOSTI,@CONFIG_KONT_DELKA,@CONFIG_KONT_SN_CARKOD,@CONFIG_DUPLIC_SN," + 
				" @CONFIG_NOVA_KARTA,@CONFIG_SKRYT_MNOZSTVI,@CONFIG_SNIMEJ_PRI_LISTU,@CONFIG_KONT_NUL_DELKA,@CONFIG_SNIM_LOCNCODE," + 
				" @CONFIG_MNOZSTVI_PREDVYPLNIT,@CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA,@CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI,@CONFIG_MNOZSTVI_ZADAVAT," + 
				" @CONFIG_MNOZSTVI_SCANNEREM,@CONFIG_KONT_PREDLOHA_SN,@CONFIG_LOCNCODE_OVERIT_SCANEREM,@CONFIG_POUZIT_CISELNIK_ZBOZI," + 
				" @CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU,@CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU,@CONFIG_LOKACE_POVOLIT,@CONFIG_LOKACE_TIMEOUT)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ENABLE_LISTSNIM", DbType = System.Data.DbType.Boolean, SourceColumn = "ENABLE_LISTSNIM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LISTSNIM", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LISTSNIM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_POKRDOHLED", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_POKRDOHLED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_PTATSE_NEANO", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_PTATSE_NEANO" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_UPL_POL", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_UPL_POL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_ZADAT_MN_POKAZDE", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_ZADAT_MN_POKAZDE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ENABLE_DOHLED_ODB", DbType = System.Data.DbType.Boolean, SourceColumn = "ENABLE_DOHLED_ODB" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_DOHLED_ODB", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_DOHLED_ODB" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_DOKONCENOSTI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_DOKONCENOSTI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_DELKA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_DELKA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_SN_CARKOD", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_SN_CARKOD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_DUPLIC_SN", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_DUPLIC_SN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_NOVA_KARTA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_NOVA_KARTA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SKRYT_MNOZSTVI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SKRYT_MNOZSTVI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIMEJ_PRI_LISTU", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIMEJ_PRI_LISTU" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_NUL_DELKA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_NUL_DELKA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIM_LOCNCODE", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIM_LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_ZADAVAT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_ZADAVAT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_SCANNEREM", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_SCANNEREM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_PREDLOHA_SN", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_PREDLOHA_SN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOCNCODE_OVERIT_SCANEREM", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LOCNCODE_OVERIT_SCANEREM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_POUZIT_CISELNIK_ZBOZI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_POUZIT_CISELNIK_ZBOZI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU", DbType = System.Data.DbType.String, SourceColumn = "CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU", DbType = System.Data.DbType.String, SourceColumn = "CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_POVOLIT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LOKACE_POVOLIT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_TIMEOUT", DbType = System.Data.DbType.Int32, SourceColumn = "CONFIG_LOKACE_TIMEOUT" });

		}

		public void InitializeCommandUpdate_Param(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE Parametry SET " +
				" ENABLE_LISTSNIM = @ENABLE_LISTSNIM, CONFIG_LISTSNIM = @CONFIG_LISTSNIM, CONFIG_POKRDOHLED = @CONFIG_POKRDOHLED, CONFIG_PTATSE_NEANO = @CONFIG_PTATSE_NEANO, " +
				" CONFIG_KONT_UPL_POL = @CONFIG_KONT_UPL_POL, CONFIG_ZADAT_MN_POKAZDE = @CONFIG_ZADAT_MN_POKAZDE, ENABLE_DOHLED_ODB = @ENABLE_DOHLED_ODB, CONFIG_DOHLED_ODB = @CONFIG_DOHLED_ODB," +
				" CONFIG_KONT_DOKONCENOSTI = @CONFIG_KONT_DOKONCENOSTI, CONFIG_KONT_DELKA = @CONFIG_KONT_DELKA, CONFIG_KONT_SN_CARKOD = @CONFIG_KONT_SN_CARKOD, CONFIG_DUPLIC_SN = @CONFIG_DUPLIC_SN," +
				" CONFIG_NOVA_KARTA = @CONFIG_NOVA_KARTA, CONFIG_SKRYT_MNOZSTVI = @CONFIG_SKRYT_MNOZSTVI, CONFIG_SNIMEJ_PRI_LISTU = @CONFIG_SNIMEJ_PRI_LISTU, CONFIG_KONT_NUL_DELKA = @CONFIG_KONT_NUL_DELKA, " +
				" CONFIG_SNIM_LOCNCODE = @CONFIG_SNIM_LOCNCODE, CONFIG_MNOZSTVI_PREDVYPLNIT = @CONFIG_MNOZSTVI_PREDVYPLNIT, CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA = @CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA," +
				" CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI = @CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI, CONFIG_MNOZSTVI_ZADAVAT = @CONFIG_MNOZSTVI_ZADAVAT, CONFIG_MNOZSTVI_SCANNEREM = @CONFIG_MNOZSTVI_SCANNEREM," +
				" CONFIG_KONT_PREDLOHA_SN = @CONFIG_KONT_PREDLOHA_SN, CONFIG_LOCNCODE_OVERIT_SCANEREM = @CONFIG_LOCNCODE_OVERIT_SCANEREM, CONFIG_POUZIT_CISELNIK_ZBOZI = @CONFIG_POUZIT_CISELNIK_ZBOZI, " +
				" CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU = @CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU, CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU = @CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU, " +
				" CONFIG_LOKACE_POVOLIT = @CONFIG_LOKACE_POVOLIT, CONFIG_LOKACE_TIMEOUT = @CONFIG_LOKACE_TIMEOUT";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ENABLE_LISTSNIM", DbType = System.Data.DbType.Boolean, SourceColumn = "ENABLE_LISTSNIM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LISTSNIM", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LISTSNIM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_POKRDOHLED", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_POKRDOHLED" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_PTATSE_NEANO", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_PTATSE_NEANO" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_UPL_POL", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_UPL_POL" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_ZADAT_MN_POKAZDE", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_ZADAT_MN_POKAZDE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ENABLE_DOHLED_ODB", DbType = System.Data.DbType.Boolean, SourceColumn = "ENABLE_DOHLED_ODB" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_DOHLED_ODB", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_DOHLED_ODB" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_DOKONCENOSTI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_DOKONCENOSTI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_DELKA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_DELKA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_SN_CARKOD", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_SN_CARKOD" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_DUPLIC_SN", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_DUPLIC_SN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_NOVA_KARTA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_NOVA_KARTA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SKRYT_MNOZSTVI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SKRYT_MNOZSTVI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIMEJ_PRI_LISTU", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIMEJ_PRI_LISTU" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_NUL_DELKA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_NUL_DELKA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_SNIM_LOCNCODE", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_SNIM_LOCNCODE" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_ZADAVAT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_ZADAVAT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_MNOZSTVI_SCANNEREM", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_MNOZSTVI_SCANNEREM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_KONT_PREDLOHA_SN", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_KONT_PREDLOHA_SN" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOCNCODE_OVERIT_SCANEREM", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LOCNCODE_OVERIT_SCANEREM" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_POUZIT_CISELNIK_ZBOZI", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_POUZIT_CISELNIK_ZBOZI" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU", DbType = System.Data.DbType.String, SourceColumn = "CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU", DbType = System.Data.DbType.String, SourceColumn = "CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_POVOLIT", DbType = System.Data.DbType.Boolean, SourceColumn = "CONFIG_LOKACE_POVOLIT" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CONFIG_LOKACE_TIMEOUT", DbType = System.Data.DbType.Int32, SourceColumn = "CONFIG_LOKACE_TIMEOUT" });
		}

		//public void InitializeCommandDelete_Param(SQLiteCommand command)
		//{
		//    command.CommandText = "DELETE FROM Parametry WHERE (CountEntries = @CountEntries)";

		//    command.Parameters.Add(new SQLiteParameter()
		//    {
		//        ParameterName = "@CountEntries",
		//        DbType = System.Data.DbType.Int32,
		//        SourceColumn = "CountEntries",
		//        SourceVersion = System.Data.DataRowVersion.Original
		//    });
		//}

		//public void InitializeCommandSelect_Param(SQLiteCommand command)
		//{
		//    command.CommandText = "Select * from Parametry";
		//}


		#endregion


		#endregion

		#region Sloucene

		public Fask.SQLiteDBs.DataSets.Vydej.SlouceneDataTable GetData_Sloucene()
		{
			Fask.SQLiteDBs.DataSets.Vydej.SlouceneDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.SlouceneDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT DISTINCT CountEntries FROM CZMST_SE";

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

		#region SI_SN

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_SNDataTable GetDataSERLTNUM_SI_SN(string SOPNUMBE, string ITEMNMBR, int CountEntries, string NMBRPAL, int ORD)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_SNDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_SNDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT SERLTNUM FROM CZMST_SI WHERE (SOPNUMBE = @SOPNUMBE) AND (ITEMNMBR = @ITEMNMBR) AND (CountEntries = @CountEntries) AND (NMBRPAL = @NMBRPAL) AND (ORD = @ORD)";

						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = NMBRPAL == null ? (object)DBNull.Value : NMBRPAL });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD });

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

		#region SI_TISK

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskDataTable GetData_SI_Tisk(int CountEntries)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT CountEntries, SOPNUMBE, ITEMNMBR, VNDITNUM, CZ_CarKod, SUM(QTYSHPPD) AS QTYSHPPD, USER_ID, TYPEPAL, NMBRPAL, ID_TERMINAL, SKL_ID, MJ, VNDDOCNM, ORD " + 
							" FROM CZMST_SI " + 
							" WHERE (CountEntries = @CountEntries) " + 
							" GROUP BY CountEntries, SOPNUMBE, ITEMNMBR, VNDITNUM, CZ_CarKod, USER_ID, TYPEPAL, NMBRPAL, ID_TERMINAL, SKL_ID, MJ, VNDDOCNM, ORD";
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

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskDataTable GetDataByNMBRPAL_SI_Tisk(string NMBRPAL)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_TiskDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT CountEntries, SOPNUMBE, ITEMNMBR, VNDITNUM, CZ_CarKod, SUM(QTYSHPPD) AS QTYSHPPD, USER_ID, TYPEPAL, NMBRPAL, ID_TERMINAL, SKL_ID, MJ, VNDDOCNM, ORD " + 
							" FROM CZMST_SI WHERE (NMBRPAL = @NMBRPAL) " + 
							" GROUP BY CountEntries, SOPNUMBE, ITEMNMBR, VNDITNUM, CZ_CarKod, USER_ID, TYPEPAL, NMBRPAL, ID_TERMINAL, SKL_ID, MJ, VNDDOCNM, ORD";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = NMBRPAL == null ? (object)DBNull.Value : NMBRPAL });

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

		#region SI_H

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIHDataTable GetDataByCountentries_SIH(int CountEntries)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIHDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIHDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_SIH WHERE (CountEntries = @CountEntries)";
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

		public int Update_SIH(object data)
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
					InitializeCommandInsert_SIH(commandInsert);
					InitializeCommandUpdate_SIH(commandUpdate);
					InitializeCommandDelete_SIH(commandDelete);
					InitializeCommandSelect_SIH(commandSelect);

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

		public void InitializeCommandInsert_SIH(SQLiteCommand command)
		{
			command.CommandText = "INSERT INTO CZMST_SIH (CountEntries, TISKARNA_NAME, PRAC_ID) VALUES (@CountEntries,@TISKARNA_NAME,@PRAC_ID)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TISKARNA_NAME", DbType = System.Data.DbType.String, SourceColumn = "TISKARNA_NAME" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRAC_ID", DbType = System.Data.DbType.String, SourceColumn = "PRAC_ID" });
		}

		public void InitializeCommandUpdate_SIH(SQLiteCommand command)
		{
			command.CommandText = @"UPDATE CZMST_SIH SET TISKARNA_NAME = @TISKARNA_NAME, PRAC_ID = @PRAC_ID WHERE (CountEntries = @CountEntries)";

			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, SourceColumn = "CountEntries" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@TISKARNA_NAME", DbType = System.Data.DbType.String, SourceColumn = "TISKARNA_NAME" });
			command.Parameters.Add(new SQLiteParameter() { ParameterName = "@PRAC_ID", DbType = System.Data.DbType.String, SourceColumn = "PRAC_ID" });
		}

		public void InitializeCommandDelete_SIH(SQLiteCommand command)
		{
			command.CommandText = "DELETE FROM CZMST_SIH WHERE (CountEntries = @CountEntries)";

			command.Parameters.Add(new SQLiteParameter()
			{
				ParameterName = "@CountEntries",
				DbType = System.Data.DbType.Int32,
				SourceColumn = "CountEntries",
				SourceVersion = System.Data.DataRowVersion.Original
			});
		}

		public void InitializeCommandSelect_SIH(SQLiteCommand command)
		{
			command.CommandText = "Select * from CZMST_SIH";
		}


		#endregion

		public int Fill_SIH(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIHDataTable dt)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_SIH";
						//adapter.SelectCommand.Parameters.AddRange(Parameter);

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


		#endregion

		#region SISE_Soupis

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_SE_SoupisDataTable GetDataByCountEntries_SISE_Soupis(int CountEntries)
		{

			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_SE_SoupisDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_SE_SoupisDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{
						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT SE.ITEMDESC, SE.CountEntries, SE.SOPNUMBE, SE.ITEMNMBR, SE.VNDITNUM, SE.CZ_CarKod, SUM(SE.QTYSHPPD) AS QTYSHPPD, SUM(SI.QTYSHPPD) AS QTYSHPPD_SI, SI.USER_ID, SI.TYPEPAL, SI.NMBRPAL, SI.ID_TERMINAL, SE.SKL_ID, SE.MJ, SE.VNDDOCNM, SE.ITEMCODE " + 
							" FROM CZMST_SE AS SE " + 
							" LEFT OUTER JOIN CZMST_SI AS SI ON SE.CountEntries = SI.CountEntries " + 
							" AND SE.ITEMNMBR = SI.ITEMNMBR " + 
							" AND SE.SOPNUMBE = SI.SOPNUMBE " + 
							" WHERE (SE.CountEntries = @CountEntries) " + 
							" GROUP BY SE.CountEntries, SE.SOPNUMBE, SE.ITEMNMBR, SE.VNDITNUM, SE.CZ_CarKod, SI.USER_ID, SI.TYPEPAL, SI.NMBRPAL, SI.ID_TERMINAL, SE.SKL_ID, SE.MJ, SE.VNDDOCNM, SE.ITEMDESC, SE.ITEMCODE";
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


		#endregion

		#region SUM(QTYSHPPPD)

		public decimal SUM_QTYSHPPD_SI(string SOPNUMBE, string ITEMNMBR, int ORD)
		{
			try
			{
				Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{

					command.CommandText = " SELECT SUM(QTYSHPPD) FROM CZMST_SI " +
											" WHERE ITEMNMBR = @ITEMNMBR " +
											" AND SOPNUMBE = @SOPNUMBE " +
											" AND ORD = @ORD";

					command.CommandType = System.Data.CommandType.Text;

					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD});

					object returnValue = command.ExecuteScalar();

					if (((returnValue == null) || (returnValue.GetType() == typeof(global::System.DBNull))))
					{
						return 0;
					}
					else
					{
						if (returnValue is long)
						{
							long pp = (long)returnValue;
							return Convert.ToDecimal(pp);
						}
						else
						{
							throw new Exception("zle čislo");
						}
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

		#region CZMSZ_SI_BV


		public int Fill_SI_BV(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_BVDataTable dt)
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
						adapter.SelectCommand.CommandText = "SELECT * FROM CZMST_SI_BV";
						//adapter.SelectCommand.Parameters.AddRange(Parameter);

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

		public Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_BVRow GetDataByNMBRPAL_CountEntries_SI_BV_Tisk(int CountEntries, string NMBRPAL)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_BVDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_BVDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT CountEntries, NMBRPAL, pal_WEIGHT, pal_W, pal_H, pal_D " +
							" FROM CZMST_SI_BV WHERE CountEntries = @CountEntries AND NMBRPAL = @NMBRPAL " +
							" GROUP BY CountEntries, NMBRPAL, pal_WEIGHT, pal_W, pal_H, pal_D ";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = NMBRPAL == null ? (object)DBNull.Value : NMBRPAL });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries  });

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

						if (dt != null && dt.Count > 0)
						{
							return dt.First();
						}
						else
							return null;
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

		public bool GetDataByNMBRPAL_CountEntries_SI_BV_Exist(int CountEntries, string NMBRPAL)
		{
			Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_BVDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SI_BVDataTable();

			try
			{
				Connection_Open();

				using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
				{
					using (var command = this.Connection.CreateCommand())
					{

						adapter.SelectCommand = command;
						adapter.SelectCommand.Connection = this.Connection;
						adapter.SelectCommand.CommandText = @"SELECT * " +
							" FROM CZMST_SI_BV WHERE CountEntries = @CountEntries AND NMBRPAL = @NMBRPAL ";
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = NMBRPAL == null ? (object)DBNull.Value : NMBRPAL });
						adapter.SelectCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries  });

						int ReturnValue;
						ReturnValue = adapter.Fill(dt);

						if (dt != null && dt.Count > 0)
						{
							return true;
						}
						else
							return false;
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

		public void Save_SI_BV(int CountEntries, string NMBRPAL,int USER_ID, decimal dimensionSirka, decimal dimensionVyska, decimal dimensionHloubka, decimal gross_weight_decimal)
		{
			try
			{
				if (GetDataByNMBRPAL_CountEntries_SI_BV_Exist(CountEntries, NMBRPAL))
				{
					Update_SI_BV(CountEntries, NMBRPAL, dimensionSirka, dimensionVyska, dimensionHloubka, gross_weight_decimal);
				}
				else
				{
					Insert_SI_BV(CountEntries,NMBRPAL, USER_ID, dimensionSirka, dimensionVyska, dimensionHloubka, gross_weight_decimal);
				}
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
			}
		}

		public int Insert_SI_BV(int CountEntries, string NMBRPAL,int USER_ID, decimal dimensionSirka, decimal dimensionVyska, decimal dimensionHloubka, decimal gross_weight_decimal)
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
						adapter.InsertCommand.CommandText = @"INSERT INTO CZMST_SI_BV (CountEntries, NMBRPAL,USER_ID, pal_W, pal_H, pal_D, pal_WEIGHT) VALUES (@CountEntries, @NMBRPAL, @USER_ID, @pal_W, @pal_H, @pal_D, @pal_WEIGHT)";
						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = NMBRPAL });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@USER_ID", DbType = System.Data.DbType.Int32, Value = USER_ID });

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@pal_W", DbType = System.Data.DbType.Decimal, Value = dimensionSirka });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@pal_H", DbType = System.Data.DbType.Decimal, Value = dimensionVyska });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@pal_D", DbType = System.Data.DbType.Decimal, Value = dimensionHloubka });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@pal_WEIGHT", DbType = System.Data.DbType.Decimal, Value = gross_weight_decimal });

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

		public int Update_SI_BV(int CountEntries, string NMBRPAL, decimal dimensionSirka, decimal dimensionVyska, decimal dimensionHloubka, decimal gross_weight_decimal)
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
						adapter.InsertCommand.CommandText = @"UPDATE CZMST_SI_BV SET pal_W = @pal_W, pal_H = @pal_H, pal_D = @pal_D, pal_WEIGHT = @pal_WEIGHT) WHERE CountEntries = @CountEntries AND NMBRPAL = @NMBRPAL";
						adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries , SourceVersion = DataRowVersion.Original});
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = NMBRPAL, SourceVersion = DataRowVersion.Original });

						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@pal_W", DbType = System.Data.DbType.Decimal, Value = dimensionSirka });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@pal_H", DbType = System.Data.DbType.Decimal, Value = dimensionVyska });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@pal_D", DbType = System.Data.DbType.Decimal, Value = dimensionHloubka });
						adapter.InsertCommand.Parameters.Add(new SQLiteParameter() { ParameterName = "@pal_WEIGHT", DbType = System.Data.DbType.Decimal, Value = gross_weight_decimal });

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

		#endregion

		#region Obecne slozitejsi konstrukce

		#region Reseni pro nactena data v si
		//volat Nacteno
		//pokud se vola nekolikrat, tak closeConnection=false, ale musi se nakonec zavolat NactenoFinalize, ktere uzavre connection a disposne command
		//=> nakonec asi nejlepe vzdy zavolat NactenoFinalize ... 
		private System.Data.SQLite.SQLiteCommand scecommandNacteno = null;
		/// <summary>
		/// Finalizacni metoda pro ukonceni spojeni a commandu pro metodu Nacteno
		/// </summary>
		public void NactenoFinalize()
		{
			if (scecommandNacteno != null)
			{
				if ((scecommandNacteno.Connection.State & ConnectionState.Open) == ConnectionState.Open)
				{
					scecommandNacteno.Connection.Close();
					scecommandNacteno.Connection.Dispose();
				}

				try { scecommandNacteno.Dispose(); }
				catch { }
				scecommandNacteno = null;
			}
		}
		/// <summary>
		/// Pocet nactenych polozek vypocteny z databaze
		/// </summary>
		/// <param name="sqlcedbfilename"></param>
		/// <param name="ITEMNMBR"></param>
		/// <param name="SOPNUMBE"></param>
		/// <param name="ORD"></param>
		/// <returns></returns>
		public decimal Nacteno(string ITEMNMBR, string SOPNUMBE, int ORD, bool closeConnection)
		{
			if (scecommandNacteno == null)
			{
				scecommandNacteno = new System.Data.SQLite.SQLiteCommand();
				scecommandNacteno.Connection = new System.Data.SQLite.SQLiteConnection(this.Connection.ConnectionString); // pouzije se aktualni
				scecommandNacteno.CommandText =
					"Select sum(qtyshppd) from czmst_si where Itemnmbr=@itemnmbr and SOPNUMBE=@sopnumbe AND ORD=@ord";

				scecommandNacteno.Parameters.AddWithValue("@itemnmbr", ITEMNMBR);
				scecommandNacteno.Parameters.AddWithValue("@sopnumbe", SOPNUMBE);
				scecommandNacteno.Parameters.AddWithValue("@ord", ORD);
			}

			scecommandNacteno.Parameters["@itemnmbr"].Value = ITEMNMBR;
			scecommandNacteno.Parameters["@sopnumbe"].Value = SOPNUMBE;
			scecommandNacteno.Parameters["@ord"].Value = ORD;

			////object res = vydejData.CZMST_SI.Compute("SUM(QTYSHPPD)", "ITEMNMBR='" + ITEMNMBR + "' AND SOPNUMBE='" + SOPNUMBE + "' AND ORD='" + ORD + "'");
			object res = null;
			try
			{
				if (scecommandNacteno.Connection.State == ConnectionState.Closed)
					scecommandNacteno.Connection.Open();

				res = scecommandNacteno.ExecuteScalar();
			}
			catch (Exception exNacteno)
			{
				Logging.ExceptionHandler2.Handle(exNacteno);
			}
			finally
			{
				if (closeConnection)
				{
					if (((scecommandNacteno.Connection.State & ConnectionState.Open) == ConnectionState.Open))
						scecommandNacteno.Connection.Close();

					scecommandNacteno.Dispose();
					scecommandNacteno = null;
				}
			}

			decimal nacteno2 = 0;
			try
			{
				if (res is System.DBNull)
					return nacteno2;
				else
					nacteno2 = Convert.ToDecimal(res);
			}
			catch { }
			return nacteno2;

		}
		#endregion

		#region Slucovani

		/// <summary>
		/// Smaze polozky davky ze sloucene
		/// </summary>
		/// <param name="filenameSloucenaDavka">plna cesta ke sloucene vydejove davce</param>
		/// <param name="countEntries2Delete">cislo davky ve sloucene, ktere se ma smazat</param>
		/// <returns>pocet smazanych zaznamu</returns>
		public int Sloucena_Smazat_Davku(string filenameSloucenaDavka, string countEntries2Delete)
		{
			// TODO : a co ostatni tabulky ? SI, ... ?
			//SqlCEDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter setaSource = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter();
			try
			{
				//using (var connection = new SQLiteConnection())
				//{
				//    connection.ConnectionString = Main.SQLiteConnectionStringFormat(filenameSloucenaDavka);

				//    setaSource.Connection = connection;

				//    return setaSource.DeleteQuery(int.Parse(countEntries2Delete));
				//}

				return this.DeleteQuery_SE(int.Parse(countEntries2Delete));

			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return 0;
			}
			finally
			{
				//setaSource.Dispose();
			}
		}

		public void ImportSEDataByCountEntriesFromTo(string sourceFilePath, string destinationFilePath, int davka)
		{
			#region OLD
			//SqlCEDBs.DataSets.Vydej.CZMST_SEDataTable sedt = null;
			//using (Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter seta = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SETableAdapter())
			//{
			//    using (var connectionSource = new System.Data.SQLite.SQLiteConnection("Data source=" + sourceFilePath))
			//    {
			//        seta.Connection = connectionSource;
			//        sedt = seta.GetDataByCountEntries(davka);

			//        if (sedt != null)
			//        {
			//            foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow row in sedt)
			//                row.SetAdded();
			//        }
			//    }

			//    using (var connectionDestination = new System.Data.SQLite.SQLiteConnection("Data source=" + destinationFilePath))
			//    {
			//        seta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + destinationFilePath);
			//        seta.Connection.Open();
			//        var transaction = seta.Connection.BeginTransaction();
			//        try
			//        {
			//            seta.Update(sedt);
			//            transaction.Commit();
			//        }
			//        catch (Exception exTranCommit)
			//        {
			//            Logging.Log.Write(exTranCommit);
			//            try { transaction.Rollback(); }
			//            catch { }
			//        }
			//        seta.Connection.Close();
			//    }
			//} 
			#endregion

			#region novy kod

			using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej ConVyr_Source = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej(sourceFilePath))
			using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej ConVyr_Destiny = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej(destinationFilePath))
			{
				System.Data.SQLite.SQLiteTransaction Trans = null;
				try
				{
					Trans = ConVyr_Destiny.Connection.BeginTransaction();

					#region SE

					var sedt = ConVyr_Source.GetDataByCountEntries_SE(davka);

					if (sedt != null)
					{
						foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow row in sedt)
							row.SetAdded();
					}

					ConVyr_Destiny.Update_SE(sedt);
					Trans.Commit();


					#endregion

					//#region SI

					//var sidt = ConVyr_Source.GetDataByCountEntries_SI(davka);

					//if (sidt != null)
					//{
					//    foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow row in sidt)
					//        row.SetAdded();
					//}


					//ConVyr_Destiny.Update_SI(sidt);
					//Trans.Commit();

					//#endregion

					//#region Param

					//var paramdt = ConVyr_Source.GetData_Param();

					//if (paramdt != null)
					//{
					//    foreach (Fask.SQLiteDBs.DataSets.Vydej.ParametryRow row in paramdt)
					//        row.SetAdded();
					//}

					//ConVyr_Destiny.Update_Param(sedt);
					//Trans.Commit();

					//#endregion

				}
				catch (Exception exTranCommit)
				{
					Logging.ExceptionHandler2.Handle(exTranCommit);
					try { Trans.Rollback(); }
					catch { }
				}
			}
			#endregion

		}

		public void ImportSIDataByCountEntriesFromTo(string sourceFilePath, string destinationFilePath, int davka)
		{
			#region OLD kod
			//SqlCEDBs.DataSets.Vydej.CZMST_SIDataTable sedt = null;

			//using (Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter sita = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.CZMST_SITableAdapter())
			//{
			//    using (var connectionSource = new System.Data.SQLite.SQLiteConnection("Data source=" + sourceFilePath))
			//    {
			//        sita.Connection = connectionSource;

			//        sedt = sita.GetDataByCountEntries(davka);

			//        if (sedt != null)
			//        {
			//            foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow row in sedt)
			//                row.SetAdded();
			//        }
			//    }

			//    using (var connectionDestination = new System.Data.SQLite.SQLiteConnection("Data source=" + destinationFilePath))
			//    {
			//        sita.Connection = connectionDestination;
			//        sita.Connection.Open();
			//        var transaction = sita.Connection.BeginTransaction();
			//        try
			//        {
			//            sita.Update(sedt);
			//            transaction.Commit();
			//        }
			//        catch (Exception exTranCommit)
			//        {
			//            Logging.Log.Write(exTranCommit);
			//            try { transaction.Rollback(); }
			//            catch { }
			//        }
			//        sita.Connection.Close();
			//    }
			//} 
			#endregion

			#region novy kod

			using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej ConVyr_Source = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej(sourceFilePath))
			using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej ConVyr_Destiny = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej(destinationFilePath))
			{
				System.Data.SQLite.SQLiteTransaction Trans = null;
				try
				{
					Trans = ConVyr_Destiny.Connection.BeginTransaction();

					//#region SE

					//var sedt = ConVyr_Source.GetDataByCountEntries_SE(davka);

					//if (sedt != null)
					//{
					//    foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow row in sedt)
					//        row.SetAdded();
					//}

					//ConVyr_Destiny.Update_SE(sedt);
					//Trans.Commit();


					//#endregion

					#region SI

					var sidt = ConVyr_Source.GetDataByCountEntries_SI(davka);

					if (sidt != null)
					{
						foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow row in sidt)
							row.SetAdded();
					}


					ConVyr_Destiny.Update_SI(sidt);
					Trans.Commit();

					#endregion

					//#region Param

					//var paramdt = ConVyr_Source.GetData_Param();

					//if (paramdt != null)
					//{
					//    foreach (Fask.SQLiteDBs.DataSets.Vydej.ParametryRow row in paramdt)
					//        row.SetAdded();
					//}

					//ConVyr_Destiny.Update_Param(sedt);
					//Trans.Commit();

					//#endregion

				}
				catch (Exception exTranCommit)
				{
					Logging.ExceptionHandler2.Handle(exTranCommit);
					try { Trans.Rollback(); }
					catch { }
				}
			}
			#endregion
		}

		public void ImportParametryDataFromTo(string sourceFilePath, string destinationFilePath)
		{
			#region old
			//SqlCEDBs.DataSets.Vydej.ParametryDataTable sedt = null;

			//using (Fask.SQLiteDBs.DataSets.VydejTableAdapters.ParametryTableAdapter seta = new Fask.SQLiteDBs.DataSets.VydejTableAdapters.ParametryTableAdapter())
			//{
			//    using (var connectionSource = new System.Data.SQLite.SQLiteConnection("Data source=" + sourceFilePath))
			//    {
			//        seta.Connection = connectionSource;

			//        sedt = seta.GetData();

			//        if (sedt != null)
			//        {
			//            foreach (Fask.SQLiteDBs.DataSets.Vydej.ParametryRow row in sedt)
			//                row.SetAdded();
			//        }
			//    }

			//    using (var connectionDestination = new System.Data.SQLite.SQLiteConnection("Data source=" + destinationFilePath))
			//    {
			//        seta.Connection = connectionDestination;
			//        seta.Connection.Open();
			//        var transaction = seta.Connection.BeginTransaction();
			//        try
			//        {
			//            seta.Update(sedt);
			//            transaction.Commit();
			//        }
			//        catch (Exception exTranCommit)
			//        {
			//            Logging.Log.Write(exTranCommit);
			//            try { transaction.Rollback(); }
			//            catch { }
			//        }
			//        seta.Connection.Close();
			//    }
			//} 
			#endregion

			#region novy kod

			using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej ConVyr_Source = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej(sourceFilePath))
			using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej ConVyr_Destiny = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Vydej(destinationFilePath))
			{
				System.Data.SQLite.SQLiteTransaction Trans = null;
				try
				{
					Trans = ConVyr_Destiny.Connection.BeginTransaction();

					//#region SE

					//var sedt = ConVyr_Source.GetDataByCountEntries_SE(davka);

					//if (sedt != null)
					//{
					//    foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SERow row in sedt)
					//        row.SetAdded();
					//}

					//ConVyr_Destiny.Update_SE(sedt);
					//Trans.Commit();


					//#endregion

					//#region SI

					//var sidt = ConVyr_Source.GetDataByCountEntries_SI(davka);

					//if (sidt != null)
					//{
					//    foreach (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow row in sidt)
					//        row.SetAdded();
					//}


					//ConVyr_Destiny.Update_SI(sidt);
					//Trans.Commit();

					//#endregion

					#region Param

					var paramdt = ConVyr_Source.GetData_Param();

					if (paramdt != null)
					{
						foreach (Fask.SQLiteDBs.DataSets.Vydej.ParametryRow row in paramdt)
							row.SetAdded();
					}

					ConVyr_Destiny.Update_Param(paramdt);
					Trans.Commit();

					#endregion

				}
				catch (Exception exTranCommit)
				{
					Logging.ExceptionHandler2.Handle(exTranCommit);
					try { Trans.Rollback(); }
					catch { }
				}
			}
			#endregion

		}

		#endregion

		#region Celkove pocty

		/// <summary>
		/// Vrati celkovy pocet zaznamu v SE a SI dohromady
		/// </summary>
		/// <returns>celkovy pocet zaznamu v SE a SI dohromady, pokud chyba tak null</returns>
		public int? CelkemPolozek_SE_A_SI()
		{
			try
			{
				this.Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "Select count(*) from czmst_se";
					int countSE = Convert.ToInt32(command.ExecuteScalar());

					command.CommandText = "Select count(*) from czmst_si";
					int countSI = Convert.ToInt32(command.ExecuteScalar());

					return countSE + countSI;
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
			finally
			{
				this.Connection_Close();
			}
		}

		/// <summary>
		/// Smazani dat vystupu SI a vlozeni novych dat z tabulky, ktere jsou Added
		/// </summary>
		/// <param name="dtSI">data pro vlozeni ve stavu Added</param>
		public void CZMST_SI_DeleteAndUpdate(Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable dtSI)
		{
			IDbTransaction transaction = null;
			try
			{
				this.Connection_Open();

				transaction = this.Connection.BeginTransaction();

				this.DeleteQuery_SI();
				this.Update_SI(dtSI);

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

		/// <summary>
		/// Nacte seznam objednavek z vystupu SI davky 
		/// </summary>
		/// <returns>List cisel dokladu (sopnumbe)</returns>
		public List<string> SI_Sopnumbe_List()
		{
			List<string> objednavkyList = new List<string>();

			try
			{
				this.Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					//"SELECT DISTINCT si.SOPNUMBE, se.CZ_REZ1_TRACK AS rez1 " +
					//"FROM CZMST_SI AS si " +
					//"JOIN CZMST_SE AS se ON se.SOPNUMBE = si.SOPNUMBE",
					command.CommandText = "SELECT distinct SOPNUMBE FROM CZMST_SI";
					//"JOIN CZMST_SE AS se ON se.SOPNUMBE = si.SOPNUMBE",

					using (var scereader = command.ExecuteReader())
					{
						while (scereader.Read())
						{
							object sopnumbeObject = scereader["SOPNUMBE"];
							if (sopnumbeObject is string) // ??? null is string ??? <= provest test
							{
								string sopnume = (sopnumbeObject as string).Trim();
								if (!objednavkyList.Contains(sopnume))
									objednavkyList.Add(sopnume);
							}
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
			}

			return objednavkyList;
		}

		/// <summary>
		/// Provede aktualizaci hodnoty Rez1 v SI dle zadaneho parametru Sopnumbe
		/// </summary>
		/// <param name="rez1_value">hodnota rez1</param>
		/// <param name="sopnumbe_filter">filter sopnumbe</param>
		public int SI_Update_Rez1_For_Sopnumbe(string REZ_1, string SOPNUMBE)
		{
			try
			{
				this.Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					command.CommandText = "Update czmst_si set REZ_1=@REZ_1 where SOPNUMBE=@SOPNUMBE";
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@REZ_1", DbType = System.Data.DbType.String, Value = REZ_1 == null ? (object)DBNull.Value : REZ_1 });
					command.Parameters.Add(new SQLiteParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
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


		#endregion

		#endregion

		#region Stav vydeje

		/// <summary>
		/// Zjisti stav vydeje
		/// </summary>
		/// <param name="nasnimat">Pocet kusu celkem k nasnimani</param>
		/// <param name="nasnimano">Pocet kusu celkem nasnimano</param>
		/// <param name="pol_nasnimat">Pocet polozek k nasnimani</param>
		/// <param name="pol_nasnimano">Pocet polozek nasnimano</param>
		/// <returns>True: pokud je vydej dokoncen, False: pokud ne</returns>
		public bool stavVydeje(out decimal nasnimat, out decimal nasnimano, out int pol_nasnimat, out int pol_nasnimano)
		{
			// klicem je (SOPNUMBE, ITEMNMBR, ORD) 
			// zajima mne pak pocet polozek a celkove mnozstvi na polozku
			nasnimat = 0;       // mnozstvti polozek predlohy k vydeji
			nasnimano = 0;      // mnozstvi polozek nasnimanych vydano
			pol_nasnimat = 0;    // pocet polozek predlohy k vydeji
			pol_nasnimano = 0;  // pocet polozek nasnimanych vadno

			//foreach (Vydej_3.ListPolozekVydej.ListPolozekRow lprow in listPolozekVydej.ListPolozek)
			//{
			//    nasnimat += lprow.Mnozstvo;
			//    pol_nasnimat++;

			//    nasnimano += lprow.Mnozstvo - lprow.Ostava;
			//    if (lprow.Ostava <= 0)
			//        pol_nasnimano++;
			//}


			DataSet ds = new DataSet();
			try
			{
				this.Connection_Open();

				using (var command = this.Connection.CreateCommand())
				{
					//select DISTINCT se.SOPNUMBE, se.ITEMNMBR, se.ORD, se.QTYSHPPD, si.QTYSHPPD2
					//from CZMST_SE se
					//left join 
					//(
					//    select SOPNUMBE, ITEMNMBR, ORD, sum(QTYSHPPD) as QTYSHPPD2
					//    from CZMST_SI
					//    group by SOPNUMBE, ITEMNMBR, ORD
					//) as si on se.SOPNUMBE=si.SOPNUMBE and se.ITEMNMBR=si.ITEMNMBR and se.ORD=si.ORD
					command.CommandText =
						@"select DISTINCT se.SOPNUMBE, se.ITEMNMBR, se.ORD, se.QTYSHPPD, coalesce(si.QTYSHPPD2, 0.0) as QTYSHPPD2
						from CZMST_SE se
						left join 
						(
							select SOPNUMBE, ITEMNMBR, ORD, sum(QTYSHPPD) as QTYSHPPD2
							from CZMST_SI
							group by SOPNUMBE, ITEMNMBR, ORD
						) as si on se.SOPNUMBE=si.SOPNUMBE and se.ITEMNMBR=si.ITEMNMBR and se.ORD=si.ORD";
					using (var reader = command.ExecuteReader())
					{
						ds.Tables.Add().Load(reader);
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
			}

			var dt = ds.Tables[0];

			decimal nasnimat_tmp = 0;
			decimal nasnimano_tmp = 0;
			//decimal zbyva_tmp = 0;
			foreach (DataRow row in dt.Rows)
			{
				pol_nasnimat++;
				nasnimat_tmp = Convert.ToDecimal(row["QTYSHPPD"]);
				nasnimat += nasnimat_tmp;

				nasnimano_tmp = Convert.ToDecimal(row["QTYSHPPD2"]);
				nasnimano += nasnimano_tmp;

				if ((nasnimat_tmp - nasnimano_tmp) <= 0)
					pol_nasnimano++;
			}

			//if (pol_nasnimat == pol_nasnimano)
			//    return true;
			//else
			//    return false;
			return pol_nasnimat == pol_nasnimano;
		}

		#endregion



	}
}