#define MULTI_START_TEST_ENABLED
#define DEBUG

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fask.Vyroba_P;
using System.Diagnostics;
using System.Threading;
using System.IO;
using JR.Utils.GUI.Forms;
using System.Runtime.InteropServices;
using Fask.Vyroba_P.Extensions;
using System.Net;
using System.Xml;
using Microsoft.Win32;
using Fask.Vyroba_P.ServerAccess;

namespace Fask.Vyroba_P.Forms
{
    public delegate void updateProgressBarDownloadDelegate(int ProgressPercentage, string Text);

    public partial class FormMain : Form
    {

        #region Parametry

        delegate void DelegateVoid();

        public static FormMain Instance_FormMain = null;
        public GlobalObject globalObject = new GlobalObject();

        public static Fask.Vyroba_P.Scanner.ScannerBase Scanner = null;

        private global::System.Threading.Timer timerDownload = null;
        private global::System.Threading.Timer timerUpload = null;

        private Object lockUpDownTest = new Object();

        public static bool _downloadInProgress = false;
        public static bool _uploadInProgress = false;
        //public static bool _downloadProductionInProgress = false;


        private Button btnUkolovani = null;
        private Button btnOdvadeni = null;
        private Button btnDotisk = null;
        private Button btnKorekce = null;
        private Button btnUdalosti = null;
        private Fask.Vyroba_P.ucDataViews.Historie.ucHistory ucHistory = null;
        private Fask.Vyroba_P.OtevreneProdukce.ucOpenedProduction ucOpenedProduction = null;


        private static Mutex m;
        System.Threading.Thread threadTimerDownload = null;
        System.Threading.Thread threadTimerUpload = null;

        private enum SynchronizationType
        {
            Souborove,
            Databazove
        }

        #endregion

        #region c'tor + eventy formu

        /// <summary>
        /// Konstruktor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();


            //MaR 24.10.2024 zmena kioskmodu

            //// Zakázat kiosk mode a zavřít aplikaci
            //DisableKioskMode();

            hideTaskbar();
           
            FlexibleMessageBox.FONT = this.menuStrip1.Font;
            /* pokus o upravu message boxu
             * Font font = this.menuStrip1.Font;
            float cislo = font.Size;
            FontStyle fontstyle = font.Style;
            fontstyle |= FontStyle.Bold;
            FlexibleMessageBox.FONT = new Font(font.FontFamily, 30, fontstyle);*/
        }

        /// <summary>
        /// Load formu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = Settings.ApplicationPosition;
            this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(Settings.ApplicationPosition);
            //this.WindowState = FormWindowState.Maximized;
            //this.WindowStyle = System.Windows.WindowStyle.None;
            //this.Bounds = Screen.PrimaryScreen.Bounds;

            //Screen.GetWorkingArea(Settings.ApplicationPosition);
            // skrytí horní lišty

            //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            //string pom = AppDomain.CurrentDomain.BaseDirectory;
            //MessageBox.Show("Current directory: " + Directory.GetCurrentDirectory());
            //MessageBox.Show("AppDomain: " + AppDomain.CurrentDomain.BaseDirectory);

            //Directory.SetCurrentDirectory(@"C:\\aaa\");

            m = new Mutex(true, "{AEDD1C36-CF9E-411b-8E9B-D4B2BCE1032A}_TID" + Settings.TerminalID);
            if (!m.WaitOne(TimeSpan.Zero, true))
            {
                FlexibleMessageBox.Show(this, "Pouze jedna instace aplikace !!!");
                //this.finalize();
                //this.Close();
                Application.Exit();
                return;
            }

            //#if MULTI_START_TEST_ENABLED
            //            Process[] procesy = Process.GetProcesses();
            //            string AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            //            AssemblyName += ".exe";

            //            int InstanceCount = 0;
            //            for (int i = 0; i < procesy.Length; i++)
            //            {
            //                if (procesy[i].ProcessName == AssemblyName)
            //                    InstanceCount++;
            //            }

            //            if (InstanceCount > 1)
            //            {
            //                FlexibleFlexibleMessageBox.Show("Aplikace je již spuštěna.", this.Text, MessageBoxButtons.OK,
            //                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            //                Application.Exit();
            //                return;
            //            }
            //#endif

            //Log.Enable = Settings.Loging;
            Fask.Logging.ExceptionHandler2.EnablePrintLogging = Settings.Loging;
            Fask.Logging.ExceptionHandler2.SetEnablePrint(Settings.Loging);
            // testovani, zdali byl PC uspan
            try
            {
                SystemEvents.PowerModeChanged += OnPowerChange;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "PowerModeChanged error (main)");
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

            FormMain.Instance_FormMain = this;

            #region Certifikat

            Globals.ServerAccessCertificateTrust = Settings.ServerAccessCertificateTrust;

            #endregion

            if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD)))
            {
                // TODO : TaD dodelat stahovani production.prd z serveru platnou verzi tu i terminal
                DownloadProductionStart();

                if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD)))
                {
                    File.Copy(
                                Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP),
                                Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD), false
                                );
                }
            }

            if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD)))
            {
                // TODO : TaD dodelat stahovani internalstate.sdf z serveru platnou verzi tu i terminal
                DownloadInternalStateStart();

                if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD)))
                {
                    File.Copy(
                                Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD + Constants.TMP),
                                Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD), false
                              );
                }
            }




            // kopie Production DB, pokud neexistuje
            if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP)))
            {
                File.Copy(
                            Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD),
                            Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP), false
                            );
            }

            // kopie ProductionHist DB, pokud neexistuje
            if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionHist + Constants.PRD)))
            {
                File.Copy(
                            Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD),
                            Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionHist + Constants.PRD), false
                            );
            }

           

            switch (Settings.ScannerType)
            {
                case Fask.Vyroba_P.Scanner.ScannerTypes.COM:
                    Scanner = new Fask.Vyroba_P.Scanner.ScannerCOM();
                    break;
                case Fask.Vyroba_P.Scanner.ScannerTypes.None:
                default:
                    Scanner = new Fask.Vyroba_P.Scanner.ScannerNone();
                    break;
            }



            timerUpload = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(timerUploadCallBack), this, Settings.TimerUploadInterval, Settings.TimerUploadInterval);

            //if (!Settings.DavkoveZpracovani)
            timerDownload = new global::System.Threading.Timer(new global::System.Threading.TimerCallback(timerDownloadCallBack), this, 2000, Settings.TimerDownloadInterval);

            timerShown.Enabled = true;

            timerDownloadThreadStart();

            // načtení zvolených modulů
            LoadModules();



            //MaR 24.10.2024 TODO rozhodovani o kioskmod - zap/vyp
            // Zakázat kiosk mode
            if (Settings.chB_system_kiskmod)
            {
                EnableKioskMode();
            }
            else
            {
                DisableKioskMode();

            }
        }

        /// <summary>
        /// Shown formu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Shown(object sender, EventArgs e)
        {
            timerShown.Enabled = false;

            //ucTime.Width = Settings.FormMainTimeFormatWidth;
            ucTime.Left = ucTime.Right - Settings.FormMainTimeFormatWidth;
            ucTime.Width = Settings.FormMainTimeFormatWidth;

            this.UpdateForm();

            LogIn();

            try
            {
                this.Show();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Closing formu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!this.LogOut(true))
                {
                    e.Cancel = true;
                    return;
                }

                #region MaR 2.9.2024 ukonceni aolikace bez hesla


                if (!Settings.LogOut_bezHesla)
                {
                    using (FormInputKod frmInputKod = new FormInputKod())
                    {
                        frmInputKod.Text = "Zadejte heslo pro ukončení";
                        frmInputKod.ShowKod = false;
                        frmInputKod.Owner = this;
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FormMain - ukončení aplikace: " + "Pokus o ukončení aplikace");
                        if (frmInputKod.ShowDialog(this) == DialogResult.OK)
                        {
                            try
                            {
                                // porovnání hesla s DB
                                bool result = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.ApplicationEndPass(Settings.TerminalID, frmInputKod.Kod.Trim());

                                if (!result)
                                {
                                    FlexibleMessageBox.Show(this, "Špatné heslo", "Chyba", MessageBoxButtons.OK);
                                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FormMain - ukončení aplikace uživatelem" + "Špatné heslo");
                                    e.Cancel = true;
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                                // TODO : dokud nebude aktualni server, tak umoznit kilnout aplikaci kodem 159
                                if (frmInputKod.Kod != "159")
                                {
                                    e.Cancel = true;
                                    //e.Cancel = true;
                                    return;
                                }
                                //e.Cancel = true;
                                //return;
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }  
                }

                #endregion





                if (Settings.ModulPovolitPrehledOdvodu && ucHistory != null)
                    ucHistory.DataGridView1.SaveConfiguration(this.GetType().ToString());

                if (Settings.ModulPovolitNedokonceneZakazky && ucOpenedProduction != null)
                    ucOpenedProduction.DataGridView1.SaveConfiguration(this.GetType().ToString() + "OpenedProduction");

                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "application stop");

                showTaskbar();
            }
            else if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
            }
            else
            {
                showTaskbar();
            }

            DisableKioskMode();

            //if (!PerformClose(e.CloseReason == CloseReason.UserClosing))
            //    e.Cancel = true;
            //showTaskbar();
            this.finalize();
        }

        /// <summary>
        /// KeyDown formu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (Settings.UEventSmenaEnabled)
                    {
                        //this.PerformClose(true);
                        //this.LogOut(true);
                        //this.BeginInvoke((MethodInvoker)delegate() { LogIn(); });
                        this.menuItemOdhlasit_Click(null, null);
                    }
                    else
                    {
                        this.PerformClose(true);
                    }
                }
                else if (Settings.ModulPovolitOdvadeni && (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)) //Odvod
                {
                    PerfomOdvadeni();
                }
                else if (Settings.ModulPovolitKorekce && (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)) //Korekce
                {
                    PerformKorekce();
                }
                else if (Settings.ModulPovolitUdalosti && (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)) //Udalosti
                {
                    PerfomUdalosti();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        #endregion

        #region kioskmod 24.10.2024
        #region old
        // pokus o skrytí taskbaru
        //[DllImport("user32.dll")]
        //private static extern int FindWindow(string className, string windowText);
        //[DllImport("user32.dll")]
        //private static extern int ShowWindow(int hwnd, int command);
        ////[DllImport("user32.dll")]
        ////private static extern bool EnableWindow(int hwnd, bool enable);
        //protected static int HandleTaskbar
        //{
        //    get
        //    {
        //        return FindWindow("Shell_TrayWnd", "");
        //    }
        //} 
        #endregion

        public static void showTaskbar()
        {
            #region old kioskmod vypnuti MaR 24.10 2024
            // ShowWindow(HandleTaskbar, 1); 
            #endregion

            //EnableWindow(HandleTaskbar, true);
        }


        // API pro manipulaci s taskbarem
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string windowText);

        const uint SW_HIDE = 0;
        const uint SW_SHOW = 5;


        // Povolit kiosk mode
        private void EnableKioskMode()
        {
            // Skrytí taskbaru
            IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);
            ShowWindow(taskbarHandle, SW_HIDE);

            // Nastavení aplikace na celou obrazovku
            this.FormBorderStyle = FormBorderStyle.None; // Skrytí okraje
            this.WindowState = FormWindowState.Maximized; // Maximalizace formuláře
           // this.TopMost = true; // Aplikace bude vždy na vrcholu

            // Zakázat klávesové zkratky (Alt + Tab, Ctrl + Alt + Del není možné z C# omezit, ale lze použít skupinovou politiku Windows)
        }

        // Zakázat kiosk mode
        private void DisableKioskMode()
        {
            // Zobrazit taskbar
            IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);
            ShowWindow(taskbarHandle, SW_SHOW);

            // Obnovit normální zobrazení aplikace
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            this.TopMost = false; // Aplikace už nebude na vrcholu
        }


        #endregion


        #region Stahovani a Odesilani

        #region Odesilani

        /// <summary>
        /// Metoda nalikovana do eventu tlačitka Odeslani dat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void odeslatDataVyrobyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerUploadCallBack(null);
        }

        /// <summary>
        /// CallBack v timeru pro odeslani dat
        /// </summary>
        /// <param name="state"></param>
        private void timerUploadCallBack(object state)
        {
            try
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload: Upload start");

                if (_uploadInProgress)
                {
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload: Upload in progress - retunrs");

                    return;
                }

                //Nelze zacit nahravat data, pokud bezi stahovani, posune se za 2 sec...
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload:Timer Upload stops");
                timerUploadStop();
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload:LockUpDown lock");
                lock (lockUpDownTest)
                {
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload:Lock acquired");
                    if (_downloadInProgress)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload:Download in progress");

                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload:Timer upload restart at 2s");
                        timerUploadStart(2000);
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload:Upload returns - next in 2s");
                        return;
                    }
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload:uploadinprogres = true");
                    _uploadInProgress = true;
                }

                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload:Zobrazeni v UI zpravy - odesilaji se data vyroby ...");
                this.BeginInvoke(new DelegateUpdateStatusBarInfo(UpdateStatusBarInfo), new object[] { "Odesílají se data výroby: " + DateTime.Now.ToString("d.M.yyyy H:mm.ss") });

                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload:New thread upload");
                //timerUploadThreadStart();
                threadTimerUpload = new Thread(new ThreadStart(timerUploadThreadStart_z_VyrobaW));
                threadTimerUpload.Name = "threadTimerUpload_" + DateTime.Now.TimeOfDay.ToString(); // pojmenovani vlakna
                threadTimerUpload.IsBackground = true;
                threadTimerUpload.Priority = ThreadPriority.Lowest;
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload:Upload thread start");
                threadTimerUpload.Start();
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload:Upload thread started");

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Metoda pro odeslani dat, v jinem vlakne
        /// </summary>
        //private void timerUploadThreadStart()
        //{
        //    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload - Thread: thread upload start");

        //    try
        //    {
        //        #region kontrola existence Production.prd.tmp

        //        try
        //        {
        //            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload - Thread:Test production tmp");
        //            // pokud tmp Production neexistuje, dojde k jeho prekopirovani
        //            if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP)))
        //            {
        //                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload - Thread:Production tmp not exist => copy");
        //                File.Copy(
        //                    Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD),
        //                    Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP),
        //                    true
        //                    );
        //            }
        //            else
        //            {
        //                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload - Thread:Productin tmp EXISTS !!!");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //            throw ex;
        //        } 

        //        #endregion

        //        // naplnění Productiontmp daty
        //        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Priprava production k odeslani - start");

        //        #region Production.prd.tmp naplneni datama z Tabulek Production a UserEvents

        //        //Smazani celej tabulky Production v souboru Production.prd.tmp
        //        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRDTMP.Delete_Production();


        //        Fask.SQLiteDBs.DataSets.Vyroba ds = new Fask.SQLiteDBs.DataSets.Vyroba();
        //        Fask.SQLiteDBs.DataSets.Vyroba dsTmp = new Fask.SQLiteDBs.DataSets.Vyroba();

        //        //Dotaženi všech dat z Production v souboru Production.prd
        //        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Fill_Production(ds.Production);

        //        foreach (var item in ds.Production)
        //        {
        //            item.SetAdded();
        //            dsTmp.Production.ImportRow(item);
        //        }

        //        //importnuti Dotažene data tabulky Production z Production.prd do Production.prd.tmp
        //        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRDTMP.Update_Production(ds.Production);

        //        dsTmp.AcceptChanges();

        //        // delete userevents tabulky
        //        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRDTMP.Delete_UserEvents();


        //        // dotaženi UserEvents z Producion.prd
        //        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Fill_UserEvents(ds.UserEvents);

        //        foreach (var item in ds.UserEvents)
        //        {
        //            item.SetAdded();
        //            dsTmp.UserEvents.ImportRow(item);
        //        }

        //        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRDTMP.Update_UserEvents(dsTmp.UserEvents);
                
        //        dsTmp.AcceptChanges();
                
        //        #endregion

        //        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Priprava production k odeslani - konec");


        //        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Odeslani a zpracovani produkce online - start");

        //        string filename = Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP);

        //        Guid guid = Guid.NewGuid();

        //        string URL = Settings.WebServiceAddressVyroba  + "Upload.aspx";
        //        string Odkud = filename + Constants.ZIP;
        //        string Kam = Settings.TerminalID + "\\" + Constants.Production + "_" + guid.ToString("N") + Constants.ZIP;

        //        Fask.Vyroba_P.FileTransfer.CompressFile.CompressToZip(filename, Odkud);

        //        if (Fask.Vyroba_P.FileTransfer.Uploading.SendFileCalcTime(URL, Odkud, Kam) != "OK")
        //            throw new Exception("Nepodařilo se odeslat davku");

        //        bool processed = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.ProcessProductionData2(Settings.TerminalID, guid);

        //        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Odeslani a zpracovani produkce online - konec");

        //        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Odeslani a zpracovani : processed=" + processed.ToString());
        //        if (processed)
        //        {
        //            //Odmazani odvedenych dat z production

        //            #region OLD odmazani Production pomomoci GUID

        //            //System.Data.SqlServerCe.SqlCeCommand sqlproductiontmp = null;
        //            //System.Data.SqlServerCe.SqlCeCommand sqlproduction = null;
        //            //try
        //            //{
        //            //    sqlproductiontmp = new System.Data.SqlServerCe.SqlCeCommand(
        //            //        "select guid from production",
        //            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP))
        //            //        );

        //            //    sqlproduction = new System.Data.SqlServerCe.SqlCeCommand(
        //            //        "Delete from production where guid=@guid",
        //            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD))
        //            //        );

        //            //    System.Data.SqlServerCe.SqlCeParameter sqlparamguid = sqlproduction.Parameters.Add("@guid", SqlDbType.UniqueIdentifier);
        //            //    sqlparamguid.Direction = ParameterDirection.Input;

        //            //    sqlproductiontmp.Connection.Open();
        //            //    sqlproduction.Connection.Open();

        //            //    System.Data.SqlServerCe.SqlCeDataReader preader = sqlproductiontmp.ExecuteReader();

        //            //    while (preader.Read())
        //            //    {
        //            //        sqlparamguid.Value = preader[0];
        //            //        int raff = sqlproduction.ExecuteNonQuery();
        //            //    }
        //            //    sqlproductiontmp.Connection.Close();
        //            //    sqlproduction.Connection.Close();
        //            //}
        //            //catch (Exception ex)
        //            //{
        //            //    if (sqlproductiontmp != null && sqlproductiontmp.Connection.State == ConnectionState.Open)
        //            //        sqlproductiontmp.Connection.Close();
        //            //    if (sqlproduction != null && sqlproduction.Connection.State == ConnectionState.Open)
        //            //        sqlproduction.Connection.Close();

        //            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //            //}

        //            #endregion

        //            FormMain.Instance_FormMain.globalObject.DeleteProductionByGuid();

        //            //Odmazani odvedenych dat z userevents
        //            #region OLD odmazani UserEvents pomomoci GUID 

        //            //System.Data.SqlServerCe.SqlCeCommand sqlusereventstmp = null;
        //            //System.Data.SqlServerCe.SqlCeCommand sqluserevents = null;
        //            //try
        //            //{
        //            //    sqlusereventstmp = new System.Data.SqlServerCe.SqlCeCommand(
        //            //        "select guid from userevents",
        //            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP))
        //            //        );
        //            //    sqluserevents = new System.Data.SqlServerCe.SqlCeCommand(
        //            //        "Delete from userevents where guid=@guid",
        //            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD))
        //            //        );
        //            //    System.Data.SqlServerCe.SqlCeParameter sqlparamguid = sqluserevents.Parameters.Add("@guid", SqlDbType.UniqueIdentifier);
        //            //    sqlparamguid.Direction = ParameterDirection.Input;

        //            //    sqlusereventstmp.Connection.Open();
        //            //    sqluserevents.Connection.Open();
        //            //    System.Data.SqlServerCe.SqlCeDataReader preader = sqlusereventstmp.ExecuteReader();
        //            //    while (preader.Read())
        //            //    {
        //            //        sqlparamguid.Value = preader[0];
        //            //        int raff = sqluserevents.ExecuteNonQuery();
        //            //    }
        //            //    sqlusereventstmp.Connection.Close();
        //            //    sqluserevents.Connection.Close();
        //            //}
        //            //catch (Exception ex)
        //            //{
        //            //    if (sqlusereventstmp != null && sqlusereventstmp.Connection.State == ConnectionState.Open)
        //            //        sqlusereventstmp.Connection.Close();
        //            //    if (sqluserevents != null && sqluserevents.Connection.State == ConnectionState.Open)
        //            //        sqluserevents.Connection.Close();
        //            //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //            //}

        //            #endregion

        //            FormMain.Instance_FormMain.globalObject.DeleteUserEventsByGuid();
        //        }

        //        //Po uspesnem provedeni to smazu
        //        //File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdfTmp));
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //    }
        //    finally
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Finalizace odesilaciho procesu");

        //        try
        //        {
        //            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "_uploadinprogres=false");
        //            _uploadInProgress = false;
        //        }
        //        catch { }
        //        try
        //        {
        //            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload - Thread:Zobrazeni zpravy o odeslani");
        //            int? countProduction = null;

        //            #region OLD puvodne dotahovani počtu z DB

        //            ////5.10.2016 JiS - zobrazeni poctu zaznamu v production po odeslani dat...
        //            //System.Data.SqlServerCe.SqlCeCommand sqlproductiontmp = null;
        //            //try
        //            //{
        //            //    sqlproductiontmp = new System.Data.SqlServerCe.SqlCeCommand(
        //            //        "select count(*) from production",
        //            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD))
        //            //        );
        //            //    sqlproductiontmp.Connection.Open();
        //            //    countProduction = (int)sqlproductiontmp.ExecuteScalar();
        //            //}
        //            //catch
        //            //{
        //            //}
        //            //finally
        //            //{
        //            //    if ((sqlproductiontmp != null) && ((sqlproductiontmp.Connection.State & ConnectionState.Open) == ConnectionState.Open))
        //            //    {
        //            //        sqlproductiontmp.Connection.Close();
        //            //    }
        //            //}

        //            #endregion

        //            countProduction = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetCount_Production();

        //            this.BeginInvoke(new DelegateUpdateStatusBarInfo(UpdateStatusBarInfo), new object[] { "Data odeslána: " + DateTime.Now.ToString("d.M.yyyy H:mm.ss") + " [" + (countProduction.HasValue ? countProduction.Value.ToString() : "?") + "]" });
        //        }
        //        catch { }
        //        //timerUploadStart();
        //        try
        //        {
        //            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "Production Upload - Thread:Znovu spusteni cekaciho timeru na upload");
        //            this.BeginInvoke((MethodInvoker)delegate () { timerUploadStart(); });
        //        }
        //        catch { }
        //    }
        //}


        private void timerUploadThreadStart_z_VyrobaW()
        {
            try
            {
                // delete all resistent "Production_[.*].prd.tmp files
                //FileInfo fi = new FileInfo(MySystem.MyPath.DataDirectory);
                var resistentFiles = Directory.GetFiles(MySystem.MyPath.DataDirectory, "Production_*.prd.tmp");
                foreach (var resFile in resistentFiles)
                {
                    File.Delete(resFile);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "timerUploadThreadStart() removing resistent old Production_*.prd.tmp files");
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            Guid guid = Guid.NewGuid();
            string filename = Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + "_" + guid.ToString("N") + Constants.PRD + Constants.TMP);

            try
            {
                try
                {
                    File.Copy(
                        Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionPrd),
                        Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + "_" + guid.ToString("N") + Constants.PRD + Constants.TMP),
                        true
                        );
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "timerUploadThreadStart(object state):File.Copy");
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    throw ex;
                }

                string Odkud = filename + Constants.ZIP;
                string Kam = Settings.TerminalID + "\\" + Constants.Production + "_" + guid.ToString("N") + Constants.PRD + Constants.TMP + Constants.ZIP;

                Fask.Vyroba_P.FileTransfer.CompressFile.CompressToZip(filename, Odkud);

                if (Fask.Vyroba_P.FileTransfer.Uploading.SendFile_API(Odkud, Kam) != "OK")
                    throw new Exception("Nepodařilo se odeslat davku");

                //if (Fask.Vyroba_P.FileTransfer.Uploading.SendFileCalcTime(URL, Odkud, Kam) != "OK")
                //    throw new Exception("Nepodařilo se odeslat davku");

                bool processed = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.ProcessProductionData2(
                    Settings.TerminalID,
                    guid
                    );


                //bool processed = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.EndProcessProductionData2(ares);
                if (processed)
                {

                    //Odmazani odvedenych dat z Production                    
                    FormMain.Instance_FormMain.globalObject.DeleteProductionByGuid(filename);

                    //Odmazani odvedenych dat z Production_SN
                    FormMain.Instance_FormMain.globalObject.DeleteProduction_SNByGuid(filename);

                    //Odmazani odvedenych dat z Production_Sources
                    FormMain.Instance_FormMain.globalObject.DeleteProduction_SourcesByGuid(filename);

                    //Odmazani odvedenych dat z UserEvents
                    FormMain.Instance_FormMain.globalObject.DeleteUserEventsByGuid(filename);
                }

                this.UpdateStatusBarInfo("Data odeslána: " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                try
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex.Message, "timerUploadThreadStart End");
                    this.UpdateStatusBarInfo("Odeslání dat se nezdařilo: " + DateTime.Now.ToString() + ex.Message);
                }
                catch { }
            }
            finally
            {

                //Vzdy ho smazu, protoze uz tento tmp neni dulezity, vsechno se provedlo a uz k nemu nikdy nepristoupim...
                try { File.Delete(filename); }
                catch (Exception exDeleteFile)
                {
                    Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Error, "timerUploadThreadStart Delete '{" + filename + "}' file");
                    Fask.Logging.ExceptionHandler2.Handle(exDeleteFile);
                }

                try { _uploadInProgress = false; }
                catch { }
                //timerUploadStart();
                try { this.BeginInvoke(new DelegateVoid(timerUploadStart)); }
                catch { }
            }
        }

        #endregion

        #region Stahovani

        /// <summary>
        /// Metoda nalikovana do eventu tlačitka stažení dat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemDownloadDatabase_Click(object sender, EventArgs e)
        {
            timerDownloadCallBack(null);
        }

        /// <summary>
        /// CallBack v timeru pro stažení dat
        /// </summary>
        /// <param name="state"></param>
        private void timerDownloadCallBack(object state)
        {
            //#warning DEBUG is enable Download is disable by TaD
            //#if (DEBUG)
            if (_downloadInProgress)
            {
                return;
            }

            //Nelze zacit stahovat, pokud bezi nahravani dat, posune se o 2 sec...
            timerDownloadStop();
            lock (lockUpDownTest)
            {
                if (_uploadInProgress)
                {
                    timerDownloadStart(2000);
                    return;
                }
                _downloadInProgress = true;
            }

            this.BeginInvoke(
                (MethodInvoker)delegate () { UpdateStatusBarInfo("Aktualizace: " + DateTime.Now.ToString("d.M.yyyy H:mm.ss")); });

            //timerDownloadThreadStart();
            threadTimerDownload = new Thread(new ThreadStart(timerDownloadThreadStart));
            threadTimerDownload.Name = "threadTimerDownload_" + DateTime.Now.TimeOfDay.ToString();
            threadTimerDownload.IsBackground = true;
            threadTimerDownload.Priority = ThreadPriority.Lowest;
            threadTimerDownload.Start();
            //#endif
        }

        public void AktualizaceDat()
        {
            // okamžité nactení dat
            timerDownloadCallBack(null);
        }

        /// <summary>
        /// Metoda pro odeslani dat, v jinem vlakne
        /// </summary>
        private void timerDownloadThreadStart()
        {
            try
            {
                this.UpdateStatusBarInfo("Aktualizace: Příprava lokálního prostoru");

                if (File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Vyroba + Constants.PRD + Constants.TMP)))
                {
                    try
                    {
                        File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Vyroba + Constants.PRD + Constants.TMP));
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    }
                }

                //Settings.LastDownload = DateTime.Now;

                this.UpdateStatusBarInfo("Aktualizace: Příprava databáze na serveru");

                //Datum a cas posledniho downloadu predlohy
                DateTime lastDownload = DateTime.Now;

                bool remotefilename = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.VyrobaDBDateTimePrepareZip(Settings.TerminalID, lastDownload);
                //Constants.VyrobaCESdfTmp = remotefilename;
                //byte[] data = _wsvyroba.VyrobaDBDateTime(Settings.TerminalID, Settings.LastDownload);
                if (!remotefilename)
                    throw new Exception("Prepare data on server was not succesfull");


                updateProgressBarDownloadDelegate callback = updateProgressBarDownload;

                string downloaded = FileTransfer.Downloading.DownloadFileFromServer(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Vyroba + Constants.PRD), false, true, callback);
                if (downloaded != "OK")
                    throw new Exception("Download from server was not succesfull", new Exception(downloaded));


                this.UpdateStatusBarInfo("Aktualizace: Synchronizace odvedených kusů");
                //Pokud existuje production tmp, tak doplnit aktualni stav
                if (File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP)))
                {
                    //Dohleda vsechny zmeny k datu posledniho downloadu...???
                    #region OLD puvodne dotaženi dat do NEtypovehoDatasetu... fuj...

                    //DataTable dt = new DataTable();
                    //System.Data.SqlServerCe.SqlCeDataAdapter sqlceda = new System.Data.SqlServerCe.SqlCeDataAdapter(
                    //    "SELECT" +
                    //    "  CountEntries," +
                    //    " SOPNUMBE," +
                    //    " ORD," +
                    //    " ITEMNMBR," +
                    //    " ITEMTYPE," +
                    //    " QTYPACK," +
                    //    " SUM(qty) AS QTYODVEDENO," +
                    //    " Count(*) AS CNTODVEDENO," +
                    //    " MAX(dateeve) as LSTMod " +
                    //    "FROM Production " +
                    //    "WHERE (dateeve > @dateeve) " +
                    //    "GROUP BY CountEntries, SOPNUMBE, ORD, ITEMNMBR, ITEMTYPE, QTYPACK",
                    //    "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD + Constants.TMP));
                    //sqlceda.SelectCommand.Parameters.Add("@dateeve", SqlDbType.DateTime).Value = lastDownload;
                    //sqlceda.SelectCommand.Connection.Open();
                    //sqlceda.Fill(dt);
                    //sqlceda.SelectCommand.Connection.Close();

                    #endregion

                    var dt = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.GetProductionUpdateByDateEve_Production(lastDownload);

                    //aktualizace predlohy na zaklade aktualniho stavu

                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Connection_Open();

                    foreach (SQLiteDBs.DataSets.Vyroba.Production_updateRow row in dt)
                    {
                        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.UpdateQtyCntOdvedenoLSTMod_CZPRO_VPP(row.QTYODVEDENO, row.CNTODVEDENO,row.LSTMod, row.CountEntries, row.SOPNUMBE, row.ORD, row.ITEMNMBR, row.ITEMTYPE, row.QTYPACK, row.BarcodeP);
                    }

                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRDTMP.Connection_Close();
                }

                this.BeginInvoke((MethodInvoker)delegate () { SynchronizeDatabases(SynchronizationType.Souborove); });

                Settings.LastDownload = lastDownload; //Last success download time
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                _downloadInProgress = false;
                timerDownloadStart();
                fileDownload_ProgressChanged(null, new ProgressChangedEventArgs(0, null));
                this.BeginInvoke(new DelegateUpdateForm(UpdateForm));
            }
        }

        #endregion

        #region Metody pro praci s Timerama

        #region Odesilani

        private void timerUploadStart(int nextrunmiliseconds)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate () { timerUploadStart(nextrunmiliseconds); });
                return;
            }

            timerUpload.Change(nextrunmiliseconds, Settings.TimerUploadInterval);
        }

        private void timerUploadStart()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate () { timerUploadStart(); });
                return;
            }

            timerUploadStart(Settings.TimerUploadInterval);
        }

        private void timerUploadStop()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate () { timerUploadStop(); });
                return;
            }

            timerUpload.Change(Timeout.Infinite, Timeout.Infinite);
        }

        #endregion

        #region Stahovani

        private void timerDownloadStart(int nextrunmiliseconds)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate () { timerDownloadStart(nextrunmiliseconds); });
                return;
            }

            timerDownload.Change(nextrunmiliseconds, Settings.TimerDownloadInterval);
        }

        private void timerDownloadStart()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate () { timerDownloadStart(); });
                return;
            }

            timerDownloadStart(Settings.TimerDownloadInterval);
        }

        private void timerDownloadStop()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate () { timerDownloadStop(); });
                return;
            }

            timerDownload.Change(Timeout.Infinite, Timeout.Infinite);
        }

        #endregion

        #endregion

        #region Synchronizace

        private void SynchronizeDatabases(SynchronizationType synchType)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate () { SynchronizeDatabases(synchType); });
                    return;
                }

                // Souborove ... 
                SynchronizationType typS = synchType;

                this.UpdateStatusBarInfo("Aktualizace: Synchronizace databáze (" + typS.ToString() + ")");
                Application.DoEvents();

                if (typS == SynchronizationType.Souborove)
                {
                    File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Vyroba + Constants.PRD));

                    File.Copy(
                        Path.Combine(MySystem.MyPath.DataDirectory, Constants.Vyroba + Constants.PRD + Constants.TMP),
                        Path.Combine(MySystem.MyPath.DataDirectory, Constants.Vyroba + Constants.PRD),
                        true
                        );

                    File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Vyroba + Constants.PRD + Constants.TMP));

                    this.UpdateStatusBarInfo("Aktualizace: Database open/close");
                    Application.DoEvents();



                    //System.Data.SqlServerCe.SqlCeConnection sconn = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));
                    //sconn.Open();
                    //sconn.Close();
                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.Void_Command(); // ale asi nechapu vyznam otevřeni a hned zavřeni komunikace...
                }
                else if (typS == SynchronizationType.Databazove)
                {
                    //Databazove se stejne nepouživa.....

                    //if (!File.Exists(Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD)))
                    //{
                    //    File.Copy(
                    //        Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD + Constants.TMP),
                    //        Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD),
                    //        true
                    //        );
                    //}
                    //else
                    //{
                    //    System.Data.SqlServerCe.SqlCeConnection sconn_src = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD + Constants.TMP));
                    //    System.Data.SqlServerCe.SqlCeCommand scomm_src = new System.Data.SqlServerCe.SqlCeCommand(
                    //        "",
                    //        sconn_src
                    //        );
                    //    scomm_src.CommandType = CommandType.TableDirect;

                    //    System.Data.SqlServerCe.SqlCeConnection sconn_dst = new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));
                    //    System.Data.SqlServerCe.SqlCeCommand scomm_dst = new System.Data.SqlServerCe.SqlCeCommand(
                    //        "",
                    //        sconn_dst
                    //        );
                    //    scomm_dst.CommandType = CommandType.TableDirect;

                    //    sconn_src.Open();
                    //    sconn_dst.Open();

                    //    List<string> tablenames = new List<string>() { "CZPRO_VPH", "CZPRO_VPP", "Logins", "Machines", "Corrects", "StatusTypes" };

                    //    foreach (var item in tablenames)
                    //    {
                    //        System.Data.SqlServerCe.SqlCeCommand delcomm = sconn_dst.CreateCommand();
                    //        delcomm.CommandText = "delete " + item;
                    //        int rowsaff = delcomm.ExecuteNonQuery();

                    //        scomm_dst.CommandText = scomm_src.CommandText = item;



                    //        System.Data.SqlServerCe.SqlCeResultSet sresset_src = scomm_src.ExecuteResultSet(System.Data.SqlServerCe.ResultSetOptions.None);
                    //        System.Data.SqlServerCe.SqlCeResultSet sresset_dst = scomm_dst.ExecuteResultSet(System.Data.SqlServerCe.ResultSetOptions.Updatable);
                    //        DataTable dt_schema_src = sresset_src.GetSchemaTable();
                    //        while (sresset_src.Read())
                    //        {
                    //            System.Data.SqlServerCe.SqlCeUpdatableRecord nrecord = sresset_dst.CreateRecord();
                    //            for (int i = 0; i < sresset_src.FieldCount; i++)
                    //            {
                    //                //if ((sresset_src.GetName(i) == "DEX_ROW_ID"))
                    //                if (((string)dt_schema_src.Rows[i]["ColumnName"] == sresset_src.GetName(i))
                    //                    && (bool)(dt_schema_src.Rows[i]["IsAutoIncrement"])
                    //                    )
                    //                {
                    //                    ;
                    //                }
                    //                else
                    //                {
                    //                    nrecord[sresset_src.GetName(i)] = sresset_src[i];
                    //                }
                    //            }
                    //            sresset_dst.Insert(nrecord, System.Data.SqlServerCe.DbInsertOptions.PositionOnInsertedRow);
                    //        }
                    //    }

                    //    sconn_dst.Close();
                    //    sconn_src.Close();
                    //}

                    //File.Delete(Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD + Constants.TMP));
                }
                else
                { //???
                }

                UpdateStatusBarInfo();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }


        #endregion

        #endregion

        #region MenuItem Click eventy

        private void mi_Konec_Click(object sender, EventArgs e)
        {
            this.PerformClose(true);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (AboutBox abox = new AboutBox())
            {
                abox.ShowDialog();
            }



        }





        #endregion



        /// <summary>
        /// Načtení veškerých modulů do table layout panelu.
        /// </summary>
        private void LoadModules()
        {
            try
            {
                // odstranění tlačítek při refreshi
                if (btnOdvadeni != null)
                    tableLayoutPanel1.Controls.Remove(btnOdvadeni);
                if (btnKorekce != null)
                    tableLayoutPanel1.Controls.Remove(btnKorekce);
                if (btnUdalosti != null)
                    tableLayoutPanel1.Controls.Remove(btnUdalosti);
                if (ucHistory != null)
                    tableLayoutPanel1.Controls.Remove(ucHistory);
                if (ucOpenedProduction != null)
                    tableLayoutPanel1.Controls.Remove(ucOpenedProduction);
                if (btnUkolovani != null)
                    tableLayoutPanel1.Controls.Remove(btnUkolovani);
                if (btnDotisk != null)
                    tableLayoutPanel1.Controls.Remove(btnDotisk);

                this.tableLayoutPanel1.RowCount = Settings.ModulPocetRadku;
                this.tableLayoutPanel1.ColumnCount = Settings.ModulPocetSloupcu;
                tableLayoutPanel1.RowStyles.Clear();
                for (int i = 0; i < Settings.ModulPocetRadku; i++)
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / Settings.ModulPocetRadku));
                }
                tableLayoutPanel1.ColumnStyles.Clear();
                for (int i = 0; i < Settings.ModulPocetSloupcu; i++)
                {
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / Settings.ModulPocetSloupcu));
                }
                // povolení odvádění
                if (Settings.ModulPovolitOdvadeni)
                {
                    btnOdvadeni = new Button();
                    btnOdvadeni.Parent = this.tableLayoutPanel1;
                    btnOdvadeni.Text = "Odvádění";
                    btnOdvadeni.BackColor = Color.IndianRed;
                    btnOdvadeni.Dock = DockStyle.Fill;
                    //b.Font = new Font(;   //Tahoma; 20,25pt; style=Bold
                    btnOdvadeni.Font = new Font("Tahoma", 20.25f, FontStyle.Bold);
                    btnOdvadeni.UseVisualStyleBackColor = false;
                    btnOdvadeni.Click += new EventHandler(button_odvadeni_click);
                    btnOdvadeni.Margin = new Padding(2);                    
                    btnOdvadeni.KeyDown += new KeyEventHandler(btnOdvadeni_KeyDown);
                    this.btnOdvadeni.Enabled = Settings.ModulPovolitOdvadeni;
                    tableLayoutPanel1.SetColumn(btnOdvadeni, Settings.ModulOdvadeniColumn);
                    tableLayoutPanel1.SetRow(btnOdvadeni, Settings.ModulOdvadeniRow);
                    tableLayoutPanel1.SetRowSpan(btnOdvadeni, Settings.ModulOdvadeniRowSpan);
                    tableLayoutPanel1.SetColumnSpan(btnOdvadeni, Settings.ModulOdvadeniColumnSpan);
                }

                // dotisk štítků
                if (Settings.ModulPovolitDotisk)
                {
                    btnDotisk = new Button();
                    btnDotisk.Parent = this.tableLayoutPanel1;
                    btnDotisk.Text = "Dotisk";
                    btnDotisk.BackColor = Color.Yellow;
                    btnDotisk.Dock = DockStyle.Fill;
                    //b.Font = new Font(;   //Tahoma; 20,25pt; style=Bold
                    btnDotisk.Font = new Font("Tahoma", 20.25f, FontStyle.Bold);
                    btnDotisk.UseVisualStyleBackColor = false;
                    btnDotisk.Click += new EventHandler(button_dotisk_click);
                    btnDotisk.Margin = new Padding(2);
                    btnDotisk.KeyDown += new KeyEventHandler(btnDotisk_KeyDown);
                    this.btnDotisk.Enabled = Settings.ModulPovolitDotisk;
                    tableLayoutPanel1.SetColumn(btnDotisk, Settings.ModulDotiskColumn);
                    tableLayoutPanel1.SetRow(btnDotisk, Settings.ModulDotiskRow);
                    tableLayoutPanel1.SetRowSpan(btnDotisk, Settings.ModulDotiskRowSpan);
                    tableLayoutPanel1.SetColumnSpan(btnDotisk, Settings.ModulDotiskColumnSpan);
                }

                // povolení korekcí
                if (Settings.ModulPovolitKorekce)
                {
                    btnKorekce = new Button();
                    btnKorekce.Parent = this.tableLayoutPanel1;
                    btnKorekce.Text = "Korekce";
                    btnKorekce.BackColor = Color.RosyBrown;
                    btnKorekce.Dock = DockStyle.Fill;
                    btnKorekce.Font = new Font("Tahoma", 20.25f, FontStyle.Bold);
                    btnKorekce.UseVisualStyleBackColor = false;
                    btnKorekce.Click += new EventHandler(button_korekce_click);
                    btnKorekce.Margin = new Padding(2);
                    btnKorekce.KeyDown += new KeyEventHandler(btnKorekce_KeyDown);
                    this.btnKorekce.Enabled = Settings.ModulPovolitKorekce;
                    tableLayoutPanel1.SetColumn(btnKorekce, Settings.ModulKorekceColumn);
                    tableLayoutPanel1.SetRow(btnKorekce, Settings.ModulKorekceRow);
                    tableLayoutPanel1.SetRowSpan(btnKorekce, Settings.ModulKorekceRowSpan);
                    tableLayoutPanel1.SetColumnSpan(btnKorekce, Settings.ModulKorekceColumnSpan);
                }
                
                // povolení událostí
                if (Settings.ModulPovolitUdalosti)
                {
                    btnUdalosti = new Button();
                    btnUdalosti.Parent = this.tableLayoutPanel1;
                    btnUdalosti.Text = "Události";
                    btnUdalosti.BackColor = Color.MediumAquamarine;
                    btnUdalosti.Dock = DockStyle.Fill;
                    btnUdalosti.Font = new Font("Tahoma", 20.25f, FontStyle.Bold);
                    btnUdalosti.UseVisualStyleBackColor = false;
                    btnUdalosti.Click += new EventHandler(button_udalosti_click);
                    btnUdalosti.Margin = new Padding(2);
                    btnUdalosti.KeyDown += new KeyEventHandler(btnUdalosti_KeyDown);
                    this.btnUdalosti.Enabled = Settings.ModulPovolitUdalosti;
                    tableLayoutPanel1.SetColumn(btnUdalosti, Settings.ModulUdalostiColumn);
                    tableLayoutPanel1.SetRow(btnUdalosti, Settings.ModulUdalostiRow);
                    tableLayoutPanel1.SetRowSpan(btnUdalosti, Settings.ModulUdalostiRowSpan);
                    tableLayoutPanel1.SetColumnSpan(btnUdalosti, Settings.ModulUdalostiColumnSpan);
                }

                if (Settings.ModulPovolitPrehledOdvodu)
                {
                    ucHistory = new Fask.Vyroba_P.ucDataViews.Historie.ucHistory();
                    //ucHistory = new Fask.Vyroba_P.OtevreneProdukce.ucOpenedProduction();
                    ucHistory.Parent = this.tableLayoutPanel1;
                    ucHistory.Dock = DockStyle.Fill;
                    ucHistory.Margin = new Padding(2);
                    this.tableLayoutPanel1.SetColumn(ucHistory, Settings.ModulPrehledOdvoduColumn);
                    this.tableLayoutPanel1.SetRow(ucHistory, Settings.ModulPrehledOdvoduRow);
                    this.tableLayoutPanel1.SetRowSpan(ucHistory, Settings.ModulPrehledOdvoduRowSpan);
                    this.tableLayoutPanel1.SetColumnSpan(ucHistory, Settings.ModulPrehledOdvoduColumnSpan);

                    // načtení rozložení datagridu
                    ucHistory.DataGridView1.LoadConfiguration(this.GetType().ToString());
                }

                if (Settings.ModulPovolitNedokonceneZakazky)
                {
                    //ucHistory = new Fask.Vyroba_P.Historie.ucHistory();
                    ucOpenedProduction = new Fask.Vyroba_P.OtevreneProdukce.ucOpenedProduction();
                    ucOpenedProduction.Parent = this.tableLayoutPanel1;
                    ucOpenedProduction.Dock = DockStyle.Fill;
                    ucOpenedProduction.Margin = new Padding(2);
                    this.tableLayoutPanel1.SetColumn(ucOpenedProduction, Settings.ModulNedokonceneZakazkyColumn);
                    this.tableLayoutPanel1.SetRow(ucOpenedProduction, Settings.ModulNedokonceneZakazkyRow);
                    this.tableLayoutPanel1.SetRowSpan(ucOpenedProduction, Settings.ModulNedokonceneZakazkyRowSpan);
                    this.tableLayoutPanel1.SetColumnSpan(ucOpenedProduction, Settings.ModulNedokonceneZakazkyColumnSpan);

                    // načtení rozložení datagridu
                    ucOpenedProduction.DataGridView1.LoadConfiguration(this.GetType().ToString() + "OpenedProduction");
                }

                if (Settings.ModulPovolitUkolovani)
                {
                    btnUkolovani = new Button();
                    btnUkolovani.Parent = this.tableLayoutPanel1;
                    btnUkolovani.Text = "Úkolování";
                    btnUkolovani.Dock = DockStyle.Fill;
                    btnUkolovani.Font = new Font("Tahoma", 20.25f, FontStyle.Bold);
                    btnUkolovani.UseVisualStyleBackColor = false;
                    btnUkolovani.Click += new EventHandler(button_ukolovani_click);
                    btnUkolovani.Margin = new Padding(2);
                    this.btnUkolovani.Enabled = Settings.ModulPovolitUkolovani;
                    tableLayoutPanel1.SetColumn(btnUkolovani, Settings.ModulUkolovaniColumn);
                    tableLayoutPanel1.SetRow(btnUkolovani, Settings.ModulUkolovaniRow);
                    tableLayoutPanel1.SetRowSpan(btnUkolovani, Settings.ModulUkolovaniRowSpan);
                    tableLayoutPanel1.SetColumnSpan(btnUkolovani, Settings.ModulUkolovaniColumnSpan);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text);
            }
            
        }

        void btnOdvadeni_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.PerfomOdvadeni();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        void btnKorekce_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.PerformKorekce();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        void btnUdalosti_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.PerfomUdalosti();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        void btnDotisk_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.PerformDotisk();
                }
                else
                    return;
            }
            else
                return;

            e.Handled = true;
        }

        void button_odvadeni_click(object sender, EventArgs e)
        {
            this.PerfomOdvadeni();

          
        }

        void button_dotisk_click(object sender, EventArgs e)
        {
            this.PerformDotisk();
        }

        void button_korekce_click(object sender, EventArgs e)
        {
            this.PerformKorekce();
        }

        void button_udalosti_click(object sender, EventArgs e)
        {
            this.PerfomUdalosti();
        }

        void button_ukolovani_click(object sender, EventArgs e)
        {
            this.PerformUkolovani();
        }

        private void LogIn()
        {
            SystemTimeSynchronize();

            try
            {
                // aktivace tlačítka podle toho, zdali se má uživatel/směna přihlašovat, nebo ne
                menuItemOdhlasitPracovnika.Enabled = Settings.UEventPracovnikLoginEnabled;
                menuItemOdhlasitSmenu.Enabled = Settings.UEventSmenaEnabled;
                if (Settings.UEventSmenaEnabled)
                {
                    using (FormSmenaLogin fsl = new FormSmenaLogin())
                    {
                        DialogResult dr = fsl.ShowDialog(this);
                        if (dr == DialogResult.Cancel)
                        {
                            //this.PerformClose(false);
                            return;
                        }

                        try
                        {
                            Fask.SQLiteDBs.DataSets.InternalState.DeleteAll();
                        }
                        catch (Exception ex)
                        {
                            Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        }
                    }
                }

                //vyber zakazky
                if (Settings.VyberZakazkyPoPrihlaseni)
                {
                    using (FormInputKod fik = new FormInputKod())
                    {
                        fik.Text = "Zadejte číslo výrobního příkazu: ";
                        string zakazka = string.Empty;
                        while (true)
                        { // dokud neni dobre vybrano ... 
                            fik.Kod = zakazka;
                            if (fik.ShowDialog() == DialogResult.Cancel)
                            {
                                //LogOut(false);
                                return;
                            }

                            zakazka = fik.Kod;

                            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter vphta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter();
                            //vphta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
                            var zakazky = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcodeH_CZPRO_VPH(zakazka);
                            if (zakazky.Count > 1)
                            {
                                FlexibleMessageBox.Show(this, "Více zakázek");
                                continue;
                            }
                            else if (zakazky.Count == 0)
                            {
                                FlexibleMessageBox.Show(this, "Žádná zakázka");
                                continue;
                            }
                            else
                            {
                                Globals.Zakazka = zakazky[0];
                                break;
                            }
                        }
                    }
                }

                if (Settings.UEventPracovnikLoginEnabled)
                {
                    using (Forms.FormIDPracovnikaLogin fp = new FormIDPracovnikaLogin())
                    {
                        DialogResult dr = fp.ShowDialog(this);
                        if (dr == DialogResult.Cancel)
                        {
                            //this.PerformClose(false);
                            return;
                        }
                    }                    
                }

                
            }
            finally
            {
                UpdateForm();
            }

        }

        /// <summary>
        /// Odhlášení pracovníka a směny
        /// </summary>
        /// <param name="showConfirm">true - zobrazí se potvrzovací dialogy, false - nezobrazí</param>
        /// <returns>True if logged out, False if not logged out ...</returns>
        private bool LogOut(bool showConfirm)
        {
            try
            {
                if (Settings.UEventPracovnikLoginEnabled && Globals.Pracovnik != null)
                {
                    // zobrazeni prehledu
                    if (Settings.UEventPracovnikOdhlaseniShowReport)
                    {
                        using (Reports.FormReportUzivatelVyrobaDen fuvd = new Fask.Vyroba_P.Reports.FormReportUzivatelVyrobaDen())
                        {
                            if (fuvd.ShowDialog() == DialogResult.Cancel)
                                return false;
                        }
                    }
                    else
                    {
                        if (showConfirm && FlexibleMessageBox.Show(this, "Odhlásit pracovníka '" + Globals.Pracovnik.ToString() + "'?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                            == DialogResult.No)
                            return false;
                    }

                    try
                    {
                        // odhlaseni znamena odstraneni interniho zaznamu posledni operace uzivatele
                        Fask.SQLiteDBs.DataSets.InternalState.DeleteInternalStateLstOperationUser(Globals.Pracovnik.id);

                        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                        //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                        //ueta.Insert(Settings.LastProductionUserID, null, DateTime.Now, UEventStatusTypes.SmenaLogout, Settings.LastProductionUserID, Settings.TerminalID, Guid.NewGuid());
                        //ueta.Insert(Settings.LastProductionUserID, null, DateTime.Now, Settings.UEventSmenaLogout, Settings.LastProductionUserID, Settings.TerminalID, string.Empty, Guid.NewGuid());
                        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(Settings.LastProductionUserID, Settings.MachineID, DateTime.Now, Settings.UEventPracovnikOdhlaseni, Globals.Pracovnik.id, Settings.TerminalID, string.Empty, Guid.NewGuid());
                        Globals.Pracovnik = null;
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        FlexibleMessageBox.Show(this, ex.Message, this.Text);
                        return false;
                    }
                }

                if (Settings.UEventSmenaEnabled)
                {
                    if (showConfirm && FlexibleMessageBox.Show(this, "Odhlásit směnu?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        == DialogResult.No)
                    {
                        return false;
                    }

                    if (Settings.UEventSmenaLogoutPasswordConfig != string.Empty)
                    {
                        using (FormInputKod fik = new FormInputKod())
                        {                            
                            fik.Text = "Zadejte heslo pro odhlášení směny";
                            fik.Owner = this;
                            fik.ShowKod = false;
                            if (fik.ShowDialog(this) == DialogResult.Cancel)
                                return false;
                            if (Settings.UEventSmenaLogoutPasswordConfig != fik.Kod.Trim())
                            {
                                FlexibleMessageBox.Show(this, "Neplatné heslo", this.Text);
                                return false;
                            }
                        }
                    }

                    try
                    {
                        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                        //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                        //ueta.Insert(Settings.LastProductionUserID, null, DateTime.Now, UEventStatusTypes.SmenaLogout, Settings.LastProductionUserID, Settings.TerminalID, Guid.NewGuid());
                        //ueta.Insert(Settings.LastProductionUserID, null, DateTime.Now, Settings.UEventSmenaLogout, Settings.LastProductionUserID, Settings.TerminalID, string.Empty, Guid.NewGuid());
                        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(Settings.LastProductionUserID, Settings.MachineID, DateTime.Now, Settings.UEventSmenaLogout, Settings.LastProductionUserID, Settings.TerminalID, string.Empty, Guid.NewGuid());
                        Globals.PracovnikVedouciSmeny = null;
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                        FlexibleMessageBox.Show(this, ex.Message, this.Text);
                        return false;
                    }
                }


                Globals.Zakazka = null;

                //this.BeginInvoke((MethodInvoker)delegate() { LogIn(); });
            }
            finally
            {
                UpdateForm();
            }
            
            return true;
        }

        private void buttonUdalosti_Click(object sender, EventArgs e)
        {
            PerfomUdalosti();
        }

        private void PerfomUdalosti()
        {
            try
            {
                string idPracovnik = string.Empty;
                Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow pracovnik = null;

                if (Globals.Pracovnik != null)
                {
                    pracovnik = Globals.Pracovnik;
                    idPracovnik = pracovnik.id;
                }
                else
                {
                    using (Odvadeni.FormIDPracovnika fidprac = new Fask.Vyroba_P.Odvadeni.FormIDPracovnika())
                    {
                        if (fidprac.ShowDialog(this) == DialogResult.Cancel)
                            return;
                        pracovnik = fidprac.Pracovnik;
                        idPracovnik = pracovnik.id;
                    }
                }

                Fask.SQLiteDBs.DataSets.Vyroba.StatusTypesRow statusRow = null;
                //string idUdalosti = string.Empty;

                //using (FormInputKod fik = new FormInputKod())
                using (Udalosti.FormUdalosti fik = new Fask.Vyroba_P.Udalosti.FormUdalosti())
                {
                    fik.Text = "Zadejte ID události";
                    //fik.Kod = string.Empty;

                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.StatusTypesTableAdapter stta = new Fask.Vyroba_P.Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.StatusTypesTableAdapter();
                    //stta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdf);
                    //Fask.SQLiteDBs.DataSets.Vyroba.StatusTypesDataTable stdt = null;

                    //string stid = string.Empty;
                    while (true)
                    {
                        if (fik.ShowDialog(this) == DialogResult.Cancel)
                            return;

                        //idUdalosti = fik.Kod.Trim();
                        //stdt = stta.GetDataByStatusid(idUdalosti);
                        //if (stdt.Count == 0)
                        //{
                        //    FlexibleMessageBox.Show("Událost neexistuje", this.Text);
                        //    continue;
                        //}
                        //else
                        //{
                        //    stid = stdt[0].statusid;
                        //    if (stid == Settings.UEventSmenaLogin)
                        //        continue;
                        //    else if (stid == Settings.UEventSmenaLogout)
                        //        continue;

                        //    if (FlexibleMessageBox.Show(
                        //        pracovnik.firstname.Trim() + " " + pracovnik.surname.Trim() + "\n" +
                        //        stdt[0].statusdesc, 
                        //        this.Text, 
                        //        MessageBoxButtons.OKCancel, 
                        //        MessageBoxIcon.Question, 
                        //        MessageBoxDefaultButton.Button1
                        //        ) == DialogResult.Cancel
                        //        )
                        //        continue;
                        //    else
                        //        break;
                        //}

                        statusRow = fik.SelectedStatusTypesRow;
                        if (statusRow == null)
                        {
                            FlexibleMessageBox.Show(this, "Není zvolen záznam", this.Text);
                            continue;
                        }
                        if (statusRow.statusid == Settings.UEventSmenaLogin)
                            continue;
                        else if (statusRow.statusid == Settings.UEventSmenaLogout)
                            continue;

                        if (FlexibleMessageBox.Show(
                            this, 
                            pracovnik.firstname.Trim() + " " + pracovnik.surname.Trim() + "\n" +
                            statusRow.statusdesc,
                            this.Text,
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1
                            ) == DialogResult.Cancel
                            )
                            continue;
                        else
                            break;
                    }

                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                    //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(Settings.LastProductionUserID, string.Empty, DateTime.Now, statusRow.statusid, idPracovnik, Settings.TerminalID, string.Empty, Guid.NewGuid());

                    //if (stid == Settings.UEventPracovnikPrihlaseni) //Prihlaseni pracovnika => zapis do Internal logoper
                    if (statusRow.statusid == Settings.UEventPracovnikPrihlaseni) //Prihlaseni pracovnika => zapis do Internal logoper
                    {
                        Fask.SQLiteDBs.DataSets.InternalState.UpdateInternalStateLstOperationUser(idPracovnik, DateTime.Now);
                    }
                    //else if (stid == Settings.UEventPracovnikOdhlaseni)
                    else if (statusRow.statusid == Settings.UEventPracovnikOdhlaseni)
                    {
                        Fask.SQLiteDBs.DataSets.InternalState.DeleteInternalStateLstOperationUser(idPracovnik);
                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text);
            }
        }

        private void PerformKorekce()
        {
            try
            {
                string idPracovnik = string.Empty;
                Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow pracovnik = null;


                //Zadani ID pracovnika
                if (Globals.Pracovnik != null)
                {
                    pracovnik = Globals.Pracovnik;
                    idPracovnik = pracovnik.id;
                }
                else
                {
                    using (Odvadeni.FormIDPracovnika fidprac = new Odvadeni.FormIDPracovnika())
                    {
                        if (fidprac.ShowDialog(this) == DialogResult.Cancel)
                            return;
                        pracovnik = fidprac.Pracovnik;
                        idPracovnik = pracovnik.id;
                    }
                }

                // Korekce
                // TODO : implementovat ...
                // Zjistit posledni otevrenou korekci
                // pokud neni
                // - dialog pro vytvoreni korekce
                // - pokud je dialog pro ukonceni korekce 

                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter cta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CorrectsTableAdapter();
                //cta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter taprohist = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                //taprohist.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionHist + Constants.PRD));

                Fask.SQLiteDBs.DataSets.Vyroba dsV = new Fask.SQLiteDBs.DataSets.Vyroba();
                Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow correctionRow = null;

                // TODO : vyvest typ korekce do konfigurace ...
                if (Settings.CorrectionType == "1")
                {
                    #region Korekce postupne (Start a Stop oddelene)
                    // 1) zjistit zda je nejaka korekce
                    // 2) a) neni => zahajeni korekce
                    //    b) je   => ukonceni korekce 

                    // TODO : 1)
                    #region Nalezeni zda je neukoncena korekce

                    //Fask.SQLiteDBs.DataSets.Vyroba.ProductionDataTable pdt = pta.GetDataByUserIDMachineIDNULL(pracovnik.id);
                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.FillByUserIDMachineIDNULL_Production(dsV.Production ,pracovnik.id);

                    try
                    {
                        WebServiceVyroba.VyrobaDataSet dsVWeb = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Production_OpenedCorrection(pracovnik.id, string.Empty); //korekce mimo vyrobu ...

                        dsV.Production.Merge(dsVWeb.Production, false, MissingSchemaAction.Ignore);
                    }
                    catch (Exception exWeb)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exWeb);
                        if (Settings.ZobrazovatChybySynchronizaceDatabaze && (DialogResult.Cancel == FlexibleMessageBox.Show(this, "Nezdařilo se zjištění stavu korekce ze serveru..." + "\nPokračovat?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)))
                        {
                            return;
                        }
                    }

                    bool korekceStart = true; //zahajit korekci

                    var correctionOpened = dsV.Production.OrderByDescending(p => p.dateeve);
                    if (correctionOpened.Count() > 0 
                        && correctionOpened.First().IsTIMECORSTOPNull() 
                        && !correctionOpened.First().IsTIMECRIDNull() 
                        && !correctionOpened.First().IsTIMECORSTARTNull()
                        )
                    {
                        Fask.SQLiteDBs.DataSets.Vyroba.CorrectsDataTable cdt = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Corrects(correctionOpened.First().TIMECRID);
                        if (cdt.Count <= 0)
                        {
                            FlexibleMessageBox.Show(this, "Otevřená korekce s ID='" + correctionOpened.First().TIMECRID + "' nenalezena!", "Korekce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        correctionRow = cdt[0];
                        korekceStart = false; // ukoncit korekci
                    }

                    #endregion

                    if (korekceStart)
                    { // Neni otevrena korekce ...
                        using (Korekce.FormKorekceStart formKorekce = new Korekce.FormKorekceStart())
                        {
                            Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = dsV.Production.NewProductionRow();

                            //formKorekce.Pracovnik = pracovnik;
                            formKorekce.ProductionRow = productionRow;
                            formKorekce.Correction = correctionRow;
                            formKorekce.CorretionMinimumDateTime = Data.DatabaseActions.UserLastAction(pracovnik.id);
                            if (DialogResult.Cancel == formKorekce.ShowDialog(this))
                                return;

                            //productionRow.CountEntries = rowvpp.CountEntries;
                            //productionRow.BarcodeP = rowvpp.BarcodeP;
                            //productionRow.dateeve = DateTime.Now;
                            productionRow.description = string.Empty;
                            productionRow.GUID = Guid.NewGuid();
                            //productionRow.ITEMNMBR = rowvpp.ITEMNMBR;
                            //productionRow.ITEMTYPE = rowvpp.ITEMTYPE;
                            //if (!rowvpp.IsITEMMJNull())
                            //    productionRow.ITEMMJ = rowvpp.ITEMMJ;
                            productionRow.loginid = Settings.LastProductionUserID;
                            //productionRow.machineid = idmachine.id;
                            //productionRow.ORD = rowvpp.ORD;
                            productionRow.qty = 0; // pocetOdvedeno;
                            productionRow.qtyReal = 0; // pocetOdvedeno;
                            //productionRow.QTYPACK = rowvpp.QTYPACK;
                            //if (!rowvpp.IsQTYPACKMJNull())
                            //    productionRow.QTYPACKMJ = rowvpp.QTYPACKMJ;
                            //productionRow.SOPNUMBE = rowvpp.SOPNUMBE;
                            //productionRow.TIMEMODE = rowvpp.TIMEMODE;
                            //productionRow.TIMEUNIT = rowvpp.TIMEUNIT;

                            productionRow.UserID = pracovnik.id;
                            productionRow.TermID = Settings.TerminalID;

                            productionRow.CORRGUID = Guid.NewGuid();
                            dsV.Production.AddProductionRow(productionRow);
                            //pta.Update(productionRow);
                            Data.DatabaseActions.InsertProduction(productionRow);
                            // pridani do historie
                            if (Settings.ModulPovolitPrehledOdvodu)
                            {
                                productionRow.SetAdded();
                                //taprohist.Update(productionRow);
                                Data.DatabaseActions.InsertProductionHistory(productionRow);
                            }
                            //this.ucHistory1.updateData(null);
                        }
                    }
                    else
                    { // b) ukoncit korekci ...

                        if (DialogResult.No == FlexibleMessageBox.Show(this, "Ukončit korekci '" + correctionRow.desc.Trim() + "'?", "Korekce", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            return;
                        }
                        Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = dsV.Production.NewProductionRow();

                        Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRowCorrection = correctionOpened.First();
                        productionRow.description = productionRowCorrection.description;
                        productionRow.GUID = Guid.NewGuid();
                        productionRow.loginid = Settings.LastProductionUserID;
                        productionRow.qty = productionRowCorrection.qty;
                        productionRow.qtyReal = productionRowCorrection.qtyReal;
                        productionRow.UserID = pracovnik.id;
                        productionRow.TermID = Settings.TerminalID;
                        productionRow.TIMECRID = productionRowCorrection.TIMECRID;
                        productionRow.TIMECORSTART = productionRowCorrection.TIMECORSTART;
                        productionRow.TIMECORSTOP = DateTime.Now;
                        productionRow.TIMECOR = (float)(productionRow.TIMECORSTOP - productionRow.TIMECORSTART).TotalMinutes;
                        productionRow.dateeve = DateTime.Now;
                        if(!productionRowCorrection.IsCORRGUIDNull())
                            productionRow.CORRGUID = productionRowCorrection.CORRGUID;
                        dsV.Production.AddProductionRow(productionRow);
                        //pta.Update(productionRow);
                        Data.DatabaseActions.InsertProduction(productionRow);
                        // pridani do historie
                        if (Settings.ModulPovolitPrehledOdvodu)
                        {
                            productionRow.SetAdded();
                            //taprohist.Update(productionRow);
                            Data.DatabaseActions.InsertProductionHistory(productionRow);
                        }
                        //this.ucHistory1.updateData(null);
                    }

                    #endregion
                }
                else //if (korekceZpusob == 0)
                {
                    #region Korekce jednotne (Start a Stop soucasne)
                    using (Korekce.FormKorekce formKorekce = new Korekce.FormKorekce())
                    {
                        Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = dsV.Production.NewProductionRow();

                        //formKorekce.Pracovnik = pracovnik;
                        formKorekce.ProductionRow = productionRow;
                        formKorekce.Correction = correctionRow;
                        formKorekce.CorretionMinimumDateTime = Data.DatabaseActions.UserLastAction(pracovnik.id);
                        if (DialogResult.Cancel == formKorekce.ShowDialog(this))
                            return;

                        //productionRow.CountEntries = rowvpp.CountEntries;
                        //productionRow.BarcodeP = rowvpp.BarcodeP;
                        //productionRow.dateeve = DateTime.Now;
                        productionRow.description = string.Empty;
                        productionRow.GUID = Guid.NewGuid();
                        //productionRow.ITEMNMBR = rowvpp.ITEMNMBR;
                        //productionRow.ITEMTYPE = rowvpp.ITEMTYPE;
                        //if (!rowvpp.IsITEMMJNull())
                        //    productionRow.ITEMMJ = rowvpp.ITEMMJ;
                        productionRow.loginid = Settings.LastProductionUserID;
                        //productionRow.machineid = idmachine.id;
                        //productionRow.ORD = rowvpp.ORD;
                        productionRow.qty = 0; // pocetOdvedeno;
                        productionRow.qtyReal = 0; // pocetOdvedeno;
                        //productionRow.QTYPACK = rowvpp.QTYPACK;
                        //if (!rowvpp.IsQTYPACKMJNull())
                        //    productionRow.QTYPACKMJ = rowvpp.QTYPACKMJ;
                        //productionRow.SOPNUMBE = rowvpp.SOPNUMBE;
                        //productionRow.TIMEMODE = rowvpp.TIMEMODE;
                        //productionRow.TIMEUNIT = rowvpp.TIMEUNIT;

                        productionRow.UserID = pracovnik.id;
                        productionRow.TermID = Settings.TerminalID;

                        dsV.Production.AddProductionRow(productionRow);
                        //pta.Update(productionRow);
                        Data.DatabaseActions.InsertProduction(productionRow);
                        // pridani do historie
                        if (Settings.ModulPovolitPrehledOdvodu)
                        {
                            productionRow.SetAdded();
                            //taprohist.Update(productionRow);
                            Data.DatabaseActions.InsertProductionHistory(productionRow);
                        }
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text);
            }            
        }

        private void PerfomOdvadeni()
        {
            try
            {
                Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idPracovnik = null;
                Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idMachine = null;

                // zadání vedoucího směny při odvádění, pokud není zvolen a je požadován
                if (Settings.UEventSmenaEnabled && Globals.PracovnikVedouciSmeny == null)
                {
                    using (FormSmenaLogin fsl = new FormSmenaLogin())
                    {
                        fsl.Text = "Zadejte vedoucího směny";
                        DialogResult dr = fsl.ShowDialog(this);
                        if (dr == DialogResult.Cancel)
                        {
                            return;
                        }
                        //else 
                        //    if (dr == DialogResult.Abort) //Vstup do administracni casti
                        //{
                        //    Logging.Log.Write("Administrative login");
                        //    this.AdminMode = true;
                        //    return;
                        //}

                        //try
                        //{
                        //    Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter louta = new Fask.Vyroba_P.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
                        //    louta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
                        //    int raff = louta.DeleteAll();
                        //}
                        //catch (Exception ex)
                        //{
                        //    Logging.Log.Write(ex, this.Text);
                        //}
                        UpdateForm();
                    }
                }

                //vyber zakazky
                if (Settings.VyberZakazkyPoPrihlaseni && Globals.Zakazka == null)
                {
                    using (FormInputKod fik = new FormInputKod())
                    {
                        fik.Text = "Zadejte číslo zakázky";
                        string zakazka = string.Empty;
                        while (true)
                        { // dokud neni dobre vybrano ... 
                            fik.Kod = zakazka;
                            if (fik.ShowDialog() == DialogResult.Cancel)
                            {
                                return;
                            }

                            zakazka = fik.Kod;

                            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter vphta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZPRO_VPHTableAdapter();
                            //vphta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
                            var zakazky = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByBarcodeH_CZPRO_VPH(zakazka);
                            if (zakazky.Count > 1)
                            {
                                FlexibleMessageBox.Show(this, "Více zakázek");
                                continue;
                            }
                            else if (zakazky.Count == 0)
                            {
                                FlexibleMessageBox.Show(this, "Žádná zakázka");
                                continue;
                            }
                            else
                            {
                                Globals.Zakazka = zakazky[0];
                                break;
                            }
                        }
                        UpdateForm();
                    }
                }

                //Zadani ID pracovnika
                if (Globals.Pracovnik != null)
                {
                    idPracovnik = Globals.Pracovnik;
                }
                else if(Globals.Pracovnik == null && Settings.Odvadeni_OvereniUzivateleBezHesla)
                {
                    using (Odvadeni.FormIDPracovnikaBezHesla frmIDPracovnika = new Fask.Vyroba_P.Odvadeni.FormIDPracovnikaBezHesla())
                    {
                        if (frmIDPracovnika.ShowDialog(this) == DialogResult.Cancel)
                            return;

                        idPracovnik = frmIDPracovnika.Pracovnik;
                    }
                }
                else
                {
                    using (Odvadeni.FormIDPracovnika frmIDPracovnika = new Fask.Vyroba_P.Odvadeni.FormIDPracovnika())
                    {
                        if (frmIDPracovnika.ShowDialog(this) == DialogResult.Cancel)
                            return;

                        idPracovnik = frmIDPracovnika.Pracovnik;
                    }
                }

                //Zjisteni posledniho casu operace pracovnika
                //Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter lstoperta = new Fask.Vyroba_P.Data.InternalStateDataSetTableAdapters.LstOperationUserTableAdapter();
                //lstoperta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf);
                //DateTime lstopertimePracovnik = lstoperta.GetLastOperationDateTime(idPracovnik.id) ?? DateTime.MinValue;
                DateTime lstopertimePracovnik = Fask.SQLiteDBs.DataSets.InternalState.GetInternalStateLstOperationUser(idPracovnik.id) ?? DateTime.MinValue;

                //Datum a cas posledni oprace pracovnika je vetsi nez maximalni mozny cas pracovnika
                // => zadat datum a cas prichodu a zaznamenat prichod
                if (lstopertimePracovnik < (DateTime.Now - Settings.LoginUserTimeOut))
                {
                    if (Settings.UdalostiZobrazitCasPrihlaseniPracovnika)
                    {
                        using (Forms.FormUserLoginWithTimeInput formusertime = new FormUserLoginWithTimeInput(idPracovnik))
                        {
                            if (formusertime.ShowDialog(this) == DialogResult.Cancel)
                                return;

                            //datum a cas posledni operace
                            lstopertimePracovnik = formusertime.UserLoginDateTime;

                        }
                    }
                    else
                    {
                        lstopertimePracovnik = DateTime.Now;
                    }

                    //prihlaseni je operaci pracovnika ...
                    Fask.SQLiteDBs.DataSets.InternalState.UpdateInternalStateLstOperationUser(idPracovnik.id, lstopertimePracovnik);
                    //Zaznamenat udalost prihlaseni pracovnika
                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter ueta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.UserEventsTableAdapter();
                    //ueta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);
                    Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Production_PRD.Insert_UserEvents(Settings.LastProductionUserID, string.Empty, lstopertimePracovnik, Settings.UEventPracovnikPrihlaseni, idPracovnik.id, Settings.TerminalID, string.Empty, Guid.NewGuid());
                }

                //Zadani id stroje
                if (Settings.OdvadeniPozadovatZadaniStroje)
                {
                    if (!String.IsNullOrEmpty(Settings.MachineID))
                    {
                        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter taMachines = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter();
                        //taMachines.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
                        Fask.SQLiteDBs.DataSets.Vyroba.MachinesDataTable dtMachines = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.controller_Vyroba__Vyroba_PRD.GetDataByID_Machines(Settings.MachineID);
                        if (dtMachines.Count > 0)
                            idMachine = dtMachines[0];
                        else
                            throw new Exception("Stroj s ID='" + Settings.MachineID + "' nenalezen!");
                    }
                    else
                    {
                        using (Odvadeni.FormIDMachine frmIDMachine = new Fask.Vyroba_P.Odvadeni.FormIDMachine())
                        {
                            if (frmIDMachine.ShowDialog(this) == DialogResult.Cancel)
                                return;

                            idMachine = frmIDMachine.Machine;
                        }
                    }
                }
                else
                {
                    using (Odvadeni.FormIDMachine frmIDMachine = new Fask.Vyroba_P.Odvadeni.FormIDMachine())
                    {
                        if (frmIDMachine.ShowDialog(this) == DialogResult.Cancel)
                            return;

                        idMachine = frmIDMachine.Machine;
                    }
                }

                //Odvadeni
                // TODO : odvadeni type na Enum ...
                if (Settings.OdvadeniType == "P") //odvadeni prikazy
                {
                    using (Odvadeni.FormOdvadeni frmOdvadeni = new Fask.Vyroba_P.Odvadeni.FormOdvadeni(idPracovnik, idMachine))
                    {
                        frmOdvadeni.ShowDialog(this);
                    }
                }
                else if (Settings.OdvadeniType == "Z") //odvadeni zakazky
                {
                    using (OdvadeniNadop.FormOdvadeni frmOdvadeni = new Fask.Vyroba_P.OdvadeniNadop.FormOdvadeni(idPracovnik, idMachine))
                    {
                        DialogResult dr = frmOdvadeni.ShowDialog(this);
                        if (dr == DialogResult.Retry)
                        {
                            //this.ucHistory1.updateData(null);
                            //this.LogOut(false);
                            timerUploadCallBack(null);
                            PerfomOdvadeni();
                        }
                    }
                    timerUploadCallBack(null);
                    //this.ucHistory1.updateData(null);
                }
                else
                {
                    FlexibleMessageBox.Show(this, "Není korektně nastaven typ odvádění.\nPovolené varianty: [Z, P]");
                }


                if (Settings.ModulPovolitPrehledOdvodu  && ucHistory != null)
                {

                    ucHistory.AktualizaceDat();
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text);
            }
        }

        private void PerformUkolovani()
        {
            using (Fask.Vyroba_P.Ukolovani.UkolovaniMain ukolovani = new Fask.Vyroba_P.Ukolovani.UkolovaniMain()) 
            {
                ukolovani.Owner = this;
                ukolovani.ShowDialog();
            }
        }

        private bool PerformClose(bool question)
        {
            //if (question)
            //{
            //    if (FlexibleMessageBox.Show("Ukončit aplikaci?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            //        == DialogResult.No)
            //        return false;
            //}
            //this.finalize();
            this.Close();
            return true;
        }

        private void PerformDotisk()
        {
            try
            {
                //TODO MaR dopsat logiku dotisku
                var data = ucHistory.SelectedRow;

                if (data == null)
                    return;

                Classes.PrintPaleta pr = new Classes.PrintPaleta();
                pr.PerformPaletaTisk(data); //metoda pro tisk

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                FlexibleMessageBox.Show(this, ex.Message, this.Text);
            }
        }

        private void buttonOdvadeni_Click(object sender, EventArgs e)
        {
            this.PerfomOdvadeni();
        }

        private void DownloadInternalStateStart()
        {
            try
            {
                DateTime lastDownload = DateTime.Now;

                bool remotefilename = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.VyrobaDBInternalStatePrepareZip(Settings.TerminalID, lastDownload);

                if (!remotefilename)
                    throw new Exception("Prepare data on server was not succesfull");

                updateProgressBarDownloadDelegate callback = updateProgressBarDownload;

                string downloaded = FileTransfer.Downloading.DownloadFileFromServer(Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalState + Constants.PRD), false, true, callback);
                if (downloaded != "OK")
                    throw new Exception("Download from server was not succesfull", new Exception(downloaded));

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                this.BeginInvoke(new DelegateUpdateForm(UpdateForm));
            }
        
        }

        private void DownloadProductionStart()
        {
            try
            {
                DateTime lastDownload = DateTime.Now;

                bool remotefilename = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.VyrobaDBProductionPrepareZip(Settings.TerminalID, lastDownload);

                if (!remotefilename)
                    throw new Exception("Prepare data on server was not succesfull");

                updateProgressBarDownloadDelegate callback = updateProgressBarDownload;

                string downloaded = FileTransfer.Downloading.DownloadFileFromServer(Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD), false, true, callback);
                if (downloaded != "OK")
                    throw new Exception("Download from server was not succesfull", new Exception(downloaded));

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                this.BeginInvoke(new DelegateUpdateForm(UpdateForm));
            }
        
        }

        void fileDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.BeginInvoke(
                new updateProgressBarDownloadDelegate(updateProgressBarDownload), 
                new object[] { e.ProgressPercentage , "" }
                );
        }

        // Create a delegate instance
        
        void updateProgressBarDownload(int ProgressPercentage, string Text)
        {
            if (this.InvokeRequired)
            {
                //sa vytvori nove vlakno v kterem je zavolana znovu zazo metoda
                this.BeginInvoke((System.Threading.ThreadStart)delegate () { this.updateProgressBarDownload(ProgressPercentage, Text); });
                return;
            }

            toolStripProgressBar1.Value = ProgressPercentage;
            statusBarInfo.Text = Text;
        }

        private void menuItemTimeSynchronization_Click(object sender, EventArgs e)
        {
            SystemTimeSynchronize();
        }

        private void SystemTimeSynchronize()
        {
            Cursor.Current = Cursors.WaitCursor; Application.DoEvents();

            try
            {
                DateTime servertime = Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.ServerDateTime();
                Fask.Vyroba_P.MySystem.SystemDateTime.SetDeviceTime(servertime);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //FlexibleMessageBox.Show(ex.Message);
            }
            finally
            {
            }

            Cursor.Current = Cursors.Default;
        }

        private void finalize()
        {
            try
            {
                //Settings.ApplicationPosition = this.Location;
                Settings.Update();

                timerUploadStop();
                timerDownloadStop();

                if (threadTimerDownload != null)
                {
                    try { threadTimerDownload.Join(1000); }
                    catch { }
                    try { threadTimerDownload.Abort(); }
                    catch { }
                    threadTimerDownload = null;
                }

                if (Scanner != null)
                {
                    Scanner.TerminateScanner();
                    Scanner = null;
                }

                if (m != null)
                {
                    m.ReleaseMutex();
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        delegate void DelegateUpdateForm();

        private void UpdateForm()
        {
            this.UpdateStatusBarInfo();
            this.UpdateStatuBarLogins();
            this.UpdateStatusBarMachine();
            this.UpdateStatusBarZakazka();
        }

        private void UpdateStatusBarZakazka()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate() { UpdateStatusBarZakazka(); });
                return;
            }

            toolStripStatusZakazka.Text = Globals.Zakazka == null ? string.Empty : "Z: " + Globals.Zakazka.SOPDESC.Trim() + "(" + Globals.Zakazka.SOPNUMBE.Trim() + ")";
            //buttonOdvadeni.Text = "odvádění" + Globals.Zakazka == null ? string.Empty : "Z: " + Globals.Zakazka.SOPDESC.Trim() + "(" + Globals.Zakazka.SOPNUMBE.Trim() + ")";
            if(btnOdvadeni != null)
                btnOdvadeni.Text = Globals.Zakazka == null ? "Odvádění" : "Odvádění\nZ: " + Globals.Zakazka.SOPDESC.Trim() + "(" + Globals.Zakazka.SOPNUMBE.Trim() + ")";
        }

        delegate void DelegateUpdateStatusBarInfo(string text);

        private void UpdateStatusBarInfo()
        {
            UpdateStatusBarInfo("Aktualizováno: " + Settings.LastDownload.ToString());
        }

        public void UpdateStatusBarInfo(string text)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate() { UpdateStatusBarInfo(text); });
                return;
            }
            statusBarInfo.Text = text;
        }

        public void TasksSynchronizationStart()
        {
            if (Settings.TasksEnable)
                Fask.Vyroba_P.Ukolovani.Ukolovani_Checker.SynchronizationStart();
        }

        public void TasksSynchronizationStop()
        {
            Fask.Vyroba_P.Ukolovani.Ukolovani_Checker.SynchronizationStop();
        }

        public void ShowAsynchNewTasks(List<UkolovaniService.Ukol> newTasks)
        {
            // TODO : zastavit scanner a pamatovat zda byl pusteny a na konci ve finally ho spustit pokud byl spusteny...

            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        ShowAsynchNewTasks(newTasks);
                    });
                    return;
                }

                string messageTitle = "Nové úkoly = " + newTasks.Count;

                if (newTasks.Count != 1)
                {
                    string message = string.Empty;
                    foreach (var item in newTasks)
                    {
                        if (message.Length > 0)
                            message += "\n";
                        message += "" + item.id + " : " + item.nazev.Trim();
                    }

                    //MessageBoxBigTimeout.Show(message, messageTitle, MessageBoxButtons.OK, MessageBoxBigIcon.Information, 20, false);
                    MessageBox.Show(message, messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                { //pokud je prave jeden, tak umoznit ihned editaci na ok ...
                    //DialogResult drTask = MessageBoxBigTimeout.Show("Zobrazit nový úkol?\n" + newTasks[0].id + " : " + newTasks[0].nazev, messageTitle, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information, 20, false);
                    DialogResult drTask = MessageBox.Show("Zobrazit nový úkol?\n" + newTasks[0].id + " : " + newTasks[0].nazev, messageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (drTask == DialogResult.Yes)
                    { // => zobrazit editaci ...
                        TasksSynchronizationStop();

                        Fask.Vyroba_P.Data.UkolyTableAdapters.CZ_UKOLTableAdapter ta_ukol = new Fask.Vyroba_P.Data.UkolyTableAdapters.CZ_UKOLTableAdapter();
                        Fask.Vyroba_P.Data.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter ta_ukol_uziv = new Fask.Vyroba_P.Data.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();

                        ta_ukol.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Constants.CiselnikUkolyDB);
                        ta_ukol_uziv.Connection = ta_ukol.Connection;

                        Fask.Vyroba_P.Data.Ukoly.CZ_UKOL_UZIVRow uuziv = ta_ukol_uziv.GetDataByID(newTasks[0].id)[0];
                        Fask.Vyroba_P.Data.Ukoly.CZ_UKOLRow u = ta_ukol.GetDataByID(uuziv.UkolID)[0];

                        using (Fask.Vyroba_P.Ukolovani.UkolovaniUkolEdit uedit = new Fask.Vyroba_P.Ukolovani.UkolovaniUkolEdit(u, uuziv))
                        {
                            DialogResult drUEdit = uedit.ShowDialog();
                            if (drUEdit == DialogResult.OK)
                                ta_ukol_uziv.Update(uedit._ukol_uziv); //tady se zmenil stav...

                        }

                        TasksSynchronizationStart();
                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void ShowAsynchNotificationTasks(List<UkolovaniService.Ukol> notificationTasks)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        ShowAsynchNotificationTasks(notificationTasks);
                    });
                    return;
                }

                string messageTitle = "Připomenutí úkolů = " + notificationTasks.Count;

                //todo:kdyz je 0 ukolu,tak nic neudelat !!!
                if (notificationTasks.Count == 0)
                    return;

                if (notificationTasks.Count != 1)
                {
                    string message = string.Empty;
                    foreach (var item in notificationTasks)
                    {
                        if (message.Length > 0)
                            message += "\n";
                        message += "" + item.id + " : " + item.nazev.Trim();
                    }

                    DialogResult dRes = FlexibleMessageBox.Show(message, messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                { //pokud je prave jeden, tak umoznit ihned editaci na ok ...
                    DialogResult drTask = FlexibleMessageBox.Show("Zobrazit úkol?\n" + notificationTasks[0].id + " : " + notificationTasks[0].nazev, messageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (drTask == DialogResult.Yes)
                    { // => zobrazit editaci ...
                        TasksSynchronizationStop();

                        Fask.Vyroba_P.Data.UkolyTableAdapters.CZ_UKOLTableAdapter ta_ukol = new Fask.Vyroba_P.Data.UkolyTableAdapters.CZ_UKOLTableAdapter();
                        Fask.Vyroba_P.Data.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter ta_ukol_uziv = new Fask.Vyroba_P.Data.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();

                        ta_ukol.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Constants.CiselnikUkolyDB);
                        ta_ukol_uziv.Connection = ta_ukol.Connection;

                        Fask.Vyroba_P.Data.Ukoly.CZ_UKOL_UZIVRow uuziv = ta_ukol_uziv.GetDataByID(notificationTasks[0].id)[0];
                        Fask.Vyroba_P.Data.Ukoly.CZ_UKOLRow u = ta_ukol.GetDataByID(uuziv.UkolID)[0];

                        using (Fask.Vyroba_P.Ukolovani.UkolovaniUkolEdit uedit = new Fask.Vyroba_P.Ukolovani.UkolovaniUkolEdit(u, uuziv))
                        {
                            DialogResult drUEdit = uedit.ShowDialog();
                            if (drUEdit == DialogResult.OK)
                                ta_ukol_uziv.Update(uedit._ukol_uziv); //tady se zmenil stav...

                        }

                        TasksSynchronizationStart();
                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void UpdateStatuBarLogins()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate() { UpdateStatuBarLogins(); });
                return;
            }

            toolStripStatusPracovnik.Text = "" + (Globals.PracovnikVedouciSmeny == null ? string.Empty : "V: " + Globals.PracovnikVedouciSmeny.surname.Trim() + " ");
            toolStripStatusPracovnik.Text += "" + (Globals.Pracovnik == null ? string.Empty : "P: " + Globals.Pracovnik.ToString());
        }

        public void UpdateStatusBarMachine()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate() { UpdateStatusBarMachine(); });
                return;
            }

            try
            {
                if (!String.IsNullOrEmpty(Settings.MachineID))
                {
                    toolStripStatusStroj.Text = "M: " + (Settings.MachineID);
                }
                else
                {
                    toolStripStatusStroj.Text = string.Empty;
                }
            }
            catch
            {
                toolStripStatusStroj.Text = "M: ?";
            }
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            if (!this.LogOut(true))
                return;
            this.BeginInvoke((MethodInvoker)delegate() { LogIn(); });
        }

        private void menuItemConfiguration_Click(object sender, EventArgs e)
        {
            using (FormInputKod frmInputKod = new FormInputKod()) 
            {
                frmInputKod.Owner = this;
                frmInputKod.ShowKod = false;
                Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FormMain - přístup do FormConfig" + "Pokus o přechod do konfigurace aplikace" );
                if (frmInputKod.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {                        
                        if (Settings.PasswordConfig.Equals(frmInputKod.Kod))
                        {
                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FormMain - přístup do FormConfig" + "Správné heslo");
                            showFormConfig();
                        }
                        else
                        {
                            FlexibleMessageBox.Show(this, "Neplatné heslo", "", MessageBoxButtons.OK);
                            Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FormMain - přístup do FormConfig" + "Špatné heslo");
                        }
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    }
                }
            }          
        }

        private void showFormConfig()
        {
            // uložení rozložení datagridu
            if (Settings.ModulPovolitPrehledOdvodu && ucHistory != null)
                ucHistory.DataGridView1.SaveConfiguration(this.GetType().ToString());

            if (Settings.ModulPovolitNedokonceneZakazky && ucOpenedProduction != null)
                ucOpenedProduction.DataGridView1.SaveConfiguration(this.GetType().ToString() + "OpenedProduction");

            using (FormConfig frmConfig = new FormConfig())
            {
                frmConfig.Owner = this;
                if (frmConfig.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        menuItemOdhlasitPracovnika.Enabled = Settings.UEventPracovnikLoginEnabled;
                        menuItemOdhlasitSmenu.Enabled = Settings.UEventSmenaEnabled;
                        if (btnKorekce != null && Settings.ModulPovolitKorekce)
                            this.btnKorekce.Enabled = Settings.ModulPovolitKorekce;
                        if(btnOdvadeni != null && Settings.ModulPovolitOdvadeni)
                            this.btnOdvadeni.Enabled = Settings.ModulPovolitOdvadeni;
                        if (btnUdalosti != null && Settings.ModulPovolitUdalosti)
                            this.btnUdalosti.Enabled = Settings.ModulPovolitUdalosti;
                        if (btnDotisk != null && Settings.ModulPovolitDotisk)
                            this.btnDotisk.Enabled = Settings.ModulPovolitDotisk;
                        //Logging.Log.Enable = Settings.Loging;
                        LoadModules();
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    }

                    try
                    {
                        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Url = Settings.WebServiceAddressVyroba + Constants.Vyroba_asmx;
                        Fask.Vyroba_P.Forms.FormMain.Instance_FormMain.globalObject.vyrobaServis.Timeout = Settings.TimeOut;

                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    }

                    menuItemOdhlasitSmenu.Enabled = Settings.UEventSmenaEnabled;
                }
            }
        }

        private void buttonKorekce_Click(object sender, EventArgs e)
        {
            this.PerformKorekce();
        }

        private void menuItemOdhlasitPracovnika_Click(object sender, EventArgs e)
        {
            if (!this.LogOut(true))
                return;
            this.BeginInvoke((MethodInvoker)delegate() { LogIn(); });
        }

        public static void hideTaskbar()
        {
#if !DEBUG
            ShowWindow(HandleTaskbar, 0);
            //EnableWindow(HandleTaskbar, false);
#endif
           // ShowWindow(HandleTaskbar, 1);
            //EnableWindow(HandleTaskbar, false);
        }



        void OnPowerChange(Object sender, PowerModeChangedEventArgs e)
        {
            try
            {
                switch (e.Mode)
                {
                    case PowerModes.Resume:
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "application resume");
                        break;
                    case PowerModes.Suspend:
                        Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Warn, "application suspend");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            // aktualizuje zobrazeni casu vlevo nahore ...
            string timetoshow = DateTime.Now.ToString("HH:mm");
        }

        private void menuItemOdhlasit_Click(object sender, EventArgs e)
        {
            if (!this.LogOut(true))
                return;
            this.BeginInvoke((MethodInvoker)delegate() { this.LogIn(); });
        }


        #region Netuším k čemu to je....

        /// <summary>
        /// CO TO JE???
        /// </summary>
        private void testfilesharingviolation()
        {
            using (Test.FormFilesharingviolation ffsv = new Fask.Vyroba_P.Test.FormFilesharingviolation())
            {
                ffsv.ShowDialog();
            }
        }

        /// <summary>
        /// CO TO JE???
        /// </summary>
        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            testfilesharingviolation();
        }


        #endregion

        private void sledováiPapouchToolStripMenuItem_Click(object sender, EventArgs e)
        {


            FormSledovaniPapouch newForm = new FormSledovaniPapouch();
           // newForm.Show(); // zobrazí formulář neblokujícím způsobem
            newForm.ShowDialog(); // zablokuje hlavní okno, dokud se nezavře



        }
    }
}