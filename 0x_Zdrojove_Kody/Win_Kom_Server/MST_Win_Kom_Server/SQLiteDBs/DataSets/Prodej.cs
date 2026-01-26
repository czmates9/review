namespace Fask.MST_W_Server.SQLiteDBs.DataSets
{


	public partial class Prodej
	{
	}
}

namespace Fask.MST_W_Server.SQLiteDBs.DataSets.ProdejTableAdapters
{
	public partial class CZMST_DEHTableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}

	public partial class CZMST_DITableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}

	public partial class CZMST_DIHTableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}

	public partial class CZMST_DI_RFIDTableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}
}