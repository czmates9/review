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
using MES_Android.Ciselniky;
using MES_Android.Classes;
using MES_Android.Listner;
using MES_Android.ServerAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_Android.Prodej
{
    [Activity(Label = "@string/Prodej_Lokace_Label", Theme = "@style/AppTheme")]
    public class Prodej_Lokace : Scanner_Activity, NavigationView.IOnNavigationItemSelectedListener, ICiselniky_Datum
    {
        #region Parametry

        Users Uzivatel = null;

        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;

        Prodej_Lokace_Item_Adapter mAdapter;

        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;
        private NavigationView mNavigationView;

        private Android.Support.V7.Widget.SearchView _searchView;

        string SKL_ID = null;
        int? LokText = null;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                SetContentView(Resource.Layout.Prodej_Lokace);

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

                SKL_ID = Intent?.GetStringExtra(DataInfo_Static.Lok_SKL_ID);
                LokText = Intent?.GetIntExtra(DataInfo_Static.Lok_Text, -1);
                                

                mRecycleView = FindViewById<RecyclerView>(Resource.Id.Prodej_Lokace_recyclerView);
                mLayoutManager = new LinearLayoutManager(this);
                mRecycleView.SetLayoutManager(mLayoutManager);

                InitAdapter();

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Prodej_Lokace_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Prodej_Lokace_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.Prodej_Lokace_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);

                SetDateToMenu(Resource.String.Lokace, DataInfo_Static.CiselnikLokaceDB);

                if (LokText.HasValue)
                {
                    SupportActionBar.SetTitle(LokText.Value);
                }
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
                Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable dtLokace = new Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable();

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Lokace conLokace = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Lokace(DataInfo_Static.CiselnikLokaceDB))
                {
                    if (string.IsNullOrEmpty(SKL_ID))
                    {
                        conLokace.CZMST094_Fill(dtLokace);
                    }
                    else
                    {
                        conLokace.CZMST094_FillBySklID(dtLokace, SKL_ID);
                    }

                }

                mAdapter = new Prodej_Lokace_Item_Adapter(this, new Prodej_Lokace_Seznam(dtLokace));
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

        public void SetDateToMenu(int ID_String, string FileName)
        {
            int cnt = mNavigationView.HeaderCount;

            if (cnt == 1)
            {
                int ID_Menu = 0;

                if (ID_String == Resource.String.Lokace)
                    ID_Menu = Resource.Id.nav_Ciselnik_proLokace;

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

        private void StartAktivityNasledujici(Type typAktivity, Fask.SQLiteDBs.DataSets.Lokace.CZMST094Row lok)
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);

            if (lok != null)
                intent.PutExtra(DataInfo_Static.Lok_Lokace,lok.LOCNCODE);

            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            this.SetResult(Result.Ok, intent);
            //this.StartActivity(intent);
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
                Fask.SQLiteDBs.DataSets.Lokace.CZMST094Row lok = mAdapter.PolozkaSeznam.mItems[e];

                StartAktivityNasledujici(typeof(Prodej_Davky), lok);
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

            if (id == Resource.Id.nav_HlMenu_ProLokace)
            {
                StartAktivityNasledujici(typeof(Prodej_Davky), null);
            }
            else if (id == Resource.Id.nav_Ciselnik_proLokace)
            {
                CiselnikLokace();
            }

            mDrawerLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }


        #endregion

        #region Vyhledavani

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Prodej_Lokace_Search, menu);

            var item = menu.FindItem(Resource.Id.ProdejLokace_search);

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
                CiselnikLokace();
            }
            if (keyCode == Keycode.Escape)
            {
                StartAktivityNasledujici(typeof(Prodej_Davky), null);
            }

            return base.OnKeyDown(keyCode, e);
        }

        private async void CiselnikLokace()
        {
            try
            {

                Ciselniky_Helper _Helper = new Ciselniky_Helper();
                bool state = await _Helper.SynchronizeCiselniky(CiselnikServiceOperations.Operation.Lokace, this, Konfigurace_Singleton.Instance.Prodej.SkladID);

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


        #region Scanner

        protected override void ScannerData(Fask.Interfaces.ScannerEventArgs e)
        {
            string barcode = e.Data.Trim();
            if (barcode != string.Empty)
            {
                SkladFindByBarcode(barcode);
            }

        }


        private async Task<bool> SkladFindByBarcode(string barcode)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            Fask.SQLiteDBs.DataSets.Lokace.CZMST094Row row = null;
            try
            {
                var lok = mAdapter.PolozkaSeznam.mItems.Where(x => x.Barcode == barcode);

                if (lok.Count() <= 0)
                {
                    await MessageBoxAsync.Show(this, string.Format("Lokace '{0}' nenalezena", barcode), "Info", MessageBoxButtons.OK);
                    return tcs.Task.Result;
                }
                else
                {
                    row = lok.First();
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }

            if (row != null)
            {
                StartAktivityNasledujici(typeof(Prodej_Davky), row); 
            }

            return tcs.Task.Result;
        }

        #endregion


    }
}