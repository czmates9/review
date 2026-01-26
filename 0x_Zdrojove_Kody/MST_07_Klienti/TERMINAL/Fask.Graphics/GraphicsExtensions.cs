using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Fask.Graphic
{
    public static class GraphicsExtensions
    {
        const string eclipsingstr = "...";
        public static string EclipseString(this System.Drawing.Graphics g, string text, Font font, RectangleF rectangleF)
        {
            StringBuilder sbEclipsed = new StringBuilder(text);
            SizeF sizeF1 = g.MeasureString(sbEclipsed.ToString(), font);
            SizeF sizeF2 = new SizeF(rectangleF.Width, rectangleF.Height);
            int step = text.Length / 2;
            int actlength = text.Length;
            while (true)
            {
                if (sizeF1.Width > sizeF2.Width)
                {
                    actlength -= step;
                }
                else
                {
                    step = step / 2;
                    actlength += step;
                }
                sbEclipsed = new StringBuilder(text.Substring(0, actlength) + eclipsingstr);
                sizeF1 = g.MeasureString(sbEclipsed.ToString(), font);

                if (step <= 1)
                    break;
            }
            return sbEclipsed.ToString();
        }
    }
}
