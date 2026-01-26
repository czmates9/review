using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094
{
    public interface ISkladLokace_CZMST094_DeleteSkladLokace : ISkladLokace_CZMST094
    {
        /// <summary>
        /// Smaže lokaci.
        /// </summary>
        /// <param name="skl_id">ID skladu.</param>
        /// <param name="locncode">ID lokace.</param>
        /// <returns></returns>
        bool DeleteSkladLokace_CZMST094(string skl_id, string locncode);

    }
}
