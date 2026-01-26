using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LVS
{
    public interface ISkladLokace_LVS2_GetFiltrovanySkladLokace_LokaceVariantySortiment : ISkladLokace_LVS2
    {
        /// <summary>
        /// Vraci filtrovane varianty sortimentu.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.SkladLokace GetFiltrovanySkladLokace_LokaceVariantySortiment(Filtry.LokaceVariantySortimentListFiltr filtr);
   
    }
}
