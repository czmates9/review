using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.MST_W_Server.API_BusinessObjects
{
    public class Konfig_Terminal_List
    {
        public string PathToFile_Relative = null; // relativní cesta k souboru
        public string ID_Terminal = null; // ID Terminalu
        public string ID_User = null; // ID Uživatele
        public DateTime? DateLastChange = null; // Datum poslednej zmeny souboru
    }
}