namespace Fask.ModulePohodaXML.Pohoda_DataSets
{


    public partial class Uzivatele
    {
    }
}

namespace Fask.ModulePohodaXML.Pohoda_DataSets.UzivateleTableAdapters
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
