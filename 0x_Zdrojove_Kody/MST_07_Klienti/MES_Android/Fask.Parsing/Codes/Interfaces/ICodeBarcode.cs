using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Parsing.Codes.Interfaces
{
    /// <summary>
    /// Interface for barcode
    /// </summary>
    public interface ICodeBarcode
    {
        /// <summary>
        /// If code has barcode, than this returns it or null/empty if it has not been parsed yet
        /// </summary>
        string Barcode { get; }
    }
}
