using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.RFID
{
    public static class StringExtensions
    {
        public static string Range(this string str, int startindex, int stopindex)
        {
            int len_pozadovany = stopindex - startindex + 1;
            int len_string = str.Length;
            string out_string = string.Empty;

            if((len_pozadovany + startindex)  < len_string )
            { 
                out_string =str.Substring(startindex, stopindex - startindex + 1);
            }

            return out_string;
        }

        public static string RangeReplace(this string str, int startindex, int stopindex, string s)
        {
            if (s.Length != stopindex - startindex + 1)
                throw new EPCMemoryFormatException("Length of s[" + s.Length + "] is not same as (stopindex - startindex + 1)[" + (stopindex - startindex + 1) + "]");

            StringBuilder sb = new StringBuilder();
            sb.Append(str.Substring(0, startindex));
            sb.Append(s);
            sb.Append(str.Substring(stopindex + 1));
            return sb.ToString();
        }
    }
}
