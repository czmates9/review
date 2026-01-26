using Fask.DataSets;
using Fask.Server.Interfaces.Classes_Vyroba;
using Fask.Server.Interfaces.DataSets;
using Fask.Server.Interfaces.Extension;
using Fask.SQL.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using Ingres.Client;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Fask.Module.Ingres.SAD
{
    public partial class Provider :
     Fask.Interfaces.Terminal.ITerminal,
     //Fask.Console.Interfaces.ITerminal_GetTerminalAkt,
     //Fask.Console.Interfaces.ITerminal_GetTerminalDefinition,
     Fask.Interfaces.Terminal.ITerminal_GetTerminalAll
    {


        public Fask.Interfaces.DataSets.Terminal GetTerminalAll()
        {
            Globals.LoadConfiguration();
            string ConnectionString = Globals.Konfigurace.ConnectionString[0].FASKDB;

            IngresConnection connection = null;
            IngresCommand command = null;
            IngresDataAdapter adapter = null;

            Fask.Interfaces.DataSets.Terminal dsterm = new Fask.Interfaces.DataSets.Terminal();
            try
            {

                connection = new IngresConnection(ConnectionString);
                command = new IngresCommand();
                adapter = new IngresDataAdapter();

                command.CommandText =
                    " SELECT" +
                    " akt.ID_TERMINAL" +
                    " , akt.IP" +
                    " , akt.DATEREQ" +
                    " , def.DB_TYPE" +
                    " FROM CZMST_TERMINAL_AKT as akt" +
                    " left join CZMST_TERMINAL_DEFINITION as def" +
                    " ON def.ID_TERMINAL = akt.ID_TERMINAL";

                command.Connection = connection;
                adapter.SelectCommand = command;

                adapter.Fill(dsterm, dsterm.CZMST_TERMINAL_ALL.TableName);

                return dsterm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
