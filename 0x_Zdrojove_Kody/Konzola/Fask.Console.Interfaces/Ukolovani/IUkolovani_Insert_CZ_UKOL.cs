using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Ukolovani
{
    public interface IUkolovani_Insert_CZ_UKOL : IUkolovani
    {

        int Insert(string Name, string Description, string Code, int CreatorID, System.DateTime DateCreated, DateTime? DateFrom, DateTime? DateTo, string State, string Kind, string Type, int Priority, string PartnerID);



    }
}
