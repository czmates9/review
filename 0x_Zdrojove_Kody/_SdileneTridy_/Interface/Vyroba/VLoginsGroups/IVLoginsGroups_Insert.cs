using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Vyroba.VLoginsGroups
{
    public interface IVLoginsGroups_Insert : IVLoginsGroups
    {
        void Insert(string loginid, string groupid);
    }
}
