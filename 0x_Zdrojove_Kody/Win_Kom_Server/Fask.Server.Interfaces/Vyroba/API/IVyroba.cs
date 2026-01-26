using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Interfaces.Vyroba.API
{
    public interface IVyroba : IMES
    {

        bool FASK_Events_row_11_2023_Insert(Fask.WEBAPI.API_BusinessObjects.FASK_Events_row_11_2023 FE_object);
    }
}
