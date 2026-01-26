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
    public class Prodej_Meny_Item_Adapter : Adapter
    {

        public Prodej_Meny_Seznam PolozkaSeznam_Original;
        public Prodej_Meny_Seznam PolozkaSeznam;

       // public Filter Filter { get; private set; }

        public override int ItemCount { get { return PolozkaSeznam.numPolozek; }}


        public Prodej_Meny_Item_Adapter(AppCompatActivity Parent, Prodej_Meny_Seznam polozkaSeznam) : base(Parent)
        {
            //mDragStartListener = dragStartListener;
            PolozkaSeznam = polozkaSeznam;

            Filter = new Prodej_Meny_Filter(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            //Inflate(Resource.Layout.Prodej_TypDokladu_ItemV1, parent, false);
            Inflate(Resource.Layout.Prodej_Meny_Item, parent, false);

            Prodej_Meny_Item_Holder vh = new Prodej_Meny_Item_Holder(itemView, Resource.Id.Prodej_Meny_LL, OnClick, OnLongClick, OnFocusChange);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Prodej_Meny_Item_Holder vh = holder as Prodej_Meny_Item_Holder;
            vh.tv_Popis.Text = PolozkaSeznam[position].mena_text;
            vh.tv_ID.Text = GetString(Resource.String.ProdejMeny_ID, PolozkaSeznam[position].mena_ID);
            vh.tv_Vychozi.Text = GetString(Resource.String.ProdejMeny_Vychozi, PolozkaSeznam[position].mena_hlavni);
            vh.tv_Kurz.Text = GetString(Resource.String.ProdejMeny_Kurz, PolozkaSeznam[position].Ismena_kurzNull() ? "-" : PolozkaSeznam[position].mena_kurz.ToString());
            vh.tv_DatumKurzu.Text = GetString(Resource.String.ProdejMeny_DatumKurzu, PolozkaSeznam[position].Ismena_kurzDatumNull() ? "-" : PolozkaSeznam[position].mena_kurzDatum.ToString());

            vh.linearLayout.SetBackgroundResource(Resource.Drawable.lost_focus_border_prodej);
        }

    }
}