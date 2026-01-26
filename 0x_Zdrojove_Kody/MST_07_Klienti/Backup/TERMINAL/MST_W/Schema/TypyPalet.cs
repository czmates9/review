namespace Fask.MST_W.Schema {


    partial class TypyPalet
    {
        partial class PaletyDataTable
        {
        }
    
        partial class PaletyRow
        {
            public override string ToString()
            {
                //return base.ToString();
                return this.ID.Trim() + " : " + this.Name.Trim();
            }
        }
    }
}
