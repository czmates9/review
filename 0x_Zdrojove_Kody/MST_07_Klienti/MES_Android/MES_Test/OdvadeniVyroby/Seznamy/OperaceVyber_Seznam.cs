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

namespace MES_Android.OdvadeniVyroby
{
    public class OperaceVyber_Seznam : Java.Lang.Object
    {

        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable mItems = new Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable();


        public OperaceVyber_Seznam(Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPDataTable listPolozek)
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

        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow this[int i]
        {
            get { return mItems[i]; }
        }

        internal void Move(int fromPosition, int toPosition)
        {
            mItems.Rows.InsertAt(mItems[fromPosition], toPosition);
            mItems.RemoveCZPRO_VPPRow(mItems[fromPosition]);
        }

        internal void Remove(int position)
        {
            mItems.RemoveCZPRO_VPPRow(mItems[position]);
        }
    }
}