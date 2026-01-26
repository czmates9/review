namespace Fask.Console.Interfaces.DataSets {
    public partial class Uzivatele { 
        public partial class CZMSTPWDRow
        {
            public override string ToString()
            {
                return this.SECONDNAME.Trim() + " " + this.FIRSTNAME.Trim() + " (" + this.LOGIN.Trim() + ")";
            }
        }
    }
}
