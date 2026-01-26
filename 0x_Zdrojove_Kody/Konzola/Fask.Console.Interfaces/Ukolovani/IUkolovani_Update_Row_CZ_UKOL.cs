using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ukolovani
{
    public interface IUkolovani_Update_Row_CZ_UKOL : IUkolovani
    {

        void UpdateRow(Fask.Interfaces.DataSets.Ukolovani.CZ_UKOLRow dt);

    }
}
