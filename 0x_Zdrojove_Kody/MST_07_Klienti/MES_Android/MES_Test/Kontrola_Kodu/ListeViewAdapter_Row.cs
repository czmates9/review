using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android.Kontrola_Kodu
{
    class ListeViewAdapter_Row : BaseAdapter<Row_Code>
    {
        public List<Row_Code> mItems; // neco jak pole stringu ale je to list to znamena ze je moznost pridavat
        private Context mContext;  // Rozhraní globálních informací o aplikačním prostředí.
        private int[] mAlternatingColors;
        private int mRowLayout;

        //
        // Summary:
        //     ctor pro adapter
        //
        public ListeViewAdapter_Row(Context context, int rowLayout, List<Row_Code> items)
        {
            mItems = items;
            mContext = context;
            mRowLayout = rowLayout;
            mAlternatingColors = new int[] { 0xF2F2F2, 0x23A4BF };
        }

        //
        // Summary : 
        //          pocet v Listview
        //
        public override int Count
        {
            get { return mItems.Count; }
        }

        //
        // Summary : 
        //          vraci ID polozky
        //
        public override long GetItemId(int position)
        {
            return position;
        }

        //
        // Summary : 
        //          vraci pozici v listview
        //
        public override Row_Code this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(mRowLayout, parent, false);
            }

            row.SetBackgroundColor(GetColorFromInteger(mAlternatingColors[position % mAlternatingColors.Length]));

            TextView txtID = row.FindViewById<TextView>(Resource.Id.txt_ParsingCodes_ID);
            txtID.Text = mItems[position].Code;


            if ((position % 2) == 1)
            {
                //row.SetBackgroundColor(Color.Rgb(Color.GetRedComponent(54), Color.GetGreenComponent(141), Color.GetBlueComponent(235)));
                //Green background, set text white
                txtID.SetTextColor(Color.White);

            }

            else
            {
                //row.SetBackgroundColor(Color.Rgb(Color.GetRedComponent(255), Color.GetGreenComponent(255), Color.GetBlueComponent(255)));
                //White background, set text black
                txtID.SetTextColor(Color.Black);

            }

            return row;
        }

        private Color GetColorFromInteger(int color)
        {
            return Color.Rgb(Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color));
        }
    }
}