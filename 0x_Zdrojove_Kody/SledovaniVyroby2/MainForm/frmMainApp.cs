using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Transactions;
using FASK.SledovaniVyroby.ErrorLog;
using FASK.SledovaniVyroby.Logging;
using FASK.SledovaniVyroby.ModuleIfc;
using System.Reflection;
using System.IO;
using Fask.Emailing;
using System.Linq;
using Fask.Logging;
using Fask.Vyroba_P.Forms;
using JR.Utils.GUI.Forms;
using System.Net;
using RestSharp;
using FASK.SledovaniVyroby.Forms;
using ICommDatabase;

namespace FASK.SledovaniVyroby.Main
{
    public partial class frmMainApp : Form
    {
        /*
        private frmVrtacka module0 = null;
        private frmVrtackaStara module2 = null;
         */

        //Connector pro pripojovani modulu
       public IModuleConnector connector = null;
        //FullScreen fullScreen = null;

        System.Threading.Timer timer1 = null;

        //Konstruktor
        public frmMainApp()
        {
            InitializeComponent();

            //fullScreen = new FullScreen(this);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new Point(0, 0);
            //this.Icon = Properties.Resources.logo_FASK2;
            this.Bounds = Screen.GetBounds(new Point(0, 0));



            UpdateFormText();

            this.DataBindings.Add(new System.Windows.Forms.Binding("WindowState", global::FASK.SledovaniVyroby.Main.Properties.Settings.Default, "VyrobaWindowState", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
        }

        //Zobrazeni informaci
        private void UpdateFormText()
        {
            this.Text = "Sledování výroby " + " (" + this.ProductVersion.ToString() + ")";
            this.Text += " " + LogConfig.LoginString;
        }

        //Stisk tlacitka pro logovani
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            if (module0 != null && module0.Visible)
            {
                try
                {
                    module0.ClosePorts();
                }
                catch { }
            }
            if (module2 != null && module2.Visible)
            {
                try
                {
                    module2.ClosePorts();
                }
                catch { }
            }
             */

            //Pokud je nejaky modul nacten, uzavru mu porty
            if (connector != null)
            {
                connector.ClosePorts();
            }

            //Cekam na nalogovani nebo zruseni
            while (!LogConfig.LogIn(false)) ;

            /*
            if (!LogConfig.Logged())
            {
                CloseModules();
                return;
            }
             */

            //Pokud se nenalogoval, uzavru modul, ..
            if (!LogConfig.Logged())
            {
                if (connector != null)
                {
                    connector.Close();
                    connector = null;
                }
                return;
            }

            //Nalogoval se, pokud je nejaky modul nacteny, vratim porty do puvodniho stavu
            if (connector != null)
            {
                connector.ReturnPortsToPreviousState();
            }

            /*
            if (module0 != null && module0.Visible)
            {
                try
                {
                    module0.ReturnPortsToPreviousState();
                }
                catch { }
            }
            if (module2 != null && module2.Visible)
            {
                try
                {
                    module0.ClosePorts();
                }
                catch { }
            }
             */

            //Uprava jmena
            UpdateFormText();

            //Nacteni modulu, ktery se ma zobrazovat po startu, pokud je nejaky zvolen
            //string.Empty je kontrolovan proto, ze ConfigMod.modulenonestring neni vkladan do databaze, ale pri
            //ukladani zpusobi ulozeni prazdneho stringu do xml souboru - a tak se zadny modul jmenovat nemuze!
            if (Configuration.Config.config.Main[0].ModuleAutoStart != string.Empty)
            {
                LoadModule(Configuration.Config.config.Main[0].ModuleAutoStart);
            }
        }

        //Nacteni formu
        private void frmMainApp_Load(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Settings");
            }

            ////Nacteni obecne konfigurace
            Configuration.Config.Load();
            //Nacteni konfigurace modulu - je nutne je vlozit do okna
            Configuration.ConfigMod.Load();
            ////Nacteni konfigurace loginu
            //LogConfig.Load();

            Fask.Logging.ExceptionHandler2.SetEnablePrint(Configuration.Config.config.Logging[0].Enable_Error);
            

            //Dynamicke vlozeni do okna
            addModulesToWindow();

            LogConfig.LoginChanged += new MethodInvoker(Config_LoginChanged);

            //Zde jeste nikdo nemuze byt prilogovany - loginID == null
            Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.AppStarted, LogConfig.LoginID == null ? string.Empty : LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

            SynchronizationThreadStart(null);

            //nastavit timer synchronizace databaze a spustit jej po startu
            TimerSetInterval();
        }

        //Dynamicke vlozeni nactenych modulu do okna
        private void addModulesToWindow()
        {
            //Vyprazdneni kolekce
            modulyToolStripMenuItem.DropDownItems.Clear();

            //Vkladani moduluu
            foreach (Configuration.ConfigModules.ModulesRow modul in Configuration.ConfigMod.config.Modules.Rows)
            {
                modulyToolStripMenuItem.DropDownItems.Add(Configuration.ConfigMod.createModuleListString((string)modul["Id"], (string)modul["name"]), (Image)FASK.SledovaniVyroby.Main.Properties.Resources._class, new EventHandler(moduleInWindowClick));
            }
        }

        //EventHandler pri kliknuti na nejaky modul v okne
        void moduleInWindowClick(object e, EventArgs args)
        {
            try
            {
                //Zjisteni odkud prislo volani
                ToolStripMenuItem item = e as ToolStripMenuItem;
                //Kontrola - druhe dva by nemely nastat, pro jistotu!
                if (item != null && item.Text.Contains(" ") && item.Text != Configuration.ConfigMod.modulenonestring)
                {
                    string[] moduleInfo = item.Text.Split(new char[] { ' ' });
                    LoadModule(moduleInfo[0]);
                }
            }
            catch (Exception ex)
            {
                //Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        //Zmena loginu
        void Config_LoginChanged()
        {
            this.UpdateFormText();
        }

        //Prvni zobrazei formu
        private void frmMainApp_Shown(object sender, EventArgs e)
        {
            //Zmena velikosti podle rodice
            this.frmMainApp_Resize(sender, e);

            /*
            if (module0 != null)
            {
                try
                {
                    module0.ClosePorts();
                }
                catch { }
            }
             */

            //Cekam na uspesne zalogovani, nebo zruseni
            while (!LogConfig.LogIn(false)) ;

            /*
            if (module0 != null)
            {
                try
                {
                    module0.ReturnPortsToPreviousState();
                }
                catch { }
            }
             */

            //Upraveni nazvu
            UpdateFormText();

            //Vyzkousim nacist automaticky modul - try-catch kvuli moznosti IndexIsOutOfRange
            try
            {
                if (Configuration.Config.config.Main[0].ModuleAutoStart != string.Empty)
                {
                    LoadModule(Configuration.Config.config.Main[0].ModuleAutoStart);
                }
            }
            catch (Exception ex)
            {
                //Log.WriteException(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        //Konec aplikace
        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsReadyToClose())
                return;

            //Uzavru radsi rucne porty a connector, pokud je nejaky modul nacteny
            if (connector != null)
            {
                connector.ClosePorts();
                connector.Close();
            }

            // Log.Write("Ukonceni hlavni aplikace");
            ExceptionHandler2.Handle("Ukonceni hlavni aplikace", "Log_Vyroba", "txt");

            //Uzavreni formu
            this.Close();
        }

        //'Uzavreni' moduluu
        public void CloseModules()
        {
            /*
            if (module0 != null && !module0.Disposing && !module0.IsDisposed && module0.Visible)
            {
                module0.ClosePorts();
                module0.Hide();
            }

            if (module2 != null && !module2.Disposing && !module2.IsDisposed && module2.Visible)
            {
                module2.ClosePorts();
                module2.Hide();
            }
             */

            //Pokud je nejaky otevreny, ..
            if (connector != null)
            {
                //.. uzavru porty, konektor a vynuluji konektor
                connector.ClosePorts();
                connector.Close();
                connector = null;
            }
        }

        //Nacteni modulu
        private void LoadModule(string module)
        {
            //Nekdo musi byt zalogovan
            if (!LogConfig.Logged())
            {
                StatusTextErr("Pro spuštění modulu je nutné přihlášení!");
                return;
            }

            //Musi byt nactena masina
            if (!Configuration.Config.MachineLoad())
            {
                StatusTextErr("Pro spuštění modulu je nutné vybrat stroj!");
                return;
            }

            //Vymazani statusbaru
            StatusTextErr(string.Empty);

            //Uzavreni modulu, pokud uz je nejaky nacteny
            CloseModules();

            //Nacteni modulu podle ID + udaju z tabulky
            foreach (Configuration.ConfigModules.ModulesRow modul in Configuration.ConfigMod.config.Modules.Rows)
            {
                if ((string)modul["Id"] == module)
                {
                    try
                    {
                        //Dynamicke nacteni
                        Assembly executingAssembly = Assembly.GetExecutingAssembly();
                        string binPath = (new Uri(Path.GetDirectoryName(executingAssembly.CodeBase))).AbsolutePath;
                        Assembly assembly = Assembly.LoadFrom(Path.Combine(binPath, (string)modul["Assembly"]));
                        connector = assembly.CreateInstance((string)modul["Object"]) as IModuleConnector;

                        //Zobrazeni
                        connector.NotifyIconState = notifyIcon1;
                        connector.MdiParent = this;
                        //INFO:
                        //status bar neni prozatim v modulu nijak vyuzivan, do budoucna do nej mohou moduly zobrazovat
                        //ruzne zpravy, idealne pomoci vhodne tridy.
                        connector.StatusLabel = tsModuleStatus;
                        string msg;

                        if (connector.IsReadyToShow(out msg))
                        {
                            connector.Show();
                            connector.WindowState = FormWindowState.Maximized;
                        }
                        else
                        {
                            tsModuleStatus.Text = msg;
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Načtení modulu " + (string)modul["Name"] + " nebylo úspěšné.", "Výroba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ExceptionHandler2.Handle(ex);
                    }

                    //Uz jsem nasel modul
                    break;
                }
            }

            /*
            if (module == Modules.clsModules.Rezacka.ID)
            {
                MessageBox.Show("Not implemented module");
            }
            else if (module == Modules.clsModules.VrtackaStara.ID)
            {
                if (module2 == null || module2.Disposing || module2.IsDisposed)
                {
                    module2 = new frmVrtackaStara();
                    module2.NotifyIconState = notifyIcon1;
                    module2.MdiParent = this;
                    module2.Show();
                    module2.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    //MessageBox.Show("Modul vrtačka je již spuštěn", "Výroba");
                    module2.Show();
                    module2.ReturnPortsToPreviousState();
                }
            }
            else if (module == Modules.clsModules.Vrtacka.ID)
            {
                if (module0 == null || module0.Disposing || module0.IsDisposed)
                {
                    module0 = new frmVrtacka();
                    module0.NotifyIconState = notifyIcon1;
                    module0.MdiParent = this;
                    module0.Show();
                    module0.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    //MessageBox.Show("Modul vrtačka je již spuštěn", "Výroba");
                    module0.Show();
                    module0.ReturnPortsToPreviousState();
                }
            }
             */
        }

        //Zobrazeni globalni konfigurace
        private void nastaveniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration.frmGlobalConfiguration frmconfig = new FASK.SledovaniVyroby.Main.Configuration.frmGlobalConfiguration();
            frmconfig.StartPosition = FormStartPosition.CenterParent;
            if (frmconfig.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                //Po zmene nastaveni aktualizuji mozny zmeneny seznam modulu
                addModulesToWindow();
            }

            TimerSetInterval();
        }

        //Nastaveni intervalu timeru
        private void TimerSetInterval()
        {
            try
            {
                if (timer1 == null)
                    timer1 = new System.Threading.Timer(timer1_Tick);



                if (Configuration.Config.config.Main[0].SqlSynchronizationTimeout == 0)
                {
                    timer1.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                }
                else
                {
                    timer1.Change(Configuration.Config.config.Main[0].SqlSynchronizationTimeout * 1000, System.Threading.Timeout.Infinite);
                }
            }
            catch { }
        }

        //Synchronizacni timer
        private void timer1_Tick(object sender)
        {
            //Pozadavek na synchronizaci od uzivatele
            if (sender is ToolStripMenuItem)
            {
                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.DatabaseSynchUsr, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
            }
            //Pozadavek na synchronizaci od aplikace
            else if (sender is Timer)
            {
                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.DatabaseSynchApp, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
            }

            // Synchronizace databaze mezi lokalni stanici a serverem ...
            // tato operace bezi na pozadi.

            //timer1.Enabled = false;
            timer1.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            //SynchronizationThreadStart(sender);
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(SynchronizationThreadStart));
            thread.Start((object)sender);

            //timer1.Enabled = true;
        }

        //bool err = false;
        //string msg = string.Empty;

        //Nastaveni statusu po synchronizaci
        //private void SetStatus()
        //{
        //    if (!err)
        //        StatusText(msg);
        //    else 
        //        StatusTextErr(msg);
        //}

        //Synchronizace dat ze serveru a na server
        private void SynchronizationThreadStart(object sender)
        {
            int errors = 0;
            //if (!(sender is Timer))
            //{

            //Loginy - presun do konfigurace

            try
            {
                if (errors == 0)
                    StatusText(Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (Logins)");
                errors += DownloadTableRemote_Logins();
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                //StatusTextErr = ex.Message;
                errors++;
            }

            System.Threading.Thread.Sleep(1000);


            //Typy masin
            /*
                try
                {
                    if (errors == 0)
                        StatusText = Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (MachineTypes)";
                    errors += UpdateTableLocalMachineTypes();
                }
                catch (Exception ex)
                {
                    ExceptionHandler2.Handle(ex);
                    //StatusTextErr = ex.Message;
                    errors++;
                }

                System.Threading.Thread.Sleep(1000);
             */

            //Masiny - presun do konfigurace

            try
            {
                if (errors == 0)
                    StatusText(Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (Machines)");
                errors += DownloadTableRemote_Machines();
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                //StatusTextErr = ex.Message;
                errors++;
            }

            System.Threading.Thread.Sleep(1000);

            /*JoZ: ToDo...
            try
            {
                if (errors == 0)
                    StatusText = Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (StatusTypes)";
                errors += UpdateTableLocalStatusTypes();
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                //StatusTextErr = ex.Message;
                errors++;
            }
            
            System.Threading.Thread.Sleep(1000);
            */

            try
            {
                if (errors == 0)
                    StatusText(Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (Events)");
                errors += UpdateTableRemoteEvents(UpdateTableRemoteMethodType.RowPerRow);
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                //StatusText = ex.Message;
                errors++;
            }

            System.Threading.Thread.Sleep(1000);

            try
            {
                if (errors == 0)
                    StatusText(Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (UserEvents)");
                errors += UpdateTableRemoteUserEvents(UpdateTableRemoteMethodType.RowPerRow);
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                //StatusText = ex.Message;
                errors++;
            }

            #region Dotazeni VPP a VPH

            try
            {
                if (errors == 0)
                    StatusText(Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (VPH)");
                errors += DownloadTableRemote_VPH();
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                //StatusText = ex.Message;
                errors++;
            }

            try
            {
                if (errors == 0)
                    StatusText(Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (VPP)");
                errors += DownloadTableRemote_VPP();
            }
            catch (Exception ex)
            {
                ExceptionHandler2.Handle(ex);
                //StatusText = ex.Message;
                errors++;
            }

            #endregion

            if (errors == 0)
            {
                //err = false;
                StatusText("Databáze úspěšně synchronizována [" + DateTime.Now.ToString("g") + "]");
                //StatusText = "Databáze úspěšně synchronizována [" + DateTime.Now.ToString("g") + "]";
            }
            else
            {
                //err = true;
                StatusTextErr("Při synchronizaci databáze se vyskytly chyby (" + errors.ToString() + ")");
                //StatusTextErr = "Při synchronizaci databáze se vyskytly chyby (" + errors.ToString() + ")";
            }
            try
            {
                this.BeginInvoke(new MethodInvoker(TimerSetInterval));
                //this.BeginInvoke(new MethodInvoker(SetStatus));
            }
            catch
            {
            }
        }


        private void StatusText(string txt)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() => { StatusText(txt); }));
                    return;
                }

                try
                {
                    tsDatabaseSynchronizationStatus.ForeColor = Color.Black;
                    tsDatabaseSynchronizationStatus.Text = txt;
                }
                catch { }
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }
        }

        private void StatusTextErr(string txt)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() => { StatusTextErr(txt); }));
                    return;
                }

                try
                {
                    tsDatabaseSynchronizationStatus.ForeColor = Color.Red;
                    tsDatabaseSynchronizationStatus.Text = txt;
                }
                catch { }
            }
            catch (Exception ex)
            {

                //Log.Write(ex);
                ExceptionHandler2.Handle(ex);
            }
        }

        #region OLD Nahraní dat z lokalu na server

        //private int UpdateTableLocalLogins()
        //{
        //    int errors = 0;

        //    Database.Vyroba vyroba = new Database.Vyroba();

        //    //Database.VyrobaTableAdapters.FASK_LoginsTableAdapter lta = new Database.VyrobaTableAdapters.FASK_LoginsTableAdapter();
        //    //lta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //    //lta.ClearBeforeFill = true;
        //    //lta.Fill(vyroba.FASK_Logins);

        //    Database.Vyroba.Fill_FASK_Logins(Configuration.Config.config.Main[0].SqlConnectionStringRemote, vyroba.FASK_Logins, true);

        //    foreach (Database.Vyroba.FASK_LoginsRow lrow in vyroba.FASK_Logins.Rows)
        //    {
        //        lrow.SetAdded();
        //    }

        //    SqlConnection sqlconn = null;
        //    try
        //    {
        //        sqlconn = new SqlConnection(Configuration.Config.config.Main[0].SqlConnectionStringLocal);
        //        SqlCommand sqlcomm = new SqlCommand("Delete from FASK_Logins", sqlconn);
        //        sqlconn.Open();
        //        int rowsaffected = sqlcomm.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusTextErr ( ex.Message);
        //        ExceptionHandler2.Handle(ex);
        //        errors++;
        //    }
        //    finally
        //    {
        //        if (sqlconn != null && sqlconn.State == ConnectionState.Open)
        //            sqlconn.Close();
        //    }

        //    try
        //    {
        //        //lta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //        //lta.Update(vyroba);
        //        Database.Vyroba.Update_FASK_Logins(Configuration.Config.config.Main[0].SqlConnectionStringLocal, vyroba.FASK_Logins);
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusTextErr ( ex.Message);
        //        ExceptionHandler2.Handle(ex);
        //        errors++;
        //    }

        //    return errors;
        //}

        //Typy masin
        //private int UpdateTableLocalMachineTypes()
        //{
        //    //Inicializace na 0
        //    int errors = 0;

        //    //Dataset s tabulkami
        //    Database.Vyroba vyroba = new Database.Vyroba();

        //    //Table adapter na praci s tabulkou machine type - vyplneni do pameti
        //    Database.VyrobaTableAdapters.FASK_MachineTypeTableAdapter mtta = new Database.VyrobaTableAdapters.FASK_MachineTypeTableAdapter();
        //    mtta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //    mtta.ClearBeforeFill = true;
        //    mtta.Fill(vyroba.FASK_MachineType);

        //    //Oznaceni jednotlivych radku jako pridane
        //    foreach (Database.Vyroba.FASK_MachineTypeRow mrow in vyroba.FASK_MachineType.Rows)
        //    {
        //        mrow.SetAdded();
        //    }

        //    SqlConnection sqlconn = null;
        //    try
        //    {
        //        //Pripojeni k lokalni db a odtsraneni dosavadnich zaznamu
        //        sqlconn = new SqlConnection(Configuration.Config.config.Main[0].SqlConnectionStringLocal);
        //        SqlCommand sqlcomm = new SqlCommand("Delete from FASK_MachineType", sqlconn);
        //        sqlconn.Open();
        //        int rowsaffected = sqlcomm.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusTextErr(ex.Message);
        //        ExceptionHandler2.Handle(ex);
        //        errors++;
        //    }
        //    finally
        //    {
        //        //Uzavreni spojeni
        //        if (sqlconn != null && sqlconn.State == ConnectionState.Open) sqlconn.Close();
        //    }

        //    try
        //    {
        //        //Commit
        //        mtta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //        mtta.Update(vyroba);
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusTextErr(ex.Message);
        //        ExceptionHandler2.Handle(ex);
        //        errors++;
        //    }

        //    //Vracim pocet chyb
        //    return errors;
        //}

        ////Masiny
        //private int UpdateTableLocalMachines()
        //{
        //    int errors = 0;

        //    Database.Vyroba vyroba = new Database.Vyroba();

        //    //Database.VyrobaTableAdapters.FASK_MachinesTableAdapter mta = new Database.VyrobaTableAdapters.FASK_MachinesTableAdapter();
        //    //mta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //    //mta.ClearBeforeFill = true;
        //    //mta.Fill(vyroba.FASK_Machines);

        //    Database.Vyroba.Fill_FASK_Machines(Configuration.Config.config.Main[0].SqlConnectionStringRemote, vyroba.FASK_Machines, true);

        //    foreach (Database.Vyroba.FASK_MachinesRow lrow in vyroba.FASK_Machines.Rows)
        //    {
        //        lrow.SetAdded();
        //    }

        //    foreach (Database.Vyroba.FASK_MachinesRow mrow in vyroba.FASK_Machines.Rows)
        //    {
        //        mrow.SetAdded();
        //    }

        //    SqlConnection sqlconn = null;
        //    try
        //    {
        //        sqlconn = new SqlConnection(Configuration.Config.config.Main[0].SqlConnectionStringLocal);
        //        SqlCommand sqlcomm = new SqlCommand("Delete from FASK_Machines", sqlconn);
        //        sqlconn.Open();
        //        int rowsaffected = sqlcomm.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusTextErr(ex.Message);
        //        ExceptionHandler2.Handle(ex);
        //        errors++;
        //    }
        //    finally
        //    {
        //        if (sqlconn != null && sqlconn.State == ConnectionState.Open)
        //            sqlconn.Close();
        //    }

        //    try
        //    {
        //        mta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //        mta.Update(vyroba);
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusTextErr(ex.Message);
        //        ExceptionHandler2.Handle(ex);
        //        errors++;
        //    }

        //    return errors;
        //}

        //private int UpdateTableLocalStatusTypes()
        //{
        //    int errors = 0;

        //    Database.Vyroba vyroba = new Database.Vyroba();

        //    Database.VyrobaTableAdapters.FASK_StatusTypesTableAdapter stta = new Database.VyrobaTableAdapters.FASK_StatusTypesTableAdapter();
        //    stta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //    stta.ClearBeforeFill = true;
        //    stta.Fill(vyroba.FASK_StatusTypes);

        //    foreach (Database.Vyroba.FASK_StatusTypesRow strow in vyroba.FASK_StatusTypes.Rows)
        //    {
        //        strow.SetAdded();
        //    }

        //    SqlConnection sqlconn = null;
        //    try
        //    {
        //        sqlconn = new SqlConnection(Configuration.Config.config.Main[0].SqlConnectionStringLocal);
        //        SqlCommand sqlcomm = new SqlCommand("Delete from FASK_StatusTypes", sqlconn);
        //        sqlconn.Open();
        //        int rowsaffected = sqlcomm.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusTextErr(ex.Message);
        //        ExceptionHandler2.Handle(ex);
        //        errors++;
        //    }
        //    finally
        //    {
        //        if (sqlconn != null && sqlconn.State == ConnectionState.Open)
        //            sqlconn.Close();
        //    }

        //    try
        //    {
        //        stta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //        stta.Update(vyroba);
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusTextErr(ex.Message);
        //        ExceptionHandler2.Handle(ex);
        //        errors++;
        //    }

        //    return errors;
        //}

        #endregion

        #region Nahraní dat z lokalu na server

        private enum UpdateTableRemoteMethodType
        {
            DataAdapters,
            RowPerRow
        }

        private int UpdateTableRemoteEvents(UpdateTableRemoteMethodType updateMethodType)
        {
            int errors = 0;

            switch (updateMethodType)
            {
                case UpdateTableRemoteMethodType.RowPerRow:
                    {
                        ICommDatabase.DSVyroba vyroba = new ICommDatabase.DSVyroba();

                        vyroba = Database.Classes.Vyroba_Local.getVyrobaEventsData(Configuration.Config.config.Main[0].SqlConnectionStringLocal);

                        /* JoZ: uprava, prace s knihovnami...ToDo: upravit vsude!!! synchronizace server, errtable,... (ted neni cas)
                        Database.Vyroba vyroba = new Database.Vyroba();
                        int rowsaffected = 0;
                        
                        Database.VyrobaTableAdapters.FASK_EventsTableAdapter eta = new Database.VyrobaTableAdapters.FASK_EventsTableAdapter();
                        eta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
                        eta.ClearBeforeFill = true;
                        rowsaffected = eta.Fill(vyroba.FASK_Events);
                 
                        Database.VyrobaTableAdapters.FASK_EventsErrTableAdapter etaErr = new Database.VyrobaTableAdapters.FASK_EventsErrTableAdapter();
                        etaErr.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
                        etaErr.ClearBeforeFill = true;
                        */
                        //Database.VyrobaTableAdapters.FASK_EventsTableAdapter etaCentral = new Database.VyrobaTableAdapters.FASK_EventsTableAdapter();
                        //etaCentral.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
                        //etaCentral.Connection = new SqlConnection(Configuration.Config.config.Main[0].SqlConnectionStringRemote);
                        //etaCentral.ClearBeforeFill = true;

                        try
                        {
                            //eta.Connection.Open();
                            //etaCentral.Connection.Open();
                            //etaErr.Connection.Open();


                            foreach (ICommDatabase.DSVyroba.FASK_EventsRow erow in vyroba.FASK_Events.Rows)
                            {
                                int raffLokal = 0;
                                int raffCentral = 0;
                                int raffLokalErr = 0;

                                try
                                {   // inicializacia transakcii

                                    using (TransactionScope s = new TransactionScope())
                                    {
                                        //int? rowsWithGUID = etaCentral.CountGUID(erow.faskGUID);
                                        int? rowsWithGUID = Database.Classes.Vyroba_Remote.FASK_Events_CountGUID(Configuration.Config.config.Main[0].SqlConnectionStringRemote, erow.faskGUID, int.Parse(LogConfig.MachineID));


                                        if (rowsWithGUID.HasValue && rowsWithGUID.Value == 0)
                                        {   // neexistuje duplicitny zaznam
                                            #region master puvodny
                                            // raffCentral = etaCentral.Insert(
                                            //     erow.loginid,
                                            //     erow.machineid,
                                            //     erow.dateeve,
                                            //     erow.qty,
                                            //     erow.qtyReal,
                                            //     erow.description,
                                            //     erow.barcodeReaded,
                                            //     erow.barcodeSended,
                                            //     erow.zakazka,
                                            //     erow.popis,
                                            //     erow.faskGUID,
                                            //     erow.reportType,
                                            //     erow.IDO,
                                            //     erow.scan1,
                                            //     erow.scan2,
                                            //     erow.scan3,
                                            //     erow.sensor,
                                            //     erow.material,
                                            //     erow.VPH,
                                            //     erow.VPPol,
                                            //     erow.EAN_IS,
                                            //     erow.IS_ID,
                                            //     erow.IsstatusNull() ? (int?)null: erow.status,
                                            //     erow.NMBRPAL,
                                            //     erow.IsproductionGuidNull() ? null : (Guid?)erow.productionGuid,
                                            //     erow.QTYPACK,
                                            //     erow.IsPackTypeNull() ? string.Empty : erow.PackType,
                                            //     erow.IsWEIGHTNull() ? null : (decimal?)erow.WEIGHT,
                                            //     erow.BarcodeT,
                                            //     erow.IsREZ_1Null() ? null : erow.REZ_1,
                                            //     erow.IsREZ_2Null() ? null : erow.REZ_2,
                                            //     erow.IsREZ_3Null() ? null : erow.REZ_3,
                                            //     erow.IsREZ_4Null() ? null : erow.REZ_4,
                                            //     erow.IsREZ_5Null() ? null : erow.REZ_5
                                            //     );
                                            #endregion
                                            //raffCentral = etaCentral.Insert(
                                            //    erow.loginid,
                                            //    erow.IsmachineidNull() ? null : erow.machineid,
                                            //    erow.dateeve,
                                            //    erow.qty,
                                            //    erow.qtyReal,
                                            //    erow.IsdescriptionNull() ? null : erow.description,
                                            //    erow.barcodeReaded,
                                            //    erow.barcodeSended,
                                            //    erow.IszakazkaNull() ? null : erow.zakazka,
                                            //    erow.IspopisNull() ? null : erow.popis,
                                            //    erow.faskGUID,
                                            //    erow.reportType,
                                            //    erow.IsIDONull() ? null : erow.IDO,
                                            //    erow.Isscan1Null() ? null : erow.scan1,
                                            //    erow.Isscan2Null() ? null : erow.scan2,
                                            //    erow.Isscan3Null() ? null : erow.scan3,
                                            //    erow.IssensorNull() ? null : erow.sensor,
                                            //    erow.IsmaterialNull() ? null : erow.material,
                                            //    erow.IsVPHNull() ? null : erow.VPH,
                                            //    erow.IsVPPolNull() ? -1 : erow.VPPol,
                                            //    erow.IsEAN_ISNull() ? null : erow.EAN_IS,
                                            //    erow.IsIS_IDNull() ? null : erow.IS_ID,
                                            //    erow.IsstatusNull() ? (int?)null: erow.status,
                                            //    erow.IsNMBRPALNull() ? null : erow.NMBRPAL,
                                            //    erow.IsproductionGuidNull() ? null : (Guid?)erow.productionGuid,
                                            //    erow.QTYPACK,
                                            //    erow.IsPackTypeNull() ? string.Empty : erow.PackType,
                                            //    erow.IsWEIGHTNull() ? null : (decimal?)erow.WEIGHT
                                            //    );

                                            // TODO: Upravit metodu FASK_EventsInsert aby vracena INT počet zpracovaných řádků
                                            // zavísí od toho metoda dole, která odmazává

                                            raffCentral = Database.Classes.Vyroba_Remote.FASK_EventsInsert(
                                            Configuration.Config.config.Main[0].SqlConnectionStringRemote,
                                            erow.loginid,
                                            erow.IsmachineidNull() ? null : erow.machineid,
                                            erow.dateeve,
                                            erow.qty,
                                            erow.qtyReal,
                                            erow.IsdescriptionNull() ? null : erow.description,
                                            erow.barcodeReaded,
                                            erow.barcodeSended,
                                            erow.IszakazkaNull() ? null : erow.zakazka,
                                            erow.IspopisNull() ? null : erow.popis,
                                            erow.faskGUID,
                                            erow.reportType,
                                            erow.IsIDONull() ? null : erow.IDO,
                                            erow.Isscan1Null() ? null : erow.scan1,
                                            erow.Isscan2Null() ? null : erow.scan2,
                                            erow.Isscan3Null() ? null : erow.scan3,
                                            erow.IssensorNull() ? string.Empty : erow.sensor,
                                            erow.IsmaterialNull() ? null : erow.material,
                                            erow.IsVPHNull() ? null : erow.VPH,
                                            erow.IsVPPolNull() ? -1 : erow.VPPol,
                                            erow.IsEAN_ISNull() ? null : erow.EAN_IS,
                                            erow.IsIS_IDNull() ? null : erow.IS_ID,
                                            erow.IsstatusNull() ? (int?)null : erow.status,
                                            erow.IsNMBRPALNull() ? null : erow.NMBRPAL,
                                            erow.IsproductionGuidNull() ? null : (Guid?)erow.productionGuid,
                                            erow.QTYPACK,
                                            erow.IsPackTypeNull() ? string.Empty : erow.PackType,
                                            erow.IsWEIGHTNull() ? null : (decimal?)erow.WEIGHT,
                                            erow.IsBarcodeTNull() ? (byte)0 : erow.BarcodeT,
                                            erow.IsREZ_1Null() ? string.Empty : erow.REZ_1,
                                            erow.IsREZ_2Null() ? string.Empty : erow.REZ_2,
                                            erow.IsREZ_3Null() ? string.Empty : erow.REZ_3,
                                            erow.IsREZ_4Null() ? string.Empty : erow.REZ_4,
                                            erow.IsREZ_5Null() ? string.Empty : erow.REZ_5
                                            );


                                        }
                                        else
                                        {   // duplicitny zaznam vlozime (logujeme) do Err tabulky
                                            //raffLokalErr = etaErr.Insert(erow.loginid, erow.machineid, erow.dateeve, erow.qty, erow.qtyReal, erow.description, erow.barcodeReaded, erow.barcodeSended, erow.zakazka, erow.popis, erow.faskGUID, erow.reportType,null,null,null,null,null);
                                            raffLokalErr = Database.Classes.Vyroba_Local.EventsErrInsert(
                                                erow.loginid,
                                                Configuration.Config.config.Main[0].SqlConnectionStringLocal,
                                                erow.qty,
                                                erow.qtyReal,
                                                erow.IsdescriptionNull() ? null : erow.description,
                                                erow.barcodeReaded,
                                                erow.barcodeSended,
                                                 erow.IszakazkaNull() ? null : erow.zakazka,
                                                 erow.IspopisNull() ? null : erow.popis,
                                                 erow.reportType,
                                                erow.IsIDONull() ? null : erow.IDO,
                                                erow.Isscan1Null() ? null : erow.scan1,
                                                erow.Isscan2Null() ? null : erow.scan2,
                                                erow.Isscan3Null() ? null : erow.scan3,
                                                erow.IssensorNull() ? string.Empty : erow.sensor,
                                                erow.IsmaterialNull() ? null : erow.material);
                                        }
                                    }

                                    //JoZ: uprava mimo TransactionScope...kvuli Open()
                                    // zmazame z lokalnej tabulky
                                    if (raffCentral > 0 || raffLokalErr > 0)
                                        raffLokal = Database.Classes.Vyroba_Local.DeleteEvent(erow.id);
                                    //raffLokal = eta.Delete(erow.id);
                                }
                                catch (Exception ex)
                                {
                                    StatusTextErr(ex.Message);
                                    ExceptionHandler2.Handle(ex);
                                    errors++;
                                }

                            }

                        }
                        finally
                        {
                            //if ((etaCentral.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                            //    etaCentral.Connection.Close();
                            //if ((eta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                            //    eta.Connection.Close();
                            //if ((etaErr.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                            //    etaErr.Connection.Close();
                        }
                    }
                    break;
            }

            return errors;
        }


        private int UpdateTableRemoteUserEvents(UpdateTableRemoteMethodType updateMethodType)
        {
            int errors = 0;

            switch (updateMethodType)
            {
                case UpdateTableRemoteMethodType.RowPerRow:
                    {

                        ICommDatabase.DSVyroba vyroba = new ICommDatabase.DSVyroba();

                        vyroba = Database.Classes.Vyroba_Local.getVyrobaUserEventsData(Configuration.Config.config.Main[0].SqlConnectionStringLocal);

                        //Database.VyrobaTableAdapters.FASK_UserEventsTableAdapter uetaCentral = new Database.VyrobaTableAdapters.FASK_UserEventsTableAdapter();
                        ////uetaCentral.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
                        //uetaCentral.Connection = new SqlConnection(Configuration.Config.config.Main[0].SqlConnectionStringRemote);
                        //uetaCentral.ClearBeforeFill = true;

                        //Database.Vyroba2TableAdapters.FASK_UserEventsTableAdapter uetaCentral2 = new Database.Vyroba2TableAdapters.FASK_UserEventsTableAdapter();
                        //uetaCentral2.Connection.ConnectionString = "Provider=SQLOLEDB;" + Configuration.Config.config.Main[0].SqlConnectionStringRemote;
                        //uetaCentral2.ClearBeforeFill = true;

                        /*
                        int rowsaffected = 0;
                        Database.Vyroba vyroba = new Database.Vyroba();

                        Database.VyrobaTableAdapters.FASK_UserEventsTableAdapter ueta = new Database.VyrobaTableAdapters.FASK_UserEventsTableAdapter();
                        ueta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
                        ueta.ClearBeforeFill = true;
                        rowsaffected = ueta.Fill(vyroba.FASK_UserEvents);

                        Database.VyrobaTableAdapters.FASK_UserEventsErrTableAdapter uetaErr = new Database.VyrobaTableAdapters.FASK_UserEventsErrTableAdapter();
                        uetaErr.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
                        uetaErr.ClearBeforeFill = true;
                        */

                        try
                        {
                            //ueta.Connection.Open();
                            //uetaCentral.Connection.Open();
                            //uetaCentral2.Connection.Open();
                            //uetaErr.Connection.Open();

                            foreach (ICommDatabase.DSVyroba.FASK_UserEventsRow uerow in vyroba.FASK_UserEvents.Rows)
                            {
                                int raffLokal = 0;
                                int raffCentral = 0;
                                int raffLokalErr = 0;

                                try
                                {
                                    using (TransactionScope s = new TransactionScope())
                                    {

                                        //int? rowsWithGUID = uetaCentral.CountGUID(uerow.faskGUID);
                                        int? rowsWithGUID =  Database.Classes.Vyroba_Remote.FASK_UserEvents_CountGUID(Configuration.Config.config.Main[0].SqlConnectionStringRemote,  uerow.faskGUID, int.Parse(LogConfig.MachineID));
                                        //int? rowsWithGUID = (int)uetaCentral.CountGUID(uerow.faskGUID);
                                        if (rowsWithGUID.HasValue && rowsWithGUID.Value == 0)
                                        {   // neexistuje duplicitny zaznam

                                            //raffCentral = uetaCentral.Insert(
                                            //    string.IsNullOrEmpty(uerow.loginid) ? string.Empty : uerow.loginid,
                                            //    uerow.machineid,
                                            //    uerow.dateeve,
                                            //    string.IsNullOrEmpty(uerow.statusid) ? string.Empty : uerow.statusid,
                                            //    uerow.faskGUID,
                                            //    uerow.Isrez_1Null() ? string.Empty : uerow.rez_1,
                                            //    uerow.Isrez_2Null() ? string.Empty : uerow.rez_2
                                            //    );

                                            Database.Classes.Vyroba_Remote.FASK_UserEventsInsert(
                                            Configuration.Config.config.Main[0].SqlConnectionStringRemote,
                                            string.IsNullOrEmpty(uerow.loginid) ? string.Empty : uerow.loginid,
                                            uerow.machineid,
                                            uerow.dateeve,
                                            string.IsNullOrEmpty(uerow.statusid) ? string.Empty : uerow.statusid,
                                            uerow.faskGUID,
                                            uerow.Isrez_1Null() ? string.Empty : uerow.rez_1,
                                            uerow.Isrez_2Null() ? string.Empty : uerow.rez_2
                                            );

                                            //raffCentral = uetaCentral2.Insert(uerow.loginid, uerow.machineid, uerow.dateeve, uerow.statusid, uerow.faskGUID, uerow.rez_1);
                                        }
                                        else
                                        {   // duplicitny zaznam vlozime (logujeme) do Err tabulky
                                            //raffLokalErr = uetaErr.Insert(uerow.loginid, uerow.machineid, uerow.dateeve, uerow.statusid, uerow.faskGUID);
                                            raffLokalErr = Database.Classes.Vyroba_Local.UserEventsErrInsert(uerow.id, uerow.statusid, uerow.loginid, uerow.machineid, Configuration.Config.config.Main[0].SqlConnectionStringLocal);
                                        }
                                    }

                                    //JoZ: uprava mimo TransactionScope...kvuli Open()
                                    if (raffCentral > 0 || raffLokalErr > 0)
                                        raffLokal = Database.Classes.Vyroba_Local.DeleteUserEvent(uerow.id);
                                    //raffLokal = ueta.Delete(uerow.id);
                                    //}

                                }
                                catch (Exception ex)
                                {
                                    errors++;
                                    StatusTextErr(ex.Message);
                                    ExceptionHandler2.Handle(ex);

                                }

                            }

                        }
                        finally
                        {
                            //if ((uetaCentral.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                            //    uetaCentral.Connection.Close();
                            //if ((ueta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                            //    ueta.Connection.Close();
                            //if ((uetaErr.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                            //    uetaErr.Connection.Close();
                        }
                    }
                    break;

            }

            return errors;
        }


        #endregion

        #region Stažení VPP, VPH, Logins a Machines z serveru na lokal

        private int DownloadTableRemote_VPP()
        {
            int errors = 0;
            try
            {
                ICommDatabase.DSVyroba.CZPRO_VPPDataTable dt = Database.Classes.Vyroba_Remote.getVyroba_CZPRO_VPP(Configuration.Config.config.Main[0].SqlConnectionStringRemote, int.Parse(LogConfig.MachineID));

                foreach (var item in dt)
                {
                    item.SetAdded();
                }

                Database.Classes.Vyroba_Local.Delete_VPP(Configuration.Config.config.Main[0].SqlConnectionStringLocal);
                Database.Classes.Vyroba_Local.upload_VPP(dt, Configuration.Config.config.Main[0].SqlConnectionStringLocal);
            }
            catch (Exception ex)
            {
                errors++;
                StatusTextErr(ex.Message);
                ExceptionHandler2.Handle(ex);
            }

            return errors;
        }

        private int DownloadTableRemote_VPH()
        {
            int errors = 0;
            try
            {
                ICommDatabase.DSVyroba.CZPRO_VPHDataTable dt = Database.Classes.Vyroba_Remote.getVyroba_CZPRO_VPH(Configuration.Config.config.Main[0].SqlConnectionStringRemote, int.Parse(LogConfig.MachineID));

                foreach (var item in dt)
                {
                    item.SetAdded();
                }

                Database.Classes.Vyroba_Local.Delete_VPH(Configuration.Config.config.Main[0].SqlConnectionStringLocal);
                Database.Classes.Vyroba_Local.upload_VPH(dt, Configuration.Config.config.Main[0].SqlConnectionStringLocal);
            }
            catch (Exception ex)
            {
                errors++;
                StatusTextErr(ex.Message);
                ExceptionHandler2.Handle(ex);
            }

            return errors;
        }

        private int DownloadTableRemote_Logins()
        {
            int errors = 0;
            try
            {
                ICommDatabase.DSVyroba.FASK_LoginsDataTable dt = Database.Classes.Vyroba_Remote.Get_FASK_Logins(Configuration.Config.config.Main[0].SqlConnectionStringRemote, int.Parse(LogConfig.MachineID));

                foreach (var item in dt)
                {
                    item.SetAdded();
                }

                Database.Classes.Vyroba_Local.Delete_FASK_Logins(Configuration.Config.config.Main[0].SqlConnectionStringLocal);
                Database.Classes.Vyroba_Local.upload_FASK_Logins(dt, Configuration.Config.config.Main[0].SqlConnectionStringLocal);
            }
            catch (Exception ex)
            {
                errors++;
                StatusTextErr(ex.Message);
                ExceptionHandler2.Handle(ex);
            }

            return errors;
        }

        private int DownloadTableRemote_Machines()
        {
            int errors = 0;
            try
            {
                ICommDatabase.DSVyroba.FASK_MachinesDataTable dt = Database.Classes.Vyroba_Remote.Get_FASK_Machines(Configuration.Config.config.Main[0].SqlConnectionStringRemote, int.Parse(LogConfig.MachineID));

                foreach (var item in dt)
                {
                    item.SetAdded();
                }

                Database.Classes.Vyroba_Local.Delete_FASK_Machines(Configuration.Config.config.Main[0].SqlConnectionStringLocal);
                Database.Classes.Vyroba_Local.upload_FASK_Machines(dt, Configuration.Config.config.Main[0].SqlConnectionStringLocal);
            }
            catch (Exception ex)
            {
                errors++;
                StatusTextErr(ex.Message);
                ExceptionHandler2.Handle(ex);
            }

            return errors;
        }

        #endregion

        private void synchronizaceDatabazeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Configuration.Config.config.Main[0].SqlSynchronizationTimeout > 0)
            {
                timer1_Tick(sender);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void frmMainApp_Resize(object sender, EventArgs e)
        {
            Properties.Settings.Default.VyrobaWindowState = this.WindowState;
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void logoutToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (!IsReadyToClose())
                return;

            if (LogConfig.LogOut())
                CloseModules();
        }

        private bool IsReadyToClose()
        {
            string message = string.Empty;
            if (connector != null)
            {
                if (!connector.IsReadyToClose(out message))
                {
                    MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;
        }

        private void logoutToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            LogConfig.LogOut();
        }

        private void frmMainApp_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if (LogConfig.Logged())
                    {
                        MessageBox.Show("Před ukončením aplikace je nutné odhlášení (Logout).", "Ukončení aplikace", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                        return;
                    }

                    using (FormInputKod frmInputKod = new FormInputKod())
                    {
                        frmInputKod.WindowState = FormWindowState.Maximized;

                        frmInputKod.Text_msg = "Zadejte heslo pro ukončení";
                        frmInputKod.ShowKod = false;
                        frmInputKod.Owner = this;
                        //Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FormMain - ukončení aplikace: " + "Pokus o ukončení aplikace");
                        ExceptionHandler2.Handle(LogLevel.Debug, "FormMain - ukončení aplikace: " + "Pokus o ukončení aplikace");


                        bool result = true;
                        do
                        {
                            if (frmInputKod.ShowDialog(this) == DialogResult.OK)
                            {
                                try
                                {

                                    if (frmInputKod.Kod == "159")
                                    {
                                        // e.Cancel = true;
                                        result = false;
                                        //return;
                                        //e.Cancel = true;
                                        // return;
                                    }
                                    else
                                    {
                                        #region komunikace API
                                        // API komunikace

                                        IRestResponse restResponse;
                                        string param = "UkonceniAplikace_odhlaseni/" + frmInputKod.Kod.Trim();

                                        // o = GetSSCC_zaznam(MachineID, Status, StatusNew);



                                        if (!Vyroba.Classes.WEBAPI.Comunication.Communicate(Vyroba.Classes.WEBAPI.Comunication.REST_Type.GET, out restResponse, param, string.Empty, LogConfig.MachineID, "SledovaniVyroby"))
                                        {
                                            //throw new Exception("Komunikace s IS AGRO se nezdařila");
                                            //Log.Write("Komunikace s IS AGRO se nezdarila");
                                            ExceptionHandler2.Handle(LogLevel.Error, "Komunikace s IS AGRO se nezdarila--" + restResponse.Content);
                                        }

                                        if (restResponse.StatusCode == HttpStatusCode.OK)
                                        {
                                            result = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(restResponse.Content);
                                            //zapis do DB status 1
                                            //  result = false;
                                            //e.Cancel = true;
                                            //return;
                                        }
                                        else
                                        {
                                            //FlexibleMessageBox.Show(this, "Špatné heslo", "Chyba", MessageBoxButtons.OK);
                                            //Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "FormMain - ukončení aplikace uživatelem" + "Špatné heslo");
                                            ExceptionHandler2.Handle(LogLevel.Debug, "FormMain - ukončení aplikace uživatelem" + "Špatné heslo");
                                            frmInputKod.Text_msg = "Špatné heslo!! Zadejte heslo znovu!";
                                            frmInputKod.Text_color = Color.Red;
                                            frmInputKod.Kod = string.Empty;
                                            //e.Cancel = true;
                                            //return;
                                        }

                                        #endregion
                                    }



                                }
                                catch (Exception ex)
                                {
                                    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                                    // TODO : dokud nebude aktualni server, tak umoznit kilnout aplikaci kodem 159
                                    if (frmInputKod.Kod == "159")
                                    {
                                        //e.Cancel = true;
                                        result = false;
                                        //e.Cancel = true;
                                        // return;
                                    }
                                    else
                                    {
                                        frmInputKod.Text_msg = "Špatné heslo!! Zadejte heslo znovu!";
                                        frmInputKod.Text_color = Color.Red;
                                        frmInputKod.Kod = string.Empty;
                                    }

                                    ExceptionHandler2.Handle(ex);
                                }
                            }
                            else
                            {
                                result = false;
                                e.Cancel = true;
                                //return;
                            }
                        } while (result);
                    }

                    //Fask.Logging.ExceptionHandler2.Handle(Logging.LogLevel.Debug, "application stop");
                    ExceptionHandler2.Handle(LogLevel.Debug, "application stop");

                }
            }
            catch (Exception ex)
            {

                ExceptionHandler2.Handle(ex);
            }



            try
            {
                Database.Classes.Vyroba_Local.UserEventsInsert(StatusTypesEnum.AppEnded, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                Properties.Settings.Default.Save();
            }
            catch (Exception ex) { ExceptionHandler2.Handle(ex); }
        }

        private void errorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErrorLogDialog eld = new ErrorLogDialog();
            eld.ShowDialog();
        }

        private void kioskModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //kioskMódToolStripMenuItem.Checked = kioskMódToolStripMenuItem.Checked ? false : true;

            //this.fullScreen.ShowFullScreen();
        }

        //Metoda pro odeslani emailu.
        private void sendEmail(Exception ex)
        {
            //Nacteni konfigurace z konfiguracniho souboru
            Email.LoadConfiguration();
            //Vlozeni Tela zpravy
            Email.Body = ex.Source + " : " + ex.Message;
            //Vytvoreni zpravy
            Email.CreateEmailMessage();
            //Odeslani
            Email.SendEmailMessageAsynch();
        }

        private void tsmi_AboutBox_Click(object sender, EventArgs e)
        {
            try
            {
                using (AboutBox abox = new AboutBox())
                {
                    abox.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}