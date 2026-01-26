namespace Fask.Interfaces.DataSets
{


    public partial class Odberatele
    {
        partial class CZMST090DataTable
        {
        }

        public partial class CZMST090Row
        {
            public override string ToString()
            {
                return (this.Isodb_descNull() ? string.Empty : this.odb_desc.Trim()) + " (" + this.odb_id.Trim() + ")";
            }
        }
    }
}
