namespace Fask.Module.Zbozi.DataSets {
    public partial class Sklady {        
    }
}

namespace Fask.Module.Zbozi.DataSets.SkladyTableAdapters
{
    public partial class CZMST093TableAdapter
    {
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
        }
    }
}
