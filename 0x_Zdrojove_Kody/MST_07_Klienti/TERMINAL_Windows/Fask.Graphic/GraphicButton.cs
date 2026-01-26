using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Fask.Graphic
{
    public partial class GraphicButton : Control
    {
        private SolidBrush bPressed = null; //new SolidBrush(Color.Red);
        private SolidBrush bBackColor = null; //new SolidBrush(SystemColors.Control);
        private SolidBrush bForeColor = null; //new SolidBrush(Color.Black);
        private SolidBrush bFocusTextColor = null;

        //private SolidBrush bFocusedColor = null;
        private float lw = 2F;
        private Pen pBlack = null;
        private Pen pWhite = null;
        private Pen pFocused = null;
        private StringFormat sf = null;
        private Bitmap bmpOffScr = null;

        //private byte bOpacity = 0x7F / 2;
        //public byte Opacity
        //{
        //    get { return bOpacity; }
        //    set
        //    {
        //        bOpacity = value;
        //        if (this.BackColor != null)
        //        {
        //            Color c = this.BackColor;
        //            bFocusedColor = new SolidBrush(Color.FromArgb(((int)bOpacity << 24))); // + (c.R << 16) + (c.G << 8) + c.B));
        //        }
        //    }
        //}

        private ImageAttributes imagettributes = null;
        //private Bitmap _bitmapEmpty = new Bitmap(1, 1);
        private Bitmap _bitmapNormal = null;
        /// <summary>
        /// Normal bitmap
        /// </summary>
        public Bitmap BitmapNormal
        {
            get { return _bitmapNormal; }
            set
            {
                if (value != null)
                    _bitmapNormal = value;
                else
                    _bitmapNormal = null; //_bitmapEmpty;
                this.Invalidate();
            }
        }

        //private Bitmap _bitmapPressed;
        ///// <summary>
        ///// Pressed bitmap
        ///// </summary>
        //public Bitmap BitmapPressed
        //{
        //    get { return _bitmapPressed; }
        //    set { _bitmapPressed = value; }
        //}

        private int _focusMargin = 5;
        /// <summary>
        /// Focus Margin form clientrectangle
        /// </summary>
        public int FocusMargin
        {
            get { return _focusMargin; }
            set
            {
                _focusMargin = value;
                this.Invalidate();
            }
        }


        private bool _pressed = false;
        public bool Pressed
        {
            get { return _pressed; }
            set
            {
                _pressed = value;
                this.Invalidate();
            }
        }

        public GraphicButton()
        {
            InitializeComponent();

            bPressed = new SolidBrush(Color.Gray);
            bBackColor = new SolidBrush(this.BackColor);
            bForeColor = new SolidBrush(this.ForeColor);
            bFocusTextColor = new SolidBrush(Color.White);
            //Opacity = bOpacity;

            pBlack = new Pen(Color.Black, lw);
            pWhite = new Pen(Color.LightGray, lw);
            pFocused = new Pen(Color.Black, 2F);
            pFocused.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            //sf.LineAlignment = StringAlignment.Center;   

            bmpOffScr = null;

            imagettributes = new ImageAttributes();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (bmpOffScr == null || bmpOffScr.Width != this.ClientSize.Width || bmpOffScr.Height != this.ClientSize.Height)
            {
                bmpOffScr = new Bitmap(this.ClientSize.Width, ClientSize.Height);
            }

            Graphics goff = Graphics.FromImage(bmpOffScr);

            if (this.Enabled)
                goff.Clear(this.BackColor);
            else
                goff.Clear(Color.Gray);

            //Vykresledni pozadi
            //if (this.Enabled)
            //    goff.FillRectangle(bBackColor, ClientRectangle);
            //else
            //    goff.FillRectangle(new SolidBrush(Color.Gray), ClientRectangle);

            //Vykresleni bitmapy
            if (_bitmapNormal != null) // && _bitmapNormal != _bitmapEmpty)
            {
                //e.Graphics.DrawImage(_image, this.Width / 2 - _image.Width / 2, this.Height / 2 - _image.Height / 2);
                //e.Graphics.DrawImage(_bitmapNormal, 0, 0);
                //goff.DrawImage(_bitmapNormal, ClientRectangle, 0, 0, _bitmapNormal.Width, _bitmapNormal.Height, GraphicsUnit.Pixel, imagettributes);

                //Pozice?
                goff.DrawImage(_bitmapNormal, new Rectangle(0, 0, _bitmapNormal.Width, _bitmapNormal.Height), 0, 0, _bitmapNormal.Width, _bitmapNormal.Height, GraphicsUnit.Pixel, imagettributes);
            }

            //Vykresleni okraju a stinovani
            Point[] ps1 = new Point[] { 
                new Point(ClientRectangle.Left, ClientRectangle.Bottom), 
                new Point(ClientRectangle.Left, ClientRectangle.Top),
                new Point(ClientRectangle.Right, ClientRectangle.Top)
            };
            Point[] ps2 = new Point[] { 
                new Point(ClientRectangle.Right, ClientRectangle.Top), 
                new Point(ClientRectangle.Right, ClientRectangle.Bottom),
                new Point(ClientRectangle.Left, ClientRectangle.Bottom)
            };
            if (_pressed)
            {
                //goff.DrawRectangle(pWhite, ClientRectangle);
                goff.DrawLines(pBlack, ps1);
                goff.DrawLines(pWhite, ps2);
            }
            else
            {
                //goff.DrawRectangle(pBlack, ClientRectangle);
                goff.DrawLines(pWhite, ps1);
                goff.DrawLines(pBlack, ps2);
            }

            Rectangle rectangle = new Rectangle(ClientRectangle.Left + _focusMargin, ClientRectangle.Top + _focusMargin, ClientRectangle.Width - 2 * _focusMargin, ClientRectangle.Height - 2 * _focusMargin);
            //Vykresleni informace o focusu
            if (this.Focused)
            //if (true)
            {
                goff.DrawRectangle(pFocused, rectangle);
            }

            // 2.5.2016 PeV: na nekterych zarizenich z nejakeho duvodu font vraci null, docasne vyreseno takto -> vyuziva se vice fontu, takze to neni uplne vyreseni problemu, ale pouze zamezeni padu aplikace
            // TODO: ukladat fonty primo v konfiguraci aplikace ??
            //Vykresleni textu
            //SizeF sizef = goff.MeasureString(this.Text, this.Font);
            SizeF sizef;
            try
            {
                sizef = goff.MeasureString(this.Text, this.Font);
            }
            catch
            {
                Font f = new Font("Arial", 12, FontStyle.Bold);
                sizef = goff.MeasureString(this.Text, f);
            }
            //goff.DrawString(this.Text, this.Font, bForeColor, ClientRectangle, sf);
            //goff.DrawString(this.Text, this.Font, bForeColor, this.Width / 2F, this.Height / 2F, sf);
            //goff.DrawString(this.Text, this.Font, bForeColor, new RectangleF(ClientRectangle.Left + _focusMargin, ClientRectangle.Top + _focusMargin, ClientRectangle.Width - 2 * _focusMargin, ClientRectangle.Height - 2 * _focusMargin), sf);

            //Rectangle rectangle = new Rectangle(ClientRectangle.Left + _focusMargin, ClientRectangle.Top + _focusMargin, ClientRectangle.Width - 2 * _focusMargin, ClientRectangle.Height - 2 * _focusMargin);
            //Pen ptest0 = new Pen(Color.Green);
            //goff.DrawRectangle(ptest0, new Rectangle(0, 0, (int)sizef.Width, (int)sizef.Height));
            //Pen ptest1 = new Pen(Color.Red);
            //goff.DrawRectangle(ptest1, rectangle);

            //int x = rectangle.X;
            //int y = (int)((float)rectangle.Bottom / 2 - (sizef.Height / 2) * Math.Ceiling(sizef.Width / rectangle.Width));
            //int w = rectangle.Width;
            //int h = rectangle.Height;
            //int x = ClientRectangle.Width / 2 - (int)(sizef.Width / 2); //na levy roh...
            //int y = ClientRectangle.Height / 2 - (int)(sizef.Height / 2); //na stred ...
            //int w = (int)sizef.Width; 
            //int h = (int)sizef.Height; //rectangle.Height;
            //int x = rectangle.X;
            //int y = rectangle.Y;
            //int w = rectangle.Width;
            //int h = rectangle.Height;
            //int x = rectangle.X;
            //int y = rectangle.Y;
            //int w = rectangle.Width;
            //int h = rectangle.Height;
            //rectangle = new Rectangle(x, y, w, h);

            //Pen ptest2 = new Pen(Color.Blue);
            //goff.DrawRectangle(ptest2, rectangle);
            //Region region = new Region(rectangle);
            //goff.Clip = region;

            //if (this.Focused) //!!! bila na bile neni vubec videt !!!
            //    goff.DrawString(this.Text, this.Font, bFocusTextColor, rectangle, sf);
            //else
            //    goff.DrawString(this.Text, this.Font, bForeColor, rectangle, sf);
            sf.LineAlignment = StringAlignment.Center;
            // 2.5.2016 PeV: na nekterych zarizenich z nejakeho duvodu font vraci null, docasne vyreseno takto -> vyuziva se vice fontu, takze to neni uplne vyreseni problemu, ale pouze zamezeni padu aplikace
            // TODO: ukladat fonty primo v konfiguraci aplikace ??
            //goff.DrawString(this.Text, this.Font, bForeColor, rectangle, sf);
            try
            {
                goff.DrawString(this.Text, this.Font, bForeColor, ClientRectangle, sf);
            }
            catch
            {
                Font f = new Font("Arial", 12, FontStyle.Bold);
                goff.DrawString(this.Text, f, bForeColor, ClientRectangle, sf);
            }

            goff.Dispose();

            e.Graphics.DrawImage(bmpOffScr, 0, 0);

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _pressed = true;
            base.OnMouseDown(e);
            this.Focus();
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _pressed = false;
            base.OnMouseUp(e);
            this.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                bBackColor = new SolidBrush(base.BackColor);
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                bForeColor = new SolidBrush(base.ForeColor);
                this.Invalidate();
            }
        }

        private Color _transparentColor = Color.White;
        /// <summary>
        /// Transparentni barva
        /// </summary>
        public Color Transparent
        {
            get { return _transparentColor; }
            set
            {
                _transparentColor = value;
                imagettributes.SetColorKey(_transparentColor, _transparentColor);
                this.Invalidate();
            }
        }

        private string _text = string.Empty;
        public override string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
                this.Invalidate();
            }
        }

        public virtual void PerformClick()
        {
            this.OnClick(new EventArgs());
        }
    }
}