using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Tisky
{
	public interface ITisky2_MetodaSoupis : ITisky2
    {
		bool TiskMetodaSoupis(
            ref Fask.Server.Interfaces.DataSets.DSValues dataHeader,
            ref List<Fask.Server.Interfaces.DataSets.DSValues> dataRowList, 
            ref Fask.Server.Interfaces.DataSets.DSValues dataFooter
            ); 
    }
}
