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

using Android.Support.V4.View;
using Android.Support.V7.Widget;

namespace MES_Android.ItemTouch
{
    public class TouchListenerHelper : Java.Lang.Object, View.IOnTouchListener
    {
        private RecyclerView.ViewHolder _itemHolder;
        private IOnStartDragListener _mDragStartListener;

        public TouchListenerHelper(RecyclerView.ViewHolder holder, IOnStartDragListener mDragStartListener)
        {
            _itemHolder = holder;
            _mDragStartListener = mDragStartListener;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                _mDragStartListener.OnStartDrag(_itemHolder);
            }
            return false;
        }
    }
}