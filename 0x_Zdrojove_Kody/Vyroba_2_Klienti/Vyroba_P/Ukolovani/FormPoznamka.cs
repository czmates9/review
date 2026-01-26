using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fask.Vyroba_P.Forms;
using System.IO;
using Fask.Vyroba_P.Extensions;
using JR.Utils.GUI.Forms;

namespace Fask.Vyroba_P.Ukolovani
{
    public partial class FormPoznamka : Form
    {

        private string _poznamka = string.Empty;
        public string Poznamka
        {
            get { return textBoxPoznamka.Text.Trim(); }
        }


        public FormPoznamka()
        {
            InitializeComponent();

        }

        private void FormBase_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            //textBoxVyrobniOperaceFocusAll();
            //ScannerStart();
        }

        //private void ScannerStart()
        //{
        //    try
        //    {
        //        FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
        //        FormMain.Scanner.DataReady += new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
        //        FormMain.Scanner.Enable();
        //    }
        //    catch
        //    {
        //    }
        //}

        //private void ScannerStop()
        //{
        //    try
        //    {
        //        FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
        //        FormMain.Scanner.Disable();
        //    }
        //    catch
        //    {
        //    }
        //}

        //private void ScannerEventHandlerMethod(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        //{
        //    if (e.BarcodeData.Trim().Length == 0)
        //        return;

        //    this.textBoxPoznamka.Text = e.BarcodeData.Trim();
        //    //textBoxVyrobniOperaceFocusAll();

        //    this.PerformOK();
        //}

        private void PerformOK()
        {
            DialogResult = DialogResult.OK;
        }

        private void PerformCancel()
        {
            DialogResult = DialogResult.Cancel;
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

        private void FormOdvadeni_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        private void FormOdvadeni_Shown(object sender, EventArgs e)
        {
            bool focused = this.textBoxPoznamka.Focus();
        }

        private void ucKeyboard1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }



    }
}