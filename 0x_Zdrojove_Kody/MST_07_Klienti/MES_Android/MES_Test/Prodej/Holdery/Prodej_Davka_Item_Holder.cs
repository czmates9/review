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
    class Prodej_Davka_Item_Holder : Holder
    {

        public TextView tv_CisloDavky { get; private set; }
        public TextView tv_Pocet { get; private set; }

        public Prodej_Davka_Item_Holder(View itemView, int ID_LinearLayout, Action<int> listenerClick, Action<int> listenerLongClick, Action<Object, View.FocusChangeEventArgs> listenerFocusChange) : base(itemView, ID_LinearLayout)
        {
            _itemView = itemView;
            tv_CisloDavky = (TextView)itemView.FindViewById(Resource.Id.Prodej_Davka_Cis);
            tv_Pocet = (TextView)itemView.FindViewById(Resource.Id.Prodej_Davka_Item_PocetPol);
            itemView.Click += (sender, e) => listenerClick(base.AdapterPosition); // pokud to bude zle tak inu pozici...
            itemView.LongClick += (sender, e) => listenerLongClick(base.AdapterPosition);
            itemView.FocusChange += (sender, e) => listenerFocusChange(sender, e);
        }

    }
}