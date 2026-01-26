using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fask.BO
{

    public class CZMST093
    {
        public CZMST093_row[] rows { get; set; }
    }

    public class CZMST093_row
    {
        public System.Data.DataRowState RowState { get; set; }

        public string skl_id { get; set; }

        public string skl_desc { get; set; }

        public string skl_typ { get; set; }

        public string skl_carcode { get; set; }

        public int DEX_ROW_ID { get; set; }

        
    }
}