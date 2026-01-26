namespace Fask.Module.Zbozi.DataSets {
    public partial class Zbozi {        
    }
}


namespace Fask.Module.Zbozi.DataSets.ZboziTableAdapters {
    public partial class CZMST095TableAdapter
    {
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
        }
    }
}