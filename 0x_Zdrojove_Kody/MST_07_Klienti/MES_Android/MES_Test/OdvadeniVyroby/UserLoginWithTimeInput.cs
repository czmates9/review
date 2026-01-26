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
using MES_Android.Extensions;
using MES_Android.Listner;
using MES_Android.ServerAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_Android.OdvadeniVyroby
{
    [Activity(Label = "@string/UserLoginWithTimeInput_Label", Theme = "@style/AppTheme")]
    public class UserLoginWithTimeInput : Base_Aktivita, NavigationView.IOnNavigationItemSelectedListener
    {
        #region Parametry

        Users Uzivatel = null;

        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;
        private NavigationView mNavigationView;
        #endregion

        DateTime? datum = null;
        Button _dateSelectButton;
        TextView _dateDisplay;

        Button _timeSelectButton;
        TextView _timeDisplay;

        Button _userLoginWithTimeInput_btn_Storno;
        Button _userLoginWithTimeInput_btn_OK;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            try
            {
                SetContentView(Resource.Layout.UserLoginWithTimeInput);

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

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.UserLoginWithTimeInput_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.UserLoginWithTimeInput_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.UserLoginWithTimeInput_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);


                _userLoginWithTimeInput_btn_OK = FindViewById<Button>(Resource.Id.UserLoginWithTimeInput_btn_OK);
                _userLoginWithTimeInput_btn_OK.Click += UserLoginWithTimeInput_btn_OK_OnClick;
                _userLoginWithTimeInput_btn_Storno = FindViewById<Button>(Resource.Id.UserLoginWithTimeInput_btn_Storno);
                _userLoginWithTimeInput_btn_Storno.Click += UserLoginWithTimeInput_btn_Storno_OnClick;


                _dateDisplay = FindViewById<TextView>(Resource.Id.UserLoginWithTimeInput_txt_Date);
                _dateSelectButton = FindViewById<Button>(Resource.Id.UserLoginWithTimeInput_btn_Date);
                _dateSelectButton.Click += DateSelect_OnClick;

                _timeDisplay = FindViewById<TextView>(Resource.Id.UserLoginWithTimeInput_txt_Time);
                _timeSelectButton = FindViewById<Button>(Resource.Id.UserLoginWithTimeInput_btn_Time);
                _timeSelectButton.Click += TimeSelect_OnClick;


                _userLoginWithTimeInput_btn_OK.FocusChange += Btn_FocusChange;
                _userLoginWithTimeInput_btn_Storno.FocusChange += Btn_FocusChange;
                _dateSelectButton.FocusChange += Btn_FocusChange;
                _timeSelectButton.FocusChange += Btn_FocusChange;

                _userLoginWithTimeInput_btn_OK.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                _userLoginWithTimeInput_btn_Storno.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                _dateSelectButton.SetBackgroundResource(Resource.Drawable.focus_button);
                _timeSelectButton.SetBackgroundResource(Resource.Drawable.lost_focus_button);

                datum = new DateTime();

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowErrorMessage(this, ex);
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

        private void UserLoginWithTimeInput_btn_OK_OnClick(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void UserLoginWithTimeInput_btn_Storno_OnClick(object sender, EventArgs e)
        {
            StartAktivityNasledujici(typeof(OdvadeniVyroby), null);
        }

        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _dateDisplay.Text = time.ToLongDateString();
                datum = datum.Value.ChangeTime(Year: time.Year);
                datum = datum.Value.ChangeTime(Month: time.Month);
                datum = datum.Value.ChangeTime(Day: time.Day);
            });

            frag.Show(SupportFragmentManager, DatePickerFragment.TAG);
        }

        void TimeSelect_OnClick(object sender, EventArgs eventArgs)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (DateTime time)
            {
                _timeDisplay.Text = time.ToLongTimeString();
                datum = datum.Value.ChangeTime(Hour: time.Hour);
                datum = datum.Value.ChangeTime(Minute: time.Minute);
            });

            frag.Show(SupportFragmentManager, DatePickerFragment.TAG);
        }


        private void StartAktivityNasledujici(Type typAktivity, DateTime? dat)
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);

            if (dat.HasValue)
                intent.PutExtra(DataInfo_Static.OV_DateTime_UserLoginWithTimeInput, dat.Value.ToString());

            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if(dat.HasValue)
                this.SetResult(Result.Ok, intent);
            else
                this.SetResult(Result.Canceled, intent);

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


        #region Bočne menu

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_UserLoginWithTimeInput_OK)
            {
                PerformOK();
            }
            else if (id == Resource.Id.nav_UserLoginWithTimeInput_Storno)
            {
                StartAktivityNasledujici(typeof(OdvadeniVyroby), null);
            }

            mDrawerLayout.CloseDrawer(GravityCompat.Start);
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
                //StartAktivityNasledujici(typeof(OdvadeniVyroby), datum);
            }

        }

        #endregion

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {

            if (keyCode == Keycode.Escape)
            {
                StartAktivityNasledujici(typeof(OdvadeniVyroby), null);
            }
            if (keyCode == Keycode.Enter)
            {
                PerformOK();
            }

            return base.OnKeyDown(keyCode, e);
        }



        private async void PerformOK()
        {
            if (_dateDisplay.Text == "-")
            {
                await MessageBoxAsync.Show(this, "Zadej datum!", "Error", MessageBoxButtons.OK);
                return;
            }

            if (_timeDisplay.Text == "-")
            {
                await MessageBoxAsync.Show(this, "Zadej čas!", "Error", MessageBoxButtons.OK);
                return;
            }

            StartAktivityNasledujici(typeof(OdvadeniVyroby), datum);
        }




    }
}