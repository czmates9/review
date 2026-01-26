using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Fask.Vyroba_P.Korekce
{
    public class KorekceExceptionZpozdeni : KorekceException
    {
        public KorekceExceptionZpozdeni()
            : base()
        {
        }

        public KorekceExceptionZpozdeni(string message)
            : base(message)
        {
        }

        protected KorekceExceptionZpozdeni(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public KorekceExceptionZpozdeni(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
