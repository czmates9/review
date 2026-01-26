using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Runtime;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Content.Res;
using Android.Content.PM;

using Android.Support.V7.App;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.View;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Com.Karumi.Dexter;
using Android;
using Com.Karumi.Dexter.Listener.Multi;
using MES_Android.Listner;
using MES_Android.Classes;

namespace MES_Android.Config
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden)]
    public class Config_Main : Base_Aktivita
    {
        #region konfigurace zobrazena
        private SupportToolbar mToolbar;

        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;

        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftList;

        #endregion

        private Config_Terminal scf_term;
        private Config_Scannery scf_scan;
        private Config_HTTPS scf_https;


        Classes.Users Uzivatel = null;
        private string zdroj { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            #region Presmission

            Dexter.WithActivity(this)
                .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                Manifest.Permission.WriteExternalStorage)
                .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                .WithErrorListener(new SampleErrorListner())
                .Check();

            #endregion

            SetContentView(Resource.Layout.Config_Main);

            try
            {

                zdroj = Intent.GetStringExtra(DataInfo_Static.VolatelKonfigurace);

                var bundle = Intent?.GetBundleExtra(DataInfo_Static.User);
                var binder = bundle?.GetBinder(DataInfo_Static.object_User);
                if (binder != null)
                {
                    Uzivatel = ((Classes.WrapperForBinder<Users>)binder).getData();
                }

                #region nacteni fragmentu

                System.Collections.Generic.IDictionary<int, MyFrament> SeznamFragments = new System.Collections.Generic.Dictionary<int, MyFrament>();

                #region tvorba fragmentu

                scf_term = new Config_Terminal();
                scf_scan = new Config_Scannery();
                scf_https = new Config_HTTPS();

                #endregion

                MyFrament mf1 = new MyFrament();
                mf1.Name = "Terminal";
                mf1.Fragment = scf_term;

                MyFrament mf2 = new MyFrament();
                mf2.Name = "Skennery";
                mf2.Fragment = scf_scan;

                MyFrament mf3 = new MyFrament();
                mf3.Name = "Https";
                mf3.Fragment = scf_https;

                //MyFrament mf4 = new MyFrament();
                //mf4.Name = "Inventura";
                //mf4.Fragment = scf_inv;

                //MyFrament mf5 = new MyFrament();
                //mf5.Name = "Logovani";
                //mf5.Fragment = scf_log;

                //MyFrament mf6 = new MyFrament();
                //mf6.Name = "Ukolování";
                //mf6.Fragment = scf_ukol;

                SeznamFragments.Add(0, mf1);
                SeznamFragments.Add(1, mf2);
                SeznamFragments.Add(2, mf3);
                //SeznamFragments.Add(3, mf4);
                //SeznamFragments.Add(4, mf5);
                //SeznamFragments.Add(5, mf6);

                MyFragmentPagerAdapter adapter = new MyFragmentPagerAdapter(SupportFragmentManager, SeznamFragments);


                // Find the ViewPager and plug in the adapter:
                ViewPager pager = (ViewPager)FindViewById(Resource.Id.pager_Config_Main);
                pager.Adapter = adapter;

                #endregion

                #region MyRegion

                mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar_Config_Main);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout__Config_Main);
                mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer_Config_Main);

                SetSupportActionBar(mToolbar);

                mLeftList = new List<string>();
                mLeftList.Add("Uložit");
                mLeftList.Add("Storno");
                mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftList);
                mLeftDrawer.Adapter = mLeftAdapter;
                mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;


                mDrawerToggle = new FASK_ActionBarDrawerToggle(
                    this,
                    mDrawerLayout,
                    mToolbar,
                    Resource.String.openDrawer,
                    Resource.String.closeDrawer
                    );

                mDrawerLayout.AddDrawerListener(mDrawerToggle);
                SupportActionBar.SetHomeButtonEnabled(true);
                SupportActionBar.SetDisplayShowTitleEnabled(true);

                mDrawerToggle.SyncState();

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
            catch (Exception ex)
            {
                throw ex;
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

        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {

                switch (e.Id)
                {
                    case 0:
                        PerformOK(null, null);
                        break;
                    case 1:
                        PerformStorno(null, null);
                        break;
                    default:
                        break;
                }

                mDrawerLayout.CloseDrawers();
                mDrawerToggle.SyncState();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Performy

        public void PerformOK(object sender, EventArgs e)
        {
            Settings.Update();
            Konec();
        }

        public void PerformStorno(object sender, EventArgs e)
        {
            Konec();
        }

        private void Konec()
        {
            try
            {
                Type type = null;
                
                if (zdroj == DataInfo_Static.Activity_HlavneMenu)
                    type = typeof(Activity_HlavneMenu);
                else if (zdroj == DataInfo_Static.MainActivity)
                    type = typeof(MainActivity);
                else
                    type = typeof(MainActivity);


                Intent intent = new Intent(this, type);

                if (Uzivatel != null)
                {
                    Bundle bundle = new Bundle();
                    bundle.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
                    intent.PutExtra(DataInfo_Static.User, bundle);

                }
                this.StartActivity(intent);
                this.Finish();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }

}