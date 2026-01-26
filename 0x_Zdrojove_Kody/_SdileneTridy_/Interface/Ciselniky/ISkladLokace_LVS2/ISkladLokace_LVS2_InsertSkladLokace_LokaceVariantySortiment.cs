using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LVS
{
    public interface ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment : ISkladLokace_LVS2
    {
        /// <summary>
        /// Vloží do DB nový záznam varianty zbozi.
        /// </summary>
        /// <param name="TermID">ID Terminalu</param>
        /// <param name="row">varianta zbozi, která se má vložit.</param>
        /// <param name="type">Typ lokace</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        Fask.Interfaces.Classes.INVENTURA_PLNENI_VARIANT_STATUS InsertSkladLokace_LokaceVariantySortiment(byte TermID, Fask.Interfaces.DataSets.Inventura.CZMST_I4Row row, string type);

    }
}
