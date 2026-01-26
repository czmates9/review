using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Fask.PhotoProviderMC21802D
{
    public class MC2180 : Fask.PhotoProvider.IPhotoProvider
    {
        #region IPhotoProvider Members

        public void SetForm(Form topLevelForm)
        {
        }

        public Fask.ScannerProvider.IScannerProvider Scanner
        {
            get;
            set;
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
            return CaptureImage(filename, string.Empty);
        }

        public DialogResult CaptureServisImage(string filename)
        {
            return CaptureServisImage(filename, string.Empty);
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

                using (SejmiImageForm sif = new SejmiImageForm(statusBarText))
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

                using (ServisSejmiImageForm sif = new ServisSejmiImageForm(statusBarText))
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
