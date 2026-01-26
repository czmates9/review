using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class EventsTypes
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

		private static string TableName = "CZMST_EventsTypes";
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_EventsTypes = new Dictionary<string, ColumnType>();

			#region c'tor

			static EventsTypes()
			{
                //try
                //{
                //    DataSet ds = new DataSet();
                //    using (System.Data.SQLite.SQLiteDataAdapter a = new System.Data.SQLite.SQLiteDataAdapter())
                //    {
                //        using (var command = new System.Data.SQLite.SQLiteCommand(
                //            "Select * from information_schema.columns ",
                //            new System.Data.SQLite.SQLiteConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "EventsTypes.sdf"))
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
                //            ColumnsInfo_CZMST_EventsTypes.AddIfNotExists(dr);
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Logging.Log.Write(ex);
                //}
			}

			#endregion 

	}
}

