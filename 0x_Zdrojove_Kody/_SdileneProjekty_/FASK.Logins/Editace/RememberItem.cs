using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FASK.Logins.Editace
{
    //class RememberItem
    //{
    //}
    public class RememberItem
    {
        public FASK.Logins.Editace.Filtry_Login_A filtr;
        public string ID;
        public DataSets.Pristupy  ds;

        public RememberItem(FASK.Logins.Editace.Filtry_Login_A _filtr, string _id, DataSets.Pristupy _ds)
        {
            filtr = _filtr;
            ID = _id;
            ds = _ds;
        }

    }
}
