using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VLoginsGroups
{
    public interface IVLoginsGroups_DeleteByGoupID : IVLoginsGroups
    {
        void DeleteByGoupID(string ID);
    }
}
