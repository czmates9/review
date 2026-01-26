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
    public class OperaceVyber_Item_Adapter : Adapter
    {

        public OperaceVyber_Seznam PolozkaSeznam_Original;
        public OperaceVyber_Seznam PolozkaSeznam;

        // public Filter Filter { get; private set; }

        public override int ItemCount { get { return PolozkaSeznam.numPolozek; } }


        public OperaceVyber_Item_Adapter(AppCompatActivity Parent, OperaceVyber_Seznam polozkaSeznam) : base(Parent)
        {
            //mDragStartListener = dragStartListener;
            PolozkaSeznam = polozkaSeznam;

            Filter = new OperaceVyber_Filter(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            //Inflate(Resource.Layout.Prodej_TypDokladu_ItemV1, parent, false);
            Inflate(Resource.Layout.OperaceVyber_Item, parent, false);

           OperaceVyber_Item_Holder vh = new OperaceVyber_Item_Holder(itemView, Resource.Id.OperaceVyber_LL, OnClick, OnLongClick, OnFocusChange);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
           OperaceVyber_Item_Holder vh = holder as OperaceVyber_Item_Holder;

            vh.tv_ITEMDESC.Text = GetString(Resource.String.OperaceVyber_ITEMDESC, string.IsNullOrEmpty(PolozkaSeznam[position].ITEMDESC) ? "-" : PolozkaSeznam[position].ITEMDESC.ToString());
            vh.tv_CountEntries.Text = GetString(Resource.String.PrikazVyber_CountEntries, PolozkaSeznam[position].CountEntries);
            vh.tv_SOPNUMBE.Text = GetString(Resource.String.OperaceVyber_SOPNUMBE, string.IsNullOrEmpty(PolozkaSeznam[position].SOPNUMBE) ? "-" : PolozkaSeznam[position].SOPNUMBE.ToString());
            vh.tv_BarcodeP.Text = GetString(Resource.String.OperaceVyber_BarcodeP, string.IsNullOrEmpty(PolozkaSeznam[position].BarcodeP) ? "-" : PolozkaSeznam[position].BarcodeP.ToString());

            vh.tv_ITEMNMBR.Text = GetString(Resource.String.OperaceVyber_ITEMNMBR, string.IsNullOrEmpty(PolozkaSeznam[position].ITEMNMBR) ? "-" : PolozkaSeznam[position].ITEMNMBR.ToString());
            vh.tv_QTYSHPPD.Text = GetString(Resource.String.OperaceVyber_QTYSHPPD, PolozkaSeznam[position].QTYSHPPD);
            vh.tv_QTYODVEDENO.Text = GetString(Resource.String.OperaceVyber_QTYODVEDENO, PolozkaSeznam[position].QTYODVEDENO);
            vh.tv_ITEMMJ.Text = GetString(Resource.String.OperaceVyber_ITEMMJ, string.IsNullOrEmpty(PolozkaSeznam[position].ITEMMJ) ? "-" : PolozkaSeznam[position].ITEMMJ.ToString());

            vh.linearLayout.SetBackgroundResource(Resource.Drawable.lost_focus_border_OV);
        }

    }
}