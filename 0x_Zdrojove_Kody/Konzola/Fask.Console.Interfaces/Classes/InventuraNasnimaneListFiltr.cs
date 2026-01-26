using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class InventuraNasnimaneListFiltr
    {
        public InventuraNasnimaneListFiltr()
        { }

        public override string ToString()
        {
            return string.IsNullOrEmpty(NazevFiltru) ? string.Empty : NazevFiltru.Trim();
        }

        /// <summary>
        /// Název filtru, který se bude zobrazovat.
        /// </summary>
        public string NazevFiltru { get; set; }

        /// <summary>
        /// Urceni podle ceho se ma pocitat aktualni stav filtru (vyuziva se pro ulozeni filtru).
        /// </summary>
        public VYPOCET_STAVU FiltrStav { get; set; }

        /// <summary>
        /// Vybrane cislo davky.
        /// </summary>
        public int CountEntries { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis
        //public Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow rowUzivatel { get; set; }
        public string rowUzivatel { get; set; }
        
        /// <summary>
        /// Vyplnene ID uzivatele nebo nazev)
        /// </summary>
        public string UzivatelID { get; set; }

        /// <summary>
        /// Vyplnene ID materialu nebo nazev materialu
        /// </summary>
        public string MaterialID { get; set; }

        /// <summary>
        /// Vyplnena lokace
        /// </summary>
        public string MaterialLocncode { get; set; }

        /// <summary>
        /// Vybrany zaznam z comboboxu nebo vyplneny popis
        /// </summary>
        //public Fask.Console.Interfaces.DataSets.Sklady.CZMST093Row rowMaterialSKLID { get; set; }
        public string rowMaterialSKLID { get; set; }
        
        /// <summary>
        /// Vyplnene ID skladu
        /// </summary>
        public string MaterialSKLID { get; set; }
    }
}
