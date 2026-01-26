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
    public class FormKorekceCasu : Form_A
    {
        public override string NameError { get => "FormKorekceCasu"; }
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow;
    }
}