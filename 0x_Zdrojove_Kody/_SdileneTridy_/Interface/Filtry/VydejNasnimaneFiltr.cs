using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{

    public class VydejNasnimaneFiltr : FilterBase
    {


        /// <summary>
        /// Vybrane cislo davky. (ma byt int)
        /// </summary>
        public string CountEntries { get; set; }

        /// <summary>
        /// čislo položky
        /// </summary>
        public string ITEMNMBR { get; set; }

        /// <summary>
        /// čislo dokladu
        /// </summary>
        public string SOPNUMBE { get; set; }

        public DateTime? DATEDONE_DO { get; set; }
        public DateTime? DATEDONE_OD { get; set; }

        //public Fask.Interfaces.Classes.TimeFilters.TimeVariants? Dateeve_TimeVariant { get; set; }
        public string Dateeve_TimeVariant { get; set; }


        /// <summary>
        /// nazev polozky
        /// </summary>
        public string ITEMDESC { get; set; }



        /// <summary>
        /// nazev polozky
        /// </summary>
        public string ITEMCODE { get; set; }


        /// <summary>
        /// typ dokladu
        /// </summary>
        public string ITEMTYPE { get; set; }
        /// <summary>
        /// ID uzivatele
        /// </summary>
        public List<int> USERID { get; set; }


        /// <summary>
        /// ID terminalu
        /// </summary>
        public List<int> ID_TERMINAL { get; set; }


        /// <summary>
        /// Priznak ITEMTYPE vyhledavai vydej
        /// </summary>
        public bool ITEMTYPE_J { get; set; }

        /// <summary>
        /// Priznak ITEMTYPE vyhledavai inventura
        /// </summary>
        public bool ITEMTYPE_I { get; set; }

        /// <summary>
        /// Priznak ITEMTYPE vyhledavai prijem
        /// </summary>
        public bool ITEMTYPE_P { get; set; }

        /// <summary>
        /// Priznak ITEMTYPE vyhledavai exdpedice
        /// </summary>
        public bool ITEMTYPE_E { get; set; }

        /// <summary>
        /// Priznak ITEMTYPE vyhledavai vyroba
        /// </summary>
        public bool ITEMTYPE_V { get; set; }

        /// <summary>
        /// Priznak ITEMTYPE vyhledavai ostatni
        /// </summary>
        public bool ITEMTYPE_O { get; set; }

    }
}
