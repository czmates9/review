using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class InventuraPredlohaListFiltr : FilterBase
    {


        /// <summary>
        /// Vybrane cislo davky.
        /// </summary>
        public string CountEntries { get; set; }

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
        //public Fask.Interfaces.DataSets.Sklady.CZMST093Row rowMaterialSKLID { get; set; }
        public string rowMaterialSKLID { get; set; }
        
        /// <summary>
        /// Vyplnene ID skladu
        /// </summary>
        public string MaterialSKLID { get; set; }

        /// <summary>
        /// Zobrazit pouze nenasnimane polozky.
        /// </summary>
        public bool ZobrazitPouzeNenasnimane { get; set; }


        public bool ZobrazitAlternativnyCaroveKody { get; set; }

    }
}
