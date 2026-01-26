using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vazby
{
    public interface IVazby2_GetFiltrovanyVazbyMaterialy : IVazby2
    {
        /// <summary>
        /// Meotoda ktera vyhledava vazby materialy
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Vyroba GetFiltrovanyVazbyMaterialy(Fask.Interfaces.Filtry.VazbyMaterialyFiltr filtr);
    }
}
