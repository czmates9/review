using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class SkladPohybListFiltr : FilterBase
    {


        /// <summary>
        /// Typ zobrazeni dat (historie, aktualni, historie a aktualni)
        /// </summary>
        public Fask.Console.Interfaces.Classes.ZOBRAZENI_DAT ZobrazeniDat { get; set; }

        /// <summary>
        /// Moznosti vypoctu aktualniho stavu.
        /// </summary>
        public Fask.Console.Interfaces.Classes.VYPOCET_STAVU VypocetStavu { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis
        //public Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow rowUzivatel { get; set; }
        public string rowUzivatel { get; set; }

        /// <summary>
        /// Vyplnene ID uzivatele nebo nazev)
        /// </summary>
        public string UzivatelID { get; set; }

        /// <summary>
        /// Vyplnene ID materialu
        /// </summary>
        public string MaterialID { get; set; }

        /// <summary>
        /// Vyplneny ITEMCODE
        /// </summary>
        public string MaterialITEMCODE { get; set; }

        /// <summary>
        /// Vybrany zaznam lokace z leveho splitteru
        /// </summary>
        public string MaterialLocncode { get; set; }

        /// <summary>
        /// Text, ktery je vyplnen v comboboxu skl_id
        /// </summary>
        public string cbTextMaterialLocncode { get; set; }

        /// <summary>
        /// Typ lokace
        /// </summary>
        public string LocncodeType { get; set; }

        /// <summary>
        /// Vybrany typ pohybu
        /// </summary>
        //public Fask.Console.Interfaces.DataSets.SkladPohyb.CZMST_Sklad_PohybRow rowType { get; set; }
        public string rowType { get; set; }
        
        /// <summary>
        /// Typ pohybu
        /// </summary>
        public string PohybType { get; set; }

        /// <summary>
        /// Vybrany zaznam z comboboxu
        /// </summary>
        //public Fask.Console.Interfaces.DataSets.Sklady.CZMST093Row rowMaterialSKLID { get; set; }
        public string cbSelectedMaterialSKLID { get; set; }

        /// <summary>
        /// Text, ktery je vyplnen v comboboxu skl_id
        /// </summary>
        public string cbTextMaterialSKLID { get; set; }

        /// <summary>
        /// Vybrany zaznam skladu z leveho splitteru
        /// </summary>
        public string MaterialSKLID { get; set; }

        /// <summary>
        /// uzivatelem vyplnena sarze
        /// </summary>
        public string MaterialSERLTNUM { get; set; }

        /// <summary>
        /// uzivatelem vyplnena sarze
        /// </summary>
        public string MaterialPracID { get; set; }

        /// <summary>
        /// uzivatelem vyplnene cislo dokumentu (ponumber, sopnumbe, document_number, ...)
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Datum expirace od (pokud je null, tak nezaskrtnuto)
        /// </summary>
        public DateTime? DatumOd { get; set; }

        /// <summary>
        /// Datum expirace do (pokud je null, tak nezaskrtnuto)
        /// </summary>
        public DateTime? DatumDo { get; set; }

        /// <summary>
        /// aktivni filtr na lokaci, sklad
        /// </summary>
        public string AktivniFiltr { get; set; }
    }
}
