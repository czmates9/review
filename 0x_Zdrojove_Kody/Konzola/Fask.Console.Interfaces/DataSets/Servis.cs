namespace Fask.Interfaces.DataSets
{


    public partial class Servis
    {
        partial class CZMST_Servis_OkruhRow
        {
            public override string ToString()
            {
                return this.Oznaceni.Trim() + " (" + this.ID.Trim() + ")";
            }
        }

        public partial class CZMST_Servis_ZdrojRow
        {
            public override string ToString()
            {
                return this.Oznaceni.Trim() + " (" + this.ID.Trim() + ")";
            }
        }

        public partial class CZMST_Servis_StavRow
        {
            public override string ToString()
            {
                return this.Oznaceni.Trim() + " (" + this.ID.Trim() + ")";
            }
        }

        public partial class CZMST_Servis_CinnostRow
        {
            public override string ToString()
            {
                return this.Oznaceni.Trim() + " (" + this.ID.Trim() + ")";
            }
        }
    }
}
