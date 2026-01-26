using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.MST_W.Forms
{
    public partial class DateInputBox : System.Windows.Forms.Form
    {
        new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                this.labelText.Text = value;
            }
        }

        public DateTime Datum
        {
            get { return this.dateTimePicker1.Value; }
            set
            {
                this.dateTimePicker1.Value = value;
                this.dateTimePicker1.Focus();
            }
        }

        protected DateInputBox()
        {
            InitializeComponent();

            this.Location = MySystem.FormMidLocation.GetFormLocation(this.Size);
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.Size = Forms.FormLocation.ScreenResolution;

        }

        protected DateInputBox(string caption, DateTime defaultvalue)
            : this()
        {
            this.Text = caption;
            //this.textBox1.Text = defaultvalue;
            this.Datum = defaultvalue;
        }

        public static DialogResult Show(string caption, DateTime defaultvalue, out DateTime value)
        {
            using (DateInputBox myInputBox = new DateInputBox(caption, defaultvalue))
            {                
                value = defaultvalue;
                DialogResult dr = myInputBox.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //value = myInputBox.textBox1.Text;
                    value = myInputBox.Datum;
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

        private void finalize()
        {
        }

        private void PerformStorno()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        private void PerformOK()
        {
            finalize();
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
            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;

            this.dateTimePicker1.Focus();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nSize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonOK.Size = nSize;
        }

        private void DateInputBox_Activated(object sender, EventArgs e)
        {
            // prepnuti na numerickou klavesnici
            Components.KeyboardManager.Switch(Fask.MST_W.Components.KeyboardManager.KeyboardMode.Numeric);
        }
    }
}