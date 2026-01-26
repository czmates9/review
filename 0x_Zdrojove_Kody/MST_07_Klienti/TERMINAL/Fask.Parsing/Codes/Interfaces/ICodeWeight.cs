using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Parsing.Codes.Interfaces
{
    /// <summary>
    /// If barcode support Weight, this interface have to be implemented
    /// </summary>
    public interface ICodeWeight
    {
        /// <summary>
        /// Returns weight if parsed, else null if not parsed yet
        /// </summary>
        decimal? Weight { get; }
    }
}
