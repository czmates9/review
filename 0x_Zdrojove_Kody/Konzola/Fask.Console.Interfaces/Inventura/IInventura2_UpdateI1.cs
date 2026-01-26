using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Interfaces.Classes;

namespace Fask.Interfaces.Inventura
{
    public interface IInventura2_UpdateI1 : IInventura2
    {
        /// <summary>
        /// vloži řadek...
        /// </summary>
        /// <param name="zdrojRow"> row </param>
        /// <returns></returns>
        bool UpdateI1(Fask.Interfaces.DataSets.Inventura.CZMST_I1_PredlohaRow I1Row);
    }
}
