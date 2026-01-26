using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Informations
{
    public interface IInformations2_MnozstviNaSklade : IInformations2
    {
        float MnozstviNaSklade(string ItemNumber);
    }
}
