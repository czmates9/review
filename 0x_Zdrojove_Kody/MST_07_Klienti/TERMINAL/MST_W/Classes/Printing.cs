using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Fask.MST_W.Classes
{
    public static class PrintingExtensions
    {
        public static string ToPrint(this Object o)
        {
            return Printing.Object2String(o);
        }
    }

    public class Printing
    {
        public static string Object2String(Object o)
        {
            try
            {
                if ((o == null) || (o is DBNull))
                    return string.Empty;

                if (o is decimal)
                    return ((decimal)o).ToString(NumberFormatInfo.InvariantInfo);

                if (o is float)
                    return ((float)o).ToString(NumberFormatInfo.InvariantInfo);

                if (o is double)
                    return ((double)o).ToString(NumberFormatInfo.InvariantInfo);

                if (o is DateTime)
                    return ((DateTime)o).ToString(NumberFormatInfo.InvariantInfo);

                return o.ToString().Trim();

            }
            catch (Exception e)
            {
                Logging.Log.Write(e);
                return "?";
            }
        }
    }
}
