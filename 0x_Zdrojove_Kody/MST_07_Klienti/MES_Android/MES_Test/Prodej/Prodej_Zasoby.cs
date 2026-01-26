using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Karumi.Dexter;
using Com.Karumi.Dexter.Listener.Multi;
using MES_Android._WebReferences_Globals;
using MES_Android.Classes;
using MES_Android.Listner;
using MES_Android.ServerAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MES_Android.Ciselniky;
using System.IO;

namespace MES_Android.Prodej
{
    [Activity(Label = "@string/Prodej_Zasoby_Label", Theme = "@style/AppTheme")]
    public class Prodej_Zasoby : Base_Aktivita, NavigationView.IOnNavigationItemSelectedListener, ICiselniky_Datum
    {
        #region Parametry

        
        private Users Uzivatel = null;
        //Prodej.Prodej_Davka_Item _Item = null;
        private string SKL_ID = null;
        Fask.SQLiteDBs.DataSets.Zbozi zbozi = null;

        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;

        Prodej_Zasoby_Item_Adapter mAdapter;

        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;
        private NavigationView mNavigationView;

        private Android.Support.V7.Widget.SearchView _searchView;


        #endregion


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                SetContentView(Resource.Layout.Prodej_Zasoby);

                Dexter.WithActivity(this)
                    .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                    Manifest.Permission.WriteExternalStorage,
                                    Manifest.Permission.AccessNetworkState,
                                    Manifest.Permission.Camera)
                    .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                    .WithErrorListener(new SampleErrorListner())
                    .Check();


                mNavigationView = FindViewById<NavigationView>(Resource.Id.Prodej_Zasoby_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);



                var bundleU = Intent?.GetBundleExtra(DataInfo_Static.User);
                var binderU = bundleU?.GetBinder(DataInfo_Static.object_User);
                if (binderU != null)
                {
                    Uzivatel = ((WrapperForBinder<Users>)binderU).getData();
                }

                var bundleZ = Intent?.GetBundleExtra(DataInfo_Static.DT_095);
                var binderZ = bundleZ?.GetBinder(DataInfo_Static.object_DT_095);
                if (binderZ != null)
                {
                    zbozi = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Zbozi>)binderZ).getData();
                }

                SKL_ID = Intent?.GetStringExtra(DataInfo_Static.Sklad_Zasoby);

                //var bundleTD = Intent?.GetBundleExtra("ProdejItem");
                //var binderTD = bundleTD?.GetBinder("object_ProdejItem");
                //if (binderTD != null)
                //{
                //    _Item = ((Classes.ProdejItemWrapperForBinder)binderTD).getData();
                //}

                mRecycleView = FindViewById<RecyclerView>(Resource.Id.Prodej_Zasoby_recyclerView);
                mLayoutManager = new LinearLayoutManager(this);
                mRecycleView.SetLayoutManager(mLayoutManager);

                if (zbozi == null)
                    InitAdapter();
                else
                {
                    DisableMenu(Resource.Id.nav_Ciselnik_proZasoby);
                    InitAdapterZbozi();
                }

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Prodej_Zasoby_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Prodej_Zasoby_drawer_layout);
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


                SetDateToMenu(Resource.String.Zasoby, DataInfo_Static.CiselnikZboziDB);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowErrorMessage(this, ex);
            }
        }


        private void InitAdapter()
        {
            try
            {

                Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable dtZB = new Fask.SQLiteDBs.DataSets.Zbozi.CZMST095DataTable();

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi conZB = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(DataInfo_Static.CiselnikZboziDB))
                {
                    if (string.IsNullOrEmpty(SKL_ID))
                        conZB.Fill(dtZB);
                    else
                        conZB.Fill_BySKL_ID(dtZB, SKL_ID);
                }

                mAdapter = new Prodej_Zasoby_Item_Adapter(this, new Prodej_Zasoby_Seznam(dtZB));
                mAdapter.ItemClick += MAdapter_ItemClick;
                mAdapter.ItemLongClick += MAdapter_ItemLongClick;
                mRecycleView.SetAdapter(mAdapter);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void InitAdapterZbozi()
        {
            try
            {
                mAdapter = new Prodej_Zasoby_Item_Adapter(this, new Prodej_Zasoby_Seznam(zbozi.CZMST095));
                mAdapter.ItemClick += MAdapter_ItemClick;
                mAdapter.ItemLongClick += MAdapter_ItemLongClick;
                mRecycleView.SetAdapter(mAdapter);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        #region Metoda pro nastaveni datumu aktualizace do bočního menu

        public void DisableMenu(int ID_Menu)
        {
            int cnt = mNavigationView.HeaderCount;

            if (cnt == 1)
            {
                var menu = mNavigationView.Menu;
                var txt = menu.FindItem(ID_Menu);

                RunOnUiThread(() =>
                {
                    txt.SetEnabled(false);
                });
            }

        }


        public void SetDateToMenu(int ID_String, string FileName)
        {
            int cnt = mNavigationView.HeaderCount;

            if (cnt == 1)
            {
                int ID_Menu = 0;

                if (ID_String == Resource.String.Zasoby)
                    ID_Menu = Resource.Id.nav_Ciselnik_proZasoby;

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

                RunOnUiThread(() =>
                {
                    txt.SetTitle(msg);
                });
            }
        }

        #endregion

        //private void StartAktivityNasledujici(Type typAktivity, Prodej_Davka_Item _Item)
        //{
        //    //Button_Click(this.Resources.GetString(Resource.String.Prodej));
        //    Intent intent = new Intent(this, typAktivity);

        //    //if (_Item != null)
        //    //{
        //    //    Bundle bundle = new Bundle();
        //    //    bundle.PutBinder("object_ProdejItem", new ProdejItemWrapperForBinder(_Item));
        //    //    intent.PutExtra("ProdejItem", bundle);
        //    //}

        //    Bundle bundleUser = new Bundle();
        //    bundleUser.PutBinder(DataInfo_Static.object_User, new UsersWrapperForBinder(Uzivatel));
        //    intent.PutExtra(DataInfo_Static.User, bundleUser);

        //    this.SetResult(Result.Ok, intent);
        //    //this.StartActivity(intent);
        //    this.Finish();
        //}

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

        #region ClickEventy

        private void MAdapter_ItemClick(object sender, int e)
        {
            try
            {
                var x = mAdapter.PolozkaSeznam.mItems[e];
                StartAktivityNasledujici(typeof(Prodej_SberDat), x);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

        }



        private void MAdapter_ItemLongClick(object sender, int e)
        {
            Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row x = mAdapter.PolozkaSeznam.mItems[e];
            Perform_Detail(x);
        }


        #endregion

        #region Dialog detailu

        private void Perform_Detail(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row row)
        {
            try
            {

                Android.Support.V4.App.FragmentTransaction transaction = SupportFragmentManager.BeginTransaction();
                MES_Android.Classes.DetailZasoby_Dialog.Dialog_DetailZasoby dialog = new MES_Android.Classes.DetailZasoby_Dialog.Dialog_DetailZasoby();
                dialog.Row = row;
                dialog.Show(transaction, "Dialog Fragment");

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                ShowMessage(this, "Chybí soubor uživatelů", "Error");
            }
        }

        #endregion

        #region Bočne menu

        public bool OnNavigationItemSelected(IMenuItem item)
        {

            //CiselnikServiceSession ciselnikS = new CiselnikServiceSession();
            //ciselnikS.Url = Config.Settings.Adresa + "Ciselnik.asmx";
            //ciselnikS.Timeout = Config.Settings.TimeOut;
            //ciselnikS.UpdateWebServiceCredentials();

            //string SkladFilter = string.Empty;

            int id = item.ItemId;

            if (id == Resource.Id.nav_HlMenu_ProZasoby)
            {
                StartAktivityNasledujici(typeof(Prodej_Davky), null);
            }
            else if (id == Resource.Id.nav_Ciselnik_proZasoby)
            {
                CiselnikZasob();
            }

            mDrawerLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }


        #endregion

        #region Vyhledavani

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Prodej_Zasoby_Search, menu);

            var item = menu.FindItem(Resource.Id.ProdejZasoby_search);

            var searchView = item.ActionView; // IMenuItem obsahuje rovno objekt view :D

            _searchView = searchView.JavaCast<Android.Support.V7.Widget.SearchView>();

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
                StartAktivityNasledujici(typeof(Prodej_SberDat), null);
            }

        }

        #endregion

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {

            if (keyCode == Keycode.F1)
            {
                CiselnikZasob();
            }

            if (keyCode == Keycode.Escape)
            {
                StartAktivityNasledujici(typeof(Prodej_SberDat), null);
            }

            return base.OnKeyDown(keyCode, e);
        }

        private void StartAktivityNasledujici(Type typAktivity, Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row Z)
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);

            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if (Z != null)
            {
                Bundle bundleRow = new Bundle();
                bundleRow.PutBinder(DataInfo_Static.object_Row095, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row>(Z));
                intent.PutExtra(DataInfo_Static.Row095, bundleRow);
                this.SetResult(Result.Ok, intent);
                this.Finish();
            }
            else
            {
                this.SetResult(Result.Canceled, intent);
                this.Finish();
            }
        }

        private async void CiselnikZasob()
        {
            try
            {

                Ciselniky_Helper _Helper = new Ciselniky_Helper();
                bool state = await _Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Zbozi, this, SKL_ID);

                if (state)
                {
                    UpdateUI(this);
                }
                else
                {
                    await MessageBoxAsync.Show(this, "NEco je špatně...", "error", MessageBoxButtons.OK);
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void UpdateUI(AppCompatActivity parent)
        {
            parent.RunOnUiThread(() =>
            {
                InitAdapter();
            });
        }

    }
}