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
using Com.Karumi.Dexter.Listener;
using Com.Karumi.Dexter.Listener.Multi;
using Android.Support.V7.App;

namespace MES_Android.Listner
{
    public class SampleMultiplePermissionListner : Java.Lang.Object, IMultiplePermissionsListener
    {
        private Base_Aktivita act;

        public SampleMultiplePermissionListner(Base_Aktivita A)
        {
            this.act = A;
        }

        public void OnPermissionRationaleShouldBeShown(IList<PermissionRequest> p0, IPermissionToken token)
        {

            act.OnPermission_HlasicShow(token);

            //if (act is Prodej.Prodej_Davky)
            //{
            //    ((Prodej.Prodej_Davky)act).ShowRequestPermissionRationale(token);
            //}
            //if (act is Prodej.Prodej_TypDokladu)
            //{
            //    ((Prodej.Prodej_TypDokladu)act).ShowRequestPermissionRationale(token);
            //}
            //if (act is MainActivity)
            //{
            //    ((MainActivity)act).ShowRequestPermissionRationale(token);
            //}
            //if (act is Config.Config_Main)
            //{
            //    ((Config.Config_Main)act).ShowRequestPermissionRationale(token);
            //}
            //if (act is Activity_HlavneMenu)
            //{
            //    ((Activity_HlavneMenu)act).ShowRequestPermissionRationale(token);
            //}

        }

        public void OnPermissionsChecked(MultiplePermissionsReport p0)
        {

        }
    }
}