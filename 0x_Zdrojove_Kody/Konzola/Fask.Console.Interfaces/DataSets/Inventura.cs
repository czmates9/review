namespace Fask.Interfaces.DataSets
{


    public partial class Inventura
    {
        partial class CZMST_I1_PredlohaDataTable
        {
        }

        public partial class CZMST_I1HRow
        {
            public override string ToString()
            {
                return this.CountEntries.ToString() + (this.IsDescriptionNull() ? string.Empty : (" (" + this.Description.Trim() + ")"));
            }
        }
    }
}
