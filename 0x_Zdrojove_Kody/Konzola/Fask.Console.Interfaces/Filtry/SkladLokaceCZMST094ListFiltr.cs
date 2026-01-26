using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Filtry
{
    public class SkladLokaceCZMST094ListFiltr : FilterBase
    {


        /// <summary>
        /// lokace
        /// </summary>
        public string locncode { get; set; }

        /// <summary>
        /// sklad
        /// </summary>
        public string skl_id { get; set; }
    }
}
