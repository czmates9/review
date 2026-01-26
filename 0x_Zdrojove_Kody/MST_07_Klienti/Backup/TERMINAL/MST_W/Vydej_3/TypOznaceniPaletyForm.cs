using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;
using Fask.ScannerProvider;

namespace Fask.MST_W.Vydej_3
{
    public partial class TypOznaceniPaletyForm : System.Windows.Forms.Form
    {
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

		private bool _automat = false;
		public bool Automat
		{
			set { _automat = value; }
			get { return _automat; }
		}


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

        private void TypOznaceniPaletyForm_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            ScannerStart();

			if (Automat)
			{
				PerformOK();
			}
        }

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

        private void ok_but_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }


        private void PerformOK()
        {
            try
            {

                //TaD count jedna se o to po kolko se budu pri4itavat k seriovemu cislu palety
                int count = 1;


                //TaD 19.12.2017
                if (tOznaceni.Text.Length == 0)
                {
                    tOznaceni.Text = Vydej.vydejInstance.globalObject.service_vydej.Terminal_GetSSCCCode(MST_Global.VydejSequence, count, MST_Global.TerminalID).ToString();
                    _typOznaceni = new Fask.MST_W.Classes.Paleta(((Schema.TypyPalet.PaletyRow)cbTyp.SelectedItem).ID, ((Schema.TypyPalet.PaletyRow)cbTyp.SelectedItem).Name, tOznaceni.Text);
                    //Fask.MST_W.Classes.SSCC sscckod = Fask.MST_W.Classes.SSCC.Parse(ssccCode.ToString());
                    this.DialogResult = DialogResult.OK;
                    return;
                }

                //Verifikace  SSCC
                if (SSCC_Verifikace(tOznaceni.Text))
                {

                    Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable dt = new Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIDataTable();
                    Vydej.vydejInstance.globalObject.controller_vydej.FillBy_TOPjeden_NMBRPAL_SI(dt, tOznaceni.Text);

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

			ScannerFinalize();
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

        #region CHECK
        //private bool Check()
        //{
        //    //if (cbTyp.Text.Trim().Length == 0 && tOznaceni.Text.Trim().Length == 0)
        //    //{
        //    //    DialogResult dr = MessageBoxBig.Show("Není zadán typ a èíslo.\n\nChcete pokraèovat?", "", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning);
        //    //    return dr == DialogResult.Yes;
        //    //} 
        //    //else if (cbTyp.Text.Trim().Length == 0)
        //    //{
        //    //    MessageBoxBig.Show("Vypòte typ", Color.Red);
        //    //    return false;
        //    //}
        //    //else 

        //    if ((cbTyp.SelectedItem as Schema.TypyPalet.PaletyRow) == null)
        //    {
        //        //MessageBoxBig.Show(Fask.Localization.Localization.Vydej3TypOznaceniPaletyFormOldNeniVybranTypPalety, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
        //        //cbTyp.Focus();
        //        //return false;

        //        string cisloPaletyHledani = string.Empty;

        //        #region vypocetCislaPalety

        //        string sscc = tOznaceni.Text.Trim();
        //        try
        //        {
        //            Fask.MST_W.Classes.SSCC sscckod = Fask.MST_W.Classes.SSCC.Parse(sscc);
        //            if (sscc.IndexOf("00") == 0 && sscc.Length == MST_Global.VydejDelkaKoduPalety && CountParity(sscc.Substring(0, sscc.Length - 1)) == sscc[sscc.Length - 1].ToString())
        //            { //jedna se o platny sscc kod
        //                cisloPaletyHledani = sscc;
        //            }
        //        }
        //        catch
        //        { }
        //        //neni to platny sscc kod => doplnit podle zadani ...
        //        //upravit, otestovat zda cisloPaletyHledani je rovno string.Emtpy
        //        if (cisloPaletyHledani == string.Empty)
        //        {
        //            if (MST_Global.VydejPrefix.Length + tOznaceni.Text.Trim().Length + 2 > MST_Global.VydejDelkaKoduPalety)
        //            {
        //                //if (MST_Global.VydejPrefix.Length + tOznaceni.Text.Length + 1 > MST_Global.VydejDelkaKoduPalety)
        //                //{
        //                MessageBoxBig.Show("Zadané èíslo je vìtší než maximální délka kódu palety!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
        //                tOznaceni.SelectAll();
        //                return false;
        //            }
        //            else if (MST_Global.VydejPrefix.Length + tOznaceni.Text.Length < MST_Global.VydejDelkaKoduPalety)
        //            {
        //                string year = DateTime.Today.ToString("yy");
        //                year = year.Substring(year.Length - 1, 1);
        //                //pocet nul, ktere se maji generovat
        //                int pocetNul = MST_Global.VydejDelkaKoduPalety - MST_Global.VydejPrefix.Length - tOznaceni.Text.Trim().Length - 2;
        //                string nuly = string.Empty;

        //                for (int i = 0; i < pocetNul; i++) nuly += "0";
        //                string pom = string.Join(nuly, new string[] { MST_Global.VydejPrefix + year, tOznaceni.Text.Trim() });
        //                pom += CountParity(pom);
        //                cisloPaletyHledani = pom;
        //            }
        //        }

        //        #endregion

        //        System.Data.SqlServerCe.SqlCeDataAdapter sda = new System.Data.SqlServerCe.SqlCeDataAdapter(
        //            "Select * from czmst_si where nmbrpal='" + cisloPaletyHledani + "'",
        //            "Data source=" + filename
        //            );

        //        Fask.SQLiteDBs.DataSets.Vydej vydejData2 = new Fask.SQLiteDBs.DataSets.Vydej();
        //        sda.Fill(vydejData2, vydejData2.CZMST_SE.TableName);

        //        Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow[] founded = (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow[])vydejData2.CZMST_SI.Select();

        //        if (founded.Length > 0)
        //        {
        //            Schema.TypyPalet.PaletyRow paletar = typyPalet.Palety.FindByID(founded[0].TYPEPAL);
        //            cbTyp.SelectedItem = paletar;

        //        }
        //        else
        //        {
        //            MessageBoxBig.Show("Není vybrán typ palety", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
        //            cbTyp.Focus();
        //            return false;
        //        }
        //    }

        //    if (tOznaceni.Text.Trim().Length == 0)
        //    {
        //        MessageBoxBig.Show(Fask.Localization.Localization.Vydej3TypOznaceniPaletyFormOldVyplnteCislo, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
        //        tOznaceni.Focus();
        //        tOznaceni.SelectAll();
        //        return false;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            WebRefServer = new VydejService.VydejService();
        //            WebRefServer.Url = MST_Global.ServerAddress + "Vydej.asmx";
        //            WebRefServer.Timeout = MST_Global.ServiceTimeOut;

        //            //int ssccCode = WebRefServer.Terminal_GetSSCCCode();

        //            ////Fask.MST_W.Classes.SSCC sscckod = Fask.MST_W.Classes.SSCC.Parse(ssccCode.ToString());


        //            tOznaceni.Text = ssccCode.ToString();
        //            tOznaceni.Focus();
        //            tOznaceni.SelectAll();
        //            return false;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBoxBig.Show("Web: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
        //            tOznaceni.Focus();
        //            tOznaceni.SelectAll();
        //            return false;
        //        }
        //    }

        //    ulong cisloPalety = 1;
            
        //    try
        //    {
        //        cisloPalety = ulong.Parse(tOznaceni.Text);
        //    }
        //    catch
        //    {
        //        MessageBoxBig.Show(Fask.Localization.Localization.Vydej3TypOznaceniPaletyFormOldVkladejtePouzeCisla, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
        //        tOznaceni.Focus();
        //        tOznaceni.SelectAll();
        //        return false;
        //    }

        //    if (MST_Global.VydejDoplnitPrefix)
        //    {
        //        string sscc = tOznaceni.Text.Trim();
        //        //Test zda se jedna o sscc
        //        Fask.MST_W.Classes.SSCC sscckod = Fask.MST_W.Classes.SSCC.Parse(sscc);
        //        if (sscc.IndexOf("00") == 0 && sscc.Length == MST_Global.VydejDelkaKoduPalety && CountParity(sscc.Substring(0, sscc.Length-1)) == sscc[sscc.Length-1].ToString())
        //        { //jedna se o platny sscc kod
        //            MST_Global.VydejAktualniCisloPalety = sscc.Substring(0, sscc.Length - 1);
        //            return true;
        //        }

        //        //neni to platny sscc kod => doplnit podle zadani ...
        //        if (MST_Global.VydejPrefix.Length + tOznaceni.Text.Trim().Length + 1 > MST_Global.VydejDelkaKoduPalety)
        //        {
        //            MessageBoxBig.Show(Fask.Localization.Localization.Vydej3TypOznaceniPaletyFormOldCisloVetsiNezMaximum, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1, Color.Red);
        //            tOznaceni.SelectAll();
        //            return false;
        //        }
        //        else if(MST_Global.VydejPrefix.Length + tOznaceni.Text.Length < MST_Global.VydejDelkaKoduPalety)
        //        {

        //            ////pocet nul, ktere se maji generovat
        //            //int pocetNul = MST_Global.VydejDelkaKoduPalety - MST_Global.VydejPrefix.Length - tOznaceni.Text.Trim().Length - 1;
        //            //string nuly = string.Empty;
        //            //for (int i = 0; i < pocetNul; i++) nuly += "0";

        //            //string pom = string.Join(nuly, new string[] { MST_Global.VydejPrefix, tOznaceni.Text.Trim() });
        //            //pom += CountParity(pom);
        //            //tOznaceni.Text = pom;

        //            string year = DateTime.Today.ToString("yy");
        //            year = year.Substring(year.Length - 1, 1);
        //            //pocet nul, ktere se maji generovat
        //            int pocetNul = MST_Global.VydejDelkaKoduPalety - MST_Global.VydejPrefix.Length - tOznaceni.Text.Trim().Length - 2;
        //            string nuly = string.Empty;

        //            for (int i = 0; i < pocetNul; i++) nuly += "0";
        //            string pom = string.Join(nuly, new string[] { MST_Global.VydejPrefix + year, tOznaceni.Text.Trim() });
        //            MST_Global.VydejAktualniCisloPalety = pom;
        //            pom += CountParity(pom);
        //            tOznaceni.Text = pom;
        //        }

        //    }

        //    return true;
        //}

        #endregion

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

        private void ok_but_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cbTyp_TextChanged(object sender, EventArgs e)
        {
            if (!tOznaceni.Focused)
                tOznaceni.Focus();
            tOznaceni.SelectAll();
        }

        #region Scanner start stop
        private bool scannserstart = true;
        private void ScannerFinalize()
        {
			if (!_automat)
			{
				this.ScannerStop();
				this.scannserstart = false; 
			}
        }

        private void ScannerStart()
        {
			if (!_automat)
			{
				if (!scannserstart)
					return;

				Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
				Program.mstw.EnableScanner(); 
			}
        }

        private void ScannerStop()
        {
			if (!_automat)
			{
				Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
				Program.mstw.DisableScanner(); 
			}
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
            catch (Exception ex )
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

        private void TypOznaceniPaletyForm_Activated(object sender, EventArgs e)
        {
            // prepnuti na numerickou klavesnici
            Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
        }

        private void TypOznaceniPaletyForm_Closing(object sender, CancelEventArgs e)
        {
            ScannerFinalize();
        }



        #region TISK etikety

        //private void TisknoutAktualniPaletovyListek(string ssccCode)
        //{

        //    if (MessageBoxBig.Show("Chcete vytisknout aktuální paletový lístek?", "Info", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.No)
        //        return;

        //    tiskData = new Fask.MST_W.PrintServer.DB.CZMST_SIDataTable();

        //    string styp = ((Schema.TypyPalet.PaletyRow)cbTyp.SelectedItem).ID;


        //    System.Data.SqlServerCe.SqlCeDataAdapter sda = new System.Data.SqlServerCe.SqlCeDataAdapter(
        //        "Select * from czmst_se",
        //        "Data source=" + filename
        //        );

        //    Fask.SQLiteDBs.DataSets.Vydej data = new Fask.SQLiteDBs.DataSets.Vydej();
        //    sda.Fill(data, data.CZMST_SE.TableName);

        //    string sopnumbe = "1";
        //    if (data.CZMST_SE.Count > 0)
        //        sopnumbe = data.CZMST_SE[0].SOPNUMBE;

        //    tiskData.AddCZMST_SIRow(1, sopnumbe, "1", 1, "1", "", "", "", "", 1, 0, 0, "", "", "", "", "", "", "", MST_Global.UserID, styp,
        //               ssccCode, 0, 1, new Guid().ToByteArray());

        //    tiskData.AddCZMST_SIRow(1, sopnumbe, "2", 1, "2", "", "", "", "", 1, 0, 0, "", "", "", "", "", "", "", MST_Global.UserID, styp,
        //   ssccCode, 0, 2, new Guid().ToByteArray());


        //    try
        //    {
        //        WebRefPrintServer = new Fask.MST_W.PrintServer.Tisk();
        //        WebRefPrintServer.Url = MST_Global.PrintServerAddress + "Tisk.asmx";
        //        WebRefPrintServer.Timeout = MST_Global.ServiceTimeOut;

        //        if (WebRefPrintServer.Terminal_VytiskniPaletovyListek(tiskData, MST_Global.TerminalID))
        //            MessageBoxBig.Show("Paletový lístek byl úspìšnì vytisknut.", "Info", MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        //        else
        //            MessageBoxBig.Show("Chyba pøi tisku paletového lístku!", "Info", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);

        //        tiskData.Clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex, "ListCelePaletyForm.tiskPalListek()");
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //    }

        //    return;
        //}

        //private bool zmenaCislaPalety(string sscccode)
        //{
        //    Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow[] nalezeneRadky;


        //    System.Data.SqlServerCe.SqlCeDataAdapter sda = new System.Data.SqlServerCe.SqlCeDataAdapter(
        //        "Select * from czmst_si where nmbrpal='" + sscccode + "'",
        //        "Data source=" + filename
        //        );

        //    Fask.SQLiteDBs.DataSets.Vydej vydejData2 = new Fask.SQLiteDBs.DataSets.Vydej();
        //    sda.Fill(vydejData2, vydejData2.CZMST_SI.TableName);

        //    nalezeneRadky = (Fask.SQLiteDBs.DataSets.Vydej.CZMST_SIRow[])vydejData2.CZMST_SI.Select();

        //    if (nalezeneRadky.Length <= 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        if (MessageBoxBig.Show("Chcete vytisknout pøedchozí paletový lístek?", "Info", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.No)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            tiskData = new Fask.MST_W.PrintServer.DB.CZMST_SIDataTable();
        //            if (!nalezeneRadky[0].PRINTED)
        //            {
        //                for (int i = 0; i < nalezeneRadky.Length; i++)
        //                {
        //                    tiskData.AddCZMST_SIRow(
        //                        nalezeneRadky[i].CountEntries,
        //                        nalezeneRadky[i].SOPNUMBE,
        //                        nalezeneRadky[i].ITEMNMBR,
        //                        nalezeneRadky[i].ORD,
        //                        nalezeneRadky[i].VNDDOCNM,
        //                        nalezeneRadky[i].VNDITNUM,
        //                        nalezeneRadky[i].CZ_CarKod,
        //                        nalezeneRadky[i].LOCNCODE,
        //                        nalezeneRadky[i].MJ,
        //                        nalezeneRadky[i].QTYSHPPD,
        //                        nalezeneRadky[i].QTYSHPPDMJ,
        //                        nalezeneRadky[i].QTYPACK,
        //                        nalezeneRadky[i].SERLTNUM,
        //                        nalezeneRadky[i].KOD_SW,
        //                        nalezeneRadky[i].DAT_VYROBY,
        //                        nalezeneRadky[i].REZ_1,
        //                        nalezeneRadky[i].ODBER_ID,
        //                        nalezeneRadky[i].DATEDONE,
        //                        nalezeneRadky[i].TIMEDONE,
        //                        nalezeneRadky[i].USER_ID,
        //                        nalezeneRadky[i].TYPEPAL,
        //                        nalezeneRadky[i].NMBRPAL,
        //                        0, //false/*nalezeneRadky[i].PRINTED*/);
        //                        nalezeneRadky[i].DEX_ROW_ID,
        //                        nalezeneRadky[i].guid.ToByteArray()
        //                        );
        //                }

        //                tiskPalListek(tiskData, sscccode);
        //                return true;
        //            }
        //            else
        //            {
        //                if (MessageBoxBig.Show("Pøedchozí paletový lístek byl vytisknut, chcete jej vytisknout znova?", "Info", MessageBoxButtons.YesNo, MessageBoxBigIcon.Information) == DialogResult.No)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    for (int i = 0; i < nalezeneRadky.Length; i++)
        //                    {
        //                        //nalezeneRadky[i].PRINTED je vzdy false!
        //                        tiskData.AddCZMST_SIRow(
        //                            nalezeneRadky[i].CountEntries,
        //                            nalezeneRadky[i].SOPNUMBE,
        //                            nalezeneRadky[i].ITEMNMBR,
        //                            nalezeneRadky[i].ORD,
        //                            nalezeneRadky[i].VNDDOCNM,
        //                            nalezeneRadky[i].VNDITNUM,
        //                            nalezeneRadky[i].CZ_CarKod,
        //                            nalezeneRadky[i].LOCNCODE,
        //                            nalezeneRadky[i].MJ,
        //                            nalezeneRadky[i].QTYSHPPD,
        //                            nalezeneRadky[i].QTYSHPPDMJ,
        //                            nalezeneRadky[i].QTYPACK,
        //                            nalezeneRadky[i].SERLTNUM,
        //                            nalezeneRadky[i].KOD_SW,
        //                            nalezeneRadky[i].DAT_VYROBY,
        //                            nalezeneRadky[i].REZ_1,
        //                            nalezeneRadky[i].ODBER_ID,
        //                            nalezeneRadky[i].DATEDONE,
        //                            nalezeneRadky[i].TIMEDONE,
        //                            nalezeneRadky[i].USER_ID,
        //                            nalezeneRadky[i].TYPEPAL,
        //                            nalezeneRadky[i].NMBRPAL,
        //                            0, //false);
        //                            nalezeneRadky[i].DEX_ROW_ID,
        //                            nalezeneRadky[i].guid.ToByteArray()
        //                            );
        //                    }

        //                    tiskPalListek(tiskData, sscccode);

        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //}

        //private void tiskPalListek(Fask.MST_W.PrintServer.DB.CZMST_SIDataTable tiskDataSI, string oldSSCCcode)
        //{
        //    try
        //    {
        //        WebRefPrintServer = new Fask.MST_W.PrintServer.Tisk();
        //        WebRefPrintServer.Url = MST_Global.PrintServerAddress + "Tisk.asmx";
        //        WebRefPrintServer.Timeout = MST_Global.ServiceTimeOut;

        //        if (WebRefPrintServer.Terminal_VytiskniPaletovyListek(tiskDataSI, MST_Global.TerminalID))
        //        {
        //            MessageBoxBig.Show("Paletový lístek byl úspìšnì vytisknut.", "Info", MessageBoxButtons.OK, MessageBoxBigIcon.Information);

        //            /* smazano vydejData...neni toto potreba
        //            for (int i = 0; i < tabulkaSESI.CZMST_SI.Count; i++)
        //            {
        //                if (tabulkaSESI.CZMST_SI[i].NMBRPAL == oldSSCCcode)
        //                {
        //                    tabulkaSESI.CZMST_SI[i].PRINTED = true;
        //                    tabulkaSESI.AcceptChanges();
        //                }
        //            }
        //            */

        //            _ta_si.UpdatePrintedByNmbrlpal(true, oldSSCCcode);
        //        }
        //        else
        //        {
        //            /*  smazano vydejData...neni toto potreba
        //            for (int i = 0; i < tabulkaSESI.CZMST_SI.Count; i++)
        //            {
        //                if (tabulkaSESI.CZMST_SI[i].NMBRPAL == oldSSCCcode)
        //                {
        //                    tabulkaSESI.CZMST_SI[i].PRINTED = false;
        //                    tabulkaSESI.AcceptChanges();
        //                }
        //            }
        //            */
        //            _ta_si.UpdatePrintedByNmbrlpal(false, oldSSCCcode);
        //            MessageBoxBig.Show("Chyba pøi tisku paletového lístku!", "Info", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
        //        }

        //        //JiS - snad byse melo vyprazdnito, s cim pracuje metoda ?? (i pres to ze je to to stejne co promenna objektu...)
        //        tiskDataSI.Clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex, "ListCelePaletyForm.tiskPalListek()");
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //    }

        //    ScannerStop();
        //}

        #endregion
    }
}