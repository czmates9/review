using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094
{
    public interface ISkladLokace_CZMST094_GetSkladLokaceByLOCNCODE : ISkladLokace_CZMST094
    {

        /// <summary>
        /// Vraci mapu skladu.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.SkladLokace.CZMST094Row GetSkladLokaceByLOCNCODE_CZMST094(string LOCNCODE);
    }
}

