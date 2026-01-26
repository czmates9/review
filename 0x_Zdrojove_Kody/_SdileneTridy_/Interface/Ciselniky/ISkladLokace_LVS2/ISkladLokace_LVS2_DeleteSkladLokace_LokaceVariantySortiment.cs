using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LVS
{
    public interface ISkladLokace_LVS2_DeleteSkladLokace_LokaceVariantySortiment : ISkladLokace_LVS2
    {
        /// <summary>
        /// Smaže variantu zbozi.
        /// </summary>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="locncode">ID lokace.</param>
        /// <param name="itemnmbr">ID polozky.</param>
        /// <returns></returns>
        bool DeleteSkladLokace_LokaceVariantySortiment(string skl_id, string locncode, string itemnmbr);

    }
}
