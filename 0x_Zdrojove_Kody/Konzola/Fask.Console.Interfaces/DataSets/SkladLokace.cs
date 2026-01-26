namespace Fask.Interfaces.DataSets
{
    public partial class SkladLokace
    {
        public partial class CZMST_SkladLokace_LokaceTypyRow
        {
            public override string ToString()
            {
                return (this.IsDescriptionNull() ? string.Empty : this.Description.Trim()) + " (" + (this.IsTYPENull() ? string.Empty : this.TYPE.Trim()) + ")";
            }
        }
    }
}
