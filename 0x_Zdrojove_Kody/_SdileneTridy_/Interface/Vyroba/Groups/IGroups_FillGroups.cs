using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Groups
{
    public interface IGroups_FillGroups : IGroups
    {
        void Groups_Fill(Fask.Interfaces.DataSets.Vyroba ds);
    }
}
