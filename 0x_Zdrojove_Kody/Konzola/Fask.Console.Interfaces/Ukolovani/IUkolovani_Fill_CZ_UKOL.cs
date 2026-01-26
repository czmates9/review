using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ukolovani
{
    public interface IUkolovani_Fill_CZ_UKOL : IUkolovani
    {

        bool Fill_CZ_UKOL(Fask.Interfaces.DataSets.Ukolovani ds);

    }
}
