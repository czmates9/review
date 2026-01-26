using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Corrects
{
    public interface ICorrects_GetDataByID : ICorrects
    {
        Fask.Interfaces.DataSets.Vyroba.CorrectsDataTable Corrects_GetDataByID(int ID);
    }
}
