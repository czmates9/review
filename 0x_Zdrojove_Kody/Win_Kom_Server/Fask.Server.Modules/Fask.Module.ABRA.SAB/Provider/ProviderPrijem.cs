using Fask.DataSets;
using Fask.Logging;
using Fask.Server.Interfaces.Classes;
using Fask.Server.Interfaces.DataSets;
using Fask.Server.Interfaces.Prijem;
using Fask.Tracing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Provider
{
    public partial class Provider : Server.Interfaces.Prijem.IPrijem
    {
        #region Implementovane

        public Fask.DataSets.PrijemDavky Prijem_GetPrijemky(Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item)
        {
            // \TODO : seznam objednavek vydanych z sql serveru ... 
            try
            {

                string select =
                    " select A.countentries, A.ponumber, B.CntItems CntItems, C.qtyshppdsum SumItems " +
                    " from " + Constants.Common.TABLE_CZMST_PE + " A " +
                    " INNER JOIN " +
                    " ( " +
                    "	select X.countentries, X.ponumber, count(X.itemnmbr) CntItems " +
                    "	from ( " +
                    "		Select countentries, ponumber, itemnmbr " +
                    "		from " + Constants.Common.TABLE_CZMST_PE + " " +
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
                    "	from " + Constants.Common.TABLE_CZMST_PE + " " +
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

                Fask.DataSets.PrijemDavky dsV = new Fask.DataSets.PrijemDavky();
                Database.Prijem.Fill_DataSet(select, dsV, dsV.Hlavicky.TableName);
                dsV.AcceptChanges();

                return dsV;
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

            string selectCount = "SELECT Count(CountEntries) as davka FROM " + Constants.Common.TABLE_CZMST_PE + " where CountEntries=" + davka.ID.Value + " AND (CZ_Doslo<=0 OR CZ_Doslo=" + terminal.ID + ")";
            string update = "Update " + Constants.Common.TABLE_CZMST_PE + " set CZ_Doslo=" + terminal.ID + " where countentries=" + davka.ID.Value;
            System.Data.SqlClient.SqlConnection xconn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
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

                string select = "SELECT * FROM " + Constants.Common.TABLE_CZMST_PE + " where CountEntries=" + davka.ID.Value + " order by dex_row_id";
                //string select = "SELECT *, 0 as Nasnimano FROM " + Constants.Common.TABLE_CZMST_PE + " where CountEntries=" + davka.ID.Value + " order by dex_row_id";
                Database.Prijem.Fill_DataSet(select, prijem, prijem.CZMST_PE.TableName);

                select = "SELECT * FROM " + Constants.Common.TABLE_CZMST_PI + " where CountEntries=" + davka.ID.Value + " order by dex_row_id";
                Database.Prijem.Fill_DataSet(select, prijem, prijem.CZMST_PI.TableName);

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(prijem);
                throw ex;
            }

            return prijem;
        }

        public StatusObject Prijem_Storno_Prijemka(Davka davka, Server.Interfaces.Classes.Terminal terminal, string password)
        {
            StatusObject so = new StatusObject();


            if (password.Trim() != Globals_V1.Konfigurace.Prijem[0].HesloStornoPrijemka.Trim())
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

        public Fask.Server.Interfaces.DataSets.Obecne Prijem_GetPrijemky_External(Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item, List<string> ListCarKod)
        {
            //Generovat 
            Fask.Server.Interfaces.DataSets.Obecne ds_obecne = new Fask.Server.Interfaces.DataSets.Obecne();
            Fask.DataSets.Prijem ds_prijem = new Fask.DataSets.Prijem();

            try
            {
                string select = "SELECT * FROM " + Constants.Common.TABLE_CZMST_PE + " where cz_doslo <= 100"; // jen pripravene nebo stazene se budou vylucovat
                Database.Prijem.Fill_DataSet(select, ds_prijem, ds_prijem.CZMST_PE.TableName);

                ds_obecne = Database.ABRA.Prijemka_GetDavkyByCarKody(terminal.ID, sklad.ID, String.Join(",", ListCarKod.ToArray()));

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

        public Fask.Server.Interfaces.Classes.StatusInfo Prijem_GenerateDavka(Fask.Server.Interfaces.Classes.Objednavka objednavka, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            Globals_V1.LoadConfiguration();
            Fask.Server.Interfaces.Classes.StatusInfo si = new Fask.Server.Interfaces.Classes.StatusInfo();
            string result = string.Empty;


            byte? czdoslo = Database.Prijem.CZMSTPE_PONUMBER_CZDOSLO(objednavka.ID);
            // 1) test na rozpracovany doklad 
            if (czdoslo.HasValue && czdoslo.Value > 0)
            { //existuje a je stazene v terminalu => vrati chybu ... nelze vytvorit duplicitu ...
                si.ID = -1;
                si.Description = "Existuje rozpracovaná dávka pro doklad '" + objednavka.ID + "' na terminálu č.:" + czdoslo.Value;
                si.InnerException = new Exception(si.Description);
                Logging.ExceptionHandler2.Handle(LogLevel.Error, si.Description);
                return si;
            }

            // 2) test zda je to objednavka vydana
            if (Database.ABRA.Prijemka_Exists(objednavka.ID))
            {
                string stav = Classes.ABRA.ExportPrijemka_ABRA_Z_PR(objednavka, sklad);

                string ID_DL = Database.ABRA.Get_ID_PR(objednavka.ID, sklad.ID);

                stav = Classes.ABRA.Edit_Stav_PR(Globals_V1.Konfigurace.Procesni_Rizeni[0].StavID_Prijem_NaskladnujeSe.Trim(), ID_DL);


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

        public StatusObject Prijem_Process(Davka davka, Server.Interfaces.Classes.Terminal terminal, Sklad sklad, Item item, Prijem prijemdata, ProcessState processPrijemState)
        {
            string pom = string.Empty;
            pom = Globals_V1.LoadConfiguration();
            if (pom != "OK")
                throw new Exception("Prijem_Process, chyba nacteni konfigurace: " + pom);

            TracId tracid = new TracId(null, terminal.ID, davka.ID.Value);

            string guidDavka = string.Empty;
            if (prijemdata.CZMST_PEH.Count > 0)
                guidDavka = prijemdata.CZMST_PEH[0].GUID.ToString();
            else
                guidDavka = Guid.NewGuid().ToString();

            string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals_V1.Konfigurace.Prijem[0].StatusObjectsDirectory, guidDavka));

            //StatusObject so = new StatusObject(filePath);
            Fask.Server.Interfaces.Classes.StatusObject so = Fask.Server.Interfaces.Classes.StatusObject.Load(filePath);
            if (so == null) //neexistuje => vytvorit a pokracovat
            {
                so = new Fask.Server.Interfaces.Classes.StatusObject(filePath);
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

            so.StatusText = "Příjem zpracování...";

            SqlTransaction trans = null;
            SqlConnection conn = null;

            if (prijemdata == null)
                return so;

            prijemdata.AcceptChanges();

            try
            {
                using (conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    if (processPrijemState == Fask.Server.Interfaces.Prijem.ProcessState.Uvolnit) //uvolnit davku
                    {
                        Database.Prijem.UpdateCzDosloByCountEntries(conn, trans, 0, davka.ID.Value);
                    }
                    else
                    {
                        byte terminalid = (byte)terminal.ID;

                        int terminalid100 = terminal.ID + 100;

                        bool allowInsertData = Database.Prijem.PI_AllowInsert(davka.ID, terminalid);

                        if (allowInsertData)
                        {
                            so.Write("PE zmena CZDoslo v DB.");
                            Database.Prijem.UpdateCzDosloByCountEntries(conn, trans, Convert.ToByte(terminalid100), davka.ID.Value);

                            so.Write("PI zmena na ADD.");

                            prijemdata.CZMST_PI.ToList().ForEach(x => x.SetAdded());
                            //foreach (Fask.DataSets.Prijem.CZMST_PIRow sirow in prijemdata.CZMST_PI)
                            //{
                            //    sirow.SetAdded();
                            //}

                            so.Write("PI Update");
                            var pi = prijemdata.CZMST_PI.Select(null, null, DataViewRowState.Added);

                            Database.Prijem.Update_CZMST_PI(pi, conn, trans);
                            
                            #region lokace

                            if (prijemdata.Parametry.Count > 0 && !prijemdata.Parametry.First().IsCONFIG_LOKACE_POVOLITNull() && prijemdata.Parametry.First().CONFIG_LOKACE_POVOLIT)
                            {
                                foreach (Fask.DataSets.Prijem.CZMST_PIRow pirow in prijemdata.CZMST_PI)
                                {
                                    int guidcount = Database.Lokace.Count_STAVPOHYB(conn, pirow.GUID);

                                    if (guidcount % 2 == 0)
                                    {
                                        DateTime dtnow = DateTime.Now;
                                        Fask.Server.Interfaces.Lokace.LokacePohyb pohybrow = new Fask.Server.Interfaces.Lokace.LokacePohyb();
                                        pohybrow.ITEMNMBR = pirow.ITEMNMBR;
                                        pohybrow.DOCUMENT_NUMBER = pirow.PONUMBER;
                                        pohybrow.POHYB_TYPE = Fask.Server.Interfaces.Lokace.TypeOfRecord.P;
                                        pohybrow.POHYB_SRC = "P";
                                        pohybrow.SOURCE = "S";      // doplneni ze serveru ...
                                        pohybrow.QTYSHPPD = pirow.QTYSHPPD;
                                        pohybrow.SERLTNUM = pirow.SERLTNUM;
                                        pohybrow.SKL_ID_SRC = pirow.IsSKL_IDNull() ? string.Empty : pirow.SKL_ID;
                                        pohybrow.SKL_ID_DST = string.Empty;
                                        pohybrow.LOCNCODE_SRC = pirow.IsLOCNCODENull() ? string.Empty : pirow.LOCNCODE;
                                        pohybrow.LOCNCODE_DST = string.Empty;
                                        pohybrow.UserID = pirow.USER_ID;
                                        pohybrow.TermID = terminal.ID;
                                        pohybrow.guid = pirow.GUID;
                                        pohybrow.Expiration = pirow.IsExpiraceNull() ? (DateTime?)null : pirow.Expirace;
                                        pohybrow.ITEMDESC = string.Empty;   // dotahnout nazev??
                                        pohybrow.CountEntries = pirow.CountEntries;
                                        pohybrow.dateeveS = dtnow;
                                        if (pirow.IsDATEDONENull() || pirow.IsTIMEDONENull())
                                            pohybrow.dateeveS = dtnow;
                                        else
                                            pohybrow.dateeveT = DateTime.ParseExact(pirow.DATEDONE + " " + pirow.TIMEDONE, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);

                                        Classes.Lokace l = new Classes.Lokace();
                                        l.ProcessPrijem(pohybrow, new SqlCommand(), conn, trans, new SqlDataAdapter());

                                    }
                                }
                            }

                            #endregion


                            Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Prijem Trancakce...");

                            if (trans != null)
                                trans.Commit();

                            Logging.ExceptionHandler2.Handle(LogLevel.Debug, "Prijem Trancakce OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // StatusObject
                so.SetException(ex);
                so.Finished = true;

                if (trans != null)
                    trans.Rollback();

                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "(ProcessPrijemState: '" + processPrijemState.ToString() + "', davka:'" + davka.ID.Value + "')");
                Fask.Logging.ExceptionHandler2.Handle(ex);

                throw ex;
            }

            try
            {
                #region Generovani přijemky

                if (processPrijemState == ProcessState.Zpracovat)
                {

                    if (Globals_V1.Konfigurace.Prijem[0].ZpracovatDokladDoISABRA)
                    {

                        so.Write("Zpracovani Dokladu Do IS ABRA");

                        if (prijemdata.CZMST_PI.Count > 0)
                        {
                            //tady odeslat do ABRY ... 

                            string vysledek = Classes.ABRA.Prijem_Import_ABRA(davka.ID.Value, prijemdata.CZMST_PI[0].SKL_ID.Trim(), prijemdata.CZMST_PI[0].PONUMBER.Trim(), string.Empty);

                            if (vysledek != "OK")
                            {
                                throw new Exception(vysledek);
                            }
                        }
                    }
                }

                #endregion
            }
            catch (System.Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                so.SetException(ex);
                so.Write();

                #region odmazani
                //SqlTransaction transerr = null;
                //SqlConnection connerr = null;

                //try
                //{
                //    using (connerr = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                //    {
                //        connerr.Open();
                //        transerr = connerr.BeginTransaction();

                //        var pi = prijemdata.CZMST_PI.Select(null, null, DataViewRowState.Added);
                //        pi.ToList().ForEach(x => x.Delete());
                //        Database.Prijem.Delete_PI(pi, connerr, transerr);


                //        if (transerr != null)
                //            transerr.Commit();
                //    }
                //}
                //catch (Exception exx)
                //{
                //    try
                //    {
                //        if (transerr != null)
                //            transerr.Rollback();

                //    }
                //    catch (Exception exTrans)
                //    {
                //        Logging.ExceptionHandler2.Handle(LogLevel.Error, "Error transakce mazani pri chybe");
                //        Logging.ExceptionHandler2.Handle(exTrans);
                //    }
                //}
                #endregion

                throw ex;
            }

            so.SetOK();
            so.Write();
            return so;


        }

        public string TEST_ImportPrijem_Do_IS(int countEntries, string SKL_ID, string PONUMBER, string note)
        {
            try
            {
                #region Generovani přijemky

                if (Globals_V1.Konfigurace.Prijem[0].ZpracovatDokladDoISABRA)
                {
                    try
                    {

                        string vysledek = Classes.ABRA.Prijem_Import_ABRA(countEntries, SKL_ID.Trim(), PONUMBER.Trim(), note);

                        return vysledek;
                    }
                    catch (Exception ex)
                    {
                        Logging.ExceptionHandler2.Handle(ex);
                        throw ex;
                    }

                }
                else
                {
                    return "Import do IS ABRA neni povolen...";
                }

                #endregion
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return "OK";
        }

        #endregion

        #region Implementovano ale nic nedela

        public bool Prijem_AfterProcessedAction(Davka davka)
        {
            return true;
        }

        public DataSet Prijem_DetailDavka(Davka davka)
        {
            return null;
        }

        #endregion

        #region NEimplementovane

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

        public DataSet Prijem_Detail(Objednavka objednavka, Sklad sklad)
        {
            throw new NotImplementedException();
        }

        public DataSet Prijem_Detail_Polozka(Objednavka objednavka, Sklad sklad, Item polozka)
        {
            throw new NotImplementedException();
        }

        public bool Prijem_GetSkladExpedice(string ITEMNMBR, decimal MnozstviZadane, decimal MnozstviNasnimane, out decimal MnozstviDodavatelePozadovano, out decimal MnozstviDodavateleDodano, out decimal MnozstviDodavateleDodat, out decimal MnozstviOdberateliPozadovano, out decimal MnozstviOdberatelumDodano, out decimal MnozstviOdberatelumDodat, out decimal Vysledek)
        {
            throw new NotImplementedException();
        }

        public StatusObject Prijem_Online_Add(Davka davka, Server.Interfaces.Classes.Terminal terminal, Prijem prijemRows)
        {
            throw new NotImplementedException();
        }

        public StatusObject Prijem_Online_Del(Davka davka, Server.Interfaces.Classes.Terminal terminal, Prijem prijemRows)
        {
            throw new NotImplementedException();
        }

        public StatusOverLokace Prijem_Online_OverLokace(string serltnum, string itemnmbr, string locncode, decimal qtyshppd, string skl_id)
        {
            throw new NotImplementedException();
        }

        public StatusObject Prijem_Online_Quantity(Davka davka, Server.Interfaces.Classes.Terminal terminal, ref Prijem prijemRows)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
