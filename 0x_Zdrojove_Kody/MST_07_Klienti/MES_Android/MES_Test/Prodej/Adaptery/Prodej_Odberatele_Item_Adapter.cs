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
    public class Prodej_Odberatele_Item_Adapter : Adapter
    {

        public Prodej_Odberatele_Seznam PolozkaSeznam_Original;
        public Prodej_Odberatele_Seznam PolozkaSeznam;

       // public Filter Filter { get; private set; }

        public override int ItemCount { get { return PolozkaSeznam.numPolozek; }}


        public Prodej_Odberatele_Item_Adapter(AppCompatActivity Parent, Prodej_Odberatele_Seznam polozkaSeznam) : base(Parent)
        {
            //mDragStartListener = dragStartListener;
            PolozkaSeznam = polozkaSeznam;

            Filter = new Prodej_Odberatele_Filter(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            //Inflate(Resource.Layout.Prodej_TypDokladu_ItemV1, parent, false);
            Inflate(Resource.Layout.Prodej_Odberatele_Item, parent, false);

            Prodej_Odberatele_Item_Holder vh = new Prodej_Odberatele_Item_Holder(itemView, Resource.Id.Prodej_Odberatele_LL, OnClick, OnLongClick, OnFocusChange);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Prodej_Odberatele_Item_Holder vh = holder as Prodej_Odberatele_Item_Holder;
            vh.tv_Popis.Text = PolozkaSeznam[position].Isodb_descNull() ? "-" : PolozkaSeznam[position].odb_desc;
            vh.tv_ID.Text = GetString(Resource.String.ProdejOdberatele_ID, PolozkaSeznam[position].odb_id);
            vh.tv_CKod.Text = GetString(Resource.String.ProdejOdberatele_CKod, PolozkaSeznam[position].odb_carcode);
            vh.tv_ICO.Text = GetString(Resource.String.ProdejOdberatele_ICO, PolozkaSeznam[position].Isodb_icoNull() ? "-" : PolozkaSeznam[position].odb_ico.ToString());
            vh.tv_DIC.Text = GetString(Resource.String.ProdejOdberatele_DIC, PolozkaSeznam[position].Isodb_dicNull() ? "-" : PolozkaSeznam[position].odb_dic.ToString());

            vh.linearLayout.SetBackgroundResource(Resource.Drawable.lost_focus_border_prodej);
        }

    }
}