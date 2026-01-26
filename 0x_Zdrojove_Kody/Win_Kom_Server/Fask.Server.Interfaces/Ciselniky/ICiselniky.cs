using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Ciselniky
{
	/// <summary>
	/// Zakladný Interface pro združení všech číselníkú
	/// </summary>
    public interface ICiselniky : IStrediska, IZbozi, ITypDokladu, IOdberatele, IPracovnici, ISklady, ILokace, IMeny
    {
    }
}
