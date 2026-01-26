namespace Fask.ModulePohodaXML.Pohoda_DataSets
{


    public partial class SkladLokace
    {
    }
}


namespace Fask.ModulePohodaXML.Pohoda_DataSets.SkladLokaceTableAdapters
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
