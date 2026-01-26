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
    public class Prodej_VyberMaterial_Seznam : Java.Lang.Object
    {

        public MES_Android.ProdejService.Location.CZMST_SkladLokace_StavDataTable mItems = new MES_Android.ProdejService.Location.CZMST_SkladLokace_StavDataTable();


        public Prodej_VyberMaterial_Seznam(MES_Android.ProdejService.Location.CZMST_SkladLokace_StavDataTable listPolozek)
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

        public MES_Android.ProdejService.Location.CZMST_SkladLokace_StavRow this[int i]
        {
            get { return mItems[i]; }
        }

        internal void Move(int fromPosition, int toPosition)
        {
            mItems.Rows.InsertAt(mItems[fromPosition], toPosition);
            mItems.RemoveCZMST_SkladLokace_StavRow(mItems[fromPosition]);
        }

        internal void Remove(int position)
        {
            mItems.RemoveCZMST_SkladLokace_StavRow(mItems[position]);
        }
    }
}