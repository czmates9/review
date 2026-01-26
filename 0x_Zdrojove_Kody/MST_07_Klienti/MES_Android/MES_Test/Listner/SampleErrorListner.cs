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

using Com.Karumi.Dexter.Listener;
using Android.Util;

namespace MES_Android.Listner
{
    public class SampleErrorListner : Java.Lang.Object, IPermissionRequestErrorListener
    {
        public void OnError(DexterError error)
        {
            Log.Error("Dexter", error.ToString());
        }
    }
}