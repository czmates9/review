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
    class OperaceVyber_Item_Holder : Holder
    {

        public TextView tv_ITEMDESC { get; private set; }
        public TextView tv_CountEntries { get; private set; }
        public TextView tv_SOPNUMBE { get; private set; }
        public TextView tv_BarcodeP { get; private set; }

        public TextView tv_ITEMNMBR { get; private set; }
        public TextView tv_QTYSHPPD { get; private set; }
        public TextView tv_QTYODVEDENO { get; private set; }
        public TextView tv_ITEMMJ { get; private set; }


        public OperaceVyber_Item_Holder(View itemView, int ID_LinearLayout, Action<int> listenerClick, Action<int> listenerLongClick, Action<Object, View.FocusChangeEventArgs> listenerFocusChange) : base(itemView, ID_LinearLayout)
        {
            _itemView = itemView;

            tv_ITEMDESC = (TextView)itemView.FindViewById(Resource.Id.OperaceVyber_ITEMDESC);
            tv_CountEntries = (TextView)itemView.FindViewById(Resource.Id.OperaceVyber_CountEntries);
            tv_SOPNUMBE = (TextView)itemView.FindViewById(Resource.Id.OperaceVyber_SOPNUMBE);
            tv_BarcodeP = (TextView)itemView.FindViewById(Resource.Id.OperaceVyber_BarcodeP);

            tv_ITEMNMBR = (TextView)itemView.FindViewById(Resource.Id.OperaceVyber_ITEMNMBR);
            tv_QTYSHPPD = (TextView)itemView.FindViewById(Resource.Id.OperaceVyber_QTYSHPPD);
            tv_QTYODVEDENO = (TextView)itemView.FindViewById(Resource.Id.OperaceVyber_QTYODVEDENO);
            tv_ITEMMJ = (TextView)itemView.FindViewById(Resource.Id.OperaceVyber_ITEMMJ);

            itemView.Click += (sender, e) => listenerClick(base.AdapterPosition);
            itemView.LongClick += (sender, e) => listenerLongClick(base.AdapterPosition);
            itemView.FocusChange += (sender, e) => listenerFocusChange(sender, e);
        }

    }
}