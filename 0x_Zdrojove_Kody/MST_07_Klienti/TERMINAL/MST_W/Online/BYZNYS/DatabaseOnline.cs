
using System.Data;
using System.Data.SqlClient;
namespace MST_W_RF.Schema.DatabaseOnlinePrijemTableAdapters
{
   

}

namespace MST_W_RF.Schema {
    
    
}
namespace Fask.MST_W.Online.BYZNYS {    
    public partial class DatabaseOnline {
        public partial class JEDNOTKYRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.ZKRATKAMJ.Trim() + ":" + this.NAZEV_MJ.Trim();
            }
        }
        public partial class SKLADRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.CISLO_MAT + ":" + this.NAZEV_MAT;
            }
        }
    }
}
