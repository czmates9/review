using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Com.Karumi.Dexter;
using Com.Karumi.Dexter.Listener.Multi;
using MES_Android._WebReferences_Globals;
using MES_Android.Classes;
using MES_Android.Listner;
using MES_Android.ServerAccess;
using System.IO;
using Fask.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using static Android.Views.View;

namespace MES_Android.Prodej
{
    //[Activity(Label = "Prodej_SberDat")]
    [Activity(Label = "@string/Prodej_SberDat_Label", Theme = "@style/AppTheme")]
    //public class Prodej_SberDat : Scanner_Activity, NavigationView.IOnNavigationItemSelectedListener
    public class Prodej_SberDat : Base_Aktivita, NavigationView.IOnNavigationItemSelectedListener
    {
        #region Parametry

        enum HledaniStatus
        {
            Nenalezeno = 0,
            NalezenJedenZaznam = 1,
            NalezenoViceZaznamu = 2,
            NalezenoViceJakNastavenyPocetZaznamu = 3
        }

        public Users Uzivatel = null;
        public Prodej.Prodej_Davka_Item _Item = null;

        public RecyclerView mRecycleView;
        public RecyclerView.LayoutManager mLayoutManager;

        public Prodej_SberDat_Item_Adapter mAdapter;

        public Android.Support.V7.Widget.Toolbar mToolbar;
        public FASK_ActionBarDrawerToggle mDrawerToggle;
        public Android.Support.V4.Widget.DrawerLayout mDrawerLayout;
        public NavigationView mNavigationView;

        public Android.Support.V7.Widget.SearchView _searchView;

        //Fask.Scanner.Zebra_EMDK.Zebra_EMDK scanner_Zebra = null;

        private string PathToDavka
        {
            get
            {
                if (_Item != null)
                {
                    if (_Item.CisloDavky.HasValue)
                        return Path.Combine(DataInfo_Static.PathDir, _Item.CisloDavky.Value.ToString() + DataInfo_Static.PriponaDI);
                    else
                        return null;
                }
                else
                    return null;
            }
        }


        public int ResoultCode_AddZasobu = 666;
        public int ResoultCode_ZasobyVyber = 777;
        public int ResoultCode_ChangePaleta = 888;



        public TaskCompletionSource<Logika.Prodej_SberDat_LogikaAsync.O_GetData> TCS_CilovySkladAsync;
        public int ResoultCode_CilovySklad = 123;


        public TaskCompletionSource<Logika.Prodej_SberDat_LogikaAsync.O_GetData> TCS_GetLokaciAsync;
        public int ResoultCode_Lokace = 789;



        public TaskCompletionSource<Logika.Prodej_SberDat_LogikaAsync.O_GetData> TCS_PPP_Async;
        public int ResoultCode_PPP = 11111;



        public TaskCompletionSource<Logika.Prodej_SberDat_LogikaAsync.O_GetData> TCS_ZadejVyberMaterialuAsync;
        public int ResoultCode_VyberMaterial = 456;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            try
            {
                this.DataWedge_Scanner_Disable();

                SetContentView(Resource.Layout.Prodej_SberDat);

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

                var bundleTD = Intent?.GetBundleExtra(DataInfo_Static.ProdejItem);
                var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_ProdejItem);
                if (binderTD != null)
                {
                    _Item = ((WrapperForBinder<Prodej.Prodej_Davka_Item>)binderTD).getData();
                }

                mRecycleView = FindViewById<RecyclerView>(Resource.Id.Prodej_SberDat_recyclerView);
                mLayoutManager = new LinearLayoutManager(this);
                mRecycleView.SetLayoutManager(mLayoutManager);

                Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dtdi = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable();
                if (PathToDavka != null)
                {
                    using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
                    {
                        conPro.Fill_DI(dtdi);
                    }
                }



                // NEFUNGUJE, odchytavani klaves je strašne pofiderne a naprd
                //IOnKeyListener onKey = new LocationKeyListner();
                //mRecycleView.SetOnKeyListener(onKey);


                mAdapter = new Prodej_SberDat_Item_Adapter(this, new Prodej_SberDat_Seznam(dtdi));
                mAdapter.ItemClick += MAdapter_ItemClick;
                mAdapter.ItemLongClick += MAdapter_ItemLongClick;
                mRecycleView.SetAdapter(mAdapter);


                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Prodej_SberDat_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Prodej_SberDat_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.Prodej_SberDat_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);

                //try
                //{
                //    scanner_Zebra = new Fask.Scanner.Zebra_EMDK.Zebra_EMDK();
                //    scanner_Zebra.ScannerEvent += Scanner_Zebra_ScannerEvent;
                //    scanner_Zebra.StatusEvent += Scanner_StatusEvent;

                //    //ScannerStart();
                //}
                //catch (Exception exZebra)
                //{
                //    Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exZebra);
                //}

                if (_Item.CisloDavky.HasValue)
                {

                    SupportActionBar.Title = string.Format(Resources.GetString(Resource.String.Prodej_SberDat_Label_Custom), _Item.CisloDavky.Value.ToString());
                }

                SupportActionBar.Subtitle = Resources.GetString(Resource.String.Prodej_SberDat_SubTitle);


                Scanner_START();

                HideInMenu();

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowErrorMessage(this, ex);
            }

        }

        #region SkritMenu

        public void HideInMenu()
        {
            Android.Views.IMenuItem imi = null;
            int cnt = mNavigationView.HeaderCount;

            if (cnt == 1)
            {
                int ID_Menu = Resource.Id.nav_Paleta_ProSberDat;
                var menu = mNavigationView.Menu;
                var v = mNavigationView.GetHeaderView(0);
                imi = menu.FindItem(ID_Menu);
            }

            if (_Item.TypDokladu != null && _Item.TypDokladu.cfg_palety > 0)
            {
                RunOnUiThread(() =>
                {
                    imi.SetVisible(true);
                });
            }
            else
            {
                RunOnUiThread(() =>
                {
                    imi.SetVisible(false);
                });
            }
        }


        #endregion


        #region životny cyklus aplikace

        //protected override void OnResume()
        //{
        //    scanner_Zebra?.StartScanner();
        //    base.OnResume();
        //}

        //protected override void OnPause()
        //{
        //    scanner_Zebra?.StopScanner();
        //    base.OnPause();
        //}

        ////protected override void OnStop()
        ////{
        ////    scanner_Zebra?.KillScanner();
        ////    base.OnDestroy();
        ////}

        //protected override void OnDestroy()
        //{
        //    scanner_Zebra?.KillScanner();
        //    base.OnDestroy();
        //}



        #endregion

        #region OnKeyDown


        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {

            if (keyCode == Keycode.F1)
            {
                ZasobySeznam();
                return true;

            }
            if (keyCode == Keycode.F2)
            {
                PerformZmenaPalety();
                return true;

            }
            else if (keyCode == Keycode.Escape)
            {
                GoHome();
                return true;
            }
            //else if (keyCode == Keycode.F2)
            //{
            //    int pos = mAdapter.vh.AdapterPosition;
            //    int posa = mAdapter.vh.Position;
            //    int posd = mAdapter.vh.OldPosition;

            //    //if (Prodej_Globals.PovolitPrintServer)
            //    //{
            //    //   TiskEtikety(di);
            //    //}

            //}


                //return false;
                return base.OnKeyDown(keyCode, e);
        }

        #endregion

        #region ClickEventy

        private void MAdapter_ItemClick(object sender, int e)
        {
            string s = string.Format("pozice:'{0}'", e);
            Toast.MakeText(this, s, ToastLength.Short).Show();
        }

        private async void MAdapter_ItemLongClick(object sender, int e)
        {
            var dr =  await MessageBoxAsync.Show(this, "Odstranit položku?", "Otazka", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {

                var selected = mAdapter.PolozkaSeznam[e];
                if (selected == null)
                    return;

                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej ConPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
                {
                    ConPro.Delete_DI_ByGUID(selected.guid);
                }


                mAdapter.RemoveItem(e);
            }
        }

        #endregion

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

            //CiselnikServiceSession ciselnikS = new CiselnikServiceSession();
            //ciselnikS.Url = Config.Settings.Adresa + "Ciselnik.asmx";
            //ciselnikS.Timeout = Config.Settings.TimeOut;
            //ciselnikS.UpdateWebServiceCredentials();

            //string SkladFilter = string.Empty;

            int id = item.ItemId;

            if (id == Resource.Id.nav_HlMenu_ProSberDat)
            {

                GoHome();
                //var confirmThread = new Thread(() => GoHome());
                //confirmThread.Start();
            }
            else if (id == Resource.Id.nav_Zasoby_ProSberDat)
            {
                ZasobySeznam();
            }
            else if (id == Resource.Id.nav_Paleta_ProSberDat)
            {
                PerformZmenaPalety();
            }


            mDrawerLayout.CloseDrawer(GravityCompat.Start);
            return true;
        }

        private async void PerformZmenaPalety()
        {
            await PerformTiskPaleta();
            await zobrazeniPaleta();
        }

        private async void GoHome()
        {

            var dr = await MessageBoxAsync.Show(this,
                string.Format(Resources.GetString(Resource.String.ProdejSberDat_LC_Message), _Item.CisloDavky),
                Resources.GetString(Resource.String.ProdejSberDat_LC_Title),
                MessageBoxButtons.YesNoCancel
                );


            if (dr == DialogResult.Yes)
            {

                if (_Item.TypDokladu != null && _Item.TypDokladu.cfg_palety > 0)
                {
                    await PerformTiskPaleta();
                }

                Scanner_STOP();

                try
                {

                    bool state = await SendDavku();

                    //if (!state)
                    //{
                    //    await MessageBoxAsync.Show(this, "Neco je špatně...", "error", MessageBoxButtons.OK);
                    //}
                }
                catch (System.Exception ex)
                {
                    Fask.Logging.ExceptionHandler2.Handle(ex);
                    ProgressDialog_Infinity.Dispose();
                    await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                }
                finally
                {
                    ProgressDialog_Infinity.Dispose();
                }

                RunOnUiThread(()=> { GoToListDavek(); });
            }
            else if (dr == DialogResult.No)
            {
                Scanner_STOP();

                RunOnUiThread(() => { GoToListDavek(); });
                
            }
            else
            {
                // nic
            }
            
        }

        private async Task<bool> SendDavku()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            var cislodavkykodeslani = _Item.CisloDavky.Value;

            ProgressDialog_Infinity.Show(this);

            await Task.Delay(2000);

            int pocetpolozek = 0;

            using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
            {
                pocetpolozek = conPro.CZMST_DI_Count();
            }

            if (pocetpolozek <= 0)
            {
                await MessageBoxAsync.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejMainDavkaNeobsahujePolozky), cislodavkykodeslani.ToString()), "Info", MessageBoxButtons.OK);

                tcs.SetResult(false);
                return tcs.Task.Result;
            }

            ProdejServiceOperations serviceOperations = new ProdejServiceOperations();

            var T = serviceOperations.SendData(this, DataInfo_Static.ProdejGO_Instance.servis_prodej, cislodavkykodeslani, Uzivatel.ID.Value, Uzivatel.Login);
            bool result = await T;

            if (result)
            {
                if (Konfigurace_Singleton.Instance.Prodej.DialogUspesnehoOdeslaniDavky)
                    await MessageBoxAsync.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejMainDavkaOdeslana), cislodavkykodeslani.ToString()), "hurá", MessageBoxButtons.OK);
            }
            else
            {
                await MessageBoxAsync.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejMainDavkaSeNepodarilaOdeslat), cislodavkykodeslani.ToString()), "Hups...", MessageBoxButtons.OK);
            }

            tcs.SetResult(true);

            return tcs.Task.Result;
        }

        private void GoToListDavek()
        {
            try
            {
                Intent intent = new Intent(this, typeof(Prodej_Davky));
                Bundle bundle = new Bundle();
                bundle.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
                intent.PutExtra(DataInfo_Static.User, bundle);
                //intent.pu
                this.StartActivity(intent);
                this.Finish();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private async void ZasobySeznam()
        {
            //Intent intent = new Intent(this, typeof(Prodej_Zasoby));
            //Bundle bundleUser = new Bundle();
            //bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(this.Uzivatel));
            //intent.PutExtra(DataInfo_Static.User, bundleUser);

            //intent.PutExtra(DataInfo_Static.Sklad_Zasoby, _Item.SkladZdroj.skl_id);

            //this.StartActivityForResult(intent, ResoultCode_AddZasobu);

            await zobrazeniList(null);

        }

        #endregion

        #region Vyhledavani

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Prodej_SberDat_Search, menu);

            var item = menu.FindItem(Resource.Id.ProdejSberDat_search);

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
                GoHome();
                //base.OnBackPressed();
            }

        }

        #endregion

        #region Scanner

        //public void ScannerStop()
        //{
        //    scanner_Zebra?.StopScanner();
        //}

        //public void ScannerStart()
        //{
        //    scanner_Zebra?.StartScanner();
        //}

        #endregion

        #region Zebra scanner

        public void Scanner_START()
        {
            Zebra_Scanner.BarcodeScanner.getInstance(this);
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent += Scanner_Zebra_ScannerEvent;
            Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent += Scanner_StatusEvent;
        }

        public void Scanner_STOP()
        {
            if (Zebra_Scanner.BarcodeScanner.mBarcodeScanner == null)
                return;

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

            //var confirmThread = new Thread(() => UpdateUI(ck, _input_mode));
            //confirmThread.Start();
            this.RunOnUiThread(() =>
            {
                UpdateUI(ck, _input_mode);
            });


        }

        private void Scanner_StatusEvent(object sender, StatusEventArgs e)
        {
            //Zde zasila z eventu stav scanneru....
        }



        #endregion

        #region životny cyklus aplikace

        //protected override void OnResume()
        //{
        //    scanner_Zebra?.StartScanner();
        //    base.OnResume();
        //}

        //protected override void OnPause()
        //{
        //    scanner_Zebra?.StopScanner();
        //    base.OnPause();
        //}

        //protected override void OnDestroy()
        //{
        //    scanner_Zebra?.KillScanner();
        //    base.OnDestroy();
        //}

        #endregion

        //private void UpdateUI(string carkod, byte _input_mode)
        private async void UpdateUI(string carkod, byte _input_mode)
        {
            try
            {
                Logika.Prodej_SberDat_LogikaAsync _Logika = new Logika.Prodej_SberDat_LogikaAsync(this, _Item.CisloDavky.Value, _Item.TypDokladu);

                Scanner_STOP();
                //ScannerStop();

                string ck = carkod.Trim();
                Fask.SQLiteDBs.DataSets.Zbozi Polozky = null;
                // parsovani kodu
                Fask.Parsing.Codes.BaseCode code = null;

                if (_Item.TypDokladu != null && !_Item.TypDokladu.Iscfg_parsovat_ckNull() && _Item.TypDokladu.cfg_parsovat_ck > 0)
                {

                    //multipars
                    #region

                    //TODO je potreba konfigurace parsovani nekde...
                    //code = Fask.Parsing.ParsingFactory.Parse(ck, Settings.Parsing_Config);
                    ////if (code is Parsing.Codes.GS1) // TODO : ? and GS1.Multiscan.Enabled ? 
                    //if ((code is Fask.Parsing.Codes.GS1) || (code is Fask.Parsing.Codes.SAB_GS1_Zavorky) || (code is Fask.Parsing.Codes.SAB_GS1_BALTON))
                    //{
                    //    try
                    //    {
                    //        this.ScannerStop();
                    //        //TODO tady je potreba multiparse okno...
                    //        //using (var mbscan = new Forms.MultiBarcode_Scan())
                    //        //{
                    //        //    mbscan.ParseBarcode(ck);
                    //        //    if (mbscan.ShowDialog() == DialogResult.Cancel)
                    //        //        return;
                    //        //    code = mbscan.Kod;
                    //        //}
                    //    }
                    //    finally
                    //    {
                    //        this.ScannerStart();
                    //    }
                    //}

                    //if (code is Fask.Parsing.Codes.Interfaces.ICodeBarcode)
                    //    ck = ((Fask.Parsing.Codes.Interfaces.ICodeBarcode)code).Barcode ?? string.Empty;

                    #endregion


                }

                if (ck.Length > 0)
                {

                    HledaniStatus found = await NajdiPolozkuPodleCK(
                        ck,
                        code,
                        out Polozky
                        );

                    if (found == HledaniStatus.Nenalezeno)
                    {
                        if (Konfigurace_Singleton.Instance.Prodej.PovolitNovouPolozku)
                        {
                            //pridatPolozkuNeexistujici(ck);
                        }
                        else
                        {
                            // je povoleno vyhledavani podle sarze a 
                            if (Konfigurace_Singleton.Instance.Prodej.PolozkyVyhledatPomociSarze && (_Item.TypDokladu != null && !_Item.TypDokladu.Iscfg_onl_dop_palNull() && _Item.TypDokladu.cfg_onl_dop_pal > 0))
                            {
                                string sklad_id = _Item.SkladZdroj != null ? _Item.SkladZdroj.skl_id : string.Empty;

                                #region zjisteni ID zdrojoveho a ciloveho skladu online (ANC)
                                // nacteni skladu online ...
                                if (Konfigurace_Singleton.Instance.Prodej.NacistSkladIDOnline)
                                {

                                    string sklad_id_dest = string.Empty;

                                    ProdejService.STATUS status = OnlineGetSklad(_Item.TypDokladu != null ? _Item.TypDokladu.doc_id : string.Empty, string.Empty, ck, out sklad_id, out sklad_id_dest);
                                    if (status == ProdejService.STATUS.ERROR) // chyba, ukoncit ...
                                        return;
                                    else if (string.IsNullOrEmpty(sklad_id) && string.IsNullOrEmpty(sklad_id_dest))
                                    {
                                        ShowMessage(this, string.Format(GetString(Resource.String.Prodej3ProdejListPolozkaSCarKodNeboSarziNenalezenaVeSkladuOnline), ck), "Error");
                                        return;
                                    }
                                }
                                #endregion

                                //ProdejService.Location ds = OnlineGetMaterial(string.Empty, _Item.SkladZdroj != null ? _Item.SkladZdroj.skl_id : string.Empty, ck);
                                //if (ds != null)
                                //{
                                //    // vratily se nejake zaznamy
                                //    if (ds.CZMST_SkladLokace_Stav.Count > 0)
                                //    {
                                //        // vytvorit novy prvek pro vlozeni pridatpolozku(zbozi)

                                //        Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zTmp = _katalogZbozi.CZMST095.NewCZMST095Row();
                                //        zTmp.ITEMNMBR = ds.CZMST_SkladLokace_Stav[0].ITEMNMBR;
                                //        zTmp.LOCNCODE = ds.CZMST_SkladLokace_Stav[0].IsLOCNCODENull() ? string.Empty : ds.CZMST_SkladLokace_Stav[0].LOCNCODE; // !! nastavit
                                //        zTmp.SERLTNUM = ds.CZMST_SkladLokace_Stav[0].SERLTNUM; // !! nastavit
                                //        zTmp.QTY = ds.CZMST_SkladLokace_Stav[0].QTYSHPPD;   // !! nastavit
                                //        zTmp.SKL_ID = ds.CZMST_SkladLokace_Stav[0].IsSKL_IDNull() ? string.Empty : ds.CZMST_SkladLokace_Stav[0].SKL_ID;   // !! nastavit
                                //        zTmp.QTYPACK = 0;
                                //        zTmp.CZ_SerNum_Track = 2; //na sarze ...
                                //        zTmp.CZ_SerNum_Delka = 0;
                                //        zTmp.CZ_Rez1_Track = 0;
                                //        zTmp.CZ_Rez2_Track = 0;
                                //        zTmp.CZ_Rez3_Track = 0;
                                //        zTmp.CZ_Rez4_Track = 0;
                                //        zTmp.VNDITNUM = string.Empty;
                                //        zTmp.CZ_CarKod = string.Empty;
                                //        zTmp.MJ = string.Empty;
                                //        // ... ???
                                //        //pridatPolozku(zTmp);
                                //        return;
                                //    }
                                //    else // nenalezeny dop. palety 
                                //        MessageBox.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejListPolozkaSCarKodNeboSarziNenalezenaOnline), ck), "info", MessageBoxButtons.OK);
                                //} // else  // chyba v online funkci, hlaska se zobrazuje primo
                            }
                            else if (Konfigurace_Singleton.Instance.Prodej.PolozkyVyhledatPomociSarze)  // polozka nenalezena a je zapnute hledani pomoci sarze
                            {
                                await MessageBoxAsync.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejListPolozkaSCarKodNeboSarziNenalezena), ck), "info", MessageBoxButtons.OK);
                            }
                            else // polozka nenalezena a hleda se pouze pomoci ck 
                            {
                                await MessageBoxAsync.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejListPolozkaSCarKodNenalezena), ck), "info", MessageBoxButtons.OK);
                            }
                        }
                    }
                    else if (found == HledaniStatus.NalezenJedenZaznam)
                    { 
                        await _Logika.pridatPolozku_New(
                            Polozky.CZMST095[0],
                            code: code,
                            _input_mode: _input_mode,
                            nmbrpal: _Item.NMBRPAL
                            );
                    }
                    else if (found == HledaniStatus.NalezenoViceZaznamu)
                    {
                        await MessageBoxAsync.Show(this, GetString(Resource.String.Prodej3ProdejListNalezenoViceZaznamuVyberRucne), "Warning", MessageBoxButtons.OK);

                        Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row zMST095Row = await zobrazeniList(Polozky);

                        await _Logika.pridatPolozku_New(
                                zMST095Row,
                                code: code,
                                _input_mode: _input_mode,
                                nmbrpal: _Item.NMBRPAL
                                );
                    }
                    else if (found == HledaniStatus.NalezenoViceJakNastavenyPocetZaznamu)
                    {
                        await MessageBoxAsync.Show(this, string.Format(GetString(Resource.String.Prodej3ProdejListNalezenoViceZaznamuUpresneteVyhledani), Konfigurace_Singleton.Instance.Prodej.GridViewRowCount), "Warning", MessageBoxButtons.OK);
                    }


                }

            }
            catch (Exception ex)
            {
                await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
            finally
            {
                Scanner_START();
                //ScannerStart();
                //if (MST_Global.OnScannerSound_Prodej_3)
                //{
                //    MySystem.Audio.PlaySound(Path.Combine(Main.SoundDir, "scannerready.wav"));
                //}
            }
        }

        #region Zasoby okno

        TaskCompletionSource<Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row> tcs_ZboziVyber = null;

        private Task<Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row> zobrazeniList(Fask.SQLiteDBs.DataSets.Zbozi zbozi)
        {
            tcs_ZboziVyber = new TaskCompletionSource<Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row>();

            try
            {

                Intent intent = new Intent(this, typeof(Prodej_Zasoby));
                Bundle bundleUser = new Bundle();
                bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(this.Uzivatel));
                intent.PutExtra(DataInfo_Static.User, bundleUser);

                if (zbozi != null)
                {
                    Bundle bundle_Z = new Bundle();
                    bundle_Z.PutBinder(DataInfo_Static.object_DT_095, new WrapperForBinder<Fask.SQLiteDBs.DataSets.Zbozi>(zbozi));
                    intent.PutExtra(DataInfo_Static.DT_095, bundle_Z);
                    this.StartActivityForResult(intent, ResoultCode_ZasobyVyber);
                }
                else
                {
                    intent.PutExtra(DataInfo_Static.Sklad_Zasoby, _Item.SkladZdroj.skl_id);
                    this.StartActivityForResult(intent, ResoultCode_AddZasobu);
                }




            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                tcs_ZboziVyber.SetException(ex);
            }

            return tcs_ZboziVyber.Task;

        }

        #endregion

        #region Palety okno

        TaskCompletionSource<bool> tcs_PaletaVyber = null;

        private Task<bool> zobrazeniPaleta()
        {
            tcs_PaletaVyber = new TaskCompletionSource<bool>();

            try
            {

                Intent intent = new Intent(this, typeof(Prodej_VyberPalety));
                Bundle bundleUser = new Bundle();
                bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(this.Uzivatel));
                intent.PutExtra(DataInfo_Static.User, bundleUser);

                Bundle bundle = new Bundle();
                bundle.PutBinder(DataInfo_Static.object_ProdejItem, new WrapperForBinder<Prodej.Prodej_Davka_Item>(_Item));
                intent.PutExtra(DataInfo_Static.ProdejItem, bundle);

                this.StartActivityForResult(intent, ResoultCode_ChangePaleta);
                

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                tcs_PaletaVyber.SetException(ex);
            }

            return tcs_PaletaVyber.Task;

        }

        #endregion

        /// <summary>
        /// Vyhledava polozky dle čaroveho kodu
        /// </summary>
        /// <param name="cislo"></param>
        /// <returns>0=nenalezeno, 1=nalezen jeden zaznam, 2=nalezeno vice zaznamu, 3=nalezeno vice jak 100zaznamu</returns>
        // private HledaniStatus NajdiPolozkuPodleCK
        private Task<HledaniStatus> NajdiPolozkuPodleCK
            (
            string ck,
            Fask.Parsing.Codes.BaseCode code,
            out Fask.SQLiteDBs.DataSets.Zbozi _katalogZbozi
            )
        {
            TaskCompletionSource<HledaniStatus> tcs = new TaskCompletionSource<HledaniStatus>();

            _katalogZbozi = new Fask.SQLiteDBs.DataSets.Zbozi();
            try
            {


                if (Konfigurace_Singleton.Instance.Prodej.PolozkyVyhledatPomociSarze)
                {
                    if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr) && !String.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr))
                        ck = ((Fask.Parsing.Codes.Interfaces.ICodeSerltnmbr)code).Serltnmbr;

                    if ((!Konfigurace_Singleton.Instance.Prodej.FiltrCiselnikSkladu || _Item.SkladZdroj == null) && (_Item.Odberatel == null || !Konfigurace_Singleton.Instance.Prodej.FiltrDodavatele))
                        DataInfo_Static.ProdejGO_Instance.controller_zbozi.FillByCarKodSarze(_katalogZbozi.CZMST095, ck);
                    else if (_Item.SkladZdroj == null && _Item.Odberatel != null)
                        DataInfo_Static.ProdejGO_Instance.controller_zbozi.FillByCarKodOdbIdSarze(_katalogZbozi.CZMST095, ck, _Item.Odberatel.odb_id);
                    else if (_Item.SkladZdroj != null) // && this._odberatel == null)
                        DataInfo_Static.ProdejGO_Instance.controller_zbozi.FillByCarKodSkladSarze(_katalogZbozi.CZMST095, ck, _Item.SkladZdroj.skl_id);
                    else
                    {
                        //ToDo: ...sklad i odberatel jsou zvoleni...
                    }
                }
                else
                {
                    if ((code != null) && (code is Fask.Parsing.Codes.Interfaces.ICodeBarcode) && !String.IsNullOrEmpty(((Fask.Parsing.Codes.Interfaces.ICodeBarcode)code).Barcode))
                        ck = ((Fask.Parsing.Codes.Interfaces.ICodeBarcode)code).Barcode;

                    if ((!Konfigurace_Singleton.Instance.Prodej.FiltrCiselnikSkladu || _Item.SkladZdroj == null) && (_Item.Odberatel == null || !Konfigurace_Singleton.Instance.Prodej.FiltrDodavatele))
                        DataInfo_Static.ProdejGO_Instance.controller_zbozi.FillByCarKod(_katalogZbozi.CZMST095, ck);
                    else if (_Item.SkladZdroj == null && _Item.Odberatel != null)
                        DataInfo_Static.ProdejGO_Instance.controller_zbozi.FillByCarKodOdbId(_katalogZbozi.CZMST095, ck, _Item.Odberatel.odb_id);
                    else if (_Item.SkladZdroj != null) // && this._odberatel == null)
                        DataInfo_Static.ProdejGO_Instance.controller_zbozi.FillByCarKodSklad(_katalogZbozi.CZMST095, ck, _Item.SkladZdroj.skl_id);
                    else
                    {
                        //ToDo: ...sklad i odberatel jsou zvoleni...
                    }
                }


                // Aktualizace pomocnych popisku
                DataInfo_Static.ProdejGO_Instance.controller_sklady.Update_SkladDescription(_katalogZbozi.CZMST095);

                //if (_katalogZbozi.CZMST095.Rows.Count == 1)
                //    _zbozi = _katalogZbozi.CZMST095[0];
                //else
                //    _zbozi = null;

                

                if (_katalogZbozi.CZMST095.Count == 0)
                    tcs.SetResult(HledaniStatus.Nenalezeno);
                else if (_katalogZbozi.CZMST095.Count == 1)
                    tcs.SetResult(HledaniStatus.NalezenJedenZaznam);
                else if (_katalogZbozi.CZMST095.Count > Konfigurace_Singleton.Instance.Prodej.GridViewRowCount)
                    tcs.SetResult(HledaniStatus.NalezenoViceJakNastavenyPocetZaznamu);
                else
                    tcs.SetResult(HledaniStatus.NalezenoViceZaznamu);



            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                tcs.SetResult(HledaniStatus.Nenalezeno);
            }

            return tcs.Task;
        }

        /// <summary>
        /// Online ziskani ID zdrojoveho a ciloveho skladu.
        /// </summary>
        /// <param name="doc_id">typ dokladu.</param>
        /// <param name="itemnmbr">itemnmbr.</param>
        /// <param name="serltnum">serltnum.</param>
        /// <param name="skl_id">skl_id (out parameter)</param>
        /// <param name="skl_id_dest">skl_id_dest (out parameter)</param>
        /// <returns>STATUS (OK, ERROR)</returns>
        private ProdejService.STATUS OnlineGetSklad(string doc_id, string itemnmbr, string serltnum, out string skl_id, out string skl_id_dest)
        {
            ProdejService.STATUS status = ProdejService.STATUS.ERROR;
            skl_id = string.Empty;
            skl_id_dest = string.Empty;

            try
            {

                ProdejServiceSession prodejService = new ProdejServiceSession();
                prodejService.Url = Config.Settings.Adresa + "Prodej.asmx";
                prodejService.Timeout = Config.Settings.TimeOut;
                prodejService.UpdateWebServiceCredentials();

                status = prodejService.GetSklad(Config.Settings.TerminalID, Uzivatel.ID.Value, doc_id, itemnmbr, serltnum, out skl_id, out skl_id_dest);


            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex.Message, "Prodej.ProdejList, OnlineGetSklad");
                ShowErrorMessage(this, ex);

                return ProdejService.STATUS.ERROR;
            }

            return ProdejService.STATUS.OK;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {

            if (requestCode == ResoultCode_AddZasobu)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    var bundle = data?.GetBundleExtra(DataInfo_Static.Row095);
                    var binder = bundle?.GetBinder(DataInfo_Static.object_Row095);
                    if (binder != null)
                    {
                        var Row095 = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row>)binder).getData();
                        Logika.Prodej_SberDat_LogikaAsync _Logika = new Logika.Prodej_SberDat_LogikaAsync(this, _Item.CisloDavky.Value, _Item.TypDokladu);

                        _Logika.pridatPolozku_NewAsync(
                             Row095,
                             nmbrpal: _Item.NMBRPAL
                             );


                        //var confirmThread = new Thread(() => _Logika.pridatPolozku(
                        //    Row095,
                        //    null
                        //    ));
                        //confirmThread.Start();

                    }
                }
                else if (resultCode == Result.Canceled)
                {
                    GetUzivatelFromIntent(data);
                }
            }
            else if (requestCode == ResoultCode_ChangePaleta)
            {
                Scanner_START();

                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    var bundleTD = Intent?.GetBundleExtra(DataInfo_Static.ProdejItem);
                    var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_ProdejItem);
                    if (binderTD != null)
                    {
                        _Item = ((WrapperForBinder<Prodej.Prodej_Davka_Item>)binderTD).getData();
                    }
                }
                else if (resultCode == Result.Canceled)
                {
                    GetUzivatelFromIntent(data);
                }
            }
            else if (requestCode == ResoultCode_ZasobyVyber)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    var bundle = data?.GetBundleExtra(DataInfo_Static.Row095);
                    var binder = bundle?.GetBinder(DataInfo_Static.object_Row095);
                    if (binder != null)
                    {
                        var Row095 = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row>)binder).getData();
                        tcs_ZboziVyber.SetResult(Row095);
                        return;
                    }
                }
                else if (resultCode == Result.Canceled)
                {
                    GetUzivatelFromIntent(data);
                }
            }
            else if (requestCode == ResoultCode_CilovySklad)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);


                    var bundleTD = data?.GetBundleExtra(DataInfo_Static.PPP_out);
                    var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_PPP_out);
                    var ppp_data = ((WrapperForBinder<Logika.Prodej_SberDat_LogikaAsync.O_GetData>)binderTD).getData();

                    TCS_CilovySkladAsync.SetResult(ppp_data);

                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_CilovySkladAsync.SetResult(new Logika.Prodej_SberDat_LogikaAsync.O_GetData()
                    {
                        status = false
                    });
                }
            }
            else if (requestCode == ResoultCode_VyberMaterial)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    decimal? VyberMaterial_QTY = null;
                    var VyberMaterial_LOCNCODE = data?.GetStringExtra(DataInfo_Static.Mat_LOCNCODE);
                    var VyberMaterial_SERLTNUM = data?.GetStringExtra(DataInfo_Static.Mat_SERLTNUM);

                    var qtytmp = data?.GetStringExtra(DataInfo_Static.Mat_QTY);

                    if (!string.IsNullOrEmpty(qtytmp))
                    {
                        VyberMaterial_QTY = decimal.Parse(qtytmp);
                    }

                    TCS_ZadejVyberMaterialuAsync.SetResult(new Logika.Prodej_SberDat_LogikaAsync.O_GetData()
                    {
                        status = true,
                        QTY = VyberMaterial_QTY,
                        SERLTNUM = VyberMaterial_SERLTNUM,
                        LOCNCODE = VyberMaterial_LOCNCODE
                    });

                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_ZadejVyberMaterialuAsync.SetResult(new Logika.Prodej_SberDat_LogikaAsync.O_GetData()
                    {
                        status = false
                    });
                }
            }
            else if (requestCode == ResoultCode_Lokace)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);

                    var Lokace_LOCNCODE = data?.GetStringExtra(DataInfo_Static.Lok_Lokace);
                    //_IsCurrentlyInConfirmProcess_Lokace = false;


                    TCS_GetLokaciAsync.SetResult(new Logika.Prodej_SberDat_LogikaAsync.O_GetData()
                    {
                        status = true,
                        LOCNCODE = Lokace_LOCNCODE
                    });

                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_GetLokaciAsync.SetResult(new Logika.Prodej_SberDat_LogikaAsync.O_GetData()
                    {
                        status = false
                    });
                }
            }
            else if (requestCode == ResoultCode_PPP)
            {
                if (resultCode == Result.Ok)
                {
                    GetUzivatelFromIntent(data);


                    var bundleTD = data?.GetBundleExtra(DataInfo_Static.PPP_out);
                    var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_PPP_out);
                    var ppp_data = ((WrapperForBinder<Logika.Prodej_SberDat_LogikaAsync.O_GetData>)binderTD).getData();

                    TCS_PPP_Async.SetResult(ppp_data);

                }
                else if (resultCode == Result.Canceled)
                {
                    TCS_PPP_Async.SetResult(new Logika.Prodej_SberDat_LogikaAsync.O_GetData()
                    {
                        status = false
                    });
                }
            }

        }

        private void GetUzivatelFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.User);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_User);
            Uzivatel = ((WrapperForBinder<Users>)binderTD).getData();
        }

        public void UpdateForm()
        {
            try
            {
                RunOnUiThread(() =>
                {

                    Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dtdi = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable();
                    if (PathToDavka != null)
                    {
                        using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej conPro = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Prodej(PathToDavka))
                        {
                            conPro.Fill_DI(dtdi);
                        }
                    }

                    mAdapter = new Prodej_SberDat_Item_Adapter(this, new Prodej_SberDat_Seznam(dtdi));
                    mAdapter.ItemClick += MAdapter_ItemClick;
                    mAdapter.ItemLongClick += MAdapter_ItemLongClick;
                    mRecycleView.SetAdapter(mAdapter);
                });
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        #region SSCC Paleta

        /// <summary>
        /// Metoda pro online generovani cisla palety
        /// </summary>
        //private void PerformGenerovatCisloPalety()
        //{
        //    try
        //    {
        //        ScannerStop();
        //        Cursor.Current = Cursors.WaitCursor;
        //        nmbrpal = Prodej_3.ProdejMain.prodejInstance.globalObject.servis_expedice.SSCC_Generovat(MST_Global.TerminalID, MST_Global.UserID, _skladZdroj != null ? _skladZdroj.skl_id : string.Empty);
        //    }
        //    catch (Exception ex)
        //    {
        //        Cursor.Current = Cursors.Default;
        //        Logging.Log.Write(ex.Message, "Expedice.ExpedicePolozkyList, PerformGenerovatCisloPalety");
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //    }
        //    finally
        //    {
        //        this.UpdateForm();
        //        ScannerStart();
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

        /// <summary>
        /// Metoda pro zmenu cisla palety
        /// </summary>
        //private void PerformZmenitCisloPalety()
        //{
        //    try
        //    {
        //        ScannerStop();
        //        // zadani cisla palety
        //        string kod = string.Empty;
        //        using (SejmiKodForm skf = new SejmiKodForm("Číslo palety", SejmiKodForm.TypeOfCode.AlphaNumeric, 0, false, false, string.Empty))
        //        {
        //            skf.Text = "Zadejte číslo palety";
        //            DialogResult dr = skf.ShowDialog();

        //            if (dr != DialogResult.OK)
        //                return;

        //            kod = skf.Kod;
        //        }

        //        Cursor.Current = Cursors.WaitCursor;
        //        //nmbrpal = wsExpedice.SSCC_Get(MST_Global.TerminalID, MST_Global.UserID, sklad != null ? sklad.skl_id : string.Empty, _Hlavicka.ID, pal);
        //        throw new NotImplementedException("Implementovat ... ");
        //        Fask.MST_W.ExpediceService.SSCC sscc = null;
        //        //Fask.MST_W.ExpediceService.SSCC sscc = wsExpedice.SSCC_Get(MST_Global.TerminalID, MST_Global.UserID, _skladZdroj != null ? _skladZdroj.skl_id : string.Empty, _Hlavicka.ID, kod);

        //        // TODO: vyhledani cisla palety v davce

        //        if (sscc == null)
        //        {
        //            Cursor.Current = Cursors.Default;
        //            MessageBoxBig.Show(string.Format("Zadané číslo palety '{0}' nebylo nalezeno", kod), this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
        //        }
        //        else
        //            nmbrpal = sscc;
        //    }
        //    catch (Exception ex)
        //    {
        //        Cursor.Current = Cursors.Default;
        //        Logging.Log.Write(ex.Message, "Prodej_3.ProdejList, PerformZmenitCisloPalety");
        //        MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
        //    }
        //    finally
        //    {
        //        this.UpdateForm();
        //        ScannerStart();
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

        #endregion

        #region Tisk

        /// <summary>
        /// Metoda tisk Paleta
        /// </summary>
        private async Task PerformTiskPaleta()
        {
            try
            {

                bool vytisteno = false;
            
                if (_Item.NMBRPAL == null)
                {
                    await MessageBoxAsync.Show(this, "Není vybrán nasnímaný záznam", "Error", MessageBoxButtons.OK);
                    return;
                }

                //  tisk soupisu
                Dictionary<string, string> dataHlavicka = new Dictionary<string, string>();
                List<Dictionary<string, string>> dataRadky = new List<Dictionary<string, string>>();
                Dictionary<string, string> dataPaticka = new Dictionary<string, string>();

                // Priprava parametru pro tisk soupisu ... 
                
                #region Hlavicka

                dataHlavicka.Add("odb_desc", _Item.Odberatel != null ? _Item.Odberatel.odb_desc.Trim() : string.Empty);
                dataHlavicka.Add("odb_ico", _Item.Odberatel != null && !_Item.Odberatel.Isodb_icoNull() ? _Item.Odberatel.odb_ico.Trim() : string.Empty);
                dataHlavicka.Add("odb_carcode", _Item.Odberatel != null && !_Item.Odberatel.Isodb_carcodeNull() ? _Item.Odberatel.odb_carcode.Trim() : string.Empty);
                dataHlavicka.Add("odb_cisloOr", _Item.Odberatel != null && !_Item.Odberatel.Isodb_cisloOrNull() ? _Item.Odberatel.odb_cisloOr.Trim() : string.Empty);
                dataHlavicka.Add("odb_dic", _Item.Odberatel != null && !_Item.Odberatel.Isodb_dicNull() ? _Item.Odberatel.odb_dic.Trim() : string.Empty);
                dataHlavicka.Add("odb_Dodavatel", _Item.Odberatel != null && !_Item.Odberatel.Isodb_DodavatelNull() ? _Item.Odberatel.odb_Dodavatel.ToString() : string.Empty);
                dataHlavicka.Add("odb_misto", _Item.Odberatel != null && !_Item.Odberatel.Isodb_mistoNull() ? _Item.Odberatel.odb_misto.Trim() : string.Empty);
                dataHlavicka.Add("odb_Odberatel", _Item.Odberatel != null && !_Item.Odberatel.Isodb_OdberatelNull() ? _Item.Odberatel.odb_Odberatel.ToString() : string.Empty);
                dataHlavicka.Add("odb_psc", _Item.Odberatel != null && !_Item.Odberatel.Isodb_pscNull() ? _Item.Odberatel.odb_psc.Trim() : string.Empty);
                dataHlavicka.Add("odb_ulice", _Item.Odberatel != null && !_Item.Odberatel.Isodb_uliceNull() ? _Item.Odberatel.odb_ulice.Trim() : string.Empty);
                //dataHlavicka.Add("NMBRPAL", (_Item.NMBRPAL != null && !string.IsNullOrEmpty(_Item.NMBRPAL.sscc)) ? _Item.NMBRPAL.sscc.Trim() : string.Empty);


                #region SSCC 

                //skladani caroveho kodu GS1 --START---------------------
                //zadat key
                //zadat value

                string GS1_KOD_1_1D = string.Empty;
                string GS1_KOD_1_TX = string.Empty;
                string GS1_KOD_2_1D = string.Empty;
                string GS1_KOD_2_TX = string.Empty;
                string SSCC = string.Empty;
                string SSCC_bez_nul = string.Empty;
               
                //------------START-DATA----------------
                //dotahovat data SSCC


                if (_Item.NMBRPAL != null && !string.IsNullOrEmpty(_Item.NMBRPAL.sscc))
                {
                    //SSCC a WEIGHT doplnit logiku dohledani
                    //SSCC a WEIGHT natvrdo zadano
                    SSCC = _Item.NMBRPAL.sscc.Trim();
                    SSCC_bez_nul = SSCC.Substring(2);
                }

                if (!dataHlavicka.ContainsKey("SSCC"))
                    dataHlavicka.Add("SSCC", SSCC_bez_nul);

                //------------END-DATA----------------

                Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dtdiHead = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable();
                DataInfo_Static.ProdejGO_Instance.controller_prodej.Fill_DI_ByNmbrpal(dtdiHead, (_Item.NMBRPAL != null && !string.IsNullOrEmpty(_Item.NMBRPAL.sscc)) ? _Item.NMBRPAL.sscc.Trim() : string.Empty);

                string Barcode = string.Empty;
                decimal qty = 0;

                if (dtdiHead.Count > 0)
                {
                    Barcode = dtdiHead[0].VNDITNUM.Trim();
                    qty = dtdiHead[0].QTYSHPPD;

                    #region Vlozeni info o polozke prvni nalezene

                    dataHlavicka.Add("ITEMNMBR", dtdiHead[0].ITEMNMBR.Trim());
                    dataHlavicka.Add("VNDITNUM", dtdiHead[0].IsVNDITNUMNull() ? string.Empty : dtdiHead[0].VNDITNUM.Trim());
                    dataHlavicka.Add("BarcodeP", dtdiHead[0].IsVNDITNUMNull() ? string.Empty : dtdiHead[0].VNDITNUM.Trim());
                    dataHlavicka.Add("CZ_CarKod", dtdiHead[0].IsCZ_CarKodNull() ? string.Empty : dtdiHead[0].CZ_CarKod.Trim());

                    dataHlavicka.Add("QTYSHPPD", dtdiHead[0].QTYSHPPD.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                    dataHlavicka.Add("qty", dtdiHead[0].QTYSHPPD.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));

                    dataHlavicka.Add("WEIGHT", dtdiHead[0].IsWEIGHTNull() ? string.Empty : dtdiHead[0].WEIGHT.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));

                    dataHlavicka.Add("UserID", dtdiHead[0].IsUSER_IDNull() ? "?" : dtdiHead[0].USER_ID.ToString());

                    try
                    {
                        if (!dtdiHead[0].IsITEMDESCNull())
                        {
                            dataHlavicka.Add("ITEMDESC", dtdiHead[0].ITEMDESC.Trim());
                        }
                        else
                            dataHlavicka.Add("ITEMDESC", "?");

                    }
                    catch
                    {
                        dataHlavicka.Add("ITEMDESC", "?");
                    }

                    #endregion

                }

                GS1_KOD_1_1D = "02" + Barcode.PadLeft(14, '0') + "37" + qty.ToString("0000") + "";
                GS1_KOD_1_TX = "(02)" + Barcode.PadLeft(14, '0') + "(37)" + qty.ToString("0000") + ""; //(02) (37)
                GS1_KOD_2_1D = SSCC; //SSCC neni v production
                GS1_KOD_2_TX = "(00)" + SSCC_bez_nul; // SSCC neni v production

                if (!dataHlavicka.ContainsKey("GS1_KOD_1_1D"))
                    dataHlavicka.Add("GS1_KOD_1_1D", GS1_KOD_1_1D);

                if (!dataHlavicka.ContainsKey("GS1_KOD_1_TX"))
                    dataHlavicka.Add("GS1_KOD_1_TX", GS1_KOD_1_TX);

                if (!dataHlavicka.ContainsKey("GS1_KOD_2_1D"))
                    dataHlavicka.Add("GS1_KOD_2_1D", GS1_KOD_2_1D);

                if (!dataHlavicka.ContainsKey("GS1_KOD_2_TX"))
                    dataHlavicka.Add("GS1_KOD_2_TX", GS1_KOD_2_TX);

                //skladani caroveho kodu GS1 --END---------------------
                if (!dataHlavicka.ContainsKey("SOURCE"))
                    dataHlavicka.Add("SOURCE", "Manual");

                #endregion


                #endregion

                #region jsou povolena strediska

                if ((_Item.TypDokladu != null && _Item.TypDokladu.cfg_str > 0))
                {
                    dataHlavicka.Add("str_desc", _Item.Stredisko != null && !_Item.Stredisko.Isstr_descNull() ? _Item.Stredisko.str_desc.Trim() : string.Empty);
                    dataHlavicka.Add("str_carcode", _Item.Stredisko != null && !_Item.Stredisko.Isstr_carcodeNull() ? _Item.Stredisko.str_carcode.Trim() : string.Empty);
                } 

                #endregion

                #region nacteni uzivatele

                dataHlavicka.Add("LOGIN", Uzivatel.Login ?? string.Empty);
                dataHlavicka.Add("FIRSTNAME", string.IsNullOrEmpty(Uzivatel.FIRSTNAME) ? string.Empty : Uzivatel.FIRSTNAME.Trim());
                dataHlavicka.Add("SECONDNAME", string.IsNullOrEmpty(Uzivatel.SECONDNAME) ? string.Empty : Uzivatel.SECONDNAME.Trim());

                #endregion

                string mena_id = string.Empty;
                string mena_symbol = string.Empty;

                string mena_idM = string.Empty;
                string mena_symbolM = string.Empty;
                decimal? mena_kurzM;
                mena_kurzM = null;

                if (String.IsNullOrEmpty(mena_idM))
                {
                    var dtDIData = DataInfo_Static.ProdejGO_Instance.controller_prodej.GetData_DI();
                    foreach (var item in dtDIData.Rows)
                    {
                        Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow row = (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow)item;
                        if(!row.Ismena_IDMNull())
                        {
                            mena_idM = row.mena_IDM.Trim();
                            break;
                        }
                    }
                }


                
                #region Mapovani men na zastupne symboly

                mena_symbol = _Translations.Meny.GetSymbol(mena_id);
                mena_symbolM = _Translations.Meny.GetSymbol(mena_idM);

                dataHlavicka.Add("mena_id", mena_id);
                dataHlavicka.Add("mena_symbol", mena_symbol);
                dataHlavicka.Add("mena_idM", mena_idM);
                dataHlavicka.Add("mena_symbolM", mena_symbolM); 

                #endregion


                #region V jake mene se chce tisknout?

                string mena_id_print = string.Empty;
                if (_Item.Odberatel != null && !_Item.Odberatel.Ismena_IDNull())
                    mena_id_print = _Item.Odberatel.mena_ID.Trim();
                if (_Item.Mena != null)
                    mena_id_print = _Item.Mena.mena_ID.Trim();
                dataHlavicka.Add("mena_id_print", mena_id_print); 

                #endregion

              
                #region promenne pro paticku

                decimal p_sum_mn = 0; // celkem mnozstvi
                decimal p_tax = 0; //pouzita dan

                decimal p_sum_bdph = 0; // celkem cena bez DPH
                decimal p_sum_sdph = 0; // celkem cena s DPH
                decimal p_sum_dph = 0; // celkem DPH za vse

                decimal p_sum_bdphM = 0; // celkem cena bez DPH
                decimal p_sum_sdphM = 0; // celkem cena s DPH
                decimal p_sum_dphM = 0; // celkem DPH za vse 

                #endregion

           
                Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dtdi = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable();             
                DataInfo_Static.ProdejGO_Instance.controller_prodej.Fill_DI_ByNmbrpal(dtdi, (_Item.NMBRPAL != null && !string.IsNullOrEmpty(_Item.NMBRPAL.sscc)) ? _Item.NMBRPAL.sscc.Trim() : string.Empty);
             
                using (Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi ConZbo = new Fask.SQLiteDBs.Controllers.SQLite_Controller_Zbozi(DataInfo_Static.CiselnikZboziDB))
                {
                    #region ForEach

                    ConZbo.Connection_Open();

                    try
                    {
                        //int cisloZaznamu = 0;
                        foreach (var item in dtdi.Rows)
                        {
                            Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow drdi = (Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow)item;

                            Dictionary<string, string> dataRadek = new Dictionary<string, string>();

                            dataRadek.Add("ITEMNMBR", drdi.ITEMNMBR.Trim());
                            dataRadek.Add("VNDITNUM", drdi.IsVNDITNUMNull() ? string.Empty : drdi.VNDITNUM.Trim());
                            dataRadek.Add("CZ_CarKod", drdi.IsCZ_CarKodNull() ? string.Empty : drdi.CZ_CarKod.Trim());

                            try
                            {
                                if (!drdi.IsITEMDESCNull())
                                {
                                    dataRadek.Add("ITEMDESC", drdi.ITEMDESC.Trim());
                                }
                                else
                                    dataRadek.Add("ITEMDESC", ConZbo.GetDataByPolozkacisloLike(drdi.ITEMNMBR)[0].ITEMDESC.Trim());

                            }
                            catch
                            {
                                dataRadek.Add("ITEMDESC", "?");
                            }

                            dataRadek.Add("ITEMCODE", drdi.IsITEMCODENull() ? string.Empty : drdi.ITEMCODE.Trim());
                            dataRadek.Add("REZ_1", drdi.REZ_1.Trim());
                            dataRadek.Add("REZ_2", drdi.REZ_2.Trim());
                            dataRadek.Add("QTYSHPPD", drdi.QTYSHPPD.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                            dataRadek.Add("QTYPACK", drdi.IsQTYPACKNull() ? string.Empty : drdi.QTYPACK.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                            dataRadek.Add("QTYSHPPDMJ", drdi.QTYSHPPDMJ.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                            dataRadek.Add("NMBRPAL", drdi.IsNMBRPALNull() ? string.Empty : drdi.NMBRPAL.Trim());

                            decimal priceMJTaxWith = 0;       // cena/MJ s DPH[mena]
                            decimal priceMJTaxWithout = 0;    // cena/MJ bez DPH[mena]
                            decimal priceMJTax = 0;           // MJ DPH[mena]
                            decimal priceTaxWith = 0;       // cena s DPH[mena]
                            decimal priceTaxWithout = 0;    // cena bez DPH[mena]
                            decimal priceTax = 0;           // DPH[mena]
                            decimal priceMJTaxWithM = 0;       // cena/MJ s DPH[mena]
                            decimal priceMJTaxWithoutM = 0;    // cena/MJ bez DPH[mena]
                            decimal priceMJTaxM = 0;           // MJ DPH[mena]
                            decimal priceTaxWithM = 0;       // cena s DPH[mena]
                            decimal priceTaxWithoutM = 0;    // cena bez DPH[mena]
                            decimal priceTaxM = 0;           // DPH[mena]
                            decimal Tax = 0;                // vyse DPH[%]
                            string MJ = string.Empty;

                            MJ = drdi.MJ.Trim();

                            if (drdi.IsWITHTAXNull())
                            {
                                if (drdi.WITHTAX == 1)
                                {
                                    priceMJTaxWith = drdi.AMOUNPIE;
                                    priceMJTaxWithout = drdi.AMOUNPIE - drdi.TAXAMPIE;
                                    priceTaxWith = drdi.AMOUNPIE * drdi.QTYSHPPD;
                                    priceTaxWithout = drdi.AMOUNPIE * drdi.QTYSHPPD - drdi.TAXAMPIE * drdi.QTYSHPPD;

                                    if (!drdi.Ismena_IDMNull() && !drdi.IsAMOUNPIEMNull() && !drdi.IsTAXAMPIEMNull()
                                        && mena_idM == drdi.mena_IDM.Trim())
                                    { // je mena a je cena za kus a je dan za kus, tak spocitam toto
                                      // a mena se shoduje s touto menou ... 
                                        priceMJTaxWithM = drdi.AMOUNPIEM;
                                        priceMJTaxWithoutM = drdi.AMOUNPIEM - drdi.TAXAMPIEM;
                                        priceTaxWithM = drdi.AMOUNPIEM * drdi.QTYSHPPD;
                                        priceTaxWithoutM = drdi.AMOUNPIEM * drdi.QTYSHPPD - drdi.TAXAMPIEM * drdi.QTYSHPPD;
                                    }
                                    else
                                    { // jinak musim prepocitat do meny, kterou chci kurzem z hlavni meny ... 
                                      // pokud ovsem mam k dispozici prepocitaci kurz pro pozadovanou menu a neni to mena hlavni ... :)
                                        priceMJTaxWithM = priceMJTaxWith * (mena_kurzM ?? 0);
                                        priceMJTaxWithoutM = priceMJTaxWithout * (mena_kurzM ?? 0);
                                        priceTaxWithM = priceTaxWith * (mena_kurzM ?? 0);
                                        priceTaxWithoutM = priceTaxWithout * (mena_kurzM ?? 0);
                                    }
                                }
                                else
                                {
                                    priceMJTaxWithout = drdi.AMOUNPIE;
                                    priceMJTaxWith = drdi.AMOUNPIE + drdi.TAXAMPIE;
                                    priceTaxWithout = drdi.AMOUNPIE * drdi.QTYSHPPD;
                                    priceTaxWith = drdi.AMOUNPIE * drdi.QTYSHPPD + drdi.TAXAMPIE * drdi.QTYSHPPD;

                                    if (!drdi.Ismena_IDMNull() && !drdi.IsAMOUNPIEMNull() && !drdi.IsTAXAMPIEMNull()
                                        && mena_idM == drdi.mena_IDM.Trim())
                                    {
                                        priceMJTaxWithoutM = drdi.AMOUNPIEM;
                                        priceMJTaxWithM = drdi.AMOUNPIEM + drdi.TAXAMPIEM;
                                        priceTaxWithoutM = drdi.AMOUNPIEM * drdi.QTYSHPPD;
                                        priceTaxWithM = drdi.AMOUNPIEM * drdi.QTYSHPPD + drdi.TAXAMPIEM * drdi.QTYSHPPD;
                                    }
                                    else
                                    { // jinak musim prepocitat do meny, kterou chci kurzem z hlavni meny ... 
                                      // pokud ovsem mam k dispozici prepocitaci kurz pro pozadovanou menu a neni to mena hlavni ... :)
                                        priceMJTaxWithoutM = priceMJTaxWithout * (mena_kurzM ?? 0);
                                        priceMJTaxWithM = priceMJTaxWith * (mena_kurzM ?? 0);
                                        priceTaxWithoutM = priceTaxWithout * (mena_kurzM ?? 0);
                                        priceTaxWithM = priceTaxWith * (mena_kurzM ?? 0);
                                    }
                                }
                                priceMJTax = priceMJTaxWith - priceMJTaxWithout;
                                priceTax = priceTaxWith - priceTaxWithout;

                                priceMJTaxM = priceMJTaxWithM - priceMJTaxWithoutM;
                                priceTaxM = priceTaxWithM - priceTaxWithoutM;

                                Tax = Math.Round((priceTax / (priceTaxWithout == 0 ? 1 : priceTaxWithout)) * 100);

                                // Cena MJ
                                dataRadek.Add("pricemjtaxwith", priceMJTaxWith.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                dataRadek.Add("pricemjtaxwithout", priceMJTaxWithout.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                dataRadek.Add("pricemjtax", priceMJTax.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                // Cena MJ v mene
                                dataRadek.Add("pricemjtaxwithM", priceMJTaxWithM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                dataRadek.Add("pricemjtaxwithoutM", priceMJTaxWithoutM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                dataRadek.Add("pricemjtaxM", priceMJTaxM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                // Cena celkem za radek
                                dataRadek.Add("pricetaxwith", priceTaxWith.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                dataRadek.Add("pricetaxwithout", priceTaxWithout.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                dataRadek.Add("pricetax", priceTax.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                // Cena celkem za radek v mene
                                dataRadek.Add("pricetaxwithM", priceTaxWithM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                dataRadek.Add("pricetaxwithoutM", priceTaxWithoutM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                dataRadek.Add("pricetaxM", priceTaxM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                                // Sazba dane
                                dataRadek.Add("tax", Tax.ToString("0", System.Globalization.CultureInfo.InvariantCulture));

                                dataRadek.Add("mena_id", mena_id);
                                dataRadek.Add("mena_symbol", mena_symbol);
                                dataRadek.Add("mena_idM", mena_idM);
                                dataRadek.Add("mena_symbolM", mena_symbolM);

                                // V jake mene se bude tisknout
                                //dataRadek.Add("mena_id_print", mena_id_print);
                            }
                            dataRadky.Add(dataRadek);

                            //vypocet sum pro paticku ...
                            p_sum_mn += drdi.QTYSHPPD;
                            p_tax = Tax;

                            p_sum_bdph += priceTaxWithout;
                            p_sum_sdph += priceTaxWith;
                            p_sum_dph += priceTax;

                            p_sum_bdphM += priceTaxWithoutM;
                            p_sum_sdphM += priceTaxWithM;
                            p_sum_dphM += priceTaxM;
                        }
                    }
                    catch (Exception ex)
                    {
                        Fask.Logging.ExceptionHandler2.Handle(ex);
                    }
                    finally
                    {
                        ConZbo.Connection_Close();
                    }

                    #endregion
                }

                // Paticka
                dataPaticka.Add("sum_mn", p_sum_mn.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                dataPaticka.Add("tax", p_tax.ToString("0", System.Globalization.CultureInfo.InvariantCulture));
                // Suma
                dataPaticka.Add("sum_bdph", p_sum_bdph.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                dataPaticka.Add("sum_sdph", p_sum_sdph.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                dataPaticka.Add("sum_dph", p_sum_dph.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                // Suma v mene
                dataPaticka.Add("sum_bdphM", p_sum_bdphM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                dataPaticka.Add("sum_sdphM", p_sum_sdphM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                dataPaticka.Add("sum_dphM", p_sum_dphM.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));

                dataPaticka.Add("datetime", DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture));
                dataPaticka.Add("mena_id", mena_id);
                dataPaticka.Add("mena_symbol", mena_symbol);
                dataPaticka.Add("mena_idM", mena_idM);
                dataPaticka.Add("mena_symbolM", mena_symbolM);
                
                vytisteno = await ProdejTisk.PrintPaletaSendToPrinter(
                    this, 
                    dataHlavicka, 
                    dataRadky, 
                    dataPaticka, 
                    1);

            }
            catch (Exception ex)
            {
                
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowErrorMessage(this, ex);
            }

        }


        #endregion

    }

    //public class LocationKeyListner : Java.Lang.Object, View.IOnKeyListener
    //{

    //    //public event EventHandler<int> KeyClick;

    //    public bool OnKey(View v, [GeneratedEnum] Keycode keyCode, KeyEvent e)
    //    {

    //        var Con = v.Context;

    //        if (Con != null && Con is Prodej_SberDat)
    //        {
    //            Prodej_SberDat s = (Prodej_SberDat)Con;
    //            //s.OnKeyDown(keyCode, e);

    //            int pos = s.mAdapter.vh.AdapterPosition;

    //        }

    //        return true;
    //    }

    //    //private void OnClick(int obj)
    //    //{
    //    //    if (ItemClick != null)
    //    //        ItemClick(this, obj);
    //    //}

    //}
}