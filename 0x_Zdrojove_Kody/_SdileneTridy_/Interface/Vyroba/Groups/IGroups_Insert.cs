using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.Groups
{
    public interface IGroups_Insert : IGroups
    {
        void Groups_Insert(string id, string Name, string Description);
    }
}
