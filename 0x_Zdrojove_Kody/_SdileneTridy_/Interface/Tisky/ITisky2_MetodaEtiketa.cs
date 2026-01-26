using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Tisky
{
    public interface ITisky2_MetodaEtiketa : ITisky2
    {
        bool TiskMetodaEtiketa(int terminalID,ref  string templateName, ref Fask.Interfaces.DataSets.DSValues data, int pocetVytisku); 
    }
}
