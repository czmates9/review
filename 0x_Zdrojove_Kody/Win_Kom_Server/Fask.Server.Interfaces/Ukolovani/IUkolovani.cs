using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.Ukolovani
{
    public interface IUkolovani
    {
        Fask.DataSets.Ukoly GetServiceMan();

        Fask.DataSets.Ukoly GetUkoly(Classes.Terminal terminal, Classes.User user);

        bool SendEmailToServiceMan(Classes.User User, byte TerminalID, string MachineID, string To, string Subject, string poznamka);
    }
}
