using FASK.SledovaniVyroby.ModuleIfc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_SV_Modbus.Configuration;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_SV_Modbus.Classes;
using System.Threading;
using Fask.Logging;
using static FASK.SledovaniVyroby.Module.Vyroba_Agro_SV_Modbus.Classes.Logika_Voziky;
using FASK.SledovaniVyroby.Main.Configuration;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_SV_Modbus
{


    public partial class frmMain : Form, IModuleConnector
    {

        #region Parametry

        #region drzeni informaci o prihlasenem uzivateli

        //const ??
        public int LoginID = -1;
        public object LoginID_object = new object();

        #endregion

        #region promenne online log

        public bool OnlineLog_Bocedi_1 = false;
        public object OnlineLog_Bocedi_1_object = new object();

        #endregion

        public Forms.FormIDPracovnikaLogin Rodic = null;

        public string log_hlaska = string.Empty;


        private bool _openFlag = false;
        public bool isOpen()
        {
            return _openFlag;
        }

        private System.Threading.Timer timer_strec_1;

        public string Const_Zapis_Linky = "Zapis_Linky";
        public string Const_Zapis_Odvadeni = "Zapis_Odvadeni";
        public string Const_Kontrola_Linky = "Kontrola_Linky";
        public string Const_Kontrola_Odvadeni = "Kontrola_Odvadeni";

        private System.Threading.Timer timer_vaha_1;

        public bool vaha_1_data = false;
        public bool vaha_1_ERROR = false;
        public object vaha_1_data_object = new object();

        private ADAM.ADAM_60XX adam_1 = null;
        private ADAM.ADAM_60XX adam_2 = null;

        private const string vyber_adam_1 = "adam_1";
        private const string vyber_adam_2 = "adam_2";

        private System.Threading.Timer timerRefreshUI = null;

        delegate void AdamSensorDelegate(int e);

        private Classes.Logika_Voziky LV;

        private int _interniCisloLinka = 0; //vyjadruje cislo linky ze ktere vyjela paleta, ktera je nalozena na voziku aktualne
        public int interniCisloLinka
        {
            get { return _interniCisloLinka; }
            set 
            {
                _interniCisloLinka = value;
                Show_interniCisloLinka(_interniCisloLinka);
            }
        }

        public object interniCisloLinka_object = new object();

        #endregion

        #region Event RFID

        public delegate void LogikaZAdama_EventHandler(Object sender, LogikaZAdama_EventArgs e);

        public event LogikaZAdama_EventHandler _logikaZAdama_EventHandler;

        private void OnLogikaZAdamaEvent(LogikaZAdama_EventArgs e)
        {
            LogikaZAdama_EventHandler handler = _logikaZAdama_EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Eventy formu

        /// <summary>
        /// c'tor
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            try
            {
                #region Nastaveni cesty pro logovani

                string startupPath = System.IO.Directory.GetCurrentDirectory();
                ExceptionHandler2.SetPath(startupPath); 

                #endregion

                //Fask.Tracing.Trac.Enable = true;
                Fask.Tracing.Trac.Enable = Config.config.Logging[0].Enable_Trace;

                ExceptionHandler2.Handle("Spusteni modulu", "Log_LV", "txt");

                LoadConfiguration();

                #region ADAM inicializace

                #region ADAM_1
                
                if(AgroSledovaniVozikuConfig.config.ADAM_1[0].Connect)
                {
                    var cfgPortP2PAdam_1 = AgroSledovaniVozikuConfig.config.ADAM_1[0].IsPortP2PAdamNull() ? 1025 : AgroSledovaniVozikuConfig.config.ADAM_1[0].PortP2PAdam;

                    adam_1 = new ADAM.ADAM_60XX(
                        AgroSledovaniVozikuConfig.config.ADAM_1[0].IPAdresaAdam,
                        cfgPortP2PAdam_1,    //bude vkladano rucne IP a 1025 default
                        AgroSledovaniVozikuConfig.config.ADAM_1[0].TimerPeriodSensorsCheckAdam,
                        AgroSledovaniVozikuConfig.config.ADAM_1[0].AdamTCPTimeout,
                        AgroSledovaniVozikuConfig.config.ADAM_1[0].MinHighSignalWidthAdam,
                        AgroSledovaniVozikuConfig.config.ADAM_1[0].communication_TCP,
                        AgroSledovaniVozikuConfig.config.ADAM_1[0].communication_P2P
                        );
                    //AdamStart();
                    aDAM_UC_1.Gb_value = "ADAM vozik";
                    if (AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].enabled)
                    {
                        aDAM_UC_1.DI0_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI0;
                        aDAM_UC_1.DI1_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI1;
                        aDAM_UC_1.DI2_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI2;
                        aDAM_UC_1.DI3_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI3;
                        aDAM_UC_1.DI4_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI4;
                        aDAM_UC_1.DI5_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI5;
                        aDAM_UC_1.DI6_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI6;
                        aDAM_UC_1.DI7_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI7;
                        aDAM_UC_1.DI8_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI8;
                        aDAM_UC_1.DI9_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI9;
                        aDAM_UC_1.DI10_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI10;
                        aDAM_UC_1.DI11_popis = AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION[0].DI11; 
                    }
                }

                #endregion

                #region ADAM_2

                if (AgroSledovaniVozikuConfig.config.ADAM_2[0].Connect)
                {
                    var cfgPortP2PAdam_2 = AgroSledovaniVozikuConfig.config.ADAM_2[0].IsPortP2PAdamNull() ? 1025 : AgroSledovaniVozikuConfig.config.ADAM_2[0].PortP2PAdam;

                    adam_2 = new ADAM.ADAM_60XX(
                        AgroSledovaniVozikuConfig.config.ADAM_2[0].IPAdresaAdam,
                        cfgPortP2PAdam_2,    //bude vkladano rucne IP a 1025 default
                        AgroSledovaniVozikuConfig.config.ADAM_2[0].TimerPeriodSensorsCheckAdam,
                        AgroSledovaniVozikuConfig.config.ADAM_2[0].AdamTCPTimeout,
                        AgroSledovaniVozikuConfig.config.ADAM_2[0].MinHighSignalWidthAdam,
                        AgroSledovaniVozikuConfig.config.ADAM_2[0].communication_TCP,
                        AgroSledovaniVozikuConfig.config.ADAM_2[0].communication_P2P
                        );

                    aDAM_UC_2.Gb_value = "ADAM Strech";

                    if (AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].enabled)
                    {
                        aDAM_UC_2.DI0_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI0;
                        aDAM_UC_2.DI1_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI1;
                        aDAM_UC_2.DI2_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI2;
                        aDAM_UC_2.DI3_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI3;
                        aDAM_UC_2.DI4_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI4;
                        aDAM_UC_2.DI5_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI5;
                        aDAM_UC_2.DI6_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI6;
                        aDAM_UC_2.DI7_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI7;
                        aDAM_UC_2.DI8_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI8;
                        aDAM_UC_2.DI9_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI9;
                        aDAM_UC_2.DI10_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI10;
                        aDAM_UC_2.DI11_popis = AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION[0].DI11; 
                    }
                }

                #endregion

                AdamStart();

                #endregion

                timerRefreshUI = new System.Threading.Timer(TimerRefreshUICallback, null, 100, -1);

                LV = new Classes.Logika_Voziky(this);

                Show_interniCisloLinka(0);
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            panel1_Resize(null,null);
           
            bt_Linka_1.Text = Fask.Constants.AGRO.BTN_Linka4_R;
            bt_RV.Text = Fask.Constants.AGRO.BTN_RV_R;
            bt_bocedi1.Text = Fask.Constants.AGRO.BTN_Bocedi3_R;

            _openFlag = true;
            this.WindowState = FormWindowState.Maximized;
            timer_strec_1 = new System.Threading.Timer(CallBack_Timer_Strec1, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
         
            timer_vaha_1 = new System.Threading.Timer(CallBack_Timer_Vaha1, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            _logikaZAdama_EventHandler += FrmMain_LogikaZAdama_EventHandler;
            //strec_EventHandler += FrmMain_strec_EventHandler;
            //strec_sleep_EventHandler += FrmMain_strec_sleep_EventHandler;

            updateForm(false, "init");

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                AdamStop();
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

        #region CallBackTimers

        int cnt = 0;

        private void SetTextButton()
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        SetTextButton();
                    }));
                    return;
                }
                cnt++;
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }

        private void CallBack_Timer_Strec1(object state)
        {
            try
            {
#if DEBUG
                log_hlaska = string.Format("TIMER--VYKLADKA_Boceddi_{0}", 1);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                if (interniCisloLinka != 0)
                {
#if DEBUG
                    log_hlaska = string.Format("TIMER--VYKLADKA_Boceddi_{0} interniCisloLinka != 0 --číslo linky: {0}", 1, interniCisloLinka);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                    LV.OnVykladkaEvent(new Vykladka_EventArgs(interniCisloLinka, Fask.Constants.AGRO.bocedi_3_R));
                }
                else
                {
                    if(OnlineLog_Bocedi_1)
                    {
                        int akce = 4;
                        string popisek = string.Format("VYKLADKA - Casovas_1 - strec:1 - Falešná vykládka, korektní vyhodnocení!!"); 
                        LV.OnLogsEvent(new Logs_EventArgs(Fask.Constants.AGRO.bocedi_3_R, akce, popisek, LoginID, 0));
                    }
                    else
                    { 
                        //vse je ok
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

        }

        private void CallBack_Timer_Vaha1(object state)
        {
            try
            {

#if DEBUG
                log_hlaska = string.Format(" TIMER--VAHA_{0}", 1);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                if (vaha_1_data)
                {
#if DEBUG
                    log_hlaska = string.Format(" TIMER--VAHA_{0}--START", 1);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                    Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                    FE_objekt = LV.Najdi_zaznam(Fask.Constants.AGRO.status_31, Fask.Constants.AGRO.status_41);

                    if (FE_objekt != null && FE_objekt.faskGUID.HasValue)
                    {
#if DEBUG
                        log_hlaska = string.Format(" TIMER--VAHA_{0} start logiky tisk status 31 ", 1);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                        OnLogikaZAdamaEvent(new LogikaZAdama_EventArgs(Fask.Constants.AGRO.status_31));

                    }
                    else
                    {
#if DEBUG
                        log_hlaska = string.Format(" TIMER--VAHA_{0} start logiky tisk status 21 ", 1);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                        OnLogikaZAdamaEvent(new LogikaZAdama_EventArgs(Fask.Constants.AGRO.status_21));
                    }

                    OnLogikaZAdamaEvent(new LogikaZAdama_EventArgs(Fask.Constants.AGRO.status_41));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

        }

        #endregion

        private void Show_interniCisloLinka(int interniCisloLinka)
        {
       
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        Show_interniCisloLinka(interniCisloLinka);
                    }));

                    return;
                }

            lbl_InterniCisloLinky.Text = interniCisloLinka.ToString();

        }

        private void FrmMain_LogikaZAdama_EventHandler(object sender, LogikaZAdama_EventArgs e)
        {
            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "FrmMain_rfid_EventHandler");
            Fask.Tracing.Trac.Write("START metody FrmMain_rfid_EventHandler", tracId);

            try
            {
                if (e.CisloStatus == Fask.Constants.AGRO.status_21)
                {
                    Fask.Tracing.Trac.Write("START logiky TISK bez vahy 21 ", tracId);
                    lock (vaha_1_data_object)
                    {
                        vaha_1_data = false;
                    }
#if DEBUG
                    log_hlaska = string.Format("RFID_EVENT číslo linky: {0}", e.CisloStatus);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    LV.TiskPrijem(e.CisloStatus);

                    Fask.Tracing.Trac.Write("END logiky TISK bez vahy 21 ", tracId);
                }
                else if (e.CisloStatus == Fask.Constants.AGRO.status_31)
                {
                    Fask.Tracing.Trac.Write("START logiky TISK 31 ", tracId);
                    //nova logika vaha_1
                    lock (vaha_1_data_object)
                    {
                        vaha_1_data = false;
                    }
#if DEBUG
                    log_hlaska = string.Format("RFID_EVENT číslo linky: {0}", e.CisloStatus);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    LV.TiskPrijem_Vaha(e.CisloStatus);

                    Fask.Tracing.Trac.Write("END logiky TISK 31 ", tracId);

                }
                else if (e.CisloStatus == Fask.Constants.AGRO.status_41)
                {

                    Fask.Tracing.Trac.Write("START logiky vizualizace 41 ", tracId);
#if DEBUG
                    log_hlaska = string.Format("RFID_EVENT_VIZUALIZACE císlo linky: {0}", e.CisloStatus);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                    Thread.Sleep(AgroSledovaniVozikuConfig.config.Vykladka[0].TimeVizualizaceTisk);

                    Fask.Tracing.Trac.Write("END logiky vizualizace 41 ", tracId);
                }
                else if (e.CisloStatus == Fask.Constants.AGRO.status_61)
                {
                    Fask.Tracing.Trac.Write("START logiky ukonceni 61 ", tracId);

                    log_hlaska = string.Format("Pokus o zmenu statusu na " + e.CisloStatus);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    if(LV.ZmenaStatusu(e.CisloStatus))
                    {
                        log_hlaska = string.Format("Status "+ e.CisloStatus + "--OK");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }
                    else
                    {
                        log_hlaska = string.Format("Status " + e.CisloStatus + "--ERROR");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }

                    Fask.Tracing.Trac.Write("END logiky ukonceni 61 ", tracId);

                }
                else
                {
                    log_hlaska = string.Format("ERROR FrmMain_rfid_EventHandler neocekavana varianta");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

            Fask.Tracing.Trac.Write("END metody FrmMain_rfid_EventHandler", tracId);
        }

        #region Dedene metody a parametry okna povinne

        //Ikona oznameni
        private NotifyIcon notifyIconState;
        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        //Status label modulu
        private ToolStripStatusLabel statusLabel = null;
        public ToolStripStatusLabel StatusLabel
        {
            set { statusLabel = value; }
        }

        public void ReturnPortsToPreviousState()
        {
        }

        public void ClosePorts()
        {
            TimerRefreshUIOff();
            timerRefreshUI = null;

            AdamStop();
            ExceptionHandler2.Handle("Ukonceni modulu", "Log_LV", "txt");
        }

        public bool IsReadyToClose(out string message)
        {
            message = string.Empty;
            return true;
        }

        public bool IsReadyToShow(out string message)
        {
            message = "ok";
            return true;
        }

        #endregion

        private void LoadConfiguration()
        {
            try
            {
                //Pokud nebyly nacteny zadne parametry, vlozim vychzoi (?)
                AgroSledovaniVozikuConfig.LoadConfiguration();

                try
                {
                    #region DB_Config

                    tb_DB_Con_Separator.Text = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator;
                    tb_DB_Con_PocetPruchodu.Text = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchodu.ToString();
                    tb_DB_Con_TimeSleep.Text = AgroSledovaniVozikuConfig.config.DB_Config[0].TimeSleep.ToString();
                    tb_DB_Con_Separator_strec.Text = AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec;
                    tb_DB_Con_PocetPruchoduNakladka.Text = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchoduNakladka.ToString();
                    tb_DB_Con_PocetPruchoduTisk.Text = AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchoduTisk.ToString();
                    tb_DB_Con_TextSeparator_Tisk_Vaha.Text = AgroSledovaniVozikuConfig.config.DB_Config[0].TextSeparator_Tisk_Vaha;
                    tb_DB_Con_TextSeparator_Tisk.Text = AgroSledovaniVozikuConfig.config.DB_Config[0].TextSeparator_Tisk;

                    #endregion

                    #region ADAM1

                    tb_ADAM1_PortP2P.Text = AgroSledovaniVozikuConfig.config.ADAM_1[0].PortP2PAdam.ToString();
                    tb_ADAM1_IPAdresa.Text = AgroSledovaniVozikuConfig.config.ADAM_1[0].IPAdresaAdam;
                    tb_ADAM1_TimerPeriodSensorsCheckAdam.Text = AgroSledovaniVozikuConfig.config.ADAM_1[0].TimerPeriodSensorsCheckAdam.ToString();
                    tb_ADAM1_AdamTCPTimeout.Text = AgroSledovaniVozikuConfig.config.ADAM_1[0].AdamTCPTimeout.ToString();
                    tb_ADAM1_MinHighSignalWidthAdam.Text = AgroSledovaniVozikuConfig.config.ADAM_1[0].MinHighSignalWidthAdam.ToString();
                    chb_ADAM1_Connect.Checked = AgroSledovaniVozikuConfig.config.ADAM_1[0].Connect;

                    #endregion

                    #region ADAM2

                    tb_ADAM2_PortP2P.Text = AgroSledovaniVozikuConfig.config.ADAM_2[0].PortP2PAdam.ToString();
                    tb_ADAM2_IPAdresa.Text = AgroSledovaniVozikuConfig.config.ADAM_2[0].IPAdresaAdam;
                    tb_ADAM2_TimerPeriodSensorsCheckAdam.Text = AgroSledovaniVozikuConfig.config.ADAM_2[0].TimerPeriodSensorsCheckAdam.ToString();
                    tb_ADAM2_AdamTCPTimeout.Text = AgroSledovaniVozikuConfig.config.ADAM_2[0].AdamTCPTimeout.ToString();
                    tb_ADAM2_MinHighSignalWidthAdam.Text = AgroSledovaniVozikuConfig.config.ADAM_2[0].MinHighSignalWidthAdam.ToString();
                    chb_ADAM2_Connect.Checked = AgroSledovaniVozikuConfig.config.ADAM_2[0].Connect;

                    #endregion

                    #region ADAM DI 1


                    tb_ADAM1_Linka1_RUN.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_RUN.ToString();
                    tb_ADAM1_RucnyVstup_OK.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_OK.ToString();
   
                    #endregion

                    #region ADAM DI 2

                    tb_ADAM2_Strec1_OK.Text = AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Strec1_OK.ToString();
                    tb_ADAM2_Vaha_START.Text = AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Bocedi1_OUT.ToString();
                    tb_ADAM2_Vaha_END.Text = AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Vaha1_END.ToString();
                    tb_ADAM2_Tisk_END.Text = AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Tisk1_END.ToString();

                    #endregion

                    #region CS

                    tb_DB_Local.Text = AgroSledovaniVozikuConfig.config.CS[0].DB_Local;
                    tb_DB_Remote.Text = AgroSledovaniVozikuConfig.config.CS[0].DB_Remote;

                    #endregion

                    #region WEB API TISK

                    tb_APITISK_Autorizace_DoAPI.Text = AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].Autorizace_DoAPI;
                    tb_APITISK_Adresa.Text = AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].Adresa;
                    tb_APITISK_AliasDB.Text = AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].AliasDB;
                    chb_APITISK_isHTTPS.Checked = AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].isHTTPS;
                    tb_APITISK_API_TimeOut.Text = AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].API_TimeOut.ToString();                   
                    chb_APITISK_Enable.Checked = AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].Enable;

                    #endregion

                }
                catch (Exception ex)
                {
                    ExceptionHandler2.Handle(ex);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

        }

        #region ADAM reakce na udalosti

        private void AdamStart()
        {
            try
            {
                if (adam_1 != null)
                {
                    adam_1.DataReady_NabeznaHrana -= new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_1);
                    adam_1.DataReady_NabeznaHrana += new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_1);

                    adam_1.Start();
                }

                if (adam_2 != null)
                {
                    adam_2.DataReady_NabeznaHrana -= new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_2);
                    adam_2.DataReady_NabeznaHrana += new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_2);
                   
                    adam_2.Start();
                }

            }
            catch(Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }

        private void AdamStop()
        {
            try
            {
                if (adam_1 != null)
                {
                    adam_1.DataReady_NabeznaHrana -= new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_1);
                    adam_1.Stop();
                }

                if (adam_2 != null)
                {
                    adam_2.DataReady_NabeznaHrana -= new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_2);
                    adam_2.Stop();
                }

            }
            catch(Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }

        private void Adam_DataReady_1(ADAM.ADAM_60XX.AdamEventHandlerArgs e)
        {
            try
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady_1), new object[] { e.Data });
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }

        private void Adam_DataReady_2(ADAM.ADAM_60XX.AdamEventHandlerArgs e)
        {
            try
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady_2), new object[] { e.Data });
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }

        /// <summary>
        /// Jedna se o adama na Voziku
        /// </summary>
        /// <param name="sensor"></param>
        private void AdamSensorDataReady_1(int sensor)
        {

            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "AdamSensorDataReady_1");
            Fask.Tracing.Trac.Write("START metody AdamSensorDataReady_1", tracId);

            #region ADAM Events _DI
            try
            {
                if (sensor == AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_RUN)
                {

                    Fask.Tracing.Trac.Write("START sensor == Linka1_RUN", tracId);


                    log_hlaska = string.Format("ADAM_1 - Linka1_RUN");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    //plneni interni promenne linky
                    if (interniCisloLinka == 0)
                    {
                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = Fask.Constants.AGRO.linka_4_R;
                        }
                    }
                    else
                    {

                        log_hlaska = string.Format("NAKLADKA -- NEVYLOŽENO!! Z linky: {0} ", interniCisloLinka);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        log_hlaska = string.Format("NAKLADKA_INFO_Linka_[{0}] nebyla vykladka? neni vycistena interniCisloLinka = ({1})!!", 1, interniCisloLinka);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = Fask.Constants.AGRO.linka_4_R;
                        }
                    }

                    Fask.Tracing.Trac.Write("END sensor == Linka1_RUN", tracId);
                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_OK)
                {
                    Fask.Tracing.Trac.Write("START sensor == RucniVstup_OK", tracId);

                    log_hlaska = string.Format("ADAM_1 - RucniVstup_OK");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


                    if (interniCisloLinka == 0)
                    {
                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = Fask.Constants.AGRO.rucni_vstup_R; 
                        }
                    }
                    else
                    {
                        log_hlaska = string.Format("NAKLADKA -- NEVYLOŽENO!! Z linky: {0} ", interniCisloLinka);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        log_hlaska = string.Format("NAKLADKA_INFO_Linka_[{0}] neni vycistena interniCisloLinka = ({1})!!", Fask.Constants.AGRO.rucni_vstup_R,interniCisloLinka);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = Fask.Constants.AGRO.rucni_vstup_R; 
                        }
                    }

                    //OnRFIDEvent(new RFID_EventArgs(6));

                    Fask.Tracing.Trac.Write("END sensor == RucniVstup_OK", tracId);
                }
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }

            #endregion

            Fask.Tracing.Trac.Write("END metody AdamSensorDataReady_1", tracId);


        }

        /// <summary>
        /// Jedná se o adama na Bocedi 1
        /// </summary>
        /// <param name="sensor"></param>
        private void AdamSensorDataReady_2(int sensor)
        {
            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null,null,null, "AdamSensorDataReady_2");
            Fask.Tracing.Trac.Write("START metody AdamSensorDataReady_2", tracId);

            #region ADAM Events _OK

            try
            {
                //zjistuji jestli je vaha v erroru ---START---
                try
                {
                    if (AgroSledovaniVozikuConfig.config.ADAM_2[0].communication_TCP)
                    {
                        bool[] vaha1_ERROR_pole = adam_2.getDIStatus(); // z nejakeho duvodu pri drzeni DI_ERROR na true a zaroven prichoziho signalu z jineho pinu se vyhodnoti DI_ERROR na false!!! POZOR!!
                        if (vaha1_ERROR_pole != null)
                        {
                            vaha_1_ERROR = vaha1_ERROR_pole[AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Vaha1_ERROR];
                        }
                    }
                    else if (AgroSledovaniVozikuConfig.config.ADAM_2[0].communication_P2P)
                    {
                        if (adam_2.actualState_DI != null && adam_2.actualState_DI.Length >= AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Vaha1_ERROR)
                        {
                            bool[] vaha1_ERROR_pole = adam_2.actualState_DI;
                            if (vaha1_ERROR_pole != null)
                            {
                                vaha_1_ERROR = vaha1_ERROR_pole[AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Vaha1_ERROR];
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler2.Handle(ex);
                }
                //zjistuji jestli je vaha v erroru ---END---

                if (sensor == AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Strec1_OK)
                {

                    Fask.Tracing.Trac.Write("START sensor == Strec1_OK", tracId);

                    log_hlaska = string.Format("ADAM_2 - Strec1_OK");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


                    lock (OnlineLog_Bocedi_1_object)
                    {
                        OnlineLog_Bocedi_1 = false;
                    }

                    timer_strec_1.Change(AgroSledovaniVozikuConfig.config.Vykladka[0].TimeEventStrec, System.Threading.Timeout.Infinite);

                    Fask.Tracing.Trac.Write("END sensor == Strec1_OK", tracId);

                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Bocedi1_OUT)
                {

                    Fask.Tracing.Trac.Write("START sensor == Bocedi1_OUT", tracId);

                    log_hlaska = string.Format("ADAM_2 - Bocedi1_OUT");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


                    if (!uC_Vaha.Vaha_1_Connected() || vaha_1_ERROR)
                    {

                        log_hlaska = string.Format("Bocedi1_OUT - start logiky tisk status 21");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        OnLogikaZAdamaEvent(new LogikaZAdama_EventArgs(Fask.Constants.AGRO.status_21));
                        OnLogikaZAdamaEvent(new LogikaZAdama_EventArgs(Fask.Constants.AGRO.status_41));
                    }


                    Fask.Tracing.Trac.Write("END sensor == Bocedi1_OUT", tracId);
                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Vaha1_OK)
                {
                    Fask.Tracing.Trac.Write("START sensor == Vaha1_OK", tracId);


                    log_hlaska = string.Format("ADAM_2 - Vaha1_OK");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    lock (vaha_1_data_object)
                    {
                        vaha_1_data = true;
                    }

                    timer_vaha_1.Change(AgroSledovaniVozikuConfig.config.Vykladka[0].Time_Vaha1, System.Threading.Timeout.Infinite);
                    Fask.Tracing.Trac.Write("END sensor == Vaha1_OK", tracId);
                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Vaha1_END)
                {
                    Fask.Tracing.Trac.Write("START sensor == Vaha1_END", tracId);


                    log_hlaska = string.Format("ADAM_2 - Vaha1_END");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    if (vaha_1_data && !vaha_1_ERROR)
                    {


                        log_hlaska = string.Format("Vaha1_END - start logiky tisk status 31");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


                        OnLogikaZAdamaEvent(new LogikaZAdama_EventArgs(Fask.Constants.AGRO.status_31));
                        OnLogikaZAdamaEvent(new LogikaZAdama_EventArgs(Fask.Constants.AGRO.status_41));
                    }

                    Fask.Tracing.Trac.Write("END sensor == Vaha1_END", tracId);

                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Tisk1_END)
                {
                    Fask.Tracing.Trac.Write("START sensor == Tisk1_END", tracId);


                    log_hlaska = string.Format("ADAM_2 - Tisk1_END");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    OnLogikaZAdamaEvent(new LogikaZAdama_EventArgs(Fask.Constants.AGRO.status_61));

                    Fask.Tracing.Trac.Write("END sensor == Tisk1_END", tracId);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

            #endregion
            Fask.Tracing.Trac.Write("END metody AdamSensorDataReady_2", tracId);

        }

        #endregion

        #region UpdateForm

        private void updateForm(bool input, string source)
        {
            try
            {
                if (this.InvokeRequired)
                { 
                    this.BeginInvoke(new MethodInvoker(() => { updateForm(input, source); }));
                    return;
                }

                #region UC inicializace + deklarace ADAM DI 

                bool Linka1_RUN = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_RUN, vyber_adam_1);
                bool RucniVstup_OK = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_OK, vyber_adam_1);

                #endregion

                if (adam_1 != null)
                {
                    if (adam_1.AdamTCP_Connected)
                        aDAM_UC_1._adamTCP_Connected = true;
                    else
                        aDAM_UC_1._adamTCP_Connected = false;
                    

                    if (adam_1.AdamP2P_Started)
                        aDAM_UC_1._adamP2P_Started = true;
                    else
                        aDAM_UC_1._adamP2P_Started = false;
                    

                    if (AgroSledovaniVozikuConfig.config.ADAM_1[0].communication_TCP)
                        aDAM_UC_1.DIData = adam_1.DIStatusLast;
                    else if (AgroSledovaniVozikuConfig.config.ADAM_1[0].communication_P2P)
                    {
                        if (adam_1.actualState_DI != null)
                            aDAM_UC_1.DIData = adam_1.actualState_DI;
                    }

                    aDAM_UC_1.updateForm();

                }

                if (adam_2 != null)
                {
                    if (adam_2.AdamTCP_Connected)
                        aDAM_UC_2._adamTCP_Connected = true;
                    else
                        aDAM_UC_2._adamTCP_Connected = false;
                    if (adam_2.AdamP2P_Started)
                        aDAM_UC_2._adamP2P_Started = true;
                    else
                        aDAM_UC_2._adamP2P_Started = false;

                    if (AgroSledovaniVozikuConfig.config.ADAM_2[0].communication_TCP)
                        aDAM_UC_2.DIData = adam_2.DIStatusLast;
                    
                    else if (AgroSledovaniVozikuConfig.config.ADAM_2[0].communication_P2P)
                    {
                        if (adam_2.actualState_DI != null)
                            aDAM_UC_2.DIData = adam_2.actualState_DI;
                    }
                 
                    aDAM_UC_2.updateForm();

                }

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

        private bool GetAdamDI(int pozice, string vyberAdam)
        {
            try
            {
                if (pozice == -1)
                {
                    return false;
                }
                else
                {
                    if (vyberAdam == vyber_adam_1 && adam_1 != null)
                    {
                        if (adam_1.DIStatusLast.Length - 1 < pozice)
                            return false;
                        else
                            return adam_1.DIStatusLast[pozice];
                    }
                    else if (vyberAdam == vyber_adam_2 && adam_2 != null)
                    {
                        if (adam_2.DIStatusLast.Length - 1 < pozice)
                            return false;
                        else
                            return adam_2.DIStatusLast[pozice];
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        #region Update formu v timeru

        private void TimerRefreshUIOff()
        {
            if (timerRefreshUI != null)
                timerRefreshUI.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        private void TimerRefreshUIOn()
        {
            if (timerRefreshUI != null)
                timerRefreshUI.Change(500, System.Threading.Timeout.Infinite);
        }

        private void TimerRefreshUICallback(object state)
        {
            System.Threading.Thread.CurrentThread.Name = "TimerRefreshUI " + DateTime.Now.ToString();

            this.RefreshUI();
        }

        private void RefreshUI()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.RefreshUI();
                });
                return;
            }

            try
            {
                TimerRefreshUIOff();
                updateForm(false, "timer");
            }
            finally
            {
                TimerRefreshUIOn();
            }
        }

        #endregion

        #region Konfigurace

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Configuration.AgroSledovaniVozikuConfig.config.HesloDoKonfigurace[0].IsPasswordNull() && !String.IsNullOrEmpty(Configuration.AgroSledovaniVozikuConfig.config.HesloDoKonfigurace[0].Password))
                {
                    if (Configuration.AgroSledovaniVozikuConfig.config.HesloDoKonfigurace[0].Password != Convert.ToBase64String(Encoding.Default.GetBytes(txtPassword.Text), Base64FormattingOptions.None))
                        return;
                }

                groupBox_ADAM1.Enabled =
                    groupBox_ADAM1_DI.Enabled =
                    groupBox_ADAM2.Enabled =
                    groupBox_ADAM2_DI.Enabled =
                    groupBox_CS.Enabled =
                    groupBox_DB_Config.Enabled =
                    groupBox_WEB_API_TISK.Enabled =
                buttonUlozitNastaveni.Enabled = true;
            }
            catch (Exception ex)
            {
                DialogResult dialogResult_value;
                ExceptionHandler2.Handle(ex, true, MessageBoxButtons.OK, MessageBoxIcon.Information, out dialogResult_value);
            }
        }

        private void button_SaveConfig_Click(object sender, EventArgs e)
        {
            AgroSledovaniVozikuConfig.config.HesloDoKonfigurace[0].Password = Convert.ToBase64String(Encoding.Default.GetBytes(txtPassword.Text), Base64FormattingOptions.None);

            try
            {
                #region DB_Config

                AgroSledovaniVozikuConfig.config.DB_Config[0].Separator = tb_DB_Con_Separator.Text;
                AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchodu = int.Parse(tb_DB_Con_PocetPruchodu.Text); 
                AgroSledovaniVozikuConfig.config.DB_Config[0].TimeSleep = int.Parse(tb_DB_Con_TimeSleep.Text);
                AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec = tb_DB_Con_Separator_strec.Text;
                AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchoduNakladka = int.Parse(tb_DB_Con_PocetPruchoduNakladka.Text);
                AgroSledovaniVozikuConfig.config.DB_Config[0].PocetPruchoduTisk = int.Parse(tb_DB_Con_PocetPruchoduTisk.Text);
                AgroSledovaniVozikuConfig.config.DB_Config[0].TextSeparator_Tisk_Vaha = tb_DB_Con_TextSeparator_Tisk_Vaha.Text;
                AgroSledovaniVozikuConfig.config.DB_Config[0].TextSeparator_Tisk = tb_DB_Con_TextSeparator_Tisk.Text;

                #endregion

                #region ADAM1

                AgroSledovaniVozikuConfig.config.ADAM_1[0].PortP2PAdam = int.Parse(tb_ADAM1_PortP2P.Text);
                AgroSledovaniVozikuConfig.config.ADAM_1[0].IPAdresaAdam = tb_ADAM1_IPAdresa.Text;
                AgroSledovaniVozikuConfig.config.ADAM_1[0].TimerPeriodSensorsCheckAdam = int.Parse(tb_ADAM1_TimerPeriodSensorsCheckAdam.Text);
                AgroSledovaniVozikuConfig.config.ADAM_1[0].AdamTCPTimeout = int.Parse(tb_ADAM1_AdamTCPTimeout.Text);
                AgroSledovaniVozikuConfig.config.ADAM_1[0].MinHighSignalWidthAdam = int.Parse(tb_ADAM1_MinHighSignalWidthAdam.Text);
                AgroSledovaniVozikuConfig.config.ADAM_1[0].Connect = chb_ADAM1_Connect.Checked;

                #endregion

                #region ADAM2

                AgroSledovaniVozikuConfig.config.ADAM_2[0].PortP2PAdam = int.Parse(tb_ADAM2_PortP2P.Text);
                AgroSledovaniVozikuConfig.config.ADAM_2[0].IPAdresaAdam = tb_ADAM2_IPAdresa.Text;
                AgroSledovaniVozikuConfig.config.ADAM_2[0].TimerPeriodSensorsCheckAdam = int.Parse(tb_ADAM2_TimerPeriodSensorsCheckAdam.Text);
                AgroSledovaniVozikuConfig.config.ADAM_2[0].AdamTCPTimeout = int.Parse(tb_ADAM2_AdamTCPTimeout.Text);
                AgroSledovaniVozikuConfig.config.ADAM_2[0].MinHighSignalWidthAdam = int.Parse(tb_ADAM2_MinHighSignalWidthAdam.Text);
                AgroSledovaniVozikuConfig.config.ADAM_2[0].Connect = chb_ADAM2_Connect.Checked;

                #endregion

                #region ADAM DI 1

                AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_RUN = int.Parse(tb_ADAM1_Linka1_RUN.Text);
                AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_OK = int.Parse(tb_ADAM1_RucnyVstup_OK.Text);


                #endregion

                #region ADAM DI 2

                AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Strec1_OK = int.Parse(tb_ADAM2_Strec1_OK.Text);
                AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Bocedi1_OUT = int.Parse(tb_ADAM2_Vaha_START.Text);
                AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Vaha1_END = int.Parse(tb_ADAM2_Vaha_END.Text);
                AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Tisk1_END = int.Parse(tb_ADAM2_Tisk_END.Text);

                #endregion

                #region CS

                AgroSledovaniVozikuConfig.config.CS[0].DB_Local = tb_DB_Local.Text;
                AgroSledovaniVozikuConfig.config.CS[0].DB_Remote = tb_DB_Remote.Text;

                #endregion

                #region WEB API TISK

                AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].Autorizace_DoAPI = tb_APITISK_Autorizace_DoAPI.Text;
                AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].Adresa = tb_APITISK_Adresa.Text;
                AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].AliasDB = tb_APITISK_AliasDB.Text;
                AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].isHTTPS = chb_APITISK_isHTTPS.Checked;
                AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].API_TimeOut = int.Parse(tb_APITISK_API_TimeOut.Text);
                AgroSledovaniVozikuConfig.config.WEBAPI_TISK[0].Enable = chb_APITISK_Enable.Checked;

                #endregion

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

            ////Ulozeni do xml
            try
            {
                AgroSledovaniVozikuConfig.SaveConfiguration();

                groupBox_ADAM1.Enabled =
                    groupBox_ADAM1_DI.Enabled =
                    groupBox_ADAM2.Enabled =
                    groupBox_ADAM2_DI.Enabled =
                    groupBox_CS.Enabled =
                    groupBox_DB_Config.Enabled =
                    groupBox_WEB_API_TISK.Enabled =
                buttonUlozitNastaveni.Enabled = false;

                txtPassword.Text = string.Empty;
            }
            catch (Exception ex)
            {
                DialogResult dialogResult_value;
                ExceptionHandler2.Handle(ex, true, MessageBoxButtons.OK, MessageBoxIcon.Information, out dialogResult_value);
            }
        }

        #endregion

        #region Buttons click

        private void bt_Odhlasit_Click(object sender, EventArgs e)
        {
            //zobrazit upozorneni??
            //logika status 0, zbyva x zaznamu??
            // ok -> archivace na status 200 ?? zaznamenani archivace operatorem status 300?

            log_hlaska = string.Format("Odhlaseni z modulu, uzivatel: {0}", Rodic.uzivatel);
            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

            _openFlag = false;
            Rodic.Show();
            this.Close();
            Rodic.SetMain();
        }

        #region Archivace

        private void bt_archivace_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button)
                {
                    Button btn = sender as Button;
                    if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Linka4_R)
                    {
                        Archivace(
                            Fask.Constants.AGRO.linka_4_R,
                            Fask.Constants.AGRO.status_0,
                            AgroSledovaniVozikuConfig.config.DB_Config[0].Separator,
                            Fask.Constants.AGRO.BTN_Linka4_R
                            );
                    }
                    else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_RV_R)
                    {
                        Archivace(Fask.Constants.AGRO.rucni_vstup_R,
                            Fask.Constants.AGRO.status_0,
                            AgroSledovaniVozikuConfig.config.DB_Config[0].Separator,
                            Fask.Constants.AGRO.BTN_RV_R
                            );
                    }
                    else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Bocedi3_R)
                    {
                        Archivace(-1,
                            Fask.Constants.AGRO.status_21,
                            AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec,
                            Fask.Constants.AGRO.BTN_Bocedi3_R);
                    }
                }
            }
            catch (Exception ex)
            {

                //throw ex;
                ExceptionHandler2.Handle(ex);
            }
        }

        private void Archivace(
            int CisloLinky,
            int Status,
            string Separator,
            string text)
        {

            var p = new Forms.ParametryProArchivaci() 
            { 
                CisloLinky = CisloLinky,
                Separator = Separator,
                Status = Status
            };

            using (Forms.frmArchivaceZaznamu frm = new Forms.frmArchivaceZaznamu(p, text))
            {
                frm.WindowState = FormWindowState.Maximized;
                var dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    return;
                }
            }
        }

        #endregion

        #region Verze

        private void btn_verze_Click(object sender, EventArgs e)
        {
            try
            {
                using (Forms.AboutBox abox = new Forms.AboutBox())
                {
                    abox.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion 

        #endregion

        private void panel1_Resize(object sender, EventArgs e)
        {
            int W = panel1.Width;
            panel_Bocedi.Size = new Size(W/2,panel1.Height);

           int H =  panel_Bocedi.Height - 35;

            bt_bocedi1.Size = new Size(panel1.Width,H);

            bt_Linka_1.Size = new Size(panel1.Width, H / 2);
            bt_RV.Size = new Size(panel1.Width, H / 2);
        }

    private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
    {
            #if DEBUG
            if (e.Handled)
                return;

            if (e.KeyChar == '-')
            {
                statusLabel.Text = string.Empty;
            }
            if (e.KeyChar == '1')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady_1), new object[] { 1 }); 
            }
            else if (e.KeyChar == '2')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady_1), new object[] { 2 }); 
            }
            else if (e.KeyChar == '3')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady_2), new object[] { 3 }); 
            }
            else if (e.KeyChar == '4')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady_2), new object[] { 4 });
            }
            else if (e.KeyChar == '5')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady_2), new object[] { 5 });
            }
            else if (e.KeyChar == '6')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady_2), new object[] { 6 });
            }
            else if (e.KeyChar == '7')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady_2), new object[] { 7 });
            }
            else if (e.KeyChar == '8')
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady_2), new object[] { 8 });
            }
            else
            {
                return;
            }

            statusLabel.Text += e.KeyChar;

            #endif
        }

    }

    #region Pomocne tridy jak EventArgs

    public class LogikaZAdama_EventArgs : EventArgs
    {
        public int CisloStatus { get; set; }

        public LogikaZAdama_EventArgs(int cl)
        {
            CisloStatus = cl;
        }
    }

    #endregion

}
