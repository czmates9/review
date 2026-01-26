using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Fask.MST_W.ServerAccess;
using System.Text;

using System.Threading;

namespace Fask.MST_W.Forms
{
    public partial class LoginForm2 : Form
    {
        /// <summary>
        /// Login zadany uzivatelem
        /// </summary>
        public string Login
        {
            get { return this.login_tb.Text; }
            set { this.login_tb.Text = value; this.heslo_tb.Focus(); }
        }

        /// <summary>
        /// Heslo zadane uzivatelem
        /// </summary>
        public string Heslo
        {
            get { return this.heslo_tb.Text; }
        }

        /// <summary>
        /// Smaze text zadany do policka 'Heslo'
        /// </summary>
        /// <param name="rstLogin">True - smaze i login, False - smaze pouze heslo</param>
        public void ResetPasswd(bool rstLogin)
        {
            if (rstLogin)
            {
                this.login_tb.Text = "";
                this.heslo_tb.Text = "";
                this.login_tb.Focus();
            }
            else
            {
                this.heslo_tb.Text = "";
                this.heslo_tb.Focus();
            }
        }

        public LoginForm2()
        {
            InitializeComponent();

            this.Size = Forms.FormLocation.ScreenResolution;
            this.panel1_Resize(null, null);



            this.Text += " TID:" + MST_Global.TerminalID;

            this.KeyPreview = true;
            this.login_tb.Focus();

           
        }

        private void performEnterPress()
        {
            if (this.Login == "")
            {
                MessageBoxBig.Show(Fask.Localization.Localization.FormsLoginFormZadejteLogin, Fask.Localization.Localization.FormsLoginFormChyba, MessageBoxButtons.OK,
                    MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button1);
                this.login_tb.Focus();
            }
            else if (this.Heslo == "")
            {
                MessageBoxBig.Show(Fask.Localization.Localization.FormsLoginFormZadejteHeslo, Fask.Localization.Localization.FormsLoginFormChyba, MessageBoxButtons.OK,
                    MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button1);
                this.heslo_tb.Focus();
            }
            else
            {
                ScannerFinalize();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            try
            {
                Size nsize = new Size(panel1.Size.Width / 2, panel1.Size.Height);
                ok_but.Size = nsize;
                konec_but.Size = nsize;

                Graphics g = this.CreateGraphics();
                SizeF sizefLabel = g.MeasureString(labelLogin.Text, labelLogin.Font);
                SizeF sizefTextBox = g.MeasureString(login_tb.Text, login_tb.Font);
                g.Dispose();

                labelHeslo.Height = labelLogin.Height = (int)sizefLabel.Height;
                heslo_tb.Height = login_tb.Height = (int)sizefTextBox.Height;

                panelLogin.Height = labelLogin.Height + login_tb.Height + 1;
                panelHeslo.Height = panelLogin.Height;

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void konec_but_Click(object sender, EventArgs e)
        {
            ScannerFinalize();
            DialogResult = DialogResult.Cancel;
        }

        private void ok_but_Click(object sender, EventArgs e)
        {
            performEnterPress();
        }

        private void LoginForm2_Load(object sender, EventArgs e)
        {
            // nacteni lokalizace
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.menuItem5.Enabled = MST_Global.PovolitAktualizacePristupu;

            FillLogins();
            //System.Windows.Forms.Control.DefaultFont,
			this.Text += " " + MST_W.MySystem.Net.Info;   
            ScannerStart();
        }

        private void FillLogins()
        {
            try
            {
                login_tb.BeginUpdate();
                login_tb.Items.Clear();
                foreach (string login in Fask.MST_W.MySystem.LogOperator.GetLogins(Main.CiselnikUzivateleDB))
                {
                    login_tb.Items.Add(login);
                }
                login_tb.EndUpdate();

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void LoginForm2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (login_tb.Focused)
                {
                    heslo_tb.Focus();
                    heslo_tb.SelectAll();
                }
                else
                {
                    performEnterPress();
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (heslo_tb.Focused)
                {
                    login_tb.Focus();
                    login_tb.SelectAll();
                }
                else
                {
                    //this.DialogResult = DialogResult.Cancel;
                    konec_but_Click(null, null);
                }
            }
        }

        private void buttonLoginsUpdate_Click(object sender, EventArgs e)
        {
            _WebRefernces_Globals.LoginServiceSession loginService = new _WebRefernces_Globals.LoginServiceSession();

            loginService.Timeout = MST_Global.ServiceTimeOut;
            loginService.Url = MST_Global.ServerAddress + "LoginService.asmx";
            loginService.UpdateWebServiceCredentials();
            try
            {

                //Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.FormsLoginFormAktualizacePristupu);
                
                //LoginService.StatusObject so = loginService.GetKatalogUzivatele(MST_Global.TerminalID);
                //if (so.Exception)
                //    throw new Exception(Fask.Localization.Localization.FormsLoginFormAktualizacePristupu + "\n" + so.StatusText);

                _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogUzivatelu(loginService);

                //Program.mstw.mbw.EndPracujiForm();

            }
            catch (Exception ex)
            {

                Program.mstw.mbw.EndPracujiForm();
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            FillLogins();

            MessageBoxBig.Show(Fask.Localization.Localization.FormsLoginFormAktualizacePristupuDokoncena, Color.DarkGreen);
            
        }

        private void chkPasswordShow_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkPasswordShow.Checked)
                heslo_tb.PasswordChar = (char)0;
            else
                heslo_tb.PasswordChar = '*';
        }

        private bool scannserstart = true;
        private void ScannerFinalize()
        {
            this.ScannerStop();
            this.scannserstart = false;
        }

        private void ScannerStart()
        {
            if (!scannserstart)
                return;

            //try
            //{
            //    Program.mstw.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            //    Program.mstw.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            //}
            //catch (Exception ex)
            //{
            //    Logging.Log.Write(ex);
            //    //MessageBox.Show("Chyba  Scanneru : " + ex.Message, "Chyba");
            //    return;
            //}
            Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady)); 
            //Program.mstw.ScannerEventAdd(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady2));
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            //try
            //{
            //    Program.mstw.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            //}
            //catch
            //{
            //}
            //try
            //{
            //    Program.mstw.DisableScanner();
            //}
            //catch
            //{
            //}
            Program.mstw.ScannerEventRemove(new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady));
            Program.mstw.DisableScanner();
        }


        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        }

        void Scanner_DataReady2(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        }

        private delegate void ScannerEventMethodDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        private void ScannerEventMethod(Fask.ScannerProvider.ScannerEventArgs e)
        {
            string barcode = e.BarcodeData.Trim();
            if (barcode != string.Empty)
            {
                //SqlCEDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter usersta = new Fask.SQLiteDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter();
                //usersta.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Main.CiselnikUzivateleDB);
                //usersta.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikUzivateleDB);

				Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable users = new Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable();

				try
				{
					using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Users usr = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Users(Main.CiselnikUzivateleDB))
					{
						users = usr.GetDataByEAN(barcode);
					}
				}
				catch (Exception ex)
				{
					Logging.Log.Write(ex);
				}

                if (users.Count <= 0)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.FormsLoginFormCarKodNenalezen, Fask.Localization.Localization.FormsLoginFormChyba, MessageBoxButtons.OK,
                            MessageBoxBigIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (users.Count > 1)
                {
                    MessageBoxBig.Show(Fask.Localization.Localization.FormsLoginFormCarKodNalezenoViceUzivatelu, Fask.Localization.Localization.FormsLoginFormChyba, MessageBoxButtons.OK,
                MessageBoxBigIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                string pswd = "";
                if (!users[0].IsPwdNull() && users[0].Pwd.Trim().Length > 0)
                    pswd = users[0].Pwd;
                //else
                //    pswd = users[0].Hash;

                this.login_tb.Text = users[0].Login.Trim();
                this.heslo_tb.Text = pswd.Trim();
                performEnterPress();
            }
            if (MST_Global.OnScannerSound_Forms)
            {
                MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
            }
        }



    }
}