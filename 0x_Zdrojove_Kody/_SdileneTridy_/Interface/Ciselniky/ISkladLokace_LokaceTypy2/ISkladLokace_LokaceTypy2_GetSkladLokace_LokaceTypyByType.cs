using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy
{
    public interface ISkladLokace_LokaceTypy2_GetSkladLokace_LokaceTypyByType : ISkladLokace_LokaceTypy2
    {
        /// <summary>
        /// Vraci typ lokace podle id.
        /// </summary>
        /// <param name="type">Typ lokace.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.SkladLokace.CZMST_SkladLokace_LokaceTypyRow GetSkladLokace_LokaceTypyByType(string type);

    }
}
