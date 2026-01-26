using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
   public interface IVydej2_UpdateSI : IVydej2
    {
        /// <summary>
        /// vloži řadek...
        /// </summary>
        /// <param name="zdrojRow"> row </param>
        /// <returns></returns>
        bool UpdateSI(Fask.Interfaces.DataSets.Vydej.CZMST_SIRow SIRow);

   
    }
}
