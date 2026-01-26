using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MES_Android.Prodej
{
    public class Prodej_Lokace_Item_Adapter : Adapter
    {

        public Prodej_Lokace_Seznam PolozkaSeznam_Original;
        public Prodej_Lokace_Seznam PolozkaSeznam;

       // public Filter Filter { get; private set; }

        public override int ItemCount { get { return PolozkaSeznam.numPolozek; }}


        public Prodej_Lokace_Item_Adapter(AppCompatActivity Parent, Prodej_Lokace_Seznam polozkaSeznam) : base(Parent)
        {
            //mDragStartListener = dragStartListener;
            PolozkaSeznam = polozkaSeznam;

            Filter = new Prodej_Lokace_Filter(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            //Inflate(Resource.Layout.Prodej_TypDokladu_ItemV1, parent, false);
            Inflate(Resource.Layout.Prodej_Lokace_Item, parent, false);

            Prodej_Lokace_Item_Holder vh = new Prodej_Lokace_Item_Holder(itemView, Resource.Id.Prodej_Lokace_LL, OnClick, OnLongClick, OnFocusChange);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Prodej_Lokace_Item_Holder vh = holder as Prodej_Lokace_Item_Holder;

            
            string LOCNCODE = string.IsNullOrEmpty(PolozkaSeznam[position].LOCNCODE) ? "-" : PolozkaSeznam[position].LOCNCODE;
            vh.tv_LOCNCODE.Text = LOCNCODE;

            vh.tv_BarCode.SetText(GetString_Spannable(Resource.String.ProdejLokace_BarCode, PolozkaSeznam[position].IsBarcodeNull() ? "-" : PolozkaSeznam[position].Barcode), TextView.BufferType.Spannable);
            vh.tv_desc.SetText(GetString_Spannable(Resource.String.ProdejLokace_Desc, PolozkaSeznam[position].IsDescriptionNull() ? "-" : PolozkaSeznam[position].Description), TextView.BufferType.Spannable);
            vh.tv_SKL_ID.SetText(GetString_Spannable(Resource.String.ProdejLokace_SKL_ID, PolozkaSeznam[position].IsSKL_IDNull() ? "-" : PolozkaSeznam[position].SKL_ID), TextView.BufferType.Spannable);
            vh.tv_Type.SetText(GetString_Spannable(Resource.String.ProdejLokace_Type, PolozkaSeznam[position].IsTYPENull() ? "-" : PolozkaSeznam[position].TYPE), TextView.BufferType.Spannable);

            vh.linearLayout.SetBackgroundResource(Resource.Drawable.lost_focus_border_prodej);
        }

    }
}