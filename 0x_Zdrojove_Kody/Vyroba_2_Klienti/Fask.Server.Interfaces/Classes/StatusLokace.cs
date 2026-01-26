using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
    public enum States
    {
        OK = 0,
        ERROR = 1
    }

    public class StatusLokace
    {
        /// <summary>
        /// 0 - vse OK 
        /// > 1 - Chyba
        /// </summary>
        public States State;
        public string ErrorMessage;
    }
}
