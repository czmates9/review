using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;


using Android.Support.V4.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using System.Collections.Generic;
using System;
using System.IO;
using Android.Content;
using Android.Views;
using MES_Android.Classes;
using Android.Content.Res;

using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android.Support.Design.Widget;

using Com.Karumi.Dexter;
using Android;
using Com.Karumi.Dexter.Listener.Multi;
using Com.Karumi.Dexter.Listener;
using Com.Karumi.Dexter.Listener.Single;
using MES_Android.Listner;
using Fask.Interfaces;
using Android.Support.V4.View;
using MES_Android.ServerAccess;
using System.Threading;
using System.Threading.Tasks;
using MES_Android.Ciselniky;
using System.Globalization;

namespace MES_Android.Kontrola_Kodu
{
    [Activity(Label = "ParsovaniKodu_Konfigurace")]
    public class ParsovaniKodu_Konfigurace : Base_Aktivita
    {

        public CheckedTextView CHTV_WeightCode;
        public CheckedTextView CHTV_WeightCode_12;

        public CheckedTextView CHTV_BarcodeSlashSarze;

        public CheckedTextView CHTV_FenixBarcodeObal;
        public CheckedTextView CHTV_Fenix_HIBC;

        public CheckedTextView CHTV_GS1_FASK;

        public CheckedTextView CHTV_SAB_AustralianNorm;
        public CheckedTextView CHTV_SAB_Balton;
        public CheckedTextView CHTV_SAB_GS1_Zavorky;
        public CheckedTextView CHTV_SAB_NeznamyKod;
        

        public Button btnSave;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            try
            {
                SetContentView(Resource.Layout.ParKod_Konf);

                this.DataWedge_Scanner_Disable();

                #region Presmission

                Dexter.WithActivity(this)
                    .WithPermissions(Manifest.Permission.ReadExternalStorage,
                                    Manifest.Permission.WriteExternalStorage,
                                    Manifest.Permission.AccessNetworkState,
                                    Manifest.Permission.Camera)
                    .WithListener(new CompositeMultiplePermissionsListener(new SampleMultiplePermissionListner(this)))
                    .WithErrorListener(new SampleErrorListner())
                    .Check();

                #endregion

                
                CHTV_WeightCode = FindViewById<CheckedTextView>(Resource.Id.CHTV_WeightCode);
                CHTV_WeightCode.Click += CHTV_WeightCode_Click;

                CHTV_WeightCode_12 = FindViewById<CheckedTextView>(Resource.Id.CHTV_WeightCode_12);
                CHTV_WeightCode_12.Click += CHTV_WeightCode_12_Click;

                CHTV_BarcodeSlashSarze = FindViewById<CheckedTextView>(Resource.Id.CHTV_BarcodeSlashSarze);
                CHTV_BarcodeSlashSarze.Click += CHTV_BarcodeSlashSarze_Click;


                CHTV_FenixBarcodeObal = FindViewById<CheckedTextView>(Resource.Id.CHTV_FenixBarcodeObal);
                CHTV_FenixBarcodeObal.Click += CHTV_FenixBarcodeObal_Click;
                CHTV_Fenix_HIBC = FindViewById<CheckedTextView>(Resource.Id.CHTV_Fenix_HIBC);
                CHTV_Fenix_HIBC.Click += CHTV_Fenix_HIBC_Click;

                CHTV_GS1_FASK = FindViewById<CheckedTextView>(Resource.Id.CHTV_GS1_FASK);
                CHTV_GS1_FASK.Click += CHTV_GS1_FASK_Click;

                CHTV_SAB_AustralianNorm = FindViewById<CheckedTextView>(Resource.Id.CHTV_SAB_AustralianNorm);
                CHTV_SAB_AustralianNorm.Click += CHTV_SAB_AustralianNorm_Click;
                CHTV_SAB_Balton = FindViewById<CheckedTextView>(Resource.Id.CHTV_SAB_Balton);
                CHTV_SAB_Balton.Click += CHTV_SAB_Balton_Click;
                CHTV_SAB_GS1_Zavorky = FindViewById<CheckedTextView>(Resource.Id.CHTV_SAB_GS1_Zavorky);
                CHTV_SAB_GS1_Zavorky.Click += CHTV_SAB_GS1_Zavorky_Click;
                CHTV_SAB_NeznamyKod = FindViewById<CheckedTextView>(Resource.Id.CHTV_SABNeznamyKod);
                CHTV_SAB_NeznamyKod.Click += CHTV_SABNeznamyKod_Click;       
                
                btnSave = FindViewById<Button>(Resource.Id.ParKod_konf_btnSaveConfig);

                btnSave.Click += BtnSave_Click;
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(ex);
            }
        }

        private void CHTV_SAB_Balton_Click(object sender, EventArgs e)
        {
            CHTV_SAB_Balton.Checked = !CHTV_SAB_Balton.Checked;
        }

        private void CHTV_Fenix_HIBC_Click(object sender, EventArgs e)
        {
            CHTV_Fenix_HIBC.Checked = !CHTV_Fenix_HIBC.Checked;
        }

        private void CHTV_SAB_GS1_Zavorky_Click(object sender, EventArgs e)
        {
            CHTV_SAB_GS1_Zavorky.Checked = !CHTV_SAB_GS1_Zavorky.Checked;
        }

        private void CHTV_SAB_AustralianNorm_Click(object sender, EventArgs e)
        {
            CHTV_SAB_AustralianNorm.Checked = !CHTV_SAB_AustralianNorm.Checked;
        }

        private void CHTV_GS1_FASK_Click(object sender, EventArgs e)
        {
            CHTV_GS1_FASK.Checked = !CHTV_GS1_FASK.Checked;
        }

        private void CHTV_FenixBarcodeObal_Click(object sender, EventArgs e)
        {
            CHTV_FenixBarcodeObal.Checked = !CHTV_FenixBarcodeObal.Checked;
        }

        private void CHTV_BarcodeSlashSarze_Click(object sender, EventArgs e)
        {
            CHTV_BarcodeSlashSarze.Checked = !CHTV_BarcodeSlashSarze.Checked;
        }

        private void CHTV_SABNeznamyKod_Click(object sender, EventArgs e)
        {
            CHTV_SAB_NeznamyKod.Checked = !CHTV_SAB_NeznamyKod.Checked;
        }

        private void CHTV_WeightCode_12_Click(object sender, EventArgs e)
        {
            CHTV_WeightCode_12.Checked = !CHTV_WeightCode_12.Checked;
        }

        private void CHTV_WeightCode_Click(object sender, EventArgs e)
        {
            CHTV_WeightCode.Checked = !CHTV_WeightCode.Checked;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //SaveKonfig();


            Fask.Parsing.Config config = new Fask.Parsing.Config(
                CHTV_WeightCode_12.Checked,
                CHTV_WeightCode.Checked,
                CHTV_SAB_NeznamyKod.Checked,
                CHTV_BarcodeSlashSarze.Checked,
                CHTV_FenixBarcodeObal.Checked,
                CHTV_Fenix_HIBC.Checked,
                CHTV_GS1_FASK.Checked,
                true,
                CHTV_SAB_AustralianNorm.Checked,
                CHTV_SAB_GS1_Zavorky.Checked,
                CHTV_SAB_Balton.Checked
                );


        Intent intent = new Intent(this, typeof(Kontrola_Kodu.ParsovaniKodu_UkazatKod));

            Bundle bundleRow = new Bundle();
            bundleRow.PutBinder(DataInfo_Static.object_ParsingConfig, new WrapperForBinder<Fask.Parsing.Config>(config));
            intent.PutExtra(DataInfo_Static.ParsingConfig, bundleRow);

            this.StartActivity(intent);
            this.Finish();
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Escape)
            {
                Intent intent = new Intent(this, typeof(Activity_HlavneMenu));
                this.StartActivity(intent);
                this.Finish();
            }

            return base.OnKeyDown(keyCode, e);
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(Activity_HlavneMenu));
            this.StartActivity(intent);
            this.Finish();
        }
    }
}