namespace Fask.Interfaces.DataSets
{


    public partial class Zbozi
    {
        partial class FASK_ZASOBY_ALL_KONZOLADataTable
        {
        }


        public partial class FASK_ZASOBY_ALL_KONZOLARow
        {
            public override string ToString()
            {
                return this.ITEMDESC.Trim() + " (" + this.ITEMNMBR.Trim() + ")";
            }
        }

        partial class FASK_ZASOBY_KONZOLARow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.ITEMDESC.Trim() + " (" + this.ITEMNMBR.Trim() + ")";
            }
        }
    }
}
