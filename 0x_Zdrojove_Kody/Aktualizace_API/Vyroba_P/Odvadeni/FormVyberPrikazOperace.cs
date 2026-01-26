using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Aktualizace_API.Forms;
using JR.Utils.GUI.Forms;

namespace Fask.Aktualizace_API.Odvadeni
{
    public partial class FormVyberPrikazOperace : Form
    {

        string textform = string.Empty;

        private Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _pracovnik = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik
        {
            set
            {
                _pracovnik = value;
                UpdateTextForm();
            }
        }

        private void UpdateTextForm()
        {
            this.Text = textform;
            if (_pracovnik != null)
                this.Text += ", " + _pracovnik.ToString();
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow vyrobniPrikaz = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow VyrobniPrikaz
        {
            set
            {
                vyrobniPrikaz = value;
                ucDetailHlavicka.DetialObject = new Classes.VyrobniPrikaz(vyrobniPrikaz);
            }
        }

        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable vyrobniPrikazOperace = null;
        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable VyrobniPrikazOperace
        {
            set
            {
                vyrobniPrikazOperace = value;
                cZPROVPPBindingSource.DataSource = vyrobniPrikazOperace;
            }
        }

        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow VyrobniPrikazOperaceVybrana
        {
            get
            {
                try
                {
                    return (cZPROVPPBindingSource.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow;
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    return null;
                }
            }
        }


        public FormVyberPrikazOperace()
        {
            InitializeComponent();

            this.textform = this.Text;
        }

        private void FormVyberPrikazOperace_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            ScannerStart();

            SettingsLoad();

            dataGridView1.Focus();
        }

        private void SettingsLoad()
        {
            splitContainer1.SplitterDistance = Settings.FormVyberPrikazOperaceSplitterDistance1;
            splitContainer2.SplitterDistance = Settings.FormVyberPrikazOperaceSplitterDistance2;
        }

        private void SettingSave()
        {
            Settings.FormVyberPrikazOperaceSplitterDistance1 = splitContainer1.SplitterDistance;
            Settings.FormVyberPrikazOperaceSplitterDistance2 = splitContainer2.SplitterDistance;
            Settings.Update();
        }

        private void ScannerStart()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.DataReady += new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
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
                FormMain.Scanner.DataReady -= new Fask.Aktualizace_API.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length == 0)
                return;

            FlexibleMessageBox.Show(this, "Not Implemented");
        }

        void Scanner_DataReady(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.Aktualizace_API.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        public void PerformCancel()
        {
            finalize();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            finalize();
            DialogResult = DialogResult.OK;
        }

        private void finalize()
        {
            ScannerStop();
            SettingSave();
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ucDetailOperace.DetialObject = new Classes.VyrobniOperace((cZPROVPPBindingSource.Current as DataRowView).Row as Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            this.PerformOK();
        }


    }
}

