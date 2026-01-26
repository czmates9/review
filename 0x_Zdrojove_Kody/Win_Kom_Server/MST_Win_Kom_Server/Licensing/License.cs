using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.MST_W_Server.Licensing
{
    public class License
    {
        public string numberTerminal;
        public string company;
        public string contact;
        public string expiration;
        public string encryprition;
        public string created;
        public bool isValid = false;
        public bool isExpirated = true;
        public bool showInfoExpirationInTerminal = false;

        public License()
        {
        }

        public License(string numberterminal, string company, string contact, string expiration, string encryption, string created, bool isValid, bool expirated)
        {
            this.company = company;
            this.contact = contact;
            this.numberTerminal = numberterminal;
            this.expiration = expiration;
            this.encryprition = encryption;
            this.created = created;
            this.isValid = isValid;
            this.isExpirated = expirated;

        }
    }
}
