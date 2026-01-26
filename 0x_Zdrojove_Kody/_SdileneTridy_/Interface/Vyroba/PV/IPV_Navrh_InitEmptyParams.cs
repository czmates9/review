using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Navrh_InitEmptyParams : IPV
    {
        Fask.Interfaces.DataSets.Vyroba_Planovani InitEmptyParams(Guid G);
    }
}
