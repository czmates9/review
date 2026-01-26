using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Zbozi
{
    public interface IZbozi2_GetZboziByID : IZbozi2
    {
        /// <summary>
        /// Vraci zbozi podle ID.
        /// </summary>
        /// <param name="id">ID zbozi.</param>
        /// <returns>Pokud nalezeno, vraci zaznam, jinak null</returns>
        Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow GetZboziByID(string id);
    }
}