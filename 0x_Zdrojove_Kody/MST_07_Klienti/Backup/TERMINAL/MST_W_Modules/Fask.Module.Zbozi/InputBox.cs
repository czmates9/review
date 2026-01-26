using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.Module.Zbozi
{
    public partial class InputBox : System.Windows.Forms.Form
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

        protected InputBox()
        {
            InitializeComponent();
        }

        private static Point midPoint = new Point(120, 147);

        private static Point GetFormLocation(Size s)
        {
            return new Point(midPoint.X - s.Width / 2, midPoint.Y - s.Height / 2);
        }

        protected InputBox(string caption, string defaultvalue)
            : this(caption, defaultvalue, false)
        {
            this.Text = caption;
            //this.textBox1.Text = defaultvalue;
            this.Kod = defaultvalue;
        }

        protected InputBox(string caption, string defaultvalue, bool enableScanner)
            : this()
        {
            this.Text = caption;
            //this.textBox1.Text = defaultvalue;
            this.Kod = defaultvalue;
            this._enableScanner = enableScanner;
        }

        public static DialogResult Show(string caption, string defaultvalue, out string value)
        {
            using (InputBox myInputBox = new InputBox(caption, defaultvalue))
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

        public static DialogResult Show(string caption, string defaultvalue, out string value, bool enableScanner)
        {
            using (InputBox myInputBox = new InputBox(caption, defaultvalue, enableScanner))
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
            ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            ScannerStop();
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
            //this.Location = Forms.FormLocation.GetFormLocation(this.Size);
            if (this._enableScanner) ScannerStart();
            //this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.Location = GetFormLocation(this.Size);

            //nastaveni kursoru a vyberu na textbox1 (kod)
            this.textBox1.SelectAll();
            this.textBox1.Focus();
        }

        private void ScannerStart()
        {
            if (Globals.Scanner != null)
            {
                //Program.mstw.Scanner.DataReady += new USICF.USIClass.USIEventHandler(Scanner_DataReady);
                //Program.mstw.EnableScanner();
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.Enable();
            }
        }

        private void ScannerStop()
        {
            if (Globals.Scanner != null)
            {
                //Program.mstw.Scanner.DataReady -= new USICF.USIClass.USIEventHandler(Scanner_DataReady);
                //Program.mstw.DisableScanner();
                Globals.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
                Globals.Scanner.Disable();
            }
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
                this.Kod = barcode;
                this.PerformOK();
            }
        }
    }
}