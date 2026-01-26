using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Expedice
{
   public interface IExpedice2_GetBufferPolozky : IExpedice2
    {
        /// <summary>
        /// Vraci seznam vsech hlavicek vydeje.
        /// </summary>
        /// <returns></returns>
       Fask.Interfaces.DataSets.Expedice GetBufferPolozky();
    }
}
