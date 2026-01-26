using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_ImportPV : IPV
    {

         string ImportPV();

        List<Tuple<string, string, bool>> GetPVTableInfo();
    }
}
