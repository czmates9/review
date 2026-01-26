using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Zbozi
{
    public interface IZbozi2_DeleteZbozi : IZbozi2
    {
        /// <summary>
        /// Smaže zbozi.
        /// </summary>
        /// <param name="id">ID zbozi.</param>
        /// <returns></returns>
        bool DeleteZbozi(int id_ZBOZI, int? ID_Params);
    }
}
