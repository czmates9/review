using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fask.Vyroba_W.Forms
{
    public partial class FormUserLoginWithTimeInput : Form
    {
        public DateTime UserLoginDateTime
        {
            get
            {
                return new DateTime(
                    dateTimePickerDate.Value.Year,
                    dateTimePickerDate.Value.Month,
                    dateTimePickerDate.Value.Day,
                    dateTimePickerTime.Value.Hour,
                    dateTimePickerTime.Value.Minute,
                    dateTimePickerTime.Value.Second
                    ); 
                //return DateTime.Now;
            }
        }

        public FormUserLoginWithTimeInput()
        {
            InitializeComponent();
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

        private void PerformOK()
        {
            DialogResult = DialogResult.OK;
        }

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FormUserLoginWithTimeInput_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            dateTimePickerTime.Value = dateTimePickerDate.Value = DateTime.Now;
            this.panelButtons_Resize(null, null);
            this.dateTimePickerTime.Focus();
        }

        private void FormUserLoginWithTimeInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.PerformOK();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    this.PerformCancel();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }
    }
}