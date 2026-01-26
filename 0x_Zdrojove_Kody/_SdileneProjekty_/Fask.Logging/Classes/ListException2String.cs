using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Logging.Classes
{
    public class ListException2String
    {
        public static string ToString(List<Exception> exceptions)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Exception exc in exceptions)
            {
                sb.AppendLine(exc.Source + " : " + exc.Message);
            }
            return sb.ToString();
        }
    }
}
