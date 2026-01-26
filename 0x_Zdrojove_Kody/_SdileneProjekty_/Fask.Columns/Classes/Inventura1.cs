using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
	public  class Inventura1
	{

		#region SQL script

		//create table [CZMST_I1]
		//(
		//    [CountEntries] int not null,
		//    [ITEMNMBR] nvarchar(40) not null,
		//    [CZ_CarKod] nvarchar(31) not null,
		//    [ITEMDESC] nvarchar(100) not null,
		//    [LOCNCODE] nvarchar(11) not null,
		//    [QUANTITY] numeric not null,
		//    [DATEDONE] datetime not null,
		//    [IntegerValue] smallint not null,
		//    [TIMESPRT] smallint not null,
		//    [CZ_SerNum_Track] tinyint not null,
		//    [CZ_SerNum_Find] tinyint not null,
		//    [DEX_ROW_ID] int not null,
		//    [TerminalID] tinyint not null,
		//    [O_TID] tinyint,
		//    [skl_id] nvarchar(20),
		//    [DMJ] nvarchar(10) default '',
		//    [REZ_1] nvarchar(21),
		//    [REZ_2] nvarchar(21),
		//    [ITEMCODE] nvarchar(50),
		//    [CZ_REZ_1_Track] tinyint,
		//    [CZ_REZ_2_Track] tinyint
		//);
		//create index IDX_CZCarKod on [CZMST_I1] ([CZ_CarKod]);
		//create index IDX_ITEMNMBR on [CZMST_I1] ([ITEMNMBR]);

		//create table [CZMST_I1H]
		//(
		//    [CountEntries] int not null,
		//    [Description] nvarchar(100),
		//    [Status] tinyint not null default 0
		//);
		//alter table [CZMST_I1H] add primary key ([CountEntries]);

		//create table [CZMST_I2]
		//(
		//    [CountEntries] int not null,
		//    [ITEMNMBR] nvarchar(40) not null,
		//    [SERLNMBR] nvarchar(50) not null,
		//    [DEX_ROW_ID] int not null,
		//    [QTY] numeric
		//);
		//create index IDX_ITEMNMBR on [CZMST_I2] ([ITEMNMBR]);
		//create index IDX_SERLNMBR on [CZMST_I2] ([SERLNMBR]);

		//create table [CZMST_I3]
		//(
		//    [CountEntries] int,
		//    [ITEMNMBR] nvarchar(40),
		//    [CZ_CarKod] nvarchar(31),
		//    [QTYPACK] numeric,
		//    [VENDORID] nvarchar(15) not null,
		//    [VNDITNUM] nvarchar(31),
		//    [VENDNAME] nvarchar(31) not null,
		//    [DEX_ROW_ID] int not null,
		//    [MJ] nvarchar(10) not null default '',
		//    [WEIGHT] numeric
		//);
		//create index IDX_1 on [CZMST_I3] ([CZ_CarKod],[VNDITNUM]);
		//create index IDX_2 on [CZMST_I3] ([VNDITNUM],[CZ_CarKod]);
		//create index IDX_CZCarKod on [CZMST_I3] ([CZ_CarKod]);
		//create index IDX_ITEMNMBR on [CZMST_I3] ([ITEMNMBR]);
		//create index IDX_VNDITNUM on [CZMST_I3] ([VNDITNUM]);

		//create table [CZMST_I4]
		//(
		//    [CountEntries] int not null,
		//    [ITEMNMBR] nvarchar(40) not null,
		//    [CZ_CarKod] nvarchar(31) not null,
		//    [LOCNCODE] nvarchar(11) not null,
		//    [VNDITNUM] nvarchar(31) not null,
		//    [QUANTITY] numeric not null,
		//    [QTYPACK] numeric,
		//    [SERLNMBR] nvarchar(50) not null,
		//    [DATEDONE] nvarchar(8),
		//    [TIMEDONE] nvarchar(6),
		//    [USERID] int,
		//    [DEX_ROW_ID] int not null,
		//    [GUID] uniqueidentifier,
		//    [O_Checked] bit not null default 0,
		//    [skl_id] nvarchar(20),
		//    [MJ] nvarchar(10) not null default '',
		//    [QUANTITYMJ] numeric not null,
		//    [INPUT_MODE] tinyint not null,
		//    [ID_TERMINAL] int not null,
		//    [ITEMCODE] nvarchar(50),
		//    [REZ_1] nvarchar(50),
		//    [REZ_2] nvarchar(50),
		//    [WEIGHT] numeric
		//);
		//create index IDX_DATUMCAS on [CZMST_I4] ([DATEDONE],[TIMEDONE]);
		//create index IDX_ITEMNMBR on [CZMST_I4] ([ITEMNMBR]);
		//create index IDX_SERLNMBR on [CZMST_I4] ([SERLNMBR]);

		//create table [CZMST_IH]
		//(
		//    [CountEntries] int not null,
		//    [GUID] uniqueidentifier not null
		//);

		//create table [Parametry]
		//(
		//    [CFG_UpozornitNaPrebytek] bit not null,
		//    [CFG_DalsiPolozkuBezDotazu] bit not null,
		//    [CFG_KontrolaUplnostiPolozky] bit not null,
		//    [CFG_PoZadaniSNZpetNaMN] bit not null,
		//    [CFG_PredvyplnitMnozstvi] bit not null,
		//    [CFG_PredvyplnitMnozstviZbyvajici] bit not null,
		//    [CFG_PredvyplnitMnozstviOJedna] bit not null,
		//    [CFG_PovolitDuplicituSN] bit not null,
		//    [CFG_MnozstviScannerem] bit not null,
		//    [CFG_PosunNaDalsiPolozku] bit not null,
		//    [CFG_PovolitZaporneMnozstvi] bit not null,
		//    [CFG_KontrolaUplnosti] bit not null,
		//    [CFG_PovolitZmenuLokace] bit not null default 0,
		//    [CFG_PovolitZobrazeniMnozstviNaSklade] bit,
		//    [CONFIG_LOKACE_POVOLIT] bit,
		//    [CONFIG_LOKACE_TIMEOUT] int
		//);

		#endregion


		private  string TableName_I1 = "CZMST_I1";
		private  string TableName_I1H = "CZMST_I1H";
		private  string TableName_I2 = "CZMST_I2";
		private  string TableName_I3 = "CZMST_I3";
		private  string TableName_I4 = "CZMST_I4";
		private  string TableName_IH = "CZMST_IH";
		private  string TableName_P = "Parametry";


		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_I1 = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_I1H = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_I2 = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_I3 = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_I4 = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_IH = new Dictionary<string, ColumnType>();
		public  Dictionary<string, ColumnType> ColumnsInfo_Parametry = new Dictionary<string, ColumnType>();

		#region c'tor

		public Inventura1(DS_Information ds)
		{


            #region I1

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_I1 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_I1.Trim());

            foreach (var dr in DT_I1)
            {
                ColumnsInfo_CZMST_I1.AddIfNotExists(dr);
            }

            #endregion

            #region I1H

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_I1H = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_I1H.Trim());

            foreach (var dr in DT_I1H)
            {
                ColumnsInfo_CZMST_I1H.AddIfNotExists(dr);
            }

            #endregion

            #region I2

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_I2 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_I2.Trim());

            foreach (var dr in DT_I2)
            {
                ColumnsInfo_CZMST_I2.AddIfNotExists(dr);
            }

            #endregion

            #region I3

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_I3 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_I3.Trim());

            foreach (var dr in DT_I3)
            {
                ColumnsInfo_CZMST_I3.AddIfNotExists(dr);
            }

            #endregion

            #region I4

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_I4 = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_I4.Trim());

            foreach (var dr in DT_I4)
            {
                ColumnsInfo_CZMST_I4.AddIfNotExists(dr);
            }

            #endregion

            #region IH

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_IH = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_IH.Trim());

            foreach (var dr in DT_IH)
            {
                ColumnsInfo_CZMST_IH.AddIfNotExists(dr);
            }

            #endregion

            #region Parametry

            EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_Param = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_P.Trim());

            foreach (var dr in DT_Param)
            {
                ColumnsInfo_Parametry.AddIfNotExists(dr);
            }

            #endregion
		}

		#endregion

	}
}

