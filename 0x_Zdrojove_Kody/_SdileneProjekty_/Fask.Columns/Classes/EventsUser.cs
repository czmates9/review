using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class EventsUser
	{

		#region SQL script

		//create table [CZMST_EventsUser]
		//(
		//    [id] int identity not null,
		//    [eguid] uniqueidentifier not null,
		//    [eid] nvarchar(10) not null,
		//    [etype] nvarchar(10) not null,
		//    [etime] datetime not null,
		//    [termid] int not null,
		//    [userid] int not null,
		//    [loginid] nvarchar(10),
		//    [machineid] nvarchar(16),
		//    [modul] nvarchar(5),
		//    [countentries] int,
		//    [docnmbr] nvarchar(30),
		//    [itemnmbr] nvarchar(40),
		//    [REZ1] nvarchar(21),
		//    [REZ2] nvarchar(21)
		//);
		//alter table [CZMST_EventsUser] add primary key ([eguid]);

		#endregion

		private  string TableName = "CZMST_EventsUser";
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_EventsUser = new Dictionary<string, ColumnType>();

		#region c'tor

		public EventsUser(DS_Information ds)
		{

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_1 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName.Trim());

            foreach (var dr in DT_1)
            {
                ColumnsInfo_CZMST_EventsUser.AddIfNotExists(dr);
            }


		}

		#endregion

	}
}

