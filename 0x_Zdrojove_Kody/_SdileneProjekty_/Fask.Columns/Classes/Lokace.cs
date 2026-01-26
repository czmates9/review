using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Lokace
	{

		#region SQL script

		//create table [CZMST_SkladLokace_LokaceTypy]
		//(
		//    [TYPE] nvarchar(2),
		//    [Description] nvarchar(50),
		//    [IS_RECEIVE] tinyint not null,
		//    [IS_DEFAULT] tinyint not null,
		//    [IS_NORMAL] tinyint not null
		//);

		//create table [CZMST094]
		//(
		//    [SKL_ID] nvarchar(20),
		//    [LOCNCODE] nvarchar(11) not null,
		//    [TYPE] nvarchar(2),
		//    [DEX_ROW_ID] int,
		//    [Description] nvarchar(40),
		//    [Barcode] nvarchar(21)
		//);
		//create index IX_skl_id on [CZMST094] ([SKL_ID]);

		#endregion

		private  string TableName_SLLT = "CZMST_SkladLokace_LokaceTypy";
		private  string TableName_094 = "CZMST094";

		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_SkladLokace_LokaceTypy = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST094 = new Dictionary<string, ColumnType>();

		#region c'tor

		public Lokace(DS_Information ds)
		{
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_1 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_SLLT.Trim());

            foreach (var dr in DT_1)
            {
                ColumnsInfo_CZMST_SkladLokace_LokaceTypy.AddIfNotExists(dr);
            }

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_2 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_094.Trim());

            foreach (var dr in DT_2)
            {
                ColumnsInfo_CZMST094.AddIfNotExists(dr);
            }

		}

		#endregion

	}
}


