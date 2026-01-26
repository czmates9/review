using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Groups
{
    public interface IGroups_Update : IGroups
    {

        void Groups_Update(Fask.Interfaces.DataSets.Vyroba.GroupsDataTable dt);
    }
}
