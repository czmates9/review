using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VPP
{
    public interface IVPP_Update : IVPP
    {
        void VPP_Update(Fask.Interfaces.DataSets.Vyroba.CZPRO_VPPDataTable dt);
    }
}
