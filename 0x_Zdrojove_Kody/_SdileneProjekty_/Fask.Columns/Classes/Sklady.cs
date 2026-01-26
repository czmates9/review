using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Sklady
	{

		#region SQL script

		//create table [CZMST093]
		//(
		//    [skl_id] nvarchar(20) not null,
		//    [skl_desc] nvarchar(40),
		//    [skl_typ] nvarchar(3),
		//    [skl_carcode] nvarchar(21),
		//    [DEX_ROW_ID] int not null
		//);

		#endregion

		private  string TableName = "CZMST093";
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST093 = new Dictionary<string, ColumnType>();

		#region c'tor

		public Sklady(DS_Information ds)
		{

			try
			{
                EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_093 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName.Trim());

                foreach (var dr in DT_093)
                {
                    ColumnsInfo_CZMST093.AddIfNotExists(dr);
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


