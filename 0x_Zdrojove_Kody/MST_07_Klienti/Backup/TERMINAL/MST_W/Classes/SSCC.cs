using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Classes
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
                return AI + CompanyPrefix + Year + Number + CheckDigit;
            }
        }

        public override string ToString()
        {
            //return base.ToString();
            return this.Code;
        }

        public static SSCC Parse(string s)
        {
            SSCC sscc = new SSCC();
            //if (s.Length != MST_Global.VydejDelkaKoduPalety)
            //    throw new Exception("Delka SSCC kodu nesouhlasi");

            sscc.AI = s.Substring(0, 2);
            if (sscc.AI != "00")
                throw new Exception("AI kod neni platny");

            //sscc.CompanyPrefix = s.Substring(2, MST_Global.VydejPrefix.Length - 2);
           // if (sscc.CompanyPrefix != MST_Global.VydejPrefix.Substring(sscc.AI.Length))
           //     throw new Exception("Kód oraganizace nesouhlasí s nastavením aplikace");

           // sscc.Year = s.Substring(MST_Global.VydejPrefix.Length, 1);
            if (sscc.Year != DateTime.Today.ToString("yy").Substring(1, 1))
            {
                // TODO : doresit konfiguracne vkladani roku
            }

            sscc.Number = s.Substring(
                sscc.AI.Length + sscc.CompanyPrefix.Length + sscc.Year.Length,
                s.Length - 1 - (sscc.AI.Length + sscc.CompanyPrefix.Length + sscc.Year.Length)
            );

            sscc.CheckDigit = BarCodes.CountParity_Modulo10(sscc.Code);
            if (sscc.CheckDigit != s.Substring(s.Length - 1, 1))
                throw new Exception("Kontrolni soucet se neshoduje");

            return sscc;
        }

        /// <summary>
        /// Rozparsovani SSCC kodu bez toho, aniz by doslo ke kontrole kodu (az na delku)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static SSCC Parse2(string s)
        {
            SSCC sscc = new SSCC();

            if (s.Length != 20)
                throw new Exception("Delka SSCC kodu nesouhlasi");

            sscc.AI = s.Substring(0, 2);
            if (sscc.AI != "00")
                throw new Exception("AI kod neni platny");

            sscc.CompanyPrefix = s.Substring(2, 8);

            sscc.Year = s.Substring(10, 4);

            sscc.Number = s.Substring(14, 5);
            sscc.CheckDigit = s.Substring(19, 1);

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
