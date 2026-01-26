using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Console.Interfaces.Konzola
{
    public interface IKonzola2_GetUzivateleAOpravneni : IKonzola2
    {
        /// <summary>
        /// Vrací seznam veškerých uživatelů a jejich oprávnění
        /// </summary>
        /// <returns></returns>
        Fask.Console.Interfaces.DataSets.Konzola GetUzivateleAOpravneni();
    }
}
