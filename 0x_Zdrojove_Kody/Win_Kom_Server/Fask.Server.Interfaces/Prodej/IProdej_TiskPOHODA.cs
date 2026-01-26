using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Prodej
{
	public interface IProdej_TiskPOHODA : IProdej
	{
		Fask.Server.Interfaces.Classes.StatusResult TiskPOHODA(string agenda, int ID_Dokladu, int ID_Sablony, int PocetKopii, string NazevTiskarny);
	}
}
