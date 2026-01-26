using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.TypyDokladu
{
    public interface ITypyDokladu2_Delete : ITypyDokladu2
    {
        /// <summary>
        /// Smaže zbozi.
        /// </summary>
        /// <param name="id">ID zbozi.</param>
        /// <returns></returns>
        bool TypyDokladu_Delete(int id);
    }
}
