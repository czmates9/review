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
    [Activity(Label = "@string/OperaceVyber_Label")]
    public class OperaceVyber : Scanner_Activity, NavigationView.IOnNavigationItemSelectedListener
    {

        #region Parametry

        Users Uzivatel = null;

        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;

        OperaceVyber_Item_Adapter mAdapter;

        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;
        private NavigationView mNavigationView;

        private Android.Support.V7.Widget.SearchView _searchView;

        Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable Operace;

        #endregion

    protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                this.DataWedge_Scanner_Disable();
                SetContentView(Resource.Layout.OperaceVyber);
                Dexter.WithActivity(this)
                   .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                   Manifest.Permission.WriteExternalStorage,
                                   Manifest.Permission.AccessNetworkState,
                                   Manifest.Permission.Camera)
                   .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                   .WithErrorListener(new SampleErrorListner())
                   .Check();

                 GetUzivatelFromIntent(Intent);
                GetOperaciFromIntent(Intent);

                mRecycleView = FindViewById<RecyclerView>(Resource.Id.OperaceVyber_recyclerView);
                mLayoutManager = new LinearLayoutManager(this);
                mRecycleView.SetLayoutManager(mLayoutManager);

                InitAdapter();

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.OperaceVyber_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.OperaceVyber_drawer_layout);

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

                mNavigationView = FindViewById<NavigationView>(Resource.Id.OperaceVyber_nav_view);
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

                //Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable dtVPH = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable();
                //dtVPH = MES_Android.Classes.DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetData_CZPRO_VPP();

                if (Operace != null && Operace.Count == 1)
                {
                    var sop = Operace[0];
                    StartAktivityNasledujici(typeof(OdvadeniVyroby_SberDat), sop);
                }

                mAdapter = new OperaceVyber_Item_Adapter(this, new OperaceVyber_Seznam(Operace));
                mAdapter.ItemClick += MAdapter_ItemClick;
                mAdapter.ItemLongClick += MAdapter_ItemLongClick;
                mRecycleView.SetAdapter(mAdapter);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        #region ClickEventy

        private void MAdapter_ItemClick(object sender, int e)
        {
            try
            {
                var x = mAdapter.PolozkaSeznam.mItems[e];

                StartAktivityNasledujici(typeof(OdvadeniVyroby_SberDat), x);
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
                //HlavneMenu();
            }
        }

        #endregion

        #region Bočne menu

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            
            if (id == Resource.Id.nav_HlMenu_ProOperaceVyber)
            {
                StartAktivityNasledujici(typeof(OdvadeniVyroby_SberDat), null);
            }
            mDrawerLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }

        #endregion

        #region Vyhledavani

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.OperaceVyber_Search, menu);

            var item = menu.FindItem(Resource.Id.OperaceVyber_search);

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

        private void GetUzivatelFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.User);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_User);
            Uzivatel = ((WrapperForBinder<Users>)binderTD).getData();
        }

        private void GetOperaciFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.OV_dt_VPP);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_dt_VPP);
            Operace = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable>)binderTD).getData();
        }

        private void StartAktivityNasledujici(Type typAktivity, Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow RowVPP)
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);
            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if (RowVPP != null)
            {
                Bundle bundleUserM = new Bundle();
                bundleUserM.PutBinder(DataInfo_Static.object_OV_VPPRow, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow>(RowVPP));
                intent.PutExtra(DataInfo_Static.OV_VPPRow, bundleUserM);
            }

            if (RowVPP == null)
                this.SetResult(Result.Canceled, intent);
            else
                this.SetResult(Result.Ok, intent);

            //this.StartActivity(intent);
            this.Finish();
        }


        override async protected void ScannerData(ScannerEventArgs e)
        {
            if (e.Data.Trim().Length == 0)
                return;

            string CKout = e.Data.Trim();

            var prikazu = NajdiPrikaz(CKout);
            if (prikazu.status)
            {
                StartAktivityNasledujici(typeof(OdvadeniVyroby_SberDat), prikazu.Row);
            }
            else
            {
                this.RunOnUiThread(() =>
                {
                    MessageBox(CKout);
                });
            }
        }

        private async void MessageBox(string CKout)
        {
            await MessageBoxAsync.Show(this, "Příkaz číslo:'" + CKout + "' nenalezen", "Warning", MessageBoxButtons.OK);
        }


        private (Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow Row, bool status) NajdiPrikaz(string bcode)
        {
            Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow row = null;
            bool status = false;

            foreach (var item in mAdapter.PolozkaSeznam.mItems)
            {
                if (item.SOPNUMBE.Trim() == bcode)
                {
                    row = item;
                    status = true;
                    break;
                }
            }

            return (row, status);
        }
    }
}
