using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Fask.Server.Interfaces.Classes;
using System.IO;

namespace Fask.ModuleSql
{
	/// <summary>
	/// Trida Provider pro Modul SQL, v které jsou implementovany metody z Interface. Část Příjem.
	/// </summary>
    public partial class Provider : Fask.Server.Interfaces.Prijem.IPrijem
    {
        private string TABLE_CZMST_PE = "CZMST_PE";
        private string TABLE_CZMST_PI = "CZMST_PI";
		private string TABLE_CZMST_PE_SN = "CZMST_PE_SN";

        //private string prijemParamsXMLPath =
        //    (new Uri(System.IO.Path.Combine(
        //        System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
        //     Properties.Settings.Default.prijemParamsXMLPath))).LocalPath;
        //private string prijemParamsXMLPath = MyPath.Path.PrijemParams;
        /*
        private SqlCommand CreateInsertCommand(SqlConnection con)
        {
            // Create the InsertCommand.
            SqlCommand command = new SqlCommand(
              @"INSERT INTO " + TABLE_CZMST_PI +
                                                     " (CountEntries, PONUMBER, ORD, ITEMNMBR, VNDDOCNM, VNDITNUM, LOCNCODE, QTYSHPPD, QTYPACK, SERLTNUM, KOD_SW, DAT_VYROBY, DATEDONE, TIMEDONE, CZ_CarKod, REZ_1, REZ_2, USER_ID" +
                                                     (Properties.Settings.Default.DexRowIdInsert ? ", DEX_ROW_ID" : "") +
                                                     ", GUID, INPUT_MODE, ID_TERMINAL " +
                                                     ") VALUES (@CountEntries, @PONUMBER, @ORD, @ITEMNMBR, @VNDDOCNM, @VNDITNUM, @LOCNCODE, @QTYSHPPD, @QTYPACK, @SERLTNUM, @KOD_SW, @DAT_VYROBY, @DATEDONE, @TIMEDONE, @CZ_CarKod, @REZ_1, @REZ_2, @USER_ID" +
                                                     (Properties.Settings.Default.DexRowIdInsert ? ", @DEX_ROW_ID" : "") +
                                                     ", @GUID, @INPUT_MODE, @ID_TERMINAL" +
                                                     ")", con);

            // Add the parameters for the InsertCommand.
            command.Parameters.Add("@CountEntries", SqlDbType.Int, 4, "CountEntries");
            command.Parameters.Add("@PONUMBER", SqlDbType.VarChar, 17, "PONUMBER");
            command.Parameters.Add("@ORD", SqlDbType.Int, 4, "ORD");
            command.Parameters.Add("@ITEMNMBR", SqlDbType.VarChar, 31, "ITEMNMBR");
            command.Parameters.Add("@VNDDOCNM", SqlDbType.VarChar, 21, "VNDDOCNM");
            command.Parameters.Add("@VNDITNUM", SqlDbType.VarChar, 31, "VNDITNUM");
            command.Parameters.Add("@LOCNCODE", SqlDbType.VarChar, 11, "LOCNCODE");
            command.Parameters.Add("@QTYSHPPD", SqlDbType.Decimal, 9, "QTYSHPPD");
            command.Parameters.Add("@QTYPACK", SqlDbType.Decimal, 9, "QTYPACK");
            command.Parameters.Add("@SERLTNUM", SqlDbType.VarChar, 21, "SERLTNUM");
            command.Parameters.Add("@KOD_SW", SqlDbType.VarChar, 11, "KOD_SW");
            command.Parameters.Add("@DAT_VYROBY", SqlDbType.VarChar, 11, "DAT_VYROBY");
            command.Parameters.Add("@DATEDONE", SqlDbType.VarChar, 8, "DATEDONE");
            command.Parameters.Add("@TIMEDONE", SqlDbType.VarChar, 6, "TIMEDONE");
            command.Parameters.Add("@CZ_CarKod", SqlDbType.VarChar, 31, "CZ_CarKod");
            command.Parameters.Add("@REZ_1", SqlDbType.VarChar, 21, "REZ_1");
            command.Parameters.Add("@REZ_2", SqlDbType.VarChar, 21, "REZ_2");
            command.Parameters.Add("@USER_ID", SqlDbType.Int, 4, "USER_ID");
            if (Properties.Settings.Default.DexRowIdInsert)
            {
                command.Parameters.Add("@DEX_ROW_ID", SqlDbType.Int, 4, "DEX_ROW_ID");
            }
            command.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier, 4, "GUID");
            command.Parameters.Add("@INPUT_MODE", SqlDbType.TinyInt, 1, "INPUT_MODE");
            command.Parameters.Add("@ID_TERMINAL", SqlDbType.Int, 4, "ID_TERMINAL");

            return command;
        }
        */
        public Fask.Server.Interfaces.Classes.StatusInfo Prijem_GenerateDavka(Fask.Server.Interfaces.Classes.Objednavka objednavka, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            Globals.LoadConfiguration();
            StatusInfo si = new StatusInfo();
            si.Description = "Prijem_GenerateDavka start";
            si.ID = 0;


            if (objednavka.ID == "prelokovani")
            {
                //logika
                Globals.LoadConfiguration();


                string stav = Classes.Prijem.Export_Prelokovani_SQL_Prijem(objednavka, sklad);

                //rozhodnuti na vysledny stav
                if (stav != "OK")
                {
                    si.ID = -10;
                    si.Description = stav;
                    si.InnerException = new Exception(si.Description);
                    return si;
                }
                else
                {
                    if (!string.IsNullOrEmpty(objednavka.CisloDavky) && int.TryParse(objednavka.CisloDavky, out int id))
                    {
                        si.ID = id;
                    }
                    else
                    {
                        si.ID = -1; // nebo jiná defaultní hodnota / error handling
                    }

                    //25.9.2025 MaR zakomentoval aby nehazelo vyjimku
                    //si.ID = int.Parse(objednavka.CisloDavky);

                    si.Description = "OK";
                    si.InnerException = null;
                }

            }
            // 2) nepodporovany typ transakce
            else
            {
                //si.Description = "Doklad '" + objednavka.ID + "' nenalezen.";
                si.Description = "Transakce '" + objednavka.ID + "' nenalezena.";
                if (sklad != null)
                    si.Description += "\nSklad " + sklad.ID;
                si.InnerException = new Exception(si.Description);
                si.ID = -10;
                throw new Exception(si.Description);
            }

            return si;
        }

        public Fask.DataSets.PrijemDavky Prijem_GetPrijemky(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        {
            try
            {
                Globals.LoadConfiguration();

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
                    " (A.CZ_Doslo<=0 OR A.CZ_Doslo=" + terminal.ID + ")" +
                    " and A.SKL_ID like '" + SQLInjection.Filter(sklad.ID) + "%' " +
                    " group by A.countentries, A.ponumber, B.CntItems, C.qtyshppdsum " +
                    " order by A.countentries ";

                Fask.DataSets.PrijemDavky volneprijemky = new Fask.DataSets.PrijemDavky();

                using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select, Globals.Konfigurace.ConnectionString[0].FASKDB))
                {
                    sql.Fill(volneprijemky, volneprijemky.Hlavicky.TableName);
                }

                #region Rozsireni o dotazeni informace do infa z existujiciho pohledu detailu. vic neni mozne
                try
                {
                    if (Globals.Konfigurace.Prijem[0].PrijemkaDetail2Hlavicka)
                    {
                        DataSet ds = Prijem_Detail(new Fask.Server.Interfaces.Classes.Objednavka(), new Fask.Server.Interfaces.Classes.Sklad());
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataColumn dcol in ds.Tables[0].Columns)
                            {
                                volneprijemky.Hlavicky.Columns.Add(dcol.ColumnName, dcol.DataType);
                            }
                        }

                        if (volneprijemky.Hlavicky.Count > 0)
                        {
                            foreach (Fask.DataSets.PrijemDavky.HlavickyRow hrow in volneprijemky.Hlavicky)
                            {
                                try
                                {
                                    Fask.Server.Interfaces.Classes.Objednavka dv = new Fask.Server.Interfaces.Classes.Objednavka();
                                    dv.ID = hrow.PONUMBER;
                                    Fask.Server.Interfaces.Classes.Sklad skl = new Fask.Server.Interfaces.Classes.Sklad();
                                    skl.ID = hrow.SKL_ID;

                                    ds = Prijem_Detail(dv, skl);
                                    foreach (DataColumn dcol in ds.Tables[0].Columns)
                                    {
                                        DataColumn dcolhrow = hrow.Table.Columns[dcol.ColumnName];
                                        hrow.SetField<object>(dcolhrow, ds.Tables[0].Rows[0][dcol]);
                                    }
                                }
                                catch
                                { }
                            }
                        }
                        volneprijemky.AcceptChanges();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion


                return volneprijemky;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public Fask.DataSets.Prijem Prijem_GetPrijemka(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        {
            Globals.LoadConfiguration();
            string select = "SELECT * FROM " + TABLE_CZMST_PE + " where CountEntries=" + davka.ID + " order by dex_row_id";

            //Nacteni dat prijemky z databaze
            Fask.DataSets.Prijem prijem = new Fask.DataSets.Prijem();

            using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select, Globals.Konfigurace.ConnectionString[0].FASKDB))
            {
                sql.Fill(prijem, prijem.CZMST_PE.TableName);
            }

            select = "SELECT * FROM " + TABLE_CZMST_PI + " where CountEntries=" + davka.ID + " order by dex_row_id";

            using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select, Globals.Konfigurace.ConnectionString[0].FASKDB))
            {
                sql.Fill(prijem, prijem.CZMST_PI.TableName);
            }

			#region Dotaženi seznamu SN

			select = "SELECT * FROM " + TABLE_CZMST_PE_SN + " where CountEntries=" + davka.ID + " order by dex_row_id";

			using (System.Data.SqlClient.SqlDataAdapter sql = new System.Data.SqlClient.SqlDataAdapter(select, Globals.Konfigurace.ConnectionString[0].FASKDB))
			{
				sql.Fill(prijem, prijem.CZMST_PE_SN.TableName);
			}
			
			#endregion

            return prijem;
        }

        public bool Prijem_GetPrijemkaReceived(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item)
        {
            Globals.LoadConfiguration();
            string selectCount = "SELECT Count(CountEntries) as davka FROM " + TABLE_CZMST_PE + " where CountEntries=" + davka.ID + " AND (CZ_Doslo<=0 OR CZ_Doslo=" + terminal.ID + ")";
            string update = "Update " + TABLE_CZMST_PE + " set CZ_Doslo=" + terminal.ID + " where countentries=" + davka.ID;

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
                if (r is int && ((int)r) <= 0)
                    throw new Exception("Dávka se již zpracovává.");

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

        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Process(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item, Fask.DataSets.Prijem prijemdata, Fask.Server.Interfaces.Prijem.ProcessState processPrijemState)
        {
            Globals.LoadConfiguration();

            SqlTransaction trans = null;

            //string guidDavka = prijemdata.CZMST_PEH[0].GUID.ToString();
            // pokud se davka neotevrela, je tabulka CZMST_PEH prazdna, dojde k vygenerovani noveho GUIDu
            string guidDavka = string.Empty;
            if (prijemdata.CZMST_PEH.Count > 0)
                guidDavka = prijemdata.CZMST_PEH[0].GUID.ToString();
            else
                guidDavka = Guid.NewGuid().ToString();

            //string filePath = Path.Combine(Properties.Settings.Default.PathStateDataFile, guidDavka);
            string filePath = Path.Combine(Fask.MyPath.Path.RootPath, Path.Combine(Globals.Konfigurace.Prijem[0].StatusObjectsDirectory, guidDavka));

            Fask.Server.Interfaces.Classes.StatusObject so = new Fask.Server.Interfaces.Classes.StatusObject(filePath);

            //zjistit zda soubor s danym guid existuje
            if (File.Exists(filePath))
            { //soubor jiz existuje
                so = Fask.Server.Interfaces.Classes.StatusObject.Load(filePath);
                if (!so.Exception)
                    return so;
            }
            //pokud ne, projit normalne dal

            bool uvolnitdavku = processPrijemState == Fask.Server.Interfaces.Prijem.ProcessState.Uvolnit;

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

            try
            {
                so.Write("vytvareni connection");
                conn.Open();

                if (uvolnitdavku) //uvolnit davku
                {
                    so.Write("uvolnit davku");
                    string updateuvolnit = "Update " + TABLE_CZMST_PE + " set CZ_Doslo=0 where CountEntries=" + davka.ID;
                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(updateuvolnit, conn, trans);
                    int rows = comm.ExecuteNonQuery();
                }
                else //zapsat davku
                {

                    so.Write("zapsat davku");
                    bool allowInsertData = true;
                    //Test zda je mozne data pridat, jestlize jiz existuji, tak nepridat. 
                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand("Select Count(*) as number from " + TABLE_CZMST_PI + " where countentries=" + davka.ID, conn);
                    object datacount = comm.ExecuteScalar();
                    if (datacount != null && ((int)datacount) > 0)
                    {
                        //return true;
                        allowInsertData = false;
                    }

                    if (allowInsertData)
                    {
                        if (Globals.Konfigurace.Prijem[0].DexRowIdInsert)
                        {
                            int dexrowid = 0;
                            System.Data.SqlClient.SqlCommand xdexrowidmax = new System.Data.SqlClient.SqlCommand("Select MAX(DEX_ROW_ID) from " + TABLE_CZMST_PI, conn, trans);
                            object maxdexrowid = xdexrowidmax.ExecuteScalar();
                            if (maxdexrowid != null && !(maxdexrowid is System.DBNull))
                                dexrowid = (int)maxdexrowid;

                            prijemdata.CZMST_PI.Columns["DEX_ROW_ID"].ReadOnly = false;

                            foreach (Fask.DataSets.Prijem.CZMST_PIRow pirow in prijemdata.CZMST_PI)
                            {
                                pirow.DEX_ROW_ID = ++dexrowid;
                            }
                        }

                        //Update databaze
                        so.Write("Update databaze");
                        trans = conn.BeginTransaction();
                        string updatePE = "Update " + TABLE_CZMST_PE + " set CZ_Doslo=" + (terminal.ID + 100) + " where CountEntries=" + davka.ID;
                        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(updatePE, conn, trans);

                        SQL_Datasets.PrijemTableAdapters.CZMST_PITableAdapter dita = new Fask.ModuleSql.SQL_Datasets.PrijemTableAdapters.CZMST_PITableAdapter();
                        dita.Connection = conn;
                        dita.Transaction = trans;
                        dita.Update(prijemdata.CZMST_PI.Select(null, null, DataViewRowState.Added));

                        #region lokace
                        if (prijemdata.Parametry.Count > 0 && !prijemdata.Parametry.First().IsCONFIG_LOKACE_POVOLITNull() && prijemdata.Parametry.First().CONFIG_LOKACE_POVOLIT)
                        {
                            foreach (Fask.DataSets.Prijem.CZMST_PIRow pirow in prijemdata.CZMST_PI)
                            {
                                string countCommandText = "select count(*) from " + TABLE_CZMST_SKLADLOKACE_STAVPOHYB + " where guid=@guid";
                                SqlCommand countCommand = new SqlCommand(countCommandText, conn, trans);
                                countCommand.Parameters.Clear();
                                countCommand.Parameters.AddWithValue("@guid", pirow.GUID);

                                int guidcount = (int)countCommand.ExecuteScalar();

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
                                    if(pirow.IsDATEDONENull() || pirow.IsTIMEDONENull())
                                        pohybrow.dateeveS = dtnow;
                                    else
                                        pohybrow.dateeveT = DateTime.ParseExact(pirow.DATEDONE + " " + pirow.TIMEDONE, "yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture);

                                    ProcessPrijem(pohybrow, new SqlCommand(), conn, trans, new SqlDataAdapter());

                                }
                            }
                        }
                        #endregion

						/*
                        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
                        //da.UpdateCommand.Transaction = trans;

                        // \TODO: dat mimo tuto funkci, aby se porad nevytvarel dokola...
                        da.InsertCommand = CreateInsertCommand(conn);
                        da.InsertCommand.Transaction = trans;

                        da.Update(prijemdata.CZMST_PI.Select(null, null, DataViewRowState.Added));
                        */
						int rows = command.ExecuteNonQuery();

                    }
                }

                so.Write("commit transakce");

                if (trans != null)
                    trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

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
            if (Globals.Konfigurace.Prijem[0].AfterDataProcessed_Action_Asynchronous)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Prijem_AfterProcessedActionAsync));
                thread.Start(davka);
            }
            else
            {
                if (!Prijem_AfterProcessedAction(davka))
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


        //Fask.Server.Interfaces.Classes.Davka
        public void Prijem_AfterProcessedActionAsync(object davka)
        {
            try
            {
                Fask.Server.Interfaces.Classes.Davka d = (Fask.Server.Interfaces.Classes.Davka)davka;
                Prijem_AfterProcessedAction(d);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Prijem_AfterProcessedAction(Fask.Server.Interfaces.Classes.Davka davka)
        {
            try
            {
                Globals.LoadConfiguration();
                string aDP_Action = Globals.Konfigurace.Prijem[0].AfterDataProcessed_Action;
                string aDP_Action_P1 = Globals.Konfigurace.Prijem[0].AfterDataProcessed_Action_P1;
                string aDP_Action_P2 = Globals.Konfigurace.Prijem[0].AfterDataProcessed_Action_P2;
                if (aDP_Action.Length != 0)
                {
                    Routines.AfterProcessAction.Execute(Globals.Konfigurace.ConnectionString[0].FASKDB, TABLE_CZMST_PI, (int)davka.ID, aDP_Action, aDP_Action_P1, aDP_Action_P2, 120);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataSet Prijem_Detail(Fask.Server.Interfaces.Classes.Objednavka objednavka, Fask.Server.Interfaces.Classes.Sklad sklad)
        {
            try
            {
                Globals.LoadConfiguration();
                System.Data.SqlClient.SqlConnection dbconnection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                System.Data.SqlClient.SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand();
                dbCommand.CommandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Prijem[0].CommandType);

                string prijemkadetail = Globals.Konfigurace.Prijem[0].PrijemkaDetail;
                string prijemkadetail_paramname = Globals.Konfigurace.Prijem[0].Detail_paramName;

                if (dbCommand.CommandType == CommandType.StoredProcedure)
                {
                    dbCommand.CommandText = prijemkadetail;
                    dbCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(prijemkadetail_paramname, objednavka.ID));
                }
                else if (dbCommand.CommandType == CommandType.Text)
                {
                    dbCommand.CommandText = "Select * from " + prijemkadetail + " where " + prijemkadetail_paramname + "='" + objednavka.ID + "'";
                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + dbCommand.CommandType.ToString());
                }

                dbCommand.Connection = dbconnection;

                DataSet data = new DataSet();
                System.Data.SqlClient.SqlDataAdapter dbda = new System.Data.SqlClient.SqlDataAdapter(dbCommand);
                dbda.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataSet Prijem_Detail_Polozka(Fask.Server.Interfaces.Classes.Objednavka objednavka, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item polozka)
        {
			return new DataSet();
        }

        public System.Data.DataSet Prijem_DetailDavka(Fask.Server.Interfaces.Classes.Davka davka)
        {
            throw new NotImplementedException();
        }

        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Storno_Prijemka(Fask.Server.Interfaces.Classes.Davka davka, Server.Interfaces.Classes.Terminal terminal, string password)
        {
            throw new NotImplementedException();
        }

        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Finish_Prijemka(Fask.Server.Interfaces.Classes.Davka davka, Server.Interfaces.Classes.Terminal terminal, string password)
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

        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Online_Add(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.DataSets.Prijem prijemRows)
        {
            throw new NotImplementedException();
        }

        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Online_Del(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, Fask.DataSets.Prijem prijemRows)
        {
            throw new NotImplementedException();
        }

        public Fask.Server.Interfaces.Classes.StatusObject Prijem_Online_Quantity(Fask.Server.Interfaces.Classes.Davka davka, Fask.Server.Interfaces.Classes.Terminal terminal, ref Fask.DataSets.Prijem prijemRows)
        {
            throw new NotImplementedException();
        }

        public Fask.Server.Interfaces.Classes.StatusOverLokace Prijem_Online_OverLokace(string serltnum, string itemnmbr, string locncode, decimal qtyshppd, string skl_id)
        {
            return Lokace_OverLokace(skl_id, locncode);
        }

        #region IPrijem Members


        public Fask.Server.Interfaces.DataSets.Obecne Prijem_GetPrijemky_External(Fask.Server.Interfaces.Classes.Terminal terminal, Fask.Server.Interfaces.Classes.Sklad sklad, Fask.Server.Interfaces.Classes.Item item, List<string> ListCarKod)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region IPrijem Members

        public bool Prijem_GetSkladExpedice(string ITEMNMBR, decimal MnozstviZadane, decimal MnozstviNasnimane, out decimal MnozstviDodavatelePozadovano, out decimal MnozstviDodavateleDodano, out decimal MnozstviDodavateleDodat, out decimal MnozstviOdberateliPozadovano, out decimal MnozstviOdberatelumDodano, out decimal MnozstviOdberatelumDodat, out decimal Vysledek)
        {
            throw new NotImplementedException();
        }

        public string Online_GenerateSerltnum(string itemnmbr, string skl_id, string oldSerltnum)
        {
            string serltnum = string.Empty;
            SqlConnection adpaconnection = null;

            try
            {
                Globals.LoadConfiguration();

                string prijem_generateData = Globals.Konfigurace.Prijem[0].GenerateSerltnum_Action;

                if (prijem_generateData.Length != 0)
                {
                    adpaconnection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                    SqlCommand adpacommand = new SqlCommand(prijem_generateData);
                    adpacommand.CommandType = CommandType.StoredProcedure;

                    // parametry
                    adpacommand.Parameters.Add((new SqlParameter("@Itemnmbr", SqlDbType.NVarChar, 31)));
                    adpacommand.Parameters.Add((new SqlParameter("@OldSerltnum", SqlDbType.NVarChar, 21)));
                    adpacommand.Parameters.Add((new SqlParameter("@Skl_id", SqlDbType.NVarChar, 20)));
                    adpacommand.Parameters.Add((new SqlParameter("@Serltnum", SqlDbType.NVarChar, 21)));

                    // hodnoty vstupnich parametru
                    ((IDataParameter)adpacommand.Parameters["@Itemnmbr"]).Value = itemnmbr;
                    ((IDataParameter)adpacommand.Parameters["@OldSerltnum"]).Value = oldSerltnum;
                    ((IDataParameter)adpacommand.Parameters["@Skl_id"]).Value = skl_id;

                    // hodnoty vystupnich parametru
                    ((IDataParameter)adpacommand.Parameters["@Serltnum"]).Direction = ParameterDirection.Output;

                    adpacommand.Connection = adpaconnection;

                    adpaconnection.Open();
                    adpacommand.ExecuteNonQuery();

                    serltnum = ((IDataParameter)adpacommand.Parameters["@Serltnum"]).Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
                if ((adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }

            return serltnum;
        }

        public Fask.Server.Interfaces.DataSets.Obecne Online_GetDoporuceneLokace(string itemnmbr, string skl_id, string serltnum)
        {
            Fask.Server.Interfaces.DataSets.Obecne ds = new Fask.Server.Interfaces.DataSets.Obecne();
            SqlConnection adpaconnection = null;
            try
            {
                Globals.LoadConfiguration();

                string prijem_generateData = Globals.Konfigurace.Prijem[0].GetDoporuceneLokace_Action;
                if (prijem_generateData.Length != 0)
                {
                    adpaconnection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                    SqlCommand adpacommand = new SqlCommand(prijem_generateData);
                    adpacommand.CommandType = CommandType.StoredProcedure;

                    // vstupni parametry
                    adpacommand.Parameters.Add((new SqlParameter("@Itemnmbr", SqlDbType.NVarChar, 31)));
                    adpacommand.Parameters.Add((new SqlParameter("@Serltnum", SqlDbType.NVarChar, 21)));
                    adpacommand.Parameters.Add((new SqlParameter("@Skl_id", SqlDbType.NVarChar, 20)));

                    // hodnoty vstupnich parametru
                    ((IDataParameter)adpacommand.Parameters["@Itemnmbr"]).Value = itemnmbr;
                    ((IDataParameter)adpacommand.Parameters["@Serltnum"]).Value = serltnum;
                    ((IDataParameter)adpacommand.Parameters["@Skl_id"]).Value = skl_id;

                    adpacommand.Connection = adpaconnection;

                    adpaconnection.Open();

                    SqlDataAdapter xda = new SqlDataAdapter();
                    xda.SelectCommand = adpacommand;

                    xda.Fill(ds, ds.Lokace.TableName);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
                if ((adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }

            return ds;
        }

        public Fask.Server.Interfaces.DataSets.Obecne Online_GetNezrealizovanePrijemky()
        {
            Fask.Server.Interfaces.DataSets.Obecne ds = new Fask.Server.Interfaces.DataSets.Obecne();
            SqlConnection adpaconnection = null;
            try
            {
                Globals.LoadConfiguration();

                string prijem_generateData = Globals.Konfigurace.Prijem[0].GetNezrealizovanePrijemky_Action;
                if (prijem_generateData.Length != 0)
                {
                    adpaconnection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                    SqlCommand adpacommand = new SqlCommand(prijem_generateData);
                    adpacommand.CommandType = CommandType.StoredProcedure;

                    adpacommand.Connection = adpaconnection;

                    adpaconnection.Open();

                    SqlDataAdapter xda = new SqlDataAdapter();
                    xda.SelectCommand = adpacommand;

                    xda.Fill(ds, ds.Prijemky.TableName);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            finally
            {
                if ((adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }

            return ds;
        }



        #endregion

        public string TEST_ImportPrijem_Do_IS(int countEntries, string SKL_ID, string PONUMBER, string note)
        {
            throw new NotImplementedException();
        }

    }
}
