using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MSTW_Update
{

    public static class String2X_Extension
    {
        public static Version String2Font(this String s)
        {
            return String2X_Methods.String2Version(s);
        }
    }

    public class String2X_Methods
    {
       public static Version String2Version(string s)
        {
            Version v = new Version(s);
            return v;
        }


    }
}