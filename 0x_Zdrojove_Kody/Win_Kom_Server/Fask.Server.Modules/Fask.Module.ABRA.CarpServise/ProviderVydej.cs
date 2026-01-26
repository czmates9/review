using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using System.IO;
using Fask.Logging;
using System.Data.Common;

namespace Fask.Module.ABRA.CarpServise
{
    public partial class Provider : Fask.Server.Interfaces.Vydej.IVydej
    {

        private string TABLE_CZMST_SE = "CZMST_SE";
        private string TABLE_CZMST_SI = "CZMST_SI";
        private string TABLE_CZMST_SE_SN = "CZMST_SE_SN";
        private string TABLE_CZMST_SIH = "CZMST_SIH";


		#region IVydej Members Implementovane

		public Fask.Server.Interfaces.Classes.StatusInfo Vydej_GenerateDavka( Fask.Server.Interfaces.Classes.Objednavka objednavka, Fask.Server.Interfaces.Classes.Sklad sklad)
		{

			StatusInfo si = new StatusInfo();
			si.Description = "Vydej_GenerateDavka start";
			si.ID = 0;

			// 1) test na rozpracovany doklad 

			byte? czdoslo = Database.Vydej.CZMSTSE_SOPNUMBER_CZDOSLO(objednavka.ID);

			if (czdoslo.HasValue)
			{ //existuje a je stazene v terminalu => vrati chybu ... nelze vytvorit duplicitu ...
				if (czdoslo.Value > 0)
				{
					si.ID = -1;
					si.Description = string.Format("Existuje rozpracovaná dávka pro doklad '{0}' na terminálu č.:{1}", objednavka.ID, czdoslo.Value); //"Existuje rozpracovaná dávka pro doklad '" + objednavka.ID + "' na terminálu č.:" + czdoslo.Value;
					si.InnerException = new Exception(si.Description);
					Logging.ExceptionHandler2.Handle(LogLevel.Warn,si.Description);
					return si;
				}
				else if (czdoslo.Value == 0) //je pripravena ke zpracovani => uzavrit ... 
				{
					Database.Vydej.CZMSTSE_UPDATE_CZDOSLO(objednavka.ID);
				}
			}


			// 2) test zda je to vydejka
			//if (Database.ABRA.Vydejka_Exists(objednavka.ID))
			//{
			//    string stav = this.ExportVydejka_ABRA_Z_Vydejky(objednavka, sklad);

			//    if (stav != "OK")
			//    {
			//        si.ID = -10;
			//        si.Description = stav;
			//        si.InnerException = new Exception(si.Description);
			//        return si;
			//    }
			//    else
			//    {
			//        si.ID = int.Parse(objednavka.CisloDavky);
			//        si.Description = "OK";
			//        si.InnerException = null;
			//    }
			//}
			//Zda je to Objednavka Pridata
			if (Database.ABRA.ObjednavkaPrijata_Exists(objednavka.ID))
			{
				string stav = this.ExportVydejka_ABRA_Z_ObjednavkaPrijata(objednavka, sklad);

				if (stav != "OK")
				{
					si.ID = -10;
					si.Description = stav;
					si.InnerException = new Exception(si.Description);
					return si;
				}
				else
				{
					si.ID = int.Parse(objednavka.CisloDavky);
					si.Description = "OK";
					si.InnerException = null;
				}
			}
			// 2) nepodporovany typ dokladu
			else
			{
				si.Description = "Doklad '" + objednavka.ID + "' nenalezen.";
				if (sklad != null)
					si.Description += "\nSklad " + sklad.ID;
				si.InnerException = new Exception(si.Description);
				si.ID = -10;
				throw new Exception(si.Description);
			}

			return si;
		}

		public Fask.DataSets.Vydejky Vydej_GetVydejky(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item, Fask.Server.Interfaces.Classes.User user)
		{

			string select =
				" SELECT A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO " +
				//" SELECT distinct A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO " +
				" FROM CZMST_SE A" +
				" INNER JOIN " +
				" ( " +
				"	SELECT X.COUNTENTRIES, X.SOPNUMBE, COUNT(X.ITEMNMBR) CNTITEMS " +
				"	FROM ( " +
				"		SELECT COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
				"		FROM CZMST_SE " +
				"       WHERE QTYPACK=0" +
				"       AND " +
				"       SKL_ID LIKE '" + sklad.ID + "%' " +
				"       AND " +
				"       ITEMTYPE LIKE '" + item.Type + "%' " +
				"       AND " +
				"       (CZ_DOSLO<=0 OR CZ_DOSLO=" + terminal.ID + ")" +
				"		GROUP BY COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
				"	) X " +
				"	GROUP BY X.COUNTENTRIES, X.SOPNUMBE " +
				" ) B ON  " +
				" B.COUNTENTRIES=A.COUNTENTRIES " +
				" AND B.SOPNUMBE=A.SOPNUMBE " +
				" INNER JOIN " +
				" ( " +
				"	SELECT COUNTENTRIES, SOPNUMBE, " +
				"       SUM(QTYSHPPD) AS QTYSHPPDSUM" +
				"	FROM CZMST_SE " +
				"   WHERE QTYPACK=0" +
				"   AND " +
				"   SKL_ID LIKE '" + sklad.ID + "%' " +
				"   AND " +
				"   ITEMTYPE LIKE '" + item.Type + "%' " +
				"   AND " +
				"   (CZ_DOSLO<=0 OR CZ_DOSLO=" + terminal.ID + ")" +
				"	GROUP BY COUNTENTRIES, SOPNUMBE " +
				" ) C ON " +
				" C.COUNTENTRIES=A.COUNTENTRIES  " +
				" AND C.SOPNUMBE=A.SOPNUMBE " +
				//15.2.2011, JiS - rozsireni o dotazeni stavu rozpracovanosti vydejky
				" LEFT OUTER JOIN " +
				" ( " +
				"   SELECT COUNTENTRIES, SOPNUMBE, COUNT(*) AS ROZPRACOVANO FROM CZMST_SI" +
				"   GROUP BY COUNTENTRIES, SOPNUMBE " +
				" ) D ON " +
				" D.COUNTENTRIES=A.COUNTENTRIES " +
				" and D.SOPNUMBE=A.SOPNUMBE " +
				//konec rozpracovanosti vydejky
				" WHERE " +
				//" A.CountEntries not in (Select CountEntries from " + TABLE_CZMST_SI + ") " +
				//" and " +
				//" A.CZ_Doslo<=0 " + 
				" (A.CZ_DOSLO<=0 OR A.CZ_DOSLO=" + terminal.ID + ")" +
				" AND " +
				" A.SKL_ID LIKE '" + sklad.ID + "%' " +
				" AND " +
				" A.ITEMTYPE LIKE '" + item.Type + "%' " +
				" GROUP BY A.PRIORITY, A.COUNTENTRIES, A.SOPNUMBE, B.CNTITEMS, C.QTYSHPPDSUM, D.ROZPRACOVANO " +
				""; 

			System.Data.SqlClient.SqlDataAdapter xda = new System.Data.SqlClient.SqlDataAdapter(select, (new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.SqlProviderConnection)));

			Fask.DataSets.Vydejky volnevydejky = new Fask.DataSets.Vydejky();
			((System.Data.Common.DbDataAdapter)xda).Fill(volnevydejky, volnevydejky.Hlavicky.TableName);


			volnevydejky.AcceptChanges();

			return volnevydejky;
		}

		public Fask.DataSets.Vydej Vydej_GetVydejka(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
		{

			string select = string.Empty;
			if (Properties.Settings.Default.VydejPokracovatNaJinemTerminalu)
			{
				select = "SELECT * FROM CZMST_SE " +
				" where CountEntries=" + davka.ID.Value.ToString() +
					//" and ITEMTYPE like '" + SQLInjection.Filter(itemtype) + "%' " +
				" and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
				" order by dex_row_id" +
				"";
			}
			else
			{
				select = "SELECT * FROM CZMST_SE " +
				" where CountEntries=" + davka.ID.Value.ToString() +
				" and ITEMTYPE like '" + item.Type + "%' " +
				" and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
				" order by dex_row_id" +
				"";
			}


			System.Data.SqlClient.SqlDataAdapter xda = new System.Data.SqlClient.SqlDataAdapter(select, (new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.SqlProviderConnection)));

			Fask.DataSets.Vydej vydej = new Fask.DataSets.Vydej();

			((System.Data.Common.DbDataAdapter)xda).Fill(vydej, vydej.CZMST_SE.TableName);
			foreach (Fask.DataSets.Vydej.CZMST_SERow sr in vydej.CZMST_SE)
			{
				sr.CZ_Doslo = (byte)terminal.ID;
			}

			// Dotazeni dat z SI => pouze v pripade, ze se nepouziva vychystavani vice terminaly!!!
			// Slouzi k poslani davky k pozdejsimu zpracovani na jinem terminalu ...
			if (item.Type.Trim().Length == 0)
			{
				string selectSI =
					"SELECT * FROM CZMST_SI " +
					" where CountEntries=" + davka.ID.Value.ToString() +
					//" and ITEMTYPE like '" + SQLInjection.Filter(itemtype) + "%' " +
					"";
				System.Data.SqlClient.SqlDataAdapter xdaSI = new System.Data.SqlClient.SqlDataAdapter(selectSI, (new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.SqlProviderConnection)));
				((System.Data.Common.DbDataAdapter)xdaSI).Fill(vydej, vydej.CZMST_SI.TableName);
			}

			//Dotazeni predlohy seriovych cisel
			try
			{
				string selectSESN =
					"Select * from CZMST_SE_SN " +
					" where CountEntries=" + davka.ID.Value.ToString();

				SqlDataAdapter xdaSESN = new SqlDataAdapter(selectSESN, (new SqlConnection(Properties.Settings.Default.SqlProviderConnection)));
				((DbDataAdapter)xdaSESN).Fill(vydej, vydej.CZMST_SE_SN.TableName);

			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Vydej", "GetVydejka.PredlohaSN", ex );
			}

			return vydej;

		}

		public bool Vydej_GetVydejkaReceived(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
		{
			string selectCount = string.Empty;
			string update = string.Empty;
			if (Properties.Settings.Default.VydejPokracovatNaJinemTerminalu)
			{
				selectCount =
					"SELECT Count(CountEntries) as davka FROM CZMST_SE " +
					" where CountEntries=" + davka.ID.Value.ToString() +
					" AND (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
					"";

				update =
					"Update CZMST_SE " +
					" set cz_doslo=" + terminal.ID.ToString() +
					" where countentries=" + davka.ID.Value.ToString() +
					" and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
					"";
			}
			else
			{
				selectCount =
					"SELECT Count(CountEntries) as davka FROM CZMST_SE " +
					" where CountEntries=" + davka.ID.Value.ToString() +
					" and ITEMTYPE like '" + item.Type + "%' " +
					" AND (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
					"";

				update =
					"Update CZMST_SE " +
					" set cz_doslo=" + terminal.ID.ToString() +
					" where countentries=" + davka.ID.Value.ToString() +
					" and ITEMTYPE like '" + item.Type + "%' " +
					" and (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
					"";
			}

			SqlConnection xconn = new SqlConnection(Properties.Settings.Default.SqlProviderConnection);
			SqlCommand xcommAllowed = new SqlCommand(selectCount, xconn);
			SqlCommand xcomm = new SqlCommand(update, xconn);
			IDbTransaction itrans = null;

			int res = 0;
			try
			{
				xconn.Open();
				itrans = xconn.BeginTransaction(IsolationLevel.Serializable);

				xcommAllowed.Transaction = (SqlTransaction)itrans;
				object r = xcommAllowed.ExecuteScalar();
				if (r == null)
					throw new Exception("Dávka nenalezena.");
				if (r is int && ((int)r) <= 0)
					throw new Exception("Dávka se již zpracovává.");

				xcomm.Transaction = (SqlTransaction)itrans;
				res = xcomm.ExecuteNonQuery();
				if (itrans != null)
					itrans.Commit();

			}
			catch (Exception ex)
			{
				if (itrans != null)
					itrans.Rollback();

				Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error ," (Dávka: " + davka.ID.Value.ToString() + ", Terminál ID:" + terminal.ID.ToString() + ", ITEMTYPE:" + item.Type + ")");
				Logging.ExceptionHandler2.Handle(ex);
				//throw ex;
				return false;
			}
			finally
			{
				if (xconn.State == ConnectionState.Open)
					xconn.Close();
			}

			return (res > 0);
		}

		public DataSet Vydej_Detail(Objednavka objednavka)
		{
			return null;
		}

		public DataSet Vydej_DetailDavka(Davka davka)
		{
			return null;
		}
		
		public bool Vydej_AfterProcessedAction(Davka davka)
		{
			return true;
		}

		public StatusObject Vydej_Finish_Vydejka(Davka davka, Terminal terminal, string password)
		{
			StatusObject so = new StatusObject();
			so.StatusText = "";

			try
			{
				Database.Vydej.UpdateCzDosloByCountEntries((byte)(terminal.ID + 100), davka.ID.Value);
				so.SetOK();
			}
			catch (Exception e)
			{
				so.Exception = true;
				so.StatusText = e.Message;
			}

			return so;
		}

		public StatusObject Vydej_Storno_Vydejka(Davka davka, Terminal terminal, string password)
		{
			StatusObject so = new StatusObject();

			if (password.Trim() != Properties.Settings.Default.HesloStornoVydejka.Trim())
			{
				so.StatusText = "Zadané heslo je špatně!";
				so.Exception = true;
				return so;
			}

			SQL_Datasets.VydejTableAdapters.CZMST_SITableAdapter si_ta = null;
			SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter se_ta = null;

			try
			{
				si_ta = new SQL_Datasets.VydejTableAdapters.CZMST_SITableAdapter();
				si_ta.Connection = new SqlConnection(Properties.Settings.Default.SqlProviderConnection);

				if (Database.Vydej.GetDataByCountEntries(davka.ID.Value).Count > 0)
				{
					so.StatusText = "Danou dávku nelze zrušit! Existují nasnímané položky!";
					so.Exception = true;
					return so;
				}


				Database.Vydej.UpdateCzDosloByCountEntries(100, davka.ID.Value);

				so.StatusText = "OK";
				return so;

			}
			finally
			{
				if (si_ta != null && si_ta.Connection.State == System.Data.ConnectionState.Open)
					si_ta.Connection.Close();


				if (se_ta != null && se_ta.Connection.State == System.Data.ConnectionState.Open)
					se_ta.Connection.Close();
			}
		}


		public Fask.Server.Interfaces.Classes.StatusObject Vydej_Process(
	Fask.Server.Interfaces.Classes.Davka davka,
	Fask.Server.Interfaces.Classes.Terminal terminal,
	Fask.Server.Interfaces.Classes.Sklad sklad,
	Fask.Server.Interfaces.Classes.Item item,
	Fask.DataSets.Vydej vydejdata,
	Fask.Server.Interfaces.Vydej.ProcessState processVydejState)
		{

			string guidDavka = string.Empty;
			if (vydejdata.CZMST_SEH.Count > 0)
				guidDavka = vydejdata.CZMST_SEH[0].GUID.ToString();
			else
				guidDavka = Guid.NewGuid().ToString();
			string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Properties.Settings.Default.StatusObjectsDirectory, guidDavka));

			//StatusObject so = new StatusObject(filePath);
			StatusObject so = StatusObject.Load(filePath);
			if (so == null) //neexistuje => vytvorit a pokracovat
			{
				so = new StatusObject(filePath);
			}
			else if (so.Exists)
			{
				//return so; //pokud existuje, tak vraci informaci terminalu, at se rozhodne co s tim chce delat ...
				////pokud se chce pokracovat znovu, tak se musi nejprve smazat

				if (so.Exception) // || so.Finished)
				{ // nastala vyjimka pri zpracovani => umozni nasledne volat znovu...
					so.Delete();
					return so; // Po prvnim volani vrati informaci o chybe, pri druhem volani uz to pusti dal, protoze neexistuje ...
				}
				else
				{ // probiha nebo bylo dokonceno uspesne => neumozni znovu zpracovat...
					return so; // pokud neni ukoncen nebo neni chyba, tak jeste probiha a neni mozne znovu generovat ...
				}
			}

			so.StatusText = "Výdej zpracování ...";

			SqlTransaction trans = null;
			SqlConnection conn = null;

			if (vydejdata == null)
				return so;

			vydejdata.AcceptChanges();

			try
			{
				conn = new SqlConnection(Properties.Settings.Default.SqlProviderConnection);
				conn.Open();
				trans = conn.BeginTransaction();

				if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.Uvolnit) //uvolnit davku
				{
					foreach (Fask.DataSets.Vydej.CZMST_SERow serow in vydejdata.CZMST_SE.Rows)
					{
						if (serow.RowState == DataRowState.Modified || serow.RowState == DataRowState.Unchanged)
						{
							serow["CZ_Doslo"] = 0;
						}
					}

					SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter seta = new SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter();
					seta.Connection = conn;
					seta.Transaction = trans;
					var v = vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent);
					int pocetRadku = seta.Update(v);

					if (trans != null)
						trans.Commit();

				}
				else if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.Zpracovat || processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.ZpracovatAPokracovat)
				{
					byte terminalid = (byte)terminal.ID;

					string xselectcommand =
						"Select Count(*) as number from " + TABLE_CZMST_SE +
						" where countentries=" + davka.ID +
						//" and cz_doslo>0 and cz_doslo<100" + 
						" and cz_doslo=" + terminalid +
						"";

					bool allowInsertData = true;

					System.Data.SqlClient.SqlCommand xselect = new System.Data.SqlClient.SqlCommand(xselectcommand, conn, trans);
					object datacount = xselect.ExecuteScalar();
					if (datacount != null && ((int)datacount) == 0)
					{
						allowInsertData = false;
					}

					if (allowInsertData)
					{

						so.Write("Import Dávky.");

						if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.ZpracovatAPokracovat)
							terminalid = 0;
						else //if (processVydejState == ProcessVydejState.ZpracovatAPokracovat)
							terminalid += 100;

						foreach (Fask.DataSets.Vydej.CZMST_SERow serow in vydejdata.CZMST_SE)
						{
							if (serow.RowState == DataRowState.Modified || serow.RowState == DataRowState.Unchanged)
							{
								serow.CZ_Doslo = terminalid;
							}
						}

						vydejdata.CZMST_SI.AcceptChanges();

						#region Smazani starych dat davky z vystupu
						if (Properties.Settings.Default.VydejPokracovatNaJinemTerminalu)
						{
							System.Data.SqlClient.SqlCommand xdeleteolddata = new System.Data.SqlClient.SqlCommand("Delete from " + TABLE_CZMST_SI + " where countentries=" + davka.ID, conn, trans);
							int ndeleted = xdeleteolddata.ExecuteNonQuery();

							xdeleteolddata.CommandText = "Delete from " + TABLE_CZMST_SIH + " where countentries=" + davka.ID;
							ndeleted = xdeleteolddata.ExecuteNonQuery();
						}
						#endregion

						foreach (Fask.DataSets.Vydej.CZMST_SIRow sirow in vydejdata.CZMST_SI)
						{
							sirow.SetAdded();
						}
						foreach (Fask.DataSets.Vydej.CZMST_SIHRow sihrow in vydejdata.CZMST_SIH)
						{
							sihrow.SetAdded();
						}
						so.Write("Dávka již byla jednou importována.");

						SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter seta = new SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter();
						seta.Connection = conn;
						seta.Transaction = trans;
						seta.Update(vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent));

						SQL_Datasets.VydejTableAdapters.CZMST_SITableAdapter sita = new SQL_Datasets.VydejTableAdapters.CZMST_SITableAdapter();
						sita.Connection = conn;
						sita.Transaction = trans;
						sita.Update(vydejdata.CZMST_SI.Select(null, null, DataViewRowState.Added));


						SQL_Datasets.VydejTableAdapters.CZMST_SIHTableAdapter sihta = new SQL_Datasets.VydejTableAdapters.CZMST_SIHTableAdapter();
						sihta.Connection = conn;
						sihta.Transaction = trans;
						sihta.Update(vydejdata.CZMST_SIH.Select(null, null, DataViewRowState.Added));


						Logging.ExceptionHandler2.Handle(LogLevel.Info,"Vydej Trancakce...");

						if (trans != null)
							trans.Commit();

						Logging.ExceptionHandler2.Handle(LogLevel.Info,"Vydej Trancakce OK");

					}
				}

			}
			catch (Exception ex)
			{

				Logging.ExceptionHandler2.Handle(LogLevel.Error, "(ProcessVydejState: " + processVydejState.ToString() + ")");
				Logging.ExceptionHandler2.Handle(ex);

				try
				{
					if (trans != null)
						trans.Rollback();

				}
				catch (Exception exTrans)
				{
					Logging.ExceptionHandler2.Handle(LogLevel.Error, "(ProcessVydejState: " + processVydejState.ToString() + ")");
					Logging.ExceptionHandler2.Handle(ex);
				}
				throw ex;
			}
			finally
			{
				if (conn != null && (conn.State & ConnectionState.Open) == ConnectionState.Open)
					conn.Close();
			}

			so.SetOK();

			return so;

		}


		#endregion


		#region IVydej Members


		#region Neimplementovano

		public StatusObject TEST_ImportVydejkaPohoda(int countEntries, int userID, string SKL_ID, string note)
		{
			throw new NotImplementedException();
		}

		public StatusObject TEST_ImportFakturaPohoda(int countEntries, int userID, string SKL_ID, string note)
		{
			throw new NotImplementedException();
		}

		
		#endregion


		#endregion


		#region Private

		private string ExportVydejka_ABRA_Z_ObjednavkaPrijata(Objednavka objednavka, Sklad sklad)
		{
			try
			{

				if (String.IsNullOrEmpty(objednavka.ID.Trim()))
					throw new Exception("Číslo dokladu nesmí být prázdné");

				string pom = string.Empty;


				ABRA_Datasets.Vydej.CZMST_SEDataTable dt_abra = Database.ABRA.ObjednavkaPrijata_GetData(objednavka.ID, sklad.ID);


				// *************************************************
				// naplneni do czmst_se ... 
				// *************************************************
				SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter();
				CZMST_SETableAdapter.Connection = new SqlConnection(Properties.Settings.Default.SqlProviderConnection);


				SQL_Datasets.Vydej.CZMST_SEDataTable seTable = new SQL_Datasets.Vydej.CZMST_SEDataTable();
				SQL_Datasets.Vydej.CZMST_SERow seRow = null;

				int cisloDavky = 0;

				int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
				int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;

				foreach (var Item in dt_abra)
				{


					seRow = seTable.NewCZMST_SERow();

					seRow.ITEMDESC = Item.IsITEMDESCNull() ? null : Item.ITEMDESC;
					if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
					{
						seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
					}

					seRow.ITEMNMBR = Item.ITEMNMBR;
					seRow.ITEMTYPE = Item.ITEMTYPE;
					seRow.ITEMCODE = Item.ITEMCODE;
					seRow.CountEntries = cisloDavky;

					seRow.CZ_CarKod = string.Empty;
					seRow.CZ_DatVyr_Delka = 0;
					seRow.CZ_DatVyr_Track = 0;
					seRow.CZ_Doslo = 0;
					seRow.CZ_SerNum_Delka = 0;
					seRow.CZ_SerNum_Track = 0; // TODO ??


					seRow.CZ_SW_Delka = 0;
					seRow.CZ_SW_Track = 0;

					seRow.LOCNCODE = string.Empty;
					seRow.Note = null;
					seRow.ORD = Item.ORD;
					seRow.PRINTED = 0;
					seRow.PRIORITY = 3;
					seRow.QTYPACK = 0;
					seRow.QTYPAL = 0;
					seRow.QTYSHPPD = Item.QTYSHPPD;
					seRow.SKL_ID = Item.SKL_ID;
					seRow.SOPNUMBE = Item.SOPNUMBE;
					seRow.TYPEPAL = "";

					seRow.VNDDOCNM = "";
					seRow.VNDITNUM = Item.VNDITNUM;
					seRow.MJ = Item.MJ;
					if (seRow.MJ.Length > MJ_MaxLength)
					{
						seRow.MJ = seRow.ITEMDESC.Remove(MJ_MaxLength);
					}

					//seRow.USERID = null;
					seRow.CZ_REZ1_Track = 0;
					seRow.CZ_REZ2_Track = 0;
					
					seRow.SetWEIGHTNull();

					seTable.AddCZMST_SERow(seRow);
					seRow = null;
				}


				try
				{
					// ulozit do se
					CZMST_SETableAdapter.Connection.Open();
					CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
					// prideleni cisla davky v transakci ..
					cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries();
					cisloDavky += 1;
					foreach (var item in seTable)
					{
						item.CountEntries = cisloDavky;

						// porad je pouze pridana, nikoli zmenena po zmene countentries...
						item.AcceptChanges();
						item.SetAdded();
					}
					int updatedRows = CZMST_SETableAdapter.Update(seTable);
					CZMST_SETableAdapter.Transaction.Commit();
					objednavka.CisloDavky = cisloDavky.ToString();
				}
				catch (Exception e)
				{
					Logging.ExceptionHandler2.Handle("ABRA","ExportVydejka_ABRA_Z_Vydejky" , e);
					try
					{
						CZMST_SETableAdapter.Transaction.Rollback();
					}
					catch { }
					throw new Exception("Uložení načtených položek se nezdařilo!");
				}
				finally
				{
					if ((CZMST_SETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
						CZMST_SETableAdapter.Connection.Close();
				}

				return "OK";
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(LogLevel.Error, "(Vydejka: " + objednavka.ID + ")");
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
				//return ex.Message;
			}
		}

		private string ExportVydejka_ABRA_Z_Vydejky(Objednavka objednavka, Sklad sklad)
		{
			try
			{

				if (String.IsNullOrEmpty(objednavka.ID.Trim()))
					throw new Exception("Číslo dokladu nesmí být prázdné");

				string pom = string.Empty;


				ABRA_Datasets.Vydej.CZMST_SEDataTable dt_abra = Database.ABRA.Vydejka_GetData(objednavka.ID, sklad.ID);


				// *************************************************
				// naplneni do czmst_se ... 
				// *************************************************
				SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter CZMST_SETableAdapter = new SQL_Datasets.VydejTableAdapters.CZMST_SETableAdapter();
				CZMST_SETableAdapter.Connection = new SqlConnection(Properties.Settings.Default.SqlProviderConnection);


				SQL_Datasets.Vydej.CZMST_SEDataTable seTable = new SQL_Datasets.Vydej.CZMST_SEDataTable();
				SQL_Datasets.Vydej.CZMST_SERow seRow = null;

				int cisloDavky = 0;

				int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["MJ"].MaxLength;
				int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Vydej.ColumnsInfo_CZMST_SE["ITEMDESC"].MaxLength;

				foreach (var Item in dt_abra)
				{

					seRow = seTable.NewCZMST_SERow();

					seRow.ITEMDESC = Item.IsITEMDESCNull() ? null : Item.ITEMDESC;
					if (seRow.ITEMDESC.Length > ITEMDESC_MaxLength)
					{
						seRow.ITEMDESC = seRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
					}

					seRow.ITEMNMBR = Item.ITEMNMBR;
					seRow.ITEMTYPE = "";
					seRow.CountEntries = cisloDavky;

					seRow.CZ_CarKod = string.Empty;
					seRow.CZ_DatVyr_Delka = 0;
					seRow.CZ_DatVyr_Track = 0;
					seRow.CZ_Doslo = 0;
					seRow.CZ_SerNum_Delka = 0;
					seRow.CZ_SerNum_Track = 0; // TODO ??


					seRow.CZ_SW_Delka = 0;
					seRow.CZ_SW_Track = 0;

					seRow.LOCNCODE = string.Empty;
					seRow.Note = null;
					seRow.ORD = 0;
					seRow.PRINTED = 0;
					seRow.PRIORITY = 3;
					seRow.QTYPACK = 0;
					seRow.QTYPAL = 0;
					seRow.QTYSHPPD = Item.QTYSHPPD;
					seRow.SKL_ID = Item.SKL_ID;
					seRow.SOPNUMBE = Item.SOPNUMBE;
					seRow.TYPEPAL = "";

					seRow.VNDDOCNM = "";
					seRow.VNDITNUM = Item.VNDITNUM;
					seRow.MJ = Item.MJ;


					seTable.AddCZMST_SERow(seRow);
					seRow = null;
				}


				try
				{
					// ulozit do se
					CZMST_SETableAdapter.Connection.Open();
					CZMST_SETableAdapter.Transaction = CZMST_SETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
					// prideleni cisla davky v transakci ..
					cisloDavky = Database.Vydej.CZMSTSE_MAX_CountEntries();
					cisloDavky += 1;
					foreach (var item in seTable)
					{
						item.CountEntries = cisloDavky;

						// porad je pouze pridana, nikoli zmenena po zmene countentries...
						item.AcceptChanges();
						item.SetAdded();
					}
					int updatedRows = CZMST_SETableAdapter.Update(seTable);
					CZMST_SETableAdapter.Transaction.Commit();
					objednavka.CisloDavky = cisloDavky.ToString();
				}
				catch (Exception e)
				{
					Logging.ExceptionHandler2.Handle("ABRA","ExportVydejka_ABRA_Z_Vydejky", e);
					try
					{
						CZMST_SETableAdapter.Transaction.Rollback();
					}
					catch { }
					throw new Exception("Uložení načtených položek se nezdařilo!");
				}
				finally
				{
					if ((CZMST_SETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
						CZMST_SETableAdapter.Connection.Close();
				}

				return "OK";
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(LogLevel.Error,"(Vydejka: " + objednavka.ID + ")");
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
				//return ex.Message;
			}
		}

		public DataSet Vydej_DetailPolozka(Item item)
		{
			throw new NotImplementedException();
		}


		#endregion


	}
}
