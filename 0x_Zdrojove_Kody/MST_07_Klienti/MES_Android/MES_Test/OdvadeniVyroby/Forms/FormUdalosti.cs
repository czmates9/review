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
    public class FormUdalosti : Form_A
    {
        public override string NameError { get => "FormUdalosti"; }
        public string Text { get; internal set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.StatusTypesRow SelectedStatusTypesRow { get; internal set; }
    }
}