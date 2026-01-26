using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W._Translations
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
