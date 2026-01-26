using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using Fask.ScannerProvider;

namespace Fask.MST_W.Prijem_4
{
    public partial class TypOznaceniPaletyForm : System.Windows.Forms.Form
    {


		#region Parametry

		private Fask.MST_W.Classes.Paleta _typOznaceni = new Fask.MST_W.Classes.Paleta();
		public Fask.MST_W.Classes.Paleta TypOznaceni
		{
			get { return _typOznaceni; }
			set
			{
				this._typOznaceni = value;
				tOznaceni.Text = value.sscc;
				Schema.TypyPalet.PaletyRow paletar = typyPalet.Palety.FindByID(value.ID);
				cbTyp.SelectedItem = paletar;
				tOznaceni.Focus();
				tOznaceni.SelectAll();
			}
		}

		public int Cislo
		{
			get
			{
				return int.Parse(tOznaceni.Text);
			}
			set
			{
				this.tOznaceni.Text = value.ToString();
				this.tOznaceni.Focus();
				this.tOznaceni.SelectAll();
			}
		}

		
		#endregion

		#region c'tor

		public TypOznaceniPaletyForm()
		{
			InitializeComponent();
			this.tOznaceni.Text = "";

			try
			{
				typyPalet.Clear();
				typyPalet.ReadXml(MST_W.Main.ConfigTypyPalet, XmlReadMode.IgnoreSchema);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text);
				Logging.Log.Write(ex.Message, this.Text);
			}

			UpdateTyp();

		}

		public TypOznaceniPaletyForm(bool visible)
		{
			InitializeComponent();

			this.tOznaceni.Text = "1";

			label2.Visible = visible;
			tOznaceni.Visible = visible;

			try
			{
				//typyPalet = new Fask.MST_W.Schema.TypyPalet();
				typyPalet.Clear();
				typyPalet.ReadXml(MST_W.Main.ConfigTypyPalet, XmlReadMode.IgnoreSchema);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text);
				Logging.Log.Write(ex.Message, this.Text);
			}

			UpdateTyp();

		}

		#endregion

		#region Eventy formu

		private void TypOznaceniPaletyForm_Load(object sender, EventArgs e)
		{
			// nacteni lokalizace ze souboru
			Fask.Localization.LocalizationExtensionForm.Localize(this);

			this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
			this.Size = Forms.FormLocation.ScreenResolution;

			ScannerStart();
		}

		private void TypOznaceniPaletyForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				PerformOK();
			}
			else if (e.KeyCode == Keys.Escape)
			{
				PerformCancel();
			}
			else
			{
				return;
			}

			e.Handled = true;
		}

		private void TypOznaceniPaletyForm_Activated(object sender, EventArgs e)
		{
			// prepnuti na numerickou klavesnici
			Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
		}

		private void TypOznaceniPaletyForm_Closing(object sender, CancelEventArgs e)
		{
			ScannerFinalize();
		}


		#endregion

		#region Scanner start stop

		private bool scannserstart = true;
		private void ScannerFinalize()
		{
			this.ScannerStop();
			this.scannserstart = false;
		}

		private void ScannerStart()
		{
			if (!scannserstart)
				return;

			Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
			Program.mstw.EnableScanner();
		}

		private void ScannerStop()
		{
			Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
			Program.mstw.DisableScanner();
		}

		delegate void ScannerEventHandlerCall(ScannerEventArgs e);

		private void UpdateUI(ScannerEventArgs e)
		{
			string ck = e.BarcodeData.Trim();

			try
			{
				if (ck.Length > 0)
				{
					//Fask.MST_W.Classes.SSCC sscc = Fask.MST_W.Classes.SSCC.Parse(ck);
					this.tOznaceni.Text = ck;
				}
			}
			catch (Exception ex)
			{
				MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);

			}

			PerformOK();
			if (MST_Global.OnScannerSound_Vydej_3)
			{
				MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
			}
		}

		void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
		{
			if (e.BarcodeData.Trim().Length > 0)
				this.BeginInvoke(new ScannerEventHandlerCall(UpdateUI), new object[] { e });

			//if (IsNumber(TXT_pocet_SN2.Text))
			//    BTN_generate_zazkaznicke_SN_Click(sender, e);
		}

		#endregion

		#region Button events


		private void ok_but_Click(object sender, EventArgs e)
		{
			PerformOK();
		}

		private void zpet_but_Click(object sender, EventArgs e)
		{
			PerformCancel();
		}
		
		#endregion


        private void UpdateTyp()
        {
            cbTyp.BeginUpdate();
            foreach (Schema.TypyPalet.PaletyRow prow in typyPalet.Palety)
            {
                cbTyp.Items.Add(prow);
            }
            cbTyp.EndUpdate();

            cbTyp.SelectedIndex = 0;
        }

        private void PerformOK()
        {
            try
            {

                //TaD count jedna se o to po kolko se budu prièitavat k seriovemu cislu palety
                int count = 1;


                //TaD 19.12.2017
                if (tOznaceni.Text.Length == 0)
                {
					tOznaceni.Text = Fask.MST_W.Vydej_3.Vydej.vydejInstance.globalObject.service_vydej.Terminal_GetSSCCCode(Globals.SequenceSSCC, count, MST_Global.TerminalID).ToString();
                    _typOznaceni = new Fask.MST_W.Classes.Paleta(((Schema.TypyPalet.PaletyRow)cbTyp.SelectedItem).ID, ((Schema.TypyPalet.PaletyRow)cbTyp.SelectedItem).Name, tOznaceni.Text);

                    this.DialogResult = DialogResult.OK;
                    return;
                }

                //Verifikace  SSCC
                if (SSCC_Verifikace(tOznaceni.Text))
                {

                    Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable dt = new Fask.SQLiteDBs.DataSets.Prijem.CZMST_PIDataTable();
					Fask.MST_W.Prijem_4.PrijemMain.prijemInstance.globalObject.controller_prijem.FillBy_TOPjeden_NMBRPAL_PI(dt, tOznaceni.Text);

                    if (dt.Count != 0)
                    {
                        //this._typOznaceni = tOznaceni.Text;
                        _typOznaceni = new Fask.MST_W.Classes.Paleta(((Schema.TypyPalet.PaletyRow)cbTyp.SelectedItem).ID, ((Schema.TypyPalet.PaletyRow)cbTyp.SelectedItem).Name, tOznaceni.Text);

                    }
                    else
                    {

                        MessageBoxBig.Show(Fask.Localization.Localization.Vydej3TypOynaceniPaletyFormNenalezenaPaleta, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
                        tOznaceni.Focus();
                        tOznaceni.SelectAll();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBoxBig.Show(ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
                tOznaceni.Focus();
                tOznaceni.SelectAll();
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        public bool SSCC_Verifikace(string s)
        {
            //SSCC sscc = new SSCC();
            if (s.Length != 20)
                throw new Exception(Fask.Localization.Localization.Vydej3TypOynaceniPaletyFormERR_DelkaSSCC);

            string AI = s.Substring(0, 2);
            if (AI != "00")
                throw new Exception(Fask.Localization.Localization.Vydej3TypOynaceniPaletyFormERR_neplatnyAI);

            string CheckDigit = CountParity(s.Substring(0, 19));

            if (CheckDigit != s.Substring(s.Length - 1, 1))
                throw new Exception(Fask.Localization.Localization.Vydej3TypOynaceniPaletyFormERR_neplatnyCRC);

            return true;
        }

        private void PerformCancel()
        {
            ScannerFinalize();
            this.DialogResult = DialogResult.Cancel;
        }

        private void kontrolaTypPaletyNmbrpal()
        {
            string styp = ((Schema.TypyPalet.PaletyRow)cbTyp.SelectedItem).ID;
            //_ta_si.UpdateNmbrpalTypepalByNmbrpal(tOznaceni.Text.Trim(), styp, tOznaceni.Text.Trim());
        }

        private void cbTyp_TextChanged(object sender, EventArgs e)
        {
            if (!tOznaceni.Focused)
                tOznaceni.Focus();
            tOznaceni.SelectAll();
        }

        /// <summary>
        /// Vypocet kontrolniho cisla : Modulo 10
        /// </summary>
        /// <param name="newSSCC"></param>
        /// <returns></returns>
        /// <remarks>Mod 10 Check Digit
        ///The calculations for determining the Mod 10 Check Digit character are as follows:
        ///1. Start at the first position and add the value of every other position together.
        ///0 + 2 + 4 + 6 + 8 + 0 = 20
        ///2. The result of Step 1 is multiplied by 3.
        ///20 x 3 = 60
        ///3. Start at the second position and add the value of every other position together.
        ///1 + 3 + 5 + 7 + 9 = 25
        ///4. The results of steps 1 and 3 are added together.
        ///60 + 25 = 85
        ///5. The check character (12th character) is the smallest number which, when added to the
        ///result in step 4, produces a multiple of 10.
        ///85 + X = 90 (next higher multiple of 10)
        ///X = 5 Check Character
        ///</remarks>
        private static string CountParity(string newSSCC)
        {
            int sumLiche = 0;
            int sumSude = 0;

            // index : hodnota
            // 0,1 : "0"
            // 2-19 : cisla
            // 20 : kontrolni cislo
            for (int i = 2; i < newSSCC.Length; i++)
            {                  
                if ((i+1) % 2 == 0) //Sude poradove cislo
                    sumSude += int.Parse(newSSCC[i].ToString());
                else //je liche poradove cislo
                    sumLiche += int.Parse(newSSCC[i].ToString());
            }

            return ((10 - (sumLiche * 3 + sumSude) % 10) % 10).ToString();

        }
    }
}