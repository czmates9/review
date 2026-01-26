using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Fask.Aktualizace_API.Korekce
{
    public class KorekceExceptionUspora : KorekceException
    {
        public KorekceExceptionUspora()
            : base()
        {
        }

        public KorekceExceptionUspora(string message)
            : base(message)
        {
        }

        protected KorekceExceptionUspora(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public KorekceExceptionUspora(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
