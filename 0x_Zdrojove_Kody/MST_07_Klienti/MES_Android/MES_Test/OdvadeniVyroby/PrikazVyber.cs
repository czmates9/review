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
using Fask.Interfaces;
using Fask.Parsing.Codes;

namespace MES_Android.OdvadeniVyroby
{
    [Activity(Label = "@string/PrikazVyber_Label")]
    public class PrikazVyber : Scanner_Activity, NavigationView.IOnNavigationItemSelectedListener
    {


        #region Parametry

        Users Uzivatel = null;

        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;

        PrikazVyber_Item_Adapter mAdapter;

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
                SetContentView(Resource.Layout.PrikazVyber);

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


                mRecycleView = FindViewById<RecyclerView>(Resource.Id.PrikazVyber_recyclerView);
                mLayoutManager = new LinearLayoutManager(this);
                mRecycleView.SetLayoutManager(mLayoutManager);

                InitAdapter();

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.PrikazVyber_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.PrikazVyber_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.PrikazVyber_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);

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

                Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable dtVPH = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHDataTable();
                dtVPH = MES_Android.Classes.DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetData_CZPRO_VPH();

                if (dtVPH.Count == 1)
                {
                    var sop = dtVPH[0];
                    StartAktivityNasledujici(typeof(OdvadeniVyroby), sop);
                }

                mAdapter = new PrikazVyber_Item_Adapter(this, new PrikazVyber_Seznam(dtVPH));
                mAdapter.ItemClick += MAdapter_ItemClick;
                mAdapter.ItemLongClick += MAdapter_ItemLongClick;
                mRecycleView.SetAdapter(mAdapter);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void StartAktivityNasledujici(Type typAktivity, Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow _Item)
        {
            Intent intent = new Intent(this, typAktivity);
            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if (_Item != null)
            {
                Bundle bundleOV = new Bundle();
                bundleOV.PutBinder(DataInfo_Static.object_PrikazVyber_SOPNUMBE, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow>(_Item));
                intent.PutExtra(DataInfo_Static.PrikazVyber_SOPNUMBE, bundleOV);
            }

            this.StartActivity(intent);
            this.Finish();
        }

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

                StartAktivityNasledujici(typeof(OdvadeniVyroby), x);
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
            int id = item.ItemId;

            if (id == Resource.Id.nav_HlMenu_ProPrikazVyber)
            {
                StartAktivityNasledujici(typeof(Activity_HlavneMenu), null);
            }


            mDrawerLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }


        #endregion

        #region Vyhledavani

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.PrikazVyber_Search, menu);

            var item = menu.FindItem(Resource.Id.PrikazVyber_search);

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
                //StartAktivityNasledujici(typeof(Prodej_Davky), null);
            }

        }

        #endregion

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Escape)
            {
                StartAktivityNasledujici(typeof(Activity_HlavneMenu), null);
            }

            return base.OnKeyDown(keyCode, e);
        }

        private (Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow Row, bool status) NajdiPrikaz(string bcode)
        {
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow row = null;
            bool status = false;

            foreach (var item in mAdapter.PolozkaSeznam.mItems)
            {
                if(item.BarcodeH.Trim() == bcode)
                {
                    row = item;
                    status = true;
                    break;
                }
            }

            return (row, status);
        }

        override async protected void ScannerData(ScannerEventArgs e)
        {
            if (e.Data.Trim().Length == 0)
                return;

            string CKout = e.Data.Trim();

            var prikazu = NajdiPrikaz(CKout);
            if (prikazu.status)
            {
                StartAktivityNasledujici(typeof(OdvadeniVyroby), prikazu.Row);
            }
            else
            {
                await MessageBoxAsync.Show(this, "Příkaz '" + CKout + "' nenalezen", "Warning", MessageBoxButtons.OK);
            }
        }
    }
}