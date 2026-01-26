using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace Fask.Graphic
{
    [DesignTimeVisible(true)]
    public class ImageControl : Control
    {
        //Private members      
        private Image image = null;
        //flag to indicate the pressed state
        private Bitmap m_bmpOffscreen;

        private Color _transparent = Color.Transparent;
        public Color Transparent
        {
            get { return _transparent; }
            set
            {
                _transparent = value;
                if (_transparent == null)
                    _transparent = ImageFirstPixelColor;
                this.Invalidate();
            }
        }

        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                this.Invalidate();
            }
        }

        public ImageControl()
        {
            //default minimal size
            this.Size = new Size(21, 21);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Graphics gxOff;      //Offscreen graphics
            Rectangle imgRect; //image rectangle

            if (m_bmpOffscreen == null) //Bitmap for doublebuffering
            {
                m_bmpOffscreen = new Bitmap(ClientSize.Width, ClientSize.Height);
            }

            gxOff = Graphics.FromImage(m_bmpOffscreen);

            gxOff.Clear(this.BackColor);

            if (image != null)
            {

                //Set transparent key
                ImageAttributes imageAttr = new ImageAttributes();
                //Color tColor = BackgroundImageColor(image);
                imageAttr.SetColorKey(_transparent, _transparent);

                //Draw image
                //if (_sizeMode == PictureBoxSizeMode.Normal)
                if (_sizeMode == ImageSizeMode.Normal)
                {
                    imgRect = new Rectangle(
                        0,
                        0,
                        image.Width,
                        image.Height
                        );

                    gxOff.DrawImage(image, imgRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                }
                //else if (_sizeMode == PictureBoxSizeMode.CenterImage)
                else if (_sizeMode == ImageSizeMode.CenterImage)
                {
                    imgRect = new Rectangle(
                        (this.Width - image.Width) / 2,
                        (this.Height - image.Height) / 2,
                        image.Width,
                        image.Height
                        );

                    gxOff.DrawImage(image, imgRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                }
                //else if (_sizeMode == PictureBoxSizeMode.StretchImage)
                else if (_sizeMode == ImageSizeMode.StretchImage)
                {
                    imgRect = new Rectangle(
                        0,
                        0,
                        this.Width,
                        this.Height
                        );

                    gxOff.DrawImage(image, imgRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                }
                else if (_sizeMode == ImageSizeMode.FitImage)
                {
                    int sourceWidth = image.Width;
                    int sourceHeight = image.Height;

                    float nPercent = 0;
                    float nPercentW = 0;
                    float nPercentH = 0;

                    nPercentW = ((float)this.Width / (float)sourceWidth);
                    nPercentH = ((float)this.Height / (float)sourceHeight);

                    if (nPercentH < nPercentW)
                        nPercent = nPercentH;
                    else
                        nPercent = nPercentW;

                    int destWidth = (int)(sourceWidth * nPercent);
                    int destHeight = (int)(sourceHeight * nPercent);
                    
                    imgRect = new Rectangle(
                        (this.Width - destWidth) / 2,
                        (this.Height - destHeight) / 2,
                        destWidth,
                        destHeight
                        );

                    gxOff.DrawImage(image, imgRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                }
                //gxOff.DrawImage(image, this.ClientRectangle, 0, 0, image.Width, image.Height,
                //    GraphicsUnit.Pixel, imageAttr);

            }

            //Draw from the memory bitmap
            e.Graphics.DrawImage(m_bmpOffscreen, 0, 0);

            if (this.Text.Length > 0)
            {
                SizeF stringsize = e.Graphics.MeasureString(this.Text, this.Font);
                RectangleF textposition = new RectangleF(
                    (e.ClipRectangle.Width - stringsize.Width) / 2F,
                    (e.ClipRectangle.Height - stringsize.Height),
                    stringsize.Width,
                    stringsize.Height
                    );
                e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textposition);
            }

            //base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //Do nothing
        }

        public Color ImageFirstPixelColor
        {
            get { return BackgroundImageColor(image); }
        }

        private Color BackgroundImageColor(Image image)
        {            
            if (image == null)
                return Color.FromArgb(0);

            Bitmap bmp = new Bitmap(image);
            return bmp.GetPixel(0, 0);
        }

        //private PictureBoxSizeMode _sizeMode = PictureBoxSizeMode.StretchImage;
        //public PictureBoxSizeMode SizeMode
        private ImageSizeMode _sizeMode = ImageSizeMode.StretchImage;
        public ImageSizeMode SizeMode
        {
            get { return this._sizeMode; }
            set
            {
                this._sizeMode = value;
                this.Invalidate();
            }
        }

        // Summary:
        //     Specifies how an image is positioned within a System.Windows.Forms.PictureBox.
        public enum ImageSizeMode
        {
            /// <summary>
            /// The image is placed in the upper-left corner of the System.Windows.Forms.PictureBox.
            /// The image is clipped if it is larger than the System.Windows.Forms.PictureBox
            /// it is contained in.
            /// </summary>
            Normal = 0,
            /// <summary>
            /// The image within the System.Windows.Forms.PictureBox is stretched or shrunk
            /// to fit the size of the System.Windows.Forms.PictureBox.
            /// </summary>
            StretchImage = 1,
            /// <summary>
            /// The image is displayed in the center if the System.Windows.Forms.PictureBox
            /// is larger than the image. If the image is larger than the System.Windows.Forms.PictureBox,
            /// the picture is placed in the center of the System.Windows.Forms.PictureBox
            /// and the outside edges are clipped.
            /// </summary>
            CenterImage = 3,
            /// <summary>
            /// Fits image into pictureBox Visible Area ...
            /// </summary>
            FitImage = 4
        }

    }
}
