using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Fask.Aktualizace_API.Korekce
{
    public class KorekceException : Exception
    {
        public KorekceException()
            : base()
        {
        }

        public KorekceException(string message)
            : base(message)
        {
        }

        protected KorekceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public KorekceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
    }
}
