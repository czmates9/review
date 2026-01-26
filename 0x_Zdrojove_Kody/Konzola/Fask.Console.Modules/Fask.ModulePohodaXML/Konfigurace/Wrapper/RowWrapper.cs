using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fask.ModulePohodaXML
{
    [TypeConverter(typeof(RowWrapperConverter))]
    public class RowWrapper
    {
        public string Category = string.Empty;

        private readonly List<string> exclude = new List<string>();
        public List<string> Exclude { get { return exclude; } }
        private readonly DataRowView rowView;
        public RowWrapper(DataRow row, string Category)
        {
            this.Category = Category;
            DataView view = new DataView(row.Table);
            foreach (DataRowView tmp in view)
            {
                if (tmp.Row == row)
                {
                    rowView = tmp;
                    break;
                }
            }
        }
        
        public DataRowView GetRowView()
        {
            return this.rowView;
        }

        public DataRowView GetRowView(object rw)
        {
            return ((RowWrapper)rw).rowView;
        }
    }

}
