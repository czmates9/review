using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Prijem
	{

		#region SQL script

		//create table [CZMST_PE]
		//(
		//    [CountEntries] int not null,
		//    [PONUMBER] nvarchar(100) not null,
		//    [ITEMNMBR] nvarchar(40),
		//    [ITEMDESC] nvarchar(100),
		//    [ORD] int not null,
		//    [VNDDOCNM] nvarchar(21),
		//    [VNDITNUM] nvarchar(31),
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
		//    [Nasnimano] numeric not null default 0,
		//    [MJ] nvarchar(10),
		//    [SKL_ID] nvarchar(20),
		//    [WEIGHT] numeric,
		//    [NMBRPAL] nvarchar(50),
		//    [TYPEPAL] nvarchar(10),
		//    [ITEMCODE] nvarchar(50),
		//    [SERLTNUM] nvarchar(50) not null,
		//    [CZ_REZ1_TRACK] tinyint default 0,
		//    [CZ_REZ2_TRACK] tinyint default 0
		//);
		//create index CaroveKody on [CZMST_PE] ([VNDITNUM],[CZ_CarKod]);
		//create index CaroveKodyPalety on [CZMST_PE] ([VNDITNUM],[CZ_CarKod],[NMBRPAL]);
		//create index Nazev on [CZMST_PE] ([ITEMDESC]);

		//create table [CZMST_PE_SN]
		//(
		//    [CountEntries] int not null,
		//    [ITEMNMBR] nvarchar(40) not null,
		//    [SERLNMBR] nvarchar(50) not null,
		//    [DEX_ROW_ID] int not null
		//);

		//create table [CZMST_PEH]
		//(
		//    [CountEntries] nvarchar(30) not null,
		//    [GUID] uniqueidentifier
		//);
		//alter table [CZMST_PEH] add primary key ([CountEntries]);

		//create table [CZMST_PI]
		//(
		//    [CountEntries] int not null,
		//    [PONUMBER] nvarchar(30) not null,
		//    [ORD] int not null,
		//    [ITEMNMBR] nvarchar(40),
		//    [VNDDOCNM] nvarchar(21),
		//    [VNDITNUM] nvarchar(31),
		//    [LOCNCODE] nvarchar(11),
		//    [QTYSHPPD] numeric not null,
		//    [QTYPACK] numeric not null,
		//    [SERLTNUM] nvarchar(50) not null,
		//    [KOD_SW] nvarchar(11),
		//    [DAT_VYROBY] nvarchar(11),
		//    [DATEDONE] nvarchar(8),
		//    [TIMEDONE] nvarchar(6),
		//    [CZ_CarKod] nvarchar(31),
		//    [REZ_1] nvarchar(21),
		//    [REZ_2] nvarchar(21),
		//    [USER_ID] int not null,
		//    [DEX_ROW_ID] int identity not null,
		//    [guid] uniqueidentifier not null,
		//    [INPUT_MODE] tinyint not null,
		//    [ID_TERMINAL] int not null,
		//    [MJ] nvarchar(10),
		//    [QTYSHPPDMJ] numeric not null,
		//    [SKL_ID] nvarchar(20),
		//    [WEIGHT] numeric,
		//    [NMBRPAL] nvarchar(50),
		//    [TYPEPAL] nvarchar(10),
		//    [ITEMCODE] nvarchar(50)
		//);
		//create index Vyhledavani1 on [CZMST_PI] ([PONUMBER],[ORD],[ITEMNMBR],[SERLTNUM]);
		//create index Vyhledavani2 on [CZMST_PI] ([ITEMNMBR],[SERLTNUM]);

		//create table [CZMST_PI_F]
		//(
		//    [IMG_NAME] nvarchar(30) not null,
		//    [GUID] uniqueidentifier not null,
		//    [DEX_ROW_ID] int identity not null
		//);

		//create table [CZMST_PIH]
		//(
		//    [CountEntries] int not null,
		//    [DATUMDOKLADU] datetime
		//);

		//create table [Parametry]
		//(
		//    [CONFIG_DUPLIC_SN] bit not null,
		//    [CONFIG_KONT_DELKA] bit not null,
		//    [CONFIG_KONT_DOKONCENOSTI] bit not null,
		//    [CONFIG_KONT_NUL_DELKA] bit not null,
		//    [CONFIG_KONT_UPL_POL] bit not null,
		//    [CONFIG_LISTSNIM] bit not null,
		//    [CONFIG_NOVA_KARTA] bit not null,
		//    [CONFIG_POKRDOHLED] bit not null,
		//    [CONFIG_PRIM_KEY1] bit not null,
		//    [CONFIG_PTATSE_NEANO] bit not null,
		//    [CONFIG_SNIM_LOCNCODE] bit not null,
		//    [CONFIG_SNIM_PONUMBER] bit not null default 0,
		//    [CONFIG_SNIM_REZ2] bit not null default 0,
		//    [CONFIG_SNIMAT_POL1] bit not null default 0,
		//    [CONFIG_SNIMAT_POL2] bit not null default 0,
		//    [CONFIG_SNIMAT_POL3] bit not null default 0,
		//    [CONFIG_SNIMATZADAT_SN] bit not null default 0,
		//    [CONFIG_ZADAT_MN_POKAZDE] bit not null default 0,
		//    [ENABLE_LISTSNIM] bit not null default 0,
		//    [ENABLE_SNIMATZADAT_SN] bit not null default 0,
		//    [CONFIG_MNOZSTVI_PREDVYPLNIT] bit not null default 0,
		//    [CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA] bit not null default 0,
		//    [CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI] bit not null default 0,
		//    [CONFIG_MNOZSTVI_ZADAVAT] bit not null default 0,
		//    [CONFIG_MNOZSTVI_SCANNEREM] bit not null default 0,
		//    [CONFIG_LOKACE_POVOLIT] bit,
		//    [CONFIG_LOKACE_TIMEOUT] int,
		//    [CONFIG_LOKACE_PRIJMOVA] bit
		//);

		#endregion

		private  string TableName_PE = "CZMST_PE";
		private  string TableName_PE_SN = "CZMST_PE_SN";
		private  string TableName_PEH = "CZMST_PEH";
		private  string TableName_PI = "CZMST_PI";
		private  string TableName_PI_F = "CZMST_PI_F";
		private  string TableName_PIH = "CZMST_PIH";
		private  string TableName_Param = "Parametry";


		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_PE = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_PE_SN = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_PEH = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_PI = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_PI_F = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_PIH = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_Parametry = new Dictionary<string, ColumnType>();

		#region c'tor

		public Prijem(DS_Information ds)
		{

            #region z XML souboru

            #region PE

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_1 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_PE.Trim());

            foreach (var dr in DT_1)
            {
                ColumnsInfo_CZMST_PE.AddIfNotExists(dr);
            }

            #endregion

            #region PESN
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_2 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_PE_SN.Trim());

            foreach (var dr in DT_2)
            {
                ColumnsInfo_CZMST_PE_SN.AddIfNotExists(dr);
            }
            #endregion

            #region PEH
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_3 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_PEH.Trim());

            foreach (var dr in DT_3)
            {
                ColumnsInfo_CZMST_PEH.AddIfNotExists(dr);
            }
            #endregion

            #region PI

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_4 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_PI.Trim());

            foreach (var dr in DT_4)
            {
                ColumnsInfo_CZMST_PI.AddIfNotExists(dr);
            }
            #endregion

            #region PIF
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_5 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_PI_F.Trim());

            foreach (var dr in DT_5)
            {
                ColumnsInfo_CZMST_PI_F.AddIfNotExists(dr);
            }
            #endregion

            #region PIH
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_6 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_PIH.Trim());

            foreach (var dr in DT_6)
            {
                ColumnsInfo_CZMST_PIH.AddIfNotExists(dr);
            }
            #endregion

            #region Params
            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_7 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_Param.Trim());

            foreach (var dr in DT_7)
            {
                ColumnsInfo_Parametry.AddIfNotExists(dr);
            }
            #endregion


            #endregion


		}

		#endregion

	}
}

