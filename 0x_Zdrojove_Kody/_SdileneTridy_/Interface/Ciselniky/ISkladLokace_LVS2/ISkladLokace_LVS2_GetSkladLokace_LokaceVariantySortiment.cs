using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LVS
{
    public interface ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortiment : ISkladLokace_LVS2
    {
        /// <summary>
        /// Vraci varianty zbozi.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.SkladLokace GetSkladLokace_LokaceVariantySortiment();
    }
}
