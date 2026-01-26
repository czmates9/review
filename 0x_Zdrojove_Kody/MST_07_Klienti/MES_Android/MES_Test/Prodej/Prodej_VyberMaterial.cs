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
using MES_Android.ProdejService;
using MES_Android.ServerAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android.Prodej
{
    [Activity(Label = "@string/Prodej_VyberMaterial", Theme = "@style/AppTheme")]
    public class Prodej_VyberMaterial : Base_Aktivita, NavigationView.IOnNavigationItemSelectedListener
    {
        #region Parametry

        Users Uzivatel = null;
        //Prodej.Prodej_Davka_Item _Item = null;

        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;

        Prodej_VyberMaterial_Item_Adapter mAdapter;

        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;
        private NavigationView mNavigationView;

        private Android.Support.V7.Widget.SearchView _searchView;

        Location dsVyberMaterial = null;

        #endregion


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                SetContentView(Resource.Layout.Prodej_VyberMaterialu);

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

                var bundleL = Intent?.GetBundleExtra(DataInfo_Static.Location);
                var binderL = bundleL?.GetBinder(DataInfo_Static.object_Location);
                if (binderL != null)
                {
                    dsVyberMaterial = ((WrapperForBinder<Location>)binderL).getData();
                }

                mRecycleView = FindViewById<RecyclerView>(Resource.Id.Prodej_VyberMaterial_recyclerView);
                mLayoutManager = new LinearLayoutManager(this);
                mRecycleView.SetLayoutManager(mLayoutManager);


                mAdapter = new Prodej_VyberMaterial_Item_Adapter(this, new Prodej_VyberMaterial_Seznam(dsVyberMaterial.CZMST_SkladLokace_Stav));
                mAdapter.ItemClick += MAdapter_ItemClick;
                mAdapter.ItemLongClick += MAdapter_ItemLongClick;
                mRecycleView.SetAdapter(mAdapter);

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Prodej_VyberMaterial_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Prodej_VyberMaterial_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.Prodej_VyberMaterial_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowErrorMessage(this,ex);
            }
        }


        private void StartAktivityNasledujici(Type typAktivity, Location.CZMST_SkladLokace_StavRow _Item)
        {
            Intent intent = new Intent(this, typAktivity);

            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if (_Item != null)
            {
                intent.PutExtra(DataInfo_Static.Mat_LOCNCODE, _Item.IsLOCNCODENull() ? string.Empty : _Item.LOCNCODE);
                intent.PutExtra(DataInfo_Static.Mat_SERLTNUM, _Item.SERLTNUM);
                intent.PutExtra(DataInfo_Static.Mat_QTY, _Item.QTYSHPPD.ToString());
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
               var _Item = mAdapter.PolozkaSeznam.mItems[e];

                StartAktivityNasledujici(typeof(Prodej_SberDat), _Item);
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

            if (id == Resource.Id.nav_HlMenu_ProVyberMaterialu)
            {
                StartAktivityNasledujici(typeof(Prodej_SberDat), null);
            }


            mDrawerLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }


        #endregion

        #region Vyhledavani

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Prodej_VyberMaterialu_Search, menu);

            var item = menu.FindItem(Resource.Id.ProdejVyberMaterialu_search);

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

            if (keyCode == Keycode.Escape)
            {
                StartAktivityNasledujici(typeof(Prodej_SberDat), null);
            }

            return base.OnKeyDown(keyCode, e);
        }
    }
}