using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Groups
{
    public interface IGroups_Update_Row : IGroups
    {

        void Groups_Update_Row(Fask.Interfaces.DataSets.Vyroba.GroupsRow groupsRow);
    }
}
