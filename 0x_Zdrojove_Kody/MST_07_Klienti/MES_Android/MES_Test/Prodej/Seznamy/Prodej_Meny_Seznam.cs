using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;




namespace MES_Android.Prodej
{
    public class Prodej_Meny_Seznam : Java.Lang.Object
    {

        public Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable mItems = new Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable();


        public Prodej_Meny_Seznam(Fask.SQLiteDBs.DataSets.Meny.CZMST097DataTable listPolozek)
        {
            this.mItems = listPolozek;
        }

        public int numPolozek
        {
            get
            {
                return mItems.Count;
            }
        }

        public Fask.SQLiteDBs.DataSets.Meny.CZMST097Row this[int i]
        {
            get { return mItems[i]; }
        }

        internal void Move(int fromPosition, int toPosition)
        {
            mItems.Rows.InsertAt(mItems[fromPosition], toPosition);
            mItems.RemoveCZMST097Row(mItems[fromPosition]);
        }

        internal void Remove(int position)
        {
            mItems.RemoveCZMST097Row(mItems[position]);
        }
    }
}