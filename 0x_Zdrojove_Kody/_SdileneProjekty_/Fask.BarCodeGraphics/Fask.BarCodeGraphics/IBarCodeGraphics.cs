using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.BarCodeGraphics
{
    public enum BarCodeType
    {
        QR
    }

    public interface IBarCodeGraphics
    {
        /// <summary>
        /// Vraci upravenou sablonu o doplnenou grafiku (QR kod.
        /// </summary>
        /// <param name="template">Sablona s vyplnenymi udaji, ktere se maji tisknout</param>
        /// <returns>sablona s doplnenou grafikou</returns>
        StringBuilder AddGraphics(StringBuilder template);
    }
}
