using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Sklady
{
    public interface ISklady2_DeleteSklad : ISklady2
    {
        /// <summary>
        /// Smaže sklad.
        /// </summary>
        /// <param name="id">ID skladu.</param>
        /// <returns></returns>
        bool DeleteSklad(string id);
    }
}
