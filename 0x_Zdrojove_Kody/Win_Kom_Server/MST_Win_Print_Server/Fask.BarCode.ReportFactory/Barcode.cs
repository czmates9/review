using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Fask.BarCode.ReportFactory
{
    public class Barcode
    {
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Unrestricted = true)]
        public byte[] GetBarcode(string codetype, int height, int width, string code)
        {
            try
            {
                ZXing.BarcodeWriter _writer = new ZXing.BarcodeWriter();
                _writer.Format = (ZXing.BarcodeFormat)Enum.Parse(typeof(ZXing.BarcodeFormat), codetype, true);
                _writer.Options.Height = height;
                _writer.Options.Width = width;
                System.Drawing.Bitmap bmp = _writer.Write(code);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.ToArray();
                ms.Close();
                return data;
            }
            catch (Exception e)
            {
                // zalogovat ... ?
                throw e;
            }
        }
    }
}
