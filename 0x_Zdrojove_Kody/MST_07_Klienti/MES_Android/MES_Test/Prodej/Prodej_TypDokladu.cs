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
using Fask.SQLiteDBs.DataSets;
using MES_Android._WebReferences_Globals;
using MES_Android.Ciselniky;
using MES_Android.Classes;
using MES_Android.Listner;
using MES_Android.ServerAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace MES_Android.Prodej
{
    [Activity(Label = "@string/Prodej_TypDokladu_Label", Theme = "@style/AppTheme")]
    public class Prodej_TypDokladu : Base_Aktivita, NavigationView.IOnNavigationItemSelectedListener, ICiselniky_Datum
    {

        #region Parametry

        Users Uzivatel = null;
        Prodej.Prodej_Davka_Item _Item = null;

        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;

        Prodej_TypDokladu_Item_Adapter mAdapter;

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
                SetContentView(Resource.Layout.Prodej_TypDokladu);

                this.DataWedge_Scanner_Disable();

                Dexter.WithActivity(this)
                    .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                    Manifest.Permission.WriteExternalStorage,
                                    Manifest.Permission.AccessNetworkState,
                                    Manifest.Permission.Camera)
                    .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                    .WithErrorListener(new SampleErrorListner())
                    .Check();

                var bundleU = Intent?.GetBundleExtra(DataInfo_Static.User);
                var binderU = bundleU?.GetBinder(DataInfo_Static.object_User);
                if (binderU != null)
                {
                    Uzivatel = ((WrapperForBinder<Users>)binderU).getData();
                }

                var bundleTD = Intent?.GetBundleExtra(DataInfo_Static.ProdejItem);
                var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_ProdejItem);
                if (binderTD != null)
                {
                    _Item = ((WrapperForBinder<Prodej.Prodej_Davka_Item>)binderTD).getData();
                }

                mRecycleView = FindViewById<RecyclerView>(Resource.Id.Prodej_TypDokladu_recyclerView);
                mLayoutManager = new LinearLayoutManager(this);
                mRecycleView.SetLayoutManager(mLayoutManager);

                if (!System.IO.File.Exists(DataInfo_Static.CiselnikTypDokladuDB))
                {
                    ShowMessage(this, "Zaktualizuj si číselnik typu dokladu!", "Error");
                    StartAktivityNasledujici(typeof(Prodej_Davky), null);
                    return;
                }

                InitAdapter();

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Prodej_TypDokladu_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Prodej_TypDokladu_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.Prodej_TypDokladu_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);


                SetDateToMenu(Resource.String.TypyDokladu, DataInfo_Static.CiselnikTypDokladuDB);

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

                Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092DataTable dttypDokladu = new Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092DataTable();

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_TypDokladu conTD = new Fask.SQLiteDBs.Controllers.SQLite_Controller_TypDokladu(DataInfo_Static.CiselnikTypDokladuDB))
                {
                    conTD.Fill(dttypDokladu);
                }

                if (dttypDokladu.Count == 1)
                {
                    _Item.TypDokladu = dttypDokladu[0];
                    StartAktivityNasledujici(typeof(Prodej_Davky), _Item);
                }

                mAdapter = new Prodej_TypDokladu_Item_Adapter(this, new Prodej_TypDokladu_Seznam(dttypDokladu));
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

        public void SetDateToMenu( int ID_String, string FileName)
        {
            int cnt = mNavigationView.HeaderCount;

            if (cnt == 1)
            {
                int ID_Menu = 0;
                
                if (ID_String == Resource.String.TypyDokladu)
                    ID_Menu = Resource.Id.nav_Ciselnik_ProTypDok;

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

        private void StartAktivityNasledujici(Type typAktivity, Prodej_Davka_Item _Item)
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);

            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if (_Item != null)
            {
                Bundle bundle = new Bundle();
                bundle.PutBinder(DataInfo_Static.object_ProdejItem, new WrapperForBinder<Prodej.Prodej_Davka_Item>(_Item));
                intent.PutExtra(DataInfo_Static.ProdejItem, bundle);
                this.SetResult(Result.Ok, intent);
                this.Finish();
            }
            else
            {
                this.SetResult(Result.Canceled, intent);
                this.Finish();
            }
        }

        #region Metory pro Permision

        //internal void ShowRequestPermissionRationale(IPermissionToken token)
        //{


        //    new Android.Support.V7.App.AlertDialog.Builder(this)
        //        .SetTitle(this.Resources.GetString(Resource.String.Title))
        //        .SetMessage(this.Resources.GetString(Resource.String.Message))
        //        .SetNegativeButton(Android.Resource.String.Cancel, delegate
        //        {
        //            token.ContinuePermissionRequest();
        //        })
        //        .SetPositiveButton(Android.Resource.String.Ok, delegate
        //        {
        //            token.ContinuePermissionRequest();
        //        })
        //        .SetOnDismissListener(new MyDismissListner(token))
        //        .Show();
        //}

        #endregion

        #region ClickEventy

        private void MAdapter_ItemClick(object sender, int e)
        {
            //string s = string.Format("pozice:'{0}'", e);
            //Toast.MakeText(this, s, ToastLength.Short).Show();

            try
            {

                //_Item.TypDoklad_ID = mAdapter.PolozkaSeznam.mItems[e].doc_id;
                //_Item.TypDoklad_ID2 = mAdapter.PolozkaSeznam.mItems[e].doc_id2;
                _Item.TypDokladu = mAdapter.PolozkaSeznam.mItems[e];

                StartAktivityNasledujici(typeof(Prodej_Davky), _Item);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

        }



        private void MAdapter_ItemLongClick(object sender, int e)
        {

        }


        #endregion

        #region Bočne menu

        public bool OnNavigationItemSelected(IMenuItem item)
        {

            CiselnikServiceSession ciselnikS = new CiselnikServiceSession();
            ciselnikS.Url = Config.Settings.Adresa + "Ciselnik.asmx";
            ciselnikS.Timeout = Config.Settings.TimeOut;
            ciselnikS.UpdateWebServiceCredentials();

            int id = item.ItemId;

            if (id == Resource.Id.nav_HlMenu_ProTypDok)
            {
                //Intent intent = new Intent(this, typeof(Prodej_Davky));
                //Bundle bundle = new Bundle();
                //bundle.PutBinder("object_User", new UsersWrapperForBinder(Uzivatel));
                //intent.PutExtra("User", bundle);
                //this.StartActivity(intent);
                //this.Finish();

                StartAktivityNasledujici(typeof(Prodej_Davky), null);

            }
            else if (id == Resource.Id.nav_Ciselnik_ProTypDok)
            {
                CiselnikTypuDokladu();
                //Ciselniky.Ciselniky_Helper _Helper = new Ciselniky_Helper();
                //_Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.TypDokladu, this, Konfigurace_Singleton.Instance.Prodej.SkladID);

                //CiselnikServiceOperations.KatalogTypDokladu(this, SupportFragmentManager.BeginTransaction(), ciselnikS, SkladFilter);
            }

            mDrawerLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }


        #endregion

        #region Vyhledavani

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Prodej_TypDokladu_Search, menu);

            var item = menu.FindItem(Resource.Id.ProdejTypDokladu_search);

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
                StartAktivityNasledujici(typeof(Prodej_Davky), null);
            }

        }

        #endregion

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.F1)
            {
                CiselnikTypuDokladu();
            }
            else if (keyCode == Keycode.Escape)
            {
                StartAktivityNasledujici(typeof(Prodej_Davky), null);
            }

            return base.OnKeyDown(keyCode,e);
            
        }

        private async void CiselnikTypuDokladu()
        {
            try
            {

                Ciselniky_Helper _Helper = new Ciselniky_Helper();
                bool state = await _Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.TypDokladu, this, Konfigurace_Singleton.Instance.Prodej.SkladID);

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