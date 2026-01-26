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
    public class FormOperacePotvrzeni : Form_A
    {
        public override string NameError { get => "FormOperacePotvrzeni"; }
        public decimal PocetOdvedeno { get; internal set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.LoginsRow Pracovnik { get; internal set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.MachinesRow Machine { get; internal set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.CZPRO_VPPRow VPP { get; internal set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.ProductionRow ProductionRow { get; internal set; }
        public Fask.SQLiteDBs.DataSets.Vyroba.Production_SourcesDataTable ProductionSDT { get; internal set; }
    }
}