using Fask.API_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Vyroba
{
    public interface IVyroba_Logs_Insert : IVyroba
    {
        bool Logs_Insert(DataSets.Vyroba.FASK_EventsErrDataTable dt);
    }
}
