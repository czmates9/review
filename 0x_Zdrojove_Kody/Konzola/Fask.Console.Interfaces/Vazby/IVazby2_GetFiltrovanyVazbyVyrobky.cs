using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vazby
{
    public interface IVazby2_GetFiltrovanyVazbyVyrobky : IVazby2
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVazbyVyrobky(Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr);
    }
}
