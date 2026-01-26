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
using FASK.SledovaniVyroby.Module.Vyroba_SV.Configuration;

namespace FASK.SledovaniVyroby.Module.Vyroba_SV.Forms
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

            if (AgroSledovaniVozikuConfig.ExistiFile())
            {
                string cestaKAdresari_SQLiteCommLib = (new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), "SQLiteCommLib.dll"))).LocalPath;
                string cestaKAdresari_SQLRemoteLib = (new Uri(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), "SQLRemoteLib.dll"))).LocalPath;


                AgroSledovaniVozikuConfig.config.DB_Config.AddDB_ConfigRow(
                    "automaticke ulozeni paleta;posledni paleta 1;posledni paleta 3",
                    50,
                    100,
                    "presun na vozik;presun na streckovacku",
                    20,
                    "presun na streckovacku;presun na tisk",
                    "vaha;vaha_chyba;presun na tisk",
                    100,
                    "presun na tisk;tisk_aplikovano;tisk_neaplikovano_NP;tisk_neaplikovano_FP;tisk_chyba",
                    100,
                    20
                    );

                AgroSledovaniVozikuConfig.config.RFID_1.AddRFID_1Row(false, "192.168.1.54", 5084, -1, -1, -1, -1, 300, 10);
                AgroSledovaniVozikuConfig.config.RFID_2.AddRFID_2Row(false, "192.168.1.56", 5084, -1, -1, -1, -1, 300, 10);

                AgroSledovaniVozikuConfig.config.ADAM_1.AddADAM_1Row(1025, "192.168.1.63", 1025, 350, 100, false, false, true);
                AgroSledovaniVozikuConfig.config.ADAM_2.AddADAM_2Row(1025, "192.168.1.63", 1025, 350, 100, false, false, true);
                AgroSledovaniVozikuConfig.config.ADAM_3.AddADAM_3Row(1025, "192.168.1.63", 1025, 350, 100, false, false, true);

                AgroSledovaniVozikuConfig.config.ADAM_1_DI.AddADAM_1_DIRow(-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);
                AgroSledovaniVozikuConfig.config.ADAM_2_DI.AddADAM_2_DIRow(-1, -1, -1, -1, -1, -1);
                AgroSledovaniVozikuConfig.config.ADAM_3_DI.AddADAM_3_DIRow(-1, -1, -1, -1, -1, -1);

                AgroSledovaniVozikuConfig.config.ADAM_1_DESCRIPTION.AddADAM_1_DESCRIPTIONRow(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false);
                AgroSledovaniVozikuConfig.config.ADAM_2_DESCRIPTION.AddADAM_2_DESCRIPTIONRow(string.Empty, "Vykladka", "Tiskni", "Vaha start", "Odjezd", "Vaha konec", "Vaha chyba", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false);
                AgroSledovaniVozikuConfig.config.ADAM_3_DESCRIPTION.AddADAM_3_DESCRIPTIONRow(string.Empty, "Vykladka", "Tiskni", "Vaha start", "Odjezd", "Vaha konec", "Vaha chyba", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false);

                AgroSledovaniVozikuConfig.config.Vykladka.AddVykladkaRow(5000, 5000, 5000, 5000, false, 3000, false, false, 36000000, 36000000, 36000000);

                AgroSledovaniVozikuConfig.config.Vizualizace.AddVizualizaceRow(false);

                AgroSledovaniVozikuConfig.config.RFID_sprava.AddRFID_spravaRow(1000,false,false,false,false, false, false);

                AgroSledovaniVozikuConfig.config.CS.AddCSRow(
                    cestaKAdresari_SQLiteCommLib,
                    cestaKAdresari_SQLRemoteLib
                    );



                //AgroSledovaniVozikuConfig.config.WEBAPI.AddWEBAPIRow(
                //    "MDox",
                //    "192.168.1.121:8080", // 192.168.1.69/MST_Win_Kom_Server_7_Dasenka                     
                //    "api",
                //    false,
                //    5000,
                //    false
                //    );


                AgroSledovaniVozikuConfig.config.WEBAPI_TISK.AddWEBAPI_TISKRow(
                    "MDox",
                    "10.11.10.60:56425", // 192.168.1.69/MST_Win_Kom_Server_7_Dasenka                     
                    "api",
                    false,
                    5000,
                    false,
                    false
                    );

                AgroSledovaniVozikuConfig.config.HesloDoKonfigurace.AddHesloDoKonfiguraceRow(string.Empty, string.Empty);


                //Ulozeni
                AgroSledovaniVozikuConfig.Save();
            }

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

