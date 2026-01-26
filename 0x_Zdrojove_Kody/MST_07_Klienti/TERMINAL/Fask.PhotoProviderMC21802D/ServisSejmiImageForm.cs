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

namespace Fask.PhotoProviderMC21802D
{
    public partial class ServisSejmiImageForm : System.Windows.Forms.Form
    {
        //public static object ImageLockObject = new object();

        private Symbol.Imaging2.Imaging2 imaging2 = null;
        private Symbol.ResourceCoordination.Trigger trigger = null;
        private List<Symbol.ResourceCoordination.Trigger> triggerList = new List<Symbol.ResourceCoordination.Trigger>();

        /// <summary>
        /// Nazev (vcetne cesty) porizene fotografie.
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

        private Symbol.Imaging2.ImageData imageData = null;
        //public Symbol.Imaging2.ImageData ImageData
        //{
        //    get { return imageData; }
        //}

        public ServisSejmiImageForm(string statusBarText)
        {
            try
            {
                InitializeComponent();
                this.statusBarText = statusBarText;
                this.statusBar1.Text = statusBarText;
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "Fask.Fask.PhotoProviderMC21802D.SejmiImageForm -> konstruktor");
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
            else
            {
                e.Handled = false;
                return;
            } 
            e.Handled = true;
        }

        private void SejmiImageForm_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = MST_Global.FormBorderStyleGlobal;
            //this.Size = Forms.FormLocation.ScreenResolution;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            try
            {
                // je zakazan fotoaparat
                //if (!MST_Global.ServisAllowCamera)
                //{
                //    MessageBoxBig.Show("Focení není možné", this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Information);
                //    //PerformOK();
                //    return;
                //}
                //Symbol.Imaging2.Device[] devices = Symbol.Imaging2.Devices.AvailableDevices;

                imaging2 = new Symbol.Imaging2.Imaging2();

                // TODO : jen pokud jde o MC...???
                //Program.mstw.Scanner.TerminateScanner();

                //imaging2.OnCapture += new Symbol.Imaging2.Imaging2.OnCaptureHandler(imaging2_OnCapture);
                //imaging2.OnStatus += new Symbol.Imaging2.Imaging2.OnStatusHandler(imaging2_OnStatus);

                imaging2.Config.AcquisitionCapability.AimingMode.Value = Symbol.Imaging2.AimingModes.OFF;
                imaging2.Config.AcquisitionCapability.LampOn.Value = false;

                /******************************************************************
                 * 
                 * THE FOLLOWING CODE ADDS THE SUPPORT FOR ALL THE TRIGGERS.
                 * SOME OF THE TRIGGERS MAY NOT BE RETRIEVED BY USING THE 
                 * COLLECTION TriggerDevice.AvailableTriggers. 
                 * 
                 * ****************************************************************/
                try
                {
                    trigger = new Symbol.ResourceCoordination.Trigger(
                        new Symbol.ResourceCoordination.TriggerDevice(
                        Symbol.ResourceCoordination.TriggerID.ALL_TRIGGERS, new ArrayList()));
                    triggerList.Add(trigger);
                    trigger.Stage2Notify += new Symbol.ResourceCoordination.Trigger.TriggerEventHandler(trigger_Stage2Notify);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failure in adding ALL_TRIGGERS: " + ex.Message, "Error");
                }

                StartAcquisition();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex, "SejmiImageForm_Load");
            }

        }

        private void trigger_Stage2Notify(object sender, Symbol.ResourceCoordination.TriggerEventArgs e)
        {
            if (e.NewState == Symbol.ResourceCoordination.TriggerState.STAGE2)
            {
                if (imageData != null)
                    StartAcquisition();
                else
                    GetImage();
            }
        }

        private void GetImage()
        {
            try
            {
                statusBar1.Text = "Poøizuji fotku ...";

                Cursor.Current = Cursors.WaitCursor;
                Cursor.Show();

                imaging2.StopViewfinder();
                //ImageFilename = Path.Combine(ImagesDirectory, Guid.NewGuid().ToString("N") + ".jpg");
                ImageFilename = Path.Combine(ImagesDirectory, string.IsNullOrEmpty(CustomFileName) ? (Guid.NewGuid().ToString("N") + ".jpg") : (CustomFileName + ".jpg"));
                
                // pokud pokracuje dal ve foceni, tak smazat starou fotku? Ulozeni fotky by melo probehnout az pri OK ...
                //imageData = imaging2.CaptureImageNow(
                //    ImageFilename,
                //    true
                //    );
                imageData = imaging2.CaptureImageNow();
                pictureBox.Image = imageData.GetBitmap();
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            finally
            {
                statusBar1.Text = "Fotka poøízena. Triggerem znovu aktivovat focení ...";
                Cursor.Current = Cursors.Default;
            }

        }

        private void StartAcquisition()
        {
            statusBar1.Text = "Triggerem poøídit fotku ...";

            try
            {
                imaging2.StartAcquisition(pictureBox);
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }
            ImageFilename = null;
            imageData = null;
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
            // je zakazan fotoaparat
            //if (!MST_Global.ServisAllowCamera)
            //{
            //    createErrorImage();
            //    Logging.Log.Write("PerformOK", "SejmiImageForm");
            //    this.DialogResult = DialogResult.OK;
            //    return;
            //}

            if (imageData == null)
            {
                GetImage();

                if (imageData == null)
                {
                    //MessageBoxBig.Show("Nepodaøilo se vyfotit!");
                    MessageBox.Show("Nepodaøilo se vyfotit!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2); 
                    StartAcquisition();
                    return;
                }
            }

            FileStream fs = null;
            try
            {
                //lock (SejmiImageForm.ImageLockObject)
                //{
                fs = new FileStream(ImageFilename, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                imageData.GetBitmap().Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                //}
            }
            catch (Exception ex)
            {
                //MessageBoxBig.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxBigIcon.Critical);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                return;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Flush();
                    fs.Close();
                }
            }

            finalize();
            Logging.Log.Write("PerformOK", "SejmiImageForm");
            this.DialogResult = DialogResult.OK;
        }

        protected virtual void PerformCancel()
        {
            finalize();
            Logging.Log.Write("PerformCancel", "SejmiImageForm");
            this.DialogResult = DialogResult.Cancel;
        }

        protected virtual void finalize()
        {
            Logging.Log.Write("finalize", "SejmiImageForm(1)=>finalizing");
            try
            {
                Logging.Log.Write("finalize", "SejmiImageForm(2)=>triggers");
                foreach (Symbol.ResourceCoordination.Trigger trigger
                       in triggerList)
                {
                    Logging.Log.Write("finalize", "SejmiImageForm(3)=>trigger disposing");
                    if (trigger != null)
                        trigger.Dispose();
                }
                Logging.Log.Write("finalize", "SejmiImageForm(4)=>trigger list clear");
                triggerList.Clear();

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            try
            {
                Logging.Log.Write("finalize", "SejmiImageForm(5)=>imaging capture cancel");
                if (imaging2.IsCapturePending)
                    imaging2.CaptureCancel();

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            try
            {
                Logging.Log.Write("finalize", "SejmiImageForm(6)=>imaging stopacquisiton");
                if (imaging2.AcquisitionStarted)
                {
                    imaging2.StopAcquisition();
                }

            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            try
            {
                Logging.Log.Write("finalize", "SejmiImageForm(7)=>imaging disposing");
                if (imaging2 != null)
                {
                    imaging2.Disable();
                    imaging2.Dispose();
                    imaging2 = null;
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Write(ex);
            }

            try
            {
                Logging.Log.Write("finalize", "SejmiImageForm(8a)=>scanner restart:initializescanner");
                // TODO : jen pokud jde o MC...???
                // !!! Znovu musim nahodit scanner, aby to fungovalo jinde ...
                //Program.mstw.Scanner.InitializeScanner();

                Logging.Log.Write("finalize", "SejmiImageForm(8b)=>scanner restart:initializescanner End");
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

        private void graphicButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
        }

        private void krokzpet_but_Click(object sender, EventArgs e)
        {

        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            finalize();
            this.DialogResult = DialogResult.Retry;
        }

    }
}