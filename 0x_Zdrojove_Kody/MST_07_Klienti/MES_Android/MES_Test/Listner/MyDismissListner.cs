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

using Com.Karumi.Dexter;

namespace MES_Android.Listner
{
    public class MyDismissListner : Java.Lang.Object, IDialogInterfaceOnDismissListener
    {
        IPermissionToken token;

        public MyDismissListner(IPermissionToken token)
        {
            this.token = token;

        }

        public void OnDismiss(IDialogInterface dialog)
        {
            token.CancelPermissionRequest();
        }
    }
}