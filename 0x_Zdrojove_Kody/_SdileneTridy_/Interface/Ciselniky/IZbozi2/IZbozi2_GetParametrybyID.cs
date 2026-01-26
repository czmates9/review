using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Zbozi
{
    public interface IZbozi2_GetParametrybyID : IZbozi2
    {
       
        bool GetParametrybyID(string ITEMNMBR);
    }
}
