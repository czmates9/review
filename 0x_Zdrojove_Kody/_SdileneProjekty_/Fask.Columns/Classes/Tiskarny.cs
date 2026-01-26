


using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Tiskarny
	{

		#region SQL script

		//create table [CZMST_TISKARNA]
		//(
		//    [ID] int not null,
		//    [NAME] nvarchar(30) not null,
		//    [LOCATION] nvarchar(50),
		//    [IP] nvarchar(64),
		//    [PORT] nvarchar(10),
		//    [COM] nvarchar(50),
		//    [SOUBOR] ntext,
		//    [TIMEOUT] int,
		//    [BARCODE] nvarchar(50),
		//    [DEFAULT] bit default 0
		//);
		//alter table [CZMST_TISKARNA] add primary key ([ID]);
		//create unique index UQ__CZMST_TISKARNA__0000000000000036 on [CZMST_TISKARNA] ([NAME]);

		#endregion

		private  string TableName = "CZMST_TISKARNA";
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_TISKARNA = new Dictionary<string, ColumnType>();

		#region c'tor

		public Tiskarny(DS_Information ds)
		{


            #region z XML souboru

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_tisk = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName.Trim());

            foreach (var dr in DT_tisk)
            {
                ColumnsInfo_CZMST_TISKARNA.AddIfNotExists(dr);
            }

            #endregion


		}

		#endregion

	}
}

