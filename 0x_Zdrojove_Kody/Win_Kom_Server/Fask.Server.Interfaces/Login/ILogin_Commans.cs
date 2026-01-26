using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Login
{
    public interface ILogin_Commans : ILogin
    {

		DataSets.Pristupy GetViewData(string USERID);

		DataSets.Pristupy GetLikeAgednaID(string AgendaID);

		DataSets.Pristupy GetAgednaID(string AgendaID);

		DataSets.Pristupy.FASK_LoginsDataTable GetLoginsByID(string AgendaID);

		DataSets.Pristupy.FASK_LoginsDataTable GetLogins();

		DataSets.Pristupy.FASK_LoginsDataTable GetOverLogins(string USERID, string PWD, string AGENDA);

	}

}
