using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Classes
{
    public enum StatusResultEnum
    {
        OK = 0,
        WARNING = 1,
        ERROR = 2
    }

    public class StatusResult
    {
        /// <summary>
        /// 0 - vse OK 
        /// > 1 - Chyba
        /// </summary>
        public StatusResultEnum Status;
        public string Message;
    }
}
