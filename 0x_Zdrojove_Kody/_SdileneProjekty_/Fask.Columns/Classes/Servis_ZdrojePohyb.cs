


using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Servis_ZdrojePohyb
	{

		#region SQL script

		//create table [CZMST_Servis_ZdrojPohyb]
		//(
		//    [IDZdroj] nvarchar(20) not null,
		//    [IDStav] nvarchar(20) not null,
		//    [IDCinnost] nvarchar(20),
		//    [Modified] datetime not null,
		//    [IDTerminal] int not null,
		//    [IDUser] int not null,
		//    [GUID] uniqueidentifier not null,
		//    [CinnostValue] nvarchar(50),
		//    [CinnostType] nvarchar(2),
		//    [CountEntries] int,
		//    [ODB_ID] nvarchar(12),
		//    [OkruhID] nvarchar(20),
		//    [CinnostOznaceni] nvarchar(50),
		//    [GPS_X] float,
		//    [GPS_Y] float,
		//    [GPS_Z] int
		//);

		#endregion

		private  string TableName = "CZMST_Servis_ZdrojPohyb";
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_ZdrojPohyb = new Dictionary<string, ColumnType>();

		#region c'tor

		public Servis_ZdrojePohyb(DS_Information ds)
		{
            #region z XML souboru

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_1 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName.Trim());

            foreach (var dr in DT_1)
            {
                ColumnsInfo_CZMST_Servis_ZdrojPohyb.AddIfNotExists(dr);
            }

            #endregion
		}

		#endregion

	}
}

