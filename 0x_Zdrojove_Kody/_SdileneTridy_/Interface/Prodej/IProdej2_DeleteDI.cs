using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Prodej
{
   public interface IProdej2_DeleteDI : IProdej2
    {
        /// <summary>
        /// Smaže řadek
        /// </summary>
        /// <param name="zdrojRow">DexRowID</param>
        /// <returns></returns>
        bool DeleteDI(int ID);

        /// <summary>
        /// Smaže řadek
        /// </summary>
        /// <param name="zdrojRow">DexRowID</param>
        /// <returns></returns>
        bool DeleteDI(Fask.Interfaces.DataSets.Prodej.CZMST_DIRow DIRow);
    }
}
