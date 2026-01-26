using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Groups
{
    public interface IGroups_GetDataByID : IGroups
    {
        Fask.Interfaces.DataSets.Vyroba.GroupsDataTable Groups_GetDataByID(string ID);
    }
}
