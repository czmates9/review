using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Java.Lang;
using MES_Android.ItemTouch;


namespace MES_Android.Prodej
{
    public class Prodej_SberDat_Item_Adapter : Adapter 
    {
        public Prodej_SberDat_Seznam PolozkaSeznam_Original;
        public Prodej_SberDat_Seznam PolozkaSeznam;

        public override int ItemCount { get { return PolozkaSeznam.numPolozek; }}

        public Prodej_SberDat_Item_Holder vh;

        public Prodej_SberDat_Item_Adapter(Prodej_SberDat Parent, Prodej_SberDat_Seznam polozkaSeznam) : base(Parent)
        {
            PolozkaSeznam = polozkaSeznam;

            Filter = new Prodej_SberDat_Filter(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.Prodej_SberDat_Item, parent, false);

            vh = new Prodej_SberDat_Item_Holder(itemView, Resource.Id.Prodej_SberDat_LL, OnClick, OnLongClick, OnFocusChange);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Prodej_SberDat_Item_Holder vh = holder as Prodej_SberDat_Item_Holder;
            vh.tv_ITEMDESC.Text = PolozkaSeznam[position].IsITEMDESCNull() ? "-" : PolozkaSeznam[position].ITEMDESC;
            vh.tv_QTYSHPPD.SetText(GetString_Spannable(Resource.String.ProdejSberDat_QTYSHPPD, PolozkaSeznam[position].QTYSHPPD), TextView.BufferType.Spannable);

            DateTime dateTime = new DateTime(
                int.Parse(PolozkaSeznam[position].DATEDONE.Substring(0, 4)),
                int.Parse(PolozkaSeznam[position].DATEDONE.Substring(4, 2)),
                int.Parse(PolozkaSeznam[position].DATEDONE.Substring(6, 2)),
                int.Parse(PolozkaSeznam[position].TIMEDONE.Substring(0, 2)),
                int.Parse(PolozkaSeznam[position].TIMEDONE.Substring(2, 2)),
                int.Parse(PolozkaSeznam[position].TIMEDONE.Substring(4, 2)));

            vh.tv_Datum.SetText(GetString_Spannable(Resource.String.ProdejSberDat_Datum, dateTime), TextView.BufferType.Spannable);

            vh.linearLayout.SetBackgroundResource(Resource.Drawable.lost_focus_border_prodej);
        }

        public void AddItem(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow _Item)
        {
            if (PolozkaSeznam.mItems.Count == 0)
            {
                PolozkaSeznam.mItems.ImportRow(_Item);

                if (PolozkaSeznam_Original != null)
                    PolozkaSeznam_Original.mItems.ImportRow(_Item);

                NotifyItemInserted(0);
            }
            else
            {
                PolozkaSeznam.mItems.ImportRow(_Item);

                if (PolozkaSeznam_Original != null)
                    PolozkaSeznam_Original.mItems.ImportRow( _Item);

                NotifyItemInserted(PolozkaSeznam.mItems.Count);

            }

        }

        public void RemoveItem(int position)
        {

            PolozkaSeznam.Remove(position);

            if (PolozkaSeznam_Original != null)
            {
                PolozkaSeznam_Original.Remove(position);
            }

            NotifyItemRemoved(position);
        }

    }
}