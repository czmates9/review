using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Localization
{
    public class Globals
    {
        public static bool LokalizacePovolit { get; set; }
        public static bool LokalizaceVlastniPovolit { get; set; }
        public static string LocalizationDir { get; set; }
        private static Fask.Localization.LocalizationSupport.LocalType _lokalizaceZvolena = LocalizationSupport.LocalType.cs;
        public static Fask.Localization.LocalizationSupport.LocalType LokalizaceZvolena
        {
            get { return _lokalizaceZvolena; }
            set { _lokalizaceZvolena = value; }
        }
    }
}
