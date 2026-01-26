using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fask.Vyroba_P.Controls
{
    public partial class ucTime : UserControl
    {
        private string formatTime = null;
        public string FormatTime
        {
            get { return this.formatTime; }
            set { this.formatTime = value; }
        }

        private int? formatTimeWidth = null;
        public int? FormatTimeWidth
        {
            get { return this.formatTimeWidth; }
            set
            {
                this.formatTimeWidth = value;
                if (formatTimeWidth.HasValue)
                {
                    this.Left = this.Right - formatTimeWidth.Value;
                    this.Width = this.formatTimeWidth.Value;
                }
            }
        }

        private Timer timerTime = new Timer();

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.timerTime_Tick(null, null);
        }

        public ucTime()
        {
            InitializeComponent();

            timerTime.Interval = 1000;
            timerTime.Tick += new EventHandler(timerTime_Tick);
            timerTime.Enabled = true;
        }

        void timerTime_Tick(object sender, EventArgs e)
        {
            try
            {
                //g.DrawString(DateTime.Now.ToString("HH:mm"), this.Font, new SolidBrush(this.TimeColor), this.ClientRectangle);

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                Graphics g = Graphics.FromHwnd(this.Handle);
                g.Clear(this.BackColor);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                //string longString = DateTime.Now.ToString("HH:mm:ss");
                string longString = DateTime.Now.ToString(this.formatTime == null ? Settings.FormMainTimeFormat : this.formatTime);
                Font PreferedFont = this.Font;
                Rectangle Room = this.ClientRectangle;

                //you should perform some scale functions!!!
                SizeF RealSize = g.MeasureString(longString, PreferedFont);
                float HeightScaleRatio = Room.Height / RealSize.Height;
                float WidthScaleRatio = Room.Width / RealSize.Width;
                float ScaleRatio = (HeightScaleRatio < WidthScaleRatio) ? ScaleRatio = HeightScaleRatio : ScaleRatio = WidthScaleRatio;
                float ScaleFontSize = PreferedFont.Size * ScaleRatio;
                Font font = new Font(PreferedFont.FontFamily, ScaleFontSize, PreferedFont.Style, PreferedFont.Unit);

                g.DrawString(longString, font, new SolidBrush(this.ForeColor), this.ClientRectangle, stringFormat);

                g.Dispose();
                g = null;
            }
            catch
            {
            }
        }
    }
}
