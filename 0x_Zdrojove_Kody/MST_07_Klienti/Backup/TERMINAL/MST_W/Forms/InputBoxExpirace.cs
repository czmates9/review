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
    public partial class InputBoxExpirace : System.Windows.Forms.Form
    {
        private bool _enableScanner = false;

        new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                this.labelText.Text = value;
            }
        }

        public string Kod
        {
            get { return this.textBox1.Text; }
            set
            {
                this.textBox1.Text = String.IsNullOrEmpty(value) ? string.Empty : value;
                this.textBox1.SelectAll();
                this.textBox1.Focus();
            }
        }

        protected InputBoxExpirace()
        {
            InitializeComponent();

            this.Location = MySystem.FormMidLocation.GetFormLocation(this.Size);
        }

        protected InputBoxExpirace(string caption, string defaultvalue)
            : this(caption, defaultvalue, false)
        {
            this.Text = caption;
            //this.textBox1.Text = defaultvalue;
            this.Kod = defaultvalue;
        }

        protected InputBoxExpirace(string caption, string defaultvalue, bool enableScanner)
            : this()
        {
            this.Text = caption;
            //this.textBox1.Text = defaultvalue;
            this.Kod = defaultvalue;
            this._enableScanner = enableScanner;
        }

        public static DialogResult Show(string caption, string defaultvalue, out string value)
        {
            using (var myInputBox = new InputBoxExpirace(caption, defaultvalue))
            {
                value = string.Empty;
                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //value = myInputBox.textBox1.Text;
                    value = myInputBox.Kod;
                }
                return dr;
            }
        }

        public static DialogResult Show(string caption, string defaultvalue, out string value, Components.KeyboardManager.KeyboardMode keyboardMode)
        {
            using (var myInputBox = new InputBoxExpirace(caption, defaultvalue))
            {
                value = string.Empty;
                Components.KeyboardManager.Switch(keyboardMode);
                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //value = myInputBox.textBox1.Text;
                    value = myInputBox.Kod;
                }
                return dr;
            }
        }

        public static DialogResult Show(string caption, string defaultvalue, out string value, bool enableScanner)
        {
            using (var myInputBox = new InputBoxExpirace(caption, defaultvalue, enableScanner))
            {
                value = string.Empty;
                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //value = myInputBox.textBox1.Text;
                    value = myInputBox.Kod;
                }
                return dr;
            }
        }

        public static DialogResult Show(string caption, string defaultvalue, out string value, bool enableScanner, Components.KeyboardManager.KeyboardMode keyboardMode)
        {
            using (var myInputBox = new InputBoxExpirace(caption, defaultvalue, enableScanner))
            {
                value = string.Empty;
                Components.KeyboardManager.Switch(keyboardMode);
                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //value = myInputBox.textBox1.Text;
                    value = myInputBox.Kod;
                }
                return dr;
            }
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformStorno();
            }
            else
            {
                return;
            }

            e.Handled = true;
        }

        private void PerformStorno()
        {
            ScannerFinalize();
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            ScannerFinalize();
            DialogResult = DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            PerformStorno();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace ze souboru
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.Location = Forms.FormLocation.GetFormLocation(this.Size);
            if (this._enableScanner) ScannerStart();
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;

            //nastaveni kursoru a vyberu na textbox1 (kod)
            this.textBox1.SelectAll();
            this.textBox1.Focus();
        }

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

        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        }

        private delegate void ScannerEventMethodDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        private void ScannerEventMethod(Fask.ScannerProvider.ScannerEventArgs e)
        {
            string barcode = e.BarcodeData.Trim();
            if (barcode != string.Empty)
            {
                //this.textBox1.Text = barcode;

                // parsing
                var code = Parsing.ParsingFactory.Parse(barcode, Settings.Parsing_Config);
                if (code is Fask.Parsing.Codes.Interfaces.ICodeExpiration)
                {
                    var expiration = (code as Fask.Parsing.Codes.Interfaces.ICodeExpiration).Expiration;
                    barcode = expiration.HasValue ? expiration.Value.ToString(Main.dateFormatRRMMDD) : string.Empty;
                }

                this.Kod = barcode;
                this.PerformOK();
            }
            //22.3.2017 Ta.D. neni moznost povolit nebo zakazat v congfig
            //if (MST_Global.OnScannerSound_Forms)
            //{
            //    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            //}
        }
    }
}