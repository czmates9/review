using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{

	public class Zbozi
	{


		private string TableName = "FASK_ZASOBY";
		private string TableNameM = "FASK_ZASOBY_MENY";
		private string TableNameP = "FASK_ZASOBY_PARAMETRY";

		public Dictionary<string, ColumnType> ColumnsInfo_FASK_ZASOBY = new Dictionary<string, ColumnType>();
		public Dictionary<string, ColumnType> ColumnsInfo_FASK_ZASOBY_MENY = new Dictionary<string, ColumnType>();
		public Dictionary<string, ColumnType> ColumnsInfo_FASK_ZASOBY_PARAMETRY = new Dictionary<string, ColumnType>();

		#region c'tor

		public Zbozi(DS_Information ds)
		{

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_FASK_ZASOBY = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName.Trim());

            foreach (var dr in DT_FASK_ZASOBY)
			{
				ColumnsInfo_FASK_ZASOBY.AddIfNotExists(dr);
			}

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_FASK_ZASOBYM = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableNameM.Trim());

            foreach (var dr in DT_FASK_ZASOBYM)
			{
				ColumnsInfo_FASK_ZASOBY_MENY.AddIfNotExists(dr);
			}

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_FASK_ZASOBYP = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableNameP.Trim());

            foreach (var dr in DT_FASK_ZASOBYP)
			{
				ColumnsInfo_FASK_ZASOBY_PARAMETRY.AddIfNotExists(dr);
			}

		}

		#endregion

	}


}

