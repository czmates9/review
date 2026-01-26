using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.Logins
{
    public partial class Uzivatel
    {

        public bool GetPrava_Server_API(string USERID, string pwd)
        {
            try
            {

                IEnumerable<FASK.Logins.DataSets.Pristupy.FASK_LoginsRow> tmp = null;
                FASK.Logins.DataSets.Pristupy pris = FASK.Logins.Uzivatel.Instance.GetUzivateleSOpravnenim("API_");


                if ((pris != null) && (pris.FASK_Logins.Count > 0))
                {
                    tmp = pris.FASK_Logins.Where(x => x.USERID.Trim() == USERID.Trim() && x.psswd.Trim() == pwd.Trim());

                    if (tmp.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }
    }
}
