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
    public class Prodej_TypDokladu_Seznam : Java.Lang.Object
    {
        //private Polozka[] polozky;
        //public ObservableCollection<Prodej_Davka_Item> mItems = new ObservableCollection<Prodej_Davka_Item>();
        //Random random;
        public Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092DataTable mItems = new Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092DataTable();


        public Prodej_TypDokladu_Seznam(Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092DataTable listPolozek)
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

        public Fask.SQLiteDBs.DataSets.TypDokladu.CZMST092Row this[int i]
        {
            get { return mItems[i]; }
        }

        internal void Move(int fromPosition, int toPosition)
        {
            //mItems.Move(fromPosition, toPosition);
            mItems.Rows.InsertAt(mItems[fromPosition], toPosition);
            mItems.RemoveCZMST092Row(mItems[fromPosition]);
        }

        internal void Remove(int position)
        {
            //mItems.Remove(mItems.ElementAt(position));
            mItems.RemoveCZMST092Row(mItems[position]);
        }
    }
}