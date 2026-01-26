using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fask.Vyroba_W.Forms
{
    public partial class FormSmenaLogin : Form
    {

		private Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow _vedouciSmenyLogin = null;
		public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow VedouciSmenyLogin
        {
            get { return _vedouciSmenyLogin; }
            //set { _vedouciSmenyLogin = value; }
        }


        public FormSmenaLogin()
        {
            InitializeComponent();
        }

        private void FormInputKod_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            panelButtons_Resize(null, null);
            ScannerStart();
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
            if (this.textBoxPassword.Text.Length == 0)
            {
                this.textBoxPassword.Focus();
                this.textBoxPassword.SelectAll();
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
            if (this.textBoxPassword.Focused)
            {
                this.textBoxID.Focus();
                return;
            }

            ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
            if (this.textBoxID.Focused)
            {
                this.textBoxPassword.Focus();
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            this.Focus();
            Application.DoEvents();

            ScannerStop();
            try
            {
                if (this.textBoxID.Text.Trim().Length == 0)
                {
                    this.textBoxID.Focus();
                    this.textBoxID.SelectAll();
                    throw new Exception("Není zadáno ID pracovníka");
                }

                if (this.textBoxPassword.Text.Trim().Length == 0)
                {
                    this.textBoxPassword.Focus();
                    this.textBoxPassword.SelectAll();
                    throw new Exception("Není zadáno heslo");
                }

				//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter lta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.LoginsTableAdapter();
				//lta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
				Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable dtLogins = Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Logins(textBoxID.Text.Trim());

                if (dtLogins.Rows.Count == 0)
                    throw new Exception("Uživatel neexistuje");

                if (dtLogins[0].psswd.Trim() != textBoxPassword.Text.Trim())
                    throw new Exception("Heslo uživatele:\n'" + dtLogins[0].firstname.Trim() + " " + dtLogins[0].surname.Trim() + "'\nnesouhlasí");

                this._vedouciSmenyLogin = dtLogins[0];

				//Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
				//ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);

                //ueta.Insert(_vedouciSmenyLogin.id, null, DateTime.Now, UEventStatusTypes.SmenaLogin, _vedouciSmenyLogin.id, Settings.TerminalID, Guid.NewGuid());
				Fask.Vyroba_W.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(_vedouciSmenyLogin.id, null, DateTime.Now, Settings.UEventSmenaLogin, _vedouciSmenyLogin.id, Settings.TerminalID, string.Empty, Guid.NewGuid());
                Settings.LastProductionUserID = _vedouciSmenyLogin.id;
                Settings.LastProductionDateTime = DateTime.Now;
                Settings.Update();
            }
            catch (Exception ex)
            {
                if (textBoxID.Text.Trim().Length == 0 && textBoxPassword.Text.Trim() == "159")
                {
                    //administrativni vstup pro konfiguraci
                    DialogResult = DialogResult.Abort;
                    return;
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundChyba));
                    MessageBox.Show(ex.Message, this.Text);
                    ScannerStart();
                    return;
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
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

