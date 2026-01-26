using FASK.SledovaniVyroby.ModuleIfc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Configuration;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Vozikova_matice;
using FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Classes;
using FASK.SledovaniVyroby.IRFIDProvider;
using System.Threading;
using Fask.Logging;
using static FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Classes.Logika_Voziky;
using Fask.Constants;
using FASK.SledovaniVyroby.Main.Configuration;
using System.Reflection;
using System.IO;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku
{
    public partial class frmMain : Form, IModuleConnector
    {
        #region drzeni informaci o prihlasenem uzivateli
        //const ??
        public int LoginID = -1;
        public object LoginID_object = new object();
        #endregion


        #region promenne interni buffer Nakladka/Vykladka
        public Queue<Fask.WEBAPI.API_BusinessObjects.FE_pom> Vykladka_Boccedi1_Buffer = new Queue<Fask.WEBAPI.API_BusinessObjects.FE_pom>();
        public Queue<Fask.WEBAPI.API_BusinessObjects.FE_pom> Vykladka_Boccedi2_Buffer = new Queue<Fask.WEBAPI.API_BusinessObjects.FE_pom>();
        public Queue<Fask.WEBAPI.API_BusinessObjects.FE_pom> Nakladka_Buffer = new Queue<Fask.WEBAPI.API_BusinessObjects.FE_pom>();
#if false
        public FE_pom Nakladka_data = null;
        public object Nakladka_data_object = new object(); //locker pro Nakladka_data 
#endif

#endregion

        #region promenne online log
        public bool OnlineLog_Bocedi_1 = false;
        public object OnlineLog_Bocedi_1_object = new object();

        public bool OnlineLog_Bocedi_2 = false;
        public object OnlineLog_Bocedi_2_object = new object();


#endregion

        public Forms.FormIDPracovnikaLogin Rodic = null;

        public string log_hlaska = string.Empty;
        

        private bool _openFlag = false;
        public bool isOpen()
        {
            return _openFlag;
        }

        private System.Threading.Timer timer_strec_1;
        private System.Threading.Timer timer_strec_2;

        public string Const_Zapis_Linky = "Zapis_Linky";
        public string Const_Zapis_Odvadeni = "Zapis_Odvadeni";
        public string Const_Kontrola_Linky = "Kontrola_Linky";
        public string Const_Kontrola_Odvadeni = "Kontrola_Odvadeni";



        private System.Threading.Timer timer_vaha_1;
        private System.Threading.Timer timer_vaha_2;
        //public bool vaha_1;
        //public bool vaha_2;
        public bool vaha_1_data = false;
        public bool vaha_2_data = false;
        public bool vaha_1_ERROR = false;
        public bool vaha_2_ERROR = false;
        public object vaha_1_data_object = new object();
        public object vaha_2_data_object = new object();


        private ADAM.ADAM_60XX adam_1 = null;
        private ADAM.ADAM_60XX adam_2 = null;
        private ADAM.ADAM_60XX adam_3 = null;

        private const string vyber_adam_1 = "adam_1";
        private const string vyber_adam_2 = "adam_2";
        private const string vyber_adam_3 = "adam_3";

        private System.Threading.Timer timer_adam_1;
        private System.Threading.Timer timer_adam_2;
        private System.Threading.Timer timer_adam_3;

        private bool ADAM1_signal = false;
        private bool ADAM2_signal = false;
        private bool ADAM3_signal = false;

        private System.Threading.Timer timerRefreshUI = null;

        IRFIDProvider.IRFIDProvider RFID_Linky_IN = null;
        IRFIDProvider.IRFIDProvider RFID_Linky_OUT = null;

        //IRFIDProvider.IRFIDProvider RFID_Linky_default = null;

        delegate void AdamSensorDelegate(int e);

        private Classes.Logika_Voziky LV;

        private Vozikova_matice.VM_logika VM;

       // private string SSCC_pom;
       // private ICommDatabase.DSVyroba.FASK_MachinesRow pozice_vozik;
        private bool SQL ;

        #region RFID promenne
        private const string NenactenoEPC = "nenačten";
        public bool validace_RFID_LED = false;
        public object validace_RFID_LED_object = new object();
        public int RFID_cislo_linky = -1;
     
        #endregion




        TridaDataProKresleni trida = null;
        TridaDataProKresleni_vykladka trida_vykladka = null;

        public int _interniCisloLinka = 0; //vyjadruje cislo linky ze ktere vyjela paleta, ktera je nalozena na voziku aktualne
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

        public int interniCisloLinka_matice = 0; //vyjadruje cislo linky ze ktere vyjela paleta, ktera je nalozena na voziku aktualne
        public object interniCisloLinka_matice_object = new object();

        //public bool var_L2 = false;
        //public bool var_L3 = false;
        //public bool var_L4 = false;

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


        #region Event RFID

        public delegate void RFID_EventHandler(Object sender, RFID_EventArgs e);

        public event RFID_EventHandler rfid_EventHandler;

        private void OnRFIDEvent(RFID_EventArgs e)
        {
            RFID_EventHandler handler = rfid_EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void Show_RFID_LED(int barva)
        {

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    Show_RFID_LED(barva);
                }));

                return;
            }

            if (barva == 1)
            {
                RFID_LED.BackColor = Color.Red;
            }
            else if (barva == 2)
            {
                RFID_LED.BackColor = Color.Green;
            }
            else
            {
                RFID_LED.BackColor = Color.Orange;
            }


        }

        #endregion

        #region Event ADAM

        public delegate void ADAM_EventHandler(Object sender, ADAM_EventArgs e);

        public event ADAM_EventHandler adam_EventHandler;

        private void OnADAMEvent(ADAM_EventArgs e)
        {
            ADAM_EventHandler handler = adam_EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void FrmMain_adam_EventHandler(object sender, ADAM_EventArgs e)
        {
            try
            {
                if (e.CisloAdam == 1)
                {
#if DEBUG
                    //Log.Write(string.Format("ADAM_EVENT adam: {0}", e.CisloAdam));
                    log_hlaska = string.Format("ADAM_EVENT adam: {0}", e.CisloAdam);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    //saveToSQL_adam_1();
                }

                if (e.CisloAdam == 2)
                {
#if DEBUG
                    //Log.Write(string.Format("ADAM_EVENT adam: {0}", e.CisloAdam));
                    log_hlaska = string.Format("ADAM_EVENT adam: {0}", e.CisloAdam);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    //saveToSQL_adam_2();
                }

                if (e.CisloAdam == 3)
                {
#if DEBUG
                    //Log.Write(string.Format("ADAM_EVENT adam: {0}", e.CisloAdam));
                    log_hlaska = string.Format("ADAM_EVENT adam: {0}", e.CisloAdam);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    //saveToSQL_adam_3();
                }

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

#endregion

        #region Event STREC

        public delegate void STREC_sleep_EventHandler(Object sender, STREC_EventArgs e);

        public event STREC_sleep_EventHandler strec_sleep_EventHandler;

        private void OnSTREC_sleep_Event(STREC_EventArgs e)
        {
            STREC_sleep_EventHandler handler = strec_sleep_EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public delegate void STREC_EventHandler(Object sender, STREC_EventArgs e);

        public event STREC_EventHandler strec_EventHandler;

        private void OnSTRECEvent(STREC_EventArgs e)
        {
            STREC_EventHandler handler = strec_EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        private void FrmMain_strec_sleep_EventHandler(object sender, STREC_EventArgs e)
        {
            try
            {

#if DEBUG
                //Log.Write(string.Format("STREC_EVENT_SLEEP číslo linky: {0}", e.CisloStrec));
                log_hlaska = string.Format("STREC_EVENT_SLEEP číslo linky: {0}", e.CisloStrec);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                Thread.Sleep(AgroSledovaniVozikuConfig.config.Vykladka[0].TimeEventStrec);

                
                if (e.CisloStrec == AGRO.bocedi_1 || e.CisloStrec == AGRO.bocedi_2)
                {


                    //vyvolam event pro kontrolu interni promenne
                    //vytvorit metodu, ktera ceka 5s a pak zavola LV.OnVykladkaEvent

                    OnSTRECEvent(new STREC_EventArgs(e.CisloStrec));


                }

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
            }
        }


        private void FrmMain_strec_EventHandler(object sender, STREC_EventArgs e)
        {
            try
            {
                //if(RFID_Linky_IN != null)
                //    LV.RFID_tag_zapis(RFID_Linky_IN, e.CisloLinky);

                if (e.CisloStrec == AGRO.bocedi_1 || e.CisloStrec == AGRO.bocedi_2)
                {

#if DEBUG
                    //Log.Write(string.Format("STREC_EVENT_LOGIKA číslo linky: {0}", e.CisloStrec));
                    log_hlaska = string.Format("STREC_EVENT_LOGIKA číslo linky: {0}", e.CisloStrec);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    //vyvolam event pro kontrolu interni promenne
                    //vytvorit metodu, ktera ceka 5s a pak zavola LV.OnVykladkaEvent
                    if (interniCisloLinka != 0)
                    {
                        //vypnuti cteni z rfid !!
                        if(RFID_Linky_OUT != null)
                        RFID_Linky_OUT.Stop_Read_tags();

                        LV.OnVykladkaEvent(new Vykladka_EventArgs(interniCisloLinka, e.CisloStrec));

                     

                    }

                }

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

#endregion

        #region Eventy formu

        public frmMain()
        {
            InitializeComponent();

            try
            {
#region Nastaveni cesty pro logovani
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                ExceptionHandler2.SetPath(startupPath); 
#endregion

                //zmeneno na false, aby se neukladalo
               // Fask.Tracing.Trac.Enable = true;
                Fask.Tracing.Trac.Enable = Config.config.Logging[0].Enable_Trace;


                InitUserInfo();
                //Log.Write("Spusteni modulu");
                ExceptionHandler2.Handle("Spusteni modulu", "Log_LV", "txt");



                LoadConfiguration();

                //#region vycteni verze knihovny RFID a porovnani
                //string PathToFile;
                //try
                //{
                //    PathToFile = Path.GetFullPath("Symbol.RFID3.Host");
                //    PathToFile += ".dll";

                //    // Get current assemblies
                //    AssemblyName currentAssemblyName = AssemblyName.GetAssemblyName(PathToFile);
                //    string verze = currentAssemblyName.Version.ToString();

                //    // Compare both versions
                //    if (verze == "1.5.6.2")
                //    {
                //        // verze knihovny je spravna, melo by jet RFID!

                //    }
                //    else
                //    {
                //        MessageBox.Show("Špatná verze knihovny Symbol.RFID3.Host!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                      
                //        this.Close();
                //    }


                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Špatná verze knihovny Symbol.RFID3.Host!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    ExceptionHandler2.Handle(ex);
                //    this.Close();

                //}


                //#endregion

                //SQL = AgroSledovaniVozikuConfig.config.WEBAPI[0].Enable;

                #region ADAM inicializace
                #region ADAM_1

                if (AgroSledovaniVozikuConfig.config.ADAM_1[0].Connect)
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
                    //AdamStart();
                    aDAM_UC_2.Gb_value = "ADAM Strech_1";
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
                    //testovaci ADAM FASK
                    //aDAM_UC_2.Adam_Test = true;

                }
#endregion

#region ADAM_3
                if (AgroSledovaniVozikuConfig.config.ADAM_3[0].Connect)
                {
                    var cfgPortP2PAdam_3 = AgroSledovaniVozikuConfig.config.ADAM_3[0].IsPortP2PAdamNull() ? 1025 : AgroSledovaniVozikuConfig.config.ADAM_3[0].PortP2PAdam;

                    adam_3 = new ADAM.ADAM_60XX(
                        AgroSledovaniVozikuConfig.config.ADAM_3[0].IPAdresaAdam,
                        cfgPortP2PAdam_3,    //bude vkladano rucne IP a 1025 default
                        AgroSledovaniVozikuConfig.config.ADAM_3[0].TimerPeriodSensorsCheckAdam,
                        AgroSledovaniVozikuConfig.config.ADAM_3[0].AdamTCPTimeout,
                        AgroSledovaniVozikuConfig.config.ADAM_3[0].MinHighSignalWidthAdam,
                        AgroSledovaniVozikuConfig.config.ADAM_3[0].communication_TCP,
                        AgroSledovaniVozikuConfig.config.ADAM_3[0].communication_P2P
                        );
                    //AdamStart();
                    aDAM_UC_3.Gb_value = "ADAM Strech_2";
                    if (AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].enabled)
                    {
                        aDAM_UC_3.DI0_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI0;
                        aDAM_UC_3.DI1_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI1;
                        aDAM_UC_3.DI2_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI2;
                        aDAM_UC_3.DI3_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI3;
                        aDAM_UC_3.DI4_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI4;
                        aDAM_UC_3.DI5_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI5;
                        aDAM_UC_3.DI6_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI6;
                        aDAM_UC_3.DI7_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI7;
                        aDAM_UC_3.DI8_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI8;
                        aDAM_UC_3.DI9_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI9;
                        aDAM_UC_3.DI10_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI10;
                        aDAM_UC_3.DI11_popis = AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION[0].DI11; 
                    }
                }
#endregion
                AdamStart();
#endregion

#region inicializace RFID
                if (AgroSledovaniVozikuConfig.config.RFID_1[0].Enable)
                {
                    RFID_Linky_IN = RFIDFactory.RFIDFactory.Init();
                    RFID_Linky_IN.Count_Write_Pruchody = AgroSledovaniVozikuConfig.config.RFID_1[0].PocetPruchodu;
                    RFID_Linky_IN.DataReadyZEBRA += RFID_Linky_DataReadyZEBRA;
                    RFID_Linky_IN.init(AgroSledovaniVozikuConfig.config.RFID_1[0].IP_Adresa, AgroSledovaniVozikuConfig.config.RFID_1[0].Port);
                    RFID_Linky_IN.Start();
                }
                if (AgroSledovaniVozikuConfig.config.RFID_2[0].Enable)
                {
                    RFID_Linky_OUT = RFIDFactory.RFIDFactory.Init();
                    RFID_Linky_OUT.Count_Write_Pruchody = AgroSledovaniVozikuConfig.config.RFID_2[0].PocetPruchodu;
                    RFID_Linky_OUT.DataReadyZEBRA += RFID_Linky_DataReadyZEBRA;
                    RFID_Linky_OUT.init(AgroSledovaniVozikuConfig.config.RFID_2[0].IP_Adresa, AgroSledovaniVozikuConfig.config.RFID_2[0].Port);
                    RFID_Linky_OUT.Start();
                }
#endregion

                timerRefreshUI = new System.Threading.Timer(TimerRefreshUICallback, null, 100, -1);

                txB_EPC_cil.Text = "99999999999999999999";
                txB_EPC_zdroj.Text = "00301234560000000088";

                LV = new Classes.Logika_Voziky(this);
                VM = new VM_logika();
                trida = new TridaDataProKresleni();
                trida_vykladka = new TridaDataProKresleni_vykladka();

#region RFID inicializace User Control
                rfiD_UC1.Tag_Text_R = NenactenoEPC;
                rfiD_UC1.Tag_Text_W = NenactenoEPC;
                RFID_LED.Enabled = false;
                RFID_LED.BackColor = Color.Yellow;

                #endregion
                lbl_InterniCisloLinky.Text = "00";

                //vaha_1 = 
                //vaha_2 = uC_Vaha.Vaha_2_connected();

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            #region generovani SSCC --TEST
            ////-------------------vzdaleny debug TEST---------------------------------------
            //FASK.Palety_SSCC.SQLite.Classes.SSCC_Generator generatorSSCC = new Palety_SSCC.SQLite.Classes.SSCC_Generator();
            //string sscc_FP_1 = generatorSSCC.Get_Next_SSCC(4, 1, 4);
            //MessageBox.Show(sscc_FP_1, "generované sscc -TEST-", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////-----testy end----------------------- 
            #endregion



            panel1_Resize(null,null);

            
            bt_Linka_1.Text = Fask.Constants.AGRO.BTN_Linka1;
            bt_Linka_2.Text = Fask.Constants.AGRO.BTN_Linka2;
            bt_Linka_3.Text = Fask.Constants.AGRO.BTN_Linka3;
            bt_RV.Text = Fask.Constants.AGRO.BTN_RV;
            bt_bocedi2.Text = Fask.Constants.AGRO.BTN_Bocedi2;
            bt_bocedi1.Text = Fask.Constants.AGRO.BTN_Bocedi1;

            _openFlag = true;
            this.WindowState = FormWindowState.Maximized;
            timer_strec_1 = new System.Threading.Timer(CallBack_Timer_Strec1, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            timer_strec_2 = new System.Threading.Timer(CallBack_Timer_Strec2, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            timer_vaha_1 = new System.Threading.Timer(CallBack_Timer_Vaha1, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            timer_vaha_2 = new System.Threading.Timer(CallBack_Timer_Vaha2, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            timer_adam_1 = new System.Threading.Timer(CallBack_Timer_Adam1, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            timer_adam_2 = new System.Threading.Timer(CallBack_Timer_Adam2, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            timer_adam_3 = new System.Threading.Timer(CallBack_Timer_Adam3, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            timer_adam_1.Change(AgroSledovaniVozikuConfig.config.Vykladka[0].Time_ADAM1, System.Threading.Timeout.Infinite);
            timer_adam_2.Change(AgroSledovaniVozikuConfig.config.Vykladka[0].Time_ADAM2, System.Threading.Timeout.Infinite);
            timer_adam_3.Change(AgroSledovaniVozikuConfig.config.Vykladka[0].Time_ADAM3, System.Threading.Timeout.Infinite);

            rfid_EventHandler += FrmMain_rfid_EventHandler;
            strec_EventHandler += FrmMain_strec_EventHandler;
            strec_sleep_EventHandler += FrmMain_strec_sleep_EventHandler;
            //adam_EventHandler += FrmMain_adam_EventHandler;

            updateForm(false, "init");

            if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
            {
                pojezdUC1.Trida = new TridaDataProKresleni();
                pojezdUC1.Trida_vykladka = new TridaDataProKresleni_vykladka();
                pojezdUC1.Draw();
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage3);
            }

            //zapis do ERR logu o nefunkcnosti RFID 
            Show_RFID_LED(3);
            int akce = 1;
            string popisek = string.Format("RFID funkčnost TAG / zluta");
            LV.OnLogsEvent(new Logs_EventArgs(0, akce, popisek, LoginID, AGRO.status_0));

            #region TEST
            //int akce = 5;
            //string popisek = string.Format("VYKLADKA-ZAZNAM-stretch:{0}-chybi zaznam nakladky", 1);
            //LV.OnLogsEvent(new Logs_EventArgs(2, akce, popisek, LoginID, AGRO.status_0));
            #endregion


        }

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

                //button3.Text = cnt.ToString("00");
                cnt++;
            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH---SetTextButton: {0} ", ex));
                ExceptionHandler2.Handle(ex);
            }
        }

        private void CallBack_Timer_Strec1(object state)
        {
            try
            {
                //---------------------------Start logika uspani----------------///
                //OnSTREC_sleep_Event(new STREC_EventArgs(e.CisloLinky));
#if DEBUG
                //Log.Write(string.Format("TIMER--VYKLADKA_STR_{0}", 1));
                log_hlaska = string.Format("TIMER--VYKLADKA_Boceddi_{0}", 1);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif


                if (interniCisloLinka != 0) 
                {
#if DEBUG
                    //Log.Write(string.Format("TIMER--VYKLADKA_STR_{0} interniCisloLinka != 0 --číslo linky: {0}", 1, interniCisloLinka));
                    log_hlaska = string.Format("TIMER--VYKLADKA_Boceddi_{0} interniCisloLinka != 0 --číslo linky: {0}", 1, _interniCisloLinka);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    //vypnuti cteni z rfid !!
                    if (RFID_Linky_OUT != null)
                    {
                        RFID_Linky_OUT.Stop_Read_tags();
                    }


                    LV.OnVykladkaEvent(new Vykladka_EventArgs(interniCisloLinka, AGRO.bocedi_1));

                    #region zapis do ERR_logs
                    if (AgroSledovaniVozikuConfig.config.Vykladka[0].logovani_RFID_TAG)
                    {
                        //zapis do ERR logu o nefunkcnosti RFID 
                        int stretch = 1;
                        int akce = 1;
                        string popisek = string.Format("VYKLADKA-RFID-strec:{0}-nefunkcni RFID TAG", stretch);
                        LV.OnLogsEvent(new Logs_EventArgs(interniCisloLinka, akce, popisek, LoginID, AGRO.status_0));
                    }

                    #endregion
                }
                else
                {
                    if(OnlineLog_Bocedi_1)
                    {
                        //logovani online fatalni chyby o vykladce
                        int akce = 4;
                        string popisek = string.Format("VYKLADKA - Casovas_1 - strec:1 - Falešná vykládka, korektní vyhodnocení!!"); 
                        LV.OnLogsEvent(new Logs_EventArgs(Fask.Constants.AGRO.bocedi_1, akce, popisek, LoginID, 0));
                    }
                    else
                    {
                        //vse je ok
                    }

                }


                //---------------------------End logika uspani----------------///TIMER--VYKLADKA_STR_1 interniCisloLinka != 0
            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH-TIMER--CallBack_Timer_Strec1: {0} ", ex));
                ExceptionHandler2.Handle(ex);
            }

        }

        private void CallBack_Timer_Strec2(object state)
        {
            try
            {
                //---------------------------Start logika uspani----------------///
                //OnSTREC_sleep_Event(new STREC_EventArgs(e.CisloLinky));
#if DEBUG
                //Log.Write(string.Format("TIMER--VYKLADKA_STR_{0}", 2));
                log_hlaska = string.Format("TIMER--VYKLADKA_Boceddi_{0}", 2);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                //Thread.Sleep(AgroSledovaniVozikuConfig.config.Vykladka[0].TimeEventStrec);

                if (interniCisloLinka != 0)
                {
#if DEBUG
                    //Log.Write(string.Format("TIMER--VYKLADKA_STR_{0} interniCisloLinka != 0 --číslo linky: {0}",2, interniCisloLinka));
                    log_hlaska = string.Format("TIMER--VYKLADKA_Boceddi_{0} interniCisloLinka != 0 --číslo linky: {0}", 1, _interniCisloLinka);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    //vypnuti cteni z rfid !!
                    if (RFID_Linky_OUT != null)
                    {
                        RFID_Linky_OUT.Stop_Read_tags();
                    }

                    LV.OnVykladkaEvent(new Vykladka_EventArgs(interniCisloLinka, AGRO.bocedi_2));

                    #region zapis do ERR_logs
                    if( AgroSledovaniVozikuConfig.config.Vykladka[0].logovani_RFID_TAG)
                    {
                        //zapis do ERR logu o nefunkcnosti RFID 
                        int stretch = 2;
                        int akce = 1;
                        string popisek = string.Format("VYKLADKA-RFID-strec:{0}-nefunkcni RFID TAG", stretch);
                        LV.OnLogsEvent(new Logs_EventArgs(interniCisloLinka, akce, popisek, LoginID, AGRO.status_0));
                    }
               
                    #endregion
                }
                else
                {
                    if (OnlineLog_Bocedi_2)
                    {
                        //logovani online fatalni chyby o vykladce
                        int akce = 4;
                        string popisek = string.Format("VYKLADKA - Casovas_2 - strec:2 - Falešná vykládka, korektní vyhodnocení!!"); 
                        LV.OnLogsEvent(new Logs_EventArgs(Fask.Constants.AGRO.bocedi_2, akce, popisek, LoginID, 0));
                    }
                    else
                    {
                        //vse je ok
                    }

                }


                //---------------------------End logika uspani----------------///
            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH-TIMER--CallBack_Timer_Strec2: {0} ", ex));
                ExceptionHandler2.Handle(ex);
            }
        }

        private void CallBack_Timer_Vaha1(object state)
        {
            try
            {

                //---------------------------Start logika uspani----------------///
                //OnSTREC_sleep_Event(new STREC_EventArgs(e.CisloLinky));
#if DEBUG
                //Log.Write(string.Format(" TIMER--VAHA_{0}", 1));
                log_hlaska = string.Format(" TIMER--VAHA_{0}", 1);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                //Thread.Sleep(AgroSledovaniVozikuConfig.config.Vykladka[0].TimeEventStrec);



                //kouknout se jestli je zaznam 31, pokud neni beru 21
                if (vaha_1_data)
                {
#if DEBUG
                    //Log.Write(string.Format(" TIMER--VAHA_{0}", 2));
                    log_hlaska = string.Format(" TIMER--VAHA_{0}--START", 1);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                    Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                    FE_objekt = LV.Najdi_zaznam(AGRO.status_31, AGRO.status_41);

                    if (FE_objekt != null)
                    {
#if DEBUG
                        //Log.Write(string.Format(" TIMER--VAHA_{0} start logiky tisk status 31 ", 1));
                        log_hlaska = string.Format(" TIMER--VAHA_{0} start logiky tisk status 31 ", 1);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_31));

                    }
                    else
                    {
#if DEBUG
                        //Log.Write(string.Format(" TIMER--VAHA_{0} start logiky tisk status 21 ", 1));
                        log_hlaska = string.Format(" TIMER--VAHA_{0} start logiky tisk status 21 ", 1);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_21));
                    }


                    ////vizualizace vykladka ctverecek 4
                    ////vyhledej zaznam se statusem 41, zacni hledat az po x sekundach
                    OnRFIDEvent(new RFID_EventArgs(AGRO.status_41));
                }

                //---------------------------End logika uspani----------------///
            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH-TIMER--CallBack_Timer_Vaha1: {0} ", ex));
                ExceptionHandler2.Handle(ex);
            }

        }

        private void CallBack_Timer_Vaha2(object state)
        {
            try
            {

                //---------------------------Start logika uspani----------------///
                //OnSTREC_sleep_Event(new STREC_EventArgs(e.CisloLinky));
#if DEBUG
                //Log.Write(string.Format(" TIMER--VAHA_{0}", 2));
                log_hlaska = string.Format(" TIMER--VAHA_{0}", 2);
                ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                //Thread.Sleep(AgroSledovaniVozikuConfig.config.Vykladka[0].TimeEventStrec);

                if (vaha_2_data)
                {

#if DEBUG
                    //Log.Write(string.Format(" TIMER--VAHA_{0}", 2));
                    log_hlaska = string.Format(" TIMER--VAHA_{0}--START", 2);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                    Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_objekt = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                    FE_objekt = LV.Najdi_zaznam(AGRO.status_32, AGRO.status_42);

                    if (FE_objekt != null)
                    {
#if DEBUG
                        //Log.Write(string.Format(" TIMER--VAHA_{0} start logiky tisk status 32 ", 2));
                        log_hlaska = string.Format(" TIMER--VAHA_{0} start logiky tisk status 32 ", 2);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_32));

                    }
                    else
                    {
#if DEBUG
                        //Log.Write(string.Format(" TIMER--VAHA_{0} start logiky tisk status 22 ", 2));
                        log_hlaska = string.Format(" TIMER--VAHA_{0} start logiky tisk status 22 ", 2);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_22));
                    }

                    ////vizualizace vykladka ctverecek 4
                    ////vyhledej zaznam se statusem 41, zacni hledat az po x sekundach
                    OnRFIDEvent(new RFID_EventArgs(AGRO.status_42));
                }

                //---------------------------End logika uspani----------------///
            }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH-TIMER--CallBack_Timer_Vaha2: {0} ", ex));
                ExceptionHandler2.Handle(ex);
            }

        }

        #endregion


        #region monitoring komunikace ADAM timery
        private void CallBack_Timer_Adam1(object state)
        {
            try
            {
                log_hlaska = string.Format(" TIMER--ADAM_1");

                if(ADAM1_signal == true)
                {
                    ADAM1_signal = false;
                }
                else
                {
                    string hlaska = string.Format("ADAM_1 výpadek komunikace!");
                    int akce_nakladka = 8;
                    LV.OnLogsEvent(new Logs_EventArgs(0, akce_nakladka, hlaska, LoginID, AGRO.status_0));
                }

                timer_adam_1.Change(AgroSledovaniVozikuConfig.config.Vykladka[0].Time_ADAM1, System.Threading.Timeout.Infinite);

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

        }
        private void CallBack_Timer_Adam2(object state)
        {
            try
            {
                log_hlaska = string.Format(" TIMER--ADAM_2");
                if (ADAM2_signal == true)
                {
                    ADAM2_signal = false;
                }
                else
                {
                    string hlaska = string.Format("ADAM_2 výpadek komunikace!");
                    int akce_nakladka = 8;
                    LV.OnLogsEvent(new Logs_EventArgs(0, akce_nakladka, hlaska, LoginID, AGRO.status_0));
                }
                timer_adam_2.Change(AgroSledovaniVozikuConfig.config.Vykladka[0].Time_ADAM1, System.Threading.Timeout.Infinite);
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

        }
        private void CallBack_Timer_Adam3(object state)
        {
            try
            {
                log_hlaska = string.Format(" TIMER--ADAM_3");
                if (ADAM3_signal == true)
                {
                    ADAM3_signal = false;
                }
                else
                {
                    string hlaska = string.Format("ADAM_3 výpadek komunikace!");
                    int akce_nakladka = 8;
                    LV.OnLogsEvent(new Logs_EventArgs(0, akce_nakladka, hlaska, LoginID, AGRO.status_0));
                }
                timer_adam_3.Change(AgroSledovaniVozikuConfig.config.Vykladka[0].Time_ADAM1, System.Threading.Timeout.Infinite);
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            }

        }
        #endregion

        private void FrmMain_rfid_EventHandler(object sender, RFID_EventArgs e)
        {

            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "FrmMain_rfid_EventHandler");
            Fask.Tracing.Trac.Write("START metody FrmMain_rfid_EventHandler", tracId); 


            try
            {



                //if(RFID_Linky_IN != null)
                //    LV.RFID_tag_zapis(RFID_Linky_IN, e.CisloLinky);

                if ((RFID_Linky_IN != null) && (e.CisloLinky == AGRO.linka_1 || e.CisloLinky == AGRO.linka_2 || e.CisloLinky == AGRO.linka_3))
                {

                    Fask.Tracing.Trac.Write("START Nakladka Linka 1-3", tracId); 

#if DEBUG
                    //Log.Write(string.Format("RFID_EVENT císlo linky: {0}", e.CisloLinky));
                    log_hlaska = string.Format("RFID_EVENT číslo linky: {0}", e.CisloLinky);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    LV.RFID_tag_zapis(RFID_Linky_IN, e.CisloLinky);

                    Fask.Tracing.Trac.Write("END Nakladka Linka 1-3", tracId); 

                }
                else if ((RFID_Linky_OUT != null) && (e.CisloLinky == AGRO.bocedi_1 || e.CisloLinky == AGRO.bocedi_2))
                {

                    Fask.Tracing.Trac.Write("START Vykladka Bocedi 1-2", tracId); 


#if DEBUG
                    //Log.Write(string.Format("RFID_EVENT číslo linky: {0}", e.CisloLinky));
                    log_hlaska = string.Format("RFID_EVENT číslo linky: {0}", e.CisloLinky);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                    LV.RFID_tag_zapis(RFID_Linky_OUT, e.CisloLinky);


                    Fask.Tracing.Trac.Write("END Vykladka Bocedi 1-2", tracId); 


                }
                else if ((RFID_Linky_OUT != null) && (e.CisloLinky == AGRO.rucni_vstup))
                {

                    Fask.Tracing.Trac.Write("START Nakladka RV ", tracId); 

#if DEBUG
                    //Log.Write(string.Format("RFID_EVENT císlo linky: {0}", e.CisloLinky));
                    log_hlaska = string.Format("RFID_EVENT číslo linky: {0}", e.CisloLinky);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    LV.RFID_tag_zapis(RFID_Linky_OUT, e.CisloLinky);


                    Fask.Tracing.Trac.Write("END Nakladka RV ", tracId); 

                }
                else if (e.CisloLinky == AGRO.status_21)
                {

                    Fask.Tracing.Trac.Write("START logiky TISK bez vahy 21 ", tracId); 

                    lock (vaha_1_data_object)
                    {
                        vaha_1_data = false;
                    }
#if DEBUG
                    //Log.Write(string.Format("RFID_EVENT_TISK císlo linky: {0}", e.CisloLinky));
                    log_hlaska = string.Format("RFID_EVENT číslo linky: {0}", e.CisloLinky);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    LV.TiskPrijem(e.CisloLinky);


                    Fask.Tracing.Trac.Write("END logiky TISK bez vahy 21 ", tracId); 

                }
                else if (e.CisloLinky == AGRO.status_22)
                {


                    Fask.Tracing.Trac.Write("START logiky TISK bez vahy 22 ", tracId); 

                    lock (vaha_2_data_object)
                    {
                        vaha_2_data = false;
                    }
#if DEBUG
                    //Log.Write(string.Format("RFID_EVENT_TISK císlo linky: {0}", e.CisloLinky));
                    log_hlaska = string.Format("RFID_EVENT číslo linky: {0}", e.CisloLinky);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    LV.TiskPrijem(e.CisloLinky);


                    Fask.Tracing.Trac.Write("END logiky TISK bez vahy 22 ", tracId); 

                }
                else if (e.CisloLinky == AGRO.status_31)
                {

                    Fask.Tracing.Trac.Write("START logiky TISK 31 ", tracId); 

                    //nova logika vaha_1
                    lock (vaha_1_data_object)
                    {
                        vaha_1_data = false;
                    }
#if DEBUG
                    //Log.Write(string.Format("RFID_EVENT_TISK císlo linky: {0}", e.CisloLinky));
                    log_hlaska = string.Format("RFID_EVENT číslo linky: {0}", e.CisloLinky);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    LV.TiskPrijem_Vaha(e.CisloLinky);


                    Fask.Tracing.Trac.Write("END logiky TISK 31 ", tracId); 


                }
                else if (e.CisloLinky == AGRO.status_32)
                {

                    Fask.Tracing.Trac.Write("START logiky TISK 32 ", tracId); 


                    //nova logika vaha_2
                    lock (vaha_2_data_object)
                    {
                        vaha_2_data = false;
                    }
#if DEBUG
                    //Log.Write(string.Format("RFID_EVENT_TISK císlo linky: {0}", e.CisloLinky));
                    log_hlaska = string.Format("RFID_EVENT číslo linky: {0}", e.CisloLinky);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                    LV.TiskPrijem_Vaha(e.CisloLinky);


                    Fask.Tracing.Trac.Write("END logiky TISK 32 ", tracId); 

                }
                else if (e.CisloLinky == AGRO.status_41)
                {


                    Fask.Tracing.Trac.Write("START logiky vizualizace 41 ", tracId); 


                    //muze nastat chyba ve vizualizaci skrz novou logiku vahy!!

#if DEBUG
                    //Log.Write(string.Format("RFID_EVENT_VIZUALIZACE císlo linky: {0}", e.CisloLinky));
                    log_hlaska = string.Format("RFID_EVENT_VIZUALIZACE císlo linky: {0}", e.CisloLinky);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                    Thread.Sleep(AgroSledovaniVozikuConfig.config.Vykladka[0].TimeVizualizaceTisk);

                    //najdu data
#region vizualizace
                    if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    {
                        if (!Vizualizace_vykladka_logika(1))
                        {
                            string log_hlaska_x = string.Format("ERROR! -RFID_EVENT_VIZUALIZACE číslo linky: {0} se nezdařila!!", e.CisloLinky);
                            ExceptionHandler2.Handle(log_hlaska_x, "Log_LV", "txt");
                        } 
                    }

                    #endregion
                    //LV.TiskPrijem_Vaha(e.CisloLinky);

                    Fask.Tracing.Trac.Write("END logiky vizualizace 41 ", tracId); 

                }
                else if (e.CisloLinky == AGRO.status_42)
                {

                    Fask.Tracing.Trac.Write("START logiky vizualizace 42 ", tracId); 


                    //muze nastat chyba ve vizualizaci skrz novou logiku vahy!!
#if DEBUG
                    //Log.Write(string.Format("RFID_EVENT_VIZUALIZACE císlo linky: {0}", e.CisloLinky));
                    log_hlaska = string.Format("RFID_EVENT_VIZUALIZACE císlo linky: {0}", e.CisloLinky);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


#endif

                    Thread.Sleep(AgroSledovaniVozikuConfig.config.Vykladka[0].TimeVizualizaceTisk);

#region vizualizace
                    if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    {
                        if (!Vizualizace_vykladka_logika(2))
                        {
                            string log_hlaska_x = string.Format("ERROR! -RFID_EVENT_VIZUALIZACE číslo linky: {0} se nezdařila!!", e.CisloLinky);
                            ExceptionHandler2.Handle(log_hlaska_x, "Log_LV", "txt");
                        } 
                    }

                    #endregion


                    Fask.Tracing.Trac.Write("END logiky vizualizace 42 ", tracId); 

                }
                else if (e.CisloLinky == AGRO.status_61)
                {

                    Fask.Tracing.Trac.Write("START logiky ukonceni 61 ", tracId); 


                    log_hlaska = string.Format("Pokus o zmenu statusu na " + e.CisloLinky);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    if(LV.ZmenaStatusu(e.CisloLinky))
                    {
                        log_hlaska = string.Format("Status "+ e.CisloLinky + "--OK");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }
                    else
                    {
                        log_hlaska = string.Format("Status " + e.CisloLinky + "--ERROR");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }

#region vizualizace
                    if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    {
                        if (!Vizualizace_vykladka_logika(1))
                        {
                            string log_hlaska_x = string.Format("ERROR! -RFID_EVENT_VIZUALIZACE číslo linky: {0} se nezdařila!!", e.CisloLinky);
                            ExceptionHandler2.Handle(log_hlaska_x, "Log_LV", "txt");
                        } 
                    }

                    #endregion


                    Fask.Tracing.Trac.Write("END logiky ukonceni 61 ", tracId); 


                }
                else if (e.CisloLinky == AGRO.status_62)
                {


                    Fask.Tracing.Trac.Write("START logiky ukonceni 62 ", tracId); 

                    log_hlaska = string.Format("Pokus o zmenu statusu na " + e.CisloLinky);
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    if (LV.ZmenaStatusu(e.CisloLinky))
                    {
                        log_hlaska = string.Format("Status " + e.CisloLinky + "--OK");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }
                    else
                    {
                        log_hlaska = string.Format("Status " + e.CisloLinky + "--ERROR");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }
                }
                else
                {
                    //Log.Write("ERROR FrmMain_rfid_EventHandler neocekavana varianta");
                    log_hlaska = string.Format("ERROR FrmMain_rfid_EventHandler neocekavana varianta");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                }

#region vizualizace
                if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                {
                    if (!Vizualizace_vykladka_logika(2))
                    {
                        string log_hlaska_x = string.Format("ERROR! -RFID_EVENT_VIZUALIZACE číslo linky: {0} se nezdařila!!", e.CisloLinky);
                        ExceptionHandler2.Handle(log_hlaska_x, "Log_LV", "txt");
                    } 
                }

                #endregion



                Fask.Tracing.Trac.Write("END logiky ukonceni 62 ", tracId); 

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
            }


            Fask.Tracing.Trac.Write("END metody FrmMain_rfid_EventHandler", tracId); 

        }

        #region RFID blikacka anten

      
        public void RFID_anteny_barva(int ID_Linka, bool zapnuti =false)
        {
            try
            {
                string reader = string.Empty;
                int pozice = 0;

                if (ID_Linka == AGRO.linka_1)
                {
                    reader = "reader_1";
                    pozice = (ushort)AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka1;
                }
                else if (ID_Linka == AGRO.linka_2)
                {
                    reader = "reader_1";
                    pozice = (ushort)AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka2;
                }
                else if (ID_Linka == AGRO.linka_3)
                {
                    reader = "reader_1";
                    pozice = (ushort)AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka3;
                }
                else if (ID_Linka == AGRO.bocedi_1)
                {
                    reader = "reader_2";
                    pozice = (ushort)AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_strec1;
                }
                else if (ID_Linka == AGRO.bocedi_2)
                {
                    reader = "reader_2";
                    pozice = (ushort)AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_strec2;
                }
                else if (ID_Linka == AGRO.rucni_vstup)
                {
                    reader = "reader_2";
                    pozice = (ushort)AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_rucniVstup;
                }

                if(zapnuti)
                {
                    RFID_Antena_zobraz_barvy( pozice,  reader);
                }
                else
                {
                    rfiD_UC1.updateForm();
                }

              }
            catch (Exception ex)
            {

                //Log.Write(string.Format("CATCH-RFID--RFID_tag_zapis: {0} ", ex));
                ExceptionHandler2.Handle(ex);
            }

        }

        private void RFID_Antena_zobraz_barvy(int pozice, string reader)
        {
            try
            {
                if (reader == "reader_1")
                {
                    switch (pozice)
                    {
                        case 1:
                           // rfiD_UC1.ChB_R1_ANT1 = true;
                            rfiD_UC1.chB1_ANT_1.BackColor  = Color.LightGreen;
                            break;
                        case 2:
                          //  rfiD_UC1.ChB_R1_ANT2 = true;
                            rfiD_UC1.chB1_ANT_2.BackColor = Color.LightGreen;
                            break;
                        case 3:
                           // rfiD_UC1.ChB_R1_ANT3 = true;
                            rfiD_UC1.chB1_ANT_3.BackColor = Color.LightGreen;
                            break;
                        case 4:
                           // rfiD_UC1.ChB_R1_ANT4 = true;
                            rfiD_UC1.chB1_ANT_4.BackColor = Color.LightGreen;
                            break;
                        default:
                            break;
                    }
                }
                if (reader == "reader_2")
                {
                    switch (pozice)
                    {
                        case 1:
                           // rfiD_UC1.ChB_R2_ANT1 = true;
                            rfiD_UC1.chB2_ANT_1.BackColor = Color.LightGreen;
                            break;
                        case 2:
                           // rfiD_UC1.ChB_R2_ANT2 = true;
                            rfiD_UC1.chB2_ANT_2.BackColor = Color.LightGreen;
                            break;
                        case 3:
                           // rfiD_UC1.ChB_R2_ANT3 = true;
                            rfiD_UC1.chB2_ANT_2.BackColor = Color.LightGreen;
                            break;
                        case 4:
                            //rfiD_UC1.ChB_R2_ANT4 = true;
                            rfiD_UC1.chB2_ANT_2.BackColor = Color.LightGreen;
                            break;
                        default:
                            break;
                    }
                }
                //rfiD_UC1.updateForm();
            }
            catch (Exception ex)
            {
                //Exceptions.Handler.ErrorHandle(ex.Message, "RFID_Antena_zobraz", true);
                //Log.WriteException(ex.Message);
                ExceptionHandler2.Handle(ex);

            }
        }
        #endregion

        #endregion

        #region RFID Zebra Event

        private void RFID_Linky_DataReadyZEBRA(object sender, IRFIDProvider.RFID_ZABRA_EventArgs e)
        {


            try
            {
                List<Item_Tag> tmp = new List<Item_Tag>();
                foreach (var item in e.TagIDs)
                {
                    var y = LV.Tags.Where(x => x.EPC == item.EPC && x.ID_Antena == item.ID_Antena).ToList();

                    if (y.Count == 0)
                    {
                        LV.Tags.Add(item);
                        tmp.Add(item);
                    }
                }

                //--odkomentovat v pripade chyby!!!!--------------
                //if (tmp.Count == 0)
                //    return;



#region Testy
                ////var x = e.TagIDs;
                ////txB_data.Text = string.Empty;
                //SetText(string.Empty);
                //string Text = string.Empty;
                //string EPC = string.Empty;
                //string Antena = string.Empty;




                //foreach (var item in e.TagIDs)
                //{
                //    //vypis dat o RFID tagu
                //    Text += string.Format("Čas: {0} EPC: {1} Antena ID: {2}" + Environment.NewLine, DateTime.Now.ToString(), item.EPC, item.ID_Antena);

                //    //zobrazeni hodnoty v tagu
                //    EPC = item.EPC;
                //    Antena = item.ID_Antena;
                //}

                //SetText(Text);
                //SetText_ToUC(EPC);
#endregion


                string EPC = string.Empty;
                string Antena = string.Empty;

                if (e.TagIDs.Count == 1)
                {
                    EPC = e.TagIDs[0].EPC;
                    Antena = e.TagIDs[0].ID_Antena;

                    if(!button1.Enabled)
                    SetText(EPC);

                    //if (!string.IsNullOrEmpty(EPC) && EPC.Length == 24)
                    //zmenit !!

                    if (!string.IsNullOrEmpty(EPC))
                    {
                        if (EPC.Length == 24)
                        {
                            if (e.CisloLinky == AGRO.linka_1 || e.CisloLinky == AGRO.linka_2 || e.CisloLinky == AGRO.linka_3)
                            {
                                LV.StopReadRFID(RFID_Linky_IN, e.CisloLinky, EPC, ushort.Parse(Antena), e.Volajici);
                                vizualizace(true, e.Volajici);
                            }

                            if (e.CisloLinky == AGRO.bocedi_1 || e.CisloLinky == AGRO.bocedi_2)
                            {
                                LV.StopReadRFID(RFID_Linky_OUT, e.CisloLinky, EPC, ushort.Parse(Antena), e.Volajici);
                                vizualizace(true, e.Volajici);

#if DEBUG
                               // Log.Write(string.Format("READ_TAG PRUCHOD_TRUE číslo linky: {0} EPC: {1} Volajici: {2}", e.CisloLinky, EPC, e.Volajici));
#endif
                            }

                            if (e.CisloLinky == AGRO.rucni_vstup)
                            {
                                LV.StopReadRFID(RFID_Linky_OUT, e.CisloLinky, EPC, ushort.Parse(Antena), e.Volajici);
                                vizualizace(true, e.Volajici);
                            } 
                        }

                    }
                }

                //cisteni TAGu
                e.TagIDs.Clear();
            }
            catch (Exception ex)
            {

                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
            }

        }

        private void SetText(string txt)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    SetText(txt);
                }));
                return;
            }

            txB_data.Text = txt;
            txB_EPC_zdroj.Text = txt;
        }

        public void SetText_ToUC(string txt, string source)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    SetText_ToUC(txt, source);
                }));
                return;
            }

            if(source == Const_Kontrola_Linky)
                rfiD_UC1.Tag_Text_W = txt;
            else if(source == Const_Zapis_Linky)
                rfiD_UC1.Tag_Text_R = txt;
            else if (source == Const_Zapis_Odvadeni)
                rfiD_UC1.Tag_Text_R = txt;
            else if (source == Const_Kontrola_Odvadeni)
                rfiD_UC1.Tag_Text_W = txt;
        }
#endregion

        private void InitUserInfo()
        {
            

        }


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


        public void ClosePorts()
        {
            TimerRefreshUIOff();
            timerRefreshUI = null;

            AdamStop();
            //Log.Write("Ukonceni modulu");
            ExceptionHandler2.Handle("Ukonceni modulu", "Log_LV", "txt");
        }

        public bool IsReadyToClose(out string message)
        {
            message = string.Empty;
            return true;
        }

        public void ReturnPortsToPreviousState()
        {
        }


        private void LoadConfiguration()
        {
            try
            {
                //Pokud nebyly nacteny zadne parametry, vlozim vychzoi (?)
                if (AgroSledovaniVozikuConfig.ExistiFile())
                {
                    string cestaKAdresari_SQLiteCommLib = (new Uri( System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), "SQLiteCommLib.dll"))).LocalPath;
                    string cestaKAdresari_SQLRemoteLib = (new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), "SQLRemoteLib.dll"))).LocalPath;


                    AgroSledovaniVozikuConfig.config.DB_Config.AddDB_ConfigRow(
                        "automaticke ulozeni paleta;posledni paleta 1;posledni paleta 3",
                        50,
                        100,
                        "presun na vozik;presun na streckovacku",
                        20,
                        "presun na streckovacku;presun na tisk",
                        "vaha;vaha_chyba;presun na tisk",
                        100,
                        "presun na tisk;tisk_aplikovano;tisk_neaplikovano_NP;tisk_neaplikovano_FP;tisk_chyba",
                        100,
                        20
                        );

                    AgroSledovaniVozikuConfig.config.ADAM_1.AddADAM_1Row(1025, "192.168.1.63", 1025, 350, 100,false,false, true);
                    AgroSledovaniVozikuConfig.config.ADAM_2.AddADAM_2Row(1025, "192.168.1.63", 1025, 350, 100,false, false, true);
                    AgroSledovaniVozikuConfig.config.ADAM_3.AddADAM_3Row(1025, "192.168.1.63", 1025, 350, 100,false, false, true);

                    AgroSledovaniVozikuConfig.config.RFID_1.AddRFID_1Row(false, "192.168.1.54", 5084, -1, -1, -1, -1, 300,10);
                    AgroSledovaniVozikuConfig.config.RFID_2.AddRFID_2Row(false, "192.168.1.56", 5084, -1, -1, -1, -1, 300,10);

                    AgroSledovaniVozikuConfig.config.ADAM_DI.AddADAM_DIRow(-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,-1,-1,-1,-1,-1,-1,-1);
                    AgroSledovaniVozikuConfig.config.ADAM_2_DI.AddADAM_2_DIRow(-1, -1, -1, -1, -1, -1);
                    AgroSledovaniVozikuConfig.config.ADAM_3_DI.AddADAM_3_DIRow(-1, -1, -1, -1, -1, -1);
                    AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION.AddADAM_1_DESCRIPTIONRow(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,false);
                    AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION.AddADAM_2_DESCRIPTIONRow(string.Empty, "Vykladka", "Tiskni", "Vaha start", "Odjezd", "Vaha konec", "Vaha chyba", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false);
                    AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION.AddADAM_3_DESCRIPTIONRow(string.Empty, "Vykladka", "Tiskni", "Vaha start", "Odjezd", "Vaha konec", "Vaha chyba", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false);

                    AgroSledovaniVozikuConfig.config.Vykladka.AddVykladkaRow(5000,5000,5000,5000, false, 3000,false,false,36000000,36000000,36000000);

                    AgroSledovaniVozikuConfig.config.Vizualizace.AddVizualizaceRow(true);

                    AgroSledovaniVozikuConfig.config.CS.AddCSRow(
                        cestaKAdresari_SQLiteCommLib,
                        cestaKAdresari_SQLRemoteLib
                        );

                    

                    //AgroSledovaniVozikuConfig.config.WEBAPI.AddWEBAPIRow(
                    //    "MDox",
                    //    "192.168.1.121:8080", // 192.168.1.69/MST_Win_Kom_Server_7_Dasenka                     
                    //    "api",
                    //    false,
                    //    5000,
                    //    false
                    //    );


                    AgroSledovaniVozikuConfig.config.WEBAPI_TISK.AddWEBAPI_TISKRow(
                        "MDox",
                        "10.11.10.60:56425", // 192.168.1.69/MST_Win_Kom_Server_7_Dasenka                     
                        "api",
                        false,
                        5000,
                        false,
                        false
                        );

                    AgroSledovaniVozikuConfig.config.HesloDoKonfigurace.AddHesloDoKonfiguraceRow(string.Empty, string.Empty);

              
                    //Ulozeni
                    AgroSledovaniVozikuConfig.Save();
                }

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

#region ADAM3

                    tb_ADAM3_PortP2P.Text = AgroSledovaniVozikuConfig.config.ADAM_3[0].PortP2PAdam.ToString();
                    tb_ADAM3_IPAdresa.Text = AgroSledovaniVozikuConfig.config.ADAM_3[0].IPAdresaAdam;
                    tb_ADAM3_TimerPeriodSensorsCheckAdam.Text = AgroSledovaniVozikuConfig.config.ADAM_3[0].TimerPeriodSensorsCheckAdam.ToString();
                    tb_ADAM3_AdamTCPTimeout.Text = AgroSledovaniVozikuConfig.config.ADAM_3[0].AdamTCPTimeout.ToString();
                    tb_ADAM3_MinHighSignalWidthAdam.Text = AgroSledovaniVozikuConfig.config.ADAM_3[0].MinHighSignalWidthAdam.ToString();
                    cb_ADAM3_Connect.Checked = AgroSledovaniVozikuConfig.config.ADAM_3[0].Connect;

#endregion

#region RFID 1

                    chb_RFID1_Enabled.Checked = AgroSledovaniVozikuConfig.config.RFID_1[0].Enable;
                    tb_RFID1_IP_Adresa.Text = AgroSledovaniVozikuConfig.config.RFID_1[0].IP_Adresa;
                    tb_RFID1_Port.Text = AgroSledovaniVozikuConfig.config.RFID_1[0].Port.ToString();
                    tb_RFID1_ID_antena_Linka1.Text = AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka1.ToString();
                    tb_RFID1_ID_antena_Linka2.Text = AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka2.ToString();
                    tb_RFID1_ID_antena_Linka3.Text = AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka3.ToString();
                    tb_RFID1_ID_antena_4.Text = AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_4.ToString();
                    tb_RFID1_Time_read.Text = AgroSledovaniVozikuConfig.config.RFID_1[0].Time_read.ToString();
                    tb_RFID1_PocetPruchodu.Text = AgroSledovaniVozikuConfig.config.RFID_1[0].PocetPruchodu.ToString();

#endregion

#region RFID 2

                    chb_RFID2_Enabled.Checked = AgroSledovaniVozikuConfig.config.RFID_2[0].Enable;
                    tb_RFID2_IP_Adresa.Text = AgroSledovaniVozikuConfig.config.RFID_2[0].IP_Adresa;
                    tb_RFID2_Port.Text = AgroSledovaniVozikuConfig.config.RFID_2[0].Port.ToString();
                    tb_RFID2_ID_antena_strec1.Text = AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_strec1.ToString();
                    tb_RFID2_ID_antena_strec2.Text = AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_strec2.ToString();
                    tb_RFID2_ID_antena_rucniVstup.Text = AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_rucniVstup.ToString();
                    tb_RFID2_ID_antena_4.Text = AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_4.ToString();
                    tb_RFID2_Time_read.Text = AgroSledovaniVozikuConfig.config.RFID_2[0].Time_read.ToString();
                    tb_RFID2_PocetPruchodu.Text = AgroSledovaniVozikuConfig.config.RFID_2[0].PocetPruchodu.ToString();

#endregion

#region ADAM DI 1

                    tb_ADAM1_Linka1_END.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_END.ToString();
                    tb_ADAM1_Linka1_RUN.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_RUN.ToString();
                    tb_ADAM1_Linka1_OK.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_OK.ToString();
                    tb_ADAM1_Linka1_CALL.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_CALL.ToString();

                    tb_ADAM1_Linka2_END.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_END.ToString();
                    tb_ADAM1_Linka2_RUN.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_RUN.ToString();
                    tb_ADAM1_Linka2_OK.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_OK.ToString();
                    tb_ADAM1_Linka2_CALL.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_CALL.ToString();

                    tb_ADAM1_Linka3_END.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_END.ToString();
                    tb_ADAM1_Linka3_RUN.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_RUN.ToString();
                    tb_ADAM1_Linka3_OK.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_OK.ToString();
                    tb_ADAM1_Linka3_CALL.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_CALL.ToString();

                    tb_ADAM1_L1.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].L1.ToString();
                    tb_ADAM1_L2.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].L2.ToString();
                    tb_ADAM1_L3.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].L3.ToString();
                    tb_ADAM1_L4.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].L4.ToString();

                    tb_ADAM1_RucnyVstup_END.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_END.ToString();
                    tb_ADAM1_RucnyVstup_RUN.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_RUN.ToString();
                    tb_ADAM1_RucnyVstup_OK.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_OK.ToString();
                    tb_ADAM1_RucnyVstup_CALL.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_CALL.ToString();

                    tb_ADAM1_Strec1_OK.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_OK.ToString();
                    tb_ADAM1_Strec1_Bit1.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_Bit1.ToString();
                    tb_ADAM1_Strec1_Bit2.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_Bit2.ToString();
                    tb_ADAM1_Strec1_Bit4.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_Bit4.ToString();

                    tb_ADAM1_Strec2_OK.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec2_OK.ToString();
                    tb_ADAM1_Strec2_Bit1.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec2_Bit1.ToString();
                    tb_ADAM1_Strec2_Bit2.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec2_Bit2.ToString();
                    tb_ADAM1_Strec2_Bit4.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec2_Bit4.ToString();

                    tb_ADAM1_Strec_Vyber.Text = AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec_vyber.ToString();

#endregion

#region ADAM DI 2

                    tb_ADAM2_Strec1_OK.Text = AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Strec1_OK.ToString();
                    tb_ADAM2_Vaha_START.Text = AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Bocedi1_OUT.ToString();
                    tb_ADAM2_Vaha_END.Text = AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Vaha1_END.ToString();
                    tb_ADAM2_Tisk_END.Text = AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Tisk1_END.ToString();

#endregion

#region ADAM DI 3

                    tb_ADAM3_Strec2_OK.Text = AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Strec2_OK.ToString();
                    tb_ADAM3_Vaha_START.Text = AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Bocedi2_OUT.ToString();
                    tb_ADAM3_Vaha_END.Text = AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Vaha2_END.ToString();
                    tb_ADAM3_Tisk_END.Text = AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Tisk2_END.ToString();

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

#region WEB API FASK

                    //tb_APIFASK_Autorizace_DoAPI.Text = AgroSledovaniVozikuConfig.config.WEBAPI[0].Autorizace_DoAPI;
                    //tb_APIFASK_Adresa.Text = AgroSledovaniVozikuConfig.config.WEBAPI[0].Adresa;
                    //tb_APIFASK_AliasDB.Text = AgroSledovaniVozikuConfig.config.WEBAPI[0].AliasDB;
                    //chb_APIFASK_isHTTPS.Checked = AgroSledovaniVozikuConfig.config.WEBAPI[0].isHTTPS;
                    //tb_APIFASK_API_TimeOut.Text = AgroSledovaniVozikuConfig.config.WEBAPI[0].API_TimeOut.ToString();
                    //chb_APIFASK_Enable.Checked = AgroSledovaniVozikuConfig.config.WEBAPI[0].Enable;

#endregion

                }
                catch (Exception ex)
                {
                    //ErrorLog.Log.WriteException(ex);
                    ExceptionHandler2.Handle(ex);
                }


            }
            catch (Exception ex)
            {

                //Log.Write(ex);
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
                    //adam_1.DataReady -= new ADAM.ADAM_60XX.AdamEventHandler(adam_1.SaveStates4);
                    //adam_1.DataReady += new ADAM.ADAM_60XX.AdamEventHandler(adam_1.SaveStates4);

                    //adam_1.DataReady_SaveData -= new ADAM.ADAM_60XX.AdamEventHandler(adam_1.SaveStates4);
                    //adam_1.DataReady_SaveData += new ADAM.ADAM_60XX.AdamEventHandler(adam_1.SaveStates4);

                    adam_1.DataReady_NabeznaHrana -= new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_1);
                    adam_1.DataReady_NabeznaHrana += new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_1);

                    adam_1.Start();
                }

                if (adam_2 != null)
                {
                    //adam_2.DataReady -= new ADAM.ADAM_60XX.AdamEventHandler(adam_2.SaveStates4);
                    //adam_2.DataReady += new ADAM.ADAM_60XX.AdamEventHandler(adam_2.SaveStates4);

                    //adam_2.DataReady_SaveData -= new ADAM.ADAM_60XX.AdamEventHandler(adam_2.SaveStates4);
                    //adam_2.DataReady_SaveData += new ADAM.ADAM_60XX.AdamEventHandler(adam_2.SaveStates4);

                    adam_2.DataReady_NabeznaHrana -= new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_2);
                    adam_2.DataReady_NabeznaHrana += new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_2);
                   
                    adam_2.Start();
                }

                if (adam_3 != null)
                {

                    //adam_3.DataReady -= new ADAM.ADAM_60XX.AdamEventHandler(adam_3.SaveStates4);
                    //adam_3.DataReady += new ADAM.ADAM_60XX.AdamEventHandler(adam_3.SaveStates4);

                    //adam_3.DataReady_SaveData -= new ADAM.ADAM_60XX.AdamEventHandler(adam_3.SaveStates4);
                    //adam_3.DataReady_SaveData += new ADAM.ADAM_60XX.AdamEventHandler(adam_3.SaveStates4);

                    adam_3.DataReady_NabeznaHrana -= new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_3);
                    adam_3.DataReady_NabeznaHrana += new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_3);


                    adam_3.Start();
                }

            }
            catch(Exception ex)
            {
                //Log.Write(ex);
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
                    //adam_1.DataReady -= new ADAM.ADAM_60XX.AdamEventHandler(adam_1.SaveStates4);
                    //adam_1.DataReady_SaveData -= new ADAM.ADAM_60XX.AdamEventHandler(adam_1.SaveStates4);
                    adam_1.Stop();
                }

                if (adam_2 != null)
                {
                    adam_2.DataReady_NabeznaHrana -= new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_2);
                    //adam_2.DataReady -= new ADAM.ADAM_60XX.AdamEventHandler(adam_2.SaveStates4);
                    //adam_2.DataReady_SaveData -= new ADAM.ADAM_60XX.AdamEventHandler(adam_2.SaveStates4);
                    adam_2.Stop();
                }

                if (adam_3 != null)
                {
                    adam_3.DataReady_NabeznaHrana -= new ADAM.ADAM_60XX.AdamEventHandler(Adam_DataReady_3);
                    //adam_3.DataReady -= new ADAM.ADAM_60XX.AdamEventHandler(adam_3.SaveStates4);
                    //adam_3.DataReady_SaveData -= new ADAM.ADAM_60XX.AdamEventHandler(adam_3.SaveStates4);
                    adam_3.Stop();
                }
            }
            catch(Exception ex)
            {
                //Log.Write(ex);
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
                //Exceptions.Handler.ErrorHandle(ex.Message, "Adam_DataReady", false);
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
                //Exceptions.Handler.ErrorHandle(ex.Message, "Adam_DataReady", false);
                ExceptionHandler2.Handle(ex);
            }
        }
        private void Adam_DataReady_3(ADAM.ADAM_60XX.AdamEventHandlerArgs e)
        {
            try
            {
                this.BeginInvoke(new AdamSensorDelegate(AdamSensorDataReady_3), new object[] { e.Data });
            }
            catch (Exception ex)
            {
                //Exceptions.Handler.ErrorHandle(ex.Message, "Adam_DataReady", false);
                ExceptionHandler2.Handle(ex);
            }
        }


        /// <summary>
        /// logika pohybu palet na zaklade sepnutych signalu z ADAM - vetev nakladka
        /// </summary>
        /// <param name="sensor"></param>
        private void AdamSensorDataReady_1(int sensor)
        {
            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "AdamSensorDataReady_1");
            Fask.Tracing.Trac.Write("START metody AdamSensorDataReady_1", tracId);

            #region logika pohybu palet na zaklade sepnutych signalu z ADAM - vetev nakladka
            ADAM1_signal = true;


            try
            {
                if (sensor == AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_RUN)
                {
                    Fask.Tracing.Trac.Write("START sensor == Linka1_RUN", tracId); 

                    log_hlaska = string.Format("ADAM_1 - Linka1_RUN");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    #region logika fronty NAKLADKA Linka 1
#if false
                    //Nakladka_Buffer = new Queue<FE_pom>();
                    Nakladka_data = new FE_pom();

                    lock (Nakladka_data_object)
                    {
                        //inicializace objektu nakladky
                        Nakladka_data.dateeve = DateTime.Now;
                        Nakladka_data.machine = 1;
                        Nakladka_data.status = 0;
                    }

                    //prace s frontou nakladky

                    Nakladka_Buffer.Enqueue(Nakladka_data); 
#endif
#endregion

                    #region handshake L3 a L4

                    if (AgroSledovaniVozikuConfig.config.Vykladka[0].zapnuti_zalozni_logiky)
                    {
                        if (interniCisloLinka_matice != 0)
                        {
                            lock (interniCisloLinka_matice_object)
                            {
                                interniCisloLinka_matice = 0;
                            }
                        }
                        else
                        {

                            log_hlaska = string.Format("ADAM_1 - Linka1_RUN--ERROR - nebyla vykladka??");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                        }
                    }

#endregion

                    //plneni interni promenne linky
                    if (interniCisloLinka == 0)
                    {
                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = AGRO.linka_1;
                        }
                    }
                    else
                    {

                        //log_hlaska = string.Format("NAKLADKA_INFO_Linka_[{0}] nebyla vykladka? neni vycistena interniCisloLinka({1})!!", AGRO.linka_1, interniCisloLinka);
                        log_hlaska = string.Format("NAKLADKA_INFO - nedoslo k vykladce z linky: {1}! nakladam na lince: {0}", AGRO.linka_1, interniCisloLinka);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        int akce_nakladka = 7;
                        LV.OnLogsEvent(new Logs_EventArgs(0, akce_nakladka, log_hlaska, LoginID, AGRO.status_0));

                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = AGRO.linka_1;
                        }
                    }

                    #region obsluzne rutiny - 1) zapnuti RFID cteni/zapis

                    OnRFIDEvent(new RFID_EventArgs(AGRO.linka_1));

                    #endregion

                    if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    {
                        trida.Clear();
                        trida.Linka1_run = true;
                        trida.Zapis = true;
                        trida.color = Color.Blue;
                        pojezdUC1.Trida = trida;
                        pojezdUC1.Draw();
                    }

                    Fask.Tracing.Trac.Write("END sensor == Linka1_RUN", tracId); 

                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_RUN)
                {

                    Fask.Tracing.Trac.Write("START sensor == Linka2_RUN", tracId); 


                    //Log.Write(string.Format("ADAM_1 - Linka2_RUN"));
                    log_hlaska = string.Format("ADAM_1 - Linka2_RUN");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");



                    #region hanshake L3 a L4

                    if (AgroSledovaniVozikuConfig.config.Vykladka[0].zapnuti_zalozni_logiky)
                    {
                        if (interniCisloLinka_matice != 0)
                        {
                            lock (interniCisloLinka_matice_object)
                            {
                                interniCisloLinka_matice = 0;
                            }
                        }
                        else
                        {
                            log_hlaska = string.Format("ADAM_1 - Linka2_RUN--ERROR - nebyla vykladka??");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                        }
                    }

#endregion

                    if (interniCisloLinka == 0)
                    {
                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = AGRO.linka_2;
                        }
                    }
                    else
                    {
                        //Log.Write(string.Format("NAKLADKA_INFO_Linka_[{0}] nebyla vykladka? neni vycistena interniCisloLinka!!", 1));
                        log_hlaska = string.Format("NAKLADKA_INFO - nedoslo k vykladce z linky: {1}! nakladam na lince: {0}", AGRO.linka_2, interniCisloLinka);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        int akce_nakladka = 7;
                        LV.OnLogsEvent(new Logs_EventArgs(0, akce_nakladka, log_hlaska, LoginID, AGRO.status_0));

                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = AGRO.linka_2;
                        }
                    }

         

                    #region obsluzne rutiny - 1) zapnuti RFID cteni/zapis 

                    OnRFIDEvent(new RFID_EventArgs(AGRO.linka_2));

                    #endregion

                    //vizualizace(true, "adam");
                    if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    {
                        trida.Clear();
                        trida.Linka2_run = true;
                        trida.Zapis = true;
                        trida.color = Color.Blue;
                        pojezdUC1.Trida = trida;
                        pojezdUC1.Draw();
                    }

                    Fask.Tracing.Trac.Write("END sensor == Linka2_RUN", tracId); 

                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_RUN)
                {


                    Fask.Tracing.Trac.Write("START sensor == Linka3_RUN", tracId); 


                    //Log.Write(string.Format("ADAM_1 - Linka3_RUN"));
                    log_hlaska = string.Format("ADAM_1 - Linka3_RUN");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


                    #region hanshake L3 a L4

                    if (AgroSledovaniVozikuConfig.config.Vykladka[0].zapnuti_zalozni_logiky)
                    {
                        if (interniCisloLinka_matice != 0)
                        {
                            lock (interniCisloLinka_matice_object)
                            {
                                interniCisloLinka_matice = 0;
                            }
                        }
                        else
                        {
                            log_hlaska = string.Format("ADAM_1 - Linka3_RUN--ERROR - nebyla vykladka??");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                        }
                    }

#endregion

                    if (interniCisloLinka == 0)
                    {
                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = AGRO.linka_3;
                        }
                    }
                    else
                    {
                        //Log.Write(string.Format("NAKLADKA_INFO_Linka_[{0}] nebyla vykladka? neni vycistena interniCisloLinka!!", 1));
                        log_hlaska = string.Format("NAKLADKA_INFO - nedoslo k vykladce z linky: {1}! nakladam na lince: {0}", AGRO.linka_3, interniCisloLinka);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        int akce_nakladka = 7;
                        LV.OnLogsEvent(new Logs_EventArgs(0, akce_nakladka, log_hlaska, LoginID, AGRO.status_0));

                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = AGRO.linka_3;
                        }
                    }

          

                    #region obsluzne rutiny - 1) zapnuti RFID cteni/zapis 

                    OnRFIDEvent(new RFID_EventArgs(AGRO.linka_3));

                    #endregion

                    //vizualizace(true, "adam");
                    if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    {
                        trida.Clear();
                        trida.Linka3_run = true;
                        trida.Zapis = true;
                        trida.color = Color.Blue;
                        pojezdUC1.Trida = trida;
                        pojezdUC1.Draw();
                    }

                    Fask.Tracing.Trac.Write("END sensor == Linka3_RUN", tracId); 

                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_OK)
                {

                    Fask.Tracing.Trac.Write("START sensor == RucniVstup_OK", tracId); 


                    //Log.Write(string.Format("ADAM_1 - RucniVstup_OK"));
                    log_hlaska = string.Format("ADAM_1 - RucniVstup_OK");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


                    #region hanshake L3 a L4

                    if (AgroSledovaniVozikuConfig.config.Vykladka[0].zapnuti_zalozni_logiky)
                    {
                        if (interniCisloLinka_matice != 0)
                        {
                            lock (interniCisloLinka_matice_object)
                            {
                                interniCisloLinka_matice = 0;
                            }
                        }
                        else
                        {
                            log_hlaska = string.Format("ADAM_1 - RucniVstup_OK--ERROR - nebyla vykladka??");
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                        }
                    }

#endregion

                    if (interniCisloLinka == 0)
                    {
                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = AGRO.rucni_vstup; 
                        }
                    }
                    else
                    {
                        //Log.Write(string.Format("NAKLADKA_INFO_Linka_[{0}] nebyla vykladka? neni vycistena interniCisloLinka!!", 1));
                        log_hlaska = string.Format("NAKLADKA_INFO - nedoslo k vykladce z linky: {1}! nakladam na lince: {0} -rucni vstup", AGRO.rucni_vstup, interniCisloLinka);
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        int akce_nakladka = 7;
                        LV.OnLogsEvent(new Logs_EventArgs(0, akce_nakladka, log_hlaska, LoginID, AGRO.status_0));

                        lock (interniCisloLinka_object)
                        {
                            interniCisloLinka = AGRO.rucni_vstup; 
                        }
                    }


                    #region obsluzne rutiny - 1) zapnuti RFID cteni/zapis 
                    OnRFIDEvent(new RFID_EventArgs(AGRO.rucni_vstup));
                    #endregion

                    if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    {
                        trida.Clear();
                        trida.rucVst_run = true;
                        trida.Zapis = true;
                        trida.color = Color.Blue;
                        pojezdUC1.Trida = trida;
                        pojezdUC1.Draw();
                    }

                    Fask.Tracing.Trac.Write("END sensor == RucniVstup_OK", tracId); 

                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec_vyber)
                {

                    Fask.Tracing.Trac.Write("START sensor == Strec_vyber", tracId); 


                    if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    {
                        //trida.Clear();
                        //trida.strec1_run = true;
                        //trida.color = Color.Orange;
                        trida.PojezdStrec = true;
                        pojezdUC1.Trida = trida;
                        pojezdUC1.Draw();
                        //vizualizace(true, "strec1");
                    }

                    Fask.Tracing.Trac.Write("END sensor == Strec_vyber", tracId); 

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
        /// logika pohybu palet na zaklade sepnutych signalu z ADAM - vetev vykladka - bocedi 1
        /// </summary>
        /// <param name="sensor"></param>
        private void AdamSensorDataReady_2(int sensor)
        {
            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "AdamSensorDataReady_2");
            Fask.Tracing.Trac.Write("START metody AdamSensorDataReady_2", tracId);

            #region logika pohybu palet na zaklade sepnutych signalu z ADAM - vetev vykladka - bocedi 1

            ADAM2_signal = true;

            try
            {
                bool var_L2 = false;
                bool var_L3 = false;
                bool var_L4 = false;

                #region zjistuji jestli je vaha v erroru

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
                        //bool[] vaha1_ERROR_pole = adam_2.getDIStatus();
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

                #endregion

                if (sensor == AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Strec1_OK)
                {
                    Fask.Tracing.Trac.Write("START sensor == Strec1_OK", tracId); 

                    log_hlaska = string.Format("ADAM_2 - Strec1_OK");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    lock (OnlineLog_Bocedi_1_object)
                    {
                        OnlineLog_Bocedi_1 = false;
                    }

                    #region handshake vozik L3 && L4 -- cislo linky odkud nalozil na vozik
                    try
                    {
                        if (AgroSledovaniVozikuConfig.config.Vykladka[0].zapnuti_zalozni_logiky)
                        {
                            if (AgroSledovaniVozikuConfig.config.ADAM_1[0].communication_P2P)
                            {
                                //bool[] vaha1_ERROR_pole = adam_2.getDIStatus();
                                if (adam_1.actualState_DI != null && adam_1.actualState_DI.Length >= AgroSledovaniVozikuConfig.config.ADAM_DI[0].L2)
                                {
                                    bool[] L2_pole = adam_1.actualState_DI;
                                    if (L2_pole != null)
                                    {
                                        var_L2 = L2_pole[AgroSledovaniVozikuConfig.config.ADAM_DI[0].L2];
                                    }
                                }

                                if (adam_1.actualState_DI != null && adam_1.actualState_DI.Length >= AgroSledovaniVozikuConfig.config.ADAM_DI[0].L3)
                                {
                                    bool[] L3_pole = adam_1.actualState_DI;
                                    if (L3_pole != null)
                                    {
                                        var_L3 = L3_pole[AgroSledovaniVozikuConfig.config.ADAM_DI[0].L3];
                                    }
                                }

                                if (adam_1.actualState_DI != null && adam_1.actualState_DI.Length >= AgroSledovaniVozikuConfig.config.ADAM_DI[0].L4)
                                {
                                    bool[] L4_pole = adam_1.actualState_DI;
                                    if (L4_pole != null)
                                    {
                                        var_L4 = L4_pole[AgroSledovaniVozikuConfig.config.ADAM_DI[0].L4];
                                    }
                                }

                            

                                //kontrola vycisteni promenne
                                if (interniCisloLinka_matice != 0)
                                {

                                    log_hlaska = string.Format("VYKLADKA - ADAM_2 - strec:1 - L3+L4 promenna nebyla vycistena!! hodnota:{0}", interniCisloLinka_matice);
                                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");


                                    lock (OnlineLog_Bocedi_1_object)
                                    {
                                        OnlineLog_Bocedi_1 = true;
                                    }
                                    int akce = 3;
                                    string popisek = log_hlaska;
                                    LV.OnLogsEvent(new Logs_EventArgs(Fask.Constants.AGRO.bocedi_1, akce, popisek, LoginID, 0));

                                }

                                if (var_L2 == true && var_L3 == false && var_L4 == false)
                                {
                                    lock (interniCisloLinka_matice_object)
                                    {
                                        interniCisloLinka_matice = AGRO.rucni_vstup;
                                    }
                                }
                                else if (var_L3 == false && var_L4 == true)
                                {
                                    lock (interniCisloLinka_matice_object)
                                    {
                                        interniCisloLinka_matice = AGRO.linka_1;
                                    }
                                }
                                else if (var_L3 == true && var_L4 == false)
                                {
                                    lock (interniCisloLinka_matice_object)
                                    {
                                        interniCisloLinka_matice = AGRO.linka_2;
                                    }
                                }
                                else if (var_L3 == true && var_L4 == true)
                                {
                                    lock (interniCisloLinka_matice_object)
                                    {
                                        interniCisloLinka_matice = AGRO.linka_3;
                                    }
                                }

                            }

                            log_hlaska = string.Format("ADAM_2 - prichozi paleta z LINKY-{0}", interniCisloLinka_matice);
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        }
                    }
                    catch (Exception ex)
                    {

                        ExceptionHandler2.Handle(ex);
                    }
                    #endregion

                    #region obsluzne rutiny - 1) zapnuti RFID cteni/zapis - 2) paralelni casovac, kdyby selhalo cteni RFID

                    OnRFIDEvent(new RFID_EventArgs(AGRO.bocedi_1));
                    timer_strec_1.Change(AgroSledovaniVozikuConfig.config.Vykladka[0].TimeEventStrec, System.Threading.Timeout.Infinite);

                    #endregion

                    if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    {
                        trida.Clear();
                        trida.strec1_run = true;
                        trida.Zapis = true;
                        trida.color = Color.Blue;
                        trida.tagText = interniCisloLinka.ToString("00");
                        pojezdUC1.Trida = trida;
                        pojezdUC1.Draw();
                    }

                    Fask.Tracing.Trac.Write("END sensor == Strec1_OK", tracId); 

                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Bocedi1_OUT)
                {
                    Fask.Tracing.Trac.Write("START sensor == Bocedi1_OUT", tracId); 

                    log_hlaska = string.Format("ADAM_2 - Bocedi1_OUT");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    //pokud je system bez vahy, posilam data tiskarum
                    if (!uC_Vaha.Vaha_1_Connected() || vaha_1_ERROR)
                    {
#if DEBUG
                        //Log.Write(string.Format("Bocedi1_OUT - start logiky tisk status 21"));
                        log_hlaska = string.Format("Bocedi1_OUT - start logiky tisk status 21");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif
                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_21));


                        ////vizualizace vykladka ctverecek 4
                        ////vyhledej zaznam se statusem 41, zacni hledat az po x sekundach
                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_41));
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

#if DEBUG
                        //Log.Write(string.Format("Vaha1_END - start logiky tisk status 31"));
                        log_hlaska = string.Format("Vaha1_END - start logiky tisk status 31");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_31));

                        //vizualizace vykladka ctverecek 4
                        //vyhledej zaznam se statusem 41, zacni hledat az po x sekundach
                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_41));
                    }

                    Fask.Tracing.Trac.Write("END sensor == Vaha1_END", tracId); 

                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Tisk1_END)
                {
                    Fask.Tracing.Trac.Write("START sensor == Tisk1_END", tracId); 

                    log_hlaska = string.Format("ADAM_2 - Tisk1_END");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    OnRFIDEvent(new RFID_EventArgs(AGRO.status_61));

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

        /// <summary>
        /// logika pohybu palet na zaklade sepnutych signalu z ADAM - vetev vykladka - bocedi 2
        /// </summary>
        /// <param name="sensor"></param>
        private void AdamSensorDataReady_3(int sensor)
        {
            Fask.Tracing.TracId tracId = new Fask.Tracing.TracId(null, null, null, "AdamSensorDataReady_3");
            Fask.Tracing.Trac.Write("START metody AdamSensorDataReady_3", tracId);

            #region logika pohybu palet na zaklade sepnutych signalu z ADAM - vetev vykladka - bocedi 2

            ADAM3_signal = true;


            try
            {
                bool var_L2 = false;
                bool var_L3 = false;
                bool var_L4 = false;

                #region zjistuji jestli je vaha v erroru

                try
                {
                    if (AgroSledovaniVozikuConfig.config.ADAM_3[0].communication_TCP)
                    {
                        bool[] vaha2_ERROR_pole = adam_3.getDIStatus(); // z nejakeho duvodu pri drzeni DI_ERROR na true a zaroven prichoziho signalu z jineho pinu se vyhodnoti DI_ERROR na false!!! POZOR!!
                        if (vaha2_ERROR_pole != null)
                        {
                            vaha_2_ERROR = vaha2_ERROR_pole[AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Vaha2_ERROR];
                        }
                    }
                    else if (AgroSledovaniVozikuConfig.config.ADAM_3[0].communication_P2P)
                    {
                        //bool[] vaha1_ERROR_pole = adam_2.getDIStatus();
                        if (adam_3.actualState_DI != null && adam_3.actualState_DI.Length >= AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Vaha2_ERROR)
                        {
                            bool[] vaha2_ERROR_pole = adam_3.actualState_DI;
                            if (vaha2_ERROR_pole != null)
                            {
                                vaha_2_ERROR = vaha2_ERROR_pole[AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Vaha2_ERROR];
                            }
                        }

                    }
                }
                catch (Exception ex)
                {

                    ExceptionHandler2.Handle(ex);
                }

                #endregion

                if (sensor == AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Strec2_OK)
                {
                    Fask.Tracing.Trac.Write("START sensor == Strec2_OK", tracId);

                    log_hlaska = string.Format("ADAM_3 - Strec2_OK");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    //inicializace logovani online promenne
                    lock (OnlineLog_Bocedi_2_object)
                    {
                        OnlineLog_Bocedi_2 = false;
                    }

                    #region handshake vozik L3 && L4 -- cislo linky odkud nalozil na vozik
                    try
                    {
                        if (AgroSledovaniVozikuConfig.config.Vykladka[0].zapnuti_zalozni_logiky)
                        {
                            if (AgroSledovaniVozikuConfig.config.ADAM_1[0].communication_P2P)
                            {
                                //bool[] vaha1_ERROR_pole = adam_2.getDIStatus();
                                if (adam_1.actualState_DI != null && adam_1.actualState_DI.Length >= AgroSledovaniVozikuConfig.config.ADAM_DI[0].L2)
                                {
                                    bool[] L2_pole = adam_1.actualState_DI;
                                    if (L2_pole != null)
                                    {
                                        var_L2 = L2_pole[AgroSledovaniVozikuConfig.config.ADAM_DI[0].L2];
                                    }
                                }

                                if (adam_1.actualState_DI != null && adam_1.actualState_DI.Length >= AgroSledovaniVozikuConfig.config.ADAM_DI[0].L3)
                                {
                                    bool[] L3_pole = adam_1.actualState_DI;
                                    if (L3_pole != null)
                                    {
                                        var_L3 = L3_pole[AgroSledovaniVozikuConfig.config.ADAM_DI[0].L3];
                                    }
                                }

                                if (adam_1.actualState_DI != null && adam_1.actualState_DI.Length >= AgroSledovaniVozikuConfig.config.ADAM_DI[0].L4)
                                {
                                    bool[] L4_pole = adam_1.actualState_DI;
                                    if (L4_pole != null)
                                    {
                                        var_L4 = L4_pole[AgroSledovaniVozikuConfig.config.ADAM_DI[0].L4];
                                    }
                                }


                                //kontrola vycisteni promenne
                                if (interniCisloLinka_matice != 0)
                                {

                                    log_hlaska = string.Format("VYKLADKA - ADAM_3 - strec:2 - L3+L4 promenna nebyla vycistena!! hodnota:{0}", interniCisloLinka_matice);
                                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                                    lock (OnlineLog_Bocedi_2_object)
                                    {
                                        OnlineLog_Bocedi_2 = true;
                                    }

                                    int akce = 3;
                                    string popisek = log_hlaska;
                                    LV.OnLogsEvent(new Logs_EventArgs(Fask.Constants.AGRO.bocedi_2, akce, popisek, LoginID, 0));



                                }

                                if (var_L2 == true && var_L3 == false && var_L4 == false)
                                {
                                    lock (interniCisloLinka_matice_object)
                                    {
                                        interniCisloLinka_matice = AGRO.rucni_vstup;
                                    }
                                }
                                else if (var_L3 == false && var_L4 == true)
                                {
                                    lock (interniCisloLinka_matice_object)
                                    {
                                        interniCisloLinka_matice = AGRO.linka_1;
                                    }
                                }
                                else if (var_L3 == true && var_L4 == false)
                                {
                                    lock (interniCisloLinka_matice_object)
                                    {
                                        interniCisloLinka_matice = AGRO.linka_2;
                                    }
                                }
                                else if (var_L3 == true && var_L4 == true)
                                {
                                    lock (interniCisloLinka_matice_object)
                                    {
                                        interniCisloLinka_matice = AGRO.linka_3;
                                    }
                                }

                            }

                            log_hlaska = string.Format("ADAM_3 - prichozi paleta z LINKY-{0}", interniCisloLinka_matice);
                            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        }
                    }
                    catch (Exception ex)
                    {

                        ExceptionHandler2.Handle(ex);
                    }
                    #endregion

                    #region obsluzne rutiny - 1) zapnuti RFID cteni/zapis - 2) paralelni casovac, kdyby selhalo cteni RFID

                    OnRFIDEvent(new RFID_EventArgs(AGRO.bocedi_2));
                    timer_strec_2.Change(AgroSledovaniVozikuConfig.config.Vykladka[0].TimeEventStrec, System.Threading.Timeout.Infinite);

                    #endregion

                    #region vizualizace pohybu palet

                    if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    {
                        trida.Clear();
                        trida.strec2_run = true;
                        trida.Zapis = true;
                        trida.color = Color.Blue;
                        trida.tagText = interniCisloLinka.ToString("00");
                        pojezdUC1.Trida = trida;
                        pojezdUC1.Draw();
                    }

                    #endregion

                    Fask.Tracing.Trac.Write("END sensor == Strec2_OK", tracId);

                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Bocedi2_OUT)
                {
                    Fask.Tracing.Trac.Write("START sensor == Bocedi2_OUT", tracId);

                    log_hlaska = string.Format("ADAM_3 - Bocedi2_OUT");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    //pokud je system bez vahy, posilam data tiskarum
                    if (!uC_Vaha.Vaha_2_Connected() || vaha_2_ERROR)
                    { 
                        log_hlaska = string.Format("Bocedi2_OUT - start logiky tisk status 22");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_22));

                        ////vizualizace vykladka ctverecek 4
                        ////vyhledej zaznam se statusem 41, zacni hledat az po x sekundach
                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_42));
                    }

                    Fask.Tracing.Trac.Write("END sensor == Bocedi2_OUT", tracId);
                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Vaha2_OK)
                {
                    Fask.Tracing.Trac.Write("START sensor == Vaha2_OK", tracId);

                    log_hlaska = string.Format("ADAM_3 - Vaha2_OK");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    lock (vaha_2_data_object)
                    {
                        vaha_2_data = true;
                    }

                    timer_vaha_2.Change(AgroSledovaniVozikuConfig.config.Vykladka[0].Time_Vaha2, System.Threading.Timeout.Infinite);

                    Fask.Tracing.Trac.Write("END sensor == Vaha2_OK", tracId);
                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Vaha2_END)
                {
                    Fask.Tracing.Trac.Write("START sensor == Vaha2_END", tracId);

                    log_hlaska = string.Format("ADAM_3 - Vaha2_END");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    if (vaha_2_data && !vaha_2_ERROR)
                    {

#if DEBUG
                        //Log.Write(string.Format("Vaha2_END - start logiky tisk status 32"));
                        log_hlaska = string.Format("Vaha2_END - start logiky tisk status 32");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
#endif

                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_32));

                        //vizualizace vykladka ctverecek 4
                        //vyhledej zaznam se statusem 41, zacni hledat az po x sekundach
                        OnRFIDEvent(new RFID_EventArgs(AGRO.status_42));
                    }

                    Fask.Tracing.Trac.Write("END sensor == Vaha2_END", tracId);
                }
                else if (sensor == AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Tisk2_END)
                {
                    Fask.Tracing.Trac.Write("START sensor == Tisk2_END", tracId);

                    log_hlaska = string.Format("ADAM_3 - Tisk2_END");
                    ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

                    OnRFIDEvent(new RFID_EventArgs(AGRO.status_62));

                    Fask.Tracing.Trac.Write("END sensor == Tisk2_END", tracId);
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
            } 

            #endregion

            Fask.Tracing.Trac.Write("END metody AdamSensorDataReady_3", tracId); 
        }

#endregion

        #region UpdateForm
        private void updateForm(bool input, string source)
        {
            try
            {
                if (this.InvokeRequired)
                {   //kvuli odvodu mimo smenu...jinak threadsafe exception....
                    this.BeginInvoke(new MethodInvoker(() => { updateForm(input, source); }));
                    return;
                }


#region UC inicializace + deklarace ADAM DI 
                // DI5,DI6,DI7,DI8--DI10,DI11 (TRUE/FALSE)

                //vaha_1 = uC_Vaha.Vaha_1_Connected();
                //vaha_2 = uC_Vaha.Vaha_2_connected();

                bool Strec1_OK = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_OK, vyber_adam_1);
                bool Strec1_Bit1 = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_Bit1, vyber_adam_1);
                bool Strec1_Bit2 = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_Bit2, vyber_adam_1);
                bool Strec1_Bit4 = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_Bit4, vyber_adam_1);
                bool Linka1_CALL = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_CALL, vyber_adam_1);
                bool Linka1_END = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_END, vyber_adam_1);
                bool Linka1_OK = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_OK, vyber_adam_1);
                bool Linka1_RUN = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_RUN, vyber_adam_1);

                bool Linka2_CALL = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_CALL, vyber_adam_1);
                bool Linka2_END = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_END, vyber_adam_1);
                bool Linka2_OK = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_OK, vyber_adam_1);
                bool Linka2_RUN = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_RUN, vyber_adam_1);

                bool Linka3_CALL = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_CALL, vyber_adam_1);
                bool Linka3_END = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_END, vyber_adam_1);
                bool Linka3_OK = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_OK, vyber_adam_1);
                bool Linka3_RUN = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_RUN, vyber_adam_1);

                bool RucniVstup_CALL = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_CALL, vyber_adam_1);
                bool RucniVstup_END = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_END, vyber_adam_1);
                bool RucniVstup_OK = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_OK, vyber_adam_1);
                bool RucniVstup_RUN = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_RUN, vyber_adam_1);

                bool Strec2_OK = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec2_OK, vyber_adam_2);

                bool L1_val = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].L1, vyber_adam_1);
                bool L2_val = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].L2, vyber_adam_1);
                bool L3_val = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].L3, vyber_adam_1);
                bool L4_val = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].L4, vyber_adam_1);

#endregion

#region L1-L4 a smer -- interpretace
                //bool L1 = false;
                //bool L2 = false;
                //bool L3 = false;
                //bool L4 = false;
                //bool Smer = false;

                //L1 = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].L1, vyber_adam_1);
                //L2 = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].L2, vyber_adam_1);
                //L3 = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].L3, vyber_adam_1);
                //L4 = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].L4, vyber_adam_1);
                //Smer = GetAdamDI(AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec_vyber, vyber_adam_1);

                //if (!L1 && !L2 && !L3 && L4 && Smer)
                //{
                //    Log.Write(string.Format("VYKLADKA STR_1 z linky: 1"));
                //}
                //else if (L1 && !L2 && !L3 && L4 && !Smer)
                //{
                //    Log.Write(string.Format("VYKLADKA STR_2 z linky: 1"));
                //}
                //else if (!L1 && !L2 && L3 && !L4 && Smer)
                //{
                //    Log.Write(string.Format("VYKLADKA STR_1 z linky: 2"));
                //}
                //else if (L1 && !L2 && L3 && !L4 && !Smer)
                //{
                //    Log.Write(string.Format("VYKLADKA STR_2 z linky: 2"));
                //}
                //else if (!L1 && !L2 && L3 && L4 && Smer)
                //{
                //    Log.Write(string.Format("VYKLADKA STR_1 z linky: 3"));
                //}
                //else if (L1 && !L2 && L3 && L4 && !Smer)
                //{
                //    Log.Write(string.Format("VYKLADKA STR_2 z linky: 3"));
                //}
#endregion

#region UC

#region UC RFID
                if (RFID_Linky_IN != null)
                {
                    rfiD_UC1.RFID_1_Connected = true;
                    RFID_Antena_zobraz(AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka1, "reader_1");
                    RFID_Antena_zobraz(AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka2, "reader_1");
                    RFID_Antena_zobraz(AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka3, "reader_1");
                    RFID_Antena_zobraz(AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_4, "reader_1");
                }
                else
                {
                    rfiD_UC1.RFID_1_Connected = false;
                }

                if (RFID_Linky_OUT != null)
                {
                    rfiD_UC1.RFID_2_Connected = true;
                    RFID_Antena_zobraz(AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_strec1, "reader_2");
                    RFID_Antena_zobraz(AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_strec2, "reader_2");
                    RFID_Antena_zobraz(AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_rucniVstup, "reader_2");
                    RFID_Antena_zobraz(AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_4, "reader_2");
                }
                else
                {
                    rfiD_UC1.RFID_2_Connected = false;
                }
#endregion


#endregion

                if (adam_1 != null)
                {
                    if (adam_1.AdamTCP_Connected)
                    {
                        aDAM_UC_1._adamTCP_Connected = true;
                    }
                    else
                    {
                        aDAM_UC_1._adamTCP_Connected = false;
                    }

                    if (adam_1.AdamP2P_Started)
                    {
                        aDAM_UC_1._adamP2P_Started = true;
                    }
                    else
                    {
                        aDAM_UC_1._adamP2P_Started = false;
                    }


                    // vytazeni dat z adam
                    if (AgroSledovaniVozikuConfig.config.ADAM_1[0].communication_TCP)
                    {
                        aDAM_UC_1.DIData = adam_1.DIStatusLast;
                    }
                    else if (AgroSledovaniVozikuConfig.config.ADAM_1[0].communication_P2P)
                    {
                        if (adam_1.actualState_DI != null)
                        {
                            aDAM_UC_1.DIData = adam_1.actualState_DI;

                        }
                    }

                    aDAM_UC_1.updateForm();


                    //if (adam_1.DIStatusLast.Length > 0)
                    //{
                    //    #region zapis do DB
                    //    if (!SQL)
                    //    {
                    //        adam_1.SaveStates2();  //zapis do DB
                    //    }
                    //    else
                    //    {

                    //        adam_1.SaveStates3();  //zapis do DB 3vrstva architektura

                    //    }

                    //    #endregion
                    //}
                }

                if (adam_2 != null)
                {
                    if (adam_2.AdamTCP_Connected)
                    {
                        aDAM_UC_2._adamTCP_Connected = true;
                    }
                    else
                    {
                        aDAM_UC_2._adamTCP_Connected = false;
                    }

                    if (adam_2.AdamP2P_Started)
                    {
                        aDAM_UC_2._adamP2P_Started = true;
                    }
                    else
                    {
                        aDAM_UC_2._adamP2P_Started = false;
                    }


                    // vytazeni dat z adam
                    if (AgroSledovaniVozikuConfig.config.ADAM_2[0].communication_TCP)
                    {
                        aDAM_UC_2.DIData = adam_2.DIStatusLast;
                    }
                    else if (AgroSledovaniVozikuConfig.config.ADAM_2[0].communication_P2P)
                    {
                        if (adam_2.actualState_DI != null)
                        {
                            aDAM_UC_2.DIData = adam_2.actualState_DI;

                        }

                    }

                 
                    aDAM_UC_2.updateForm();


                    //if (adam_2.DIStatusLast.Length > 0)
                    //{

                    //    #region zapis do DB
                    //    if (!SQL)
                    //    {
                    //        adam_2.SaveStates2();  //zapis do DB
                    //    }
                    //    else
                    //    {

                    //        adam_2.SaveStates3();  //zapis do DB 3vrstva architektura

                    //    }

                    //    #endregion
                    //}
                }

                if (adam_3 != null)
                {
                    if (adam_3.AdamTCP_Connected)
                    {
                        aDAM_UC_3._adamTCP_Connected = true;
                    }
                    else
                    {
                        aDAM_UC_3._adamTCP_Connected = false;
                    }

                    if (adam_3.AdamP2P_Started)
                    {
                        aDAM_UC_3._adamP2P_Started = true;
                    }
                    else
                    {
                        aDAM_UC_3._adamP2P_Started = false;
                    }


                    // vytazeni dat z adam
                    if (AgroSledovaniVozikuConfig.config.ADAM_3[0].communication_TCP)
                    {
                        aDAM_UC_3.DIData = adam_3.DIStatusLast;
                    }
                    else if (AgroSledovaniVozikuConfig.config.ADAM_3[0].communication_P2P)
                    {
                        if (adam_3.actualState_DI != null)
                        {
                            aDAM_UC_3.DIData = adam_3.actualState_DI;

                        }
                    }
                    
                    aDAM_UC_3.updateForm();


                    //if (adam_3.DIStatusLast.Length > 0)
                    //{

                    //    #region zapis do DB
                    //    if (!SQL)
                    //    {
                    //        adam_3.SaveStates2();  //zapis do DB
                    //    }
                    //    else
                    //    {

                    //        adam_3.SaveStates3();  //zapis do DB 3vrstva architektura

                    //    }

                    //    #endregion
                    //}

                }


            }
            catch (Exception ex)
            {
                // TODO : vyjimka !!!
                //Log.Write(ex.Message.ToString());
                //Exceptions.Handler.ErrorHandle(ex.Message, "frmMainAgroVyroba.RefreshUI");
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
                    else if (vyberAdam == vyber_adam_3 && adam_3 != null)
                    {
                        if (adam_3.DIStatusLast.Length - 1 < pozice)
                            return false;
                        else
                            return adam_3.DIStatusLast[pozice];
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.WriteException(ex.Message);
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }

        private void RFID_Antena_zobraz(int pozice, string reader)
        {
            try
            {
                if (reader == "reader_1")
                {
                    switch (pozice)
                    {
                        case 1:
                            rfiD_UC1.ChB_R1_ANT1 = true;
                            break;
                        case 2:
                            rfiD_UC1.ChB_R1_ANT2 = true;
                            break;
                        case 3:
                            rfiD_UC1.ChB_R1_ANT3 = true;
                            break;
                        case 4:
                            rfiD_UC1.ChB_R1_ANT4 = true;
                            break;
                        default:
                            break;
                    }
                }
                if (reader == "reader_2")
                {
                    switch (pozice)
                    {
                        case 1:
                            rfiD_UC1.ChB_R2_ANT1 = true;
                            break;
                        case 2:
                            rfiD_UC1.ChB_R2_ANT2 = true;
                            break;
                        case 3:
                            rfiD_UC1.ChB_R2_ANT3 = true;
                            break;
                        case 4:
                            rfiD_UC1.ChB_R2_ANT4 = true;
                            break;
                        default:
                            break;
                    }
                }
                rfiD_UC1.updateForm();
            }
            catch (Exception ex)
            {
                //Exceptions.Handler.ErrorHandle(ex.Message, "RFID_Antena_zobraz", true);
                //Log.WriteException(ex.Message);
                ExceptionHandler2.Handle(ex);

            }
        }

        #region VIZUALIZACE

        private void vizualizace(bool input, string source)
        {
            try
            {

                if (this.InvokeRequired)
                {   //kvuli odvodu mimo smenu...jinak threadsafe exception....
                    this.BeginInvoke(new MethodInvoker(() => { vizualizace(input, source); }));
                    return;
                }

                if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                {
                    if (source == Const_Kontrola_Linky)
                    {
                        Thread.Sleep(1000);
                        trida.Zapis = false;
                        trida.color = Color.Orange;
                        trida.Pojezd = true;
                    }
                    else if (source == Const_Kontrola_Odvadeni)
                    {
                        Thread.Sleep(1000);
                        trida.Zapis = false;
                        trida.color = Color.Orange;
                        trida.Pojezd = true;
                    }

                    if (rfiD_UC1.Tag_Text_W != NenactenoEPC && rfiD_UC1.Tag_Text_W.Length == 24)
                        trida.tagText = rfiD_UC1.Tag_Text_W.Substring(0, 2);

                    pojezdUC1.Trida = trida;
                    pojezdUC1.Draw(); 
                }
            }
            catch (Exception ex)
            {

                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        public void vizualizace_vykladka(TridaDataProKresleni_vykladka trida)
        {
            try
            {

                if (this.InvokeRequired)
                {   //kvuli odvodu mimo smenu...jinak threadsafe exception....
                    this.BeginInvoke(new MethodInvoker(() => { vizualizace_vykladka(trida); }));
                    return;
                }

                if (AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                {
                    pojezdUC1.Trida_vykladka = trida;
                    pojezdUC1.Draw();
                }
            }
            catch (Exception ex)
            {

                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        public void Vizualizace_vykladka_vykresli(Fask.WEBAPI.API_BusinessObjects.FASK_Events list_FE, int cisloStrec)
        {

            try
            {
                if (!AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    return;

                //vykresleni zapis do trida_vykladka a pote vizobrazit
               if (list_FE.rows.Length >= 2 && cisloStrec == AGRO.bocedi_1)
                {
                    pojezdUC1.Trida_vykladka.Clear_strec1();
                    trida_vykladka.strec1_Text_1 = list_FE.rows.First().ITEMDESC.Substring(0, 10);
                    trida_vykladka.strec1_SSCC_1 = list_FE.rows.First().NMBRPAL.Substring(15, 5);

                    trida_vykladka.strec1_Text_2 = list_FE.rows[1].ITEMDESC.Substring(0, 10);
                    trida_vykladka.strec1_SSCC_2 = list_FE.rows[1].NMBRPAL.Substring(15, 5);
                }
                else if (list_FE.rows.Length == 1 && cisloStrec == AGRO.bocedi_1)
                {
                    pojezdUC1.Trida_vykladka.Clear_strec1();
                    trida_vykladka.strec1_Text_2 = list_FE.rows.First().ITEMDESC.Substring(0, 10);
                    trida_vykladka.strec1_SSCC_2 = list_FE.rows.First().NMBRPAL.Substring(15, 5);
                }
                else if (list_FE.rows.Length < 1 && cisloStrec == AGRO.bocedi_1)
                {
                    pojezdUC1.Trida_vykladka.Clear_strec1();
                }

              if (list_FE.rows.Length >= 2 && cisloStrec == AGRO.bocedi_2)
                {
                    pojezdUC1.Trida_vykladka.Clear_strec2();
                    trida_vykladka.strec2_Text_1 = list_FE.rows.First().ITEMDESC.Substring(0, 10);
                    trida_vykladka.strec2_SSCC_1 = list_FE.rows.First().NMBRPAL.Substring(15, 5);

                    trida_vykladka.strec2_Text_2 = list_FE.rows[1].ITEMDESC.Substring(0, 10);
                    trida_vykladka.strec2_SSCC_2 = list_FE.rows[1].NMBRPAL.Substring(15, 5);
                }
                else if (list_FE.rows.Length == 1 && cisloStrec == AGRO.bocedi_2)
                {
                    pojezdUC1.Trida_vykladka.Clear_strec2();
                    trida_vykladka.strec2_Text_2 = list_FE.rows.First().ITEMDESC.Substring(0, 10);
                    trida_vykladka.strec2_SSCC_2 = list_FE.rows.First().NMBRPAL.Substring(15, 5);

                }
                else if (list_FE.rows.Length < 1 && cisloStrec == AGRO.bocedi_2)
                {
                    pojezdUC1.Trida_vykladka.Clear_strec2();
                }

                vizualizace_vykladka(trida_vykladka);
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                //throw ex;
            }

        }

        public void Vizualizace_vykladka_tisk_vykresli(Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE, int cisloStrec)
        {

            try
            {
                if (!AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    return;

                //vykresleni zapis do trida_vykladka a pote vizobrazit
                if (FE != null && cisloStrec == AGRO.bocedi_1)
                {
                    if(!string.IsNullOrEmpty( FE.ITEMDESC))
                    trida_vykladka.strec1_Text_3 = FE.ITEMDESC.Substring(0, 10);
                    if (!string.IsNullOrEmpty(FE.NMBRPAL))
                        trida_vykladka.strec1_SSCC_3 = FE.NMBRPAL.Substring(15, 5);

                }
                else if(FE == null && cisloStrec == AGRO.bocedi_1)
                {
                    trida_vykladka.strec1_Text_3 = string.Empty;
                    trida_vykladka.strec1_SSCC_3 = string.Empty;

                }

                if (FE != null && cisloStrec == AGRO.bocedi_2)
                {
                    if (!string.IsNullOrEmpty(FE.ITEMDESC))
                        trida_vykladka.strec2_Text_3 = FE.ITEMDESC.Substring(0, 10);
                    if (!string.IsNullOrEmpty(FE.NMBRPAL))
                        trida_vykladka.strec2_SSCC_3 = FE.NMBRPAL.Substring(15, 5);
                }
                else if (FE == null && cisloStrec == AGRO.bocedi_2)
                {
                    trida_vykladka.strec2_Text_3 = string.Empty;
                    trida_vykladka.strec2_SSCC_3 = string.Empty;

                }

                vizualizace_vykladka(trida_vykladka);
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                //throw ex;
            }

        }

        public bool Vizualizace_vykladka_logika(int cisloBocedi)
        {
            try
            {

                if (!AgroSledovaniVozikuConfig.config.Vizualizace[0].Enable)
                    return true;


                    if (cisloBocedi == 1)
                {
                    Fask.WEBAPI.API_BusinessObjects.FASK_Events list_FE = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();
                    list_FE = LV.Vizualizace_vykladka(AGRO.status_21);
                    Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_zaznam = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                    FE_zaznam = LV.Najdi_zaznam(AGRO.status_41, AGRO.status_51);

                    //vykreslim data
                    if (list_FE != null)
                    {
                        Vizualizace_vykladka_vykresli(list_FE, AGRO.bocedi_1);
                    }
                    else
                    {
                        //Log.Write(string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!"));
                        log_hlaska = string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }

                    if (FE_zaznam != null)
                    {
                        Vizualizace_vykladka_tisk_vykresli(FE_zaznam, AGRO.bocedi_1);
                    }
                    else
                    {
                        Vizualizace_vykladka_tisk_vykresli(null, AGRO.bocedi_1);
                        //Log.Write(string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!"));
                        log_hlaska = string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }
                    return true;
                }
                else if(cisloBocedi == 2)
                { //najdu data
                    Fask.WEBAPI.API_BusinessObjects.FASK_Events list_FE = new Fask.WEBAPI.API_BusinessObjects.FASK_Events();
                    list_FE = LV.Vizualizace_vykladka(AGRO.status_22);
                    Fask.WEBAPI.API_BusinessObjects.FASK_Events_row FE_zaznam = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();
                    FE_zaznam = LV.Najdi_zaznam(AGRO.status_42, AGRO.status_52);

                    //vykreslim data
                    if (list_FE != null)
                    {
                        Vizualizace_vykladka_vykresli(list_FE, AGRO.bocedi_2);
                    }
                    else
                    {
                        //Log.Write(string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!"));
                        log_hlaska = string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }

                    if (FE_zaznam != null)
                    {
                        Vizualizace_vykladka_tisk_vykresli(FE_zaznam, AGRO.bocedi_2);
                    }
                    else
                    {
                        //Log.Write(string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!"));
                        Vizualizace_vykladka_tisk_vykresli(null, AGRO.bocedi_2);
                        log_hlaska = string.Format("Vizualizace vykladky se nezdarila, nejsou zaznamy!!");
                        ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                    }
                    // LV.TiskPrijem_Vaha(e.CisloLinky); 
                    return true;
                }
                else
                {
                    //neocekavana varianta!!
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                return false;
            }
        }

#endregion

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

                updateForm(false, "timer"); // aktualizace formu v casovem intervalu 1s
                //vizualizace(false, "timer");

            }
            finally
            {
                TimerRefreshUIOn();
            }
        }

#endregion

        #region button's methods

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (RFID_Linky_IN != null)
                    RFID_Linky_IN.Start_Read_tags(null, null, null);
                //if (RFID_Linky_OUT != null)
                //    RFID_Linky_OUT.Start_Read_tags(null, null, null);

                button1.Enabled = false;
                button2.Enabled = true;
            }
            catch (Exception ex)
            {

                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (RFID_Linky_IN != null)
                    RFID_Linky_IN.Stop_Read_tags();
                //if (RFID_Linky_OUT != null)
                //    RFID_Linky_OUT.Stop_Read_tags();

                button1.Enabled = true;
                button2.Enabled = false;
            }
            catch (Exception ex)
            {

                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        private void bt_EPC_zapis_Click(object sender, EventArgs e)
        {
            bool zapis = false;
            try
            {
                if (txB_EPC_cil.Text.Length == 24)
                {
                    string inicializace_EPC = string.Empty;


                    if (RFID_Linky_IN != null)
                        zapis = RFID_Linky_IN.Write_tags(txB_EPC_zdroj.Text, txB_EPC_cil.Text, null);

                    if (RFID_Linky_OUT != null)
                        zapis = RFID_Linky_OUT.Write_tags(txB_EPC_zdroj.Text, txB_EPC_cil.Text, null);
                }
                else
                {
                    MessageBox.Show("Delka zapisovaneho EPC nema 24znaku!!", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (zapis)
                {
                    MessageBox.Show("Zapis do Tagu - zapsano", "Correct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Zapis do Tagu - nepodarilo se", "False", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);

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
                    groupBox_ADAM3.Enabled =
                    groupBox_ADAM3_DI.Enabled =
                    groupBox_CS.Enabled =
                    groupBox_DB_Config.Enabled =
                    groupBox_RFID.Enabled =
                    groupBox_RFID1.Enabled =
                    groupBox_RFID2.Enabled =
                    groupBox_WEB_API_FASK.Enabled =
                    groupBox_WEB_API_TISK.Enabled =
                buttonUlozitNastaveni.Enabled = true;
            }
            catch (Exception ex)
            {

                //throw ex;
                //Log.Write(string.Format("{0}",ex));
                //MessageBox.Show("Nastaveni konfigurace", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult dialogResult_value;
                ExceptionHandler2.Handle(ex, true, MessageBoxButtons.OK, MessageBoxIcon.Information, out dialogResult_value);
            }
        }

        private void button_SaveConfig_Click(object sender, EventArgs e)
        {
            //if ((AgroConfig.config.Agro[0].IsPasswordNull() || String.IsNullOrEmpty(AgroConfig.config.Agro[0].Password)) && !String.IsNullOrEmpty(txtPassword.Text))
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

#region ADAM3

                 AgroSledovaniVozikuConfig.config.ADAM_3[0].PortP2PAdam = int.Parse(tb_ADAM3_PortP2P.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_3[0].IPAdresaAdam  = tb_ADAM3_IPAdresa.Text;
                 AgroSledovaniVozikuConfig.config.ADAM_3[0].TimerPeriodSensorsCheckAdam = int.Parse(tb_ADAM3_TimerPeriodSensorsCheckAdam.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_3[0].AdamTCPTimeout = int.Parse(tb_ADAM3_AdamTCPTimeout.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_3[0].MinHighSignalWidthAdam = int.Parse(tb_ADAM3_MinHighSignalWidthAdam.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_3[0].Connect = cb_ADAM3_Connect.Checked;

#endregion

#region RFID 1

                 AgroSledovaniVozikuConfig.config.RFID_1[0].Enable = chb_RFID1_Enabled.Checked;
                 AgroSledovaniVozikuConfig.config.RFID_1[0].IP_Adresa = tb_RFID1_IP_Adresa.Text;
                 AgroSledovaniVozikuConfig.config.RFID_1[0].Port = uint.Parse(tb_RFID1_Port.Text);
                 AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka1 = int.Parse(tb_RFID1_ID_antena_Linka1.Text);
                 AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka2 = int.Parse(tb_RFID1_ID_antena_Linka2.Text);
                 AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_Linka3 = int.Parse(tb_RFID1_ID_antena_Linka3.Text);
                 AgroSledovaniVozikuConfig.config.RFID_1[0].ID_antena_4 = int.Parse(tb_RFID1_ID_antena_4.Text);
                 AgroSledovaniVozikuConfig.config.RFID_1[0].Time_read = int.Parse(tb_RFID1_Time_read.Text);
                 AgroSledovaniVozikuConfig.config.RFID_1[0].PocetPruchodu = int.Parse(tb_RFID1_PocetPruchodu.Text);

#endregion

#region RFID 2

                AgroSledovaniVozikuConfig.config.RFID_2[0].Enable = chb_RFID2_Enabled.Checked;
                 AgroSledovaniVozikuConfig.config.RFID_2[0].IP_Adresa = tb_RFID2_IP_Adresa.Text;
                 AgroSledovaniVozikuConfig.config.RFID_2[0].Port = uint.Parse(tb_RFID2_Port.Text);
                 AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_strec1 = int.Parse(tb_RFID2_ID_antena_strec1.Text);
                 AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_strec2 = int.Parse(tb_RFID2_ID_antena_strec2.Text);
                 AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_rucniVstup = int.Parse(tb_RFID2_ID_antena_rucniVstup.Text);
                 AgroSledovaniVozikuConfig.config.RFID_2[0].ID_antena_4 = int.Parse(tb_RFID2_ID_antena_4.Text);
                 AgroSledovaniVozikuConfig.config.RFID_2[0].Time_read = int.Parse(tb_RFID2_Time_read.Text);
                AgroSledovaniVozikuConfig.config.RFID_2[0].PocetPruchodu = int.Parse(tb_RFID2_PocetPruchodu.Text);

#endregion

#region ADAM DI 1

                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_END = int.Parse(tb_ADAM1_Linka1_END.Text);
                AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_RUN = int.Parse(tb_ADAM1_Linka1_RUN.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_OK = int.Parse(tb_ADAM1_Linka1_OK.Text);
                AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka1_CALL = int.Parse(tb_ADAM1_Linka1_CALL.Text);

                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_END = int.Parse(tb_ADAM1_Linka2_END.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_RUN = int.Parse(tb_ADAM1_Linka2_RUN.Text);
                AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_OK = int.Parse(tb_ADAM1_Linka2_OK.Text);
                AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka2_CALL = int.Parse(tb_ADAM1_Linka2_CALL.Text);

                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_END = int.Parse(tb_ADAM1_Linka3_END.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_RUN = int.Parse(tb_ADAM1_Linka3_RUN.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_OK = int.Parse(tb_ADAM1_Linka3_OK.Text);
                AgroSledovaniVozikuConfig.config.ADAM_DI[0].Linka3_CALL = int.Parse(tb_ADAM1_Linka3_CALL.Text);

                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].L1 = int.Parse(tb_ADAM1_L1.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].L2 = int.Parse(tb_ADAM1_L2.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].L3 = int.Parse(tb_ADAM1_L3.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].L4 = int.Parse(tb_ADAM1_L4.Text);

                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_END = int.Parse(tb_ADAM1_RucnyVstup_END.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_RUN = int.Parse(tb_ADAM1_RucnyVstup_RUN.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_OK = int.Parse(tb_ADAM1_RucnyVstup_OK.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].RucniVstup_CALL = int.Parse(tb_ADAM1_RucnyVstup_CALL.Text);

                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_OK = int.Parse(tb_ADAM1_Strec1_OK.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_Bit1 = int.Parse(tb_ADAM1_Strec1_Bit1.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_Bit2 = int.Parse(tb_ADAM1_Strec1_Bit2.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec1_Bit4 = int.Parse(tb_ADAM1_Strec1_Bit4.Text);

                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec2_OK = int.Parse(tb_ADAM1_Strec2_OK.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec2_Bit1 = int.Parse(tb_ADAM1_Strec2_Bit1.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec2_Bit2 = int.Parse(tb_ADAM1_Strec2_Bit2.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec2_Bit4 = int.Parse(tb_ADAM1_Strec2_Bit4.Text);

                 AgroSledovaniVozikuConfig.config.ADAM_DI[0].Strec_vyber = int.Parse(tb_ADAM1_Strec_Vyber.Text);

#endregion

#region ADAM DI 2

                AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Strec1_OK = int.Parse(tb_ADAM2_Strec1_OK.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Bocedi1_OUT = int.Parse(tb_ADAM2_Vaha_START.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Vaha1_END = int.Parse(tb_ADAM2_Vaha_END.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_2_DI[0].Tisk1_END = int.Parse(tb_ADAM2_Tisk_END.Text);

#endregion

#region ADAM DI 3

                 AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Strec2_OK = int.Parse(tb_ADAM3_Strec2_OK.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Bocedi2_OUT = int.Parse(tb_ADAM3_Vaha_START.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Vaha2_END = int.Parse(tb_ADAM3_Vaha_END.Text);
                 AgroSledovaniVozikuConfig.config.ADAM_3_DI[0].Tisk2_END = int.Parse(tb_ADAM3_Tisk_END.Text);

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

#region WEB API FASK

                // AgroSledovaniVozikuConfig.config.WEBAPI[0].Autorizace_DoAPI = tb_APIFASK_Autorizace_DoAPI.Text;
                // AgroSledovaniVozikuConfig.config.WEBAPI[0].Adresa = tb_APIFASK_Adresa.Text;
                // AgroSledovaniVozikuConfig.config.WEBAPI[0].AliasDB = tb_APIFASK_AliasDB.Text;
                //AgroSledovaniVozikuConfig.config.WEBAPI[0].isHTTPS = chb_APIFASK_isHTTPS.Checked;
                //AgroSledovaniVozikuConfig.config.WEBAPI[0].API_TimeOut = int.Parse(tb_APIFASK_API_TimeOut.Text);
                // AgroSledovaniVozikuConfig.config.WEBAPI[0].Enable = chb_APIFASK_Enable.Checked;

#endregion

            }
            catch (Exception ex)
            {
                //ErrorLog.Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
            }

            ////Ulozeni do xml
            try
            {
                AgroSledovaniVozikuConfig.Save();

                groupBox_ADAM1.Enabled =
                    groupBox_ADAM1_DI.Enabled =
                    groupBox_ADAM2.Enabled =
                    groupBox_ADAM2_DI.Enabled =
                    groupBox_ADAM3.Enabled =
                    groupBox_ADAM3_DI.Enabled =
                    groupBox_CS.Enabled =
                    groupBox_DB_Config.Enabled =
                    groupBox_RFID.Enabled =
                    groupBox_RFID1.Enabled =
                    groupBox_RFID2.Enabled =
                    groupBox_WEB_API_FASK.Enabled =
                    groupBox_WEB_API_TISK.Enabled =
                buttonUlozitNastaveni.Enabled = false;

                txtPassword.Text = string.Empty;
            }
            catch (Exception ex)
            {

                //throw ex;
                //Log.Write(string.Format("{0}", ex));
                //MessageBox.Show("Nastaveni konfigurace", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult dialogResult_value;
                ExceptionHandler2.Handle(ex, true, MessageBoxButtons.OK, MessageBoxIcon.Information, out dialogResult_value);
            }

        }

#endregion

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if(timer_strec_1!= null)
        //        {
        //            timer_strec_1.Change(2000,System.Threading.Timeout.Infinite);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //        ExceptionHandler2.Handle(ex);
        //    }
            
        //}

        private void bt_Odhlasit_Click(object sender, EventArgs e)
        {
            //zobrazit upozorneni??
            //logika status 0, zbyva x zaznamu??
            // ok -> archivace na status 200 ?? zaznamenani archivace operatorem status 300?

            

            //Log.Write(string.Format("Odhlaseni z modulu, uzivatel: {0}", Rodic.uzivatel));
            log_hlaska = string.Format("Odhlaseni z modulu, uzivatel: {0}", Rodic.uzivatel);
            ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");

            _openFlag = false;
            Rodic.Show();
            this.Close();
            Rodic.SetMain();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO Uzavřeni ADAMU a RFID

            try
            {
                AdamStop();
                if (AgroSledovaniVozikuConfig.config.RFID_1[0].Enable)
                {
                    RFID_Linky_IN.Stop();
                }

                if (AgroSledovaniVozikuConfig.config.RFID_2[0].Enable)
                {
                    RFID_Linky_OUT.Stop();
                }
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }



        }

        public bool IsReadyToShow(out string message)
        {
            message = "ok";
            return true;
        }

#region Archivace

        private void bt_archivace_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button)
                {
                    Button btn = sender as Button;
                    if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Linka1)
                    {
                        Archivace(
                                Fask.Constants.AGRO.linka_1,
                                Fask.Constants.AGRO.status_0,
                                AgroSledovaniVozikuConfig.config.DB_Config[0].Separator,
                                Fask.Constants.AGRO.BTN_Linka1
                                );                 
                    }
                    else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Linka2)
                    {
                        Archivace(
                                Fask.Constants.AGRO.linka_2,
                                Fask.Constants.AGRO.status_0,
                                AgroSledovaniVozikuConfig.config.DB_Config[0].Separator,
                                Fask.Constants.AGRO.BTN_Linka2
                                );
                    }
                    else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Linka3)
                    {
                        Archivace(
                                Fask.Constants.AGRO.linka_3,
                                Fask.Constants.AGRO.status_0,
                                AgroSledovaniVozikuConfig.config.DB_Config[0].Separator,
                                Fask.Constants.AGRO.BTN_Linka3
                                );
                    }
                    else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_RV)
                    {
                        Archivace(
                                Fask.Constants.AGRO.rucni_vstup,
                                Fask.Constants.AGRO.status_0,
                                AgroSledovaniVozikuConfig.config.DB_Config[0].Separator,
                                Fask.Constants.AGRO.BTN_RV
                                );
                    }
                    else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Bocedi1)
                    {
                        Archivace(Fask.Constants.AGRO.bocedi_1,
                                 Fask.Constants.AGRO.status_21,
                                 AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec,
                                 Fask.Constants.AGRO.BTN_Bocedi1);

                        //Archivace(21, Fask.Constants.AGRO.BTN_Bocedi1);
                    }
                    else if (btn.Text.Trim() == Fask.Constants.AGRO.BTN_Bocedi2)
                    {
                        Archivace(Fask.Constants.AGRO.bocedi_2,
                                Fask.Constants.AGRO.status_22,
                                AgroSledovaniVozikuConfig.config.DB_Config[0].Separator_strec,
                                Fask.Constants.AGRO.BTN_Bocedi2);
                        //Archivace(22, Fask.Constants.AGRO.BTN_Bocedi2);
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

            var p = new ParametryProArchivaci()
            {
                CisloLinky = CisloLinky,
                Separator = Separator,
                Status = Status
            };

            using (frmArchivaceZaznamu frm = new frmArchivaceZaznamu(p, text))
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
                using (AboutBox abox = new AboutBox())
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

        private void groupBox2_Resize(object sender, EventArgs e)
        {
            var G = (GroupBox)sender;

            bt_bocedi2.Size = new Size(G.Width,G.Height/2);
        }

        private void groupBox1_Resize(object sender, EventArgs e)
        {
            var G = (GroupBox)sender;

            bt_Linka_3.Size = new Size(G.Width, G.Height / 4);
            bt_Linka_2.Size = new Size(G.Width, G.Height / 4);
            bt_Linka_1.Size = new Size(G.Width, G.Height / 4);

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            int W = panel1.Width;
            panel_Bocedi.Size = new Size(W/2,panel1.Height);

           int H =  panel_Bocedi.Height - 35;

            bt_bocedi2.Size = new Size(panel1.Width, H / 2);
            bt_bocedi1.Size = new Size(panel1.Width,H/2);

            bt_Linka_3.Size = new Size(panel1.Width, H / 4);
            bt_Linka_2.Size = new Size(panel1.Width, H / 4);
            bt_Linka_1.Size = new Size(panel1.Width, H / 4);
            bt_RV.Size = new Size(panel1.Width, H / 4);

        }

        #region heslo na testy

        private void textBoxPassword_testy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Configuration.AgroSledovaniVozikuConfig.config.HesloDoKonfigurace[0].IsPassword_testyNull() && !String.IsNullOrEmpty(Configuration.AgroSledovaniVozikuConfig.config.HesloDoKonfigurace[0].Password_testy))
                {
                    if (Configuration.AgroSledovaniVozikuConfig.config.HesloDoKonfigurace[0].Password_testy != Convert.ToBase64String(Encoding.Default.GetBytes(textBoxPassword_testy.Text), Base64FormattingOptions.None))
                        return;
                }

                panel_testy.Enabled = true;
            }
            catch (Exception ex)
            {

                //throw ex;
                //Log.Write(string.Format("{0}",ex));
                //MessageBox.Show("Nastaveni konfigurace", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult dialogResult_value;
                ExceptionHandler2.Handle(ex, true, MessageBoxButtons.OK, MessageBoxIcon.Information, out dialogResult_value);
            }
        }

        #endregion

        private void btn_konec_Click(object sender, EventArgs e)
        {
            AgroSledovaniVozikuConfig.config.HesloDoKonfigurace[0].Password_testy = Convert.ToBase64String(Encoding.Default.GetBytes(textBoxPassword_testy.Text), Base64FormattingOptions.None);

            AgroSledovaniVozikuConfig.Save();
            panel_testy.Enabled = false;
        }

        private void btnJSON_Click(object sender, EventArgs e)
        {
            Fask.WEBAPI.API_BusinessObjects.FASK_Events_row objekt_Fe_data = new Fask.WEBAPI.API_BusinessObjects.FASK_Events_row();

            objekt_Fe_data.loginid = "44";
           
                objekt_Fe_data.machineid = Fask.Constants.AGRO.rucni_vstup_R.ToString();
                objekt_Fe_data.description = "Rucni vstup";
                objekt_Fe_data.IS_ID = "A6666A";
                objekt_Fe_data.EAN_IS = "8596666666664";
          

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
            objekt_Fe_data.PackType = "UP";
            objekt_Fe_data.WEIGHT = 666;

            LV.TiskPosliData(objekt_Fe_data, AGRO.bocedi_3_R);
        }
    }



    #region Pomocne tridy jak EventArgs

    public class RFID_EventArgs : EventArgs
    {
        public int CisloLinky { get; set; }

        public RFID_EventArgs(int cl)
        {
            CisloLinky = cl;
        }
    }

    public class STREC_EventArgs : EventArgs
    {
        public int CisloStrec { get; set; }

        public STREC_EventArgs(int cs)
        {
            CisloStrec = cs;
        }
    }

    public class ADAM_EventArgs : EventArgs
    {
        public int CisloAdam { get; set; }

        public ADAM_EventArgs(int ca)
        {
            CisloAdam = ca;
        }
    }

#endregion
}
