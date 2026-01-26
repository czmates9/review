using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPP
{
    public interface IVPP_Insert : IVPP
    {
        void VPP_Insert_Row(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPRow row);
    }
}
