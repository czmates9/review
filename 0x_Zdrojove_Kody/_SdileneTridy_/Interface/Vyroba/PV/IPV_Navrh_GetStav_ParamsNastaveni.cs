using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Navrh_GetStav_ParamsNastaveni : IPV
    {
        Fask.Interfaces.DataSets.Vyroba_Planovani GetStav_ParamsNastaveni(Guid G);
    }
}
