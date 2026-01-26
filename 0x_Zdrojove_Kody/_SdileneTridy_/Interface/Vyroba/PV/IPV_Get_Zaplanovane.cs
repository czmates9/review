using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Get_Zaplanovane : IPV
    {

        decimal? Get_Zaplanovane(int ORD_OBJpol);
    }
}
