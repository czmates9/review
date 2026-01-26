using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fask.Parsing.Codes
{
    public class BarcodeSlashSarze : BaseCode, Interfaces.ICodeBarcode, Interfaces.ICodeSerltnmbr
    {

        public override string Nazev => this.GetType().Name;

        public string barcode;
        public string sarze;

        public BarcodeSlashSarze(List<string> codes)
            : base(codes)
        {
        }

        public static BarcodeSlashSarze Parse(List<string> codes)
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
        /// parsuje kod pro labara inventura. Ocekava se se slozeni "[carovykod]/[sarze]"
        /// </summary>
        /// <param name="data">car.kod ve formatu: [barcode]/[sarze]</param>
        /// <returns>barcodeslashsarze objekt nebo null, pokud neni tato definice</returns>
        public static BarcodeSlashSarze Parse(string data)
        {
            try
            {
                BarcodeSlashSarze bss = new BarcodeSlashSarze(new List<string>() { data });

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
            //catch (Exception ex)
            //{
            //    Logging.Log.Write(ex);
            //    return null; // neni to tento kod ....
            //}
            catch
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
                return this.barcode;
            }
        }

        #endregion

        #region ICodeSerltnmbr Members

        public string Serltnmbr
        {
            get
            {
                //throw new NotImplementedException();
                return this.sarze;
            }
        }

        #endregion
    }
}
