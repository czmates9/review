using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Fask.Parsing.Codes;
using System.Threading.Tasks;

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
        public ParsingFactory()
        {
        }

        //// TODO : toto odstranit a doplnit configuraci do MST
        //[Obsolete("Docasne funkcni pro zpetnou kompatibilitu", true)]
        //public static BaseCode Parse(string data)
        //{
        //    return Parse(new List<string>() { data }, null);
        //}

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

            if (config.GS1)
            {
                code = GS1.Parse(data);
                if ((code != null) && (code is GS1))
                    return (GS1)code;
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

			#endregion


            return code;
        }


        #region Asynchdonne řešení, test

        //public async Task<List<BaseCode>> ParseAsync(string data, Config config)
        //{
        //    //TaskCompletionSource<List<BaseCode>> tcs = new TaskCompletionSource<List<BaseCode>>();

        //    //return Task.Run(()=>{
        //    //    return ParseAsync(new List<string>() { data }, config);
        //    //});

        //    //return Parse(new List<string>() { data }, config);


        //    return await ParseAsync(new List<string>() { data }, config);
        //}

        public async Task<List<BaseCode>> ParseAsync(List<string> data, Config config)
        {

            TaskCompletionSource<List<BaseCode>> tcs = new TaskCompletionSource<List<BaseCode>>();

            // Pokud parsovani neni povoleno, tak vraci null
            if (config == null)
                return null;

            List<Task<BaseCode>> tasksBaseCodes = new List<Task<BaseCode>>();


            #region Reseni pro fy Steinex
            if (config.WeightCode) // Konfiguracni podminky parsovani ... 
            {
                tasksBaseCodes.Add(WeightCode.ParseAsync(data));
            }
            #endregion

            #region Prasarna Vlach...

            if (config.WeightCode_12) //VLACH
            {
                tasksBaseCodes.Add(WeightCode_12.ParseAsync(data));
            }
            #endregion

            #region Reseni pro fy Labara
            if (config.BarcodeSlashSarze) // Konfiguracni podminky parsovani ... 
            {
                tasksBaseCodes.Add(BarcodeSlashSarze.ParseAsync(data));
            }
            #endregion

            #region Reseni pro fy FENIX
            if (config.FenixBarcodeObal)
            {
                tasksBaseCodes.Add(FenixBarcodeObal.ParseAsync(data));
            }

            if (config.HIBC) // Konfiguracni podminky parsovani ... 
            {
                tasksBaseCodes.Add(HIBC.ParseAsync(data));
            }

            if (config.GS1)
            {
                tasksBaseCodes.Add(GS1.ParseAsync(data));
            }

            if (config.GS1_StriktniNorma)
            {
                tasksBaseCodes.Add(GS1_StriktniNorma.ParseAsync(data));
            }


            #endregion

            #region Reseni prasaren pro SAB

            if (config.SABNeznamyKod)
            {
                tasksBaseCodes.Add(SABNeznamyKod.ParseAsync(data));
            }

            if (config.SAB_AustralianNorm)
            {
                tasksBaseCodes.Add(SAB_AustralianNorm.ParseAsync(data));
            }

            if (config.SAB_GS1_Zavorky)
            {
                tasksBaseCodes.Add(SAB_GS1_Zavorky.ParseAsync(data));
            }

            if (config.SAB_GS1_BALTON)
            {
                tasksBaseCodes.Add(SAB_GS1_BALTON.ParseAsync(data));
            }


            #endregion

            var x = await Task.WhenAll<BaseCode>(tasksBaseCodes);

            var listResults = new List<BaseCode>(x);

            tcs.SetResult(listResults);

            return tcs.Task.Result;

        }

        #endregion

    }
}
