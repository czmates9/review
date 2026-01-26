using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Parsing.Codes
{
	public class WeightCode_12 : BaseCode
        , Interfaces.ICodeBarcode
        , Interfaces.ICodeWeight
    {
        public override string Nazev => this.GetType().Name;

        public string prefix;
        public string id;
        public decimal weight;
        public int checkDigit;

        public WeightCode_12(List<string> codes)
            : base(codes)
        {
        }

        public static WeightCode_12 Parse(List<string> codes)
        {
            if (codes.Count != 1)
                return null;

            var data = codes.First();
            return Parse(data);
        }

        public static Task<BaseCode> ParseAsync(List<string> codes)
        {
            TaskCompletionSource<BaseCode> tcs = new TaskCompletionSource<BaseCode>();

            Task.Run(() => {

                if (codes.Count != 1)
                    tcs.SetResult(null);

                var data = codes.First();
                var code = Parse(data);
                tcs.SetResult((BaseCode)code);

            });

            return tcs.Task;
        }

        /// <summary>
        /// Parsovani vahoveho kodu 12 znaku
        /// </summary>
        /// <param name="ck"></param>
        /// <returns></returns>
        public static WeightCode_12 Parse(string ck)
        {
            try
            {
                WeightCode_12 wc = new WeightCode_12(new List<string>() { ck });
                if (ck.Length != 12)
                    return null;

                wc.prefix = ck.Substring(0, 2);
                if (wc.prefix != "28" && wc.prefix != "29")
                    return null;

                wc.id = ck.Substring(0, 6);
                string weight = ck.Substring(7, 5);
                //wc.weight = decimal.Add(decimal.Parse(weight.Substring(0, 2)), decimal.Parse(weight.Substring(2, 3)) / 1000);
                wc.weight = decimal.Parse(weight.Substring(0, 5)) / 10;

                //wc.checkDigit = int.Parse(ck.Substring(12, 1));
                wc.checkDigit = 0;

                return wc;
            }
            catch //(Exception ex)
            {
                return null;
            }
        }


        #region ICodeBarcode Members

        public string Barcode
        {
            get
            {
                //throw new NotImplementedException();
                return this.id;
            }
        }

        #endregion

        #region ICodeWeight Members

        public decimal? Weight
        {
            get
            {
                //throw new NotImplementedException();
                return this.weight;
            }
        }

        #endregion
    }
}
