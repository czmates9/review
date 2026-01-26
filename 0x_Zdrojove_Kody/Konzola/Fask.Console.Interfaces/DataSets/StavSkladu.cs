namespace Fask.Interfaces.DataSets
{


    public partial class StavSkladu
    {
        public partial class CZMSTPWDRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.SECONDNAME.Trim() + " (" + this.LOGIN.Trim() + ")";
            }
        }

        public partial class CZMST093Row
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.skl_desc.Trim() + " (" + this.skl_id.Trim() + ")";
            }
        }
    }
}
