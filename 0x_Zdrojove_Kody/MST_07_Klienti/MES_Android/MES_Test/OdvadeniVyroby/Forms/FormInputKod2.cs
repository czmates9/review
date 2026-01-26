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
    public class FormInputKod2 : Form_A
    {
        public override string NameError { get => "FormInputKod2"; }
        public string Kod { get; internal set; }

        public string Text;
        public string Nazev;
        public string Mnozstvi;
        public string Sklad;
        public string Lokace;
        public string Material;
    }
}