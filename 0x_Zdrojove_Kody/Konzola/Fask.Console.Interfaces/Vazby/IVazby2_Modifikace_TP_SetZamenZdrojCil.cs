using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vazby
{
    public interface IVazby2_Modifikace_TP_SetZamenZdrojCil : IVazby2
    {
        Fask.Interfaces.Classes.StatusInfo SetZamenZdrojCil(Fask.Interfaces.DataSets.Vyroba.Modifikace_TPDataTable dt);
    }
}
