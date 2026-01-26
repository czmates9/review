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

namespace MES_Android.Classes_HlavneMenu
{
    public class RecyclerHolder : RecyclerView.ViewHolder
    {
        public ImageButton imageButton { get; private set; }
        public LinearLayout linearLayout { get; private set; }
        public TextView textView { get; private set; }

        public RecyclerHolder(View itemView) : base(itemView)
        {
            imageButton = (ImageButton)itemView.FindViewById(Resource.Id.btn);
            linearLayout = (LinearLayout)itemView.FindViewById(Resource.Id.linearLayout1);
            textView = (TextView)itemView.FindViewById(Resource.Id.txt);
        }
    }
}