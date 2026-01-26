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
using MES_Android.Extensions;

namespace MES_Android
{
    [Activity(Label = "@string/InputKod2_Label")]
    public class InputKod2 : Scanner_Activity, NavigationView.IOnNavigationItemSelectedListener
    {

        #region Parametry
        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;
        public Users Uzivatel = null;
        NavigationView mNavigationView;
        //public TaskCompletionSource<Logika.OdvadeniVyroby_Odvadeni_LogikaAsync.O_Get_UserLoginWithTimeInput> TCS_GetUserLoginWithTimeInputAsync;
        //public int ResoultCode_UserLoginWithTimeInput = 123;
        private Button btn_OK;
        private Button btn_Storno;
        public EditText textBoxInputKod2;

        TextView InputKod2_txt_Text;
        TextView InputKod2_txt_lbl_Material;
        TextView InputKod2_textview_lbl_Mnozstvi;
        TextView InputKod2_textview_lbl_Sklad;
        TextView InputKod2_textview_lbl_Lokace;

        private string _nazev = null;
        private string _mnozstvi = null;
        private string _sklad = null;
        private string _lokace = null;
        private string _material = null;
        private string _kod = null;

    #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                this.DataWedge_Scanner_Disable();
                SetContentView(Resource.Layout.InputKod2);
                Dexter.WithActivity(this)
                   .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                   Manifest.Permission.WriteExternalStorage,
                                   Manifest.Permission.AccessNetworkState,
                                   Manifest.Permission.Camera)
                   .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                   .WithErrorListener(new SampleErrorListner())
                   .Check();

                 GetUzivatelFromIntent(Intent);

                _nazev = Intent.GetStringExtra(DataInfo_Static.OV_IN_InputKod2_Nazev);
                _mnozstvi = Intent.GetStringExtra(DataInfo_Static.OV_IN_InputKod2_Mnozstvi);
                _sklad = Intent.GetStringExtra(DataInfo_Static.OV_IN_InputKod2_Sklad);
                _lokace = Intent.GetStringExtra(DataInfo_Static.OV_IN_InputKod2_Lokace);
                _material = Intent.GetStringExtra(DataInfo_Static.OV_IN_InputKod2_Material);
                _kod = Intent.GetStringExtra(DataInfo_Static.OV_IN_InputKod2_Kod);


                InputKod2_txt_Text = FindViewById<TextView>(Resource.Id.InputKod2_txt_Text);
                InputKod2_txt_lbl_Material = FindViewById<TextView>(Resource.Id.InputKod2_txt_lbl_Material);
                InputKod2_textview_lbl_Mnozstvi = FindViewById<TextView>(Resource.Id.InputKod2_textview_lbl_Mnozstvi);
                InputKod2_textview_lbl_Sklad = FindViewById<TextView>(Resource.Id.InputKod2_textview_lbl_Sklad);
                InputKod2_textview_lbl_Lokace = FindViewById<TextView>(Resource.Id.InputKod2_textview_lbl_Lokace);

                textBoxInputKod2 = FindViewById<EditText>(Resource.Id.InputKod2_edittxt_Text);

                btn_OK = FindViewById<Button>(Resource.Id.InputKod2_btn_OK);
                btn_Storno = FindViewById<Button>(Resource.Id.InputKod2_btn_Storno);


                #region Fill textview + EditText

                if (!string.IsNullOrEmpty(_kod))
                    textBoxInputKod2.Text = _kod;
                else
                    textBoxInputKod2.Text = "-";

                if (!string.IsNullOrEmpty(_nazev))
                    InputKod2_txt_Text.Text = _nazev;
                else
                    InputKod2_txt_Text.Text = "-";

                if (!string.IsNullOrEmpty(_mnozstvi))
                    InputKod2_textview_lbl_Mnozstvi.Text = _mnozstvi;
                else
                    InputKod2_textview_lbl_Mnozstvi.Text = "-";

                if (!string.IsNullOrEmpty(_sklad))
                    InputKod2_textview_lbl_Sklad.Text = _sklad;
                else
                    InputKod2_textview_lbl_Sklad.Text = "-";

                if (!string.IsNullOrEmpty(_lokace))
                    InputKod2_textview_lbl_Lokace.Text = _lokace;
                else
                    InputKod2_textview_lbl_Lokace.Text = "-";

                if (!string.IsNullOrEmpty(_material))
                    InputKod2_txt_lbl_Material.Text = _material;
                else
                    InputKod2_txt_lbl_Material.Text = "-";



                #endregion


                btn_OK.Click += Btn_OK_Click;
                btn_Storno.Click += Btn_Storno_Click;

                textBoxInputKod2.FocusChange += EditText_FocusChange;

                btn_OK.FocusChange += Btn_FocusChange;
                btn_Storno.FocusChange += Btn_FocusChange;

                textBoxInputKod2.ShowSoftInputOnFocus = false;

                textBoxInputKod2.SetBackgroundResource(Resource.Drawable.lost_focus_style);
                btn_OK.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btn_Storno.SetBackgroundResource(Resource.Drawable.lost_focus_button);

                textBoxInputKod2.RequestFocus();

                btn_OK.Selected = false;
                btn_Storno.Selected = false;

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.InputKod2_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.InputKod2_drawer_layout);

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
                mNavigationView = FindViewById<NavigationView>(Resource.Id.InputKod2_nav_view);
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
        private void EditText_FocusChange(object sender, View.FocusChangeEventArgs e)
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
            if (id == Resource.Id.nav_InputKod2_OK)
            {
                PerformOK();
            }
            else if (id == Resource.Id.nav_InputKod2_Storno)
            {
                StartAktivityNasledujici(typeof(MainActivity), null);
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
            StartAktivityNasledujici(typeof(MainActivity), null);
        }
        #endregion

        private void GetUzivatelFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.User);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_User);
            Uzivatel = ((WrapperForBinder<Users>)binderTD).getData();
        }

        private void StartAktivityNasledujici(Type typAktivity, string ID)
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);
            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);
            //if(ID != null)
            //{
            //    Bundle bundleUserM = new Bundle();
            //    bundleUserM.PutBinder(DataInfo_Static.object_OV_IDMachine, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow>(ID));
            //    intent.PutExtra(DataInfo_Static.OV_IDMachine, bundleUserM);
            //}
            if(ID == null)
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
            string text = string.Empty;
                StartAktivityNasledujici(typeof(MainActivity), text);
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
            textBoxInputKod2.SelectAll();
            textBoxInputKod2.RequestFocus();
        }

        override protected void ScannerData(ScannerEventArgs e)
        {
            if (e.Data.Trim().Length == 0)
                return;
            string CKout = e.Data.Trim();
            this.textBoxInputKod2.Text = CKout;
            PerformOK();
        }
    }
}
