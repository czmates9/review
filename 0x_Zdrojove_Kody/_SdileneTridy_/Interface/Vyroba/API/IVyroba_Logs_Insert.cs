using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.API
{
    public interface IVyroba_Logs_Insert : IMES
    {
        bool Logs_Insert(Fask.Interfaces.DataSets.Vyroba.FASK_EventsErrDataTable dt);
    }
}
