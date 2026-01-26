using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;
using System.Data;

namespace Fask.Server.Interfaces.Inventura1
{
    public interface IInventura1
    {
        // ************** Metody pro zisakni predlohy **************** //
        Fask.DataSets.Inventury1 Inventura_GetInventury(Terminal terminal, Sklad sklad);
        Fask.DataSets.Inventura1 Inventura_GetInventura(Davka davka, Terminal terminal);
        bool Inventura_GetInventuraReceived(Davka davka, Terminal terminal);

        // ************** Metody pro zpracovani dat ************ //
        StatusObject Inventura_Process(
            Davka davka, Terminal terminal, Fask.DataSets.Inventura1 inventuradata, ProcessState processInventuraState);
        bool Inventura_AfterProcessedAction(Davka davka);

        // ************** Metody pro online kontroly ********* //
        bool Inventura_OnlineUnCheckState(Davka davka, Terminal terminal, string itemnmbr, out byte o_terminalid);

        bool Inventura_OnlineCheckState(Davka countentries, Terminal terminal, string itemnmbr, out byte o_terminalid);

    }
}
