using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.RFID
{
    public class EPCMemoryFormatException : Exception
    {
        public EPCMemoryFormatException() : base()
        {
        }
        public EPCMemoryFormatException(string message) : base(message)
        {
        }
        public EPCMemoryFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
    }
}
