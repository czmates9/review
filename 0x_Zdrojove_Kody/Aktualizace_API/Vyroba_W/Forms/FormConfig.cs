using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.Vyroba_W.Forms
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

        private void FormBaseButtonOKStorno_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);

            comboBoxScannerType.Items.Add(Fask.MST_W.Scanner.ScannerTypes.None); 
            comboBoxScannerType.Items.Add(Fask.MST_W.Scanner.ScannerTypes.Unitech_HT660);
            comboBoxScannerType.Items.Add(Fask.MST_W.Scanner.ScannerTypes.Unitech_PA600);
            comboBoxScannerType.Items.Add(Fask.MST_W.Scanner.ScannerTypes.Symbol_MC3000);
            comboBoxScannerType.Items.Add(Fask.MST_W.Scanner.ScannerTypes.Symbol_PT8800);

            SettingsLoad();
        }

        private void FormBaseButtonOKStorno_KeyDown(object sender, KeyEventArgs e)
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
                //tisky
                textBoxURLTisk.Text = Settings.TiskWebServiceAddress;
                textBoxURLTiskTimeout.Text = Settings.TiskWebServiceTimeOut.ToString();
                checkBoxTiskCekatNaOdpovedTisku.Checked = !Settings.TiskOneWayPrint;
                
                checkBoxTiskPotvrzovatPocetVytisku.Checked = Settings.TiskPotvrzovaniPoctuVytisku;
                textBoxTiskPocetVytisku.Text = Settings.TiskPocetVytisku;
                textBoxTiskBaleniTiskarna.Text = Settings.TiskBaleniTiskarna;
                textBoxTiskBaleniSablona.Text = Settings.TiskBaleniSablona;

                //System
                textBoxTID.Text = Settings.TerminalID.ToString();

				textBox_API_Konst.Text = Settings.API_konstant;
                textBoxURL.Text = Settings.Adresa_API;
				checkBox_isHTTPS.Checked = Settings.isHTTPS;
				textBox_api_auth.Text = Settings.Autorizace_API;
				textBoxURLTimeout.Text = Settings.TimeOut.ToString();


                checkBoxLog.Checked = Settings.Loging;
                textBoxDownload.Text = Settings.TimerDownloadInterval.ToString();
                textBoxUpload.Text = Settings.TimerUploadInterval.ToString();

                //Odvadeni
                checkBox_PotvrzeníOperace.Checked = Settings.OdvadeniPotvrzeniOperace;
                checkBox_Prehled.Checked = Settings.OdvadeniPrehled;

                checkBox_CasNecinnosti.Checked = Settings.Odvadeni_CasNecinnosti;

                production_onlyPositive.Checked = Settings.Odvadeni_production_onlyPositive;
                production_NeupozornovatNaVetsiPocet.Checked = Settings.Odvadeni_production_NeupozornovatNaVetsiPocet;

                checkBoxPrihlaseniSmeny.Checked = Settings.UEventSmenaEnabled;
                textBoxUEventSmenaLogin.Text = Settings.UEventSmenaLogin;
                textBoxUEventSmenaLogout.Text = Settings.UEventSmenaLogout;
                checkBoxPrihlaseniPracovnika.Checked = Settings.UEventPracovnikLoginEnabled;
                textBoxUEventPracovnikPrihlaseni.Text = Settings.UEventPracovnikPrihlaseni;
                textBoxUEventPracovnikOdhlaseni.Text = Settings.UEventPracovnikOdhlaseni;

                comboBoxScannerType.SelectedItem = Settings.ScannerType;
                dateTimePickerUserTimeOut.Value = DateTime.Today + Settings.LoginUserTimeOut;

                production_online_timeout.Text = Settings.ProductionOnlineTimeout.ToString();                
                production_UserMaxTimeSpanNoAction.Value = DateTime.Today + Settings.Production_UserMaxTimeSpanNoAction;
                production_machineID.Text = Settings.Production_MachineID_Preset;
                production_VyberZakazkyPoPrihlaseni.Checked = Settings.VyberZakazkyPoPrihlaseni;

                production_StartVyrobaPoStopPripravaIhned.Checked = Settings.StopPripravaStartVyrobaIhned;
                production_StopVyrobaPoStartVyrobaIhned.Checked = Settings.StopVyrobaPoStartVyrobaIhned;
                production_SledovatCastecneOdvody.Checked = Settings.OdvadeniSledovatCastecneOdvody;

                production_Destination_SKLID_Enter.Checked = Settings.Production_Destination_SKLID_Enter;
                production_Destination_SKLID.Text = Settings.Production_Destination_SKLID;
                production_Destination_LOCNCODE_Enter.Checked = Settings.Production_Destination_LOCNCODE_Enter;
                production_Destination_LOCNCODE.Text = Settings.Production_Destination_LOCNCODE;
                production_Material_Enter.Checked = Settings.Production_Material_Enter;

                Production_Material_PoVyrobe.Checked = Settings.Production_Material_PoVyrobe;
                Production_Material_PredVyrobou.Checked = Settings.Production_Material_PredVyrobou;
                Production_Material_OperacePotvrzeniButton.Checked = Settings.Production_Material_OperacePotvrzeniButton;

                production_Material_Source_SKLID_Enter.Checked = Settings.Production_Material_Source_SKLID_Enter;
                production_Material_Source_SKLID.Text = Settings.Production_Material_Source_SKLID;
                production_Material_Source_LOCNCODE_Enter.Checked = Settings.Production_Material_Source_LOCNCODE_Enter;
                production_Material_Source_LOCNCODE.Text = Settings.Production_Material_Source_LOCNCODE;

                production_Online_Actions.Checked = Settings.Vyroba_Online;

				parsing_Enable.Checked = Settings.PovolParsovani;
				parsing_WeightCode.Checked = Settings.PovolParsovani_WeightCode;
				parsing_WeightCode12.Checked = Settings.PovolParsovani_WeightCode_12;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex.Message, this.Text);
            }
        }

        private bool SettingsSave()
        {
            try
            {
                //tisky
                Settings.TiskWebServiceAddress = textBoxURLTisk.Text;
                Settings.TiskWebServiceTimeOut = int.Parse(textBoxURLTiskTimeout.Text);
                Settings.TiskOneWayPrint = !checkBoxTiskCekatNaOdpovedTisku.Checked;

                Settings.TiskPotvrzovaniPoctuVytisku = checkBoxTiskPotvrzovatPocetVytisku.Checked;
                Settings.TiskPocetVytisku = textBoxTiskPocetVytisku.Text;

                //tisky baleni
                Settings.TiskBaleniTiskarna = textBoxTiskBaleniTiskarna.Text;
                Settings.TiskBaleniSablona = textBoxTiskBaleniSablona.Text;

                Settings.TerminalID = byte.Parse(textBoxTID.Text);

				Settings.API_konstant = textBox_API_Konst.Text;
				Settings.Adresa_API = textBoxURL.Text;
				Settings.isHTTPS = checkBox_isHTTPS.Checked;
				Settings.Autorizace_API = textBox_api_auth.Text;
                Settings.TimeOut = int.Parse(textBoxURLTimeout.Text);


                Settings.Loging = checkBoxLog.Checked;
				


                Settings.TimerDownloadInterval = int.Parse(textBoxDownload.Text);
                Settings.TimerUploadInterval = int.Parse(textBoxUpload.Text);

                Settings.OdvadeniPotvrzeniOperace = checkBox_PotvrzeníOperace.Checked;
                Settings.OdvadeniPrehled = checkBox_Prehled.Checked;

                Settings.Odvadeni_CasNecinnosti = checkBox_CasNecinnosti.Checked;

                Settings.Odvadeni_production_onlyPositive = production_onlyPositive.Checked;
                Settings.Odvadeni_production_NeupozornovatNaVetsiPocet = production_NeupozornovatNaVetsiPocet.Checked;

                Settings.UEventSmenaEnabled = checkBoxPrihlaseniSmeny.Checked;
                Settings.UEventSmenaLogin = textBoxUEventSmenaLogin.Text.Trim();
                Settings.UEventSmenaLogout = textBoxUEventSmenaLogout.Text.Trim();
                Settings.UEventPracovnikLoginEnabled = checkBoxPrihlaseniPracovnika.Checked;
                Settings.UEventPracovnikPrihlaseni= textBoxUEventPracovnikPrihlaseni.Text.Trim();
                Settings.UEventPracovnikOdhlaseni= textBoxUEventPracovnikOdhlaseni.Text.Trim();

                Settings.ScannerType = (Fask.MST_W.Scanner.ScannerTypes)comboBoxScannerType.SelectedItem;
                Settings.LoginUserTimeOut = dateTimePickerUserTimeOut.Value - DateTime.Today;

                Settings.ProductionOnlineTimeout = int.Parse(production_online_timeout.Text);
                Settings.Production_UserMaxTimeSpanNoAction = production_UserMaxTimeSpanNoAction.Value - DateTime.Today;
                Settings.Production_MachineID_Preset = production_machineID.Text.Trim();
                Settings.VyberZakazkyPoPrihlaseni = production_VyberZakazkyPoPrihlaseni.Checked;

                Settings.StopPripravaStartVyrobaIhned = production_StartVyrobaPoStopPripravaIhned.Checked;
                Settings.StopVyrobaPoStartVyrobaIhned = production_StopVyrobaPoStartVyrobaIhned.Checked;
                Settings.OdvadeniSledovatCastecneOdvody = production_SledovatCastecneOdvody.Checked;

                Settings.Production_Destination_SKLID_Enter = production_Destination_SKLID_Enter.Checked;
                Settings.Production_Destination_SKLID = production_Destination_SKLID.Text.Trim();
                Settings.Production_Destination_LOCNCODE_Enter = production_Destination_LOCNCODE_Enter.Checked;
                Settings.Production_Destination_LOCNCODE = production_Destination_LOCNCODE.Text.Trim();
                Settings.Production_Material_Enter = production_Material_Enter.Checked;

                Settings.Production_Material_PoVyrobe = Production_Material_PoVyrobe.Checked ;
                Settings.Production_Material_PredVyrobou = Production_Material_PredVyrobou.Checked ;
                Settings.Production_Material_OperacePotvrzeniButton = Production_Material_OperacePotvrzeniButton.Checked;


                Settings.Production_Material_Source_SKLID_Enter = production_Material_Source_SKLID_Enter.Checked;
                Settings.Production_Material_Source_SKLID = production_Material_Source_SKLID.Text.Trim();
                Settings.Production_Material_Source_LOCNCODE_Enter = production_Material_Source_LOCNCODE_Enter.Checked;
                Settings.Production_Material_Source_LOCNCODE = production_Material_Source_LOCNCODE.Text.Trim();

                Settings.Vyroba_Online = production_Online_Actions.Checked;

				Settings.PovolParsovani = parsing_Enable.Checked;
				Settings.PovolParsovani_WeightCode = parsing_WeightCode.Checked;
				Settings.PovolParsovani_WeightCode_12 = parsing_WeightCode12.Checked;

                Settings.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				Logging.Log.Write(ex.Message, this.Text);
                return false;
            }

            return true;
        }

        private void checkBoxPrihlaseniSmeny_CheckStateChanged(object sender, EventArgs e)
        {
            this.textBoxUEventSmenaLogin.Enabled = this.textBoxUEventSmenaLogout.Enabled = this.checkBoxPrihlaseniSmeny.Enabled;
        }

    }
}

