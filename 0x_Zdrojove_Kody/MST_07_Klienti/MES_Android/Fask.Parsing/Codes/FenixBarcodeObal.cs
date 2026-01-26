using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Parsing.Codes
{
    public class FenixBarcodeObal : BaseCode
        , Interfaces.ICodeItemnmbr
        , Interfaces.ICodeSerltnmbr
        , Interfaces.ICodeQuantity
    {
        public override string Nazev => this.GetType().Name;

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

        public FenixBarcodeObal(List<string> codes)
            : base(codes)
        {
        }

        public static FenixBarcodeObal Parse(List<string> codes)
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

        public static FenixBarcodeObal Parse(string data)
        {
            try
            {
                FenixBarcodeObal code = new FenixBarcodeObal(new List<string>() { data });

                var dataSplit1 = data.Split("_".ToCharArray());
                if (dataSplit1.Length != 5)
                    return null;

                code.MAT_ID = dataSplit1[0];
                code.LOT = dataSplit1[1];
                code.QTY = int.Parse(dataSplit1[2]);
                code.PURCHASEFORMNUMBER = dataSplit1[3] + @"/" + dataSplit1[4];

                return code;
            }
            //catch (Exception ex)
            //{
            //    Logging.Log.Write(ex);
            //    return null;
            //}
            catch
            {
                return null;
            }
        }

        #region ICodeItemnmbr Members

        public string Itemnmbr
        {
            get
            {
                //throw new NotImplementedException();
                return this.MAT_ID;
            }
        }

        #endregion

        #region ICodeSerltnmbr Members

        public string Serltnmbr
        {
            get
            {
                //throw new NotImplementedException();
                return this.LOT;
            }
        }

        #endregion

        #region ICodeQuantity Members

        public decimal? Quantity
        {
            get
            {
                //throw new NotImplementedException();
                return this.QTY;
            }
        }

        #endregion
    }
}
