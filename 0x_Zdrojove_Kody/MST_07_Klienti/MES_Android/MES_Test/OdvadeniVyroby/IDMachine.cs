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
    [Activity(Label = "@string/IDMachine_Label")]
    public class IDMachine : Scanner_Activity, NavigationView.IOnNavigationItemSelectedListener
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
        public EditText textBoxIDMachine;

        Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow idMachine = null;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                this.DataWedge_Scanner_Disable();

                SetContentView(Resource.Layout.IDMachine);

                Dexter.WithActivity(this)
                   .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                   Manifest.Permission.WriteExternalStorage,
                                   Manifest.Permission.AccessNetworkState,
                                   Manifest.Permission.Camera)
                   .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                   .WithErrorListener(new SampleErrorListner())
                   .Check();

                 GetUzivatelFromIntent(Intent);

                textBoxIDMachine = FindViewById<EditText>(Resource.Id.IDMachine_edittxt_IDStroje);

                btn_OK = FindViewById<Button>(Resource.Id.IDMachine_btn_OK);
                btn_Storno = FindViewById<Button>(Resource.Id.IDMachine_btn_Storno);


                btn_OK.Click += Btn_OK_Click;
                btn_Storno.Click += Btn_Storno_Click;

                textBoxIDMachine.FocusChange += Edittext_Test_FocusChange;

                btn_OK.FocusChange += Btn_FocusChange;
                btn_Storno.FocusChange += Btn_FocusChange;

                textBoxIDMachine.ShowSoftInputOnFocus = false;

                textBoxIDMachine.SetBackgroundResource(Resource.Drawable.lost_focus_style);

                btn_OK.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btn_Storno.SetBackgroundResource(Resource.Drawable.lost_focus_button);

                textBoxIDMachine.RequestFocus();
                btn_OK.Selected = false;
                btn_Storno.Selected = false;

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.IDMachine_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.IDMachine_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.IDMachine_nav_view);
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

            if (id == Resource.Id.nav_IDMachine_OK)
            {
                PerformOK();
            }
            else if (id == Resource.Id.nav_IDMachine_Storno)
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

        private void StartAktivityNasledujici(Type typAktivity, Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow ID)
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);


            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);


            if(ID != null)
            {
                Bundle bundleUserM = new Bundle();
                bundleUserM.PutBinder(DataInfo_Static.object_OV_IDMachine, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow>(ID));
                intent.PutExtra(DataInfo_Static.OV_IDMachine, bundleUserM);
            }

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
                //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter mta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.MachinesTableAdapter();
                //mta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);
                Fask.SQLiteDBs.DataSets.Vyroba.MachinesDataTable dtMachines = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataByID_Machines(textBoxIDMachine.Text.Trim());

                if (dtMachines.Rows.Count == 0)
                    throw new Exception("Stroj neexistuje");

                this.idMachine = dtMachines[0];
                StartAktivityNasledujici(typeof(OdvadeniVyroby), this.idMachine);
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
            textBoxIDMachine.SelectAll();
            textBoxIDMachine.RequestFocus();
        }

        override protected void ScannerData(ScannerEventArgs e)
        {
            if (e.Data.Trim().Length == 0)
                return;

            string CKout = e.Data.Trim();

            this.textBoxIDMachine.Text = CKout;
            PerformOK();
        }
    }
}