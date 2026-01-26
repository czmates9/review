using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Drawing.Imaging;
using HSM.Embedded.Camera;

namespace Fask.PhotoProviderDolphin60S
{
    public partial class SejmiImageForm : System.Windows.Forms.Form
    {
        //public static object ImageLockObject = new object();
        int height, width;
        CameraAssembly camera;
        JPGQuality m_QualityLevel = JPGQuality.High;

        private bool povolitKrokZpet = false;

        /// <summary>
        /// Nazev porizene fotografie.
        /// </summary>
        public string ImageFilename
        {
            get;
            set;
        }

        /// <summary>
        /// Adresar pro ukladani fotografii.
        /// </summary>
        public string ImagesDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Napevno zvoleny nazev souboru
        /// </summary>
        public string CustomFileName
        {
            get;
            set;
        }

        private string statusBarText = string.Empty;

        public SejmiImageForm()
        {
            InitializeComponent();
        }

        public SejmiImageForm(bool povolitKrokZpet, string statusBarText)
        {
            try
            {
                InitializeComponent();
                this.povolitKrokZpet = povolitKrokZpet;
                this.statusBarText = statusBarText;
                this.statusBar1.Text = statusBarText;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "Fask.Fask.PhotoProviderDolphin60S.SejmiImageForm -> konstruktor");
                MessageBox.Show(ex.Message, this.Text);
            }
        }

        private void SejmiImageForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;
            

            if ((e.KeyCode == Keys.Enter))
            {
                PerformOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PerformCancel();
            }
            else if (e.KeyCode == Keys.D1)
            {
                miSvetlo_Click(null, null);
            }
            else
            {
                e.Handled = false;
                return;
            }
          e.Handled = true;
        }

        private void InitCamera()
        {
            camera = new CameraAssembly();

            // Optionally, add a Handler for the Camera Event for notification
            // when various asynchronous camera events have completed
            camera.CameraEvent += new CameraAssembly.CameraEventHandler(camera_CameraEvent);
        }

        // We registered above for a Camera event so we will be notified
        // when the image capture has been completed. This is the event handler.
        private void camera_CameraEvent(object sender, CameraAssembly.CameraEventArgs e)
        {
            if (e.TaskCode == CameraAssembly.CameraTaskCodes.ImageCaptureComplete)
            {
                // presunuto do eventu
                finalize();
                Logging.Log.Write("camera_CameraEvent", "SejmiImageForm");
                this.DialogResult = DialogResult.OK;
                //sbcamStatus.Text = "File " + filename + " saved";
                //MessageBox.Show("Snap " + filename + " saved");
            }
        }

        private void SejmiImageForm_Load(object sender, EventArgs e)
        {
            try
            {
                // nacteni lokalizace ze souboru
                Fask.Localization.LocalizationExtensionForm.Localize(this);

                //this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
                //this.Size = Forms.FormLocation.ScreenResolution;
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                
                try
                {
                    InitCamera();
                    ConnectCamera(SensorType.Camera, null, pictureBox1.Handle);
                    // TODO: odkomentovat a opravit zamrznuti aplikace
                    //camera.StartPreview();
                }
                catch (Exception ex)
                {
                    Fask.Logging.Log.Write(ex, "SejmiImageForm_Load");
                }

                try
                {
                    // vypnuti svetla
                    camera.SetProperty(CameraProperty.Illumination, 0, PropertyMode.Manual);
                }
                catch (Exception ex)
                {
                    Logging.Log.Write(ex, "Fask.Fask.PhotoProviderDolphin60S.SejmiImageForm -> svetlo");
                    MessageBox.Show(ex.Message, this.Text);
                }

                // odstraneni tlacitka krok zpet z menu
                if (!povolitKrokZpet && mainMenu1.MenuItems.Contains(miKrokZpet))
                {
                    mainMenu1.MenuItems.Remove(miKrokZpet);
                }

                // spusteni timeru load
                timerLoad.Enabled = true;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "Fask.Fask.PhotoProviderDolphin60S.SejmiImageForm -> svetlo");
                MessageBox.Show(ex.Message, this.Text);
            }

        }

        /// <summary>
        /// Zobrazeni vystupu v picture boxu.
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="profile"></param>
        /// <param name="hWnd"></param>
        private void ConnectCamera(SensorType sensor, string profile, IntPtr hWnd)
        {
            try
            {
                // When not using a profile null's should be passed
                camera.Connect(sensor, hWnd, null, profile);
                //camera.Connect(hWnd, null, null);
            }
            catch (CameraException ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }

            //int val = 0;
            //int flags = 0;
            //try
            //{
            //    camera.GetProperty(CameraProperty.ColorEnable, out val, out flags);
            //    mnuColor.Checked = Convert.ToBoolean(val);
            //}
            //catch (CameraException ex)
            //{
            //    MessageBox.Show("GetProperty failed", ex.Message);
            //}

            //try
            //{
            //    camera.GetProperty(CameraProperty.Illumination, out val, out flags);
            //    mnuIllumination.Checked = Convert.ToBoolean(val);
            //}
            //catch (CameraException ex)
            //{
            //    MessageBox.Show("GetProperty failed", ex.Message);
            //}
        }

        private void ok_but_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

        private void zpet_but_Click(object sender, EventArgs e)
        {
            this.PerformCancel();
        }

        protected virtual void PerformOK()
        {
            statusBar1.Text = Fask.Localization.Localization.FormsSejmiImageFormPorizujiFotku;

            try
            {
                camera.GetResolutionDimensions(out height, out width, -1);
            }
            catch (CameraException ex)
            {
                MessageBox.Show("GetResolutionDimensions failed", ex.Message);
                return;
            }

            try
            {
                System.Drawing.Font font = new Font(FontFamily.GenericSansSerif, 8.0f, FontStyle.Italic);
                System.Drawing.Color color = Color.Blue;
                //camera.SnapPictureWithText(ImageFilename, m_QualityLevel, DateTime.Now.ToShortDateString(), TextOverlayLocation.UpperCenter, color, font);
                // ulozeni jako bmp, nic jineho asi neumi a automaticky pridava priponu bmp v pripade, ze je jina
                ImageFilename = Path.Combine(ImagesDirectory, string.IsNullOrEmpty(CustomFileName) ? (Guid.NewGuid().ToString("N") + ".bmp") : (CustomFileName + ".bmp"));
                camera.SnapPicture(ImageFilename, m_QualityLevel);
                this.statusBar1.Text = "Soubor " + ImageFilename + " uložen";
            }
            catch (CameraException ex)
            {
                MessageBox.Show("Snap " + ImageFilename + " failed", ex.Message);
                return;
            }

            // presunuto do eventu
            //finalize();
            //Logging.Log.Write("PerformOK", "SejmiImageForm");
            //this.DialogResult = DialogResult.OK;
        }

        protected virtual void PerformCancel()
        {
            try
            {
                finalize();
                Logging.Log.Write("PerformCancel", "SejmiImageForm");
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "Fask.Fask.PhotoProviderDolphin60S.SejmiImageForm -> PerformCancel");
                MessageBox.Show(ex.Message, this.Text);
            }
        }

        protected virtual void finalize()
        {
            Logging.Log.Write("finalize", "SejmiImageForm(1)=>finalizing");
            try
            {
                if (camera != null)
                {
                    camera.StopPreview();                
                    camera.Disconnect();
                    camera.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            Logging.Log.Write("finalize", "SejmiImageForm(9)=>End");
        }

        private void panelButtons_Resize(object sender, EventArgs e)
        {
            Size s = new Size(panelButtons.Width / 2, panelButtons.Height);
            ok_but.Size = s;
        }

        private void createErrorImage()
        {
            string text = "Focení není možné";

            Bitmap bitmap = new Bitmap(200, 200);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            g.DrawString(text, new Font("Arial", 20, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF(0, 0, 200, 200));
            g.Dispose();
            ImageFilename = Path.Combine(ImagesDirectory, Guid.NewGuid().ToString("N") + ".jpg");
            bitmap.Save(ImageFilename, ImageFormat.Jpeg);
            bitmap.Dispose();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {

        }

        private void miSvetlo_Click(object sender, EventArgs e)
        {
            miSvetlo.Checked = !miSvetlo.Checked;
            int enable = Convert.ToInt32(miSvetlo.Checked);

            try
            {
                camera.SetProperty(CameraProperty.Illumination, enable, PropertyMode.Manual);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "Fask.Fask.PhotoProviderDolphin60S.SejmiImageForm -> svetlo");
                MessageBox.Show(ex.Message, this.Text);
            }
        }

        private void miKrokZpet_Click(object sender, EventArgs e)
        {
            finalize();
            this.DialogResult = DialogResult.Retry;
        }

        private void timerLoad_Tick(object sender, EventArgs e)
        {
            timerLoad.Enabled = false;
            try
            {
                if (camera != null)
                    camera.StartPreview();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "Start cameraPreview(), Fask.Fask.PhotoProviderDolphin60S.SejmiImageForm -> svetlo");
                MessageBox.Show(ex.Message, this.Text);
            }
        }

        private void menuItemCancel_Click(object sender, EventArgs e)
        {
            PerformCancel();
        }

        private void menuItemOK_Click(object sender, EventArgs e)
        {
            PerformOK();
        }

    }
}