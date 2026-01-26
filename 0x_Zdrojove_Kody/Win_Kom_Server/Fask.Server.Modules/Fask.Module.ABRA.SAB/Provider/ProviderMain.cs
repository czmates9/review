using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Module.ABRA.SAB.Provider
{
    public partial class Provider
    {
        /// <summary>
        /// Vstupni bod ...
        /// </summary>
        public Provider()
        {
            try
            {

                Fask.Server.Interfaces.Configuration.IConfiguration iconfig = this as Fask.Server.Interfaces.Configuration.IConfiguration;
                if (iconfig != null)
                    iconfig.LoadConfiguration();

            }
            catch (Exception ex)
            {
                // \TODO : zalogovat chybu, poslat chybu logovacim mechanismem ... 
                throw ex;
            }
        }
    }
}
