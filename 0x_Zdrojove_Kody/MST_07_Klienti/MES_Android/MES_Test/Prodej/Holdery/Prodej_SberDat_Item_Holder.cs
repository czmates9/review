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

namespace MES_Android.Prodej
{
    public class Prodej_SberDat_Item_Holder : Holder
    {

        public TextView tv_ITEMDESC { get; private set; }
        public TextView tv_QTYSHPPD { get; private set; }
        public TextView tv_Datum{ get; private set; }

        public Prodej_SberDat_Item_Holder(View itemView, int ID_LinearLayout, Action<int> listenerClick, Action<int> listenerLongClick, Action<Object, View.FocusChangeEventArgs> listenerFocusChange) : base(itemView, ID_LinearLayout)
        {
            _itemView = itemView;

            tv_ITEMDESC = (TextView)itemView.FindViewById(Resource.Id.Prodej_SberDat_ITEMDESC);
            tv_QTYSHPPD = (TextView)itemView.FindViewById(Resource.Id.Prodej_SberDat_QTYSHPPD);
            tv_Datum = (TextView)itemView.FindViewById(Resource.Id.Prodej_SberDat_Datum);


            itemView.Click += (sender, e) => listenerClick(base.AdapterPosition); // pokud to bude zle tak inu pozici...
            itemView.LongClick += (sender, e) => listenerLongClick(base.AdapterPosition);
            itemView.FocusChange += (sender, e) => listenerFocusChange(sender, e);
            itemView.FocusChange += (sender, e) => listenerFocusChange(sender, e);
            itemView.FocusChange += (sender, e) => listenerFocusChange(sender, e);
        }

    }
}