using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.SQL
{    
    public partial class Provider : Fask.Server.Interfaces.Configuration.IConfiguration
    {
        #region IConfiguration Members

        bool Fask.Server.Interfaces.Configuration.IConfiguration.LoadConfiguration()
        {
            //throw new NotImplementedException();
            //return true;

            return Globals_V1.LoadConfiguration() == "OK";
        }

        #endregion
    }
}
