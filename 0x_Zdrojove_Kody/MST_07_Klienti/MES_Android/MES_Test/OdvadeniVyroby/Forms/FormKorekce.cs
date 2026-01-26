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
    public class FormKorekce : Form_A
    {
        public override string NameError { get => "FormKorekce"; }
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow { get; internal set; }
        public DateTime? CorretionMinimumDateTime { get; internal set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.CorrectsRow Correction { get; internal set; }
    }
}