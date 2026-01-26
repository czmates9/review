using Fask.Interfaces.DataSets;
using Fask.Interfaces.Vyroba.API;
using Fask.WEBAPI.API_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.SQL
{
    public partial class Provider : Server.Interfaces.Vyroba.IProduction, Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow, IVyroba_PostMSSEventRow
    {
        public bool AllowUserProductionOnMoreMachines
        {
            get;
            set;
        }

        public Fask.Interfaces.DataSets.Vyroba AllOpenedProductions()
        {
            try
            {
                // TODO: upravit metodu tak, aby prijimala jako parametry userid a machineid
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = new Fask.Interfaces.DataSets.Vyroba();
                System.Data.SqlClient.SqlDataAdapter ta_production = new System.Data.SqlClient.SqlDataAdapter();
                ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand(
                    "Select p.* " +
                    "From "+ Fask.SQL.Constants.Common.TABLE_PRODUCTION + " p with (nolock) " +
                    "Where not exists ( " +
                    "select SOUBEHGUID " +
                    "from Production with (nolock) " +
                    "where " +
                    "(TIMESTOP is not null or TIMEPREPSTOP is not null) " +
                    "and " +
                    "SOUBEHGUID=p.SOUBEHGUID " +
                    ") " +
                    "and SOUBEHGUID is not null " +
                    "order by id "
                    );
                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                ta_production.Fill(ds_vyroba.Production);

                return ds_vyroba;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetCiselniky(Fask.Interfaces.DataSets.Vyroba dsvyroba)
        {
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            da.SelectCommand = new System.Data.SqlClient.SqlCommand();
            da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            da.SelectCommand.CommandText = "Select * from Corrects";
            da.Fill(dsvyroba.Corrects);

            da.SelectCommand.CommandText = "Select * from Logins";
            da.Fill(dsvyroba.Logins);

            da.SelectCommand.CommandText = "Select * from Machines";
            da.Fill(dsvyroba.Machines);

            da.SelectCommand.CommandText = "Select * from Operations";
            da.Fill(dsvyroba.Operations);

            da.SelectCommand.CommandText = "Select * from VMachinesOperations";
            da.Fill(dsvyroba.VMachinesOperations);

            da.SelectCommand.CommandText = "Select * from StatusTypes";
            da.Fill(dsvyroba.StatusTypes);

            //da.SelectCommand.CommandText = "Select * from FASK_CONS_095";
            da.SelectCommand.CommandText = "SELECT Z.ITEMNMBR ,Z.ITEMDESC ,Z.VNDITNUM ,Z.CZ_CarKod ,Z.LOCNCODE ,Z.SKL_ID ,Z.QTY ,Z.QTYPACK ,Z.MJ ,Z.DMJ ,Z.TAXRATE ,Z.PRICE0 ,Z.PRICE1 ,Z.PRICE2 ,Z.PRICE3 ,Z.PRICE4 ,Z.PRICE5 ,Z.CZ_SerNum_Track ,Z.CZ_SerNum_Delka ,Z.CZ_Rez1_Track ,Z.CZ_Rez2_Track ,Z.CZ_Rez3_Track ,Z.CZ_Rez4_Track ,Z.REZ1 ,Z.DEX_ROW_ID ,Z.ITEMCODE ,Z.ODB_ID , ISNULL(P.RefVPrTIMEMODE,0) as TIMEMODE ,ISNULL(P.VPrTIMEPREP,0) as TIMEPREP ,ISNULL(P.VPrTIMEUNIT,0) as TIMEUNIT ,Z.TIMEFROM ,Z.TIMETO ,Z.LSTMod ,Z.loginid FROM FASK_ZASOBY as Z left join FASK_ZASOBY_PARAMETRY as P ON Z.ITEMNMBR = P.ITEMNMBR";
            da.Fill(dsvyroba.FASK_CONS_095);

            da.SelectCommand.CommandText = "Select * from FASK_Vyroba_TP";
            da.Fill(dsvyroba.FASK_Vyroba_TP);
        }

        public Fask.Interfaces.DataSets.Vyroba GetHlavicky(byte terminalID)
        {
            Fask.Interfaces.DataSets.Vyroba dsvyroba = new Fask.Interfaces.DataSets.Vyroba();

            System.Data.SqlClient.SqlDataAdapter odbcda = new System.Data.SqlClient.SqlDataAdapter();
            odbcda.SelectCommand = new System.Data.SqlClient.SqlCommand();
            odbcda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            odbcda.SelectCommand.CommandText = "Select * from CZPRO_VPH Where TermID=0 OR TermID=" + terminalID;
            odbcda.Fill(dsvyroba.CZPRO_VPH);

            return dsvyroba;
        }

        public Fask.Interfaces.DataSets.Vyroba GetPolozky(Server.Interfaces.Classes_Vyroba.VyrobniPrikazHlavicka hlavicka)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba dsvyroba = new Fask.Interfaces.DataSets.Vyroba();
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = @"SELECT * FROM CZPRO_VPP_View WHERE (CountEntries = @CountEntries) AND (SOPNUMBE = @SOPNUMBE)";
                        using (System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter())
                        {

                            adapter.SelectCommand = command;
                            adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = hlavicka.COUNTENTRIES });
                            adapter.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = hlavicka.SOPNUMBE == null ? (object)null : hlavicka.SOPNUMBE });
                            int returnValue = adapter.Fill(dsvyroba);
                            return dsvyroba;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        public void GetTables(Fask.Interfaces.DataSets.Vyroba dsvyroba)
        {
            try
            {
                System.Data.SqlClient.SqlDataAdapter sqlda = new System.Data.SqlClient.SqlDataAdapter();
                sqlda.SelectCommand = new System.Data.SqlClient.SqlCommand();
                sqlda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                sqlda.SelectCommand.CommandText = "Select * from CZPRO_VPH_View";
                sqlda.Fill(dsvyroba.CZPRO_VPH);

                sqlda.SelectCommand.CommandText = "Select * from CZPRO_VPP_View";
                sqlda.Fill(dsvyroba.CZPRO_VPP);

                sqlda.SelectCommand.CommandText = "Select * from Corrects";
                sqlda.Fill(dsvyroba.Corrects);

                #region Puvodne plneni...

                //sqlda.SelectCommand.CommandText = "Select * from Logins";
                //sqlda.Fill(dsvyroba.Logins); 

                #endregion

                sqlda.SelectCommand.CommandText = "Select * from Machines";
                sqlda.Fill(dsvyroba.Machines);

                sqlda.SelectCommand.CommandText = "Select * from Operations";
                sqlda.Fill(dsvyroba.Operations);

                sqlda.SelectCommand.CommandText = "Select * from VMachinesOperations";
                sqlda.Fill(dsvyroba.VMachinesOperations);

                sqlda.SelectCommand.CommandText = "Select * from StatusTypes";
                sqlda.Fill(dsvyroba.StatusTypes);

                //sqlda.SelectCommand.CommandText = "Select * from FASK_CONS_095";
                sqlda.SelectCommand.CommandText = "SELECT Z.ITEMNMBR ,Z.ITEMDESC ,Z.VNDITNUM ,Z.CZ_CarKod ,Z.LOCNCODE ,Z.SKL_ID ,Z.QTY ,Z.QTYPACK ,Z.MJ ,Z.DMJ ,Z.TAXRATE ,Z.PRICE0 ,Z.PRICE1 ,Z.PRICE2 ,Z.PRICE3 ,Z.PRICE4 ,Z.PRICE5 ,Z.CZ_SerNum_Track ,Z.CZ_SerNum_Delka ,Z.CZ_Rez1_Track ,Z.CZ_Rez2_Track ,Z.CZ_Rez3_Track ,Z.CZ_Rez4_Track ,Z.REZ1 ,Z.DEX_ROW_ID ,Z.ITEMCODE ,Z.ODB_ID , ISNULL(P.RefVPrTIMEMODE,0) as TIMEMODE ,ISNULL(P.VPrTIMEPREP,0) as TIMEPREP ,ISNULL(P.VPrTIMEUNIT,0) as TIMEUNIT ,Z.TIMEFROM ,Z.TIMETO ,Z.LSTMod ,Z.loginid FROM FASK_ZASOBY as Z left join FASK_ZASOBY_PARAMETRY as P ON Z.ITEMNMBR = P.ITEMNMBR";
                sqlda.Fill(dsvyroba.FASK_CONS_095);

                // Helios Orange => dotazeni skladu, lokaci skladu ...
                // definice v MST ... 
                sqlda.SelectCommand.CommandText = "Select * from CZMST093";
                sqlda.Fill(dsvyroba.CZMST093);

                // Helios Orange => dotazeni lokaci k natazenym skladum ...
                // HeO => pri generovani skladu je pouzit insert/delete trigger na generovani umisteni ... 
                sqlda.SelectCommand.CommandText = "Select * from CZMST094";
                sqlda.Fill(dsvyroba.CZMST094);

                sqlda.SelectCommand.CommandText = "Select * from FASK_Vyroba_TP";
                sqlda.Fill(dsvyroba.FASK_Vyroba_TP);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(dsvyroba);
                Logging.ExceptionHandler2.Handle(ex);
                dsvyroba = null;
            }
        }

        public Server.Interfaces.Classes_Vyroba.Report_UserDay Get_Report_UserDay(string UserID, DateTime datetimeLogin, DateTime datetimeLastOperation)
        {
            // pripojeni k sql serveru ...
            System.Data.SqlClient.SqlConnection sqlconnection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            // data adapter pro dotazy ...
            System.Data.SqlClient.SqlDataAdapter sqlda = new System.Data.SqlClient.SqlDataAdapter();

            Server.Interfaces.Classes_Vyroba.Report_UserDay rud = new Server.Interfaces.Classes_Vyroba.Report_UserDay();
            rud.UserID = UserID;
            rud.UserLogin = datetimeLogin;
            rud.UserLastOperation = datetimeLastOperation;

            // 3) vytahnout data uzivatele z rozsahu prihlaseni-odhlaseni/posledniakce
            // 4) spocitat z dat casy Normovany, Skutecny cas, celkovy cas korekci

            Fask.Interfaces.DataSets.Vyroba vds = new Fask.Interfaces.DataSets.Vyroba();

            //    // 3) vytahnout veskerou produkci uzivatele ... 

            System.Data.SqlClient.SqlCommand pcommand = new System.Data.SqlClient.SqlCommand();
            pcommand.Connection = sqlconnection;
            pcommand.CommandText = "Select * from " + Fask.SQL.Constants.Common.TABLE_PRODUCTION + " where userid=@userid and dateeve between @tStart and @tEnd order by dateeve desc";
            pcommand.Parameters.Clear();
            pcommand.Parameters.AddWithValue("@tStart", rud.UserLogin);
            pcommand.Parameters.AddWithValue("@tEnd", rud.UserLastOperation);
            pcommand.Parameters.AddWithValue("@userid", rud.UserID);

            sqlda.SelectCommand = pcommand;
            sqlda.Fill(vds.Production);

            // 4) vypocty casu ... 
            // a) cisla stroju
            var machines = vds.Production.Where(p => !p.IsmachineidNull()).GroupBy(p => p.machineid);
            machines.ToList().ForEach(p => rud.Machines.Add(p.Key));

            // b) celkem normovany cas
            decimal tNorm = vds.Production.Where(p => !p.IsTIMESTOPNull()).Sum(p => Convert.ToDecimal(p.TIMEUNIT) * p.qty + Convert.ToDecimal(p.TIMEPREP)) / 60;
            decimal tSkut = Convert.ToDecimal((rud.UserLastOperation - rud.UserLogin).TotalHours);
            decimal tKors = Convert.ToDecimal(vds.Production.Where(p => !p.IsTIMECORSTOPNull()).Sum(p => (p.TIMECORSTOP - p.TIMECORSTART).TotalHours));

            rud.TimeNorm = tNorm;
            rud.TimeReal = tSkut;
            rud.TimeCorrects = tKors;

            return rud;
        }

        public Fask.Interfaces.DataSets.Vyroba OpenedCorrection(string userID, string MachineID)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = new Fask.Interfaces.DataSets.Vyroba();
                System.Data.SqlClient.SqlDataAdapter ta_production = new System.Data.SqlClient.SqlDataAdapter();
                ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand(
                    "Select top 1 * " + //prvni vyskyt
                    "From " + Fask.SQL.Constants.Common.TABLE_PRODUCTION + " " +
                    "Where isnull(UserID,'')=@UserID " +
                    "AND isnull(MachineID,'')=@MachineID " +
                    "AND TIMECRID is not null " + //jedna se o korekci ... 
                    "Order by dateeve desc" +
                    ""
                    );

                ta_production.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                ta_production.SelectCommand.Parameters.AddWithValue("@MachineID", MachineID);

                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                ta_production.MissingSchemaAction = System.Data.MissingSchemaAction.Ignore;
                ta_production.Fill(ds_vyroba.Production);

                return ds_vyroba;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fask.Interfaces.DataSets.Vyroba OpenedProduction(string userID, string MachineID)
        {
            try
            {
                // true - bude podminka, folse - nebude
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = new Fask.Interfaces.DataSets.Vyroba();
                System.Data.SqlClient.SqlDataAdapter ta_production = new System.Data.SqlClient.SqlDataAdapter();
                ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand(
                    "Select top 1 * " + //jen 1. radek vyhovujici podmince
                    "From " + Fask.SQL.Constants.Common.TABLE_PRODUCTION + " " +
                    "Where isnull(UserID,'')=@UserID " +
                    (AllowUserProductionOnMoreMachines ? "AND isnull(MachineID,'')=@MachineID " : string.Empty) +
                    "AND TIMECRID is null " + //neni to korekce
                    "Order by dateeve desc" +
                    ""
                    );

                ta_production.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                ta_production.SelectCommand.Parameters.AddWithValue("@MachineID", MachineID);

                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                ta_production.MissingSchemaAction = System.Data.MissingSchemaAction.Ignore;
                ta_production.Fill(ds_vyroba.Production);

                if (ds_vyroba.Production.Count > 0
                    && ds_vyroba.Production[0].IsTIMESTOPNull()
                    && !ds_vyroba.Production[0].IsSOUBEHGUIDNull())
                {
                    return Soubeh(ds_vyroba.Production[0].SOUBEHGUID);
                }
                else
                {
                    return ds_vyroba;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fask.Interfaces.DataSets.Vyroba ProductionHistory(string UserID, string MachineID, string sopnumbe, DateTime? dateFrom, DateTime? dateTo, int nLastActions)
        {
            try
            {
                // 23.11.2016 JiS => dle Vyroba_P.ucHistorie.timerUpdateThreadStart() ...
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = new Fask.Interfaces.DataSets.Vyroba();
                System.Data.SqlClient.SqlDataAdapter ta_production = new System.Data.SqlClient.SqlDataAdapter();

                ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand("Select * From " + Fask.SQL.Constants.Common.TABLE_PRODUCTION + " ");

                string whereConditions = string.Empty;

                // filtruje se podle datumu, je třeba načíst všechna data
                if (!dateFrom.HasValue && !dateTo.HasValue)
                {
                    whereConditions += (whereConditions.Length > 0 ? "and " : "") + "dateeve > @datum ";
                    ta_production.SelectCommand.Parameters.AddWithValue("@datum", DateTime.Now.AddHours(-nLastActions));
                }
                else
                {
                    whereConditions += (whereConditions.Length > 0 ? "and " : "") + "dateeve >= @datumOd and dateeve <= @datumDo ";
                    ta_production.SelectCommand.Parameters.AddWithValue("@datumOd", dateFrom ?? DateTime.Now.AddHours(-nLastActions));
                    ta_production.SelectCommand.Parameters.AddWithValue("@datumDo", dateTo ?? DateTime.Now);
                }

                if (!String.IsNullOrEmpty(UserID))
                {
                    whereConditions += (whereConditions.Length > 0 ? "and " : "") + "UserID=@userID ";
                    ta_production.SelectCommand.Parameters.AddWithValue("@userID", UserID);
                }

                if (!String.IsNullOrEmpty(MachineID))
                {
                    whereConditions += (whereConditions.Length > 0 ? "and " : "") + "MachineID=@machineID ";
                    ta_production.SelectCommand.Parameters.AddWithValue("@machineID", MachineID);
                }

                if (!String.IsNullOrEmpty(sopnumbe))
                {
                    whereConditions += (whereConditions.Length > 0 ? "and " : "") + "SOPNUMBE=@sopnumbe ";
                    ta_production.SelectCommand.Parameters.AddWithValue("@sopnumbe", sopnumbe);
                }

                ta_production.SelectCommand.CommandText += "WHERE " + whereConditions;
                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                ta_production.Fill(ds_vyroba.Production);

                return ds_vyroba;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fask.Interfaces.DataSets.Vyroba ProductionHistoryFilter(Server.Interfaces.Classes_Vyroba.FiltersHistory filtersHistory, int nLastHours)
        {
            try
            {
                // 23.11.2016 JiS => dle Vyroba_P.ucHistorie.timerUpdateThreadStart() ...
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = new Fask.Interfaces.DataSets.Vyroba();
                System.Data.SqlClient.SqlDataAdapter ta_production = new System.Data.SqlClient.SqlDataAdapter();

                ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand("Select * From " + Fask.SQL.Constants.Common.TABLE_PRODUCTION + " ");

                string whereConditions = string.Empty;

                // filtruje se podle datumu, je třeba načíst všechna data
                if (filtersHistory != null)
                {
                    if (!filtersHistory.filtrDatumOd.HasValue && !filtersHistory.filtrDatumDo.HasValue)
                    {
                        whereConditions += (whereConditions.Length > 0 ? "and " : "") + "dateeve > @datum ";
                        ta_production.SelectCommand.Parameters.AddWithValue("@datum", DateTime.Now.AddHours(-nLastHours));
                    }
                    else
                    {
                        whereConditions += (whereConditions.Length > 0 ? "and " : "") + "dateeve >= @datumOd and dateeve <= @datumDo ";
                        ta_production.SelectCommand.Parameters.AddWithValue("@datumOd", filtersHistory.filtrDatumOd ?? DateTime.Now.AddHours(-nLastHours));
                        ta_production.SelectCommand.Parameters.AddWithValue("@datumDo", filtersHistory.filtrDatumDo ?? DateTime.Now);
                    }

                    if (!String.IsNullOrEmpty(filtersHistory.filtrOsoba))
                    {
                        whereConditions += (whereConditions.Length > 0 ? "and " : "") + "UserID=@userID ";
                        ta_production.SelectCommand.Parameters.AddWithValue("@userID", filtersHistory.filtrOsoba);
                    }

                    if (!String.IsNullOrEmpty(filtersHistory.filtrStroj))
                    {
                        whereConditions += (whereConditions.Length > 0 ? "and " : "") + "MachineID=@machineID ";
                        ta_production.SelectCommand.Parameters.AddWithValue("@machineID", filtersHistory.filtrStroj);
                    }

                    if (!String.IsNullOrEmpty(filtersHistory.filtrZakazka))
                    {
                        whereConditions += (whereConditions.Length > 0 ? "and " : "") + "SOPNUMBE=@sopnumbe ";
                        ta_production.SelectCommand.Parameters.AddWithValue("@sopnumbe", filtersHistory.filtrZakazka);
                    }
                }
                else
                {
                    whereConditions += (whereConditions.Length > 0 ? "and " : "") + "dateeve >= @datumOd and dateeve <= @datumDo ";
                    ta_production.SelectCommand.Parameters.AddWithValue("@datumOd", filtersHistory.filtrDatumOd ?? DateTime.Now.AddHours(-nLastHours));
                    ta_production.SelectCommand.Parameters.AddWithValue("@datumDo", filtersHistory.filtrDatumDo ?? DateTime.Now);
                }

                ta_production.SelectCommand.CommandText += "WHERE " + whereConditions;
                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                ta_production.Fill(ds_vyroba.Production);

                return ds_vyroba;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fask.Interfaces.DataSets.Vyroba ProductionLastAction(string UserID, string MachineID, bool? Corrections, int nLastActions)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = new Fask.Interfaces.DataSets.Vyroba();
                System.Data.SqlClient.SqlDataAdapter ta_production = new System.Data.SqlClient.SqlDataAdapter();
                ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand(
                    "Select top " + nLastActions + " * " +
                    "From " + Fask.SQL.Constants.Common.TABLE_PRODUCTION + " " +
                    "Where isnull(UserID,'')=@UserID " +
                    "AND isnull(MachineID,'')=@MachineID " +
                    (Corrections.HasValue ? (Corrections.Value ? "AND TIMECRID is not null " : "AND TIMECRID is null ") : "") +
                    "Order by dateeve desc" +
                    ""
                    );

                ta_production.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
                ta_production.SelectCommand.Parameters.AddWithValue("@MachineID", MachineID);

                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                ta_production.Fill(ds_vyroba.Production);

                return ds_vyroba;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fask.Interfaces.DataSets.Vyroba_StatistikaOdvadeni Production_Filter(Server.Interfaces.Classes_Vyroba.FiltersHistory filter)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba_StatistikaOdvadeni ds = new Fask.Interfaces.DataSets.Vyroba_StatistikaOdvadeni();

                System.Data.SqlClient.SqlConnection sqlconnection = null;
                System.Data.SqlClient.SqlCommand sqlcommand = null;
                sqlconnection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                sqlcommand = new System.Data.SqlClient.SqlCommand();
                sqlcommand.CommandTimeout = 1000;
                sqlcommand.Connection = sqlconnection;

                sqlcommand.CommandType = System.Data.CommandType.Text;
                sqlcommand.CommandText = "Select * from FASK_Filter(@in_DatumOd, @in_DatumDo, @in_NedokonceneZakazky, @in_Osoba, @in_Stroj, @in_Zakazka)";

                sqlcommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@in_DatumOd", (DateTime)filter.filtrDatumOd));
                sqlcommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@in_DatumDo", (DateTime)filter.filtrDatumDo));
                sqlcommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@in_NedokonceneZakazky", filter.filtrNedokonceneZakazky));
                sqlcommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@in_Osoba", filter.filtrOsoba));
                sqlcommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@in_Stroj", filter.filtrStroj));
                sqlcommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@in_Zakazka", filter.filtrZakazka));

                sqlcommand.Connection.Open();

                SqlDataReader o = sqlcommand.ExecuteReader();

                foreach (Fask.Interfaces.DataSets.Vyroba_StatistikaOdvadeni.FASK_FilterRow item in o)
                {
                    Fask.Interfaces.DataSets.Vyroba_StatistikaOdvadeni.FASK_FilterRow row = ds.FASK_Filter.NewFASK_FilterRow();
                    row = item;
                    ds.FASK_Filter.AddFASK_FilterRow(row);
                }
                ds.FASK_Filter.AcceptChanges();

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fask.Interfaces.DataSets.Vyroba Soubeh(Guid soubehGUID)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = new Fask.Interfaces.DataSets.Vyroba();
                System.Data.SqlClient.SqlDataAdapter ta_production = new System.Data.SqlClient.SqlDataAdapter();
                ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand(
                    "Select * " +
                    "From " + Fask.SQL.Constants.Common.TABLE_PRODUCTION + " " +
                    "Where SoubehGUID=@SoubehGUID " +
                    "Order by dateeve desc" +
                    ""
                    );

                ta_production.SelectCommand.Parameters.AddWithValue("@SoubehGUID", soubehGUID);

                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                ta_production.MissingSchemaAction = System.Data.MissingSchemaAction.Ignore;
                ta_production.Fill(ds_vyroba.Production);

                return ds_vyroba;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateProduction(Fask.Interfaces.DataSets.Vyroba.ProductionDataTable dtProduction)
        {
            //Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.ProductionTableAdapter tap = new Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            SqlConnection con;
            con = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            System.Data.SqlClient.SqlTransaction sqltrans = null;
            try
            {
                // v rámci transakce projít všechny věci (všechny záznamy zkontrolovat na GUID a následně provést update pomocí transakce)
                con.Open();
                sqltrans = con.BeginTransaction();
                //tap.Adapter.InsertCommand.Transaction = sqltrans;

                //tap.Transaction = sqltrans;

                int termid = 0;

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Zacatek zpracovani zaznamu " + Fask.SQL.Constants.Common.TABLE_PRODUCTION + "(TID:" + termid + ")");
                //foreach (Production.DataServices.VyrobaDataSet.ProductionRow item in dtProduction.Select(null, "dateeve"))
                foreach (Fask.Interfaces.DataSets.Vyroba.ProductionRow item in dtProduction.OrderBy(x => x.dateeve))
                {
                    termid = item.TermID;
                    //var data = tap.GetDataByGUID(item.GUID);
                    //var data = tap.CountByGUID(item.GUID);
                    var data = Database.Vyroba_Production.CountByGUID(con, sqltrans, item.GUID);

                    // zaznam jeste nebyl vlozen
                    if (data.HasValue && data.Value == 0)
                    {
                        //Fask.Logging.Log.writeErrorLog("Item RowState: " + item.RowState.ToString() + " (TID:" + termid + ")");
                        //tap.Update(item);
                        Database.Vyroba_Production.Update_Trans(con, sqltrans, item);
                        // Docasne pro test zda bylo ulozeno ...
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "GUID " + item.GUID + " inserted (TID:" + termid + ")");
                    }
                    else   // zaznam jiz je v DB, zalogovat ...
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "GUID " + item.GUID + " already in database(TID:" + termid + ")");

                    if (sqltrans != null)
                    {
                        if (sqltrans.Connection == null)
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "!!! Sql trans. connection: IS NULL (TID:" + termid + ")");
                        else
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Trans. connection: " + sqltrans.Connection.State.ToString() + " (TID:" + termid + ")");
                    }
                }

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Potvrzeni transakce (TID:" + termid + ")");
                if (sqltrans != null)
                    sqltrans.Commit();

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Konec(TID:" + termid + ")");
                //tap.Update(dtProduction.Select());
            }
            catch (Exception ex)
            {
                try
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error,
                        "Update " + Fask.SQL.Constants.Common.TABLE_PRODUCTION + " error: " + ex.Message +
                        "\nSource: " + ex.Source +
                        "\nType: " + ex.GetType().ToString() +
                        "\nStack: " + ex.StackTrace
                        );
                    if (ex.InnerException != null)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "InnerEx: " + ex.InnerException.Message);
                    }

                }
                catch //(Exception exLog)
                {
                }

                try
                {
                    if (sqltrans == null)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Transaction: IS NULL");
                    }
                    else
                    {
                        if (sqltrans.Connection == null)
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Trans. connection: IS NULL");
                        else
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Trans. connection: " + sqltrans.Connection.State.ToString());
                    }

                }
                catch { }

                try
                {
                    if (sqltrans != null)
                        sqltrans.Rollback();
                }
                catch (Exception sqltransex)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Rollback transakce neuspel: " + sqltransex.Message +
                        "Type: " + sqltransex.GetType() +
                        "Stack: " + sqltransex.StackTrace
                        );
                }

                throw ex;
            }
            //finally
            //{
            //    if ((tap.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
            //        tap.Connection.Close();
            //}
        }

        public void UpdateProduction_Sources(Fask.Interfaces.DataSets.Vyroba.Production_SourcesDataTable dtProduction_Sources)
        {
            Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter taps = new Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter();

            taps.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            System.Data.SqlClient.SqlTransaction sqltrans = null;
            try
            {
                // v rámci transakce projít všechny věci (všechny záznamy zkontrolovat na GUID a následně provést update pomocí transakce)
                taps.Connection.Open();
                sqltrans = taps.Connection.BeginTransaction();
                taps.Adapter.InsertCommand.Transaction = sqltrans;

                taps.Transaction = sqltrans;

                int termid = 0;

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Zacatek zpracovani zaznamu Production_Sources(TID:" + termid + ")");
                foreach (Fask.Interfaces.DataSets.Vyroba.Production_SourcesRow item in dtProduction_Sources)
                {
                    termid = item.TERMINAL_ID;
                    //var data = tap.GetDataByGUID(item.GUID);
                    var data = taps.CountByGUID(item.GUID);
                    // zaznam jeste nebyl vlozen
                    if (data.HasValue && data.Value == 0)
                    {
                        //Fask.Logging.Log.writeErrorLog("Item RowState: " + item.RowState.ToString() + " (TID:" + termid + ")");
                        taps.Update(item);
                        // Docasne pro test zda bylo ulozeno ...
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "GUID " + item.GUID + " inserted (TID:" + termid + ")");
                    }
                    else   // zaznam jiz je v DB, zalogovat ...
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "GUID " + item.GUID + " already in database(TID:" + termid + ")");

                    if (sqltrans != null)
                    {
                        if (sqltrans.Connection == null)
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "!!! Sql trans. connection: IS NULL (TID:" + termid + ")");
                        else
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Trans. connection: " + sqltrans.Connection.State.ToString() + " (TID:" + termid + ")");
                    }
                }

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Potvrzeni transakce (TID:" + termid + ")");
                if (sqltrans != null)
                    sqltrans.Commit();

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Konec(TID:" + termid + ")");
                //tap.Update(dtProduction.Select());
            }
            catch (Exception ex)
            {
                try
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error,
                        "Update Production_Sources error: " + ex.Message +
                        "\nSource: " + ex.Source +
                        "\nType: " + ex.GetType().ToString() +
                        "\nStack: " + ex.StackTrace
                        );
                    if (ex.InnerException != null)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "InnerEx: " + ex.InnerException.Message);
                    }

                }
                catch //(Exception exLog)
                {
                }

                try
                {
                    if (sqltrans == null)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Transaction: IS NULL");
                    }
                    else
                    {
                        if (sqltrans.Connection == null)
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Trans. connection: IS NULL");
                        else
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Trans. connection: " + sqltrans.Connection.State.ToString());
                    }

                }
                catch { }

                try
                {
                    if (sqltrans != null)
                        sqltrans.Rollback();
                }
                catch (Exception sqltransex)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Rollback transakce neuspel: " + sqltransex.Message +
                        "Type: " + sqltransex.GetType() +
                        "Stack: " + sqltransex.StackTrace
                        );
                }

                throw ex;
            }
            finally
            {
                if ((taps.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    taps.Connection.Close();
            }
        }

        public void UpdateTables(Fask.Interfaces.DataSets.Vyroba dsvyroba)
        {
            // k čemu to je??? 
            throw new NotImplementedException();
            
            //VyrobaDataSetTableAdapters.CorrectsTableAdapter tac = new Production.DataServices.MicrosoftSQL.Net.VyrobaDataSetTableAdapters.CorrectsTableAdapter();
            ////VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter tavph = new Production.DataServices.MicrosoftSQL.Net.VyrobaDataSetTableAdapters.CZPRO_VPHTableAdapter();
            ////VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter tavpp = new Production.DataServices.MicrosoftSQL.Net.VyrobaDataSetTableAdapters.CZPRO_VPPTableAdapter();
            //VyrobaDataSetTableAdapters.LoginsTableAdapter tal = new Production.DataServices.MicrosoftSQL.Net.VyrobaDataSetTableAdapters.LoginsTableAdapter();
            //VyrobaDataSetTableAdapters.MachinesTableAdapter tam = new Production.DataServices.MicrosoftSQL.Net.VyrobaDataSetTableAdapters.MachinesTableAdapter();
            //VyrobaDataSetTableAdapters.OperationsTableAdapter tao = new Production.DataServices.MicrosoftSQL.Net.VyrobaDataSetTableAdapters.OperationsTableAdapter();
            //VyrobaDataSetTableAdapters.VMachinesOperationsTableAdapter tavmo = new Production.DataServices.MicrosoftSQL.Net.VyrobaDataSetTableAdapters.VMachinesOperationsTableAdapter();
            //VyrobaDataSetTableAdapters.ProductionTableAdapter tap = new Production.DataServices.MicrosoftSQL.Net.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            //VyrobaDataSetTableAdapters.StatusTypesTableAdapter tas = new Production.DataServices.MicrosoftSQL.Net.VyrobaDataSetTableAdapters.StatusTypesTableAdapter();
            //VyrobaDataSetTableAdapters.UserEventsTableAdapter taue = new Production.DataServices.MicrosoftSQL.Net.VyrobaDataSetTableAdapters.UserEventsTableAdapter();

            //tac.Connection.ConnectionString = this.ConnectionString;
            ////tavph.Connection = tac.Connection;
            ////tavpp.Connection = tac.Connection;
            //tal.Connection = tac.Connection;
            //tam.Connection = tac.Connection;
            //tao.Connection = tac.Connection;
            //tavmo.Connection = tac.Connection;
            //tap.Connection = tac.Connection;
            //tas.Connection = tac.Connection;
            //taue.Connection = tac.Connection;

            //try
            //{
            //    tac.Connection.Open();

            //    tac.Update(dsvyroba.Corrects.Select());
            //    //tavph.Update(dsvyroba.CZPRO_VPH.Select());
            //    //tavpp.Update(dsvyroba.CZPRO_VPP.Select());
            //    tal.Update(dsvyroba.Logins.Select());
            //    tam.Update(dsvyroba.Machines.Select());
            //    tao.Update(dsvyroba.Operations.Select());
            //    tavmo.Update(dsvyroba.VMachinesOperations.Select());
            //    tap.Update(dsvyroba.Production.Select());
            //    tas.Update(dsvyroba.StatusTypes.Select());
            //    taue.Update(dsvyroba.UserEvents.Select());
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (tac.Connection.State == System.Data.ConnectionState.Open)
            //        tac.Connection.Close();
            //}
        }

        public void UpdateUserEvents(Fask.Interfaces.DataSets.Vyroba.UserEventsDataTable dtUserEvents)
        {
            Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.UserEventsTableAdapter taue = new Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.UserEventsTableAdapter();

            taue.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            try
            {
                // TODO : doplnit kontrolu na jedinecnost vlozeni dle GUID (ala UpdateProduction)...

                taue.Connection.Open();

                taue.Update(dtUserEvents.Select());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (taue.Connection.State == System.Data.ConnectionState.Open)
                    taue.Connection.Close();
            }
        }

        public DateTime? UserLastAction(string UserID)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = new Fask.Interfaces.DataSets.Vyroba();
                System.Data.SqlClient.SqlDataAdapter ta_production = new System.Data.SqlClient.SqlDataAdapter();
                ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand(
                    "Select top 1 * " +
                    " From " + Fask.SQL.Constants.Common.TABLE_PRODUCTION + "" +
                    " Where isnull(UserID,'')=@UserID" +
                    " Order by dateeve desc" +
                    ""
                    );

                ta_production.SelectCommand.Parameters.AddWithValue("@UserID", UserID);

                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                ta_production.Fill(ds_vyroba.Production);

                if (ds_vyroba.Production.Count > 0)
                {
                    return ds_vyroba.Production[0].dateeve;
                }
                else
                { // produkce pro uzivatele nenalezena
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fask.Interfaces.DataSets.Vyroba Vyroba_Online_CheckOperation(string operaceID, string terminalID, out Server.Interfaces.Classes_Vyroba.ProductionState productionStateEnabled)
        {
            productionStateEnabled =Server.Interfaces.Classes_Vyroba.ProductionState.Unknown;

            bool? enStartPre = false;
            bool? enStartOp = false;
            bool? enEndOp = false;
            string popisOp = string.Empty;
            string machineId = string.Empty;
            int? mnOp = 0;
            string message = string.Empty;
            object returnvalue = null;

            Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.mtj_fask_production_CheckOpTableAdapter vta = new Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.mtj_fask_production_CheckOpTableAdapter();
            vta.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            var operationData = vta.GetData(
                operaceID
                , terminalID
                , ref enStartPre
                , ref enStartOp
                , ref enEndOp
                , ref popisOp
                , ref machineId
                , ref mnOp
                , ref message
                );
            returnvalue = vta.mtj_fask_production_CheckOp_GetReturnValue();

            // RETURN 
            //  == 0 => OK 
            //  <> 0 => Chyba
            if ((returnvalue is int) && ((int)returnvalue != 0))
            {
                Fask.Logging.ExceptionHandler2.Handle( Logging.LogLevel.Error,String.Format("mtj_fask_production_CheckOp returnvalue:{0}\n{1}", (int)returnvalue, message));
                throw new Exception(message);
            }

            // 29.1.2019 JiS => zmena chovani ...
            //int timemode = -1;
            //if (enEndOp ?? false)   // STOP 
            //    timemode= 0;
            //if (enStartOp ?? false) // START / STOP
            //    timemode = 1;
            //if (enStartPre ?? false)// START / START / STOP
            //    timemode = 2;

            //int timemode = 1; // prozatim napevno start/stop
            int timemode = 0;
            timemode = operationData[0].TIMEMODE;

            if (enEndOp ?? false)   // STOP 
                productionStateEnabled =Server.Interfaces.Classes_Vyroba.ProductionState.Odvod_Stop;
            if (enStartOp ?? false) // START / STOP
                productionStateEnabled =Server.Interfaces.Classes_Vyroba.ProductionState.Odvod_Start;
            if (enStartPre ?? false)// START / START / STOP
                productionStateEnabled =Server.Interfaces.Classes_Vyroba.ProductionState.Priprava_Start;
            //... ???

            Fask.Interfaces.DataSets.Vyroba vyrobaDS = new Fask.Interfaces.DataSets.Vyroba();
            vyrobaDS.CZPRO_VPH.AddCZPRO_VPHRow(
                1               //Countentries
                , operationData[0].sopnumbe //"SOPNUMBE"     //SOPNUMBE
                , string.Empty   //SOPTYPE
                , "SOPDESC"      //SOPDESC
                , "VNDDOCNMH"    //VNDDOCNMH
                , "BARCODEH"     //BARCODEH
                , "LOCNCODE"     //LOCNCODE
                , 15             //DateProd
                , string.Empty   //Rez1
                , string.Empty   //Rez2
                , 0              //TermID
                , DateTime.Now   //LSTMod 
                , 0
                , 0
                );

            var row = vyrobaDS.CZPRO_VPP.NewCZPRO_VPPRow();
            row.SetRealization_StartNull();
            //parametry radku VPP:
            row.CountEntries = 0;
            row.SOPNUMBE = operationData[0].sopnumbe;
            row.ITEMNMBR = operaceID;
            row.ITEMTYPE = string.Empty;
            row.ITEMDESC = popisOp;
            row.ITEMMJ = "IMJ";
            row.VNDDOCNMP = "VNDDOCNMP";
            row.VNDITNUM = "VNDITNUM";
            row.ORD = 1;
            row.BarcodeP = operaceID;
            row.LOCNCODE = "LOCNCODE";
            row.QTYSHPPD = mnOp ?? 0;
            row.QTYDOKON = 0;
            row.QTYPACK = 0;
            row.QTYPACKMJ = "QMJ";
            row.TIMEMODE = 0;
            row.TIMEPREP = 0;
            row.TIMEUNIT = 0;
            row.DtProdT = 0;
            row.DtProdL = 0;
            row.SerNumT = 0;
            row.SerNumL = 0;
            row.VerT = 0;
            row.VerL = 0;
            row.TermID = 0;
            row.LSTMod = DateTime.Now;
            row.SetRealization_StartNull();
            row.SetRealization_StopNull();
            row.BarcodeT = 0;

            vyrobaDS.CZPRO_VPP.AddCZPRO_VPPRow(row);

            //vyrobaDS.CZPRO_VPP.AddCZPRO_VPPRow(
            //    0                   //int CountEntries, 
            //    , operationData[0].sopnumbe //string SOPNUMBE, 
            //    , operaceID         //string ITEMNMBR, =>operaceID?
            //    , string.Empty      //string ITEMTYPE, 
            //    , popisOp           //string ITEMDESC, => popis operace
            //    , "IMJ"             //string ITEMMJ, 
            //    , "VNDDOCNMP"       //string VNDDOCNMP, 
            //    , "VNDITNUM"        //string VNDITNUM, 
            //    , 1                 //int ORD, 
            //    , operaceID         //string BarcodeP, 
            //    , "LOCNCODE"        //string LOCNCODE, 
            //    , mnOp ?? 0         //decimal QTYSHPPD, 
            //    , 0                 //decimal QTYPACK, 
            //    , "QMJ"             //string QTYPACKMJ, 
            //    , 0                 //float TIMEPREP, 
            //    , 0                 //float TIMEUNIT, 
            //    , 0                 //byte DtProdT, 
            //    , 0                 //short DtProdL, 
            //    , 0                 //byte SerNumT, 
            //    , 0                 //short SerNumL, 
            //    , 0                 //byte VerT, 
            //    , 0                 //short VerL, 
            //    , 0                 //byte TermID, 
            //    , DateTime.Now      //System.DateTime LSTMod, 
            //    , -1                //int DEX_ROW_ID, 
            //    , 0                 //decimal QTYODVEDENO, => jak toto naplnit???
            //    , 0                 //int CNTODVEDENO   => jak toto naplnit???
            //    , timemode          //int TIMEMODE    => mod vyroby 
            //    , 0                 //BarcodeT
            //    , 0                  //QTYDOKON
            //    ,string.Empty       //ITEMCODE
            //    );

            vyrobaDS.AcceptChanges();
            return vyrobaDS;
        }

        public bool Vyroba_Online_WriteOperation(Server.Interfaces.Classes_Vyroba.ProductionObject productionObject, out string message)
        {
            message = string.Empty;

            Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.QueriesTableAdapter qta = new Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.QueriesTableAdapter(); 
            qta.ConncetionStringChange(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            if (productionObject.stavOperace == Server.Interfaces.Classes_Vyroba.ProductionState.Priprava_Start)
            {
                qta.mtj_fask_production_StartPre(productionObject.operaceID, productionObject.terminalID, ref message);
                return true;
            }
            if (productionObject.stavOperace == Server.Interfaces.Classes_Vyroba.ProductionState.Priprava_Stop)
            {
                return true;
            }
            else if (productionObject.stavOperace == Server.Interfaces.Classes_Vyroba.ProductionState.Odvod_Start)
            {
                qta.mtj_fask_production_StartOp(productionObject.operaceID, productionObject.terminalID, ref message);
                return true;
            }
            else if (productionObject.stavOperace == Server.Interfaces.Classes_Vyroba.ProductionState.Odvod_Stop)
            {
                qta.mtj_fask_production_EndOp(
                    productionObject.operaceID
                    , productionObject.terminalID
                    , Convert.ToInt32(productionObject.mnozstviVyrobeno)
                    , Convert.ToInt32(productionObject.mnozstviZmetek)
                    , productionObject.popisZmetek
                    , ref message
                    );
                return true;
            }
            else
            {
                message = String.Format("Neznamy stav odvodu vyroby : {0}", productionObject.stavOperace);
                return false;
            }
        }

        public bool VyrobniPrikazBlokace(byte terminalID, Server.Interfaces.Classes_Vyroba.VyrobniPrikazHlavicka vyrobniprikaz, Server.Interfaces.Classes_Vyroba.BlokaceTyp pozadavekBlokace)
        {
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

                command.Connection.Open();
                System.Data.SqlClient.SqlTransaction transaction = command.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                command.Transaction = transaction;

                byte termIDstate = terminalID;
                //Pokud se zde vrati pocet=0, tak je blokovano jinym terminalem a nelze blokovat...
                if (pozadavekBlokace == Server.Interfaces.Classes_Vyroba.BlokaceTyp.Blokovat)
                {
                    termIDstate = terminalID;

                    command.CommandText = "Select Count(*) from CZPRO_VPH Where CountEntries=@CountEntries and SOPNUMBE=@SOPNUMBE and (TermID=0 OR TermID=" + terminalID + ")";
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountEntries", vyrobniprikaz.COUNTENTRIES));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SOPNUMBR", vyrobniprikaz.SOPNUMBE));
                    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TermID", terminalID));
                    object result = command.ExecuteScalar();
                    if ((result != null) && (result is int) && ((int)result == 0))
                    {
                        throw new Exception("Již je blokováno jiným terminálem");
                    }
                }
                else if (pozadavekBlokace == Server.Interfaces.Classes_Vyroba.BlokaceTyp.OdBlokovat)
                {
                    termIDstate = 0;
                }
                else if (pozadavekBlokace == Server.Interfaces.Classes_Vyroba.BlokaceTyp.Uzavrit)
                {
                    termIDstate = (byte)(terminalID + 100);
                }

                //command.CommandText = "Update CZPRO_VPH SET TermID=@TermID Where CountEntries=@CountEntries And SOPNUMBE=@SOPNUMBE and TermID=@TermIDaktual";
                command.CommandText = "Update CZPRO_VPH SET TermID=@TermID Where CountEntries=@CountEntries And SOPNUMBE=@SOPNUMBE";
                command.Parameters.Clear();
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TermID", termIDstate));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CountEntries", vyrobniprikaz.COUNTENTRIES));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SOPNUMBR", vyrobniprikaz.SOPNUMBE));
                //command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TermIDaktual", terminalID));
                int raff = command.ExecuteNonQuery();

                if (command != null && command.Transaction != null)
                {
                    command.Transaction.Commit();
                }

                return true;
            }
            catch //(Exception ex)
            {
                if (command != null && command.Transaction != null)
                {
                    command.Transaction.Rollback();
                }
                return false;
            }
            finally
            {
                if (command != null && command.Connection.State == System.Data.ConnectionState.Open)
                {
                    command.Connection.Close();
                }
            }
        }

        public string ReturnSarze(string smenaID, string userID, string linkaID, decimal qty, string ITEMNMBR)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {

                Globals_V1.LoadConfiguration();
                sqlConn = new System.Data.SqlClient.SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                sqlComm.CommandText = "FASK_procGetSarzeVyroba";
                sqlComm.Parameters.AddWithValue("@smenaID", smenaID);
                sqlComm.Parameters.AddWithValue("@userID", userID);
                sqlComm.Parameters.AddWithValue("@linkaID", linkaID);
                sqlComm.Parameters.AddWithValue("@QTY", qty);
                sqlComm.Parameters.AddWithValue("@ITEMNMBR", ITEMNMBR);

                sqlComm.Connection.Open();

                object o = sqlComm.ExecuteScalar();
                if (o is string)
                {
                    return (string)o;
                }
                else
                {
                    return string.Empty;
                }
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }
            }
        }

        public void UpdateProduction_SN(Fask.Interfaces.DataSets.Vyroba.Production_SNDataTable dtProduction_SN)
        {
            Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.Production_SNTableAdapter taps = new Fask.ModulePohodaXML.Datasets.VyrobaDataSetTableAdapters.Production_SNTableAdapter();

            taps.Connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
            System.Data.SqlClient.SqlTransaction sqltrans = null;
            try
            {
                // v rámci transakce projít všechny věci (všechny záznamy zkontrolovat na GUID a následně provést update pomocí transakce)
                taps.Connection.Open();
                sqltrans = taps.Connection.BeginTransaction();
                taps.Adapter.InsertCommand.Transaction = sqltrans;

                taps.Transaction = sqltrans;

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Zacatek zpracovani zaznamu Production_SN");
                foreach (Fask.Interfaces.DataSets.Vyroba.Production_SNRow item in dtProduction_SN)
                {
                    var data = taps.CountByGUID(item.GUID);
                    if (data.HasValue && data.Value == 0)
                    {
                        //Fask.Logging.Log.writeErrorLog("Item RowState: " + item.RowState.ToString() + " (TID:" + termid + ")");
                        taps.Update(item);
                        // Docasne pro test zda bylo ulozeno ...
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "GUID " + item.GUID + " inserted ");
                    }
                    else   // zaznam jiz je v DB, zalogovat ...
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "GUID " + item.GUID + " already in database");

                    if (sqltrans != null)
                    {
                        if (sqltrans.Connection == null)
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "!!! Sql trans. connection: IS NULL ");
                        else
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Trans. connection: " + sqltrans.Connection.State.ToString() );
                    }
                }

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Potvrzeni transakce");
                if (sqltrans != null)
                    sqltrans.Commit();

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Konec");
                //tap.Update(dtProduction.Select());
            }
            catch (Exception ex)
            {
                try
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error,
                        "Update Production_Sources error: " + ex.Message +
                        "\nSource: " + ex.Source +
                        "\nType: " + ex.GetType().ToString() +
                        "\nStack: " + ex.StackTrace
                        );
                    if (ex.InnerException != null)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "InnerEx: " + ex.InnerException.Message);
                    }

                }
                catch //(Exception exLog)
                {
                }

                try
                {
                    if (sqltrans == null)
                    {
                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Transaction: IS NULL");
                    }
                    else
                    {
                        if (sqltrans.Connection == null)
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Trans. connection: IS NULL");
                        else
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Trans. connection: " + sqltrans.Connection.State.ToString());
                    }

                }
                catch { }

                try
                {
                    if (sqltrans != null)
                        sqltrans.Rollback();
                }
                catch (Exception sqltransex)
                {
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Rollback transakce neuspel: " + sqltransex.Message +
                        "Type: " + sqltransex.GetType() +
                        "Stack: " + sqltransex.StackTrace
                        );
                }

                throw ex;
            }
            finally
            {
                if ((taps.Connection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
                    taps.Connection.Close();
            }
        }

        public FASK_Events_row GetFASKEventsRow(Filtr_FASK_Events filtr)
        {
            throw new NotImplementedException();
        }

        public void SetFaskEventsRow(FASK_Events_row FE_object)
        {
            throw new NotImplementedException();
        }

        public void UpdateProductionRow(Guid? g, decimal? vaha)
        {
            throw new NotImplementedException();
        }

        public int Update_StatusByGuid(Filtr_UpdateStatus FE_object)
        {
            throw new NotImplementedException();
        }

        public FASK_Events_row GetFASK_Events_BySSCC(Filtr_FASK_Events filtr)
        {
            throw new NotImplementedException();
        }

        public FASK_Events_row Vrat_zaznam_By_SSCC(Filtr_FASK_Events filtr)
        {
            throw new NotImplementedException();
        }

        public FASK_Events_row Get_FE_Row(Filtr_FASK_Events filtr)
        {
            throw new NotImplementedException();
        }

        public FASK_Events Get_FE_List(Filtr_FASK_Events filtr)
        {
            throw new NotImplementedException();
        }

        public Vyroba.FASK_Events_archivaceDataTable FE_archivace_Rows_data(Filtr_FASK_Events filtr)
        {
            throw new NotImplementedException();
        }

        public int FE_archivace_Rows_deaktivace_nakladka(Vyroba.FASK_Events_archivaceDataTable data_rows)
        {
            throw new NotImplementedException();
        }

        public int FE_archivace_Rows_deaktivace_vykladka(Vyroba.FASK_Events_archivaceDataTable data_rows)
        {
            throw new NotImplementedException();
        }

        public Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable Konfigurace_ADAM_data(int ID_group)
        {
            //-----------------------------------------------START-------------------------------------------------------

            try
            {
                //Globals.LoadConfiguration();

                Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable();


                // Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable();


                DT_S0 = Get_ADAM_konfig_Rows((Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB), ID_group);

                var x = DT_S0.Count;


                return DT_S0;
            }
            catch (Exception ex)
            {

                var message_sent = String.Format("CATCH--Konfigurace_ADAM_data!!");
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, ex.Message);
                return null;
            }
            //----------------------------------------------END-----------------------------------------------------------

        }


        private Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable Get_ADAM_konfig_Rows(string connectionString, int ID_group)
        {
            Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable DT = new Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable();
            //SqlCommand comm = null; //CommandBehavior do databaze
            SqlConnection conn = null; //connection do databaze
            //int result = 0;
            try
            {
                using (conn = new SqlConnection(connectionString))
                {


                    using (var com = conn.CreateCommand())
                    {
                        com.CommandType = CommandType.Text;

                        //---------------SQL dotaz-----------------------------

                        //SELECT [IP]
                        //        ,[Description]
                        //        ,[MType]
                        //        ,[PORT]
                        //FROM [MachinesDefinition]

                        //---------------SQL dotaz-----------------------------


                        //com.CommandText = "SELECT" +
                        //    " IP" +
                        //    " ,Description" +
                        //    " ,MType" +
                        //    " ,PORT" +
                        //    " FROM [MachinesDefinition]" ;

                        com.CommandText = "SELECT" +
                        " * " +
                        " FROM [MachinesDefinition]";

                        com.CommandText += " WHERE 1=1 " +
                      " and ID_group = " + ID_group + " ;";


                        conn.Open();

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.SelectCommand = com;
                            int returnValue = adapter.Fill(DT);

                        }
                    }
                }

                var w = DT.Count;

                return DT;


            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                //ErrorLog.Log.Write(ex);
                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }





        public Vyroba.FASK_EventsDataTable GetFASKEventsRows(Filtr_FASK_Events filtr)
        {
            throw new NotImplementedException();
        }

        public int GetStatus(string ID)
        {
            throw new NotImplementedException();
        }

        public Vyroba.MachinesDefinitionMeasurementDataTable Konfigurace_Mericich_Zarizeni_00(string IP)
        {
            //-----------------------------------------------START-------------------------------------------------------

            try
            {
                //Globals.LoadConfiguration();

                //Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable();


                // Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable();


                //DT_S0 = Get_ADAM_konfig_Rows((Globals.Konfigurace.ConnectionString[0].FASKDB), 1);

                //var x = DT_S0.Count;

                Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable DT = new Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionMeasurementDataTable();
                //SqlCommand comm = null; //CommandBehavior do databaze
                SqlConnection conn = null; //connection do databaze
                                           //int result = 0;
                try
                {
                    using (conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                    {


                        using (var com = conn.CreateCommand())
                        {
                            com.CommandType = CommandType.Text;

                            //---------------SQL dotaz-----------------------------

                            //SELECT [IP]
                            //        ,[Description]
                            //        ,[MType]
                            //        ,[PORT]
                            //FROM [MachinesDefinition]

                            //---------------SQL dotaz-----------------------------


                            //com.CommandText = "SELECT" +
                            //    " IP" +
                            //    " ,Description" +
                            //    " ,MType" +
                            //    " ,PORT" +
                            //    " FROM [MachinesDefinition]" ;

                            com.CommandText = "SELECT" +
                            " * " +
                            " FROM [MachinesDefinitionMeasurement]";

                            com.CommandText += " WHERE 1=1 " +
                          " and IP =  '" + IP + "' ;";


                            conn.Open();

                            using (var adapter = new SqlDataAdapter())
                            {
                                adapter.SelectCommand = com;
                                int returnValue = adapter.Fill(DT);

                            }
                        }
                    }

                    var w = DT.Count;

                    return DT;
                }
                catch (Exception ex)
                {

                    var message_sent = String.Format("CATCH--Konfigurace_ADAM_data!!");
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, ex.Message);
                    return null;
                }
                //----------------------------------------------END-----------------------------------------------------------
            }
            catch (Exception ex)
            {

                var message_sent = String.Format("CATCH--Konfigurace_ADAM_data!!");
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, ex.Message);
                return null;
            }


        }

        #region POKUS_1
        public Fask.WEBAPI.API_BusinessObjects.MachineStateSet PostMachineStateSetEventsRow(Fask.WEBAPI.API_BusinessObjects.MachineStateSet MSS_objekt)
        {
            //--------------------------Zapis do DB tabulka MachineStateSet-------START-------------------------------------------------//

            Fask.Interfaces.DataSets.Vyroba.MachineStateSetDataTable DT = new Fask.Interfaces.DataSets.Vyroba.MachineStateSetDataTable();
            //Vyroba.FASK_EventsDataTable DT = new Vyroba.FASK_EventsDataTable();
            SqlCommand com = null; //CommandBehavior do databaze
            SqlConnection conn = null; //connection do databaze
            SqlTransaction transaction = null;
            Fask.WEBAPI.API_BusinessObjects.MachineStateSet ms = new Fask.WEBAPI.API_BusinessObjects.MachineStateSet();
            try
            {
                //Globals.LoadConfiguration();

                using (conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (com = conn.CreateCommand())
                    {
                        com.Transaction = transaction;
                        com.CommandType = CommandType.Text;
                        // com.CommandText = "SELECT TOP (1) * FROM MachineStateSet WHERE (IP = '" + filtr.IP + "') order by DateModified desc";

                        com.CommandText = "INSERT INTO [dbo].[MachineStateSet]" +
           "([IP]" +
           ",[DateModified]" +
           ",[S0]" +
           ",[S1]" +
           ",[S2]" +
           ",[S3]" +
           ",[S4]" +
           ",[S5]" +
           ",[S6]" +
           ",[S7]" +
           ",[LastError]" +
           ",[S8]" +
           ",[S9]" +
           ",[S10]" +
           ",[S11]" +
           ",[counter_0]" +
           ",[counter_1]" +
           ",[counter_2]" +
           ",[counter_3]" +
           ",[counter_4]" +
           ",[counter_5]" +
           ",[counter_6]" +
           ",[counter_7]" +
           ",[counter_8]" +
           ",[counter_9]" +
           ",[counter_10]" +
           ",[counter_11])" +
     "VALUES" +
           "(" + MSS_objekt.IP +
           "," + DateTime.Now +
           "," + MSS_objekt.S0 +
           "," + MSS_objekt.S1 +
           "," + MSS_objekt.S2 +
           "," + MSS_objekt.S3 +
           "," + MSS_objekt.S4 +
           "," + MSS_objekt.S5 +
           "," + MSS_objekt.S6 +
           "," + MSS_objekt.S7 +
           "," + MSS_objekt.LastError +
           "," + MSS_objekt.S8 +
           "," + MSS_objekt.S9 +
           "," + MSS_objekt.S10 +
           "," + MSS_objekt.S11 +
           "," + MSS_objekt.counter_0 +
           "," + MSS_objekt.counter_1 +
           "," + MSS_objekt.counter_2 +
           "," + MSS_objekt.counter_3 +
           "," + MSS_objekt.counter_4 +
           "," + MSS_objekt.counter_5 +
           "," + MSS_objekt.counter_6 +
           "," + MSS_objekt.counter_7 +
           "," + MSS_objekt.counter_8 +
           "," + MSS_objekt.counter_9 +
           "," + MSS_objekt.counter_10 +
           "," + MSS_objekt.counter_11 +
           ")";

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.SelectCommand = com;

                        }
                    }
                    transaction.Commit();
                }
                return MSS_objekt;
            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                //ErrorLog.Log.Write(ex);
                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        #endregion

        //public void SetMachineStateSet(MachineStateSet dsmachine)
        //{
        //    throw new NotImplementedException();
        //}


        #region zapis do DB ADAM
        public void SetMachineStateSet(Fask.WEBAPI.API_BusinessObjects.MachineStateSet dsmachine)
        {
            Fask.Interfaces.DataSets.Vyroba.MachineStateSetDataTable DT = new Fask.Interfaces.DataSets.Vyroba.MachineStateSetDataTable();
            //Vyroba.FASK_EventsDataTable DT = new Vyroba.FASK_EventsDataTable();
            SqlCommand com = null; //CommandBehavior do databaze
            SqlConnection conn = null; //connection do databaze
            Fask.WEBAPI.API_BusinessObjects.MachineStateSet ms = new Fask.WEBAPI.API_BusinessObjects.MachineStateSet();

            try
            {
               // Globals.LoadConfiguration();

                #region Zkouska zda uz je zaznam se shodnou IP

                using (conn = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB))
                {
                    using (com = conn.CreateCommand())
                    {
                        com.CommandType = CommandType.Text;
                        com.CommandText = "SELECT TOP (1) * FROM MachineStateSet WHERE (IP = '" + dsmachine.IP + "') order by DateModified desc";
                        conn.Open();

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.SelectCommand = com;
                            int returnValue = adapter.Fill(DT);

                            #region UPDATE else INSERT
                            if (DT.Count == 1)
                            {
                                if (dsmachine.DateModified.HasValue)
                                    DT[0].DateModified = dsmachine.DateModified.Value;
                                if (dsmachine.S0.HasValue)
                                    DT[0].S0 = dsmachine.S0.Value;
                                if (dsmachine.S1.HasValue)
                                    DT[0].S1 = dsmachine.S1.Value;
                                if (dsmachine.S2.HasValue)
                                    DT[0].S2 = dsmachine.S2.Value;
                                if (dsmachine.S3.HasValue)
                                    DT[0].S3 = dsmachine.S3.Value;
                                if (dsmachine.S4.HasValue)
                                    DT[0].S4 = dsmachine.S4.Value;
                                if (dsmachine.S5.HasValue)
                                    DT[0].S5 = dsmachine.S5.Value;
                                if (dsmachine.S6.HasValue)
                                    DT[0].S6 = dsmachine.S6.Value;
                                if (dsmachine.S7.HasValue)
                                    DT[0].S7 = dsmachine.S7.Value;
                                if (dsmachine.S8.HasValue)
                                    DT[0].S8 = dsmachine.S8.Value;
                                if (dsmachine.S9.HasValue)
                                    DT[0].S9 = dsmachine.S9.Value;
                                if (dsmachine.S10.HasValue)
                                    DT[0].S10 = dsmachine.S10.Value;
                                if (dsmachine.S11.HasValue)
                                    DT[0].S11 = dsmachine.S11.Value;
                                if (dsmachine.counter_0.HasValue)
                                    DT[0].counter_0 = dsmachine.counter_0.Value;
                                if (dsmachine.counter_1.HasValue)
                                    DT[0].counter_1 = dsmachine.counter_1.Value;
                                if (dsmachine.counter_2.HasValue)
                                    DT[0].counter_2 = dsmachine.counter_2.Value;
                                if (dsmachine.counter_3.HasValue)
                                    DT[0].counter_3 = dsmachine.counter_3.Value;
                                if (dsmachine.counter_4.HasValue)
                                    DT[0].counter_4 = dsmachine.counter_4.Value;
                                if (dsmachine.counter_5.HasValue)
                                    DT[0].counter_5 = dsmachine.counter_5.Value;
                                if (dsmachine.counter_6.HasValue)
                                    DT[0].counter_6 = dsmachine.counter_6.Value;
                                if (dsmachine.counter_7.HasValue)
                                    DT[0].counter_7 = dsmachine.counter_7.Value;
                                if (dsmachine.counter_8.HasValue)
                                    DT[0].counter_8 = dsmachine.counter_8.Value;
                                if (dsmachine.counter_9.HasValue)
                                    DT[0].counter_9 = dsmachine.counter_9.Value;
                                if (dsmachine.counter_10.HasValue)
                                    DT[0].counter_10 = dsmachine.counter_10.Value;
                                if (dsmachine.counter_11.HasValue)
                                    DT[0].counter_11 = dsmachine.counter_11.Value;
                                if (dsmachine.LastError != null)
                                    DT[0].LastError = dsmachine.LastError;

                                DT[0].ID_group = dsmachine.ID_group;

                                //  DT[0].SetModified();
                            }
                            else if (DT.Count == 0)
                            {
                                var row = DT.NewMachineStateSetRow();

                                row.IP = dsmachine.IP;
                                if (dsmachine.DateModified.HasValue)
                                    row.DateModified = dsmachine.DateModified.Value;
                                if (dsmachine.S0.HasValue)
                                    row.S0 = dsmachine.S0.Value;
                                if (dsmachine.S1.HasValue)
                                    row.S1 = dsmachine.S1.Value;
                                if (dsmachine.S2.HasValue)
                                    row.S2 = dsmachine.S2.Value;
                                if (dsmachine.S3.HasValue)
                                    row.S3 = dsmachine.S3.Value;
                                if (dsmachine.S4.HasValue)
                                    row.S4 = dsmachine.S4.Value;
                                if (dsmachine.S5.HasValue)
                                    row.S5 = dsmachine.S5.Value;
                                if (dsmachine.S6.HasValue)
                                    row.S6 = dsmachine.S6.Value;
                                if (dsmachine.S7.HasValue)
                                    row.S7 = dsmachine.S7.Value;
                                if (dsmachine.S8.HasValue)
                                    row.S8 = dsmachine.S8.Value;
                                if (dsmachine.S9.HasValue)
                                    row.S9 = dsmachine.S9.Value;
                                if (dsmachine.S10.HasValue)
                                    row.S10 = dsmachine.S10.Value;
                                if (dsmachine.S11.HasValue)
                                    row.S11 = dsmachine.S11.Value;
                                if (dsmachine.counter_0.HasValue)
                                    row.counter_0 = dsmachine.counter_0.Value;
                                if (dsmachine.counter_1.HasValue)
                                    row.counter_1 = dsmachine.counter_1.Value;
                                if (dsmachine.counter_2.HasValue)
                                    row.counter_2 = dsmachine.counter_2.Value;
                                if (dsmachine.counter_3.HasValue)
                                    row.counter_3 = dsmachine.counter_3.Value;
                                if (dsmachine.counter_4.HasValue)
                                    row.counter_4 = dsmachine.counter_4.Value;
                                if (dsmachine.counter_5.HasValue)
                                    row.counter_5 = dsmachine.counter_5.Value;
                                if (dsmachine.counter_6.HasValue)
                                    row.counter_6 = dsmachine.counter_6.Value;
                                if (dsmachine.counter_7.HasValue)
                                    row.counter_7 = dsmachine.counter_7.Value;
                                if (dsmachine.counter_8.HasValue)
                                    row.counter_8 = dsmachine.counter_8.Value;
                                if (dsmachine.counter_9.HasValue)
                                    row.counter_9 = dsmachine.counter_9.Value;
                                if (dsmachine.counter_10.HasValue)
                                    row.counter_10 = dsmachine.counter_10.Value;
                                if (dsmachine.counter_11.HasValue)
                                    row.counter_11 = dsmachine.counter_11.Value;
                                if (dsmachine.LastError != null)
                                    row.LastError = dsmachine.LastError;

                                row.ID_group = dsmachine.ID_group;

                                DT.AddMachineStateSetRow(row);
                            }
                            else
                            {
                                // Nemnelo by nastat
                                throw new Exception("fujto, moc IP");
                            }
                            #endregion
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                //todo zalogovat exception
                //ErrorLog.Log.Write(ex);
                throw ex;
            }
            finally
            {
                if ((conn.State & ConnectionState.Open) == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }


            #region zapis do DB

            Fask.ModuleSql.Database.Vyroba.Update(DT, Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);

            #endregion


        }
        #endregion

    }
}
