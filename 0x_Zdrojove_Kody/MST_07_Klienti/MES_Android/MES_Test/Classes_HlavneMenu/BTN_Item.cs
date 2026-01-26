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

namespace MES_Android.Classes_HlavneMenu
{
    public delegate void Volac<in T1>(T1 arg1); // Volač

    public class BTN_Item
    {
        public string Nazev { get; set; }
        public int ImageID;
        private Volac<string> button_Click;
        public bool isFocus { get; set; }

        public BTN_Item(string nazev, int imageID, Volac<string> button_Click, bool isfocus)
        {
            this.Nazev = nazev;
            this.ImageID = imageID;
            this.button_Click = button_Click;
            this.isFocus = isfocus;
        }

        public void MyButtonClick(object sender, EventArgs e)
        {

            try
            {
                string x = string.Empty;
                LinearLayout linLay = null;

                var btn = (sender as ImageButton);

                if (btn != null)
                    linLay = btn.Parent as LinearLayout;

                if (linLay == null)
                    linLay = (sender as LinearLayout);

                if (linLay == null)
                    x = "error";
                else
                {
                    int count = linLay.ChildCount;
                    View v = null;
                    for (int i = 0; i < count; i++)
                    {
                        v = linLay.GetChildAt(i);
                        //do something with your child element
                        if (v.Id == Resource.Id.txt)
                        {

                            TextView tv = v as TextView;

                            x = tv.Text;
                        }
                    }
                }

                this.button_Click.Invoke(x);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FocusChange(object sender, View.FocusChangeEventArgs e)
        {

            int count = ((LinearLayout)sender).ChildCount;
            View v = null;
            for (int i = 0; i < count; i++)
            {
                v = ((LinearLayout)sender).GetChildAt(i);
                //do something with your child element
                if (v.Id == Resource.Id.txt)
                {

                    TextView tv = v as TextView;
                    if (e.HasFocus)
                    {
                        tv.SetBackgroundResource(Resource.Drawable.focus_textview_border);
                    }
                    else
                    {
                        tv.SetBackgroundResource(Resource.Drawable.lost_focus_textview_border);
                    }

                }
            }
        }
    }
}