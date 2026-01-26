using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class EventsUser
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

		private static string TableName = "CZMST_EventsUser";
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_EventsUser = new Dictionary<string, ColumnType>();

		#region c'tor

        static EventsUser()
        {
            //try
            //{
            //    DataSet ds = new DataSet();
            //    using (System.Data.SQLite.SQLiteDataAdapter a = new System.Data.SQLite.SQLiteDataAdapter())
            //    {
            //        using (var command = new System.Data.SQLite.SQLiteCommand(
            //            "Select * from information_schema.columns ",
            //            //new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "EventsUser.sdf"))
            //            new System.Data.SQLite.SQLiteConnection(Main.SQLiteConnectionStringFormat(Main.EventsUserDBData))
            //        ))
            //        {
            //            a.SelectCommand = command;
            //            a.Fill(ds);
            //        }
            //    }
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName.Trim())
            //        {
            //            ColumnsInfo_CZMST_EventsUser.AddIfNotExists(dr);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logging.Log.Write(ex);
            //}
            //finally
            //{
            //}
        }

		#endregion

	}
}

