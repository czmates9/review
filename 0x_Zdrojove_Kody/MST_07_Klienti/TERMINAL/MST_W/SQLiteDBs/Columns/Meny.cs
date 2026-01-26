using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Meny
	{

		#region SQL script

		//create table [CZMST097]
		//(
		//    [mena_ID] nvarchar(10) not null,
		//    [mena_text] nvarchar(30) not null,
		//    [mena_hlavni] bit not null default 0,
		//    [mena_kurz] numeric,
		//    [mena_kurzDatum] datetime
		//);
		//alter table [CZMST097] add primary key ([mena_ID]);

		#endregion

		private static string TableName = "CZMST097";
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST097 = new Dictionary<string, ColumnType>();

		#region c'tor

		static Meny()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Meny.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName.Trim())
            //        {
            //            ColumnsInfo_CZMST097.AddIfNotExists(dr);
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



