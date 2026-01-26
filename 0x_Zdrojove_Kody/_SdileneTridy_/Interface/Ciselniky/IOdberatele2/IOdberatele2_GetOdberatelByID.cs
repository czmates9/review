using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Odberatele
{
    public interface IOdberatele2_GetOdberatelByID : IOdberatele2
    {
        /// <summary>
        /// Vraci odberatele podle ID.
        /// </summary>
        /// <param name="id">ID odberatele.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Odberatele.CZMST090Row GetOdberatelByID(string id);
    }
}
