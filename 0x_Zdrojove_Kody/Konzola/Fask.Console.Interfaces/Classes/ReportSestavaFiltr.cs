using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class ReportSestavaFiltr
    {


        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Console.Interfaces.DataSets.Odberatele.CZMST090Row rowOdberatelID { get; set; }
        public string OdberatelID { get; set; }

        /// <summary>
        /// Datum expirace od (pokud je null, tak nezaskrtnuto)
        /// </summary>
        public DateTime? DatumOd { get; set; }

        /// <summary>
        /// Datum expirace do (pokud je null, tak nezaskrtnuto)
        /// </summary>
        public DateTime? DatumDo { get; set; }


        public Fask.Console.Interfaces.DataSets.Servis.CZMST_Servis_OkruhRow rowOkruhID { get; set;}


        /// <summary>
        /// Vybrane ID okruhu.
        /// </summary>
        public string OkruhID { get; set; }



        /// <summary>
        /// Po Mereni
        /// </summary>
        public bool PoMereni { get; set; }

        /// <summary>
        /// Po Mesici
        /// </summary>
        public bool PoMesici { get; set; }

        /// <summary>
        /// Po Roce
        /// </summary>
        public bool PoRoce { get; set; }

    }
}
