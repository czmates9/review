using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LVS
{
    public interface ISkladLokace_LVS2_InsertSkladLokace_LokaceVariantySortiment_Row : ISkladLokace_LVS2
    {
        /// <summary>
        /// Vloží do DB nový záznam varianty zbozi.
        /// </summary>
        /// <param name="row">varianta zbozi, která se má vložit.</param>
        /// <returns>True - vše v pořádku, False - chyba</returns>
        bool InsertSkladLokace_LokaceVariantySortiment(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow row);

    }
}
