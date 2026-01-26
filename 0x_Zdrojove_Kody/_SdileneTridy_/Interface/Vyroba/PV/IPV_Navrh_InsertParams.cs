using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Navrh_InsertParams : IPV
    {
        void InsertParams(Fask.Interfaces.DataSets.Vyroba_Planovani dtParams);
    }
}
