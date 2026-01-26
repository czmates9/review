using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Fask.Parsing.Codes;

namespace Fask.Parsing
{
    public sealed class ParsingFactory
    {
        public static BaseCode Parse(string data, Config config)
        {
            // Pokud parsovani neni povoleno, tak vraci null
            if (config == null)
                return null;

            BaseCode code = null;

            #region Reseni pro fy Steinex
            if (config.WeightCode) // Konfiguracni podminky parsovani ... 
            {
                code = WeightCode.Parse(data);
                if ((code != null) && (code is WeightCode))
                    return (WeightCode)code;
            }
            #endregion

			#region Reseni pro fy Vlach
			if (config.WeightCode_12) // Konfiguracni podminky parsovani ... 
			{
				code = WeightCode_12.Parse(data);
				if ((code != null) && (code is WeightCode_12))
					return (WeightCode_12)code;
			}
			#endregion

			#region Fenix a Labara
			//#region Reseni pro fy Labara
			//if (config.BarcodeSlashSarze) // Konfiguracni podminky parsovani ... 
			//{
			//    code = BarcodeSlashSarze.Parse(data);
			//    if ((code != null) && (code is BarcodeSlashSarze))
			//        return (BarcodeSlashSarze)code;
			//}
			//#endregion

			//#region Reseni pro fy FENIX
			//if (config.FenixBarcodeObal)
			//{
			//    code = FenixBarcodeObal.Parse(data);
			//    if ((code != null) && (code is FenixBarcodeObal))
			//        return (FenixBarcodeObal)code;
			//}

			//if (config.FenixHIBC) // Konfiguracni podminky parsovani ... 
			//{
			//    code = FenixHIBC.Parse(data);
			//    if ((code != null) && (code is FenixHIBC))
			//        return (FenixHIBC)code;
			//}

			//if (config.FenixGS1)
			//{
			//    code = FenixGS1.Parse(data);
			//    if ((code != null) && (code is FenixGS1))
			//        return (FenixGS1)code;
			//}
			//#endregion 
			#endregion

            return code;
        }
    }
}
