using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class StavSkladuListFiltr
    {
        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Console.Interfaces.DataSets.StavSkladu.get_os_msRow rowMaterialID { get; set; }
        public string MaterialID { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Console.Interfaces.DataSets.StavSkladu.get_os_msRow rowMaterialLOCNCODE { get; set; }
        public string MaterialLocncode { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Console.Interfaces.DataSets.StavSkladu.CZMST093Row rowMaterialSKLID { get; set; }
        public string MaterialSKLID { get; set; }

        // uzivatelem vyplnena sarze
        public string MaterialSERLTNUM { get; set; }

        /// <summary>
        /// Datum expirace od (pokud je null, tak nezaskrtnuto)
        /// </summary>
        public DateTime? ExpiraceOd { get; set; }

        /// <summary>
        /// Datum expirace do (pokud je null, tak nezaskrtnuto)
        /// </summary>
        public DateTime? ExpiraceDo { get; set; }

        /// <summary>
        /// Množství je větší než vyplněné
        /// </summary>
        public bool MnozstviVetsi { get; set; }

        /// <summary>
        /// Množství je menší než vyplněné
        /// </summary>
        public bool MnozstviMensi { get; set; }

        /// <summary>
        /// Množství je rovno vyplněnému
        /// </summary>
        public bool MnozstviRovno { get; set; }

        /// <summary>
        /// Zadané množsví
        /// </summary>
        public Decimal? Mnozstvi { get; set; }
    }
}
