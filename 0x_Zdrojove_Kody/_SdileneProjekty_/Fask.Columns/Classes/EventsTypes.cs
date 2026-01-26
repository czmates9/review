using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class EventsTypes
	{

		#region SQL script

		//create table [CZMST_EventsTypes]
		//(
		//    [eid] nvarchar(10) not null,
		//    [etype] nvarchar(10) not null,
		//    [edesc] nvarchar(100),
		//    [ebarcode] nvarchar(21)
		//);
		
		#endregion

		private  string TableName = "CZMST_EventsTypes";
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_EventsTypes = new Dictionary<string, ColumnType>();

			#region c'tor

		public EventsTypes(DS_Information ds)
			{
				#region z XML souboru

                EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName.Trim());

                foreach (var dr in DT)
                {
                    ColumnsInfo_CZMST_EventsTypes.AddIfNotExists(dr);
                }			

				
				#endregion
			}

			#endregion 

	}
}

