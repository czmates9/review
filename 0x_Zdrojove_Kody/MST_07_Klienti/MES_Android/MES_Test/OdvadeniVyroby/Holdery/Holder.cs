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

namespace MES_Android.OdvadeniVyroby
{
    public abstract class Holder : RecyclerView.ViewHolder
    {

        public View _itemView;
        public LinearLayout linearLayout { get; set; }

        public Holder(View itemView, int ID_LinearLayout) : base(itemView)
        {
            linearLayout = (LinearLayout)itemView.FindViewById(ID_LinearLayout);
        }
    }
}