using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy
{
    public interface ISkladLokace_LokaceTypy2_UpdateSkladLokace_LokaceTypy : ISkladLokace_LokaceTypy2
    {
        /// <summary>
        /// Aktualizuje typ lokace.
        /// </summary>
        /// <param name="row">Lokace, ktera se bude aktualizovat</param>
        /// <returns></returns>
        bool UpdateSkladLokace_LokaceTypy(Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow row);

    }
}
