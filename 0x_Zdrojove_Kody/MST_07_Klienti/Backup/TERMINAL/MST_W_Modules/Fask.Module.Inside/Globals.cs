using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Module.Inside
{
    public class Globals
    {
        public static int UserID = -1;
        public static string UserLogin = string.Empty;
        public static string UserPwd = string.Empty;
        public static Fask.ScannerProvider.IScannerProvider Scanner = null;
    }
}
