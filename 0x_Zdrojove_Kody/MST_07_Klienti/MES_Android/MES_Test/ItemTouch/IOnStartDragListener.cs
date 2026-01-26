using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V7.Widget;

namespace MES_Android.ItemTouch
{
    public interface IOnStartDragListener
    {
        void OnStartDrag(RecyclerView.ViewHolder viewHolder);
    }
}