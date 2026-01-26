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
using Fask.Vyroba_P.ServerAccess;
using Fask.Logging;

namespace Fask.Vyroba_P.Odvadeni
{
    public partial class FormOdvadeni : Form
    {
        public enum TIMEMODES
        {
            Unknown = -1,
            Stop = 0,
            StartStop = 1,
            StartStartStop = 2
        }

        public enum TIMESTATE
        {
            Unknown = -1,
            Nezahajeno = 0,
            Korekce_Zahajena = 2,
            Korekce_Dokoncena = 3,
            Priprava_Zahajena = 4,
            Priprava_Dokoncena = 5,
            Odvod_Zahajen = 6,
            Odvod_Dokoncen = 7
        }


        Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idpracovnik = null;
        Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idmachine = null;

        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta;

        string textform = string.Empty;


        private void UpdateTextForm()
        {
            this.Text = textform;
            if (idpracovnik != null)
                this.Text += ", " + idpracovnik.ToString();

            if (idmachine != null)
                this.Text += ", " + idmachine.name.Trim();
        }

        private FormOdvadeni()
        {
            InitializeComponent();

            this.textform = this.Text;

            //pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
            //pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);

        }

        public FormOdvadeni(Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idpracovnik, Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idmachine)
            : this()
        {
            this.idpracovnik = idpracovnik;
            this.idmachine = idmachine;

            UpdateTextForm();
        }

        private void FormBase_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
#if DEBUG
            //textBoxVyrobniOperace.Text = "8594005006744";
#endif

            panelButtons_Resize(null, null);
            textBoxVyrobniOperaceFocusAll();
            ScannerStart();
        }

        private void ScannerStart()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.DataReady += new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Enable();
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }

        private void ScannerStop()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length == 0)
                return;

            this.textBoxVyrobniOperace.Text = e.BarcodeData.Trim();
            textBoxVyrobniOperaceFocusAll();

            this.PerformOK();
        }

        void Scanner_DataReady(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        {
            try
            {
                this.BeginInvoke(new Fask.Vyroba_P.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });

            }
            catch (Exception ex)
            {
                //Exceptions.Handler.ErrorHandle(ex.Message, "Adam_DataReady", false);
                ExceptionHandler2.Handle(ex);
            }
        }

        private void textBoxVyrobniOperaceFocusAll()
        {
            try
            {
                this.textBoxVyrobniOperace.Focus();
                this.textBoxVyrobniOperace.SelectAll();
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
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
            if (Settings.ZruseniOperace)
            {
                Size nsize = new Size(panelButtons.Width / 4, panelButtons.Height);
                buttonStorno.Size = nsize;
                buttonOK.Size = nsize;
            }
            else
            {
                Size nsize = new Size(panelButtons.Width / 2, panelButtons.Height);
                buttonStorno.Size = nsize;
                buttonOK.Size = nsize;
            }
        }

        private void FormOdvadeni_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        private void FormOdvadeni_Shown(object sender, EventArgs e)
        {
            bool focused = this.textBoxVyrobniOperace.Focus();
        }

        private void ucKeyboard1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void button_zrusitoperaci_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteOperace();
                //this.textBoxVyrobniOperace.Focus();
                //this.textBoxVyrobniOperace.SelectAll();
            }
            catch (Exception ex) 
            {
                FlexibleMessageBox.Show(this, ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            
            }
        }


    }
}