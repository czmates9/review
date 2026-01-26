using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPP
{
    public interface IVPP_Fill: IVPP
    {
        void VPP_Fill(Fask.Interfaces.DataSets.Vyroba ds);
    }
}
