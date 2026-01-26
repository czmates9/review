using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Server.Interfaces.BarCodes
{
    public class SSCC
    {
        private string _CompanyPrefix;
        public string CompanyPrefix { get { return _CompanyPrefix; } set { _CompanyPrefix = value; } }

        private string _Year;
        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        private string _Number;
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }

        private string _CheckDigit;
        public string CheckDigit
        {
            get { return _CheckDigit; }
            set { _CheckDigit = value; }
        }

        private string _AI;
        public string AI
        {
            get { return _AI; }
            set { _AI = value; }
        }

        public string Code
        {
            get
            {
                //return AI + CompanyPrefix + Year + Number + CheckDigit;
                return AI + CompanyPrefix + Year + Number + CheckDigit;
            }
            set
            {
                string pom = value;
            }
        }

        public override string ToString()
        {
            //return base.ToString();
            return this.Code;
        }

        // TODO: dodelat ...
        public static SSCC Parse(string s)
        {
            SSCC sscc = new SSCC();

            // TODO: konfiguracne
            if (s.Length != 20)
                throw new Exception("Delka SSCC kodu nesouhlasi");

            sscc.AI = s.Substring(0, 2);
            if (sscc.AI != "00")
                throw new Exception("AI kod neni platny");

            sscc.CompanyPrefix = s.Substring(2, 8);

            sscc.Year = s.Substring(10, 4);

            sscc.Number = s.Substring(14, 5);
            sscc.CheckDigit = s.Substring(19, 1);

            //sscc.CheckDigit = BarCodes.CountParity_Modulo10(sscc.Code);
            //if (sscc.CheckDigit != s.Substring(s.Length - 1, 1))
            //    throw new Exception("Kontrolni soucet se neshoduje");

            //sscc.CheckDigit = BarCodes.CountParity_Modulo10(sscc.Code);
            //if (sscc.CheckDigit != s.Substring(s.Length - 1, 1))
            //    throw new Exception("Kontrolni soucet se neshoduje");

            return sscc;
        }

        public static string GenerateParity(string s)
        {
            if (s.Length != 19)
                throw new Exception("Delka SSCC kodu nesouhlasi");

            s = BarCodes.CountParity_Modulo10(s);
            //if (sscc.CheckDigit != s.Substring(s.Length - 1, 1))
            //    throw new Exception("Kontrolni soucet se neshoduje");

            return s;
        }
    }
}
