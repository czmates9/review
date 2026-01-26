using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Fask.MST_W.Forms;

namespace Fask.MST_W.Prijem_4
{
    public partial class PrijemZadejLokaci : System.Windows.Forms.Form
    {
        //private Fask.MST_W._WebRefernces_Globals.LokaceServiceSession wsLokace = null;
        public enum TypeOfCode { Numeric, AlphaNumeric };
        private TypeOfCode typeOfCode;

        protected override void Finalize()
        {
            //base.Finalize();
        }

        public Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow perow
        {
            get;
            set;
        }

        public TypeOfCode CodeType
        {
            get { return this.typeOfCode; }
            set { 
                this.typeOfCode = value;
                //switch (typeOfCode)
                //{
                //    case TypeOfCode.Numeric:
                //        Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
                //        break;
                //    case TypeOfCode.AlphaNumeric:
                //        Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Alpha);
                //        break;
                //    default:
                //        Components.KeyboardManager.SetDefault();
                //        break;
                //}
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
                //ScannerStop();
                //ScannerFinalize();
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

        public PrijemZadejLokaci()
            : this ("", TypeOfCode.AlphaNumeric)
        {
        }

        public PrijemZadejLokaci(Fask.SQLiteDBs.DataSets.Prijem.CZMST_PERow perow)
            : this("", TypeOfCode.AlphaNumeric)
        {
            this.perow = perow;
        }

        /// <summary>
        /// Vyzve k vlozeni kodu z klavesnice, nebo scannerem
        /// </summary>
        /// <param name="popis">Vyzva k sejmuti kodu</param>
        /// <param name="typeOfCode">Typ kodu</param>
        public PrijemZadejLokaci(string popis, TypeOfCode typeOfCode)
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
        public PrijemZadejLokaci(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty)
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
        public PrijemZadejLokaci(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni)
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
        public PrijemZadejLokaci(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni, bool povolitScanner)
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
        public PrijemZadejLokaci(string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni, int maxlength)
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
        public PrijemZadejLokaci(string statusbar, string popis, TypeOfCode typeOfCode, decimal len, bool checkLen, bool allowEmpty, string retezecKPredvyplneni, int maxlength)
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
                if (MST_Global.OnScannerSound_Prijem_4)
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
                //KodFocus();
            }
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

        private bool isRightCode()
        {
            if (!allowEmpty && this.kod_tb.Text.Trim().Length == 0)
            {
                MessageBoxBig.Show(Fask.Localization.Localization.FormsSejmiKodFormVlozteKod, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
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

            // TODO: online kontrola , zdali je lokace nastavena v LokaceSortiment ...
            if (Prijem_4.Globals.DoplneniVychoziLokacePovolit && perow != null)
            {
                bool? exists = OnlineVariantySortimentExistsDefault(perow.IsITEMNMBRNull() ? string.Empty : perow.ITEMNMBR, perow.IsSKL_IDNull() ? string.Empty : perow.SKL_ID, this.kod_tb.Text.Trim());

                // zbozi neexistuje v tabulce lokaci k sortimentu ... dotaz na pridani
                if (exists.HasValue && !exists.Value)
                {
                    DialogResult dr = MessageBoxBig.Show("Lokace '" + this.kod_tb.Text.Trim() + "' nebyla nalezena v tabulce lokací k sortimentu." +
                        "\nChcete ji nastavit?",
                        this.Text,
                        MessageBoxButtons.YesNo,
                        MessageBoxBigIcon.Question);

                    if (dr != DialogResult.No)
                    {
                        // vyber typu lokace z ciselniku ...
                        // TODO: dodelat, najit vychozi lokaci, pripadne zobrazit ciselnik filtrovany na vychozi lokaci
                        // JIS: napevno zde nastaveno, ze se maji filtrovat "Vychozi" lokace

                        string type = string.Empty;
                        using (FormLokaceTypVyber fltv = new FormLokaceTypVyber())
                        {
                            fltv.LokaceTypeISDefault = true; // filtr na vychozi pozice ...
                            if (DialogResult.Cancel == fltv.ShowDialog())
                                return false;

                            type = fltv.LokaceType.TYPE.Trim();
                        }

                        // nastaveni lokace
                        while (true)
                        {
                            exists = OnlineVariantySortimentNastav(perow.IsITEMNMBRNull() ? string.Empty : perow.ITEMNMBR, perow.IsSKL_IDNull() ? string.Empty : perow.SKL_ID, this.kod_tb.Text.Trim(), type);
                            // pokud je null nebo se vratilo false
                            if (!exists.HasValue || !exists.Value)
                            {
                                dr = MessageBoxBig.Show("Nepodaøilo se nastavit lokaci k sortimentu",
                                    this.Text,
                                    MessageBoxButtons.AbortRetryIgnore,
                                    MessageBoxBigIcon.Warning);

                                // ignorovat ... pokracovat dal
                                if (dr == DialogResult.Ignore)
                                    break;
                                // abort ... zrusit
                                if (dr == DialogResult.Abort)
                                    return false;
                                // jinak opakovat
                                else
                                    continue;
                            }
                            else  // vse proslo, ukoncit ...
                                break;
                        }

                    }
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

        private void PrijemZadejLokaci_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                this.Size = Forms.FormLocation.ScreenResolution;

                //wsLokace = new Fask.MST_W._WebRefernces_Globals.LokaceServiceSession();
                //wsLokace.Url = MST_Global.ServerAddress + "Lokace.asmx";
                //wsLokace.Timeout = MST_Global.ServiceTimeOut;
                //wsLokace.UpdateWebServiceCredentials();

                if (povolitScanner)
                    ScannerStart();

                panelButtons_Resize(null, null);

                // nastaveni focusu na zadani kodu ...
                KodFocus();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex.Message);
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
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
        private void ScannerFinalize()
        {
            this.ScannerStop();
            //this.scannserstart = false;
        }
       //Jedna se o vicekrat použivany sklad tak proto se nemuže zakazat znovukonstuovani
        private void ScannerStart()
        {
            //if (!scannserstart)
            //    return;

            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
        }

  
        private void PrijemZadejLokaci_Closing(object sender, CancelEventArgs e)
        {
            //if(povolitScanner)
            ScannerFinalize();
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
            this.ScannerFinalize();
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

        private bool? OnlineVariantySortimentExistsDefault(string itemnmbr, string skl_id, string locncode)
        {
            Fask.MST_W.PrijemService.Obecne ds = new Fask.MST_W.PrijemService.Obecne();
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //return wsLokace.VariantySortimentExistsDefault(MST_Global.TerminalID, MST_Global.UserID, itemnmbr, skl_id, locncode);
                return Prijem_4.PrijemMain.prijemInstance.globalObject.servis_lokace.VariantySortimentExistsDefault(MST_Global.TerminalID, MST_Global.UserID, itemnmbr, skl_id, locncode);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex.Message, "Prijem4.PrijemZadejLokaci.OnlineVariantySortimentExists");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool? OnlineVariantySortimentNastav(string itemnmbr, string skl_id, string locncode, string type)
        {
            Fask.MST_W.PrijemService.Obecne ds = new Fask.MST_W.PrijemService.Obecne();
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //return wsLokace.VariantySortimentNastav(MST_Global.TerminalID, MST_Global.UserID, itemnmbr, skl_id, locncode, type);
                return Prijem_4.PrijemMain.prijemInstance.globalObject.servis_lokace.VariantySortimentNastav(MST_Global.TerminalID, MST_Global.UserID, itemnmbr, skl_id, locncode, type);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Logging.Log.Write(ex.Message, "Prijem4.PrijemZadejLokaci.OnlineVariantySortimentExists");
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                return null;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}