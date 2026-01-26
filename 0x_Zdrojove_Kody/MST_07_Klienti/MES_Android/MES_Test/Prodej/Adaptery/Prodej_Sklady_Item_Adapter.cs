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
    public class Prodej_Sklady_Item_Adapter : Adapter
    {

        public Prodej_Sklady_Seznam PolozkaSeznam_Original;
        public Prodej_Sklady_Seznam PolozkaSeznam;

        public override int ItemCount { get { return PolozkaSeznam.numPolozek; }}


        public Prodej_Sklady_Item_Adapter(AppCompatActivity Parent, Prodej_Sklady_Seznam polozkaSeznam) : base(Parent)
        {
            PolozkaSeznam = polozkaSeznam;
            Filter = new Prodej_Sklady_Filter(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.Prodej_Sklady_Item, parent, false);

            Prodej_Sklady_Item_Holder vh = new Prodej_Sklady_Item_Holder(itemView, Resource.Id.Prodej_Sklady_LL, OnClick, OnLongClick, OnFocusChange);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Prodej_Sklady_Item_Holder vh = holder as Prodej_Sklady_Item_Holder;
            vh.tv_Popis.Text = PolozkaSeznam[position].Isskl_descNull() ? "-" : PolozkaSeznam[position].skl_desc;
            vh.tv_ID.Text = GetString(Resource.String.ProdejSklady_ID, PolozkaSeznam[position].skl_id);
            vh.tv_Typ.Text = GetString(Resource.String.ProdejSklady_Typ, PolozkaSeznam[position].Isskl_typNull() ? "-" : PolozkaSeznam[position].skl_typ.ToString());
            vh.tv_Ckod.Text = GetString(Resource.String.ProdejSklady_Ckod, PolozkaSeznam[position].Isskl_carcodeNull() ? "-" : PolozkaSeznam[position].skl_carcode.ToString());
            vh.tv_DRI.Text = GetString(Resource.String.ProdejSklady_DRI, PolozkaSeznam[position].DEX_ROW_ID);

            vh.linearLayout.SetBackgroundResource(Resource.Drawable.lost_focus_border_prodej);
        }

    }
}