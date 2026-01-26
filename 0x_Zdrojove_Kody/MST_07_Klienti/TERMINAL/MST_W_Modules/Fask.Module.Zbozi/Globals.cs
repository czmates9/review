using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Fask.Module.Zbozi
{
    public class Globals
    {

        private class A
        {
        }

        public static DataSets.Configuration Configuration = null;

        public static Fask.ScannerProvider.IScannerProvider Scanner = null;

        public static int UserID = -1;
        public static string UserLogin = string.Empty;
        public static string UserPwd = string.Empty;

        public static string ServerAddress = string.Empty;
        public static int ServerTimeout = 10000;

        public static string CiselnikKatalogZboziDB { get { return Path.Combine(DataDir, "Zbozi.prd"); } }
        public static string CiselnikKatalogSkladyDB { get { return Path.Combine(DataDir, "Sklady.prd"); } }
        public static string ConfigurationFile
        {
            get
            {
                return (new A()).GetType().Assembly.GetName().CodeBase + ".config";
            }
        }

        public static string DataDir { get { return WrkDir + @"Data\"; } }

        public static string WrkDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\";
       
    
    }
}
