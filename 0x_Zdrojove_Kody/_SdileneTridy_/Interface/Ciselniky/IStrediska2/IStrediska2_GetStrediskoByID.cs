using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Strediska
{
    public interface IStrediska2_GetStrediskoByID : IStrediska2
    {
        /// <summary>
        /// Vraci stredisko podle ID.
        /// </summary>
        /// <param name="id">ID strediska.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Strediska.CZMST091Row GetStrediskoByID(string id);
    }
}
