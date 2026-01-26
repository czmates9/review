using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Fask.Graphic
{
    public class Label2 : System.Windows.Forms.Control
    {
        private SolidBrush bForeColor = null; //new SolidBrush(Color.Black);
        private Bitmap bmpOffScr = null;
        private StringFormat sf = null;

        public Label2()
        {
            bForeColor = new SolidBrush(this.ForeColor);
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            //sf.LineAlignment = StringAlignment.Center;            
        }

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

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.Invalidate();
            }
        }

        public System.Drawing.StringAlignment StringAlignment
        {
            get { return sf.Alignment; }
            set { sf.Alignment = value; this.Invalidate(); }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnPaint(e);

            if (bmpOffScr == null || bmpOffScr.Width != this.ClientSize.Width || bmpOffScr.Height != this.ClientSize.Height)
            {
                bmpOffScr = new Bitmap(this.ClientSize.Width, ClientSize.Height);
            }

            Graphics goff = Graphics.FromImage(bmpOffScr);

            goff.DrawLine(new Pen(Color.Red), 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);

            if (this.Enabled)
                goff.Clear(this.BackColor);
            else
                goff.Clear(Color.Gray);

            //Vykresleni textu
            SizeF sizef = goff.MeasureString(this.Text, this.Font);

            Rectangle rectangle = new Rectangle(ClientRectangle.Left + _focusMargin, ClientRectangle.Top + _focusMargin, ClientRectangle.Width - 2 * _focusMargin, ClientRectangle.Height - 2 * _focusMargin);
            //Pen ptest0 = new Pen(Color.Green);
            //goff.DrawRectangle(ptest0, new Rectangle(0, 0, (int)sizef.Width, (int)sizef.Height));
            //Pen ptest1 = new Pen(Color.Red);
            //goff.DrawRectangle(ptest1, rectangle);
            int x = rectangle.X;
            int y = (int)((float)rectangle.Bottom / 2 - (sizef.Height / 2) * Math.Ceiling(sizef.Width / rectangle.Width));
            int w = rectangle.Width;
            int h = rectangle.Height;
            rectangle = new Rectangle(x, y, w, h);
            //Pen ptest2 = new Pen(Color.Blue);
            //goff.DrawRectangle(ptest2, rectangle);
            //Region region = new Region(rectangle);
            //goff.Clip = region;
            goff.DrawString(this.Text, this.Font, bForeColor, rectangle, sf);
            //goff.ResetClip();

            goff.Dispose();

            e.Graphics.DrawImage(bmpOffScr, 0, 0);

        }
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
    }
}
