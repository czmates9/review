using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.ProductionSources
{
    public interface IProductionSources : IMES
    {
        /// <summary>
        /// GUID_Production pro vyhledavani vazeb materialů k vyrobku
        /// </summary>
        Guid? GUID_Production { get; set; }
    }
}
