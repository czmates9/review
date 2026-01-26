using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JR.Utils.GUI.Forms;
using System.IO;

namespace Fask.Aktualizace_API.OdvadeniNadop
{
    public partial class FormVyberZakazka : Form
    {
        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow Zakazka
        {
            get;
            set;
        }

        public FormVyberZakazka()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                label1.Text = base.Text = value;
                //label1.Text = value;
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
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            ScannerStart();
        }

        private void ScannerStart()
        {
            try
            {
                Forms.FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                Forms.FormMain.Scanner.DataReady += new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                Forms.FormMain.Scanner.Enable();
            }
            catch
            {
            }
        }

        private void ScannerStop()
        {
            try
            {
                Forms.FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
            }
            catch
            {
            }
            try
            {
                Forms.FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length == 0)
                return;

            this.Kod = e.BarcodeData.Trim();
            this.PerformOK();
        }

        void Scanner_DataReady(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.Aktualizace_API.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        public void PerformCancel()
        {
            ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            try
            {
                Zakazka = _1NajdiZakazku(this.textBoxKod.Text.Trim());
                if (Zakazka == null)
                    return;
            }
            catch (Exception ex)
            {
                FlexibleMessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                ScannerStart();
                textBoxKod_Activate();
                return;
            }
            ScannerStop();
            DialogResult = DialogResult.OK;
        }

        private void textBoxKod_Activate()
        {
            textBoxKod.SelectAll();
            textBoxKod.Focus();
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow _1NajdiZakazku(string p)
        {
            //Najit vyr.prikaz
            try
            {
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter tavph = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter();
                //tavph.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));

                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable dtVPH = Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcodeH_CZPRO_VPH(p);

                if (dtVPH.Count == 0)
                {
                    textBoxKod_Activate();
                    FlexibleMessageBox.Show(this, "Zakázka nenalezena", this.Text, MessageBoxButtons.OK);
                    return null;
                }
                else if (dtVPH.Count > 1)
                {
                    textBoxKod_Activate();
                    FlexibleMessageBox.Show(this, "Více zakázek!", this.Text, MessageBoxButtons.OK);
                    // TODO : Výbìr
                    return null;
                }
                else
                {
                    return dtVPH[0];
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                textBoxKod_Activate();
                FlexibleMessageBox.Show(this, ex.Message);
                return null;
            }
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

