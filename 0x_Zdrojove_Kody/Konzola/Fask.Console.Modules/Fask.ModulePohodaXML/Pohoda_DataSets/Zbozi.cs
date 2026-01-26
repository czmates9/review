namespace Fask.ModulePohodaXML.Pohoda_DataSets
{


    public partial class Zbozi
    {
    }
}


namespace Fask.ModulePohodaXML.Pohoda_DataSets.ZboziTableAdapters
{
    public partial class FASK_ZASOBYTableAdapter
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

    public partial class FASK_ZASOBY_PARAMETRYTableAdapter
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
