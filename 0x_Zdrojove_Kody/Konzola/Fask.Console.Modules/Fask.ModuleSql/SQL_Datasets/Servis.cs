namespace Fask.ModuleSql.SQL_Datasets
{


    public partial class Servis
    {
    }
}

namespace Fask.ModuleSql.SQL_Datasets.ServisTableAdapters
{
    public partial class CZMST_Servis_OkruhTableAdapter
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

    public partial class CZMST_Servis_ZdrojSeznamTableAdapter
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

    public partial class CZMST_Servis_ZdrojStavTableAdapter
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

    public partial class CZMST_Servis_Dynamic_Table_DefinitionTableAdapter
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