using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Prodej
{
   public interface IProdej2_UpdateDI : IProdej2
    {
        /// <summary>
        /// vloži řadek...
        /// </summary>
        /// <param name="zdrojRow"> row </param>
        /// <returns></returns>
        bool UpdateDI(Fask.Interfaces.DataSets.Prodej.CZMST_DIRow DIRow);
    }
}
