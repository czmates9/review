using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

using Android.Graphics;
using Java.Util;
using System.Collections.ObjectModel;
using Android.Support.V4.View;
using MES_Android.ItemTouch;

namespace MES_Android.OdvadeniVyroby
{
    class PrikazVyber_Item_Holder : Holder
    {

        public TextView tv_SOPNUMBE { get; private set; }
        public TextView tv_CountEntries { get; private set; }
        public TextView tv_SOPDESC { get; private set; }
        public TextView tv_DateProd { get; private set; }
        public TextView tv_BarcodeH { get; private set; }

        public PrikazVyber_Item_Holder(View itemView, int ID_LinearLayout, Action<int> listenerClick, Action<int> listenerLongClick, Action<Object, View.FocusChangeEventArgs> listenerFocusChange) : base(itemView, ID_LinearLayout)
        {
            _itemView = itemView;

            tv_SOPNUMBE = (TextView)itemView.FindViewById(Resource.Id.PrikazVyber_SOPNUMBE);
            tv_SOPDESC = (TextView)itemView.FindViewById(Resource.Id.PrikazVyber_SOPDESC);
            tv_CountEntries = (TextView)itemView.FindViewById(Resource.Id.PrikazVyber_CountEntries);
            tv_DateProd = (TextView)itemView.FindViewById(Resource.Id.PrikazVyber_DateProd);
            tv_BarcodeH = (TextView)itemView.FindViewById(Resource.Id.PrikazVyber_BarcodeH);


            itemView.Click += (sender, e) => listenerClick(base.AdapterPosition);
            itemView.LongClick += (sender, e) => listenerLongClick(base.AdapterPosition);
            itemView.FocusChange += (sender, e) => listenerFocusChange(sender, e);
        }

    }
}