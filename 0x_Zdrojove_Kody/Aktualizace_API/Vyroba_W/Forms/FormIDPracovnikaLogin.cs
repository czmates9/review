using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Fask.Vyroba_W.Forms;
using Fask.SQLiteDBs.DataSets;

namespace Fask.Vyroba_W.Forms
{
    public partial class FormIDPracovnikaLogin : Form
    {

		private Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _pracovnik = null;
		public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik
        {
            get { return _pracovnik; }
            //set { _vedouciSmenyLogin = value; }
        }


        public FormIDPracovnikaLogin()
        {
            InitializeComponent();
        }

        private void FormIDPracovnikaLogin_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Location = Settings.ApplicationPosition;
            //this.Icon = Properties.Resources.logo_FASK2;
            //this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            textBoxID_Activate();
            ScannerStart();

            //Keyboard.FormKeyboard.HandleWindowToSend = this.Handle;
            //Keyboard.FormKeyboard.ShowInstance();
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

            if (textBoxHeslo.Text.Trim().Length == 0)
            {
                textBoxHeslo.Focus();
                textBoxHeslo.SelectAll();
                return;
            }

            this.PerformOK();
        }

        void Scanner_DataReady(object sender, Fask.MST_W.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.MST_W.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        public void PerformCancel()
        {
            if (textBoxHeslo.Focused)
            {
                this.textBoxID.Focus();
                this.textBoxID.SelectAll();
                return;
            }

            ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            ScannerStop();
            try
            {
                if (this.textBoxID.Focused)
                {
                    this.textBoxHeslo.Focus();
                    this.textBoxHeslo.SelectAll();
                    return;
                }

				//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter lta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter();
				//lta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
				Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable dtLogins = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Logins(textBoxID.Text.Trim());

                if (dtLogins.Rows.Count == 0)
                    throw new Exception("Uživatel neexistuje");

                if (dtLogins[0].psswd.Trim() != textBoxHeslo.Text.Trim())
                {
                    throw new Exception("Heslo uživatele "+ dtLogins[0].firstname.Trim() + " " + dtLogins[0].surname.Trim() +" není zadáno správnì");
                }

                this._pracovnik = dtLogins[0];
                Globals.Pracovnik = this.Pracovnik;

				//TODO: TaD 30.9.2020 , musela by se tabulka Logins rozširit o VS, a doplnit Vyroba.prd a aj server o tento parametr
				//if (this.Pracovnik.VS)
				//{
				//    Globals.PracovnikVedouciSmeny = this.Pracovnik;
				//}
				//else
				//{
				//    Globals.PracovnikVedouciSmeny = null;
				//}


                // Co tady ???
                DateTime? lstopertimePracovnik = InternalState.GetInternalStateLstOperationUser(Globals.Pracovnik.id);
                lstopertimePracovnik = DateTime.Now;
                // ulozeni posledni operace ... 
                InternalState.UpdateInternalStateLstOperationUser(Globals.Pracovnik.id, lstopertimePracovnik.Value);
                // Zaznamenat cas prihlaseni pracovnika
				//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
				//ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                //ueta.Insert(_vedouciSmenyLogin.id, null, DateTime.Now, UEventStatusTypes.SmenaLogin, _vedouciSmenyLogin.id, Settings.TerminalID, Guid.NewGuid());
				Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(Settings.LastProductionUserID, Settings.MachineID, DateTime.Now, Settings.UEventPracovnikPrihlaseni, Globals.Pracovnik.id, Settings.TerminalID, string.Empty, Guid.NewGuid());

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

        private void FormIDPracovnikaLogin_KeyDown(object sender, KeyEventArgs e)
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

        private void FormIDPracovnikaLogin_Shown(object sender, EventArgs e)
        {

        }

        private void FormIDPracovnikaLogin_Activated(object sender, EventArgs e)
        {
        }

    }
}

