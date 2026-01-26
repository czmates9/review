using Fask.Server.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace Fask.Module.ABRA.SAB.Provider
{
    public partial class Provider : Fask.Server.Interfaces.Informations.IInformations2_FEFOFIFO
    {
        public Location FEFOFIFO_Online(string itemnmbr, string skl_id, string serltnum, string doc_id, string locncode)
        {
            //throw new NotImplementedException();
            //Globals_V1.LoadConfiguration();

            Obecne ds = new Obecne();
            Location dsLocation = new Location();
            FirebirdSql.Data.FirebirdClient.FbConnection connection = null;
            FirebirdSql.Data.FirebirdClient.FbCommand command = null;
            FirebirdSql.Data.FirebirdClient.FbDataAdapter adapter = null;
            string FEFOFIFO_proc = Globals_V1.Konfigurace.Informations[0].FEFOFIFO_proc;
            try
            {
                //Globals.LoadConfiguration();
                CommandType commandType = (CommandType)Enum.Parse(typeof(CommandType), Globals_V1.Konfigurace.Informations[0].FEFOFIFO_I_L_CommandType);
               
                if (commandType == CommandType.StoredProcedure)
                {

                    if (Globals_V1.Konfigurace.Informations[0].FEFOFIFO_pouzita_metoda ==1)
                    {
                        if (FEFOFIFO_proc.Length != 0)
                        {
                            connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
                            command = new FirebirdSql.Data.FirebirdClient.FbCommand(FEFOFIFO_proc);
                            command.CommandType = CommandType.StoredProcedure;


                            // parametry
                            command.Parameters.Add(new FbParameter("@Itemnmbr", FbDbType.VarChar, 31));
                            command.Parameters.Add(new FbParameter("@Skl_id", FbDbType.VarChar, 20));
                            command.Parameters.Add(new FbParameter("@Serltnum", FbDbType.VarChar, 21));
                            command.Parameters.Add(new FbParameter("@doc_id", FbDbType.VarChar, 12));
                            command.Parameters.Add(new FbParameter("@locncode", FbDbType.VarChar, 20));


                            // hodnoty vstupnich parametru
                            command.Parameters["@Itemnmbr"].Value = itemnmbr ?? string.Empty;
                            command.Parameters["@Skl_id"].Value = skl_id ?? string.Empty;
                            command.Parameters["@Serltnum"].Value = serltnum ?? string.Empty;
                            command.Parameters["@doc_id"].Value = doc_id ?? string.Empty;
                            command.Parameters["@locncode"].Value = locncode ?? string.Empty;

                            command.Connection = connection;
                            connection.Open();

                            adapter = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
                            adapter.SelectCommand = command;

                            // naplneni puvodniho (stareho) datasetu
                            adapter.Fill(ds, ds.Palety.TableName);

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
                    else if(Globals_V1.Konfigurace.Informations[0].FEFOFIFO_pouzita_metoda == 2)
                    {
                        if (FEFOFIFO_proc.Length != 0)
                        {
                            connection = new FirebirdSql.Data.FirebirdClient.FbConnection(Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB);
                            command = new FirebirdSql.Data.FirebirdClient.FbCommand(FEFOFIFO_proc);
                            command.CommandType = CommandType.StoredProcedure;


                            // parametry
                            command.Parameters.Add(new FbParameter("@Itemnmbr", FbDbType.VarChar, 31));
                            command.Parameters.Add(new FbParameter("@Skl_id", FbDbType.VarChar, 20));
                            command.Parameters.Add(new FbParameter("@Serltnum", FbDbType.VarChar, 21));
                            command.Parameters.Add(new FbParameter("@doc_id", FbDbType.VarChar, 12));
                            command.Parameters.Add(new FbParameter("@locncode", FbDbType.VarChar, 20));


                            // hodnoty vstupnich parametru
                            command.Parameters["@Itemnmbr"].Value = itemnmbr ?? string.Empty;
                            command.Parameters["@Skl_id"].Value = skl_id ?? string.Empty;
                            command.Parameters["@Serltnum"].Value = serltnum ?? string.Empty;
                            command.Parameters["@doc_id"].Value = doc_id ?? string.Empty;
                            command.Parameters["@locncode"].Value = locncode ?? string.Empty;

                            command.Connection = connection;
                            connection.Open();

                            adapter = new FirebirdSql.Data.FirebirdClient.FbDataAdapter();
                            adapter.SelectCommand = command;

                            adapter.Fill(dsLocation, dsLocation.CZMST_SkladLokace_Stav.TableName);
                            //// naplneni puvodniho (stareho) datasetu
                            //adapter.Fill(ds, ds.Palety.TableName);

                            //// import do noveho datasetu
                            //if (ds != null && ds.Palety.Count > 0)
                            //{
                            //    DateTime dtnow = DateTime.Now;
                            //    foreach (var paletyrow in ds.Palety)
                            //    {
                            //        Location.CZMST_SkladLokace_StavRow newrow = dsLocation.CZMST_SkladLokace_Stav.NewCZMST_SkladLokace_StavRow();
                            //        newrow.Index = paletyrow.Index;
                            //        newrow.ITEMNMBR = paletyrow.IsITEMNMBRNull() ? string.Empty : paletyrow.ITEMNMBR;
                            //        newrow.ITEMDESC = paletyrow.IsITEMDESCNull() ? string.Empty : paletyrow.ITEMDESC;
                            //        newrow.QTYSHPPD_DEF = paletyrow.QTYSHPPD;
                            //        newrow.QTYSHPPD = paletyrow.QTYSHPPD;
                            //        newrow.SERLTNUM = paletyrow.IsSERLTNUMNull() ? string.Empty : paletyrow.SERLTNUM;
                            //        newrow.SKL_ID = paletyrow.IsSKL_IDNull() ? string.Empty : paletyrow.SKL_ID;
                            //        newrow.LOCNCODE = paletyrow.IsLOCNCODENull() ? string.Empty : paletyrow.LOCNCODE;
                            //        newrow.DATECHANGE = dtnow;
                            //        //newrow.SetEXPIRATIONNull();
                            //        if (!paletyrow.IsEXPIRACENull())
                            //            newrow.EXPIRATION = paletyrow.EXPIRACE;
                            //        if (!paletyrow.IsPRIJEMNull())
                            //            newrow.PRIJEM = paletyrow.PRIJEM;
                            //        if (!paletyrow.IsRAZENINull())
                            //            newrow.RAZENI = paletyrow.RAZENI;
                            //        if (!paletyrow.IsBLOKACENull())
                            //            newrow.BLOKACE = paletyrow.BLOKACE;

                            //        dsLocation.CZMST_SkladLokace_Stav.AddCZMST_SkladLokace_StavRow(newrow);
                            //    }
                            //}
                        }
                    }
                    else if (Globals_V1.Konfigurace.Informations[0].FEFOFIFO_pouzita_metoda == 3)
                    {
                        string vysledek = string.Empty;
                      
                        using (SqlConnection conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
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


                        //Blok pro FireBird
                        if (Globals_V1.Konfigurace.Informations[0].FEFOFIFO_M3 == "firebird")
                        {
                            using (connection = new FirebirdSql.Data.FirebirdClient.FbConnection(
                               Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
                            using (command = new FirebirdSql.Data.FirebirdClient.FbCommand(vysledek, connection))
                            using (adapter = new FirebirdSql.Data.FirebirdClient.FbDataAdapter(command))
                            {
                                command.CommandType = CommandType.Text;

                                connection.Open();

                                adapter.Fill(dsLocation, dsLocation.CZMST_SkladLokace_Stav.TableName);
                            } 
                        }
                        //blok pro SQL
                        else if (Globals_V1.Konfigurace.Informations[0].FEFOFIFO_M3 == "ms-sql")
                        {
                            using (SqlConnection Sqlconnection = new SqlConnection(
                                                 Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                            using (SqlCommand Sqlcommand = new SqlCommand(vysledek, Sqlconnection))
                            using (SqlDataAdapter Sqladapter = new SqlDataAdapter(Sqlcommand))
                            {
                                Sqlcommand.CommandType = CommandType.Text;

                                Sqlconnection.Open();

                                Sqladapter.Fill(dsLocation, dsLocation.CZMST_SkladLokace_Stav.TableName);
                            } 
                        }


                    }
                    else if (Globals_V1.Konfigurace.Informations[0].FEFOFIFO_pouzita_metoda == 4)
                    {
                        string vysledek = string.Empty;
                        Location dsLocationPom = new Location();
                        using (SqlConnection conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
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


                        //Blok pro FireBird
                        if (Globals_V1.Konfigurace.Informations[0].FEFOFIFO_M3 == "firebird")
                        {
                            using (connection = new FirebirdSql.Data.FirebirdClient.FbConnection(
                               Globals_V1.Konfigurace.ConnectionStrings[0].ABRADB))
                            using (command = new FirebirdSql.Data.FirebirdClient.FbCommand(vysledek, connection))
                            using (adapter = new FirebirdSql.Data.FirebirdClient.FbDataAdapter(command))
                            {
                                command.CommandType = CommandType.Text;

                                connection.Open();

                                adapter.Fill(dsLocationPom, dsLocationPom.CZMST_SkladLokace_Stav.TableName);
                            }
                        }
                        //blok pro SQL
                        else if (Globals_V1.Konfigurace.Informations[0].FEFOFIFO_M3 == "ms-sql")
                        {
                            using (SqlConnection Sqlconnection = new SqlConnection(
                                                 Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                            using (SqlCommand Sqlcommand = new SqlCommand(vysledek, Sqlconnection))
                            using (SqlDataAdapter Sqladapter = new SqlDataAdapter(Sqlcommand))
                            {
                                Sqlcommand.CommandType = CommandType.Text;

                                Sqlconnection.Open();

                                Sqladapter.Fill(dsLocationPom, dsLocationPom.CZMST_SkladLokace_Stav.TableName);
                            }
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
      
        /// <summary>
        /// Metoda ktera byla vychozi pro urceni FEFOFIFO z databaze SQL
        /// </summary>
        /// <param name="itemnmbr"></param>
        /// <param name="skl_id"></param>
        /// <param name="serltnum"></param>
        /// <param name="doc_id"></param>
        /// <param name="locncode"></param>
        /// <returns></returns>
        public Location Prodej_Online_GetMaterial_OLD(string itemnmbr, string skl_id, string serltnum, string doc_id, string locncode)
        {
            //throw new NotImplementedException();
            //Globals_V1.LoadConfiguration();

            Obecne ds = new Obecne();
            Location dsLocation = new Location();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            string FEFOFIFO_proc = Globals_V1.Konfigurace.Informations[0].FEFOFIFO_proc;
            try
            {
                //Globals.LoadConfiguration();
                CommandType commandType = (CommandType)Enum.Parse(typeof(CommandType), Globals_V1.Konfigurace.Informations[0].FEFOFIFO_I_L_CommandType);

                if (commandType == CommandType.StoredProcedure)
                {

                    if (Globals_V1.Konfigurace.Informations[0].FEFOFIFO_pouzita_metoda == 1)
                    {
                        if (FEFOFIFO_proc.Length != 0)
                        {
                            connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                            command = new SqlCommand(FEFOFIFO_proc);
                            command.CommandType = CommandType.StoredProcedure;


                            // parametry
                            command.Parameters.Add((new SqlParameter("@Itemnmbr", SqlDbType.NVarChar, 31)));
                            command.Parameters.Add((new SqlParameter("@Skl_id", SqlDbType.NVarChar, 20)));
                            command.Parameters.Add((new SqlParameter("@Serltnum", SqlDbType.NVarChar, 21)));
                            command.Parameters.Add((new SqlParameter("@doc_id", SqlDbType.NVarChar, 12)));
                            command.Parameters.Add((new SqlParameter("@locncode", SqlDbType.NVarChar, 20)));

                            // hodnoty vstupnich parametru
                            command.Parameters["@Itemnmbr"].Value = itemnmbr ?? string.Empty;
                            command.Parameters["@Skl_id"].Value = skl_id ?? string.Empty;
                            command.Parameters["@Serltnum"].Value = serltnum ?? string.Empty;
                            command.Parameters["@doc_id"].Value = doc_id ?? string.Empty;
                            command.Parameters["@locncode"].Value = locncode ?? string.Empty;

                            command.Connection = connection;
                            connection.Open();

                            adapter = new SqlDataAdapter();
                            adapter.SelectCommand = command;

                            // naplneni puvodniho (stareho) datasetu
                            adapter.Fill(ds, ds.Palety.TableName);

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
                    else if (Globals_V1.Konfigurace.Informations[0].FEFOFIFO_pouzita_metoda == 2)
                    {
                        if (FEFOFIFO_proc.Length != 0)
                        {
                            connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                            command = new SqlCommand(FEFOFIFO_proc);
                            command.CommandType = CommandType.StoredProcedure;


                            // parametry
                            command.Parameters.Add((new SqlParameter("@Itemnmbr", SqlDbType.NVarChar, 31)));
                            command.Parameters.Add((new SqlParameter("@Skl_id", SqlDbType.NVarChar, 20)));
                            command.Parameters.Add((new SqlParameter("@Serltnum", SqlDbType.NVarChar, 21)));
                            command.Parameters.Add((new SqlParameter("@doc_id", SqlDbType.NVarChar, 12)));
                            command.Parameters.Add((new SqlParameter("@locncode", SqlDbType.NVarChar, 20)));

                            // hodnoty vstupnich parametru
                            command.Parameters["@Itemnmbr"].Value = itemnmbr ?? string.Empty;
                            command.Parameters["@Skl_id"].Value = skl_id ?? string.Empty;
                            command.Parameters["@Serltnum"].Value = serltnum ?? string.Empty;
                            command.Parameters["@doc_id"].Value = doc_id ?? string.Empty;
                            command.Parameters["@locncode"].Value = locncode ?? string.Empty;

                            command.Connection = connection;
                            connection.Open();

                            adapter = new SqlDataAdapter();
                            adapter.SelectCommand = command;

                            adapter.Fill(dsLocation, dsLocation.CZMST_SkladLokace_Stav.TableName);
                            //// naplneni puvodniho (stareho) datasetu
                            //adapter.Fill(ds, ds.Palety.TableName);

                            //// import do noveho datasetu
                            //if (ds != null && ds.Palety.Count > 0)
                            //{
                            //    DateTime dtnow = DateTime.Now;
                            //    foreach (var paletyrow in ds.Palety)
                            //    {
                            //        Location.CZMST_SkladLokace_StavRow newrow = dsLocation.CZMST_SkladLokace_Stav.NewCZMST_SkladLokace_StavRow();
                            //        newrow.Index = paletyrow.Index;
                            //        newrow.ITEMNMBR = paletyrow.IsITEMNMBRNull() ? string.Empty : paletyrow.ITEMNMBR;
                            //        newrow.ITEMDESC = paletyrow.IsITEMDESCNull() ? string.Empty : paletyrow.ITEMDESC;
                            //        newrow.QTYSHPPD_DEF = paletyrow.QTYSHPPD;
                            //        newrow.QTYSHPPD = paletyrow.QTYSHPPD;
                            //        newrow.SERLTNUM = paletyrow.IsSERLTNUMNull() ? string.Empty : paletyrow.SERLTNUM;
                            //        newrow.SKL_ID = paletyrow.IsSKL_IDNull() ? string.Empty : paletyrow.SKL_ID;
                            //        newrow.LOCNCODE = paletyrow.IsLOCNCODENull() ? string.Empty : paletyrow.LOCNCODE;
                            //        newrow.DATECHANGE = dtnow;
                            //        //newrow.SetEXPIRATIONNull();
                            //        if (!paletyrow.IsEXPIRACENull())
                            //            newrow.EXPIRATION = paletyrow.EXPIRACE;
                            //        if (!paletyrow.IsPRIJEMNull())
                            //            newrow.PRIJEM = paletyrow.PRIJEM;
                            //        if (!paletyrow.IsRAZENINull())
                            //            newrow.RAZENI = paletyrow.RAZENI;
                            //        if (!paletyrow.IsBLOKACENull())
                            //            newrow.BLOKACE = paletyrow.BLOKACE;

                            //        dsLocation.CZMST_SkladLokace_Stav.AddCZMST_SkladLokace_StavRow(newrow);
                            //    }
                            //}
                        }
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
