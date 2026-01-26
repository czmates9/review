using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Ciselniky.ISkladLokace_CZMST094
{
    public interface ISkladLokace_CZMST094_GetFiltrovaneSkladLokace : ISkladLokace_CZMST094
    {

        /// <summary>
        /// Vraci mapu skladu.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.SkladLokace GetFiltrovaneSkladLokace_CZMST094(Fask.Interfaces.Filtry.SkladLokaceCZMST094ListFiltr filtr);
    }
}
