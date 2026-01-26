namespace Fask.ModuleSql.SQL_Datasets
{


    public partial class Vydej
    {
    }
}

namespace Fask.ModuleSql.SQL_Datasets.VydejTableAdapters
{
    public partial class CZMST_SETableAdapter
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