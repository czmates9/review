using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Ciselniky
{
    public interface IUzivatele2_DeleteUzivatel : IUzivatele2
    {
        /// <summary>
        /// Smaže uzivatele.
        /// </summary>
        /// <param name="id">ID uzivatele.</param>
        /// <returns></returns>
        bool DeleteUzivatel(string id);

    }
}
