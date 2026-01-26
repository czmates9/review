using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Zbozi
{
    public interface IZbozi2_UpdateParametry : IZbozi2
    {
        
        bool UpdateParametry(Fask.Interfaces.DataSets.Zbozi.FASK_ZASOBY_ALL_KONZOLARow zboziRow);
    }
}
