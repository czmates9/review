


using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Tiskarny
	{

		#region SQL script

		//create table [CZMST_TISKARNA]
		//(
		//    [ID] int not null,
		//    [NAME] nvarchar(30) not null,
		//    [LOCATION] nvarchar(50),
		//    [IP] nvarchar(64),
		//    [PORT] nvarchar(10),
		//    [COM] nvarchar(50),
		//    [SOUBOR] ntext,
		//    [TIMEOUT] int,
		//    [BARCODE] nvarchar(50),
		//    [DEFAULT] bit default 0
		//);
		//alter table [CZMST_TISKARNA] add primary key ([ID]);
		//create unique index UQ__CZMST_TISKARNA__0000000000000036 on [CZMST_TISKARNA] ([NAME]);

		#endregion

		private static string TableName = "CZMST_TISKARNA";
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_TISKARNA = new Dictionary<string, ColumnType>();

		#region c'tor

		static Tiskarny()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Tiskarny.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName.Trim())
            //        {
            //            ColumnsInfo_CZMST_TISKARNA.AddIfNotExists(dr);
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

