using FASK.Logins.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASK.Logins.Komunikace
{
    public interface ICommans
    {
		Pristupy GetViewData(string USERID);

		Pristupy GetLikeAgednaID(string AgendaID);

		Pristupy GetAgednaID(string AgendaID);

		Pristupy.FASK_LoginsDataTable GetLoginsByID(string AgendaID);

		Pristupy.FASK_LoginsDataTable GetLogins();

		Pristupy.FASK_LoginsDataTable GetOverLogins(string USERID, string PWD, string AGENDA);
	}
}
