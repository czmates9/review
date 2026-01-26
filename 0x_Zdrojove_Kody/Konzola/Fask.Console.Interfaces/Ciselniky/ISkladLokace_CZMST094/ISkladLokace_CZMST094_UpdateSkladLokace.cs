using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094
{
    public interface ISkladLokace_CZMST094_UpdateSkladLokace : ISkladLokace_CZMST094
    {
        /// <summary>
        /// Aktualizuje informace o lokaci.
        /// </summary>
        /// <param name="row">Lokace, ktera se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateSkladLokace_CZMST094(Fask.Interfaces.DataSets.SkladLokace.CZMST094Row row);

    }
}


