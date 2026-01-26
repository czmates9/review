using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Fask.Graphic;

namespace Fask.Graphic
{
    public class DataGrid2NumberBoxColumn : DataGrid2TextBoxColumnBase
    {
        public DataGrid2NumberBoxColumn()
            : base()
        {
        }

        StringFormat _sf = new StringFormat();
        /// <summary>
        /// Horizontalni zarovnani
        /// </summary>
        public System.Drawing.StringAlignment Alignment
        {
            get { return _sf.Alignment; }
            set { _sf.Alignment = value; }
        }
        /// <summary>
        /// Vertikalni zarovnani
        /// </summary>
        public System.Drawing.StringAlignment LineAlignment
        {
            get { return _sf.LineAlignment; }
            set { _sf.LineAlignment = value; }
        }

        [System.ComponentModel.DefaultValue(false)]
        public bool SelectionShow { get; set; }

        /// <summary>
        /// Format cisla: defaultne "N" - dle nastaveni systemu
        /// </summary>
        [System.ComponentModel.DefaultValue("N")]
        new public string Format
        {
            get { return base.Format; }
            set { base.Format = value; }
        }

        private DataGrid grid = null;
        public DataGrid Grid
        {
            set
            {
                try
                {
                    grid = value;
                    bBrush = new SolidBrush(grid.BackColor);
                    fBrush = new SolidBrush(grid.ForeColor);
                    bBrushAlt = new SolidBrush(((DataGrid2)grid).BackColorAlternating);
                    //fBrushAlt = new SolidBrush(((DataGrid2)grid).ForeColorAlternating);
                }
                catch
                {
                    bBrushAlt = bBrush;
                    //fBrushAlt = fBrush;
                }
            }
        }

        private Brush bBrush = null;
        private Brush bBrushAlt = null;
        private Brush fBrush = null;
        //private Brush fBrushAlt = null;

        protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight)
        {
            //if (!this.SelectionShow && grid != null)
            //{
            //    if (grid.IsSelected(rowNum))
            //        base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
            //    else
            //        base.Paint(g, bounds, source, rowNum, bBrush, fBrush, alignToRight);
            //}
            //else
            //{
            //    base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
            //}
            bool alt = rowNum % 2 == 0;
            if (!this.SelectionShow && grid != null)
            {
                if (grid.IsSelected(rowNum))
                    this.OwnPaint(g, bounds, source, rowNum, backBrush, foreBrush);
                else
                    this.OwnPaint(g, bounds, source, rowNum, alt ? bBrush : bBrushAlt, fBrush);
                    //this.OwnPaint(g, bounds, source, rowNum, odd ? bBrush : bBrushAlt, odd ? fBrush : fBrushAlt);
            }
            else
            {
                //this.OwnPaint(g, bounds, source, rowNum, backBrush, foreBrush);
                base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
            }
        }
        

        private void OwnPaint(System.Drawing.Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush)
        {
            DataGrid2 dgOwner = this.grid as DataGrid2;
            if (dgOwner != null)
            {
                g.FillRectangle(backBrush, bounds);
                bounds.Inflate(-dgOwner.DefaultTextInset, -dgOwner.DefaultTextInset);
                RectangleF layoutRectangle = new RectangleF((float)bounds.X, (float)bounds.Y, (float)bounds.Width, (float)bounds.Height);

                //string s = (string)Formatter.FormatObject(this.PropertyDescriptor.GetValue(source.List[rowNum]), typeof(string), null, null, this.Format, this.FormatInfo, this.NullText, DBNull.Value);
                //Type type = System.Reflection.Assembly.GetExecutingAssembly().GetType("System.Windows.Forms.Formatter");
                //Type type = Type.GetType("System.Windows.Forms.Formatter", false, true);
                //System.Reflection.MethodInfo minfo = type.GetMethod("FormatObject", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.NonPublic);
                //object o = minfo.Invoke(null, new object[] { this.PropertyDescriptor.GetValue(source.List[rowNum]), typeof(string), null, null, this.Format, this.FormatInfo, this.NullText, DBNull.Value });
                //string s = (string)o;
                string s = (string)Formatter.FormatObject(this.PropertyDescriptor.GetValue(source.List[rowNum]), typeof(string), null, null, this.Format, this.FormatInfo, this.NullText, DBNull.Value);
                g.DrawString(s, dgOwner.Font, foreBrush, layoutRectangle, _sf);
            }
        }

    }
}
