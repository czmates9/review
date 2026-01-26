using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class VydejDavkyFiltr : FilterBase
    {


        /// <summary>
        /// Vybrane cislo davky. (ma byt int)
        /// </summary>
        public string CountEntries { get; set; }

        /// <summary>
        /// sopnumbe
        /// </summary>
        public string Sopnumbe { get; set; }

        /// <summary>
        /// Rozpracovano (ma byt int)
        /// </summary>
        public string Rozpracovano { get; set; }

        /// <summary>
        /// Priority (ma byt byte)
        /// </summary>
        public string Priority { get; set; }


        /// <summary>
        /// čislo položky
        /// </summary>
        public string ITEMNMBR { get; set; }




        /// <summary>
        /// Zobrazeni  ulovnene davky CZ doslo = 0
        /// </summary>
        public bool UvolneneDavky { get; set; }

        /// <summary>
        /// Zobrazeni  NEulovnene davky CZ doslo = 255
        /// </summary>
        public bool NEUvolneneDavky { get; set; }

        /// <summary>
        /// Zobrazeni stažene davky CZ doslo = 1-99
        /// </summary>
        public bool StazeneDavky { get; set; }

        /// <summary>
        /// Zobrazeni spracovane davky CZ doslo = 101-199
        /// </summary>
        public bool SpracovaneDavky { get; set; }


        /// <summary>
        /// Zobrazeni mrtvé davky CZ doslo = 201
        /// </summary>
        public bool MrtveDavky { get; set; }


        /// <summary>
        /// Pouze kladne mnozstvy
        /// </summary>
        public bool PouzeKladneMnozstvy { get; set; }

        /// <summary>
        /// Typ polozky
        /// </summary>
        public string TypPolozky { get; set; }

        /// <summary>
        /// Priznak ridici stav davky
        /// </summary>
        public string PriznakDavky { get; set; }


        /// <summary>
        /// Priznak polozky
        /// </summary>
        public bool PriznakPolozky_vsechny { get; set; }

        /// <summary>
        /// Priznak polozky
        /// </summary>
        public bool PriznakPolozky_vykryte { get; set; }

        /// <summary>
        /// Priznak polozky
        /// </summary>
        public bool PriznakPolozky_nevykryte { get; set; }

        /// <summary>
        /// Priznak polozky
        /// </summary>
        public bool PriznakPolozky_neplnene { get; set; }


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
