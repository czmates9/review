using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Fask.PhotoProviderDolphin60S2D
{
    public class Dolphin60S2D : Fask.PhotoProvider.IPhotoProvider
    {
        #region IPhotoProvider Members

        public Fask.ScannerProvider.IScannerProvider Scanner
        {
            get;
            set;
        }

        public void SetForm(System.Windows.Forms.Form topLevelForm)
        {
            // nic se nedela
        }

        private string imageFileName = string.Empty;
        public string ImageFilename
        {
            get
            {
                return System.IO.Path.GetFileName(imageFileName);
            }
            set
            {
                imageFileName = value;
            }
        }

        public string ImagesDirectory
        {
            get;
            set;
        }

        public System.Windows.Forms.DialogResult CaptureImage()
        {
            return CaptureImage(string.Empty);
        }

        public DialogResult CaptureImage(string filename)
        {
            return CaptureImage(string.Empty, string.Empty);
        }

        public DialogResult CaptureServisImage(string filename)
        {
            return CaptureServisImage(string.Empty, string.Empty);
        }

        public DialogResult CaptureServisImage()
        {
            return CaptureServisImage(string.Empty);
        }

        public DialogResult CaptureImage(string filename, string statusBarText)
        {
            DialogResult dr = DialogResult.None;

            try
            {
                Scanner.TerminateScanner();

                using (SejmiImageForm sif = new SejmiImageForm(false, statusBarText))
                {
                    sif.ImagesDirectory = ImagesDirectory;
                    sif.CustomFileName = filename;

                    dr = sif.ShowDialog();
                    imageFileName = sif.ImageFilename;
                }

                return dr;
            }
            catch
            {
                throw;
            }
            finally
            {
                Scanner.InitializeScanner();
            }
        }

        public DialogResult CaptureServisImage(string filename, string statusBarText)
        {
            DialogResult dr = DialogResult.None;

            try
            {
                Scanner.TerminateScanner();

                using (SejmiImageForm sif = new SejmiImageForm(true, statusBarText))
                {
                    sif.ImagesDirectory = ImagesDirectory;
                    sif.CustomFileName = filename;

                    dr = sif.ShowDialog();
                    imageFileName = sif.ImageFilename;
                }

                return dr;
            }
            catch
            {
                throw;
            }
            finally
            {
                Scanner.InitializeScanner();
            }
        }

        #endregion
    }
}
