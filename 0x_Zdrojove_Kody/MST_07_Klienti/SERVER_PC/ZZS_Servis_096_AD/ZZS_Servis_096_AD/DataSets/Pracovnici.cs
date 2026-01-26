namespace ZZS_Servis_096_AD.DataSets {
    
    
    public partial class Pracovnici {
    }
}

namespace ZZS_Servis_096_AD.DataSets.PracovniciTableAdapters
{
    public partial class CZMST096TableAdapter
    {
        public global::System.Data.SqlClient.SqlTransaction MyTransaction
        {
            get { return this.Transaction; }
            set
            {
                this.Transaction = value;
            }
        }
    }
}