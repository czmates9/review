using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Parsing.Codes.Interfaces
{
    /// <summary>
    /// If barcode supports quantity, than this interface have to be implemented
    /// </summary>
    public interface ICodeQuantity
    {
        /// <summary>
        /// Returns quantity if parsed, else null if not parsed yet
        /// </summary>
        decimal? Quantity { get; }
    }
}
