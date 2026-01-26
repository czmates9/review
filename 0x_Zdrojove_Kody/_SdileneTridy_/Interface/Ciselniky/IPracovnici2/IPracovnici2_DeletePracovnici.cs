using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Pracovnici
{
    public interface IPracovnici2_DeletePracovnici : IPracovnici2
    {
        /// <summary>
        /// Smaže Pracovnika.
        /// </summary>
        /// <param name="id">ID pracovnika.</param>
        /// <returns></returns>
        bool DeletePracovnici(string id);
    }
}
