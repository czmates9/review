using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ukolovani
{
    public interface IUkolovani_Update_CZ_UKOL_UZIV : IUkolovani
    {

        void Update(Fask.Interfaces.DataSets.Ukolovani.CZ_UKOL_UZIVDataTable dt);

    }
}
