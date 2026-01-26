using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
   public interface IVydej2_InsertSI : IVydej2
    {
        /// <summary>
        /// vloži řadek...
        /// </summary>
        /// <param name="zdrojRow"> row </param>
        /// <returns></returns>
        int InsertSI(Fask.Interfaces.DataSets.Vydej.CZMST_SIRow SIRow);
    }
}
