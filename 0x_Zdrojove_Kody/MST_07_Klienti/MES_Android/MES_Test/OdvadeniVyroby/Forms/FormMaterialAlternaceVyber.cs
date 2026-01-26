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
    public class FormMaterialAlternaceVyber : Form_A
    {
        public override string NameError { get => "FormMaterialAlternaceVyber"; }

        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow[] Alternativy;
        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_Vyroba_TPRow AlternativaSelected;
        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095DataTable dt_Material;
        public Fask.SQLiteDBs.DataSets.Vyroba.FASK_CONS_095Row MaterialSelected;
    }
}