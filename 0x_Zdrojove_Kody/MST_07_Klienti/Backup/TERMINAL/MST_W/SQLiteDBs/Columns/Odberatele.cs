using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Odberatele
	{

		#region SQL script

		//create table [CZMST090]
		//(
		//    [odb_id] nvarchar(12) not null,
		//    [odb_desc] nvarchar(31),
		//    [odb_typ] nvarchar(3),
		//    [odb_carcode] nvarchar(21),
		//    [DEX_ROW_ID] int not null,
		//    [odb_ico] nvarchar(20),
		//    [mena_ID] nvarchar(10),
		//    [odb_misto] nvarchar(100) default '',
		//    [odb_ulice] nvarchar(100) default '',
		//    [odb_cisloOr] nvarchar(15) default '',
		//    [odb_psc] nvarchar(15) default '',
		//    [odb_dic] nvarchar(15) default '',
		//    [odb_Odberatel] bit default 0,
		//    [odb_Dodavatel] bit default 0
		//);
		//create index IDX_CARCODE on [CZMST090] ([odb_carcode]);
		//create index IDX_DESC on [CZMST090] ([odb_desc]);
		//create index IDX_ID on [CZMST090] ([odb_id]);

		#endregion

		private static string TableName = "CZMST090";
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST090 = new Dictionary<string, ColumnType>();

		#region c'tor

		static Odberatele()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Odberatele.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName.Trim())
            //        {
            //            ColumnsInfo_CZMST090.AddIfNotExists(dr);
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

