using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.Vyroba_W.Forms
{
    public partial class DateTimeInputBox : System.Windows.Forms.Form
    {
        new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
            }
        }

        public DateTime Datum
        {
            get
            {
                return new DateTime(
                    this.dateTimePicker1.Value.Year, this.dateTimePicker1.Value.Month, this.dateTimePicker1.Value.Day,
                    this.dateTimePicker2.Value.Hour, this.dateTimePicker2.Value.Minute, 0
                    );
            }
            set
            {
                this.dateTimePicker1.Value = value;
                this.dateTimePicker2.Value = value;
                this.dateTimePicker2.Focus();
            }
        }

        protected DateTimeInputBox()
        {
            InitializeComponent();
        }

        protected DateTimeInputBox(string caption, DateTime defaultvalue)
            : this()
        {
            this.Text = caption;
            //this.textBox1.Text = defaultvalue;
            this.Datum = defaultvalue;
        }

        public static DialogResult Show(string caption, DateTime defaultvalue, out DateTime value)
        {
            using (DateTimeInputBox myInputBox = new DateTimeInputBox(caption, defaultvalue))
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
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            this.dateTimePicker2.Focus();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            Size nSize = new Size(panel1.Width / 2, panel1.Height);
            buttonOK.Size = nSize;
        }
    }
}