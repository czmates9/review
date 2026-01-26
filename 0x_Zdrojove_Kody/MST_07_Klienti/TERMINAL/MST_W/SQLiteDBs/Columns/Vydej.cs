using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Fask.SQLiteDBs.Columns
{
	public static class Vydej
	{

		#region SQL script

		//create table [CZMST_SE]
		//(
		//    [CountEntries] int not null,
		//    [SOPNUMBE] nvarchar(17) not null,
		//    [ITEMNMBR] nvarchar(40),
		//    [ITEMTYPE] nvarchar(11) not null default (''),
		//    [ITEMDESC] nvarchar(100),
		//    [VNDDOCNM] nvarchar(21),
		//    [VNDITNUM] nvarchar(31),
		//    [ORD] int not null,
		//    [CZ_CarKod] nvarchar(31) not null,
		//    [LOCNCODE] nvarchar(11),
		//    [QTYSHPPD] numeric not null,
		//    [QTYPACK] numeric not null,
		//    [CZ_DatVyr_Track] tinyint not null,
		//    [CZ_DatVyr_Delka] smallint not null,
		//    [CZ_SerNum_Track] tinyint not null,
		//    [CZ_SerNum_Delka] smallint not null,
		//    [CZ_SW_Track] tinyint not null,
		//    [CZ_SW_Delka] smallint not null,
		//    [CZ_Doslo] tinyint not null,
		//    [DEX_ROW_ID] int not null,
		//    [Note] nvarchar(100) not null default '',
		//    [TYPEPAL] nvarchar(4),
		//    [QTYPAL] numeric,
		//    [PRINTED] tinyint default 0,
		//    [PRIORITY] tinyint not null default 3,
		//    [SKL_ID] nvarchar(20),
		//    [MJ] nvarchar(10),
		//    [CZ_REZ1_TRACK] tinyint,
		//    [CZ_REZ2_TRACK] tinyint,
		//    [ITEMCODE] nvarchar(50),
		//    [WEIGHT] numeric
		//);
		//create index CZCARKOD on [CZMST_SE] ([CZ_CarKod]);
		//create index ITEMNMBR on [CZMST_SE] ([ITEMNMBR]);
		//create index [Key] on [CZMST_SE] ([CountEntries],[SOPNUMBE],[ITEMNMBR],[ORD]);
		//create index VNDITNUM on [CZMST_SE] ([VNDITNUM]);

		//create table [CZMST_SE_SN]
		//(
		//    [CountEntries] int not null,
		//    [ITEMNMBR] nvarchar(40),
		//    [SERLNMBR] nvarchar(50) not null,
		//    [DEX_ROW_ID] int not null,
		//    [QTY] numeric default 1,
		//    [SOPNUMBE] nvarchar(17),
		//    [ORD] int
		//);
		//create index IDX_ITEM_SN on [CZMST_SE_SN] ([ITEMNMBR],[SERLNMBR]);
		//create index IDX_ITEMNMBR on [CZMST_SE_SN] ([ITEMNMBR]);

		//create table [CZMST_SEH]
		//(
		//    [CountEntries] int not null,
		//    [GUID] uniqueidentifier
		//);
		//alter table [CZMST_SEH] add primary key ([CountEntries]);

		//create table [CZMST_SI]
		//(
		//    [CountEntries] int not null,
		//    [SOPNUMBE] nvarchar(17) not null,
		//    [ITEMNMBR] nvarchar(40),
		//    [ORD] int not null,
		//    [VNDDOCNM] nvarchar(21),
		//    [VNDITNUM] nvarchar(31),
		//    [CZ_CarKod] nvarchar(31),
		//    [LOCNCODE] nvarchar(11),
		//    [QTYSHPPD] numeric not null,
		//    [QTYPACK] numeric not null,
		//    [QTYSHPPDMJ] numeric not null,
		//    [SERLTNUM] nvarchar(50) not null,
		//    [KOD_SW] nvarchar(11),
		//    [DAT_VYROBY] nvarchar(11),
		//    [REZ_1] nvarchar(21),
		//    [ODBER_ID] nvarchar(12),
		//    [DATEDONE] nvarchar(8),
		//    [TIMEDONE] nvarchar(6),
		//    [USER_ID] int not null,
		//    [DEX_ROW_ID] int identity not null,
		//    [guid] uniqueidentifier,
		//    [TYPEPAL] nvarchar(4),
		//    [NMBRPAL] nvarchar(20),
		//    [PRINTED] bit default 0,
		//    [REZ_2] nvarchar(21) default '',
		//    [INPUT_MODE] tinyint not null,
		//    [ID_TERMINAL] int not null,
		//    [SKL_ID] nvarchar(20),
		//    [MJ] nvarchar(10),
		//    [ITEMCODE] nvarchar(50),
		//    [WEIGHT] numeric
		//);
		//create unique index IX_CZMST_SI on [CZMST_SI] ([guid]);
		//create index [Key] on [CZMST_SI] ([SOPNUMBE],[ITEMNMBR],[ORD],[SERLTNUM]);

		//create table [CZMST_SIH]
		//(
		//    [CountEntries] int not null,
		//    [TISKARNA_NAME] nvarchar(30),
		//    [PRAC_ID] nvarchar(30)
		//);
		//alter table [CZMST_SIH] add primary key ([CountEntries]);

		//create table [Parametry]
		//(
		//    [ENABLE_LISTSNIM] bit not null,
		//    [CONFIG_LISTSNIM] bit not null,
		//    [CONFIG_POKRDOHLED] bit not null,
		//    [CONFIG_PTATSE_NEANO] bit not null,
		//    [CONFIG_KONT_UPL_POL] bit not null,
		//    [CONFIG_ZADAT_MN_POKAZDE] bit not null,
		//    [ENABLE_DOHLED_ODB] bit not null,
		//    [CONFIG_DOHLED_ODB] bit not null,
		//    [CONFIG_KONT_DOKONCENOSTI] bit not null,
		//    [CONFIG_KONT_DELKA] bit not null,
		//    [CONFIG_KONT_SN_CARKOD] bit not null,
		//    [CONFIG_DUPLIC_SN] bit not null,
		//    [CONFIG_NOVA_KARTA] bit not null,
		//    [CONFIG_SKRYT_MNOZSTVI] bit not null,
		//    [CONFIG_SNIMEJ_PRI_LISTU] bit not null,
		//    [CONFIG_KONT_NUL_DELKA] bit not null,
		//    [CONFIG_SNIM_LOCNCODE] bit not null,
		//    [CONFIG_MNOZSTVI_PREDVYPLNIT] bit not null default 0,
		//    [CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA] bit not null default 0,
		//    [CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI] bit not null default 0,
		//    [CONFIG_MNOZSTVI_ZADAVAT] bit not null default 1,
		//    [CONFIG_MNOZSTVI_SCANNEREM] bit default 0,
		//    [CONFIG_KONT_PREDLOHA_SN] bit,
		//    [CONFIG_LOCNCODE_OVERIT_SCANEREM] bit,
		//    [CONFIG_POUZIT_CISELNIK_ZBOZI] bit not null,
		//    [CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU] nvarchar(15),
		//    [CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU] nvarchar(15),
		//    [CONFIG_LOKACE_POVOLIT] bit,
		//    [CONFIG_LOKACE_TIMEOUT] int
		//);

		#endregion

		private static string TableName_SE = "CZMST_SE";
		private static string TableName_SE_SN = "CZMST_SE_SN";
		private static string TableName_SEH = "CZMST_SEH";
		private static string TableName_SI = "CZMST_SI";
		private static string TableName_SIH = "CZMST_SIH";
		private static string TableName_Param = "Parametry";

		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_SE = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_SE_SN = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_SEH = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_SI = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_CZMST_SIH = new Dictionary<string, ColumnType>();
		public static Dictionary<string, ColumnType> ColumnsInfo_Parametry = new Dictionary<string, ColumnType>();

		#region c'tor

		static Vydej()
		{

            //try
            //{
            //    System.Data.SqlServerCe.SqlCeDataAdapter a = new System.Data.SqlServerCe.SqlCeDataAdapter();
            //    a.SelectCommand = new System.Data.SqlServerCe.SqlCeCommand(
            //        "Select * from information_schema.columns ",
            //        new System.Data.SqlServerCe.SqlCeConnection("Data source=" + Path.Combine(Main.SqlCEDBsDir, "Vydej.sdf"))
            //    );
            //    DataSet ds = new DataSet();
            //    a.Fill(ds);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_SE.Trim())
            //        {
            //            ColumnsInfo_CZMST_SE.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_SE_SN.Trim())
            //        {
            //            ColumnsInfo_CZMST_SE_SN.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_SEH.Trim())
            //        {
            //            ColumnsInfo_CZMST_SEH.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_SI.Trim())
            //        {
            //            ColumnsInfo_CZMST_SI.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_SIH.Trim())
            //        {
            //            ColumnsInfo_CZMST_SIH.AddIfNotExists(dr);
            //            continue;
            //        }
            //        if (((string)dr["TABLE_NAME"]).Trim() == TableName_Param.Trim())
            //        {
            //            ColumnsInfo_Parametry.AddIfNotExists(dr);
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

