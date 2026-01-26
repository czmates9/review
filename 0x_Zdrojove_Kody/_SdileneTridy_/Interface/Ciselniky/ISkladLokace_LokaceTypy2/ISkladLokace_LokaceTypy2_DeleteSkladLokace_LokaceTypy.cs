using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy
{
    public interface ISkladLokace_LokaceTypy2_DeleteSkladLokace_LokaceTypy : ISkladLokace_LokaceTypy2
    {
        /// <summary>
        /// Smaže typ lokace.
        /// </summary>
        /// <param name="skl_id">Typ lokace.</param>
        /// <returns></returns>
        bool DeleteSkladLokace_LokaceTypy(string type);

    }
}
