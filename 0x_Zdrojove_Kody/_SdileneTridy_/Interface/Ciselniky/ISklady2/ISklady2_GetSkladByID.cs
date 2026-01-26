using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Sklady
{
    public interface ISklady2_GetSkladByID : ISklady2
    {
        /// <summary>
        /// Vraci sklad podle ID.
        /// </summary>
        /// <param name="id">ID skladu.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Sklady.CZMST093Row GetSkladByID(string id);
    }
}
