using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Pracovnici
	{

		#region SQL script

		//create table [CZMST096]
		//(
		//    [prac_id] nvarchar(30) not null,
		//    [prac_desc] nvarchar(40),
		//    [prac_typ] nvarchar(3),
		//    [prac_carcode] nvarchar(21),
		//    [DEX_ROW_ID] int not null
		//);
		//create index IDX_CARCODE on [CZMST096] ([prac_carcode]);
		//create index IDX_DESC on [CZMST096] ([prac_desc]);
		//create index IDX_ID on [CZMST096] ([prac_id]);

		#endregion

		private static string TableName = "CZMST096";
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST096 = new Dictionary<string, ColumnType>();

		#region c'tor

		static Pracovnici()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Pracovnici.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName.Trim())
            //        {
            //            ColumnsInfo_CZMST096.AddIfNotExists(dr);
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

