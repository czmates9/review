using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Views;
using Android.Content;
using MES_Android.Classes_HlavneMenu;

using Android.Support.V4.Widget;
using System;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using MES_Android.Classes;
using MES_Android.Ciselniky;
using MES_Android._WebReferences_Globals;
using MES_Android.ServerAccess;
using Android.Util;

using System.IO;
using Com.Karumi.Dexter;
using Android;
using Com.Karumi.Dexter.Listener.Multi;
using MES_Android.Listner;
using static MES_Android.Ciselniky.CiselnikServiceOperations;

namespace MES_Android
{
    //[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    //[IntentFilter(new[] { "com.FASK.datawedge.xamarin.ACTION" }, Categories = new[] { Intent.CategoryDefault })]
    public class Activity_HlavneMenu : Base_Aktivita, NavigationView.IOnNavigationItemSelectedListener, ICiselniky_Datum
    {

        #region Menu

        private Android.Support.V7.Widget.Toolbar mToolbar;

        private Classes.FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;

        //private ListView mLeftDrawer;
        //private ArrayAdapter mLeftAdapter;
        //private List<string> mLeftList;

        #endregion

        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;

        Classes.Users Uzivatel = null;

        NavigationView mNavigationView;

        //Android.Support.V4.App.FragmentTransaction Trans_Cis;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {

                this.DataWedge_Scanner_Disable();

                base.OnCreate(savedInstanceState);

                SetContentView(Resource.Layout.HlavneMenu);

                Dexter.WithActivity(this)
                    .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                    Manifest.Permission.WriteExternalStorage,
                                    Manifest.Permission.AccessNetworkState,
                                    Manifest.Permission.Camera)
                    .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                    .WithErrorListener(new SampleErrorListner())
                    .Check();

                //DWUtilities.CreateDWProfile(this);

                #region RecycleView

                //textView_LeftMenu_User
                // Leve menu, prihlasenyUzivatel

                var bundle = Intent?.GetBundleExtra(DataInfo_Static.User);
                var binder = bundle?.GetBinder(DataInfo_Static.object_User);
                if (binder != null)
                {
                    Uzivatel = ((Classes.WrapperForBinder<Users>)binder).getData();
                }


                //enableScanner();

                mRecycleView = FindViewById<RecyclerView>(Resource.Id.HlavneMenu_recyclerView);

                ////Pro Grid zobrazeni
                //var gridLayoutManager = new GridLayoutManager(this, 1);
                //recyclerView.SetLayoutManager(gridLayoutManager);
                //recyclerView.HasFixedSize = true;

                mLayoutManager = new LinearLayoutManager(this);
                mRecycleView.SetLayoutManager(mLayoutManager);

                List<BTN_Item> bTN_Items = new List<BTN_Item>();

                BTN_Item btn1 = new BTN_Item(this.Resources.GetString(Resource.String.Prijem), Resource.Drawable.Prijem, Button_Click, true);
                bTN_Items.Add(btn1);

                BTN_Item btn2 = new BTN_Item(this.Resources.GetString(Resource.String.Vydej), Resource.Drawable.Vydej, Button_Click, false);
                bTN_Items.Add(btn2);

                BTN_Item btn3 = new BTN_Item(this.Resources.GetString(Resource.String.Prodej), Resource.Drawable.Prodej, Prodej_Davka, false);
                bTN_Items.Add(btn3);

                BTN_Item btn4 = new BTN_Item(this.Resources.GetString(Resource.String.Inventura), Resource.Drawable.Inventura, Button_Click, false);
                bTN_Items.Add(btn4);

                BTN_Item btn5 = new BTN_Item(this.Resources.GetString(Resource.String.Expedice), Resource.Drawable.Expedice, Button_Click, false);
                bTN_Items.Add(btn5);

                BTN_Item btn6 = new BTN_Item(this.Resources.GetString(Resource.String.Vyroba), Resource.Drawable.Vyroba, OdvadeniVyroby, false);
                bTN_Items.Add(btn6);

                BTN_Item btn7 = new BTN_Item(this.Resources.GetString(Resource.String.ParsKode), Resource.Drawable.barcodeScan, ParsKode_Click, false);
                bTN_Items.Add(btn7);

                var arrItems = bTN_Items.ToArray();

                var RecAdap = new RecyclerAdapter(arrItems);

                mRecycleView.SetAdapter(RecAdap);

                #endregion

                #region ToolBar + leve vysuvaci neco

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_HlavneMenu);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout_HlavneMenu);
                //mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer_HlavneMenu);

                #endregion

                #region ToolBar

                SetSupportActionBar(mToolbar);

                //mLeftList = new List<string>();

                //Tady je seznam do bocneho panelu
                //mLeftList.Add("Konfigurace");
                //mLeftList.Add("Odhlásit");

                //mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftList);
                //mLeftDrawer.Adapter = mLeftAdapter;
                //mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;

                mDrawerToggle = new Classes.FASK_ActionBarDrawerToggle(
                    this,
                    mDrawerLayout,
                    mToolbar,
                    Resource.String.openDrawer,
                    Resource.String.closeDrawer
                    );

                //mDrawerLayout.SetDrawerListener(mDrawerToggle); // drive bzval set a ted je add
                mDrawerLayout.AddDrawerListener(mDrawerToggle);
                //SupportActionBar.SetHomeButtonEnabled(true);
                //SupportActionBar.SetDisplayShowTitleEnabled(true);

                mDrawerToggle.SyncState();

                //if (savedInstanceState != null)
                //{
                //    if (savedInstanceState.GetString("DrawerState") == "Opened")
                //        SupportActionBar.SetTitle(Resource.String.openDrawer);
                //    else
                //        SupportActionBar.SetTitle(Resource.String.closeDrawer);
                //}
                //else
                //{ SupportActionBar.SetTitle(Resource.String.closeDrawer); }

                #endregion

                mNavigationView = FindViewById<NavigationView>(Resource.Id.nav_view_HlavneMenu);
                mNavigationView.SetNavigationItemSelectedListener(this);

                int cnt = mNavigationView.HeaderCount;

                if (cnt == 1)
                {
                    var v = mNavigationView.GetHeaderView(0);
                    var txt = v.FindViewById<TextView>(Resource.Id.txt_Head_HlavneMenu);
                    string msg = "Uživatel nepřihlášen!";

                    if (Uzivatel != null)
                    {
                        msg = string.Format("Jméno uživatele: {0} {1}", Uzivatel.FIRSTNAME, Uzivatel.SECONDNAME);
                    }

                    txt.Text = msg;
                }

                SetDateToMenu(Resource.String.Meny, Classes.DataInfo_Static.CiselnikMenyDB);
                SetDateToMenu(Resource.String.Zasoby, Classes.DataInfo_Static.CiselnikZboziDB);
                SetDateToMenu(Resource.String.Odberatele, Classes.DataInfo_Static.CiselnikOdberateleDB);
                SetDateToMenu(Resource.String.Strediska, Classes.DataInfo_Static.CiselnikStrediskaDB);
                SetDateToMenu(Resource.String.TypyDokladu, Classes.DataInfo_Static.CiselnikTypDokladuDB);
                SetDateToMenu(Resource.String.Sklady, Classes.DataInfo_Static.CiselnikSkladyDB);
                SetDateToMenu(Resource.String.Pracovnici, Classes.DataInfo_Static.CiselnikPracovniciDB);
                SetDateToMenu(Resource.String.Lokace, Classes.DataInfo_Static.CiselnikLokaceDB);
                

                //Trans_Cis = SupportFragmentManager.BeginTransaction();

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        public void SetDateToMenu(int ID_String, string FileName)
        {
            int cnt = mNavigationView.HeaderCount;

            if (cnt == 1)
            {
                int ID_Menu = 0;

                if (ID_String == Resource.String.Zasoby)
                    ID_Menu = Resource.Id.nav_Ciselnik_Zasoby;
                else if (ID_String == Resource.String.Meny)
                    ID_Menu = Resource.Id.nav_Ciselnik_Meny;
                else if (ID_String == Resource.String.Odberatele)
                    ID_Menu = Resource.Id.nav_Ciselnik_Odberatele;
                else if (ID_String == Resource.String.Strediska)
                    ID_Menu = Resource.Id.nav_Ciselnik_Strediska;
                else if (ID_String == Resource.String.TypyDokladu)
                    ID_Menu = Resource.Id.nav_Ciselnik_TypyDokladu;
                else if (ID_String == Resource.String.Sklady)
                    ID_Menu = Resource.Id.nav_Ciselnik_Sklady;
                else if (ID_String == Resource.String.Pracovnici)
                    ID_Menu = Resource.Id.nav_Ciselnik_Pracovnici;
                else if (ID_String == Resource.String.Lokace)
                    ID_Menu = Resource.Id.nav_Ciselnik_Lokace;

                var menu = mNavigationView.Menu;
                var v = mNavigationView.GetHeaderView(0);
                var txt = menu.FindItem(ID_Menu);
                string msg = Resources.GetString(ID_String);

                if(File.Exists(FileName))
                {
                    DateTime modification = File.GetLastWriteTime(FileName);
                    string Datum = " (" + modification.ToString("dd.MM.yy HH:mm") + ")";
                    msg += Datum;
                }
                else
                {
                    msg += " Nenalezen";
                }

                RunOnUiThread(()=> {
                    txt.SetTitle(msg);
                });

                
            }
        }

        //protected override void OnResume()
        //{
        //    try
        //    {
        //        //resumeScanner();
        //        //registerReceivers();
        //        base.OnResume();
        //        DWUtilities.CreateDWProfile(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        string s = ex.Message;
        //    }
        //}

        //protected override void OnPause()
        //{
        //    //suspendScanner();
        //    unRegisterReceivers();
        //    base.OnPause();
        //}

        //protected override void OnDestroy()
        //{
        //    try
        //    {
        //        base.OnDestroy();
        //        UnregisterReceiver(receiver);
        //    }
        //    catch (Exception ex)
        //    {
        //        string s = ex.Message;
        //    }
        //}

        #region Prace s DataWedge API

        //private void suspendScanner()
        //{
        //    Intent i = new Intent();
        //    i.SetAction("com.symbol.datawedge.api.ACTION");
        //    i.PutExtra("com.symbol.datawedge.api.SCANNER_INPUT_PLUGIN", "SUSPEND_PLUGIN");
        //    i.PutExtra("SEND_RESULT", "true");
        //    i.PutExtra("COMMAND_IDENTIFIER", "MY_SUSPEND_SCANNER");  //Unique identifier
        //    this.SendBroadcast(i);
        //}

        //private void resumeScanner()
        //{
        //    Intent i = new Intent();
        //    i.SetAction("com.symbol.datawedge.api.ACTION");
        //    i.PutExtra("com.symbol.datawedge.api.SCANNER_INPUT_PLUGIN", "RESUME_PLUGIN");
        //    i.PutExtra("SEND_RESULT", "true");
        //    i.PutExtra("COMMAND_IDENTIFIER", "MY_RESUME_SCANNER");  //Unique identifier
        //    this.SendBroadcast(i);
        //}

        //private void enableScanner()
        //{
        //    Intent i = new Intent();
        //    i.SetAction("com.symbol.datawedge.api.ACTION");
        //    i.PutExtra("com.symbol.datawedge.api.SCANNER_INPUT_PLUGIN", "ENABLE_PLUGIN");
        //    i.PutExtra("SEND_RESULT", "true");
        //    i.PutExtra("COMMAND_IDENTIFIER", "MY_ENABLE_SCANNER");  //Unique identifier
        //    this.SendBroadcast(i);
        //}

        //private void disableScanner()
        //{
        //    Intent i = new Intent();
        //    i.SetAction("com.symbol.datawedge.api.ACTION");
        //    i.PutExtra("com.symbol.datawedge.api.SCANNER_INPUT_PLUGIN", "DISABLE_PLUGIN");
        //    i.PutExtra("SEND_RESULT", "true");
        //    i.PutExtra("COMMAND_IDENTIFIER", "MY_DISABLE_SCANNER");  //Unique identifier
        //    this.SendBroadcast(i);
        //}

        //DW_API_Receiver receiver = new DW_API_Receiver();

        ////call in onResume() method
        //private void registerReceivers()
        //{
        //    try
        //    {
        //        IntentFilter filter = new IntentFilter();
        //        filter.AddAction(Resources.GetString(Resource.String.activity_intent_filter_action));
        //        filter.AddCategory(Intent.CategoryDefault);
        //        RegisterReceiver(receiver, filter);
        //    }
        //    catch (Exception ex)
        //    {
        //        string s = ex.Message;
        //    }
        //}

        #endregion

        #region onNewIntent test

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
        //    String xxx = scan;
        //    //TextView output = FindViewById<TextView>(Resource.Id.txtOutput);
        //    //output.Text = scan + output.Text;
        //}

        #endregion


        public override void OnBackPressed()
        {
            //DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout_HlavneMenu);
            if (mDrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                mDrawerLayout.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                //base.OnBackPressed();
                PerformOdhlasit();
            }

        }

        //private void Button_Click(object sender, System.EventArgs e)
        private async void Button_Click(string txt)
        {
            try
            {
                //Toast.MakeText(this, txt, ToastLength.Short).Show();
                string s = string.Format("Modul {0} není licencován!", txt);
                await MessageBoxAsync.Show(this,s, "Upozornění", MessageBoxButtons.OK);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void ParsKode_Click(string txt)
        {
            try
            {
                Intent intent = new Intent(this, typeof(Kontrola_Kodu.ParsovaniKodu_Konfigurace));
                this.StartActivity(intent);
                this.Finish();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        


        #region Metoda Click na bocny panel

        #region OLD

        // aby bylo mozno predat do eventu click musi byt   (object sender, AdapterView.ItemClickEventArgs e)
        //private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    try
        //    {

        //        switch (e.Id)
        //        {
        //            case 0:
        //                PerformKonfigurace();
        //                break;
        //            case 1:
        //                PerformOdhlasit();
        //                break;
        //            default:
        //                break;
        //        }

        //        mDrawerLayout.CloseDrawers();
        //        mDrawerToggle.SyncState();

        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
        //        //Logging.Log.Write(ex, this);
        //    }
        //}

        #endregion

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Ciselniky_Helper _Helper = new Ciselniky_Helper();
            //CiselnikServiceSession ciselnikS = new CiselnikServiceSession();
            //ciselnikS.Url = Config.Settings.Adresa + "Ciselnik.asmx";
            //ciselnikS.Timeout = Config.Settings.TimeOut;
            //ciselnikS.UpdateWebServiceCredentials();

            //string SkladFilter = Prodej.Globals.SkladID;

            int id = item.ItemId;

            if (id == Resource.Id.nav_Konfigurace)
            {
                PerformKonfigurace();
            }
            else if (id == Resource.Id.nav_Odhlasit)
            {
                PerformOdhlasit();
            }
            else if (id == Resource.Id.nav_Prijem)
            {
                Button_Click(this.Resources.GetString(Resource.String.Prijem));
            }
            else if (id == Resource.Id.nav_Vydej)
            {
                Button_Click(this.Resources.GetString(Resource.String.Vydej));
            }
            else if (id == Resource.Id.nav_Prodej)
            {
                Prodej_Davka(string.Empty);
            }
            else if (id == Resource.Id.nav_Inventura)
            {
                Button_Click(this.Resources.GetString(Resource.String.Inventura));
            }
            else if (id == Resource.Id.nav_Expedice)
            {
                Button_Click(this.Resources.GetString(Resource.String.Expedice));
            }
            else if (id == Resource.Id.nav_Vyroba)
            {
                //Button_Click(this.Resources.GetString(Resource.String.Vyroba));
                OdvadeniVyroby(string.Empty);
            }
            else if (id == Resource.Id.nav_Ciselniky)
            {
                Ciselnik(Operation.All);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.All, this,  Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Zasoby)
            {
                Ciselnik(Operation.Zbozi);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Zbozi, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Odberatele)
            {
                Ciselnik(Operation.Odberatele);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Odberatele, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Sklady)
            {
                Ciselnik(Operation.Sklady);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Sklady, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Meny)
            {
                Ciselnik(Operation.Meny);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Meny, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Strediska)
            {
                Ciselnik(Operation.Strediska);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Strediska, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_TypyDokladu)
            {
                Ciselnik(Operation.TypDokladu);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.TypDokladu, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Lokace)
            {
                Ciselnik(Operation.Lokace);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Lokace, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Pracovnici)
            {
                Ciselnik(Operation.Pracovnici);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Pracovnici, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }

            mDrawerLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }

        private void Prodej_Davka(string nic)
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typeof(Prodej.Prodej_Davky));
            Bundle bundle = new Bundle();
            bundle.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundle);
            //intent.pu
            this.StartActivity(intent);
            this.Finish();
        }

        private void OdvadeniVyroby(string nic)
        {

            if (Konfigurace_Singleton.Instance.Vyroba.VyberZakazkyPoPrihlaseni)
            {
                // nastaveni aktivniho vyrobniho prikazu ...
                StartAktivityNasledujici(typeof(OdvadeniVyroby.PrikazVyber));
            }
            else
            {
                StartAktivityNasledujici(typeof(OdvadeniVyroby.OdvadeniVyroby));
            }
        }

        private void StartAktivityNasledujici(Type typAktivity)
        {
            Intent intent = new Intent(this, typAktivity);
            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);
            this.StartActivity(intent);
            this.Finish();
        }

        private void PerformOdhlasit()
        {
            try
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
                this.Finish();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void PerformKonfigurace()
        {
            Intent intent_konfigurace = new Intent(this, typeof(Config.Config_Main));
            intent_konfigurace.PutExtra(DataInfo_Static.VolatelKonfigurace, DataInfo_Static.Activity_HlavneMenu);
            Bundle bundle = new Bundle();
            bundle.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent_konfigurace.PutExtra(DataInfo_Static.User, bundle);
            this.StartActivity(intent_konfigurace);
            this.Finish();
        }

        #endregion

        #region Metory pro Permision

        internal void ShowRequestPermissionRationale(IPermissionToken token)
        {


            new Android.Support.V7.App.AlertDialog.Builder(this)
                .SetTitle(this.Resources.GetString(Resource.String.Title))
                .SetMessage(this.Resources.GetString(Resource.String.Message))
                .SetNegativeButton(Android.Resource.String.Cancel, delegate
                {
                    token.ContinuePermissionRequest();
                })
                .SetPositiveButton(Android.Resource.String.Ok, delegate
                {
                    token.ContinuePermissionRequest();
                })
                .SetOnDismissListener(new MyDismissListner(token))
                .Show();
        }

        #endregion


        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Escape)
            {
                PerformOdhlasit();
            }

            return base.OnKeyDown(keyCode, e);
        }

        private async void Ciselnik(Operation typ)
        {
            try
            {

                Ciselniky_Helper _Helper = new Ciselniky_Helper();
                bool state = await _Helper.SynchronizeCiselniky(typ, this, Konfigurace_Singleton.Instance.Prodej.SkladID);

                if (!state)
                {
                    await MessageBoxAsync.Show(this, "NEco je špatně...", "error", MessageBoxButtons.OK);
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

    }

    //public class DW_API_Receiver : BroadcastReceiver
    //{
    //    public override void OnReceive(Context context, Intent intent)
    //    {

    //        //Get result of the suspend/resume API call
    //        try
    //        {
    //            String action = intent.GetStringExtra("com.symbol.datawedge.api.ACTION");
    //            if (action != null && action.Equals("com.symbol.datawedge.api.RESULT_ACTION"))
    //            {
    //                Bundle extras = intent.GetBundleExtra(action);
    //                if (extras != null)
    //                {
    //                    //user specified ID
    //                    String cmdID = extras.GetString("COMMAND_IDENTIFIER");
    //                    if ("MY_RESUME_SCANNER".Equals(cmdID) || "MY_SUSPEND_SCANNER".Equals(cmdID))
    //                    {
    //                        //success or failure
    //                        String result = extras.GetString("RESULT");
    //                        //Original command
    //                        String command = extras.GetString("COMMAND");
    //                        if ("FAILURE".Equals(result))
    //                        {
    //                            Bundle info = extras.GetBundle("RESULT_INFO");
    //                            String errorCode = "";

    //                            if (info != null)
    //                            {
    //                                errorCode = info.GetString("RESULT_CODE");
    //                            }
    //                            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Debug, " Command:" + command + ":" + cmdID + ":" + result + ",Code:" + errorCode);
    //                        }
    //                        else
    //                        {
    //                            Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Debug, " Command:" + command + ":" + cmdID + ":" + result);
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            string s = ex.Message;
    //        }

    //    }
    //}

}