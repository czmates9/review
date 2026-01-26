using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.VydejService
{
    public partial class Vydejky
    {
        public void ReadXmlSchemaDynamic(string filename)
        {
            this.Reset();
            global::System.Data.DataSet ds = new global::System.Data.DataSet();
            ds.ReadXmlSchema(filename);
            if ((ds.Tables["Hlavicky"] != null))
            {
                this.Tables.Add(new HlavickyDataTable(ds.Tables["Hlavicky"]));
            }
            this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
    }
}
