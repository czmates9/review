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
    [Activity(Label = "@string/InputWeight_Label", Theme = "@style/AppTheme")]
    public class InputWeight : Scanner_Activity , NavigationView.IOnNavigationItemSelectedListener
    {

        #region Parametry

        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;

        public Users Uzivatel = null;

        NavigationView mNavigationView;

        private Button btn_OK;
        private Button btn_Storno;
        public EditText textBoxInputWeight;

        private TextView InputWeight_txt_Vaha;

        private Button btn_InputWeight_btn_VAHA;

        private string Kod;
        private string Text;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                this.DataWedge_Scanner_Disable();

                SetContentView(Resource.Layout.InputWeight);

                Dexter.WithActivity(this)
                   .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                   Manifest.Permission.WriteExternalStorage,
                                   Manifest.Permission.AccessNetworkState,
                                   Manifest.Permission.Camera)
                   .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                   .WithErrorListener(new SampleErrorListner())
                   .Check();

                GetUzivatelFromIntent(Intent);

                Kod = Intent?.GetStringExtra(DataInfo_Static.OV_Weight_Kod);
                Text = Intent?.GetStringExtra(DataInfo_Static.OV_Weight_Text);

                textBoxInputWeight = FindViewById<EditText>(Resource.Id.InputWeight_edittxt_QTY);
                InputWeight_txt_Vaha = FindViewById<TextView>(Resource.Id.InputWeight_txt_QTY);

                
                btn_OK = FindViewById<Button>(Resource.Id.InputWeight_btn_OK);
                btn_Storno = FindViewById<Button>(Resource.Id.InputWeight_btn_Storno);
                btn_InputWeight_btn_VAHA = FindViewById<Button>(Resource.Id.InputWeight_btn_VAHA);

                btn_OK.Click += Btn_OK_Click;
                btn_Storno.Click += Btn_Storno_Click;
                btn_InputWeight_btn_VAHA.Click += Btn_InputWeight_btn_VAHA_Click;

                textBoxInputWeight.FocusChange += Edittext_Test_FocusChange;

                btn_OK.FocusChange += Btn_FocusChange;
                btn_Storno.FocusChange += Btn_FocusChange;
                btn_InputWeight_btn_VAHA.FocusChange += Btn_FocusChange;

                textBoxInputWeight.ShowSoftInputOnFocus = false;

                textBoxInputWeight.SetBackgroundResource(Resource.Drawable.lost_focus_style);

                btn_OK.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btn_Storno.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btn_InputWeight_btn_VAHA.SetBackgroundResource(Resource.Drawable.lost_focus_button);

                textBoxInputWeight.Text = Kod;

                textBoxInputWeight.RequestFocus();
                textBoxInputWeight.SelectAll();
                btn_OK.Selected = false;
                btn_Storno.Selected = false;
                btn_InputWeight_btn_VAHA.Selected = false;

                if (string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_Vaha_IP))
                {
                    btn_InputWeight_btn_VAHA.Visibility = ViewStates.Invisible;
                }

                    mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.InputWeight_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.InputWeight_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.InputWeight_nav_view);
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

            if (id == Resource.Id.nav_InputWeight_OK)
            {
                PerformOK();
            }
            else if (id == Resource.Id.nav_InputWeight_Storno)
            {
                StartAktivityNasledujici(typeof(OdvadeniVyroby), null);
            }
            else if (id == Resource.Id.nav_InputWeight_Vaha)
            {
                AsyncGetWeight(this);
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
            PerformKonec();
        }

        private void Btn_InputWeight_btn_VAHA_Click(object sender, EventArgs e)
        {
            AsyncGetWeight(this);
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
                intent.PutExtra(DataInfo_Static.OV_Weight_Kod_out, kod);



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


                if (this.textBoxInputWeight.Text.Trim().Length == 0)
                {
                    await MessageBoxAsync.Show(this, "Musíte zadat hodnotu!", this.Text, MessageBoxButtons.OK);
                    textBoxID_Activate();
                    return;
                }

                decimal cislo;
                bool status = decimal.TryParse(textBoxInputWeight.Text, out cislo);

                if (!status)
                {
                    await MessageBoxAsync.Show(this, "Musíte zadat číslo!", this.Text, MessageBoxButtons.OK);
                    textBoxID_Activate();
                    return;
                }

                if (cislo <= 0)
                {
                    await MessageBoxAsync.Show(this, "Musíte zadat pouze kladné číslo!", this.Text, MessageBoxButtons.OK);
                    textBoxID_Activate();
                    return;
                }


                StartAktivityNasledujici(typeof(OdvadeniVyroby), textBoxInputWeight.Text);
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
            textBoxInputWeight.RequestFocus();
            textBoxInputWeight.SelectAll();
        }

        override protected void ScannerData(ScannerEventArgs e)
        {
            if (e.Data.Trim().Length == 0)
                return;

            string CKout = e.Data.Trim();

            this.textBoxInputWeight.Text = CKout;
            PerformOK();
        }

        #region Vaha

        private async void AsyncGetWeight(AppCompatActivity parent)
        {
            if (string.IsNullOrEmpty(Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_Vaha_IP))
            {
                await MessageBoxAsync.Show(parent, "Komunikace s váhou neni povolena!", "Info", MessageBoxButtons.OK);
            }
            else
            {
                var xxx = await GetWeight_WaitDialog(parent);

                RunOnUiThread(() =>
                {
                    textBoxInputWeight.Text = xxx;
                    if (xxx == "0")
                    {
                        textBoxInputWeight.RequestFocus();
                        textBoxInputWeight.SelectAll();
                    }
                });
            }
        }

        private async Task<string> GetWeight_WaitDialog(AppCompatActivity parent)
        {

            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            ProgressDialog_Infinity.Show(this);
            ProgressDialog_Infinity.Message = "Vyčítává se váha...";
            await Task.Delay(5000);

            var xxx = await GetWeight(parent);

            tcs.SetResult(xxx);

            ProgressDialog_Infinity.Dispose();
            return tcs.Task.Result;

        }

        private async Task<string> GetWeight(AppCompatActivity parent)
        {
            var _Client = new FASK.Vaha.RAVAS.RAVAS_3200_Client();

            try
            {

                _Client.IP = Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_Vaha_IP;
                _Client.Port = Konfigurace_Singleton.Instance.Vyroba.Odvadeni_production_Vaha_PORT;
                _Client.Connect();

                var x = _Client.Get_Gross();

                if (x.HasValue)
                {
                    string tmp = x.Value.ToString(Config.Settings.UIFormatDesCisel);
                    return tmp;
                }
                else
                {
                    string tmp = 0.ToString(Config.Settings.UIFormatDesCisel);
                    return tmp;
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                await MessageBoxAsync.Show(parent, "Komunikace s váhou se nezdařila. Prosím zadej váhu ručně.", "Error Vaha", MessageBoxButtons.OK);
                int x = 0;
                return x.ToString();
            }
            finally
            {
                if (_Client.IsConnect)
                {
                    _Client.Disconnect();
                    _Client = null;
                }
            }
        }

        #endregion

        #region Perform metody

        private void PerformKonec()
        {
            StartAktivityNasledujici(typeof(OdvadeniVyroby), null);
        }



        #endregion

        #region Klavesy

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {

            if (keyCode == Keycode.Escape)
            {
                PerformKonec();
            }

            else if (keyCode == Keycode.Button1)
            {
                AsyncGetWeight(this);
            }


            return base.OnKeyDown(keyCode, e);

        }

            #endregion
        }
}