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

namespace MES_Android._Translations
{
    class Meny
    {
        public static string GetSymbol(string mena_id)
        {
            switch (mena_id)
            {
                case "EUR": return "€";
                case "CZK": return "Kč";
                default: return "?";
            }
        }
    }
}