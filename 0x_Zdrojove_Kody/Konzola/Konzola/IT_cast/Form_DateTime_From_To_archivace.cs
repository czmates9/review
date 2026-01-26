using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Konzola.IT_cast
{
    public partial class Form_DateTime_From_To_archivace : Form
    {
        public string Popis
        {
            set { this.Text = value; }
            get { return this.Text; }
        }

        public DateTime? OD
        {
            get 
            {
                if (dtp_OD.Checked)
                {
                    return dtp_OD.Value;
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime? DO
        {
            get
            {
                if (dtp_DO.Checked)
                {
                    return dtp_DO.Value;
                }
                else
                {
                    return null;
                }
            }
        }


        public Form_DateTime_From_To_archivace()
        {
            InitializeComponent();

            dtp_DO.Format = DateTimePickerFormat.Custom;
            dtp_DO.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            dtp_OD.Format = DateTimePickerFormat.Custom;
            dtp_OD.CustomFormat = "dd/MM/yyyy HH:mm:ss";


        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            Size s = new Size( panel1.Width / 2 ,panel1.Height);
            button_OK.Size = s;

        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void button_Storno_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void Form_DateTime_From_To_KeyDown(object sender, KeyEventArgs e)
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

        private void PerformOK()
        {
            DialogResult = DialogResult.OK;
        }

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Form_DateTime_From_To_Load(object sender, EventArgs e)
        {
            dtp_OD.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dtp_DO.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);

            dtp_OD.Checked = false;
            dtp_DO.Checked = false;

       
        }


        
    }
}
