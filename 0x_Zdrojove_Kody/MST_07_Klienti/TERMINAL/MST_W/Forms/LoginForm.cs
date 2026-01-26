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
	/// <summary>
	/// Formular vyzyvajici k zalogovani operatora
	/// </summary>
	public class LoginForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label labelLogin;
		private System.Windows.Forms.Label labelHeslo;
		private System.Windows.Forms.ComboBox login_tb;
        private Button konec_but;
        private Button ok_but;
        private Panel panel1;
        private MainMenu mainMenu1;
        private Panel panel2;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuItem3;
        private MenuItem menuItem4;
        private MenuItem menuItem5;
        private Panel panelLogin;
        private Panel panelHeslo;
        private CheckBox chkPasswordShow;
        private System.Windows.Forms.TextBox heslo_tb;
	
		public LoginForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            this.Size = Forms.FormLocation.ScreenResolution;
            this.panel1_Resize(null, null);

            

            this.Text += " TID:" + MST_Global.TerminalID;

            this.KeyPreview = true;
            this.login_tb.Focus();

            ScannerStart();
		}
    

		/// <summary>
		/// Login zadany uzivatelem
		/// </summary>
		public string Login
		{
            get { return this.login_tb.Text;}
            set { this.login_tb.Text = value; this.heslo_tb.Focus(); }
		}

		/// <summary>
		/// Heslo zadane uzivatelem
		/// </summary>
		public string Heslo
		{
			get{return this.heslo_tb.Text;}
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

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelHeslo = new System.Windows.Forms.Label();
            this.login_tb = new System.Windows.Forms.ComboBox();
            this.heslo_tb = new System.Windows.Forms.TextBox();
            this.konec_but = new System.Windows.Forms.Button();
            this.ok_but = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelHeslo = new System.Windows.Forms.Panel();
            this.chkPasswordShow = new System.Windows.Forms.CheckBox();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelHeslo.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLogin
            // 
            resources.ApplyResources(this.labelLogin, "labelLogin");
            this.labelLogin.Name = "labelLogin";
            // 
            // labelHeslo
            // 
            resources.ApplyResources(this.labelHeslo, "labelHeslo");
            this.labelHeslo.Name = "labelHeslo";
            // 
            // login_tb
            // 
            resources.ApplyResources(this.login_tb, "login_tb");
            this.login_tb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.login_tb.Name = "login_tb";
            // 
            // heslo_tb
            // 
            resources.ApplyResources(this.heslo_tb, "heslo_tb");
            this.heslo_tb.Name = "heslo_tb";
            // 
            // konec_but
            // 
            resources.ApplyResources(this.konec_but, "konec_but");
            this.konec_but.Name = "konec_but";
            this.konec_but.TabStop = false;
            this.konec_but.Click += new System.EventHandler(this.konec_but_Click);
            // 
            // ok_but
            // 
            resources.ApplyResources(this.ok_but, "ok_but");
            this.ok_but.Name = "ok_but";
            this.ok_but.TabStop = false;
            this.ok_but.Click += new System.EventHandler(this.ok_but_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.konec_but);
            this.panel1.Controls.Add(this.ok_but);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem4);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItem2
            // 
            resources.ApplyResources(this.menuItem2, "menuItem2");
            this.menuItem2.Click += new System.EventHandler(this.ok_but_Click);
            // 
            // menuItem3
            // 
            resources.ApplyResources(this.menuItem3, "menuItem3");
            this.menuItem3.Click += new System.EventHandler(this.konec_but_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.MenuItems.Add(this.menuItem5);
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // menuItem5
            // 
            resources.ApplyResources(this.menuItem5, "menuItem5");
            this.menuItem5.Click += new System.EventHandler(this.buttonLoginsUpdate_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelHeslo);
            this.panel2.Controls.Add(this.panelLogin);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panelHeslo
            // 
            this.panelHeslo.Controls.Add(this.chkPasswordShow);
            this.panelHeslo.Controls.Add(this.heslo_tb);
            this.panelHeslo.Controls.Add(this.labelHeslo);
            resources.ApplyResources(this.panelHeslo, "panelHeslo");
            this.panelHeslo.Name = "panelHeslo";
            // 
            // chkPasswordShow
            // 
            resources.ApplyResources(this.chkPasswordShow, "chkPasswordShow");
            this.chkPasswordShow.Name = "chkPasswordShow";
            this.chkPasswordShow.CheckStateChanged += new System.EventHandler(this.chkPasswordShow_CheckStateChanged);
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.login_tb);
            this.panelLogin.Controls.Add(this.labelLogin);
            resources.ApplyResources(this.panelLogin, "panelLogin");
            this.panelLogin.Name = "panelLogin";
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Menu = this.mainMenu1;
            this.Name = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelHeslo.ResumeLayout(false);
            this.panelLogin.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

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

		private void ok_but_Click(object sender, System.EventArgs e)
        {
            performEnterPress();
		}

		private void LoginForm_Load(object sender, System.EventArgs e)
		{
            // nacteni lokalizace
            Fask.Localization.LocalizationExtensionForm.Localize(this);

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            this.menuItem5.Enabled = MST_Global.PovolitAktualizacePristupu;
            
            FillLogins();
            //System.Windows.Forms.Control.DefaultFont
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

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
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

        private void buttonLoginsUpdate_Click(object sender, EventArgs e)
        {
            LoginService.LoginService loginService = new Fask.MST_W.LoginService.LoginService();

            loginService.Timeout = MST_Global.ServiceTimeOut;
            loginService.Url = MST_Global.ServerAddress + "LoginService.asmx";
            loginService.UpdateWebServiceCredentials();
            try
            {

                Program.mstw.mbw.BeginPracujiForm(Fask.Localization.Localization.FormsLoginFormAktualizacePristupu);

                LoginService.StatusObject so = loginService.GetKatalogUzivatele(MST_Global.TerminalID);
                if (so.Exception)
                    throw new Exception(Fask.Localization.Localization.FormsLoginFormAktualizacePristupu + "\n" + so.StatusText);

                _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogUzivatelu(loginService);

                Program.mstw.mbw.EndPracujiForm();

            }
            catch (Exception ex)
            {
 
                Program.mstw.mbw.EndPracujiForm();
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            #region oldVersion
            /*
            ConfigurationService.Configuration configurations = new Fask.MST_W.ConfigurationService.Configuration();
            configurations.Timeout = MST_Global.ServiceTimeOut;
            configurations.Url = MST_Global.ServerAddress + "Configuration.asmx";
            configurations.UpdateWebServiceCredentials();

            string pwd = null;

            try
            {
                Program.mstw.mbw.BeginPracujiForm("Aktualizace p¯Ìstup˘");
                pwd = configurations.GetPasswords();
                Program.mstw.mbw.EndPracujiForm();
            }
            catch (Exception ex)
            {
                Program.mstw.mbw.EndPracujiForm();
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                return;
            }

            if (pwd != null)
            {
                StreamWriter sw = null;
                try
                {
                    sw = System.IO.File.CreateText(MST_W.Main.PasswordsFileName);
                    sw.Write(pwd);
                    sw.Close();
                    sw = null;
                }
                catch (Exception ex)
                {
                    MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    return;
                }
                finally
                {
                    if (sw != null)
                        sw.Close();
                }
            }
            */
         
         
            #endregion

            FillLogins();

            MessageBoxBig.Show(Fask.Localization.Localization.FormsLoginFormAktualizacePristupuDokoncena, Color.DarkGreen);
            
        }

        private void konec_but_Click(object sender, EventArgs e)
        {
            ScannerFinalize();
            DialogResult = DialogResult.Cancel;
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
            
            try
            {
               Program.mstw.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
               Program.mstw.Scanner.DataReady += new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                //MessageBox.Show("Chyba  Scanneru : " + ex.Message, "Chyba");
                return;
            }
            Program.mstw.EnableScanner();
        }

        private void ScannerStop()
        {
            try
            {
                Program.mstw.Scanner.DataReady -= new Fask.ScannerProvider.ScannerEventHandler(Scanner_DataReady);
            }
            catch
            {
            }
            try
            {
                Program.mstw.DisableScanner();
            }
            catch
            {
            }
        }


        void Scanner_DataReady(object sender, Fask.ScannerProvider.ScannerEventArgs e)
        {
            this.BeginInvoke(new ScannerEventMethodDelegate(ScannerEventMethod), new object[] { e });
        }

        private delegate void ScannerEventMethodDelegate(Fask.ScannerProvider.ScannerEventArgs e);

        private void ScannerEventMethod(Fask.ScannerProvider.ScannerEventArgs e)
        {
            string barcode = e.BarcodeData.Trim();
            if (barcode != string.Empty)
            {
                SqlCEDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter usersta = new Fask.MST_W.SqlCEDBs.DataSets.UzivateleTableAdapters.UsersTableAdapter();
                usersta.Connection = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Main.CiselnikUzivateleDB);

                SqlCEDBs.DataSets.Uzivatele.UsersDataTable users = usersta.GetDataByEAN(barcode);

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
                if(!users[0].IsPwdNull() && users[0].Pwd.Trim().Length > 0)
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

        //private void printTest(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //string texttoprint =
        //        StringBuilder sb2print = new StringBuilder();
        //        sb2print.Append(
        //            //"\r\n" +
        //            //"! U1 SETLP CN.CPF 0 15\r\n" +
        //            //"! U1 ENCODING ASCII\r\n" +
        //            //"! U1 PW 900\r\n" +
        //            //"! U1 SETLP CNB.CPF 0 15\r\n" +
        //            //"KÛd      N·zov          Veækosù  Farba      Mn.   MJ  Cena/MJ     Spolu\r\n" +
        //            //"! U1 SETLP CN.CPF 0 15\r\n" +
        //            //"================================================================\r\n" +
        //            //"5012     ! U1 SETLP CNB.CPF 0 15  ZvinovaËka obyË., bez kraj.viaz, dvojvol·n-obdÂûnik\r\n" +
        //            //"! U1 SETLP CN.CPF 0 15\r\n" +
        //            //"   112x35   Modr·      99,99 ks  11,67 EUR   1166,88 EUR\r\n" +
        //            //"VytæaËenÈ: 13.2.2015 17:15\r\n" +
        //            //"\r\n"
        //            //;
        //            "\r\n" +
        //            "! U1 SETLP CN.CPF 0 10\r\n" +
        //            "! U1 ENCODING ASCII\r\n" +
        //            "! U1 PW 900\r\n" +
        //            "|         |         |         |         |         |         |   \r\n" +
        //            "1234567890123456789012345678901234567890123456789012345678901234\r\n" +
        //            "\r\n" +
        //            "Dod·vateæ:                     Odberateæ:\r\n" +
        //            "Viera Korytkov· - Richelieu    Brakon-veækosklad\r\n" +
        //            "J.Greö·ka 11                   Ing. Branislav Kovaæ\r\n" +
        //            "085 01 Bardejov                Frantiök·nske n·m. 6\r\n" +
        //            "I»O: 30634105                  080 01 Preöov\r\n" +
        //            "I» DPH: SK1020703002           \r\n" +
        //            "================================================================\r\n" +
        //            "KÛd      N·zov             Veækosù  Farba      Mnoûstvo Cena/MJ\r\n" +
        //            "================================================================\r\n" +
        //            "5012     ZvinovaËka obyË., 112x35   Modr·         99,99  99,99 Ä\r\n" +
        //            "5019     ZavinovaËka bez k 112x35   Ruûov·        99,99  99,99 Ä\r\n" +
        //            "5012     ZavinovaËka bez k 112x35   Ruûov·        99,99  99,99 Ä\r\n" +
        //            "1321     PolodupaËky OLY   80       Biela-svet    99,99  99,99 Ä\r\n" +
        //            "6001     Fus·k z OV»IEHO R 85x38x12 Modr·-svet    99,99  99,99 Ä\r\n" +
        //            "================================================================\r\n" +
        //            "         Mnoûstvo    DPH      bez DPH        s DPH           DPH\r\n" +
        //            "Spolu:     499,95    21%  249950,00 Ä  302439,50 Ä     52489,5 Ä\r\n" +
        //            "================================================================\r\n" +
        //            "VystavenÈ: $Datetimeprint$\r\n" +
        //            "\r\n"
        //            );
        //        sb2print.Replace("$Datetimeprint$", DateTime.Now.ToString());

        //        bool printed = Program.mstw.Printer.PrintText(
        //            sb2print.ToString()
        //            , Fask.PrinterFactory.PrinterModules.TextVolny, 1);
        //        MessageBox.Show("Printed=" + printed);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void menuItem7_Click(object sender, EventArgs e)
        //{
        //    //System.Collections.Generic.List<System.Text.Encoding> encodingList = new System.Collections.Generic.List<Encoding>();
        //    //System.Collections.Generic.List<Exception> exceptionList = new System.Collections.Generic.List<Exception>();

        //    string filename = Path.Combine(Main.WrkDir, "encodings.txt");
        //    StreamWriter sw = new StreamWriter(filename, false);

        //    for (int i = 0; i < 30000; i++)
        //    {
        //        try
        //        {
        //            Encoding enc = System.Text.Encoding.GetEncoding(i);
        //            sw.WriteLine(enc.CodePage.ToString() + " : " + enc.WebName);
        //        }
        //        catch //(Exception ex)
        //        {
        //            //exceptionList.Add(ex);
        //        }
        //    }

        //    sw.Flush();
        //    sw.Close();
        //}

	}
}
