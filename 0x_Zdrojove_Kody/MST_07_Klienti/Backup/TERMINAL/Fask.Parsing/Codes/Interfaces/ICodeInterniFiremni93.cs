using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Parsing.Codes.Interfaces
{
	public interface ICodeInterniFiremni93
    {
        /// <summary>
        /// Returns Serial number or Lot (one or other) if it has been parsed or null/epty if not parsed yet
        /// </summary>
        string InterniParametr { get; }
    }
}
