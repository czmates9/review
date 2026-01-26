using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;

using Fask.Interfaces.Terminal;
using Fask.Interfaces.DataSets;

namespace Fask.ModulePohodaXML.Provider
{
    public partial class Provider : ITerminal,
        //    Fask.Interfaces.ITerminal_GetTerminalAkt,
        //Fask.Interfaces.ITerminal_GetTerminalDefinition,
        ITerminal_GetTerminalAll
    {
        #region ITerminal_GetTerminalAll Members

        public Terminal GetTerminalAll()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;

            Terminal dsterm = new Terminal();
            try
            {
                Globals_V1.LoadConfiguration();
                connection = new SqlConnection(Globals_V1.Konfigurace.ConnectionStrings[0].FASKDB);
                command = new SqlCommand();
                adapter = new SqlDataAdapter();

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
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
