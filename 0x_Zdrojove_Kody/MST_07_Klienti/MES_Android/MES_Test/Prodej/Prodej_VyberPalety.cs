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
using MES_Android.Extensions;
using MES_Android.Classes.TypyPalet_Dialog_ID;

namespace MES_Android.Prodej
{
    [Activity(Label = "@string/Prodej_VyberPalety")]
    public class Prodej_VyberPalety : Base_Aktivita, NavigationView.IOnNavigationItemSelectedListener
    {

        #region Parametry

        Users Uzivatel = null;
        Prodej.Prodej_Davka_Item _Item = null;

        Schema.TypyPalet typyPalet;

        private Android.Support.V7.Widget.Toolbar mToolbar;
        private FASK_ActionBarDrawerToggle mDrawerToggle;
        private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;
        private NavigationView mNavigationView;

        Button btn_ok;
        Button btn_strono;
        Button btn_VyberTyp;

        EditText edit_typPalety;
        EditText edit_SSCCPalety;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                SetContentView(Resource.Layout.Prodej_VyberPalety);

                Dexter.WithActivity(this)
                    .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                    Manifest.Permission.WriteExternalStorage,
                                    Manifest.Permission.AccessNetworkState,
                                    Manifest.Permission.Camera)
                    .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                    .WithErrorListener(new SampleErrorListner())
                    .Check();

               

                 btn_ok = FindViewById<Button>(Resource.Id.Prodej_VyberPalety_btn_OK);
                 btn_strono = FindViewById<Button>(Resource.Id.Prodej_VyberPalety_btn_Storno);
                 btn_VyberTyp = FindViewById<Button>(Resource.Id.Prodej_VyberPalety_btn_VyberTyp);

                 edit_typPalety = FindViewById<EditText>(Resource.Id.Prodej_VyberPalety_edittxt_1);
                 edit_SSCCPalety = FindViewById<EditText>(Resource.Id.Prodej_VyberPalety_edittxt_2);

                edit_typPalety.FocusChange += Edittext_FocusChange;
                edit_SSCCPalety.FocusChange += Edittext_FocusChange;

                edit_typPalety.SetBackgroundResource(Resource.Drawable.lost_focus_style);
                edit_SSCCPalety.SetBackgroundResource(Resource.Drawable.lost_focus_style);

                btn_ok.Click += btn_ok_Click;
                btn_strono.Click += btn_strono_Click;
                btn_VyberTyp.Click += btn_VyberTyp_Click;

                btn_ok.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btn_strono.SetBackgroundResource(Resource.Drawable.lost_focus_button);
                btn_VyberTyp.SetBackgroundResource(Resource.Drawable.lost_focus_button);

                btn_ok.FocusChange += Btn_FocusChange;
                btn_strono.FocusChange += Btn_FocusChange;
                btn_VyberTyp.FocusChange += Btn_FocusChange;

                btn_VyberTyp.RequestFocus();
                btn_strono.Selected = false;
                btn_ok.Selected = false;


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

                mToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Prodej_VyberPalety_toolbar);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Prodej_VyberPalety_drawer_layout);
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


                mNavigationView = FindViewById<NavigationView>(Resource.Id.Prodej_VyberPalety_nav_view);
                mNavigationView.SetNavigationItemSelectedListener(this);

                InicializaceTypuPalet();

                Scanner_START();
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
                ShowErrorMessage(this, ex);
            }
        }

        private void InicializaceTypuPalet()
        {
            if (typyPalet == null)
                typyPalet = new Schema.TypyPalet();

            if (!File.Exists(DataInfo_Static.ConfigTypyPaletXML))
            {
                typyPalet.Palety.AddPaletyRow("C",  "čtvrtpaleta");
                typyPalet.Palety.AddPaletyRow("CD", "čtvrtpaleta s displayem");
                typyPalet.Palety.AddPaletyRow("E1", "EUROPALETY");
                typyPalet.Palety.AddPaletyRow("N",  "nevratná paleta(+atyp)");
                typyPalet.Palety.AddPaletyRow("P",  "půlpaleta");
                typyPalet.Palety.AddPaletyRow("PD", "půlpaleta s displayem");

                typyPalet.Palety.AcceptChanges();

                typyPalet.WriteXml(DataInfo_Static.ConfigTypyPaletXML);
            }

            typyPalet.Clear();
            typyPalet.ReadXml(DataInfo_Static.ConfigTypyPaletXML);


            var tmp = typyPalet.Palety[0];
            edit_typPalety.Tag = tmp.Object_2_JObject();
            edit_typPalety.Text = tmp.ToString();
        }

        #region Button evets

        private void btn_VyberTyp_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            Perform_TypyPaletList();
        }

        private void btn_strono_Click(object sender, EventArgs e)
        {
            StartAktivityNasledujici(typeof(Prodej_Davky), null);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            var state = PerformOK();

            if(state)
                StartAktivityNasledujici(typeof(Prodej_Davky), _Item);
        }

        #endregion

        #region Perform Metody

        private bool PerformOK()
        {
            try
            {
                if (edit_SSCCPalety.Text.Length == 0)
                {
                    edit_SSCCPalety.Text = DataInfo_Static.VyrobaGO_Instance.vydejServis.Terminal_GetSSCCCode(Konfigurace_Singleton.Instance.Prodej.SSCC_Sequence, 1, Config.Settings.TerminalID).ToString();
                    //edit_SSCCPalety.Text = DataInfo_Static.VyrobaGO_Instance.vydejServis.Terminal_GetSSCCCode(123, 1, Config.Settings.TerminalID).ToString();
                    SetTypPalety();

                    return true;
                }

                //Verifikace  SSCC
                if (SSCC_Verifikace(edit_SSCCPalety.Text))
                {

                    Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable dt = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable();
                    DataInfo_Static.ProdejGO_Instance.controller_prodej.FillBy_TOPjeden_NMBRPAL_DI(dt, edit_SSCCPalety.Text);

                    if (dt.Count != 0)
                    {
                        var ID = dt[0].TYPEPAL;

                        if(!string.IsNullOrEmpty(ID))
                        {
                            var ArrPalet = typyPalet.Palety.Where(x => x.ID.Trim() == ID.Trim());
                            if(ArrPalet.Count() > 0)
                            {
                                _Item.NMBRPAL = new Paleta(
                                    ArrPalet.First().ID,
                                    ArrPalet.First().Name,
                                    edit_SSCCPalety.Text
                                    );
                            }
                            else
                                SetTypPalety();
                           
                        }
                        else
                            SetTypPalety();

                        return true;
                    }
                    else
                    {
                        ShowErrorMessage(this, new Exception("Nenalezena odpovidajici paleta"));
                        return false;
                    }
                }
                else
                {
                    ShowErrorMessage(this, new Exception("SSCC neni podle normy"));
                    return false;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(this, ex);
                return false;
            }

        }

        private void SetTypPalety()
        {
            HelpObject o = (HelpObject)edit_typPalety.Tag;
            var Row_obj = (Schema.TypyPalet.PaletyRow)o.JObject_2_Object();

            _Item.NMBRPAL = new Paleta(
                Row_obj.ID,
                Row_obj.Name,
                edit_SSCCPalety.Text
                );
        }

        public bool SSCC_Verifikace(string s)
        {
            //SSCC sscc = new SSCC();
            if (s.Length != 20)
                throw new Exception("Delka SSCC kodu nesouhlasi");

            string AI = s.Substring(0, 2);
            if (AI != "00")
                throw new Exception("AI kod neni platny");

            string CheckDigit = CountParity(s.Substring(0, 19));

            if (CheckDigit != s.Substring(s.Length - 1, 1))
                throw new Exception("Kontrolni soucet se neshoduje");

            return true;
        }

        /// <summary>
        /// Vypocet kontrolniho cisla : Modulo 10
        /// </summary>
        /// <param name="newSSCC"></param>
        /// <returns></returns>
        /// <remarks>Mod 10 Check Digit
        ///The calculations for determining the Mod 10 Check Digit character are as follows:
        ///1. Start at the first position and add the value of every other position together.
        ///0 + 2 + 4 + 6 + 8 + 0 = 20
        ///2. The result of Step 1 is multiplied by 3.
        ///20 x 3 = 60
        ///3. Start at the second position and add the value of every other position together.
        ///1 + 3 + 5 + 7 + 9 = 25
        ///4. The results of steps 1 and 3 are added together.
        ///60 + 25 = 85
        ///5. The check character (12th character) is the smallest number which, when added to the
        ///result in step 4, produces a multiple of 10.
        ///85 + X = 90 (next higher multiple of 10)
        ///X = 5 Check Character
        ///</remarks>
        private static string CountParity(string newSSCC)
        {
            int sumLiche = 0;
            int sumSude = 0;

            // index : hodnota
            // 0,1 : "0"
            // 2-19 : cisla
            // 20 : kontrolni cislo
            for (int i = 2; i < newSSCC.Length; i++)
            {
                if ((i + 1) % 2 == 0) //Sude poradove cislo
                    sumSude += int.Parse(newSSCC[i].ToString());
                else //je liche poradove cislo
                    sumLiche += int.Parse(newSSCC[i].ToString());
            }

            return ((10 - (sumLiche * 3 + sumSude) % 10) % 10).ToString();

        }

        #endregion


        private void StartAktivityNasledujici(Type typAktivity, Prodej_Davka_Item _Item)
        {
            Scanner_STOP();
            //Button_Click(this.Resources.GetString(Resource.String.Prodej));
            Intent intent = new Intent(this, typAktivity);
            Bundle bundleUser = new Bundle();
            bundleUser.PutBinder(DataInfo_Static.object_User, new WrapperForBinder<Users>(Uzivatel));
            intent.PutExtra(DataInfo_Static.User, bundleUser);


            if (_Item != null)
            {
                Bundle bundle = new Bundle();
                bundle.PutBinder(DataInfo_Static.object_ProdejItem, new WrapperForBinder<Prodej.Prodej_Davka_Item>(_Item));
                intent.PutExtra(DataInfo_Static.ProdejItem, bundle);
                this.SetResult(Result.Ok, intent);
                this.Finish();
            }
            else
            {
                this.SetResult(Result.Canceled, intent);
                this.Finish();
            }

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

            if (id == Resource.Id.nav_HlMenu_ProVyberPalety)
            {
               
                StartAktivityNasledujici(typeof(Prodej_Davky), null);
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
                //StartAktivityNasledujici(typeof(Prodej_Davky), null);
            }

        }

        #endregion

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

        private void Edittext_FocusChange(object sender, View.FocusChangeEventArgs e)
        {

            if (e.HasFocus)
            {
                ((EditText)sender).SetBackgroundResource(Resource.Drawable.focus_border_style);
            }
            else
            {
                ((EditText)sender).SetBackgroundResource(Resource.Drawable.lost_focus_style);
            }
        }

        #region Zebra scanner

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

        private async void UpdateUI(string carkod, byte _input_mode)
        {
            try
            {
                Scanner_STOP();
                //ScannerStop();

                string ck = carkod.Trim();

                RunOnUiThread(()=> {
                    edit_SSCCPalety.Text = ck;
                });

            }
            catch (Exception ex)
            {
                await MessageBoxAsync.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
            finally
            {
                Scanner_START();
            }
        }


        #endregion

        #region MyRegion

        private void Perform_TypyPaletList()
        {
            try
            {
                if (!File.Exists(DataInfo_Static.ConfigTypyPaletXML))
                {
                    ShowMessage(this, "Chybí soubor Typu Palet", "Error");
                    return;
                }

                Android.Support.V4.App.FragmentTransaction transaction = SupportFragmentManager.BeginTransaction();
                MES_Android.Classes.TypyPalet_Dialog_ID.Dialog_TypyPalet dialog = new MES_Android.Classes.TypyPalet_Dialog_ID.Dialog_TypyPalet();
                dialog.dt = typyPalet.Palety;
                dialog.Show(transaction, "Dialog Fragment");
                dialog.mOnTypyPaletComplete += Dialog_mOnTypyPaletComplete;

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                ShowMessage(this, "Chybí soubor uživatelů", "Error");
            }
        }

        private void Dialog_mOnTypyPaletComplete(object sender, OnTypyPaletEventArgs e)
        {
            string ID = e.TypyPalet_ID;
            var ArrPalet = typyPalet.Palety.Where(x => x.ID.Trim() == ID.Trim());
            if (ArrPalet.Count() > 0)
            {
                var tmp = ArrPalet.First();

                RunOnUiThread(()=> {
                    edit_typPalety.Tag = tmp.Object_2_JObject();
                    edit_typPalety.Text = tmp.ToString();
                });

                
            }


        }

        #endregion
    }
}