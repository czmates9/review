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

namespace MES_Android.Classes
{
    public class SkladCilWrapperForBinder : Binder
    {

        private Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row mData;

        public SkladCilWrapperForBinder(Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row data)
        {
            mData = data;
        }

        public Fask.SQLiteDBs.DataSets.Sklady.CZMST093Row getData()
        {
            return mData;
        }
    }
}