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

namespace MES_Android.Classes
{
    public class ProdejItemWrapperForBinder : Binder
    {

        private Prodej.Prodej_Davka_Item mData;

        public ProdejItemWrapperForBinder(Prodej.Prodej_Davka_Item data)
        {
            mData = data;
        }

        public Prodej.Prodej_Davka_Item getData()
        {
            return mData;
        }
    }
}