using Android.App;
using Android.Content;
using Android.Graphics;
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
    public abstract class Adapter : RecyclerView.Adapter, IFilterable
    {
        public event EventHandler<int> ItemClick;
        public event EventHandler<int> ItemLongClick;


        public Filter Filter { get; set; }
        private AppCompatActivity _parent { get; set; }


        public Adapter(AppCompatActivity Parent)
        {
            _parent = Parent;
        }

        public string GetString(int ID)
        {
            return _parent.GetString(ID);
        }


        public string GetString(int ID, params object[] parametry)
        {
            return string.Format(_parent.GetString(ID), parametry);
        }

        public Android.Text.SpannableString GetString_Spannable(int ID, params object[] parametry)
        {
            var S = string.Format(_parent.GetString(ID), parametry);

            Android.Text.SpannableString span = new Android.Text.SpannableString(S);
            int index = S.IndexOf(':');
            if (index > 0)
            {
                span.SetSpan(new Android.Text.Style.ForegroundColorSpan(Color.Red), index + 1, S.Length, 0);
                span.SetSpan(new Android.Text.Style.StyleSpan(TypefaceStyle.Bold), index + 1, S.Length, 0);
            }
            return span;
        }

        #region Metoda pro Click Event

        public void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }

        public void OnLongClick(int obj)
        {
            if (ItemLongClick != null)
                ItemLongClick(this, obj);
        }

        #endregion

        public void OnFocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (sender is LinearLayout)
            {
                var ll = ((LinearLayout)sender);

                if (e.HasFocus)
                {
                    ll.SetBackgroundResource(Resource.Drawable.focus_border_OV);
                }
                else
                {
                    ll.SetBackgroundResource(Resource.Drawable.lost_focus_border_OV);
                }
            }
        }
    }
}