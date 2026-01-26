using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Vyroba_W.Forms;

namespace Fask.Vyroba_W.Odvadeni
{
    public partial class FormIDPracovnika : Form
    {

		private Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _pracovnik = null;
		public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik
        {
            get { return _pracovnik; }
            //set { _vedouciSmenyLogin = value; }
        }


        public FormIDPracovnika()
        {
            InitializeComponent();
        }

        private void FormInputKod_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            textBoxID_Activate();
            ScannerStart();
        }

        private void textBoxID_Activate()
        {
            textBoxID.SelectAll();
            textBoxID.Focus();
        }

        private void ScannerStart()
        {
            try
            {
                FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.DataReady += new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
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
                FormMain.Scanner.DataReady -= new Fask.MST_W.Scanner.ScannerEventHandler(Scanner_DataReady);
                FormMain.Scanner.Disable();
            }
            catch
            {
            }
        }

        private void ScannerEventHandlerMethod(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            if (e.BarcodeData.Trim().Length == 0)
                return;

            this.textBoxID.Text = e.BarcodeData.Trim();

            this.PerformOK();
        }

        void Scanner_DataReady(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.MST_W.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        public void PerformCancel()
        {
            ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            ScannerStop();
            try
            {
				//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter lta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter();
				//lta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
				Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable dtLogins = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Logins(textBoxID.Text.Trim());

                if (dtLogins.Rows.Count == 0)
                    throw new Exception("Uživatel neexistuje");

                this._pracovnik = dtLogins[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                ScannerStart();
                textBoxID_Activate();
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


    }
}

