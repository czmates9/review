namespace Fask.ModuleSql.SQL_Datasets
{


    public partial class Sklady
    {
    }
}

namespace Fask.ModuleSql.SQL_Datasets.SkladyTableAdapters
{
    public partial class CZMST093TableAdapter
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