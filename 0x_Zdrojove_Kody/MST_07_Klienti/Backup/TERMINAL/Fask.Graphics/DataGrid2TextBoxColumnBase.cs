using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.Graphic
{
    public class DataGrid2TextBoxColumnBase : System.Windows.Forms.DataGridTextBoxColumn
    {        
        private string _tag = string.Empty;
        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
    }
}
