using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Symbol.XamarinEMDK;
using Symbol.XamarinEMDK.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android.Zebra_Scanner
{
    public class BarcodeScanner
    {

        public static Fask.Scanner.Zebra_EMDK.Zebra_EMDK mBarcodeScanner = null;
        
        
        public static Fask.Scanner.Zebra_EMDK.Zebra_EMDK getInstance(Context context)
        {
            if (mBarcodeScanner != null &&
                mBarcodeScanner.context != null  &&
                context != null &&
                context != mBarcodeScanner.context 
                )
            {
                mBarcodeScanner.StopScanner();
                mBarcodeScanner.KillScanner();
                mBarcodeScanner = null;
            }

            if (mBarcodeScanner == null)
            {
                mBarcodeScanner = new Fask.Scanner.Zebra_EMDK.Zebra_EMDK(context);
            }
            return mBarcodeScanner;
        }
    }
}