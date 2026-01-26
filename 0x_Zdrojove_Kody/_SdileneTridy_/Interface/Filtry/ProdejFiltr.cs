using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class ProdejFiltr : FilterBase
    {


        /// <summary>
        /// Vybrane cislo davky. (ma byt int)
        /// </summary>
        public string CountEntries { get; set; }

        /// <summary>
        /// čislo položky
        /// </summary>
        public string ITEMNMBR { get; set; }

        public DateTime? DATEDONE_DO { get; set; }
        public DateTime? DATEDONE_OD { get; set; }

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants? Dateeve_TimeVariant { get; set; }
        public string Dateeve_TimeVariant { get; set; }


        /// <summary>
        /// nazev polozky
        /// </summary>
        public string ITEMDESC { get; set; }
        /// <summary>
        /// typ dokladu
        /// </summary>
        public string DOC_ID { get; set; }



        /// <summary>
        /// typ dokladu
        /// </summary>
        public string DOC_ID2 { get; set; }/// <summary>
       
        /// typ dokladu
        /// </summary>
        public string ITEMCODE { get; set; }


        /// <summary>
        /// ID uzivatele
        /// </summary>
        public List<int> USERID { get; set; }


        /// <summary>
        /// ID terminalu
        /// </summary>
        public List<int> ID_TERMINAL { get; set; }

    }
}
