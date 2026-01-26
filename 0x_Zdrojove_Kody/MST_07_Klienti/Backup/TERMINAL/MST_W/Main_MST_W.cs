
#define LOGIN_ENABLED

/*******Sekce, ktera definuje jednotlive moduly, ktere se budou kompilovat do aplikace ********/
#define MOD_VYDEJ           //Vydej klasicky
#define MOD_VYDEJ_KOLOVY    //Vydej kolovy
#define MOD_PRIJEM          //Prijem klasicky
#define MOD_PRODEJ          //Prodej klasicky
#define MOD_INVENTURA1      //Inventura 1
#define MOD_INVENTURA2      //Inventura 2

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Fask.MST_W.Forms;
using System.IO;
using Fask.MST_W.MySystem;
using System.Reflection;
using System.Threading;
using Fask.MST_W.Config;
using System.Media;
using System.Net;
using System.Xml;
using Fask.MST_W.ServerAccess;
using System.Linq;

namespace Fask.MST_W
{
    public partial class Main : System.Windows.Forms.Form
    {
        private delegate void MethodInvoker();

        #region Konstanty
        /// <summary>
        /// Formaty datumu zadavane
        /// </summary>
        public const string dateFormatRRMMDD = "yyMMdd";
        public const string datetimeFormatDMYYYYHmm = "d.M.yyyy H:mm";
        public const string datetimeFormatDMYYYYHmmssfff = "d.M.yyyy H:mm:ss.fff";

		public static DateTime? Date_RRMMDD(string date)
		{
			try
			{
				System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("cs-CZ");
				cultureInfo.Calendar.TwoDigitYearMax = 2099;
				return DateTime.ParseExact(date, dateFormatRRMMDD, cultureInfo);
			}
			catch (System.Exception ex)
			{
				Fask.Logging.ExceptionHandler2.Handle(ex); 
				return null;
			}
		}

        #endregion

        #region Prenesene funckie
        [DllImport("coredll.dll", CharSet = CharSet.Auto)]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("coredll.dll", CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(int hwnd, int nCmdShow);
        [DllImport("coredll.dll", CharSet = CharSet.Auto)]
        public static extern bool EnableWindow(int hwnd, bool enabled);
        #endregion

        #region Scannery
        // scanner
        public Fask.ScannerProvider.IScannerProvider Scanner = null;
        public Scanner.ScannerBaseRFID RFIDUHFScanner; //RFID - UHF ??? 
        private Fask.PrinterFactory.PrinterFactory Printer = null;
        // parsovani
        //private Fask.Parsing.ParsingFactory Parsing = null;
        #endregion

        #region Foceni
        public Fask.PhotoProvider.IPhotoProvider Photo = null;
        #endregion

        #region Ukolovani
        public void TasksSynchronizationStart()
        {
            if (MST_Global.TasksEnable)
                Ukolovani_1.Ukolovani_Checker.SynchronizationStart();
        }

        public void TasksSynchronizationStop()
        {
            Ukolovani_1.Ukolovani_Checker.SynchronizationStop();
        }
        #endregion

        #region Udalosti Obsluhy
        public Fask.Events.IEvents eventsUser = null;
        public System.Threading.Timer timerEventsSynchronization = null;
        private System.Threading.Timer log_upload;

        public void EventsSychronizationStart()
        {
            if (timerEventsSynchronization == null)
                timerEventsSynchronization = new System.Threading.Timer(new TimerCallback(EventsSychronizationPerform), null, Timeout.Infinite, Timeout.Infinite);

            if (MST_Global.EventsEnable)
                timerEventsSynchronization.Change(MST_Global.EventsSynchronizationInterval, MST_Global.EventsSynchronizationInterval);
        }
        public void EventsSynchronizationStop()
        {
            if (timerEventsSynchronization != null)
                timerEventsSynchronization.Change(Timeout.Infinite, Timeout.Infinite);
        }
        public void EventsSychronizationPerform(object state)
        {
            EventsSynchronizationStop();

            if (eventsUser == null)
                return;

            if (!eventsUser.synchronize())
                Logging.Log.Write("Events synchronization not succeded", "EventSynchronizationPerform");

            EventsSychronizationStart();
        }
        #endregion

        #region Nazvy souboru
        // nazvy souboru
        public static string LastUserFileName { get { return Path.Combine(WrkDir, "lstusr.cfg"); } }
        public static string ConfigTerminalFileName { get { return Path.Combine(DataDir, "ConfigTerminal.xml"); } }
        public static string ConfigModulesFileName { get { return Path.Combine(DataDir, "ConfigModules.xml"); } }
        public static string ConfigTypyPalet { get { return Path.Combine(StorageDir, "TypyPalet.xml"); } }
        public static string ConfigScanner { get { return Path.Combine(DataDir, "Scanner.xml"); } }
        
        // databaze
		//public static string SQLiteConnectionString
		//{
		//    get
		//    {
		//        // konfiguracne?
		//        StringBuilder sbConnection = new StringBuilder();
		//        sbConnection.Append("FailIfMissing=True;");
		//        sbConnection.Append("Data Source={0};");
		//        return sbConnection.ToString();
		//    }
		//}
		//public static string SQLiteConnectionStringFormat(string dbname)
		//{
		//    return String.Format(SQLiteConnectionString, Path.Combine(Main.StorageDir, Path.GetFileName(dbname)));
		//}

        public static string CiselnikZboziDB { get { return Path.Combine(StorageDir, "Zbozi.prd"); } } // TODO : prejmenovat CiselnikKatalogZboziDB na CiselnikZboziDB
        public static string CiselnikOdberateleDB { get { return Path.Combine(StorageDir, "Odberatele.prd"); } }
        public static string CiselnikStrediskaDB { get { return Path.Combine(StorageDir, "Strediska.prd"); } }
        public static string CiselnikMenDB { get { return Path.Combine(StorageDir, "Meny.prd"); } }
        public static string CiselnikTypDokladuDB { get { return Path.Combine(StorageDir, "TypDokladu.prd"); } }
        public static string CiselnikUzivateleDB { get { return Path.Combine(StorageDir, "Uzivatele.prd"); } }
        public static string CiselnikSkladyDB { get { return Path.Combine(StorageDir, "Sklady.prd"); } }
        public static string CiselnikLokaceDB { get { return Path.Combine(StorageDir, "Lokace.prd"); } }
        public static string CiselnikPracovniciDB { get { return Path.Combine(StorageDir, "Pracovnici.prd"); } }
        public static string CiselnikTiskarnyDB { get { return Path.Combine(StorageDir, "Tiskarny.prd"); } }
        public static string CiselnikUkolyDB { get { return Path.Combine(StorageDir, "Ukoly.prd"); } }
        public static string CiselnikUkolyDBSynch { get { return Path.Combine(StorageDir, "Ukoly_synch.prd"); } }

        // xml soubory => predelat na databaze ...
        // TODO : !!! veskere xml datove soubory rozsahle predelat na databaze ... 
        public static string CiselnikSkladuFileName { get { return Path.Combine(StorageDir, "Sklad.xml"); } }   // TODO => na databazi ... 
        public static string ConfigPriorityColors { get { return Path.Combine(DataDir, "PriorityColors.xml"); } }
        public static string StiahnuteVydajkyName { get { return Path.Combine(StorageDir, "hlavickyVydej.xml"); } }
        public static string StiahnuteVydajkyNameSchema { get { return Path.Combine(StorageDir, "hlavickyVydejSchema.xml"); } }
        public static string StiahnutePrijemkyName { get { return Path.Combine(StorageDir, "hlavickyPrijem.xml"); } }
        public static string StiahnutePrijemkyNameSchema { get { return Path.Combine(StorageDir, "hlavickyPrijemSchema.xml"); } }
        public static string StiahnuteServiskyName { get { return Path.Combine(StorageDir, "hlavickyServis.xml"); } }
        public static string StiahnuteServiskyNameSchema { get { return Path.Combine(StorageDir, "hlavickyServisSchema.xml"); } }
        public static string ConfigRFCodesRemoved { get { return Path.Combine(DataDir, "RFCodesRemoved.txt"); } }

        //Events
        public static string CiselnikEventsTypesDB { get { return Path.Combine(StorageDir, "EventsTypes.prd"); } }
        public static string EventsUserDBData { get { return Path.Combine(DataDir, "EventsUser.prd"); } }
		public static string EventsUserDBSQLCeDBs { get { return Path.Combine(SQLiteDBsDir, "EventsUser.prd"); } }

		//Servis

		public const string ServisCiselnikFileName = "Servis_Ciselniky.prd";
		public const string ServisZdrojePohybFileName = "Servis_ZdrojePohyb.prd";
		public const string ServisZdrojePohybTmpFileName = "Servis_ZdrojePohyb.prd.tmp";
		public const string ServisZdrojeStavFileName = "Servis_ZdrojeStav.prd";
        public const string ServisZdrojeStavTmpFileName = "Servis_ZdrojeStav.prd.tmp";

		public static string ServisCiselnikDB { get { return Path.Combine(StorageDir, ServisCiselnikFileName); } }
		public static string ServisZdrojePohybDB { get { return Path.Combine(StorageDir, ServisZdrojePohybFileName); } }
        public static string ServisZdrojePohybDBTmp { get { return Path.Combine(StorageDir, ServisZdrojePohybTmpFileName); } }
		public static string ServisZdrojeStavDB { get { return Path.Combine(StorageDir, ServisZdrojeStavFileName); } }
        public static string ServisZdrojeStavDBTmp { get { return Path.Combine(StorageDir, ServisZdrojeStavTmpFileName); } }



        // pripony datovych souboru
        public const string Ext_Vydej = "vi";
        public const string Ext_Inventura1 = "in1";
        public const string Ext_Inventura2 = "in2";
        public const string Ext_Prodej = "di";
        public const string Ext_Prijem = "pp";
        public const string Ext_ServisI = "si";
        public const string Ext_EventsToTransfer = "evt";

		public const string PRD = ".prd";

        // pracovni adresar
        public static string WrkDir = @"Flash Storage\MST_W\";

        static Main()
        {
            try
            {
                WrkDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\";
            }
            catch
            {
            }
        }

        //public const string WrkDir = @"Program Files\MST_W\";
        //public const string WrkDir = @"application\MST_W\";
        private static string AppCurrentDataDir = @"Data\";
        public static string StorageDir { get { return MST_Global.Storage; } }
        public static string SoundDir { get { return WrkDir + @"Sounds\"; } }
        public static string DataDir { get { return WrkDir + AppCurrentDataDir; } }
        public static string ConfigDir { get { return Path.Combine(WrkDir, "Config"); } }
		public static string SQLiteDBsDir { get { return Path.Combine(WrkDir, "SQLiteDBs"); } }
        public static string LocalizationDir
        {
            get
            {
                string localizationdir = Path.Combine(WrkDir, "Lokalizace");
                if (!Directory.Exists(localizationdir))
                    Directory.CreateDirectory(localizationdir);
                return localizationdir;
            }
        }
        public static string ImagesDir
        {
            get
            {
                string imagesdir = Path.Combine(WrkDir, "Images");
                if (!Directory.Exists(imagesdir))
                    Directory.CreateDirectory(imagesdir);
                return imagesdir;
            }
        }

        #endregion

        #region Premenne
        // ostatni promenne
        public MySystem.MyBackgroundWorker mbw;
        //public int userID;
        //public byte IDTerminal;

        // 26.7.2016 v Husky: byla vytvorena funkcnost nad sklady, takze toto je nadbytecne !!!
        //public string kodSkladu = string.Empty; //Kod skladu, ktery se bude dohledavat (vydej)

        public string itemType = string.Empty;  //Typ polozky, ktery se bude dohledavat (vydej)
        //aktualny rozmer
        public Size AktualnyRozmerOkna = new Size();

        internal Licence.Licensing _licence = null;

        public static SplashScreen splashScreen = null;
        #endregion

        #region Splash
        public static void StartSplash()
        {
            //// Instance a splash form given the image names
            //splashScreen = new SplashScreen();
            // Run the form
            splashScreen = new SplashScreen();
            splashScreen.Show();

            Application.DoEvents();
            //splashScreen.TopMost = false;

        }
        public static void CloseSplash()
        {
            if (splashScreen == null)
                return;

            // Shut down the splash screen

            //splashScreen.Invoke(new EventHandler(splashScreen.KillMe));
            splashScreen.Close();
            splashScreen.Dispose();
            splashScreen = null;
            Application.DoEvents();
        }
        #endregion

        #region Stahnout Ciselniky

        public _WebRefernces_Globals.CiselnikServiceSession ciselnikS = null;

        #endregion


        private string originalText = string.Empty;

        #region Construct
        public Main()
        {
            StartSplash();

            InitializeComponent();
            try
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                //this.Text = "MST_W(" + version.ToString(2) + ")";
                this.Text = "MST_W,";
                this.originalText = this.Text;
            }
            catch { }

            try
            {
                Logging.Log.Delete();
            }
            catch
            {
            }
        }
        #endregion

        #region Functions
        public bool ScannerEventAdd(ScannerProvider.ScannerEventHandler method)
        {
            try
            {
                if (this.Scanner != null)
                {
                    Delegate[] invocationList = this.Scanner.InvocationList();

                    if (invocationList.Contains(method))
                    {
#if DEBUG
                        Logging.Log.Write(new Exception(string.Format("Scanner method is allready contained in invocation list. Assambly : : '{0}' ", method.Method.DeclaringType.AssemblyQualifiedName)));
#endif
                        //throw new Exception("Scanner method is allready contained in invocation list...");
                        return true;
                    }
                    else if (invocationList.Count() > 0)
                    {

                        Logging.Log.Write(string.Format("Scanner invocation list contains {0} methods.", invocationList.Count()));

                        foreach (var item in invocationList)
                        {

                            ParameterInfo[] Params = item.Method.GetParameters();

                            foreach (ParameterInfo param in Params)
                            {
                                Logging.Log.Write(string.Format("Scanner invocation Method {0}", invocationList[0].Method.Name));
                                Logging.Log.Write(string.Format("Scanner invocation list {0}", param.Member.DeclaringType.AssemblyQualifiedName));
                            }
                        }

                        throw new Exception(String.Format("Scanner invocation list contains {0} methods: first:{1}", invocationList.Count(), invocationList[0].Method.Name));
                    }

                    this.Scanner.DataReady -= method;
                    this.Scanner.DataReady += method;
                }
                else
                {
                    throw new Exception("Scanner is not initialized...");
                }
            }
            catch (Exception exScanner)
            {
                Logging.Log.Write(exScanner);
#if DEBUG
                MessageBox.Show("Critical Error : " + exScanner.Message, "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
#endif      
                //throw exScanner;
            }

            return true;
        }
        public bool ScannerEventRemove(ScannerProvider.ScannerEventHandler method)
        {
            if (this.Scanner != null)
                this.Scanner.DataReady -= method;

            if (this.Scanner.InvocationList().Count() > 0)
            {
                Delegate[] invocationList = this.Scanner.InvocationList();

                Logging.Log.Write(new Exception("Scanner obsahuje metody..."));

                foreach (var item in invocationList)
                {

                    ParameterInfo[] Params = item.Method.GetParameters();

                    foreach (ParameterInfo param in Params)
                    {
                        Logging.Log.Write(string.Format("Scanner invocation Method {0}", invocationList[0].Method.Name));
                        Logging.Log.Write(string.Format("Scanner invocation list {0}", param.Member.DeclaringType.AssemblyQualifiedName));
                    }
                }
            }

            return true;
        }

        public void EnableScanner()
        {
			if (this.Scanner != null)
			{
				this.Scanner.Enable();
			}
        }
        public void DisableScanner()
        {
            if (this.Scanner != null)
                this.Scanner.Disable();
        }
        private bool PerformEnd()
        {
            if (MessageBoxBig.Show("Opravdu chcete ukonèit aplikaci?", "MST_W", MessageBoxButtons.YesNo, MessageBoxBigIcon.Question)
                == DialogResult.No)
                return false;

            if (Scanner != null)
                Scanner.TerminateScanner();

            // TODO : RFID Scanner Terminate ... 

            this.Close();

            return true;
        }
        #endregion

        #region Login

        /// <summary>
        /// Vyzve operatora k zadani loginu a hesla a zaloguje jej
        /// </summary>
        private void ZalogujOperatora()
        {
            Cursor.Current = Cursors.WaitCursor;

            Settings.UserLogin = string.Empty;

            bool closeApp = false;

            Fask.MST_W.Forms.LoginForm2 loginForm = new Fask.MST_W.Forms.LoginForm2();
            try
            {
                FileStream lastUserFile = null;
                bool loginOK = false;
                BinaryReader br;
                BinaryWriter bw;



                // nacteme si naposledy zalogovaneho uzivatele
                try
                {
                    lastUserFile = File.Open(LastUserFileName, FileMode.Open, FileAccess.Read);
                    if (lastUserFile.Length > 0)
                    {
                        br = new BinaryReader(lastUserFile);
                        loginForm.Login = br.ReadString();
                    }
                    lastUserFile.Close();

                    /*
                                    loginForm.Login = (new localhost.Service()).ReturnS("username");
                    */
                }
                catch (FileNotFoundException fnfex) // soubor tam neni, nevadi
                {
                    Fask.Logging.Log.Write(fnfex.Message, "Zaloguj operátora");
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.Log.Write(ex.Message, "Zaloguj operátora");
                }

                TasksSynchronizationStop();

                Cursor.Current = Cursors.Default;
                loginForm.ResetPasswd(false);

                while (!loginOK)
                {
                    loginForm.ShowDialog();

                    if (loginForm.DialogResult != DialogResult.OK)
                    {
                        closeApp = true;
                        break;
                    }
                    else
                    {
                        try
                        {
                            Cursor.Current = Cursors.WaitCursor;


                            LogOperator.Login(CiselnikUzivateleDB, WrkDir + "operator.log",
                                loginForm.Login, loginForm.Heslo);
                            /*LogOperator.Login(WrkDir + @"Data\passwd.xml", WrkDir + "operator.log",
                                loginForm.Login, loginForm.Heslo);*/
                            loginOK = true;

                            Cursor.Current = Cursors.Default;
                        }
                        catch (MySystem.LogOperator.LoginNotFoundException e)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBoxBig.Show(e.Message, "Chyba", MessageBoxButtons.OK,
                                MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button1);
                            loginForm.ResetPasswd(true);
                        }
                        catch (MySystem.LogOperator.IncorrectPasswordException e)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBoxBig.Show(e.Message, "Chyba", MessageBoxButtons.OK,
                                MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button1);
                            loginForm.ResetPasswd(false);
                        }
                        catch (LogOperator.PasswdFileCorruptedException e)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBoxBig.Show(e.Message + "\nKonèím aplikaci.", "Chyba", MessageBoxButtons.OK,
                                MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                            this.Close();
                            return;
                        }
                        catch (System.Xml.XmlException e)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBoxBig.Show(e.Message + "\nKonèím aplikaci.", "Chyba", MessageBoxButtons.OK,
                                MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                            this.Close();
                            return;
                        }
                        catch (System.Exception e)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBoxBig.Show(e.Message + "\nKonèím aplikaci.", "Chyba", MessageBoxButtons.OK,
                                MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                            this.Close();
                            return;
                        }
                        finally
                        {
                            Cursor.Current = Cursors.Default;
                        }

                        //Jestlize neni login v poradku, pokracuje dalsi smyckou ...
                        if (!loginOK)
                            continue;

                        MST_Global.UserID = Convert.ToInt32(LogOperator.UserID);
                        MST_Global.UserLoginName = Settings.UserLogin = loginForm.Login.Trim();
                        MST_Global.UserPwd = loginForm.Heslo;
                        this.Text = this.originalText + "(" + Settings.UserLogin.Trim() + ")";

                        // zapiseme si naposledy zalogovaneho uzivatele
                        try
                        {
                            lastUserFile = File.Open(LastUserFileName, FileMode.Create, FileAccess.Write);
                            bw = new BinaryWriter(lastUserFile);
                            bw.Write(LogOperator.OperatorLogin);
                            lastUserFile.Close();
                            lastUserFile = null;
                        }
                        catch (FileNotFoundException)
                        {
                            MessageBoxBig.Show("Nepodaøilo se zapsat login do konfiguraèního souboru!",
                                "Info", MessageBoxButtons.OK, MessageBoxBigIcon.Warning, MessageBoxDefaultButton.Button1);
                        }
                        finally
                        {
                            if (lastUserFile != null)
                                lastUserFile.Close();
                        }
                    }

                }// while(!loginOK)

                if (!closeApp)
                {
                    TasksSynchronizationStart();
                    UpdateStatusBar();
                    this.Show();

					this.BeginInvoke((ThreadStart)delegate()
					{
						Automatika_Invoke();
					});
                }


                //Zobrazit okno, pokud je minimalizovane ...

            }
            catch (Exception e)
            {
                Logging.Log.Write(e);
            }
            finally
            {
                loginForm.Dispose();
                Cursor.Current = Cursors.Default;

            }

            if (closeApp)
            {
                if (!PerformEnd())
                {
#if LOGIN_ENABLED
                    this.BeginInvoke((MethodInvoker)delegate { ZalogujOperatora(); });
#endif
                }
            }
        }

        private void Login()
        {
            DialogResult res = MessageBoxBig.Show("Opravdu se chcete odhlásit?", "Dotaz",
                MessageBoxButtons.YesNo, MessageBoxBigIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
            {
                LogOperator.Logout(WrkDir + "operator.log");
                ZalogujOperatora();
            }
        }

        #endregion

		#region Automatika

		private void Automatika_Invoke()
		{
			//10.9.2020 TaD Tady atomaticky konfiguracne spustit Modul X bez nutnosti maèkat èudlik
			MST_Global.Automatika_FirtsRun = true;



			if (Settings.uia_enable && Settings.uia_Expedice)
			{ buttonExpedice_Click(null, null); }
			else if (Settings.uia_enable && Settings.uia_inventura1)
			{ buttonInventura1csv_Click(null, null); }
			else if (Settings.uia_enable && Settings.uia_inventura2)
			{ buttonInventura2_Click(null, null); }
			else if (Settings.uia_enable && Settings.uia_prijem)
			{ buttonPrijem_Click(null, null); }
			else if (Settings.uia_enable && Settings.uia_prodej)
			{ buttonProdej_Click(null, null); }
			else if (Settings.uia_enable && Settings.uia_Servis)
			{ buttonServis_Click(null, null); }
			else if (Settings.uia_enable && Settings.uia_udalosti)
			{ buttonEvents_Click(null, null); }
			else if (Settings.uia_enable && Settings.uia_ukoly)
			{ buttonTasks_Click(null, null); }
			else if (Settings.uia_enable && Settings.uia_vydej)
			{ buttonVydej_Click(null, null); }
			else
			{
				MST_Global.Automatika_FirtsRun = false;
			}
		}

		
		#endregion

        #region Events
        private void menuItem3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Main_MST_W_Load(object sender, EventArgs e)
        {

            this.AktualnyRozmerOkna = this.Size;

            //tlacitko se musi nejprve schovat, 
            //jinak bude vzdy viditelne pri jakemkoliv nastaveni
            this.buttonInventura1csv.Visible = false;
            this.buttonInventura2.Visible = false;
            this.buttonPrijem.Visible = false;
            this.buttonProdej.Visible = false;
            this.buttonVydej.Visible = false;
            this.buttonEvents.Visible = false;
            this.buttonTasks.Visible = false;
            this.buttonServis.Visible = false;
            this.buttonExpedice.Visible = false;
            //this.buttonHuskyVratka.Visible = false;

            this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;

            if (!Settings.Online_BYZNYS)
            {
                mainMenu1.MenuItems.Remove(menuItemOnline);
            }

			

            timerLoad.Enabled = true;
        }

        private void LoadRFCodesRemoved()
        {
            //if (Settings.Inventura2RemovedRFIDCodes == null)
            //    Settings.Inventura2RemovedRFIDCodes = new List<string>();

            //if (!File.Exists(MST_W.Main.ConfigRFCodesRemoved))
            //    return;

            //using (StreamReader r = new StreamReader(MST_W.Main.ConfigRFCodesRemoved))
            //{
            //    string line;

            //    while ((line = r.ReadLine()) != null)
            //    {
            //        Settings.Inventura2RemovedRFIDCodes.Add(line);
            //    }
            //}

            List<string> removedCodes = new List<string>();
            Settings.RemovedRFIDCodes = removedCodes;

            if (!File.Exists(MST_W.Main.ConfigRFCodesRemoved))
                return;

            using (StreamReader r = new StreamReader(MST_W.Main.ConfigRFCodesRemoved))
            {
                string line;

                while ((line = r.ReadLine()) != null)
                {
                    if (!removedCodes.Contains(line))
                        removedCodes.Add(line);
                }
            }

            //if (RFIDUHFScanner != null)
            //    RFIDUHFScanner.RemovedCodes = removedCodes;

        }
        private void Main_MST_W_Shown(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;

            try
            {

                #region Multistart test
                // Instance a splash form given the image names
                if (Settings.UIMultistartTest)
                {
                    try
                    {
                        if (splashScreen != null)
                            splashScreen.Status = "Test vícenásobného spuštìní";

                        MySystem.Process[] procesy = MySystem.Process.GetProcesses();
                        string AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                        AssemblyName += ".exe";

                        int InstanceCount = 0;
                        for (int i = 0; i < procesy.Length; i++)
                        {
                            if (procesy[i].ProcessName == AssemblyName)
                                InstanceCount++;
                        }

                        if (InstanceCount > 1)
                        {
							// TODO : if running -> question to kill and try to continue this instance ...

                            CloseSplash();
                            MessageBoxBig.Show("Aplikace je již spuštìna.", "Chyba", MessageBoxButtons.OK,
                                MessageBoxBigIcon.Critical, MessageBoxDefaultButton.Button1);
                            Application.Exit();
                            return;
                        }
                    }
                    catch (Exception exmultistart)
                    {
                        splashScreen.Hide();
                        Logging.Log.Write(exmultistart);
                        MessageBoxBig.Show(exmultistart.Message, "Multistart test", MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                    }
                }
                #endregion

                #region Hide task bar
                // schova taskbar
                if (Settings.UIHideTaskBar)
                {
                    if (splashScreen != null)
                        splashScreen.Status = "Schování taskbaru";

                    int h = FindWindow("HHTaskBar", "");
                    ShowWindow(h, 0);
                    EnableWindow(h, false);
                }
                #endregion

                mbw = new MySystem.MyBackgroundWorker();

                CloseSplash();
                // zvoleni s jakou konfiguraci ma aplikace pracovat
                using (ConfigAppForm appConfig = new ConfigAppForm())
                {
                    if (appConfig.ShowDialog() == DialogResult.Cancel)
                    {
                        this.Close();
                        return;
                    }

                    AppCurrentDataDir = appConfig.SelectedConfig.DataFolderPath;
                }

                StartSplash();

                if (splashScreen != null)
                    splashScreen.Status = "Naèítání Config souboru";

                MST_Global.Load(Main.ConfigTerminalFileName);
                MST_Global.Load(Main.ConfigModulesFileName);
                MST_Global.PriorityColorsLoad();
				Prodej.Globals.Load(Main.ConfigModulesFileName);
				Prijem_4.Globals.Load(Main.ConfigModulesFileName);

				#region 26.3.2020 - Certificate policy - viz. Tierra Verde https protocol
				// INFO : po nacteni konfigurace se musi ihned nastavit pravidla pro komunikace, protoze jinak neuspeji online dotazy ...
				// pri predani v tierra verde 25.3.2020 toto nastalo, pri zmene na https protokol -> problem s validaci certifikatu, protoze nebylo dobre nastaveno ... 
				// otazka jestli by nemelo byt umisteno jinde ? -> primo pri nastaveni hodnoty MST_Global.ServerAccessCertificateTrust ? asi ano ... 
				// INFO: prenesono primo do setteru parametru MST_Global.ServerAccessCertificateTrust

				//if (splashScreen != null)
				//    splashScreen.Status = "Nastavení pravidel certikátù";
				//switch (MST_Global.ServerAccessCertificateTrust)
				//{
				//    case MST_Global.ServerAccessCertificatesTrustType.TrustAll:
				//        System.Net.ServicePointManager.CertificatePolicy = new ServerAccess.CertificatesPolicy.TrustAllCertificatePolicy();
				//        break;
				//    case MST_Global.ServerAccessCertificatesTrustType.TrustQuery:
				//        System.Net.ServicePointManager.CertificatePolicy = new ServerAccess.CertificatesPolicy.QueryTrustCertificatePolicy();
				//        break;
				//    case MST_Global.ServerAccessCertificatesTrustType.OnlyInstalled:
				//    default:
				//        break;
				//}
				#endregion


                #region Synchronizace èasu
                try
                {
                    if (Settings.SystemTimeUpdate)
                    {
                        if (splashScreen != null)
                            splashScreen.Status = "Synchronizace èasu";

                        Classes.SystemTimeSynchronization timesync = new Classes.SystemTimeSynchronization();
                        if (!timesync.Synchronize())
                        {
                            throw new Exception("Synchronizace èasu se nezdaøila!");
                        }
                    }
                }
                catch (WebException webex)
                {
                    Logging.Log.Write(webex);
                    if (splashScreen != null)
                        splashScreen.Status = webex.Message;
                    System.Threading.Thread.Sleep(4000);
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    if (splashScreen != null)
                        splashScreen.Status = ex.Message;
                    System.Threading.Thread.Sleep(4000);
                }
                #endregion


                #region Licence
				// TODO : Upravit lepe validaci licence, èasty problem s èasem, kde terminal ma licenci, vytvoøenou k urèitemu datu, ale terminal je v minulosti...  

                try
                {
                    if (splashScreen != null)
                        splashScreen.Status = "Kontrola licence";

                    _licence = new Fask.MST_W.Licence.Licensing();

                    if (!_licence.IsLicensed)
                    {
                        if (splashScreen != null)
                            splashScreen.Status = "Licence: Není platná => '" + _licence.Licence + "'" + Environment.NewLine + _licence.Status;
                        System.Threading.Thread.Sleep(10000);
                    }

                    //terminal ID kde se nacitava< tady este neni
                    if (_licence.TerminalID != MST_Global.TerminalID.ToString())
                    {
                        if (splashScreen != null)
                        {
                            _licence.IsLicensed = false;
                            _licence.Licence = "DEMO";
                            splashScreen.Status = "Licence: Není správné ID Terminálu pro licenci => '" + _licence.Licence + "'";
                        }
                        System.Threading.Thread.Sleep(10000);
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "Licence");
                    if (splashScreen != null)
                        splashScreen.Status = ex.Message;
                    System.Threading.Thread.Sleep(4000);
                }



                _WebRefernces_Globals.ConfigurationServiceSession configurations = new _WebRefernces_Globals.ConfigurationServiceSession();
                configurations.Timeout = MST_Global.ServiceTimeOut;
                configurations.Url = MST_Global.ServerAddress + "Configuration.asmx";
                configurations.UpdateWebServiceCredentials();

                ConfigurationService.License licenceServer = null;

                try
                {
                    if (splashScreen != null)
                        splashScreen.Status = "Kontrola licence na serveru";

                    licenceServer = configurations.GetLicenceInfo();

                    if (licenceServer != null)
                    {
                        if (!licenceServer.isValid || licenceServer.isExpirated)
                        {
                            splashScreen.Status = "Licence na serveru není platná!";
                            System.Threading.Thread.Sleep(10000);
                        }
                        else if (licenceServer.showInfoExpirationInTerminal)
                        {
                            splashScreen.Status = "Licence na serveru vyprší " + licenceServer.expiration;
                            System.Threading.Thread.Sleep(2000);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "Licence Serveru");
                    if (splashScreen != null)
                        splashScreen.Status = ex.Message;
                    System.Threading.Thread.Sleep(4000);
                }

                #endregion


				// TODO : nacitani konfigurace ??? -> spise nacitani lokalizace.
				// TODO : a proc to nakonec closne? -> mohl by pokracovat??? 
				if (splashScreen != null)
				{
					//splashScreen.Status = "Naèítání konfiguraci";
					splashScreen.Status = "Naèítání Lokalizaci";
				}

                try
                {
                    // inicializace lokalizace
                    Fask.Localization.Globals.LokalizacePovolit = MST_Global.LokalizacePovolit;
                    Fask.Localization.Globals.LokalizaceVlastniPovolit = MST_Global.LokalizaceVlastniPovolit;
                    Fask.Localization.Globals.LocalizationDir = LocalizationDir;
                    Fask.Localization.Globals.LokalizaceZvolena = MST_Global.LokalizaceZvolena;
                    if (Fask.Localization.Globals.LokalizacePovolit)
                        Fask.Localization.Localization.Culture = System.Globalization.CultureInfo.GetCultureInfo(Fask.Localization.Globals.LokalizaceZvolena.ToString());
                    else
                        Fask.Localization.Localization.Culture = System.Globalization.CultureInfo.GetCultureInfo("");   // TODO: otestovat defaultni culture info
                    //Fask.Localization.Localization.Culture = System.Globalization.CultureInfo.GetCultureInfo("cs");
                    // nacteni dat z Location.rest a jeho obdob, pokud je lokalizace povolena
                    Fask.Localization.LocalizationSupport.InitLocalizationData();
                }
                catch (Exception ex)
                {
                    CloseSplash();
                    MessageBox.Show(ex.Message, Properties.Resources.errNacitaniKonfiguracnihoSouboru);
                    //Fask.Logging.Log.Write(ex.Message, "Naèítání konfigurace");
					Fask.Logging.Log.Write(ex.Message, "Naèítání Lokalizaci");
                    this.Close();
                }

                if (splashScreen != null)
                    splashScreen.Status = "Inicializace modulù";

                bool modulfocused = false;
                //vytvorenie modulov
                MenuItem mojeMenu = null;


                // 20.6.2018 TaD do menu pridana možnost zaktualizovat všechny èiselniky
                mojeMenu = new MenuItem();
                mojeMenu.Text = "Aktualizovat èísleniky";
                mojeMenu.Click += new EventHandler(mojeMenuCiselniky_Click);
                //mojeMenu.Enabled = _licence.Servis;
                menuItem6.MenuItems.Add(mojeMenu);


                mojeMenu = new MenuItem();
                mojeMenu.Text = "-";
                menuItem6.MenuItems.Add(mojeMenu);




                if (MST_Global.ServisEnable)
                {
                    if (MST_Global.ServisShowInMST)
                    {
                        this.buttonServis.Visible = true;
                        if (!modulfocused)
                            modulfocused = this.buttonServis.Focus();

                        this.buttonServis.Enabled = _licence.Servis;

                    }
                    mojeMenu = new MenuItem();
                    mojeMenu.Text = this.buttonServis.Text;
                    mojeMenu.Click += new EventHandler(mojeMenuServis_Click);
                    mojeMenu.Enabled = _licence.Servis;
                    menuItem6.MenuItems.Add(mojeMenu);
                }

                //if (MST_Global.HuskyVratka)
                //{
                //    if (MST_Global.HuskyVratkaShowInMST)
                //    {
                //        this.buttonHuskyVratka.Visible = true;
                //        if (!modulfocused)
                //            modulfocused = this.buttonHuskyVratka.Focus();
                //    }
                //    mojeMenu = new MenuItem();
                //    mojeMenu.Text = this.buttonHuskyVratka.Text;
                //    mojeMenu.Click += new EventHandler(mojeMenuHuskyVratka_Click);
                //    menuItem6.MenuItems.Add(mojeMenu);
                //}

                if (MST_Global.TasksEnable)
                {
                    if (MST_Global.TasksShowInMST)
                    {
                        this.buttonTasks.Visible = true;
                        this.buttonTasks.Enabled = _licence.Tasks;
                        //this.buttonTasks.Text = MST_Global.Inventura1Name;
                        if (!modulfocused)
                            modulfocused = this.buttonTasks.Focus();
                    }
                    mojeMenu = new MenuItem();
                    //mojeMenu.Text = MST_Global.Inventura1Name;
                    mojeMenu.Text = this.buttonTasks.Text;
                    mojeMenu.Click += new EventHandler(mojeMenuTasks_Click);
                    mojeMenu.Enabled = _licence.Tasks;
                    menuItem6.MenuItems.Add(mojeMenu);
                }
                if (MST_Global.EventsEnable)
                {
                    if (MST_Global.EventsShowInMST)
                    {
                        this.buttonEvents.Visible = true;
                        this.buttonEvents.Enabled = _licence.Events;
                        //this.buttonEvents.Text = MST_Global.Inventura1Name;
                        if (!modulfocused)
                            modulfocused = this.buttonEvents.Focus();
                    }
                    mojeMenu = new MenuItem();
                    //mojeMenu.Text = MST_Global.Inventura1Name;
                    mojeMenu.Text = this.buttonEvents.Text;
                    mojeMenu.Click += new EventHandler(mojeMenuUdalosti_Click);
                    mojeMenu.Enabled = _licence.Events;
                    menuItem6.MenuItems.Add(mojeMenu);
                }
                if (MST_Global.Inventura1)
                {
                    if (MST_Global.Inventura1ShowInMST)
                    {
                        this.buttonInventura1csv.Visible = true;
                        this.buttonInventura1csv.Enabled = _licence.Inventura1;
                        this.buttonInventura1csv.Text = MST_Global.Inventura1Name;
                        if (!modulfocused)
                            modulfocused = this.buttonInventura1csv.Focus();
                    }
                    mojeMenu = new MenuItem();
                    mojeMenu.Text = MST_Global.Inventura1Name;
                    mojeMenu.Click += new EventHandler(mojeMenuInventura_Click);
                    mojeMenu.Enabled = _licence.Inventura1;
                    menuItem6.MenuItems.Add(mojeMenu);
                }
                if (MST_Global.Inventura2)
                {
                    if (MST_Global.Inventura2ShowInMST)
                    {
                        this.buttonInventura2.Visible = true;
                        this.buttonInventura2.Enabled = _licence.Inventura2;
                        this.buttonInventura2.Text = MST_Global.Inventura2Name;
                        if (!modulfocused)
                            modulfocused = this.buttonInventura2.Focus();
                    }
                    mojeMenu = new MenuItem();
                    mojeMenu.Text = MST_Global.Inventura2Name;
                    mojeMenu.Click += new EventHandler(mojeMenuInventura2_Click);
                    mojeMenu.Enabled = _licence.Inventura2;
                    menuItem6.MenuItems.Add(mojeMenu);
                }
                if (MST_Global.Prijem)
                {
                    this.buttonPrijem.Visible = true;
                    this.buttonPrijem.Text = MST_Global.PrijemName;
                    this.buttonPrijem.Enabled = _licence.Prijem;
                    if (!modulfocused)
                        modulfocused = this.buttonPrijem.Focus();
                    mojeMenu = new MenuItem();
                    mojeMenu.Text = MST_Global.PrijemName;
                    mojeMenu.Click += new EventHandler(mojeMenuPrijem_Click);
                    mojeMenu.Enabled = _licence.Prijem;
                    menuItem6.MenuItems.Add(mojeMenu);
                }
                if (MST_Global.Prodej)
                {
                    this.buttonProdej.Visible = true;
                    this.buttonProdej.Text = MST_Global.ProdejName;
                    this.buttonProdej.Enabled = _licence.Prodej;
                    if (!modulfocused)
                        modulfocused = this.buttonProdej.Focus();
                    mojeMenu = new MenuItem();
                    mojeMenu.Text = MST_Global.ProdejName;
                    mojeMenu.Click += new EventHandler(mojeMenuProdej_Click);
                    mojeMenu.Enabled = _licence.Prodej;
                    menuItem6.MenuItems.Add(mojeMenu);
                }
                if (MST_Global.Vydej)
                {
                    this.buttonVydej.Visible = true;
                    this.buttonVydej.Text = MST_Global.VydejName;
                    this.buttonVydej.Enabled = _licence.Vydej;
                    if (!modulfocused)
                        modulfocused = this.buttonVydej.Focus();
                    mojeMenu = new MenuItem();
                    mojeMenu.Text = MST_Global.VydejName;
                    mojeMenu.Click += new EventHandler(mojeMenuVydej_Click);
                    mojeMenu.Enabled = _licence.Vydej;
                    menuItem6.MenuItems.Add(mojeMenu);
                }
                if (MST_Global.Expedice)
                {
                    this.buttonExpedice.Visible = true;
                    this.buttonExpedice.Text = MST_Global.ExpediceName;
                    this.buttonExpedice.Enabled = _licence.Expedice;
                    if (!modulfocused)
                        modulfocused = this.buttonExpedice.Focus();
                    mojeMenu = new MenuItem();
                    mojeMenu.Text = MST_Global.ExpediceName;
                    mojeMenu.Click += new EventHandler(mojeMenuExpedice_Click);
                    mojeMenu.Enabled = _licence.Expedice;
                    menuItem6.MenuItems.Add(mojeMenu);
                }

                //this.buttonInventura1csv.Enabled = MST_Global.Inventura1;
                //this.buttonProdej.Enabled = MST_Global.Prodej;
                //this.buttonVydej.Enabled = MST_Global.Vydej;
                //this.buttonPrijem.Enabled = MST_Global.Prijem;

                //JoZ: 2012-07-04: pridano nacitani konfigurace dynamickych knihoven
                try
                {
                    LoadAssembliesConfiguration();
                }
                catch (Exception ex)
                {
                    Fask.Logging.Log.Write(ex.Message, "LoadAssembliesConfiguration");
                    if (splashScreen != null)
                        splashScreen.Status = "LoadAssembliesConfiguration: " + ex.Message;
                    System.Threading.Thread.Sleep(4000);
                }

                if (splashScreen != null)
                    splashScreen.Status = "Aktivace scanneru";

                //JoZ: 2012-07-04: pridano dynamicke nacitani knihoven scanneru
                try
                {
                    LoadAssembliesScanner();
                }
                catch (Exception ex)
                {
                    Fask.Logging.Log.Write(ex, "Aktivace scanneru");
                    if (splashScreen != null)
                        splashScreen.Status = "Barcode scanner: " + ex.Message;
                    System.Threading.Thread.Sleep(4000);
                }

                //LoadAssembliesPhoto
                if (splashScreen != null)
                    splashScreen.Status = "Aktivace focení";

                try
                {
                    LoadAssembliesPhoto();
                }
                catch (Exception ex)
                {
                    Fask.Logging.Log.Write(ex, "Aktivace focení");
                    if (splashScreen != null)
                        splashScreen.Status = "Focení: " + ex.Message;
                    System.Threading.Thread.Sleep(4000);
                }

                if (splashScreen != null)
                    splashScreen.Status = "Aktivace tiskáren";

                try
                {
                    //Printer = new Fask.PrinterFactory.PrinterFactory();
                    Printer = Fask.PrinterFactory.PrinterFactory.Instance;
                }
                catch (Exception ex)
                {
                    Fask.Logging.Log.Write(ex, "Aktivace tiskáren");
                    if (splashScreen != null)
                        splashScreen.Status = "Tiskárny: " + ex.Message;
                    System.Threading.Thread.Sleep(4000);
                }

                if (splashScreen != null)
                    splashScreen.Status = "Aktivace parsování";

                //try
                //{
                //    Parsing = Fask.Parsing.ParsingFactory.Instance;

                //    //Dictionary<string, string> data = Parsing.Parse(Fask.Parsing.ParsingModules.PrijemBarcode, "asdasd");
                //    //Dictionary<string, string> data2 = Parsing.Parse(new Parsing.ParsingModule(Fask.Parsing.ParsingModules.ProdejBarcode, "test"), "asdasd");

                //    //Dictionary<string, string> data3 = Parsing.Parse(new Parsing.ParsingModule(Fask.Parsing.ParsingModules.ProdejBarcode, "ahoj"), "asdasd");
                //    //Dictionary<string, string> data4 = Parsing.Parse(new Parsing.ParsingModule(Fask.Parsing.ParsingModules.ProdejBarcode, "test"), "asdasd,2010");
                //}
                //catch (Exception ex)
                //{
                //    Fask.Logging.Log.Write(ex, "Aktivace parsování");
                //    if (splashScreen != null)
                //        splashScreen.Status = "Parsování: " + ex.Message;
                //    System.Threading.Thread.Sleep(4000);
                //}

                //Pridani RFID scaneru
                //=====================
                try
                {
                    if (MST_Global.RFIDPovolitUHF) //Pokud je poveleno tak zapne na zacatku
                    {
                        if (splashScreen != null)
                            splashScreen.Status = "Aktivace RFID scanneru";

                        switch (MST_Global.RFIDScannerType)
                        {
                            //25.10.2016 - odstraneno => nepouziva se ...
                            //case Fask.MST_W.Scanner.ScannerRFIDTypes.TT8000:
                            //    RFIDUHFScanner = new Scanner.ScannerRFIDTT8000();
                            //    RFIDUHFScanner.Enable();
                            //    RFIDUHFScanner.Power = MST_Global.RFIDPowerLevel;
                            //    break;
                            case Fask.MST_W.Scanner.ScannerRFIDTypes.MC9090:
                                RFIDUHFScanner = new Scanner.ScannerRFIDMC9090();
                                RFIDUHFScanner.Enable();
                                break;
                            case Fask.MST_W.Scanner.ScannerRFIDTypes.MC319Z:
                                RFIDUHFScanner = new Scanner.ScannerRFIDMC319Z();
                                RFIDUHFScanner.Enable();
                                break;
                            case Fask.MST_W.Scanner.ScannerRFIDTypes.MC319Z_v2:
                                RFIDUHFScanner = new Scanner.ScannerRFIDMC319Z_v2();
                                RFIDUHFScanner.Enable();
                                break;
                            case Fask.MST_W.Scanner.ScannerRFIDTypes.None:
                            default:
                                RFIDUHFScanner = null;
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Fask.Logging.Log.Write(ex.Message, "Aktivace RFID scanneru");
                    if (splashScreen != null)
                        splashScreen.Status = "RFID scanner: " + ex.Message;
                    System.Threading.Thread.Sleep(4000);
                }
                //=====================

                LoadRFCodesRemoved();

                #region Aktivace Modulu Udalosti obsluhy

                if (splashScreen != null)
                    splashScreen.Status = "Aktivace událostí obsluhy";

                try
                {
                    eventsUser = Fask.Events.EventsFactory.SharedInstance;
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex);
                    if (splashScreen != null)
                        splashScreen.Status = "Události obsluhy: " + ex.Message;
                    System.Threading.Thread.Sleep(4000);
                }
                finally
                {
                    // TODO : Spusteni threadu pro synchronizaci eventu na server ... 
                    EventsSychronizationStart();
                }
                #endregion

                //#region Aktivace Modulu Ukolovani obsluhy

                //if (splashScreen != null)
                //    splashScreen.Status = "Aktivace úkolování";

                //try
                //{
                //    TasksSynchronizationStart();
                //}
                //catch (Exception ex)
                //{
                //    Logging.Log.Write(ex);
                //    if (splashScreen != null)
                //        splashScreen.Status = "Úkolování: " + ex.Message;
                //    System.Threading.Thread.Sleep(4000);
                //}
                //finally
                //{
                //}
                //#endregion



                //Spusteni thredu pro odesilani logu
                if (Logging.Trace2.Enable || Logging.Log.Enable)
                    log_upload = new System.Threading.Timer(CallbackLogUpload, null, MST_Global.LogUploadInterval * 1000, Timeout.Infinite);

                this.Size = Forms.FormLocation.ScreenResolution;
                //Pro windows mobile, kdy se nemeni velikost oken nutne rucni volani udalosti resize
                this.panel1_Resize(null, null);

                this.Text += " TID:" + MST_Global.TerminalID;
                UpdateStatusBar();
                this.originalText = this.Text;

                //try
                //{ // zjisteni IP adresy ... 
                //    this.Text += " " + MST_W.MySystem.Net.Info;
                //    this.originalText = this.Text;
                //}
                //catch
                //{
                //}

                CloseSplash();

                #region kontrola nove verze

                //// povolit / zadazat update
                //if (false)
                //{
                //    // v konfiguraci mozno zadat
                //    string xml = "http://192.168.1.101/MST_Win_Kom_Server_6/upgrade/update.xml";

                //    Upgrade.Updater upd = new Upgrade.Updater(xml);
                //    if (upd.CheckForNewVersion())
                //    {
                //        PerformEnd();
                //        return;
                //    }


                //}

                #endregion

                ciselnikS = new Fask.MST_W._WebRefernces_Globals.CiselnikServiceSession();
                ciselnikS.Url = MST_Global.ServerAddress + "Ciselnik.asmx";
                ciselnikS.Timeout = MST_Global.ServiceTimeOut;
                ciselnikS.UpdateWebServiceCredentials();

				mi_KonScan.Checked = Program.mstw.Scanner.ContinuousRead;

#if LOGIN_ENABLED
                this.BeginInvoke((MethodInvoker)delegate { ZalogujOperatora(); });
#endif
            }
            catch (Exception ex)
            {
                CloseSplash();

                Logging.Log.Write(ex, "MST_SHOWN");
                MessageBoxBig.Show(ex.Message, "MST_W", MessageBoxButtons.OK, MessageBoxBigIcon.Critical, Color.Red);
                this.Close();
            }
        }

        private void CallbackLogUpload(object o)
        {
            log_upload.Change(Timeout.Infinite, Timeout.Infinite);

            if (Logging.Log.Enable)
                LogUpload(Fask.Logging.Log.FilePath);

            if (Logging.Trace2.Enable)
                LogUpload(Fask.Logging.Trace2.FilePath);

            //resetovat timer pokazde jen jednou aby vzdy pockal nez predchozi thread dokonci upload
            log_upload.Change(MST_Global.LogUploadInterval * 100, Timeout.Infinite);
        }

        private void LogUpload(string file_path)
        {
            try
            {
                if (String.IsNullOrEmpty(file_path))
                    return;

                string file_path_temp = file_path + ".tmp";
                string file_path_zip = file_path_temp + ".zip";

                //neni nic na praci
                if (!File.Exists(file_path) && !File.Exists(file_path_temp) && !(File.Exists(file_path_zip)))
                    return;

                //pokud je co uploadovat tak vytvorit novy .tmp
                //pokud zustal .tmp z minula tak preskocit a poslat ten z minula
                if (File.Exists(file_path) && !File.Exists(file_path_temp) && !(File.Exists(file_path_zip)))
                    File.Move(file_path, file_path_temp);

                if (!File.Exists(file_path_zip))
                {
                    FileTransfer.CompressFile.CompressToZip(file_path_temp, file_path_zip);
                }

				string KamNaServer = MST_Global.TerminalID.ToString() + "\\" + Path.GetFileName(file_path_zip);

				FileTransfer.Uploading.SendFile_API(file_path_zip, KamNaServer, Fask.MST_W.FileTransfer.Uploading.Co.Log);
                //FileTransfer.Uploading.SendFileCalcTime(MST_Global.ServerAddress + "Upload.aspx", file_path_zip, MST_Global.TerminalID.ToString() + "\\" + Path.GetFileName(file_path_zip), Fask.MST_W.FileTransfer.Uploading.Co.Log);
                
                _WebRefernces_Globals.FileTransferServiceSession ft = new Fask.MST_W._WebRefernces_Globals.FileTransferServiceSession();
                ft.Url = MST_Global.ServerAddress + "FileTransfer.asmx";
                ft.Timeout = MST_Global.ServiceTimeOut;
                ft.UpdateWebServiceCredentials();
                ft.SaveLog2(MST_Global.TerminalID, Path.GetFileName(file_path_zip));
                
                File.Delete(file_path_temp);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        public void UpdateStatusBar()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    UpdateStatusBar();
                });
                return;
            }
            string l = "L: " + (_licence != null ? _licence.Licence : Licence.Licensing.licensedemo);
            // TODO : pridat informaci o ukolech ... dotazem na ukolovani_checker...???
            // a take nejake podminky zobrazeni ... ???
            string u = string.Empty;
            if (MST_Global.TasksEnable)
                u = Ukolovani_1.Ukolovani_Checker.UkolyInfo(MST_Global.UserID) + " ";

            this.statusBarInfo.Text = u + l;
        }

        private void LoadAssembliesConfiguration()
        {
            string FilePath;
            try
            {
                //FilePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "Settings.xml");
                FilePath = ConfigModulesFileName; //Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "Settings.xml");
                //FilePath = (new Uri(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "Settings.xml"))).LocalPath;
                string pom;

                #region Parsovani konfigurace externich pluginu, reseni ...
                using (XmlReader reader = XmlReader.Create(FilePath))
                {
                    while (reader.Read())
                    {

                        // Only detect start elements.
                        if (reader.IsStartElement())
                        {
                            // Get element name and switch on it.
                            switch (reader.Name)
                            {
                                case "Assembly":

                                    Classes.AssemblyInfo assemb = new Fask.MST_W.Classes.AssemblyInfo();

                                    pom = reader["path"];
                                    if (pom != null)
                                        assemb.dllPath = pom;
                                    //  MST_Global.dllPath.Add(pom);
                                    pom = reader["buttonName"];
                                    if (pom != null)
                                        assemb.buttonText = pom;
                                    //   MST_Global.buttonName.Add(pom);
                                    pom = reader["isButtonEnabled"];
                                    if (pom != null)
                                        assemb.isButtonEnabled = bool.Parse(pom);
                                    //  MST_Global.buttonName.Add(pom);
                                    pom = reader["keyCode"];
                                    if (pom != null)
                                        assemb.keyCode = pom;
                                    //  MST_Global.buttonName.Add(pom);

                                    MST_Global.assemblies.Add(assemb);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                #endregion

                #region Natazeni externich modulu do aplikace ...
                Assembly ass;
                //string instance = "Fask.Module";//ToDo: odstranit...

                MenuItem mojeMenu;
                Button button1;

                //Size nsize = ButtonsSize();
                //// ??? coze proc ???
                //if (nsize == new Size(0, 0))
                //    return;

                for (int i = 0; i < MST_Global.assemblies.Count; i++)
                {
                    ass = Assembly.LoadFrom(Path.Combine(WrkDir, MST_Global.assemblies[i].dllPath));
                    Type[] types = ass.GetTypes();
                    foreach (Type t in types)
                    {
                        if (typeof(Fask.ModuleProvider.IModuleProvider).IsAssignableFrom(t))
                        {
                            Fask.ModuleProvider.IModuleProvider service = (Fask.ModuleProvider.IModuleProvider)ass.CreateInstance(t.FullName);

                            #region Pridani do menu a tlacitko ...
                            if (service != null)
                            {
                                button1 = new Button();
                                button1.Dock = System.Windows.Forms.DockStyle.Top;
                                button1.Location = new System.Drawing.Point(0, 0);
                                button1.BackColor = System.Drawing.Color.Lime;
                                button1.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
                                // button1.Size = nsize; // toto je asi jedno, protoze se to pozdeji prepocita ...
                                button1.Text = MST_Global.assemblies[i].buttonText;
                                //button1.Tag = service;
                                System.EventHandler handler = new System.EventHandler(
                                (System.EventHandler)delegate(object sender, EventArgs e)
                                {
                                    service.Execute(new Fask.ModuleProvider.ExecuteParams(MST_Global.TerminalID, MST_Global.UserLoginName, MST_Global.UserID, MST_Global.UserPwd, MST_Global.ServerAddress, MST_Global.ServiceTimeOut, Settings.UIGridFont, Settings.UIFormatDesCisel), Scanner);
                                });
                                button1.Click += handler;

                                MST_Global.assemblies[i].eventHandler = handler;

                                if (MST_Global.assemblies[i].isButtonEnabled)
                                    this.panel1.Controls.Add(button1);

                                mojeMenu = new MenuItem();
                                mojeMenu.Text = MST_Global.assemblies[i].buttonText;
                                mojeMenu.Click += handler;

                                //mojeMenu.Click += new System.EventHandler(
                                //(System.EventHandler)delegate(object sender, EventArgs e)
                                //{
                                //    service.Execute(new Fask.ModuleProvider.ExecuteParams(Settings.UserLogin, MST_Global.UserID), MST_Global.scanner);
                                //});
                                menuItem6.MenuItems.Add(mojeMenu);

                            }
                            #endregion
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                throw ex;
            }
        }

        private static string LoadElement(XmlDocument XmlDoc, string NodeName)
        {
            string nodeValue = string.Empty;

            XmlElement configNode = XmlDoc.SelectSingleNode(NodeName) as XmlElement;

            if (configNode != null)
            {
                try { nodeValue = configNode.InnerText; }
                catch { }
            }

            return nodeValue;
        }

        private void LoadAssembliesScanner()
        {
            Scanner = Fask.ScannerFactory.ScannerFactory.Init();
            MST_Global.ScannerTypeName = Fask.ScannerFactory.ScannerFactory.GetScannerTypeName();
            Scanner.SetForm(this);
            Scanner.InitializeScanner();
        }

        private void LoadAssembliesPhoto()
        {
            Photo = Fask.PhotoFactory.PhotoFactory.Init();
            Photo.ImagesDirectory = ImagesDir;
            Photo.Scanner = Scanner;
            Photo.SetForm(this);
        }

        void miTest_Click(object sender, EventArgs e)
        {
            MySystem.Audio.PlayBeep(150, 150);
        }
        private void mojeMenuVydej_Click(object sender, EventArgs e)
        {
            this.buttonVydej_Click(null, null);
        }
        private void mojeMenuProdej_Click(object sender, EventArgs e)
        {
            this.buttonProdej_Click(null, null);
        }
        private void mojeMenuPrijem_Click(object sender, EventArgs e)
        {
            this.buttonPrijem_Click(null, null);
        }
        private void mojeMenuExpedice_Click(object sender, EventArgs e)
        {
            this.buttonExpedice_Click(null, null);
        }

        private void mojeMenuInventura_Click(object sender, EventArgs e)
        {
            this.buttonInventura1csv_Click(null, null);
        }
        private void mojeMenuUdalosti_Click(object sender, EventArgs e)
        {
            this.buttonEvents_Click(sender, e);
        }
        private void mojeMenuTasks_Click(object sender, EventArgs e)
        {
            this.buttonTasks_Click(sender, e);
        }
        private void mojeMenuServis_Click(object sender, EventArgs e)
        {
            this.buttonServis_Click(sender, e);
        }
        private void mojeMenuCiselniky_Click(object sender, EventArgs e)
        {
            this.buttonCiselniky_Click(sender, e);
        }
        //private void mojeMenuHuskyVratka_Click(object sender, EventArgs e)
        //{
        //    this.buttonHuskyVratka_Click(sender, e);
        //}
        private void mojeMenuInventura2_Click(object sender, EventArgs e)
        {
            this.buttonInventura2_Click(null, null);
        }
        private void Main_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                Logging.Log.Write("main closing");
                // odkryje taskbar
                int h = FindWindow("HHTaskBar", "");
                ShowWindow(h, 1);
                EnableWindow(h, true);

                Logging.Log.Write("main closing tasksynch...");
                TasksSynchronizationStop();

                EventsSynchronizationStop();

                if (Scanner != null)
                {
                    Scanner.Disable();
                    Scanner.TerminateScanner();
                }

                if (RFIDUHFScanner != null)
                {
                    RFIDUHFScanner.TerminateScanner();
                }

                if (Printer != null)
                {
                    Printer.TerminatePrinterProviders();
                }

                Settings.Update();
            }
            catch (Exception exClosing)
            {
                Logging.Log.Write(exClosing);
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                //CloseSplash();
                base.OnClosing(e);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }
        private void menuItem6_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void buttonVydej_Click(object sender, EventArgs e)
        {
            if (!MST_Global.Vydej)
                return;
#if MOD_VYDEJ
            if (MST_Global.VydejItemTypeQuestion)
            {
                using (SejmiKodForm zadejTypePolozky = new SejmiKodForm("Vložte typ položek", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false))
                {
                    zadejTypePolozky.Owner = this;
                    zadejTypePolozky.ZpetButText = "Konec";
                    if (zadejTypePolozky.ShowDialog() == DialogResult.Cancel)
                        return;
                    itemType = zadejTypePolozky.Kod;
                }
                if (itemType == "*") //napevno nastaveny znak pro vsechny polozky
                    itemType = string.Empty;
            }
            else
            {
                itemType = string.Empty;
            }

            //Modifikace vydej_3 - sqlce
            using (Vydej_3.Vydej vydej3 = new Fask.MST_W.Vydej_3.Vydej())
            {
                vydej3.Owner = this;
                vydej3.ShowDialog();
            }
            //}

#else
            MessageBoxBig.Show("Modul není souèástí této kompilace", this.Text);
#endif
            UpdateStatusBar();
            this.Show();

        }
        private void buttonKonec_Click(object sender, EventArgs e)
        {
            PerformEnd();
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void buttonEvents_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonEvents_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            if (!MST_Global.EventsEnable)
                return;

            using (Fask.Events.FormUdalosti udalosti = new Fask.Events.FormUdalosti())
            {
                udalosti.Owner = this;
                udalosti.ShowDialog();
            }
            UpdateStatusBar();
            this.Show();
        }

        private void buttonInventura1csv_Click(object sender, EventArgs e)
        {
            if (!MST_Global.Inventura1)
                return;

#if MOD_INVENTURA1
            //if (!MST_Global.SqlCe)
            //{
            //    MessageBoxBig.Show("This functionality was deprecated", "Inventura1", MessageBoxButtons.OK, MessageBoxBigIcon.Warning, Color.Red);
            //    //using (Inventura1class.Inventura1classForm inventura = new Fask.MST_W.Inventura1class.Inventura1classForm())
            //    //{
            //    //    inventura.Owner = this;
            //    //    inventura.ShowDialog();
            //    //}
            //}
            //else
            //{
            using (Inventura1_sqlce.Inventura1_sqlce inventura = new Fask.MST_W.Inventura1_sqlce.Inventura1_sqlce())
            {
                inventura.Owner = this;
                inventura.ShowDialog();
            }
            //}
#else
            MessageBoxBig.Show("Modul není souèástí této kompilace", this.Text);
#endif
            UpdateStatusBar();
            this.Show();
        }
        private void buttonInventura2_Click(object sender, EventArgs e)
        {
            if (!MST_Global.Inventura2)
                return;

#if MOD_INVENTURA2
            using (Inventura2.Inventura2 inventura2 = new Fask.MST_W.Inventura2.Inventura2())
            {
                inventura2.Owner = this;
                inventura2.ShowDialog();
            }
#else
            MessageBoxBig.Show("Modul není souèástí této kompilace", this.Text);
#endif
            UpdateStatusBar();
            this.Show();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Up)
            //{
            //    //this.SelectNextControl(this, true, true, true, true);
            //    this.OnKeyDown(new KeyEventArgs(Keys.Shift | Keys.Tab));
            //}
            //else if (e.KeyCode == Keys.Down)
            //{
            //    //this.SelectNextControl(this, false, true, true, true);
            //    this.OnKeyDown(new KeyEventArgs(Keys.Tab));
            //}
            //else 

            e.Handled = true;

            if (e.KeyCode == Keys.D1)
            {
                buttonInventura1csv_Click(null, null);
            }
            else if (e.KeyCode == Keys.D2)
            {
                buttonVydej_Click(null, null);
            }
            else if (e.KeyCode == Keys.D3)
            {
                buttonProdej_Click(null, null);
            }
            else if (e.KeyCode == Keys.D4)
            {
                buttonPrijem_Click(null, null);
            }
            else if (e.KeyCode == Keys.D5)
            {
                buttonInventura2_Click(null, null);
            }
            else if (e.KeyCode == Keys.D6)
            {
                buttonEvents_Click(null, null);
            }
            else if (e.KeyCode == Keys.D7)
            {
                buttonTasks_Click(null, null);
            }
            else if (e.KeyCode == Keys.D8)
            {
                buttonServis_Click(null, null);
            }
            else if (e.KeyCode == Keys.D9)
            {
                buttonExpedice_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Login();
                //PerformEnd();
            }
            else if (e.KeyCode == Keys.F2)
            {
                buttonKonfig_Click(null, null);
            }
            else if (e.KeyCode == Keys.F5 && Settings.Online_BYZNYS)
            {
                Online_NovyEAN();
            }
			else if (e.KeyCode == Keys.F1)
			{
#if DEBUG

				int PocetStazeni = 10;
				int cnt = 0;

				while (cnt < PocetStazeni)
				{
					stahnoutZbozi();
					cnt++;
				}

#endif

			}
			else
			{

				foreach (var item in MST_Global.assemblies)
				{
					if (item.eventHandler != null)
					{
						if (e.KeyCode.ToString() == item.keyCode)
						{
							this.BeginInvoke((MethodInvoker)delegate { item.eventHandler(null, null); });
							e.Handled = true;
							return;
						}
					}
				}

				e.Handled = false;
			}
        }
        private void buttonProdej_Click(object sender, EventArgs e)
        {
            if (!MST_Global.Prodej)
                return;

#if MOD_PRODEJ
            //if (!MST_Global.SqlCe)
            //{

            //    using (Prodej.ProdejMain prodej = new Fask.MST_W.Prodej.ProdejMain())
            //    {
            //        prodej.Owner = this;
            //        prodej.ShowDialog();
            //    }
            //}
            //else
            //{
            using (Prodej_3.ProdejMain prodej = new Fask.MST_W.Prodej_3.ProdejMain())
            {
                prodej.Owner = this;
                prodej.ShowDialog();
            }
            //}
#else
            MessageBoxBig.Show("Modul není souèástí této kompilace", this.Text);
#endif
            UpdateStatusBar();
            this.Show();
        }

        private void buttonExpedice_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MST_Global.Expedice)
                    return;

                using (Expedice.ExpediceMain expedice = new Fask.MST_W.Expedice.ExpediceMain())
                {
                    expedice.Owner = this;
                    expedice.ShowDialog();
                }

                // 22.6.2016 PeV: zakomentovano, pouziva se dialog pro vyber mezi balenim a expedici ...
                //// nacteni konfigurace aplikace
                //Expedice.Globals.Load(Main.ConfigModulesFileName);

                //Fask.MST_W.ExpediceService.ExpediceHlavicky.CZMST_Expedice_HlavickaRow hlavicka = null;

                //// nacteni skladu
                //SqlCEDBs.DataSets.Sklady.CZMST093Row sklad = null;
                //if (sklad == null && !string.IsNullOrEmpty(Expedice.Globals.SkladID))
                //{
                //    try
                //    {
                //        Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter ta_sklad = new Fask.SQLiteDBs.DataSets.SkladyTableAdapters.CZMST093TableAdapter();
                //        ta_sklad.Connection.ConnectionString = "Data source=" + Main.CiselnikSkladyDB;
                //        Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = ta_sklad.GetDataBySkl_id(Expedice.Globals.SkladID);
                //        if (dt_sklady.Count > 0)
                //            sklad = dt_sklady[0];
                //        else
                //        {
                //            MessageBoxBigTimeout.Show(string.Format(Fask.Localization.Localization.Prijem4PrijemMainSkladNenalezenVyberZeSeznamu, Expedice.Globals.SkladID), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Logging.Log.Write(ex);
                //        MessageBoxBigTimeout.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                //        return;
                //    }
                //}

                //if (sklad == null)
                //{
                //    using (Forms.FormSkladVyber fsv = new FormSkladVyber())
                //    {
                //        if (fsv.ShowDialog() == DialogResult.Cancel)
                //            return;

                //        sklad = fsv.Sklad;

                //        if (sklad == null)
                //        {
                //            Logging.Log.Write("Není vybrán sklad, pøestože je vyžadován!");
                //            return;
                //        }
                //    }
                //}

                //// TODO: prepsat ?? ...
                //while (true)
                //{
                //    hlavicka = null;
                //    using (Expedice.ExpediceVyberHlavickyList expedice = new Fask.MST_W.Expedice.ExpediceVyberHlavickyList(sklad))
                //    {
                //        expedice.Owner = this;
                //        DialogResult dr = expedice.ShowDialog();
                //        if (dr != DialogResult.OK)
                //            return;

                //        hlavicka = expedice._Hlavicka;
                //    }

                //    using (Expedice.ExpedicePolozkyList expedice = new Fask.MST_W.Expedice.ExpedicePolozkyList(hlavicka, sklad))
                //    {
                //        expedice.Owner = this;
                //        expedice.ShowDialog();
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "FormMain, Expedice");
                MessageBoxBig.Show(ex.Message, buttonExpedice.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
            finally
            {
                UpdateStatusBar();
                this.Show();
            }
        }

        private void buttonPrijem_Click(object sender, EventArgs e)
        {
            if (!MST_Global.Prijem)
                return;

#if MOD_PRIJEM
            ////if (!MST_Global.SqlCe)
            ////{

            ////    using (Prijem.PrijemMain prijem = new Fask.MST_W.Prijem.PrijemMain())
            ////    {
            ////        prijem.Owner = this;
            ////        prijem.ShowDialog();
            ////    }
            ////}
            ////else
            ////{
            //    if (MST_Global.PrijemModel == MST_Global.MODEL_CODEBOOK)
            //    {
            //        using (Prijem_3.PrijemMain prijem = new Fask.MST_W.Prijem_3.PrijemMain())
            //        {
            //            prijem.Owner = this;
            //            prijem.ShowDialog();
            //        }
            //    }
            //    else if (MST_Global.PrijemModel == MST_Global.MODEL_MEMORY) 
            //    {
            //        using (Prijem_4.PrijemMain prijem = new Fask.MST_W.Prijem_4.PrijemMain())
            //        {
            //            prijem.Owner = this;
            //            prijem.ShowDialog();
            //        }
            //    }
            ////}

            // Prijem_3 zrusen, nepouziva se a rozsiruje se jen model Prijem_4...

            using (Prijem_4.PrijemMain prijem = new Fask.MST_W.Prijem_4.PrijemMain())
            {
                prijem.Owner = this;
                prijem.ShowDialog();
            }

#else
            MessageBoxBig.Show("Modul není souèástí této kompilace", this.Text);
#endif

            UpdateStatusBar();
            this.Show();
        }
        private void imageButton1_Click(object sender, EventArgs e)
        {

        }
        private void buttonKonfig_Click(object sender, EventArgs e)
        {

            using (FormAdminAccess frmAccess = new FormAdminAccess(FormAdminAccess.AccessType.Admin))
            {
                frmAccess.Location = MySystem.FormMidLocation.GetFormLocation(frmAccess.Size);
                if (frmAccess.ShowDialog() != DialogResult.OK)
                {
                    //DialogResult = DialogResult.Cancel;
                    return;
                }
            }

            using (Config.formConfig config = new Fask.MST_W.Config.formConfig())
            {
                config.Owner = this;

                DialogResult ds = config.ShowDialog();

                switch (ds)
                {
                    //case DialogResult.Abort:
                    //    break;
                    //case DialogResult.Cancel:
                    //    break;
                    //case DialogResult.Ignore:
                    //    break;
                    //case DialogResult.No:
                    //    break;
                    //case DialogResult.None:
                    //    break;
                    case DialogResult.OK:
                        MessageBoxBig.Show("Je nutné znovu spustit aplikaci, aby se provedené zmìny projevily", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        this.Close();
                        break;
                    //case DialogResult.Retry:
                    //    break;
                    case DialogResult.Yes:
                        MessageBoxBig.Show("Je nutné vypnout aplikaci, aby se provedl update aplikace", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                        this.Close();
                        break;
                    default:
                        UpdateStatusBar();
                        this.Show();
                        break;
                }


            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.buttonLogin_Click(null, e);
        }
        private void menuItem3_Click_1(object sender, EventArgs e)
        {
            this.buttonKonfig_Click(null, e);
        }
        private void menuItem4_Click(object sender, EventArgs e)
        {
            this.buttonKonec_Click(null, e);
        }

        private void buttonPrijem_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonPrijem_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }
        private void buttonProdej_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonProdej_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }
        private void buttonVydej_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonVydej_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }
        private void buttonInventura1csv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonInventura1csv_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }
        private void buttonInventura2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonInventura2_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }
        //private void buttonHuskyVratka_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        this.buttonHuskyVratka_Click(null, null);
        //    }
        //    else
        //    {
        //        return;
        //    }
        //    e.Handled = true;
        //}
        //private void panelPrijmy_Resize(object sender, EventArgs e)
        //{
        //    int countmodules = 0;
        //    countmodules += (MST_Global.Prijem ? 1 : 0);
        //    countmodules += (MST_Global.Prevod ? 1 : 0);
        //    if (countmodules == 0) return;
        //    //Nastaveni tlacitek pro prijmy
        //    Size newsize = new Size(panelPrijmy.Width / countmodules, panelPrijmy.Height);
        //    buttonPrijem.Size = newsize;
        //    button_prijem_2.Size = newsize;
        //    button_polohovani.Size = newsize;
        //}
        //private void panelVydeje_Resize(object sender, EventArgs e)
        //{
        //    int countmodules = 0;
        //    countmodules += (MST_Global.Vydej ? 1 : 0);
        //    countmodules += (MST_Global.VydejVS ? 1 : 0);
        //    countmodules += (MST_Global.Zavoz ? 1 : 0);
        //    if (countmodules == 0) return;
        //    //nastaveni tlacitek pro vydeje
        //    Size newsize = new Size(panelVydeje.Width / countmodules, panelVydeje.Height);
        //    buttonVydej.Size = newsize;
        //    button_Vydej_VS.Size = newsize;
        //    button_Zavoz.Size = newsize;
        //}
        //private void panel_moduly_Resize(object sender, EventArgs e)
        //{
        //    Size nsize = new Size(panel_moduly.Width, panel_moduly.Height / 4);
        //    panelPrijmy.Size = nsize;
        //    panelVydeje.Size = nsize;
        //    panelProdeje.Size = nsize;
        //    panelInventury.Size = nsize;
        //}
        private void Main_DoubleClick(object sender, EventArgs e)
        {
            this.Location = (new Point(0, 0));
        }
        #endregion

        private void Main_Resize(object sender, EventArgs e)
        {
            //int countmodules = 0;
            //countmodules += (MST_Global.Vydej ? 1 : 0);
            //countmodules += (MST_Global.VydejVS ? 1 : 0);
            //countmodules += (MST_Global.Zavoz ? 1 : 0);
            //countmodules += (MST_Global.Prijem ? 1 : 0);
            //countmodules += (MST_Global.Prevod ? 1 : 0);
            //countmodules += (MST_Global.Inventura1 ? 1 : 0);
            //countmodules += (MST_Global.Prodej ? 1 : 0);
            //if (countmodules == 0) return;
            //Size nsize = new Size(this.ClientRectangle.Width, this.ClientRectangle.Height / countmodules);
            //button_polohovani.Size = nsize;
            //button_Vydej_VS.Size = nsize;
            //button_Zavoz.Size = nsize;
            //buttonInventura1csv.Size = nsize;
            //buttonPrijem.Size = nsize;
            //buttonProdej.Size = nsize;
            //buttonVydej.Size = nsize;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            // TODO : opravit ...
            // 1) spocitat pocet "Buttonu" v panel1.Controls
            // 2) podle poctu prepocitat velikost pro buttony
            // 3) nastavit velikost buttonu ...

            int countButtons = 0;
            foreach (object i in panel1.Controls)
            {
                if (
                    ((i is Button) || (i is Graphic.GraphicButton))
                    && ((Control)i).Visible
                    )
                {
                    countButtons++;
                }
            }

            Size nsize = nsize = new Size(0, 0);
            if (countButtons > 0) // kvuli moznemu deleni nulou ...
                nsize = new Size(panel1.Width, panel1.Height / countButtons);


            foreach (object i in panel1.Controls)
            {
                if ((i is Button) || (i is Graphic.GraphicButton))
                    ((Control)i).Size = nsize;
            }

            //// toto neee
            //Size nsize = ButtonsSize();
            //if (nsize == new Size(0,0))
            //    return;

            //buttonInventura1csv.Size = nsize;
            //buttonInventura2.Size = nsize;
            //buttonPrijem.Size = nsize;
            //buttonProdej.Size = nsize;
            //buttonVydej.Size = nsize;
            //buttonEvents.Size = nsize;
            //buttonTasks.Size = nsize;
            //buttonServis.Size = nsize;

        }

        private Size ButtonsSize()
        {
            int countmodules = 0;
            countmodules += (MST_Global.Vydej ? 1 : 0);
            countmodules += (MST_Global.Prijem ? 1 : 0);
            countmodules += (MST_Global.Inventura1 && MST_Global.Inventura1ShowInMST ? 1 : 0);
            countmodules += (MST_Global.Inventura2 && MST_Global.Inventura2ShowInMST ? 1 : 0);
            countmodules += (MST_Global.Prodej ? 1 : 0);
            countmodules += (MST_Global.EventsEnable && MST_Global.EventsShowInMST ? 1 : 0);
            countmodules += (MST_Global.TasksEnable && MST_Global.TasksShowInMST ? 1 : 0);
            countmodules += (MST_Global.ServisEnable && MST_Global.ServisShowInMST ? 1 : 0);
            countmodules += (MST_Global.Expedice && MST_Global.ExpediceShowInMST ? 1 : 0);
            //countmodules += (MST_Global.HuskyVratka && MST_Global.HuskyVratkaShowInMST ? 1 : 0);

            int assCount = 0;
            foreach (var item in MST_Global.assemblies)
            {
                if (item.isButtonEnabled)
                    assCount++;
            }

            countmodules += assCount;
            if (countmodules == 0) return new Size(0, 0);

            Size nsize = new Size(this.panel1.Width, this.panel1.Height / countmodules);
            return nsize;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Rectangle rect = this.ClientRectangle;
            //Microsoft.WindowsCE.Forms.LogFont lf = new Microsoft.WindowsCE.Forms.LogFont();
            //lf.Height = 18;
            //lf.Escapement = 45 * 10;
            //lf.Orientation = lf.Escapement;
            //lf.FaceName = "Arial";
            //lf.CharSet = Microsoft.WindowsCE.Forms.LogFontCharSet.Default;
            //lf.OutPrecision = Microsoft.WindowsCE.Forms.LogFontPrecision.Default;
            //lf.ClipPrecision = Microsoft.WindowsCE.Forms.LogFontClipPrecision.Default;
            //lf.Quality = Microsoft.WindowsCE.Forms.LogFontQuality.ClearType;
            //lf.PitchAndFamily = Microsoft.WindowsCE.Forms.LogFontPitchAndFamily.Default;
            //Font f = Font.FromLogFont(lf);
            //e.Graphics.DrawString(" D E M O ", f, new SolidBrush(Color.Black), rect);
            //e.Graphics.DrawStringEclipsed(" D E M O ", f, new SolidBrush(Color.Black), rect);
        }

        private void menuItemOnlineNovyEAN_Click(object sender, EventArgs e)
        {
            Online_NovyEAN();
        }

        private void Online_NovyEAN()
        {
            Online.BYZNYS.Algorithms.InsertNewEAN();
        }



        private void buttonServis_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MST_Global.ServisEnable)
                    return;

                if (!MST_Global.ServisDavkoveZpracovani)
                {
                    // nedavkove zpracovani
                    using (ServisModule.ServisList servis = new Fask.MST_W.ServisModule.ServisList())
                    {
                        servis.Owner = this;
                        servis.ShowDialog();
                    }
                }
                else
                {
                    // davkove zpracovani
                    using (ServisModule.ServisMain servis = new Fask.MST_W.ServisModule.ServisMain())
                    {
                        servis.Owner = this;
                        servis.ShowDialog();
                    }
                }

                this.Show();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, buttonServis.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        //private void buttonHuskyVratka_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!MST_Global.HuskyVratka)
        //            return;

        //        using (HuskyVratka.HuskyVratkaMain form = new Fask.MST_W.HuskyVratka.HuskyVratkaMain())
        //        {
        //            form.Owner = this;
        //            form.ShowDialog();
        //        }

        //        //using (ServisModule.ServisMain servis = new Fask.MST_W.ServisModule.ServisMain())
        //        //{
        //        //    servis.Owner = this;
        //        //    servis.ShowDialog();
        //        //}

        //        this.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.Log.Write(ex);
        //        MessageBoxBig.Show(ex.Message, buttonServis.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //    }
        //}

        private void buttonTasks_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MST_Global.TasksEnable)
                    return;

                TasksSynchronizationStop();

                using (Fask.MST_W.Ukolovani_1.UkolovaniMain ukolovani = new Fask.MST_W.Ukolovani_1.UkolovaniMain())
                {
                    ukolovani.Owner = this;
                    ukolovani.ShowDialog();
                }

                TasksSynchronizationStart();

                UpdateStatusBar();
                this.Show();

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, buttonTasks.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        private void buttonServis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonServis_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        private void buttonTasks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonTasks_Click(null, null);
            }
            else
            {
                return;
            }
            e.Handled = true;
        }

        public void ShowAsynchMessageBoxBig(string message)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        ShowAsynchMessageBoxBig(message);
                    });
                    return;
                }

                MessageBoxBigTimeout.Show(message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information, 20);

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
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
                    MessageBoxBig.Show(message, messageTitle, MessageBoxButtons.OK, MessageBoxBigIcon.Information, false);
                }
                else
                { //pokud je prave jeden, tak umoznit ihned editaci na ok ...
                    //DialogResult drTask = MessageBoxBigTimeout.Show("Zobrazit nový úkol?\n" + newTasks[0].id + " : " + newTasks[0].nazev, messageTitle, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information, 20, false);
                    DialogResult drTask = MessageBoxBig.Show("Zobrazit nový úkol?\n" + newTasks[0].id + " : " + newTasks[0].nazev, messageTitle, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information, false);
                    if (drTask == DialogResult.Yes)
                    { // => zobrazit editaci ...
                        TasksSynchronizationStop();

						//SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter ta_ukol = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter();
						//SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter ta_ukol_uziv = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();

						//ta_ukol.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikUkolyDB);
						//ta_ukol_uziv.Connection = ta_ukol.Connection;

						//SqlCEDBs.DataSets.Ukoly.CZ_UKOL_UZIVRow uuziv = ta_ukol_uziv.GetDataByID(newTasks[0].id)[0];
						//SqlCEDBs.DataSets.Ukoly.CZ_UKOLRow u = ta_ukol.GetDataByID(uuziv.UkolID)[0];

						Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVRow uuziv = null;
						Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOLRow u = null;

						using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly ConUkoly = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly(Main.CiselnikUkolyDB))
						{
							uuziv = ConUkoly.CZ_UKOL_UZIV_GetDataByID(newTasks[0].id)[0];
							u = ConUkoly.CZ_UKOL_GetDataByID(uuziv.UkolID)[0];


							using (Ukolovani_1.UkolovaniUkolEdit uedit = new Fask.MST_W.Ukolovani_1.UkolovaniUkolEdit(u, uuziv))
							{
								DialogResult drUEdit = uedit.ShowDialog();
								if (drUEdit == DialogResult.OK)
									ConUkoly.CZ_UKOL_UZIV_Update(uedit._ukol_uziv); //tady se zmenil stav...

							}
						}

                        TasksSynchronizationStart();
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
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

                string messageTitle = "Pøipomenutí úkolù = " + notificationTasks.Count;

                if (notificationTasks.Count != 1)
                {
                    string message = string.Empty;
                    foreach (var item in notificationTasks)
                    {
                        if (message.Length > 0)
                            message += "\n";
                        message += "" + item.id + " : " + item.nazev.Trim();
                    }

                    DialogResult dRes = MessageBoxBigTimeout.Show(message, messageTitle, MessageBoxButtons.OK, MessageBoxBigIcon.Information, MST_Global.TasksNotifyDialogShowSeconds, false);
                }
                else
                { //pokud je prave jeden, tak umoznit ihned editaci na ok ...
                    DialogResult drTask = MessageBoxBigTimeout.Show("Zobrazit úkol?\n" + notificationTasks[0].id + " : " + notificationTasks[0].nazev, messageTitle, MessageBoxButtons.YesNo, MessageBoxBigIcon.Information, MST_Global.TasksNotifyDialogShowSeconds, false);
                    if (drTask == DialogResult.Yes)
                    { // => zobrazit editaci ...
                        TasksSynchronizationStop();

						//SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter ta_ukol = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOLTableAdapter();
						//SqlCEDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter ta_ukol_uziv = new Fask.SQLiteDBs.DataSets.UkolyTableAdapters.CZ_UKOL_UZIVTableAdapter();

						//ta_ukol.Connection = new System.Data.SQLite.SQLiteConnection("Data source=" + Main.CiselnikUkolyDB);
						//ta_ukol_uziv.Connection = ta_ukol.Connection;

						//SqlCEDBs.DataSets.Ukoly.CZ_UKOL_UZIVRow uuziv = ta_ukol_uziv.GetDataByID(notificationTasks[0].id)[0];
						//SqlCEDBs.DataSets.Ukoly.CZ_UKOLRow u = ta_ukol.GetDataByID(uuziv.UkolID)[0];

						Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOL_UZIVRow uuziv = null;
						Fask.SQLiteDBs.DataSets.Ukoly.CZ_UKOLRow u = null;

						using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly ConUkoly = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Ukoly(Main.CiselnikUkolyDB))
						{
							uuziv = ConUkoly.CZ_UKOL_UZIV_GetDataByID(notificationTasks[0].id)[0];
							u = ConUkoly.CZ_UKOL_GetDataByID(uuziv.UkolID)[0];


							using (Ukolovani_1.UkolovaniUkolEdit uedit = new Fask.MST_W.Ukolovani_1.UkolovaniUkolEdit(u, uuziv))
							{
								DialogResult drUEdit = uedit.ShowDialog();
								if (drUEdit == DialogResult.OK)
									ConUkoly.CZ_UKOL_UZIV_Update(uedit._ukol_uziv); //tady se zmenil stav...
							}
						}

                        TasksSynchronizationStart();
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            // aktivace pri zavreni jineho formulare (napr. SejmiKodForm), pripadne loadu tohoto
            Components.KeyboardManager.LoadDefaultKeyboardMode();

			if ((Program.mstw != null) && (Program.mstw.Scanner != null))
				mi_KonScan.Checked = Program.mstw.Scanner.ContinuousRead;
        }

        private void Main_Deactivate(object sender, EventArgs e)
        {
            // deaktivace v pripade otevreni jineho formulare (napr. SejmiKodForm)
            Components.KeyboardManager.SaveDefaultKeyboardMode();
        }

        private void menuItemRFIDSnimat_Click(object sender, EventArgs e)
        {
            try
            {
                using (Forms.SnimatRFID srfid = new SnimatRFID())
                {
                    srfid.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Stahnout ciselniky



        private void buttonCiselniky_Click(object sender, EventArgs e)
        {
            try
            {
                #region Zbozi

                if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportZboziDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
                {
                    exportovatZbozi();
                }

                stahnoutZbozi();

                #endregion

                //return;

                #region Odberatele

                if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportOdberateluDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
                {
                    exportovatOdberatele();
                }

                stahnoutOdberatele();

                #endregion

                #region Sklady

                if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportSkladuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
                {
                    exportovatSklady();
                }

                stahnoutSklady();

                #endregion

                #region Meny

                if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportMenDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
                {
                    exportovatMeny();
                }


                stahnoutMeny();

                #endregion

                #region Strediska

                if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportStredisekDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
                {
                    exportovatStrediska();
                }


                stahnoutStrediska();

                #endregion

                #region Typy dokladu

                stahnoutTypDokladu();

                #endregion

                #region Lokace

                stahnoutLokace();

                #endregion

                #region Pracovnici

                if (Prodej.Globals.PovolitExportCiselniku && MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainProvestExportPracovnikuDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question) == DialogResult.Yes)
                {
                    exportovatPracovniky();
                }

                stahnoutPracovniky();

                #endregion

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                MessageBoxBig.Show(ex.Message, buttonServis.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
            }
        }

        #region Zbozi

        private void exportovatZbozi()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogZboziExport(ciselnikS, Prodej.Globals.SkladID);
        }

        private void stahnoutZbozi()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogZbozi(ciselnikS, Prodej.Globals.SkladID);
        }

        #endregion

        #region Odberatele

        private void exportovatOdberatele()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogOdberateleExport(ciselnikS);
        }

        private void stahnoutOdberatele()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogOdberatele(ciselnikS, Prodej.Globals.SkladID);
        }

        #endregion

        #region Sklady

        private void exportovatSklady()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSkladyExport(ciselnikS);
        }

        private void stahnoutSklady()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogSklady(ciselnikS);
        }

        #endregion

        #region Meny

        private void exportovatMeny()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogMenyExport(ciselnikS);
        }

        private void stahnoutMeny()
        {
            if (Prodej.Globals.FiltrCiselnikMen)
                _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogMen(ciselnikS);
            else
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainFiltryMenyNeniPovolen, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        }

        #endregion

        #region Strediska

        private void exportovatStrediska()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogStrediskaExport(ciselnikS);
        }

        private void stahnoutStrediska()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogStrediska(ciselnikS, Prodej.Globals.SkladID);
        }

        #endregion

        #region Typy dokladu

        private void stahnoutTypDokladu()
        {
            if (Prodej.Globals.TypDokladu)
                _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogTypDokladu(ciselnikS, Prodej.Globals.SkladID);
            else  // prace s typy dokladu neni povolena
                MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainPraceSTypyDokladuNeniPovolena, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        }

        #endregion

        #region Lokace

        private void stahnoutLokace()
        {
            // TODO: omezeni na urcity sklad/sklady??
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogLokace(ciselnikS, string.Empty);
        }

        #endregion

        #region Pracovnici

        private void exportovatPracovniky()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogPracovniciExport(ciselnikS);
        }

        private void stahnoutPracovniky()
        {
            _WebRefernces_Globals.CiselnikServiceOperationsForm.KatalogPracovnici(ciselnikS, Prodej.Globals.SkladID);
        }

        #endregion

		private void mi_KonScan_Click(object sender, EventArgs e)
		{
			mi_KonScan.Checked = !mi_KonScan.Checked;
			Program.mstw.Scanner.ContinuousRead = mi_KonScan.Checked ;
		}

        #endregion

        private void menuItemInfoIPAdresy_Click(object sender, EventArgs e)
        {
            try
            {
                //var hostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                //StringBuilder sbAddress = new StringBuilder();
                //hostEntry.AddressList.ToList().ForEach(x =>
                //{
                //    sbAddress.AppendLine(x.ToString());
                //});
                MessageBoxBig.Show(MST_W.MySystem.Net.Info, "IP Addresses", MessageBoxButtons.OK, MessageBoxBigIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Warning);
            }
        }



    }
}