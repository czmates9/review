using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int ADM { get; set; }

        public string firstname { get; set; }
        public string secondname { get; set; }
        public string barcode { get; set; }
    }
}
