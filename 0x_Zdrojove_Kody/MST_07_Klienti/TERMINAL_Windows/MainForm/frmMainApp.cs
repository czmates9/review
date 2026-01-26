using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Transactions;
using FASK.MST_WINDOWS.ErrorLog;
using FASK.MST_WINDOWS.Logging;
using FASK.MST_WINDOWS.ModuleIfc;
using System.Reflection;
using System.IO;
using Fask.Emailing;
using FASK.MST_WINDOWS.Main.Configuration;

namespace FASK.MST_WINDOWS.Main
{
    public partial class frmMainApp : Form
    {
        /*
        private frmVrtacka module0 = null;
        private frmVrtackaStara module2 = null;
         */
         
        //Connector pro pripojovani modulu
        IModuleConnector connector = null;
        //FullScreen fullScreen = null;

        public FASK.MST_WINDOWS.Main._WebRefernces_Globals.CiselnikServiceSession ciselnikS = null;
        LoginService.LoginService loginser = null;


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

            this.DataBindings.Add(new System.Windows.Forms.Binding("WindowState", global::FASK.MST_WINDOWS.Main.Properties.Settings.Default, "VyrobaWindowState", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
        }

        //Zobrazeni informaci
        private void UpdateFormText()
        {
            this.Text = "MST Windows " + " (" + this.ProductVersion.ToString() + ")";
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
                FASK.MST_WINDOWS.Main.Properties.Settings.Default.Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Settings");
            }

            ////Nacteni obecne konfigurace
            Configuration.Config.Load();
            //Nacteni konfigurace modulu - je nutne je vlozit do okna
            //Configuration.ConfigMod.Load();
            ////Nacteni konfigurace loginu
            //LogConfig.Load();

            //Dynamicke vlozeni do okna
            addModulesToWindow();

            LogConfig.LoginChanged += new MethodInvoker(Config_LoginChanged);

            //Zde jeste nikdo nemuze byt prilogovany - loginID == null
            //Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.AppStarted, LogConfig.LoginID == null ? string.Empty : LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

            //nastavit timer synchronizace databaze a spustit jej po startu
            //if (false)
            //{
            //    TimerSetInterval();
            //    timer1.Start(); 
            //}

        }

        //Dynamicke vlozeni nactenych modulu do okna
        private void addModulesToWindow()
        {
            //Vyprazdneni kolekce
            modulyToolStripMenuItem.DropDownItems.Clear();

            //Vkladani moduluu
            foreach (GlobalConfig.ModulesRow modul in Config.config.Modules.Rows)
            {
                modulyToolStripMenuItem.DropDownItems.Add(Configuration.Config.createModuleListString((string)modul["Id"], (string)modul["name"]), (Image)FASK.MST_WINDOWS.Main.Properties.Resources._class, new EventHandler(moduleInWindowClick));
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
                if (item != null && item.Text.Contains(" ") && item.Text != Config.modulenonestring)
                {
                    string[] moduleInfo = item.Text.Split(new char[] { ' ' });
                    LoadModule(moduleInfo[0]);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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

            ciselnikS = new FASK.MST_WINDOWS.Main._WebRefernces_Globals.CiselnikServiceSession();
            ciselnikS.Url = Config.Main_KomServer + "Ciselnik.asmx";
            ciselnikS.Timeout =  Properties.Settings.Default.TimeOut_Ciselniky;
            //ciselnikS.UpdateWebServiceCredentials(); // ?? 

            loginser = new FASK.MST_WINDOWS.Main.LoginService.LoginService();
            loginser.Timeout = Properties.Settings.Default.TimeOut_Ciselniky;
            loginser.Url = Config.Main_KomServer + "LoginService.asmx";
            //loginser.UpdateWebServiceCredentials();


            

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
                Log.WriteException(ex);
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

            Log.Write("Ukonceni hlavni aplikace");

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
                StatusTextErr = "Pro spuštìní modulu je nutné pøihlášení!";
                return;
            }

            ////Musi byt nactena masina
            //if (!Configuration.Config.MachineLoad())
            //{
            //    StatusTextErr = "Pro spuštìní modulu je nutné vybrat stroj!";
            //    return;
            //}

            //Vymazani statusbaru
            StatusTextErr = string.Empty;

            //Uzavreni modulu, pokud uz je nejaky nacteny
            CloseModules();

            //Nacteni modulu podle ID + udaju z tabulky
            foreach (GlobalConfig.ModulesRow modul in Config.config.Modules.Rows)
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
                        connector.Show();
                        connector.WindowState = FormWindowState.Maximized;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Naètení modulu " + (string)modul["Name"] + " nebylo úspìšné.", "Výroba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Log.WriteException(ex);
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
                    //MessageBox.Show("Modul vrtaèka je již spuštìn", "Výroba");
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
                    //MessageBox.Show("Modul vrtaèka je již spuštìn", "Výroba");
                    module0.Show();
                    module0.ReturnPortsToPreviousState();
                }
            }
             */
        }

        //Zobrazeni globalni konfigurace
        private void nastaveníToolStripMenuItem_Click(object sender, EventArgs e)
        {
      
            Configuration.frmGlobalConfiguration frmconfig = new FASK.MST_WINDOWS.Main.Configuration.frmGlobalConfiguration();
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

            //TimerSetInterval();
        }

        //Nastaveni intervalu timeru
        //private void TimerSetInterval()
        //{
        //    try
        //    {
        //        //INFO: zmena synchronizacniho intervalu na sekundy.
        //        //timer1.Interval = Configuration.Config.config.Main[0].SqlSynchronizationTimeout * 60 * 1000;
        //        timer1.Interval = Configuration.Config.config.Main[0].SqlSynchronizationTimeout * 1000;
        //    }
        //    catch { }
        //}

        //Synchronizacni timer
        //private void timer1_Tick(object sender, EventArgs e)
        //{

        //    //Pozadavek na synchronizaci od uzivatele
        //    if (sender is ToolStripMenuItem)
        //    {
        //        Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.DatabaseSynchUsr, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        //    }
        //    //Pozadavek na synchronizaci od aplikace
        //    else if (sender is Timer)
        //    {
        //        Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.DatabaseSynchApp, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        //    }

        //    // Synchronizace databaze mezi lokalni stanici a serverem ...
        //    // tato operace bezi na pozadi.

        //    //timer1.Enabled = false;
        //    if(timer1 != null)
        //        timer1.Stop();

        //    //SynchronizationThreadStart(sender);
        //    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(SynchronizationThreadStart));
        //    thread.Start((object)sender);

        //    //timer1.Enabled = true;
        //}

        bool err = false;
        string msg = string.Empty;

        //Nastaveni statusu po synchronizaci
        private void SetStatus()
        {
            if (!err) StatusText = msg;
            else StatusTextErr = msg;
        }

        //Synchronizace dat ze serveru a na server
        //private void SynchronizationThreadStart(object sender)
        //{
        //    int errors = 0;
        //    //if (!(sender is Timer))
        //    //{

        //    //Loginy - presun do konfigurace
        //    /*
        //    try
        //    {
        //        if (errors == 0)
        //            StatusText = Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (Logins)";
        //        errors += UpdateTableLocalLogins();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        //StatusTextErr = ex.Message;
        //        errors++;
        //    }

        //    System.Threading.Thread.Sleep(1000);
        //     */

        //    //Typy masin
        //    /*
        //        try
        //        {
        //            if (errors == 0)
        //                StatusText = Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (MachineTypes)";
        //            errors += UpdateTableLocalMachineTypes();
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.WriteException(ex);
        //            //StatusTextErr = ex.Message;
        //            errors++;
        //        }

        //        System.Threading.Thread.Sleep(1000);
        //     */

        //    //Masiny - presun do konfigurace
        //    /*
        //    try
        //    {
        //        if (errors == 0)
        //            StatusText = Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (Machines)";
        //        errors += UpdateTableLocalMachines();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        //StatusTextErr = ex.Message;
        //        errors++;
        //    }

        //    System.Threading.Thread.Sleep(1000);
        //     */
        //    /*JoZ: ToDo...
        //    try
        //    {
        //        if (errors == 0)
        //            StatusText = Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (StatusTypes)";
        //        errors += UpdateTableLocalStatusTypes();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        //StatusTextErr = ex.Message;
        //        errors++;
        //    }
            
        //    System.Threading.Thread.Sleep(1000);
        //    */

        //    try
        //    {
        //        if (errors == 0)
        //            StatusText = Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (Events)";
        //        errors += UpdateTableRemoteEvents(UpdateTableRemoteMethodType.RowPerRow);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        //StatusText = ex.Message;
        //        errors++;
        //    }

        //    System.Threading.Thread.Sleep(1000);

        //    try
        //    {
        //        if (errors == 0)
        //            StatusText = Properties.Resources.strProbihaSynchronizaceDatMeziServeremAStanici + " (UserEvents)";
        //        errors += UpdateTableRemoteUserEvents(UpdateTableRemoteMethodType.RowPerRow);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        //StatusText = ex.Message;
        //        errors++;
        //    }

        //    if (errors == 0)
        //    {
        //        err = false;
        //        msg = "Databáze úspìšnì synchronizována [" + DateTime.Now.ToString("g") + "]";
        //        //StatusText = "Databáze úspìšnì synchronizována [" + DateTime.Now.ToString("g") + "]";
        //    }
        //    else
        //    {
        //        err = true;
        //        msg = "Pøi synchronizaci databáze se vyskytly chyby (" + errors.ToString() + ")";
        //        //StatusTextErr = "Pøi synchronizaci databáze se vyskytly chyby (" + errors.ToString() + ")";
        //    }
        //    try
        //    {
        //        this.BeginInvoke(new MethodInvoker(timer1.Start));
        //        this.BeginInvoke(new MethodInvoker(SetStatus));
        //    }
        //    catch
        //    {
        //    }
        //}

        //Synchronizace OK
        private string StatusText
        {
            set
            {
                try
                {
                    tsDatabaseSynchronizationStatus.ForeColor = Color.Black;
                    tsDatabaseSynchronizationStatus.Text = value;
                }
                catch { }
            }

        }

        //Synchronizace KO
        private string StatusTextErr
        {
            set
            {
                try
                {
                    tsDatabaseSynchronizationStatus.ForeColor = Color.Red;
                    tsDatabaseSynchronizationStatus.Text = value;
                }
                catch { }
            }
        }

        //private int UpdateTableLocalLogins()
        //{
        //    int errors = 0;

        //    Database.Vyroba vyroba = new Database.Vyroba();

        //    Database.VyrobaTableAdapters.FASK_LoginsTableAdapter lta = new Database.VyrobaTableAdapters.FASK_LoginsTableAdapter();
        //    lta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //    lta.ClearBeforeFill = true;
        //    lta.Fill(vyroba.FASK_Logins);
            
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
        //        StatusTextErr = ex.Message;
        //        Log.WriteException(ex);
        //        errors++;
        //    }
        //    finally
        //    {
        //        if (sqlconn != null && sqlconn.State == ConnectionState.Open)
        //            sqlconn.Close();
        //    }

        //    try
        //    {
        //        lta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //        lta.Update(vyroba);
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusTextErr = ex.Message;
        //        Log.WriteException(ex);
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
        //        StatusTextErr = ex.Message;
        //        Log.WriteException(ex);
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
        //        StatusTextErr = ex.Message;
        //        Log.WriteException(ex);
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

        //    Database.VyrobaTableAdapters.FASK_MachinesTableAdapter mta = new Database.VyrobaTableAdapters.FASK_MachinesTableAdapter();
        //    mta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //    mta.ClearBeforeFill = true;
        //    mta.Fill(vyroba.FASK_Machines);

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
        //        StatusTextErr = ex.Message;
        //        Log.WriteException(ex);
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
        //        StatusTextErr = ex.Message;
        //        Log.WriteException(ex);
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
        //    catch(Exception ex)
        //    {
        //        StatusTextErr = ex.Message;
        //        Log.WriteException(ex);
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
        //        StatusTextErr = ex.Message;
        //        Log.WriteException(ex);
        //        errors++;
        //    }

        //    return errors;
        //}

        private enum UpdateTableRemoteMethodType
        {
            DataAdapters,
            RowPerRow
        }

        //private int UpdateTableRemoteEvents(UpdateTableRemoteMethodType updateMethodType)
        //{
        //    int errors = 0;

        //    switch (updateMethodType)
        //    {
        //        case UpdateTableRemoteMethodType.RowPerRow:
        //            {
        //                ICommDatabase.DSVyroba vyroba = new ICommDatabase.DSVyroba();

        //                vyroba = Database.Vyroba.getVyrobaEventsData(Configuration.Config.config.Main[0].SqlConnectionStringLocal);

        //                /* JoZ: uprava, prace s knihovnami...ToDo: upravit vsude!!! synchronizace server, errtable,... (ted neni cas)
        //                Database.Vyroba vyroba = new Database.Vyroba();
        //                int rowsaffected = 0;
                        
        //                Database.VyrobaTableAdapters.FASK_EventsTableAdapter eta = new Database.VyrobaTableAdapters.FASK_EventsTableAdapter();
        //                eta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //                eta.ClearBeforeFill = true;
        //                rowsaffected = eta.Fill(vyroba.FASK_Events);
                 
        //                Database.VyrobaTableAdapters.FASK_EventsErrTableAdapter etaErr = new Database.VyrobaTableAdapters.FASK_EventsErrTableAdapter();
        //                etaErr.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //                etaErr.ClearBeforeFill = true;
        //                */
        //                Database.VyrobaTableAdapters.FASK_EventsTableAdapter etaCentral = new Database.VyrobaTableAdapters.FASK_EventsTableAdapter();
        //                //etaCentral.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //                etaCentral.Connection = new SqlConnection(Configuration.Config.config.Main[0].SqlConnectionStringRemote);
        //                etaCentral.ClearBeforeFill = true;

        //                try
        //                {
        //                    //eta.Connection.Open();
        //                    etaCentral.Connection.Open();
        //                    //etaErr.Connection.Open();


        //                    foreach (ICommDatabase.DSVyroba.FASK_EventsRow erow in vyroba.FASK_Events.Rows)
        //                    {
        //                        int raffLokal = 0;
        //                        int raffCentral = 0;
        //                        int raffLokalErr = 0;

        //                        try
        //                        {   // inicializacia transakcii

        //                            using (TransactionScope s = new TransactionScope())
        //                            {
        //                                int? rowsWithGUID = etaCentral.CountGUID(erow.faskGUID);
        //                                if (rowsWithGUID.HasValue && rowsWithGUID.Value == 0)
        //                                {   // neexistuje duplicitny zaznam
        //                                    raffCentral = etaCentral.Insert(erow.loginid, erow.machineid, erow.dateeve, erow.qty, erow.qtyReal, erow.description, erow.barcodeReaded, erow.barcodeSended, erow.zakazka, erow.popis, erow.faskGUID, erow.reportType, erow.IDO, erow.scan1, erow.scan2, erow.scan3, erow.sensor, erow.material);
        //                                }
        //                                else
        //                                {   // duplicitny zaznam vlozime (logujeme) do Err tabulky
        //                                    //raffLokalErr = etaErr.Insert(erow.loginid, erow.machineid, erow.dateeve, erow.qty, erow.qtyReal, erow.description, erow.barcodeReaded, erow.barcodeSended, erow.zakazka, erow.popis, erow.faskGUID, erow.reportType,null,null,null,null,null);
        //                                    raffLokalErr = Database.Vyroba.EventsErrInsert(erow.loginid, Configuration.Config.config.Main[0].SqlConnectionStringLocal, erow.qty, erow.qtyReal, erow.description, erow.barcodeReaded, erow.barcodeSended, erow.zakazka, erow.popis, erow.reportType, erow.IDO, erow.scan1, erow.scan2, erow.scan3, erow.sensor, erow.material);
        //                                }
        //                            }

        //                            //JoZ: uprava mimo TransactionScope...kvuli Open()
        //                            // zmazame z lokalnej tabulky
        //                            if (raffCentral > 0 || raffLokalErr > 0)
        //                                raffLokal = Database.Vyroba.DeleteEvent(erow.id);
        //                            //raffLokal = eta.Delete(erow.id);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            StatusTextErr = ex.Message;
        //                            Log.WriteException(ex);
        //                            errors++;
        //                        }

        //                    }

        //                }
        //                finally
        //                {
        //                    if ((etaCentral.Connection.State & ConnectionState.Open) == ConnectionState.Open)
        //                        etaCentral.Connection.Close();
        //                    //if ((eta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
        //                    //    eta.Connection.Close();
        //                    //if ((etaErr.Connection.State & ConnectionState.Open) == ConnectionState.Open)
        //                    //    etaErr.Connection.Close();
        //                }
        //            }
        //            break;
        //    }

        //    return errors;
        //}


        //private int UpdateTableRemoteUserEvents(UpdateTableRemoteMethodType updateMethodType)
        //{
        //    int errors = 0;

        //    switch (updateMethodType)
        //    {
        //        case UpdateTableRemoteMethodType.RowPerRow:
        //            {

        //                ICommDatabase.DSVyroba vyroba = new ICommDatabase.DSVyroba();

        //                vyroba = Database.Vyroba.getVyrobaUserEventsData(Configuration.Config.config.Main[0].SqlConnectionStringLocal);

        //                Database.VyrobaTableAdapters.FASK_UserEventsTableAdapter uetaCentral = new Database.VyrobaTableAdapters.FASK_UserEventsTableAdapter();
        //                //uetaCentral.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //                uetaCentral.Connection = new SqlConnection(Configuration.Config.config.Main[0].SqlConnectionStringRemote);
        //                uetaCentral.ClearBeforeFill = true;

        //                //Database.Vyroba2TableAdapters.FASK_UserEventsTableAdapter uetaCentral2 = new Database.Vyroba2TableAdapters.FASK_UserEventsTableAdapter();
        //                //uetaCentral2.Connection.ConnectionString = "Provider=SQLOLEDB;" + Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //                //uetaCentral2.ClearBeforeFill = true;

        //                /*
        //                int rowsaffected = 0;
        //                Database.Vyroba vyroba = new Database.Vyroba();

        //                Database.VyrobaTableAdapters.FASK_UserEventsTableAdapter ueta = new Database.VyrobaTableAdapters.FASK_UserEventsTableAdapter();
        //                ueta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //                ueta.ClearBeforeFill = true;
        //                rowsaffected = ueta.Fill(vyroba.FASK_UserEvents);

        //                Database.VyrobaTableAdapters.FASK_UserEventsErrTableAdapter uetaErr = new Database.VyrobaTableAdapters.FASK_UserEventsErrTableAdapter();
        //                uetaErr.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //                uetaErr.ClearBeforeFill = true;
        //                */

        //                try
        //                {
        //                    //ueta.Connection.Open();
        //                    uetaCentral.Connection.Open();
        //                    //uetaCentral2.Connection.Open();
        //                    //uetaErr.Connection.Open();

        //                    foreach (ICommDatabase.DSVyroba.FASK_UserEventsRow uerow in vyroba.FASK_UserEvents.Rows)
        //                    {
        //                        int raffLokal = 0;
        //                        int raffCentral = 0;
        //                        int raffLokalErr = 0;

        //                        try
        //                        {
        //                            using (TransactionScope s = new TransactionScope())
        //                            {

        //                                int? rowsWithGUID = uetaCentral.CountGUID(uerow.faskGUID);
        //                                //int? rowsWithGUID = (int)uetaCentral.CountGUID(uerow.faskGUID);
        //                                if (rowsWithGUID.HasValue && rowsWithGUID.Value == 0)
        //                                {   // neexistuje duplicitny zaznam
        //                                    raffCentral = uetaCentral.Insert(uerow.loginid, uerow.machineid, uerow.dateeve, uerow.statusid, uerow.faskGUID,uerow.rez_1,uerow.rez_2);
        //                                    //raffCentral = uetaCentral2.Insert(uerow.loginid, uerow.machineid, uerow.dateeve, uerow.statusid, uerow.faskGUID, uerow.rez_1);
        //                                }
        //                                else  
        //                                {   // duplicitny zaznam vlozime (logujeme) do Err tabulky
        //                                    //raffLokalErr = uetaErr.Insert(uerow.loginid, uerow.machineid, uerow.dateeve, uerow.statusid, uerow.faskGUID);
        //                                    raffLokalErr = Database.Vyroba.UserEventsErrInsert(uerow.id, uerow.statusid, uerow.loginid, uerow.machineid, Configuration.Config.config.Main[0].SqlConnectionStringLocal, "");
        //                                }
        //                            }

        //                            //JoZ: uprava mimo TransactionScope...kvuli Open()
        //                            if (raffCentral > 0 || raffLokalErr > 0)
        //                                raffLokal = Database.Vyroba.DeleteUserEvent(uerow.id);
        //                            //raffLokal = ueta.Delete(uerow.id);
        //                            //}

        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            errors++;
        //                            StatusTextErr = ex.Message;
        //                            Log.WriteException(ex);

        //                        }

        //                    }

        //                }
        //                finally
        //                {
        //                    if ((uetaCentral.Connection.State & ConnectionState.Open) == ConnectionState.Open)
        //                        uetaCentral.Connection.Close();
        //                    //if ((ueta.Connection.State & ConnectionState.Open) == ConnectionState.Open)
        //                    //    ueta.Connection.Close();
        //                    //if ((uetaErr.Connection.State & ConnectionState.Open) == ConnectionState.Open)
        //                    //    uetaErr.Connection.Close();
        //                }
        //            }
        //            break;

        //    }

        //    return errors;
        //}


        //private void synchronizaceDatabázeToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //    //Nepouživa se
        //    timer1_Tick(sender, e);
        //}

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
            FASK.MST_WINDOWS.Main.Properties.Settings.Default.VyrobaWindowState = this.WindowState;
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
            //Neni dovoleno ukoncit bez odhlaseni, pokud je nekdo zalogovan
            if (LogConfig.Logged() && e.CloseReason == CloseReason.UserClosing)
            {
                MessageBox.Show("Pøed ukonèením aplikace je nutné odhlášení (Logout).", "Ukonèení aplikace", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                return;
            }

            try
            {
                //Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.AppEnded, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                FASK.MST_WINDOWS.Main.Properties.Settings.Default.Save();
            }
            catch { }
        }

        private void errorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErrorLogDialog eld = new ErrorLogDialog();
            eld.ShowDialog();
        }

        private void kioskMódToolStripMenuItem_Click(object sender, EventArgs e)
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

        #region Stazeni ciselniku...

        private void zbožíToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stahnoutZbozi();
        }

        private void stahnoutZbozi()
        {
            if (FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogZbozi(ciselnikS, ""))
            {
                FlexibleMessageBox.Show(this, "èíslenik byl uspešne stažen", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void typiDokladuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stahnoutTypDokladu();
        }

        private void stahnoutTypDokladu()
        {
            if (FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogTypDokladu(ciselnikS, ""))
            {
                FlexibleMessageBox.Show(this, "èíslenik byl uspešne stažen", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void skladyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stahnoutSklady();
        }

        private void stahnoutSklady()
        {
            if (FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogSklady(ciselnikS))
            {
                FlexibleMessageBox.Show(this, "èíslenik byl uspešne stažen", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

        }

        private void oberateleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stahnoutoberatele();
        }

        private void stahnoutoberatele()
        {
            if (FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogOdberatele(ciselnikS, string.Empty))
            {
                FlexibleMessageBox.Show(this, "èíslenik byl uspešne stažen", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

        }

        private void pracovniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stahnoutpracovnici();
        }

        private void stahnoutpracovnici()
        {
            if (FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogPracovnici(ciselnikS, string.Empty))
            {
                FlexibleMessageBox.Show(this, "èíslenik byl uspešne stažen", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

        }

        private void støediskaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stahnoutstrediska();
        }

        private void stahnoutstrediska()
        {
            if (FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogStrediska(ciselnikS, string.Empty))
            {
                FlexibleMessageBox.Show(this, "èíslenik byl uspešne stažen", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void všechnyAktualizovatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AktualizovatCiselniky();
        }

        public void AktualizovatCiselniky()
        {
            if (!FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogZbozi(ciselnikS, ""))
                throw new Exception("Chyba pøi stahováni èísleniku Zboží");

            if (!FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogTypDokladu(ciselnikS, ""))
                throw new Exception("Chyba pøi stahováni èísleniku Typu dokladu");

            if (!FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogSklady(ciselnikS))
                throw new Exception("Chyba pøi stahováni èísleniku Skladu");

            if (!FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogOdberatele(ciselnikS, ""))
                throw new Exception("Chyba pøi stahováni èísleniku Odberatele");

            if (!FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogPracovnici(ciselnikS, ""))
                throw new Exception("Chyba pøi stahováni èísleniku Pracovnici");

            if (!FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogStrediska(ciselnikS, ""))
                throw new Exception("Chyba pøi stahováni èísleniku Stredisek");

            if (!FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogUzivatelu(loginser))
                throw new Exception("Chyba pøi stahováni èísleniku Uzivatelu");
        }
        #endregion



        private void uživateleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stahnoutUzivatele();
        }

        private void stahnoutUzivatele()
        {
            if (FASK.MST_WINDOWS.CiselnikServiceOperationsForm.KatalogUzivatelu(loginser))
            {
                FlexibleMessageBox.Show(this, "èíslenik byl uspešne stažen", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
    }
}