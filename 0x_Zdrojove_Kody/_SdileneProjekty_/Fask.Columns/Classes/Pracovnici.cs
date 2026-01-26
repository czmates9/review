using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Pracovnici
	{

		#region SQL script

		//create table [CZMST096]
		//(
		//    [prac_id] nvarchar(30) not null,
		//    [prac_desc] nvarchar(40),
		//    [prac_typ] nvarchar(3),
		//    [prac_carcode] nvarchar(21),
		//    [DEX_ROW_ID] int not null
		//);
		//create index IDX_CARCODE on [CZMST096] ([prac_carcode]);
		//create index IDX_DESC on [CZMST096] ([prac_desc]);
		//create index IDX_ID on [CZMST096] ([prac_id]);

		#endregion

		private  string TableName = "CZMST096";
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST096 = new Dictionary<string, ColumnType>();

		#region c'tor

		public Pracovnici(DS_Information ds)
		{
            #region z XML Souboru

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_1 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName.Trim());

            foreach (var dr in DT_1)
            {
                ColumnsInfo_CZMST096.AddIfNotExists(dr);
            }

            #endregion

		}

		#endregion

	}
}

