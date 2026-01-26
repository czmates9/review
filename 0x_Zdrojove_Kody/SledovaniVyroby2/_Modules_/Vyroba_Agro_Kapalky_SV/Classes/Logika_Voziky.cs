using System;
using System.Collections.Generic;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Kapalky_SV.Configuration;
using FASK.SledovaniVyroby.ErrorLog;
using RestSharp;
using System.Net;
using Fask.Logging;
using System.Threading;
using Fask.WEBAPI;
using Fask.WEBAPI.API_BusinessObjects;
using Fask.Constants;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Kapalky_SV.Classes
{
    public class Logika_Voziky
    {

        public string log_hlaska = string.Empty;

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

            ICommDatabase.DSVyroba.FASK_EventsErrDataTable dt = new ICommDatabase.DSVyroba.FASK_EventsErrDataTable();

            try
            { 
                var row = dt.NewFASK_EventsErrRow();

                #region row spolecna data

                int LoginID = _parent.LoginID;

                if (LoginID == -1)
                    row.loginid = e.LoginID.HasValue ? e.LoginID.Value.ToString() : "22";
                else
                    row.loginid = LoginID.ToString();


                //logika statusu: 0 kdyz vznikne zaznam a 200 pri deaktivaci
                if (e.StatusLogs.HasValue)
                {
                    row.status = e.StatusLogs.Value;
                }
                else
                {
                    row.status = 0;
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
                else if (cisloAkce == 2)
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
                else
                {
                    //nedefinovana akce
                    ExceptionHandler2.Handle("LOGS -- metoda selhala, nedefinovaná akce!", "Log_LV", "txt");
                    return;
                }


                dt.AddFASK_EventsErrRow(row);



                Database.Classes.Vyroba_Remote.SV_Logs_insert(
                    Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                     int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                    dt);

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

                //mazu interni promennou
                lock (_parent.interniCisloLinka_object)
                {
                    _parent.interniCisloLinka = 0;
                }

                //ay bude odladena logika bez RFID, tak zakomentovat nasledujici a odkomentovat radek s //*33
                //nastavuji prichozi paletu z linky 
                cisloLinky = e.CisloLinky;

                log_hlaska = string.Format("VYKLADKA--START- Příjem z linky: {0}", cisloLinky);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row objekt_Fe_data = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row objekt_Fe_data_falsak = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();

                Guid? G_value = null;

                //logika hledani zaznamu na dane lince
                int statusNakladka =  AGRO.status_0;
                int statusVykladka =  AGRO.status_24;
                bool zapis_OK = false;
                bool falesnyZaznam = false;

                statusNakladka =  AGRO.status_0; 
                statusVykladka =  AGRO.status_24; 
                zapis_OK = false; 
                falesnyZaznam = false;

                #region Dohledání/Vytvoření záznamu nakládky
                Fask.Tracing.Trac.Write("START dohledání záznamu nakládky", tracId);
                if (cisloLinky ==  AGRO.rucni_vstup_K || cisloLinky ==  AGRO.falsak_K)
                {
                    //objekt_Fe_data.id = 33;
                    objekt_Fe_data.loginid = "44";
                    if (cisloLinky ==  AGRO.rucni_vstup_K)
                    {
                        objekt_Fe_data.machineid =  AGRO.rucni_vstup_K.ToString();
                        objekt_Fe_data.description = "Rucni vstup";
                        objekt_Fe_data.IS_ID = "A6666A";
                        objekt_Fe_data.EAN_IS = "8596666666664";
                    }
                    else
                    {
                        objekt_Fe_data.description = "Nedohledan vstup";
                        objekt_Fe_data.machineid =  AGRO.falsak_K.ToString();
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
                    objekt_Fe_data.status = 0;
                    objekt_Fe_data.productionGuid = Guid.NewGuid();
                    objekt_Fe_data.popis = string.Empty;
                    objekt_Fe_data.QTYPACK = 1;
                    objekt_Fe_data.PackType = "FP";
                    objekt_Fe_data.WEIGHT = 0;

                    //------------------------Generovat SSCC !!!!
                    try
                    {
                        FASK.Palety_SSCC.SQLite.Classes.SSCC_Generator generatorSSCC = new Palety_SSCC.SQLite.Classes.SSCC_Generator();
                        string sscc_FP_1 = generatorSSCC.Get_Next_SSCC(AGRO.rucni_vstup_K, 1, AGRO.rucni_vstup_K); // TODO konfigurace ID stroje
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

                    if (e.CisloStrec ==  AGRO.bocedi_4_K)
                    {
                        log_hlaska = string.Format("VYKLADKA zaznam z linky: {0} SSCC: {1} Strec_1: {2}", cisloLinky, objekt_Fe_data.NMBRPAL, e.CisloStrec);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                        zapis_OK = Database.Classes.Vyroba_Remote.FaskEvents_WriteToRow(
                            Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                            int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                            objekt_Fe_data, 
                             AGRO.status_24, 
                            "presun na streckovacku");


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
                    //--------reakce na chybejici zaznam pri vykladce START--------------------------------------
                    if (cisloLinky ==  AGRO.linka_4_R || cisloLinky ==  AGRO.rucni_vstup_K)
                    {

                        //if (cisloLinky ==  AGRO.rucni_vstup_K)
                        //{
                        //    cisloLinky = 4;
                        //    cisloLinky_FP = 4;
                        //}

                        objekt_Fe_data_falsak.machineid = cisloLinky.ToString();
                    }
                    else
                    {
                        objekt_Fe_data_falsak.machineid =  AGRO.falsak_K.ToString();
                        cisloLinky_FP =  AGRO.falsak_K;
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
                    objekt_Fe_data_falsak.status = 0;
                    objekt_Fe_data_falsak.productionGuid = Guid.NewGuid();
                    //objekt_Fe_data.productionGuid = null;
                    objekt_Fe_data_falsak.popis = string.Empty;
                    objekt_Fe_data_falsak.QTYPACK = 1;
                    objekt_Fe_data_falsak.PackType = "FP";
                    objekt_Fe_data_falsak.WEIGHT = 0;


                    //--------reakce na chybejici zaznam pri vykladce END----------------------------------------


                    G_value = objekt_Fe_data_falsak.faskGUID;
                    //zapis do DB streckovacky, podle cisla strec bud status 21 nebo 22
                    if (e.CisloStrec ==  AGRO.bocedi_4_K)
                    {
                        log_hlaska = string.Format("VYKLADKA falesny zaznam z linky: {0} SSCC: {1} Strec_1: {2}", cisloLinky, objekt_Fe_data_falsak.NMBRPAL, e.CisloStrec);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        zapis_OK = Database.Classes.Vyroba_Remote.FaskEvents_WriteToRow(
                                    Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                    int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                    objekt_Fe_data,
                                     AGRO.status_24,
                                    "presun na streckovacku");

                    }


                    Fask.Tracing.Trac.Write("END logiky neznámé nakládky", tracId);
                }

                if (zapis_OK == true)
                {
                    Fask.Tracing.Trac.Write("START archivace nakládky", tracId);
                    log_hlaska = string.Format("VYKLADKA zapis do DB --- CORRECT");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    if (G_value != null)
                    {
                        if (!falesnyZaznam)
                        {
                            if (cisloLinky ==  AGRO.rucni_vstup_K)
                            {
                                log_hlaska = string.Format("VYKLADKA archivace --- Rucni vstup");
                                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                            }
                            else
                            {
                                log_hlaska = string.Format("VYKLADKA archivace GUID: {0}", G_value);
                                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                                
                                Database.Classes.Vyroba_Remote.FaskEvents_Update(
                                    Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                    int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                    G_value,
                                     AGRO.status_200);

                                ExceptionHandler2.Handle("VYKLADKA archivace --- OK", "Log_LV", "txt");
                            }
                        }
                        else
                        {
                            ExceptionHandler2.Handle("VYKLADKA archivace --- Falesny zaznam", "Log_LV", "txt");
                        }


                    }
                    else
                    {
                        ExceptionHandler2.Handle("VYKLADKA archivace --- FALSE", "Log_LV", "txt");
                    }

                    Fask.Tracing.Trac.Write("END archivace nakládky", tracId);
                }
                else
                {
                    ExceptionHandler2.Handle("VYKLADKA zapis do DB --- FALSE", "Log_LV", "txt");
                }


            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

            Fask.Tracing.Trac.Write("END metody Logika_Voziky_vykladka_EventHandler", tracId);
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
                string separator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator;
                int pocetPruchodu = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchoduNakladka;
                int timeSleep = AgroSledovaniVozikuConfig.config.DB_Config[0].TimeSleep;

                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row o = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();

                Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events filtr_Events = new Fask.WEBAPI.API_BusinessObjects.Filtr_FASK_Events();
                filtr_Events.machineid = MachineID;
                filtr_Events.status = Status;
                filtr_Events.statusNew = StatusNew; //zmenit na promennou
                filtr_Events.separator = separator;
                filtr_Events.pocetPruchodu = pocetPruchodu;
                filtr_Events.timeSleep = timeSleep;

                o = Database.Classes.Vyroba_Remote.SledovaniVyroby_data(
                    Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                     int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                    filtr_Events
                    );

                Fask.Tracing.Trac.Write("END metody GetSSCC_zaznam", tracId);

                return o;
            }
            catch (Exception ex)
            {
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

                if (Status ==  AGRO.status_0)
                {
                    TextSeparator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator;
                }
                else if (Status ==  AGRO.status_1)
                {
                    TextSeparator = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec;
                }
                else if (StatusNew ==  AGRO.status_44)
                {
                    if (_parent.uC_Vaha.Vaha_1_Connected() && StatusNew ==  AGRO.status_44 && !_parent.vaha_1_ERROR && Status !=  AGRO.status_24)
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
                else if (Status ==  AGRO.status_44)
                {
                    TextSeparator = AgroSledovaniVozikuConfig.config.DB_Config[0].TextSeparator_Vytisknuto;
                    pocetPruchodu = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchoduVytisknuto;

                    localMachineID = -1;
                }

                #region Nova logika

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

                #endregion


                Fask.Tracing.Trac.Write("END metody Najdi_zaznam", tracId);
                return o;
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return null;
            }


        }

        #endregion

        #region Vykladka - TISK

        public void TiskPrijem(int cisloStatus)
        {
            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "TiskPrijem");
            Fask.Tracing.Trac.Write("START metody TiskPrijem", tracId);

            try
            {
                int cislo_bocedi = 0;
                if (cisloStatus ==  AGRO.status_24)
                {
                    cislo_bocedi =  AGRO.bocedi_4_K;
                }

                bool zapis_OK = false;
                Guid? G_value = null;
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                //FASK_Events FE_objekt_kopie = new FASK_Events();
                //na zaklade cisloStatus = 21 or 22 vyhledam zaznam
                //------------hledam status = 21 a nema status = 41
                //---------------vratim nejstarsi zaznam-----------
                if (cisloStatus ==  AGRO.status_24)
                {
                    FE_objekt = Najdi_zaznam(cisloStatus,  AGRO.status_44);
#if DEBUG
                    //Log.Write(string.Format("TISK STR_1 status: {0}", cisloStatus));
                    log_hlaska = string.Format("TISK STR_1 status: {0}", cisloStatus);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                }

                //pridam hodnoty do objektu pro tisk
                //vyhledany zaznam odeslu na tisk
                if (FE_objekt != null && FE_objekt.faskGUID.HasValue)
                {
                    G_value = FE_objekt.faskGUID;
#if DEBUG
                    log_hlaska = string.Format("TISK hledany zaznam SSCC: {0}", FE_objekt.NMBRPAL);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                    //zmena status na 41
#if DEBUG
                    log_hlaska = string.Format("TISK zmena status SSCC: {0} status: {1}", FE_objekt.NMBRPAL, FE_objekt.status);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    if (cisloStatus ==  AGRO.status_24)
                    {
                        FE_objekt.reportType = "A";

                        zapis_OK = Database.Classes.Vyroba_Remote.FaskEvents_WriteToRow(
                                        Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                        int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                        FE_objekt,
                                         AGRO.status_44,
                                        "presun na tisk");
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
                    log_hlaska = string.Format("TISK zmena status --- TRUE");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    if (G_value != null)
                    {
#if DEBUG
                        log_hlaska = string.Format("TISK archivace GUID: {0}", G_value);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                        if (cisloStatus ==  AGRO.status_24)
                        {
                            Database.Classes.Vyroba_Remote.FaskEvents_Update(
                                 Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                 int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                 G_value,
                                  AGRO.status_224);
#if DEBUG
                            log_hlaska = string.Format("TISK archivace --- OK");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                        }
                    }
                    else
                    {
#if DEBUG
                        log_hlaska = string.Format("TISK archivace --- FALSE");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    }
                }
                else
                {
#if DEBUG
                    log_hlaska = string.Format("TISK zmena status --- FALSE");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                }
            }
            catch (Exception ex)
            {
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
                if (cisloStatus ==  AGRO.status_34)
                {
                    cislo_bocedi =  AGRO.bocedi_4_K;
                }


                bool zapis_OK = false;
                Guid? G_value = null;
                Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                //FASK_Events FE_objekt_kopie = new FASK_Events();
                //na zaklade cisloStatus = 21 or 22 vyhledam zaznam
                //------------hledam status = 21 a nema status = 41
                //---------------vratim nejstarsi zaznam-----------
                if (cisloStatus ==  AGRO.status_34)
                {
                    FE_objekt = Najdi_zaznam(cisloStatus,  AGRO.status_44);
#if DEBUG
                    //Log.Write(string.Format("TISK STR_1 status: {0}", cisloStatus));
                    log_hlaska = string.Format("TISK STR_1 status: {0}", cisloStatus);
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
                    //zmena status na 41 
#if DEBUG
                    //Log.Write(string.Format("TISK zmena status SSCC: {0} status: {1}", FE_objekt.NMBRPAL, FE_objekt.status));
                    log_hlaska = string.Format("TISK zmena status SSCC: {0} status: {1}", FE_objekt.NMBRPAL, FE_objekt.status);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    if (cisloStatus ==  AGRO.status_34)
                    {
                        FE_objekt.reportType = "S";
                        //zapis_OK = FaskEvents_WriteToRow_tisk(FE_objekt, 41);
                        //zapis_OK = Komunikace.FaskEvents_WriteToRow(FE_objekt, 41, "presun na tisk");
                        zapis_OK = Database.Classes.Vyroba_Remote.FaskEvents_WriteToRow(
                                    Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                    int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                    FE_objekt,
                                     AGRO.status_44,
                                    "presun na tisk");

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
                        if (cisloStatus ==  AGRO.status_34)
                        {
                            //Komunikace.FaskEvents_Update(G_value, 231);
                            Database.Classes.Vyroba_Remote.FaskEvents_Update(
                                 Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Remote,
                                 int.Parse(FASK.SledovaniVyroby.Main.Configuration.Config.config.Main[0].MachineID),
                                 G_value,
                                  AGRO.status_234);
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

            Fask.Tracing.Trac.Write("END metody TiskPrijem_Vaha", tracId);
        }

        public void TiskPosliData(Fask.WEBAPI.API_BusinessObjects.FASK_Events_row o, int bocedi_cislo_drahy)
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


                    if (bocedi_cislo_drahy ==  AGRO.bocedi_4_K)
                    {
                        o.status =  AGRO.status_44;
                    }

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

                        if (bocedi_cislo_drahy ==  AGRO.bocedi_4_K)
                        {
                            o.status =  AGRO.status_44;
                        }

                    }
                }

                var txt = Komunikace.Leonardo_TISK_API(o);
                if (txt != "OK")
                {
                    int akce = 6;
                    int cisloLinky = 0;
                    if (!string.IsNullOrEmpty(o.machineid))
                        cisloLinky = Int32.Parse(o.machineid);

                    this.OnLogsEvent(new Logs_EventArgs(cisloLinky, akce, txt, 0,  AGRO.status_44));
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

        #endregion

        #region STATUS 61

        public bool ZmenaStatusu(int status)
        {
            try
            {
                
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
                log_hlaska = string.Format("Status zmena 61 zpozdeni {0}ms", zpozdeni);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


                //return Classes.Komunikace.SV_ZmenaStatusu(status);
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
 


