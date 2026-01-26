using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Lokace
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

		private static string TableName_SLLT = "CZMST_SkladLokace_LokaceTypy";
		private static string TableName_094 = "CZMST094";

		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_SkladLokace_LokaceTypy = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST094 = new Dictionary<string, ColumnType>();

		#region c'tor

		static Lokace()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Lokace.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_SLLT.Trim())
            //        {
            //            ColumnsInfo_CZMST_SkladLokace_LokaceTypy.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_094.Trim())
            //        {
            //            ColumnsInfo_CZMST094.AddIfNotExists(dr);
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


