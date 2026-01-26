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
using Fask.Interfaces;
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
    [Activity(Label = "@string/InputQuantity_Label", Theme = "@style/AppTheme")]
    public class InputQuantity : Scanner_Activity , NavigationView.IOnNavigationItemSelectedListener
    {
        #region Parametry

        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;

        public Users Uzivatel = null;

        NavigationView mNavigationView;

        private Button btn_OK;
        private Button btn_Storno;
        public EditText textBoxInputQuantity;

        private TextView InputQuantity_txt_QTY;

        private string Kod;
        private string Text;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                this.DataWedge_Scanner_Disable();

                SetContentView(Resource.Layout.InputQuantity);

                Dexter.WithActivity(this)
                   .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                   Manifest.Permission.WriteExternalStorage,
                                   Manifest.Permission.AccessNetworkState,
                                   Manifest.Permission.Camera)
                   .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                   .WithErrorListener(new SampleErrorListner())
                   .Check();

                GetUzivatelFromIntent(Intent);

                Kod = Intent?.GetStringExtra(DataInfo_Static.OV_QTY_Kod);
                Text = Intent?.GetStringExtra(DataInfo_Static.OV_QTY_Text);

                textBoxInputQuantity = FindViewById<EditText>(Resource.Id.InputQuantity_edittxt_QTY);
                InputQuantity_txt_QTY = FindViewById<TextView>(Resource.Id.InputQuantity_txt_QTY);

                

                btn_OK = FindViewById<Button>(Resource.Id.InputQuantity_btn_OK);
                btn_Storno = FindViewById<Button>(Resource.Id.InputQuantity_btn_Storno);


                btn_OK.Click += Btn_OK_Click;
                btn_Storno.Click += Btn_Storno_Click;

                textBoxInputQuantity.FocusChange += Edittext_Test_FocusChange;

                btn_OK.FocusChange += Btn_FocusChange;
                btn_Storno.FocusChange += Btn_FocusChange;

                textBoxInputQuantity.ShowSoftInputOnFocus = false;

                textBoxInputQuantity.SetBackgroundResource(Resource.Drawable.lost_focus_style);

                btn_OK.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btn_Storno.SetBackgroundResource(Resource.Drawable.lost_focus_button);

                textBoxInputQuantity.Text = Kod;

                textBoxInputQuantity.RequestFocus();
                textBoxInputQuantity.SelectAll();
                btn_OK.Selected = false;
                btn_Storno.Selected = false;


                

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.InputQuantity_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.InputQuantity_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.InputQuantity_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowErrorMessage(this, ex);
            }
        }

        #region Focus eventy

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

        private void Edittext_Test_FocusChange(object sender, View.FocusChangeEventArgs e)
        {

            if (e.HasFocus)
            {
                ((EditText)sender).HideKeyboard();
                ((EditText)sender).SetBackgroundResource(Resource.Drawable.focus_border_style);
            }
            else
            {
                ((EditText)sender).SetBackgroundResource(Resource.Drawable.lost_focus_style);
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
                //HlavneMenu();
            }

        }

        #endregion

        #region Bočne menu

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_InputQuantity_OK)
            {
                PerformOK();
            }
            else if (id == Resource.Id.nav_InputQuantity_Storno)
            {
                StartAktivityNasledujici(typeof(OdvadeniVyroby), null);
            }

            mDrawerLayout.CloseDrawer(GravityCompat.Start);
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

        #region Click events to button

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void Btn_Storno_Click(object sender, EventArgs e)
        {
            StartAktivityNasledujici(typeof(OdvadeniVyroby), null);
        }

        #endregion

        private void GetUzivatelFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.User);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_User);
            Uzivatel = ((WrapperForBinder<Users>)binderTD).getData();
        }

        private void StartAktivityNasledujici(Type typAktivity, string kod)
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);


            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if(!string.IsNullOrEmpty(kod))
                intent.PutExtra(DataInfo_Static.OV_Kod, kod);



            if (string.IsNullOrEmpty(kod))
                this.SetResult(Result.Canceled, intent);
            else
                this.SetResult(Result.Ok, intent);

            //this.StartActivity(intent);
            this.Finish();
        }

        private async void PerformOK()
        {
            try
            {

                if (this.textBoxInputQuantity.Text.Trim().Length == 0)
                {
                    await MessageBoxAsync.Show(this, "Musíte zadat hodnotu!", this.Text, MessageBoxButtons.OK);
                    textBoxID_Activate();
                    return;
                }


                StartAktivityNasledujici(typeof(OdvadeniVyroby), textBoxInputQuantity.Text);
            }
            catch (Exception ex)
            {
                await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                textBoxID_Activate();
                return;
            }

        }

        private void textBoxID_Activate()
        {
            textBoxInputQuantity.RequestFocus();
            textBoxInputQuantity.SelectAll();
        }

        override protected void ScannerData(ScannerEventArgs e)
        {
            if (e.Data.Trim().Length == 0)
                return;

            string CKout = e.Data.Trim();

            this.textBoxInputQuantity.Text = CKout;
            PerformOK();
        }
    }
}