using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.Vyroba_P.Forms
{
    public partial class FormInputKod : Form
    {
        /// <summary>
        /// Zobrazeni znaku (true - default)), nebo hvezdicek (false)
        /// </summary>
        public bool ShowKod = true;

        public FormInputKod()
        {
            InitializeComponent();
        }

        public string Text_msg
        {
            set
            {
                //label1.Text = base.Text = value;
                label1.Text = value;
            }
        }

        public Color Text_color
        {
            set
            {
                //label1.Text = base.Text = value;
                label1.ForeColor = value;
            }
        }



        public string Kod
        {
            get { return this.textBoxKod.Text; }
            set
            {
                this.textBoxKod.Text = value;
                this.textBoxKod.SelectAll();
            }
        }

        private void FormInputKod_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Location = Settings.ApplicationPosition;
            //this.Icon = Properties.Resources.logo_FASK2;
            //this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            if (!ShowKod)
                textBoxKod.PasswordChar = '*';
            panelButtons_Resize(null, null);
       
        }




        public void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {

            if (this.textBoxKod.Text.Trim().Length == 0)
            {
                //FlexibleMessageBox.Show(this, "Musíte zadat hodnotu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                MessageBox.Show(this, "Musíte zadat hodnotu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
            buttonStorno.Size = nsize;
        }

        private void FormInputKod_KeyDown(object sender, KeyEventArgs e)
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

        private void ucKeyboard1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }


    }
}

