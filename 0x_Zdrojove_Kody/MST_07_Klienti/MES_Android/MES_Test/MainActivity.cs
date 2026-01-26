using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;


using Android.Support.V4.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using System.Collections.Generic;
using System;
using System.IO;
using Android.Content;
using Android.Views;
using MES_Android.Classes;
using Android.Content.Res;

using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android.Support.Design.Widget;

using Com.Karumi.Dexter;
using Android;
using Com.Karumi.Dexter.Listener.Multi;
using Com.Karumi.Dexter.Listener;
using Com.Karumi.Dexter.Listener.Single;
using MES_Android.Listner;
using Fask.Interfaces;
using Android.Support.V4.View;
using MES_Android.ServerAccess;
using System.Threading;
using System.Threading.Tasks;
using MES_Android.Ciselniky;
using System.Globalization;
using Android.Views.InputMethods;
using MES_Android.Extensions;

namespace MES_Android
{
    //[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme" )]
    //[IntentFilter(new[] { "com.FASK.datawedge.xamarin.ACTION" }, Categories = new[] { Intent.CategoryDefault })]
    public sealed class MainActivity : Scanner_Activity, NavigationView.IOnNavigationItemSelectedListener
    {

        #region Promenne pro Activity Login

        private SupportToolbar mToolbar;

        private Classes.FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;

        private Button btnOK;
        private Button btnKonec;
        private Button btnLogins;

        private EditText edittextLogin;
        private EditText edittextPWD;

        Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable dtUzivatele;

        //Fask.Scanner.Camera_ZXing.ScannerDialog scanner_Fotak = null;
        #endregion


        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Nastavení pro Logovaní
            string rootpath = DataInfo_Static.PathDir;
            Fask.Logging.ExceptionHandler2.SetPath(rootpath);
            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Info, "Nastavena cesta :'" + rootpath + "'");
            Fask.Logging.ExceptionHandler2.EnablePrintLogging = false;
            Fask.Logging.ExceptionHandler2.SendErrorEmail = false;


            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main_Login);

            try
            {
                this.DataWedge_Scanner_Disable();

                #region Presmission

                Dexter.WithActivity(this)
                    .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                    Manifest.Permission.WriteExternalStorage,
                                    Manifest.Permission.AccessNetworkState,
                                    Manifest.Permission.Camera)
                    .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                    .WithErrorListener(new SampleErrorListner())
                    .Check();

                #endregion

                #region ToolBar + leve vysuvaci neco

                mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar_MainLogin);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout_MainLogin);

                #endregion

                #region Inicializace objektu tlačiten a edittext

                btnKonec = FindViewById<Button>(Resource.Id.btn_Login_Konec_V2);
                btnOK = FindViewById<Button>(Resource.Id.btn_Login_OK_V2);
                btnLogins = FindViewById<Button>(Resource.Id.btn_Login_Logins_V2);

                edittextLogin = FindViewById<EditText>(Resource.Id.edittext_Login_ID_V2);
                edittextPWD = FindViewById<EditText>(Resource.Id.edittext_Login_PWD_V2);

                #endregion

                #region Priradeni eventu


                edittextPWD.EditorAction += HandleEditorAction;

                edittextLogin.FocusChange += Edittext_FocusChange;
                edittextPWD.FocusChange += Edittext_FocusChange;

                edittextLogin.ShowSoftInputOnFocus = false;
                edittextPWD.ShowSoftInputOnFocus = false;

                btnKonec.Click += btnKonec_Click;
                btnOK.Click += btnOK_Click;
                btnLogins.Click += btnLogins_Click;

                btnKonec.FocusChange += Btn_FocusChange;
                btnOK.FocusChange += Btn_FocusChange;
                btnLogins.FocusChange += Btn_FocusChange;


                #endregion

                #region Nastaveni edittext focusy

                edittextLogin.SetBackgroundResource(Resource.Drawable.lost_focus_style);
                edittextPWD.SetBackgroundResource(Resource.Drawable.lost_focus_style);

                btnKonec.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btnOK.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btnLogins.SetBackgroundResource(Resource.Drawable.lost_focus_button);

                edittextLogin.RequestFocus();
                edittextPWD.Selected = false;

                btnKonec.Selected = false;
                btnOK.Selected = false;
                btnLogins.Selected = false;

                #endregion

                #region ToolBar

                SetSupportActionBar(mToolbar);

                mDrawerToggle = new Classes.FASK_ActionBarDrawerToggle(
                    this,
                    mDrawerLayout,
                    mToolbar,
                    Resource.String.openDrawer,
                    Resource.String.closeDrawer
                    );

                //mDrawerLayout.SetDrawerListener(mDrawerToggle); // drive bzval set a ted je add
                mDrawerLayout.AddDrawerListener(mDrawerToggle);

                mDrawerToggle.SyncState();

                NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_MainLogin);
                navigationView.SetNavigationItemSelectedListener(this);


                int cnt = navigationView.HeaderCount;

                if (cnt == 1)
                {
                    var v = navigationView.GetHeaderView(0);
                    var txt = v.FindViewById<TextView>(Resource.Id.txt_Head_MainLogin);
                    txt.Text = this.Resources.GetString(Resource.String.MES_Welcome);

                    var txt_ver = v.FindViewById<TextView>(Resource.Id.txt_Version_MainLogin);
                    var x = GetAppVersion();
                    txt_ver.Text = string.Format("Verze: {0} ({1})",x.Item2, x.Item1 );
                }

                if (savedInstanceState != null)
                {
                    if (savedInstanceState.GetString("DrawerState") == "Opened")
                        SupportActionBar.SetTitle(Resource.String.openDrawer);
                    else
                        SupportActionBar.SetTitle(Resource.String.closeDrawer);
                }
                else
                { SupportActionBar.SetTitle(Resource.String.closeDrawer); }

                #endregion


                

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public (double,string) GetAppVersion()
        {
            var info = PackageManager.GetPackageInfo(this.PackageName, 0);
            return (info.VersionCode, info.VersionName);
        }

        protected override void OnStart()
        {
            if (File.Exists(DataInfo_Static.CiselnikUzivateleDB))
            {
                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Users conUser = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Users(Classes.DataInfo_Static.CiselnikUzivateleDB))
                {
                    dtUzivatele = conUser.GetData();
                }
            }
            else
            {
                //Logging.Log.Write("Nenalezena databaze.", this);
            }

            base.OnStart();
        }

        #region Udalost pro stažení uživatelu z serveru

        //private void Loginservice_GetKatalogUzivateleCompleted(object sender, LoginService.GetKatalogUzivateleCompletedEventArgs e)
        //{
        //    try
        //    {
        //        Routines.DownloadDecompressDelete(Classes.DataInfo_Static.CiselnikUzivateleDB);
        //        Toast.MakeText(this, "Číselník je aktualizován", ToastLength.Long).Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        MessageBox.Show(this, ex.Message, "Uzivatele", MessageBoxButtons.OK, MB_YES);
        //    }
        //}

        #endregion


        #region Metody Eventy

        private void HandleEditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            e.Handled = false;
            if (e.ActionId == Android.Views.InputMethods.ImeAction.Done)
            {
                //SendMessage();
                PerformOK();
                e.Handled = true;
            }
        }

        private void Btn_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.focus_button);
            }
            else
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.lost_focus_button);
            }
        }

        private void Edittext_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            
            if (e.HasFocus)
            {
                ((EditText)sender).HideKeyboard();
                ((EditText)sender).SetBackgroundResource(Resource.Drawable.focus_border_style);
            }
            else
            {
                ((EditText)sender).SetBackgroundResource(Resource.Drawable.lost_focus_style);
                                
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void btnKonec_Click(object sender, EventArgs e)
        {
            PerformKonec();
        }

        private void btnLogins_Click(object sender, EventArgs e)
        {
            Perform_LoginsList();
        }

        private void Dialog_mOnLoginsComplete(object sender, Classes.Login_Dialog_ID.OnLoginsEventArgs e)
        {
            edittextLogin.Text = String.Empty;
            edittextLogin.Text = e.Login;
            edittextPWD.Text = string.Empty;
        }

        #endregion

        #region Metoda Click na bocny panel

        public bool OnNavigationItemSelected(IMenuItem item)
        {

            int id = item.ItemId;

            if (id == Resource.Id.nav_MainLogin_Kamera)
            {
                PerformFotak();
            }
            else if (id == Resource.Id.nav_MainLogin_Konfigurace)
            {
                PerformKonfigurace();
            }
            else if (id == Resource.Id.nav_MainLogin_Konec)
            {
                PerformKonec();
            }
            else if (id == Resource.Id.nav_MainLogin_DownUziv)
            {
                PerformDownload();
            }

            mDrawerLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }

        #endregion

        #region Perform metody

        private void PerformOK()
        {
            try
            {

                if (!File.Exists(Classes.DataInfo_Static.CiselnikUzivateleDB))
                {
                    //MessageBox.Show(this, "Chybí soubor uživatelů", "", MessageBoxButtons.OK, MB_YES);
                    ShowMessage(this, "Chybí soubor uživatelů", "Error");
                    return;
                }


                if (dtUzivatele == null || dtUzivatele.Count == 0)
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Users conUser = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Users(Classes.DataInfo_Static.CiselnikUzivateleDB))
                    {
                        dtUzivatele = conUser.GetData();
                    }
                }

                if (dtUzivatele == null)
                {
                    PerformUzivatelNenalezen(Resource.String.User_Message);
                    return;
                }

                var users = (Fask.SQLiteDBs.DataSets.Uzivatele.UsersRow[])dtUzivatele.Select($"Login='{edittextLogin.Text}' and PWD='{edittextPWD.Text}'");
                if (users.Length == 0)
                {
                    PerformUzivatelNenalezen(Resource.String.User_Message2);
                    return;
                }


                //otevřeni prvniho hlavniho okna
                Classes.Users Uzivatel = new Users();
                Uzivatel.EAN = users[0].EAN;
                Uzivatel.FIRSTNAME = users[0].FIRSTNAME;
                Uzivatel.Hash = users[0].Hash;
                Uzivatel.ID = users[0].ID;
                Uzivatel.Login = users[0].Login;
                Uzivatel.Pwd = users[0].Pwd;
                Uzivatel.SECONDNAME = users[0].SECONDNAME;


                Intent intent = new Intent(this, typeof(Activity_HlavneMenu));
                Bundle bundle = new Bundle();
                bundle.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
                intent.PutExtra(DataInfo_Static.User, bundle);
                this.StartActivity(intent);
                this.Finish();
                return;

                //Logging.Log.Write("Uzivatel nenalezen", "Uzivatele:", "OK", this);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //Logging.Log.Write(ex, this);
            }
        }

        private void PerformUzivatelNenalezen(int ID)
        {
            try
            {
                RunOnUiThread(() => {
                    //string msg = string.Format("Uživatel: {0} bud nenalezen anebo chybne heslo.", edittextLogin.Text.Trim());
                    //MessageBox.Show(this, msg, "Info", MessageBoxButton.OK, MB_YES);

                    new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetTitle(this.Resources.GetString(Resource.String.User_Title))
                        .SetMessage(this.Resources.GetString(ID))
                        .SetPositiveButton(Android.Resource.String.Ok, delegate
                        {
                            //OK
                        })
                        .Create()
                        .Show();

                });

                edittextPWD.Text = string.Empty;
                edittextLogin.Selected = true;

            }
            catch (Exception)
            {

                throw;
            }
        }


        override protected void ScannerData(ScannerEventArgs e)
        {
            PerformOK(e.Data);
        }

        private void PerformOK(string kod)
        {
            try
            {

                //for (int i = 0; i < dtUzivatele.Count; i++)
                //{
                //    if (kod.Trim() == dtUzivatele[i].EAN.ToString())
                //    {
                //        //otevřeni prvniho hlavniho okna
                //        Classes.DataInfo_Static.Uzivatel = dtUzivatele[i].ID.ToString();
                //        Intent intent = new Intent(this, typeof(Activity_HlavneMenu));
                //        this.StartActivity(intent);
                //        this.Finish();
                //        return;
                //    }
                //}

                //PerformUzivatelNenalezen(Resource.String.User_Message2);


                var users = (Fask.SQLiteDBs.DataSets.Uzivatele.UsersRow[])dtUzivatele.Select($"EAN='{kod.Trim()}'");
                if (users.Length == 0)
                {
                    PerformUzivatelNenalezen(Resource.String.User_Message2);
                    return;
                }


                //otevřeni prvniho hlavniho okna
                Classes.Users Uzivatel = new Users();

                Uzivatel.EAN = users[0].EAN;
                Uzivatel.FIRSTNAME = users[0].FIRSTNAME;
                Uzivatel.Hash = users[0].Hash;
                Uzivatel.ID = users[0].ID;
                Uzivatel.Login = users[0].Login;
                Uzivatel.Pwd = users[0].Pwd;
                Uzivatel.SECONDNAME = users[0].SECONDNAME;

                Intent intent = new Intent(this, typeof(Activity_HlavneMenu));
                Bundle bundle = new Bundle();
                bundle.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
                intent.PutExtra(DataInfo_Static.User, bundle);
                //intent.pu
                this.StartActivity(intent);
                this.Finish();
                return;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                //Logging.Log.Write(ex, this);
            }
        }

        private void PerformKonec()
        {
            try
            {
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void Perform_LoginsList()
        {
            try
            {
                if (!File.Exists(Classes.DataInfo_Static.CiselnikUzivateleDB))
                {
                    ShowMessage(this, "Chybí soubor uživatelů", "Error");
                    return;
                }
                Android.Support.V4.App.FragmentTransaction transaction = SupportFragmentManager.BeginTransaction();

                Classes.Login_Dialog_ID.Dialog_Logins dialog = new Classes.Login_Dialog_ID.Dialog_Logins();

                dtUzivatele = new Fask.SQLiteDBs.DataSets.Uzivatele.UsersDataTable();

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Users conUser = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Users(Classes.DataInfo_Static.CiselnikUzivateleDB))
                {
                    dtUzivatele.Clear();
                    dtUzivatele = conUser.GetData();
                }

                dialog.dt = dtUzivatele;


                dialog.Show(transaction, "Dialog Fragment");

                dialog.mOnLoginsComplete += Dialog_mOnLoginsComplete;
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                ShowMessage(this, "Chybí soubor uživatelů", "Error");
            }
        }

        //private void PerformFotak()
        //{
        //    try
        //    {

        //        Android.Support.V4.App.FragmentTransaction trans = SupportFragmentManager.BeginTransaction();
        //        scanner_Fotak = new Fask.Scanner.Camera_ZXing.ScannerDialog();

        //        scanner_Fotak.ScannerEvent += Scanner_Fotak_ScannerEvent;
        //        //scanner_Fotak.StatusEvent += Scanner_StatusEvent;

        //        //scanner_Fotak.StartScanner();

        //        scanner_Fotak.Show(trans, "Dialog Fragment");
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        //Fask.Logging.ExceptionHandler2.Handle(ex);
        //    }
        //}

        //private void PerformOKCamera(string kod)
        //{
        //    try
        //    {
        //        scanner_Fotak?.StopScanner();
        //        scanner_Fotak?.KillScanner();
        //        scanner_Fotak?.Dismiss();
        //        scanner_Fotak = null;

        //        PerformOK(kod.Trim());

        //        //for (int i = 0; i < dtUzivatele.Count; i++)
        //        //{
        //        //    if (kod.Trim() == dtUzivatele[i].EAN.ToString())
        //        //    {
        //        //        //otevřeni prvniho hlavniho okna
        //        //        Classes.DataInfo_Static.Uzivatel = dtUzivatele[i].ID.ToString();
        //        //        Intent intent = new Intent(this, typeof(Activity_HlavneMenu));
        //        //        this.StartActivity(intent);
        //        //        this.Finish();
        //        //        return;
        //        //    }
        //        //}

        //        //PerformUzivatelNenalezen(Resource.String.User_Message2);

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        //Logging.Log.Write(ex, this);
        //    }
        //}

        private async void PerformDownload()
        {

            try
            {

                List<string> prava = new List<string>();

                prava.Add("M_");
                prava.Add("V_");

                var tcs = new TaskCompletionSource<bool>();
                Classes.ProgressDialog_Infinity.Show(this);
                var loginservice = new _WebReferences_Globals.LoginServiceSession();
                loginservice.Timeout = Config.Settings.TimeOut;
                loginservice.Url = Config.Settings.Adresa + "LoginService.asmx";
                loginservice.UpdateWebServiceCredentials();
                Task<bool> x = Ciselniky.CiselnikServiceOperations.KatalogUzivatelu(tcs, this, loginservice, prava);

                var st = await x;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
            finally
            {
                Classes.ProgressDialog_Infinity.Dispose();
            }
        }

        private void PerformKonfigurace()
        {
            try
            {
                Intent intent_konfigurace = new Intent(this, typeof(Config.Config_Main));
                intent_konfigurace.PutExtra(DataInfo_Static.VolatelKonfigurace, DataInfo_Static.MainActivity);
                this.StartActivity(intent_konfigurace);
                this.Finish();

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }

        }

        #endregion


        #region Override metody

        /// <summary>
        /// Ked sa stlaci tlacitko spet
        /// </summary>
        public override void OnBackPressed()
        {
            PerformKonec();
        }


        /// <summary>
        /// Změna nadpisuv toolbaru
        /// </summary>
        /// <param name="outState"></param>
        protected override void OnSaveInstanceState(Bundle outState)
        {
            try
            {
                if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
                    outState.PutString("DrawerState", "Opened");
                else
                    outState.PutString("DrawerState", "Closed");

                base.OnSaveInstanceState(outState);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Uprimne, netuším už... ale asi to taky musí byt.... :D
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnPostCreate(savedInstanceState);
                mDrawerToggle.SyncState();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Uprimne, netuším už... ale asi to taky musí byt.... :D
        /// </summary>
        /// <param name="newConfig"></param>
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            try
            {
                base.OnConfigurationChanged(newConfig);
                mDrawerToggle.OnConfigurationChanged(newConfig);
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        #endregion

        #region DW event

        //protected override void OnNewIntent(Intent intent)
        //{
        //    base.OnNewIntent(intent);
        //    DisplayScanResult(intent);
        //}

        //private void DisplayScanResult(Intent scanIntent)
        //{
        //    String decodedSource = scanIntent.GetStringExtra(Resources.GetString(Resource.String.datawedge_intent_key_source));
        //    String decodedData = scanIntent.GetStringExtra(Resources.GetString(Resource.String.datawedge_intent_key_data));
        //    String decodedLabelType = scanIntent.GetStringExtra(Resources.GetString(Resource.String.datawedge_intent_key_label_type));
        //    String scan = decodedData + " [" + decodedLabelType + "]\n\n";
        //    //TextView output = FindViewById<TextView>(Resource.Id.txtOutput);
        //    //output.Text = scan + output.Text;
        //}


        #endregion

        #region Testovaci Volani

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {

            if (keyCode == Keycode.Escape)
            {
                PerformKonec();
            }


            //if (keyCode == Keycode.F1)
            //{
            //    //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            //    Intent intent = new Intent(this, typeof(Prodej.Prodej_Meny));
            //    this.StartActivityForResult(intent, 999);
            //    this.Finish();
            //}
            //else if (keyCode == Keycode.F2)
            //{
            //    //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            //    Intent intent = new Intent(this, typeof(Prodej.Prodej_Odberatel));
            //    this.StartActivityForResult(intent, 999);
            //    this.Finish();
            //}
            //else if (keyCode == Keycode.F3)
            //{
            //    //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            //    Intent intent = new Intent(this, typeof(Prodej.Prodej_Sklady));
            //    intent.PutExtra(DataInfo_Static.TypSkladu, DataInfo_Static.TypSkladu_Zdroj);
            //    this.StartActivityForResult(intent, 999);
            //    this.Finish();
            //}


#if DEBUG

            if (keyCode == Keycode.F1)
            {
                //MySystem.Audio.PlaySound(this, Path.Combine(DataInfo_Static.SoundDir, Prodej_Globals.PrijemSoundExpedice));

                TestVolaniAPI();

                //Konfigurace_Singleton.Instance.

                //JSON = Classes.JSON_Class.Serialize_JSON(Konfigurace_Singleton.Instance);

            }
            else if (keyCode == Keycode.F2)
            {
                //MySystem.Audio.PlaySound(this, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundSklad));


                Intent intent = new Intent(this, typeof(Prodej.Prodej_PridatPolozku));
                Bundle bundle = new Bundle();

                Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dtZbo = new Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable();
                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi ConZbo = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(Classes.DataInfo_Static.CiselnikZboziDB))
                {
                    ConZbo.FillByCarKod(dtZbo, "8594165001450");
                }


                Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row row = dtZbo[0];

                bundle.PutBinder(DataInfo_Static.object_Row095, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row>(row));
                intent.PutExtra(DataInfo_Static.Row095, bundle);

                this.StartActivityForResult(intent, 666);

            }
            else if (keyCode == Keycode.F3)
            {
                //PlayZvuk();

                TestDialog(this);

            }
            else if (keyCode == Keycode.F4)
            {

                TestTisk();
            }
            else if (keyCode == Keycode.F5)
            {
                var confirmThread = new Thread(() =>
                {
                    var dr = ShowReport();
                });

                confirmThread.Start();

            }
            else if (keyCode == Keycode.F6)
            {
                //myProcess();
                //ShowWaitDialog();
                //Ciselniky_Helper _Helper = new Ciselniky_Helper();
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Zbozi, this, Prodej_Globals.SkladID);
                //scanner_Zebra?.StopScanner();
                InputTest();
                
            }

#endif


            return base.OnKeyDown(keyCode, e);
        }

        private async void TestDialog( AppCompatActivity parent)
        {
            //Async_Metody.TEST_Asynchronne_Okno okno = new Async_Metody.TEST_Asynchronne_Okno();
            //var xxx = await okno.DisplayMessage(this, "Ahoj", "Jak se máš??");

            var xxx =  await metoda_v_metode_0(parent);

            //await MessageBoxAsync.ShowAsync(parent, "aaaaaa", "bbb", MessageBoxButtons.OK);

        }


        private async Task<bool> metoda_v_metode_0(AppCompatActivity parent)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            ProgressDialog_Infinity.Show(this);
            ProgressDialog_Infinity.Message = "Testík...";
            await Task.Delay(5000);

            var xxx = await metoda_v_metode(parent);

            tcs.SetResult(xxx);

            ProgressDialog_Infinity.Dispose();
            return tcs.Task.Result;
        }

        private async Task<bool> metoda_v_metode(AppCompatActivity parent) 
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            

            ProgressDialog_Infinity.Message = "A";
            await Task.Delay(5000);
            ProgressDialog_Infinity.Message = "B";

            var dr = await MessageBoxAsync.Show(parent, "aaaaaa", "bbb", MessageBoxButtons.YesNoCancel);
            //Async_Metody.TEST_Asynchronne_Okno okno = new Async_Metody.TEST_Asynchronne_Okno();
            //var xxx = await okno.DisplayMessage(parent, "Ahoj", "Jak se máš??");

            tcs.SetResult(true);


            return tcs.Task.Result;
        }


        private async void TestTisk()
        {
            var di = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable();

            di.AddCZMST_DIRow(
                1,
                "-",
                "-",
                "-",
                "-",
                "-",
                10,
                5,
                "-",
                0,
                0,
                0,
                0,
                "-", "-", "-",
                "-",
                0,
                "-",
                "-",
                0,
                Guid.NewGuid(),
                "-",
                "-",
                "-",
                "-",
                0,
                "-",
                 0,
                99,
                "-",
                "-",
                "-",
                "-",
                "-",
                "-",
                0, 
                0, 
                "-", 
                "-", 
                "-", 
                0, 
                false, 
                DateTime.Now,
                "-"
                );

            var vytisteno = await Prodej.ProdejTisk.PrintAsync(this,
                    row095: null,
                    rowDI: di[0],
                    data: null,
                    printerModule: Fask.PrinterFactory.PrinterModules.ProdejNasnimane,
                    pocetVytisku: string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Prodej.PredvyplneneMnozstviEtikety) ? (int?)null : System.Convert.ToInt32(Konfigurace_Singleton.Instance.Prodej.PredvyplneneMnozstviEtikety),
                    TiskSCenou: false);
        }


        private async void PlayZvuk()
        {
            await MySystem.Audio.PlaySoundAsync(this, Path.Combine(DataInfo_Static.SoundDir, Konfigurace_Singleton.Instance.Prodej.SoundSkladExpedice));
        }

        private async void InputTest()
        {
             var res =  await InputBoxAsync.Show(
                 this, 
                 Title: "Test vstup", 
                 Message: "aaa",
                 Defaultvalue: "xxxx",
                 buttons: MessageBoxButtons.OKCancel,
                 keyboardMode: Android.Text.InputTypes.ClassText );

            string TextRes = string.Empty;
            string TextVal = string.Empty;

            if (res.Dialog_Result == DialogResult.OK)
            {
                TextRes = "OK";
                TextVal = res.Value;
            }

            else if (res.Dialog_Result == DialogResult.Cancel)
            {
                TextRes = "Cancel";
                TextVal = "nic";
            }

            var result = await AsynDialogWait.AlertAsync(this, TextRes, TextVal, "Yes", "No", "Cosik");

            //scanner_Zebra?.StartScanner();
        }


        private async void TestVolaniAPI()
        {
   
            var T = SkutecneVolaniAPI();

            string res = await T;

            string neco = res;
        }


        private Task<string> SkutecneVolaniAPI()
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            Task.Run(() => { NejhlubsiUrovenVolani(tcs); });

            return tcs.Task;
        }

        private void NejhlubsiUrovenVolani(TaskCompletionSource<string> tcs)
        {

            RestSharp.IRestResponse response1;

            API_Server.API_Komunikator.Communicate(API_Server.API_Komunikator.REST_Type.GET,out response1, "Login");
            //var response = Classes.DataInfo_Static.API_GO_Instance.REST_GET("Login");

            string JSON = response1.Content;
            tcs.SetResult(JSON);
        }


        private async void ShowWaitDialog()
        {
            try
            {
                Ciselniky_Helper _Helper = new Ciselniky_Helper();
                //var result = await Ciselniky_Helper.TEST_Zbozi(this, Prodej.Globals.SkladID);
                //var taskbool = Ciselniky_Helper.TEST_Zbozi(this, Prodej.Globals.SkladID);
                List<Task<bool>> listTasks = new List<Task<bool>>();
                listTasks.Add(_Helper.TEST_Zbozi(this, Konfigurace_Singleton.Instance.Prodej.SkladID));
                listTasks.Add(_Helper.TEST_Zbozi(this, Konfigurace_Singleton.Instance.Prodej.SkladID, description: "Prdlajs"));
                listTasks.Add(_Helper.TEST_Zbozi(this, Konfigurace_Singleton.Instance.Prodej.SkladID, description: "Krless"));

                //var result = Task.WaitAll(listTasks.ToArray());
                var listResults = new List<bool>(await Task.WhenAll<bool>(listTasks));
                Classes.ProgressDialog_Infinity.Dispose();

                Toast.MakeText(this, $"Result from alert = {listResults[0]}", ToastLength.Short).Show();
            }
            catch (Exception exDialog)
            {
                Toast.MakeText(this, $"Alert is cancelled: {exDialog.Message}", ToastLength.Short).Show();
            }
        }

        private async void myProcess()
        {
            try
            {
                var result = await AsynDialogWait.AlertAsync(this, "My Title", "My Message", "Yes", "No", "Cosik");
                //Snackbar.Make(this, $"Result from alert = {result}", Snackbar.LengthLong)
                //    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
                Toast.MakeText(this, $"Result from alert = {result}", ToastLength.Short).Show();
            }
            catch (Exception exDialog)
            {
                //Snackbar.Make(view, $"Alert is cancelled: {exDialog.Message}", Snackbar.LengthLong)
                //    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
                Toast.MakeText(this, $"Alert is cancelled: {exDialog.Message}", ToastLength.Short).Show();
            }
        }

        private DialogResult ShowReport()
        {
            using (Prodej.FormReport frmrep = new Prodej.FormReport())
            {
                //frmrep.PERow = PERow;
                //frmrep.ds = ds;


                frmrep.CZ_CarKod = "156185.3184";
                frmrep.ITEMDESC = "Neco";
                frmrep.ITEMNMBR = "666";


                frmrep.NaExpedici = 25;
                frmrep.NaSklad = 10;

                frmrep.MnozstviDodavatelePozadovano = 1;
                frmrep.MnozstviDodavateleDodano = 2;
                frmrep.MnozstviDodavateleDodat = 3;
                frmrep.MnozstviOdberateliPozadovano = 4;
                frmrep.MnozstviOdberatelumDodano = 5;
                frmrep.MnozstviOdberatelumDodat = 6;
                
                return frmrep.ShowDialog(this);

            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        #endregion

    }
}