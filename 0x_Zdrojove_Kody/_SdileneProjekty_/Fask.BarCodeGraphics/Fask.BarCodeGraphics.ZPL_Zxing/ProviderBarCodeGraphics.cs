using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Fask.BarCodeGraphics.ZPL_Zxing
{
    public class ProviderBarCodeGraphics : Fask.BarCodeGraphics.IBarCodeGraphics
    {
        #region IBarCodeGraphics Members

        /// <summary>
        /// Vraci upravenou sablonu o doplnenou grafiku (QR kod.
        /// </summary>
        /// <param name="template">Sablona s vyplnenymi udaji, ktere se maji tisknout</param>
        /// <returns>sablona s doplnenou grafikou</returns>
        public StringBuilder AddGraphics(StringBuilder template)
        {
            StringBuilder sbNew = new StringBuilder(template.ToString());

            // for testing
            // => http://regexstorm.net/tester
            //string txt = @"\#([\" + @"w ]+)(,)(\d+)(,)(\d+)(,)(\d+)(,)(\d+)(,)(\d+)(,)([\" + @"w ]+)\#";
            //string txt = @"\#([\w ]+)(,)(\d+)(,)(\d+)(,)(\d+)(,)(\d+)(,)(\d+)(,)([\w\W ]+)\#";
            //string txt = @"\#([\w ]+)(,)(\d+)(,)(\d+)(,)(\d+)(,)(\d+)(,)(\d+)(,)([\w /]*)\#";  //31.3.2020 - DobrePodlahy +lomitko, vse

            //31.3.2020 - DobrePodlahy +lomitko, vse,
            //29.9.2020 - SAB, uprava pomlcka v šarži
            string txt =   @"\#([\w ]+)(,)(\d+)(,)(\d+)(,)(\d+)(,)(\d+)(,)(\d+)(,)([\w /-]*)\#";  
            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(template.ToString(), txt);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string tresult = string.Empty;
                //string key = match.Groups[1].Value;
                Fask.BarCodeGraphics.BarCodeType type = (Fask.BarCodeGraphics.BarCodeType)Enum.Parse(typeof(Fask.BarCodeGraphics.BarCodeType), match.Groups[1].Value);

                //int? width = null;
                //try { width = int.Parse(match.Groups[3].Value); }
                //catch { }
                int xpoint = int.Parse(match.Groups[3].Value);
                int ypoint = int.Parse(match.Groups[5].Value);
                int width = int.Parse(match.Groups[7].Value);
                int height = int.Parse(match.Groups[9].Value);
                int rotation = int.Parse(match.Groups[11].Value);
                string data = match.Groups[13].Value;

                if (!string.IsNullOrEmpty(data))
                {
                    switch (type)
                    {
                        case BarCodeType.QR:
                            tresult = QRCodeCreator(width, height, xpoint, ypoint, rotation, data);
                            break;
                        default:
                            throw new Exception("Unknown BarCodeType");
                    } 
                }

                // finalni nahrazeni matche vysledkem formatovani ...
                sbNew.Replace(match.Value, tresult);
            }
            return sbNew;
        }

        private string QRCodeCreator(int width, int height, int xpoint, int ypoint, int rotation, string data)
        {
            IBarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                    {
                        Width = width,
                        Height = height,
                        Margin = 0
                    }
            };

            // vytvoreni bitmapy
            Bitmap bmap = writer.Write(data);
            // rotace bitmapy
            if(rotation != 0)
            {
                bmap = rotateImage(bmap, rotation);
            }

            bmap = ConvertTo1Bit(bmap);

            return CreateGRF(bmap, xpoint, ypoint);
        }

        public string CreateGRF(Bitmap bmap, int xpoint, int ypoint) //string filename, string imagename)
        {
            Bitmap bmp = null;
            BitmapData imgData = null;
            byte[] pixels;
            int x, y, width;
            StringBuilder sb;
            IntPtr ptr;

            try
            {
                bmp = new Bitmap(bmap);
                imgData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);
                width = (bmp.Width + 7) / 8;
                pixels = new byte[width];
                sb = new StringBuilder(width * bmp.Height * 2);
                ptr = imgData.Scan0;
                for (y = 0; y < bmp.Height; y++)
                {
                    Marshal.Copy(ptr, pixels, 0, width);
                    for (x = 0; x < width; x++)
                        sb.AppendFormat("{0:X2}", (byte)~pixels[x]);
                    ptr = (IntPtr)(ptr.ToInt64() + imgData.Stride);
                }
            }
            finally
            {
                if (bmp != null)
                {
                    if (imgData != null) bmp.UnlockBits(imgData);
                    bmp.Dispose();
                }
            }
            return String.Format("^FO{0},{1}^GFA,{2},{3},{4},", xpoint, ypoint, width * y, width * y, width) + sb.ToString();
        }

        private static Bitmap ConvertTo1Bit(Bitmap input)
        {
            var masks = new byte[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
            var output = new Bitmap(input.Width, input.Height, PixelFormat.Format1bppIndexed);
            var data = new byte[input.Width, input.Height];
            var inputData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                var scanLine = inputData.Scan0;
                var line = new byte[inputData.Stride];
                for (var y = 0; y < inputData.Height; y++, scanLine = new IntPtr(scanLine.ToInt64() + inputData.Stride))
                {
                    Marshal.Copy(scanLine, line, 0, line.Length);
                    for (var x = 0; x < input.Width; x++)
                    {
                        //data[x, y] = (byte)(64 * (GetGreyLevel(line[x * 3 + 2], line[x * 3 + 1], line[x * 3 + 0]) - 0.5));
                        data[x, y] = (byte)(255 * GetGreyLevel(line[x * 3 + 2], line[x * 3 + 1], line[x * 3 + 0]));
                    }
                }
            }
            finally
            {
                input.UnlockBits(inputData);
            }
            var outputData = output.LockBits(new Rectangle(0, 0, output.Width, output.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
            try
            {
                var scanLine = outputData.Scan0;
                for (var y = 0; y < outputData.Height; y++, scanLine = new IntPtr(scanLine.ToInt64() + outputData.Stride))
                {
                    var line = new byte[outputData.Stride];
                    for (var x = 0; x < input.Width; x++)
                    {
                        byte black = data[x, y] > 125 ? (byte)1 : (byte)0;
                        line[x / 8] |= (byte)(black << (7 - x % 8));
                        //var j = data[x, y] > 0;
                        //if (j) line[x / 8] |= masks[x % 8];
                        //var error = (sbyte)(data[x, y] - (j ? 32 : -32));
                        //if (x < input.Width - 1) data[x + 1, y] += (sbyte)(7 * error / 16);
                        //if (y < input.Height - 1)
                        //{
                        //    if (x > 0) data[x - 1, y + 1] += (sbyte)(3 * error / 16);
                        //    data[x, y + 1] += (sbyte)(5 * error / 16);
                        //    if (x < input.Width - 1) data[x + 1, y + 1] += (sbyte)(1 * error / 16);
                        //}
                    }
                    //Convert.ToString(line[x / 8], 2).PadLeft(8, '0');
                    Marshal.Copy(line, 0, scanLine, outputData.Stride);
                }
            }
            finally
            {
                output.UnlockBits(outputData);
            }
           
            return output;
        }
        

        /// <summary>
        /// Prevedeni do 1 bitove bitmapy (ZPL podporuje pouze jednobitovy)
        /// </summary>
        /// <param name="input">Image, ktery se prevadi</param>
        /// <returns>Prevedeny Image.</returns>
        //private static Bitmap ConvertTo1Bit(Bitmap input)
        //{
        //    var masks = new byte[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
        //    var output = new Bitmap(input.Width, input.Height, PixelFormat.Format1bppIndexed);
        //    var data = new sbyte[input.Width, input.Height];
        //    var inputData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
        //    try
        //    {
        //        var scanLine = inputData.Scan0;
        //        var line = new byte[inputData.Stride];
        //        for (var y = 0; y < inputData.Height; y++, scanLine = new IntPtr(scanLine.ToInt64() + inputData.Stride))
        //        {
        //            Marshal.Copy(scanLine, line, 0, line.Length);
        //            for (var x = 0; x < input.Width; x++)
        //            {
        //                data[x, y] = (sbyte)(64 * (GetGreyLevel(line[x * 3 + 2], line[x * 3 + 1], line[x * 3 + 0]) - 0.5));
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        input.UnlockBits(inputData);
        //    }
        //    var outputData = output.LockBits(new Rectangle(0, 0, output.Width, output.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
        //    try
        //    {
        //        var scanLine = outputData.Scan0;
        //        for (var y = 0; y < outputData.Height; y++, scanLine = new IntPtr(scanLine.ToInt64()+ outputData.Stride))
        //        {
        //            var line = new byte[outputData.Stride];
        //            for (var x = 0; x < input.Width; x++)
        //            {
        //                var j = data[x, y] > 0;
        //                if (j) line[x / 8] |= masks[x % 8];
        //                var error = (sbyte)(data[x, y] - (j ? 32 : -32));
        //                if (x < input.Width - 1) data[x + 1, y] += (sbyte)(7 * error / 16);
        //                if (y < input.Height - 1)
        //                {
        //                    if (x > 0) data[x - 1, y + 1] += (sbyte)(3 * error / 16);
        //                    data[x, y + 1] += (sbyte)(5 * error / 16);
        //                    if (x < input.Width - 1) data[x + 1, y + 1] += (sbyte)(1 * error / 16);
        //                }
        //            }
        //            Marshal.Copy(line, 0, scanLine, outputData.Stride);
        //        }
        //    }
        //    finally
        //    {
        //        output.UnlockBits(outputData);
        //    }
        //    return output;
        //}

        private static double GetGreyLevel(byte r, byte g, byte b)
        {
            return (r * 0.299 + g * 0.587 + b * 0.114) / 255;
        }

        /// <summary>
        /// Zajistuje rotaci Image.
        /// </summary>
        /// <param name="bmap">Image, ktery se ma pootocit</param>
        /// <param name="rotation">Uhel pootoceni.</param>
        /// <returns></returns>
        private Bitmap rotateImage(Bitmap bmap, int rotation)
        {
            //bmap.Save(@"C:\beforerotate.png", ImageFormat.Png);

            Bitmap returnBitmap = new Bitmap(bmap.Width, bmap.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(returnBitmap);
            g.Clear(Color.White);
            g.TranslateTransform((float)returnBitmap.Width / 2, (float)returnBitmap.Height / 2);
            g.RotateTransform(rotation);
            //g.TranslateTransform(-(float)bmap.Width / 2, -(float)bmap.Height / 2);
            g.TranslateTransform(-(float)returnBitmap.Width / 2, -(float)returnBitmap.Height / 2);
            g.DrawImage(bmap, new Point(0, 0));
            g.Dispose();
            //returnBitmap.Save(@"C:\afterrotate.png", ImageFormat.Png);

            return returnBitmap;
        }

        /// <summary>
        /// Prepise nalezene klice pomoci regularniho vyrazu => \$(\w+)((,)(\d+))?\$
        /// $[id](,[delka])?$
        /// [id] = identifikator
        /// [delka] = maximalni delka retezce (nemusi byt definovano, pak vraci cely retezec)
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="template">Template</param>
        /// <returns>Novy objekt s nahrazenymi parametry</returns>
        private StringBuilder ReplaceTemplateKeys(Dictionary<string, string> data, StringBuilder template)
        {
            StringBuilder sbNew = new StringBuilder(template.ToString());

            // RegEx 
            // => \$(\w+|.+,\d+)\$
            // => \$\w+(,\d+)?\$
            // => \$(\w+)((,)(\d+))?\$ 
            //  Group[0] = cely match
            //  Group[1] = identifikator (\w+)
            //  Group[2] = postfix ((,)(\d+))?
            //  Group[3] = carka (,)
            //  Group[4] = delka (\d+)
            //  Group[5] = postfix ((,)(\d+))?
            //  Group[6] = carka (,)
            //  Group[7] = delka (\d+)

            // puvodni - Obsahuje odpovidajici matche
            //System.Text.RegularExpressions.MatchCollection matches =
            //    System.Text.RegularExpressions.Regex.Matches(sbNew.ToString(), @"\$(\w+)((,)(\d+))?((,)(\d+))?\$");
            // puvodni - Obsahuje odpovídající matche
            //System.Text.RegularExpressions.MatchCollection matches =
            //    System.Text.RegularExpressions.Regex.Matches(sbNew.ToString(), @"\$([\w ]+)((,)(\d+))?((,)(\d+))?\$");
            System.Text.RegularExpressions.MatchCollection matches =
                System.Text.RegularExpressions.Regex.Matches(sbNew.ToString(), @"\#([\w ]+)(,)(\d+)(,)(\d+)(,)(\d+)(,)(\d+)(,)(\d+)(,)([\w ]+)\#");

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string tresult = string.Empty;
                string key = match.Groups[1].Value;
                if (!data.ContainsKey(key))
                { //klic v datech nenalezen, nahradim do sablony prazdnym retezcem ...
                    tresult = string.Empty;
                }
                else
                { //klic nalezen, tak se ho pokusim naformatovat ...
                    tresult = data[key];
                    int? p1 = null;
                    try { p1 = int.Parse(match.Groups[4].Value); }
                    catch { }
                    int? p2 = null;
                    try { p2 = int.Parse(match.Groups[7].Value); }
                    catch { }
                    if (p2.HasValue)
                    {
                        if (tresult.Length < p1.Value) //index mimo rozsah 
                            tresult = string.Empty;
                        else if (p2.Value <= 0)
                        {
                            tresult = tresult.Substring(
                                p1.Value,
                                // test na preteceni maximalni delky
                                tresult.Length - p1.Value
                                );
                        }
                        else // index v rozsahu
                            tresult = tresult.Substring(
                                p1.Value,
                                // test na preteceni maximalni delky
                                tresult.Length < (p1.Value + p2.Value) ? tresult.Length - p1.Value : p2.Value
                                );
                    }
                    else if (p1.HasValue && p1.Value > 0)
                    {
                        tresult = tresult.Substring(0, tresult.Length < p1.Value ? tresult.Length : p1.Value);
                    }
                    else
                    { // ??? neni nutny ... $<key>$
                    }

                }

                // finalni nahrazeni matche vysledkem formatovani ...
                sbNew.Replace(match.Value, tresult);
            }

            return sbNew;
        }

        #endregion
    }
}
