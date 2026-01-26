


using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Servis_ZdrojePohyb
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

		private static string TableName = "CZMST_Servis_ZdrojPohyb";
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_Servis_ZdrojPohyb = new Dictionary<string, ColumnType>();

		#region c'tor

		static Servis_ZdrojePohyb()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Servis_ZdrojePohyb.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName.Trim())
            //        {
            //            ColumnsInfo_CZMST_Servis_ZdrojPohyb.AddIfNotExists(dr);
            //            continue;
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

