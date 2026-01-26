using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.KapacitniPlanovani
{
    public interface IKapacitniPlanovani_Get_Vypocet : IKapacitniPlanovani
    {
        Fask.Interfaces.DataSets.Vyroba Get_Vypocet(Fask.Interfaces.DataSets.Vyroba ds);
    }
}
