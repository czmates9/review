using Fask.Server.Interfaces.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Login
{
    public interface ILogin_Komunikace : ILogin
    {
        Pristupy GetFiltrovanyLogins(Fask.WEBAPI.API_BusinessObjects.Filtry_Login Filtr);
        Pristupy GetLogins_Auth(Fask.WEBAPI.API_BusinessObjects.Filtry_Auth filtr);
        bool Delete_Login(string id);
        bool Delete_Auth(int DEX_ROW_ID);
        bool Update_Login_Row(Pristupy.FASK_LoginsRow loginsrow);
        bool Insert_Login(string USERID, string firstname, string surname, string psswd);
        Pristupy GetFASK_AGENDA_Filtrovana(Fask.WEBAPI.API_BusinessObjects.Filtry_Agenda filtr);
        bool Insert_Auth(string USERID, string AGENDAID);
        bool isExist_Auth(string USERID, string AGENDAID);

    }
}
