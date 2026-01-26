using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FASK.SledovaniVyroby.ModuleIfc;
using FASK.SledovaniVyroby.ErrorLog;
using Fask.Logging;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Forms
{
    public partial class FormIDPracovnikaLogin : Form, IModuleConnector
    {

        frmMain main = null;
        public string uzivatel = string.Empty;

        #region drzeni informaci o prihlasenem uzivateli
        //const ??
        public int loginID = -1;
        public object loginID_object = new object();
        #endregion

        #region Povinne veci okna

        //Ikona oznameni
        private NotifyIcon notifyIconState;
        public NotifyIcon NotifyIconState
        {
            get { return notifyIconState; }
            set { notifyIconState = value; }
        }

        //Status label modulu
        private ToolStripStatusLabel statusLabel = null;
        public ToolStripStatusLabel StatusLabel
        {
            set { statusLabel = value; }
        }

        public void ReturnPortsToPreviousState()
        {
            //throw new NotImplementedException();
        }
        public void SetMain()
        {
            main = null;
        }

        public void ClosePorts()
        {
            //throw new NotImplementedException();
        }

        public bool IsReadyToClose(out string message)
        {
            message = string.Empty;


            if (main == null)
                return true;

            if (main.isOpen())
            {
                
                message = string.Format("Uzivatel je prihlasen! LoginID: {0} ", loginID); 
                return false;
            }
            else
            {
                main.Dispose();
                main = null;
                return true;
            }
        }
        #endregion


        public FormIDPracovnikaLogin()
        {
            InitializeComponent();
        }

        private void FormIDPracovnikaLogin_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            textBoxID_Activate();
        }

        private void textBoxID_Activate()
        {
            textBoxID.SelectAll();
            textBoxID.Focus();
        }


        public void PerformOK()
        {

            try
            {


                //Overit uzivatele

                if (this.textBoxID.Text.Trim().Length == 0)
                {
                    this.textBoxID.Focus();
                    this.textBoxID.SelectAll();
                    throw new Exception("Není zadáno ID pracovníka");
                }
                else
                {
                    loginID = int.Parse( this.textBoxID.Text.Trim());
                }

                if (this.textBoxHeslo.Text.Trim().Length == 0)
                {
                    this.textBoxHeslo.Focus();
                    this.textBoxHeslo.SelectAll();
                    throw new Exception("Není zadáno heslo");
                }

                #region MyRegion

                string SqlConnectionStringLocal = Configuration.AgroSledovaniVozikuConfig.config.CS[0].DB_Local;
                //string SqlConnectionStringLocal = @"d:\_w\MES-Projekt\0x_Zdrojove_Kody\SledovaniVyroby2\!Build!\Vyroba\Debug\SQLCECommLib.dll";

                //string SqlConnectionStringLocal = @"d:\_w\MES-Projekt\0x_Zdrojove_Kody\SledovaniVyroby2\!Build!\Vyroba\Debug\SQLCEDatabase\Vyroba.sdf";


                if (Database.Classes.Vyroba_Local.LogUser(textBoxID.Text.Trim(), textBoxHeslo.Text, SqlConnectionStringLocal))
                {
                    uzivatel = textBoxID.Text.Trim();
                    this.textBoxID.Text = string.Empty;
                    this.textBoxHeslo.Text = string.Empty;
                    //TODO yavolat form main

                   


                    #endregion

                    if (main == null)
                    {

                        //Log.Write(string.Format("Prihlaseni do modulu, uzivatel: {0}", uzivatel));

                        //string log_hlaska = string.Format("Vizualizace vykladka -- chybi zaznamy");
                        //ExceptionHandler2.Handle(log_hlaska, "Log_LV", "txt");
                        main = new frmMain();

                        lock (loginID_object)
                        {
                            main.LoginID = loginID;
                        }
                        main.Rodic = this;
                        main.MdiParent = this.MdiParent;
                        this.Hide();
                        main.Show();

                    }
                    else if (main.isOpen())
                    {
                        statusLabel.Text = "Okno je jiz otevøeno a nekorektne zavøeno!";
                    }
                    else
                    {
                        statusLabel.Text = "Hele, pokud tohle nastane seš BUH :D!";
                    }
                }
                else
                {
                    //DialogResult drErr = MessageBox.Show("Pøihlášení se nezdaøilo, ID: " + textBoxID.Text.Trim() + ", " + SqlConnectionStringLocal, "Výroba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    DialogResult drErr = MessageBox.Show("Pøihlášení se nezdaøilo, ID: " + textBoxID.Text.Trim() + ", " + "Jsou zadány správné pøihlašovací údaje?", "Výroba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        
                    this.textBoxID.Text = string.Empty;
                    this.textBoxHeslo.Text = string.Empty;
                    this.textBoxID.Focus();
                }




            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                textBoxID_Activate();
                return;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.PerformOK();
        }


        private void FormIDPracovnikaLogin_KeyDown(object sender, KeyEventArgs e)
        {
            Handle_KeyDown(sender, e);
        }

        public void Handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Enter)
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
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        public bool IsReadyToShow(out string message)
        {
            message = string.Empty;

            if (main == null)
                return true;

            if (main.isOpen())
            {
                message = "Okno je jiz otevøeno a nekorektne zavøeno!";
                return false;
            }
            else
            {
                main.Dispose();
                main = null;
                return true;
            }

        }

        private void FormIDPracovnikaLogin_Shown(object sender, EventArgs e)
        {
            #region vycteni verze knihovny RFID a porovnani
            string PathToFile;
            string DirPath; 
            try
            {
                DirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                PathToFile = Path.Combine(DirPath, "Symbol.RFID3.Host.dll");
                //logovani
                //ExceptionHandler2.Handle(PathToFile, "Log_RFID_file", "txt");

                // Get current assemblies
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(PathToFile);
                string verze = info.FileVersion;


                // Compare both versions
                if (verze == "1.5.6.2")
                {
                    // verze knihovny je spravna, melo by jet RFID!

                }
                else
                {
                    DialogResult dr = MessageBox.Show(string.Format("Špatná verze knihovny Symbol.RFID3.Host! Aktualni verze v instalaci: ({0}) Chcete pokracavota? Jinak bude aplikace ukoncena!", verze), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (dr == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        this.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                var dr = MessageBox.Show(string.Format("Špatná verze knihovny Symbol.RFID3.Host! Catch chyba! Chcete pokracavota? Jinak bude aplikace ukoncena!"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (dr == DialogResult.Yes)
                {

                }
                else
                {
                    this.Close();
                }

            }
            #endregion
        }
    }
}

