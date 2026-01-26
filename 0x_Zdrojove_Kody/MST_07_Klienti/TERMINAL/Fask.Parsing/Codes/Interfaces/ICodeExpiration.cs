using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Parsing.Codes.Interfaces
{
    public interface ICodeExpiration
    {
        /// <summary>
        /// Returns expiration if parsed, else null if not parsed yet
        /// </summary>
        DateTime? Expiration { get; }
    }
}
