using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Zbozi
	{

		#region SQL script
		//create table [CZMST095]
		//(
		//    [ITEMNMBR] nvarchar(40) not null,
		//    [ITEMDESC] nvarchar(100),
		//    [VNDITNUM] nvarchar(31),
		//    [CZ_CarKod] nvarchar(31),
		//    [LOCNCODE] nvarchar(11),
		//    [QTY] numeric not null,
		//    [QTYPACK] numeric,
		//    [TAXRATE] numeric,
		//    [PRICE0] numeric,
		//    [PRICE1] numeric,
		//    [PRICE2] numeric,
		//    [PRICE3] numeric,
		//    [PRICE4] numeric,
		//    [PRICE5] numeric,
		//    [CZ_SerNum_Track] tinyint not null,
		//    [CZ_SerNum_Delka] smallint not null,
		//    [CZ_Rez1_Track] tinyint not null default 0,
		//    [CZ_Rez2_Track] tinyint not null default 0,
		//    [CZ_Rez3_Track] tinyint not null default 0,
		//    [CZ_Rez4_Track] tinyint not null default 0,
		//    [DEX_ROW_ID] int not null,
		//    [SKL_ID] nvarchar(20),
		//    [MJ] nvarchar(10) not null default '',
		//    [DMJ] nvarchar(200) not null default '',
		//    [REZ1] nvarchar(21),
		//    [ITEMCODE] nvarchar(50),
		//    [ODB_ID] nvarchar(12),
		//    [REZ2] nvarchar(21),
		//    [REZ3] nvarchar(21),
		//    [REZ4] nvarchar(21),
		//    [MENA_ID] nvarchar(10),
		//    [SERLTNUM] nvarchar(50),
		//    [WEIGHT] numeric
		//);
		//create index IDX_CarkodHledani on [CZMST095] ([VNDITNUM],[CZ_CarKod]);
		//create index IDX_CarkodHledani2 on [CZMST095] ([CZ_CarKod],[VNDITNUM]);
		//create index IDX_CZCARKOD on [CZMST095] ([CZ_CarKod]);
		//create index IDX_ITEMCODE on [CZMST095] ([ITEMCODE]);
		//create index IDX_ITEMDESC on [CZMST095] ([ITEMDESC]);
		//create index IDX_ITEMNMBR on [CZMST095] ([ITEMNMBR]);
		//create index IDX_ODB_ID on [CZMST095] ([ODB_ID]);
		//create index IDX_SERLTNUM on [CZMST095] ([SERLTNUM]);
		//create index IDX_VNDITNUM on [CZMST095] ([VNDITNUM]);

		//create table [CZMST095M]
		//(
		//    [ITEMNMBR] nvarchar(40) not null,
		//    [mena_ID] nvarchar(10) not null,
		//    [PRICE] numeric,
		//    [PRICEX] int,
		//    [DEX_ROW_ID] int not null
		//);
		//create index IDX_ITEM on [CZMST095M] ([ITEMNMBR]);
		//create index IDX_ItemMena on [CZMST095M] ([ITEMNMBR],[mena_ID]);
		//create index IDX_ItemMenaPricex on [CZMST095M] ([ITEMNMBR],[mena_ID],[PRICEX]);
		//create index IDX_Mena on [CZMST095M] ([mena_ID]);

		#endregion

		private static string TableName = "CZMST095";
		private static string TableNameM = "CZMST095M";

		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST095 = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST095M = new Dictionary<string, ColumnType>();

		#region c'tor

		static Zbozi()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Zbozi.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName.Trim())
            //        {
            //            ColumnsInfo_CZMST095.AddIfNotExists(dr);
            //            continue;
            //        }
					
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableNameM.Trim())
            //        {
            //            ColumnsInfo_CZMST095M.AddIfNotExists(dr);
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

