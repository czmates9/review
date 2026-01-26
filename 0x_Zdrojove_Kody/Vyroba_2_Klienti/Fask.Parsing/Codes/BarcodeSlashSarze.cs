using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Parsing.Codes
{
    public class BarcodeSlashSarze : BaseCode
    {
        public string barcode;
        public string sarze;

        /// <summary>
        /// parsuje kod pro labara inventura. Ocekava se se slozeni "[carovykod]/[sarze]"
        /// </summary>
        /// <param name="data">car.kod ve formatu: [barcode]/[sarze]</param>
        /// <returns>barcodeslashsarze objekt nebo null, pokud neni tato definice</returns>
        public static BarcodeSlashSarze Parse(string data)
        {
            try
            {
                BarcodeSlashSarze bss = new BarcodeSlashSarze();

                // neobsahuje to slash, tak neni ocekavany .. 
                if (!data.Contains("/"))
                {
                    return null;
                }
                int index = data.IndexOf('/');

                bss.barcode = data.Substring(0, index);
                bss.sarze = data.Substring(index + 1);

                return bss;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return null; // neni to tento kod ....
            }
        }
    }
}
