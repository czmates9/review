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
    public class Prodej_Sklady_Seznam : Java.Lang.Object
    {
        public Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable mItems = new Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable();


        public Prodej_Sklady_Seznam(Fask.SQLiteDBs.DataSets.Sklady.CZMST093DataTable listPolozek)
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

        public Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row this[int i]
        {
            get { return mItems[i]; }
        }

        internal void Move(int fromPosition, int toPosition)
        {
            mItems.Rows.InsertAt(mItems[fromPosition], toPosition);
            mItems.RemoveCZMST093Row(mItems[fromPosition]);
        }

        internal void Remove(int position)
        {
            mItems.RemoveCZMST093Row(mItems[position]);
        }
    }
}