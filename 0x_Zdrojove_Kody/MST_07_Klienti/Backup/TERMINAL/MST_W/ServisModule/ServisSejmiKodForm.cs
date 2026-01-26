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
    public partial class ServisSejmiKodForm : System.Windows.Forms.Form
    {
        public enum TypeOfCode { Numeric, AlphaNumeric };
        private TypeOfCode typeOfCode;

        public TypeOfCode CodeType
        {
            get { return this.typeOfCode; }
            set 
            {
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

        private bool checkRequiredLen;
        private decimal requiredLen;
        /// <summary>
        /// Delka vstupni hodnoty, ktera se bude kontrolovat (!!presna delka!!)
        /// </summary>
        public decimal RequiredLen
        {
            get { return requiredLen; }
            set { requiredLen = value; }
        }

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
                ScannerStop();
                ScannerStart();
            }
        }

        /// <summary>
        /// Text, který se má zobrazit ve status baru
        /// </summary>
        public string StatusBarInfoText { get; set; }

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
        /// <summary>
        /// Maximalni delka vstupni hodnoty
        /// </summary>
        public int MaxLength
        {
            get { return this.maxlength; }
            set { this.maxlength = value; }
        }
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

        public ServisSejmiKodForm()
            : this ("", TypeOfCode.AlphaNumeric)
        {
        }

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        public ServisSejmiKodForm(string popis, TypeOfCode typeOfCode)
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
        public ServisSejmiKodForm(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty)
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
        public ServisSejmiKodForm(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni)
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
        public ServisSejmiKodForm(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni, bool povolitScanner)
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
        public ServisSejmiKodForm(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni, int maxlength)
        {
            InitializeComponent();
            if (popis != null)
                this.Text = popis;
            this.popis_l.Text = popis;
            Components.KeyboardManager.SaveDefaultKeyboardMode();
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
        public ServisSejmiKodForm(string statusbar, string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni, int maxlength)
        {
            InitializeComponent();
            if (statusbar != null)
                this.Text = statusbar;
            this.popis_l.Text = popis;
            Components.KeyboardManager.SaveDefaultKeyboardMode();
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
        /// <param name="checkRequiredLen">Kontrolovat na pøesnou délku øetìzce?</param>
        /// <param name="requiredLen">Pøesná délka øetìzce pro kontrolu.</param>
        public ServisSejmiKodForm(string statusbar, string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni, int maxlength, bool checkRequiredLen, int requiredLen)
        {
            InitializeComponent();
            if (statusbar != null)
                this.Text = statusbar;
            this.popis_l.Text = popis;
            Components.KeyboardManager.SaveDefaultKeyboardMode();
            //this.typeOfCode = typeOfCode;
            CodeType = typeOfCode;
            this.len = len;
            this.checkLen = checkLen;
            this.allowEmpty = allowEmpty;
            this.maxlength = maxlength;
            this.KeyPreview = true;
            //this.kod_tb.Focus();
            this.kod_tb.Text = retezecKPredvyplneni;
            //this.kod_tb.SelectAll();
            this.checkRequiredLen = checkRequiredLen;
            this.requiredLen = requiredLen;
            this.Kod = retezecKPredvyplneni;
        }

        delegate void UpdateUIDelegate(string barcode);
        protected virtual void UpdateUI(string barcode)
        {
            try
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
            }
            catch { }
            finally
            {
                if (MST_Global.OnScannerSound_ServisModul)
                {
                    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                }
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
                kod_tb.Focus();
                kod_tb.SelectAll();
            }
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

        private bool isRightCode()
        {
            // je povoleno zadavat prazdne hodnoty a pocet znaku je 0
            if (allowEmpty && this.kod_tb.Text.Trim().Length == 0)
            {
                if (MST_Global.ServisPrehratZvukPoVyberuMoznosti)
                    MySystem.Audio.PlaySound(System.IO.Path.Combine(Main.SoundDir, "notify.wav"));

                return true;
            }

            if (!allowEmpty && this.kod_tb.Text.Trim().Length == 0)
            {
                MessageBoxBig.Show("Vložte kód!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
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
                    MessageBoxBig.Show("Smíte vkládat pouze èísla!", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                    return false;
                }

                if (qty < -999999999 || 999999999 < qty)
                {
                    MessageBoxBig.Show("Èíslo je mimo povolený rozsah (-999999999,999999999)", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                    return false;
                }
            }

            if (maxlength > 0)
            {
                if (this.kod_tb.Text.Trim().Length > maxlength)
                {
                    MessageBoxBig.Show("Kód je delší než " + maxlength + " znakù", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                    return false;
                }
            }

            if ((len > 0) && checkLen)
            {
                if (this.kod_tb.Text.Trim().Length > len)
                {
                    if (MessageBoxBig.Show("Sejmutý kód je delší než " + len + " znakù. Chcete ho uložit?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button2, Color.Red)
                        == DialogResult.No)
                        return false;
                }
                else if (this.kod_tb.Text.Trim().Length < len)
                {
                    if (MessageBoxBig.Show("Sejmutý kód je kratší než " + len + " znakù. Chcete ho uložit?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button2, Color.Red)
                        == DialogResult.No)
                        return false;
                }
            }

            // checkRequiredLen
            if ((requiredLen > 0) && checkRequiredLen)
            {
                if (this.kod_tb.Text.Trim().Length > requiredLen)
                {
                    MessageBoxBig.Show("Zadaný kód je delší než " + requiredLen + " znakù.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button2, Color.Red);
                    return false;
                }
                else if (this.kod_tb.Text.Trim().Length < requiredLen)
                {
                    MessageBoxBig.Show("Zadaný kód je kratší než " + requiredLen + " znakù.", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button2, Color.Red);
                    return false;
                }
            }
            
            if (MST_Global.ServisPrehratZvukPoVyberuMoznosti)
                MySystem.Audio.PlaySound(System.IO.Path.Combine(Main.SoundDir, "notify.wav"));

            return true;
        }

        private void ServisSejmiKodForm_KeyDown(object sender, KeyEventArgs e)
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

        private void ServisSejmiKodForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;
            if(povolitScanner)
                ScannerStart();
            panelButtons_Resize(null, null);
            statusBarInfo.Text = StatusBarInfoText != null ? StatusBarInfoText : string.Empty;            
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

        private bool scannserstart = true;
        private void ScannerFinalize()
        {
            this.ScannerStop();
            this.scannserstart = false;
        }

        private void ScannerStart()
        {

            if (_scannerCheckOnly || !scannerOff)
            {

                if (!scannserstart)
                    return;



                Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
                Program.mstw.EnableScanner();
            }
        }

        private void ScannerStop()
        {
            if (_scannerCheckOnly || !scannerOff)
            {
                Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
                Program.mstw.DisableScanner();
            }
        }


        private void ServisSejmiKodForm_Closing(object sender, CancelEventArgs e)
        {
            //if(povolitScanner)
            ScannerFinalize();
            Components.KeyboardManager.LoadDefaultKeyboardMode();
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
            finalize();
            //if (povolitScanner)
            //    this.ScannerStop();
            this.DialogResult = DialogResult.OK;
        }

        protected virtual void PerformCancel()
        {
            finalize();
            //if (povolitScanner)
            //    this.ScannerStop();
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
            //if (povolitScanner)
            this.ScannerFinalize();
        }

        private void graphicButton1_Click(object sender, EventArgs e)
        {
            finalize();
            //if (povolitScanner)
            //    this.ScannerStop();
            this.DialogResult = DialogResult.Retry;
        }

        private void menuItemOK_Click(object sender, EventArgs e)
        {

        }

        private void menuItem2_Click(object sender, EventArgs e)
        {

        }

        private void menuItemCancel_Click(object sender, EventArgs e)
        {

        }

        private void krokzpet_but_Click(object sender, EventArgs e)
        {
            if(DialogResult.No == MessageBoxBig.Show("Opravdu se chcete vrátit o krok zpìt?", this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information))
                return;
            
            finalize();
            //if (povolitScanner)
            //    this.ScannerStop();
            this.DialogResult = DialogResult.Retry;
        }

    }
}