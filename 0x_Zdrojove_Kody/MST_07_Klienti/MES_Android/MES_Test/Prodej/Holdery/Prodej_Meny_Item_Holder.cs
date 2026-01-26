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
    class Prodej_Meny_Item_Holder : Holder
    {

        public TextView tv_ID { get; private set; }
        public TextView tv_Popis { get; private set; }
        public TextView tv_Vychozi { get; private set; }
        public TextView tv_Kurz { get; private set; }
        public TextView tv_DatumKurzu { get; private set; }
        
        public Prodej_Meny_Item_Holder(View itemView, int ID_LinearLayout, Action<int> listenerClick, Action<int> listenerLongClick, Action<Object, View.FocusChangeEventArgs> listenerFocusChange) : base(itemView, ID_LinearLayout)
        {
            _itemView = itemView;

            tv_ID = (TextView)itemView.FindViewById(Resource.Id.Prodej_Meny_ID);
            tv_Popis = (TextView)itemView.FindViewById(Resource.Id.Prodej_Meny_Popis);
            tv_Vychozi = (TextView)itemView.FindViewById(Resource.Id.Prodej_Meny_Vychozi);
            tv_Kurz = (TextView)itemView.FindViewById(Resource.Id.Prodej_Meny_kurz);
            tv_DatumKurzu = (TextView)itemView.FindViewById(Resource.Id.Prodej_Meny_DatumKurzu);
            

            itemView.Click += (sender, e) => listenerClick(base.AdapterPosition);
            itemView.LongClick += (sender, e) => listenerLongClick(base.AdapterPosition);
            itemView.FocusChange += (sender, e) => listenerFocusChange(sender, e);
        }

    }
}