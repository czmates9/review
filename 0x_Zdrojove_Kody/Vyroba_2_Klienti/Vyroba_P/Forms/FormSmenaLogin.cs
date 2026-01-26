using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JR.Utils.GUI.Forms;

namespace Fask.Vyroba_P.Forms
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

        public override string  Text
        {
	        get 
	        { 
		        return base.Text;
	        }
	        set 
	        { 
		        base.Text = value;
                label1.Text = value;
	        }
        }

        private void FormInputKod_Load(object sender, EventArgs e)
        {
#if DEBUG
            textBoxID.Text = "0";
            textBoxPassword.Text = "1";


#endif

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

            this.textBoxID.Text = e.BarcodeData.Trim();
            if (this.textBoxPassword.Text.Length == 0)
            {
                this.textBoxPassword.Focus();
                this.textBoxPassword.SelectAll();
                return;
            }

            this.PerformOK();
        }

        void Scanner_DataReady(object sender, Fask.Vyroba_P.Scanner.ScannerEventArgs e)
        {
            this.BeginInvoke(new Fask.Vyroba_P.Scanner.ScannerEventHandler(ScannerEventHandlerMethod), new object[] { sender, e });
        }

        public void PerformCancel()
        {
            ScannerStop();
            DialogResult = DialogResult.Cancel;
        }

        public void PerformOK()
        {
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
                Fask.SQLiteDBs.DataSets.Vyroba.LoginsDataTable dtLogins = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Logins(textBoxID.Text.Trim());

                if (dtLogins.Rows.Count == 0)
                    throw new Exception("Uživatel neexistuje");

                if (dtLogins[0].psswd.Trim() != textBoxPassword.Text)
                    throw new Exception("Heslo uživatele:\n'" + dtLogins[0].firstname.Trim() + " " + dtLogins[0].surname.Trim() + "'\nnesouhlasí");

                if (dtLogins[0].VS != 1)
                    throw new Exception("Uživatel '" + dtLogins[0].firstname.Trim() + " " + dtLogins[0].surname.Trim() + "'\nnení vedoucí smìny");

                this._vedouciSmenyLogin = dtLogins[0];
                Globals.PracovnikVedouciSmeny = this._vedouciSmenyLogin;

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);

                //ueta.Insert(_vedouciSmenyLogin.id, null, DateTime.Now, UEventStatusTypes.SmenaLogin, _vedouciSmenyLogin.id, Settings.TerminalID, Guid.NewGuid());
                Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(_vedouciSmenyLogin.id, Settings.MachineID, DateTime.Now, Settings.UEventSmenaLogin, _vedouciSmenyLogin.id, Settings.TerminalID, string.Empty, Guid.NewGuid());
                Settings.LastProductionUserID = _vedouciSmenyLogin.id;
                Settings.LastProductionDateTime = DateTime.Now;
                Settings.Update();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MySystem.Audio.PlaySound(Path.Combine(MySystem.MyPath.SoundDirectory, Constants.SoundChyba));
                FlexibleMessageBox.Show(this, ex.Message, this.Text);
                ScannerStart();
                return;
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
            Control ctl;
            ctl = (Control)sender;
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (Settings.OvladaniNumerickouKlavesnici)
                {
                    if (buttonOK.Focused && Keys.Enter == e.KeyCode)
                    {
                        PerformOK();
                    }
                    //else if (Keys.Enter == e.KeyCode)
                    else if (Keys.Down == e.KeyCode)
                    {
                        //SendKeys.Send("{TAB}");
                        //if(Parent != null)
                        //Parent.SelectNextControl(Parent, true, false, false, true);
                        ctl.SelectNextControl(this.ActiveControl, true, true, true, true);
                    }
                    //else if (Keys.Divide == e.KeyCode)
                    else if (Keys.Up == e.KeyCode)
                    {
                        //SendKeys.Send("+{TAB}");
                        ctl.SelectNextControl(this.ActiveControl, false, true, true, true); // (this.ActiveControl, false, false, true, true);
                    }
                    else if ((Keys.OemBackslash == e.KeyCode) || (Keys.Divide == e.KeyCode))  // "/"
                    {
                    }
                    else if ((Keys.Oemplus == e.KeyCode) || (Keys.Add == e.KeyCode))  // "+"
                    {
                    }
                    else if ((Keys.OemMinus == e.KeyCode) || (Keys.Subtract == e.KeyCode))  // "-"
                    {
                    }
                    else if (Keys.Multiply == e.KeyCode)  // "*"
                    {
                    }
                    
                    //this.SelectNextControl(null, true, 
                }
                else
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

