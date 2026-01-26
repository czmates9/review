using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Views;
using Android.Content;

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
using System.Collections.ObjectModel;
using Android.Support.V7.Widget.Helper;
using System.Text;
using MES_Android.SQLiteDBs_Classes;
using Com.Karumi.Dexter;
using Android;
using Com.Karumi.Dexter.Listener.Multi;
using MES_Android.Listner;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using static MES_Android.Ciselniky.CiselnikServiceOperations;

namespace MES_Android.Prodej
{
    //[Activity(Label = "@string/Prodej_Davky_Label", Theme = "@style/AppTheme", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    [Activity(Label = "@string/Prodej_Davky_Label", Theme = "@style/AppTheme")]
    //[IntentFilter(new[] { "com.FASK.datawedge.xamarin.ACTION" }, Categories = new[] { Intent.CategoryDefault })]
    //public class Prodej_Davky : Base_Aktivita, NavigationView.IOnNavigationItemSelectedListener//, IOnStartDragListener
    public class Prodej_Davky : Base_Aktivita, NavigationView.IOnNavigationItemSelectedListener, ICiselniky_Datum
    {


        #region Parametry

        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;

        Users Uzivatel = null;

        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;

        //FloatingActionButton fab;
        FloatingActionButton fab;

        Prodej_Davka_Item_Adapter mAdapter;

        Android.Support.V7.Widget.SearchView _searchView;
        //private ItemTouchHelper mItemTouchHelper;

        NavigationView mNavigationView;

        //Android.Support.V4.App.FragmentTransaction Trans_Cis;

        private Prodej_Davka_Item _Item;

        private string PathToDavka
        {
            get
            {
                if (_Item != null)
                {
                    if (_Item.CisloDavky.HasValue)
                        return Path.Combine(DataInfo_Static.PathDir, _Item.CisloDavky.Value.ToString() + DataInfo_Static.PriponaDI);
                    else
                        return null;
                }
                else
                    return null;
            }
        }

        #endregion

        #region OnCreate

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                this.DataWedge_Scanner_Disable();

                SetContentView(Resource.Layout.Prodej_Davky);

                Dexter.WithActivity(this)
                    .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                    Manifest.Permission.WriteExternalStorage,
                                    Manifest.Permission.AccessNetworkState,
                                    Manifest.Permission.Camera)
                    .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                    .WithErrorListener(new SampleErrorListner())
                    .Check();

             

                var bundle = Intent?.GetBundleExtra(DataInfo_Static.User);
                var binder = bundle?.GetBinder(DataInfo_Static.object_User);
                if (binder != null)
                {
                    Uzivatel = ((WrapperForBinder<Users>)binder).getData();
                }

                fab = FindViewById<FloatingActionButton>(Resource.Id.Prodej_Davka_Nova);

                if (fab != null)
                    fab.Click += FabOnClick;

                mRecycleView = FindViewById<RecyclerView>(Resource.Id.Prodej_Davky_recyclerView);
                mLayoutManager = new LinearLayoutManager(this);
                mRecycleView.SetLayoutManager(mLayoutManager);

                string[] fileNames = Directory.GetFiles(DataInfo_Static.PathDir, DataInfo_Static.Prodej_SearchPatern_AllFilesDavek);
                var seznam = GetExistujiciDavky(fileNames);

                mAdapter = new Prodej_Davka_Item_Adapter(this, seznam);
                mAdapter.ItemClick += MAdapter_ItemClick;
                mAdapter.ItemLongClick += MAdapter_ItemLongClick;
                mRecycleView.SetAdapter(mAdapter);

  
                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Prodej_Davky_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Prodej_Davky_drawer_layout);
                SetSupportActionBar(mToolbar);
                mDrawerToggle = new FASK_ActionBarDrawerToggle(
                            this,
                            mDrawerLayout,
                            mToolbar,
                            Resource.String.openDrawer,
                            Resource.String.closeDrawer
                            );

                mDrawerLayout.AddDrawerListener(mDrawerToggle);
                mDrawerToggle.SyncState();


                mNavigationView = FindViewById<NavigationView>(Resource.Id.Prodej_Davky_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);

                //ItemTouchHelper.Callback callback = new SimpleItemTouchHelperCallback(mAdapter);
                //mItemTouchHelper = new ItemTouchHelper(callback);
                //mItemTouchHelper.AttachToRecyclerView(mRecycleView);

                SetDateToMenu(Resource.String.Meny, DataInfo_Static.CiselnikMenyDB);
                SetDateToMenu(Resource.String.Zasoby, DataInfo_Static.CiselnikZboziDB);
                SetDateToMenu(Resource.String.Odberatele, DataInfo_Static.CiselnikOdberateleDB);
                SetDateToMenu(Resource.String.Strediska, DataInfo_Static.CiselnikStrediskaDB);
                SetDateToMenu(Resource.String.TypyDokladu, DataInfo_Static.CiselnikTypDokladuDB);
                SetDateToMenu(Resource.String.Sklady, DataInfo_Static.CiselnikSkladyDB);
                SetDateToMenu(Resource.String.Pracovnici, DataInfo_Static.CiselnikPracovniciDB);
                SetDateToMenu(Resource.String.Lokace, DataInfo_Static.CiselnikLokaceDB);

                //Trans_Cis = SupportFragmentManager.BeginTransaction();

                // dotaz na aktualizaci ciselniku zbozi
                if (Konfigurace_Singleton.Instance.Prodej.AktualizaceZboziPredVyberemDavky)
                {
                    CiselniZbozi_Dotaz();

                    //Ciselniky.Ciselniky_Helper _Helper = new Ciselniky_Helper();

                    //if (MessageBox.Show(this, Resources.GetString(Resource.String.Prodej3ProdejMainAktualizaceCiselnikuZboziDotaz), "Otázka", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //{
                    //    Ciselnik(CiselnikServiceOperations.Operation.Zbozi);
                    //}
                        //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Zbozi, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
                }

                
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowErrorMessage(this, ex);
            }
        }

        #endregion

        #region OnKeyDown


        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            

            if (keyCode == Keycode.F1)
            {
                FabOnClick(null, null);
            }
            else if (keyCode == Keycode.Escape)
            {
                HlavneMenu();
            }

            return base.OnKeyDown(keyCode, e);
        }

        #endregion

        #region Metoda pro nastaveni datumu aktualizace do bočního menu

        public void SetDateToMenu( int ID_String, string FileName)
        {
            int cnt = mNavigationView.HeaderCount;

            if (cnt == 1)
            {
                int ID_Menu = 0;

                if (ID_String == Resource.String.Zasoby)
                    ID_Menu = Resource.Id.nav_Ciselnik_Zasoby_Pro;
                else if (ID_String == Resource.String.Meny)
                    ID_Menu = Resource.Id.nav_Ciselnik_Meny_Pro;
                else if (ID_String == Resource.String.Odberatele)
                    ID_Menu = Resource.Id.nav_Ciselnik_Odberatele_Pro;
                else if (ID_String == Resource.String.Strediska)
                    ID_Menu = Resource.Id.nav_Ciselnik_Strediska_Pro;
                else if (ID_String == Resource.String.TypyDokladu)
                    ID_Menu = Resource.Id.nav_Ciselnik_TypyDokladu_Pro;
                else if (ID_String == Resource.String.Sklady)
                    ID_Menu = Resource.Id.nav_Ciselnik_Sklady_Pro;
                else if (ID_String == Resource.String.Pracovnici)
                    ID_Menu = Resource.Id.nav_Ciselnik_Pracovnici_Pro;
                else if (ID_String == Resource.String.Lokace)
                    ID_Menu = Resource.Id.nav_Ciselnik_Lokace_Pro;

                var menu = mNavigationView.Menu;
                var v = mNavigationView.GetHeaderView(0);
                var txt = menu.FindItem(ID_Menu);
                string msg = Resources.GetString(ID_String);

                if (File.Exists(FileName))
                {
                    DateTime modification = File.GetLastWriteTime(FileName);
                    string Datum = " (" + modification.ToString("dd.MM.yy HH:mm") + ")";
                    msg += Datum;
                }
                else
                {
                    msg += " Nenalezen";
                }

                RunOnUiThread(() => {
                    txt.SetTitle(msg);
                });
            }
        }

        #endregion

        #region OnBackPress

        public override void OnBackPressed()
        {
            if (mDrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                mDrawerLayout.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                //base.OnBackPressed();
                HlavneMenu();
            }

        }

        #endregion

        #region Bočne menu

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Ciselniky_Helper _Helper = new Ciselniky_Helper();

            int id = item.ItemId;

            if (id == Resource.Id.nav_HlMenu_Pro)
            {
                HlavneMenu();
            }
            else if (id == Resource.Id.nav_Ciselniky_Pro)
            {
                Ciselnik(Operation.All);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.All, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Zasoby_Pro)
            {
                Ciselnik(Operation.Zbozi);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Zbozi, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Odberatele_Pro)
            {
                Ciselnik(Operation.Odberatele);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Odberatele, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Sklady_Pro)
            {
                Ciselnik(Operation.Sklady);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Sklady, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Meny_Pro)
            {
                Ciselnik(Operation.Meny);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Meny, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Strediska_Pro)
            {
                Ciselnik(Operation.Strediska);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Strediska, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_TypyDokladu_Pro)
            {
                Ciselnik(Operation.TypDokladu);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.TypDokladu, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Lokace_Pro)
            {
                Ciselnik(Operation.Lokace);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Lokace, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }
            else if (id == Resource.Id.nav_Ciselnik_Pracovnici_Pro)
            {
                Ciselnik(Operation.Pracovnici);
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Pracovnici, this, Konfigurace_Singleton.Instance.Prodej.SkladID);
            }

            mDrawerLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }

        private void HlavneMenu()
        {
            Intent intent = new Intent(this, typeof(Activity_HlavneMenu));
            Bundle bundle = new Bundle();
            bundle.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundle);
            //intent.pu
            this.StartActivity(intent);
            this.Finish();
        }

        #endregion

        #region Tlaítko pro novou dávku

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            LogikaNovaDavka();
        }

        private async void LogikaNovaDavka()
        {
            TaskCompletionSource<Prodej_Davka_Item> tsc_PerformNew = new TaskCompletionSource<Prodej_Davka_Item>();
            try
            {

                _Item = await PerformNew(tsc_PerformNew);

                if (_Item != null)
                {
                    mAdapter.AddItem(_Item);
                    await otevriDavkuAsync();
                    //otevriDavku_ToThread(StavyDavky.TypDokladu);
                }
                else
                {
                    await MessageBoxAsync.Show(this, "Chyba při tvorbe nové dávky!", "Error", MessageBoxButtons.OK);
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }


        #endregion

        #region Metoda pro volaní další aktivity

        //private void StartAktivityNasledujici(Type typAktivity, int ResoultCode = 0, bool finish = false, string menaID = null)
        //{
        //    //Button_Click(this.Resources.GetString(Resource.String.Prodej));
        //    Intent intent = new Intent(this, typAktivity);
        //    Bundle bundle = new Bundle();
        //    bundle.PutBinder(DataInfo_Static.object_ProdejItem, new ProdejItemWrapperForBinder(_Item));
        //    intent.PutExtra(DataInfo_Static.ProdejItem, bundle);

        //    Bundle bundleUser = new Bundle();
        //    bundleUser.PutBinder(DataInfo_Static.object_User, new UsersWrapperForBinder(Uzivatel));
        //    intent.PutExtra(DataInfo_Static.User, bundleUser);

        //    if (!string.IsNullOrEmpty(menaID))
        //    {
        //        intent.PutExtra(DataInfo_Static.MenaID, menaID);
        //    }

        //    if (ResoultCode == ResultCode_SkladyZdroj)
        //    {
        //        intent.PutExtra(DataInfo_Static.TypSkladu, DataInfo_Static.TypSkladu_Zdroj);
        //        intent.PutExtra(DataInfo_Static.TypVratky, DataInfo_Static.ProdejItem);
        //    }
        //    if (ResoultCode == ResultCode_SkladyCil)
        //    {
        //        intent.PutExtra(DataInfo_Static.TypSkladu, DataInfo_Static.TypSkladu_Cil);
        //        intent.PutExtra(DataInfo_Static.TypVratky, DataInfo_Static.ProdejItem);
        //    }


        //    if (finish)
        //    {
        //        this.StartActivity(intent);
        //        this.Finish();
        //    }
        //    else 
        //    {
        //        this.StartActivityForResult(intent, ResoultCode);
        //    }
        //}

        TaskCompletionSource<bool> _tcs = null;

        private Task<bool> StartAktivityNasledujici_Async(Type typAktivity, int ResoultCode = 0, bool finish = false, string menaID = null)
        {
            _tcs = null;
            _tcs = new TaskCompletionSource<bool>();
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);
            Bundle bundle = new Bundle();
            bundle.PutBinder(DataInfo_Static.object_ProdejItem, new WrapperForBinder<Prodej.Prodej_Davka_Item>(_Item));
            intent.PutExtra(DataInfo_Static.ProdejItem, bundle);

            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if (!string.IsNullOrEmpty(menaID))
            {
                intent.PutExtra(DataInfo_Static.MenaID, menaID);
            }

            if (ResoultCode == ResultCode_SkladyZdroj)
            {
                intent.PutExtra(DataInfo_Static.TypSkladu, DataInfo_Static.TypSkladu_Zdroj);
                intent.PutExtra(DataInfo_Static.TypVratky, DataInfo_Static.ProdejItem);
            }
            if (ResoultCode == ResultCode_SkladyCil)
            {
                intent.PutExtra(DataInfo_Static.TypSkladu, DataInfo_Static.TypSkladu_Cil);
                intent.PutExtra(DataInfo_Static.TypVratky, DataInfo_Static.ProdejItem);
            }


            if (finish)
            {
                this.StartActivity(intent);
                this.Finish();
                _tcs.SetResult(true);
                return _tcs.Task;
            }
            else
            {
                this.StartActivityForResult(intent, ResoultCode);
                return _tcs.Task;
            }

           
        }

        #endregion

        #region Nova davka

        private async Task<Prodej_Davka_Item> PerformNew(TaskCompletionSource<Prodej_Davka_Item> tsc_PerformNew)
        {
            try
            {
                string davkaprodej = string.Empty;
                Prodej_Davka_Item _Item = new Prodej_Davka_Item();

                if (Konfigurace_Singleton.Instance.Prodej.RangeEnable)
                {
                    Config.CiselneRady cr = new Config.CiselneRady();
                    davkaprodej = cr.ProdejGetNext();
                }
                else
                {
                    var res = await InputBoxAsync.Show(
                        this,
                        Title: string.Empty,
                        Message: Resources.GetString(Resource.String.Prodej3ProdejVyberDavkyZadejteCisloDavky),
                        Defaultvalue: string.Empty,
                        buttons: MessageBoxButtons.OKCancel,
                        keyboardMode: Android.Text.InputTypes.ClassNumber);

                    if (res.Dialog_Result == DialogResult.OK)
                    {
                        davkaprodej = res.Value;
                    }
                    else
                    {
                        return null;
                    }
                }

                if (!KontrolaCislaDavky(davkaprodej))
                {
                    await MessageBoxAsync.Show(this, Resources.GetString(Resource.String.Prodej3ProdejVyberDavkyChybaVCisleDavky), "Info", MessageBoxButtons.OK);
                    tsc_PerformNew.SetException(new Exception(Resources.GetString(Resource.String.Prodej3ProdejVyberDavkyChybaVCisleDavky)));
                }

                int davkaprodeji = int.Parse(davkaprodej);

                string dstFile = Path.Combine(DataInfo_Static.PathDir, davkaprodeji.ToString() + DataInfo_Static.PriponaDI);

                try
                {
                    if (File.Exists(dstFile))
                    {
                        await MessageBoxAsync.Show(this, string.Format(
                            Resources.GetString(Resource.String.Prodej3ProdejVyberDavkyDavkaJizExistuje)
                            , davkaprodeji)
                            , "Info", MessageBoxButtons.OK);

                        tsc_PerformNew.SetException(new Exception(Resources.GetString(Resource.String.Prodej3ProdejVyberDavkyDavkaJizExistuje)));
                    }
                    else
                    {

                        if (!File.Exists(DataInfo_Static.ProdejScript))
                        {
                            RestSharp.IRestResponse response;
                            API_Server.API_Komunikator.Communicate(API_Server.API_Komunikator.REST_Type.GET, out response, "ProdejSQLTemplate");
                            string SQLTemplateObsah = response.Content;

                            if (string.IsNullOrEmpty(SQLTemplateObsah))
                            {
                                throw new Exception("Problém, nebyl nalezen SQL Script pro založení lokalní databáze!");
                            }

                            MES_Android.SQLite_ProdejScript script = Newtonsoft.Json.JsonConvert.DeserializeObject<MES_Android.SQLite_ProdejScript>(SQLTemplateObsah);



                            if (!Directory.Exists(DataInfo_Static.SQLiteDBsDir))
                                Directory.CreateDirectory(DataInfo_Static.SQLiteDBsDir);

                            File.WriteAllText(DataInfo_Static.ProdejScript, script.Script);
                        }

                        SQLite_Helper helper = new SQLite_Helper();
                        if (!helper.SQLite_CreateFile(dstFile, DataInfo_Static.ProdejScript))
                        {
                            throw new Exception("Nastala chyba pri tvorbe SQLite souboru pro " + "Prodej");
                        }

                        _Item.CisloDavky = davkaprodeji;
                        DataInfo_Static.ProdejGO_Instance.Davka = davkaprodeji;

                        tsc_PerformNew.SetResult(_Item);
                    }
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                    tsc_PerformNew.SetException(ex);
                    //finalize();
                    //DialogResult = DialogResult.OK;
                    // return _Item;
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                tsc_PerformNew.SetException(ex);
            }

            return tsc_PerformNew.Task.Result;
        }
        

        /// <summary>
        /// Kontrola, zda se opravdu jedná o číslo
        /// </summary>
        /// <param name="cislodavky"></param>
        /// <returns></returns>
        private bool KontrolaCislaDavky(string cislodavky)
        {
            try
            {
                var _cislodavky = int.Parse(cislodavky);
                if (_cislodavky < 0)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Eventy click a longClick pro 

        private async void MAdapter_ItemClick(object sender, int e)
        {

            try
            {

                //string s = string.Format("pozice:'{0}'", e);

                //Toast.MakeText(this, s, ToastLength.Short).Show();

                _Item = mAdapter.PolozkaSeznam[e];

                if (_Item == null)
                {
                    await MessageBoxAsync.Show(this, "Položka nenalezena.", "Error!", MessageBoxButtons.OK);
                    return;
                }

                //if (polozka.Polozek > 0)
                //{
                //    StartAktivityNasledujici(typeof(Prodej.Prodej_SberDat), polozka);
                //}
                //else
                //{
                //    StartAktivityNasledujici(typeof(Prodej.Prodej_TypDokladu), polozka);
                //}

                //otevriDavku_ToThread(StavyDavky.TypDokladu);
                await otevriDavkuAsync();

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

        }

        private void MAdapter_ItemLongClick(object sender, int e)
        {

            RunOnUiThread(() => {

                new Android.Support.V7.App.AlertDialog.Builder(this)
                    .SetTitle(Resources.GetString(Resource.String.ProdejDavka_LC_Title))
                    .SetMessage(string.Format(Resources.GetString(Resource.String.ProdejDavka_LC_Message), mAdapter.PolozkaSeznam[e].CisloDavky))
                    .SetPositiveButton(Resources.GetString(Resource.String.ProdejDavka_LC_PositiveBTN), delegate { SendDavka(e); })
                    .SetNegativeButton(Resources.GetString(Resource.String.ProdejDavka_LC_NegativeBTN), delegate { DeleteDavka(e); })
                    .SetNeutralButton(Resources.GetString(Resource.String.ProdejDavka_LC_NeutralBTN), delegate { })
                    .Create()
                    .Show();

            });
        }


        #endregion

        #region Smazaní dávky

        private void DeleteDavka(int pozice)
        {
            try
            {


                var selected = mAdapter.PolozkaSeznam[pozice];
                if (selected == null)
                    return;

                // pokud jsou zapnuty lokace a davka obsahuje zaznamy, neni mozne davku smazat ...
                if (selected.PocetRadku > 0 && selected.cfg_lok_mech.HasValue && selected.cfg_lok_mech > 0)
                {
                    ShowMessage(this, Resources.GetString(Resource.String.ProdejDavkaLokMechKontrola), "Error");
                    return;
                }

                string davka = selected.CisloDavky.ToString();

                RunOnUiThread(() => {

                    new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetTitle(Resources.GetString(Resource.String.ProdejDavka_LC_Title))
                        .SetMessage(string.Format(Resources.GetString(Resource.String.ProdejDavka_LC_Opravdu), davka))
                        .SetNegativeButton(Resources.GetString(Resource.String.Text_OK), delegate {
                            string fn = Path.Combine(DataInfo_Static.PathDir, davka + DataInfo_Static.PriponaDI);
                            File.Delete(fn);
                            mAdapter.RemoveItem(pozice);
                            Config.CiselneRady cr = new Config.CiselneRady();
                            cr.ProdejSetDeleted(davka);
                        })
                        .SetNeutralButton(Resources.GetString(Resource.String.Text_Zrusit), delegate { })
                        .Create()
                        .Show();

                });

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        #endregion

        #region Odeslaní dávky


        private async void SendDavka(int pozice)
        {
            try
            {
                ProgressDialog_Infinity.Show(this);

                Prodej_Davka_Item selected = null;
                RunOnUiThread(() =>
                {
                    selected = mAdapter.PolozkaSeznam[pozice];
                });

                if (selected == null)
                    return;

                int cd = selected.CisloDavky.Value;
                DataInfo_Static.ProdejGO_Instance.Davka = null;
                DataInfo_Static.ProdejGO_Instance.Davka = cd;
                int pocetpolozek = DataInfo_Static.ProdejGO_Instance.controller_prodej.CZMST_DI_Count();
                if (pocetpolozek <= 0)
                {
                     await MessageBoxAsync.Show(this, string.Format(Resources.GetString(Resource.String.Prodej3ProdejMainDavkaNeobsahujePolozky), cd.ToString()), "info", MessageBoxButtons.OK);
                    return;
                }


                ProdejServiceOperations serviceOperations = new ProdejServiceOperations();
                //bool result = true;
                //TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
                var T = serviceOperations.SendData(this, DataInfo_Static.ProdejGO_Instance.servis_prodej, cd, Uzivatel.ID.Value, Uzivatel.Login);

                bool result = await T;

                if (result)
                {
                    bool ProdejDialogUspesnehoOdeslaniDavky = true;

                    if (ProdejDialogUspesnehoOdeslaniDavky)
                        await MessageBoxAsync.Show(this, string.Format(Resources.GetString(Resource.String.Prodej3ProdejMainDavkaOdeslana), cd.ToString()), "info", MessageBoxButtons.OK);

                    RunOnUiThread(()=> {
                        mAdapter.RemoveItem(pozice);
                    });

                    var filename = Path.Combine(DataInfo_Static.PathDir, cd.ToString() + DataInfo_Static.PriponaDI);

                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }


                }
                else
                {
                    await MessageBoxAsync.Show(this, string.Format(Resources.GetString(Resource.String.Prodej3ProdejMainDavkaSeNepodarilaOdeslat), cd.ToString()), "info", MessageBoxButtons.OK);
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ProgressDialog_Infinity.Dispose();
                await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
            finally
            {
                ProgressDialog_Infinity.Dispose();
            }

        }


        #endregion

        #region Scanner DataWedge Experiment

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

        #region Metoda pro dotažení informací ohledne dávek nalezenych lokalně


        private Prodej_Davka_Seznam GetExistujiciDavky(string[] fileNames)
        {
            try
            {
                ObservableCollection<Prodej_Davka_Item> _Items = new ObservableCollection<Prodej_Davka_Item>();

                try { DataInfo_Static.ProdejGO_Instance.controller_odberatele.Connection.Open(); }
                catch { }
                try { DataInfo_Static.ProdejGO_Instance.controller_typdokladu.Connection.Open(); }
                catch { }
                try { DataInfo_Static.ProdejGO_Instance.controller_sklady.Connection.Open(); }
                catch { }
                try { DataInfo_Static.ProdejGO_Instance.controller_strediska.Connection.Open(); }
                catch { }

                foreach (string filename in fileNames)
                {
                    int pocetpolozek = 0; // OK
                    Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row typDok = null; // OK
                    Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odber = null; // OK
                    Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad = null; // OK
                    Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row sklad_dest = null; // OK
                    Fask.SQLiteDBs.DataSets.Strediska.CZMST091Row stre = null; // OK
                    Fask.SQLiteDBs.DataSets.Meny.CZMST097Row mena = null; // OK
                    byte cfg_lok_mech = 0; // OK
                    int davka = int.Parse(Path.GetFileNameWithoutExtension(filename)); // OK


                    Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow diFirstRow = null;

                    using (var controller_davka_tmp = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(filename))
                    {
                        diFirstRow = controller_davka_tmp.CZMST_DI_GetFirstRecord();

                        if (diFirstRow != null)
                        {
                            try
                            {
                                var dt = DataInfo_Static.ProdejGO_Instance.controller_odberatele.GetDataByOdbid(diFirstRow.ODB_ID);
                                if (dt != null && (dt.Count == 1))
                                    odber = dt[0];
                            }
                            catch { }

                            try
                            {

                                object o = DataInfo_Static.ProdejGO_Instance.controller_typdokladu.GetLokMech_Status(diFirstRow.DOC_ID, diFirstRow.DOC_ID2);
                                if (o != null && (o is byte))
                                    cfg_lok_mech = (byte)o;
                                else cfg_lok_mech = 0;
                            }
                            catch { }

                            try
                            {
                                var dt = DataInfo_Static.ProdejGO_Instance.controller_typdokladu.GetDataByDocID_DocID2(diFirstRow.DOC_ID, diFirstRow.DOC_ID2);
                                if (dt != null && (dt.Count == 1))
                                    typDok = dt[0];

                            }
                            catch { }


                            try
                            {
                                if (!Konfigurace_Singleton.Instance.Prodej.StrediskoText)
                                {
                                var dt = DataInfo_Static.ProdejGO_Instance.controller_strediska.GetDataByStr_id(diFirstRow.STR_ID);
                                    if (dt != null && (dt.Count == 1))
                                        stre = dt[0];
                                }
                            }
                            catch { }

                            try
                            {
                                var dt = DataInfo_Static.ProdejGO_Instance.controller_sklady.GetDataBySkl_id(diFirstRow.SKL_ID);
                                if (dt != null && (dt.Count == 1))
                                    sklad = dt[0];
                            }
                            catch { }

                            try
                            {
                                var dt = DataInfo_Static.ProdejGO_Instance.controller_sklady.GetDataBySkl_id(diFirstRow.SKL_ID_DEST);
                                if (dt != null && (dt.Count == 1))
                                    sklad_dest = dt[0];
                            }
                            catch { }

                            try
                            {
                                var dt = DataInfo_Static.ProdejGO_Instance.controller_meny.GetDataByMenaID(diFirstRow.mena_ID);
                                if (dt != null && (dt.Count == 1))
                                    mena = dt[0];
                            }
                            catch { }

                            try
                            {
                                pocetpolozek = controller_davka_tmp.PocetPolozek_DI() ?? 0;

                            }
                            catch { }
                        }
                    }

                    Prodej_Davka_Item _Item = new Prodej_Davka_Item();
                    _Item.CisloDavky = davka;
                    _Item.PocetRadku = pocetpolozek;
                    _Item.Odberatel = odber;
                    _Item.TypDokladu = typDok;
                    _Item.Stredisko = stre;
                    _Item.SkladZdroj = sklad;
                    _Item.SkladCil = sklad_dest;
                    _Item.cfg_lok_mech = cfg_lok_mech;
                    _Item.Mena = mena;

                    _Items.Add(_Item);
                }


                return new Prodej_Davka_Seznam(_Items);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                return new Prodej_Davka_Seznam(new ObservableCollection<Prodej_Davka_Item>());
            }
            finally
            {
                // nakonec vse uzavru, pokud je otevreno ...
                if ((DataInfo_Static.ProdejGO_Instance.controller_odberatele.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                    DataInfo_Static.ProdejGO_Instance.controller_odberatele.Connection.Close();
                if ((DataInfo_Static.ProdejGO_Instance.controller_sklady.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                    DataInfo_Static.ProdejGO_Instance.controller_sklady.Connection.Close();
                if ((DataInfo_Static.ProdejGO_Instance.controller_strediska.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                    DataInfo_Static.ProdejGO_Instance.controller_strediska.Connection.Close();
                if ((DataInfo_Static.ProdejGO_Instance.controller_typdokladu.Connection.State & ConnectionState.Open) == ConnectionState.Open)
                    DataInfo_Static.ProdejGO_Instance.controller_typdokladu.Connection.Close();
            }
        }


        #endregion

        #region Search

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Prodej_Davky_Search, menu);

            var item = menu.FindItem(Resource.Id.ProdejDavky_search);

            var searchView = item.ActionView; // IMenuItem obsahuje rovno objekt view :D

            _searchView = searchView.JavaCast<Android.Support.V7.Widget.SearchView>();
            _searchView.InputType = (int)Android.Text.InputTypes.ClassNumber;


            _searchView.QueryTextChange += (s, e) => mAdapter.Filter.InvokeFilter(e.NewText);

            _searchView.QueryTextSubmit += (s, e) =>
            {
                // Handle enter/search button on keyboard here
                Toast.MakeText(this, "Searched for: " + e.NewText, ToastLength.Short).Show();
                e.Handled = true;
            };

            //MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(mAdapter));  // Obeselete
            item.SetOnActionExpandListener(new SearchViewExpandListener(mAdapter)); // primo objekt IMenuItem obsahuje metodu


            return true;
        }


        #endregion

        #region Metody pro Permision

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

        #region Logika Otevření / nová dávka

        //public enum StavyDavky
        //{
        //    TypDokladu,
        //    SNNaDavku,
        //    Odberatel,
        //    mena_ID,
        //    cfg_mena_id,
        //    Sklad_Zdroj,
        //    Sklad_Cil,
        //    cfg_prevod_sklad,
        //    cfg_zakazka_id,
        //    cfg_paleta_id,
        //    SBER_DAT
        //}

        int ResultCode_TypDokladu = 1;
        int ResultCode_Odberatel = 2;
        int ResultCode_cfg_mena_id = 3;
        int ResultCode_SkladyZdroj = 4;
        int ResultCode_SkladyCil = 5;
        int ResultCode_VyberPalety = 6;



        private async Task<bool> otevriDavkuAsync()
        {
            TaskCompletionSource<bool> tcs_Konfigurace = new TaskCompletionSource<bool>();

            try
            {
                string zakazkaID = string.Empty;
                string paletaID = string.Empty;
                string skladID = string.Empty;

                #region TypDokladu

                if (_Item.TypDokladu == null)
                {
                    bool stateTD = await StartAktivityNasledujici_Async(typeof(Prodej_TypDokladu), ResultCode_TypDokladu);

                    if (!stateTD)
                    {
                        tcs_Konfigurace.SetResult(false);
                        return tcs_Konfigurace.Task.Result;
                    }
                }

                #endregion

                #region cfg_sn_na_davku

                string di_serltnum = string.Empty;

                // zadani sarze na davku
                // zobrazit SN, pripadne vygenerovat?? ...
                if (_Item.TypDokladu != null && !_Item.TypDokladu.Iscfg_sn_na_davkuNull() && _Item.TypDokladu.cfg_sn_na_davku > 0)
                {
                    // vygenerovani, pokud je povoleno, jinak rucne zadat
                    if (!_Item.TypDokladu.Iscfg_generovat_snNull() && _Item.TypDokladu.cfg_generovat_sn > 0)
                    {
                        di_serltnum = GenerovatSN();
                    }

                   var stateSN =  await InputBoxAsync.Show(
                        this, 
                        Title: "Šarže", 
                        Message: "Zadaní šarže",
                        Defaultvalue: string.IsNullOrEmpty(di_serltnum) ? string.Empty : di_serltnum.Trim(),
                        buttons: MessageBoxButtons.OKCancel);

                    if (stateSN.Dialog_Result == DialogResult.Cancel)
                    {
                        tcs_Konfigurace.SetResult(false);
                        return tcs_Konfigurace.Task.Result;
                    }

                    di_serltnum = stateSN.Value;
                }

                #endregion

                #region cfg_odb

                if (_Item.TypDokladu != null && _Item.TypDokladu.cfg_odb > 0)
                {

                    if (_Item.Odberatel == null)
                    {
                        bool stateODB = await StartAktivityNasledujici_Async(typeof(Prodej_Odberatel), ResultCode_Odberatel);

                        if (!stateODB)
                        {
                            tcs_Konfigurace.SetResult(false);
                            return tcs_Konfigurace.Task.Result;
                        }
                    }
                }

                #endregion

                #region mena_ID

                if (_Item.Odberatel != null && !_Item.Odberatel.Ismena_IDNull() && _Item.Odberatel.mena_ID.Trim().Length > 0)
                {
                    Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable dt = DataInfo_Static.ProdejGO_Instance.controller_meny.GetDataByMenaID(_Item.Odberatel.mena_ID);
                    if (dt.Count > 0)
                        _Item.Mena = dt[0];
                    else
                    { // vychozi menu nastavit ... ???
                      //dt = meny_ta.GetData();
                      //var linqHlavniMena = dt.Where(m => m.mena_hlavni);
                      //if (linqHlavniMena.Count() > 0)
                      //{
                      //    mena = linqHlavniMena.First();
                      //}                        

                        //dt = meny_ta.GetDataByHlavni(true);
                        //if (dt.Count > 0)
                        //{
                        //    mena = dt[0]; //nastavena hlavni mena ...
                        //}
                    }

                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
                    {

                        Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = conPro.GetData_DIH();

                        if (dih_dt.Count > 0)
                        {
                            zakazkaID = dih_dt[0].Zakazka_ID;
                            paletaID = dih_dt[0].Paleta_ID;
                            skladID = dih_dt[0].SKL_ID;
                            conPro.DeleteQuery_DIH();
                        }
                        conPro.Insert_DIH(zakazkaID, paletaID, _Item.Mena == null ? "" : _Item.Mena.mena_ID, skladID, _Item.CisloDavky.Value);
                    }
                }

                #endregion

                #region cfg_mena_id

                if (_Item.TypDokladu != null && !_Item.TypDokladu.Iscfg_mena_idNull() && _Item.TypDokladu.cfg_mena_id > 0 && _Item.Mena == null)
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
                    {

                        Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = conPro.GetData_DIH();

                        string SelectedMenaID = null;

                        if (dih_dt.Count > 0)
                            SelectedMenaID = dih_dt[0].mena_ID;

                        bool stateMena = await StartAktivityNasledujici_Async(
                            typeof(Prodej_Meny),
                            ResultCode_cfg_mena_id,
                            menaID: SelectedMenaID);

                        if (!stateMena)
                        {
                            tcs_Konfigurace.SetResult(false);
                            return tcs_Konfigurace.Task.Result;
                        }

                    }
                }

                #endregion

                #region skladZdroj

                // vyber zdrojoveho skladu
                //if (Prodej.Globals.FiltrCiselnikSkladu)  // PeV - 25.9.2015 uprava, povoleni skladu a filtr na sklady je zvlast
                if (_Item.TypDokladu != null && !_Item.TypDokladu.Iscfg_skladyNull() && _Item.TypDokladu.cfg_sklady > 0)
                {

                    if (_Item.SkladZdroj == null)
                    {
                        // SKLAD_ID je nastaven -> dohleda se sklad z ciselniku skladu (pokud nenalezeno, probehne vyber)
                        // SKLAD_ID neni nastaven a neexistuje vybrany sklad -> zobrazi se ciselnik skladu a bude se prenaset skl_id z 095
                        // SKLAD_ID neni nastaven a existuje vybrany sklad -> dohleda se z davky (spatne popsano, existuje only one??)

                        // ma se vyuzit id zadaneho skladu
                        // id skladu vyplneno v konfiguraci aplikace
                        if (!string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Prodej.SkladID))
                        {
                            try
                            {
                                Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = DataInfo_Static.ProdejGO_Instance.controller_sklady.GetDataBySkl_id(Konfigurace_Singleton.Instance.Prodej.SkladID);
                                if (dt_sklady.Count > 0)
                                    _Item.SkladZdroj = dt_sklady[0];
                                else
                                {
                                    await MessageBoxAsync.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejMainSkladNenalezenVyberteJiny), Konfigurace_Singleton.Instance.Prodej.SkladID.Trim()), "info", MessageBoxButtons.OK);
                                }
                            }
                            catch (Exception ex)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(ex);
                                await MessageBoxAsync.Show(this, ex.Message, "error", MessageBoxButtons.OK);
                                tcs_Konfigurace.SetResult(false);
                                return tcs_Konfigurace.Task.Result;
                            }
                        }

                        // predvyplneno id skladu (SKL_ID) v typu dokladu
                        if (_Item.SkladZdroj == null && !_Item.TypDokladu.IsSKL_IDNull() && !string.IsNullOrEmpty(_Item.TypDokladu.SKL_ID.Trim()))
                        {
                            try
                            {
                                Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = DataInfo_Static.ProdejGO_Instance.controller_sklady.GetDataBySkl_id(_Item.TypDokladu.SKL_ID.Trim());
                                if (dt_sklady.Count > 0)
                                    _Item.SkladZdroj = dt_sklady[0];
                                else
                                {
                                    await MessageBoxAsync.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejMainSkladNenalezenVyberteJiny), _Item.TypDokladu.SKL_ID), "info", MessageBoxButtons.OK);
                                }
                            }
                            catch (Exception ex)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(ex);
                                await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                                tcs_Konfigurace.SetResult(false);
                                return tcs_Konfigurace.Task.Result;
                            }
                        }

                        if (_Item.SkladZdroj == null)
                        {
                            await StartAktivityNasledujici_Async(typeof(Prodej_Sklady), ResultCode_SkladyZdroj); ;

                        }

                    }
                }


                #endregion

                #region skladCil

                if (_Item.SkladCil == null)
                {
                    // zadani ciloveho skladu
                    if (_Item.TypDokladu != null && !_Item.TypDokladu.Iscfg_skl_id_destNull() && _Item.TypDokladu.cfg_skl_id_dest > 0)
                    {
                        // sklad se ma prevzit ze zdrojoveho skladu
                        if (!_Item.TypDokladu.Iscfg_skl_id_dest_prevzitNull() && _Item.TypDokladu.cfg_skl_id_dest_prevzit > 0 && _Item.SkladZdroj != null)
                            _Item.SkladCil = _Item.SkladZdroj;

                        // predvyplneno id skladu (SKL_ID) v typu dokladu
                        if (_Item.SkladCil == null && !_Item.TypDokladu.Ispredvyplnit_locncodedestNull() && !string.IsNullOrEmpty(_Item.TypDokladu.predvyplnit_skl_id_dest.Trim()))
                        {
                            try
                            {
                                Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = DataInfo_Static.ProdejGO_Instance.controller_sklady.GetDataBySkl_id(_Item.TypDokladu.predvyplnit_skl_id_dest.Trim());
                                if (dt_sklady.Count > 0)
                                    _Item.SkladCil = dt_sklady[0];
                                else
                                {
                                    await MessageBoxAsync.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejMainSkladCilNenalezenVyberteJiny), _Item.TypDokladu.predvyplnit_skl_id_dest), "info", MessageBoxButtons.OK);
                                }
                            }
                            catch (Exception ex)
                            {
                                Fask.Logging.ExceptionHandler2.Handle(ex);
                                await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                                tcs_Konfigurace.SetResult(false);
                                return tcs_Konfigurace.Task.Result;
                            }
                        }

                        if (_Item.SkladCil == null)
                        {
                            await StartAktivityNasledujici_Async(typeof(Prodej_Sklady), ResultCode_SkladyCil);

                        }

                    }

                }

                #endregion

                // TODO : nechapu naco to je... dodelat
                #region cfg_prevod_sklad

                //if ((_Item.TypDokladu != null && _Item.TypDokladu.cfg_prevod_sklad > 0) || Prodej.Globals.PovolitPrevodMeziSklady)
                //{
                //    Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = this.globalObject.controller_prodej.GetData_DIH();
                //    DialogResult dresult = DialogResult.None;
                //    if (dih_dt.Count > 0)
                //    {
                //        zakazkaID = dih_dt[0].Zakazka_ID;
                //        skladID = dih_dt[0].SKL_ID;
                //        paletaID = dih_dt[0].Paleta_ID;
                //        dresult = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainCilovySkladDriveZvolenPouzitStejnyDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
                //    }

                //    if (dresult != DialogResult.Yes)
                //    {
                //        using (FormSkladVyber fsv = new FormSkladVyber())
                //        {
                //            fsv.Text = Prodej3ProdejMainVyberteCilovySklad;  // "Vyberte cílový sklad";
                //            if (fsv.ShowDialog() == DialogResult.Cancel)
                //                return;

                //            if (fsv.Sklad == null)
                //            {
                //                Logging.Log.Write("Není vybrán cílový sklad, přestože je vyžadován!");
                //                return;
                //            }
                //            else
                //            {
                //                skladID = fsv.Sklad.skl_id;
                //            }
                //        }
                //    }

                //    if (dih_dt.Count > 0)
                //        this.globalObject.controller_prodej.DeleteQuery_DIH();
                //    this.globalObject.controller_prodej.Insert_DIH(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, this.globalObject.Davka.Value);
                //}

                #endregion

                #region cfg_zakazka_id

                if (_Item.TypDokladu != null && _Item.TypDokladu.cfg_zakazka_id > 0)
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
                    {
                        Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = conPro.GetData_DIH();

                        if (dih_dt.Count > 0)
                        {
                            zakazkaID = dih_dt[0].Zakazka_ID;
                            skladID = dih_dt[0].SKL_ID;
                        }

                        var res = await InputBoxAsync.Show(this,
                            Title: string.Empty,
                            Message: Resources.GetString(Resource.String.Prodej3ProdejMainZadejteCisloZakazky),
                            Defaultvalue: zakazkaID,
                            buttons: MessageBoxButtons.OKCancel,
                            keyboardMode: Android.Text.InputTypes.ClassNumber
                            );

                        if (res.Dialog_Result != DialogResult.OK)
                        {
                            tcs_Konfigurace.SetResult(false);
                            return tcs_Konfigurace.Task.Result;
                        }
;
                        zakazkaID = res.Value;
                        if (dih_dt.Count > 0)
                        {
                            paletaID = dih_dt[0].Paleta_ID;
                            conPro.DeleteQuery_DIH();
                        }

                        conPro.Insert_DIH(zakazkaID, paletaID, _Item.Mena == null ? "" : _Item.Mena.mena_ID, skladID, _Item.CisloDavky.Value);

                    }
                }


                #endregion

                #region cfg_paleta_id

                if (_Item.TypDokladu != null && _Item.TypDokladu.cfg_paleta_id > 0)
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
                    {
                        Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = conPro.GetData_DIH();

                        if (dih_dt.Count > 0)
                        {
                            paletaID = dih_dt[0].Paleta_ID;
                            skladID = dih_dt[0].SKL_ID;
                        }

                        var res = await InputBoxAsync.Show(this,
                                    Title: string.Empty,
                                    Message: Resources.GetString(Resource.String.Prodej3ProdejMainZadejteCisloPalety),
                                    Defaultvalue: paletaID,
                                    buttons: MessageBoxButtons.OKCancel,
                                    keyboardMode: Android.Text.InputTypes.ClassNumber
                                    );

                        if (res.Dialog_Result != DialogResult.OK)
                        {
                            tcs_Konfigurace.SetResult(false);
                            return tcs_Konfigurace.Task.Result;
                        }


                        if (dih_dt.Count > 0)
                        {
                            paletaID = dih_dt[0].Paleta_ID;
                            conPro.DeleteQuery_DIH();
                        }

                        conPro.Insert_DIH(zakazkaID, paletaID, _Item.Mena == null ? "" : _Item.Mena.mena_ID, skladID, _Item.CisloDavky.Value);
                    }
                }


                #endregion

                #region cfg_palety

                if ((_Item.TypDokladu != null && _Item.TypDokladu.cfg_palety > 0 ))
                {
                    bool stateTD = await StartAktivityNasledujici_Async(typeof(Prodej_VyberPalety), ResultCode_VyberPalety);

                    if (!stateTD)
                    {
                        tcs_Konfigurace.SetResult(false);
                        return tcs_Konfigurace.Task.Result;
                    }
                }

                #endregion

                bool state = await StartAktivityNasledujici_Async(typeof(Prodej_SberDat), finish: true);

                if (!state)
                {
                    tcs_Konfigurace.SetResult(false);
                    return tcs_Konfigurace.Task.Result;
                }


                tcs_Konfigurace.SetResult(true);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            return tcs_Konfigurace.Task.Result;

        }

        #region Puvodni logika

        //private void otevriDavku(StavyDavky stav)
        //{
        //    try
        //    {

        //        //parametry hlavicky ...
        //        string zakazkaID = string.Empty;
        //        string paletaID = string.Empty;
        //        string skladID = string.Empty;

        //        if (stav == StavyDavky.TypDokladu)
        //        {

        //            #region Typ Dokladu

        //            if (_Item.TypDokladu == null)
        //            {
        //                StartAktivityNasledujici(typeof(Prodej_TypDokladu), ResultCode_TypDokladu);
        //            }
        //            else
        //            {
        //                stav = StavyDavky.SNNaDavku;
        //            }

        //            // 22.3.2021 dle JiS Odtranen parametr Prodej.Globals.TypDokladu

        //            //Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row typdokladu = null;
        //            //if (Prodej.Globals.TypDokladu)
        //            //{
        //            //    using (ProdejVyberTypuDokladu ptd = new ProdejVyberTypuDokladu(this.globalObject.Davka.Value))
        //            //    {
        //            //        if (ptd.ShowDialog() == DialogResult.Cancel)
        //            //            return;
        //            //        typdokladu = ptd.TypDokladu;
        //            //        if (typdokladu == null)
        //            //            return;
        //            //    }
        //            //}

        //            #endregion
        //        }

        //        if (stav == StavyDavky.SNNaDavku)
        //        {

        //            #region cfg_sn_na_davku

        //            string di_serltnum = string.Empty;

        //            // zadani sarze na davku
        //            // zobrazit SN, pripadne vygenerovat?? ...
        //            if (_Item.TypDokladu != null && !_Item.TypDokladu.Iscfg_sn_na_davkuNull() && _Item.TypDokladu.cfg_sn_na_davku > 0)
        //            {
        //                // vygenerovani, pokud je povoleno, jinak rucne zadat
        //                if (!_Item.TypDokladu.Iscfg_generovat_snNull() && _Item.TypDokladu.cfg_generovat_sn > 0)
        //                {
        //                    di_serltnum = GenerovatSN();
        //                }

        //                using (SejmiKodForm skf = new SejmiKodForm())
        //                {
        //                    skf.Popis = "Šarže";
        //                    skf.Text = "Zadaní šarže";
        //                    skf.CodeType = Android.Text.InputTypes.ClassText;
        //                    skf.AllowEmpty = false; //TODO : udelat nejak logiku na AllowEmpty
        //                    skf.Kod = string.IsNullOrEmpty(di_serltnum) ? string.Empty : di_serltnum.Trim();

        //                    if (skf.ShowDialog(this) == DialogResult.Cancel)
        //                        return;

        //                    di_serltnum = skf.Kod;
        //                    stav = StavyDavky.Odberatel;
        //                }
        //            }
        //            else
        //            {
        //                stav = StavyDavky.Odberatel;
        //            }

        //            #endregion

        //        }

        //        if (stav == StavyDavky.Odberatel)
        //        {
        //            if (_Item.TypDokladu != null && _Item.TypDokladu.cfg_odb > 0)
        //            {

        //                if (_Item.Odberatel == null)
        //                {
        //                    StartAktivityNasledujici(typeof(Prodej_Odberatel), ResultCode_Odberatel);
        //                }
        //                else
        //                {
        //                    stav = StavyDavky.mena_ID;
        //                }
        //            }
        //            else
        //            {
        //                stav = StavyDavky.mena_ID;
        //            }

        //            #region cfg_odb

        //            //Fask.SQLiteDBs.DataSets.Odberatele.CZMST090Row odberatel = null;
        //            //if ((typdokladu != null && typdokladu.cfg_odb > 0) || Prodej.Globals.Odberatel)
        //            //{
        //            //    using (ProdejVyberOdberatele po = new ProdejVyberOdberatele(typdokladu, this.globalObject.Davka.Value))
        //            //    {
        //            //        if (po.ShowDialog() == DialogResult.Cancel)
        //            //            return;

        //            //        odberatel = po.Odberatel;

        //            //        if (odberatel == null)
        //            //        {
        //            //            Logging.Log.Write("Není vybrán odběratel, přestože je vyžadován!");
        //            //            return;
        //            //        }
        //            //    }
        //            //}

        //            #endregion

        //        }

        //        if (stav == StavyDavky.mena_ID)
        //        {
        //            #region mena_ID

        //            if (_Item.Odberatel != null && !_Item.Odberatel.Ismena_IDNull() && _Item.Odberatel.mena_ID.Trim().Length > 0)
        //            {
        //                Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable dt = Classes.DataInfo_Static.ProdejGO_Instance.controller_meny.GetDataByMenaID(_Item.Odberatel.mena_ID);
        //                if (dt.Count > 0)
        //                    _Item.Mena = dt[0];
        //                else
        //                { // vychozi menu nastavit ... ???
        //                  //dt = meny_ta.GetData();
        //                  //var linqHlavniMena = dt.Where(m => m.mena_hlavni);
        //                  //if (linqHlavniMena.Count() > 0)
        //                  //{
        //                  //    mena = linqHlavniMena.First();
        //                  //}                        

        //                    //dt = meny_ta.GetDataByHlavni(true);
        //                    //if (dt.Count > 0)
        //                    //{
        //                    //    mena = dt[0]; //nastavena hlavni mena ...
        //                    //}
        //                }

        //                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
        //                {

        //                    Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = conPro.GetData_DIH();

        //                    if (dih_dt.Count > 0)
        //                    {
        //                        zakazkaID = dih_dt[0].Zakazka_ID;
        //                        paletaID = dih_dt[0].Paleta_ID;
        //                        skladID = dih_dt[0].SKL_ID;
        //                        conPro.DeleteQuery_DIH();
        //                    }
        //                    conPro.Insert_DIH(zakazkaID, paletaID, _Item.Mena == null ? "" : _Item.Mena.mena_ID, skladID, _Item.CisloDavky.Value);
        //                }
        //            }

        //            stav = StavyDavky.cfg_mena_id;

        //            #endregion 
        //        }

        //        if (stav == StavyDavky.cfg_mena_id)
        //        {

        //            #region cfg_mena_id

        //            if (_Item.TypDokladu != null && !_Item.TypDokladu.Iscfg_mena_idNull() && _Item.TypDokladu.cfg_mena_id > 0 && _Item.Mena == null)
        //            {
        //                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
        //                {

        //                    Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = conPro.GetData_DIH();

        //                    //using (Prodej_VyberMeny po = new Prodej_VyberMeny())
        //                    //{
        //                    string SelectedMenaID = null;

        //                    if (dih_dt.Count > 0)
        //                        SelectedMenaID = dih_dt[0].mena_ID;

        //                    StartAktivityNasledujici(
        //                        typeof(Prodej_Meny),
        //                        ResultCode_cfg_mena_id,
        //                        menaID: SelectedMenaID);

        //                    //if (po.ShowDialog() == DialogResult.Cancel)
        //                    //    return;
        //                }
        //            }
        //            else
        //            {
        //                stav = StavyDavky.Sklad_Zdroj;
        //            }

        //            #endregion 
        //        }

        //        if (stav == StavyDavky.Sklad_Zdroj)
        //        {
        //            #region skladZdroj

        //            // vyber zdrojoveho skladu
        //            //if (Prodej.Globals.FiltrCiselnikSkladu)  // PeV - 25.9.2015 uprava, povoleni skladu a filtr na sklady je zvlast
        //            if (_Item.TypDokladu != null && !_Item.TypDokladu.Iscfg_skladyNull() && _Item.TypDokladu.cfg_sklady > 0) 
        //            {

        //                if (_Item.SkladZdroj != null)
        //                {
        //                    stav = StavyDavky.Sklad_Cil;
        //                }
        //                else
        //                {
        //                    // SKLAD_ID je nastaven -> dohleda se sklad z ciselniku skladu (pokud nenalezeno, probehne vyber)
        //                    // SKLAD_ID neni nastaven a neexistuje vybrany sklad -> zobrazi se ciselnik skladu a bude se prenaset skl_id z 095
        //                    // SKLAD_ID neni nastaven a existuje vybrany sklad -> dohleda se z davky (spatne popsano, existuje only one??)

        //                    // ma se vyuzit id zadaneho skladu
        //                    // id skladu vyplneno v konfiguraci aplikace
        //                    if (!string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Prodej.SkladID))
        //                    {
        //                        try
        //                        {
        //                            Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = DataInfo_Static.ProdejGO_Instance.controller_sklady.GetDataBySkl_id(Konfigurace_Singleton.Instance.Prodej.SkladID);
        //                            if (dt_sklady.Count > 0)
        //                                _Item.SkladZdroj = dt_sklady[0];
        //                            else
        //                            {
        //                                MessageBox.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejMainSkladNenalezenVyberteJiny), Konfigurace_Singleton.Instance.Prodej.SkladID.Trim()), "info", MessageBoxButtons.OK);
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            Fask.Logging.ExceptionHandler2.Handle(ex);
        //                            MessageBox.Show(this, ex.Message, "error", MessageBoxButtons.OK);
        //                            return;
        //                        }
        //                    }

        //                    // predvyplneno id skladu (SKL_ID) v typu dokladu
        //                    if (_Item.SkladZdroj == null && !_Item.TypDokladu.IsSKL_IDNull() && !string.IsNullOrEmpty(_Item.TypDokladu.SKL_ID.Trim()))
        //                    {
        //                        try
        //                        {
        //                            Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = DataInfo_Static.ProdejGO_Instance.controller_sklady.GetDataBySkl_id(_Item.TypDokladu.SKL_ID.Trim());
        //                            if (dt_sklady.Count > 0)
        //                                _Item.SkladZdroj = dt_sklady[0];
        //                            else
        //                            {
        //                                MessageBox.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejMainSkladNenalezenVyberteJiny), _Item.TypDokladu.SKL_ID), "info", MessageBoxButtons.OK);
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            Fask.Logging.ExceptionHandler2.Handle(ex);
        //                            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
        //                            return;
        //                        }
        //                    }

        //                    if (_Item.SkladZdroj == null)
        //                    {
        //                        StartAktivityNasledujici(typeof(Prodej_Sklady), ResultCode_SkladyZdroj); ;

        //                        //using (FormSkladVyber fsv = new FormSkladVyber())
        //                        //{
        //                        //    if (fsv.ShowDialog() == DialogResult.Cancel)
        //                        //        return;

        //                        //    skladZdroj = fsv.Sklad;

        //                        //    if (skladZdroj == null)
        //                        //    {
        //                        //        Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "Není vybrán sklad, přestože je vyžadován!");
        //                        //        return;
        //                        //    }
        //                        //}
        //                    }
        //                    else
        //                    {
        //                        stav = StavyDavky.Sklad_Cil;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                stav = StavyDavky.Sklad_Cil;
        //            }

        //            #endregion
        //        }

        //        if (stav == StavyDavky.Sklad_Cil)
        //        {
        //            #region skladCil

        //            if (_Item.SkladCil != null)
        //            {
        //                stav = StavyDavky.cfg_prevod_sklad;
        //            }
        //            else
        //            {
        //                // zadani ciloveho skladu
        //                if (_Item.TypDokladu != null && !_Item.TypDokladu.Iscfg_skl_id_destNull() && _Item.TypDokladu.cfg_skl_id_dest > 0)
        //                {
        //                    // sklad se ma prevzit ze zdrojoveho skladu
        //                    if (!_Item.TypDokladu.Iscfg_skl_id_dest_prevzitNull() && _Item.TypDokladu.cfg_skl_id_dest_prevzit > 0 && _Item.SkladZdroj != null)
        //                        _Item.SkladCil = _Item.SkladZdroj;

        //                    // predvyplneno id skladu (SKL_ID) v typu dokladu
        //                    if (_Item.SkladCil == null && !_Item.TypDokladu.Ispredvyplnit_locncodedestNull() && !string.IsNullOrEmpty(_Item.TypDokladu.predvyplnit_skl_id_dest.Trim()))
        //                    {
        //                        try
        //                        {
        //                            Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable dt_sklady = DataInfo_Static.ProdejGO_Instance.controller_sklady.GetDataBySkl_id(_Item.TypDokladu.predvyplnit_skl_id_dest.Trim());
        //                            if (dt_sklady.Count > 0)
        //                                _Item.SkladCil = dt_sklady[0];
        //                            else
        //                            {
        //                                MessageBox.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejMainSkladCilNenalezenVyberteJiny), _Item.TypDokladu.predvyplnit_skl_id_dest), "info", MessageBoxButtons.OK);
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            Fask.Logging.ExceptionHandler2.Handle(ex);
        //                            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
        //                            return;
        //                        }
        //                    }

        //                    if (_Item.SkladCil == null)
        //                    {
        //                        StartAktivityNasledujici(typeof(Prodej_Sklady), ResultCode_SkladyCil);

        //                        //using (Forms.FormSkladVyber fsv = new FormSkladVyber())
        //                        //{
        //                        //    fsv.Text = Fask.Localization.Localization.Prodej3ProdejMainVyberCilovehoSkladu;
        //                        //    if (fsv.ShowDialog() == DialogResult.Cancel)
        //                        //        return;

        //                        //    skladCil = fsv.Sklad;

        //                        //    if (skladCil == null)
        //                        //    {
        //                        //        Logging.Log.Write("Není vybrán cílový sklad, přestože je vyžadován!");
        //                        //        return;
        //                        //    }
        //                        //}
        //                    }
        //                    else
        //                    {
        //                        stav = StavyDavky.cfg_prevod_sklad;
        //                    }
        //                }
        //                else
        //                {
        //                    stav = StavyDavky.cfg_prevod_sklad;
        //                }
        //            }

        //            #endregion

        //        }

        //        if (stav == StavyDavky.cfg_prevod_sklad)
        //        {
        //            stav = StavyDavky.cfg_zakazka_id;

        //            // TODO : nechapu naco to je... dodelat
        //            #region cfg_prevod_sklad

        //            //if ((_Item.TypDokladu != null && _Item.TypDokladu.cfg_prevod_sklad > 0) || Prodej.Globals.PovolitPrevodMeziSklady)
        //            //{
        //            //    Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = this.globalObject.controller_prodej.GetData_DIH();
        //            //    DialogResult dresult = DialogResult.None;
        //            //    if (dih_dt.Count > 0)
        //            //    {
        //            //        zakazkaID = dih_dt[0].Zakazka_ID;
        //            //        skladID = dih_dt[0].SKL_ID;
        //            //        paletaID = dih_dt[0].Paleta_ID;
        //            //        dresult = MessageBoxBig.Show(Fask.Localization.Localization.Prodej3ProdejMainCilovySkladDriveZvolenPouzitStejnyDotaz, this.Text, MessageBoxButtons.YesNo, MessageBoxBigIcon.Question);
        //            //    }

        //            //    if (dresult != DialogResult.Yes)
        //            //    {
        //            //        using (FormSkladVyber fsv = new FormSkladVyber())
        //            //        {
        //            //            fsv.Text = Prodej3ProdejMainVyberteCilovySklad;  // "Vyberte cílový sklad";
        //            //            if (fsv.ShowDialog() == DialogResult.Cancel)
        //            //                return;

        //            //            if (fsv.Sklad == null)
        //            //            {
        //            //                Logging.Log.Write("Není vybrán cílový sklad, přestože je vyžadován!");
        //            //                return;
        //            //            }
        //            //            else
        //            //            {
        //            //                skladID = fsv.Sklad.skl_id;
        //            //            }
        //            //        }
        //            //    }

        //            //    if (dih_dt.Count > 0)
        //            //        this.globalObject.controller_prodej.DeleteQuery_DIH();
        //            //    this.globalObject.controller_prodej.Insert_DIH(zakazkaID, paletaID, mena == null ? "" : mena.mena_ID, skladID, this.globalObject.Davka.Value);
        //            //}

        //            #endregion

        //        }

        //        if (stav == StavyDavky.cfg_zakazka_id)
        //        {
        //            #region cfg_zakazka_id

        //            if (_Item.TypDokladu != null && _Item.TypDokladu.cfg_zakazka_id > 0)
        //            {
        //                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
        //                {
        //                    Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = conPro.GetData_DIH();

        //                    if (dih_dt.Count > 0)
        //                    {
        //                        zakazkaID = dih_dt[0].Zakazka_ID;
        //                        skladID = dih_dt[0].SKL_ID;
        //                    }

        //                    SejmiKodForm sejmi = new SejmiKodForm();
        //                    sejmi.Popis = GetString(Resource.String.Prodej3ProdejMainZadejteCisloZakazky);
        //                    sejmi.Kod = zakazkaID;
        //                    sejmi.CodeType = Android.Text.InputTypes.ClassNumber;

        //                    if (sejmi.ShowDialog(this) == DialogResult.OK)
        //                    {

        //                        zakazkaID = sejmi.Kod;
        //                        if (dih_dt.Count > 0)
        //                        {
        //                            paletaID = dih_dt[0].Paleta_ID;
        //                            conPro.DeleteQuery_DIH();
        //                        }

        //                        conPro.Insert_DIH(zakazkaID, paletaID, _Item.Mena == null ? "" : _Item.Mena.mena_ID, skladID, _Item.CisloDavky.Value);

        //                        stav = StavyDavky.cfg_paleta_id;
        //                    }
        //                    else
        //                    {
        //                        return;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                stav = StavyDavky.cfg_paleta_id;
        //            }

        //            #endregion 
        //        }

        //        if (stav == StavyDavky.cfg_paleta_id)
        //        {
        //            #region cfg_paleta_id

        //            if (_Item.TypDokladu != null && _Item.TypDokladu.cfg_paleta_id > 0)
        //            {
        //                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
        //                {
        //                    Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = conPro.GetData_DIH();

        //                    if (dih_dt.Count > 0)
        //                    {
        //                        paletaID = dih_dt[0].Paleta_ID;
        //                        skladID = dih_dt[0].SKL_ID;
        //                    }

        //                    SejmiKodForm sejmi = new SejmiKodForm();
        //                    sejmi.Popis = GetString(Resource.String.Prodej3ProdejMainZadejteCisloPalety);
        //                    sejmi.Kod = zakazkaID;
        //                    sejmi.CodeType = Android.Text.InputTypes.ClassNumber;

        //                    if (sejmi.ShowDialog(this) == DialogResult.OK)
        //                    {
        //                        if (dih_dt.Count > 0)
        //                        {
        //                            paletaID = dih_dt[0].Paleta_ID;
        //                            conPro.DeleteQuery_DIH();
        //                        }

        //                        conPro.Insert_DIH(zakazkaID, paletaID, _Item.Mena == null ? "" : _Item.Mena.mena_ID, skladID, _Item.CisloDavky.Value);

        //                        stav = StavyDavky.SBER_DAT;
        //                    }
        //                    else
        //                    {
        //                        return;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                stav = StavyDavky.SBER_DAT;
        //            }

        //            #endregion
        //        }

        //        if (stav == StavyDavky.SBER_DAT)
        //        {
        //            StartAktivityNasledujici(
        //                typeof(Prodej_SberDat),
        //                finish: true); ;

        //            //using (ProdejList prodejlist = new ProdejList(this.globalObject.Davka.Value, odberatel, typdokladu, skladZdroj, skladCil, di_strid, mena, di_serltnum))
        //            //{
        //            //    prodejlist.Zobrazeni = ProdejList.ZobrazeniTyp.List;
        //            //    prodejlist.ShowDialog();
        //            //}
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle( Fask.Logging.LogLevel.Error,"otevriDavku:");
        //        Fask.Logging.ExceptionHandler2.Handle(ex);
        //    }

        //}

        #endregion

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            //base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == ResultCode_TypDokladu)
            {
                if (resultCode == Result.Ok)
                {
                    GetItemFromIntent(data);
                    GetUzivatelFromIntent(data);
                    //otevriDavku_ToThread(StavyDavky.SNNaDavku);
                    _tcs.SetResult(true);
                }
                else if (resultCode == Result.Canceled)
                {
                    StornoItem();
                    GetUzivatelFromIntent(data);
                    _tcs.SetResult(false);
                }
            }
            if (requestCode == ResultCode_Odberatel)
            {
                if (resultCode == Result.Ok)
                {
                    GetItemFromIntent(data);
                    GetUzivatelFromIntent(data);
                    //otevriDavku_ToThread(StavyDavky.mena_ID);
                    _tcs.SetResult(true);
                }
                else if (resultCode == Result.Canceled)
                {
                    StornoItem();
                    GetUzivatelFromIntent(data);
                    _tcs.SetResult(false);
                }
            }
            if (requestCode == ResultCode_cfg_mena_id)
            {
                if (resultCode == Result.Ok)
                {

                    //TODO prenest do te aktivity
                    //if (bezCiziMeny.HasValue && bezCiziMeny.Value)
                    //    _Item.Mena = null;
                    //else
                    //    _Item.Mena = po.SelectedMena;

                    GetItemFromIntent(data);
                    GetUzivatelFromIntent(data);

                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
                    {
                        string zakazkaID = string.Empty;
                        string paletaID = string.Empty;
                        string skladID = string.Empty;

                        Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIHDataTable dih_dt = conPro.GetData_DIH();

                        if (dih_dt.Count > 0)
                        {
                            zakazkaID = dih_dt[0].Zakazka_ID;
                            paletaID = dih_dt[0].Paleta_ID;
                            skladID = dih_dt[0].SKL_ID;
                            conPro.DeleteQuery_DIH();
                        }
                        conPro.Insert_DIH(zakazkaID, paletaID, _Item.Mena == null ? "" : _Item.Mena.mena_ID, skladID, _Item.CisloDavky.Value);
                    }

                    //otevriDavku_ToThread(StavyDavky.Sklad_Zdroj);
                    _tcs.SetResult(true);
                }
                else if (resultCode == Result.Canceled)
                {
                    StornoItem();
                    GetUzivatelFromIntent(data);
                    _tcs.SetResult(false);
                }
            }
            if (requestCode == ResultCode_SkladyZdroj)
            {
                if (resultCode == Result.Ok)
                {

                    GetItemFromIntent(data);
                    GetUzivatelFromIntent(data);
                    //otevriDavku_ToThread(StavyDavky.Sklad_Cil);
                    _tcs.SetResult(true);
                }
                else if (resultCode == Result.Canceled)
                {
                    StornoItem();
                    GetUzivatelFromIntent(data);
                    _tcs.SetResult(false);
                }
            }
            if (requestCode == ResultCode_SkladyCil)
            {
                if (resultCode == Result.Ok)
                {
                    GetItemFromIntent(data);
                    GetUzivatelFromIntent(data);
                    //otevriDavku_ToThread(StavyDavky.cfg_prevod_sklad);
                    _tcs.SetResult(true);
                }
                else if (resultCode == Result.Canceled)
                {
                    StornoItem();
                    GetUzivatelFromIntent(data);
                    _tcs.SetResult(false);
                }
            }
            if (requestCode == ResultCode_VyberPalety)
            {
                if (resultCode == Result.Ok)
                {
                    GetItemFromIntent(data);
                    GetUzivatelFromIntent(data);
                    //otevriDavku_ToThread(StavyDavky.SNNaDavku);
                    _tcs.SetResult(true);
                }
                else if (resultCode == Result.Canceled)
                {
                    StornoItem();
                    GetUzivatelFromIntent(data);
                    _tcs.SetResult(false);
                }
            }
        }

        private void GetItemFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.ProdejItem);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_ProdejItem);
            if (binderTD != null)
                _Item = ((WrapperForBinder<Prodej_Davka_Item>)binderTD).getData();
            else
                _Item = null;
        }

        private void GetUzivatelFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.User);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_User);
            Uzivatel = ((WrapperForBinder<Users>)binderTD).getData();
        }

        #endregion

        #region Pomocne metody

        /// <summary>
        /// Generovani SN.
        /// </summary>
        /// <returns>Vraci soucasny den v roce - 1 den</returns>
        private string GenerovatSN()
        {
            try
            {
                // TODO: tvorba knihovny pro generovani
                int day = DateTime.Now.DayOfYear;
                return DateTime.Now.ToString("yy") + day.ToString();
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowErrorMessage(this, ex);
            }

            return string.Empty;
        }

        

        private void StornoItem()
        {
            if (_Item.PocetRadku > 0)
                return;

            foreach (var item in mAdapter.PolozkaSeznam.mItems)
            {
                if (item.CisloDavky == _Item.CisloDavky)
                {
                    item.cfg_lok_mech = null;
                    item.Mena = null;
                    item.Odberatel = null;
                    item.SkladCil = null;
                    item.SkladZdroj = null;
                    item.Stredisko = null;
                    item.TypDokladu = null;

                    _Item = null;

                    break;

                }
                else
                    continue;
            }


            

        }


        private async void CiselniZbozi_Dotaz()
        {
            try
            {
                var dr =  await MessageBoxAsync.Show(this, Resources.GetString(Resource.String.Prodej3ProdejMainAktualizaceCiselnikuZboziDotaz), "Otázka", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    Ciselniky_Helper _Helper = new Ciselniky_Helper();
                    bool state = await _Helper.SynchronizeCiselniky(Operation.Zbozi, this, Konfigurace_Singleton.Instance.Prodej.SkladID);

                    if (!state)
                    {
                        await MessageBoxAsync.Show(this, "NEco je špatně...", "error", MessageBoxButtons.OK);
                    }
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private async void Ciselnik(Operation typ)
        {
            try
            {

                Ciselniky_Helper _Helper = new Ciselniky_Helper();
                bool state = await _Helper.SynchronizeCiselniky(typ, this, Konfigurace_Singleton.Instance.Prodej.SkladID);

                if (!state)
                {
                    await MessageBoxAsync.Show(this, "Neco je špatně...", "error", MessageBoxButtons.OK);
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        #endregion
    }
}