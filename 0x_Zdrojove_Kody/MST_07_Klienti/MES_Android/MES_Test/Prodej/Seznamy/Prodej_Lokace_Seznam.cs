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
    public class Prodej_Lokace_Seznam : Java.Lang.Object
    {

        public Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable mItems = new Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable();


        public Prodej_Lokace_Seznam(Fask.SQLiteDBs.DataSets.Lokace.CZMST094DataTable listPolozek)
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

        public Fask.SQLiteDBs.DataSets.Lokace.CZMST094Row this[int i]
        {
            get { return mItems[i]; }
        }

        internal void Move(int fromPosition, int toPosition)
        {
            mItems.Rows.InsertAt(mItems[fromPosition], toPosition);
            mItems.RemoveCZMST094Row(mItems[fromPosition]);
        }

        internal void Remove(int position)
        {
            mItems.RemoveCZMST094Row(mItems[position]);
        }
    }
}