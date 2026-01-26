using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Fask.MST_W.ServerAccess;

namespace Fask.MST_W.Ukolovani_1
{
    public class Ukolovani_Helper
    {
        public Fask.MST_W._WebRefernces_Globals.UkolovaniServiceSession ukolovaniService;

        public Ukolovani_Helper()
        {
            ukolovaniService = new Fask.MST_W._WebRefernces_Globals.UkolovaniServiceSession();
            ukolovaniService.Url = MST_Global.ServerAddress + "Ukolovani.asmx";
            ukolovaniService.Timeout = MST_Global.TasksTimeout;
            ukolovaniService.UpdateWebServiceCredentials();
        }
    }
}
