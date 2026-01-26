using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Fask.Vyroba_W
{
    public class ConnectionStrings
    {
        public static string VyrobaCE_ConnectionString
        {
            get { return "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdf); }
        }

        public static string VyrobaCE_Tmp_ConnectionString
        {
            get { return "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.VyrobaCESdfTmp); }
        }

        public static string Production_ConnectionString
        {
            get { return "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdf); }
        }

        public static string Production_Tmp_ConnectionString
        {
            get { return "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdfTmp); }
        }

        public static string ProductionHist_ConnectionString
        {
            get { return "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.ProductionSdfHist); }
        }

        public static string InternalState_ConnectionString
        {
            get { return "Data source=" + Path.Combine(MySystem.MyPath.DataDirectory, Constants.InternalStateSdf); }
        }

    }
}
