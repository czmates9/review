using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support
{
    class Utils
    {
        public static string Dump<T>(IEnumerable<T> list, string glue)
        {
            if (glue == null)
                glue = ", ";
            
            return string.Join(glue, list.Select(x => x.ToString()).ToArray());
        }
    }
}
