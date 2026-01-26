namespace Fask.ModulePohodaXML.Pohoda_DataSets
{


    public partial class Vydej
    {
    }
}



namespace Fask.ModulePohodaXML.Pohoda_DataSets.VydejTableAdapters
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