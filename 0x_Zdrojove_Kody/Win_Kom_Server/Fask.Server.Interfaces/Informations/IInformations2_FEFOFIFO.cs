using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Server.Interfaces.Informations
{
    
        public interface IInformations2_FEFOFIFO : IInformations2
        {
        // ************** Online metody pro navrat dat ************* //
       Fask.Server.Interfaces.DataSets.Location FEFOFIFO_Online(string itemnmbr, string skl_id, string serltnum, string doc_id, string locncode);


    }
}
