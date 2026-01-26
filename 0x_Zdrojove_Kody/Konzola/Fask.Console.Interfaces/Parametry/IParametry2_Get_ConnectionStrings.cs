using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Parametry
{
    public interface IParametry2_Get_ConnectionStrings : IParametry2
    {
        void Get_ConnectionStrings(out string DB_FASK, out string DB_POHODA);
    }
}
