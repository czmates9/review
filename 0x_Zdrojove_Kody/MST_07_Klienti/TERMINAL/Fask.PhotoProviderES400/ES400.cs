using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Fask.PhotoProviderES400
{
    public class ES400 : Fask.PhotoProvider.IPhotoProvider
    {
        #region IPhotoProvider Members
        private Form form = null;
        public void SetForm(Form topLevelForm)
        {
            form = topLevelForm;
        }

        public Fask.ScannerProvider.IScannerProvider Scanner
        {
            get;
            set;
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

        public DialogResult CaptureServisImage(string filename)
        {
            return CaptureImage(filename);
        }

        public DialogResult CaptureServisImage()
        {
            return CaptureImage(string.Empty);
        }

        public DialogResult CaptureImage(string filename, string statusBarText)
        {
            DialogResult dr = DialogResult.None;
            Microsoft.WindowsMobile.Forms.CameraCaptureDialog cameraCapture = null;
            try
            {
                //Scanner.TerminateScanner();

                cameraCapture = new Microsoft.WindowsMobile.Forms.CameraCaptureDialog();
                cameraCapture.Owner = form;
                
                // adresar pro ukladani
                cameraCapture.InitialDirectory = ImagesDirectory;

                // It is necessary to end picture files with ".jpg".
                // Otherwise the argument is invalid.
                cameraCapture.DefaultFileName = string.IsNullOrEmpty(filename) ? (Guid.NewGuid().ToString("N") + ".jpg") : (filename + ".jpg");

                // title
                cameraCapture.Title = statusBarText;

                // TODO: konfiguracne rozliseni foceni ...
                // 2048X1536 or a 3.1 Megapixel
                int resolutionWidth = 768;
                int resolutionHeight = 1024;

                cameraCapture.Resolution = new Size(resolutionWidth, resolutionHeight);

                // Specify capture mode
                cameraCapture.Mode = Microsoft.WindowsMobile.Forms.CameraCaptureMode.Still;   // fotka...

                // Specify still quality
                cameraCapture.StillQuality = Microsoft.WindowsMobile.Forms.CameraCaptureStillQuality.High;  // kvalita ...

                // Displays the "Camera Capture" dialog box
                dr = cameraCapture.ShowDialog();

                //TODO

                ImageFilename = cameraCapture.DefaultFileName;

                return dr;
            }
            catch
            {
                throw;
            }
            finally
            {
                try
                {
                    if (cameraCapture != null)
                        cameraCapture.Dispose();
                }
                catch (Exception ex)
                {
                    Fask.Logging.Log.Write(ex, "CaptureImage.ES400 -> dispose problem");
                }
                //Scanner.InitializeScanner();
            }
        }

        public DialogResult CaptureServisImage(string filename, string statusBarText)
        {
            return CaptureImage(filename, statusBarText);
        }

        #endregion
    }
}
