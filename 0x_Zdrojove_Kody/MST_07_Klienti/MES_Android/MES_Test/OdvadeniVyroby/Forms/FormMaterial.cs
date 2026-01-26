using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MES_Android.OdvadeniVyroby.Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES_Android.OdvadeniVyroby
{
    public class FormMaterial : Form_A
    {
        public override string NameError { get => "FormMaterial"; }
        internal decimal pocetodvedeno;
        internal ShowTypes types;
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow { get; internal set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable ProductionSDT { get; internal set; }

    }
}