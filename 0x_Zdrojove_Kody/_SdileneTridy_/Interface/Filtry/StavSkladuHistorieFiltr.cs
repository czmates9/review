using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    /// <summary>
    /// MD Let
    /// </summary>
    public class StavSkladuHistorieFiltr
    {
        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Interfaces.DataSets.StavSkladu.CZMSTPWDRow rowUzivatel { get; set; }
        public string UzivatelID { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Interfaces.DataSets.StavSkladu.get_os_msRow rowMaterialID { get; set; }
        public string MaterialID { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Interfaces.DataSets.StavSkladu.get_os_msRow rowMaterialLOCNCODESRC { get; set; }
        public string MaterialLocncodeSRC { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Interfaces.DataSets.StavSkladu.get_os_msRow rowMaterialLOCNCODEDST { get; set; }
        public string MaterialLocncodeDST { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis zdrojoveho skladu
        public Fask.Interfaces.DataSets.StavSkladu.CZMST093Row rowMaterialSKLIDSRC { get; set; }
        public string MaterialSKLIDSRC { get; set; }

        /// <summary>
        /// Pokud je True, vyhledava se podle presne zadaneho retezce (i kdyz je prazdny)
        /// </summary>
        public bool MaterialSKLIDDSTPresnaShoda { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis zdrojoveho skladu
        public Fask.Interfaces.DataSets.StavSkladu.CZMST093Row rowMaterialSKLIDDST { get; set; }
        public string MaterialSKLIDDST { get; set; }

        /// <summary>
        /// uzivatelem vyplnena sarze
        /// </summary>
        public string MaterialSERLTNUM { get; set; }

        /// <summary>
        /// Datum expirace od (pokud je null, tak nezaskrtnuto)
        /// </summary>
        public DateTime? DatumOd { get; set; }

        /// <summary>
        /// Datum expirace do (pokud je null, tak nezaskrtnuto)
        /// </summary>
        public DateTime? DatumDo { get; set; }

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
