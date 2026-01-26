using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.WEBAPI.API_BusinessObjects
{
    public class Filtry_Login
    {
        public string USERID { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public bool dtp_Create { get; set; }

        public bool dtp_ValidFrom { get; set; }

        public bool dtp_ValidTo { get; set; }

        public DateTime dtp_Create_Value { get; set; }

        public DateTime dtp_ValidFrom_Value { get; set; }

        public DateTime dtp_ValidTo_Value { get; set; }

        public string RFID { get; set; }
    }

    public class Filtry_Auth
    {
        public string USERID;
        public string IDAgendy;

    }

    public class Filtry_Agenda
    {
        public string AGENDAID;
        public string NAME;
        public string DESCIPTION;
    }
}

