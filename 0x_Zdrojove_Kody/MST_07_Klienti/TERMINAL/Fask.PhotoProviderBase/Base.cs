using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Fask.PhotoProviderBase
{
    public class Base : Fask.PhotoProvider.IPhotoProvider
    {
        #region IPhotoProvider Members

        public Fask.ScannerProvider.IScannerProvider Scanner
        {
            get;
            set;
        }

        public void SetForm(System.Windows.Forms.Form topLevelForm)
        {
        }

        public string ImageFilename
        {
            get;
            set;
        }

        public string ImagesDirectory
        {
            get;
            set;
        }

        public System.Windows.Forms.DialogResult CaptureImage(string filename)
        {
            return CaptureImage(filename, string.Empty);
        }

        public System.Windows.Forms.DialogResult CaptureImage()
        {
            return CaptureImage(string.Empty);
        }

        public System.Windows.Forms.DialogResult CaptureServisImage(string filename)
        {
            CaptureImage(filename);
            // vraci OK, aby bylo mozne v servisnim modulu pokracovat ...
            return DialogResult.OK;
        }

        public System.Windows.Forms.DialogResult CaptureServisImage()
        {
            return CaptureServisImage(string.Empty);
        }

        /// <summary>
        /// Vytvoreni obrazku s textem Foceni neni mozne (historicka zalezitost ...)
        /// </summary>
        /// <param name="filename">Nazev souboru.</param>
        private void createErrorImage(string filename)
        {
            string text = "Focení není možné";

            Bitmap bitmap = new Bitmap(200, 200);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            g.DrawString(text, new Font("Arial", 20, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF(0, 0, 200, 200));
            g.Dispose();
            //ImageFilename = Path.Combine(ImagesDirectory, Guid.NewGuid().ToString("N") + ".jpg");
            bitmap.Save(Path.Combine(ImagesDirectory, filename), System.Drawing.Imaging.ImageFormat.Jpeg);
            bitmap.Dispose();
        }

        public DialogResult CaptureImage(string filename, string statusBarText)
        {
            try
            {
                MessageBox.Show("Focení není možné", "Focení", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                ImageFilename = string.IsNullOrEmpty(filename) ? (Guid.NewGuid().ToString("N") + ".jpg") : filename;
                createErrorImage(ImageFilename);
                return System.Windows.Forms.DialogResult.Cancel;
            }
            catch
            {
                throw;
            }
        }

        public DialogResult CaptureServisImage(string filename, string statusBarText)
        {
            CaptureImage(filename);
            // vraci OK, aby bylo mozne v servisnim modulu pokracovat ...
            return DialogResult.OK;
        }

        #endregion
    }
}
