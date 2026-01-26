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
    class Prodej_TypDokladu_Item_Holder : Holder
    {

        public TextView tv_DocDesc { get; private set; }
        public TextView tv_DocCarcode { get; private set; }
        public TextView tv_DocID { get; private set; }
        public TextView tv_DocID2 { get; private set; }
        public TextView tv_DocTyp { get; private set; }
        
        public Prodej_TypDokladu_Item_Holder(View itemView,int ID_LinearLayout,  Action<int> listenerClick, Action<int> listenerLongClick, Action<Object, View.FocusChangeEventArgs> listenerFocusChange) : base(itemView, ID_LinearLayout)
        {
            _itemView = itemView;
            
            tv_DocDesc = (TextView)itemView.FindViewById(Resource.Id.Prodej_TypDokladu_Doc_Desc);
            tv_DocCarcode = (TextView)itemView.FindViewById(Resource.Id.Prodej_TypDokladu_Item_Doc_Carcode);
            tv_DocID = (TextView)itemView.FindViewById(Resource.Id.Prodej_TypDokladu_Item_Doc_ID);
            tv_DocID2 = (TextView)itemView.FindViewById(Resource.Id.Prodej_TypDokladu_Item_Doc_ID2);
            tv_DocTyp = (TextView)itemView.FindViewById(Resource.Id.Prodej_TypDokladu_Item_Doc_Typ);
            

            itemView.Click += (sender, e) => listenerClick(base.AdapterPosition); // pokud to bude zle tak inu pozici...
            itemView.LongClick += (sender, e) => listenerLongClick(base.AdapterPosition);
            itemView.FocusChange += (sender, e) => listenerFocusChange(sender, e);

        }

    }
}