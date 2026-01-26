using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy
{
    public interface ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypy : ISkladLokace_LokaceTypy2
    {
        /// <summary>
        /// Vraci typy lokaci.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.SkladLokace GetSkladLokace_LokaceTypy();
    }
}
