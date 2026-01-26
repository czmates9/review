namespace Fask.Interfaces.DataSets
{
}
namespace Fask.Interfaces.DataSets
{
}
namespace Fask.Interfaces.DataSets
{


    public partial class Ukolovani
    {
        partial class CZ_UKOL_UZIVDataTable
        {
        }

        //public partial class CZMSTPWDRow
        //{
        //    public override string ToString()
        //    {
        //        //return base.ToString();
        //        return this.SECONDNAME.Trim() + " " + this.FIRSTNAME.Trim() + " (" + this.LOGIN.Trim() + ")";
        //    }
        //}

        public partial class CZ_UKOL_STATERow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.Description.Trim() + " (" + this.State.Trim() + ")";
            }
        }

        public partial class CZ_UKOLRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.Name.Trim() + " (" + this.Description.Trim() + ")";
            }
        }
    }


}

