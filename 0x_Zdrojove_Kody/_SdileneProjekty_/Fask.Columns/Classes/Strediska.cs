using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Strediska
	{

		#region SQL script
		//create table [CZMST091]
		//(
		//    [str_id] nvarchar(30) not null,
		//    [str_desc] nvarchar(40),
		//    [str_typ] nvarchar(3),
		//    [str_carcode] nvarchar(21),
		//    [DEX_ROW_ID] int not null
		//);
		//create index IDX_CARCODE on [CZMST091] ([str_carcode]);
		//create index IDX_DESC on [CZMST091] ([str_desc]);
		//create index IDX_ID on [CZMST091] ([str_id]);
		#endregion

		private  string TableName = "CZMST091";
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST091 = new Dictionary<string, ColumnType>();

		#region c'tor

		public Strediska(DS_Information ds)
		{

			try
			{

                EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_091 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName.Trim());

                foreach (var dr in DT_091)
                {
                    ColumnsInfo_CZMST091.AddIfNotExists(dr);
                }

			}
			catch //(Exception ex)
			{
				//Fask.Logging.ExceptionHandler2.HandleErrorLog(ex);
			}
		}

		#endregion

	}
}

