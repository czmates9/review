using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fask.MST_W.Forms
{
    public partial class SejmiKodForm : System.Windows.Forms.Form
    {
        public enum TypeOfCode { Numeric, AlphaNumeric };
        private TypeOfCode typeOfCode;

        protected override void Finalize()
        {
            //base.Finalize();
        }

        public TypeOfCode CodeType
        {
            get { return this.typeOfCode; }
            set { 
                this.typeOfCode = value;
                switch (typeOfCode)
                {
                    case TypeOfCode.Numeric:
                        Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
                        break;
                    case TypeOfCode.AlphaNumeric:
                        Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Alpha);
                        break;
                    default:
                        Components.KeyboardManager.SetDefault();
                        break;
                }
            }
        }
        private bool povolitScanner = true;
        private decimal len;
        /// <summary>
        /// Delka vstupni hodnoty, ktera se bude kontrolovat
        /// </summary>
        public decimal Len
        {
            get { return len; }
            set { len = value; }
        }
        private bool checkLen;

        private bool scannerOff = false;
        /// <summary>
        /// Aktivni scanner
        /// </summary>
        public bool ScannerOff
        {
            get { return this.scannerOff; }
            set
            {
                this.scannerOff = value;
                // 27.10.2017 JiS -> toto je problematicke ... zapinani a vypinani scanneru pouze pri Load/PerformOK(Cancel,...)
                //ScannerStop();
                //ScannerStart();
            }
        }

        private bool _scannerCheckOnly = false;
        public bool ScannerCheckOnly
        {
            get { return _scannerCheckOnly; }
            set
            {
                _scannerCheckOnly = value;
                if (_scannerCheckOnly)
                {
                    this.kod_tb.ReadOnly = true;
                    this.ok_but.Enabled = false;
                    if (scannerOff) ScannerOff = false;
                }
                else
                {
                    this.kod_tb.ReadOnly = false;
                    this.ok_but.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Kontrolovat delku vstupni hodnoty
        /// </summary>
        public bool CheckLen
        {
            get { return this.checkLen; }
            set { this.checkLen = value; }
        }

        private int maxlength = 0;
        ///// <summary>
        ///// Maximalni delka vstupni hodnoty
        ///// </summary>
        //public int MaxLength
        //{
        //    get { return this.maxlength; }
        //    set { this.maxlength = value; }
        //}
        private bool allowEmpty;
        /// <summary>
        /// Povolit prazdny vstup
        /// </summary>
        public bool AllowEmpty
        {
            get { return this.allowEmpty; }
            set { this.allowEmpty = value; }
        }
        /// <summary>
        /// Popis dialogu - textova hodnota
        /// </summary>
        public string Popis
        {
            get { return this.popis_l.Text; }
            set { this.popis_l.Text = value; }
        }

        public SejmiKodForm()
            : this ("", TypeOfCode.AlphaNumeric)
        {
        }

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        public SejmiKodForm(string popis, TypeOfCode typeOfCode)
            : this (popis, typeOfCode, 0, false, false)
        {
        }

        /// <summary>
        /// Nastavi text na tlacitku Zpet
        /// </summary>
        public string ZpetButText
        {
            set
            {
                this.zpet_but.Text = value;
            }
        }

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        /// <param name="len">Delka kodu (0 - nekontrolovat)</param>
        /// <param name="checkLen">Kontrolovat delku kodu?</param>
        /// <param name="allowEmpty">Ma umoznit prazdny vstup?</param>
        public SejmiKodForm(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty)
            : this(popis, typeOfCode, len, checkLen, allowEmpty, "")
        {
        }

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        /// <param name="len">Delka kodu (0 - nekontrolovat)</param>
        /// <param name="checkLen">Kontrolovat delku kodu?</param>
        /// <param name="allowEmpty">Ma umoznit prazdny vstup?</param>
        /// <param name="retezecKPredvyplneni">Retezec, ktery se predvyplni do policka 'kod'</param>
        public SejmiKodForm(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni)
            : this(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni, 0)
        {
        }

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        /// <param name="len">Delka kodu (0 - nekontrolovat)</param>
        /// <param name="checkLen">Kontrolovat delku kodu?</param>
        /// <param name="allowEmpty">Ma umoznit prazdny vstup?</param>
        /// <param name="retezecKPredvyplneni">Retezec, ktery se predvyplni do policka 'kod'</param>
        public SejmiKodForm(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni, bool povolitScanner)
            : this(popis, typeOfCode, len, checkLen, allowEmpty, retezecKPredvyplneni, 0)
        {
            this.povolitScanner = povolitScanner;
        }

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        /// <param name="len">Delka kodu (0 - nekontrolovat)</param>
        /// <param name="checkLen">Kontrolovat delku kodu?</param>
        /// <param name="allowEmpty">Ma umoznit prazdny vstup?</param>
        /// <param name="retezecKPredvyplneni">Retezec, ktery se predvyplni do policka 'kod'</param>
        /// <param name="maxlength">Maximální povolená délka øetìzce</param>
        public SejmiKodForm(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni, int maxlength)
        {
            InitializeComponent();
            if (!String.IsNullOrEmpty(popis))
            {
                this.Text = popis;
                this.popis_l.Text = popis;
            }
            //Components.KeyboardManager.SaveDefaultKeyboardMode();
            //this.typeOfCode = typeOfCode;
            CodeType = typeOfCode;
            this.len = len;
            this.checkLen = checkLen;
            this.allowEmpty = allowEmpty;
            //this.maxlength = maxlength;
            this.KeyPreview = true;
            //this.kod_tb.Focus();
            //this.kod_tb.Text = retezecKPredvyplneni;
            //this.kod_tb.SelectAll();
            this.Kod = retezecKPredvyplneni;
        }

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="statusbar">Text zobrazen ve statusbaru</param>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        /// <param name="len">Delka kodu (0 - nekontrolovat)</param>
        /// <param name="checkLen">Kontrolovat delku kodu?</param>
        /// <param name="allowEmpty">Ma umoznit prazdny vstup?</param>
        /// <param name="retezecKPredvyplneni">Retezec, ktery se predvyplni do policka 'kod'</param>
        /// <param name="maxlength">Maximální povolená délka øetìzce</param>
        public SejmiKodForm(string statusbar, string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni, int maxlength)
        {
            InitializeComponent();
            if (!String.IsNullOrEmpty(statusbar))
                this.Text = statusbar;
            if (!String.IsNullOrEmpty(popis))
                this.popis_l.Text = popis;
            //Components.KeyboardManager.SaveDefaultKeyboardMode();
            //this.typeOfCode = typeOfCode;
            CodeType = typeOfCode;
            this.len = len;
            this.checkLen = checkLen;
            this.allowEmpty = allowEmpty;
            this.maxlength = maxlength;
            this.KeyPreview = true;
            //this.kod_tb.Focus();
            //this.kod_tb.Text = retezecKPredvyplneni;
            //this.kod_tb.SelectAll();
            this.Kod = retezecKPredvyplneni;
        }

        delegate void UpdateUIDelegate(string barcode);
        protected virtual void UpdateUI(string barcode)
        {
            if (_scannerCheckOnly)
            {
                if (this._kodPrednastaveny != barcode)
                {
                    MessageBoxBig.Show("Pøednastavený kód '" + _kodPrednastaveny + "' neodpovídá naètenému kódu '" + barcode + "' !!!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
            }

            this.kod_tb.Text = barcode;
            if (isRightCode())
                PerformOK();
            else
                kod_tb.SelectAll();
            if (MST_Global.OnScannerSound_Forms)
            {
                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            }
        }

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length > 0)
                this.BeginInvoke(new UpdateUIDelegate(UpdateUI), new object[] { e.BarcodeData.Trim() });
        }

        private string _kodPrednastaveny = string.Empty;
        /// <summary>
        /// Hodnota vstupniho pole
        /// </summary>
        public string Kod
        {
            get
            {
                return kod_tb.Text.Trim();
            }
            set
            {
                kod_tb.Text = value.Trim();
                _kodPrednastaveny = kod_tb.Text;
                //KodFocus();
            }
        }

        // JiS : Task #46 - DP : zamezit moznost zmeny mnozstvi v dialogu
        /// <summary>
        /// Nastavi priznak na readonly pro pole Kod
        /// </summary>
        /// <param name="readOnly">bude jen readonly?</param>
        public void KodReadOnly(bool readOnly)
        {
            this.kod_tb.ReadOnly = readOnly;
        }

        private void KodFocus()
        {
            kod_tb.Focus();
            kod_tb.SelectAll();
        }

        //private decimal _nactenoMnozstvi = 0;
        //public decimal NactenoMnozstvi
        //{
        //    get
        //    {
        //        return _nactenoMnozstvi;
        //    }
        //    set
        //    {
        //        _nactenoMnozstvi = value;
        //    }
        //}

        //private decimal _celkovePozadovaneMnozstvi = 0;
        //public decimal CelkovePozadovaneMnozstvi
        //{
        //    get
        //    {
        //        return _celkovePozadovaneMnozstvi;
        //    }
        //    set
        //    {
        //        _celkovePozadovaneMnozstvi = value;
        //    }
        //}

        virtual protected bool isRightCode()
        {
            if (!allowEmpty && this.kod_tb.Text.Trim().Length == 0)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.FormsSejmiKodFormVlozteKod, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                return false;
            }
            else if (this.kod_tb.Text.Trim().Length == 0)
            {
                // TODO : upozorneni ze je prazdne a zda pokracovat 
                DialogResult drEmpty = MessageBoxBig.Show("Není zadána hodnota.\nPokraèovat?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning, false);
                if (drEmpty == DialogResult.No)
                    return false;
            }

            if (typeOfCode == TypeOfCode.Numeric)
            {
                decimal qty = 0;
                try
                {
                    qty = decimal.Parse(this.kod_tb.Text);
                }
                catch
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.FormsSejmiKodFormSmiteZadavatPouzeCisla, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                    return false;
                }

                if (qty < -999999999 || 999999999 < qty)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.FormsSejmiKodFormCisloJeMimoRozsah, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                    return false;
                }
            }

            if (maxlength > 0)
            {
                if (this.kod_tb.Text.Trim().Length > maxlength)
                {
                    MessageBoxBig.Show(string.Format(Fask.Localization.Localization.FormsSejmiKodFormKodJeMensiNezX, maxlength), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                    return false;
                }
            }

            if ((len > 0) && checkLen)
            {
                if (this.kod_tb.Text.Trim().Length > len)
                {
                    if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.FormsSejmiKodFormKodJeDelsiNezXUlozitDotaz, len), Fask.Localization.Localization.FormsSejmiKodFormDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button2, Color.Red)
                        == DialogResult.No)
                        return false;
                }
                else if (this.kod_tb.Text.Trim().Length < len)
                {
                    if (MessageBoxBig.Show(string.Format(Fask.Localization.Localization.FormsSejmiKodFormKodJeKratsiNezXUlozitDotaz, len), Fask.Localization.Localization.FormsSejmiKodFormDotaz, MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button2, Color.Red)
                        == DialogResult.No)
                        return false;
                }
            }

            return true;
        }

        private void SejmiKodForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;
            e.Handled = true;

            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {// stiskl Enter, provede kontrolu kodu
                this.ok_but_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
                return;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void SejmiKodForm_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

            if(povolitScanner)
                ScannerStart();
            
            panelButtons_Resize(null, null);

            // nastaveni focusu na zadani kodu ...
            KodFocus();
        }

        //protected void ScannerStart()
        //{
        //    if (_scannerCheckOnly || !scannerOff)
        //    {
        //        try
        //        {
        //            //Program.mstw.Scanner.DataReady += new USICF.USIClass.USIEventHandler(Scanner_DataReady);
        //            Program.mstw.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
        //            Program.mstw.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
        //        }
        //        catch { }

        //        try
        //        {
        //            Program.mstw.EnableScanner();
        //        }
        //        catch { }
        //    }
        //}

        //private bool scannserstart = true;
        public void ScannerFinalize()
        {
            this.ScannerStop();
            //this.scannserstart = false;
        }
        // Jedna se o vice použivany dialog tak nemužeme zakazat start skeneru
        public void ScannerStart()
        {
            if (_scannerCheckOnly || !scannerOff)
            {
                //if (!scannserstart)
                //    return;

                Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
                Program.mstw.EnableScanner();
            }
        }

        public void ScannerStop()
        {

                Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
                Program.mstw.DisableScanner();
        }
     
        private void SejmiKodForm_Closing(object sender, CancelEventArgs e)
        {
            if(povolitScanner)
                ScannerStop();
            //Components.KeyboardManager.LoadDefaultKeyboardMode();
        }

        private void ok_but_Click(object sender, EventArgs e)
        {
            if (_scannerCheckOnly) //neumozni potvrzeni enterem...
                return;

            if (isRightCode())
                PerformOK();
            else
                kod_tb.SelectAll();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        protected virtual void PerformOK()
        {
            //if (povolitScanner)
            //    this.ScannerStop();
            finalize();
            this.DialogResult = DialogResult.OK;
        }

        protected virtual void PerformCancel()
        {
            //if (povolitScanner)
            //    this.ScannerStop();
            finalize();
            this.DialogResult = DialogResult.Cancel;
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = nsize;
        }

        public virtual void SetDefaultValues()
        {
            this.kod_tb.Text = string.Empty;
            this.kod_tb.ReadOnly = false;       // JiS : Task #46 - DP : zamezit moznost zmeny mnozstvi v dialogu
            this.allowEmpty = false;
            this.checkLen = false;
            this.len = 0;
            this.maxlength = 0;
            this.typeOfCode = TypeOfCode.AlphaNumeric;
            this.ScannerOff = false;
            this.ScannerCheckOnly = false;
        }

        protected virtual void finalize()
        {
            ScannerStop();
        }

        private void SejmiKodForm_Activated(object sender, EventArgs e)
        {
            switch (typeOfCode)
            {
                case TypeOfCode.Numeric:
                    Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
                    break;
                case TypeOfCode.AlphaNumeric:
                    Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Alpha);
                    break;
                default:
                    Components.KeyboardManager.SetDefault();
                    break;
            }
        }

    }
}