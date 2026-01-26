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
    public class Prodej_Zasoby_Item_Adapter : Adapter
    {

        public Prodej_Zasoby_Seznam PolozkaSeznam_Original;
        public Prodej_Zasoby_Seznam PolozkaSeznam;

        public override int ItemCount { get { return PolozkaSeznam.numPolozek; }}


        public Prodej_Zasoby_Item_Adapter(AppCompatActivity Parent, Prodej_Zasoby_Seznam polozkaSeznam) : base(Parent)
        {
            PolozkaSeznam = polozkaSeznam;
            Filter = new Prodej_Zasoby_Filter(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.Prodej_Zasoby_Item, parent, false);

            Prodej_Zasoby_Item_Holder vh = new Prodej_Zasoby_Item_Holder(itemView, Resource.Id.Prodej_Zasoby_LL, OnClick, OnLongClick, OnFocusChange);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Prodej_Zasoby_Item_Holder vh = holder as Prodej_Zasoby_Item_Holder;
            vh.tv_ITEMDESC.Text = PolozkaSeznam[position].IsITEMDESCNull() ? "-" : PolozkaSeznam[position].ITEMDESC;
            vh.tv_ITEMNMBR.Text = GetString(Resource.String.ProdejZasoby_ITEMNMBR, PolozkaSeznam[position].ITEMNMBR);
            vh.tv_ITEMCODE.Text = GetString(Resource.String.ProdejZasoby_ITEMCODE, PolozkaSeznam[position].IsITEMCODENull() ? "-" : PolozkaSeznam[position].ITEMCODE);
            vh.tv_VNDITNUM.Text = GetString(Resource.String.ProdejZasoby_VNDITNUM, PolozkaSeznam[position].IsVNDITNUMNull() ? "-" : PolozkaSeznam[position].VNDITNUM);
            vh.tv_CZ_CarKod.Text = GetString(Resource.String.ProdejZasoby_CZCarKod, PolozkaSeznam[position].IsCZ_CarKodNull() ? "-" : PolozkaSeznam[position].CZ_CarKod);

            vh.linearLayout.SetBackgroundResource(Resource.Drawable.lost_focus_border_prodej);
        }

    }
}