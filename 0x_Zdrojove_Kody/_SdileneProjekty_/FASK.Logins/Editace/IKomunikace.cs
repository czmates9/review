using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FASK.Logins.Editace
{
    public interface IKomunikace
    {
        DataSets.Pristupy GetFiltrovanyLogins(FASK.Logins.Editace.Filtry_Login_A Filtr);
        DataSets.Pristupy GetLogins_Auth(FASK.Logins.Editace.Filtry_Auth_A filtr);
        bool Delete_Login(string id);
        bool Delete_Auth(int DEX_ROW_ID);
        bool Update_Login_Row(DataSets.Pristupy.FASK_LoginsRow loginsrow);
        bool Insert_Login(string USERID, string firstname, string surname, string psswd);
        DataSets.Pristupy GetFASK_AGENDA_Filtrovana(Filtry_Agenda_A filtr);
        bool Insert_Auth(string USERID, string AGENDAID);
        bool isExist_Auth(string USERID, string AGENDAID);
    }
}
