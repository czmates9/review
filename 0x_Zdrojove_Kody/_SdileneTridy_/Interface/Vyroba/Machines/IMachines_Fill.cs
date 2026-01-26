using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Machines
{
    public interface IMachines_Fill : IMachines
    {
        void Machines_Fill(Fask.Interfaces.DataSets.Vyroba ds);
    }
}
