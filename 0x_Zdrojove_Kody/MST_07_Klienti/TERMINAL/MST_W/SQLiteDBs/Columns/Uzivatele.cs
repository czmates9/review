


using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Uzivatele
	{

		#region SQL script

		//create table [Users]
		//(
		//    [ID] int not null,
		//    [Login] nvarchar(100) not null,
		//    [Pwd] nvarchar(128),
		//    [Hash] nvarchar(128),
		//    [EAN] nvarchar(50),
		//    [FIRSTNAME] nvarchar(15),
		//    [SECONDNAME] nvarchar(40)
		//);
		//alter table [Users] add primary key ([ID]);
		//create unique index UQ__Users__0000000000000010 on [Users] ([Login]);

		#endregion

		private static string TableName = "Users";
		public static Dictionary<string, ColumnType> ColumnsInfo_Users = new Dictionary<string, ColumnType>();

		#region c'tor

		static Uzivatele()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Uzivatele.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName.Trim())
            //        {
            //            ColumnsInfo_Users.AddIfNotExists(dr);
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

