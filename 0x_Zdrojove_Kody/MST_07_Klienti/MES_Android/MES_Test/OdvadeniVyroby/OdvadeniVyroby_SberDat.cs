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

namespace MES_Android.OdvadeniVyroby
{
    [Activity(Label = "@string/OdvadeniVyroby_SberDat_Label")]
    public class OdvadeniVyroby_SberDat : Base_Aktivita , NavigationView.IOnNavigationItemSelectedListener
    {
        #region Parametry

        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;

        public Users Uzivatel = null;

        NavigationView mNavigationView;

        public TaskCompletionSource<Logika.OV_Core_LogikaAsync.O_GetInputQuantityAsync> TCS_GetInputQuantityAsync;
        public int ResoultCode_InputQuantityAsync = 123;

        public TaskCompletionSource<Logika.OV_Core_LogikaAsync.O_GetOperaceVyberAsync> TCS_GetOperaceVyberAsync;
        public int ResoultCode_OperaceVyberAsync = 456;

        public TaskCompletionSource<Logika.OV_Core_LogikaAsync.O_GetOdvadeniPrehledAsync> TCS_GetOdvadeniPrehledAsync;
        public int ResoultCode_OdvadeniPrehledAsync = 789;

        public TaskCompletionSource<Logika.OV_Core_LogikaAsync.O_GetOperacePotvrzeniAsync> TCS_GetOperacePotvrzeniAsync;
        public int ResoultCode_OperacePotvrzeniAsync = 147;

        public TaskCompletionSource<Logika.OV_Core_LogikaAsync.O_GetInputWeightAsync> TCS_GetInputWeightAsync;
        public int ResoultCode_InputWeightAsync = 666;

        private Button btn_OK;
        private Button btn_Storno;
        public EditText textBoxVyrobniOperace;

        Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow idPracovnik = null;
        Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idMachine = null;

        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow ZAKAZKA = null;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                this.DataWedge_Scanner_Disable();

                SetContentView(Resource.Layout.OdvadeniVyroby_SberDat);

                Dexter.WithActivity(this)
                   .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                   Manifest.Permission.WriteExternalStorage,
                                   Manifest.Permission.AccessNetworkState,
                                   Manifest.Permission.Camera)
                   .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                   .WithErrorListener(new SampleErrorListner())
                   .Check();



                var bundle = Intent?.GetBundleExtra(DataInfo_Static.User);
                var binder = bundle?.GetBinder(DataInfo_Static.object_User);
                if (binder != null)
                {
                    Uzivatel = ((WrapperForBinder<Users>)binder).getData();
                }

                var bundleM = Intent?.GetBundleExtra(DataInfo_Static.OV_Machine);
                var binderM = bundleM?.GetBinder(DataInfo_Static.object_OV_Machine);
                if (binderM != null)
                {
                    idMachine = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow>)binderM).getData();
                }

                var bundleP = Intent?.GetBundleExtra(DataInfo_Static.OV_Pracovnik);
                var binderP = bundleP?.GetBinder(DataInfo_Static.object_OV_Pracovnik);
                if (binderP != null)
                {
                    idPracovnik = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow>)binderP).getData();
                }

                var bundleZ = Intent?.GetBundleExtra(DataInfo_Static.OV_Zakazka);
                var binderZ = bundleZ?.GetBinder(DataInfo_Static.object_OV_Zakazka);
                if (binderZ != null)
                {
                    ZAKAZKA = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow>)binderZ).getData();
                }

                textBoxVyrobniOperace = FindViewById<EditText>(Resource.Id.OdvadeniVyroby_SberDat_edittxt_VyrobniOperace);

                btn_OK = FindViewById<Button>(Resource.Id.OdvadeniVyroby_SberDat_btn_OK);
                btn_Storno = FindViewById<Button>(Resource.Id.OdvadeniVyroby_SberDat_btn_Storno);


                btn_OK.Click += Btn_OK_Click;
                btn_Storno.Click += Btn_Storno_Click;

                textBoxVyrobniOperace.FocusChange += Edittext_Test_FocusChange;

                btn_OK.FocusChange += Btn_FocusChange;
                btn_Storno.FocusChange += Btn_FocusChange;

                textBoxVyrobniOperace.ShowSoftInputOnFocus = false;

                textBoxVyrobniOperace.SetBackgroundResource(Resource.Drawable.focus_border_style);

                btn_OK.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btn_Storno.SetBackgroundResource(Resource.Drawable.lost_focus_button);

                textBoxVyrobniOperace.RequestFocus();
                btn_OK.Selected = false;
                btn_Storno.Selected = false;

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.OdvadeniVyroby_SberDat_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.OdvadeniVyroby_SberDat_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.OdvadeniVyroby_SberDat_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);

                Scanner_START();
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

            if (id == Resource.Id.nav_OdvadeniVyroby_SberDat_OK)
            {
                PerformOK(null);
            }
            else if (id == Resource.Id.nav_OdvadeniVyroby_SberDat_Storno)
            {
                StartAktivityNasledujici(typeof(OdvadeniVyroby));
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
            PerformOK(null);
        }

        private void Btn_Storno_Click(object sender, EventArgs e)
        {
            StartAktivityNasledujici(typeof(OdvadeniVyroby));
        }
        
        #endregion

        private void GetUzivatelFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.User);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_User);
            Uzivatel = ((WrapperForBinder<Users>)binderTD).getData();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {

            if (requestCode == ResoultCode_InputQuantityAsync)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    string kod = data?.GetStringExtra(DataInfo_Static.OV_Kod);
                    //_IsCurrentlyInConfirmProcess_Lokace = false;

                    TCS_GetInputQuantityAsync.SetResult(new Logika.OV_Core_LogikaAsync.O_GetInputQuantityAsync()
                    {
                        status = true,
                        Kod= kod
                    });

                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_GetInputQuantityAsync.SetResult(new Logika.OV_Core_LogikaAsync.O_GetInputQuantityAsync()
                    {
                        status = false,
                        Kod = string.Empty
                    });
                }
            }


            if (requestCode == ResoultCode_OperaceVyberAsync)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    var bundleVPP = data?.GetBundleExtra(DataInfo_Static.OV_VPPRow);
                    var binderVPP = bundleVPP?.GetBinder(DataInfo_Static.object_OV_VPPRow);
                    var row = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow>)binderVPP).getData();


                    TCS_GetOperaceVyberAsync.SetResult(new Logika.OV_Core_LogikaAsync.O_GetOperaceVyberAsync()
                    {
                        status = true,
                        Row = row
                    });

                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_GetOperaceVyberAsync.SetResult(new Logika.OV_Core_LogikaAsync.O_GetOperaceVyberAsync()
                    {
                        status = false,
                        Row = null
                    });
                }
            }

            if (requestCode == ResoultCode_OdvadeniPrehledAsync)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    TCS_GetOdvadeniPrehledAsync.SetResult(new Logika.OV_Core_LogikaAsync.O_GetOdvadeniPrehledAsync()
                    {
                        status = true
                    });
                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_GetOdvadeniPrehledAsync.SetResult(new Logika.OV_Core_LogikaAsync.O_GetOdvadeniPrehledAsync()
                    {
                        status = false
                    });
                }
            }

            if (requestCode == ResoultCode_OperacePotvrzeniAsync)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    TCS_GetOperacePotvrzeniAsync.SetResult(new Logika.OV_Core_LogikaAsync.O_GetOperacePotvrzeniAsync()
                    {
                        status = true
                    });
                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_GetOperacePotvrzeniAsync.SetResult(new Logika.OV_Core_LogikaAsync.O_GetOperacePotvrzeniAsync()
                    {
                        status = false
                    });
                }
            }

            if (requestCode == ResoultCode_InputWeightAsync)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    string kod = data?.GetStringExtra(DataInfo_Static.OV_Weight_Kod_out);
                    //_IsCurrentlyInConfirmProcess_Lokace = false;

                    TCS_GetInputWeightAsync.SetResult(new Logika.OV_Core_LogikaAsync.O_GetInputWeightAsync()
                    {
                        status = true,
                        Kod = kod
                    });

                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_GetInputWeightAsync.SetResult(new Logika.OV_Core_LogikaAsync.O_GetInputWeightAsync()
                    {
                        status = false,
                        Kod = string.Empty
                    });
                }
            }

        }

        private void StartAktivityNasledujici(Type typAktivity)
        {
            Scanner_STOP();
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);


            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);
            
            this.SetResult(Result.Ok, intent);

            //this.StartActivity(intent);
            this.Finish();
        }

        private void PerformOK(BaseCode kod)
        {
           
            try
            {
                while (true)
                {
                    string k = textBoxVyrobniOperace.Text;

                    Logika.OV_Core_LogikaAsync _Logika = new Logika.OV_Core_LogikaAsync(this, idPracovnik, idMachine);
                    _Logika.Zakazka = ZAKAZKA;
                    _Logika.PerformOKAsync(kod, k);

                    #region Znovu Spousteni automaticky ...
                    if (Konfigurace_Singleton.Instance.Vyroba.StopVyrobaPoStartVyrobaIhned)
                    {
                        // TODO : provest akce => provedeni akci
                        // => automaticky provest stopodvod po startodvod...
                        // automaticky provest stopodvod ...
                        if (_Logika.odvodProcessedMode == Logika.OV_Core_LogikaAsync.TIMEMODES.StartStop && _Logika.odvodProcessedState == Logika.OV_Core_LogikaAsync.TIMESTATE.Odvod_Zahajen)
                        { // automaticke zahajeni prikazu
                          // Provede:
                          // - naplanovani znovu performok po dokonceni teto operace
                          // - dialog se neukonci a zustane tam puvodni cislo operace

                            //Maty: tady by mne mnelo znovu zavolat sam sebe
                            //_parent.BeginInvoke((Action)(() => { this.PerformOK(null); }));
                            continue;
                        }
                        else
                            break;
                    }
                    else
                        break;

                    #endregion
                }
            }
            finally
            {
            
            }
        }

        public void Scanner_START()
        {
            Zebra_Scanner.BarcodeScanner.getInstance(this);
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent += Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent += Scanner_StatusEvent;
        }

        public void Scanner_STOP()
        {
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent -= Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent -= Scanner_StatusEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StopScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.KillScanner();
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner = null;
        }

        private void Scanner_Zebra_ScannerEvent(object sender, ScannerEventArgs e)
        {
            string ck = e.Data.Trim();
            byte _input_mode = InputModeChecker.setInputMode(InputModeChecker._input_modes.INPUT_SCANNER);

            this.RunOnUiThread(() =>
            {
                UpdateUI(e, _input_mode);
            });


        }

        private void Scanner_StatusEvent(object sender, StatusEventArgs e)
        {
            //Zde zasila z eventu stav scanneru....
        }

        void UpdateUI(ScannerEventArgs e, byte inputMod)
        {
            if (e.Data.Trim().Length == 0)
                return;

            string CKout = e.Data.Trim();
            BaseCode code = null;

            code = Fask.Parsing.ParsingFactory.Parse(CKout, new Fask.Parsing.Config());

            if (code is WeightCode)
                CKout = ((WeightCode)code).id;
            else if (code is Fask.Parsing.Codes.WeightCode_12)
                CKout = ((WeightCode_12)code).id;

            this.textBoxVyrobniOperace.Text = CKout;
            PerformOK(code);
        }
    }
}