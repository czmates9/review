using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MES_Android.Classes
{
    public class WrapperForBinder<T> : Binder
    {
        private T mData;

        public WrapperForBinder(T data)
        {
            mData = data;
        }

        public T getData()
        {
            return mData;
        }
    }
}