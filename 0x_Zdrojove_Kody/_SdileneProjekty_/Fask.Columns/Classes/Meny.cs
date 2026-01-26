using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Meny
	{

		#region SQL script

		//create table [CZMST097]
		//(
		//    [mena_ID] nvarchar(10) not null,
		//    [mena_text] nvarchar(30) not null,
		//    [mena_hlavni] bit not null default 0,
		//    [mena_kurz] numeric,
		//    [mena_kurzDatum] datetime
		//);
		//alter table [CZMST097] add primary key ([mena_ID]);

		#endregion

		private  string TableName = "CZMST097";
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST097 = new Dictionary<string, ColumnType>();

		#region c'tor

		public Meny(DS_Information ds)
		{

            #region z XML souboru

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_090 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName.Trim());

            foreach (var dr in DT_090)
            {
                ColumnsInfo_CZMST097.AddIfNotExists(dr);
            }

            #endregion

		}

		#endregion

	}
}



