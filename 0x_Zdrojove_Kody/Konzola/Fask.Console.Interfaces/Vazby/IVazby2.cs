using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vazby
{
    public interface IVazby2 : IMES
    {
        /// <summary>
        /// ITEMNMBR pro vyhledavani vazeb materialu k vyrobku
        /// </summary>
        string ITEMNMBR_Def { get; set; }

        /// <summary>
        /// rowvyrobek_ID_L pro vyhledavani vazeb materialu k vyrobku
        /// </summary>
        string rowvyrobek_ID_L { get; set; }
    }
}
