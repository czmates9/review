using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Fask.Module.MTJ.JimiTore.Baleni
{
    public class Globals
    {
        private class A
        {
        }

        public static DataSets.Configuration Configuration = null;

        public static byte TermID = 0;
        public static int UserID = -1;
        public static string UserLogin = string.Empty;
        public static string UserPwd = string.Empty;
        public static int UIGridFont;
        public static string UIFormatDesCisel;

        public static string ServerAddress = string.Empty;
        public static int ServerTimeout = 10000;

        public static Fask.ScannerProvider.IScannerProvider Scanner = null;

        public static string ConfigurationFile
        {
            get
            {
                return (new A()).GetType().Assembly.GetName().CodeBase + ".config";
            }
        }
        public static string DataDir { get { return WrkDir + @"Data\"; } }
        public static string ConfigDir { get { return WrkDir + @"Config\"; } }
        public static string SoundDir { get { return WrkDir + @"Sounds\"; } }

        public static string WrkDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\";           
    }
}
