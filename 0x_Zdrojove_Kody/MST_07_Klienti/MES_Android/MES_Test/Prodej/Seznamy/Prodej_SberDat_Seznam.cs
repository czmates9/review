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
    public class Prodej_SberDat_Seznam : Java.Lang.Object
    {
        public Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable mItems = new Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable();


        public Prodej_SberDat_Seznam(Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIDataTable listPolozek)
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

        public Fask.SQLiteDBs.DataSets.Prodej.CZMST_DIRow this[int i]
        {
            get { return mItems[i]; }
        }

        internal void Move(int fromPosition, int toPosition)
        {
            mItems.Rows.InsertAt(mItems[fromPosition], toPosition);
            mItems.RemoveCZMST_DIRow(mItems[fromPosition]);
        }

        internal void Remove(int position)
        {
            mItems.RemoveCZMST_DIRow(mItems[position]);
        }
    }
}