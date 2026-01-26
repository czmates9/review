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

namespace MES_Android.OdvadeniVyroby
{
    public class PrikazVyber_Item_Adapter : Adapter
    {

        public PrikazVyber_Seznam PolozkaSeznam_Original;
        public PrikazVyber_Seznam PolozkaSeznam;

        // public Filter Filter { get; private set; }

        public override int ItemCount { get { return PolozkaSeznam.numPolozek; } }


        public PrikazVyber_Item_Adapter(AppCompatActivity Parent, PrikazVyber_Seznam polozkaSeznam) : base(Parent)
        {
            //mDragStartListener = dragStartListener;
            PolozkaSeznam = polozkaSeznam;

            Filter = new PrikazVyber_Filter(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            //Inflate(Resource.Layout.Prodej_TypDokladu_ItemV1, parent, false);
            Inflate(Resource.Layout.PrikazVyber_Item, parent, false);

            PrikazVyber_Item_Holder vh = new PrikazVyber_Item_Holder(itemView, Resource.Id.PrikazVyber_LL, OnClick, OnLongClick, OnFocusChange);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PrikazVyber_Item_Holder vh = holder as PrikazVyber_Item_Holder;
            vh.tv_SOPNUMBE.Text = string.IsNullOrEmpty(PolozkaSeznam[position].SOPNUMBE) ? "-" : PolozkaSeznam[position].SOPNUMBE;
            vh.tv_SOPDESC.Text = GetString(Resource.String.PrikazVyber_SOPDESC, PolozkaSeznam[position].IsSOPDESCNull() ? "-" : PolozkaSeznam[position].SOPDESC.Trim());
            vh.tv_CountEntries.Text = GetString(Resource.String.PrikazVyber_CountEntries, PolozkaSeznam[position].CountEntries);
            vh.tv_DateProd.Text = GetString(Resource.String.PrikazVyber_DateProd, PolozkaSeznam[position].DateProd);
            vh.tv_BarcodeH.Text = GetString(Resource.String.PrikazVyber_BarcodeH, string.IsNullOrEmpty(PolozkaSeznam[position].BarcodeH) ? "-" : PolozkaSeznam[position].BarcodeH.ToString());

            vh.linearLayout.SetBackgroundResource(Resource.Drawable.lost_focus_border_OV);
        }

    }
}