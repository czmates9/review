using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LVS
{
    public interface ISkladLokace_LVS2_UpdateSkladLokace_LokaceVariantySortiment : ISkladLokace_LVS2
    {
        /// <summary>
        /// Aktualizuje variantu zbozi.
        /// </summary>
        /// <param name="row">varianta, ktera se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateSkladLokace_LokaceVariantySortiment(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow row, string locncodeold);

    }
}
