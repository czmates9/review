using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Sklady
	{

		#region SQL script

		//create table [CZMST093]
		//(
		//    [skl_id] nvarchar(20) not null,
		//    [skl_desc] nvarchar(40),
		//    [skl_typ] nvarchar(3),
		//    [skl_carcode] nvarchar(21),
		//    [DEX_ROW_ID] int not null
		//);

		#endregion

		private static string TableName = "CZMST093";
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST093 = new Dictionary<string, ColumnType>();

		#region c'tor

		static Sklady()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Sklady.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName.Trim())
            //        {
            //            ColumnsInfo_CZMST093.AddIfNotExists(dr);
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


