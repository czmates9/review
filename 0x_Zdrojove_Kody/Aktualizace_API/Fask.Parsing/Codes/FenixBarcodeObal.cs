using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Parsing.Codes
{
    public class FenixBarcodeObal : BaseCode
    {
        /// <summary>
        /// Cislo materialu
        /// </summary>
        public string MAT_ID;
        /// <summary>
        /// Sarze
        /// </summary>
        public string LOT;
        /// <summary>
        /// Mnozstvi
        /// </summary>
        public int? QTY;
        /// <summary>
        /// Cislo dodaciho listu
        /// </summary>
        public string PURCHASEFORMNUMBER;

        public static FenixBarcodeObal Parse(string data)
        {
            try
            {
                FenixBarcodeObal code = new FenixBarcodeObal();

                var dataSplit1 = data.Split("_".ToCharArray());
                if (dataSplit1.Length != 5)
                    return null;

                code.MAT_ID = dataSplit1[0];
                code.LOT = dataSplit1[1];
                code.QTY = int.Parse(dataSplit1[2]);
                code.PURCHASEFORMNUMBER = dataSplit1[3] + @"/" + dataSplit1[4];

                return code;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
                return null;
            }
        }
    }
}
