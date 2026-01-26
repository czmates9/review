namespace Fask.SQLiteDBs.DataSets
{
}

namespace Fask.SQLiteDBs.DataSets
{


    public partial class Vyroba
    {
        public partial class LoginsRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return (this.firstname.Trim() + " " + this.surname.Trim()).Trim();
            }
        }
    }
}
