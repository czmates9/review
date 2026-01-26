using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Fask.MST_W.MySystem
{
    class FormMidLocation
    {
        private static Point midPoint = new Point(120, 147);

        public static Point GetFormLocation(Size s)
        {
            return new Point(midPoint.X - s.Width / 2, midPoint.Y - s.Height / 2);
        }
    }
}
