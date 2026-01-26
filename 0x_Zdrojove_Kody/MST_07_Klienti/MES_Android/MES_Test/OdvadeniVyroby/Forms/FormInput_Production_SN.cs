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
    public class FormInput_Production_SN : Form_A
    {
        public override string NameError { get => "FormInput_Production_SN"; }
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow { get; internal set; }
        public decimal QTY_SN { get; internal set; }
    }
}