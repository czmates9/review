namespace Fask.SQLiteDBs.DataSets
{


    public partial class Ukoly
    {
        partial class CZ_UKOL_STATEDataTable
        {
        }

        public partial class CZ_UKOL_STATERow
        {
            public override string ToString()
            {
                return this.State.Trim() + " : " + this.Description.Trim();
            }
        }

        partial class CZ_UKOLDataTable
        {
        }
    }
}
