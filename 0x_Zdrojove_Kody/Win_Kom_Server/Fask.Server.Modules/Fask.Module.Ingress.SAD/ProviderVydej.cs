using Fask.DataSets;
using Fask.Logging;
using Fask.Server.Interfaces.Classes;
using Fask.Server.Interfaces.Vydej;
using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fask.Module.Ingres.SAD
{
	public partial class Provider :
		Fask.Server.Interfaces.Vydej.IVydej
	{

		#region IVydej Members Implementovane s obsahem...

		public StatusInfo Vydej_GenerateDavka(Objednavka objednavka, Sklad sklad)
		{
			throw new NotImplementedException($"{nameof(Vydej_GenerateDavka)} is not Implemented");

			//StatusInfo si = new StatusInfo();
			//si.Description = "Vydej_GenerateDavka start";
			//si.ID = 0;

			//// 1) test na rozpracovany doklad 

			//byte? czdoslo = Database.Vydej.CZMSTSE_SOPNUMBER_CZDOSLO(objednavka.ID);

			//if (czdoslo.HasValue)
			//{ //existuje a je stazene v terminalu => vrati chybu ... nelze vytvorit duplicitu ...
			//	if (czdoslo.Value > 0)
			//	{
			//		si.ID = -1;
			//		si.Description = string.Format("Existuje rozpracovaná dávka pro doklad '{0}' na terminálu č.:{1}", objednavka.ID, czdoslo.Value); //"Existuje rozpracovaná dávka pro doklad '" + objednavka.ID + "' na terminálu č.:" + czdoslo.Value;
			//		si.InnerException = new Exception(si.Description);
			//		Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, si.Description);
			//		return si;
			//	}
			//	else if (czdoslo.Value == 0) //je pripravena ke zpracovani => uzavrit ... 
			//	{
			//		Database.Vydej.CZMSTSE_UPDATE_CZDOSLO(objednavka.ID);
			//	}
			//}


			//// test zda je to vydejka
			////if (Database.ABRA.Vydejka_Exists(objednavka.ID))
			////{
			////    string stav = this.ExportVydejka_ABRA_Z_Vydejky(objednavka, sklad);

			////    if (stav != "OK")
			////    {
			////        si.ID = -10;
			////        si.Description = stav;
			////        si.InnerException = new Exception(si.Description);
			////        return si;
			////    }
			////    else
			////    {
			////        si.ID = int.Parse(objednavka.CisloDavky);
			////        si.Description = "OK";
			////        si.InnerException = null;
			////    }
			////}
			////Zda je to Objednavka Pridata
			////if (Database.ABRA.ObjednavkaPrijata_Exists(objednavka.ID))
			////{
			////	string stav = this.ExportVydejka_ABRA_Z_ObjednavkaPrijata(objednavka, sklad);

			////	if (stav != "OK")
			////	{
			////		si.ID = -10;
			////		si.Description = stav;
			////		si.InnerException = new Exception(si.Description);
			////		return si;
			////	}
			////	else
			////	{
			////		si.ID = int.Parse(objednavka.CisloDavky);
			////		si.Description = "OK";
			////		si.InnerException = null;
			////	}
			////}
			////Zda je to Dodací list
			//if (Database.ABRA.DodaciList_Exists(objednavka.ID))
			//{
			//	Globals.LoadConfiguration();

			//	string stav = Classes.ABRA.ExportVydejka_ABRA_Z_DodaciList(objednavka, sklad);

			//	string ID_DL = Database.ABRA.Get_ID_DodaciList(objednavka.ID, sklad.ID);

			//	stav = Classes.ABRA.Edit_Stav_DL(Globals.Konfigurace.Procesni_Rizeni[0].StavID_Vydej_VyskladnujeSe.Trim(), ID_DL);


			//	if (stav != "OK")
			//	{
			//		si.ID = -10;
			//		si.Description = stav;
			//		si.InnerException = new Exception(si.Description);
			//		return si;
			//	}
			//	else
			//	{
			//		si.ID = int.Parse(objednavka.CisloDavky);
			//		si.Description = "OK";
			//		si.InnerException = null;
			//	}
			//}
			//         //Zda je to Prevodka Vydej
			//         else if (Database.ABRA.PrevodVydej_Exists(objednavka.ID))
			//         {
			//             Globals.LoadConfiguration();

			//             string stav = Classes.ABRA.ExportVydejka_ABRA_Z_PrevodkaVydej(objednavka, sklad);

			//             string ID_DL = Database.ABRA.Get_ID_PrevodkaVydej(objednavka.ID, sklad.ID);

			//             stav = Classes.ABRA.Edit_Stav_PRV(Globals.Konfigurace.Procesni_Rizeni[0].StavID_PrevodVydej_VTerminalu.Trim(), ID_DL);


			//             if (stav != "OK")
			//             {
			//                 si.ID = -10;
			//                 si.Description = stav;
			//                 si.InnerException = new Exception(si.Description);
			//                 return si;
			//             }
			//             else
			//             {
			//                 si.ID = int.Parse(objednavka.CisloDavky);
			//                 si.Description = "OK";
			//                 si.InnerException = null;
			//             }
			//         }
			//         // 2) nepodporovany typ dokladu
			//         else
			//{
			//	si.Description = "Doklad '" + objednavka.ID + "' nenalezen.";
			//	if (sklad != null)
			//		si.Description += "\nSklad " + sklad.ID;
			//	si.InnerException = new Exception(si.Description);
			//	si.ID = -10;
			//	throw new Exception(si.Description);
			//}

			//return si;
		}

		public Fask.DataSets.Vydejky Vydej_GetVydejky(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item, Fask.Server.Interfaces.Classes.User user)
		{

			string select =
				" SELECT A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO " +
				//" SELECT distinct A.COUNTENTRIES, A.PRIORITY, A.SOPNUMBE, B.CNTITEMS CNTITEMS, C.QTYSHPPDSUM SUMITEMS, D.ROZPRACOVANO ROZPRACOVANO " +
				" FROM " + Constants.Common.TABLE_CZMST_SE + " A" +
				" INNER JOIN " +
				" ( " +
				"	SELECT X.COUNTENTRIES, X.SOPNUMBE, COUNT(X.ITEMNMBR) CNTITEMS " +
				"	FROM ( " +
				"		SELECT COUNTENTRIES, SOPNUMBE, ITEMNMBR " +
				"		FROM " + Constants.Common.TABLE_CZMST_SE +
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
				"	FROM " + Constants.Common.TABLE_CZMST_SE +
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
				"   SELECT COUNTENTRIES, SOPNUMBE, COUNT(*) AS ROZPRACOVANO FROM " + Constants.Common.TABLE_CZMST_SI +
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

			Fask.Logging.ExceptionHandler2.Handle(LogLevel.Trace, $"{nameof(Vydej_GetVydejky)} requested select statement {Environment.NewLine}'{select}'");

			Fask.DataSets.Vydejky dsV = new Vydejky();

			Database.Vydej.Fill_DataSet(select, dsV, dsV.Hlavicky.TableName);
			dsV.AcceptChanges();

			return dsV;
		}

		public Fask.DataSets.Vydej Vydej_GetVydejka(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
		{

			string select = string.Empty;

			if (Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
			{
				select = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SE +
				" WHERE CountEntries=" + davka.ID.Value.ToString() +
				" AND (cz_doslo<=0 OR cz_doslo=" + terminal.ID + ")" +
				" ORDER BY dex_row_id" +
				"";
			}
			else
			{
				select = "SELECT * FROM " + Constants.Common.TABLE_CZMST_SE +
				" WHERE CountEntries=" + davka.ID.Value.ToString() +
				" AND ITEMTYPE like '" + item.Type + "%' " +
				" AND (cz_doslo<=0 OR cz_doslo=" + terminal.ID + ")" +
				" ORDER BY dex_row_id" +
				"";
			}

			Fask.Logging.ExceptionHandler2.Handle(LogLevel.Trace, $"{nameof(Vydej_GetVydejka)} requested select statement {Environment.NewLine}'{select}'");

			Fask.DataSets.Vydej dsV = new Vydej();

			Database.Vydej.Fill_DataSet(select, dsV, dsV.CZMST_SE.TableName); //Vrati data dle SELECTU

			dsV.CZMST_SE.ToList().ForEach(x => x.CZ_Doslo = (byte)terminal.ID); // Nastaví ma CZ_Doslo ID Terminalu

			// Dotazeni dat z SI => pouze v pripade, ze se nepouziva vychystavani vice terminaly!!!
			// Slouzi k poslani davky k pozdejsimu zpracovani na jinem terminalu ...
			if (item.Type.Trim().Length == 0)
			{
				string selectSI =
					"SELECT * FROM " + Constants.Common.TABLE_CZMST_SI +
					" WHERE CountEntries=" + davka.ID.Value.ToString() +
					//" and ITEMTYPE like '" + SQLInjection.Filter(itemtype) + "%' " +
					"";

				var col_guid_guid = dsV.CZMST_SI.GUIDColumn;
				col_guid_guid.ColumnName = "guid_tmp";
				var col_guid_byte = dsV.CZMST_SI.Columns.Add("guid", typeof(byte[]));

				Database.Vydej.Fill_DataSet(selectSI, dsV, dsV.CZMST_SI.TableName); //Vrati data dle SELECTU

				dsV.CZMST_SI.ToList().ForEach(r => r[col_guid_guid] = new Guid((byte[])r[col_guid_byte]));
				dsV.CZMST_SI.Columns.Remove(col_guid_byte);
				col_guid_guid.ColumnName = "guid";

			}

			//Dotazeni predlohy seriovych cisel
			try
			{
				string selectSESN =
					"SELECT * FROM " + Constants.Common.TABLE_CZMST_SE_SN +
					" WHERE CountEntries=" + davka.ID.Value.ToString();

				Database.Vydej.Fill_DataSet(selectSESN, dsV, dsV.CZMST_SE_SN.TableName); //Vrati data dle SELECTU

			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle("Vydej", "GetVydejka.PredlohaSN", ex);
			}

			return dsV;

		}

		public bool Vydej_GetVydejkaReceived(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
		{
			Globals.LoadConfiguration();

			string selectCount = string.Empty;
			string update = string.Empty;

			if (Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
			{
				selectCount =
					"SELECT Count(CountEntries) as davka FROM " + Constants.Common.TABLE_CZMST_SE +
					" WHERE CountEntries=" + davka.ID.Value.ToString() +
					" AND (cz_doslo<=0 or cz_doslo=" + terminal.ID + ")" +
					"";

				update =
					"UPDATE " + Constants.Common.TABLE_CZMST_SE +
					" SET cz_doslo=" + terminal.ID.ToString() +
					" WHERE countentries=" + davka.ID.Value.ToString() +
					" AND (cz_doslo<=0 OR cz_doslo=" + terminal.ID + ")" +
					"";
			}
			else
			{
				selectCount =
					"SELECT Count(CountEntries) as davka FROM " + Constants.Common.TABLE_CZMST_SE +
					" WHERE CountEntries=" + davka.ID.Value.ToString() +
					" AND ITEMTYPE like '" + item.Type + "%' " +
					" AND (cz_doslo<=0 OR cz_doslo=" + terminal.ID + ")" +
					"";

				update =
					"UPDATE " + Constants.Common.TABLE_CZMST_SE +
					" SET cz_doslo=" + terminal.ID.ToString() +
					" WHERE countentries=" + davka.ID.Value.ToString() +
					" AND ITEMTYPE like '" + item.Type + "%' " +
					" AND (cz_doslo<=0 OR cz_doslo=" + terminal.ID + ")" +
					"";
			}

			IngresTransaction trans = null;
			IngresConnection connection = null;

			try
			{
				int res;
				using (connection = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
				{
					connection.Open();
					trans = connection.BeginTransaction();

					using (var comA = connection.CreateCommand())
					{
						comA.Transaction = trans;
						comA.CommandText = selectCount;
						comA.CommandType = CommandType.Text;

						object r = comA.ExecuteScalar();

						if (r == null)
							throw new Exception("Dávka nenalezena.");
						if (r is int && ((int)r) <= 0)
							throw new Exception("Dávka se již zpracovává.");
					}

					using (var comB = connection.CreateCommand())
					{
						comB.Transaction = trans;
						comB.CommandText = update;
						comB.CommandType = CommandType.Text;

						res = comB.ExecuteNonQuery();
					}

					if (trans != null)
						trans.Commit();
				}

				return (res > 0);

			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();

				throw ex;
			}
			finally
			{
				if (connection != null && connection.State == System.Data.ConnectionState.Open)
					connection.Close();
			}

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
			catch (Exception ex)
			{
				so.SetException(ex);
			}

			return so;
		}

		public StatusObject Vydej_Storno_Vydejka(Davka davka, Terminal terminal, string password)
		{
			StatusObject so = new StatusObject();

			if (!(Globals.Konfigurace.Vydej[0].IsHesloStornoVydejkaNull() || string.IsNullOrEmpty(Globals.Konfigurace.Vydej[0].HesloStornoVydejka)))
			{
				if (password.Trim() != Globals.Konfigurace.Vydej[0].HesloStornoVydejka.Trim())
				{
					so.StatusText = "Zadané heslo je špatně!";
					so.Exception = true;
					return so;
				}
			}
			else
			{
				Logging.ExceptionHandler2.Handle(LogLevel.Warn, $"{nameof(Vydej_Storno_Vydejka)}: password for storno vydejka is not configured.");
			}

			try
			{
				string select = @"SELECT * FROM " + Constants.Common.TABLE_CZMST_SI + " WHERE CountEntries = " + davka.ID.Value.ToString();

				Fask.DataSets.Vydej dsV = new Vydej();
				Database.Vydej.Fill_DataSet(select, dsV, dsV.CZMST_SI.TableName); //Vrati data dle SELECTU

				if (dsV.CZMST_SI.Count > 0)
				{
					so.StatusText = "Danou dávku nelze zrušit! Existují nasnímané položky!";
					so.Exception = true;
					return so;
				}

				Database.Vydej.UpdateCzDosloByCountEntries(100, davka.ID.Value);

				so.SetOK();

				return so;
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex);
				so.SetException(ex);
				return so;
			}

		}

		public StatusObject Vydej_Process(Davka davka, Terminal terminal, Sklad sklad, Item item, Fask.DataSets.Vydej vydejdata, ProcessState processVydejState, string itemtype)
		{

			string guidDavka = string.Empty;

			if (vydejdata.CZMST_SEH.Count > 0)
				guidDavka = vydejdata.CZMST_SEH[0].GUID.ToString();
			else
				guidDavka = Guid.NewGuid().ToString();

			string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals.Konfigurace.Vydej[0].StatusObjectsDirectory, guidDavka));

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

			IngresTransaction trans = null;
			IngresConnection conn = null;

			if (vydejdata == null)
				return so;

			vydejdata.AcceptChanges();

			try
			{

				using (conn = new IngresConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
				{
					conn.Open();
					trans = conn.BeginTransaction();

					if (processVydejState == ProcessState.Uvolnit) //uvolnit davku
					{
						foreach (Fask.DataSets.Vydej.CZMST_SERow serow in vydejdata.CZMST_SE)
						{
							if (serow.RowState == DataRowState.Modified || serow.RowState == DataRowState.Unchanged)
							{
								serow.CZ_Doslo = 0;
							}
						}

						var v = vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent);
						Database.Vydej.Update_CZMST_SE(v, conn, trans);

						if (trans != null)
							trans.Commit();
					}
					else if (processVydejState == ProcessState.Zpracovat || processVydejState == ProcessState.ZpracovatAPokracovat)
					{
						byte terminalid = (byte)terminal.ID;

						bool allowInsertData = Database.Vydej.SI_AllowInsert(davka.ID, terminalid);

						if (allowInsertData)
                        {

                            so.Write("Import Dávky.");

                            if (processVydejState == Fask.Server.Interfaces.Vydej.ProcessState.ZpracovatAPokracovat)
                                terminalid = 0;
                            else
                                terminalid += 100;

                            foreach (Fask.DataSets.Vydej.CZMST_SERow serow in vydejdata.CZMST_SE)
                            {
                                if (serow.RowState == DataRowState.Modified || serow.RowState == DataRowState.Unchanged)
                                {
                                    serow.CZ_Doslo = terminalid;
                                }
                            }

                            vydejdata.CZMST_SI.AcceptChanges(); // NACO TO JE TADY??

                            #region Smazani starych dat davky z vystupu

                            if (Globals.Konfigurace.Vydej[0].PokracovatNaJinemTerminalu)
                            {
                                Database.Vydej.Deleted_SI_SIH_vTransakci(davka.ID);
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

                            var se = vydejdata.CZMST_SE.Select(null, null, DataViewRowState.ModifiedCurrent);
                            Database.Vydej.Update_CZMST_SE(se, conn, trans);

							fill_notnull_values_with_defaults(vydejdata);
                            var si = add_guid_byte_type_and_values(vydejdata);
                            Database.Vydej.Update_CZMST_SI(si, conn, trans);

                            var sih = vydejdata.CZMST_SIH.Select(null, null, DataViewRowState.Added);
                            Database.Vydej.Update_CZMST_SIH(sih, conn, trans);


                            #region lokace

                            if (vydejdata.Parametry.Count > 0 && !vydejdata.Parametry.First().IsCONFIG_LOKACE_POVOLITNull() && vydejdata.Parametry.First().CONFIG_LOKACE_POVOLIT)
                            {
                                so.Write("Lokační Mechanizmus");

                                foreach (Fask.DataSets.Vydej.CZMST_SIRow sirow in vydejdata.CZMST_SI)
                                {
                                    int guidcount = Database.Lokace.Count_STAVPOHYB(conn, sirow.GUID);

                                    if (guidcount % 2 == 0)
                                    {
                                        DateTime dtnow = DateTime.Now;
                                        Fask.Server.Interfaces.Lokace.LokacePohyb pohybrow = new Fask.Server.Interfaces.Lokace.LokacePohyb();
                                        pohybrow.ITEMNMBR = sirow.ITEMNMBR;
                                        pohybrow.DOCUMENT_NUMBER = sirow.SOPNUMBE;
                                        pohybrow.POHYB_TYPE = Fask.Server.Interfaces.Lokace.TypeOfRecord.V;
                                        pohybrow.POHYB_SRC = "V";
                                        pohybrow.SOURCE = "S";      // doplneni ze serveru ...
                                        pohybrow.QTYSHPPD = sirow.QTYSHPPD;
                                        pohybrow.SERLTNUM = sirow.SERLTNUM;
                                        pohybrow.SKL_ID_SRC = sirow.IsSKL_IDNull() ? string.Empty : sirow.SKL_ID;
                                        pohybrow.SKL_ID_DST = string.Empty;
                                        pohybrow.LOCNCODE_SRC = sirow.IsLOCNCODENull() ? string.Empty : sirow.LOCNCODE;
                                        pohybrow.LOCNCODE_DST = string.Empty;
                                        pohybrow.UserID = sirow.USER_ID;
                                        pohybrow.TermID = terminal.ID;
                                        pohybrow.guid = sirow.GUID;
                                        pohybrow.Expiration = sirow.IsExpiraceNull() ? (DateTime?)null : sirow.Expirace;
                                        pohybrow.ITEMDESC = string.Empty;   // dotahnout nazev??
                                        pohybrow.CountEntries = sirow.CountEntries;
                                        pohybrow.dateeveS = dtnow;
                                        if (sirow.IsDATEDONENull() || sirow.IsTIMEDONENull())
                                            pohybrow.dateeveS = dtnow;
                                        else
                                            pohybrow.dateeveT = DateTime.ParseExact(sirow.DATEDONE + " " + sirow.TIMEDONE, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);

                                        Classes.Lokace l = new Classes.Lokace();
                                        l.ProcessVydej(pohybrow, new IngresCommand(), conn, trans, new IngresDataAdapter());

                                    }
                                }
                            }

                            #endregion


                            Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej Trancakce...");

                            if (trans != null)
                                trans.Commit();

                            Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Vydej Trancakce OK");
                        }

                    }
				}
			}
			catch (Exception ex)
			{
				Logging.ExceptionHandler2.Handle(LogLevel.Error, "(ProcessVydejState: " + processVydejState.ToString() + ")");
				Logging.ExceptionHandler2.Handle(ex);
				so.SetException(ex);
				so.Write();

				try
				{
					if (trans != null)
						trans.Rollback();

				}
				catch (Exception exTrans)
				{
					Logging.ExceptionHandler2.Handle(LogLevel.Error, "(ProcessVydejState: " + processVydejState.ToString() + ")");
					Logging.ExceptionHandler2.Handle(exTrans);
				}

				throw ex;
			}

			//try
			//{
			//	if (processVydejState == ProcessState.Zpracovat)
			//	{
			//		#region Generovani Faktury

			//		if (Globals.Konfigurace.Vydej[0].ZpracovatDokladDoISABRA)
			//		{

			//			so.Write("Zpracovani Dokladu Do IS ABRA");

			//			if (vydejdata.CZMST_SI.Count > 0)
			//			{
			//				//tady odeslat do ABRY ... 

			//				string vysledek = string.Empty;

			//				if (Database.ABRA.DodaciList_Exists_Process(vydejdata.CZMST_SI[0].SOPNUMBE.Trim()))
			//				{
			//					vysledek = Classes.ABRA.Vydej_Import_ABRA_DL(davka.ID.Value, vydejdata.CZMST_SI[0].SKL_ID.Trim(), vydejdata.CZMST_SI[0].SOPNUMBE.Trim(), string.Empty);
			//				}
			//				else if (Database.ABRA.PrevodVydej_Exists_Process(vydejdata.CZMST_SI[0].SOPNUMBE.Trim()))
			//				{
			//					vysledek = Classes.ABRA.Vydej_Import_ABRA_PRV(davka.ID.Value, vydejdata.CZMST_SI[0].SKL_ID.Trim(), vydejdata.CZMST_SI[0].SOPNUMBE.Trim(), string.Empty);
			//				}
			//				else 
			//				{
			//					Logging.ExceptionHandler2.Handle(LogLevel.Error, "Neznamz doklad pro import:" + vydejdata.CZMST_SI[0].SOPNUMBE);
			//				}


			//				if (vysledek != "OK")
			//				{
			//					throw new Exception(vysledek);
			//				}
			//			}
			//		}

			//		#endregion
			//	}
			//}
			//catch (System.Exception ex)
			//{
			//	Fask.Logging.ExceptionHandler2.Handle(ex);
			//	so.SetException(ex);
			//	so.Write();

			//	#region odmazani
			//	//if (processVydejState == ProcessState.Zpracovat)
			//	//{
			//	//	SqlTransaction transerr = null;
			//	//	SqlConnection connerr = null;

			//	//	try
			//	//	{
			//	//		using (connerr = new SqlConnection(Globals.Konfigurace.ConnectionStrings[0].FASKDB))
			//	//		{
			//	//			connerr.Open();
			//	//			trans = connerr.BeginTransaction();

			//	//			Database.Vydej.UpdateCzDosloByCountEntries((byte)terminal.ID, davka.ID.Value);

			//	//			var si = vydejdata.CZMST_SI.Select(null, null, DataViewRowState.Added);
			//	//			si.ToList().ForEach(x => x.Delete());
			//	//			Database.Vydej.Delete_SI(si, connerr, transerr);

			//	//			var sih = vydejdata.CZMST_SIH.Select(null, null, DataViewRowState.Added);
			//	//			sih.ToList().ForEach(x => x.Delete());
			//	//			Database.Vydej.Delete_SIH(sih, connerr, transerr);

			//	//			if (transerr != null)
			//	//				transerr.Commit();
			//	//		}
			//	//	}
			//	//	catch (Exception exx)
			//	//	{
			//	//		try
			//	//		{
			//	//			if (transerr != null)
			//	//				transerr.Rollback();

			//	//		}
			//	//		catch (Exception exTrans)
			//	//		{
			//	//			Logging.ExceptionHandler2.Handle(LogLevel.Error, "Error transakce mazani pri chybe");
			//	//			Logging.ExceptionHandler2.Handle(exTrans);
			//	//		}
			//	//	}
			//	//}
			//	#endregion

			//	throw ex;
			//}

			so.SetOK();
			return so;
		}

        private void fill_notnull_values_with_defaults(Vydej vydejdata)
        {
			vydejdata.CZMST_SI.ToList().ForEach(r =>
            {
				r.Expirace = r.IsExpiraceNull() ? DateTime.MinValue : r.Expirace;
            });
        }

        private static DataRow[] add_guid_byte_type_and_values(Vydej vydejdata)
        {
			//this.columnGUID = new global::System.Data.DataColumn("GUID", typeof(byte[]), null, global::System.Data.MappingType.Element);
			DataColumn guid_byte_column = new DataColumn("GUID_byte", typeof(byte[]));
			vydejdata.CZMST_SI.Columns.Add(guid_byte_column);
			vydejdata.CZMST_SI.ToList().ForEach(r => r[guid_byte_column] = r.GUID.ToByteArray());
            DataRow[] datarows = vydejdata.CZMST_SI.Select(null, null, DataViewRowState.Added);
			return datarows;
        }

        #endregion

        #region IVydej Members Implementovane ale nic nedelaji...

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

		public DataSet Vydej_DetailPolozka(Item item)
		{
			return null;
		}


		#endregion

		#region IVydej Members Neimplementovano

		public StatusObject TEST_ImportVydejka_Do_IS(int countEntries, int userID, string SKL_ID, string note)
		{
			throw new NotImplementedException();
		}

		public StatusObject TEST_ImportFaktura_Do_IS(int countEntries, int userID, string SKL_ID, string note)
		{
			throw new NotImplementedException();

			#region ABRA code : implement?

			//StatusObject so = new StatusObject();
			//         so.SetOK();

			//         var IS_CREATEINVOICE = Database.ABRA.Get_IS_CREATEINVOICE_By_IDPRV(note.Trim());

			//         return so;

			#endregion
		}

		#endregion

		#region Online metody

		public Fask.Server.Interfaces.DataSets.Vydej_Items_Online Vydej_Online_GetMaterial(string itemnmbr, string skl_id, string serltnum)
		{
			throw new NotImplementedException();

			#region ABRA code : implement?

			//Fask.Server.Interfaces.DataSets.Vydej_Items_Online vydej_Items_Online = new Server.Interfaces.DataSets.Vydej_Items_Online();

			//if (string.IsNullOrEmpty(itemnmbr) && string.IsNullOrEmpty(serltnum))
			//	throw new Exception("Nelze vyhledat bez zadané šarže anebo ID položky!"); 

			//var ds = Database.ABRA.Get_Vydej_Online_GetMaterial(itemnmbr, skl_id, serltnum);

			//if (ds != null && ds.Items.Count > 0)
			//{

			//	foreach (var row in ds.Items)
			//	{
			//		var radek = vydej_Items_Online.Items.NewItemsRow();

			//		radek.Index = row.IndexXX;
			//		radek.Itemnmbr = row.IsItemnmbrNull() ? string.Empty : row.Itemnmbr;
			//		radek.Itemdesc = row.IsItemdescNull() ? string.Empty : row.Itemdesc;
			//		radek.Skl_Id = row.IsSkl_IdNull() ? string.Empty : row.Skl_Id;
			//		radek.Locncode = row.IsLocncodeNull() ? string.Empty : row.Locncode;
			//		radek.Qty = row.Qty;
			//		radek.Serltnum = row.IsSerltnumNull() ? string.Empty : row.Serltnum;

			//		if(!row.IsExpirationNull())
			//		{
			//			radek.Expiration = new DateTime(1899, 12, 30).AddDays(row.Expiration);
			//		}
			//		else
			//		{
			//			radek.SetExpirationNull();
			//		}

			//		if (!row.IsPrijemNull())
			//		{
			//			radek.Prijem = new DateTime(1899, 12, 30).AddDays(row.Prijem);
			//		}
			//		else
			//		{
			//			radek.SetPrijemNull();
			//		}

			//		vydej_Items_Online.Items.AddItemsRow(radek);

			//	}

			//}

			//return vydej_Items_Online; 
			#endregion

		}

		public StatusOverExpirace Vydej_Online_Expirace_Verify(string itemnmbr, string sklad_id, string serltnum, DateTime? expirace)
		{
			throw new NotImplementedException();

			#region ABRA code : implement?

			//Globals.LoadConfiguration();

			//double pocetdnidoexpirace = Globals.Konfigurace.Vydej[0].IsFEFO_FIFO_Expirace_Po_PocetDniNull() ? 30 : Globals.Konfigurace.Vydej[0].FEFO_FIFO_Expirace_Po_PocetDni;

			//// TODO : implementovat 
			//// vratit status expirace, pokud je ji mozne vydat ... 
			//if (!expirace.HasValue)
			//	return new StatusOverExpirace() { State=StatusOverExpiraceState.OK, Message=string.Empty };

			//if (expirace.Value > (DateTime.Now.AddDays(pocetdnidoexpirace)))
			//	return new StatusOverExpirace() { State=StatusOverExpiraceState.OK, Message=string.Empty };

			//if (expirace.Value > DateTime.Now)
			//{

			//	string msg = string.Empty;

			//	msg += string.Format("Šarže bude expirovat dříve než za {0} dní!", pocetdnidoexpirace);

			//	DateTime NowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

			//	double rozdil = (expirace.Value - NowDate).TotalDays;

			//	msg += Environment.NewLine;
			//	msg += string.Format("Expirovat bude za {0} dní!", rozdil);


			//	return new StatusOverExpirace() { State = StatusOverExpiraceState.WARNING, Message = msg};
			//}

			//if (expirace.Value == DateTime.Now)
			//{

			//	string msg = string.Empty;

			//	msg += string.Format("Šarže expiruje dnes!");
			//	return new StatusOverExpirace() { State = StatusOverExpiraceState.WARNING, Message = msg };
			//}

			//return new StatusOverExpirace() { State = StatusOverExpiraceState.WARNING, Message = $"Šarže je již expirovaná !!!" }; 

			#endregion

		}

		public bool Vydej_Online_Expirace_Confirm(byte terminalID, string userID, string password)
		{
			throw new NotImplementedException();

			#region ABRA code : implement?

			//try
			//{

			//             Globals.LoadConfiguration();

			//             using (SqlConnection con = new SqlConnection(Globals.Konfigurace.ConnectionStrings[0].FASKDB))
			//             using (SqlCommand comm = new SqlCommand("CZMST_Verify_Operation", con))
			//             {
			//                 comm.CommandType = CommandType.StoredProcedure;

			//                 comm.Parameters.Add(new SqlParameter() { ParameterName = "@operation", DbType = DbType.String, Value = "expirace" });
			//                 comm.Parameters.Add(new SqlParameter() { ParameterName = "@pwdhash", DbType = DbType.String, Value = password });
			//                 comm.Parameters.Add(new SqlParameter() { ParameterName = "@verified", DbType = DbType.Boolean, Direction = ParameterDirection.Output });

			//                 comm.Connection.Open();
			//                 comm.ExecuteNonQuery();

			//                 object verified = ((IDbDataParameter)(comm.Parameters["@verified"])).Value;

			//                 return Convert.ToBoolean(verified);
			//             }
			//         }
			//         catch (System.Exception ex)
			//         {
			//             Fask.Logging.ExceptionHandler2.Handle(ex);
			//	throw ex;
			//         }

			//     }

			#endregion

			#endregion

		}

        public StatusInfo Vydej_GenerateData_CZMST094(Objednavka objednavka, Sklad sklad)
        {
            throw new NotImplementedException();
        }
    }
}
