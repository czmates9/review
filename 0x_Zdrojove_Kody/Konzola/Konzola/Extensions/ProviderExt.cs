using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konzola.Extensions
{
    public static class ProviderExt
    {
        /// <summary>
        /// TODO toto by mnelo zmiznut
        /// </summary>
        /// <param name="provider"></param>
        public static void InitProvider(this Fask.Interfaces.IMES provider)
        {
            if ((provider != null) && (provider is Fask.Interfaces.Parametry.IParametry2_ConnectionString))
                ((Fask.Interfaces.Parametry.IParametry2_ConnectionString)provider).ConnectionString = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].FASKDB_ConnesctionString;

            if ((provider != null) && (provider is Fask.Interfaces.Parametry.IParametry2_Terminal_ID))
                ((Fask.Interfaces.Parametry.IParametry2_Terminal_ID)provider).Terminal_ID = Konfigurace.Globals_Konfig_Konzola.Konfigurace.System[0].TerminalID.ToString();

        }
    }
}
