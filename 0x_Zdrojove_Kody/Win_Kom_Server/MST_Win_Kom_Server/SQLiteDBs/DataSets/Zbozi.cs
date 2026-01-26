namespace Fask.MST_W_Server.SQLiteDBs.DataSets
{


    public partial class Zbozi
    {
    }
}

namespace Fask.MST_W_Server.SQLiteDBs.DataSets.ZboziTableAdapters
{
	public partial class CZMST095TableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}

	public partial class CZMST095MTableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}
}
