using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Strediska
	{

		#region SQL script
		//create table [CZMST091]
		//(
		//    [str_id] nvarchar(30) not null,
		//    [str_desc] nvarchar(40),
		//    [str_typ] nvarchar(3),
		//    [str_carcode] nvarchar(21),
		//    [DEX_ROW_ID] int not null
		//);
		//create index IDX_CARCODE on [CZMST091] ([str_carcode]);
		//create index IDX_DESC on [CZMST091] ([str_desc]);
		//create index IDX_ID on [CZMST091] ([str_id]);
		#endregion

		private static string TableName = "CZMST091";
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST091 = new Dictionary<string, ColumnType>();

		#region c'tor

		static Strediska()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Strediska.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName.Trim())
            //        {
            //            ColumnsInfo_CZMST091.AddIfNotExists(dr);
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

