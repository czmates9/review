using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Vydej
{
	/// <summary>
	/// Enum které definuje přiznak co se ma stat s dávkou
	/// </summary>
    public enum ProcessState
    {
        Uvolnit,
        Zpracovat,
        ZpracovatAPokracovat
    }
}
