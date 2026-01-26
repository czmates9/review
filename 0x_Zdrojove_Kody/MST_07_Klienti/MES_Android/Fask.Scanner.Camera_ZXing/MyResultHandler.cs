using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EDMTDev.ZXingXamarinAndroid;


namespace Fask.Scanner.Camera_ZXing
{
    internal class MyResultHandler : IResultHandler
    {
        private ScannerDialog mainActivity;

        public MyResultHandler(ScannerDialog mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void HandleResult(ZXing.Result rawResult)
        {
            //mainActivity.txtResult.Text = rawResult.Text;
            mainActivity.ShowKod(rawResult);
        }
    }
}