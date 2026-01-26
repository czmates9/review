using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.SkladLokace_Mapa
{
    public interface ISkladLokace_Mapa2_ImportSkladLokace_Mapa : ISkladLokace_Mapa2
    {

        string ImportSkladLokace_Mapa();
        List<Tuple<string, string, bool>> GetSkladLokace_MapaTableInfo();

    }
}
