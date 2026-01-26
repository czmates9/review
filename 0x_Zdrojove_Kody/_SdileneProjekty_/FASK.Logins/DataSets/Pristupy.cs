namespace FASK.Logins.DataSets
{


    public partial class Pristupy
    {

        public partial class FASK_LoginsRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.surname.Trim() + " " + this.firstname.Trim() + " (" + this.USERID.Trim() + ")";
            }
        }
    }
}
