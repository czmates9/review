using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_UpdateSE_QTY_ByDEXROWID : IVydej2
    {
        /// <summary>
        /// vloži řadek...
        /// </summary>
        /// <param name="zdrojRow"> row </param>
        /// <returns></returns>
        bool UpdateSE_QTY_ByDEXROWID(decimal QTY, int DEXROWID);
    }
}
