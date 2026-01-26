using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Pracovnici
{
    public interface IPracovnici2_GetPracovnikByID : IPracovnici2
    {
        /// <summary>
        /// Vraci pracovnika podle ID.
        /// </summary>
        /// <param name="id">ID pracovnika.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Pracovnici.CZMST096Row GetPracovnikByID(string id);
    }
}
