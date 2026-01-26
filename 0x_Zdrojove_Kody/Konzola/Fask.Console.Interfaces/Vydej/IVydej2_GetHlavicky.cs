using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vydej
{
    public interface IVydej2_GetHlavicky : IVydej2
    {
        /// <summary>
        /// Vraci seznam vsech hlavicek vydeje.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Vydej GetHlavicky();
    }

}
