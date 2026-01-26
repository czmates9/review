using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Definition_SQL_Struncture.Dokumentace.Generate_HTML
{
    public class Row_Table_View_List
    {
        private Cell _table_View_Name = null;
        public Cell Table_View_Name
        {
            get { return _table_View_Name; }
            set { _table_View_Name = value; }
        }

        private Cell _table_View_Desc = null;
        public Cell Table_View_Desc
        {
            get { return _table_View_Desc; }
            set { _table_View_Desc = value; }
        }

        private Cell _table_View_Typ = null;
        public Cell Table_View_Typ
        {
            get { return _table_View_Typ; }
            set { _table_View_Typ = value; }
        }
    }
}
