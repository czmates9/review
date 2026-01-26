using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Konzola.Extensions
{
    public static class ComboBoxExtensions
    {
        public static void DropDownWitdhAutosize(this ComboBox cb)
        {
            if (cb == null)
                return;

            int maxWidth = 0;
            int temp = 0;
            Label label1 = new Label();

            foreach (var obj in cb.Items)
            {
                label1.Text = obj.ToString();
                temp = label1.PreferredWidth;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            label1.Dispose();
            //cb.Width = maxWidth;
            cb.DropDownWidth = maxWidth == 0 ? 170 : maxWidth;
    
        }
    }
}
