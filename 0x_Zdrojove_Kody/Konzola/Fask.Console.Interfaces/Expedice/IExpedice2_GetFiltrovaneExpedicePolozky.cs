using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Expedice
{
    public interface IExpedice2_GetFiltrovaneExpedicePolozky : IExpedice2
    {
        /// <summary>
        /// Vraci hlavicky podle zadaneho filtru
        /// </summary>
        /// <param name="filtr"></param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.Expedice GetFiltrovaneExpedicePolozky(Fask.Interfaces.Filtry.ExpediceFormDodaciListyListFiltr filtr);
    }
}
