namespace Fask.MST_W.Vydej_3.RFID {
        
    public partial class DsRFID {
    }
}

namespace Fask.MST_W.Vydej_3.RFID.DsRFIDTableAdapters
{
    //public class All
    //{
    //    RFID.DsRFIDTableAdapters.PredlohaTableAdapter a;
    //    DsRFIDTableAdapters.RfidTableAdapter a;
    //}

    public partial class PredlohaTableAdapter
    {
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
        }
    }

    public partial class RfidTableAdapter
    {
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
        }
    }
}



