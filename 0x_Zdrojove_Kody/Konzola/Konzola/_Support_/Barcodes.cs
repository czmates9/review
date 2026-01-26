using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Konzola._Support_
{
    public class Barcodes
    {
        public static byte[] GetBarcodeImage(string barcode, ZXing.BarcodeFormat barcodeformat, ZXing.Common.EncodingOptions options = null)
        {
            //ZXing.OneD.Code128Writer c = new ZXing.OneD.Code128Writer();
            ZXing.BarcodeWriter bw = new ZXing.BarcodeWriter();
            bw.Format = barcodeformat;

            if (options != null)
                bw.Options = options;

            Bitmap bmp = bw.Write(barcode);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] data = ms.ToArray();
            return data;
        }

    }
}
