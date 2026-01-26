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
    class Prodej_VyberMaterial_Item_Holder : Holder
    {

        public TextView tv_QTY { get; private set; }
        public TextView tv_Popis { get; private set; }
        public TextView tv_PolozkaC { get; private set; }
        public TextView tv_Lokace { get; private set; }
        public TextView tv_SN { get; private set; }
        
        public Prodej_VyberMaterial_Item_Holder(View itemView, int ID_LinearLayout, Action<int> listenerClick, Action<int> listenerLongClick, Action<Object, View.FocusChangeEventArgs> listenerFocusChange) : base(itemView, ID_LinearLayout)
        {
            _itemView = itemView;

            tv_QTY = (TextView)itemView.FindViewById(Resource.Id.Prodej_VyberMaterial_QTY);
            tv_Popis = (TextView)itemView.FindViewById(Resource.Id.Prodej_VyberMaterial_Popis);
            tv_PolozkaC = (TextView)itemView.FindViewById(Resource.Id.Prodej_VyberMaterial_PolozkaC);
            tv_Lokace = (TextView)itemView.FindViewById(Resource.Id.Prodej_VyberMaterial_Lokace);
            tv_SN = (TextView)itemView.FindViewById(Resource.Id.Prodej_VyberMaterial_SN);
            

            itemView.Click += (sender, e) => listenerClick(base.AdapterPosition);
            itemView.LongClick += (sender, e) => listenerLongClick(base.AdapterPosition);
            itemView.FocusChange += (sender, e) => listenerFocusChange(sender, e);
        }

    }
}