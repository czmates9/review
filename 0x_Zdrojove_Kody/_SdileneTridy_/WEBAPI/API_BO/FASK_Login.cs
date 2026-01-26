using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.WEBAPI.API_BusinessObjects
{
    public class FASK_Login
    {
        public string USERID;

        public string firstname;

        public string surname;

        public string psswd;

        public DateTime? CREATED;

        public DateTime? VALIDFROM;

        public DateTime? VALIDTO;

        public string RFID;
    }
}