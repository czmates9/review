using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fask.Interfaces.Vyroba.Odvod_MachineStateSet
{
    public interface IOdvod_MachineStateSet_GetEnum_description : IOdvod_MachineStateSet
    {
        List<string> LoadStrojNameFromDatabase();

        List<string> LoadStrojDescriptionFromDatabase();

    }
}

