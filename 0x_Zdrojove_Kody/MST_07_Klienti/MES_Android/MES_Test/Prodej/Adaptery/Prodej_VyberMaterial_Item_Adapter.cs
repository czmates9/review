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
    public class Prodej_VyberMaterial_Item_Adapter : Adapter
    {

        public Prodej_VyberMaterial_Seznam PolozkaSeznam_Original;
        public Prodej_VyberMaterial_Seznam PolozkaSeznam;

       // public Filter Filter { get; private set; }

        public override int ItemCount { get { return PolozkaSeznam.numPolozek; }}


        public Prodej_VyberMaterial_Item_Adapter(AppCompatActivity Parent, Prodej_VyberMaterial_Seznam polozkaSeznam) : base(Parent)
        {
            //mDragStartListener = dragStartListener;
            PolozkaSeznam = polozkaSeznam;

            Filter = new Prodej_VyberMaterial_Filter(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            //Inflate(Resource.Layout.Prodej_TypDokladu_ItemV1, parent, false);
            Inflate(Resource.Layout.Prodej_VyberMaterialu_Item, parent, false);

            Prodej_VyberMaterial_Item_Holder vh = new Prodej_VyberMaterial_Item_Holder(itemView, Resource.Id.Prodej_VyberMaterialu_LL, OnClick, OnLongClick, OnFocusChange);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Prodej_VyberMaterial_Item_Holder vh = holder as Prodej_VyberMaterial_Item_Holder;
            string ITEMDESC = PolozkaSeznam[position].IsITEMDESCNull() ? "-" : PolozkaSeznam[position].ITEMDESC;
            ITEMDESC = string.IsNullOrEmpty(ITEMDESC) ? "-" : ITEMDESC;
            vh.tv_Popis.Text = ITEMDESC;
            
            
            vh.tv_QTY.SetText(GetString_Spannable(Resource.String.ProdejVyberMaterial_QTY, PolozkaSeznam[position].QTYSHPPD.ToString(Config.Settings.UIFormatDesCisel)), TextView.BufferType.Spannable);
            vh.tv_Lokace.SetText(GetString_Spannable(Resource.String.ProdejVyberMaterial_Lokace, PolozkaSeznam[position].IsLOCNCODENull() ? "-" : PolozkaSeznam[position].LOCNCODE), TextView.BufferType.Spannable);
            vh.tv_PolozkaC.SetText(GetString_Spannable(Resource.String.ProdejVyberMaterial_PolozkaC, PolozkaSeznam[position].ITEMNMBR), TextView.BufferType.Spannable); 
            vh.tv_SN.SetText(GetString_Spannable(Resource.String.ProdejVyberMaterial_SN, string.IsNullOrEmpty(PolozkaSeznam[position].SERLTNUM.Trim()) ? "-" : PolozkaSeznam[position].SERLTNUM), TextView.BufferType.Spannable);

            vh.linearLayout.SetBackgroundResource(Resource.Drawable.lost_focus_border_prodej);
        }

    }
}