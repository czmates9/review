namespace Fask.ModuleSql.SQL_Datasets
{


    public partial class Uzivatele
    {
    }
}

namespace Fask.ModuleSql.SQL_Datasets.UzivateleTableAdapters
{
    public partial class CZMSTPWDTableAdapter
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