using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MST_Print_Server_Classes
{
    public class SSCC
    {
        public string CompanyPrefix { get; set; }
        public string Year { get; set; }
        public string Number { get; set; }
        public string CheckDigit { get; set; }
        public string AI { get; set; }
        public string Code
        {
            get
            {
                return AI + CompanyPrefix + Year + Number + CheckDigit;
            }
        }
        public string CodeZPL
        {
            get
            {
                return ">;>8" + Code;
            }
        }

        public override string ToString()
        {
            //return base.ToString();
            return this.Code;
        }

        public static SSCC Parse(string s, int companyprefixlength)
        {
            SSCC sscc = new SSCC();

            sscc.AI = s.Substring(0, 2);
            if (sscc.AI != "00")
                throw new Exception("AI kod neni platny");

            sscc.CompanyPrefix = s.Substring(2, companyprefixlength);

            sscc.Year = s.Substring(2 + companyprefixlength, 1);
            //if (sscc.Year != DateTime.Today.ToString("yy").Substring(1, 1))
            //    ; // TODO : doresit konfiguracne vkladani roku

            sscc.Number = s.Substring(sscc.AI.Length + sscc.CompanyPrefix.Length + sscc.Year.Length, s.Length - 1 - 1);

            sscc.CheckDigit = BarCodes.CountParity_Modulo10(sscc.Code);
            if (sscc.CheckDigit != s.Substring(s.Length - 1 - 1, 1))
                throw new Exception("Kontrolni soucet se neshoduje");

            return sscc;
        }
    }
}
