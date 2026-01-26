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

namespace MES_Android.Kontrola_Kodu
{

    public class Config_WrapperForBinder : Binder
    {

        private Fask.Parsing.Config mData;

        public Config_WrapperForBinder(Fask.Parsing.Config data)
        {
            mData = data;
        }

        public Fask.Parsing.Config getData()
        {
            return mData;
        }
    }
}