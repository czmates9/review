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
    public class Prodej_Davka_Item_Adapter : Adapter
    {

        public Prodej_Davka_Seznam PolozkaSeznam_Original;
        public Prodej_Davka_Seznam PolozkaSeznam;

        public override int ItemCount { get { return PolozkaSeznam.numPolozek; }}

        //private Prodej_Davky _parent;

        public Prodej_Davka_Item_Adapter(Prodej_Davky Parent, Prodej_Davka_Seznam polozkaSeznam) : base(Parent)
        {
            PolozkaSeznam = polozkaSeznam;
            Filter = new Prodej_Davka_Filter(this);

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.Prodej_Davky_Item, parent, false);

            Prodej_Davka_Item_Holder vh = new Prodej_Davka_Item_Holder(itemView,Resource.Id.Prodej_Davky_LL, OnClick, OnLongClick, OnFocusChange);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = PolozkaSeznam[position];

            Prodej_Davka_Item_Holder vh = holder as Prodej_Davka_Item_Holder;
            vh.tv_CisloDavky.Text = PolozkaSeznam[position].CisloDavky.HasValue ? PolozkaSeznam[position].CisloDavky.Value.ToString() : "-";
            vh.tv_Pocet.Text = GetString(Resource.String.ProdejDavka_PocetPolozek, PolozkaSeznam[position].PocetRadku);

            vh.linearLayout.SetBackgroundResource(Resource.Drawable.lost_focus_border_prodej);

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


        public void AddItem(Prodej_Davka_Item _Item)
        {
            if (PolozkaSeznam.mItems.Count == 0)
            {
                PolozkaSeznam.mItems.Insert(0, _Item);

                if (PolozkaSeznam_Original != null)
                    PolozkaSeznam_Original.mItems.Insert(0, _Item);

                NotifyItemInserted(0);
            }
            else
            {

                Prodej_Davka_Item pol = PolozkaSeznam.mItems.Last();
                int index = PolozkaSeznam.mItems.IndexOf(pol);

                PolozkaSeznam.mItems.Insert(index + 1, _Item);

                if (PolozkaSeznam_Original != null)
                    PolozkaSeznam_Original.mItems.Insert(index + 1, _Item);

                NotifyItemInserted(index + 1);

            }

        }

    }
}