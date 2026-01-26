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
    class Prodej_Zasoby_Item_Holder : Holder
    {

        public TextView tv_ITEMNMBR { get; private set; }
        public TextView tv_CZ_CarKod { get; private set; }
        public TextView tv_ITEMDESC { get; private set; }
        public TextView tv_ITEMCODE { get; private set; }
        public TextView tv_VNDITNUM { get; private set; }
        
        public Prodej_Zasoby_Item_Holder(View itemView, int ID_LinearLayout, Action<int> listenerClick, Action<int> listenerLongClick, Action<Object, View.FocusChangeEventArgs> listenerFocusChange) : base(itemView, ID_LinearLayout)
        {
            _itemView = itemView;

            tv_ITEMNMBR = (TextView)itemView.FindViewById(Resource.Id.Prodej_Zasoby_ITEMNMBR);
            tv_CZ_CarKod = (TextView)itemView.FindViewById(Resource.Id.Prodej_Zasoby_CZ_CarKod);
            tv_ITEMDESC = (TextView)itemView.FindViewById(Resource.Id.Prodej_Zasoby_ITEMDESC);
            tv_ITEMCODE = (TextView)itemView.FindViewById(Resource.Id.Prodej_Zasoby_ITEMCODE);
            tv_VNDITNUM = (TextView)itemView.FindViewById(Resource.Id.Prodej_Zasoby_VNDITNUM);
            

            itemView.Click += (sender, e) => listenerClick(base.AdapterPosition);
            itemView.LongClick += (sender, e) => listenerLongClick(base.AdapterPosition);
            itemView.FocusChange += (sender, e) => listenerFocusChange(sender, e);
        }

    }
}