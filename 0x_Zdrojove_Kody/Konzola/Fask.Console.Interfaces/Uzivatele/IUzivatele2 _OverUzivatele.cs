using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Interfaces.Uzivatele
{
    public interface IUzivatele2_OverUzivatele : IMES
    {
        //Hlavni prazdny interface

        bool OverUzivatele(string UserID, string pwd, string TID, string TID_typ);
    }
}

