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
    public class Prodej_TypDokladu_Item_Adapter : Adapter 
    {

 
        public Prodej_TypDokladu_Seznam PolozkaSeznam_Original;
        public Prodej_TypDokladu_Seznam PolozkaSeznam;

 
        public override int ItemCount { get { return PolozkaSeznam.numPolozek; }}


        public Prodej_TypDokladu_Item_Adapter(Prodej_TypDokladu Parent, Prodej_TypDokladu_Seznam polozkaSeznam) : base(Parent)
        {
            PolozkaSeznam = polozkaSeznam;
            Filter = new Prodej_TypDokladu_Filter(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.Prodej_TypDokladu_ItemV2, parent, false);

            Prodej_TypDokladu_Item_Holder vh = new Prodej_TypDokladu_Item_Holder(itemView, Resource.Id.Prodej_TypDokladu_LL, OnClick, OnLongClick, OnFocusChange);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Prodej_TypDokladu_Item_Holder vh = holder as Prodej_TypDokladu_Item_Holder;

            string doc_desc = PolozkaSeznam[position].Isdoc_descNull() ? "-" : PolozkaSeznam[position].doc_desc;
            doc_desc = string.IsNullOrEmpty(doc_desc) ? "-" : doc_desc;
            vh.tv_DocDesc.Text = doc_desc;

            vh.tv_DocCarcode.SetText(GetString_Spannable( Resource.String.ProdejTypDokladu_Carkod, PolozkaSeznam[position].Isdoc_carcodeNull() ? "" : PolozkaSeznam[position].doc_carcode), TextView.BufferType.Spannable);
            vh.tv_DocID.SetText(GetString_Spannable(Resource.String.ProdejTypDokladu_ID, PolozkaSeznam[position].doc_id ), TextView.BufferType.Spannable);
            vh.tv_DocID2.SetText(GetString_Spannable(Resource.String.ProdejTypDokladu_ID2, PolozkaSeznam[position].doc_id2), TextView.BufferType.Spannable);
            vh.tv_DocTyp.SetText(GetString_Spannable(Resource.String.ProdejTypDokladu_Typ, PolozkaSeznam[position].Isdoc_typNull() ? "-" : PolozkaSeznam[position].doc_typ), TextView.BufferType.Spannable);

            vh.linearLayout.SetBackgroundResource(Resource.Drawable.lost_focus_border_prodej);
        }

    }
}