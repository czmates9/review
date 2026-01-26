using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPH
{
    public interface IVPH_Fill : IVPH
    {
        void VPH_Fill(Fask.Interfaces.DataSets.Vyroba ds);
    }
}
