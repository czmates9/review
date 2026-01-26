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
    public class Row095_WrapperForBinder_WrapperForBinder : Binder
    {

        private Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row mData;

        public Row095_WrapperForBinder_WrapperForBinder(Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row data)
        {
            mData = data;
        }

        public Fask.SQLiteDBs.DataSets.Zbozi.CZMST095Row getData()
        {
            return mData;
        }
    }
}