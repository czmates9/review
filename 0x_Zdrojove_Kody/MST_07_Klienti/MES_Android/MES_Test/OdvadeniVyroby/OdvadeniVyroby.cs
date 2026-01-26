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
using static MES_Android.Ciselniky.CiselnikServiceOperations;
using Fask.SQLiteDBs.DataSets;

namespace MES_Android.OdvadeniVyroby
{
    [Activity(Label = "@string/OdvadeniVyroby_label")]
    public class OdvadeniVyroby : Base_Aktivita, NavigationView.IOnNavigationItemSelectedListener, ICiselniky_Datum
    {
        #region Parametry

        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;

        public Users Uzivatel = null;
        private Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow ZAKAZKA;

        NavigationView mNavigationView;

        Button btn_Odvadeni;
        Button btn_Tisk;
        Button btn_Korekce;
        Button btn_Udalosti;

        public TaskCompletionSource<Logika.OdvadeniVyroby_Odvadeni_LogikaAsync.O_Get_UserLoginWithTimeInput> TCS_GetUserLoginWithTimeInputAsync;
        public int ResoultCode_UserLoginWithTimeInput = 123;

        public TaskCompletionSource<Logika.OdvadeniVyroby_Odvadeni_LogikaAsync.O_GetOdvadeniVyroby_SberDatAsync> TCS_GetOdvadeniVyroby_SberDatAsync;
        public int ResoultCode_OdvadeniVyroby_SberDat = 456;

        public TaskCompletionSource<Logika.OdvadeniVyroby_Odvadeni_LogikaAsync.O_GetIDMachineAsync> TCS_GetIDMachineAsync;
        public int ResoultCode_IDMachine = 789;



        #endregion

        #region OnCreate

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                this.DataWedge_Scanner_Disable();

                SetContentView(Resource.Layout.OdvadeniVyroby);

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

                var bundleZAK = Intent?.GetBundleExtra(DataInfo_Static.PrikazVyber_SOPNUMBE);
                var binderZAK = bundleZAK?.GetBinder(DataInfo_Static.object_PrikazVyber_SOPNUMBE);
                if (binderZAK != null)
                {
                    ZAKAZKA = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPHRow>)binderZAK).getData();
                }

                btn_Odvadeni = FindViewById<Button>(Resource.Id.btn_OV_Odvadeni);
                btn_Korekce = FindViewById<Button>(Resource.Id.btn_OV_Korekce);
                btn_Tisk = FindViewById<Button>(Resource.Id.btn_OV_Tisk);
                btn_Udalosti = FindViewById<Button>(Resource.Id.btn_OV_Udalosti);

                btn_Odvadeni.Click += Btn_Odvadeni_Click;
                btn_Korekce.Click += Btn_Korekce_Click;
                btn_Tisk.Click += Btn_Tisk_Click;
                btn_Udalosti.Click += Btn_Udalosti_Click;

                btn_Odvadeni.SetBackgroundResource(Resource.Drawable.OV_focus_button_odvadeni);
                btn_Korekce.SetBackgroundResource(Resource.Drawable.OV_lost_focus_button_korekce);
                btn_Tisk.SetBackgroundResource(Resource.Drawable.OV_lost_focus_button_tisk);
                btn_Udalosti.SetBackgroundResource(Resource.Drawable.OV_lost_focus_button_udalosti);

                btn_Odvadeni.FocusChange += Btn_Odvadeni_FocusChange;
                btn_Korekce.FocusChange += Btn_Korekce_FocusChange;
                btn_Tisk.FocusChange += Btn_Tisk_FocusChange;
                btn_Udalosti.FocusChange += Btn_Udalosti_FocusChange;


                btn_Odvadeni.RequestFocus();
                btn_Korekce.Selected = false;
                btn_Tisk.Selected = false;
                btn_Udalosti.Selected = false;

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.OdvadeniVyroby_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.OdvadeniVyroby_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.OdvadeniVyroby_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);

                if(ZAKAZKA != null)
                {
                    SupportActionBar.Title = ZAKAZKA.SOPNUMBE.Trim();
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowErrorMessage(this, ex);
            }
        }


        #endregion

        #region FocusChange

        private void Btn_Odvadeni_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.OV_focus_button_odvadeni);
            }
            else
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.OV_lost_focus_button_odvadeni);
            }
        }

        private void Btn_Korekce_FocusChange(object sender, View.FocusChangeEventArgs e)

        {
            if (e.HasFocus)
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.OV_focus_button_korekce);
            }
            else
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.OV_lost_focus_button_korekce);
            }
        }

        private void Btn_Tisk_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.OV_focus_button_tisk);
            }
            else
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.OV_lost_focus_button_tisk);
            }
        }

        private void Btn_Udalosti_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.OV_focus_button_udalosti);
            }
            else
            {
                ((Button)sender).SetBackgroundResource(Resource.Drawable.OV_lost_focus_button_udalosti);
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
                HlavneMenu();
            }

        }

        #endregion

        #region Bočne menu

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Ciselniky_Helper _Helper = new Ciselniky_Helper();

            int id = item.ItemId;

            if (id == Resource.Id.nav_HlMenu_OV)
            {
                HlavneMenu();
            }
            else if (id == Resource.Id.nav_menu_OV_DB_upload)
            {
                DB_upload();
            }
            else if (id == Resource.Id.nav_menu_OV_DB_download)
            {
                DB_download(Operation.OdvadeniVyroby);
            }

            mDrawerLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }

        private void HlavneMenu()
        {
            Intent intent = new Intent(this, typeof(Activity_HlavneMenu));
            Bundle bundle = new Bundle();
            bundle.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundle);
            //intent.pu
            this.StartActivity(intent);
            this.Finish();
        }

        #region práce s DB
        private void DB_upload()
        {
            timerUploadThreadStart();
        }

        private async void DB_download(Operation typ)
        {
            try
            {
                // Konfigurace_Singleton.Instance.Vyroba.SkladID

                Ciselniky_Helper _Helper = new Ciselniky_Helper();
                bool state = await _Helper.SynchronizeCiselniky(typ, this, Konfigurace_Singleton.Instance.Prodej.SkladID); //instance na vyrobu!!!

                if (!state)
                {
                    await MessageBoxAsync.Show(this, "NEco je špatně...", "error", MessageBoxButtons.OK);
                }

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        #endregion

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

        private void Btn_Udalosti_Click(object sender, EventArgs e)
        {
            PerfomUdalosti();
        }

        private void Btn_Tisk_Click(object sender, EventArgs e)
        {
            PerformTisk();
        }

        private void Btn_Korekce_Click(object sender, EventArgs e)
        {
            PerformKorekce();
        }

        private void Btn_Odvadeni_Click(object sender, EventArgs e)
        {
            Logika.OdvadeniVyroby_Odvadeni_LogikaAsync _Logika = new Logika.OdvadeniVyroby_Odvadeni_LogikaAsync(this, Uzivatel);
            _Logika.PerfomOdvadeni_Async(ZAKAZKA);
        }



        #endregion

        #region Perform metody

        private async void PerformTisk()
        {
            try
            {
                using (Tisk.Baleni.FormBaleniMain fbm = new Tisk.Baleni.FormBaleniMain())
                {
                    fbm.ShowDialog();
                }
            }
            catch (Exception exTisk)
            {
                Fask.Logging.ExceptionHandler2.Handle(exTisk);
                await MessageBoxAsync.Show(this, exTisk.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private async void PerfomUdalosti()
        {
            try
            {
                string idPracovnik = string.Empty;
                Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow pracovnik = null;

                //if (Settings.UEventPracovnikLoginEnabled && Globals.Pracovnik != null)
                //{
                //    idPracovnik = Globals.Pracovnik.id;
                //    pracovnik = Globals.Pracovnik;
                //}
                //else
                //{
                //    using (Odvadeni.FormIDPracovnika fidprac = new Fask.Vyroba_W.Odvadeni.FormIDPracovnika())
                //    {
                //        if (fidprac.ShowDialog() == DialogResult.Cancel)
                //            return;
                //        idPracovnik = fidprac.Pracovnik.id;
                //        pracovnik = fidprac.Pracovnik;
                //    }
                //}

                Fask.SQLiteDBs.DataSets.Vyroba.StatusTypesRow statusRow = null;

                using (FormUdalosti fik = new FormUdalosti())
                {
                    fik.Text = "Zadejte ID události";

                    while (true)
                    {
                        if (fik.ShowDialog() == DialogResult.Cancel)
                            return;


                        statusRow = fik.SelectedStatusTypesRow;
                        if (statusRow.statusid == Konfigurace_Singleton.Instance.Vyroba.UEventSmenaLogin)
                            continue;
                        else if (statusRow.statusid == Konfigurace_Singleton.Instance.Vyroba.UEventSmenaLogout)
                            continue;

                        if (await MessageBoxAsync.Show(
                            this,
                            pracovnik.firstname.Trim() + " " + pracovnik.surname.Trim() + "\n" + statusRow.statusdesc,
                            "Dotaz",
                            MessageBoxButtons.OKCancel
                            ) == DialogResult.Cancel
                            )
                            continue;
                        else
                            break;
                    }

                    DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.Insert_UserEvents(Config.Settings_DB.LastProductionUserID, null, DateTime.Now, statusRow.statusid, idPracovnik, Config.Settings.TerminalID, string.Empty, Guid.NewGuid());

                    if (statusRow.statusid == Konfigurace_Singleton.Instance.Vyroba.UEventPracovnikPrihlaseni) //Prihlaseni pracovnika => zapis do Internal logoper
                    {
                        InternalState.UpdateInternalStateLstOperationUser(idPracovnik, DateTime.Now);
                    }
                    else if (statusRow.statusid == Konfigurace_Singleton.Instance.Vyroba.UEventPracovnikOdhlaseni)
                    {
                        InternalState.DeleteInternalStateLstOperationUser(idPracovnik);
                    }
                }

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private async void PerformKorekce()
        {
            try
            {
                string idPracovnik = string.Empty;
                //Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow pracovnik = null;

                //if (Konfigurace_Singleton.Instance.Vyroba.UEventPracovnikLoginEnabled && Globals.Pracovnik != null)
                //{
                //    idPracovnik = Globals.Pracovnik.id;
                //    pracovnik = Globals.Pracovnik;
                //}
                //else
                //{
                //    using (FormIDPracovnika fidprac = new FormIDPracovnika())
                //    {
                //        if (fidprac.ShowDialog() == DialogResult.Cancel)
                //            return;
                //        idPracovnik = fidprac.Pracovnik.id;
                //        pracovnik = fidprac.Pracovnik;
                //    }
                //}

                // Korekce
                // TODO : implementovat ...
                // Zjistit posledni otevrenou korekci
                // pokud neni
                // - dialog pro vytvoreni korekce
                // - pokud je dialog pro ukonceni korekce 

                Fask.SQLiteDBs.DataSets.Vyroba dsVyroba = new Fask.SQLiteDBs.DataSets.Vyroba();
                Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow correctionRow = null;
                Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow productionRow = dsVyroba.Production.NewProductionRow();

                using (FormKorekce formKorekce = new FormKorekce())
                {
                    //formKorekce.Pracovnik = pracovnik;
                    formKorekce.ProductionRow = productionRow;
                    formKorekce.Correction = correctionRow;
                    formKorekce.CorretionMinimumDateTime = Data.DatabaseActions.UserLastAction(Uzivatel.ID.ToString());
                    if (DialogResult.Cancel == formKorekce.ShowDialog())
                        return;

                    //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter pta = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.ProductionTableAdapter();
                    //pta.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.Production + Constants.PRD);

                    //productionRow.CountEntries = rowvpp.CountEntries;
                    //productionRow.BarcodeP = rowvpp.BarcodeP;
                    //productionRow.dateeve = DateTime.Now;
                    productionRow.description = string.Empty;
                    productionRow.GUID = Guid.NewGuid();
                    //productionRow.ITEMNMBR = rowvpp.ITEMNMBR;
                    //productionRow.ITEMTYPE = rowvpp.ITEMTYPE;
                    //if (!rowvpp.IsITEMMJNull())
                    //    productionRow.ITEMMJ = rowvpp.ITEMMJ;
                    productionRow.loginid = Config.Settings_DB.LastProductionUserID;
                    //productionRow.machineid = idmachine.id;
                    //productionRow.ORD = rowvpp.ORD;
                    productionRow.qty = 0; // pocetOdvedeno;
                    productionRow.qtyReal = 0; // pocetOdvedeno;
                    //productionRow.QTYPACK = rowvpp.QTYPACK;
                    //if (!rowvpp.IsQTYPACKMJNull())
                    //    productionRow.QTYPACKMJ = rowvpp.QTYPACKMJ;
                    //productionRow.SOPNUMBE = rowvpp.SOPNUMBE;
                    //productionRow.TIMEMODE = rowvpp.TIMEMODE;
                    //productionRow.TIMEUNIT = rowvpp.TIMEUNIT;

                    productionRow.UserID = Uzivatel.ID.ToString();
                    productionRow.TermID = Config.Settings.TerminalID;

                    dsVyroba.Production.AddProductionRow(productionRow);
                    DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Production_PRD.Update_Production(productionRow);

                }


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }


        #endregion

        #region Metoda pro nastaveni datumu aktualizace do bočního menu

        public void SetDateToMenu(int ID_String, string FileName)
        {
            int cnt = mNavigationView.HeaderCount;

            if (cnt == 1)
            {
                int ID_Menu = 0;

                if (ID_String == Resource.String.menu_OV_DB_download)
                    ID_Menu = Resource.Id.nav_menu_OV_DB_download;

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

        private void GetUzivatelFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.User);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_User);
            Uzivatel = ((WrapperForBinder<Users>)binderTD).getData();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {

            if (requestCode == ResoultCode_UserLoginWithTimeInput)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    var OV_DateTime_UserLoginWithTimeInput = data?.GetStringExtra(DataInfo_Static.OV_DateTime_UserLoginWithTimeInput);
                    //_IsCurrentlyInConfirmProcess_Lokace = false;

                    TCS_GetUserLoginWithTimeInputAsync.SetResult(new Logika.OdvadeniVyroby_Odvadeni_LogikaAsync.O_Get_UserLoginWithTimeInput()
                    {
                        UserLoginDateTime = DateTime.Parse(OV_DateTime_UserLoginWithTimeInput)
                    });

                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_GetUserLoginWithTimeInputAsync.SetResult(new Logika.OdvadeniVyroby_Odvadeni_LogikaAsync.O_Get_UserLoginWithTimeInput()
                    {
                        UserLoginDateTime = null
                    });
                }
            }

            else if (requestCode == ResoultCode_OdvadeniVyroby_SberDat)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    TCS_GetOdvadeniVyroby_SberDatAsync.SetResult(new Logika.OdvadeniVyroby_Odvadeni_LogikaAsync.O_GetOdvadeniVyroby_SberDatAsync()
                    {
                        Status = "OK"
                    });

                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_GetOdvadeniVyroby_SberDatAsync.SetResult(new Logika.OdvadeniVyroby_Odvadeni_LogikaAsync.O_GetOdvadeniVyroby_SberDatAsync()
                    {
                        Status = "Storno"
                    });
                }
            }

            else if (requestCode == ResoultCode_IDMachine)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    var bundleM = data?.GetBundleExtra(DataInfo_Static.OV_IDMachine);
                    var binderM = bundleM?.GetBinder(DataInfo_Static.object_OV_IDMachine);
                    var x = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow>)binderM).getData();

                    TCS_GetIDMachineAsync.SetResult(new Logika.OdvadeniVyroby_Odvadeni_LogikaAsync.O_GetIDMachineAsync()
                    {
                        IDMachine = x
                    });

                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_GetIDMachineAsync.SetResult(new Logika.OdvadeniVyroby_Odvadeni_LogikaAsync.O_GetIDMachineAsync()
                    {
                        IDMachine = null
                    });
                }
            }
        }

        #region MyRegion

        private async void timerUploadThreadStart()
        {
            try
            {
                ProgressDialog_Infinity.Show(this);

                await Task.Delay(5000);

                var resistentFiles = Directory.GetFiles(DataInfo_Static.Production_ALL_TemplateDB);
                foreach (var resFile in resistentFiles)
                {
                    File.Delete(resFile);
                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "timerUploadThreadStart() removing resistent old Production_*.prd.tmp files");
            }

            Guid guid = Guid.NewGuid();
            var NameFile_PRD_TMP = DataInfo_Static.Production_ALL + guid.ToString("N") + DataInfo_Static.PriponaPRD + DataInfo_Static.PriponaTMP;
            var NameFile_PRD_TMP_ZIP = NameFile_PRD_TMP + DataInfo_Static.PriponaZIP;
            string PathToFile_TMP = Path.Combine(DataInfo_Static.PathDir, NameFile_PRD_TMP);
            string PathToFile_ZIP = Path.Combine(DataInfo_Static.PathDir, NameFile_PRD_TMP_ZIP);

            try
            {
                try
                {
                    File.Copy(
                        DataInfo_Static.ProductionDB,
                        PathToFile_TMP,
                        true
                        );
                }
                catch (Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "timerUploadThreadStart(object state):File.Copy");
                    throw ex;
                }


                //string URL = Settings.WebServiceAddressVyroba + "Upload.aspx";
                string Odkud = PathToFile_ZIP;
                string KamNaServer = Config.Settings.TerminalID + "\\" + NameFile_PRD_TMP_ZIP;


                CompressFile.CompressToZip(PathToFile_TMP, PathToFile_ZIP);

                if (DataInfo_Static.API_GO_Instance.SendFile_API(Odkud, KamNaServer) != "OK")
                {
                    await MessageBoxAsync.Show(this, "Nepodařilo se odeslat davku", "Error", MessageBoxButtons.OK);
                    throw new Exception("Nepodařilo se odeslat davku");
                }


                bool processed = DataInfo_Static.VyrobaGO_Instance.vyrobaServis.ProcessProductionData2(
                    Config.Settings.TerminalID,
                    guid
                    );

                if (processed)
                {

                    //Odmazani odvedenych dat z Production                    
                    DataInfo_Static.VyrobaGO_Instance.DeleteProductionByGuid(PathToFile_TMP);

                    //Odmazani odvedenych dat z Production_SN
                    DataInfo_Static.VyrobaGO_Instance.DeleteProduction_SNByGuid(PathToFile_TMP);

                    //Odmazani odvedenych dat z Production_Sources
                    DataInfo_Static.VyrobaGO_Instance.DeleteProduction_SourcesByGuid(PathToFile_TMP);

                    //Odmazani odvedenych dat z UserEvents
                    DataInfo_Static.VyrobaGO_Instance.DeleteUserEventsByGuid(PathToFile_TMP);
                }

                ProgressDialog_Infinity.Message = "Data odeslána: " + DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                try
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "timerUploadThreadStart End");
                    ProgressDialog_Infinity.Message = "Odeslání dat se nezdařilo: " + DateTime.Now.ToString() + ex.Message;
                }
                catch { }
            }
            finally
            {
                ProgressDialog_Infinity.Dispose();
                //Vzdy ho smazu, protoze uz tento tmp neni dulezity, vsechno se provedlo a uz k nemu nikdy nepristoupim...
                try { 
                    File.Delete(PathToFile_TMP);
                    File.Delete(PathToFile_ZIP);
                }
                catch (Exception exDeleteFile)
                {
                    Fask.Logging.ExceptionHandler2.Handle(exDeleteFile);
                    Fask.Logging.ExceptionHandler2.Handle(Fask.Logging.LogLevel.Error, "timerUploadThreadStart Delete '{" + NameFile_PRD_TMP + "}' file");
                }

                //try { _uploadInProgress = false; }
                //catch { }
                ////timerUploadStart();
                //try { this.BeginInvoke(new DelegateVoid(timerUploadStart)); }
                //catch { }
            }

        }


        #endregion

    }
}