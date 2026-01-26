using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MST_Print_Server_ZPL_Printing
{
    public class User
    {
        /// <summary>
        /// Uzivatelske jmeno uzivatele, ktery bude tisknout.
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// Heslo uzivatele, ktery bude tisknout.
        /// </summary>
        public string Password = string.Empty;

        /// <summary>
        /// Nazev domeny.
        /// </summary>
        public string DomainName = string.Empty;

        public User()
        {
        }

        public User(string name, string password, string domainName)
        {
            this.Name = name;
            this.Password = password;
            this.DomainName = domainName;
        }
    }
}
