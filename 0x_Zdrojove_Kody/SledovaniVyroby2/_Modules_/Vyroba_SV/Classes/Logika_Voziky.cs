using System;
using System.Collections.Generic;
using FASK.SledovaniVyroby.Module.Vyroba_SV.Configuration;
using FASK.SledovaniVyroby.IRFIDProvider;
using Symbol.RFID3;
using FASK.SledovaniVyroby.ErrorLog;
using RestSharp;
using System.Net;
using Fask.Logging;
using System.Threading;
using Fask.WEBAPI;
using Fask.Constants;

namespace FASK.SledovaniVyroby.Module.Vyroba_SV.Classes
{
    public class Logika_Voziky
    {

        public string log_hlaska = string.Empty;


        //FASK.Palety_SSCC.SQLite.Classes.SSCC_Generator generatorSSCC = new Palety_SSCC.SQLite.Classes.SSCC_Generator();

        private AntennaInfo antennaInfo = new AntennaInfo();
        //IRFIDProvider.IRFIDProvider RFID_Linky_default = null;
        public List<Item_Tag> Tags = new List<Item_Tag>();
        //private int value_EPC = 0;
        private string EPC_ToWrite = string.Empty;

        public string Const_Zapis_Linky = "Zapis_Linky";
        public string Const_Zapis_Odvadeni = "Zapis_Odvadeni";
        public string Const_Kontrola_Linky = "Kontrola_Linky";
        public string Const_Kontrola_Odvadeni = "Kontrola_Odvadeni";


        private frmMain _parent;

        /// <summary>
        /// c'tor
        /// </summary>
        public Logika_Voziky(frmMain frm)
        {
            _parent = frm;
            vykladka_EventHandler += Logika_Voziky_vykladka_EventHandler;
            logs_EventHandler += Logika_Logs_EventHandler;
        }

        #region Event Logs zapis do DB

        public delegate void Logs_EventHandler(Object sender, Logs_EventArgs e);

        public event Logs_EventHandler logs_EventHandler;



        public void OnLogsEvent(Logs_EventArgs e)
        {
            Logs_EventHandler handler = logs_EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public class Logs_EventArgs : EventArgs
        {
            public int CisloLinky { get; set; }
            public string PopisAkce { get; set; }
            public int CisloAkce { get; set; }

            public int? LoginID { get; set; }

            public int? StatusLogs { get; set; }

            public Logs_EventArgs(int cl, int cisloAkce, string popis, int? login_ID = null, int? status_ID = null)
            {
                CisloLinky = cl;
                PopisAkce = popis;
                CisloAkce = cisloAkce;

                if (login_ID.HasValue)
                    LoginID = login_ID.Value;
                else
                    LoginID = null;

                if (status_ID.HasValue)
                    StatusLogs = status_ID.Value;
                else
                    StatusLogs = null;
            }
        }

        private void Logika_Logs_EventHandler(object sender, Logs_EventArgs e)
        {
            ExceptionHandler2.Handle("LOGS -- start online logu!", "Log_LV", "txt");

            int cisloStroje = e.CisloLinky;
            int cisloAkce = e.CisloAkce;
            string popisek = e.PopisAkce;
            object o = new object();
            Fask.WEBAPI.API_BusinessObjects.BO_Logs bo = new Fask.WEBAPI.API_BusinessObjects.BO_Logs();
            #region old code - nepotrebne
#if false
            FaskEventsDataSet.FASK_EventsErrDataTable dt = new FaskEventsDataSet.FASK_EventsErrDataTable();

#endif 
            #endregion
            ICommDatabase.DSVyroba.FASK_EventsErrDataTable dt = new ICommDatabase.DSVyroba.FASK_EventsErrDataTable();
            //FaskEventsDataSet.FASK_EventsRow row = null;


            try
            {
                #region typy akci
                var row = dt.NewFASK_EventsErrRow();


                #region row spolecna data

                int LoginID = _parent.LoginID;

                if (LoginID == -1)
                    row.loginid = e.LoginID.HasValue ? e.LoginID.Value.ToString() : "22";
                else
                    row.loginid = LoginID.ToString();

               //logika statusu: 0 kdyz vznikne zaznam a 200 pri deaktivaci
               if(e.StatusLogs.HasValue)
                {
                    row.status = e.StatusLogs.Value;
                }
                else
                {
                    row.status = AGRO.status_0;
                }


                row.machineid = cisloStroje.ToString();
                row.dateeve = DateTime.Now;
                row.qty = 0;
                row.qtyReal = 0;
                row.description = popisek;
                row.barcodeReaded = string.Empty;
                row.barcodeSended = string.Empty;
                row.faskGUID = Guid.NewGuid();
                row.reportType = "N";
                #endregion

                if (cisloAkce == 1)
                {
                    row.popis = "RFID";
                }
                else if(cisloAkce == 2)
                {
                    row.popis = "RFID";
                }
                else if (cisloAkce == 3)
                {
                    row.popis = "L1-L4";
                }
                else if (cisloAkce == 4)
                {
                    row.popis = "Timer";
                }
                else if (cisloAkce == 5)
                {
                    row.popis = "ZAZNAM";
                }
                else if (cisloAkce == 6)
                {
                    row.popis = "TISK";
                }
                else if (cisloAkce == 7)
                {
                    row.popis = "NAKLADKA";
                }
                else if (cisloAkce == 8)
                {
                    row.popis = "ADAM";
                }
                else if (cisloAkce == 9)
                {
                    row.popis = "OVERENI";
                }
                else
                {
                    //nedefinovana akce
                    ExceptionHandler2.Handle("LOGS -- metoda selhala, nedefinovaná akce!", "Log_LV", "txt");
                    return;
                }


                dt.AddFASK_EventsErrRow(row);

                #endregion

                Database.Classes.Vyroba_Remote.SV_Logs_insert(
                  Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                   int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                  dt);

                #region old code - nepotrebne

#if false
                #region transformace do BO
                BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Logs, Fask.WEBAPI.API_BusinessObjects.BO_Logs_row, FaskEventsDataSet.FASK_EventsErrDataTable, FaskEventsDataSet.FASK_EventsErrRow> y = new BusinessObject<Fask.WEBAPI.API_BusinessObjects.BO_Logs, Fask.WEBAPI.API_BusinessObjects.BO_Logs_row, FaskEventsDataSet.FASK_EventsErrDataTable, FaskEventsDataSet.FASK_EventsErrRow>();
                bo = y.GetBOFromDT(dt);

                #endregion

                IRestResponse restResponse;
                string param = "SV_Logs_insert";
                string JSON = "";


                JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(bo);

                if (!Classes.WEBAPI.Comunication.Communicate(Classes.WEBAPI.Comunication.REST_Type.POST, out restResponse, param, JSON))
                {
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace s IS AGRO se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    bool neco = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);
                    if (neco)
                    {
                        ExceptionHandler2.Handle("LOGS - odeslana data OK", "Log_LV", "txt");
                    }
                    else
                    {
                        ExceptionHandler2.Handle("LOGS - odeslana data ERROR", "Log_LV", "txt");
                    }
                }
                else
                {
                    Exception neco_2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Exception>(restResponse.Content);
                    ExceptionHandler2.Handle("LOGS - odeslana data ERROR", "Log_LV", "txt");
                } 
#endif 
                #endregion


            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle("LOGS -- metoda selhala, catch", "Log_LV", "txt");
                ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

        #region Event vykladky

        public delegate void Vykladka_EventHandler(Object sender, Vykladka_EventArgs e);

        public event Vykladka_EventHandler vykladka_EventHandler;



        public void OnVykladkaEvent(Vykladka_EventArgs e)
        {
            Vykladka_EventHandler handler = vykladka_EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }




        private void Logika_Voziky_vykladka_EventHandler(object sender, Vykladka_EventArgs e)
        {

            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "Logika_Voziky_vykladka_EventHandler");
            Fask.Tracing.Trac.Write("START metody Logika_Voziky_vykladka_EventHandler", tracId); 

            // TODO zde bude logika
            try
            {
                //promenna linky prichozi paletu
                int cisloLinky = -1;


                #region logika handshake L3 a L4
                try
                {
                    if (AgroSledovaniVozikuConfig.config.Vykladka[0].zapnuti_zalozni_logiky)
                    {

                        if (_parent.interniCisloLinka_matice == AGRO.rucni_vstup)
                        {
                            log_hlaska = string.Format("VYKLADKA--START- PALETA jede z RV!");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                        }
                        else if (_parent.interniCisloLinka_matice == 0)
                        {
                            log_hlaska = string.Format("VYKLADKA--START- PALETA jede z NEDOHLEDANO!");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                        }

                        if (e.CisloLinky == _parent.interniCisloLinka_matice)
                        {
                            log_hlaska = string.Format("VYKLADKA--START- linka z L3+L4 se shoduje s promennou NAKLADKY");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                        }

                        //nastavuji prichozi paletu z linky za pomoci promenne L2-L4.. 100 = linka RV, -00 = linka 1, -10 = linka 2, -11 = linka 3 
                        //cisloLinky = _parent.interniCisloLinka_matice; //*33
                    }
                    else
                    {
                        //vetev bez RFID
                        //nastavuji prichozi paletu z linky 
                        //cisloLinky = e.CisloLinky; //*33
                    }
                }
                catch (Exception ex)
                {

                    ExceptionHandler2.Handle(ex);
                    cisloLinky = e.CisloLinky;
                }
                #endregion

                //mazu interni promennou
                lock (_parent.interniCisloLinka_object)
                {
                    _parent.interniCisloLinka = 0;
                }

                //ay bude odladena logika bez RFID, tak zakomentovat nasledujici a odkomentovat radek s //*33
                //nastavuji prichozi paletu z linky 
                cisloLinky = e.CisloLinky;

                //overeni funkcnosti RFID pomoci blikajici LED 
                //if (_parent.RFID_cislo_linky == cisloLinky)
                //{
                //    _parent.validace_RFID_LED = true;
                //        //2= zelena barva
                //        _parent.Show_RFID_LED(2);
                //}
                //else
                //{
                //    _parent.validace_RFID_LED = false;
                //    //1= cervena barva
                //    _parent.Show_RFID_LED(1);
                //}

                //Log.Write(string.Format("VYKLADKA--START- Příjem z linky: {0}", cisloLinky));
                log_hlaska = string.Format("VYKLADKA--START- Příjem z linky: {0}", cisloLinky);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row objekt_Fe_data = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row objekt_Fe_data_falsak = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();

                //FASK_Events objekt_Fe_RV_data = new FASK_Events();

                Guid? G_value = null;

                //logika hledani zaznamu na dane lince
                int statusNakladka = AGRO.status_0;
                int statusVykladka = AGRO.status_21;
                bool zapis_OK = false;
                bool falesnyZaznam = false;

                statusNakladka = AGRO.status_0; statusVykladka = AGRO.status_21; zapis_OK = false; falesnyZaznam = false;

                #region Dohledání/Vytvoření záznamu nakládky

                Fask.Tracing.Trac.Write("START dohledání záznamu nakládky", tracId); 

                if (cisloLinky == AGRO.rucni_vstup || cisloLinky == AGRO.falsak)
                {
                    //objekt_Fe_data.id = 33;
                    objekt_Fe_data.loginid = "44";
                    if (cisloLinky == AGRO.rucni_vstup)
                    {
                        objekt_Fe_data.machineid = "4";
                        objekt_Fe_data.description = "Rucni vstup";
                        objekt_Fe_data.IS_ID = "A6666A";
                        objekt_Fe_data.EAN_IS = "8596666666664";
                    }
                    else
                    {
                        objekt_Fe_data.description = "Nedohledan vstup";
                        objekt_Fe_data.machineid = AGRO.falsak.ToString();
                        objekt_Fe_data.IS_ID = "A7777A";
                        objekt_Fe_data.EAN_IS = "8597777777774";
                    }

                    objekt_Fe_data.dateeve = DateTime.Now;
                    objekt_Fe_data.qty = -1;
                    objekt_Fe_data.qtyReal = 1;
                    objekt_Fe_data.barcodeReaded = "44444444444444"; //string.Empty;
                    objekt_Fe_data.barcodeSended = "44444444444444"; // string.Empty;
                    objekt_Fe_data.zakazka = string.Empty;
                    objekt_Fe_data.faskGUID = Guid.NewGuid();
                    objekt_Fe_data.reportType = string.Empty;
                    objekt_Fe_data.isProcessed = null; //
                    objekt_Fe_data.IDO = string.Empty;
                    objekt_Fe_data.scan1 = string.Empty;
                    objekt_Fe_data.scan2 = string.Empty;
                    objekt_Fe_data.scan3 = string.Empty;
                    objekt_Fe_data.sensor = string.Empty;
                    objekt_Fe_data.material = "444444444444"; // string.Empty;
                    objekt_Fe_data.VPH = string.Empty;
                    objekt_Fe_data.VPPol = 1;
                    objekt_Fe_data.status = AGRO.status_0;
                    objekt_Fe_data.productionGuid = Guid.NewGuid();
                    objekt_Fe_data.popis = string.Empty;
                    objekt_Fe_data.QTYPACK = 1;
                    objekt_Fe_data.PackType = "FP";
                    objekt_Fe_data.WEIGHT = 0;

                    //------------------------Generovat SSCC !!!!
                    try
                    {
                        FASK.Palety_SSCC.SQLite.Classes.SSCC_Generator generatorSSCC = new Palety_SSCC.SQLite.Classes.SSCC_Generator();
                        string sscc_FP_1 = generatorSSCC.Get_Next_SSCC(4, 1, 4); // TODO konfigurace ID stroje
                        objekt_Fe_data.NMBRPAL = sscc_FP_1;
                    }
                    catch (Exception ex)
                    {
                        objekt_Fe_data.NMBRPAL = "99999999999999999999";
                        ExceptionHandler2.Handle(ex);
                    }

#if DEBUG
                    log_hlaska = string.Format("VYKLADKA--Inicializace palety z RV --SSCC: {0}", objekt_Fe_data.NMBRPAL);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                }
                else
                {
                    objekt_Fe_data = GetSSCC_zaznam(cisloLinky, statusNakladka, statusVykladka);
                }

                Fask.Tracing.Trac.Write("END dohledání záznamu nakládky", tracId); 

                #endregion


                //logika zapisu zaznamu na streckovacku
                if (objekt_Fe_data != null && objekt_Fe_data.faskGUID.HasValue)
                {

                    Fask.Tracing.Trac.Write("START zápis záznamu vykládky", tracId); 

                    G_value = objekt_Fe_data.faskGUID;
                    //zapis do DB streckovacky, podle cisla strec bud status 21 nebo 22
                    if (e.CisloStrec == AGRO.bocedi_1)
                    {
                        //Log.Write(string.Format("VYKLADKA zaznam z linky: {0} SSCC: {1} Strec_1: {2}", cisloLinky, objekt_Fe_data.NMBRPAL, e.CisloStrec));
                        log_hlaska = string.Format("VYKLADKA zaznam z linky: {0} SSCC: {1} Strec_1: {2}", cisloLinky, objekt_Fe_data.NMBRPAL, e.CisloStrec);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        #region old code - nepotrebne
                        //zapis_OK = FaskEvents_WriteToRow_strec(objekt_Fe_data, AGRO.status_21); 
                        #endregion

                        zapis_OK = Database.Classes.Vyroba_Remote.FaskEvents_WriteToRow(
                         Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                         int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                         objekt_Fe_data,
                         Fask.Constants.AGRO.status_21,
                         "presun na streckovacku");

                        //TODO buffer zaznamu pro vykresleni vizualizace vykladka Bocedi 1
                        if (zapis_OK)
                        {
                            if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                            {
                                // list zaznamu o 3 prvcich
                                //pokud najdu vic nebo rovno 3 zaznamy, vezmu nejmladsi 3 zaznamy a ty postupne propisi do promennych *trida_vykladka
                                //zobrazim vizualizaci ctverecek 1 az 3 jsou vyplnene

                                //najdu data
                                Fask.WEBAPI.API_BusinessObjects.FASK_Events list_FE = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();
                                list_FE = Vizualizace_vykladka(AGRO.status_21);

                                //vykreslim data
                                if (list_FE != null)
                                {
                                    _parent.Vizualizace_vykladka_vykresli(list_FE, e.CisloStrec);
                                }
                                else
                                {
                                    //Log.Write(string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!"));
                                    log_hlaska = string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!");
                                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                                } 
                            }


                        }

                    }
                    else if (e.CisloStrec == AGRO.bocedi_2)
                    {

                        //Log.Write(string.Format("VYKLADKA zaznam z linky: {0} SSCC: {1} Strec_2: {2}", cisloLinky, objekt_Fe_data.NMBRPAL, e.CisloStrec));
                        log_hlaska = string.Format("VYKLADKA zaznam z linky: {0} SSCC: {1} Strec_2: {2}", cisloLinky, objekt_Fe_data.NMBRPAL, e.CisloStrec);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        #region old code - nepotrebne
                        //zapis_OK = FaskEvents_WriteToRow_strec(objekt_Fe_data, AGRO.status_22); 
                        #endregion

                        zapis_OK = Database.Classes.Vyroba_Remote.FaskEvents_WriteToRow(
                         Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                         int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                         objekt_Fe_data,
                         Fask.Constants.AGRO.status_22,
                         "presun na streckovacku");

                        //TODO buffer zaznamu pro vykresleni vizualizace vykladka Bocedi 2
                        if (zapis_OK)
                        {
                            if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                            {
                                // list zaznamu o 3 prvcich
                                //pokud najdu vic nebo rovno 3 zaznamy, vezmu nejmladsi 3 zaznamy a ty postupne propisi do promennych *trida_vykladka
                                //zobrazim vizualizaci ctverecek 1 az 3 jsou vyplnene

                                //najdu data
                                Fask.WEBAPI.API_BusinessObjects.FASK_Events list_FE = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();
                                list_FE = Vizualizace_vykladka(AGRO.status_22);

                                //vykreslim data
                                if (list_FE != null)
                                {
                                    _parent.Vizualizace_vykladka_vykresli(list_FE, e.CisloStrec);
                                }
                                else
                                {
                                    //Log.Write(string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!"));
                                    log_hlaska = string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!");
                                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                                } 
                            }


                        }

                    }


                    Fask.Tracing.Trac.Write("END zápis záznamu vykládky", tracId); 

                }
                else
                {

                    Fask.Tracing.Trac.Write("START logiky neznámé nakládky", tracId); 


                    int cisloLinky_FP = cisloLinky;
                    falesnyZaznam = true;
                    //Log.Write(string.Format("VYKLADKA neni zaznam z linky: {0} Strec_2: {1}", cisloLinky, e.CisloStrec));
                    log_hlaska = string.Format("VYKLADKA neni zaznam z linky: {0} Strec_2: {1}", cisloLinky, e.CisloStrec);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    #region zapis do ERR_logu, ktery jde videt v konzole
                    if (AgroSledovaniVozikuConfig.config.Vykladka[0].logovani_zaznam_0)
                    {
                        //zapis do ERR_logs online do databaze, videt to jde na konzole
                        int akce = 5;
                        string popisek = string.Format("VYKLADKA-ZAZNAM-stretch:{0}-chybi zaznam nakladky", e.CisloStrec == AGRO.bocedi_1 ? 1 : 2);
                        OnLogsEvent(new Logs_EventArgs(cisloLinky, akce, popisek, _parent.LoginID, AGRO.status_0));
                    }
                        
                    #endregion


                    //--------reakce na chybejici zaznam pri vykladce START--------------------------------------
                    if (cisloLinky == AGRO.linka_1 || cisloLinky == AGRO.linka_2 || cisloLinky == AGRO.linka_3 || cisloLinky == AGRO.rucni_vstup)
                    {

                        if (cisloLinky == AGRO.rucni_vstup)
                        {
                            cisloLinky = 4;
                            cisloLinky_FP = 4;
                        }

                        objekt_Fe_data_falsak.machineid = cisloLinky.ToString();
                    }
                    else
                    {
                        objekt_Fe_data_falsak.machineid = AGRO.falsak.ToString();
                        cisloLinky_FP = AGRO.falsak;
                    }

                    try
                    {
                        FASK.Palety_SSCC.SQLite.Classes.SSCC_Generator generatorSSCC = new Palety_SSCC.SQLite.Classes.SSCC_Generator();
                        string sscc_FP_2 = generatorSSCC.Get_Next_SSCC(cisloLinky_FP, 1, cisloLinky_FP); // TODO konfigurace ID stroje
                        objekt_Fe_data_falsak.NMBRPAL = sscc_FP_2;
                    }
                    catch (Exception ex)
                    {
                        objekt_Fe_data_falsak.NMBRPAL = "99999999999999999999";
                        ExceptionHandler2.Handle(ex);
                    }




                    objekt_Fe_data_falsak.loginid = "44";
                    //objekt_Fe_data.machineid = cisloLinky.ToString();
                    objekt_Fe_data_falsak.dateeve = DateTime.Now;
                    objekt_Fe_data_falsak.qty = -1;
                    objekt_Fe_data_falsak.qtyReal = 1;
                    objekt_Fe_data_falsak.description = "Falesny zaznam";
                    objekt_Fe_data_falsak.barcodeReaded = "44444444444444"; // string.Empty;
                    objekt_Fe_data_falsak.barcodeSended = "44444444444444"; // string.Empty;
                    objekt_Fe_data_falsak.zakazka = string.Empty;
                    objekt_Fe_data_falsak.faskGUID = Guid.NewGuid();
                    objekt_Fe_data_falsak.reportType = string.Empty;
                    objekt_Fe_data_falsak.isProcessed = null; //
                    objekt_Fe_data_falsak.IDO = string.Empty;
                    objekt_Fe_data_falsak.scan1 = string.Empty;
                    objekt_Fe_data_falsak.scan2 = string.Empty;
                    objekt_Fe_data_falsak.scan3 = string.Empty;
                    objekt_Fe_data_falsak.sensor = string.Empty;
                    objekt_Fe_data_falsak.material = "444444444444"; // string.Empty;
                    objekt_Fe_data_falsak.VPH = string.Empty;
                    objekt_Fe_data_falsak.VPPol = 1;
                    objekt_Fe_data_falsak.EAN_IS = "8597777777774";
                    objekt_Fe_data_falsak.IS_ID = "A7777A";

                    //------------------------Generovat SSCC !!!!
                    //objekt_Fe_data_falsak.NMBRPAL = "99999999999999999999";
                    //x.SetstatusNull();
                    objekt_Fe_data_falsak.status = AGRO.status_0;
                    objekt_Fe_data_falsak.productionGuid = Guid.NewGuid();
                    //objekt_Fe_data.productionGuid = null;
                    objekt_Fe_data_falsak.popis = string.Empty;
                    objekt_Fe_data_falsak.QTYPACK = 1;
                    objekt_Fe_data_falsak.PackType = "FP";
                    objekt_Fe_data_falsak.WEIGHT = 0;


                    //--------reakce na chybejici zaznam pri vykladce END----------------------------------------


                    G_value = objekt_Fe_data_falsak.faskGUID;
                    //zapis do DB streckovacky, podle cisla strec bud status 21 nebo 22
                    if (e.CisloStrec == AGRO.bocedi_1)
                    {
                        //Log.Write(string.Format("VYKLADKA falesny zaznam z linky: {0} SSCC: {1} Strec_1: {2}", cisloLinky, objekt_Fe_data_falsak.NMBRPAL, e.CisloStrec));
                        log_hlaska = string.Format("VYKLADKA falesny zaznam z linky: {0} SSCC: {1} Strec_1: {2}", cisloLinky, objekt_Fe_data_falsak.NMBRPAL, e.CisloStrec);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        #region old code - nepotrebne
                        //zapis_OK = FaskEvents_WriteToRow_strec(objekt_Fe_data_falsak, AGRO.status_21); 
                        #endregion

                        zapis_OK = Database.Classes.Vyroba_Remote.FaskEvents_WriteToRow(
                         Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                         int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                         objekt_Fe_data_falsak,
                         Fask.Constants.AGRO.status_21,
                         "presun na streckovacku");

                        //TODO buffer zaznamu pro vykresleni vizualizace vykladka Bocedi 1
                        if (zapis_OK)
                        {
                            // list zaznamu o 3 prvcich
                            //pokud najdu vic nebo rovno 3 zaznamy, vezmu nejmladsi 3 zaznamy a ty postupne propisi do promennych *trida_vykladka
                            //zobrazim vizualizaci ctverecek 1 az 3 jsou vyplnene

                            //najdu data
                            Fask.WEBAPI.API_BusinessObjects.FASK_Events list_FE = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();
                            list_FE = Vizualizace_vykladka(AGRO.status_21);

                            //vykreslim data
                            if (list_FE != null)
                            {
                                _parent.Vizualizace_vykladka_vykresli(list_FE, e.CisloStrec);
                            }
                            else
                            {
                                //Log.Write(string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!"));
                                log_hlaska = string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!");
                                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                            }


                        }

                    }
                    else if (e.CisloStrec == AGRO.bocedi_2)
                    {

                        //Log.Write(string.Format("VYKLADKA falesny zaznam z linky: {0} SSCC: {1} Strec_2: {2}", cisloLinky, objekt_Fe_data_falsak.NMBRPAL, e.CisloStrec));
                        log_hlaska = string.Format("VYKLADKA falesny zaznam z linky: {0} SSCC: {1} Strec_2: {2}", cisloLinky, objekt_Fe_data_falsak.NMBRPAL, e.CisloStrec);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        #region old code - nepotrebne
                        //zapis_OK = FaskEvents_WriteToRow_strec(objekt_Fe_data_falsak, AGRO.status_22); 
                        #endregion

                        zapis_OK = Database.Classes.Vyroba_Remote.FaskEvents_WriteToRow(
                         Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                         int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                         objekt_Fe_data_falsak,
                         Fask.Constants.AGRO.status_22,
                         "presun na streckovacku");

                        //TODO buffer zaznamu pro vykresleni vizualizace vykladka Bocedi 2
                        if (zapis_OK)
                        {
                            // list zaznamu o 3 prvcich
                            //pokud najdu vic nebo rovno 3 zaznamy, vezmu nejmladsi 3 zaznamy a ty postupne propisi do promennych *trida_vykladka
                            //zobrazim vizualizaci ctverecek 1 az 3 jsou vyplnene

                            //najdu data
                            Fask.WEBAPI.API_BusinessObjects.FASK_Events list_FE = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();
                            list_FE = Vizualizace_vykladka(AGRO.status_22);

                            //vykreslim data
                            if (list_FE != null)
                            {
                                _parent.Vizualizace_vykladka_vykresli(list_FE, e.CisloStrec);
                            }
                            else
                            {
                                //Log.Write(string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!"));
                                log_hlaska = string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!");
                                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                            }


                        }

                    }


                    Fask.Tracing.Trac.Write("END logiky neznámé nakládky", tracId); 

                }

                if (zapis_OK == true)
                {

                    Fask.Tracing.Trac.Write("START archivace nakládky", tracId); 

                    //Log.Write(string.Format("VYKLADKA zapis do DB --- CORRECT"));
                    log_hlaska = string.Format("VYKLADKA zapis do DB --- CORRECT");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    if (G_value != null)
                    {
                        if (!falesnyZaznam)
                        {
                            if (cisloLinky == AGRO.rucni_vstup)
                            {
                                //Log.Write(string.Format("VYKLADKA archivace --- Rucni vstup"));
                                log_hlaska = string.Format("VYKLADKA archivace --- Rucni vstup");
                                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                            }
                            else
                            {
                                //Log.Write(string.Format("VYKLADKA archivace GUID: {0}", G_value));
                                log_hlaska = string.Format("VYKLADKA archivace GUID: {0}", G_value);
                                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                                #region old code - nepotrebne
                               // FaskEvents_Update(G_value, AGRO.status_200); 
                                #endregion

                                Database.Classes.Vyroba_Remote.FaskEvents_Update(
                                   Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                   int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                   G_value,
                                   Fask.Constants.AGRO.status_200);
                                //Log.Write(string.Format("VYKLADKA archivace --- OK"));

                                ExceptionHandler2.Handle("VYKLADKA archivace --- OK", "Log_LV", "txt");
                            }
                        }
                        else
                        {
                            //Log.Write(string.Format("VYKLADKA archivace --- Falesny zaznam"));

                            ExceptionHandler2.Handle("VYKLADKA archivace --- Falesny zaznam", "Log_LV", "txt");
                        }


                    }
                    else
                    {
                        //Log.Write(string.Format("VYKLADKA archivace --- FALSE"));

                        //TODO - Mar ukazka logovani
                        //nazev souboru jako predavany parametr...dat 
                        ExceptionHandler2.Handle("VYKLADKA archivace --- FALSE", "Log_LV", "txt");
                    }


                    Fask.Tracing.Trac.Write("END archivace nakládky", tracId); 

                }
                else
                {
                    //Log.Write(string.Format("VYKLADKA zapis do DB --- FALSE"));
                    //ExceptionHandler2.Handle(LogLevel.Info, "VYKLADKA zapis do DB --- FALSE");
                    ExceptionHandler2.Handle("VYKLADKA zapis do DB --- FALSE", "Log_LV", "txt");
                }


            }
            catch (Exception ex)
            {
                //Log.Write(string.Format("CATCH--Logika_Voziky_vykladka_EventHandler: {0} ", ex));


                ExceptionHandler2.Handle(ex);

            }


            Fask.Tracing.Trac.Write("END metody Logika_Voziky_vykladka_EventHandler", tracId); 

        }

        public Fask.WEBAPI.API_BusinessObjects.FASK_Events Vizualizace_vykladka(int cisloStreckovacky)
        {

            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "Vizualizace_vykladka");
            Fask.Tracing.Trac.Write("START metody Vizualizace_vykladka", tracId); 


            Fask.WEBAPI.API_BusinessObjects.FASK_Events list_FE = null;
            Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();
            try
            {

                if (cisloStreckovacky == AGRO.status_21)
                {
                    filtr_Events.status = cisloStreckovacky;
                }
                else if (cisloStreckovacky == AGRO.status_22)
                {
                    filtr_Events.status = cisloStreckovacky;
                }

                //vyhledam zaznamy
                //dane zaznamy jsou naplnene v listu, ktery predam k vykresleni
                // API komunikace
                //FASK_Events o = new FASK_Events();


                IRestResponse restResponse;
                string param = "Vyroba_data_list_FE";
                string JSON = "";

                filtr_Events.separator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec;
                filtr_Events.pocetPruchodu = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchodu;
                filtr_Events.timeSleep = AgroSledovaniVozikuConfig.config.DB_Config[0].TimeSleep;


                JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(filtr_Events);

                if (!Classes.WEBAPI.Comunication.Communicate(WEBAPI.Comunication.REST_Type.POST, out restResponse, param, JSON, "nakladka"))
                {
                    //throw new Exception("Komunikace s IS AGRO se nezdařila");
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace s IS AGRO se nezdarila--" + restResponse.Content);
                }

                list_FE = Newtonsoft.Json.JsonConvert.DeserializeObject< Fask.WEBAPI.API_BusinessObjects.FASK_Events>(restResponse.Content);


                if (list_FE == null)
                {
                    //Log.Write("Vizualizace vykladka -- chybi zaznamy");
                    log_hlaska = string.Format("Vizualizace vykladka -- chybi zaznamy");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                }
                //vraci List ->


                Fask.Tracing.Trac.Write("END metody Vizualizace_vykladka", tracId); 
                return list_FE;
                
            }
            catch (Exception ex)
            {
                //Log.Write(string.Format("CATCH--Vizualizace_vykladka: {0} ", ex));
                ExceptionHandler2.Handle(ex);
                return null;
            }

            
        }
        #endregion

        #region SSCC - get number of SSCC

        /// <summary>
        /// This method return Fask_Events object.
        /// </summary>_
        /// <param name="MachineID">ID number of machine </param>
        /// <param name="Status">Number of status</param>
        public Fask.WEBAPI.API_BusinessObjects.FASK_Events_row GetSSCC_zaznam(int MachineID, int Status, int StatusNew)
        {

            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "GetSSCC_zaznam");
            Fask.Tracing.Trac.Write("START metody GetSSCC_zaznam", tracId);


            try
            {
                // nastavení proměnných ze konfiguračního souboru
                string separator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator;
                int pocetPruchodu = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchoduNakladka;
                int timeSleep = AgroSledovaniVozikuConfig.config.DB_Config[0].TimeSleep;
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row o = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();

                ICommDatabase.DSVyroba.FASK_EventsRow x = null;

                // vytvoření instance třídy Filtr_FASK_Events a nastavení jeho vlastností
                Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();
                filtr_Events.machineid = MachineID;
                filtr_Events.status = Status;
                filtr_Events.statusNew = StatusNew; //zmenit na promennou
                filtr_Events.separator = separator;
                filtr_Events.pocetPruchodu = pocetPruchodu;
                filtr_Events.timeSleep = timeSleep;

                // volání metody pro získání dat z databáze
                o = Database.Classes.Vyroba_Remote.SledovaniVyroby_data(
                Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                filtr_Events
                );

                Fask.Tracing.Trac.Write("END metody GetSSCC_zaznam", tracId);

                // vrácení dat z databáze
                return o;
            }
            catch (Exception ex)
            {
                // zpracování výjimky a zalogování chyby
                ExceptionHandler2.Handle(ex);
                return null;
            }


        }
        #endregion

        #region Hledani zaznamu ve FASK_Events

        /// <summary>
        /// This method return Fask_Events object.
        /// </summary>
        /// <param name="MachineID">ID number of machine </param>
        /// <param name="Status">Number of status</param>
        public Fask.WEBAPI.API_BusinessObjects.FASK_Events_row Najdi_zaznam(int Status, int StatusNew)
        {

            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "Najdi_zaznam");
            Fask.Tracing.Trac.Write("START metody Najdi_zaznam", tracId); 


            try
            {
                int localMachineID = 0;
                string TextSeparator = "";
                //string separator = "";
                int pocetPruchodu = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchodu;
                int timeSleep = AgroSledovaniVozikuConfig.config.DB_Config[0].TimeSleep;
                //string SSCC = string.Empty;
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row o = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();

                //bool SQL = AgroSledovaniVozikuConfig.config.WEBAPI[0].Enable;
                ICommDatabase.DSVyroba.FASK_EventsRow x = null;


                if (Status == AGRO.status_0)
                {
                    TextSeparator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator;
                }
                else if (Status == AGRO.status_1)
                {
                    TextSeparator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec;
                }
                else if (StatusNew == AGRO.status_41 || StatusNew == AGRO.status_42)
                {
                    if (_parent.uC_Vaha.Vaha_1_Connected() && StatusNew == AGRO.status_41 && !_parent.vaha_1_ERROR && Status != AGRO.status_21)
                    {
                        TextSeparator = AgroSledovaniVozikuConfig.config.DB_Config[0].TextSeparator_Tisk_Vaha;
                        pocetPruchodu = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchodu_Vaha;
                    }
                    else if (_parent.uC_Vaha.Vaha_2_Connected() && StatusNew == AGRO.status_42 && !_parent.vaha_1_ERROR && Status != AGRO.status_22)
                    {
                        TextSeparator = AgroSledovaniVozikuConfig.config.DB_Config[0].TextSeparator_Tisk_Vaha;
                        pocetPruchodu = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchodu_Vaha;
                    }
                    else
                    {
                        TextSeparator = AgroSledovaniVozikuConfig.config.DB_Config[0].TextSeparator_Tisk;
                        pocetPruchodu = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchoduTisk;
                    }
                    localMachineID = -1;
                }
                else if (Status == AGRO.status_41 || Status == AGRO.status_42)
                {
                    TextSeparator = AgroSledovaniVozikuConfig.config.DB_Config[0].TextSeparator_Vytisknuto;
                    pocetPruchodu = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchoduVytisknuto;

                    localMachineID = -1;
                }


                Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();
                filtr_Events.machineid = localMachineID;
                filtr_Events.status = Status;
                filtr_Events.statusNew = StatusNew; //zmenit na promennou
                filtr_Events.separator = TextSeparator;
                filtr_Events.pocetPruchodu = pocetPruchodu;
                filtr_Events.timeSleep = timeSleep;

                o = Database.Classes.Vyroba_Remote.SledovaniVyroby_data(
               Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
               filtr_Events
               );


                Fask.Tracing.Trac.Write("END metody Najdi_zaznam", tracId); 

                return o;
            }
            catch (Exception ex)
            {
                //Log.Write(string.Format("CATCH--Najdi_zaznam: {0} ", ex));
                ExceptionHandler2.Handle(ex);
                return null;

            }


        }
        #endregion

        #region RFID -- all logic

        #region test's method for write to EPC tag SSCC value
        /// <summary>
        /// This method write to EPC code default value.
        /// </summary>
        /// <param name="rFIDProvider">RFID provider </param>
        /// <param name="antena_ID">Number of ID antenna </param>
        public void RFID_test(IRFIDProvider.IRFIDProvider rFIDProvider, string EPC_source_value, List<ushort> ID_antenas, int? ID_Linka)
        {
            try
            {
                //string EPC_source_value = GetEPC(rFIDProvider, ID_antena);              
                //int value_max = 999999999;
                string SSCC_value = "";
                bool zapisOK = false;
                //FASK_Events objekt_Fe_data = new FASK_Events();
                //int statusNakladka = 0;
                //int statusVykladka = 21;
                //int cisloLinky = -1;

                if (!string.IsNullOrEmpty(EPC_source_value))
                {

                    if (ID_Linka != null)
                    {

                        // ----------------v pripade potreby odkomentovat a pote bude v TAGu SSCC----------
                        //-----------------SSCC v TAGu START------------------------------------
                        //---------------pozor na cas, muze odjet vozik!!-----------------------
                        //cisloLinky = (int)ID_Linka;
                        //objekt_Fe_data = GetSSCC_zaznam(cisloLinky, statusNakladka, statusVykladka);

                        //if (objekt_Fe_data != null)
                        //{
                        //    SSCC_value = ID_Linka.Value.ToString("00");
                        //    SSCC_value += objekt_Fe_data.NMBRPAL;
                        //    SSCC_value += "93";
                        //}
                        //else
                        //{
                        //    SSCC_value = ID_Linka.Value.ToString("00");
                        //    SSCC_value += "00" + "88888888888888888" + "0";
                        //    SSCC_value += "93";
                        //}
                        //-----------------SSCC v TAGu END------------------------------------

                        //-----------logika SSCC START NEW 7/12/2021---------------------
                        //----------v TAGu jen cislo linky!!-----------------------------
                        SSCC_value = ID_Linka.Value.ToString("00");
                        SSCC_value += "00" + "88888888888888888" + "0";
                        SSCC_value += "93";

                        //-----------logika SSCC END NEW 7/12/2021-------------------------
                        EPC_ToWrite = SSCC_value;

                    }

                    if (!string.IsNullOrEmpty(EPC_ToWrite))
                        zapisOK = rFIDProvider.Write_tags(EPC_source_value, EPC_ToWrite, ID_antenas);

                    if (!zapisOK)
                    {
                        if (EPC_source_value != "999999999999999999999999")
                            EPC_source_value = "999999999999999999999999";

                        SSCC_value = ID_Linka.Value.ToString("00");
                        SSCC_value += "00" + "88888888888888888" + "0";
                        SSCC_value += "93";


                        if (SSCC_value.Length != 24)
                            throw new Exception("Linka spatna delka SSCC");

                        EPC_ToWrite = SSCC_value;
                        zapisOK = rFIDProvider.Write_tags(EPC_source_value, EPC_ToWrite, ID_antenas);
                    }

                    if (zapisOK)
                    {
                        //string mess_OK = "CORRECT Zapis do tagu: Linka: " + ID_Linka.Value.ToString("00") + " EPC_new: " + EPC_ToWrite;
                        //Log.Write(mess_OK);
                        log_hlaska = string.Format("CORRECT Zapis do tagu: Linka: " + ID_Linka.Value.ToString("00") + " EPC_new: " + EPC_ToWrite);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }
                    else
                    {
                        //string mess_BAD = "ERROR zapis do tagu: Linka: " + ID_Linka.Value.ToString("00") + " EPC_new: " + EPC_ToWrite;
                        //Log.Write(mess_BAD);

                        log_hlaska = string.Format("ERROR zapis do tagu: Linka: " + ID_Linka.Value.ToString("00") + " EPC_new: " + EPC_ToWrite);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }
                }

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);

            }

        }

        /// <summary>
        /// This method write to EPC code default value.
        /// </summary>
        /// <param name="rFIDProvider">RFID provider </param>
        /// <param name="antena_ID">Number of ID antenna </param>
        public void RFID_vykladka(IRFIDProvider.IRFIDProvider rFIDProvider, string EPC_source_value, List<ushort> ID_antenas, int? ID_Linka)
        {
            try
            {
                string EPC_value = "999999999999999999999999";
                //FASK_Events o = new FASK_Events();
                //bool neniSSCC = true;
                bool test = false;
                int prichoziLinka = 0;
                string LinkaLocal;
                //int statusNakladka = 0;
                //int statusVykladka = 21;
                int CisloStrec = 0;

                if (EPC_source_value != null)
                {

                    if (test)
                    {
                        if (EPC_source_value.Length != 24)
                            throw new Exception("STREC spatna delka SSCC");

                        if (EPC_source_value.Substring(0, 2) != "00")
                            throw new Exception("STREC Nekorektné SSCC");

                        if (EPC_source_value.Substring(20, 2) != "93")
                            throw new Exception("STREC Nekorektné cislo linky");
                    }

                    //zjisteni z jake linky dojela paleta
                    LinkaLocal = EPC_source_value.Substring(0, 2);
                    prichoziLinka = Int32.Parse(LinkaLocal);
                    _parent.RFID_cislo_linky = prichoziLinka;


                    //Log.Write(" STR-Linka: " + prichoziLinka);
                    log_hlaska = string.Format(" STR-Linka: " + prichoziLinka);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    if (ID_Linka != null)
                    {
                        CisloStrec = (int)ID_Linka;
                    }
                    


                    //zkouska shody RFID a interniCisloLinka, pokud je neshoda zapise se do ERR logu, ktery jde videt v konzole
                    if(_parent.interniCisloLinka != prichoziLinka)
                    {
                        int stretch = -1;
                        if(CisloStrec == AGRO.bocedi_1)
                        {
                            stretch = 1;
                        }
                        else if(CisloStrec == AGRO.bocedi_2)
                        {
                            stretch = 2;
                        }

                        int akce = 1;
                        string popisek = string.Format("VYKLADKA-RFID-strec:{0}-nesouhlasi interni promenna s RFID TAG", stretch);
                        OnLogsEvent(new Logs_EventArgs(CisloStrec == 4 ? Fask.Constants.AGRO.bocedi_1 : Fask.Constants.AGRO.bocedi_2, akce, popisek, _parent.LoginID, AGRO.status_0));

                    }




                    if (_parent.interniCisloLinka != 0)
                    {
                        if (_parent.interniCisloLinka == _parent.interniCisloLinka_matice)
                        {
                            //100% bez chyby
                            prichoziLinka = _parent.interniCisloLinka;
                            
                        }
                        else
                        {
                            //logovat!!

                            if (prichoziLinka == _parent.interniCisloLinka)
                            {
                                //je to ok
                                //logovat!!
                            }
                            else
                            {
                                if (prichoziLinka == 99)
                                {
                                    prichoziLinka = _parent.interniCisloLinka;
                                }
                                else
                                {
                                    log_hlaska = string.Format("VYKLADKA-RFID-strec:{0}-L1-L4 chybi a RFID==99 !", CisloStrec == AGRO.bocedi_1 ? 1 : 2);
                                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                                    prichoziLinka = AGRO.falsak;
                                }
                            }
                        }
                    }
                    else
                    {
                        //TODO MAR dodelat zapis logu do DB
                        //int akce = 1;
                        //string popisek = string.Format("VYKLADKA-RFID-strec:{0}-interni promenna chybi nakladka", CisloStrec == AGRO.bocedi_1 ? 1 : 2);
                        //OnLogsEvent(new Logs_EventArgs(CisloStrec == 4 ? Fask.Constants.AGRO.bocedi_1 : Fask.Constants.AGRO.bocedi_2, akce, popisek, _parent.LoginID, AGRO.status_0));

                        int akce = 1;
                        string popisek = string.Format("VYKLADKA - prepnuto na zalozni system RFID");
                        OnLogsEvent(new Logs_EventArgs(CisloStrec == 4 ? Fask.Constants.AGRO.bocedi_1 : Fask.Constants.AGRO.bocedi_2, akce, popisek, _parent.LoginID, AGRO.status_0));


                        log_hlaska = string.Format("VYKLADKA-RFID-strec:{0}-interni promenna chybi nakladka", CisloStrec == AGRO.bocedi_1 ? 1 : 2);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


                        //overeni funkcnosti RFID pomoci blikajici LED 
                        if (_parent.validace_RFID_LED)
                        { 
                            if (prichoziLinka == 99)
                            {
                                int akce_rfid_0 = 1;
                                string popisek_selhani_RFID_0 = string.Format("VYKLADKA - zalozni system RFID nekorektni vykladka, TAG == 99");
                                OnLogsEvent(new Logs_EventArgs(CisloStrec == 4 ? Fask.Constants.AGRO.bocedi_1 : Fask.Constants.AGRO.bocedi_2, akce_rfid_0, popisek_selhani_RFID_0, _parent.LoginID, AGRO.status_0));


                                log_hlaska = string.Format("VYKLADKA-RFID-strec:{0}-TAG vycteno linka 99", CisloStrec == AGRO.bocedi_1 ? 1 : 2);
                                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                                
                                prichoziLinka = -1;

                            }
                            else
                            {
                                int akce_rfid_1 = 1;
                                string popisek_selhani_RFID_1 = string.Format("VYKLADKA - zalozni system RFID OK, cislo linky v TAG == {0}",prichoziLinka);
                                OnLogsEvent(new Logs_EventArgs(CisloStrec == 4 ? Fask.Constants.AGRO.bocedi_1 : Fask.Constants.AGRO.bocedi_2, akce_rfid_1, popisek_selhani_RFID_1, _parent.LoginID, AGRO.status_0));

                            }
                        }
                        else
                        {
                            int akce_rfid = 1;
                            string popisek_selhani_RFID = string.Format("VYKLADKA - zalozni system RFID selhal, nefunkcni TAG");
                            OnLogsEvent(new Logs_EventArgs(CisloStrec == 4 ? Fask.Constants.AGRO.bocedi_1 : Fask.Constants.AGRO.bocedi_2, akce_rfid, popisek_selhani_RFID, _parent.LoginID, AGRO.status_0));


                            log_hlaska = string.Format("VYKLADKA-RFID-strec:{0}-TAG nefunkcni", CisloStrec == AGRO.bocedi_1 ? 1 : 2);
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                            
                            prichoziLinka = -1;
                        }
                    }


                    //if (prichoziLinka == 99)
                    //{
                    //        if (_parent.interniCisloLinka_matice != 0)
                    //        {
                    //            prichoziLinka = _parent.interniCisloLinka_matice;
                    //        }
                    //        else
                    //        {
                    //            log_hlaska = string.Format("VYKLADKA-RFID-strec:{0}-TAG vycteno linka 99", CisloStrec == AGRO.bocedi_1 ? 1 : 2);
                    //            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    //            prichoziLinka = -1;
                    //        }
                    //}

             
                  

                    if (prichoziLinka != -1)
                    {
                        OnVykladkaEvent(new Vykladka_EventArgs(prichoziLinka, CisloStrec));
                    }
                    //else
                    //{
                    //    //mazani interni promenne linky:
                    //    lock (_parent.interniCisloLinka_object)
                    //    {
                    //        _parent.interniCisloLinka = 0;
                    //    }
                    //    int akce_rfid_10 = 1;
                    //    string popisek_selhani_RFID_10 = string.Format("VYKLADKA - neprosla, RFID udalost nastavuje interni promennou linky == 0");
                    //    OnLogsEvent(new Logs_EventArgs(CisloStrec == 4 ? Fask.Constants.AGRO.bocedi_1 : Fask.Constants.AGRO.bocedi_2, akce_rfid_10, popisek_selhani_RFID_10, _parent.LoginID, AGRO.status_0));

                    //}


                    EPC_ToWrite = EPC_value;

                    if (EPC_ToWrite != null)
                        rFIDProvider.Write_tags(EPC_source_value, EPC_ToWrite, ID_antenas);

                    //------------------------------------logika END-------------------------------------

                }

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);

            }

        }
        #endregion

        #region method for write to EPC tag 

        internal void StopReadRFID(IRFIDProvider.IRFIDProvider rFIDProvider, int? CisloLinky, string EPC, ushort IDAnteny, string source)
        {
            try
            {
                //metoda RFID anteny
                    _parent.RFID_anteny_barva(0, false);



                rFIDProvider.Stop_Read_tags();
                //string chybaHlaska ="TAG--EPC[" + EPC + "] " + source;
                //Log.Write(chybaHlaska);
                log_hlaska = string.Format("TAG--EPC[" + EPC + "] " + source);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


                if (source == Const_Zapis_Linky)
                {
                    _parent.SetText_ToUC(EPC, source);
                    RFID_test(rFIDProvider, EPC, new List<ushort> { IDAnteny }, CisloLinky);
                    //opravdu se propisuje hodnota do EPC????
                    StartRead(rFIDProvider, CisloLinky, new List<ushort> { IDAnteny }, Const_Kontrola_Linky);
                }
                else if (source == Const_Kontrola_Linky)
                {
                    if (RFID_Kontrola(EPC, IDAnteny, CisloLinky))
                    {
                        _parent.SetText_ToUC(EPC_ToWrite, source);
                        //zapis do DB status 1
                        //var z = FaskEvents_WriteToRow((int)CisloLinky, 0, 1, EPC_ToWrite);
                    }
                    else
                    {
                        try
                        {
                            int opakuj = 0;

                            while (opakuj <= 2)
                            {
                                rFIDProvider.Write_tags(EPC, EPC_ToWrite, new List<ushort> { IDAnteny });

                                opakuj++;
                            }
                        }
                        catch (Exception ex)
                        {

                          string  log_hlaska_RFID = string.Format("**RFID-KONTROLA-CHYBA**  TAG-- " + "zdroj EPC[" + EPC + "] " + "zapisována EPC[" + EPC_ToWrite + "] " + source);
                          ExceptionHandler2.Handle(log_hlaska_RFID, "Log_LV", "txt");
                          ExceptionHandler2.Handle(ex);

                        }
                    }
                }
                else if (source == Const_Zapis_Odvadeni)
                {
                    _parent.SetText_ToUC(EPC, source);
                    RFID_vykladka(rFIDProvider, EPC, new List<ushort> { IDAnteny }, CisloLinky);
                    StartRead(rFIDProvider, CisloLinky, new List<ushort> { IDAnteny }, Const_Kontrola_Odvadeni);
                }
                else if (source == Const_Kontrola_Odvadeni)
                {
                    //logika odvod na streckovacky
                    if (RFID_Kontrola(EPC, IDAnteny, CisloLinky))
                    {
                        _parent.SetText_ToUC(EPC_ToWrite, source);
                        //zapis do DB status 21 nebo 22
                        // var z = FaskEvents_WriteToRow((int)CisloLinky, 0, 1, EPC_ToWrite);
                    }
                    else
                    {
                        try
                        {
                            int opakuj = 0;

                            while (opakuj <= 2)
                            {
                                rFIDProvider.Write_tags(EPC, EPC_ToWrite, new List<ushort> { IDAnteny });

                                opakuj++;
                            }
                        }
                        catch (Exception ex)
                        {

                            string log_hlaska_RFID = string.Format("**RFID-KONTROLA**  TAG--EPC[" + EPC + "] " + source);
                            ExceptionHandler2.Handle(log_hlaska_RFID, "Log_LV", "txt");
                            ExceptionHandler2.Handle(ex);

                        }
                    }
                }



                Tags.Clear();
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);

            }
        }

        private bool RFID_Kontrola(string ePC, ushort iDAnteny, int? cisloLinky)
        {
            try
            {
                bool kontrola = false;

                //cislo stroje
                int machineID = 0;

               
                if (cisloLinky == Fask.Constants.AGRO.bocedi_1)
                {
                    kontrola = CheckEPC(ePC, EPC_ToWrite, "VYKLADKA-RFID-Strec:1", Fask.Constants.AGRO.bocedi_1);
                }
                else if (cisloLinky == Fask.Constants.AGRO.bocedi_2)
                {
                    kontrola = CheckEPC(ePC, EPC_ToWrite, "VYKLADKA-RFID-Strec:2", Fask.Constants.AGRO.bocedi_2);
                }
                else if (cisloLinky == Fask.Constants.AGRO.rucni_vstup)
                {
                    kontrola = CheckEPC(ePC, EPC_ToWrite, "NAKLADKA-RFID-Rucni vstup", Fask.Constants.AGRO.rucni_vstup);
                }
                else if (cisloLinky != null)
                {
                    if (cisloLinky == Fask.Constants.AGRO.linka_1)
                    {
                        machineID = Fask.Constants.AGRO.linka_1;
                    }
                    else if (cisloLinky == Fask.Constants.AGRO.linka_2)
                    {
                        machineID = Fask.Constants.AGRO.linka_2;
                    }
                    else if (cisloLinky == Fask.Constants.AGRO.linka_3)
                    {
                        machineID = Fask.Constants.AGRO.linka_3;
                    }

                    kontrola = CheckEPC(ePC, EPC_ToWrite, "NAKLADKA-RFID-Linka:" + cisloLinky, machineID);
                }
                else
                {

                    kontrola = CheckEPC(ePC, EPC_ToWrite, "NAKLADKA/VYKLADKA-RFID-Linka:" + cisloLinky, machineID);
                }


                return kontrola;
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;
            }

        }



        /// <summary>
        /// This method write to EPC code default value.
        /// </summary>
        /// <param name="rFIDProvider">RFID provider </param>
        /// <param name="antena_ID">Number of ID antenna </param>
        public void RFID_tag_zapis(IRFIDProvider.IRFIDProvider rFIDProvider, int ID_Linka)
        {
            try
            {
                if (ID_Linka == AGRO.linka_1)
                    StartRead(rFIDProvider, ID_Linka, new List<ushort> { (ushort)AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka1 }, Const_Zapis_Linky);

                if (ID_Linka == AGRO.linka_2)
                    StartRead(rFIDProvider, ID_Linka, new List<ushort> { (ushort)AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka2 }, Const_Zapis_Linky);

                if (ID_Linka == AGRO.linka_3)
                    StartRead(rFIDProvider, ID_Linka, new List<ushort> { (ushort)AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka3 }, Const_Zapis_Linky);

                if (ID_Linka == AGRO.bocedi_1)
                    StartRead(rFIDProvider, ID_Linka, new List<ushort> { (ushort)AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_strec1 }, Const_Zapis_Odvadeni);

                if (ID_Linka == AGRO.bocedi_2)
                    StartRead(rFIDProvider, ID_Linka, new List<ushort> { (ushort)AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_strec2 }, Const_Zapis_Odvadeni);

                if (ID_Linka == AGRO.rucni_vstup)
                    StartRead(rFIDProvider, ID_Linka, new List<ushort> { (ushort)AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_rucniVstup }, Const_Zapis_Linky);
            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH-RFID--RFID_tag_zapis: {0} ", ex));
                ExceptionHandler2.Handle(ex);
            }

        }

        private void StartRead(IRFIDProvider.IRFIDProvider rFIDProvider, int? iD_Linka, List<ushort> Anteny, string source)
        {
            try
            {

                //metoda rfid anteny
                if(iD_Linka.HasValue)
                _parent.RFID_anteny_barva(iD_Linka.Value, true);

                rFIDProvider.Start_Read_tags(Anteny, iD_Linka, source);
            }
            catch (Exception ex)
            {
                rFIDProvider.Stop_Read_tags();
                //Log.Write(string.Format("CATCH-RFID--StartRead: {0} ", ex));
                ExceptionHandler2.Handle(ex);
            }
        }

        private bool CheckEPC(string pom1, string pom2, string source, int machineID)
        {
            try
            {

                if (!string.IsNullOrEmpty(pom1) && !string.IsNullOrEmpty(pom2) && pom1 != pom2)
                {
                    if (_parent.validace_RFID_LED == false)
                    {
                        lock (_parent.validace_RFID_LED_object)
                        {
                            _parent.validace_RFID_LED = true;
                        }
                        //2= zelena barva
                        _parent.Show_RFID_LED(2);

                        //zapis do ERR logu o nefunkcnosti RFID 
                        int akce = 1;
                        string popisek = string.Format("RFID funkčnost TAG / zelena");
                        OnLogsEvent(new Logs_EventArgs(0, akce, popisek, _parent.LoginID, AGRO.status_0));
                    }


                    //Log.Write(source + " - Zapis do tagu uspesny: " + pom2);
                    ExceptionHandler2.Handle(source + " - Zapis do tagu uspesny: " + pom2, "Log_LV", "txt");

                    return true;
                }
                else
                {
                    if(_parent.validace_RFID_LED == true)
                    {
                        lock (_parent.validace_RFID_LED_object)
                        {
                            _parent.validace_RFID_LED = false;
                        }
                        //1= cervena barva
                        _parent.Show_RFID_LED(1);

                        //zapis do ERR logu o nefunkcnosti RFID 
                        int akce_rfid = 1;
                        string popisek_rfid = string.Format("RFID funkčnost TAG / cervena");
                        OnLogsEvent(new Logs_EventArgs(0, akce_rfid, popisek_rfid, _parent.LoginID, AGRO.status_0));
                    }
                    

                    //TODO MAR dodelat zapis logu do DB
                    int akce = 2;
                    string popisek = string.Format(source + " - Zapis do tagu byl neuspesny!!");
                    OnLogsEvent(new Logs_EventArgs(machineID, akce, popisek, _parent.LoginID, 0));

                    if (string.IsNullOrEmpty(pom1))
                    {
                        //Log.Write(source + " - Zapis do tagu byl neuspesny!! 1");
                        ExceptionHandler2.Handle(source + " - Zapis do tagu byl neuspesny!! 1", "Log_LV", "txt");
                        return false;
                    }
                    else if (string.IsNullOrEmpty(pom2))
                    {
                        //Log.Write(source + " - Zapis do tagu byl neuspesny!! 2");
                        ExceptionHandler2.Handle(source + " - Zapis do tagu byl neuspesny!! 2", "Log_LV", "txt");
                        return false;
                    }
                    else
                    {
                        //Log.Write(source + " - Zapis do tagu byl neuspesny!! 3");
                        ExceptionHandler2.Handle(source + " - Zapis do tagu byl neuspesny!! 3", "Log_LV", "txt");
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {

                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }
        #endregion

        #endregion

        #region Vykladka - TISK

        public void TiskPrijem(int cisloStatus)
        {

            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "TiskPrijem");
            Fask.Tracing.Trac.Write("START metody TiskPrijem", tracId); 


            try
            {
                int cislo_bocedi = 0;
                if (cisloStatus == AGRO.status_21)
                {
                    cislo_bocedi = AGRO.bocedi_1;
                }
                else if (cisloStatus == AGRO.status_22)
                {
                    cislo_bocedi = AGRO.bocedi_2;
                }

                bool zapis_OK = false;
                Guid? G_value = null;
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                //FASK_Events FE_objekt_kopie = new FASK_Events();
                //na zaklade cisloStatus = 21 or 22 vyhledam zaznam
                //------------hledam status = 21 a nema status = 41
                //---------------vratim nejstarsi zaznam-----------
                if (cisloStatus == AGRO.status_21)
                {
                    FE_objekt = Najdi_zaznam(cisloStatus, AGRO.status_41);
#if DEBUG
                    //Log.Write(string.Format("TISK STR_1 status: {0}", cisloStatus));
                    log_hlaska = string.Format("TISK STR_1 status: {0}", cisloStatus);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                }
                else if (cisloStatus == AGRO.status_22)
                {
                    FE_objekt = Najdi_zaznam(cisloStatus, AGRO.status_42);
#if DEBUG
                    //Log.Write(string.Format("TISK STR_2 status: {0}", cisloStatus));
                    log_hlaska = string.Format("TISK STR_2 status: {0}", cisloStatus);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                }

                //pridam hodnoty do objektu pro tisk
                //vyhledany zaznam odeslu na tisk
                if (FE_objekt != null && FE_objekt.faskGUID.HasValue)
                {
                    // OnTISKEvent(new TISK_EventArgs(FE_objekt_kopie));

                    G_value = FE_objekt.faskGUID;
#if DEBUG
                    //Log.Write(string.Format("TISK hledany zaznam SSCC: {0}", FE_objekt.NMBRPAL));
                    log_hlaska = string.Format("TISK hledany zaznam SSCC: {0}", FE_objekt.NMBRPAL);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    //TiskPosliData(FE_objekt);

                    //zmena status na 41 or 42
#if DEBUG
                    //Log.Write(string.Format("TISK zmena status SSCC: {0} status: {1}", FE_objekt.NMBRPAL, FE_objekt.status));
                    log_hlaska = string.Format("TISK zmena status SSCC: {0} status: {1}", FE_objekt.NMBRPAL, FE_objekt.status);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    if (cisloStatus == AGRO.status_21)
                    {
                        FE_objekt.reportType = "A";
                        zapis_OK = FaskEvents_WriteToRow_tisk(FE_objekt, AGRO.status_41);
                    }
                    else if (cisloStatus == AGRO.status_22)
                    {
                        FE_objekt.reportType = "A";
                        zapis_OK = FaskEvents_WriteToRow_tisk(FE_objekt, AGRO.status_42);
                    }

                    TiskPosliData(FE_objekt, cislo_bocedi);

                }
                else
                {
#if DEBUG
                    //Log.Write(string.Format("TISK neni hledany zaznam !ERROR!"));
                    log_hlaska = string.Format("TISK neni hledany zaznam !ERROR!");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                }



                if (zapis_OK)
                {
#if DEBUG
                    //Log.Write(string.Format("TISK zmena status --- TRUE"));
                    log_hlaska = string.Format("TISK zmena status --- TRUE");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    if (G_value != null)
                    {
#if DEBUG
                        //Log.Write(string.Format("TISK archivace GUID: {0}", G_value));
                        log_hlaska = string.Format("TISK archivace GUID: {0}", G_value);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                        if (cisloStatus == AGRO.status_21)
                        {
                            #region old code - nepotrebne
                            // FaskEvents_Update(G_value, AGRO.status_221); 
                            #endregion
                            Database.Classes.Vyroba_Remote.FaskEvents_Update(
                                   Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                   int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                   G_value,
                                   Fask.Constants.AGRO.status_221);
#if DEBUG
                            //Log.Write(string.Format("TISK archivace --- OK"));
                            log_hlaska = string.Format("TISK archivace --- OK");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                        }
                        else if (cisloStatus == AGRO.status_22)
                        {
                            #region old code - nepotrebne
                            //FaskEvents_Update(G_value, AGRO.status_222); 
                            #endregion

                            Database.Classes.Vyroba_Remote.FaskEvents_Update(
                                   Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                   int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                   G_value,
                                   Fask.Constants.AGRO.status_222);
#if DEBUG
                            //Log.Write(string.Format("TISK archivace --- OK"));
                            log_hlaska = string.Format("TISK archivace --- OK");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                        }
                    }
                    else
                    {
#if DEBUG
                        //Log.Write(string.Format("TISK archivace --- FALSE"));
                        log_hlaska = string.Format("TISK archivace --- FALSE");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    }
                }
                else
                {
#if DEBUG
                    //Log.Write(string.Format("TISK zmena status --- FALSE"));
                    log_hlaska = string.Format("TISK zmena status --- FALSE");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                }




            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH TiskPrijem"));
                //Log.Write(string.Format(ex.Message));
                ExceptionHandler2.Handle(ex);
            }


            Fask.Tracing.Trac.Write("END metody TiskPrijem", tracId); 

        }

        public void TiskPrijem_Vaha(int cisloStatus)
        {

            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "TiskPrijem_Vaha");
            Fask.Tracing.Trac.Write("START metody TiskPrijem_Vaha", tracId); 


            try
            {
                int cislo_bocedi = 0;
                if (cisloStatus == AGRO.status_31)
                {
                    cislo_bocedi = 1;
                }
                else if (cisloStatus == AGRO.status_32)
                {
                    cislo_bocedi = 2;
                }

                bool zapis_OK = false;
                Guid? G_value = null;
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                //FASK_Events FE_objekt_kopie = new FASK_Events();
                //na zaklade cisloStatus = 21 or 22 vyhledam zaznam
                //------------hledam status = 21 a nema status = 41
                //---------------vratim nejstarsi zaznam-----------
                if (cisloStatus == AGRO.status_31)
                {
                    FE_objekt = Najdi_zaznam(cisloStatus, AGRO.status_41);
#if DEBUG
                    //Log.Write(string.Format("TISK STR_1 status: {0}", cisloStatus));
                    log_hlaska = string.Format("TISK STR_1 status: {0}", cisloStatus);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                }
                else if (cisloStatus == AGRO.status_32)
                {
                    FE_objekt = Najdi_zaznam(cisloStatus, AGRO.status_42);
#if DEBUG
                    //Log.Write(string.Format("TISK STR_2 status: {0}", cisloStatus));
                    log_hlaska = string.Format("TISK STR_2 status: {0}", cisloStatus);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                }

                //pridam hodnoty do objektu pro tisk
                //vyhledany zaznam odeslu na tisk
                if (FE_objekt != null && FE_objekt.faskGUID.HasValue)
                {
                    // OnTISKEvent(new TISK_EventArgs(FE_objekt_kopie));

                    G_value = FE_objekt.faskGUID;
#if DEBUG
                    // Log.Write(string.Format("TISK hledany zaznam SSCC: {0}", FE_objekt.NMBRPAL));
                    log_hlaska = string.Format("TISK hledany zaznam SSCC: {0}", FE_objekt.NMBRPAL);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                    //TiskPosliData(FE_objekt);
                    //zmena status na 41 or 42
#if DEBUG
                    //Log.Write(string.Format("TISK zmena status SSCC: {0} status: {1}", FE_objekt.NMBRPAL, FE_objekt.status));
                    log_hlaska = string.Format("TISK zmena status SSCC: {0} status: {1}", FE_objekt.NMBRPAL, FE_objekt.status);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    if (cisloStatus == AGRO.status_31)
                    {
                        FE_objekt.reportType = "S";
                        zapis_OK = FaskEvents_WriteToRow_tisk(FE_objekt, AGRO.status_41);
                    }
                    else if (cisloStatus == AGRO.status_32)
                    {
                        FE_objekt.reportType = "S";
                        zapis_OK = FaskEvents_WriteToRow_tisk(FE_objekt, AGRO.status_42);
                    }

                    TiskPosliData(FE_objekt, cislo_bocedi);

                }
                else
                {
#if DEBUG
                    //Log.Write(string.Format("TISK neni hledany zaznam !ERROR!"));
                    log_hlaska = string.Format("TISK neni hledany zaznam !ERROR!");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                }



                if (zapis_OK)
                {
#if DEBUG
                    //Log.Write(string.Format("TISK zmena status --- TRUE"));
                    log_hlaska = string.Format("TISK zmena status --- TRUE");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    if (G_value != null)
                    {
#if DEBUG
                        //Log.Write(string.Format("TISK archivace GUID: {0}", G_value));
                        log_hlaska = string.Format("TISK archivace GUID: {0}", G_value);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                        if (cisloStatus == AGRO.status_31)
                        {
                            #region old code - nepotrebne
                            //FaskEvents_Update(G_value, AGRO.status_231); 
                            #endregion

                            Database.Classes.Vyroba_Remote.FaskEvents_Update(
                                   Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                   int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                   G_value,
                                   Fask.Constants.AGRO.status_231);
#if DEBUG
                            //Log.Write(string.Format("TISK archivace --- OK"));
                            log_hlaska = string.Format("TISK archivace --- OK");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                        }
                        else if (cisloStatus == AGRO.status_32)
                        {
                            #region old code - nepotrebne
                            //FaskEvents_Update(G_value, AGRO.status_232); 
                            #endregion

                            Database.Classes.Vyroba_Remote.FaskEvents_Update(
                                   Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                   int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                   G_value,
                                   Fask.Constants.AGRO.status_232);
#if DEBUG
                            //Log.Write(string.Format("TISK archivace --- OK"));
                            log_hlaska = string.Format("TISK archivace --- OK");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                        }
                    }
                    else
                    {
#if DEBUG
                        //Log.Write(string.Format("TISK archivace --- FALSE"));
                        log_hlaska = string.Format("TISK archivace --- FALSE");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    }
                }
                else
                {
#if DEBUG
                    //Log.Write(string.Format("TISK zmena status --- FALSE"));
                    log_hlaska = string.Format("TISK zmena status --- FALSE");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                }




            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH TiskPrijem"));
                //Log.Write(string.Format(ex.Message));
                ExceptionHandler2.Handle(ex);
            }

#if DEBUG
            Fask.Tracing.Trac.Write("END metody TiskPrijem_Vaha", tracId); 
#endif
        }

        public void TiskPosliData(Fask.WEBAPI.API_BusinessObjects.FASK_Events_row o, int bocedi_cislo)
        {

            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "TiskPosliData");
            Fask.Tracing.Trac.Write("START metody TiskPosliData", tracId); 


            try
            {
                //Log.Write("TISK--START-POSLI DATA");
                ExceptionHandler2.Handle("TISK--START-POSLI DATA", "Log_LV", "txt");

                #region Logovani dat posilanych na tisk
                try
                {
                    TiskDataLog(o);

                }
                catch (Exception ex)
                {
                    ExceptionHandler2.Handle(ex);
                }
                #endregion

                //kontrola posilanych dat k TISKU: pokud nejaky parametr je prazdny, tak posilam data tak aby se netisknulo
                if (o == null)
                {

                    ExceptionHandler2.Handle("TISK ERROR --posilam falesna data!!", "Log_LV", "txt");
                    o = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                    o.NMBRPAL = "33333333333333333333";
                    o.material = "NO DATA";
                    o.PackType = "FP";
                    o.barcodeSended = "NO DATA";
                    o.ITEMDESC = "NO DATA";
                    o.WEIGHT = 0;

                    //netusim jaky dat status 41/42 ?? pusti paletu spravna tiskarna???
                    if (bocedi_cislo == AGRO.bocedi_1)
                    {
                        o.status = AGRO.status_41;
                    }
                    else if (bocedi_cislo == AGRO.bocedi_2)
                    {
                        o.status = AGRO.status_42;
                    }//potreba dalsi bocedi? dopsat kod
                    else if (bocedi_cislo == AGRO.bocedi_4_K)
                    {
                        o.status = AGRO.status_44;
                    }//potreba dalsi bocedi? dopsat kod


                }
                else
                {
                    if (
                       string.IsNullOrEmpty(o.PackType)
                    || string.IsNullOrEmpty(o.NMBRPAL)
                    || !o.status.HasValue
                    || string.IsNullOrEmpty(o.material)
                    || string.IsNullOrEmpty(o.barcodeSended)
                    || string.IsNullOrEmpty(o.ITEMDESC)
                    || !o.WEIGHT.HasValue
                        )
                    {
                        ExceptionHandler2.Handle("TISK ERROR --posilam falesna data!!", "Log_LV", "txt");
                        o.NMBRPAL = "33333333333333333333";
                        o.material = "NO DATA";
                        o.PackType = "FP";
                        o.barcodeSended = "NO DATA";
                        o.ITEMDESC = "NO DATA";
                        o.WEIGHT = 0;

                        //netusim jaky dat status 41/42 ?? pusti paletu spravna tiskarna???
                        if (bocedi_cislo == AGRO.bocedi_1)
                        {
                            o.status = AGRO.status_41;
                        }
                        else if (bocedi_cislo == AGRO.bocedi_2)
                        {
                            o.status = AGRO.status_42;
                        }//potreba dalsi bocedi? dopsat kod
                        else if (bocedi_cislo == AGRO.bocedi_4_K)
                        {
                            o.status = AGRO.status_44;
                        }//potreba dalsi bocedi? dopsat kod

                    }
                }
                // API komunikace
                //FASK_Events o = new FASK_Events();

                #region old code - nepotrebne
                //IRestResponse restResponse;
                //string param = "agrocs";
                //string JSON = "";

                ////Log.Write("TISK START:" + o.NMBRPAL);
                //ExceptionHandler2.Handle("TISK START:" + o.NMBRPAL, "Log_LV", "txt");

                //JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(o);

                //if (!Classes.WEBAPI.Comunication.Communicate(Classes.WEBAPI.Comunication.REST_Type.POST, out restResponse, param, JSON, "tisk"))
                //{
                //    // throw new Exception("Komunikace s TISK se nezdarila");
                //    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace s IS AGRO se nezdarila--" + restResponse.Content);
                //}

                ////TODO doplnit reakci na poslana data Tisku
                ////pokud je OK neresim
                ////dalsi varianty dohodnout..nova logika --> vzit posledni zaznam se statusem 41/42 a znovu poslat na tisk???

                //if (restResponse.StatusCode != HttpStatusCode.OK)
                //{
                //    var content = restResponse.Content;
                //    //Log.Write("TISK - StatusCode: {0}" + restResponse.StatusCode.ToString());
                //    ExceptionHandler2.Handle("TISK - StatusCode: {0}" + restResponse.StatusCode.ToString(), "Log_LV", "txt");

                //    //Log.Write("TISK - Content: {0}" + restResponse.Content);
                //    ExceptionHandler2.Handle("TISK - Content: {0}" + restResponse.Content, "Log_LV", "txt");
                //}
                //else
                //{
                //    // var content = Newtonsoft.Json.JsonConvert.DeserializeObject<>(restResponse.Content);
                //    // Log.Write("TISK odeslana data OK");
                //    ExceptionHandler2.Handle("TISK odeslana data OK", "Log_LV", "txt");
                //}  
                #endregion


                var txt = Komunikace.Leonardo_TISK_API(o);

                if (txt != "OK")
                {
                    int akce = 6;
                    int cisloLinky = 0;
                    if (!string.IsNullOrEmpty(o.machineid))
                        cisloLinky = Int32.Parse(o.machineid);

                    this.OnLogsEvent(new Logs_EventArgs(cisloLinky, akce, txt, 0, AGRO.status_40));//potreba dalsi bocedi? dopsat kod
                }
                   

            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }


            Fask.Tracing.Trac.Write("END metody TiskPosliData", tracId); 

        }

        public void TiskDataLog(Fask.WEBAPI.API_BusinessObjects.FASK_Events_row o)
        {
            try
            {
                string data_tisk = string.Empty;

                if (AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].Logging)
                {
                    if (o == null)
                    {
                        data_tisk = "ID:" + "[" + string.Empty + "]";
                        data_tisk += ", status:" + "[" + string.Empty + "]";
                        data_tisk += ", PackType:" + "[" + string.Empty + "]";
                        data_tisk += ", NMBRPAL:" + "[" + string.Empty + "]";
                        data_tisk += ", material:" + "[" + string.Empty + "]";
                        data_tisk += ", barcodeSended:" + "[" + string.Empty + "]";
                        data_tisk += ", ITEMDESC:" + "[" + string.Empty + "]";
                        data_tisk += ", WEIGHT:" + "[" + string.Empty + "]";
                        data_tisk += ", LOGINID:" + "[" + string.Empty + "]";
                        data_tisk += ", machineid:" + "[" + string.Empty + "]";
                        data_tisk += ", dateeve:" + "[" + string.Empty + "]";
                        data_tisk += ", qty:" + "[" + string.Empty + "]";
                        data_tisk += ", qtyReal:" + "[" + string.Empty + "]";
                        data_tisk += ", description:" + "[" + string.Empty + "]";
                        data_tisk += ", barcodeReaded:" + "[" + string.Empty + "]";
                        data_tisk += ", zakazka:" + "[" + string.Empty + "]";
                        data_tisk += ", popis:" + "[" + string.Empty + "]";
                        data_tisk += ", faskGUID:" + "[" + string.Empty + "]";
                        data_tisk += ", reportType:" + "[" + string.Empty + "]";
                        data_tisk += ", isProcessed:" + "[" + string.Empty + "]";
                        data_tisk += ", IDO:" + "[" + string.Empty + "]";
                        data_tisk += ", scan1:" + "[" + string.Empty + "]";
                        data_tisk += ", scan2:" + "[" + string.Empty + "]";
                        data_tisk += ", scan3:" + "[" + string.Empty + "]";
                        data_tisk += ", sensor:" + "[" + string.Empty + "]";
                        data_tisk += ", VPH:" + "[" + string.Empty + "]";
                        data_tisk += ", VPPol:" + "[" + string.Empty + "]";
                        data_tisk += ", EAN_IS:" + "[" + string.Empty + "]";
                        data_tisk += ", IS_ID:" + "[" + string.Empty + "]";
                        data_tisk += ", productionGuid:" + "[" + string.Empty + "]";
                        data_tisk += ", QTYPACK:" + "[" + string.Empty + "]";
                    }
                    else
                    {
                        data_tisk = "ID:" + "[" + o.id.ToString().Trim() + "]";
                        data_tisk += ", status:" + "[" + (o.status.HasValue ? o.status.ToString().Trim() : string.Empty) + "]";
                        data_tisk += ", PackType:" + "[" + (string.IsNullOrEmpty(o.PackType) ? string.Empty : o.PackType.Trim()) + "]";
                        data_tisk += ", NMBRPAL:" + "[" + (string.IsNullOrEmpty(o.NMBRPAL) ? string.Empty : o.NMBRPAL.Trim()) + "]";
                        data_tisk += ", material:" + "[" + (string.IsNullOrEmpty(o.material) ? string.Empty : o.material.Trim()) + "]";
                        data_tisk += ", barcodeSended:" + "[" + (string.IsNullOrEmpty(o.barcodeSended) ? string.Empty : o.barcodeSended.Trim()) + "]";
                        data_tisk += ", ITEMDESC:" + "[" + (string.IsNullOrEmpty(o.ITEMDESC) ? string.Empty : o.ITEMDESC.Trim()) + "]";
                        data_tisk += ", WEIGHT:" + "[" + (o.WEIGHT.HasValue ? o.WEIGHT.ToString().Trim() : string.Empty) + "]";
                        data_tisk += ", LOGINID:" + "[" + (string.IsNullOrEmpty(o.loginid) ? string.Empty : o.loginid.Trim()) + "]";
                        data_tisk += ", machineid:" + "[" + (string.IsNullOrEmpty(o.machineid) ? string.Empty : o.machineid.Trim()) + "]";
                        data_tisk += ", dateeve:" + "[" + (o.dateeve.HasValue ? o.dateeve.ToString().Trim() : string.Empty) + "]";
                        data_tisk += ", qty:" + "[" + o.qty.ToString().Trim() + "]";
                        data_tisk += ", qtyReal:" + "[" + o.qtyReal.ToString().Trim() + "]";
                        data_tisk += ", description:" + "[" + (string.IsNullOrEmpty(o.description) ? string.Empty : o.description.Trim()) + "]";
                        data_tisk += ", barcodeReaded:" + "[" + (string.IsNullOrEmpty(o.barcodeReaded) ? string.Empty : o.barcodeReaded.Trim()) + "]";
                        data_tisk += ", zakazka:" + "[" + (string.IsNullOrEmpty(o.zakazka) ? string.Empty : o.zakazka.Trim()) + "]";
                        data_tisk += ", popis:" + "[" + (string.IsNullOrEmpty(o.popis) ? string.Empty : o.popis.Trim()) + "]";
                        data_tisk += ", faskGUID:" + "[" + (o.faskGUID.HasValue ? o.faskGUID.ToString() : string.Empty) + "]";
                        data_tisk += ", reportType:" + "[" + (string.IsNullOrEmpty(o.reportType) ? string.Empty : o.reportType.Trim()) + "]";
                        data_tisk += ", isProcessed:" + "[" + (o.isProcessed.HasValue ? o.isProcessed.ToString().Trim() : string.Empty) + "]";
                        data_tisk += ", IDO:" + "[" + (string.IsNullOrEmpty(o.IDO) ? string.Empty : o.IDO.Trim()) + "]";
                        data_tisk += ", scan1:" + "[" + (string.IsNullOrEmpty(o.scan1) ? string.Empty : o.scan1.Trim()) + "]";
                        data_tisk += ", scan2:" + "[" + (string.IsNullOrEmpty(o.scan2) ? string.Empty : o.scan2.Trim()) + "]";
                        data_tisk += ", scan3:" + "[" + (string.IsNullOrEmpty(o.scan3) ? string.Empty : o.scan3.Trim()) + "]";
                        data_tisk += ", sensor:" + "[" + (string.IsNullOrEmpty(o.sensor) ? string.Empty : o.sensor.Trim()) + "]";
                        data_tisk += ", VPH:" + "[" + (string.IsNullOrEmpty(o.VPH) ? string.Empty : o.VPH.Trim()) + "]";
                        data_tisk += ", VPPol:" + "[" + (o.VPPol.HasValue ? o.VPPol.ToString().Trim() : string.Empty) + "]";
                        data_tisk += ", EAN_IS:" + "[" + (string.IsNullOrEmpty(o.EAN_IS) ? string.Empty : o.EAN_IS.Trim()) + "]";
                        data_tisk += ", IS_ID:" + "[" + (string.IsNullOrEmpty(o.IS_ID) ? string.Empty : o.IS_ID.Trim()) + "]";
                        data_tisk += ", productionGuid:" + "[" + (o.productionGuid.HasValue ? o.productionGuid.ToString() : string.Empty) + "]";
                        data_tisk += ", QTYPACK:" + "[" + o.QTYPACK.ToString().Trim() + "]";
                    }

                    ExceptionHandler2.Handle(data_tisk, "TISK_LV", "txt");
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }

        public bool FaskEvents_WriteToRow_tisk(Fask.WEBAPI.API_BusinessObjects.FASK_Events_row o, int StatusNew)
        {

            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "FaskEvents_WriteToRow_tisk");
            Fask.Tracing.Trac.Write("START metody FaskEvents_WriteToRow_tisk", tracId); 


            try
            {
                bool stav = false;
                //bool SQL_select = AgroSledovaniVozikuConfig.config.WEBAPI[0].Enable;
                // FASK_Events o = new FASK_Events();

                stav = Database.Classes.Vyroba_Remote.FaskEvents_WriteToRow(
                    Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                    int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                    o,
                    StatusNew,
                    "presun na tisk");


                Fask.Tracing.Trac.Write("END metody FaskEvents_WriteToRow_tisk", tracId); 


                return stav;

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
                return false;

            }

        }
        #endregion

        #region TEST_KOMUNIKACE
        public void FE_data()
        {
            try
            {
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row objekt_Fe_data = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row objekt_Fe_data_test = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                object o = CallServerMethod(objekt_Fe_data);

                objekt_Fe_data = o as Fask.WEBAPI.API_BusinessObjects.FASK_Events_row;
                objekt_Fe_data_test = (Fask.WEBAPI.API_BusinessObjects.FASK_Events_row)o;
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        } 

        public object CallServerMethod(object type)
        {
           // FASK_Events type = new FASK_Events();
            bool prubeh = false;
            object o = null;
            string separator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator;
            int pocetPruchodu = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchoduNakladka;
            int timeSleep = AgroSledovaniVozikuConfig.config.DB_Config[0].TimeSleep;

            Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();
            filtr_Events.machineid = AGRO.linka_1;
            filtr_Events.status = AGRO.status_0;
            filtr_Events.statusNew = AGRO.status_21; //zmenit na promennou
            filtr_Events.separator = separator;
            filtr_Events.pocetPruchodu = pocetPruchodu;
            filtr_Events.timeSleep = timeSleep;

            prubeh = ServerComm(filtr_Events, "SledovaniVyroby_data", "fask", type, out o);

            return o;
        }

        #endregion

        #region KOMUNIKACE SE SERVREM

        /// <summary>
        /// API Comunnication with server as 3 layers
        /// </summary>
        /// <param name="data_IN">input object for sent data to server</param>
        /// <param name="paramURL">URL method for call</param>
        /// <param name="select_comm">select communication configuration</param>
        /// <param name="type">object type for output</param>
        /// <param name="data_OUT">output object for recieve data from server</param>
        /// <returns>state for communication</returns>
        public bool ServerComm(object data_IN, string paramURL, string select_comm, object type, out object data_OUT)
        {
            bool stateMethod = false;
            IRestResponse restResponse;
            string JSON = string.Empty;
            data_OUT = null;
            Fask.WEBAPI.API_BusinessObjects.FASK_Events_row type_1 = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
            //Type t = type.GetType();

            try
            {
                // API komunikace
                JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(data_IN);

                if (!Classes.WEBAPI.Comunication.Communicate(Classes.WEBAPI.Comunication.REST_Type.POST, out restResponse, paramURL, JSON, select_comm))
                {
                    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace s IS AGRO se nezdarila--" + restResponse.Content);
                }

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    //data_OUT = Newtonsoft.Json.JsonConvert.DeserializeObject(restResponse.Content);
                   data_OUT = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(restResponse.Content, type_1);
                    stateMethod = true; //komunikace probehla v poradku
                }
                else
                {
                   // data_OUT = Newtonsoft.Json.JsonConvert.DeserializeObject(restResponse.Content);

                    data_OUT = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(restResponse.Content,restResponse);

                }

                return stateMethod;

            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
                return stateMethod;
            }

        }
        #endregion

        #region STATUS 61/62

        public bool ZmenaStatusu(int status)
        {
            try
            {
                bool remoteData = false;
                int zpozdeni = 2000;

                try
                {
                    zpozdeni = AgroSledovaniVozikuConfig.config.Vykladka[0].Time_Tisk_END;
                }
                catch (Exception ex)
                {
                    zpozdeni = 2000;
                    ExceptionHandler2.Handle(ex);
                }

                Thread.Sleep(zpozdeni);
                log_hlaska = string.Format("Status zmena 61/62 zpozdeni {0}ms", zpozdeni);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                #region old code - nepotrebne

                //// API komunikace
                //IRestResponse restResponse;
                //string param = "SV_ZmenaStatusu";
                //string JSON = "";
                //JSON = Classes.WEBAPI.JSON_Class.Serialize_JSON(status);


                //if (!Classes.WEBAPI.Comunication.Communicate(Classes.WEBAPI.Comunication.REST_Type.POST, out restResponse, param, JSON))
                //{
                //    ExceptionHandler2.Handle(LogLevel.Error, "Komunikace se nezdarila--" + restResponse.Content);
                //}

                //if (restResponse.StatusCode == HttpStatusCode.OK)
                //{


                //    if (!string.IsNullOrEmpty(restResponse.Content))
                //    {
                //        remoteData = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);
                //    }
                //    else
                //        throw new Exception("Data nenalezena");


                //    return remoteData;
                //}

                //return false; 
                #endregion

                return Database.Classes.Vyroba_Remote.SV_ZmenaStatusu(
                   Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                   int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                   status);

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }

        }
        #endregion



    }

    public class Vykladka_EventArgs : EventArgs
    {
        public int CisloLinky { get; set; }
        public int CisloStrec { get; set; }

        public Vykladka_EventArgs(int cl, int cislo_strec)
        {
            CisloLinky = cl;
            CisloStrec = cislo_strec;
        }
    }

   

}
 


