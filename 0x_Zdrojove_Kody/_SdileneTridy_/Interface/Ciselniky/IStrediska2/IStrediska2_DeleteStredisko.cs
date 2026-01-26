using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Strediska
{
    public interface IStrediska2_DeleteStredisko : IStrediska2
    {
        /// <summary>
        /// Smaže stredisko.
        /// </summary>
        /// <param name="id">ID strediska.</param>
        /// <returns></returns>
        bool DeleteStredisko(string id);
    }
}
