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
    class Prodej_Lokace_Item_Holder : Holder
    {

        public TextView tv_LOCNCODE { get; private set; }
        public TextView tv_desc { get; private set; }
        public TextView tv_SKL_ID { get; private set; }
        public TextView tv_BarCode { get; private set; }
        public TextView tv_Type { get; private set; }
        
        public Prodej_Lokace_Item_Holder(View itemView, int ID_LinearLayout, Action<int> listenerClick, Action<int> listenerLongClick, Action<Object, View.FocusChangeEventArgs> listenerFocusChange) : base(itemView, ID_LinearLayout)
        {
            _itemView = itemView;

            tv_LOCNCODE = (TextView)itemView.FindViewById(Resource.Id.Prodej_Lokace_LOCNCODE);
            tv_desc = (TextView)itemView.FindViewById(Resource.Id.Prodej_Lokace_desc);
            tv_SKL_ID = (TextView)itemView.FindViewById(Resource.Id.Prodej_Lokace_SKL_ID);
            tv_BarCode = (TextView)itemView.FindViewById(Resource.Id.Prodej_Lokace_BarCode);
            tv_Type = (TextView)itemView.FindViewById(Resource.Id.Prodej_Lokace_Type);
            

            itemView.Click += (sender, e) => listenerClick(base.AdapterPosition);
            itemView.LongClick += (sender, e) => listenerLongClick(base.AdapterPosition);
            itemView.FocusChange += (sender, e) => listenerFocusChange(sender, e);
        }

    }
}