using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Parsing.Codes.Interfaces
{
    /// <summary>
    /// Interface for Itemnumber
    /// </summary>
    public interface ICodeItemnmbr
    {
        /// <summary>
        /// if code has itemnmbr returns it, or null/empty if it has not been parsed yet
        /// </summary>
        string Itemnmbr { get; }
    }
}
