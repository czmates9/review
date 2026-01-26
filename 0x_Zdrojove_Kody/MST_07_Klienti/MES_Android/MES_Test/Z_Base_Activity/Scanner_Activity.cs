using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Fask.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android
{
    public class Scanner_Activity : Base_Aktivita
    {
        #region Zebra

        //public Fask.Scanner.Zebra_EMDK.Zebra_EMDK scanner_Zebra = null;

        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);


        //    try
        //    {
        //        //if (Config.Settings.ONOFF_ZEBRA)
        //        //{
        //        //    scanner_Zebra = new Fask.Scanner.Zebra_EMDK.Zebra_EMDK(this);
        //        //    scanner_Zebra.ScannerEvent += Scanner_Zebra_ScannerEvent;
        //        //    scanner_Zebra.StatusEvent += Scanner_StatusEvent;
        //        //}
        //    }
        //    catch (Exception exZebra)
        //    {
        //        Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, exZebra);
        //    }
        //}

        #region životny cyklus aplikace

        protected override void OnResume()
        {
            if (Config.Settings.ONOFF_ZEBRA)
            {
                Zebra_Scanner.BarcodeScanner.getInstance(this);
                //Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StartScanner();
                Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent += Scanner_Zebra_ScannerEvent;
                Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent += Scanner_StatusEvent;
            }

            base.OnResume();
        }

        protected override void OnPause()
        {
            if (Config.Settings.ONOFF_ZEBRA)
            {
                Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent -= Scanner_Zebra_ScannerEvent;
                Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent -= Scanner_StatusEvent;
                Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StopScanner();
                Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.KillScanner();
                Zebra_Scanner.BarcodeScanner.mBarcodeScanner = null;
            }
            base.OnPause();
        }


        //protected override void OnStop()
        //{
        //    if (Config.Settings.ONOFF_ZEBRA)
        //    {
        //        Zebra_Scanner.BarcodeScanner.mBarcodeScanner.ScannerEvent -= Scanner_Zebra_ScannerEvent;
        //        Zebra_Scanner.BarcodeScanner.mBarcodeScanner.StatusEvent -= Scanner_StatusEvent;
        //        Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.StopScanner();
        //        Zebra_Scanner.BarcodeScanner.mBarcodeScanner?.KillScanner();
        //    }
        //    base.OnStop();
        //}

        //protected override void OnDestroy()
        //{
        //    scanner_Zebra?.KillScanner();
        //    base.OnDestroy();
        //}

        #endregion

        #region Zebra scanner

        private void Scanner_Zebra_ScannerEvent(object sender, ScannerEventArgs e)
        {
            ScannerData(e);
        }

        private void Scanner_StatusEvent(object sender, StatusEventArgs e)
        {
            //Zde zasila z eventu stav scanneru....
        }

        #endregion

        virtual protected void ScannerData(ScannerEventArgs e)
        {

        }

        #endregion

        #region Fotak

        public Fask.Scanner.Camera_ZXing.ScannerDialog scanner_Fotak = null;

        public void PerformFotak()
        {
            try
            {
                if (Config.Settings.ONOFF_ZXing)
                {
                    Android.Support.V4.App.FragmentTransaction trans = SupportFragmentManager.BeginTransaction();
                    scanner_Fotak = new Fask.Scanner.Camera_ZXing.ScannerDialog();

                    scanner_Fotak.ScannerEvent += Scanner_Fotak_ScannerEvent;
                    scanner_Fotak.Show(trans, "Dialog Fragment");
                }
            }
            catch (System.Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void Scanner_Fotak_ScannerEvent(object sender, ScannerEventArgs e)
        {
            try
            {
                scanner_Fotak?.StopScanner();
                scanner_Fotak?.KillScanner();
                scanner_Fotak?.Dismiss();
                scanner_Fotak = null;

                ScannerData(e);

            }
            catch (Exception ex)
            {
                Fask.Logging.ExceptionHandler2.Handle(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        #endregion


    }
}