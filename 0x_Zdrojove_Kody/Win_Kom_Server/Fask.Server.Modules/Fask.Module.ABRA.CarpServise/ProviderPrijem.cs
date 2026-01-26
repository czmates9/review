using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using System.IO;
using System.Data.Common;
using Fask.Logging;
using Fask.Server.Interfaces.DataSets;

namespace Fask.Module.ABRA.CarpServise
{
    public partial class Provider : Server.Interfaces.Prijem.IPrijem
    {

		#region IPrijem Members Implementovane

		public Fask.DataSets.PrijemDavky Prijem_GetPrijemky(Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item)
		{
			// \TODO : seznam objednavek vydanych z sql serveru ... 
			try
			{

				string select =
					" select A.countentries, A.ponumber, B.CntItems CntItems, C.qtyshppdsum SumItems " +
					" from " + TABLE_CZMST_PE + " A " +
					" INNER JOIN " +
					" ( " +
					"	select X.countentries, X.ponumber, count(X.itemnmbr) CntItems " +
					"	from ( " +
					"		Select countentries, ponumber, itemnmbr " +
					"		from " + TABLE_CZMST_PE + " " +
					"       where qtypack=0" +
					"		group by countentries, ponumber, itemnmbr " +
					"	) X " +
					"	group by X.countentries, X.ponumber " +
					" ) B ON  " +
					" A.countentries=B.CountEntries " +
					" and A.ponumber=B.ponumber" +
					" INNER JOIN " +
					" ( " +
					"	Select countentries, ponumber, " +
					"		Sum(qtyshppd) as qtyshppdsum" +
					"	from " + TABLE_CZMST_PE + " " +
					"   where qtypack=0" +
					"	group by countentries, ponumber " +
					" ) C ON " +
					" B.countentries=C.CountEntries  " +
					" and B.ponumber=C.ponumber " +
					" where " +
					//" A.CountEntries not in (Select CountEntries from " + TABLE_CZMST_PI + ") " +
					//" and " +
					" (A.CZ_Doslo<=0 OR A.CZ_Doslo=" + terminal.ID + ")" +
					" and A.SKL_ID LIKE '" + sklad.ID + "%' " +
					" group by A.countentries, A.ponumber, B.CntItems, C.qtyshppdsum " +
					" order by A.countentries ";



				System.Data.SqlClient.SqlDataAdapter xda = new System.Data.SqlClient.SqlDataAdapter(select, Properties.Settings.Default.SqlProviderConnection);

				Fask.DataSets.PrijemDavky volneprijemky = new Fask.DataSets.PrijemDavky();
				((DbDataAdapter)xda).Fill(volneprijemky, volneprijemky.Hlavicky.TableName);

				return volneprijemky;
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(LogLevel.Error, "(Sklad: " + sklad.ID + ")");
				Logging.ExceptionHandler2.Handle(ex);
				return null;
			}
		}

		public bool Prijem_GetPrijemkaReceived(Davka davka, Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item)
		{
			// \TODO : potvrdit stazeni ... 

			string selectCount = "SELECT Count(CountEntries) as davka FROM " + TABLE_CZMST_PE + " where CountEntries=" + davka.ID.Value + " AND (CZ_Doslo<=0 OR CZ_Doslo=" + terminal.ID + ")";
			string update = "Update " + TABLE_CZMST_PE + " set CZ_Doslo=" + terminal.ID + " where countentries=" + davka.ID.Value;
			System.Data.SqlClient.SqlConnection xconn = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.SqlProviderConnection);
			System.Data.SqlClient.SqlCommand xcommAllowed = new System.Data.SqlClient.SqlCommand(selectCount, xconn);
			System.Data.SqlClient.SqlCommand xcomm = new System.Data.SqlClient.SqlCommand(update, xconn);
			System.Data.IDbTransaction itrans = null;

			int res = 0;
			try
			{
				xconn.Open();
				itrans = xconn.BeginTransaction(System.Data.IsolationLevel.Serializable);

				xcommAllowed.Transaction = (System.Data.SqlClient.SqlTransaction)itrans;
				object r = xcommAllowed.ExecuteScalar();
				if (r == null)
					throw new Exception("Dávka nenalezena.");
				if (r is int && ((int)r) <= 0)
					throw new Exception("Dávka se již zpracovává.");

				xcomm.Transaction = (System.Data.SqlClient.SqlTransaction)itrans;
				res = xcomm.ExecuteNonQuery();
				if (itrans != null)
					itrans.Commit();

			}
			catch (Exception ex)
			{
				if (itrans != null)
					itrans.Rollback();
				Logging.ExceptionHandler2.Handle(LogLevel.Error, " (I1 : Dávka: " + davka.ID.Value + ", Terminál ID:" + terminal.ID + ")");
				Logging.ExceptionHandler2.Handle(ex);
				//throw ex;
				return false;
			}
			finally
			{
				if (xconn.State == System.Data.ConnectionState.Open)
					xconn.Close();
			}

			return (res > 0);
		}

		public StatusObject Prijem_Finish_Prijemka(Davka davka, Server.Interfaces.Classes.Terminal terminal, string password)
		{
			StatusObject so = new StatusObject();
			so.StatusText = "";

			try
			{
				Database.Prijem.UpdateCzDosloByCountEntries((byte)(terminal.ID + 100), davka.ID.Value);

				so.SetOK();
			}
			catch (Exception e)
			{
				so.Exception = true;
				so.StatusText = e.Message;
			}

			return so;
		}

		public Fask.DataSets.Prijem Prijem_GetPrijemka(Davka davka, Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item)
		{
			Fask.DataSets.Prijem prijem = new Fask.DataSets.Prijem();

			try
			{
				// \TODO : stahnout konkretni objednavku vydanou ...

				string select = "SELECT * FROM " + TABLE_CZMST_PE + " where CountEntries=" + davka.ID.Value + " order by dex_row_id";
				System.Data.SqlClient.SqlDataAdapter xda = new System.Data.SqlClient.SqlDataAdapter(select, Properties.Settings.Default.SqlProviderConnection);

				//Nacteni dat prijemky z databaze

				((DbDataAdapter)xda).Fill(prijem, prijem.CZMST_PE.TableName);

				select = "SELECT * FROM " + TABLE_CZMST_PI + " where CountEntries=" + davka.ID.Value + " order by dex_row_id";
				xda.SelectCommand.CommandText = select;

				((DbDataAdapter)xda).Fill(prijem, prijem.CZMST_PI.TableName);
			}
			catch (Exception ex)
			{
				if (prijem.HasErrors)
				{
					if (prijem.CZMST_PE.HasErrors)
					{
						Logging.ExceptionHandler2.Handle(prijem);

						DataRow[] rows = prijem.CZMST_PE.GetErrors();

						foreach (var row in rows)
						{
							if (row.HasErrors)
							{
								DataColumn[] Columns = row.GetColumnsInError();
								foreach (DataColumn Column in Columns)
								{
									string err = row.GetColumnError(Column);
								}
							}
						}
					}
				}

				throw ex;
			}

			return prijem;
		}

		public bool Prijem_AfterProcessedAction(Davka davka)
		{
			return true;
		}

		public StatusObject Prijem_Storno_Prijemka(Davka davka, Server.Interfaces.Classes.Terminal terminal, string password)
		{
			StatusObject so = new StatusObject();


			if (password.Trim() != Properties.Settings.Default.HesloStornoPrijemka.Trim())
			{
				so.StatusText = "Zadané heslo je špatně!";
				so.Exception = true;
				return so;
			}

			try
			{

				if (Database.Prijem.GetDataByCountEntries(davka.ID.Value).Count > 0)
				{
					so.StatusText = "Danou dávku nelze zrušit! Existují nasnímané položky!";
					so.Exception = true;
					return so;
				}

				Database.Prijem.UpdateCzDosloByCountEntries(100, davka.ID.Value);

				so.StatusText = "OK";
				return so;

			}
			finally
			{
			}

		}

		public DataSet Prijem_DetailDavka(Davka davka)
		{
			return null;
		}

		public StatusObject Prijem_Process(Davka davka, Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item, Fask.DataSets.Prijem prijemdata, Fask.Server.Interfaces.Prijem.ProcessState processPrijemState)
		{

			string guidDavka = string.Empty;
			if (prijemdata.CZMST_PEH.Count > 0)
				guidDavka = prijemdata.CZMST_PEH[0].GUID.ToString();
			else
				guidDavka = Guid.NewGuid().ToString();
			string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Properties.Settings.Default.StatusObjectsDirectory, guidDavka));

			//StatusObject so = new StatusObject(filePath);
			StatusObject so = StatusObject.Load(filePath);
			if (so == null) //neexistuje => vytvorit a pokracovat
			{
				so = new StatusObject(filePath);
				so.Write("probiha zpracovani");
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

			//Fask.Server.Interfaces.Classes.StatusObject so = new Fask.Server.Interfaces.Classes.StatusObject();
			so.StatusText = "Příjem zpracování...";

			SqlTransaction trans = null;
			SqlConnection conn = null;
			bool uvolnitdavku = processPrijemState == Fask.Server.Interfaces.Prijem.ProcessState.Uvolnit;
			try
			{
				conn = new SqlConnection(Properties.Settings.Default.SqlProviderConnection);
				conn.Open();

				if (uvolnitdavku) //uvolnit davku
				{
					string updateuvolnit = "Update " + TABLE_CZMST_PE + " set CZ_Doslo=0 where CountEntries=" + davka.ID.Value;
					System.Data.SqlClient.SqlCommand xcommand = new System.Data.SqlClient.SqlCommand(updateuvolnit, conn, trans);
					int rows = xcommand.ExecuteNonQuery();
				}
				else //zapsat davku
				{

					bool allowInsertData = true;
					//Test zda je mozne data pridat, jestlize jiz existuji, tak nepridat. 
					System.Data.SqlClient.SqlCommand xselect = new System.Data.SqlClient.SqlCommand("Select Count(*) as number from " + TABLE_CZMST_PI + " where countentries=" + davka.ID.Value, conn);
					object datacount = xselect.ExecuteScalar();
					if (datacount != null && ((int)datacount) > 0)
					{
						Logging.ExceptionHandler2.Handle(LogLevel.Error,"Data allready exist in database" + "(Prijem : Dávka " + davka.ID.Value + ")");

						allowInsertData = false;
					}

					if (allowInsertData)
					{

						trans = conn.BeginTransaction();
						string updatePE = "Update " + TABLE_CZMST_PE + " set CZ_Doslo=" + (terminal.ID + 100) + " where CountEntries=" + davka.ID;
						System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatePE, conn, trans);

						SQL_Datasets.PrijemTableAdapters.CZMST_PITableAdapter dita = new SQL_Datasets.PrijemTableAdapters.CZMST_PITableAdapter();
						dita.Connection = conn;
						dita.Transaction = trans;
						dita.Update(prijemdata.CZMST_PI.Select(null, null, DataViewRowState.Added));


						daPIH.SelectCommand.Transaction = trans;
						daPIH.InsertCommand.Transaction = trans;

						if (prijemdata.CZMST_PIH.Count > 0)
						{
							daPIH.Update(prijemdata.CZMST_PIH.Select(null, null, System.Data.DataViewRowState.Added));
						}

						int rows = command.ExecuteNonQuery();

						if (trans != null)
							trans.Commit();
					}

				}

				so.SetOK();
				return so;
			}
			catch (Exception ex)
			{
				// StatusObject
				so.SetException(ex);
				so.Finished = true;

				if (trans != null)
					trans.Rollback();

				Logging.ExceptionHandler2.Handle(LogLevel.Error,"(ProcessPrijemState: '" + processPrijemState.ToString() + "', davka:'" + davka.ID.Value + "')");
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
			}
			finally
			{
				so.Write();
				if ((conn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
					conn.Close();
			}
		}

		public Fask.Server.Interfaces.Classes.StatusInfo Prijem_GenerateDavka(
	Fask.Server.Interfaces.Classes.Objednavka objednavka,
	Fask.Server.Interfaces.Classes.Sklad sklad
	)
		{

			Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();
			string result = string.Empty;


			byte? czdoslo = Database.Prijem.CZMSTPE_PONUMBER_CZDOSLO(objednavka.ID);
			// 1) test na rozpracovany doklad 
			if (czdoslo.HasValue && czdoslo.Value > 0)
			{ //existuje a je stazene v terminalu => vrati chybu ... nelze vytvorit duplicitu ...
				si.ID = -1;
				si.Description = "Existuje rozpracovaná dávka pro doklad '" + objednavka.ID + "' na terminálu č.:" + czdoslo.Value;
				si.InnerException = new Exception(si.Description);
				Logging.ExceptionHandler2.Handle(LogLevel.Error,si.Description);
				return si;
			}

			// 2) test zda je to objednavka vydana
			if (Database.ABRA.ObjednavkaVydana_Exists(objednavka.ID))
			{

				string stav = this.ExportPrijemkaABRA_Z_ObjednavkyVydane(objednavka, sklad);
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


		#endregion

		public Fask.Server.Interfaces.DataSets.Obecne Prijem_GetPrijemky_External(Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item, List<string> ListCarKod)
		{
			//Generovat 
			Fask.Server.Interfaces.DataSets.Obecne ds_obecne = new Fask.Server.Interfaces.DataSets.Obecne();
			Fask.DataSets.Prijem ds_prijem = new Fask.DataSets.Prijem();

			try
			{

				System.Data.SqlClient.SqlConnection dbconnection = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.SqlProviderConnection);
				System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();
				dbCommand.CommandType = CommandType.Text;

				dbCommand.CommandText = "Select * from CZMST_PE where cz_doslo <= 100"; // jen pripravene nebo stazene se budou vylucovat

				dbCommand.Connection = dbconnection;
				System.Data.SqlClient.SqlDataAdapter dbda = new System.Data.SqlClient.SqlDataAdapter(dbCommand);

				dbda.Fill(ds_prijem, ds_prijem.CZMST_PE.TableName);

				ds_obecne = Database.ABRA.ObjednavkaVydana_GetDavkyByCarKody(terminal.ID, sklad.ID, String.Join(",", ListCarKod.ToArray()));


				ds_obecne.Prijemky.ToList().ForEach(row =>
				{
					if (ds_prijem.CZMST_PE.Any(rowp => rowp.PONUMBER.Trim() == row.PONUMBER.Trim()))
					{
						row.Delete();
					}
				});

				ds_obecne.Prijemky.AcceptChanges();


			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(ex);
				Logging.ExceptionHandler2.Handle(ds_obecne);
				Logging.ExceptionHandler2.Handle(ds_prijem);

				throw ex;
			}

			return ds_obecne;

		}




		#region NE implementovane

		public StatusObject Prijem_Online_Add(Davka davka, Server.Interfaces.Classes.Terminal terminal, Fask.DataSets.Prijem prijemRows)
		{
			throw new NotImplementedException();
		}

		public StatusObject Prijem_Online_Del(Davka davka, Server.Interfaces.Classes.Terminal terminal, Fask.DataSets.Prijem prijemRows)
		{
			throw new NotImplementedException();
		}

		public StatusObject Prijem_Online_Quantity(Davka davka, Server.Interfaces.Classes.Terminal terminal, ref Fask.DataSets.Prijem prijemRows)
		{
			throw new NotImplementedException();
		}

		public StatusOverLokace Prijem_Online_OverLokace(string serltnum, string itemnmbr, string locncode, decimal qtyshppd, string skl_id)
		{
			throw new NotImplementedException();
		}

		public bool Prijem_GetSkladExpedice(string ITEMNMBR, decimal MnozstviZadane, decimal MnozstviNasnimane, out decimal MnozstviDodavatelePozadovano, out decimal MnozstviDodavateleDodano, out decimal MnozstviDodavateleDodat, out decimal MnozstviOdberateliPozadovano, out decimal MnozstviOdberatelumDodano, out decimal MnozstviOdberatelumDodat, out decimal Vysledek)
		{
			throw new NotImplementedException();
		}

		public DataSet Prijem_Detail(Objednavka objednavka, Sklad sklad)
		{
			throw new NotImplementedException();
		}

		public DataSet Prijem_Detail_Polozka(Objednavka objednavka, Sklad sklad, Item polozka)
		{
			throw new NotImplementedException();
		}

		public string Online_GenerateSerltnum(string itemnmbr, string skl_id, string oldSerltnum)
		{
			throw new NotImplementedException();
		}

		public Obecne Online_GetDoporuceneLokace(string itemnmbr, string skl_id, string serltnum)
		{
			throw new NotImplementedException();
		}

		public Obecne Online_GetNezrealizovanePrijemky()
		{
			throw new NotImplementedException();
		}


		#endregion

		#region Private


		public string ExportPrijemkaABRA_Z_ObjednavkyVydane(Fask.Server.Interfaces.Classes.Objednavka objednavka, Fask.Server.Interfaces.Classes.Sklad sklad)
		{

			try
			{

				if (String.IsNullOrEmpty(objednavka.ID.Trim()))
					throw new Exception("Číslo dokladu nesmí být prázdné");

				string pom = string.Empty;


				ABRA_Datasets.Prijem.CZMST_PEDataTable dt_abra = Database.ABRA.ObjednavkaVydana_GetData(objednavka.ID, sklad.ID);


				// *************************************************
				// naplneni do czmst_pe ... 
				// *************************************************
				SQL_Datasets.PrijemTableAdapters.CZMST_PETableAdapter CZMST_PETableAdapter = new Fask.Module.ABRA.CarpServise.SQL_Datasets.PrijemTableAdapters.CZMST_PETableAdapter();
				CZMST_PETableAdapter.Connection = new SqlConnection(Properties.Settings.Default.SqlProviderConnection);


				SQL_Datasets.Prijem.CZMST_PEDataTable peTable = new SQL_Datasets.Prijem.CZMST_PEDataTable();
				SQL_Datasets.Prijem.CZMST_PERow peRow = null;

				int cisloDavky = 0;

				int MJ_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["MJ"].MaxLength;
				int ITEMDESC_MaxLength = (int)Fask.Columns.Inicializace.InitInstance.Prijem.ColumnsInfo_CZMST_PE["ITEMDESC"].MaxLength;

				foreach (var Item in dt_abra)
				{


					peRow = peTable.NewCZMST_PERow();

					peRow.ITEMDESC = Item.IsITEMDESCNull() ? null : Item.ITEMDESC;
					if (peRow.ITEMDESC.Length > ITEMDESC_MaxLength)
					{
						peRow.ITEMDESC = peRow.ITEMDESC.Remove(ITEMDESC_MaxLength);
					}

					peRow.ITEMNMBR = Item.ITEMNMBR;
					peRow.CountEntries = cisloDavky;

					peRow.CZ_CarKod = string.Empty;
					peRow.CZ_DatVyr_Delka = 0;
					peRow.CZ_DatVyr_Track = 0;
					peRow.CZ_Doslo = 0;
					peRow.CZ_SerNum_Delka = 0;
					peRow.CZ_SerNum_Track = 0; // TODO ??


					peRow.CZ_SW_Delka = 0;
					peRow.CZ_SW_Track = 0;

					peRow.LOCNCODE = string.Empty;
					peRow.ORD = 0;
					peRow.QTYPACK = 0;
					peRow.QTYSHPPD = Item.QTYSHPPD;
					peRow.SKL_ID = Item.SKL_ID;
					peRow.PONUMBER = Item.PONUMBER;
					peRow.TYPEPAL = "";

					peRow.VNDDOCNM = "";
					peRow.VNDITNUM = Item.VNDITNUM;

					peRow.MJ = Item.MJ;
					if (peRow.MJ.Length > MJ_MaxLength)
					{
						peRow.MJ = peRow.ITEMDESC.Remove(MJ_MaxLength);
					}

					peRow.SetWEIGHTNull();
					peRow.SetNMBRPALNull();
					peRow.ITEMCODE = Item.ITEMCODE;

					//peRow.SERLTNUM = string.empty;
					//peRow.CZ_REZ1_Track = 0;
					//peRow.CZ_REZ2_Track = 0;

					peTable.AddCZMST_PERow(peRow);
					peRow = null;
				}


				try
				{
					// ulozit do se
					CZMST_PETableAdapter.Connection.Open();
					CZMST_PETableAdapter.Transaction = CZMST_PETableAdapter.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
					// prideleni cisla davky v transakci ..
					cisloDavky = Database.Prijem.CZMSTPE_MAX_CountEntries();
					cisloDavky += 1;
					foreach (var item in peTable)
					{
						item.CountEntries = cisloDavky;

						// porad je pouze pridana, nikoli zmenena po zmene countentries...
						item.AcceptChanges();
						item.SetAdded();
					}
					int updatedRows = CZMST_PETableAdapter.Update(peTable);
					CZMST_PETableAdapter.Transaction.Commit();
					objednavka.CisloDavky = cisloDavky.ToString();
				}
				catch (Exception e)
				{
					Logging.ExceptionHandler2.Handle("ABRA","ExportPrijemka_ABRA_Z_Prijemky" , e);
					try
					{
						CZMST_PETableAdapter.Transaction.Rollback();
					}
					catch { }
					throw new Exception("Uložení načtených položek se nezdařilo!");
				}
				finally
				{
					if ((CZMST_PETableAdapter.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
						CZMST_PETableAdapter.Connection.Close();
				}

				return "OK";
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(LogLevel.Error, "(Prijemka: " + objednavka.ID + ")");
				Logging.ExceptionHandler2.Handle(ex);
				throw ex;
				//return ex.Message;
			}

		}





		#endregion

	}
}
