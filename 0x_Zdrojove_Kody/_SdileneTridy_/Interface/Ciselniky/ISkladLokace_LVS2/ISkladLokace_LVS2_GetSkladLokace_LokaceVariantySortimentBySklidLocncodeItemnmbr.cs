using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LVS
{
    public interface ISkladLokace_LVS2_GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr : ISkladLokace_LVS2
    {
        /// <summary>
        /// Vraci varianty zbozi id.
        /// </summary>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="locncode">ID lokace.</param>
        /// <param name="itemnmbr">ID polozky.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceVariantySortimentRow GetSkladLokace_LokaceVariantySortimentBySklidLocncodeItemnmbr(string skl_id, string locncode, string itemnmbr);

    }
}
