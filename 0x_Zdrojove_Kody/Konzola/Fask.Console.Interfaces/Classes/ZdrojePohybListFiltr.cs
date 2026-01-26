using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Classes
{
    public class ZdrojePohybListFiltr
    {
        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Console.Interfaces.DataSets.Uzivatele.CZMSTPWDRow rowUzivatel { get; set; }
        public string UzivatelID { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Console.Interfaces.DataSets.Servis.CZMST_Servis_ZdrojRow rowZdrojID { get; set; }
        public string ZdrojID { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Console.Interfaces.DataSets.Servis.CZMST_Servis_StavRow rowStavID { get; set; }
        public string StavID { get; set; }

        // vybrany zaznam z comboboxu nebo vyplneny popis
        public Fask.Console.Interfaces.DataSets.Servis.CZMST_Servis_CinnostRow rowCinnostID { get; set; }
        public string CinnostID { get; set; }

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

        /// <summary>
        /// Vybrane ID okruhu.
        /// </summary>
        public string OkruhID { get; set; }

        /// <summary>
        /// Vybrane cislo davky
        /// <remarks>je-li null => neni pozadovan vyber</remarks>
        /// </summary>
        public int? Davka { get; set; }

        /// <summary>
        /// Vylouci nedefinovane hodnoty (kde je CinnostValue NULL)
        /// </summary>
        public bool VyloucitNedefinovaneHodnoty { get; set; }

        /// <summary>
        /// Doplni zdroje okruhu, ktere nejsou soucasti vysledku
        /// </summary>
        /// <remarks>Vsechny hodnoty stavove budou NULL</remarks>
        public bool VsechnyZdrojeOkruhu { get; set; }

        /// <summary>
        /// Misto zdroje
        /// </summary>
        public string Misto { get; set; }

        /// <summary>
        /// Typ zdroje
        /// </summary>
        public string Type { get; set; }

    }
}
