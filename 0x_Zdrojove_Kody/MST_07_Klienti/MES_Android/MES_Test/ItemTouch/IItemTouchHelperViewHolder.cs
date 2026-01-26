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

namespace MES_Android.ItemTouch
{
    public interface IItemTouchHelperViewHolder
    {
        void OnItemSelected();

        void OnItemClear();
    }
}