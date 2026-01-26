using Fask.Logging;
using FASK.SledovaniVyroby.ErrorLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Rapol.InkJet
{
    public class DataDecode
    {
        private const string header01PCCMR = "01PCCMR";

        /// <summary>
        /// Start Of Header
        /// </summary>
        private static readonly char SOH = (char)0x01; 
        /// <summary>
        /// Start of TeXt
        /// </summary>
        private static readonly char STX = (char)0x02;
        /// <summary>
        /// End Of Transfer
        /// </summary>
        private static readonly char EOT = (char)0x04;
        /// <summary>
        /// SI = Shift In
        /// </summary>
        private static readonly char SI = (char)0x0F;
        /// <summary>
        /// SO = Shift Out
        /// </summary>
        private static readonly char SO = (char)0x0E;
        /// <summary>
        /// RecordS
        /// </summary>
        private static readonly char RS = (char)0x1E;


        public string Header { get; private set; }
        public List<string> Texty { get; private set; }

        public static DataDecode Parse(string dataFull)
        {
            DataDecode dataDecode = new DataDecode();

            var datas = dataFull.Split(EOT);

            foreach (var d in datas)
            {
                try
                {
                    var data = d;

                    if (!data.StartsWith(SOH.ToString()))
                    //if (!data.StartsWith(Char.ToString(SOH)))
                    {
                        continue;
                    }

                    data = data.Substring(1); // bez SOH

                    dataDecode.Header = data.Substring(0, data.IndexOf(STX)); // header

                    if (dataDecode.Header != header01PCCMR)
                        throw new Exception(String.Format("Header '{0}' is not equal to {1}", dataDecode.Header, header01PCCMR));

                    data = data.Substring(data.IndexOf(STX) + 1); //bez headeru a STX

                    string texts = data.Substring(0, data.IndexOf(SI));

                    dataDecode.Texty = texts.Split(RS).ToList();   //pole textu

                    data = data.Substring(data.LastIndexOf(SO) + 1);    //posledni cast od posledni SO

                    var sohIndex = data.IndexOf(SOH);

                    dataDecode.Texty.Add(sohIndex > 0 ? data.Substring(0, sohIndex) : data);

                    break;

                }
                catch (Exception exParse)
                {
                   // Log.WriteException(exParse.Message);
                    ExceptionHandler2.Handle(exParse);
                    dataDecode = new DataDecode();
                }

            }

            return dataDecode;
        }
    }
}
