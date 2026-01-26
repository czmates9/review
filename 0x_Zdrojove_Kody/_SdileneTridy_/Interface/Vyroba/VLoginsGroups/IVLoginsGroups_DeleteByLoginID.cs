using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VLoginsGroups
{
    public interface IVLoginsGroups_DeleteByLoginID : IVLoginsGroups
    {
        void DeleteByLoginID(string ID);
    }
}
