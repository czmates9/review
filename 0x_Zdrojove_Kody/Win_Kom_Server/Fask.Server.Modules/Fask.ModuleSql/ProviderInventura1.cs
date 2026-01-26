using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Fask.Server.Interfaces.Classes;
using System.IO;

namespace Fask.ModuleSql
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část Inventura1.
	/// </summary>
    class ProviderInventura1 : Fask.Server.Interfaces.Inventura1.IInventura1
    {
		#region Nazvy tabulek + cesta k params souboru

		private string TABLE_CZMST_I1 = "CZMST_I1";
		private string TABLE_CZMST_I2 = "CZMST_I2";
		private string TABLE_CZMST_I3 = "CZMST_I3";
		private string TABLE_CZMST_I4 = "CZMST_I4";

		#endregion


        #region IInventura Members

		/// <summary>
		/// Metoda která vrací hlavičky inventury
		/// </summary>
		/// <param name="terminal">Terminal</param>
		/// <param name="sklad">Sklad</param>
		/// <returns>Dataset Inventury1 naplnen datma</returns>
        public Fask.DataSets.Inventury1 Inventura_GetInventury(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            try
            {
                Globals.LoadConfiguration();
                //Vrati seznam davek inventury, ktere jsou volne pro stazeni nebo jsou stazene terminalem, ktery zada o stazeni
                string select = string.Empty;
                if (Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
                {
                    select = "SELECT distinct CountEntries FROM " + 
                        TABLE_CZMST_I1 + 
                        " WHERE TerminalID < 100 "+
                        (!String.IsNullOrEmpty(sklad.ID) ? (" AND SKL_ID Like '" + sklad.ID + "%'") : "") + //filtr na sklad s like
                        " order by countentries";
                }
                else
                {
                    select = "SELECT distinct CountEntries FROM " + 
                        TABLE_CZMST_I1 + 
                        " WHERE TerminalID <= 0 or TerminalID=" + terminal.ID +
                        (!String.IsNullOrEmpty(sklad.ID) ? (" AND SKL_ID Like '" + sklad.ID + "%'") : "") + //filtr na sklad s like
                        " order by countentries";
                }
                //Properties.Settings.Default.SqlProviderConnection
                System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(select, Globals.Konfigurace.ConnectionString[0].FASKDB);


                Fask.DataSets.Inventury1 inventury = new Fask.DataSets.Inventury1();
                dataAdapter.Fill(inventury, inventury.Hlavicky.TableName);

                inventury.AcceptChanges();

                return inventury;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// Metoda pro dotažení dat inventury pro Terminal
		/// </summary>
		/// <param name="davka">Dávka</param>
		/// <param name="terminal">Terminal</param>
		/// <returns>Dataset Inventura1 s naplnenima datama</returns>
        public Fask.DataSets.Inventura1 Inventura_GetInventura(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal)
        {
            try
            {
                Globals.LoadConfiguration();
                Fask.DataSets.Inventura1 inventura = new Fask.DataSets.Inventura1();

                string select1 = "SELECT * FROM " + TABLE_CZMST_I1 + " where CountEntries=" + davka.ID + " order by dex_row_id";
                string select2 = "SELECT * FROM " + TABLE_CZMST_I2 + " where CountEntries=" + davka.ID + " order by dex_row_id";
                string select3 = "SELECT * FROM " + TABLE_CZMST_I3 + " where CountEntries=" + davka.ID + " order by dex_row_id";
                string select4 = "SELECT * FROM " + TABLE_CZMST_I4 + " where CountEntries=" + davka.ID + " order by dex_row_id";


                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select1, Globals.Konfigurace.ConnectionString[0].FASKDB))
                {
                    sql.Fill(inventura, inventura.CZMST_I1.TableName);

                }

                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select2, Globals.Konfigurace.ConnectionString[0].FASKDB))
                {
                    sql.Fill(inventura, inventura.CZMST_I2.TableName);
                }

                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select3, Globals.Konfigurace.ConnectionString[0].FASKDB))
                {
                    sql.Fill(inventura, inventura.CZMST_I3.TableName);
                }

                //StringReader sr = new StringReader(inventuraParamsXMLPath);
                //inventura.ReadXml(sr);
                //inventura.AcceptChanges();

                return inventura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// Metoda pro označení stažené inventury
		/// </summary>
		/// <param name="davka">Davka</param>
		/// <param name="terminal">Terminal</param>
		/// <returns>True-OK, False- Chyba</returns>
        public bool Inventura_GetInventuraReceived(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal)
        {
            Globals.LoadConfiguration();
            string selectCount = "SELECT Count(CountEntries) as davka FROM " + TABLE_CZMST_I1 + " where CountEntries=" + davka.ID + " AND (TerminalID<=0 or TerminalID=" + terminal.ID + ")";
            string update = "Update " + TABLE_CZMST_I1 + " set TerminalID=" + terminal.ID + " where countentries=" + davka.ID;


            System.Data.SqlClient.SqlConnection sql = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(selectCount, sql);
            System.Data.SqlClient.SqlCommand commandUpdate = new System.Data.SqlClient.SqlCommand(update, sql);
            SqlTransaction itrans = null;

            int res = 0;
            try
            {
                sql.Open();
                itrans = sql.BeginTransaction(IsolationLevel.Serializable);

                command.Transaction = itrans;
                object r = command.ExecuteScalar();
                if (r == null)
                    throw new Exception("Dávka nenalezena.");

                if (!Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
                {
                    if (r is int && ((int)r) <= 0)
                        throw new Exception("Dávka se již zpracovává.");
                }


                commandUpdate.Transaction = itrans;
                res = commandUpdate.ExecuteNonQuery();
                if (itrans != null)
                    itrans.Commit();

            }
            catch (Exception ex)
            {
                if (itrans != null)
                    itrans.Rollback();

                throw ex;
            }
            finally
            {
                if (sql.State == ConnectionState.Open)
                    sql.Close();
            }

            return (res > 0);
        }

		/// <summary>
		/// Metoda pro zpracovaní dat na serveru do SQL a IS
		/// </summary>
		/// <param name="davka">Dávka</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="inventuradata">Data pro zpracovaní</param>
		/// <param name="processInventuraState">příznak, co se ma s datama udelat</param>
		/// <returns>StatusObject - Nese informace o stavu</returns>
        public StatusObject Inventura_Process(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.DataSets.Inventura1 inventuradata, Fask.Server.Interfaces.Inventura1.ProcessState processInventuraState)
        {
            Globals.LoadConfiguration();

            SqlTransaction iTrans1 = null;

            string guidDavka = inventuradata.CZMST_IH[0].GUID.ToString();
            string filePath = Path.Combine(Globals.Konfigurace.Inventura[0].PathStateDataFile, guidDavka);

            StatusObject so = new StatusObject(filePath);

            bool uvolnitdavku = processInventuraState == Fask.Server.Interfaces.Inventura1.ProcessState.Uvolnit;

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);


            try
            {
                so.Write("vytvareni connection");
                conn.Open();

                if (uvolnitdavku) //uvolnit davku
                {
                    so.Write("uvolnit davku");

                    string updateuvolnit = "Update " + TABLE_CZMST_I1 + " set TerminalID=0 where CountEntries=" + davka.ID;
                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(updateuvolnit, conn, iTrans1);
                    int rows = comm.ExecuteNonQuery();
                }
                else //zapsat davku
                {
                    so.Write("zapsat davku");
                    bool allowInsertData = true;
                    if (!Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
                    {
                        //Test zda je mozne data pridat, jestlize jiz existuji, tak nepridat. 
                        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand("Select Count(*) as number from " + TABLE_CZMST_I4 + " where countentries=" + davka.ID, conn);
                        object datacount = comm.ExecuteScalar();
                        if (datacount != null && ((int)datacount) > 0)
                            allowInsertData = false;
                    }

                    if (allowInsertData)
                    {
                        so.Write("Update databaze");

                        iTrans1 = conn.BeginTransaction();

                        if (inventuradata.CZMST_I4.Count > 0)
                        {
                            // kontrola na duplicitu guid prvniho a posledniho zaznamu
                            string guidTest = "select COUNT(*) from " + TABLE_CZMST_I4 + " where GUID='" + inventuradata.CZMST_I4.First().GUID.ToString() + "'";
                            if (inventuradata.CZMST_I4.Count > 1)
                            {
                                guidTest += " OR GUID='" + inventuradata.CZMST_I4.Last().GUID.ToString() + "'";
                            }

                            System.Data.SqlClient.SqlCommand testguidcommand = new SqlCommand(guidTest, conn, iTrans1);
                            int guidcount = (int)testguidcommand.ExecuteScalar();

                            if (guidcount == 0)
                            {
                                // guidy nejsou v DB, je mozne ulozit data
                                SQL_Datasets.Inventura1TableAdapters.CZMST_I4TableAdapter i4ta = new Fask.ModuleSql.SQL_Datasets.Inventura1TableAdapters.CZMST_I4TableAdapter();
                                i4ta.Connection = conn;
                                i4ta.Transaction = iTrans1;
                                i4ta.Update(inventuradata.CZMST_I4.Select(null, null, DataViewRowState.Added));
                            }
                            else
                            {
                                // jeden z guidu je jiz v DB, zalogovat ...
                                string errortext = "Davka:" + davka.ID + ",GUID:" + inventuradata.CZMST_I4.First().GUID.ToString();
                                if (inventuradata.CZMST_I4.Count > 1)
                                    errortext += " nebo " + inventuradata.CZMST_I4.Last().GUID.ToString();

                                errortext += "jiz je v databazi.";
								Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, errortext);
                            }
                        }

                        string updatei1 = "Update " + TABLE_CZMST_I1 + " set TerminalID=" + (terminal.ID + 100) + " where CountEntries=" + davka.ID;
                        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatei1, conn, iTrans1);

                        //iTrans1 = conn.BeginTransaction();
                        //string updatei1 = "Update " + TABLE_CZMST_I1 + " set TerminalID=" + (terminal.ID + 100) + " where CountEntries=" + davka.ID;
                        //System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatei1, conn, iTrans1);

                        //SQL_Datasets.Inventura1TableAdapters.CZMST_I4TableAdapter i4ta = new Fask.ModuleSql.SQL_Datasets.Inventura1TableAdapters.CZMST_I4TableAdapter();
                        //i4ta.Connection = conn;
                        //i4ta.Transaction = iTrans1;
                        //i4ta.Update(inventuradata.CZMST_I4.Select(null, null, DataViewRowState.Added));

                        /*
                        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();

                        da.InsertCommand = CreateInsertCommand(conn);
                        da.SelectCommand = CreateSelectCommand(conn);

                        CreateI4AdapterTableMaping(da);

                        da.InsertCommand.Transaction = iTrans1;
                        da.SelectCommand.Transaction = iTrans1;

                        da.Update(inventuradata.CZMST_I4.Select(null, null, DataViewRowState.Added));
                        */
 
                        if (!Globals.Konfigurace.Inventura[0].DavkaTerminalVice)
                        {
                            int rows = command.ExecuteNonQuery();
                        }

                    }
                }


                so.Write("commit transakce");

                if (iTrans1 != null)
                    iTrans1.Commit();
            }
            catch (Exception ex)
            {
                if (iTrans1 != null)
                    iTrans1.Rollback();

                so.Exception = true;
                so.Write(ex.Message);

                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                    conn.Close();
            }

            #region Action after data processed
            if (Globals.Konfigurace.Inventura[0].AfterDataProcessed_Action_Asynchronous)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Inventura_AfterProcessedActionAsync));
                thread.Start(davka);
            }
            else
            {
                if (!Inventura_AfterProcessedAction(davka))
                {
                    so.Exception = true;
                    so.Write("chyba");
                    return so;
                }
            }
            #endregion


            so.SetOK();

            return so;

        }

		/// <summary>
		/// Metoda která se volá po zprocesovaní dat pra asynchonne 
		/// </summary>
		/// <param name="davka">Davka</param>
        private void Inventura_AfterProcessedActionAsync(object davka)
        {
            try
            {
                Fask.Server.Interfaces.Classes.Davka d = (Fask.Server.Interfaces.Classes.Davka)davka;
                Inventura_AfterProcessedAction(d);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// Metoda která se volá po zprocesovaní dat
		/// </summary>
		/// <param name="davka">Dávka</param>
		/// <returns>True-OK, False- chyba</returns>
        public bool Inventura_AfterProcessedAction(Fask.Server.Interfaces.Classes.Davka davka)
        {
            try
            {
                Globals.LoadConfiguration();
                string aDP_Action = Globals.Konfigurace.Inventura[0].AfterDataProcessed_Action;
                string aDP_Action_P1 = Globals.Konfigurace.Inventura[0].AfterDataProcessed_Action_P1;
                string aDP_Action_P2 = Globals.Konfigurace.Inventura[0].AfterDataProcessed_Action_P2;
                if (aDP_Action.Length != 0)
                {
                    Routines.AfterProcessAction.Execute(Globals.Konfigurace.ConnectionString[0].FASKDB, TABLE_CZMST_I4, (int)davka.ID, aDP_Action, aDP_Action_P1, aDP_Action_P2, 120);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// Metoda pro online kontrolu položky a označeni
		/// </summary>
		/// <param name="davka">Dávka</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="o_terminalid">reference na O_TID</param>
		/// <returns>True-OK, False-chyba</returns>
        public bool Inventura_OnlineUnCheckState(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, string itemnmbr, out byte o_terminalid)
        {
            System.Data.SqlClient.SqlConnection xconnection = null;
            System.Data.SqlClient.SqlCommand xcommand = null;
            try
            {
                Globals.LoadConfiguration();
                xconnection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                xcommand = new System.Data.SqlClient.SqlCommand();

                xcommand.Connection = xconnection;
                xcommand.CommandText = "select * from " + TABLE_CZMST_I1 + " where countentries=" + davka.ID + " and itemnmbr='" + itemnmbr + "'";

                xconnection.Open();
                xcommand.Transaction = xcommand.Connection.BeginTransaction(IsolationLevel.Serializable);

                Fask.DataSets.Inventura1 dsinv1 = new Fask.DataSets.Inventura1();

                IDataReader ireader = xcommand.ExecuteReader();
                try
                {
                    if (ireader.Read()) //existuje zaznam =
                    {
                        object o_tid = ireader["O_TID"];
                        if (o_tid == null || o_tid is System.DBNull)
                        { //neni nastaveno => povolit odmaz
                            o_terminalid = (byte)terminal.ID;
                            return true; //nemusim dale pokracovat, protoze O_TID v db je NULL ...
                        }
                        else
                        { //je nastaveno 
                            o_terminalid = Convert.ToByte(o_tid);
                            // je nastaveno timto terminalem => povolit odmaz
                            // neni nastaveno timto terminalem => nepovolit odmaz
                            if (Convert.ToByte(o_tid) != (byte)terminal.ID)
                                return false;
                        }
                    }
                    else //zaznam neexistuje => nelze overit
                    {
                        o_terminalid = 0;
                        return false;
                    }

                }
                finally
                {
                    if (ireader != null && !ireader.IsClosed)
                        ireader.Close();
                }

                xcommand.CommandText = "Update " + TABLE_CZMST_I1 + " set O_TID=NULL where countentries=" + davka.ID + " and itemnmbr='" + itemnmbr + "'";
                int raff = xcommand.ExecuteNonQuery();

                xcommand.Transaction.Commit();

                return true;

            }
            catch (Exception ex)
            {
                //Log.writeErrorLog(ex.Message);
                if (xcommand != null && xcommand.Transaction != null)
                {
                    xcommand.Transaction.Rollback();
                }
                o_terminalid = 0;
                throw ex;
            }
            finally
            {
                if (xconnection != null && xconnection.State == ConnectionState.Open)
                {
                    xconnection.Close();
                    xconnection.Dispose();
                    xconnection = null;
                }
            }
        }

		/// <summary>
		/// Metoda pro online kontrolu položky a odznačeni
		/// </summary>
		/// <param name="countentries">číslo dávky</param>
		/// <param name="terminal">Terminal</param>
		/// <param name="itemnmbr">ID položky</param>
		/// <param name="o_terminalid">reference na O_TID</param>
		/// <returns> True-OK, False-chzba </returns>
        public bool Inventura_OnlineCheckState(Fask.Server.Interfaces.Classes.Davka countentries, Fask.Server.Interfaces.Classes.Terminal terminal, string itemnmbr, out byte o_terminalid)
        {
            System.Data.SqlClient.SqlConnection xconnection = null;
            System.Data.SqlClient.SqlCommand xcommand = null;
            try
            {
                Globals.LoadConfiguration();
                xconnection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                xcommand = new System.Data.SqlClient.SqlCommand();

                xcommand.Connection = xconnection;
                xcommand.CommandText = "select * from " + TABLE_CZMST_I1 + " where countentries=" + countentries + " and itemnmbr='" + itemnmbr + "'";

                xconnection.Open();
                xcommand.Transaction = xcommand.Connection.BeginTransaction(IsolationLevel.Serializable);

                Fask.DataSets.Inventura1 dsinv1 = new Fask.DataSets.Inventura1();

                IDataReader ireader = xcommand.ExecuteReader();
                try
                {
                    if (ireader.Read()) //existuje zaznam =
                    {
                        object o_tid = ireader["O_TID"];
                        if (o_tid == null || o_tid is System.DBNull)
                        { //neni nastaveno => nastavit
                            o_terminalid = (byte)terminal.ID;
                        }
                        else if (Convert.ToByte(o_tid) == 0) //nebylo nastaveno terminalem
                        {
                            //pokracuje dale nastavenim ...
                            o_terminalid = (byte)terminal.ID;
                        }
                        else
                        { //je nastaveno 
                            o_terminalid = Convert.ToByte(o_tid);
                            // je nastaveno timto terminalem => povolit zapis
                            // neni nastaveno timto terminalem => nepovolit zapis
                            if (Convert.ToByte(o_tid) == (byte)terminal.ID)
                                return true; //nemusim dale pokracovat, protoze jiz bylo nastaveno...
                            else
                                return false;
                        }
                    }
                    else //zaznam neexistuje => nelze overit
                    {
                        o_terminalid = 0;
                        return false;
                    }

                }
                finally
                {
                    if (ireader != null && !ireader.IsClosed)
                        ireader.Close();
                }
                xcommand.CommandText = "Update " + TABLE_CZMST_I1 + " set O_TID=" + o_terminalid + " where countentries=" + countentries + " and itemnmbr='" + itemnmbr + "'";
                int raff = xcommand.ExecuteNonQuery();

                xcommand.Transaction.Commit();

                return true;

            }
            catch (Exception ex)
            {
                if (xcommand != null && xcommand.Transaction != null)
                {
                    xcommand.Transaction.Rollback();
                }
                o_terminalid = 0;
                throw ex;
            }
            finally
            {
                if (xconnection != null && xconnection.State == ConnectionState.Open)
                {
                    xconnection.Close();
                    xconnection.Dispose();
                    xconnection = null;
                }
            }
        }

        #endregion

    }
}
