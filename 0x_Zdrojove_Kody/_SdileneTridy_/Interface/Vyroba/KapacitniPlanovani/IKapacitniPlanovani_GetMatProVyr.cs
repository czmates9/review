using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.KapacitniPlanovani
{
    public interface IKapacitniPlanovani_GetMatProVyr : IKapacitniPlanovani
    {
        Fask.Interfaces.DataSets.Vyroba GetMatProVyr(Fask.Interfaces.Filtry.Vyroba_KapPla_MatProVyr_Filtr filtr);
    }
}
