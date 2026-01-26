namespace Fask.Module.MTJ.MCL.VydejKontrola {
    public partial class VydejKontrolaDSLocal {        
    }
}

namespace Fask.Module.MTJ.MCL.VydejKontrola.VydejKontrolaDSLocalTableAdapters{
    public partial class fask_Vydej_KontrolaTableAdapter
    {
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
        }
    }
}
