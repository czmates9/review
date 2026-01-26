using System.Drawing;

namespace Fask.MST_W.Vydej_3.PriorityColumns
{
    public class DataGrid2TextBoxColumn : Fask.Graphic.DataGrid2TextBoxColumn
    {
        private Brush bBrushOrig;
        private Brush bBrushAltOrig;
        protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight)
        {
            bBrushOrig = bBrush;
            bBrushAltOrig = bBrushAlt;

            System.Data.DataRowView drowview = source.List[rowNum] as System.Data.DataRowView;
            VydejService.Vydejky.HlavickyRow hlavicka = drowview.Row as VydejService.Vydejky.HlavickyRow;

            Config.PriorityColors.PriorityColorRow prow = null;
            if (hlavicka != null && !hlavicka.IsPRIORITYNull())
            {
                prow = MST_Global.PriorityColors.PriorityColor.FindByPriority(hlavicka.PRIORITY);
            }

            try
            {
                if (prow != null && !prow.IsColorNull() && !grid.IsSelected(rowNum))
                {
                    bBrush = new SolidBrush(Color.FromArgb(prow.Color));
                    bBrushAlt = bBrush;
                }

            }
            catch { }
            base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);

            bBrush = bBrushOrig;
            bBrushAlt = bBrushAltOrig;
        }
    }
}
