using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.PV
{
    public interface IPV_Edit_Zaplanovane : IPV
    {

        //bool Edit_Zaplanovane(decimal QTY, int ORD_OBJpol);
        bool Edit_Zaplanovane(int ORD_OBJpol, decimal? QTY, int? Flag);
    }
}
