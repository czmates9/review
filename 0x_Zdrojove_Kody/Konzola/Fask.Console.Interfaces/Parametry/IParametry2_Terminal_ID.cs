using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Parametry
{
    public interface IParametry2_Terminal_ID
    {
        /// <summary>
        /// Connection string pro pripojeni k DB.
        /// </summary>
        string Terminal_ID { get; set; }
    }
}
