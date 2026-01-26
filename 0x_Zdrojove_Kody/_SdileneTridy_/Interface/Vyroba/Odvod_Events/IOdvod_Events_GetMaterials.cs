using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Odvod_Events
{
    public interface IOdvod_Events_GetMaterials : IOdvod_Events
    {
        System.Data.DataTable GetMaterials();
    }
}
