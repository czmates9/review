using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Prodej
	{

		#region SQL script

		//create table [CZMST_DEH]
		//(
		//    [CountEntries] int not null,
		//    [GUID] uniqueidentifier
		//);
		//alter table [CZMST_DEH] add primary key ([CountEntries]);

		//create table [CZMST_DI]
		//(
		//    [CountEntries] int not null,
		//    [ODB_ID] nvarchar(12),
		//    [STR_ID] nvarchar(30),
		//    [DOC_ID] nvarchar(12),
		//    [ITEMNMBR] nvarchar(40) not null,
		//    [LOCNCODE] nvarchar(11),
		//    [QTYSHPPD] numeric not null,
		//    [QTYPACK] numeric,
		//    [SERLTNUM] nvarchar(50) not null,
		//    [TAXAMPIE] numeric,
		//    [AMOUNPIE] numeric,
		//    [WITHTAX] tinyint,
		//    [PRICEX] tinyint,
		//    [REZ_1] nvarchar(21),
		//    [REZ_2] nvarchar(21),
		//    [REZ_3] nvarchar(21),
		//    [REZ_4] nvarchar(21),
		//    [USER_ID] int,
		//    [DATEDONE] nvarchar(8),
		//    [TIMEDONE] nvarchar(6),
		//    [DEX_ROW_ID] int identity not null,
		//    [guid] uniqueidentifier not null,
		//    [VNDITNUM] nvarchar(31),
		//    [CZ_CarKod] nvarchar(31),
		//    [SKL_ID] nvarchar(30),
		//    [MJ] nvarchar(10) not null default '',
		//    [QTYSHPPDMJ] numeric not null,
		//    [PRAC_ID] nvarchar(30),
		//    [INPUT_MODE] tinyint not null,
		//    [ID_TERMINAL] int not null,
		//    [TYPEPAL] nvarchar(10),
		//    [NMBRPAL] nvarchar(50),
		//    [ITEMCODE] nvarchar(50),
		//    [DOC_ID2] nvarchar(12) default '',
		//    [mena_ID] nvarchar(10),
		//    [TAXAMPIEM] numeric,
		//    [AMOUNPIEM] numeric,
		//    [mena_IDM] nvarchar(10),
		//    [ITEMDESC] nvarchar(100),
		//    [LOCNCODEDEST] nvarchar(11),
		//    [SKL_ID_DEST] nvarchar(20),
		//    [WEIGHT] numeric,
		//    [PRINTED] bit not null
		//);
		//create index IDX_ITEMNMBR on [CZMST_DI] ([ITEMNMBR]);

		//create table [CZMST_DI_RFID]
		//(
		//    [ID] int identity not null,
		//    [ITEMNMBR] nvarchar(40) not null,
		//    [SEQUENCENMBR] int not null,
		//    [SKL_ID] nvarchar(20),
		//    [CountEntries] int,
		//    [DOCUMENTNMBR] nvarchar(20),
		//    [ORD] int,
		//    [SERLNMBR] nvarchar(50),
		//    [guid] uniqueidentifier not null,
		//    [M_ID] nvarchar(100),
		//    [M_TID] nvarchar(100),
		//    [M_EPC] nvarchar(100),
		//    [M_USER] nvarchar(150),
		//    [M_RESERVED] nvarchar(100),
		//    [O_M_ID] nvarchar(100),
		//    [O_M_TID] nvarchar(100),
		//    [O_M_EPC] nvarchar(100),
		//    [O_M_USER] nvarchar(150),
		//    [O_M_RESERVED] nvarchar(100),
		//    [TerminalID] int not null,
		//    [UserID] int not null,
		//    [Created_T] datetime not null
		//);
		//create unique index UQ__CZMST_DI_RFID__00000000000001B6 on [CZMST_DI_RFID] ([guid]);

		//create table [CZMST_DIH]
		//(
		//    [Zakazka_ID] nvarchar(50),
		//    [Paleta_ID] nvarchar(50),
		//    [mena_ID] nvarchar(10),
		//    [SKL_ID] nvarchar(20),
		//    [CountEntries] int not null
		//);

		#endregion

		private  string TableName_DEH = "CZMST_DEH";
		private  string TableName_DI = "CZMST_DI";
		private  string TableName_DIRFID = "CZMST_DI_RFID";
		private  string TableName_DIH = "CZMST_DIH";


		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_DEH = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_DI = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_DI_RFID = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_DIH = new Dictionary<string, ColumnType>();

		#region c'tor

		public Prodej(DS_Information ds)
		{
            #region z XML souboru


            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_1 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_DEH.Trim());

            foreach (var dr in DT_1)
            {
                ColumnsInfo_CZMST_DEH.AddIfNotExists(dr);
            }

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_2 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_DI.Trim());

            foreach (var dr in DT_2)
            {
                ColumnsInfo_CZMST_DI.AddIfNotExists(dr);
            }

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_3 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_DIRFID.Trim());

            foreach (var dr in DT_3)
            {
                ColumnsInfo_CZMST_DI_RFID.AddIfNotExists(dr);
            }

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_4 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_DIH.Trim());

            foreach (var dr in DT_4)
            {
                ColumnsInfo_CZMST_DIH.AddIfNotExists(dr);
            }



            #endregion
		}

		#endregion

	}
}

