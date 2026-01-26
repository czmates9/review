using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Informations
{
    public interface IInformations2_Command1 : IInformations2
    {
        DataSet Command1(string param1, string param2);
    }
}
