using System;

using Android;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;
using Android.Content;

using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V4.View;

using Com.Karumi.Dexter;
using Com.Karumi.Dexter.Listener.Multi;

using MES_Android.Listner;
using MES_Android.Classes;


using static MES_Android.OdvadeniVyroby.Logika.OV_Core_LogikaAsync;

namespace MES_Android.OdvadeniVyroby
{

    [Activity(Label = "@string/OdvadeniPrehled_Label")]
    public class OdvadeniPrehled : Base_Aktivita , NavigationView.IOnNavigationItemSelectedListener
    {

        #region Parametry
        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;
        public Users Uzivatel = null;
        NavigationView mNavigationView;

        private Button btn_OK;
        private Button btn_Storno;

        private TextView dfVphSOPNUMBE;
        private TextView dfVphSOPDESC;
        private TextView dfVppItemnmbr;
        private TextView dfVppItemdesc;
        private TextView dfVppVnditnum;
        private TextView dfVppBarcodeP;
        private TextView dfTimeStateActual;
        private TextView dfLastSOPNUMBER;
        private TextView dfLastITEMDESC;
        private TextView dfLastBarcodeP;
        private TextView dfLastQuantity;
        private TextView dfTimeStateLast;


        private TextView OdvadeniPrehled_TW_dfVphSOPNUMBE;
        private TextView OdvadeniPrehled_TW_dfVphSOPDESC;
        private TextView OdvadeniPrehled_TW_dfVppItemnmbr;
        private TextView OdvadeniPrehled_TW_dfVppItemdesc;
        private TextView OdvadeniPrehled_TW_dfVppVnditnum;
        private TextView OdvadeniPrehled_TW_dfVppBarcodeP;
        private TextView OdvadeniPrehled_TW_dfTimeStateActual;
        private TextView OdvadeniPrehled_TW_dfLastSOPNUMBER;
        private TextView OdvadeniPrehled_TW_dfLastITEMDESC;
        private TextView OdvadeniPrehled_TW_dfLastBarcodeP;
        private TextView OdvadeniPrehled_TW_dfLastQuantity;
        private TextView OdvadeniPrehled_TW_dfTimeStateLast;

        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow _VPH { get; set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow _VPP { get; set; }
        public TIMESTATE _TimeStateActual = TIMESTATE.Unknown;
        public TIMESTATE _TimeStateLast = TIMESTATE.Unknown;
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow _LastProduction { get; set; }

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                this.DataWedge_Scanner_Disable();
                SetContentView(Resource.Layout.OdvadeniPrehled);
                Dexter.WithActivity(this)
                   .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                   Manifest.Permission.WriteExternalStorage,
                                   Manifest.Permission.AccessNetworkState,
                                   Manifest.Permission.Camera)
                   .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                   .WithErrorListener(new SampleErrorListner())
                   .Check();

                 GetUzivatelFromIntent(Intent);
                GetVPHFromIntent(Intent);
                GetVPPFromIntent(Intent);
                GetProductionFromIntent(Intent);
                GetTimeActualFromIntent(Intent);
                GetTimeLastFromIntent(Intent);


                #region TextView

                dfVphSOPNUMBE = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfVphSOPNUMBE);
                dfVphSOPDESC = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfVphSOPDESC);
                dfVppItemnmbr = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfVppItemnmbr);
                dfVppItemdesc = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfVppItemdesc);
                dfVppVnditnum = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfVppVnditnum);
                dfVppBarcodeP = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfVppBarcodeP);
                dfTimeStateActual = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfTimeStateActual);
                dfLastSOPNUMBER = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfLastSOPNUMBER);
                dfLastITEMDESC = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfLastITEMDESC);
                dfLastBarcodeP = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfLastBarcodeP);
                dfLastQuantity = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfLastQuantity);
                dfTimeStateLast = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_textview_dfTimeStateLast);


                OdvadeniPrehled_TW_dfVphSOPNUMBE = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfVphSOPNUMBE);
                OdvadeniPrehled_TW_dfVphSOPDESC = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfVphSOPDESC);
                OdvadeniPrehled_TW_dfVppItemnmbr = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfVppItemnmbr);
                OdvadeniPrehled_TW_dfVppItemdesc = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfVppItemdesc);
                OdvadeniPrehled_TW_dfVppVnditnum = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfVppVnditnum);
                OdvadeniPrehled_TW_dfVppBarcodeP = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfVppBarcodeP);
                OdvadeniPrehled_TW_dfTimeStateActual = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfTimeStateActual);
                OdvadeniPrehled_TW_dfLastSOPNUMBER = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfLastSOPNUMBER);
                OdvadeniPrehled_TW_dfLastITEMDESC = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfLastITEMDESC);
                OdvadeniPrehled_TW_dfLastBarcodeP = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfLastBarcodeP);
                OdvadeniPrehled_TW_dfLastQuantity = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfLastQuantity);
                OdvadeniPrehled_TW_dfTimeStateLast = FindViewById<TextView>(Resource.Id.OdvadeniPrehled_TW_dfTimeStateLast);

        UpdateUI();

                #endregion

                btn_OK = FindViewById<Button>(Resource.Id.OdvadeniPrehled_btn_OK);
                btn_Storno = FindViewById<Button>(Resource.Id.OdvadeniPrehled_btn_Storno);

                btn_OK.Click += Btn_OK_Click;
                btn_Storno.Click += Btn_Storno_Click;

                btn_OK.FocusChange += Btn_FocusChange;
                btn_Storno.FocusChange += Btn_FocusChange;

                btn_OK.SetBackgroundResource(Resource.Drawable.focus_button);
                btn_Storno.SetBackgroundResource(Resource.Drawable.lost_focus_button);

                btn_OK.RequestFocus();
                btn_OK.Selected = false;
                btn_Storno.Selected = false;

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.OdvadeniPrehled_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.OdvadeniPrehled_drawer_layout);
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
                mNavigationView = FindViewById<NavigationView>(Resource.Id.OdvadeniPrehled_nav_view);
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
            if (id == Resource.Id.nav_OdvadeniPrehled_OK)
            {
                StartAktivityNasledujici(typeof(MainActivity), true);
            }
            else if (id == Resource.Id.nav_OdvadeniPrehled_Storno)
            {
                StartAktivityNasledujici(typeof(MainActivity), false);
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
            StartAktivityNasledujici(typeof(MainActivity), true);
        }

        private void Btn_Storno_Click(object sender, EventArgs e)
        {
            StartAktivityNasledujici(typeof(MainActivity), false);
        }

        #endregion

        #region Get From Intent

        private void GetUzivatelFromIntent(Intent intent)
        {
            var bundleTD = intent?.GetBundleExtra(DataInfo_Static.User);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_User);
            Uzivatel = ((WrapperForBinder<Users>)binderTD).getData();
        }

        private void GetTimeLastFromIntent(Intent intent)
        {
            var bundleTD = intent?.GetBundleExtra(DataInfo_Static.OV_lLastTimeState);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_lLastTimeState);
            _TimeStateLast = ((WrapperForBinder<TIMESTATE>)binderTD).getData();
        }

        private void GetTimeActualFromIntent(Intent intent)
        {
            var bundleTD = intent?.GetBundleExtra(DataInfo_Static.OV_lActualTimeState);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_lActualTimeState);
            _TimeStateActual = ((WrapperForBinder<TIMESTATE>)binderTD).getData();
        }

        private void GetProductionFromIntent(Intent intent)
        {
            var bundleTD = intent?.GetBundleExtra(DataInfo_Static.OV_row_lastProductionRowTmp);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_row_lastProductionRowTmp);
            _LastProduction = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow>)binderTD).getData();
        }

        private void GetVPPFromIntent(Intent intent)
        {
            var bundleTD = intent?.GetBundleExtra(DataInfo_Static.OV_row_VPP);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_row_VPP);
            _VPP = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow>)binderTD).getData();
        }

        private void GetVPHFromIntent(Intent intent)
        {
            var bundleTD = intent?.GetBundleExtra(DataInfo_Static.OV_row_VPH);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_row_VPH);
            _VPH = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow>)binderTD).getData();
        }

        #endregion

        private void StartAktivityNasledujici(Type typAktivity, bool ID)
        {
            
            Intent intent = new Intent(this, typAktivity);
            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);


            if(ID)
                this.SetResult(Result.Ok, intent);
            else
                this.SetResult(Result.Canceled, intent);

            this.Finish();
        }


        #region Pomocne metody

        private void UpdateUI()
        {
            try
            {
                if (_VPH != null)
                {
                    dfVphSOPNUMBE.Text = _VPH.SOPNUMBE.Trim();
                    dfVphSOPDESC.Text = _VPH.IsSOPDESCNull() ? "-" : _VPH.SOPDESC.Trim();
                }

                if (_VPP != null)
                {
                    dfVppItemnmbr.Text = _VPP.ITEMNMBR.Trim();
                    dfVppItemdesc.Text = _VPP.IsITEMDESCNull() ? "-" : _VPP.ITEMDESC.Trim();
                    dfVppVnditnum.Text = _VPP.IsVNDITNUMNull() ? "-" : _VPP.VNDITNUM.Trim();
                    dfVppBarcodeP.Text = _VPP.BarcodeP.Trim();

                }

                if (_LastProduction != null)
                {
                    dfLastSOPNUMBER.Text = _LastProduction.SOPNUMBE.Trim();
                    dfLastITEMDESC.Text = _LastProduction.ITEMDESC.Trim();

                    dfLastBarcodeP.Text = _LastProduction.BarcodeP.Trim();
                    dfLastQuantity.Text = _LastProduction.qty.ToString();

                }

                dfTimeStateActual.Text = _TimeStateActual.ToString();
               dfTimeStateLast.Text = _TimeStateLast.ToString();

                UpdateUITimeStateActualColor();
                UpdateUITimeStateLastColor();

            }
            catch
            {
            }
        }

        private void UpdateUITimeStateActualColor()
        {
            int ID = GetColorForTimeState(_TimeStateActual);

            dfVphSOPDESC.SetBackgroundResource(ID);
            dfVphSOPNUMBE.SetBackgroundResource(ID);
            dfVppBarcodeP.SetBackgroundResource(ID);
            dfVppItemdesc.SetBackgroundResource(ID);
            dfVppItemnmbr.SetBackgroundResource(ID);
            dfVppVnditnum.SetBackgroundResource(ID);
            dfTimeStateActual.SetBackgroundResource(ID);

            OdvadeniPrehled_TW_dfVphSOPDESC.SetBackgroundResource(ID);
            OdvadeniPrehled_TW_dfVphSOPNUMBE.SetBackgroundResource(ID);
            OdvadeniPrehled_TW_dfVppBarcodeP.SetBackgroundResource(ID);
            OdvadeniPrehled_TW_dfVppItemdesc.SetBackgroundResource(ID);
            OdvadeniPrehled_TW_dfVppItemnmbr.SetBackgroundResource(ID);
            OdvadeniPrehled_TW_dfVppVnditnum.SetBackgroundResource(ID);
            OdvadeniPrehled_TW_dfTimeStateActual.SetBackgroundResource(ID);

        }

        private void UpdateUITimeStateLastColor()
        {
            int ID = GetColorForTimeState(_TimeStateLast);

            dfLastBarcodeP.SetBackgroundResource(ID);
            dfLastITEMDESC.SetBackgroundResource(ID);
            dfLastQuantity.SetBackgroundResource(ID);
            dfLastSOPNUMBER.SetBackgroundResource(ID);
            dfTimeStateLast.SetBackgroundResource(ID);

            OdvadeniPrehled_TW_dfLastBarcodeP.SetBackgroundResource(ID);
            OdvadeniPrehled_TW_dfLastITEMDESC.SetBackgroundResource(ID);
            OdvadeniPrehled_TW_dfLastQuantity.SetBackgroundResource(ID);
            OdvadeniPrehled_TW_dfLastSOPNUMBER.SetBackgroundResource(ID);
            OdvadeniPrehled_TW_dfTimeStateLast.SetBackgroundResource(ID);
        }

        private int GetColorForTimeState(TIMESTATE ts)
        {
            switch (ts)
            {
                case TIMESTATE.Korekce_Zahajena:
                case TIMESTATE.Priprava_Zahajena:
                    return Resource.Drawable.OV_textview_YELLOW;
                case TIMESTATE.Odvod_Zahajen:
                    return Resource.Drawable.OV_textview_GREEN;
                case TIMESTATE.Korekce_Dokoncena:
                case TIMESTATE.Priprava_Dokoncena:
                case TIMESTATE.Odvod_Dokoncen:
                    return Resource.Drawable.OV_textview_RED;
                case TIMESTATE.Unknown:
                case TIMESTATE.Nezahajeno:
                default:
                    return Resource.Drawable.OV_textview_WHITE;
            }
        }

        #endregion


    }
}
