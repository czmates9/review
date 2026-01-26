using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Lokace
{
    public interface ILokace2_Fill : ILokace2
    {
        void Fill(Fask.Interfaces.DataSets.Vyroba ds);
    }
}
