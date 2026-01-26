using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using FASK.MST_WINDOWS.ErrorLog;
using FASK.MST_WINDOWS.Logging;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using IRFIDProvider;
using FASK.MST_WINDOWS.Main.Configuration;




namespace FASK.MST_WINDOWS.Main.Configuration
{
    public partial class frmGlobalConfiguration : Form
    {
        public frmGlobalConfiguration()
        {
            InitializeComponent();
        }

        //Nacteni globalni konfigurace z tabulky
        private void frmGlobalConfiguration_Load(object sender, EventArgs e)
        {
            ////Spojeni na centralni databazi
            //try { txtDBCentral.Text = Config.config.Main[0].SqlConnectionStringRemote; }
            //catch { }
            ////Spojeni na lokalni databazi
            //try { txtDBLocal.Text = Config.config.Main[0].SqlConnectionStringLocal; }
            //catch { }
            ////Casovy interval automatickeho presnosu dat
            //try { nuSqlSynchTimeout.Value = Config.config.Main[0].SqlSynchronizationTimeout; }
            //catch { }

            try { tb_address_komServer.Text = Config.config.Main[0].KomServer; }
            catch { }

            try { tb_TerminalID.Text = Config.config.Main[0].TerminalID; }
            catch { }


            try { cb_OverVuciAD.Checked = Config.config.Main[0].OverVuciAD; }
            catch { }



            //Seznam stroju v databazi
            //SqlConnection sqlconn = null;
            //try
            //{
                //ICommDatabase.DSVyroba data = Database.Vyroba.getMachines(Config.config.Main[0].SqlConnectionStringLocal);
                //foreach (DSVyroba.FASK_MachinesRow item in data.FASK_Machines)
                //{
                //    clsMachine machine = new clsMachine();
                //    machine.ID = item.id.Trim();
                //    machine.Name = item.name.Trim();
                //    machine.Description = item.description.Trim();
                //    machine.Type = item.machinetype.Trim();
                //    machine.Koeficient = item.koeficient;
                //    cmbMachine.Items.Add(machine);

                //    if (item.id.Trim() == Config.config.Main[0].MachineID.Trim() && item.machinetype.Trim() == Config.config.Main[0].MachineType.Trim())
                //          cmbMachine.SelectedItem = machine;
                //}

                /*JoZ: prace s dynamickou knihovnou (databazi)...
                sqlconn = new SqlConnection(Config.config.Main[0].SqlConnectionStringLocal);
                SqlCommand sqlcomm = new SqlCommand("Select * from FASK_Machines", sqlconn);
                sqlconn.Open();

                SqlDataReader sqldr = sqlcomm.ExecuteReader();
                while (sqldr.Read())
                {
                    clsMachine machine = new clsMachine();
                    machine.ID = ((string)sqldr["id"]).Trim();
                    machine.Name = ((string)sqldr["name"]).Trim();
                    machine.Description = ((string)sqldr["description"]).Trim();
                    machine.Type = ((string)sqldr["machinetype"]).Trim();
                    machine.Koeficient = ((decimal)sqldr["koeficient"]);
                    cmbMachine.Items.Add(machine);

                    //FIXED: je treba kontrolovat dvojici ID+TYP.
                    if (machine.ID == Config.config.Main[0].MachineID && machine.Type == Config.config.Main[0].MachineType)
                        cmbMachine.SelectedItem = machine;
                }
                */
            //}
            //catch
            //{
            //    if (sqlconn != null && sqlconn.State == ConnectionState.Open) sqlconn.Close();
            //}

            //try
            //{
            //    //Identifikator stroje
            //    if (cmbMachine.Text.Length == 0) cmbMachine.Text = Config.config.Main[0].MachineID.Trim();
            //}
            //catch { }

            //COM nastavenie pro login - scanner
            //try
            //{
            //    if (LogConfig.config.Scanner.Rows.Count == 0)
            //    {
            //        LogConfig.config.Scanner.AddScannerRow("", "", "", "", "");
            //        LogConfig.Save();
            //    }

            //    txt_spinPort.Text = LogConfig.config.Scanner[0].SP_IN_PortName;
            //    txt_spinBaudRate.Text = LogConfig.config.Scanner[0].SP_IN_BaudRate;
            //    txt_spinDataBits.Text = LogConfig.config.Scanner[0].SP_IN_DataBits;
            //    txt_spinStopBits.Text = LogConfig.config.Scanner[0].SP_IN_StopBits;
            //    txt_spinParity.Text = LogConfig.config.Scanner[0].SP_IN_Parity;
            //}
            //catch{}

            //Com nastaveni pro login - ctecka cipu
            //try
            //{
            //    if (LogConfig.config.ChipScanner.Rows.Count == 0)
            //    {
            //        LogConfig.config.ChipScanner.AddChipScannerRow("", "", "", "", "", "", "");
            //        LogConfig.Save();
            //    }

            //    txt_spinPort2.Text = LogConfig.config.ChipScanner[0].SP_IN_PortName;
            //    txt_spinBaudRate2.Text = LogConfig.config.ChipScanner[0].SP_IN_BaudRate;
            //    txt_spinDataBits2.Text = LogConfig.config.ChipScanner[0].SP_IN_DataBits;
            //    txt_spinStopBits2.Text = LogConfig.config.ChipScanner[0].SP_IN_StopBits;
            //    txt_spinParity2.Text = LogConfig.config.ChipScanner[0].SP_IN_Parity;

            //    txtChipScannerAssembly.Text = LogConfig.config.ChipScanner[0].Assembly;
            //    txtChipScannerObject.Text = LogConfig.config.ChipScanner[0].Object;
            //}
            //catch{ }

            //Nastaveni metody prihlasovani
            try
            {
                //if (LogConfig.config.LoginMetod.Rows.Count == 0)
                //{
                //    //Vychozi hodnota - vkladam jen rucne.
                //    cmbLoginMethod.SelectedIndex = 0;

                //    LogConfig.config.LoginMetod.AddLoginMetodRow(cmbLoginMethod.SelectedIndex.ToString(),cmbLoginMethod.SelectedText, string.Empty, string.Empty);
                //    LogConfig.Save();
                //}

                cmbLoginMethod.SelectedIndex = int.Parse(Config.LoginMetod_Method);

                //JoZ: jmeno a heslo pro automaticke prihlaseni uzivatele
                txtIdUzivatel.Text = Config.LoginMetod_Login;
                txtPassword.Text = Config.LoginMetod_Password;
            }
            catch { }

            //Sprava modulu - pridani do seznamuu (modulu a autostart)
            initStartModule();
            moduleList.Items.Clear();
            string autoStart = string.Empty;
            //Auto start
            try { autoStart = Config.config.Main[0].ModuleAutoStart; }
            catch { autoStart = string.Empty; }
            //Pruchod
            foreach (GlobalConfig.ModulesRow modul in Config.config.Modules.Rows)
            {
                moduleList.Items.Add(Config.createModuleListString((string)modul["Id"],(string)modul["Name"]));
                cmbModuleAutostart.Items.Add(Config.createModuleListString((string)modul["Id"], (string)modul["Name"]));
                //Startovaci modul
                if ((string)modul["id"] == autoStart)
                {
                    //Index == id modulu, pac id je sekvencni <0,inf)
                    cmbModuleAutostart.SelectedIndex = int.Parse((string)modul["id"])+1;
                }
            }


            //if (LogConfig.config.RFID.Rows.Count == 0)
            //{
            //    LogConfig.config.RFID.AddRFIDRow("COM1", "_57600bps", "FF");
            //    LogConfig.Save();
            //}

            ComboBox_baud.DataSource = Enum.GetValues(typeof(Baudrate));
            ComboBox_COM.DataSource = Enum.GetValues(typeof(ComPorty));

            ComboBox_COM.SelectedItem = (ComPorty)Enum.Parse(typeof(ComPorty), Config.RFID_ComPort);
            ComboBox_baud.SelectedItem = (Baudrate)Enum.Parse(typeof(Baudrate), Config.RFID_Baudrate);
            Edit_CmdComAddr.Text = Config.RFID_Address;
        }

        //Vlozeni volby zadny vychozi modul
        private void initStartModule()
        {
            cmbModuleAutostart.Items.Clear();
            cmbModuleAutostart.Items.Add(Config.modulenonestring);
            cmbModuleAutostart.SelectedIndex = 0;
        }

        private void buttonStorno_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        //Ulozeni globalni konfigurace
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Configuration.Config.config.Main.Rows.Count == 0) Configuration.Config.config.Main.AddMainRow("", "", "", "", "", 10, "", "", "1", "");

                //Masina
                //clsMachine machine = (clsMachine)cmbMachine.SelectedItem;

                //if (machine != null)
                //{
                //    LogConfig.MachineID = machine.ID;
                //    LogConfig.MachineType = machine.Type;
                //    LogConfig.Koeficient = machine.Koeficient;
                //    Configuration.Config.config.Main[0].MachineID = machine.ID;
                //    Configuration.Config.config.Main[0].MachineName = machine.Name;
                //    Configuration.Config.config.Main[0].MachineDescription = machine.Description;
                //    Configuration.Config.config.Main[0].MachineType = machine.Type;
                //    Configuration.Config.config.Main[0].Koeficient = machine.Koeficient.ToString();
                //}
                //else
                //{
                //    LogConfig.MachineID = string.Empty;
                //    LogConfig.MachineType = string.Empty;
                //    LogConfig.Koeficient = (decimal)1;
                //    Configuration.Config.config.Main[0].MachineID = string.Empty;
                //    Configuration.Config.config.Main[0].MachineName = string.Empty;
                //    Configuration.Config.config.Main[0].MachineDescription = string.Empty;
                //    Configuration.Config.config.Main[0].MachineType = string.Empty;
                //    Configuration.Config.config.Main[0].Koeficient = ((decimal)(1)).ToString();
                //}

                //Connection stringy
                //Configuration.Config.config.Main[0].SqlConnectionStringLocal = txtDBLocal.Text;
                //LogConfig.SqlConnectionStringLocal = txtDBLocal.Text;
                //Configuration.Config.config.Main[0].SqlConnectionStringRemote = txtDBCentral.Text;
                //LogConfig.SqlConnectionStringGlobal = txtDBCentral.Text;

                //Config.config.Main[0].SqlSynchronizationTimeout = Convert.ToInt32(nuSqlSynchTimeout.Value);

                Config.config.Main[0].KomServer = tb_address_komServer.Text.Trim();
                Config.config.Main[0].TerminalID = tb_TerminalID.Text.Trim();
                Config.config.Main[0].OverVuciAD = cb_OverVuciAD.Checked;


                //Vyber modulu pro start
                string selectedModule = (string)cmbModuleAutostart.SelectedItem;
                //Pokud je null, neni vybran zadny, pokud neobsahuje mezeru, neco je spatne - nemelo by nastat
                if (selectedModule == null || !selectedModule.Contains(" ") || selectedModule == Config.modulenonestring)
                {
                    Config.config.Main[0].ModuleAutoStart = string.Empty;
                }
                else
                {
                    //Ziskam Id - prvni prvek pole pokud delim podle mezery, melo by vzdy byt
                    //moduleInfo[0] = id
                    //moduleInfo[1] = ":"
                    //moduleInfo[2] = modul_name
                    string[] moduleInfo = selectedModule.Split(new char[] { ' ' });
                    Config.config.Main[0].ModuleAutoStart = moduleInfo[0];
                }


                //Ulouzime udaje o seriovem porte pre login - scanner
                //if (LogConfig.config.Scanner.Rows.Count == 0) LogConfig.config.Scanner.AddScannerRow("", "", "", "", "");

                //LogConfig.config.Scanner[0].SP_IN_BaudRate = txt_spinBaudRate.Text;
                //LogConfig.config.Scanner[0].SP_IN_DataBits = txt_spinDataBits.Text;
                //LogConfig.config.Scanner[0].SP_IN_Parity = txt_spinParity.Text;
                //LogConfig.config.Scanner[0].SP_IN_PortName = txt_spinPort.Text;
                //LogConfig.config.Scanner[0].SP_IN_StopBits = txt_spinStopBits.Text;

                //Ulouzime udaje o seriovem porte pre login - ctecka cipu
                //if (LogConfig.config.ChipScanner.Rows.Count == 0) LogConfig.config.ChipScanner.AddChipScannerRow("", "", "", "", "", "", "");

                //LogConfig.config.ChipScanner[0].SP_IN_BaudRate = txt_spinBaudRate2.Text;
                //LogConfig.config.ChipScanner[0].SP_IN_DataBits = txt_spinDataBits2.Text;
                //LogConfig.config.ChipScanner[0].SP_IN_Parity = txt_spinParity2.Text;
                //LogConfig.config.ChipScanner[0].SP_IN_PortName = txt_spinPort2.Text;
                //LogConfig.config.ChipScanner[0].SP_IN_StopBits = txt_spinStopBits2.Text;

                //Ulozime index comboboxu, podle ktereho se ma prihlasovat
                Config.config.LoginMetod[0].Method = cmbLoginMethod.SelectedIndex.ToString();
                Config.config.LoginMetod[0].Description = cmbLoginMethod.SelectedItem.ToString();
                Config.config.LoginMetod[0].Login = txtIdUzivatel.Text;
                Config.config.LoginMetod[0].Password = txtPassword.Text;

                //A ulozime assembly scanneru a object
                //LogConfig.config.ChipScanner[0].Assembly = txtChipScannerAssembly.Text.Trim();
                //LogConfig.config.ChipScanner[0].Object = txtChipScannerObject.Text.Trim();

                Config.config.RFID[0].Address = Edit_CmdComAddr.Text;
                Config.config.RFID[0].ComPort = ComboBox_COM.SelectedItem.ToString();
                Config.config.RFID[0].Baudrate = ComboBox_baud.SelectedItem.ToString();

                //Ulozeni obecne konfigurace
                Config.Save();
                //Ulozeni konfigurace moduluu
                //ConfigMod.Save();
                //Ulozeni konfigurace loginu
                //LogConfig.Save();

                //Log-event
                //Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.ConfigAppChanged, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);

                //OK
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                //throw;
            }
        }

        //Registrace noveho modulu
        private void registrModul_Click_1(object sender, EventArgs e)
        {
            string name = moduleName.Text.Trim();
            string asse = moduleAssembly.Text.Trim();
            string obj = moduleObject.Text.Trim();
            string id = moduleList.Items.Count.ToString();

            //Vse musi byt vyplneno
            if (name == string.Empty || asse == string.Empty || obj == string.Empty)
            {
                MessageBox.Show("Registrace modulu se nezdaøila. Je tøeba vyplnit všechny položky.", "Výroba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Pridam modul, jen pokud uz takovy neni v seznamu - identifikace id : name
                //V soucasne dobe nenastane pac id se inkrementuje
                if (moduleList.Items.Contains(Config.createModuleListString(id, name)))
                {
                    MessageBox.Show("Registrace modulu se nezdaøila. Uvedený modul je již zaregistrovaný.", "Výroba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        //Pridani do datasetu
                        Config.config.Modules.Rows.Add(new object[] { name, asse, obj, id });
                        //A do seznamu modulu
                        int index = moduleList.Items.Add(Config.createModuleListString(id, name));
                        moduleList.SelectedIndex = index;
                        //A do seznamu pro spusteni
                        cmbModuleAutostart.Items.Add(Config.createModuleListString(id, name));
                        //Vyprazdneni textboxuu
                        //moduleName.Clear();
                        //moduleAssembly.Clear();
                        //moduleObject.Clear();
                        //moduleConfig.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Registrace modulu se nezdaøila.", "Výroba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Log.WriteException(ex);
                    }
                }
            }
        }

        //Vyber modulu ze seznamu modulu
        private void moduleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Polozka
            string mod = (string)moduleList.SelectedItem;
            //Index
            int index = -1;
            index = moduleList.FindStringExact(mod);
            //Vyplneni informaci: split vrati id modulu, :, jmeno ...
            string[] modInfo = mod.Split(new char[] { ' ' });
            //Nalezei ostatnich informaci v tabulce
            foreach (GlobalConfig.ModulesRow modul in Config.config.Modules.Rows)
            {
                //Identifikace spravneho
                if ((string)modul["Id"] == modInfo[0])
                {
                    moduleName.Text = (string)modul["Name"];
                    moduleAssembly.Text = (string)modul["Assembly"];
                    moduleObject.Text = (string)modul["Object"];
                }
            }
        }

        //Odstraneni modulu ze seznamu moduluu
        private void delModule_Click(object sender, EventArgs e)
        {
            //Polozka
            string mod = (string)moduleList.SelectedItem;
            string startmod = (string)cmbModuleAutostart.SelectedItem;
            //Index
            int index = -1;
            index = moduleList.FindStringExact(mod);
            //pokud je neco vybrano
            if (index != -1)
            {
                //Odstraneni z tabulky
                Config.config.Modules.Rows[index].Delete();
                //Posunuti ID v tabulce i v seznamu
                moveId(startmod);
                //Vyprazdneni textboxuu
                moduleName.Clear();
                moduleAssembly.Clear();
                moduleObject.Clear();
            }
        }

        //Posune ID od smazaneho zpet
        private void moveId(string startmod)
        {
            //Inicializace dvou seznamu
            initStartModule();
            moduleList.Items.Clear();
            //Pruchod tabulkou
            int newId = -1;
            for (int i = 0; i < Config.config.Modules.Rows.Count; i++)
            {
                //Pokud je vybrany startovaci modul stale v tabulce,ziskava novou pozici
                if (startmod == Config.createModuleListString((string)Config.config.Modules.Rows[i]["Id"], (string)Config.config.Modules.Rows[i]["Name"]))
                {
                    newId = i;
                }
                //PrecislovaniID
                Config.config.Modules.Rows[i]["Id"] = i.ToString();
                //Vlozeni zpet do seznamu s novym id
                moduleList.Items.Add(Config.createModuleListString((string)Config.config.Modules.Rows[i]["Id"], (string)Config.config.Modules.Rows[i]["Name"]));
                cmbModuleAutostart.Items.Add(Config.createModuleListString((string)Config.config.Modules.Rows[i]["Id"], (string)Config.config.Modules.Rows[i]["Name"]));
            }
            //Nastaveni nove pozice nebo none (+1 prave kvuli <none>)
            cmbModuleAutostart.SelectedIndex = newId < 0 ? 0 : newId+1;
        }

        ////Synchronizace administratorskych dat - operace, loginy, masiny, typy masin, ..
        //private void btnSyncAdminData_Click(object sender, EventArgs e)
        //{
        //    //Log-event
        //    Database.Vyroba.UserEventsInsert(Database.Vyroba.StatusTypesEnum.AdminDatabaseSynchUsr, LogConfig.LoginID, LogConfig.MachineID, LogConfig.SqlConnectionStringLocal);
        //    //Synchronizacni thread
        //    Thread syncThread = new Thread(new ParameterizedThreadStart(synchronizationAdminDataThread));
        //    syncThread.Start(sender);
        //}

        //Funkce vykonavana threadem pro sycnchronizaci - vlastni synchronizace
        //void synchronizationAdminDataThread(object sender)
        //{
        //    //
        //    //PLNENI TABULEK V PAMETI (GLOBALNI DATABAZE -> PAMET)
        //    //
        //    Database.Vyroba vyroba = new Database.Vyroba();
        //    string info = "Probíhá synchronizace dat mezi serverem a stanicí";
        //    string error = "Pøi synchronizaci databáze se vyskytla chyba";
        //    //Adaptery
        //    Database.VyrobaTableAdapters.FASK_LoginsTableAdapter lta = null;
        //    Database.VyrobaTableAdapters.FASK_MachineTypeTableAdapter mtta = null;
        //    Database.VyrobaTableAdapters.FASK_MachinesTableAdapter mta = null;
        //    Database.VyrobaTableAdapters.FASK_OperationsTableAdapter ota = null;
        //    Database.VyrobaTableAdapters.FASK_Operations_NextTableAdapter onta = null;

        //    //Naplneni tabulky s loginy
        //    try
        //    {
        //        adminSyncStatusText = info + " (Logins).";

        //        lta = new Database.VyrobaTableAdapters.FASK_LoginsTableAdapter();
        //        lta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //        lta.ClearBeforeFill = true;
        //        lta.Fill(vyroba.FASK_Logins);

        //        foreach (Database.Vyroba.FASK_LoginsRow lrow in vyroba.FASK_Logins.Rows)
        //        {
        //            lrow.SetAdded();
        //        }

        //        //Naplneni machine type tabulky
        //        System.Threading.Thread.Sleep(1000);
        //        adminSyncStatusText = info + " (MachineTypes).";

        //        //Table adapter na praci s tabulkou machine type - vyplneni do pameti
        //        mtta = new Database.VyrobaTableAdapters.FASK_MachineTypeTableAdapter();
        //        mtta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //        mtta.ClearBeforeFill = true;
        //        mtta.Fill(vyroba.FASK_MachineType);

        //        //Oznaceni jednotlivych radku jako pridane
        //        foreach (Database.Vyroba.FASK_MachineTypeRow mtrow in vyroba.FASK_MachineType.Rows)
        //        {
        //            mtrow.SetAdded();
        //        }

        //        //Naplneni machines tabulky
        //        System.Threading.Thread.Sleep(1000);
        //        adminSyncStatusText = info + " (Machines).";

        //        mta = new Database.VyrobaTableAdapters.FASK_MachinesTableAdapter();
        //        mta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //        mta.ClearBeforeFill = true;
        //        mta.Fill(vyroba.FASK_Machines);

        //        foreach (Database.Vyroba.FASK_MachinesRow mrow in vyroba.FASK_Machines.Rows)
        //        {
        //            mrow.SetAdded();
        //        }

        //        //Naplneni operations tabulky
        //        System.Threading.Thread.Sleep(1000);
        //        adminSyncStatusText = info + " (Operations).";

        //        ota = new Database.VyrobaTableAdapters.FASK_OperationsTableAdapter();
        //        ota.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //        ota.ClearBeforeFill = true;
        //        ota.Fill(vyroba.FASK_Operations);

        //        foreach (Database.Vyroba.FASK_OperationsRow orow in vyroba.FASK_Operations.Rows)
        //        {
        //            orow.SetAdded();
        //        }

        //        //Naplneni operations next tabulky
        //        System.Threading.Thread.Sleep(1000);
        //        adminSyncStatusText = info + " (OperationsNext).";

        //        onta = new Database.VyrobaTableAdapters.FASK_Operations_NextTableAdapter();
        //        onta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringRemote;
        //        onta.ClearBeforeFill = true;
        //        onta.Fill(vyroba.FASK_Operations_Next);

        //        foreach (Database.Vyroba.FASK_Operations_NextRow onrow in vyroba.FASK_Operations_Next.Rows)
        //        {
        //            onrow.SetAdded();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        adminSyncStatusTextErr = error + " (" + ex.Message + "). ";
        //        Log.WriteException(ex);
        //        return;
        //    }
        //    //
        //    //MAZANI TABULEK V LOKALNI DATABAZI
        //    //
        //    bool useLocalDP = true;

        //    if (!useLocalDP)
        //    {
        //        #region local SQL server
        //        SqlConnection sqlconn = null;
        //        SqlCommand sqlcomm = null;
        //        string delete = "Odstraòuji obsah tabulky";

        //        try
        //        {
        //            //Vytvoreni spojeni
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = "Vytváøí se spojení s lokální databází.";

        //            sqlconn = new SqlConnection(Configuration.Config.config.Main[0].SqlConnectionStringLocal);
        //            sqlcomm = new SqlCommand();
        //            sqlcomm.Connection = sqlconn;
        //            sqlconn.Open();

        //            //Odstraneni logins tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = delete + " (Logins).";

        //            sqlcomm.CommandText = "Delete from FASK_Logins";
        //            sqlcomm.ExecuteNonQuery();

        //            //Odstraneni obsahu operations next tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = delete + " (OperationsNext).";

        //            sqlcomm.CommandText = "Delete from FASK_Operations_Next";
        //            sqlcomm.ExecuteNonQuery();

        //            //Naplneni operations tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = delete + " (Operations).";

        //            sqlcomm.CommandText = "Delete from FASK_Operations";
        //            sqlcomm.ExecuteNonQuery();

        //            //Odstraneni obsahu machines tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = delete + " (Machines).";

        //            sqlcomm.CommandText = "Delete from FASK_Machines";
        //            sqlcomm.ExecuteNonQuery();

        //            //Odstraneni obsahu machine type tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = delete + " (MachineTypes).";

        //            sqlcomm.CommandText = "Delete from FASK_MachineType";
        //            sqlcomm.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {
        //            adminSyncStatusTextErr = error + " (" + ex.Message + "). ";
        //            Log.WriteException(ex);
        //            return;
        //        }
        //        finally
        //        {
        //            //Uzavreni spojeni
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = "Ukonèuje se spojení s lokální databází.";
        //            if (sqlconn != null && sqlconn.State == ConnectionState.Open) sqlconn.Close();
        //        }

        //        #endregion
        //    }
        //    else 
        //    {
        //        #region local sdf
        //        //SqlConnection sqlconn = null;
        //        //SqlCommand sqlcomm = null;
        //        string delete = "Odstraòuji obsah tabulky";

        //        try
        //        {
        //            //Vytvoreni spojeni
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = "Vytváøí se spojení s lokální databází.";

        //            //sqlconn = new SqlConnection(Configuration.Config.config.Main[0].SqlConnectionStringLocal);
        //            //sqlcomm = new SqlCommand();
        //            //sqlcomm.Connection = sqlconn;
        //            //sqlconn.Open();

        //            //Odstraneni logins tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = delete + " (Logins).";

        //            //sqlcomm.CommandText = "Delete from FASK_Logins";
        //            //sqlcomm.ExecuteNonQuery();
        //            Database.Vyroba.Delete_FASK_Logins(Configuration.Config.config.Main[0].SqlConnectionStringLocal);

        //            //Odstraneni obsahu operations next tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = delete + " (OperationsNext).";

        //            //sqlcomm.CommandText = "Delete from FASK_Operations_Next";
        //            //sqlcomm.ExecuteNonQuery();
        //            Database.Vyroba.Delete_FASK_Operations_Next(Configuration.Config.config.Main[0].SqlConnectionStringLocal);

        //            //Naplneni operations tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = delete + " (Operations).";

        //            //sqlcomm.CommandText = "Delete from FASK_Operations";
        //            //sqlcomm.ExecuteNonQuery();
        //            Database.Vyroba.Delete_FASK_Operations(Configuration.Config.config.Main[0].SqlConnectionStringLocal);

        //            //Odstraneni obsahu machines tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = delete + " (Machines).";

        //            //sqlcomm.CommandText = "Delete from FASK_Machines";
        //            //sqlcomm.ExecuteNonQuery();
        //            Database.Vyroba.Delete_FASK_Machines(Configuration.Config.config.Main[0].SqlConnectionStringLocal);

        //            //Odstraneni obsahu machine type tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = delete + " (MachineTypes).";

        //            //sqlcomm.CommandText = "Delete from FASK_MachineType";
        //            //sqlcomm.ExecuteNonQuery();
        //            Database.Vyroba.Delete_FASK_MachineType(Configuration.Config.config.Main[0].SqlConnectionStringLocal);
        //        }
        //        catch (Exception ex)
        //        {
        //            adminSyncStatusTextErr = error + " (" + ex.Message + "). ";
        //            Log.WriteException(ex);
        //            return;
        //        }
        //        finally
        //        {
        //            //Uzavreni spojeni
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = "Ukonèuje se spojení s lokální databází.";
        //            //if (sqlconn != null && sqlconn.State == ConnectionState.Open) sqlconn.Close();
        //        }

        //        #endregion
        //    }
        //    //
        //    //PLNENI TABULEK V LOKALNI DATAABZI (PAMET -> LOKALNI DATABAZE)
        //    //
        //    string update = "Provádím zmìny v lokální databázi";

        //    if (!useLocalDP)
        //    {
        //        #region local SQL server
        //        try
        //        {
        //            //Potvrzeni logins tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = update + " (Logins).";

        //            lta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //            lta.Update(vyroba.FASK_Logins);


        //            //Potvrzeni machine type tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = update + " (MachineTypes).";

        //            mtta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //            mtta.Update(vyroba.FASK_MachineType);

        //            //Potvrzeni machines tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = update + " (Machines).";

        //            mta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //            mta.Update(vyroba.FASK_Machines);

        //            //Potvrzeni operations tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = update + " (Operations).";

        //            ota.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //            ota.Update(vyroba.FASK_Operations);

        //            //Potvrzeni operations next tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = update + " (OperationsNext).";

        //            onta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //            onta.Update(vyroba.FASK_Operations_Next);
        //        }
        //        catch (Exception ex)
        //        {
        //            adminSyncStatusTextErr = error + " (" + ex.Message + "). ";
        //            Log.WriteException(ex);
        //            return;
        //        } 
        //        #endregion
        //    }
        //    else 
        //    {
        //        #region local sdf soubor
        //        try
        //        {
        //            //Potvrzeni logins tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = update + " (Logins).";

        //            //lta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //            //lta.Update(vyroba.FASK_Logins);
        //            ICommDatabase.DSVyroba.FASK_LoginsDataTable dtlogins = new DSVyroba.FASK_LoginsDataTable();

        //            foreach (var item in vyroba.FASK_Logins)
        //            {
        //                var row = dtlogins.NewFASK_LoginsRow();

        //                row.id = item.id;
        //                row.firstname = item.firstname;
        //                row.surname = item.surname;
        //                row.psswd = item.psswd;

        //                if (item.IsrfidNull())
        //                    row.SetrfidNull();
        //                else
        //                    row.rfid = item.rfid;

        //                if (item.IsbarcodeNull())
        //                    row.SetbarcodeNull();
        //                else
        //                    row.barcode = item.barcode;

        //                dtlogins.AddFASK_LoginsRow(row);
        //            }

        //            Database.Vyroba.Update_FASK_Logins(dtlogins, Configuration.Config.config.Main[0].SqlConnectionStringLocal);


        //            //Potvrzeni machine type tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = update + " (MachineTypes).";

        //            //mtta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //            //mtta.Update(vyroba.FASK_MachineType);
        //            ICommDatabase.DSVyroba.FASK_MachineTypeDataTable dtmachinestype = new DSVyroba.FASK_MachineTypeDataTable(); 
        //            foreach (var item in vyroba.FASK_MachineType)
        //            {
        //                var row = dtmachinestype.NewFASK_MachineTypeRow();

        //                row.machinetype = item.machinetype;
        //                row.machinetypename = item.machinetypename;
      
        //                dtmachinestype.AddFASK_MachineTypeRow(row);
        //            }

        //            Database.Vyroba.Update_FASK_MachineType(dtmachinestype, Configuration.Config.config.Main[0].SqlConnectionStringLocal);

        //            //Potvrzeni machines tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = update + " (Machines).";

        //            //mta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //            //mta.Update(vyroba.FASK_Machines);
        //            ICommDatabase.DSVyroba.FASK_MachinesDataTable dtmachines = new DSVyroba.FASK_MachinesDataTable();
        //            foreach (var item in vyroba.FASK_Machines)
        //            {
        //                var row = dtmachines.NewFASK_MachinesRow();

        //                row.id = item.id;
        //                row.machinetype = item.machinetype;
        //                row.name = item.name;
        //                row.description = item.description;
        //                row.koeficient = item.koeficient;


        //                dtmachines.AddFASK_MachinesRow(row);
        //            }

        //            Database.Vyroba.Update_FASK_Machines(dtmachines, Configuration.Config.config.Main[0].SqlConnectionStringLocal);

        //            //Potvrzeni operations tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = update + " (Operations).";
        //            //ota.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //            //ota.Update(vyroba.FASK_Operations);

        //            ICommDatabase.DSVyroba.FASK_OperationsDataTable dtoperations = new DSVyroba.FASK_OperationsDataTable();
        //            foreach (var item in vyroba.FASK_Operations)
        //            {
        //                var row = dtoperations.NewFASK_OperationsRow();

        //                row.machinetype = item.machinetype;
        //                row.IDO = item.IDO;
        //                row.NAZEV = item.NAZEV;
        //                row.CK = item.CK;
        //                row.SCAN1 = item.SCAN1;
        //                row.SCAN2 = item.SCAN2;
        //                row.SCAN3 = item.SCAN3;
        //                row.SENSOR = item.SENSOR;
        //                row.VOLNA = item.VOLNA;
        //                row.START = item.START;
        //                row.KONEC = item.KONEC;
        //                row.SPHLAVICKA = item.SPHLAVICKA;
        //                row.SPINFO = item.SPINFO;
        //                row.SCANZAKAZKA = item.SCANZAKAZKA;
        //                row.SPZAKAZKA = item.SPZAKAZKA;
        //                row.KONTROLAMAT = item.KONTROLAMAT;
        //                row.SPMATERIAL = item.SPMATERIAL;
        //                row.LOGIN = item.LOGIN;

        //                dtoperations.AddFASK_OperationsRow(row);
        //            }
        //            Database.Vyroba.Update_FASK_Operations(dtoperations, Configuration.Config.config.Main[0].SqlConnectionStringLocal);

        //            //Potvrzeni operations next tabulky
        //            System.Threading.Thread.Sleep(1000);
        //            adminSyncStatusText = update + " (OperationsNext).";

        //            //onta.Connection.ConnectionString = Configuration.Config.config.Main[0].SqlConnectionStringLocal;
        //            //onta.Update(vyroba.FASK_Operations_Next);
        //            ICommDatabase.DSVyroba.FASK_Operations_NextDataTable dtoperationsnext = new DSVyroba.FASK_Operations_NextDataTable();
        //            foreach (var item in vyroba.FASK_Operations_Next)
        //            {
        //                var row = dtoperationsnext.NewFASK_Operations_NextRow();

        //                row.machinetype = item.machinetype;
        //                row.IDO = item.IDO;
        //                row.IDO_NEXT = item.IDO_NEXT;

        //                dtoperationsnext.AddFASK_Operations_NextRow(row);
        //            }
        //            Database.Vyroba.Update_FASK_Operations_Next(dtoperationsnext, Configuration.Config.config.Main[0].SqlConnectionStringLocal);
        //        }
        //        catch (Exception ex)
        //        {
        //            adminSyncStatusTextErr = error + " (" + ex.Message + "). ";
        //            Log.WriteException(ex);
        //            return;
        //        }
        //        #endregion
               
            
        //    }
        //    //
        //    //USPECH
        //    //
        //    adminSyncStatusText = "Databáze úspìšnì synchronizována [" + DateTime.Now.ToString("g") + "].";
        //}

        //Synchronizace OK
        private string adminSyncStatusText
        {
            set
            {
                try
                {
                    adminDataSynchronizationStatus.ForeColor = Color.Black;
                    adminDataSynchronizationStatus.Text = value;
                }
                catch { }
            }
        }

        //Synchronizace KO
        private string adminSyncStatusTextErr
        {
            set
            {
                try
                {
                    adminDataSynchronizationStatus.ForeColor = Color.Red;
                    adminDataSynchronizationStatus.Text = value;
                }
                catch { }
            }
        }

        private void cmbLoginMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLoginMethod.SelectedIndex == 1)
            {
                ShowAutoPrihlaseniUzivatel(true);
            }
            else
            {
                ShowAutoPrihlaseniUzivatel(false);
            }
        }

        private void ShowAutoPrihlaseniUzivatel(bool visible)
        {
            lblIDUzivatel.Visible = visible;
            txtIdUzivatel.Visible = visible;
            txtPassword.Visible = visible;
            lblPassword.Visible = visible;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Edit_CmdComAddr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ("0123456789ABCDEF".IndexOf(Char.ToUpper(e.KeyChar)) < 0);
        }

        private Update.Updater updater;

        private void button_check_Click(object sender, EventArgs e)
        {

            try
            {
                //string FilePath = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;

                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                String updateFilePath = Path.Combine(MyPath.CurrentDirectory, "update.xml");


                // TODO Dataset naèteni z xml do datasetu
                updater = new Update.Updater(Config.config.Main[0].KomServer + "upgrade/update.xml", updateFilePath);
                FASK.MST_WINDOWS.Main.Configuration.Update.UpdateInfo ds = new FASK.MST_WINDOWS.Main.Configuration.Update.UpdateInfo();

                updater.CheckForVersion(ds);

                if (ds.UpdateData.Count > 0)
                {
                    this.CB_MSTWNEWVersion.Items.Clear();
                }


                foreach (FASK.MST_WINDOWS.Main.Configuration.Update.UpdateInfo.UpdateDataRow row in ds.UpdateData.Rows)
                {
                    this.CB_MSTWNEWVersion.Items.Add(row);
                    //this.CB_MSTWNEWVersion = row;
                }

                this.CB_MSTWNEWVersion.SelectedItem = 1;

                this.button_update.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                //string FilePath = (new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase))).LocalPath;

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName =  FASK.MST_WINDOWS.Main.Properties.Resources.PathToUpdater;

                if (CB_MSTWNEWVersion.SelectedItem is FASK.MST_WINDOWS.Main.Configuration.Update.UpdateInfo.UpdateDataRow)
                {
                    FASK.MST_WINDOWS.Main.Configuration.Update.UpdateInfo.UpdateDataRow row = (FASK.MST_WINDOWS.Main.Configuration.Update.UpdateInfo.UpdateDataRow)CB_MSTWNEWVersion.SelectedItem;

                    Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                    string Argumenty = '"' + row.Version + '"' + " " + '"' + version.ToString() + '"' + " " + '"' + MyPath.CurrentDirectory + '"';

                    startInfo.Arguments = Argumenty;
                    Process.Start(startInfo);

                    DialogResult = DialogResult.Yes;

                }
                else
                    return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
            }
        }
    }
}