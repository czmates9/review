namespace Fask.Interfaces.DataSets
{
    public partial class Sklady
    {
        public partial class CZMST093Row
        {
            public override string ToString()
            {
                return this.Isskl_descNull() ? this.skl_id.Trim() : (this.skl_desc.Trim() + " (" + this.skl_id.Trim() + ")");
            }
        }
    }

    public partial class Sklady
    {
    }
}
