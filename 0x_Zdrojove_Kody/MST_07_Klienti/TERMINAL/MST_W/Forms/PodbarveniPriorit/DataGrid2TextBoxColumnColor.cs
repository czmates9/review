using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Fask.MST_W.Forms.PodbarveniPriorit
{
    public class DataGrid2TextBoxColumnColor : Fask.Graphic.DataGrid2TextBoxColumn
    {
        protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight)
        {
            System.Data.DataRowView drowview = source.List[rowNum] as System.Data.DataRowView;
            Config.PriorityColors.PriorityColorRow priorityRow = drowview.Row as Config.PriorityColors.PriorityColorRow;
            
            backBrush = new SolidBrush(Color.FromArgb(priorityRow.Color));
            this.OwnPaint(g, bounds, source, rowNum, backBrush, foreBrush);
        }
    }
}
