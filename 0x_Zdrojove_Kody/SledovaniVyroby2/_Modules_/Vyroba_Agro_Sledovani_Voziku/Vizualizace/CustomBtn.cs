using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FASK.SledovaniVyroby.Module.Vyroba_Agro_Sledovani_Voziku.Vizualizace
{
    public class CustomBtn : Button
    {
        private bool ShouldDraw = false;
        private LinearGradientBrush myBrush = null;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            ShouldDraw = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ShouldDraw = false;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            if (ShouldDraw)
            {
                if (myBrush == null || (myBrush != null && myBrush.Rectangle != ClientRectangle))
                {
                    myBrush = new LinearGradientBrush(ClientRectangle, Color.Brown, Color.AliceBlue, LinearGradientMode.Horizontal);
                }
                pevent.Graphics.FillRectangle(myBrush, ClientRectangle);
                TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
                TextRenderer.DrawText(pevent.Graphics, Text, Font, ClientRectangle, ForeColor, flags);
            }
        }
    }
}
