using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Fask.Parsing.Codes;

namespace Fask.Parsing
{
    /// <summary>
    /// Singleton instance of ParsingFactory
    /// </summary>
    public sealed class ParsingFactory
    {
        //private static volatile ParsingFactory instance;
        //private static object syncRoot = new Object();

        //public static ParsingFactory Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            lock (syncRoot)
        //            {
        //                if (instance == null)
        //                    instance = new ParsingFactory();
        //            }
        //        }

        //        return instance;
        //    }
        //}

        /// <summary>
        /// Privatni konstruktor. Neni mozne tvorit dalsi instance teto tridy ...
        /// </summary>
        private ParsingFactory()
        {
        }

        // TODO : toto odstranit a doplnit configuraci do MST
        [Obsolete("Docasne funkcni pro zpetnou kompatibilitu", true)]
        public static BaseCode Parse(string data)
        {
            return Parse(new List<string>() { data }, null);
        }

        public static BaseCode Parse(string data, Config config)
        {
            return Parse(new List<string>() { data }, config);
        }

        public static BaseCode Parse(List<string> data, Config config)
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

			#region Prasarna Vlach...

			if (config.WeightCode_12) //VLACH
			{
				code = WeightCode_12.Parse(data);
				if ((code != null) && (code is WeightCode_12))
					return (WeightCode_12)code;
			} 
			#endregion

            #region Reseni pro fy Labara
            if (config.BarcodeSlashSarze) // Konfiguracni podminky parsovani ... 
            {
                code = BarcodeSlashSarze.Parse(data);
                if ((code != null) && (code is BarcodeSlashSarze))
                    return (BarcodeSlashSarze)code;
            }
            #endregion

			if (config.GS1)
			{
				code = GS1.Parse(data);
				if ((code != null) && (code is GS1))
					return (GS1)code;
			}

            #region Reseni pro fy FENIX

            if (config.FenixBarcodeObal)
            {
                code = FenixBarcodeObal.Parse(data);
                if ((code != null) && (code is FenixBarcodeObal))
                    return (FenixBarcodeObal)code;
            }



            if (config.HIBC) // Konfiguracni podminky parsovani ... 
            {
                code = HIBC.Parse(data);
                if ((code != null) && (code is HIBC))
                    return (HIBC)code;
            }


            #endregion

			#region Reseni prasaren pro SAB

			if (config.SABNeznamyKod)
			{
				code = SABNeznamyKod.Parse(data);
				if ((code != null) && (code is SABNeznamyKod))
					return (SABNeznamyKod)code;
			}

			if (config.SAB_AustralianNorm)
			{
				code = SAB_AustralianNorm.Parse(data);
				if ((code != null) && (code is SAB_AustralianNorm))
					return (SAB_AustralianNorm)code;
			}

			if (config.SAB_GS1_Zavorky)
			{
				code = SAB_GS1_Zavorky.Parse(data);
				if ((code != null) && (code is SAB_GS1_Zavorky))
					return (SAB_GS1_Zavorky)code;
			}

			if (config.SAB_GS1_BALTON)
			{
				code = SAB_GS1_BALTON.Parse(data);
				if ((code != null) && (code is SAB_GS1_BALTON))
					return (SAB_GS1_BALTON)code;
			}

			if (config.SAB_GS1_BELDICO)
			{
				code = SAB_GS1_BELDICO.Parse(data);
				if ((code != null) && (code is SAB_GS1_BELDICO))
					return (SAB_GS1_BELDICO)code;
			}

			#endregion

			
			#region řešení prasaren pro InnovaMedical

			if (config.InnovaMedical_GS1_Datum)
			{
				code = InnovaMedical_GS1_Datum.Parse(data);
				if ((code != null) && (code is InnovaMedical_GS1_Datum))
					return (InnovaMedical_GS1_Datum)code;
			}
			
			#endregion

            return code;
        }

	}
}
