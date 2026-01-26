using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ukolovani
{
    public interface IUkolovani_Insert_CZ_UKOL_UZIV : IUkolovani
    {

        void Insert(int UkolID, int UserID, string State,DateTime? DateChanged, int? UserIDChanged, string Note, DateTime? DateNotify, DateTime? DateFinished);

    }
}
