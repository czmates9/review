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
using System.Linq;
using MES_Android.OdvadeniVyroby.Logika;

namespace MES_Android.OdvadeniVyroby
{
    [Activity(Label = "@string/OperacePotvrzeni_Label")]
    public class OperacePotvrzeni : Base_Aktivita, NavigationView.IOnNavigationItemSelectedListener
    {

        #region Parametry
        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;
        public Users Uzivatel = null;
        NavigationView mNavigationView;

        private DateTime? lastoperationuserdt = null;

        private Button btn_OK;
        private Button btn_Storno;
        private Button btn_Korekce;
        private Button btn_Materialy;

        private TextView OperacePotvrzeni_textview_lblPolozka;
        private TextView OperacePotvrzeni_textview_lblPracovnik;
        private TextView OperacePotvrzeni_textview_lblStroj;
        private TextView OperacePotvrzeni_textview_lblPripravnyCas;
        private TextView OperacePotvrzeni_textview_lblJednotkovyCas;
        private TextView OperacePotvrzeni_textview_lblKorekceCasu;
        private TextView OperacePotvrzeni_textview_lblCelkovyCas;
        private TextView OperacePotvrzeni_textview_lblKusu;


        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow VPP { get; set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow Machine { get; set; }
        public decimal PocetOdvedeno { get; set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik { get; set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable ProductionSDT { get; set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow { get; set; }

        System.Threading.Timer timerDateTimeOperaceUpdate = null;
        private bool PriznakNacteneMaterialy;

        #endregion

        #region Parametry pro InputKod2

        public TaskCompletionSource<O_GetInputKod2Async> TCS_GetInputKod2Async;
        public int ResoultCode_InputKod2 = 123;

        public class O_GetInputKod2Async
        {
            public bool status;
            public string Kod;
        }

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            try
            {
                this.DataWedge_Scanner_Disable();
                SetContentView(Resource.Layout.OperacePotvrzeni);
                Dexter.WithActivity(this)
                   .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                   Manifest.Permission.WriteExternalStorage,
                                   Manifest.Permission.AccessNetworkState,
                                   Manifest.Permission.Camera)
                   .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                   .WithErrorListener(new SampleErrorListner())
                   .Check();


                 GetUzivatelFromIntent(Intent);

                GetVPPFromIntent(Intent);
                GetMachineFromIntent(Intent);
                GetPocetOdvedenoFromIntent(Intent);
                GetPracovnikFromIntent(Intent);
                GetProductionSDTFromIntent(Intent);
                GetProductionRowFromIntent(Intent);

                #region TextView

                OperacePotvrzeni_textview_lblPolozka = FindViewById<TextView>(Resource.Id.OperacePotvrzeni_textview_lblPolozka);
                OperacePotvrzeni_textview_lblPracovnik= FindViewById<TextView>(Resource.Id.OperacePotvrzeni_textview_lblPracovnik);
                OperacePotvrzeni_textview_lblStroj= FindViewById<TextView>(Resource.Id.OperacePotvrzeni_textview_lblStroj);
                OperacePotvrzeni_textview_lblPripravnyCas= FindViewById<TextView>(Resource.Id.OperacePotvrzeni_textview_lblPripravnyCas);
                OperacePotvrzeni_textview_lblJednotkovyCas= FindViewById<TextView>(Resource.Id.OperacePotvrzeni_textview_lblJednotkovyCas);
                OperacePotvrzeni_textview_lblKorekceCasu= FindViewById<TextView>(Resource.Id.OperacePotvrzeni_textview_lblKorekceCasu);
                OperacePotvrzeni_textview_lblCelkovyCas= FindViewById<TextView>(Resource.Id.OperacePotvrzeni_textview_lblCelkovyCas);
                OperacePotvrzeni_textview_lblKusu= FindViewById<TextView>(Resource.Id.OperacePotvrzeni_textview_lblKusu);

                OperacePotvrzeni_textview_lblPolozka.Text = "-";
                OperacePotvrzeni_textview_lblPracovnik.Text = "-";
                OperacePotvrzeni_textview_lblStroj.Text = "-";
                OperacePotvrzeni_textview_lblPripravnyCas.Text = "-";
                OperacePotvrzeni_textview_lblJednotkovyCas.Text = "-";
                OperacePotvrzeni_textview_lblKorekceCasu.Text = "-";
                OperacePotvrzeni_textview_lblCelkovyCas.Text = "-";
                OperacePotvrzeni_textview_lblKusu.Text = "-";



                UpdateForm();

                #endregion


                btn_OK = FindViewById<Button>(Resource.Id.OperacePotvrzeni_btn_OK);
                btn_Storno = FindViewById<Button>(Resource.Id.OperacePotvrzeni_btn_Storno);

                btn_Korekce = FindViewById<Button>(Resource.Id.OperacePotvrzeni_btn_Korekce);
                btn_Materialy = FindViewById<Button>(Resource.Id.OperacePotvrzeni_btn_Material);

                btn_OK.Click += Btn_OK_Click;
                btn_Storno.Click += Btn_Storno_Click;

                btn_Korekce.Click += Btn_Korekce_Click;
               

                btn_OK.FocusChange += Btn_FocusChange;
                btn_Storno.FocusChange += Btn_FocusChange;

                btn_Korekce.FocusChange += Btn_FocusChange;
                btn_Materialy.FocusChange += Btn_FocusChange;

                btn_OK.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btn_Storno.SetBackgroundResource(Resource.Drawable.lost_focus_button);

                btn_Korekce.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btn_Materialy.SetBackgroundResource(Resource.Drawable.lost_focus_button);

                btn_OK.RequestFocus();
                btn_OK.Selected = true;
                btn_Storno.Selected = false;
                btn_Korekce.Selected = false;
                btn_Materialy.Selected = false;


                if (Konfigurace_Singleton.Instance.Vyroba.Production_Material_Enter && Konfigurace_Singleton.Instance.Vyroba.Production_Material_OperacePotvrzeniButton)
                {
                    btn_Materialy.Click += Btn_Materialy_Click;
                }
                else
                {
                    btn_Materialy.Text = string.Empty;
                }


                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.OperacePotvrzeni_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.OperacePotvrzeni_drawer_layout);

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

                mNavigationView = FindViewById<NavigationView>(Resource.Id.OperacePotvrzeni_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);

                timerDateTimeOperaceUpdate = new Timer(timerDateTimeOperaceUpdate_Tick);
                timerDateTimeOperaceUpdate.Change(1000, 1000);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowErrorMessage(this, ex);
            }
        }



        #region Timer event

        private void timerDateTimeOperaceUpdate_Tick(object sender)
        {
            try
            {
                //if (this.IsDisposed)
                //    return;

                if (ProductionRow != null)
                {
                    ProductionRow.TIMESTOP = DateTime.Now;
                }

                RunOnUiThread(() => {
                    UpdateForm();
                });
                //this.BeginInvoke(delegateUpdateForm);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //Logging.Log.Write(ex.Message, "FormOperacePotvrzeni.timerDateTimeOperaceUpdate_Tick()");
            }
        }

        #endregion

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
            if (id == Resource.Id.nav_OperacePotvrzeni_OK)
            {
                PerformOK();
            }
            else if (id == Resource.Id.nav_OperacePotvrzeni_Storno)
            {
                StartAktivityNasledujici(typeof(OdvadeniVyroby_SberDat), null);
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
            StartAktivityNasledujici(typeof(OdvadeniVyroby_SberDat), null);
        }

        private async void Btn_Korekce_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    FormKorekceCasu frmkorekcecasu = new FormKorekceCasu();
            //    frmkorekcecasu.ProductionRow = ProductionRow;
            //    frmkorekcecasu.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    Fask.Logging.ExceptionHandler2.Handle(ex);
            //    await MessageBoxAsync.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK);
            //}

            //this.UpdateForm();



            #region Android kod

            string Nazev = "Zadejte zdrojový sklad";
            string Mnozstvi = "999999";
            string Sklad = "456";
            string Lokace = "R2D2-001";
            string Material = "NATURA Bylinková směs na svilušky 10x10g";

            string Kod_tmp = "12345";


            while (true)
            {

                var InputKod2_Sklad = await GetInputKod2Async(
                                                            Nazev,
                                                            Mnozstvi,
                                                            Sklad,
                                                            Lokace,
                                                            Material,
                                                            Kod_tmp
                                                            );

                if (InputKod2_Sklad.status)
                    return;

                Kod_tmp = InputKod2_Sklad.Kod;
                break;
            }

            #endregion


        }

        private async void Btn_Materialy_Click(object sender, EventArgs e)
        {
            try
            {

                //tatp = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter();
                //tatp.Connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));

                Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable vyrobky = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBy_itemnmbrDef_IDHNULL_FASK_Vyroba_TP(VPP.ITEMNMBR);


                //VPP ITEMNMBR a podle toho hledam materialy
                if (!PriznakNacteneMaterialy && vyrobky.Count > 0)
                {
                    LoadMaterialy3(vyrobky);
                }

                //Rozpad materialu
                using (FormMaterial frmMaterial = new FormMaterial())
                {
                    frmMaterial.pocetodvedeno = this.PocetOdvedeno;
                    frmMaterial.ProductionRow = this.ProductionRow;
                    frmMaterial.ProductionSDT = this.ProductionSDT;
                    frmMaterial.types = ShowTypes.Back;
                    frmMaterial.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                await MessageBoxAsync.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region Get From Intent


        private void GetUzivatelFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.User);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_User);
            Uzivatel = ((WrapperForBinder<Users>)binderTD).getData();
        }

        private void GetPocetOdvedenoFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.OV_OP_PocetOdvedeno);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_OP_PocetOdvedeno);
            PocetOdvedeno = ((WrapperForBinder<decimal>)binderTD).getData();
        }

        private void GetPracovnikFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.OV_OP_Pracovnik);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_OP_Pracovnik);
            Pracovnik = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow>)binderTD).getData();
        }

        private void GetMachineFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.OV_OP_Machine);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_OP_Machine);
            Machine = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow>)binderTD).getData();
        }

        private void GetVPPFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.OV_OP_VPP_ROW);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_OP_VPP_ROW);
            VPP = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow>)binderTD).getData();
        }

        private void GetProductionRowFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.OV_OP_ProductionRow);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_OP_ProductionRow);
            ProductionRow = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow>)binderTD).getData();
        }

        private void GetProductionSDTFromIntent(Intent data)
        {
            var bundleTD = data?.GetBundleExtra(DataInfo_Static.OV_OP_ProductionSDT);
            var binderTD = bundleTD?.GetBinder(DataInfo_Static.object_OV_OP_ProductionSDT);
            ProductionSDT = ((WrapperForBinder<Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable>)binderTD).getData();
        }

        #endregion

        private void StartAktivityNasledujici(Type typAktivity, string ID)
        {
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);
            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if(ID == null)
                this.SetResult(Result.Canceled, intent);
            else
                this.SetResult(Result.Ok, intent);

            this.Finish();
        }

        private async void PerformOK()
        {
            try
            {
            string text = string.Empty;
                StartAktivityNasledujici(typeof(OdvadeniVyroby_SberDat), text);
            }
            catch (Exception ex)
            {
                await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                return;
            }
        }

        public async void PerformOK2()
        {
            #region TaD dle Zadani od JaS dne 27.7.2020 je s toho vytvořek kočkopes... kde čas je zadavan max 1 den...

            TimeSpan celkovycas;
            try
            {
                celkovycas = TimeSpan.Parse(OperacePotvrzeni_textview_lblCelkovyCas.Text);
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                celkovycas = TimeSpan.Parse("23:59:59");
                DialogResult dr = await MessageBoxAsync.Show(this, "Celkový čas je moc velký. Bude použit maximalni povolený: " + celkovycas.ToStringHHmm() + "\n\nOpravdu chcete odvést výrobu?", "Dotaz", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return;
                }
            }

            if (celkovycas < TimeSpan.Zero)
            {
                DialogResult dr = await MessageBoxAsync.Show(this, "Celkový čas je menší než " + TimeSpan.Zero.ToStringHHmm() + "\n\nOpravdu chcete odvést výrobu?", "Dotaz", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) return;
            }
            #endregion

            //this.finalize();

            if (Konfigurace_Singleton.Instance.Vyroba.Production_Material_Enter && Konfigurace_Singleton.Instance.Vyroba.Production_Material_OperacePotvrzeniButton)
            {
                //tatp = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_Vyroba_TPTableAdapter();
                //tatp.Connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));

                Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable vyrobky = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBy_itemnmbrDef_IDHNULL_FASK_Vyroba_TP(VPP.ITEMNMBR);


                if (!PriznakNacteneMaterialy && vyrobky.Count > 0)
                {
                    if (await MessageBoxAsync.Show(this, "Nebyly zadány materiály. " + System.Environment.NewLine + "Zadat materialy?", "Dotaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        LoadMaterialy3(vyrobky);
                    else
                        return;
                }

                if (ProductionSDT.Count == 0 && vyrobky.Count > 0)
                {
                    if (await MessageBoxAsync.Show(this, "Materiály byly zadány ale seznam je prázdný. " + System.Environment.NewLine + "Zadat materialy?", "Dotaz", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        LoadMaterialy3(vyrobky);
                    else
                        return;
                }
            }

            //this.DialogResult = DialogResult.OK;
            string text = string.Empty;
            StartAktivityNasledujici(typeof(OdvadeniVyroby_SberDat), text);
        }


        private void UpdateForm()
        {
            try
            {
                if (lastoperationuserdt == null)
                {
                    //lastoperationuserdt = _lstoperuserta.GetLastOperationDateTime(_pracovnik.id);
                    lastoperationuserdt = DataInfo_Static.VyrobaGO_Instance.controller_InternalState.GetLastOperationDateTime(Pracovnik.Login);

                }
            }
            catch (Exception ex)
            {
                // zalogovat
                string exx = ex.Message.ToString();

            }
            // TODO : osetrit vyjimky ...
            try
            {
                try
                {
                    OperacePotvrzeni_textview_lblPracovnik.Text = Pracovnik.surname.Trim() + " " + Pracovnik.firstname.Trim();
                }
                catch { }
                try
                {
                    OperacePotvrzeni_textview_lblStroj.Text = Machine.description.Trim();
                }
                catch { }

                try 
                { 
                    OperacePotvrzeni_textview_lblPolozka.Text = VPP == null || VPP.IsITEMDESCNull() ? "?" : VPP.ITEMDESC.Trim(); 
                }
                catch { }

                try
                {
                    OperacePotvrzeni_textview_lblPripravnyCas.Text = TimeSpan.FromMinutes(ProductionRow.TIMEPREP).ToStringHHmm(); //new TimeSpan(0, _vpp.TIMEPREP, 0).ToString();
                }
                catch { }
                try
                {
                    OperacePotvrzeni_textview_lblJednotkovyCas.Text = TimeSpan.FromMinutes(ProductionRow.TIMEUNIT).ToStringHHmm(); //new TimeSpan(0, _vpp.TIMEUNIT, 0).ToString();
                }
                catch { }
                try
                {
                    OperacePotvrzeni_textview_lblKusu.Text = ProductionRow.qty.ToString("0.####");
                }
                catch { }
                try
                {
                    OperacePotvrzeni_textview_lblKorekceCasu.Text = TimeSpan.FromMinutes(ProductionRow.IsTIMECORNull() ? 0 : ProductionRow.TIMECOR).ToStringHHmmss(); //new TimeSpan(0,_productionRow.TIMECOR, 0).ToString();
                }
                catch { }

                try
                {
                    OperacePotvrzeni_textview_lblCelkovyCas.Text =
                        (ProductionRow.TIMESTOP
                        - (lastoperationuserdt ?? Config.Settings_DB.LastProductionDateTime)
                        - (ProductionRow.IsTIMECORNull() ? TimeSpan.FromMinutes(0) : TimeSpan.FromMinutes(ProductionRow.TIMECOR))).ToStringHHmmss();
                }
                catch { }

            }
            catch (Exception ex)
            {
               Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }


        #region Nacteni Materialu

        private async void LoadMaterialy3(Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPDataTable vyrobky)
        {

            if (vyrobky.Count() == 0)
            {
                // TODO : upravit hlaseni error ... 
                await MessageBoxAsync.Show(this, "Nenalezeno ... ", "Error", MessageBoxButtons.OK);
            }
            else if (vyrobky.Count() == 1)
            {
                PriznakNacteneMaterialy = !isMaterial(vyrobky.First(), null);
                //if (!isMaterial(vyrobky.First(), null))
                //    PriznakNacteneMaterialy = true;
                //else
                //    PriznakNacteneMaterialy = false;
            }
            else
            { // TODO : Vyber, ktery z vyrobku / variant vyrobku ... 
                PriznakNacteneMaterialy = !isMaterial(vyrobky.First(), null);
                //if (!isMaterial(vyrobky.First(), null))
                //    PriznakNacteneMaterialy = true;
                //else
                //    PriznakNacteneMaterialy = false;
            }
        }

        /// <summary>
        /// Rekurzivni volani a 
        /// </summary>
        /// <param name="rUp"></param>
        /// <returns></returns>
        private bool isMaterial(Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow rUp, materialParams materialparams)
        {

            // predavani parametru materialu z vyssi urovne ... 
            if (materialparams == null)
            {
                materialparams = new materialParams();
            }
            else
            {
                decimal koefUp = 1;
                try
                {

                    string tmpKoef = string.IsNullOrEmpty(rUp.koef) ? string.Empty : rUp.koef.Trim();

                    if (tmpKoef.Contains(","))
                        tmpKoef = tmpKoef.Replace(",", ".");

                    koefUp = decimal.Parse(tmpKoef, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                { }

                materialparams = new materialParams(materialparams, koefUp);
            }

            if ((rUp == null) || (rUp.IsID_LNull()))
                return false;

            var rDowns = MES_Android.Classes.DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataByIDH_FASK_Vyroba_TP(rUp.ID_L);


            if (rDowns.Count() == 0)
            {
                return true;
            }
            else
            {
                // alternace a jen nektere ... 
                // vyberu jestli jsou alternace na teto urovni .. 
                var rAlt = rDowns.GroupBy(x => x.IsalterNull() ? string.Empty : x.alter).OrderBy(x => x.Key);
                if (rAlt.Count() == 0)
                {
                    return false;
                }

                foreach (var alternace in rAlt)
                {
                    if (String.IsNullOrEmpty(alternace.Key))
                    {
                        foreach (var rDown in alternace)
                        {
                            bool isM = isMaterial(rDown, materialparams);

                            if (isM)
                            { // vlozim data
                                FillDatasetMaterialy(rDown, materialparams);
                            }
                        }
                    }
                    else
                    { // alternace
                        // dat na vyber z alternativnich
                        using (FormMaterialAlternaceVyber mat_alter = new FormMaterialAlternaceVyber())
                        {
                            mat_alter.Alternativy = alternace.ToArray();

                            if (mat_alter.ShowDialog() == DialogResult.Cancel)
                            {
                                continue;
                            }
                            var row_vybrana_alt = mat_alter.AlternativaSelected;

                            bool isM = isMaterial(row_vybrana_alt, materialparams);
                            if (isM)
                            {
                                FillDatasetMaterialy(row_vybrana_alt, materialparams);
                            }
                        }
                    }
                }

                //foreach (var rDown in rDowns)
                //{
                //    bool isM = isMaterial(rDown);

                //    if (isM)
                //    { // vlozim data
                //    }
                //}
            }


            return false;
        }

        #region Parametry materialu pro rekurzi ...
        /// <summary>
        /// Slouzi pro interni vypocty v ramci rekurzivniho pruchodu stromu TP
        /// </summary>
        private class materialParams
        {
            public materialParams()
            {
            }

            public materialParams(decimal koef)
            {
                this.KoeficientSet(koef);
            }

            public materialParams(materialParams mP, decimal koef)
            {
                if (mP == null)
                {
                    mP = new materialParams();
                }

                this.KoeficientSet(koef);
            }

            public void KoeficientSet(decimal koef)
            {
                this.KoeficientNadrazeny = koef;
                this.KoeficientKumulovany *= koef;
            }

            /// <summary>
            /// Koeficient nadrazeneho uzlu
            /// </summary>
            public decimal KoeficientNadrazeny = 1;
            /// <summary>
            /// Kumulovany koeficient od 1.uzlu az do aktualni urovne...
            /// </summary>
            public decimal KoeficientKumulovany = 1;
        }

        #endregion


        /// <summary>
        /// vyplneni datasetu materialu
        /// </summary>
        /// <param name="dt_cons_095"></param>
        /// 
        /// 
        /// 

        private async void FillDatasetMaterialy(Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow rTP, materialParams materialparams)
        {
            // TaD
            //Pridani materialu do tabulky production sources
            // na zaklade jednoh radku v tabulke FASK_Vyroba_TP
            //
            // 1. načteni z tabulky zbozi(fask_CONS_095) vsechny nalezene zaznamy odpovidajici ITEMNMBR pomoci FillByITEMNMBR
            // 2. kontrola zda byl nalezen jeden material nebo víc, mužou byt rozdilne čar kody, mnozstvi, seriove cislo ...
            // 3. pokud neni nalezen zadny metoda se ukonci
            // 4. pokud je nalezeno vyc jak jeden tak se zavla okno s vyberem a to smaže datatable a vrati pouze jeden radek
            // 5. pokud je jen jeden radek tak se jde rovnou plnit
            // (plneni jednoh radku z tabulky pomoci freach je historicka zalezitost kdy se plnila najednou cela tabulka)


            Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable dt_cons_095 = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable();

            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter ta_cons_095 = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.FASK_CONS_095TableAdapter();
            //ta_cons_095.Connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD));

            DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.FillByITEMNMBR_CZMST_095(true, dt_cons_095, rTP.ITEMNMBR_fol);

            if ((dt_cons_095 == null) || (dt_cons_095.Count == 0))
                return;


            else if (dt_cons_095.Count > 1)
            {
                using (FormMaterialAlternaceVyber mat_alter = new FormMaterialAlternaceVyber())
                {
                    //mat_alter.Alternativy = new Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow[] { rTP };

                    mat_alter.dt_Material = dt_cons_095;

                    if (mat_alter.ShowDialog() == DialogResult.Cancel)
                    {
                        // TODO : urcite???
                        return;
                    }
                    var row_vybrany_095 = mat_alter.MaterialSelected;
                    dt_cons_095.Clear();
                    dt_cons_095.ImportRow(row_vybrany_095);
                }
            }




            // predavany parametr je tabulka zbozi materialu pridavaneho 

            try
            {
                foreach (Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row i in dt_cons_095)
                {

                    Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesRow row = ProductionSDT.NewProduction_SourcesRow();

                    row.CountEntries = ProductionRow.CountEntries;
                    row.SOPNUMBE = ProductionRow.SOPNUMBE.Trim();
                    row.ITEMNAME = i.ITEMDESC.Trim();
                    row.ITEMNMBR = i.ITEMNMBR.Trim();
                    row.ITEMTYPE = string.Empty;

                    if (i.IsITEMCODENull())
                        row.SetITEMCODENull();
                    else
                        row.ITEMCODE = i.ITEMCODE.Trim();

                    row.MJ = i.MJ.Trim();

                    decimal mnozstvi = 1;
                    decimal koeficient = 1;
                    try
                    {
                        string tmpKoef = string.IsNullOrEmpty(rTP.koef) ? string.Empty : rTP.koef.Trim();

                        if (tmpKoef.Contains(","))
                            tmpKoef = tmpKoef.Replace(',', '.');

                        koeficient = decimal.Parse(tmpKoef, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch { }
                    //mnozstvi = (decimal.Parse(rTP.koef) * PocetOdvedeno);
                    mnozstvi = materialparams.KoeficientKumulovany * koeficient * PocetOdvedeno;

                    row.QTYSHPPD = mnozstvi * (i.QTYPACK == 0 ? 1 : i.QTYPACK);
                    row.QTYSHPPDMJ = mnozstvi;
                    row.QTYPACK = i.QTYPACK;


                    // TODO : seriove cisla
                    row.SERLTNUM = string.Empty;

                    //Zadavani skladu Material konfiguracne
                    if (Konfigurace_Singleton.Instance.Vyroba.Production_Material_Source_SKLID_Enter)
                    {
                        // kontrola zda je SKL_ID zadano a ci neni prazdne v FASK_CONS_095
                        if (!i.IsSKL_IDNull() && !String.IsNullOrEmpty(i.SKL_ID.Trim()))
                        { // zadani skladu z vybrane polozky ...
                            // TODO overeni???
                            row.SKL_ID = i.SKL_ID;
                        }
                        else
                        { //zadani skladu rucne

                            //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter taSklady = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST093TableAdapter();
                            //taSklady.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);


                            #region Android kod

                            string Nazev = "Zadejte zdrojový sklad";
                            string Mnozstvi = row.QTYSHPPD.ToString();
                            string Sklad = null;
                            string Lokace = null;
                            string Material = row.ITEMNAME;

                            string Kod_tmp = string.Empty;

                            if (!row.IsSKL_IDNull() && !string.IsNullOrEmpty(row.SKL_ID))
                            {
                                Fask.SQLiteDBs.DataSets.Vyroba.CZMST093DataTable listSklady = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(row.SKL_ID);

                                if (listSklady.Count > 0)
                                    Kod_tmp = listSklady[0].skl_carcode;
                            }
                            else
                                Kod_tmp = Konfigurace_Singleton.Instance.Vyroba.Production_Material_Source_SKLID;

                            while (true)
                            {

                                var InputKod2_Sklad = await GetInputKod2Async(
                                                                            Nazev,
                                                                            Mnozstvi,
                                                                            Sklad,
                                                                            Lokace,
                                                                            Material,
                                                                            Kod_tmp
                                                                            );

                                if (InputKod2_Sklad.status)
                                    return;

                                Kod_tmp = InputKod2_Sklad.Kod;
                                // Test na existenci id cil. skladu ...
                                var listSklady = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(Kod_tmp);
                                if (listSklady.Count == 0)
                                {
                                    if (DialogResult.Cancel == await MessageBoxAsync.Show(this, "Sklad '" + Kod_tmp + "' nenalezen!", "Cíloví sklad", MessageBoxButtons.RetryCancel))
                                        return;
                                }
                                else
                                {
                                    Kod_tmp = listSklady[0].skl_id.Trim();

                                    break;
                                }
                            }

                            row.SKL_ID = Kod_tmp;
        

                            #endregion

                            //Zadani ciloveho skladu a cilove lokace ...
                            #region Puvodny kod

                            //using (FormInputKod2 fik = new FormInputKod2())
                            //{
                            //    fik.Text = "Zadejte zdrojový sklad";
                            //    fik.Nazev = "Zadejte zdrojový sklad";
                            //    fik.Mnozstvi = row.QTYSHPPD.ToString();
                            //    fik.Sklad = null;
                            //    fik.Lokace = null;
                            //    fik.Material = row.ITEMNAME;


                            //    if (!row.IsSKL_IDNull() && !string.IsNullOrEmpty(row.SKL_ID))
                            //    {
                            //        Fask.SQLiteDBs.DataSets.Vyroba.CZMST093DataTable listSklady = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklid_CZMST093(row.SKL_ID);

                            //        if (listSklady.Count > 0)
                            //            fik.Kod = listSklady[0].skl_carcode;
                            //    }
                            //    else
                            //        fik.Kod = Konfigurace_Singleton.Instance.Vyroba.Production_Material_Source_SKLID;

                            //    while (true)
                            //    {
                            //        fik.Kod = fik.Kod;
                            //        if (DialogResult.Cancel == fik.ShowDialog())
                            //            return;

                            //        // Test na existenci id cil. skladu ...
                            //        var listSklady = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataByBarcode_CZMST093(fik.Kod);
                            //        if (listSklady.Count == 0)
                            //        {
                            //            if (DialogResult.Cancel == await MessageBoxAsync.Show(this, "Sklad '" + fik.Kod + "' nenalezen!", "Cíloví sklad", MessageBoxButtons.RetryCancel))
                            //                return;
                            //        }
                            //        else
                            //        {
                            //            fik.Kod = listSklady[0].skl_id.Trim();

                            //            break;
                            //        }
                            //    }
                            //    row.SKL_ID = fik.Kod;

                            //} 
                            #endregion
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Konfigurace_Singleton.Instance.Vyroba.Production_Material_Source_SKLID))
                            row.SKL_ID = Konfigurace_Singleton.Instance.Vyroba.Production_Material_Source_SKLID;
                    }

                    // TODO : dialog vyberu lokace dle skladu
                    if (Konfigurace_Singleton.Instance.Vyroba.Production_Material_Source_LOCNCODE_Enter)
                    {
                        //Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter taLokace = new Fask.SQLiteDBs.DataSets.VyrobaTableAdapters.CZMST094TableAdapter();
                        //taLokace.Connection.ConnectionString = "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCE + Constants.PRD);


                        #region Android kod

                        string Nazev = "Zadejte zdrojový lokaci";
                        string Mnozstvi = row.QTYSHPPD.ToString();
                        string Sklad = row.SKL_ID;
                        string Lokace = null;
                        string Material = row.ITEMNAME;

                        string Kod_tmp = string.Empty;

                        if (!row.IsSKL_IDNull()
                                 && !row.IsLOCNCODENull()
                                 && !string.IsNullOrEmpty(row.SKL_ID)
                                 && !string.IsNullOrEmpty(row.LOCNCODE)
                                 )
                        {
                            var listLokace = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklidLocncode_CZMST094(row.SKL_ID, row.LOCNCODE);
                            if (listLokace.Count > 0)
                                Kod_tmp = listLokace[0].Barcode;
                        }
                        else
                            Kod_tmp = Konfigurace_Singleton.Instance.Vyroba.Production_Material_Source_LOCNCODE;

                        while (true)
                        {

                            var InputKod2_Sklad = await GetInputKod2Async(
                                                                        Nazev,
                                                                        Mnozstvi,
                                                                        Sklad,
                                                                        Lokace,
                                                                        Material,
                                                                        Kod_tmp
                                                                        );

                            if (InputKod2_Sklad.status)
                                return;

                            Kod_tmp = InputKod2_Sklad.Kod;
                            // Test na existenci id cil. skladu ...
                            var listSklady = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklidBarcode_CZMST094(row.SKL_ID, Kod_tmp);
                            if (listSklady.Count == 0)
                            {
                                if (DialogResult.Cancel == await MessageBoxAsync.Show(this, "Lokace '" + Kod_tmp + "' nenalezena!", "Cílová lokace", MessageBoxButtons.RetryCancel))
                                    return;
                            }
                            else
                            {
                                Kod_tmp = listSklady[0].LOCNCODE.Trim();

                                break;
                            }
                        }

                        row.LOCNCODE = Kod_tmp;


                        #endregion

                        #region Puvodny kod

                        //zadani cilove lokace
                        //using (FormInputKod2 fik = new FormInputKod2())
                        //{
                        //    fik.Text = "Zadejte zdrojovu lokaci";
                        //    fik.Nazev = "Zadejte zdrojovou lokaci";
                        //    fik.Mnozstvi = row.QTYSHPPD.ToString();
                        //    fik.Sklad = row.SKL_ID;
                        //    fik.Lokace = null;
                        //    fik.Material = row.ITEMNAME;

                        //    if (!row.IsSKL_IDNull()
                        //        && !row.IsLOCNCODENull()
                        //        && !string.IsNullOrEmpty(row.SKL_ID)
                        //        && !string.IsNullOrEmpty(row.LOCNCODE)
                        //        )
                        //    {
                        //        var listLokace = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklidLocncode_CZMST094(row.SKL_ID, row.LOCNCODE);
                        //        if (listLokace.Count > 0)
                        //            fik.Kod = listLokace[0].Barcode;
                        //    }
                        //    else
                        //        fik.Kod = Konfigurace_Singleton.Instance.Vyroba.Production_Material_Source_LOCNCODE;

                        //    while (true)
                        //    {
                        //        fik.Kod = fik.Kod;
                        //        if (DialogResult.Cancel == fik.ShowDialog())
                        //            return;

                        //        // Test na existenci id cil. lokace ...
                        //        var listLokace = DataInfo_Static.VyrobaGO_Instance.controller_Vyroba__Vyroba_PRD.GetDataBySklidBarcode_CZMST094(row.SKL_ID, fik.Kod);
                        //        if (listLokace.Count == 0)
                        //        {
                        //            if (DialogResult.Cancel == await MessageBoxAsync.Show(this, "Lokace '" + fik.Kod + "' nenalezena!", "Cílová lokace", MessageBoxButtons.RetryCancel))
                        //                return;
                        //        }
                        //        else
                        //        {
                        //            fik.Kod = listLokace[0].LOCNCODE.Trim();

                        //            break;
                        //        }
                        //    }
                        //    row.LOCNCODE = fik.Kod;

                        //}

                        #endregion
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Konfigurace_Singleton.Instance.Vyroba.Production_Material_Source_LOCNCODE))
                            row.LOCNCODE = Konfigurace_Singleton.Instance.Vyroba.Production_Material_Source_LOCNCODE;
                    }

                    row.GUID = Guid.NewGuid();
                    row.GUID_Production = ProductionRow.GUID;
                    row.USER_ID = ProductionRow.UserID;
                    row.TERMINAL_ID = ProductionRow.TermID;
                    //row.WEIGHT = 
                    row.NMBRPAL = string.Empty;
                    row.TYPEPAL = string.Empty;
                    row.PRINTED = 0;

                    ProductionSDT.AddProduction_SourcesRow(row);
                }

                //_productionSDT.AcceptChanges();

            }
            catch (Exception ex)
            {
                await MessageBoxAsync.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK);
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }
        #endregion

        #region Pomocne metody pro dalsi okna

        private Task<O_GetInputKod2Async> GetInputKod2Async(
            string Nazev,
            string Mnozstvi,
            string Sklad,
            string Lokace,
            string Material,
            string Kod
            )
        {
            try
            {
                this.TCS_GetInputKod2Async = new TaskCompletionSource<O_GetInputKod2Async>();

                Action messageBoxDelegate = () =>
                {
                    StartAktivityNasledujici_ForResult(
                        typeof(InputKod2),
                        this.ResoultCode_InputKod2,
                        Nazev,
                        Mnozstvi,
                        Sklad,
                        Lokace,
                        Material,
                        Kod
                        );
                };

                this.RunOnUiThread(messageBoxDelegate);

            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                //return null;
                this.TCS_GetInputKod2Async.SetException(ex);
            }

            return this.TCS_GetInputKod2Async.Task;
        }

        private void StartAktivityNasledujici_ForResult(
            Type typAktivity,
            int ResoultCode = 0,
            string nazev = null,
            string mnozstvi = null,
            string sklad = null,
            string lokace = null,
            string material = null,
            string kod = null)
        {
            Intent intent = new Intent(this, typAktivity);

            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(this.Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);

            if (nazev != null)
                intent.PutExtra(DataInfo_Static.OV_IN_InputKod2_Nazev, nazev);

            if (mnozstvi != null)
                intent.PutExtra(DataInfo_Static.OV_IN_InputKod2_Mnozstvi, mnozstvi);

            if (sklad != null)
                intent.PutExtra(DataInfo_Static.OV_IN_InputKod2_Sklad, sklad);

            if (lokace != null)
                intent.PutExtra(DataInfo_Static.OV_IN_InputKod2_Lokace, lokace);

            if (material != null)
                intent.PutExtra(DataInfo_Static.OV_IN_InputKod2_Material, material);

            if (kod != null)
                intent.PutExtra(DataInfo_Static.OV_IN_InputKod2_Kod, kod);


            this.StartActivityForResult(intent, ResoultCode);

        }

        #endregion
    }
}
