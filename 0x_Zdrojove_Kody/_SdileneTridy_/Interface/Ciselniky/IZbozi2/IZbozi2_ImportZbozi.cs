using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ciselniky.Zbozi
{
    public interface IZbozi2_ImportZbozi : IZbozi2
    {
        string ImportZbozi();

        List<Tuple<string, string, bool>> GetZasobyTableInfo();

        //List<Tuple<string, string,bool>> GetZasobyTableInfo();
    }
}
