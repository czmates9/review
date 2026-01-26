using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_LokaceTypy
{
    public interface ISkladLokace_LokaceTypy2_GetFiltrovaneSkladLokace_LokaceTypy : ISkladLokace_LokaceTypy2
    {
        /// <summary>
        /// Vraci filtrovane typy lokaci.
        /// </summary>
        /// <param name="filtr">filtr zaznamu</param>
        /// <returns></returns>
        Fask.Interfaces.DataSets.SkladLokace GetFiltrovaneSkladLokace_LokaceTypy(Filtry.LokaceTypyListFiltr filtr);
  
    }
}
