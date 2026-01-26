using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JR.Utils.GUI.Forms;

namespace Fask.Aktualizace_API.Odvadeni
{
    public partial class FormBlokace : Form
    {
        
        //private Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();

        private Classes.VyrobniPrikaz _vyrobniPrikaz = null;
        public FormBlokace(Classes.VyrobniPrikaz vyrobniPrikaz)
        {
            InitializeComponent();

            _vyrobniPrikaz = vyrobniPrikaz;
            ucDetail1.DetialObject = _vyrobniPrikaz;
            
            //pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
        }

        private void FormBase_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            ScannerStart();

            SettingLoad();
            SettingSave();

            if ((Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.NotUploadedCnt_Production(_vyrobniPrikaz.CountEntries, _vyrobniPrikaz.SOPNUMBE) ?? 0) > 0)
            {
                buttonUkoncit.Enabled = false;
            }
        }

        private void SettingSave()
        {
            Settings.FormBlokaceSplitterDistance1 = splitContainer1.SplitterDistance;
            Settings.Update();
        }

        private void SettingLoad()
        {
            splitContainer1.SplitterDistance = Settings.FormBlokaceSplitterDistance1;
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
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            try
            {
                Forms.FormMain.Scanner.Disable();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length == 0)
                return;

            this.PerformOK();
        }

        void Scanner_DataReady(object sender, Fask.Aktualizace_API.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.Aktualizace_API.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        private void finalize()
        {
            ScannerStop();
            SettingSave();
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

        public void PerformAbort()
        {
            if ((Fask.Aktualizace_API.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.NotUploadedCnt_Production(_vyrobniPrikaz.CountEntries, _vyrobniPrikaz.SOPNUMBE) ?? 0) > 0)
            {
                FlexibleMessageBox.Show(this, "Vyrobní data ještì nebyla synchronizována se serverem.\nVyèkejte nebo proveïte synchronizaci ruènì z hlavního menu.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DialogResult.No == FlexibleMessageBox.Show(this, "Opravdu chcete ukonèit dávku?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            finalize();
            DialogResult = DialogResult.Abort;
        }

        public void PerformRetry()
        {
            if (DialogResult.No == FlexibleMessageBox.Show(this, "Opravdu chcete odblokovat dávku?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            finalize();
            DialogResult = DialogResult.Retry;
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
                else if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.Enter)
                {
                    PerformOK();
                }
                else if (e.KeyCode == Keys.F5)
                {
                    PerformRetry();
                }
                else if (e.KeyCode == Keys.F9)
                {
                    PerformAbort();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        private void buttonPokracovat_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void buttonUkoncit_Click(object sender, EventArgs e)
        {
            PerformAbort();
        }

        private void buttonOdblokovat_Click(object sender, EventArgs e)
        {
            PerformRetry();
        }
    }
}