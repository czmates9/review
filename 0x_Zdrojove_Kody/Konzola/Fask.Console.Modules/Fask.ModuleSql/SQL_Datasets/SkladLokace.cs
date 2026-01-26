namespace Fask.ModuleSql.SQL_Datasets
{


    public partial class SkladLokace
    {
    }
}

namespace Fask.ModuleSql.SQL_Datasets.SkladLokaceTableAdapters
{
    public partial class CZMST_SkladLokace_MapaTableAdapter
    {
        public global::System.Data.SqlClient.SqlTransaction MyTransaction
        {
            get { return this.Transaction; }
            set
            {
                this.Transaction = value;
                //this.Adapter.SelectCommand.Transaction = value;
            }
        }
    }

    public partial class CZMST_SkladLokace_LokaceTypyTableAdapter
    {
        public global::System.Data.SqlClient.SqlTransaction MyTransaction
        {
            get { return this.Transaction; }
            set
            {
                this.Transaction = value;
                //this.Adapter.SelectCommand.Transaction = value;
            }
        }
    }

    public partial class CZMST_SkladLokace_LokaceVariantySortimentTableAdapter
    {
        public global::System.Data.SqlClient.SqlTransaction MyTransaction
        {
            get { return this.Transaction; }
            set
            {
                this.Transaction = value;
                //this.Adapter.SelectCommand.Transaction = value;
            }
        }
    }
}