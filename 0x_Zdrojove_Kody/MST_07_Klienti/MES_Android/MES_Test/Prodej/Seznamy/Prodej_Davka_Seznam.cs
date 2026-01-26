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
    public class Prodej_Davka_Seznam : Java.Lang.Object
    {
        //private Polozka[] polozky;
        public ObservableCollection<Prodej_Davka_Item> mItems = new ObservableCollection<Prodej_Davka_Item>();
        //Random random;

        public Prodej_Davka_Seznam(ObservableCollection<Prodej_Davka_Item> listPolozek) 
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

        public Prodej_Davka_Item this[int i]
        {
            get { return mItems[i]; }
        }

        internal void Move(int fromPosition, int toPosition)
        {
            mItems.Move(fromPosition, toPosition);
        }

        internal void Remove(int position)
        {
            mItems.Remove(mItems.ElementAt(position));
        }
    }
}