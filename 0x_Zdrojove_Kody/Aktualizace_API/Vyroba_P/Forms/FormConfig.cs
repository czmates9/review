using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Aktualizace_API;
using Fask.Logging;
using JR.Utils.GUI.Forms;
using System.IO;

namespace Fask.Aktualizace_API.Forms
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);

            comboBoxScannerType.Items.Add(Fask.Aktualizace_API.Scanner.ScannerTypes.None); 
            comboBoxScannerType.Items.Add(Fask.Aktualizace_API.Scanner.ScannerTypes.COM);

            foreach (var printername in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                printerNameDataGridViewTextBoxColumn.Items.Add(printername);
            }

            // naètení korekcí s ID 0 do comboboxu
            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter tacor = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter();
            //tacor.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + System.IO.Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));
            var productions = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByProduction_Corrects(0);
            comboBoxPrestavkaID.DataSource = productions;
            comboBoxPrestavkaID.DisplayMember = "desc";
            comboBoxPrestavkaID.ValueMember = "id";
            SettingsLoad();
        }

        private void FormConfig_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    PerformCancel();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    PerformOK();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        public void PerformOK()
        {
            if (SettingsSave())
                this.DialogResult = DialogResult.OK;
        }

        public void PerformCancel()
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void menuItemStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void menuItemOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void SettingsLoad()
        {
            try
            {

                chB_system_kiskmod.Checked = Settings.chB_system_kiskmod;
                chB_LogOutBezHesla.Checked = Settings.LogOut_bezHesla;

                checkBox_PotvrzeniOperace.Checked = Settings.OdvadeniPotvrzeniOperace;
                checkBox_Prehled.Checked = Settings.OdvadeniPrehled;

                checkBox_ZruseniOperace.Checked = Settings.ZruseniOperace;
                
                checkBox_ZruseniPouzeVS.Checked = Settings.ZruseniPouzeVS;


                textBoxUEventZruseniOdvod.Text = Settings.UEventZruseniOdvod;
                textBoxUEventZruseniPriprava.Text = Settings.UEventZruseniPriprava ;

                textBoxTID.Text = Settings.TerminalID.ToString();
                textBoxMachineID.Text = Settings.MachineID != null ? Settings.MachineID : string.Empty;

                textBox_API_Konst.Text = Settings.API_konstant;
                textBoxURL.Text = Settings.Adresa_API;
                checkBox_isHTTPS.Checked = Settings.isHTTPS;
                textBox_api_auth.Text = Settings.Autorizace_API;
                textBoxURLTimeout.Text = Settings.TimeOut.ToString();

                textBox_connection_DB.Text = Settings.Connection_DB;

                checkBoxLog.Checked = Settings.Loging;
                checkBoxOdvadeniVyberZakazkyPoPrihlaseni.Checked = Settings.VyberZakazkyPoPrihlaseni;
                checkBoxOdvadeniPredvyplnitZbyvajiciMnozstvi.Checked = Settings.PredvyplnitZbyvajiciMnozstvi;
                checkBoxDavkoveZpracovani.Checked = Settings.DavkoveZpracovani;
                checkBoxDavkoveZpracovaniBlokace.Checked = Settings.DavkoveZpracovaniBlokace;
                textBoxDownload.Text = Settings.TimerDownloadInterval.ToString();
                textBoxUpload.Text = Settings.TimerUploadInterval.ToString();
                textBoxPasswordConfig.Text = Settings.PasswordConfig;
                checkBoxOvladaniNumerickouKlavesnici.Checked = Settings.OvladaniNumerickouKlavesnici;
                checkBoxZobrazovatChybySynchronizaceDatabaze.Checked = Settings.ZobrazovatChybySynchronizaceDatabaze;

                checkBoxPrihlaseniSmeny.Checked = Settings.UEventSmenaEnabled;
                checkBoxPrihlaseniPracovnika.Checked = Settings.UEventPracovnikLoginEnabled;
                textBoxUEventSmenaLogin.Text = Settings.UEventSmenaLogin;
                textBoxUEventSmenaLogout.Text = Settings.UEventSmenaLogout;
                textBoxUEventPracovnikPrihlaseni.Text = Settings.UEventPracovnikPrihlaseni;
                textBoxUEventPracovnikOdhlaseni.Text = Settings.UEventPracovnikOdhlaseni;
                checkBoxPracovnikOdhlaseniShowReport.Checked = Settings.UEventPracovnikOdhlaseniShowReport;
                checkBoxUdalostiPovolitZmenuCasuPrihlaseniPracovnika.Checked = Settings.UdalostiPovolitZmenuCasuPrihlaseniPracovnika;
                checkBoxZobrazitCasPrihlaseniPracovnika.Checked = Settings.UdalostiZobrazitCasPrihlaseniPracovnika;
                textBoxUEventSmenaLogoutPasswordConfig.Text = Settings.UEventSmenaLogoutPasswordConfig;
                checkBoxZahajeniZakazkyDotaz.Checked = Settings.ZahajeniZakazkyDotaz;
                checkBoxNecinnostTrvalaDeleNezDotaz.Checked = Settings.NecinnostTrvalaDeleNezDotaz;
                checkBoxSouhrnPoStartzakázky.Checked = Settings.OdvadeniSouhrnPoStartzakazky;
                checkBoxPolozkaJizBylaOdvedenaDotaz.Checked = Settings.OdvadeniPolozkaJizBylaOdvedena;
                checkBoxZadanVetsiPocetKOdvedeniDotaz.Checked = Settings.OdvadeniZadanVetsiPocetKOdvedeniDotaz;
                checkBoxOdvadeniPovolitUkonceniZakazekZJinhoStroje.Checked = Settings.PovolitUkonceniZJinehoStroje;
                checkBoxPovolitOdvadeniMnozstviNula.Checked = Settings.PovolitOdvadeniMnozstviNula;

                comboBoxScannerType.SelectedItem = Settings.ScannerType;
                dateTimePickerUserTimeOut.Value = DateTime.Today + Settings.LoginUserTimeOut;
                checkBoxOdvadeniSledovatCastecneOdvody.Checked = Settings.OdvadeniSledovatCastecneOdvody;
                checkBoxOdvadeniUkonceniStartStopPovolitVlozeniStart.Checked = Settings.OdvadeniUkonceniStartStopPovolitVlozeniStart;

                // nastavení modulù
                textBoxModulPocetRadku.Text = Settings.ModulPocetRadku.ToString();
                textBoxModulPocetSloupcu.Text = Settings.ModulPocetSloupcu.ToString();
                // modul odvadeni
                checkBoxModulPovolitKonzola.Checked = Settings.ModulKonzola;
                checkBoxModulPovolitKonzola_CheckedChanged(null, null);
                textBoxModulKonzolaRow.Text = Settings.ModulKonzolaRow.ToString();
                textBoxModulKonzolaRowSpan.Text = Settings.ModulKonzolaRowSpan.ToString();
                textBoxModulKonzolaColumn.Text = Settings.ModulKonzolaColumn.ToString();
                textBoxModulKonzolaColumnSpan.Text = Settings.ModulKonzolaColumnSpan.ToString();
                //Materialy
                production_Material_Enter.Checked = Settings.Production_Material_Enter;
                Production_Material_PoVyrobe.Checked = Settings.Production_Material_PoVyrobe;
                Production_Material_PredVyrobou.Checked = Settings.Production_Material_PredVyrobou;
                Production_Material_OperacePotvrzeniButton.Checked = Settings.Production_Material_OperacePotvrzeniButton;
                chB_Production_Material_AUTO_vyber.Checked = Settings.Production_Material_AUTO_vyber;
                chB_Production_Material_Dopocist.Checked = Settings.Production_Material_Dopocist;
                chB_Production_Material_DoplnSarzi.Checked= Settings.Production_Material_Doplnit_Sarze;
                txB_Production_Material_Dopocist_Sarze.Text = Settings.Production_Material_Hodnota_Sarze;

                // sklady 
                production_Destination_SKLID_Enter.Checked = Settings.Production_Destination_SKLID_Enter;
                production_Destination_SKLID.Text = Settings.Production_Destination_SKLID;
                production_Destination_LOCNCODE_Enter.Checked = Settings.Production_Destination_LOCNCODE_Enter;
                production_Destination_LOCNCODE.Text = Settings.Production_Destination_LOCNCODE;

                //sklady materialy
                production_Material_Source_SKLID_Enter.Checked = Settings.Production_Material_Source_SKLID_Enter;
                production_Material_Source_SKLID.Text = Settings.Production_Material_Source_SKLID;
                production_Material_Source_LOCNCODE_Enter.Checked = Settings.Production_Material_Source_LOCNCODE_Enter;
                production_Material_Source_LOCNCODE.Text = Settings.Production_Material_Source_LOCNCODE;

                // modul korekce
                checkBoxModulPovolitKorekce.Checked = Settings.ModulPovolitKorekce;
                checkBoxModulPovolitKorekce_CheckedChanged(null, null);
                textBoxModulKorekceRow.Text = Settings.ModulKorekceRow.ToString();
                textBoxModulKorekceRowSpan.Text = Settings.ModulKorekceRowSpan.ToString();
                textBoxModulKorekceColumn.Text = Settings.ModulKorekceColumn.ToString();
                textBoxModulKorekceColumnSpan.Text = Settings.ModulKorekceColumnSpan.ToString();

                // modul udalosti
                checkBoxModulPovolitUdalosti.Checked = Settings.ModulPovolitUdalosti;
                checkBoxModulPovolitUdalosti_CheckedChanged(null, null);
                textBoxModulUdalostiRow.Text = Settings.ModulUdalostiRow.ToString();
                textBoxModulUdalostiRowSpan.Text = Settings.ModulUdalostiRowSpan.ToString();
                textBoxModulUdalostiColumn.Text = Settings.ModulUdalostiColumn.ToString();
                textBoxModulUdalostiColumnSpan.Text = Settings.ModulUdalostiColumnSpan.ToString();
                // modul prehled odvodu
                checkBoxModulPovolitPrehledOdvodu.Checked = Settings.ModulPovolitPrehledOdvodu;
                checkBoxModulPovolitPrehledOdvodu_CheckedChanged(null, null);
                textBoxModulPrehledOdvoduRow.Text = Settings.ModulPrehledOdvoduRow.ToString();
                textBoxModulPrehledOdvoduRowSpan.Text = Settings.ModulPrehledOdvoduRowSpan.ToString();
                textBoxModulPrehledOdvoduColumn.Text = Settings.ModulPrehledOdvoduColumn.ToString();
                textBoxModulPrehledOdvoduColumnSpan.Text = Settings.ModulPrehledOdvoduColumnSpan.ToString();
                textBoxModulPrehledOdvoduAutoUpdateInterval.Text = Settings.ModulPrehledOdvoduAutoUpdateInterval.ToString();
                textBoxModulPrehledOdvoduFilterResetInterval.Text = Settings.ModulPrehledOdvoduFilterResetInterval.ToString();
                textBoxModulPrehledPocetHodinHistorie.Text = Settings.ModulPrehledPocetHodinHistorie.ToString();
                textBoxModulPrehledPocetHodinHistorieServer.Text = Settings.ModulPrehledPocetHodinHistorieServer.ToString();
                checkBoxModulPrehledOdvoduPouzitDataZeServeru.Checked = Settings.ModulPrehledOdvoduPouzitDataZeServeru;
                textBoxModulPrehledOdvoduFontSize.Text = Settings.ModulPrehledOdvoduFontSize.ToString();
                textBoxModulPrehledOdvoduFontRowSize.Text = Settings.ModulPrehledOdvoduFontRowSize.ToString();
                textBoxModulPrehledOdvoduMaxDaysInHistory.Text = Settings.ModulPrehledOdvoduMaxDaysInHistory.ToString();

                // modul nedokoncene zakazky
                checkBoxModulPovolitNedokonceneZakazky.Checked = Settings.ModulPovolitNedokonceneZakazky;
                checkBoxModulPovolitNedokonceneZakazky_CheckedChanged(null, null);
                textBoxModulNedokonceneZakazkyRow.Text = Settings.ModulNedokonceneZakazkyRow.ToString();
                textBoxModulNedokonceneZakazkyRowSpan.Text = Settings.ModulNedokonceneZakazkyRowSpan.ToString();
                textBoxModulNedokonceneZakazkyColumn.Text = Settings.ModulNedokonceneZakazkyColumn.ToString();
                textBoxModulNedokonceneZakazkyColumnSpan.Text = Settings.ModulNedokonceneZakazkyColumnSpan.ToString();
                textBoxModulNedokonceneZakazkyAutoUpdateInterval.Text = Settings.ModulNedokonceneZakazkyAutoUpdateInterval.ToString();
                textBoxModulNedokonceneZakazkyFilterResetInterval.Text = Settings.ModulNedokonceneZakazkyFilterResetInterval.ToString();
                checkBoxModulNedokonceneZakazkyPouzitDataZeServeru.Checked = Settings.ModulNedokonceneZakazkyPouzitDataZeServeru;
                checkBoxModulNedokonceneZakazkyPouzitFiltrNaStroj.Checked = Settings.ModulNedokonceneZakazkyPouzitFiltrNaStroj;
                checkBoxModulNedokonceneZakazkyPouzitFiltrNaUzivatele.Checked = Settings.ModulNedokonceneZakazkyPouzitFiltrNaUzivatele;
                textBoxModulNedokonceneZakazkyFontSize.Text = Settings.ModulNedokonceneZakazkyFontSize.ToString();
                textBoxModulNedokonceneZakazkyFontRowSize.Text = Settings.ModulNedokonceneZakazkyFontRowSize.ToString();

                //modul ukolovani
                checkBox1.Checked = Settings.ModulPovolitUkolovani;
                checkBoxModulPovolitUkolovani_CheckedChanged(null, null);
                textBoxModulUkolovaniRow.Text = Settings.ModulUkolovaniRow.ToString();
                textBoxModulUkolovaniRowSpan.Text = Settings.ModulUkolovaniRowSpan.ToString();
                textBoxModulUkolovaniColumn.Text = Settings.ModulUkolovaniColumn.ToString();
                textBoxModulUkolovaniColumnSpan.Text = Settings.ModulUkolovaniColumnSpan.ToString();

                // modul dotisk
                checkBoxModulPovolitDotisk.Checked = Settings.ModulPovolitDotisk;
                checkBoxModulPovolitDotisk_CheckedChanged(null, null);
                textBoxModulDotiskRow.Text = Settings.ModulDotiskRow.ToString();
                textBoxModulDotiskRowSpan.Text = Settings.ModulDotiskRowSpan.ToString();
                textBoxModulDotiskColumn.Text = Settings.ModulDotiskColumn.ToString();
                textBoxModulDotiskColumnSpan.Text = Settings.ModulDotiskColumnSpan.ToString();

                //tisk stitku
                checkBox_tiskPal.Checked = Settings.TiskPalety;
                checkBox_TiskPotvrzeni.Checked = Settings.TiskPotvrzeni;
                checkBox_TiskMn1Auto.Checked = Settings.TiskMnozstviJednaAutomaticky;
                tB_mnozstvi.Text = Settings.TiskMnozstviPredvyplnit;
                tB_NazevSablony.Text = Settings.TiskNazevSablony;
                tB_NazevTiskarny.Text = Settings.TiskNazevTiskarny;
                checkBox_Tisk_OneWayPrint.Checked = Settings.Tisk_OneWayPrint;
                textBox_popisPoctuTisku.Text = Settings.Tisk_PopisPoctuTisku;
                //generovani SSCC
                chB_SSCC_auto.Checked = Settings.SSCC_Generovani_auto;

                //overeni uzivatele bez hesla
                checkBoxOdvadeniUzivatelOvereniBezHesla.Checked = Settings.Odvadeni_OvereniUzivateleBezHesla;


                switch (Settings.OdvadeniType)
                {
                    case "P": 
                        rbOdvodTypePrikazy.Checked = true;
                        break;
                    case "Z":
                    default:
                        rbOdvodTypeZakazky.Checked = true;
                        break;
                }


                // Korekce
                

                // Korekce
                checkBoxCorrectsEnable.Checked = Settings.CorrectsEnable;
                switch (Settings.CorrectionType)
                {
                    case "0":
                        rbCorrectTypeJednorazove.Checked = true;
                        break;
                    case "1":
                    default:
                        rbCorrectTypePostupne.Checked = true;
                        break;
                }

                checkBoxCorrectsCheckEnable.Checked = Settings.CorrectsCheckEnable;
                checkBoxCorrectsCheckPercentEnable.Checked = Settings.CorrectsCheckPercentEnable;
                checkBoxCorrectsCheckMinimumEnable.Checked = Settings.CorrectsCheckMinimumEnable;

                nuCorrectsCheckPercentValue.Value = Settings.CorrectsCheckPercentValue;
                dateTimePickerCorretsCheckMinimumValue.Value = DateTime.Today + Settings.CorrectsCheckMinimumValue; //v minutach...

                checkBoxCorrectsUsporaZadaniEnable.Checked = Settings.CorrectsUsporaZadaniEnable;

                checkBoxOdvadeniUzivatelHeslo.Checked = Settings.OdvadeniPozadovatHesloUzivatele;
                checkBoxOdvadeniPozadovatZadaniStroje.Checked = Settings.OdvadeniPozadovatZadaniStroje;
                checkBoxOdvadeniZobrazovatCasy.Checked = Settings.OdvadeniZobrazovatCasy;
                checkBoxStartVyrobaPoStopPripravaIhned.Checked = Settings.StopPripravaStartVyrobaIhned;
                textBoxOnline_timeout.Text = Settings.ProductionOnlineTimeout.ToString();
                dateTimePickerUserMaxTimeSpanNoAction.Value = DateTime.Today + Settings.Production_UserMaxTimeSpanNoAction;
                checkBoxOdvadeniOdhlasitUzivatelePriOdchoduZPracoviste.Checked = Settings.OdvadeniOdhlasitUzivatelePriOdchoduZPracoviste;
                checkBoxOdvadeniOdhlasitUzivatelePriUkonceniPrestavky.Checked = Settings.OdvadeniOdhlasitUzivatelePriUkonceniPrestavky;
                checkBoxOdvadeniOdhlasitUzivatelePriZahajeniPrestavky.Checked = Settings.OdvadeniOdhlasitUzivatelePriZahajeniPrestavky;

                //9.2.2024 MaR Šarže
                checkBoxOdvadeniKontrolaSarze.Checked = Settings.OdvadeniKontrolaSarze;



                //Format casu 
                textBoxFormMainTimeFormat.Text = Settings.FormMainTimeFormat;
                nuFormMainTimeFormat.Value = Settings.FormMainTimeFormatWidth;

                try
                {
                    if (Settings.ConfigTiskSablony.Trim().Length > 0)
                        textBoxTiskoveSablony.Text = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), Settings.ConfigTiskSablony);
                    tiskSablony.Clear();
                    tiskSablony.ReadXml(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), Settings.ConfigTiskSablony));
                }
                catch (Exception exc)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exc);
                    FlexibleMessageBox.Show(this, exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter tacor = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter();
                //tacor.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + System.IO.Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));

                if (Settings.PrestavkaID != -1)
                {
                    var dtRows = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Corrects(Settings.PrestavkaID);
                    if(dtRows.Count > 0)
                    {
                        comboBoxPrestavkaID.SelectedValue = dtRows[0].id;
                    } 
                    else  // nenalezen øádek s daným ID
                        comboBoxPrestavkaID.SelectedItem = null;
                } 
                else // ID nebylo vyplnìno 
                    comboBoxPrestavkaID.SelectedItem = null;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private bool SettingsSave()
        {
            try
            {

                Settings.chB_system_kiskmod = chB_system_kiskmod.Checked;


                Settings.TerminalID = byte.Parse(textBoxTID.Text);
                if (textBoxMachineID.Text.Trim().Length > 0)
                    Settings.MachineID = textBoxMachineID.Text.Trim();
                else
                    Settings.MachineID = null;

                Settings.LogOut_bezHesla = chB_LogOutBezHesla.Checked;

                Settings.Connection_DB = textBox_connection_DB.Text.Trim();
                

                Settings.OdvadeniPotvrzeniOperace = checkBox_PotvrzeniOperace.Checked;
                Settings.OdvadeniPrehled = checkBox_Prehled.Checked;

                Settings.ZruseniOperace = checkBox_ZruseniOperace.Checked;
                Settings.ZruseniPouzeVS = checkBox_ZruseniPouzeVS.Checked;
                
                    

                Settings.UEventZruseniOdvod = textBoxUEventZruseniOdvod.Text;
                Settings.UEventZruseniPriprava = textBoxUEventZruseniPriprava.Text;


                Settings.API_konstant = textBox_API_Konst.Text;
                Settings.Adresa_API = textBoxURL.Text;
                Settings.isHTTPS = checkBox_isHTTPS.Checked;
                Settings.Autorizace_API = textBox_api_auth.Text;
                Settings.TimeOut = int.Parse(textBoxURLTimeout.Text);


                Settings.Loging = checkBoxLog.Checked;
                Fask.Logging.ExceptionHandler2.SetEnablePrint(Settings.Loging);

                Settings.VyberZakazkyPoPrihlaseni = checkBoxOdvadeniVyberZakazkyPoPrihlaseni.Checked;
                Settings.PredvyplnitZbyvajiciMnozstvi = checkBoxOdvadeniPredvyplnitZbyvajiciMnozstvi.Checked;
                Settings.DavkoveZpracovani = checkBoxDavkoveZpracovani.Checked;
                Settings.DavkoveZpracovaniBlokace = checkBoxDavkoveZpracovaniBlokace.Checked;
                Settings.TimerDownloadInterval = int.Parse(textBoxDownload.Text);
                Settings.TimerUploadInterval = int.Parse(textBoxUpload.Text);
                if (textBoxPasswordConfig.Text.Trim().Length > 0)
                    Settings.PasswordConfig = textBoxPasswordConfig.Text.Trim();
                Settings.OvladaniNumerickouKlavesnici = checkBoxOvladaniNumerickouKlavesnici.Checked;
                Settings.ZobrazovatChybySynchronizaceDatabaze = checkBoxZobrazovatChybySynchronizaceDatabaze.Checked;

                Settings.UEventSmenaEnabled = checkBoxPrihlaseniSmeny.Checked;
                Settings.UEventPracovnikLoginEnabled = checkBoxPrihlaseniPracovnika.Checked;
                Settings.UEventSmenaLogin = textBoxUEventSmenaLogin.Text.Trim();
                Settings.UEventSmenaLogout = textBoxUEventSmenaLogout.Text.Trim();
                Settings.UEventPracovnikPrihlaseni= textBoxUEventPracovnikPrihlaseni.Text.Trim();
                Settings.UEventPracovnikOdhlaseni= textBoxUEventPracovnikOdhlaseni.Text.Trim();
                Settings.UEventPracovnikOdhlaseniShowReport = checkBoxPracovnikOdhlaseniShowReport.Checked;
                Settings.UEventSmenaLogoutPasswordConfig = textBoxUEventSmenaLogoutPasswordConfig.Text;

                Settings.ScannerType = (Fask.Aktualizace_API.Scanner.ScannerTypes)comboBoxScannerType.SelectedItem;
                // 18.11.2016 - JiS - oprava ukladani TimeSpanu
                //Settings.LoginUserTimeOut = dateTimePickerUserTimeOut.Value - DateTime.Today;
                Settings.LoginUserTimeOut = dateTimePickerUserTimeOut.Value.TimeOfDay;

                Settings.UdalostiPovolitZmenuCasuPrihlaseniPracovnika = checkBoxUdalostiPovolitZmenuCasuPrihlaseniPracovnika.Checked;
                Settings.UdalostiZobrazitCasPrihlaseniPracovnika = checkBoxZobrazitCasPrihlaseniPracovnika.Checked;
                Settings.ZahajeniZakazkyDotaz = checkBoxZahajeniZakazkyDotaz.Checked;
                Settings.NecinnostTrvalaDeleNezDotaz = checkBoxNecinnostTrvalaDeleNezDotaz.Checked;
                Settings.OdvadeniSouhrnPoStartzakazky = checkBoxSouhrnPoStartzakázky.Checked;
                Settings.OdvadeniSledovatCastecneOdvody = checkBoxOdvadeniSledovatCastecneOdvody.Checked;
                Settings.OdvadeniUkonceniStartStopPovolitVlozeniStart = checkBoxOdvadeniUkonceniStartStopPovolitVlozeniStart.Checked;

                if (rbOdvodTypeZakazky.Checked)
                    Settings.OdvadeniType = (string)rbOdvodTypeZakazky.Tag;
                else if (rbOdvodTypePrikazy.Checked)
                    Settings.OdvadeniType = (string)rbOdvodTypePrikazy.Tag;
                else
                    Settings.OdvadeniType = string.Empty;

                //Korekce
                Settings.CorrectsEnable = checkBoxCorrectsEnable.Checked;

                //Korekce
               

                if (rbCorrectTypeJednorazove.Checked)
                    Settings.CorrectionType = (string)rbCorrectTypeJednorazove.Tag;
                else if (rbCorrectTypePostupne.Checked)
                    Settings.CorrectionType = (string)rbCorrectTypePostupne.Tag;
                else
                    Settings.CorrectionType = string.Empty;

                // nastaveni hlidani korekci...
                Settings.CorrectsCheckEnable = checkBoxCorrectsCheckEnable.Checked;
                Settings.CorrectsCheckPercentEnable = checkBoxCorrectsCheckPercentEnable.Checked;
                Settings.CorrectsCheckMinimumEnable = checkBoxCorrectsCheckMinimumEnable.Checked;

                Settings.CorrectsCheckPercentValue = nuCorrectsCheckPercentValue.Value;
                // 18.11.2016 - JiS - oprava ukladani TimeSpanu
                //Settings.CorrectsCheckMinimumValue = dateTimePickerCorretsCheckMinimumValue.Value - DateTime.Today; //v minutach...
                Settings.CorrectsCheckMinimumValue = dateTimePickerCorretsCheckMinimumValue.Value.TimeOfDay;

                Settings.CorrectsUsporaZadaniEnable = checkBoxCorrectsUsporaZadaniEnable.Checked;

                Settings.OdvadeniPozadovatHesloUzivatele = checkBoxOdvadeniUzivatelHeslo.Checked;
                Settings.OdvadeniPozadovatZadaniStroje = checkBoxOdvadeniPozadovatZadaniStroje.Checked;
                Settings.OdvadeniZobrazovatCasy = checkBoxOdvadeniZobrazovatCasy.Checked;
                Settings.OdvadeniPolozkaJizBylaOdvedena = checkBoxPolozkaJizBylaOdvedenaDotaz.Checked;
                Settings.OdvadeniZadanVetsiPocetKOdvedeniDotaz = checkBoxZadanVetsiPocetKOdvedeniDotaz.Checked;
                Settings.StopPripravaStartVyrobaIhned = checkBoxStartVyrobaPoStopPripravaIhned.Checked;
                Settings.ProductionOnlineTimeout = int.Parse(textBoxOnline_timeout.Text);                
                // 18.11.2016 - JiS - oprava ukladani TimeSpanu
                //Settings.Production_UserMaxTimeSpanNoAction = dateTimePickerUserMaxTimeSpanNoAction.Value - DateTime.Today;
                Settings.Production_UserMaxTimeSpanNoAction = dateTimePickerUserMaxTimeSpanNoAction.Value.TimeOfDay;
                Settings.PovolitUkonceniZJinehoStroje = checkBoxOdvadeniPovolitUkonceniZakazekZJinhoStroje.Checked;
                Settings.PovolitOdvadeniMnozstviNula = checkBoxPovolitOdvadeniMnozstviNula.Checked;

                if (comboBoxPrestavkaID.SelectedItem != null)
                    Settings.PrestavkaID = (int)comboBoxPrestavkaID.SelectedValue;
                Settings.OdvadeniOdhlasitUzivatelePriOdchoduZPracoviste = checkBoxOdvadeniOdhlasitUzivatelePriOdchoduZPracoviste.Checked;
                Settings.OdvadeniOdhlasitUzivatelePriUkonceniPrestavky = checkBoxOdvadeniOdhlasitUzivatelePriUkonceniPrestavky.Checked;
                Settings.OdvadeniOdhlasitUzivatelePriZahajeniPrestavky = checkBoxOdvadeniOdhlasitUzivatelePriZahajeniPrestavky.Checked;

                //9.2.2024 MaR Šarže
                Settings.OdvadeniKontrolaSarze = checkBoxOdvadeniKontrolaSarze.Checked;

                //Format casu 
                Settings.FormMainTimeFormat = textBoxFormMainTimeFormat.Text;
                Settings.FormMainTimeFormatWidth = Convert.ToInt32(nuFormMainTimeFormat.Value);

                // nastavení modulù
                Settings.ModulPocetRadku = int.Parse(textBoxModulPocetRadku.Text);
                Settings.ModulPocetSloupcu = int.Parse(textBoxModulPocetSloupcu.Text);
                // modul odvadeni
                Settings.ModulKonzola = checkBoxModulPovolitKonzola.Checked;
                Settings.ModulKonzolaRow = int.Parse(textBoxModulKonzolaRow.Text);
                Settings.ModulKonzolaRowSpan = int.Parse(textBoxModulKonzolaRowSpan.Text);
                Settings.ModulKonzolaColumn = int.Parse(textBoxModulKonzolaColumn.Text);
                Settings.ModulKonzolaColumnSpan = int.Parse(textBoxModulKonzolaColumnSpan.Text);
                //Materialy
                Settings.Production_Material_Enter = production_Material_Enter.Checked;
                Settings.Production_Material_PoVyrobe = Production_Material_PoVyrobe.Checked;
                Settings.Production_Material_PredVyrobou = Production_Material_PredVyrobou.Checked;
                Settings.Production_Material_OperacePotvrzeniButton = Production_Material_OperacePotvrzeniButton.Checked;
                Settings.Production_Material_AUTO_vyber = chB_Production_Material_AUTO_vyber.Checked;
                Settings.Production_Material_Dopocist = chB_Production_Material_Dopocist.Checked;
                Settings.Production_Material_Doplnit_Sarze = chB_Production_Material_DoplnSarzi.Checked;
                Settings.Production_Material_Hodnota_Sarze = txB_Production_Material_Dopocist_Sarze.Text.Trim();
                //sklady
                Settings.Production_Destination_SKLID_Enter = production_Destination_SKLID_Enter.Checked;
                Settings.Production_Destination_SKLID = production_Destination_SKLID.Text.Trim();
                Settings.Production_Destination_LOCNCODE_Enter = production_Destination_LOCNCODE_Enter.Checked;
                Settings.Production_Destination_LOCNCODE = production_Destination_LOCNCODE.Text.Trim();
                Settings.Production_Material_Enter = production_Material_Enter.Checked;
                //sklady materialy
                Settings.Production_Material_Source_SKLID_Enter = production_Material_Source_SKLID_Enter.Checked;
                Settings.Production_Material_Source_SKLID = production_Material_Source_SKLID.Text.Trim();
                Settings.Production_Material_Source_LOCNCODE_Enter = production_Material_Source_LOCNCODE_Enter.Checked;
                Settings.Production_Material_Source_LOCNCODE = production_Material_Source_LOCNCODE.Text.Trim();

                // modul korekce
                Settings.ModulPovolitKorekce = checkBoxModulPovolitKorekce.Checked;
                Settings.ModulKorekceRow = int.Parse(textBoxModulKorekceRow.Text);
                Settings.ModulKorekceRowSpan = int.Parse(textBoxModulKorekceRowSpan.Text);
                Settings.ModulKorekceColumn = int.Parse(textBoxModulKorekceColumn.Text);
                Settings.ModulKorekceColumnSpan = int.Parse(textBoxModulKorekceColumnSpan.Text);

                // modul udalosti
                Settings.ModulPovolitUdalosti = checkBoxModulPovolitUdalosti.Checked;
                Settings.ModulUdalostiRow = int.Parse(textBoxModulUdalostiRow.Text);
                Settings.ModulUdalostiRowSpan = int.Parse(textBoxModulUdalostiRowSpan.Text);
                Settings.ModulUdalostiColumn = int.Parse(textBoxModulUdalostiColumn.Text);
                Settings.ModulUdalostiColumnSpan = int.Parse(textBoxModulUdalostiColumnSpan.Text);

                // modul prehled odvodu
                Settings.ModulPovolitPrehledOdvodu = checkBoxModulPovolitPrehledOdvodu.Checked;
                Settings.ModulPrehledOdvoduRow = int.Parse(textBoxModulPrehledOdvoduRow.Text);
                Settings.ModulPrehledOdvoduRowSpan = int.Parse(textBoxModulPrehledOdvoduRowSpan.Text);
                Settings.ModulPrehledOdvoduColumn = int.Parse(textBoxModulPrehledOdvoduColumn.Text);
                Settings.ModulPrehledOdvoduColumnSpan = int.Parse(textBoxModulPrehledOdvoduColumnSpan.Text);
                Settings.ModulPrehledOdvoduAutoUpdateInterval = int.Parse(textBoxModulPrehledOdvoduAutoUpdateInterval.Text);
                Settings.ModulPrehledOdvoduFilterResetInterval = int.Parse(textBoxModulPrehledOdvoduFilterResetInterval.Text);
                Settings.ModulPrehledPocetHodinHistorie = int.Parse(textBoxModulPrehledPocetHodinHistorie.Text);
                Settings.ModulPrehledPocetHodinHistorieServer = int.Parse(textBoxModulPrehledPocetHodinHistorieServer.Text);

                Settings.ModulPrehledOdvoduFontSize = float.Parse(textBoxModulPrehledOdvoduFontSize.Text);
                Settings.ModulPrehledOdvoduFontRowSize = float.Parse(textBoxModulPrehledOdvoduFontRowSize.Text);
                Settings.ModulPrehledOdvoduMaxDaysInHistory = int.Parse(textBoxModulPrehledOdvoduMaxDaysInHistory.Text);
                Settings.ModulPrehledOdvoduPouzitDataZeServeru = checkBoxModulPrehledOdvoduPouzitDataZeServeru.Checked;

                // modul nedokoncene zakazky
                Settings.ModulPovolitNedokonceneZakazky = checkBoxModulPovolitNedokonceneZakazky.Checked;
                Settings.ModulNedokonceneZakazkyRow = int.Parse(textBoxModulNedokonceneZakazkyRow.Text);
                Settings.ModulNedokonceneZakazkyRowSpan = int.Parse(textBoxModulNedokonceneZakazkyRowSpan.Text);
                Settings.ModulNedokonceneZakazkyColumn = int.Parse(textBoxModulNedokonceneZakazkyColumn.Text);
                Settings.ModulNedokonceneZakazkyColumnSpan = int.Parse(textBoxModulNedokonceneZakazkyColumnSpan.Text);
                Settings.ModulNedokonceneZakazkyAutoUpdateInterval = int.Parse(textBoxModulNedokonceneZakazkyAutoUpdateInterval.Text);
                Settings.ModulNedokonceneZakazkyFilterResetInterval = int.Parse(textBoxModulNedokonceneZakazkyFilterResetInterval.Text);
                Settings.ModulNedokonceneZakazkyPouzitDataZeServeru = checkBoxModulNedokonceneZakazkyPouzitDataZeServeru.Checked;
                Settings.ModulNedokonceneZakazkyPouzitFiltrNaStroj = checkBoxModulNedokonceneZakazkyPouzitFiltrNaStroj.Checked;
                Settings.ModulNedokonceneZakazkyPouzitFiltrNaUzivatele = checkBoxModulNedokonceneZakazkyPouzitFiltrNaUzivatele.Checked;
                Settings.ModulNedokonceneZakazkyFontSize = float.Parse(textBoxModulNedokonceneZakazkyFontSize.Text);
                Settings.ModulNedokonceneZakazkyFontRowSize = float.Parse(textBoxModulNedokonceneZakazkyFontRowSize.Text);

                // modul ukolovani
                Settings.ModulPovolitUkolovani = checkBox1.Checked;
                Settings.ModulUkolovaniRow = int.Parse(textBoxModulUkolovaniRow.Text);
                Settings.ModulUkolovaniRowSpan = int.Parse(textBoxModulUkolovaniRowSpan.Text);
                Settings.ModulUkolovaniColumn = int.Parse(textBoxModulUkolovaniColumn.Text);
                Settings.ModulUkolovaniColumnSpan = int.Parse(textBoxModulUkolovaniColumnSpan.Text);

                // modul dotisk
                Settings.ModulPovolitDotisk = checkBoxModulPovolitDotisk.Checked;
                Settings.ModulDotiskRow = int.Parse(textBoxModulDotiskRow.Text);
                Settings.ModulDotiskRowSpan = int.Parse(textBoxModulDotiskRowSpan.Text);
                Settings.ModulDotiskColumn = int.Parse(textBoxModulDotiskColumn.Text);
                Settings.ModulDotiskColumnSpan = int.Parse(textBoxModulDotiskColumnSpan.Text);


                //tisk stitku
                Settings.TiskPalety = checkBox_tiskPal.Checked;
                Settings.TiskPotvrzeni = checkBox_TiskPotvrzeni.Checked;
                Settings.TiskMnozstviJednaAutomaticky = checkBox_TiskMn1Auto.Checked;
                Settings.TiskMnozstviPredvyplnit = tB_mnozstvi.Text.Trim();
                Settings.TiskNazevSablony = tB_NazevSablony.Text.Trim();
                Settings.TiskNazevTiskarny = tB_NazevTiskarny.Text.Trim();
                Settings.Tisk_OneWayPrint = checkBox_Tisk_OneWayPrint.Checked;
                Settings.Tisk_PopisPoctuTisku = textBox_popisPoctuTisku.Text.Trim();

                //generovani SSCC
                Settings.SSCC_Generovani_auto = chB_SSCC_auto.Checked;

                //overeni uzivatele bez hesla
                Settings.Odvadeni_OvereniUzivateleBezHesla = checkBoxOdvadeniUzivatelOvereniBezHesla.Checked;

                Settings.Update();

                tiskSablony.WriteXml(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), Settings.ConfigTiskSablony));
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }

            return true;
        }

        private void checkBoxPrihlaseniSmeny_CheckStateChanged(object sender, EventArgs e)
        {
            this.textBoxUEventSmenaLogin.Enabled = this.textBoxUEventSmenaLogout.Enabled = this.checkBoxPrihlaseniSmeny.Enabled;
        }

        private void buttonSouborTiskSablony_Click(object sender, EventArgs e)
        {
            ofdTiskSablona.FileName = textBoxTiskoveSablony.Text;
            if (ofdTiskSablona.ShowDialog(this) == DialogResult.Cancel)
                return;

            textBoxTiskoveSablony.Text = ofdTiskSablona.FileName;
            tiskSablony.Clear();
            tiskSablony.ReadXml(textBoxTiskoveSablony.Text);
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void buttonScannerSettings_Click(object sender, EventArgs e)
        {
            if ((Scanner.ScannerTypes)comboBoxScannerType.SelectedItem == Scanner.ScannerTypes.COM)
            {
                Scanner.ScannerCOM sc = new Fask.Aktualizace_API.Scanner.ScannerCOM();
                sc.ScannerSetting();
            }
        }

        private void rbOdvodTypeZakazky_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxOdvadeniOdhlasitUzivatelePriZahajeniPrestavky.Enabled = true;
            checkBoxOdvadeniOdhlasitUzivatelePriOdchoduZPracoviste.Enabled = true;
            checkBoxOdvadeniOdhlasitUzivatelePriUkonceniPrestavky.Enabled = true;
            comboBoxPrestavkaID.Enabled = true;
        }

        private void rbOdvodTypePrikazy_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxOdvadeniOdhlasitUzivatelePriZahajeniPrestavky.Enabled = false;
            checkBoxOdvadeniOdhlasitUzivatelePriOdchoduZPracoviste.Enabled = false;
            checkBoxOdvadeniOdhlasitUzivatelePriUkonceniPrestavky.Enabled = false;
            comboBoxPrestavkaID.Enabled = false;
        }

        private void checkBoxModulPovolitKonzola_CheckedChanged(object sender, EventArgs e)
        {
            textBoxModulKonzolaRow.Enabled = checkBoxModulPovolitKonzola.Checked;
            textBoxModulKonzolaRowSpan.Enabled = checkBoxModulPovolitKonzola.Checked;
            textBoxModulKonzolaColumn.Enabled = checkBoxModulPovolitKonzola.Checked;
            textBoxModulKonzolaColumnSpan.Enabled = checkBoxModulPovolitKonzola.Checked;
        }

        private void checkBoxModulPovolitDotisk_CheckedChanged(object sender, EventArgs e)
        {
            textBoxModulDotiskRow.Enabled = checkBoxModulPovolitDotisk.Checked;
            textBoxModulDotiskRowSpan.Enabled = checkBoxModulPovolitDotisk.Checked;
            textBoxModulDotiskColumn.Enabled = checkBoxModulPovolitDotisk.Checked;
            textBoxModulDotiskColumnSpan.Enabled = checkBoxModulPovolitDotisk.Checked;
        }

        private void checkBoxModulPovolitKorekce_CheckedChanged(object sender, EventArgs e)
        {
            textBoxModulKorekceRow.Enabled = checkBoxModulPovolitKorekce.Checked;
            textBoxModulKorekceRowSpan.Enabled = checkBoxModulPovolitKorekce.Checked;
            textBoxModulKorekceColumn.Enabled = checkBoxModulPovolitKorekce.Checked;
            textBoxModulKorekceColumnSpan.Enabled = checkBoxModulPovolitKorekce.Checked;
        }

        private void checkBoxModulPovolitUdalosti_CheckedChanged(object sender, EventArgs e)
        {
            textBoxModulUdalostiRow.Enabled = checkBoxModulPovolitUdalosti.Checked;
            textBoxModulUdalostiRowSpan.Enabled = checkBoxModulPovolitUdalosti.Checked;
            textBoxModulUdalostiColumn.Enabled = checkBoxModulPovolitUdalosti.Checked;
            textBoxModulUdalostiColumnSpan.Enabled = checkBoxModulPovolitUdalosti.Checked;
        }

        private void checkBoxModulPovolitPrehledOdvodu_CheckedChanged(object sender, EventArgs e)
        {
            textBoxModulPrehledOdvoduRow.Enabled = checkBoxModulPovolitPrehledOdvodu.Checked;
            textBoxModulPrehledOdvoduRowSpan.Enabled = checkBoxModulPovolitPrehledOdvodu.Checked;
            textBoxModulPrehledOdvoduColumn.Enabled = checkBoxModulPovolitPrehledOdvodu.Checked;
            textBoxModulPrehledOdvoduColumnSpan.Enabled = checkBoxModulPovolitPrehledOdvodu.Checked;
            textBoxModulPrehledOdvoduAutoUpdateInterval.Enabled = checkBoxModulPovolitPrehledOdvodu.Checked;
            textBoxModulPrehledOdvoduFilterResetInterval.Enabled = checkBoxModulPovolitPrehledOdvodu.Checked;
            textBoxModulPrehledPocetHodinHistorie.Enabled = checkBoxModulPovolitPrehledOdvodu.Checked;
            checkBoxModulPrehledOdvoduPouzitDataZeServeru.Enabled = checkBoxModulPovolitPrehledOdvodu.Checked;
            textBoxModulPrehledOdvoduFontSize.Enabled = checkBoxModulPovolitPrehledOdvodu.Checked;
            textBoxModulPrehledOdvoduFontRowSize.Enabled = checkBoxModulPovolitPrehledOdvodu.Checked;
            textBoxModulPrehledOdvoduMaxDaysInHistory.Enabled = checkBoxModulPovolitPrehledOdvodu.Checked;
        }

        private void checkBoxModulPovolitNedokonceneZakazky_CheckedChanged(object sender, EventArgs e)
        {
            textBoxModulNedokonceneZakazkyRow.Enabled = checkBoxModulPovolitNedokonceneZakazky.Checked;
            textBoxModulNedokonceneZakazkyRowSpan.Enabled = checkBoxModulPovolitNedokonceneZakazky.Checked;
            textBoxModulNedokonceneZakazkyColumn.Enabled = checkBoxModulPovolitNedokonceneZakazky.Checked;
            textBoxModulNedokonceneZakazkyColumnSpan.Enabled = checkBoxModulPovolitNedokonceneZakazky.Checked;
            textBoxModulNedokonceneZakazkyAutoUpdateInterval.Enabled = checkBoxModulPovolitNedokonceneZakazky.Checked;
            textBoxModulNedokonceneZakazkyFilterResetInterval.Enabled = checkBoxModulPovolitNedokonceneZakazky.Checked;
            checkBoxModulNedokonceneZakazkyPouzitDataZeServeru.Enabled = checkBoxModulPovolitNedokonceneZakazky.Checked;
            textBoxModulNedokonceneZakazkyFontSize.Enabled = checkBoxModulPovolitNedokonceneZakazky.Checked;
            textBoxModulNedokonceneZakazkyFontRowSize.Enabled = checkBoxModulPovolitNedokonceneZakazky.Checked;
            checkBoxModulNedokonceneZakazkyPouzitFiltrNaStroj.Enabled = checkBoxModulPovolitNedokonceneZakazky.Checked;
            checkBoxModulNedokonceneZakazkyPouzitFiltrNaUzivatele.Enabled = checkBoxModulPovolitNedokonceneZakazky.Checked;
        }

        private void checkBoxModulPovolitUkolovani_CheckedChanged(object sender, EventArgs e)
        {
            textBoxModulUkolovaniRow.Enabled = checkBox1.Checked;
            textBoxModulUkolovaniRowSpan.Enabled = checkBox1.Checked;
            textBoxModulUkolovaniColumn.Enabled = checkBox1.Checked;
            textBoxModulUkolovaniColumnSpan.Enabled = checkBox1.Checked;
        }

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void checkBoxCorrectsEnable_CheckedChanged(object sender, EventArgs e)
        {
            gbCorrectType.Enabled = checkBoxCorrectsEnable.Checked;
        }

        private void checkBoxCorrectsCheck_CheckedChanged(object sender, EventArgs e)
        {
            //vizualni omezeni nastaveni procenta korekci ...
            nuCorrectsCheckPercentValue.Enabled = checkBoxCorrectsCheckPercentEnable.Checked;
        }

        private void checkBoxCorrectsCheckEnable_CheckedChanged(object sender, EventArgs e)
        {
            panelCorrectsParams.Enabled = checkBoxCorrectsCheckEnable.Checked;
        }

        private void checkBoxCorretsCheckMinimumEnable_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerCorretsCheckMinimumValue.Enabled = checkBoxCorrectsCheckMinimumEnable.Checked;
        }

        private void checkBoxPrihlaseniPracovnika_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxPracovnikOdhlaseniShowReport.Enabled = checkBoxPrihlaseniPracovnika.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.ucTime1.FormatTimeWidth = Convert.ToInt32(nuFormMainTimeFormat.Value);
            }
            catch 
            {
            }
        }

        private void textBoxFormMainTimeFormat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ucTime1.FormatTime = textBoxFormMainTimeFormat.Text;
            }
            catch
            {
            }
        }

    }
}

