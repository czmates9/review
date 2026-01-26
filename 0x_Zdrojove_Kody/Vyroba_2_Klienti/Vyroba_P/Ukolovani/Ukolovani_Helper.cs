using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.Vyroba_P.UkolovaniService;
using Fask.Vyroba_P.ServerAccess;

namespace Fask.Vyroba_P.Ukolovani
{
    public class Ukolovani_Helper
    {
        public _WebRefernces_Globals.UkolovaniServiceSession ukolovaniService;

        public Ukolovani_Helper()
        {
            ukolovaniService = new _WebRefernces_Globals.UkolovaniServiceSession();
            ukolovaniService.Url = Settings.WebServiceAddressVyroba + Constants.Ukolovani_asmx;
            ukolovaniService.Timeout = Settings.WebServiceTimeOut;
            ukolovaniService.UpdateWebServiceCredentials();
        }
    }
}
