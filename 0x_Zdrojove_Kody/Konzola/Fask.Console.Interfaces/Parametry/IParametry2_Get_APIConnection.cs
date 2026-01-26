using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Parametry
{
    public interface IParametry2_Get_APIConnection : IParametry2
    {
        void Get_APIConnection(out string adresa, out string autorizace_DoAPI, out string aliasDB, out bool ishttps, out int timeout);

    }
}

