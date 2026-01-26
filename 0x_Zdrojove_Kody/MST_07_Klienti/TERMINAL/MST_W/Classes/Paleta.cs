using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.Classes
{
    public class Paleta
    {
        public string Nazev = string.Empty;
        public string ID = string.Empty;
        public string sscc = string.Empty;

        public Paleta()
        {
        }

        public Paleta(string id, string nazev, string sscc)
        {
            this.Nazev = nazev;
            this.ID = id;
            this.sscc = sscc;
        }

        /// <summary>
        /// Parsuje id a sscc kod palety
        /// </summary>
        /// <param name="paletastring"></param>
        /// <returns></returns>
        /// <value>[ID]:[SSCC]</value>
        public static Paleta Parse(string paletastring)
        {
            
            string[] pole = paletastring.Split(new char[] { ':' });
            return new Paleta(pole[0], string.Empty, pole[1]);
        }

        public override string ToString()
        {
            //return base.ToString();
            return this.ID.Trim() + ":" + this.sscc;
        }
    }
}
