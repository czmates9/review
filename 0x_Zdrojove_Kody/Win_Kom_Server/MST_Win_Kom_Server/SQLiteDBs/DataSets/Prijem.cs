namespace Fask.MST_W_Server.SQLiteDBs.DataSets
{


    public partial class Prijem
    {
    }
}

namespace Fask.MST_W_Server.SQLiteDBs.DataSets.PrijemTableAdapters
{
	public partial class CZMST_PETableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}

	public partial class CZMST_PI_FTableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}

	public partial class CZMST_PIHTableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}

	public partial class CZMST_PE_SNTableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}

	public partial class CZMST_PEHTableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}

	public partial class ParametryTableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}

	public partial class CZMST_PITableAdapter
	{
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			SQLite_Classes.SQLite_Common.DisposeTableAdapter(disposing, this._adapter, this._commandCollection);
		}
	}
}