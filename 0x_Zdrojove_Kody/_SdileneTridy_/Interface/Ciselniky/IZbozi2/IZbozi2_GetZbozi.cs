using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Zbozi
{
    public interface IZbozi2_GetZbozi : IZbozi2
    {
        /// <summary>
        /// Vraci seznam zbozi.
        /// </summary>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Zbozi GetZbozi();
    }
}
