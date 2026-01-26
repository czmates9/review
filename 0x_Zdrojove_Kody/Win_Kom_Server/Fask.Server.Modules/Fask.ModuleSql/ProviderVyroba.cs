using Fask.DataSets;
using Fask.Server.Interfaces.Classes_Vyroba;
using Fask.Server.Interfaces.DataSets;
using Fask.Server.Interfaces.Extension;
using Fask.SQL.Constants;
using Fask.Server.Interfaces.Vyroba;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fask.ModuleSql.Classes;
using Fask.Constants;
using Fask.Interfaces.DataSets;
using Fask.Interfaces.Vyroba.Odvod_MachineStateSet;
using Fask.Interfaces.Filtry;
using Fask.WEBAPI.API_BusinessObjects;
using Fask.Interfaces.Vyroba.Odvod_TiskoveSablony;
using Fask.Interfaces.Classes;

namespace Fask.ModuleSql
{
    public partial class Provider : 
        Fask.Server.Interfaces.Vyroba.IProduction,

        Fask.Interfaces.Vyroba.API.IVyroba_PostFaskEventsRow,
        Fask.Interfaces.Vyroba.API.IVyroba_GetMachineStateSetEventsRow,
        Fask.Interfaces.Vyroba.API.IVyroba_PostMSSEventRow,
        Fask.Interfaces.Vyroba.API.IVyroba_Vaha,
        Fask.Interfaces.Vyroba.API.IVyroba_Logs_Insert,
        Fask.Interfaces.Vyroba.API.IVyroba,

        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_DeleteZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetFiltrovaneZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_GetZboziByID,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_ImportZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_InsertZboziParams,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZbozi,
        Fask.Interfaces.Ciselniky.Zbozi.IZbozi2_UpdateZboziParams,
        
        Fask.Interfaces.Ciselniky.Sklady.ISklady2,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_GetSklady,
        Fask.Interfaces.Ciselniky.Sklady.ISklady2_Vyroba_Fill,

        Fask.Interfaces.Ciselniky.Lokace.ILokace2,
        Fask.Interfaces.Ciselniky.Lokace.ILokace2_Fill,
        
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_CallProcedura,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetFiltrovanyOdvodEvents,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_GetMaterials,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent,
        Fask.Interfaces.Vyroba.Odvod_Events.IOdvod_Events_StornoEvent_OnlineCheck,
        
        Fask.Interfaces.Vyroba.Odvod_EventsErr.IOdvod_EventsErr_GetFiltrovanyOdvodEvents,
        Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets,
        Fask.Interfaces.Vyroba.Odvod_MachineStateSet.IOdvod_MachineStateSet_GetEnum_description,

        Fask.Interfaces.Vyroba.Odvod_TiskoveSablony.IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony,

        Fask.Interfaces.Vyroba.VPH.IVPH,
        Fask.Interfaces.Vyroba.VPH.IVPH_Fill,
        Fask.Interfaces.Vyroba.VPH.IVPH_GetDataByCountEntriesSOPNUMBE,
        Fask.Interfaces.Vyroba.VPH.IVPH_Insert,
        Fask.Interfaces.Vyroba.VPH.IVPH_Update,
        Fask.Interfaces.Vyroba.VPH.IVPH_Update_Row,
        Fask.Interfaces.Vyroba.VPH.IVPH_GetFiltrovanyVPHList,

        Fask.Interfaces.Vyroba.VPP.IVPP,
        Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSOPNUMBEITEMNMBR,
        Fask.Interfaces.Vyroba.VPP.IVPP_GetDataByCountEntriesSopnumbeItemnmbrBarcodeP,
        Fask.Interfaces.Vyroba.VPP.IVPP_FillByCountEntriesAndSOPNUMBE,
        Fask.Interfaces.Vyroba.VPP.IVPP_DeleteByCountEntriesSOPNUMBE,
        Fask.Interfaces.Vyroba.VPP.IVPP_Insert,
        Fask.Interfaces.Vyroba.VPP.IVPP_Update,
        Fask.Interfaces.Vyroba.VPP.IVPP_Fill,
        Fask.Interfaces.Vyroba.VPP.IVPP_Update_Row,
        Fask.Interfaces.Vyroba.VPP.IVPP_GetFiltrovanyVPPList,

        Fask.Interfaces.Vyroba.Groups.IGroups,
        Fask.Interfaces.Vyroba.Groups.IGroups_FillGroups,
        Fask.Interfaces.Vyroba.Groups.IGroups_GetDataByID,
        Fask.Interfaces.Vyroba.Groups.IGroups_Insert,
        Fask.Interfaces.Vyroba.Groups.IGroups_Update,
        Fask.Interfaces.Vyroba.Groups.IGroups_Update_Row,
        
        Fask.Interfaces.Vyroba.Machines.IMachines_GetDataByID,
        Fask.Interfaces.Vyroba.Machines.IMachines_Fill,
        
        Fask.Interfaces.Vyroba.Operations.IOperations_GetDataByID,
        Fask.Interfaces.Vyroba.Operations.IOperations_Fill,
        
        Fask.Interfaces.Vyroba.Production.IProduction,
        Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionList,
        Fask.Interfaces.Vyroba.Production.IProduction_FillByCORRGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_FillBySOUBEHGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_GetDataByCORRGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_GetDataBySOUBEHGUID,
        Fask.Interfaces.Vyroba.Production.IProduction_Update,
        Fask.Interfaces.Vyroba.Production.IProduction_GetFiltrovanyProductionVazby,
       
        Fask.Interfaces.Vyroba.Corrects.ICorrects_GetDataByID,
       
        Fask.Interfaces.Vyroba.VMachinesOperations.IVMachinesOperations_GetDataByMachineIDoperationID,

        Fask.Interfaces.Vyroba.API.IVyroba_Sarze,

        Fask.Server.Interfaces.Vyroba.IVyroba_00





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
                    "From " + Common.TABLE_PRODUCTION + " p with (nolock) " +
                    "Where not exists ( " +
                    "select SOUBEHGUID " +
                    "from " + Common.TABLE_PRODUCTION + " with (nolock) " +
                    "where " +
                    "(TIMESTOP is not null or TIMEPREPSTOP is not null) " +
                    "and " +
                    "SOUBEHGUID=p.SOUBEHGUID " +
                    ") " +
                    "and SOUBEHGUID is not null " +
                    "order by id "
                    );
                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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
            da.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

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
            odbcda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

            odbcda.SelectCommand.CommandText = "Select * from CZPRO_VPH Where TermID=0 OR TermID=" + terminalID;
            odbcda.Fill(dsvyroba.CZPRO_VPH);

            return dsvyroba;
        }

        public Fask.Interfaces.DataSets.Vyroba GetPolozky(VyrobniPrikazHlavicka hlavicka)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba dsvyroba = new Fask.Interfaces.DataSets.Vyroba();
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
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
                sqlda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

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

        public Report_UserDay Get_Report_UserDay(string UserID, DateTime datetimeLogin, DateTime datetimeLastOperation)
        {
            // pripojeni k sql serveru ...
            System.Data.SqlClient.SqlConnection sqlconnection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

            // data adapter pro dotazy ...
            System.Data.SqlClient.SqlDataAdapter sqlda = new System.Data.SqlClient.SqlDataAdapter();

            Report_UserDay rud = new Report_UserDay();
            rud.UserID = UserID;
            rud.UserLogin = datetimeLogin;
            rud.UserLastOperation = datetimeLastOperation;

            // 3) vytahnout data uzivatele z rozsahu prihlaseni-odhlaseni/posledniakce
            // 4) spocitat z dat casy Normovany, Skutecny cas, celkovy cas korekci

            Fask.Interfaces.DataSets.Vyroba vds = new Fask.Interfaces.DataSets.Vyroba();

            //    // 3) vytahnout veskerou produkci uzivatele ... 

            System.Data.SqlClient.SqlCommand pcommand = new System.Data.SqlClient.SqlCommand();
            pcommand.Connection = sqlconnection;
            pcommand.CommandText = "Select * from " + Common.TABLE_PRODUCTION + " where userid=@userid and dateeve between @tStart and @tEnd order by dateeve desc";
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
                    "From " + Common.TABLE_PRODUCTION + " " +
                    "Where isnull(UserID,'')=@UserID " +
                    "AND isnull(MachineID,'')=@MachineID " +
                    "AND TIMECRID is not null " + //jedna se o korekci ... 
                    "Order by dateeve desc" +
                    ""
                    );

                ta_production.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                ta_production.SelectCommand.Parameters.AddWithValue("@MachineID", MachineID);

                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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
                    "From " + Common.TABLE_PRODUCTION + " " +
                    "Where isnull(UserID,'')=@UserID " +
                    (AllowUserProductionOnMoreMachines ? "AND isnull(MachineID,'')=@MachineID " : string.Empty) +
                    "AND TIMECRID is null " + //neni to korekce
                    "Order by dateeve desc" +
                    ""
                    );

                ta_production.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                ta_production.SelectCommand.Parameters.AddWithValue("@MachineID", MachineID);

                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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
                // 23.11.2016 JiS => dle Fask.Interfaces.DataSets.Vyroba_P.ucHistorie.timerUpdateThreadStart() ...
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = new Fask.Interfaces.DataSets.Vyroba();
                System.Data.SqlClient.SqlDataAdapter ta_production = new System.Data.SqlClient.SqlDataAdapter();

                ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand("Select * From " + Common.TABLE_PRODUCTION + " ");

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
                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                ta_production.Fill(ds_vyroba.Production);

                return ds_vyroba;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fask.Interfaces.DataSets.Vyroba ProductionHistoryFilter(FiltersHistory filtersHistory, int nLastHours)
        {
            try
            {
                // 23.11.2016 JiS => dle Fask.Interfaces.DataSets.Vyroba_P.ucHistorie.timerUpdateThreadStart() ...
                Fask.Interfaces.DataSets.Vyroba ds_vyroba = new Fask.Interfaces.DataSets.Vyroba();
                System.Data.SqlClient.SqlDataAdapter ta_production = new System.Data.SqlClient.SqlDataAdapter();

                ta_production.SelectCommand = new System.Data.SqlClient.SqlCommand("Select * From " + Common.TABLE_PRODUCTION + " ");

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
                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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
                    "From " + Common.TABLE_PRODUCTION + " " +
                    "Where isnull(UserID,'')=@UserID " +
                    "AND isnull(MachineID,'')=@MachineID " +
                    (Corrections.HasValue ? (Corrections.Value ? "AND TIMECRID is not null " : "AND TIMECRID is null ") : "") +
                    "Order by dateeve desc" +
                    ""
                    );

                ta_production.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
                ta_production.SelectCommand.Parameters.AddWithValue("@MachineID", MachineID);

                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                ta_production.Fill(ds_vyroba.Production);

                return ds_vyroba;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fask.Interfaces.DataSets.Vyroba_StatistikaOdvadeni Production_Filter(FiltersHistory filter)
        {
            try
            {
                Fask.Interfaces.DataSets.Vyroba_StatistikaOdvadeni ds = new Fask.Interfaces.DataSets.Vyroba_StatistikaOdvadeni();

                System.Data.SqlClient.SqlConnection sqlconnection = null;
                System.Data.SqlClient.SqlCommand sqlcommand = null;
                sqlconnection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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
                    "From " + Common.TABLE_PRODUCTION + " " +
                    "Where SoubehGUID=@SoubehGUID " +
                    "Order by dateeve desc" +
                    ""
                    );

                ta_production.SelectCommand.Parameters.AddWithValue("@SoubehGUID", soubehGUID);

                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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
            //Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.ProductionTableAdapter tap = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.ProductionTableAdapter();
            SqlConnection con;

            con = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            System.Data.SqlClient.SqlTransaction sqltrans = null;
            try
            {
                // v rámci transakce projít všechny věci (všechny záznamy zkontrolovat na GUID a následně provést update pomocí transakce)
                con.Open();
                sqltrans = con.BeginTransaction();
                //tap.Adapter.InsertCommand.Transaction = sqltrans;

                //tap.Transaction = sqltrans;

                int termid = 0;

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Zacatek zpracovani zaznamu " + Common.TABLE_PRODUCTION + "(TID:" + termid + ")");
                //foreach (Production.DataServices.Vyroba.ProductionRow item in dtProduction.Select(null, "dateeve"))
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
                        "Update " + Common.TABLE_PRODUCTION + " error: " + ex.Message +
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
            Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter taps = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.Production_SourcesTableAdapter();

            taps.Connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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

            //VyrobaTableAdapters.CorrectsTableAdapter tac = new Production.DataServices.MicrosoftSQL.Net.VyrobaTableAdapters.CorrectsTableAdapter();
            ////VyrobaTableAdapters.CZPRO_VPHTableAdapter tavph = new Production.DataServices.MicrosoftSQL.Net.VyrobaTableAdapters.CZPRO_VPHTableAdapter();
            ////VyrobaTableAdapters.CZPRO_VPPTableAdapter tavpp = new Production.DataServices.MicrosoftSQL.Net.VyrobaTableAdapters.CZPRO_VPPTableAdapter();
            //VyrobaTableAdapters.LoginsTableAdapter tal = new Production.DataServices.MicrosoftSQL.Net.VyrobaTableAdapters.LoginsTableAdapter();
            //VyrobaTableAdapters.MachinesTableAdapter tam = new Production.DataServices.MicrosoftSQL.Net.VyrobaTableAdapters.MachinesTableAdapter();
            //VyrobaTableAdapters.OperationsTableAdapter tao = new Production.DataServices.MicrosoftSQL.Net.VyrobaTableAdapters.OperationsTableAdapter();
            //VyrobaTableAdapters.VMachinesOperationsTableAdapter tavmo = new Production.DataServices.MicrosoftSQL.Net.VyrobaTableAdapters.VMachinesOperationsTableAdapter();
            //VyrobaTableAdapters.ProductionTableAdapter tap = new Production.DataServices.MicrosoftSQL.Net.VyrobaTableAdapters.ProductionTableAdapter();
            //VyrobaTableAdapters.StatusTypesTableAdapter tas = new Production.DataServices.MicrosoftSQL.Net.VyrobaTableAdapters.StatusTypesTableAdapter();
            //VyrobaTableAdapters.UserEventsTableAdapter taue = new Production.DataServices.MicrosoftSQL.Net.VyrobaTableAdapters.UserEventsTableAdapter();

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
            Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.UserEventsTableAdapter taue = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.UserEventsTableAdapter();

            taue.Connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

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
                    " From " + Common.TABLE_PRODUCTION + "" +
                    " Where isnull(UserID,'')=@UserID" +
                    " Order by dateeve desc" +
                    ""
                    );

                ta_production.SelectCommand.Parameters.AddWithValue("@UserID", UserID);

                ta_production.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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

        public Fask.Interfaces.DataSets.Vyroba Vyroba_Online_CheckOperation(string operaceID, string terminalID, out ProductionState productionStateEnabled)
        {
            productionStateEnabled = ProductionState.Unknown;

            bool? enStartPre = false;
            bool? enStartOp = false;
            bool? enEndOp = false;
            string popisOp = string.Empty;
            string machineId = string.Empty;
            int? mnOp = 0;
            string message = string.Empty;
            object returnvalue = null;

            Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.mtj_fask_production_CheckOpTableAdapter vta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.mtj_fask_production_CheckOpTableAdapter();
            vta.Connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

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
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, String.Format("mtj_fask_production_CheckOp returnvalue:{0}\n{1}", (int)returnvalue, message));
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
                productionStateEnabled = ProductionState.Odvod_Stop;
            if (enStartOp ?? false) // START / STOP
                productionStateEnabled = ProductionState.Odvod_Start;
            if (enStartPre ?? false)// START / START / STOP
                productionStateEnabled = ProductionState.Priprava_Start;
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
                ,0
                ,0
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


       //     vyrobaDS.CZPRO_VPP.AddCZPRO_VPPRow(
       //0                   //int CountEntries, 
       //, operationData[0].sopnumbe //string SOPNUMBE, 
       //, operaceID         //string ITEMNMBR, =>operaceID?
       //, string.Empty      //string ITEMTYPE, 
       //, popisOp           //string ITEMDESC, => popis operace
       //, "IMJ"             //string ITEMMJ, 
       //, "VNDDOCNMP"       //string VNDDOCNMP, 
       //, "VNDITNUM"        //string VNDITNUM, 
       //, 1                 //int ORD, 
       //, operaceID         //string BarcodeP, 
       //, "LOCNCODE"        //string LOCNCODE, 
       //, mnOp ?? 0         //decimal QTYSHPPD, 
       //, 0                 //decimal QTYPACK, 
       //, "QMJ"             //string QTYPACKMJ, 
       //, 0                 //float TIMEPREP, 
       //, 0                 //float TIMEUNIT, 
       //, 0                 //byte DtProdT, 
       //, 0                 //short DtProdL, 
       //, 0                 //byte SerNumT, 
       //, 0                 //short SerNumL, 
       //, 0                 //byte VerT, 
       //, 0                 //short VerL, 
       //, 0                 //byte TermID, 
       //, DateTime.Now      //System.DateTime LSTMod, 
       //, -1                //int DEX_ROW_ID, 
       //, 0                 //decimal QTYODVEDENO, => jak toto naplnit???
       //, 0                 //int CNTODVEDENO   => jak toto naplnit???
       //, timemode          //int TIMEMODE    => mod vyroby 
       //, 0                 //BarcodeT
       //, 0                  //QTYDOKON
       //, string.Empty       //ITEMCODE
       //, (object)DBNull.Value
       //, (object)DBNull.Value
       //);

            vyrobaDS.AcceptChanges();
            return vyrobaDS;
        }

        public bool Vyroba_Online_WriteOperation(ProductionObject productionObject, out string message)
        {
            message = string.Empty;

            Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.QueriesTableAdapter qta = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.QueriesTableAdapter();
            qta.ConncetionStringChange(Globals.Konfigurace.ConnectionString[0].FASKDB);
            if (productionObject.stavOperace == ProductionState.Priprava_Start)
            {
                qta.mtj_fask_production_StartPre(productionObject.operaceID, productionObject.terminalID, ref message);
                return true;
            }
            if (productionObject.stavOperace == ProductionState.Priprava_Stop)
            {
                return true;
            }
            else if (productionObject.stavOperace == ProductionState.Odvod_Start)
            {
                qta.mtj_fask_production_StartOp(productionObject.operaceID, productionObject.terminalID, ref message);
                return true;
            }
            else if (productionObject.stavOperace == ProductionState.Odvod_Stop)
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

        public bool VyrobniPrikazBlokace(byte terminalID, VyrobniPrikazHlavicka vyrobniprikaz, BlokaceTyp pozadavekBlokace)
        {
            System.Data.SqlClient.SqlCommand command = null;

            try
            {
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);

                command.Connection.Open();
                System.Data.SqlClient.SqlTransaction transaction = command.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                command.Transaction = transaction;

                byte termIDstate = terminalID;
                //Pokud se zde vrati pocet=0, tak je blokovano jinym terminalem a nelze blokovat...
                if (pozadavekBlokace == BlokaceTyp.Blokovat)
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
                else if (pozadavekBlokace == BlokaceTyp.OdBlokovat)
                {
                    termIDstate = 0;
                }
                else if (pozadavekBlokace == BlokaceTyp.Uzavrit)
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

                Globals.LoadConfiguration();
                sqlConn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
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

        public void UpdateProduction_SN( Fask.Interfaces.DataSets.Vyroba.Production_SNDataTable dtProduction_SN)
        {
            Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.Production_SNTableAdapter taps = new Fask.ModuleSql.SQL_Datasets.VyrobaDataSetTableAdapters.Production_SNTableAdapter();

            taps.Connection = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
            System.Data.SqlClient.SqlTransaction sqltrans = null;
            try
            {
                // v rámci transakce projít všechny věci (všechny záznamy zkontrolovat na GUID a následně provést update pomocí transakce)
                taps.Connection.Open();
                sqltrans = taps.Connection.BeginTransaction();
                taps.Adapter.InsertCommand.Transaction = sqltrans;

                taps.Transaction = sqltrans;

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Zacatek zpracovani zaznamu Production_SN");
                foreach ( Fask.Interfaces.DataSets.Vyroba.Production_SNRow item in dtProduction_SN)
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
                            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Trans. connection: " + sqltrans.Connection.State.ToString());
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

        public Fask.WEBAPI.API_BusinessObjects.FASK_Events_row GetFASKEventsRow(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr)
        {
            //-----------------------------------------------START-------------------------------------------------------

            try
            {
                Globals.LoadConfiguration();

                string par = filtr.separator;
                int pocetPruchoduMax = filtr.pocetPruchodu;

                int pocetPruchodu = 0;

                var list = par.Split(';').ToList();
               Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_out = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();  // vystup
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row fe_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
               Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();
               Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_S1 = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();

                do
                {
                    pocetPruchodu++;
                    Thread.Sleep(filtr.timeSleep);
                    if (pocetPruchodu > pocetPruchoduMax)
                        break;



                    if (filtr.NMBRPAL != null)
                    {
                        DT_S0 = GetFASK_Events_ByStatus((Globals.Konfigurace.ConnectionString[0].FASKDB), -1, filtr.status, list, filtr.NMBRPAL);
                        DT_S1 = GetFASK_Events_ByStatus((Globals.Konfigurace.ConnectionString[0].FASKDB), -1, -1, list, filtr.NMBRPAL);
                    }
                    else
                    {
                        DT_S0 = GetFASK_Events_ByStatus((Globals.Konfigurace.ConnectionString[0].FASKDB), filtr.machineid, filtr.status, list, null);
                        DT_S1 = GetFASK_Events_ByStatus((Globals.Konfigurace.ConnectionString[0].FASKDB), filtr.machineid, filtr.statusNew, list, null);
                    }







                    //ICommDatabase.DSVyroba.FASK_EventsDataTable DT_S0 = new DSVyroba.FASK_EventsDataTable(); // naplnit
                    //ICommDatabase.DSVyroba.FASK_EventsDataTable DT_S1 = new DSVyroba.FASK_EventsDataTable(); // naplnit


                    var c_0 = DT_S0.Count();
                    var c_1 = DT_S1.Count();

                    foreach (var item in DT_S0)
                    {

                        var dt_tmp = DT_S1.Where(x =>
                        x.barcodeReaded == item.barcodeReaded
                        && x.barcodeSended == item.barcodeSended
                        && x.machineid == item.machineid
                        && x.material == item.material
                        && x.NMBRPAL == item.NMBRPAL
                        && x.qty == item.qty
                        && x.qtyReal == item.qtyReal
                        ).ToList();

                        if (dt_tmp.Count == 0)
                        {
                            DT_out.ImportRow(item);
                        }
                    }

                    //do
                    //{
                    //    pocetPruchodu++;
                    //    Thread.Sleep(filtr.timeSleep);
                    //    if (pocetPruchodu > pocetPruchoduMax)
                    //        break;


                } while (DT_out.Count == 0);


                //vrat objekt FASK:Events !!!!!!

                if (DT_out.Count == 1)
                {
                    //return DT_out.First();
                    fe_objekt = VratFASKEventsRow(DT_out.First());
                }
                else
                {

                    if (DT_out.Count > 0)
                    {
                        //return DT_out.OrderByDescending(x => x.dateeve).Last();

                        //tato podminka nema vyznam pokud se i na nakladce berou nejstarsi zaznamy
                        //ma to vyznam jen kdyz se bere nejnovejsi zaznam na nakladce, v tom pripade zmen v else na First!!
                        if (filtr.statusNew == 41 || filtr.statusNew == 42)
                        {
                            fe_objekt = VratFASKEventsRow(DT_out.OrderByDescending(x => x.dateeve).Last());
                        }
                        else
                        {
                            //beru nejstarsi zaznam - nove po dohode s p. Skrivankem ..pozor na chybove zaznamy!!
                            //stare, odkomentovano 12.7.2022

                            fe_objekt = VratFASKEventsRow(DT_out.OrderByDescending(x => x.dateeve).Last());

                            //stare, zakomentovano 12.7.2022
                            //fe_objekt = VratFASKEventsRow(DT_out.OrderByDescending(x => x.dateeve).First());

                        }

                    }
                    else
                    {
                        //vratim prazdny objekt, ktery je nainicializovan!!
                        //fe_objekt = null; //neni inicializace s konstruktorem!!
                    }
                }
                return fe_objekt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //----------------------------------------------END-----------------------------------------------------------

        }

        public Fask.WEBAPI.API_BusinessObjects.FASK_Events_row VratFASKEventsRow(Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow row)
        {
            Fask.WEBAPI.API_BusinessObjects.FASK_Events_row fe = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
            if (row != null)
            {
                // TODO  naplnit cely objekt
                fe.id = row.id;
                fe.loginid = row.loginid;
                fe.machineid = row.machineid;
                fe.dateeve = row.dateeve;
                fe.qty = row.qty;
                fe.qtyReal = row.qtyReal;
                fe.description = row.IsdescriptionNull() ? string.Empty : (string.IsNullOrEmpty(row.description) ? string.Empty : row.description.Trim());
                fe.barcodeReaded = row.barcodeReaded;
                fe.barcodeSended = row.barcodeSended;
                fe.zakazka = row.IszakazkaNull() ? string.Empty : (string.IsNullOrEmpty(row.zakazka) ? string.Empty : row.zakazka.Trim());
                fe.faskGUID = row.faskGUID;
                fe.reportType = row.reportType;
                fe.isProcessed = row.IsisProcessedNull() ? (DateTime?)null : row.isProcessed; //
                fe.IDO = row.IsIDONull() ? string.Empty : (string.IsNullOrEmpty(row.IDO) ? string.Empty : row.IDO.Trim());
                fe.scan1 = row.Isscan1Null() ? string.Empty : (string.IsNullOrEmpty(row.scan1) ? string.Empty : row.scan1.Trim());
                fe.scan2 = row.Isscan2Null() ? string.Empty : (string.IsNullOrEmpty(row.scan2) ? string.Empty : row.scan2.Trim());
                fe.scan3 = row.Isscan3Null() ? string.Empty : (string.IsNullOrEmpty(row.scan3) ? string.Empty : row.scan3.Trim());
                fe.sensor = row.IssensorNull() ? string.Empty : (string.IsNullOrEmpty(row.sensor) ? string.Empty : row.sensor.Trim());
                fe.material = row.IsmaterialNull() ? string.Empty : (string.IsNullOrEmpty(row.material) ? string.Empty : row.material.Trim());
                fe.VPH = row.IsVPHNull() ? string.Empty : (string.IsNullOrEmpty(row.VPH) ? string.Empty : row.VPH.Trim());
                fe.VPPol = row.IsVPPolNull() ? (int?)null : row.VPPol;
                fe.EAN_IS = row.IsEAN_ISNull() ? string.Empty : (string.IsNullOrEmpty(row.EAN_IS) ? string.Empty : row.EAN_IS.Trim());
                fe.IS_ID = row.IsIS_IDNull() ? string.Empty : (string.IsNullOrEmpty(row.IS_ID) ? string.Empty : row.IS_ID.Trim());
                fe.NMBRPAL = row.IsNMBRPALNull() ? string.Empty : (string.IsNullOrEmpty(row.NMBRPAL) ? string.Empty : row.NMBRPAL.Trim());
                //row.SetstatusNull();
                fe.status = row.IsstatusNull() ? (int?)null : row.status;
                fe.productionGuid = row.IsproductionGuidNull() ? (Guid?)null : row.productionGuid;
                fe.popis = row.IspopisNull() ? null : row.popis;
                fe.QTYPACK = row.QTYPACK;
                fe.PackType = row.IsPackTypeNull() ? string.Empty : (string.IsNullOrEmpty(row.PackType) ? string.Empty : row.PackType.Trim());
                fe.WEIGHT = row.IsWEIGHTNull() ? (decimal?)null : row.WEIGHT;
                fe.ITEMDESC = row.IsITEMDESCNull() ? string.Empty : (string.IsNullOrEmpty(row.ITEMDESC) ? string.Empty : row.ITEMDESC.Trim());

            }
            return fe;
        }

        #region get zaznam z Fask_Events status a linka
        private static Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable GetFASK_Events_ByStatus(string connectionString, int MachineID, int Status, List<string> descFilter, string SSCC)
        {
            Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();
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

                        //-----SQL dotaz-----------------------------
                        //SELECT
                        //FE.*
                        //,FZ.ITEMDESC
                        // FROM FASK_Events as FE
                        //left join FASK_ZASOBY FZ on FE.IS_ID = FZ.ITEMNMBR and FE.EAN_IS = FZ.VNDITNUM
                        //WHERE 1 = 1
                        //and productionGuid is not null
                        //and status = 21
                        //AND(1 != 1
                        //OR description like 'presun na streckovacku'
                        //OR description like 'vytisknuto'
                        //)
                        //order by dateeve desc


                        //zkouska vyber korektni zaznamy status 0
                        com.CommandText = "SELECT" +
                            " FE.*" +
                            " ,FZ.ITEMDESC" +
                            " FROM FASK_Events as FE" +
                            " left join FASK_ZASOBY FZ on FE.IS_ID = FZ.ITEMNMBR and FE.EAN_IS = FZ.VNDITNUM" +
                            " WHERE 1 = 1" +
                            " and productionGuid is not null "; //oddelat strednik a nasledujici radek odkomentovat
                                                                // " and machineid = '" + MachineID + "'";
                        //if(Status == -1)
                        //    com.CommandText += " and (status = '21' or status = '22')";
                        //else
                        //    com.CommandText += " and status = '" + Status + "'" ;
                        
                        if (Status == -1)
                        {
                            com.CommandText += " and (status = '21' or status = '22')";
                        }
                        else if (Status == 21 && MachineID != -1)
                        {
                            int Status2 = Status + 1;
                            com.CommandText +=
                            " and (status = '" + Status + "'" +
                            " OR status = '" + Status2 + "')";
                        }
                        else
                        {
                            com.CommandText += " and status = '" + Status + "'";
                        }



                        if (SSCC != null)
                            com.CommandText += " and NMBRPAL = '" + SSCC + "'";


                        if (MachineID != -1)
                            com.CommandText += " and machineid = '" + MachineID + "'";

                        com.CommandText += " AND ( 1 != 1 ";

                        foreach (string item in descFilter)
                        {
                            com.CommandText += " OR description like '" + item + "'";
                        }

                        com.CommandText += " ) ";

                        com.CommandText += " order by dateeve desc";


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

        #endregion

        #region get zaznam z Fask_Events SSCC a description

        public Fask.WEBAPI.API_BusinessObjects.FASK_Events_row Vrat_zaznam_By_SSCC(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr)
        {
            //-----------------------------------------------START-------------------------------------------------------

            try
            {
                Globals.LoadConfiguration();

                string par = filtr.separator;
                int pocetPruchoduMax = filtr.pocetPruchodu;

                int pocetPruchodu = 0;

                var list = par.Split(';').ToList();
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_out = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();  // vystup
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row fe_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();

                do
                {
                    pocetPruchodu++;
                    Thread.Sleep(filtr.timeSleep);
                    if (pocetPruchodu > pocetPruchoduMax)
                        break;

                    DT_S0 = hledej_zaznamy_By_SSCC((Globals.Konfigurace.ConnectionString[0].FASKDB), list, filtr.NMBRPAL);


                    var c_0 = DT_S0.Count();


                } while (DT_S0.Count == 0);


                //vrat objekt FASK:Events !!!!!!

                if (DT_S0.Count < 1)
                {
                    var message_print_SSCC = String.Format("Hledany zaznam by SSCC--Neni zaznam!!");
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_print_SSCC);
                }


                if (DT_S0.Count == 1)
                {

                    int w = DT_S0.Count;
                    var message_hledej_SSCC = String.Format("Hledany zaznam by SSCC--pocet: {0}", w);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_hledej_SSCC);
                    //return DT_out.First();
                    fe_objekt = VratFASKEventsRow(DT_S0.First());
                }
                else
                {

                    int w = DT_S0.Count;
                    var message_hledej_SSCC = String.Format("Hledany zaznam by SSCC--pocet: {0}", w);
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_hledej_SSCC);

                    fe_objekt = VratFASKEventsRow(DT_S0.OrderByDescending(x => x.dateeve).First());

                }

                return fe_objekt;
            }
            catch (Exception)
            {

                throw;
            }
            //----------------------------------------------END-----------------------------------------------------------

        }


        private static Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable hledej_zaznamy_By_SSCC(string connectionString, List<string> descFilter, string SSCC)
        {

            //SqlCommand comm = null; //CommandBehavior do databaze
            SqlConnection conn = null; //connection do databaze

            try
            {
                Globals.LoadConfiguration();
               // string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

               // string par = filtr.separator;
               // int pocetPruchoduMax = filtr.pocetPruchodu;

                //int pocetPruchodu = 0;

                // List<string> descFilter = par.Split(';').ToList();
                Fask.WEBAPI.API_BusinessObjects.FASK_Events fe_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();

                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();
                //int result = 0;
                using (conn = new SqlConnection(connectionString))
                {


                    using (var com = conn.CreateCommand())
                    {
                        com.CommandType = CommandType.Text;

                        //zkouska vyber korektni zaznamy status 0
                        com.CommandText = "SELECT * FROM FASK_Events" +
                            " WHERE 1 = 1" +
                            " and productionGuid is not null "; //oddelat strednik a nasledujici radek odkomentovat
                                                                // " and machineid = '" + MachineID + "'";

                        if (!string.IsNullOrEmpty(SSCC))
                            com.CommandText += " and NMBRPAL = '" + SSCC + "'";

                        com.CommandText += " AND ( 1 != 1 ";

                        foreach (string item in descFilter)
                        {
                            com.CommandText += " OR description like '" + item + "'";
                        }

                        com.CommandText += " ) ";

                        com.CommandText += " order by dateeve desc";


                        conn.Open();

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.SelectCommand = com;
                            int returnValue = adapter.Fill(DT);

                        }
                    }
                }

                //int w = DT.Count;
                //var message_hledej_SSCC = String.Format("Hledany zaznam by SSCC--pocet: {0}", w);
                //Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_hledej_SSCC);


                //fe_objekt = VratFASKEventsRow(DT.First());

                //if (fe_objekt == null)
                //{
                //    var message_print_SSCC = String.Format("Hledany zaznam by SSCC--Neni zaznam!!");
                //    Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_print_SSCC);
                //}

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


        public Fask.WEBAPI.API_BusinessObjects.FASK_Events_row GetFASK_Events_BySSCC(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr)
        {
            //SqlCommand comm = null; //CommandBehavior do databaze
            SqlConnection conn = null; //connection do databaze

            try
            {
                Globals.LoadConfiguration();
                string connectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

                string par = filtr.separator;
                int pocetPruchoduMax = filtr.pocetPruchodu;

                //int pocetPruchodu = 0;

                List<string> descFilter = par.Split(';').ToList();
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row fe_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();

                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();
                //int result = 0;
                using (conn = new SqlConnection(connectionString))
                {


                    using (var com = conn.CreateCommand())
                    {
                        com.CommandType = CommandType.Text;

                        //zkouska vyber korektni zaznamy status 0
                        com.CommandText = "SELECT * FROM FASK_Events" +
                            " WHERE 1 = 1" +
                            " and productionGuid is not null "; //oddelat strednik a nasledujici radek odkomentovat
                                                                // " and machineid = '" + MachineID + "'";
                        
                        if (filtr.NMBRPAL != null)
                            com.CommandText += " and NMBRPAL = '" + filtr.NMBRPAL + "'";

                        com.CommandText += " AND ( 1 != 1 ";

                        foreach (string item in descFilter)
                        {
                            com.CommandText += " OR description like '" + item + "'";
                        }

                        com.CommandText += " ) ";

                        com.CommandText += " order by dateeve desc";


                        conn.Open();

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.SelectCommand = com;
                            int returnValue = adapter.Fill(DT);

                        }
                    }
                }

                int w = DT.Count;
                var message_hledej_SSCC = String.Format("Hledany zaznam by SSCC--pocet: {0}",w);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_hledej_SSCC);


                fe_objekt = VratFASKEventsRow(DT.First());

                if(fe_objekt == null)
                {
                    var message_print_SSCC = String.Format("Hledany zaznam by SSCC--Neni zaznam!!");
                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_print_SSCC);
                }

                return fe_objekt;


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

        public Fask.WEBAPI.API_BusinessObjects.MachineStateSet GetMachineStateSetEventsRow(Fask.WEBAPI.API_BusinessObjects.Filtr_MachineStateSet filtr)
        {
            Fask.Interfaces.DataSets.Vyroba.MachineStateSetDataTable DT = new Fask.Interfaces.DataSets.Vyroba.MachineStateSetDataTable();
            //Vyroba.FASK_EventsDataTable DT = new Vyroba.FASK_EventsDataTable();
            SqlCommand com = null; //CommandBehavior do databaze
            SqlConnection conn = null; //connection do databaze
            Fask.WEBAPI.API_BusinessObjects.MachineStateSet ms = new Fask.WEBAPI.API_BusinessObjects.MachineStateSet();
            try
            {
                Globals.LoadConfiguration();

                using (conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                {


                    using (com = conn.CreateCommand())
                    {
                        com.CommandType = CommandType.Text;
                        com.CommandText = "SELECT TOP (1) * FROM MachineStateSet WHERE (IP = '" + filtr.IP + "') order by DateModified desc";
                        conn.Open();

                        using (var adapter = new SqlDataAdapter())
                        {
                            adapter.SelectCommand = com;
                            int returnValue = adapter.Fill(DT);

                            if (DT.Count == 1)
                            {
                                var row = DT.First();
                                if (row != null)
                                {
                                    // TODO  naplnit cely objekt
                                    ms.IP = row.IP;
                                    ms.DateModified = row.DateModified;
                                    ms.LastError = row.IsLastErrorNull() ? string.Empty : (string.IsNullOrEmpty(row.LastError) ? string.Empty : row.LastError.Trim());
                                    ms.S0 = row.IsS0Null() ? (int?)null : row.S0;
                                    ms.S1 = row.IsS1Null() ? (int?)null : row.S1;
                                    ms.S2 = row.IsS2Null() ? (int?)null : row.S2;
                                    ms.S3 = row.IsS3Null() ? (int?)null : row.S3;
                                    ms.S4 = row.IsS4Null() ? (int?)null : row.S4;
                                    ms.S5 = row.IsS5Null() ? (int?)null : row.S5;
                                    ms.S6 = row.IsS6Null() ? (int?)null : row.S6;
                                    ms.S7 = row.IsS7Null() ? (int?)null : row.S7;
                                    ms.S8 = row.IsS8Null() ? (int?)null : row.S8;
                                    ms.S9 = row.IsS9Null() ? (int?)null : row.S9;
                                    ms.S10 = row.IsS10Null() ? (int?)null : row.S10;
                                    ms.S11 = row.IsS11Null() ? (int?)null : row.S11;
                                    ms.counter_0 = row.Iscounter_0Null() ? (int?)null : row.counter_0;
                                    ms.counter_1 = row.Iscounter_1Null() ? (int?)null : row.counter_1;
                                    ms.counter_2 = row.Iscounter_2Null() ? (int?)null : row.counter_2;
                                    ms.counter_3 = row.Iscounter_3Null() ? (int?)null : row.counter_3;
                                    ms.counter_4 = row.Iscounter_4Null() ? (int?)null : row.counter_4;
                                    ms.counter_5 = row.Iscounter_5Null() ? (int?)null : row.counter_5;
                                    ms.counter_6 = row.Iscounter_6Null() ? (int?)null : row.counter_6;
                                    ms.counter_7 = row.Iscounter_7Null() ? (int?)null : row.counter_7;
                                    ms.counter_8 = row.Iscounter_8Null() ? (int?)null : row.counter_8;
                                    ms.counter_9 = row.Iscounter_9Null() ? (int?)null : row.counter_9;
                                    ms.counter_10 = row.Iscounter_10Null() ? (int?)null : row.counter_10;
                                    ms.counter_11 = row.Iscounter_11Null() ? (int?)null : row.counter_11;
                                }
                                return ms;

                            }
                            else
                                return null;
                        }
                    }
                }
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
                Globals.LoadConfiguration();

                using (conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (com = conn.CreateCommand())
                    {
                        com.Transaction = transaction;
                        com.CommandType = CommandType.Text;
                       // com.CommandText = "SELECT TOP (1) * FROM MachineStateSet WHERE (IP = '" + filtr.IP + "') order by DateModified desc";

                        com.CommandText = "INSERT INTO [dbo].[MachineStateSet]"+
           "([IP]"+
           ",[DateModified]" +
           ",[S0]" +
           ",[S1]" +
           ",[S2]"+
           ",[S3]"+
           ",[S4]"+
           ",[S5]"+
           ",[S6]"+
           ",[S7]"+
           ",[LastError]"+
           ",[S8]"+
           ",[S9]"+
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
                Globals.LoadConfiguration();

                #region Zkouska zda uz je zaznam se shodnou IP

                using (conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
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

            Fask.ModuleSql.Database.Vyroba.Update(DT);

            #endregion

           
        }
        #endregion

        #region zapis do DB Fask_Events
        public void SetFaskEventsRow(Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_object)
        {

            Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();


            try
            {
                //osetrit vsechny stavy!!!
                var row = DT.NewFASK_EventsRow();

                //row.id = FE_object.id;
                
                if(string.IsNullOrEmpty(FE_object.loginid))
                    throw new InvalidOperationException("FE_object.loginid cannot be null/empty"); //row.loginid = "2019004";
                else
                    row.loginid = FE_object.loginid;

                //if (string.IsNullOrEmpty(FE_object.machineid))
                //    throw new InvalidOperationException("FE_object.machineid cannot be null/empty"); //row.machineid = "1";
                //else
                row.machineid = FE_object.machineid;

                if (FE_object.dateeve.HasValue)
                    row.dateeve = FE_object.dateeve.Value;
                else
                    row.dateeve = DateTime.Now;

                //if(FE_object.qty.Equals(null))
                //    throw new InvalidOperationException("FE_object.qty cannot be null");  //row.qty = FE_object.qty;
                //else
                    row.qty = FE_object.qty;

                //if (FE_object.qtyReal.Equals(null))
                //    throw new InvalidOperationException("FE_object.qtyReal cannot be null");  //row.qty = FE_object.qty;
                //else
                    row.qtyReal = FE_object.qtyReal;

                row.description = FE_object.description;

                //if (string.IsNullOrEmpty(FE_object.barcodeReaded))
                //    throw new InvalidOperationException("FE_object.barcodeReaded cannot be null");  //row.qty = FE_object.qty;
                //else
                    row.barcodeReaded = FE_object.barcodeReaded;


                row.barcodeSended = FE_object.barcodeSended;
                row.zakazka = FE_object.zakazka;

                if (FE_object.faskGUID.HasValue)
                    row.faskGUID = FE_object.faskGUID.Value;
                else
                    row.faskGUID = Guid.NewGuid();

                row.reportType = FE_object.reportType;

                if (FE_object.isProcessed.HasValue)
                    row.isProcessed = FE_object.isProcessed.Value;
                else
                    row.SetisProcessedNull();

                row.IDO = FE_object.IDO;
                row.scan1 = FE_object.scan1;
                row.scan2 = FE_object.scan2;
                row.scan3 = FE_object.scan3;
                row.sensor = FE_object.sensor;
                row.material = FE_object.material;
                row.VPH = FE_object.VPH;

                if (FE_object.VPPol.HasValue)
                    row.VPPol = FE_object.VPPol.Value;
                else
                    row.SetVPPolNull();

                row.EAN_IS = FE_object.EAN_IS;
                row.IS_ID = FE_object.IS_ID;
                row.NMBRPAL = FE_object.NMBRPAL;

                if (FE_object.status.HasValue)
                    row.status = FE_object.status.Value;
                else
                    row.SetstatusNull();

                if (FE_object.productionGuid.HasValue)
                    row.productionGuid = FE_object.productionGuid.Value;
                else
                    row.SetproductionGuidNull();
               
                row.popis = FE_object.popis;
                row.QTYPACK = FE_object.QTYPACK;
                row.PackType = FE_object.PackType;

                if (FE_object.WEIGHT.HasValue)
                    row.WEIGHT = FE_object.WEIGHT.Value;
                else
                    row.SetWEIGHTNull();


                DT.AddFASK_EventsRow(row);



                #region zapis do DB
                int pocetUlozeni = 0;

                pocetUlozeni = Fask.ModuleSql.Database.Vyroba_FaskEvents.Update(DT);

                string message_sent = String.Format("Pocet ulozenych zaznamů: {0}", pocetUlozeni);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);

                #endregion

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);


                if (DT.HasErrors)
                {
                    Logging.ExceptionHandler2.Handle(DT);
                }

                throw ex;
                
            }

        }

        public int Update_StatusByGuid(Fask.WEBAPI.API_BusinessObjects.Filtr_UpdateStatus FE_object)
        {
            SqlConnection conn = null;
            int result;

            try
            {
                Globals.LoadConfiguration();
                using (SqlDataAdapter ada = new SqlDataAdapter())
                {
                    using (conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                    {
                        using (ada.UpdateCommand = conn.CreateCommand())
                        {
                            ada.UpdateCommand.CommandType = System.Data.CommandType.Text;
                            ada.UpdateCommand.CommandText = "UPDATE FASK_Events SET status = @status WHERE (faskGUID = @GUID)";

                            ada.UpdateCommand.Parameters.AddWithValue("@status", FE_object.Status);
                            ada.UpdateCommand.Parameters.AddWithValue("@GUID", FE_object.G);

                            ada.UpdateCommand.Connection.Open();

                            result = ada.UpdateCommand.ExecuteNonQuery();

                            return result;

                        }
                    }
                }
            }
            catch (SqlException sqlex)
            {
                Logging.ExceptionHandler2.Handle(sqlex);
                return -1;

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                return -1;
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

        public void Vaha_zapis(Fask.WEBAPI.API_BusinessObjects.FASK_Events FE_data)
        {
            
        }

        private static Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable Get_FE_ByStatus(string connectionString, int Status, List<string> descFilter)
        {
            Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();
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

                        //-----SQL dotaz-----------------------------

                                       //SELECT*
                                        // FROM[Agro_fask].[dbo].[FASK_Events]
                                        //where 1 = 1
                                        //and productionGuid is not null
                                        //and status = 21
                                        //and(1 != 1
                                        //or description like 'presun na streckovacku'
                                        //)
                                        //order by dateeve


                        //zkouska vyber korektni zaznamy status 0
                        com.CommandText = "SELECT" +
                            " *" +
                            " " +
                            " FROM FASK_Events" +
                            " WHERE 1 = 1" +
                            " and productionGuid is not null "; //oddelat strednik a nasledujici radek odkomentovat
                                                                // " and machineid = '" + MachineID + "'";
                                                                //if(Status == -1)
                                                                //    com.CommandText += " and (status = '21' or status = '22')";
                                                                //else
                          com.CommandText += " and status = " + Status;

                        //if (Status == -1)
                        //{
                        //    com.CommandText += " and (status = '21' or status = '22')";
                        //}
                        //else if (Status == 21 && MachineID != -1)
                        //{
                        //    int Status2 = Status + 1;
                        //    com.CommandText +=
                        //    " and (status = '" + Status + "'" +
                        //    " OR status = '" + Status2 + "')";
                        //}
                        //else
                        //{
                        //    com.CommandText += " and status = '" + Status + "'";
                        //}



                        //if (SSCC != null)
                        //    com.CommandText += " and NMBRPAL = '" + SSCC + "'";


                        //if (MachineID != -1)
                        //    com.CommandText += " and machineid = '" + MachineID + "'";

                        com.CommandText += " AND ( 1 != 1 ";

                        foreach (string item in descFilter)
                        {
                            com.CommandText += " OR description like '" + item + "'";
                        }

                        com.CommandText += " ) ";

                        com.CommandText += " order by dateeve desc";


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
        
        public Fask.WEBAPI.API_BusinessObjects.FASK_Events_row Get_FE_Row(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr)
        {
            //-----------------------------------------------START-------------------------------------------------------

            if(filtr.machineid == 1033)
            {
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row fe_objekt_roboty = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                fe_objekt_roboty = vratZaznamVahy_Roboty(filtr);

                return fe_objekt_roboty;
            }


            try
            {
                Globals.LoadConfiguration();

                string par = filtr.separator;
                int pocetPruchoduMax = filtr.pocetPruchodu;

                int pocetPruchodu = 0;

                var list = par.Split(';').ToList();
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_out = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();  // vystup
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row fe_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_S1 = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();

                //List<FASK_Events> list_FE = new List<FASK_Events>();
                //list_FE = DT_out;

                do
                {
                    pocetPruchodu++;
                    Thread.Sleep(filtr.timeSleep);
                    if (pocetPruchodu > pocetPruchoduMax)
                        break;


                    DT_S0 = Get_FE_ByStatus((Globals.Konfigurace.ConnectionString[0].FASKDB), filtr.status, list);
                    DT_S1 = Get_FE_ByStatus((Globals.Konfigurace.ConnectionString[0].FASKDB), filtr.statusNew, list);



                    //ICommDatabase.DSVyroba.FASK_EventsDataTable DT_S0 = new DSVyroba.FASK_EventsDataTable(); // naplnit
                    //ICommDatabase.DSVyroba.FASK_EventsDataTable DT_S1 = new DSVyroba.FASK_EventsDataTable(); // naplnit


                    var c_0 = DT_S0.Count();
                    var c_1 = DT_S1.Count();

                    foreach (var item in DT_S0)
                    {

                        var dt_tmp = DT_S1.Where(x =>
                        x.barcodeReaded == item.barcodeReaded
                        && x.barcodeSended == item.barcodeSended
                         && x.loginid == item.loginid
                        && x.machineid == item.machineid
                        && x.material == item.material
                        && x.NMBRPAL == item.NMBRPAL
                        && x.qty == item.qty
                        && x.qtyReal == item.qtyReal
                        ).ToList();

                        if (dt_tmp.Count == 0)
                        {
                            DT_out.ImportRow(item);
                        }
                    }


                } while (DT_out.Count == 0);


                //vrat objekt FASK:Events !!!!!!

                if (DT_out.Count == 1)
                {
                    //return DT_out.First();
                    fe_objekt = VratFASKEventsRow(DT_out.First());
                }
                else
                {
                    //return DT_out.OrderByDescending(x => x.dateeve).Last();
                    if (filtr.statusNew == 31 || filtr.statusNew == 32 || filtr.statusNew == 33 || filtr.statusNew == 34)
                    {
                        fe_objekt = VratFASKEventsRow(DT_out.OrderByDescending(x => x.dateeve).Last());
                    }
                    else
                    {
                        fe_objekt = VratFASKEventsRow(DT_out.OrderByDescending(x => x.dateeve).First());

                    }
                }
                return fe_objekt;
            }
            catch (Exception ex)
            {

                 var message_sent = String.Format("Cas: {0}, Nedohledan zaznam!!",DateTime.Now);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, ex.Message);
                return null;
            }
            //----------------------------------------------END-----------------------------------------------------------

        }

        private FASK_Events_row vratZaznamVahy_Roboty(Filtr_FASK_Events filtr)
        {
            try
            {
                Globals.LoadConfiguration();

                string par = filtr.separator;
                int pocetPruchoduMax = filtr.pocetPruchodu;

                int pocetPruchodu = 0;

                var list = par.Split(';').ToList();
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_out = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();  // vystup
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row fe_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_S1 = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();

                //List<FASK_Events> list_FE = new List<FASK_Events>();
                //list_FE = DT_out;

                do
                {
                    pocetPruchodu++;
                    Thread.Sleep(filtr.timeSleep);
                    if (pocetPruchodu > pocetPruchoduMax)
                        break;


                    DT_S0 = Get_FE_ByStatus_Vaha_Roboty((Globals.Konfigurace.ConnectionString[0].FASKDB), filtr.status, list);
                    DT_S1 = Get_FE_ByStatus_Vaha_Roboty((Globals.Konfigurace.ConnectionString[0].FASKDB), filtr.statusNew, list);



                    //ICommDatabase.DSVyroba.FASK_EventsDataTable DT_S0 = new DSVyroba.FASK_EventsDataTable(); // naplnit
                    //ICommDatabase.DSVyroba.FASK_EventsDataTable DT_S1 = new DSVyroba.FASK_EventsDataTable(); // naplnit


                    var c_0 = DT_S0.Count();
                    var c_1 = DT_S1.Count();

                    foreach (var item in DT_S0)
                    {

                        var dt_tmp = DT_S1.Where(x =>
                        x.barcodeReaded == item.barcodeReaded
                        && x.barcodeSended == item.barcodeSended
                         && x.loginid == item.loginid
                        && x.machineid == item.machineid
                        && x.material == item.material
                        && x.NMBRPAL == item.NMBRPAL
                        && x.qty == item.qty
                        && x.qtyReal == item.qtyReal
                        ).ToList();

                        if (dt_tmp.Count == 0)
                        {
                            DT_out.ImportRow(item);
                        }
                    }


                } while (DT_out.Count == 0);


                //vrat objekt FASK:Events !!!!!!

                if (DT_out.Count == 1)
                {
                    //return DT_out.First();
                    fe_objekt = VratFASKEventsRow(DT_out.First());
                }
                else
                {
                    //return DT_out.OrderByDescending(x => x.dateeve).Last();
                    if (filtr.statusNew == 31 || filtr.statusNew == 32)
                    {
                        fe_objekt = VratFASKEventsRow(DT_out.OrderByDescending(x => x.dateeve).Last());
                    }
                    else
                    {
                        fe_objekt = VratFASKEventsRow(DT_out.OrderByDescending(x => x.dateeve).First());

                    }
                }
                return fe_objekt;
            }
            catch (Exception ex)
            {

                var message_sent = String.Format("Cas: {0}, Nedohledan zaznam!!", DateTime.Now);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, ex.Message);
                return null;
            }
        }

        private Vyroba.FASK_EventsDataTable Get_FE_ByStatus_Vaha_Roboty(string connectionString, int Status, List<string> descFilter)
        {
            Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();
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

                        //-----SQL dotaz-----------------------------

                        //SELECT*
                        // FROM[Agro_fask].[dbo].[FASK_Events]
                        //where 1 = 1
                        //and productionGuid is not null
                        //and status = 21
                        //and(1 != 1
                        //or description like 'presun na streckovacku'
                        //)
                        //order by dateeve


                        //zkouska vyber korektni zaznamy status 0
                        com.CommandText = "SELECT" +
                            " *" +
                            " " +
                            " FROM FASK_Events" +
                            " WHERE 1 = 1" +
                            " and productionGuid is not null "; //oddelat strednik a nasledujici radek odkomentovat
                                                                // " and machineid = '" + MachineID + "'";
                                                                //if(Status == -1)
                                                                //    com.CommandText += " and (status = '21' or status = '22')";
                                                                //else
                        com.CommandText += " and status = " + Status;

                        int linka_4_R = 7;
                        int bocedi_3_R = 8;
                        int rucni_vstup_R = 9;
                        int falsak_R = 77;


                        com.CommandText += " and ( machineid = " + linka_4_R;
                        com.CommandText += " or machineid = " + bocedi_3_R;
                        com.CommandText += " or machineid = " + rucni_vstup_R;
                        com.CommandText += " or machineid = " + falsak_R;
                        com.CommandText += " ) ";

                        //if (Status == -1)
                        //{
                        //    com.CommandText += " and (status = '21' or status = '22')";
                        //}
                        //else if (Status == 21 && MachineID != -1)
                        //{
                        //    int Status2 = Status + 1;
                        //    com.CommandText +=
                        //    " and (status = '" + Status + "'" +
                        //    " OR status = '" + Status2 + "')";
                        //}
                        //else
                        //{
                        //    com.CommandText += " and status = '" + Status + "'";
                        //}



                        //if (SSCC != null)
                        //    com.CommandText += " and NMBRPAL = '" + SSCC + "'";


                        //if (MachineID != -1)
                        //    com.CommandText += " and machineid = '" + MachineID + "'";

                        com.CommandText += " AND ( 1 != 1 ";

                        foreach (string item in descFilter)
                        {
                            com.CommandText += " OR description like '" + item + "'";
                        }

                        com.CommandText += " ) ";

                        com.CommandText += " order by dateeve desc";


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

        public Fask.WEBAPI.API_BusinessObjects.FASK_Events Get_FE_List(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr)
        {
            //-----------------------------------------------START-------------------------------------------------------

            try
            {
                Globals.LoadConfiguration();

                string par = filtr.separator;
                int pocetPruchoduMax = filtr.pocetPruchodu;

                int pocetPruchodu = 0;

                var list = par.Split(';').ToList();
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_out = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();  // vystup
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();

                Fask.WEBAPI.API_BusinessObjects.FASK_Events list_FE = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();
                //list_FE = DT_out;

                do
                {
                    pocetPruchodu++;
                    Thread.Sleep(filtr.timeSleep);
                    if (pocetPruchodu > pocetPruchoduMax)
                        break;


                    DT_S0 = GetFASK_Events_ByStatus((Globals.Konfigurace.ConnectionString[0].FASKDB),-1, filtr.status, list, null);

                    var c_0 = DT_S0.Count();

                    foreach (var item in DT_S0)
                    {
                        DT_out.ImportRow(item);

                    }


                } while (DT_out.Count == 0);





                //vrat objekt FASK:Events !!!!!!

                if (DT_out.Count > 0)
                {
                    //return DT_out.First();
                    //fe_objekt = VratFASKEventsRow(DT_out.First());


                    list_FE.rows = DT_out.ToList_FASK_Events().ToArray();

                }
                return list_FE;
            }
            catch (Exception ex)
            {

                var message_sent = String.Format("Cas: {0}, Nedohledan zaznam!!", DateTime.Now);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, ex.Message);
                return null;
            }
            //----------------------------------------------END-----------------------------------------------------------

        }

        public Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable FE_archivace_Rows_data(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr)
        {
            //-----------------------------------------------START-------------------------------------------------------

            try
            {
                Globals.LoadConfiguration();

                string par = filtr.separator;
                //int pocetPruchoduMax = filtr.pocetPruchodu;

                //int pocetPruchodu = 0;

                var list = par.Split(';').ToList();


                Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable();
                

                    DT_S0 = Get_FE_ByMachineId_Rows((Globals.Konfigurace.ConnectionString[0].FASKDB),filtr.machineid, filtr.status, list);

                var x = DT_S0.Count;


                return DT_S0;
            }
            catch (Exception ex)
            {

                var message_sent = String.Format("FE_archivace_Rows--Nedohledan zaznam!!");
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, ex.Message);
                return null;
            }
            //----------------------------------------------END-----------------------------------------------------------

        }
        #region ADAM_data_konfigurace

        public Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable Konfigurace_ADAM_data(int ID_group)
        {
            //-----------------------------------------------START-------------------------------------------------------

            try
            {
                Globals.LoadConfiguration();

                Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.MachinesDefinitionDataTable();


               // Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable();


                DT_S0 = Get_ADAM_konfig_Rows((Globals.Konfigurace.ConnectionString[0].FASKDB), ID_group);

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


        #endregion

        #region zaznamy z Fask_Events k archivaci
#if true
        private Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable Get_FE_ByMachineId_Rows(string connectionString, int MachineID, int Status, List<string> descFilter)
        {
            Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable DT = new Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable();
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

                        //SELECT
                        //                  FE.id
                        //                  ,FE.dateeve
                        //                  ,FE.description
                        //                  ,FE.status
                        //                  ,FE.NMBRPAL
                        //                  ,FE.PackType
                        //                  ,FZ.ITEMDESC
                        //                   FROM[fask].[dbo].[FASK_Events] as FE
                        //                  left join[fask].[dbo].[FASK_ZASOBY] FZ on FE.IS_ID = FZ.ITEMNMBR and FE.EAN_IS = FZ.VNDITNUM
                        //                  WHERE 1 = 1
                        //                  and productionGuid is not null
                        //                  and status = 0
                        //                  and machineid = 1
                        //                  AND(1 != 1
                        //                  OR description like 'automaticke ulozeni paleta'
                        //                  OR description like 'posledni paleta 1'
                        //                  OR description like 'posledni paleta 3'
                        //                  )
                        //                  order by dateeve desc

                        //---------------SQL dotaz-----------------------------
                        //SELECT
                        //    FE.id
                        //    ,FE.dateeve
                        //    ,FE.faskGUID
                        //    ,RTRIM(LTRIM(FE.machineid)) as machineid
                        //    ,FE.description
                        //    ,FE.status
                        //    ,FE.NMBRPAL
                        //    ,FE.PackType
                        //    ,RTRIM(LTRIM(FZ.ITEMDESC)) as ITEMDESC
                        //    FROM[dbo].[FASK_Events] as FE
                        //    left join[dbo].[FASK_ZASOBY] FZ on FE.IS_ID = FZ.ITEMNMBR and FE.EAN_IS = FZ.VNDITNUM
                        //    WHERE 1 = 1
                        //    and productionGuid is not null
                        //    and status = 21
                        //    or status = 31
                        //    or status = 41
                        //order by status desc
                        //, dateeve desc

                        //---------------SQL dotaz Vykladka-----------------------------




                        com.CommandText = "SELECT DISTINCT" +
                            " FE.id" +
                            " ,FE.dateeve" +
                            " ,FE.faskGUID" +
                            " ,RTRIM(LTRIM(FE.machineid)) as machineid" +
                            " ,FE.description" +
                            " ,FE.status" +
                            " ,FE.NMBRPAL" +
                            " ,FE.PackType" +
                            " ,RTRIM(LTRIM(FZ.ITEMDESC)) as ITEMDESC" +
                            " FROM [dbo].[FASK_Events] as FE" +
                            " INNER join [dbo].[FASK_ZASOBY] FZ on FE.IS_ID = FZ.ITEMNMBR and FE.EAN_IS = FZ.VNDITNUM" +
                            " WHERE 1 = 1" +
                            " and productionGuid is not null ";



                        if (MachineID == -1)
                        {
                            if (Status == 21)
                            {
                                com.CommandText += " and status = '" + Status + "'";
                                com.CommandText += " or status = '" + (Status + 10) + "'";
                                com.CommandText += " or status = '" + (Status + 20) + "'";
                                com.CommandText += " or status = '" + (Status + 30) + "'";
                            }
                            else if (Status == 22)
                            {
                                com.CommandText += " and status = '" + Status + "'";
                                com.CommandText += " or status = '" + (Status + 10) + "'";
                                com.CommandText += " or status = '" + (Status + 20) + "'";
                                com.CommandText += " or status = '" + (Status + 30) + "'";
                            }
                            else if (Status == 24)
                            {
                                com.CommandText += " and status = '" + Status + "'";
                                com.CommandText += " or status = '" + (Status + 10) + "'";
                                com.CommandText += " or status = '" + (Status + 20) + "'";
                                com.CommandText += " or status = '" + (Status + 30) + "'";
                            }
                        }
                        else if(MachineID == AGRO.bocedi_1 || MachineID == AGRO.bocedi_2)
                        {
                            if(Status ==21)
                            {
                                com.CommandText += " and status = '" + Status + "'";
                                com.CommandText += " or status = '" + (Status+10) + "'";
                                com.CommandText += " or status = '" + (Status + 20) + "'";
                                com.CommandText += " or status = '" + (Status + 30) + "'";
                            }
                            else if(Status == 22)
                            {
                                com.CommandText += " and status = '" + Status + "'";
                                com.CommandText += " or status = '" + (Status + 10) + "'";
                                com.CommandText += " or status = '" + (Status + 20) + "'";
                                com.CommandText += " or status = '" + (Status + 30) + "'";
                            }
                        }
                        else
                        {
                            com.CommandText += " and machineid = '" + MachineID + "'";

                            com.CommandText += " and status = '" + Status + "'";

                            com.CommandText += " AND ( 1 != 1 ";

                            foreach (string item in descFilter)
                            {
                                com.CommandText += " OR description like '" + item + "'";
                            }

                            com.CommandText += " ) ";
                        }


                        com.CommandText += " order by status ";
                        com.CommandText += ", dateeve desc";


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

        public int FE_archivace_Rows_deaktivace_nakladka(Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable filtr)
        {
            try
            {
                int pocetZaznamu = 0;
                //int cisloLinky = filtr.machineid;
                //pocetZaznamu = ReturnPocetArchZaznamu(cisloLinky);

                //----nova logika deaktivace
                foreach (Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceRow item in filtr)
                {
                    pocetZaznamu += ReturnPocetArchZaznamu_nakladka(item.faskGUID);
                }

                return pocetZaznamu;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, ex.Message);
                return 0;
            }
        }

        public int ReturnPocetArchZaznamu_nakladka(Guid guid)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                int pocetZaznamu = 0;

                Globals.LoadConfiguration();
                sqlConn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

                sqlComm.CommandText = "FASKEvents_Archivace";

                sqlComm.Parameters.AddWithValue("@faskID", guid);

                //sqlComm.Parameters.AddWithValue("@cisloLinky", cisloLinky);
                //------vraceni promenne START-----------------------
                // Return value as parameter
                SqlParameter returnPocet = new SqlParameter("pocet", SqlDbType.Int);
                returnPocet.Direction = ParameterDirection.ReturnValue;
                sqlComm.Parameters.Add(returnPocet);

                // Execute the stored procedure
                sqlComm.Connection.Open();
                sqlComm.ExecuteNonQuery();
                //myConnection.Close();

                pocetZaznamu = (int)returnPocet.Value;
                //------vraceni promenne END-----------------------

                //sqlComm.Connection.Open();


                return pocetZaznamu;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, ex.Message);
                return 0;
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }
            }
        }

        public int FE_archivace_Rows_deaktivace_vykladka(Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceDataTable filtr)
        {
            try
            {
                int pocetZaznamu = 0;
                //int cisloLinky = filtr.machineid;
                //pocetZaznamu = ReturnPocetArchZaznamu(cisloLinky);

                //----nova logika deaktivace
                foreach (Fask.Interfaces.DataSets.Vyroba.FASK_Events_archivaceRow item in filtr)
                {
                    pocetZaznamu += ReturnPocetArchZaznamu_vykladka(item.faskGUID);
                }

                return pocetZaznamu;
            }
            catch (Exception ex)
            {

                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, ex.Message);
                return 0;
            }
        }

        public int ReturnPocetArchZaznamu_vykladka(Guid guid)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                int pocetZaznamu = 0;

                Globals.LoadConfiguration();
                sqlConn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                //sqlComm.CommandText = "FASK_procGetSarzeVyroba";

                //if (cisloLinky == 21 || cisloLinky == 22)
                //{
                //    sqlComm.CommandText = "FASKEvents_Archivace_Vykladka";
                //}
                //else
                //{
                //    sqlComm.CommandText = "FASKEvents_Archivace";
                //}

                sqlComm.CommandText = "FASKEvents_Archivace_Vykladka";

                sqlComm.Parameters.AddWithValue("@faskID", guid);

                //sqlComm.Parameters.AddWithValue("@cisloLinky", cisloLinky);
                //------vraceni promenne START-----------------------
                // Return value as parameter
                SqlParameter returnPocet = new SqlParameter("pocet", SqlDbType.Int);
                returnPocet.Direction = ParameterDirection.ReturnValue;
                sqlComm.Parameters.Add(returnPocet);

                // Execute the stored procedure
                sqlComm.Connection.Open();
                sqlComm.ExecuteNonQuery();
                //myConnection.Close();

                pocetZaznamu = (int)returnPocet.Value;
                //------vraceni promenne END-----------------------

                //sqlComm.Connection.Open();


                return pocetZaznamu;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, ex.Message);
                return 0;
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }
            }
        }



#endif

        #endregion

        #endregion

        #region konzola

        #region IZbozi2
        public Fask.Interfaces.DataSets.Zbozi GetFiltrovaneZbozi(Fask.Interfaces.Filtry.ZboziListFiltr filtr)
        {

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

            try
            {
                Globals.LoadConfiguration();

                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = " SELECT Z.DEX_ROW_ID as DEX_ROW_ID_Zbozi ,Z.ITEMNMBR ,Z.ITEMDESC ,Z.ITEMCODE ,Z.VNDITNUM ,Z.CZ_CarKod ,Z.LOCNCODE ,Z.SKL_ID " +
                " ,Z.QTY ,Z.QTYPACK ,Z.MJ ,Z.DMJ ,Z.TAXRATE ,Z.PRICE0 ,Z.PRICE1 ,Z.PRICE2 ,Z.PRICE3 ,Z.PRICE4 ,Z.PRICE5 ,Z.CZ_SerNum_Track " +
                " ,Z.CZ_SerNum_Delka ,Z.CZ_Rez1_Track ,Z.CZ_Rez2_Track ,Z.CZ_Rez3_Track ,Z.CZ_Rez4_Track ,Z.REZ1 ,Z.REZ2 ,Z.REZ3 ,Z.REZ4 ,Z.ODB_ID ,Z.mena_ID ,Z.SERLTNUM ,Z.WEIGHT ,Z.TIMEFROM ,Z.TIMETO ,Z.LSTMod ,Z.loginid " +
                " ,P.DEX_ROW_ID as DEX_ROW_ID_PARAMETRY ,P.VPrFVTS ,P.VPrFPTS ,P.VPrFDTS ,P.VPrFITS ,P.VPrFXTS ,P.RefVPrFVTS ,P.RefVPrFPTS ,P.RefVPrFDTS ,P.RefVPrFITS ,P.RefVPrFXTS ,P.VPrTIMEPREP ,P.VPrTIMEUNIT ,P.RefVPrTIMEMODE " +
                " ,S.skl_desc as SKL_DESC" +
                " FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " as Z " +
                " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY + " as P ON Z.ITEMNMBR = P.ITEMNMBR" +
                " LEFT JOIN " + Fask.SQL.Constants.Common.TABLE_CZMST093 + " as S ON S.skl_id = Z.SKL_ID";


                command.CommandText += " where 1=1 ";

                if (!string.IsNullOrEmpty(filtr.MaterialID))
                {
                    command.CommandText += "AND Z.ITEMNMBR=@itemnmbr ";
                    command.Parameters.AddWithValue("@itemnmbr", filtr.MaterialID);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialNazev))
                {
                    command.CommandText += "AND Z.ITEMDESC like '%' + @nazev + '%' ";
                    command.Parameters.AddWithValue("@nazev", filtr.MaterialNazev);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialItemcode))
                {
                    command.CommandText += "AND Z.ITEMCODE=@itemcode ";
                    command.Parameters.AddWithValue("@itemcode", filtr.MaterialItemcode);
                }

                if (!string.IsNullOrEmpty(filtr.MaterialBarcode))
                {
                    command.CommandText += "AND (Z.CZ_CarKod=@barcode or Z.VNDITNUM=@barcode) ";
                    command.Parameters.AddWithValue("@barcode", filtr.MaterialBarcode);
                }

                if (!string.IsNullOrEmpty(filtr.SKL_ID))
                {
                    command.CommandText += "AND (Z.SKL_ID=@SKL_ID) ";
                    command.Parameters.AddWithValue("@SKL_ID", filtr.SKL_ID);
                }

                if (filtr.ZobrazitDuplicitniCaroveKody)
                {
                    command.CommandText +=
                        "AND Z.VNDITNUM IN ( " +
                        "   SELECT VNDITNUM " +
                        "   FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " " +
                        "   where VNDITNUM <> '' " +
                        "   group by VNDITNUM " +
                        "   HAVING COUNT(*) > 1" +
                        ") "
                        ;
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_ZASOBY_ALL_KONZOLA);

                return ds;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //Fask.Logging.ExceptionHandler2.Handle(ds);
                throw ex;
            }
        }

        public Fask.Interfaces.DataSets.Zbozi GetZbozi()
        {
            Fask.Interfaces.Filtry.ZboziListFiltr filtr = new Fask.Interfaces.Filtry.ZboziListFiltr();
            return GetFiltrovaneZbozi(filtr);
        }

        public bool DeleteZbozi(int id_ZBOZI, int? ID_Params)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;


            try
            {
                Globals.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY;
                    comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";
                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }

                if (ID_Params.HasValue)
                {
                    using (SqlCommand comm = connection.CreateCommand())
                    {
                        comm.Transaction = trans;
                        comm.CommandType = System.Data.CommandType.Text;
                        comm.CommandText = "DELETE FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY;
                        comm.CommandText += " WHERE (DEX_ROW_ID = '" + ID_Params + "')";
                        comm.ExecuteNonQuery();

                        if (trans != null)
                            trans.Commit();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow GetZboziByID(string id)
        {
            try
            {

                if (string.IsNullOrEmpty(id))
                    return null;

                Fask.Interfaces.Filtry.ZboziListFiltr filtr = new Fask.Interfaces.Filtry.ZboziListFiltr();
                filtr.MaterialID = id;
                Fask.Interfaces.DataSets.Zbozi ds = GetFiltrovaneZbozi(filtr);



                if ((ds != null) && (ds.FASK_ZASOBY_ALL_KONZOLA.Count > 0))
                {
                    return ds.FASK_ZASOBY_ALL_KONZOLA.First();
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                throw;
            }
        }

        #region Import Zbozi

        public string ImportZbozi()
        {
            ExportKatalogZasoby_Procedura();

            return "OK";
        }

        private string ExportKatalogZasoby_Procedura()
        {
            System.Data.SqlClient.SqlConnection adpaconnection = null;

            try
            {
                Globals.LoadConfiguration();

                adpaconnection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                System.Data.SqlClient.SqlCommand adpacommand = new System.Data.SqlClient.SqlCommand("FASK_proc_EXPORT_SQL_FASK_ZASOBY");
                adpacommand.CommandType = CommandType.StoredProcedure;

                adpacommand.CommandTimeout = 1000;

                adpacommand.Parameters.Add((new SqlParameter("@ExportTypFilter", SqlDbType.NVarChar, 100)));
                adpacommand.Parameters.Add((new SqlParameter("@ExportSkladFilter", SqlDbType.NVarChar, 100)));
                adpacommand.Parameters.Add((new SqlParameter("@ExportovatPouzeAktivniPolozky", SqlDbType.Bit)));
                adpacommand.Parameters.Add((new SqlParameter("@EXZas_DotahovatAlternativniDodavatele", SqlDbType.Bit)));
                adpacommand.Parameters.Add((new SqlParameter("@EvidenceSarzi", SqlDbType.Bit)));
                adpacommand.Parameters.Add((new SqlParameter("@EvidenceVyrobnichCisel", SqlDbType.Bit)));

                //((IDataParameter)adpacommand.Parameters["@ExportTypFilter"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportTypFilter;
                //((IDataParameter)adpacommand.Parameters["@ExportSkladFilter"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportSkladFilter;
                //((IDataParameter)adpacommand.Parameters["@ExportovatPouzeAktivniPolozky"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].ExportovatPouzeAktivniPolozky;
                //((IDataParameter)adpacommand.Parameters["@EXZas_DotahovatAlternativniDodavatele"]).Value = Globals_V1.Konfigurace.ExportZasoby[0].EXZas_DotahovatAlternativniDodavatele;
                //((IDataParameter)adpacommand.Parameters["@EvidenceSarzi"]).Value = Globals_V1.Konfigurace.Sdilene[0].EvidenceSarzi;
                //((IDataParameter)adpacommand.Parameters["@EvidenceVyrobnichCisel"]).Value = Globals_V1.Konfigurace.Sdilene[0].EvidenceVyrobnichCisel;

                ((IDataParameter)adpacommand.Parameters["@ExportTypFilter"]).Value = "";
                ((IDataParameter)adpacommand.Parameters["@ExportSkladFilter"]).Value = "";
                ((IDataParameter)adpacommand.Parameters["@ExportovatPouzeAktivniPolozky"]).Value = true;
                ((IDataParameter)adpacommand.Parameters["@EXZas_DotahovatAlternativniDodavatele"]).Value = true;
                ((IDataParameter)adpacommand.Parameters["@EvidenceSarzi"]).Value = true;
                ((IDataParameter)adpacommand.Parameters["@EvidenceVyrobnichCisel"]).Value = true;



                adpacommand.Connection = adpaconnection;

                adpaconnection.Open();
                adpacommand.ExecuteNonQuery();

                return "OK";
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
            finally
            {
                if (adpaconnection != null && (adpaconnection.State & ConnectionState.Open) == ConnectionState.Open)
                    adpaconnection.Close();
            }
        }

        #endregion

        public bool UpdateZbozi(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;


            try
            {
                Globals.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    comm.CommandText = @"UPDATE " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + " SET" +
                        "  ITEMNMBR = @ITEMNMBR, ITEMDESC = @ITEMDESC, ITEMCODE = @ITEMCODE," +
                        " VNDITNUM = @VNDITNUM, LOCNCODE = @LOCNCODE, CZ_CarKod = @CZ_CarKod," +
                        " SKL_ID = @SKL_ID, QTY = @QTY, QTYPACK = @QTYPACK," +
                        " MJ = @MJ, DMJ = @DMJ, TAXRATE = @TAXRATE," +
                        " PRICE0 = @PRICE0, PRICE1 = @PRICE1, PRICE2 = @PRICE2," +
                        " PRICE3 = @PRICE3, PRICE4 = @PRICE4, PRICE5 = @PRICE5, " +
                        "CZ_SerNum_Track = @CZ_SerNum_Track, CZ_SerNum_Delka = @CZ_SerNum_Delka, CZ_Rez1_Track = @CZ_Rez1_Track," +
                        " CZ_Rez2_Track = @CZ_Rez2_Track, CZ_Rez3_Track = @CZ_Rez3_Track, CZ_Rez4_Track = @CZ_Rez4_Track," +
                        " REZ1 = @REZ1, REZ2 = @REZ2, REZ3 = @REZ3," +
                        " REZ4 = @REZ4, ODB_ID = @ODB_ID, mena_ID = @mena_ID," +
                        " SERLTNUM = @SERLTNUM, WEIGHT = @WEIGHT, TIMEFROM = @TIMEFROM," +
                        " TIMETO = @TIMETO, LSTMod = @LSTMod, loginid = @loginid" +
                        " WHERE (DEX_ROW_ID = @DEX_ROW_ID)";

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.ITEMNMBR) ? throw new Exception("ITEMNMBR is null!") : zboziRow.ITEMNMBR });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = zboziRow.IsITEMDESCNull() ? (object)DBNull.Value : zboziRow.ITEMDESC });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsITEMCODENull() ? (object)DBNull.Value : zboziRow.ITEMCODE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, Value = zboziRow.IsVNDITNUMNull() ? (object)DBNull.Value : zboziRow.VNDITNUM });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, Value = zboziRow.IsCZ_CarKodNull() ? (object)DBNull.Value : zboziRow.CZ_CarKod });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsLOCNCODENull() ? (object)DBNull.Value : zboziRow.LOCNCODE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = zboziRow.IsSKL_IDNull() ? (object)DBNull.Value : zboziRow.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@QTY", DbType = System.Data.DbType.Decimal, Value = zboziRow.QTY });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsQTYPACKNull() ? (object)DBNull.Value : zboziRow.QTYPACK });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.MJ) ? string.Empty : zboziRow.MJ });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@DMJ", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.DMJ) ? string.Empty : zboziRow.DMJ });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TAXRATE", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsTAXRATENull() ? (object)DBNull.Value : zboziRow.TAXRATE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE0", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE0Null() ? (object)DBNull.Value : zboziRow.PRICE0 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE1", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE1Null() ? (object)DBNull.Value : zboziRow.PRICE1 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE2", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE2Null() ? (object)DBNull.Value : zboziRow.PRICE2 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE3", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE3Null() ? (object)DBNull.Value : zboziRow.PRICE3 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE4", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE4Null() ? (object)DBNull.Value : zboziRow.PRICE4 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE5", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE5Null() ? (object)DBNull.Value : zboziRow.PRICE5 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_SerNum_Track }); // not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, Value = zboziRow.CZ_SerNum_Delka });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez1_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez1_Track });// not null

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez2_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez2_Track });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez3_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez3_Track });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez4_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez4_Track });// not null

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ1Null() ? (object)DBNull.Value : zboziRow.REZ1 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ2", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ2Null() ? (object)DBNull.Value : zboziRow.REZ2 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ3", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ3Null() ? (object)DBNull.Value : zboziRow.REZ3 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ4", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ4Null() ? (object)DBNull.Value : zboziRow.REZ4 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = zboziRow.IsODB_IDNull() ? (object)DBNull.Value : zboziRow.ODB_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, Value = zboziRow.Ismena_IDNull() ? (object)DBNull.Value : zboziRow.mena_ID });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, Value = zboziRow.IsSERLTNUMNull() ? (object)DBNull.Value : zboziRow.SERLTNUM });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsWEIGHTNull() ? (object)DBNull.Value : zboziRow.WEIGHT });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEFROM", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsTIMEFROMNull() ? (object)DBNull.Value : zboziRow.TIMEFROM });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TIMETO", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsTIMETONull() ? (object)DBNull.Value : zboziRow.TIMETO });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsLSTModNull() ? (object)DBNull.Value : zboziRow.LSTMod });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, Value = zboziRow.IsloginidNull() ? (object)DBNull.Value : zboziRow.loginid });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, Value = zboziRow.DEX_ROW_ID_Zbozi });

                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool InsertZbozi(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_KONZOLARow zboziRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;


            try
            {
                Globals.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    comm.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY + "([ITEMNMBR], [ITEMDESC], [ITEMCODE], [VNDITNUM], [CZ_CarKod], [LOCNCODE], [SKL_ID], [QTY], [QTYPACK], [MJ], [DMJ], [TAXRATE], [PRICE0], [PRICE1], [PRICE2], [PRICE3], [PRICE4], [PRICE5], [CZ_SerNum_Track], [CZ_SerNum_Delka], [CZ_Rez1_Track], [CZ_Rez2_Track], [CZ_Rez3_Track], [CZ_Rez4_Track], [REZ1], [REZ2], [REZ3], [REZ4], [ODB_ID], [mena_ID], [SERLTNUM], [WEIGHT], [TIMEFROM], [TIMETO], [LSTMod], [loginid]" +
                        " ) VALUES (" +
                        " @ITEMNMBR, @ITEMDESC, @ITEMCODE, " +
                        " @VNDITNUM, @CZ_CarKod, @LOCNCODE, " +
                        " @SKL_ID, @QTY, @QTYPACK, " +
                        " @MJ, @DMJ, @TAXRATE, " +
                        " @PRICE0, @PRICE1, @PRICE2, " +
                        " @PRICE3, @PRICE4, @PRICE5, " +
                        " @CZ_SerNum_Track, @CZ_SerNum_Delka, @CZ_Rez1_Track, " +
                        " @CZ_Rez2_Track, @CZ_Rez3_Track, @CZ_Rez4_Track, " +
                        " @REZ1, @REZ2, @REZ3, " +
                        " @REZ4, @ODB_ID, @mena_ID, " +
                        " @SERLTNUM, @WEIGHT, @TIMEFROM, " +
                        " @TIMETO, @LSTMod, @loginid)";


                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.ITEMNMBR) ? throw new Exception("ITEMNMBR is null!") : zboziRow.ITEMNMBR });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = zboziRow.IsITEMDESCNull() ? (object)DBNull.Value : zboziRow.ITEMDESC });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsITEMCODENull() ? (object)DBNull.Value : zboziRow.ITEMCODE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, Value = zboziRow.IsVNDITNUMNull() ? (object)DBNull.Value : zboziRow.VNDITNUM });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_CarKod", DbType = System.Data.DbType.String, Value = zboziRow.IsCZ_CarKodNull() ? (object)DBNull.Value : zboziRow.CZ_CarKod });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = zboziRow.IsLOCNCODENull() ? (object)DBNull.Value : zboziRow.LOCNCODE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SKL_ID", DbType = System.Data.DbType.String, Value = zboziRow.IsSKL_IDNull() ? (object)DBNull.Value : zboziRow.SKL_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@QTY", DbType = System.Data.DbType.Decimal, Value = zboziRow.QTY });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsQTYPACKNull() ? (object)DBNull.Value : zboziRow.QTYPACK });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@MJ", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.MJ) ? string.Empty : zboziRow.MJ });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@DMJ", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.DMJ) ? string.Empty : zboziRow.DMJ });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TAXRATE", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsTAXRATENull() ? (object)DBNull.Value : zboziRow.TAXRATE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE0", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE0Null() ? (object)DBNull.Value : zboziRow.PRICE0 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE1", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE1Null() ? (object)DBNull.Value : zboziRow.PRICE1 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE2", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE2Null() ? (object)DBNull.Value : zboziRow.PRICE2 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE3", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE3Null() ? (object)DBNull.Value : zboziRow.PRICE3 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE4", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE4Null() ? (object)DBNull.Value : zboziRow.PRICE4 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@PRICE5", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsPRICE5Null() ? (object)DBNull.Value : zboziRow.PRICE5 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_SerNum_Track }); // not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_SerNum_Delka", DbType = System.Data.DbType.Int16, Value = zboziRow.CZ_SerNum_Delka });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez1_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez1_Track });// not null

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez2_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez2_Track });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez3_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez3_Track });// not null
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@CZ_Rez4_Track", DbType = System.Data.DbType.Byte, Value = zboziRow.CZ_Rez4_Track });// not null

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ1", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ1Null() ? (object)DBNull.Value : zboziRow.REZ1 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ2", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ2Null() ? (object)DBNull.Value : zboziRow.REZ2 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ3", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ3Null() ? (object)DBNull.Value : zboziRow.REZ3 });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@REZ4", DbType = System.Data.DbType.String, Value = zboziRow.IsREZ4Null() ? (object)DBNull.Value : zboziRow.REZ4 });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ODB_ID", DbType = System.Data.DbType.String, Value = zboziRow.IsODB_IDNull() ? (object)DBNull.Value : zboziRow.ODB_ID });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@mena_ID", DbType = System.Data.DbType.String, Value = zboziRow.Ismena_IDNull() ? (object)DBNull.Value : zboziRow.mena_ID });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@SERLTNUM", DbType = System.Data.DbType.String, Value = zboziRow.IsSERLTNUMNull() ? (object)DBNull.Value : zboziRow.SERLTNUM });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, Value = zboziRow.IsWEIGHTNull() ? (object)DBNull.Value : zboziRow.WEIGHT });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEFROM", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsTIMEFROMNull() ? (object)DBNull.Value : zboziRow.TIMEFROM });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@TIMETO", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsTIMETONull() ? (object)DBNull.Value : zboziRow.TIMETO });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, Value = zboziRow.IsLSTModNull() ? (object)DBNull.Value : zboziRow.LSTMod });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.String, Value = zboziRow.IsloginidNull() ? (object)DBNull.Value : zboziRow.loginid });

                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }

        #region Parametry
        public bool InsertZboziParams(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_PARAMETRY_KONZOLARow zboziRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    comm.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY +
                        " ([ITEMNMBR], [VPrFVTS], [VPrFPTS]," +
                        " [VPrFDTS], [VPrFITS], [VPrFXTS]," +
                        " [RefVPrFVTS], [RefVPrFPTS], [RefVPrFDTS]," +
                        " [RefVPrFITS], [RefVPrFXTS], [VPrTIMEPREP]," +
                        " [VPrTIMEUNIT], [RefVPrTIMEMODE])" +
                        " VALUES (@ITEMNMBR, @VPrFVTS, @VPrFPTS," +
                        " @VPrFDTS, @VPrFITS, @VPrFXTS," +
                        " @RefVPrFVTS, @RefVPrFPTS, @RefVPrFDTS," +
                        " @RefVPrFITS, @RefVPrFXTS, @VPrTIMEPREP," +
                        " @VPrTIMEUNIT, @RefVPrTIMEMODE)";

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.ITEMNMBR) ? throw new Exception("ITEMNMBR is null!") : zboziRow.ITEMNMBR });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFVTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFVTSNull() ? (object)DBNull.Value : zboziRow.VPrFVTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFPTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFPTSNull() ? (object)DBNull.Value : zboziRow.VPrFPTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFDTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFDTSNull() ? (object)DBNull.Value : zboziRow.VPrFDTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFITS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFITSNull() ? (object)DBNull.Value : zboziRow.VPrFITS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFXTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFXTSNull() ? (object)DBNull.Value : zboziRow.VPrFXTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFVTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFVTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFVTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFPTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFPTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFPTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFDTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFDTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFDTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFITS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFITSNull() ? (object)DBNull.Value : zboziRow.RefVPrFITS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFXTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFXTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFXTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEPREP", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEPREPNull() ? (object)DBNull.Value : zboziRow.VPrTIMEPREP });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEUNIT", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEUNITNull() ? (object)DBNull.Value : zboziRow.VPrTIMEUNIT });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrTIMEMODE", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrTIMEMODENull() ? (object)DBNull.Value : zboziRow.RefVPrTIMEMODE });

                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }
        }
        public bool UpdateZboziParams(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow)
        {
            if (GetParametrybyID(zboziRow.ITEMNMBR))
            {
                return UpdateParametry(zboziRow);
            }
            else
            {
                System.Data.SqlClient.SqlTransaction trans = null;
                System.Data.SqlClient.SqlConnection connection = null;

                try
                {
                    Globals.LoadConfiguration();
                    connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                    connection.Open();
                    trans = connection.BeginTransaction();

                    using (SqlCommand comm = connection.CreateCommand())
                    {
                        comm.Transaction = trans;
                        //comm.CommandType = System.Data.CommandType.Text;
                        //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                        //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                        comm.CommandType = global::System.Data.CommandType.Text;
                        comm.CommandText = @"INSERT INTO " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY +
                            " ([ITEMNMBR], [VPrFVTS], [VPrFPTS]," +
                            " [VPrFDTS], [VPrFITS], [VPrFXTS]," +
                            " [RefVPrFVTS], [RefVPrFPTS], [RefVPrFDTS]," +
                            " [RefVPrFITS], [RefVPrFXTS], [VPrTIMEPREP]," +
                            " [VPrTIMEUNIT], [RefVPrTIMEMODE])" +
                            " VALUES (@ITEMNMBR, @VPrFVTS, @VPrFPTS," +
                            " @VPrFDTS, @VPrFITS, @VPrFXTS," +
                            " @RefVPrFVTS, @RefVPrFPTS, @RefVPrFDTS," +
                            " @RefVPrFITS, @RefVPrFXTS, @VPrTIMEPREP," +
                            " @VPrTIMEUNIT, @RefVPrTIMEMODE)";

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.ITEMNMBR) ? throw new Exception("ITEMNMBR is null!") : zboziRow.ITEMNMBR });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFVTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFVTSNull() ? (object)DBNull.Value : zboziRow.VPrFVTS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFPTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFPTSNull() ? (object)DBNull.Value : zboziRow.VPrFPTS });

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFDTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFDTSNull() ? (object)DBNull.Value : zboziRow.VPrFDTS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFITS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFITSNull() ? (object)DBNull.Value : zboziRow.VPrFITS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFXTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFXTSNull() ? (object)DBNull.Value : zboziRow.VPrFXTS });

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFVTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFVTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFVTS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFPTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFPTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFPTS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFDTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFDTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFDTS });

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFITS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFITSNull() ? (object)DBNull.Value : zboziRow.RefVPrFITS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFXTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFXTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFXTS });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEPREP", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEPREPNull() ? (object)DBNull.Value : zboziRow.VPrTIMEPREP });

                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEUNIT", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEUNITNull() ? (object)DBNull.Value : zboziRow.VPrTIMEUNIT });
                        comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrTIMEMODE", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrTIMEMODENull() ? (object)DBNull.Value : zboziRow.RefVPrTIMEMODE });

                        comm.ExecuteNonQuery();

                        if (trans != null)
                            trans.Commit();
                    }


                    return true;
                }
                catch (Exception ex)
                {
                    Logging.ExceptionHandler2.Handle(ex);

                    try
                    {
                        if (trans != null)
                            trans.Rollback();
                    }
                    catch
                    { }
                    throw;

                }
                finally
                {
                    if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        private bool UpdateParametry(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow)
        {
            System.Data.SqlClient.SqlTransaction trans = null;
            System.Data.SqlClient.SqlConnection connection = null;

            try
            {
                Globals.LoadConfiguration();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                connection.Open();
                trans = connection.BeginTransaction();

                using (SqlCommand comm = connection.CreateCommand())
                {
                    comm.Transaction = trans;
                    //comm.CommandType = System.Data.CommandType.Text;
                    //comm.CommandText = "DELETE FROM " + Common.TABLE_FASK_ZASOBY;
                    //comm.CommandText += " WHERE (DEX_ROW_ID = '" + id_ZBOZI + "')";

                    comm.CommandType = global::System.Data.CommandType.Text;
                    comm.CommandText = @"UPDATE " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY +
                        " SET ITEMNMBR = @ITEMNMBR, VPrFVTS = @VPrFVTS, VPrFPTS = @VPrFPTS," +
                        " VPrFDTS = @VPrFDTS, VPrFITS = @VPrFITS, VPrFXTS = @VPrFXTS," +
                        " RefVPrFVTS = @RefVPrFVTS, RefVPrFPTS = @RefVPrFPTS, RefVPrFDTS = @RefVPrFDTS," +
                        " RefVPrFITS = @RefVPrFITS, RefVPrFXTS = @RefVPrFXTS, VPrTIMEPREP = @VPrTIMEPREP," +
                        " VPrTIMEUNIT = @VPrTIMEUNIT, RefVPrTIMEMODE = @RefVPrTIMEMODE" +
                        " WHERE (DEX_ROW_ID = @DEX_ROW_ID)";

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = string.IsNullOrEmpty(zboziRow.ITEMNMBR) ? throw new Exception("ITEMNMBR is null!") : zboziRow.ITEMNMBR });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFVTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFVTSNull() ? (object)DBNull.Value : zboziRow.VPrFVTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFPTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFPTSNull() ? (object)DBNull.Value : zboziRow.VPrFPTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFDTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFDTSNull() ? (object)DBNull.Value : zboziRow.VPrFDTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFITS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFITSNull() ? (object)DBNull.Value : zboziRow.VPrFITS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrFXTS", DbType = System.Data.DbType.Boolean, Value = zboziRow.IsVPrFXTSNull() ? (object)DBNull.Value : zboziRow.VPrFXTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFVTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFVTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFVTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFPTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFPTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFPTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFDTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFDTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFDTS });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFITS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFITSNull() ? (object)DBNull.Value : zboziRow.RefVPrFITS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrFXTS", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrFXTSNull() ? (object)DBNull.Value : zboziRow.RefVPrFXTS });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEPREP", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEPREPNull() ? (object)DBNull.Value : zboziRow.VPrTIMEPREP });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@VPrTIMEUNIT", DbType = System.Data.DbType.Double, Value = zboziRow.IsVPrTIMEUNITNull() ? (object)DBNull.Value : zboziRow.VPrTIMEUNIT });
                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@RefVPrTIMEMODE", DbType = System.Data.DbType.Int32, Value = zboziRow.IsRefVPrTIMEMODENull() ? (object)DBNull.Value : zboziRow.RefVPrTIMEMODE });

                    comm.Parameters.Add(new SqlParameter() { ParameterName = "@DEX_ROW_ID", DbType = System.Data.DbType.Int32, Value = zboziRow.DEX_ROW_ID_PARAMETRY });

                    comm.ExecuteNonQuery();

                    if (trans != null)
                        trans.Commit();
                }


                return true;
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);

                try
                {
                    if (trans != null)
                        trans.Rollback();
                }
                catch
                { }
                throw;

            }
            finally
            {
                if ((connection != null) && (connection.State & ConnectionState.Open) == ConnectionState.Open)
                    connection.Close();
            }

        }

        private bool GetParametrybyID(string ITEMNMBR)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Zbozi ds = new Fask.Interfaces.DataSets.Zbozi();

            try
            {
                Globals.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_ZASOBY_PARAMETRY;

                command.CommandText += " WHERE ITEMNMBR = " + ITEMNMBR;

                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_ZASOBY_PARAMETRY_KONZOLA);

                if ((ds != null) && (ds.FASK_ZASOBY_PARAMETRY_KONZOLA != null) && (ds.FASK_ZASOBY_PARAMETRY_KONZOLA.Count > 0))
                {
                    return true;
                }
                else
                    return false;


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                Fask.Logging.ExceptionHandler2.Handle(ds);
                throw ex;
            }
        }
        #endregion
        #endregion

        #region ISklady2

        #region ISklady2_GetSklady

        public Fask.Interfaces.DataSets.Sklady GetSklady()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Sklady ds = new Fask.Interfaces.DataSets.Sklady();

            try
            {
                Globals.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText = "select * from " + Fask.SQL.Constants.Common.TABLE_CZMST093;
                adapter.SelectCommand = command;
                adapter.Fill(ds.CZMST093);

                return ds;
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region ISklady2_Vyroba_Fill Members

        public void Sklady_Vyroba_Fill( Fask.Interfaces.DataSets.Vyroba ds)
        {
            Fask.ModuleSql.Database.Ciselnik cis = new Fask.ModuleSql.Database.Ciselnik();
            cis.Fill_Universal(Globals.Konfigurace.ConnectionString[0].FASKDB,
                "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST093,
                ds.CZMST093
                );
        }

        #endregion

        #endregion

        #region ILokace2

        #region ILokace2_Fill Members

        public void Fill( Fask.Interfaces.DataSets.Vyroba ds)
        {
            Fask.ModuleSql.Database.Ciselnik cis = new Fask.ModuleSql.Database.Ciselnik();
            cis.Fill_Universal(Globals.Konfigurace.ConnectionString[0].FASKDB,
                "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_CZMST094,
                ds.CZMST094
                );
        }

        #endregion 

        #endregion

        #region Vyroba

        #region Odvod_Events

        public string CallProcedura(DateTime? OD, DateTime? DO, string Material, out int? CountEntries)
        {

            string msg = string.Empty;
            CountEntries = null;

            SqlConnection conn = null;
            SqlDataAdapter adapter = null;
            SqlCommand command = null;

            try
            {
                Globals.LoadConfiguration();
                conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                adapter = new SqlDataAdapter();
                command = new SqlCommand();
                command.CommandText = "fask_Events2Production_Confirm";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@from", OD);
                command.Parameters.AddWithValue("@to", DO);
                command.Parameters.AddWithValue("@material", string.IsNullOrEmpty(Material) ? string.Empty : Material.Trim());
                command.Parameters.Add("@CountEntries", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@Message", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;



                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();


                object tmpCountEntries = command.Parameters["@CountEntries"].Value;
                object tmpmsg = command.Parameters["@Message"].Value;

                if (tmpCountEntries != null)
                    CountEntries = tmpCountEntries as int?;

                if (tmpmsg != null)
                    msg = tmpmsg as string;

                conn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return msg;
        }

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyOdvodEvents(Fask.Interfaces.Filtry.Odvod_EventsListFiltr filtr)
        {

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                Globals.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                //command.CommandText =
                //    "SELECT * FROM " + Fask.Console.Interfaces.Constants.Fask.SQL.Constants.Common.TABLE_FASK_Events +
                //    " WHERE " +
                //    " 1=1 "
                //    ;

                command.CommandText =
                        "SELECT E.*, Z.ITEMDESC FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Events + " as E " +
                        " LEFT JOIN ( SELECT ITEMNMBR, VNDITNUM, MAX(ITEMDESC) as ITEMDESC from FASK_ZASOBY GROUP BY ITEMNMBR, VNDITNUM) as Z " +
                        " ON Z.ITEMNMBR = E.IS_ID AND Z.VNDITNUM = E.EAN_IS " +
                        " WHERE " +
                        " 1=1 ";


                if (filtr.productionGUID != null)
                {

                    //command.CommandText += "and CAST(productionGUID as uniqueidentifier) = CAST(@productionGUID as uniqueidentifier) ";
                    //command.Parameters.AddWithValue("@productionGUID", filtr.productionGUID);


                    command.CommandText += "and E.productionGUID = @productionGUID";
                    command.Parameters.AddWithValue("@productionGUID", filtr.productionGUID);

                    //command.CommandText += "and productionGUID ='" + filtr.productionGUID.ToString().Trim() + "' ";
                }


                if (filtr.Zpracovane && !filtr.NEZpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NOT NULL ";
                }
                else if (filtr.NEZpracovane && !filtr.Zpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NULL ";
                }
                else if (!filtr.NEZpracovane && !filtr.Zpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NULL and E.IsProcessed is NOT NULL ";

                }




                // hledání podle datumu
                if (filtr.IsProcessed_OD != null && filtr.IsProcessed_DO != null)
                {
                    command.CommandText += " AND E.IsProcessed between @IsProcessedOD and @IsProcessedDO";
                    command.Parameters.AddWithValue("@IsProcessedOD", filtr.IsProcessed_OD);
                    command.Parameters.AddWithValue("@IsProcessedDO", filtr.IsProcessed_DO);
                }
                else
                {
                    if (filtr.IsProcessed_OD != null)
                    {
                        command.CommandText += " AND E.IsProcessed > @IsProcessedOD";
                        command.Parameters.AddWithValue("@IsProcessedOD", filtr.IsProcessed_OD);
                    }
                    else if (filtr.IsProcessed_DO != null)
                    {
                        command.CommandText += " AND E.IsProcessed < @IsProcessedDO";
                        command.Parameters.AddWithValue("@IsProcessedDO", filtr.IsProcessed_DO);
                    }
                }

                if (!string.IsNullOrEmpty(filtr.IsProcessed_TimeVariant))
                {
                    if (!filtr.IsProcessed_TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.IsProcessed_TimeVariant.Split(';');
                        TimeFilters.TimeVariants TimeVar = (TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);
                        command.CommandText += " AND E.IsProcessed > @IsProcessed_TV";
                        command.Parameters.AddWithValue("@IsProcessed_TV", TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                    }
                }

                if (filtr.Dateeve_OD != null && filtr.Dateeve_DO != null)
                {
                    command.CommandText += " AND E.dateeve between @dateeveOD and @dateeveDO";
                    command.Parameters.AddWithValue("@dateeveOD", filtr.Dateeve_OD);
                    command.Parameters.AddWithValue("@dateeveDO", filtr.Dateeve_DO);
                }
                else
                {
                    if (filtr.Dateeve_OD != null)
                    {
                        command.CommandText += " AND E.dateeve > @dateeveOD";
                        command.Parameters.AddWithValue("@dateeveOD", filtr.Dateeve_OD);
                    }
                    else if (filtr.Dateeve_DO != null)
                    {
                        command.CommandText += " AND E.dateeve < @dateeveDO";
                        command.Parameters.AddWithValue("@dateeveDO", filtr.Dateeve_DO);
                    }
                }


                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant))
                {
                    if (!filtr.Dateeve_TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.Dateeve_TimeVariant.Split(';');
                         TimeFilters.TimeVariants TimeVar = ( TimeFilters.TimeVariants)Enum.Parse(typeof( TimeFilters.TimeVariants), arr[0], true);

                        command.CommandText += " AND E.dateeve > @dateeve_TV";
                        command.Parameters.AddWithValue("@dateeve_TV", TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                    }
                }

                if (!string.IsNullOrEmpty(filtr.MachineID))
                {
                    command.CommandText += " AND E.machineid = @machineid ";
                    command.Parameters.AddWithValue("@machineid", filtr.MachineID);
                }

                if (!string.IsNullOrEmpty(filtr.Description))
                {
                    command.CommandText += " AND CAST(E.[description] AS NVARCHAR(MAX)) like '" + filtr.Description + "%'";
                }

                if (!string.IsNullOrEmpty(filtr.status))
                {
                    command.CommandText += " AND E.status = @status ";
                    command.Parameters.AddWithValue("@status", filtr.status);
                }

                if (!string.IsNullOrEmpty(filtr.PackType))
                {
                    command.CommandText += " AND E.PackType = @PackType ";
                    command.Parameters.AddWithValue("@PackType", filtr.PackType);
                }

                if (!string.IsNullOrEmpty(filtr.Razeni_Column))
                {
                    command.CommandText += " order by " + "E." + filtr.Razeni_Column + " " + filtr.asc_desc;
                }
                else
                {
                    command.CommandText += " order by " + "E.dateeve desc"; ;
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_Events);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMaterials()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            DataTable dt = new DataTable();

            try
            {
                Globals.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                command.CommandText =
                    "SELECT material FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Events +
                    " WHERE IsProcessed is NULL " +
                    " Group by material "
                    ;

                adapter.SelectCommand = command;
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Metody pro APIRemoteLib z projektu sledovani vyroby


        public Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable GetFASKEventsRows(Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr)
            {
                //-----------------------------------------------START-------------------------------------------------------

                try
                {
                    Globals.LoadConfiguration();

                    string par = filtr.separator;
                    int pocetPruchoduMax = filtr.pocetPruchodu;

                    int pocetPruchodu = 0;

                    var list = par.Split(';').ToList();
                    Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_out = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();  // vystup
                    Fask.WEBAPI.API_BusinessObjects.FASK_Events fe_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();
                    Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_S0 = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();
                    Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT_S1 = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();

                    do
                    {
                        pocetPruchodu++;
                        Thread.Sleep(filtr.timeSleep);
                        if (pocetPruchodu > pocetPruchoduMax)
                            break;

                        DT_out = GetFASK_Events_ByStatus((Globals.Konfigurace.ConnectionString[0].FASKDB), filtr.machineid, filtr.status, list, filtr.NMBRPAL);


                        var c_0 = DT_out.Count();

                    } while (DT_out.Count == 0);


                    return DT_out;
                }
                catch (Exception)
                {

                    throw;
                }
            //----------------------------------------------END-----------------------------------------------------------

        }



        public bool StornoEvent( Fask.Interfaces.DataSets.Vyroba.FASK_EventsRow row)
        {
            SqlConnection con = null;
            SqlDataAdapter ada = null;
          
            try
            {
                Globals.LoadConfiguration();


                Guid NovyGUID = Guid.NewGuid();


                using (con = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                {
                    using (ada = new SqlDataAdapter())
                    {
                        using (var command = con.CreateCommand())
                        {

                            con.Open();

                            #region Insert

                            ada.InsertCommand = command;
                            ada.InsertCommand.Connection = con;

                            ada.InsertCommand.CommandText += " INSERT INTO [FASK_Events] " +
                                                            "(" +
                                                            " [loginid]" +
                                                            " ,[machineid]" +
                                                            " ,[dateeve]" +
                                                            " ,[qty]" +
                                                            " ,[qtyReal]" +
                                                            " ,[description]" +
                                                            " ,[barcodeReaded]" +
                                                            " ,[barcodeSended]" +
                                                            " ,[zakazka]" +
                                                            " ,[popis]" +
                                                            " ,[faskGUID]" +
                                                            " ,[reportType]" +
                                                            //" ,[isProcessed]" +
                                                            " ,[IDO]" +
                                                            " ,[scan1]" +
                                                            " ,[scan2]" +
                                                            " ,[scan3]" +
                                                            " ,[sensor]" +
                                                            " ,[material]" +
                                                            " ,[VPH]" +
                                                            " ,[VPPol]" +
                                                            " ,[EAN_IS]" +
                                                            " ,[IS_ID]" +
                                                            " ,[NMBRPAL]" +
                                                            " ,[status]" +
                                                            //" ,[productionGuid]" +
                                                            " ,[QTYPACK]" +
                                                            " ,[PackType]" +
                                                            " ,[WEIGHT]" +
                                                            " ) " +
                                                            " VALUES" +
                                                            " ( " +
                                                            " @loginid" +
                                                            " ,@machineid" +
                                                            " ,@dateeve" +
                                                            " ,@qty" +
                                                            " ,@qtyReal" +
                                                            " ,@description" +
                                                            " ,@barcodeReaded" +
                                                            " ,@barcodeSended" +
                                                            " ,@zakazka" +
                                                            " ,@popis" +
                                                            " ,@faskGUID" +
                                                            " ,@reportType" +
                                                            //" ,@isProcessed" +
                                                            " ,@IDO" +
                                                            " ,@scan1" +
                                                            " ,@scan2" +
                                                            " ,@scan3" +
                                                            " ,@sensor" +
                                                            " ,@material" +
                                                            " ,@VPH" +
                                                            " ,@VPPol" +
                                                            " ,@EAN_IS" +
                                                            " ,@IS_ID" +
                                                            " ,@NMBRPAL" +
                                                            " ,@status" +
                                                            // " ,@productionGuid" +
                                                            " ,@QTYPACK" +
                                                            " ,@PackType" +
                                                            " ,@WEIGHT" +
                                                            " )";


                            ada.InsertCommand.CommandType = System.Data.CommandType.Text;

                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@loginid", DbType = System.Data.DbType.Int32, Value = row.loginid });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@machineid", DbType = System.Data.DbType.String, Value = row.machineid });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@dateeve", DbType = System.Data.DbType.DateTime, Value = row.dateeve });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@qty", DbType = System.Data.DbType.Decimal, Value = row.qty });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@qtyReal", DbType = System.Data.DbType.Decimal, Value = row.qtyReal });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@description", DbType = System.Data.DbType.String, Value = row.IsdescriptionNull() ? null : row.description.Trim() });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@barcodeReaded", DbType = System.Data.DbType.String, Value = row.barcodeReaded });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@barcodeSended", DbType = System.Data.DbType.String, Value = row.barcodeSended });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@zakazka", DbType = System.Data.DbType.String, Value = row.IszakazkaNull() ? null : row.zakazka });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@popis", DbType = System.Data.DbType.String, Value = row.IspopisNull() ? null : row.popis });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@faskGUID", DbType = System.Data.DbType.Guid, Value = NovyGUID });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@reportType", DbType = System.Data.DbType.String, Value = row.reportType });
                            //ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@isProcessed", DbType = System.Data.DbType.DateTime, Value = row.IsisProcessedNull() ? null : (object)row.isProcessed });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@IDO", DbType = System.Data.DbType.String, Value = row.IsIDONull() ? null : row.IDO });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@scan1", DbType = System.Data.DbType.String, Value = row.Isscan1Null() ? null : row.scan1 });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@scan2", DbType = System.Data.DbType.String, Value = row.Isscan2Null() ? null : row.scan2 });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@scan3", DbType = System.Data.DbType.String, Value = row.Isscan3Null() ? null : row.scan3 });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@sensor", DbType = System.Data.DbType.String, Value = row.IssensorNull() ? null : row.sensor });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@material", DbType = System.Data.DbType.String, Value = row.IsmaterialNull() ? null : row.material });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@VPH", DbType = System.Data.DbType.String, Value = row.IsVPHNull() ? null : row.VPH });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@VPPol", DbType = System.Data.DbType.Int32, Value = row.IsVPPolNull() ? null : (object)row.VPPol });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@EAN_IS", DbType = System.Data.DbType.String, Value = row.IsEAN_ISNull() ? null : row.EAN_IS });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@IS_ID", DbType = System.Data.DbType.String, Value = row.IsIS_IDNull() ? null : row.IS_ID });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@NMBRPAL", DbType = System.Data.DbType.String, Value = row.IsNMBRPALNull() ? null : row.NMBRPAL });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@status", DbType = System.Data.DbType.Int32, Value = row.status });
                            //ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@productionGuid", DbType = System.Data.DbType.Guid, Value = row.IsproductionGuidNull() ? null : (object)row.productionGuid });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = row.IsQTYPACKNull() ? null : (object)row.QTYPACK });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@PackType", DbType = System.Data.DbType.String, Value = row.IsPackTypeNull() ? null : row.PackType });
                            ada.InsertCommand.Parameters.Add(new SqlParameter() { ParameterName = "@WEIGHT", DbType = System.Data.DbType.Decimal, Value = row.IsWEIGHTNull() ? null : (object)row.WEIGHT });

                            int returnValue_I = ada.InsertCommand.ExecuteNonQuery();

                            #endregion

                            if (returnValue_I == 1)
                            {

                                ada.SelectCommand = command;
                                ada.SelectCommand.Connection = con;
                                ada.SelectCommand.CommandText = "SELECT id FROM [FASK_Events] " +
                                    " WHERE  1 = 1" +
                                    " AND faskGUID = @faskGUID ";


                                ada.SelectCommand.Parameters.Clear();
                                ada.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "@faskGUID", DbType = System.Data.DbType.Guid, Value = NovyGUID });

                                ada.SelectCommand.CommandType = System.Data.CommandType.Text;
                                var id_O = ada.SelectCommand.ExecuteScalar();

                                int? ID = null;

                                if (id_O != null)
                                {
                                    try
                                    {
                                        ID = (int)id_O;
                                    }
                                    catch (Exception ex)
                                    {
                                        Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "ID FASK_Events nenalezeno!");
                                        Logging.ExceptionHandler2.Handle(ex);
                                        return false;
                                    }
                                }

                                ada.UpdateCommand = command;
                                ada.UpdateCommand.Connection = con;

                                ada.UpdateCommand.CommandText += " UPDATE [FASK_Events] " +
                                                                 " SET [IDO] = @IDO " +
                                                                 " ,[status] = 999 " +
                                                                 " WHERE 1 = 1 " +
                                                                 // AND [id] = @id_original " +
                                                                 " AND  faskGUID = @faskGUID";

                                ada.UpdateCommand.Parameters.Clear();
                                ada.UpdateCommand.Parameters.Add(new SqlParameter() { ParameterName = "@IDO", DbType = System.Data.DbType.String, Value = string.Format("SN:{0}", ID) });
                                //ada.UpdateCommand.Parameters.Add(new SqlParameter() { ParameterName = "@id_original", DbType = System.Data.DbType.Int32, Value = row.id });
                                ada.UpdateCommand.Parameters.Add(new SqlParameter() { ParameterName = "@faskGUID", DbType = System.Data.DbType.Guid, Value = row.faskGUID });

                                int returnValue_U = ada.UpdateCommand.ExecuteNonQuery();

                                if (returnValue_U == 1)
                                {
                                    return true;
                                }
                                else
                                {
                                    Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "Update do FASK_Events Neproveden!");
                                    return false;
                                }
                            }
                        }
                    }
                }


                return true;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        public int? StornoEvent_OnlineCheck(Guid G)
        {

            SqlConnection con = null;
            SqlCommand com = null;
            int? status_value = -1;

            try
            {
                Globals.LoadConfiguration();
                if (G != null)
                {
                    using (con = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                    {

                        using (com = con.CreateCommand())
                        {

                            con.Open();

                            com.Connection = con;
                            com.CommandType = CommandType.Text;
                            com.CommandText = "SELECT status FROM [FASK_Events] " +
                                " WHERE  1 = 1" +
                                " AND faskGUID = @faskGUID ";

                            com.Parameters.Add(new SqlParameter() { ParameterName = "@faskGUID", DbType = System.Data.DbType.Guid, Value = G });

                            com.CommandType = System.Data.CommandType.Text;
                            var xx = com.ExecuteScalar();

                            status_value = (int?)xx;

                        }
                    }
                }

                return status_value;


            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Transakce


        #region Výrobní příkazy

        #region VPH

        public void VPH_Fill(Fask.Interfaces.DataSets.Vyroba ds)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            ds.CZPRO_VPH.Clear();
            Fask.ModuleSql.Database.Vyroba_CZPRO_VPH.Fill_VPH(ConnectionString, ds);

        }

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable GetDataByCountEntriesSOPNUMBE(int CountEntries, string SOPNUMBE)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            return Fask.ModuleSql.Database.Vyroba_CZPRO_VPH.Get_VPH_ByCountEntriesSOPNUMBE(ConnectionString, CountEntries, SOPNUMBE);
        }

        public int Update( Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHDataTable dt)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            return Fask.ModuleSql.Database.Vyroba_CZPRO_VPH.Update(dt, ConnectionString);
        }

        public int Update_Row( Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow Row)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            return Fask.ModuleSql.Database.Vyroba_CZPRO_VPH.Update(Row, ConnectionString);
        }

        public void Insert( Fask.Interfaces.DataSets.Vyroba.CZPRO_VPHRow row)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            Fask.ModuleSql.Database.Vyroba_CZPRO_VPH.Insert_VPH(ConnectionString, row);
        }

        #region IVPH_GetFiltrovanyVPHList Members   

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVPHList(Fask.Interfaces.Filtry.Vyroba_VPH_Filtr filtr)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            return Database.Vyroba_CZPRO_VPH.Get_VPHByFilter(ConnectionString, filtr);
        }

        #endregion

        #endregion

        #region VPP


        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSOPNUMBEITEMNMBR(int CountEntries, string SOPNUMBE, string ITEMNMBR)
        {

           

            Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();

            try
            {
                Globals.LoadConfiguration();
                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

                using (var con = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT CountEntries, SOPNUMBE, ITEMNMBR, ITEMTYPE, ITEMDESC, ITEMMJ, VNDDOCNMP, VNDITNUM, ORD," +
                                " BarcodeP, LOCNCODE, QTYSHPPD, QTYDOKON, QTYPACK, QTYPACKMJ, TIMEMODE, TIMEPREP, TIMEUNIT, DtProdT, DtProdL," +
                                " SerNumT, SerNumL, VerT, VerL, TermID, LSTMod, DEX_ROW_ID, BarcodeT " +
                                " FROM CZPRO_VPP " +
                                " WHERE " +
                                " (CountEntries = " + CountEntries.ToString() + ") AND (SOPNUMBE = '" + SOPNUMBE + "') AND (ITEMNMBR = '" + ITEMNMBR + "')";


                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }

        }

        public Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable GetDataByCountEntriesSopnumbeItemnmbrBarcodeP(int CountEntries, string SOPNUMBE, string ITEMNMBR, string BarcodeP)
        {
            

            Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt = new Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable();
            try
            {
                Globals.LoadConfiguration();
                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

                using (var con = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT CountEntries, SOPNUMBE, ITEMNMBR, ITEMTYPE, ITEMDESC, ITEMMJ, VNDDOCNMP, VNDITNUM," +
                                " ORD, BarcodeP, LOCNCODE, QTYSHPPD, QTYDOKON, QTYPACK, QTYPACKMJ, TIMEMODE, TIMEPREP, TIMEUNIT, " +
                                " DtProdT, DtProdL, SerNumT, SerNumL, VerT, VerL, TermID, LSTMod, DEX_ROW_ID, BarcodeT " +
                                " FROM CZPRO_VPP WHERE" +
                                " (CountEntries = " + CountEntries.ToString() + ") AND (SOPNUMBE = '" + SOPNUMBE + "') AND (ITEMNMBR = '" + ITEMNMBR + "') AND (BarcodeP = '" + BarcodeP + "')";


                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }

        }

        public int FillByCountEntriesAndSOPNUMBE( Fask.Interfaces.DataSets.Vyroba ds, string TypeORDERBY, int CountEntries, string SOPNUMBE)
        {
            

            try
            {
                Globals.LoadConfiguration();
                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
                ds.CZPRO_VPP.Clear();

                using (var con = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT " +
                                " vpp.CountEntries, vpp.SOPNUMBE, vpp.ITEMNMBR, vpp.ITEMTYPE, vpp.ITEMDESC, vpp.ITEMMJ, vpp.VNDDOCNMP, vpp.VNDITNUM, vpp.ORD, vpp.BarcodeP," +
                                " vpp.LOCNCODE, vpp.QTYSHPPD, vpp.QTYDOKON, vpp.QTYPACK, vpp.QTYPACKMJ, vpp.TIMEMODE, vpp.TIMEPREP, vpp.TIMEUNIT, vpp.DtProdT, vpp.DtProdL, " +
                                " vpp.SerNumT, vpp.SerNumL, vpp.VerT, vpp.VerL, vpp.TermID, vpp.LSTMod, vpp.DEX_ROW_ID, vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) " +
                                " AS QTYODVEDENO, ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO " +
                                " , vpp.BarcodeT " +
                                " FROM CZPRO_VPP AS vpp LEFT OUTER JOIN " +
                                " (SELECT CountEntries, SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, COUNT(*) AS CNTODVEDENO " +
                                " FROM Production " +
                                " GROUP BY CountEntries, SOPNUMBE, ITEMNMBR) AS psum ON psum.CountEntries = vpp.CountEntries AND psum.SOPNUMBE = vpp.SOPNUMBE AND " +
                                " psum.ITEMNMBR = vpp.ITEMNMBR " +
                                " WHERE (vpp.CountEntries = '" + CountEntries.ToString() + "') AND (vpp.SOPNUMBE = '" + SOPNUMBE + "') ";
                            //" WHERE (vpp.CountEntries = @CountEntries) AND (vpp.SOPNUMBE = @SOPNUMBE) ";                                 

                            if (string.IsNullOrEmpty(TypeORDERBY))
                            {
                                select += " ORDER BY vpp.ITEMDESC ";
                            }
                            else
                            {
                                select += " ORDER BY vpp." + TypeORDERBY;
                            }

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(ds, ds.CZPRO_VPP.TableName);
                            return returnValue;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return -1;
            }
        }

        public void DeleteByCountEntriesSOPNUMBE(int CountEntries, string SOPNUMBE)
        {


            try
            {
                Globals.LoadConfiguration();
                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = con.CreateCommand())
                    {
                        con.Open();
                        comm.CommandType = System.Data.CommandType.Text;
                        comm.CommandText = "DELETE FROM CZPRO_VPP " +
                            " WHERE (CountEntries = " + CountEntries + ") AND (SOPNUMBE = '" + SOPNUMBE + "')";

                        int retunValue;
                        retunValue = comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        public int VPP_Insert(int CountEntries, string SOPNUMBE, string ITEMNMBR, string ITEMTYPE, string ITEMDESC, string ITEMMJ, string VNDDOCNMP, string VNDITNUM, int ORD, string BarcodeP, string LOCNCODE, decimal QTYSHPPD, decimal QTYDOKON, decimal QTYPACK, string QTYPACKMJ, int TIMEMODE, float TIMEPREP, float TIMEUNIT, byte DtProdT, short DtProdL, byte SerNumT, short SerNumL, byte VerT, short VerL, byte TermID, DateTime LSTMod, byte BarcodeT)
        {


            try
            {
                Globals.LoadConfiguration();
                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        using (var command = con.CreateCommand())
                        {

                            con.Open();
                            adapter.InsertCommand = command;
                            adapter.InsertCommand.Connection = con;
                            adapter.InsertCommand.CommandType = System.Data.CommandType.Text;

                            adapter.InsertCommand.CommandText = @"INSERT INTO [CZPRO_VPP] (" +
                                " [CountEntries], [SOPNUMBE], [ITEMNMBR], [ITEMTYPE], [ITEMDESC], " +
                                " [ITEMMJ], [VNDDOCNMP], [VNDITNUM], [ORD], [BarcodeP], " +
                                " [LOCNCODE], [QTYSHPPD], [QTYDOKON], [QTYPACK], [QTYPACKMJ], " +
                                " [TIMEMODE], [TIMEPREP], [TIMEUNIT], [DtProdT], [DtProdL], " +
                                " [SerNumT], [SerNumL], [VerT], [VerL], [TermID], " +
                                " [LSTMod], [BarcodeT]" +
                                " ) VALUES (" +
                                " @CountEntries, @SOPNUMBE, @ITEMNMBR, @ITEMTYPE, @ITEMDESC, " +
                                " @ITEMMJ, @VNDDOCNMP, @VNDITNUM, @ORD, @BarcodeP, " +
                                " @LOCNCODE, @QTYSHPPD, @QTYDOKON, @QTYPACK, @QTYPACKMJ, " +
                                " @TIMEMODE, @TIMEPREP, @TIMEUNIT, @DtProdT, @DtProdL, " +
                                " @SerNumT, @SerNumL, @VerT, @VerL, @TermID, " +
                                " @LSTMod, @BarcodeT" +
                                " ) ";

                            #region PARAMETRY

                            command.Parameters.Add(new SqlParameter() { ParameterName = "@CountEntries", DbType = System.Data.DbType.Int32, Value = CountEntries });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@SOPNUMBE", DbType = System.Data.DbType.String, Value = SOPNUMBE == null ? (object)DBNull.Value : SOPNUMBE });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMNMBR", DbType = System.Data.DbType.String, Value = ITEMNMBR == null ? (object)DBNull.Value : ITEMNMBR });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMTYPE", DbType = System.Data.DbType.String, Value = ITEMTYPE == null ? (object)DBNull.Value : ITEMTYPE });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMDESC", DbType = System.Data.DbType.String, Value = ITEMDESC == null ? (object)DBNull.Value : ITEMDESC });

                            command.Parameters.Add(new SqlParameter() { ParameterName = "@ITEMMJ", DbType = System.Data.DbType.String, Value = ITEMMJ == null ? (object)DBNull.Value : ITEMMJ });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDDOCNMP", DbType = System.Data.DbType.String, Value = VNDDOCNMP == null ? (object)DBNull.Value : VNDDOCNMP });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@VNDITNUM", DbType = System.Data.DbType.String, Value = VNDITNUM == null ? (object)DBNull.Value : VNDITNUM });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@ORD", DbType = System.Data.DbType.Int32, Value = ORD });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@BarcodeP", DbType = System.Data.DbType.String, Value = BarcodeP == null ? (object)DBNull.Value : BarcodeP });

                            command.Parameters.Add(new SqlParameter() { ParameterName = "@LOCNCODE", DbType = System.Data.DbType.String, Value = LOCNCODE == null ? (object)DBNull.Value : LOCNCODE });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYSHPPD", DbType = System.Data.DbType.Decimal, Value = QTYSHPPD });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYDOKON", DbType = System.Data.DbType.Decimal, Value = QTYDOKON });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACK", DbType = System.Data.DbType.Decimal, Value = QTYPACK });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@QTYPACKMJ", DbType = System.Data.DbType.String, Value = QTYPACKMJ == null ? (object)DBNull.Value : QTYPACKMJ });

                            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEMODE", DbType = System.Data.DbType.Int32, Value = TIMEMODE });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEPREP", DbType = System.Data.DbType.Single, Value = TIMEPREP });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@TIMEUNIT", DbType = System.Data.DbType.Single, Value = TIMEUNIT });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@DtProdT", DbType = System.Data.DbType.Byte, Value = DtProdT });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@DtProdL", DbType = System.Data.DbType.Int16, Value = DtProdL });

                            command.Parameters.Add(new SqlParameter() { ParameterName = "@SerNumT", DbType = System.Data.DbType.Byte, Value = SerNumT });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@SerNumL", DbType = System.Data.DbType.Int16, Value = SerNumL });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@VerT", DbType = System.Data.DbType.Byte, Value = VerT });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@VerL", DbType = System.Data.DbType.Int16, Value = VerL });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@TermID", DbType = System.Data.DbType.Int16, Value = TermID });

                            command.Parameters.Add(new SqlParameter() { ParameterName = "@LSTMod", DbType = System.Data.DbType.DateTime, Value = LSTMod });
                            command.Parameters.Add(new SqlParameter() { ParameterName = "@BarcodeT", DbType = System.Data.DbType.Byte, Value = BarcodeT });


                            #endregion

                            int returnValue = adapter.InsertCommand.ExecuteNonQuery();

                            return returnValue;
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

        public void VPP_Insert_Row( Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow row)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            Fask.ModuleSql.Database.Vyroba_CZPRO_VPP.Update( row, ConnectionString);
        }

        public void VPP_Update( Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt)
        {

            try
            {
                Globals.LoadConfiguration();
                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

                Fask.ModuleSql.Database.Vyroba_CZPRO_VPP.Update(dt, ConnectionString);
                //Update_CZPRO_VPP(dt);
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                throw;
            }
        }

        public void VPP_Fill( Fask.Interfaces.DataSets.Vyroba ds)
        {

            try
            {
                Globals.LoadConfiguration();
                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
                ds.CZPRO_VPP.Clear();

                using (var con = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT" +
                                " vpp.CountEntries, vpp.SOPNUMBE, vpp.ITEMNMBR, vpp.ITEMTYPE, vpp.ITEMDESC, " +
                                " vpp.ITEMMJ, vpp.VNDDOCNMP, vpp.VNDITNUM, vpp.ORD, vpp.BarcodeP, " +
                                " vpp.LOCNCODE, vpp.QTYSHPPD, vpp.QTYDOKON, vpp.QTYPACK, vpp.QTYPACKMJ, " +
                                " vpp.TIMEMODE, vpp.TIMEPREP, vpp.TIMEUNIT, vpp.DtProdT, vpp.DtProdL, " +
                                " vpp.SerNumT, vpp.SerNumL, vpp.VerT, vpp.VerL, vpp.TermID, " +
                                " vpp.LSTMod, vpp.DEX_ROW_ID, vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) AS QTYODVEDENO, ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO, " +
                                " vpp.BarcodeT " +
                                " FROM CZPRO_VPP AS vpp " +
                                " LEFT OUTER JOIN " +
                                " (SELECT CountEntries, SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, COUNT(*) AS CNTODVEDENO FROM Production GROUP BY CountEntries, SOPNUMBE, ITEMNMBR) AS psum " +
                                " ON psum.CountEntries = vpp.CountEntries " +
                                " AND psum.SOPNUMBE = vpp.SOPNUMBE " +
                                " AND psum.ITEMNMBR = vpp.ITEMNMBR";


                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(ds, ds.CZPRO_VPP.TableName);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        public void VPP_Update_Row( Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow row)
        {
            try
            {
                //row.SetModified();
                Globals.LoadConfiguration();
                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

                Fask.ModuleSql.Database.Vyroba_CZPRO_VPP.Update(row, ConnectionString);
                //Update_CZPRO_VPP(row);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                throw ex;
            }
        }

        #region IVPP_GetFiltrovanyVPHList Members   

        public Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVPPList(Fask.Interfaces.Filtry.Vyroba_VPP_Filtr filtr)
        {
            Fask.Interfaces.DataSets.Vyroba ds = new Interfaces.DataSets.Vyroba();
            try
            {
                Globals.LoadConfiguration();
                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

                ds.CZPRO_VPP.Clear();

                using (var con = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    using (var ada = new System.Data.SqlClient.SqlDataAdapter())
                    {
                        using (var com = con.CreateCommand())
                        {

                            var select = @"SELECT " +
                                " vpp.CountEntries, vpp.SOPNUMBE, vpp.ITEMNMBR, vpp.ITEMTYPE, vpp.ITEMDESC, vpp.ITEMMJ, vpp.VNDDOCNMP, vpp.VNDITNUM, vpp.ORD, vpp.BarcodeP," +
                                " vpp.LOCNCODE, vpp.QTYSHPPD, vpp.QTYDOKON, vpp.QTYPACK, vpp.QTYPACKMJ, vpp.TIMEMODE, vpp.TIMEPREP, vpp.TIMEUNIT, vpp.DtProdT, vpp.DtProdL, " +
                                " vpp.SerNumT, vpp.SerNumL, vpp.VerT, vpp.VerL, vpp.TermID, vpp.LSTMod, vpp.DEX_ROW_ID, vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) " +
                                " AS QTYODVEDENO, ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO " +
                                " , vpp.BarcodeT " +
                                " FROM " + Fask.SQL.Constants.Common.TABLE_CZPRO_VPP +
                                " AS vpp LEFT OUTER JOIN " +
                                " (SELECT CountEntries, SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, COUNT(*) AS CNTODVEDENO " +
                                " FROM Production " +
                                " GROUP BY CountEntries, SOPNUMBE, ITEMNMBR) AS psum ON psum.CountEntries = vpp.CountEntries AND psum.SOPNUMBE = vpp.SOPNUMBE AND " +
                                " psum.ITEMNMBR = vpp.ITEMNMBR " +
                                " WHERE  1 = 1";



                            if (string.IsNullOrEmpty(filtr.SOPNUMBE))
                            {
                                select += "AND (vpp.SOPNUMBE = '" + filtr.SOPNUMBE + "') ";
                            }

                            if (filtr.CountEntries.HasValue)
                            {
                                select += "AND (vpp.CountEntries = " + filtr.CountEntries.Value.ToString() + ") ";
                            }

                            if (string.IsNullOrEmpty(filtr.OrderBy))
                            {
                                select += " ORDER BY vpp.ITEMDESC ";
                            }
                            else
                            {
                                select += " ORDER BY vpp." + filtr.OrderBy;
                            }

                            ada.SelectCommand = com;
                            ada.SelectCommand.CommandText = select;
                            ada.SelectCommand.Connection = con;
                            ada.SelectCommand.CommandType = CommandType.Text;

                            int returnValue;
                            returnValue = ada.Fill(ds, ds.CZPRO_VPP.TableName);
                            return ds;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region Rozbory

        #region prehled vyrobky

        public void Groups_Fill( Fask.Interfaces.DataSets.Vyroba ds)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            Fask.ModuleSql.Database.Vyroba_Groups.Groups_Fill(ConnectionString, ds);
        }

        public Fask.Interfaces.DataSets.Vyroba.GroupsDataTable Groups_GetDataByID(string ID)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            return Fask.ModuleSql.Database.Vyroba_Groups.Groups_GetData(ConnectionString);
        }

        public void Groups_Update( Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dt)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            Fask.ModuleSql.Database.Vyroba_Groups.Update(dt, ConnectionString);
        }

        public void Groups_Insert(string id, string Name, string Description)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            Fask.ModuleSql.Database.Vyroba_Groups.Groups_Insert(ConnectionString, Name, Description);
        }

        public void Groups_Update_Row( Fask.Interfaces.DataSets.Vyroba.GroupsRow groupsRow)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            Fask.ModuleSql.Database.Vyroba_Groups.Update(groupsRow, ConnectionString);
        }

        public Fask.Interfaces.DataSets.Vyroba.MachinesDataTable Machines_GetDataByID(string ID)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            return Fask.ModuleSql.Database.Vyroba_Machines.Machines_GetDataByID(ConnectionString, ID);
        }

        public void Machines_Fill( Fask.Interfaces.DataSets.Vyroba ds)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            Fask.ModuleSql.Database.Vyroba_Machines.Machines_Fill(ConnectionString, ds);
        }

        public Fask.Interfaces.DataSets.Vyroba.OperationsDataTable Operations_GetDataByID(string ID)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            return Fask.ModuleSql.Database.Vyroba_Operations.Operations_GetDataByID(ConnectionString, ID);
        }

        public void Operations_Fill( Fask.Interfaces.DataSets.Vyroba ds)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            Fask.ModuleSql.Database.Vyroba_Operations.Operations_Fill(ConnectionString, ds);
        }

        public Fask.Interfaces.DataSets.Vyroba Production_GetFiltrovanyProductionList(Fask.Interfaces.Filtry.ProductionListFiltr filtr)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            return Fask.ModuleSql.Database.Vyroba_Production.GetFiltrovanyProductionList(ConnectionString, filtr);
        }

        public void Production_FillByCORRGUID( Fask.Interfaces.DataSets.Vyroba ds, Guid CORRGUID)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            Fask.ModuleSql.Database.Vyroba_Production.Production_FillByCORRGUID(ConnectionString, ds, CORRGUID);

        }

        public void Production_FillBySOUBEHGUID( Fask.Interfaces.DataSets.Vyroba ds, Guid SOUBEHGUID)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            Fask.ModuleSql.Database.Vyroba_Production.Production_FillBySOUBEHGUID(ConnectionString, ds, SOUBEHGUID);

        }

        public Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataByCORRGUID(Guid CORRGUID)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            return Fask.ModuleSql.Database.Vyroba_Production.Production_GetDataByCORRGUID(ConnectionString, CORRGUID);
        }

        public Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable Production_GetDataBySOUBEHGUID(Guid SOUBEHGUID)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            return Fask.ModuleSql.Database.Vyroba_Production.Production_GetDataBySOUBEHGUID(ConnectionString, SOUBEHGUID);
        }

        public void Production_Update( Fask.Interfaces.DataSets.Vyroba.Production_KonzolaDataTable dt)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            Fask.ModuleSql.Database.Vyroba_Production.Update(dt, ConnectionString);
        }

        public Fask.Interfaces.DataSets.Vyroba Production_GetFiltrovanyProductionVazby(Fask.Interfaces.Filtry.Vazby_P_PS_Filtr filtr)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            return Fask.ModuleSql.Database.Vyroba_Production.GetFiltrovanyProductionVazby(ConnectionString, filtr);
        }

        public Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable Corrects_GetDataByID(int ID)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            return Fask.ModuleSql.Database.Vyroba_Corrects.Corrects_GetDataByID(ConnectionString, ID);
        }

        public Fask.Interfaces.DataSets.Vyroba.VMachinesOperationsDataTable VMachinesOperations_GetDataByMachineIDoperationID(string MachinesID, string OperationsID)
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            return Fask.ModuleSql.Database.Vyroba_VMachinesOperations.GetDataByMachineIDoperationID(ConnectionString, MachinesID, OperationsID);
        }


        public Fask.Interfaces.DataSets.Vyroba EventsErr_GetFiltrovanyOdvodEvents(Fask.Interfaces.Filtry.Odvod_EventsErrListFiltr filtr)
        {

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                Globals.LoadConfiguration();
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                //command.CommandText =
                //    "SELECT * FROM " + Fask.Console.Interfaces.Constants.Fask.SQL.Constants.Common.TABLE_FASK_EventsErr +
                //    " WHERE " +
                //    " 1=1 "
                //    ;

                command.CommandText =
                        "SELECT E.* FROM " + Fask.SQL.Constants.Common.TABLE_FASK_EventsErr + " as E " +
                        " WHERE " +
                        " 1=1 ";


                if (filtr.productionGUID != null)
                {

                    //command.CommandText += "and CAST(productionGUID as uniqueidentifier) = CAST(@productionGUID as uniqueidentifier) ";
                    //command.Parameters.AddWithValue("@productionGUID", filtr.productionGUID);


                    command.CommandText += "and E.productionGUID = @productionGUID";
                    command.Parameters.AddWithValue("@productionGUID", filtr.productionGUID);

                    //command.CommandText += "and productionGUID ='" + filtr.productionGUID.ToString().Trim() + "' ";
                }


                if (filtr.Zpracovane && !filtr.NEZpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NOT NULL ";
                }
                else if (filtr.NEZpracovane && !filtr.Zpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NULL ";
                }
                else if (!filtr.NEZpracovane && !filtr.Zpracovane)
                {
                    command.CommandText += "and E.IsProcessed is NULL and E.IsProcessed is NOT NULL ";

                }




                // hledání podle datumu
                if (filtr.IsProcessed_OD != null && filtr.IsProcessed_DO != null)
                {
                    command.CommandText += " AND E.IsProcessed between @IsProcessedOD and @IsProcessedDO";
                    command.Parameters.AddWithValue("@IsProcessedOD", filtr.IsProcessed_OD);
                    command.Parameters.AddWithValue("@IsProcessedDO", filtr.IsProcessed_DO);
                }
                else
                {
                    if (filtr.IsProcessed_OD != null)
                    {
                        command.CommandText += " AND E.IsProcessed > @IsProcessedOD";
                        command.Parameters.AddWithValue("@IsProcessedOD", filtr.IsProcessed_OD);
                    }
                    else if (filtr.IsProcessed_DO != null)
                    {
                        command.CommandText += " AND E.IsProcessed < @IsProcessedDO";
                        command.Parameters.AddWithValue("@IsProcessedDO", filtr.IsProcessed_DO);
                    }
                }

                if (!string.IsNullOrEmpty(filtr.IsProcessed_TimeVariant))
                {
                    if (!filtr.IsProcessed_TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.IsProcessed_TimeVariant.Split(';');
                        TimeFilters.TimeVariants TimeVar = (TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);
                        command.CommandText += " AND E.IsProcessed > @IsProcessed_TV";
                        command.Parameters.AddWithValue("@IsProcessed_TV", TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                    }
                }

                if (filtr.Dateeve_OD != null && filtr.Dateeve_DO != null)
                {
                    command.CommandText += " AND E.dateeve between @dateeveOD and @dateeveDO";
                    command.Parameters.AddWithValue("@dateeveOD", filtr.Dateeve_OD);
                    command.Parameters.AddWithValue("@dateeveDO", filtr.Dateeve_DO);
                }
                else
                {
                    if (filtr.Dateeve_OD != null)
                    {
                        command.CommandText += " AND E.dateeve > @dateeveOD";
                        command.Parameters.AddWithValue("@dateeveOD", filtr.Dateeve_OD);
                    }
                    else if (filtr.Dateeve_DO != null)
                    {
                        command.CommandText += " AND E.dateeve < @dateeveDO";
                        command.Parameters.AddWithValue("@dateeveDO", filtr.Dateeve_DO);
                    }
                }


                if (!string.IsNullOrEmpty(filtr.Dateeve_TimeVariant))
                {
                    if (!filtr.Dateeve_TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.Dateeve_TimeVariant.Split(';');
                        TimeFilters.TimeVariants TimeVar = (TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);

                        command.CommandText += " AND E.dateeve > @dateeve_TV";
                        command.Parameters.AddWithValue("@dateeve_TV", TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                    }
                }

                if (!string.IsNullOrEmpty(filtr.MachineID))
                {
                    command.CommandText += " AND E.machineid = @machineid ";
                    command.Parameters.AddWithValue("@machineid", filtr.MachineID);
                }

                if (!string.IsNullOrEmpty(filtr.Description))
                {
                    command.CommandText += " AND CAST(E.[description] AS NVARCHAR(MAX)) like '" + filtr.Description + "%'";
                }

                if (!string.IsNullOrEmpty(filtr.status))
                {
                    command.CommandText += " AND E.status = @status ";
                    command.Parameters.AddWithValue("@status", filtr.status);
                }

                if (!string.IsNullOrEmpty(filtr.PackType))
                {
                    command.CommandText += " AND E.PackType = @PackType ";
                    command.Parameters.AddWithValue("@PackType", filtr.PackType);
                }

                if (!string.IsNullOrEmpty(filtr.Razeni_Column))
                {
                    command.CommandText += " order by " + "E." + filtr.Razeni_Column + " " + filtr.asc_desc;
                }
                else
                {
                    command.CommandText += " order by " + "E.dateeve desc";
                }

                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_EventsErr);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Vyroba IOdvod_MachineStateSet_GetFiltrovanyMachineStateSets.MachineStateSet_GetFiltrovanyOdvodMachineStateSet(Odvod_MachineStateSetListFiltr filtr)
        {

            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                //command.CommandText =
                //    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_EventsErr +
                //    " WHERE " +
                //    " 1=1 "
                //    ;

                //command.CommandText =
                //        "SELECT MD.Description, E.* FROM " + Fask.SQL.Constants.Common.TABLE_MachineStateSetHistory + " as E " +
                //        " left join " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition + " as MD on MD.IP=E.IP " +
                //        " WHERE " +
                //        " 1=1 ";


                if (filtr.zaznam)
                {
                    command.CommandText = "SELECT MD.ID_group, MD.Description, E.* FROM " + Fask.SQL.Constants.Common.TABLE_MachineStateSet + " as E " +
                                         " left join " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition + " as MD on MD.IP=E.IP " +
                                         " WHERE " +
                                         " 1=1 ";
                }
                else
                {
                    command.CommandText = "SELECT MD.ID_group, MD.Description, E.* FROM " + Fask.SQL.Constants.Common.TABLE_MachineStateSetHistory + " as E " +
                                            " left join " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition + " as MD on MD.IP=E.IP " +
                                            " WHERE " +
                                            " 1=1 ";
                }




                #region dohledani podle S0-S11
                for (int i = 0; i <= 11; i++)
                {
                    var property = typeof(Odvod_MachineStateSetListFiltr).GetProperty($"S{i}");
                    var value = property?.GetValue(filtr) as int?;

                    if (value.HasValue)
                    {
                        command.CommandText += $" AND E.S{i} = @S{i} ";
                        command.Parameters.AddWithValue($"@S{i}", value.Value);
                    }
                }

                #endregion



                if (filtr.DateModified_OD != null && filtr.DateModified_DO != null)
                {
                    command.CommandText += " AND E.DateModified between @DateModifiedOD and @DateModifiedDO";
                    command.Parameters.AddWithValue("@DateModifiedOD", filtr.DateModified_OD);
                    command.Parameters.AddWithValue("@DateModifiedDO", filtr.DateModified_DO);
                }
                else
                {
                    if (filtr.DateModified_OD != null)
                    {
                        command.CommandText += " AND E.DateModified > @DateModifiedOD";
                        command.Parameters.AddWithValue("@DateModifiedOD", filtr.DateModified_OD);
                    }
                    else if (filtr.DateModified_DO != null)
                    {
                        command.CommandText += " AND E.DateModified < @DateModifiedDO";
                        command.Parameters.AddWithValue("@DateModifiedDO", filtr.DateModified_DO);
                    }
                }



                if (!string.IsNullOrEmpty(filtr.AdamIP))
                {
                    command.CommandText += " AND E.IP = @IP ";
                    command.Parameters.AddWithValue("@IP", filtr.AdamIP);
                }

                if (!string.IsNullOrEmpty(filtr.TimeVariant))
                {
                    if (!filtr.TimeVariant.Contains("unknow"))
                    {

                        var arr = filtr.TimeVariant.Split(';');
                        TimeFilters.TimeVariants TimeVar = (TimeFilters.TimeVariants)Enum.Parse(typeof(TimeFilters.TimeVariants), arr[0], true);
                        command.CommandText += " AND E.DateModified > @DateModified_TV";
                        command.Parameters.AddWithValue("@DateModified_TV", TimeFilters.GetDateByFilter(DateTime.Now, TimeVar));
                    }
                }

                if (!string.IsNullOrEmpty(filtr.CisloSluzby))
                {
                    command.CommandText += " AND MD.ID_group in (" + filtr.CisloSluzby + ")";
                }

                if (!string.IsNullOrEmpty(filtr.Description))
                {
                    command.CommandText += " AND MD.Description in ('" + filtr.Description + "')";
                }


                //if (!string.IsNullOrEmpty(filtr.Razeni_Column))
                //{
                //    command.CommandText += " order by " + "E." + filtr.Razeni_Column + " " + filtr.asc_desc;
                //}
                //else
                //{
                //    command.CommandText += " order by " + "E.dateeve desc";
                //}
                command.CommandText += " order by " + "E.DateModified desc";

                adapter.SelectCommand = command;
                adapter.Fill(ds.MachineStateSet);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Vyroba IOdvod_TiskoveSablony_GetFiltrovanyTiskoveSablony.TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony(Odvod_TiskoveSablonyFiltr filtr)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataAdapter adapter = null;
            Fask.Interfaces.DataSets.Vyroba ds = new Fask.Interfaces.DataSets.Vyroba();

            try
            {
                adapter = new System.Data.SqlClient.SqlDataAdapter();
                connection = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                command = new System.Data.SqlClient.SqlCommand();
                command.Connection = connection;

                //command.CommandText =
                //    "SELECT * FROM " + Fask.SQL.Constants.Common.TABLE_FASK_EventsErr +
                //    " WHERE " +
                //    " 1=1 "
                //    ;

                command.CommandText =
                        "SELECT E.* FROM " + Fask.SQL.Constants.Common.TABLE_FASK_FORMULARE + " as E " +
                        " WHERE " +
                        " 1=1 ";

                //if (!string.IsNullOrEmpty(filtr.nazev_okna))
                //{
                //    command.CommandText += " AND E.nazev_okna = '@nazev_okna' ";
                //    command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                //}

                if (!string.IsNullOrEmpty(filtr.nazev_okna))
                {
                    command.CommandText += " AND E.nazev_okna = '" + filtr.nazev_okna + "' ";
                    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                if (!string.IsNullOrEmpty(filtr.loginid))
                {
                    command.CommandText += " AND E.loginid = '" + filtr.loginid + "' ";
                    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                if (!string.IsNullOrEmpty(filtr.machineid))
                {
                    command.CommandText += " AND E.machineid = '" + filtr.machineid + "' ";
                    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                if (!string.IsNullOrEmpty(filtr.typ))
                {
                    command.CommandText += " AND E.typ = '" + filtr.typ + "' ";
                    //command.Parameters.AddWithValue("@nazev_okna", filtr.nazev_okna);
                }

                command.CommandText += " order by " + "E.ord desc";


                adapter.SelectCommand = command;
                adapter.Fill(ds.FASK_FORMULARE);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #region Logs chyby z SV

        public bool Logs_Insert( Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrDataTable dt)
        {
                System.Data.SqlClient.SqlTransaction transaction = null;
                System.Data.SqlClient.SqlConnection conn = null;
                string _table_FASK_EventsErr = "FASK_EventsErr";
                Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrRow row = dt.First();

                try
                {

                    using (conn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
                    {

                        conn.Open();

                        transaction = conn.BeginTransaction();

                        using (var commandInsert = conn.CreateCommand())
                        {
                            //command a parametry definice

                            commandInsert.CommandText =
                                @" INSERT INTO " + _table_FASK_EventsErr +
                                " (loginid,machineid,dateeve,qty,qtyReal,description,barcodeReaded,barcodeSended,zakazka,popis,faskGUID,reportType,isProcessed,IDO,scan1,scan2,scan3,sensor, material, productionGuid, VPH, VPPol, EAN_IS, IS_ID, NMBRPAL, status, QTYPACK, PackType , WEIGHT , BarcodeT, REZ_1 , REZ_2 , REZ_3 , REZ_4 , REZ_5 )" +
                                " VALUES (@loginid,@machineid,@dateeve,@qty,@qtyReal,@description,@barcodeReaded,@barcodeSended,@zakazka,@popis,@faskGUID,@reportType,@isProcessed,@IDO,@scan1,@scan2,@scan3,@sensor, @material, @productionGuid, @VPH, @VPPol, @EAN_IS, @IS_ID, @NMBRPAL, @status, @QTYPACK, @PackType , @WEIGHT , @BarcodeT, @REZ_1 , @REZ_2 , @REZ_3 , @REZ_4 , @REZ_5)";

                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@loginid", DbType = DbType.String, SourceColumn = "loginid", Value = row.loginid });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@machineid", DbType = DbType.String, SourceColumn = "machineid", Value = row.machineid });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@dateeve", DbType = DbType.DateTime, SourceColumn = "dateeve", Value = row.dateeve });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@qty", DbType = DbType.Decimal, SourceColumn = "qty", Value = row.qty });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@qtyReal", DbType = DbType.Decimal, SourceColumn = "qtyReal", Value = row.qtyReal });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@description", DbType = DbType.String, SourceColumn = "description", Value = row.IsdescriptionNull() ? (object)DBNull.Value : row.description });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@barcodeReaded", DbType = DbType.String, SourceColumn = "barcodeReaded", Value = row.barcodeReaded });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@barcodeSended", DbType = DbType.String, SourceColumn = "barcodeSended", Value = row.barcodeSended });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@zakazka", DbType = DbType.String, SourceColumn = "zakazka", Value = row.IszakazkaNull() ? (object)DBNull.Value : row.zakazka });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@popis", DbType = DbType.String, SourceColumn = "popis", Value = row.IspopisNull() ? (object)DBNull.Value : row.popis });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@faskGUID", DbType = DbType.Guid, SourceColumn = "faskGUID", Value = row.faskGUID });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@reportType", DbType = DbType.String, SourceColumn = "reportType", Value = row.reportType });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@isProcessed", DbType = DbType.DateTime, SourceColumn = "isProcessed", Value = row.IsisProcessedNull() ? (object)DBNull.Value : row.isProcessed });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@IDO", DbType = DbType.String, SourceColumn = "IDO", Value = row.IsIDONull() ? (object)DBNull.Value : row.IDO });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@scan1", DbType = DbType.String, SourceColumn = "scan1", Value = row.Isscan1Null() ? (object)DBNull.Value : row.scan1 });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@scan2", DbType = DbType.String, SourceColumn = "scan2", Value = row.Isscan2Null() ? (object)DBNull.Value : row.scan2 });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@scan3", DbType = DbType.String, SourceColumn = "scan3", Value = row.Isscan3Null() ? (object)DBNull.Value : row.scan3 });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@sensor", DbType = DbType.String, SourceColumn = "sensor", Value = row.IssensorNull() ? (object)DBNull.Value : row.sensor });

                            //rozsireni struktur:
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@material", DbType = DbType.String, SourceColumn = "material", Value = row.IsmaterialNull() ? (object)DBNull.Value : row.material });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@productionGuid", DbType = DbType.Guid, SourceColumn = "productionGuid", Value = row.IsproductionGuidNull() ? (object)DBNull.Value : row.productionGuid });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@VPH", DbType = DbType.String, SourceColumn = "VPH", Value = row.IsVPHNull() ? (object)DBNull.Value : row.VPH });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@VPPol", DbType = DbType.Int32, SourceColumn = "VPPol", Value = row.IsVPPolNull() ? (object)DBNull.Value : row.VPPol });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@EAN_IS", DbType = DbType.String, SourceColumn = "EAN_IS", Value = row.IsEAN_ISNull() ? (object)DBNull.Value : row.EAN_IS });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@IS_ID", DbType = DbType.String, SourceColumn = "IS_ID", Value = row.IsIS_IDNull() ? (object)DBNull.Value : row.IS_ID });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@NMBRPAL", DbType = DbType.String, SourceColumn = "NMBRPAL", Value = row.IsNMBRPALNull() ? (object)DBNull.Value : row.NMBRPAL });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@status", DbType = DbType.Int32, SourceColumn = "status", Value = row.IsstatusNull() ? (object)DBNull.Value : row.status });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@QTYPACK", DbType = DbType.Decimal, SourceColumn = "QTYPACK", Value = row.IsQTYPACKNull() ? (object)DBNull.Value : row.QTYPACK });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@PackType", DbType = DbType.String, SourceColumn = "PackType", Value = row.IsPackTypeNull() ? (object)DBNull.Value : row.PackType });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@WEIGHT", DbType = DbType.Decimal, SourceColumn = "WEIGHT", Value = row.IsWEIGHTNull() ? (object)DBNull.Value : row.WEIGHT });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@BarcodeT", DbType = DbType.Byte, SourceColumn = "BarcodeT", Value = row.IsproductionGuidNull() ? (object)DBNull.Value : row.BarcodeT });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@REZ_1", DbType = DbType.String, SourceColumn = "REZ_1", Value = row.IsREZ_1Null() ? (object)DBNull.Value : row.REZ_1 });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@REZ_2", DbType = DbType.String, SourceColumn = "REZ_2", Value = row.IsREZ_2Null() ? (object)DBNull.Value : row.REZ_2 });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@REZ_3", DbType = DbType.String, SourceColumn = "REZ_3", Value = row.IsREZ_3Null() ? (object)DBNull.Value : row.REZ_3 });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@REZ_4", DbType = DbType.String, SourceColumn = "REZ_4", Value = row.IsREZ_4Null() ? (object)DBNull.Value : row.REZ_4 });
                            commandInsert.Parameters.Add(new SqlParameter()
                            { ParameterName = "@REZ_5", DbType = DbType.String, SourceColumn = "REZ_5", Value = row.IsREZ_5Null() ? (object)DBNull.Value : row.REZ_5 });
                            commandInsert.Transaction = transaction;
                            commandInsert.ExecuteNonQuery();
                        }

                        transaction.Commit();

                    }
                    return true;

                }
                catch (Exception ex)
                {
                    try
                    {
                        if (transaction != null)
                            transaction.Rollback();
                    }
                    catch (Exception exTransaction)
                    {
                        throw exTransaction;
                    }

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

        #endregion

        #endregion

      

        #endregion

        #endregion

        #region AGRO Sarze dotazy

        public string ReturnSarze( string smenaID, string userID, string linkaID)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                Globals.LoadConfiguration();
                sqlConn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                sqlComm.CommandText = "fask_vyroba_GetSarze";
                sqlComm.Parameters.AddWithValue("@smenaID", smenaID);
                sqlComm.Parameters.AddWithValue("@userID", userID);
                sqlComm.Parameters.AddWithValue("@linkaID", linkaID);

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
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                return null;
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }
            }
        }

        public bool ReturnID( string inID)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                Globals.LoadConfiguration();
                sqlConn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.Text;

                sqlComm.CommandText = "Select * from FASK_vyroba_OverId(@id)";
                sqlComm.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id", inID));
                sqlComm.Connection.Open();


                object o = sqlComm.ExecuteScalar();

                if (String.IsNullOrEmpty((string)o))
                    return false;
                else
                {
                    string tmp = (string)o;
                    if (tmp.Trim() == inID)
                        return true;
                    else
                        return false;
                }



            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }

            }
        }

        public bool ReturnHeslo( string inHESLO, string inID)
        {
            System.Data.SqlClient.SqlConnection sqlConn = null;
            System.Data.SqlClient.SqlCommand sqlComm = null;
            try
            {
                Globals.LoadConfiguration();
                sqlConn = new System.Data.SqlClient.SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB);
                sqlComm = new System.Data.SqlClient.SqlCommand();
                sqlComm.CommandTimeout = 1000;
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = System.Data.CommandType.Text;

                sqlComm.CommandText = "Select * from FASK_vyroba_OverHeslo(@id,@heslo)";
                sqlComm.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id", inID));
                sqlComm.Parameters.Add(new System.Data.SqlClient.SqlParameter("@heslo", inHESLO));
                sqlComm.Connection.Open();


                object o = sqlComm.ExecuteScalar();

                if (String.IsNullOrEmpty((string)o))
                    return false;
                else
                {
                    string tmp = (string)o;
                    if (tmp.Trim() == inHESLO)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);
                return false;
            }
            finally
            {
                if ((sqlConn != null) && ((sqlConn.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open))
                {
                    sqlConn.Close();
                }

            }
        }

        public void UpdateProductionRow(Guid? g, decimal? vaha)
        {
            System.Data.SqlClient.SqlTransaction transaction = null;
            System.Data.SqlClient.SqlConnection conn = null;
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
            try
            {

                using (conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {

                    conn.Open();

                    transaction = conn.BeginTransaction();

                    using (var commandInsert = conn.CreateCommand())
                    {
                        //command a parametry definice
                        commandInsert.CommandText =
                            @" UPDATE " + Fask.SQL.Constants.Common.TABLE_PRODUCTION +
                            " SET" +
                            " WEIGHT = @WEIGHT " +
                            " WHERE  GUID = @GUID  ";

                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@WEIGHT", DbType = DbType.Decimal, SourceColumn = "WEIGHT", Value = vaha });
                        commandInsert.Parameters.Add(new SqlParameter()
                        { ParameterName = "@GUID", DbType = DbType.Guid, SourceColumn = "GUID", Value = g });


                        commandInsert.Transaction = transaction;
                        commandInsert.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }

            }
            catch (Exception ex)
            {
                try
                {
                    if (transaction != null)
                        transaction.Rollback();
                }
                catch (Exception exTransaction)
                {
                    throw exTransaction;
                }

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

        public bool FASK_Events_row_11_2023_Insert(FASK_Events_row_11_2023 FE_object)
        {
            Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable DT = new Fask.Interfaces.DataSets.Vyroba.FASK_EventsDataTable();


            try
            {
                //osetrit vsechny stavy!!!
                var row = DT.NewFASK_EventsRow();

                //row.id = FE_object.id;

                if (string.IsNullOrEmpty(FE_object.loginid))
                    throw new InvalidOperationException("FE_object.loginid cannot be null/empty"); //row.loginid = "2019004";
                else
                    row.loginid = FE_object.loginid;

                //if (string.IsNullOrEmpty(FE_object.machineid))
                //    throw new InvalidOperationException("FE_object.machineid cannot be null/empty"); //row.machineid = "1";
                //else
                row.machineid = FE_object.machineid;

                if (FE_object.dateeve.HasValue)
                    row.dateeve = FE_object.dateeve.Value;
                else
                    row.dateeve = DateTime.Now;

                //if(FE_object.qty.Equals(null))
                //    throw new InvalidOperationException("FE_object.qty cannot be null");  //row.qty = FE_object.qty;
                //else
                row.qty = FE_object.qty.Value;

                //if (FE_object.qtyReal.Equals(null))
                //    throw new InvalidOperationException("FE_object.qtyReal cannot be null");  //row.qty = FE_object.qty;
                //else
                row.qtyReal = FE_object.qtyReal.Value;

                row.description = FE_object.description;

                //if (string.IsNullOrEmpty(FE_object.barcodeReaded))
                //    throw new InvalidOperationException("FE_object.barcodeReaded cannot be null");  //row.qty = FE_object.qty;
                //else
                row.barcodeReaded = FE_object.barcodeReaded;


                row.barcodeSended = FE_object.barcodeSended;
                row.zakazka = FE_object.zakazka;

                if (FE_object.faskGUID.HasValue)
                    row.faskGUID = FE_object.faskGUID.Value;
                else
                    row.faskGUID = Guid.NewGuid();

                row.reportType = FE_object.reportType;

                if (FE_object.isProcessed.HasValue)
                    row.isProcessed = FE_object.isProcessed.Value;
                else
                    row.SetisProcessedNull();

                row.IDO = FE_object.IDO;
                row.scan1 = FE_object.scan1;
                row.scan2 = FE_object.scan2;
                row.scan3 = FE_object.scan3;
                row.sensor = FE_object.sensor;
                row.material = FE_object.material;
                row.VPH = FE_object.VPH;

                if (FE_object.VPPol.HasValue)
                    row.VPPol = FE_object.VPPol.Value;
                else
                    row.SetVPPolNull();

                row.EAN_IS = FE_object.EAN_IS;
                row.IS_ID = FE_object.IS_ID;
                row.NMBRPAL = FE_object.NMBRPAL;

                if (FE_object.status.HasValue)
                    row.status = FE_object.status.Value;
                else
                    row.SetstatusNull();

                if (FE_object.productionGuid.HasValue)
                    row.productionGuid = FE_object.productionGuid.Value;
                else
                    row.SetproductionGuidNull();

                row.popis = FE_object.popis;
                row.QTYPACK = FE_object.QTYPACK.Value;
                row.PackType = FE_object.PackType;

                if (FE_object.WEIGHT.HasValue)
                    row.WEIGHT = FE_object.WEIGHT.Value;
                else
                    row.SetWEIGHTNull();


                DT.AddFASK_EventsRow(row);


                int pocetUlozeni = 0;

                pocetUlozeni = Fask.ModuleSql.Database.Vyroba_FaskEvents.Update(DT);

                string message_sent = String.Format("Pocet ulozenych zaznamů: {0}", pocetUlozeni);
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.PrintInfo, message_sent);

          


                if(pocetUlozeni>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Logging.ExceptionHandler2.Handle(ex);


                if (DT.HasErrors)
                {
                    Logging.ExceptionHandler2.Handle(DT);
                }

                throw ex;

            }
        }

        public List<string> LoadDescriptionsFromDatabase()
        {
            List<string> descriptions = new List<string>();

            try
            {

                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
                // Připojení k databázi
                //string connectionString = @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;User ID=fask_dbowner;Password=pro147fask";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    // Otevření spojení s databází
                    connection.Open();

                    // Dotaz na získání unikátních hodnot ze sloupce description
                    string query = "SELECT DISTINCT description FROM " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition;
                    SqlCommand command = new SqlCommand(query, connection);

                    // Vytvoření čteče pro zpracování výsledků dotazu
                    SqlDataReader reader = command.ExecuteReader();

                    // Přidání hodnot z databáze do seznamu
                    while (reader.Read())
                    {
                        string description = reader.GetString(0); // Index 0 odpovídá sloupci description
                        descriptions.Add(description);
                    }

                    // Uzavření čteče a spojení s databází
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return descriptions;
        }






        #endregion
        public int GetStatus(string ID)
        {
            int status = -1;
            try
            {

                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
                // Připojení k databázi
                //string connectionString = @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;User ID=fask_dbowner;Password=pro147fask";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    // Otevření spojení s databází
                    connection.Open();

                    // Dotaz na získání unikátních hodnot ze sloupce description
                    string query = "SELECT koeficient FROM " + Fask.SQL.Constants.Common.TABLE_FASK_Machines;
                    query += " WHERE id LIKE '" + ID.ToString() + "%'";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Vytvoření čteče pro zpracování výsledků dotazu
                    SqlDataReader reader = command.ExecuteReader();

                    // Přidání hodnot z databáze do seznamu
                    //while (reader.Read())
                    //{
                    //    string description = reader.GetString(0); // Index 0 odpovídá sloupci description
                    //    descriptions.Add(description);
                    //}
                    
                    while (reader.Read())
                    { //reader.GetValue
                        object status_string = reader.GetValue(0); // Index 0 odpovídá sloupci description
                        status = (int) decimal.Parse( status_string.ToString());
                    }

                   
                    // Uzavření čteče a spojení s databází
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return status;
        }

        public List<Tuple<string, string, bool>> GetZasobyTableInfo()
        {
            throw new NotImplementedException();
        }

        public string TiskoveSablony_GetFiltrovanyOdvodTiskoveSablony_Path(Odvod_TiskoveSablonyFiltr filtr)
        {
            throw new NotImplementedException();
        }

        public Vyroba.MachinesDefinitionMeasurementDataTable Konfigurace_Mericich_Zarizeni_00(string IP)
        {
            //-----------------------------------------------START-------------------------------------------------------

            try
            {
                Globals.LoadConfiguration();

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
                    using (conn = new SqlConnection(Globals.Konfigurace.ConnectionString[0].FASKDB))
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
                          " and IP = " + IP + " ;";


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

        public Server.Interfaces.Classes.StatusInfo Vyroba_GenerateDavka(Server.Interfaces.Classes.Objednavka objednavka, Server.Interfaces.Classes.Sklad sklad)
        {
            #region IVyroba_00 Members


            Globals.LoadConfiguration();
            Server.Interfaces.Classes.StatusInfo si = new Server.Interfaces.Classes.StatusInfo();
                si.Description = "Vyroba_GenerateDavka start";
                si.ID = 0;


                if (objednavka.ID == "prelokovani")
                {
                    //logika
                    Globals.LoadConfiguration();


                    string stav = Classes.Vyroba_00.Export_Prelokovani_SQL_Vyroba(objednavka, sklad);

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



            #endregion
        }

        public Vyroba MachineStateSet_GetFiltrovanyOdvodMachineStateSet_Analyza_Odvodu(Odvod_MachineStateSetListFiltr filtr)
        {
            throw new NotImplementedException();
        }

        public List<string> LoadStrojNameFromDatabase()
        {
            List<string> descriptions = new List<string>();

            try
            {

                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
                // Připojení k databázi
                //string connectionString = @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;User ID=fask_dbowner;Password=pro147fask";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    // Otevření spojení s databází
                    connection.Open();

                    // Dotaz na získání unikátních hodnot ze sloupce description
                    string query = "SELECT DISTINCT name FROM " + Fask.SQL.Constants.Common.TABLE_Machines;
                    SqlCommand command = new SqlCommand(query, connection);

                    // Vytvoření čteče pro zpracování výsledků dotazu
                    SqlDataReader reader = command.ExecuteReader();

                    // Přidání hodnot z databáze do seznamu
                    while (reader.Read())
                    {
                        string description = reader.GetString(0); // Index 0 odpovídá sloupci description
                        descriptions.Add(description);
                    }

                    // Uzavření čteče a spojení s databází
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return descriptions;
        }

        public List<string> LoadStrojDescriptionFromDatabase()
        {
            List<string> descriptions = new List<string>();

            try
            {

                string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;
                // Připojení k databázi
                //string connectionString = @"Data Source=192.168.1.121\SQLEXPRESS;Initial Catalog=Agro_fask;User ID=fask_dbowner;Password=pro147fask";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    // Otevření spojení s databází
                    connection.Open();

                    // Dotaz na získání unikátních hodnot ze sloupce description
                    string query = "SELECT DISTINCT description FROM " + Fask.SQL.Constants.Common.TABLE_MachinesDefinition;
                    SqlCommand command = new SqlCommand(query, connection);

                    // Vytvoření čteče pro zpracování výsledků dotazu
                    SqlDataReader reader = command.ExecuteReader();

                    // Přidání hodnot z databáze do seznamu
                    while (reader.Read())
                    {
                        string description = reader.GetString(0); // Index 0 odpovídá sloupci description
                        descriptions.Add(description);
                    }

                    // Uzavření čteče a spojení s databází
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return descriptions;
        }
    }

}