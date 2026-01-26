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
    public class RecyclerAdapter : RecyclerView.Adapter
    {
        BTN_Item[] items;

        public RecyclerAdapter(BTN_Item[] data)
        {
            items = data;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
            Inflate(Resource.Layout.HlavneMenu_Item, parent, false);

            RecyclerHolder vh = new RecyclerHolder(itemView);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            var holder = viewHolder as RecyclerHolder;
            holder.imageButton.SetBackgroundResource(item.ImageID);
            holder.textView.Text = item.Nazev;
            holder.linearLayout.Click += item.MyButtonClick;
            holder.imageButton.Click += item.MyButtonClick;
            holder.linearLayout.FocusChange += item.FocusChange;

            if (item.isFocus)
            {
                holder.textView.SetBackgroundResource(Resource.Drawable.focus_textview_border);
                holder.textView.Selected = true;
            }
            else
            {
                holder.textView.SetBackgroundResource(Resource.Drawable.lost_focus_textview_border);
                holder.textView.Selected = false;
            }
        }

        public override int ItemCount
        {
            get
            {
                return items.Length;
            }
        }
    }
}