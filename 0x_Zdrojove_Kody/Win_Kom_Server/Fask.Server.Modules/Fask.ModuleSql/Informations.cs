using Fask.Server.Interfaces.DataSets;
using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Fask.ModuleSql
{
    public partial class Provider : Fask.Server.Interfaces.Informations.IInformations2,
        Fask.Server.Interfaces.Informations.IInformations2_Command1,
        Fask.Server.Interfaces.Informations.IInformations2_DetailItemnumber,
        Fask.Server.Interfaces.Informations.IInformations2_MnozstviNaSklade,
         Fask.Server.Interfaces.Informations.IInformations2_MnozstviNaSklade_Itemnumber_Location,
        Fask.Server.Interfaces.Informations.IInformations2_FEFOFIFO
    {
        public DataSet Command1(string param1, string param2)
        {
            try
            {
                Globals.LoadConfiguration();

                SqlConnection xconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                SqlCommand xcomm = new SqlCommand(Globals.Konfigurace.Informations[0].command1_proc, xconn);
                xcomm.CommandType = CommandType.StoredProcedure;
                xcomm.Parameters.Add((new SqlParameter(Globals.Konfigurace.Informations[0].command1_paramName1, param1)));
                xcomm.Parameters.Add((new SqlParameter(Globals.Konfigurace.Informations[0].command1_paramName2, param2)));

                DataSet ds = new DataSet();
                ds.DataSetName = Globals.Konfigurace.Informations[0].command1_DataSetName;
                ds.Locale = new System.Globalization.CultureInfo("en");
                SqlDataAdapter xda = new SqlDataAdapter();
                xda.SelectCommand = xcomm;
                xda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Informations: Command1[" + param1 + "," + param2 + "])");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public DataSet DetailItemnumber(string itemnumber, string doklad)
        {
            try
            {
                Globals.LoadConfiguration();

                CommandType commandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Informations[0].DetailItemnumber_CommandType);
                string detailItemnumber_proc = Globals.Konfigurace.Informations[0].DetailItemnumber;
                string detailItemnumber_proc_paramName1 = Globals.Konfigurace.Informations[0].DetailItemnumber_paramName1;
                string detailItemnumber_proc_paramName2 = Globals.Konfigurace.Informations[0].DetailItemnumber_paramName2;

                SqlConnection xconn = null;
                SqlCommand xcomm = null;
                xconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                xcomm = new SqlCommand();
                xcomm.CommandType = commandType;
                if (commandType == CommandType.StoredProcedure)
                {
                    xcomm.CommandText = detailItemnumber_proc;
                    xcomm.Parameters.Add(new SqlParameter(detailItemnumber_proc_paramName1, itemnumber));
                    xcomm.Parameters.Add(new SqlParameter(detailItemnumber_proc_paramName2, doklad));
                }
                else if (commandType == CommandType.Text)
                {
                    xcomm.CommandText = "Select * from " + detailItemnumber_proc + " where " + detailItemnumber_proc_paramName1 + "='" + itemnumber + "'";
                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + commandType.ToString());
                }

                xcomm.Connection = xconn;

                DataSet data = new DataSet();
                SqlDataAdapter xda = new SqlDataAdapter();
                xda.SelectCommand = xcomm;
                xda.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Informations: DetailItemnumber[" + itemnumber + "])");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        public float MnozstviNaSklade(string ItemNumber)
        {
            float mnozstvi = 0f;
            SqlConnection xconn = null;
            SqlCommand xcomm = null;
            try
            {
                Globals.LoadConfiguration();

                CommandType commandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Informations[0].MnozstviNaSklade_CommandType);
                string mnozstvinasklade_proc_paramName = Globals.Konfigurace.Informations[0].MnozstviNaSklade_paramName ;
                string mnozstvinasklade_proc = Globals.Konfigurace.Informations[0].MnozstviNaSklade;

                xconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                xcomm = new SqlCommand();
                xcomm.CommandType = commandType;
                if (commandType == CommandType.StoredProcedure)
                {
                    xcomm.Parameters.Add((new SqlParameter(mnozstvinasklade_proc_paramName, ItemNumber)));
                    xcomm.CommandText = mnozstvinasklade_proc;
                }
                else if (commandType == CommandType.Text)
                {
                    xcomm.CommandText = "Select * from " + mnozstvinasklade_proc + " where " + mnozstvinasklade_proc_paramName + "='" + ItemNumber + "'";
                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + commandType.ToString());
                }
                xcomm.Connection = xconn;
                xconn.Open();
                object o = xcomm.ExecuteScalar();
                if (o is System.DBNull)
                    mnozstvi = 0;
                else
                    mnozstvi = Convert.ToSingle(o);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Informations: MnozstviNaSklade[" + ItemNumber + "])");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                throw ex;
            }
            finally
            {
                if (xconn != null && xconn.State == ConnectionState.Open)
                {
                    xconn.Close();
                }
            }
            return mnozstvi;
        }

        public float? MnozstviNaSklade_Itemnumber_Location(string ItemNumber, string Location)
        {
            float? mnozstvi = null;
            SqlConnection xconn = null;
            SqlCommand xcomm = null;
            try
            {
                Globals.LoadConfiguration();
                CommandType commandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Informations[0].MnozstviNaSklade_I_L_CommandType);
                string mnozstvinasklade_proc2 = Globals.Konfigurace.Informations[0].MnozstviNaSklade2;
                string mnozstvinasklade_proc2_paramName1 = Globals.Konfigurace.Informations[0].MnozstviNaSklade2_paramName1;
                string mnozstvinasklade_proc2_paramName2 = Globals.Konfigurace.Informations[0].MnozstviNaSklade2_paramName2;

                xconn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                xcomm = new SqlCommand();
                xcomm.CommandType = commandType;
                if (commandType == CommandType.StoredProcedure)
                {
                    xcomm.CommandText = mnozstvinasklade_proc2;
                    xcomm.Parameters.Add((new SqlParameter(mnozstvinasklade_proc2_paramName1, ItemNumber)));
                    xcomm.Parameters.Add((new SqlParameter(mnozstvinasklade_proc2_paramName2, Location)));
                }
                else if (commandType == CommandType.Text)
                {
                    xcomm.CommandText = "Select * from " + mnozstvinasklade_proc2 + " where " + mnozstvinasklade_proc2_paramName1 + "='" + ItemNumber + "' and " + mnozstvinasklade_proc2_paramName2 + "='" + Location + "'";
                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + commandType.ToString());
                }
                xcomm.Connection = xconn;
                xconn.Open();
                object o = xcomm.ExecuteScalar();
                if (o is System.DBNull || o == null)
                    mnozstvi = null; //12.1.2026 pozor nevracet 0 mnozstvi ale polozka nenalezena!
                else
                    mnozstvi = Convert.ToSingle(o);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Informations: MnozstviNaSklade[" + ItemNumber + "," + Location + "])");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                throw ex;
            }
            finally
            {
                if (xconn != null && xconn.State == ConnectionState.Open)
                {
                    xconn.Close();
                }
            }
            return mnozstvi;
        }

 
        public Location FEFOFIFO_Online(string itemnmbr, string skl_id, string serltnum, string doc_id, string locncode)
        {
            //throw new NotImplementedException();
            //Globals_V1.LoadConfiguration();

            Obecne ds = new Obecne();
            Location dsLocation = new Location();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            string FEFOFIFO_proc = Globals.Konfigurace.Informations[0].FEFOFIFO_proc;
            try
            {
                //Globals.LoadConfiguration();
                CommandType commandType = (CommandType)Enum.Parse(typeof(CommandType), Globals.Konfigurace.Informations[0].FEFOFIFO_I_L_CommandType);

                if (commandType == CommandType.StoredProcedure)
                {

                    if (Globals.Konfigurace.Informations[0].FEFOFIFO_pouzita_metoda == 1)
                    {
                        if (FEFOFIFO_proc.Length != 0)
                        {
                            
                            using (connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                            using (command = new SqlCommand(FEFOFIFO_proc, connection) { CommandType = CommandType.StoredProcedure })
                            {
                                // Parametry + typy a délky
                                command.Parameters.Add("@Itemnmbr", SqlDbType.NVarChar, 31).Value = itemnmbr ?? string.Empty;
                                command.Parameters.Add("@Skl_id", SqlDbType.NVarChar, 20).Value = skl_id ?? string.Empty;
                                command.Parameters.Add("@Serltnum", SqlDbType.NVarChar, 21).Value = serltnum ?? string.Empty;
                                command.Parameters.Add("@doc_id", SqlDbType.NVarChar, 12).Value = doc_id ?? string.Empty;
                                command.Parameters.Add("@locncode", SqlDbType.NVarChar, 20).Value = locncode ?? string.Empty;

                                // hodnoty vstupnich parametru
                                command.Parameters["@Itemnmbr"].Value = itemnmbr ?? string.Empty;
                                command.Parameters["@Skl_id"].Value = skl_id ?? string.Empty;
                                command.Parameters["@Serltnum"].Value = serltnum ?? string.Empty;
                                command.Parameters["@doc_id"].Value = doc_id ?? string.Empty;
                                command.Parameters["@locncode"].Value = locncode ?? string.Empty;

                                connection.Open();

                                using (adapter = new SqlDataAdapter(command))
                                {
                                    // (volitelně) vyčistit cílovou tabulku v DataSetu před naplněním
                                    // dsLocation.CZMST_SkladLokace_Stav.Clear();

                                    adapter.Fill(ds, ds.Palety.TableName);
                                }
                            }


                            // import do noveho datasetu
                            if (ds != null && ds.Palety.Count > 0)
                            {
                                DateTime dtnow = DateTime.Now;
                                foreach (var paletyrow in ds.Palety)
                                {
                                    Location.CZMST_SkladLokace_StavRow newrow = dsLocation.CZMST_SkladLokace_Stav.NewCZMST_SkladLokace_StavRow();
                                    newrow.Index = paletyrow.Index;
                                    newrow.ITEMNMBR = paletyrow.IsITEMNMBRNull() ? string.Empty : paletyrow.ITEMNMBR;
                                    newrow.ITEMDESC = paletyrow.IsITEMDESCNull() ? string.Empty : paletyrow.ITEMDESC;
                                    newrow.QTYSHPPD_DEF = paletyrow.QTYSHPPD;
                                    newrow.QTYSHPPD = paletyrow.QTYSHPPD;
                                    newrow.SERLTNUM = paletyrow.IsSERLTNUMNull() ? string.Empty : paletyrow.SERLTNUM;
                                    newrow.SKL_ID = paletyrow.IsSKL_IDNull() ? string.Empty : paletyrow.SKL_ID;
                                    newrow.LOCNCODE = paletyrow.IsLOCNCODENull() ? string.Empty : paletyrow.LOCNCODE;
                                    newrow.DATECHANGE = dtnow;
                                    //newrow.SetEXPIRATIONNull();
                                    if (!paletyrow.IsEXPIRACENull())
                                        newrow.EXPIRATION = paletyrow.EXPIRACE;
                                    if (!paletyrow.IsPRIJEMNull())
                                        newrow.PRIJEM = paletyrow.PRIJEM;
                                    if (!paletyrow.IsRAZENINull())
                                        newrow.RAZENI = paletyrow.RAZENI;
                                    if (!paletyrow.IsBLOKACENull())
                                        newrow.BLOKACE = paletyrow.BLOKACE;

                                    dsLocation.CZMST_SkladLokace_Stav.AddCZMST_SkladLokace_StavRow(newrow);
                                }
                            }
                        }
                    }
                    else if (Globals.Konfigurace.Informations[0].FEFOFIFO_pouzita_metoda == 2)
                    {
                        if (FEFOFIFO_proc.Length != 0)
                        {
                           
                            using ( connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                            using ( command = new SqlCommand(FEFOFIFO_proc, connection) { CommandType = CommandType.StoredProcedure })
                            {
                                // Parametry + typy a délky
                                command.Parameters.Add("@Itemnmbr", SqlDbType.NVarChar, 31).Value = itemnmbr ?? string.Empty;
                                command.Parameters.Add("@Skl_id", SqlDbType.NVarChar, 20).Value = skl_id ?? string.Empty;
                                command.Parameters.Add("@Serltnum", SqlDbType.NVarChar, 21).Value = serltnum ?? string.Empty;
                                command.Parameters.Add("@doc_id", SqlDbType.NVarChar, 12).Value = doc_id ?? string.Empty;
                                command.Parameters.Add("@locncode", SqlDbType.NVarChar, 20).Value = locncode ?? string.Empty;

                                // hodnoty vstupnich parametru
                                command.Parameters["@Itemnmbr"].Value = itemnmbr ?? string.Empty;
                                command.Parameters["@Skl_id"].Value = skl_id ?? string.Empty;
                                command.Parameters["@Serltnum"].Value = serltnum ?? string.Empty;
                                command.Parameters["@doc_id"].Value = doc_id ?? string.Empty;
                                command.Parameters["@locncode"].Value = locncode ?? string.Empty;

                                connection.Open();

                                using ( adapter = new SqlDataAdapter(command))
                                {
                                    // (volitelně) vyčistit cílovou tabulku v DataSetu před naplněním
                                    // dsLocation.CZMST_SkladLokace_Stav.Clear();

                                    adapter.Fill(dsLocation, dsLocation.CZMST_SkladLokace_Stav.TableName);
                                }
                            }

                        }
                    }
                    else if (Globals.Konfigurace.Informations[0].FEFOFIFO_pouzita_metoda == 3)
                    {
                        string vysledek = string.Empty;

                        using (SqlConnection conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                        using (SqlCommand cmd = new SqlCommand(FEFOFIFO_proc, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // parametry
                            cmd.Parameters.Add((new SqlParameter("@Itemnmbr", SqlDbType.NVarChar, 31)));
                            cmd.Parameters.Add((new SqlParameter("@Skl_id", SqlDbType.NVarChar, 20)));
                            cmd.Parameters.Add((new SqlParameter("@Serltnum", SqlDbType.NVarChar, 21)));
                            cmd.Parameters.Add((new SqlParameter("@doc_id", SqlDbType.NVarChar, 12)));
                            cmd.Parameters.Add((new SqlParameter("@locncode", SqlDbType.NVarChar, 20)));

                            // hodnoty vstupnich parametru
                            cmd.Parameters["@Itemnmbr"].Value = itemnmbr ?? string.Empty;
                            cmd.Parameters["@Skl_id"].Value = skl_id ?? string.Empty;
                            cmd.Parameters["@Serltnum"].Value = serltnum ?? string.Empty;
                            cmd.Parameters["@doc_id"].Value = doc_id ?? string.Empty;
                            cmd.Parameters["@locncode"].Value = locncode ?? string.Empty;

                            conn.Open();
                            object result = cmd.ExecuteScalar();
                            vysledek = result?.ToString() ?? string.Empty;
                        }

                        // vysledek = SQL dotaz, např. "SELECT * FROM ... WHERE ...;"
                        if (string.IsNullOrWhiteSpace(vysledek))
                            throw new ArgumentException("SQL dotaz (vysledek pro data z procedury) je prázdný.");


                     
                            using (SqlConnection Sqlconnection = new SqlConnection(
                                                 Globals.Konfigurace.ConnectionString[0].FASKDB))
                            using (SqlCommand Sqlcommand = new SqlCommand(vysledek, Sqlconnection))
                            using (SqlDataAdapter Sqladapter = new SqlDataAdapter(Sqlcommand))
                            {
                                Sqlcommand.CommandType = CommandType.Text;

                                Sqlconnection.Open();

                                Sqladapter.Fill(dsLocation, dsLocation.CZMST_SkladLokace_Stav.TableName);
                            }
                        


                    }
                    else if (Globals.Konfigurace.Informations[0].FEFOFIFO_pouzita_metoda == 4)
                    {
                        #region 6.1.2026 OLD MaR
                        //string vysledek = string.Empty;
                        //Location dsLocationPom = new Location();
                        //using (SqlConnection conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                        //using (SqlCommand cmd = new SqlCommand(FEFOFIFO_proc, conn))
                        //{
                        //    cmd.CommandType = CommandType.StoredProcedure;

                        //    // parametry
                        //    cmd.Parameters.Add((new SqlParameter("@Itemnmbr", SqlDbType.NVarChar, 31)));
                        //    cmd.Parameters.Add((new SqlParameter("@Skl_id", SqlDbType.NVarChar, 20)));
                        //    cmd.Parameters.Add((new SqlParameter("@Serltnum", SqlDbType.NVarChar, 21)));
                        //    cmd.Parameters.Add((new SqlParameter("@doc_id", SqlDbType.NVarChar, 12)));
                        //    cmd.Parameters.Add((new SqlParameter("@locncode", SqlDbType.NVarChar, 20)));

                        //    // hodnoty vstupnich parametru
                        //    cmd.Parameters["@Itemnmbr"].Value = itemnmbr ?? string.Empty;
                        //    cmd.Parameters["@Skl_id"].Value = skl_id ?? string.Empty;
                        //    cmd.Parameters["@Serltnum"].Value = serltnum ?? string.Empty;
                        //    cmd.Parameters["@doc_id"].Value = doc_id ?? string.Empty;
                        //    cmd.Parameters["@locncode"].Value = locncode ?? string.Empty;

                        //    conn.Open();
                        //    object result = cmd.ExecuteScalar();
                        //    vysledek = result?.ToString() ?? string.Empty;
                        //}

                        //// vysledek = SQL dotaz, např. "SELECT * FROM ... WHERE ...;"
                        //if (string.IsNullOrWhiteSpace(vysledek))
                        //    throw new ArgumentException("SQL dotaz (vysledek pro data z procedury) je prázdný.");


                        //    using (SqlConnection Sqlconnection = new SqlConnection(
                        //                         Globals.Konfigurace.ConnectionString[0].FASKDB))
                        //    using (SqlCommand Sqlcommand = new SqlCommand(vysledek, Sqlconnection))
                        //    using (SqlDataAdapter Sqladapter = new SqlDataAdapter(Sqlcommand))
                        //    {
                        //        Sqlcommand.CommandType = CommandType.Text;

                        //        Sqlconnection.Open();

                        //        Sqladapter.Fill(dsLocationPom, dsLocationPom.CZMST_SkladLokace_Stav.TableName);
                        //    }



                        ////MaR 18.8.2025 logika dopocitavani zda je sarze existuje nebo ne
                        ////// import do noveho datasetu
                        //if (dsLocationPom != null && dsLocationPom.CZMST_SkladLokace_Stav.Count > 0)
                        //{
                        //    DateTime dtnow = DateTime.Now;
                        //    foreach (var CZMST_SkladLokace_Stavrow in dsLocationPom.CZMST_SkladLokace_Stav)
                        //    {
                        //        Location.CZMST_SkladLokace_StavRow newrow = dsLocation.CZMST_SkladLokace_Stav.NewCZMST_SkladLokace_StavRow();
                        //        newrow.ITEMNMBR = CZMST_SkladLokace_Stavrow.ITEMNMBR;
                        //        newrow.ITEMDESC = CZMST_SkladLokace_Stavrow.IsITEMDESCNull() ? string.Empty : CZMST_SkladLokace_Stavrow.ITEMDESC;
                        //        newrow.QTYSHPPD_DEF = CZMST_SkladLokace_Stavrow.QTYSHPPD_DEF;
                        //        newrow.QTYSHPPD = CZMST_SkladLokace_Stavrow.QTYSHPPD;
                        //        newrow.SKL_ID = CZMST_SkladLokace_Stavrow.IsSKL_IDNull() ? string.Empty : CZMST_SkladLokace_Stavrow.SKL_ID;
                        //        newrow.LOCNCODE = CZMST_SkladLokace_Stavrow.IsLOCNCODENull() ? string.Empty : CZMST_SkladLokace_Stavrow.LOCNCODE;
                        //        newrow.DATECHANGE = dtnow;
                        //        if (!CZMST_SkladLokace_Stavrow.IsEXPIRATIONNull())
                        //            newrow.EXPIRATION = CZMST_SkladLokace_Stavrow.EXPIRATION;
                        //        newrow.Index = CZMST_SkladLokace_Stavrow.Index;
                        //        newrow.SERLTNUM = CZMST_SkladLokace_Stavrow.SERLTNUM;
                        //        if (!CZMST_SkladLokace_Stavrow.IsQTY_OWNERNull())
                        //            newrow.QTY_OWNER = CZMST_SkladLokace_Stavrow.QTY_OWNER;
                        //        newrow.PRAC_ID_OWNER = CZMST_SkladLokace_Stavrow.IsPRAC_ID_OWNERNull() ? string.Empty : CZMST_SkladLokace_Stavrow.PRAC_ID_OWNER;

                        //        if (!CZMST_SkladLokace_Stavrow.IsPRIJEMNull())
                        //            newrow.PRIJEM = CZMST_SkladLokace_Stavrow.PRIJEM;
                        //        if (!CZMST_SkladLokace_Stavrow.IsRAZENINull())
                        //            newrow.RAZENI = CZMST_SkladLokace_Stavrow.RAZENI;
                        //        if (!CZMST_SkladLokace_Stavrow.IsBLOKACENull())
                        //            newrow.BLOKACE = CZMST_SkladLokace_Stavrow.BLOKACE;


                        //        if (CZMST_SkladLokace_Stavrow.SERLTNUM == serltnum)
                        //        {
                        //            newrow.State = 0;
                        //            newrow.Message = "";
                        //        }
                        //        else
                        //        {
                        //            newrow.State = 1;
                        //            newrow.Message = "Šarže nenalezena";
                        //        }


                        //        dsLocation.CZMST_SkladLokace_Stav.AddCZMST_SkladLokace_StavRow(newrow);
                        //    }

                        //}
                        //else
                        //{
                        //    DateTime dtnow = DateTime.Now;
                        //    Location.CZMST_SkladLokace_StavRow newrow = dsLocation.CZMST_SkladLokace_Stav.NewCZMST_SkladLokace_StavRow();
                        //    newrow.ITEMNMBR = string.Empty;
                        //    //newrow.ITEMDESC = CZMST_SkladLokace_Stavrow.IsITEMDESCNull() ? string.Empty : CZMST_SkladLokace_Stavrow.ITEMDESC;
                        //    newrow.QTYSHPPD_DEF = 0;
                        //    newrow.QTYSHPPD = 0;
                        //    //newrow.SKL_ID = CZMST_SkladLokace_Stavrow.IsSKL_IDNull() ? string.Empty : CZMST_SkladLokace_Stavrow.SKL_ID;
                        //    //newrow.LOCNCODE = CZMST_SkladLokace_Stavrow.IsLOCNCODENull() ? string.Empty : CZMST_SkladLokace_Stavrow.LOCNCODE;
                        //    newrow.DATECHANGE = dtnow;
                        //    //if (!CZMST_SkladLokace_Stavrow.IsEXPIRATIONNull())
                        //    //    newrow.EXPIRATION = CZMST_SkladLokace_Stavrow.EXPIRATION;
                        //    newrow.Index = 1;
                        //    newrow.SERLTNUM = string.Empty;
                        //    //if (!CZMST_SkladLokace_Stavrow.IsQTY_OWNERNull())
                        //    //    newrow.QTY_OWNER = CZMST_SkladLokace_Stavrow.QTY_OWNER;
                        //    //newrow.PRAC_ID_OWNER = CZMST_SkladLokace_Stavrow.IsPRAC_ID_OWNERNull() ? string.Empty : CZMST_SkladLokace_Stavrow.PRAC_ID_OWNER;

                        //    //if (!CZMST_SkladLokace_Stavrow.IsPRIJEMNull())
                        //    //    newrow.PRIJEM = CZMST_SkladLokace_Stavrow.PRIJEM;
                        //    //if (!CZMST_SkladLokace_Stavrow.IsRAZENINull())
                        //    //    newrow.RAZENI = CZMST_SkladLokace_Stavrow.RAZENI;
                        //    //if (!CZMST_SkladLokace_Stavrow.IsBLOKACENull())
                        //    //    newrow.BLOKACE = CZMST_SkladLokace_Stavrow.BLOKACE;


                        //        newrow.State = 1;
                        //        newrow.Message = "Záznam nenalezen";



                        //    dsLocation.CZMST_SkladLokace_Stav.AddCZMST_SkladLokace_StavRow(newrow);
                        //} 
                        #endregion

                        #region 6.1.2026 NEW MaR pozadavek na rozsireni vyberu databaze od JaS

                        FirebirdSql.Data.FirebirdClient.FbConnection connection1 = null;
                        FirebirdSql.Data.FirebirdClient.FbCommand command1 = null;
                        FirebirdSql.Data.FirebirdClient.FbDataAdapter adapter1 = null;

                        string sqlText = string.Empty;
                        Location dsLocationPom = new Location();
                        using (SqlConnection conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                        using (SqlCommand cmd = new SqlCommand(FEFOFIFO_proc, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // parametry
                            cmd.Parameters.Add((new SqlParameter("@Itemnmbr", SqlDbType.NVarChar, 31)));
                            cmd.Parameters.Add((new SqlParameter("@Skl_id", SqlDbType.NVarChar, 20)));
                            cmd.Parameters.Add((new SqlParameter("@Serltnum", SqlDbType.NVarChar, 21)));
                            cmd.Parameters.Add((new SqlParameter("@doc_id", SqlDbType.NVarChar, 12)));
                            cmd.Parameters.Add((new SqlParameter("@locncode", SqlDbType.NVarChar, 20)));

                            // hodnoty vstupnich parametru
                            cmd.Parameters["@Itemnmbr"].Value = itemnmbr ?? string.Empty;
                            cmd.Parameters["@Skl_id"].Value = skl_id ?? string.Empty;
                            cmd.Parameters["@Serltnum"].Value = serltnum ?? string.Empty;
                            cmd.Parameters["@doc_id"].Value = doc_id ?? string.Empty;
                            cmd.Parameters["@locncode"].Value = locncode ?? string.Empty;

                            conn.Open();
                            object result = cmd.ExecuteScalar();
                            sqlText = result?.ToString() ?? string.Empty;
                        }

                        // vysledek = SQL dotaz, např. "SELECT * FROM ... WHERE ...;"
                        if (string.IsNullOrWhiteSpace(sqlText))
                            throw new ArgumentException("SQL dotaz (vysledek pro data z procedury) je prázdný.");


                        SQL_Datasets.SQL.dbtypesRow activeDb = null;

                        int activeCount = 0;

                        foreach (SQL_Datasets.SQL.dbtypesRow row in Globals.Konfigurace.dbtypes)
                        {
                            if (row.use)
                            {
                                activeCount++;
                                if (activeDb == null)
                                    activeDb = row;
                            }
                        }

                        if (activeCount == 0)
                            throw new InvalidOperationException("V konfiguraci dbtypes není žádná aktivní DB (use=true).");

                        if (activeCount > 1)
                        {
                            // (volitelné) vypsání typů aktivních DB bez LINQ
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            foreach (SQL_Datasets.SQL.dbtypesRow row in Globals.Konfigurace.dbtypes)
                            {
                                if (!row.use) continue;
                                if (sb.Length > 0) sb.Append(", ");
                                sb.Append(row.dbtype);
                            }

                            throw new InvalidOperationException(
                                "V konfiguraci dbtypes je více aktivních DB (use=true): " + sb.ToString() + ". Musí být právě jedna.");
                        }

                        string dbType = (activeDb.dbtype ?? string.Empty).Trim().ToLowerInvariant();

                        if (dbType == "firebird")
                        {
                            using (connection1 = new FirebirdSql.Data.FirebirdClient.FbConnection(activeDb.dbconnectionstring))
                            using (command1 = new FirebirdSql.Data.FirebirdClient.FbCommand(sqlText, connection1))
                            using (adapter1 = new FirebirdSql.Data.FirebirdClient.FbDataAdapter(command1))
                            {
                                command1.CommandType = CommandType.Text;
                                connection1.Open();
                                adapter1.Fill(dsLocationPom, dsLocationPom.CZMST_SkladLokace_Stav.TableName);
                            }
                        }
                        else if (dbType == "ms-sql")
                        {
                            using (SqlConnection Sqlconnection = new SqlConnection(activeDb.dbconnectionstring))
                            using (SqlCommand Sqlcommand = new SqlCommand(sqlText, Sqlconnection))
                            using (SqlDataAdapter Sqladapter = new SqlDataAdapter(Sqlcommand))
                            {
                                Sqlcommand.CommandType = CommandType.Text;
                                Sqlconnection.Open();
                                Sqladapter.Fill(dsLocationPom, dsLocationPom.CZMST_SkladLokace_Stav.TableName);
                            }
                        }
                        else if (dbType == "postgresql")
                        {
                            using (var pgConnection = new NpgsqlConnection(activeDb.dbconnectionstring))
                            using (var pgCommand = new NpgsqlCommand(sqlText, pgConnection))
                            using (var pgAdapter = new NpgsqlDataAdapter(pgCommand))
                            {
                                pgCommand.CommandType = CommandType.Text;
                                pgConnection.Open();
                                pgAdapter.Fill(dsLocationPom, dsLocationPom.CZMST_SkladLokace_Stav.TableName);
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException($"Neznámý typ databáze v dbtypes: '{activeDb.dbtype}'.");
                        }



                        //MaR 18.8.2025 logika dopocitavani zda je sarze existuje nebo ne
                        //// import do noveho datasetu
                        if (dsLocationPom != null && dsLocationPom.CZMST_SkladLokace_Stav.Count > 0)
                        {
                            DateTime dtnow = DateTime.Now;
                            foreach (var CZMST_SkladLokace_Stavrow in dsLocationPom.CZMST_SkladLokace_Stav)
                            {
                                Location.CZMST_SkladLokace_StavRow newrow = dsLocation.CZMST_SkladLokace_Stav.NewCZMST_SkladLokace_StavRow();
                                newrow.ITEMNMBR = CZMST_SkladLokace_Stavrow.ITEMNMBR;
                                newrow.ITEMDESC = CZMST_SkladLokace_Stavrow.IsITEMDESCNull() ? string.Empty : CZMST_SkladLokace_Stavrow.ITEMDESC;
                                newrow.QTYSHPPD_DEF = CZMST_SkladLokace_Stavrow.QTYSHPPD_DEF;
                                newrow.QTYSHPPD = CZMST_SkladLokace_Stavrow.QTYSHPPD;
                                newrow.SKL_ID = CZMST_SkladLokace_Stavrow.IsSKL_IDNull() ? string.Empty : CZMST_SkladLokace_Stavrow.SKL_ID;
                                newrow.LOCNCODE = CZMST_SkladLokace_Stavrow.IsLOCNCODENull() ? string.Empty : CZMST_SkladLokace_Stavrow.LOCNCODE;
                                newrow.DATECHANGE = dtnow;
                                if (!CZMST_SkladLokace_Stavrow.IsEXPIRATIONNull())
                                    newrow.EXPIRATION = CZMST_SkladLokace_Stavrow.EXPIRATION;
                                newrow.Index = CZMST_SkladLokace_Stavrow.Index;
                                newrow.SERLTNUM = CZMST_SkladLokace_Stavrow.SERLTNUM;
                                if (!CZMST_SkladLokace_Stavrow.IsQTY_OWNERNull())
                                    newrow.QTY_OWNER = CZMST_SkladLokace_Stavrow.QTY_OWNER;
                                newrow.PRAC_ID_OWNER = CZMST_SkladLokace_Stavrow.IsPRAC_ID_OWNERNull() ? string.Empty : CZMST_SkladLokace_Stavrow.PRAC_ID_OWNER;

                                if (!CZMST_SkladLokace_Stavrow.IsPRIJEMNull())
                                    newrow.PRIJEM = CZMST_SkladLokace_Stavrow.PRIJEM;
                                if (!CZMST_SkladLokace_Stavrow.IsRAZENINull())
                                    newrow.RAZENI = CZMST_SkladLokace_Stavrow.RAZENI;
                                if (!CZMST_SkladLokace_Stavrow.IsBLOKACENull())
                                    newrow.BLOKACE = CZMST_SkladLokace_Stavrow.BLOKACE;


                                if (CZMST_SkladLokace_Stavrow.SERLTNUM == serltnum)
                                {
                                    newrow.State = 0;
                                    newrow.Message = "";
                                }
                                else
                                {
                                    newrow.State = 1;
                                    newrow.Message = "Šarže nenalezena";
                                }


                                dsLocation.CZMST_SkladLokace_Stav.AddCZMST_SkladLokace_StavRow(newrow);
                            }
                        }
                        else
                        {
                            DateTime dtnow = DateTime.Now;
                            Location.CZMST_SkladLokace_StavRow newrow = dsLocation.CZMST_SkladLokace_Stav.NewCZMST_SkladLokace_StavRow();
                            newrow.ITEMNMBR = string.Empty;
                            //newrow.ITEMDESC = CZMST_SkladLokace_Stavrow.IsITEMDESCNull() ? string.Empty : CZMST_SkladLokace_Stavrow.ITEMDESC;
                            newrow.QTYSHPPD_DEF = 0;
                            newrow.QTYSHPPD = 0;
                            //newrow.SKL_ID = CZMST_SkladLokace_Stavrow.IsSKL_IDNull() ? string.Empty : CZMST_SkladLokace_Stavrow.SKL_ID;
                            //newrow.LOCNCODE = CZMST_SkladLokace_Stavrow.IsLOCNCODENull() ? string.Empty : CZMST_SkladLokace_Stavrow.LOCNCODE;
                            newrow.DATECHANGE = dtnow;
                            //if (!CZMST_SkladLokace_Stavrow.IsEXPIRATIONNull())
                            //    newrow.EXPIRATION = CZMST_SkladLokace_Stavrow.EXPIRATION;
                            newrow.Index = 1;
                            newrow.SERLTNUM = string.Empty;
                            //if (!CZMST_SkladLokace_Stavrow.IsQTY_OWNERNull())
                            //    newrow.QTY_OWNER = CZMST_SkladLokace_Stavrow.QTY_OWNER;
                            //newrow.PRAC_ID_OWNER = CZMST_SkladLokace_Stavrow.IsPRAC_ID_OWNERNull() ? string.Empty : CZMST_SkladLokace_Stavrow.PRAC_ID_OWNER;

                            //if (!CZMST_SkladLokace_Stavrow.IsPRIJEMNull())
                            //    newrow.PRIJEM = CZMST_SkladLokace_Stavrow.PRIJEM;
                            //if (!CZMST_SkladLokace_Stavrow.IsRAZENINull())
                            //    newrow.RAZENI = CZMST_SkladLokace_Stavrow.RAZENI;
                            //if (!CZMST_SkladLokace_Stavrow.IsBLOKACENull())
                            //    newrow.BLOKACE = CZMST_SkladLokace_Stavrow.BLOKACE;


                            newrow.State = 1;
                            newrow.Message = "Záznam nenalezen";



                            dsLocation.CZMST_SkladLokace_Stav.AddCZMST_SkladLokace_StavRow(newrow);
                        }

                        #endregion

                    }



                }
                else if (commandType == CommandType.Text)
                {
                    // command.CommandText = "Select * from " + FEFOFIFO_proc + " where " + mnozstvinasklade_proc2_paramName1 + "='" + ItemNumber + "' and " + mnozstvinasklade_proc2_paramName2 + "='" + Location + "'";
                }
                else
                {
                    throw new Exception("Neznámý typ příkazu: " + commandType.ToString());
                }
                return dsLocation;

            }
            catch (Exception ex)
            {
                //Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, " (Informations: MnozstviNaSklade[" + ItemNumber + "," + Location + "])");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                throw ex;
            }

            finally
            {
                if (connection != null && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

    }
}
