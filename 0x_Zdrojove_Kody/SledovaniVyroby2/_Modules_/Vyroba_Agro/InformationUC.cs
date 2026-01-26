using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FASK.SledovaniVyroby.Module.Vyroba_Agro.Classes;
using FASK.SledovaniVyroby.ErrorLog;
using Vyroba_Agro;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro
{
    public partial class InformationUC : UserControl
    {
        public static InfoForm frm = null;
        private DataVyroba vyroba;
        private string smenaID = string.Empty;

        private string _srazeID = string.Empty;



        private KeyboardText _klavesniceText;
        public KeyboardText klavesniceText
        {
            get { return this._klavesniceText; }
            set
            {
                this._klavesniceText = value;
            }
        }

        public event ZmenaEventHandler Zmena;

        public delegate void ZmenaEventHandler();

        public InformationUC()
        {
            InitializeComponent();

            this.AutoSize = true;
            this.Dock = DockStyle.Fill;
        }

        public InformationUC(DataVyroba dv)
            : this()
        {
            this.vyroba = dv;
            this.vyroba.ZmenaStavu += new DataVyroba.StavZmenaEventHandler(vyroba_ZmenaStavu);
            this.vyroba.VarovaniZmena += new DataVyroba.WarningEventHandler(vyroba_VarovaniZmena);
            this.vyroba.ProhazZmena += new DataVyroba.ProhazEventHandler(vyroba_ProhazZmena);

            this.klavesniceText = new KeyboardText();
        }

        void vyroba_ProhazZmena(string hlaska, string znak)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => { vyroba_ProhazZmena(hlaska, znak); }));
            }
            else
            {
                this.lblProhazovani.Text = hlaska;
                this.lblProhazSymbol.Text = znak;
            }
        }

        void vyroba_VarovaniZmena(string hlaska)
        {
            if (this.InvokeRequired)
            {   //kvuli odvodu mimo smenu...jinak threadsafe exception....
                this.BeginInvoke(new MethodInvoker(() => { vyroba_VarovaniZmena(hlaska); }));
            }
            else
                this.lblWarning.Text = hlaska;
        }

        private void vyroba_ZmenaStavu(DataVyroba.VyrobaStavy novystav)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => { vyroba_ZmenaStavu(novystav); }));
                //this.BeginInvoke((MethodInvoker)delegate() { vyroba_ZmenaStavu(novystav); });
            }
            else
            {
                vyroba.error_id_code = 0;

                InitText();

                if (novystav == DataVyroba.VyrobaStavy.LogIDPracovnik)
                {
                    this.lblText.Text = "Vložte ID pracovníka";
                    this.klavesniceText.BtnEnter = "OK";
                    this.klavesniceText.Btn19 = "Další";
                }
                else if (novystav == DataVyroba.VyrobaStavy.LogIDSmena)
                {
                    // ?? neni tady nastaveno vypnuti linky ???
                    // >> kde se pri odhlaseni vypina linka?

                    vyroba.SetSmenaId(AgroConfig.config.Agro[0].OdvodMimoSmenuID.ToString());
                    vyroba.PracovniciClear(); // vynulovani seznamu pracovniku ...

                    repeatInsertSmena = true;

                    this.lblText.Text = "Vložte ID směny";
                    this.klavesniceText.Btn19 = "Zpět";
                    this.klavesniceText.BtnEnter = "OK";
                    this.txtText.Text = (Convert.ToInt32(AgroConfig.config.Agro[0].PosledniPrihlasenaSmena) + 1).ToString();
                }
                else if (novystav == DataVyroba.VyrobaStavy.ZadatEAN)
                {
                    // po prihlaseni smeny je prohaz vyply, ale protoze je dotaz na EAN, je stopla i linka
                    vyroba.SetLinkaState(false);

                    this.lblText.Text = "Vložte EAN kód.\nF1: Dobrý, F2: Ručně, F3: Nezadávat";
                    this.klavesniceText.BtnF1 = "F1";
                    this.klavesniceText.BtnF2 = "F2";
                    this.klavesniceText.BtnF3 = "F3";
                }
                else if (novystav == DataVyroba.VyrobaStavy.Main)
                {
                    this.lblText.Text = "Odvod výroby";
                    this.klavesniceText.BtnF1 = "Vložit kód";
                    this.klavesniceText.BtnF2 = "Změna";
                    this.klavesniceText.BtnF3 = "Prohaz";
                    this.klavesniceText.BtnF4 = "Odhlásit";
                    this.klavesniceText.BtnF5 = "Odečíst";
                    this.klavesniceText.Btn19 = "Šarže";
                }
                else if (novystav == DataVyroba.VyrobaStavy.Odhlaseni)
                {
                    this.lblText.Text = "Opravdu chcete odhlásit směnu?";
                    this.klavesniceText.Btn19 = "Zpět";
                    this.klavesniceText.BtnEnter = "OK";
                }
                else if (novystav == DataVyroba.VyrobaStavy.ZmenaProhaz)
                {
                    this.lblText.Text = "Opravdu chcete " + (vyroba.ProhazZapnut ? "vypnout" : "zapnout") + " prohaz?";
                    this.klavesniceText.Btn19 = "Ne";
                    this.klavesniceText.BtnEnter = "Ano";
                }
                else if (novystav == DataVyroba.VyrobaStavy.UlozeniEAN)
                {
                    vyroba.SetLinkaState(false);
                   
                    vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.ON);

                    repeatInsertEAN = true;

                    this.lblText.Text = "!!! Vložte EAN kód !!!";
                    this.klavesniceText.BtnEnter = "OK";
                }
                else if (novystav == DataVyroba.VyrobaStavy.Vyrobek)
                {
                    this.lblText.Text = "Chcete změnit výrobek?";
                    this.klavesniceText.Btn19 = "Zpět";
                    this.klavesniceText.BtnEnter = "OK";
                }
                else if (novystav == DataVyroba.VyrobaStavy.VlozKod)
                {
                    vyroba.SetLinkaState(false);

                    repeatInsertEAN = true;
                    this.lblText.Text = "Zadejte kód ručně.";
                    this.klavesniceText.Btn19 = "Zpět";
                    this.klavesniceText.BtnEnter = "OK";
                }
                else if (novystav == DataVyroba.VyrobaStavy.Event36)
                {
                    this.lblWarning.Text = "Scan bez dat!\n" + Udalosti.event36(Udalosti.Event36Stav.COUNT, 0) + "/" + Udalosti.event36(Udalosti.Event36Stav.GET_TIME, 0);
                    this.klavesniceText.BtnEnter = "OK";

                    vyroba.error_id_code = 4;
                }
                else if (novystav == DataVyroba.VyrobaStavy.Event11)
                {
                    this.lblWarning.Text = "Více nepotvrzených! Čidlo nepotvrdilo ČK.\n" + Udalosti.event11(Udalosti.Event11Stav.COUNT, 0) + "/" + Udalosti.event11(Udalosti.Event11Stav.GET_TIME, 0);
                    this.klavesniceText.BtnEnter = "OK";

                    vyroba.error_id_code = 5;
                }
                else if (novystav == DataVyroba.VyrobaStavy.VlozPocet)
                {
                    vyroba.SetLinkaState(false);

                    showMessageBox = true;

                    this.lblText.Text = "Zadejte počet kusů.";
                    this.klavesniceText.BtnF1 = "Akt. kód";
                    this.klavesniceText.BtnF2 = "Zadat kód";
                    this.klavesniceText.BtnEnter = "Zpět";
                }
                else if (novystav == DataVyroba.VyrobaStavy.VlozPocetEAN)
                {
                    vyroba.SetLinkaState(false);

                    this.lblText.Text = "Vložte EAN kód!";
                    this.klavesniceText.Btn19 = "Zpět";
                    this.klavesniceText.BtnEnter = "OK";
                }
                 //23.8.2017 Ta.D. pridani ID uzivatele pri zadavani sarze   
                else if (novystav == DataVyroba.VyrobaStavy.SarzeID)
                {
                    vyroba.SetLinkaState(false);

                    this.txtText.Text = string.Empty;
                    this.lblText.Text = "Zadejte ID ke změně šarže.";
                    this.lblWarning.Text = string.Empty;
                    this.klavesniceText.Btn19 = "Zpět";
                    this.klavesniceText.BtnEnter = "OK";

                }
                else if (novystav == DataVyroba.VyrobaStavy.SarzeHeslo)
                {
                    vyroba.SetLinkaState(false);

                    this.txtText.Text = string.Empty;
                    this.lblText.Text = "Zadejte heslo ke změně šarže.";
                    this.lblWarning.Text = string.Empty;
                    this.klavesniceText.Btn19 = "Zpět";
                    this.klavesniceText.BtnEnter = "OK";

                }
                else if (novystav == DataVyroba.VyrobaStavy.SarzeHodnota)
                {
                    vyroba.SetLinkaState(false);

                    this.txtText.Text = vyroba._sarze;
                    this.lblText.Text = "Zadejte šarži.";
                    this.klavesniceText.BtnF5 = "Generovat";
                    this.klavesniceText.Btn19 = "Zpět";
                    this.klavesniceText.BtnEnter = "OK";
                }
                else
                {
                    this.lblText.Text = "dodelat";
                    this.klavesniceText.BtnEnter = "dodelat";
                }

            }
        }

        private string countOdecistKusy = string.Empty;

        private bool showHistory = true;
        private bool showMessageBox = true;
        private bool repeatInsertSmena = true;
        private bool repeatInsertEAN = true;
        private string tempEAN = string.Empty; 

        public void ButtonClick(object sender, EventArgs args)
        {
            try
            {
                Button btn = (Button)sender;

                if ((vyroba.cidla[AgroConfig.config.Agro[0].PotvrzovaciCidloCislo].SensorStatePrevious == true) || (vyroba.cidla[AgroConfig.config.Agro[0].PotvrzovaciCidloCislo].SensorStateActual == true))
                {
                    //vracime se do pocatecniho stavu
                    vyroba.cidla[AgroConfig.config.Agro[0].PotvrzovaciCidloCislo].setSensorStateActualDeactive();
                    vyroba.cidla[AgroConfig.config.Agro[0].PotvrzovaciCidloCislo].setSensorStatePreviousDeactive();
                    vyroba.codeReadResultActual = FASK.SledovaniVyroby.IScannerProvider.Code.NoData;
                    vyroba.codeReadResultPrevious = FASK.SledovaniVyroby.IScannerProvider.Code.NoData;
                }


                if (btn.Name == "btnF6")
                {
                    if (vyroba._pocetPruchoduProhaz >= AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz)
                    {

                        vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);
                        vyroba._pocetPruchoduProhaz = 0;
                    }
                }

                #region LogIDSmena
                if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.LogIDSmena)
                {
                    NumKeyPressedCheck(btn);

                    if (btn.Name == "btn19")
                    {
                        txtText.Text = smenaID = "";
                        repeatInsertSmena = true;
                        lblWarning.Text = "-";
                        this.lblText.Text = "Vložte ID směny";
                    }
                    else if (btn.Name == "BtnEnter")
                    {
                        #region testovani smeny a opakovane zadani smeny
                        if (txtText.Text.Length <= 0)
                        {
                            lblWarning.Text = "Musíte zadat ID směny!";
                            return;
                        }

                        if (repeatInsertSmena)
                        {
                            smenaID = txtText.Text;

                            txtText.Text = "";
                            lblText.Text = "Zadejte znovu ID směny.";
                            repeatInsertSmena = false;
                            return;
                        }

                        if (smenaID != txtText.Text)
                        {
                            lblWarning.Text = "Směny se neshodují! Opakujte zadání.";
                            return;
                        }
                        #endregion

                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.LogIDPracovnik);

                        Classes.Database.InsertNewUserEvents(smenaID, AgroConfig.LOG_PRIHLASENI_SMENY, smenaID, "");
                    }
                }
                #endregion

                #region LogIDPracovnik
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.LogIDPracovnik)
                {
                    NumKeyPressedCheck(btn);

                    if (btn.Name == "BtnEnter")
                    {
                        if (txtText.Text.Length <= 0)
                        {
                            lblWarning.Text = "Musíte zadat ID pracovníka!";
                            return;
                        }

                        Classes.Database.InsertNewUserEvents(smenaID, AgroConfig.LOG_PRIHLASENI_PRACOVNIKA, txtText.Text, "");

                        //Ulozeni posledni prihlasene smeny do xml
                        AgroConfig.config.Agro[0].PosledniPrihlasenaSmena = smenaID;
                        AgroConfig.Save();

                        vyroba.SetSmenaId(smenaID);
                        vyroba.PracovnikSet(txtText.Text);
                        try
                        {
                            lblSarze.Text = vyroba.SarzeGenerateAndSet();
                        }
                        catch (Exception exSarzeGenerate)
                        {
                            Log.Write(exSarzeGenerate.Message.ToString());
                            lblSarze.Text = "?";
                        } 
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.ZadatEAN);


                    }
                    else if (btn.Name == "btn19")
                    {
                        if (txtText.Text.Length <= 0)
                        {
                            lblWarning.Text = "Musíte zadat ID pracovníka!";
                            return;
                        }

                        vyroba.PracovnikSet(txtText.Text);
                        Classes.Database.InsertNewUserEvents(smenaID, AgroConfig.LOG_PRIHLASENI_PRACOVNIKA, txtText.Text, "");

                        txtText.Text = string.Empty;
                    }
                }
                #endregion

                #region ZadatEAN
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.ZadatEAN)
                {
                    NumKeyPressedCheck(btn);

                    if (btn.Name == "btnF1")
                    { //dobry EAN
                        Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_ZMENA_VYROBKU, "dobry EAN", txtText.Text);

                        vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.ZAPNOUT_HLIDANI);

                        if (!vyroba.ProhazZapnut)
                            vyroba.SetLinkaState(true);

                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                    }
                    if (btn.Name == "btnF2")
                    {// EAN zada rucne
                        vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.VYPNOUT_HLIDANI);

                        Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_VYZVA_VLOZIT_EAN,"Vyzva zadat EAN rucne", "");

                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.VlozKod);

                        //vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.UlozeniEAN);

                    }
                    if (btn.Name == "btnF3")
                    {// bez EANu
                       
                        Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_ZMENA_VYROBKU, "bez EANu",Convert.ToString(0));
                        vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.VYPNOUT_HLIDANI);

                        if (!vyroba.ProhazZapnut)
                            vyroba.SetLinkaState(true);

                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                    }
                }
                #endregion

                #region Main
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.Main)
                {
                    if (btn.Name == "btnF4")
                    {
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Odhlaseni);
                    }
                    else if (btn.Name == "btnF3")
                    {
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.ZmenaProhaz);
                    }
                    else if (btn.Name == "btnF2")
                    {
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Vyrobek);
                    }
                    else if (btn.Name == "btnF1")
                    {

                        vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);

                        if (vyroba.BarcodeActual == string.Empty)
                        {// kod je neinicializovany, zepta se obsluhy, jestli ho chce dodatecne zadat rucne
                            vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.VlozKod);
                        }
                    }
                    else if (btn.Name == "btnF5")
                    {
                        vyroba.SetLinkaState(false);
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.VlozPocet);
                    }
                    else if (btn.Name == "btn19")
                    {
                    Log.Write("Sarze vypni linku.");   
                    vyroba.SetLinkaState(false);
                    //vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.SarzeHeslo);
                    vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.SarzeID);
                    }
                }
                #endregion

                #region Odhlaseni

                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.Odhlaseni)
                {
                    if (btn.Name == "btn19")
                    {
                        vyroba.SetLinkaState(true);// zapneme linku
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                        showHistory = true;
                    }
                    else if (btn.Name == "BtnEnter")
                    {
                        if ((vyroba.BarcodeActual == string.Empty) && (vyroba.CodeNoReadCnt > 0 || vyroba.CodeReadCnt > 0))
                        {
                            vyroba.SetLinkaState(false);

                            vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.ON);
                            vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.UlozeniEAN);
                        }
                        else
                        {
                            if (vyroba.GetStavVyroba() != DataVyroba.VyrobaStavy.UlozeniEAN)
                            {
                                if (vyroba._list_NoRead_Count > 0)
                                {   //pokud je nejaky potvrzeny no_Read kod, tak jej ulozime do listu
                                    vyroba.aktualDataRow = vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", vyroba.CodeReadCnt, vyroba._list_NoRead_Count, "sensor", vyroba.BarcodeActual, "", "", "", "", "", "", "", "", "", vyroba._sarze);
                                    vyroba._list_NoRead_Count = 0;
                                }
                            }

                            if (showHistory)
                            {
                                Zmena();
                                showHistory = false;
                                return;
                            }

                            showHistory = true;


                            if (vyroba.GetStavVyroba() != DataVyroba.VyrobaStavy.UlozeniEAN)
                            {
                                Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_ODHLASENI_SMENY, "","");
                                Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), vyroba.CodeReadCnt, vyroba.CodeNoReadCnt, "odhlaseni", vyroba.BarcodeActual, "", "", vyroba.ProhazZapnut.ToString(), "", "", "", "", "", "", vyroba._sarze);
                                // Odeslani notifikace o konci smeny
                                NotificationMail.SendEmailOdhlaseniSmeny("InformationUC.ButtonClick()|Odhlaseni|btnEnter", vyroba.GetSmenaId(), vyroba.GetDataVyrobaHistory());

                                vyroba.CodeReadCnt = 0;
                                vyroba.CodeNoReadCnt = 0;
                                vyroba.dtSaveLast = DateTime.Now;

                                vyroba.ClearDataHistory();//zaroven ulozi pocet sepnuti cidel a pocet nactenych CK..

                                vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);//vypnuti houkacky
                                vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.RESET);

                                vyroba.SetLinkaState(false); // vypne linku 

                                vyroba.SetPocetPruchodProhaz(0);

                                vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.LogIDSmena); // nastavi stav na prihlaseni smeny ...

                            }
                        }
                    }
                }
                #endregion

                #region Zmena Prohaz
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.ZmenaProhaz)
                {
                    if (btn.Name == "btn19")
                    {
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                    }
                    else if (btn.Name == "BtnEnter")
                    {
                        if (vyroba.GetPocetPruchodProhaz() >= AgroConfig.config.Agro[0].PocetPruchoduZmenDoProhaz)
                        {

                            vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);
                            vyroba.SetPocetPruchodProhaz(0);
                        }

                        if (vyroba.BarcodeActual == string.Empty && (vyroba.CodeReadCnt > 0 || vyroba.CodeNoReadCnt > 0))
                        {
                            if (vyroba.GetStavVyroba() != DataVyroba.VyrobaStavy.UlozeniEAN)
                            {
                                vyroba.SetLinkaState(false);
                                vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.ON);
                                vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.UlozeniEAN);
                            }
                        }
                        else
                        {
                            Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), vyroba.CodeReadCnt, vyroba.CodeNoReadCnt, "zmena prohaz", vyroba.BarcodeActual, "", "", vyroba.ProhazZapnut.ToString(), "", "", "", "", "", "", vyroba._sarze);
                            vyroba.CodeReadCnt = 0;
                            vyroba.CodeNoReadCnt = 0;
                            vyroba.dtSaveLast = DateTime.Now;

                            if (!vyroba.ProhazZapnut)
                            {// prohazovani
                                vyroba.SetLinkaState(false);
                                vyroba.ProhazZapnut = true;
                                Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_ZAPNOUT_PROHAZ, "","");
                            }
                            else
                            {
                                vyroba.SetLinkaState(true);
                                vyroba.ProhazZapnut = false;
                                Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_VYPNOUT_PROHAZ, "","");
                            }

                            vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                        }
                    }
                }
                #endregion

                #region UlozeniEAN
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.UlozeniEAN)
                {
                    NumKeyPressedCheck(btn);

                    if (btn.Name == "BtnEnter")
                    {
                        if (txtText.Text.Length == AgroConfig.MaxEANLength && IsDigitsOnly(txtText.Text))
                        {
                            if (repeatInsertEAN)
                            {
                                tempEAN = txtText.Text;
                                lblText.Text = "Opakujte zadání EAN kódu.";
                                repeatInsertEAN = false;
                                txtText.Text = "";
                                return;
                            }

                            if (tempEAN != txtText.Text)
                            {
                                lblText.Text = "!!! Vložte EAN kód !!!";
                                txtText.Text = "";
                                lblWarning.Text = "Zadané kódy se neshodují!\nOpakujte jejich zadání!";
                                repeatInsertEAN = true;
                                return;
                            }


                            vyroba.SetActualBarcode(txtText.Text);
                            vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);

                            if (vyroba.StavVyrobaPredchozi == DataVyroba.VyrobaStavy.Odhlaseni)
                            {
                                //Todo...vyroba.CodeNoReadCnt + vyroba.HistoryListNoReadCount ???vyroba.CodeReadCnt 0?
                                vyroba.aktualDataRow = vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", vyroba.CodeReadCnt, vyroba._list_NoRead_Count, "sensor", vyroba.BarcodeActual, "", "", "", "", "", "", "", "", "", vyroba._sarze);

                                vyroba._list_NoRead_Count = 0;

                                if (showHistory)
                                {
                                    Zmena();
                                    showHistory = false;
                                    return;
                                }

                                showHistory = true;

                                Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_ODHLASENI_SMENY, "","");
                                Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), vyroba.CodeReadCnt, vyroba.CodeNoReadCnt, "odhlaseni", vyroba.BarcodeActual, "", "", vyroba.ProhazZapnut.ToString(), "", "", "", "", "", "", vyroba._sarze);
                                // Odeslani notifikace o konci smeny
                                NotificationMail.SendEmailOdhlaseniSmeny("InformationUC.ButtonClick()|UlozeniEAN|btnEnter", vyroba.GetSmenaId(), vyroba.GetDataVyrobaHistory());

                                vyroba.CodeReadCnt = 0;
                                vyroba.CodeNoReadCnt = 0;
                                vyroba.dtSaveLast = DateTime.Now;

                                vyroba.ClearDataHistory();//zaroven ulozi pocet sepnuti cidel a pocet nactenych CK..

                                vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);//vypnuti houkacky
                                vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.RESET);

                                vyroba.SetLinkaState(false); //vypne linku ...

                                vyroba.SetPocetPruchodProhaz(0);

                                vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.LogIDSmena); // nastavi na prihlaseni smeny
                            }
                            else if (vyroba.StavVyrobaPredchozi == DataVyroba.VyrobaStavy.ZmenaProhaz)
                            {
                                if (!vyroba.ProhazZapnut)
                                {// prohazovani
                                    vyroba.SetLinkaState(false);
                                    vyroba.ProhazZapnut = true;

                                    Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_ZAPNOUT_PROHAZ, "","");
                                }
                                else
                                {
                                    vyroba.SetLinkaState(true);
                                    vyroba.ProhazZapnut = false;

                                    Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_VYPNOUT_PROHAZ, "","");
                                }

                                vyroba.aktualDataRow = vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", vyroba.CodeReadCnt, vyroba._list_NoRead_Count, "zmena prohazu - ean", vyroba.BarcodeActual, "", "", "", "", "", "", "", "", "", vyroba._sarze);

                                Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), vyroba.CodeReadCnt, vyroba.CodeNoReadCnt, "zmena prohaz", vyroba.BarcodeActual, "", "", vyroba.ProhazZapnut.ToString(), "", "", "", "", "", "", vyroba._sarze);

                                vyroba.CodeReadCnt = 0;
                                vyroba.CodeNoReadCnt = 0;
                                vyroba.dtSaveLast = DateTime.Now;

                                vyroba._list_NoRead_Count = 0;

                                vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main); //=> prechod na hlavni menu
                            }
                            else if (vyroba.StavVyrobaPredchozi == DataVyroba.VyrobaStavy.Vyrobek)
                            {
                                vyroba.SetPocetPruchodProhaz(0);

                                vyroba.SetLinkaState(false);

                                vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);
                                vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.RESET);

                                vyroba.aktualDataRow = vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", vyroba.CodeReadCnt, vyroba._list_NoRead_Count, "zmena vyrobek ean", vyroba.BarcodeActual, "", "", "", "", "", "", "", "", "", vyroba._sarze);

                                Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), vyroba.CodeReadCnt, vyroba.CodeNoReadCnt, "zmena vyrobek", vyroba.BarcodeActual, "", "", vyroba.ProhazZapnut.ToString(), "", "", "", "", "", "", vyroba._sarze);
                                vyroba.CodeReadCnt = 0;
                                vyroba.CodeNoReadCnt = 0;
                                vyroba.dtSaveLast = DateTime.Now;

                                vyroba.SetActualBarcode(string.Empty);

                                vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.ZadatEAN);
                            }
                            else
                            {
                                vyroba.SetLinkaState(true);

                                vyroba.aktualDataRow = vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", vyroba.CodeReadCnt, vyroba._list_NoRead_Count, "zmena vyrobek - else", vyroba.BarcodeActual, "", "", "", "", "", "", "", "", "", vyroba._sarze);
                                Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), vyroba.CodeReadCnt, vyroba.CodeNoReadCnt, "zmena vyrobek - else", vyroba.BarcodeActual, "", "", vyroba.ProhazZapnut.ToString(), "", "", "", "", "", "", vyroba._sarze);
                                vyroba.CodeReadCnt = 0;
                                vyroba.CodeNoReadCnt = 0;
                                vyroba.dtSaveLast = DateTime.Now;

                                vyroba._list_NoRead_Count = 0;
                                vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                            }

                            vyroba.CodeReadCnt = 0;
                            vyroba.CodeNoReadCnt = 0;
                            vyroba.StavVyrobaPredchozi = DataVyroba.VyrobaStavy.Main;
                            vyroba.dtSaveLast = DateTime.Now;
                        }
                        else
                            lblWarning.Text = "Špatný EAN kód!\nOpakujte zadání.";
                    }
                }
                #endregion

                #region zmena vyrobku
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.Vyrobek)
                {
                    if (btn.Name == "btn19")
                    {
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                    }
                    else if (btn.Name == "BtnEnter")
                    {
                        if ((vyroba.BarcodeActual == string.Empty) && (vyroba.CodeNoReadCnt + vyroba.CodeReadCnt > 0))
                        {
                            vyroba.SetLinkaState(false);

                            vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.ON);
                            vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.UlozeniEAN);
                        }
                        else
                        {
                            //listData = save_queue_data(listData, Code, &CodeReadCnt, &CodeNoReadCnt, rizeni_prohazu(PROHAZ_STAV), &id_zaznam, pfentry.id);
                            Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), vyroba.CodeReadCnt, vyroba.CodeNoReadCnt, "zmena vyrobku", vyroba.BarcodeActual, "", "", vyroba.ProhazZapnut.ToString(), "", "", "", "", "", "", vyroba._sarze);

                            vyroba.dtSaveLast = DateTime.Now;

                            vyroba.CodeReadCnt = 0;
                            vyroba.CodeNoReadCnt = 0;
                            vyroba.SetPocetPruchodProhaz(0);
                            vyroba.SetActualBarcode(string.Empty);
                            vyroba.SetLinkaState(false);

                            vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);
                            vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.RESET);

                            vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.ZadatEAN);
                        }
                    }
                }
                #endregion

                #region vloz kod
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.VlozKod) // dotaz na dodatecne vlozeni EANu rucne
                {
                    NumKeyPressedCheck(btn);

                    if (btn.Name == "btn19")
                    {
                        if (vyroba.StavVyrobaPredchozi == DataVyroba.VyrobaStavy.ZadatEAN)
                        {
                            vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.ZadatEAN);
                        }
                        else
                            vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                    }
                    else if (btn.Name == "BtnEnter")
                    {
                        vyroba.SetLinkaState(false); // vypne linku

                        if (txtText.Text.Length != AgroConfig.MaxEANLength)
                        {
                            lblWarning.Text = "Špatný kód! Opakujte zadání\n a potvrďte jej stiskem 'OK'!";
                        }
                        else
                        {
                            if (repeatInsertEAN)
                            {
                                tempEAN = txtText.Text;
                                lblText.Text = "Opakujte zadání EAN kódu.";
                                txtText.Text = string.Empty;
                                repeatInsertEAN = false;
                                return;
                            }

                            if (tempEAN != txtText.Text)
                            {
                                lblText.Text = "Zadejte kód.";
                                txtText.Text = "";
                                lblWarning.Text = "Zadané kódy se neshodují! Opakujte jejich zadání!";
                                repeatInsertEAN = true;
                                return;
                            }


                            //if (vyroba.BarcodeActual == string.Empty)
                            //    vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", 0, vyroba.CodeNoReadCnt, "vlozen kod", txtText.Text, "", "", "", "", "", "", "", "", "", "");
                            //Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), DateTime.Now, 0, vyroba.CodeNoReadCnt, "vlozen kod", txtText.Text, "", "", "", "", DateTime.Now, "", "", "", "", "", "");

                            vyroba.SetActualBarcode(txtText.Text);

                            if (vyroba.StavVyrobaPredchozi == DataVyroba.VyrobaStavy.ZadatEAN)
                            {
                                if (!vyroba.ProhazZapnut)
                                    vyroba.SetLinkaState(true);

                                Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_ZMENA_VYROBKU, vyroba.BarcodeActual,"");


                                vyroba.aktualDataRow = vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", 0, 0, "vlozen kod", vyroba.BarcodeActual, "", "", "", "", "", "", "", "", "", vyroba._sarze);

                                vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                            }
                            else
                            {
                                if (!vyroba.ProhazZapnut)
                                    vyroba.SetLinkaState(true);


                                vyroba.aktualDataRow = vyroba.InsertDataToDataset(vyroba.GetSmenaId(), "", 0, vyroba.CodeNoReadCnt, "vlozen kod", vyroba.BarcodeActual, "", "", "", "", "", "", "", "", "", vyroba._sarze);

                                vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                            }
                        }
                    }
                }
                #endregion

                #region event36
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.Event36)
                {
                    if (btn.Name == "BtnEnter")
                    {
                        vyroba.SetLinkaState(vyroba.LinkaStateBeforeEvent);
                        vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);
                        Udalosti.event36(Udalosti.Event36Stav.RESET, 0);
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                    }
                }
                #endregion

                #region event11
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.Event11)
                {
                    if (btn.Name == "BtnEnter")
                    {

                        vyroba.SetLinkaState(vyroba.LinkaStateBeforeEvent);
                        vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);
                        Udalosti.event11(Udalosti.Event11Stav.RESET, 0);
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                    }
                }
                #endregion

                #region odecist kusy
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.VlozPocet)
                {
                    NumKeyPressedCheck(btn);

                    if (btn.Name == "BtnEnter")
                    {
                        vyroba.SetLinkaState(true);
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                        showMessageBox = true;
                    }
                    else if (btn.Name == "btnF1")
                    {
                        if (vyroba.BarcodeActual == string.Empty)
                        {
                            vyroba.SetLinkaState(true);
                            vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                            return;
                        }

                        if (txtText.Text.Length <= 0)
                        {
                            lblText.Text = "Napište počet kusů!";
                        }
                        else
                        {
                            if (showMessageBox)
                            {
                                lblText.Text = "Opravdu chcete odečíst " + txtText.Text + " kusů\n od kódu " + vyroba.BarcodeActual + "?";
                                showMessageBox = false;
                                this.klavesniceText.BtnF1 = "Ano";
                                this.klavesniceText.BtnF2 = "-";
                                return;
                            }

                            if (!vyroba.InsertMinusQtyToDataset(vyroba.GetSmenaId(), decimal.Parse(txtText.Text), 0, vyroba.BarcodeActual, vyroba._sarze))
                            {
                                lblWarning.Text = "Nelze odečíst zadané kusy!\nKód ještě nebyl načten!";
                                txtText.Text = "";
                                showMessageBox = true;
                                return;
                            }

                            decimal odecet = decimal.Parse(txtText.Text);
                            Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), -odecet, 0, "odecet", vyroba.BarcodeActual, "", "", vyroba.ProhazZapnut.ToString(), "", "", "", "", "", "", vyroba._sarze);
                            //vyroba.CodeReadCnt -= odecet;
                            //vyroba.CodeNoReadCnt = 0;
                            vyroba.dtSaveLast = DateTime.Now;

                            Classes.Database.InsertNewUserEvents(smenaID, AgroConfig.LOG_EAN_MINUS_COUNT, txtText.Text,"");

                            vyroba.SetLinkaState(true);
                            vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                        }
                    }
                    else if (btn.Name == "btnF2")
                    {
                        if (txtText.Text.Length <= 0)
                        {
                            lblText.Text = "Napište počet kusů!";
                        }
                        else
                        {
                            countOdecistKusy = txtText.Text;

                            vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.VlozPocetEAN);
                        }
                    }
                }
                #endregion

                #region odecist kusy ean
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.VlozPocetEAN)
                {
                    NumKeyPressedCheck(btn);

                    if (btn.Name == "btn19")
                    {
                        vyroba.SetLinkaState(true);
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                        showMessageBox = true;
                    }
                    else if (btn.Name == "BtnEnter")
                    {

                        if (txtText.Text.Length != AgroConfig.MaxEANLength)
                        {
                            lblText.Text = "Špatný EAN kód!";
                        }
                        else
                        {
                            if (showMessageBox)
                            {
                                lblText.Text = "Opravdu chcete odečíst " + countOdecistKusy + " kusů\nod kódu " + txtText.Text + "?";
                                lblWarning.Text = "-";
                                showMessageBox = false;
                                return;
                            }

                            if (!vyroba.InsertMinusQtyToDataset(vyroba.GetSmenaId(), decimal.Parse(countOdecistKusy), 0, txtText.Text, vyroba._sarze))
                            {
                                lblWarning.Text = "Zadaný kód nebyl nalezen!\nOpakujte zadání kódu.";
                                txtText.Text = "";
                                showMessageBox = true;
                                return;
                            }

                            Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), -decimal.Parse(countOdecistKusy), 0, "odecist kusy", txtText.Text, "", "", vyroba.ProhazZapnut.ToString(), "", "", "", "", "", "", vyroba._sarze);
                            //vyroba.CodeReadCnt = 0;
                            //vyroba.CodeNoReadCnt = 0;
                            vyroba.dtSaveLast = DateTime.Now;

                            Classes.Database.InsertNewUserEvents(smenaID, AgroConfig.LOG_EAN_MINUS_COUNT, countOdecistKusy,"");


                            //Classes.Database.UpdateQtyMinusEvents(vyroba.GetSmenaId(), decimal.Parse(txtText.Text), codeOdecistKusy);

                            vyroba.SetLinkaState(true);
                            vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                        }
                    }
                }
                #endregion

                #region Sarze
                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.SarzeID)
                {
                    NumKeyPressedCheck(btn, 50);
                    if (btn.Name == "btn19")
                    { // zpet (storno)
                        //zalogovat pokus o zmenu sarze
                        //Classes.Database.InsertNewUserEvents(smenaID, AgroConfig.LOG_SARZE_ZMENA_HESLO, txtText.Text,"");
                        Log.Write("Sarze zapni linku do main");
                        vyroba.SetLinkaState(true);
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                    }
                    else if (btn.Name.ToUpper() == "BtnEnter".ToUpper())
                    {
                        
                        //zalogovani LOG_SARZE_ZMENA_ID ale neexistuje
                         Classes.Database.InsertNewUserEvents(smenaID, AgroConfig.LOG_SARZE_ZMENA_ID, txtText.Text,"");
                         this._srazeID = txtText.Text;
      

                        //skonrolovat ci sa ID nachadzi na serveru
                       /* if (!vyroba.ReturnID(txtText.Text))
                        {
                            //zalogovat ze neni nastaveno ID
                            lblWarning.Text = "ID pro změnu šarže není nastaveno...";
                            return;
                        }
                        */

                        if (!vyroba.ReturnID(txtText.Text))
                        {
                            //zalogovat ze se ID neshoduje
                            lblWarning.Text = "ID se neshoduje...";
                            this._srazeID = string.Empty;
                            return;
                        }
                         
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.SarzeHeslo);
                        txtText.PasswordChar = '*';
                    }
                }               
                



                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.SarzeHeslo)
                {
                    

                    NumKeyPressedCheck(btn, 50);
                    if (btn.Name == "btn19")
                    { // zpet (storno)
                        Log.Write("Sarze vypni linku do sarzeID");
                        vyroba.SetLinkaState(false);
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.SarzeID);
                        txtText.PasswordChar = '\0';
                    }
                    else if (btn.Name.ToUpper() == "BtnEnter".ToUpper())
                    {
                        Classes.Database.InsertNewUserEvents(smenaID, AgroConfig.LOG_SARZE_ZMENA_HESLO, txtText.Text, "");

                        if (!vyroba.ReturnHeslo(txtText.Text,this._srazeID))
                        {
                            lblWarning.Text = "Heslo se neshoduje...";
                            return;
                        }
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.SarzeHodnota);
                        txtText.PasswordChar = '\0';
                    }
                }


                else if (vyroba.GetStavVyroba() == DataVyroba.VyrobaStavy.SarzeHodnota)
                {
                    NumKeyPressedCheck(btn, 50);
                    if (btn.Name == "btn19")
                    { // zpet (storno)
                        Log.Write("Sarze zapni linku do main");
                        vyroba.SetLinkaState(true);
                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                    }
                    else if (btn.Name.ToUpper() == "BtnF5".ToUpper())
                    { // Generovat
                        txtText.Text = vyroba.SarzeGenerate();
                    }
                    else if (btn.Name == "BtnEnter")
                    {
                        // zalogovala sa zmena sarze, ulozit hodnotu ??kam??
                        Classes.Database.InsertNewUserEvents(smenaID, AgroConfig.LOG_SARZE_ZMENA_HODNOTA,txtText.Text ,"" ); // dodat rez1 aktualnz pracovnik
                        
                        //vyroba.SetLinkaState(true); // zapne sa na konci

                        #region po vytvoreni novej sarze, odeslat stara data ze starou sarzi pric

                        //Classes.Database.InsertNewUserEvents(vyroba.GetSmenaId(), AgroConfig.LOG_ODHLASENI_SMENY, "", "");
                        Classes.Database.InsertNewEvents(vyroba.GetSmenaId(), vyroba.CodeReadCnt, vyroba.CodeNoReadCnt, "zmena sarze", vyroba.BarcodeActual, "", "", vyroba.ProhazZapnut.ToString(), "", "", "", "", "", "", vyroba._sarze);
                        // Odeslani notifikace o konci smeny
                        //NotificationMail.SendEmailOdhlaseniSmeny("InformationUC.ButtonClick()|Odhlaseni|btnEnter", vyroba.GetSmenaId(), vyroba.GetDataVyrobaHistory());

                        vyroba.CodeReadCnt = 0;
                        vyroba.CodeNoReadCnt = 0;
                        vyroba.dtSaveLast = DateTime.Now;

                        //vyroba.ClearDataHistory();//zaroven ulozi pocet sepnuti cidel a pocet nactenych CK..

                        //vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.OFF);//vypnuti houkacky
                        //vyroba.rizeniHoukacky(DataVyroba.HoukackaStav.RESET);

                        vyroba.SarzeSet(txtText.Text);
                        Log.Write("Sarze zmenena, zapni linku do main");
                        vyroba.SetLinkaState(true); // vypne linku 
                        #endregion

                        vyroba.SetStavVyroba(DataVyroba.VyrobaStavy.Main);
                    }
                }
                #endregion

            }
            catch (Exception exceptionOdvod)
            {
                Log.Write(exceptionOdvod.Message.ToString());
            }
        }

        private void InitText()
        {
            this.txtText.Text = "";
            this.lblWarning.Text = "-";
            this.klavesniceText.Btn19 = "-";
            this.klavesniceText.BtnEnter = "-";
            this.lblProhazovani.Text = vyroba.ProhazZapnut ? "Zapnuto" : "Vypnuto";
            this.lblSarze.Text = vyroba.Sarze;
            this.klavesniceText.BtnF1 = "-";
            this.klavesniceText.BtnF2 = "-";
            this.klavesniceText.BtnF3 = "-";
            this.klavesniceText.BtnF4 = "-";
            this.klavesniceText.BtnF5 = "-";
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void NumKeyPressedCheck(Button btn)
        {
            NumKeyPressedCheck(btn, AgroConfig.MaxEANLength);
        }

        private void NumKeyPressedCheck(Button btn, int maxTextLengt)
        {
            if (btn.Name == "button13")
            {
                if (txtText.Text.Length > 0)
                    txtText.Text = txtText.Text.Substring(0, txtText.Text.Length - 1);
            }
            else if (btn.Name == "button11")
                txtText.Text = "";
            else if (txtText.Text.Length >= maxTextLengt)
                return;
            else if (btn.Name == "button12")
                txtText.Text += "0";
            else if (btn.Name == "button1")
                txtText.Text += btn.Text;
            else if (btn.Name == "button2")
                txtText.Text += btn.Text;
            else if (btn.Name == "button3")
                txtText.Text += btn.Text;
            else if (btn.Name == "button4")
                txtText.Text += btn.Text;
            else if (btn.Name == "button5")
                txtText.Text += btn.Text;
            else if (btn.Name == "button6")
                txtText.Text += btn.Text;
            else if (btn.Name == "button7")
                txtText.Text += btn.Text;
            else if (btn.Name == "button8")
                txtText.Text += btn.Text;
            else if (btn.Name == "button9")
                txtText.Text += btn.Text;
 
        }

        private void lblWarning_Click(object sender, EventArgs e)
        {
            if (frm == null)
            {                
                frm = new InfoForm();
                frm.TopLevel = true; 
            }

            frm.setID(vyroba.error_id_code);
            frm.Show();
            frm.TopMost = true;
        }
    }
}