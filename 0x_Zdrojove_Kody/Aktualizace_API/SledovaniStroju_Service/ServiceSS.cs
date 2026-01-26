using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
//using Advantech.Adam;
using System.Threading;
using System.IO;
using FASK.Modules;
using RestSharp;
using System.Net;
using Fask.Logging;
using FASK.ADAM;
using FASK.Configurace;
using SledovaniStroju_Service;

namespace FASK
{
    public partial class ServiceSS : ServiceBase
    {
       public VyrobaSS vyrobaDS = new VyrobaSS();
       public List<Modules.ModulBase> adamsList = new List<Modules.ModulBase>();
       public System.Threading.Timer timer = null;
        private int cisloSLuzby;
        private int cisloAlgoritmu;

        public ServiceSS()
        {
            try
            {

                InitializeComponent();
                LoadConfiguration();

                cisloSLuzby = Config_trida.Config_ds.WEBAPI[0].cisloSluzby;
                cisloAlgoritmu = Config_trida.Config_ds.WEBAPI[0].cisloAlgoritmu;
                
                Fask.Logging.Log.Enable = true;
                Fask.Logging.Log.Write("Start Sluzby c'tor");
                Log.Write(string.Format("Cislo sluzby: {0}", cisloSLuzby));
                Log.Write(string.Format("Cislo algoritmu: {0}", cisloAlgoritmu));
                timer = new Timer(new TimerCallback(ProcessCommunication));
                
                this.CanShutdown = true;    // Service je upozorněna, když se systém vypíná
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        //private void TimerStop()
        //{
        //    timer.Change(Timeout.Infinite, Timeout.Infinite);
        //}

        //private void TimerStart()
        //{
        //    if (!serviceStopped)
        //        timer.Change(Properties.Settings.Default.TimerPeriod, Properties.Settings.Default.TimerPeriod);
        //}

        protected override void OnStart(string[] args)
        {
            try
            {
                Fask.Logging.Log.Write("ServiceSS start");
                // timer.Change(1000, 120000);
                #region odkomentovat po odladeni
                //odkomentovat---
                LoadConfiguration();

                cisloSLuzby = Config_trida.Config_ds.WEBAPI[0].cisloSluzby;
                cisloAlgoritmu = Config_trida.Config_ds.WEBAPI[0].cisloAlgoritmu;

                Log.Write(string.Format("Cislo sluzby: {0}", cisloSLuzby));
                Log.Write(string.Format("Cislo algoritmu: {0}", cisloAlgoritmu));

                if(cisloAlgoritmu == 1)
                {
                    Log.Write(string.Format("Cislo algoritmu: {0}, prace s country", cisloAlgoritmu));
                }
                else if (cisloAlgoritmu == 2)
                {
                    Log.Write(string.Format("Cislo algoritmu: {0}, prace s DI jako true/false", cisloAlgoritmu));
                }

                InitializeModules();

                InitializeP2P();
                #endregion


            }
            catch (Exception ex)
            {

                Log.Write(ex);
            }

            //Fask.Emailing.Email.SendErrorEmail = Properties.Settings.Default.SendErrorEmail;


            //StateWriter.Delete(); //smaze log pri spusteni ...             
            //TimerStart();
        }

        private void InitializeP2P()
        {
            try
            {

                foreach (ModulBase item in adamsList)
                {

                    if (item is ADAM_60XX)
                    {
                        ADAM_60XX ada = (ADAM_60XX)item;

                        //vypnuti P2P

                        ada.DataReady_NabeznaHrana -= SaveStates4; //vytvorit metodu na ukladani
                        ada.DataReady_NabeznaHrana += SaveStates4;

                        ada.DataReady_SestupnaHrana -= SaveStates4;
                        ada.DataReady_SestupnaHrana += SaveStates4;


                        Log.Write(string.Format("InitializeP2P--ADAM-START-IP: {0}", item.IP));
                        ada.Start();
                    }

                }


            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
            }
        }

        #region Nacteni konfigurace
        private void LoadConfiguration()
        {
            try
            {
                //Pokud nebyly nacteny zadne parametry, vlozim vychzoi (?)
                if (Configurace.Config_trida.ExistiFile())
                {

                    Configurace.Config_trida.Config_ds.WEBAPI.AddWEBAPIRow(
                    "MDox",
                    "192.168.1.69/MST_Win_Kom_Server_7_Dasenka", // "192.168.1.69/MST_Win_Kom_Server_7_Dasenka" // "10.11.10.62:8088" pri nasazeni do AGRO!!                   
                    "api",
                    false, //true pri nasazeni do AGRO!!
                    5000,
                    false,
                    true,
                    false,
                    1,
                    1
                    );

                    //Ulozeni
                    Configurace.Config_trida.Save();
                }

           


            }
            catch (Exception ex)
            {
                Fask.Logging.Log.Write(ex);
                // Log.Write(ex);
            }

        }
        #endregion

        /// <summary>
        /// Systém se vypíná.
        /// </summary>
        protected override void OnShutdown()
        {
            Fask.Logging.Log.Write("System shutdown");
        }

        private void InitializeModules()
        {
            try
            {
                // TODO : aktualizace???
                if (adamsList.Count > 0)
                    adamsList.Clear();

             
                //----------------------START komunikace se servrem--------------------------------------------

                VyrobaSS.MachinesDefinitionDataTable dt = new VyrobaSS.MachinesDefinitionDataTable();
                try
                {
                    //Exceptions.Handler.ErrorHandle(string.Format("IP:{0} >> Save to DB", this.ipAddress));

                    //this.getDIStatus();

                    // API komunikace
                    Fask.WEBAPI.API_BusinessObjects.MachinesDefinition_objekt o = new Fask.WEBAPI.API_BusinessObjects.MachinesDefinition_objekt();
                    IRestResponse restResponse;
                    string param = "Konfigurace_ADAM_data";
                    string JSON = "";

                    #region predavany objekt do API
                    o.ID_group = cisloSLuzby;


                    #endregion


                    //5.zapis do DB pres IIS
                    JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(o);

                    if (!Classes.WEBAPI.Comunication.Communicate(Classes.WEBAPI.Comunication.REST_Type.POST, out restResponse, param, JSON, "nakladka"))
                    {
                        throw new Exception("Komunikace s IIS AGRO se nezdařila");
                    }

                    if (restResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dt = Newtonsoft.Json.JsonConvert.DeserializeObject<VyrobaSS.MachinesDefinitionDataTable>(restResponse.Content);
                    }
                    else
                    {
                        throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
                    }

                }
                catch (Exception ex)
                {
                    //Log.Write(ex);
                    Log.Write(string.Format("CATCH--SaveStates4--Komunikace se servrem-{0}", ex));
                    return;
                }

                //----------------------END---------------------------------------------

                if (dt == null)
                {
                    throw new Exception("Chyba : nejsou data pro konfiguraci ADAM");
                }



                foreach (var item in dt)
                {
                    Modules.ModulBase modul = null;
                    switch (item.MType)
                    {
                        case Modules.ADAM6050.MType:
                            modul = new Modules.ADAM6050(item.IP);
                            break;
                        case ADAM_60XX.MType:
                            Log.Write(string.Format("Inicializace konfigurace ADAM--IP: {0} PORT: {1} název: {2}", item.IP, item.PORT.ToString(), item.Description));
                            modul = new ADAM_60XX(cisloAlgoritmu, item.IP, item.PORT,1025,350,100, Config_trida.Config_ds.WEBAPI[0].ADAM_communication_TCP, Config_trida.Config_ds.WEBAPI[0].ADAM_communication_P2P);
                            break;
                        default:
                            break;
                    }
                    if (modul != null)
                        adamsList.Add(modul);
                }

            }
            catch (Exception ex)
            {
                StateWriter.Write("InitializeModules Exception: " + ex.Message + "\n" + ex.StackTrace, true);
                Fask.Logging.Log.Write("InitializeModules Exception: " + ex.Message + "\n" + ex.StackTrace);
                //throw ex;
            }
        }

        /// <summary>
        /// zajisti, ze se timer uz nespusti ...
        /// </summary>
        private bool serviceStopped = false;
        protected override void OnStop()
        {
            Fask.Logging.Log.Write("ServiceSS stop");
            serviceStopped = true;
            //TimerStop();

            //StateWriter.Delete(); //smaze log pred provedenim akce ... 

            foreach (Modules.ModulBase a in adamsList)
            {
                a.Terminate();
            }

            adamsList.Clear();
        }

        public void ProcessCommunication(object state)
        {
            Fask.Logging.Log.Write("START-- ProcessCommunication");

            //LoadConfiguration();
            
            InitializeModules();

            InitializeP2P();
        }


        #region Ukladani do DB
        public void SaveStates4(ADAM.ADAM_60XX.AdamEventHandlerArgs e)
        {
            try
            {

                Fask.Logging.Log.Write("Pokus o ulozeni stavu do DB");
                //Exceptions.Handler.ErrorHandle(string.Format("IP:{0} >> Save to DB", this.ipAddress));

                //this.getDIStatus();

                // API komunikace
                Fask.WEBAPI.API_BusinessObjects.MachineStateSet o = new Fask.WEBAPI.API_BusinessObjects.MachineStateSet();
                IRestResponse restResponse;
                string param = "SledovaniVyroby_dataADAM_zapis";
                string JSON = "";





                //3. naplnit stav

                // errorLst = errorAct;        // TODO: Změna i při naběhnutí spojení s Adamem

                //4.aktualizovat
                o.IP = e.IP;
                o.DateModified = DateTime.Now;
                o.LastError = string.Empty;
                if (cisloAlgoritmu ==1)
                {
                    Log.Write("ulozeni stavu do DB / algoritmus 1");

                    if (e.DI != null && e.DI.Length >= 8)
                    {
                        o.S0 = Convert.ToInt32(e.DI[0]);
                        o.S1 = Convert.ToInt32(e.DI[1]);
                        o.S2 = Convert.ToInt32(e.DI[2]);
                        o.S3 = Convert.ToInt32(e.DI[3]);
                        o.S4 = Convert.ToInt32(e.DI[4]);
                        o.S5 = Convert.ToInt32(e.DI[5]);
                        o.S6 = Convert.ToInt32(e.DI[6]);
                        o.S7 = Convert.ToInt32(e.DI[7]);
                    }
                    else
                    {
                        Fask.Logging.Log.Write("ulozeni stavu do DB, DI pole selhani!!");
                    }
                    //o.S8 = Convert.ToInt32(e.DI[8]);
                    //o.S9 = Convert.ToInt32(e.DI[9]);
                    //o.S10 = Convert.ToInt32(e.DI[10]);
                    //o.S11 = Convert.ToInt32(e.DI[11]);
                    if (e.CounterValues != null && e.CounterValues.Length >= 4)
                    {
                        o.counter_8 = Convert.ToInt32(e.CounterValues[0]);
                        o.counter_9 = Convert.ToInt32(e.CounterValues[1]);
                        o.counter_10 = Convert.ToInt32(e.CounterValues[2]);
                        o.counter_11 = Convert.ToInt32(e.CounterValues[3]);
                    } 
                }
                else //algoritmus typu 2
                {
                    Log.Write("ulozeni stavu do DB / algoritmus 2");

                    if (e.DI != null && e.DI.Length >= 12)
                    {
                        o.S0 = Convert.ToInt32(e.DI[0]);
                        o.S1 = Convert.ToInt32(e.DI[1]);
                        o.S2 = Convert.ToInt32(e.DI[2]);
                        o.S3 = Convert.ToInt32(e.DI[3]);
                        o.S4 = Convert.ToInt32(e.DI[4]);
                        o.S5 = Convert.ToInt32(e.DI[5]);
                        o.S6 = Convert.ToInt32(e.DI[6]);
                        o.S7 = Convert.ToInt32(e.DI[7]);
                        o.S8 = Convert.ToInt32(e.DI[8]);
                        o.S9 = Convert.ToInt32(e.DI[9]);
                        o.S10 = Convert.ToInt32(e.DI[10]);
                        o.S11 = Convert.ToInt32(e.DI[11]);

                        o.counter_8 = 0;
                        o.counter_9 = 0;
                        o.counter_10 = 0;
                        o.counter_11 = 0;
                    }
                    else
                    {
                        Fask.Logging.Log.Write("ulozeni stavu do DB, DI pole selhani!!");
                    }
                }

                o.ID_group = cisloSLuzby;
       



                //5.zapis do DB pres IIS
                JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(o);

                if (!Classes.WEBAPI.Comunication.Communicate(Classes.WEBAPI.Comunication.REST_Type.POST, out restResponse, param, JSON, "nakladka"))
                {
                    throw new Exception("Komunikace s IIS AGRO se nezdařila");
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    //return "OK";
                }
                else
                {
                    throw new Exception("Chyba :'" + restResponse.StatusCode.ToString() + "'" + Environment.NewLine + restResponse.Content);
                }

                //_DI_H = e.DI;

                return;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return;
            }


        }
        #endregion

    }
}
