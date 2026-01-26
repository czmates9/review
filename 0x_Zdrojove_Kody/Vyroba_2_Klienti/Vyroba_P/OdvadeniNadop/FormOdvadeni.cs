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

namespace Fask.Vyroba_P.OdvadeniNadop
{
    public partial class FormOdvadeni : Form
    {
        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter tapro = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();        
        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter tacor = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter();
        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter taprohist = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter taprocache = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();

        public enum TIMEMODES
        {
            Unknown = -1,
            Stop = 0,
            StartStop = 1,
            StartStartStop = 2
        }

        public enum TIMESTATE
        {
            Nezahajeno,
            Korekce_Zahajena,
            Korekce_Dokoncena,
            Priprava_Zahajena,
            Priprava_Dokoncena,
            Odvod_Zahajen,
            Odvod_Dokoncen
        }

        Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idpracovnik = null;
        Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idmachine = null;

        string textform = string.Empty;

        private void UpdateTextForm()
        {
            this.Text = textform;
            if (idpracovnik != null)
            {
                this.Text += ", " + idpracovnik.ToString();
                toolStripStatusPracovnik.Text = "" + (idpracovnik == null ? string.Empty : "P: " + idpracovnik.firstname + " " + idpracovnik.surname);
            }

            if (idmachine != null)
                this.Text += ", " + idmachine.name.Trim();
        }

        private FormOdvadeni()
        {
            InitializeComponent();

            this.textform = this.Text;
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
            panelButtons_Resize(null, null);
            textBoxVyrobniOperaceFocusAll();

            //tapro.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
            //tacor.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
            //taprohist.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionHist + Constants.PRD);
            //taprocache.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD);

            // je nastaveno ID pøestávky
            Logging.ExceptionHandler2.Handle( Logging.LogLevel.Info , "Prestavka ID is " + Settings.PrestavkaID);
            Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "Uzivatel " + idpracovnik.id);
            if (Settings.PrestavkaID != -1)
            {
                var correctionRow = getLastCorrectionRow();
                // je zahájena pøestávka
                Logging.ExceptionHandler2.Handle(Logging.LogLevel.Info, "correctionRow is " + (correctionRow != null ? "not null" : "null"));
                //if (correctionRow != null)
                //{
                    if (correctionRow != null && correctionRow.id == Settings.PrestavkaID)
                    {
                        this.buttonZahajeniPrestavky.Enabled = false;
                        this.buttonKonecPrestavky.Enabled = true;
                    }
                    else  // není zahájena pøestávka
                    {
                        this.buttonZahajeniPrestavky.Enabled = true;
                        this.buttonKonecPrestavky.Enabled = false;
                    }
                //}
                //else
                //{
                //    this.buttonZahajeniPrestavky.Enabled = false;
                //    this.buttonKonecPrestavky.Enabled = false;
                //}
            }
            else  // není nastaveno ID pøestávky ... vypnutí tlaèítek
            {
                this.buttonZahajeniPrestavky.Enabled = false;
                this.buttonKonecPrestavky.Enabled = false;
            }
            
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
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Vyroba_P.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch
            {
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
            this.BeginInvoke(new Fask.Vyroba_P.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        private void textBoxVyrobniOperaceFocusAll()
        {
            try
            {
                this.textBoxVyrobniOperace.Focus();
                this.textBoxVyrobniOperace.SelectAll();
            }
            catch { }
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

            Size osize = new Size(panelOdchody.Width / 3, panelOdchody.Height);
            buttonZahajeniPrestavky.Size = osize;
            buttonOdchodZPracoviste.Size = osize;
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

        private void buttonKonecPrestavky_Click(object sender, EventArgs e)
        {
            this.PerformKonecPrestavky();
        }

        private void buttonZahajeniPrestavky_Click(object sender, EventArgs e)
        {
            this.PerformZahajeniPrestavky();
        }

        private void buttonOdchodZPracoviste_Click(object sender, EventArgs e)
        {
            this.PerformOdchodZPRacoviste();
        }
    }
}