using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;
using System.Data;

namespace Fask.Server.Interfaces.Inventura2
{
    public interface IInventura2
    {

        //System.Collections.Specialized.NameValueCollection OutputParams { get; set; }
        
        // ************** Metody pro zisakni predlohy **************** //
        Fask.DataSets.Inventury2 Inventura2_GetInventury(Terminal terminal);
        Fask.DataSets.Inventura2 Inventura2_GetInventura(Davka davka, Terminal terminal);
        bool Inventura2_GetInventuraReceived(Davka davka, Terminal terminal);

        // ************** Metody pro zpracovani dat ************ //
        StatusObject Inventura2_Process(
            Davka davka, Terminal terminal, Fask.DataSets.Inventura2 inventuradata, ProcessState processInventuraState);
        bool Inventura2_AfterProcessedAction(Davka davka);

    }
}
